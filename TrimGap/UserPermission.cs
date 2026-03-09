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
                for (int i = 0; i < fram.UserData.Count; i++)
                {
                    string str = string.Empty;
                    

                    str = fram.UserData[i].Split(',')[0];
                    
                    cboUserCount.Items.Add(str);
                }

                comPerm.Items.Add(permissionEnum.op);
                comPerm.Items.Add(permissionEnum.eng);
                comPerm.Items.Add(permissionEnum.ad);

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
                    string strUser = cboUserCount.SelectedItem.ToString() ;
                    string strPas = string.Empty;
                    int ua = 0;
                    List<string> usData = new List<string>();

                    usData = fram.UserData.FindAll(x => x.Contains(strUser));
                    if (usData.Count > 0)
                    {
                        strPas = usData[0].Split(',')[1].ToString();
                        ua = Convert.ToInt32(usData[0].Split(',')[2]);

                        if (txtPassword.Text == strPas)
                        {
                            switch (ua)
                            {
                                case 0:
                                    sram.UserAuthority = permissionEnum.op;
                                    break;
                                case 1:
                                    sram.UserAuthority = permissionEnum.eng;
                                    break;
                                case 2:
                                    sram.UserAuthority = permissionEnum.ad;
                                    break;
                            }

                            this.Close();
                            DateTime dt = DateTime.Now;
                            Flag.ChangeAuthority = true; // 更改權限後，在timer照權限刷一次狀態
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

        private void btn_Add_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            btnChange.Enabled = false;
            btn_Edit.Enabled = false;
            cboUserCount.Enabled = false;
            txtPassword.Enabled = false;

            txtUser.Text = "";
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            btnChange.Enabled = true;
            btn_Add.Enabled = true;
            btn_Edit.Enabled = true;
            cboUserCount.Enabled = true;
            txtPassword.Enabled = true;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            btnChange.Enabled = true;
            btn_Add.Enabled = true;
            btn_Edit.Enabled = true;
            cboUserCount.Enabled = true;
            txtPassword.Enabled = true;

            if (txtUser.Text == "")
            {
                DialogResult tmpResult = MessageBox.Show("使用者沒有輸入");

            }

            if (txtPass.Text == "")
            {
                DialogResult tmpResult = MessageBox.Show("密碼沒有輸入");
            }

            if (txtUser.Text != "" && txtPass.Text != "")
            {
                saveData();
            }

        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            btnChange.Enabled = false;
            btn_Add.Enabled = false;
            cboUserCount.Enabled = false;
            txtPassword.Enabled = false;
            txtUser.Enabled = false;

            txtUser.Text = cboUserCount.SelectedItem.ToString();
        }

        private void saveData()
        {
            int q = 0;

            switch (comPerm.SelectedItem.ToString())
            {
                case "op":
                    q = 0;
                    break;

                case "eng":
                    q = 1;
                    break; ;

                case "ad":
                    q = 2;
                    break;
            }

            fram.Up_UserData = txtUser.Text + "," + txtPass.Text + "," + q.ToString();
            ParamFile.saveUsData("UserPermission", txtUser.Text);
            MessageBox.Show("使用者:" + txtUser.Text + "密碼修改成功");
            ParamFile.initparam();
            this.Close();
        }
    }
}