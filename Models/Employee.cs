namespace Crud_Angular_Web_API.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public long Salary { get; set; }
        public long DepartmentId { get; set; }  

    }
}
