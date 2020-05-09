using ConsoleUI.ViewModels.Lecturer;
using ConsoleUI.ViewModels.Student;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI.ViewModels.Group
{
    public class GroupViewModel
    {
        public IEnumerable<StudentViewModel> Students { get; set; }

        public IEnumerable<LecturerViewModel> Lecturers { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(Name);
            if (Students != null)
            {
                sb.AppendLine("Students:");
                foreach (var student in Students)
                {
                    sb.AppendLine(student.FirstName + " " + student.LastName);
                }
            }        

            return sb.ToString();
        }
    }
}
