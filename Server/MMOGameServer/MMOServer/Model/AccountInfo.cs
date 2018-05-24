using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMOServer.Model
{
    [ProtoContract]
    class AccountInfo
    {
        [ProtoMember(1)]
        public string AccountId { get; set; }
        [ProtoMember(2)]
        public string AccountName { get; set; }
        [ProtoMember(3)]
        public string Password { get; set; }
        [ProtoMember(4)]
        public int ImgId { get; set; }
        [ProtoMember(5)]
        public string ImgPath { get; set; }
    }
}
