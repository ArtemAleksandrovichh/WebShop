using Microsoft.AspNetCore.Mvc;
using WebShope.Domain.Entityes;
using WebShope.Domain.Models;
using WebShope.Domain.Static;
using WebShope.Extensions;

namespace WebShope.Controllers
{
    public class BucketController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
			if(HttpContext.Session.Get<int?>(Constans.SESSIONCOUNT) is not null)
			{
				return View(HttpContext.Session.Get<List<BacketViewModel>>(Constans.BACKETLIST));
			}
			else
			{
				var backetViewModel = new List<BacketViewModel>();
				HttpContext.Session.Set<int?>(Constans.SESSIONCOUNT, 0);
				HttpContext.Session.Set(Constans.BACKETLIST, backetViewModel);
				return View(backetViewModel);

			}
        }

		[HttpPost]
		public IActionResult RemoveItemFromBucket(int id)
		{
			var bucketList = HttpContext.Session.Get<List<BacketViewModel>>(Constans.BACKETLIST);
			var removeProduct = bucketList.Single(x => x.Id == id);
			bucketList.Remove(removeProduct);
			HttpContext.Session.Set(Constans.BACKETLIST, bucketList);
			HttpContext.Session.Set(Constans.SESSIONCOUNT, bucketList.Count);
			return View("Index", bucketList);
		}

		[HttpPost]
		public JsonResult PutItemInToBucket(Product product)
		{

			List<BacketViewModel> BacketList;
			if (HttpContext.Session.Get<int?>(Constans.SESSIONCOUNT) != null)
			{
				BacketList = HttpContext.Session.Get<List<BacketViewModel>>(Constans.BACKETLIST);
				var backetItem = BacketList?.FirstOrDefault(s => s.Id == product.Id);

				if (backetItem != null)
				{
					backetItem.Amount += 1;
					backetItem = BacketList?.FirstOrDefault(s => s.Id == product.Id);
					HttpContext.Session.Set(Constans.BACKETLIST, BacketList);
					return Json(new { BacketList.Count });
				}
			}
			else
			{
				BacketList = new List<BacketViewModel>();
			}

			BacketViewModel productInBacket = new BacketViewModel()
			{
				Id = product.Id,
				Name = product.Name,
				Amount = 1,
				ImageUrl = product.ImageUrl,
				Price = product.Price
			};

			BacketList.Add(productInBacket);
			HttpContext.Session.Set(Constans.BACKETLIST, BacketList);
			HttpContext.Session.Set<int?>(Constans.SESSIONCOUNT, BacketList.Count);


			return Json(new { BacketList.Count });
		}
	}
}
