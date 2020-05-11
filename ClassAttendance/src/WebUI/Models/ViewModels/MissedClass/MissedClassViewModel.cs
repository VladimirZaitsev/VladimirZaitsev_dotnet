using System;

namespace WebUI.Models.ViewModels.MissedClass
{
    public class MissedClassViewModel
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string GroupName { get; set; }

        public string StudentName { get; set; }

        public int ClassId { get; set; }

        public string SubjectName { get; set; }

        public DateTime Date { get; set; }
    }
}
