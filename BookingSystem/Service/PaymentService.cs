using BookingSystem.DB;
using BookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Services
{
    public class PaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserPackageModel?> DeductCredit(int userId, int credits)
        {
            var userPackage = await _context.UserPackages.FirstOrDefaultAsync(up => up.userId == userId && up.creditRemain >= credits);
            if (userPackage != null)
            {
                userPackage.creditRemain -= credits;
                await _context.SaveChangesAsync();
            }
            return userPackage;
        }

        public async Task RefundCredits(int userId, int credits)
        {
            var userPackage = await _context.UserPackages.FirstOrDefaultAsync(up => up.userId == userId);
            if (userPackage != null)
            {
                userPackage.creditRemain += credits;
                await _context.SaveChangesAsync();
            }
        }
    }
}
