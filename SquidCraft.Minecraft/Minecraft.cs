using SquidCraft.API.Utils;

namespace SquidCraft
{
    public static partial class Minecraft
    {
        private const string Namespace = "minecraft";

        public static Identifier CreateIdentifier(string key)
        {
            return new Identifier(Namespace, key);
        }
    }
}