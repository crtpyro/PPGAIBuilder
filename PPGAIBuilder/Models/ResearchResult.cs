using System;
using Newtonsoft.Json;

namespace PPGAIBuilder.Models
{
    public class ResearchResult
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("title")]
        public string Title { get; set; } = "";

        [JsonProperty("source")]
        public string Source { get; set; } = "";

        [JsonProperty("summary")]
        public string Summary { get; set; } = "";

        [JsonProperty("content")]
        public string Content { get; set; } = "";

        [JsonProperty("retrievedAt")]
        public DateTime RetrievedAt { get; set; } = DateTime.UtcNow;

        [JsonProperty("reliabilityScore")]
        public double ReliabilityScore { get; set; } = 0.5; // 0.0 to 1.0

        [JsonProperty("isMock")]
        public bool IsMock { get; set; } = true;
    }
}
