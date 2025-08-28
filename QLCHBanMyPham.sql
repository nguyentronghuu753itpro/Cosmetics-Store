/* Quản Lý Cửa Hàng Bán Mỹ Phẩm */
/* Tạo cơ sở dữ liệu với Collation Vietnamese_CI_AS */
CREATE DATABASE QLCHBanMyPham COLLATE Vietnamese_CI_AS;

-- Sử dụng cơ sở dữ liệu
USE QLCHBanMyPham;

/* Tạo bảng USERS */
CREATE TABLE USERS (
    TenDangNhap NVARCHAR(20) NOT NULL,  -- Tên đăng nhập (khóa chính)
    MatKhau NVARCHAR(50) NOT NULL,      -- Mật khẩu
    CONSTRAINT PK_USERS PRIMARY KEY (TenDangNhap)
);



/* Tạo bảng NhaPhanPhoi */
CREATE TABLE NhaPhanPhoi (
    MaNPP CHAR(20) NOT NULL,            -- Mã nhà phân phối (khóa chính)
    TenNhaPhanPhoi NVARCHAR(100) NOT NULL, -- Tên nhà phân phối
    DienThoai NVARCHAR(20),             -- Số điện thoại
    DiaChi NVARCHAR(50),                -- Địa chỉ
    CONSTRAINT PK_NhaPhanPhoi PRIMARY KEY (MaNPP)
);

CREATE TABLE Kho (
    MaKho CHAR(20) NOT NULL,         -- Mã kho (khóa chính)
    TenKho NVARCHAR(100) NOT NULL,   -- Tên kho
    DiaChi NVARCHAR(200),            -- Địa chỉ kho
    CONSTRAINT PK_Kho PRIMARY KEY (MaKho)
);


/* Tạo bảng NhanVien */
CREATE TABLE NhanVien (
    MaNhanVien CHAR(20) NOT NULL,       -- Mã nhân viên (khóa chính)
    MaKho CHAR(20) NOT NULL,            -- Mã kho (khóa ngoại)
    TenNhanVien NVARCHAR(50) NOT NULL,  -- Tên nhân viên
    GioiTinh NVARCHAR(50),              -- Giới tính
    DiaChi NVARCHAR(255),               -- Địa chỉ
    DienThoai NVARCHAR(20),             -- Số điện thoại
    Email NVARCHAR(200) NOT NULL,       -- Email
    NgaySinh DATE,                      -- Ngày sinh
    CONSTRAINT PK_NhanVien PRIMARY KEY (MaNhanVien),
    CONSTRAINT FK_NhanVien_Kho FOREIGN KEY (MaKho) REFERENCES Kho (MaKho) -- Khóa ngoại với bảng Kho
);


/* Tạo bảng DanhMucSP */
CREATE TABLE DanhMucSP (
    MaDanhMuc CHAR(20) NOT NULL,        -- Mã danh mục (khóa chính)
    TenDanhMuc NVARCHAR(50) NOT NULL,   -- Tên danh mục
    CONSTRAINT PK_DanhMucSP PRIMARY KEY (MaDanhMuc)
);

/* Tạo bảng PhieuNhap */
CREATE TABLE PhieuNhap (
    MaPhieuNhap CHAR(20) NOT NULL,      -- Mã phiếu nhập (khóa chính)
    MaNhanVien CHAR(20) NOT NULL,       -- Mã nhân viên lập phiếu
    MaNPP CHAR(20) NOT NULL,            -- Mã nhà phân phối
    NgayNhap DATE,                      -- Ngày nhập hàng
    TongTien DECIMAL(10, 2) NOT NULL,   -- Tổng tiền
    CONSTRAINT PK_PhieuNhap PRIMARY KEY (MaPhieuNhap),
    CONSTRAINT FK_PhieuNhap_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES NhanVien (MaNhanVien),
    CONSTRAINT FK_PhieuNhap_NhaPhanPhoi FOREIGN KEY (MaNPP) REFERENCES NhaPhanPhoi (MaNPP)
);

