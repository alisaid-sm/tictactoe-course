using LiteNetLib.Utils;

namespace NetworkShared
{
    public enum PacketType : byte
    {
        #region ClientServer
        Invalid = 0,
        AuthRequest = 1,
        #endregion

        #region ServerClient
        OnAuth = 100
        #endregion
    }

    public interface INetPacket : INetSerializable
    {
        PacketType type { get; }
    }
}