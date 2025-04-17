using DuAnQuanLyQuancafe.function;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuAnQuanLyQuancafe.Model;
using System.Collections;

namespace DuAnQuanLyQuancafe.Controller
{
    internal class HoaDonNhapController
    {
        public static string GetNextHoaDonNhap()
        {
            string MaHDN = "";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT dbo.GenerateMaHDN()", conn);
                MaHDN = cmd.ExecuteScalar().ToString();
            }
            return MaHDN;
        }
        public List<HoaDonNhapModel> LayDanhSachHDN()
        {
            List<HoaDonNhapModel> Hdn = new List<HoaDonNhapModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string query = "SELECT * FROM HoaDonNhap";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Hdn.Add(new HoaDonNhapModel
                            {
                                MaHDN = reader["MaHDN"].ToString(),
                                NgayNhap = reader["NgayNhap"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["NgayNhap"]),
                                MaNV = reader["MaNV"].ToString(),
                                MaNCC = reader["MaNCC"].ToString(),
                                TongTien = reader["TongTien"].ToString()
                            });
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Lỗi khi kết nối đến cơ sở dữ liệu: " + ex.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return Hdn;
        }
        public static void themHDN(Hashtable parameter)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string query = "INSERT INTO HoaDonNhap (MaHDN, NgayNhap, MaNV, MaNCC, TongTien) VALUES (@MaHDN, @NgayNhap, @MaNV, @MaNCC, @TongTien)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHDN", parameter["MaHDN"]);
                        cmd.Parameters.AddWithValue("@NgayNhap", parameter["NgayNhap"]);
                        cmd.Parameters.AddWithValue("@MaNV", parameter["MaNV"]);
                        cmd.Parameters.AddWithValue("@MaNCC", parameter["MaNCC"]);
                        cmd.Parameters.AddWithValue("@TongTien", parameter["TongTien"]);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Lỗi khi kết nối đến cơ sở dữ liệu: " + ex.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }
        public void XoaHDN(string MaHDN)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if(conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string query = "DELETE FROM HoaDonNhap WHERE MaHDN = @MaHDN";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHDN", MaHDN);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Lỗi khi kết nối đến cơ sở dữ liệu: " + ex.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                }
            }
        }
        public List<HoaDonNhapModel> TimKiemHDN(string tuKhoa)
        {
            List<HoaDonNhapModel> danhSachHDN = LayDanhSachHDN();
            return danhSachHDN
                .Where(hdn => hdn.MaHDN.ToLower().Contains(tuKhoa.ToLower()))
                .ToList();
        }
    }
}
