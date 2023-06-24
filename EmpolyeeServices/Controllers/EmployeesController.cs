using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Reflection;
using EmployeeServices.DTO;
using Microsoft.AspNetCore.Cors;
using EmpolyeeServices.Models;

namespace EmployeeServices.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public EmployeesController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<IEnumerable<EmployeeDTO>> GetEmployees()
        {
            //if (_context.Employees == null)
            //{
            //    return NotFound();
            //}
            return _context.Employees.Select(emp => new EmployeeDTO
            {
                EmployeeId = emp.EmployeeId,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Title = emp.Title
            });
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<EmployeeDTO> GetEmployees(int id)
        {
            var employees = await _context.Employees.FindAsync(id);

            return new EmployeeDTO
            {
                EmployeeId = employees.EmployeeId,
                FirstName = employees.FirstName,
                LastName = employees.LastName,
                Title = employees.Title
            };
        }

        //POST: api/Employees/Filter
        [HttpPost("Filter")]    //沒有{}包起來就是常數
        public async Task<IEnumerable<EmployeeDTO>> FilterEmployees([FromBody] EmployeeDTO employeeDTO)
        {
            return _context.Employees.Where(emp =>
                            emp.EmployeeId == employeeDTO.EmployeeId ||
                            emp.FirstName.Contains(employeeDTO.FirstName) ||
                            emp.LastName.Contains(employeeDTO.LastName) ||
                            emp.Title.Contains(employeeDTO.Title)
                    )
                    .Select(emp => new EmployeeDTO
                    {
                        EmployeeId = emp.EmployeeId,
                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                        Title = emp.Title
                    });
        }



        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<string> PutEmployees(int id, EmployeeDTO employees)
        {
            if (id != employees.EmployeeId)
            {
                return "修改員工紀錄發生錯誤!";
            }

            Employees emp = await _context.Employees.FindAsync(id);
            emp.FirstName = employees.FirstName;
            emp.LastName = employees.LastName;
            emp.Title = employees.Title;

            _context.Entry(emp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeesExists(id))
                {
                    return "修改員工紀錄發生錯誤!";
                }
                else
                {
                    throw;
                }
            }

            return "修改員工紀錄成功!";
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostEmployees(EmployeeDTO employees)
        {

            Employees emp = new Employees
            {
                FirstName = employees.FirstName,
                LastName = employees.LastName,
                Title = employees.Title
            };

            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();

            return "新增紀錄成功";
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteEmployees(int id)
        {
            var employees = await _context.Employees.FindAsync(id);
            if (employees == null)
            {
                return "刪除員工紀錄失敗";
            }

            try
            {
                _context.Employees.Remove(employees);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return "刪除關聯記錄失敗";
            }

            return "刪除員工紀錄成功";
        }

        private bool EmployeesExists(int id)
        {
            return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
