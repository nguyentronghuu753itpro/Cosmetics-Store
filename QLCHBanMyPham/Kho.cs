using System;
using System.Data;
using System.Windows.Forms;

namespace QLCHBanMyPham
{
    public partial class Kho : Form
    {
        DataTable kho;

        public Kho()
        {
            InitializeComponent();
        }

        private void Kho_Load_1(object sender, EventArgs e)
        {
            txtMaKho.Enabled = false;
            btnLuu.Enabled = false;
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            string sql = "SELECT * FROM Kho";
            kho = ChucNang.Functions.GetDataToTable(sql); // Đọc dữ liệu từ bảng
            dvgKho.DataSource = kho; // Gắn dữ liệu cho DataGridView

            dvgKho.Columns[0].HeaderText = "Mã Kho";
            dvgKho.Columns[1].HeaderText = "Tên Kho";
            dvgKho.Columns[2].HeaderText = "Địa Chỉ Kho";

            dvgKho.Columns[0].Width = 100;
            dvgKho.Columns[1].Width = 150;
            dvgKho.Columns[2].Width = 200;

            dvgKho.AllowUserToAddRows = false; // Không cho người dùng thêm dữ liệu trực tiếp
            dvgKho.EditMode = DataGridViewEditMode.EditProgrammatically; // Không cho sửa dữ liệu trực tiếp
        }

        private void LoadDataGridView1(string maKho)
        {
            string sql = $"SELECT SanPham.MaKho, SanPham.MaSanPham, SanPham.TenSanPham FROM SanPham WHERE SanPham.MaKho = N'{maKho}'";
            DataTable khoSanPham = ChucNang.Functions.GetDataToTable(sql); // Đọc dữ liệu từ bảng

            dgvCheckKho.DataSource = khoSanPham; // Gắn dữ liệu cho DataGridView

            dgvCheckKho.Columns[0].HeaderText = "Mã Kho";
            dgvCheckKho.Columns[1].HeaderText = "Mã Sản Phẩm";
            dgvCheckKho.Columns[2].HeaderText = "Tên Sản Phẩm";

            dgvCheckKho.Columns[0].Width = 150;
            dgvCheckKho.Columns[1].Width = 150;
            dgvCheckKho.Columns[2].Width = 200;

            dgvCheckKho.AllowUserToAddRows = false; // Không cho người dùng thêm dữ liệu trực tiếp
            dgvCheckKho.EditMode = DataGridViewEditMode.EditProgrammatically; // Không cho sửa dữ liệu trực tiếp
        }

        private void ResetValue()
        {
            txtMaKho.Text = "";
            txtTenKho.Text = "";
            txtDiaChi.Text = "";
        }

        private void dvgKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra dòng được chọn có hợp lệ hay không
            {
                string maKho = dvgKho.Rows[e.RowIndex].Cells["MaKho"].Value.ToString(); // Lấy giá trị cột "MaKho"
                LoadDataGridView1(maKho); // Gọi hàm để tải danh sách sản phẩm trong kho

                txtMaKho.Text = dvgKho.Rows[e.RowIndex].Cells["MaKho"].Value.ToString();
                txtTenKho.Text = dvgKho.Rows[e.RowIndex].Cells["TenKho"].Value.ToString();
                txtDiaChi.Text = dvgKho.Rows[e.RowIndex].Cells["DiaChi"].Value.ToString();

                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnBoQua.Enabled = true;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;

            ResetValue();
            txtMaKho.Enabled = true;
            txtMaKho.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;

            if (txtMaKho.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKho.Focus();
                return;
            }
            if (txtTenKho.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKho.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }

            sql = $"SELECT MaKho FROM Kho WHERE MaKho = N'{txtMaKho.Text.Trim()}'";
            if (ChucNang.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã kho này đã tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKho.Focus();
                return;
            }

            sql = $"INSERT INTO Kho VALUES (N'{txtMaKho.Text.Trim()}', N'{txtTenKho.Text.Trim()}', N'{txtDiaChi.Text.Trim()}')";
            ChucNang.Functions.RunSQL(sql);

            LoadDataGridView();
            ResetValue();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaKho.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;

            if (txtMaKho.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtTenKho.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            sql = $"UPDATE Kho SET TenKho = N'{txtTenKho.Text.Trim()}', DiaChi = N'{txtDiaChi.Text.Trim()}' WHERE MaKho = N'{txtMaKho.Text.Trim()}'";
            ChucNang.Functions.RunSQL(sql);

            LoadDataGridView();
            ResetValue();

            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;

            if (txtMaKho.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn mã kho để xoá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xoá kho này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = $"DELETE FROM Kho WHERE MaKho = N'{txtMaKho.Text.Trim()}'";
                ChucNang.Functions.RunSQL(sql);

                LoadDataGridView();
                ResetValue();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            txtMaKho.Enabled = false;
        }
    }
}
