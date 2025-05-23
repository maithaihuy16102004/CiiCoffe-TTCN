using DuAnQuanLyQuancafe.Model;
using DuAnQuanLyQuancafe.View.NhaCungCap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.View
{
    public partial class FrmCapCao : Form
    {
        private NhanVienModel nhanvien;
        public FrmCapCao(NhanVienModel NhanVien)
        {
            InitializeComponent();
            nhanvien = NhanVien;
        }
        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            lbTitle.Text = "Quản lý nhân viên";
            FrmNhanVien frmNhanVien = new FrmNhanVien() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmNhanVien.FormBorderStyle = FormBorderStyle.None;
            this.PnlHome.Controls.Clear();
            this.PnlHome.Controls.Add(frmNhanVien);
            frmNhanVien.Show();
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            lbTitle.Text = "Quản lý sản phẩm";
            FrmSanPham frmSanPham = new FrmSanPham() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmSanPham.FormBorderStyle = FormBorderStyle.None;
            this.PnlHome.Controls.Clear();
            this.PnlHome.Controls.Add(frmSanPham);
            frmSanPham.Show();
        }

        private void btnHDB_Click(object sender, EventArgs e)
        {
            lbTitle.Text = "Quản lý hóa đơn bán";
            FrmHDB frmHoaDonBan = new FrmHDB() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmHoaDonBan.FormBorderStyle = FormBorderStyle.None;
            this.PnlHome.Controls.Clear();
            this.PnlHome.Controls.Add(frmHoaDonBan);
            frmHoaDonBan.Show();
        }

        private void btnHDN_Click(object sender, EventArgs e)
        {
            lbTitle.Text = "Quản lý hóa đơn nhập";
            FrmHDN frmHoaDonNhap = new FrmHDN() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmHoaDonNhap.FormBorderStyle = FormBorderStyle.None;
            this.PnlHome.Controls.Clear();
            this.PnlHome.Controls.Add(frmHoaDonNhap);
            frmHoaDonNhap.Show();
        }

        private void btnNhaCungCap_Click(object sender, EventArgs e)
        {
            lbTitle.Text = "Quản lý nhà cung cấp";
            FrmNhaCungCap frmNhaCungCap = new FrmNhaCungCap() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmNhaCungCap.FormBorderStyle = FormBorderStyle.None;
            this.PnlHome.Controls.Clear();
            this.PnlHome.Controls.Add(frmNhaCungCap);
            frmNhaCungCap.Show();
        }

        private void btnAcc_Click(object sender, EventArgs e)
        {
            string maNV = nhanvien.MaNV;
            lbTitle.Text = "Quản lý tài khoản";
            FrmAcc frmTaiKhoan = new FrmAcc(maNV) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmTaiKhoan.FormBorderStyle = FormBorderStyle.None;
            this.PnlHome.Controls.Clear();
            this.PnlHome.Controls.Add(frmTaiKhoan);
            frmTaiKhoan.Show();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FrmCapCao_Load(object sender, EventArgs e)
        {
            if (nhanvien != null)
            {
                lbMa.Text = nhanvien.MaNV;
                lbTen.Text = nhanvien.TenNV;

                if (nhanvien.Anh != null)
                {
                    using (MemoryStream ms = new MemoryStream(nhanvien.Anh))
                    {
                        picAdmin.Image = Image.FromStream(ms);
                        picAdmin.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                else
                {
                    picAdmin.Image = null;
                }

            }
        }
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
        }

        private void btnThongke_Click(object sender, EventArgs e)
        {
            lbTitle.Text = "Thống kê";
            FrmThongKe frmThongKe = new FrmThongKe() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmThongKe.FormBorderStyle = FormBorderStyle.None;
            this.PnlHome.Controls.Clear();
            this.PnlHome.Controls.Add(frmThongKe);
            frmThongKe.Show();
        }
    }
}
