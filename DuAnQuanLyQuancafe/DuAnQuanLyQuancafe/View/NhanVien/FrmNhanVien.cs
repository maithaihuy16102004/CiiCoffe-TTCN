using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.function;
using DuAnQuanLyQuancafe.Model;
using DuAnQuanLyQuancafe.View.NhanVien;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.View
{
    public partial class FrmNhanVien : Form
    {
        public FrmNhanVien()
        {
            InitializeComponent();
            LoadNhanVien();
        }
        //Lấy Quê từ CSDL trong quê Model
        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            function.DatabaseHelper.FillCombo("SELECT MaQue, TenQue FROM Que", cbQue, "MaQue", "TenQue");
            // Thêm các giá trị vào ComboBox giới tính
            cbGioiTinh.Items.Add("Nam");
            cbGioiTinh.Items.Add("Nữ");
            cbGioiTinh.Items.Add("Khác");
            cbGioiTinh.SelectedIndex = 0; // Chọn giá trị đầu tiên
        }
        private void ResetValue()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            txtMa.Focus();
            txtMa.Enabled = false;
            cbQue.SelectedIndex = -1; // Đặt lại giá trị của ComboBox quê
            cbGioiTinh.SelectedIndex = -1; // Đặt lại giá trị của ComboBox giới tính
        }
        public void LoadNhanVien()
        {
            if (dgvNhanVien == null)
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                NhanVienController nhanVienController = new NhanVienController();
                List<NhanVienModel> dsNhanVien = nhanVienController.LayDanhSachNhanVien();
                dgvNhanVien.DataSource = dsNhanVien;
                dgvNhanVien.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
                dgvNhanVien.Columns["TenNV"].HeaderText = "Tên Nhân Viên";
                dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
                dgvNhanVien.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvNhanVien.Columns["SDT"].HeaderText = "Số Điện Thoại";
                dgvNhanVien.Columns["MaQue"].HeaderText = "Mã Quê";
                dgvNhanVien.Columns["TenQue"].HeaderText = "Quê Quán";
               
                // Ẩn các cột không cần thiết
                dgvNhanVien.Columns["MaQue"].Visible = false;
                //dgvNhanVien.Columns["HinhAnh"].Visible = false; // Đảm bảo tên cột đúng
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            FrmAddNhanVien frmAddNhanVien = new FrmAddNhanVien();
            frmAddNhanVien.Show();
            // Khi form thêm nhân viên đóng, tải lại danh sách nhân viên
            frmAddNhanVien.FormClosed += (s, args) =>
            {
                LoadNhanVien();
            };
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                string id = dgvNhanVien.SelectedRows[0].Cells["MaNV"].Value.ToString();
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    NhanVienController nhanVienController = new NhanVienController();
                    nhanVienController.XoaNhanVien(id);
                    LoadNhanVien();
                }
            }
        }
        //Sua Nhan Vien
        byte[] anhDuocChon; // Biến lưu trữ ảnh dưới dạng byte[]
        private void btnChon_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png",
                Title = "Chọn ảnh đại diện",
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



        private void txtTimKiem_TextChanged_1(object sender, EventArgs e)
        {
            NhanVienController nhanVienController = new NhanVienController();
            List<NhanVienModel> dsNhanVien = nhanVienController.LayDanhSachNhanVien();
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim().ToLower();
                List<NhanVienModel> locNhanVien = dsNhanVien.FindAll(emp =>
                    emp.TenNV?.ToLower().Contains(tuKhoa) ?? false);

                dgvNhanVien.DataSource = null;
                dgvNhanVien.DataSource = locNhanVien;

                if (locNhanVien.Count > 0)
                {
                    // Thiết lập tiêu đề cột
                    dgvNhanVien.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
                    dgvNhanVien.Columns["TenNV"].HeaderText = "Tên Nhân Viên";
                    dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                    dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
                    dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
                    dgvNhanVien.Columns["SDT"].HeaderText = "Số Điện Thoại";
                    dgvNhanVien.Columns["MaQue"].HeaderText = "Mã Quê";
                    dgvNhanVien.Columns["TenQue"].HeaderText = "Tên Quê";
                    dgvNhanVien.Columns["HinhAnh"].Visible = false; // Sửa tên cột
                    dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvNhanVien_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow == null || dgvNhanVien.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            txtMa.Enabled = false; // Không cho phép sửa mã nhân viên
            try
            {
                NhanVienModel selectedNhanVien = dgvNhanVien.CurrentRow.DataBoundItem as NhanVienModel;
                if (selectedNhanVien == null)
                    return;

                txtMa.Text = selectedNhanVien.MaNV;
                txtTen.Text = selectedNhanVien.TenNV;
                txtDiaChi.Text = selectedNhanVien.DiaChi;
                cbGioiTinh.Text = selectedNhanVien.GioiTinh;
                cbQue.SelectedValue = selectedNhanVien.MaQue.ToString();
                dtpNgaySinh.Value = selectedNhanVien.NgaySinh;
                txtSDT.Text = selectedNhanVien.SDT;

                // Xử lý ảnh từ byte[]
                picAnh.SizeMode = PictureBoxSizeMode.Zoom;
                if (selectedNhanVien.Anh != null && selectedNhanVien.Anh.Length > 0)
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream(selectedNhanVien.Anh))
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
                MessageBox.Show($"Lỗi khi hiển thị thông tin nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picAnh_Click(object sender, EventArgs e)
        {

        }

        private void btn_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            txtMa.Enabled = false;
            btnSua.Enabled = true;
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn chưa
            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string id;
            try
            {
                id = dgvNhanVien.SelectedRows[0].Cells["MaNV"].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Không thể lấy Mã Nhân Viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string tenNV = txtTen.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string gioiTinh = cbGioiTinh.Text;
            string maQue = cbQue.SelectedValue?.ToString();
            DateTime ngaySinh = dtpNgaySinh.Value;

            // Kiểm tra thông tin
            if (string.IsNullOrWhiteSpace(tenNV) || string.IsNullOrWhiteSpace(diaChi) ||
                string.IsNullOrWhiteSpace(sdt) || string.IsNullOrWhiteSpace(gioiTinh))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(maQue))
            {
                MessageBox.Show("Vui lòng chọn quê quán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Hashtable parameter = new Hashtable
    {
        { "MaNV", id },
        { "TenNV", tenNV },
        { "DiaChi", diaChi },
        { "SDT", sdt },
        { "GioiTinh", gioiTinh },
        { "MaQue", maQue },
        { "NgaySinh", ngaySinh },
        { "HinhAnh", anhDuocChon } // Sử dụng ảnh đã chọn (nếu có)
    };

            try
            {
                NhanVienController nhanVienController = new NhanVienController();
                nhanVienController.SuaNhanVien(parameter);
                MessageBox.Show("Sửa nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
