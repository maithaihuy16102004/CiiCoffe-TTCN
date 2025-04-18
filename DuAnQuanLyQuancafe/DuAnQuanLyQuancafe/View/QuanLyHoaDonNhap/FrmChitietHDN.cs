using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private void FrmChitietHDN_Load(object sender, EventArgs e)
        {
            ChiTietHDNController controller = new ChiTietHDNController();
            List<ChiTietHDNModel> danhSachCT = controller.LayChiTietHDNTheoMa(maHDN);

            dgvChiTiet.DataSource = danhSachCT;

            dgvChiTiet.Columns["MaCTHDN"].HeaderText = "Mã CTHĐN";
            dgvChiTiet.Columns["MaSP"].HeaderText = "Mã SP";
            dgvChiTiet.Columns["SoLuong"].HeaderText = "Số Lượng";
            dgvChiTiet.Columns["DonGia"].HeaderText = "Đơn Giá";
            dgvChiTiet.Columns["KhuyenMai"].HeaderText = "Khuyến Mãi";
            dgvChiTiet.Columns["ThanhTien"].HeaderText = "Thành Tiền";
        }
    }
}
