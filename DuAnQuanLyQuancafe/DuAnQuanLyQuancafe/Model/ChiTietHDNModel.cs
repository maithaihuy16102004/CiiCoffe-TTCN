using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DuAnQuanLyQuancafe.function;

namespace DuAnQuanLyQuancafe.Model
{
    internal class ChiTietHDNModel
    {
        public string MaCTHDN { get; set; }
        public string MaHDN { get; set; }
        public string MaSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }

        // Constructor mặc định
        public ChiTietHDNModel()
        {
            MaCTHDN = string.Empty;
            MaHDN = string.Empty;
            MaSP = string.Empty;
            SoLuong = 0;
            DonGia = 0;
            ThanhTien = 0;
        }

        // Constructor với tham số (loại bỏ ThanhTien vì sẽ tính tự động)
        public ChiTietHDNModel(string maCTHDN, string maHDN, string maSP, int soLuong, decimal donGia)
        {
            MaCTHDN = maCTHDN ?? string.Empty;
            MaHDN = maHDN ?? string.Empty;
            MaSP = maSP ?? string.Empty;
            SoLuong = soLuong;
            DonGia = donGia;

            ThanhTien = soLuong * donGia; // Tính tự động
        }

        /// <summary>
        /// Lấy danh sách chi tiết hóa đơn nhập theo mã hóa đơn nhập (READ)
        /// </summary>
        public static List<ChiTietHDNModel> LayChiTietHDNTheoMa(string maHDN)
        {
            List<ChiTietHDNModel> danhSach = new List<ChiTietHDNModel>();

            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(maHDN))
                    {
                        MessageBox.Show("Mã hóa đơn nhập không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return danhSach;
                    }

                    // Kiểm tra MaHDN tồn tại
                    string checkHDN = "SELECT COUNT(*) FROM HoaDonNhap WHERE MaHDN = @MaHDN";
                    if (!DatabaseHelper.CheckKey(checkHDN, new SqlParameter[] { new SqlParameter("@MaHDN", maHDN) }))
                    {
                        MessageBox.Show($"Mã hóa đơn nhập '{maHDN}' không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return danhSach;
                    }

                    string query = "SELECT * FROM ChiTietHDN WHERE MaHDN = @MaHDN";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaHDN", maHDN);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var ct = new ChiTietHDNModel
                                {
                                    MaCTHDN = reader["MaCTHDN"]?.ToString() ?? string.Empty,
                                    MaHDN = reader["MaHDN"]?.ToString() ?? string.Empty,
                                    MaSP = reader["MaSP"]?.ToString() ?? string.Empty,
                                    SoLuong = reader["SoLuong"] != DBNull.Value ? Convert.ToInt32(reader["SoLuong"]) : 0,
                                    DonGia = reader["DonGia"] != DBNull.Value ? Convert.ToDecimal(reader["DonGia"]) : 0,
                                    ThanhTien = reader["ThanhTien"] != DBNull.Value ? Convert.ToDecimal(reader["ThanhTien"]) : 0,
                                    
                                };
                                danhSach.Add(ct);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi khi truy vấn cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return danhSach;
                }
            }

