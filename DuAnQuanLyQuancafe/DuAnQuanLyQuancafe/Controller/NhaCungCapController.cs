using DuAnQuanLyQuancafe.function;
using DuAnQuanLyQuancafe.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAnQuanLyQuancafe.Controller
{
    internal class NhaCungCapController
    {
        public List<NhaCungCapModel> LayDanhSachNhaCC()
        {
            List<NhaCungCapModel> NhaCungCap = new List<NhaCungCapModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }
                string query = "SELECT * FROM NhaCungCap";
                SqlCommand command = new SqlCommand(query, conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NhaCungCap.Add(new NhaCungCapModel
                        {
                            MaNCC = reader["MaNCC"].ToString(),
                            TenNCC = reader["TenNCC"].ToString(),
                            DiaChi = reader["DiaChi"].ToString(),
                            SDT = reader["SDT"].ToString(),
                            MoTa = reader["MoTa"] != DBNull.Value ? reader["MoTa"].ToString() : null
                        });
                    }
                }
            }
            return NhaCungCap;
        }
        public void ThemNhaCC(Hashtable parameter)
        {
            List<NhaCungCapModel> dsNhaCC = new List<NhaCungCapModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string query = "INSERT INTO NhaCungCap (MaNCC, TenNCC, DiaChi, SDT, MoTa) VALUES (@MaNCC, @TenNCC, @DiaChi, @SDT, @MoTa)";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@MaNCC", parameter["MaNCC"] ?? DBNull.Value); // Thay thế 1 bằng giá trị thực tế
                    command.Parameters.AddWithValue("@TenNCC", parameter["TenNCC"]);
                    command.Parameters.AddWithValue("@DiaChi", parameter["DiaChi"]);
                    command.Parameters.AddWithValue("@SDT", parameter["SDT"]);
                    command.Parameters.AddWithValue("@MoTa", parameter["MoTa"]);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi thêm nhà cung cấp: " + ex.Message);
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }

            }
        }
        public void XoaNhaCungCap(string ID)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {

                string query = "DELETE FROM NhaCungCap WHERE MaNCC = @MaNCC";
                try
                {
                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@MaNCC", ID);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi kết nối đến cơ sở dữ liệu: " + ex.Message);
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }
    }
}
