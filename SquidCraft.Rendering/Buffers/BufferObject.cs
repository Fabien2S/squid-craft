using System;
using OpenTK.Graphics.OpenGL;

namespace SquidCraft.Rendering.Buffers
{
    public class BufferObject : IDisposable
    {
        private readonly int _handle;
        private readonly BufferTarget _target;

        public BufferObject(BufferTarget target)
        {
            _handle = GL.GenBuffer();
            _target = target;
        }

        public void Bind()
        {
            GL.BindBuffer(_target, _handle);
        }

        public void Unbind()
        {
            GL.BindBuffer(_target, 0);
        }

        public void Data<T>(int size, T[] data, BufferUsageHint usage = BufferUsageHint.StaticDraw) where T : struct
        {
            GL.BufferData(_target, size, data, usage);
        }

        public void Dispose()
        {
            GL.BindBuffer(_target, 0);
            GL.DeleteBuffer(_handle);
        }
    }
}