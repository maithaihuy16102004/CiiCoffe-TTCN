﻿using DuAnQuanLyQuancafe.function;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.Model
{
    public class NhaCungCapModel
    {
        // Các thuộc tính của nhà cung cấp
        public string MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string MoTa { get; set; }

        // Lấy danh sách nhà cung cấp
        public static List<NhaCungCapModel> LayDanhSachNhaCC()
        {
            List<NhaCungCapModel> nhaCungCapList = new List<NhaCungCapModel>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string query = "SELECT * FROM NhaCungCap";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nhaCungCapList.Add(new NhaCungCapModel
                            {
                                MaNCC = reader["MaNCC"].ToString(),
                                TenNCC = reader["TenNCC"].ToString(),
                                DiaChi = reader["DiaChi"].ToString(),
                                SDT = reader["SDT"].ToString(),
                                MoTa = reader["MoTa"] != DBNull.Value ? reader["MoTa"].ToString() : null
                            });
                        }
                    }
                }
            }
            return nhaCungCapList;
        }

        // Thêm nhà cung cấp
        public static void ThemNhaCC(Hashtable parameter)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    // Kiểm tra mã nhà cung cấp đã tồn tại chưa
                    string query = "SELECT COUNT(*) FROM NhaCungCap WHERE MaNCC = @MaNCC";
                    using (SqlCommand checkCmd = new SqlCommand(query, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaNCC", parameter["MaNCC"]);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Mã nhà cung cấp đã tồn tại. Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Thêm nhà cung cấp mới
                    query = "INSERT INTO NhaCungCap (MaNCC, TenNCC, DiaChi, SDT, MoTa) VALUES (@MaNCC, @TenNCC, @DiaChi, @SDT, @MoTa)";
                    using (SqlCommand insertCmd = new SqlCommand(query, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@MaNCC", parameter["MaNCC"] ?? DBNull.Value);
                        insertCmd.Parameters.AddWithValue("@TenNCC", parameter["TenNCC"] ?? DBNull.Value);
                        insertCmd.Parameters.AddWithValue("@DiaChi", parameter["DiaChi"] ?? DBNull.Value);
                        insertCmd.Parameters.AddWithValue("@SDT", parameter["SDT"] ?? DBNull.Value);
                        insertCmd.Parameters.AddWithValue("@MoTa", parameter["MoTa"] ?? DBNull.Value);
                        insertCmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        // Xóa nhà cung cấp
        public static void XoaNhaCungCap(string ID)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    // Kiểm tra xem MaNCC có đang được dùng trong bảng HoaDonNhap không
                    string checkQuery = "SELECT COUNT(*) FROM HoaDonNhap WHERE MaNCC = @MaNCC";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@MaNCC", ID);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Không thể xóa nhà cung cấp vì đã được sử dụng trong hóa đơn nhập.",
                                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Nếu không có ràng buộc, tiến hành xóa
                    string deleteQuery = "DELETE FROM NhaCungCap WHERE MaNCC = @MaNCC";
                    using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@MaNCC", ID);
                        deleteCmd.ExecuteNonQuery();
                        MessageBox.Show("Xóa nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối đến cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }

        // Sửa nhà cung cấp
        public static void SuaNCC(Hashtable parameter)
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    // Kiểm tra các trường hợp cần thiết trước khi thực hiện cập nhật
                    if (parameter["MaNCC"] == null || parameter["MaNCC"].ToString().Trim().Length == 0)
                    {
                        MessageBox.Show("Mã nhà cung cấp không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (parameter["TenNCC"] == null || parameter["TenNCC"].ToString().Trim().Length == 0)
                    {
                        MessageBox.Show("Bạn phải nhập tên nhà cung cấp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (parameter["DiaChi"] == null || parameter["DiaChi"].ToString().Trim().Length == 0)
                    {
                        MessageBox.Show("Bạn phải nhập địa chỉ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (parameter["SDT"] == null || parameter["SDT"].ToString().Trim().Length == 0)
                    {
                        MessageBox.Show("Bạn phải nhập số điện thoại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Nếu các trường hợp đã kiểm tra hợp lệ, tiếp tục thực hiện cập nhật
                    string query = @"UPDATE NhaCungCap
                                    SET TenNCC = @TenNCC, DiaChi = @DiaChi,
                                        SDT = @SDT, MoTa = @MoTa
                                    WHERE MaNCC = @MaNCC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNCC", parameter["MaNCC"].ToString());
                        cmd.Parameters.AddWithValue("@TenNCC", parameter["TenNCC"]);
                        cmd.Parameters.AddWithValue("@DiaChi", parameter["DiaChi"]);
                        cmd.Parameters.AddWithValue("@SDT", parameter["SDT"]);
                        cmd.Parameters.AddWithValue("@MoTa", parameter["MoTa"] ?? DBNull.Value);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy nhà cung cấp để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi sửa nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
        }
    }
}