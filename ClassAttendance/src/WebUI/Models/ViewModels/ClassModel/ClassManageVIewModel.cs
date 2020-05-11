using BLL.Models;
using System;
using System.Collections.Generic;

namespace WebUI.Models.ViewModels.ClassModel
{
    public class ClassManageViewModel
    {
        public int Id { get; set; }

        public IEnumerable<Lecturer> Lecturers { get; set; }

        public int LecturerId { get; set; }

        public IEnumerable<Subject> Subjects { get; set; }

        public int SubjectId { get; set; }

        public List<Group> Groups { get; set; }

        public List<Group> SelectedGroups { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CabinetId { get; set; }
    }
}
