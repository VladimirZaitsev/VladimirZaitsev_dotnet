using ConsoleUI.Contollers.Interfaces;
using ConsoleUI.Views.Interfaces;
using System;
using System.Threading.Tasks;

namespace ConsoleUI.Contollers.Implementations
{
    public class MainController : IMainController
    {
        private readonly IMainMenuView _mainMenu;

        private readonly IMenuView _studentMenu;
        private readonly ISubController _studentController;

        private readonly IMenuView _lecturerMenu;
        private readonly ISubController _lecturerController;

        private readonly IMenuView _lecturesMenu;
        private readonly ISubController _lecturesController;

        private bool exitFlag;


        public async Task InitializeInteractiveLoop()
        {
            while(!exitFlag)
            { 
                _mainMenu.PrintMainMenu();
                var input = Console.ReadKey().Key;

                switch (input)
                {
                    case ConsoleKey.D1:
                        _studentMenu.PrintMenu();
                        await _studentController.HandleInput();
                        break;
                    case ConsoleKey.D2:
                        _lecturerMenu.PrintMenu();
                        await _lecturerController.HandleInput();
                        break;
                    case ConsoleKey.D3:
                        _lecturesMenu.PrintMenu();
                        await _lecturesController.HandleInput();
                        break;
                    case ConsoleKey.D0:
                        exitFlag = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
