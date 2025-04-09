using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAnQuanLyQuancafe.Model
{
    internal class SanPhamModel
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int MaLoai { get; set; }
        public float GiaNhap { get; set; }
        public float GiaBan { get; set; }
        public byte[] HinhAnh { get; set; }
        public int SoLuong { get; set; }
        public int MaCongDung { get; set; }

    }
}
