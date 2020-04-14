using BLL.Interfaces;
using ConsoleUI.Contollers.Interfaces;
using ConsoleUI.Models.Student;
using ConsoleUI.Views.Implementations.SubMenus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleUI.Contollers.Implementations.SubControllers
{
    public class StudentController : ISubController
    {
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;
        private readonly StudentMenuView _studentMenu;

        private bool exitFlag;

        public StudentController(IStudentService studentService, StudentMenuView menuView)
        {
            _studentService = studentService;
            _studentMenu = menuView;
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

            await foreach (var student in students)
            {
                var group = await _groupService.GetGroupByStudentIdAsync(student.Id);
                viewModels.Add(new StudentViewModel
                {
                    Student = student,
                    GroupName = group.Name,
                });
            }

            _studentMenu.PrintStudents(viewModels);
        }

        private async Task PrintStudent()
        {
            var input = Console.ReadLine();
            var id = int.Parse(input);

            var student = await _studentService.GetByIdAsync(id);

            _studentMenu.PrintStudent(student);
        }
    }
}
