using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.Model;
using System.Data.SqlClient;

namespace DuAnQuanLyQuancafe.View.QuanLyHoaDonNhap
{
    public partial class FrmChiTietHDN : Form
    {
        private readonly string _maHDN;
        private readonly ChiTietHDNController _chiTietHDNController;
        private List<ChiTietHDNModel> _danhSachCT;

        public FrmChiTietHDN(string maHDN)
        {
            _maHDN = maHDN ?? throw new ArgumentNullException(nameof(maHDN), "Mã hóa đơn nhập không được để trống.");
            _chiTietHDNController = new ChiTietHDNController();
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            // Thiết lập giá trị mặc định và cấu hình control
            this.Text = $"Chi Tiết Hóa Đơn Nhập - Mã: {_maHDN}";
            txtMaHDN.Text = _maHDN;
            txtMaCTHDN.Enabled = false;
            txtMaHDN.Enabled = false;
            txtThanhTien.Enabled = false;
            dgvChiTiet.ReadOnly = true;

            // Điền danh sách sản phẩm vào ComboBox
            function.DatabaseHelper.FillCombo("SELECT MaSP, TenSP FROM SanPham", cboSanPham, "MaSP", "TenSP");
            cboSanPham.SelectedIndex = -1; // Đặt giá trị mặc định là không chọn sản phẩm
            SanPhamController sanPhamController = new SanPhamController();
            List<SanPhamModel> danhSachSP = sanPhamController.LayDanhSachSanPham();
            cboSanPham.DataSource = danhSachSP;
            cboSanPham.DisplayMember = "TenSP"; // Hiển thị cho người dùng
            cboSanPham.ValueMember = "MaSP";    // Dùng để lưu xuống DB
            cboSanPham.SelectedIndex = 0; // Chọn giá trị đầu tiên


            // Load dữ liệu
            LoadChiTietHDN();
        }
        private void LoadChiTietHDN()
        {
            try
            {
                _danhSachCT = _chiTietHDNController.LayChiTietHDNTheoMa(_maHDN);
                if (_danhSachCT == null || !_danhSachCT.Any())
                {
                    dgvChiTiet.DataSource = null;
                    return;
                }

                dgvChiTiet.DataSource = _danhSachCT;
                ConfigureDataGridViewColumns();

                if (dgvChiTiet.Rows.Count > 0)
                {
                    dgvChiTiet.Rows[0].Selected = true;
                    DgvChiTiet_Click_1(null, null);
                }

                ResetValue();
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
                dgvChiTiet.Columns["MaCTHDN"].Width = 100;
            }
            if (dgvChiTiet.Columns.Contains("MaHDN"))
            {
                dgvChiTiet.Columns["MaHDN"].HeaderText = "Mã HĐN";
                dgvChiTiet.Columns["MaHDN"].Width = 100;
            }
            if (dgvChiTiet.Columns.Contains("MaSP"))
            {
                dgvChiTiet.Columns["MaSP"].HeaderText = "Mã SP";
                dgvChiTiet.Columns["MaSP"].Width = 80;
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
                dgvChiTiet.Columns["ThanhTien"].Width = 100;
            }

            dgvChiTiet.Columns["MaCTHDN"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvChiTiet.Columns["MaHDN"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvChiTiet.Columns["MaSP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvChiTiet.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChiTiet.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChiTiet.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvChiTiet.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvChiTiet.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvChiTiet.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvChiTiet.EnableHeadersVisualStyles = false;
            dgvChiTiet.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvChiTiet.RowTemplate.Height = 30;
            dgvChiTiet.RowHeadersVisible = false;
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra mã chi tiết đã tồn tại chưa
            string maCTHDN = txtMaCTHDN.Text.Trim();
            if (function.DatabaseHelper.CheckKey(
                "SELECT COUNT(*) FROM ChiTietHDN WHERE MaCTHDN = @MaCTHDN",
                new SqlParameter[] { new SqlParameter("@MaCTHDN", maCTHDN) }))
            {
                MessageBox.Show("Mã chi tiết hóa đơn nhập đã tồn tại. Vui lòng nhập mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maSP = cboSanPham.SelectedValue?.ToString();
                if (string.IsNullOrEmpty(maSP))
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboSanPham.Focus();
                    return;
                }

                if (!ValidateInput()) return;

                var existingChiTiet = _danhSachCT?.FirstOrDefault(ct => ct.MaSP == maSP);
                if (existingChiTiet != null)
                {
                    string tenSP = cboSanPham.Text;
                    MessageBox.Show($"Sản phẩm '{tenSP}' đã tồn tại trong hóa đơn nhập này. Vui lòng cập nhật số lượng nếu cần.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var chiTiet = new ChiTietHDNModel(
                    _maHDN,
                    maSP,
                    int.Parse(txtSoLuong.Text),
                    decimal.Parse(txtDonGia.Text)
                );

                if (_chiTietHDNController.ThemChiTietHDN(chiTiet))
                {
                    MessageBox.Show("Thêm chi tiết hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateTongTienHoaDonNhap();
                    LoadChiTietHDN();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetValue();
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (dgvChiTiet.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một chi tiết hóa đơn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maCTHDN = txtMaCTHDN.Text.Trim();
            if (string.IsNullOrEmpty(maCTHDN))
            {
                MessageBox.Show("Mã chi tiết hóa đơn không được để trống. Vui lòng chọn lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa chi tiết hóa đơn này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_chiTietHDNController.XoaChiTietHDN(maCTHDN))
                    {
                        MessageBox.Show("Xóa chi tiết hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateTongTienHoaDonNhap();
                        LoadChiTietHDN();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một chi tiết hóa đơn để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput()) return;

            try
            {
                var chiTiet = _danhSachCT.FirstOrDefault(ct => ct.MaCTHDN == txtMaCTHDN.Text);
                if (chiTiet == null)
                {
                    MessageBox.Show("Chi tiết hóa đơn không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                chiTiet.MaSP = cboSanPham.SelectedValue.ToString();
                chiTiet.SoLuong = int.Parse(txtSoLuong.Text);
                chiTiet.DonGia = decimal.Parse(txtDonGia.Text);

                if (_chiTietHDNController.CapNhatChiTietHDN(chiTiet))
                {
                    MessageBox.Show("Cập nhật chi tiết hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateTongTienHoaDonNhap();
                    LoadChiTietHDN();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvChiTiet_Click_1(object sender, EventArgs e)
        {
            if (dgvChiTiet.SelectedRows.Count > 0)
            {
                var selectedRow = dgvChiTiet.SelectedRows[0];
                txtMaCTHDN.Text = selectedRow.Cells["MaCTHDN"].Value?.ToString() ?? string.Empty;
                txtMaHDN.Text = selectedRow.Cells["MaHDN"].Value?.ToString() ?? string.Empty;
                cboSanPham.SelectedValue = selectedRow.Cells["MaSP"].Value?.ToString();
                txtSoLuong.Text = selectedRow.Cells["SoLuong"].Value?.ToString() ?? "0";
                txtDonGia.Text = selectedRow.Cells["DonGia"].Value?.ToString() ?? "0";
                txtThanhTien.Text = selectedRow.Cells["ThanhTien"].Value?.ToString() ?? "0";
            }
        }

        private void txtDonGia_TextChanged_1(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void txtSoLuong_TextChanged_1(object sender, EventArgs e)
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
            txtMaCTHDN.Text = string.Empty;
            txtMaHDN.Text = _maHDN;
            cboSanPham.SelectedIndex = -1;
            txtSoLuong.Text = "0";
            txtDonGia.Text = "0";
            txtThanhTien.Text = "0";
        }

        private bool ValidateInput()
        {
            if (cboSanPham.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboSanPham.Focus();
                return false;
            }

            if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return false;
            }

            if (!decimal.TryParse(txtDonGia.Text, out decimal donGia) || donGia <= 0)
            {
                MessageBox.Show("Đơn giá phải là số dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return false;
            }

            return true;
        }

        private void UpdateTongTienHoaDonNhap()
        {
            try
            {
                var chiTiets = _chiTietHDNController.LayChiTietHDNTheoMa(_maHDN);
                decimal tongTien = chiTiets?.Sum(ct => ct.ThanhTien) ?? 0;

                using (SqlConnection conn = function.DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE HoaDonNhap SET TongTien = @TongTien WHERE MaHDN = @MaHDN";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TongTien", tongTien);
                        cmd.Parameters.AddWithValue("@MaHDN", _maHDN);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật tổng tiền hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLammoi_Click_1(object sender, EventArgs e)
        {
            ResetValue();
        }

        private void dgvChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.CurrentRow == null || dgvChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dgvChiTiet.CurrentRow;
            txtMaCTHDN.Text = selectedRow.Cells["MaCTHDN"].Value?.ToString() ?? string.Empty;
            txtMaHDN.Text = selectedRow.Cells["MaHDN"].Value?.ToString() ?? string.Empty;
            cboSanPham.SelectedValue = selectedRow.Cells["MaSP"].Value?.ToString();
            txtSoLuong.Text = selectedRow.Cells["SoLuong"].Value?.ToString() ?? "0";
            txtDonGia.Text = selectedRow.Cells["DonGia"].Value?.ToString() ?? "0";
            txtThanhTien.Text = selectedRow.Cells["ThanhTien"].Value?.ToString() ?? "0";
        }
    }
}