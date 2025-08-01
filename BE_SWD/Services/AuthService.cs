using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BE_SWD.Data;
using BE_SWD.Models;
using BE_SWD.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BE_SWD.Services
{
    public interface IAuthService
    {
        Task<object?> SignInAsync(SigninRequest request);
        Task<StudentAuthResponse?> SignUpStudentAsync(StudentSignupRequest request);
        Task<MathCenterAuthResponse?> SignUpMathCenterAsync(MathCenterSignupRequest request);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }

    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<object?> SignInAsync(SigninRequest request)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Email == request.Email && a.Status);

            // Compare plain password
            if (account == null || request.Password != account.Password)
            {
                return null;
            }

            var token = GenerateJwtToken(account);

            // Return different response based on role
            if (account.Role == "Student")
            {
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.AccountId == account.Id);
                if (student != null)
                {
                    return new StudentAuthResponse
                    {
                        Token = token,
                        Email = account.Email,
                        Role = account.Role,
                        UserId = account.Id,
                        FullName = student.FullName,
                        StudentId = student.Id
                    };
                }
            }
            else if (account.Role == "MathCenter")
            {
                var mathCenter = await _context.MathCenters
                    .FirstOrDefaultAsync(m => m.AccountId == account.Id);
                if (mathCenter != null)
                {
                    return new MathCenterAuthResponse
                    {
                        Token = token,
                        Email = account.Email,
                        Role = account.Role,
                        UserId = account.Id,
                        CenterName = mathCenter.CenterName,
                        IsVerified = mathCenter.IsVerified,
                        MathCenterId = mathCenter.Id
                    };
                }
            }
            else if (account.Role == "Admin")
            {
                var admin = await _context.Admins
                    .FirstOrDefaultAsync(a => a.AccountId == account.Id);
                if (admin != null)
                {
                    return new AdminAuthResponse
                    {
                        Token = token,
                        Email = account.Email,
                        Role = account.Role,
                        UserId = account.Id,
                        FullName = admin.FullName
                    };
                }
            }

            return null;
        }

        public async Task<StudentAuthResponse?> SignUpStudentAsync(StudentSignupRequest request)
        {
            if (await _context.Accounts.AnyAsync(a => a.Email == request.Email))
            {
                return null;
            }

            var account = new Account
            {
                Email = request.Email,
                PasswordHash = HashPassword(request.Password),
                Password = request.Password,
                Role = "Student",
                Status = true,
                CreatedAt = DateTime.Now
            };
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            var student = new Student
            {
                AccountId = account.Id,
                FullName = request.FullName,
                Phone = request.Phone,
                Address = request.Address,
                Status = true
            };
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(account);
            return new StudentAuthResponse
            {
                Token = token,
                Email = account.Email,
                Role = account.Role,
                UserId = account.Id,
                FullName = request.FullName
            };
        }

        public async Task<MathCenterAuthResponse?> SignUpMathCenterAsync(MathCenterSignupRequest request)
        {
            if (await _context.Accounts.AnyAsync(a => a.Email == request.Email))
            {
                return null;
            }

            var account = new Account
            {
                Email = request.Email,
                PasswordHash = HashPassword(request.Password),
                Password = request.Password,
                Role = "MathCenter",
                Status = true,
                CreatedAt = DateTime.Now
            };
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            var mathCenter = new MathCenter
            {
                AccountId = account.Id,
                CenterName = request.CenterName,
                Description = request.Description,
                Phone = request.Phone,
                Address = request.Address,
                IsVerified = false,
                Status = true
            };
            _context.MathCenters.Add(mathCenter);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(account);
            return new MathCenterAuthResponse
            {
                Token = token,
                Email = account.Email,
                Role = account.Role,
                UserId = account.Id,
                CenterName = request.CenterName,
                IsVerified = false
            };
        }

        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        public bool VerifyPassword(string password, string hash)
        {
            var hashedPassword = HashPassword(password);
            return hashedPassword == hash;
        }

        private string GenerateJwtToken(Account account)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "your-super-secret-key-with-at-least-32-characters"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Role, account.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? "SWD",
                audience: _configuration["Jwt:Audience"] ?? "SWD",
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
} 