using System.Net;
using System.Net.Sockets;
using System.Text;
using LiteNetLib;
using LiteNetLib.Utils;
using UnityEngine;

public class NetworkClient : MonoBehaviour, INetEventListener
{
    private NetManager _netManager;
    private NetPeer _server;
    private NetDataWriter _writer;

    private static NetworkClient _instance;

    public static NetworkClient Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        _netManager.PollEvents();
    }

    public void Init()
    {
        _writer = new NetDataWriter();
        _netManager = new NetManager(this)
        {
            DisconnectTimeout = 100000
        };
        _netManager.Start();
    }

    public void Connect()
    {
        _netManager.Connect("localhost", 9050, "");
    }

    public void SendServer(string data)
    {
        var bytes = Encoding.UTF8.GetBytes(data);
        _server.Send(bytes, DeliveryMethod.ReliableOrdered);
    }

    public void OnConnectionRequest(ConnectionRequest request)
    {
    }

    public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
    {
    }

    public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
    {
    }

    public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, byte channelNumber, DeliveryMethod deliveryMethod)
    {
        var data = Encoding.UTF8.GetString(reader.RawData).Replace("\0", "");
        Debug.Log($"Data received from server: '{data}'");
    }

    public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {
    }

    public void OnPeerConnected(NetPeer peer)
    {
        Debug.Log("We connected to server at " + peer.Address + ":" + peer.Port);
        _server = peer;
    }

    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Debug.Log("Lost connection to server!");
    }
}
