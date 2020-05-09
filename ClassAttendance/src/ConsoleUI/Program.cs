using ConsoleUI.Contollers.Interfaces;
using ConsoleUI.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false);
            var config = configBuilder.Build();

            Startup.ConfigureServices(serviceCollection, config);
            var services = serviceCollection.BuildServiceProvider();

            var mainMenu = services.GetService<IMainController>();

            await mainMenu.InitializeInteractiveLoop();
        }
    }
}
