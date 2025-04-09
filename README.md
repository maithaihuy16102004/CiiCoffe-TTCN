✅ 1. Model – Lưu trữ dữ liệu
Model giống như cái kho chứa thông tin.

🔹 Nó đại diện cho một bảng trong cơ sở dữ liệu.
🔹 Mỗi model chứa các thuộc tính (tên, mã, số lượng, v.v...) giống hệt bảng ngoài SQL.


✅ 2. Controller – Xử lý logic
Controller là người điều phối, trung gian giữa View và Model.

🔹 Lấy dữ liệu từ database
🔹 Trả về cho View để hiển thị
🔹 Xử lý thêm, sửa, xóa...

✅ 3. View – Giao diện
View là nơi để người dùng thao tác – giao diện chính bạn nhìn thấy.

🔹 Hiển thị danh sách từ Controller
🔹 Gửi yêu cầu (thêm, sửa...) cho Controller xử lý
🔹 Nhận kết quả và hiển thị lại

🔁 Mối liên kết tổng quát
View gửi yêu cầu → Controller

Controller truy vấn dữ liệu → Model

Model chứa dữ liệu → trả về Controller

Controller đưa kết quả lên View

🎯 Áp dụng cho quán cafe:
Thành phần	Model	View	Controller
Nhân viên	NhanVienModel	FrmNhanVien	NhanVienController
Sản phẩm	SanPhamModel	FrmSanPham	SanPhamController
HĐ Nhập	HoaDonNhapModel + ChiTietHDNModel	FrmHDN	HoaDonNhapController
HĐ Bán	HoaDonBanModel + ChiTietHDBModel	FrmBanHang	HoaDonBanController
