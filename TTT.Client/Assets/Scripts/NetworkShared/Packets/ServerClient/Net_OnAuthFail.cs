using LiteNetLib.Utils;

namespace NetworkShared.Packets.ServerClient
{
    public class Net_OnAuthFail : INetPacket
    {
        public PacketType type => PacketType.OnAuthFail;

        public void Deserialize(NetDataReader reader)
        {
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put((byte)type);
        }
    }
}