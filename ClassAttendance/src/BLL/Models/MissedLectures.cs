﻿namespace BLL.Models
{
    public class MissedLectures
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ClassId { get; set; }

        public int SubjectId { get; set; }
    }
}
