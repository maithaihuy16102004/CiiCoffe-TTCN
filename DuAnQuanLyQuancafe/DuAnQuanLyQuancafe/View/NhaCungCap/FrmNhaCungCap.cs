using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.View.NhaCungCap
{
    public partial class FrmNhaCungCap : Form
    {
        public FrmNhaCungCap()
        {
            InitializeComponent();
            Load_NhaCC();

        }
        public void Load_NhaCC()
        {
            if (dgvNhaCC == null)
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.");
                return;
            }
            else
            {
                NhaCungCapController nhaCungCapController = new NhaCungCapController();
                List<NhaCungCapModel> dsNhaCC = nhaCungCapController.LayDanhSachNhaCC();
                dgvNhaCC.DataSource = dsNhaCC;
                dgvNhaCC.Columns["MaNCC"].HeaderText = "Mã Nhà Cung Cấp";
                dgvNhaCC.Columns["TenNCC"].HeaderText = "Tên Nhà Cung Cấp";
                dgvNhaCC.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                dgvNhaCC.Columns["SDT"].HeaderText = "Số Điện Thoại";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            FrmAddNCC frmAddNCC = new FrmAddNCC();
            frmAddNCC.Show();
            // Khi form thêm nhà cung cấp đóng, tải lại danh sách nhà cung cấp
            frmAddNCC.FormClosed += (s, args) =>
            {
                Load_NhaCC();
            };
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string id = dgvNhaCC.SelectedRows[0].Cells["MaNCC"].Value.ToString();
            if (dgvNhaCC.SelectedRows.Count > 0)
            {
                NhaCungCapController nhaCungCapController = new NhaCungCapController();
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhà cung cấp này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    nhaCungCapController.XoaNhaCungCap(id);
                    MessageBox.Show("Xóa nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Load_NhaCC();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
