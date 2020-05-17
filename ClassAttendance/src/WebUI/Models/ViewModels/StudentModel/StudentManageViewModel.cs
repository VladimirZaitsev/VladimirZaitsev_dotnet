using BLL.Models;
using System.Collections.Generic;

namespace WebUI.Models.ViewModels.StudentModel
{
    public class StudentManageViewModel
    {
        public Student Student { get; set; }

        public List<Group> Groups { get; set; }
    }
}
