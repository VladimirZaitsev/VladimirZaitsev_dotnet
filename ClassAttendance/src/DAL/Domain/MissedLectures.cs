using DAL.Interfaces;

namespace DAL.Domain
{
    public class MissedLectures : IEntity
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ClassId { get; set; }

        public int SubjectId { get; set; }
    }
}
