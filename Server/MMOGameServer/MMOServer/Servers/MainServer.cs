using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MMOServer.Tools;
using Common;
using MMOServer.Controllor;
using MMOServer.Model;

namespace MMOServer.Servers
{
    class MainServer : IDisposable
    {
        /// <summary>
        /// 服务器程序允许的最大客户端连接数
        /// </summary>
        private int mMaxClient;

        /// <summary>
        /// 当前的连接的客户端数
        /// </summary>
        private int mClientCount;

        /// <summary>
        /// 服务器使用的异步socket
        /// </summary>
        private Socket mServerSock;

        private ControllerManager mControllerManager;
        /// <summary>
        /// 客户端会话列表
        /// </summary>
        private List<ClientPeer> mClientList;
        /// <summary>
        /// 在线账户列表
        /// </summary>
        public List<ClientPeer> OnLineAccountList
        {
            get { return mClientList; }
        }
        /// <summary>
        /// 服务器是否正在运行
        /// </summary>
        public bool IsRunning { get; private set; }

        public int RoomIndex = 10000;
        /// <summary>
        /// 监听的IP地址
        /// </summary>
        public IPAddress Address { get; private set; }

        /// <summary>
        /// 监听的端口
        /// </summary>
        public int Port { get; private set; }

        private MessageTool mMsg;

        public delegate void StorageGetData(byte[] data);

        public event StorageGetData GetData;
        /// <summary>
        /// 房间列表
        /// </summary>
        public List<RoomManager> RoomList; 

        #region 连接和断开

        public MainServer()
        {
        }

        /// <summary>
        /// 异步Socket TCP服务器
        /// </summary>
        /// <param name="listenPort">监听的端口</param>
        public MainServer(int listenPort) : this(IPAddress.Any, listenPort, 1024)
        {

        }

        /// <summary>
        /// 异步Socket TCP服务器
        /// </summary>
        /// <param name="localEP">监听的终结点</param>
        public MainServer(IPEndPoint localEP) : this(localEP.Address, localEP.Port, 1024)
        {

        }

