using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WebShope.Domain.Attributes;

namespace WebShope.Domain.Models
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Вы не ввели логин")]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "Вы не ввели имя")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Вы не ввели фамилию")]
        public string Surname { get; set; } = null!;

        [Required(ErrorMessage = "Вы не ввели возраст")]
        [Range(1, 100, ErrorMessage = "Недопустимый возраст")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Вы не ввели емейл")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Вы не ввели пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Вы не подтвердили пароль")]
        public string RepeatPassword { get; set; } = null!;

        [Required(ErrorMessage = "Вы не добавили фото")]
        [FileContentType("image/png,image/jpeg,image/jpg", ErrorMessage = "Неверный формат файла")]
        public IFormFile ProfileImage { get; set; } = null!;

    }
}
