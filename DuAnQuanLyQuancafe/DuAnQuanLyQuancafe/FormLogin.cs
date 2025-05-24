using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.Model;
using DuAnQuanLyQuancafe.View;
using System;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe
{
    public partial class FrmLogin : Form
    {
        private readonly TaiKhoanController _taiKhoanController;
        private bool _isLoggedIn = false;

        public FrmLogin()
        {
            
            InitializeComponent();
            _taiKhoanController = new TaiKhoanController();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            string maNV = txtTaikhoan.Text.Trim();
            string matKhau = txtMatkhau.Text.Trim();

            if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(matKhau))
            {
                digThatbai.Show("Vui lòng nhập tài khoản và mật khẩu.", "Thông báo");
                return;
            }

            var (success, loaiTaiKhoan, errorMessage) = _taiKhoanController.DangNhap(maNV, matKhau);
            if (success)
            {
                var (nhanVien, nhanVienError) = _taiKhoanController.LayThongTinNhanVien(maNV);
                if (nhanVien != null)
                {
                    _isLoggedIn = true;
                    if (loaiTaiKhoan == "Admin")
                        new FrmCapCao(nhanVien).Show();
                    else
                        new FrmCapThap(nhanVien).Show();
                    this.Hide();
                }
                else
                {
                    digThatbai.Show(nhanVienError, "Thông báo");
                }
            }
            else
            {
                digThatbai.Show(errorMessage, "Thông báo");
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
            if (_isLoggedIn)
            {
                digThatbai.Show("Đăng nhập thất bại", "Thông báo");
                return;
            }

            var (nhanVien, errorMessage) = _taiKhoanController.LayThongTinNhanVien(maNV);
            if (nhanVien != null)
            {
                _isLoggedIn = true;
                if (loaiTaiKhoan == "Admin")
                    new FrmCapCao(nhanVien).Show();
                else
                    new FrmCapThap(nhanVien).Show();
                this.Hide();
            }
            else
            {
                digThatbai.Show(errorMessage, "Thông báo");
            }
        }

        
    }
}