            return danhSach;
        }

        /// <summary>
        /// Thêm một chi tiết hóa đơn nhập mới (CREATE)
        /// </summary>
        public static bool ThemChiTietHDN(ChiTietHDNModel chiTiet)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (chiTiet == null || string.IsNullOrWhiteSpace(chiTiet.MaCTHDN) || string.IsNullOrWhiteSpace(chiTiet.MaHDN) || string.IsNullOrWhiteSpace(chiTiet.MaSP))
                    {
                        MessageBox.Show("Mã chi tiết HĐN, mã HĐN hoặc mã sản phẩm không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (chiTiet.SoLuong <= 0)
                    {
                        MessageBox.Show("Số lượng phải lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    // Kiểm tra MaCTHDN trùng
                    string checkMaCTHDN = "SELECT COUNT(*) FROM ChiTietHDN WHERE MaCTHDN = @MaCTHDN";
                    if (DatabaseHelper.CheckKey(checkMaCTHDN, new SqlParameter[] { new SqlParameter("@MaCTHDN", chiTiet.MaCTHDN) }))
                    {
                        MessageBox.Show($"Mã chi tiết HĐN '{chiTiet.MaCTHDN}' đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    // Kiểm tra MaHDN tồn tại
                    string checkHDN = "SELECT COUNT(*) FROM HoaDonNhap WHERE MaHDN = @MaHDN";
                    if (!DatabaseHelper.CheckKey(checkHDN, new SqlParameter[] { new SqlParameter("@MaHDN", chiTiet.MaHDN) }))
                    {
                        MessageBox.Show($"Mã hóa đơn nhập '{chiTiet.MaHDN}' không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    // Kiểm tra MaSP tồn tại
                    string checkSP = "SELECT COUNT(*) FROM SanPham WHERE MaSP = @MaSP";
                    if (!DatabaseHelper.CheckKey(checkSP, new SqlParameter[] { new SqlParameter("@MaSP", chiTiet.MaSP) }))
                    {
                        MessageBox.Show($"Mã sản phẩm '{chiTiet.MaSP}' không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    // Tính ThanhTien
                    chiTiet.ThanhTien = chiTiet.SoLuong * chiTiet.DonGia;
                    if (chiTiet.ThanhTien < 0)
                    {
                        MessageBox.Show("Thành tiền không được âm. Vui lòng kiểm tra khuyến mãi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    string query = "INSERT INTO ChiTietHDN (MaCTHDN, MaHDN, MaSP, SoLuong, DonGia, ThanhTien, KhuyenMai) " +
                                   "VALUES (@MaCTHDN, @MaHDN, @MaSP, @SoLuong, @DonGia, @ThanhTien, @KhuyenMai)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCTHDN", chiTiet.MaCTHDN);
                        command.Parameters.AddWithValue("@MaHDN", chiTiet.MaHDN);
                        command.Parameters.AddWithValue("@MaSP", chiTiet.MaSP);
                        command.Parameters.AddWithValue("@SoLuong", chiTiet.SoLuong);
                        command.Parameters.AddWithValue("@DonGia", chiTiet.DonGia);
                        command.Parameters.AddWithValue("@ThanhTien", chiTiet.ThanhTien);
                        

                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi khi thêm chi tiết hóa đơn nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Cập nhật thông tin chi tiết hóa đơn nhập (UPDATE)
        /// </summary>
        public static bool CapNhatChiTietHDN(ChiTietHDNModel chiTiet)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (chiTiet == null || string.IsNullOrWhiteSpace(chiTiet.MaCTHDN))
                    {
                        MessageBox.Show("Mã chi tiết hóa đơn nhập không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (chiTiet.SoLuong <= 0)
                    {
                        MessageBox.Show("Số lượng phải lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (chiTiet.DonGia < 0 )
                    {
                        MessageBox.Show("Đơn giá không âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    // Kiểm tra MaHDN tồn tại (nếu có)
                    if (!string.IsNullOrWhiteSpace(chiTiet.MaHDN))
                    {
                        string checkHDN = "SELECT COUNT(*) FROM HoaDonNhap WHERE MaHDN = @MaHDN";
                        if (!DatabaseHelper.CheckKey(checkHDN, new SqlParameter[] { new SqlParameter("@MaHDN", chiTiet.MaHDN) }))
                        {
                            MessageBox.Show($"Mã hóa đơn nhập '{chiTiet.MaHDN}' không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    // Kiểm tra MaSP tồn tại (nếu có)
                    if (!string.IsNullOrWhiteSpace(chiTiet.MaSP))
                    {
                        string checkSP = "SELECT COUNT(*) FROM SanPham WHERE MaSP = @MaSP";
                        if (!DatabaseHelper.CheckKey(checkSP, new SqlParameter[] { new SqlParameter("@MaSP", chiTiet.MaSP) }))
                        {
                            MessageBox.Show($"Mã sản phẩm '{chiTiet.MaSP}' không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    // Tính ThanhTien
                    chiTiet.ThanhTien = chiTiet.SoLuong * chiTiet.DonGia;
                    if (chiTiet.ThanhTien < 0)
                    {
                        MessageBox.Show("Thành tiền không được âm. Vui lòng kiểm tra khuyến mãi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    string query = "UPDATE ChiTietHDN SET MaHDN = @MaHDN, MaSP = @MaSP, SoLuong = @SoLuong, DonGia = @DonGia, ThanhTien = @ThanhTien, KhuyenMai = @KhuyenMai " +
                                   "WHERE MaCTHDN = @MaCTHDN";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCTHDN", chiTiet.MaCTHDN);
                        command.Parameters.AddWithValue("@MaHDN", chiTiet.MaHDN ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MaSP", chiTiet.MaSP ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SoLuong", chiTiet.SoLuong);
                        command.Parameters.AddWithValue("@DonGia", chiTiet.DonGia);
                        command.Parameters.AddWithValue("@ThanhTien", chiTiet.ThanhTien);


                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật chi tiết hóa đơn nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Xóa chi tiết hóa đơn nhập theo mã chi tiết (DELETE)
        /// </summary>
        public static bool XoaChiTietHDN(string maCTHDN)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(maCTHDN))
                    {
                        MessageBox.Show("Mã chi tiết hóa đơn nhập không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    // Kiểm tra MaCTHDN tồn tại
                    string checkMaCTHDN = "SELECT COUNT(*) FROM ChiTietHDN WHERE MaCTHDN = @MaCTHDN";
                    if (!DatabaseHelper.CheckKey(checkMaCTHDN, new SqlParameter[] { new SqlParameter("@MaCTHDN", maCTHDN) }))
                    {
                        MessageBox.Show($"Mã chi tiết HĐN '{maCTHDN}' không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    string query = "DELETE FROM ChiTietHDN WHERE MaCTHDN = @MaCTHDN";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaCTHDN", maCTHDN);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi khi xóa chi tiết hóa đơn nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}