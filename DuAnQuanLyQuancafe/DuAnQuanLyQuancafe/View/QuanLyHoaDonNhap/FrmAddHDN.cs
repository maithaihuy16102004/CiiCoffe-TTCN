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
            FrmAddHDN_Load();
        }

        private void FrmAddHDN_Load()
        {
            NhanVienController controllerNhanVien = new NhanVienController();
            List<NhanVienModel> danhSachNV = controllerNhanVien.LayDanhSachNhanVien();
            NhaCungCapController controllerNhaCungCap = new NhaCungCapController();
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
                // Kiểm tra ngày nhập không được lớn hơn ngày hiện tại
                if (ngayNhap.Date > DateTime.Now.Date)
                {
                    MessageBox.Show($"Ngày nhập {ngayNhap.Date} lớn hơn ngày hiện tại {DateTime.Now.Date}.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show($"Ngày nhập {ngayNhap.Date} hợp lệ (<= {DateTime.Now.Date}).", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                // Tạo danh sách tham số
                string maHDN = _hdnController.GetNextHoaDonNhap();
                Hashtable parameter = new Hashtable
                {
                    { "MaHDN", maHDN },
                    { "NgayNhap", ngayNhap },
                    { "MaNV", maNV },
                    { "MaNCC", maNCC },
                    { "TongTien", tongTien }
                };

                // Gọi hàm thêm hóa đơn nhập
                _hdnController.ThemHDN(parameter);
                MessageBox.Show("Thêm hóa đơn nhập thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FrmAddHDN_Load(); // Làm mới danh sách sau khi thêm
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