using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TrimGap
{
    public partial class RecipeManagement : Form
    {
        private CheckBox[] cb_Slot = new CheckBox[25];
        private TTVScanPattern ttvPatternForm;

        public RecipeManagement()
        {
            InitializeComponent();
            if (sram.UserAuthority == permissionEnum.ad)
            {
                btn_Browse.Visible = true;
            }
            else
            {
                btn_Browse.Visible = false;
            }
            if (!Directory.Exists(fram.Recipe.Path))
            {
                Directory.CreateDirectory(fram.Recipe.Path);
                btn_CreateNew_Click(null, null);
            }
            if (fram.m_MachineType == 0 || fram.m_MachineType == 2) // AP6
            {
                OffsetInit();
                gB_TTV.Visible = false;
            }
            else if (fram.m_MachineType == 1) // 0:AP6, 1:N2
            {
                for (int i = 0; i < Common.SF3.list_RecipesIDName.Count; i++)
                {
                    cb_SF3RecipeName.Items.Add(Common.SF3.list_RecipesIDName[i][1].ToString());
                }
            }

            tb_RecipeInitPath.Text = fram.Recipe.Path;
            tb_RecipeSelect.Text = fram.Recipe.FilenameSelect;
            tb_RecipeName.Text = fram.Recipe.Filename_LP1;
            tb_RecipeName2.Text = fram.Recipe.Filename_LP2;
            folderBrowserDialog1.SelectedPath = fram.Recipe.Path;
            string str;
            int cbnum;
            bool rtn;
            foreach (Control ctrl in gbRecipe.Controls)
            {
                if (ctrl is CheckBox)
                {
                    str = ctrl.Name.Substring(8, ctrl.Name.Length - 8);
                    rtn = int.TryParse(str, out cbnum);
                    //cbnum = Convert.ToInt16(str) - 1;
                    if (rtn)
                    {
                        cbnum = cbnum - 1;
                        cb_Slot[cbnum] = (CheckBox)ctrl;
                    }
                }
            }
            treeView_Recipe.Nodes.Add(fram.Recipe.Path);
            System.IO.FileInfo fileName;
            string filename2;
            foreach (string fname in System.IO.Directory.GetFiles(fram.Recipe.Path))
            {
                fileName = new System.IO.FileInfo(fname);//取得完整檔名(含副檔名)
                filename2 = fileName.ToString();
                filename2 = System.IO.Path.GetFileNameWithoutExtension(filename2);
                treeView_Recipe.Nodes[0].Nodes.Add(filename2);
            }
            treeView_Recipe.ExpandAll();

            //cb_selectAll.Checked = true;
            //rbtn_Trim2step.Checked = true;
            //rbtn_Rotate8.Checked = true;

            treeView_Recipe.SelectedNode = treeView_Recipe.Nodes[0].LastNode;
            foreach (TreeNode t in treeView_Recipe.Nodes[0].Nodes)
            {
                if(t.Text == fram.Recipe.FilenameSelect)
                {
                    treeView_Recipe.SelectedNode = t;
                    break;
                }
            }
            
            //treeView_Recipe.SelectedNode.Text = fram.Recipe.FilenameSelect;
            if (treeView_Recipe.SelectedNode.Text == fram.Recipe.FilenameSelect)
            {
                ParamFile.ReadRcpini(fram.Recipe.Path, "Recipe");
                for (int i = 0; i < 25; i++)
                {
                    cb_Slot[i].Checked = fram.Recipe.Slot[i] == 1 ? true : false;
                }
                if (fram.Recipe.Rotate_Count == 4)
                {
                    rbtn_Rotate4.Checked = true;
                }
                else
                {
                    rbtn_Rotate8.Checked = true;
                }
                if (fram.Recipe.Type == 0)
                {
                    rbtn_BlueTape.Checked = true;
                }
                else if (fram.Recipe.Type == 1)
                {
                    rbtn_Trim1step.Checked = true;
                }
                else if(fram.Recipe.Type == 3)
                {
                    rbtn_Trim2step2nd.Checked = true;
                }
                else if (fram.Recipe.Type == 2)
                {
                    rbtn_Trim2step.Checked = true;
                }
                else if (fram.Recipe.Type == 4)
                {
                    rbtn_Trim2step3rd.Checked = true;
                }
                else if (fram.Recipe.Type == 5)
                {
                    rbtn_CCD.Checked = true;
                }
                else if (fram.Recipe.Type == 6)
                {
                    rbtn_Trim1step2nd.Checked = true;
                }
                else if (fram.Recipe.Type == 7)
                {
                    rbtn_Trim1step3rd.Checked = true;
                }
                else if (fram.Recipe.Type == 8)
                {
                    rbtn_TTV.Checked = true;
                }
                cb_Offset.SelectedIndex = fram.Recipe.OffsetType;
                numericUpDown1.Value = fram.Recipe.RepeatTimes;
                numericUpDown_Angle1.Value = fram.Recipe.Angle[0];
                numericUpDown_Angle2.Value = fram.Recipe.Angle[1];
                numericUpDown_Angle3.Value = fram.Recipe.Angle[2];
                numericUpDown_Angle4.Value = fram.Recipe.Angle[3];
                numericUpDown_Angle5.Value = fram.Recipe.Angle[4];
                numericUpDown_Angle6.Value = fram.Recipe.Angle[5];
                numericUpDown_Angle7.Value = fram.Recipe.Angle[6];
                numericUpDown_Angle8.Value = fram.Recipe.Angle[7];
                tb_CreateTime.Text = fram.Recipe.CreateTime;
                tb_ReviseTime.Text = fram.Recipe.ReviseTime;
                cb_SF3RecipeName.Text = fram.Recipe.SF3_Name;
                tb_ScanPatternName.Text = fram.Recipe.MotionPatternName;
                cb_TrimWaferEdgeEvaluate.Checked = (fram.Recipe.WaferEdgeEvaluate == 1);
                cbAnalysis_method.Checked = (fram.Recipe.Analysis_method == 1);
                tb_BlueTape_Threshold.Text = fram.Recipe.BlueTapeThreshold.ToString();
                tb_Step1_Range_step1x0.Text = fram.Recipe.Step1_Range_step1x0.ToString();
                tb_Step1_Range_step1x1.Text = fram.Recipe.Step1_Range_step1x1.ToString();
                tb_Step2_Range_step2x0.Text = fram.Recipe.Step2_Range_step2x0.ToString();
                tb_Step2_Range_step2x1.Text = fram.Recipe.Step2_Range_step2x1.ToString();
                tb_Step2_Range_step1x0.Text = fram.Recipe.Step2_Range_step1x0.ToString();
                tb_Step2_Range_step1x1.Text = fram.Recipe.Step2_Range_step1x1.ToString();
                tb_Range1_Percent.Text = fram.Recipe.Range1_Percent.ToString();
                tb_Range2_Percent.Text = fram.Recipe.Range2_Percent.ToString();
                cb_CCDMode.SelectedIndex = fram.Recipe.RecordCCDRule;
                tb_CCDMode_Angle_Start.Text = fram.Recipe.RecordCCD_Angle_Start.ToString();
                tb_CCDMode_Angle_End.Text = fram.Recipe.RecordCCD_Angle_End.ToString();
                tb_CCDMode_Angle_Pitch.Text = fram.Recipe.RecordCCD_Angle_Pitch.ToString();
                cb_RecordAfterMeasure.Checked = (fram.Recipe.RecordAfterMeasure == 1);
                td_LJ_Flat.Text = fram.Recipe.LJ_Flat.ToString();
                cb_RD_LJ.Checked = (fram.Recipe.RD_LJ == 1);

                //================== 20240628新增系數 ======================
                txt_H1.Text = fram.Recipe.H1.ToString();
                txt_H2.Text = fram.Recipe.H2.ToString();
                txt_W1.Text = fram.Recipe.W1.ToString();
                txt_W2.Text = fram.Recipe.W2.ToString();

                //====================== 20250815 Limit ============================
                cb_LimitMethod.Checked = (fram.Recipe.LimitMethod == 1);
                tb_H1_Lower_Limit.Text = fram.Recipe.H1_LowerLimit.ToString();
                tb_H1_Upper_Limit.Text = fram.Recipe.H1_UpperLimit.ToString();
                tb_W1_Lower_Limit.Text = fram.Recipe.W1_LowerLimit.ToString();
                tb_W1_Upper_Limit.Text = fram.Recipe.W1_UpperLimit.ToString();
                tb_H2_Lower_Limit.Text = fram.Recipe.H2_LowerLimit.ToString();
                tb_H2_Upper_Limit.Text = fram.Recipe.H2_UpperLimit.ToString();
                tb_W2_Lower_Limit.Text = fram.Recipe.W2_LowerLimit.ToString();
                tb_W2_Upper_Limit.Text = fram.Recipe.W2_UpperLimit.ToString();
            }
        }

        private void OffsetInit()
        {
            cb_Offset.Items.Add("None");            // 0
            cb_Offset.Items.Add("Offline 1Step");   // 1
            cb_Offset.Items.Add("Offline 2Step");   // 2
            cb_Offset.Items.Add("Inline 1Step");    // 3
            cb_Offset.Items.Add("Inline 2Step");    // 4
            cb_Offset.Items.Add("QC 1 Step");       // 5
            cb_Offset.Items.Add("BlueTape");        // 6
            cb_Offset.Items.Add("Trim0 1Step");     // 7
            cb_Offset.Items.Add("Trim0 2Step");     // 8
            cb_Offset.Items.Add("F2F 1Step");       // 9
            cb_Offset.Items.Add("F2F 2Step");       // 10
            cb_Offset.SelectedIndex = 0;
        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                treeView_Recipe.Nodes.Clear();
                tb_RecipeInitPath.Text = folderBrowserDialog1.SelectedPath;
                if (fram.Recipe.Path != folderBrowserDialog1.SelectedPath)
                {
                    InsertLog.SavetoDB(7, "Change Recipe Path, " + fram.Recipe.Path + " -> " + folderBrowserDialog1.SelectedPath);
                }
                fram.Recipe.Path = folderBrowserDialog1.SelectedPath;
                treeView_Recipe.Nodes.Add(folderBrowserDialog1.SelectedPath);
                System.IO.FileInfo fileName;
                string filename2;
                foreach (string fname in System.IO.Directory.GetFiles(folderBrowserDialog1.SelectedPath))
                {
                    fileName = new System.IO.FileInfo(fname);//取得完整檔名(含副檔名)
                    if (System.IO.Path.GetExtension(fileName.Name) == ".ini")
                    {
                        filename2 = fileName.ToString();
                        filename2 = System.IO.Path.GetFileNameWithoutExtension(filename2);
                        if (filename2 != "param")
                        {
                            treeView_Recipe.Nodes[0].Nodes.Add(filename2);
                        }
                    }
                }
                treeView_Recipe.ExpandAll();
                ParamFile.saveparam("Recipe");
            }
        }

        private void treeView_Recipe_AfterSelect(object sender, TreeViewEventArgs e)
        {
            System.IO.FileInfo fileName;
            string filename2;
            foreach (string fname in System.IO.Directory.GetFiles(fram.Recipe.Path))
            {
                fileName = new System.IO.FileInfo(fname);//取得完整檔名(含副檔名)

                filename2 = fileName.ToString();
                if (filename2 == "")
                {
                    return;
                }
                filename2 = System.IO.Path.GetFileNameWithoutExtension(filename2);
                if (treeView_Recipe.SelectedNode != null && treeView_Recipe.SelectedNode.Text == filename2)
                {
                    fram.Recipe.FilenameSelect = filename2;
                    tb_RecipeSelect.Text = fram.Recipe.FilenameSelect;
                    Console.WriteLine(fileName.FullName);
                    ParamFile.ReadRcpini(fileName.FullName, "Recipe");
                    for (int i = 0; i < 25; i++)
                    {
                        cb_Slot[i].Checked = fram.Recipe.Slot[i] == 1 ? true : false;
                    }
                    if (fram.Recipe.Rotate_Count == 4)
                    {
                        rbtn_Rotate4.Checked = true;
                    }
                    else
                    {
                        rbtn_Rotate8.Checked = true;
                    }
                    if (fram.Recipe.Type == 0)
                    {
                        rbtn_BlueTape.Checked = true;
                    }
                    else if (fram.Recipe.Type == 1)
                    {
                        rbtn_Trim1step.Checked = true;
                    }
                    else if(fram.Recipe.Type == 3)
                    {
                        rbtn_Trim2step2nd.Checked = true;
                    }
                    else if (fram.Recipe.Type == 2)
                    {
                        rbtn_Trim2step.Checked = true;
                    }
                    else if (fram.Recipe.Type == 4)
                    {
                        rbtn_Trim2step3rd.Checked = true;
                    }
                    else if (fram.Recipe.Type == 5)
                    {
                        rbtn_CCD.Checked = true;
                    }
                    else if (fram.Recipe.Type == 6)
                    {
                        rbtn_Trim1step2nd.Checked = true;
                    }
                    else if (fram.Recipe.Type == 7)
                    {
                        rbtn_Trim1step3rd.Checked = true;
                    }
                    else if (fram.Recipe.Type == 8)
                    {
                        rbtn_TTV.Checked = true;
                    }
                    cb_Offset.SelectedIndex = fram.Recipe.OffsetType;
                    numericUpDown1.Value = fram.Recipe.RepeatTimes;
                    numericUpDown_Angle1.Value = fram.Recipe.Angle[0];
                    numericUpDown_Angle2.Value = fram.Recipe.Angle[1];
                    numericUpDown_Angle3.Value = fram.Recipe.Angle[2];
                    numericUpDown_Angle4.Value = fram.Recipe.Angle[3];
                    numericUpDown_Angle5.Value = fram.Recipe.Angle[4];
                    numericUpDown_Angle6.Value = fram.Recipe.Angle[5];
                    numericUpDown_Angle7.Value = fram.Recipe.Angle[6];
                    numericUpDown_Angle8.Value = fram.Recipe.Angle[7];
                    tb_CreateTime.Text = fram.Recipe.CreateTime;
                    tb_ReviseTime.Text = fram.Recipe.ReviseTime;
                    cb_SF3RecipeName.Text = fram.Recipe.SF3_Name;
                    tb_ScanPatternName.Text = fram.Recipe.MotionPatternName;
                    cb_TrimWaferEdgeEvaluate.Checked = (fram.Recipe.WaferEdgeEvaluate == 1);
                    cbAnalysis_method.Checked = (fram.Recipe.Analysis_method == 1);
                    tb_BlueTape_Threshold.Text = fram.Recipe.BlueTapeThreshold.ToString();
                    tb_Step1_Range_step1x0.Text = fram.Recipe.Step1_Range_step1x0.ToString();
                    tb_Step1_Range_step1x1.Text = fram.Recipe.Step1_Range_step1x1.ToString();
                    tb_Step2_Range_step2x0.Text = fram.Recipe.Step2_Range_step2x0.ToString();
                    tb_Step2_Range_step2x1.Text = fram.Recipe.Step2_Range_step2x1.ToString();
                    tb_Step2_Range_step1x0.Text = fram.Recipe.Step2_Range_step1x0.ToString();
                    tb_Step2_Range_step1x1.Text = fram.Recipe.Step2_Range_step1x1.ToString();
                    tb_Range1_Percent.Text = fram.Recipe.Range1_Percent.ToString();
                    tb_Range2_Percent.Text = fram.Recipe.Range2_Percent.ToString();
                    cb_CCDMode.SelectedIndex = fram.Recipe.RecordCCDRule;
                    tb_CCDMode_Angle_Start.Text = fram.Recipe.RecordCCD_Angle_Start.ToString();
                    tb_CCDMode_Angle_End.Text = fram.Recipe.RecordCCD_Angle_End.ToString();
                    tb_CCDMode_Angle_Pitch.Text = fram.Recipe.RecordCCD_Angle_Pitch.ToString();
                    cb_RecordAfterMeasure.Checked = (fram.Recipe.RecordAfterMeasure == 1);
                    td_LJ_Flat.Text = fram.Recipe.LJ_Flat.ToString();
                    cb_RD_LJ.Checked = (fram.Recipe.RD_LJ == 1);

                    //================= 20240628係數 ========================
                    txt_H1.Text = fram.Recipe.H1.ToString();
                    txt_H2.Text = fram.Recipe.H2.ToString();
                    txt_W1.Text = fram.Recipe.W1.ToString();
                    txt_W2.Text = fram.Recipe.W2.ToString();

                    //====================== 20250815 Limit ============================
                    cb_LimitMethod.Checked = (fram.Recipe.LimitMethod == 1);
                    tb_H1_Lower_Limit.Text = fram.Recipe.H1_LowerLimit.ToString();
                    tb_H1_Upper_Limit.Text = fram.Recipe.H1_UpperLimit.ToString();
                    tb_W1_Lower_Limit.Text = fram.Recipe.W1_LowerLimit.ToString();
                    tb_W1_Upper_Limit.Text = fram.Recipe.W1_UpperLimit.ToString();
                    tb_H2_Lower_Limit.Text = fram.Recipe.H2_LowerLimit.ToString();
                    tb_H2_Upper_Limit.Text = fram.Recipe.H2_UpperLimit.ToString();
                    tb_W2_Lower_Limit.Text = fram.Recipe.W2_LowerLimit.ToString();
                    tb_W2_Upper_Limit.Text = fram.Recipe.W2_UpperLimit.ToString();

                }
            }
        }

        private void btn_CreateNew_Click(object sender, EventArgs e)
        {
            if (tb_CreateNewName.Text == "")
            {
                MessageBox.Show("Recipe name can't be empty");
                return;
            }
            System.IO.FileInfo fileName;
            string filename2;
            foreach (string fname in System.IO.Directory.GetFiles(folderBrowserDialog1.SelectedPath))
            {
                fileName = new System.IO.FileInfo(fname);//取得完整檔名(含副檔名)
                filename2 = fileName.ToString();
                filename2 = System.IO.Path.GetFileNameWithoutExtension(filename2);
                if (tb_CreateNewName.Text == filename2)
                {
                    MessageBox.Show("Already have the same file name");
                    return;
                }
            }
            for (int i = 0; i < 25; i++)
            {
                cb_Slot[i].Checked = true;
                fram.Recipe.Slot[i] = 1;
            }
            numericUpDown1.Value = 1;
            rbtn_Rotate4.Checked = true;
            fram.Recipe.Rotate_Count = 4;
            fram.Recipe.Type = 0;
            fram.Recipe.Angle[0] = 1;
            fram.Recipe.Angle[1] = 45;
            fram.Recipe.Angle[2] = 90;
            fram.Recipe.Angle[3] = 135;
            fram.Recipe.Angle[4] = 180;
            fram.Recipe.Angle[5] = 225;
            fram.Recipe.Angle[6] = 270;
            fram.Recipe.Angle[7] = 315;
            fram.Recipe.MotionPatternName = "default";
            fram.Recipe.SF3_Name = "Default";
            fram.Recipe.WaferEdgeEvaluate = 0;
            fram.Recipe.Analysis_method = 0;
            cb_TrimWaferEdgeEvaluate.Checked = false;
            cbAnalysis_method.Checked = false;
            fram.Recipe.BlueTapeThreshold = fram.Analysis.BlueTapeThreshold;
            fram.Recipe.Step1_Range_step1x0 = fram.Analysis.Step1_Range_step1x0;
            fram.Recipe.Step1_Range_step1x1 = fram.Analysis.Step1_Range_step1x1;
            fram.Recipe.Step2_Range_step2x0 = fram.Analysis.Step2_Range_step2x0;
            fram.Recipe.Step2_Range_step2x1 = fram.Analysis.Step2_Range_step2x1;
            fram.Recipe.Step2_Range_step1x0 = fram.Analysis.Step2_Range_step1x0;
            fram.Recipe.Step2_Range_step1x1 = fram.Analysis.Step2_Range_step1x1;
            fram.Recipe.Range1_Percent = fram.Analysis.Range1_Percent;
            fram.Recipe.Range2_Percent = fram.Analysis.Range2_Percent;
            fram.Recipe.RecordCCDRule = 0;
            fram.Recipe.RecordCCD_Angle_Start = 0;
            fram.Recipe.RecordCCD_Angle_End = 315;
            fram.Recipe.RecordCCD_Angle_Pitch = 45;
            fram.Recipe.RecordAfterMeasure = 0;
            fram.Recipe.LJ_Flat = 1.5;
            fram.Recipe.RD_LJ = 0;

            //====================== 20240628 係數 ============================
            fram.Recipe.H1 = 1;
            fram.Recipe.H2 = 1;
            fram.Recipe.W1 = 1;
            fram.Recipe.W2 = 1;

            //====================== 20250815 Limit ============================
            fram.Recipe.LimitMethod = 0;
            fram.Recipe.H1_LowerLimit = 0;
            fram.Recipe.H2_LowerLimit = 0;
            fram.Recipe.W1_LowerLimit = 0;
            fram.Recipe.W2_LowerLimit = 0;
            fram.Recipe.H1_UpperLimit = 1000;
            fram.Recipe.H2_UpperLimit = 1000;
            fram.Recipe.W1_UpperLimit = 1000;
            fram.Recipe.W2_UpperLimit = 1000;

            ParamFile.CreateRCPini(fram.Recipe.Path + "\\" + tb_CreateNewName.Text + ".ini");
            ParamFile.SaveRCPini(sram.dirfilepath + "\\ProcessJob\\" + tb_RecipeSelect.Text + ".pjb", "Recipe");

            treeView_Recipe.Nodes.Clear();
            treeView_Recipe.Nodes.Add(fram.Recipe.Path);
            foreach (string fname in System.IO.Directory.GetFiles(folderBrowserDialog1.SelectedPath))
            {
                fileName = new System.IO.FileInfo(fname);//取得完整檔名(含副檔名)
                filename2 = fileName.ToString();
                filename2 = System.IO.Path.GetFileNameWithoutExtension(filename2);
                treeView_Recipe.Nodes[0].Nodes.Add(filename2);
                if (tb_CreateNewName.Text == filename2)
                {
                    // treeView_Recipe.SelectedNode.Text = filename2;
                }
            }
            treeView_Recipe.ExpandAll();
            tb_RecipeSelect.Text = tb_CreateNewName.Text;
            InsertLog.SavetoDB(6, "Create New Recipe, " + tb_CreateNewName.Text);
            MessageBox.Show("Create New Recipe");
        }

        private void btn_CopyFrom_Click(object sender, EventArgs e)
        {
            if (tb_CopyFromName.Text == "")
            {
                MessageBox.Show("Recipe name can't be empty");
                return;
            }
            System.IO.FileInfo fileName;
            string filename2;
            foreach (string fname in System.IO.Directory.GetFiles(folderBrowserDialog1.SelectedPath))
            {
                fileName = new System.IO.FileInfo(fname);//取得完整檔名(含副檔名)
                filename2 = fileName.ToString();
                filename2 = System.IO.Path.GetFileNameWithoutExtension(filename2);
                if (tb_CopyFromName.Text == filename2)
                {
                    MessageBox.Show("Already have the same file name");
                    return;
                }
            }

            ParamFile.CreateRCPini(fram.Recipe.Path + "\\" + tb_CopyFromName.Text + ".ini");
            ParamFile.SaveRCPini(sram.dirfilepath + "\\ProcessJob\\" + tb_RecipeSelect.Text + ".pjb", "Recipe");

            treeView_Recipe.Nodes.Clear();
            treeView_Recipe.Nodes.Add(fram.Recipe.Path);
            foreach (string fname in System.IO.Directory.GetFiles(folderBrowserDialog1.SelectedPath))
            {
                fileName = new System.IO.FileInfo(fname);//取得完整檔名(含副檔名)
                filename2 = fileName.ToString();
                filename2 = System.IO.Path.GetFileNameWithoutExtension(filename2);
                treeView_Recipe.Nodes[0].Nodes.Add(filename2);
            }
            treeView_Recipe.ExpandAll();
            tb_RecipeSelect.Text = tb_CopyFromName.Text;
            InsertLog.SavetoDB(6, "Copy New Recipe, " + tb_CopyFromName.Text);
            MessageBox.Show("Copy New Recipe");
        }

        private void btn_SaveRecipe_Click(object sender, EventArgs e)
        {
            bool slotSelect = false; // 至少要選一個slot
            for (int i = 0; i < 25; i++)
            {
                fram.Recipe.Slot[i] = cb_Slot[i].Checked == true ? 1 : 0;
                if (!slotSelect)
                {
                    if (fram.Recipe.Slot[i] == 1)
                    {
                        slotSelect = true;
                    }
                }
            }
            fram.Recipe.Angle[0] = Convert.ToInt16(numericUpDown_Angle1.Value);
            fram.Recipe.Angle[1] = Convert.ToInt16(numericUpDown_Angle2.Value);
            fram.Recipe.Angle[2] = Convert.ToInt16(numericUpDown_Angle3.Value);
            fram.Recipe.Angle[3] = Convert.ToInt16(numericUpDown_Angle4.Value);
            fram.Recipe.Angle[4] = Convert.ToInt16(numericUpDown_Angle5.Value);
            fram.Recipe.Angle[5] = Convert.ToInt16(numericUpDown_Angle6.Value);
            fram.Recipe.Angle[6] = Convert.ToInt16(numericUpDown_Angle7.Value);
            fram.Recipe.Angle[7] = Convert.ToInt16(numericUpDown_Angle8.Value);
            fram.Recipe.BlueTapeThreshold = Convert.ToInt32(tb_BlueTape_Threshold.Text);
            fram.Recipe.Step1_Range_step1x0 = Convert.ToInt32(tb_Step1_Range_step1x0.Text);
            fram.Recipe.Step1_Range_step1x1 = Convert.ToInt32(tb_Step1_Range_step1x1.Text);
            fram.Recipe.Step2_Range_step2x0 = Convert.ToInt32(tb_Step2_Range_step2x0.Text);
            fram.Recipe.Step2_Range_step2x1 = Convert.ToInt32(tb_Step2_Range_step2x1.Text);
            fram.Recipe.Step2_Range_step1x0 = Convert.ToInt32(tb_Step2_Range_step1x0.Text);
            fram.Recipe.Step2_Range_step1x1 = Convert.ToInt32(tb_Step2_Range_step1x1.Text);
            fram.Recipe.Range1_Percent = Convert.ToInt32(tb_Range1_Percent.Text);
            fram.Recipe.Range2_Percent = Convert.ToInt32(tb_Range2_Percent.Text);
            fram.Recipe.RecordCCDRule = cb_CCDMode.SelectedIndex;
            fram.Recipe.RecordCCD_Angle_Start = Convert.ToInt32(tb_CCDMode_Angle_Start.Text);
            fram.Recipe.RecordCCD_Angle_End = Convert.ToInt32(tb_CCDMode_Angle_End.Text);
            fram.Recipe.RecordCCD_Angle_Pitch = Convert.ToInt32(tb_CCDMode_Angle_Pitch.Text);

            fram.Recipe.H1 = Convert.ToDouble(txt_H1.Text);
            fram.Recipe.H2 = Convert.ToDouble(txt_H2.Text);
            fram.Recipe.W1 = Convert.ToDouble(txt_W1.Text);
            fram.Recipe.W2 = Convert.ToDouble(txt_W2.Text);

            //====================== 20250815 Limit ============================
            fram.Recipe.LimitMethod = cb_LimitMethod.Checked ? 1 : 0;
            fram.Recipe.H1_LowerLimit = Convert.ToDouble(tb_H1_Lower_Limit.Text);
            fram.Recipe.H1_UpperLimit = Convert.ToDouble(tb_H1_Upper_Limit.Text);
            fram.Recipe.W1_LowerLimit = Convert.ToDouble(tb_W1_Lower_Limit.Text);
            fram.Recipe.W1_UpperLimit = Convert.ToDouble(tb_W1_Upper_Limit.Text);
            fram.Recipe.H2_LowerLimit = Convert.ToDouble(tb_H2_Lower_Limit.Text);
            fram.Recipe.H2_UpperLimit = Convert.ToDouble(tb_H2_Upper_Limit.Text);
            fram.Recipe.W2_LowerLimit = Convert.ToDouble(tb_W2_Lower_Limit.Text);
            fram.Recipe.W2_UpperLimit = Convert.ToDouble(tb_W2_Upper_Limit.Text);

            if (fram.Recipe.Angle[7] < fram.Recipe.Angle[6] || fram.Recipe.Angle[6] < fram.Recipe.Angle[5] ||
                fram.Recipe.Angle[5] < fram.Recipe.Angle[4] || fram.Recipe.Angle[4] < fram.Recipe.Angle[3] ||
                fram.Recipe.Angle[3] < fram.Recipe.Angle[2] || fram.Recipe.Angle[2] < fram.Recipe.Angle[1] ||
                fram.Recipe.Angle[1] < fram.Recipe.Angle[0])
            {
                MessageBox.Show("Angle can't be greater than previous");
                treeView_Recipe_AfterSelect(null, null);
                return;
            }
            if (!slotSelect)
            {
                MessageBox.Show("Wrong Slot Select");
                treeView_Recipe_AfterSelect(null, null);
                return;
            }
            if (rbtn_Rotate4.Checked)
            {
                fram.Recipe.Rotate_Count = 4;
            }
            else
            {
                fram.Recipe.Rotate_Count = 8;
            }
            if (rbtn_BlueTape.Checked)
            {
                fram.Recipe.Type = 0;
            }
            else if (rbtn_Trim1step.Checked)
            {
                fram.Recipe.Type = 1;
            }
            else if (rbtn_Trim2step.Checked)
            {
                fram.Recipe.Type = 2;
            }
            else if(rbtn_Trim2step2nd.Checked)
            {
                fram.Recipe.Type = 3;
            }
            else if(rbtn_Trim2step3rd.Checked)
            {
                fram.Recipe.Type = 4;
            }
            else if (rbtn_CCD.Checked)
            {
                fram.Recipe.Type = 5;
            }
            else if (rbtn_Trim1step2nd.Checked)
            {
                fram.Recipe.Type = 6;
            }
            else if (rbtn_Trim1step3rd.Checked)
            {
                fram.Recipe.Type = 7;
            }
            else if (rbtn_TTV.Checked)
            {
                fram.Recipe.Type = 8;
            }
            else
                fram.Recipe.Type = 0;
            fram.Recipe.OffsetType = cb_Offset.SelectedIndex;
            fram.Recipe.RepeatTimes = Convert.ToInt16(numericUpDown1.Value);
            fram.Recipe.ReviseTime = DateTime.Now.ToString();
            if (fram.m_Hardware_SF3 == 1)
            {
                if (cb_SF3RecipeName.SelectedItem == null)
                {
                    MessageBox.Show("Please Choose SF3 RecipeName");

                    return;
                }
                fram.Recipe.SF3_Name = cb_SF3RecipeName.SelectedItem.ToString();

                for (int i = 0; i < Common.SF3.list_RecipesIDName.Count; i++)
                {
                    if (cb_SF3RecipeName.SelectedItem.ToString() == Common.SF3.list_RecipesIDName[i][1])
                    {
                        fram.Recipe.SF3_ID = Common.SF3.list_RecipesIDName[i][0];
                        break;
                    }
                }
            }
            fram.Recipe.WaferEdgeEvaluate = cb_TrimWaferEdgeEvaluate.Checked ? 1 : 0;
            fram.Recipe.Analysis_method = cbAnalysis_method.Checked ? 1 : 0;
            fram.Recipe.RecordAfterMeasure = cb_RecordAfterMeasure.Checked ? 1 : 0;
            fram.Recipe.LJ_Flat = Convert.ToDouble(td_LJ_Flat.Text);
            fram.Recipe.RD_LJ = cb_RD_LJ.Checked ? 1 : 0;

            tb_ReviseTime.Text = fram.Recipe.ReviseTime;
            ParamFile.SaveRCPini(fram.Recipe.Path + "\\" + tb_RecipeSelect.Text + ".ini", "Recipe");
            ParamFile.SaveRCPini(sram.dirfilepath + "\\ProcessJob\\" + tb_RecipeSelect.Text + ".pjb", "Recipe");
            MessageBox.Show("Save Recipe ");
            InsertLog.SavetoDB(5, "Save Recipe, " + tb_RecipeSelect.Text);
        }

        private void btn_ChangeRecipe_Click(object sender, EventArgs e)
        {
            if (tb_RecipeSelect.Text != "")
            {
                InsertLog.SavetoDB(9, "Change Recipe LP1, " + tb_RecipeName.Text + " -> " + tb_RecipeSelect.Text);
                tb_RecipeName.Text = tb_RecipeSelect.Text;
                fram.Recipe.Filename_LP1 = tb_RecipeSelect.Text;
                ParamFile.saveparam("Recipe");
                //fram.Recipe.RepeatTimes_now = fram.Recipe.RepeatTimes;
            }
        }

        private void btn_ChangeRecipe2_Click(object sender, EventArgs e)
        {
            if (tb_RecipeSelect.Text != "")
            {
                InsertLog.SavetoDB(9, "Change Recipe LP2, " + tb_RecipeName.Text + " -> " + tb_RecipeSelect.Text);
                tb_RecipeName2.Text = tb_RecipeSelect.Text;
                fram.Recipe.Filename_LP2 = tb_RecipeSelect.Text;
                //ParamFile.SaveRCPini(fram.Recipe.Path + "\\" + tb_RecipeName.Text + ".ini", "all");
                ParamFile.saveparam("Recipe");
                //fram.Recipe.RepeatTimes_now = fram.Recipe.RepeatTimes;
            }
        }

        private void cb_selectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_selectAll.Checked)
            {
                for (int i = 0; i < 25; i++)
                {
                    cb_Slot[i].Checked = true;
                }
            }
            else
            {
                for (int i = 0; i < 25; i++)
                {
                    cb_Slot[i].Checked = false;
                }
            }
        }

        private void rbtn_Rotate4_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_Rotate4.Checked)
            {
                numericUpDown_Angle2.Enabled = false;
                numericUpDown_Angle4.Enabled = false;
                numericUpDown_Angle6.Enabled = false;
                numericUpDown_Angle8.Enabled = false;
            }
            else
            {
                numericUpDown_Angle2.Enabled = true;
                numericUpDown_Angle4.Enabled = true;
                numericUpDown_Angle6.Enabled = true;
                numericUpDown_Angle8.Enabled = true;
            }
        }

        private void RecipeManagement_FormClosed(object sender, FormClosedEventArgs e)
        {
            Flag.FormRecipeOpenFlag = false;
        }

        private void rbtn_BlueTape_CheckedChanged(object sender, EventArgs e)
        {
            TrimTypeChange();
        }

        private void rbtn_Trim1step_CheckedChanged(object sender, EventArgs e)
        {
            TrimTypeChange();
        }

        private void rbtn_Trim2step_CheckedChanged(object sender, EventArgs e)
        {
            TrimTypeChange();
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            TrimTypeChange();
        }
        public void TrimTypeChange()
        {
            if (rbtn_BlueTape.Checked)
            {
                rbtn_Rotate8.Checked = true;
                rbtn_Rotate4.Enabled = false;
                rbtn_Rotate8.Enabled = false;
            }
            else if (rbtn_Trim1step.Checked || rbtn_Trim2step.Checked || rbtn_Trim1step2nd.Checked || rbtn_Trim2step2nd.Checked || rbtn_Trim1step3rd.Checked || rbtn_Trim2step3rd.Checked || rbtn_CCD.Checked)
            {
                rbtn_Rotate4.Enabled = true;
                rbtn_Rotate8.Enabled = true;
            }
            else if (rbtn_TTV.Checked)
            {
                rbtn_Rotate4.Enabled = false;
                rbtn_Rotate8.Enabled = false;
            }
        }

        private void btn_ClearRecipeLP1_Click(object sender, EventArgs e)
        {
            fram.Recipe.Filename_LP1 = "";
            tb_RecipeName.Text = fram.Recipe.Filename_LP1;
            ParamFile.saveparam("Recipe");
        }

        private void btn_ClearRecipeLP2_Click(object sender, EventArgs e)
        {
            fram.Recipe.Filename_LP2 = "";
            tb_RecipeName2.Text = fram.Recipe.Filename_LP2;
            ParamFile.saveparam("Recipe");
        }

        private void btn_TTVScanPattern_Click(object sender, EventArgs e)
        {
            ttvPatternForm = new TTVScanPattern(fram.Recipe.MotionPatternPath, fram.Recipe.MotionPatternName);
            //ttvPatternForm.recipeFolder = fram.Recipe.MotionPatternPath;
            //ttvPatternForm.TTV_Recipe_Path = fram.Recipe.MotionPatternName;
            ttvPatternForm.ShowDialog();
            tb_ScanPatternName.Text = fram.Recipe.MotionPatternName;
            //fram.Recipe.MotionPatternName = ttvPatternForm.Name;
        }


    }
}