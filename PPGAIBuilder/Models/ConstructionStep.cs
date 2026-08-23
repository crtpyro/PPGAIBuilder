using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PPGAIBuilder.Models
{
    public class ConstructionStep
    {
        [JsonProperty("stepNumber")]
        public int StepNumber { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; } = "";

        [JsonProperty("description")]
        public string Description { get; set; } = "";

        [JsonProperty("componentsUsed")]
        public List<string> ComponentsUsed { get; set; } = new List<string>();

        [JsonProperty("highlightedComponents")]
        public List<string> HighlightedComponents { get; set; } = new List<string>();

        [JsonProperty("targetPosition")]
        public Vector3 TargetPosition { get; set; } = new Vector3(0, 0, 0);

        [JsonProperty("estimatedDuration")]
        public int EstimatedDurationSeconds { get; set; } = 30;

        [JsonProperty("notes")]
        public string Notes { get; set; } = "";

        [JsonProperty("completed")]
        public bool Completed { get; set; } = false;
    }
}
