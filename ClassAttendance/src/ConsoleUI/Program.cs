using ConsoleUI.Contollers.Implementations;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            var mainMenu = new MainController();
            await mainMenu.MainLoop();
        }
    }
}
