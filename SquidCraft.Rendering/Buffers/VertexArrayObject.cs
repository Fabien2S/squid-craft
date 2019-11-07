using System;
using OpenTK.Graphics.OpenGL;

namespace SquidCraft.Rendering.Buffers
{
    public class VertexArrayObject : IDisposable
    {
        private readonly int _handle;

        public VertexArrayObject()
        {
            _handle = GL.GenVertexArray();
        }

        public void Bind()
        {
            GL.BindVertexArray(_handle);
        }

        public void Unbind()
        {
            GL.BindVertexArray(0);
        }

        public void Dispose()
        {
            GL.DeleteVertexArray(_handle);
        }
    }
}