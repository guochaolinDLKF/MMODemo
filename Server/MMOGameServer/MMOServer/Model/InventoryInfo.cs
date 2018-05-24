using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMOServer.Model
{
    [ProtoContract]
    class InventoryInfo
    {
        [ProtoMember(1)]
        public int InventoryId { get; set; }
        [ProtoMember(2)]
        public int BuyPrice { get; set; }
        [ProtoMember(3)]
        public int ShellPrice { get; set; }
        [ProtoMember(4)]
        public string Des { get; set; }
    }
}
