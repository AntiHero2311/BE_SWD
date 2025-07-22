using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE_SWD.Data;
using BE_SWD.Models;
using BE_SWD.Models.DTOs;

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

        [HttpGet("by-lesson/{lessonId}")]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzesByLesson(int lessonId)
        {
            var quizzes = await _context.Quizzes.Where(q => q.LessonId == lessonId).ToListAsync();
            return quizzes;
        }

        [HttpPost]
        public async Task<ActionResult<Quiz>> CreateQuiz(QuizCreateRequest request)
        {
            // Lấy OrderIndex lớn nhất của các quiz trong lesson này
            var maxOrder = await _context.Quizzes
                .Where(q => q.LessonId == request.LessonId)
                .MaxAsync(q => (int?)q.OrderIndex) ?? 0;

            var quiz = new Quiz
            {
                LessonId = request.LessonId,
                Title = request.Title,
                Status = request.Status,
                OrderIndex = maxOrder + 1
            };
            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetQuiz), new { id = quiz.Id }, quiz);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuiz(int id, Quiz quiz)
        {
            if (id != quiz.Id) return BadRequest();
            _context.Entry(quiz).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); } catch (DbUpdateConcurrencyException) { if (!_context.Quizzes.Any(e => e.Id == id)) return NotFound(); else throw; }
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