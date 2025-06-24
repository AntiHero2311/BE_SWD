using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE_SWD.Data;
using BE_SWD.Models;

namespace BE_SWD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizzesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public QuizzesController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes() => await _context.Quizzes.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            return quiz == null ? NotFound() : quiz;
        }

        [HttpPost]
        public async Task<ActionResult<Quiz>> CreateQuiz(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetQuiz), new { id = quiz.QuizId }, quiz);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuiz(int id, Quiz quiz)
        {
            if (id != quiz.QuizId) return BadRequest();
            _context.Entry(quiz).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); } catch (DbUpdateConcurrencyException) { if (!_context.Quizzes.Any(e => e.QuizId == id)) return NotFound(); else throw; }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null) return NotFound();
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 