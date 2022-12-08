using System.Net.Http;
using Microsoft.AspNetCore.Http;
using WebShope.Domain.Models;

namespace WebShope.Service.Interfaces
{
    public interface IUserAuthorizationService
    {
        public Task<bool> Register(UserRegisterViewModel user);
        public Task<bool> Authentication(UserLoginViewModel user, HttpContext context);
    }
}
