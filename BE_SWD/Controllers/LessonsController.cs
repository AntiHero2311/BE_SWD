using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE_SWD.Data;
using BE_SWD.Models;
using BE_SWD.Models.DTOs;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using System.IO;

namespace BE_SWD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LessonsController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons() => await _context.Lessons.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetLesson(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            return lesson == null ? NotFound() : lesson;
        }

        [HttpPost]
        public async Task<ActionResult<Lesson>> CreateLesson(LessonCreateRequest request)
        {
            // Lấy OrderIndex lớn nhất của các lesson trong course này
            var maxOrder = await _context.Lessons
                .Where(l => l.CourseId == request.CourseId)
                .MaxAsync(l => (int?)l.OrderIndex) ?? 0;

            var lesson = new Lesson
            {
                CourseId = request.CourseId,
                Title = request.Title,
                Content = request.Content,
                Status = request.Status,
                OrderIndex = maxOrder + 1
            };
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLesson), new { id = lesson.Id }, lesson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLesson(int id, LessonCreateRequest request)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null) return NotFound();

            lesson.Title = request.Title;
            lesson.Content = request.Content; // URL video
            lesson.Status = request.Status;
            lesson.CourseId = request.CourseId;
            // Có thể cập nhật OrderIndex nếu muốn, hoặc giữ nguyên

            try { await _context.SaveChangesAsync(); } 
            catch (DbUpdateConcurrencyException) { if (!_context.Lessons.Any(e => e.Id == id)) return NotFound(); else throw; }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null) return NotFound();
            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Thêm endpoint upload video lên Firebase và lưu URL vào Content
        [HttpPost("{id}/upload-video")]
        public async Task<IActionResult> UploadVideo(int id, IFormFile videoFile)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null) return NotFound();

            // Đường dẫn tới file key json của Firebase
            var credential = GoogleCredential.FromFile("firebase-key.json");
            var storage = StorageClient.Create(credential);
            var bucket = "swp291-3cac5.appspot.com"; // Thay bằng tên bucket thật
            var fileName = $"lessons/{id}/{Guid.NewGuid()}_{videoFile.FileName}";

            using var stream = videoFile.OpenReadStream();
            await storage.UploadObjectAsync(bucket, fileName, videoFile.ContentType, stream);
            var url = $"https://storage.googleapis.com/{bucket}/{fileName}";

            lesson.Content = url;
            await _context.SaveChangesAsync();

            return Ok(new { videoUrl = url });
        }
    }
} 