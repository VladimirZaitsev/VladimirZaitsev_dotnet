using BLL.Interfaces;
using ConsoleUI.Contollers.Interfaces;
using ConsoleUI.Views.Implementations.SubMenus;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleUI.Contollers.Implementations.SubControllers
{
    public class LecturesController : ISubController
    {
        private readonly LecturesMenuView _lecturesMenu;
        private readonly IMissedClassService _lecturesService;

        private bool exitFlag;

        public LecturesController(LecturesMenuView lecturesMenu, IMissedClassService lecturesService)
        {
            _lecturesMenu = lecturesMenu;
            _lecturesService = lecturesService;
        }

        public void PrintOperations()
        {
            _lecturesMenu.PrintMenu();
        }

        public async Task HandleInput()
        {
            while (!exitFlag)
            {
                var input = Console.ReadKey().Key;

                switch (input)
                {
                    case ConsoleKey.D1:
                        PrintLecturesAsync();
                        break;
                    case ConsoleKey.D2:
                        await PrintLectureAsync();
                        break;
                    case ConsoleKey.D3:
                        await AddLecture();
                        break;
                    case ConsoleKey.D4:
                        await DeleteLecture();
                        break;
                    case ConsoleKey.D5:
                        await UpdateLecture();
                        break;
                    case ConsoleKey.D0:
                        exitFlag = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void PrintLecturesAsync()
        {
            var lectures = _lecturesService.GetAll();

            _lecturesMenu.PrintLectures(lectures);
        }

        private async Task AddLecture()
        {
            var lecture = _lecturesMenu.GetLectureFromInput();
            await _lecturesService.AddAsync(lecture);
        }

        private async Task PrintLectureAsync()
        {
            var id = _lecturesMenu.GetIdFromInput();
            var lecture = await _lecturesService.GetByIdAsync(id);

            _lecturesMenu.PrintLecture(lecture);
        }

        private async Task DeleteLecture()
        {
            var id = _lecturesMenu.GetIdFromInput();

            await _lecturesService.DeleteAsync(id);
        }

        private async Task UpdateLecture()
        {
            var id = _lecturesMenu.GetIdFromInput();
            var lecture = await _lecturesService.GetByIdAsync(id);

            await _lecturesService.UpdateAsync(lecture);
        }
    }
}
