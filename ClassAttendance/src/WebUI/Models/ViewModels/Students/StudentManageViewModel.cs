using BLL.Models;
using System.Collections.Generic;

namespace WebUI.Models.ViewModels.Students
{
    public class StudentManageViewModel
    {
        public Student Student { get; set; }

        public IEnumerable<Group> Groups { get; set; }
    }
}
