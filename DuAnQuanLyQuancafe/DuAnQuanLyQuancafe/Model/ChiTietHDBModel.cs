using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        // Thao tác Create: Lưu danh sách ChiTietHDB
        public static void LuuChiTiet(List<ChiTietHDBModel> chiTietHDBs)
        {
            foreach (var ct in chiTietHDBs)
            {
                string sql = $"INSERT INTO ChiTietHDB (MaHDB, MaSP, SoLuong, ThanhTien, KhuyenMai) VALUES " +
                             $"( '{ct.MaHDB}', '{ct.MaSP}', {ct.SoLuong}, {ct.ThanhTien}, N'{ct.KhuyenMai}')";
                DatabaseHelper.RunSql(sql);
            }
        }

        // Thao tác Read: Lấy MaCTHDB tự sinh từ Database
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

        // Thao tác Read: Lấy danh sách ChiTietHDB theo MaHDB
        public static List<ChiTietHDBModel> LayChiTietHDBTheoMaHDB(string maHDB)
        {
            List<ChiTietHDBModel> chiTietList = new List<ChiTietHDBModel>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                string sql = @"
                    SELECT ct.MaCTHDB, ct.MaHDB, ct.MaSP, sp.TenSP, ct.SoLuong, ct.ThanhTien, ct.KhuyenMai
                    FROM ChiTietHDB ct
                    LEFT JOIN SanPham sp ON ct.MaSP = sp.MaSP
                    WHERE ct.MaHDB = @MaHDB";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHDB", maHDB);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            chiTietList.Add(new ChiTietHDBModel
                            {
                                MaCTHDB = reader["MaCTHDB"].ToString(),
                                MaHDB = reader["MaHDB"].ToString(),
                                MaSP = reader["MaSP"].ToString(),
                                TenSP = reader["TenSP"]?.ToString(),
                                SoLuong = Convert.ToInt32(reader["SoLuong"]),
                                ThanhTien = Convert.ToSingle(reader["ThanhTien"]),
                                KhuyenMai = reader["KhuyenMai"]?.ToString()
                            });
                        }
                    }
                }
            }

            return chiTietList;
        }
    }
}