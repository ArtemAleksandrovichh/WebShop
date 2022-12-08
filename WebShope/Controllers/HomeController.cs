using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShope.DAL;
using WebShope.Models;

namespace WebShope.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index([FromServices]ApplicationDbContext dbContext)
        {
            var a = dbContext.Users.ToList();
            var a1 = a[0].Login;
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel user)
        {
            var claims = new List<Claim>(){
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Name, user.Password)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(claimsPrincipal);

            return Redirect("/home/index");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterViewModel user)
        {
            if (ModelState.IsValid) return RedirectToAction("Login");
            else return View(user);
        }
    }
   
}
