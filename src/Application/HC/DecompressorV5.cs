using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Decompress.V5
{
    enum PacketFlags
    {
        HasId = 0b00000001,
        JSONBody = 0b00000010,
        IdIsNumber = 0b00000100,
        HasTimestamp = 0b00001000,
        ShortStringIndexMode = 0b00010000
    }

    enum ArgumentType
    {
        ShortString, // Any ascii string, up to length 255
        String, // Any ascii string, unlimited length
        Byte, // 8 bit positive int
        NegativeByte, // 8 bit negative int
        Short, // 16 bit positive int
        NegativeShort, // 16 bit negative int
        Int, // 32 bit positive int
        NegativeInt, // 32 bit negative int
        Float, // 32 bit float
        Double, // 64 bit float
        True, // Literal true
        False, // Literal false
        Null, // Literal null
        Vector, // Vector3, 3 floats
        ZeroVector, // Vector3 with all components 0
        HalfVector, // Vector3, 3 half floats
        FlaggedVector // A vector with bit flags
    }

    class Reader
    {
        private byte[] bytes;
        private int index = 0;

        public Reader(byte[] bytes)
        {
            this.bytes = bytes;
        }

        public byte ReadByte() => bytes[index++];

        public int DecompressInt()
        {
            int result = 0;
            int index = 0;
            while (index < 50)
            {
                var next = ReadByte();
                var bits = next & 0b01111111;

                result = result + (bits << (7 * index));
                if ((next & 0b10000000) == 0) break;
                index++;
            }

            return result;
        }

        public double ReadF64()
        {
            var val = BitConverter.ToDouble(bytes, index);
            index += 8;

            return val;
        }

        public float ReadF32()
        {
            var val = BitConverter.ToSingle(bytes, index);
            index += 4;

            return val;
        }

        public float ReadF16()
        {
            var val = Mathf.HalfToFloat(BitConverter.ToUInt16(bytes, index));

            index += 2;
            return val;
        }

        public uint ReadI32()
        {
            var val = BitConverter.ToUInt32(bytes, index);
            index += 4;

            return val;
        }

        public ushort ReadI16()
        {
            var val = BitConverter.ToUInt16(bytes, index);
            index += 2;

            return val;
        }

        public string ReadString(int length)
        {
            var str = Encoding.UTF8.GetString(bytes, index, length);
            index += length;
            return str;
        }
    }

    public class DecompressorV5
    {
        private int lastArgType = (int)ArgumentType.FlaggedVector;
        private Reader reader;

        private Dictionary<byte, object> dynamicArgs = new Dictionary<byte, object>();

        public DecompressorV5(byte[] bytes)
        {
            reader = new Reader(bytes);
        }

        private bool BitCheck(byte value, int bit) => (value & bit) == bit;

        private NetVector DecompressFlaggedVector()
        {
            int flagFloat16 = 0b01;
            int flagZero = 0b10;

            var flags = reader.ReadByte();
            var vector = new NetVector { x = 0, y = 0, z = 0 };


            byte xFlags = (byte)((flags >> 0) & 0b11);
            byte yFlags = (byte)((flags >> 2) & 0b11);
            byte zFlags = (byte)((flags >> 4) & 0b11);

            // Logger.Info("[HSGE] " + $"xFlag: {xFlags}, yFlags: {yFlags}, zFlags: {zFlags}");

            var xIsZero = BitCheck(xFlags, flagZero);
            var xIsF16 = BitCheck(xFlags, flagFloat16);

            var yIsZero = BitCheck(yFlags, flagZero);
            var yIsF16 = BitCheck(yFlags, flagFloat16);

            var zIsZero = BitCheck(zFlags, flagZero);
            var zIsF16 = BitCheck(zFlags, flagFloat16);

            if (!xIsZero) vector.x = xIsF16 ? reader.ReadF16() : reader.ReadF32();
            if (!yIsZero) vector.y = yIsF16 ? reader.ReadF16() : reader.ReadF32();
            if (!zIsZero) vector.z = zIsF16 ? reader.ReadF16() : reader.ReadF32();

            return vector;
        }

        private object DecompressArgument()
        {
            var typeByte = reader.ReadByte();
            var type = (ArgumentType)typeByte;
            // Logger.Info("[HSGE] " + $"Arg {type} ({typeByte})");
            switch (type)
            {
                case ArgumentType.ShortString:
                    return reader.ReadString(reader.ReadByte());
                case ArgumentType.String:
                    return reader.ReadString(reader.DecompressInt());
                case ArgumentType.True:
                    return true;
                case ArgumentType.False:
                    return false;
                case ArgumentType.Byte:
                    return reader.ReadByte();
                case ArgumentType.NegativeByte:
                    return -reader.ReadByte();
                case ArgumentType.Short:
                    return reader.ReadI16();
                case ArgumentType.NegativeShort:
                    return -reader.ReadI16();
                case ArgumentType.Int:
                    return reader.ReadI32();
                case ArgumentType.NegativeInt:
                    return -reader.ReadI32();
                case ArgumentType.Float:
                    return reader.ReadF32();
                case ArgumentType.Double:
                    return reader.ReadF64();
                case ArgumentType.Null:
                    return null;
                case ArgumentType.Vector:
                    return new NetVector { x = reader.ReadF32(), y = reader.ReadF32(), z = reader.ReadF32() };
                case ArgumentType.HalfVector:
                    return new NetVector { x = reader.ReadF16(), y = reader.ReadF16(), z = reader.ReadF16() };
                case ArgumentType.ZeroVector:
                    return new NetVector { x = 0, y = 0, z = 0 };
                case ArgumentType.FlaggedVector:
                    return DecompressFlaggedVector();
                default:
                    if (dynamicArgs.ContainsKey(typeByte))
                    {
                        return dynamicArgs[typeByte];
                    }

                    throw new ArgumentException($"Unknown argument type: {typeByte}");
            }

        }

        public List<RPC> DecompressRPCPackets()
        {
            // Logger.Info("[HSGE] " + $"== Begin decompress RPC chunk ==");
            var version = reader.ReadByte();
            // Logger.Info("[HSGE] " + $"Version: {version}");
            var numStrs = reader.DecompressInt();
            // Logger.Info("[HSGE] " + $"Num Strs: {numStrs}");

            var strings = new List<string>(numStrs);
            for (int i = 0; i < numStrs; i++)
            {
                var strLen = reader.ReadByte();
                var str = reader.ReadString(strLen);
                strings.Add(str);
                // Logger.Info("[HSGE] " + $"String {i} -> ({strLen}) {str}");
            }

            var timestampOffset = reader.ReadF64();
            // Logger.Info("[HSGE] " + $"TS Offset: {timestampOffset}");

            var dynamicArgCount = reader.ReadByte();
            // Logger.Info("[HSGE] " + $"DynArg Count: {dynamicArgCount}");
            for (int i = 0; i < dynamicArgCount; i++)
            {
                var arg = DecompressArgument();
                dynamicArgs.Add((byte)(i + lastArgType + 1), arg);
                // Logger.Info("[HSGE] " + $"Dynamic args: ({i}): {i + lastArgType + 1} -> {arg} ({arg.GetType().Name})");
            }

            var numRpcPackets = reader.ReadI32();
            // Logger.Info("[HSGE] " + $"Num Packets: {numRpcPackets}");
            var rpcPackets = new List<RPC>((int)numRpcPackets);
            for (int i = 0; i < numRpcPackets; i++)
            {
                var packetFlags = reader.ReadByte();
                var idIsNum = BitCheck(packetFlags, (int)PacketFlags.IdIsNumber);
                var hasId = BitCheck(packetFlags, (int)PacketFlags.HasId);
                var hasTimestamp = BitCheck(packetFlags, (int)PacketFlags.HasTimestamp);
                var shortIndexMode = BitCheck(packetFlags, (int)PacketFlags.ShortStringIndexMode);
                var isJsonBody = BitCheck(packetFlags, (int)PacketFlags.JSONBody);
                // Logger.Info("[HSGE] " + $"Packet Flags: ");
                // Logger.Info("[HSGE] " + $" - idIsNum: {idIsNum}");
                // Logger.Info("[HSGE] " + $" - hasId: {hasId}");
                // Logger.Info("[HSGE] " + $" - hasTimestamp: {hasTimestamp}");
                // Logger.Info("[HSGE] " + $" - shortIndexMode: {shortIndexMode}");
                // Logger.Info("[HSGE] " + $" - isJsonBody: {isJsonBody}");

                string className;
                string methodName;
                if (shortIndexMode)
                {
                    var indexByte = reader.ReadByte();
                    var classNameIdx = (indexByte & 0b11110000) >> 4;
                    var methodNameIdx = indexByte & 0b00001111;

                    // Logger.Info("[HSGE] " + $"Short Index mode: ");

                    className = strings[classNameIdx];
                    methodName = strings[methodNameIdx];

                    // Logger.Info("[HSGE] " + $" - Class: ({classNameIdx}) {className}");
                    // Logger.Info("[HSGE] " + $" - Method: ({methodNameIdx}) {methodName}");
                }
                else
                {
                    className = strings[reader.DecompressInt()];
                    methodName = strings[reader.DecompressInt()];

                    // Logger.Info("[HSGE] " + $"Regular index mode: ");
                    // Logger.Info("[HSGE] " + $" - Class: {className}");
                    // Logger.Info("[HSGE] " + $" - Method: {methodName}");
                }


                string id = null;
                if (hasId)
                {
                    if (idIsNum) id = reader.DecompressInt().ToString();
                    else id = strings[reader.DecompressInt()];

                    // Logger.Info("[HSGE] " + $"ID: {id}");
                }

                double timestamp = 0;
                if (hasTimestamp) timestamp = reader.DecompressInt() + timestampOffset;
                // Logger.Info("[HSGE] " + $"Timestamp: {timestamp}");

                object[] args;
                if (!isJsonBody)
                {
                    var argCount = reader.ReadByte();
                    // Logger.Info("[HSGE] " + $"BinArg count: {argCount}");
                    args = new object[argCount];

                    for (var j = 0; j < argCount; j++)
                    {
                        // Logger.Info("[HSGE] " + $"Arg {j}: ");
                        args[j] = DecompressArgument();
                        // Logger.Info("[HSGE] " + $" - {args[j]}");
                    }
                }
                else
                {
                    var strLen = reader.DecompressInt();
                    var argStr = reader.ReadString(strLen);
                    // Logger.Info("[HSGE] " + $"JSON Body ({strLen}): {argStr}");
                    args = JsonConvert.DeserializeObject<object[]>(argStr);
                }

                var rpc = new RPC
                {
                    className = className,
                    method = methodName,
                    id = id,
                    args = args,
                };

                rpcPackets.Add(rpc);
            }

            return rpcPackets;
        }
    }
}
