using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WebShope.DAL.Interfaces;
using WebShope.DAL.Repository;
using WebShope.Domain.Models;
using WebShope.Service.Interfaces;

namespace WebShope.Controllers
{
    enum ProfileViews
    {
        PERSONAL, INDEX
    }
    public class ProfileController : Controller
    {
        private UserProfileViewModel? userProfileViewModel;
        private UserProfileRedactorViewModel? userProfileRedactorViewModel;
        private IUserRepository userRepository;
        private IUserAuthorizationService userAuthorizationService;

        public ProfileController(IUserRepository _userRepository, IUserAuthorizationService _userAuthorizationService)
        {
            userRepository = _userRepository;
            userAuthorizationService = _userAuthorizationService;
        }
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View(GetUserProfileViewModel());
        }

        [Authorize]
        [HttpPost]
        public IActionResult GetView(string viewName)
        {
            switch (viewName)
            {
                case "Index":
                    return PartialView(viewName, GetUserProfileViewModel());
                case "Personal":
                    return PartialView(viewName, GetUserProfileRedactorViewModel());
                    
                default:
                    return null;

            }
        }

        [Authorize]
        [HttpPost]
        public async Task SavePersonal(UserProfileRedactorViewModel user)
        {
            var userFromClaims = HttpContext.User;
            var User = await userRepository.Get(new Guid(userFromClaims.FindFirst("Id")?.Value));
            User.Name = user.Name;
            User.Surname = user.Surname;
            User.Age = user.Age;

            await userRepository.Update(User);
            await userAuthorizationService.Update(User, HttpContext);

            userProfileViewModel = null;

            Redirect("/Profile/Index");
        }

        private UserProfileViewModel GetUserProfileViewModel()
        {
            var User = HttpContext.User;
            if (userProfileViewModel is null)
            {
                userProfileViewModel = new UserProfileViewModel();
                userProfileViewModel.ImageUrl = User.FindFirst("ProfileImageUrl")?.Value;
                userProfileViewModel.Name = User.FindFirst("Name")?.Value;
                userProfileViewModel.Surname = User.FindFirst("Surname")?.Value;
            }
            return userProfileViewModel;
        }

        private UserProfileRedactorViewModel GetUserProfileRedactorViewModel()
        {
            if(userProfileRedactorViewModel is not null)
            {
                return userProfileRedactorViewModel;
            }

            var userId = new Guid(HttpContext.User.FindFirst("Id")?.Value);

            var user = userRepository.Get(userId).Result;
            userProfileRedactorViewModel = new UserProfileRedactorViewModel()
            {
                Name = user.Name,
                Surname = user.Surname,
                Age = user.Age
            };
            return userProfileRedactorViewModel;

        }
    }
}
