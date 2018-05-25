using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMOServer.Model;
using System.Threading;
using Common;
using MMOServer.Tools;

namespace MMOServer.Servers
{
    class RoomManager
    {
        public List<ClientPeer> PlayList = new List<ClientPeer>();
        private MainServer mMainServer;
        public RoomManager(MainServer _server)
        {
            mMainServer = _server;
        }
        private RoomInfo mRoomData;
        public RoomInfo GetRoonInfo { get { return mRoomData; } }
        public void SetRoomData(RoomInfo _roomData)
        {
            mRoomData = _roomData;
        }

        public void AddPlay(ClientPeer _player)
        {
            PlayList.Add(_player);
        }
        public void StartTimer()
        {
            new Thread(RunTimer).Start();
        }
        private void RunTimer()
        {
            Thread.Sleep(1000);
            for (int i = 10; i > 0; i--)
            {

                BroadcastMessage(null, ActionCode.ShowTimer, MessageTool.ProtoBufDataSerialize(i));
                Thread.Sleep(1000);
            }
            BroadcastMessage(null, ActionCode.StartPlay, MessageTool.ProtoBufDataSerialize("strat"));
        }
        public void BroadcastMessage(ClientPeer excludeClient, ActionCode actionCode, byte[] data)
        {

            foreach (ClientPeer client in PlayList)
            {
                if (client != excludeClient)
                {
                    mMainServer.SendResponse(client, actionCode, data);
                }
            }
        }
    }
}
