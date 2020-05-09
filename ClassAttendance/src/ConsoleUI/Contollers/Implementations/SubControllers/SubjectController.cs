using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using ConsoleUI.Contollers.Interfaces;
using ConsoleUI.ViewModels.Subject;
using ConsoleUI.Views.Implementations.SubMenus;
using ConsoleUI.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleUI.Contollers.Implementations.SubControllers
{
    public class SubjectController : ISubController<Subject>
    {
        private readonly IService<Subject> _subjectService;
        private readonly IMenuView<Subject, SubjectViewModel> _subjectMenu;
        private readonly IMapper _mapper;

        private bool exitFlag;

        public SubjectController(IService<Subject> subjectService, IMenuView<Subject, SubjectViewModel> subjectMenu, IMapper mapper)
        {
            _subjectService = subjectService;
            _subjectMenu = subjectMenu;
            _mapper = mapper;
        }

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
                        PrintAll();
                        break;
                    case ConsoleKey.NumPad2:
                        await Print();
                        break;
                    case ConsoleKey.NumPad3:
                        await Add();
                        break;
                    case ConsoleKey.NumPad4:
                        await Delete();
                        break;
                    case ConsoleKey.NumPad5:
                        await Update();
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
            _subjectMenu.PrintMenu();
        }

        private async Task Add()
        {
            var subject = _subjectMenu.GetFromInput();

            await _subjectService.AddAsync(subject);
        }

        private async Task Delete()
        {
            var id = _subjectMenu.GetIdFromInput();

            await _subjectService.DeleteAsync(id);
        }

        private async Task Update()
        {
            var id = _subjectMenu.GetIdFromInput();
            var group = await _subjectService.GetByIdAsync(id);

            var updatedGroup = _subjectMenu.Update(group);

            await _subjectService.UpdateAsync(updatedGroup);
        }

        private void PrintAll()
        {
            var groups = _subjectService.GetAll();
            var viewModels = _mapper.Map<IEnumerable<SubjectViewModel>>(groups);

            _subjectMenu.PrintAll(viewModels);
        }

        private async Task Print()
        {
            var id = _subjectMenu.GetIdFromInput();

            var group = await _subjectService.GetByIdAsync(id);
            var viewModel = _mapper.Map<SubjectViewModel>(group);

            _subjectMenu.Print(viewModel);
        }
    }
}
