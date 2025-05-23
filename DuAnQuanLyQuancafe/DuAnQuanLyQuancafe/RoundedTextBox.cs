using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace DuAnQuanLyQuancafe
{
    public partial class RoundedTextBox: UserControl
    {
        private TextBox textBox = new TextBox();
        private int borderRadius = 15;
        private Color borderColor = Color.DodgerBlue;
        private int borderSize = 2;
        public RoundedTextBox()
        {
            InitializeComponent();
            this.Padding = new Padding(10, 5, 10, 5);
            textBox.BorderStyle = BorderStyle.None;
            textBox.Font = new Font("Segoe UI", 10);
            textBox.Dock = DockStyle.Fill;
            this.Controls.Add(textBox);
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.Size = new Size(250, 35);
        }
        [Category("Custom")]
        public string Texts
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        [Category("Custom")]
        public int BorderRadius
        {
            get { return borderRadius; }
            set { borderRadius = value; this.Invalidate(); }
        }

        [Category("Custom")]
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; this.Invalidate(); }
        }

        [Category("Custom")]
        public int BorderSize
        {
            get { return borderSize; }
            set { borderSize = value; this.Invalidate(); }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath path = GetRoundedRectPath(this.ClientRectangle, borderRadius))
            using (Pen pen = new Pen(borderColor, borderSize))
            {
                g.Clear(this.Parent.BackColor);
                g.FillPath(new SolidBrush(this.BackColor), path);
                g.DrawPath(pen, path);
            }
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float r = radius;
            path.StartFigure();
            path.AddArc(rect.Left, rect.Top, r, r, 180, 90);
            path.AddArc(rect.Right - r, rect.Top, r, r, 270, 90);
            path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
            path.AddArc(rect.Left, rect.Bottom - r, r, r, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void RoundedTextBox_Load(object sender, EventArgs e)
        {

        }
    }
}
