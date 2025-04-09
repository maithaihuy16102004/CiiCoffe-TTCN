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
    internal class LoaiController
    {
        public static List<LoaiModel> LayDanhSachLoai()
        {
            List<LoaiModel> loaiList = new List<LoaiModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }   
                string query = "SELECT MaLoai, TenLoai FROM Loai";
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        loaiList.Add(new LoaiModel
                        {
                            MaLoai = Convert.ToInt32(reader["MaLoai"]),
                            TenLoai = reader["TenLoai"].ToString()
                        });
                    }
                }
            }
            return loaiList;
        }
    }
}
