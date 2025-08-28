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
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            if (txtUsername.Text == "huu" && txtPassword.Text == "123")
            {

                TrangChu ds = new TrangChu();
                this.Hide();
                ds.Show();
            }
            else
            {
                txtUsername.Clear();
                txtPassword.Clear();
            }
        }

        private void gunaExit_Click_1(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn muốn thoát không", "cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (dl == DialogResult.OK)
            this.Close();

        }
        private void btn_Reload_Click_1(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
        }
    }
}

