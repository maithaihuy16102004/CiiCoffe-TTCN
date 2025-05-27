using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DuAnQuanLyQuancafe.Controller;

namespace DuAnQuanLyQuancafe.View.ChiTietHDB
{
    public partial class FrmChiTietHDB : Form
    {
        private string _maHDB;
        private readonly ChiTietHDBController _chiTietHDBController; // Instance của Controller

        public FrmChiTietHDB(string maHDB)
        {
            InitializeComponent();
            _maHDB = maHDB;
            _chiTietHDBController = new ChiTietHDBController(); // Khởi tạo Controller
            LoadChiTietHDB();
            InitializeDataGridView(); // Khởi tạo cấu hình DataGridView
        }

        private void InitializeDataGridView()
        {
            // Tắt tự động tạo cột
            dgvChiTietHDB.AutoGenerateColumns = false;

            // Định nghĩa các cột thủ công
            dgvChiTietHDB.Columns.Clear();

            dgvChiTietHDB.Columns.Add("MaCTHDB", "Mã CT HĐB");
            dgvChiTietHDB.Columns.Add("MaHDB", "Mã HĐB");
            dgvChiTietHDB.Columns.Add("MaSP", "Mã SP");
            dgvChiTietHDB.Columns.Add("TenSP", "Tên SP");
            dgvChiTietHDB.Columns.Add("SoLuong", "Số Lượng");
            dgvChiTietHDB.Columns.Add("ThanhTien", "Thành Tiền");
            dgvChiTietHDB.Columns.Add("KhuyenMai", "Khuyến Mãi");

            // Cấu hình các cột
            dgvChiTietHDB.Columns["MaCTHDB"].DataPropertyName = "MaCTHDB";
            dgvChiTietHDB.Columns["MaHDB"].DataPropertyName = "MaHDB";
            dgvChiTietHDB.Columns["MaSP"].DataPropertyName = "MaSP";
            dgvChiTietHDB.Columns["TenSP"].DataPropertyName = "TenSP";
            dgvChiTietHDB.Columns["SoLuong"].DataPropertyName = "SoLuong";
            dgvChiTietHDB.Columns["ThanhTien"].DataPropertyName = "ThanhTien";
            dgvChiTietHDB.Columns["KhuyenMai"].DataPropertyName = "KhuyenMai";

            // Định dạng cột
            dgvChiTietHDB.Columns["ThanhTien"].DefaultCellStyle.Format = "N0"; // Định dạng số tiền
            dgvChiTietHDB.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChiTietHDB.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Cấu hình giao diện
            dgvChiTietHDB.EnableHeadersVisualStyles = true;
            dgvChiTietHDB.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvChiTietHDB.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvChiTietHDB.ColumnHeadersHeight = 40;
        }

        private void LoadChiTietHDB()
        {
            try
            {
                var chiTietList = _chiTietHDBController.LayChiTietHDBTheoMaHDB(_maHDB); // Correct variable name

                if (chiTietList == null || chiTietList.Count == 0) // Fixed typo: chiTietHDBList -> chiTietList
                {
                    MessageBox.Show($"Chưa có chi tiết hóa đơn cho mã: {_maHDB}", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvChiTietHDB.DataSource = null;
                    return;
                }

                // Gán dữ liệu vào DataGridView
                dgvChiTietHDB.DataSource = chiTietList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu chi tiết hóa đơn:\n{ex.Message}", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}