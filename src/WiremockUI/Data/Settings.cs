using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WiremockUI.Data
{
    public class Settings
    {
        public Guid Id { get; set; }
        public Dictionary<string, string> Languages { get; set; } = new Dictionary<string, string>();
        public string DefaultLanguage { get; set; }
    }
}
