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
                // Kiểm tra kết nối SQL
                if (SqlConn.State == ConnectionState.Closed)
                {
                    SqlConn.Open();
                }

                // Truy vấn kiểm tra tài khoản và lấy LoaiTaiKhoan
                string sql = "SELECT LoaiTaiKhoan FROM TaiKhoan WHERE LOWER(TenDangNhap) = LOWER(@TenDangNhap) AND MatKhau = @MatKhau";
                using (SqlCommand cmd = new SqlCommand(sql, SqlConn))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", txtTaikhoan.Text.Trim());
                    cmd.Parameters.AddWithValue("@MatKhau", txtMatkhau.Text.Trim());

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string loaiTaiKhoan = result.ToString().Trim();

                        // Đăng nhập thành công
                        digThanhCong.Show("Đăng nhập thành công", "Thông Báo");

                        // Kiểm tra quyền và mở form tương ứng
                        if (loaiTaiKhoan == "Admin")
                        {
                            FrmCapCao formCapcao = new FrmCapCao();
                            formCapcao.Show();
                        }
                        else if (loaiTaiKhoan == "NhanVien")
                        {
                            FrmCapThap formCapthap = new FrmCapThap();
                            formCapthap.Show();
                        }

                        this.Hide();
                    }
                    else
                    {
                        digThatbai.Show("Đăng nhập thất bại", "Thông Báo");
                    }
                }
            }
            catch (Exception ex)
            {
                // Bắt lỗi kết nối
                MessageBox.Show("Lỗi kết nối SQL: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Đóng kết nối sau khi thao tác xong
                if (SqlConn.State == ConnectionState.Open)
                    SqlConn.Close();
            }
        }

        private void btnKhuonMat_Click(object sender, EventArgs e)
        {
            FrmNhanDien frmNhanDien = new FrmNhanDien();
            frmNhanDien.LoginByFace += XuLyDangNhapBangKhuonMat; // Sự kiện trả về kết quả
            frmNhanDien.Show();
        }
        private void XuLyDangNhapBangKhuonMat(string tenDangNhap, string loaiTaiKhoan)
        {
            digThanhCong.Show("Đăng nhập bằng khuôn mặt thành công", "Thông báo");

            if (loaiTaiKhoan == "Admin")
            {
                new FrmCapCao().Show();
            }
            else
            {
                new FrmCapThap().Show();
            }
            this.Hide();
        }

    }
}
