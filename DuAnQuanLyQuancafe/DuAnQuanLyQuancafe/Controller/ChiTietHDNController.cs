using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DuAnQuanLyQuancafe.Model;
using DuAnQuanLyQuancafe.function;

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
            try
            {
                if (string.IsNullOrEmpty(maHDN))
                {
                    MessageBox.Show("Mã hóa đơn nhập không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return new List<ChiTietHDNModel>();
                }

                return ChiTietHDNModel.LayChiTietHDNTheoMa(maHDN);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy danh sách chi tiết hóa đơn nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<ChiTietHDNModel>();
            }
        }

        /// <summary>
        /// Thêm một chi tiết hóa đơn nhập mới
        /// </summary>
        /// <param name="chiTiet">Đối tượng ChiTietHDNModel cần thêm</param>
        /// <returns>Trả về true nếu thêm thành công, false nếu thất bại</returns>
        public bool ThemChiTietHDN(ChiTietHDNModel chiTiet)
        {
            try
            {
                if (chiTiet == null)
                {
                    MessageBox.Show("Dữ liệu chi tiết hóa đơn nhập không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open(); // Đảm bảo kết nối được mở
                    }
                    bool result = ChiTietHDNModel.ThemChiTietHDN(chiTiet, conn); // Truyền kết nối vào Model
                    if (result)
                    {
                        // MessageBox đã được hiển thị trong FrmChiTietHDN, không cần hiển thị ở đây
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Thêm chi tiết hóa đơn nhập thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi SQL khi thêm chi tiết hóa đơn nhập: {sqlEx.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm chi tiết hóa đơn nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Lưu danh sách chi tiết hóa đơn nhập
        /// </summary>
        /// <param name="chiTietHDNs">Danh sách chi tiết hóa đơn nhập</param>
        /// <returns>Trả về true nếu tất cả đều thành công, false nếu có lỗi</returns>
        public bool LuuChiTiet(List<ChiTietHDNModel> chiTietHDNs)
        {
            try
            {
                if (chiTietHDNs == null || chiTietHDNs.Count == 0)
                {
                    MessageBox.Show("Danh sách chi tiết hóa đơn nhập không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                bool allSuccess = true;
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open(); // Đảm bảo kết nối được mở
                    }

                    foreach (var ct in chiTietHDNs)
                    {
                        if (!ChiTietHDNModel.ThemChiTietHDN(ct, conn)) // Sửa lỗi cú pháp: gọi phương thức đúng
                        {
                            MessageBox.Show($"Thêm chi tiết hóa đơn nhập cho mã HĐN '{ct.MaHDN}' thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            allSuccess = false;
                        }
                    }
                }

                if (allSuccess)
                {
                    MessageBox.Show("Lưu danh sách chi tiết hóa đơn nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return allSuccess;
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi SQL khi lưu danh sách chi tiết hóa đơn nhập: {sqlEx.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu danh sách chi tiết hóa đơn nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Cập nhật thông tin chi tiết hóa đơn nhập
        /// </summary>
        /// <param name="chiTiet">Đối tượng ChiTietHDNModel cần cập nhật</param>
        /// <returns>Trả về true nếu cập nhật thành công, false nếu thất bại</returns>
        public bool CapNhatChiTietHDN(ChiTietHDNModel chiTiet)
        {
            try
            {
                if (chiTiet == null)
                {
                    MessageBox.Show("Dữ liệu chi tiết hóa đơn nhập không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open(); // Đảm bảo kết nối được mở
                    }
                    bool result = ChiTietHDNModel.CapNhatChiTietHDN(chiTiet, conn); // Truyền kết nối vào Model
                    if (result)
                    {
                        // MessageBox đã được hiển thị trong FrmChiTietHDN, không cần hiển thị ở đây
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật chi tiết hóa đơn nhập thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi SQL khi cập nhật chi tiết hóa đơn nhập: {sqlEx.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật chi tiết hóa đơn nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Xóa chi tiết hóa đơn nhập theo mã chi tiết
        /// </summary>
        /// <param name="maCTHDN">Mã chi tiết hóa đơn nhập</param>
        /// <returns>Trả về true nếu xóa thành công, false nếu thất bại</returns>
        public bool XoaChiTietHDN(string maCTHDN)
        {
            try
            {
                if (string.IsNullOrEmpty(maCTHDN))
                {
                    MessageBox.Show("Mã chi tiết hóa đơn nhập không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open(); // Đảm bảo kết nối được mở
                    }
                    bool result = ChiTietHDNModel.XoaChiTietHDN(maCTHDN, conn); // Truyền kết nối vào Model
                    if (result)
                    {
                        // MessageBox đã được hiển thị trong FrmChiTietHDN, không cần hiển thị ở đây
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Xóa chi tiết hóa đơn nhập thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi SQL khi xóa chi tiết hóa đơn nhập: {sqlEx.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa chi tiết hóa đơn nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Lấy mã chi tiết hóa đơn nhập tự động từ cơ sở dữ liệu (tùy chọn, để hiển thị trước cho người dùng)
        /// </summary>
        /// <returns>Mã chi tiết hóa đơn nhập mới</returns>
        public string LayMaCTHDNTuDatabase()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    using (SqlCommand cmd = new SqlCommand("SELECT dbo.GenerateMaCTHDN()", conn))
                    {
                        object result = cmd.ExecuteScalar();
                        string maCTHDN = result?.ToString() ?? "CTHDN20250524001"; // Giá trị mặc định nếu không lấy được
                        return maCTHDN;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi SQL khi lấy mã chi tiết hóa đơn nhập: {sqlEx.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "CTHDN20250524001"; // Giá trị mặc định khi có lỗi
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy mã chi tiết hóa đơn nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "CTHDN20250524001"; // Giá trị mặc định khi có lỗi
            }
        }
    }
}