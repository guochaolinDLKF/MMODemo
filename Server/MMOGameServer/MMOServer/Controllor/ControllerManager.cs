using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMOServer.Servers;
using System.Reflection;

namespace MMOServer.Controllor
{
    class ControllerManager
    {
        private Dictionary<RequestCode, BaseController> controllerDict = new Dictionary<RequestCode, BaseController>();
        private MainServer mServer;

        public ControllerManager(MainServer server)
        {
            this.mServer = server;
            InitController();
        }

        void InitController()
        {
            DefaultController defaultController = new DefaultController();
            controllerDict.Add(defaultController.RequestCode, defaultController);
            //controllerDict.Add(RequestCode.Account, new UserController());
            //controllerDict.Add(RequestCode.Room, new RoomController());
            //controllerDict.Add(RequestCode.Game, new GameController());
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, byte[] data, ClientPeer client)
        {
            BaseController controller;
            bool isGet = controllerDict.TryGetValue(requestCode, out controller);
            if (isGet == false)
            {
                Console.WriteLine("无法得到[" + requestCode + "]所对应的Controller,无法处理请求"); return;
            }
            string methodName = Enum.GetName(typeof(ActionCode), actionCode);
            MethodInfo mi = controller.GetType().GetMethod(methodName);
            if (mi == null)
            {
                Console.WriteLine("[警告]在Controller[" + controller.GetType() + "]中没有对应的处理方法:[" + methodName + "]"); return;
            }
            object[] parameters = new object[] { data, client, mServer };
            object o = mi.Invoke(controller, parameters);
            if (o == null )
            {
                return;
            }
            mServer.SendResponse(client, actionCode, o as byte[]); 
        }
    }
}
