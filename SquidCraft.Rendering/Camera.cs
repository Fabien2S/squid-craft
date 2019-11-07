using OpenTK;
using SquidCraft.API.Math;
using SquidCraft.API.Rendering;
using MathHelper = SquidCraft.Math.MathHelper;

namespace SquidCraft.Rendering
{
    public class Camera : ICamera
    {
        public Vector3 Position
        {
            get => _position;
            set
            {
                _position = value;
                UpdateViewMatrix();
            }
        }

        public Rotation Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                UpdateTransform();
            }
        }

        public Vector3 Forward { get; private set; } = Vector3.UnitZ;
        public Vector3 Right { get; private set; } = Vector3.UnitX;
        public Vector3 Up { get; private set; } = Vector3.UnitY;

        public float AspectRatio { get; set; }
        public float ZNear { get; set; } = .01f;
        public float ZFar { get; set; } = 100f;
        
        public MagicalFloat FieldOfView { get; } = new MagicalFloat(70 * MathHelper.DegreesToRadians);

        public Matrix4 ProjectionMatrix => Matrix4.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, ZNear, ZFar);
        public Matrix4 ViewMatrix { get; private set; }
        
        private Vector3 _position;
        private Rotation _rotation;

        public Camera(Vector3 position, Rotation rotation)
        {
            _position = position;
            _rotation = rotation;

            UpdateTransform();
        }

        private void UpdateTransform()
        {
            Forward = Rotation.Direction;
            Right = Vector3.Normalize(Vector3.Cross(Forward, Vector3.UnitY));
            Up = Vector3.Normalize(Vector3.Cross(Right, Forward));

            UpdateViewMatrix();
        }

        private void UpdateViewMatrix()
        {
            ViewMatrix = Matrix4.LookAt(_position, _position + Forward, Up);
        }
    }
}