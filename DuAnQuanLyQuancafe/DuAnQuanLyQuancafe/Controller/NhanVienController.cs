using DuAnQuanLyQuancafe.function;
using DuAnQuanLyQuancafe.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.Controller
{
    internal class NhanVienController
    {
        public static string GetNextMaNhanVien()
        {
            string maNV = "";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT dbo.GenerateMaNhanVien()", conn);
                maNV = cmd.ExecuteScalar().ToString();
            }
            return maNV;
        }

        public List<NhanVienModel> LaydanhsachNhanVien()
        {
            List<NhanVienModel> NhanVien = new List<NhanVienModel>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = "SELECT nv.MaNV, nv.TenNV, nv.DiaChi, nv.GioiTinh, nv.NgaySinh, nv.SDT, nv.MaQue, nv.HinhAnh, q.TenQue " +
                               "FROM NhanVien AS nv JOIN Que AS q ON nv.MaQue = q.MaQue";

                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NhanVien.Add(new NhanVienModel
                        {
                            MaNV = reader["MaNV"].ToString(),
                            TenNV = reader["TenNV"].ToString(),
                            DiaChi = reader["DiaChi"] as string,
                            GioiTinh = reader["GioiTinh"] as string,
                            NgaySinh = reader["NgaySinh"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["NgaySinh"]),
                            SDT = reader["SDT"] as string,
                            MaQue = reader["MaQue"].ToString(),
                            TenQue = reader["TenQue"].ToString(), // Lấy tên quê từ kết quả JOIN
                            Anh = reader["HinhAnh"] == DBNull.Value ? null : (byte[])reader["HinhAnh"]
                        });
                    }
                }
            }

            return NhanVien;
        }


        public static List<QueModel> LayMaQue()
        {
            List<QueModel> queList = new List<QueModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == ConnectionState.Closed)
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
            return queList;
        }

        public static void ThemNhanVien(Hashtable parameter)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    string query = "INSERT INTO NhanVien (MaNV, TenNV, DiaChi, GioiTinh, NgaySinh, SDT, MaQue, HinhAnh) " +
                "VALUES (@MaNV, @TenNV, @DiaChi, @GioiTinh, @NgaySinh, @SDT, @MaQue, @Anh)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", parameter["MaNV"]);
                        cmd.Parameters.AddWithValue("@TenNV", parameter["TenNV"]);
                        cmd.Parameters.AddWithValue("@DiaChi", parameter["DiaChi"]);
                        cmd.Parameters.AddWithValue("@GioiTinh", parameter["GioiTinh"]);
                        cmd.Parameters.AddWithValue("@NgaySinh", parameter["NgaySinh"]);
                        cmd.Parameters.AddWithValue("@SDT", parameter["SDT"]);
                        cmd.Parameters.AddWithValue("@MaQue", parameter["MaQue"]);
                        cmd.Parameters.AddWithValue("@Anh", parameter["Anh"] ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        public void SuaNhanVien(Hashtable parameter)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                try
                {
                    string query = @"UPDATE NhanVien 
                                     SET TenNV = @TenNV, DiaChi = @DiaChi, GioiTinh = @GioiTinh, 
                                         NgaySinh = @NgaySinh, SDT = @SDT, MaQue = @MaQue, Anh = @Anh 
                                     WHERE MaNV = @MaNV";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", parameter["MaNV"]);
                        cmd.Parameters.AddWithValue("@TenNV", parameter["TenNV"]);
                        cmd.Parameters.AddWithValue("@DiaChi", parameter["DiaChi"]);
                        cmd.Parameters.AddWithValue("@GioiTinh", parameter["GioiTinh"]);
                        cmd.Parameters.AddWithValue("@NgaySinh", parameter["NgaySinh"]);
                        cmd.Parameters.AddWithValue("@SDT", parameter["SDT"]);
                        cmd.Parameters.AddWithValue("@MaQue", parameter["MaQue"]);
                        cmd.Parameters.AddWithValue("@Anh", parameter["Anh"] ?? DBNull.Value);
                        
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy nhân viên để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        public void XoaNhanVien(string maNV)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                cmd.ExecuteNonQuery();
            }
        }

        public List<NhanVienModel> TimKiemNhanVien(string tuKhoa)
        {
            List<NhanVienModel> danhSachNhanVien = LaydanhsachNhanVien();
            return danhSachNhanVien
                   .Where(nv => nv.TenNV.ToLower().Contains(tuKhoa.ToLower()))
                   .ToList();
        }
    }
}
