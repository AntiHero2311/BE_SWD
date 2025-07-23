using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE_SWD.Data;
using BE_SWD.Models;
using BE_SWD.Models.DTOs;

namespace BE_SWD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CoursesController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses() => await _context.Courses.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            return course == null ? NotFound() : course;
        }

        [HttpGet("by-mathcenter/{userId}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByMathCenter(int userId)
        {
            var mathCenter = await _context.MathCenters.FirstOrDefaultAsync(mc => mc.AccountId == userId);
            if (mathCenter == null)
            {
                return NotFound();
            }
            var courses = await _context.Courses
                .Where(c => c.MathCenterId == mathCenter.Id)
                .ToListAsync();
            return courses;
        }

        [HttpGet("verified")]
        public async Task<ActionResult<IEnumerable<Course>>> GetVerifiedCourses()
        {
            var courses = await _context.Courses.Where(c => c.IsVerified).ToListAsync();
            return courses;
        }

        [HttpGet("unverified")]
        public async Task<ActionResult<IEnumerable<Course>>> GetUnverifiedCourses()
        {
            var courses = await _context.Courses.Where(c => !c.IsVerified).ToListAsync();
            return courses;
        }

        [HttpPost]
        public async Task<ActionResult<Course>> CreateCourse(CourseCreateUpdateRequest request)
        {
            var course = new Course
            {
                Title = request.Title,
                Description = request.Description,
                CreatedByAccountId = request.CreatedByAccountId,
                MathCenterId = request.MathCenterId,
                IsVerified = request.IsVerified,
                VerifiedById = request.VerifiedById,
                Status = request.Status,
                Price = request.Price,
                CreatedAt = DateTime.Now
            };
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, CourseCreateUpdateRequest request)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();
            // Không cập nhật Id và CreatedAt
            course.Title = request.Title;
            course.Description = request.Description;
            course.CreatedByAccountId = request.CreatedByAccountId;
            course.MathCenterId = request.MathCenterId;
            course.IsVerified = request.IsVerified;
            course.VerifiedById = request.VerifiedById;
            course.Status = request.Status;
            course.Price = request.Price;
            try { await _context.SaveChangesAsync(); } catch (DbUpdateConcurrencyException) { if (!_context.Courses.Any(e => e.Id == id)) return NotFound(); else throw; }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 