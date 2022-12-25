using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace WebShope.Service.Interfaces
{
    public interface IPhotoService
    {
        public Task<DeletionResult> DeletePhoto(string _piblicId);

        public Task<ImageUploadResult> AddPhoto(IFormFile photo);
    }
}
