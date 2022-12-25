using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebShope.DAL.AbstactClasses;
using WebShope.DAL.Interfaces;
using WebShope.Domain.Entityes;

namespace WebShope.DAL.Repository
{
    public class UserRepository: BaseCacheRepository<User>, IUserRepository
    {
        private ApplicationDbContext DbContext = null!;
        private IMemoryCache cache;
        public UserRepository(ApplicationDbContext context, IMemoryCache _cache)
        {
            DbContext = context;
            cache = _cache;
        }
        public async Task<bool> Create(User entity)
        {
            cache.TryGetValue(entity.Id, out User? user);

            if (user is null && await DbContext.Users.FirstOrDefaultAsync(x => x.Login == entity.Login) is null)
            {
                await DbContext.Users.AddAsync(entity);
                await DbContext.SaveChangesAsync();
                AddCache(entity);

                return true;
            }
            return false;
            
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
            AddCache(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<User?> Get(Guid id)
        {
            cache.TryGetValue(id, out User? entity);
            if(entity is null)
            {
                var user = await DbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
                if(user is not null)
                {
                    AddCache(user);
                    return user;
                }
                return null;
            }
            return entity;
        }

        public async Task<User?> GetByLoginAndPassword(string login, string password)
        {
            return await DbContext.Users.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
        }

        public Task<List<User>> Select()
        {
            return null;
        }

        protected override void AddCache(User user)
        {
            cache.Set(user.Id, user, CacheOptions);
        }
    }
}
