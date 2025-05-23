using System;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe
{
    public partial class Product : UserControl
    {
        // Sự kiện click để gửi dữ liệu ra ngoài form
        public event EventHandler<ProductClickEventArgs> OnProductClick;

        public Product()
        {
            InitializeComponent();
            guna2GradientButton1__valid.Visible = false;

            // Gắn sự kiện click cho tất cả các control con
            this.Click += Product_Click;
            foreach (Control control in this.Controls)
            {
                control.Click += Product_Click;
            }
        }

        // --- Public properties để gán từ ngoài ---
        public string ProductName
        {
            get => lblProductName.Text;
            set => lblProductName.Text = value;
        }

        public decimal ProductPrice
        {
            get
            {
                decimal.TryParse(lblProductPrice.Text.Replace("đ", "").Replace(",", "").Trim(), out decimal price);
                return price;
            }
            set => lblProductPrice.Text = value.ToString("N0") + " đ";
        }

        // --- Khi click vào sản phẩm ---
        private void Product_Click(object sender, EventArgs e)
        {
            // Hiện tick
            guna2GradientButton1__valid.Visible = true;

            // Gửi dữ liệu ra ngoài Form
            OnProductClick?.Invoke(this, new ProductClickEventArgs
            {
                ProductName = this.ProductName,
                ProductPrice = this.ProductPrice
            });
        }
        private void guna2GradientButton1__valid_Click(object sender, EventArgs e)
        {
            if (guna2GradientButton1__valid.Visible == false)
            {
                guna2GradientButton1__valid.Visible = true;
            }
            else
            {
                guna2GradientButton1__valid.Visible = false;

                // (Không cần xử lý click của tick nữa nếu dùng sự kiện ở trên)
            }
        }
        public bool IsTickVisible
        {
            get => guna2GradientButton1__valid.Visible;
            set => guna2GradientButton1__valid.Visible = value;
        }

    }


    // --- Lớp chứa dữ liệu truyền ra Form ---
    public class ProductClickEventArgs : EventArgs
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
