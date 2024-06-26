using System;
using NetworkShared;
using NetworkShared.Attributes;

namespace TTT.Server.PacketHandlers
{
    [HandlerRegister(PacketType.CancelFindOpponentRequest)]
    public class CancelFindOpponentRequestHandler : IPacketHandler
    {
        public void Handle(INetPacket packet, int connectionId)
        {
            Console.WriteLine("Received cancel find opponent request!");
        }
    }
}