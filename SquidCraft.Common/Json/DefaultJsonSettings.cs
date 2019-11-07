using System.Text.Json;

namespace SquidCraft.Json
{
    public static class DefaultJsonSettings
    {
        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = UnderscoreNamingPolicy.Instance,
            DictionaryKeyPolicy = UnderscoreNamingPolicy.Instance
        };
    }
}