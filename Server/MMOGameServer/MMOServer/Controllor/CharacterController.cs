using MMOServer.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using MMOServer.Model;
using MMOServer.Servers;
using MMOServer.Tools;

namespace MMOServer.Controllor
{
    class CharacterController : BaseController
    {
        private CharacterDAO mCharacter;

        public CharacterController()
        {
            requestCode = RequestCode.Character;
            mCharacter = new CharacterDAO();
        }

        public byte[] GetCharacterInfo(byte[] data, ClientPeer client, MainServer server)
        {
            CharacterInfo receive = MessageTool.ProtoBufDataDeSerialize<CharacterInfo>(data);
            CharacterInfo info = mCharacter.GetCharacterInfoByAccountId(client.MySQLConn, receive.AccountId);
            if (info != null)
            {
                return MessageTool.ProtoBufDataSerialize(info);
            }
            else
            {
                MSGCallBack msg = new MSGCallBack(ReturnCode.Fail);
                return MessageTool.ProtoBufDataSerialize(msg);
            }
        }
    }
}
