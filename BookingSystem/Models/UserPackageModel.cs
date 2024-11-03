using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Models
{
    [Table("tblUserPackage")]
    public class UserPackageModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int upId { get; set; }
        public int userId { get; set; }
        public string? userName { get; set; }
        public int packageId { get; set; }
        public string? packagename { get; set; }
        public int? creditRemain { get; set; }
        public bool isExpire { get; set; }
        public DateTime purchaseDate { get; set; }
        public DateTime expiryDate { get; set; }
        public DateTime? dateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? dateUpdated { get; set; }


        [ForeignKey("userId")]
        public UserModel? User { get; set; }

        [ForeignKey("packcageId")]
        public PackageModel? Package { get; set; }
    }

    public class UserPackageResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public bool isEndofpage => pageNo >= pageCount;
        public List<UserPackageModel>? userPackageData { get; set; }
    }
}
