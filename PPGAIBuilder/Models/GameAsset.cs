using System;
using Newtonsoft.Json;

namespace PPGAIBuilder.Models
{
    public class GameAsset
    {
        [JsonProperty("assetId")]
        public string AssetId { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("displayName")]
        public string DisplayName { get; set; } = "";

        [JsonProperty("category")]
        public string Category { get; set; } = "Generic";

        [JsonProperty("source")]
        public string Source { get; set; } = "Unknown";

        [JsonProperty("previewImage")]
        public string? PreviewImage { get; set; }

        [JsonProperty("modelPath")]
        public string? ModelPath { get; set; }

        [JsonProperty("metadata")]
        public string? Metadata { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; } = "";
    }
}
