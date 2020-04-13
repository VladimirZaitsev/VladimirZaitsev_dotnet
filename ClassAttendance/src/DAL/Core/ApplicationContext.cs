using DAL.DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.Core
{
    internal class ApplicationContext : DbContext
    {
        public ApplicationContext() { }

        public DbSet<ClassDto> Classes { get; set; }

        public DbSet<GroupDto> Groups { get; set; }

        public DbSet<PersonDto> Persons { get; set; }

        public DbSet<SubjectDto> Subjects { get; set; }

        public DbSet<MissedLecturesDto> Lectures { get; set; }
    }
}
