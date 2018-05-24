using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMOServer.Model
{
    [ProtoContract]
    class CharacterInfo
    {
        [ProtoMember(1)]
        public string AccountId { get; set; }
        [ProtoMember(2)]
        public int CharacterId { get; set; }
        [ProtoMember(3)]
        public string Name { get; set; }
        [ProtoMember(4)]
        public int Level { get; set; }
        [ProtoMember(5)]
        public int CurStrength { get; set; }
        [ProtoMember(6)]
        public int CurExp { get; set; } 
    }
}
