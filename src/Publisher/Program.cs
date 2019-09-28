using Publisher.Core;
using SysCommand.Compatibility;
using SysCommand.ConsoleApp;
using SysCommand.ConsoleApp.Commands;
using SysCommand.ConsoleApp.Loader;
using System.Linq;

namespace Publisher
{
    class Program
    {
        public static int Main(string[] args)
        {
            var commands = (from type in typeof(Program).Assembly.GetTypes()
                            where 
                               typeof(Command).IsAssignableFrom(type) 
                            && type.IsInterface() == false 
                            && type.IsAbstract() == false
                            select type).ToList();

            return App.RunApplication(() =>
            {
                var loader = new AppDomainCommandLoader(commands);
                loader.IgnoreCommand<ArgsHistoryCommand>();

                // Está com problema
                //var app = new App(loader.GetFromAppDomain());

                var app = new App(commands);
                app.Console.ColorRead = System.ConsoleColor.Blue;
                app.Console.Verbose = Verbose.All;
                app.OnException += (result, ex) => app.Console.Error(Utils.GetExceptionDetails(ex));
                return app;
            });
        }
    }
}
