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
                dgvNhaCC.Columns["MoTa"].HeaderText = "Mô tả";
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
                    Load_NhaCC();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvNhaCC_Click(object sender, EventArgs e)
        {
            if (dgvNhaCC.CurrentRow == null || dgvNhaCC.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu");
                return;
            }
            txtMa.Enabled = false; // không cho phép sửa mã 
            // Lấy giá trị từ các cột
            txtMa.Text = dgvNhaCC.CurrentRow.Cells["MaNCC"]?.Value?.ToString() ?? "";
            txtTen.Text = dgvNhaCC.CurrentRow.Cells["TenNCC"]?.Value?.ToString() ?? "";
            txtDiaChi.Text = dgvNhaCC.CurrentRow.Cells["DiaChi"]?.Value?.ToString() ?? "";          
            txtSDT.Text = dgvNhaCC.CurrentRow.Cells["SDT"]?.Value?.ToString() ?? "";
            txtMoTa.Text = dgvNhaCC.CurrentRow.Cells["MoTa"]?.Value?.ToString() ?? "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtMa.Enabled = true; // Không cho phép sửa mã
            if (dgvNhaCC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string id;
            try
            {
                id = dgvNhaCC.SelectedRows[0].Cells["MaNCC"].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Không thể lấy mã nhà cung cấp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string tenNV = txtTen.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string mota = txtMoTa.Text.Trim();

            // Kiểm tra thông tin
            if (string.IsNullOrWhiteSpace(tenNV) || string.IsNullOrWhiteSpace(diaChi) ||
                string.IsNullOrWhiteSpace(sdt) || string.IsNullOrWhiteSpace(mota))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Hashtable parameter = new Hashtable
            {
                { "MaNCC", id },
                { "TenNCC", tenNV },
                { "DiaChi", diaChi },
                { "SDT", sdt },
                { "MoTa", mota },
             };

            try
            {
                NhaCungCapController nhaCungCapController = new NhaCungCapController();
                nhaCungCapController.SuaNCC(parameter);
                Load_NhaCC();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string tukhoa = txtTimKiem.Text.ToLower();
            NhaCungCapController nhanVienController = new NhaCungCapController();
            // Lọc danh sách nhân viên chứa từ khóa
            List<NhaCungCapModel> locnhanvien = nhanVienController.LayDanhSachNhaCC().FindAll(emp => emp.TenNCC.ToLower().Contains(tukhoa)).ToList();
            // Cập nhật DataGridView
            dgvNhaCC.DataSource = null;
            dgvNhaCC.DataSource = locnhanvien;
        }
    }
}
