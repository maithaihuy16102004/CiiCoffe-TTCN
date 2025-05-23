using DuAnQuanLyQuancafe.Model;
using System;

namespace DuAnQuanLyQuancafe.Controller
{
    public class TaiKhoanController
    {
        // Xác thực đăng nhập
        public (bool Success, string LoaiTaiKhoan, string ErrorMessage) DangNhap(string maNV, string matKhau)
        {
            return TaiKhoanModel.XacThucDangNhap(maNV, matKhau);
        }

        // Lấy thông tin nhân viên
        public (NhanVienModel NhanVien, string ErrorMessage) LayThongTinNhanVien(string maNV)
        {
            return TaiKhoanModel.LayThongTinNhanVien(maNV);
        }
    }
}