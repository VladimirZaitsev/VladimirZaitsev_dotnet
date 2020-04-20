﻿using DAL.Interfaces;

namespace DAL.Dtos
{
    public class PersonDto : IEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Passport { get; set; }

        public string Phone { get; set; }

        public Status Status { get; set; }
    }
}
