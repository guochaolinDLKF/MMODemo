﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace MMOServer.Model
{
    [ProtoContract]
    class MSGCallBack
    {
        public MSGCallBack() { }
        public MSGCallBack(RequestCode request, ActionCode action, byte[] dataList)
        {
            RequestCode = request;
            ActionCode = action;
            DataList = dataList;
        }
        public MSGCallBack(ActionCode action, byte[] dataList)
        {
            ActionCode = action;
            DataList = dataList;
        }
        public MSGCallBack(ReturnCode returnCode)
        {
            ReturnCode = returnCode; 
        }
        [ProtoMember(1)]
        public RequestCode RequestCode { get; set; }
        [ProtoMember(2)]
        public ActionCode ActionCode { get; set; }
        [ProtoMember(3)]
        public byte[] DataList;
        [ProtoMember(4)]
        public ReturnCode ReturnCode { get; set; }
    }
}
