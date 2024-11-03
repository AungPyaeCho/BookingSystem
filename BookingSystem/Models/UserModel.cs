using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BookingSystem.Service;

namespace BookingSystem.Models
{
    [Table("tblUser")]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }

        public string? userName { get; set; }
        public string? userEmail { get; set; }
        public string? userPassword { get; set; }
        public string? verifyPassword { get; set; }
        public int? countryId { get; set; }
        public string? countryName { get; set; }
        public bool isVarify { get; set; }
        public DateTime? dateCreated { get; set; } = DateTime.UtcNow;
        public DateTime? dateUpdated { get; set; }

        public void SetEncryptedPassword(string plainPassword)
        {
            userPassword = Encryption.Encrypt(plainPassword);
        }

        public string GetDecryptedPassword()
        {
            return Encryption.Decrypt(userPassword!);
        }

        public bool VerifyPassword(string passwordToVerify)
        {
            return userPassword == Encryption.Encrypt(passwordToVerify);
        }

        [ForeignKey("countryId")]
        public CountryModel? Country { get; set; }
    }

    public class ChangePasswordModel
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }

    public class ResetPasswordModel
    {
        public string Email { get; set; } = string.Empty;
    }

    public class ConfirmResetPasswordModel
    {
        public string Email { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }

    public class loginResponeModel
    {
        public bool IsSuccess { get; set; }
        public string? responeMsg { get; set; }
    }

    public class UserResponseModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public int pageCount { get; set; }
        public bool isEndofpage => pageNo >= pageCount;
        public List<UserModel>? userData { get; set; }
    }
}

