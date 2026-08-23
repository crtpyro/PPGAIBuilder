using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PPGAIBuilder.Interfaces;
using PPGAIBuilder.Models;

namespace PPGAIBuilder.Services
{
    public class MockResearchService : IResearchService
    {
        private readonly Dictionary<string, ResearchResult> _cache = new();

        public Task<List<ResearchResult>> ResearchAsync(string query)
        {
            var results = new List<ResearchResult>
            {
                new ResearchResult
                {
                    Title = "V6 Engine Mechanics",
                    Source = "Mock Database",
                    Summary = "Information about V6 engine construction and mechanics.",
                    Content = "A V6 engine is a 6-cylinder internal combustion engine with cylinders arranged in a V shape.",
                    ReliabilityScore = 0.8,
                    IsMock = true
                },
                new ResearchResult
                {
                    Title = "Crankshaft Design",
                    Source = "Mock Database",
                    Summary = "Details about crankshaft design and manufacturing.",
                    Content = "The crankshaft converts linear motion from pistons into rotational motion.",
                    ReliabilityScore = 0.75,
                    IsMock = true
                }
            };

            _cache[query] = results[0];
            return Task.FromResult(results);
        }

        public Task<ResearchResult?> GetCachedResultAsync(string query)
        {
            _cache.TryGetValue(query, out var result);
            return Task.FromResult(result);
        }

        public Task CacheResultAsync(ResearchResult result)
        {
            _cache[result.Title] = result;
            return Task.CompletedTask;
        }

        public Task ClearCacheAsync()
        {
            _cache.Clear();
            return Task.CompletedTask;
        }
    }
}
