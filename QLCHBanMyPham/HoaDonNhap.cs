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
    public partial class HoaDonNhap : Form
    {
        DataTable PhieuNhap;

        public HoaDonNhap()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void HoaDonNhap_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            btnXoa.Enabled = false;
            btnIn.Enabled = true;
            txtMaPhieuNhap.ReadOnly = true;
            txtTenNhanVien.ReadOnly = true;
            txtTenNhaPhanPhoi.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            mskDienThoai.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtThanhTien.ReadOnly = true;

            Functions.FillCombo("SELECT MaNPP, TenNhaPhanPhoi FROM NhaPhanPhoi", cboMaNhaPhanPhoi, "MaNPP", "MaNPP");
            cboMaNhaPhanPhoi.SelectedIndex = -1;

            Functions.FillCombo("SELECT MaNhanVien, TenNhanVien FROM NhanVien", cboMaNhanVien, "MaNhanVien", "MaNhanVien");
            cboMaNhanVien.SelectedIndex = -1;

            Functions.FillCombo("SELECT MaSanPham, TenSanPham FROM SanPham", cboSanPham, "MaSanPham", "MaSanPham");
            cboSanPham.SelectedIndex = -1;
            Functions.FillCombo("SELECT MaPhieuNhap  FROM PhieuNhap",
                                   cboMaHoaDonNhap, "MaPhieuNhap", "MaPhieuNhap");
            cboMaHoaDonNhap.SelectedIndex = -1;

            if (txtMaPhieuNhap.Text != "")
            {
                LoadInfoPhieuNhap();
                btnXoa.Enabled = true;
                btnIn.Enabled = true;
                btnBoQua.Enabled = true;
            }
        }

        private void LoadDataGridView()
        {
            string sql = "SELECT ChiTietNhapHang.MaPhieuNhap, SanPham.TenSanPham, ChiTietNhapHang.SoLuong, SanPham.DonGiaNhap, ChiTietNhapHang.ThanhTien " +
                          "FROM ChiTietNhapHang, SanPham " +
                          "WHERE ChiTietNhapHang.MaPhieuNhap = N'" + txtMaPhieuNhap.Text + "' AND ChiTietNhapHang.MaSanPham = SanPham.MaSanPham";
            PhieuNhap = Functions.GetDataToTable(sql);
            dgvPhieuNhap.DataSource = PhieuNhap;
            dgvPhieuNhap.Columns[0].HeaderText = "Mã Sản Phẩm";
            dgvPhieuNhap.Columns[1].HeaderText = "Tên Sản Phẩm";
            dgvPhieuNhap.Columns[2].HeaderText = "Số Lượng";
            dgvPhieuNhap.Columns[3].HeaderText = "Đơn Giá";
            dgvPhieuNhap.Columns[4].HeaderText = "Thành Tiền";
            dgvPhieuNhap.Columns[0].Width = 180;
            dgvPhieuNhap.Columns[1].Width = 200;
            dgvPhieuNhap.Columns[2].Width = 200;
            dgvPhieuNhap.Columns[3].Width = 150;
            dgvPhieuNhap.Columns[4].Width = 150;
            dgvPhieuNhap.AllowUserToAddRows = false;
            dgvPhieuNhap.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void LoadInfoPhieuNhap()
        {
            string str;

            str = "SELECT NgayNhap FROM PhieuNhap WHERE MaPhieuNhap = N'" + txtMaPhieuNhap.Text + "'";
            dtpNgayNhap.Value = DateTime.Parse(Functions.GetFieldValues(str));

            str = "SELECT MaNhanVien FROM PhieuNhap WHERE MaPhieuNhap = N'" + txtMaPhieuNhap.Text + "'";
            cboMaNhanVien.SelectedValue = Functions.GetFieldValues(str);

            str = "SELECT TenNhanVien FROM NhanVien, PhieuNhap WHERE NhanVien.MaNhanVien = PhieuNhap.MaNhanVien AND PhieuNhap.MaPhieuNhap = N'" + txtMaPhieuNhap.Text + "'";
            txtTenNhanVien.Text = Functions.GetFieldValues(str);

            str = "SELECT MaNPP FROM PhieuNhap WHERE MaPhieuNhap = N'" + txtMaPhieuNhap.Text + "'";
            cboMaNhaPhanPhoi.SelectedValue = Functions.GetFieldValues(str);

            str = "SELECT TenNhaPhanPhoi FROM NhaPhanPhoi, PhieuNhap WHERE NhaPhanPhoi.MaNPP = PhieuNhap.MaNPP AND PhieuNhap.MaPhieuNhap = N'" + txtMaPhieuNhap.Text + "'";
            txtTenNhaPhanPhoi.Text = Functions.GetFieldValues(str);

            str = "SELECT DiaChi FROM NhaPhanPhoi, PhieuNhap WHERE NhaPhanPhoi.MaNPP = PhieuNhap.MaNPP AND PhieuNhap.MaPhieuNhap = N'" + txtMaPhieuNhap.Text + "'";
            txtDiaChi.Text = Functions.GetFieldValues(str);

            str = "SELECT DienThoai FROM NhaPhanPhoi, PhieuNhap WHERE NhaPhanPhoi.MaNPP = PhieuNhap.MaNPP AND PhieuNhap.MaPhieuNhap = N'" + txtMaPhieuNhap.Text + "'";
            mskDienThoai.Text = Functions.GetFieldValues(str);
        }

        private void ResetValues()
        {
            txtMaPhieuNhap.Text = "";
            txtTenNhaPhanPhoi.Text = "";
            mskDienThoai.Text = "";
            dtpNgayNhap.Value = DateTime.Now;
            cboMaNhanVien.SelectedIndex = -1;
            cboMaNhaPhanPhoi.SelectedIndex = -1;
            cboSanPham.SelectedIndex = -1;
            txtSoLuong.Text = "";
            txtThanhTien.Text = "0";
            txtTenNhanVien.Text = "";
            txtDiaChi.Text = "";
        }

        private void ResetValuesHang()
        {
            cboSanPham.SelectedIndex = -1;
            txtSoLuong.Text = "";
            txtThanhTien.Text = "0";
            txtSanPham.Text = "";
            txtDonGia.Text = "0";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnIn.Enabled = false;
            btnThem.Enabled = false;
            btnBoQua.Enabled = true;
            ResetValues();
            txtMaPhieuNhap.Text = Functions.CreateKey("PN");
            LoadDataGridView();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            double sl, SLcon, tong, Tongmoi;

            sql = "SELECT MaPhieuNhap FROM PhieuNhap WHERE MaPhieuNhap=N'" + txtMaPhieuNhap.Text + "'";

            if (!Functions.CheckKey(sql))
            {
                if (cboMaNhanVien.SelectedIndex == -1)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaNhanVien.Focus();
                    return;
                }

                if (cboMaNhaPhanPhoi.SelectedIndex == -1)
                {
                    MessageBox.Show("Bạn phải nhập nhà Phân Phối", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaNhaPhanPhoi.Focus();
                    return;
                }

                sql = "INSERT INTO PhieuNhap(MaPhieuNhap, MaNhanVien, MaNPP, NgayNhap, TongTien) VALUES (N'" + txtMaPhieuNhap.Text.Trim() + "', N'" + cboMaNhanVien.SelectedValue + "', N'" + cboMaNhaPhanPhoi.SelectedValue + "', '" + dtpNgayNhap.Value.ToString("yyyy-MM-dd") + "', 0)";
                Functions.RunSQL(sql);
            }

            if (cboSanPham.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboSanPham.Focus();
                return;
            }
            if (txtSoLuong.Text.Trim().Length == 0 || txtSoLuong.Text == "0")
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }

            sql = "SELECT MaSanPham FROM ChiTietNhapHang WHERE MaSanPham=N'" + cboSanPham.SelectedValue + "' AND MaPhieuNhap = N'" + txtMaPhieuNhap.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã sản phẩm này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetValuesHang();
                cboSanPham.Focus();
                return;
            }

            sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuong FROM SanPham WHERE MaSanPham = N'" + cboSanPham.SelectedValue + "'"));

            sql = "INSERT INTO ChiTietNhapHang(MaPhieuNhap, MaSanPham, SoLuong, DonGiaNhap, ThanhTien) VALUES(N'" + txtMaPhieuNhap.Text.Trim() + "', N'" + cboSanPham.SelectedValue + "', " + txtSoLuong.Text + ", " + txtDonGia.Text + ", " + txtThanhTien.Text + ")";
            Functions.RunSQL(sql);
            LoadDataGridView();

            SLcon = sl + Convert.ToDouble(txtSoLuong.Text);
            sql = "UPDATE SanPham SET SoLuong = " + SLcon + " WHERE MaSanPham = N'" + cboSanPham.SelectedValue + "'";
            Functions.RunSQL(sql);

            tong = Convert.ToDouble(Functions.GetFieldValues("SELECT TongTien FROM PhieuNhap WHERE MaPhieuNhap = N'" + txtMaPhieuNhap.Text + "'"));
            Tongmoi = tong + Convert.ToDouble(txtThanhTien.Text);
            sql = "UPDATE PhieuNhap SET TongTien = " + Tongmoi + " WHERE MaPhieuNhap = N'" + txtMaPhieuNhap.Text + "'";
            Functions.RunSQL(sql);

            ResetValuesHang();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnIn.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValuesHang();
            ResetValues();
            btnThem.Enabled = true;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void cboMaNhanVien_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string str;
            if (cboMaNhanVien.SelectedIndex == -1)
                txtTenNhanVien.Text = "";
            else
            {
                str = "SELECT TenNhanVien FROM NhanVien WHERE MaNhanVien = N'" + cboMaNhanVien.SelectedValue + "'";
                txtTenNhanVien.Text = Functions.GetFieldValues(str);
            }
        }

        private void cboMaNhaPhanPhoi_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string str;
            if (cboMaNhaPhanPhoi.SelectedIndex == -1)
            {
                txtTenNhaPhanPhoi.Text = "";
                txtDiaChi.Text = "";
                mskDienThoai.Text = "";
            }
            else
            {
                str = "SELECT TenNhaPhanPhoi FROM NhaPhanPhoi WHERE MaNPP = N'" + cboMaNhaPhanPhoi.SelectedValue + "'";
                txtTenNhaPhanPhoi.Text = Functions.GetFieldValues(str);
                str = "SELECT DiaChi FROM NhaPhanPhoi WHERE MaNPP = N'" + cboMaNhaPhanPhoi.SelectedValue + "'";
                txtDiaChi.Text = Functions.GetFieldValues(str);
                str = "SELECT DienThoai FROM NhaPhanPhoi WHERE MaNPP = N'" + cboMaNhaPhanPhoi.SelectedValue + "'";
                mskDienThoai.Text = Functions.GetFieldValues(str);
            }
        }

        private void cboSanPham_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string str;
            if (cboSanPham.SelectedIndex == -1)
            {
                txtSanPham.Text = "";
                txtDonGia.Text = "";
            }
            else
            {
                str = "SELECT TenSanPham FROM SanPham WHERE MaSanPham = N'" + cboSanPham.SelectedValue + "'";
                txtSanPham.Text = Functions.GetFieldValues(str);
                str = "SELECT DonGiaNhap FROM SanPham WHERE MaSanPham = N'" + cboSanPham.SelectedValue + "'";
                txtDonGia.Text = Functions.GetFieldValues(str);
            }
        }

        private void txtSoLuong_TextChanged_1(object sender, EventArgs e)
        {
            double tt, sl, dg;
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            double sl, slcon;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "SELECT MaSanPham, SoLuong FROM ChiTietNhapHang WHERE MaPhieuNhap = N'" + txtMaPhieuNhap.Text + "'";
                DataTable tblHang = Functions.GetDataToTable(sql);

                for (int i = 0; i <= tblHang.Rows.Count - 1; i++)
                {
                    sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuong FROM SanPham WHERE MaSanPham = N'" + tblHang.Rows[i][0].ToString() + "'"));
                    slcon = sl - Convert.ToDouble(tblHang.Rows[i][1].ToString());
                    sql = "UPDATE SanPham SET SoLuong = " + slcon + " WHERE MaSanPham = N'" + tblHang.Rows[i][0].ToString() + "'";
                    Functions.RunSQL(sql);
                }

                sql = "DELETE ChiTietNhapHang WHERE MaPhieuNhap=N'" + txtMaPhieuNhap.Text + "'";
                Functions.RunSqlDel(sql);

                sql = "DELETE PhieuNhap WHERE MaPhieuNhap=N'" + txtMaPhieuNhap.Text + "'";
                Functions.RunSqlDel(sql);
                ResetValues();
                LoadDataGridView();
                btnXoa.Enabled = false;
                btnIn.Enabled = false;
                ResetValuesHang();
            }
        }
        private void btnIn_Click(object sender, EventArgs e)
        {
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;

            string sql;
            int hang = 0, cot = 0;
            DataTable ThongTinPN, ThongTinSP;

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
            exRange.Range["A1:B1"].Value = "Shop Mỹ Phẩm";
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
            exRange.Range["C2:E2"].Value = "PHIẾU NHẬP HÀNG";

            // Biểu diễn thông tin chung của hóa đơn bán
            sql = "SELECT a.MaPhieuNhap, a.NgayNhap, a.TongTien, b.TenNhaPhanPhoi, b.DiaChi, b.DienThoai, c.TenNhanVien FROM PhieuNhap AS a, " +
                "NhaPhanPhoi AS b, NhanVien AS c WHERE a.MaPhieuNhap = N'" +
                txtMaPhieuNhap.Text + "' AND a.MaNPP = b.MaNPP AND a.MaNhanVien = c.MaNhanVien";
            ThongTinPN = Functions.GetDataToTable(sql);
            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:B6"].Value = "Mã phiếu nhập:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = ThongTinPN.Rows[0][0].ToString();
            exRange.Range["B7:B7"].Value = "Nhà Phân Phối:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = ThongTinPN.Rows[0][3].ToString();
            exRange.Range["B8:B8"].Value = "Địa chỉ:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = ThongTinPN.Rows[0][4].ToString();
            exRange.Range["B9:B9"].Value = "Điện thoại:";
            exRange.Range["C9:E9"].MergeCells = true;
            exRange.Range["C9:E9"].Value = ThongTinPN.Rows[0][5].ToString();

            //Lấy thông tin các mặt hàng
            sql = "SELECT SanPham.TenSanPham,ChiTietNhapHang.SoLuong, SanPham.DonGiaNhap, ChiTietNhapHang.ThanhTien " +
                "FROM ChiTietNhapHang , SanPham " +
                "WHERE ChiTietNhapHang.MaPhieuNhap = N'" + txtMaPhieuNhap.Text + "' AND ChiTietNhapHang.MaSanPham=SanPham.MaSanPham ";
            ThongTinSP = Functions.GetDataToTable(sql);

            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Tên sản phẩm";
            exRange.Range["C11:C11"].Value = "Số lượng";
            exRange.Range["D11:D11"].Value = "Đơn giá";
            exRange.Range["E11:E11"].Value = "Thành tiền";

            for (hang = 0; hang < ThongTinSP.Rows.Count; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][hang + 12] = hang + 1;

                for (cot = 0; cot < ThongTinSP.Columns.Count; cot++)
                //Điền thông tin hàng từ cột thứ 2, dòng 12
                {
                    exSheet.Cells[cot + 2][hang + 12] = ThongTinSP.Rows[hang][cot].ToString();
                    if (cot == 3) exSheet.Cells[cot + 2][hang + 12] = ThongTinSP.Rows[hang][cot].ToString();
                }
            }

            exRange = exSheet.Cells[cot][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = exSheet.Cells[cot + 1][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = ThongTinPN.Rows[0][2].ToString();
            exRange = exSheet.Cells[1][hang + 15]; //Ô A1 
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            exRange.Range["A1:F1"].Value = "Bằng chữ: " + Functions.ChuyenSoSangChu(Double.Parse(ThongTinPN.Rows[0][2].ToString()));

            exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(ThongTinPN.Rows[0][1]);
            exRange.Range["A1:C1"].Value = "Cần Thơ, Ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].Font.Italic = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên bán hàng";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:C6"].Value = ThongTinPN.Rows[0][6];
            exSheet.Name = "Phiếu nhập";
            exApp.Visible = true;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cboMaHoaDonNhap.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaHoaDonNhap.Focus();
                return;
            }

            txtMaPhieuNhap.Text = cboMaHoaDonNhap.Text;
            LoadInfoPhieuNhap();
            LoadDataGridView();
            btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            btnIn.Enabled = true;
            btnBoQua.Enabled = true;
            cboMaHoaDonNhap.SelectedIndex = -1;
        }

    }
}
