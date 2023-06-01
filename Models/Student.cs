using System.ComponentModel.DataAnnotations;

namespace Crud_Angular_Web_API.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Course { get; set; }
        public float Percentage { get; set; }
        public long DepartmentId { get; set; }
    }
}