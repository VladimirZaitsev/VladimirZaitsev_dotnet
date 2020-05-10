using DAL.Interfaces;

namespace DAL.Dtos
{
    public class MissedLecturesDto : IEntity
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ClassId { get; set; }
    }
}
