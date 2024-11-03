using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Models
{
    public class UserLoginModel
    {
        [Key]
        public string? ldId { get; set; }
        public string? userId { get; set; }
        public string? adminEmail { get; set; }
        public string? userName { get; set; }
        public string? userType { get; set; }
        public string? sessionId { get; set; }
        public DateTime? sessionExpired { get; set; }
        public DateTime? loginAt { get; set; }
        public DateTime? logOutAt { get; set; }
        public string? deviceName { get; set; }
        public string? ipAddress { get; set; }
        public string? browserInfo { get; set; }
    }
}
