using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using WebShope.Service.Interfaces;

namespace WebShope.Service.Realization
{
    public class PhotoService : IPhotoService
    {
        private Cloudinary Cloudinary;
        
        public PhotoService(IOptions<Account> config)
        {
            Cloudinary = new Cloudinary(config.Value);
        }
        public async Task<DeletionResult> DeletePhoto(string url)
        {
            string filename = System.IO.Path.GetFileNameWithoutExtension("https://res.cloudinary.com/dyl9yk2bb/image/upload/v1671369483/kywbjqiihzuajzhvdmy0.jpg");  

            var deletionParams = new DeletionParams(filename);
                       
            return await Cloudinary.DestroyAsync(deletionParams);
        }

        public async Task<ImageUploadResult> AddPhoto(IFormFile photo)
        {
            var stream = photo.OpenReadStream();
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(photo.FileName, stream)
            };

            return await Cloudinary.UploadAsync(uploadParams);
        }

    }
}
