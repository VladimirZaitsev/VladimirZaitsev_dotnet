using BLL.Models;

namespace ConsoleUI.ViewModels.MissedLectures
{
    public class MissedClassViewModel
    {
        public MissedClass Lecture { get; set; }

        public string StudentName { get; set; }

        public string SubjectName { get; set; }

        public string LecturerName { get; set; }

        public override string ToString()
        {
            var result = $"Student name - {StudentName}\nSubject - {SubjectName}\nGroup - {LecturerName}";

            return result;
        }
    }
}
