1. Mục tiêu và Phạm vi
1.1 Mục tiêu
Dự án nhằm xây dựng một phần mềm quản lý quán cà phê giúp tự động hóa và tối ưu hóa các hoạt động hằng ngày như: quản lý nhân viên, người dùng, thực đơn đồ uống, đơn hàng và báo cáo doanh thu. Phần mềm hỗ trợ cả nhân viên lẫn quản lý dễ dàng thao tác, đồng thời phân quyền rõ ràng để đảm bảo an toàn và hiệu quả trong quản lý.

1.2 Phạm vi
Phần mềm bao gồm các chức năng chính sau:

Quản lý tài khoản người dùng với phân quyền (Admin, Nhân viên).

Quản lý nhân viên: thêm, sửa, xóa thông tin.

Quản lý đồ uống: cập nhật thực đơn, theo dõi tình trạng và nguyên liệu.

Quản lý đơn hàng: tạo hóa đơn, thanh toán.

Thống kê, báo cáo doanh thu theo ngày, tháng, năm.

2. Kiến trúc tổng quan
2.1 Kiến trúc hệ thống
Phần mềm được thiết kế theo mô hình MVC (Model - View - Controller) để dễ bảo trì, mở rộng và quản lý code:

Model: Đại diện cho dữ liệu và nghiệp vụ như Nhân viên, Đồ uống, Hóa đơn.

View: Giao diện người dùng (thiết kế bằng WinForms).

Controller: Xử lý các tương tác từ người dùng và điều hướng dữ liệu qua lại giữa View và Model.

2.2 Phân chia hệ thống
Phần mềm được chia thành các mô-đun chính:

Đăng nhập & Phân quyền: Kiểm soát truy cập hệ thống.

Quản lý nhân viên: Thêm, sửa, xóa thông tin nhân viên.

Quản lý đồ uống (thực đơn): Cập nhật, theo dõi danh sách đồ uống.

Quản lý đơn hàng: Tạo đơn, thanh toán, xuất hóa đơn.

Báo cáo doanh thu: Thống kê doanh thu chi tiết.

3. Mô-đun và Cấu trúc
3.1 Mô tả mô-đun
Đăng nhập: Hỗ trợ đăng nhập bằng tài khoản hoặc nhận diện khuôn mặt.

Quản lý nhân viên: Hiển thị và cập nhật thông tin nhân sự.

Quản lý đồ uống: Quản lý thực đơn, giá cả và trạng thái đồ uống.

Quản lý đơn hàng: Tạo đơn hàng, tính tiền, xuất hóa đơn.

Báo cáo doanh thu: Thống kê doanh thu theo thời gian.

3.2 Cấu trúc nội bộ mô-đun
Mỗi mô-đun có giao diện riêng và lớp xử lý nghiệp vụ.

Giao tiếp trực tiếp với cơ sở dữ liệu thông qua các lớp trung gian (Data Access Layer).

4. Công nghệ và hạ tầng
4.1 Công nghệ sử dụng
Ngôn ngữ: C# (.NET Framework 4.8)

Giao diện: WinForms

Cơ sở dữ liệu: SQL Server

4.2 Hạ tầng triển khai
Cơ sở dữ liệu lưu trữ trên máy chủ nội bộ hoặc máy tính quản lý.

Ứng dụng cài đặt trực tiếp trên các máy tính của nhân viên/quản lý.

5. Mô hình dữ liệu
5.1 Cấu trúc các bảng
TaiKhoan: Chứa thông tin tài khoản và quyền người dùng.

NhanVien: Lưu thông tin nhân viên (tên, chức vụ, mã nhân viên...).

DoUong: Danh sách đồ uống, giá, trạng thái.

HoaDon: Thông tin hóa đơn gồm đồ uống, số lượng, tổng tiền, người lập hóa đơn.

5.2 Quan hệ giữa các bảng
Tài khoản liên kết với nhân viên qua trường MaNV.

Hóa đơn liên kết với đồ uống thông qua chi tiết đơn hàng.

6. Thiết kế giao diện
6.1 Giao diện người dùng
FrmLogin: Đăng nhập.

FrmQuanLyNhanVien: Quản lý nhân sự.

FrmQuanLyDoUong: Quản lý thực đơn.

FrmQuanLyDonHang: Tạo đơn hàng.

FrmBaoCaoDoanhThu: Xem báo cáo doanh thu.

6.2 Tương tác người dùng
Giao diện thân thiện, dễ thao tác, phù hợp với cả nhân viên lẫn quản lý.

7. Quản lý dữ liệu
Dữ liệu được lưu trong SQL Server.

Các thao tác như thêm, sửa, xóa thực hiện thông qua giao diện và cập nhật lên cơ sở dữ liệu thông qua các lớp xử lý nghiệp vụ.

8. Bảo mật
8.1 Bảo vệ thông tin người dùng
Mật khẩu được mã hóa trước khi lưu.

Hỗ trợ đăng nhập bằng khuôn mặt để tăng tính an toàn.

Phân quyền rõ ràng giữa người dùng và quản trị viên.

9. Hiệu suất và tối ưu hóa
9.1 Đảm bảo hiệu suất
Tối ưu truy vấn SQL.

Giao diện phản hồi nhanh.

9.2 Cách tối ưu
Dùng chỉ mục (index) trong các bảng dữ liệu lớn.

Giảm tải hệ thống bằng cách lọc dữ liệu trước khi hiển thị.

10. Quản lý phiên bản và triển khai
10.1 Quản lý phiên bản
Sử dụng Git để quản lý source code.

Ghi chú rõ ràng từng lần cập nhật, giúp dễ dàng bảo trì hoặc quay lại phiên bản ổn định.

10.2 Triển khai
Cài đặt ứng dụng trực tiếp trên máy tính nội bộ.

Kết nối đến cơ sở dữ liệu SQL Server cài sẵn.

11. Hướng dẫn cài đặt và sử dụng
11.1 Cài đặt
Cài SQL Server và khởi tạo cơ sở dữ liệu.

Cài đặt phần mềm WinForms trên máy tính.

Kết nối phần mềm với cơ sở dữ liệu.

11.2 Sử dụng
Đăng nhập bằng tài khoản hoặc khuôn mặt.

Quản lý thực đơn, tạo đơn hàng, theo dõi doanh thu.

Dễ dàng thao tác với giao diện thân thiện.

12. Tài liệu tham khảo
Microsoft .NET Documentation

SQL Server Documentation

Các bài viết và ví dụ từ cộng đồng C#, Stack Overflow.
