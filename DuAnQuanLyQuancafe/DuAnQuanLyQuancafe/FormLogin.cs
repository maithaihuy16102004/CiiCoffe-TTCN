using DuAnQuanLyQuancafe.function;
using DuAnQuanLyQuancafe.Model;
using DuAnQuanLyQuancafe.View;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe
{
    public partial class FrmLogin : Form
    {
        // Đối tượng kết nối SQL
        SqlConnection SqlConn;
        private static string connString = "Data Source=DESKTOP-K56JJJ3;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Encrypt=False";

        public FrmLogin()
        {
            InitializeComponent();
            SqlConn = new SqlConnection(connString); // Khởi tạo kết nối
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Bạn có muốn thoát chương trình không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            try
            {
                if (SqlConn.State == ConnectionState.Closed)
                {
                    SqlConn.Open();
                }

                string sql = "SELECT LoaiTaiKhoan FROM TaiKhoan WHERE LOWER(MaNV) = LOWER(@MaNV) AND MatKhau = @MatKhau";
                using (SqlCommand cmd = new SqlCommand(sql, SqlConn))
                {
                    cmd.Parameters.AddWithValue("@MaNV", txtTaikhoan.Text.Trim());
                    cmd.Parameters.AddWithValue("@MatKhau", txtMatkhau.Text.Trim());

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string loaiTaiKhoan = result.ToString().Trim();
                        string maNV = txtTaikhoan.Text.Trim();

                        // Lấy thông tin nhân viên từ bảng NhanVien
                        string sqlNV = "SELECT MaNV, TenNV, HinhAnh FROM NhanVien WHERE MaNV = @MaNV";
                        using (SqlCommand cmdNV = new SqlCommand(sqlNV, SqlConn))
                        {
                            cmdNV.Parameters.AddWithValue("@MaNV", maNV);
                            using (SqlDataReader reader = cmdNV.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    NhanVienModel nv = new NhanVienModel
                                    {
                                        MaNV = reader["MaNV"].ToString(),
                                        TenNV = reader["TenNV"].ToString(),
                                        Anh = reader["HinhAnh"] != DBNull.Value ? (byte[])reader["HinhAnh"] : null
                                    };

                                    digThanhCong.Show("Đăng nhập thành công", "Thông Báo");

                                    if (loaiTaiKhoan == "Admin")
                                    {
                                        new FrmCapCao(nv).Show();  // <-- TRUYỀN NHÂN VIÊN
                                    }
                                    else
                                    {
                                        new FrmCapThap(nv).Show();  // Nếu muốn truyền luôn
                                    }

                                    this.Hide();
                                }
                                else
                                {
                                    digThatbai.Show("Không tìm thấy nhân viên", "Thông báo");
                                }
                            }
                        }
                    }
                    else
                    {
                        digThatbai.Show("Đăng nhập thất bại", "Thông báo");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (SqlConn.State == ConnectionState.Open)
                    SqlConn.Close();
            }
        }


        private void btnKhuonMat_Click(object sender, EventArgs e)
        {
            this.Hide(); // Ẩn form login
            FrmNhanDien frmNhanDien = new FrmNhanDien();
            frmNhanDien.LoginByFace += XuLyDangNhapBangKhuonMat;
            frmNhanDien.FormClosed += (s, args) => this.Hide(); // Hiện lại nếu người dùng đóng form nhận diện
            frmNhanDien.Show();
        }


        // Sửa lại để truyền cả form vào và đóng đúng form
        private void XuLyDangNhapBangKhuonMat(string tenDangNhap, string loaiTaiKhoan)
        {
            try
            {
                // Hiển thị thông báo đăng nhập thành công
                digThanhCong.Show("Đăng nhập bằng khuôn mặt thành công", "Thông báo");

                // Ẩn form đăng nhập chính nếu muốn
                this.Hide();

                // Khởi tạo kết nối với CSDL
                
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // Sử dụng SqlParameter để tránh SQL Injection
                    SqlCommand cmd = new SqlCommand("SELECT MaNV, TenNV, HinhAnh FROM NHANVIEN WHERE MaNV = @MaNV", conn);
                    cmd.Parameters.AddWithValue("@MaNV", tenDangNhap);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            // Nếu có bản ghi, lấy thông tin nhân viên
                            NhanVienModel nv = new NhanVienModel
                            {
                                MaNV = reader["MaNV"].ToString(),
                                TenNV = reader["TenNV"].ToString(),
                                Anh = reader["HinhAnh"] != DBNull.Value ? (byte[])reader["HinhAnh"] : null
                            };

                            // Hiển thị thông báo đăng nhập thành công
                            digThanhCong.Show("Đăng nhập thành công", "Thông Báo");

                            // Mở form tương ứng dựa trên loại tài khoản
                            if (loaiTaiKhoan == "Admin")
                            {
                                new FrmCapCao(nv).Show();  // TRUYỀN NHÂN VIÊN VÀO FORM
                            }
                            else
                            {
                                new FrmCapThap(nv).Show();  // Nếu muốn truyền luôn
                            }

                            // Ẩn form hiện tại
                            this.Hide();
                        }
                        else
                        {
                            // Không tìm thấy nhân viên
                            digThatbai.Show("Không tìm thấy nhân viên", "Thông báo");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Đảm bảo luôn đóng kết nối khi kết thúc
                if (SqlConn.State == ConnectionState.Open)
                {
                    SqlConn.Close();
                }
            }
        }


    }
}
