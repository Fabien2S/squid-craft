using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace SquidCraft.Audio
{
    public class AudioSource
    {
        /// <inheritdoc cref="ALSourcef.Gain"/>
        public float Gain
        {
            get
            {
                AL.GetSource(_handle, ALSourcef.Gain, out var value);
                return value;
            }
            set => AL.Source(_handle, ALSourcef.Gain, value);
        }

        /// <inheritdoc cref="ALSourcef.Pitch"/>
        public float Pitch
        {
            get
            {
                AL.GetSource(_handle, ALSourcef.Pitch, out var value);
                return value;
            }
            set => AL.Source(_handle, ALSourcef.Pitch, value);
        }

        /// <inheritdoc cref="ALSourceb.Looping"/>
        public bool Looping
        {
            get
            {
                AL.GetSource(_handle, ALSourceb.Looping, out var value);
                return value;
            }
            set => AL.Source(_handle, ALSourceb.Looping, value);
        }

        public AudioClip Clip
        {
            get => _clip;
            set
            {
                _clip = value;
                AL.Source(_handle, ALSourcei.Buffer, value.Handle);
            }
        }

        private readonly int _handle;

        private AudioClip _clip;

        public AudioSource()
        {
            _handle = AL.GenSource();

            var error = AL.GetError();
            if (error != ALError.NoError)
                throw new AudioException("Unable to generate a buffer: " + error);
        }

        public void Play()
        {
            AL.SourcePlay(_handle);
        }

        public void Stop()
        {
            AL.SourceStop(_handle);
        }
    }
}