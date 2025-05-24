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
    public partial class FrmChiTietHDB: Form
    {
        private string _maHDB;
        public FrmChiTietHDB(string maHDB)
        {
            InitializeComponent();
            _maHDB = maHDB;
            LoadChiTietHDB();
        }
        private void LoadChiTietHDB()
        {
            try
            {
                var chiTietList = ChiTietHDBController.LayChiTietHDBTheoMaHDB(_maHDB);

                if (chiTietList == null || chiTietList.Count == 0)
                {
                    MessageBox.Show("Chưa có chi tiết hóa đơn cho mã: " + _maHDB, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvChiTietHDB.DataSource = null;
                    return;
                }

                dgvChiTietHDB.AutoGenerateColumns = true;
                dgvChiTietHDB.DataSource = chiTietList;

                dgvChiTietHDB.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
                dgvChiTietHDB.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvChiTietHDB.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu chi tiết hóa đơn:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dgvChiTietHDB.EnableHeadersVisualStyles = true;
            dgvChiTietHDB.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvChiTietHDB.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvChiTietHDB.ColumnHeadersHeight = 40;

        }
    }
}
