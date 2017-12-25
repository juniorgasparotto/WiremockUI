using Publisher.Core;
using SysCommand.ConsoleApp;
using SysCommand.ConsoleApp.Loader;

namespace Publisher
{
    class Program
    {
        public static int Main(string[] args)
        {
            return App.RunApplication(() =>
            {
                var loader = new AppDomainCommandLoader();
                //loader.IgnoreCommand<ArgsHistoryCommand>();
                var app = new App(loader.GetFromAppDomain());
                app.Console.ColorRead = System.ConsoleColor.Blue;
                app.Console.Verbose = Verbose.All;
                app.OnException += (result, ex) => app.Console.Error(Utils.GetExceptionDetails(ex));
                return app;
            });
        }
    }
}
