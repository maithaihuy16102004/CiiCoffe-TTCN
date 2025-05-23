using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.View.QuanLyHoaDonNhap
{
    public partial class FrmChitietHDN : Form
    {
        private readonly string _maHDN;
        private readonly ChiTietHDNController _chiTietHDNController = new ChiTietHDNController();

        public FrmChitietHDN(string maHDN)
        {
            InitializeComponent();
            _maHDN = maHDN ?? throw new ArgumentNullException(nameof(maHDN), "Mã hóa đơn nhập không được để trống.");
            LoadChiTietHDN();
        }

        private void LoadChiTietHDN()
        {
            try
            {
                List<ChiTietHDNModel> danhSachCT = _chiTietHDNController.LayChiTietHDNTheoMa(_maHDN);
                if (danhSachCT == null || !danhSachCT.Any())
                {
                    dgvChiTiet.DataSource = null;
                    MessageBox.Show($"Không có chi tiết hóa đơn nhập nào cho mã {_maHDN}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvChiTiet.DataSource = danhSachCT;
                ConfigureDataGridViewColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvChiTiet.DataSource = null;
            }
        }

        private void ConfigureDataGridViewColumns()
        {
            if (dgvChiTiet.Columns.Contains("MaCTHDN")) dgvChiTiet.Columns["MaCTHDN"].HeaderText = "Mã CTHĐN";
            if (dgvChiTiet.Columns.Contains("MaHDN")) dgvChiTiet.Columns["MaHDN"].HeaderText = "Mã HĐN";
            if (dgvChiTiet.Columns.Contains("MaSP")) dgvChiTiet.Columns["MaSP"].HeaderText = "Mã SP";
            if (dgvChiTiet.Columns.Contains("SoLuong")) dgvChiTiet.Columns["SoLuong"].HeaderText = "Số Lượng";
            if (dgvChiTiet.Columns.Contains("DonGia"))
            {
                dgvChiTiet.Columns["DonGia"].HeaderText = "Đơn Giá";
                dgvChiTiet.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            }
            if (dgvChiTiet.Columns.Contains("KhuyenMai"))
            {
                dgvChiTiet.Columns["KhuyenMai"].HeaderText = "Khuyến Mãi";
                dgvChiTiet.Columns["KhuyenMai"].DefaultCellStyle.Format = "N0";
            }
            if (dgvChiTiet.Columns.Contains("ThanhTien"))
            {
                dgvChiTiet.Columns["ThanhTien"].HeaderText = "Thành Tiền";
                dgvChiTiet.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            }

            // Đặt độ rộng cột
            if (dgvChiTiet.Columns.Contains("MaCTHDN")) dgvChiTiet.Columns["MaCTHDN"].Width = 120;
            if (dgvChiTiet.Columns.Contains("MaHDN")) dgvChiTiet.Columns["MaHDN"].Width = 120;
            if (dgvChiTiet.Columns.Contains("MaSP")) dgvChiTiet.Columns["MaSP"].Width = 100;
            if (dgvChiTiet.Columns.Contains("SoLuong")) dgvChiTiet.Columns["SoLuong"].Width = 80;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}