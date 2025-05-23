using DuAnQuanLyQuancafe.Model;
using DuAnQuanLyQuancafe.function;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

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
                    throw new ArgumentException("Mã hóa đơn nhập không được để trống.");

                return ChiTietHDNModel.LayChiTietHDNTheoMa(maHDN); // Gọi trực tiếp từ Model
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy danh sách chi tiết hóa đơn nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        /// <summary>
        /// Thêm một chi tiết hóa đơn nhập mới
        /// </summary>
        /// <param name="chiTiet">Đối tượng ChiTietHDNModel cần thêm</param>
        public void ThemChiTietHDN(ChiTietHDNModel chiTiet)
        {
            try
            {
                ChiTietHDNModel.ThemChiTietHDN(chiTiet);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm chi tiết hóa đơn nhập: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Cập nhật thông tin chi tiết hóa đơn nhập
        /// </summary>
        /// <param name="chiTiet">Đối tượng ChiTietHDNModel cần cập nhật</param>
        public void CapNhatChiTietHDN(ChiTietHDNModel chiTiet)
        {
            try
            {
                ChiTietHDNModel.CapNhatChiTietHDN(chiTiet);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật chi tiết hóa đơn nhập: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Xóa chi tiết hóa đơn nhập theo mã chi tiết
        /// </summary>
        /// <param name="maCTHDN">Mã chi tiết hóa đơn nhập</param>
        public void XoaChiTietHDN(string maCTHDN)
        {
            try
            {
                ChiTietHDNModel.XoaChiTietHDN(maCTHDN);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa chi tiết hóa đơn nhập: {ex.Message}", ex);
            }
        }
    }
}