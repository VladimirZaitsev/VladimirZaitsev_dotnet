using DAL.Interfaces;
using System.Collections.Generic;

namespace DAL.Domain
{
    public class Group : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LecturerId { get; set; }

        public List<int> StudentIds { get; set; }
    }
}
