using Publisher.Core;
using SysCommand.ConsoleApp;
using SysCommand.Mapping;
using System;
using System.Diagnostics;
using System.IO;

namespace Publisher
{
    public class BuildCommand : Command
    {
        public BuildCommand()
        {
            this.UsePrefixInAllMethods = true;
        }

        public void Clear()
        {
            Utils.ClearFolder(PathCommand.OutputDirectory);
        }

        [Action(UsePrefix = false)]
        public void Build()
        {
            var configs = AppInfo.GetAppInfo().Configurations;
            if (configs.Count == 0)
                throw new Exception("No configuration found. Add a config using command: config-set --name Release --pack-name MyProject.zip");

            foreach (var c in configs)
                Build(c.Name);
        }

        [Action(UsePrefix = false)]
        public void Build(string config)
        {
            try
            {
                App.Console.Write($"Start build '{config}': {DateTime.Now}");
                Console.WriteLine();

                var output = Path.Combine(PathCommand.BuildDirectory, config);
                var pathCommand = App.Commands.Get<PathCommand>();
                var startInfo = new ProcessStartInfo
                {
                    CreateNoWindow = false,
                    UseShellExecute = false,
                    FileName = pathCommand.MsBuildPath,
                    WindowStyle = ProcessWindowStyle.Normal,
                    Arguments = string.Format("{0} /t:Build /m /property:Configuration={1} /p:OutDir={2}", Utils.GetFullPath(this, pathCommand.TargetProjectPath), config, output)
                };

                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                App.Console.Error(ex.Message);
            }
            finally
            {
                App.Console.Write($"End: {DateTime.Now}");
            }
        }
    }
}
