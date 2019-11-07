using SquidCraft.API.Utils;

namespace SquidCraft
{
    public static partial class Minecraft
    {
        public static class Inputs
        {
            public static Identifier DebugToggleWireFrame = Minecraft.CreateIdentifier("key.debug.toggle_wire_frame");
            
            public static Identifier Attack = CreateIdentifier("key_key.attack");
            public static Identifier Use = CreateIdentifier("key_key.use");

            public static Identifier LookX = CreateIdentifier("key_key.look_x");
            public static Identifier LookY = CreateIdentifier("key_key.look_y");

            public static Identifier Forward = CreateIdentifier("key_key.forward");
            public static Identifier Left = CreateIdentifier("key_key.left");
            public static Identifier Back = CreateIdentifier("key_key.back");
            public static Identifier Right = CreateIdentifier("key_key.right");

            public static Identifier Jump = CreateIdentifier("key_key.jump");
            public static Identifier Sneak = CreateIdentifier("key_key.sneak");
        }
    }
}