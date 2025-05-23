using DuAnQuanLyQuancafe.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.Controller
{
    public class NhaCungCapController
    {
        // Lấy danh sách nhà cung cấp
        public List<NhaCungCapModel> LayDanhSachNhaCC()
        {
            try
            {
                return NhaCungCapModel.LayDanhSachNhaCC();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy danh sách nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<NhaCungCapModel>();
            }
        }

        // Thêm nhà cung cấp
        public void ThemNhaCC(Hashtable parameter)
        {
            try
            {
                NhaCungCapModel.ThemNhaCC(parameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xóa nhà cung cấp
        public void XoaNhaCungCap(string maNCC)
        {
            try
            {
                NhaCungCapModel.XoaNhaCungCap(maNCC);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sửa nhà cung cấp
        public void SuaNCC(Hashtable parameter)
        {
            try
            {
                NhaCungCapModel.SuaNCC(parameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}