using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms.DataVisualization.Charting;

namespace TrimGap
{
    public partial class Form1 : Form
    {
        private static MotionControl frmUsrAssign;
        private ColorPalette _cp;
        public static string err = string.Empty;

        public Form1()
        {
            InitializeComponent();
            timer_EFEM_Event.Enabled = true;
        }

        private void InitUI()
        {
            Control[] _ctl;
            for (int i = 0; i < AnalysisData.WaferMeasure.Length; i++)
            {
                AnalysisData.WaferMeasure[i] = false;
            }
            for (int i = 0; i < 8; i++)
            {
                _ctl = this.Controls.Find("pb_" + i, true);
                UI.pbWafer[i] = (PictureBox)_ctl[0];
                UI.pbWafer[i].BackgroundImage = Properties.Resources.red.ToBitmap();
            }
            if (EFEM.IsInit)
            {
                for (int i = 0; i < 25; i++)
                {
                    _ctl = this.panel3.Controls.Find("pictureBox1_" + (i + 1), true);
                    Common.EFEM.LoadPort1.pb[i] = (PictureBox)_ctl[0];
                }
                for (int i = 0; i < 25; i++)
                {
                    _ctl = this.panel4.Controls.Find("pictureBox2_" + (i + 1), true);
                    Common.EFEM.LoadPort2.pb[i] = (PictureBox)_ctl[0];
                }

                _ctl = this.panel5.Controls.Find("lb_Aligner1", true);
                Common.EFEM.Aligner.lb = (Label)_ctl[0];

                _ctl = this.panel5.Controls.Find("pB_Aligner1", true);
                Common.EFEM.Aligner.pb = (PictureBox)_ctl[0];

                _ctl = this.panel5.Controls.Find("lb_Stage1", true);
                Common.EFEM.Stage1.lb = (Label)_ctl[0];

                _ctl = this.panel5.Controls.Find("pB_Stage1", true);
                Common.EFEM.Stage1.pb = (PictureBox)_ctl[0];

                _ctl = this.panel5.Controls.Find("pB_Robot_Arm_Lower", true);
                Common.EFEM.Robot.pb[0] = (PictureBox)_ctl[0];
                _ctl = this.panel5.Controls.Find("pB_Robot_Arm_Upper", true);
                Common.EFEM.Robot.pb[1] = (PictureBox)_ctl[0];

                _ctl = this.panel5.Controls.Find("lb_Arm_lower", true);
                Common.EFEM.Robot.lbwaferinfo[0] = (Label)_ctl[0];
                Common.EFEM.Robot.lbwaferinfo[0].Text = Common.EFEM.Robot.WaferInfo_Lower;
                _ctl = this.panel5.Controls.Find("lb_Arm_upper", true);
                Common.EFEM.Robot.lbwaferinfo[1] = (Label)_ctl[0];
                Common.EFEM.Robot.lbwaferinfo[1].Text = Common.EFEM.Robot.WaferInfo_Upper;

                if (Common.io.In(IOName.In.StageWafer在席) && Common.EFEM.Stage1.Slot == 0)
                {
                    Common.EFEM.Stage1.lb.Text = "Stage1 Sts Error";
                    Common.EFEM.Stage1.pb.BackColor = EFEM.slot_status_Color.Unknow;
                }
                else
                {
                    Common.EFEM.Stage1.pb.BackColor = EFEM.slot_status_Color.AlignerStageReady;
                }
                if (Common.EFEM.Aligner.WaferPresence && Common.EFEM.Aligner.Slot == 0)
                {
                    Common.EFEM.Aligner.lb.Text = "Aligner1 Sts Error";
                    Common.EFEM.Aligner.pb.BackColor = EFEM.slot_status_Color.Unknow;
                }
                else
                {
                    //Common.EFEM.Aligner.pb.BackColor = EFEM.slot_status_Color.AlignerStageReady;
                }
            }

            if (fram.m_Hardware_SF3 > 0)
            {
                //TTVScanPattern frmTTVScan = new TTVScanPattern(fram.Recipe.MotionPatternPath, fram.Recipe.MotionPatternName);
            }
            else
            {
                this.toolStripDropDownButton1.DropDownItems.Remove(tTVToolStripMenuItem);
            }

            if (fram.m_Hardware_CCD == 0)
                this.toolStripDropDownButton1.DropDownItems.Remove(cCDToolStripMenuItem);
        }

        private void InitBW()
        {
            if (!bWUpdatePara.IsBusy)
            {
                bWUpdatePara.RunWorkerAsync();
            }

            if (!bWAnalysis.IsBusy)
            {
                bWAnalysis.RunWorkerAsync();
            }
        }

        #region 系統

