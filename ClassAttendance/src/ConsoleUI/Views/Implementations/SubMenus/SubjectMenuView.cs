using BLL.Models;
using ConsoleUI.ViewModels.Subject;
using ConsoleUI.Views.Interfaces;
using System;
using System.Collections.Generic;

namespace ConsoleUI.Views.Implementations.SubMenus
{
    public class SubjectMenuView : IMenuView<Subject, SubjectViewModel>
    {
        public void PrintMenu()
        {
            Console.WriteLine("////////////////////////");
            Console.WriteLine("1. Get subject list");
            Console.WriteLine("2. Get subject by id");
            Console.WriteLine("3. Add new subject");
            Console.WriteLine("4. Delete subject");
            Console.WriteLine("5. Update subject");
            Console.WriteLine("0. Exit");
            Console.WriteLine("////////////////////////");
        }

        public void PrintAll(IEnumerable<SubjectViewModel> subjects)
        {
            foreach (var subject in subjects)
            {
                Console.WriteLine(subject);
            }
        }

        public void Print(SubjectViewModel subject)
        {
            Console.WriteLine(subject);
        }

        public Subject GetFromInput()
        {
            Console.WriteLine("Input subject name");
            var name = Console.ReadLine();

            var subject = new Subject
            {
                Name = name,
            };

            return subject;
        }

        public Subject Update(Subject subject)
        {
            Console.WriteLine("Keep input empty if you don't want to update");
            Console.WriteLine($"Current subject name - {subject.Name}");

            var name = Console.ReadLine();
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                subject.Name = name;
            }

            return subject;
        }
    }
}
