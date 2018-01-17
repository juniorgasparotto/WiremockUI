using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Publisher.Core
{
    public class Git
    {
        private string gitExe;

        public Git(string gitExe)
        {
            this.gitExe = gitExe;
        }

        public bool Exists()
        {
            return ExeOutput.IsSuccess(Utils.ProcessExeAndGetOutput(gitExe, "status"));
        }

        public string GetRootFolder()
        {
            var output = Utils.ProcessExeAndGetOutput(gitExe, "rev-parse --show-toplevel");

            if (!ExeOutput.IsSuccess(output))
                return null;

            return ExeOutput.AsString(output).Trim();
        }

        public List<FileStatus> GetUncommitedFiles()
        {
            var list = new List<FileStatus>();
            var cmds = new List<string>();
            var rootFolder = GetRootFolder();

            cmds.Add("status -s");
            foreach (var c in cmds)
            {
                var output = Utils.ProcessExeAndGetOutput(gitExe, c);

                if (rootFolder == null || !ExeOutput.IsSuccess(output))
                    return list;

                foreach (var o in output)
                {
                    var lines = o.Line.Trim().Split(' ').Where(f=> f.Trim() != "");
                    
                    var s = new FileStatus
                    {
                        Type = lines.ElementAtOrDefault(0),
                        RelativePath = lines.ElementAtOrDefault(1),
                    };

                    s.FullPath = Path.GetFullPath(s.RelativePath);
                    list.Add(s);
                }
            }
            
            return list;
        }
        
        #region Auxs
        
        public class FileStatus
        {
            public string RelativePath { get; set; }
            public string FullPath { get; set; }
            public string Type { get; set; }

            public string GetTypeFormatted()
            {
                if (Type.Length == 1)
                    return " " + Type;
                return Type;
            }
        }

        #endregion
    }
}
