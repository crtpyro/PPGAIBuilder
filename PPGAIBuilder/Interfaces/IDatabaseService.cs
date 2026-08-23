using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PPGAIBuilder.Models;

namespace PPGAIBuilder.Interfaces
{
    public interface IDatabaseService
    {
        Task InitializeAsync();
        Task<T?> GetAsync<T>(string id) where T : class;
        Task<List<T>> GetAllAsync<T>() where T : class;
        Task SaveAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(string id) where T : class;
        Task<int> ExecuteAsync(string query, params object[] parameters);
    }
}
