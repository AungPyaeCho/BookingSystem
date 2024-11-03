using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Models
{
    [Table("tblWaitlist")]
    public class WaitlistModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int wailistId { get; set; }
        public int bookingId { get; set; }
        public int classId { get; set; }
        public int statusId { get; set; }
        public string? statusName { get; set; }
        public DateTime? addAt { get; set; }
        public DateTime? dateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? dateUpdated { get; set; }


        [ForeignKey("classId")]
        public ClassModel? Class { get; set; }
        [ForeignKey("bookingId")]
        public BookingModel? Booking { get; set; }

        [ForeignKey("statusId")]
        public StatusModel? Status { get; set; }
    }

    public class WaitlistResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public bool isEndofpage => pageNo >= pageCount;
        public List<WaitlistModel>? waitlistData { get; set; }
    }
}
