using System.Threading.Tasks;

namespace BookingSystem.Service
{
    public interface IEmailService
    {
        Task SendVerificationEmail(string toEmail, string verificationLink);
        Task SendPasswordResetEmail(string toEmail, string resetToken);
    }
}
