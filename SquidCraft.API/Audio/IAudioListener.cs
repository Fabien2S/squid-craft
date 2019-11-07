using OpenTK;

namespace SquidCraft.API.Audio
{
    public interface IAudioListener
    {
        Vector3 Position { get; }
        Vector3 Forward { get; }
        Vector3 Right { get; }
        Vector3 Up { get; }
    }
}