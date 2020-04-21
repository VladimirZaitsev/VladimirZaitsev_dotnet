using BLL.Models;
using ConsoleUI.ViewModels.MissedLectures;
using ConsoleUI.Views.Interfaces;
using System;
using System.Collections.Generic;

namespace ConsoleUI.Views.Implementations.SubMenus
{
    public class LecturesMenuView : ILecturesMenuView<MissedClass, MissedClassViewModel>
    {
        public void PrintMenu()
        {
            Console.WriteLine("////////////////////////");
            Console.WriteLine("1. Get missed classes list");
            Console.WriteLine("2. Get missed class by id");
            Console.WriteLine("3. Add new missed class");
            Console.WriteLine("4. Delete missed class");
            Console.WriteLine("5. Update missed class");
            Console.WriteLine("0. Exit");
            Console.WriteLine("////////////////////////");
        }

        public void PrintAll(IEnumerable<MissedClassViewModel> lectures)
        {
            foreach (var lecture in lectures)
            {
                Console.WriteLine(lecture);
            }
        }

        public void Print(MissedClassViewModel lecture)
        {
            Console.WriteLine(lecture);
        }

        public MissedClass GetFromInput()
        {
            Console.WriteLine("Input first name");
            var classInput = Console.ReadLine();
            var classId = int.Parse(classInput);

            Console.WriteLine("Input first name");
            var studentInput = Console.ReadLine();
            var studentId = int.Parse(studentInput);

            Console.WriteLine("Input first name");
            var subjectInput = Console.ReadLine();
            var subjectId = int.Parse(subjectInput);

            var lecture = new MissedClass
            {
                ClassId = classId,
                StudentId = studentId,
            };

            return lecture;
        }

        public int GetIdFromInput()
        {
            var input = Console.ReadLine();
            var id = int.Parse(input);

            return id;
        }

        public MissedClass Update(MissedClassViewModel lecture)
        {
            Console.WriteLine("Keep input empty if you don't want to update");
            Console.WriteLine($"Current classId - {lecture.StudentName}");
            Console.WriteLine($"Current lecturer - {lecture.LecturerName}");
            Console.WriteLine($"Current subject - {lecture.SubjectName}");

            return new MissedClass();
        }
    }
}
