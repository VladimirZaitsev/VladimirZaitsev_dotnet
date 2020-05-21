using BLL.Models;
using System.Collections.Generic;

namespace WebUI.Models.ViewModels.GroupModel
{
    public class GroupViewModel
    {
        public Group Group { get; set; }

        public List<Student> Students { get; set; }
    }
}
