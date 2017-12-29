using Publisher.Core;
using SysCommand.ConsoleApp;
using SysCommand.Mapping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Publisher
{
    public class FileReplaceCommand : Command
    {
        public FileReplaceCommand()
        {
            this.UsePrefixInAllMethods = true;
        }

        public void Version()
        {
            var pathCommand = App.Commands.Get<PathCommand>();
            var nextVersion = Utils.GetVersionToString(Utils.GetCurrentVersion(pathCommand.TargetProjectAssemblyInfoPath));
            ReplaceAll("version", new KeyValuePair<string, string>("{version}", nextVersion));
        }

        public void Add(string groupName, string path, string pattern, string replacement)
        {
            var appInfo = AppInfo.GetAppInfo();
            appInfo.FilesReplace.Add(new AppInfo.FileReplace
            {
                GroupName = groupName,
                Path = path,
                Pattern = pattern,
                Replacement = replacement
            });
            AppInfo.SaveAppInfo(appInfo);
        }

        public void Remove(int index)
        {
            var appInfo = AppInfo.GetAppInfo();
            if (appInfo.FilesReplace.ElementAtOrDefault(index) == null)
                return;

            appInfo.FilesReplace.RemoveAt(index);
            AppInfo.SaveAppInfo(appInfo);
        }

        public void Show()
        {
            var appInfo = AppInfo.GetAppInfo();
            var i = 0;
            foreach (var f in appInfo.FilesReplace)
            {
                if (i > 0)
                    App.Console.Write("------------------");

                App.Console.Write("Id: " + i);
                App.Console.Write("GroupName: " + f.GroupName);
                App.Console.Write("Path: " + Utils.GetFullPath(this, f.Path));
                App.Console.Write("Pattern: " + f.Pattern);
                App.Console.Write("Replacement: " + f.Replacement);
                i++;
            }
        }

        public void FindAll()
        {
            var appInfo = AppInfo.GetAppInfo();
            foreach (var f in appInfo.FilesReplace)
            {
                var path = Utils.GetFullPath(this, f.Path);
                var content = File.ReadAllText(path);
                var matches = Regex.Matches(content, f.Pattern);
                foreach (Match m in matches)
                    App.Console.Write($"{f.Path} => {m.Value}");
            }
        }

        public void Find(string path, string pattern)
        {
            path = Utils.GetFullPath(this, path);
            var content = File.ReadAllText(path);
            var matches = Regex.Matches(content, pattern);
            foreach(Match m in matches)
                App.Console.Write(m.Value);
        }

        public void ReplaceAll(string groupName = null)
        {
            ReplaceAll(groupName, (Dictionary<string, string>)null);
        }

        [Action(Ignore = true)]
        public void ReplaceAll(string groupName, params KeyValuePair<string, string>[] replacements)
        {
            ReplaceAll(groupName, replacements.ToDictionary(f => f.Key, f => f.Value));
        }

        [Action(Ignore=true)]
        public void ReplaceAll(string groupName, Dictionary<string, string> variables)
        {
            var appInfo = AppInfo.GetAppInfo();
            var i = 0;

            var files = appInfo.FilesReplace.Where(f => f.GroupName.ToLower() == groupName.ToLower());
            foreach (var f in files)
            {
                var replacement = f.Replacement;
                if (variables !=null)
                {
                    foreach (var v in variables)
                        replacement = replacement.Replace(v.Key, v.Value);
                }

                var path = Utils.GetFullPath(this, f.Path);
                var content = File.ReadAllText(path);
                content = Regex.Replace(content, f.Pattern, m =>
                {
                    if (i > 0)
                        App.Console.Write("------------------");

                    var value = Regex.Replace(m.Value, f.Pattern, replacement);

                    App.Console.Write($"Path: {path}");
                    App.Console.Write($"Pattern: {f.Pattern}");
                    App.Console.Write($"Found: {m.Value}");
                    App.Console.Write($"Replacement: {value}");

                    i++;
                    return value;
                });
                File.WriteAllText(path, content);
            }
        }

        public void Replace(string path, string pattern, string replacement)
        {
            path = Utils.GetFullPath(this, path);
            var content = File.ReadAllText(path);
            content = Regex.Replace(content, pattern, replacement);
            File.WriteAllText(path, content);
        }
    }
}
