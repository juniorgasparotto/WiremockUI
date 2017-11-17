using java.io;
using java.lang;
using java.security;
using java.util;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System;

namespace WiremockUI
{
    public static class SslHelper
    {
        public const string GLOBAL = null;
        public const string NONE = "NONE";

        /**
        * Check if cacerts has a invalid certificate
        */
        public static string GetCurrentJavaProperty()
        {
            File storeFile = null;
            FileInputStream fis = null;
            var props = new Dictionary<string, string>();
            string sep = File.separator;

            props.Add("trustStore", java.lang.System.getProperty("javax.net.ssl.trustStore"));
            props.Add("javaHome", java.lang.System.getProperty("java.home"));
            props.Add("trustStoreType", java.lang.System.getProperty("javax.net.ssl.trustStoreType", KeyStore.getDefaultType()));
            props.Add("trustStoreProvider", java.lang.System.getProperty("javax.net.ssl.trustStoreProvider", ""));
            props.Add("trustStorePasswd", java.lang.System.getProperty("javax.net.ssl.trustStorePassword", ""));

            /*
                * Try:
                *      javax.net.ssl.trustStore  (if this variable exists, stop)
                *      jssecacerts
                *      cacerts
                *
                * If none exists, we use an empty keystore.
                */

            var storeFileName = props["trustStore"];
            if (storeFileName == null)
            {
                string javaHome = props["javaHome"];
                storeFileName = javaHome + sep + "lib" + sep
                                                + "security" + sep +
                                                "jssecacerts";
                if ((fis = GetFileInputStream(new File(storeFileName))) == null)
                {
                    storeFileName = javaHome + sep + "lib" + sep
                                          + "security" + sep +
                                            "cacerts";
                }
                return storeFileName;
            }

            if (fis != null)
                fis.close();

            return storeFileName;
        }

        public static Dictionary<X509Certificate2, System.Exception> ValidateCacerts()
        {
            var dic = new Dictionary<X509Certificate2, System.Exception>();
            var jstore = KeyStore.getInstance("jks");
            jstore.load(null);
            var cf = java.security.cert.CertificateFactory.getInstance("X509");

            X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            Dictionary<string, string> aliases = new Dictionary<string, string>();
            foreach (X509Certificate2 cert in store.Certificates)
            {
                try
                {
                    dic[cert] = null;
                    if (!cert.HasPrivateKey)
                    {
                        // the alias must be unique, otherwise we overwrite the previous certificate with that alias
                        string alias = cert.Subject;
                        int unique = 0;
                        while (aliases.ContainsKey(alias))
                        {
                            alias = cert.Subject + " #" + (++unique);
                        }
                        aliases.Add(alias, null);
                        //jstore.setCertificateEntry(alias, cf.generateCertificate(new ByteArrayInputStream(cert.RawData)));
                        cf.generateCertificate(new ByteArrayInputStream(cert.RawData));
                    }
                }
                catch (System.Exception ex)
                {
                    dic[cert] = ex;
                }
            }
            store.Close();
            return dic;
        }

        public static void UseTrustStoreCacerts()
        {
            SaveTrustStore(SslHelper.GLOBAL, null);
        }

        public static void UseTrustStoreEmpty()
        {
            SaveTrustStore(SslHelper.NONE, null);
        }

        public static void SaveTrustStore(string trustStore, string pwd)
        {
            var settings = SettingsUtils.GetSettings();
            settings.TrustStoreDefault = trustStore;
            settings.TrustStorePwdDefault = pwd;
            SettingsUtils.SaveSettings(settings);
            SetTrustStore();
        }

        public static void SetTrustStore()
        {
            java.lang.System.getProperties().remove("javax.net.ssl.trustStore");
            java.lang.System.getProperties().remove("javax.net.ssl.trustStorePassword");

            var settings = SettingsUtils.GetSettings();
            if (settings.TrustStoreDefault != SslHelper.GLOBAL)
            {
                java.lang.System.setProperty("javax.net.ssl.trustStore", settings.TrustStoreDefault);
                if (!string.IsNullOrWhiteSpace(settings.TrustStorePwdDefault))
                    java.lang.System.setProperty("javax.net.ssl.trustStorePassword", settings.TrustStorePwdDefault);
            }
        }

