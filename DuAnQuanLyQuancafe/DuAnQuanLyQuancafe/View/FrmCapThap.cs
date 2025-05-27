using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DuAnQuanLyQuancafe.Controller;
using DuAnQuanLyQuancafe.Model;

namespace DuAnQuanLyQuancafe.View
{
    public partial class FrmCapThap : Form
    {
        // Nested class for payment items
        private class SanPhamThanhToan
        {
            public SanPhamModel SanPham { get; set; }
            public int SoLuong { get; set; }
            public Label LabelSoLuong { get; set; }
        }

        // Data structures
        private Dictionary<string, SanPhamThanhToan> danhSachThanhToan = new Dictionary<string, SanPhamThanhToan>();
        private List<SanPhamModel> dsSanPhamFull = new List<SanPhamModel>();
        private int hoaDonHienTai = 1;
        private NhanVienModel nhanvien;
        private Image logoImage; 
        private PrintDocument printDocument = new PrintDocument();

        public FrmCapThap(NhanVienModel nhanvien)
        {
            InitializeComponent();
            this.nhanvien = nhanvien;

            // Event handlers
            this.Load += FrmCapThap_Load;
            guna2Button1.Click += (s, e) => ChuyenHoaDon(1);
            guna2Button2.Click += (s, e) => ChuyenHoaDon(2);
            printDocument.PrintPage += PrintDocument_PrintPage;
            roundedTextBox1.TextChanged += RoundedTextBox1_TextChanged;
            dgvSanPhamm.ReadOnly = true;

            // Load logo from resources
            LoadLogo();
        }

