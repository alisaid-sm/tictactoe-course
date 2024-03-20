using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteNetLib.Utils;
using NetworkShared;

namespace NetworkShared.Packets.ServerClient
{
    public struct Net_OnServerStatus : INetPacket
    {
        public PacketType type => PacketType.OnServerStatus;

        public void Deserialize(NetDataReader reader)
        {
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put((byte)type);
        }
    }
}