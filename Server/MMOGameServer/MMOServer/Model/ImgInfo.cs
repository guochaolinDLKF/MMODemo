using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMOServer.Model
{
    [ProtoContract]
    class ImgInfo
    {
        [ProtoMember(1)]
        public int ImgId { get; set; }
        [ProtoMember(2)]
        public string ImgPath { get; set; }
        [ProtoMember(3)]
        public string AccountId { get; set; }
    }
}
