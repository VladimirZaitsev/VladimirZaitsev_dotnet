﻿using BLL.Models;
using ConsoleUI.Models.MissedLectures;
using ConsoleUI.Views.Interfaces;
using System;
using System.Collections.Generic;

namespace ConsoleUI.Views.Implementations.SubMenus
{
    public class LecturesMenuView : IMenuView
    {
        public void PrintMenu()
        {
            Console.WriteLine("1. Get students list");
            Console.WriteLine("2. Get student by id");
            Console.WriteLine("3. Add new student");
            Console.WriteLine("4. Delete user");
            Console.WriteLine("5. Update user");
            Console.WriteLine("0. Exit");
        }

        public void PrintLectures(IEnumerable<MissedLecture> lectures)
        {
            foreach (var lecture in lectures)
            {
                Console.WriteLine(lecture);
            }
        }

        public void PrintLecture(MissedLecture lecture)
        {
            Console.WriteLine(lecture);
        }

        public MissedLecture GetLectureFromInput()
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

            var lecture = new MissedLecture
            {
                ClassId = classId,
                StudentId = studentId,
                SubjectId = subjectId,
            };

            return lecture;
        }

        public int GetIdFromInput()
        {
            var input = Console.ReadLine();
            var id = int.Parse(input);

            return id;
        }

        public MissedLecture UpdateLecture(MissedLectureViewModel lecture)
        {
            Console.WriteLine("Keep input empty if you don't want to update");
            Console.WriteLine($"Current classId - {lecture.StudentName}");
            Console.WriteLine($"Current lecturer - {lecture.LecturerName}");
            Console.WriteLine($"Current subject - {lecture.SubjectName}");

            return new MissedLecture();
        }
    }
}
