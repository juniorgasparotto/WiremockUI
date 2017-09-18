using System;
using System.Collections.Generic;
using System.IO;
using WiremockUI.Data;

namespace WiremockUI
{
    public class Dashboard
    {
        public Dictionary<Guid, WireMockServer> Services { get; private set; }
        public Dictionary<Guid, FileSystemWatcher> Watchers { get; private set; }

        public Dashboard()
        {
            this.Services = new Dictionary<Guid, WireMockServer>();
            this.Watchers = new Dictionary<Guid, FileSystemWatcher>();
        }

        public bool IsAnyRunning()
        {
            foreach (var s in Services)
                if (s.Value.IsRunning())
                    return true;

            return false;
        }

        public bool IsRunning(Scenario scenario)
        {
            if (scenario == null)
                return false;

            if (!Services.ContainsKey(scenario.Id))
                return false;
            return Services[scenario.Id].IsRunning();
        }

        public void Stop(Scenario mockService)
        {
            if (!Services.ContainsKey(mockService.Id))
                return;
            Services[mockService.Id].Stop();
            Services[mockService.Id].ShutDown();
            Services.Remove(mockService.Id);

            if (Watchers.ContainsKey(mockService.Id))
            {
                Watchers[mockService.Id].EnableRaisingEvents = false;
                Watchers.Remove(mockService.Id);
            }
        }

        public void Play(Server server, Scenario scenario, Server.PlayType type, ILogWriter textWriter, ILogTableRequestResponse logTableRequestResponse)
        {
            if (Services.ContainsKey(scenario.Id))
                Stop(scenario);

            var wiremockServer = new WireMockServer(textWriter, logTableRequestResponse);
            Services.Add(scenario.Id, wiremockServer);
            textWriter.WriteLine(CopyUtils.GetAsJavaCommand(server, scenario, type), System.Drawing.Color.Green, true);

            var args = server.GetArguments(scenario, type);
            wiremockServer.run(args);
        }

        internal void AddWatchers(Scenario service, FileSystemWatcher watcher)
        {
            if (Watchers.ContainsKey(service.Id))
                Watchers.Remove(service.Id);
            Watchers.Add(service.Id, watcher);
        }
    }
}
