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

    class InventoryController : BaseController
    {
        private InventoryDAO mInv;
        private bool isAdded = false;
        public InventoryController()
        {
            requestCode = RequestCode.Inventory;
            mInv = new InventoryDAO();
        }

        public byte[] GetInventoryListInfo(byte[] _data, ClientPeer _client, MainServer _server)
        {
            List<InventoryInfo> receive = mInv.GetInventoryListInfo(_client.MySQLConn, _client.GetCurAccount.AccountId);

            if (receive.Count > 0)
            {
                return MessageTool.ProtoBufDataSerialize(receive);
            }
            MSGCallBack send = new MSGCallBack(ReturnCode.Fail);
            return MessageTool.ProtoBufDataSerialize(send);
        }

        public byte[] UpdateInventoryListInfo(byte[] _data, ClientPeer _client, MainServer _server)
        {
            List<InventoryInfo> inv = MessageTool.ProtoBufDataDeSerialize<List<InventoryInfo>>(_data);
            MSGCallBack send = null;
            if (!isAdded)
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    mInv.AddOrUpdateInventoryInfo(_client.MySQLConn, inv[i], true);
                }
                isAdded = true;
                send = new MSGCallBack(ReturnCode.Success);
            }
            else
            {
                for (int i = 0; i < inv.Count; i++)
                {
                    mInv.AddOrUpdateInventoryInfo(_client.MySQLConn, inv[i], false);
                }
                send = new MSGCallBack(ReturnCode.Success);
            }
            return MessageTool.ProtoBufDataSerialize(send);
        }
    }
}
