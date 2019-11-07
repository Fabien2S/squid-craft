using System;

namespace SquidCraft.MoLang
{
    [Flags]
    public enum MoLangAccess
    {
        Read = 0b01,
        Write = 0b10
    }
}