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
        private List<Mock> mocks = new List<Mock>();

        public IEnumerable<Mock> Mocks => mocks;
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

        public string GetFullPath(Mock mock)
        {
            return Path.Combine(GetFullPath(), mock.GetFolderName());
        }

        public string GetMappingPath(Mock mock)
        {
            return Path.Combine(GetFullPath(mock), "mappings");
        }

        public string GetBodyFilesPath(Mock mock)
        {
            return Path.Combine(GetFullPath(mock), "__files");
        }

        public bool AlreadyRecord(Mock mock)
        {
            if (Directory.Exists(GetMappingPath(mock)))
                return true;
            return false;
        }

        public Mock GetDefaultMock()
        {
            var d = mocks.FirstOrDefault(f => f.IsDefault);
            return d;
        }

        public void SetDefault(Mock mock)
        {
            mock.IsDefault = true;
            foreach (var m in mocks)
                if (m.Id != mock.Id)
                    m.IsDefault = false;
        }

        public Mock GetMockById(Guid? id)
        {
            return mocks.FirstOrDefault(f => f.Id == id);
        }

        public void AddMock(Mock mock)
        {
            if (mock.Id == Guid.Empty)
                mock.Id = Guid.NewGuid();

            if (mocks.Count == 0)
                mock.IsDefault = true;
            mocks.Add(mock);
        }

        public void RemoveMock(Guid id)
        {
            mocks.RemoveAll(f => f.Id == id);
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
