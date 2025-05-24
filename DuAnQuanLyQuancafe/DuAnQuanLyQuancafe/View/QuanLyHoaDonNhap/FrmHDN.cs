using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.Model;
using DuAnQuanLyQuancafe.View.QuanLyHoaDonNhap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.View
{
    public partial class FrmHDN : Form
    {
        private readonly HoaDonNhapController _hdnController = new HoaDonNhapController();
        private readonly ChiTietHDNController _chiTietHDNController = new ChiTietHDNController();

        public FrmHDN()
        {
            InitializeComponent();
            LoadHDN();
            dgvHDN.CellDoubleClick += dgvHDN_CellDoubleClick; // Đăng ký sự kiện CellDoubleClick
            function.DatabaseHelper.FillCombo("SELECT MaNCC, TenNCC FROM NhaCungCap", cbNCC, "MaNCC", "TenNCC");
            function.DatabaseHelper.FillCombo("SELECT MaNV, TenNV FROM NhanVien", cbNhanVien, "MaNV", "TenNV");
            txtTimKiem.TextChanged += FrmHDN_TextChanged; // Đăng ký sự kiện TextChanged cho tìm kiếm

            // Tự động chọn dòng đầu tiên nếu có dữ liệu
            if (dgvHDN.Rows.Count > 0)
            {
                dgvHDN.Rows[0].Selected = true;
                dgvHDN_Click(null, null);
            }
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
                ConfigureDataGridViewColumns(dgvHDN); // Gọi phương thức cấu hình cột
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridViewColumns(DataGridView dgv)
        {
            dgv.Columns["MaHDN"].HeaderText = "Mã Hóa Đơn Nhập";
            dgv.Columns["NgayNhap"].HeaderText = "Ngày Nhập";
            dgv.Columns["TenNV"].HeaderText = "Tên Nhân Viên";
            dgv.Columns["TenNCC"].HeaderText = "Tên Nhà Cung Cấp";
            dgv.Columns["TongTien"].HeaderText = "Tổng Tiền";
            dgv.Columns["TongTien"].DefaultCellStyle.Format = "N0";
            dgv.Columns["MaNCC"].Visible = false;
            dgv.Columns["MaNV"].Visible = false;
            dgv.Columns["MaHDN"].HeaderText = "Mã Hóa Đơn Nhập";
            dgv.Columns["NgayNhap"].HeaderText = "Ngày Nhập";
            dgv.Columns["TenNV"].HeaderText = "Tên Nhân Viên";
            dgv.Columns["TenNCC"].HeaderText = "Tên Nhà Cung Cấp";
            dgv.Columns["TongTien"].HeaderText = "Tổng Tiền";

            // Ẩn các cột không cần thiết
            dgv.Columns["MaNCC"].Visible = false;
            dgv.Columns["MaNV"].Visible = false;

            // Định dạng dữ liệu
            dgv.Columns["TongTien"].DefaultCellStyle.Format = "N0"; // Định dạng số tiền với dấu phân cách hàng nghìn
            dgv.Columns["NgayNhap"].DefaultCellStyle.Format = "dd/MM/yyyy"; // Định dạng ngày tháng

            // Điều chỉnh chiều rộng cột
            dgv.Columns["MaHDN"].Width = 120;
            dgv.Columns["NgayNhap"].Width = 100;
            dgv.Columns["TenNV"].Width = 150;
            dgv.Columns["TenNCC"].Width = 200;
            dgv.Columns["TongTien"].Width = 120;

            // Căn chỉnh nội dung cột
            dgv.Columns["MaHDN"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["NgayNhap"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns["TenNV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns["TenNCC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Tùy chỉnh giao diện tiêu đề cột
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80); // Màu xanh đậm
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.EnableHeadersVisualStyles = false;

            // Tùy chỉnh giao diện hàng
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255); // Màu xanh nhạt xen kẽ
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219); // Màu xanh dương khi chọn
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            // Điều chỉnh chiều cao hàng
            dgv.RowTemplate.Height = 35;

            // Tùy chỉnh viền và lưới
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(200, 200, 200);

            // Tắt các tính năng không cần thiết
            dgv.ReadOnly = true; // Không cho phép chỉnh sửa
            dgv.RowHeadersVisible = false; // Ẩn cột chọn hàng
            dgv.AllowUserToResizeColumns = false; // Không cho phép thay đổi kích thước cột
            dgv.AllowUserToResizeRows = false; // Không cho phép thay đổi kích thước hàng

            // Đảm bảo DataGridView không có viền thừa
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 45;
            dgv.RowTemplate.Height = 45;

        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            FrmAddHDN frmAddHDN = new FrmAddHDN();
            frmAddHDN.Show();
            frmAddHDN.FormClosed += (s, args) =>
            {
                LoadHDN();
            };
        }

        private void dgvHDN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvHDN.Rows.Count)
            {
                if (dgvHDN.Rows[e.RowIndex].Cells["MaHDN"].Value != null)
                {
                    string maHDN = dgvHDN.Rows[e.RowIndex].Cells["MaHDN"].Value.ToString();
                    OpenChiTietHDNForm(maHDN);
                }
                else
                {
                    MessageBox.Show("Mã hóa đơn nhập không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhấp đúp vào một dòng hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void OpenChiTietHDNForm(string maHDN)
        {
            try
            {
                // Kiểm tra dữ liệu chi tiết trước khi mở form
                List<ChiTietHDNModel> danhSachCT = _chiTietHDNController.LayChiTietHDNTheoMa(maHDN);
                FrmChitietHDN frmChiTiet = new FrmChitietHDN(maHDN);
                frmChiTiet.ShowDialog(); // Mở form chi tiết dưới dạng dialog
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở chi tiết hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                LoadHDN(); // Tải lại dữ liệu sau khi đóng form (nếu có thay đổi)
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
                ConfigureDataGridViewColumns(dgvHDN); // Gọi phương thức cấu hình cột

                // Tự động chọn dòng đầu tiên sau khi tìm kiếm
                if (dgvHDN.Rows.Count > 0)
                {
                    dgvHDN.Rows[0].Selected = true;
                    dgvHDN_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(txtMa.Text) || dgvHDN.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra các trường bắt buộc
            if (cbNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbNhanVien.Focus();
                return;
            }

            if (cbNCC.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbNCC.Focus();
                return;
            }

            if (!decimal.TryParse(txttongtien.Text.Trim(), out decimal tongTien) || tongTien < 0)
            {
                MessageBox.Show("Tổng tiền không hợp lệ. Vui lòng nhập số dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txttongtien.Focus();
                return;
            }

            // Kiểm tra ngày nhập không được lớn hơn ngày hiện tại
            if (dtpNgayNhap.Value.Date > DateTime.Now.Date)
            {
                MessageBox.Show("Ngày nhập không được lớn hơn ngày hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tạo Hashtable để truyền dữ liệu
                Hashtable parameter = new Hashtable
                {
                    ["MaHDN"] = txtMa.Text.Trim(),
                    ["NgayNhap"] = dtpNgayNhap.Value,
                    ["MaNV"] = cbNhanVien.SelectedValue.ToString(),
                    ["MaNCC"] = cbNCC.SelectedValue.ToString(),
                    ["TongTien"] = tongTien
                };

                // Gọi phương thức cập nhật từ Controller
                _hdnController.CapNhatHDN(parameter);
                MessageBox.Show("Cập nhật hóa đơn nhập thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadHDN(); // Làm mới danh sách sau khi cập nhật
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (dgvHDN.SelectedRows.Count > 0)
            {
                string id = dgvHDN.SelectedRows[0].Cells["MaHDN"].Value.ToString();
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn nhập này không? Thao tác này sẽ xóa cả chi tiết hóa đơn liên quan.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        _hdnController.XoaHDN(id);
                        MessageBox.Show("Xóa hóa đơn nhập thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadHDN();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}