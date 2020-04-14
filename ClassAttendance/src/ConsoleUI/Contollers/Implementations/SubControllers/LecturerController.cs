using BLL.Interfaces;
using ConsoleUI.Contollers.Interfaces;
using ConsoleUI.Models.Lecturer;
using ConsoleUI.Views.Implementations.SubMenus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleUI.Contollers.Implementations.SubControllers
{
    public class LecturerController : ISubController
    {
        private readonly ILecturerService _lecturerService;
        private readonly LecturerMenuView _lecturerMenu;

        public LecturerController(ILecturerService lecturerService, LecturerMenuView lecturerMenu)
        {
            _lecturerService = lecturerService;
            _lecturerMenu = lecturerMenu;
        }

        private bool exitFlag;

        public async Task HandleInput()
        {
            while (!exitFlag)
            {
                var input = Console.ReadKey().Key;

                switch (input)
                {
                    case ConsoleKey.D1:
                        PrintLecturers();
                        break;
                    case ConsoleKey.D2:
                        await PrintLecturer();
                        break;
                    case ConsoleKey.D3:
                        await AddLecturer();
                        break;
                    case ConsoleKey.D4:
                        await DeleteLecturer();
                        break;
                    case ConsoleKey.D5:
                        await UpdateLecturer();
                        break;
                    case ConsoleKey.D0:
                        exitFlag = true;
                        break;
                    default:
                        break;
                }
            }
        }

        public void PrintOperations()
        {
            _lecturerMenu.PrintMenu();
        }

        private async Task AddLecturer()
        {
            var student = _lecturerMenu.GetLectureFromInput();

            await _lecturerService.AddAsync(student);
        }

        private async Task DeleteLecturer()
        {
            var id = _lecturerMenu.GetIdFromInput();

            await _lecturerService.DeleteAsync(id);
        }

        private async Task UpdateLecturer()
        {
            var id = _lecturerMenu.GetIdFromInput();
            var student = await _lecturerService.GetByIdAsync(id);
            var updatedUser = _lecturerMenu.UpdateStudent(student);

            await _lecturerService.UpdateAsync(updatedUser);
        }

        private void PrintLecturers()
        {
            var students = _lecturerService.GetAll();
            var viewModels = new List<LecturerViewModel>();

            _lecturerMenu.PrintLectures(viewModels);
        }

        private async Task PrintLecturer()
        {
            var input = Console.ReadLine();
            var id = int.Parse(input);

            var student = await _lecturerService.GetByIdAsync(id);

            _lecturerMenu.PrintLecturer(student);
        }
    }
}
