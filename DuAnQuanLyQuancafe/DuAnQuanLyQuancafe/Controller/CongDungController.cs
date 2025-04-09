using DuAnQuanLyQuancafe.function;
using DuAnQuanLyQuancafe.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAnQuanLyQuancafe.Controller
{
    internal class CongDungController
    {
        public static List<CongDungModel> LayMaCongDung()
        {

            List<CongDungModel> congDungList = new List<CongDungModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();
                    string query = "SELECT MaCongDung, TenCongDung FROM CongDung";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            congDungList.Add(new CongDungModel
                            {
                                MaCongDung = Convert.ToInt32(reader["MaCongDung"]),
                                TenCongDung = reader["TenCongDung"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                    return congDungList;
                }
                finally
                {
                    // Đảm bảo kết nối được đóng trong trường hợp có lỗi
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                }
                return congDungList;
            }
        }
    }
}
