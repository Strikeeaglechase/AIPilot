using UnityGERunner;
using Coroutine;
using UnityGERunner.UnityApplication;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;


namespace Decompress.V4
{

    enum PacketFlags
    {
        NoId = 0b00000001,
        HasId = 0b00000010,
        JSONBody = 0b00000100,
        BinBody = 0b00001000,
        IdIsNumber = 0b00010000,
        IdIsString = 0b00100000,
        HasTimestamp = 0b01000000
    }

    enum ArgumentType
    {
        String = 0b00000001,
        Number = 0b00000010,
        Boolean = 0b00000100,
        Null = 0b00001000,
        Vector = 0b00010000
    }

    public class DecompressorV4
    {
        private byte[] bytes;
        private int index;

        public DecompressorV4(byte[] bytes)
        {
            this.bytes = bytes;
        }

        private byte ReadOne() => bytes[index++];
        private bool BitCheck(byte value, int bit) => (value & bit) == bit;

        private int DecompressInt()
        {
            int result = 0;
            int index = 0;
            while (index < 50)
            {
                var next = ReadOne();
                var bits = next & 0b01111111;

                result = result + (bits << (7 * index));
                if ((next & 0b10000000) == 0) break;
                index++;
            }

            return result;
        }

        private double GetDouble()
        {
            var val = BitConverter.ToDouble(bytes, index);
            index += 8;

            return val;
        }

        public float GetFloat()
        {
            var val = BitConverter.ToSingle(bytes, index);
            index += 4;

            return val;
        }

        public List<RPC> DecompressRPCPackets()
        {
            var version = ReadOne();
            var numStrs = DecompressInt();
            var strings = new List<string>(numStrs);
            for (int i = 0; i < numStrs; i++)
            {
                var strLen = ReadOne();
                var str = Encoding.UTF8.GetString(bytes, index, strLen);
                strings.Add(str);
                index += strLen;
            }

            var timestampOffset = GetDouble();

            var numRpcPackets = GetDouble();
            var rpcPackets = new List<RPC>((int)numRpcPackets);
            for (int i = 0; i < numRpcPackets; i++)
            {
                var className = strings[DecompressInt()];
                var method = strings[DecompressInt()];
                var packetFlags = ReadOne();

                var idIsNum = BitCheck(packetFlags, (int)PacketFlags.IdIsNumber);
                var hasId = BitCheck(packetFlags, (int)PacketFlags.HasId);
                var hasTimestamp = BitCheck(packetFlags, (int)PacketFlags.HasTimestamp);

                string id = null;
                if (hasId)
                {
                    if (idIsNum) id = DecompressInt().ToString();
                    else id = strings[DecompressInt()];
                }

                double timestamp = 0;
                if (hasTimestamp) timestamp = DecompressInt() + timestampOffset;

                var argLen = DecompressInt();
                object[] args;
                if (BitCheck(packetFlags, (int)PacketFlags.BinBody))
                {
                    args = DecompressArgs(argLen);
                }
                else
                {
                    var argStr = Encoding.UTF8.GetString(bytes, index, argLen);
                    index += argLen;

                    args = JsonConvert.DeserializeObject<object[]>(argStr);
                }

                var rpc = new RPC
                {
                    className = className,
                    method = method,
                    id = id,
                    args = args,
                };

                rpcPackets.Add(rpc);
            }

            return rpcPackets;
        }

        private object[] DecompressArgs(int length)
        {
            var result = new List<object>();
            var endPoint = index + length;

            while (index < endPoint)
            {
                var type = (ArgumentType)bytes[index++];
                switch (type)
                {
                    case ArgumentType.String:
                        var len = bytes[index++];
                        var str = Encoding.UTF8.GetString(bytes, index, len);
                        index += len;
                        result.Add(str);
                        break;
                    case ArgumentType.Number:
                        result.Add(GetFloat());
                        break;
                    case ArgumentType.Boolean:
                        result.Add(bytes[index++] == 1);
                        break;
                    case ArgumentType.Null:
                        result.Add(null);
                        break;
                    case ArgumentType.Vector:
                        var x = GetFloat();
                        var y = GetFloat();
                        var z = GetFloat();
                        result.Add(new NetVector { x = x, y = y, z = z });
                        break;
                    default:
                        Logger.Error("[HSGE] " + $"Unknown argument type: {type}");
                        break;
                }
            }

            return result.ToArray();
        }
    }
}
