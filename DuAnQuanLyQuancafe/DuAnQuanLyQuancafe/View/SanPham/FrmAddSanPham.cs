using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.Model;
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

namespace DuAnQuanLyQuancafe.View.SanPham
{
    public partial class FrmAddSanPham : Form
    {
        private byte[] anhDuocChon; // Biến lưu trữ ảnh dưới dạng byte[]
        public FrmAddSanPham()
        {
            InitializeComponent();
        }

        private void FrmAddSanPham_Load(object sender, EventArgs e)
        {
            List<LoaiModel> danhSachLoai = LoaiController.LayDanhSachLoai(); // hoặc gọi trực tiếp từ lớp chứa LayMaQue()
            List<CongDungModel> danhsachCongDung = CongDungController.LayMaCongDung();
            cbMaLoai.DisplayMember = "TenLoai"; // Hiển thị cho người dùng
            cbMaLoai.ValueMember = "MaLoai";
            cbCongDung.DisplayMember = "TenCongDung"; // Hiển thị cho người dùng
            cbCongDung.ValueMember = "MaCongDung";
            cbMaLoai.DataSource = danhSachLoai;
            cbCongDung.DataSource = danhsachCongDung;
            cbMaLoai.SelectedIndex = 0; // Chọn giá trị đầu tiên
            cbCongDung.SelectedIndex = 0; // Chọn giá trị đầu tiên
        }
        private void btnChon_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png",
                Title = "Chọn ảnh đại diện",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // Hiển thị ảnh lên PictureBox
                picAnh.Image = Image.FromFile(dlg.FileName);

                // Chuyển ảnh sang byte[] để lưu vào CSDL
                anhDuocChon = File.ReadAllBytes(dlg.FileName);
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn ảnh nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string maSP = txtMa.Text.ToString();
            string tenSP = txtTen.Text.ToString();
            string maLoai = cbMaLoai.SelectedValue.ToString();
            string giaNhap = txtGiaNhap.Text.ToString();
            string giaBan = txtGiaBan.Text.ToString();
            string soLuong = txtSoLuong.Text.ToString();
            string maCongDung = cbCongDung.SelectedValue.ToString();

            if (anhDuocChon == null)
            {
                MessageBox.Show("Bạn chưa chọn ảnh nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(tenSP) || string.IsNullOrEmpty(maLoai) || string.IsNullOrEmpty(giaNhap) || string.IsNullOrEmpty(giaBan) || string.IsNullOrEmpty(soLuong) || string.IsNullOrEmpty(maCongDung))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            Hashtable parameter = new Hashtable()
            {
                {"MaSP", maSP },
                {"TenSP", tenSP },
                {"MaLoai", maLoai },
                {"GiaNhap", giaNhap },
                {"GiaBan", giaBan },
                {"SoLuong", soLuong },
                {"MaCongDung", maCongDung },
                {"HinhAnh", anhDuocChon}
            };
            // Thực hiện thêm sản phẩm
            SanPhamController sanPhamController = new SanPhamController();
            try
            {
                sanPhamController.Themsanpham(parameter);
                MessageBox.Show("Thêm sản phẩm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }

}
