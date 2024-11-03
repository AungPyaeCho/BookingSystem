using BookingSystem.DB;
using BookingSystem.Models;
using BookingSystem.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public UserController(AppDbContext context, IEmailService emailService, IConfiguration configuration)
        {
            _context = context;
            _emailService = emailService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            userModel.SetEncryptedPassword(userModel.userPassword!);
            _context.Users.Add(userModel);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                var verificationLink = Url.Action("VerifyEmail", "User", new { userId = userModel.userId }, Request.Scheme)!;
                await _emailService.SendVerificationEmail(userModel.userEmail!, verificationLink);
                return Ok(new { Message = "Registration successful, please verify your email." });
            }

            return BadRequest(new { Message = "Registration failed." });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserModel userModel)
        {
            userModel.SetEncryptedPassword(userModel.userPassword!);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.userEmail == userModel.userEmail);
            if (user == null || user.userPassword != userModel.userPassword)
                return Unauthorized(new { Message = "Invalid credentials" });

            if (!user.isVarify)
                return Unauthorized(new { Message = "Email not verified. Please check your email." });

            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound(new { Message = "User not found" });

            return Ok(new
            {
                user.userId,
                user.userName,
                user.userEmail,
                user.isVarify
            });
        }

        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound(new { Message = "User not found" });

            if (!user.VerifyPassword(model.CurrentPassword))
                return BadRequest(new { Message = "Current password is incorrect" });

            user.SetEncryptedPassword(model.NewPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Password changed successfully" });
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.userEmail == model.Email);
            if (user == null) return NotFound(new { Message = "User not found" });

            var resetToken = GenerateResetToken();
            await _emailService.SendPasswordResetEmail(user.userEmail!, resetToken);

            return Ok(new { Message = "Password reset link has been sent to your email" });
        }

        [HttpPost("confirm-reset")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmResetPassword(ConfirmResetPasswordModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.userEmail == model.Email);
            if (user == null) return NotFound(new { Message = "User not found" });

            user.SetEncryptedPassword(model.NewPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Password has been reset successfully" });
        }

        private string GenerateJwtToken(UserModel user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
                new Claim(ClaimTypes.Email, user.userEmail!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateResetToken()
        {
            using var rng = RandomNumberGenerator.Create();
            var randomBytes = new byte[32];
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}
