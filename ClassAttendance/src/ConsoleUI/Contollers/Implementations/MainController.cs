using BLL.Models;
using ConsoleUI.Contollers.Interfaces;
using ConsoleUI.Views.Interfaces;
using System;
using System.Threading.Tasks;

namespace ConsoleUI.Contollers.Implementations
{
    public class MainController : IMainController
    {
        private readonly IPrintMenuView _mainMenu;

        private readonly IStudentController _studentController;
        private readonly ILecturerController _lecturerController;
        private readonly ISubController<MissedClass> _lecturesController;
        private readonly ISubController<Subject> _subjectController;
        private readonly ISubController<Group> _groupController;

        private bool exitFlag;

        public MainController(IPrintMenuView mainMenu,
            IStudentController studentController,
            ILecturerController lecturerController,
            ISubController<MissedClass> lecturesController,
            ISubController<Subject> subjectController,
            ISubController<Group> groupController)
        {
            _mainMenu = mainMenu;
            _studentController = studentController;
            _lecturerController = lecturerController;
            _lecturesController = lecturesController;
            _subjectController = subjectController;
            _groupController = groupController;
        }

        public async Task InitializeInteractiveLoop()
        {
            while(!exitFlag)
            { 
                _mainMenu.PrintMenu();
                var input = Console.ReadKey().Key;

                switch (input)
                {
                    case ConsoleKey.NumPad1:
                        await _studentController.HandleInput();
                        break;
                    case ConsoleKey.NumPad2:
                        await _lecturerController.HandleInput();
                        break;
                    case ConsoleKey.NumPad3:
                        await _lecturesController.HandleInput();
                        break;
                    case ConsoleKey.NumPad4:
                        await _groupController.HandleInput();
                        break;
                    case ConsoleKey.NumPad5:
                        await _subjectController.HandleInput();
                        break;
                    case ConsoleKey.NumPad0:
                        exitFlag = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
