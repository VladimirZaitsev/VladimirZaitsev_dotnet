using DAL.Dtos;
using DAL.Extensions;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupDto>()
                .Property(group => group.StudentIds)
                .HasConversion(
                    id => string.Join(',', id),
                    id => id.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToInts());
            modelBuilder.Entity<ClassDto>()
                .Property(cls => cls.GroupIds)
                .HasConversion(
                    id => string.Join(',', id),
                    id => id.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToInts());
            base.OnModelCreating(modelBuilder);
        }
    }
}
