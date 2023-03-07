using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShope.Domain.Entityes;

namespace WebShope.DAL.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<Category?> GetById(int id);
        public List<Category> GetAll();
    }
}
