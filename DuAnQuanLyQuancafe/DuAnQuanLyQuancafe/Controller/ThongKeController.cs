using DuAnQuanLyQuancafe.Model;
using System;
using System.Collections.Generic;

namespace DuAnQuanLyQuancafe.Controller
{
    public class ThongKeController
    {
        private ThongKeModel model;

        public ThongKeController()
        {
            model = new ThongKeModel();
        }

        public bool LoadData(DateTime startDate, DateTime endDate)
        {
            return model.LoadData(startDate, endDate);
        }

        public int GetNumCustomers()
        {
            return model.NumCustomers;
        }

        public int GetNumSuppliers()
        {
            return model.NumSuppliers;
        }

        public int GetNumProducts()
        {
            return model.NumProducts;
        }

        public int GetNumOrders()
        {
            return model.NumOrders;
        }

        public decimal GetTotalRevenue()
        {
            return model.TotalRevenue;
        }

        public decimal GetTotalProfit()
        {
            return model.TotalProfit;
        }

        public decimal GetTotalCost()
        {
            return model.TotalCost;
        }

        public List<RevenueByDate> GetGrossRevenueList() // Fix: Use RevenueByDate directly
        {
            return model.GrossRevenueList;
        }

        public List<KeyValuePair<string, int>> GetTopProductList()
        {
            return model.TopProductList;
        }

        public List<KeyValuePair<string, int>> GetUnderstockList()
        {
            return model.UnderstockList;
        }
    }
}