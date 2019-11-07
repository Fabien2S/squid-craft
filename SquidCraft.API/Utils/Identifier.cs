using System;

namespace SquidCraft.API.Utils
{
    public struct Identifier : IEquatable<Identifier>, IComparable<Identifier>
    {
        private const char Separator = ':';

        public string Namespace { get; }
        public string Key { get; }

        [NonSerialized] private readonly int _hash;

        public Identifier(string @namespace, string key)
        {
            Namespace = @namespace;
            Key = key;
            _hash = (@namespace, key).GetHashCode();
        }

        public int CompareTo(Identifier other)
        {
            return _hash.CompareTo(other._hash);
        }

        public bool Equals(Identifier other)
        {
            return _hash == other._hash;
        }

        public override bool Equals(object obj)
        {
            return obj is Identifier other && Equals(other);
        }

        public override int GetHashCode() => _hash;

        public override string ToString() => Namespace + Separator + Key;

        public static bool operator ==(Identifier a, Identifier b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Identifier a, Identifier b)
        {
            return !a.Equals(b);
        }
    }
}