/* Tạo bảng SanPham */
CREATE TABLE SanPham (
    MaSanPham CHAR(20) NOT NULL,        -- Mã sản phẩm (khóa chính)
    TenSanPham NVARCHAR(100) NOT NULL,  -- Tên sản phẩm
    MaDanhMuc CHAR(20) NOT NULL,        -- Mã danh mục
    MaNPP CHAR(20) NOT NULL,            -- Mã nhà phân phối
	MaKho CHAR(20) NOT NULL,            -- Mã kho 
    DonGiaNhap DECIMAL(10, 2) NOT NULL, -- Đơn giá nhập
    DonGiaBan DECIMAL(10, 2) NOT NULL,  -- Đơn giá bán
    SoLuong NVARCHAR(50) NOT NULL,      -- Số lượng
    GhiChu NVARCHAR(200),               -- Ghi chú
    NgaySanXuat DATE,                   -- Ngày sản xuất
    HanSuDungDate DATE,                  -- Hạn sử dụng (dạng ngày để tính toán)
    HanSuDungText NVARCHAR(50),          -- Hạn sử dụng (mô tả dạng chữ)
    CONSTRAINT PK_SanPham PRIMARY KEY (MaSanPham),
    CONSTRAINT FK_SanPham_DanhMucSP FOREIGN KEY (MaDanhMuc) REFERENCES DanhMucSP (MaDanhMuc),
    CONSTRAINT FK_SanPham_NhaPhanPhoi FOREIGN KEY (MaNPP) REFERENCES NhaPhanPhoi (MaNPP),
	CONSTRAINT FK_SanPham_Kho FOREIGN KEY (MaKho) REFERENCES Kho (MaKho) -- Khóa ngoại cho Kho
);

/* Tạo bảng ChiTietNhapHang */
CREATE TABLE ChiTietNhapHang (
    MaPhieuNhap CHAR(20) NOT NULL,      -- Mã phiếu nhập (khóa chính)
    MaSanPham CHAR(20) NOT NULL,        -- Mã sản phẩm (khóa chính)
    SoLuong NVARCHAR(50),               -- Số lượng
    DonGiaNhap DECIMAL(10, 2) NOT NULL, -- Đơn giá nhập
    ThanhTien DECIMAL(10, 2) NOT NULL, -- Thành tiền (tự động tính)
    CONSTRAINT PK_ChiTietNhapHang PRIMARY KEY (MaPhieuNhap, MaSanPham),
    CONSTRAINT FK_ChiTietNhapHang_PhieuNhap FOREIGN KEY (MaPhieuNhap) REFERENCES PhieuNhap (MaPhieuNhap),
    CONSTRAINT FK_ChiTietNhapHang_SanPham FOREIGN KEY (MaSanPham) REFERENCES SanPham (MaSanPham)
);

/* Tạo bảng KhachHang */
CREATE TABLE KhachHang (
    MaKhachHang CHAR(20) NOT NULL,      -- Mã khách hàng (khóa chính)
    TenKhachHang NVARCHAR(40) NOT NULL, -- Tên khách hàng
    DiaChi NVARCHAR(255),               -- Địa chỉ
    DienThoai NVARCHAR(20),             -- Số điện thoại
    CONSTRAINT PK_KhachHang PRIMARY KEY (MaKhachHang)
);

/* Tạo bảng HoaDonBan */
CREATE TABLE HoaDonBan (
    MaHoaDon CHAR(50) NOT NULL,         -- Mã hóa đơn (khóa chính)
    MaKhachHang CHAR(20) NOT NULL,      -- Mã khách hàng
    MaNhanVien CHAR(20) NOT NULL,       -- Mã nhân viên
    NgayHoaDon DATE,                    -- Ngày hóa đơn
    TongTien DECIMAL(10, 2) NOT NULL,   -- Tổng tiền
    CONSTRAINT PK_HoaDonBan PRIMARY KEY (MaHoaDon),
    CONSTRAINT FK_HoaDonBan_KhachHang FOREIGN KEY (MaKhachHang) REFERENCES KhachHang (MaKhachHang),
    CONSTRAINT FK_HoaDonBan_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES NhanVien (MaNhanVien)
);

