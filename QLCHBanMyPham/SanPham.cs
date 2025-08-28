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

namespace QLCHBanMyPham
{
    public partial class SanPham : Form
    {
        DataTable SP;
        public SanPham()
        {
            InitializeComponent();
        }

        private void SanPham_Load(object sender, EventArgs e)
        {
            string sql, slq;
            sql = "SELECT * from DanhMucSP";
            slq = "SELECT * FROM NhaPhanPhoi";
            txtMaSanPham.Enabled = false;
            txtTinhTrang.Enabled = false;
            txtHangSuDung.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
            Functions.FillCombo(sql, cboTenDanhMuc, "MaDanhMuc", "TenDanhMuc");
            cboTenDanhMuc.SelectedIndex = -1;
            Functions.FillCombo(slq, cboTenNPP, "MaNPP", "TenNhaPhanPhoi");
            cboTenNPP.SelectedIndex = -1;
            ResetValues();
            string sqll = "SELECT MaKho, MaKho FROM Kho"; // Truy vấn lấy mã và tên kho
            Functions.FillCombo(sqll, cboMaKho, "MaKho", "MaKho"); // Gắn dữ liệu cho ComboBox
            cboMaKho.SelectedIndex = -1; // Không chọn mục nào mặc định
        }
        private void ResetValues()
        {
            txtMaSanPham.Text = "";
            txtTenSP.Text = "";
            cboTenDanhMuc.Text = "";
            txtSoLuong.Text = "";
            txtDonGiaNhap.Text = "";
            txtDonGiaBan.Text = "";
            txtSoLuong.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtDonGiaBan.Enabled = true;
            txtGhiChu.Text = "";
            mskNgaySanXuat.Value = DateTime.Now; ;
            txtHanSuDung.Text = "";
            cboMaKho.Text = "";


        }
        private void LoadDataGridView()
        {
            string sql;
            // Thêm cột "Hạn sử dụng (Date)" vào truy vấn SQL
            sql = "SELECT SanPham.MaSanPham, SanPham.TenSanPham, DanhMucSP.TenDanhMuc, NhaPhanPhoi.TenNhaPhanPhoi, SanPham.DonGiaNhap, SanPham.DonGiaBan, SanPham.SoLuong, SanPham.GhiChu, SanPham.NgaySanXuat, CONVERT(VARCHAR(10), SanPham.HanSuDungDate, 103) AS HanSuDungDate, SanPham.HanSuDungText, SanPham.MaKho " +
                  "FROM SanPham " +
                  "JOIN DanhMucSP ON SanPham.MaDanhMuc = DanhMucSP.MaDanhMuc " +
                  "JOIN NhaPhanPhoi ON SanPham.MaNPP = NhaPhanPhoi.MaNPP";

            SP = Functions.GetDataToTable(sql);
            dgvSanPham.DataSource = SP;

            // Đặt tiêu đề các cột
            dgvSanPham.Columns[0].HeaderText = "Mã sản phẩm";
            dgvSanPham.Columns[1].HeaderText = "Tên sản phẩm";
            dgvSanPham.Columns[2].HeaderText = "Danh mục";
            dgvSanPham.Columns[3].HeaderText = "Nhà phân phối";
            dgvSanPham.Columns[4].HeaderText = "Đơn giá nhập";
            dgvSanPham.Columns[5].HeaderText = "Đơn giá bán";
            dgvSanPham.Columns[6].HeaderText = "Số lượng";
            dgvSanPham.Columns[7].HeaderText = "Ghi chú";
            dgvSanPham.Columns[8].HeaderText = "Ngày sản xuất";
            dgvSanPham.Columns[9].HeaderText = "Hạn sử dụng (Date)";
            dgvSanPham.Columns[10].HeaderText = "Hạn sử dụng (Text)";
            dgvSanPham.Columns[11].HeaderText = "Mã kho";

            // Đặt độ rộng các cột
            dgvSanPham.Columns[0].Width = 130;
            dgvSanPham.Columns[1].Width = 140;
            dgvSanPham.Columns[2].Width = 160;
            dgvSanPham.Columns[3].Width = 150;
            dgvSanPham.Columns[4].Width = 120;
            dgvSanPham.Columns[5].Width = 120;
            dgvSanPham.Columns[6].Width = 120;
            dgvSanPham.Columns[7].Width = 120;
            dgvSanPham.Columns[8].Width = 120; // Ngày sản xuất
            dgvSanPham.Columns[9].Width = 120; // Hạn sử dụng (Date)
            dgvSanPham.Columns[10].Width = 120; // Hạn sử dụng (Text)
            dgvSanPham.Columns[11].Width = 120; // Mã kho

            dgvSanPham.AllowUserToAddRows = false;
            dgvSanPham.EditMode = DataGridViewEditMode.EditProgrammatically;
        }






