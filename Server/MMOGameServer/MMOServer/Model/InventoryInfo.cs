﻿using ProtoBuf;
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
        public string AccountId { get; set; }
        [ProtoMember(2)]
        public int InventoryId { get; set; }
        [ProtoMember(3)]
        public int HaveNum { get; set; }
    }
}
