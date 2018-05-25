using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMOServer.Model
{
    [ProtoContract]
    class MoveInfo
    {
        [ProtoMember(1)]
        public float MoveX { get; set; }
        [ProtoMember(2)]
        public float MoveY { get; set; }
        [ProtoMember(3)]
        public float MoveZ { get; set; }
    }
}
