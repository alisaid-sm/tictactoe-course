using LiteNetLib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetworkShared;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TTT.Server.NetworkShared;
using TTT.Server.NetworkShared.Registries;

namespace TTT.Server
{
    public class NetworkServer : INetEventListener
    {
        NetManager _netManager;
        Dictionary<int, NetPeer> _connections;
        private readonly ILogger<NetworkServer> _logger;
        private readonly IServiceProvider _serviceProvider;

        public NetworkServer(
            ILogger<NetworkServer> logger,
            IServiceProvider provider
        )
        {
            _logger = logger;
            _serviceProvider = provider;
        }

        public void Start()
        {
            _connections = new Dictionary<int, NetPeer>();
            _netManager = new NetManager(this)
            {
                DisconnectTimeout = 100000
            };

            _netManager.Start(9050);

            Console.WriteLine("Server listening on port 9050");
        }

        public void PollEvents()
        {
            _netManager.PollEvents();
        }

        public void OnConnectionRequest(ConnectionRequest request)
        {
            Console.WriteLine($"Incomming connection from {request.RemoteEndPoint}");
            request.Accept();
        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, byte channelNumber, DeliveryMethod deliveryMethod)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var packetType = (PacketType)reader.GetByte();
                    var packet = ResolvePacket(packetType, reader);
                    var handler = ResolveHandler(packetType);

                    handler.Handle(packet, peer.Id);

                    reader.Recycle();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message of type XX");
                }
            }

            // var data = Encoding.UTF8.GetString(reader.RawData);
            // Console.WriteLine($"Data received from client: '{data}'");

            // Reply to client
            // var reply = "General Kenobi";
            // var bytes = Encoding.UTF8.GetBytes(reply);
            // peer.Send(bytes, DeliveryMethod.ReliableOrdered);
        }

        public void OnPeerConnected(NetPeer peer)
        {
            Console.WriteLine($"Client connected to server: {peer.Address}:{peer.Port}. Id: {peer.Id}");
            _connections.Add(peer.Id, peer);
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            Console.WriteLine($"{peer.Address}:{peer.Port} disconnected!");
            _connections.Remove(peer.Id);
        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {
        }

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {
        }

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {
        }

        public IPacketHandler ResolveHandler(PacketType packetType)
        {
            var registry = _serviceProvider.GetRequiredService<HandleRegistry>();
            var type = registry.Handlers[packetType];
            return (IPacketHandler)_serviceProvider.GetRequiredService(type);
        }

        private INetPacket ResolvePacket(PacketType packetType, NetPacketReader reader)
        {
            var registry = _serviceProvider.GetRequiredService<PacketRegistry>();
            var type = registry.PacketTypes[packetType];
            var packet = (INetPacket)Activator.CreateInstance(type);
            packet.Deserialize(reader);
            return packet;
        }
    }
}
