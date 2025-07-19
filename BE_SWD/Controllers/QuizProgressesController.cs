using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE_SWD.Data;
using BE_SWD.Models;
using BE_SWD.Models.DTOs;

namespace BE_SWD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizProgressesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public QuizProgressesController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizProgress>>> GetQuizProgresses() => await _context.QuizProgresses.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizProgress>> GetQuizProgress(int id)
        {
            var progress = await _context.QuizProgresses.FindAsync(id);
            return progress == null ? NotFound() : progress;
        }

        [HttpPost]
        public async Task<ActionResult<QuizProgress>> CreateQuizProgress(QuizProgressCreateRequest request)
        {
            // Lấy AttemptNumber lớn nhất của học sinh cho quiz này
            var maxAttempt = await _context.QuizProgresses
                .Where(qp => qp.EnrollmentId == request.EnrollmentId && qp.QuizId == request.QuizId)
                .MaxAsync(qp => (int?)qp.AttemptNumber) ?? 0;

            var progress = new QuizProgress
            {
                EnrollmentId = request.EnrollmentId,
                QuizId = request.QuizId,
                Score = request.Score,
                AttemptNumber = maxAttempt + 1,
                AttemptDate = DateTime.Now
            };
            _context.QuizProgresses.Add(progress);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetQuizProgress), new { id = progress.Id }, progress);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuizProgress(int id, QuizProgress progress)
        {
            if (id != progress.Id) return BadRequest();
            _context.Entry(progress).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); } catch (DbUpdateConcurrencyException) { if (!_context.QuizProgresses.Any(e => e.Id == id)) return NotFound(); else throw; }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuizProgress(int id)
        {
            var progress = await _context.QuizProgresses.FindAsync(id);
            if (progress == null) return NotFound();
            _context.QuizProgresses.Remove(progress);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 