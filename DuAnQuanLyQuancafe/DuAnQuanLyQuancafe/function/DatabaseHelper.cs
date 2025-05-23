using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.function
{
    internal class DatabaseHelper
    {
        private static readonly string connectionString = "Data Source=DESKTOP-LBR1P0N\\KHANH;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Encrypt=False";
        // Xóa biến tĩnh connection để tránh xung đột
        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Kết nối thành công đến: " + connection.Database);
                return connection;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi khi kết nối đến cơ sở dữ liệu: " + ex.Message);
                throw;
            }
        }

        public static void CloseConnection(SqlConnection connection)
        {
            if (connection != null && connection.State != ConnectionState.Closed)
            {
                connection.Close();
                connection.Dispose();
            }
        }
        public static bool CheckKey(string sql, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            return table.Rows.Count > 0 && Convert.ToInt32(table.Rows[0][0]) > 0;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi khi kiểm tra khóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlDataAdapter Mydata = new SqlDataAdapter(sql, conn);
                DataTable table = new DataTable();
                Mydata.Fill(table);
                cbo.DataSource = table;
                cbo.ValueMember = ma;
                cbo.DisplayMember = ten;
            }
        }
        public static void fillcombophanquyen(string sql, ComboBox cbo, string loaitk)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlDataAdapter Mydata = new SqlDataAdapter(sql, conn);
                DataTable table = new DataTable();
                Mydata.Fill(table);
                cbo.DataSource = table;
                cbo.ValueMember = loaitk;
            }
        }
        public static void RunSql(string sql, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi khi thực thi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

}
