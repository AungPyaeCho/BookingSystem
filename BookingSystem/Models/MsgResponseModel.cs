using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Models
{
    public class MsgResopnseModel
    {
        public bool IsSuccess { get; set; }
        public string? responeMessage { get; set; }
    }
}
