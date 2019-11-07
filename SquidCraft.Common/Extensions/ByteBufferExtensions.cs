using System;
using System.IO;
using System.Numerics;
using System.Text;
using System.Text.Json;
using DotNetty.Buffers;
using SquidCraft.API.Math;
using SquidCraft.API.Utils;
using SquidCraft.Json;
using SquidCraft.NBT;

namespace SquidCraft.Extensions
{
    public static class ByteBufferExtensions
    {
        public static byte[] ToArray(this IByteBuffer buffer, out int offset, out int count)
        {
            if (buffer.HasArray)
            {
                offset = buffer.ArrayOffset;
                count = buffer.ReadableBytes;
                return buffer.Array;
            }

            offset = 0;
            count = buffer.ReadableBytes;
            var bytes = new byte[count];
            buffer.MarkReaderIndex();
            buffer.ReadBytes(bytes);
            buffer.ResetReaderIndex();
            return bytes;
        }

        public static byte[] ReadBytes(this IByteBuffer buffer)
        {
            var bytes = new byte[buffer.ReadableBytes];
            buffer.ReadBytes(bytes);
            return bytes;
        }

        public static byte[] ReadByteArray(this IByteBuffer buffer)
        {
            var bytes = new byte[buffer.ReadVarInt32()];
            buffer.ReadBytes(bytes);
            return bytes;
        }

        public static IByteBuffer WriteByteArray(this IByteBuffer buffer, byte[] bytes)
        {
            buffer.WriteVarInt32(bytes.Length);
            buffer.WriteBytes(bytes);
            return buffer;
        }

        public static IByteBuffer WriteGuid(this IByteBuffer buffer, Guid guid)
        {
            var bytes = guid.ToByteArray();
            foreach (var b in bytes)
                buffer.WriteByte(b);
            return buffer;
        }

        public static string ReadUtf8(this IByteBuffer buffer, short maxLen = short.MaxValue)
        {
            var len = buffer.ReadVarInt32();
            if (len > maxLen)
                throw new IndexOutOfRangeException("String is too long");

            if (buffer.HasArray)
            {
                var bytes = buffer.ReadBytes(len);
                return Encoding.UTF8.GetString(bytes.Array, bytes.ArrayOffset, len);
            }
            else
            {
                var bytes = new byte[len];
                buffer.GetBytes(buffer.ReaderIndex, bytes);
                return Encoding.UTF8.GetString(bytes);
            }
        }

        public static IByteBuffer WriteUtf8(this IByteBuffer buffer, string s)
        {
            if (s.Length > short.MaxValue)
                throw new IndexOutOfRangeException("String is too long");

            var bytes = Encoding.UTF8.GetBytes(s);
            buffer.WriteVarInt32(bytes.Length);
            buffer.WriteBytes(bytes);
            return buffer;
        }

        public static IByteBuffer WriteIdentifier(this IByteBuffer buffer, Identifier identifier)
        {
            buffer.WriteUtf8(identifier.ToString());
            return buffer;
        }

        public static T ReadJson<T>(this IByteBuffer buffer)
        {
            var serialized = buffer.ReadUtf8();
            return JsonSerializer.Deserialize<T>(serialized, DefaultJsonSettings.Options);
        }

        public static IByteBuffer WriteJson(this IByteBuffer buffer, object @object)
        {
            var serialized = JsonSerializer.Serialize(@object);
            buffer.WriteUtf8(serialized);
            return buffer;
        }

        public static IByteBuffer WriteVector3F(this IByteBuffer buffer, Vector3 vector3)
        {
            buffer.WriteFloat(vector3.X);
            buffer.WriteFloat(vector3.Y);
            buffer.WriteFloat(vector3.Z);
            return buffer;
        }

        public static Vector3 ReadVector3D(this IByteBuffer buffer)
        {
            return new Vector3(
                (float) buffer.ReadDouble(),
                (float) buffer.ReadDouble(),
                (float) buffer.ReadDouble()
            );
        }

        public static IByteBuffer WriteVector3D(this IByteBuffer buffer, Vector3 vector3)
        {
            buffer.WriteDouble(vector3.X);
            buffer.WriteDouble(vector3.Y);
            buffer.WriteDouble(vector3.Z);
            return buffer;
        }

        public static IByteBuffer WriteVector2F(this IByteBuffer buffer, Vector2 vector3)
        {
            buffer.WriteFloat(vector3.X);
            buffer.WriteFloat(vector3.Y);
            return buffer;
        }

        public static Rotation ReadRotation(this IByteBuffer buffer)
        {
            return new Rotation(
                buffer.ReadFloat(),
                buffer.ReadFloat()
            );
        }

        public static IByteBuffer WriteRotationF(this IByteBuffer buffer, Rotation rotation)
        {
            buffer.WriteFloat(rotation.Yaw);
            buffer.WriteFloat(rotation.Pitch);
            return buffer;
        }

        public static IByteBuffer WriteRotationB(this IByteBuffer buffer, Rotation rotation)
        {
            buffer.WriteByte((int) (rotation.Yaw / 360 * 256));
            buffer.WriteByte((int) (rotation.Pitch / 360 * 256));
            return buffer;
        }

        public static IByteBuffer WriteCompound(this IByteBuffer buffer, NbtCompound compound)
        {
            buffer.WriteByte(compound.Id);
            buffer.WriteShort(0); // empty name

            using (var stream = new MemoryStream())
            {
                using var writer = new BinaryWriter(stream);
                compound.Serialize(writer);
                
                var serialized = stream.ToArray();
                buffer.WriteBytes(serialized);
            }

            return buffer;
        }
    }
}