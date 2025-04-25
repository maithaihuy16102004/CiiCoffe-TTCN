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
namespace DuAnQuanLyQuancafe.View
{
    public partial class FrmCapThap : Form
    {
        NhanVienModel nhanvien;
        public FrmCapThap(NhanVienModel nhanvien)
        {
            InitializeComponent();
            this.nhanvien = nhanvien;
        }
    }
}
