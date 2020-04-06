using DAL.Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Core
{
    internal class ApplicationContext : DbContext
    {
        public ApplicationContext() { }

        public DbSet<Class> Classes { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Lecturer> Lecturers { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<MissedLectures> Lectures { get; set; }
    }
}
