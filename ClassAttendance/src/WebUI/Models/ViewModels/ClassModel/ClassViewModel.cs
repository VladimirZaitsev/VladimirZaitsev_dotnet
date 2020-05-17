using BLL.Models;
using System.Collections.Generic;

namespace WebUI.Models.ViewModels.ClassModel
{
    public class ClassViewModel
    {
        public Class Class { get; set; }

        public string LecturerName { get; set; }

        public string SubjectName { get; set; }

        public List<string> GroupNames { get; set; }

    }
}
