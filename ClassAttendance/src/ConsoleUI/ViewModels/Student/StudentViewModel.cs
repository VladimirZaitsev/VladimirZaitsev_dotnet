using BLL.Models;

namespace ConsoleUI.ViewModels.Student
{
    public class StudentViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string GroupName { get; set; }

        public override string ToString()
        {
            var result = $"Name - {FirstName} {LastName}\nAddress - {Address}\nGroup - {GroupName}";

            return result;
        }
    }
}
