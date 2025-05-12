using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.Model;
using DuAnQuanLyQuancafe.View.SanPham;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.View
{
    public partial class FrmSanPham : Form
    {
        public FrmSanPham()
        {
            InitializeComponent();
            function.DatabaseHelper.FillCombo("SELECT MaLoai, TenLoai FROM Loai", cbLoai, "MaLoai", "TenLoai");
            function.DatabaseHelper.FillCombo("SELECT MaCongDung, TenCongDung FROM CongDung", cbCongDung, "MaCongDung", "TenCongDung");
            cbCongDung.SelectedIndex = -1; // Chọn giá trị đầu tiên
            cbLoai.SelectedIndex = -1; // Chọn giá trị đầu tiên
        }
        private void FrmSanPham_Load(object sender, EventArgs e)
        {
            List<LoaiModel> danhSachLoai = LoaiController.LayDanhSachLoai(); // hoặc gọi trực tiếp từ lớp chứa
            List<CongDungModel> danhsachCongDung = CongDungController.LayMaCongDung();
            
            if (dgvSanPham == null)
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.");
                return;
            }
            else
            {
                SanPhamController sanPhamController = new SanPhamController();
                List<SanPhamModel> dsSanPham = sanPhamController.LayDanhSachSanPham();
                dgvSanPham.DataSource = dsSanPham;
                dgvSanPham.Columns["MaSP"].HeaderText = "Mã Sản Phẩm";
                dgvSanPham.Columns["TenSP"].HeaderText = "Tên Sản Phẩm";
                dgvSanPham.Columns["TenLoai"].HeaderText = "Mã Loại"; // Hiển thị tên loại thay vì mã
                dgvSanPham.Columns["GiaNhap"].HeaderText = "Giá Nhập";
                dgvSanPham.Columns["GiaBan"].HeaderText = "Giá Bán";
                dgvSanPham.Columns["SoLuong"].HeaderText = "Số Lượng";
                dgvSanPham.Columns["TenCongDung"].HeaderText = "Công Dụng"; // Hiển thị tên công dụng thay vì mã

                // Ẩn các cột không cần thiết
                dgvSanPham.Columns["MaLoai"].Visible = false;
                dgvSanPham.Columns["MaCongDung"].Visible = false;
                dgvSanPham.Columns["HinhAnh"].Visible = false;
            }

        }
        private void LoadSanPham()
        {
            if (dgvSanPham == null)
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.");
                return;
            }
            else
            {
                SanPhamController sanPhamController = new SanPhamController();
                List<SanPhamModel> dsSanPham = sanPhamController.LayDanhSachSanPham();
                dgvSanPham.DataSource = dsSanPham;
                dgvSanPham.Columns["MaSP"].HeaderText = "Mã Sản Phẩm";
                dgvSanPham.Columns["TenSP"].HeaderText = "Tên Sản Phẩm";
                dgvSanPham.Columns["TenLoai"].HeaderText = "Mã Loại"; // Hiển thị tên loại thay vì mã
                dgvSanPham.Columns["GiaNhap"].HeaderText = "Giá Nhập";
                dgvSanPham.Columns["GiaBan"].HeaderText = "Giá Bán";
                dgvSanPham.Columns["SoLuong"].HeaderText = "Số Lượng";
                dgvSanPham.Columns["TenCongDung"].HeaderText = "Công Dụng"; // Hiển thị tên công dụng thay vì mã

                // Ẩn các cột không cần thiết
                dgvSanPham.Columns["MaLoai"].Visible = false;
                dgvSanPham.Columns["MaCongDung"].Visible = false;
                dgvSanPham.Columns["HinhAnh"].Visible = false;

            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            FrmAddSanPham frmAddSanPham = new FrmAddSanPham();
            frmAddSanPham.Show();
            frmAddSanPham.FormClosed += (s, args) =>
            {
                LoadSanPham();
            };
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(dgvSanPham.SelectedRows.Count > 0)
            {
                string id = dgvSanPham.SelectedRows[0].Cells["MaSP"].Value.ToString();
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    SanPhamController sanPhamController = new SanPhamController();
                    sanPhamController.XoaSanPham(id);
                    LoadSanPham();
                    
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvSanPham_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow == null || dgvSanPham.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu");
                return;
            }
            txtMa.Enabled = false; // Không cho phép sửa mã 
            // Lấy giá trị từ các cột
            txtMa.Text = dgvSanPham.CurrentRow.Cells["MaSP"]?.Value?.ToString() ?? "";
            txtTen.Text = dgvSanPham.CurrentRow.Cells["TenSP"]?.Value?.ToString() ?? "";
            cbLoai.Text = dgvSanPham.CurrentRow.Cells["MaLoai"]?.Value?.ToString() ?? "";
            txtGiaNhap.Text = dgvSanPham.CurrentRow.Cells["GiaNhap"]?.Value?.ToString() ?? "";
            txtGiaBan.Text = dgvSanPham.CurrentRow.Cells["GiaBan"]?.Value?.ToString() ?? "";
            txtSoluong.Text = dgvSanPham.CurrentRow.Cells["Soluong"]?.Value?.ToString() ?? "";
            cbCongDung.Text = dgvSanPham.CurrentRow.Cells["MaCongDung"]?.Value?.ToString() ?? "";

            // Xử lý ảnh
            picAnh.SizeMode = PictureBoxSizeMode.Zoom;
            var cellValue = dgvSanPham.CurrentRow.Cells["HinhAnh"]?.Value;

            if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
            {
                // Nếu bạn dùng đường dẫn tương đối (ví dụ: chỉ tên file), nên chuyển sang đường dẫn tuyệt đối
                string fileName = cellValue.ToString();
                string pathAnh = Path.Combine(Application.StartupPath, "Images", fileName);

                if (File.Exists(pathAnh))
                {
                    try
                    {
                        using (FileStream fs = new FileStream(pathAnh, FileMode.Open, FileAccess.Read))
                        {
                            picAnh.Image = Image.FromStream(fs);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi tải ảnh: {ex.Message}");
                        picAnh.Image = null;
                    }
                }
                else
                {
                    picAnh.Image = null;
                }
            }
            else
            {
                picAnh.Image = null;
            }
        }
        byte[] anhDuocChon; // Biến lưu trữ ảnh dưới dạng byte[]
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string id;
            try
            {
                id = dgvSanPham.SelectedRows[0].Cells["MaSP"].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Không thể lấy mã sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string tenNV = txtTen.Text.Trim();
            string maLoai = cbLoai.SelectedValue?.ToString();
            string giaNhap = txtGiaNhap.Text.Trim();
            string giaBan = txtGiaBan.Text.Trim();
            string soLuong = txtSoluong.Text.Trim();
            string maCongDung = cbCongDung.SelectedValue?.ToString();
            // Kiểm tra thông tin
            if (string.IsNullOrWhiteSpace(tenNV) || string.IsNullOrWhiteSpace(maLoai) ||
                string.IsNullOrWhiteSpace(giaBan) || string.IsNullOrWhiteSpace(giaNhap) || string.IsNullOrWhiteSpace(soLuong))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(maLoai))
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (anhDuocChon == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Hashtable parameter = new Hashtable
            {
                { "MaSP", id },
                { "TenSP", tenNV },
                { "MaLoai", maLoai },
                { "GiaNhap", giaNhap },
                { "GiaBan", giaBan },
                { "SoLuong", soLuong },
                { "MaCongDung", maCongDung },
                { "HinhAnh", anhDuocChon }
             };

            try
            {
                SanPhamController sanPhamController = new SanPhamController();
                sanPhamController.CapNhatSanPham(parameter);
                LoadSanPham();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
