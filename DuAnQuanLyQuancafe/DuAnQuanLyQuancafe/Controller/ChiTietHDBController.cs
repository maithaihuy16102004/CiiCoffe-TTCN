using System;
using System.Collections.Generic;
using DuAnQuanLyQuancafe.Model;

namespace DuAnQuanLyQuancafe.Controller
{
    public class ChiTietHDBController
    {
        // Gọi phương thức từ Model để lưu danh sách ChiTietHDB
        public void LuuChiTiet(List<ChiTietHDBModel> chiTietHDBs)
        {
            ChiTietHDBModel.LuuChiTiet(chiTietHDBs);
        }

        // Gọi phương thức từ Model để lấy MaCTHDB tự sinh
        public string LayMaCTHDBTuDatabase()
        {
            return ChiTietHDBModel.LayMaCTHDBTuDatabase();
        }

        // Gọi phương thức từ Model để lấy danh sách ChiTietHDB theo MaHDB
        public List<ChiTietHDBModel> LayChiTietHDBTheoMaHDB(string maHDB)
        {
            return ChiTietHDBModel.LayChiTietHDBTheoMaHDB(maHDB);
        }
    }
}