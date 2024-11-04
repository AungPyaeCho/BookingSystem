using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Service
{
    public class EmailService : IEmailService
    {
        public bool SendVerificationEmail(string toEmail, string verificationLink)
        {
            Console.WriteLine($"[Mock Verification] Auto-completing verification for email: {toEmail}");
            Console.WriteLine($"Verification Link: {verificationLink}");
            return true;
        }

        public bool SendPasswordResetEmail(string toEmail, string resetToken)
        {
            Console.WriteLine($"[Mock Password Reset] Auto-completing password reset for email: {toEmail}");
            Console.WriteLine($"Password Reset Token: {resetToken}");
            return true;
        }
    }
}
