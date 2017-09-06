using System;
using System.IO;
using WiremockUI.Data;

namespace WiremockUI
{
    public class TreeNodeMappingModel
    {
        public FileModel File { get; set; }
        public MappingModel Mapping { get; set; }
        public TreeNodeBodyModel TreeNodeBody { get; set; }
        public string Name { get; set; }
        public Proxy Proxy { get; set; }
        public Scenario Mock { get; set; }
    }
}
