using LiteNetLib.Utils;

namespace NetworkShared.Packets.ClientServer
{
    public class Net_AuthRequest : INetPacket
    {
        public PacketType type => PacketType.AuthRequest;

        public string Username { get; set; }
        public string Password { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            Username = reader.GetString();
            Password = reader.GetString();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put((byte)type);
            writer.Put(Username);
            writer.Put(Password);
        }
    }
}