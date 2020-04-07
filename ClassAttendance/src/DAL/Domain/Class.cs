using DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace DAL.Domain
{
    public class Class : IEntity
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
