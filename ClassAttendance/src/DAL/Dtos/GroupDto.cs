﻿using DAL.Interfaces;
using System.Collections.Generic;

namespace DAL.Dtos
{
    public class GroupDto : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
