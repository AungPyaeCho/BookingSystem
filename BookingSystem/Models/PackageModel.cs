using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Models
{
    [Table("tblPackage")]
    public class PackageModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int packageId { get; set; }
        public string? packageName { get; set; }
        public int? countryId { get; set; }
        public string? countryName { get; set; }
        public int? credits { get; set; }
        public decimal? packagePrice { get; set; }
        public string? packageDescription { get; set; }
        public int? expiryDays { get; set; }
        public DateTime? dateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? dateUpdated { get; set; }

        [ForeignKey("countryId")]
        public CountryModel? Country { get; set; }
    }

    public class PackageResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public bool isEndofpage => pageNo >= pageCount;
        public List<PackageModel>? packageData { get; set; }
    }
}
