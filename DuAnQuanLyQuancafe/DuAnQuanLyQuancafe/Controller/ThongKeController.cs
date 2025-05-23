using DuAnQuanLyQuancafe.Model;
using System;
using System.Collections.Generic;

namespace DuAnQuanLyQuancafe.Controller
{
    public class ThongKeController
    {
        private readonly ThongKeModel _thongKeModel;

        public ThongKeController()
        {
            _thongKeModel = new ThongKeModel();
        }

        /// <summary>
        /// Tải dữ liệu thống kê dựa trên khoảng thời gian
        /// </summary>
        /// <param name="startDate">Ngày bắt đầu</param>
        /// <param name="endDate">Ngày kết thúc</param>
        /// <returns>True nếu dữ liệu được tải mới, False nếu đã tải trước đó</returns>
        public bool LoadData(DateTime startDate, DateTime endDate)
        {
            return _thongKeModel.LoadData(startDate, endDate);
        }

        /// <summary>
        /// Lấy tổng số nhà cung cấp
        /// </summary>
        /// <returns>Số lượng nhà cung cấp</returns>
        public int GetNumSuppliers()
        {
            return _thongKeModel.NumSuppliers;
        }

        /// <summary>
        /// Lấy tổng số sản phẩm
        /// </summary>
        /// <returns>Số lượng sản phẩm</returns>
        public int GetNumProducts()
        {
            return _thongKeModel.NumProducts;
        }

        /// <summary>
        /// Lấy tổng số đơn hàng
        /// </summary>
        /// <returns>Số lượng đơn hàng</returns>
        public int GetNumOrders()
        {
            return _thongKeModel.NumOrders;
        }

        /// <summary>
        /// Lấy tổng doanh thu
        /// </summary>
        /// <returns>Tổng doanh thu</returns>
        public decimal GetTotalRevenue()
        {
            return _thongKeModel.TotalRevenue;
        }

        /// <summary>
        /// Lấy tổng lợi nhuận
        /// </summary>
        /// <returns>Tổng lợi nhuận</returns>
        public decimal GetTotalProfit()
        {
            return _thongKeModel.TotalProfit;
        }

        /// <summary>
        /// Lấy tổng chi phí
        /// </summary>
        /// <returns>Tổng chi phí</returns>
        public decimal GetTotalCost()
        {
            return _thongKeModel.TotalCost;
        }

        /// <summary>
        /// Lấy danh sách doanh thu theo ngày
        /// </summary>
        /// <returns>Danh sách doanh thu theo ngày</returns>
        public List<RevenueByDate> GetGrossRevenueList()
        {
            return _thongKeModel.GrossRevenueList;
        }

        /// <summary>
        /// Lấy danh sách 5 sản phẩm bán chạy nhất
        /// </summary>
        /// <returns>Danh sách sản phẩm bán chạy</returns>
        public List<KeyValuePair<string, int>> GetTopProductList()
        {
            return _thongKeModel.TopProductList;
        }

        /// <summary>
        /// Lấy danh sách sản phẩm tồn kho thấp
        /// </summary>
        /// <returns>Danh sách sản phẩm tồn kho dưới 10</returns>
        public List<KeyValuePair<string, int>> GetUnderstockList()
        {
            return _thongKeModel.UnderstockList;
        }
    }
}