using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShope.DAL.Interfaces;
using WebShope.Domain.Models;

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

        public ProfileController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
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

        private async Task<UserProfileRedactorViewModel> GetUserProfileRedactorViewModel()
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
