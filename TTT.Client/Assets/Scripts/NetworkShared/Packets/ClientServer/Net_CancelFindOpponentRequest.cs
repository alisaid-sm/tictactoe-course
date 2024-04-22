using LiteNetLib.Utils;

namespace NetworkShared.Packets.ClientServer
{
    public struct Net_CancelFindOpponentRequest : INetPacket
    {
        public PacketType type => PacketType.CancelFindOpponentRequest;

        public void Deserialize(NetDataReader reader)
        {
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put((byte)type);
        }
    }
}