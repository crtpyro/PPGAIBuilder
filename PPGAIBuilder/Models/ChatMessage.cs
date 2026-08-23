using System;
using Newtonsoft.Json;

namespace PPGAIBuilder.Models
{
    public class ChatMessage
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("projectId")]
        public string ProjectId { get; set; } = "";

        [JsonProperty("content")]
        public string Content { get; set; } = "";

        [JsonProperty("role")]
        public string Role { get; set; } = "user"; // user or assistant

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [JsonProperty("relatedProjectId")]
        public string? RelatedProjectId { get; set; }
    }
}
