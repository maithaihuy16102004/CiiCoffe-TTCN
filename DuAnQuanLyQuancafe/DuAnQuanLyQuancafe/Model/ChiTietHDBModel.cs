using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuAnQuanLyQuancafe.function;

namespace DuAnQuanLyQuancafe.Model
{
    public class ChiTietHDBModel
    {
        public string MaCTHDB { get; set; } 
        public string MaHDB { get; set; }
        public string MaSP { get; set; }
        public int SoLuong { get; set; }
        public float ThanhTien { get; set; }
        public string KhuyenMai { get; set; }
        public string TenSP { get; set; }
    }

}
