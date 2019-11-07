using OpenTK;

namespace SquidCraft.Rendering.Models.Primitives
{
    public static class Cube
    {
        private static readonly int[] Indices =
        {
            // front
            2, 1, 0,
            0, 3, 2,
            // right
            6, 5, 1,
            1, 2, 6,
            // back
            5, 6, 7,
            7, 4, 5,
            // left
            3, 0, 4,
            4, 7, 3,
            // bottom
            1, 5, 4,
            4, 0, 1,
            // top
            6, 2, 3,
            3, 7, 6
        };

        private static readonly int[] ReverseIndices =
        {
            // front
            0, 1, 2,
            2, 3, 0,
            // right
            1, 5, 6,
            6, 2, 1,
            // back
            7, 6, 5,
            5, 4, 7,
            // left
            4, 0, 3,
            3, 7, 4,
            // bottom
            4, 5, 1,
            1, 0, 4,
            // top
            3, 2, 6,
            6, 7, 3
        };

        private static readonly Vector3[] Positions =
        {
            // front
            new Vector3(-1.0f, -1.0f, 1.0f),
            new Vector3(1.0f, -1.0f, 1.0f),
            new Vector3(1.0f, 1.0f, 1.0f),
            new Vector3(-1.0f, 1.0f, 1.0f),
            // back
            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3(1.0f, -1.0f, -1.0f),
            new Vector3(1.0f, 1.0f, -1.0f),
            new Vector3(-1.0f, 1.0f, -1.0f)
        };

        private static readonly Vector3[] Colors =
        {
            // front colors
            new Vector3(1.0f, 1.0f, 1.0f),
            new Vector3(1.0f, 1.0f, 1.0f),
            new Vector3(1.0f, 1.0f, 1.0f),
            new Vector3(1.0f, 1.0f, 1.0f),
            // back colors
            new Vector3(1.0f, 1.0f, 1.0f),
            new Vector3(1.0f, 1.0f, 1.0f),
            new Vector3(1.0f, 1.0f, 1.0f),
            new Vector3(1.0f, 1.0f, 1.0f),
        };

        public static Mesh Build(bool inverted = false)
        {
            var vertices = new Vertex[8];
            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i] = new Vertex(
                    Positions[i],
                    Colors[i],
                    Vector3.UnitY,
                    Positions[i]
                );
            }

            return new Mesh(
                vertices,
                inverted ? ReverseIndices : Indices
            );
        }
    }
}