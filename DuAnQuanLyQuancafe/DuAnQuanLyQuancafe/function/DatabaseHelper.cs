using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DuAnQuanLyQuancafe.function
{
    internal class DatabaseHelper
    {
        private static readonly string connectionString = "Data Source=DESKTOP-K56JJJ3;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Encrypt=False";
        public static SqlConnection connection = new SqlConnection(connectionString);
        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open(); // Sử dụng Open() mà không cần kiểm tra trạng thái trước
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi kết nối đến cơ sở dữ liệu: " + ex.Message);
            }
            return connection;
        }

        // Phương thức đóng kết nối an toàn
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
            SqlDataAdapter da = new SqlDataAdapter(sql, connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public static bool CheckKey(string sql)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, connection);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else
                return false;
        }
        public static void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, connection); // Replaced 'Conn' with 'connection'  
            DataTable table = new DataTable();
            Mydata.Fill(table);
            cbo.DataSource = table;

            cbo.ValueMember = ma;    // Truong gia tri  
            cbo.DisplayMember = ten;    // Truong hien thi  
        }
        public static void RunSql(string sql)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connection.ConnectionString))
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
