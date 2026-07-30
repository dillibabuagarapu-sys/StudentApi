using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;


        public StudentsController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                var data = await _context.Students.ToListAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return Ok("ERROR: " + ex.Message);
            }
        }


        // GET STUDENT BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }



        // CREATE STUDENT
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            _context.Students.Add(student);

            await _context.SaveChangesAsync();

            return Ok(student);
        }



        // UPDATE STUDENT
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }


            _context.Entry(student).State = EntityState.Modified;

            await _context.SaveChangesAsync();


            return Ok(student);
        }



        // DELETE STUDENT
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);


            if (student == null)
            {
                return NotFound();
            }


            _context.Students.Remove(student);

            await _context.SaveChangesAsync();


            return Ok(new { message = "Student deleted successfully!" });
        }
    }
}