        private void Form1_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.Manual; //窗體的位置由Location屬性決定
            this.Location = (Point)new Size(-8, 0);
            this.Text = "FTGM1";
            this.Text += "  Version： " + sram.SofewareVersion;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Common.InitAll();
            /*if(fram.m_Hardware_PT>0)
            {
                ((ISupportInitialize)(Common.actUtlType)).BeginInit();
                this.Controls.Add(Common.actUtlType);
                ((ISupportInitialize)(Common.actUtlType)).EndInit();
            }*/
            Common.Init_chart(chartSensor);
            Common.Init_chart2(chartSensorPt);
            InitUI();
            InsertLog.SavetoDB(1);
            InitBW();
            if (fram.m_MachineType == 0) // AP6
            {
                tabPage_TTVReport.Parent = null;
            }
            if (fram.m_Hardware_CCD == 0)
            {
                tabPage_TapeImg.Parent = null;
            }
            Bitmap bp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed);
            _cp = bp.Palette;
            for (int i = 0; i < 256; i++)
            {
                _cp.Entries[i] = Color.FromArgb(255, i, i, i);
            }
            label2.Text = "Trim Gap Metrology\n大量科技股份有限公司\nVersion: " + sram.ProgramVersion + " / " + sram.AnalysisVersion;
            //Common.EFEM.IO.SetFFUVoltage(0);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AutoRunEFEM.SaveEFEMSts();
            ParamFile.saveparam("EFEMSts");
            Common.Close();
            ParamFile.saveparam("SECSPara");
            InsertLog.SavetoDB(2);
            this.Dispose();
            Environment.Exit(Environment.ExitCode); // 防止程式卡住出不去 強制退出
        }

        private void sECSGEMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Common.SecsgemForm.Show();
            }
            catch (Exception)
            {
            }
        }

        private void iOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Common.io != null)
            {
                Common.io.ShowUI();
            }
            else
            {
                MessageBox.Show("", "IOToolStripMenuItem Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 操作紀錄ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Common.dataBase.ComboBox_Table.SelectedIndex = 0;

            try
            {
                Common.dataBase.Show();
                //dataBase.ShowDialog();
            }
            catch (Exception)
            {
                Common.InitDataBase();
                Common.dataBase.Show();
            }
            //InsertLog.SavetoDB(55, "LogTable"); // Log
        }

        private void signalAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SignalAnalysisForm SignalForm = new SignalAnalysisForm();
                SignalForm.ShowDialog();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void 數據報表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.dataBase.ComboBox_Table.SelectedIndex = 1;

            try
            {
                Common.dataBase.Show();
                //dataBase.ShowDialog();
            }
            catch (Exception)
            {
                Common.InitDataBase();
                Common.dataBase.Show();
            }
            //InsertLog.SavetoDB(55, "LogTable"); // Log
        }

        private void motionControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmUsrAssign = new MotionControl(fram.m_MotionType);
                //tabControl1.SelectedIndex = 0;  //martin
                //this.Visible = false;
                frmUsrAssign.Show();
                //frmUsrAssign.FormClosed += new FormClosedEventHandler(frmUsrAssign_FormClosed);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "MotionControlToolStripMenuItem Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void recipeManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Flag.FormRecipeOpenFlag)
                {
                    RecipeManagement frmUsrAssign = new RecipeManagement();
                    //tabControl1.SelectedIndex = 0;  //martin
                    //this.Visible = false;
                    frmUsrAssign.Show();
                    Flag.FormRecipeOpenFlag = true;
                    //frmUsrAssign.FormClosed += new FormClosedEventHandler(frmUsrAssign_FormClosed);
                }
                else
                {
                    // 如果已開啟，叫到最上面?
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "recipeManagement Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EFEMControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Flag.AutoidleFlag)
                {
                    EFEMControl frmUsrAssign = new EFEMControl();
                    //tabControl1.SelectedIndex = 0;  //martin
                    //this.Visible = false;
                    frmUsrAssign.Show();
                    //frmUsrAssign.FormClosed += new FormClosedEventHandler(frmUsrAssign_FormClosed);
                }
                else if (Flag.AutoidleFlag)
                {
                    MessageBox.Show("自動模式量測中，請先停止後再開啟EFEM Control");
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "EFEMControlToolStripMenuItem Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 使用者權限登入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                UserPermission frmUsrAssign = new UserPermission();
                //tabControl1.SelectedIndex = 0;  //martin
                InsertLog.SavetoDB(53); // User
                frmUsrAssign.Show();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "使用者權限設定ToolStripMenuItem_Click Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eXITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Form1_FormClosing(null, null);
        }

        private void parameterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ParaForm ParaMForm = new ParaForm();
                ParaMForm.ShowDialog();
                Common.Init_chart(chartSensor);
                Common.Init_chart2(chartSensorPt);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        #endregion 系統

        #region 主頁面按鈕

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (Common.SecsgemForm.isRemote())
            {
                return;
            }

            if (Flag.AlarmFlag || !Flag.AllHomeFlag)
            {
                MessageBox.Show("請先完成歸零後再開始");
                return;
            }
            if (EFEM.IsInit)
            {
                if (Common.EFEM.Stage1.pb.BackColor == EFEM.slot_status_Color.Unknow)
                {
                    MessageBox.Show("請先確認Stage Wafer Presence 以及 Slot 狀態，至EFEM Control修改");
                    return;
                }
                if (Common.EFEM.Aligner.pb.BackColor == EFEM.slot_status_Color.Unknow)
                {
                    MessageBox.Show("請先確認Aaligner Wafer Presence 以及 Slot 狀態，至EFEM Control修改");
                    return;
                }
            }

            if (!Flag.AutoidleFlag)
            {
                InsertLog.SavetoDB(14); // Start
                                        //if (Flag.SensorConnectFlag || Flag.VirSensorFlag)
                                        //{
                if (EFEM.IsInit)
                {
                    //Common.EFEM.E84.SetAuto(E84.E84_Num.E841);
                    //Common.EFEM.E84.SetAuto(E84.E84_Num.E842);
                    Common.EFEM.IO.SignalTower(IO.LampState.AllOff);
                    Common.EFEM.IO.SignalTower(IO.LampState.GreenOn);
                }

                if (fram.SECSPara.Loadport1_AccessMode == Mode.Auto.GetHashCode())
                {
                    Common.ChangeRecipe(fram.Recipe.Filename_LP1);
                }
                else if (fram.SECSPara.Loadport2_AccessMode == Mode.Auto.GetHashCode())
                {
                    Common.ChangeRecipe(fram.Recipe.Filename_LP2);
                }

                SpinWait.SpinUntil(() => false, 10);
                Flag.AutoidleFlag = true;
                if (!Common.SecsgemForm.isRemote())
                {
                    Flag.Autoidle_LocalFlag = true;
                }
                InsertLog.SavetoDB(12); // Start Auto

                SpinWait.SpinUntil(() => false, 100);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            PauseAction(false);
        }

        private void PauseAction(bool bRemote)
        {
            Flag.AutoidleFlag = false;
            Flag.Autoidle_LocalFlag = false;

            Common.SaveEFEMSts();
            ParamFile.saveparam("EFEMSts");
            InsertLog.SavetoDB(15); //Stop

            if (Common.EFEM.CmdSend != "" && EFEM.IsInit)
            {
                Console.WriteLine(DateTime.Now.ToString() + "  Stop, Cmd: " + Common.EFEM.CmdSend + ", Rcv Flag: " + Common.EFEM.ReceiveFlag + ", StepDone: " + Flag.EFEMStepDoneFlag);
                if(!bRemote) 
                    MessageBox.Show("動作完成後自動停止");
                SpinWait.SpinUntil(() => Common.EFEM.CmdSend == "" && Common.EFEM.ReceiveFlag && Flag.EFEMStepDoneFlag, 60000);
                Console.WriteLine(DateTime.Now.ToString() + "  Stop OK, Cmd: " + Common.EFEM.CmdSend + ", Rcv Flag: " + Common.EFEM.ReceiveFlag + ", StepDone: " + Flag.EFEMStepDoneFlag);
            }

            if (!Common.io.In(IOName.In.StageWafer在席))
            {
                for (int i = 0; i < sram.Recipe.Rotate_Count; i++)
                {
                    AnalysisData.WaferMeasure[i] = false;
                    sram.PitchAngleTotal = 0;
                }

                for (int i = 0; i < sram.Recipe.Rotate_Count; i++)
                {
                    if (sram.Recipe.Rotate_Count == 4)
                    {
                        UI.pbWafer[i * 2].BackgroundImage = AnalysisData.WaferMeasure[i] == true ? Properties.Resources.green.ToBitmap() : Properties.Resources.red.ToBitmap();
                    }
                    else if (sram.Recipe.Rotate_Count == 8)
                    {
                        UI.pbWafer[i].BackgroundImage = AnalysisData.WaferMeasure[i] == true ? Properties.Resources.green.ToBitmap() : Properties.Resources.red.ToBitmap();
                    }
                }
            }
            if (Flag.AlarmFlag)
            {
                return;
            }
            if (EFEM.IsInit)
            {
                Common.EFEM.IO.SignalTower(IO.LampState.GreenOff);
                Common.EFEM.IO.SignalTower(IO.LampState.YellowOn);
                Common.SaveEFEMSts();
                ParamFile.saveparam("EFEMSts");
            }
        }

        private void btnModeAuto_LP1_Click(object sender, EventArgs e)
        {
            if (Common.SecsgemForm.isRemote()) //Remote
            {
                MessageBox.Show("The current status is remote control");
            }
            else
            {
                if (sram.UserAuthority == permissionEnum.op && fram.SECSPara.Loadport2_AccessMode == Mode.Auto.GetHashCode())
                {
                    MessageBox.Show("LP2 is Auto Mode");
                    return;
                }

                Common.EFEM.E84.Reset(E84.E84_Num.E841);
                Common.EFEM.E84.SetAuto(E84.E84_Num.E841);

                fram.SECSPara.Loadport1_AccessMode = Mode.Auto.GetHashCode();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_AccessMode, (byte)fram.SECSPara.Loadport1_AccessMode, out err);
                InsertLog.SavetoDB(50, "LP1 Switch to Auto");// Auto
            }
        }

        private void btnModeManual_LP1_Click(object sender, EventArgs e)
        {
            if (Common.SecsgemForm.isRemote()) //Remote
            {
                MessageBox.Show("The current status is remote control");
            }
            else
            {
                if (Flag.AutoidleFlag)
                {
                    MessageBox.Show("請先停止後再切換");
                    return;
                }

                Common.EFEM.E84.SetManual(E84.E84_Num.E841);
                Console.WriteLine("LP1 Switch to Manual");
                fram.SECSPara.Loadport1_AccessMode = Mode.Manual.GetHashCode();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_AccessMode, (byte)fram.SECSPara.Loadport1_AccessMode, out err);
                InsertLog.SavetoDB(51, "LP1 Switch to Manual");// Manual
            }
        }

        private void btnModeAuto_LP2_Click(object sender, EventArgs e)
        {
            if (Common.SecsgemForm.isRemote()) //Remote
            {
                MessageBox.Show("The current status is remote control");
            }
            else
            {
                if (sram.UserAuthority == permissionEnum.op && fram.SECSPara.Loadport1_AccessMode == Mode.Auto.GetHashCode())
                {
                    MessageBox.Show("LP1 is Auto Mode");
                    return;
                }

                Common.EFEM.E84.Reset(E84.E84_Num.E842);
                Common.EFEM.E84.SetAuto(E84.E84_Num.E842);

                fram.SECSPara.Loadport2_AccessMode = Mode.Auto.GetHashCode();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_AccessMode, (byte)fram.SECSPara.Loadport2_AccessMode, out err);
                InsertLog.SavetoDB(50, "LP2 Switch to Auto");// Auto
            }
        }

        private void btnModeManual_LP2_Click(object sender, EventArgs e)
        {
            if (Common.SecsgemForm.isRemote()) //Remote
            {
                MessageBox.Show("The current status is remote control");
            }
            else
            {
                if (Flag.AutoidleFlag)
                {
                    MessageBox.Show("請先停止後再切換");
                    return;
                }
                Common.EFEM.E84.SetManual(E84.E84_Num.E842);
                fram.SECSPara.Loadport2_AccessMode = Mode.Manual.GetHashCode();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_AccessMode, (byte)fram.SECSPara.Loadport2_AccessMode, out err);
                InsertLog.SavetoDB(51, "LP2 Switch to Manual");// Manual
            }
        }

        #endregion 主頁面按鈕

        private void bWUpdatePara_DoWork(object sender, DoWorkEventArgs e)
        {
            bWUpdatePara.WorkerReportsProgress = true;
            int Count = 0; // 可以依照更新頻率 刷新不同的東西
            while (Flag.FormOpenFlag)
            {
                SpinWait.SpinUntil(() => false, 100);

                Count++;

                try
                {
                    bWUpdatePara.ReportProgress(Count);
                }
                catch (Exception ee)
                {
                    Console.WriteLine("bWUpdatePara :" + ee.Message);
                }

                if (Count >= 98) // 100 給find home
                {
                    Count = 0;
                }
            }
        }

        private void bWUpdatePara_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage % 1 == 0) // 每 100ms 刷一次
            {
                ModeLight(); // 右上手自動、開始停止狀態顏色
                if(fram.m_simulateRun == 0)
                {
                    int arrDeviceValue;             // Data for 'DeviceData'
                    arrDeviceValue = Common.PTForm.GetDevice("D100");
                    label1.Text = arrDeviceValue.ToString();
                }
            }

            if (e.ProgressPercentage % 5 == 0) // 每 500ms 刷一次
            {
                lbStatus(); // 更新主頁面下方狀態
            }
            if (e.ProgressPercentage % 10 == 0) // 1秒閃一次
            {
                if (Flag.AlarmFlag) // 有alarm時閃燈號
                {
                    Flag.AllHomeFlag = false;
                    if (btnAllHome.BackColor != Color.Red)
                    {
                        btnAllHome.BackColor = Color.Red;
                    }
                    else
                    {
                        btnAllHome.BackColor = Color.Transparent;
                    }
                }
                else // 沒有alarm時看狀態
                {
                    Flag.EFEMAlarmReportFlag = false;
                    if (!Flag.AllHomeFlag)
                    {
                        if (btnAllHome.BackColor != Color.Red)
                        {
                            btnAllHome.BackColor = Color.Red;
                        }
                        else
                        {
                            btnAllHome.BackColor = Color.Transparent;
                        }
                    }
                    else // 沒有alarm & 已歸零
                    {
                        if (btnAllHome.BackColor != Color.Green)
                        {
                            btnAllHome.BackColor = Color.Green;
                        }
                    }
                }
            }
            if (e.ProgressPercentage % 30 == 0) // 每 3000ms 刷一次
            {
                ChangeAuthority(); // 切換使用者要更改的東西都在裡面
                lbRecipeName.Text = "Recipe Name： " + fram.Recipe.Filename_LP1;
                lbRecipeName2.Text = "Recipe Name： " + fram.Recipe.Filename_LP2;

                if (EFEM.IsInit)
                {
                    lb_FoupID1.Text = "FoupID： " + Common.EFEM.LoadPort1.FoupID;
                    lb_FoupID2.Text = "FoupID： " + Common.EFEM.LoadPort2.FoupID;
                    if (System.Enum.IsDefined(typeof(PortTransferState), fram.SECSPara.Loadport1_PortTransferState)) // 檢查數值有沒有在enum裡
                    {
                        lbFoupStatus.Text = "Status：" + (PortTransferState)fram.SECSPara.Loadport1_PortTransferState;
                    }
                    if (System.Enum.IsDefined(typeof(PortTransferState), fram.SECSPara.Loadport2_PortTransferState))
                    {
                        lbFoupStatus2.Text = "Status：" + (PortTransferState)fram.SECSPara.Loadport2_PortTransferState;
                    }
                }
                lbControlMode(); // 更新右上SECS Remote狀態
            }
        }

        private void ChangeAuthority()
        {
            if (Flag.ChangeAuthority)
            {
                label77.Text = "使用者權限：" + Common.Get_Description(sram.UserAuthority);
                switch (sram.UserAuthority)
                {
                    case permissionEnum.op:

                        InsertLog.SavetoDB(11, "Op");
                        panelTest.Visible = false;
                        break;

                    case permissionEnum.eng:

                        InsertLog.SavetoDB(11, "Eng");
                        panelTest.Visible = true;
                        break;

                    case permissionEnum.ad:

                        InsertLog.SavetoDB(11, "Ad");
                        panelTest.Visible = true;
                        break;

                    default:
                        break;
                }
                Flag.ChangeAuthority = false;
            }
        }

        private void ModeLight()
        {
            #region LP1 Mode light

            if (fram.SECSPara.Loadport1_AccessMode == Mode.Auto.GetHashCode() && btnModeAuto_LP1.BackColor != Color.Lime)
            {
                btnModeAuto_LP1.BackColor = Color.Lime;
                btnModeManual_LP1.BackColor = Color.Transparent;
            }
            else if (fram.SECSPara.Loadport1_AccessMode == Mode.Manual.GetHashCode() && btnModeManual_LP1.BackColor != Color.Lime)
            {
                btnModeAuto_LP1.BackColor = Color.Transparent;
                btnModeManual_LP1.BackColor = Color.Lime;
            }

            #endregion LP1 Mode light

            #region LP2 Mode light

            if (fram.SECSPara.Loadport2_AccessMode == Mode.Auto.GetHashCode() && btnModeAuto_LP2.BackColor != Color.Lime)
            {
                btnModeAuto_LP2.BackColor = Color.Lime;
                btnModeManual_LP2.BackColor = Color.Transparent;
            }
            else if (fram.SECSPara.Loadport2_AccessMode == Mode.Manual.GetHashCode() && btnModeManual_LP2.BackColor != Color.Lime)
            {
                btnModeAuto_LP2.BackColor = Color.Transparent;
                btnModeManual_LP2.BackColor = Color.Lime;
            }

            #endregion LP2 Mode light

            if (Flag.AutoidleFlag)
            {
                if (Common.SecsgemForm.isRemote())
                {
                    if (btnStart.BackColor != Color.DarkGreen)
                    {
                        btnStart.BackColor = Color.DarkGreen;
                        btnStop.BackColor = Color.Transparent;
                    }
                }
                else
                {
                    if (Flag.Autoidle_LocalFlag)
                    {
                        if (btnStart.BackColor != Color.DarkGreen)
                        {
                            btnStart.BackColor = Color.DarkGreen;
                            btnStop.BackColor = Color.Transparent;
                        }
                    }
                    else
                    {
                        if (btnStop.BackColor != Color.PaleVioletRed)
                        {
                            btnStart.BackColor = Color.Transparent;
                            btnStop.BackColor = Color.PaleVioletRed;
                        }
                    }
                }
            }
            else
            {
                if (btnStop.BackColor != Color.PaleVioletRed)
                {
                    btnStart.BackColor = Color.Transparent;
                    btnStop.BackColor = Color.PaleVioletRed;
                }
            }
        }

        private void lbControlMode()
        {
            if (Common.SecsgemForm.isCommunicating()) //1:DISABLE, 3:NOT_COMMUNICATING, 7:COMMUNICATING
            {
                if (Common.SecsgemForm.isControlRemote()) // 3:Offline, 4:Local, 5:Remote
                {
                    lbControlStatus.Text = "Remote";
                    lbControlStatus.BackColor = Color.Green;
                }
                else if (Common.SecsgemForm.isControlLocal())
                {
                    lbControlStatus.Text = "Local";
                    lbControlStatus.BackColor = Color.Yellow;
                }
                else
                {
                    lbControlStatus.Text = "OffLine";
                    lbControlStatus.BackColor = Color.Red;
                }
            }
            else
            {
                lbControlStatus.Text = "OffLine";
                lbControlStatus.BackColor = Color.Red;
            }
        }

        private void lbStatus()
        {
            string MeasureMode = "";

            MeasureMode = sram.Mode == (int)Mode.Auto ? "自動" : "手動";
            tslbStatus.Text = "";
            tslbStatus.Text += "Status： ";
            tslbStatus.Text += DateTime.Now.ToString();
            tslbStatus.Text += ", 使用者：" + Common.Get_Description(sram.UserAuthority) + " , 量測模式：" + MeasureMode;
            tslbStatus.Text += " , LP1步驟： " + Common.Get_Description(AutoRunEFEM.AutoRunEFEMStep1); // LP1的步驟
            tslbStatus.Text += " , LP2步驟： " + Common.Get_Description(AutoRunEFEM.AutoRunEFEMStep2); // LP2的步驟
            tslbStatus.Text += " , 量測步驟： " + Common.Get_Description(AutoRunStage.AutoRunStageStep); // Stage 流程的步驟

            if (AutoRunStage.AutoRunStageStep == AutoRunStage.AutoStep.TrimMode)
            {
                tslbStatus.Text += " , Trim： " + Common.Get_Description(AutoRunStage.AutoTrimStep);  // Trim 流程的步驟
            }
            else if (AutoRunStage.AutoRunStageStep == AutoRunStage.AutoStep.TrimMode2nd)
            {
                tslbStatus.Text += " , Trim： " + Common.Get_Description(AutoRunStage.AutoTrim2ndStep);  // Trim2 流程的步驟
            }
            else if (AutoRunStage.AutoRunStageStep == AutoRunStage.AutoStep.BlueTapeMode)
            {
                tslbStatus.Text += " , BlueTape： " + Common.Get_Description(AutoRunStage.AutoBlueTapeStep);  // taping 流程的步驟
            }
            else if (AutoRunStage.AutoRunStageStep == AutoRunStage.AutoStep.TTVMode)
            {
                tslbStatus.Text += " , TTV： " + Common.Get_Description(AutoRunStage.AutoTTVStep);   // TTV 流程的步驟
            }
        }

        #region bW

        private void Update_UI_Sts()
        {
            for (int i = 0; i < sram.Recipe.Rotate_Count; i++)
            {
                if (sram.Recipe.Rotate_Count == 4)
                {
                    UI.pbWafer[i * 2].BackgroundImage = AnalysisData.WaferMeasure[i] == true ? Properties.Resources.green.ToBitmap() : Properties.Resources.red.ToBitmap();
                }
                else if (sram.Recipe.Rotate_Count == 8)
                {
                    UI.pbWafer[i].BackgroundImage = AnalysisData.WaferMeasure[i] == true ? Properties.Resources.green.ToBitmap() : Properties.Resources.red.ToBitmap();
                }
            }
        }

        private void bWAnalysis_DoWork(object sender, DoWorkEventArgs e)
        {
            bWAnalysis.WorkerReportsProgress = true;
            bWAnalysis.WorkerSupportsCancellation = true;
            double[] tmp = new double[3200];
            double[] tmp1 = new double[5000];
            double[] tmp2 = new double[5000];
            double[] tmp3 = new double[5000];
            while (Flag.FormOpenFlag)
            {
                SpinWait.SpinUntil(() => false, 1000); // 刷新頻率 200ms
                                                       //sram.spwSensor.SpinOnce();
                                                       //Thread.Sleep(200);
                if (Flag.SensorAnalysisFlag)
                {
                    try
                    {
                        if (sram.Recipe.Type == 0) // taping 不用給rawdata
                        {
                        }
                        else // 1/2 step要給rawdata
                        {
                            if (sram.Recipe.Type == 1 || sram.Recipe.Type == 2 || (sram.Recipe.Type == 3 && Flag.isPT == false))
                            {
                                for (int i = 0; i < AnalysisData.rawData.Length; i++)
                                {
                                    tmp[i] = AnalysisData.rawData[i] * AnalysisData.um2mm * fram.Analysis.Coefficient;
                                }
                            }
                            else if((sram.Recipe.Type == 3 && Flag.isPT == true))
                            {
                                for (int i = 0; i < AnalysisData.rawData.Length; i++)
                                {
                                    tmp1[i] = AnalysisData.rawData[i] ;
                                }

                                for (int i = 0; i < AnalysisData.rawData2.Length; i++)
                                {
                                    tmp2[i] = AnalysisData.rawData2[i];
                                }

                                for (int i = 0; i < AnalysisData.rawData3.Length; i++)
                                {
                                    tmp3[i] = AnalysisData.rawData3[i];
                                }
                            }
                            
                        }

                        if (sram.Recipe.Type == 0) // bluetape
                        {
                            // 預設分析2000條(左右各1000)，去掉極端值後做平均
                            Common.TrimGapAnalysis.CalculateBlueTape(Common.camera.image, Common.camera.Width, Common.camera.Height, 1000, false, sram.Recipe.BlueTapeThreshold, true, out AnalysisData.resultdata_blueW[AnalysisData.rotateCount_current]);
                        }
                        else if (sram.Recipe.Type == 1)
                        {
                            AnalysisData.removezeroData = Common.TrimGapAnalysis.removeZero2_threshold(tmp, fram.Analysis.LJ_StandardPlane);
                            Console.WriteLine("Analysis start:" + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                            Common.TrimGapAnalysis.tilting(false, AnalysisData.removezeroData, AnalysisData.Interval_X, out AnalysisData.tiltingdata_x, out AnalysisData.tiltingdata_y);
                            ParamFile.SaveRawdata_Csv(AnalysisData.tiltingdata_y, "tilting", DateTime.Now);
                            Common.TrimGapAnalysis.CalculateGap(false, AnalysisData.tiltingdata_x, AnalysisData.tiltingdata_y, AnalysisData.Interval_X, sram.Recipe.Type, sram.Recipe.Step1_Range_step1x0, sram.Recipe.Step1_Range_step1x1, 0, 0, sram.Recipe.Range1_Percent, sram.Recipe.Range2_Percent, out AnalysisData.resultdata[AnalysisData.rotateCount_current]);

                            for (int i = 0; i < AnalysisData.resultdata[AnalysisData.rotateCount_current].Count(); i++)
                            {
                                if (Double.NaN.Equals(AnalysisData.resultdata[AnalysisData.rotateCount_current][i]))
                                    AnalysisData.resultdata[AnalysisData.rotateCount_current][i] = 0.0;
                            }

                            Console.WriteLine("Analysis Finish:" + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                        }
                        else if (sram.Recipe.Type == 2 || (sram.Recipe.Type == 3 && Flag.isPT == false))
                        {
                            AnalysisData.removezeroData = Common.TrimGapAnalysis.removeZero2_threshold(tmp, fram.Analysis.LJ_StandardPlane);
                            Console.WriteLine("Analysis start:" + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                            Common.TrimGapAnalysis.tilting(false, AnalysisData.removezeroData, AnalysisData.Interval_X, out AnalysisData.tiltingdata_x, out AnalysisData.tiltingdata_y);
                            ParamFile.SaveRawdata_Csv(AnalysisData.tiltingdata_y, "tilting", DateTime.Now);
                            Common.TrimGapAnalysis.CalculateGap(false, AnalysisData.tiltingdata_x, AnalysisData.tiltingdata_y, AnalysisData.Interval_X, 2, sram.Recipe.Step2_Range_step1x0, sram.Recipe.Step2_Range_step1x1, sram.Recipe.Step2_Range_step2x0, sram.Recipe.Step2_Range_step2x1, sram.Recipe.Range1_Percent, sram.Recipe.Range2_Percent, out AnalysisData.resultdata[AnalysisData.rotateCount_current]);

                            for (int i = 0; i < AnalysisData.resultdata[AnalysisData.rotateCount_current].Count(); i++)
                            {
                                if (Double.NaN.Equals(AnalysisData.resultdata[AnalysisData.rotateCount_current][i]))
                                    AnalysisData.resultdata[AnalysisData.rotateCount_current][i] = 0.0;
                            }

                            Console.WriteLine("Analysis Finish:" + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                        }
                        else if (sram.Recipe.Type == 3 && Flag.isPT == true)
                        {
                            int Wafertype_tmp = 2;
                            int Interval_X_tmp = 1;

                            Common.TrimGapAnalysis.removeZero3(tmp1, tmp2, tmp3, out AnalysisData.removezeroData, out AnalysisData.removezeroData2, out AnalysisData.removezeroData3);
                            Console.WriteLine("Analysis start:" + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                            Common.TrimGapAnalysis.tilting3(false, AnalysisData.removezeroData, AnalysisData.removezeroData2, AnalysisData.removezeroData3, Interval_X_tmp, out AnalysisData.tiltingdata_x, out AnalysisData.tiltingdata_x2, out AnalysisData.tiltingdata_x3, out AnalysisData.tiltingdata_y, out AnalysisData.tiltingdata_y2, out AnalysisData.tiltingdata_y3);
                            ParamFile.SaveRawdata_Csv3(AnalysisData.tiltingdata_y, AnalysisData.tiltingdata_y2, AnalysisData.tiltingdata_y3, "tilting", DateTime.Now);
                            //ParamFile.SaveRawdata_Csv(AnalysisData.tiltingdata_y2, "tilting2", DateTime.Now);
                            //ParamFile.SaveRawdata_Csv(AnalysisData.tiltingdata_y3, "tilting3", DateTime.Now);
                            Common.TrimGapAnalysis.CalculateGap3(false, Wafertype_tmp, AnalysisData.tiltingdata_x, AnalysisData.tiltingdata_x2, AnalysisData.tiltingdata_x3, AnalysisData.tiltingdata_y, AnalysisData.tiltingdata_y2, AnalysisData.tiltingdata_y3, Interval_X_tmp, sram.Recipe.Step2_Range_step1x0, sram.Recipe.Step2_Range_step1x1, sram.Recipe.Step2_Range_step2x0, sram.Recipe.Step2_Range_step2x1, sram.Recipe.Range1_Percent, sram.Recipe.Range2_Percent, out AnalysisData.resultdata[AnalysisData.rotateCount_current]);

                            for (int i = 0; i < AnalysisData.resultdata[AnalysisData.rotateCount_current].Count(); i++)
                            {
                                if (Double.NaN.Equals(AnalysisData.resultdata[AnalysisData.rotateCount_current][i]))
                                    AnalysisData.resultdata[AnalysisData.rotateCount_current][i] = 0.0;
                            }
                            Console.WriteLine("Analysis Finish:" + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                        }



                        switch (sram.Recipe.OffsetType)
                        {
                            case 1:
                                fram.Analysis.Offset1StepH = fram.Analysis.Offset.Offline_1StepH;
                                fram.Analysis.Offset1StepW = fram.Analysis.Offset.Offline_1StepW;
                                fram.Analysis.Offset_PT_1StepH = fram.Analysis.Offset.Offline_PT_1StepH;
                                fram.Analysis.Offset_PT_1StepW = fram.Analysis.Offset.Offline_PT_1StepW;
                                if (sram.Recipe.WaferEdgeEvaluate == 1)
                                    fram.Analysis.Offset1StepW = fram.Analysis.Offset.Offline_EDGE_1StepW;
                                break;

                            case 2:
                                fram.Analysis.Offset2StepH1 = fram.Analysis.Offset.Offline_2StepH1;
                                fram.Analysis.Offset2StepW1 = fram.Analysis.Offset.Offline_2StepW1;
                                fram.Analysis.Offset2StepH2 = fram.Analysis.Offset.Offline_2StepH2;
                                fram.Analysis.Offset2StepW2 = fram.Analysis.Offset.Offline_2StepW2;
                                fram.Analysis.Offset_PT_2StepH1 = fram.Analysis.Offset.Offline_PT_2StepH1;
                                fram.Analysis.Offset_PT_2StepW1 = fram.Analysis.Offset.Offline_PT_2StepW1;
                                fram.Analysis.Offset_PT_2StepH2 = fram.Analysis.Offset.Offline_PT_2StepH2;
                                fram.Analysis.Offset_PT_2StepW2 = fram.Analysis.Offset.Offline_PT_2StepW2;
                                if (sram.Recipe.WaferEdgeEvaluate == 1)
                                    fram.Analysis.Offset2StepW1 = fram.Analysis.Offset.Offline_EDGE_2StepW1;
                                break;

                            case 3:
                                fram.Analysis.Offset1StepH = fram.Analysis.Offset.Inline_1StepH;
                                fram.Analysis.Offset1StepW = fram.Analysis.Offset.Inline_1StepW;
                                fram.Analysis.Offset_PT_1StepH = fram.Analysis.Offset.Inline_PT_1StepH;
                                fram.Analysis.Offset_PT_1StepW = fram.Analysis.Offset.Inline_PT_1StepW;
                                if (sram.Recipe.WaferEdgeEvaluate == 1)
                                    fram.Analysis.Offset1StepW = fram.Analysis.Offset.Inline_EDGE_1StepW;
                                break;

                            case 4:
                                fram.Analysis.Offset2StepH1 = fram.Analysis.Offset.Inline_2StepH1;
                                fram.Analysis.Offset2StepW1 = fram.Analysis.Offset.Inline_2StepW1;
                                fram.Analysis.Offset2StepH2 = fram.Analysis.Offset.Inline_2StepH2;
                                fram.Analysis.Offset2StepW2 = fram.Analysis.Offset.Inline_2StepW2;
                                fram.Analysis.Offset_PT_2StepH1 = fram.Analysis.Offset.Inline_PT_2StepH1;
                                fram.Analysis.Offset_PT_2StepW1 = fram.Analysis.Offset.Inline_PT_2StepW1;
                                fram.Analysis.Offset_PT_2StepH2 = fram.Analysis.Offset.Inline_PT_2StepH2;
                                fram.Analysis.Offset_PT_2StepW2 = fram.Analysis.Offset.Inline_PT_2StepW2;
                                if (sram.Recipe.WaferEdgeEvaluate == 1)
                                    fram.Analysis.Offset2StepW1 = fram.Analysis.Offset.Inline_EDGE_2StepW1;
                                break;

                            case 5: // QC offset
                                if (sram.Recipe.Rotate_Count == 8)
                                {
                                    fram.Analysis.Offset1StepH = fram.Analysis.Offset.QC_1StepH[AnalysisData.rotateCount_current];
                                    fram.Analysis.Offset1StepW = fram.Analysis.Offset.QC_1StepW[AnalysisData.rotateCount_current];
                                    fram.Analysis.Offset_PT_1StepH = fram.Analysis.Offset.QC_PT_1StepH[AnalysisData.rotateCount_current];
                                    fram.Analysis.Offset_PT_1StepW = fram.Analysis.Offset.QC_PT_1StepW[AnalysisData.rotateCount_current];
                                    if (sram.Recipe.WaferEdgeEvaluate == 1)
                                        fram.Analysis.Offset1StepW = fram.Analysis.Offset.QC_EDGE_1StepW[AnalysisData.rotateCount_current];
                                }
                                else
                                {
                                    fram.Analysis.Offset1StepH = fram.Analysis.Offset.QC_1StepH[AnalysisData.rotateCount_current * 2 + 1];
                                    fram.Analysis.Offset1StepW = fram.Analysis.Offset.QC_1StepW[AnalysisData.rotateCount_current * 2 + 1];
                                    fram.Analysis.Offset_PT_1StepH = fram.Analysis.Offset.QC_PT_1StepH[AnalysisData.rotateCount_current * 2 + 1];
                                    fram.Analysis.Offset_PT_1StepW = fram.Analysis.Offset.QC_PT_1StepW[AnalysisData.rotateCount_current * 2 + 1];
                                    if (sram.Recipe.WaferEdgeEvaluate == 1)
                                        fram.Analysis.Offset1StepW = fram.Analysis.Offset.QC_EDGE_1StepW[AnalysisData.rotateCount_current * 2 + 1];
                                }
                                break;

                            case 6:
                                //fram.Analysis.OffsetBlueTapeW = fram.Analysis.Offset.OffsetBlueTapeW;
                                if (sram.Recipe.Rotate_Count == 8)
                                {
                                    fram.Analysis.OffsetBlueTapeW = fram.Analysis.Offset.QC_BlueTapeW[AnalysisData.rotateCount_current];
                                }
                                else
                                {
                                    fram.Analysis.OffsetBlueTapeW = fram.Analysis.Offset.QC_BlueTapeW[AnalysisData.rotateCount_current * 2 + 1];
                                }
                                break;

                            default:
                                if (sram.Recipe.WaferEdgeEvaluate == 1)
                                    fram.Analysis.Offset1StepW = fram.Analysis.Offset_EDGE_1StepW;
                                break;
                        }
                        if (sram.Recipe.Type == 0)
                        {
                            ReportData.H1 = 0;
                            ReportData.W1 = AnalysisData.resultdata_blueW[AnalysisData.rotateCount_current][0] + fram.Analysis.OffsetBlueTapeW;
                            ReportData.H2 = 0;
                            ReportData.W2 = 0;
                        }
                        else if (sram.Recipe.Type == 1)
                        {
                            ReportData.H1 = AnalysisData.resultdata[AnalysisData.rotateCount_current][0] + fram.Analysis.Offset1StepH;
                            ReportData.W1 = AnalysisData.resultdata[AnalysisData.rotateCount_current][1] + fram.Analysis.Offset1StepW;
                            ReportData.H2 = 0;
                            ReportData.W2 = 0;
                            ReportData.Chipping = (int)AnalysisData.resultdata[AnalysisData.rotateCount_current][12];
                            if (sram.Recipe.WaferEdgeEvaluate == 1)
                                ReportData.W1 = ReportData.W1 + (AnalysisData.removezeroData.Length - AnalysisData.tiltingdata_x.Length) * AnalysisData.Interval_X;
                        }
                        else if (sram.Recipe.Type == 2 || (sram.Recipe.Type == 3 && !Flag.isPT))
                        {
                            ReportData.H1 = AnalysisData.resultdata[AnalysisData.rotateCount_current][0] + fram.Analysis.Offset2StepH1;
                            ReportData.W1 = AnalysisData.resultdata[AnalysisData.rotateCount_current][1] + AnalysisData.resultdata[AnalysisData.rotateCount_current][3] + fram.Analysis.Offset2StepW1;
                            ReportData.H2 = AnalysisData.resultdata[AnalysisData.rotateCount_current][2] + AnalysisData.resultdata[AnalysisData.rotateCount_current][0] + fram.Analysis.Offset2StepH2;
                            ReportData.W2 = AnalysisData.resultdata[AnalysisData.rotateCount_current][3] + fram.Analysis.Offset2StepW2;
                            ReportData.Chipping = (int)AnalysisData.resultdata[AnalysisData.rotateCount_current][12];
                            if (sram.Recipe.WaferEdgeEvaluate == 1)
                                ReportData.W1 = ReportData.W1 + (AnalysisData.removezeroData.Length - AnalysisData.tiltingdata_x.Length) * AnalysisData.Interval_X;
                        }
                        else if (sram.Recipe.Type == 3 && Flag.isPT)
                        {
                            ReportData.H1 = AnalysisData.resultdata[AnalysisData.rotateCount_current][0] + fram.Analysis.Offset_PT_2StepH1;
                            ReportData.W1 = AnalysisData.resultdata[AnalysisData.rotateCount_current][1] + AnalysisData.resultdata[AnalysisData.rotateCount_current][3] + fram.Analysis.Offset_PT_2StepW1;
                            ReportData.H2 = AnalysisData.resultdata[AnalysisData.rotateCount_current][2] + AnalysisData.resultdata[AnalysisData.rotateCount_current][0] + fram.Analysis.Offset_PT_2StepH2;
                            ReportData.W2 = AnalysisData.resultdata[AnalysisData.rotateCount_current][3] + fram.Analysis.Offset_PT_2StepW2;
                        }

                        if (sram.Recipe.Type == 3 && Flag.isPT) //僅更新H1 H2
                        {
                            fram.EFEMSts.H1[Common.EFEM.Stage1.Slot - 1, AnalysisData.rotateCount_current] = ReportData.H1;
                            fram.EFEMSts.H2[Common.EFEM.Stage1.Slot - 1, AnalysisData.rotateCount_current] = ReportData.H2;
                        }
                        else
                        {
                            fram.EFEMSts.H1[Common.EFEM.Stage1.Slot - 1, AnalysisData.rotateCount_current] = ReportData.H1;
                            fram.EFEMSts.W1[Common.EFEM.Stage1.Slot - 1, AnalysisData.rotateCount_current] = ReportData.W1;
                            fram.EFEMSts.H2[Common.EFEM.Stage1.Slot - 1, AnalysisData.rotateCount_current] = ReportData.H2;
                            fram.EFEMSts.W2[Common.EFEM.Stage1.Slot - 1, AnalysisData.rotateCount_current] = ReportData.W2;
                        }

                        bWAnalysis.ReportProgress(10); //report在關flag
                    }
                    catch (Exception ee)
                    {
                        ReportData.H1 = 0;
                        ReportData.W1 = 0;
                        ReportData.H2 = 0;
                        ReportData.W2 = 0;
                        fram.EFEMSts.H1[Common.EFEM.Stage1.Slot - 1, AnalysisData.rotateCount_current] = ReportData.H1;
                        fram.EFEMSts.W1[Common.EFEM.Stage1.Slot - 1, AnalysisData.rotateCount_current] = ReportData.W1;
                        fram.EFEMSts.H2[Common.EFEM.Stage1.Slot - 1, AnalysisData.rotateCount_current] = ReportData.H2;
                        fram.EFEMSts.W2[Common.EFEM.Stage1.Slot - 1, AnalysisData.rotateCount_current] = ReportData.W2;
                        bWAnalysis.ReportProgress(20); //report在關flag
                        Console.WriteLine("Analysis Fail:", ee.Message);
                        //InsertLog.SavetoDB(67, "Analysis Fail:" + ee.Message);
                        Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_DataAnalysisError, true, out err);
                    }
                    Flag.SensorAnalysisFlag = false;
                }
            }
        }

        private void bWAnalysis_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 10)
            {
                string Note = "";
                if (ReportData.Chipping == 1)
                {
                    Note = "Chipping";
                }
                else
                {
                    Note = "";
                }
                if (sram.Recipe.Type == 0) // Blue Tape
                {
                    InsertReportTable(
                DateTime.Now,
                ReportData.Lot,
                ReportData.Slot,
                sram.PitchAngleTotal,
                0, //ReportData.H1,
                ReportData.W1,
                0, //ReportData.H2,
                0, //ReportData.W2,
                "OK",
                Note);
                }
                else if (sram.Recipe.Type == 1) // 1step
                {
                    InsertReportTable(
                DateTime.Now,
                ReportData.Lot,
                ReportData.Slot,
                sram.PitchAngleTotal,
                ReportData.H1,
                ReportData.W1,
                0, //ReportData.H2,
                0, //ReportData.W2,
                "OK",
                Note);
                }
                else if (sram.Recipe.Type == 2 || sram.Recipe.Type == 3) // 2 step    //
                {
                    InsertReportTable(
                DateTime.Now,
                ReportData.Lot,
                ReportData.Slot,
                sram.PitchAngleTotal,
                ReportData.H1,
                ReportData.W1,
                ReportData.H2,
                ReportData.W2,
                "OK",
                "");
                }
                else
                {
                    InsertReportTable(
                DateTime.Now,
                ReportData.Lot,
                ReportData.Slot,
                sram.PitchAngleTotal,
                0, 0, 0, 0,
                "Fail",
                Note);
                }
            }
            else if (e.ProgressPercentage == 20)
            {
                InsertReportTable(
                DateTime.Now,
                ReportData.Lot,
                ReportData.Slot,
                sram.PitchAngleTotal,
                ReportData.H1,
                ReportData.W1,
                ReportData.H2,
                ReportData.W2,
                "Fail",
                "");
            }

            #region 計算最大值

            if (AnalysisData.rotateCount_current + 1 == sram.Recipe.Rotate_Count) // 如果是量測最後一個位置，要計算最大值
            {
                double max_H1 = 0, max_H2 = 0, max_W1 = 0, max_W2 = 0;
                for (int i = 0; i < 8; i++)
                {
                    if (fram.EFEMSts.H1[Common.EFEM.Stage1.Slot - 1, i] > max_H1)
                    {
                        max_H1 = fram.EFEMSts.H1[Common.EFEM.Stage1.Slot - 1, i];
                    }
                }
                for (int i = 0; i < 8; i++)
                {
                    if (fram.EFEMSts.H2[Common.EFEM.Stage1.Slot - 1, i] > max_H2)
                    {
                        max_H2 = fram.EFEMSts.H2[Common.EFEM.Stage1.Slot - 1, i];
                    }
                }
                for (int i = 0; i < 8; i++)
                {
                    if (fram.EFEMSts.W1[Common.EFEM.Stage1.Slot - 1, i] > max_W1)
                    {
                        max_W1 = fram.EFEMSts.W1[Common.EFEM.Stage1.Slot - 1, i];
                    }
                }
                for (int i = 0; i < 8; i++)
                {
                    if (fram.EFEMSts.W2[Common.EFEM.Stage1.Slot - 1, i] > max_W2)
                    {
                        max_W2 = fram.EFEMSts.W2[Common.EFEM.Stage1.Slot - 1, i];
                    }
                }
                string Slot_Info = "";

                SpinWait.SpinUntil(() => false, 1000);
                if (sram.Recipe.Type == 0)
                {
                    InsertReportTable(
                DateTime.Now,
                ReportData.Lot,
                ReportData.Slot,
                0,
                0,
                0,                   //max_W1,  20230628
                0, //ReportData.H2,
                0, //ReportData.W2,
                "OK",
                "Max");
                }
                else if (sram.Recipe.Type == 1) // 1step
                {
                    InsertReportTable(
                DateTime.Now,
                ReportData.Lot,
                ReportData.Slot,
                0,
                max_H1,
                max_W1,
                0, //ReportData.H2,
                0, //ReportData.W2,
                "OK",
                "Max");
                }
                else if (sram.Recipe.Type == 2 || (sram.Recipe.Type == 3 && !Flag.isPT)) // 2 step    //
                {
                    InsertReportTable(
                DateTime.Now,
                ReportData.Lot,
                ReportData.Slot,
                0,
                max_H1,
                max_W1,
                max_H2,
                max_W2,
                "OK",
                "Max");
                }
                else if (sram.Recipe.Type == 3 && Flag.isPT) // 2 step    //
                {
                    InsertReportTable(
                DateTime.Now,
                ReportData.Lot,
                ReportData.Slot,
                0,
                max_H1,
                0,
                max_H2,
                0,
                "OK",
                "Max-PT");
                }
            }

            #endregion 計算最大值

            #region Update Chart
            double dif_H = 0;
            double dif_W = 0;
            double _w1 = 0;
            double _w2 = 0;
            double _h1 = 0;
            double _h2 = 0;

            if (sram.Recipe.Type == 0)
            {
                pB_BlueTape.Image = ImageFromRawBgraArray(Common.camera.image, Common.camera.Width, Common.camera.Height, PixelFormat.Format8bppIndexed);
                pB_BlueTape.Image.Palette = _cp;
                ParamFile.SaveImg_png(pB_BlueTape.Image, Common.EFEM.LoadPort_Run.FoupID + "_" + Common.EFEM.Stage1.Slot + "_" + sram.PitchAngleTotal, DateTime.Now);
            }
            else // Trim
            {
                Common.Init_chart(chartSensor);
                Common.Init_chart2(chartSensorPt);
                if (AnalysisData.removezeroData != null && AnalysisData.tiltingdata_y != null)
                {
                    double dif = -(AnalysisData.removezeroData.Length - AnalysisData.tiltingdata_x.Length) * 2.5;

                    chartSensor.ChartAreas[0].AxisX.Minimum = -500;
                    chartSensor.ChartAreas[0].AxisY.Minimum = -10;
                    if (AnalysisData.tiltingdata_y.Min() < -10)
                    {
                        chartSensor.ChartAreas[0].AxisY.Minimum = ((int)(AnalysisData.tiltingdata_y.Min() / 10) - 1) * 10;     //設定Y軸最小值
                    }

                    if (AnalysisData.removezeroData.Max() + Math.Abs(AnalysisData.removezeroData.Min()) > AnalysisData.tiltingdata_y.Max())
                    {
                        if (AnalysisData.removezeroData.Max() / 10 < 10)
                        {
                            chartSensor.ChartAreas[0].AxisY.Maximum = ((int)((AnalysisData.removezeroData.Max() + Math.Abs(AnalysisData.removezeroData.Min())) / 10) + 1) * 10;
                        }
                        else
                        {
                            chartSensor.ChartAreas[0].AxisY.Maximum = ((int)((AnalysisData.removezeroData.Max() + Math.Abs(AnalysisData.removezeroData.Min())) / 100) + 1) * 100;
                        }
                    }
                    else
                    {
                        if (AnalysisData.tiltingdata_y.Max() / 10 < 10)
                        {
                            chartSensor.ChartAreas[0].AxisY.Maximum = ((int)(AnalysisData.tiltingdata_y.Max() / 10) + 1) * 10;
                        }
                        else
                        {
                            chartSensor.ChartAreas[0].AxisY.Maximum = ((int)(AnalysisData.tiltingdata_y.Max() / 100) + 1) * 100;
                        }
                    }

                    if (chartSensor.ChartAreas[0].AxisY.Maximum - chartSensor.ChartAreas[0].AxisY.Minimum > 100)
                    {
                        chartSensor.ChartAreas[0].AxisY.Interval = 100; //設定Y軸間隔 最大值/10
                    }
                    else
                    {
                        chartSensor.ChartAreas[0].AxisY.Interval = 10; //設定Y軸間隔 最大值/10
                    }

                    for (int i = 0; i < AnalysisData.tiltingdata_y.Length; i++)
                    {
                        chartSensor.Series[0].Points.AddXY(AnalysisData.tiltingdata_x[i] + dif, AnalysisData.removezeroData[i] - AnalysisData.removezeroData.Min());
                        chartSensor.Series[1].Points.AddXY(AnalysisData.tiltingdata_x[i], AnalysisData.tiltingdata_y[i]);
                    }
                    if (AnalysisData.resultdata[AnalysisData.rotateCount_current] != null)
                    {
                        if (sram.Recipe.Type == 1)
                        {
                            chartSensor.Series[2].BorderDashStyle = ChartDashStyle.Dot;

                            chartSensor.Series[2].Points.AddXY(0, AnalysisData.resultdata[AnalysisData.rotateCount_current][6]);
                            chartSensor.Series[2].Points.AddXY(AnalysisData.tiltingdata_x[AnalysisData.tiltingdata_x.Length - 1], AnalysisData.resultdata[AnalysisData.rotateCount_current][6]);
                            chartSensor.Series[3].Points.AddXY(0, AnalysisData.resultdata[AnalysisData.rotateCount_current][7]);
                            chartSensor.Series[3].Points.AddXY(AnalysisData.tiltingdata_x[AnalysisData.tiltingdata_x.Length - 1], AnalysisData.resultdata[AnalysisData.rotateCount_current][7]);
                            //chartSignal.Series[4].Points.AddXY(0, SignalPlotData.resultdata[8]);
                            //chartSignal.Series[4].Points.AddXY(SignalPlotData.tiltingdata_x[SignalPlotData.tiltingdata_x.Length - 1], SignalPlotData.resultdata[8]);
                            if (sram.Recipe.WaferEdgeEvaluate == 1)
                            {
                                chartSensor.Series[5].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][9]+ dif, 0);
                                chartSensor.Series[5].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][9]+ dif, AnalysisData.tiltingdata_y.Min());
                            }
                            else
                            {
                                chartSensor.Series[5].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][9], 0);
                                chartSensor.Series[5].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][9], AnalysisData.tiltingdata_y.Min());
                            }
                            chartSensor.Series[6].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][10], 0);
                            chartSensor.Series[6].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][10], AnalysisData.tiltingdata_y.Min());
                            //chartSignal.Series[7].Points.AddXY(SignalPlotData.resultdata[11], 0);
                            //chartSignal.Series[7].Points.AddXY(SignalPlotData.resultdata[11], SignalPlotData.tiltingdata_y.Min());
                            _h1 = AnalysisData.resultdata[AnalysisData.rotateCount_current][0] + fram.Analysis.Offset1StepH;
                            _w1 = AnalysisData.resultdata[AnalysisData.rotateCount_current][1] + fram.Analysis.Offset1StepW;
                            if (sram.Recipe.WaferEdgeEvaluate == 1)
                                _w1 = AnalysisData.resultdata[AnalysisData.rotateCount_current][1] + fram.Analysis.Offset1StepW - dif;
                            else
                                _w1 = AnalysisData.resultdata[AnalysisData.rotateCount_current][1] + fram.Analysis.Offset1StepW;
                            ParamFile.SaveRawdata_png(chartSensor, Common.EFEM.LoadPort_Run.FoupID + "_" + Common.EFEM.Stage1.Slot + "_" + sram.PitchAngleTotal, DateTime.Now, dif_H.ToString(), dif_W.ToString(), _w1, _w2, _h1, _h2);
                        }
                        else if (sram.Recipe.Type == 2 || (sram.Recipe.Type == 3 && Flag.isPT == false))
                        {
                            chartSensor.Series[2].Points.AddXY(0, AnalysisData.resultdata[AnalysisData.rotateCount_current][6]);
                            chartSensor.Series[2].Points.AddXY(AnalysisData.tiltingdata_x[AnalysisData.tiltingdata_x.Length - 1], AnalysisData.resultdata[AnalysisData.rotateCount_current][6]);
                            chartSensor.Series[3].Points.AddXY(0, AnalysisData.resultdata[AnalysisData.rotateCount_current][7]);
                            chartSensor.Series[3].Points.AddXY(AnalysisData.tiltingdata_x[AnalysisData.tiltingdata_x.Length - 1], AnalysisData.resultdata[AnalysisData.rotateCount_current][7]);
                            chartSensor.Series[4].Points.AddXY(0, AnalysisData.resultdata[AnalysisData.rotateCount_current][8]);
                            chartSensor.Series[4].Points.AddXY(AnalysisData.tiltingdata_x[AnalysisData.tiltingdata_x.Length - 1], AnalysisData.resultdata[AnalysisData.rotateCount_current][8]);
                            if (sram.Recipe.WaferEdgeEvaluate == 1)
                            {
                                chartSensor.Series[5].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][9] + dif, 0);
                                chartSensor.Series[5].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][9] + dif, AnalysisData.tiltingdata_y.Min());
                            }
                            else
                            {
                                chartSensor.Series[5].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][9], 0);
                                chartSensor.Series[5].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][9], AnalysisData.tiltingdata_y.Min());
                            }
                            chartSensor.Series[6].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][10], 0);
                            chartSensor.Series[6].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][10], AnalysisData.tiltingdata_y.Min());
                            chartSensor.Series[7].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][11], 0);
                            chartSensor.Series[7].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][11], AnalysisData.tiltingdata_y.Min());
                            _h1 = AnalysisData.resultdata[AnalysisData.rotateCount_current][0] + fram.Analysis.Offset2StepH1;
                            if (sram.Recipe.WaferEdgeEvaluate == 1)
                            {
                                _w1 = AnalysisData.resultdata[AnalysisData.rotateCount_current][1] + AnalysisData.resultdata[AnalysisData.rotateCount_current][3] + fram.Analysis.Offset2StepW1 - dif;
                                _w2 = AnalysisData.resultdata[AnalysisData.rotateCount_current][3] + fram.Analysis.Offset2StepW2 - dif;
                            }                               
                            else
                            {
                                _w1 = AnalysisData.resultdata[AnalysisData.rotateCount_current][1] + AnalysisData.resultdata[AnalysisData.rotateCount_current][3] + fram.Analysis.Offset2StepW1;
                                _w2 = AnalysisData.resultdata[AnalysisData.rotateCount_current][3] + fram.Analysis.Offset2StepW2;
                            }                               
                            _h2 = AnalysisData.resultdata[AnalysisData.rotateCount_current][2] + AnalysisData.resultdata[AnalysisData.rotateCount_current][0] + fram.Analysis.Offset2StepH2;
                            


                            ParamFile.SaveRawdata_png(chartSensor, Common.EFEM.LoadPort_Run.FoupID + "_" + Common.EFEM.Stage1.Slot + "_" + sram.PitchAngleTotal, DateTime.Now, dif_H.ToString(), dif_W.ToString(), _w1, _w2, _h1, _h2);

                        }
                        else if (sram.Recipe.Type == 3 && Flag.isPT == true)
                        {
                            chartSensorPt.Series[0].ChartType = SeriesChartType.Point;
                            chartSensorPt.Series[0].Color = Color.Yellow;
                            chartSensorPt.Series[1].ChartType = SeriesChartType.Point;
                            chartSensorPt.Series[1].Color = Color.Red;
                            chartSensorPt.Series[2].ChartType = SeriesChartType.Point;
                            chartSensorPt.Series[2].Color = Color.Green;
                            for (int i = 0; i < AnalysisData.tiltingdata_y.Length; i++)
                            {
                                chartSensorPt.Series[0].Points.AddXY(AnalysisData.tiltingdata_x[i], AnalysisData.tiltingdata_y[i]);
                            }

                            for (int i = 0; i < AnalysisData.tiltingdata_y2.Length; i++)
                            {
                                chartSensorPt.Series[1].Points.AddXY(AnalysisData.tiltingdata_x2[i], AnalysisData.tiltingdata_y2[i]);
                            }

                            for (int i = 0; i < AnalysisData.tiltingdata_y3.Length; i++)
                            {
                                chartSensorPt.Series[2].Points.AddXY(AnalysisData.tiltingdata_x3[i], AnalysisData.tiltingdata_y3[i]);
                            }

                            chartSensorPt.Series[3].Points.AddXY(0, AnalysisData.resultdata[AnalysisData.rotateCount_current][6]);
                            chartSensorPt.Series[3].Points.AddXY(AnalysisData.tiltingdata_x[AnalysisData.tiltingdata_x.Length - 1], AnalysisData.resultdata[AnalysisData.rotateCount_current][6]);
                            chartSensorPt.Series[4].Points.AddXY(0, AnalysisData.resultdata[AnalysisData.rotateCount_current][7]);
                            chartSensorPt.Series[4].Points.AddXY(AnalysisData.tiltingdata_x[AnalysisData.tiltingdata_x.Length - 1], AnalysisData.resultdata[AnalysisData.rotateCount_current][7]);
                            chartSensorPt.Series[5].Points.AddXY(0, AnalysisData.resultdata[AnalysisData.rotateCount_current][8]);
                            chartSensorPt.Series[5].Points.AddXY(AnalysisData.tiltingdata_x[AnalysisData.tiltingdata_x.Length - 1], AnalysisData.resultdata[AnalysisData.rotateCount_current][8]);
                            chartSensorPt.Series[6].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][9], 0);
                            chartSensorPt.Series[6].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][9], AnalysisData.tiltingdata_y.Min());
                            chartSensorPt.Series[7].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][10], 0);
                            chartSensorPt.Series[7].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][10], AnalysisData.tiltingdata_y.Min());
                            chartSensorPt.Series[8].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][11], 0);
                            chartSensorPt.Series[8].Points.AddXY(AnalysisData.resultdata[AnalysisData.rotateCount_current][11], AnalysisData.tiltingdata_y.Min());
                            _h1 = AnalysisData.resultdata[AnalysisData.rotateCount_current][0] + fram.Analysis.Offset2StepH1;
                            _w1 = AnalysisData.resultdata[AnalysisData.rotateCount_current][1] + AnalysisData.resultdata[AnalysisData.rotateCount_current][3] + fram.Analysis.Offset2StepW1;
                            _h2 = AnalysisData.resultdata[AnalysisData.rotateCount_current][2] + AnalysisData.resultdata[AnalysisData.rotateCount_current][0] + fram.Analysis.Offset2StepH2;
                            _w2 = AnalysisData.resultdata[AnalysisData.rotateCount_current][3] + fram.Analysis.Offset2StepW2;

                            ParamFile.SaveRawdata_png(chartSensorPt, Common.EFEM.LoadPort_Run.FoupID + "_" + Common.EFEM.Stage1.Slot + "_" + sram.PitchAngleTotal, DateTime.Now, dif_H.ToString(), dif_W.ToString(), _w1, _w2, _h1, _h2);
                        }
                    }
                    //ParamFile.SaveRawdata_png(chartSensor, Common.EFEM.LoadPort_Run.FoupID + "_" + Common.EFEM.Stage1.Slot + "_" + sram.PitchAngleTotal, DateTime.Now);
                    //ParamFile.SaveRawdata_png(chartSensor, Common.EFEM.LoadPort_Run.FoupID + "_" + Common.EFEM.Stage1.Slot + "_" + sram.PitchAngleTotal, DateTime.Now, dif_H.ToString(), dif_W.ToString(), _w1, _w2, _h1, _h2);
                }
            }

            #endregion Update Chart

            Update_DataGridVeiw_Report();
            ReportData.ClearResult();

            Flag.SaveChartFlag = true;
        }

        public Image ImageFromRawBgraArray(byte[] arr, int width, int height, PixelFormat pixelFormat)
        {
            var output = new Bitmap(width, height, pixelFormat);
            var rect = new Rectangle(0, 0, width, height);
            var bmpData = output.LockBits(rect, ImageLockMode.ReadWrite, output.PixelFormat);

            // Row-by-row copy
            var arrRowLength = width * Image.GetPixelFormatSize(output.PixelFormat) / 8;
            var ptr = bmpData.Scan0;
            for (var i = 0; i < height; i++)
            {
                Marshal.Copy(arr, i * arrRowLength, ptr, arrRowLength);
                ptr += bmpData.Stride;
            }

            output.UnlockBits(bmpData);
            return output;
        }

        public void Update_DataGridVeiw_Report()
        {
            try
            {
                if (tabControl1.SelectedTab.Text == "Trim")
                {
                    dataGridView_Report.Rows.Clear();
                    Common.dataBase.sQ.SearchData("reportTable",
                                            DateTime.Now.ToString("yyyy-MM-dd 00:00:00"),
                                            DateTime.Now.ToString("yyyy-MM-dd 23:59:59"),
                                            "Time",
                                            SQLite.SQLiteMethod.SortRule.DESC);
                }
                else if (tabControl1.SelectedTab.Text == "TTV")
                {
                    dataGridView_ReportTTV.Rows.Clear();
                    Common.dataBase.sQ.SearchData("reportTableTTV",
                                            DateTime.Now.ToString("yyyy-MM-dd 00:00:00"),
                                            DateTime.Now.ToString("yyyy-MM-dd 23:59:59"),
                                            "Time",
                                            SQLite.SQLiteMethod.SortRule.DESC);
                }
                else
                {
                    return;
                }

                DataRow[] drArr1 = Common.dataBase.sQ.dataTable.Select();
                if (drArr1.Length > 0)
                {
                    //Console.WriteLine(drArr1[0].ItemArray.Length);
                    dataGridView_Report.Rows.Clear();
                    int length = drArr1.Length > 15 ? 15 : drArr1.Length;

                    string[] tmp = new string[drArr1[0].ItemArray.Length];
                    string[] Headertext = new string[drArr1[0].ItemArray.Length];

                    for (int i = 0; i < length; i++)
                    {
                        for (int j = 0; j < tmp.Length; j++)
                        {
                            tmp[j] = drArr1[i].ItemArray[j].ToString();
                        }
                        if (tabControl1.SelectedTab.Text == "Trim")
                        {
                            dataGridView_Report.Rows.Add(tmp);
                        }
                        else if (tabControl1.SelectedTab.Text == "TTV")
                        {
                            dataGridView_ReportTTV.Rows.Add(tmp);
                        }
                    }
                }
                Common.dataBase.sQ.dataTable.Clear();
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                InsertLog.SavetoDB(7, ee.Message);    // 修改系統參數 拿來緊急用
            }
        }

        private void InsertReportTable(DateTime Time, string Lotnum, int slot, double Angle, double H1, double W1, double H2, double W2, string Result, string NOTE)
        {
            object[,] Data = new object[,] {
                        {"Time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},
                        {"Lot", Lotnum },
                        {"Slot", slot },
                        {"Angle", Angle },
                        {"H1", H1 },
                        {"W1", W1 },
                        {"H2", H2 },
                        {"W2", W2 },
                        {"Result",  Result},
                        {"Note", NOTE } };
            Common.dataBase.sQ.InsertData("reportTable", Data);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Update_DataGridVeiw_Report();
        }

        #endregion bW

        private void cbVirSensor_CheckedChanged(object sender, EventArgs e)
        {
            if (cbVirSensor.Checked)
            {
                Common.LJ.LJtestMode = true;
            }
            else
            {
                Common.LJ.LJtestMode = false;
            }
        }

        private void btnAllHome_Click(object sender, EventArgs e)
        {
            Flag.AllHome_busyFlag = true;
            if (fram.m_Hardware_PT == 1) Common.PTForm.PointMove(9);
            if (EFEM.IsInit)
            {
                Common.EFEM.CmdRcv_Clear();
                MessageBox.Show("按下確定後開始進行歸零", "", MessageBoxButtons.OK);
                Flag.AllHomeFlag = false;
                Common.findHomePanel.Show();
                Common.findHomePanel.TopMost = true;
            }
            else
            {
                Flag.AllHomeFlag = true;
                Flag.EFEMAlarmReportFlag = false;
                Flag.EQAlarmReportFlag = false;
            }
        }

        private void timer_EFEM_Event_Tick(object sender, EventArgs e)
        {
            if ((Common.SecsgemForm.isRemote() || (!Flag.AutoidleFlag || !Flag.Autoidle_LocalFlag)) && Flag.AlarmFlag && !Flag.EFEMAlarmReportFlag && !Flag.AllHomeFlag) //EFEMAlarmReportFlag 要false才能進來
            {   // 如果是遠端就不看 Auto flag
                if (Common.EFEM.CmdSend == "")
                {
                    Common.EFEM.GetStatus();

                    if (!Common.EFEM.Power_Sts || !Common.EFEM.Pressure_Sts || !Common.EFEM.Vacuum_Sts || Common.EFEM.EMO_Sts || !Common.EFEM.RobotMode_Sts || !Common.io.In(IOName.In.Wafer汽缸_抬起檢))
                    {
                        Flag.EFEMAlarmReportFlag = true;
                    }
                    if (Flag.EQAlarmReportFlag && !Common.io.In(IOName.In.Wafer汽缸_降下檢) || !Common.io.In(IOName.In.真空平台_負壓檢))
                    {
                        Flag.EFEMAlarmReportFlag = true;
                    }

                    if (Flag.EFEMAlarmReportFlag)
                    {
                        Common.EFEM.IO.SignalTower(IO.LampState.AllOff);
                        Common.EFEM.IO.SignalTower(IO.LampState.RedFlash);
                        Common.EFEM.IO.Buzzer(IO.BuzzerSts.On);
                    }
                    if (Flag.EQAlarmReportFlag)
                    {
                        if (!Common.io.In(IOName.In.Wafer汽缸_降下檢))
                        {
                            Flag.EQAlarmReportFlag = false;
                            MessageBox.Show("請檢查 Wafer汽缸_降下檢", "TopMostMessageBox", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        }
                        if (!Common.io.In(IOName.In.真空平台_負壓檢))
                        {
                            Flag.EQAlarmReportFlag = false;
                            MessageBox.Show("請檢查 真空平台_負壓檢", "TopMostMessageBox", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        }
                    }
                    else
                    {
                        if (!Common.io.In(IOName.In.Wafer汽缸_抬起檢))
                        {
                            MessageBox.Show("請檢查 Wafer汽缸_抬起檢", "TopMostMessageBox", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        }
                        if (!Common.EFEM.Power_Sts)
                        {
                            MessageBox.Show("請檢查 機台電源", "TopMostMessageBox", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        }
                        if (!Common.EFEM.Pressure_Sts)
                        {
                            MessageBox.Show("請檢查 正壓源", "TopMostMessageBox", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        }
                        if (!Common.EFEM.Vacuum_Sts)
                        {
                            MessageBox.Show("請檢查 Vaccum", "TopMostMessageBox", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        }
                        if (Common.EFEM.EMO_Sts)
                        {
                            MessageBox.Show("請檢查 EMO", "TopMostMessageBox", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        }
                        if (!Common.EFEM.RobotMode_Sts)
                        {
                            MessageBox.Show("請檢查 Robot狀態", "TopMostMessageBox", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        }
                    }
                }
            }
        }

        private void btn_opendoor_Click(object sender, EventArgs e)
        {
            if (sram.Mode == (int)Mode.Auto)
            {
                MessageBox.Show("自動模式不可開門，請切換至手動模式");
            }
            else
            {
                Common.io.WriteOut(IOName.Out.門鎖電磁閥偵測開啟, true);
                Common.io.WriteOut(IOName.Out.門鎖電磁閥, false);
                Common.io.WriteOut(IOName.Out.門鎖電磁閥偵測開啟, false);
            }
        }

        private void btn_lockdoor_Click(object sender, EventArgs e)
        {
            Common.io.WriteOut(IOName.Out.門鎖電磁閥, true);
            Common.io.WriteOut(IOName.Out.門鎖電磁閥偵測開啟, true);
        }

        private void tTVToolStripMenuItem_Click(object tTVToolStripMenuItemsender, EventArgs e)
        {
            try
            {
                Common.SF3.ShowUI();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString(), "TTVToolStripMenuItem Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cCDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CCDTest ccd = new CCDTest();
            ccd.Show();
        }

        private void btn_FreshDataGridView_Click(object sender, EventArgs e)
        {
            Update_DataGridVeiw_Report();
        }

        private void ndTrimSensorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.PTForm.Show();
        }
    }
}