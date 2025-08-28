using QLCHBanMyPham.ChucNang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QLCHBanMyPham
{
    public partial class HoaDonBan : Form
    {
        DataTable HoaDon;
        public HoaDonBan()
        {
            InitializeComponent();
        }
        private void HoaDonBan_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            btnIn.Enabled = false;
            txtMaHoaDon.Enabled = true;
            txtTenNhanVien.Enabled = true;
            txtTenKhachHang.Enabled = true;
            txtDiaChi.Enabled = true;
            mskDienThoai.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            txtTongTien.Text = "0";
            Functions.FillCombo("SELECT MaKhachHang,  TenKhachHang FROM KhachHang",
                                cboMaKhachHang, "MaKhachHang", "MaKhachHang");
            cboMaKhachHang.SelectedIndex = -1;

            Functions.FillCombo("SELECT MaNhanVien,  TenNhanVien FROM NhanVien",
                                cboMaNhanVien, "MaNhanVien", "MaNhanVien");
            cboMaNhanVien.SelectedIndex = -1;

            Functions.FillCombo("SELECT MaSanPham, TenSanPham FROM SanPham",
                                cboSanPham, "MaSanPham", "MaSanPham");
            cboSanPham.SelectedIndex = -1;

            Functions.FillCombo("SELECT MaHoaDon  FROM HoaDonBan",
                                   cboMaHD, "MaHoaDon", "MaHoaDon");
            cboMaHD.SelectedIndex = -1;

            //Hiển thị thông tin của một hóa đơn được gọi từ form tìm kiếm
            if (txtMaHoaDon.Text != "")
            {
                LoadInfoHoadon();
                btnXoa.Enabled = true;
                btnIn.Enabled = true;
            }
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT ChiTietBanHang.MaSanPham, SanPham.TenSanPham,ChiTietBanHang.SoLuong, SanPham.DonGiaBan,ChiTietBanHang.Thanhtien " +
                  "FROM ChiTietBanHang , SanPham " +
                  "WHERE ChiTietBanHang.MaHoaDon = N'" + txtMaHoaDon.Text + "' AND ChiTietBanHang.MaSanPham=SanPham.MaSanPham";
           HoaDon= Functions.GetDataToTable(sql);
            dgvHoaDonBan.DataSource = HoaDon;
            dgvHoaDonBan.Columns[0].HeaderText = "Mã sản phẩm";
            dgvHoaDonBan.Columns[1].HeaderText = "Tên sản phẩm";
            dgvHoaDonBan.Columns[2].HeaderText = "Số lượng";
            dgvHoaDonBan.Columns[3].HeaderText = "Đơn giá";
            dgvHoaDonBan.Columns[4].HeaderText = "Thành tiền";
            dgvHoaDonBan.Columns[0].Width = 300;
            dgvHoaDonBan.Columns[1].Width = 300;
            dgvHoaDonBan.Columns[2].Width = 300;
            dgvHoaDonBan.Columns[3].Width = 300;
            dgvHoaDonBan.Columns[4].Width = 150;
            dgvHoaDonBan.AllowUserToAddRows = false;
            dgvHoaDonBan.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void LoadInfoHoadon()
        {
            string str;
            str = "SELECT NgayHoaDon FROM HoaDonBan WHERE MaHoaDon = N'" + txtMaHoaDon.Text + "'";
            dtpNgayHoaDon.Value = DateTime.Parse(Functions.GetFieldValues(str));

            str = "SELECT MaNhanVien FROM HoaDonBan WHERE MaHoaDon = N'" + txtMaHoaDon.Text + "'";
            cboMaNhanVien.Text = Functions.GetFieldValues(str);

            str = "SELECT NhanVien.TenNhanVien FROM NhanVien,HoaDonBan" +
                " WHERE (NhanVien.MaNhanVien = HoaDonBan.MaNhanVien) AND (HoaDonBan.MaHoaDon = N'" + txtMaHoaDon.Text + "')";
            txtTenNhanVien.Text = Functions.GetFieldValues(str);

            str = "SELECT MaKhachHang FROM HoaDonBan WHERE MaHoaDon = N'" + txtMaHoaDon.Text + "'";
            cboMaKhachHang.Text = Functions.GetFieldValues(str);

            str = "SELECT TenKhachHang FROM KhachHang,HoaDonBan" +
                " WHERE (KhachHang.MaKhachHang = HoaDonBan.MaKhachHang) AND (HoaDonBan.MaHoaDon = N'" + txtMaHoaDon.Text + "')";
            txtTenKhachHang.Text = Functions.GetFieldValues(str);

            str = "SELECT DiaChi FROM KhachHang,HoaDonBan" +
                " WHERE (KhachHang.MaKhachHang = HoaDonBan.MaKhachHang) AND (HoaDonBan.MaHoaDon = N'" + txtMaHoaDon.Text + "')";
            txtDiaChi.Text = Functions.GetFieldValues(str);

            str = "SELECT DienThoai FROM KhachHang,HoaDonBan" +
                " WHERE (KhachHang.MaKhachHang = HoaDonBan.MaKhachHang) AND (HoaDonBan.MaHoaDon = N'" + txtMaHoaDon.Text + "')";
            mskDienThoai.Text = Functions.GetFieldValues(str);

            str = "SELECT TongTien FROM HoaDonBan WHERE MaHoaDon = N'" + txtMaHoaDon.Text + "'";
            txtTongTien.Text = Functions.GetFieldValues(str);
        }
        private void ResetValues()
        {
            txtMaHoaDon.Text = "";
            dtpNgayHoaDon.Value = DateTime.Now;
            txtTenKhachHang.Text = "";
            mskDienThoai.Text = "";
            txtDiaChi.Text = "";
            txtTenNhanVien.Text = "";
            cboMaNhanVien.Text = "";
            cboMaKhachHang.Text = "";
            txtTongTien.Text = "0";
            cboSanPham.Text = "";
            txtSoLuong.Text = "";
            txtThanhTien.Text = "0";
        }
        private void ResetValuesHang()
        {
            cboSanPham.Text = "";
            txtSoLuong.Text = "";
            txtThanhTien.Text = "0";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            double sl, SLcon, tong, Tongmoi;

            sql = "SELECT MaHoaDon FROM HoaDonBan WHERE MaHoaDon=N'" + txtMaHoaDon.Text + "'";

            if (!Functions.CheckKey(sql))
            {
                if (cboMaNhanVien.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaNhanVien.Focus();
                    return;
                }

                if (cboMaKhachHang.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaKhachHang.Focus();
                    return;
                }

                sql = "INSERT INTO HoaDonBan(MaHoaDon, MaKhachHang, MaNhanVien, NgayHoaDon, TongTien) VALUES ('" + txtMaHoaDon.Text.Trim() + "'" +
                    ",N'" + cboMaKhachHang.SelectedValue + "' ,N'" + cboMaNhanVien.SelectedValue + "','" + dtpNgayHoaDon.Value + "'," + txtTongTien.Text + ")";
                Functions.RunSQL(sql);
            }
            if (cboSanPham.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboSanPham.Focus();
                return;
            }
            if ((txtSoLuong.Text.Trim().Length == 0) || (txtSoLuong.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }

            sql = "SELECT MaSanPham FROM ChiTietBanHang WHERE MaSanPham=N'" + cboSanPham.SelectedValue + "' AND MaHoaDon = N'" + txtMaHoaDon.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã sản phẩm này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetValuesHang();
                cboSanPham.Focus();
                return;
            }
            // Kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?
            sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuong FROM SanPham WHERE MaSanPham = N'" + cboSanPham.SelectedValue + "'"));
            if (Convert.ToDouble(txtSoLuong.Text) > sl)
            {
                MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            // THÊM MẶT HÀNG VÀO CHI TIẾT HÓA ĐƠN
            sql = "INSERT INTO ChiTietBanHang(MaHoaDon, MaSanPham, SoLuong, DonGiaBan, ThanhTien) VALUES(N'"
                + txtMaHoaDon.Text.Trim() + "',N'" + cboSanPham.SelectedValue + "'," + txtSoLuong.Text + ","
                + txtDonGia.Text + "," + txtThanhTien.Text + ")";
            Functions.RunSQL(sql);
            LoadDataGridView();

            // Cập nhật lại số lượng của mặt hàng vào bảng SanPham
            SLcon = sl - Convert.ToDouble(txtSoLuong.Text);
            sql = "UPDATE SanPham SET SoLuong =" + SLcon + " WHERE MaSanPham= N'" + cboSanPham.SelectedValue + "'";
            Functions.RunSQL(sql);

            // Cập nhật lại tổng tiền cho hóa đơn bán
            tong = Convert.ToDouble(Functions.GetFieldValues("SELECT TongTien FROM HoaDonBan WHERE MaHoaDon = N'" + txtMaHoaDon.Text + "'"));
            Tongmoi = tong + Convert.ToDouble(txtThanhTien.Text);

            sql = "UPDATE HoaDonBan SET TongTien =" + Tongmoi + " WHERE MaHoaDon = N'" + txtMaHoaDon.Text + "'";
            Functions.RunSQL(sql);
            txtTongTien.Text = Tongmoi.ToString();
            ResetValuesHang();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnIn.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            double sl, slcon, slxoa;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "SELECT MaSanPham,SoLuong FROM ChiTietBanHang WHERE MaHoaDon = N'" + txtMaHoaDon.Text + "'";
                DataTable SanPham = Functions.GetDataToTable(sql);
                for (int hang = 0; hang <= SanPham.Rows.Count - 1; hang++)
                {
                    // Cập nhật lại số lượng cho các mặt hàng
                    sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuong FROM SanPham WHERE MaSanPham = N'" + SanPham.Rows[hang][0].ToString() + "'"));
                    slxoa = Convert.ToDouble(SanPham.Rows[hang][1].ToString());
                    slcon = sl + slxoa;
                    sql = "UPDATE SanPham SET SoLuong =" + slcon + " WHERE MaSanPham= N'" + SanPham.Rows[hang][0].ToString() + "'";
                    Functions.RunSQL(sql);
                }

                //Xóa chi tiết hóa đơn
                sql = "DELETE ChiTietBanHang WHERE MaHoaDon=N'" + txtMaHoaDon.Text + "'";
                Functions.RunSqlDel(sql);

                //Xóa hóa đơn
                sql = "DELETEHoaDonBanWHERE MaHoaDon=N'" + txtMaHoaDon.Text + "'";
                Functions.RunSqlDel(sql);
                ResetValues();
                LoadDataGridView();
                btnXoa.Enabled = false;
                btnIn.Enabled = false;

            }

        }
        private void cboMaNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cboMaNhanVien.Text == "")
                txtTenNhanVien.Text = "";
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            str = "Select TenNhanVien FROM NhanVien where MaNhanVien =N'" + cboMaNhanVien.SelectedValue + "'";
            txtTenNhanVien.Text = Functions.GetFieldValues(str);
        }

        private void cboSanPham_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string str;
            if (cboSanPham.Text == "")
            {
                txtSanPham.Text = "";
                txtDonGia.Text = "";
            }
            // Khi chọn mã hàng thì các thông tin về hàng hiện ra
            str = "SELECT TenSanPham FROM SanPham WHERE MaSanPham =N'" + cboSanPham.SelectedValue + "'";
            txtSanPham.Text = Functions.GetFieldValues(str);
            str = "SELECT DonGiaBan FROM SanPham WHERE MaSanPham =N'" + cboSanPham.SelectedValue + "'";
            txtDonGia.Text = Functions.GetFieldValues(str);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboMaHD.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                return;
            }
            txtMaHoaDon.Text = cboMaHD.Text;
            LoadInfoHoadon();
            LoadDataGridView();
            btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            btnIn.Enabled = true;
            btnBoQua.Enabled = true;
            cboMaHD.SelectedIndex = -1;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValuesHang();
            ResetValues();
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void cboMaKhachHang_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string str;
            if (cboMaKhachHang.Text == "")
            {
                txtTenKhachHang.Text = "";
                txtDiaChi.Text = "";
                mskDienThoai.Text = "";
            }
            str = "Select TenKhachHang from KhachHang where MaKhachHang = N'" + cboMaKhachHang.SelectedValue + "'";
            txtTenKhachHang.Text = Functions.GetFieldValues(str);
            str = "Select DiaChi from KhachHang where MaKhachHang = N'" + cboMaKhachHang.SelectedValue + "'";
            txtDiaChi.Text = Functions.GetFieldValues(str);
            str = "Select DienThoai from KhachHang where MaKhachHang = N'" + cboMaKhachHang.SelectedValue + "'";
            mskDienThoai.Text = Functions.GetFieldValues(str);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnIn.Enabled = false;
            btnThem.Enabled = false;
            btnBoQua.Enabled = true;
            ResetValues();
            txtMaHoaDon.Text = Functions.CreateKey("HD"); //Khóa sinh tự động HDB
            LoadDataGridView();
        }

        private void txtSoLuong_TextChanged_1(object sender, EventArgs e)
        {
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;

            else
                sl = Convert.ToDouble(txtSoLuong.Text);

            if (txtDonGia.Text == "")
                dg = 0;

            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg;
            txtThanhTien.Text = tt.ToString();
        }


        private void btnIn_Click(object sender, EventArgs e)
        {
            // Khởi động chương trình Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;

            string sql;
            int hang = 0, cot = 0;
            DataTable tblThongtinHD, tblThongtinHang;

            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];

            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Shop Giày";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Đại Học Nam Cần Thơ - Cần Thơ";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: (07) 88904745";
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN BÁN HÀNG";

            // Biểu diễn thông tin chung của hóa đơn bán
            sql = "SELECT a.MaHoaDon, a.NgayHoaDon, a.TongTien, b.TenKhachHang, b.DiaChi, b.DienThoai, c.TenNhanVien FROM HoaDonBan AS a, " +
                "KhachHang AS b, NhanVien AS c WHERE a.MaHoaDon = N'" +
                txtMaHoaDon.Text + "' AND a.MaKhachHang = b.MaKhachHang AND a.MaNhanVien = c.MaNhanVien";

            tblThongtinHD = Functions.GetDataToTable(sql);
            exRange.Range["B6:C9"].Font.Size = 11;
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = tblThongtinHD.Rows[0][0].ToString();
            exRange.Range["B7:B7"].Value = "Khách hàng:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = tblThongtinHD.Rows[0][3].ToString();
            exRange.Range["B8:B8"].Value = "Địa chỉ:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = tblThongtinHD.Rows[0][4].ToString();
            exRange.Range["B9:B9"].Value = "Điện thoại:";
            exRange.Range["C9:E9"].MergeCells = true;
            exRange.Range["C9:E9"].Value = tblThongtinHD.Rows[0][5].ToString();

            //Lấy thông tin các mặt hàng
            sql = "SELECT  SanPham.TenSanPham,ChiTietBanHang.SoLuong, SanPham.DonGiaBan,ChiTietBanHang.Thanhtien " +
                  "FROM ChiTietBanHang , SanPham " +
                  "WHERE ChiTietBanHang.MaHoaDon = N'" + txtMaHoaDon.Text + "' AND ChiTietBanHang.MaSanPham=SanPham.MaSanPham";
            tblThongtinHang = Functions.GetDataToTable(sql);

            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 11;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Tên hàng";
            exRange.Range["C11:C11"].Value = "Số lượng";
            exRange.Range["D11:D11"].Value = "Đơn giá";
            exRange.Range["E11:E11"].Value = "Thành tiền";

            for (hang = 0; hang < tblThongtinHang.Rows.Count; hang++)
            {
                exSheet.Cells[1][hang + 12] = hang + 1;

                for (cot = 0; cot < tblThongtinHang.Columns.Count; cot++)
                {
                    exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString();
                    if (cot == 3) exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString() + "VND";
                }
            }

            exRange = exSheet.Cells[cot][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = exSheet.Cells[cot + 1][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = tblThongtinHD.Rows[0][2].ToString();
            exRange = exSheet.Cells[1][hang + 15]; //Ô A1 
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            exRange.Range["A1:F1"].Value = "Bằng chữ: " + Functions.ChuyenSoSangChu(Double.Parse(tblThongtinHD.Rows[0][2].ToString()));

            exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(tblThongtinHD.Rows[0][1]);
            exRange.Range["A1:C1"].Value = "Cần Thơ, Ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].Font.Italic = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên bán hàng";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:C6"].Value = tblThongtinHD.Rows[0][6];
            exSheet.Name = "Hóa đơn nhập";
            exApp.Visible = true;
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                // Chỉ cho phép các ký tự số và ký tự điều khiển(backspace, delete, etc.)
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // Ngăn không cho ký tự không phải là số được nhập vào
                }
            }
        }

        private void cboMaNhanVien_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string str;
            if (cboMaNhanVien.Text == "")
                txtTenNhanVien.Text = "";
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            str = "Select TenNhanVien FROM NhanVien where MaNhanVien =N'" + cboMaNhanVien.SelectedValue + "'";
            txtTenNhanVien.Text = Functions.GetFieldValues(str);
        }

        private void txtThanhTien_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenNhanVien_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
