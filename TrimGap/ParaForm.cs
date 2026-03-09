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
        public static string err = string.Empty;
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
            cb_Offset.Items.Add("Trim0");        //11
            cb_Offset.Items.Add("F2F");          //12
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
            tb_Analysis_LJ_StandardPlane.Text = fram.Analysis.LJ_StandardPlane.ToString();
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
            tb_EDGE_1StepW.Text = fram.Analysis.Offset_EDGE_1StepW.ToString();
            tb_EDGE_2StepW1.Text = fram.Analysis.Offset_EDGE_2StepW1.ToString();
            tb_EDGE_2StepW2.Text = fram.Analysis.Offset_EDGE_2StepW2.ToString();

            tb_Analysis_HTW_StandardPlane.Text = fram.Analysis.HTW_StandardPlane.ToString();
            tb_Analysis_HTW_W2EdgeThreshold.Text = fram.Analysis.HTW_W2EdgeThreshold.ToString();
            tb_Analysis_HTW_H0FromTilt.Text = fram.Analysis.HTW_H0FromTilt.ToString();
            tb_Analysis_HTW_HistogramRange.Text = fram.Analysis.HTW_HistogramRange.ToString();
            tb_Analysis_HTW_SearchMaxDiff.Text = fram.Analysis.HTW_TrimSearchMaxDifference.ToString();
            tb_Analysis_HTW_GroupPoints.Text = fram.Analysis.HTW_GroupPoints.ToString();
            tb_Analysis_HTW_TrimToIntensityShift.Text = fram.Analysis.HTW_TrimToIntensityShift.ToString();

            tb_Analysis_Use_Leveling.Text = fram.Analysis.Use_Leveling.ToString();
            tb_Analysis_nZone.Text = fram.Analysis.nZone.ToString();
            tb_Analysis_Use_Intensity.Text = fram.Analysis.Use_Intensity.ToString();
            tb_Analysis_W2_LJ_Replace_HTW.Text = fram.Analysis.W2_LJ_Replace_HTW.ToString();

            //20250102 修改 - LJ、HTW量測次數
            tb_LJ_Measure_Count.Text = fram.Analysis.LJ_Measure_Count.ToString();
            tb_HTW_Measure_Count.Text = fram.Analysis.HTW_Measure_Count.ToString();
            tb_PT_Measure_Count.Text = fram.Analysis.PT_Measure_Count.ToString();

            //20250407 RD_OFFSET
            tb_RD_H.Text = fram.Analysis.Offset_RD_H.ToString();
            tb_RD_W.Text = fram.Analysis.Offset_RD_W.ToString();

            //20250418 PT_2
            tb_PT_2.Text = fram.Analysis.PT_2.ToString();
            tb_PT_2_X.Text = fram.Analysis.PT_2_X.ToString();
            tb_PT_2_Z_Offset.Text = fram.Analysis.PT_2_Z_Offset.ToString();


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
                        Common.SecsgemForm.UpdateEC(TrimGap_EqpID.MotionRotate, 1, out err);
                    }
                    else
                    {
                        Common.SecsgemForm.UpdateEC(TrimGap_EqpID.MotionRotate, 0, out err);
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
                        Common.SecsgemForm.UpdateEC(TrimGap_EqpID.Cofficient, fram.Analysis.Coefficient, out err);
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
                pattern = "^[+-]?([0-9]+\\.?)?[0-9]+$";
                if (tb_Analysis_LJ_StandardPlane_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Analysis_LJ_StandardPlane_copy.Text, pattern))
                    {
                        MessageBox.Show("LJ_StandardPlane" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "LJ_StandardPlane " + fram.Analysis.LJ_StandardPlane + "->" + tb_Analysis_LJ_StandardPlane_copy.Text);
                        tb_Analysis_LJ_StandardPlane.Text = tb_Analysis_LJ_StandardPlane_copy.Text;
                        fram.Analysis.LJ_StandardPlane = Convert.ToDouble(tb_Analysis_LJ_StandardPlane_copy.Text);
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

                            case 11:
                                fram.Analysis.Offset.Trim0_1StepW = fram.Analysis.Offset1StepW;
                                break;

                            case 12:
                                fram.Analysis.Offset.F2F_1StepW = fram.Analysis.Offset1StepW;
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

                            case 11:
                                fram.Analysis.Offset.Trim0_1StepH = fram.Analysis.Offset1StepH;
                                break;

                            case 12:
                                fram.Analysis.Offset.F2F_1StepH = fram.Analysis.Offset1StepH;
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

                            case 11:
                                fram.Analysis.Offset.Trim0_2StepW1 = fram.Analysis.Offset2StepW1;
                                break;

                            case 12:
                                fram.Analysis.Offset.F2F_2StepW1 = fram.Analysis.Offset2StepW1;
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

                            case 11:
                                fram.Analysis.Offset.Trim0_2StepH1 = fram.Analysis.Offset2StepH1;
                                break;

                            case 12:
                                fram.Analysis.Offset.F2F_2StepH1 = fram.Analysis.Offset2StepH1;
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

                            case 11:
                                fram.Analysis.Offset.Trim0_2StepW2 = fram.Analysis.Offset2StepW2;
                                break;

                            case 12:
                                fram.Analysis.Offset.F2F_2StepW2 = fram.Analysis.Offset2StepW2;
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

                            case 11:
                                fram.Analysis.Offset.Trim0_2StepH2 = fram.Analysis.Offset2StepH2;
                                break;

                            case 12:
                                fram.Analysis.Offset.F2F_2StepH2 = fram.Analysis.Offset2StepH2;
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

                            case 11:
                                fram.Analysis.Offset.Trim0_PT_1StepW = fram.Analysis.Offset_PT_1StepW;
                                break;

                            case 12:
                                fram.Analysis.Offset.F2F_PT_1StepW = fram.Analysis.Offset_PT_1StepW;
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

                            case 11:
                                fram.Analysis.Offset.Trim0_PT_1StepH = fram.Analysis.Offset_PT_1StepH;
                                break;

                            case 12:
                                fram.Analysis.Offset.F2F_PT_1StepH = fram.Analysis.Offset_PT_1StepH;
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

                            case 11:
                                fram.Analysis.Offset.Trim0_PT_2StepW1 = fram.Analysis.Offset_PT_2StepW1;
                                break;

                            case 12:
                                fram.Analysis.Offset.F2F_PT_2StepW1 = fram.Analysis.Offset_PT_2StepW1;
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

                            case 11:
                                fram.Analysis.Offset.Trim0_PT_2StepH1 = fram.Analysis.Offset_PT_2StepH1;
                                break;

                            case 12:
                                fram.Analysis.Offset.F2F_PT_2StepH1 = fram.Analysis.Offset_PT_2StepH1;
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

                            case 11:
                                fram.Analysis.Offset.Trim0_PT_2StepW2 = fram.Analysis.Offset_PT_2StepW2;
                                break;

                            case 12:
                                fram.Analysis.Offset.F2F_PT_2StepW2 = fram.Analysis.Offset_PT_2StepW2;
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

                            case 11:
                                fram.Analysis.Offset.Trim0_PT_2StepH2 = fram.Analysis.Offset_PT_2StepH2;
                                break;

                            case 12:
                                fram.Analysis.Offset.F2F_PT_2StepH2 = fram.Analysis.Offset_PT_2StepH2;
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

                            case 11:
                                fram.Analysis.Offset.Trim0_BlueTapeW = fram.Analysis.OffsetBlueTapeW;
                                break;

                            case 12:
                                fram.Analysis.Offset.F2F_BlueTapeW = fram.Analysis.OffsetBlueTapeW;
                                break;

                            default:
                                fram.Analysis.Offset.QC_BlueTapeW[cb_Offset.SelectedIndex - 3] = fram.Analysis.OffsetBlueTapeW;
                                break;
                        }
                    }
                }
                if (tb_EDGE_1StepW_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_EDGE_1StepW_copy.Text, pattern))
                    {
                        MessageBox.Show("Offset EDGE 1 Step W" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, cb_Offset.SelectedText + " PT W " + fram.Analysis.Offset_EDGE_1StepW + "->" + tb_EDGE_1StepW_copy.Text);
                        tb_EDGE_1StepW.Text = tb_EDGE_1StepW_copy.Text;
                        fram.Analysis.Offset_EDGE_1StepW = Convert.ToDouble(tb_EDGE_1StepW_copy.Text);
                        switch (cb_Offset.SelectedIndex)
                        {
                            case 0:

                                break;

                            case 1:
                                fram.Analysis.Offset.Offline_EDGE_1StepW = fram.Analysis.Offset_EDGE_1StepW;
                                break;

                            case 2:
                                fram.Analysis.Offset.Inline_EDGE_1StepW = fram.Analysis.Offset_EDGE_1StepW;
                                break;

                            case 11:
                                fram.Analysis.Offset.Trim0_EDGE_1StepW = fram.Analysis.Offset_EDGE_1StepW;
                                break;

                            case 12:
                                fram.Analysis.Offset.F2F_EDGE_1StepW = fram.Analysis.Offset_EDGE_1StepW;
                                break;

                            default:
                                fram.Analysis.Offset.QC_EDGE_1StepW[cb_Offset.SelectedIndex - 3] = fram.Analysis.Offset_EDGE_1StepW;
                                break;
                        }
                    }
                }
                if (tb_EDGE_2StepW1_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_EDGE_2StepW1_copy.Text, pattern))
                    {
                        MessageBox.Show("Offset EDGE 2 Step W1" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, cb_Offset.SelectedText + " PT W1 " + fram.Analysis.Offset_EDGE_2StepW1 + "->" + tb_EDGE_2StepW1_copy.Text);
                        tb_EDGE_2StepW1.Text = tb_EDGE_2StepW1_copy.Text;
                        fram.Analysis.Offset_EDGE_2StepW1 = Convert.ToDouble(tb_EDGE_2StepW1_copy.Text);
                        switch (cb_Offset.SelectedIndex)
                        {
                            case 0:

                                break;

                            case 1:
                                fram.Analysis.Offset.Offline_EDGE_2StepW1 = fram.Analysis.Offset_EDGE_2StepW1;
                                break;

                            case 2:
                                fram.Analysis.Offset.Inline_EDGE_2StepW1 = fram.Analysis.Offset_EDGE_2StepW1;
                                break;

                            case 11:
                                fram.Analysis.Offset.Trim0_EDGE_2StepW1 = fram.Analysis.Offset_EDGE_2StepW1;
                                break;

                            case 12:
                                fram.Analysis.Offset.F2F_EDGE_2StepW1 = fram.Analysis.Offset_EDGE_2StepW1;
                                break;

                            default:
                                fram.Analysis.Offset.QC_EDGE_2StepW1[cb_Offset.SelectedIndex - 3] = fram.Analysis.Offset_EDGE_2StepW1;
                                break;
                        }
                    }
                }

                if (tb_EDGE_2StepW2_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_EDGE_2StepW2_copy.Text, pattern))
                    {
                        MessageBox.Show("Offset EDGE 2 Step W2" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, cb_Offset.SelectedText + " PT W1 " + fram.Analysis.Offset_EDGE_2StepW2 + "->" + tb_EDGE_2StepW2_copy.Text);
                        tb_EDGE_2StepW2.Text = tb_EDGE_2StepW2_copy.Text;
                        fram.Analysis.Offset_EDGE_2StepW2 = Convert.ToDouble(tb_EDGE_2StepW2_copy.Text);
                        switch (cb_Offset.SelectedIndex)
                        {
                            case 0:

                                break;

                            case 1:
                                fram.Analysis.Offset.Offline_EDGE_2StepW2 = fram.Analysis.Offset_EDGE_2StepW2;
                                break;

                            case 2:
                                fram.Analysis.Offset.Inline_EDGE_2StepW2 = fram.Analysis.Offset_EDGE_2StepW2;
                                break;
                            case 11:
                                fram.Analysis.Offset.Trim0_EDGE_2StepW2 = fram.Analysis.Offset_EDGE_2StepW2;
                                break;

                            case 12:
                                fram.Analysis.Offset.F2F_EDGE_2StepW2 = fram.Analysis.Offset_EDGE_2StepW2;
                                break;

                            default:
                                fram.Analysis.Offset.QC_EDGE_2StepW2[cb_Offset.SelectedIndex - 3] = fram.Analysis.Offset_EDGE_2StepW2;
                                break;
                        }
                    }
                }
                pattern = "^[+]?([0-9]+\\.?)?[0-9]+$";

                #endregion [Analysis offset] groupbox3

                #region [Analysis] groupbox4

                pattern = "^[+-]?([0-9]+\\.?)?[0-9]+$";
                if (tb_Analysis_HTW_StandardPlane_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Analysis_HTW_StandardPlane_copy.Text, pattern))
                    {
                        MessageBox.Show("LJ_StandardPlane" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "HTW_StandardPlane " + fram.Analysis.HTW_StandardPlane + "->" + tb_Analysis_HTW_StandardPlane_copy.Text);
                        tb_Analysis_HTW_StandardPlane.Text = tb_Analysis_HTW_StandardPlane_copy.Text;
                        fram.Analysis.HTW_StandardPlane = Convert.ToDouble(tb_Analysis_HTW_StandardPlane_copy.Text);
                    }
                }

                if (tb_Analysis_HTW_W2EdgeThreshold_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Analysis_HTW_W2EdgeThreshold_copy.Text, pattern))
                    {
                        MessageBox.Show("LJ_W2EdgeThreshold" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "HTW_W2EdgeThreshold " + fram.Analysis.HTW_W2EdgeThreshold + "->" + tb_Analysis_HTW_W2EdgeThreshold_copy.Text);
                        tb_Analysis_HTW_W2EdgeThreshold.Text = tb_Analysis_HTW_W2EdgeThreshold_copy.Text;
                        fram.Analysis.HTW_W2EdgeThreshold = Convert.ToDouble(tb_Analysis_HTW_W2EdgeThreshold_copy.Text);
                    }
                }

                if (tb_Analysis_HTW_H0FromTilt_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Analysis_HTW_H0FromTilt_copy.Text, pattern))
                    {
                        MessageBox.Show("LJ_H0FromTilt" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "HTW_H0FromTilt " + fram.Analysis.HTW_H0FromTilt + "->" + tb_Analysis_HTW_H0FromTilt_copy.Text);
                        tb_Analysis_HTW_H0FromTilt.Text = tb_Analysis_HTW_H0FromTilt_copy.Text;
                        fram.Analysis.HTW_H0FromTilt = Convert.ToInt32(tb_Analysis_HTW_H0FromTilt_copy.Text);
                    }
                }

                if (tb_Analysis_HTW_HistogramRange_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Analysis_HTW_HistogramRange_copy.Text, pattern))
                    {
                        MessageBox.Show("HTW_HistogramRange" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "HTW_HistogramRange " + fram.Analysis.HTW_HistogramRange + "->" + tb_Analysis_HTW_HistogramRange_copy.Text);
                        tb_Analysis_HTW_HistogramRange.Text = tb_Analysis_HTW_HistogramRange_copy.Text;
                        fram.Analysis.HTW_HistogramRange = Convert.ToDouble(tb_Analysis_HTW_HistogramRange_copy.Text);
                    }
                }

                if (tb_Analysis_HTW_SearchMaxDiff_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Analysis_HTW_SearchMaxDiff_copy.Text, pattern))
                    {
                        MessageBox.Show("HTW_SearchMaxDiff" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "HTW_SearchMaxDiff " + fram.Analysis.HTW_TrimSearchMaxDifference + "->" + tb_Analysis_HTW_SearchMaxDiff_copy.Text);
                        tb_Analysis_HTW_SearchMaxDiff.Text = tb_Analysis_HTW_SearchMaxDiff_copy.Text;
                        fram.Analysis.HTW_TrimSearchMaxDifference = Convert.ToDouble(tb_Analysis_HTW_SearchMaxDiff_copy.Text);
                    }
                }

                if (tb_Analysis_HTW_GroupPoints_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Analysis_HTW_GroupPoints_copy.Text, pattern))
                    {
                        MessageBox.Show("HTW_GroupPoints" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "HTW_GroupPoints " + fram.Analysis.HTW_GroupPoints + "->" + tb_Analysis_HTW_GroupPoints_copy.Text);
                        tb_Analysis_HTW_GroupPoints.Text = tb_Analysis_HTW_GroupPoints_copy.Text;
                        fram.Analysis.HTW_GroupPoints = Convert.ToDouble(tb_Analysis_HTW_GroupPoints_copy.Text);
                    }
                }

                if (tb_Analysis_HTW_TrimToIntensityShift_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Analysis_HTW_TrimToIntensityShift_copy.Text, pattern))
                    {
                        MessageBox.Show("HTW_TrimToIntensityShift" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "HTW_TrimToIntensityShift " + fram.Analysis.HTW_TrimToIntensityShift + "->" + tb_Analysis_HTW_TrimToIntensityShift_copy.Text);
                        tb_Analysis_HTW_TrimToIntensityShift.Text = tb_Analysis_HTW_TrimToIntensityShift_copy.Text;
                        fram.Analysis.HTW_TrimToIntensityShift = Convert.ToDouble(tb_Analysis_HTW_TrimToIntensityShift_copy.Text);
                    }
                }

                if (tb_Analysis_Use_Leveling_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Analysis_Use_Leveling_copy.Text, pattern))
                    {
                        MessageBox.Show("Use_Leveling" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "Use_Leveling " + fram.Analysis.Use_Leveling + "->" + tb_Analysis_Use_Leveling_copy.Text);
                        tb_Analysis_Use_Leveling.Text = tb_Analysis_Use_Leveling_copy.Text;
                        fram.Analysis.Use_Leveling = Convert.ToDouble(tb_Analysis_Use_Leveling_copy.Text);
                    }
                }

                if (tb_Analysis_nZone_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Analysis_nZone_copy.Text, pattern))
                    {
                        MessageBox.Show("nZone" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "nZone " + fram.Analysis.nZone + "->" + tb_Analysis_nZone_copy.Text);
                        tb_Analysis_nZone.Text = tb_Analysis_nZone_copy.Text;
                        fram.Analysis.nZone = Convert.ToDouble(tb_Analysis_nZone_copy.Text);
                    }
                }

                if (tb_Analysis_Use_Intensity_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Analysis_Use_Intensity_copy.Text, pattern))
                    {
                        MessageBox.Show("Use_Intensity" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "Use_Intensity " + fram.Analysis.Use_Intensity + "->" + tb_Analysis_Use_Intensity_copy.Text);
                        tb_Analysis_Use_Intensity.Text = tb_Analysis_Use_Intensity_copy.Text;
                        fram.Analysis.Use_Intensity = Convert.ToDouble(tb_Analysis_Use_Intensity_copy.Text);
                    }
                }

                if (tb_Analysis_W2_LJ_Replace_HTW_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_Analysis_W2_LJ_Replace_HTW_copy.Text, pattern))
                    {
                        MessageBox.Show("W2_LJ_Replace_HTW" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "W2_LJ_Replace_HTW " + fram.Analysis.W2_LJ_Replace_HTW + "->" + tb_Analysis_W2_LJ_Replace_HTW_copy.Text);
                        tb_Analysis_W2_LJ_Replace_HTW.Text = tb_Analysis_W2_LJ_Replace_HTW_copy.Text;
                        fram.Analysis.W2_LJ_Replace_HTW = Convert.ToDouble(tb_Analysis_W2_LJ_Replace_HTW_copy.Text);
                    }
                }

                if (tb_LJ_Measure_Count_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_LJ_Measure_Count_copy.Text, pattern))
                    {
                        MessageBox.Show("LJ 量測次數" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "LJ 量測次數 " + fram.Analysis.LJ_Measure_Count + "->" + tb_LJ_Measure_Count_copy.Text);
                        tb_LJ_Measure_Count.Text = tb_LJ_Measure_Count_copy.Text;
                        fram.Analysis.LJ_Measure_Count = Convert.ToDouble(tb_LJ_Measure_Count_copy.Text);
                    }
                }

                if (tb_HTW_Measure_Count_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_HTW_Measure_Count_copy.Text, pattern))
                    {
                        MessageBox.Show("HTW 量測次數" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "HTW 量測次數 " + fram.Analysis.HTW_Measure_Count + "->" + tb_HTW_Measure_Count_copy.Text);
                        tb_HTW_Measure_Count.Text = tb_HTW_Measure_Count_copy.Text;
                        fram.Analysis.HTW_Measure_Count = Convert.ToDouble(tb_HTW_Measure_Count_copy.Text);
                    }
                }

                if (tb_PT_Measure_Count_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_PT_Measure_Count_copy.Text, pattern))
                    {
                        MessageBox.Show("HTW 量測次數" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "HTW 量測次數 " + fram.Analysis.PT_Measure_Count + "->" + tb_PT_Measure_Count_copy.Text);
                        tb_PT_Measure_Count.Text = tb_PT_Measure_Count_copy.Text;
                        fram.Analysis.PT_Measure_Count = Convert.ToDouble(tb_PT_Measure_Count_copy.Text);
                    }
                }

                if (tb_RD_H_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_RD_H_copy.Text, pattern))
                    {
                        MessageBox.Show("RD Offset H" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "RD Offset H " + fram.Analysis.Offset_RD_H + "->" + tb_RD_H_copy.Text);
                        tb_RD_H.Text = tb_RD_H_copy.Text;
                        fram.Analysis.Offset_RD_H = Convert.ToDouble(tb_RD_H_copy.Text);
                    }
                }

                if (tb_RD_W_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_RD_W_copy.Text, pattern))
                    {
                        MessageBox.Show("RD Offset W" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "RD Offset W " + fram.Analysis.Offset_RD_W + "->" + tb_RD_W_copy.Text);
                        tb_RD_W.Text = tb_RD_W_copy.Text;
                        fram.Analysis.Offset_RD_W = Convert.ToDouble(tb_RD_W_copy.Text);
                    }
                }

                if (tb_PT_2_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_PT_2_copy.Text, pattern))
                    {
                        MessageBox.Show("PT_2" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "PT_2 " + fram.Analysis.PT_2 + "->" + tb_PT_2_copy.Text);
                        tb_PT_2.Text = tb_PT_2_copy.Text;
                        fram.Analysis.PT_2 = Convert.ToDouble(tb_PT_2_copy.Text);
                    }
                }

                if (tb_PT_2_Z_Offset_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_PT_2_Z_Offset_copy.Text, pattern))
                    {
                        MessageBox.Show("PT_2_Z_Offset" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "PT_2_Z_Offset " + fram.Analysis.PT_2_Z_Offset + "->" + tb_PT_2_Z_Offset_copy.Text);
                        tb_PT_2_Z_Offset.Text = tb_PT_2_Z_Offset_copy.Text;
                        fram.Analysis.PT_2_Z_Offset = Convert.ToDouble(tb_PT_2_Z_Offset_copy.Text);
                    }
                }

                if (tb_PT_2_X_copy.Text != "")
                {
                    if (!Regex.IsMatch(tb_PT_2_X_copy.Text, pattern))
                    {
                        MessageBox.Show("PT_2_X" + "資料輸入錯誤");
                        return;
                    }
                    else
                    {
                        InsertLog.SavetoDB(7, "PT_2_X " + fram.Analysis.PT_2_X + "->" + tb_PT_2_X_copy.Text);
                        tb_PT_2_X.Text = tb_PT_2_X_copy.Text;
                        fram.Analysis.PT_2_X = Convert.ToDouble(tb_PT_2_X_copy.Text);
                    }
                }

                #endregion [Analysis] groupbox4

				Flag.isPT_2 = Convert.ToBoolean(fram.Analysis.PT_2);
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
                    tb_EDGE_1StepW.Text = "0";
                    tb_EDGE_2StepW1.Text = "0";
                    tb_EDGE_2StepW2.Text = "0";
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
                    tb_EDGE_1StepW.Text = fram.Analysis.Offset.Offline_EDGE_1StepW.ToString();
                    tb_EDGE_2StepW1.Text = fram.Analysis.Offset.Offline_EDGE_2StepW1.ToString();
                    tb_EDGE_2StepW2.Text = fram.Analysis.Offset.Offline_EDGE_2StepW2.ToString();
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
                    tb_EDGE_1StepW.Text = fram.Analysis.Offset.Inline_EDGE_1StepW.ToString();
                    tb_EDGE_2StepW1.Text = fram.Analysis.Offset.Inline_EDGE_2StepW1.ToString();
                    tb_EDGE_2StepW2.Text = fram.Analysis.Offset.Inline_EDGE_2StepW2.ToString();
                    break;
                case 11:
                    tb_1StepH.Text = fram.Analysis.Offset.Trim0_1StepH.ToString();
                    tb_1StepW.Text = fram.Analysis.Offset.Trim0_1StepW.ToString();
                    tb_2StepH1.Text = fram.Analysis.Offset.Trim0_2StepH1.ToString();
                    tb_2StepW1.Text = fram.Analysis.Offset.Trim0_2StepW1.ToString();
                    tb_2StepH2.Text = fram.Analysis.Offset.Trim0_2StepH2.ToString();
                    tb_2StepW2.Text = fram.Analysis.Offset.Trim0_2StepW2.ToString();
                    tb_PT_1StepH.Text = fram.Analysis.Offset.Trim0_PT_1StepH.ToString();
                    tb_PT_1StepW.Text = fram.Analysis.Offset.Trim0_PT_1StepW.ToString();
                    tb_PT_2StepH1.Text = fram.Analysis.Offset.Trim0_PT_2StepH1.ToString();
                    tb_PT_2StepW1.Text = fram.Analysis.Offset.Trim0_PT_2StepW1.ToString();
                    tb_PT_2StepH2.Text = fram.Analysis.Offset.Trim0_PT_2StepH2.ToString();
                    tb_PT_2StepW2.Text = fram.Analysis.Offset.Trim0_PT_2StepW2.ToString();
                    tb_BlueTapeW.Text = fram.Analysis.Offset.Trim0_BlueTapeW.ToString();
                    tb_EDGE_1StepW.Text = fram.Analysis.Offset.Trim0_EDGE_1StepW.ToString();
                    tb_EDGE_2StepW1.Text = fram.Analysis.Offset.Trim0_EDGE_2StepW1.ToString();
                    tb_EDGE_2StepW2.Text = fram.Analysis.Offset.Trim0_EDGE_2StepW2.ToString();
                    break;

                case 12:
                    tb_1StepH.Text = fram.Analysis.Offset.F2F_1StepH.ToString();
                    tb_1StepW.Text = fram.Analysis.Offset.F2F_1StepW.ToString();
                    tb_2StepH1.Text = fram.Analysis.Offset.F2F_2StepH1.ToString();
                    tb_2StepW1.Text = fram.Analysis.Offset.F2F_2StepW1.ToString();
                    tb_2StepH2.Text = fram.Analysis.Offset.F2F_2StepH2.ToString();
                    tb_2StepW2.Text = fram.Analysis.Offset.F2F_2StepW2.ToString();
                    tb_PT_1StepH.Text = fram.Analysis.Offset.F2F_PT_1StepH.ToString();
                    tb_PT_1StepW.Text = fram.Analysis.Offset.F2F_PT_1StepW.ToString();
                    tb_PT_2StepH1.Text = fram.Analysis.Offset.F2F_PT_2StepH1.ToString();
                    tb_PT_2StepW1.Text = fram.Analysis.Offset.F2F_PT_2StepW1.ToString();
                    tb_PT_2StepH2.Text = fram.Analysis.Offset.F2F_PT_2StepH2.ToString();
                    tb_PT_2StepW2.Text = fram.Analysis.Offset.F2F_PT_2StepW2.ToString();
                    tb_BlueTapeW.Text = fram.Analysis.Offset.F2F_BlueTapeW.ToString();
                    tb_EDGE_1StepW.Text = fram.Analysis.Offset.F2F_EDGE_1StepW.ToString();
                    tb_EDGE_2StepW1.Text = fram.Analysis.Offset.F2F_EDGE_2StepW1.ToString();
                    tb_EDGE_2StepW2.Text = fram.Analysis.Offset.F2F_EDGE_2StepW2.ToString();
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
                    tb_EDGE_1StepW.Text = fram.Analysis.Offset.QC_EDGE_1StepW[cb_Offset.SelectedIndex - 3].ToString();
                    tb_EDGE_2StepW1.Text = fram.Analysis.Offset.QC_EDGE_2StepW1[cb_Offset.SelectedIndex - 3].ToString();
                    tb_EDGE_2StepW2.Text = fram.Analysis.Offset.QC_EDGE_2StepW2[cb_Offset.SelectedIndex - 3].ToString();
                    break;
            }
        }
    }
}