using System;
using System.Collections.Generic;
using System.Data;
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
     public List<HoaDonBanModel> LayDanhSachHDB()
        {
            List<HoaDonBanModel> HDB = new List<HoaDonBanModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string sql = "SELECT MaHDB, NgayBan, MaNV, TongTien FROM HoaDonBan";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HDB.Add(new HoaDonBanModel
                            {
                                MaHDB = reader["MaHDB"].ToString(),
                                NgayBan = reader["NgayBan"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["NgayBan"]),
                                MaNV = reader["MaNV"] as string,
                                Tongtien = reader["TongTien"] == DBNull.Value ? 0f : Convert.ToSingle(reader["TongTien"])
                            });
                        }
                    }
                }
            }
            return HDB;
        }

        public void LuuHoaDon(HoaDonBanModel hoaDon)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string sql = "INSERT INTO HoaDonBan (MaHDB, NgayBan, MaNV, TongTien) VALUES (@MaHDB, @NgayBan, @MaNV, @TongTien)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaHDB", hoaDon.MaHDB ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayBan", hoaDon.NgayBan == DateTime.MinValue ? (object)DBNull.Value : hoaDon.NgayBan);
                    cmd.Parameters.AddWithValue("@MaNV", hoaDon.MaNV ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TongTien", hoaDon.Tongtien);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public string LayMaHDBTuDatabase()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT dbo.GenerateMaHDB()", conn))
                {
                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
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

                string sql = "SELECT MaHDB, NgayBan, MaNV, TongTien FROM HoaDonBan WHERE MaNV = @MaNV";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNV", maNV ?? (object)DBNull.Value);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HDB.Add(new HoaDonBanModel
                            {
                                MaHDB = reader["MaHDB"].ToString(),
                                NgayBan = reader["NgayBan"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["NgayBan"]),
                                MaNV = reader["MaNV"] as string,
                                Tongtien = reader["TongTien"] == DBNull.Value ? 0f : Convert.ToSingle(reader["TongTien"])
                            });
                        }
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

                string sql = "SELECT MaHDB, NgayBan, MaNV, TongTien FROM HoaDonBan WHERE NgayBan >= @NgayBatDau AND NgayBan <= @NgayKetThuc";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
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
                                MaNV = reader["MaNV"] as string,
                                Tongtien = reader["TongTien"] == DBNull.Value ? 0f : Convert.ToSingle(reader["TongTien"])
                            });
                        }
                    }
                }
            }
            return HDB;
        }

        public string LayMaHDBMoiNhat(string maNV, DateTime ngayBan)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string sql = "SELECT TOP 1 MaHDB FROM HoaDonBan WHERE MaNV = @MaNV AND NgayBan = @NgayBan ORDER BY MaHDB DESC";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNV", maNV ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayBan", ngayBan.Date);
                    object result = cmd.ExecuteScalar();
                    return result?.ToString();
                }
            }
        }
    }
}
  
