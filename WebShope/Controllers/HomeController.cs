using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShope.DAL;

namespace WebShope.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index([FromServices] ApplicationDbContext dbContext)
        {
            return View();
        }


        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("login", "user");
        }

    }
   
}
