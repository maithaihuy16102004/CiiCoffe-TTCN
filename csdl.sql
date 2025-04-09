create database QuanLyQuanCafe2
drop database QuanLyQuanCafe2


 -- Bảng Quê quán
CREATE TABLE Que (
    MaQue INT PRIMARY KEY IDENTITY(1,1),
    TenQue NVARCHAR(100) NOT NULL
);

-- Bảng Công dụng
CREATE TABLE CongDung (
    MaCongDung INT PRIMARY KEY IDENTITY(1,1),
    TenCongDung NVARCHAR(100) NOT NULL
);

-- Bảng Loại sản phẩm
CREATE TABLE Loai (
    MaLoai INT PRIMARY KEY IDENTITY(1,1),
    TenLoai NVARCHAR(100) NOT NULL
);

-- Bảng Nhân viên
CREATE TABLE NhanVien (
    MaNV NVARCHAR(10) PRIMARY KEY,
    TenNV NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255),
    GioiTinh NVARCHAR(10),
    NgaySinh DATE,
    SDT NVARCHAR(15),
    MaQue INT,
    HinhAnh VARBINARY(MAX),
    FOREIGN KEY (MaQue) REFERENCES Que(MaQue)
);

-- Bảng Khách hàng
CREATE TABLE KhachHang (
    MaKH NVARCHAR(10) PRIMARY KEY,
    TenKH NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255),
    SDT NVARCHAR(15)
);

-- Bảng Nhà cung cấp
CREATE TABLE NhaCungCap (
    MaNCC NVARCHAR(10) PRIMARY KEY,
    TenNCC NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255),
    SDT NVARCHAR(15),
    MoTa NVARCHAR(100)
);

-- Bảng Sản phẩm
CREATE TABLE SanPham (
    MaSP NVARCHAR(10) PRIMARY KEY,
    TenSP NVARCHAR(100) NOT NULL,
    MaLoai INT,
    GiaNhap DECIMAL(18,2),
    GiaBan DECIMAL(18,2),
    SoLuong INT,
    MaCongDung INT,
    HinhAnh VARBINARY(MAX),
    FOREIGN KEY (MaLoai) REFERENCES Loai(MaLoai),
    FOREIGN KEY (MaCongDung) REFERENCES CongDung(MaCongDung)
);

-- Bảng Hóa đơn bán hàng
CREATE TABLE HoaDonBan (
    MaHDB NVARCHAR(10) PRIMARY KEY,
    NgayBan DATE,
    MaNV NVARCHAR(10),
    MaKH NVARCHAR(10),
    TongTien DECIMAL(18,2),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
);

-- Bảng Chi tiết hóa đơn bán
CREATE TABLE ChiTietHDB (
    MaCTHDB INT PRIMARY KEY IDENTITY(1,1),
    MaHDB NVARCHAR(10),
    MaSP NVARCHAR(10),
    SoLuong INT,
    ThanhTien DECIMAL(18,2),
    KhuyenMai NVARCHAR(50),
    FOREIGN KEY (MaHDB) REFERENCES HoaDonBan(MaHDB),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);

-- Bảng Hóa đơn nhập hàng
CREATE TABLE HoaDonNhap (
    MaHDN NVARCHAR(10) PRIMARY KEY,
    NgayNhap DATE,
    MaNV NVARCHAR(10),
    MaNCC NVARCHAR(10),
    TongTien DECIMAL(18,2),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC)
);

-- Bảng Chi tiết hóa đơn nhập
CREATE TABLE ChiTietHDN (
    MaCTHDN INT PRIMARY KEY IDENTITY(1,1),
    MaHDN NVARCHAR(10),
    MaSP NVARCHAR(10),
    SoLuong INT,
    DonGia DECIMAL(18,2),
    ThanhTien DECIMAL(18,2),
    KhuyenMai NVARCHAR(50),
    FOREIGN KEY (MaHDN) REFERENCES HoaDonNhap(MaHDN),
    FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);

