using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using MMOServer.DAO;
using MMOServer.Model;
using MMOServer.Servers;
using MMOServer.Tools;

namespace MMOServer.Controllor
{
    class AccountController : BaseController
    {
        private AccountDAO mAccount;

        public AccountController()
        {
            requestCode = RequestCode.Account;
            mAccount = new AccountDAO();
        }

        public byte[] Login(byte[] data, ClientPeer client, MainServer server)
        {
            AccountInfo recive = MessageTool.ProtoBufDataDeSerialize<AccountInfo>(data);
            AccountInfo user = mAccount.VerifyUser(client.MySQLConn, recive.AccountName, recive.Password);

            MSGCallBack msg;
            if (user == null)
            {
                msg = new MSGCallBack(ReturnCode.Fail);
            }
            else
            {
                client.SetCurAccountData(user);
                msg = new MSGCallBack(ReturnCode.Success);
            }
            return MessageTool.ProtoBufDataSerialize(msg);
        }

        public byte[] Register(byte[] data, ClientPeer client, MainServer server)
        {
            AccountInfo recive = MessageTool.ProtoBufDataDeSerialize<AccountInfo>(data);

            bool res = mAccount.GetAccountByAccountId(client.MySQLConn, recive.AccountId);
            MSGCallBack msg;
            if (res)
            {
                msg = new MSGCallBack(ReturnCode.Fail);
            }
            else
            {
                mAccount.AddAccount(client.MySQLConn,recive.AccountId,
                    recive.AccountName,recive.Password,1001,client.ImgPathDic[1001]);
                msg = new MSGCallBack(ReturnCode.Success);
            }
            return MessageTool.ProtoBufDataSerialize(msg);
        }
    }
}

