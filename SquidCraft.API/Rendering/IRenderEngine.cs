using System;
using SquidCraft.API.Utils;

namespace SquidCraft.API.Rendering
{
    public interface IRenderEngine : IUpdatable, IRenderable, IDisposable
    {
        bool Wireframe { get; set; }

        void Init();
    }
}