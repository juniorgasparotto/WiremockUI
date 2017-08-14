using com.github.tomakehurst.wiremock.common;
using com.github.tomakehurst.wiremock.http;
using com.github.tomakehurst.wiremock.matching;
using com.github.tomakehurst.wiremock.standalone;
using com.github.tomakehurst.wiremock.stubbing;
using System.Text;
using System.IO;
using System;
using com.github.tomakehurst.wiremock.core;

namespace WiremockUI
{
    public class WireMockServer
    {
        private com.github.tomakehurst.wiremock.WireMockServer wireMockServer;
        private TextWriter writer;

        private const string BANNER = @" /$$      /$$ /$$                     /$$      /$$                     /$$      \n" +
            "| $$  /$ | $$|__/                    | $$$    /$$$                    | $$      \n" +
            "| $$ /$$$| $$ /$$  /$$$$$$   /$$$$$$ | $$$$  /$$$$  /$$$$$$   /$$$$$$$| $$   /$$\n" +
            "| $$/$$ $$ $$| $$ /$$__  $$ /$$__  $$| $$ $$/$$ $$ /$$__  $$ /$$_____/| $$  /$$/\n" +
            "| $$$$_  $$$$| $$| $$  \\__/| $$$$$$$$| $$  $$$| $$| $$  \\ $$| $$      | $$$$$$/ \n" +
            "| $$$/ \\  $$$| $$| $$      | $$_____/| $$\\  $ | $$| $$  | $$| $$      | $$_  $$ \n" +
            "| $$/   \\  $$| $$| $$      |  $$$$$$$| $$ \\/  | $$|  $$$$$$/|  $$$$$$$| $$ \\  $$\n" +
            "|__/     \\__/|__/|__/       \\_______/|__/     |__/ \\______/  \\_______/|__/  \\__/";
        private bool useLogStdout;

        static WireMockServer()
        {
            java.lang.System.setProperty("wiremock.org.mortbay.log.class", "com.github.tomakehurst.wiremock.jetty.LoggerAdapter");
        }

        public WireMockServer(TextWriter writer, bool useLogStdout = false)
        {
            this.useLogStdout = useLogStdout;
            this.writer = writer;
        }

        public void run(params string[] args)
        {
            CommandLineOptions options;
            if (useLogStdout)
            {
                var debug = new InternalOutput(writer);
                var print = new java.io.PrintStream(debug);
                java.lang.System.setOut(print);
                options = new CommandLineOptions(args);
            }
            else
            {
                options = new CommandLineOptionsWithLog(new Log(writer), args);
            }
            
            var FILES_ROOT = @"__files";
            var MAPPINGS_ROOT = @"mappings";

            FileSource fileSource = options.filesRoot();
            fileSource.createIfNecessary();
            FileSource filesFileSource = fileSource.child(FILES_ROOT);
            filesFileSource.createIfNecessary();
            FileSource mappingsFileSource = fileSource.child(MAPPINGS_ROOT);
            mappingsFileSource.createIfNecessary();

            wireMockServer = new com.github.tomakehurst.wiremock.WireMockServer(options);
            if (options.recordMappingsEnabled())
            {
                wireMockServer.enableRecordMappings(mappingsFileSource, filesFileSource);
            }

            if (options.specifiesProxyUrl())
            {
                addProxyMapping(options.proxyUrl());
            }

            wireMockServer.start();

            if (useLogStdout)
            {
                java.lang.System.@out.println(BANNER);
                java.lang.System.@out.println();
                java.lang.System.@out.println(options);
            }
            else
            {
                options.notifier().info(BANNER);
                options.notifier().info("\r\n\r\n");
                options.notifier().info(options.ToString());
                options.notifier().info("\r\n");
            }
        }

        private void addProxyMapping(string baseUrl)
        {
            wireMockServer.loadMappingsUsing(new MappingsLoaderClass(baseUrl));
        }

        public void Stop()
        {
            wireMockServer?.stop();
        }

        public bool IsRunning()
        {
            return wireMockServer.isRunning();
        }

        public void ShutDown()
        {
            wireMockServer?.shutdown();
            wireMockServer?.shutdownServer();
        }

        private class CommandLineOptionsWithLog : CommandLineOptions
        {
            private Log log;

            public CommandLineOptionsWithLog(Log log, params string[] args) : base(args)
            {
                this.log = log;
            }

            public override Notifier notifier()
            {
                return log;
            }
        }

        private class Log : Notifier
        {
            private TextWriter writer;

            public Log(TextWriter writer)
            {
                this.writer = writer;
            }

            public void error(string str)
            {
                this.writer.Write(Helper.ResolveBreakLineIncompatibility(str));
            }

            public void error(string str, Exception t)
            {
                this.writer.Write(Helper.ResolveBreakLineIncompatibility(str));
            }

            public void info(string str)
            {
                this.writer.Write(Helper.ResolveBreakLineIncompatibility(str));
            }
        }

        private class InternalOutput : java.io.OutputStream
        {
            private TextWriter log;

            public InternalOutput(TextWriter log)
            {
                this.log = log;
            }

            public override void write(int i)
            {
                
            }
            
            public override void write(byte[] bytes, int off, int len)
            {
                var strbuilder = new StringBuilder();
                char last = default(char);
                for(var i = off; i < len; i++)
                {
                    var b = (char)bytes[i];
                    if (last != '\r' && b == '\n')
                        strbuilder.Append("\r\n");
                    else
                        strbuilder.Append(b);

                    last = b;
                }

                this.log.Write(strbuilder.ToString());
            }
        }

        private class MappingsLoaderClass : MappingsLoader
        {
            private string baseUrl;

            public MappingsLoaderClass(string baseUrl)
            {
                this.baseUrl = baseUrl;
            }

            public void loadMappingsInto(StubMappings stubMappings)
            {
                RequestPattern requestPattern = RequestPatternBuilder.newRequestPattern(RequestMethod.ANY, com.github.tomakehurst.wiremock.client.WireMock.anyUrl()).build();
                ResponseDefinition responseDef = com.github.tomakehurst.wiremock.client.ResponseDefinitionBuilder.responseDefinition()
                        .proxiedFrom(baseUrl)
                        .build();

                StubMapping proxyBasedMapping = new StubMapping(requestPattern, responseDef);
                proxyBasedMapping.setPriority(new java.lang.Integer(10));
                stubMappings.addMapping(proxyBasedMapping);
            }
        }
    }
}