/* Tạo bảng ChiTietBanHang */
CREATE TABLE ChiTietBanHang (
    MaHoaDon CHAR(50) NOT NULL,         -- Mã hóa đơn (khóa chính)
    MaSanPham CHAR(20) NOT NULL,        -- Mã sản phẩm (khóa chính)
    SoLuong NVARCHAR(50),               -- Số lượng
    DonGiaBan DECIMAL(10, 2) NOT NULL,  -- Đơn giá bán
    ThanhTien DECIMAL(10, 2) NOT NULL, -- Thành tiền (tự động tính)
    CONSTRAINT PK_ChiTietBanHang PRIMARY KEY (MaHoaDon, MaSanPham),
    CONSTRAINT FK_ChiTietBanHang_HoaDon FOREIGN KEY (MaHoaDon) REFERENCES HoaDonBan (MaHoaDon),
    CONSTRAINT FK_ChiTietBanHang_SanPham FOREIGN KEY (MaSanPham) REFERENCES SanPham (MaSanPham)
);



/* Chèn dữ liệu */

-- Chèn dữ liệu vào bảng DanhMucSP
INSERT INTO DanhMucSP (MaDanhMuc, TenDanhMuc) VALUES
('DM006', N'Mỹ phẩm chăm sóc da'),
('DM007', N'Mỹ phẩm trang điểm'),
('DM008', N'Mỹ phẩm môi'),
('DM009', N'Mỹ phẩm mắt'),
('DM010', N'Mỹ phẩm chống nắng'),
('DM011', N'Mỹ phẩm mascara'),
('DM012', N'Mỹ phẩm toner'),
('DM013', N'Mỹ phẩm tẩy trang'),
('DM014', N'Mỹ phẩm trị mụn'),
('DM015', N'Mỹ phẩm dưỡng da'),
('DM016', N'Mỹ phẩm chăm sóc tóc'),
('DM017', N'Mỹ phẩm dầu gội'),
('DM018', N'Mỹ phẩm sữa tắm'),
('DM019', N'Tẩy tế bào chết'),
('DM020', N'Mỹ phẩm dưỡng thể');

-- Chèn dữ liệu vào bảng KhachHang
INSERT INTO KhachHang (MaKhachHang, TenKhachHang, DiaChi, DienThoai) VALUES
('KH006', N'Nguyễn Văn A', N'Quận 1, TP.HCM', '0901234561'),
('KH007', N'Lê Thị B', N'Quận 2, TP.HCM', '0911234562'),
('KH008', N'Trần Văn C', N'Quận 3, TP.HCM', '0921234563'),
('KH009', N'Phạm Thị D', N'Quận 4, TP.HCM', '0931234564'),
('KH010', N'Hồ Văn E', N'Quận 5, TP.HCM', '0941234565'),
('KH011', N'Võ Thị F', N'Quận 6, TP.HCM', '0951234566'),
('KH012', N'Thái Văn G', N'Quận 7, TP.HCM', '0961234567'),
('KH013', N'Đỗ Thị H', N'Quận 8, TP.HCM', '0971234568'),
('KH014', N'Hoàng Văn I', N'Quận 9, TP.HCM', '0981234569'),
('KH015', N'Vũ Thị K', N'Quận 10, TP.HCM', '0991234570'),
('KH016', N'Nguyễn Văn L', N'Quận 11, TP.HCM', '0901234571'),
('KH017', N'Lê Thị M', N'Quận 12, TP.HCM', '0911234572'),
('KH018', N'Trần Văn N', N'Quận Tân Bình, TP.HCM', '0921234573'),
('KH019', N'Phạm Thị O', N'Quận Bình Thạnh, TP.HCM', '0931234574'),
('KH020', N'Hồ Văn P', N'Quận Gò Vấp, TP.HCM', '0941234575');

