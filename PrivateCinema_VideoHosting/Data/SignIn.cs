using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PrivateCinema_VideoHosting.Data
{
    public class SignIn
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(12, ErrorMessage = "Login can not contain more than 12 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Login can contain only a-zA-Z characters and numerals 0-1")]
        public string Login { get; set; } = null!;
        [Required]
        [MaxLength(12, ErrorMessage = "Password can not contain more than 12 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Password can contain only a-zA-Z characters and numerals 0-1")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "This field cannot be empty")]
        [MaxLength(254, ErrorMessage = "Surname can not have more than 20 characters")]
        public string Email { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}
