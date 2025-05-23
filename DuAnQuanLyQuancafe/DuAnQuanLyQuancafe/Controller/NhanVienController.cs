using System;
using System.Collections;
using System.Collections.Generic;
using DuAnQuanLyQuancafe.Model;

namespace DuAnQuanLyQuancafe.Controller
{
    public class NhanVienController
    {
        // Lấy mã nhân viên tiếp theo
        public string GetNextMaNhanVien()
        {
            return NhanVienModel.GetNextMaNhanVien();
        }

        // Lấy danh sách nhân viên
        public List<NhanVienModel> LayDanhSachNhanVien()
        {
            return NhanVienModel.LayDanhSachNhanVien();
        }

        // Thêm nhân viên
        public (bool Success, string ErrorMessage) ThemNhanVien(Hashtable parameter)
        {
            return NhanVienModel.ThemNhanVien(parameter);
        }

        // Sửa nhân viên
        public void SuaNhanVien(Hashtable parameter)
        {
            NhanVienModel.SuaNhanVien(parameter);
        }

        // Xóa nhân viên
        public (bool Success, string ErrorMessage) XoaNhanVien(string maNV)
        {
            return NhanVienModel.XoaNhanVien(maNV);
        }
    }
}