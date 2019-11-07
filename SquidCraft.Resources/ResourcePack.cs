using System;
using SquidCraft.Text;

namespace SquidCraft.Resources
{
    public class ResourcePack
    {
        public string Name { get; }
        public PackData Data { get; }

        public ResourcePack(string name)
        {
            Name = name;
        }

        [Serializable]
        public struct PackData
        {
            public int PackFormat;
            public TextComponent Description;
        }
    }
}