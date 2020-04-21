using BLL.Models;
using ConsoleUI.ViewModels.Lecturer;
using ConsoleUI.Views.Interfaces;
using DAL.Dtos;
using System;
using System.Collections.Generic;

namespace ConsoleUI.Views.Implementations.SubMenus
{
    public class LecturerMenuView : IMenuView<Person, LecturerViewModel>
    {
        public void PrintMenu()
        {
            Console.WriteLine("////////////////////////");
            Console.WriteLine("1. Get lecturers list");
            Console.WriteLine("2. Get lecturer by id");
            Console.WriteLine("3. Add new lecturer");
            Console.WriteLine("4. Delete lecturer");
            Console.WriteLine("5. Update lecturer");
            Console.WriteLine("0. Exit");
            Console.WriteLine("////////////////////////");
        }

        public void PrintAll(IEnumerable<LecturerViewModel> lecturers)
        {
            foreach (var lecturer in lecturers)
            {
                Console.WriteLine(lecturer);
            }
        }

        public void Print(LecturerViewModel lecturer)
        {
            Console.WriteLine(lecturer);
        }

        public Person GetFromInput()
        {
            Console.WriteLine("Input first name");
            var firstName = Console.ReadLine();
            Console.WriteLine("Input last name");
            var lastName = Console.ReadLine();

            var lecturer = new Person
            {
                FirstName = firstName,
                LastName = lastName,
                Status = Status.Lecturer,
            };

            return lecturer;
        }

        public Person Update(Person lecturer)
        {
            Console.WriteLine("Keep input empty if you don't want to update");
            Console.WriteLine($"Current first name - {lecturer.FirstName}");

            var firstName = Console.ReadLine();
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrWhiteSpace(firstName))
            {
                lecturer.FirstName = firstName;
            }

            var lastName = Console.ReadLine();
            if (string.IsNullOrEmpty(lastName) || string.IsNullOrWhiteSpace(lastName))
            {
                lecturer.LastName = lastName;
            }

            return lecturer;
        }
    }
}
