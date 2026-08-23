using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PPGAIBuilder.Models;

namespace PPGAIBuilder.Interfaces
{
    public interface IResearchService
    {
        Task<List<ResearchResult>> ResearchAsync(string query);
        Task<ResearchResult?> GetCachedResultAsync(string query);
        Task CacheResultAsync(ResearchResult result);
        Task ClearCacheAsync();
    }
}
