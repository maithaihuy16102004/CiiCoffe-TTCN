using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.Model;

namespace DuAnQuanLyQuancafe.View.QuanLyHoaDonNhap
{
    public partial class FrmChiTietHDN : Form
    {
        private readonly string _maHDN;
        private readonly ChiTietHDNController _chiTietHDNController = new ChiTietHDNController();

        public FrmChiTietHDN(string maHDN)
        {
            InitializeComponent();
            function.DatabaseHelper.FillCombo("SELECT MaSP, TenSP FROM SanPham", cboSanPham, "MaSP", "TenSP");
            function.DatabaseHelper.FillCombo("SELECT MaHDN FROM HoaDonNhap", cboHDN, "MaHDN", "MaHDN"); // Sửa lại để dùng FillCombo
            cboSanPham.SelectedIndex = -1; // Đặt giá trị mặc định là không chọn sản phẩm
            cboHDN.SelectedIndex = -1; // Đặt giá trị mặc định là không chọn hóa đơn nhập
            _maHDN = maHDN ?? throw new ArgumentNullException(nameof(maHDN), "Mã hóa đơn nhập không được để trống.");
            ResetValue(); // Đảm bảo mã chi tiết được sinh ngay khi khởi tạo
            LoadChiTietHDN();

            // Cấu hình các control
            txtMaChiTiet.Enabled = false; // Không cho phép chỉnh sửa mã chi tiết
            txtThanhTien.Enabled = false; // Không cho phép chỉnh sửa tổng tiền
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
            if (dgvChiTiet.Columns.Contains("MaCTHDN"))
            {
                dgvChiTiet.Columns["MaCTHDN"].HeaderText = "Mã CTHĐN";
                dgvChiTiet.Columns["MaCTHDN"].Width = 120;
            }
            if (dgvChiTiet.Columns.Contains("MaHDN"))
            {
                dgvChiTiet.Columns["MaHDN"].HeaderText = "Mã HĐN";
                dgvChiTiet.Columns["MaHDN"].Width = 120;
            }
            if (dgvChiTiet.Columns.Contains("MaSP"))
            {
                dgvChiTiet.Columns["MaSP"].HeaderText = "Mã SP";
                dgvChiTiet.Columns["MaSP"].Width = 100;
            }
            if (dgvChiTiet.Columns.Contains("SoLuong"))
            {
                dgvChiTiet.Columns["SoLuong"].HeaderText = "Số Lượng";
                dgvChiTiet.Columns["SoLuong"].Width = 80;
            }
            if (dgvChiTiet.Columns.Contains("DonGia"))
            {
                dgvChiTiet.Columns["DonGia"].HeaderText = "Đơn Giá";
                dgvChiTiet.Columns["DonGia"].DefaultCellStyle.Format = "N0";
                dgvChiTiet.Columns["DonGia"].Width = 100;
            }
            if (dgvChiTiet.Columns.Contains("ThanhTien"))
            {
                dgvChiTiet.Columns["ThanhTien"].HeaderText = "Thành Tiền";
                dgvChiTiet.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
                dgvChiTiet.Columns["ThanhTien"].Width = 120;
            }
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            try
            {
                ResetValue();
                MessageBox.Show("Dữ liệu đã được làm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi làm mới dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtDonGia_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void TinhThanhTien()
        {
            try
            {
                if (int.TryParse(txtSoLuong.Text, out int soLuong) && decimal.TryParse(txtDonGia.Text, out decimal donGia))
                {
                    txtThanhTien.Text = (soLuong * donGia).ToString("N0");
                }
                else
                {
                    txtThanhTien.Text = "0";
                }
            }
            catch (Exception)
            {
                txtThanhTien.Text = "0";
            }
        }

        private void ResetValue()
        {
            try
            {
                txtMaChiTiet.Text = _chiTietHDNController.LayMaCTHDNTuDatabase(); // Đặt mã mới khi làm mới
                cboHDN.SelectedIndex = -1;
                cboSanPham.SelectedIndex = -1;
                txtSoLuong.Text = string.Empty;
                txtDonGia.Text = string.Empty;
                txtThanhTien.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi làm mới dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
        }

        private void DgvChiTiet_Click(object sender, EventArgs e)
        {
        }

        private void BtnCapNhat_Click(object sender, EventArgs e)
        {
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            try
            {
                string maHDN = cboHDN.SelectedValue?.ToString();
                string maSP = cboSanPham.SelectedValue?.ToString();
                string maCTHDN = _chiTietHDNController.LayMaCTHDNTuDatabase(); // Lấy mã tự động từ database

                if (string.IsNullOrWhiteSpace(maHDN) || string.IsNullOrWhiteSpace(maSP) ||
                    string.IsNullOrWhiteSpace(txtSoLuong.Text) || string.IsNullOrWhiteSpace(txtDonGia.Text) || string.IsNullOrWhiteSpace(maCTHDN))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong <= 0)
                {
                    MessageBox.Show("Số lượng phải là số nguyên dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtDonGia.Text, out decimal donGia) || donGia < 0)
                {
                    MessageBox.Show("Đơn giá phải là số không âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var chiTiet = new ChiTietHDNModel(maHDN, maSP, soLuong, donGia, maCTHDN);
                if (_chiTietHDNController.ThemChiTietHDN(chiTiet))
                {
                    MessageBox.Show("Thêm chi tiết hóa đơn nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TinhThanhTien();
                    LoadChiTietHDN();
                    ResetValue();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCapNhat_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dgvChiTiet.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một chi tiết hóa đơn nhập để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maHDN = cboHDN.SelectedValue?.ToString();
                string maSP = cboSanPham.SelectedValue?.ToString();

                if (string.IsNullOrWhiteSpace(txtMaChiTiet.Text) || string.IsNullOrWhiteSpace(maHDN) || string.IsNullOrWhiteSpace(maSP) ||
                    string.IsNullOrWhiteSpace(txtSoLuong.Text) || string.IsNullOrWhiteSpace(txtDonGia.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong <= 0)
                {
                    MessageBox.Show("Số lượng phải là số nguyên dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtDonGia.Text, out decimal donGia) || donGia < 0)
                {
                    MessageBox.Show("Đơn giá phải là số không âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var chiTiet = new ChiTietHDNModel(maHDN, maSP, soLuong, donGia, txtMaChiTiet.Text);

                if (_chiTietHDNController.CapNhatChiTietHDN(chiTiet))
                {
                    MessageBox.Show("Cập nhật chi tiết hóa đơn nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadChiTietHDN();
                    ResetValue();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvChiTiet_Click_1(object sender, EventArgs e)
        {
            if (dgvChiTiet.SelectedRows.Count > 0)
            {
                var selectedRow = dgvChiTiet.SelectedRows[0];
                txtMaChiTiet.Text = selectedRow.Cells["MaCTHDN"].Value?.ToString();
                cboHDN.Text = selectedRow.Cells["MaHDN"].Value?.ToString();
                cboSanPham.Text = selectedRow.Cells["MaSP"].Value?.ToString();
                txtSoLuong.Text = selectedRow.Cells["SoLuong"].Value?.ToString();
                txtDonGia.Text = selectedRow.Cells["DonGia"].Value?.ToString();
                txtThanhTien.Text = selectedRow.Cells["ThanhTien"].Value?.ToString();
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dgvChiTiet.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một chi tiết hóa đơn nhập để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maCTHDN = txtMaChiTiet.Text;
                if (string.IsNullOrWhiteSpace(maCTHDN))
                {
                    MessageBox.Show("Mã chi tiết hóa đơn nhập không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa chi tiết hóa đơn nhập '{maCTHDN}'?", "Xác nhận xóa",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_chiTietHDNController.XoaChiTietHDN(maCTHDN))
                    {
                        MessageBox.Show("Xóa chi tiết hóa đơn nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadChiTietHDN();
                        ResetValue();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLammoi_Click_1(object sender, EventArgs e)
        {
            ResetValue();
        }
    }
}