using Publisher.Core;
using SysCommand.ConsoleApp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

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
            return OuputIsSuccess(Run("status"));
        }

        public string GetRootFolder()
        {
            var output = Run("rev-parse --show-toplevel");

            if (!OuputIsSuccess(output))
                return null;

            return GetOutputAsString(output).Trim();
        }

        public List<FileStatus> GetUncommitedFiles()
        {
            var list = new List<FileStatus>();
            var cmds = new List<string>();
            var rootFolder = GetRootFolder();

            cmds.Add("status -s");
            foreach (var c in cmds)
            {
                var output = Run(c);

                if (rootFolder == null || !OuputIsSuccess(output))
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
        
        public List<Output> Run(string args)
        {
            var ret = new List<Output>();
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = gitExe,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = false
                }
            };

            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
                ret.Add(new Output(proc.StandardOutput.ReadLine(), false));

            while (!proc.StandardError.EndOfStream)
                ret.Add(new Output(proc.StandardError.ReadLine(), true));

            return ret;
        }

        public void RunOutput(string args)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = gitExe,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    CreateNoWindow = false
                }
            };

            proc.Start();
        }

        #region Auxs

        public string GetOutputAsString(List<Output> output)
        {
            var strBuilder = new StringBuilder();
            foreach (var o in output)
                strBuilder.AppendLine(o.Line);
            return strBuilder.ToString();
        }

        public bool OuputIsSuccess(List<Output> output)
        {
            var line1 = output.FirstOrDefault();
            if (line1?.IsError == true)
                return false;

            return true;
        }

        public class Output
        {
            public string Line { get; set; }
            public bool IsError { get; set; }

            public Output(string line, bool isError)
            {
                this.Line = line;
                this.IsError = isError;
            }
        }

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
