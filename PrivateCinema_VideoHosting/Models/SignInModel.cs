using System.ComponentModel.DataAnnotations;

namespace PrivateCinema_VideoHosting.Models
{
    public class SignInModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(12, ErrorMessage = "Login can not contain more than 12 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Login can contain only a-zA-Z characters and numerals 0-1")]
        public string Login { get; set; } = null!;
        [Required]
        [MaxLength(12, ErrorMessage = "Password can not contain more than 12 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Password can contain only a-zA-Z characters and numerals 0-1")]
        public string Password { get; set; } = null!;
    }
}
