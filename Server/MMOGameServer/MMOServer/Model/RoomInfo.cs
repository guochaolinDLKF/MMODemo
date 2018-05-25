using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMOServer.Model
{
    [ProtoContract]
    class RoomInfo
    {
        [ProtoMember(1)]
        public int RoomId { get; set; }
        [ProtoMember(2)]
        public string RoomName { get; set; }
        [ProtoMember(3)]
        public int PlayerNum { get; set; }
        [ProtoMember(4)]
        public int TotalNum { get; set; }
    }
}
