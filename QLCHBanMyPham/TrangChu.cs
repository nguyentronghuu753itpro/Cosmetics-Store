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
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
        }

        private Form activeForm = null;
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();

            }
            //ActivateButton(btnSender); /*đổi màu các Form*/
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelChildForm1.Controls.Add(childForm);
            this.panelChildForm1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btn_KhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new KhachHang(), sender);
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            Functions.Connect();
        }

        private void btn_Kho_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Kho(), sender);
        }

        private void gunaExit_Click(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn muốn thoát không", "cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (dl == DialogResult.OK)
                this.Close();
        }
        private void btn_Giay_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new SanPham(), sender);
        }
        private void TrangChu_Load_1(object sender, EventArgs e)
        {
            ChucNang.Functions.Connect();
        }
        private void btn_KhachHang_Click_1(object sender, EventArgs e)
        {
             OpenChildForm(new KhachHang(), sender);
        }
        private void btn_Nhanvien_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new NhanVien(), sender);
        }

        private void btn_Thoat_Click_1(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn muốn thoát không", "cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (dl == DialogResult.OK)
                this.Close();
        }

        private void btn_NPP_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new NhaPhanPhoi(), sender);
        }

        private void btn_NhapHang_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new HoaDonNhap(), sender);
        }

        private void btn_BanHang_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new HoaDonBan(), sender);
        }

        private void btn_PhanLoaiGiay_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new LoaiMyPham(), sender);
        }

        private void btn_ThongKeBan_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new ThongKeKho(), sender);
        }
    }
}
