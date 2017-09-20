using System.Linq;
using System.IO;
using WiremockUI.Data;
using com.github.tomakehurst.wiremock.stubbing;
using System.Collections.Generic;
using System;

namespace WiremockUI
{
    public class TransformUtils
    {
        private static string jarFile;
        public static string JarFile
        {
            get
            {
                if (jarFile == null)
                {
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), "Jar");
                    DirectoryInfo di = new DirectoryInfo(dir);
                    FileInfo[] files = di.GetFiles("*.jar");
                    if (files.Length > 0)
                        jarFile = files.OrderByDescending(f => f.CreationTime).FirstOrDefault().FullName;
                }

                return jarFile;
            }
        }

        public static string GetAsJavaCommand(Server server, Data.Scenario scenario, Server.PlayType type)
        {
            var argsWithQuote = server.GetArguments(scenario, type, true);
            return $@"java -jar ""{JarFile}"" {string.Join(" ", argsWithQuote)}";
        }

        public static void GetRequestElementsByMap(string baseUrl, string mapFile, out Dictionary<string, string> headers, out string body, out string method, out string url)
        {
            headers = new Dictionary<string, string>();
            body = null;
            var fileContent = File.ReadAllText(mapFile);
            var stub = StubMapping.buildFrom(fileContent);
            method = stub.getRequest().getMethod().ToString();

            var baseUri = new Uri(baseUrl);
            Uri myUri = new Uri(baseUri, stub.getRequest().getUrl());
            url = myUri.ToString();

            var iterator = stub.getRequest().getHeaders()?.keySet()?.iterator();
            if (iterator != null)
            {
                while (iterator.hasNext())
                {
                    var name = (string)iterator.next();
                    var value = (com.github.tomakehurst.wiremock.matching.MultiValuePattern)stub.getRequest().getHeaders().get(name);
                    var compareName = value.getName();
                    headers[name] = value.getExpected();
                }
            }

            var iteratorCookie = stub.getRequest().getCookies()?.keySet()?.iterator();
            if (iteratorCookie != null && !headers.Any((KeyValuePair<string, string> f) => f.Key.ToLower() == "cookies"))
            {
                headers["Cookies"] = stub.getRequest().getCookies().ToString();
            }

            var bodyPatterns = stub.getRequest().getBodyPatterns()?.toArray();
            if (bodyPatterns != null)
            {
                foreach (var bodyPattern in bodyPatterns)
                {
                    if (bodyPattern is com.github.tomakehurst.wiremock.matching.StringValuePattern converted)
                    {
                        body = converted.getExpected();
                        break;
                    }
                }
            }
        }
    }
}