        private void LoadLogo()
        {
            try
            {
                
                logoImage = Properties.Resources.logo; // Replace "logo" with the actual resource name if different
                if (logoImage == null)
                {
                    MessageBox.Show("Không tìm thấy logo trong tài nguyên. Vui lòng kiểm tra lại.", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải logo: {ex.Message}", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            int cornerRadius = 30;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle bounds = guna2Panel1.ClientRectangle;
            using (GraphicsPath path = GetRoundedRectPath(bounds, cornerRadius))
            {
                guna2Panel1.Region = new Region(path);
            }
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void FrmCapThap_Load(object sender, EventArgs e)
        {
            List<LoaiModel> danhSachLoai = LoaiController.LayDanhSachLoai();
            List<CongDungModel> danhsachCongDung = CongDungController.LayMaCongDung();

            if (dgvSanPhamm == null)
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SanPhamController sanPhamController = new SanPhamController();
            List<SanPhamModel> dsSanPham = sanPhamController.LayDanhSachSanPham();
            dgvSanPhamm.DataSource = dsSanPham;
            dsSanPhamFull = dsSanPham;

            
            dgvSanPhamm.Columns["MaSP"].HeaderText = "Mã Sản Phẩm";
            dgvSanPhamm.Columns["TenSP"].HeaderText = "Tên Sản Phẩm";
            dgvSanPhamm.Columns["TenLoai"].HeaderText = "Mã Loại";
            dgvSanPhamm.Columns["GiaNhap"].HeaderText = "Giá Nhập";
            dgvSanPhamm.Columns["GiaBan"].HeaderText = "Giá Bán";
            dgvSanPhamm.Columns["TenCongDung"].HeaderText = "Công Dụng";

            dgvSanPhamm.Columns["GiaNhap"].Visible = false;
            dgvSanPhamm.Columns["SoLuong"].Visible = false;
            dgvSanPhamm.Columns["MaLoai"].Visible = false;
            dgvSanPhamm.Columns["MaCongDung"].Visible = false;
            dgvSanPhamm.Columns["HinhAnh"].Visible = false;
            dgvSanPhamm.AutoGenerateColumns = false;

          
            if (nhanvien != null)
            {
                lblManhanvien.Text = nhanvien.MaNV;
                lblTennhanvien.Text = nhanvien.TenNV;
            }
        }

        private SanPhamModel sanPhamDangChon;
        private void dgvSanPhamm_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvSanPhamm.Rows[e.RowIndex].DataBoundItem is SanPhamModel sp)
            {
                string maSP = sp.MaSP;
                int soLuong = 1;

                FlowLayoutPanel itemPanel = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.LeftToRight,
                    AutoSize = true,
                    WrapContents = false,
                    Padding = new Padding(2),
                    Height = 30,
                    Width = panelThongTin.Width - 10
                };

                Label lblTen = new Label { Text = sp.TenSP, AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Bold), Margin = new Padding(2, 0, 5, 0) };
                Label lblGia = new Label { Text = $"{sp.GiaBan:N0} VNĐ", AutoSize = true, Font = new Font("Segoe UI", 8), Margin = new Padding(2, 0, 5, 0) };
                Label lblSoLuong = new Label { Text = soLuong.ToString(), Width = 20, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 8), Margin = new Padding(2, 0, 5, 0) };

                Button btnCong = new Button { Text = "+", Width = 20, Height = 20, Margin = new Padding(2, 0, 2, 0) };
                Button btnTru = new Button { Text = "-", Width = 20, Height = 20, Margin = new Padding(2, 0, 2, 0) };
                Button btnXoa = new Button { Text = "×", Width = 20, Height = 20, Margin = new Padding(2, 0, 2, 0) };

                // Button event handlers
                btnCong.Click += (s, ev) =>
                {
                    if (danhSachThanhToan.ContainsKey(maSP))
                    {
                        danhSachThanhToan[maSP].SoLuong++;
                        lblSoLuong.Text = danhSachThanhToan[maSP].SoLuong.ToString();
                        CapNhatTongTien();
                    }
                };

                btnTru.Click += (s, ev) =>
                {
                    if (danhSachThanhToan.ContainsKey(maSP) && danhSachThanhToan[maSP].SoLuong > 1)
                    {
                        danhSachThanhToan[maSP].SoLuong--;
                        lblSoLuong.Text = danhSachThanhToan[maSP].SoLuong.ToString();
                        CapNhatTongTien();
                    }
                };

                btnXoa.Click += (s, ev) =>
                {
                    panelThongTin.Controls.Remove(itemPanel);
                    danhSachThanhToan.Remove(maSP);
                    CapNhatTongTien();
                };

                // Add controls to panel
                itemPanel.Controls.Add(lblTen);
                itemPanel.Controls.Add(lblGia);
                itemPanel.Controls.Add(lblSoLuong);
                itemPanel.Controls.Add(btnCong);
                itemPanel.Controls.Add(btnTru);
                itemPanel.Controls.Add(btnXoa);

                panelThongTin.Controls.Add(itemPanel);

                danhSachThanhToan[maSP] = new SanPhamThanhToan
                {
                    SanPham = sp,
                    SoLuong = soLuong,
                    LabelSoLuong = lblSoLuong
                };

                CapNhatTongTien();
            }
        }

        private void CapNhatTongTien()
        {
            float tongTien = danhSachThanhToan.Values.Sum(item => item.SanPham.GiaBan * item.SoLuong);
            float giamGia = 0; 
            float khachCanTra = tongTien - giamGia;

            lbltongtien.Text = $"{tongTien:N0} VNĐ";
            lblgiamgia.Text = $"{giamGia:N0} VNĐ";
            lblkhachtra.Text = $"{khachCanTra:N0} VNĐ";
        }

        // Handle invoice switching
        private Dictionary<string, SanPhamThanhToan> hoaDon1 = new Dictionary<string, SanPhamThanhToan>();
        private Dictionary<string, SanPhamThanhToan> hoaDon2 = new Dictionary<string, SanPhamThanhToan>();

