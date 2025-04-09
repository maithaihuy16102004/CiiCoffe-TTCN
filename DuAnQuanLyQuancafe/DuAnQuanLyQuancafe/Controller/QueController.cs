using DuAnQuanLyQuancafe.function;
using DuAnQuanLyQuancafe.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.Controller
{
    internal class QueController
    {
        public static List<QueModel> LayMaQue()
        {
            List<QueModel> queList = new List<QueModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();
                    string query = "SELECT MaQue, TenQue FROM Que";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            queList.Add(new QueModel
                            {
                                MaQue = reader["MaQue"].ToString(),
                                TenQue = reader["TenQue"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
                finally
                {
                    // Đảm bảo kết nối được đóng trong trường hợp có lỗi
                    if (conn.State == System.Data.ConnectionState.Open)
                        conn.Close();
                }
            }
            return queList;
        }

    }
}