-- Bảng Tài khoản
CREATE TABLE TaiKhoan (
    MaTK INT PRIMARY KEY IDENTITY(1,1),
    TenDangNhap NVARCHAR(50) UNIQUE NOT NULL,
    MatKhau NVARCHAR(255) NOT NULL,
    LoaiTaiKhoan NVARCHAR(20) CHECK (LoaiTaiKhoan IN ('Admin', 'NhanVien')),
    MaNV NVARCHAR(10),
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV) ON DELETE SET NULL
);

INSERT INTO Que (TenQue) VALUES
(N'An Giang'),
(N'Bà Rịa - Vũng Tàu'),
(N'Bạc Liêu'),
(N'Bắc Kạn'),
(N'Bắc Giang'),
(N'Bắc Ninh'),
(N'Bến Tre'),
(N'Bình Dương'),
(N'Bình Định'),
(N'Bình Phước'),
(N'Bình Thuận'),
(N'Cà Mau'),
(N'Cao Bằng'),
(N'Cần Thơ'),
(N'Đà Nẵng'),
(N'Đắk Lắk'),
(N'Đắk Nông'),
(N'Điện Biên'),
(N'Đồng Nai'),
(N'Đồng Tháp'),
(N'Gia Lai'),
(N'Hà Giang'),
(N'Hà Nam'),
(N'Hà Nội'),
(N'Hà Tĩnh'),
(N'Hải Dương'),
(N'Hải Phòng'),
(N'Hậu Giang'),
(N'Hòa Bình'),
(N'Hưng Yên'),
(N'Khánh Hòa'),
(N'Kiên Giang'),
(N'Kon Tum'),
(N'Lai Châu'),
(N'Lạng Sơn'),
(N'Lào Cai'),
(N'Lâm Đồng'),
(N'Long An'),
(N'Nam Định'),
(N'Nghệ An'),
(N'Ninh Bình'),
(N'Ninh Thuận'),
(N'Phú Thọ'),
(N'Phú Yên'),
(N'Quảng Bình'),
(N'Quảng Nam'),
(N'Quảng Ngãi'),
(N'Quảng Ninh'),
(N'Quảng Trị'),
(N'Sóc Trăng'),
(N'Sơn La'),
(N'Tây Ninh'),
(N'Thái Bình'),
(N'Thái Nguyên'),
(N'Thanh Hóa'),
(N'Thừa Thiên Huế'),
(N'Tiền Giang'),
(N'TP Hồ Chí Minh'),
(N'Trà Vinh'),
(N'Tuyên Quang'),
(N'Vĩnh Long'),
(N'Vĩnh Phúc'),
(N'Yên Bái');


--Cong Dung
INSERT INTO CongDung (TenCongDung) VALUES
(N'Giải khát'),
(N'Tăng tỉnh táo'),
(N'Tốt cho sức khỏe');

--Loại
INSERT INTO Loai (TenLoai) VALUES
(N'Cà phê'),
(N'Trà'),
(N'Nước ép'),
(N'Sinh tố');


--Nhan Vien
INSERT INTO NhanVien (MaNV, TenNV, DiaChi, GioiTinh, NgaySinh, SDT, MaQue, HinhAnh) VALUES
('CI001', N'Nguyễn Văn A', N'123 Đống Đa, Hà Nội', N'Nam', '1990-05-10', '0123456789', 24, NULL),
('CI002', N'Trần Thị B', N'456 Quận 1, TP.HCM', N'Nữ', '1995-07-15', '0987654321', 58, NULL);

--Khach Hang
INSERT INTO KhachHang (MaKH, TenKH, DiaChi, SDT) VALUES
('KH001', N'Lê Văn C', N'789 Cầu Giấy, Hà Nội', '0912345678'),
('KH002', N'Phạm Thị D', N'456 Quận 3, TP.HCM', '0934567890');


