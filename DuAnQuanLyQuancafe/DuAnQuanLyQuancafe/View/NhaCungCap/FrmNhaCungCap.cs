using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.Model;
using DuAnQuanLyQuancafe.View.NhanVien;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace DuAnQuanLyQuancafe.View.NhaCungCap
{
    public partial class FrmNhaCungCap : Form
    {
        private readonly NhaCungCapController _nhaCungCapController;

        public FrmNhaCungCap()
        {
            InitializeComponent();
            _nhaCungCapController = new NhaCungCapController();
            Load_NhaCC();
        }

        public void Load_NhaCC()
        {
            try
            {
                List<NhaCungCapModel> dsNhaCC = _nhaCungCapController.LayDanhSachNhaCC();
                if (dsNhaCC == null || dsNhaCC.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu nhà cung cấp để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvNhaCC.DataSource = null;
                    return;
                }

                dgvNhaCC.DataSource = dsNhaCC;
                dgvNhaCC.Columns["MaNCC"].HeaderText = "Mã Nhà Cung Cấp";
                dgvNhaCC.Columns["TenNCC"].HeaderText = "Tên Nhà Cung Cấp";
                dgvNhaCC.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                dgvNhaCC.Columns["SDT"].HeaderText = "Số Điện Thoại";
                dgvNhaCC.Columns["MoTa"].HeaderText = "Mô Tả";
                // Tùy chỉnh kích thước cột nếu cần
                // Căn chỉnh nội dung cột
                dgvNhaCC.Columns["MaNCC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNhaCC.Columns["TenNCC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvNhaCC.Columns["DiaChi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvNhaCC.Columns["SDT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNhaCC.Columns["MoTa"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                // Tùy chỉnh giao diện tiêu đề cột
                dgvNhaCC.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80); // Màu xanh đậm
                dgvNhaCC.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvNhaCC.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvNhaCC.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNhaCC.EnableHeadersVisualStyles = false;

                // Tùy chỉnh giao diện hàng
                dgvNhaCC.DefaultCellStyle.BackColor = Color.White;
                dgvNhaCC.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255); // Màu xanh nhạt xen kẽ
                dgvNhaCC.DefaultCellStyle.Font = new Font("Segoe UI", 9);
                dgvNhaCC.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219); // Màu xanh dương khi chọn
                dgvNhaCC.DefaultCellStyle.SelectionForeColor = Color.White;

                // Điều chỉnh chiều cao hàng
                dgvNhaCC.RowTemplate.Height = 35;

                // Tùy chỉnh viền và lưới
                dgvNhaCC.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvNhaCC.GridColor = Color.FromArgb(200, 200, 200);

                // Tắt các tính năng không cần thiết
                dgvNhaCC.ReadOnly = true; // Không cho phép chỉnh sửa
                dgvNhaCC.RowHeadersVisible = false; // Ẩn cột chọn hàng
                dgvNhaCC.AllowUserToResizeColumns = false; // Không cho phép thay đổi kích thước cột
                dgvNhaCC.AllowUserToResizeRows = false; // Không cho phép thay đổi kích thước hàng

                // Đảm bảo DataGridView không có viền thừa
                dgvNhaCC.BorderStyle = BorderStyle.None;
                dgvNhaCC.BackgroundColor = Color.White;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetValue()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtMoTa.Text = "";
            txtMa.Enabled = false; // Không cho phép sửa mã
            txtMa.Focus();
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            FrmAddNCC addNCC = new FrmAddNCC();
            addNCC.Show();
            // Khi form thêm nhân viên đóng, tải lại danh sách 
            addNCC.FormClosed += (s, args) =>
            {
                Load_NhaCC();
            };
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhaCC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maNCC = dgvNhaCC.SelectedRows[0].Cells["MaNCC"]?.Value?.ToString();
            if (string.IsNullOrEmpty(maNCC))
            {
                MessageBox.Show("Không thể lấy mã nhà cung cấp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    _nhaCungCapController.XoaNhaCungCap(maNCC);
                    Load_NhaCC();
                    ResetValue();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvNhaCC_Click(object sender, EventArgs e)
        {
            if (dgvNhaCC.CurrentRow == null || dgvNhaCC.Rows.Count == 0)
            {
                ResetValue();
                return;
            }

            try
            {
                txtMa.Enabled = false; // Không cho phép sửa mã
                txtMa.Text = dgvNhaCC.CurrentRow.Cells["MaNCC"]?.Value?.ToString() ?? "";
                txtTen.Text = dgvNhaCC.CurrentRow.Cells["TenNCC"]?.Value?.ToString() ?? "";
                txtDiaChi.Text = dgvNhaCC.CurrentRow.Cells["DiaChi"]?.Value?.ToString() ?? "";
                txtSDT.Text = dgvNhaCC.CurrentRow.Cells["SDT"]?.Value?.ToString() ?? "";
                txtMoTa.Text = dgvNhaCC.CurrentRow.Cells["MoTa"]?.Value?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị thông tin nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim().ToLower();
                List<NhaCungCapModel> dsNhaCC = _nhaCungCapController.LayDanhSachNhaCC();

                // Tìm kiếm theo TenNCC
                List<NhaCungCapModel> locNhaCC = dsNhaCC.FindAll(ncc =>
                    ncc.TenNCC?.ToLower().Contains(tuKhoa) ?? false);

                // Cập nhật DataGridView
                dgvNhaCC.DataSource = null; // Xóa DataSource trước để tránh lỗi hiển thị
                dgvNhaCC.DataSource = locNhaCC;

                // Cập nhật tiêu đề cột nếu danh sách không rỗng
                if (locNhaCC.Count > 0)
                {
                    dgvNhaCC.Columns["MaNCC"].HeaderText = "Mã Nhà Cung Cấp";
                    dgvNhaCC.Columns["TenNCC"].HeaderText = "Tên Nhà Cung Cấp";
                    dgvNhaCC.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                    dgvNhaCC.Columns["SDT"].HeaderText = "Số Điện Thoại";
                    dgvNhaCC.Columns["MoTa"].HeaderText = "Mô Tả";
                    dgvNhaCC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhà cung cấp nào phù hợp với từ khóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            if (dgvNhaCC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maNCC = txtMa.Text.Trim();
            string tenNCC = txtTen.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string moTa = txtMoTa.Text.Trim();

            // Kiểm tra thông tin đầu vào
            if (string.IsNullOrWhiteSpace(maNCC))
            {
                MessageBox.Show("Mã nhà cung cấp không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMa.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tenNCC))
            {
                MessageBox.Show("Tên nhà cung cấp không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(diaChi))
            {
                MessageBox.Show("Địa chỉ không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(sdt) || sdt.Length < 10 || !sdt.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại phải có ít nhất 10 chữ số và chỉ chứa số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            Hashtable parameter = new Hashtable
            {
                { "MaNCC", maNCC },
                { "TenNCC", tenNCC },
                { "DiaChi", diaChi },
                { "SDT", sdt },
                { "MoTa", string.IsNullOrEmpty(moTa) ? null : moTa }
            };

            try
            {
                _nhaCungCapController.SuaNCC(parameter);
                Load_NhaCC();
                ResetValue();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetValue();
        }
    }
}