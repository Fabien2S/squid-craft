using System;
using System.Collections.Generic;
using NVorbis;
using OpenTK.Audio.OpenAL;
using SquidCraft.API.Assets;
using SquidCraft.API.Utils;

namespace SquidCraft.Audio
{
    public class AudioClipLoader : AssetLoader<AudioClip>
    {
        public override AudioClip Load(Identifier name, Uri uri)
        {
            using var reader = new VorbisReader(uri.LocalPath)
            {
                ClipSamples = true
            };

            var channels = reader.Channels;
            var format = channels == 1 ? ALFormat.Mono16 : ALFormat.Stereo16;

            var sampleRate = reader.SampleRate;

            // TODO use stream-able clip for large sounds
            var totalTime = reader.TotalTime;
            var buffer = new float[channels * sampleRate * (int) System.Math.Ceiling(totalTime.TotalSeconds)];

            var count = 0;
            int read;
            do
            {
                read = reader.ReadSamples(buffer, count, buffer.Length - count);
                count += read;
            } while (read > 0);

            var audioClip = new AudioClip(format, sampleRate);
            var data = Convert(buffer);
            audioClip.SetData(data);
            return audioClip;
        }

        private static short[] Convert(IReadOnlyList<float> data)
        {
            var buffer = new short[data.Count];
            for (var i = 0; i < data.Count; i++)
                buffer[i] = (short) (short.MaxValue * data[i]);

            return buffer;
        }
    }
}