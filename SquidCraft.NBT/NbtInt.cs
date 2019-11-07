using System.IO;

namespace SquidCraft.NBT
{
    public class NbtInt : NbtTag<int>
    {
        public override byte Id { get; } = 3;

        public NbtInt(int value = 0) : base(value)
        {
        }

        public override void Serialize(BinaryWriter writer)
        {
            writer.Write(Value);
        }
    }
}