using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using WebShope.DAL.Interfaces;
using WebShope.Domain.Entityes;
using WebShope.Domain.Helpers;
using WebShope.Domain.Models;
using WebShope.Service.Interfaces;


namespace WebShope.Service.Realization
{
    public class UserAuthorizationService : IUserAuthorizationService
    {
        private IUserRepository UserRepository { get; set; }
        public UserAuthorizationService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        public async Task<bool> Register(UserRegisterViewModel user)
        {
            if (user.Password == user.RepeatPassword)
            {
                var newUser = new User() { 
                    Login = user.Login,
                    Name = user.Name,
                    Surname = user.Surname,
                    Age = user.Age,
                    Email = user.Email,
                    Password = HashHelper.HashPassword(user.Password),
                    Role = Role.User
                };
                return await UserRepository.Create(newUser);
            }
            else return false;
        }
        public async Task<bool> Authentication(UserLoginViewModel user, HttpContext context) {

                if (await UserRepository.GetByLoginAndPassword(user.Login, HashHelper.HashPassword(user.Password)))
                {
                    var claims = new List<Claim>(){
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Name, user.Password)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await context.SignInAsync(claimsPrincipal);
                return true;
                }
                else return false;
        }
    }
}
