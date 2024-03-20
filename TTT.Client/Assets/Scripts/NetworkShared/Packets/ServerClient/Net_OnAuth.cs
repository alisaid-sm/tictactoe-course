using LiteNetLib.Utils;

namespace NetworkShared.Packets.ServerClient
{
    public class Net_OnAuth : INetPacket
    {
        public PacketType type => PacketType.OnAuth;

        public void Deserialize(NetDataReader reader)
        {
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put((byte)type);
        }
    }
}