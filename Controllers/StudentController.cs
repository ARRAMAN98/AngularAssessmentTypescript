using Crud_Angular_Web_API.Data;
using Crud_Angular_Web_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud_Angular_Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public StudentController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var student = await _employeeDbContext.Students.ToListAsync();

            return Ok(student);
        }

        [HttpPost]

        public async Task<IActionResult> AddStudent(Student student)
        {
            try
            {
                await _employeeDbContext.Students.AddAsync(student);
                await _employeeDbContext.SaveChangesAsync();

                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetStudent([FromRoute] int id)
        {
            var student = await _employeeDbContext.Students.FirstOrDefaultAsync(x => x.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);

        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] int id, Student updateStudent)
        {
            var student = await _employeeDbContext.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            student.StudentName = updateStudent.StudentName;
            student.Percentage = updateStudent.Percentage;
            student.Email = updateStudent.Email;
            student.Course = updateStudent.Course;
            student.DepartmentId = updateStudent.DepartmentId;

            await _employeeDbContext.SaveChangesAsync();

            return Ok(student);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var student = await _employeeDbContext.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            _employeeDbContext.Students.Remove(student);
            await _employeeDbContext.SaveChangesAsync();

            return Ok(student);
        }

    }
}
