using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebShope.DAL.Interfaces;
using WebShope.Domain.Entityes;

namespace WebShope.DAL.Repository
{
    public class UserRepository: IUserRepository
    {
        private ApplicationDbContext DbContext = null!;
        public UserRepository(ApplicationDbContext context)
        {
            DbContext = context;
        }
        public async Task<bool> Create(User entity)
        {
            if(await DbContext.Users.FirstOrDefaultAsync(x => x.Login == entity.Login) is null)
            {
                await DbContext.Users.AddAsync(entity);
                await DbContext.SaveChangesAsync();
                return true;
            }
            else return false;
        }
        public async Task<bool> Delete(User entity)
        {
            DbContext.Users.Remove(entity);
            await DbContext.SaveChangesAsync();
            return true;
        }
        public async Task<User> Update(User entity)
        {
            DbContext.Users.Update(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<User> Get(int id)
        {
           return await DbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> GetByLoginAndPassword(string login, string password)
        {
            if (await DbContext.Users.FirstOrDefaultAsync(x => x.Login == login && x.Password == password) is not null)
            {
                return true;
            }
            else return false;

        }
    }
}
