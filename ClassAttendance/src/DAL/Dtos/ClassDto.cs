using DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace DAL.Dtos
{
    public class ClassDto : IEntity
    {
        public int Id { get; set; }

        public int LecturerId { get; set; }

        public int SubjectId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<int> GroupIds { get; set; }

        public int CabinetId { get; set; }
    }
}
