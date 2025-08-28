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
    public partial class NhaPhanPhoi : Form
    {
        DataTable NhaPP;
        public NhaPhanPhoi()
        {
            InitializeComponent();
        }
        private void NhaPhanPhoi_Load(object sender, EventArgs e)
        {
            txtMaNhaPhanPhoi.Enabled = false;
            btnLuu.Enabled = false;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaNPP, TenNhaPhanPhoi, DienThoai, DiaChi FROM NhaPhanPhoi";
            NhaPP = ChucNang.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dvgNhaPhanPhoi.DataSource = NhaPP; //Nguồn dữ dgv
            dvgNhaPhanPhoi.Columns[0].HeaderText = "Mã Nhà Phân Phối";
            dvgNhaPhanPhoi.Columns[1].HeaderText = "Tên Nhà Phân Phối";
            dvgNhaPhanPhoi.Columns[2].HeaderText = "Số Điện Thoại";
            dvgNhaPhanPhoi.Columns[3].HeaderText = "Địa Chỉ";
            dvgNhaPhanPhoi.Columns[0].Width = 300;
            dvgNhaPhanPhoi.Columns[1].Width = 300;
            dvgNhaPhanPhoi.Columns[2].Width = 300;
            dvgNhaPhanPhoi.Columns[3].Width = 300;
            dvgNhaPhanPhoi.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dvgNhaPhanPhoi.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }

        private void dvgNhaPhanPhoi_Click_1(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNhaPhanPhoi.Focus();
                return;
            }
            if (NhaPP.Rows.Count == 0)
            {
                MessageBox.Show("Không có thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaNhaPhanPhoi.Text = dvgNhaPhanPhoi.CurrentRow.Cells["MaNPP"].Value.ToString();
            txtTenNhaPhanPhoi.Text = dvgNhaPhanPhoi.CurrentRow.Cells["TenNhaPhanPhoi"].Value.ToString();
            txtDienThoai.Text = dvgNhaPhanPhoi.CurrentRow.Cells["DienThoai"].Value.ToString();
            txtDiaChi.Text = dvgNhaPhanPhoi.CurrentRow.Cells["DiaChi"].Value.ToString();
            
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }
        private void ResetValue()
        {
            txtMaNhaPhanPhoi.Text = "";
            txtTenNhaPhanPhoi.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValue(); //Xoá trắng các textbox
            txtMaNhaPhanPhoi.Enabled = true; //nhập mới
            txtMaNhaPhanPhoi.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaNhaPhanPhoi.Text.Trim().Length == 0) //Nếu chưa nhập mã ncc
            {
                MessageBox.Show("Bạn phải nhập mã nhà Phân Phối", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNhaPhanPhoi.Focus();
                return;
            }
            if (txtTenNhaPhanPhoi.Text.Trim().Length == 0) //Nếu chưa nhập tên ncc
            {
                MessageBox.Show("Bạn phải nhập tên nhà Phân Phối", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNhaPhanPhoi.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ nhà Phân Phối", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return;
            }
            if (txtDienThoai.Text == "(  )    -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDienThoai.Focus();
                return;
            }
            sql = "SELECT MaNPP From NhaPhanPhoi where MaNPP=N'" + txtMaNhaPhanPhoi.Text.Trim() + "'";
            if (ChucNang.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã npp này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhaPhanPhoi.Focus();
                return;
            }

            sql = "INSERT INTO NhaPhanPhoi VALUES(N'" +
                txtMaNhaPhanPhoi.Text + "',N'" + txtTenNhaPhanPhoi.Text + "',N'" + txtDienThoai.Text + "', N'" + txtDiaChi.Text + "')";
            ChucNang.Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            LoadDataGridView(); //Nạp lại DataGridView
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaNhaPhanPhoi.Enabled = false;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaNhaPhanPhoi.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (NhaPP.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNhaPhanPhoi.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenNhaPhanPhoi.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tên nhà Phân Phối", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ nhà Phân Phối", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtDienThoai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDienThoai.Focus();
                return;
            }
            sql = "UPDATE NhaPhanPhoi SET  TenNhaPhanPhoi=N'" + txtTenNhaPhanPhoi.Text.Trim().ToString() +
                    "',Diachi=N'" + txtDiaChi.Text.Trim().ToString() +
                    "',DienThoai='" + txtDienThoai.Text.ToString() + "' WHERE MaNPP=N'" + txtMaNhaPhanPhoi.Text + "'";

            ChucNang.Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (NhaPP.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNhaPhanPhoi.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE NhaPhanPhoi WHERE MaNPP=N'" + txtMaNhaPhanPhoi.Text + "'";
                ChucNang.Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValue();
            }
        }
    }
}
