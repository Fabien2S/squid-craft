using System.Text.Json;
using SquidCraft.Extensions;

namespace SquidCraft.Json
{
    public class UnderscoreNamingPolicy : JsonNamingPolicy
    {
        public static readonly JsonNamingPolicy Instance = new UnderscoreNamingPolicy();
        
        private UnderscoreNamingPolicy() {}
        
        public override string ConvertName(string name)
        {
            return name.ToUnderscoreCase();
        }
    }
}