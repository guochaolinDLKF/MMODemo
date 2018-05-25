using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public enum ActionCode
    {
        None,
        Login,
        Register,
        GetCharacterListInfo,
        UpdateCharacterListInfo, 
        GetRoomList,
        JoinRoom,
        GetImgInfo,
        UpdateImgInfo,
        GetInventoryListInfo,
        UpdateMove,
        ShowTimer,
        StartPlay,
        SatrtMatchingPlayer,
    }
}
