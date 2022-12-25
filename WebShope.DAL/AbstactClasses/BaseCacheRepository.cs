using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;

namespace WebShope.DAL.AbstactClasses
{
    public abstract class BaseCacheRepository<T>
    {
        protected virtual MemoryCacheEntryOptions CacheOptions { get; set; } = null!;

        public BaseCacheRepository()
        {
            CacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
                Priority = 0
            };

            var callbackRegistration = new PostEvictionCallbackRegistration();
            callbackRegistration.EvictionCallback = PostEvictionCallback;
            CacheOptions.PostEvictionCallbacks.Add(callbackRegistration);
        }

        protected virtual void PostEvictionCallback(object key, object? value, EvictionReason reason, object? state)
        {
            Console.WriteLine($"запись из кэша устарела");
        }
        protected abstract void AddCache(T entity);
    }
}
