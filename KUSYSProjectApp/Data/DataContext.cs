using KUSYSProjectApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KUSYSProjectApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasData(
                    new Course() { CourseId = "CSI101", CourseName = "Introduction to Computer Science", IsDeleted = false },
                    new Course() { CourseId = "CSI102", CourseName = "Algorithms", IsDeleted = false },
                    new Course() { CourseId = "MAT101", CourseName = "Calculus", IsDeleted = false },
                    new Course() { CourseId = "PHY101", CourseName = "Physics", IsDeleted = false }
                );      
        }
    }
}
