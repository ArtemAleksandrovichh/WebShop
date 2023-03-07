using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShope.DAL.Interfaces;
using WebShope.Domain.Entityes;

namespace WebShope.DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext DbContext = null!;
        private IMemoryCache cache;
        public ProductRepository(ApplicationDbContext applicationDbContext, IMemoryCache _cache)
        {
            DbContext = applicationDbContext;
            cache = _cache;
        }
        public async Task<List<Product>> GetProductsByCategoryId(int categoryId)
        {         
            return await DbContext.Products.Where(x => x.CategoryId == categoryId).ToListAsync(); ;
        }
    }
}
