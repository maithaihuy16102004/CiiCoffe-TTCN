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
                dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
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
                // Điều chỉnh chiều rộng cột để hiển thị đầy đủ dữ liệu
                dgvNhanVien.Columns["MaNV"].Width = 50;
                dgvNhanVien.Columns["TenNV"].Width = 130;
                dgvNhanVien.Columns["DiaChi"].Width = 150; // Tăng chiều rộng để hiển thị địa chỉ dài
                dgvNhanVien.Columns["GioiTinh"].Width = 80;
                dgvNhanVien.Columns["NgaySinh"].Width = 100;
                dgvNhanVien.Columns["SDT"].Width = 100; // Tăng chiều rộng để hiển thị số điện thoại đầy đủ
                dgvNhanVien.Columns["TenQue"].Width = 100; // Tăng chiều rộng cho Quê Quán

                // Căn chỉnh nội dung cột
                dgvNhanVien.Columns["MaNV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNhanVien.Columns["TenNV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvNhanVien.Columns["DiaChi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvNhanVien.Columns["GioiTinh"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNhanVien.Columns["NgaySinh"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNhanVien.Columns["SDT"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNhanVien.Columns["TenQue"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                // Tùy chỉnh giao diện tiêu đề cột
                dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80); // Màu xanh đậm
                dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvNhanVien.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvNhanVien.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNhanVien.EnableHeadersVisualStyles = false;

                // Tùy chỉnh giao diện hàng
                dgvNhanVien.DefaultCellStyle.BackColor = Color.White;
                dgvNhanVien.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255); // Màu xanh nhạt xen kẽ
                dgvNhanVien.DefaultCellStyle.Font = new Font("Segoe UI", 9);
                dgvNhanVien.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
                dgvNhanVien.DefaultCellStyle.SelectionForeColor = Color.White;

                // Điều chỉnh chiều cao hàng
                dgvNhanVien.RowTemplate.Height = 45;

                // Tùy chỉnh viền và lưới
                dgvNhanVien.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvNhanVien.GridColor = Color.FromArgb(200, 200, 200);

                // Tắt các tính năng không cần thiết
                dgvNhanVien.ReadOnly = true;
                dgvNhanVien.RowHeadersVisible = false;
                dgvNhanVien.AllowUserToResizeColumns = false;
                dgvNhanVien.AllowUserToResizeRows = false;

                // Đảm bảo DataGridView không có viền thừa
                dgvNhanVien.BorderStyle = BorderStyle.None;
                dgvNhanVien.BackgroundColor = Color.White;
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
                    dgvNhanVien.Columns["TenQue"].HeaderText = "Quê quán";


                    dgvNhanVien.Columns["MaQue"].Visible = false;
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

            // Kiểm tra ngày sinh không được lớn hơn ngày hiện tại
            DateTime ngayHienTai = DateTime.Now;
            if (ngaySinh > ngayHienTai)
            {
                MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra nhân viên đủ 18 tuổi
            int tuoi = ngayHienTai.Year - ngaySinh.Year;
            if (ngayHienTai.Month < ngaySinh.Month || (ngayHienTai.Month == ngaySinh.Month && ngayHienTai.Day < ngaySinh.Day))
            {
                tuoi--; // Giảm tuổi đi 1 nếu chưa tới ngày sinh nhật trong năm nay
            }

            if (tuoi < 18)
            {
                MessageBox.Show("Nhân viên phải đủ 18 tuổi để đi làm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
