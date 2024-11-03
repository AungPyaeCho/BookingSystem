using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Models
{
    [Table("tblClass")]
    public class ClassModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int classId { get; set; }
        public string? className { get; set; }
        public int countryId { get; set; }
        public string? countryName { get; set; }
        public int? creditToBuy { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public int? classDuration { get; set; }
        public int? maxSlots { get; set; }
        public DateTime? dateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? dateUpdated { get; set; }


        [ForeignKey("countryId")]
        public CountryModel? Country { get; set; }
    }

    public class ClassResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public bool isEndofpage => pageNo >= pageCount;
        public List<ClassModel>? classData { get; set; }
    }
}