        private void ChuyenHoaDon(int hoaDonMoi)
        {
            if (hoaDonHienTai == 1)
                hoaDon1 = new Dictionary<string, SanPhamThanhToan>(danhSachThanhToan);
            else
                hoaDon2 = new Dictionary<string, SanPhamThanhToan>(danhSachThanhToan);

            hoaDonHienTai = hoaDonMoi;
            danhSachThanhToan = hoaDonMoi == 1 ? new Dictionary<string, SanPhamThanhToan>(hoaDon1) : new Dictionary<string, SanPhamThanhToan>(hoaDon2);

            panelThongTin.Controls.Clear();
            foreach (var item in danhSachThanhToan.Values)
            {
                ThemSanPhamVaoPanel(item.SanPham, item.SoLuong);
            }

            // Update button colors
            guna2Button1.FillColor = Color.White;
            guna2Button1.ForeColor = Color.LightSkyBlue;
            guna2Button2.FillColor = Color.White;
            guna2Button2.ForeColor = Color.LightSkyBlue;

            if (hoaDonMoi == 1)
            {
                guna2Button1.FillColor = Color.LightSkyBlue;
                guna2Button1.ForeColor = Color.White;
            }
            else
            {
                guna2Button2.FillColor = Color.LightSkyBlue;
                guna2Button2.ForeColor = Color.White;
            }

            CapNhatTongTien();
        }

        private void ThemSanPhamVaoPanel(SanPhamModel sp, int soLuong)
        {
            string maSP = sp.MaSP;

            FlowLayoutPanel itemPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false,
                Padding = new Padding(2),
                Height = 30,
                Width = panelThongTin.Width - 10
            };

