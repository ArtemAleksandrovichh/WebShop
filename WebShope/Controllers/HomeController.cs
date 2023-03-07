using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShope.DAL;
using WebShope.DAL.Interfaces;
using WebShope.Domain.Entityes;

namespace WebShope.Controllers
{
    public class HomeController : Controller
    {
     

        public HomeController([FromServices] ApplicationDbContext dbContext)
        {


        }

        [Authorize]
        public IActionResult Index([FromServices] ApplicationDbContext dbContext)
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("login", "user");
        }

    }
   
}
