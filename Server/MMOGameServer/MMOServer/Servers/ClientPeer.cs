using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MMOServer.Tools;
using MySql.Data.MySqlClient;

namespace MMOServer.Servers
{
    class ClientPeer
    {
        private MainServer mServer;
        private MessageTool mMsg;
        /// <summary>
        /// 接收数据缓冲区
        /// </summary>
        private byte[] mRecvBuffer;
        /// <summary>
        /// 接收数据缓冲区 
        /// </summary>
        public byte[] RecvDataBuffer
        {
            get
            {
                return mRecvBuffer;
            }
            set
            {
                mRecvBuffer = value;
            }
        }
        private MySqlConnection mMysqlConn;
        public MySqlConnection MySQLConn
        {
            get { return mMysqlConn; }
        }
        /// <summary>
        /// 客户端的Socket
        /// </summary>
        private Socket mClientSock;
        /// <summary>
        /// 获得与客户端会话关联的Socket对象
        /// </summary>
        public Socket ClientSocket
        {
            get
            {
                return mClientSock;

            }
        }

        public ClientPeer() { }
        public ClientPeer(Socket _socket, MainServer _server)
        {
            mMsg = new MessageTool();
            mClientSock = _socket;
            this.mServer = _server;
            mMysqlConn = ConnHelper.Connect();
        }
        public void OnProcessMessage(RequestCode _requestCode, ActionCode _actionCode, byte[] _data)
        {
            mServer.HandleRequest(_requestCode, _actionCode, _data, this);
            Console.WriteLine("接受数据");
        }


        /// <summary>
        /// 关闭会话
        /// </summary>
        public void Close()
        {
            try
            {
                ConnHelper.CloseConnection(mMysqlConn);
                //关闭数据的接受和发送
                mClientSock.Shutdown(SocketShutdown.Both);
            }
            catch (Exception) { }
            if (mClientSock != null)
                mClientSock.Close();
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="actionCode"></param>
        /// <param name="data"></param>
        public void SendResponse(ActionCode actionCode, byte[] data)
        {
            Send(actionCode, data);
        }
        public void Send(ActionCode actionCode, byte[] data)
        {
            try
            {
                byte[] bytes = MessageTool.PackData(actionCode, data);
                mClientSock.Send(bytes);
            }
            catch (Exception e)
            {
                Console.WriteLine("无法发送消息:" + e);
            }
        }
    }
}
