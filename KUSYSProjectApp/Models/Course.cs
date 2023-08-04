using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUSYSProjectApp.Models
{
    public class Course
    {
        [Key]
        public string CourseId { get; set; }
     
        public string CourseName { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
