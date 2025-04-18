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
            List<QueModel> danhSachQue = QueController.LayMaQue(); // hoặc gọi trực tiếp từ lớp chứa LayMaQue()
            cbQue.DataSource = danhSachQue;
            cbQue.DisplayMember = "TenQue"; // Hiển thị cho người dùng
            cbQue.ValueMember = "MaQue";    // Dùng để lưu xuống 
            // Thêm các giá trị vào ComboBox giới tính
            cbGioiTinh.Items.Add("Nam");
            cbGioiTinh.Items.Add("Nữ");
            cbGioiTinh.Items.Add("Khác");
            cbGioiTinh.SelectedIndex = 0; // Chọn giá trị đầu tiên
        }
        public void LoadNhanVien()
        {
            if (dgvNhanVien == null)
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.");
                return;
            }
            else
            {
                NhanVienController nhanVienController = new NhanVienController();
                List<NhanVienModel> dsNhanVien = nhanVienController.LaydanhsachNhanVien();
                List<QueModel> dsQue = QueController.LayMaQue();
                dgvNhanVien.DataSource = dsNhanVien;
                dgvNhanVien.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
                dgvNhanVien.Columns["TenNV"].HeaderText = "Tên Nhân Viên";
                dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
                dgvNhanVien.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvNhanVien.Columns["SDT"].HeaderText = "Số Điện Thoại";
                dgvNhanVien.Columns["MaQue"].HeaderText = "Mã Quê";
                dgvNhanVien.Columns["Anh"].HeaderText = "Ảnh Nhân Viên";
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
                    MessageBox.Show("Xóa nhân viên thành công.");
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn chưa
            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id;
            try
            {
                id = Convert.ToInt32(dgvNhanVien.SelectedRows[0].Cells["MaNV"].Value);
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

            if (anhDuocChon == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                { "Anh", anhDuocChon }
             };

            try
            {
                NhanVienController nhanVienController = new NhanVienController();
                nhanVienController.SuaNhanVien(parameter);
                LoadNhanVien();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTimKiem_TextChanged_1(object sender, EventArgs e)
        {
            string tukhoa = txtTimKiem.Text.ToLower();
            NhanVienController nhanVienController = new NhanVienController();
            // Lọc danh sách nhân viên chứa từ khóa
            List<NhanVienModel> locnhanvien = nhanVienController.LaydanhsachNhanVien().FindAll(emp => emp.TenNV.ToLower().Contains(tukhoa)).ToList();
            // Cập nhật DataGridView
            dgvNhanVien.DataSource = null;
            dgvNhanVien.DataSource = locnhanvien;
        }

        //private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dgvNhanVien.SelectedRows.Count > 0)
        //    {
        //        string pathAnh = dgvNhanVien.SelectedRows[0].Cells["Anh"].Value?.ToString();

        //        if (!string.IsNullOrEmpty(pathAnh) && File.Exists(pathAnh))
        //        {
        //            picAnhNhanVien.Image = Image.FromFile(pathAnh);
        //        }
        //        else
        //        {
        //            picAnhNhanVien.Image = null; // hoặc ảnh mặc định
        //        }
        //    }
        //}

        //private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        //{
        //    if (dgvNhanVien.SelectedRows.Count > 0)
        //    {
        //        // Lấy giá trị đường dẫn ảnh từ cột "Anh"
        //        var cellValue = dgvNhanVien.SelectedRows[0].Cells["Anh"].Value;
        //        if (cellValue != null)
        //        {
        //            string pathAnh = cellValue.ToString();

        //            // Kiểm tra xem file có tồn tại không
        //            if (File.Exists(pathAnh))
        //            {
        //                try
        //                {
        //                    using (var tempImage = Image.FromFile(pathAnh))
        //                    {
        //                        picAnhNhanVien.Image = new Bitmap(tempImage);
        //                    }
        //                }
        //                catch
        //                {
        //                    picAnhNhanVien.Image = null;
        //                }
        //            }
        //            else
        //            {
        //                picAnhNhanVien.Image = null;
        //            }
        //        }
        //        else
        //        {
        //            picAnhNhanVien.Image = null;
        //        }
        //    }
        //}
    }
}
