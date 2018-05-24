using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using MMOServer.Servers;

namespace MMOServer.Controllor
{
    class AccountController:BaseController
    {
        public AccountController() 
        {
            requestCode = RequestCode.Account;
        }

        public byte[] Login(byte[] data, ClientPeer client, MainServer server)
        {
            return null;
        }

        public byte[] Register(byte[] data, ClientPeer client, MainServer server)
        {
            return null;
        }
    }
}
