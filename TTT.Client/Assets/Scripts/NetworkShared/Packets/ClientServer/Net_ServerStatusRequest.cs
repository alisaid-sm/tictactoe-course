using System;
using LiteNetLib.Utils;

namespace NetworkShared.Packets.ClientServer
{
    public struct Net_ServerStatusRequest : INetPacket
    {
        public PacketType type => PacketType.ServerStatusRequest;

        public void Deserialize(NetDataReader reader)
        {
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put((byte)type);
        }
    }
}