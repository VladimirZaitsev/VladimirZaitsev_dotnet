using ConsoleUI.ViewModels.Lecturer;
using ConsoleUI.ViewModels.Student;
using System.Collections.Generic;

namespace ConsoleUI.ViewModels.Group
{
    public class GroupViewModel
    {
        public IEnumerable<StudentViewModel> Students { get; set; }

        public IEnumerable<LecturerViewModel> Lecturers { get; set; }

        public string Name { get; set; }
    }
}
