using BookingSystem.DB;
using BookingSystem.Models;
using BookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Collections.Concurrent;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PaymentService _paymentService;
        private static readonly ConcurrentDictionary<int, SemaphoreSlim> _classLocks = new();

        public BookingController(AppDbContext context, PaymentService paymentService)
        {
            _context = context;
            _paymentService = paymentService;
        }

        [Authorize]
        [HttpPost("book")]
        public async Task<IActionResult> BookClass(int classId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var classInfo = await _context.Classes.FindAsync(classId);
            if (classInfo == null) return NotFound(new { Message = "Class not found" });

            if (!await IsValidBooking(userId, classInfo))
                return BadRequest(new { Message = "Cannot book overlapping class times" });

            var classLock = _classLocks.GetOrAdd(classId, _ => new SemaphoreSlim(1, 1));
            await classLock.WaitAsync();
            try
            {
                if (await IsClassFull(classId))
                {
                    await AddToWaitlist(userId, classId);
                    return Ok(new { Message = "Class is full. Added to waitlist." });
                }

                var userPackage = await _paymentService.DeductCredit(userId, classInfo.creditToBuy ?? 0);
                if (userPackage == null)
                    return BadRequest(new { Message = "Insufficient credits" });

                var booking = new BookingModel
                {
                    userId = userId,
                    classId = classId,
                    statusName = "Booked",
                    bookedAt = DateTime.Now
                };
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Class booked successfully" });
            }
            finally
            {
                classLock.Release();
            }
        }

        [Authorize]
        [HttpPost("cancel")]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            var booking = await _context.Bookings.Include(b => b.Class).FirstOrDefaultAsync(b => b.bookingId == bookingId);
            if (booking == null) return NotFound(new { Message = "Booking not found" });

            if (booking.Class == null || booking.Class.startTime == null)
            {
                return BadRequest(new { Message = "Class or class start time is not set." });
            }

            var timeUntilClass = booking.Class.startTime.Value - DateTime.Now;
            if (timeUntilClass.TotalHours > 4)
            {
                var creditAmount = booking.Class.creditToBuy ?? 0;
                await _paymentService.RefundCredits(booking.userId, creditAmount);
            }

            booking.statusName = "Canceled";
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();

            await ProcessWaitlist(booking.Class.classId);

            return Ok(new { Message = "Booking canceled successfully" });
        }

        [Authorize]
        [HttpGet("schedules/{country}")]
        public async Task<IActionResult> GetSchedules(string country)
        {
            var classes = await _context.Classes
                .Where(c => c.countryName == country)
                .Select(c => new { c.classId, c.className, c.startTime, c.creditToBuy, c.maxSlots })
                .ToListAsync();

            return Ok(classes);
        }

        private async Task<bool> IsValidBooking(int userId, ClassModel classInfo)
        {
            return !await _context.Bookings.AnyAsync(b =>
                b.userId == userId &&
                b.Class!.startTime < classInfo.endTime &&
                b.Class.endTime > classInfo.startTime);
        }

        private async Task<bool> IsClassFull(int classId)
        {
            var bookedCount = await _context.Bookings.CountAsync(b => b.classId == classId && b.statusName == "Booked");
            return bookedCount >= (await _context.Classes.FindAsync(classId))!.maxSlots;
        }

        private async Task AddToWaitlist(int userId, int classId)
        {
            var waitlist = new WaitlistModel { bookingId = userId, classId = classId, addAt = DateTime.Now };
            _context.Waitlists.Add(waitlist);
            await _context.SaveChangesAsync();
        }

        private async Task ProcessWaitlist(int classId)
        {
            var waitlistedUser = await _context.Waitlists
                .Where(w => w.classId == classId)
                .OrderBy(w => w.addAt)
                .FirstOrDefaultAsync();

            if (waitlistedUser != null)
            {
                var booking = new BookingModel
                {
                    userId = waitlistedUser.bookingId,
                    classId = classId,
                    statusName = "Booked",
                    bookedAt = DateTime.Now
                };
                _context.Bookings.Add(booking);
                _context.Waitlists.Remove(waitlistedUser);
                await _context.SaveChangesAsync();
            }
        }

        [HttpPost("endclass/{classId}")]
        public async Task<IActionResult> EndClass(int classId)
        {
            var waitlistUsers = await _context.Waitlists
                .Where(w => w.classId == classId)
                .ToListAsync();

            foreach (var user in waitlistUsers)
            {
                await _paymentService.RefundCredits(user.bookingId, 0);
                _context.Waitlists.Remove(user);
            }

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Credits refunded to waitlisted users." });
        }

        [Authorize]
        [HttpPost("checkin")]
        public async Task<IActionResult> CheckIn(int bookingId)
        {
            var booking = await _context.Bookings.Include(b => b.Class).FirstOrDefaultAsync(b => b.bookingId == bookingId);
            if (booking == null) return NotFound(new { Message = "Booking not found" });

            var currentTime = DateTime.Now;
            if (booking.Class!.startTime <= currentTime && booking.Class.endTime >= currentTime)
            {
                return Ok(new { Message = "Checked in successfully." });
            }

            return BadRequest(new { Message = "Check-in is only allowed during the class time." });
        }
    }
}
