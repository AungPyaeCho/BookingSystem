using System.Threading.Tasks;

namespace BookingSystem.Service
{
    public interface IEmailService
    {
        bool SendVerificationEmail(string toEmail, string verificationLink);
        bool SendPasswordResetEmail(string toEmail, string resetToken);
    }
}
