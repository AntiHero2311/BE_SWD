using Microsoft.AspNetCore.Mvc;
using BE_SWD.Models.DTOs;
using BE_SWD.Services;

namespace BE_SWD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SigninRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.SignInAsync(request);
            if (response == null)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            return Ok(response);
        }

        [HttpPost("signup/student")]
        public async Task<IActionResult> SignUpStudent([FromBody] StudentSignupRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.SignUpStudentAsync(request);
            if (response == null)
            {
                return BadRequest(new { message = "Email already exists" });
            }

            return Ok(response);
        }

        [HttpPost("signup/mathcenter")]
        public async Task<IActionResult> SignUpMathCenter([FromBody] MathCenterSignupRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.SignUpMathCenterAsync(request);
            if (response == null)
            {
                return BadRequest(new { message = "Email already exists" });
            }

            return Ok(response);
        }
    }
} 