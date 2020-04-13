using System;
using System.Collections.Generic;

namespace BLL.Models
{
    public class Class
    {
        public int Id { get; set; }

        public int LecturerId { get; set; }

        public int SubjectId { get; set; }

        public DateTime Beginning { get; set; }

        public DateTime Ending { get; set; }

        public List<int> GroupIds { get; set; }

        public int CabinetId { get; set; }
    }
}
