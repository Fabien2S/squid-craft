using OpenTK;
using SquidCraft.API.Audio;
using SquidCraft.API.Math;

namespace SquidCraft.API.Rendering
{
    public interface ICamera : IAudioListener
    {
        Matrix4 ProjectionMatrix { get; }
        new Vector3 Position { get; set; }
        Rotation Rotation { get; set; }
        float AspectRatio { get; set; }
        float ZNear { get; set; }
        float ZFar { get; set; }
        Matrix4 ViewMatrix { get; }
        MagicalFloat FieldOfView { get; }
    }
}