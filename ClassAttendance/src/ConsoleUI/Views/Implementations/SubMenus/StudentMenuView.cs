﻿using BLL.Models;
using ConsoleUI.ViewModels.Student;
using ConsoleUI.Views.Interfaces;
using System;
using System.Collections.Generic;

namespace ConsoleUI.Views.Implementations.SubMenus
{
    public class StudentMenuView : IMenuView<Student, StudentViewModel>
    {
        public void PrintMenu()
        {
            Console.WriteLine("////////////////////////");
            Console.WriteLine("1. Get students list");
            Console.WriteLine("2. Get student by id");
            Console.WriteLine("3. Add new student");
            Console.WriteLine("4. Delete user");
            Console.WriteLine("5. Update user");
            Console.WriteLine("0. Exit");
            Console.WriteLine("////////////////////////");
        }

        public void PrintAll(IEnumerable<StudentViewModel> students)
        {
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }

        public void Print(StudentViewModel student)
        {
            Console.WriteLine(student);
        }

        public Student GetFromInput()
        {
            Console.WriteLine("Input first name");
            var firstName = Console.ReadLine();
            Console.WriteLine("Input last name");
            var lastName = Console.ReadLine();
            Console.WriteLine("Input address");
            var address = Console.ReadLine();

            var student = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
            };

            return student;
        }

        public int GetIdFromInput()
        {
            var input = Console.ReadLine();
            var id = int.Parse(input);

            return id;
        }

        public Student Update(Student student)
        {
            Console.WriteLine("Keep input empty if you don't want to update");
            Console.WriteLine($"Current first name - {student.FirstName}");

            var firstName = Console.ReadLine();
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrWhiteSpace(firstName))
            {
                student.FirstName = firstName;
            }

            var lastName = Console.ReadLine();
            if (string.IsNullOrEmpty(lastName) || string.IsNullOrWhiteSpace(lastName))
            {
                student.LastName = lastName;
            }

            return student;
        }
    }
}
