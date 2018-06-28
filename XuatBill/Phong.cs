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
    public partial class Phong : Form
    {
        public Phong()
        {
            InitializeComponent();
        }

        private void FrPhong_Load(object sender, EventArgs e)
        {
            try
            {
                lblMAPHONG.Text = "Code";
                string LayDanhSachPhong = @"Select *
                                            From PHONG";

                DataTable dt = KetNoiCSDL.LoadCSDL(LayDanhSachPhong);
                dgvRoom.DataSource = dt;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lblMAPHONG.Text = dt.Rows[i][0].ToString();
                }
                if (lblMAPHONG.Text == "Code")
                {
                    lblMAPHONG.Text = "P0";
                    int MA = Convert.ToInt16((lblMAPHONG.Text).Substring(1)) + 1;
                    lblMAPHONG.Text = "P" + MA.ToString();
                }
                else
                {
                    int MA = Convert.ToInt16((lblMAPHONG.Text).Substring(1)) + 1;
                    lblMAPHONG.Text = "P" + MA.ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hãy thêm phòng đầu tiên cho khách sạn của bạn!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
            if (txtTENPHONG.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTENPHONG.Focus();
            }
            else if (txtGIAPHONG.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập giá phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGIAPHONG.Focus();
            }

            else
            {
                    string sql = @"INSERT INTO PHONG(MAPHONG,TENPHONG,GIAPHONG)
                           VALUES ('" + lblMAPHONG.Text + "',N'" + txtTENPHONG.Text + "', '" + txtGIAPHONG.Text + "')";

                    int kq = KetNoiCSDL.Change(sql);
                    if (kq > 0)
                    {
                        MessageBox.Show("Thêm thành công!", "Inform", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Thất bại, xin kiểm tra lại thông tin vừa nhập!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }          
            }
            ///////////////////REFESH
            string LayDanhSachPhong = @"Select *
                                            From PHONG";

            DataTable dt = KetNoiCSDL.LoadCSDL(LayDanhSachPhong);
            dgvRoom.DataSource = dt;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lblMAPHONG.Text = dt.Rows[i][0].ToString();
            }
            int MA = Convert.ToInt16((lblMAPHONG.Text).Substring(1)) + 1;
            lblMAPHONG.Text = "P" + MA.ToString();

            txtGIAPHONG.Text = "";
            txtTENPHONG.Text = "";

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có phòng nào cả!", "Thông báo", MessageBoxButtons.OK);
            }
            //////////////////////////////////////////
        }
        catch (Exception)
            {
                MessageBox.Show("Mời bạn nhập thông tin phòng muốn thêm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTENPHONG.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập tên phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTENPHONG.Focus();
                }
                else if (txtGIAPHONG.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập giá phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGIAPHONG.Focus();
                }
                else
                {
                    string sql = @"UPDATE PHONG SET GIAPHONG = '" + txtGIAPHONG.Text + "',TENPHONG =N'" + txtTENPHONG.Text + "' WHERE MAPHONG = '" + lblMAPHONG.Text + "'";

                    int kq = KetNoiCSDL.Change(sql);
                    if (kq > 0)
                    {
                        MessageBox.Show("Chỉnh sửa thành công!", "Inform", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Thất bại, xin kiểm tra lại thông tin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                ///////////////////REFESH
                string LayDanhSachPhong = @"Select *
                                            From PHONG";

                DataTable dt = KetNoiCSDL.LoadCSDL(LayDanhSachPhong);
                dgvRoom.DataSource = dt;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lblMAPHONG.Text = dt.Rows[i][0].ToString();
                }
                int MA = Convert.ToInt16((lblMAPHONG.Text).Substring(1)) + 1;
                lblMAPHONG.Text = "P" + MA.ToString();

                txtGIAPHONG.Text = "";
                txtTENPHONG.Text = "";

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Not Room", "Inform", MessageBoxButtons.OK);
                }
                //////////////////////////////////////////
            }
            catch (Exception)
            {
                MessageBox.Show("Hãy chọn phòng muốn sửa thông tin từ bảng hiển thị!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    int Selected = Convert.ToInt32(dgvRoom.CurrentCell.RowIndex);

                    string MAPHONG = dgvRoom.Rows[Selected].Cells[0].Value.ToString();

                    DialogResult dl;
                    dl = MessageBox.Show("Bạn có thật sự muốn xóa?", "Chú ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dl == DialogResult.Yes)
                    {

                        string sql2 = @"DELETE FROM KHACHHANG
                           WHERE MAPHONG = '" + MAPHONG + "'";
                        int kq2 = KetNoiCSDL.Change(sql2);

                        string sql = @"DELETE FROM PHONG 
                           WHERE MAPHONG = '" + MAPHONG + "'";
                        int kq = KetNoiCSDL.Change(sql);

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
                    MessageBox.Show("Hãy chọn một phòng để xóa từ bảng hiển thị!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ///////////////////REFESH
                string LayDanhSachPhong = @"Select *
                                            From PHONG";

                DataTable dt = KetNoiCSDL.LoadCSDL(LayDanhSachPhong);
                dgvRoom.DataSource = dt;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lblMAPHONG.Text = dt.Rows[i][0].ToString();
                }
                int MA = Convert.ToInt16((lblMAPHONG.Text).Substring(1)) + 1;
                lblMAPHONG.Text = "P" + MA.ToString();

                txtGIAPHONG.Text = "";
                txtTENPHONG.Text = "";

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có phòng nào cả!", "Thông báo", MessageBoxButtons.OK);
                }
                //////////////////////////////////////////
            }
            catch (Exception )
            {
                MessageBox.Show("Không có phòng nào để bạn xóa cả!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvRoom_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int Selected = Convert.ToInt32(dgvRoom.CurrentCell.RowIndex);
                lblMAPHONG.Text = dgvRoom.Rows[Selected].Cells[0].Value.ToString();
                txtTENPHONG.Text = dgvRoom.Rows[Selected].Cells[1].Value.ToString();
                txtGIAPHONG.Text = dgvRoom.Rows[Selected].Cells[2].Value.ToString();
                
            }
            catch (Exception a)
            {
                MessageBox.Show("Lỗi " + a, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtGIAPHONG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
