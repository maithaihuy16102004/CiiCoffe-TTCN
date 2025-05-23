using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DuAnQuanLyQuancafe.function;

namespace DuAnQuanLyQuancafe.Model
{
    public struct RevenueByDate
    {
        public string Date { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class ThongKeModel
    {
        private DateTime startDate;
        private DateTime endDate;
        public int numberDays;

        public int NumSuppliers { get; private set; }
        public int NumProducts { get; private set; }
        public List<KeyValuePair<string, int>> TopProductList { get; private set; }
        public List<KeyValuePair<string, int>> UnderstockList { get; private set; }
        public List<RevenueByDate> GrossRevenueList { get; private set; }
        public int NumOrders { get; private set; }
        public decimal TotalRevenue { get; private set; }
        public decimal TotalProfit { get; private set; }
        public decimal TotalCost { get; private set; }

        public ThongKeModel()
        {
            TopProductList = new List<KeyValuePair<string, int>>();
            UnderstockList = new List<KeyValuePair<string, int>>();
            GrossRevenueList = new List<RevenueByDate>();
        }

        // Private methods
        private void GetNumberItems()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    // Get total number of suppliers
                    string query = "SELECT COUNT(*) FROM NhaCungCap";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        NumSuppliers = (int)cmd.ExecuteScalar();
                    }

                    // Get total number of products
                    query = "SELECT COUNT(*) FROM SanPham";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        NumProducts = (int)cmd.ExecuteScalar();
                    }

