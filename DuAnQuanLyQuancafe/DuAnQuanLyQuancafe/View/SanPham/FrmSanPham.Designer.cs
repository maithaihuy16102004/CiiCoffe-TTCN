namespace DuAnQuanLyQuancafe.View
{
    partial class FrmSanPham
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSanPham));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.picAnh = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnChon = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cbLoai = new System.Windows.Forms.ComboBox();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
            this.btnSua = new Guna.UI2.WinForms.Guna2ImageButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.txtMa = new System.Windows.Forms.TextBox();
            this.btnXoa = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btn = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnThem = new Guna.UI2.WinForms.Guna2ImageButton();
            this.dgvSanPham = new Guna.UI2.WinForms.Guna2DataGridView();
            this.cbCongDung = new System.Windows.Forms.ComboBox();
            this.txtGiaNhap = new System.Windows.Forms.TextBox();
            this.txtSoluong = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).BeginInit();
            this.SuspendLayout();
            // 
            // picAnh
            // 
            this.picAnh.BackColor = System.Drawing.Color.Transparent;
            this.picAnh.BorderRadius = 50;
            this.picAnh.ImageRotate = 0F;
            this.picAnh.Location = new System.Drawing.Point(854, 615);
            this.picAnh.Name = "picAnh";
            this.picAnh.Size = new System.Drawing.Size(77, 74);
            this.picAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAnh.TabIndex = 91;
            this.picAnh.TabStop = false;
            this.picAnh.UseTransparentBackground = true;
            // 
            // btnChon
            // 
            this.btnChon.Location = new System.Drawing.Point(771, 655);
            this.btnChon.Name = "btnChon";
            this.btnChon.Size = new System.Drawing.Size(62, 23);
            this.btnChon.TabIndex = 90;
            this.btnChon.Text = "Chọn ảnh";
            this.btnChon.UseVisualStyleBackColor = true;
            this.btnChon.Click += new System.EventHandler(this.btnChon_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(746, 638);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 89;
            this.label8.Text = "Chọn ảnh sản phẩm";
            // 
            // cbLoai
            // 
            this.cbLoai.FormattingEnabled = true;
            this.cbLoai.Location = new System.Drawing.Point(224, 652);
            this.cbLoai.Name = "cbLoai";
            this.cbLoai.Size = new System.Drawing.Size(77, 21);
            this.cbLoai.TabIndex = 88;
            // 
            // txtGiaBan
            // 
            this.txtGiaBan.Location = new System.Drawing.Point(425, 652);
            this.txtGiaBan.Name = "txtGiaBan";
            this.txtGiaBan.Size = new System.Drawing.Size(83, 20);
            this.txtGiaBan.TabIndex = 85;
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.Transparent;
            this.btnSua.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnSua.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnSua.Image = ((System.Drawing.Image)(resources.GetObject("btnSua.Image")));
            this.btnSua.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnSua.ImageRotate = 0F;
            this.btnSua.ImageSize = new System.Drawing.Size(32, 32);
            this.btnSua.Location = new System.Drawing.Point(937, 630);
            this.btnSua.Name = "btnSua";
            this.btnSua.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnSua.Size = new System.Drawing.Size(48, 44);
            this.btnSua.TabIndex = 83;
            this.btnSua.UseTransparentBackground = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(621, 635);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 82;
            this.label7.Text = "Công dụng";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(319, 636);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 81;
            this.label6.Text = "Giá nhập";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(221, 638);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 80;
            this.label5.Text = "Loại SP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(112, 636);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 79;
            this.label4.Text = "Tên sản phẩm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 636);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 78;
            this.label3.Text = "Mã sản phẩm";
            // 
            // txtTen
            // 
            this.txtTen.Location = new System.Drawing.Point(115, 652);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(100, 20);
            this.txtTen.TabIndex = 77;
            // 
            // txtMa
            // 
            this.txtMa.Location = new System.Drawing.Point(21, 652);
            this.txtMa.Name = "txtMa";
            this.txtMa.Size = new System.Drawing.Size(75, 20);
            this.txtMa.TabIndex = 76;
            // 
            // btnXoa
            // 
            this.btnXoa.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnXoa.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnXoa.Image = ((System.Drawing.Image)(resources.GetObject("btnXoa.Image")));
            this.btnXoa.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnXoa.ImageRotate = 0F;
            this.btnXoa.ImageSize = new System.Drawing.Size(32, 32);
            this.btnXoa.Location = new System.Drawing.Point(140, 8);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnXoa.Size = new System.Drawing.Size(39, 39);
            this.btnXoa.TabIndex = 75;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btn
            // 
            this.btn.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btn.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.btn.Image = ((System.Drawing.Image)(resources.GetObject("btn.Image")));
            this.btn.ImageOffset = new System.Drawing.Point(0, 0);
            this.btn.ImageRotate = 0F;
            this.btn.ImageSize = new System.Drawing.Size(32, 32);
            this.btn.Location = new System.Drawing.Point(86, 8);
            this.btn.Name = "btn";
            this.btn.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btn.Size = new System.Drawing.Size(48, 44);
            this.btn.TabIndex = 74;
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.Location = new System.Drawing.Point(-12, 53);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(1021, 10);
            this.guna2Separator1.TabIndex = 73;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(736, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 16);
            this.label2.TabIndex = 72;
            this.label2.Text = "Tìm kiếm";
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.IconLeft = ((System.Drawing.Image)(resources.GetObject("txtTimKiem.IconLeft")));
            this.txtTimKiem.Location = new System.Drawing.Point(739, 27);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Nhập tên nhân viên";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(237, 25);
            this.txtTimKiem.TabIndex = 71;
            // 
            // btnThem
            // 
            this.btnThem.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnThem.HoverState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnThem.Image = ((System.Drawing.Image)(resources.GetObject("btnThem.Image")));
            this.btnThem.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnThem.ImageRotate = 0F;
            this.btnThem.ImageSize = new System.Drawing.Size(32, 32);
            this.btnThem.Location = new System.Drawing.Point(41, 8);
            this.btnThem.Name = "btnThem";
            this.btnThem.PressedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnThem.Size = new System.Drawing.Size(39, 39);
            this.btnThem.TabIndex = 70;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // dgvSanPham
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dgvSanPham.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSanPham.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSanPham.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSanPham.ColumnHeadersHeight = 15;
            this.dgvSanPham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSanPham.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSanPham.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvSanPham.Location = new System.Drawing.Point(21, 69);
            this.dgvSanPham.Name = "dgvSanPham";
            this.dgvSanPham.RowHeadersVisible = false;
            this.dgvSanPham.Size = new System.Drawing.Size(961, 540);
            this.dgvSanPham.TabIndex = 69;
            this.dgvSanPham.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvSanPham.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvSanPham.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvSanPham.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvSanPham.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvSanPham.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvSanPham.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvSanPham.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvSanPham.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvSanPham.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSanPham.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvSanPham.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvSanPham.ThemeStyle.HeaderStyle.Height = 15;
            this.dgvSanPham.ThemeStyle.ReadOnly = false;
            this.dgvSanPham.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvSanPham.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSanPham.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvSanPham.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvSanPham.ThemeStyle.RowsStyle.Height = 22;
            this.dgvSanPham.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvSanPham.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvSanPham.Click += new System.EventHandler(this.dgvSanPham_Click);
            // 
            // cbCongDung
            // 
            this.cbCongDung.FormattingEnabled = true;
            this.cbCongDung.Location = new System.Drawing.Point(624, 651);
            this.cbCongDung.Name = "cbCongDung";
            this.cbCongDung.Size = new System.Drawing.Size(94, 21);
            this.cbCongDung.TabIndex = 92;
            // 
            // txtGiaNhap
            // 
            this.txtGiaNhap.Location = new System.Drawing.Point(322, 653);
            this.txtGiaNhap.Name = "txtGiaNhap";
            this.txtGiaNhap.Size = new System.Drawing.Size(87, 20);
            this.txtGiaNhap.TabIndex = 93;
            // 
            // txtSoluong
            // 
            this.txtSoluong.Location = new System.Drawing.Point(527, 651);
            this.txtSoluong.Name = "txtSoluong";
            this.txtSoluong.Size = new System.Drawing.Size(69, 20);
            this.txtSoluong.TabIndex = 94;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(422, 635);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 95;
            this.label9.Text = "Giá bán";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(524, 635);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 96;
            this.label10.Text = "Số lượng";
            // 
            // FrmSanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 698);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtSoluong);
            this.Controls.Add(this.txtGiaNhap);
            this.Controls.Add(this.cbCongDung);
            this.Controls.Add(this.picAnh);
            this.Controls.Add(this.btnChon);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbLoai);
            this.Controls.Add(this.txtGiaBan);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTen);
            this.Controls.Add(this.txtMa);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.guna2Separator1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dgvSanPham);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSanPham";
            this.Text = "FrmSanPham";
            this.Load += new System.EventHandler(this.FrmSanPham_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox picAnh;
        private System.Windows.Forms.Button btnChon;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbLoai;
        private System.Windows.Forms.TextBox txtGiaBan;
        private Guna.UI2.WinForms.Guna2ImageButton btnSua;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.TextBox txtMa;
        private Guna.UI2.WinForms.Guna2ImageButton btnXoa;
        private Guna.UI2.WinForms.Guna2ImageButton btn;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private Guna.UI2.WinForms.Guna2ImageButton btnThem;
        private Guna.UI2.WinForms.Guna2DataGridView dgvSanPham;
        private System.Windows.Forms.ComboBox cbCongDung;
        private System.Windows.Forms.TextBox txtGiaNhap;
        private System.Windows.Forms.TextBox txtSoluong;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}