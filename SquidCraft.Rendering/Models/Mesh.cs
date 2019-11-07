using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SquidCraft.Rendering.Buffers;
using SquidCraft.Rendering.Shaders;

namespace SquidCraft.Rendering.Models
{
    public class Mesh : IDisposable
    {
        private readonly Vertex[] _vertices;
        private readonly int[] _indices;

        private readonly VertexArrayObject _vertexArrayObject;
        private readonly BufferObject _elementBufferObject;
        private readonly BufferObject _vertexBufferObject;

        private Shader _shader;

        public Mesh(Vertex[] vertices, int[] indices)
        {
            _vertices = vertices;
            _indices = indices;

            _vertexArrayObject = new VertexArrayObject();
            _elementBufferObject = new BufferObject(BufferTarget.ElementArrayBuffer);
            _vertexBufferObject = new BufferObject(BufferTarget.ArrayBuffer);
            _shader = null;
        }

        public void Prepare(Shader shader)
        {
            _shader = shader;

            _vertexArrayObject.Bind();

            _vertexBufferObject.Bind();
            _vertexBufferObject.Data(_vertices.Length * Vertex.SizeInBytes, _vertices);

            _elementBufferObject.Bind();
            _elementBufferObject.Data(_indices.Length * sizeof(int), _indices);

            var positionParam = shader.Param("in_position");
            GL.EnableVertexAttribArray(positionParam);
            GL.VertexAttribPointer(positionParam, 3, VertexAttribPointerType.Float, false,  Vertex.SizeInBytes, 0);

            var colorParam = shader.Param("in_color");
            GL.EnableVertexAttribArray(colorParam);
            GL.VertexAttribPointer(colorParam, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, Vector3.SizeInBytes);

            var normalParam = shader.Param("in_normal");
            GL.EnableVertexAttribArray(normalParam);
            GL.VertexAttribPointer(normalParam, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 2 * Vector3.SizeInBytes);

            var uvParam = shader.Param("in_uv");
            GL.EnableVertexAttribArray(uvParam);
            GL.VertexAttribPointer(uvParam, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 3 * Vector3.SizeInBytes);

            _vertexBufferObject.Unbind();
            _vertexArrayObject.Unbind();
        }

        public void Render()
        {
            _shader.Use();
            _vertexArrayObject.Bind();
            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        public void Dispose()
        {
            _vertexArrayObject.Dispose();
            _elementBufferObject.Dispose();
            _vertexBufferObject.Dispose();
        }
    }
}