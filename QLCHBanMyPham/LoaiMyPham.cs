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
    public partial class LoaiMyPham : Form
    {
        DataTable DanhMuc;
        public LoaiMyPham()
        {
            InitializeComponent();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT *FROM DanhMucSP";
            DanhMuc = ChucNang.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dvgDanhMuc.DataSource = DanhMuc; //Nguồn dữ dgv
            dvgDanhMuc.Columns[0].HeaderText = "Mã Danh Mục";
            dvgDanhMuc.Columns[1].HeaderText = "Tên Danh Mục";
            dvgDanhMuc.Columns[0].Width = 250;
            dvgDanhMuc.Columns[1].Width = 250;
            dvgDanhMuc.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dvgDanhMuc.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }
        private void LoaiGiay_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            txtMaDanhMuc.Enabled = false;
            btnLuu.Enabled = false;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValue(); //Xoá trắng các textbox
            txtMaDanhMuc.Enabled = true; //nhập mới
            txtMaDanhMuc.Focus();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (txtMaDanhMuc.Text.Trim().Length == 0) //Nếu chưa nhập mã npp
            {
                MessageBox.Show("Bạn phải nhập mã danh mục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaDanhMuc.Focus();
                return;
            }
            if (txtTenDanhMuc.Text.Trim().Length == 0) //Nếu chưa nhập tên npp
            {
                MessageBox.Show("Bạn phải nhập tên danh mục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenDanhMuc.Focus();
                return;
            }

            sql = "SELECT MaDanhMuc From DanhMucSP where MaDanhMuc=N'" + txtMaDanhMuc.Text.Trim() + "'";
            if (ChucNang.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã npp này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaDanhMuc.Focus();
                return;
            }

            sql = "INSERT INTO DanhMucSP VALUES(N'" +
                txtMaDanhMuc.Text + "',N'" + txtTenDanhMuc.Text + "')";
            ChucNang.Functions.RunSQL(sql); //Thực hiện câu lệnh sql
            LoadDataGridView(); //Nạp lại DataGridView
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaDanhMuc.Enabled = false;
        }
        private void ResetValue()
        {
            txtMaDanhMuc.Text = "";
            txtTenDanhMuc.Text = "";

        }
        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaDanhMuc.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (DanhMuc.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaDanhMuc.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenDanhMuc.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tên danh mục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            sql = "UPDATE DanhMucSP SET  TenDanhMuc=N'" + txtTenDanhMuc.Text.Trim().ToString() + "'";

            ChucNang.Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (DanhMuc.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaDanhMuc.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE DanhMucSP WHERE MaDanhMuc=N'" + txtMaDanhMuc.Text + "'";
                ChucNang.Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValue();
            }
        }

        private void dvgDanhMuc_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaDanhMuc.Focus();
                return;
            }
            if (DanhMuc.Rows.Count == 0)
            {
                MessageBox.Show("Không có thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaDanhMuc.Text = dvgDanhMuc.CurrentRow.Cells["MaDanhMuc"].Value.ToString();
            txtTenDanhMuc.Text = dvgDanhMuc.CurrentRow.Cells["TenDanhMuc"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lbl_LoaiGiay_Click(object sender, EventArgs e)
        {

        }
    }
}
