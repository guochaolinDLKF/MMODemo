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
        private ImgDAO mImgDao;
        public CharacterController()
        {
            requestCode = RequestCode.Character;
            mCharacter = new CharacterDAO();
            mImgDao = new ImgDAO();
        }
        /// <summary>
        /// 获取角色信息
        /// </summary> 
        public byte[] GetCharacterListInfo(byte[] _data, ClientPeer _client, MainServer _server)
        {
            List<CharacterInfo> info = mCharacter.GetCharacterInfoByAccountId(_client.MySQLConn, _client.GetCurAccount.AccountId);

            if (info.Count > 0)
            {
                return MessageTool.ProtoBufDataSerialize(info);
            }
            else
            {
                CharacterInfo infoInit = new CharacterInfo()
                {
                    AccountId = _client.GetCurAccount.AccountId,
                    CharacterId = 1001,
                    CurExp = 0,
                    CurStrength = 0,
                    Level = 1,
                    Name = "默认名称",
                    Coin = 0
                };
                info = new List<CharacterInfo>();
                info.Add(infoInit);
                mCharacter.UpdateOrAddCharacter(_client.MySQLConn, infoInit, true);
                return MessageTool.ProtoBufDataSerialize(info);
            }
        }
        /// <summary>
        /// 更新人物信息
        /// </summary>
        public byte[] UpdateCharacterListInfo(byte[] _data, ClientPeer _client, MainServer _server)
        {
            CharacterInfo receive = MessageTool.ProtoBufDataDeSerialize<CharacterInfo>(_data);
            CharacterInfo info = mCharacter.UpdateOrAddCharacter(_client.MySQLConn, receive, false);

            MSGCallBack send = null;
            if (info != null)
            {
                send = new MSGCallBack(ReturnCode.Success);
            }
            else
            {
                send = new MSGCallBack(ReturnCode.Fail);
            }
            return MessageTool.ProtoBufDataSerialize(send);
        }

        public byte[] GetImgInfo(byte[] _data, ClientPeer _client, MainServer _server)
        {
            ImgInfo img = mImgDao.GetImgInfo(_client.MySQLConn, _client.GetCurAccount.AccountId);

            if (img == null)
            {
                img = new ImgInfo()
                {
                    AccountId = _client.GetCurAccount.AccountId,
                    ImgId = 1001,
                    ImgPath = _client.ImgPathDic[1001]
                };
                mImgDao.AddOrUpdateImgInfo(_client.MySQLConn, img, true);
            }
            return MessageTool.ProtoBufDataSerialize(img);
        }
        public byte[] UpdateImgInfo(byte[] _data, ClientPeer _client, MainServer _server)
        {
            ImgInfo receive = MessageTool.ProtoBufDataDeSerialize<ImgInfo>(_data);
            receive = new ImgInfo()
            {
                AccountId = _client.GetCurAccount.AccountId,
                ImgId = receive.ImgId,
                ImgPath = _client.ImgPathDic[receive.ImgId]
            };
            ImgInfo send = mImgDao.AddOrUpdateImgInfo(_client.MySQLConn, receive, false);
            return MessageTool.ProtoBufDataSerialize(send);
        }

        public byte[] UpdateMove(byte[] _data, ClientPeer _client, MainServer _server)
        {
            _server.BroadcastMessage(_client,ActionCode.UpdateMove, _data);
            return null;
        }

    }
}
