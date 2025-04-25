using System;

namespace DuAnQuanLyQuancafe.Model
{
    public class NhanVienModel
    {
        public string MaNV { get; set; }              // Mã nhân viên (Khóa chính)
        public string TenNV { get; set; }          // Tên nhân viên (Mặc định không null)
        public string DiaChi { get; set; }         // Địa chỉ (Có thể NULL)
        public string GioiTinh { get; set; }       // Giới tính (Nam/Nữ)
        public DateTime NgaySinh { get; set; }    // Ngày sinh (Có thể NULL, dùng Nullable)
        public string SDT { get; set; }            // Số điện thoại (Có thể NULL)
        public string TenQue { get; set; }            // Tên quê (Có thể NULL)
        public string MaQue { get; set; }            // Mã quê (Có thể NULL)
        public byte[] Anh { get; set; }            // Ảnh nhân viên (Kiểu varbinary trong SQL)
    }
}
