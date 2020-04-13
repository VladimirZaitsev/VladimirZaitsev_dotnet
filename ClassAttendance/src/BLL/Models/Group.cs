using System.Collections.Generic;

namespace BLL.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LecturerId { get; set; }

        public List<int> StudentIds { get; set; }
    }
}
