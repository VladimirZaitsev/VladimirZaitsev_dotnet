using DAL.Core;
using System.Collections.Generic;

namespace DAL.Domain
{
    public class Group : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Student> Students { get; set; }
    }
}
