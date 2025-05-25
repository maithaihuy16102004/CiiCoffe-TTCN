using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.Model
{
    internal class ChiTietHDNModel
    {
        private readonly string _connectionString = "Data Source=DESKTOP-K56JJJ3;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Encrypt=False";
        public string MaCTHDN { get; private set; } // Không cho phép sửa
        public string MaHDN { get; private set; }   // Không cho phép sửa
        public string MaSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; private set; } // Không cho phép sửa, tự tính

        public ChiTietHDNModel(string maHDN, string maSP, int soLuong, decimal donGia, string maCTHDN = null)
        {
            if (string.IsNullOrEmpty(maHDN)) throw new ArgumentNullException(nameof(maHDN), "Mã hóa đơn nhập không được để trống.");
            if (string.IsNullOrEmpty(maSP)) throw new ArgumentNullException(nameof(maSP), "Mã sản phẩm không được để trống.");
            if (soLuong <= 0) throw new ArgumentException("Số lượng phải lớn hơn 0.", nameof(soLuong));
            if (donGia < 0) throw new ArgumentException("Đơn giá không được âm.", nameof(donGia));

            MaHDN = maHDN;
            MaSP = maSP;
            SoLuong = soLuong;
            DonGia = donGia;
            MaCTHDN = string.IsNullOrEmpty(maCTHDN) ? GenerateMaCTHDN() : maCTHDN;
            TinhThanhTien();
        }

        private void TinhThanhTien()
        {
            ThanhTien = SoLuong * DonGia;
        }

        private string GenerateMaCTHDN()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT dbo.GenerateMaCTHDN()";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        return result?.ToString() ?? throw new Exception("Không thể tạo mã chi tiết hóa đơn tự động.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tạo mã chi tiết hóa đơn: {ex.Message}");
            }
        }

        public bool Them()
        {
            try
            {
                if (CheckKey(MaCTHDN))
                {
                    MessageBox.Show($"Mã chi tiết hóa đơn {MaCTHDN} đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO ChiTietHDN (MaCTHDN, MaHDN, MaSP, SoLuong, DonGia, ThanhTien) " +
                                   "VALUES (@MaCTHDN, @MaHDN, @MaSP, @SoLuong, @DonGia, @ThanhTien)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaCTHDN", MaCTHDN);
                        cmd.Parameters.AddWithValue("@MaHDN", MaHDN);
                        cmd.Parameters.AddWithValue("@MaSP", MaSP);
                        cmd.Parameters.AddWithValue("@SoLuong", SoLuong);
                        cmd.Parameters.AddWithValue("@DonGia", DonGia);
                        cmd.Parameters.AddWithValue("@ThanhTien", ThanhTien);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool CapNhat()
        {
            try
            {
                TinhThanhTien(); // Tính lại ThanhTien trước khi cập nhật

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "UPDATE ChiTietHDN SET MaSP = @MaSP, SoLuong = @SoLuong, DonGia = @DonGia, ThanhTien = @ThanhTien " +
                                   "WHERE MaCTHDN = @MaCTHDN";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaCTHDN", MaCTHDN);
                        cmd.Parameters.AddWithValue("@MaSP", MaSP);
                        cmd.Parameters.AddWithValue("@SoLuong", SoLuong);
                        cmd.Parameters.AddWithValue("@DonGia", DonGia);
                        cmd.Parameters.AddWithValue("@ThanhTien", ThanhTien);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool Xoa(string maCTHDN)
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-K56JJJ3;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Encrypt=False";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM ChiTietHDN WHERE MaCTHDN = @MaCTHDN";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaCTHDN", maCTHDN);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static List<ChiTietHDNModel> LayTheoMaHDN(string maHDN)
        {
            List<ChiTietHDNModel> danhSach = new List<ChiTietHDNModel>();
            string connectionString = "Data Source=DESKTOP-K56JJJ3;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Encrypt=False";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM ChiTietHDN WHERE MaHDN = @MaHDN";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHDN", string.IsNullOrEmpty(maHDN) ? (object)DBNull.Value : maHDN);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var chiTiet = new ChiTietHDNModel(
                                    reader["MaHDN"].ToString(),
                                    reader["MaSP"].ToString(),
                                    Convert.ToInt32(reader["SoLuong"]),
                                    Convert.ToDecimal(reader["DonGia"]),
                                    reader["MaCTHDN"].ToString()
                                )
                                {
                                    ThanhTien = Convert.ToDecimal(reader["ThanhTien"])
                                };
                                danhSach.Add(chiTiet);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return danhSach;
        }

        public static bool CheckKey(string maCTHDN)
        {
            string connectionString = "Data Source=DESKTOP-K56JJJ3;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Encrypt=False";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM ChiTietHDN WHERE MaCTHDN = @MaCTHDN";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaCTHDN", string.IsNullOrEmpty(maCTHDN) ? (object)DBNull.Value : maCTHDN);
                        return (int)cmd.ExecuteScalar() > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false; // Trả về false nếu có lỗi
            }
        }
    }
}