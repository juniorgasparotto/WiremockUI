using SysCommand.ConsoleApp;
using System.Linq;

namespace WiremockUI.Publish
{
    public class PublishCommand : Command
    {
        public void Publish()
        {
            var build = App.Commands.Get<BuildCommand>();
            var pack = App.Commands.Get<PackWiremockUICommand>();
            var version = App.Commands.Get<VersionCommand>();
            var github = App.Commands.Get<GitHubCommand>();
            var pathCommand = App.Commands.Get<PathCommand>();

            Title("BUILD");
            build.Clear();
            build.Build();

            Title("PACK");
            pack.Pack();

            Title("SET NEXT VERSION");
            version.Next();

            if (Utils.Continue(this, "Do you create a new release in github?"))
            {
                Title("GITHUB - CREATE NEW RELEASE");

                // Check error in connection
                github.TestConnection();

                github.CreateRelease();
                                
                if (Utils.Continue(this, "Do you want to upload the package in the new github release?"))
                {
                    Title("GITHUB - UPLOADING PACKS");
                    github.UploadPackFolder();
                }
            }
        }

        private void Title(string text)
        {
            var count = 80;
            App.Console.Write(new string('-', count));
            App.Console.Write(text);
            App.Console.Write(new string('-', count));
        }
    }
}