                    // Get total number of orders
                    query = "SELECT COUNT(*) FROM HoaDonBan " +
                            "WHERE NgayBan BETWEEN @fromDate AND @toDate";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@fromDate", startDate);
                        cmd.Parameters.AddWithValue("@toDate", endDate);
                        NumOrders = (int)cmd.ExecuteScalar();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi khi truy vấn cơ sở dữ liệu: {ex.Message}");
                }
                finally
                {
                    //DatabaseHelper.CloseConnection(conn);
                }
            }
        }

        private void GetOrderAnalysis()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                GrossRevenueList = new List<RevenueByDate>();
                TotalProfit = 0;
                TotalRevenue = 0;
                TotalCost = 0;
                try
                {
                    // Calculate total cost from HoaDonNhap
                    string query = "SELECT TongTien FROM HoaDonNhap " +
                                   "WHERE NgayNhap BETWEEN @fromDate AND @toDate";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@fromDate", startDate);
                        cmd.Parameters.AddWithValue("@toDate", endDate);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TotalCost += (decimal)reader[0];
                            }
                        }
                    }

                    // Calculate revenue and group by date
                    query = "SELECT NgayBan, SUM(TongTien) FROM HoaDonBan " +
                            "WHERE NgayBan BETWEEN @fromDate AND @toDate " +
                            "GROUP BY NgayBan";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@fromDate", startDate);
                        cmd.Parameters.AddWithValue("@toDate", endDate);
                        using (var reader = cmd.ExecuteReader())
                        {
                            var resultTable = new List<KeyValuePair<DateTime, decimal>>();
                            while (reader.Read())
                            {
                                resultTable.Add(new KeyValuePair<DateTime, decimal>((DateTime)reader[0], (decimal)reader[1]));
                                TotalRevenue += (decimal)reader[1];
                            }
                            TotalProfit = TotalRevenue - TotalCost;
                            reader.Close();

                            // Group by days
                            if (numberDays <= 30)
                            {
                                foreach (var item in resultTable)
                                {
                                    GrossRevenueList.Add(new RevenueByDate
                                    {
                                        Date = item.Key.ToString("dd/MM/yyyy"),
                                        TotalAmount = item.Value
                                    });
                                }
                            }
                            // Group by weeks
                            else if (numberDays <= 92)
                            {
                                GrossRevenueList = (from orderList in resultTable
                                                    group orderList by CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(orderList.Key, CalendarWeekRule.FirstDay, DayOfWeek.Monday)
                                                    into order
                                                    select new RevenueByDate
                                                    {
                                                        Date = "Tuần " + order.Key.ToString(),
                                                        TotalAmount = order.Sum(amount => amount.Value)
                                                    }).ToList();
                            }
                            // Group by months
                            else if (numberDays <= 365 * 2)
                            {
                                bool isYear = numberDays <= 365;
                                GrossRevenueList = (from orderList in resultTable
                                                    group orderList by orderList.Key.ToString("MM/yyyy")
                                                    into order
                                                    select new RevenueByDate
                                                    {
                                                        Date = isYear && order.Key.Length >= 4 ? order.Key.Substring(3) : order.Key,
                                                        TotalAmount = order.Sum(amount => amount.Value)
                                                    }).ToList();
                            }
                            // Group by years
                            else if (numberDays > 365 * 2)
                            {
                                GrossRevenueList = (from orderList in resultTable
                                                    group orderList by orderList.Key.ToString("yyyy")
                                                    into order
                                                    select new RevenueByDate
                                                    {
                                                        Date = order.Key,
                                                        TotalAmount = order.Sum(amount => amount.Value)
                                                    }).ToList();
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi khi truy vấn cơ sở dữ liệu: {ex.Message}");
                }
                finally
                {
                    //DatabaseHelper.CloseConnection(conn);
                }
            }
        }

        private void GetProductAnalysis()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                TopProductList = new List<KeyValuePair<string, int>>();
                UnderstockList = new List<KeyValuePair<string, int>>();
                try
                {
                    // Get top 5 products
                    string query = "SELECT TOP 5 s.TenSP, SUM(ct.SoLuong) FROM ChiTietHDB ct " +
                                   "JOIN SanPham s ON s.MaSP = ct.MaSP " +
                                   "JOIN HoaDonBan h ON ct.MaHDB = h.MaHDB " +
                                   "WHERE NgayBan BETWEEN @fromDate AND @toDate " +
                                   "GROUP BY s.TenSP ORDER BY SUM(ct.SoLuong) DESC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@fromDate", startDate);
                        cmd.Parameters.AddWithValue("@toDate", endDate);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0) && !reader.IsDBNull(1))
                                {
                                    TopProductList.Add(new KeyValuePair<string, int>(reader[0].ToString(), Convert.ToInt32(reader[1])));
                                }
                            }
                        }
                    }

                    // Get understock products
                    query = "SELECT TOP 5 TenSP, SoLuong FROM SanPham WHERE SoLuong < 10 ORDER BY SoLuong ASC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0) && !reader.IsDBNull(1))
                                {
                                    UnderstockList.Add(new KeyValuePair<string, int>(reader[0].ToString(), Convert.ToInt32(reader[1])));
                                }
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi khi truy vấn cơ sở dữ liệu: {ex.Message}");
                }
                finally
                {
                    //DatabaseHelper.CloseConnection(conn);
                }
            }
        }

        // Public methods
        public bool LoadData(DateTime startDate, DateTime endDate)
        {
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endDate.Hour, endDate.Minute, 59);
            if (startDate != this.startDate || endDate != this.endDate)
            {
                this.startDate = startDate;
                this.endDate = endDate;
                this.numberDays = (endDate - startDate).Days + 1;
                GetNumberItems();
                GetOrderAnalysis();
                GetProductAnalysis();
                return true;
            }
            else
            {
                MessageBox.Show("Dữ liệu đã được tải trước đó.");
                return false;
            }
        }
    }
}


//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Drawing;
//using System.Globalization;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using DuAnQuanLyQuancafe.function;

//namespace DuAnQuanLyQuancafe.Model
//{
//    public struct RevenueByDate
//    {
//        public string Date { get; set; }
//        public decimal TotalAmount { get; set; }
//    }

//    public class ThongKeModel
//    {
//        private DateTime startDate;
//        private DateTime endDate;
//        public int numberDays;

//        public int NumCustomers { get; private set; }
//        public int NumSuppliers { get; private set; }
//        public int NumProducts { get; private set; }
//        public List<KeyValuePair<string, int>> TopProductList { get; private set; }
//        public List<KeyValuePair<string, int>> UnderstockList { get; private set; }
//        public List<RevenueByDate> GrossRevenueList { get; private set; }
//        public int NumOrders { get; private set; }
//        public decimal TotalRevenue { get; private set; }
//        public decimal TotalProfit { get; private set; }
//        public decimal TotalCost { get; private set; }

//        public ThongKeModel()
//        {
//        }

