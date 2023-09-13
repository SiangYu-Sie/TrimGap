using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrimGap
{
    public partial class ParaForm : Form
    {
        public ParaForm()
        {
            InitializeComponent();
            initparameter();

            if (sram.UserAuthority == permissionEnum.ad)
            {
                tb_Analysis_Coefficient_copy.ReadOnly = false;
                tb_Analysis_BlueTapeThreshold_copy.ReadOnly = true;
                cb_Sensorbypass_copy.Enabled = true;
                cb_MotionRotate_copy.Enabled = true;
            }
            else
            {
                tb_Analysis_Coefficient_copy.ReadOnly = true;
                tb_Analysis_BlueTapeThreshold_copy.ReadOnly = false;
                cb_Sensorbypass_copy.Enabled = false;
                cb_MotionRotate_copy.Enabled = false;
            }
            cb_MotionRotate_copy.Items.Add("True");
            cb_MotionRotate_copy.Items.Add("False");
            cb_MotionRotate_copy.Text = fram.S_MotionRotate;
            cb_Sensorbypass_copy.Items.Add("True");
            cb_Sensorbypass_copy.Items.Add("False");
            cb_Sensorbypass_copy.Text = fram.S_Sensorbypass;
            tb_TotalCompleted.Text = fram.EFEMSts.TotalCompletedCount.ToString();
            OffsetInit();
            timer1.Interval = 1000;
        }

        private void OffsetInit()
        {
            cb_Offset.Items.Add("None");
            cb_Offset.Items.Add("Offline");
            cb_Offset.Items.Add("Inline");
            cb_Offset.Items.Add("QC_Angle1");
            cb_Offset.Items.Add("QC_Angle2");
            cb_Offset.Items.Add("QC_Angle3");
            cb_Offset.Items.Add("QC_Angle4");
            cb_Offset.Items.Add("QC_Angle5");
            cb_Offset.Items.Add("QC_Angle6");
            cb_Offset.Items.Add("QC_Angle7");
            cb_Offset.Items.Add("QC_Angle8");
            cb_Offset.SelectedIndex = 0;
        }

        private void initparameter()
        {
            // [Filter]

            // [System]
            tb_ShowDataNum.Text = fram.S_ShowDataNum.ToString();
            //tb_SensorProgramNo.Text = fram.S_SensorProgram.ToString();
            tb_MotionRotate.Text = fram.S_MotionRotate;
            tb_Sensorbypass.Text = fram.S_Sensorbypass;
            // [Analysis]
            tb_Analysis_Coefficient.Text = fram.Analysis.Coefficient.ToString();
            tb_Analysis_BlueTapeThreshold.Text = fram.Analysis.BlueTapeThreshold.ToString();
            tb_Step1_Range_step1x0.Text = fram.Analysis.Step1_Range_step1x0.ToString();
            tb_Step1_Range_step1x1.Text = fram.Analysis.Step1_Range_step1x1.ToString();
            tb_Step2_Range_step1x0.Text = fram.Analysis.Step2_Range_step1x0.ToString();
            tb_Step2_Range_step1x1.Text = fram.Analysis.Step2_Range_step1x1.ToString();
            tb_Step2_Range_step2x0.Text = fram.Analysis.Step2_Range_step2x0.ToString();
            tb_Step2_Range_step2x1.Text = fram.Analysis.Step2_Range_step2x1.ToString();
            tb_Range1_Percent.Text = fram.Analysis.Range1_Percent.ToString();
            tb_Range2_Percent.Text = fram.Analysis.Range2_Percent.ToString();
            tb_1StepH.Text = fram.Analysis.Offset1StepH.ToString();
            tb_1StepW.Text = fram.Analysis.Offset1StepW.ToString();
            tb_2StepH1.Text = fram.Analysis.Offset2StepH1.ToString();
            tb_2StepW1.Text = fram.Analysis.Offset2StepW1.ToString();
            tb_2StepH2.Text = fram.Analysis.Offset2StepH2.ToString();
            tb_2StepW2.Text = fram.Analysis.Offset2StepW2.ToString();
            tb_PT_1StepH.Text = fram.Analysis.Offset_PT_1StepH.ToString();
            tb_PT_1StepW.Text = fram.Analysis.Offset_PT_1StepW.ToString();
            tb_PT_2StepH1.Text = fram.Analysis.Offset_PT_2StepH1.ToString();
            tb_PT_2StepW1.Text = fram.Analysis.Offset_PT_2StepW1.ToString();
            tb_PT_2StepH2.Text = fram.Analysis.Offset_PT_2StepH2.ToString();
            tb_PT_2StepW2.Text = fram.Analysis.Offset_PT_2StepW2.ToString();
            tb_BlueTapeW.Text = fram.Analysis.OffsetBlueTapeW.ToString();
        }

        private void btnParamSave_Click(object sender, EventArgs e)
        {
            if (Flag.AutoidleFlag)
            {
                DialogResult tmpResult = MessageBox.Show("請先停止量測在修改參數", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string pattern = "";
                pattern = "^[+]?([0-9]+\\.?)?[0-9]+$";

                #region [System] groupbox1

                if (tb_ShowDataNum_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_ShowDataNum_copy.Text, pattern))
                    {
                        MessageBox.Show("tb_ShowDataNum_copy " + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "ShowDataNum " + fram.S_ShowDataNum + " -> " + tb_ShowDataNum_copy.Text);
                        tb_ShowDataNum.Text = tb_ShowDataNum_copy.Text;
                        fram.S_ShowDataNum = Convert.ToInt32(tb_ShowDataNum_copy.Text);
                    }
                }
                if (tb_SensorProgramNo_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_SensorProgramNo_copy.Text, pattern))
                    {
                        MessageBox.Show("SensorProgramNo " + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "SensorProgramNo " + fram.S_SensorProgram + " -> " + tb_SensorProgramNo_copy.Text);
                        tb_SensorProgramNo.Text = tb_SensorProgramNo_copy.Text;
                        fram.S_SensorProgram = Convert.ToInt32(tb_SensorProgramNo_copy.Text);
                    }
                }
                if (tb_MotionRotate.Text != cb_MotionRotate_copy.Text)
                {
                    tb_MotionRotate.Text = cb_MotionRotate_copy.Text;
                    InsertLog.SavetoDB(7, "MotionRotate " + fram.S_MotionRotate + " -> " + cb_MotionRotate_copy.Text);
                    fram.S_MotionRotate = cb_MotionRotate_copy.Text;

                    if (fram.S_MotionRotate == "True")
                    {
                        Common.CGWrapper.UpdateEC(TrimGap_EqpID.MotionRotate, 1);
                    }
                    else
                    {
                        Common.CGWrapper.UpdateEC(TrimGap_EqpID.MotionRotate, 0);
                    }
                }
                if (tb_Sensorbypass.Text != cb_Sensorbypass_copy.Text)
                {
                    tb_Sensorbypass.Text = cb_Sensorbypass_copy.Text;
                    InsertLog.SavetoDB(7, "Sensorbypass " + fram.S_Sensorbypass + " -> " + cb_Sensorbypass_copy.Text);
                    fram.S_Sensorbypass = cb_Sensorbypass_copy.Text;
                }

                #endregion [System] groupbox1

                #region [Analysis] groupbox2

                if (tb_Analysis_Coefficient_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Analysis_Coefficient_copy.Text, pattern))
                    {
                        MessageBox.Show("Coefficient " + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "Coefficient " + fram.Analysis.Coefficient + "->" + tb_Analysis_Coefficient_copy.Text);
                        tb_Analysis_Coefficient.Text = tb_Analysis_Coefficient_copy.Text;
                        fram.Analysis.Coefficient = Convert.ToInt32(tb_Analysis_Coefficient_copy.Text);
                        Common.CGWrapper.UpdateEC(TrimGap_EqpID.Cofficient, fram.Analysis.Coefficient);
                    }
                }
                if (tb_Analysis_BlueTapeThreshold_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Analysis_BlueTapeThreshold_copy.Text, pattern))
                    {
                        MessageBox.Show("BlueTape Threshold " + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "BlueTape Threshold " + fram.Analysis.BlueTapeThreshold + "->" + tb_Analysis_BlueTapeThreshold_copy.Text);
                        tb_Analysis_BlueTapeThreshold.Text = tb_Analysis_BlueTapeThreshold_copy.Text;
                        fram.Analysis.BlueTapeThreshold = Convert.ToInt32(tb_Analysis_BlueTapeThreshold_copy.Text);
                    }
                }
                if (tb_Step2_Range_step1x0_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Step2_Range_step1x0_copy.Text, pattern))
                    {
                        MessageBox.Show("Step2_Range_step1x0" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "Step2_Range_step1x0 " + fram.Analysis.Step2_Range_step1x0 + "->" + tb_Step2_Range_step1x0_copy.Text);
                        tb_Step2_Range_step1x0.Text = tb_Step2_Range_step1x0_copy.Text;
                        fram.Analysis.Step2_Range_step1x0 = Convert.ToInt32(tb_Step2_Range_step1x0_copy.Text);
                    }
                }
                if (tb_Step2_Range_step1x1_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Step2_Range_step1x1_copy.Text, pattern))
                    {
                        MessageBox.Show("Step2_Range_step1x1" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "Step2_Range_step1x1 " + fram.Analysis.Step2_Range_step1x1 + "->" + tb_Step2_Range_step1x1_copy.Text);
                        tb_Step2_Range_step1x1.Text = tb_Step2_Range_step1x1_copy.Text;
                        fram.Analysis.Step2_Range_step1x1 = Convert.ToInt32(tb_Step2_Range_step1x1_copy.Text);
                    }
                }
                if (tb_Step2_Range_step2x0_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Step2_Range_step2x0_copy.Text, pattern))
                    {
                        MessageBox.Show("Step2_Range_step2x0" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "Step2_Range_step2x0 " + fram.Analysis.Step2_Range_step2x0 + "->" + tb_Step2_Range_step2x0_copy.Text);
                        tb_Step2_Range_step2x0.Text = tb_Step2_Range_step2x0_copy.Text;
                        fram.Analysis.Step2_Range_step2x0 = Convert.ToInt32(tb_Step2_Range_step2x0_copy.Text);
                    }
                }
                if (tb_Step2_Range_step2x1_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Step2_Range_step2x1_copy.Text, pattern))
                    {
                        MessageBox.Show("Step2_Range_step2x1" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "Step2_Range_step2x1 " + fram.Analysis.Step2_Range_step2x1 + "->" + tb_Step2_Range_step2x1_copy.Text);
                        tb_Step2_Range_step2x1.Text = tb_Step2_Range_step2x1_copy.Text;
                        fram.Analysis.Step2_Range_step2x1 = Convert.ToInt32(tb_Step2_Range_step2x1_copy.Text);
                    }
                }
                if (tb_Step1_Range_step1x0_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Step1_Range_step1x0_copy.Text, pattern))
                    {
                        MessageBox.Show("Step1_Range_step1x0" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "Step1_Range_step1x0 " + fram.Analysis.Step1_Range_step1x0 + "->" + tb_Step1_Range_step1x0_copy.Text);
                        tb_Step1_Range_step1x0.Text = tb_Step1_Range_step1x0_copy.Text;
                        fram.Analysis.Step1_Range_step1x0 = Convert.ToInt32(tb_Step1_Range_step1x0_copy.Text);
                    }
                }
                if (tb_Step1_Range_step1x1_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Step1_Range_step1x1_copy.Text, pattern))
                    {
                        MessageBox.Show("Step1_Range_step1x1" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "Step1_Range_step1x1 " + fram.Analysis.Step1_Range_step1x1 + "->" + tb_Step1_Range_step1x1_copy.Text);
                        tb_Step1_Range_step1x1.Text = tb_Step1_Range_step1x1_copy.Text;
                        fram.Analysis.Step1_Range_step1x1 = Convert.ToInt32(tb_Step1_Range_step1x1_copy.Text);
                    }
                }

                if (tb_Range1_Percent_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Range1_Percent_copy.Text, pattern))
                    {
                        MessageBox.Show("Range1_Percent" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "Range1_Percent " + fram.Analysis.Range1_Percent + "->" + tb_Range1_Percent_copy.Text);
                        tb_Range1_Percent.Text = tb_Range1_Percent_copy.Text;
                        fram.Analysis.Range1_Percent = Convert.ToInt32(tb_Range1_Percent_copy.Text);
                    }
                }
                if (tb_Range2_Percent_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Range2_Percent_copy.Text, pattern))
                    {
                        MessageBox.Show("Range2_Percent" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "Range2_Percent " + fram.Analysis.Range2_Percent + "->" + tb_Range2_Percent_copy.Text);
                        tb_Range2_Percent.Text = tb_Range2_Percent_copy.Text;
                        fram.Analysis.Range2_Percent = Convert.ToInt32(tb_Range2_Percent_copy.Text);
                    }
                }

                #endregion [Analysis] groupbox2

                #region [Analysis offset] groupbox3

                pattern = "^[+-]?([0-9]+\\.?)?[0-9]+$";
                if (tb_1StepW_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_1StepW_copy.Text, pattern))
                    {
                        MessageBox.Show("Offset 1 Step W" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, cb_Offset.SelectedText + " W " + fram.Analysis.Offset1StepW + "->" + tb_1StepW_copy.Text);
                        tb_1StepW.Text = tb_1StepW_copy.Text;
                        fram.Analysis.Offset1StepW = Convert.ToDouble(tb_1StepW_copy.Text);
                        switch (cb_Offset.SelectedIndex)
                        {
                            case 0:

                                break;

                            case 1:
                                fram.Analysis.Offset.Offline_1StepW = fram.Analysis.Offset1StepW;
                                break;

                            case 2:
                                fram.Analysis.Offset.Inline_1StepW = fram.Analysis.Offset1StepW;
                                break;

                            default:
                                fram.Analysis.Offset.QC_1StepW[cb_Offset.SelectedIndex - 3] = fram.Analysis.Offset1StepW;
                                break;
                        }
                    }
                }
                if (tb_1StepH_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_1StepH_copy.Text, pattern))
                    {
                        MessageBox.Show("Offset 1 Step H" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, cb_Offset.SelectedText + " H " + fram.Analysis.Offset1StepH + "->" + tb_1StepH_copy.Text);
                        tb_1StepH.Text = tb_1StepH_copy.Text;
                        fram.Analysis.Offset1StepH = Convert.ToDouble(tb_1StepH_copy.Text);
                        switch (cb_Offset.SelectedIndex)
                        {
                            case 0:

                                break;

                            case 1:
                                fram.Analysis.Offset.Offline_1StepH = fram.Analysis.Offset1StepH;
                                break;

                            case 2:
                                fram.Analysis.Offset.Inline_1StepH = fram.Analysis.Offset1StepH;
                                break;

                            default:
                                fram.Analysis.Offset.QC_1StepH[cb_Offset.SelectedIndex - 3] = fram.Analysis.Offset1StepH;
                                break;
                        }
                    }
                }
                if (tb_2StepW1_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_2StepW1_copy.Text, pattern))
                    {
                        MessageBox.Show("Offset 2 Step W1" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, cb_Offset.SelectedText + " W1 " + fram.Analysis.Offset2StepW1 + "->" + tb_2StepW1_copy.Text);
                        tb_2StepW1.Text = tb_2StepW1_copy.Text;
                        fram.Analysis.Offset2StepW1 = Convert.ToDouble(tb_2StepW1_copy.Text);
                        switch (cb_Offset.SelectedIndex)
                        {
                            case 0:

                                break;

                            case 1:
                                fram.Analysis.Offset.Offline_2StepW1 = fram.Analysis.Offset2StepW1;
                                break;

                            case 2:
                                fram.Analysis.Offset.Inline_2StepW1 = fram.Analysis.Offset2StepW1;
                                break;

                            default:
                                fram.Analysis.Offset.QC_2StepW1[cb_Offset.SelectedIndex - 3] = fram.Analysis.Offset2StepW1;
                                break;
                        }
                    }
                }
                if (tb_2StepH1_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_2StepH1_copy.Text, pattern))
                    {
                        MessageBox.Show("Offset 2 Step H1" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, cb_Offset.SelectedText + " H1 " + fram.Analysis.Offset2StepH1 + "->" + tb_2StepH1_copy.Text);
                        tb_2StepH1.Text = tb_2StepH1_copy.Text;
                        fram.Analysis.Offset2StepH1 = Convert.ToDouble(tb_2StepH1_copy.Text);
                        switch (cb_Offset.SelectedIndex)
                        {
                            case 0:

                                break;

                            case 1:
                                fram.Analysis.Offset.Offline_2StepH1 = fram.Analysis.Offset2StepH1;
                                break;

                            case 2:
                                fram.Analysis.Offset.Inline_2StepH1 = fram.Analysis.Offset2StepH1;
                                break;

                            default:
                                fram.Analysis.Offset.QC_2StepH1[cb_Offset.SelectedIndex - 3] = fram.Analysis.Offset2StepH1;
                                break;
                        }
                    }
                }
                if (tb_2StepW2_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_2StepW2_copy.Text, pattern))
                    {
                        MessageBox.Show("Offset 2 Step W2" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, cb_Offset.SelectedText + " W2 " + fram.Analysis.Offset2StepW2 + "->" + tb_2StepW2_copy.Text);
                        tb_2StepW2.Text = tb_2StepW2_copy.Text;
                        fram.Analysis.Offset2StepW2 = Convert.ToDouble(tb_2StepW2_copy.Text);
                        switch (cb_Offset.SelectedIndex)
                        {
                            case 0:

                                break;

                            case 1:
                                fram.Analysis.Offset.Offline_2StepW2 = fram.Analysis.Offset2StepW2;
                                break;

                            case 2:
                                fram.Analysis.Offset.Inline_2StepW2 = fram.Analysis.Offset2StepW2;
                                break;

                            default:
                                fram.Analysis.Offset.QC_2StepW2[cb_Offset.SelectedIndex - 3] = fram.Analysis.Offset2StepW2;
                                break;
                        }
                    }
                }
                if (tb_2StepH2_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_2StepH2_copy.Text, pattern))
                    {
                        MessageBox.Show("Offset 2 Step H2" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, cb_Offset.SelectedText + " H2 " + fram.Analysis.Offset2StepH2 + "->" + tb_2StepH2_copy.Text);
                        tb_2StepH2.Text = tb_2StepH2_copy.Text;
                        fram.Analysis.Offset2StepH2 = Convert.ToDouble(tb_2StepH2_copy.Text);
                        switch (cb_Offset.SelectedIndex)
                        {
                            case 0:

                                break;

                            case 1:
                                fram.Analysis.Offset.Offline_2StepH2 = fram.Analysis.Offset2StepH2;
                                break;

                            case 2:
                                fram.Analysis.Offset.Inline_2StepH2 = fram.Analysis.Offset2StepH2;
                                break;

                            default:
                                fram.Analysis.Offset.QC_2StepH2[cb_Offset.SelectedIndex - 3] = fram.Analysis.Offset2StepH2;
                                break;
                        }
                    }
                }
                if (tb_PT_1StepW_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_PT_1StepW_copy.Text, pattern))
                    {
                        MessageBox.Show("Offset PT 1 Step W" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, cb_Offset.SelectedText + " PT W " + fram.Analysis.Offset_PT_1StepW + "->" + tb_PT_1StepW_copy.Text);
                        tb_PT_1StepW.Text = tb_PT_1StepW_copy.Text;
                        fram.Analysis.Offset_PT_1StepW = Convert.ToDouble(tb_PT_1StepW_copy.Text);
                        switch (cb_Offset.SelectedIndex)
                        {
                            case 0:

                                break;

                            case 1:
                                fram.Analysis.Offset.Offline_PT_1StepW = fram.Analysis.Offset_PT_1StepW;
                                break;

                            case 2:
                                fram.Analysis.Offset.Inline_PT_1StepW = fram.Analysis.Offset_PT_1StepW;
                                break;

                            default:
                                fram.Analysis.Offset.QC_PT_1StepW[cb_Offset.SelectedIndex - 3] = fram.Analysis.Offset_PT_1StepW;
                                break;
                        }
                    }
                }
                if (tb_PT_1StepH_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_PT_1StepH_copy.Text, pattern))
                    {
                        MessageBox.Show("Offset PT 1 Step H" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, cb_Offset.SelectedText + " PT H " + fram.Analysis.Offset_PT_1StepH + "->" + tb_PT_1StepH_copy.Text);
                        tb_PT_1StepH.Text = tb_PT_1StepH_copy.Text;
                        fram.Analysis.Offset_PT_1StepH = Convert.ToDouble(tb_PT_1StepH_copy.Text);
                        switch (cb_Offset.SelectedIndex)
                        {
                            case 0:

                                break;

                            case 1:
                                fram.Analysis.Offset.Offline_PT_1StepH = fram.Analysis.Offset_PT_1StepH;
                                break;

                            case 2:
                                fram.Analysis.Offset.Inline_PT_1StepH = fram.Analysis.Offset_PT_1StepH;
                                break;

                            default:
                                fram.Analysis.Offset.QC_PT_1StepH[cb_Offset.SelectedIndex - 3] = fram.Analysis.Offset_PT_1StepH;
                                break;
                        }
                    }
                }
                if (tb_PT_2StepW1_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_PT_2StepW1_copy.Text, pattern))
                    {
                        MessageBox.Show("Offset PT 2 Step W1" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, cb_Offset.SelectedText + " PT W1 " + fram.Analysis.Offset_PT_2StepW1 + "->" + tb_PT_2StepW1_copy.Text);
                        tb_PT_2StepW1.Text = tb_PT_2StepW1_copy.Text;
                        fram.Analysis.Offset_PT_2StepW1 = Convert.ToDouble(tb_PT_2StepW1_copy.Text);
                        switch (cb_Offset.SelectedIndex)
                        {
                            case 0:

                                break;

                            case 1:
                                fram.Analysis.Offset.Offline_PT_2StepW1 = fram.Analysis.Offset_PT_2StepW1;
                                break;

                            case 2:
                                fram.Analysis.Offset.Inline_PT_2StepW1 = fram.Analysis.Offset_PT_2StepW1;
                                break;

                            default:
                                fram.Analysis.Offset.QC_PT_2StepW1[cb_Offset.SelectedIndex - 3] = fram.Analysis.Offset_PT_2StepW1;
                                break;
                        }
                    }
                }
                if (tb_PT_2StepH1_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_PT_2StepH1_copy.Text, pattern))
                    {
                        MessageBox.Show("Offset PT 2 Step H1" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, cb_Offset.SelectedText + " PT H1 " + fram.Analysis.Offset_PT_2StepH1 + "->" + tb_PT_2StepH1_copy.Text);
                        tb_PT_2StepH1.Text = tb_PT_2StepH1_copy.Text;
                        fram.Analysis.Offset_PT_2StepH1 = Convert.ToDouble(tb_PT_2StepH1_copy.Text);
                        switch (cb_Offset.SelectedIndex)
                        {
                            case 0:

                                break;

                            case 1:
                                fram.Analysis.Offset.Offline_PT_2StepH1 = fram.Analysis.Offset_PT_2StepH1;
                                break;

                            case 2:
                                fram.Analysis.Offset.Inline_PT_2StepH1 = fram.Analysis.Offset_PT_2StepH1;
                                break;

                            default:
                                fram.Analysis.Offset.QC_PT_2StepH1[cb_Offset.SelectedIndex - 3] = fram.Analysis.Offset_PT_2StepH1;
                                break;
                        }
                    }
                }
                if (tb_PT_2StepW2_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_PT_2StepW2_copy.Text, pattern))
                    {
                        MessageBox.Show("Offset PT 2 Step W2" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, cb_Offset.SelectedText + " PT W2 " + fram.Analysis.Offset_PT_2StepW2 + "->" + tb_PT_2StepW2_copy.Text);
                        tb_PT_2StepW2.Text = tb_PT_2StepW2_copy.Text;
                        fram.Analysis.Offset_PT_2StepW2 = Convert.ToDouble(tb_PT_2StepW2_copy.Text);
                        switch (cb_Offset.SelectedIndex)
                        {
                            case 0:

                                break;

                            case 1:
                                fram.Analysis.Offset.Offline_PT_2StepW2 = fram.Analysis.Offset_PT_2StepW2;
                                break;

                            case 2:
                                fram.Analysis.Offset.Inline_PT_2StepW2 = fram.Analysis.Offset_PT_2StepW2;
                                break;

                            default:
                                fram.Analysis.Offset.QC_PT_2StepW2[cb_Offset.SelectedIndex - 3] = fram.Analysis.Offset_PT_2StepW2;
                                break;
                        }
                    }
                }
                if (tb_PT_2StepH2_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_PT_2StepH2_copy.Text, pattern))
                    {
                        MessageBox.Show("Offset PT 2 Step H2" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, cb_Offset.SelectedText + " PT H2 " + fram.Analysis.Offset_PT_2StepH2 + "->" + tb_PT_2StepH2_copy.Text);
                        tb_PT_2StepH2.Text = tb_PT_2StepH2_copy.Text;
                        fram.Analysis.Offset_PT_2StepH2 = Convert.ToDouble(tb_PT_2StepH2_copy.Text);
                        switch (cb_Offset.SelectedIndex)
                        {
                            case 0:

                                break;

                            case 1:
                                fram.Analysis.Offset.Offline_PT_2StepH2 = fram.Analysis.Offset_PT_2StepH2;
                                break;

                            case 2:
                                fram.Analysis.Offset.Inline_PT_2StepH2 = fram.Analysis.Offset_PT_2StepH2;
                                break;

                            default:
                                fram.Analysis.Offset.QC_PT_2StepH2[cb_Offset.SelectedIndex - 3] = fram.Analysis.Offset_PT_2StepH2;
                                break;
                        }
                    }
                }
                if (tb_BlueTapeW_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_BlueTapeW_copy.Text, pattern))
                    {
                        MessageBox.Show("BlueTape W " + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, cb_Offset.SelectedText + " W " + fram.Analysis.OffsetBlueTapeW + "->" + tb_BlueTapeW_copy.Text);
                        tb_BlueTapeW.Text = tb_BlueTapeW_copy.Text;
                        fram.Analysis.OffsetBlueTapeW = Convert.ToDouble(tb_BlueTapeW_copy.Text);
                        switch (cb_Offset.SelectedIndex)
                        {
                            case 0:

                                break;

                            case 1:
                                fram.Analysis.Offset.Offline_BlueTapeW = fram.Analysis.OffsetBlueTapeW;
                                break;

                            case 2:
                                fram.Analysis.Offset.Inline_BlueTapeW = fram.Analysis.OffsetBlueTapeW;
                                break;

                            default:
                                fram.Analysis.Offset.QC_BlueTapeW[cb_Offset.SelectedIndex - 3] = fram.Analysis.OffsetBlueTapeW;
                                break;
                        }
                    }
                }
                pattern = "^[+]?([0-9]+\\.?)?[0-9]+$";

                #endregion [Analysis offset] groupbox3

                ParamFile.saveparam("all");
                btnParamClear_Click(null, null);
            }
        }

        private void btnParamClear_Click(object sender, EventArgs e)
        {
            int last;
            foreach (Control ctrl in this.groupBox1.Controls)
            {
                if (ctrl is TextBox)
                {
                    last = ctrl.Name.LastIndexOf("copy");
                    if (last > 0)
                    {
                        ctrl.Text = "";
                    }
                }
            }
            foreach (Control ctrl in this.groupBox2.Controls)
            {
                if (ctrl is TextBox)
                {
                    last = ctrl.Name.LastIndexOf("copy");
                    if (last > 0)
                    {
                        ctrl.Text = "";
                    }
                }
            }
            foreach (Control ctrl in this.groupBox3.Controls)
            {
                if (ctrl is TextBox)
                {
                    last = ctrl.Name.LastIndexOf("copy");
                    if (last > 0)
                    {
                        ctrl.Text = "";
                    }
                }
            }
        }

        private void btn_TotalCompletedClear_Click(object sender, EventArgs e)
        {
            InsertLog.SavetoDB(9, "TotalCompletedCount " + fram.EFEMSts.TotalCompletedCount + " -> 0");
            fram.EFEMSts.TotalCompletedCount = 0;
            tb_TotalCompleted.Text = fram.EFEMSts.TotalCompletedCount.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tb_TotalCompleted.Text != fram.EFEMSts.TotalCompletedCount.ToString())
            {
                tb_TotalCompleted.Text = fram.EFEMSts.TotalCompletedCount.ToString();
            }
        }

        private void ParaForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void cb_Offset_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cb_Offset.SelectedIndex)
            {
                case 0:
                    tb_1StepH.Text = "0";
                    tb_1StepW.Text = "0";
                    tb_2StepH1.Text = "0";
                    tb_2StepW1.Text = "0";
                    tb_2StepH2.Text = "0";
                    tb_2StepW2.Text = "0";
                    tb_PT_1StepH.Text = "0";
                    tb_PT_1StepW.Text = "0";
                    tb_PT_2StepH1.Text = "0";
                    tb_PT_2StepW1.Text = "0";
                    tb_PT_2StepH2.Text = "0";
                    tb_PT_2StepW2.Text = "0";
                    tb_BlueTapeW.Text = "0";
                    break;

                case 1:
                    tb_1StepH.Text = fram.Analysis.Offset.Offline_1StepH.ToString();
                    tb_1StepW.Text = fram.Analysis.Offset.Offline_1StepW.ToString();
                    tb_2StepH1.Text = fram.Analysis.Offset.Offline_2StepH1.ToString();
                    tb_2StepW1.Text = fram.Analysis.Offset.Offline_2StepW1.ToString();
                    tb_2StepH2.Text = fram.Analysis.Offset.Offline_2StepH2.ToString();
                    tb_2StepW2.Text = fram.Analysis.Offset.Offline_2StepW2.ToString();
                    tb_PT_1StepH.Text = fram.Analysis.Offset.Offline_PT_1StepH.ToString();
                    tb_PT_1StepW.Text = fram.Analysis.Offset.Offline_PT_1StepW.ToString();
                    tb_PT_2StepH1.Text = fram.Analysis.Offset.Offline_PT_2StepH1.ToString();
                    tb_PT_2StepW1.Text = fram.Analysis.Offset.Offline_PT_2StepW1.ToString();
                    tb_PT_2StepH2.Text = fram.Analysis.Offset.Offline_PT_2StepH2.ToString();
                    tb_PT_2StepW2.Text = fram.Analysis.Offset.Offline_PT_2StepW2.ToString();
                    tb_BlueTapeW.Text = fram.Analysis.OffsetBlueTapeW.ToString();
                    break;

                case 2:
                    tb_1StepH.Text = fram.Analysis.Offset.Inline_1StepH.ToString();
                    tb_1StepW.Text = fram.Analysis.Offset.Inline_1StepW.ToString();
                    tb_2StepH1.Text = fram.Analysis.Offset.Inline_2StepH1.ToString();
                    tb_2StepW1.Text = fram.Analysis.Offset.Inline_2StepW1.ToString();
                    tb_2StepH2.Text = fram.Analysis.Offset.Inline_2StepH2.ToString();
                    tb_2StepW2.Text = fram.Analysis.Offset.Inline_2StepW2.ToString();
                    tb_PT_1StepH.Text = fram.Analysis.Offset.Inline_PT_1StepH.ToString();
                    tb_PT_1StepW.Text = fram.Analysis.Offset.Inline_PT_1StepW.ToString();
                    tb_PT_2StepH1.Text = fram.Analysis.Offset.Inline_PT_2StepH1.ToString();
                    tb_PT_2StepW1.Text = fram.Analysis.Offset.Inline_PT_2StepW1.ToString();
                    tb_PT_2StepH2.Text = fram.Analysis.Offset.Inline_PT_2StepH2.ToString();
                    tb_PT_2StepW2.Text = fram.Analysis.Offset.Inline_PT_2StepW2.ToString();
                    tb_BlueTapeW.Text = fram.Analysis.Offset.Inline_BlueTapeW.ToString();
                    break;

                default:  // >3 (QC)
                    tb_1StepH.Text = fram.Analysis.Offset.QC_1StepH[cb_Offset.SelectedIndex - 3].ToString();
                    tb_1StepW.Text = fram.Analysis.Offset.QC_1StepW[cb_Offset.SelectedIndex - 3].ToString();
                    tb_2StepH1.Text = fram.Analysis.Offset.QC_2StepH1[cb_Offset.SelectedIndex - 3].ToString();
                    tb_2StepW1.Text = fram.Analysis.Offset.QC_2StepW1[cb_Offset.SelectedIndex - 3].ToString();
                    tb_2StepH2.Text = fram.Analysis.Offset.QC_2StepH2[cb_Offset.SelectedIndex - 3].ToString();
                    tb_2StepW2.Text = fram.Analysis.Offset.QC_2StepW2[cb_Offset.SelectedIndex - 3].ToString();
                    tb_PT_1StepH.Text = fram.Analysis.Offset.QC_PT_1StepH[cb_Offset.SelectedIndex - 3].ToString();
                    tb_PT_1StepW.Text = fram.Analysis.Offset.QC_PT_1StepW[cb_Offset.SelectedIndex - 3].ToString();
                    tb_PT_2StepH1.Text = fram.Analysis.Offset.QC_PT_2StepH1[cb_Offset.SelectedIndex - 3].ToString();
                    tb_PT_2StepW1.Text = fram.Analysis.Offset.QC_PT_2StepW1[cb_Offset.SelectedIndex - 3].ToString();
                    tb_PT_2StepH2.Text = fram.Analysis.Offset.QC_PT_2StepH2[cb_Offset.SelectedIndex - 3].ToString();
                    tb_PT_2StepW2.Text = fram.Analysis.Offset.QC_PT_2StepW2[cb_Offset.SelectedIndex - 3].ToString();
                    tb_BlueTapeW.Text = fram.Analysis.Offset.QC_BlueTapeW[cb_Offset.SelectedIndex - 3].ToString();
                    break;
            }
        }
    }
}