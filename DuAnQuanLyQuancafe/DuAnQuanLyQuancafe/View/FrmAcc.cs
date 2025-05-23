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
            function.DatabaseHelper.fillcombophanquyen("SELECT LoaiTaiKhoan From TaiKhoan", cboLoaiTK, "LoaiTaiKhoan");
            maNVHienTai = maNV; // Nhận mã NV từ form đăng nhập
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
            dgvTaiKhoan.Columns.Add("MaNV", "Mã NV");
            dgvTaiKhoan.Columns.Add("TenNV", "Tên NV");
            dgvTaiKhoan.Columns.Add("LoaiTaiKhoan", "Loại TK");
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

       

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
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
    }
}