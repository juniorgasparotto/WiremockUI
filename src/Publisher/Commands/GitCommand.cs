using System;
using Publisher.Core;
using SysCommand.ConsoleApp;

namespace Publisher
{
    public class GitCommand : Command
    {
        public GitCommand()
        {
            UsePrefixInAllMethods = true;
        }

        public void CheckUncommitedFiles()
        {
            var pathCommand = App.Commands.Get<PathCommand>();
            var git = new Git(pathCommand.GitPath);

            check:
            var files = git.GetUncommitedFiles();

            if (files.Count > 0)
            {
                Status();

                if (Utils.Continue(this, "Are there files that need to be commited and published in GIT. Already been committed?"))
                {
                    goto check;
                }
            }
        }

        public bool Exists()
        {
            var pathCommand = App.Commands.Get<PathCommand>();
            var git = new Git(pathCommand.GitPath);
            return git.Exists();
        }

        public void Run(string args)
        {
            var pathCommand = App.Commands.Get<PathCommand>();
            Utils.ProcessExeOnly(pathCommand.GitPath, args);
        }

        public string RootFolder()
        {
            var pathCommand = App.Commands.Get<PathCommand>();
            var git = new Git(pathCommand.GitPath);
            return git.GetRootFolder();
        }

        public void Status()
        {
            var pathCommand = App.Commands.Get<PathCommand>();
            var git = new Git(pathCommand.GitPath);
            var files = git.GetUncommitedFiles();
            foreach (var f in files)
                App.Console.Write($"{f.GetTypeFormatted()} {f.FullPath}");
        }

    }
}
