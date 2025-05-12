using DuAnQuanLyQuancafe.Controller;
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
    public partial class FrmAddNCC : Form
    {
        public FrmAddNCC()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maNCC = txtMa.Text.ToString();
            string tenNCC = txtTen.Text.ToString();
            string diaChi = txtDiaChi.Text.ToString();
            string sdt = txtSDT.Text.ToString();
            string moTa = txtMoTa.Text.ToString();
            if (string.IsNullOrEmpty(tenNCC) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (sdt.Length < 10)
            {
                MessageBox.Show("Số điện thoại không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Hashtable parameter = new Hashtable
            {
                { "MaNCC", maNCC }, // Gọi hàm sinh mã mới
                { "TenNCC", tenNCC },
                { "DiaChi", diaChi },
                { "SDT", sdt },
                { "MoTa", moTa }
            };
            NhaCungCapController nhaCungCapController = new NhaCungCapController();
            nhaCungCapController.ThemNhaCC(parameter);
            this.Close(); // Đóng form sau khi thêm nhà cung cấp thành công
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