//        private void GetNumberItems()
//        {
//            using (SqlConnection conn = DatabaseHelper.GetConnection())
//            {
//                try
//                {
//                    string query = "SELECT COUNT(*) FROM KhachHang";
//                    using (SqlCommand cmd = new SqlCommand(query, conn))
//                    {
//                        NumCustomers = (int)cmd.ExecuteScalar();
//                    }
//                    query = "SELECT COUNT(*) FROM NhaCungCap";
//                    using (SqlCommand cmd = new SqlCommand(query, conn))
//                    {
//                        NumSuppliers = (int)cmd.ExecuteScalar();
//                    }
//                    query = "SELECT COUNT(*) FROM SanPham";
//                    using (SqlCommand cmd = new SqlCommand(query, conn))
//                    {
//                        NumProducts = (int)cmd.ExecuteScalar();
//                    }
//                    query = "SELECT COUNT(*) FROM HoaDonBan " +
//                            "WHERE NgayBan BETWEEN @fromDate and @toDate";
//                    using (SqlCommand cmd = new SqlCommand(query, conn))
//                    {
//                        cmd.Parameters.AddWithValue("@fromDate", startDate);
//                        cmd.Parameters.AddWithValue("@toDate", endDate);
//                        NumOrders = (int)cmd.ExecuteScalar();
//                    }
//                }
//                catch (SqlException ex)
//                {
//                    MessageBox.Show($"Lỗi khi truy vấn cơ sở dữ liệu: {ex.Message}");
//                }
//                finally
//                {
//                    DatabaseHelper.CloseConnection(conn);
//                }
//            }
//        }

//        private void GetOrderAnalisys()
//        {
//            using (SqlConnection conn = DatabaseHelper.GetConnection())
//            {
//                GrossRevenueList = new List<RevenueByDate>();
//                TotalProfit = 0;
//                TotalRevenue = 0;
//                try
//                {
//                    string query = "SELECT TongTien FROM HoaDonNHap " +
//                                   "WHERE NgayNhap BETWEEN @fromDate and @toDate";
//                    using (SqlCommand cmd = new SqlCommand(query, conn))
//                    {
//                        cmd.Parameters.AddWithValue("@fromDate", startDate);
//                        cmd.Parameters.AddWithValue("@toDate", endDate);
//                        using (var reader = cmd.ExecuteReader())
//                        {
//                            while (reader.Read())
//                            {
//                                TotalCost += (decimal)reader[0];
//                            }
//                        }
//                    }
//                    query = "SELECT NgayBan, SUM(TongTien) FROM HoaDonBan " +
//                            "WHERE NgayBan BETWEEN @fromDate and @toDate " +
//                            "GROUP BY NgayBan";
//                    using (SqlCommand cmd = new SqlCommand(query, conn))
//                    {
//                        cmd.Parameters.AddWithValue("@fromDate", startDate);
//                        cmd.Parameters.AddWithValue("@toDate", endDate);
//                        using (var reader = cmd.ExecuteReader())
//                        {
//                            var resultTable = new List<KeyValuePair<DateTime, decimal>>();
//                            while (reader.Read())
//                            {
//                                resultTable.Add(new KeyValuePair<DateTime, decimal>((DateTime)reader[0], (decimal)reader[1]));
//                                TotalRevenue += (decimal)reader[1];
//                            }
//                            TotalProfit = TotalRevenue - TotalCost;
//                            if (numberDays <= 30)
//                            {
//                                foreach (var item in resultTable)
//                                {
//                                    GrossRevenueList.Add(new RevenueByDate
//                                    {
//                                        Date = item.Key.ToString("dd/MM/yyyy"),
//                                        TotalAmount = item.Value
//                                    });
//                                }
//                            }
//                            else if (numberDays <= 92)
//                            {
//                                GrossRevenueList = (from orderList in resultTable
//                                                    group orderList by CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(orderList.Key, CalendarWeekRule.FirstDay, DayOfWeek.Monday)
//                                                    into order
//                                                    select new RevenueByDate
//                                                    {
//                                                        Date = "Tuần " + order.Key.ToString(),
//                                                        TotalAmount = order.Sum(amount => amount.Value)
//                                                    }).ToList();
//                            }
//                            else if (numberDays <= 365 * 2)
//                            {
//                                bool isYear = numberDays <= 365;
//                                GrossRevenueList = (from orderList in resultTable
//                                                    group orderList by orderList.Key.ToString("MM/yyyy")
//                                                    into order
//                                                    select new RevenueByDate
//                                                    {
//                                                        Date = isYear && order.Key.Length >= 4 ? order.Key.Substring(3) : order.Key,
//                                                        TotalAmount = order.Sum(amount => amount.Value)
//                                                    }).ToList();
//                            }
//                            else if (numberDays > 365 * 2)
//                            {
//                                GrossRevenueList = (from orderList in resultTable
//                                                    group orderList by orderList.Key.ToString("yyyy")
//                                                    into order
//                                                    select new RevenueByDate
//                                                    {
//                                                        Date = order.Key,
//                                                        TotalAmount = order.Sum(amount => amount.Value)
//                                                    }).ToList();
//                            }
//                        }
//                    }
//                }
//                catch (SqlException ex)
//                {
//                    MessageBox.Show($"Lỗi khi truy vấn cơ sở dữ liệu: {ex.Message}");
//                }
//                finally
//                {
//                    DatabaseHelper.CloseConnection(conn);
//                }
//            }
//        }

