using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using ConsoleUI.Contollers.Interfaces;
using ConsoleUI.ViewModels.Student;
using ConsoleUI.Views.Implementations.SubMenus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleUI.Contollers.Implementations.SubControllers
{
    public class StudentController : ISubController
    {
        private readonly IService<Person> _studentService;
        private readonly IGroupService _groupService;
        private readonly StudentMenuView _studentMenu;
        private readonly IMapper _mapper;

        private bool exitFlag;

        public StudentController(IService<Person> studentService, IGroupService groupService, StudentMenuView studentMenu, IMapper mapper)
        {
            _studentService = studentService;
            _groupService = groupService;
            _studentMenu = studentMenu;
            _mapper = mapper;
        }

        public async Task HandleInput()
        {
            while (!exitFlag)
            {
                var input = Console.ReadKey().Key;
                PrintOperations();

                switch (input)
                {
                    case ConsoleKey.D1:
                        await PrintStudents();
                        break;
                    case ConsoleKey.D2:
                        await PrintStudent();
                        break;
                    case ConsoleKey.D3:
                        await AddStudent();
                        break;
                    case ConsoleKey.D4:
                        await DeleteStudent();
                        break;
                    case ConsoleKey.D5:
                        await UpdateStudent();
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
            _studentMenu.PrintMenu();
        }

        private async Task AddStudent()
        {
            var student = _studentMenu.GetPersonFromInput();

            await _studentService.AddAsync(student);
        }

        private async Task DeleteStudent()
        {
            var id = _studentMenu.GetIdFromInput();

            await _studentService.DeleteAsync(id);
        }
        
        private async Task UpdateStudent()
        {
            var id = _studentMenu.GetIdFromInput();
            var student = await _studentService.GetByIdAsync(id);
            var updatedUser = _studentMenu.UpdateStudent(student);

            await _studentService.UpdateAsync(updatedUser);
        }

        private async Task PrintStudents()
        {
            var students = _studentService.GetAll();
            var viewModels = new List<StudentViewModel>();

            foreach (var student in students)
            {
                var group = await _groupService.GetGroupByStudentIdAsync(student.Id);
                var viewModel = _mapper.Map<StudentViewModel>(student);
                viewModel.GroupName = group.Name;

                viewModels.Add(viewModel);
            }

            _studentMenu.PrintStudents(viewModels);
        }

        private async Task PrintStudent()
        {
            var id = _studentMenu.GetIdFromInput();

            var student = await _studentService.GetByIdAsync(id);

            _studentMenu.PrintStudent(student);
        }
    }
}
