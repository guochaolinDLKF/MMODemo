using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using MMOServer.Model;
using MySql.Data.MySqlClient;

namespace MMOServer.DAO
{
    class InventoryDAO
    {
        public List<InventoryInfo> GetInventoryListInfo(MySqlConnection _conn, string _accountId)
        {
            MySqlDataReader reader = null;
            List<InventoryInfo> invInfo = new List<InventoryInfo>();
            invInfo.Clear();
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from inventoryinfo where" +
                                                    " accountId = @accountId", _conn);
                cmd.Parameters.AddWithValue("accountId", _accountId);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int invid = reader.GetInt32("inventoryid");
                    int buyprice = reader.GetInt32("buyprice");
                    int shellprice = reader.GetInt32("shellprice");
                    int havenum = reader.GetInt32("havenum");
                    InventoryInfo inv = new InventoryInfo()
                    {
                        AccountId = _accountId,
                        InventoryId = invid,
                        HaveNum = havenum
                    };
                    invInfo.Add(inv);
                    
                }
                return invInfo;
            }
            catch (Exception e)
            {
                Console.WriteLine("在GetInventoryInfo的时候出现异常：" + e);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return invInfo;
        }
         
        public InventoryInfo AddOrUpdateInventoryInfo(MySqlConnection conn, InventoryInfo _invInfo, bool _isAdd)
        {
            try
            {
                MySqlCommand cmd = null;
                if (_isAdd)
                {
                    cmd = new MySqlCommand("insert into inventoryinfo set" +
                                                        " accountId = @accountId ," +
                                                        " inventoryid = @inventoryid ," +
                                                        " havenum = @havenum " , conn);
                    cmd.Parameters.AddWithValue("accountId", _invInfo.AccountId);
                    cmd.Parameters.AddWithValue("inventoryid", _invInfo.InventoryId);
                    cmd.Parameters.AddWithValue("havenum", _invInfo.HaveNum); 
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd = new MySqlCommand("update imginfo set" +
                                                        " havenum = @havenum where" +
                    " inventoryid = @" + _invInfo.InventoryId, conn);
                    cmd.Parameters.AddWithValue("havenum", _invInfo.HaveNum); 
                    cmd.ExecuteNonQuery();
                }
                return _invInfo;
            }
            catch (Exception e)
            {
                Console.WriteLine("在AddOrUpdateInventoryInfo的时候出现异常：" + e);
            }
            return null;
        }
    }
}

