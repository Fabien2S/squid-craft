using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace SquidCraft.Rendering.Textures
{
    public abstract class Texture : IDisposable
    {
        public int Handle { get; }

        private readonly TextureTarget _target;

        protected Texture(TextureTarget target)
        {
            Handle = GL.GenTexture();
            _target = target;
        }

        public abstract void Data(int width, int height, byte[,,] data);
        protected abstract void Data(BitmapData data);

        public void Data(Bitmap image)
        {
            var imageSize = image.Size;
            var imageRect = new Rectangle(Point.Empty, imageSize);
            var data = image.LockBits(imageRect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Data(data);
            image.UnlockBits(data);
        }

        public void Param<T>(TextureParameterName parameterName, T value) where T : Enum
        {
            GL.TextureParameter(Handle, parameterName, Convert.ToInt32(value));
        }

        public void Param(TextureParameterName parameterName, int value)
        {
            GL.TextureParameter(Handle, parameterName, value);
        }

        public void Bind()
        {
            GL.BindTexture(_target, Handle);
        }

        public void Unbind()
        {
            GL.BindTexture(_target, 0);
        }

        public void Dispose()
        {
            GL.DeleteTexture(Handle);
        }
    }
}