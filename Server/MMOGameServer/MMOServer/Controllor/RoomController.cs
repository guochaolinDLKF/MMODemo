using MMOServer.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using MMOServer.DAO;
using MMOServer.Model;
using MMOServer.Tools;

namespace MMOServer.Controllor
{
    class RoomController:BaseController
    {
        private RoomDAO room;
        public RoomController()
        { 
            requestCode = RequestCode.Room;
            room = new RoomDAO();
        }
        public byte[] GetRoomList(byte[] _data, ClientPeer _client, MainServer _server)
        {
            List<RoomInfo> send = room.GetRoomListInfo(_client.MySQLConn);
            for (int i = 0; i < send.Count; i++)
            {
                _server.RoomList.Add(new RoomManager(_server));
            }
            for (int i = 0; i < _server.RoomList.Count; i++)
            {
                _server.RoomList[i].SetRoomData(send[i]);
            }
            return MessageTool.ProtoBufDataSerialize(send);
        }

        public byte[] JoinRoom(byte[] _data, ClientPeer _client, MainServer _server)
        {
            RoomInfo roomInfo = MessageTool.ProtoBufDataDeSerialize<RoomInfo>(_data);
            MSGCallBack sendMsg = null;
            foreach (var room in _server.RoomList)
            {
                if (roomInfo.RoomId == room.GetRoonInfo.RoomId)
                {
                    if (room.GetRoonInfo.TotalNum >= room.GetRoonInfo.PlayerNum)
                    {
                        _server.JoinRoom(_client, room);
                        sendMsg = new MSGCallBack(ReturnCode.Success);
                    }
                    else
                    {
                        sendMsg = new MSGCallBack(ReturnCode.Fail);
                    }
                }
            }
            return MessageTool.ProtoBufDataSerialize(sendMsg);
        }
    }
}
