using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShope.Domain.Models
{
    public class BacketViewModel
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public int Amount { get; set; }
    }
}
