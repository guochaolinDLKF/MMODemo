using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMOServer.Model;
using MySql.Data.MySqlClient;

namespace MMOServer.DAO
{
    class ImgDAO
    {
        /// <summary>
        /// 获取图片信息
        /// </summary>
        /// <param name="_conn"></param>
        /// <param name="_accountId"></param>
        /// <returns></returns>
        public ImgInfo GetImgInfo(MySqlConnection _conn,string _accountId)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from imginfo where" +
                                                    " accountId = @accountId", _conn);
                cmd.Parameters.AddWithValue("accountId", _accountId);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int imgid = reader.GetInt32("imgid");
                    string imgPath = reader.GetString("imgpath");
                    ImgInfo user = new ImgInfo()
                    { 
                        ImgId = imgid,
                        AccountId = _accountId,
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

         
        public ImgInfo AddOrUpdateImgInfo(MySqlConnection conn,ImgInfo _imgInfo,bool _isAdd )
        {
            try
            {
                MySqlCommand cmd = null;
                if (_isAdd)
                {
                    cmd = new MySqlCommand("insert into imginfo set" +
                                                        " accountId = @accountId ," +
                                                        " imgid = @imgid ," +
                                                        " imgpath = @imgpath ", conn);
                    cmd.Parameters.AddWithValue("accountId", _imgInfo.AccountId);
                    cmd.Parameters.AddWithValue("imgid", _imgInfo.ImgId);
                    cmd.Parameters.AddWithValue("imgpath", _imgInfo.ImgPath);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd = new MySqlCommand("update imginfo set" +
                                                        " imgpath = @imgpath where" +
                    " imgid = @" + _imgInfo.ImgId, conn);
                    cmd.Parameters.AddWithValue("imgpath", _imgInfo.ImgPath);
                    cmd.ExecuteNonQuery();
                }
                return _imgInfo;
            }
            catch (Exception e)
            {
                Console.WriteLine("在AddOrUpdateImgInfo的时候出现异常：" + e);
            }
            return null;
        }
    }
}
