using System.Collections.Generic;
using SquidCraft.API.Blocks;
using SquidCraft.API.Blocks.Properties;
using SquidCraft.API.Utils;

namespace SquidCraft.API.Registries
{
    public interface IBlockRegistry
    {
        byte BitsPerBlock { get; }

        void Register(Identifier name, IReadOnlyList<IBlockProperty> properties, IReadOnlyList<dynamic> defaultValues);
        IBlockState CreateState(Identifier name, Dictionary<string, string> properties = null);

        IBlockState this[int id] { get; }
        IBlock this[Identifier name] { get; }
    }
}