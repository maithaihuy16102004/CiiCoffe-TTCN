using DuAnQuanLyQuancafe.function;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DuAnQuanLyQuancafe.Model
{
    public class SanPhamModel
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int MaLoai { get; set; }
        public float GiaNhap { get; set; }
        public float GiaBan { get; set; }
        public int SoLuong { get; set; }
        public int MaCongDung { get; set; }
        public byte[] HinhAnh { get; set; }
        public string TenLoai { get; set; }
        public string TenCongDung { get; set; }

        // Lấy danh sách sản phẩm
        public static List<SanPhamModel> LayDanhSachSanPham()
        {
            List<SanPhamModel> sanPhamList = new List<SanPhamModel>();
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    string query = "SELECT sp.MaSP, sp.TenSP, sp.GiaBan, sp.GiaNhap, sp.SoLuong, sp.HinhAnh, sp.MaLoai, sp.MaCongDung, cd.TenCongDung, l.TenLoai " +
                                  "FROM SanPham AS sp LEFT JOIN CongDung AS cd ON sp.MaCongDung = cd.MaCongDung " +
                                  "LEFT JOIN Loai AS l ON sp.MaLoai = l.MaLoai";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                sanPhamList.Add(new SanPhamModel
                                {
                                    MaSP = reader["MaSP"].ToString(),
                                    TenSP = reader["TenSP"]?.ToString(),
                                    MaLoai = reader["MaLoai"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MaLoai"]),
                                    GiaNhap = reader["GiaNhap"] == DBNull.Value ? 0 : Convert.ToSingle(reader["GiaNhap"]),
                                    GiaBan = reader["GiaBan"] == DBNull.Value ? 0 : Convert.ToSingle(reader["GiaBan"]),
                                    SoLuong = reader["SoLuong"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SoLuong"]),
                                    MaCongDung = reader["MaCongDung"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MaCongDung"]),
                                    HinhAnh = reader["HinhAnh"] == DBNull.Value ? null : (byte[])reader["HinhAnh"],
                                    TenLoai = reader["TenLoai"]?.ToString(),
                                    TenCongDung = reader["TenCongDung"]?.ToString()
                                });
                            }
                        }
                    }
                }
                return sanPhamList;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách sản phẩm: {ex.Message}");
            }
        }

        // Thêm sản phẩm
        public static void ThemSanPham(Hashtable parameter)
        {
            // Kiểm tra dữ liệu đầu vào
            if (parameter["MaSP"] == null || parameter["MaSP"].ToString().Trim().Length == 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ.");

            if (parameter["TenSP"] == null || parameter["TenSP"].ToString().Trim().Length == 0)
                throw new ArgumentException("Tên sản phẩm không được để trống.");

            if (parameter["MaLoai"] == null || parameter["MaLoai"].ToString().Trim().Length == 0)
                throw new ArgumentException("Mã loại sản phẩm không được để trống.");

            if (parameter["GiaNhap"] == null || Convert.ToSingle(parameter["GiaNhap"]) < 0)
                throw new ArgumentException("Giá nhập không hợp lệ.");

            if (parameter["GiaBan"] == null || Convert.ToSingle(parameter["GiaBan"]) < 0)
                throw new ArgumentException("Giá bán không hợp lệ.");

            if (parameter["SoLuong"] == null || Convert.ToInt32(parameter["SoLuong"]) < 0)
                throw new ArgumentException("Số lượng không hợp lệ.");

            if (parameter["MaCongDung"] == null || parameter["MaCongDung"].ToString().Trim().Length == 0)
                throw new ArgumentException("Mã công dụng không được để trống.");

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    string query = "INSERT INTO SanPham (MaSP, TenSP, MaLoai, GiaNhap, GiaBan, HinhAnh, SoLuong, MaCongDung) " +
                                  "VALUES (@MaSP, @TenSP, @MaLoai, @GiaNhap, @GiaBan, @HinhAnh, @SoLuong, @MaCongDung)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", parameter["MaSP"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TenSP", parameter["TenSP"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MaLoai", parameter["MaLoai"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@GiaNhap", parameter["GiaNhap"] ?? 0);
                        cmd.Parameters.AddWithValue("@GiaBan", parameter["GiaBan"] ?? 0);
                        cmd.Parameters.AddWithValue("@HinhAnh", parameter["HinhAnh"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SoLuong", parameter["SoLuong"] ?? 0);
                        cmd.Parameters.AddWithValue("@MaCongDung", parameter["MaCongDung"] ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Lỗi cơ sở dữ liệu khi thêm sản phẩm: {ex.Message}");
            }
        }

        // Xóa sản phẩm
        public static void XoaSanPham(string maSP)
        {
            if (string.IsNullOrEmpty(maSP))
                throw new ArgumentException("Mã sản phẩm không hợp lệ.");

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    // Kiểm tra ràng buộc khóa ngoại
                    string checkQuery = "SELECT COUNT(*) FROM ChiTietHDB WHERE MaSP = @MaSP";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaSP", maSP);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                            throw new InvalidOperationException("Sản phẩm này đang được sử dụng trong hóa đơn bán. Không thể xóa!");
                    }

                    // Xóa sản phẩm
                    string deleteQuery = "DELETE FROM SanPham WHERE MaSP = @MaSP";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@MaSP", maSP);
                        int rowsAffected = deleteCmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                            throw new Exception("Không tìm thấy sản phẩm để xóa.");
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Lỗi cơ sở dữ liệu khi xóa sản phẩm: {ex.Message}");
            }
        }

        // Cập nhật sản phẩm
        public static void CapNhatSanPham(Hashtable parameter)
        {
            // Kiểm tra dữ liệu đầu vào
            if (parameter["MaSP"] == null || parameter["MaSP"].ToString().Trim().Length == 0)
                throw new ArgumentException("Mã sản phẩm không hợp lệ.");

            if (parameter["TenSP"] == null || parameter["TenSP"].ToString().Trim().Length == 0)
                throw new ArgumentException("Tên sản phẩm không được để trống.");

            if (parameter["MaLoai"] == null || parameter["MaLoai"].ToString().Trim().Length == 0)
                throw new ArgumentException("Mã loại sản phẩm không được để trống.");

            if (parameter["GiaNhap"] == null || Convert.ToSingle(parameter["GiaNhap"]) < 0)
                throw new ArgumentException("Giá nhập không hợp lệ.");

            if (parameter["GiaBan"] == null || Convert.ToSingle(parameter["GiaBan"]) < 0)
                throw new ArgumentException("Giá bán không hợp lệ.");

            if (parameter["SoLuong"] == null || Convert.ToInt32(parameter["SoLuong"]) < 0)
                throw new ArgumentException("Số lượng không hợp lệ.");

            if (parameter["MaCongDung"] == null || parameter["MaCongDung"].ToString().Trim().Length == 0)
                throw new ArgumentException("Mã công dụng không được để trống.");

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    string query = @"UPDATE SanPham 
                    SET TenSP = @TenSP, MaLoai = @MaLoai, GiaNhap = @GiaNhap, 
                        GiaBan = @GiaBan, SoLuong = @SoLuong, MaCongDung = @MaCongDung, 
                        HinhAnh = @HinhAnh
                    WHERE MaSP = @MaSP";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", parameter["MaSP"].ToString());
                        cmd.Parameters.AddWithValue("@TenSP", parameter["TenSP"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MaLoai", parameter["MaLoai"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@GiaNhap", parameter["GiaNhap"] ?? 0);
                        cmd.Parameters.AddWithValue("@GiaBan", parameter["GiaBan"] ?? 0);
                        cmd.Parameters.AddWithValue("@SoLuong", parameter["SoLuong"] ?? 0);
                        cmd.Parameters.AddWithValue("@MaCongDung", parameter["MaCongDung"] ?? DBNull.Value);

                        // Xử lý HinhAnh một cách an toàn
                        if (parameter["HinhAnh"] != null && parameter["HinhAnh"] is byte[])
                        {
                            cmd.Parameters.AddWithValue("@HinhAnh", (byte[])parameter["HinhAnh"]);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@HinhAnh", DBNull.Value);
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                            throw new Exception("Không tìm thấy sản phẩm để cập nhật.");
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception($"Lỗi cơ sở dữ liệu khi sửa sản phẩm: {ex.Message}");
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }
    }
}