-- Chèn dữ liệu vào bảng NhaPhanPhoi
INSERT INTO NhaPhanPhoi (MaNPP, TenNhaPhanPhoi, DienThoai, DiaChi) VALUES
('NPP006', N'Nhà phân phối Hà Nội', '0982345678', N'Hà Nội'),
('NPP007', N'Nhà phân phối Nha Trang', '0992345678', N'Nha Trang'),
('NPP008', N'Nhà phân phối Huế', '0902345678', N'Huế'),
('NPP009', N'Nhà phân phối Vũng Tàu', '0912345678', N'Vũng Tàu'),
('NPP010', N'Nhà phân phối Bình Dương', '0922345678', N'Bình Dương'),
('NPP011', N'Nhà phân phối Đồng Nai', '0932345678', N'Đồng Nai'),
('NPP012', N'Nhà phân phối Long An', '0942345678', N'Long An'),
('NPP013', N'Nhà phân phối Bạc Liêu', '0952345678', N'Bạc Liêu'),
('NPP014', N'Nhà phân phối Vĩnh Long', '0963456789', N'Vĩnh Long'),
('NPP015', N'Nhà phân phối Tiền Giang', '0973456789', N'Tiền Giang'),
('NPP016', N'Nhà phân phối Quảng Nam', '0983456789', N'Quảng Nam'),
('NPP017', N'Nhà phân phối Khánh Hòa', '0993456789', N'Khánh Hòa'),
('NPP018', N'Nhà phân phối Phú Yên', '0904567890', N'Phú Yên'),
('NPP019', N'Nhà phân phối Bắc Giang', '0914567890', N'Bắc Giang'),
('NPP020', N'Nhà phân phối Thừa Thiên Huế', '0924567890', N'Thừa Thiên Huế');

-- Chèn dữ liệu vào bảng Kho
INSERT INTO Kho (MaKho, TenKho, DiaChi) VALUES
('KHO001', N'Kho Hà Nộ', N'Số 1, Đống Đa, Hà Nội'),
('KHO002', N'Kho Đà Nẵng', N'Số 2, Hải Châu, Đà Nẵng'),
('KHO003', N'Kho TP.HCM', N'Số 3, Quận 1, TP.HCM'),
('KHO006', N'Kho Hà Nội', N'Quận Đống Đa, Hà Nội'),
('KHO007', N'Kho Nha Trang', N'Quận Phước Long, Nha Trang'),
('KHO008', N'Kho Huế', N'Quận Phú Nhuận, Huế'),
('KHO009', N'Kho Vũng Tàu', N'Quận Thắng Nhất, Vũng Tàu'),
('KHO010', N'Kho Bình Dương', N'Quận Dĩ An, Bình Dương'),
('KHO011', N'Kho Đồng Nai', N'Quận Long Thành, Đồng Nai'),
('KHO012', N'Kho Long An', N'Quận Tân An, Long An'),
('KHO013', N'Kho Bạc Liêu', N'Quận Nhà Mát, Bạc Liêu'),
('KHO014', N'Kho Vĩnh Long', N'Quận Long Hồ, Vĩnh Long'),
('KHO015', N'Kho Tiền Giang', N'Quận Cai Lậy, Tiền Giang'),
('KHO016', N'Kho Quảng Nam', N'Quận Núi Thành, Quảng Nam'),
('KHO017', N'Kho Khánh Hòa', N'Quận Diên Khánh, Khánh Hòa'),
('KHO018', N'Kho Phú Yên', N'Quận Tuy Hòa, Phú Yên'),
('KHO019', N'Kho Bắc Giang', N'Quận Việt Yên, Bắc Giang');



