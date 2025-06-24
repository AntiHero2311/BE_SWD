using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE_SWD.Data;
using BE_SWD.Models;
using BE_SWD.Models.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace BE_SWD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AuthController(ApplicationDbContext context) => _context = context;

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
                return BadRequest("Username already exists");

            var user = new User
            {
                Username = request.Username,
                PasswordHash = HashPassword(request.Password),
                Role = request.Role,
                Email = request.Email,
                MathCenterId = request.MathCenterId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { user.UserId, user.Username, user.Role });
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Signin([FromBody] User login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == login.Username);
            if (user == null || user.PasswordHash != HashPassword(login.PasswordHash))
                return Unauthorized("Invalid credentials");
            // Return a dummy token for now
            return Ok(new { token = "dummy-token", user.UserId, user.Username, user.Role });
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
} 