using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMOServer.Model;
using MySql.Data.MySqlClient;

namespace MMOServer.DAO
{
    class RoomDAO
    {
        public List<RoomInfo> GetRoomListInfo(MySqlConnection _conn)
        {
            MySqlCommand cmd = new MySqlCommand("select * from roominfo", _conn);

            MySqlDataReader reader = cmd.ExecuteReader();
            List<RoomInfo> infoList=new List<RoomInfo>();
            while (reader.Read())
            {
                int roomId = reader.GetInt32("roonid");
                string roomname = reader.GetString("roomname");
                int playernum = reader.GetInt32("playernum");
                int totalnum = reader.GetInt32("totalnum");
                RoomInfo info = new RoomInfo()
                {
                    RoomId = roomId,
                    PlayerNum = playernum,
                    RoomName = roomname,
                    TotalNum = totalnum
                };
                infoList.Add(info);
            }
            reader.Close();
            return infoList;
        }
    }
}
