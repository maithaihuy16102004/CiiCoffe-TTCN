using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuAnQuanLyQuancafe.function;
using DuAnQuanLyQuancafe.Model;

namespace DuAnQuanLyQuancafe.Controller
{
    public class ChiTietHDBController
    {
        public static void LuuChiTiet(List<ChiTietHDBModel> chiTietHDBs)
        {
            foreach (var ct in chiTietHDBs)
            {
                string sql = $"INSERT INTO ChiTietHDB ( MaHDB, MaSP, SoLuong, ThanhTien, KhuyenMai) VALUES " +
                             $"( '{ct.MaHDB}', '{ct.MaSP}', {ct.SoLuong}, {ct.ThanhTien}, N'{ct.KhuyenMai}')";
                DatabaseHelper.RunSql(sql);
            }
        }

        public static string LayMaCTHDBTuDatabase()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT dbo.GenerateMaCTHDB()", conn))
                {
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : null;
                }
            }
        }
    }

}
