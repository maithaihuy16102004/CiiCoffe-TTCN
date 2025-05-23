using System.Collections;
using System.Collections.Generic;
using DuAnQuanLyQuancafe.Model;

namespace DuAnQuanLyQuancafe.Controller
{
    public class SanPhamController
    {
        public List<SanPhamModel> LayDanhSachSanPham()
        {
            return SanPhamModel.LayDanhSachSanPham();
        }

        public void ThemSanPham(Hashtable parameter)
        {
            SanPhamModel.ThemSanPham(parameter);
        }

        public void XoaSanPham(string maSP)
        {
            SanPhamModel.XoaSanPham(maSP);
        }

        public void CapNhatSanPham(Hashtable parameter)
        {
            SanPhamModel.CapNhatSanPham(parameter);
        }
    }
}