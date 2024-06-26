using System;
using LiteNetLib.Utils;

namespace NetworkShared.Packets.ServerClient
{
    public struct Net_OnStartGame : INetPacket
    {
        public PacketType type => PacketType.OnStartGame;

        public string XUser { get; set; }
        public string OUser { get; set; }
        public Guid GameId { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            XUser = reader.GetString();
            OUser = reader.GetString();
            GameId = Guid.Parse(reader.GetString());
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put((byte)type);
            writer.Put(XUser);
            writer.Put(OUser);
            writer.Put(GameId.ToString());
        }
    }
}