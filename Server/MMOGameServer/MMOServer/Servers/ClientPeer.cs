using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MMOServer.Model;
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
        /// <summary>
        /// 是否正在匹配
        /// </summary>
        public bool IsMatching = false;
        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnLine = false;
        //当前账户
        private AccountInfo mAccount=null;
        private List<CharacterInfo> mCharacterList = null; 
        /// <summary>
        /// 当前账户拥有的角色列表
        /// </summary>
        public List<CharacterInfo> GetCurCharacterInfoList { get { return mCharacterList; } }
        /// <summary>
        /// 获取当前账户
        /// </summary>
        public AccountInfo GetCurAccount { get {return mAccount;} }
        /// <summary>
        /// 头像路径字典
        /// </summary>
        public Dictionary<int, string> ImgPathDic;

        public RoomManager CurRoom;
        public ClientPeer() { }
        public ClientPeer(Socket _socket, MainServer _server)
        {
            mMsg = new MessageTool();
            mClientSock = _socket;
            this.mServer = _server;
            mMysqlConn = ConnHelper.Connect();
            ImgPathDic=new Dictionary<int, string>()
            {
                {1001,"iiefrqwrief"},
                {1002,"iiefrqwrief"},
                {1003,"iiefrqwrief"},
                {1004,"iiefrqwrief"},
            };
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
        #region 当前用户数据
        /// <summary>
        /// 设置当前用户数据
        /// </summary>
        /// <param name="info"></param>
        public void SetCurAccountData(AccountInfo info)
        {
            mAccount = info;
        }
        #endregion




    }
}
