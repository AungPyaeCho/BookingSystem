using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Models
{
    [Table("tblTransactionHistory")]
    public class TransactionHistoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int thId { get; set; }
        public int upId { get; set; }
        public int classId { get; set; }
        public string? className { get; set; }
        public int usedCredits { get; set; }
        public int refundCredits { get; set; }
        public string? transactionType { get; set; }
        public DateTime transactionDate { get; set; }
        public DateTime? dateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? dateUpdated { get; set; }


        [ForeignKey("upId")]
        public UserPackageModel? UserPackage { get; set; }

        [ForeignKey("classId")]
        public ClassModel? Class { get; set; }
    }

    public class TransactionHistoryResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public bool isEndofpage => pageNo >= pageCount;
        public List<TransactionHistoryModel>? transactionHistoryData { get; set; }
    }
}

