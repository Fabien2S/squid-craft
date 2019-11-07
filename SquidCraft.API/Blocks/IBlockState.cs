using System;
using System.Collections.Generic;

namespace SquidCraft.API.Blocks
{
    public interface IBlockState : IEquatable<IBlockState>
    {
        int Id { get; }
        IBlock Block { get; }
        IReadOnlyList<dynamic> Properties { get; }
    }
}