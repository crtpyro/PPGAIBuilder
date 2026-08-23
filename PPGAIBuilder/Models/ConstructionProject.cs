using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PPGAIBuilder.Models
{
    public class ConstructionProject
    {
        [JsonProperty("projectId")]
        public string ProjectId { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("name")]
        public string Name { get; set; } = "New Project";

        [JsonProperty("description")]
        public string Description { get; set; } = "";

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [JsonProperty("components")]
        public List<Component> Components { get; set; } = new List<Component>();

        [JsonProperty("steps")]
        public List<ConstructionStep> Steps { get; set; } = new List<ConstructionStep>();

        [JsonProperty("currentStep")]
        public int CurrentStep { get; set; } = 0;

        [JsonProperty("thumbnailPath")]
        public string? ThumbnailPath { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; } = new List<string>();

        [JsonProperty("estimatedCompletionTime")]
        public int EstimatedCompletionSeconds { get; set; } = 0;
    }
}
