using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace SquidCraft.Audio
{
    public class AudioClip
    {
        public int Handle { get; }
        public bool Streamed { get; } = false;

        private readonly ALFormat _format;
        private readonly int _sampleRate;

        protected internal AudioClip(ALFormat format, int sampleRate)
        {
            Handle = AL.GenBuffer();

            var error = AL.GetError();
            if (error != ALError.NoError)
                throw new AudioException("Unable to generate a buffer: " + error);

            _format = format;
            _sampleRate = sampleRate;
        }

        public void SetData(short[] buffer)
        {
            AL.BufferData(Handle, _format, buffer, sizeof(short) * buffer.Length, _sampleRate);
        }
    }
}