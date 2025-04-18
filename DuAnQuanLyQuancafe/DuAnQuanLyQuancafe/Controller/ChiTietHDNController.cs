using DuAnQuanLyQuancafe.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DuAnQuanLyQuancafe.Controller
{
    internal class ChiTietHDNController
    {
        private string connectionString = @"Data Source=.;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True";

        // Hàm lấy danh sách chi tiết hóa đơn nhập theo mã HDN
        public List<ChiTietHDNModel> LayChiTietHDNTheoMa(string maHDN)
        {
            List<ChiTietHDNModel> danhSach = new List<ChiTietHDNModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ChiTietHDN WHERE MaHDN = @MaHDN";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaHDN", maHDN);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ChiTietHDNModel ct = new ChiTietHDNModel
                        {
                            MaCTHDN = reader["MaCTHDN"].ToString(),
                            MaHDN = reader["MaHDN"].ToString(),
                            MaSP = reader["MaSP"].ToString(),
                            SoLuong = int.TryParse(reader["SoLuong"]?.ToString(), out int sl) ? sl : 0,
                            DonGia = decimal.TryParse(reader["DonGia"]?.ToString(), out decimal dg) ? dg : 0,
                            ThanhTien = decimal.TryParse(reader["ThanhTien"]?.ToString(), out decimal tt) ? tt : 0,
                            KhuyenMai = decimal.TryParse(reader["KhuyenMai"]?.ToString(), out decimal km) ? km : 0,
                        };

                        danhSach.Add(ct);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Lỗi khi lấy dữ liệu chi tiết HDN: " + ex.Message);
                }
            }

            return danhSach;
        }
    }
}
