using DuAnQuanLyQuancafe.function;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.Model
{
    internal class ChiTietHDNModel
    {
        public string MaCTHDN { get; private set; }
        public string MaHDN { get; set; }
        public string MaSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; private set; }

        public ChiTietHDNModel(string maHDN, string maSP, int soLuong, decimal donGia, string maCTHDN = null)
        {
            MaHDN = maHDN ?? throw new ArgumentNullException(nameof(maHDN));
            MaSP = maSP ?? throw new ArgumentNullException(nameof(maSP));
            SoLuong = soLuong;
            DonGia = donGia;
            MaCTHDN = maCTHDN;
        }

        public static bool ThemChiTietHDN(ChiTietHDNModel chiTiet, SqlConnection conn)
        {
            try
            {
                if (conn == null)
                {
                    throw new ArgumentNullException(nameof(conn), "Kết nối cơ sở dữ liệu không được null.");
                }

                if (conn.State != ConnectionState.Open)
                {
                    throw new InvalidOperationException("Kết nối cơ sở dữ liệu chưa được mở.");
                }

                if (string.IsNullOrEmpty(chiTiet.MaCTHDN))
                {
                    throw new ArgumentException("MaCTHDN không được để trống.", nameof(chiTiet.MaCTHDN));
                }

                string query = "INSERT INTO ChiTietHDN (MaCTHDN, MaHDN, MaSP, SoLuong, DonGia) VALUES (@MaCTHDN, @MaHDN, @MaSP, @SoLuong, @DonGia)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaCTHDN", chiTiet.MaCTHDN ?? (object)DBNull.Value); // Đảm bảo không NULL
                    cmd.Parameters.AddWithValue("@MaHDN", chiTiet.MaHDN);
                    cmd.Parameters.AddWithValue("@MaSP", chiTiet.MaSP);
                    cmd.Parameters.AddWithValue("@SoLuong", chiTiet.SoLuong);
                    cmd.Parameters.AddWithValue("@DonGia", chiTiet.DonGia);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi SQL khi thêm chi tiết hóa đơn: {sqlEx.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool CapNhatChiTietHDN(ChiTietHDNModel chiTiet, SqlConnection conn)
        {
            try
            {
                if (conn == null)
                {
                    throw new ArgumentNullException(nameof(conn), "Kết nối cơ sở dữ liệu không được null.");
                }

                if (conn.State != ConnectionState.Open)
                {
                    throw new InvalidOperationException("Kết nối cơ sở dữ liệu chưa được mở.");
                }

                string query = "UPDATE ChiTietHDN SET MaHDN = @MaHDN, MaSP = @MaSP, SoLuong = @SoLuong, DonGia = @DonGia WHERE MaCTHDN = @MaCTHDN";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaCTHDN", chiTiet.MaCTHDN);
                    cmd.Parameters.AddWithValue("@MaHDN", chiTiet.MaHDN);
                    cmd.Parameters.AddWithValue("@MaSP", chiTiet.MaSP);
                    cmd.Parameters.AddWithValue("@SoLuong", chiTiet.SoLuong);
                    cmd.Parameters.AddWithValue("@DonGia", chiTiet.DonGia);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi SQL khi cập nhật chi tiết hóa đơn: {sqlEx.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool XoaChiTietHDN(string maCTHDN, SqlConnection conn)
        {
            try
            {
                if (conn == null)
                {
                    throw new ArgumentNullException(nameof(conn), "Kết nối cơ sở dữ liệu không được null.");
                }

                if (conn.State != ConnectionState.Open)
                {
                    throw new InvalidOperationException("Kết nối cơ sở dữ liệu chưa được mở.");
                }

                string query = "DELETE FROM ChiTietHDN WHERE MaCTHDN = @MaCTHDN";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaCTHDN", maCTHDN);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi SQL khi xóa chi tiết hóa đơn: {sqlEx.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static List<ChiTietHDNModel> LayChiTietHDNTheoMa(string maHDN)
        {
            List<ChiTietHDNModel> danhSach = new List<ChiTietHDNModel>();
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    string query = "SELECT * FROM ChiTietHDN WHERE MaHDN = @MaHDN";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHDN", maHDN);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var chiTiet = new ChiTietHDNModel(
                                    reader["MaHDN"] != DBNull.Value ? reader["MaHDN"].ToString() : "",
                                    reader["MaSP"] != DBNull.Value ? reader["MaSP"].ToString() : "",
                                    reader["SoLuong"] != DBNull.Value ? Convert.ToInt32(reader["SoLuong"]) : 0,
                                    reader["DonGia"] != DBNull.Value ? Convert.ToDecimal(reader["DonGia"]) : 0m,
                                    reader["MaCTHDN"] != DBNull.Value ? reader["MaCTHDN"].ToString() : ""
                                )
                                {
                                    ThanhTien = reader["ThanhTien"] != DBNull.Value ? Convert.ToDecimal(reader["ThanhTien"]) : 0m
                                };
                                danhSach.Add(chiTiet);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi SQL khi lấy chi tiết hóa đơn: {sqlEx.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return danhSach;
        }
    }
}