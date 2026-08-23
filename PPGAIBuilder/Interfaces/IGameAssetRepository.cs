using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PPGAIBuilder.Models;

namespace PPGAIBuilder.Interfaces
{
    public interface IGameAssetRepository
    {
        Task<GameAsset?> GetAssetAsync(string assetId);
        Task<List<GameAsset>> SearchAssetsAsync(string category, string query);
        Task<List<GameAsset>> GetAssetsByCategoryAsync(string category);
        Task AddAssetAsync(GameAsset asset);
        Task UpdateAssetAsync(GameAsset asset);
        Task<int> GetTotalAssetsAsync();
    }
}
