using DotNetty.Buffers;
using SquidCraft.API.Blocks;
using SquidCraft.API.Blocks.Palette;
using SquidCraft.API.Registries;

namespace SquidCraft.Blocks.Palette
{
    public class GlobalBlockPalette : IBlockPalette
    {
        public byte BitsPerBlock { get; }

        private readonly IBlockRegistry _blockRegistry;

        public GlobalBlockPalette(IBlockRegistry blockRegistry)
        {
            _blockRegistry = blockRegistry;
            BitsPerBlock = blockRegistry.BitsPerBlock;
        }

        public int GetId(IBlockState blockState)
        {
            return blockState.Id;
        }

        public IBlockState GetBlockState(int id)
        {
            return _blockRegistry[id];
        }

        public void Serialize(IByteBuffer buffer)
        {
        }
    }
}