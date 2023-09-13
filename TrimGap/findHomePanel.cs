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
    public partial class findHomePanel : Form
    {
        int count = 1;
        public findHomePanel()
        {
            InitializeComponent();
        }

        private void findHomePanel_Load(object sender, EventArgs e)
        {
            //if (fram.sys_LanguageSelect == 0)
                label1.Text = "回原點動作進行中.";

            //if (fram.sys_LanguageSelect == 1)
                //label1.Text = "Find home.";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           // if (fram.sys_LanguageSelect == 0)
            //{
                switch (count)
                {
                    case 1:
                        label1.Text = "回原點動作進行中.";
                        pictureBox1.Visible = true;
                        count++;
                        break;

                    case 2:
                        label1.Text = "回原點動作進行中..";
                        pictureBox1.Visible = false;
                        count++;
                        break;

                    case 3:
                        label1.Text = "回原點動作進行中...";
                        pictureBox1.Visible = true;
                        count++;
                        break;

                    case 4:
                        label1.Text = "回原點動作進行中....";
                        pictureBox1.Visible = false;
                        count = 1;
                        break;

                    default:
                        break;
                }
            //}
            /*
            if (fram.sys_LanguageSelect == 1)
            {
                switch (count)
                {
                    case 1:
                        label1.Text = "Find home.";
                        pictureBox1.Visible = true;
                        count++;
                        break;

                    case 2:
                        label1.Text = "Find home..";
                        pictureBox1.Visible = false;
                        count++;
                        break;

                    case 3:
                        label1.Text = "Find home...";
                        pictureBox1.Visible = true;
                        count++;
                        break;

                    case 4:
                        label1.Text = "Find home....";
                        pictureBox1.Visible = false;
                        count = 1;
                        break;

                    default:
                        break;
                }
            }
            */
        }

        private void findHomePanel_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.F4) && (e.Alt == true))  //遮蔽ALT+F4
            {
                e.Handled = true;
            }
        }
    }
}
