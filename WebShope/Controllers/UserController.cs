using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebShope.Domain.Models;
using WebShope.Service.Interfaces;
using WebShope.Service.Realization;
using WebShope.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebShope.Controllers
{
    public class UserController : Controller
    {
        IUserAuthorizationService UserAuthorizationService;
        IPhotoService PhotoService;
        public UserController(IUserAuthorizationService userAuthorizationService, IPhotoService photoService)
        {
            UserAuthorizationService = userAuthorizationService;
            PhotoService = photoService;
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
            else 
            {
                ModelState.AddModelError("", "Вы ввели неверный логин или пароль.");
                return View(user);
            }
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
                else
                {
                    ModelState.AddModelError("", "Такой пользователь уже существует.");
                    return View(user);
                }
            }
            else return View(user);
        }

        

       /* [HttpPost]
        [Authorize]
        public async Task<IActionResult> Profile(UserProfileViewModel userProfile)
        {
            if (ModelState.IsValid)
            {

                await PhotoService.DeletePhoto(userProfile.ImageUrl);

                var newImage = await PhotoService.AddPhoto(userProfile.formFile);
                userProfile.ImageUrl = newImage.Url.ToString();

            }
            return View(userProfile);
        }*/

    }
}

