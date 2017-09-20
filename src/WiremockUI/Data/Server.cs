using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WiremockUI.Data
{
    public class Server
    {
        public Guid Id { get; set; }
        public string UrlTarget { get; set; }
        public int Port { get; set; }
        public string Name { get; set; }
        private List<Scenario> scenario = new List<Scenario>();

        public IEnumerable<Scenario> Scenarios => scenario;
        public IEnumerable<Argument> Arguments { get; set; }

        public string GetFormattedName()
        {
            if (!string.IsNullOrWhiteSpace(UrlTarget))
                return $"{Name} (http://localhost:{Port} <- {UrlTarget})";

            return $"{Name} (http://localhost:{Port})";
        }

        public string GetFolderName()
        {
            return Port.ToString();
        }

        public string GetServerUrl()
        {
            return $"http://localhost:{Port}";
        }

        public string GetFullPath()
        {
            return Path.Combine(Helper.GetBasePath(), GetFolderName());
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

        public string[] GetArguments(Scenario scenario, PlayType type, bool addQuoteInStrings = false)
        {
            string[] args;
            var relativeFolder = GetFullPath(scenario);

            if (type == PlayType.PlayAndRecord)
            {
                args = new string[]
                {
                    "--port", Port.ToString(),
                    "--proxy-all", Helper.AddQuote(UrlTarget, addQuoteInStrings),
                    "--record-mappings",
                    "--root-dir", Helper.AddQuote(relativeFolder, addQuoteInStrings)
                };
            }
            else if (type == PlayType.PlayAsProxy)
            {
                args = new string[]
                {
                    "--port", Port.ToString(),
                    "--proxy-all", Helper.AddQuote(UrlTarget, addQuoteInStrings),
                };
            }
            else
            {
                args = new string[]
                {
                    "--port", Port.ToString(),
                    "--root-dir",  Helper.AddQuote(relativeFolder, addQuoteInStrings)
                };
            }

            var argsAux = GetArguments();
            argsAux.InsertRange(0, args);
            return argsAux.ToArray();
        }

        public List<string> GetArguments(bool addQuoteInStrings = false)
        {
            var lst = new List<string>();
            foreach (var arg in Arguments)
            {
                if (!string.IsNullOrWhiteSpace(arg.Value))
                {
                    var type = arg.Type.ToLower();
                    if (type == "boolean" && bool.TryParse(arg.Value, out var b))
                    {
                        if (b)
                            lst.Add(arg.ArgName);
                    }
                    else
                    {
                        lst.Add(arg.ArgName);
                        if (arg.ArgName != arg.Value)
                        {
                            var value = arg.Value;
                            if (type == "string")
                                value = Helper.AddQuote(value, addQuoteInStrings);
                            lst.Add(value);
                        }
                    }
                }
            }
            return lst;
        }

        public Server Copy()
        {
            return new Server()
            {
                Id = Guid.NewGuid(),
                Arguments = Arguments,
                Name = Name,
                Port = GetAutoPort(),
                UrlTarget = UrlTarget
            };
        }

        public static int GetAutoPort()
        {
            var db = new UnitOfWork();
            var maxPort = db.Servers.AsQueryable().Select(f => f.Port).DefaultIfEmpty().Max();
            if (maxPort == 0)
                return 5500;
            return maxPort + 1;
        }

        public enum PlayType
        {
            Play,
            PlayAsProxy,
            PlayAndRecord
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
