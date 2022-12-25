using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebShope.Domain.Models
{
    public class UserProfileViewModel
    {
        public string? ImageUrl { get; set; }
        public string? Name { get; set; } 
        public string? Surname { get; set; }


    }
}
