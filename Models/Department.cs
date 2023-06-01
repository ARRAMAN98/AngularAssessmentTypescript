using System.ComponentModel.DataAnnotations;

namespace Crud_Angular_Web_API.Models
{
    public class Department
    {
        [Key]
        public long DepartmentId { get; set; }
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
