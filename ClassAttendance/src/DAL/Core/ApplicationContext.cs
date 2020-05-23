using DAL.Dtos;
using DAL.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Core
{
    public class ApplicationContext : IdentityDbContext<UserDto>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ClassDto> Classes { get; set; }

        public DbSet<GroupDto> Groups { get; set; }

        public DbSet<StudentDto> Students { get; set; }

        public DbSet<LecturerDto> Lecturers { get; set; }

        public DbSet<SubjectDto> Subjects { get; set; }

        public DbSet<MissedLecturesDto> Lectures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassDto>()
                .Property(cls => cls.GroupIds)
                .HasConversion(
                    id => string.Join(',', id),
                    id => id.Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToInts());
            base.OnModelCreating(modelBuilder);
        }
    }
}
