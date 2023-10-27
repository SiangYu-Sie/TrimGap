using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrimGap
{
    public partial class EFEMControl : Form
    {
        public static string API_Mode;
        public static string FoupID;
        public static string err = string.Empty;

        private struct listBox_Str
        {
            public static string API;
            public static string Robot;
            public static string Aligner;
            public static string LoadPort;

            public static string SignalTower;
            public static string E84;
        }

        private struct ComboBox_Str
        {
            public static string Robot_arm;
            public static string Robot_destination;
            public static string Robot_slot;
            public static string LoadPort_port;
            public static string LoadPort_led;
            public static string E84_Num;
        }

        private struct flag
        {
            public static bool API;
            public static bool Robot;
            public static bool Aligner;
            public static bool LoadPort;
            public static bool SignalTower;
            public static bool E84;
            public static bool SendCmd;
        }

        public EFEMControl()
        {
            InitializeComponent();
            InitUI();
            //this.TopMost = true;
            flag.API = false;
            flag.Robot = false;
            flag.Aligner = false;
            flag.LoadPort = false;
            flag.SignalTower = false;
            flag.E84 = false;
            flag.SendCmd = false;
            if (Common.EFEM.CmdSend != "")
            {
                SpinWait.SpinUntil(() => Common.EFEM.ReceiveFlag, 6000);
                if (!Common.EFEM.ReceiveFlag)
                {
                    Common.EFEM.CmdSend_Clear();
                }
            }
        }

        public void InitUI()
        {
            cb_Robot_Arm.SelectedIndex = 0;
            cb_Robot_Destination.SelectedIndex = 2;
            cb_Robot_Slot.SelectedIndex = 0;
            cb_Alignment_Angle.SelectedIndex = 0;
            cb_LP_LEDSts.SelectedIndex = 0;
            cb_LP_Port.SelectedIndex = 2;
            cb_SetPort.SelectedIndex = 0;
            cb_SetSlot.SelectedIndex = 0;
            cb_SetStatus.SelectedIndex = 0;
            cb_Set_Robot.SelectedIndex = 0;
            cb_SetSlot_Robot.SelectedIndex = 0;
            cb_SetStatus_Robot.SelectedIndex = 0;
            listBox_Aligner.SelectedIndex = 0;
            listBox_API.SelectedIndex = 0;
            listBox_LoadPort.SelectedIndex = 0;
            listBox_Robot.SelectedIndex = 0;
            listBox_SignalTower.SelectedIndex = 0;
            listBox_E84.SelectedIndex = 0;
        }

        private void API_Send_Click(object sender, EventArgs e)
        {
            if (!flag.SendCmd)
            {
                listBox_Str.API = listBox_API.Text;
                flag.API = true;
                flag.SendCmd = true;
                bW_SendCmd.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("請等待動作完成");
            }
        }

        private void SignalTower_Send_Click(object sender, EventArgs e)
        {
            if (!flag.SendCmd)
            {
                listBox_Str.SignalTower = listBox_SignalTower.Text;
                flag.SignalTower = true;
                flag.SendCmd = true;
                bW_SendCmd.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("請等待動作完成");
            }
        }

        private void Robot_Send_Click(object sender, EventArgs e)
        {
            if (!flag.SendCmd)
            {
                listBox_Str.Robot = listBox_Robot.Text;
                ComboBox_Str.Robot_slot = cb_Robot_Slot.Text;
                ComboBox_Str.Robot_arm = cb_Robot_Arm.Text;
                ComboBox_Str.Robot_destination = cb_Robot_Destination.Text;
                flag.Robot = true;
                flag.SendCmd = true;
                bW_SendCmd.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("請等待動作完成");
            }
        }

        private void LoadPort_Send_Click(object sender, EventArgs e)
        {
            if (!flag.SendCmd)
            {
                listBox_Str.LoadPort = listBox_LoadPort.Text;
                ComboBox_Str.LoadPort_port = cb_LP_Port.Text;
                ComboBox_Str.LoadPort_led = cb_LP_LEDSts.Text;
                flag.LoadPort = true;
                flag.SendCmd = true;
                bW_SendCmd.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("請等待動作完成");
            }
        }

        private void Aligner_Send_Click(object sender, EventArgs e)
        {
            if (!flag.SendCmd)
            {
                listBox_Str.Aligner = listBox_Aligner.Text;
                flag.Aligner = true;
                flag.SendCmd = true;
                bW_SendCmd.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("請等待動作完成");
            }
        }

        private void E84_Send_Click(object sender, EventArgs e)
        {
            if (!flag.SendCmd)
            {
                listBox_Str.E84 = listBox_E84.Text;
                ComboBox_Str.E84_Num = cb_E84_Num.Text;
                flag.E84 = true;
                flag.SendCmd = true;
                bW_SendCmd.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("請等待動作完成");
            }
        }

        // 完成
        private bool API_send(string listBox_API_Str)
        {
            bool rtn;
            switch (listBox_API_Str)
            {
                case "Remote":
                    if (rtn = Common.EFEM.API.Remote())
                    {
                        API_Mode = "Remote";
                    }
                    else
                    {
                        API_Mode = "";
                    }
                    break;

                case "Local":
                    if (rtn = Common.EFEM.API.Local())
                    {
                        API_Mode = "Local";
                    }
                    else
                    {
                        API_Mode = "";
                    }
                    break;

                case "CurrentMode":
                    API_Mode = Common.EFEM.API.CurrentMode();
                    rtn = true;
                    break;

                default:
                    API_Mode = "";
                    rtn = false;
                    break;
            }
            return rtn;
        }

        // 完成
        private bool SignalTower_send(string listBox_SignalTower_Str)
        {
            bool rtn;
            switch (listBox_SignalTower_Str)
            {
                case "GreenOn":
                    rtn = Common.EFEM.IO.SignalTower(IO.LampState.GreenOn);
                    break;

                case "GreenOff":
                    rtn = Common.EFEM.IO.SignalTower(IO.LampState.GreenOff);
                    break;

                case "GreenFlash":
                    rtn = Common.EFEM.IO.SignalTower(IO.LampState.GreenFlash);
                    break;

                case "YellowOn":
                    rtn = Common.EFEM.IO.SignalTower(IO.LampState.YellowOn);
                    break;

                case "YellowOff":
                    rtn = Common.EFEM.IO.SignalTower(IO.LampState.YellowOff);
                    break;

                case "YellowFlash":
                    rtn = Common.EFEM.IO.SignalTower(IO.LampState.YellowFlash);
                    break;

                case "RedOn":
                    rtn = Common.EFEM.IO.SignalTower(IO.LampState.RedOn);
                    break;

                case "RedOff":
                    rtn = Common.EFEM.IO.SignalTower(IO.LampState.RedOff);
                    break;

                case "RedFlash":
                    rtn = Common.EFEM.IO.SignalTower(IO.LampState.RedFlash);
                    break;

                case "AllOff":
                    rtn = Common.EFEM.IO.SignalTower(IO.LampState.AllOff);
                    break;

                case "AllFlash":
                    rtn = Common.EFEM.IO.SignalTower(IO.LampState.AllFlash);
                    break;

                case "BuzzerOn":
                    rtn = Common.EFEM.IO.Buzzer(IO.BuzzerSts.On);
                    break;

                case "BuzzerOff":
                    rtn = Common.EFEM.IO.Buzzer(IO.BuzzerSts.Off);
                    break;

                default:
                    rtn = false;
                    break;
            }
            return rtn;
            //if (!rtn)
            //{
            //    MessageBox.Show(listBox_SignalTower_Str + " Fail", "TopMostMessageBox", MessageBoxButtons.OKCancel,
            //           MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            //}
        }

        // 剩 get/put 待測試
        private bool Robot_send(string listBox_Robot_Str, string cb_Robot_Slot, string cb_Robot_Destination, string cb_Robot_Arm, string cb_LP_Port)
        {
            bool rtn;
            Robot.ArmID armID;
            Robot.Pn pn_Robot;
            LoadPort loadPort;
            int Slot = Convert.ToInt32(cb_Robot_Slot);
            if (cb_Robot_Arm == "Lower")
            {
                armID = Robot.ArmID.LowerArm;
            }
            else
            {
                armID = Robot.ArmID.UpperArm;
            }
            switch (cb_Robot_Destination)
            {
                case "P1":

                    pn_Robot = Robot.Pn.P1;
                    break;

                case "P2":

                    pn_Robot = Robot.Pn.P2;
                    break;

                case "Pn_Run":
                    if (Common.EFEM.LoadPort_Run != null)
                    {
                        if (Common.EFEM.LoadPort_Run.pn.ToString() == "P1")
                        {
                            pn_Robot = Robot.Pn.P1;
                        }
                        else
                        {
                            pn_Robot = Robot.Pn.P2;
                        }
                    }
                    else
                    {
                        pn_Robot = Robot.Pn.P1;
                    }

                    break;

                case "Aligner1":
                    pn_Robot = Robot.Pn.Aligner1;

                    Common.EFEM.Aligner.AlignerVacuum(Aligner.OnOff.Off);
                    Common.EFEM.Aligner.GetStatus();
                    break;

                case "Stage1":
                    pn_Robot = Robot.Pn.Stage1;
                    break;

                default:
                    pn_Robot = Robot.Pn.Stage1;
                    break;
            }

            switch (cb_LP_Port)
            {
                case "P1":
                    loadPort = Common.EFEM.LoadPort1;
                    break;

                case "P2":
                    loadPort = Common.EFEM.LoadPort2;
                    break;

                case "Pn_Run":
                    loadPort = Common.EFEM.LoadPort_Run;
                    break;

                default:
                    loadPort = Common.EFEM.LoadPort_Run;
                    break;
            }
            switch (listBox_Robot_Str)
            {
                case "GetStatus":
                    rtn = Common.EFEM.Robot.GetStatus();
                    break;

                case "ResetError":
                    rtn = Common.EFEM.Robot.ResetError();
                    break;

                case "Home":
                    rtn = Common.EFEM.Robot.Home();
                    break;

                case "WaferGet":
                    loadPort.GetLEDStatus();
                    if (!loadPort.LED_Persence && !loadPort.LED_Placement)
                    {
                        //report

                        flag.Robot = false;
                        flag.SendCmd = false;
                        MessageBox.Show(loadPort.pn.ToString() + " Not Ready");
                        return false;
                    }
                    if (cb_Robot_Destination == "Aligner1")
                    {
                        Slot = Common.EFEM.Aligner.Slot;
                    }
                    else if (cb_Robot_Destination == "Stage1")
                    {
                        Slot = Common.EFEM.Stage1.Slot;
                    }
                    if (pn_Robot == Robot.Pn.Stage1)
                    {
                        if (!Common.io.In(IOName.In.Wafer汽缸_抬起檢))
                        {
                            MessageBox.Show("請確認Wafer汽缸_抬起檢是否On");
                            flag.Robot = false;
                            flag.SendCmd = false;
                            return false;
                        }
                        else if (!Common.io.In(IOName.In.StageWafer在席))
                        {
                            MessageBox.Show("請確認Wafer在席是否On");
                            flag.Robot = false;
                            flag.SendCmd = false;
                            return false;
                        }
                    }
                    rtn = Common.EFEM.Robot.WaferGet(armID, pn_Robot, Slot, loadPort, EFEM.slot_status.Unknow);
                    if (rtn)
                    {
                        loadPort.Update_slot_Status(Slot, EFEM.slot_status.Unknow);
                    }
                    Flag.AlarmFlag = true;
                    // report
                    //MessageBox.Show(Common.EFEM.Robot.ErrorDescription + " Fail");
                    break;

                case "WaferPut":
                    loadPort.GetLEDStatus();

                    if (!loadPort.LED_Persence && !loadPort.LED_Placement)
                    {
                        // report
                        //MessageBox.Show(loadPort.pn.ToString() + " Not Ready");
                        flag.Robot = false;
                        flag.SendCmd = false;
                        return false;
                    }
                    if (pn_Robot == Robot.Pn.Stage1)
                    {
                        if (!Common.io.In(IOName.In.Wafer汽缸_抬起檢))
                        {
                            MessageBox.Show("請確認Wafer汽缸_抬起檢是否On");
                            flag.Robot = false;
                            flag.SendCmd = false;
                            return false;
                        }
                        else if (Common.io.In(IOName.In.StageWafer在席))
                        {
                            MessageBox.Show("請確認Wafer在席是否Off");
                            flag.Robot = false;
                            flag.SendCmd = false;
                            return false;
                        }
                    }

                    if (armID == Robot.ArmID.LowerArm)
                    {
                        if (Slot != Common.EFEM.Robot.Slot_Arm_lower)
                        {
                            MessageBox.Show("Lower Arm Slot：" + Common.EFEM.Robot.Slot_Arm_lower + ", Put to " + Slot + " Error", "TopMostMessageBox", MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                            flag.Robot = false;
                            flag.SendCmd = false;
                            return false;
                        }
                        else
                        {
                            Slot = Common.EFEM.Robot.Slot_Arm_lower;
                        }
                    }
                    else
                    {
                        if (Slot != Common.EFEM.Robot.Slot_Arm_upper)
                        {
                            MessageBox.Show("Upper Arm Slot：" + Common.EFEM.Robot.Slot_Arm_upper + ", Put to " + Slot + " Error", "TopMostMessageBox", MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                            flag.Robot = false;
                            flag.SendCmd = false;
                            return false;
                        }
                        else
                        {
                            Slot = Common.EFEM.Robot.Slot_Arm_upper;
                        }
                    }
                    rtn = Common.EFEM.Robot.WaferPut(armID, pn_Robot, Slot, loadPort, EFEM.slot_status.Unknow);
                    if (rtn)
                    {
                        loadPort.Update_slot_Status(Slot, EFEM.slot_status.Ready);
                    }
                    Flag.AlarmFlag = true;
                    break;

                case "VacuumOn":
                    rtn = Common.EFEM.Robot.VacuumOn(armID);
                    break;

                case "VacuumOff":
                    rtn = Common.EFEM.Robot.VacuumOff(armID);
                    Flag.AlarmFlag = true;
                    break;

                case "SetSpeed10":
                    rtn = Common.EFEM.Robot.SetRobotSpeed(10, 10);
                    if (rtn)
                    {
                        Common.SecsgemForm.UpdateEC(TrimGap_EqpID.RobotSpeed, 10, out err);  //更新EC參數
                    }
                    break;

                case "SetSpeed30":
                    rtn = Common.EFEM.Robot.SetRobotSpeed(30, 30);
                    if (rtn)
                    {
                        Common.SecsgemForm.UpdateEC(TrimGap_EqpID.RobotSpeed, 30, out err); //更新EC參數
                    }
                    break;

                case "SetSpeed50":
                    rtn = Common.EFEM.Robot.SetRobotSpeed(50, 50);
                    if (rtn)
                    {
                        Common.SecsgemForm.UpdateEC(TrimGap_EqpID.RobotSpeed, 50, out err); //更新EC參數
                    }
                    break;

                default:
                    rtn = false;
                    break;
            }
            return rtn;
        }

        // 待測試
        private bool LoadPort_send(string listBox_LoadPort_Str, string cb_LP_Port, string cb_LP_LEDSts)
        {
            Common.EFEM.LoadPort1.GetStatus();
            Common.EFEM.LoadPort1.GetLEDStatus();
            Common.EFEM.LoadPort2.GetStatus();
            Common.EFEM.LoadPort2.GetLEDStatus();
            bool rtn;
            LoadPort loadPort;
            switch (cb_LP_Port)
            {
                case "P1":
                    if (!Common.EFEM.LoadPort1.LED_Persence && !Common.EFEM.LoadPort1.LED_Placement)
                    {
                        bW_SendCmd.ReportProgress(41);
                        flag.LoadPort = false;
                        flag.SendCmd = false;
                        return false;
                    }
                    loadPort = Common.EFEM.LoadPort1;
                    break;

                case "P2":
                    if (!Common.EFEM.LoadPort2.LED_Persence && !Common.EFEM.LoadPort2.LED_Placement)
                    {
                        bW_SendCmd.ReportProgress(42);
                        flag.LoadPort = false;
                        flag.SendCmd = false;
                        return false;
                    }
                    loadPort = Common.EFEM.LoadPort2;
                    break;

                case "Pn_Run":
                    if (Common.EFEM.LoadPort_Run == null)
                    {
                        bW_SendCmd.ReportProgress(43);
                        flag.LoadPort = false;
                        flag.SendCmd = false;
                        return false;
                    }
                    if (!Common.EFEM.LoadPort1.LED_Persence && !Common.EFEM.LoadPort1.LED_Placement && !Common.EFEM.LoadPort2.LED_Persence && !Common.EFEM.LoadPort2.LED_Placement)
                    {
                        bW_SendCmd.ReportProgress(41);
                        bW_SendCmd.ReportProgress(42);
                        flag.LoadPort = false;
                        flag.SendCmd = false;
                        return false;
                    }
                    loadPort = Common.EFEM.LoadPort_Run;
                    break;

                default:
                    bW_SendCmd.ReportProgress(43);
                    loadPort = Common.EFEM.LoadPort_Run;
                    flag.LoadPort = false;
                    flag.SendCmd = false;
                    return false;
                    //break;
            }
            switch (listBox_LoadPort_Str)
            {
                case "GetStatus":
                    rtn = loadPort.GetStatus();
                    break;

                case "ResetError":
                    rtn = loadPort.ResetError();
                    break;

                case "Home":
                    rtn = loadPort.Home();
                    Flag.AlarmFlag = true;
                    break;

                case "Load":
                    rtn = loadPort.Load();
                    Flag.AlarmFlag = true;
                    break;

                case "Unload":
                    rtn = loadPort.Unload();
                    if (rtn)
                    {
                        if (Common.EFEM.LoadPort_Run != null)
                        {
                            Common.EFEM.LoadPort_Run = null;
                        }
                    }
                    Flag.AlarmFlag = true;
                    break;

                case "Mapping":
                    rtn = loadPort.Map();
                    Flag.AlarmFlag = true;
                    break;

                case "ReadFoupID":
                    rtn = loadPort.ReadFoupID();
                    FoupID = loadPort.FoupID;
                    bW_SendCmd.ReportProgress(44);
                    break;

                case "LEDLoad":
                    switch (cb_LP_LEDSts)
                    {
                        case "On":
                            rtn = loadPort.LEDLoad(LoadPort.LEDsts.On);
                            break;

                        case "Off":
                            rtn = loadPort.LEDLoad(LoadPort.LEDsts.Off);
                            break;

                        case "Flash":
                            rtn = loadPort.LEDLoad(LoadPort.LEDsts.Flash);
                            break;

                        default:
                            rtn = loadPort.LEDLoad(LoadPort.LEDsts.On);
                            break;
                    }
                    break;

                case "LEDUnLoad":
                    switch (cb_LP_LEDSts)
                    {
                        case "On":
                            rtn = loadPort.LEDUnLoad(LoadPort.LEDsts.On);
                            break;

                        case "Off":
                            rtn = loadPort.LEDUnLoad(LoadPort.LEDsts.Off);
                            break;

                        case "Flash":
                            rtn = loadPort.LEDUnLoad(LoadPort.LEDsts.Flash);
                            break;

                        default:
                            rtn = loadPort.LEDUnLoad(LoadPort.LEDsts.On);
                            break;
                    }
                    break;

                case "LEDStatus1":
                    switch (cb_LP_LEDSts)
                    {
                        case "On":
                            rtn = loadPort.LEDStatus1(LoadPort.LEDsts.On);
                            break;

                        case "Off":
                            rtn = loadPort.LEDStatus1(LoadPort.LEDsts.Off);
                            break;

                        case "Flash":
                            rtn = loadPort.LEDStatus1(LoadPort.LEDsts.Flash);
                            break;

                        default:
                            rtn = loadPort.LEDStatus1(LoadPort.LEDsts.On);
                            break;
                    }
                    break;

                case "LEDStatus2":
                    switch (cb_LP_LEDSts)
                    {
                        case "On":
                            rtn = loadPort.LEDStatus2(LoadPort.LEDsts.On);
                            break;

                        case "Off":
                            rtn = loadPort.LEDStatus2(LoadPort.LEDsts.Off);
                            break;

                        case "Flash":
                            rtn = loadPort.LEDStatus2(LoadPort.LEDsts.Flash);
                            break;

                        default:
                            rtn = loadPort.LEDStatus2(LoadPort.LEDsts.On);
                            break;
                    }
                    break;

                case "SetOperatorAccessButton":
                    switch (cb_LP_LEDSts)
                    {
                        case "On":
                            rtn = loadPort.SetOperatorAccessButton(LoadPort.LEDsts.On);
                            break;

                        case "Off":
                            rtn = loadPort.SetOperatorAccessButton(LoadPort.LEDsts.Off);
                            break;

                        case "Flash":
                            rtn = loadPort.SetOperatorAccessButton(LoadPort.LEDsts.Flash);
                            break;

                        default:
                            rtn = loadPort.SetOperatorAccessButton(LoadPort.LEDsts.On);
                            break;
                    }
                    break;

                default:
                    rtn = false;
                    break;
            }
            return rtn;
            if (!rtn)
            {
                //MessageBox.Show(listBox_LoadPort_Str + " Fail");
            }
        }

        // 完成
        private bool Aligner_send(string listBox_Aligner)
        {
            bool rtn;
            switch (listBox_Aligner)
            {
                case "GetStatus":
                    rtn = Common.EFEM.Aligner.GetStatus();
                    break;

                case "ResetError":
                    rtn = Common.EFEM.Aligner.ResetError();
                    break;

                case "Home":
                    rtn = Common.EFEM.Aligner.Home();
                    break;

                case "VacuumOn":
                    rtn = Common.EFEM.Aligner.AlignerVacuum(Aligner.OnOff.On);
                    Flag.AlarmFlag = true;
                    break;

                case "VacuumOff":
                    rtn = Common.EFEM.Aligner.AlignerVacuum(Aligner.OnOff.Off);
                    Flag.AlarmFlag = true;
                    break;

                case "Alignment":
                    rtn = Common.EFEM.Aligner.Alignment();
                    break;

                case "ToAngle":
                    rtn = Common.EFEM.Aligner.ToAngle(Convert.ToInt32(cb_Alignment_Angle.Text));
                    break;

                default:
                    rtn = false;
                    break;
            }
            return rtn;
        }

        private bool E84_send(string listBox_E84)
        {
            bool rtn;
            E84.E84_Num e84_Num = new E84.E84_Num();
            if (ComboBox_Str.E84_Num == "1")
            {
                e84_Num = E84.E84_Num.E841;
            }
            else
            {
                e84_Num = E84.E84_Num.E842;
            }
            switch (listBox_E84)
            {
                case "Reset":
                    rtn = Common.EFEM.E84.Reset(e84_Num);
                    break;

                case "Auto":
                    rtn = Common.EFEM.E84.SetAuto(e84_Num);
                    break;

                case "Manual":
                    rtn = Common.EFEM.E84.SetManual(e84_Num);
                    break;

                default:
                    rtn = false;
                    break;
            }
            return rtn;
        }

        //ing
        private void bW_SendCmd_DoWork(object sender, DoWorkEventArgs e)
        {
            bW_SendCmd.WorkerReportsProgress = true;
            bool rtn = false;
            try
            {
                if (flag.API)
                {
                    rtn = API_send(listBox_Str.API);
                    bW_SendCmd.ReportProgress(10);
                    flag.API = false;
                }
                else if (flag.SignalTower)
                {
                    rtn = SignalTower_send(listBox_Str.SignalTower);
                    bW_SendCmd.ReportProgress(20);
                    flag.SignalTower = false;
                }
                else if (flag.Robot)
                {
                    rtn = Robot_send(listBox_Str.Robot, ComboBox_Str.Robot_slot, ComboBox_Str.Robot_destination, ComboBox_Str.Robot_arm, ComboBox_Str.LoadPort_port);
                    if (listBox_Str.Robot == "WaferGet" || listBox_Str.Robot == "WaferPut")
                    {
                        bW_SendCmd.ReportProgress(30);
                    }
                    flag.Robot = false;
                }
                else if (flag.LoadPort)
                {
                    rtn = LoadPort_send(listBox_Str.LoadPort, ComboBox_Str.LoadPort_port, ComboBox_Str.LoadPort_led);
                    if (listBox_Str.LoadPort == "Mapping")
                    {
                        bW_SendCmd.ReportProgress(40);
                    }

                    flag.LoadPort = false;
                }
                else if (flag.Aligner)
                {
                    rtn = Aligner_send(listBox_Str.Aligner);
                    bW_SendCmd.ReportProgress(50);
                    flag.Aligner = false;
                }
                else if (flag.E84)
                {
                    rtn = E84_send(listBox_Str.E84);
                    bW_SendCmd.ReportProgress(60);
                    flag.E84 = false;
                }
                flag.SendCmd = false;
                if (rtn)
                {
                    bW_SendCmd.ReportProgress(100);
                }
                else
                {
                    bW_SendCmd.ReportProgress(99);// false
                }
            }
            catch { }
        }

        private void bW_SendCmd_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 10)
            {
                tb_API_Sts.Text = API_Mode;
            }
            else if (e.ProgressPercentage == 30)
            {
                AutoRunEFEM.Update_EFEM_Sts();
            }
            else if (e.ProgressPercentage == 40)
            {
                DialogResult tmpResult = MessageBox.Show("是否將LoadPort狀態回復至Ready", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (tmpResult == DialogResult.OK)
                {
                    if (ComboBox_Str.LoadPort_port == "P1")
                    {
                        Common.EFEM.LoadPort1.GetWaferSlot2();
                    }
                    else if (ComboBox_Str.LoadPort_port == "P2")
                    {
                        Common.EFEM.LoadPort2.GetWaferSlot2();
                    }
                    else
                    {
                        Common.EFEM.LoadPort_Run.GetWaferSlot2();
                    }
                    AutoRunEFEM.Update_EFEM_Sts();
                    AutoRunEFEM.SaveEFEMSts();
                }
            }
            else if (e.ProgressPercentage == 41)
            {
                MessageBox.Show("P1 Not Ready");
            }
            else if (e.ProgressPercentage == 42)
            {
                MessageBox.Show("P2 Not Ready");
            }
            else if (e.ProgressPercentage == 43)
            {
                MessageBox.Show("LoadPort_Run Not Ready");
            }
            else if (e.ProgressPercentage == 44)
            {
                tb_FoupID.Text = FoupID;
            }
            else if (e.ProgressPercentage == 99)
            {
                MessageBox.Show("Command Fail");
            }
            else if (e.ProgressPercentage == 100)
            {
                MessageBox.Show("Command OK");
            }
        }

        private void btn_SetSlotSts_Click(object sender, EventArgs e)
        {
            int slot;
            LoadPort loadPort;
            EFEM.slot_status slot_Status;

            if (cb_SetPort.Text == "P1" || cb_SetPort.Text == "P2" || cb_SetPort.Text == "Pn_Run")
            {
                if (cb_SetPort.Text == "P1")
                {
                    loadPort = Common.EFEM.LoadPort1;
                }
                else if (cb_SetPort.Text == "P2")
                {
                    loadPort = Common.EFEM.LoadPort2;
                }
                else
                {
                    loadPort = Common.EFEM.LoadPort_Run;
                    if (Common.EFEM.LoadPort_Run == null)
                    {
                        MessageBox.Show("No LoadPort Run!!");
                        return;
                    }
                }

                if (loadPort.slot_Status[Convert.ToInt32(cb_SetSlot.Text) - 1] == EFEM.slot_status.Empty)
                {
                    MessageBox.Show("Slot is Empty!!");
                    return;
                }
                else
                {
                    slot = Convert.ToInt32(cb_SetSlot.Text);
                }
                if (cb_SetStatus.Text == "Empty")
                {
                    slot_Status = EFEM.slot_status.Empty;
                }
                else if (cb_SetStatus.Text == "Ready")
                {
                    slot_Status = EFEM.slot_status.Ready;
                }
                else if (cb_SetStatus.Text == "Error")
                {
                    slot_Status = EFEM.slot_status.Error;
                }
                else if (cb_SetStatus.Text == "Aligner1")
                {
                    slot_Status = EFEM.slot_status.ProcessingAligner1;
                }
                else if (cb_SetStatus.Text == "Stage1")
                {
                    slot_Status = EFEM.slot_status.ProcessingStage1;
                }
                else if (cb_SetStatus.Text == "End")
                {
                    slot_Status = EFEM.slot_status.ProcessEnd;
                }
                else
                {
                    slot_Status = EFEM.slot_status.Empty;
                }
                loadPort.Update_slot_Status(slot, slot_Status);

                if (cb_SetPort.Text == "P1")
                {
                    Common.EFEM.LoadPort1.Update_Sts();
                }
                else
                {
                    Common.EFEM.LoadPort2.Update_Sts();
                }
            }
            else
            {
                if (cb_SetPort.Text == "Aligner1")
                {
                    Common.EFEM.Aligner.GetStatus();
                    if (Common.EFEM.Aligner.WaferPresence)
                    {
                        slot = Convert.ToInt32(cb_SetSlot.Text);
                        Common.EFEM.Aligner.Slot = slot;
                    }
                    else
                    {
                        MessageBox.Show("Aligner No Wafer");
                        return;
                    }
                }
                else // Stage1
                {
                    if (Common.io.In(IOName.In.StageWafer在席))
                    {
                        slot = Convert.ToInt32(cb_SetSlot.Text);
                        Common.EFEM.Stage1.Slot = slot;
                    }
                    else
                    {
                        MessageBox.Show("Stage No Wafer");
                        return;
                    }
                }
            }
            DialogResult tmpResult = MessageBox.Show("是否儲存參數", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tmpResult == DialogResult.OK)
            {
                Flag.AlarmFlag = true;
                AutoRunEFEM.Update_EFEM_Sts();
                AutoRunEFEM.SaveEFEMSts();
                ParamFile.saveparam("EFEMSts");
            }
        }

        private void btn_SetSlotSts_Robot_Click(object sender, EventArgs e)
        {
            int slot;
            Robot.ArmID armID;
            EFEM.slot_status slot_Status;

            Common.EFEM.Robot.GetStatus();

            slot = Convert.ToInt32(cb_SetSlot_Robot.Text);

            if (cb_Set_Robot.Text == "Lower")
            {
                armID = Robot.ArmID.LowerArm;
                if (Common.EFEM.Robot.WaferPresence_Lower)
                {
                    Common.EFEM.Robot.Slot_Arm_lower = slot;
                }
                else
                {
                    MessageBox.Show("Lower Arm No Wafer");
                    return;
                }
            }
            else
            {
                armID = Robot.ArmID.UpperArm;
                if (Common.EFEM.Robot.WaferPresence_Upper)
                {
                    Common.EFEM.Robot.Slot_Arm_upper = slot;
                }
                else
                {
                    MessageBox.Show("Upper Arm No Wafer");
                    return;
                }
            }

            if (cb_SetStatus_Robot.Text == "Empty")
            {
                slot_Status = EFEM.slot_status.Empty;
            }
            else if (cb_SetStatus_Robot.Text == "Ready")
            {
                slot_Status = EFEM.slot_status.Ready;
            }
            else if (cb_SetStatus_Robot.Text == "Error")
            {
                slot_Status = EFEM.slot_status.Error;
            }
            else if (cb_SetStatus_Robot.Text == "Aligner1")
            {
                slot_Status = EFEM.slot_status.ProcessingAligner1;
            }
            else if (cb_SetStatus_Robot.Text == "Stage1")
            {
                slot_Status = EFEM.slot_status.ProcessingStage1;
            }
            else if (cb_SetStatus_Robot.Text == "End")
            {
                slot_Status = EFEM.slot_status.ProcessEnd;
            }
            else
            {
                slot_Status = EFEM.slot_status.Ready;
            }

            Common.EFEM.Robot.Update_slot_Status(armID, slot_Status);
            Common.EFEM.Robot.Update_Sts();
            DialogResult tmpResult = MessageBox.Show("是否儲存參數", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (tmpResult == DialogResult.OK)
            {
                Flag.AlarmFlag = true;
                AutoRunEFEM.Update_EFEM_Sts();
                AutoRunEFEM.SaveEFEMSts();
                ParamFile.saveparam("EFEMSts");
            }
        }
    }
}