using System.Runtime.InteropServices;
using OpenTK;

namespace SquidCraft.Rendering.Models
{
    public struct Vertex
    {
        public static readonly int SizeInBytes = Marshal.SizeOf(new Vertex());

        public readonly Vector3 Position;
        public readonly Vector3 Color;
        public readonly Vector3 Normal;
        public readonly Vector3 Uv;

        public Vertex(Vector3 position, Vector3 color, Vector3 normal)
        {
            Position = position;
            Color = color;
            Normal = normal;
            Uv = position;
        }

        public Vertex(Vector3 position, Vector3 color, Vector3 normal, Vector2 uv)
        {
            Position = position;
            Color = color;
            Normal = normal;
            Uv = new Vector3(uv);
        }

        public Vertex(Vector3 position, Vector3 color, Vector3 normal, Vector3 uv)
        {
            Position = position;
            Color = color;
            Normal = normal;
            Uv = uv;
        }
    }
}