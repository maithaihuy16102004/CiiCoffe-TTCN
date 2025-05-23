using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.function
{
    internal class DatabaseHelper
    {
        private static readonly string connectionString = "Data Source=DESKTOP-K56JJJ3;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Encrypt=False";
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

        public static DataTable GetDataToTable(string sql)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static bool CheckKey(string sql)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlDataAdapter Mydata = new SqlDataAdapter(sql, conn);
                DataTable table = new DataTable();
                Mydata.Fill(table);
                return table.Rows.Count > 0;
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

        public static void RunSql(string sql)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}