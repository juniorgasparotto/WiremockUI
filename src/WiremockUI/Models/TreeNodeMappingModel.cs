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
        public Server Server { get; set; }
        public Scenario Scenario { get; set; }
    }
}
