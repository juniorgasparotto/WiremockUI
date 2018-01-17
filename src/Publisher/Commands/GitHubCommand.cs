using Octokit;
using Publisher.Core;
using SysCommand.ConsoleApp;
using SysCommand.ConsoleApp.Files;
using SysCommand.Mapping;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Publisher
{
    public class GitHubCommand : Command
    {
        private static string githubPassword;

        public string GitHubUserName
        {
            get
            {
                var appInfo = AppInfo.GetAppInfo();
                if (string.IsNullOrWhiteSpace(appInfo.GitHubUserName))
                {
                    appInfo.GitHubUserName = App.Console.Read("Github.UserName: ");
                    AppInfo.SaveAppInfo(appInfo);
                }
                return appInfo.GitHubUserName;
            }
            set
            {
                var appInfo = AppInfo.GetAppInfo();
                appInfo.GitHubUserName = value;
                AppInfo.SaveAppInfo(appInfo);
            }
        }

        public string GitHubRepositoryName
        {
            get
            {
                var appInfo = AppInfo.GetAppInfo();
                if (string.IsNullOrWhiteSpace(appInfo.GitHubRepositoryName))
                {
                    appInfo.GitHubRepositoryName = App.Console.Read("Github.RepositoryName: ");
                    AppInfo.SaveAppInfo(appInfo);
                }
                return appInfo.GitHubRepositoryName;
            }
            set
            {
                var appInfo = AppInfo.GetAppInfo();
                appInfo.GitHubRepositoryName = value;
                AppInfo.SaveAppInfo(appInfo);
            }
        }

        public string GitHubPassword
        {
            get
            {
                if (string.IsNullOrWhiteSpace(githubPassword))
                    githubPassword = Utils.SecureStringToString(Utils.ReadPassword(this, "Github.Password: "));
                return githubPassword;
            }
            set => githubPassword = value;
        }
        
        public GitHubCommand()
        {
            this.UsePrefixInAllMethods = true;
        }

        public void SetUrl(string url)
        {
            var info = AppInfo.GetAppInfo();
            info.GitHubUrl = url;
            AppInfo.SaveAppInfo(info);
        }

        public void SetTimeout(int hour = 2, int minutes = 0, int seconds = 0)
        {
            var info = AppInfo.GetAppInfo();
            info.GitHubTimeout = new TimeSpan(hour, minutes, seconds);
            AppInfo.SaveAppInfo(info);
        }

        
        public void TestConnection()
        {
            GitHub:

            try
            {
                var github = new GitHub(GitHubUserName, GitHubPassword, GitHubRepositoryName);
                var client = github.GetClient();
                github.GetRepository(client);
            }
            catch (Exception ex)
            {
                if (ex?.GetBaseException() is AuthorizationException baseEx)
                {
                    App.Console.Error(baseEx.Message);
                    GitHubUserName = null;
                    GitHubPassword = null;
                }
                else if (ex?.GetBaseException() is NotFoundException baseEx2)
                {
                    App.Console.Error(baseEx2.Message);
                    GitHubRepositoryName = null;
                }
                else
                {
                    App.Console.Error(ex.Message);
                    GitHubPassword = null;
                }

                goto GitHub;
            }
        }

        public string GetCommits(DateTime? since = null, DateTime? until = null, string author = null, string path = null, string sha = null, [Argument(ShortName = 'a', LongName = "all")]bool all = false)
        {
            var github = new GitHub(GitHubUserName, GitHubPassword, GitHubRepositoryName);
            var client = github.GetClient();
            var result = github.GetCommits(client, since, until, author, path, sha).ToArray();
            if (all)
            {
                return JsonFileManager.GetContentJsonFromObject(result);
            }
            else
            {
                var strBuilder = new StringBuilder();
                strBuilder.AppendLine($"Count: {result.Length}");
                foreach(var c in result)
                    strBuilder.AppendLine($"{c.Commit.Committer.Date.DateTime}: {c.Commit.Message}");
                return strBuilder.ToString();
            }
        }

        public string GetLastRelease([Argument(ShortName = 'a', LongName = "all")]bool all = false)
        {
            var github = new GitHub(GitHubUserName, GitHubPassword, GitHubRepositoryName);
            var client = github.GetClient();
            var last = github.GetLastRelease(client);

            if (last == null)
            {
                App.Console.Warning("No data found.");
                return null;
            }

            if (all)
                return JsonFileManager.GetContentJsonFromObject(last);
            else
                return last.Name;
        }

        public string GetRelease(int releaseId, [Argument(ShortName = 'a', LongName = "all")]bool all = false)
        {
            var github = new GitHub(GitHubUserName, GitHubPassword, GitHubRepositoryName);
            var client = github.GetClient();
            var release = github.GetRelease(client, releaseId);

            if (release == null)
            {
                App.Console.Warning("No data found.");
                return null;
            }

            if (all)
                return JsonFileManager.GetContentJsonFromObject(release);
            else
                return release.Name;
        }

        public string GetRelease(string releaseName, [Argument(ShortName = 'a', LongName = "all")]bool all = false)
        {
            var github = new GitHub(GitHubUserName, GitHubPassword, GitHubRepositoryName);
            var client = github.GetClient();
            var release = github.GetRelease(client, releaseName);

            if (release == null)
            {
                App.Console.Warning("No data found.");
                return null;
            }

            if (all)
                return JsonFileManager.GetContentJsonFromObject(release);
            else
                return release.Name;
        }

        public void GetAllReleases([Argument(ShortName = 'a', LongName = "all")]bool all = false)
        {
            var github = new GitHub(GitHubUserName, GitHubPassword, GitHubRepositoryName);
            var client = github.GetClient();
            var result = github.GetAllReleases(client);
            if (all)
                App.Console.Write(JsonFileManager.GetContentJsonFromObject(result));
            else 
                foreach(var r in result)
                    App.Console.Write(r.Name);
        }

        public void CreateRelease()
        {
            var pathCommand = App.Commands.Get<PathCommand>();
            var github = new GitHub(GitHubUserName, GitHubPassword, GitHubRepositoryName);
            var client = github.GetClient();

            var lastRelease = github.GetLastRelease(client);
            var createdDate = lastRelease?.CreatedAt;
            var commits = github.GetCommits(client, createdDate?.DateTime);

            var strBuilder = new StringBuilder();
            foreach (var c in commits)
                strBuilder.AppendLine($"{c.Commit.Committer.Date.DateTime}: {c.Commit.Message}");

            var body = strBuilder.ToString();

            if (Utils.Continue(this, "Open an external editor to change the release notes? (save and close editor to continue)?"))
            {
                File.WriteAllText(PathCommand.CommitFilePath, body);
                Process.Start("notepad", PathCommand.CommitFilePath).WaitForExit();
                body = File.ReadAllText(PathCommand.CommitFilePath);
                File.Delete(PathCommand.CommitFilePath);
            }

            var newVersion = Utils.GetVersionToString(Utils.GetCurrentVersion(pathCommand.TargetProjectAssemblyInfoPath));
            var result = github.CreateRelease(client, newVersion, body, false, false);
            App.Console.Write($"Created: {result.Name}");
        }

        public void CreateRelease(string releaseName, string body, bool draft = false, bool prerelease = false)
        {
            var github = new GitHub(GitHubUserName, GitHubPassword, GitHubRepositoryName);
            var client = github.GetClient();
            var result = github.CreateRelease(client, releaseName, body, draft, prerelease);
            App.Console.Write($"Created: {result.Name}");
        }

        public void DeleteRelease(int releaseId)
        {
            var github = new GitHub(GitHubUserName, GitHubPassword, GitHubRepositoryName);
            var client = github.GetClient();
            var result = github.DeleteRelease(client, releaseId);

            if (result)
            {
                App.Console.Write($"Deleted release: {releaseId}");
                App.Console.Warning("Delete the TAG manually.");
            }
            else
            {
                App.Console.Warning("No data found.");
            }
        }

        public void DeleteRelease(string releaseName)
        {
            var github = new GitHub(GitHubUserName, GitHubPassword, GitHubRepositoryName);
            var client = github.GetClient();
            var result = github.DeleteRelease(client, releaseName);

            if (result)
            {
                App.Console.Write($"Deleted release: {releaseName}");
                App.Console.Warning("Delete the TAG manually.");
            }
            else
            {
                App.Console.Warning("No data found.");
            }
        }

        public void UploadPackFolder()
        {
            var pathCommand = App.Commands.Get<PathCommand>();
            var newVersion = Utils.GetVersionToString(Utils.GetCurrentVersion(pathCommand.TargetProjectAssemblyInfoPath));
            UploadAll(newVersion, PathCommand.GitHubPackDirectory);
        }

        public void UploadAll(string releaseName, string folderPath)
        {
            string[] fileEntries = Directory.GetFiles(folderPath);
            foreach (string fileName in fileEntries)
                Upload(releaseName, fileName);
        }

        public void Upload(string releaseName, string path)
        {
            Console.WriteLine();
            var github = new GitHub(GitHubUserName, GitHubPassword, GitHubRepositoryName);
            var client = github.GetClient((e) =>
            {
                Console.Write($"\rUploading {Path.GetFileName(path)}: {e.ProgressPercentage}%");
                if (e.ProgressPercentage == 100)
                {
                    Console.WriteLine();
                    Console.WriteLine("Done.");
                }
            });

            var result = github.UploadAsset(client, releaseName, path);
        }

        public void DeleteAsset(string releaseName, string fileName)
        {
            var github = new GitHub(GitHubUserName, GitHubPassword, GitHubRepositoryName);
            var client = github.GetClient();
            github.DeleteAsset(client, releaseName, fileName);
            App.Console.Write($"Deleted asset: {releaseName} -> {fileName}");
        }

        public void GetAsset(int assetId)
        {
            var github = new GitHub(GitHubUserName, GitHubPassword, GitHubRepositoryName);
            var client = github.GetClient();
            var asset = github.GetAsset(client, assetId);
            App.Console.Write(JsonFileManager.GetContentJsonFromObject(asset));
        }
    }
}
