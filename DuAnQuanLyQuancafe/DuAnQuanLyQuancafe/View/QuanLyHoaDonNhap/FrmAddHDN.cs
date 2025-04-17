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

namespace DuAnQuanLyQuancafe.View.QuanLyHoaDonNhap
{
    public partial class FrmAddHDN : Form
    {
        public FrmAddHDN()
        {
            InitializeComponent();

        }
        private void FrmAddHDN_Load(object sender, EventArgs e)
        {
            var controllerNhanVien = new NhanVienController();
            List<NhanVienModel> danhsachNV = controllerNhanVien.LaydanhsachNhanVien();
            var controllerNhaCungCap = new NhaCungCapController();
            List<NhaCungCapModel> danhsachNCC = controllerNhaCungCap.LayDanhSachNhaCC();
            cbMaNV.DataSource = danhsachNV;
            cbMaNV.DisplayMember = "TenNV"; // Hiển thị cho người dùng
            cbMaNV.ValueMember = "MaNV";    // Dùng để lưu xuống DB
            cbMaNV.SelectedIndex = 0; // Chọn giá trị đầu tiên
            cbNCC.DataSource = danhsachNCC;
            cbNCC.DisplayMember = "TenNCC"; // Hiển thị cho người dùng
            cbNCC.ValueMember = "MaNCC";    // Dùng để lưu xuống DB
            cbNCC.SelectedIndex = 0; // Chọn giá trị đầu tiên
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            DateTime NgayNhap = dtpNgayNhap.Value;
            string MaNV = cbMaNV.SelectedValue.ToString();
            string MaNCC = cbNCC.SelectedValue.ToString();
            string TongTien = txtTongTien.Text.ToString();
            // Kiểm tra thông tin đầu vào
            if (string.IsNullOrEmpty(MaNV) || string.IsNullOrEmpty(MaNCC) || string.IsNullOrEmpty(TongTien))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (TongTien.Length < 0)
            {
                MessageBox.Show("Tổng tiền không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Tạo danh sách tham số
            string MaHDN = HoaDonNhapController.GetNextHoaDonNhap(); // Gọi hàm sinh mã mới
            Hashtable parameter = new Hashtable
            {
                { "MaHDN", MaHDN }, // THÊM DÒNG NÀY!
                { "NgayNhap", NgayNhap },
                { "MaNV", MaNV },
                { "MaNCC", MaNCC },
                { "TongTien", TongTien },
            };
            // Gọi hàm thêm hóa đơn nhập
            HoaDonNhapController.themHDN(parameter);
            // Hiển thị thông báo thành công
            MessageBox.Show("Thêm hóa đơn nhập thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();

        }
    }
}
    
