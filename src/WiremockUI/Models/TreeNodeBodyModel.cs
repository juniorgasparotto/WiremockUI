using System;
using System.IO;

namespace WiremockUI
{
    public class TreeNodeBodyModel
    {
        public FileModel File { get; set; }
        public TreeNodeMappingModel TreeNodeMapping { get; set; }
        public string Name { get; internal set; }
    }
}
