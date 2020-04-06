using DAL.Core;

namespace DAL.Domain
{
    public class MissedLectures : IEntity
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int LectureId { get; set; }
    }
}
