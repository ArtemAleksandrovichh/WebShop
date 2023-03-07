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

        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;


        public int Age { get; set; }
    }
}
