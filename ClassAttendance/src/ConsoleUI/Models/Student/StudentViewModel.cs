using BLL.Models;

namespace ConsoleUI.Models.Student
{
    public class StudentViewModel
    {
        public Person Student { get; set; }

        public string GroupName { get; set; }

        public override string ToString()
        {
            var result = $"Name - {Student.FirstName} {Student.MiddleName} {Student.LastName}\nGroup - {GroupName}";

            return result;
        }
    }
}