        /// <summary>
        /// 异步Socket TCP服务器
        /// </summary>
        /// <param name="localIPAddress">监听的IP地址</param>
        /// <param name="listenPort">监听的端口</param>
        /// <param name="maxClient">最大客户端数量</param>
        public MainServer(IPAddress localIPAddress, int listenPort, int maxClient)
        {
            this.Address = localIPAddress;
            this.Port = listenPort;
            mMaxClient = maxClient;
            mClientList = new List<ClientPeer>();
            mServerSock = new Socket(localIPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            mControllerManager = new ControllerManager(this);
            RoomList=new List<RoomManager>();
        }

        /// <summary>
        /// 启动服务器
        /// </summary>
        public void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                mServerSock.Bind(new IPEndPoint(this.Address, this.Port));
                mServerSock.Listen(mMaxClient);
                mServerSock.BeginAccept(new AsyncCallback(AcceptCallBack), mServerSock);
            }
        }

        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <param name="backlog">
        /// 服务器所允许的挂起连接序列的最大长度
        /// </param>
        public void Start(int backlog)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                mServerSock.Bind(new IPEndPoint(this.Address, this.Port));
                mServerSock.Listen(backlog);
                mServerSock.BeginAccept(new AsyncCallback(AcceptCallBack), mServerSock);
            }
        }

        public ClientPeer client = null;

        private void AcceptCallBack(IAsyncResult ar)
        {
            if (ar.AsyncWaitHandle.WaitOne(5000))
            {
                if (IsRunning)
                {
                    Socket server = (Socket)ar.AsyncState;
                    Socket clientSocket = server.EndAccept(ar);
                    if (mClientCount <= mMaxClient)
                    {
                        client = new ClientPeer(clientSocket, this);
                        lock (mClientList)
                        {
                            mClientList.Add(client);
                            mClientCount++;
                            Console.WriteLine("一个客户端连接进来了");
                            mMsg = new MessageTool(client, this);

                        }
                        client.RecvDataBuffer = new byte[mMsg.DataBytesMaxLength];
                        //开始接受来自该客户端的数据 
                        clientSocket.BeginReceive(client.RecvDataBuffer, 0, client.RecvDataBuffer.Length,
                            SocketFlags.None,
                            new AsyncCallback(HandleDataReceived), client);
                        mServerSock.BeginAccept(new AsyncCallback(AcceptCallBack), mServerSock);
                    }
                    else
                    {
                        Console.WriteLine("服务器爆满");
                    }
                }
            }
            else
            {
                Console.WriteLine("超时");
            }

        }

        /// <summary>
        /// 处理客户端数据
        /// </summary>
        /// <param name="ar"></param>
        private void HandleDataReceived(IAsyncResult ar)
        {
            if (IsRunning)
            {
               
                ClientPeer state = (ClientPeer)ar.AsyncState;
                Socket client = state.ClientSocket;
                if (!ar.AsyncWaitHandle.WaitOne(5000))
                {
                    Console.WriteLine("超时");
                    Close(state);
                    return;
                }
                try
                {

                    int count = client.EndReceive(ar);
                    if (count == 0)
                    {
                        Close(state);
                        return;
                    }
                    if (GetData != null)
                    {
                        GetData(state.RecvDataBuffer);
                    }
                    mMsg.ReadMessage(count);
                    client.BeginReceive(state.RecvDataBuffer, 0, state.RecvDataBuffer.Length, SocketFlags.None,
                        new AsyncCallback(HandleDataReceived), state);
                }
                catch (Exception e)
                {
                    Console.WriteLine("一个客户端断开连接:");
                    Close(state);
                }
            }
        }
        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, byte[] data, ClientPeer client)
        {
            mControllerManager.HandleRequest(requestCode, actionCode, data, client);
        }
        public void SendResponse(ClientPeer client, ActionCode actionCode, byte[] data)
        {
            client.SendResponse(actionCode, data);
        }
        /// <summary>
        /// 关闭一个与客户端之间的会话
        /// </summary>
        /// <param name="state">需要关闭的客户端会话对象</param>
        public void Close(ClientPeer state)
        {
            if (state != null)
            {
                state.RecvDataBuffer = null;
                state.IsOnLine = false;
                lock (mClientList)
                {
                    mClientList.Remove(state);
                }
                mClientCount--;
                //触发关闭事件
                state.Close();
            }
        }

        /// <summary>
        /// 关闭所有的客户端会话,与所有的客户端连接会断开
        /// </summary>
        public void CloseAllClient()
        {
            foreach (ClientPeer client in mClientList)
            {
                Close(client);
            }
            mClientCount = 0;
            mClientList.Clear();
        }

        public void Dispose()
        {
            CloseAllClient();
        }

        #endregion
        /// <summary>
        /// 服务器主动广播
        /// </summary>
        public void BroadcastMessage(ClientPeer excludeClient, ActionCode actionCode, byte[] data)
        {

            foreach (ClientPeer client in OnLineAccountList)
            {
                if (client.IsOnLine)
                {
                    if (client != excludeClient)
                    {
                        SendResponse(client, actionCode, data);
                    }
                }
               
            }
        }
        /// <summary>
        /// 加入房间
        /// </summary>
        /// <param name="_player"></param>
        /// <param name="_room"></param>
        public void JoinRoom(ClientPeer _player, RoomManager _room)
        {
            _room.AddPlay(_player);
        }

        public RoomManager CreatRoom(List<ClientPeer> _client)
        {
            RoomManager room=new RoomManager(this);
            RoomInfo info=new RoomInfo()
            {
                RoomId = RoomIndex++,
                TotalNum = 3
            };
            room.PlayList= _client;
            room.SetRoomData(info);
            return room;
        }
    }

}
