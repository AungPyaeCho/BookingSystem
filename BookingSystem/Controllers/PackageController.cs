using BookingSystem.DB;
using BookingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PackageController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPackages()
        {
            var packages = await _context.Packages.ToListAsync();
            return Ok(packages);
        }
        [Authorize]
        [HttpPost("purchase")]
        public async Task<IActionResult> PurchasePackage(int packageId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var package = await _context.Packages.FindAsync(packageId);
            if (package == null) return NotFound(new { Message = "Package not found" });

            var existingUserPackage = await _context.UserPackages
                .FirstOrDefaultAsync(up => up.userId == userId && up.packageId == packageId);
            if (existingUserPackage != null)
            {
                return BadRequest(new { Message = "You already own this package." });
            }

            var userPackage = new UserPackageModel
            {
                userId = userId,
                packageId = packageId,
                creditRemain = package.credits ?? 0,
                expiryDate = DateTime.Now.AddDays(package.expiryDays ?? 0)
            };

            _context.UserPackages.Add(userPackage);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Purchase successful.", Package = userPackage });
        }

        [Authorize]
        [HttpGet("my-packages")]
        public async Task<IActionResult> GetMyPackages()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var userPackages = await _context.UserPackages
                .Where(up => up.userId == userId)
                .Select(up => new
                {
                    PackageId = up.packageId,
                    CreditsRemaining = up.creditRemain,
                    ExpiryDate = up.expiryDate,
                    Status = up.expiryDate < DateTime.Now ? "Expired" : "Active"
                })
                .ToListAsync();

            return Ok(userPackages);
        }
    }
}
