namespace QLCHBanMyPham
{
    partial class ThongKeKho
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label tk;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartThongKe = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnNhaPhanPhoi = new System.Windows.Forms.Button();
            this.btnNhanVien = new System.Windows.Forms.Button();
            this.btnThongKeBan = new System.Windows.Forms.Button();
            this.dtpNgayBatDau = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtpNgayKetThuc = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnThongKeNhap = new System.Windows.Forms.Button();
            this.btnThongKeSoLuong = new System.Windows.Forms.Button();
            tk = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartThongKe)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tk
            // 
            tk.AutoSize = true;
            tk.BackColor = System.Drawing.Color.White;
            tk.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            tk.Location = new System.Drawing.Point(511, 9);
            tk.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            tk.Name = "tk";
            tk.Size = new System.Drawing.Size(288, 69);
            tk.TabIndex = 36;
            tk.Text = "Thống Kê";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.White;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            label1.Location = new System.Drawing.Point(772, 97);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(120, 22);
            label1.TabIndex = 36;
            label1.Text = "Ngày kết thúc";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.White;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            label2.Location = new System.Drawing.Point(529, 97);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(117, 22);
            label2.TabIndex = 37;
            label2.Text = "Ngày bắt đầu";
            // 
            // chartThongKe
            // 
            chartArea1.Name = "ChartArea1";
            this.chartThongKe.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartThongKe.Legends.Add(legend1);
            this.chartThongKe.Location = new System.Drawing.Point(13, 176);
            this.chartThongKe.Margin = new System.Windows.Forms.Padding(4);
            this.chartThongKe.Name = "chartThongKe";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Thống Kê doanh thu bán ";
            this.chartThongKe.Series.Add(series1);
            this.chartThongKe.Size = new System.Drawing.Size(1005, 443);
            this.chartThongKe.TabIndex = 0;
            this.chartThongKe.Text = "Thong kê";
            this.chartThongKe.Click += new System.EventHandler(this.chartThongKe_Click);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.btnNhaPhanPhoi);
            this.guna2Panel1.Controls.Add(this.btnNhanVien);
            this.guna2Panel1.Controls.Add(tk);
            this.guna2Panel1.Location = new System.Drawing.Point(-3, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1356, 95);
            this.guna2Panel1.TabIndex = 84;
            // 
            // btnNhaPhanPhoi
            // 
            this.btnNhaPhanPhoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhaPhanPhoi.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnNhaPhanPhoi.Location = new System.Drawing.Point(186, 13);
            this.btnNhaPhanPhoi.Margin = new System.Windows.Forms.Padding(4);
            this.btnNhaPhanPhoi.Name = "btnNhaPhanPhoi";
            this.btnNhaPhanPhoi.Size = new System.Drawing.Size(160, 67);
            this.btnNhaPhanPhoi.TabIndex = 38;
            this.btnNhaPhanPhoi.Text = "&Thống kê nhà phân phối";
            this.btnNhaPhanPhoi.UseVisualStyleBackColor = true;
            this.btnNhaPhanPhoi.Click += new System.EventHandler(this.btnNhaPhanPhoi_Click);
            // 
            // btnNhanVien
            // 
            this.btnNhanVien.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhanVien.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnNhanVien.Location = new System.Drawing.Point(16, 13);
            this.btnNhanVien.Margin = new System.Windows.Forms.Padding(4);
            this.btnNhanVien.Name = "btnNhanVien";
            this.btnNhanVien.Size = new System.Drawing.Size(147, 67);
            this.btnNhanVien.TabIndex = 37;
            this.btnNhanVien.Text = "&Thống kê nhân viên";
            this.btnNhanVien.UseVisualStyleBackColor = true;
            this.btnNhanVien.Click += new System.EventHandler(this.btnNhanVien_Click);
            // 
            // btnThongKeBan
            // 
            this.btnThongKeBan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKeBan.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnThongKeBan.Location = new System.Drawing.Point(13, 112);
            this.btnThongKeBan.Margin = new System.Windows.Forms.Padding(4);
            this.btnThongKeBan.Name = "btnThongKeBan";
            this.btnThongKeBan.Size = new System.Drawing.Size(147, 46);
            this.btnThongKeBan.TabIndex = 5;
            this.btnThongKeBan.Text = "&Thống kê bán";
            this.btnThongKeBan.UseVisualStyleBackColor = true;
            this.btnThongKeBan.Click += new System.EventHandler(this.btnThongKeBan_Click);
            // 
            // dtpNgayBatDau
            // 
            this.dtpNgayBatDau.Checked = true;
            this.dtpNgayBatDau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpNgayBatDau.Location = new System.Drawing.Point(533, 123);
            this.dtpNgayBatDau.Margin = new System.Windows.Forms.Padding(4);
            this.dtpNgayBatDau.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgayBatDau.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgayBatDau.Name = "dtpNgayBatDau";
            this.dtpNgayBatDau.Size = new System.Drawing.Size(235, 45);
            this.dtpNgayBatDau.TabIndex = 33;
            this.dtpNgayBatDau.Value = new System.DateTime(2024, 11, 29, 2, 10, 7, 34);
            // 
            // dtpNgayKetThuc
            // 
            this.dtpNgayKetThuc.Checked = true;
            this.dtpNgayKetThuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgayKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpNgayKetThuc.Location = new System.Drawing.Point(776, 123);
            this.dtpNgayKetThuc.Margin = new System.Windows.Forms.Padding(4);
            this.dtpNgayKetThuc.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgayKetThuc.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgayKetThuc.Name = "dtpNgayKetThuc";
            this.dtpNgayKetThuc.Size = new System.Drawing.Size(232, 45);
            this.dtpNgayKetThuc.TabIndex = 34;
            this.dtpNgayKetThuc.Value = new System.DateTime(2024, 11, 29, 2, 10, 10, 478);
            // 
            // btnThongKeNhap
            // 
            this.btnThongKeNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKeNhap.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnThongKeNhap.Location = new System.Drawing.Point(168, 112);
            this.btnThongKeNhap.Margin = new System.Windows.Forms.Padding(4);
            this.btnThongKeNhap.Name = "btnThongKeNhap";
            this.btnThongKeNhap.Size = new System.Drawing.Size(162, 46);
            this.btnThongKeNhap.TabIndex = 85;
            this.btnThongKeNhap.Text = "&Thống kê nhập";
            this.btnThongKeNhap.UseVisualStyleBackColor = true;
            this.btnThongKeNhap.Click += new System.EventHandler(this.btnThongKeNhap_Click);
            // 
            // btnThongKeSoLuong
            // 
            this.btnThongKeSoLuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThongKeSoLuong.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnThongKeSoLuong.Location = new System.Drawing.Point(338, 114);
            this.btnThongKeSoLuong.Margin = new System.Windows.Forms.Padding(4);
            this.btnThongKeSoLuong.Name = "btnThongKeSoLuong";
            this.btnThongKeSoLuong.Size = new System.Drawing.Size(188, 44);
            this.btnThongKeSoLuong.TabIndex = 86;
            this.btnThongKeSoLuong.Text = "&Thống kê số lượng";
            this.btnThongKeSoLuong.UseVisualStyleBackColor = true;
            this.btnThongKeSoLuong.Click += new System.EventHandler(this.btnThongKeSoLuong_Click);
            // 
            // ThongKeKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1314, 766);
            this.Controls.Add(this.btnThongKeSoLuong);
            this.Controls.Add(this.btnThongKeNhap);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.dtpNgayKetThuc);
            this.Controls.Add(this.dtpNgayBatDau);
            this.Controls.Add(this.btnThongKeBan);
            this.Controls.Add(this.chartThongKe);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ThongKeKho";
            this.Text = "ThongKeKho";
            this.Load += new System.EventHandler(this.ThongKeKho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartThongKe)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartThongKe;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Button btnThongKeBan;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayBatDau;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgayKetThuc;
        private System.Windows.Forms.Button btnThongKeNhap;
        private System.Windows.Forms.Button btnThongKeSoLuong;
        private System.Windows.Forms.Button btnNhanVien;
        private System.Windows.Forms.Button btnNhaPhanPhoi;
    }
}