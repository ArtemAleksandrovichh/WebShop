using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WebShope.DAL.Interfaces;
using WebShope.DAL.Repository;
using WebShope.Domain.Entityes;
using WebShope.Domain.Models;
using WebShope.Domain.Static;
using WebShope.Extensions;

namespace WebShope.Controllers
{
    public class CatalogController : Controller
    {
        ICategoryRepository _category;
        public IProductRepository ProductRepository { get; set; }
        public CatalogController(ICategoryRepository category, IProductRepository productRepository)
        {
            _category = category;
            ProductRepository = productRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View(_category.GetAll());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Index(int categoryId)
        {
           var listProducts = await ProductRepository.GetProductsByCategoryId(categoryId);
           return View("Products", listProducts);

        }

       
    }
}
