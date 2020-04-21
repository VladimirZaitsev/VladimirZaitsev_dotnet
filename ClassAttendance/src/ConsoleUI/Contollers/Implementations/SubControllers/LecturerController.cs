using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using ConsoleUI.Contollers.Interfaces;
using ConsoleUI.ViewModels.Lecturer;
using ConsoleUI.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleUI.Contollers.Implementations.SubControllers
{
    public class LecturerController : ILecturerController
    {
        private readonly IService<Person> _lecturerService;
        private readonly IMenuView<Person, LecturerViewModel> _lecturerMenu;
        private readonly IMapper _mapper;

        public LecturerController(IService<Person> lecturerService, IMenuView<Person, LecturerViewModel> lecturerMenu, IMapper mapper)
        {
            _lecturerService = lecturerService;
            _lecturerMenu = lecturerMenu;
            _mapper = mapper;
        }

        private bool exitFlag;

        public async Task HandleInput()
        {
            while (!exitFlag)
            {
                Console.WriteLine();
                var input = Console.ReadKey().Key;
                PrintOperations();

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
            var student = _lecturerMenu.GetFromInput();

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
            var updatedUser = _lecturerMenu.Update(student);

            await _lecturerService.UpdateAsync(updatedUser);
        }

        private void PrintLecturers()
        {
            var students = _lecturerService.GetAll();
            var lecturerViewModels = _mapper.Map<IEnumerable<LecturerViewModel>>(students);

            _lecturerMenu.PrintAll(lecturerViewModels);
        }

        private async Task PrintLecturer()
        {
            var id = _lecturerMenu.GetIdFromInput();

            var lecturer = await _lecturerService.GetByIdAsync(id);
            var lecturerViewModel = _mapper.Map<LecturerViewModel>(lecturer);

            _lecturerMenu.Print(lecturerViewModel);
        }
    }
}
