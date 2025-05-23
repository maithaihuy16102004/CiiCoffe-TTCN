using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DuAnQuanLyQuancafe.Model;
using DuAnQuanLyQuancafe.function;
using System.Globalization;

namespace DuAnQuanLyQuancafe.View
{
    public partial class FrmThongKe : Form
    {
        //Fields
        private ThongKeModel thongKeModel;
        //Constructor
        public FrmThongKe()
        {
            InitializeComponent();
            //Default - Last 7 days
            dtpStartDate.Value = DateTime.Now.AddDays(-7);
            dtpEndDate.Value = DateTime.Now;
            btnLast7Days.Select();

            thongKeModel = new ThongKeModel();
            LoadData();
        }
        //Private methods
        private void LoadData()
        {
            var refreshData = thongKeModel.LoadData(dtpStartDate.Value, dtpEndDate.Value);
            if(refreshData == true)
            {
                lblNumOrders.Text = thongKeModel.NumOrders.ToString();
                lblTotalRevenue.Text = thongKeModel.TotalRevenue.ToString("C0", new CultureInfo("vi-VN"));
                lblTotalProfit.Text = thongKeModel.TotalProfit.ToString("C0", new CultureInfo("vi-VN"));

                lblNumCustomers.Text = thongKeModel.NumCustomers.ToString();
                lblNumSuppliers.Text = thongKeModel.NumSuppliers.ToString();
                lblNumProducts.Text = thongKeModel.NumProducts.ToString();

                chartGrossRevenue.DataSource = thongKeModel.GrossRevenueList;
                chartGrossRevenue.Series[0].XValueMember = "Date";
                chartGrossRevenue.Series[0].YValueMembers = "TotalAmount";
                chartGrossRevenue.DataBind();

                if (thongKeModel.TopProductList != null && thongKeModel.TopProductList.Any())
                {
                    chartTop5Product.DataSource = thongKeModel.TopProductList;
                    chartTop5Product.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
                    chartTop5Product.Series[0].XValueMember = "Key";
                    chartTop5Product.Series[0].YValueMembers = "Value";
                    chartTop5Product.Series[0].Label = "#VALX: #VALY";
                    chartTop5Product.Legends[0].Enabled = true;
                    chartTop5Product.DataBind();
                }
                else
                {
                    chartTop5Product.DataSource = null;
                    chartTop5Product.DataBind();
                    MessageBox.Show("Không có dữ liệu top 5 sản phẩm để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                dgvUnderStock.DataSource = thongKeModel.UnderstockList;
                dgvUnderStock.Columns[0].HeaderText = "Tên sản phẩm";
                dgvUnderStock.Columns[1].HeaderText = "Số lượng tồn kho";
                MessageBox.Show("Data loaded successfully", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to load data", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisableCustomDate()
        {
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
            btnOK.Enabled = false;
        }

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
    }
}
