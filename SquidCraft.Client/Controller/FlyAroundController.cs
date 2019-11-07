using OpenTK;
using SquidCraft.API.Math;
using SquidCraft.API.Rendering;
using SquidCraft.API.Utils;
using SquidCraft.Input;

namespace SquidCraft.Client.Controller
{
    public class FlyAroundController : PlayerController
    {
        private static readonly Vector3 WorldUp = Vector3.UnitY;
        
        private readonly ICamera _camera;
        private readonly InputManager _inputManager;

        private readonly float _speed = 4f;
        private readonly float _sensitivity = 2.5f;

        public FlyAroundController(ICamera camera, InputManager inputManager)
        {
            _camera = camera;
            _inputManager = inputManager;
        }

        public override void Update(float deltaTime)
        {
            var translation = Vector3.Zero;

            var forward = ReadAxis(Minecraft.Inputs.Forward, Minecraft.Inputs.Back);
            translation += _camera.Forward * forward;
            
            var right = ReadAxis(Minecraft.Inputs.Right, Minecraft.Inputs.Left);
            translation += _camera.Right * right;
            
            var up = ReadAxis(Minecraft.Inputs.Jump, Minecraft.Inputs.Sneak);
            translation += WorldUp * up;

            _camera.Position += translation * (_speed * deltaTime);

            var lookX = _inputManager[Minecraft.Inputs.LookX];
            var lookY = _inputManager[Minecraft.Inputs.LookY];
            
            var cameraRotation = _camera.Rotation;
            var cameraSpeed = _sensitivity * deltaTime;
            _camera.Rotation = new Rotation(
                cameraRotation.Yaw - lookX.Value * cameraSpeed,
                System.Math.Clamp(cameraRotation.Pitch + lookY.Value * cameraSpeed, -89.99f, 89.99f)
            );
        }

        private float ReadAxis(Identifier pos, Identifier neg)
        {
            var positiveBinding = _inputManager[pos];
            var negativeBinding = _inputManager[neg];
            return positiveBinding.Value + -negativeBinding.Value;
        }
    }
}