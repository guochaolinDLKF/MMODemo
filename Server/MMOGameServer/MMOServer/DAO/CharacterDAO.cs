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
        public CharacterInfo GetCharacterInfoByAccountId(MySqlConnection _conn, string _accountId)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from user where accountid = @accountid", conn);
                cmd.Parameters.AddWithValue("accountid", _accountId);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int characterid = reader.GetInt32("chracterid");
                    string name = reader.GetString("name");
                    int level = reader.GetInt32("level");
                    int curexp = reader.GetInt32("curexp");
                    int curstrength = reader.GetInt32("curstrength");
                    CharacterInfo info=new CharacterInfo()
                    {
                        AccountId = _accountId,CharacterId = characterid,
                        CurExp = curexp,CurStrength = curstrength,Level = level,
                        Name = name
                    };
                    return info;
                }
                else
                {
                    CharacterInfo info = new CharacterInfo()
                    {
                        AccountId = _accountId,
                        CharacterId = 1001,
                        CurExp = 0,
                        CurStrength = 0,
                        Level = 1,
                        Name = "默认名称"
                    };
                    AddCharacter(_conn, info);
                    return info;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在GetAccountByAccountId的时候出现异常：" + e);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return null;
        }

        public void AddCharacter(MySqlConnection conn,CharacterInfo info )
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into accountinfo set" +
                                                    " accountId = @accountId ," +
                                                    " characterid = @characterid ," +
                                                    " curexp = @curexp ," +
                                                    " curstrength = @curstrength ," +
                                                    "level = @level ,"+
                                                    "name = @name", conn);
                cmd.Parameters.AddWithValue("accountId", info.AccountId);
                cmd.Parameters.AddWithValue("characterid", info.CharacterId);
                cmd.Parameters.AddWithValue("curexp", info.CurExp);
                cmd.Parameters.AddWithValue("curstrength", info.CurStrength);
                cmd.Parameters.AddWithValue("level", info.Level);
                cmd.Parameters.AddWithValue("name", info.Name); 
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("在AddUser的时候出现异常：" + e);
            }
        }
    }
}
