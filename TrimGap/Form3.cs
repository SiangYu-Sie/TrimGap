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
    public partial class Form3 : Form
    {
        public Form3(string strItem)
        {
            InitializeComponent();
            OpenForm(strItem);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
        }

        private void OpenForm(string strItem)
        {
            switch (strItem)
            {
                case "parameter":
                    this.panel1.Controls.Clear();
                    this.panel1.Width = 981;
                    this.panel1.Height = 824;
                    ParaForm para = new ParaForm();
                    para.TopLevel = false;
                    para.FormBorderStyle = FormBorderStyle.None;
                    para.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.panel1.Controls.Add(para);
                    para.Show();
                    break;

                case "UserPermission":
                    this.panel1.Controls.Clear();
                    this.panel1.Width = 473;
                    this.panel1.Height = 364;
                    UserPermission user = new UserPermission();
                    user.TopLevel = false;
                    user.FormBorderStyle = FormBorderStyle.None;
                    user.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.panel1.Controls.Add(user);
                    user.Show();
                    break;

                //case "SECS":
                //    Common.SecsgemForm.Show();
                //    break;

                //case "IO":
                //    Common.io.ShowUI();
                //    break;

                case "Recipe":
                    this.panel1.Controls.Clear();
                    this.panel1.Width = 926;
                    this.panel1.Height = 800;
                    RecipeManagement recipe = new RecipeManagement();
                    recipe.TopLevel = false;
                    recipe.FormBorderStyle = FormBorderStyle.None;
                    recipe.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.panel1.Controls.Add(recipe);
                    recipe.Show();
                    break;

                case "EFEM":
                    this.panel1.Controls.Clear();
                    this.panel1.Width = 968;
                    this.panel1.Height = 551;
                    EFEMControl efem = new EFEMControl();
                    efem.TopLevel = false;
                    efem.FormBorderStyle = FormBorderStyle.None;
                    efem.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.panel1.Controls.Add(efem);
                    efem.Show();
                    break;

                case "Motion":
                    this.panel1.Controls.Clear();
                    this.panel1.Width = 1394;
                    this.panel1.Height = 793;
                    MotionControl motion = new MotionControl(fram.m_MotionType);
                    motion.TopLevel = false;
                    motion.FormBorderStyle = FormBorderStyle.None;
                    motion.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.panel1.Controls.Add(motion);
                    motion.Show();
                    break;

                case "SignalAnalysis":
                    this.panel1.Controls.Clear();
                    this.panel1.Width = 1394;
                    this.panel1.Height = 793;
                    SignalAnalysisForm SignalForm = new SignalAnalysisForm();
                    SignalForm.TopLevel = false;
                    SignalForm.FormBorderStyle = FormBorderStyle.None;
                    SignalForm.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.panel1.Controls.Add(SignalForm);
                    SignalForm.Show();
                    break;

                case "TTV":
                    Common.SF3.ShowUI();
                    break;

                case "CCD":
                    this.panel1.Controls.Clear();
                    this.panel1.Width = 1274;
                    this.panel1.Height = 587;
                    CCDTest ccd = new CCDTest();
                    ccd.TopLevel = false;
                    ccd.FormBorderStyle = FormBorderStyle.None;
                    ccd.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.panel1.Controls.Add(ccd);
                    ccd.Show();
                    break;

                case "PT":
                    Common.PTForm.Show();
                    break;

                default:
                    break;

            }
        }
    }
}
