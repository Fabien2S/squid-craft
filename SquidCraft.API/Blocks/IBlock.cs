using System;
using System.Collections.Generic;
using SquidCraft.API.Blocks.Properties;
using SquidCraft.API.Utils;

namespace SquidCraft.API.Blocks
{
    public interface IBlock : IEquatable<IBlock>
    {
        int Id { get; }
        Identifier Name { get; }
        IReadOnlyList<IBlockProperty> Properties { get; }
        IReadOnlyList<dynamic> DefaultValues { get; }
    }
}