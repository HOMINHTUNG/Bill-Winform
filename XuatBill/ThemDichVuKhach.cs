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
    public partial class ThemDichVuKhach : Form
    {
        public ThemDichVuKhach()
        {
            InitializeComponent();
        }

        private void FrAddOrder_Load(object sender, EventArgs e)
        {
            try
            {
                string ChonMAPHONG = @"Select * 
                                    from PHONG,KHACHHANG
                                    where PHONG.MAPHONG=KHACHHANG.MAPHONG ";

                cbxMAPHONG.DataSource = KetNoiCSDL.LoadCSDL(ChonMAPHONG);
                cbxMAPHONG.DisplayMember = "MAPHONG";
                cbxMAPHONG.ValueMember = "MAPHONG";
                cbxMAPHONG.SelectedIndex = -1;
            }
            catch(Exception)
            {
                MessageBox.Show("Hãy thêm dịch vụ đầu tiên cho khách đi nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cbxMAPHONG_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                cbxService.Items.Clear();
                txtPrice.Text = "";
                txtQuantity.Text = "";
                string ClientName = @"Select PHONG.TENPHONG,KHACHHANG.TENKH,KHACHHANG.MAKH
                                    From PHONG,KHACHHANG
                                    Where PHONG.MAPHONG = KHACHHANG.MAPHONG and PHONG.MAPHONG='" + cbxMAPHONG.SelectedValue.ToString() + "'";
                DataTable dtRoom = KetNoiCSDL.LoadCSDL(ClientName);

                lblRoom.Text = dtRoom.Rows[0][0].ToString();
                txtClientName.Text = dtRoom.Rows[0][1].ToString();
                lblMAKH.Text = dtRoom.Rows[0][2].ToString();

                string ListServiceOld = @"Select DICHVU.TENDV,SUDUNGDV.SOLUONG,SUDUNGDV.GIADV
                                    From DICHVU,SUDUNGDV
                                    Where DICHVU.MADV = SUDUNGDV.MADV and SUDUNGDV.MAKH='" + lblMAKH.Text + "'";
                DataTable dtgv = KetNoiCSDL.LoadCSDL(ListServiceOld);
                dgvService.DataSource = dtgv;
                if (dtgv.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dịch vụ nào cả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Đưa dữ liệu list service chưa được chọn vào combobox khi chọn phòng

                string ChonDICHVU = @"Select * 
                                    from DICHVU";
                DataTable DichVu = KetNoiCSDL.LoadCSDL(ChonDICHVU);
                //cbxService.DataSource = DichVu;

                cbxService.SelectedIndex = -1;

                for (int i = 0; i < DichVu.Rows.Count; i++)
                {
                    cbxService.Items.Add(DichVu.Rows[i][1].ToString());

                    for (int j = 0; j < dtgv.Rows.Count; j++)
                    {
                        if (dtgv.Rows[j][0].ToString() == DichVu.Rows[i][1].ToString())
                        {
                            cbxService.Items.Remove(dtgv.Rows[j][0].ToString());
                        }
                    }
                }
                cbxService.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxService.SelectedIndex == -1)
                {
                    MessageBox.Show("Bạn chưa chọn dịch vụ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbxService.Focus();
                }
                else if (cbxMAPHONG.SelectedIndex == -1)
                {
                    MessageBox.Show("Bạn chưa chọn phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbxMAPHONG.Focus();
                }
                else if (txtQuantity.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập số lượng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQuantity.Focus();
                }
                else if (lblMADV.Text == "")
                {
                    MessageBox.Show("Bạn chưa chọn dịch vụ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string sql = @"INSERT INTO SUDUNGDV(MAKH,MADV,SOLUONG,GIADV)
                           VALUES ('" + lblMAKH.Text + "','" + lblMADV.Text + "', '" + txtQuantity.Text + "', '" + Convert.ToInt32(txtQuantity.Text) * Convert.ToInt32(txtPrice.Text) + "')";

                    int kq = KetNoiCSDL.Change(sql);
                    if (kq > 0)
                    {
                        MessageBox.Show("Thêm thành công!", "Inform", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Thất bại, xin kiểm tra lại thông tin vừa nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                //////////////////REFESH

                string ListServiceOld = @"Select DICHVU.TENDV,SUDUNGDV.SOLUONG,SUDUNGDV.GIADV
                                    From DICHVU,SUDUNGDV
                                    Where DICHVU.MADV = SUDUNGDV.MADV and SUDUNGDV.MAKH='" + lblMAKH.Text + "'";
                DataTable dtgv = KetNoiCSDL.LoadCSDL(ListServiceOld);
                dgvService.DataSource = dtgv;
                if (dtgv.Rows.Count == 0)
                {
                    MessageBox.Show("Bạn chưa nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK);
                }
                cbxService.Text = "";
                txtPrice.Text = "";
                txtQuantity.Text = "";
                cbxService.Items.Clear();
                ////////////////////////////////////////////
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxService_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                string temp = @"Select DICHVU.DONGIA,DICHVU.MADV
                                 From DICHVU
                                 Where DICHVU.MADV='" + cbxService.SelectedValue.ToString() + "'";
                DataTable dttemp = KetNoiCSDL.LoadCSDL(temp);

                txtPrice.Text = dttemp.Rows[0][0].ToString();
                lblMADV.Text = dttemp.Rows[0][1].ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvService_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                // Tạo con trỏ khi nhấp chọn thuộc tính trên DataGV
                int Selected = Convert.ToInt32(dgvService.CurrentCell.RowIndex);

                string Temp = @"Select SUDUNGDV.MAKH,SUDUNGDV.MADV
                                    From DICHVU,SUDUNGDV
                                    Where DICHVU.TENDV=N'" + dgvService.Rows[Selected].Cells[0].Value.ToString() + "' AND DICHVU.MADV = SUDUNGDV.MADV";
                DataTable dtgv = KetNoiCSDL.LoadCSDL(Temp);
                // Đổ dữ liệu tại vị trí đang click trên DataGV lên các lable và textbox
                if (dtgv.Rows.Count != 0)
                {
                    lblTempMAKH.Text = dtgv.Rows[0][0].ToString();
                    lblTempMADV.Text = dtgv.Rows[0][1].ToString();

                    // var ViTri = this.cbxService.GetItemText(this.cbxService.FindStringExact(dgvService.Rows[Selected].Cells[0].Value.ToString()));
                    // cbxService.SelectedIndex = Convert.ToInt16(ViTri);
                    cbxService.Text = dgvService.Rows[Selected].Cells[0].Value.ToString();

                    txtQuantity.Text = dgvService.Rows[Selected].Cells[1].Value.ToString();
                    txtPrice.Text = (Convert.ToInt32(dgvService.Rows[Selected].Cells[2].Value.ToString()) / Convert.ToInt32(dgvService.Rows[Selected].Cells[1].Value.ToString())).ToString();
;
                }
                else
                {
                    txtQuantity.Text = "";
                    txtPrice.Text = "";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = @"UPDATE SUDUNGDV SET SOLUONG = '" + txtQuantity.Text + "', GIADV ='" + Convert.ToInt32(txtQuantity.Text) * Convert.ToInt32(txtPrice.Text) + "' WHERE MADV = '" + lblTempMADV.Text + "'";

                int kq = KetNoiCSDL.Change(sql);
                if (kq > 0)
                {
                    MessageBox.Show("Chỉnh sửa thành công!", "Inform", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Thất bại, xin kiểm tra lại thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //////////////////REFESH
                string ListServiceOld = @"Select DICHVU.TENDV,SUDUNGDV.SOLUONG,SUDUNGDV.GIADV
                                    From DICHVU,SUDUNGDV
                                    Where DICHVU.MADV = SUDUNGDV.MADV and SUDUNGDV.MAKH='" + lblMAKH.Text + "'";
                DataTable dtgv = KetNoiCSDL.LoadCSDL(ListServiceOld);
                dgvService.DataSource = dtgv;
                if (dtgv.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dịch vụ nào cả!", "Inform", MessageBoxButtons.OK);
                }
                ////////////////////////////////////////////
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    int Selected = Convert.ToInt32(dgvService.CurrentCell.RowIndex);
                    DialogResult dl;
                    dl = MessageBox.Show("Bạn có thật sự muốn xóa?", "Chú ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dl == DialogResult.Yes)
                    {
                        string sql = @"DELETE FROM SUDUNGDV 
                           WHERE MAKH = '" + lblTempMAKH.Text + "' AND MADV = '" + lblTempMADV.Text + "'";

                        int kq = KetNoiCSDL.Change(sql);
                        if (kq > 0)
                        {
                            MessageBox.Show("Xóa thành công!", "Inform", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Thất bại, xin kiểm tra lại thông tin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Không có dịch vụ nào cả", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //////////////////REFESH
                string ListServiceOld = @"Select DICHVU.TENDV,SUDUNGDV.SOLUONG,SUDUNGDV.GIADV
                                    From DICHVU,SUDUNGDV
                                    Where DICHVU.MADV = SUDUNGDV.MADV and SUDUNGDV.MAKH='" + lblMAKH.Text + "'";
                DataTable dtgv = KetNoiCSDL.LoadCSDL(ListServiceOld);
                dgvService.DataSource = dtgv;
                if (dtgv.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dịch vụ nào cả", "Thông báo", MessageBoxButtons.OK);
                }
                ////////////////////////////////////////////
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxService_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtQuantity.Text = "";
                string LayPrice = @"Select *
                                    From DICHVU";
                DataTable dtgv = KetNoiCSDL.LoadCSDL(LayPrice);
                for (int i = 0; i < dtgv.Rows.Count; i++)
                {
                    if (dtgv.Rows[i][1].ToString() == cbxService.Text)
                    {
                        txtPrice.Text = dtgv.Rows[i][2].ToString();
                        lblMADV.Text = dtgv.Rows[i][0].ToString();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
