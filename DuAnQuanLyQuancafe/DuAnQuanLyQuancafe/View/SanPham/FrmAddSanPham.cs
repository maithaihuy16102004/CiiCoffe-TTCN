using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.function;
using DuAnQuanLyQuancafe.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.View.SanPham
{
    public partial class FrmAddSanPham : Form
    {
        private readonly SanPhamController _sanPhamController;
        private byte[] _anhDuocChon; // Biến lưu trữ ảnh dưới dạng byte[]

        public FrmAddSanPham()
        {
            InitializeComponent();
            _sanPhamController = new SanPhamController();
        }

        private void FrmAddSanPham_Load(object sender, EventArgs e)
        {
            try
            {
                // Tải danh sách loại và công dụng
                List<LoaiModel> danhSachLoai = LoaiController.LayDanhSachLoai();
                List<CongDungModel> danhSachCongDung = CongDungController.LayMaCongDung();

                if (danhSachLoai == null || danhSachLoai.Count == 0)
                    throw new Exception("Không có dữ liệu loại sản phẩm.");
                if (danhSachCongDung == null || danhSachCongDung.Count == 0)
                    throw new Exception("Không có dữ liệu công dụng.");

                cbMaLoai.DataSource = danhSachLoai;
                cbMaLoai.DisplayMember = "TenLoai";
                cbMaLoai.ValueMember = "MaLoai";
                cbMaLoai.SelectedIndex = 0;

                cbCongDung.DataSource = danhSachCongDung;
                cbCongDung.DisplayMember = "TenCongDung";
                cbCongDung.ValueMember = "MaCongDung";
                cbCongDung.SelectedIndex = 0;

                // Cho phép nhập mã sản phẩm
                txtMa.Enabled = true;
                txtMa.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png",
                Title = "Chọn ảnh sản phẩm",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Hiển thị ảnh lên PictureBox
                        picAnh.Image = Image.FromFile(dlg.FileName);
                        // Chuyển ảnh sang byte[]
                        _anhDuocChon = File.ReadAllBytes(dlg.FileName);

                        // Kiểm tra kích thước ảnh (giới hạn 1MB)
                        if (_anhDuocChon.Length > 1048576)
                        {
                            MessageBox.Show("Ảnh quá lớn, vui lòng chọn ảnh dưới 1MB.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            _anhDuocChon = null;
                            picAnh.Image = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi chọn ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _anhDuocChon = null;
                        picAnh.Image = null;
                    }
                }
            }
        }

 

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các ô nhập liệu
                string maSP = txtMa.Text.Trim();
                string tenSP = txtTen.Text.Trim();
                string maLoai = cbMaLoai.SelectedValue?.ToString();
                string giaNhapText = txtGiaNhap.Text.Trim();
                string giaBanText = txtGiaBan.Text.Trim();
                string soLuongText = txtSoLuong.Text.Trim();
                string maCongDung = cbCongDung.SelectedValue?.ToString();

                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(maSP))
                {
                    MessageBox.Show("Mã sản phẩm không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMa.Focus();
                    return;
                }

                // Kiểm tra mã sản phẩm trùng
                string sql = "SELECT COUNT(*) FROM SanPham WHERE MaSP = @MaSP";
                SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@MaSP", maSP) };
                if (DatabaseHelper.CheckKey(sql, parameters))
                {
                    MessageBox.Show($"Mã sản phẩm '{maSP}' đã tồn tại. Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMa.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(tenSP))
                {
                    MessageBox.Show("Tên sản phẩm không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTen.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(maLoai))
                {
                    MessageBox.Show("Vui lòng chọn loại sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbMaLoai.Focus();
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
                    txtSoLuong.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(maCongDung))
                {
                    MessageBox.Show("Vui lòng chọn công dụng sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbCongDung.Focus();
                    return;
                }

                if (_anhDuocChon == null)
                {
                    MessageBox.Show("Vui lòng chọn ảnh sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnChon.Focus();
                    return;
                }

                // Tạo Hashtable chứa dữ liệu
                Hashtable parameter = new Hashtable
        {
            { "MaSP", maSP },
            { "TenSP", tenSP },
            { "MaLoai", maLoai },
            { "GiaNhap", giaNhap },
            { "GiaBan", giaBan },
            { "SoLuong", soLuong },
            { "MaCongDung", maCongDung },
            { "HinhAnh", _anhDuocChon }
        };

                // Gọi controller để thêm sản phẩm
                _sanPhamController.ThemSanPham(parameter);
                MessageBox.Show("Thêm sản phẩm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Hỏi người dùng có muốn tiếp tục thêm sản phẩm khác không
                DialogResult result = MessageBox.Show("Bạn có muốn tiếp tục thêm sản phẩm khác?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    this.Close();
                }
                else
                {
                    // Xóa các trường nhập liệu để thêm sản phẩm mới
                    txtMa.Clear();
                    txtTen.Clear();
                    txtGiaNhap.Clear();
                    txtGiaBan.Clear();
                    txtSoLuong.Clear();
                    cbMaLoai.SelectedIndex = -1;
                    cbCongDung.SelectedIndex = -1;
                    _anhDuocChon = null;
                    txtMa.Focus();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }
    }
}