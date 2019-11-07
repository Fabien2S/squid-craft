using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SquidCraft.Assets
{
    [Serializable]
    public struct AssetRegistry
    {
        [JsonPropertyName("objects")]
        public Dictionary<string, AssetObject> Objects { get; set; }

        [Serializable]
        public struct AssetObject
        {
            [JsonPropertyName("hash")]
            public string Hash { get; set; }
            [JsonPropertyName("size")]
            public uint Size { get; set; }
        }
    }
}