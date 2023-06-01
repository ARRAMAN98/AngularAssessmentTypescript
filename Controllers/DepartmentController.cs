using Crud_Angular_Web_API.Data;
using Crud_Angular_Web_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud_Angular_Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public DepartmentController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var department= await _employeeDbContext.Departments.ToListAsync();

            return Ok(department);
        }

        [HttpPost]

        public async Task<IActionResult> AddDepartment (Department department)
        {
            try
            {
                await _employeeDbContext.Departments.AddAsync(department);
                await _employeeDbContext.SaveChangesAsync();

                return Ok(department);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var department = await _employeeDbContext.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);

        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, Department updateDepartment)
        {
            var department = await _employeeDbContext.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }
            department.Name = updateDepartment.Name;
            department.DepartmentId = updateDepartment.DepartmentId;

            await _employeeDbContext.SaveChangesAsync();

            return Ok(department);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _employeeDbContext.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }
            _employeeDbContext.Departments.Remove(department);
            await _employeeDbContext.SaveChangesAsync();

            return Ok(department);
        }
    }
}
