#define TEST

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrimGap
{
    public partial class UserPermission : Form
    {
        public UserPermission()
        {
            InitializeComponent();
        }

        private void UserPermission_Load(object sender, EventArgs e)
        {
            try
            {
                cboUserCount.Items.Add("Operator");
                cboUserCount.Items.Add("Engineer");
                cboUserCount.Items.Add("Administrator");
                cboUserCount.SelectedIndex = 0;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "frmUserPermission_Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKeyBoard_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("" + System.Environment.SystemDirectory + "/osk.exe");
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text != "")
                {
                    if (cboUserCount.SelectedIndex == 0 && txtPassword.Text == fram.op_password)
                    {
                        sram.UserAuthority = permissionEnum.op;

                        this.Close();
                        //this.Dispose();
                        DateTime dt = DateTime.Now;
                        // form1_ref.insertEvent(20007, "Administrator");
                        //ErrorSystem.addMsg(11, "Op");
                        Flag.ChangeAuthority = true; // 更改權限後，在timer照權限刷一次狀態
                    }
                    else if (cboUserCount.SelectedIndex == 1 && txtPassword.Text == fram.eng_password)
                    {
                        sram.UserAuthority = permissionEnum.eng;

                        this.Close();
                        //this.Dispose();
                        DateTime dt = DateTime.Now;
                        // form1_ref.insertEvent(20007, "Administrator");
                        //ErrorSystem.addMsg(11, "Eng");
                        Flag.ChangeAuthority = true; // 更改權限後，在timer照權限刷一次狀態
                    }
                    else if (cboUserCount.SelectedIndex == 2 && txtPassword.Text == fram.ad_password)
                    {
                        sram.UserAuthority = permissionEnum.ad;

                        this.Close();
                        //this.Dispose();
                        DateTime dt = DateTime.Now;
                        // form1_ref.insertEvent(20007, "Administrator");
                        //ErrorSystem.addMsg(11, "Ad");
                        Flag.ChangeAuthority = true; // 更改權限後，在timer照權限刷一次狀態
                    }
                    else
                    {
                        //DialogResult tmpResult = MessageBox.Show("密碼錯誤！", "更改使用者權現錯誤 視窗", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        DialogResult tmpResult = MessageBox.Show("密碼錯誤！", "更改使用者權現錯誤 視窗", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        if (tmpResult == DialogResult.Retry)
                        {
                            txtPassword.Focus();
                            //this.Dispose();
                        }
                        else if (tmpResult == DialogResult.Cancel)
                        {
                            this.Close();
                        }
                    }
                }
                else
                {
                    DialogResult tmpResult = MessageBox.Show("沒有輸入密碼", "更改始用者權限錯誤視窗", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
                    if (tmpResult == DialogResult.Retry)
                    {
                        txtPassword.Focus();
                        //this.Dispose();
                    }
                    else if (tmpResult == DialogResult.Cancel)
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "btnChange_Click Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //txtPassword.Focus();
                btnChange_Click(null, null);
            }
        }

        private void ChangePasseord()
        {
            switch (sram.UserAuthority)
            {
                case permissionEnum.op:
                    fram.op_password = txtPasswordChange.Text;
                    //ErrorSystem.addMsg(11, "Op 修改密碼成功");
                    break;

                case permissionEnum.eng:
                    fram.eng_password = txtPasswordChange.Text;
                    //ErrorSystem.addMsg(11, "Eng 修改密碼成功");
                    break;

                case permissionEnum.ad:
                    fram.ad_password = txtPasswordChange.Text;
                    //ErrorSystem.addMsg(11, "Ad 修改密碼成功");
                    break;

                default:
                    break;
            }

            ParamFile.saveparam("UserPermission");
            MessageBox.Show("使用者:" + (permissionEnum)sram.UserAuthority + "密碼修改成功");
            this.Close();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if ((int)sram.UserAuthority == cboUserCount.SelectedIndex)
            {
                // 先檢查原密碼
                if (cboUserCount.SelectedIndex == 0 && txtPassword.Text == fram.op_password)
                {
                    if (txtPasswordChange.Text != "" && txtPassword.Text != txtPasswordChange.Text)
                    { ChangePasseord(); }
                    else if (txtPassword.Text == txtPasswordChange.Text)
                    {
                        DialogResult tmpResult = MessageBox.Show("新密碼與舊密碼相同", "更改始用者密碼錯誤視窗", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
                        txtPasswordChange.Focus();
                        txtPasswordChange.Text = "";
                    }
                    else if (txtPasswordChange.Text == "")
                    {
                        DialogResult tmpResult = MessageBox.Show("新密碼沒有輸入", "更改始用者密碼錯誤視窗", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
                        txtPasswordChange.Focus();
                        txtPasswordChange.Text = "";
                    }
                }
                else if (cboUserCount.SelectedIndex == 1 && txtPassword.Text == fram.eng_password)
                {
                    if (txtPasswordChange.Text != "" && txtPassword.Text != txtPasswordChange.Text)
                    { ChangePasseord(); }
                    else if (txtPassword.Text == txtPasswordChange.Text)
                    {
                        DialogResult tmpResult = MessageBox.Show("新密碼與舊密碼相同", "更改始用者密碼錯誤視窗", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
                        txtPasswordChange.Focus();
                        txtPasswordChange.Text = "";
                    }
                    else if (txtPasswordChange.Text == "")
                    {
                        DialogResult tmpResult = MessageBox.Show("新密碼沒有輸入", "更改始用者密碼錯誤視窗", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
                        txtPasswordChange.Focus();
                        txtPasswordChange.Text = "";
                    }
                }
                else if (cboUserCount.SelectedIndex == 2 && txtPassword.Text == fram.ad_password)
                {
                    if (txtPasswordChange.Text != "" && txtPassword.Text != txtPasswordChange.Text)
                    { ChangePasseord(); }
                    else if (txtPassword.Text == txtPasswordChange.Text)
                    {
                        DialogResult tmpResult = MessageBox.Show("新密碼與舊密碼相同", "更改始用者密碼錯誤視窗", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
                        txtPasswordChange.Focus();
                        txtPasswordChange.Text = "";
                    }
                    else if (txtPasswordChange.Text == "")
                    {
                        DialogResult tmpResult = MessageBox.Show("新密碼沒有輸入", "更改始用者密碼錯誤視窗", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
                        txtPasswordChange.Focus();
                        txtPasswordChange.Text = "";
                    }
                }
                else
                {
                    DialogResult tmpResult = MessageBox.Show("原密碼錯誤", "更改始用者密碼錯誤視窗", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
                    if (tmpResult == DialogResult.Retry)
                    {
                        txtPasswordChange.Focus();
                        txtPasswordChange.Text = "";
                        //this.Dispose();
                    }
                }
            }
            else
            {
                MessageBox.Show("請先更換使用者再修改密碼");
            }
        }
    }
}