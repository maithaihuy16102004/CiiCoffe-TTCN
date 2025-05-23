using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuAnQuanLyQuancafe.function;

namespace DuAnQuanLyQuancafe.Model
{
    public class HoaDonBanModel
    {
        public string MaHDB { get; set; }
        public DateTime NgayBan { get; set; } 
        public string MaNV { get; set; }
        public float Tongtien { get; set; }
    }
}
  
