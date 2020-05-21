using BLL.Models;
using System.Collections.Generic;
using WebUI.Models.ViewModels.ClassModel;

namespace WebUI.Models.ViewModels.MissedClass
{
    public class MissedClassManageViewModel
    {
        public int Id { get; set; }

        public int ClassId { get; set; }

        public int StudentId { get; set; }

        public List<ClassDisplayModel> Classes { get; set; }

        public List<Student> Students { get; set; }
    }
}
