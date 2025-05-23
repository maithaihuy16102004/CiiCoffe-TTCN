using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuAnQuanLyQuancafe.function;
using DuAnQuanLyQuancafe.Model;

namespace DuAnQuanLyQuancafe.Controller
{
    public class HoaDonBanController
    {
        public List<HoaDonBanModel> LaydanhsachHDB()
        {
            List<HoaDonBanModel> HDB = new List<HoaDonBanModel>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string sql = "SELECT hdb.MaHDB, hdb.Ngayban, hdb.MaNV, hdb.Tongtien " +
                               "FROM Hoadonban hdb ";

                SqlCommand cmd = new SqlCommand(sql, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        HDB.Add(new HoaDonBanModel
                        {
                            MaHDB = reader["MaHDB"].ToString(),
                            NgayBan = reader["NgayBan"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["NgayBan"]),
                            MaNV = reader["MaNV"] as string,
                            Tongtien = reader["Tongtien"] == DBNull.Value ? 0f : Convert.ToSingle(reader["Tongtien"]),
                        });
                    }
                }
            }

            return HDB;
        }
        public static void LuuHoaDon(HoaDonBanModel hoaDon)
        {
            string sql = $"INSERT INTO HoaDonBan ( NgayBan, MaNV, TongTien) VALUES " +
                         $"( '{hoaDon.NgayBan:yyyy-MM-dd}', '{hoaDon.MaNV}', {hoaDon.Tongtien})";
            DatabaseHelper.RunSql(sql);
        }

        public static string LayMaHDBTuDatabase()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT dbo.GenerateMaHDB()", conn))
                {
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : null;
                }
            }
        }
        public List<HoaDonBanModel> TimKiemHDBTheoMaNV(string maNV)
        {
            List<HoaDonBanModel> HDB = new List<HoaDonBanModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string sql = "SELECT MaHDB, Ngayban, MaNV, Tongtien FROM Hoadonban WHERE MaNV = @MaNV";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        HDB.Add(new HoaDonBanModel
                        {
                            MaHDB = reader["MaHDB"].ToString(),
                            NgayBan = reader["NgayBan"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["NgayBan"]),
                            MaNV = reader["MaNV"] as string,
                            Tongtien = reader["Tongtien"] == DBNull.Value ? 0f : Convert.ToSingle(reader["Tongtien"]),
                        });
                    }
                }
            }
            return HDB;

        }
        public List<HoaDonBanModel> LocHDBTheoNgay(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            List<HoaDonBanModel> HDB = new List<HoaDonBanModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string sql = "SELECT MaHDB, Ngayban, MaNV, Tongtien FROM Hoadonban WHERE Ngayban >= @NgayBatDau AND Ngayban <= @NgayKetThuc";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NgayBatDau", ngayBatDau.Date);
                cmd.Parameters.AddWithValue("@NgayKetThuc", ngayKetThuc.Date);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        HDB.Add(new HoaDonBanModel
                        {
                            MaHDB = reader["MaHDB"].ToString(),
                            NgayBan = reader["NgayBan"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["NgayBan"]),
                            MaNV = reader["MaNV"] == DBNull.Value ? null : reader["MaNV"].ToString(),
                            Tongtien = reader["Tongtien"] == DBNull.Value ? 0f : Convert.ToSingle(reader["Tongtien"]),
                        });
                    }
                }
            }
            return HDB;
        }

    }
}

