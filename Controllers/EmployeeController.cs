using Crud_Angular_Web_API.Data;
using Crud_Angular_Web_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Crud_Angular_Web_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees =  await _employeeDbContext.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpPost]

        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            try
            {
                employee.Id = Guid.NewGuid();
                await _employeeDbContext.Employees.AddAsync(employee);
                await _employeeDbContext.SaveChangesAsync();

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        { 
            var employee =  await _employeeDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(employee == null)
            {
                return NotFound();
            }

            return Ok(employee);

        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployee)
        {
            var employee = await _employeeDbContext.Employees.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }
            employee.Name = updateEmployee.Name;
            employee.PhoneNumber = updateEmployee.PhoneNumber;
            employee.Email = updateEmployee.Email;
            employee.Salary = updateEmployee.Salary;
            employee.DepartmentId = updateEmployee.DepartmentId;

           await _employeeDbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _employeeDbContext.Employees.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }
            _employeeDbContext.Employees.Remove(employee);
            await _employeeDbContext.SaveChangesAsync();

            return Ok(employee);
        }

    }
}
