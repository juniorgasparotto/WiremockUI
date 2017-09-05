using System;

namespace WiremockUI.Data
{
    public class Mock
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool ShowURL { get; set; } = true;
        public string Description { get; set; }

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
