using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebShope.Domain.Models;
using WebShope.Service.Interfaces;
using WebShope.Service.Realization;

namespace WebShope.Controllers
{
    public class UserController : Controller
    {
        IUserAuthorizationService UserAuthorizationService;
        public UserController(IUserAuthorizationService userAuthorizationService)
        {
            UserAuthorizationService = userAuthorizationService;
        }

        [HttpGet]
        public IActionResult Login()
        {
                return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel user)
        {
            if (await UserAuthorizationService.Authentication(user, HttpContext))
                return Redirect("/home/index");
            else return null;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel user)
        {

            if (ModelState.IsValid)
            {
                if (await UserAuthorizationService.Register(user)) return RedirectToAction("Login");
                else return View(user);
            }
            else return View(user);
        }
    }
}

