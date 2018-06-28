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
    public partial class DanhSachDichVu : Form
    {
        public DanhSachDichVu()
        {
            InitializeComponent();
        }

        private void FrDichVu_Load(object sender, EventArgs e)
        {
            try
            {
                lblMADV.Text = "Code";
                string LayDanhSachPhong = @"Select *
                                            From DICHVU";

                DataTable dt = KetNoiCSDL.LoadCSDL(LayDanhSachPhong);
                dgvService.DataSource = dt;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lblMADV.Text = dt.Rows[i][0].ToString();
                }

                if (lblMADV.Text == "Code")
                {
                    lblMADV.Text = "DV0";
                    int MA = Convert.ToInt16((lblMADV.Text).Substring(2)) + 1;
                    lblMADV.Text = "DV" + MA.ToString();
                }
                else
                {
                    int MA = Convert.ToInt16((lblMADV.Text).Substring(2)) + 1;
                    lblMADV.Text = "DV" + MA.ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hãy thêm dịch vụ đầu tiên cho khách sạn của bạn!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTENDV.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập tên dịch vụ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTENDV.Focus();
                }
                else if (txtDONGIA.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập giá dịch vụ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDONGIA.Focus();
                }

                else
                {
                            string sql = @"INSERT INTO DICHVU(MADV,TENDV,DONGIA)
                           VALUES ('" + lblMADV.Text + "',N'" + txtTENDV.Text + "', '" + txtDONGIA.Text + "')";

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

                //////////////////////REFESH
                string LayDanhSachPhong = @"Select *
                                            From DICHVU";

                DataTable dt = KetNoiCSDL.LoadCSDL(LayDanhSachPhong);
                dgvService.DataSource = dt;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lblMADV.Text = dt.Rows[i][0].ToString();
                }
                int MA = Convert.ToInt16((lblMADV.Text).Substring(2)) + 1;
                lblMADV.Text = "DV" + MA.ToString();

                txtDONGIA.Text = "";
                txtTENDV.Text = "";
                txtTENDV.Focus();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Bạn chưa nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                ///////////////////////////////////
            }
            catch (Exception)
            {
                MessageBox.Show("Bạn đã thêm dịch vụ này rồi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTENDV.Text == "")
                {
                    MessageBox.Show("Hãy chọn dịch vụ cần sửa ở bảng hiển thị!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTENDV.Focus();
                }
                else if (txtDONGIA.Text == "")
                {
                    MessageBox.Show("Hãy chọn dịch vụ cần sửa ở bảng hiển thị!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDONGIA.Focus();
                }
                else
                {
                    string sql = @"UPDATE DICHVU SET DONGIA = '" + txtDONGIA.Text + "',TENDV =N'" + txtTENDV.Text + "' WHERE MADV = '" + lblMADV.Text + "'";

                    int kq = KetNoiCSDL.Change(sql);
                    if (kq > 0)
                    {
                        MessageBox.Show("Chỉnh sửa thành công!", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Thất bại, xin kiểm tra lại thông tin dịch vụ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //////////////////////REFESH
                string LayDanhSachPhong = @"Select *
                                            From DICHVU";

                DataTable dt = KetNoiCSDL.LoadCSDL(LayDanhSachPhong);
                dgvService.DataSource = dt;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lblMADV.Text = dt.Rows[i][0].ToString();
                }
                int MA = Convert.ToInt16((lblMADV.Text).Substring(2)) + 1;
                lblMADV.Text = "DV" + MA.ToString();

                txtDONGIA.Text = "";
                txtTENDV.Text = "";
                txtTENDV.Focus();

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dịch vụ nào để bạn chọn cả, hãy thêm dịch vụ đi nào!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ///////////////////////////////////
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

                    string MADV = dgvService.Rows[Selected].Cells[0].Value.ToString();

                    DialogResult dl;
                    dl = MessageBox.Show("Bạn có thật sự muốn xóa?", "Chú ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dl == DialogResult.Yes)
                    {

                        string sql1 = @"DELETE FROM SUDUNGDV 
                           WHERE MADV = '" + MADV + "'";
                        int kq1 = KetNoiCSDL.Change(sql1);

                        string sql2 = @"DELETE FROM DICHVU
                           WHERE MADV = '" + MADV + "'";
                        int kq2 = KetNoiCSDL.Change(sql2);

                        if (kq2 > 0)
                        {
                            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Thất bại, xin kiểm tra lại thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hãy chọn dịch vụ muốn xóa ở bảng hiển thị!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //////////////////////REFESH
                string LayDanhSachPhong = @"Select *
                                            From DICHVU";

                DataTable dt = KetNoiCSDL.LoadCSDL(LayDanhSachPhong);
                dgvService.DataSource = dt;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lblMADV.Text = dt.Rows[i][0].ToString();
                }
                int MA = Convert.ToInt16((lblMADV.Text).Substring(2)) + 1;
                lblMADV.Text = "DV" + MA.ToString();

                txtDONGIA.Text = "";
                txtTENDV.Text = "";

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dịch vụ nào để bạn chọn cả, hãy thêm dịch vụ đi nào!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ///////////////////////////////////
            }
            catch (Exception)
            {
                MessageBox.Show("No Information", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dgvService_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int Selected = Convert.ToInt32(dgvService.CurrentCell.RowIndex);
                lblMADV.Text = dgvService.Rows[Selected].Cells[0].Value.ToString();
                txtTENDV.Text = dgvService.Rows[Selected].Cells[1].Value.ToString();
                txtDONGIA.Text = dgvService.Rows[Selected].Cells[2].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("No Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
