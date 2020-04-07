﻿using DAL.Interfaces;

namespace DAL.Domain
{
    public class Subject : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
