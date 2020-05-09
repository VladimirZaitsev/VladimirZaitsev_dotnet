using DAL.Interfaces;

namespace DAL.Dtos
{
    public class StudentDto : IEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }
    }
}
