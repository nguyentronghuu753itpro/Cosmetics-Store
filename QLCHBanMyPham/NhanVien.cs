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
    public partial class NhanVien : Form
    {
        DataTable nv;
        public NhanVien()
        {
            InitializeComponent();
        }
        public void LoadDataGridView()
        {
            string sql = "SELECT MaNhanVien, MaKho, TenNhanVien, GioiTinh, DienThoai, Email, NgaySinh, DiaChi FROM NhanVien";
            nv = Functions.GetDataToTable(sql); //lấy dữ liệu
            dgvNhanVien.DataSource = nv;
            dgvNhanVien.Columns[0].HeaderText = "Mã Nhân Viên";
            dgvNhanVien.Columns[1].HeaderText = "Mã Kho";
            dgvNhanVien.Columns[2].HeaderText = "Tên Nhân Viên";
            dgvNhanVien.Columns[3].HeaderText = "Giới tính";
            dgvNhanVien.Columns[4].HeaderText = "Điện thoại"; // Đổi vị trí Điện thoại
            dgvNhanVien.Columns[5].HeaderText = "Email";      // Đổi vị trí Email
            dgvNhanVien.Columns[6].HeaderText = "Ngày sinh"; // Đổi vị trí Ngày sinh
            dgvNhanVien.Columns[7].HeaderText = "Địa chỉ";    // Đưa Địa chỉ xuống cuối
            dgvNhanVien.Columns[0].Width = 100;
            dgvNhanVien.Columns[1].Width = 100;
            dgvNhanVien.Columns[2].Width = 200;
            dgvNhanVien.Columns[3].Width = 200;
            dgvNhanVien.Columns[4].Width = 200;
            dgvNhanVien.Columns[5].Width = 200;
            dgvNhanVien.Columns[6].Width = 200;
            dgvNhanVien.Columns[7].Width = 200;
            dgvNhanVien.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvNhanVien.AllowUserToAddRows = false;
        }

        private void ResetValues()
        {
            txtMaNhanVien.Text = "";
            txtTenNhanVien.Text = "";
            chkGioiTinh.Checked = false;
            txtDiaChi.Text = "";
            mskNgaySinh.Value = DateTime.Now; ;
            mskDienThoai.Text = "";
            txtEmail.Text = "";
            cboMaKho.Text = "";
        }

        private void dgvNhanVien_Click(object sender, EventArgs e)
        {
            if (tsThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNhanVien.Focus();
                return;
            }
            if (nv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaNhanVien.Text = dgvNhanVien.CurrentRow.Cells["MaNhanVien"].Value.ToString();
            txtTenNhanVien.Text = dgvNhanVien.CurrentRow.Cells["TenNhanVien"].Value.ToString();
            if (dgvNhanVien.CurrentRow.Cells["GioiTinh"].Value.ToString() == "Nam") chkGioiTinh.Checked = true;
            else chkGioiTinh.Checked = false;
            txtDiaChi.Text = dgvNhanVien.CurrentRow.Cells["DiaChi"].Value.ToString();
            mskDienThoai.Text = dgvNhanVien.CurrentRow.Cells["DienThoai"].Value.ToString();
            mskNgaySinh.Value = (DateTime)dgvNhanVien.CurrentRow.Cells["NgaySinh"].Value;
            txtEmail.Text = dgvNhanVien.CurrentRow.Cells["Email"].Value.ToString();
            cboMaKho.Text = dgvNhanVien.CurrentRow.Cells["MaKho"].Value.ToString();
            tsSua.Enabled = true;
            tsXoa.Enabled = true;
            tsXoa.Enabled = true;
        }


        private void tsLuu_Click(object sender, EventArgs e)
        {
            string sql, gt;

            if (txtMaNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanVien.Focus();
                return;
            }
            if (txtTenNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhanVien.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if (mskDienThoai.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskDienThoai.Focus();
                return;
            }

            if (cboMaKho.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaKho.Focus();
                return;
            }

            // Lấy mã kho từ ComboBox
            string maKho = cboMaKho.SelectedValue.ToString();

            // Kiểm tra tuổi
            int currentYear = DateTime.Now.Year;
            int birthYear = mskNgaySinh.Value.Year;
            if (currentYear - birthYear < 18)
            {
                MessageBox.Show("Năm sinh không hợp lệ. Tuổi phải lớn hơn hoặc bằng 18.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaySinh.Focus();
                return;
            }

            // Xác định giá trị của giới tính
            if (chkGioiTinh.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";

            // Kiểm tra nếu nhân viên đã tồn tại trong cơ sở dữ liệu
            sql = "SELECT MaNhanVien FROM NhanVien WHERE MaNhanVien=N'" + txtMaNhanVien.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                // Nếu nhân viên đã tồn tại, cập nhật thông tin
                sql = "UPDATE NhanVien SET " +
                      "TenNhanVien = N'" + txtTenNhanVien.Text.Trim() + "', " +
                      "GioiTinh = N'" + gt + "', " +
                      "DiaChi = N'" + txtDiaChi.Text.Trim() + "', " +
                      "DienThoai = '" + mskDienThoai.Text + "', " +
                      "Email = N'" + txtEmail.Text.Trim() + "', " +
                      "NgaySinh = '" + mskNgaySinh.Value.ToString("yyyy-MM-dd") + "', " +
                      "MaKho = N'" + cboMaKho.SelectedValue.ToString() + "' " + // Cập nhật MaKho
                      "WHERE MaNhanVien = N'" + txtMaNhanVien.Text.Trim() + "'";
            }
            else
            {
                // Nếu nhân viên không tồn tại, thêm mới
                sql = "INSERT INTO NhanVien (MaNhanVien, MaKho, TenNhanVien, GioiTinh, DiaChi, DienThoai, Email, NgaySinh) " +
                      "VALUES (" +
                      "N'" + txtMaNhanVien.Text.Trim() + "', " +
                      "N'" + cboMaKho.SelectedValue.ToString() + "', " + // Thêm MaKho
                      "N'" + txtTenNhanVien.Text.Trim() + "', " +
                      "N'" + gt + "', " +
                      "N'" + txtDiaChi.Text.Trim() + "', " +
                      "'" + mskDienThoai.Text + "', " +
                      "N'" + txtEmail.Text.Trim() + "', " +
                      "'" + mskNgaySinh.Value.ToString("yyyy-MM-dd") + "')";
            }


            // Thực thi câu lệnh SQL
            Functions.RunSQL(sql);

            // Tải lại dữ liệu vào DataGridView
            LoadDataGridView();

            // Đặt lại các trường nhập liệu về trạng thái ban đầu
            ResetValues();

            // Cập nhật trạng thái các nút
            tsXoa.Enabled = true;
            tsThem.Enabled = true;
            tsSua.Enabled = true;
            tsLuu.Enabled = false;
            txtMaNhanVien.Enabled = false;
        }



        private void NhanVien_Load_1(object sender, EventArgs e)
        {
            txtMaNhanVien.Enabled = false;
            LoadDataGridView();
            string sql = "SELECT MaKho, MaKho FROM Kho"; // Truy vấn lấy mã và tên kho
            Functions.FillCombo(sql, cboMaKho, "MaKho", "MaKho"); // Gắn dữ liệu cho ComboBox
            cboMaKho.SelectedIndex = -1; // Không chọn mục nào mặc định
        }

        private void tsThem_Click_1(object sender, EventArgs e)
        {
            tsSua.Enabled = false;
            tsXoa.Enabled = false;
            tsLuu.Enabled = true;
            tsThem.Enabled = false;
            ResetValues();
            tsLamMoi.Enabled = true;
            txtMaNhanVien.Enabled = true;
            txtMaNhanVien.Focus();
        }

        private void tsSua_Click_1(object sender, EventArgs e)
        {
            string sql, gt;

            if (nv.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNhanVien.Text == "") // Nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenNhanVien.Text.Trim().Length == 0) // Nếu chưa nhập tên nhân viên
            {
                MessageBox.Show("Bạn chưa nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0) // Nếu chưa nhập địa chỉ
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (mskDienThoai.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskDienThoai.Focus();
                return;
            }

            // Kiểm tra tuổi
            int currentYear = DateTime.Now.Year;
            int birthYear = mskNgaySinh.Value.Year;
            if (currentYear - birthYear < 18)
            {
                MessageBox.Show("Năm sinh không hợp lệ. Tuổi phải lớn hơn hoặc bằng 18.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaySinh.Focus();
                return;
            }

            // Xác định giá trị của giới tính
            if (chkGioiTinh.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";

            // Câu lệnh SQL cập nhật thông tin nhân viên
            sql = "UPDATE NhanVien SET TenNhanVien=N'" + txtTenNhanVien.Text.Trim() +
                  "', GioiTinh=N'" + gt +
                  "', DiaChi=N'" + txtDiaChi.Text.Trim() +
                  "', Email=N'" + txtEmail.Text.Trim() +
                  "', DienThoai='" + mskDienThoai.Text.ToString() +
                  "', NgaySinh='" + mskNgaySinh.Value.ToString("yyyy-MM-dd") +
                  "' WHERE MaNhanVien=N'" + txtMaNhanVien.Text.Trim() + "'";

            // Thực thi câu lệnh SQL
            Functions.RunSQL(sql);

            // Tải lại dữ liệu vào DataGridView
            LoadDataGridView();

            // Đặt lại các trường nhập liệu về trạng thái ban đầu
            ResetValues();

            // Cập nhật trạng thái các nút
            tsLamMoi.Enabled = false;
        }

        private void tsXoa_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (nv.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNhanVien.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE NhanVien WHERE MaNhanVien=N'" + txtMaNhanVien.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void tsLamMoi_Click_1(object sender, EventArgs e)
        {
            ResetValues();
            tsLamMoi.Enabled = false;
            tsThem.Enabled = true;
            tsXoa.Enabled = true;
            tsSua.Enabled = true;
            tsLuu.Enabled = false;
            txtMaNhanVien.Enabled = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cboMaKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaKho.SelectedIndex != -1)
            {
                string maKho = cboMaKho.SelectedValue.ToString();
                string sql = "SELECT DiaChi FROM Kho WHERE MaKho = N'" + maKho + "'";
                txtDiaChi.Text = Functions.GetFieldValues(sql);
            }
            else
            {
                txtDiaChi.Text = "";
            }
        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

