using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
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
        private class SanPhamThanhToan
        {
            public SanPhamModel SanPham { get; set; }
            public int SoLuong { get; set; }
            public Label LabelSoLuong { get; set; }
        }
        private Dictionary<string, SanPhamThanhToan> danhSachThanhToan = new Dictionary<string, SanPhamThanhToan>();
        private List<SanPhamModel> dsSanPhamFull = new List<SanPhamModel>();
        private int hoaDonHienTai = 1;
        NhanVienModel nhanvien;
        public FrmCapThap(NhanVienModel nhanvien)
        {
            InitializeComponent();
            this.nhanvien = nhanvien;
            this.Load += FrmCapThap_Load;
            guna2Button1.Click += (s, e) => ChuyenHoaDon(1);
            guna2Button2.Click += (s, e) => ChuyenHoaDon(2);
            printDocument.PrintPage += PrintDocument_PrintPage;
            roundedTextBox1.TextChanged += RoundedTextBox1_TextChanged;
        }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
            int cornerRadius = 30; // chỉnh độ cong ở đây
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle bounds = guna2Panel1.ClientRectangle;
            using (GraphicsPath path = GetRoundedRectPath(bounds, cornerRadius))
            {
                guna2Panel1.Region = new Region(path); // tạo hình bo góc
            }

        }
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90); // góc trái trên
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90); // góc phải trên
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90); // góc phải dưới
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90); // góc trái dưới
            path.CloseFigure();
            return path;
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }
        // Thêm vài sản phẩm mẫu khi form load
        private void FrmCapThap_Load(object sender, EventArgs e)
        {
            List<LoaiModel> danhSachLoai = LoaiController.LayDanhSachLoai(); // hoặc gọi trực tiếp từ lớp chứa
            List<CongDungModel> danhsachCongDung = CongDungController.LayMaCongDung();
            if (dgvSanPhamm == null)
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.");
                return;
            }
            else
            {
                SanPhamController sanPhamController = new SanPhamController();
                List<SanPhamModel> dsSanPham = sanPhamController.LayDanhSachSanPham();
                dgvSanPhamm.DataSource = dsSanPham;
                dgvSanPhamm.Columns["MaSP"].HeaderText = "Mã Sản Phẩm";
                dgvSanPhamm.Columns["TenSP"].HeaderText = "Tên Sản Phẩm";
                dgvSanPhamm.Columns["TenLoai"].HeaderText = "Mã Loại"; // Hiển thị tên loại thay vì mã
                dgvSanPhamm.Columns["GiaNhap"].HeaderText = "Giá Nhập";
                dgvSanPhamm.Columns["GiaBan"].HeaderText = "Giá Bán";
                dgvSanPhamm.Columns["TenCongDung"].HeaderText = "Công Dụng"; // Hiển thị tên công dụng thay vì mã

                // Ẩn các cột không cần thiết
                dgvSanPhamm.Columns["SoLuong"].Visible = false;
                dgvSanPhamm.Columns["MaLoai"].Visible = false;
                dgvSanPhamm.Columns["MaCongDung"].Visible = false;
                dgvSanPhamm.Columns["HinhAnh"].Visible = false;
                dgvSanPhamm.AutoGenerateColumns = false;

            }
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
           
                FlowLayoutPanel itemPanel = new FlowLayoutPanel();
                itemPanel.FlowDirection = FlowDirection.LeftToRight;
                itemPanel.AutoSize = true;
                itemPanel.WrapContents = false;
                itemPanel.Padding = new Padding(5);

                Label lblTen = new Label();
                lblTen.Text = sp.TenSP;
                lblTen.AutoSize = true;
                lblTen.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lblTen.Margin = new Padding(5, 0, 10, 0);

                Label lblGia = new Label();
                lblGia.Text = $"{sp.GiaBan:N0} VNĐ";
                lblGia.AutoSize = true;
                lblGia.Font = new Font("Segoe UI", 10);
                lblGia.Margin = new Padding(5, 0, 10, 0);

                Label lblSoLuong = new Label();
                lblSoLuong.Text = soLuong.ToString();
                lblSoLuong.Width = 20;
                lblSoLuong.TextAlign = ContentAlignment.MiddleCenter;
                lblSoLuong.Font = new Font("Segoe UI", 10);
                lblSoLuong.Margin = new Padding(0, 0, 5, 0);

                Button btnCong = new Button();
                btnCong.Text = "+";
                btnCong.Width = 25;
                btnCong.Height = 25;
                btnCong.Margin = new Padding(5, 0, 5, 0);
                btnCong.Click += (s, ev) =>
                {
                    int sl = int.Parse(lblSoLuong.Text);
                    lblSoLuong.Text = (++sl).ToString();
                };

                Button btnTru = new Button();
                btnTru.Text = "-";
                btnTru.Width = 25;
                btnTru.Height = 25;
                btnTru.Margin = new Padding(5, 0, 5, 0);
                btnTru.Click += (s, ev) =>
                {
                    int sl = int.Parse(lblSoLuong.Text);
                    if (sl > 1)
                        lblSoLuong.Text = (--sl).ToString();
                };

                Button btnXoa = new Button();
                btnXoa.Text = "×";
                btnXoa.Width = 25;
                btnXoa.Height = 25;
                btnXoa.Margin = new Padding(5, 0, 0, 0);
                btnXoa.Click += (s, ev) =>
                {
                    panelThongTin.Controls.Remove(itemPanel);
                };
                // Căn chỉnh lại font và kích thước
                lblTen.Font = new Font("Segoe UI", 8, FontStyle.Bold);
                lblGia.Font = new Font("Segoe UI", 8);
                lblSoLuong.Font = new Font("Segoe UI", 8);

                btnCong.Width = btnCong.Height = 20;
                btnTru.Width = btnTru.Height = 20;
                btnXoa.Width = btnXoa.Height = 20;

                itemPanel.Padding = new Padding(2);
                lblTen.Margin = new Padding(2, 0, 5, 0);
                lblGia.Margin = new Padding(2, 0, 5, 0);
                lblSoLuong.Margin = new Padding(2, 0, 5, 0);
                btnCong.Margin = new Padding(2, 0, 2, 0);
                btnTru.Margin = new Padding(2, 0, 2, 0);
                btnXoa.Margin = new Padding(2, 0, 2, 0);

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
                    SoLuong = 1,
                    LabelSoLuong = lblSoLuong
                };
                //Khi bấm  + - thì cập nhật lại tiền thanh toán
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
                CapNhatTongTien();
            }
        }
        private void CapNhatTongTien()
        {
            float tongTien = 0;

            foreach (var spTT in danhSachThanhToan.Values)
            {
                tongTien += spTT.SanPham.GiaBan * spTT.SoLuong;
            }

            float giamGia = 0; // Nếu có TextBox nhập giảm giá thì bạn parse ra
            float khachCanTra = tongTien - giamGia;

            lbltongtien.Text = $"{tongTien:N0} VNĐ";
            lblgiamgia.Text = $"{giamGia:N0} VNĐ";
            lblkhachtra.Text = $"{khachCanTra:N0} VNĐ";
        }

        //Xử lý chuyển hóa đơn
        private Dictionary<string, SanPhamThanhToan> hoaDon1 = new Dictionary<string, SanPhamThanhToan>();
        private Dictionary<string, SanPhamThanhToan> hoaDon2 = new Dictionary<string, SanPhamThanhToan>();
        private void ChuyenHoaDon(int hoaDonMoi)
        {
            // Lưu lại hóa đơn hiện tại
            if (hoaDonHienTai == 1)
                hoaDon1 = new Dictionary<string, SanPhamThanhToan>(danhSachThanhToan);
            else
                hoaDon2 = new Dictionary<string, SanPhamThanhToan>(danhSachThanhToan);

            // Cập nhật hóa đơn đang dùng
            hoaDonHienTai = hoaDonMoi;

            // Load dữ liệu hóa đơn mới
            danhSachThanhToan = hoaDonMoi == 1 ? new Dictionary<string, SanPhamThanhToan>(hoaDon1)
                                               : new Dictionary<string, SanPhamThanhToan>(hoaDon2);

            // Hiển thị lại các sản phẩm lên panelThongTin
            panelThongTin.Controls.Clear();
            var danhSachTam = danhSachThanhToan.Values.ToList();

            foreach (var item in danhSachTam)
            {
                ThemSanPhamVaoPanel(item.SanPham, item.SoLuong);
            }
            // Reset màu 2 nút
            guna2Button1.FillColor = Color.White;
            guna2Button1.ForeColor = Color.LightSkyBlue;

            guna2Button2.FillColor = Color.White;
            guna2Button2.ForeColor = Color.LightSkyBlue;

            // Đổi màu nút đang chọn
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

            FlowLayoutPanel itemPanel = new FlowLayoutPanel();
            itemPanel.FlowDirection = FlowDirection.LeftToRight;
            itemPanel.AutoSize = true;
            itemPanel.WrapContents = false;
            itemPanel.Padding = new Padding(2);
            itemPanel.Height = 30;
            itemPanel.Width = panelThongTin.Width - 10;

            Label lblTen = new Label();
            lblTen.Text = sp.TenSP;
            lblTen.AutoSize = true;
            lblTen.Font = new Font("Segoe UI", 8, FontStyle.Bold);

            Label lblGia = new Label();
            lblGia.Text = $"{sp.GiaBan:N0} VNĐ";
            lblGia.AutoSize = true;
            lblGia.Font = new Font("Segoe UI", 8);

            Label lblSL = new Label();
            lblSL.Text = soLuong.ToString();
            lblSL.Font = new Font("Segoe UI", 8);
            lblSL.Width = 20;

            Button btnCong = new Button() { Text = "+", Width = 20, Height = 20 };
            Button btnTru = new Button() { Text = "-", Width = 20, Height = 20 };
            Button btnXoa = new Button() { Text = "×", Width = 20, Height = 20 };

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
        // xử lý in hóa đơn
        private PrintDocument printDocument = new PrintDocument();
        private string noiDungCanIn = "";

        private string TaoNoiDungHoaDon()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("====== HÓA ĐƠN BÁN HÀNG ======");
            sb.AppendLine($"Thời gian: {DateTime.Now}");
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
            e.Graphics.DrawString(noiDungCanIn, new Font("Segoe UI", 10), Brushes.Black, new RectangleF(40, 40, e.MarginBounds.Width, e.MarginBounds.Height));
        }  
        private void panelThongtin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            noiDungCanIn = TaoNoiDungHoaDon();

            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = printDocument;
            previewDialog.Width = 800;
            previewDialog.Height = 600;
            previewDialog.ShowDialog();
        }
        //xu ly tim kiem
        private void RoundedTextBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = roundedTextBox1.Texts.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                // Hiển thị tất cả sản phẩm khi ô tìm kiếm trống
                dgvSanPhamm.DataSource = dsSanPhamFull;
            }
            else
            {
                // Lọc danh sách sản phẩm theo tên hoặc mã sản phẩm chứa từ khóa
                var filteredList = dsSanPhamFull
                    .Where(sp => sp.TenSP.ToLower().Contains(searchText) || sp.MaSP.ToLower().Contains(searchText))
                    .ToList();

                dgvSanPhamm.DataSource = filteredList;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (danhSachThanhToan.Count == 0)
            {
                MessageBox.Show("Chưa có sản phẩm nào trong hóa đơn!");
                return;
            }

            var hoaDon = new HoaDonBanModel
            {
                MaNV = nhanvien.MaNV,
                NgayBan = DateTime.Now,
                Tongtien = lbltongtien.Text == "" ? 0 : float.Parse(lbltongtien.Text.Replace(" VNĐ", "").Replace(",", ""))
            };
            HoaDonBanController.LuuHoaDon(hoaDon);

            MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            panelThongTin.Controls.Clear();          // Xóa các item hiển thị trong panel
            danhSachThanhToan.Clear();               // Xóa danh sách sản phẩm thanh toán

            // Reset các label về mặc định, ví dụ:
            lbltongtien.Text = "0 VNĐ";
            lblgiamgia.Text = "0 VNĐ";
            lblkhachtra.Text = "0 VNĐ";
        }
        private void dgvSanPhamm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
