using Microsoft.AspNetCore.Mvc;
using NextQuest.Data;
using NextQuest.DTOs;
using NextQuest.Models;


namespace NextQuest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReviewController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReviewDto request)
        {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var review = new Review
                {
                    Title = request.Title,
                    Comment = request.Comment
                };

                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = review.Id }, review);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                return NotFound();
            return Ok(review);
        }
    }
}