namespace ConsoleUI.ViewModels.Subject
{
    public class SubjectViewModel
    {
        public string Name { get; set; }

        public override string ToString() => $"Name - {Name}";
    }
}
