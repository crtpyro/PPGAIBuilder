using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PPGAIBuilder.Interfaces;

namespace PPGAIBuilder.Services
{
    public class CacheService : ICacheService
    {
        private class CacheEntry
        {
            public object Value { get; set; }
            public DateTime? ExpiresAt { get; set; }
        }

        private readonly Dictionary<string, CacheEntry> _cache = new();

        public Task<T?> GetAsync<T>(string key) where T : class
        {
            if (_cache.TryGetValue(key, out var entry))
            {
                if (entry.ExpiresAt != null && DateTime.UtcNow > entry.ExpiresAt)
                {
                    _cache.Remove(key);
                    return Task.FromResult<T?>(null);
                }
                return Task.FromResult(entry.Value as T);
            }
            return Task.FromResult<T?>(null);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class
        {
            _cache[key] = new CacheEntry
            {
                Value = value,
                ExpiresAt = expiration.HasValue ? DateTime.UtcNow.Add(expiration.Value) : null
            };
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }

        public Task ClearAsync()
        {
            _cache.Clear();
            return Task.CompletedTask;
        }
    }
}
