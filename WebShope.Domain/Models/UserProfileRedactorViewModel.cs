using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShope.Domain.Models
{
    public class UserProfileRedactorViewModel
    {

        [Required(ErrorMessage = "Вы не ввели имя")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Вы не ввели фамилию")]
        public string Surname { get; set; } = null!;

        [Required(ErrorMessage = "Вы не ввели возраст")]
        [Range(1, 100, ErrorMessage = "Недопустимый возраст")]
        public int Age { get; set; }
    }
}
