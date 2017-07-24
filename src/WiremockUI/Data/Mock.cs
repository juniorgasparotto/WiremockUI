using System;

namespace WiremockUI.Data
{
    public class Mock
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }

        internal string GetFormattedName()
        {
            return Name;
        }

        internal string GetFolderName()
        {
            return Name.Trim().ToLower();
        }

        //internal string GetUrlMockProxy()
        //{
        //    return $"http://localhost:{PortProxy}";
        //}

        //internal string GetFullPath()
        //{
        //    var path = Path.Combine(Directory.GetCurrentDirectory(), GetFolderName());
        //    return Path.Combine(Directory.GetCurrentDirectory(), GetFolderName());
        //}

        //internal string GetMappingPath()
        //{
        //    return Path.Combine(GetFullPath(), "mappings");
        //}

        //internal bool AlreadyRecord()
        //{
        //    if (Directory.Exists(GetFullPath()))
        //        return true;
        //    return false;
        //}
    }
}
