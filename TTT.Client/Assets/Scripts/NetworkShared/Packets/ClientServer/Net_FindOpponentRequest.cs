using LiteNetLib.Utils;

namespace NetworkShared.Packets.ClientServer
{
    public struct Net_FindOpponentRequest : INetPacket
    {
        public PacketType type => PacketType.FindOpponentRequest;

        public void Deserialize(NetDataReader reader)
        {
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put((byte)type);
        }
    }
}