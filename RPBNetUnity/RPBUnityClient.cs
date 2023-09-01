using System;
using System.Net.Sockets;
using RPBCommon;
using RPBCommon.Packet;
using RPBNet.NetworkBase;
using RPBNet.NetworkBase.Connections;
using RPBNet.NetworkBase.Server;
using RPBPacketBase;
using RPBClient = RPBNet.NetworkBase.Client.RPBUnityClient;
using UnityEngine;

namespace RPBNetUnity
{
    public class RPBUnityClient : MonoBehaviour
    {
        [SerializeField] private string serverIPAddress;
        [SerializeField] private int serverPort;

        private readonly RPBClient _client = new RPBClient();

        private void Awake()
        {
            RPBNet.Network.Initialize();
        }

        void Start()
        {
            var rc = _client.TryConnect(serverIPAddress, serverPort);

#if UNITY_EDITOR
            Debug.Log(rc);
#endif
        }

        private void Update()
        {
            _client.Update();
        }

        public void Send(RPBPacket packet)
        {
            _client.Send(packet);
        }


        public void RegisterAction<T>(Action<T,IConnection> action) where T : RPBPacket
        {
            PacketParser.RegisterPacketAction(action);
        }
    }

}