-- Chèn dữ liệu vào bảng NhanVien
INSERT INTO NhanVien (MaNhanVien, MaKho, TenNhanVien, GioiTinh, DiaChi, DienThoai, Email, NgaySinh) VALUES
('NV006', 'KHO006', N'Trần Văn An', N'Nam', N'TP.HCM', '0961234561', 'antran@gmail.com', '1993-06-15'),
('NV007', 'KHO007', N'Lê Thị Hạnh', N'Nữ', N'Đà Nẵng', '0971234562', 'lehanh@gmail.com', '1994-07-16'),
('NV008', 'KHO008', N'Nguyễn Thái Bình', N'Nam', N'Hà Nội', '0981234563', 'nguyenbinh@gmail.com', '1995-08-17'),
('NV009', 'KHO009', N'Phạm Văn Cường', N'Nam', N'Nha Trang', '0991234564', 'phamcuong@gmail.com', '1996-09-18'),
('NV010', 'KHO010', N'Vũ Thị Thắm', N'Nữ', N'Huế', '0901234565', 'vutham@gmail.com', '1997-10-19'),
('NV011', 'KHO011', N'Đặng Văn Hùng', N'Nam', N'Đà Lạt', '0911234566', 'danghung@gmail.com', '1998-11-20'),
('NV012', 'KHO012', N'Lý Thị Bích', N'Nữ', N'Hải Phòng', '0921234567', 'lybich@gmail.com', '1999-12-21'),
('NV013', 'KHO013', N'Trương Văn Dũng', N'Nam', N'Cần Thơ', '0931234568', 'truongdung@gmail.com', '2000-01-22'),
('NV014', 'KHO014', N'Trần Văn Tiến', N'Nam', N'Biên Hòa', '0941234569', 'trantien@gmail.com', '1992-02-23'),
('NV015', 'KHO015', N'Nguyễn Văn Toản', N'Nam', N'Long An', '0951234560', 'toannguyen@gmail.com', '1991-03-24'),
('NV016', 'KHO016', N'Hoàng Văn Vũ', N'Nam', N'Vũng Tàu', '0962345671', 'hoangvu@gmail.com', '1990-04-25'),
('NV017', 'KHO017', N'Lâm Thị Nga', N'Nữ', N'Bình Dương', '0972345672', 'lamnga@gmail.com', '1993-05-26'),
('NV018', 'KHO018', N'Phan Thị Bích', N'Nữ', N'Bắc Ninh', '0982345673', 'phanbich@gmail.com', '1994-06-27'),
('NV019', 'KHO019', N'Trần Minh Quang', N'Nam', N'Hưng Yên', '0992345674', 'tranquang@gmail.com', '1995-07-28'),
('NV020', 'KHO007', N'Trần Thanh Huyền', N'Nữ', N'Nam Định', '0902345675', 'thanhhuyen@gmail.com', '1996-08-29');


-- Chèn dữ liệu vào bảng Phiếu Nhập
INSERT INTO PhieuNhap (MaPhieuNhap, MaNhanVien, MaNPP, NgayNhap, TongTien) VALUES
('PN006', 'NV008', 'NPP006', '2024-10-08', 850000.00),
('PN007', 'NV009', 'NPP007', '2024-10-09', 950000.00),
('PN008', 'NV010', 'NPP008', '2024-10-10', 700000.00),
('PN009', 'NV011', 'NPP009', '2024-10-11', 750000.00),
('PN010', 'NV012', 'NPP010', '2024-10-12', 800000.00),
('PN011', 'NV013', 'NPP011', '2024-10-13', 600000.00),
('PN012', 'NV014', 'NPP012', '2024-10-14', 700000.00),
('PN013', 'NV015', 'NPP013', '2024-10-15', 900000.00),
('PN014', 'NV016', 'NPP014', '2024-10-16', 950000.00),
('PN015', 'NV017', 'NPP015', '2024-10-17', 850000.00),
('PN016', 'NV018', 'NPP016', '2024-10-18', 780000.00),
('PN017', 'NV019', 'NPP017', '2024-10-19', 880000.00),
('PN018', 'NV020', 'NPP018', '2024-10-20', 920000.00),
('PN019', 'NV006', 'NPP019', '2024-10-21', 990000.00),
('PN020', 'NV007', 'NPP020', '2024-10-22', 610000.00);


