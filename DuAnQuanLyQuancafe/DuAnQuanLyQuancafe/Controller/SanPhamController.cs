using DuAnQuanLyQuancafe.function;
using DuAnQuanLyQuancafe.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.Controller
{
    internal class SanPhamController
    {
        byte[] anh = null;
        public List<SanPhamModel> LayDanhSachSanPham()
        {
            List<SanPhamModel> SanPham = new List<SanPhamModel>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = "SELECT * FROM SanPham";

                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SanPham.Add(new SanPhamModel
                        {
                            MaSP = reader["MaSP"].ToString(),
                            TenSP = reader["TenSP"]?.ToString(),
                            MaLoai = reader["MaLoai"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MaLoai"]),
                            GiaNhap = reader["GiaNhap"] == DBNull.Value ? 0 : Convert.ToSingle(reader["GiaNhap"]),
                            GiaBan = reader["GiaBan"] == DBNull.Value ? 0 : Convert.ToSingle(reader["GiaBan"]),
                            SoLuong = reader["SoLuong"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SoLuong"]),
                            MaCongDung = reader["MaCongDung"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MaCongDung"]),
                            HinhAnh = reader["HinhAnh"] == DBNull.Value ? null : (byte[])reader["HinhAnh"]
                        });
                    }
                }
            }
            return SanPham;
        }
        public void Themsanpham(Hashtable parameter)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string query = "INSERT INTO SanPham (MaSP, TenSP, MaLoai, GiaNhap, GiaBan, HinhAnh, SoLuong, MaCongDung) VALUES (@MaSP, @TenSP, @MaLoai, @GiaNhap, @GiaBan, @HinhAnh, @SoLuong, @MaCongDung)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", parameter["MaSP"] ?? DBNull.Value); // Thay thế 1 bằng giá trị thực tế
                        cmd.Parameters.AddWithValue("@TenSP", parameter["TenSP"]);
                        cmd.Parameters.AddWithValue("@MaLoai", parameter["MaLoai"]);
                        cmd.Parameters.AddWithValue("@GiaNhap", parameter["GiaNhap"]);
                        cmd.Parameters.AddWithValue("@GiaBan", parameter["GiaBan"]);
                        cmd.Parameters.AddWithValue("@HinhAnh", parameter["HinhAnh"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SoLuong", parameter["SoLuong"]);
                        cmd.Parameters.AddWithValue("@MaCongDung", parameter["MaCongDung"]);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message);
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
        public void XoaSanPham(string MaSP)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string query = "DELETE FROM SanPham WHERE MaSP = @MaSP";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", MaSP); // Thay thế 1 bằng giá trị thực tế
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa sản phẩm: " + ex.Message);
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
    }
}
