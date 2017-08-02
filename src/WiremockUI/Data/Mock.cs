using System;

namespace WiremockUI.Data
{
    public class Mock
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }

        public string GetFormattedName()
        {
            return Name;
        }

        public string GetFolderName()
        {
            return Name.Trim().ToLower();
        }
    }
}
