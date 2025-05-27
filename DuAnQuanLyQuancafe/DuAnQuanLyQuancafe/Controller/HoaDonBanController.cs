using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DuAnQuanLyQuancafe.Model;

namespace DuAnQuanLyQuancafe.Controller
{
    public class HoaDonBanController
    {
        private readonly HoaDonBanModel _model;

        public HoaDonBanController()
        {
            _model = new HoaDonBanModel();
        }

        /// <summary>
        /// Lấy danh sách toàn bộ hóa đơn bán.
        /// </summary>
        public List<HoaDonBanModel> LayDanhSachHDB()
        {
            return _model.LayDanhSachHDB();
        }

        /// <summary>
        /// Lưu thông tin hóa đơn bán. Nếu chưa có mã thì tự động tạo mới.
        /// </summary>
        public void LuuHoaDon(HoaDonBanModel hoaDon)
        {
            if (hoaDon == null)
                throw new ArgumentNullException(nameof(hoaDon), "Hóa đơn không được để null.");

            if (string.IsNullOrWhiteSpace(hoaDon.MaHDB))
                hoaDon.MaHDB = _model.LayMaHDBTuDatabase();

            _model.LuuHoaDon(hoaDon);
        }

        /// <summary>
        /// Lấy mã hóa đơn bán tự động từ cơ sở dữ liệu.
        /// </summary>
        public string LayMaHDBTuDatabase()
        {
            return _model.LayMaHDBTuDatabase();
        }

        /// <summary>
        /// Tìm kiếm hóa đơn theo mã nhân viên.
        /// </summary>
        public List<HoaDonBanModel> TimKiemHDBTheoMaNV(string maNV)
        {
            if (string.IsNullOrWhiteSpace(maNV))
                throw new ArgumentException("Mã nhân viên không được để trống.", nameof(maNV));

            return _model.TimKiemHDBTheoMaNV(maNV);
        }

        /// <summary>
        /// Lọc danh sách hóa đơn theo khoảng ngày bán.
        /// </summary>
        public List<HoaDonBanModel> LocHDBTheoNgay(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            if (ngayBatDau > ngayKetThuc)
                    MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày kết thúc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return _model.LocHDBTheoNgay(ngayBatDau, ngayKetThuc);
        }

        /// <summary>
        /// Lấy mã hóa đơn mới nhất theo mã nhân viên và ngày bán.
        /// </summary>
        public string LayMaHDBMoiNhat(string maNV, DateTime ngayBan)
        {
            if (string.IsNullOrWhiteSpace(maNV))
                throw new ArgumentException("Mã nhân viên không được để trống.", nameof(maNV));

            return _model.LayMaHDBMoiNhat(maNV, ngayBan);
        }
    }
}
