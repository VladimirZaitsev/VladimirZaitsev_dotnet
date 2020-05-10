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
        private readonly IService<Lecturer> _lecturerService;
        private readonly IMenuView<Lecturer, LecturerViewModel> _lecturerMenu;
        private readonly IMapper _mapper;

        public LecturerController(IService<Lecturer> lecturerService, IMenuView<Lecturer, LecturerViewModel> lecturerMenu, IMapper mapper)
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
                PrintOperations();
                var input = Console.ReadKey().Key;

                switch (input)
                {
                    case ConsoleKey.NumPad1:
                        PrintLecturers();
                        break;
                    case ConsoleKey.NumPad2:
                        await PrintLecturer();
                        break;
                    case ConsoleKey.NumPad3:
                        await AddLecturer();
                        break;
                    case ConsoleKey.NumPad4:
                        await DeleteLecturer();
                        break;
                    case ConsoleKey.NumPad5:
                        await UpdateLecturer();
                        break;
                    case ConsoleKey.NumPad0:
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
            var lecturer = _lecturerMenu.GetFromInput();

            await _lecturerService.AddAsync(lecturer);
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