-- Chèn dữ liệu vào bảng SanPham
INSERT INTO SanPham (MaSanPham, TenSanPham, MaDanhMuc, MaNPP, MaKho, DonGiaNhap, DonGiaBan, SoLuong, GhiChu, NgaySanXuat, HanSuDungDate, HanSuDungText) VALUES
('SP006', N'Kem nền Maybelline', 'DM006', 'NPP006', 'KHO001', 150000, 200000, 30, N'Kem nền mịn da', '2024-06-01', '2025-06-01', N'1 năm'),
('SP007', N'Phấn phủ Innisfree', 'DM007', 'NPP007', 'KHO002', 130000, 170000, 35, N'Phấn phủ kiềm dầu', '2024-07-01', '2025-07-01', N'1 năm'),
('SP008', N'Son môi 3CE', 'DM008', 'NPP008', 'KHO003', 200000, 250000, 20, N'Son lì lâu trôi', '2024-08-01', '2025-08-01', N'1 năm'),
('SP009', N'Phấn mắt Huda Beauty', 'DM009', 'NPP009', 'KHO001', 600000, 750000, 25, N'Bảng màu mắt đa sắc', '2024-09-01', '2025-09-01', N'1 năm'),
('SP010', N'Kẻ mắt nước NYX', 'DM010', 'NPP010', 'KHO002', 100000, 150000, 60, N'Kẻ mắt nước đen tuyền', '2024-10-01', '2025-10-01', N'1 năm'),
('SP011', N'Mascara Maybelline', 'DM011', 'NPP011', 'KHO003', 120000, 170000, 15, N'Mascara làm dày mi', '2024-11-01', '2025-11-01', N'1 năm'),
('SP012', N'Nước hoa hồng Evoluderm', 'DM012', 'NPP012', 'KHO001', 80000, 120000, 45, N'Dành cho da nhạy cảm', '2024-12-01', '2025-12-01', N'1 năm'),
('SP013', N'Dầu tẩy trang DHC', 'DM013', 'NPP013', 'KHO002', 270000, 320000, 55, N'Tẩy trang cho da dầu', '2024-01-15', '2025-01-15', N'1 năm'),
('SP014', N'Kem trị mụn Decumar', 'DM014', 'NPP014', 'KHO003', 90000, 130000, 40, N'Trị mụn hiệu quả', '2024-02-15', '2025-02-15', N'1 năm'),
('SP015', N'Xịt khoáng Avene', 'DM015', 'NPP015', 'KHO001', 150000, 200000, 25, N'Xịt khoáng làm dịu da', '2024-03-15', '2025-03-15', N'1 năm'),
('SP016', N'Dưỡng tóc TRESemmé', 'DM016', 'NPP016', 'KHO002', 180000, 250000, 35, N'Dưỡng ẩm cho tóc khô', '2024-04-15', '2025-04-15', N'1 năm'),
('SP017', N'Dầu gội Pantene', 'DM017', 'NPP017', 'KHO003', 80000, 110000, 28, N'Dầu gội chống rụng tóc', '2024-05-15', '2025-05-15', N'1 năm'),
('SP018', N'Sữa tắm Dove', 'DM018', 'NPP018', 'KHO001', 90000, 120000, 50, N'Sữa tắm dưỡng ẩm', '2024-06-15', '2025-06-15', N'1 năm'),
('SP019', N'Tẩy tế bào chết St.Ives', 'DM019', 'NPP019', 'KHO002', 120000, 160000, 22, N'Loại bỏ tế bào chết', '2024-07-15', '2025-07-15', N'1 năm'),
('SP020', N'Kem dưỡng thể Vaseline', 'DM020', 'NPP020', 'KHO003', 100000, 150000, 20, N'Kem dưỡng trắng da', '2024-08-15', '2025-08-15', N'1 năm');




