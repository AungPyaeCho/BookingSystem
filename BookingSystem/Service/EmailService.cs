using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Service
{
    public class EmailService : IEmailService
    {
        public Task SendVerificationEmail(string toEmail, string verificationLink)
        {
            Console.WriteLine($"[Mock Email] Sending verification email to: {toEmail}");
            Console.WriteLine($"Verification Link: {verificationLink}");
            return Task.CompletedTask;
        }

        public Task SendPasswordResetEmail(string toEmail, string resetToken)
        {
            Console.WriteLine($"[Mock Email] Sending password reset email to: {toEmail}");
            Console.WriteLine($"Password Reset Token: {resetToken}");
            return Task.CompletedTask;
        }
    }
}
