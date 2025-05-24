using System.Drawing;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.View
{
    partial class FrmHDB
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvHDB = new Guna.UI2.WinForms.Guna2DataGridView();
            this.txttimkiemHDB = new DuAnQuanLyQuancafe.RoundedTextBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.dtstart = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtend = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnLoc = new Guna.UI2.WinForms.Guna2Button();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnExcel = new Guna.UI2.WinForms.Guna2Button();
            this.find = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHDB)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvHDB
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvHDB.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHDB.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHDB.ColumnHeadersHeight = 30;
            this.dgvHDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHDB.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvHDB.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvHDB.Location = new System.Drawing.Point(167, 116);
            this.dgvHDB.Name = "dgvHDB";
            this.dgvHDB.RowHeadersVisible = false;
            this.dgvHDB.Size = new System.Drawing.Size(647, 383);
            this.dgvHDB.TabIndex = 0;
            this.dgvHDB.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvHDB.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvHDB.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvHDB.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvHDB.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvHDB.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvHDB.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvHDB.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvHDB.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvHDB.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvHDB.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvHDB.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvHDB.ThemeStyle.HeaderStyle.Height = 30;
            this.dgvHDB.ThemeStyle.ReadOnly = false;
            this.dgvHDB.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvHDB.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvHDB.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvHDB.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvHDB.ThemeStyle.RowsStyle.Height = 22;
            this.dgvHDB.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvHDB.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // txttimkiemHDB
            // 
            this.txttimkiemHDB.BackColor = System.Drawing.Color.White;
            this.txttimkiemHDB.BorderColor = System.Drawing.Color.DodgerBlue;
            this.txttimkiemHDB.BorderRadius = 15;
            this.txttimkiemHDB.BorderSize = 2;
            this.txttimkiemHDB.ForeColor = System.Drawing.Color.Black;
            this.txttimkiemHDB.Location = new System.Drawing.Point(657, 47);
            this.txttimkiemHDB.Name = "txttimkiemHDB";
            this.txttimkiemHDB.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.txttimkiemHDB.Size = new System.Drawing.Size(285, 29);
            this.txttimkiemHDB.TabIndex = 1;
            this.txttimkiemHDB.Texts = "";
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(75, 53);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(71, 15);
            this.guna2HtmlLabel1.TabIndex = 3;
            this.guna2HtmlLabel1.Text = "Ngày bắt đầu:";
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(329, 55);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(73, 15);
            this.guna2HtmlLabel2.TabIndex = 4;
            this.guna2HtmlLabel2.Text = "Ngày kết thúc:";
            // 
            // dtstart
            // 
            this.dtstart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dtstart.Checked = true;
            this.dtstart.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dtstart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtstart.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtstart.Location = new System.Drawing.Point(150, 47);
            this.dtstart.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtstart.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtstart.Name = "dtstart";
            this.dtstart.Size = new System.Drawing.Size(170, 28);
            this.dtstart.TabIndex = 5;
            this.dtstart.Value = new System.DateTime(2025, 5, 23, 19, 17, 12, 305);
            // 
            // dtend
            // 
            this.dtend.Checked = true;
            this.dtend.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dtend.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtend.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtend.Location = new System.Drawing.Point(408, 47);
            this.dtend.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtend.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtend.Name = "dtend";
            this.dtend.Size = new System.Drawing.Size(170, 28);
            this.dtend.TabIndex = 6;
            this.dtend.Value = new System.DateTime(2025, 5, 23, 19, 17, 12, 305);
            // 
            // btnLoc
            // 
            this.btnLoc.BorderRadius = 10;
            this.btnLoc.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLoc.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLoc.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLoc.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(514, 86);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(141, 18);
            this.btnLoc.TabIndex = 7;
            this.btnLoc.Text = "Lọc theo ngày tháng";
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(733, 26);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(136, 15);
            this.guna2HtmlLabel3.TabIndex = 8;
            this.guna2HtmlLabel3.Text = "Tìm kiếm theo mã nhân viên";
            // 
            // btnExcel
            // 
            this.btnExcel.BorderRadius = 15;
            this.btnExcel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExcel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExcel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExcel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExcel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Image = global::DuAnQuanLyQuancafe.Properties.Resources._2993694_brand_brands_excel_logo_logos_icon;
            this.btnExcel.Location = new System.Drawing.Point(774, 534);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(118, 35);
            this.btnExcel.TabIndex = 9;
            this.btnExcel.Text = "Xuất ra file Excel";
            this.btnExcel.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // find
            // 
            this.find.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.find.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.find.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.find.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.find.FillColor = System.Drawing.Color.White;
            this.find.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.find.ForeColor = System.Drawing.Color.White;
            this.find.Image = global::DuAnQuanLyQuancafe.Properties.Resources._5402443_search_find_magnifier_magnifying_magnifying_glass_icon;
            this.find.Location = new System.Drawing.Point(901, 53);
            this.find.Name = "find";
            this.find.Size = new System.Drawing.Size(32, 21);
            this.find.TabIndex = 10;
            this.find.Click += new System.EventHandler(this.find_Click_1);
            // 
            // FrmHDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 626);
            this.Controls.Add(this.find);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.guna2HtmlLabel3);
            this.Controls.Add(this.btnLoc);
            this.Controls.Add(this.dtend);
            this.Controls.Add(this.dtstart);
            this.Controls.Add(this.guna2HtmlLabel2);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.txttimkiemHDB);
            this.Controls.Add(this.dgvHDB);
            this.Name = "FrmHDB";
            this.Text = "FrmHDB";
            ((System.ComponentModel.ISupportInitialize)(this.dgvHDB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2DataGridView dgvHDB;
        private RoundedTextBox txttimkiemHDB;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtstart;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtend;
        private Guna.UI2.WinForms.Guna2Button btnLoc;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2Button btnExcel;
        private Guna.UI2.WinForms.Guna2Button find;
    }
}