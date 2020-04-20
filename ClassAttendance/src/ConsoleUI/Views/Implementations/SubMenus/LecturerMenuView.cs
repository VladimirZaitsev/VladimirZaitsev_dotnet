using BLL.Models;
using ConsoleUI.Models.Lecturer;
using ConsoleUI.Views.Interfaces;
using System;
using System.Collections.Generic;

namespace ConsoleUI.Views.Implementations.SubMenus
{
    public class LecturerMenuView : IMenuView
    {
        public void PrintMenu()
        {
            Console.WriteLine("1. Get lecturers list");
            Console.WriteLine("2. Get lecturer by id");
            Console.WriteLine("3. Add new lecturer");
            Console.WriteLine("4. Delete lecturer");
            Console.WriteLine("5. Update lecturer");
            Console.WriteLine("0. Exit");
        }

        public void PrintLectures(IEnumerable<LecturerViewModel> lectures)
        {
            foreach (var lecture in lectures)
            {
                Console.WriteLine(lecture);
            }
        }

        public void PrintLecture(LecturerViewModel lecture)
        {
            Console.WriteLine(lecture);
        }

        public MissedClass GetLectureFromInput()
        {
            Console.WriteLine("Input first name");
            var firstName = Console.ReadLine();
            Console.WriteLine("Input last name");
            var lastName = Console.ReadLine();

            var lecture = new MissedClass
            {
                ClassId = classId,
                StudentId = studentId,
                SubjectId = subjectId,
            };

            return missedLecture;
        }

        public int GetIdFromInput()
        {
            var input = Console.ReadLine();
            var id = int.Parse(input);

            return id;
        }

        public MissedClass UpdateLecturer(LecturerViewModel student)
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
