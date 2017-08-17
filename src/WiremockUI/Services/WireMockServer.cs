using com.github.tomakehurst.wiremock.common;
using com.github.tomakehurst.wiremock.http;
using com.github.tomakehurst.wiremock.matching;
using com.github.tomakehurst.wiremock.standalone;
using com.github.tomakehurst.wiremock.stubbing;
using System.Text;
using System.IO;
using System;
using com.github.tomakehurst.wiremock.core;
using com.github.tomakehurst.wiremock.http.trafficlistener;
using java.net;
using java.nio;
using java.nio.charset;
using System.Linq;

namespace WiremockUI
{
    public class WireMockServer
    {
        private com.github.tomakehurst.wiremock.WireMockServer wireMockServer;
        private ILogWriter writer;
        private bool useLogStdout;

        static WireMockServer()
        {
            java.lang.System.setProperty("wiremock.org.mortbay.log.class", "com.github.tomakehurst.wiremock.jetty.LoggerAdapter");
        }

        public WireMockServer(ILogWriter writer, bool useLogStdout = false)
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
                options = new CommandLineOptionsWithLog(writer, args);
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
            wireMockServer.addMockServiceRequestListener(new RequestListenerTest());

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
                java.lang.System.@out.println();
                java.lang.System.@out.println(options);
            }
            else
            {
                writer.Info(Helper.ResolveBreakLineInCompatibility(options.ToString()), true);
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
            private ConsoleNotifier log;
            private TrafficLog trafficLog;

            public CommandLineOptionsWithLog(ILogWriter writer, params string[] args) : base(args)
            {
                this.log = new ConsoleNotifier(this.verboseLoggingEnabled(), writer);
                this.trafficLog = new TrafficLog(args.Contains("--print-all-network-traffic"), writer);
            }

            public override WiremockNetworkTrafficListener networkTrafficListener()
            {
                return this.trafficLog;
            }

            public override Notifier notifier()
            {
                return log;
            }
        }

        private class TrafficLog : WiremockNetworkTrafficListener
        {
            private bool verbose;
            private ILogWriter writer;
            private static Charset charset = Charset.forName("UTF-8");
            private static CharsetDecoder decoder = charset.newDecoder();

            public TrafficLog(bool verbose, ILogWriter writer)
            {
                this.verbose = verbose;
                this.writer = writer;

                if (verbose)
                    writer.Info(FormatMessage("Enable print all raw incoming and outgoing network traffic"));
            }

            public void incoming(Socket socket, ByteBuffer bytes)
            {
                if (!verbose)
                    return;

                try
                {
                    writer.Info(decoder.decode(bytes).toString());
                }
                catch (CharacterCodingException e)
                {
                    writer.Error("Problem decoding network traffic: " + e.ToString());
                }
            }

            public void outgoing(Socket socket, ByteBuffer bytes)
            {
                if (!verbose)
                    return;

                try
                {
                    writer.Info(decoder.decode(bytes).toString());
                }
                catch (CharacterCodingException e)
                {
                    writer.Error("Problem decoding network traffic" + e.ToString());
                }
            }

            public void opened(Socket socket)
            {
                if (!verbose)
                    return;
            }

            public void closed(Socket s)
            {
                if (!verbose)
                    return;
            }

            private static String FormatMessage(String message)
            {
                return String.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), message);
            }
        }

        private class ConsoleNotifier : Notifier
        {
            private ILogWriter writer;
            private bool verbose;

            public ConsoleNotifier(bool verbose, ILogWriter writer)
            {
                this.verbose = verbose;
                this.writer = writer;

                if (verbose)
                    info("Verbose logging enabled");
            }

            public void info(string str)
            {
                if (verbose)
                    this.writer.Info(FormatMessage(Helper.ResolveBreakLineInCompatibility(str)));
            }

            public void error(string str)
            {
                this.writer.Error(FormatMessage(Helper.ResolveBreakLineInCompatibility(str)));
            }

            public void error(string str, Exception e)
            {
                this.writer.Error(FormatMessage(Helper.ResolveBreakLineInCompatibility(str)));
                this.writer.Error(e.ToString());
            }

            private static String FormatMessage(String message)
            {
                return String.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), message);
            }
        }

        private class InternalOutput : java.io.OutputStream
        {
            private ILogWriter log;

            public InternalOutput(ILogWriter log)
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