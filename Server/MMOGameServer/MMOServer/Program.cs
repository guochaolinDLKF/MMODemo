using MMOServer.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MMOServer
{
    class Program
    {
        static void Main(string[] args)
        {
            MainServer server = new MainServer(IPAddress.Parse("127.0.0.1"), 64555, 1000);
            server.Start();
            Console.ReadKey();
        }
    }
}
