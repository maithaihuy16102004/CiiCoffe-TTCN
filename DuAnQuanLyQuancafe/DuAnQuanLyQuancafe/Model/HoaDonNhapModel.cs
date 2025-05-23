using DuAnQuanLyQuancafe.function;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.Model
{
    internal class HoaDonNhapModel
    {
        public string MaHDN { get; set; }
        public DateTime NgayNhap { get; set; }
        public string MaNV { get; set; }
        public string MaNCC { get; set; }
        public decimal TongTien { get; set; }

        public string TenNV { get; set; }

        public string TenNCC { get; set; }
        // Constructor mặc định
        public HoaDonNhapModel()
        {
            NgayNhap = DateTime.MinValue;
            TongTien = 0;
        }

        // Constructor với tham số
        public HoaDonNhapModel(string maHDN, DateTime ngayNhap, string maNV, string maNCC, decimal tongTien, string tenNV, string tenNCC)
        {
            MaHDN = maHDN;
            NgayNhap = ngayNhap;
            MaNV = maNV;
            MaNCC = maNCC;
            TongTien = tongTien;
            TenNV = tenNV;
            TenNCC = tenNCC;
        }

        /// <summary>
        /// Lấy mã hóa đơn nhập tiếp theo từ stored procedure dbo.GenerateMaHDN
        /// </summary>
        /// <returns>Mã hóa đơn nhập mới</returns>
        public static string GetNextHoaDonNhap()
        {
            string maHDN = string.Empty;
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SELECT dbo.GenerateMaHDN()", conn);
                    object result = cmd.ExecuteScalar();
                    maHDN = result != DBNull.Value ? result.ToString() : string.Empty;
                    Console.WriteLine($"Mã hóa đơn nhập mới: {maHDN}");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi khi lấy mã hóa đơn nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
            return maHDN;
        }

        /// <summary>
        /// Lấy danh sách tất cả các hóa đơn nhập từ cơ sở dữ liệu
        /// </summary>
        /// <returns>Danh sách các hóa đơn nhập</returns>
        public List<HoaDonNhapModel> LayDanhSachHDN()
        {
            List<HoaDonNhapModel> hdnList = new List<HoaDonNhapModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    string query = "SELECT hdn.MaHDN, hdn.NgayNhap, nv.MaNV, nv.TenNV, ncc.MaNCC, ncc.TenNCC, hdn.TongTien FROM HoaDonNhap hdn JOIN NhanVien nv ON hdn.MaNV = nv.MaNV JOIN NhaCungCap ncc ON hdn.MaNCC = ncc.MaNCC;";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                hdnList.Add(new HoaDonNhapModel
                                {
                                    MaHDN = reader["MaHDN"].ToString(),
                                    NgayNhap = reader["NgayNhap"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["NgayNhap"]),
                                    MaNV = reader["MaNV"].ToString(),
                                    TenNV = reader["TenNV"].ToString(),
                                    MaNCC = reader["MaNCC"].ToString(),
                                    TenNCC = reader["TenNCC"].ToString(),
                                    TongTien = reader["TongTien"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TongTien"])
                                });
                            }
                        }
                    }
                    Console.WriteLine($"Đã tải {hdnList.Count} hóa đơn nhập.");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi khi lấy danh sách hóa đơn nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
            return hdnList;
        }

        /// <summary>
        /// Thêm một hóa đơn nhập mới vào cơ sở dữ liệu
        /// </summary>
        /// <param name="parameter">Hashtable chứa các tham số (MaHDN, NgayNhap, MaNV, MaNCC, TongTien)</param>
        public static void ThemHDN(Hashtable parameter)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    string query = "INSERT INTO HoaDonNhap (MaHDN, NgayNhap, MaNV, MaNCC, TongTien) VALUES (@MaHDN, @NgayNhap, @MaNV, @MaNCC, @TongTien)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHDN", parameter["MaHDN"] ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NgayNhap", parameter["NgayNhap"] ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@MaNV", parameter["MaNV"] ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@MaNCC", parameter["MaNCC"] ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TongTien", parameter["TongTien"] ?? (object)0);
                        cmd.ExecuteNonQuery();
                    }
                    Console.WriteLine($"Đã thêm hóa đơn nhập với mã: {parameter["MaHDN"]}");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi khi thêm hóa đơn nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }

        /// <summary>
        /// Xóa một hóa đơn nhập dựa trên mã hóa đơn. Nếu hóa đơn có chi tiết liên quan thì hiển thị thông báo.
        /// </summary>
        /// <param name="maHDN">Mã hóa đơn cần xóa</param>
        public void XoaHDN(string maHDN)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    string query = "DELETE FROM HoaDonNhap WHERE MaHDN = @MaHDN";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHDN", maHDN);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Đã xóa hóa đơn nhập với mã: {maHDN}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException ex)
                {
                    // Kiểm tra lỗi do ràng buộc khóa ngoại (ví dụ như ChiTietHDN vẫn còn liên kết với MaHDN)
                    if (ex.Number == 547) // Foreign key violation
                    {
                        MessageBox.Show("Không thể xóa hóa đơn nhập vì đang có chi tiết hóa đơn liên quan.\nVui lòng xóa chi tiết trước khi xóa hóa đơn.",
                                        "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi khi xóa hóa đơn nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Có thể ghi log nếu cần
                    // Log.Error("Lỗi khi xóa Hóa Đơn Nhập", ex);

                    // Không throw lại, tránh crash ứng dụng
                }
            }
        }

        /// <summary>
        /// Cập nhật thông tin hóa đơn nhập dựa vào MaHDN
        /// </summary>
        /// <param name="parameter">Hashtable chứa các tham số cần cập nhật</param>
        /// <summary>
        /// Cập nhật thông tin hóa đơn nhập trong cơ sở dữ liệu
        /// </summary>
        /// <param name="parameter">Hashtable chứa các tham số (MaHDN, NgayNhap, MaNV, MaNCC, TongTien)</param>
        public static void CapNhatHDN(Hashtable parameter)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (parameter["MaHDN"] == null || parameter["MaHDN"].ToString().Trim().Length == 0)
                        throw new ArgumentException("Mã hóa đơn nhập không hợp lệ.");

                    string query = "UPDATE HoaDonNhap SET NgayNhap = @NgayNhap, MaNV = @MaNV, MaNCC = @MaNCC, TongTien = @TongTien WHERE MaHDN = @MaHDN";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHDN", parameter["MaHDN"] ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@NgayNhap", parameter["NgayNhap"] ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@MaNV", parameter["MaNV"] ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@MaNCC", parameter["MaNCC"] ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TongTien", parameter["TongTien"] ?? (object)0);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                            throw new Exception("Không tìm thấy hóa đơn nhập để cập nhật.");
                        Console.WriteLine($"Đã cập nhật hóa đơn nhập với mã: {parameter["MaHDN"]}");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật hóa đơn nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }
        /// <summary>
        /// Tìm kiếm hóa đơn nhập dựa trên từ khóa
        /// </summary>
        /// <param name="tuKhoa">Từ khóa tìm kiếm (trong mã hóa đơn)</param>
        /// <returns>Danh sách hóa đơn nhập khớp với từ khóa</returns>
        public List<HoaDonNhapModel> TimKiemHDN(string tuKhoa)
        {
            List<HoaDonNhapModel> danhSachHDN = LayDanhSachHDN();
            return danhSachHDN
                .Where(hdn => hdn.MaHDN.ToLower().Contains(tuKhoa.ToLower()))
                .ToList();
        }
    }
}