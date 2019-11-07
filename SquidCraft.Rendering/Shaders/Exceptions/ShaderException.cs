using System;

namespace SquidCraft.Rendering.Shaders.Exceptions
{
    public class ShaderException : Exception
    {
        protected ShaderException(string message) : base(message)
        {
        }
    }
}