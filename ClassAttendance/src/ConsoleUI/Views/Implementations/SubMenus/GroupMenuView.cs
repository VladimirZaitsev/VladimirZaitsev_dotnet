using BLL.Models;
using ConsoleUI.ViewModels.Group;
using ConsoleUI.Views.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleUI.Views.Implementations.SubMenus
{
    public class GroupMenuView : IGroupMenuView<Group, GroupViewModel>
    {
        public void PrintMenu()
        {
            Console.WriteLine("////////////////////////");
            Console.WriteLine("1. Get group list");
            Console.WriteLine("2. Get group by id");
            Console.WriteLine("3. Add new group");
            Console.WriteLine("4. Delete group");
            Console.WriteLine("5. Update group");
            Console.WriteLine("0. Exit");
            Console.WriteLine("////////////////////////");
        }

        public void PrintAll(IEnumerable<GroupViewModel> groups)
        {
            foreach (var group in groups)
            {
                Console.WriteLine(group);
            }
        }

        public void Print(GroupViewModel group)
        {
            Console.WriteLine(group);
        }

        public Group GetFromInput(GroupViewModel model)
        {
            Console.WriteLine("Input lecturer id");
            foreach (var lecturer in model.Lecturers)
            {
                Console.WriteLine(lecturer.ToStringWithId());
            }
            var lecturerId = ((IGroupMenuView<Group, GroupViewModel>)this).GetIdFromInput();

            Console.WriteLine("Input group name");
            var name = Console.ReadLine();

            Console.WriteLine("Input students ids (separate it with ' ')");
            foreach (var student in model.Students)
            {
                Console.WriteLine(student.ToStringWithId());
            }
            var idInput = Console.ReadLine();
            var studentIds = idInput.Split(' ')
                .Select(id => Convert.ToInt32(id))
                .ToList();

            var group = new Group
            {
                LecturerId = lecturerId,
                Name = name,
                StudentIds = studentIds,
            };

            return group;
        }

        public Group Update(Group group)
        {
            Console.WriteLine("Keep input empty if you don't want to update");
            Console.WriteLine($"Current group name - {group.Name}");

            var name = Console.ReadLine();
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                group.Name = name;
            }

            return group;
        }
    }
}
