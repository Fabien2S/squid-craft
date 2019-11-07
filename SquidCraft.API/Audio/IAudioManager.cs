using System;
using SquidCraft.API.Utils;

namespace SquidCraft.API.Audio
{
    public interface IAudioManager : IUpdatable, IDisposable
    {
        IAudioListener Listener { get; set; }
    }
}