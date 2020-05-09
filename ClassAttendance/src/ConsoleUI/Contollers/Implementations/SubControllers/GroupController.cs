using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using ConsoleUI.Contollers.Interfaces;
using ConsoleUI.ViewModels.Group;
using ConsoleUI.ViewModels.Lecturer;
using ConsoleUI.ViewModels.Student;
using ConsoleUI.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleUI.Contollers.Implementations.SubControllers
{
    public class GroupController : ISubController<Group>
    {
        private readonly IService<Student> _studentService;
        private readonly IService<Lecturer> _lecturerService;
        private readonly IGroupService _groupService;
        private readonly IGroupMenuView<Group, GroupViewModel> _groupMenu;
        private readonly IMapper _mapper;

        public GroupController(IService<Student> studentService,
            IService<Lecturer> lecturerService,
            IGroupService groupService,
            IGroupMenuView<Group, GroupViewModel> groupMenu,
            IMapper mapper)
        {
            _studentService = studentService;
            _lecturerService = lecturerService;
            _groupService = groupService;
            _groupMenu = groupMenu;
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
            _groupMenu.PrintMenu();
        }

        private async Task Add()
        {
            var viewModel = GetViewModel();
            var group = _groupMenu.GetFromInput(viewModel);

            await _groupService.AddAsync(group);
        }

        private async Task Delete()
        {
            var id = _groupMenu.GetIdFromInput();

            await _studentService.DeleteAsync(id);
        }

        private async Task Update()
        {
            var id = _groupMenu.GetIdFromInput();
            var group = await _groupService.GetByIdAsync(id);

            var updatedGroup = _groupMenu.Update(group);

            await _groupService.UpdateAsync(updatedGroup);
        }

        private void PrintAll()
        {
            var groups = _groupService.GetAll();
            var viewModels = _mapper.Map<IEnumerable<GroupViewModel>>(groups);

            _groupMenu.PrintAll(viewModels);
        }

        private async Task Print()
        {
            var id = _groupMenu.GetIdFromInput();

            var group = await _groupService.GetByIdAsync(id);
            var viewModel = _mapper.Map<GroupViewModel>(group);

            _groupMenu.Print(viewModel);
        }

        private GroupViewModel GetViewModel()
        {
            var viewModel = new GroupViewModel
            {
                Lecturers = _mapper.Map<IEnumerable<LecturerViewModel>>(_lecturerService.GetAll()),
                Students = _mapper.Map<IEnumerable<StudentViewModel>>(_studentService.GetAll()),
            };

            return viewModel;
        }
    }
}
