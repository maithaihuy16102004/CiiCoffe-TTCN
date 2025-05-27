using DuAnQuanLyQuancafe.Controller;
using System;
using System.Collections;
using System.Linq;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.View.NhaCungCap
{
    public partial class FrmAddNCC : Form
    {
        private readonly NhaCungCapController _nhaCungCapController;

        public FrmAddNCC()
        {
            InitializeComponent();
            _nhaCungCapController = new NhaCungCapController();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các ô nhập liệu
            string maNCC = txtMa.Text.Trim();
            string tenNCC = txtTen.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string moTa = txtMoTa.Text.Trim();

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(maNCC))
            {
                MessageBox.Show("Mã nhà cung cấp không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMa.Focus();
                return;
            }

            if (string.IsNullOrEmpty(tenNCC))
            {
                MessageBox.Show("Tên nhà cung cấp không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return;
            }

            if (string.IsNullOrEmpty(diaChi))
            {
                MessageBox.Show("Địa chỉ không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }

            if (sdt.Length > 10 || sdt.Length < 1 || !sdt.StartsWith("0"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Số điện thoại phải bắt đầu bằng 0 và có độ dài không quá 10 ký tự.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (sdt.Length < 10 || !sdt.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại phải có ít nhất 10 chữ số và chỉ chứa số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            // Tạo Hashtable để chứa dữ liệu
            Hashtable parameter = new Hashtable
            {
                { "MaNCC", maNCC },
                { "TenNCC", tenNCC },
                { "DiaChi", diaChi },
                { "SDT", sdt },
                { "MoTa", string.IsNullOrEmpty(moTa) ? null : moTa } // Xử lý mô tả rỗng
            };

            // Gọi controller để thêm nhà cung cấp
            _nhaCungCapController.ThemNhaCC(parameter);

            this.Close(); // Đóng form sau khi thêm nhà cung cấp
                          // Xóa nội dung các ô nhập liệu sau khi thêm thành công

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form thay vì thoát toàn bộ ứng dụng
        }
    }
}