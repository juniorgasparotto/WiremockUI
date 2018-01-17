using Octokit;
using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Handlers;

namespace Publisher.Core
{
    public class GitHub
    {
        private string userName;
        private string password;
        private string repositoryName;

        public string Url => AppInfo.GetAppInfo().GitHubUrl ?? "https://api.github.com";
        public TimeSpan Timeout => AppInfo.GetAppInfo().GitHubTimeout ?? TimeSpan.FromHours(2);

        public GitHub(string userName, string password, string repositoryName)
        {
            this.userName = userName;
            this.password = password;
            this.repositoryName = repositoryName;
        }

        public GitHubClient GetClient(Action<HttpProgressEventArgs> action = null)
        {
            var progress = new ProgressMessageHandler();

            if (action != null)
            {
                progress.HttpSendProgress += (object sender, HttpProgressEventArgs e) => action(e);
            }

            progress.InnerHandler = HttpMessageHandlerFactory.CreateDefault();

            var connection = new Connection
            (
                productInformation: new ProductHeaderValue(repositoryName),
                baseAddress: new Uri(Url),
                credentialStore: new InMemoryCredentialStore(new Credentials(userName, password)),
                httpClient: new HttpClientAdapter(() => progress),
                serializer: new SimpleJsonSerializer()
            );

            var client = new GitHubClient(connection);
            client.Connection.SetRequestTimeout(Timeout);
            return client;
        }

        public Release CreateRelease(GitHubClient client, string releaseName, string body, bool draft = false, bool prerelease = false)
        {
            var newRelease = new NewRelease(releaseName);
            newRelease.Body = body;
            newRelease.Name = releaseName;
            newRelease.Draft = draft;
            newRelease.Prerelease = prerelease;

            return client.Repository.Release.Create(userName, repositoryName, newRelease).Result;
        }

        public bool DeleteRelease(GitHubClient client, int id)
        {
            var release = GetRelease(client, id);
            if (release == null)
                return false;

            client.Repository.Release.Delete(userName, repositoryName, id);
            return true;
        }

        public bool DeleteRelease(GitHubClient client, string name)
        {
            var release = GetRelease(client, name);
            if (release == null)
                return false;

            client.Repository.Release.Delete(userName, repositoryName, release.Id);
            return true;
        }

        public ReleaseAsset UploadAsset(GitHubClient client, string releaseName, string path)
        {
            var release = GetRelease(client, releaseName);
            var fileName = Path.GetFileName(path);
            var contentType = MimeType.GetMimeType(path);
            return UploadAsset(client, release, fileName, path, contentType);
        }

        public void DeleteAsset(GitHubClient client, string releaseName, string fileName)
        {
            var release = GetRelease(client, releaseName);
            foreach (var a in release.Assets)
            {
                if (a.Name == fileName)
                    client.Repository.Release.DeleteAsset(userName, repositoryName, a.Id);
            }
        }

        private ReleaseAsset UploadAsset(GitHubClient client, Release release, string name, string path, string contentType)
        {
            using (var stream = new FileStream(path, System.IO.FileMode.Open))
            {
                var asset = new ReleaseAssetUpload(name, contentType, stream, Timeout);
                return client.Repository.Release.UploadAsset(release, asset).Result;
            }
        }

        public IEnumerable<GitHubCommit> GetCommits(GitHubClient client, DateTime? since = null, DateTime? until = null, string author = null, string path = null, string sha = null)
        {
            return client.Repository.Commit.GetAll(userName, repositoryName, new CommitRequest()
            {
                Until = until,
                Author = author,
                Sha = sha,
                Since = since,
                Path = path
            }).Result;
        }

        public Repository GetRepository(GitHubClient client)
        {
            return client.Repository.Get(userName, repositoryName).Result;
        }

        public Release GetLastRelease(GitHubClient client)
        {
            try
            {
                return client.Repository.Release.GetLatest(userName, repositoryName).Result;
            }
            catch (Exception ex) when (ex?.GetBaseException() is NotFoundException baseEx2)
            {
                return null;
            }
        }

        public Release GetRelease(GitHubClient client, int id)
        {
            return client.Repository.Release.Get(userName, repositoryName, id).Result;
        }

        public Release GetRelease(GitHubClient client, string name)
        {
            var all = client.Repository.Release.GetAll(userName, repositoryName).Result;
            foreach (var r in all)
                if (r.Name == name)
                    return r;
            return null;
        }

        public IEnumerable<Release> GetAllReleases(GitHubClient client)
        {
            return client.Repository.Release.GetAll(userName, repositoryName).Result;
        }

        public ReleaseAsset GetAsset(GitHubClient client, int assetId)
        {
            return client.Repository.Release.GetAsset(userName, repositoryName, assetId).Result;
        }
    }
}
