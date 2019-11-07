﻿using DotNetty.Buffers;

 namespace SquidCraft.API.Buffers
{
    public interface IByteBufferSerializable
    {
        void Serialize(IByteBuffer buffer);
    }
}