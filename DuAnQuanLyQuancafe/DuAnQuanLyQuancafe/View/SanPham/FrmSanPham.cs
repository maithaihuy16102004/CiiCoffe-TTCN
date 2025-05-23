using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.Model;
using DuAnQuanLyQuancafe.View.SanPham;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.View
{
    public partial class FrmSanPham : Form
    {
        private readonly SanPhamController _sanPhamController;
        private List<SanPhamModel> _dsSanPham; // Lưu danh sách sản phẩm để tái sử dụng
        byte[] anhDuocChon; // Biến lưu trữ ảnh dưới dạng byte[]
        public FrmSanPham()
        {
            InitializeComponent();
            _sanPhamController = new SanPhamController();
            _dsSanPham = new List<SanPhamModel>();
            function.DatabaseHelper.FillCombo("SELECT MaLoai, TenLoai FROM Loai", cbLoai, "MaLoai", "TenLoai");
            function.DatabaseHelper.FillCombo("SELECT MaCongDung, TenCongDung FROM CongDung", cbCongDung, "MaCongDung", "TenCongDung");
            cbLoai.SelectedIndex = -1;
            cbCongDung.SelectedIndex = -1;
        }

        private void FrmSanPham_Load(object sender, EventArgs e)
        {
            LoadSanPham();
        }

        private void LoadSanPham()
        {
            try
            {
                _dsSanPham = _sanPhamController.LayDanhSachSanPham();
                dgvSanPham.DataSource = null;
                if (_dsSanPham == null || _dsSanPham.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                dgvSanPham.DataSource = _dsSanPham;
                dgvSanPham.Columns["MaSP"].HeaderText = "Mã Sản Phẩm";
                dgvSanPham.Columns["TenSP"].HeaderText = "Tên Sản Phẩm";
                dgvSanPham.Columns["TenLoai"].HeaderText = "Tên Loại"; // Sửa nhãn
                dgvSanPham.Columns["GiaNhap"].HeaderText = "Giá Nhập";
                dgvSanPham.Columns["GiaBan"].HeaderText = "Giá Bán";
                dgvSanPham.Columns["SoLuong"].HeaderText = "Số Lượng";
                dgvSanPham.Columns["TenCongDung"].HeaderText = "Công Dụng";
                // Ẩn các cột không cần thiết
                dgvSanPham.Columns["MaLoai"].Visible = false;
                dgvSanPham.Columns["MaCongDung"].Visible = false;
                dgvSanPham.Columns["HinhAnh"].Visible = false;
                dgvSanPham.Columns["MaSP"].Width = 50;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void resetValue()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtGiaNhap.Text = "";
            txtGiaBan.Text = "";
            txtSoluong.Text = "";
            cbLoai.SelectedIndex = -1;
            cbCongDung.SelectedIndex = -1;
            picAnh.Image = null;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            FrmAddSanPham frmAddSanPham = new FrmAddSanPham();
            frmAddSanPham.FormClosed += (s, args) => LoadSanPham();
            frmAddSanPham.ShowDialog();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string id = dgvSanPham.SelectedRows[0].Cells["MaSP"].Value?.ToString();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Mã sản phẩm không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    _sanPhamController.XoaSanPham(id);
                    MessageBox.Show("Xóa sản phẩm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSanPham();
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvSanPham_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow == null || dgvSanPham.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            txtMa.Enabled = false; // Không cho sửa mã
            try
            {
                SanPhamModel selectedSanPham = dgvSanPham.CurrentRow.DataBoundItem as SanPhamModel;
                if (selectedSanPham == null)
                    return;

                txtMa.Text = selectedSanPham.MaSP;
                txtTen.Text = selectedSanPham.TenSP;
                cbLoai.SelectedValue = selectedSanPham.MaLoai.ToString();
                txtGiaNhap.Text = selectedSanPham.GiaNhap.ToString();
                txtGiaBan.Text = selectedSanPham.GiaBan.ToString();
                txtSoluong.Text = selectedSanPham.SoLuong.ToString();
                cbCongDung.SelectedValue = selectedSanPham.MaCongDung.ToString();

                // Xử lý ảnh từ byte[]
                picAnh.SizeMode = PictureBoxSizeMode.Zoom;
                if (selectedSanPham.HinhAnh != null && selectedSanPham.HinhAnh.Length > 0)
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream(selectedSanPham.HinhAnh))
                        {
                            picAnh.Image = Image.FromStream(ms);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi tải ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        picAnh.Image = null;
                    }
                }
                else
                {
                    picAnh.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị thông tin sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim().ToLower();
                List<SanPhamModel> locSanPham = _dsSanPham.FindAll(emp => emp.TenSP?.ToLower().Contains(tuKhoa) ?? false);
                dgvSanPham.DataSource = null;
                dgvSanPham.DataSource = locSanPham;
                // Thiết lập lại tiêu đề cột
                if (locSanPham.Count > 0)
                {
                    dgvSanPham.Columns["MaSP"].HeaderText = "Mã Sản Phẩm";
                    dgvSanPham.Columns["TenSP"].HeaderText = "Tên Sản Phẩm";
                    dgvSanPham.Columns["TenLoai"].HeaderText = "Tên Loại";
                    dgvSanPham.Columns["GiaNhap"].HeaderText = "Giá Nhập";
                    dgvSanPham.Columns["GiaBan"].HeaderText = "Giá Bán";
                    dgvSanPham.Columns["SoLuong"].HeaderText = "Số Lượng";
                    dgvSanPham.Columns["TenCongDung"].HeaderText = "Công Dụng";
                    dgvSanPham.Columns["MaLoai"].Visible = false;
                    dgvSanPham.Columns["MaCongDung"].Visible = false;
                    dgvSanPham.Columns["HinhAnh"].Visible = false;
                    dgvSanPham.Columns["MaSP"].Width = 50;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            if (dgvSanPham.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string id = dgvSanPham.SelectedRows[0].Cells["MaSP"].Value?.ToString();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Mã sản phẩm không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string tenSP = txtTen.Text.Trim();
            string maLoai = cbLoai.SelectedValue?.ToString();
            string giaNhapText = txtGiaNhap.Text.Trim();
            string giaBanText = txtGiaBan.Text.Trim();
            string soLuongText = txtSoluong.Text.Trim();
            string maCongDung = cbCongDung.SelectedValue?.ToString();

            // Kiểm tra thông tin
            if (string.IsNullOrEmpty(tenSP))
            {
                MessageBox.Show("Tên sản phẩm không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            if (string.IsNullOrEmpty(maLoai))
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbLoai.Focus();
                return;
            }

            if (!float.TryParse(giaNhapText, out float giaNhap) || giaNhap < 0)
            {
                MessageBox.Show("Giá nhập phải là số không âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaNhap.Focus();
                return;
            }

            if (!float.TryParse(giaBanText, out float giaBan) || giaBan < 0)
            {
                MessageBox.Show("Giá bán phải là số không âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaBan.Focus();
                return;
            }

            if (!int.TryParse(soLuongText, out int soLuong) || soLuong < 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên không âm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoluong.Focus();
                return;
            }

            if (string.IsNullOrEmpty(maCongDung))
            {
                MessageBox.Show("Vui lòng chọn công dụng sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbCongDung.Focus();
                return;
            }

            Hashtable parameter = new Hashtable
    {
        { "MaSP", id },
        { "TenSP", tenSP },
        { "MaLoai", maLoai },
        { "GiaNhap", giaNhap },
        { "GiaBan", giaBan },
        { "SoLuong", soLuong },
        { "MaCongDung", maCongDung },
        { "HinhAnh", anhDuocChon } // Sử dụng anhDuocChon nếu đã chọn ảnh mới
    };

            try
            {
                _sanPhamController.CapNhatSanPham(parameter);
                MessageBox.Show("Sửa sản phẩm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSanPham();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi sửa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picAnhSP_Click(object sender, EventArgs e)
        {
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png",
                Title = "Chọn ảnh sản phẩm",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                // Hiển thị ảnh lên PictureBox
                picAnh.Image = Image.FromFile(dlgOpen.FileName);

                // Chuyển ảnh sang byte[] để lưu vào CSDL
                anhDuocChon = File.ReadAllBytes(dlgOpen.FileName);
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn ảnh nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            resetValue();
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            txtMa.Enabled = false;
            LoadSanPham();
        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}