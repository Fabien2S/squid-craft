using OpenTK;
using SquidCraft.API.Utils;
using SquidCraft.Math;

namespace SquidCraft.Utils
{
    public static class CoordinateHelper
    {
        public static readonly Vector3 Down = new Vector3(0, -1, 0);
        public static readonly Vector3 Up = new Vector3(0, 1, 0);
        public static readonly Vector3 North = new Vector3(-1, 0, 0);
        public static readonly Vector3 South = new Vector3(1, 0, 0);
        public static readonly Vector3 West = new Vector3(0, 0, -1);
        public static readonly Vector3 East = new Vector3(0, 0, 1);

        private static readonly Vector3[] FaceToDirectionMapping =
        {
            Down, Up, North, South, West, East
        };

        public static Vector3 FaceToDirection(Direction face)
        {
            return FaceToDirectionMapping[(int) face];
        }
    }
}