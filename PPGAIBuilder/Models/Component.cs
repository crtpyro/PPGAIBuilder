using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PPGAIBuilder.Models
{
    public class Component
    {
        [JsonProperty("id")]
        public string ComponentId { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("name")]
        public string Name { get; set; } = "Component";

        [JsonProperty("category")]
        public string Category { get; set; } = "Generic";

        [JsonProperty("description")]
        public string Description { get; set; } = "";

        [JsonProperty("position")]
        public Vector3 Position { get; set; } = new Vector3(0, 0, 0);

        [JsonProperty("rotation")]
        public Vector3 Rotation { get; set; } = new Vector3(0, 0, 0);

        [JsonProperty("scale")]
        public Vector3 Scale { get; set; } = new Vector3(1, 1, 1);

        [JsonProperty("color")]
        public string Color { get; set; } = "#888888";

        [JsonProperty("connections")]
        public List<string> Connections { get; set; } = new List<string>();

        [JsonProperty("parentId")]
        public string? ParentId { get; set; }

        [JsonProperty("visible")]
        public bool Visible { get; set; } = true;

        [JsonProperty("assetType")]
        public string AssetType { get; set; } = "Primitive"; // Primitive, Placeholder, GameAsset
    }

    public class Vector3
    {
        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("z")]
        public double Z { get; set; }

        public Vector3(double x = 0, double y = 0, double z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
