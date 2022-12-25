using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShope.Domain.Entityes;

namespace WebShope.DAL.Interfaces
{
    public interface IUserRepository: IBaseRepository<User>
    {
        public Task<User?> GetByLoginAndPassword(string login, string password);
    }
}
