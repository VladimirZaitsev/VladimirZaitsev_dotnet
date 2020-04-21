using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using ConsoleUI.Contollers.Interfaces;
using ConsoleUI.ViewModels.MissedLectures;
using ConsoleUI.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleUI.Contollers.Implementations.SubControllers
{
    public class LecturesController : ISubController<MissedClass>
    {
        private readonly ILecturesMenuView<MissedClass, MissedClassViewModel> _lecturesMenu;
        private readonly IMissedClassService _lecturesService;
        private readonly IMapper _mapper;

        private bool exitFlag;

        public LecturesController(ILecturesMenuView<MissedClass, MissedClassViewModel> lecturesMenu, IMissedClassService lecturesService, IMapper mapper)
        {
            _lecturesMenu = lecturesMenu;
            _lecturesService = lecturesService;
            _mapper = mapper;
        }

        public void PrintOperations()
        {
            _lecturesMenu.PrintMenu();
        }

        public async Task HandleInput()
        {
            while (!exitFlag)
            {
                Console.WriteLine();
                var input = Console.ReadKey().Key;
                PrintOperations();

                switch (input)
                {
                    case ConsoleKey.NumPad1:
                        PrintLecturesAsync();
                        break;
                    case ConsoleKey.NumPad2:
                        await PrintLectureAsync();
                        break;
                    case ConsoleKey.NumPad3:
                        await AddLecture();
                        break;
                    case ConsoleKey.NumPad4:
                        await DeleteLecture();
                        break;
                    case ConsoleKey.NumPad5:
                        await UpdateLecture();
                        break;
                    case ConsoleKey.NumPad0:
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
            var viewModels = _mapper.Map<IEnumerable<MissedClassViewModel>>(lectures);

            _lecturesMenu.PrintAll(viewModels);
        }

        private async Task AddLecture()
        {
            var lecture = _lecturesMenu.GetFromInput();
            await _lecturesService.AddAsync(lecture);
        }

        private async Task PrintLectureAsync()
        {
            var id = _lecturesMenu.GetIdFromInput();
            var lecture = await _lecturesService.GetByIdAsync(id);
            var viewModel = _mapper.Map<MissedClassViewModel>(lecture);

            _lecturesMenu.Print(viewModel);
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
