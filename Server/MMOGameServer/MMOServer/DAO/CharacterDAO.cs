using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMOServer.Model;
using MySql.Data.MySqlClient;

namespace MMOServer.DAO
{
    class CharacterDAO
    {
        public List<CharacterInfo> GetCharacterInfoByAccountId(MySqlConnection _conn, string _accountId)
        {
            MySqlDataReader reader = null;
            List < CharacterInfo > infoList=new List<CharacterInfo>();
            infoList.Clear();
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from accountinfo where accountid = @accountid", _conn);
                cmd.Parameters.AddWithValue("accountid", _accountId);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int characterid = reader.GetInt32("chracterid");
                    string name = reader.GetString("name");
                    int level = reader.GetInt32("level");
                    int curexp = reader.GetInt32("curexp");
                    int curstrength = reader.GetInt32("curstrength");
                    int coin = reader.GetInt32("coin");
                    CharacterInfo info = new CharacterInfo()
                    {
                        AccountId = _accountId,
                        CharacterId = characterid,
                        CurExp = curexp,
                        CurStrength = curstrength,
                        Level = level,
                        Name = name,
                        Coin = coin
                    };
                    infoList.Add(info);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("在GetCharacterInfoByAccountId的时候出现异常：" + e);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return infoList;
        }
         /// <summary>
         /// 更新或者插入人物信息
         /// </summary>
         /// <param name="conn"></param>
         /// <param name="info"></param>
         /// <param name="isAdd"></param>
         /// <returns></returns>
        public CharacterInfo UpdateOrAddCharacter(MySqlConnection conn,CharacterInfo info,bool isAdd )
        {
            try
            {
                MySqlCommand cmd = null;
                if (isAdd)
                {
                    cmd = new MySqlCommand("insert into accountinfo set" +
                                           " accountId = @accountId ," +
                                           " characterid = @characterid ," +
                                           " curexp = @curexp ," +
                                           " curstrength = @curstrength ," +
                                           "level = @level ," +
                                           "name = @name ,coin = @coin", conn);
                    cmd.Parameters.AddWithValue("accountId", info.AccountId);
                }
                else
                {
                    cmd = new MySqlCommand("update accountinfo set" +
                                           " characterid = @characterid ," +
                                           " curexp = @curexp ," +
                                           " curstrength = @curstrength ," +
                                           "level = @level ," +
                                           "name = @name ," +
                                           "coin = @coin where" +
                                           " accountId = @"+ info.AccountId, conn);
                }
                
                cmd.Parameters.AddWithValue("characterid", info.CharacterId);
                cmd.Parameters.AddWithValue("curexp", info.CurExp);
                cmd.Parameters.AddWithValue("curstrength", info.CurStrength);
                cmd.Parameters.AddWithValue("level", info.Level);
                cmd.Parameters.AddWithValue("name", info.Name); 
                cmd.Parameters.AddWithValue("coin", info.Coin); 
                cmd.ExecuteNonQuery();
                return info;
            }
            catch (Exception e)
            {
                Console.WriteLine("在UpdateOrAddCharacter的时候出现异常：" + e);
            }
            return null;
        }
    }
}
