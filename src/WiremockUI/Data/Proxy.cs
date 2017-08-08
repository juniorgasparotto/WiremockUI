using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WiremockUI.Data
{
    public class Proxy
    {
        public Guid Id { get; set; }
        public string UrlOriginal { get; set; }
        public int PortProxy { get; set; }
        public string Name { get; set; }
        private List<Mock> mocks = new List<Mock>();

        public IEnumerable<Mock> Mocks => mocks;

        internal string GetFormattedName()
        {
            return $"{Name} (http://localhost:{PortProxy} <- {UrlOriginal})";
        }

        internal string GetFolderName()
        {
            return PortProxy.ToString();
        }

        internal string GetUrlProxy()
        {
            return $"http://localhost:{PortProxy}";
        }

        internal string GetFullPath()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), GetFolderName());
            return Path.Combine(Directory.GetCurrentDirectory(), GetFolderName());
        }

        internal string GetFullPath(Mock mock)
        {
            return Path.Combine(GetFullPath(), mock.GetFolderName());
        }

        internal string GetMappingPath(Mock mock)
        {
            return Path.Combine(GetFullPath(mock), "mappings");
        }

        internal string GetBodyFilesPath(Mock mock)
        {
            return Path.Combine(GetFullPath(mock), "__files");
        }

        internal bool AlreadyRecord(Mock mock)
        {
            if (Directory.Exists(GetMappingPath(mock)))
                return true;
            return false;
        }

        internal Mock GetDefaultMock()
        {
            var d = mocks.FirstOrDefault(f => f.IsDefault);
            //if (d == null)
            //{
            //    FixDefault();
            //    d = mocks.FirstOrDefault(f => f.IsDefault);
            //}

            return d;
        }

        internal void SetDefault(Mock mock)
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

        internal void RemoveMock(Guid id)
        {
            mocks.RemoveAll(f => f.Id == id);
            //FixDefault();
        }

        //private void FixDefault()
        //{
        //    var existsDefault = mocks.Any(f => f.IsDefault);
        //    if (!existsDefault)
        //    {
        //        var first = mocks.FirstOrDefault();
        //        if (first != null)
        //            first.IsDefault = true;
        //    }
        //}
    }
}
