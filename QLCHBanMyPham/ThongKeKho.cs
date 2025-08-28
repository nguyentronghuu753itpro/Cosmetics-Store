using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using QLCHBanMyPham.ChucNang;

namespace QLCHBanMyPham
{
    public partial class ThongKeKho : Form
    {
        public ThongKeKho()
        {
            InitializeComponent();
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime endDate = DateTime.Now;
                LoadThongKeBan(startDate, endDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải biểu đồ: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadThongKeBan(DateTime startDate, DateTime endDate)
        {
            try
            {
                string sql = $@"
                    SELECT HoaDonBan.NgayHoaDon, 
                           SUM(CAST(ChiTietBanHang.ThanhTien AS DECIMAL(10,2))) AS TongDoanhThu
                    FROM HoaDonBan
                    JOIN ChiTietBanHang ON HoaDonBan.MaHoaDon = ChiTietBanHang.MaHoaDon
                    WHERE HoaDonBan.NgayHoaDon BETWEEN '{startDate:yyyy-MM-dd}' AND '{endDate:yyyy-MM-dd}'
                    GROUP BY HoaDonBan.NgayHoaDon
                    ORDER BY HoaDonBan.NgayHoaDon ASC";

                DataTable dtThongKe = Functions.GetDataToTable(sql);

                if (dtThongKe.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                chartThongKe.Series.Clear();

                Series series = new Series("Doanh thu bán hàng")
                {
                    ChartType = SeriesChartType.Line,
                    IsValueShownAsLabel = true,
                    BorderWidth = 3,
                    Color = Color.OrangeRed // Màu chính cho biểu đồ doanh thu bán
                };

                foreach (DataRow row in dtThongKe.Rows)
                {
                    DateTime ngayHoaDon = Convert.ToDateTime(row["NgayHoaDon"]);
                    decimal tongDoanhThu = Convert.ToDecimal(row["TongDoanhThu"]);
                    series.Points.AddXY(ngayHoaDon.ToString("dd/MM/yyyy"), tongDoanhThu);
                }

                chartThongKe.Series.Add(series);

                chartThongKe.ChartAreas[0].AxisX.Title = "Ngày hóa đơn";
                chartThongKe.ChartAreas[0].AxisY.Title = "Tổng doanh thu (VND)";
                chartThongKe.ChartAreas[0].AxisX.Interval = 1;
                chartThongKe.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

                chartThongKe.Titles.Clear();
                chartThongKe.Titles.Add($"Thống kê doanh thu từ {startDate:dd/MM/yyyy} đến {endDate:dd/MM/yyyy}");
                chartThongKe.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu biểu đồ: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadThongKeNhap(DateTime startDate, DateTime endDate)
        {
            try
            {
                string sql = $@"
                    SELECT PhieuNhap.NgayNhap, 
                           SUM(CAST(ChiTietNhapHang.ThanhTien AS DECIMAL(10,2))) AS TongChiPhiNhap
                    FROM PhieuNhap
                    JOIN ChiTietNhapHang ON PhieuNhap.MaPhieuNhap = ChiTietNhapHang.MaPhieuNhap
                    WHERE PhieuNhap.NgayNhap BETWEEN '{startDate:yyyy-MM-dd}' AND '{endDate:yyyy-MM-dd}'
                    GROUP BY PhieuNhap.NgayNhap
                    ORDER BY PhieuNhap.NgayNhap ASC";

                DataTable dtThongKe = Functions.GetDataToTable(sql);

                if (dtThongKe.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                chartThongKe.Series.Clear();

                Series series = new Series("Chi phí nhập hàng")
                {
                    ChartType = SeriesChartType.Column,
                    IsValueShownAsLabel = true,
                    Color = Color.CornflowerBlue // Màu chính cho biểu đồ nhập hàng
                };

                foreach (DataRow row in dtThongKe.Rows)
                {
                    DateTime ngayNhap = Convert.ToDateTime(row["NgayNhap"]);
                    decimal tongChiPhi = Convert.ToDecimal(row["TongChiPhiNhap"]);
                    series.Points.AddXY(ngayNhap.ToString("dd/MM/yyyy"), tongChiPhi);
                }

                chartThongKe.Series.Add(series);

                chartThongKe.ChartAreas[0].AxisX.Title = "Ngày nhập";
                chartThongKe.ChartAreas[0].AxisY.Title = "Tổng chi phí (VND)";
                chartThongKe.ChartAreas[0].AxisX.Interval = 1;
                chartThongKe.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

                chartThongKe.Titles.Clear();
                chartThongKe.Titles.Add($"Thống kê nhập hàng từ {startDate:dd/MM/yyyy} đến {endDate:dd/MM/yyyy}");
                chartThongKe.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu biểu đồ: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadThongKeSoLuong()
        {
            try
            {
                string sql = $@"
                    SELECT DanhMucSP.TenDanhMuc, 
                           SUM(CAST(SanPham.SoLuong AS INT)) AS TongSoLuong
                    FROM SanPham
                    JOIN DanhMucSP ON SanPham.MaDanhMuc = DanhMucSP.MaDanhMuc
                    GROUP BY DanhMucSP.TenDanhMuc
                    ORDER BY DanhMucSP.TenDanhMuc 
                    ASC";

                DataTable dtThongKe = Functions.GetDataToTable(sql);

                if (dtThongKe.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                chartThongKe.Series.Clear();

                Series series = new Series("Số lượng sản phẩm")
                {
                    ChartType = SeriesChartType.Pie,
                    IsValueShownAsLabel = true,
                    LabelForeColor = Color.Black, // Màu chữ trên biểu đồ tròn
                };

                foreach (DataRow row in dtThongKe.Rows)
                {
                    string tenDanhMuc = row["TenDanhMuc"].ToString();
                    int tongSoLuong = Convert.ToInt32(row["TongSoLuong"]);
                    series.Points.AddXY(tenDanhMuc, tongSoLuong);
                }

                chartThongKe.Series.Add(series);

                chartThongKe.Titles.Clear();
                chartThongKe.Titles.Add("Thống kê số lượng sản phẩm theo danh mục");
                chartThongKe.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu biểu đồ: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThongKeBan_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy ngày bắt đầu và kết thúc từ người dùng
                DateTime startDate = dtpNgayBatDau.Value;
                DateTime endDate = dtpNgayKetThuc.Value;

                // Kiểm tra nếu ngày bắt đầu lớn hơn ngày kết thúc
                if (startDate > endDate)
                {
                    MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gọi hàm tải biểu đồ thống kê bán
                LoadThongKeBan(startDate, endDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thực hiện thống kê bán: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThongKeNhap_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy ngày bắt đầu và kết thúc từ người dùng
                DateTime startDate = dtpNgayBatDau.Value;
                DateTime endDate = dtpNgayKetThuc.Value;

                // Kiểm tra nếu ngày bắt đầu lớn hơn ngày kết thúc
                if (startDate > endDate)
                {
                    MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gọi hàm tải biểu đồ thống kê nhập
                LoadThongKeNhap(startDate, endDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thực hiện thống kê nhập: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThongKeSoLuong_Click(object sender, EventArgs e)
        {
            try
            {
                // Gọi hàm tải biểu đồ thống kê số lượng
                LoadThongKeSoLuong();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thực hiện thống kê số lượng: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chartThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nếu biểu đồ có Series
                if (chartThongKe.Series.Count > 0)
                {
                    // Lấy tên Series đầu tiên
                    string seriesName = chartThongKe.Series[0].Name;

                    // Hiển thị thông báo
                    MessageBox.Show($"Bạn vừa nhấp vào biểu đồ: {seriesName}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Hiện tại không có dữ liệu để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xử lý sự kiện nhấp biểu đồ: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ThongKeKho_Load(object sender, EventArgs e)
        {

        }
        private void LoadThongKeNhanVien()
        {
            try
            {
                // Truy vấn dữ liệu thống kê nhân viên
                string sql = @"
            SELECT NhanVien.TenNhanVien, 
                   COUNT(HoaDonBan.MaHoaDon) AS SoHoaDon, 
                   SUM(HoaDonBan.TongTien) AS TongDoanhThu
            FROM NhanVien
            LEFT JOIN HoaDonBan ON NhanVien.MaNhanVien = HoaDonBan.MaNhanVien
            GROUP BY NhanVien.TenNhanVien
            ORDER BY TongDoanhThu DESC";

                // Lấy dữ liệu từ cơ sở dữ liệu
                DataTable dtThongKe = Functions.GetDataToTable(sql);

                // Kiểm tra nếu không có dữ liệu
                if (dtThongKe.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Xóa các Series cũ nếu có
                chartThongKe.Series.Clear();

                // Tạo Series mới
                Series seriesDoanhThu = new Series("Doanh thu")
                {
                    ChartType = SeriesChartType.Column,
                    IsValueShownAsLabel = true,
                    Color = Color.SeaGreen
                };

                Series seriesHoaDon = new Series("Số hóa đơn")
                {
                    ChartType = SeriesChartType.Line,
                    IsValueShownAsLabel = true,
                    Color = Color.Orange
                };

                // Thêm dữ liệu vào Series
                foreach (DataRow row in dtThongKe.Rows)
                {
                    string tenNhanVien = row["TenNhanVien"].ToString();
                    int soHoaDon = row["SoHoaDon"] == DBNull.Value ? 0 : Convert.ToInt32(row["SoHoaDon"]);
                    decimal tongDoanhThu = row["TongDoanhThu"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TongDoanhThu"]);

                    seriesDoanhThu.Points.AddXY(tenNhanVien, tongDoanhThu);
                    seriesHoaDon.Points.AddXY(tenNhanVien, soHoaDon);
                }


                // Thêm Series vào Chart
                chartThongKe.Series.Add(seriesDoanhThu);
                chartThongKe.Series.Add(seriesHoaDon);

                // Cấu hình trục X và Y
                chartThongKe.ChartAreas[0].AxisX.Title = "Nhân viên";
                chartThongKe.ChartAreas[0].AxisY.Title = "Giá trị (VND / Số lượng)";
                chartThongKe.ChartAreas[0].AxisX.Interval = 1;
                chartThongKe.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

                // Thêm tiêu đề biểu đồ
                chartThongKe.Titles.Clear();
                chartThongKe.Titles.Add("Thống kê nhân viên theo doanh thu và số hóa đơn");
                chartThongKe.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu biểu đồ: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                // Gọi hàm thống kê nhân viên
                LoadThongKeNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thực hiện thống kê nhân viên: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadThongKeNhaPhanPhoi()
        {
            try
            {
                // Truy vấn dữ liệu thống kê nhà phân phối
            string sql = @"
            SELECT NhaPhanPhoi.TenNhaPhanPhoi, 
            COUNT(DISTINCT PhieuNhap.MaPhieuNhap) AS SoPhieuNhap,
            SUM(CAST(ChiTietNhapHang.SoLuong AS INT)) AS TongSoLuong,
            SUM(CAST(ChiTietNhapHang.ThanhTien AS DECIMAL(10,2))) AS TongGiaTriNhap
            FROM NhaPhanPhoi
            LEFT JOIN PhieuNhap ON NhaPhanPhoi.MaNPP = PhieuNhap.MaNPP
            LEFT JOIN ChiTietNhapHang ON PhieuNhap.MaPhieuNhap = ChiTietNhapHang.MaPhieuNhap
            GROUP BY NhaPhanPhoi.TenNhaPhanPhoi
            ORDER BY TongGiaTriNhap DESC";

                // Lấy dữ liệu từ cơ sở dữ liệu
                DataTable dtThongKe = Functions.GetDataToTable(sql);

                // Kiểm tra nếu không có dữ liệu
                if (dtThongKe.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Xóa các Series cũ nếu có
                chartThongKe.Series.Clear();

                // Tạo Series mới
                Series seriesGiaTriNhap = new Series("Tổng giá trị nhập")
                {
                    ChartType = SeriesChartType.Column,
                    IsValueShownAsLabel = true,
                    Color = Color.RoyalBlue
                };

                Series seriesSoPhieuNhap = new Series("Số phiếu nhập")
                {
                    ChartType = SeriesChartType.Line,
                    IsValueShownAsLabel = true,
                    Color = Color.Orange
                };

                // Thêm dữ liệu vào Series
                foreach (DataRow row in dtThongKe.Rows)
                {
                    string tenNPP = row["TenNhaPhanPhoi"].ToString();
                    int soPhieuNhap = row["SoPhieuNhap"] == DBNull.Value ? 0 : Convert.ToInt32(row["SoPhieuNhap"]);
                    int tongSoLuong = row["TongSoLuong"] == DBNull.Value ? 0 : Convert.ToInt32(row["TongSoLuong"]);
                    decimal tongGiaTriNhap = row["TongGiaTriNhap"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TongGiaTriNhap"]);

                    seriesGiaTriNhap.Points.AddXY(tenNPP, tongGiaTriNhap);
                    seriesSoPhieuNhap.Points.AddXY(tenNPP, soPhieuNhap);
                }

                // Thêm Series vào Chart
                chartThongKe.Series.Add(seriesGiaTriNhap);
                chartThongKe.Series.Add(seriesSoPhieuNhap);

                // Cấu hình trục X và Y
                chartThongKe.ChartAreas[0].AxisX.Title = "Nhà phân phối";
                chartThongKe.ChartAreas[0].AxisY.Title = "Giá trị (VND / Số lượng)";
                chartThongKe.ChartAreas[0].AxisX.Interval = 1;
                chartThongKe.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

                // Thêm tiêu đề biểu đồ
                chartThongKe.Titles.Clear();
                chartThongKe.Titles.Add("Thống kê nhà phân phối theo giá trị nhập và số phiếu nhập");
                chartThongKe.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải dữ liệu biểu đồ: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnNhaPhanPhoi_Click(object sender, EventArgs e)
        {
            try
            {
                // Gọi hàm thống kê nhà phân phối
                LoadThongKeNhaPhanPhoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thực hiện thống kê nhà phân phối: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
