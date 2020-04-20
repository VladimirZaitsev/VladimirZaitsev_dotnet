using BLL.Models;

namespace ConsoleUI.Models.MissedLectures
{
    public class MissedLectureViewModel
    {
        public MissedClass Lecture { get; set; }

        public string StudentName { get; set; }

        public string SubjectName { get; set; }

        public string LecturerName { get; set; }
    }
}
