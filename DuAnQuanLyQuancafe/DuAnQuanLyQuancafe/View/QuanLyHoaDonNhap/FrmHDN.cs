using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.Model;
using DuAnQuanLyQuancafe.View.QuanLyHoaDonNhap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.View
{
    public partial class FrmHDN : Form
    {
        private readonly HoaDonNhapController _hdnController = new HoaDonNhapController();

        public FrmHDN()
        {
            InitializeComponent();
            LoadHDN();
            dgvHDN.CellDoubleClick += dgvHDN_CellDoubleClick; // Đăng ký sự kiện CellDoubleClick
            function.DatabaseHelper.FillCombo("SELECT MaNCC, TenNCC FROM NhaCungCap", cbNCC, "MaNCC", "TenNCC");
            function.DatabaseHelper.FillCombo("SELECT MaNV, TenNV FROM NhanVien", cbNhanVien, "MaNV", "TenNV");
            txtTimKiem.TextChanged += FrmHDN_TextChanged; // Đăng ký sự kiện TextChanged cho tìm kiếm
        }

        private void LoadHDN()
        {
            try
            {
                List<HoaDonNhapModel> dsachHDN = _hdnController.LayDanhSachHDN();
                if (dsachHDN == null || !dsachHDN.Any())
                {
                    dgvHDN.DataSource = null;
                    MessageBox.Show("Không có dữ liệu hóa đơn nhập để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dgvHDN.DataSource = dsachHDN;
                dgvHDN.Columns["MaHDN"].HeaderText = "Mã Hóa Đơn Nhập";
                dgvHDN.Columns["NgayNhap"].HeaderText = "Ngày Nhập";
                dgvHDN.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
                dgvHDN.Columns["MaNCC"].HeaderText = "Mã Nhà Cung Cấp";
                dgvHDN.Columns["TongTien"].HeaderText = "Tổng Tiền";
                // Định dạng cột Tổng Tiền
                dgvHDN.Columns["TongTien"].DefaultCellStyle.Format = "N0";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            FrmAddHDN frmAddHDN = new FrmAddHDN();
            frmAddHDN.ShowDialog(); // Sử dụng ShowDialog để chờ người dùng hoàn tất
            frmAddHDN.FormClosed += (s, args) =>
            {
                LoadHDN(); // Tải lại dữ liệu sau khi thêm
            };
        }

        private void dgvHDN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvHDN.Rows[e.RowIndex].Cells["MaHDN"].Value != null)
            {
                string maHDN = dgvHDN.Rows[e.RowIndex].Cells["MaHDN"].Value.ToString();
                FrmChitietHDN frmChiTiet = new FrmChitietHDN(maHDN);
                frmChiTiet.ShowDialog(); // Sử dụng ShowDialog để kiểm soát form chi tiết
                LoadHDN(); // Tải lại dữ liệu sau khi xem chi tiết (nếu có thay đổi)
            }
        }

        private void FrmHDN_TextChanged(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(tuKhoa))
            {
                LoadHDN(); // Tải lại toàn bộ nếu không có từ khóa
                return;
            }

            try
            {
                List<HoaDonNhapModel> locHDN = _hdnController.TimKiemHDN(tuKhoa);
                if (locHDN == null || !locHDN.Any())
                {
                    dgvHDN.DataSource = null;
                    MessageBox.Show("Không tìm thấy hóa đơn nhập nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvHDN.DataSource = locHDN;
                dgvHDN.Columns["MaHDN"].HeaderText = "Mã Hóa Đơn Nhập";
                dgvHDN.Columns["NgayNhap"].HeaderText = "Ngày Nhập";
                dgvHDN.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
                dgvHDN.Columns["MaNCC"].HeaderText = "Mã Nhà Cung Cấp";
                dgvHDN.Columns["TongTien"].HeaderText = "Tổng Tiền";
                dgvHDN.Columns["TongTien"].DefaultCellStyle.Format = "N0";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHDN.SelectedRows.Count > 0)
            {
                string maHDN = dgvHDN.SelectedRows[0].Cells["MaHDN"].Value.ToString();
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa hóa đơn {maHDN} không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        _hdnController.XoaHDN(maHDN);
                        LoadHDN();
                        MessageBox.Show("Xóa hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvHDN_Click(object sender, EventArgs e)
        {
            if (dgvHDN.CurrentRow == null || dgvHDN.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtMa.Enabled = false; // Không cho phép sửa mã
            txtMa.Text = dgvHDN.CurrentRow.Cells["MaHDN"]?.Value?.ToString() ?? string.Empty;
            cbNCC.SelectedValue = dgvHDN.CurrentRow.Cells["MaNCC"]?.Value?.ToString() ?? string.Empty;
            cbNhanVien.SelectedValue = dgvHDN.CurrentRow.Cells["MaNV"]?.Value?.ToString() ?? string.Empty;
            txttongtien.Text = dgvHDN.CurrentRow.Cells["TongTien"]?.Value?.ToString() ?? "0";
            dtpNgayNhap.Value = DateTime.TryParse(dgvHDN.CurrentRow.Cells["NgayNhap"]?.Value?.ToString(), out DateTime ngayNhap) ? ngayNhap : DateTime.Now;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMa.Text) || dgvHDN.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tạo Hashtable để truyền dữ liệu
                Hashtable parameter = new Hashtable
                {
                    ["MaHDN"] = txtMa.Text,
                    ["NgayNhap"] = dtpNgayNhap.Value,
                    ["MaNV"] = cbNhanVien.SelectedValue?.ToString(),
                    ["MaNCC"] = cbNCC.SelectedValue?.ToString(),
                    ["TongTien"] = decimal.TryParse(txttongtien.Text, out decimal tongTien) ? tongTien : 0
                };

                // Thực hiện cập nhật (chưa có phương thức cập nhật trong model, cần thêm)
                MessageBox.Show("Chức năng sửa chưa được triển khai. Vui lòng thêm phương thức cập nhật trong HoaDonNhapModel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadHDN(); // Gọi lại sau khi cập nhật (khi có phương thức)
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picAnh_Click(object sender, EventArgs e)
        {
            // Chưa có chức năng, có thể thêm logic xử lý ảnh nếu cần
        }
    }
}