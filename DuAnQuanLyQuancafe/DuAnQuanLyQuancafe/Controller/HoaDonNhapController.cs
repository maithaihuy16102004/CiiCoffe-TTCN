using DuAnQuanLyQuancafe.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DuAnQuanLyQuancafe.Controller
{
    internal class HoaDonNhapController
    {
        private readonly HoaDonNhapModel _hoaDonNhapModel = new HoaDonNhapModel();

        /// <summary>
        /// Lấy mã hóa đơn nhập tiếp theo
        /// </summary>
        /// <returns>Mã hóa đơn nhập mới</returns>
        public string GetNextHoaDonNhap()
        {
            return HoaDonNhapModel.GetNextHoaDonNhap();
        }

        /// <summary>
        /// Lấy danh sách tất cả các hóa đơn nhập
        /// </summary>
        /// <returns>Danh sách các hóa đơn nhập</returns>
        public List<HoaDonNhapModel> LayDanhSachHDN()
        {
            return _hoaDonNhapModel.LayDanhSachHDN();
        }

        /// <summary>
        /// Thêm một hóa đơn nhập mới
        /// </summary>
        /// <param name="parameter">Hashtable chứa các tham số (MaHDN, NgayNhap, MaNV, MaNCC, TongTien)</param>
        public void ThemHDN(Hashtable parameter)
        {
            HoaDonNhapModel.ThemHDN(parameter);
        }

        /// <summary>
        /// Xóa một hóa đơn nhập dựa trên mã hóa đơn
        /// </summary>
        /// <param name="maHDN">Mã hóa đơn cần xóa</param>
        public void XoaHDN(string maHDN)
        {
            _hoaDonNhapModel.XoaHDN(maHDN);
        }

        /// <summary>
        /// Tìm kiếm hóa đơn nhập dựa trên từ khóa
        /// </summary>
        /// <param name="tuKhoa">Từ khóa tìm kiếm (trong mã hóa đơn)</param>
        /// <returns>Danh sách hóa đơn nhập khớp với từ khóa</returns>
        public List<HoaDonNhapModel> TimKiemHDN(string tuKhoa)
        {
            return _hoaDonNhapModel.TimKiemHDN(tuKhoa);
        }
    }
}