using System.ComponentModel.DataAnnotations;

namespace WebShope.Models
{
    public class UserRegisterViewModel
    {
        [Required]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; } = null!;

        [Required]
        public string Surname { get; set; } = null!;

        [Required]
        public int Age { get; set; }

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; } = null!;

    }
}
