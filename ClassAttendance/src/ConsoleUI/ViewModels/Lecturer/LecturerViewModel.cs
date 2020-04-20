using BLL.Models;

namespace ConsoleUI.ViewModels.Lecturer
{
    public class LecturerViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public override string ToString()
        {
            var result = $"Name - {FirstName} {LastName}\nAddress - {Address}";

            return result;
        }
    }
}
