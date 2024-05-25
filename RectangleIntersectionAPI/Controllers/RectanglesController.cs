using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RectangleIntersectionAPI.Data;
using RectangleIntersectionAPI.Data.Entities;
using RectangleIntersectionAPI.Helpers;

namespace RectangleIntersectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RectanglesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RectanglesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("intersecting")]
        public async Task<ActionResult<IEnumerable<Rectangle>>> GetIntersectingRectangles([FromBody] Segment segment)
        {
            var rectangles = await _context.Rectangles.ToListAsync();
            var intersectingRectangles = rectangles.Where(r => r.Intersects(segment)).ToList();

            return Ok(intersectingRectangles);
        }
    }
}
