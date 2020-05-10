using DAL.Interfaces;
using System.Collections.Generic;

namespace DAL.Dtos
{
    public class GroupDto : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int LecturerId { get; set; }

        public List<int> StudentIds { get; set; }
    }
}
