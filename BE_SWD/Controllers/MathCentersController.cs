using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE_SWD.Data;
using BE_SWD.Models;

namespace BE_SWD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MathCentersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public MathCentersController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MathCenter>>> GetMathCenters() => await _context.MathCenters.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<MathCenter>> GetMathCenter(int id)
        {
            var mathCenter = await _context.MathCenters.FindAsync(id);
            return mathCenter == null ? NotFound() : mathCenter;
        }

        [HttpPost]
        public async Task<ActionResult<MathCenter>> CreateMathCenter(MathCenter mathCenter)
        {
            _context.MathCenters.Add(mathCenter);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMathCenter), new { id = mathCenter.Id }, mathCenter);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMathCenter(int id, MathCenter mathCenter)
        {
            if (id != mathCenter.Id) return BadRequest();
            _context.Entry(mathCenter).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); } catch (DbUpdateConcurrencyException) { if (!_context.MathCenters.Any(e => e.Id == id)) return NotFound(); else throw; }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMathCenter(int id)
        {
            var mathCenter = await _context.MathCenters.FindAsync(id);
            if (mathCenter == null) return NotFound();
            _context.MathCenters.Remove(mathCenter);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 