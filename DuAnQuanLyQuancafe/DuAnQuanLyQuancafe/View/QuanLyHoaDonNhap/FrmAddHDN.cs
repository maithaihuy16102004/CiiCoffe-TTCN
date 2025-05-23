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
        private readonly HoaDonNhapController _hdnController = new HoaDonNhapController();

        public FrmAddHDN()
        {
            InitializeComponent();
        }

        private void FrmAddHDN_Load(object sender, EventArgs e)
        {
            var controllerNhanVien = new NhanVienController();
            List<NhanVienModel> danhSachNV = controllerNhanVien.LayDanhSachNhanVien();
            var controllerNhaCungCap = new NhaCungCapController();
            List<NhaCungCapModel> danhSachNCC = controllerNhaCungCap.LayDanhSachNhaCC();

            cbMaNV.DataSource = danhSachNV;
            cbMaNV.DisplayMember = "TenNV"; // Hiển thị cho người dùng
            cbMaNV.ValueMember = "MaNV";    // Dùng để lưu xuống DB
            cbMaNV.SelectedIndex = 0; // Chọn giá trị đầu tiên

            cbNCC.DataSource = danhSachNCC;
            cbNCC.DisplayMember = "TenNCC"; // Hiển thị cho người dùng
            cbNCC.ValueMember = "MaNCC";    // Dùng để lưu xuống DB
            cbNCC.SelectedIndex = 0; // Chọn giá trị đầu tiên
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime ngayNhap = dtpNgayNhap.Value;
                string maNV = cbMaNV.SelectedValue?.ToString();
                string maNCC = cbNCC.SelectedValue?.ToString();

                // Kiểm tra thông tin đầu vào
                if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(maNCC) || string.IsNullOrEmpty(txtTongTien.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtTongTien.Text, out decimal tongTien) || tongTien < 0)
                {
                    MessageBox.Show("Tổng tiền không hợp lệ. Vui lòng nhập số dương.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo danh sách tham số
                string maHDN = _hdnController.GetNextHoaDonNhap(); // Gọi qua instance
                Hashtable parameter = new Hashtable
                {
                    { "MaHDN", maHDN },
                    { "NgayNhap", ngayNhap },
                    { "MaNV", maNV },
                    { "MaNCC", maNCC },
                    { "TongTien", tongTien } // Sử dụng decimal thay vì string
                };

                // Gọi hàm thêm hóa đơn nhập
                _hdnController.ThemHDN(parameter);
                MessageBox.Show("Thêm hóa đơn nhập thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm hóa đơn nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}