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
        private bool isLoggedIn = false;
        private static string connString = "Data Source=DESKTOP-K56JJJ3;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Encrypt=False";

        public FrmLogin()
        {
            InitializeComponent();
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
            string maNV = txtTaikhoan.Text.Trim();
            string matKhau = txtMatkhau.Text.Trim();

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string sql = "SELECT LoaiTaiKhoan FROM TaiKhoan WHERE LOWER(MaNV) = LOWER(@MaNV) AND MatKhau = @MatKhau";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        cmd.Parameters.AddWithValue("@MatKhau", matKhau);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            string loaiTaiKhoan = result.ToString().Trim();
                            DangNhapThanhCong(maNV, loaiTaiKhoan);
                        }
                        else
                        {
                            digThatbai.Show("Đăng nhập thất bại", "Thông báo");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKhuonMat_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmNhanDien frmNhanDien = new FrmNhanDien();
            frmNhanDien.LoginByFace += XuLyDangNhapBangKhuonMat;
            frmNhanDien.FormClosed += (s, args) => this.Show();
            frmNhanDien.Show();
        }

        private void XuLyDangNhapBangKhuonMat(string maNV, string loaiTaiKhoan)
        {
            try
            {
                if (!isLoggedIn)
                {
                    DangNhapThanhCong(maNV, loaiTaiKhoan);
                }
                else
                {
                    digThatbai.Show("Đăng nhập thất bại", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DangNhapThanhCong(string maNV, string loaiTaiKhoan)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT MaNV, TenNV, HinhAnh FROM NhanVien WHERE MaNV = @MaNV", conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        NhanVienModel nv = new NhanVienModel
                        {
                            MaNV = reader["MaNV"].ToString(),
                            TenNV = reader["TenNV"].ToString(),
                            Anh = reader["HinhAnh"] != DBNull.Value ? (byte[])reader["HinhAnh"] : null
                        };

                        isLoggedIn = true;

                        if (loaiTaiKhoan == "Admin")
                            new FrmCapCao(nv).Show();
                        else
                            new FrmCapThap(nv).Show();

                        this.Hide();
                    }
                    else
                    {
                        digThatbai.Show("Không tìm thấy nhân viên", "Thông báo");
                    }
                }
            }
        }
    }
}