        private static FileInputStream GetFileInputStream(File file)
        {
            try
            {
                if (file.exists())
                {
                    return new FileInputStream(file);
                }
                else
                {
                    return null;
                }
            }
            catch (FileNotFoundException e)
            {
                // couldn't find it, oh well.
                return null;
            }
        }

        /**
        * Returns the keystore with the configured CA certificates.
        * ORIGINAL VERSION: http://grepcode.com/file/repository.grepcode.com/java/root/jdk/openjdk/6-b27/sun/security/ssl/TrustManagerFactoryImpl.java
        */
        //public static KeyStore GetCacertsKeyStore(KeyStore ks)
        //{
        //    if (ks == null)
        //    {

        //        File storeFile = null;
        //        FileInputStream fis = null;
        //        var props = new Dictionary<string, string>();
        //        string sep = File.separator;

        //        //java.lang.System.setProperty("java.home", @"C:\Program Files\Java\jdk1.8.0_121\jre");

        //        //var password = "123456";
        //        //var path = @"D:\cert\keystore.jks";

        //        //KeyStore trustStore = KeyStore.getInstance(KeyStore.getDefaultType());
        //        //var instream = new FileInputStream(path);
        //        //trustStore.load(instream, password.ToCharArray());

        //        props.Add("trustStore", java.lang.System.getProperty("javax.net.ssl.trustStore"));
        //        props.Add("javaHome", java.lang.System.getProperty("java.home"));
        //        props.Add("trustStoreType", java.lang.System.getProperty("javax.net.ssl.trustStoreType", KeyStore.getDefaultType()));
        //        props.Add("trustStoreProvider", java.lang.System.getProperty("javax.net.ssl.trustStoreProvider", ""));
        //        props.Add("trustStorePasswd", java.lang.System.getProperty("javax.net.ssl.trustStorePassword", ""));

        //        /*
        //         * Try:
        //         *      javax.net.ssl.trustStore  (if this variable exists, stop)
        //         *      jssecacerts
        //         *      cacerts
        //         *
        //         * If none exists, we use an empty keystore.
        //         */

        //        var storeFileName = props["trustStore"];
        //        if (storeFileName != "NONE")
        //        {
        //            if (storeFileName != null)
        //            {
        //                storeFile = new File(storeFileName);
        //                fis = GetFileInputStream(storeFile);
        //            }
        //            else
        //            {
        //                string javaHome = props["javaHome"];
        //                storeFile = new File(javaHome + sep + "lib" + sep
        //                                                + "security" + sep +
        //                                                "jssecacerts");
        //                if ((fis = GetFileInputStream(storeFile)) == null)
        //                {
        //                    storeFile = new File(javaHome + sep + "lib" + sep
        //                                                + "security" + sep +
        //                                                "cacerts");
        //                    fis = GetFileInputStream(storeFile);
        //                }
        //            }

        //            if (fis != null)
        //            {
        //                storeFileName = storeFile.getPath();
        //            }
        //            else
        //            {
        //                storeFileName = "No File Available, using empty keystore.";
        //            }
        //        }

        //        var defaultTrustStoreType = props["trustStoreType"];
        //        var defaultTrustStoreProvider = props["trustStoreProvider"];

        //        /*
        //         * Try to initialize trust store.
        //         */
        //        if (defaultTrustStoreType.Length != 0)
        //        {
        //            if (defaultTrustStoreProvider.Length == 0)
        //            {
        //                ks = KeyStore.getInstance(defaultTrustStoreType);
        //            }
        //            else
        //            {
        //                ks = KeyStore.getInstance(defaultTrustStoreType,
        //                                        defaultTrustStoreProvider);
        //            }
        //            char[] passwd = null;
        //            string defaultTrustStorePassword = props["trustStorePasswd"];
        //            if (defaultTrustStorePassword.Length != 0)
        //                passwd = defaultTrustStorePassword.ToCharArray();

        //            // if trustStore is NONE, fis will be null
        //            ks.load(fis, passwd);

        //            // Zero out the temporary password storage
        //            if (passwd != null)
        //            {
        //                for (int i = 0; i < passwd.Length; i++)
        //                {
        //                    passwd[i] = (char)0;
        //                }
        //            }
        //        }

        //        if (fis != null)
        //        {
        //            fis.close();
        //        }

        //        return ks;
        //    }

        //    return null;
        //}


    }
}
