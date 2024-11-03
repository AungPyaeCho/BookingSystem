using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Models
{
    [Table("tblBooking")]
    public class BookingModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bookingId { get; set; }

        public int userId { get; set; }
        public string? userName { get; set; }

        public int classId { get; set; }
        public string? className { get; set; }

        public int statusId { get; set; }
        public string? statusName { get; set; }

        public int upId { get; set; }

        public DateTime totalHour { get; set; }

        public DateTime? bookedAt { get; set; }
        public DateTime? cancelAt { get; set; }
        public DateTime? dateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? dateUpdated { get; set; }

        public bool ssCheckedIn { get; set; } = false;

        [ForeignKey("UserId")]
        public UserModel? User { get; set; }

        [ForeignKey("ClassId")]
        public ClassModel? Class { get; set; }

        [ForeignKey("StatusId")]
        public StatusModel? Status { get; set; }

        [ForeignKey("UpId")]
        public UserPackageModel? UserPackage { get; set; }
    }

    public class BookingResponseModel
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public bool IsEndOfPage => PageNo >= PageCount;
        public List<BookingModel>? BookingData { get; set; }
    }
}
