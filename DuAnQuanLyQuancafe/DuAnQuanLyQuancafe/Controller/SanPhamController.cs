using DuAnQuanLyQuancafe.function;
using DuAnQuanLyQuancafe.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.Controller
{
    internal class SanPhamController
    {
        byte[] anh = null;
        public List<SanPhamModel> LayDanhSachSanPham()
        {
            List<SanPhamModel> SanPham = new List<SanPhamModel>();

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string query = "SELECT sp.MaSP, sp.TenSP, sp.GiaBan, sp.GiaNhap, sp.SoLuong, sp.HinhAnh, sp.MaLoai, sp.MaCongDung, cd.TenCongDung, l.TenLoai FROM SanPham AS sp JOIN CongDung AS cd ON sp.MaCongDung = cd.MaCongDung JOIN Loai AS l ON sp.MaLoai = l.MaLoai";
                SqlCommand cmd = new SqlCommand(query, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SanPham.Add(new SanPhamModel
                        {
                            MaSP = reader["MaSP"].ToString(),
                            TenSP = reader["TenSP"]?.ToString(),
                            MaLoai = reader["MaLoai"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MaLoai"]),
                            GiaNhap = reader["GiaNhap"] == DBNull.Value ? 0 : Convert.ToSingle(reader["GiaNhap"]),
                            GiaBan = reader["GiaBan"] == DBNull.Value ? 0 : Convert.ToSingle(reader["GiaBan"]),
                            SoLuong = reader["SoLuong"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SoLuong"]),
                            MaCongDung = reader["MaCongDung"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MaCongDung"]),
                            HinhAnh = reader["HinhAnh"] == DBNull.Value ? null : (byte[])reader["HinhAnh"],
                            TenLoai = reader["TenLoai"]?.ToString(),      // Đọc TenLoai
                            TenCongDung = reader["TenCongDung"]?.ToString() // Đọc TenCongDung
                        });
                    }
                }
            }
            return SanPham;
        }
        public void Themsanpham(Hashtable parameter)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string query = "INSERT INTO SanPham (MaSP, TenSP, MaLoai, GiaNhap, GiaBan, HinhAnh, SoLuong, MaCongDung) VALUES (@MaSP, @TenSP, @MaLoai, @GiaNhap, @GiaBan, @HinhAnh, @SoLuong, @MaCongDung)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", parameter["MaSP"] ?? DBNull.Value); // Thay thế 1 bằng giá trị thực tế
                        cmd.Parameters.AddWithValue("@TenSP", parameter["TenSP"]);
                        cmd.Parameters.AddWithValue("@MaLoai", parameter["MaLoai"]);
                        cmd.Parameters.AddWithValue("@GiaNhap", parameter["GiaNhap"]);
                        cmd.Parameters.AddWithValue("@GiaBan", parameter["GiaBan"]);
                        cmd.Parameters.AddWithValue("@HinhAnh", parameter["HinhAnh"] ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SoLuong", parameter["SoLuong"]);
                        cmd.Parameters.AddWithValue("@MaCongDung", parameter["MaCongDung"]);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }
        public void XoaSanPham(string MaSP)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    string checkQuery = "SELECT COUNT(*) FROM ChiTietHDB WHERE MaSP = @MaSP";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@MaSP", MaSP); // maSP là mã sản phẩm bạn muốn xóa
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Sản phẩm này đang được sử dụng trong hóa đơn bán. Không thể xóa!");
                    }
                    else
                    {
                        string deleteQuery = "DELETE FROM SanPham WHERE MaSP = @MaSP";
                        SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                        deleteCmd.Parameters.AddWithValue("@MaSP", MaSP);
                        deleteCmd.ExecuteNonQuery();
                        MessageBox.Show("Xóa sản phẩm thành công!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa sản phẩm: " + ex.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }
        public void CapNhatSanPham(Hashtable parameter)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                try
                {
                    // Kiểm tra các trường hợp cần thiết trước khi thực hiện cập nhật
                    if (parameter["MaSP"] == null || parameter["MaSP"].ToString().Trim().Length == 0)
                    {
                        MessageBox.Show("Mã sản phẩm không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (parameter["TenSP"] == null || parameter["TenSP"].ToString().Trim().Length == 0)
                    {
                        MessageBox.Show("Bạn phải nhập tên sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (parameter["GiaNhap"] == null || parameter["GiaNhap"].ToString().Trim().Length == 0)
                    {
                        MessageBox.Show("Bạn điền giá nhập", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (parameter["GiaBan"] == null || parameter["GiaBan"].ToString().Trim().Length == 0)
                    {
                        MessageBox.Show("Bạn điền giá bán", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (parameter["SoLuong"] == null || parameter["SoLuong"].ToString().Trim().Length == 0)
                    {
                        MessageBox.Show("Bạn điền số lượng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (parameter["MaCongDung"] == null || parameter["MaCongDung"].ToString().Trim().Length == 0)
                    {
                        MessageBox.Show("Bạn điền chưa điền công dụng sản phẩm ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (parameter["MaLoai"] == null || parameter["MaLoai"].ToString().Trim().Length == 0)
                    {
                        MessageBox.Show("Bạn chưa điền loại sản phẩm", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (parameter["HinhAnh"] == null)
                    {
                        MessageBox.Show("Bạn chưa chọn ảnh nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    // Nếu các trường hợp đã kiểm tra hợp lệ, tiếp tục thực hiện cập nhật
                    string query = @"UPDATE SanPham 
                             SET TenSP = @TenSP,  MaLoai = @MaLoai, GiaNhap = @GiaNhap,GiaBan = @GiaBan, SoLuong = @SoLuong, 
                                 MaCongDung = @MaCongDung, HinhAnh = @Anh 
                             WHERE MaSP = @MaSP";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Thêm các tham số với giá trị từ parameter
                        cmd.Parameters.AddWithValue("@MaSP", parameter["MaSP"].ToString());  // Chuyển giá trị sang chuỗi nếu cần
                        cmd.Parameters.AddWithValue("@TenSP", parameter["TenSP"]);
                        cmd.Parameters.AddWithValue("@MaLoai", parameter["MaLoai"]);
                        cmd.Parameters.AddWithValue("@GiaNhap", parameter["GiaNhap"]);
                        cmd.Parameters.AddWithValue("@GiaBan", parameter["GiaBan"]);
                        cmd.Parameters.AddWithValue("@SoLuong", parameter["SoLuong"]);
                        cmd.Parameters.AddWithValue("@MaCongDung", parameter["MaCongDung"]);
                        cmd.Parameters.AddWithValue("@Anh", parameter["HinhAnh"] ?? DBNull.Value);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật sản phẩm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sản phẩm để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
