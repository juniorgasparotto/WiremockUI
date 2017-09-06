using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WiremockUI.Data
{
    public class Proxy
    {
        public Guid Id { get; set; }
        public string UrlTarget { get; set; }
        public int PortProxy { get; set; }
        public string Name { get; set; }
        private List<Scenario> scenario = new List<Scenario>();

        public IEnumerable<Scenario> Scenarios => scenario;
        public IEnumerable<Argument> Arguments { get; set; }

        public string GetFormattedName()
        {
            return $"{Name} (http://localhost:{PortProxy} <- {UrlTarget})";
        }

        public string GetFolderName()
        {
            return PortProxy.ToString();
        }

        public string GetUrlProxy()
        {
            return $"http://localhost:{PortProxy}";
        }

        public string GetFullPath()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), GetFolderName());
            return Path.Combine(Directory.GetCurrentDirectory(), GetFolderName());
        }

        public string GetFullPath(Scenario scenario)
        {
            return Path.Combine(GetFullPath(), scenario.GetFolderName());
        }

        public string GetMappingPath(Scenario scenario)
        {
            return Path.Combine(GetFullPath(scenario), "mappings");
        }

        public string GetBodyFilesPath(Scenario scenario)
        {
            return Path.Combine(GetFullPath(scenario), "__files");
        }

        public bool AlreadyRecord(Scenario scenario)
        {
            if (Directory.Exists(GetMappingPath(scenario)))
                return true;
            return false;
        }

        public Scenario GetDefaultScenario()
        {
            var d = scenario.FirstOrDefault(f => f.IsDefault);
            return d;
        }

        public void SetDefault(Scenario scenario)
        {
            scenario.IsDefault = true;
            foreach (var m in this.scenario)
                if (m.Id != scenario.Id)
                    m.IsDefault = false;
        }

        public Scenario GetScenarioById(Guid? id)
        {
            return scenario.FirstOrDefault(f => f.Id == id);
        }

        public void AddScenario(Scenario scenario)
        {
            if (scenario.Id == Guid.Empty)
                scenario.Id = Guid.NewGuid();

            if (this.scenario.Count == 0)
                scenario.IsDefault = true;
            this.scenario.Add(scenario);
        }

        public void RemoveScenario(Guid id)
        {
            scenario.RemoveAll(f => f.Id == id);
        }

        public List<string> GetArguments()
        {
            var lst = new List<string>();
            foreach (var arg in Arguments)
            {
                if (!string.IsNullOrWhiteSpace(arg.Value))
                {
                    if (arg.Type == "Boolean" && bool.TryParse(arg.Value, out var b))
                    {
                        if (b)
                            lst.Add(arg.ArgName);
                    }
                    else
                    {
                        lst.Add(arg.ArgName);
                        if (arg.ArgName != arg.Value)
                            lst.Add(arg.Value);
                    }
                }
            }
            return lst;
        }

        public class Argument
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public string ArgName { get; set; }
            public string Type { get; set; }
        }
    }
}
