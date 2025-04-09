using System;
using System.Data;
using System.Data.SqlClient;

namespace DuAnQuanLyQuancafe.function
{
    internal class DatabaseHelper
    {
        private static readonly string connectionString = "Data Source=DESKTOP-K56JJJ3;Initial Catalog=QuanLyQuanCafe2;Integrated Security=True;Encrypt=False";

        // Phương thức tạo và trả về một kết nối SQL đã mở
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
                connection.Close();   // Đóng kết nối
                connection.Dispose(); // Giải phóng tài nguyên
            }
        }
    }
}
