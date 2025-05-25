using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DuAnQuanLyQuancafe.Model;

namespace DuAnQuanLyQuancafe.Controller
{
    internal class ChiTietHDNController
    {
        /// <summary>
        /// Lấy danh sách chi tiết hóa đơn nhập theo mã hóa đơn nhập
        /// </summary>
        /// <param name="maHDN">Mã hóa đơn nhập</param>
        /// <returns>Danh sách chi tiết hóa đơn nhập</returns>
        public List<ChiTietHDNModel> LayChiTietHDNTheoMa(string maHDN)
        {
            if (string.IsNullOrEmpty(maHDN))
            {
                MessageBox.Show("Mã hóa đơn nhập không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return new List<ChiTietHDNModel>();
            }

            // Kiểm tra hóa đơn nhập có tồn tại không (gọi qua model)
            if (!CheckHoaDonNhapExists(maHDN))
            {
                MessageBox.Show($"Hóa đơn nhập với mã {maHDN} không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return new List<ChiTietHDNModel>();
            }

            return ChiTietHDNModel.LayTheoMaHDN(maHDN);
        }

        /// <summary>
        /// Thêm một chi tiết hóa đơn nhập mới
        /// </summary>
        /// <param name="chiTiet">Đối tượng ChiTietHDNModel cần thêm</param>
        /// <returns>True nếu thêm thành công, False nếu thất bại</returns>
        public bool ThemChiTietHDN(ChiTietHDNModel chiTiet)
        {
            if (chiTiet == null)
            {
                MessageBox.Show("Dữ liệu chi tiết hóa đơn nhập không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra hóa đơn nhập có tồn tại không
            if (!CheckHoaDonNhapExists(chiTiet.MaHDN))
            {
                MessageBox.Show($"Hóa đơn nhập với mã {chiTiet.MaHDN} không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra sản phẩm có tồn tại không
            if (!CheckSanPhamExists(chiTiet.MaSP))
            {
                MessageBox.Show($"Sản phẩm với mã {chiTiet.MaSP} không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return chiTiet.Them();
        }

        /// <summary>
        /// Lưu danh sách chi tiết hóa đơn nhập
        /// </summary>
        /// <param name="chiTietHDNs">Danh sách chi tiết hóa đơn nhập</param>
        /// <returns>True nếu tất cả đều thành công, False nếu có lỗi</returns>
        public bool LuuChiTiet(List<ChiTietHDNModel> chiTietHDNs)
        {
            if (chiTietHDNs == null || chiTietHDNs.Count == 0)
            {
                MessageBox.Show("Danh sách chi tiết hóa đơn nhập không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra tất cả chi tiết có cùng MaHDN không
            string maHDN = chiTietHDNs[0].MaHDN;
            if (!CheckHoaDonNhapExists(maHDN))
            {
                MessageBox.Show($"Hóa đơn nhập với mã {maHDN} không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (chiTietHDNs.Any(ct => ct.MaHDN != maHDN))
            {
                MessageBox.Show("Tất cả chi tiết hóa đơn nhập phải thuộc cùng một hóa đơn nhập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            bool allSuccess = true;
            foreach (var chiTiet in chiTietHDNs)
            {
                if (!CheckSanPhamExists(chiTiet.MaSP))
                {
                    MessageBox.Show($"Sản phẩm với mã {chiTiet.MaSP} không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    allSuccess = false;
                    continue;
                }

                if (!chiTiet.Them())
                {
                    allSuccess = false;
                }
            }

            if (allSuccess)
            {
                MessageBox.Show("Lưu danh sách chi tiết hóa đơn nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi lưu một số chi tiết hóa đơn nhập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return allSuccess;
        }

        /// <summary>
        /// Cập nhật thông tin chi tiết hóa đơn nhập
        /// </summary>
        /// <param name="chiTiet">Đối tượng ChiTietHDNModel cần cập nhật</param>
        /// <returns>True nếu cập nhật thành công, False nếu thất bại</returns>
        public bool CapNhatChiTietHDN(ChiTietHDNModel chiTiet)
        {
            if (chiTiet == null)
            {
                MessageBox.Show("Dữ liệu chi tiết hóa đơn nhập không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra chi tiết hóa đơn có tồn tại không
            if (!CheckKey(chiTiet.MaCTHDN))
            {
                MessageBox.Show($"Chi tiết hóa đơn với mã {chiTiet.MaCTHDN} không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra sản phẩm có tồn tại không
            if (!CheckSanPhamExists(chiTiet.MaSP))
            {
                MessageBox.Show($"Sản phẩm với mã {chiTiet.MaSP} không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return chiTiet.CapNhat();
        }

        /// <summary>
        /// Xóa chi tiết hóa đơn nhập theo mã chi tiết
        /// </summary>
        /// <param name="maCTHDN">Mã chi tiết hóa đơn nhập</param>
        /// <returns>True nếu xóa thành công, False nếu thất bại</returns>
        public bool XoaChiTietHDN(string maCTHDN)
        {
            if (string.IsNullOrEmpty(maCTHDN))
            {
                MessageBox.Show("Mã chi tiết hóa đơn nhập không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra chi tiết hóa đơn có tồn tại không
            if (!CheckKey(maCTHDN))
            {
                MessageBox.Show($"Chi tiết hóa đơn với mã {maCTHDN} không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return ChiTietHDNModel.Xoa(maCTHDN);
        }

        // Kiểm tra xem hóa đơn nhập có tồn tại không (gọi qua model)
        private bool CheckHoaDonNhapExists(string maHDN)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-K56JJJ3;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Encrypt=False"))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM HoaDonNhap WHERE MaHDN = @MaHDN";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHDN", maHDN);
                        return (int)cmd.ExecuteScalar() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kiểm tra hóa đơn nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Kiểm tra xem sản phẩm có tồn tại không (gọi qua model)
        private bool CheckSanPhamExists(string maSP)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-K56JJJ3;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Encrypt=False"))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM SanPham WHERE MaSP = @MaSP";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", maSP);
                        return (int)cmd.ExecuteScalar() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kiểm tra sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Kiểm tra xem chi tiết hóa đơn có tồn tại không (gọi qua model)
        private bool CheckKey(string maCTHDN)
        {
            return ChiTietHDNModel.CheckKey(maCTHDN);
        }
    }
}