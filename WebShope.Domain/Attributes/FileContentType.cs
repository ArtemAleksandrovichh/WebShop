using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebShope.Domain.Attributes
{
    public class FileContentType: ValidationAttribute
    {
        IEnumerable<string> _contentTypes;
        public FileContentType(string contentTypes)
        {
            _contentTypes = contentTypes.Split(",").Select(x => x.Trim());
        }

        public override bool IsValid(object? value)
        {
            return (value is IFormFile file && _contentTypes.Contains(file.ContentType));

        }
    }
}
