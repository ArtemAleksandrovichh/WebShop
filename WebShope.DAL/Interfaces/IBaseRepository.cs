using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShope.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        public Task<bool> Create(T entity);
        public Task<bool> Delete(T entity);
        public Task<T> Update(T entity);
        public Task<T?> Get(Guid id);
        public Task<List<T>> Select();

    }
}
