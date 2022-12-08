using System.ComponentModel.DataAnnotations;

namespace WebShope.Domain.Models
{
    public class UserLoginViewModel
    {
        [Required]
        public string Login { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

    }
}
