using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Models
{
    [Table("tblCountry")]
    public class CountryModel 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int countryId { get; set; }
        public string? countryName { get; set; }
        public DateTime? dateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? dateUpdated { get; set; }

    }

    public class CountryResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public bool isEndofpage => pageNo >= pageCount;
        public List<CountryModel>? countryData { get; set; }
    }
}
