using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE_SWD.Data;
using BE_SWD.Models;

namespace BE_SWD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserQuizResultsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserQuizResultsController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserQuizResult>>> GetUserQuizResults() => await _context.UserQuizResults.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<UserQuizResult>> GetUserQuizResult(int id)
        {
            var result = await _context.UserQuizResults.FindAsync(id);
            return result == null ? NotFound() : result;
        }

        [HttpPost]
        public async Task<ActionResult<UserQuizResult>> CreateUserQuizResult(UserQuizResult result)
        {
            _context.UserQuizResults.Add(result);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserQuizResult), new { id = result.ResultId }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserQuizResult(int id, UserQuizResult result)
        {
            if (id != result.ResultId) return BadRequest();
            _context.Entry(result).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); } catch (DbUpdateConcurrencyException) { if (!_context.UserQuizResults.Any(e => e.ResultId == id)) return NotFound(); else throw; }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserQuizResult(int id)
        {
            var result = await _context.UserQuizResults.FindAsync(id);
            if (result == null) return NotFound();
            _context.UserQuizResults.Remove(result);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 