using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMOServer.Model;
using MySql.Data.MySqlClient;

namespace MMOServer.DAO
{
    class AccountDAO
    {
    /// <summary>
    /// 验证并返回用户
    /// </summary>
    /// <param name="conn"></param>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
        public AccountInfo VerifyUser(MySqlConnection _conn, string _accountName, string _password)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from accountinfo where" +
                                                    " accountname = @accountname and" +
                                                    " password = @password", _conn);
                cmd.Parameters.AddWithValue("accountname", _accountName);
                cmd.Parameters.AddWithValue("password", _password);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string id = reader.GetString("accountid");
                    string imgPath = reader.GetString("imgpath");
                    AccountInfo user = new AccountInfo()
                    {
                        AccountId=id,
                        AccountName = _accountName,
                        Password = _password,
                        ImgPath = imgPath
                    };
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在VerifyUser的时候出现异常：" + e);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return null;
        }  
        /// <summary>
        /// 根据id验证账户是否存在
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="_accountId"></param>
        /// <returns></returns>
        public bool GetAccountByAccountId(MySqlConnection conn, string _accountId)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from accountinfo where accountid = @accountid", conn);
                cmd.Parameters.AddWithValue("accountid", _accountId);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
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
            return false;
        }
        /// <summary>
        /// 添加账户
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="username"></param>
        /// <param name="password"></param> 
        public void AddAccount(MySqlConnection conn,string _accountId, string _accountName, string _password,int _imgId,string _imgPath)
        {
            try   
            { 
                MySqlCommand cmd = new MySqlCommand("insert into accountinfo set" +
                                                    " accountId = @accountId ," +
                                                    " accountName = @accountname ," +
                                                    " password = @password ,"+
                                                    " imgid = @imgid ,"+"imgpath = @imgpath", conn);
                cmd.Parameters.AddWithValue("accountId", _accountId);
                cmd.Parameters.AddWithValue("accountName", _accountName);
                cmd.Parameters.AddWithValue("password", _password);
                cmd.Parameters.AddWithValue("imgid", _imgId);
                cmd.Parameters.AddWithValue("imgpath", _imgPath); 
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("在AddUser的时候出现异常：" + e);
            }
        }
    }
}
