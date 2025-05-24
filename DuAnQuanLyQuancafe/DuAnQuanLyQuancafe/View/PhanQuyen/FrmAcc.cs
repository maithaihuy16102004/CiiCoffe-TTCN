using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DuAnQuanLyQuancafe.function;
using System.Data.SqlClient;

namespace DuAnQuanLyQuancafe.View
{
    public partial class FrmAcc : Form
    {
        private string maNVHienTai; // Lưu mã nhân viên hiện tại (đăng nhập)

        public FrmAcc(string maNV)
        {
            InitializeComponent();
            function.DatabaseHelper.fillcombophanquyen("SELECT 'NhanVien' AS LoaiTaiKhoan UNION SELECT 'Admin' AS LoaiTaiKhoan", cboLoaiTK, "LoaiTaiKhoan");
            maNVHienTai = maNV; // Nhận mã NV từ form đăng nhập
            cboLoaiTK.SelectedIndex = -1; // Chọn giá trị đầu tiên
        }

        private void FrmAcc_Load(object sender, EventArgs e)
        {
            // Khởi tạo giao diện và tải dữ liệu khi form được load
            KhoiTaoGiaoDien();
            LoadDuLieu();
            KiemTraQuyen();
        }

        private void KhoiTaoGiaoDien()
        {
            // Khởi tạo các cột với tiêu đề
            dgvTaiKhoan.Columns.Add("MaNV", "Mã NV");
            dgvTaiKhoan.Columns.Add("TenNV", "Tên NV");
            dgvTaiKhoan.Columns.Add("LoaiTaiKhoan", "Loại TK");

            // Tùy chỉnh giao diện DataGridView
            dgvTaiKhoan.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80); // Màu xanh đậm
            dgvTaiKhoan.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTaiKhoan.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvTaiKhoan.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTaiKhoan.EnableHeadersVisualStyles = false;

            dgvTaiKhoan.DefaultCellStyle.BackColor = Color.White;
            dgvTaiKhoan.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255); // Màu xanh nhạt xen kẽ
            dgvTaiKhoan.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvTaiKhoan.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvTaiKhoan.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvTaiKhoan.RowTemplate.Height = 35;
            dgvTaiKhoan.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTaiKhoan.GridColor = Color.FromArgb(200, 200, 200);

            dgvTaiKhoan.ReadOnly = true;
            dgvTaiKhoan.RowHeadersVisible = false;
            dgvTaiKhoan.AllowUserToResizeColumns = false;
            dgvTaiKhoan.AllowUserToResizeRows = false;
            dgvTaiKhoan.BorderStyle = BorderStyle.None;
            dgvTaiKhoan.BackgroundColor = Color.White;

            // Điều chỉnh chiều rộng cột
            dgvTaiKhoan.Columns["MaNV"].Width = 100;
            dgvTaiKhoan.Columns["TenNV"].Width = 250; // Tăng chiều rộng để hiển thị tên đầy đủ
            dgvTaiKhoan.Columns["LoaiTaiKhoan"].Width = 120;

            // Căn chỉnh nội dung cột
            dgvTaiKhoan.Columns["MaNV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTaiKhoan.Columns["TenNV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvTaiKhoan.Columns["LoaiTaiKhoan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTaiKhoan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvTaiKhoan.ColumnHeadersHeight = 45;

            dgvTaiKhoan.RowTemplate.Height = 45;

        }

        private void LoadDuLieu()
        {
            dgvTaiKhoan.Rows.Clear();

            string sql = "SELECT TAIKHOAN.MaNV, NhanVien.TenNV, TAIKHOAN.LoaiTaiKhoan " +
                        "FROM TAIKHOAN INNER JOIN NhanVien ON TAIKHOAN.MaNV = NhanVien.MaNV";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dgvTaiKhoan.Rows.Add(
                                    reader["MaNV"]?.ToString(),
                                    reader["TenNV"]?.ToString(),
                                    reader["LoaiTaiKhoan"]?.ToString()
                                );
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void KiemTraQuyen()
        {
            Button btnCapNhat = (Button)this.Controls["btnCapNhat"];
            bool isAdmin = KiemTraQuyen(maNVHienTai);
            btnSua.Enabled = isAdmin;
        }

        private bool KiemTraQuyen(string maNV)
        {
            string sql = "SELECT LoaiTaiKhoan FROM TaiKhoan WHERE MaNV = @MaNV";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        object result = cmd.ExecuteScalar();
                        return result != null && result.ToString() == "Admin";
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi khi kiểm tra quyền: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTaiKhoan.Rows[e.RowIndex];
                txtMa.Text = row.Cells["MaNV"].Value?.ToString();
                txtTen.Text = row.Cells["TenNV"].Value?.ToString();
                cboLoaiTK.SelectedValue = row.Cells["LoaiTaiKhoan"].Value?.ToString();
            }
        }

        private void LamMoi()
        {
            txtMa.Text = string.Empty;
            txtTen.Text = string.Empty;
            cboLoaiTK.SelectedIndex = -1;
        }

        private void dgvTaiKhoan_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.CurrentRow == null || dgvTaiKhoan.Rows.Count == 0)
            {
                LamMoi();
                return;
            }
            try
            {
                txtMa.Enabled = false; // Không cho phép sửa mã
                txtTen.Enabled = false; // Không cho phép sửa mã
                txtMa.Text = dgvTaiKhoan.CurrentRow.Cells["MaNV"]?.Value?.ToString() ?? "";
                txtTen.Text = dgvTaiKhoan.CurrentRow.Cells["TenNV"]?.Value?.ToString() ?? "";
                cboLoaiTK.SelectedValue = dgvTaiKhoan.CurrentRow.Cells["LoaiTaiKhoan"]?.Value?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị thông tin nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string updateSql = "UPDATE TAIKHOAN SET LoaiTaiKhoan = @LoaiTaiKhoan WHERE MaNV = @MaNV";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    using (SqlCommand cmd = new SqlCommand(updateSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", txtMa.Text);
                        cmd.Parameters.AddWithValue("@LoaiTaiKhoan", cboLoaiTK.SelectedValue);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật Role thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDuLieu();
                            LamMoi();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy tài khoản để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimkiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadDuLieu(); // Tải lại toàn bộ dữ liệu nếu ô tìm kiếm trống
                return;
            }

            dgvTaiKhoan.Rows.Clear();
            string sql = "SELECT TAIKHOAN.MaNV, NhanVien.TenNV, TAIKHOAN.LoaiTaiKhoan " +
                        "FROM TAIKHOAN INNER JOIN NhanVien ON TAIKHOAN.MaNV = NhanVien.MaNV " +
                        "WHERE TAIKHOAN.MaNV LIKE @Keyword OR NhanVien.TenNV LIKE @Keyword OR TAIKHOAN.LoaiTaiKhoan LIKE @Keyword";
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dgvTaiKhoan.Rows.Add(
                                    reader["MaNV"]?.ToString(),
                                    reader["TenNV"]?.ToString(),
                                    reader["LoaiTaiKhoan"]?.ToString()
                                );
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click_1(object sender, EventArgs e)
        {
            LamMoi();
        }
    }
}