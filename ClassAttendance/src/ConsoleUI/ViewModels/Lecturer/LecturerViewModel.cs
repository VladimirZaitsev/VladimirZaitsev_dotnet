namespace ConsoleUI.ViewModels.Lecturer
{
    public class LecturerViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public override string ToString()
        {
            var result = $"Name - {FirstName} {LastName}\nAddress - {Address}";

            return result;
        }

        public string ToStringWithId()
        {
            var result = $"Id - {Id} " + ToString();

            return result;
        }
    }
}
