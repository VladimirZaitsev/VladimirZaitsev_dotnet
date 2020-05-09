using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using ConsoleUI.Contollers.Interfaces;
using ConsoleUI.ViewModels.Student;
using ConsoleUI.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleUI.Contollers.Implementations.SubControllers
{
    public class StudentController : IStudentController
    {
        private readonly IService<Student> _studentService;
        private readonly IGroupService _groupService;
        private readonly IMenuView<Student, StudentViewModel> _studentMenu;
        private readonly IMapper _mapper;

        private bool exitFlag;

        public StudentController(IService<Student> studentService,
            IGroupService groupService,
            IMenuView<Student, StudentViewModel> studentMenu,
            IMapper mapper)
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
                Console.WriteLine();
                var input = Console.ReadKey().Key;
                PrintOperations();

                switch (input)
                {
                    case ConsoleKey.NumPad1:
                        await PrintStudents();
                        break;
                    case ConsoleKey.NumPad2:
                        await PrintStudent();
                        break;
                    case ConsoleKey.NumPad3:
                        await AddStudent();
                        break;
                    case ConsoleKey.NumPad4:
                        await DeleteStudent();
                        break;
                    case ConsoleKey.NumPad5:
                        await UpdateStudent();
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
            _studentMenu.PrintMenu();
        }

        private async Task AddStudent()
        {
            var student = _studentMenu.GetFromInput();

            
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
            var updatedUser = _studentMenu.Update(student);

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

            _studentMenu.PrintAll(viewModels);
        }

        private async Task PrintStudent()
        {
            var id = _studentMenu.GetIdFromInput();

            var student = await _studentService.GetByIdAsync(id);
            var viewModel = _mapper.Map<StudentViewModel>(student);

            _studentMenu.Print(viewModel);
        }
    }
}
