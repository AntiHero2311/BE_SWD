using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE_SWD.Data;
using BE_SWD.Models;

namespace BE_SWD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AnswersController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers() => await _context.Answers.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> GetAnswer(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            return answer == null ? NotFound() : answer;
        }

        [HttpGet("by-question/{questionId}")]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswersByQuestion(int questionId)
        {
            var answers = await _context.Answers.Where(a => a.QuestionId == questionId).ToListAsync();
            return answers;
        }

        [HttpPost]
        public async Task<ActionResult<Answer>> CreateAnswer(Answer answer)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAnswer), new { id = answer.Id }, answer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnswer(int id, Answer answer)
        {
            if (id != answer.Id) return BadRequest();
            _context.Entry(answer).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); } catch (DbUpdateConcurrencyException) { if (!_context.Answers.Any(e => e.Id == id)) return NotFound(); else throw; }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            if (answer == null) return NotFound();
            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 