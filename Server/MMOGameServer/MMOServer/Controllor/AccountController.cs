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

        public byte[] Login(string data, ClientPeer client, MainServer server)
        {
            return null;
        }

        public string Register(string data, ClientPeer client, MainServer server)
        {
            return null;
        }
    }
}
