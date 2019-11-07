using System;

namespace SquidCraft.Assets.Exceptions
{
    public class AssetException : Exception
    {
        protected AssetException(string message) : base(message)
        {
        }
    }
}