//        private void GetProductAnalisys()
//        {
//            using (SqlConnection conn = DatabaseHelper.GetConnection())
//            {
//                TopProductList = new List<KeyValuePair<string, int>>();
//                UnderstockList = new List<KeyValuePair<string, int>>();
//                try
//                {
//                    string query = "SELECT TOP 5 s.TenSP, SUM(ct.SoLuong) FROM ChiTietHDB ct JOIN SanPham s ON s.MaSP = ct.MaSP JOIN HoaDonBan h ON ct.MaHDB = h.MaHDB WHERE NgayBan BETWEEN @fromDate AND @toDate GROUP BY TenSP ORDER BY SUM(ct.SoLuong) DESC";
//                    using (SqlCommand cmd = new SqlCommand(query, conn))
//                    {
//                        cmd.Parameters.AddWithValue("@fromDate", startDate);
//                        cmd.Parameters.AddWithValue("@toDate", endDate);
//                        using (var reader = cmd.ExecuteReader())
//                        {
//                            while (reader.Read())
//                            {
//                                if (!reader.IsDBNull(1) && reader[1] != DBNull.Value)
//                                {
//                                    TopProductList.Add(new KeyValuePair<string, int>(reader[0].ToString(), (int)(decimal)reader[1]));
//                                    Console.WriteLine($"Top Product - TenSP: {reader[0]}, SoLuong: {reader[1]}");
//                                }
//                            }
//                        }
//                    }
//                    query = "SELECT TOP 5 TenSP, SoLuong FROM SanPham WHERE SoLuong < 10 ORDER BY SoLuong ASC";
//                    using (SqlCommand cmd = new SqlCommand(query, conn))
//                    {
//                        using (var reader = cmd.ExecuteReader())
//                        {
//                            while (reader.Read())
//                            {
//                                if (!reader.IsDBNull(1) && reader[1] != DBNull.Value)
//                                {
//                                    UnderstockList.Add(new KeyValuePair<string, int>(reader[0].ToString(), (int)(decimal)reader[1]));
//                                    Console.WriteLine($"Understock - TenSP: {reader[0]}, SoLuong: {reader[1]}");
//                                }
//                            }
//                        }
//                    }
//                }
//                catch (SqlException ex)
//                {
//                    MessageBox.Show($"Lỗi khi truy vấn cơ sở dữ liệu: {ex.Message}");
//                }
//                finally
//                {
//                    DatabaseHelper.CloseConnection(conn);
//                }
//            }
//        }

//        public bool LoadData(DateTime startDate, DateTime endDate)
//        {
//            if (startDate > endDate)
//            {
//                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.");
//                return false;
//            }
//            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endDate.Hour, endDate.Minute, 59);
//            if (startDate != this.startDate || endDate != this.endDate)
//            {
//                this.startDate = startDate;
//                this.endDate = endDate;
//                this.numberDays = (endDate - startDate).Days + 1;
//                GetNumberItems();
//                GetOrderAnalisys();
//                GetProductAnalisys();
//                return true;
//            }
//            else
//            {
//                MessageBox.Show("Dữ liệu đã được tải trước đó.");
//                return false;
//            }
//        }
//    }
//}