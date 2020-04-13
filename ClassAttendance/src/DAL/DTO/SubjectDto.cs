using DAL.Interfaces;

namespace DAL.DTO
{
    public class SubjectDto : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
