using DuAnQuanLyQuancafe.function;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.Model
{
    public class NhanVienModel
    {
        public string MaNV { get; set; }              // Mã nhân viên (Khóa chính)
        public string TenNV { get; set; }             // Tên nhân viên (Mặc định không null)
        public string DiaChi { get; set; }            // Địa chỉ (Có thể NULL)
        public string GioiTinh { get; set; }          // Giới tính (Nam/Nữ)
        public DateTime NgaySinh { get; set; }        // Ngày sinh (Có thể NULL)
        public string SDT { get; set; }               // Số điện thoại (Có thể NULL)
        public string TenQue { get; set; }            // Tên quê (Có thể NULL)
        public string MaQue { get; set; }             // Mã quê (Có thể NULL)
        public byte[] Anh { get; set; }               // Ảnh nhân viên (Kiểu varbinary trong SQL)

        // Lấy mã nhân viên tiếp theo
        public static string GetNextMaNhanVien()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT dbo.GenerateMaNhanVien()", conn))
                    {
                        return cmd.ExecuteScalar()?.ToString() ?? "NV001";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy mã nhân viên tiếp theo: {ex.Message}");
            }
        }

        // Lấy danh sách nhân viên
        public static List<NhanVienModel> LayDanhSachNhanVien()
        {
            List<NhanVienModel> dsNhanVien = new List<NhanVienModel>();
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    string query = @"SELECT NV.MaNV, NV.TenNV, NV.DiaChi, NV.GioiTinh, NV.NgaySinh, NV.SDT, NV.MaQue, NV.HinhAnh, Q.TenQue 
                            FROM NhanVien NV 
                            LEFT JOIN Que Q ON NV.MaQue = Q.MaQue";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                NhanVienModel nhanVien = new NhanVienModel
                                {
                                    MaNV = reader["MaNV"]?.ToString(),
                                    TenNV = reader["TenNV"]?.ToString(),
                                    DiaChi = reader["DiaChi"]?.ToString(),
                                    GioiTinh = reader["GioiTinh"]?.ToString(),
                                    NgaySinh = reader["NgaySinh"] != DBNull.Value ? Convert.ToDateTime(reader["NgaySinh"]) : DateTime.Now,
                                    SDT = reader["SDT"]?.ToString(),
                                    MaQue = reader["MaQue"].ToString(), // Xử lý NULL
                                    TenQue = reader["TenQue"]?.ToString(),
                                    Anh = reader["HinhAnh"] != DBNull.Value ? (byte[])reader["HinhAnh"] : null
                                };
                                dsNhanVien.Add(nhanVien);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Lỗi cơ sở dữ liệu khi lấy danh sách nhân viên: {ex.Message}");
            }
            return dsNhanVien;
        }

        // Thêm nhân viên
        public static (bool Success, string ErrorMessage) ThemNhanVien(Hashtable parameter)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    string query = "INSERT INTO NhanVien (MaNV, TenNV, DiaChi, GioiTinh, NgaySinh, SDT, MaQue, HinhAnh) " +
                                  "VALUES (@MaNV, @TenNV, @DiaChi, @GioiTinh, @NgaySinh, @SDT, @MaQue, @Anh)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", parameter["MaNV"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TenNV", parameter["TenNV"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiaChi", parameter["DiaChi"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@GioiTinh", parameter["GioiTinh"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@NgaySinh", parameter["NgaySinh"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SDT", parameter["SDT"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MaQue", parameter["MaQue"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Anh", parameter["Anh"] ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                        return (true, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi thêm nhân viên: {ex.Message}");
            }
        }

        // Sửa nhân viên
        public static void SuaNhanVien(Hashtable parameter)
        {
            // Kiểm tra dữ liệu đầu vào
            if (parameter["MaNV"] == null || parameter["MaNV"].ToString().Trim().Length == 0)
                throw new ArgumentException("Mã nhân viên không hợp lệ.");

            if (parameter["TenNV"] == null || parameter["TenNV"].ToString().Trim().Length == 0)
                throw new ArgumentException("Tên nhân viên không được để trống.");

            if (parameter["DiaChi"] == null || parameter["DiaChi"].ToString().Trim().Length == 0)
                throw new ArgumentException("Địa chỉ không được để trống.");

            if (parameter["SDT"] == null || parameter["SDT"].ToString().Trim().Length == 0)
                throw new ArgumentException("Số điện thoại không được để trống.");

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    string query = @"UPDATE NhanVien 
                                    SET TenNV = @TenNV, DiaChi = @DiaChi, GioiTinh = @GioiTinh, 
                                        NgaySinh = @NgaySinh, SDT = @SDT, MaQue = @MaQue, HinhAnh = @Anh 
                                    WHERE MaNV = @MaNV";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", parameter["MaNV"].ToString());
                        cmd.Parameters.AddWithValue("@TenNV", parameter["TenNV"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@DiaChi", parameter["DiaChi"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@GioiTinh", parameter["GioiTinh"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@NgaySinh", parameter["NgaySinh"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SDT", parameter["SDT"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MaQue", parameter["MaQue"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Anh", parameter["HinhAnh"] ?? DBNull.Value);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                            throw new Exception("Không tìm thấy nhân viên để cập nhật.");
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception($"Lỗi cơ sở dữ liệu khi sửa nhân viên: {ex.Message}");
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        // Xóa nhân viên
        public static (bool Success, string ErrorMessage) XoaNhanVien(string maNV)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            return (true, null);
                        return (false, "Không tìm thấy nhân viên để xóa.");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi xóa nhân viên: {ex.Message}");
            }
        }
    }
}