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
using DuAnQuanLyQuancafe.Model;
using OfficeOpenXml;
using System.IO;

namespace DuAnQuanLyQuancafe.View
{
    public partial class FrmHDB : Form
    {
        public FrmHDB()
        {
            InitializeComponent();
            LoadHDB();
        }
        public void LoadHDB()
        {
            if (dgvHDB == null)
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.");
                return;
            }
            else
            {
                HoaDonBanController HoaDonBanController = new HoaDonBanController();
                List<HoaDonBanModel> dsHDB = HoaDonBanController.LaydanhsachHDB();
                dgvHDB.DataSource = dsHDB;
                dgvHDB.Columns["MaHDB"].HeaderText = "Mã Hóa Đơn";
                dgvHDB.Columns["NgayBan"].HeaderText = "Ngày Lập";
                dgvHDB.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
                dgvHDB.Columns["Tongtien"].HeaderText = "Tổng tiền";
            }
            dgvHDB.ReadOnly = true;
        }

        private void find_Click(object sender, EventArgs e)
        {

        }

        private void find_Click_1(object sender, EventArgs e)
        {

            string keyword = txttimkiemHDB.Texts.Trim();
            HoaDonBanController controller = new HoaDonBanController();
            List<HoaDonBanModel> dsHDB;

            if (string.IsNullOrEmpty(keyword))
            {
                dsHDB = controller.LaydanhsachHDB();
            }
            else
            {
                dsHDB = controller.TimKiemHDBTheoMaNV(keyword);
                if (dsHDB.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn nào với mã nhân viên: " + keyword);
                }
            }

            dgvHDB.DataSource = dsHDB;
            if (dsHDB.Count > 0)
            {
                dgvHDB.Columns["MaHDB"].HeaderText = "Mã Hóa Đơn";
                dgvHDB.Columns["NgayBan"].HeaderText = "Ngày Lập";
                dgvHDB.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
                dgvHDB.Columns["Tongtien"].HeaderText = "Tổng tiền";
            }

        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            DateTime ngayBatDau = dtstart.Value.Date;
            DateTime ngayKetThuc = dtend.Value.Date;

            HoaDonBanController controller = new HoaDonBanController();
            List<HoaDonBanModel> dsHDB = controller.LocHDBTheoNgay(ngayBatDau, ngayKetThuc);

            dgvHDB.DataSource = dsHDB;
            if (dsHDB.Count > 0)
            {
                dgvHDB.Columns["MaHDB"].HeaderText = "Mã Hóa Đơn";
                dgvHDB.Columns["NgayBan"].HeaderText = "Ngày Lập";
                dgvHDB.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
                dgvHDB.Columns["Tongtien"].HeaderText = "Tổng tiền";
            }
            else
            {
                MessageBox.Show("Không có hóa đơn nào trong khoảng thời gian này.");
            }
        }
        private void ExportDataGridViewToExcel(DataGridView dgvHDB, string filePath)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("Sheet1");

                // Ghi header cột
                for (int i = 0; i < dgvHDB.Columns.Count; i++)
                {
                    ws.Cells[1, i + 1].Value = dgvHDB.Columns[i].HeaderText;
                }

                // Ghi dữ liệu từng ô
                for (int row = 0; row < dgvHDB.Rows.Count; row++)
                {
                    for (int col = 0; col < dgvHDB.Columns.Count; col++)
                    {
                        ws.Cells[row + 2, col + 1].Value = dgvHDB.Rows[row].Cells[col].Value?.ToString() ?? "";
                    }
                }

                // Tự động chỉnh độ rộng cột
                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                // Lưu file
                FileInfo fi = new FileInfo(filePath);
                package.SaveAs(fi);
            }
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
            sfd.FileName = "DanhSachHoaDon.xlsx";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ExportDataGridViewToExcel(dgvHDB, sfd.FileName);
                MessageBox.Show("Xuất Excel thành công!", "Thông báo");
            }
        }
    }
}
