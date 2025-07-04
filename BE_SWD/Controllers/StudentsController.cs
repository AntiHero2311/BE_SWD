using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE_SWD.Data;
using BE_SWD.Models;

namespace BE_SWD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StudentsController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents() => await _context.Students.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            return student == null ? NotFound() : student;
        }

        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.Id) return BadRequest();
            _context.Entry(student).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); } catch (DbUpdateConcurrencyException) { if (!_context.Students.Any(e => e.Id == id)) return NotFound(); else throw; }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 