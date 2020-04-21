using DAL.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DAL.Core
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<ClassDto> Classes { get; set; }

        public DbSet<GroupDto> Groups { get; set; }

        public DbSet<PersonDto> Persons { get; set; }

        public DbSet<SubjectDto> Subjects { get; set; }

        public DbSet<MissedLecturesDto> Lectures { get; set; }
    }
}