            Label lblTen = new Label { Text = sp.TenSP, AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Bold), Margin = new Padding(2, 0, 5, 0) };
            Label lblGia = new Label { Text = $"{sp.GiaBan:N0} VNĐ", AutoSize = true, Font = new Font("Segoe UI", 8), Margin = new Padding(2, 0, 5, 0) };
            Label lblSL = new Label { Text = soLuong.ToString(), Width = 20, TextAlign = ContentAlignment.MiddleCenter, Font = new Font("Segoe UI", 8), Margin = new Padding(2, 0, 5, 0) };

            Button btnCong = new Button { Text = "+", Width = 20, Height = 20, Margin = new Padding(2, 0, 2, 0) };
            Button btnTru = new Button { Text = "-", Width = 20, Height = 20, Margin = new Padding(2, 0, 2, 0) };
            Button btnXoa = new Button { Text = "×", Width = 20, Height = 20, Margin = new Padding(2, 0, 2, 0) };

            btnCong.Click += (s, ev) =>
            {
                danhSachThanhToan[maSP].SoLuong++;
                lblSL.Text = danhSachThanhToan[maSP].SoLuong.ToString();
                CapNhatTongTien();
            };

            btnTru.Click += (s, ev) =>
            {
                if (danhSachThanhToan[maSP].SoLuong > 1)
                {
                    danhSachThanhToan[maSP].SoLuong--;
                    lblSL.Text = danhSachThanhToan[maSP].SoLuong.ToString();
                    CapNhatTongTien();
                }
            };

            btnXoa.Click += (s, ev) =>
            {
                panelThongTin.Controls.Remove(itemPanel);
                danhSachThanhToan.Remove(maSP);
                CapNhatTongTien();
            };

            itemPanel.Controls.Add(lblTen);
            itemPanel.Controls.Add(lblGia);
            itemPanel.Controls.Add(lblSL);
            itemPanel.Controls.Add(btnCong);
            itemPanel.Controls.Add(btnTru);
            itemPanel.Controls.Add(btnXoa);

            panelThongTin.Controls.Add(itemPanel);

            danhSachThanhToan[maSP] = new SanPhamThanhToan
            {
                SanPham = sp,
                SoLuong = soLuong,
                LabelSoLuong = lblSL
            };
        }

        private string TaoNoiDungHoaDon()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("====== HÓA ĐƠN BÁN HÀNG ======");
            sb.AppendLine($"Thời gian: {DateTime.Now}");
            sb.AppendLine($"Nhân viên: {nhanvien?.TenNV ?? "Không xác định"} (Mã: {nhanvien?.MaNV ?? "N/A"})");
            sb.AppendLine();

            sb.AppendLine($"{"Tên",-25}{"Số lượng",10}{"Đơn giá",15}{"Thành tiền",15}");
            sb.AppendLine(new string('-', 70));

            float tongTien = 0;
            foreach (var item in danhSachThanhToan.Values)
            {
                float thanhTien = item.SanPham.GiaBan * item.SoLuong;
                tongTien += thanhTien;
                sb.AppendLine($"{item.SanPham.TenSP,-25}{item.SoLuong,10}{item.SanPham.GiaBan,15:N0}{thanhTien,15:N0}");
            }

            sb.AppendLine(new string('-', 70));
            sb.AppendLine($"TỔNG CỘNG: {tongTien:N0} VNĐ");

            return sb.ToString();
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (logoImage != null)
            {
                int logoWidth = 100;
                int logoHeight = 100;
                int x = (e.MarginBounds.Width - logoWidth) / 2; // Center the logo
                int y = 40;
                e.Graphics.DrawImage(logoImage, new Rectangle(x, y, logoWidth, logoHeight));
            }
            else
            {
                e.Graphics.DrawString("Không có logo để hiển thị.", new Font("Segoe UI", 10), Brushes.Red, new PointF(40, 40));
            }

            e.Graphics.DrawString(TaoNoiDungHoaDon(), new Font("Segoe UI", 10), Brushes.Black,
                                  new RectangleF(40, 160, e.MarginBounds.Width, e.MarginBounds.Height));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
        }

        private void RoundedTextBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = RemoveDiacritics(roundedTextBox1.Texts.Trim().ToLower());
            if (string.IsNullOrEmpty(searchText))
            {
                dgvSanPhamm.DataSource = dsSanPhamFull;
            }
            else
            {
                var filteredList = dsSanPhamFull
                    .Where(sp => RemoveDiacritics(sp.TenSP.ToLower()).Contains(searchText) ||
                                 RemoveDiacritics(sp.MaSP.ToLower()).Contains(searchText))
                    .ToList();
                dgvSanPhamm.DataSource = filteredList;
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (danhSachThanhToan.Count == 0)
            {
                MessageBox.Show("Chưa có sản phẩm nào trong hóa đơn!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            HoaDonBanController hoaDonBanController = new HoaDonBanController();
            var hoaDon = new HoaDonBanModel
            {
                MaNV = nhanvien.MaNV,
                NgayBan = DateTime.Now,
                Tongtien = lbltongtien.Text == "" ? 0 : float.Parse(lbltongtien.Text.Replace(" VNĐ", "").Replace(",", ""))
            };
            hoaDonBanController.LuuHoaDon(hoaDon);

            string maHDB = hoaDonBanController.LayMaHDBMoiNhat(hoaDon.MaNV, hoaDon.NgayBan);
            if (string.IsNullOrEmpty(maHDB))
            {
                MessageBox.Show("Lỗi không lấy được mã hóa đơn.", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<ChiTietHDBModel> chiTietList = new List<ChiTietHDBModel>();
            foreach (var item in danhSachThanhToan.Values)
            {
                chiTietList.Add(new ChiTietHDBModel
                {
                    MaHDB = maHDB,
                    MaSP = item.SanPham.MaSP,
                    SoLuong = item.SoLuong,
                    ThanhTien = item.SanPham.GiaBan * item.SoLuong,
                    KhuyenMai = ""
                });
            }

            ChiTietHDBController chiTietHDBController = new ChiTietHDBController();
            chiTietHDBController.LuuChiTiet(chiTietList);

            MessageBox.Show("Thanh toán thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            panelThongTin.Controls.Clear();
            danhSachThanhToan.Clear();

            lbltongtien.Text = "0 VNĐ";
            lblgiamgia.Text = "0 VNĐ";
            lblkhachtra.Text = "0 VNĐ";
        }

        private void btnPrint_Click_1(object sender, EventArgs e)
        {
            PrintPreviewDialog previewDialog = new PrintPreviewDialog
            {
                Document = printDocument,
                Width = 800,
                Height = 600
            };
            previewDialog.ShowDialog();
        }

        public static string RemoveDiacritics(string text)
        {
            var normalized = text.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder();
            foreach (var c in normalized)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(c);
                }
            }
            return builder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}