-- Chèn dữ liệu vào bảng Hóa đơn
INSERT INTO HoaDonBan (MaHoaDon, MaKhachHang, MaNhanVien, NgayHoaDon, TongTien) VALUES
('HD006', 'KH006', 'NV006', '2024-10-17', 1300000.00),
('HD007', 'KH007', 'NV007', '2024-10-18', 1200000.00),
('HD008', 'KH008', 'NV008', '2024-10-19', 1100000.00),
('HD009', 'KH009', 'NV009', '2024-10-20', 1250000.00),
('HD010', 'KH010', 'NV010', '2024-10-21', 1350000.00),
('HD011', 'KH011', 'NV011', '2024-10-22', 1450000.00),
('HD012', 'KH012', 'NV012', '2024-10-23', 1550000.00),
('HD013', 'KH013', 'NV013', '2024-10-24', 1600000.00),
('HD014', 'KH014', 'NV014', '2024-10-25', 1700000.00),
('HD015', 'KH015', 'NV015', '2024-10-26', 1800000.00),
('HD016', 'KH016', 'NV016', '2024-10-27', 1900000.00),
('HD017', 'KH017', 'NV017', '2024-10-28', 2000000.00),
('HD018', 'KH018', 'NV018', '2024-10-29', 2100000.00),
('HD019', 'KH019', 'NV019', '2024-10-30', 2200000.00),
('HD020', 'KH020', 'NV020', '2024-10-31', 2300000.00);



-- Chèn dữ liệu vào bảng Chi tiết nhập hàng
INSERT INTO ChiTietNhapHang (MaPhieuNhap, MaSanPham, SoLuong, DonGiaNhap, ThanhTien) VALUES
('PN006', 'SP006', 25, 110000, 25 * 110000),
('PN007', 'SP007', 30, 75000, 30 * 75000),
('PN008', 'SP008', 20, 100000, 20 * 100000),
('PN009', 'SP009', 22, 115000, 22 * 115000),
('PN010', 'SP010', 28, 105000, 28 * 105000),
('PN011', 'SP011', 15, 950000, 15 * 950000),
('PN012', 'SP012', 40, 600000, 40 * 600000),
('PN013', 'SP013', 35, 1300000, 35 * 1300000),
('PN014', 'SP014', 25, 900000, 25 * 900000),
('PN015', 'SP015', 20, 800000, 20 * 800000),
('PN016', 'SP016', 18, 500000, 18 * 500000),
('PN017', 'SP017', 30, 950000, 30 * 950000),
('PN018', 'SP018', 45, 400000, 45 * 400000),
('PN019', 'SP019', 25, 1000000, 25 * 1000000),
('PN020', 'SP020', 10, 1200000, 10 * 1200000);



-- Chèn dữ liệu vào bảng Chi tiết bán hàng
INSERT INTO ChiTietBanHang (MaHoaDon, MaSanPham, SoLuong, DonGiaBan, ThanhTien) VALUES
('HD006', 'SP006', 3, 1400000, 3 * 1400000),
('HD007', 'SP007', 4, 950000, 4 * 950000),
('HD008', 'SP008', 2, 1250000, 2 * 1250000),
('HD009', 'SP009', 3, 1450000, 3 * 1450000),
('HD010', 'SP010', 5, 1300000, 5 * 1300000),
('HD011', 'SP011', 2, 1200000, 2 * 1200000),
('HD012', 'SP012', 6, 850000, 6 * 850000),
('HD013', 'SP013', 4, 1600000, 4 * 1600000),
('HD014', 'SP014', 5, 1200000, 5 * 1200000),
('HD015', 'SP015', 3, 1000000, 3 * 1000000),
('HD016', 'SP016', 4, 700000, 4 * 700000),
('HD017', 'SP017', 2, 1200000, 2 * 1200000),
('HD018', 'SP018', 5, 600000, 5 * 600000),
('HD019', 'SP019', 3, 1300000, 3 * 1300000),
('HD020', 'SP020', 1, 1500000, 1 * 1500000);