--Nha Cung CXap
INSERT INTO NhaCungCap (MaNCC, TenNCC, DiaChi, SDT, MoTa) VALUES
('NCC001', N'Công ty Cà phê Trung Nguyên', N'Buôn Ma Thuột, Đắk Lắk', '0901234567', N'Chuyên cung cấp cà phê'),
('NCC002', N'Công ty Trà Tân Cương', N'Thái Nguyên', '0912345678', N'Chuyên cung cấp trà');

----San Pham
INSERT INTO SanPham (MaSP, TenSP, MaLoai, GiaNhap, GiaBan, SoLuong, MaCongDung, HinhAnh) VALUES
('SP001', N'Cà phê đen', 1, 15000, 25000, 100, 2, NULL),
('SP002', N'Trà sữa trân châu', 2, 10000, 20000, 80, 1, NULL);

--Hoa Don Ban
INSERT INTO HoaDonBan (MaHDB, NgayBan, MaNV, MaKH, TongTien) VALUES
('HDB001', '2025-04-09', 'CI001', 'KH001', 50000),
('HDB002', '2025-04-09', 'CI002', 'KH002', 40000);

--Chi tiet hoa don ban 
INSERT INTO ChiTietHDB (MaHDB, MaSP, SoLuong, ThanhTien, KhuyenMai) VALUES
('HDB001', 'SP001', 2, 50000, N'Giảm 10%'),
('HDB002', 'SP002', 2, 40000, N'Không');

--Hoa Don Bán

--Hóa đơn nhập
INSERT INTO HoaDonNhap (MaHDN, NgayNhap, MaNV, MaNCC, TongTien) VALUES
('HDN001', '2025-04-08', 'CI001', 'NCC001', 150000),
('HDN002', '2025-04-08', 'CI002', 'NCC002', 100000);

--Chi tiêt hóa đo nanhapj
INSERT INTO ChiTietHDN (MaHDN, MaSP, SoLuong, DonGia, ThanhTien, KhuyenMai) VALUES
('HDN001', 'SP001', 10, 15000, 150000, N'Không'),
('HDN002', 'SP002', 10, 10000, 100000, N'Không');

--Tai Khoan 
INSERT INTO TaiKhoan (TenDangNhap, MatKhau, LoaiTaiKhoan, MaNV) VALUES
('admin', '1', 'Admin', 'CI001'),
('nv2', '2', 'NhanVien', 'CI002');


EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;

EXEC sp_configure 'Ad Hoc Distributed Queries', 1;
RECONFIGURE;

INSERT INTO NhanVien (MaNV, TenNV, DiaChi, GioiTinh, NgaySinh, SDT, MaQue, HinhAnh)
SELECT 
    'CI003',  -- Cần thêm dòng này!
    N'Nguyễn Văn A', 
    N'123 Đường ABC, TP.HCM', 
    N'Nam', 
    '1990-01-01', 
    '0912345678', 
    1,  
    BulkColumn
FROM OPENROWSET(BULK 'D:\Study\C#\Cuoiki\Picture\avt.png', SINGLE_BLOB) AS AnhFile;

--✅ 1. Tạo hàm SQL để sinh mã tự động
CREATE FUNCTION dbo.GenerateMaNhanVien()
RETURNS NVARCHAR(10)
AS
BEGIN
    DECLARE @NewMa NVARCHAR(10)
    DECLARE @MaxMa INT

    SELECT @MaxMa = MAX(CAST(SUBSTRING(MaNV, 3, LEN(MaNV)) AS INT)) FROM NhanVien

    IF @MaxMa IS NULL
        SET @MaxMa = 0

    SET @MaxMa = @MaxMa + 1

    SET @NewMa = 'CI' + RIGHT('000' + CAST(@MaxMa AS NVARCHAR), 3)

    RETURN @NewMa
