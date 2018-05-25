using MMOServer.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using MMOServer.Model;
using MMOServer.Tools;

namespace MMOServer.Controllor
{
    class GameController:BaseController
    {
        public GameController()
        {
            requestCode=RequestCode.Game;
        }

        private RoomManager room = null;
        /// <summary>
        /// 匹配
        /// </summary>
        public byte[] SatrtMatchingPlayer(byte[] _data, ClientPeer _client, MainServer _server)
        {
            _client.IsMatching = true;
            List<ClientPeer> matchList=new List<ClientPeer>();
            do
            {
                foreach (var account in _server.OnLineAccountList)
                {
                    if (account.IsMatching)
                    {
                        matchList.Add(account);
                    }
                }
            } while (matchList.Count < 3);
            if (matchList.Count >= 3)
            {
              room=  _server.CreatRoom(matchList);
            }
            return MessageTool.ProtoBufDataSerialize("goLobby");
        }

        public byte[] StartGame(byte[] _data, ClientPeer _client, MainServer _server)
        {
            room.StartTimer();
            return null;
        }
    }
}
