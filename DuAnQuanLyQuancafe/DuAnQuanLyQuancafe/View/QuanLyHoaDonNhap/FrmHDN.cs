using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.Model;
using DuAnQuanLyQuancafe.View.QuanLyHoaDonNhap;
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
    public partial class FrmHDN : Form
    {
        public FrmHDN()
        {
            InitializeComponent();
            LoadHDN();
            dgvHDN.CellDoubleClick += dgvHDN_CellDoubleClick;// Đăng ký sự kiện CellClick

        }
        public void LoadHDN()
        {
            if (dgvHDN == null)
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.");
                return;
            }
            else
            {
                HoaDonNhapController HDNController = new HoaDonNhapController();
                List<HoaDonNhapModel> dsachHDN = HDNController.LayDanhSachHDN();
                dgvHDN.DataSource = dsachHDN;
                dgvHDN.Columns["MaHDN"].HeaderText = "Mã Hóa Đơn Nhập";
                dgvHDN.Columns["NgayNhap"].HeaderText = "Ngày Nhập";
                dgvHDN.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
                dgvHDN.Columns["MaNCC"].HeaderText = "Mã Nhà Cung Cấp";
                dgvHDN.Columns["TongTien"].HeaderText = "Tổng Tiền";
            }

        }

        private void btnThem_Click(object sender, EventArgs e)
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
            if (e.RowIndex >= 0 && dgvHDN.Rows[e.RowIndex].Cells["MaHDN"].Value != null)
            {
                string maHDN = dgvHDN.Rows[e.RowIndex].Cells["MaHDN"].Value.ToString();

                FrmChitietHDN frmChiTiet = new FrmChitietHDN(maHDN);
                frmChiTiet.ShowDialog();
            }
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

        private void FrmHDN_TextChanged(object sender, EventArgs e)
        {
            string tukoa = txtTimKiem.Text.Trim();
            HoaDonNhapController HDNController = new HoaDonNhapController();
            List<HoaDonNhapModel> locHDN = HDNController.LayDanhSachHDN().FindAll(hdn => hdn.MaHDN.ToLower().Contains(tukoa.ToLower())).ToList();
            dgvHDN.DataSource = null; // Đặt lại DataSource trước khi gán mới
            dgvHDN.DataSource = locHDN;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHDN.SelectedRows.Count > 0)
            {
                string id = dgvHDN.SelectedRows[0].Cells["MaHDN"].Value.ToString();
                HoaDonNhapController HDNController = new HoaDonNhapController();
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    HDNController.XoaHDN(id);
                    LoadHDN();
                    MessageBox.Show("Xóa hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}