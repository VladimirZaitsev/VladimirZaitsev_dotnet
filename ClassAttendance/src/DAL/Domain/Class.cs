using DAL.Core;
using System;
using System.Collections.Generic;

namespace DAL.Domain
{
    public class Class : IEntity
    {
        public int Id { get; set; }

        public Lecturer Lecturer { get; set; }

        public Subject Subject { get; set; }

        public DateTime Date { get; set; }

        public List<Group> Groups { get; set; }
    }
}