        private void btnThem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            ResetValues();
            txtMaSanPham.Enabled = true;
            txtMaSanPham.Focus();
            txtSoLuong.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtDonGiaBan.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaSanPham.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSanPham.Focus();
                return;
            }

            if (txtTenSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenSP.Focus();
                return;
            }

            if (cboTenDanhMuc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập danh mục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboTenDanhMuc.Focus();
                return;
            }

            if (cboTenNPP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập nhà phân phối", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboTenNPP.Focus();
                return;
            }

            if (txtSoLuong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Focus();
                return;
            }

            if (cboMaKho.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaKho.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(mskNgaySanXuat.Text) || mskNgaySanXuat.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập Ngày sản xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskNgaySanXuat.Focus();
                return;
            }

            if (txtHanSuDung.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập hạn sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHanSuDung.Focus();
                return;
            }

            DateTime ngaySanXuat = DateTime.Parse(mskNgaySanXuat.Text);
            string hanSuDungText = txtHanSuDung.Text.Trim();
            DateTime hanSuDungDate;

            if (hanSuDungText.Contains("năm"))
            {
                int soNam = int.Parse(hanSuDungText.Replace("năm", "").Trim());
                hanSuDungDate = ngaySanXuat.AddYears(soNam);
            }
            else if (hanSuDungText.Contains("tháng"))
            {
                int soThang = int.Parse(hanSuDungText.Replace("tháng", "").Trim());
                hanSuDungDate = ngaySanXuat.AddMonths(soThang);
            }
            else if (hanSuDungText.Contains("ngày"))
            {
                int soNgay = int.Parse(hanSuDungText.Replace("ngày", "").Trim());
                hanSuDungDate = ngaySanXuat.AddDays(soNgay);
            }
            else
            {
                MessageBox.Show("Hạn sử dụng không hợp lệ. Vui lòng nhập theo định dạng 'X năm', 'X tháng' hoặc 'X ngày'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHanSuDung.Focus();
                return;
            }

            sql = "INSERT INTO SanPham(MaSanPham, TenSanPham, MaDanhMuc, MaNPP, MaKho, SoLuong, DonGiaNhap, DonGiaBan, GhiChu, NgaySanXuat, HanSuDungText, HanSuDungDate) VALUES(N'"
                 + txtMaSanPham.Text.Trim() + "', N'" + txtTenSP.Text.Trim() +
                 "', N'" + cboTenDanhMuc.SelectedValue.ToString() +
                 "', N'" + cboTenNPP.SelectedValue.ToString() +
                 "', N'" + cboMaKho.SelectedValue.ToString() +
                 "', " + txtSoLuong.Text.Trim() + ", " + txtDonGiaNhap.Text +
                 ", " + txtDonGiaBan.Text + ", N'" + txtGhiChu.Text.Trim() + "', '"
                 + ngaySanXuat.ToString("yyyy-MM-dd") + "', N'"
                 + hanSuDungText + "', '"
                 + hanSuDungDate.ToString("yyyy-MM-dd") + "')";

            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaSanPham.Enabled = false;
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql; // Lưu câu lệnh SQL

            // Kiểm tra nếu không còn dữ liệu
            if (SP.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Kiểm tra nếu chưa chọn bản ghi nào
            if (txtMaSanPham.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Kiểm tra nếu các trường dữ liệu bắt buộc chưa được nhập
            if (txtTenSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboTenNPP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập tên nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboTenDanhMuc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập tên danh mục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboMaKho.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaKho.Focus();
                return;
            }

            // Kiểm tra hạn sử dụng
            if (string.IsNullOrWhiteSpace(txtHanSuDung.Text))
            {
                MessageBox.Show("Bạn phải nhập hạn sử dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtHanSuDung.Focus();
                return;
            }

            // Tính ngày hết hạn dựa trên hạn sử dụng và ngày sản xuất
            DateTime ngaySanXuat = DateTime.Parse(mskNgaySanXuat.Text);
            string hanSuDungText = txtHanSuDung.Text.Trim();
            DateTime hanSuDungDate;

            if (hanSuDungText.Contains("năm"))
            {
                int soNam = int.Parse(hanSuDungText.Replace("năm", "").Trim());
                hanSuDungDate = ngaySanXuat.AddYears(soNam);
            }
            else if (hanSuDungText.Contains("tháng"))
            {
                int soThang = int.Parse(hanSuDungText.Replace("tháng", "").Trim());
                hanSuDungDate = ngaySanXuat.AddMonths(soThang);
            }
            else if (hanSuDungText.Contains("ngày"))
            {
                int soNgay = int.Parse(hanSuDungText.Replace("ngày", "").Trim());
                hanSuDungDate = ngaySanXuat.AddDays(soNgay);
            }
            else
            {
                MessageBox.Show("Hạn sử dụng không hợp lệ. Vui lòng nhập theo định dạng 'X năm', 'X tháng' hoặc 'X ngày'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHanSuDung.Focus();
                return;
            }

            // Câu lệnh SQL cập nhật
            sql = "UPDATE SanPham SET " +
                  "TenSanPham=N'" + txtTenSP.Text.Trim() + "', " +
                  "DonGiaNhap=" + txtDonGiaNhap.Text.Trim() + ", " +
                  "DonGiaBan=" + txtDonGiaBan.Text.Trim() + ", " +
                  "SoLuong=" + txtSoLuong.Text.Trim() + ", " +
                  "MaDanhMuc=N'" + cboTenDanhMuc.SelectedValue.ToString() + "', " +
                  "MaNPP=N'" + cboTenNPP.SelectedValue.ToString() + "', " +
                  "MaKho=N'" + cboMaKho.SelectedValue.ToString() + "', " +
                  "GhiChu=N'" + txtGhiChu.Text.Trim() + "', " +
                  "NgaySanXuat='" + ngaySanXuat.ToString("yyyy-MM-dd") + "', " +
                  "HanSuDungText=N'" + hanSuDungText + "', " +
                  "HanSuDungDate='" + hanSuDungDate.ToString("yyyy-MM-dd") + "' " +
                  "WHERE MaSanPham=N'" + txtMaSanPham.Text.Trim() + "'";

            // Thực thi câu lệnh SQL
            ChucNang.Functions.RunSQL(sql);

            // Tải lại DataGridView
            LoadDataGridView();

            // Đặt lại các trường nhập liệu về trạng thái ban đầu
            ResetValues();

            // Cập nhật trạng thái các nút
            btnBoQua.Enabled = false;

            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaSanPham.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (SP.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtMaSanPham.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE SanPham WHERE MaSanPham=N'" + txtMaSanPham.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValues();
            }
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

        private void dgvSanPham_Click(object sender, EventArgs e)
        {
            string sql;
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSanPham.Focus();
                return;
            }

            if (SP.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy thông tin từ DataGridView
            txtMaSanPham.Text = dgvSanPham.CurrentRow.Cells["MaSanPham"].Value.ToString();
            txtTenSP.Text = dgvSanPham.CurrentRow.Cells["TenSanPham"].Value.ToString();
            cboTenDanhMuc.Text = dgvSanPham.CurrentRow.Cells["TenDanhMuc"].Value.ToString();
            cboTenNPP.Text = dgvSanPham.CurrentRow.Cells["TenNhaPhanPhoi"].Value.ToString();
            txtSoLuong.Text = dgvSanPham.CurrentRow.Cells["SoLuong"].Value.ToString();
            txtDonGiaNhap.Text = dgvSanPham.CurrentRow.Cells["DonGiaNhap"].Value.ToString();
            txtDonGiaBan.Text = dgvSanPham.CurrentRow.Cells["DonGiaBan"].Value.ToString();
            mskNgaySanXuat.Value = (DateTime)dgvSanPham.CurrentRow.Cells["NgaySanXuat"].Value;
            txtHanSuDung.Text = dgvSanPham.CurrentRow.Cells["HanSuDungText"].Value.ToString();
            cboMaKho.Text = dgvSanPham.CurrentRow.Cells["MaKho"].Value.ToString();

            sql = "SELECT GhiChu FROM SanPham WHERE MaSanPham = N'" + txtMaSanPham.Text + "'";
            txtGhiChu.Text = Functions.GetFieldValues(sql);

            // Xác định tình trạng hàng dựa trên số lượng
            int soLuong = Convert.ToInt32(dgvSanPham.CurrentRow.Cells["SoLuong"].Value);
            if (soLuong > 15)
            {
                txtTinhTrang.Text = "Còn hàng";
                txtTinhTrang.BackColor = Color.LightGreen;
                txtTinhTrang.ForeColor = Color.Black;
            }
            else if (soLuong > 5)
            {
                txtTinhTrang.Text = "Sắp hết hàng";
                txtTinhTrang.BackColor = Color.Yellow;
                txtTinhTrang.ForeColor = Color.Black;
            }
            else if (soLuong == 0)
            {
                txtTinhTrang.Text = "Hết hàng";
                txtTinhTrang.BackColor = Color.Red;
                txtTinhTrang.ForeColor = Color.White;
            }
            else
            {
                txtTinhTrang.Text = "Gần hết hàng";
                txtTinhTrang.BackColor = Color.Orange;
                txtTinhTrang.ForeColor = Color.Black;
            }

            // Kiểm tra hạn sử dụng
            DateTime NgaySanXuat = Convert.ToDateTime(dgvSanPham.CurrentRow.Cells["NgaySanXuat"].Value);
            string hanSuDungText = dgvSanPham.CurrentRow.Cells["HanSuDungText"].Value.ToString(); // Lấy thông tin hạn sử dụng dạng chuỗi
            DateTime currentDate = DateTime.Now;

            // Tính ngày hết hạn dựa trên ngày sản xuất và hạn sử dụng
            DateTime ngayHetHan;
            if (hanSuDungText.Contains("năm"))
            {
                int soNam = int.Parse(hanSuDungText.Replace("năm", "").Trim());
                ngayHetHan = NgaySanXuat.AddYears(soNam);
            }
            else if (hanSuDungText.Contains("tháng"))
            {
                int soThang = int.Parse(hanSuDungText.Replace("tháng", "").Trim());
                ngayHetHan = NgaySanXuat.AddMonths(soThang);
            }
            else if (hanSuDungText.Contains("ngày"))
            {
                int soNgay = int.Parse(hanSuDungText.Replace("ngày", "").Trim());
                ngayHetHan = NgaySanXuat.AddDays(soNgay);
            }
            else
            {
                // Trường hợp không đọc được hạn sử dụng
                txtHangSuDung.Text = "Không xác định hạn sử dụng";
                txtHangSuDung.BackColor = Color.Gray;
                txtHangSuDung.ForeColor = Color.White;
                return;
            }

            // Tính số ngày còn lại từ ngày hiện tại đến ngày hết hạn
            int remainingDays = (ngayHetHan - currentDate).Days;

            // Đánh giá trạng thái hạn sử dụng
            if (remainingDays > 30)
            {
                txtHangSuDung.Text = "Còn hạn sử dụng";
                txtHangSuDung.BackColor = Color.LightGreen;
                txtHangSuDung.ForeColor = Color.Black;
            }
            else if (remainingDays > 7)
            {
                txtHangSuDung.Text = "Còn hạn 1 tháng";
                txtHangSuDung.BackColor = Color.Yellow;
                txtHangSuDung.ForeColor = Color.Black;
            }
            else if (remainingDays > 0)
            {
                txtHangSuDung.Text = "Sắp hết hạn";
                txtHangSuDung.BackColor = Color.Orange;
                txtHangSuDung.ForeColor = Color.Black;
            }
            else
            {
                txtHangSuDung.Text = "Hết hạn";
                txtHangSuDung.BackColor = Color.Red;
                txtHangSuDung.ForeColor = Color.White;
            }

            // Bật các nút
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu không có mục nào được chọn
            if (cboMaKho.SelectedIndex == -1)
            {
                cboMaKho.Text = ""; // Xóa dữ liệu hiện tại
                return;
            }

            // Lấy giá trị MaKho từ ComboBox
            string maKho = cboMaKho.SelectedValue.ToString();

            // Hiển thị mã kho trong TextBox (hoặc vị trí mong muốn)
            cboMaKho.Text = maKho;
        }

        private void txtTinhTrang_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

