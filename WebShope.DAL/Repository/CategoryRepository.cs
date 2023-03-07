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
    public class CategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext DbContext = null!;
        private IMemoryCache cache;
        public CategoryRepository(ApplicationDbContext applicationDbContext, IMemoryCache _cache) {
            DbContext = applicationDbContext;
            cache = _cache;
        }
        public List<Category> GetAll()
        {
            return  DbContext.Categories.ToList();
        }

        public async Task<Category?> GetById(int id)
        {
            var category = await DbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return category;
        }
    }
}
