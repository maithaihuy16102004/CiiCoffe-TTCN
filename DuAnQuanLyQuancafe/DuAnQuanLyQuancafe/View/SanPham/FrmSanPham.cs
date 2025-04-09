using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.Model;
using DuAnQuanLyQuancafe.View.SanPham;
using System;
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
    public partial class FrmSanPham : Form
    {
        public FrmSanPham()
        {
            InitializeComponent();

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
                dgvSanPham.Columns["MaLoai"].HeaderText = "Mã Loại";
                dgvSanPham.Columns["GiaNhap"].HeaderText = "Giá Nhập";
                dgvSanPham.Columns["GiaBan"].HeaderText = "Giá Bán";
                dgvSanPham.Columns["SoLuong"].HeaderText = "Số Lượng";
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
                dgvSanPham.Columns["MaLoai"].HeaderText = "Mã Loại";
                dgvSanPham.Columns["GiaNhap"].HeaderText = "Giá Nhập";
                dgvSanPham.Columns["GiaBan"].HeaderText = "Giá Bán";
                dgvSanPham.Columns["SoLuong"].HeaderText = "Số Lượng";
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
                    MessageBox.Show("Xóa sản phẩm thành công.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
