using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XuatBill
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void FrMaster_Load(object sender, EventArgs e)
        {
            try
            {
                cbxRoom.Items.Clear();
                cbxRoom.Focus();
                // đưa dữ liệu từ csdl load lên DataGV
                string LayDanhSachPhong = @"Select PHONG.MAPHONG,PHONG.TENPHONG,KHACHHANG.TENKH,KHACHHANG.NAMSINH,KHACHHANG.SDT,KHACHHANG.NGAYDEN
                                            From PHONG, KHACHHANG
                                            Where PHONG.MAPHONG=KHACHHANG.MAPHONG";

                DataTable dt = KetNoiCSDL.LoadCSDL(LayDanhSachPhong);
                dgvService.DataSource = dt;

                // đưa dữ liệu lên listbox ROOM trống khách

                string ChonRoom = @"Select * 
                                    from PHONG";
                DataTable Room = KetNoiCSDL.LoadCSDL(ChonRoom);

                //cbxService.DataSource = DichVu;

                cbxRoom.SelectedIndex = -1;

                for (int i = 0; i < Room.Rows.Count; i++)
                {
                    cbxRoom.Items.Add(Room.Rows[i][1].ToString());
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j][0].ToString() == Room.Rows[i][0].ToString())
                        {
                            cbxRoom.Items.Remove(dt.Rows[j][1].ToString());
                        }
                    }
                }
                //Tạo Mã KH ngẫu nhiên 
                lblMAKH.Text = "Code";
                string LayMaKH = @"Select MAKH
                                    From PHONG, KHACHHANG
                                    Where PHONG.MAPHONG=KHACHHANG.MAPHONG";

                DataTable dtgv = KetNoiCSDL.LoadCSDL(LayMaKH);
                for (int i = 0; i < dtgv.Rows.Count; i++)
                {
                    lblMAKH.Text = dtgv.Rows[i][0].ToString();
                }
                if (lblMAKH.Text == "Code")
                {
                    lblMAKH.Text = "KH0";
                    int MA = Convert.ToInt16((lblMAKH.Text).Substring(2)) + 1;
                    lblMAKH.Text = "KH" + MA.ToString();
                }
                else
                {
                    int MA = Convert.ToInt16((lblMAKH.Text).Substring(2)) + 1;
                    lblMAKH.Text = "KH" + MA.ToString();
                }
            }
            catch (Exception)
            {
                lblMAKH.Text = "Code";
                string LayMaKH = @"Select MAKH
                                    From PHONG, KHACHHANG
                                    Where PHONG.MAPHONG=KHACHHANG.MAPHONG";

                DataTable dtgv = KetNoiCSDL.LoadCSDL(LayMaKH);
                for (int i = 0; i < dtgv.Rows.Count; i++)
                {
                    lblMAKH.Text = dtgv.Rows[i][0].ToString();
                }
                if (lblMAKH.Text == "Code")
                {
                    lblMAKH.Text = "KH0";
                    int MA = Convert.ToInt16((lblMAKH.Text).Substring(2)) + 1;
                    lblMAKH.Text = "KH" + MA.ToString();
                }
                else
                {
                    int MA = Convert.ToInt16((lblMAKH.Text).Substring(2)) + 1;
                    lblMAKH.Text = "KH" + MA.ToString();
                }
                MessageBox.Show("Thêm khách hàng mới nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAddNewOrder_Click(object sender, EventArgs e)
        {
            ThemDichVuKhach ListAddOrder = new ThemDichVuKhach();
            ListAddOrder.ShowDialog();
        }

        private void PhongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Phong ListPhong = new Phong();
            ListPhong.ShowDialog();
        }


        private void DichVuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DanhSachDichVu ListDichVu = new DanhSachDichVu();
            ListDichVu.ShowDialog();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTENKH.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập tên khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTENKH.Focus();
                }
                else if (dpkNAMSINH.Value.ToString() == "")
                {
                    MessageBox.Show("Bạn chưa chọn ngày sinh!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dpkNAMSINH.Focus();
                }
                else if (txtSDT.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập SĐT khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSDT.Focus();
                }

                else
                {
                    string sql = @"INSERT INTO KHACHHANG(MAKH,MAPHONG,TENKH,NAMSINH,SDT,NGAYDEN)
                           VALUES ('" + lblMAKH.Text + "','" + lblMAPHONG.Text + "',N'" + txtTENKH.Text + "', '" + dpkNAMSINH.Value.ToString() + "', '" + txtSDT.Text + "', '" + (DateTime.Now).ToString() + "')";
                    
                    int kq = KetNoiCSDL.Change(sql);
                    if (kq > 0)
                    {
                        MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Thất bại, xin kiểm tra lại thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }

                ////////////////////////REFESH
                cbxRoom.Text = "";
                txtGIAPHONG.Text = "";
                txtSDT.Text = "";
                txtTENKH.Text = "";
                cbxRoom.Focus();
                cbxRoom.Items.Clear();
                // đưa dữ liệu từ csdl load lên DataGV
                string LayDanhSachPhong = @"Select PHONG.MAPHONG,PHONG.TENPHONG,KHACHHANG.TENKH,KHACHHANG.NAMSINH,KHACHHANG.SDT,KHACHHANG.NGAYDEN
                                            From PHONG, KHACHHANG
                                            Where PHONG.MAPHONG=KHACHHANG.MAPHONG";

                DataTable dt = KetNoiCSDL.LoadCSDL(LayDanhSachPhong);
                dgvService.DataSource = dt;

                // đưa dữ liệu lên listbox ROOM trống khách

                string ChonRoom = @"Select * 
                                    from PHONG";
                DataTable Room = KetNoiCSDL.LoadCSDL(ChonRoom);

                //cbxService.DataSource = DichVu;

                cbxRoom.SelectedIndex = -1;

                for (int i = 0; i < Room.Rows.Count; i++)
                {
                    cbxRoom.Items.Add(Room.Rows[i][1].ToString());
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j][0].ToString() == Room.Rows[i][0].ToString())
                        {
                            cbxRoom.Items.Remove(dt.Rows[j][1].ToString());
                        }
                    }
                }
                //Tạo Mã KH ngẫu nhiên 

                string LayMaKH = @"Select MAKH
                                    From PHONG, KHACHHANG
                                    Where PHONG.MAPHONG=KHACHHANG.MAPHONG";

                DataTable dtgv = KetNoiCSDL.LoadCSDL(LayMaKH);
                for (int i = 0; i < dtgv.Rows.Count; i++)
                {
                    lblMAKH.Text = dtgv.Rows[i][0].ToString();
                }
                int MA = Convert.ToInt16((lblMAKH.Text).Substring(2)) + 1;
                lblMAKH.Text = "KH" + MA.ToString();
                ///////////////////////////////////////////////////////////////////////////
            }
            catch (Exception a)
            {
                MessageBox.Show("Hãy thêm tiếp khách hàng nữa nào!"+a, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // khi chọn giá trị trong combobox sẽ lấy ra mã phòng tương ứng để Add
                string LayMAPHONG = @"Select *
                                            From PHONG";
                DataTable dtgv = KetNoiCSDL.LoadCSDL(LayMAPHONG);
                for (int i = 0; i < dtgv.Rows.Count; i++)
                {
                    if (dtgv.Rows[i][1].ToString() == cbxRoom.Text)
                    {
                        lblMAPHONG.Text = dtgv.Rows[i][0].ToString();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    int Selected = Convert.ToInt32(dgvService.CurrentCell.RowIndex);

                    string LayMaKH = @"Select MAKH
                                    From PHONG, KHACHHANG
                                    Where PHONG.MAPHONG=KHACHHANG.MAPHONG AND KHACHHANG.TENKH=N'" + dgvService.Rows[Selected].Cells[2].Value.ToString() + "'";

                    DataTable dtgv = KetNoiCSDL.LoadCSDL(LayMaKH);

                    DialogResult dl;
                    dl = MessageBox.Show("Bạn có thật sự muốn xóa?", "Chú ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dl == DialogResult.Yes)
                    {
                        string sql = @"DELETE FROM SUDUNGDV 
                           WHERE MAKH = '" + dtgv.Rows[0][0].ToString() + "'";
                        KetNoiCSDL.LoadCSDL(sql);

                        string sql2 = @"DELETE FROM KHACHHANG 
                           WHERE MAKH = '" + dtgv.Rows[0][0].ToString() + "'";

                        int kq = KetNoiCSDL.Change(sql2);
                        if (kq > 0)
                        {
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Thất bại, xin kiểm tra lại thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Không có phòng nào cả!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ////////////////////////REFESH
                cbxRoom.Text = "";
                txtGIAPHONG.Text = "";
                txtSDT.Text = "";
                txtTENKH.Text = "";
                cbxRoom.Focus();
                cbxRoom.Items.Clear();
                // đưa dữ liệu từ csdl load lên DataGV
                string LayDanhSachPhong = @"Select PHONG.MAPHONG,PHONG.TENPHONG,KHACHHANG.TENKH,KHACHHANG.NAMSINH,KHACHHANG.SDT,KHACHHANG.NGAYDEN
                                            From PHONG, KHACHHANG
                                            Where PHONG.MAPHONG=KHACHHANG.MAPHONG";

                DataTable dt = KetNoiCSDL.LoadCSDL(LayDanhSachPhong);
                dgvService.DataSource = dt;

                // đưa dữ liệu lên listbox ROOM trống khách

                string ChonRoom = @"Select * 
                                    from PHONG";
                DataTable Room = KetNoiCSDL.LoadCSDL(ChonRoom);

                //cbxService.DataSource = DichVu;

                cbxRoom.SelectedIndex = -1;

                for (int i = 0; i < Room.Rows.Count; i++)
                {
                    cbxRoom.Items.Add(Room.Rows[i][1].ToString());
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (dt.Rows[j][0].ToString() == Room.Rows[i][0].ToString())
                        {
                            cbxRoom.Items.Remove(dt.Rows[j][1].ToString());
                        }
                    }
                }


                //Tạo Mã KH ngẫu nhiên
                string LayMa = @"Select MAKH
                                    From PHONG, KHACHHANG
                                    Where PHONG.MAPHONG=KHACHHANG.MAPHONG";

                DataTable dtgv1 = KetNoiCSDL.LoadCSDL(LayMa);
                for (int i = 0; i < dtgv1.Rows.Count; i++)
                {
                    lblMAKH.Text = dtgv1.Rows[i][0].ToString();
                }
                int MA = Convert.ToInt16((lblMAKH.Text).Substring(2)) + 1;
                lblMAKH.Text = "KH" + MA.ToString();
                ///////////////////////////////////////////////////////////////////////////
            }
            catch (Exception)
            {
                MessageBox.Show("No Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog.Document = PrintDocument;
            PrintPreviewDialog.ShowDialog();   
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument.Print();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dl;
            dl = MessageBox.Show("Bạn có thật sự muốn thoát khỏi phần mềm?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.Yes)
                Application.Exit();
        }

        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                if (txtExchange.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập tỷ giá!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtExchange.Focus();
                }
                else if (txtSoDem.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập số đêm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSoDem.Focus();
                }
                else if (txtGIAPHONG.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn phòng cần in hóa đơn trong bảng hiển thị!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Desigh Page Print
                    Bitmap bmp = Properties.Resources.Sleep_in_tent1;
                    Image newImage = bmp;
                    double TotalPay = 0;
                    double SoDem = Convert.ToDouble(txtSoDem.Text);
                    double DonGiaPhong = Convert.ToDouble(txtGIAPHONG.Text);
                    double GiaPhong = Convert.ToDouble(txtGIAPHONG.Text) * Convert.ToDouble(txtSoDem.Text);
                    string NgayDen = "";
                    string LayNgayDen = @"Select *
                                            From KHACHHANG";
                    DataTable LAYNGAY = KetNoiCSDL.LoadCSDL(LayNgayDen);
                    for (int i = 0; i < LAYNGAY.Rows.Count; i++)
                    {
                        if (LAYNGAY.Rows[i][0].ToString() == lblTempMAKH.Text)
                        {
                            NgayDen = LAYNGAY.Rows[i][5].ToString();
                        }
                    }
                    string Gach = "---------------------------------------------------------------------------------------------------------------------------------";
                    e.Graphics.DrawImage(newImage, 180, 20, newImage.Width, newImage.Height);
                    e.Graphics.DrawString("Room Name: " + cbxRoom.Text, new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(30, 280));
                    e.Graphics.DrawString("Client Name: " + txtTENKH.Text, new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(30, 310));
                    e.Graphics.DrawString("Check-in: " + NgayDen, new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(30, 340));
                    e.Graphics.DrawString("Check-out: " + DateTime.Now, new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(30, 370));
                   // ( Exchange Rate: " + txtExchange.Text + " ₫/$)

                    e.Graphics.DrawString(Gach, new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(30, 390));
                    e.Graphics.DrawString("Service Name", new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(60, 430));
                    e.Graphics.DrawString("Quantity", new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(260, 430));
                    e.Graphics.DrawString("Unit Price(₫)", new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(460, 430));
                    e.Graphics.DrawString("Amount(₫)", new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(660, 430));

                    e.Graphics.DrawString("Day", new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(40, 460));
                    e.Graphics.DrawString(SoDem.ToString("#,#"), new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(280, 460));
                    e.Graphics.DrawString(DonGiaPhong.ToString("#,#") + " ₫", new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(460, 460));
                    e.Graphics.DrawString(GiaPhong.ToString("#,#") + " ₫", new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(660, 460));

                    string Temp = @"Select DICHVU.TENDV,SUDUNGDV.SOLUONG,DICHVU.DONGIA,SUDUNGDV.GIADV
                            From DICHVU,SUDUNGDV
                            where DICHVU.MADV=SUDUNGDV.MADV and SUDUNGDV.MAKH = '" + lblTempMAKH.Text + "'";
                    DataTable dtgv = KetNoiCSDL.LoadCSDL(Temp);

                    int Location = 460;

                    // Dữ liệu từ sử dụng dịch vụ và dịch vụ ứng với phòng đó
                    for (int i = 0; i < dtgv.Rows.Count; i++)
                    {
                        double DonGiaDV = Convert.ToDouble(dtgv.Rows[i][2].ToString());
                        double GiaDV = Convert.ToDouble(dtgv.Rows[i][3].ToString());
                        double SoLuongDV = Convert.ToDouble(dtgv.Rows[i][1].ToString());
                        Location += 30;
                        e.Graphics.DrawString(dtgv.Rows[i][0].ToString(), new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(40, Location));
                        e.Graphics.DrawString(SoLuongDV.ToString("#,#"), new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(280, Location));
                        e.Graphics.DrawString(DonGiaDV.ToString("#,#") + " ₫", new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(460, Location));
                        e.Graphics.DrawString(GiaDV.ToString("#,#") + " ₫", new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(660, Location));
                        TotalPay += GiaDV;
                    }

                    //Tổng tiền 
                    TotalPay += GiaPhong;

                    e.Graphics.DrawString(Gach, new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(30, Location + 30));

                    //Quy đổi ra USD theo tỷ giá Exchange
                    double QuyDoi = Convert.ToDouble(txtExchange.Text);
                    e.Graphics.DrawString("Exchange Rate: " + QuyDoi.ToString("#,#") + " ₫/$", new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(30, Location + 60));
                    e.Graphics.DrawString("Total Pay: " + TotalPay.ToString("#,#") + " ₫" + " ≈ " + Math.Round((TotalPay / QuyDoi), 1).ToString("#,#.#") + " $ ", new Font("Time New Roman", 13, FontStyle.Regular), Brushes.Black, new Point(540, Location + 60));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvService_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                // Tạo con trỏ khi nhấp chọn thuộc tính trên DataGV
                int Selected = Convert.ToInt32(dgvService.CurrentCell.RowIndex);

                lblTempMAPHONG.Text = dgvService.Rows[Selected].Cells[0].Value.ToString();
                cbxRoom.Text = dgvService.Rows[Selected].Cells[1].Value.ToString();
                txtTENKH.Text = dgvService.Rows[Selected].Cells[2].Value.ToString();
                dpkNAMSINH.Text = dgvService.Rows[Selected].Cells[3].Value.ToString();
                txtSDT.Text = dgvService.Rows[Selected].Cells[4].Value.ToString();

                // Trả về Giá phòng và mã KH để truy suất trong previewPrint
                string Temp = @"Select PHONG.GIAPHONG
                            From PHONG
                            where PHONG.TENPHONG=N'" + dgvService.Rows[Selected].Cells[1].Value.ToString() + "'";
                DataTable dtgv = KetNoiCSDL.LoadCSDL(Temp);
                string Temp1 = @"Select KHACHHANG.MAKH
                            From KHACHHANG
                            where KHACHHANG.TENKH=N'" + dgvService.Rows[Selected].Cells[2].Value.ToString() + "'";
                DataTable dtgv1 = KetNoiCSDL.LoadCSDL(Temp1);

                // Đổ dữ liệu tại vị trí đang click trên DataGV lên các lable và textbox
                if (dtgv.Rows.Count != 0)
                {
                    txtGIAPHONG.Text = dtgv.Rows[0][0].ToString();
                    lblTempMAKH.Text = dtgv1.Rows[0][0].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // khóa textbox khi nhập kí tự chữ
        private void txtExchange_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void LienHeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrLienHe LienHe = new FrLienHe();
            LienHe.ShowDialog();
        }

    }
}
