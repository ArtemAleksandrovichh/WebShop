using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using WebShope.DAL.Interfaces;
using WebShope.Domain.Entityes;
using WebShope.Domain.StaticClasses;
using WebShope.Domain.Helpers;
using WebShope.Domain.Models;
using WebShope.Service.Interfaces;
using System.Runtime.InteropServices;

namespace WebShope.Service.Realization
{
    public class UserAuthorizationService : IUserAuthorizationService
    {
        private IUserRepository UserRepository { get; set; }
        private IPhotoService PhotoService;
        public UserAuthorizationService(IUserRepository userRepository, IPhotoService photoService)
        {
            UserRepository = userRepository;
            PhotoService = photoService;
        }
        public async Task<bool> Register(UserRegisterViewModel user)
        {
            var imageUploadResult = PhotoService.AddPhoto(user.ProfileImage).Result.Url;
            var imageUrl = imageUploadResult.ToString();


            return await UserRepository.Create(CreateNewUser(user, imageUrl));

        }
        public async Task<bool> Authentication(UserLoginViewModel user, HttpContext context) {

            var _user = await UserRepository.GetByLoginAndPassword(user.Login, HashHelper.HashPassword(user.Password));
            if (_user is not null)
            {
                var claims = new List<Claim>(){
                    new Claim(IdentityTypes.Id, _user.Id.ToString()),
                    new Claim(IdentityTypes.Login, _user.Login),
                    new Claim(IdentityTypes.Name, _user.Name),
                    new Claim(IdentityTypes.Surname, _user.Surname),
                    new Claim(IdentityTypes.ProfileImageUrl, _user.ProfileImageUrl)
                    };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await context.SignInAsync(claimsPrincipal);
                return true;
            }
            else return false;
        }
        public async Task Update(User _user, HttpContext context)
        {
            await context.SignOutAsync();

            var claims = new List<Claim>(){
                    new Claim(IdentityTypes.Id, _user.Id.ToString()),
                    new Claim(IdentityTypes.Login, _user.Login),
                    new Claim(IdentityTypes.Name, _user.Name),
                    new Claim(IdentityTypes.Surname, _user.Surname),
                    new Claim(IdentityTypes.ProfileImageUrl, _user.ProfileImageUrl)
                    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await context.SignInAsync(claimsPrincipal);
        }


        private User CreateNewUser(UserRegisterViewModel user, string url)
        {
            
            return new()
            {
                Login = user.Login,
                Name = user.Name,
                Surname = user.Surname,
                Age = user.Age,
                Email = user.Email,
                Password = HashHelper.HashPassword(user.Password),
                ProfileImageUrl = url,
                Role = Role.User
            };

        }
    }
}