END

/*
🧠 Ý tưởng:

Cắt phần số trong mã hiện tại (bỏ "CI") → ép kiểu về số → lấy lớn nhất → +1 → ghép lại với "CI".
*/

--✅ 1. Mã hóa đơn bán – MaHDB
CREATE FUNCTION dbo.GenerateMaHDB()
RETURNS NVARCHAR(10)
AS
BEGIN
    DECLARE @NewMa NVARCHAR(10)
    DECLARE @MaxMa INT

    SELECT @MaxMa = MAX(CAST(SUBSTRING(MaHDB, 4, LEN(MaHDB)) AS INT)) FROM HoaDonBan

    IF @MaxMa IS NULL SET @MaxMa = 0

    SET @MaxMa = @MaxMa + 1

    SET @NewMa = 'HDB' + RIGHT('000' + CAST(@MaxMa AS NVARCHAR), 3)

    RETURN @NewMa
END

--✅ 2. Mã hóa đơn nhập – MaHDN
CREATE FUNCTION dbo.GenerateMaHDN()
RETURNS NVARCHAR(10)
AS
BEGIN
    DECLARE @NewMa NVARCHAR(10)
    DECLARE @MaxMa INT

    SELECT @MaxMa = MAX(CAST(SUBSTRING(MaHDN, 4, LEN(MaHDN)) AS INT)) FROM HoaDonNhap

    IF @MaxMa IS NULL SET @MaxMa = 0

    SET @MaxMa = @MaxMa + 1

    SET @NewMa = 'HDN' + RIGHT('000' + CAST(@MaxMa AS NVARCHAR), 3)

    RETURN @NewMa
END

--✅ 3. Mã chi tiết hóa đơn bán – MaCTHDB
CREATE FUNCTION dbo.GenerateMaCTHDB()
RETURNS NVARCHAR(10)
AS
BEGIN
    DECLARE @NewMa NVARCHAR(10)
    DECLARE @MaxMa INT

    SELECT @MaxMa = MAX(CAST(SUBSTRING(CAST(MaCTHDB AS NVARCHAR), 4, LEN(MaCTHDB)) AS INT)) 
    FROM ChiTietHDB

    IF @MaxMa IS NULL SET @MaxMa = 0

    SET @MaxMa = @MaxMa + 1

    SET @NewMa = 'CTB' + RIGHT('000' + CAST(@MaxMa AS NVARCHAR), 3)

    RETURN @NewMa
END

--4. Mã chi tiết hóa đơn nhập – MaCTHDN
CREATE FUNCTION dbo.GenerateMaCTHDN()
RETURNS NVARCHAR(10)
AS
BEGIN
    DECLARE @NewMa NVARCHAR(10)
    DECLARE @MaxMa INT

    SELECT @MaxMa = MAX(CAST(SUBSTRING(CAST(MaCTHDN AS NVARCHAR), 4, LEN(CAST(MaCTHDN AS NVARCHAR))) AS INT))
    FROM ChiTietHDN
    WHERE ISNUMERIC(SUBSTRING(CAST(MaCTHDN AS NVARCHAR), 4, LEN(CAST(MaCTHDN AS NVARCHAR)))) = 1

    IF @MaxMa IS NULL SET @MaxMa = 0

    SET @MaxMa = @MaxMa + 1

    SET @NewMa = 'CTN' + RIGHT('000' + CAST(@MaxMa AS NVARCHAR), 3)

    RETURN @NewMa
END

--cần tạo stored procedure trong SQL Server:
CREATE PROCEDURE CapNhatTongTienHDN
    @MaHDN NVARCHAR(10)
AS
BEGIN
    UPDATE HoaDonNhap
    SET TongTien = (
        SELECT SUM(ThanhTien)
        FROM ChiTietHDN
        WHERE MaHDN = @MaHDN
    )
    WHERE MaHDN = @MaHDN
END





