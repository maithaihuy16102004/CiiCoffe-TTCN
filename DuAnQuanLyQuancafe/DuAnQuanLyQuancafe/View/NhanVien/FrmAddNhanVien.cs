using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.View.NhanVien
{
    public partial class FrmAddNhanVien : Form
    {
        private readonly NhanVienController _nhanVienController;
        private byte[] anhDuocChon; // Biến lưu trữ ảnh dưới dạng byte[]

        public FrmAddNhanVien()
        {
            InitializeComponent();
            _nhanVienController = new NhanVienController();
        }
        private void FrmAddNhanVien_Load(object sender, EventArgs e)
        {
            function.DatabaseHelper.FillCombo("SELECT MaQue, TenQue FROM Que", cbQue, "MaQue", "TenQue");
            cbQue.DisplayMember = "TenQue"; // Hiển thị cho người dùng
            cbQue.ValueMember = "MaQue";    // Dùng để lưu xuống DB
            cbGioiTInh.Items.Add("Nam");
            cbGioiTInh.Items.Add("Nữ");
            cbGioiTInh.Items.Add("Khác");
            cbGioiTInh.SelectedIndex = 0; // Chọn giá trị đầu tiên
        }


        //Chon anh dai dien nhan vien
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
        // Thêm nhân viên
        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenNV = txtTen.Text.ToString();
            string diaChi = txtDiaChi.Text.ToString();
            string sdt = txtSDT.Text.ToString();
            string gioiTinh = cbGioiTInh.Text.ToString(); // Sửa từ txtGioiTinh thành ComboBox
            DateTime ngaysinh = dtpNgaySinh.Value;
            string maQue = cbQue.SelectedValue.ToString(); // không dùng cbQue.Text!

            // Kiểm tra thông tin đầu vào
            if (string.IsNullOrEmpty(tenNV) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(gioiTinh))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (sdt.Length > 10 || sdt.Length < 1 || !sdt.StartsWith("0"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Số điện thoại phải bắt đầu bằng 0 và có độ dài không quá 10 ký tự.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (anhDuocChon == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra ngày sinh không được lớn hơn ngày hiện tại
            DateTime ngayHienTai = DateTime.Now;
            if (ngaysinh > ngayHienTai)
            {
                MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra nhân viên đủ 18 tuổi
            int tuoi = ngayHienTai.Year - ngaysinh.Year;
            if (ngayHienTai.Month < ngaysinh.Month || (ngayHienTai.Month == ngaysinh.Month && ngayHienTai.Day < ngaysinh.Day))
            {
                tuoi--; // Giảm tuổi đi 1 nếu chưa tới ngày sinh nhật trong năm nay
            }

            if (tuoi < 18)
            {
                MessageBox.Show("Nhân viên phải đủ 18 tuổi để đi làm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo danh sách tham số
            string maNV = _nhanVienController.GetNextMaNhanVien(); // Gọi hàm sinh mã mới
            Hashtable parameter = new Hashtable
            {
                { "MaNV", maNV }, // THÊM DÒNG NÀY!
                { "TenNV", tenNV },
                { "DiaChi", diaChi },
                { "SDT", sdt },
                { "GioiTinh", gioiTinh },
                { "NgaySinh", ngaysinh },
                { "MaQue", maQue },
                { "Anh", anhDuocChon }
            };

            // Gọi hàm thêm nhân viên từ Controller
            _nhanVienController.ThemNhanVien(parameter);
            MessageBox.Show("Thêm nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close(); // Đóng form sau khi thêm nhân viên thành công
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
