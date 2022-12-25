using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShope.Domain.Models;

namespace WebShope.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Main()
        {
            var User = HttpContext.User;
            UserProfileViewModel profile = new UserProfileViewModel();
            if (User is not null)
            {
                profile.ImageUrl = User.FindFirst("ProfileImageUrl")?.Value;
                profile.Name = User.FindFirst("Name")?.Value;
                profile.Surname = User.FindFirst("Surname")?.Value;
            }

            return View(profile);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Personal()
        {
            return View();
        }

    }
}
