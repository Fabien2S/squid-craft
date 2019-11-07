using SquidCraft.API.Buffers;

namespace SquidCraft.API.Blocks.Palette
{
    public interface IBlockPalette : IByteBufferSerializable
    {
        byte BitsPerBlock { get; }

        int GetId(IBlockState blockState);
        IBlockState GetBlockState(int id);
    }
}