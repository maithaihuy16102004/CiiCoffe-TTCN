using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.View.QuanLyHoaDonNhap
{
    public partial class FrmChitietHDN : Form
    {
        private string maHDN;

        public FrmChitietHDN(string maHDN)
        {
            InitializeComponent();
            this.maHDN = maHDN;

            // Thêm sự kiện Deactivate để đóng form khi mất focus
            this.Deactivate += FrmChitietHDN_Deactivate;

            // Thêm sự kiện MouseClick để đóng form khi nhấp chuột ra ngoài
            this.MouseClick += FrmChitietHDN_MouseClick;
        }

        private void FrmChitietHDN_Load(object sender, EventArgs e)
        {
            ChiTietHDNController controller = new ChiTietHDNController();
            List<ChiTietHDNModel> danhSachCT = controller.LayChiTietHDNTheoMa(maHDN);

            dgvChiTiet.DataSource = danhSachCT;

            // Đổi tên cột trong DataGridView
            dgvChiTiet.Columns["MaCTHDN"].HeaderText = "Mã CTHĐN";
            dgvChiTiet.Columns["MaSP"].HeaderText = "Mã SP";
            dgvChiTiet.Columns["SoLuong"].HeaderText = "Số Lượng";
            dgvChiTiet.Columns["DonGia"].HeaderText = "Đơn Giá";
            dgvChiTiet.Columns["KhuyenMai"].HeaderText = "Khuyến Mãi";
            dgvChiTiet.Columns["ThanhTien"].HeaderText = "Thành Tiền";
        }

        // Sự kiện Deactivate khi form mất focus
        private void FrmChitietHDN_Deactivate(object sender, EventArgs e)
        {
            this.Close(); // Đóng form khi mất focus
        }

        // Sự kiện MouseClick khi nhấp chuột ra ngoài form
        private void FrmChitietHDN_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close(); // Đóng form khi nhấp chuột ra ngoài
        }
    }
}
