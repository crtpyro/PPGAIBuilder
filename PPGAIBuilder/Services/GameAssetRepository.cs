using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PPGAIBuilder.Interfaces;
using PPGAIBuilder.Models;

namespace PPGAIBuilder.Services
{
    public class GameAssetRepository : IGameAssetRepository
    {
        private readonly IDatabaseService _db;
        private readonly List<GameAsset> _seedAssets;

        public GameAssetRepository(IDatabaseService db)
        {
            _db = db;
            _seedAssets = new List<GameAsset>
            {
                new GameAsset
                {
                    AssetId = "asset_crankshaft",
                    DisplayName = "Crankshaft",
                    Category = "Engine",
                    Source = "Placeholder",
                    Description = "Standard engine crankshaft component."
                },
                new GameAsset
                {
                    AssetId = "asset_piston",
                    DisplayName = "Piston",
                    Category = "Engine",
                    Source = "Placeholder",
                    Description = "Engine piston component."
                },
                new GameAsset
                {
                    AssetId = "asset_cylinder",
                    DisplayName = "Cylinder",
                    Category = "Engine",
                    Source = "Placeholder",
                    Description = "Engine cylinder block."
                }
            };
        }

        public async Task<GameAsset?> GetAssetAsync(string assetId)
        {
            return await _db.GetAsync<GameAsset>(assetId);
        }

        public async Task<List<GameAsset>> SearchAssetsAsync(string category, string query)
        {
            var allAssets = await _db.GetAllAsync<GameAsset>();
            var filtered = new List<GameAsset>();

            foreach (var asset in allAssets)
            {
                if ((string.IsNullOrEmpty(category) || asset.Category.Contains(category, StringComparison.OrdinalIgnoreCase)) &&
                    (string.IsNullOrEmpty(query) || asset.DisplayName.Contains(query, StringComparison.OrdinalIgnoreCase)))
                {
                    filtered.Add(asset);
                }
            }

            return filtered;
        }

        public async Task<List<GameAsset>> GetAssetsByCategoryAsync(string category)
        {
            return await SearchAssetsAsync(category, "");
        }

        public async Task AddAssetAsync(GameAsset asset)
        {
            await _db.SaveAsync(asset);
        }

        public async Task UpdateAssetAsync(GameAsset asset)
        {
            await _db.SaveAsync(asset);
        }

        public async Task<int> GetTotalAssetsAsync()
        {
            var assets = await _db.GetAllAsync<GameAsset>();
            return assets.Count;
        }

        public async Task InitializeSeedAssetsAsync()
        {
            foreach (var asset in _seedAssets)
            {
                var existing = await GetAssetAsync(asset.AssetId);
                if (existing == null)
                {
                    await AddAssetAsync(asset);
                }
            }
        }
    }
}
