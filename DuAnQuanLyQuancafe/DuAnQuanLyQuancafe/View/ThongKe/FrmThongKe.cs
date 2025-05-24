using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DuAnQuanLyQuancafe.Controller;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace DuAnQuanLyQuancafe.View
{
    public partial class FrmThongKe : Form
    {
        // Fields
        private readonly ThongKeController _thongKeController;

        // Constructor
        public FrmThongKe()
        {
            InitializeComponent();
            _thongKeController = new ThongKeController();

            // Default: Last 7 days
            dtpStartDate.Value = DateTime.Now.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            btnLast7Days.Select();

            LoadData();
        }

        // Private methods
        private void LoadData()
        {
            // Validate dates
            if (dtpStartDate.Value > dtpEndDate.Value)
            {
                digCanhBao.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.", "Thông báo");
                return;
            }

            try
            {
                bool refreshData = _thongKeController.LoadData(dtpStartDate.Value, dtpEndDate.Value);
                if (refreshData)
                {
                    // Update labels
                    lblNumOrders.Text = _thongKeController.GetNumOrders().ToString();
                    lblTotalRevenue.Text = _thongKeController.GetTotalRevenue().ToString("C0", new CultureInfo("vi-VN"));
                    lblTotalProfit.Text = _thongKeController.GetTotalProfit().ToString("C0", new CultureInfo("vi-VN"));
                    lblNumSuppliers.Text = _thongKeController.GetNumSuppliers().ToString();
                    lblNumProducts.Text = _thongKeController.GetNumProducts().ToString();

                    // Update Gross Revenue Chart
                    var grossRevenueList = _thongKeController.GetGrossRevenueList();
                    if (grossRevenueList != null && grossRevenueList.Count > 0)
                    {
                        chartGrossRevenue.DataSource = grossRevenueList;
                        chartGrossRevenue.Series[0].XValueMember = "Date";
                        chartGrossRevenue.Series[0].YValueMembers = "TotalAmount";
                        chartGrossRevenue.DataBind();
                    }
                    else
                    {
                        chartGrossRevenue.DataSource = null;
                        chartGrossRevenue.DataBind();
                        MessageBox.Show("Không có dữ liệu doanh thu để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Update Top 5 Products Chart
                    var topProductList = _thongKeController.GetTopProductList();
                    if (topProductList != null && topProductList.Count > 0)
                    {
                        chartTop5Product.DataSource = topProductList;
                        chartTop5Product.Series[0].ChartType = SeriesChartType.Doughnut;
                        chartTop5Product.Series[0].XValueMember = "Key";
                        chartTop5Product.Series[0].YValueMembers = "Value";
                        chartTop5Product.Series[0].Label = "#VALX: #VALY";
                        chartTop5Product.Series[0].IsValueShownAsLabel = true;
                        chartTop5Product.Series[0]["PieLabelStyle"] = "Outside";
                        chartTop5Product.Series[0]["DoughnutRadius"] = "60";
                        chartTop5Product.Series[0].Font = new Font("Arial", 20f, FontStyle.Bold);
                        chartTop5Product.Legends[0].Enabled = true;
                        chartTop5Product.DataBind();
                    }
                    else
                    {
                        chartTop5Product.DataSource = null;
                        chartTop5Product.DataBind();
                    }

                    // Update Understock DataGridView
                    var understockList = _thongKeController.GetUnderstockList();
                    if (understockList != null && understockList.Count > 0)
                    {
                        dgvUnderStock.DataSource = understockList;
                        dgvUnderStock.Columns[0].HeaderText = "Tên sản phẩm";
                        dgvUnderStock.Columns[1].HeaderText = "Số lượng tồn kho";
                    }
                    else
                    {
                        dgvUnderStock.DataSource = null;
                        MessageBox.Show("Không có sản phẩm nào dưới mức tồn kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    MessageBox.Show("Tải dữ liệu thành công.", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Dữ liệu đã được tải trước đó với khoảng thời gian này.", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisableCustomDate()
        {
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
            btnOK.Enabled = false;
        }

        // Event handlers
        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDate();
        }

        private void btnLast7Days_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDate();
        }

        private void btnLast30Days_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = DateTime.Today.AddDays(-30);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDate();
        }

        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            dtpStartDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpEndDate.Value = DateTime.Now;
            LoadData();
            DisableCustomDate();
        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
            dtpStartDate.Enabled = true;
            dtpEndDate.Enabled = true;
            btnOK.Enabled = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void chartTop5Product_Click(object sender, EventArgs e)
        {

        }
    }
}