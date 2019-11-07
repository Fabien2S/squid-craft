using OpenTK.Graphics.OpenGL;
using SquidCraft.API.Utils;
using SquidCraft.Rendering.Textures;

namespace SquidCraft.Rendering
{
    public static class Fallback
    {
        public static readonly Identifier Name = Minecraft.CreateIdentifier("missing");

        public static Texture2D MissingTexture { get; private set; }

        public static void Init()
        {
            MissingTexture = new Texture2D();
            MissingTexture.Bind();

            MissingTexture.Param(TextureParameterName.TextureMagFilter, TextureMagFilter.Nearest);
            MissingTexture.Param(TextureParameterName.TextureMinFilter, TextureMinFilter.Nearest);
            MissingTexture.Param(TextureParameterName.TextureWrapS, TextureWrapMode.ClampToEdge);
            MissingTexture.Param(TextureParameterName.TextureWrapT, TextureWrapMode.ClampToEdge);

            const int width = 16;
            const int height = 16;

            var pixels = new byte[height, width, 3];
            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++)
            {
                var isMagenta = x < 8 ^ y < 8;
                pixels[y, x, 0] = isMagenta ? byte.MaxValue : byte.MinValue;
                pixels[y, x, 1] = 0;
                pixels[y, x, 2] = isMagenta ? byte.MaxValue : byte.MinValue;
            }

            MissingTexture.Data(width, height, pixels);
        }
    }
}