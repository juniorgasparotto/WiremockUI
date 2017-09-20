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

        public void Stop(Scenario scenario)
        {
            if (!Services.ContainsKey(scenario.Id))
                return;
            Services[scenario.Id].Stop();
            Services[scenario.Id].ShutDown();
            Services.Remove(scenario.Id);

            if (Watchers.ContainsKey(scenario.Id))
            {
                Watchers[scenario.Id].EnableRaisingEvents = false;
                Watchers.Remove(scenario.Id);
            }
        }

        public void Play(Server server, Scenario scenario, Server.PlayType type, ILogWriter textWriter, ILogTableRequestResponse logTableRequestResponse)
        {
            if (Services.ContainsKey(scenario.Id))
                Stop(scenario);

            var wiremockServer = new WireMockServer(textWriter, logTableRequestResponse, type);
            Services.Add(scenario.Id, wiremockServer);
            textWriter.WriteLine(TransformUtils.GetAsJavaCommand(server, scenario, type), System.Drawing.Color.Green, true);

            var args = server.GetArguments(scenario, type);
            wiremockServer.run(args);
            
        }

        public void AddWatchers(Scenario service, FileSystemWatcher watcher)
        {
            if (Watchers.ContainsKey(service.Id))
                Watchers.Remove(service.Id);
            Watchers.Add(service.Id, watcher);
        }

        public void Refresh(Scenario scenario)
        {
            if (!Services.ContainsKey(scenario.Id))
                return;
            Services[scenario.Id].Refresh();
        }

        public WireMockServer GetWireMockServer(Scenario scenario)
        {
            if (!Services.ContainsKey(scenario.Id))
                return null;
            return Services[scenario.Id];
        }
    }
}
