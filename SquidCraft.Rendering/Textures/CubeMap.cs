using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace SquidCraft.Rendering.Textures
{
    public class CubeMap : Texture
    {
        private static readonly TextureTarget[] Targets =
        {
            TextureTarget.TextureCubeMapPositiveZ,
            TextureTarget.TextureCubeMapPositiveX,
            TextureTarget.TextureCubeMapNegativeZ,
            TextureTarget.TextureCubeMapNegativeX,
            TextureTarget.TextureCubeMapPositiveY,
            TextureTarget.TextureCubeMapNegativeY
        };

        private TextureIndex _index = TextureIndex.PositiveZ;

        public CubeMap() : base(TextureTarget.TextureCubeMap)
        {
        }

        public void SetIndex(TextureIndex index)
        {
            _index = index;
        }

        public override void Data(int width, int height, byte[,,] data)
        {
            GL.TexImage2D(
                Targets[(int) _index], 0,
                PixelInternalFormat.Rgb8,
                width, height,
                0,
                PixelFormat.Rgb,
                PixelType.UnsignedByte,
                data
            );
        }

        protected override void Data(BitmapData data)
        {
            GL.TexImage2D(
                Targets[(byte) _index],
                0,
                PixelInternalFormat.Rgba,
                data.Width, data.Height,
                0,
                PixelFormat.Bgra,
                PixelType.UnsignedByte,
                data.Scan0
            );
        }

        public enum TextureIndex : byte
        {
            PositiveZ,
            PositiveX,
            NegativeZ,
            NegativeX,
            PositiveY,
            NegativeY,
        }
    }
}