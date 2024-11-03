using BookingSystem.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClassController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("schedule")]
        [AllowAnonymous]
        public async Task<IActionResult> GetClassSchedule()
        {
            var classes = await _context.Classes.ToListAsync();
            return Ok(classes);
        }
    }
}

