using System.Drawing;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.View
{
    partial class FrmCapThap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gunaAreaDataset1 = new Guna.Charts.WinForms.GunaAreaDataset();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Button3 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.panelThongTin = new System.Windows.Forms.FlowLayoutPanel();
            this.panelThanhtoan = new Guna.UI2.WinForms.Guna2Panel();
            this.btnPrint = new Guna.UI2.WinForms.Guna2Button();
            this.lblkhachthanhtoan = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblkhachtra = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblgiamgia = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lbltongtien = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.lblTennhanvien = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblManhanvien = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.roundedTextBox1 = new DuAnQuanLyQuancafe.RoundedTextBox();
            this.dgvSanPhamm = new Guna.UI2.WinForms.Guna2DataGridView();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Panel1.SuspendLayout();
            this.panelThanhtoan.SuspendLayout();
            this.guna2GradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPhamm)).BeginInit();
            this.SuspendLayout();
            // 
            // gunaAreaDataset1
            // 
            this.gunaAreaDataset1.BorderColor = System.Drawing.Color.Empty;
            this.gunaAreaDataset1.FillColor = System.Drawing.Color.Empty;
            this.gunaAreaDataset1.Label = "Area1";
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(3, 34);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(86, 17);
            this.guna2HtmlLabel1.TabIndex = 0;
            this.guna2HtmlLabel1.Text = "Tổng tiền hàng";
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(3, 68);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(53, 17);
            this.guna2HtmlLabel2.TabIndex = 1;
            this.guna2HtmlLabel2.Text = "Giảm giá";
            this.guna2HtmlLabel2.Click += new System.EventHandler(this.guna2HtmlLabel2_Click);
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Font = new System.Drawing.Font("Montserrat", 8.249999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(3, 101);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(86, 17);
            this.guna2HtmlLabel3.TabIndex = 2;
            this.guna2HtmlLabel3.Text = "Khách cần trả";
            this.guna2HtmlLabel3.Click += new System.EventHandler(this.guna2HtmlLabel3_Click);
            // 
            // guna2Button3
            // 
            this.guna2Button3.BorderRadius = 20;
            this.guna2Button3.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button3.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button3.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button3.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button3.Font = new System.Drawing.Font("Montserrat", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button3.ForeColor = System.Drawing.Color.White;
            this.guna2Button3.Location = new System.Drawing.Point(23, 166);
            this.guna2Button3.Name = "guna2Button3";
            this.guna2Button3.Size = new System.Drawing.Size(283, 45);
            this.guna2Button3.TabIndex = 4;
            this.guna2Button3.Text = "THANH TOÁN";
            this.guna2Button3.Click += new System.EventHandler(this.guna2Button3_Click);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.Controls.Add(this.panelThongTin);
            this.guna2Panel1.Controls.Add(this.panelThanhtoan);
            this.guna2Panel1.Location = new System.Drawing.Point(779, 87);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(344, 541);
            this.guna2Panel1.TabIndex = 0;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // panelThongTin
            // 
            this.panelThongTin.AutoScroll = true;
            this.panelThongTin.BackColor = System.Drawing.Color.White;
            this.panelThongTin.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelThongTin.Location = new System.Drawing.Point(12, 8);
            this.panelThongTin.Name = "panelThongTin";
            this.panelThongTin.Size = new System.Drawing.Size(318, 310);
            this.panelThongTin.TabIndex = 7;
            this.panelThongTin.WrapContents = false;
            // 
            // panelThanhtoan
            // 
            this.panelThanhtoan.Controls.Add(this.btnPrint);
            this.panelThanhtoan.Controls.Add(this.lblkhachthanhtoan);
            this.panelThanhtoan.Controls.Add(this.guna2HtmlLabel1);
            this.panelThanhtoan.Controls.Add(this.guna2HtmlLabel2);
            this.panelThanhtoan.Controls.Add(this.guna2HtmlLabel3);
            this.panelThanhtoan.Controls.Add(this.guna2Button3);
            this.panelThanhtoan.Controls.Add(this.lblkhachtra);
            this.panelThanhtoan.Controls.Add(this.lblgiamgia);
            this.panelThanhtoan.Controls.Add(this.lbltongtien);
            this.panelThanhtoan.Location = new System.Drawing.Point(12, 324);
            this.panelThanhtoan.Name = "panelThanhtoan";
            this.panelThanhtoan.Size = new System.Drawing.Size(318, 214);
            this.panelThanhtoan.TabIndex = 6;
            // 
            // btnPrint
            // 
            this.btnPrint.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPrint.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPrint.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPrint.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPrint.FillColor = System.Drawing.Color.White;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Image = global::DuAnQuanLyQuancafe.Properties.Resources._39263_print_printer_icon;
            this.btnPrint.ImageSize = new System.Drawing.Size(40, 40);
            this.btnPrint.Location = new System.Drawing.Point(259, 123);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(38, 36);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click_1);
            // 
            // lblkhachthanhtoan
            // 
            this.lblkhachthanhtoan.AutoSize = false;
            this.lblkhachthanhtoan.BackColor = System.Drawing.Color.Transparent;
            this.lblkhachthanhtoan.Location = new System.Drawing.Point(140, 123);
            this.lblkhachthanhtoan.Name = "lblkhachthanhtoan";
            this.lblkhachthanhtoan.Size = new System.Drawing.Size(113, 17);
            this.lblkhachthanhtoan.TabIndex = 3;
            this.lblkhachthanhtoan.Text = null;
            // 
            // lblkhachtra
            // 
            this.lblkhachtra.AutoSize = false;
            this.lblkhachtra.BackColor = System.Drawing.Color.Transparent;
            this.lblkhachtra.Location = new System.Drawing.Point(148, 101);
            this.lblkhachtra.Name = "lblkhachtra";
            this.lblkhachtra.Size = new System.Drawing.Size(113, 17);
            this.lblkhachtra.TabIndex = 2;
            this.lblkhachtra.Text = null;
            // 
            // lblgiamgia
            // 
            this.lblgiamgia.AutoSize = false;
            this.lblgiamgia.BackColor = System.Drawing.Color.Transparent;
            this.lblgiamgia.Location = new System.Drawing.Point(148, 68);
            this.lblgiamgia.Name = "lblgiamgia";
            this.lblgiamgia.Size = new System.Drawing.Size(113, 17);
            this.lblgiamgia.TabIndex = 1;
            this.lblgiamgia.Text = null;
            // 
            // lbltongtien
            // 
            this.lbltongtien.AutoSize = false;
            this.lbltongtien.BackColor = System.Drawing.Color.Transparent;
            this.lbltongtien.Location = new System.Drawing.Point(148, 34);
            this.lbltongtien.Name = "lbltongtien";
            this.lbltongtien.Size = new System.Drawing.Size(113, 17);
            this.lbltongtien.TabIndex = 0;
            this.lbltongtien.Text = null;
            // 
            // guna2Button1
            // 
            this.guna2Button1.BorderRadius = 20;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.White;
            this.guna2Button1.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button1.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.guna2Button1.Location = new System.Drawing.Point(436, 18);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(153, 35);
            this.guna2Button1.TabIndex = 2;
            this.guna2Button1.Text = "Hóa Đơn 1";
            // 
            // guna2Button2
            // 
            this.guna2Button2.BorderRadius = 20;
            this.guna2Button2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button2.FillColor = System.Drawing.Color.White;
            this.guna2Button2.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button2.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.guna2Button2.Location = new System.Drawing.Point(608, 18);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.Size = new System.Drawing.Size(153, 35);
            this.guna2Button2.TabIndex = 3;
            this.guna2Button2.Text = "Hóa Đơn 2";
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.guna2GradientPanel1.Controls.Add(this.lblTennhanvien);
            this.guna2GradientPanel1.Controls.Add(this.lblManhanvien);
            this.guna2GradientPanel1.Controls.Add(this.pictureBox2);
            this.guna2GradientPanel1.Controls.Add(this.guna2Button2);
            this.guna2GradientPanel1.Controls.Add(this.guna2Button1);
            this.guna2GradientPanel1.Controls.Add(this.pictureBox1);
            this.guna2GradientPanel1.Controls.Add(this.roundedTextBox1);
            this.guna2GradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(1149, 67);
            this.guna2GradientPanel1.TabIndex = 1;
            // 
            // lblTennhanvien
            // 
            this.lblTennhanvien.AutoSize = false;
            this.lblTennhanvien.BackColor = System.Drawing.Color.Transparent;
            this.lblTennhanvien.Location = new System.Drawing.Point(980, 32);
            this.lblTennhanvien.Name = "lblTennhanvien";
            this.lblTennhanvien.Size = new System.Drawing.Size(81, 20);
            this.lblTennhanvien.TabIndex = 6;
            this.lblTennhanvien.Text = null;
            // 
            // lblManhanvien
            // 
            this.lblManhanvien.AutoSize = false;
            this.lblManhanvien.BackColor = System.Drawing.Color.Transparent;
            this.lblManhanvien.Location = new System.Drawing.Point(931, 33);
            this.lblManhanvien.Name = "lblManhanvien";
            this.lblManhanvien.Size = new System.Drawing.Size(81, 20);
            this.lblManhanvien.TabIndex = 5;
            this.lblManhanvien.Text = null;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DuAnQuanLyQuancafe.Properties.Resources._9110796_x_icon;
            this.pictureBox2.Location = new System.Drawing.Point(1087, 18);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(42, 34);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::DuAnQuanLyQuancafe.Properties.Resources._5402443_search_find_magnifier_magnifying_magnifying_glass_icon;
            this.pictureBox1.Location = new System.Drawing.Point(367, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(33, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // roundedTextBox1
            // 
            this.roundedTextBox1.BackColor = System.Drawing.Color.White;
            this.roundedTextBox1.BorderColor = System.Drawing.Color.DodgerBlue;
            this.roundedTextBox1.BorderRadius = 15;
            this.roundedTextBox1.BorderSize = 2;
            this.roundedTextBox1.ForeColor = System.Drawing.Color.Black;
            this.roundedTextBox1.Location = new System.Drawing.Point(57, 18);
            this.roundedTextBox1.Name = "roundedTextBox1";
            this.roundedTextBox1.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.roundedTextBox1.Size = new System.Drawing.Size(351, 35);
            this.roundedTextBox1.TabIndex = 0;
            this.roundedTextBox1.Texts = "";
            // 
            // dgvSanPhamm
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dgvSanPhamm.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSanPhamm.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSanPhamm.ColumnHeadersHeight = 30;
            this.dgvSanPhamm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSanPhamm.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSanPhamm.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvSanPhamm.Location = new System.Drawing.Point(67, 132);
            this.dgvSanPhamm.Name = "dgvSanPhamm";
            this.dgvSanPhamm.RowHeadersVisible = false;
            this.dgvSanPhamm.Size = new System.Drawing.Size(678, 438);
            this.dgvSanPhamm.TabIndex = 2;
            this.dgvSanPhamm.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvSanPhamm.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvSanPhamm.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvSanPhamm.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvSanPhamm.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvSanPhamm.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvSanPhamm.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvSanPhamm.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvSanPhamm.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvSanPhamm.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSanPhamm.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvSanPhamm.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvSanPhamm.ThemeStyle.HeaderStyle.Height = 30;
            this.dgvSanPhamm.ThemeStyle.ReadOnly = false;
            this.dgvSanPhamm.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvSanPhamm.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSanPhamm.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSanPhamm.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvSanPhamm.ThemeStyle.RowsStyle.Height = 22;
            this.dgvSanPhamm.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvSanPhamm.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvSanPhamm.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSanPhamm_CellClick);
            this.dgvSanPhamm.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSanPhamm_CellContentClick);
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("SVN-Dessert Menu Script", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(254, 79);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(275, 47);
            this.guna2HtmlLabel4.TabIndex = 3;
            this.guna2HtmlLabel4.Text = "Danh sách sản phẩm";
            // 
            // FrmCapThap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1148, 640);
            this.Controls.Add(this.guna2HtmlLabel4);
            this.Controls.Add(this.dgvSanPhamm);
            this.Controls.Add(this.guna2GradientPanel1);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCapThap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "S";
            this.guna2Panel1.ResumeLayout(false);
            this.panelThanhtoan.ResumeLayout(false);
            this.panelThanhtoan.PerformLayout();
            this.guna2GradientPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPhamm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.Charts.WinForms.GunaAreaDataset gunaAreaDataset1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2Button guna2Button3;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private RoundedTextBox roundedTextBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2Panel panelThanhtoan;
        private PictureBox pictureBox2;
        private Guna.UI2.WinForms.Guna2DataGridView dgvSanPhamm;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblkhachthanhtoan;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblkhachtra;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblgiamgia;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbltongtien;
        private System.Windows.Forms.FlowLayoutPanel panelThongTin;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTennhanvien;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblManhanvien;
        private Guna.UI2.WinForms.Guna2Button btnPrint;
    }
}