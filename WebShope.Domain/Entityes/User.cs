using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShope.Domain.Entityes
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public int Age { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public Role Role { get; set; }
    }
}
