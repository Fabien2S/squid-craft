using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using SquidCraft.API.Assets;
using SquidCraft.API.Audio;

namespace SquidCraft.Audio
{
    public class AudioManager : IAudioManager
    {
        public IAudioListener Listener { get; set; }

        private readonly AudioContext _context;

        public AudioManager()
        {
            AssetLoader.Register<AudioClipLoader, AudioClip>();

            _context = new AudioContext();
        }

        public void Update(float deltaTime)
        {
            var position = Listener.Position;
            var front = Listener.Forward;
            var up = Listener.Up;
            AL.Listener(ALListener3f.Position, ref position);
            AL.Listener(ALListenerfv.Orientation, ref front, ref up);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}