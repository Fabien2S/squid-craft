using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace SquidCraft.Rendering.Textures
{
    public class Texture2D : Texture
    {
        public Texture2D() : base(TextureTarget.Texture2D)
        {
        }

        public override void Data(int width, int height, byte[,,] data)
        {
            GL.TexImage2D(
                TextureTarget.Texture2D, 0,
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
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                data.Width, data.Height,
                0,
                PixelFormat.Bgra,
                PixelType.UnsignedByte,
                data.Scan0
            );
        }
    }
}