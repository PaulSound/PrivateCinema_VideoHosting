using System.ComponentModel.DataAnnotations;

namespace PrivateCinema_VideoHosting.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "This field cannot be empty")]
        [MaxLength(12, ErrorMessage = "Login can not have more than 12 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Login can contain only a-zA-Z characters and numerals 0-1")]
        public string Login { get; set; } = null!;
        [Required(ErrorMessage = "This field cannot be empty")]
        [MaxLength(12, ErrorMessage = "Login can not have more than 12 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Login can contain only a-zA-Z characters and numerals 0-1")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "This field cannot be empty")]
        [MaxLength(20, ErrorMessage = "Name can not have more than 20 characters")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "Name can contain only a-zA-Z characters or а-яА-Я characters")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "This field cannot be empty")]
        [MaxLength(20, ErrorMessage = "Surname can not have more than 20 characters")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я]+$", ErrorMessage = "Surname can contain only a-zA-Z characters or а-яА-Я characters")]
        public string Surname { get; set; } = null!;
        [Required(ErrorMessage = "This field cannot be empty")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.(com|ru)$", ErrorMessage = "The wrong format of email has been inserted")]
        [MaxLength(254, ErrorMessage = "Surname can not have more than 20 characters")]
        public string Email { get; set; } = null!;
    }
}
