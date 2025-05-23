using System;
using System.Data;
using System.Data.SqlClient;

namespace DuAnQuanLyQuancafe.Model
{
    public class TaiKhoanModel
    {
        private static string connString = "Data Source=DESKTOP-K56JJJ3;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Encrypt=False";

        // Xác thực đăng nhập
        public static (bool Success, string LoaiTaiKhoan, string ErrorMessage) XacThucDangNhap(string maNV, string matKhau)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string sql = "SELECT LoaiTaiKhoan FROM TaiKhoan WHERE LOWER(MaNV) = LOWER(@MaNV) AND MatKhau = @MatKhau";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            return (true, result.ToString().Trim(), null);
                        }
                        return (false, null, "Tài khoản hoặc mật khẩu không đúng.");
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, null, $"Lỗi khi xác thực đăng nhập: {ex.Message}");
            }
        }

        // Lấy thông tin nhân viên
        public static (NhanVienModel NhanVien, string ErrorMessage) LayThongTinNhanVien(string maNV)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string sql = "SELECT MaNV, TenNV, HinhAnh FROM NhanVien WHERE MaNV = @MaNV";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                NhanVienModel nv = new NhanVienModel
                                {
                                    MaNV = reader["MaNV"].ToString(),
                                    TenNV = reader["TenNV"].ToString(),
                                    Anh = reader["HinhAnh"] != DBNull.Value ? (byte[])reader["HinhAnh"] : null
                                };
                                return (nv, null);
                            }
                            return (null, "Không tìm thấy nhân viên.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (null, $"Lỗi khi lấy thông tin nhân viên: {ex.Message}");
            }
        }
    }
}