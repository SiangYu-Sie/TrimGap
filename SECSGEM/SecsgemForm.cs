using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CTLT;

namespace SECSGEM
{
    public partial class SecsgemForm : Form
    {
        private ctlt_SECS CSWrapper;
        public static ctlt_GEM CGWrapper;

        private delegate void TextBoxAppendCallback(string Str);

        private long ulConnectionStatus;
        private long lQGInitResult;
        public static long g_lOperationResult;
        public static string MachineType = "";
        private bool g_fFormLoadFinishedFlag;   //Update EC by option button must wait form load finished
        private int g_iSECSStratTimer;
        private int g_iHSMSRestartTimer;
        private int g_iGemControlState;
        private int g_SystemBytes;
        private String MyStr;

        private Config config = new Config();

        public SecsgemForm()
        {
            InitializeComponent();

            CSWrapper = new ctlt_SECS(); //*new
            CSWrapper.CSEvent += new _ICSWrapperEvents_CSEventEventHandler(CSWrapper_CSEvent);

            CGWrapper = new ctlt_GEM(CSWrapper);
            CGWrapper.CGEvent += new _ICGWrapperEvents_CGEventEventHandler(CGWrapper_CGEvent);
            CGWrapper.PPEvent += new _ICGWrapperEvents_PPEventEventHandler(CGWrapper_PPEvent);
            CGWrapper.TerminalMsgReceive += new _ICGWrapperEvents_TerminalMsgReceiveEventHandler(CGWrapper_TerminalMsgReceive);

            //' HSMS-SS Parameters
            CSWrapper.T5 = SECSGEMDefine.HSMS_SS_Params.T5;
            CSWrapper.T6 = SECSGEMDefine.HSMS_SS_Params.T6;
            CSWrapper.T7 = SECSGEMDefine.HSMS_SS_Params.T7;
            CSWrapper.T8 = SECSGEMDefine.HSMS_SS_Params.T8;
            CSWrapper.lLinkTestPeriod = SECSGEMDefine.HSMS_SS_Params.LinkTestPeriod;
            CSWrapper.szLocalIP = SECSGEMDefine.HSMS_SS_Params.LocalIP;
            CSWrapper.nLocalPort = SECSGEMDefine.HSMS_SS_Params.LocalPort;
            CSWrapper.szRemoteIP = SECSGEMDefine.HSMS_SS_Params.RemoteIP;
            CSWrapper.nRemotePort = SECSGEMDefine.HSMS_SS_Params.RemotePort;
            CSWrapper.HSMS_Connect_Mode = (HSMS_COMM_MODE)SECSGEMDefine.HSMS_SS_Params.HSMS_SS_Conect_Mode;

            //' Common Parameters
            CSWrapper.T3 = SECSGEMDefine.SECS_Common_Params.T3;
            CSWrapper.lDeviceID = SECSGEMDefine.SECS_Common_Params.DeviceID;
            CSWrapper.lCOMM_Mode = (COMMMODE)SECSGEMDefine.SECS_Common_Params.SECS_Connect_Mode;

            CSWrapper.lLogEnable = 1;
            CSWrapper.lFlowControlEnable = 0;

            try
            {
                CSWrapper.Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (MachineType == "SMT")
                Size = new Size(1176, 768);
            else
                Size = new Size(883, 768);
        }

        public void SECS_Connect()
        {
            long lRC;
            if (CSWrapper.lCOMM_Mode == COMMMODE.HSMS_MODE)
            {
                ulConnectionStatus = 0;
                if (EnableComm.Enabled == false)
                {
                    if (CSWrapper.HSMS_Connect_Mode == HSMS_COMM_MODE.HSMS_ACTIVE_MODE)
                    {
                        CSWrapper.Start();
                        AppendText("Try to establish connection..." + "\r\n");
                        g_iHSMSRestartTimer = 6;
                        lbl_SECSConnectState.Text = "Disconnection(Start)";
                        lbl_SECSConnectState.BackColor = Color.Yellow;
                    }
                    else
                    {
                        lRC = CSWrapper.Start();
                        if (lRC == 0)
                        {
                            AppendText("HSMS Start failed..." + "\r\n");
                            g_iHSMSRestartTimer = 6;
                            lbl_SECSConnectState.Text = "Disconnection(Stop)";
                            lbl_SECSConnectState.BackColor = Color.Red;
                        }
                        else
                        {
                            AppendText("HSMS Start Successful..." + "\r\n");
                            lbl_SECSConnectState.Text = "Disconnection(Start)";
                            lbl_SECSConnectState.BackColor = Color.Yellow;
                        }
                    }
                }
            }
        }

        public void SECS_Disconnect()
        {
            long lRC;
            lRC = CSWrapper.Stop();
            lbl_SECSConnectState.Text = "Disconnection(Stop)";
            lbl_SECSConnectState.BackColor = Color.Red;
        }

        private void DisableComm_Click(object sender, EventArgs e)
        {
            ExeDisableComm();
        }

        private void ExeDisableComm()
        {
            g_lOperationResult = CGWrapper.DisableComm();
            SECS_Disconnect();
            if (CSWrapper.lCOMM_Mode == COMMMODE.SECS_MODE)
            {
                lbl_SECSConnectState.Text = "COM Port Closed";
                lbl_SECSConnectState.BackColor = Color.Red;
            }
        }

        private void EnableComm_Click(object sender, EventArgs e)
        {
            long lRC;

            g_lOperationResult = CGWrapper.EnableComm();

            EnableComm.Enabled = false;
            DisableComm.Enabled = true;
            SECS_Connect();
        }

        public string GetVersion()
        {
            string dllPath = Application.StartupPath + "\\SECSGEM.dll";
            return System.Diagnostics.FileVersionInfo.GetVersionInfo(dllPath).FileVersion.ToString();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            int lECID;
            object Value;
            EC_DATA_TYPE lGetFormat;
            int intTemp;

            Text = "SECS/GEM Version - " + GetVersion();

            ulConnectionStatus = 0;
            lQGInitResult = -1; //not initial
            QGInitial_Click();    //GEM Init
            lECID = GemSystemID.GEM_INIT_CONTROL_STATE;
            g_lOperationResult = CGWrapper.GetEC(lECID, out lGetFormat, out Value);
            intTemp = int.Parse(Value.ToString());
            switch (intTemp)
            {
                case 1:
                    opt_OffLine.Checked = true;
                    break;

                case 2:
                    opt_OnLine.Checked = true;
                    break;
            }
            lECID = GemSystemID.GEM_OFF_LINE_SUBSTATE;
            g_lOperationResult = CGWrapper.GetEC(lECID, out lGetFormat, out Value);
            intTemp = int.Parse(Value.ToString());
            switch (intTemp)
            {
                case 1:
                    opt_EqpOffLine.Checked = true;
                    break;

                case 2:
                    opt_AttemptOnLine.Checked = true;
                    break;

                case 3:
                    opt_HostOffLine.Checked = true;
                    break;
            }
            lECID = GemSystemID.GEM_ON_LINE_SUBSTATE;
            g_lOperationResult = CGWrapper.GetEC(lECID, out lGetFormat, out Value);
            intTemp = int.Parse(Value.ToString());
            switch (intTemp)
            {
                case 4:
                    opt_OnLineLocal.Checked = true;
                    break;

                case 5:
                    opt_OnLineRemote.Checked = true;
                    break;
            }
            lECID = GemSystemID.GEM_ON_LINE_FAILED;
            g_lOperationResult = CGWrapper.GetEC(lECID, out lGetFormat, out Value);
            intTemp = int.Parse(Value.ToString());
            switch (intTemp)
            {
                case 1:
                    opt_FailToEqpOffLine.Checked = true;
                    break;

                case 3:
                    opt_FailToHostOffLine.Checked = true;
                    break;
            }
            lECID = GemSystemID.GEM_WBIT_S5;
            g_lOperationResult = CGWrapper.GetEC(lECID, out lGetFormat, out Value);
            intTemp = int.Parse(Value.ToString());
            switch (intTemp)
            {
                case 0:
                    opt_S5WbitOff.Checked = true;
                    break;

                case 1:
                    opt_S5WbitOn.Checked = true;
                    break;
            }
            lECID = GemSystemID.GEM_WBIT_S6;
            g_lOperationResult = CGWrapper.GetEC(lECID, out lGetFormat, out Value);
            intTemp = int.Parse(Value.ToString());
            switch (intTemp)
            {
                case 0:
                    opt_S6WbitOff.Checked = true;
                    break;

                case 1:
                    opt_S6WbitOn.Checked = true;
                    break;
            }
            lECID = GemSystemID.GEM_WBIT_S10;
            g_lOperationResult = CGWrapper.GetEC(lECID, out lGetFormat, out Value);
            intTemp = int.Parse(Value.ToString());
            switch (intTemp)
            {
                case 0:
                    opt_S10WbitOff.Checked = true;
                    break;

                case 1:
                    opt_S10WbitOn.Checked = true;
                    break;
            }
            lECID = GemSystemID.GEM_CONFIG_SPOOL;
            g_lOperationResult = CGWrapper.GetEC(lECID, out lGetFormat, out Value);
            intTemp = int.Parse(Value.ToString());
            switch (intTemp)
            {
                case 0:
                    opt_ConfigSpoolDisable.Checked = true;
                    break;

                case 1:
                    opt_ConfigSpoolEnable.Checked = true;
                    break;
            }
            lECID = GemSystemID.GEM_OVER_WRITE_SPOOL;
            g_lOperationResult = CGWrapper.GetEC(lECID, out lGetFormat, out Value);
            intTemp = (int)Value.GetHashCode();
            switch (intTemp)
            {
                case 0:
                    opt_OverWriteSpoolDisable.Checked = true;
                    break;

                case 1:
                    opt_OverWriteSpoolEnable.Checked = true;
                    break;
            }
            g_fFormLoadFinishedFlag = true;
            lECID = GemSystemID.GEM_INIT_COMM_STATE;
            g_lOperationResult = CGWrapper.GetEC(lECID, out lGetFormat, out Value);
            intTemp = (int)Value.GetHashCode();
            switch (intTemp)
            {
                case 0:
                    opt_DisableComm.Checked = true;
                    break;

                case 1:
                    opt_EnableComm.Checked = true;
                    EnableComm.Enabled = false;
                    DisableComm.Enabled = true;
                    SECS_Connect();
                    if (CSWrapper.lCOMM_Mode == COMMMODE.SECS_MODE)
                    {
                        lbl_SECSConnectState.Text = "COM Port Open";
                        lbl_SECSConnectState.BackColor = Color.GreenYellow;
                    }
                    break;
            }
            RefreshTimer.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //CSWrapper.Destroy();
            //CGWrapper.Close();
            //Environment.Exit(0);
        }

        private void btn_OnLine_Click(object sender, EventArgs e)
        {
            g_lOperationResult = CGWrapper.OnLineRequest();
        }

        private void btnS1F1_Click(object sender, EventArgs e)
        {
            int SystemBytes = 0;
            object objTemp1 = null;

            CSWrapper.SendSECSIIMessage(1, 1, 1, ref SystemBytes, objTemp1);
        }

        private void GetSV_Click(object sender, EventArgs e)
        {
            long lSVID;
            object Value;
            SV_DATA_TYPE GetFormat;

            lSVID = long.Parse(SVID.Text);

            g_lOperationResult = CGWrapper.GetSV((int)lSVID, out GetFormat, out Value);

            if (Value != null)
                SVValue.Text = Value.ToString();
            else
                SVValue.Text = "";
        }

        private void btn_Offline_Click(object sender, EventArgs e)
        {
            g_lOperationResult = CGWrapper.OffLine();
        }

        private void btn_OnlineLocal_Click(object sender, EventArgs e)
        {
            g_lOperationResult = CGWrapper.OnLineLocal();
        }

        private void btn_OnLineRemote_Click(object sender, EventArgs e)
        {
            g_lOperationResult = CGWrapper.OnLineRemote();
        }

        private void opt_S10WbitOff_Click(object sender, EventArgs e)
        {
            object byVal1;

            if (g_fFormLoadFinishedFlag == true)
            {
                byVal1 = 0;

                g_lOperationResult = CGWrapper.UpdateEC((int)GemSystemID.GEM_WBIT_S10, byVal1);
            }
        }

        private void opt_S10WbitOn_Click(object sender, EventArgs e)
        {
            object byVal1;

            if (g_fFormLoadFinishedFlag == true)
            {
                byVal1 = 1;
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_WBIT_S10, byVal1);
            }
        }

        private void QGClose_Click()
        {
            long lResult;
            lResult = CGWrapper.Close();
            lbl_ErrorCode.Text = lResult.ToString();
            FrameGuickGEM.Enabled = false;
        }

        private void QGInitial_Click()
        {
            string path;
            long lResult;
            object objVal;

            path = System.Environment.CurrentDirectory;
            lQGInitResult = CGWrapper.Initialize(path);

            lbl_ErrorCode.Text = lQGInitResult.ToString();
            if (lQGInitResult == 0)
            {
                lbl_ErrorCode.BackColor = Color.GreenYellow;
                AppendText("QuickGEM Initiation success." + "\r\n");
            }
            else
            {
                lbl_ErrorCode.BackColor = Color.Red;
                AppendText("QuickGEM Initiation error." + "\r\n");
                MessageBox.Show("QuickGEM Initiation error. Press any key to close program !");
                lQGInitResult = -2; //initial failed
                Environment.Exit(0);
            }

            if (MachineType == "SMT")
                objVal = "SMT";
            else if (MachineType == "FVI")
                objVal = "FVI";
            else if (MachineType == "TrimGap")
                objVal = "TrimGap";
            else
                objVal = "Equipment";

            lResult = CGWrapper.UpdateSV((int)GemSystemID.GEM_MDLN, objVal);

            objVal = "1.0.0.0";
            lResult = CGWrapper.UpdateSV((int)GemSystemID.GEM_SOFTREV, objVal);
        }

        //******************************************************************************************
        //* GEM Event Call Back Area
        //******************************************************************************************
        private void CGWrapper_PPEvent(PP_TYPE MsgID, string PPID)
        {
            string strMsgType = "";

            //Host Initiated PP transmission
            if (MsgID == PP_TYPE.PP_DELETE)
            {
                strMsgType = "Process Program Delete. PPID = " + PPID.ToString();
                AppendText(strMsgType + "\r\n");
                PPEventText.Text = strMsgType;
            }
            else if (MsgID == PP_TYPE.PP_DOWNLOAD)
            {
                strMsgType = "Process Program Download. PPID =" + PPID.ToString();
                PPEventText.Text = strMsgType;
            }
            else if (MsgID == PP_TYPE.PP_UPLOAD)
            {
                strMsgType = "Process Program Upload. PPID =" + PPID.ToString();
                PPEventText.Text = strMsgType;
            }
            //Eqp Initiated PP transmission
            else if (MsgID == PP_TYPE.PP_INQUIRE_GRANT)
            {
                strMsgType = "Eqp Send Load Inquire. Grant= " + PPID.ToString();
                PPEventText.Text = strMsgType;
            }
            else if (MsgID == PP_TYPE.PP_SEND_ACK)
            {
                strMsgType = "Eqp Send PP. ACK= " + PPID.ToString();
                PPEventText.Text = strMsgType;
            }
            else if (MsgID == PP_TYPE.PP_REQUEST_RESULT)
            {
                strMsgType = "Eqp Request PP. Result = " + PPID.ToString();
                PPEventText.Text = strMsgType;
            }
            else if (MsgID == PP_TYPE.RECEIVE_NEW_EC)
            {
                strMsgType = "EC,Value = " + PPID.ToString();
                AppendText(strMsgType + "\r\n");
                tbHostSendnewEC.Text = strMsgType;
            }
        }

        private void CGWrapper_TerminalMsgReceive(string Message)//*new
        {
            txt_MessageFromHost.Text = Message.ToString();
        }

        private void CGWrapper_CGEvent(int lID, int S, int F, int W_Bit, int SystemBytes, object RawData, int Length) //*new
        {
            CSWrapper.SendSECSIIMessage(S, F, W_Bit, ref SystemBytes, RawData);
            AppendText("CTLT.GEM Send ===>" + "\r\n");
        }

        //******************************************************************************************
        //* Event Call Back Area
        //******************************************************************************************
        private void CSWrapper_CSEvent(int lID, EVENT_ID lMsgID, int S, int F, int W_Bit, int ulSystemBytes, object RawData, object Head, string pEventText) //*new
        {
            object OutputRawData = null;
            long ulOutputSystemBytes = 0;
            int i;
            PROCESS_MSG_RESULT lResult;
            //*** User can process your own GEM message from here ****************************
            //
            //*** User can process your own GEM message from here ****************************
            lResult = CGWrapper.ProcessMessage((int)lMsgID, S, F, W_Bit, ulSystemBytes, ref RawData, ref Head, pEventText);

            if (lMsgID == EVENT_ID.QS_EVENT_RECV_MSG)
            {
                //------------------------------------------------------------------------'
                if ((lResult == PROCESS_MSG_RESULT.UNKNOWN_FUNCTION)
                            || (lResult == PROCESS_MSG_RESULT.UNKNOWN_STREAM))
                {
                    //*** User process message from here ****************************
                    //
                    //
                    i = ExeS2F41RCMDAction(ulSystemBytes, RawData);
                    if (i != 0)
                    {
                        ExeSendS9F7(Head);
                    }
                    //
                    //
                    //*** User process message till here ****************************
                }
                else if (lResult == PROCESS_MSG_RESULT.PROCESS_OK)
                {
                    // OK, This message was processed by QuickGEM
                }
                else if (lResult == PROCESS_MSG_RESULT.ILLEGAL_DATA)
                {
                    // Message formation Error and send S9F7 by QuickGEM
                }

                //------------------------------------------------------------------------'
                if (S == 12 && F == 1)
                {
                }
                AppendText(pEventText + "\r\n");
                ShowSECSIIMessage(RawData);
            }
            else if (lMsgID == EVENT_ID.QS_EVENT_SEND_MSG)
            {
                AppendText(pEventText + "\r\n");
                ShowSECSIIMessage(RawData);
            }
            else if (lMsgID == EVENT_ID.QS_EVENT_CONNECTED)
            {
                ulConnectionStatus = 1;
                g_iHSMSRestartTimer = 0;

                if (CSWrapper.HSMS_Connect_Mode == HSMS_COMM_MODE.HSMS_ACTIVE_MODE)
                    AppendText("Connected to the passive entity" + "\r\n");
                else
                    AppendText("Accept remote active entity's connection" + "\r\n");

                lbl_SECSConnectState.Text = "Connection";
                lbl_SECSConnectState.BackColor = Color.GreenYellow;
            }
            else if (lMsgID == EVENT_ID.QS_EVENT_DISCONNECTED)
            {
                lbl_SECSConnectState.Text = "Disconnection(Stop)";
                lbl_SECSConnectState.BackColor = Color.Red;
                SECS_Connect();
            }
            else if (lMsgID == EVENT_ID.QS_EVENT_REPLY_TIMEOUT)
            {
                AppendText(pEventText + "\r\n");
                CSWrapper.DataItemOut(ref OutputRawData, 10, SECSII_DATA_TYPE.BINARY_TYPE, ref Head);

                int ulOutputSystemBytes1 = (int)ulOutputSystemBytes;
                CSWrapper.SendSECSIIMessage(9, 9, 0, ref ulOutputSystemBytes1, OutputRawData);
            }
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            long lSVID, lRetval;
            object Value;
            object Value2;
            //object RawData;
            //string InputData;
            //long ulSystemBytes;
            SV_DATA_TYPE GetFormat;
            EC_DATA_TYPE GetECFormat;
            COMM_STATE CommState;
            //string sStatus;
            object bCOMM_Mode;
            long lOperationResult;
            int intTemp;

            if (MachineType == "SMT")
            {
                txtEQPStatus.Text = SMT_EqpParameter.EqpStatus;

                if (SMT_EqpParameter.EqpStatus == "IDLE")
                    txtEQPStatus.BackColor = Color.LightYellow;
                else if (SMT_EqpParameter.EqpStatus == "ALARM")
                    txtEQPStatus.BackColor = Color.Red;
                else if (SMT_EqpParameter.EqpStatus == "RUN")
                    txtEQPStatus.BackColor = Color.LightGreen;
                else
                    txtEQPStatus.BackColor = Color.White;

                label_ULD_Pass1MagazineBoatCount.Text = "Boat Count : " + SMT_EqpParameter.Pass1MagazineBoatCount;
                label_ULD_Pass2MagazineBoatCount.Text = "Boat Count : " + SMT_EqpParameter.Pass2MagazineBoatCount;
                label_ULD_NGMagazineBoatCount.Text = "Boat Count : " + SMT_EqpParameter.NGMagazineBoatCount;
                label_ULD_EmptyMagazineBoatCount.Text = "Boat Count : " + SMT_EqpParameter.EmptyMagazineBoatCount;

                label_ULD_Pass1MagazineUnitCount.Text = "Unit Count : " + SMT_EqpParameter.Pass1MagazineUnitCount;
                label_ULD_Pass2MagazineUnitCount.Text = "Unit Count : " + SMT_EqpParameter.Pass2MagazineUnitCount;
                label_ULD_NGMagazineUnitCount.Text = "Unit Count : " + SMT_EqpParameter.NGMagazineUnitCount;
            }

            //Ver:4.10
            if (g_iHSMSRestartTimer > 0)
            {
                g_iHSMSRestartTimer = g_iHSMSRestartTimer - 1;
            }
            if (g_iHSMSRestartTimer == 1)
            {
                SECS_Connect();
            }

            if (CSWrapper.lCOMM_Mode == COMMMODE.SECS_MODE)
            {
                if (g_iSECSStratTimer < 4)
                {
                    g_iSECSStratTimer += 1;
                }

                if (g_iSECSStratTimer == 3)
                {
                    //Must Update GEM_COMM_MODE SV for SECS I Mode
                    lSVID = GemSystemID.GEM_COMM_MODE;
                    bCOMM_Mode = CSWrapper.lCOMM_Mode;
                    lOperationResult = CGWrapper.UpdateSV((int)lSVID, bCOMM_Mode);
                }
            }

            if (lQGInitResult == -2)
            {
                // QuickGEM initial failed
                Environment.Exit(0);
            }
            //Refresh Message Send State

            CGWrapper.GetSV(GemSystemID.GEM_ALARM_SET, out GetFormat, out Value);
            tbAlarmSetSV.Text = Value.ToString();

            //////////////////////////////////////////////////////////////////////

            CGWrapper.GetSV(GemSystemID.GEM_CLOCK, out GetFormat, out Value);
            lbl_GemClock.Text = Value.ToString();

            CGWrapper.GetSV(GemSystemID.GEM_MDLN, out GetFormat, out Value);
            lbl_GemMDLN.Text = Value.ToString();

            CGWrapper.GetSV(GemSystemID.GEM_SOFTREV, out GetFormat, out Value);
            lbl_GemSOFTREV.Text = Value.ToString();

            CGWrapper.GetSV(GemSystemID.GEM_SOFTWARE_REVISION, out GetFormat, out Value);
            lbl_GemVer.Text = Value.ToString();

            CGWrapper.GetSV(GemSystemID.GEM_ECID_CHANGED, out GetFormat, out Value);
            lbl_ECChanged.Text = Value.ToString();

            //Update Communication State
            CommState = CGWrapper.GetCurrentCommState();
            Comm_State = CommState.GetHashCode();
            switch (CommState)
            {
                case COMM_STATE.DISABLE:
                    lbl_CommState.Text = CommState.GetHashCode() + ":Disable";
                    lbl_CommState.BackColor = Color.White;
                    EnableComm.Enabled = true;
                    DisableComm.Enabled = false;
                    break;

                case COMM_STATE.NOT_COMMUNICATING:
                    lbl_CommState.Text = CommState.GetHashCode() + ":Not Communicating";
                    lbl_CommState.BackColor = Color.Red;
                    EnableComm.Enabled = false;
                    DisableComm.Enabled = true;
                    break;

                case COMM_STATE.COMMUNICATING:
                    lbl_CommState.Text = CommState.GetHashCode() + ":Communicating";
                    lbl_CommState.BackColor = Color.GreenYellow;
                    EnableComm.Enabled = false;
                    DisableComm.Enabled = true;
                    break;
            }

            //Get Control State
            //1:Eqp OffLine 2:OnLine Local, 3:OnLine Remote
            lSVID = GemSystemID.GEM_CONTROL_STATE;
            CGWrapper.GetSV((int)lSVID, out GetFormat, out Value);

            intTemp = int.Parse(Value.ToString());
            Control_State = intTemp;
            g_iGemControlState = intTemp;
            switch (intTemp)
            {
                case 1:
                    lbl_CcontrolStats.BackColor = Color.Red;
                    btn_OnLineRemote.Enabled = false;
                    btn_OnlineLocal.Enabled = false;

                    lbl_CcontrolStats.Text = "1:OffLine (EQP OffLine)";
                    btn_OnLine.Enabled = true;
                    btn_Offline.Enabled = false;
                    break;

                case 2:
                    lbl_CcontrolStats.BackColor = Color.Red;
                    btn_OnLineRemote.Enabled = false;
                    btn_OnlineLocal.Enabled = false;

                    lbl_CcontrolStats.Text = "2:OffLine (Attempt OnLine)";
                    btn_OnLine.Enabled = false;
                    btn_Offline.Enabled = false;
                    break;

                case 3:
                    lbl_CcontrolStats.BackColor = Color.Red;
                    btn_OnLineRemote.Enabled = false;
                    btn_OnlineLocal.Enabled = false;

                    lbl_CcontrolStats.Text = "3:OffLine (Host OffLine)";
                    btn_OnLine.Enabled = false;
                    btn_Offline.Enabled = true;
                    break;

                case 4:
                    lbl_CcontrolStats.Text = "4:OnLine Local";
                    lbl_CcontrolStats.BackColor = Color.Yellow;
                    btn_OnLine.Enabled = false;
                    btn_Offline.Enabled = true;
                    btn_OnLineRemote.Enabled = true;
                    btn_OnlineLocal.Enabled = false;
                    break;

                case 5:
                    lbl_CcontrolStats.Text = "5:OnLine Remote";
                    lbl_CcontrolStats.BackColor = Color.GreenYellow;
                    btn_OnLine.Enabled = false;
                    btn_Offline.Enabled = true;
                    btn_OnLineRemote.Enabled = false;
                    btn_OnlineLocal.Enabled = true;
                    break;
            }

            //*****************************************************************
            //Update Spool 's SV and EC
            //
            lSVID = GemSystemID.GEM_SPOOL_STATE;
            CGWrapper.GetSV((int)lSVID, out GetFormat, out Value);
            lbl_SpoolingState.Text = Value.ToString();

            intTemp = int.Parse(Value.ToString());

            if (intTemp == 0)
            {
                lbl_SpoolingState.BackColor = Color.GreenYellow;
            }
            else if (intTemp == 1)
            {
                lbl_SpoolingState.BackColor = Color.Yellow;
            }
            else
            {
                lbl_SpoolingState.BackColor = Color.Red;
            }

            lSVID = GemSystemID.GEM_SPOOL_COUNT_ACTUAL;
            CGWrapper.GetSV((int)lSVID, out GetFormat, out Value);
            lbl_SpoolActualCount.Text = Value.ToString();

            lSVID = GemSystemID.GEM_SPOOL_COUNT_TOTAL;
            CGWrapper.GetSV((int)lSVID, out GetFormat, out Value);
            lbl_SpoolTotalCount.Text = Value.ToString();

            lSVID = GemSystemID.GEM_SPOOL_FULL_TIME;
            CGWrapper.GetSV((int)lSVID, out GetFormat, out Value);
            lbl_SpoolFullTime.Text = Value.ToString();

            lSVID = GemSystemID.GEM_SPOOL_START_TIME;
            CGWrapper.GetSV((int)lSVID, out GetFormat, out Value);
            lbl_SpoolStartTime.Text = Value.ToString();

            lSVID = GemSystemID.GEM_MAX_SPOOL_TRANSMIT;
            CGWrapper.GetEC((int)lSVID, out GetECFormat, out Value);
            lbl_MaxSpoolTransmit.Text = Value.ToString();

            //Update Operation Result
            lbl_OpResult.Text = "Result = " + g_lOperationResult.ToString();

            if (g_lOperationResult == 0)
            {
                lbl_OpResult.BackColor = Color.GreenYellow;
            }
            else
            {
                lbl_OpResult.BackColor = Color.Red;
            }
        }

        //************************************************************************************
        //* Show SECS Message
        //************************************************************************************
        private void ShowSECSIIMessage(object myRawData)
        {
            int[] myStack = new int[10];
            int myStackPtr;
            int lOffset;
            int lItemNum;
            object ItemData = null;
            int lLength;
            SECSII_DATA_TYPE lItemType;
            string DisplayString;
            //int i;

            // Verify whether the input data is an array or not
            System.Array myArray = myRawData as System.Array;
            if (myArray != null)
                lLength = myArray.Length;
            else
                return;

            if (myArray.Length == 0)
                return;

            myStackPtr = 0;
            lOffset = 0;

            //int lOffset1 = 0;

            try
            {
                while (lOffset < lLength)
                {
                    if (myStackPtr > 0)
                    {
                        if (myStack[myStackPtr - 1] > 0)
                        {
                            myStack[myStackPtr - 1] = myStack[myStackPtr - 1] - 1;
                        }
                        else
                        {
                            myStackPtr = myStackPtr - 1;
                            if (myStackPtr == 0)
                            {
                                // force show end. Normal message should not run to here
                                AppendSxFyMessage(">" + "\r\n");
                                AppendSxFyMessage("." + "\r\n");
                                AppendText(MyStr);
                                MyStr = "";
                                return;
                            }

                            while (myStack[myStackPtr] == 0)
                            {
                                AppendIndent(myStackPtr);
                                AppendSxFyMessage(">" + "\r\n");
                                myStackPtr = myStackPtr - 1;
                            }
                            myStackPtr = myStackPtr + 1;
                            myStack[myStackPtr - 1] = myStack[myStackPtr - 1] - 1;
                        }
                    }

                    //if (lOffset > lOffset1 + 1000) // for what ?
                    //{
                    //    lOffset1 = lOffset;

                    //    AppendText(lOffset + "\r\n");
                    //}

                    lItemType = CSWrapper.GetDataItemType(ref myRawData, lOffset);
                    if (lItemType == SECSII_DATA_TYPE.LIST_TYPE)
                    {
                        lItemNum = 99;

                        object objTemp1 = null;
                        lOffset = CSWrapper.DataItemIn(ref myRawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemNum, ref objTemp1);
                        // Display the data item
                        AppendIndent(myStackPtr);
                        AppendSxFyMessage("<L[" + lItemNum + "]" + "\r\n");
                        // Increase the indent level
                        myStack[myStackPtr] = lItemNum;
                        myStackPtr = myStackPtr + 1;
                    }
                    else if (lItemType == SECSII_DATA_TYPE.ASCII_TYPE)
                    {
                        lOffset = CSWrapper.DataItemIn(ref myRawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemNum, ref ItemData);
                        AppendIndent(myStackPtr);
                        AppendSxFyMessage("<A[" + lItemNum + "] " + (char)34 + ItemData + (char)34 + ">" + "\r\n");
                    }
                    else if (lItemType == SECSII_DATA_TYPE.JIS_TYPE)
                    {
                        lOffset = CSWrapper.DataItemIn(ref myRawData, lOffset, SECSII_DATA_TYPE.JIS_TYPE, out lItemNum, ref ItemData);
                        AppendIndent(myStackPtr);
                        AppendSxFyMessage("<J[" + lItemNum + "] " + (char)34 + ItemData + (char)34 + ">" + "\r\n");
                    }
                    else if (lItemType == SECSII_DATA_TYPE.BINARY_TYPE)
                    {
                        lOffset = CSWrapper.DataItemIn(ref myRawData, lOffset, SECSII_DATA_TYPE.BINARY_TYPE, out lItemNum, ref ItemData);
                        AppendIndent(myStackPtr);
                        DisplayString = "";

                        int[] tData = (int[])ItemData;

                        for (int intIndex = 0; intIndex <= (lItemNum - 1); intIndex++)
                        {
                            DisplayString = DisplayString + " 0x" + Convert.ToString(tData[intIndex], 16).ToUpper();
                        }
                        AppendSxFyMessage("<B[" + lItemNum + "]" + DisplayString + ">" + "\r\n");
                    }
                    else if (lItemType == SECSII_DATA_TYPE.BOOLEAN_TYPE)
                    {
                        lOffset = CSWrapper.DataItemIn(ref myRawData, lOffset, SECSII_DATA_TYPE.BOOLEAN_TYPE, out lItemNum, ref ItemData);
                        AppendIndent(myStackPtr);
                        DisplayString = "";

                        int[] tData = (int[])ItemData;

                        for (int intIndex = 0; intIndex <= (lItemNum - 1); intIndex++)
                        {
                            DisplayString = DisplayString + " 0x" + Convert.ToString(tData[intIndex], 16).ToUpper();
                        }
                        AppendSxFyMessage("<Boolean[" + lItemNum + "]" + DisplayString + ">" + "\r\n");
                    }
                    else if (lItemType == SECSII_DATA_TYPE.UINT_1_TYPE)
                    {
                        lOffset = CSWrapper.DataItemIn(ref myRawData, lOffset, SECSII_DATA_TYPE.UINT_1_TYPE, out lItemNum, ref ItemData);
                        AppendIndent(myStackPtr);
                        DisplayString = "";
                        uint[] tData = (uint[])ItemData;

                        for (int intIndex = 0; intIndex <= (lItemNum - 1); intIndex++)
                        {
                            DisplayString = DisplayString + " " + Convert.ToString(tData[intIndex]);
                        }
                        AppendSxFyMessage("<U1[" + lItemNum + "]" + DisplayString + ">" + "\r\n");
                    }
                    else if (lItemType == SECSII_DATA_TYPE.UINT_2_TYPE)
                    {
                        lOffset = CSWrapper.DataItemIn(ref myRawData, lOffset, SECSII_DATA_TYPE.UINT_2_TYPE, out lItemNum, ref ItemData);
                        AppendIndent(myStackPtr);
                        DisplayString = "";
                        uint[] tData = (uint[])ItemData;

                        for (int intIndex = 0; intIndex <= (lItemNum - 1); intIndex++)
                        {
                            DisplayString = DisplayString + " " + Convert.ToString(tData[intIndex]);
                        }
                        AppendSxFyMessage("<U2[" + lItemNum + "]" + DisplayString + ">" + "\r\n");
                    }
                    else if (lItemType == SECSII_DATA_TYPE.UINT_4_TYPE)
                    {
                        lOffset = CSWrapper.DataItemIn(ref myRawData, lOffset, SECSII_DATA_TYPE.UINT_4_TYPE, out lItemNum, ref ItemData);
                        AppendIndent(myStackPtr);
                        DisplayString = "";
                        uint[] tData = (uint[])ItemData;

                        for (int intIndex = 0; intIndex <= (lItemNum - 1); intIndex++)
                        {
                            DisplayString = DisplayString + " " + Convert.ToString(tData[intIndex]);
                        }
                        AppendSxFyMessage("<U4[" + lItemNum + "]" + DisplayString + ">" + "\r\n");
                    }
                    else if (lItemType == SECSII_DATA_TYPE.INT_1_TYPE)
                    {
                        lOffset = CSWrapper.DataItemIn(ref myRawData, lOffset, SECSII_DATA_TYPE.INT_1_TYPE, out lItemNum, ref ItemData);
                        AppendIndent(myStackPtr);
                        DisplayString = "";
                        int[] tData = (int[])ItemData;

                        for (int intIndex = 0; intIndex <= (lItemNum - 1); intIndex++)
                        {
                            DisplayString = DisplayString + " " + Convert.ToString(tData[intIndex]);
                        }
                        AppendSxFyMessage("<I1[" + lItemNum + "]" + DisplayString + ">" + "\r\n");
                    }
                    else if (lItemType == SECSII_DATA_TYPE.INT_2_TYPE)
                    {
                        lOffset = CSWrapper.DataItemIn(ref myRawData, lOffset, SECSII_DATA_TYPE.INT_2_TYPE, out lItemNum, ref ItemData);
                        AppendIndent(myStackPtr);
                        DisplayString = "";
                        int[] tData = (int[])ItemData;

                        for (int intIndex = 0; intIndex <= (lItemNum - 1); intIndex++)
                        {
                            DisplayString = DisplayString + " " + Convert.ToString(tData[intIndex]);
                        }
                        AppendSxFyMessage("<I2[" + lItemNum + "]" + DisplayString + ">" + "\r\n");
                    }
                    else if (lItemType == SECSII_DATA_TYPE.INT_4_TYPE)
                    {
                        lOffset = CSWrapper.DataItemIn(ref myRawData, lOffset, SECSII_DATA_TYPE.INT_4_TYPE, out lItemNum, ref ItemData);
                        AppendIndent(myStackPtr);
                        DisplayString = "";
                        int[] tData = (int[])ItemData;

                        for (int intIndex = 0; intIndex <= (lItemNum - 1); intIndex++)
                        {
                            DisplayString = DisplayString + " " + Convert.ToString(tData[intIndex]);
                        }
                        AppendSxFyMessage("<I4[" + lItemNum + "]" + DisplayString + ">" + "\r\n");
                    }
                    else if (lItemType == SECSII_DATA_TYPE.FT_4_TYPE)
                    {
                        lOffset = CSWrapper.DataItemIn(ref myRawData, lOffset, SECSII_DATA_TYPE.FT_4_TYPE, out lItemNum, ref ItemData);
                        AppendIndent(myStackPtr);
                        DisplayString = "";
                        Single[] tData = (Single[])ItemData;

                        for (int intIndex = 0; intIndex <= (lItemNum - 1); intIndex++)
                        {
                            DisplayString = DisplayString + " " + Convert.ToString(tData[intIndex]);
                        }
                        AppendSxFyMessage("<F4[" + lItemNum + "]" + DisplayString + ">" + "\r\n");
                    }
                    else if (lItemType == SECSII_DATA_TYPE.FT_8_TYPE)
                    {
                        lOffset = CSWrapper.DataItemIn(ref myRawData, lOffset, SECSII_DATA_TYPE.FT_8_TYPE, out lItemNum, ref ItemData);
                        AppendIndent(myStackPtr);
                        DisplayString = "";
                        double[] tData = (double[])ItemData;

                        for (int intIndex = 0; intIndex <= (lItemNum - 1); intIndex++)
                        {
                            DisplayString = DisplayString + " " + Convert.ToString(tData[intIndex]);
                        }
                        AppendSxFyMessage("<F8[" + lItemNum + "]" + DisplayString + ">" + "\r\n");
                    }
                    else
                    {
                        lOffset = CSWrapper.DataItemInSkip(ref myRawData, lOffset, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            while (myStackPtr > 0)
            {
                myStackPtr = myStackPtr - 1;
                AppendIndent(myStackPtr);
                AppendSxFyMessage(">" + "\r\n");
            }

            AppendSxFyMessage("." + "\r\n");
            AppendText(MyStr);
            MyStr = "";
        }

        private void AppendSxFyMessage(string myText)
        {
            MyStr = MyStr + myText;
        }

        private void AppendText(string myText)
        {
            if (textBox1.InvokeRequired)
            {
                TextBoxAppendCallback d = new TextBoxAppendCallback(AppendText);
                this.Invoke(d, new object[] { myText });
            }
            else
            {
                // Clear text if its too long
                if (textBox1.Text.Length > 8192)
                {
                    textBox1.Text = "";
                }

                // New-line character is added only when there's already texts
                // in the Edit Control

                if (textBox1.Text != "")
                {
                    textBox1.Text += myText;
                }
                else
                {
                    textBox1.Text = myText;
                }

                // Put the caret in the end of the text
                textBox1.SelectionStart = textBox1.Text.Length;
                // Scroll to the caret
                textBox1.ScrollToCaret();
            }
        }

        private void AppendIndent(long myIndentLevel)
        {
            for (long lngIndex = 0; lngIndex < myIndentLevel; lngIndex++)
            {
                MyStr = MyStr + "  ";
            }
        }

        //************************************************************************************
        //*
        //* Option Buttom Click Area
        //*
        //************************************************************************************

        //******** Default Communicating State Setting ********
        private void opt_EnableComm_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true)
            {
                byVal1 = 1;
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_INIT_COMM_STATE, byVal1);
            }
        }

        private void opt_DisableComm_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true) //Enable update when form load finished
            {
                byVal1 = 0;
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_INIT_COMM_STATE, byVal1);
            }
        }

        //******** Default Control State Setting ********
        private void opt_OnLine_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true)
            {
                byVal1 = 2; //On Line
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_INIT_CONTROL_STATE, byVal1);
            }
        }

        private void opt_OffLine_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true)
            {
                byVal1 = 1; //Off Line
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_INIT_CONTROL_STATE, byVal1);
            }
        }

        private void opt_EqpOffLine_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true)
            {
                byVal1 = 1; //Off Line
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_OFF_LINE_SUBSTATE, byVal1);
            }
        }

        private void opt_AttemptOnLine_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true)
            {
                byVal1 = 2; //Attempt On Line
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_OFF_LINE_SUBSTATE, byVal1);
            }
        }

        private void opt_HostOffLine_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true)
            {
                byVal1 = 3; //Host Off Line
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_OFF_LINE_SUBSTATE, byVal1);
            }
        }

        //******** Default On Line Substate Setting ********
        private void opt_OnLineLocal_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true)
            {
                byVal1 = 4; //On Line Local
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_ON_LINE_SUBSTATE, byVal1);
            }
        }

        private void opt_OnLineRemote_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true)
            {
                byVal1 = 5; //On Line Remote
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_ON_LINE_SUBSTATE, byVal1);
            }
        }

        //******** Default On Line Substate Setting ********
        private void opt_FailToEqpOffLine_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true)
            {
                byVal1 = 1; //Eqp Off Line
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_ON_LINE_FAILED, byVal1);
            }
        }

        private void opt_FailToHostOffLine_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true)
            {
                byVal1 = 3; //Host Off Line
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_ON_LINE_FAILED, byVal1);
            }
        }

        //*********** Config Spool ******************************
        private void opt_ConfigSpoolEnable_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true) //Enable update when form load finished
            {
                byVal1 = 1;
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_CONFIG_SPOOL, byVal1);
            }
        }

        private void opt_ConfigSpoolDisable_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true) //Enable update when form load finished
            {
                byVal1 = 0;
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_CONFIG_SPOOL, byVal1);
            }
        }

        private void opt_OverWriteSpoolEnable_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true) //Enable update when form load finished
            {
                byVal1 = 1;
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_OVER_WRITE_SPOOL, byVal1);
            }
        }

        private void opt_OverWriteSpoolDisable_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true) //Enable update when form load finished
            {
                byVal1 = 0;
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_OVER_WRITE_SPOOL, byVal1);
            }
        }

        //**************************************************************************************
        //*  Event Report Area
        //**************************************************************************************
        private void SendEvent_Click(object sender, EventArgs e)
        {
            int lEventID;
            bool blnResult;

            blnResult = int.TryParse(EventID.Text, out lEventID);
            g_lOperationResult = CGWrapper.EventReportSend(lEventID);
        }

        private void opt_S6WbitOn_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true)
            {
                byVal1 = 1;
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_WBIT_S6, byVal1);
            }
        }

        private void opt_S6WbitOff_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true)
            {
                byVal1 = 0;
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_WBIT_S6, byVal1);
            }
        }

        //*******************************************************************************
        //* Ararm Report Area
        //*******************************************************************************
        private void AlarmReportSend_Click(object sender, EventArgs e)
        {
            int iAlarmID;
            int iAlarmCD;
            bool blnResult;

            blnResult = int.TryParse(AlarmID.Text, out iAlarmID);
            blnResult = int.TryParse(AlarmCD.Text, out iAlarmCD);
            g_lOperationResult = CGWrapper.AlarmReportSend(iAlarmID, iAlarmCD);
        }

        private void btnAlarmSet_Click(object sender, EventArgs e)
        {
            object objTemp1;
            object objTemp2;

            objTemp1 = (object)AlarmIDSet.Text;
            g_lOperationResult = CGWrapper.Command((int)PP_TYPE.CMD_ALARM_SET, ref objTemp1, out objTemp2);
        }

        private void opt_S5WbitOn_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true)
            {
                byVal1 = 1;
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_WBIT_S5, byVal1);
            }
        }

        private void opt_S5WbitOff_Click(object sender, EventArgs e)
        {
            object byVal1;
            if (g_fFormLoadFinishedFlag == true)
            {
                byVal1 = 0;
                g_lOperationResult = CGWrapper.UpdateEC(GemSystemID.GEM_WBIT_S5, byVal1);
            }
        }

        //**************************************************************************************
        //*  Terminal Area
        //**************************************************************************************
        private void btn_MsgRecognitionEvent_Click(object sender, EventArgs e)
        {
            g_lOperationResult = CGWrapper.EventReportSend(GemSystemID.GEM_MESSAGE_RECOGNITION);
        }

        private void SendTerminalMsg_Click(object sender, EventArgs e)
        {
            g_lOperationResult = CGWrapper.SendTerminalMessage(txt_TerminalTextToHost.Text);
        }

        //**************************************************************************************
        //*  Equipment Management Area
        //**************************************************************************************
        private void GetEC_Click(object sender, EventArgs e)
        {
            int lECID;
            object Value;
            EC_DATA_TYPE lGetFromat;
            bool blnResult;

            blnResult = int.TryParse(ECID.Text, out lECID);

            g_lOperationResult = CGWrapper.GetEC(lECID, out lGetFromat, out Value);
            if (g_lOperationResult == 0)
            {
                ECVal.Text = Value.ToString();
                ShowECType(lGetFromat);
            }
        }

        private void UpdateEC_Click(object sender, EventArgs e)
        {
            int intECID;

            int.TryParse(ECID.Text.Trim(), out intECID);
            Update_EC(intECID, ECVal.Text.Trim());

            if (MachineType == "TrimGap")
            {
                switch (intECID)
                {
                    //ECID
                    case TrimGap_EqpID.ChunkRotateDistance:
                        TrimGap_EqpParameter.ChunkRotateDistance = ECVal.Text;
                        break;

                    case TrimGap_EqpID.Cofficient:
                        TrimGap_EqpParameter.Cofficient = ECVal.Text;
                        break;

                    case TrimGap_EqpID.RobotSpeed:
                        TrimGap_EqpParameter.RobotSpeed = ECVal.Text;
                        break;

                    case TrimGap_EqpID.MotionRotate:
                        TrimGap_EqpParameter.MotionRotate = ECVal.Text;
                        break;

                    case TrimGap_EqpID.SoftWareVersion:
                        TrimGap_EqpParameter.SoftWareVersion = ECVal.Text;
                        break;

                    default:
                        AppendText(" ※ID doesn't exist!!" + Environment.NewLine);
                        return;
                }
                AppendText(" Update EC " + Environment.NewLine);
            }
        }

        private void Update_EC(int ECID, string ECVal)
        {
            byte byVal1;
            int iVal2;
            float fVal3;
            double dVal5;
            bool bVal6;

            int lECID;
            object Value;
            EC_DATA_TYPE lGetFromat;
            object objTemp;
            string strTemp;
            ushort uintVal11;
            short shtVal12;

            lECID = ECID;

            g_lOperationResult = CGWrapper.GetEC(lECID, out lGetFromat, out Value);

            if (g_lOperationResult != 0)
            {
                return;
            }

            switch (lGetFromat)
            {
                case EC_DATA_TYPE.EC_BINARY_TYPE:
                    byte.TryParse(ECVal, out byVal1);
                    objTemp = (object)byVal1;
                    g_lOperationResult = CGWrapper.UpdateEC(lECID, objTemp);
                    txt_ECType.Text = "BINARY";
                    break;

                case EC_DATA_TYPE.EC_BOOLEAN_TYPE:
                    strTemp = ECVal.ToLower();
                    if (strTemp == "true")
                    {
                        bVal6 = true;
                    }
                    else
                    {
                        bVal6 = false;
                    }
                    objTemp = (object)bVal6.GetHashCode();
                    g_lOperationResult = CGWrapper.UpdateEC(lECID, objTemp);
                    txt_ECType.Text = "BOOLEAN";
                    break;

                case EC_DATA_TYPE.EC_UINT_1_TYPE:
                    byte.TryParse(ECVal, out byVal1);
                    objTemp = (object)byVal1;
                    g_lOperationResult = CGWrapper.UpdateEC(lECID, objTemp);
                    txt_ECType.Text = "UINT_1";
                    break;

                case EC_DATA_TYPE.EC_UINT_2_TYPE:
                    int.TryParse(ECVal, out iVal2);
                    objTemp = (object)iVal2;
                    g_lOperationResult = CGWrapper.UpdateEC(lECID, objTemp);
                    txt_ECType.Text = "UINT_2";
                    break;

                case EC_DATA_TYPE.EC_UINT_4_TYPE:
                    UInt16.TryParse(ECVal, out uintVal11);
                    objTemp = (object)uintVal11;
                    g_lOperationResult = CGWrapper.UpdateEC(lECID, objTemp);
                    txt_ECType.Text = "UINT_4";
                    break;

                case EC_DATA_TYPE.EC_INT_1_TYPE:
                    Int16.TryParse(ECVal, out shtVal12);
                    objTemp = (object)shtVal12;
                    g_lOperationResult = CGWrapper.UpdateEC(lECID, objTemp);
                    txt_ECType.Text = "INT_1";
                    break;

                case EC_DATA_TYPE.EC_INT_2_TYPE:
                    Int32.TryParse(ECVal, out iVal2);
                    objTemp = (object)iVal2;
                    g_lOperationResult = CGWrapper.UpdateEC(lECID, objTemp);
                    txt_ECType.Text = "INT_2";
                    break;

                case EC_DATA_TYPE.EC_INT_4_TYPE:
                    Int32.TryParse(ECVal, out iVal2);
                    objTemp = (object)iVal2;
                    g_lOperationResult = CGWrapper.UpdateEC(lECID, objTemp);
                    txt_ECType.Text = "INT_4";
                    break;

                case EC_DATA_TYPE.EC_FT_4_TYPE:
                    float.TryParse(ECVal.ToString(), out fVal3);
                    objTemp = (object)fVal3;
                    g_lOperationResult = CGWrapper.UpdateEC(lECID, objTemp);
                    txt_ECType.Text = "FT_4";
                    break;

                case EC_DATA_TYPE.EC_FT_8_TYPE:
                    double.TryParse(ECVal, out dVal5);
                    objTemp = (object)dVal5;
                    g_lOperationResult = CGWrapper.UpdateEC(lECID, objTemp);
                    txt_ECType.Text = "FT_8";
                    break;

                case EC_DATA_TYPE.EC_ASCII_TYPE:
                    strTemp = ECVal;
                    objTemp = (object)strTemp;
                    g_lOperationResult = CGWrapper.UpdateEC(lECID, objTemp);
                    txt_ECType.Text = "ASCII";
                    break;
            }
        }

        private void ShowECType(EC_DATA_TYPE lGetFromat)
        {
            switch (lGetFromat)
            {
                case EC_DATA_TYPE.EC_BINARY_TYPE:
                    txt_ECType.Text = "BINARY";
                    break;

                case EC_DATA_TYPE.EC_BOOLEAN_TYPE:
                    txt_ECType.Text = "BOOLEAN";
                    break;

                case EC_DATA_TYPE.EC_UINT_1_TYPE:
                    txt_ECType.Text = "UINT_1";
                    break;

                case EC_DATA_TYPE.EC_UINT_2_TYPE:
                    txt_ECType.Text = "UINT_2";
                    break;

                case EC_DATA_TYPE.EC_UINT_4_TYPE:
                    txt_ECType.Text = "UINT_4";
                    break;

                case EC_DATA_TYPE.EC_INT_1_TYPE:
                    txt_ECType.Text = "INT_1";
                    break;

                case EC_DATA_TYPE.EC_INT_2_TYPE:
                    txt_ECType.Text = "INT_2";
                    break;

                case EC_DATA_TYPE.EC_INT_4_TYPE:
                    txt_ECType.Text = "INT_4";
                    break;

                case EC_DATA_TYPE.EC_FT_4_TYPE:
                    txt_ECType.Text = "FT_4";
                    break;

                case EC_DATA_TYPE.EC_FT_8_TYPE:
                    txt_ECType.Text = "FT_8";
                    break;

                case EC_DATA_TYPE.EC_ASCII_TYPE:
                    txt_ECType.Text = "ASCII";
                    break;
            }
        }

        private void Cmd_LoadInquire_Click(object sender, EventArgs e)
        {
            object objTemp1;
            object objTemp2;

            PPEventText.Text = "";
            objTemp1 = (object)txtPPID.Text;
            g_lOperationResult = CGWrapper.Command((int)PP_TYPE.CMD_PP_LOAD_INQUIRE, ref objTemp1, out objTemp2);
        }

        private void Cmd_SendPP_Click(object sender, EventArgs e)
        {
            object objTemp1;
            object objTemp2;
            PPEventText.Text = "";
            objTemp1 = (object)txtPPID.Text;
            g_lOperationResult = CGWrapper.Command((int)PP_TYPE.CMD_UNFORMATTED_PP_SEND, ref objTemp1, out objTemp2);
        }

        private void Cmd_RequestPP_Click(object sender, EventArgs e)
        {
            object objTemp1;
            object objTemp2;
            PPEventText.Text = "";
            objTemp1 = (object)txtPPID.Text;
            g_lOperationResult = CGWrapper.Command((int)PP_TYPE.CMD_UNFORMATTED_PP_REQUEST, ref objTemp1, out objTemp2);
        }

        private void Cmd_SendPP_Formatted_Click(object sender, EventArgs e)
        {
            object objTemp1;
            object objTemp2;
            PPEventText.Text = "";
            objTemp1 = (object)txtPPID.Text;
            g_lOperationResult = CGWrapper.Command((int)PP_TYPE.CMD_FORMATTED_PP_SEND, ref objTemp1, out objTemp2);
        }

        private void Cmd_RequestPP_Formatted_Click(object sender, EventArgs e)
        {
            object objTemp1;
            object objTemp2;
            PPEventText.Text = "";
            objTemp1 = (object)txtPPID.Text;
            g_lOperationResult = CGWrapper.Command((int)QGACTIVEXLib.PP_TYPE.CMD_FORMATTED_PP_REQUEST, ref objTemp1, out objTemp2);
        }

        #region SMT Flag

        //MAGAZINE_MOVEIN
        public bool blnS2F41_MAGAZINE_MOVEIN_LOTID = false;

        public bool blnS2F41_MAGAZINE_MOVEIN_MAGAZINEID = false;
        public bool blnS2F41_MAGAZINE_MOVEIN_STATUS = false;

        //BOAT_MOVEIN
        public bool blnS2F41_BOAT_MOVEIN_BARCODE = false;

        public bool blnS2F41_BOAT_MOVEIN_STATUS = false;
        public bool blnS2F41_RECIPE = false;

        //PASSBOAT_MOVEOUT
        public bool blnS2F41_PASSBOAT_MOVEOUT_BARCODE = false;

        //NGBOAT_MOVEOUT
        public bool blnS2F41_NGBOAT_MOVEOUT_BARCODE = false;

        //EMPTYBOAT_RELEASE
        public bool blnS2F41_EMPTYBOAT_RELEASE_BARCODE = false;

        public bool blnS2F41_EMPTYBOAT_RELEASE_STATUS = false;

        //PASS1MAGAZINE_MOVEIN
        public bool blnS2F41_PASS1MAGAZINE_MOVEIN_ID = false;

        public bool blnS2F41_PASS1MAGAZINE_MOVEIN_STATUS = false;

        //PASS2MAGAZINE_MOVEIN
        public bool blnS2F41_PASS2MAGAZINE_MOVEIN_ID = false;

        public bool blnS2F41_PASS2MAGAZINE_MOVEIN_STATUS = false;

        //NGMAGAZINE_MOVEIN
        public bool blnS2F41_NGMAGAZINE_MOVEIN_ID = false;

        public bool blnS2F41_NGMAGAZINE_MOVEIN_STATUS = false;

        //EMPTYMAGAZINE_MOVEIN
        public bool blnS2F41_EMPTYMAGAZINE_MOVEIN_ID = false;

        public bool blnS2F41_EMPTYMAGAZINE_MOVEIN_STATUS = false;

        //PASS1MAGAZINE_MOVEOUT
        public bool blnS2F41_PASS1MAGAZINE_MOVEOUT_ID = false;

        public bool blnS2F41_PASS1MAGAZINE_MOVEOUT_STATUS = false;

        //PASS2MAGAZINE_MOVEOUT
        public bool blnS2F41_PASS2MAGAZINE_MOVEOUT_ID = false;

        public bool blnS2F41_PASS2MAGAZINE_MOVEOUT_STATUS = false;

        //NGMAGAZINE_MOVEOUT
        public bool blnS2F41_NGMAGAZINE_MOVEOUT_ID = false;

        public bool blnS2F41_NGMAGAZINE_MOVEOUT_STATUS = false;

        //BOAT_ASSING_NGBOAT
        public bool blnS2F41_BOAT_ASSING_NGBOAT = false;

        public bool blnS2F41_BOAT_ASSING_NGBOAT_STATUS = false;

        //UI_LOCK
        public bool blnS2F41_UI_LOCK_COMMAND = false;

        //EQUIPMENT_OPERATIONAL
        public bool blnS2F41_EQUIPMENT_OPERATIONAL_COMMAND = false;

        //EQUIPMENT_ENDLOT
        public bool blnS2F41_EQUIPMENT_ENDLOT_COMMAND = false;

        //EQUIPMENT_STARTRUN
        public bool blnS2F41_EQUIPMENT_STARTRUN_STATUS = false;

        #endregion SMT Flag

        #region FVI Flag

        //REELID_VERIFY_RESULT
        public bool blnS2F41_APN = false;

        public bool blnS2F41_DateCode = false;
        public bool blnS2F41_Lot6 = false;

        //RECIPE_CHANGE
        public bool blnS2F41_Current_RecipeName = false;

        //EQUIPMENT_REMOTECONTROL
        public bool blnS2F41_EQUIPMENT_REMOTE_CONTROL = false;

        public bool DoOnce = true;

        //DIECOUNT_COMPARERESULT
        public bool blnS2F41_EXPECTED_DIECOUNT = false;

        public bool blnS2F41_DIECOUNT_COMPARE_RESULT = false;

        #endregion FVI Flag

        #region TrimGap Flag

        //ACCESSMODE-STATUS-REQUEST
        public string LoadPortID = "";

        public string CarrierID = "";
        public string RecipeID = "";
        public string AccessMode = "";

        public bool blnS2F41_LoadPortID = false;
        public bool blnS2F41_CarrierID = false;
        public bool blnS2F41_RecipeID = false;
        public bool blnS2F41_AccessMode = false;

        public bool bWaitSECS_ACCESSMODE_ASK = false;
        public bool bWaitSECS_PORT_TRANSFERSTATUS_ASK = false;
        public bool bWaitSECS_PP_SELECT = false;
        public bool bWaitSECS_SlotMapCmd = false;
        public bool bWaitSECS_MeasureCmd = false;
        public bool bWaitSECS_ReleaseCmd = false;
        public bool bWaitSECS_CancelCmd = false;
        public bool bSECS_ReadyToLoad_LP1 = false;
        public bool bSECS_ReadyToLoad_LP2 = false;
        public bool bSECS_ChangeAccessMode_Recive = false; // 判斷有沒有收到
        public bool bWaitSECS_StartCmd = false;
        public bool bWaitSECS_StopCmd = false;
        public int Control_State = 0; // 3:Offline, 4:Local, 5:Remote
        public int Comm_State = 0; // 1:DISABLE, 3:NOT_COMMUNICATING, 7:COMMUNICATING

        public class SecsDataTemp
        {
            public string LoadPortID;
            public string CarrierID;

            public SecsDataTemp()
            {
                LoadPortID = "";
                CarrierID = "";
            }

            public void Clear()
            {
                LoadPortID = "";
                CarrierID = "";
            }

            public void Set(string lpID, string caID = "")
            {
                LoadPortID = lpID;
                CarrierID = caID;
            }

            public bool IsEmpty()
            {
                return LoadPortID == "" && CarrierID == "";
            }
        }

        public SecsDataTemp SlotMapData = new SecsDataTemp();
        public SecsDataTemp ReleaseData = new SecsDataTemp();
        public SecsDataTemp MeasureStartData = new SecsDataTemp();
        public SecsDataTemp CancelData = new SecsDataTemp();
        public SecsDataTemp AccessModeChangeData = new SecsDataTemp();
        public SecsDataTemp AccessModeAskData = new SecsDataTemp();
        public SecsDataTemp ChangeRecipeData = new SecsDataTemp();

        #endregion TrimGap Flag

        private int ExeS2F41RCMDAction(int ulSystemBytes, object RawData)
        {
            int lItemBytes = 0;
            object ItemData = null;
            object OutputRawData = null;
            int lOffset = 0;
            string strTemp = null;
            string strRCMD = null;
            int[] lTemp = new int[11];
            int intIndex = 0;

            int lngCPItemCount = 0;
            object Value = null;
            int[] l_IntTmp;

            int iHCACK = 0;
            int iCPACK1 = 0;
            int iCPACK2 = 0;
            int iCPACK3 = 0;

            #region SMT Flag

            //MAGAZINE_MOVEIN
            blnS2F41_MAGAZINE_MOVEIN_LOTID = false;
            blnS2F41_MAGAZINE_MOVEIN_MAGAZINEID = false;
            blnS2F41_MAGAZINE_MOVEIN_STATUS = false;

            //BOAT_MOVEIN
            blnS2F41_BOAT_MOVEIN_BARCODE = false;
            blnS2F41_BOAT_MOVEIN_STATUS = false;
            blnS2F41_RECIPE = false;

            //PASSBOAT_MOVEOUT
            blnS2F41_PASSBOAT_MOVEOUT_BARCODE = false;

            //NGBOAT_MOVEOUT
            blnS2F41_NGBOAT_MOVEOUT_BARCODE = false;

            //EMPTYBOAT_RELEASE
            blnS2F41_EMPTYBOAT_RELEASE_BARCODE = false;
            blnS2F41_EMPTYBOAT_RELEASE_STATUS = false;

            //PASS1MAGAZINE_MOVEIN
            blnS2F41_PASS1MAGAZINE_MOVEIN_ID = false;
            blnS2F41_PASS1MAGAZINE_MOVEIN_STATUS = false;

            //PASS2MAGAZINE_MOVEIN
            blnS2F41_PASS2MAGAZINE_MOVEIN_ID = false;
            blnS2F41_PASS2MAGAZINE_MOVEIN_STATUS = false;

            //NGMAGAZINE_MOVEIN
            blnS2F41_NGMAGAZINE_MOVEIN_ID = false;
            blnS2F41_NGMAGAZINE_MOVEIN_STATUS = false;

            //EMPTYMAGAZINE_MOVEIN
            blnS2F41_EMPTYMAGAZINE_MOVEIN_ID = false;
            blnS2F41_EMPTYMAGAZINE_MOVEIN_STATUS = false;

            //PASS1MAGAZINE_MOVEOUT
            blnS2F41_PASS1MAGAZINE_MOVEOUT_ID = false;
            blnS2F41_PASS1MAGAZINE_MOVEOUT_STATUS = false;

            //PASS2MAGAZINE_MOVEOUT
            blnS2F41_PASS2MAGAZINE_MOVEOUT_ID = false;
            blnS2F41_PASS2MAGAZINE_MOVEOUT_STATUS = false;

            //NGMAGAZINE_MOVEOUT
            blnS2F41_NGMAGAZINE_MOVEOUT_ID = false;
            blnS2F41_NGMAGAZINE_MOVEOUT_STATUS = false;

            //BOAT_ASSING_NGBOAT
            blnS2F41_BOAT_ASSING_NGBOAT = false;
            blnS2F41_BOAT_ASSING_NGBOAT_STATUS = false;

            //UI_LOCK
            blnS2F41_UI_LOCK_COMMAND = false;

            //EQUIPMENT_OPERATIONAL
            blnS2F41_EQUIPMENT_OPERATIONAL_COMMAND = false;

            //EQUIPMENT_ENDLOT
            blnS2F41_EQUIPMENT_ENDLOT_COMMAND = false;

            //EQUIPMENT_STARTRUN
            blnS2F41_EQUIPMENT_STARTRUN_STATUS = false;

            #endregion SMT Flag

            #region FVI flag Init

            //REELID_VERIFY_RESULT
            blnS2F41_APN = false;
            blnS2F41_DateCode = false;
            blnS2F41_Lot6 = false;

            //RECIPE_CHANGE
            blnS2F41_Current_RecipeName = false;

            //EQUIPMENT_REMOTECONTROL
            blnS2F41_EQUIPMENT_REMOTE_CONTROL = false;

            //DIECOUNT_COMPARERESULT
            blnS2F41_EXPECTED_DIECOUNT = false;
            blnS2F41_DIECOUNT_COMPARE_RESULT = false;

            #endregion FVI flag Init

            #region TrimGap flag init

            LoadPortID = "";
            CarrierID = "";
            RecipeID = "";
            AccessMode = "";
            blnS2F41_LoadPortID = false;
            blnS2F41_CarrierID = false;
            blnS2F41_RecipeID = false;
            blnS2F41_AccessMode = false;

            #endregion TrimGap flag init

            if (g_iGemControlState == 4)
            {
                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);

                if (lItemBytes != 2)
                    return (1);

                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                strRCMD = ItemData.ToString().ToUpper();
                lbRemoteCmd.Text = strRCMD;
                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);

                lbCPNAME1.Text = "";
                lbCPVAL1.Text = "";
                lbCPNAME2.Text = "";
                lbCPVAL2.Text = "";

                if ((MachineType == "FVI" || MachineType == "TrimGap") && strRCMD == "GO-REMOTE")
                    g_lOperationResult = CGWrapper.OnLineRemote();
                else
                    iHCACK = 2;  //2：Cannot perform now
            }
            else if (g_iGemControlState == 5) //Online Remote
            {
                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);

                if (lItemBytes != 2)
                    return (1);

                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                strRCMD = ItemData.ToString().ToUpper();
                lbRemoteCmd.Text = strRCMD;
                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);

                lbCPNAME1.Text = "";
                lbCPVAL1.Text = "";
                lbCPNAME2.Text = "";
                lbCPVAL2.Text = "";

                //SMT
                if (MachineType == "SMT")
                {
                    if (strRCMD == "MAGAZINE_MOVEIN")
                    {
                        //S2F41傳過來的CP Item=數
                        lngCPItemCount = lItemBytes;

                        //Host有傳CP Data過來
                        if (lngCPItemCount != 0)
                        {
                            //Check S2F41傳送過來的CPName與CPVAL是否合法
                            for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                            {
                                //  <L[2]
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                if (lItemBytes != 2)
                                {
                                    return (1);
                                }

                                //Decode CPName
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                strTemp = ItemData.ToString().ToUpper();
                                switch (strTemp)
                                {
                                    case "LOT_ID":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.LotID = ItemData.ToString();
                                        label_LotID.Text = "Lot ID : " + ItemData.ToString();
                                        blnS2F41_MAGAZINE_MOVEIN_LOTID = true;
                                        break;

                                    case "MAGAZINE_ID":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.MagazineID = ItemData.ToString();
                                        label_LD_MagazineID.Text = "Magazine ID : " + ItemData.ToString();
                                        blnS2F41_MAGAZINE_MOVEIN_MAGAZINEID = true;
                                        break;

                                    case "MAGAZINE_MOVEIN_STATUS":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.MagazineIDCheck = ItemData.ToString();
                                        blnS2F41_MAGAZINE_MOVEIN_STATUS = true;
                                        break;

                                    default:
                                        iHCACK = 3;       //3 = At least one parameter is invalid
                                        break;
                                }
                            }
                            if (blnS2F41_MAGAZINE_MOVEIN_LOTID == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                            if (blnS2F41_MAGAZINE_MOVEIN_MAGAZINEID == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                            if (blnS2F41_MAGAZINE_MOVEIN_STATUS == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK3 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                        }
                        else
                        {
                            iHCACK = 3;    //3 = At least one parameter is invalid
                            iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                            iCPACK2 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                            iCPACK3 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                        }
                    }
                    else if (strRCMD == "BOAT_MOVEIN")
                    {
                        //S2F41傳過來的CP Item=數
                        lngCPItemCount = lItemBytes;

                        //Host有傳CP Data過來
                        if (lngCPItemCount != 0)
                        {
                            //Check S2F41傳送過來的CPName與CPVAL是否合法
                            for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                            {
                                //  <L[2]
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                if (lItemBytes != 2)
                                {
                                    return (1);
                                }

                                //Decode CPName
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                strTemp = ItemData.ToString().ToUpper();
                                switch (strTemp)
                                {
                                    case "BOAT_BARCODE":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.BoatBarcode = ItemData.ToString();
                                        blnS2F41_BOAT_MOVEIN_BARCODE = true;
                                        break;

                                    case "BOAT_MOVEIN_STATUS":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.BoatBarcodeCheck = ItemData.ToString();
                                        blnS2F41_BOAT_MOVEIN_STATUS = true;
                                        break;

                                    default:
                                        iHCACK = 3;       //3 = At least one parameter is invalid
                                        break;
                                }
                            }
                            if (blnS2F41_BOAT_MOVEIN_BARCODE == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                            if (blnS2F41_BOAT_MOVEIN_STATUS == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                        }
                        else
                        {
                            iHCACK = 3;    //3 = At least one parameter is invalid
                            iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                            iCPACK2 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                        }
                    }
                    //else if (strRCMD == "PASSBOAT_MOVEOUT")
                    //{
                    //    //S2F41傳過來的CP Item=數
                    //    lngCPItemCount = lItemBytes;

                    //    //Host有傳CP Data過來
                    //    if (lngCPItemCount != 0)
                    //    {
                    //        //Check S2F41傳送過來的CPName與CPVAL是否合法
                    //        for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                    //        {
                    //            //  <L[2]
                    //            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                    //            if (lItemBytes != 2)
                    //            {
                    //                return (1);
                    //            }

                    //            //Decode CPName
                    //            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                    //            strTemp = ItemData.ToString().ToUpper();
                    //            switch (strTemp)
                    //            {
                    //                case "PASSBOAT_BARCODE":
                    //                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                    //                    SMT_EqpParameter.PassBoatBarcode = ItemData.ToString();
                    //                    blnS2F41_PASSBOAT_MOVEOUT_BARCODE = true;
                    //                    break;
                    //                default:
                    //                    iHCACK = 3;       //3 = At least one parameter is invalid
                    //                    break;
                    //            }
                    //        }
                    //        if (blnS2F41_PASSBOAT_MOVEOUT_BARCODE == false)
                    //        {
                    //            iHCACK = 3;     //3 = At least one parameter is invalid
                    //            iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                    //        }
                    //    }
                    //    else
                    //    {
                    //        iHCACK = 3;    //3 = At least one parameter is invalid
                    //        iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                    //    }
                    //}
                    //else if (strRCMD == "NGBOAT_MOVEOUT")
                    //{
                    //    //S2F41傳過來的CP Item=數
                    //    lngCPItemCount = lItemBytes;

                    //    //Host有傳CP Data過來
                    //    if (lngCPItemCount != 0)
                    //    {
                    //        //Check S2F41傳送過來的CPName與CPVAL是否合法
                    //        for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                    //        {
                    //            //  <L[2]
                    //            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                    //            if (lItemBytes != 2)
                    //            {
                    //                return (1);
                    //            }

                    //            //Decode CPName
                    //            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                    //            strTemp = ItemData.ToString().ToUpper();
                    //            switch (strTemp)
                    //            {
                    //                case "NGBOAT_BARCODE":
                    //                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                    //                    SMT_EqpParameter.NGBoatBarcode = ItemData.ToString();
                    //                    blnS2F41_NGBOAT_MOVEOUT_BARCODE = true;
                    //                    break;
                    //                default:
                    //                    iHCACK = 3;       //3 = At least one parameter is invalid
                    //                    break;
                    //            }
                    //        }
                    //        if (blnS2F41_NGBOAT_MOVEOUT_BARCODE == false)
                    //        {
                    //            iHCACK = 3;     //3 = At least one parameter is invalid
                    //            iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                    //        }
                    //    }
                    //    else
                    //    {
                    //        iHCACK = 3;    //3 = At least one parameter is invalid
                    //        iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                    //    }
                    //}
                    else if (strRCMD == "EMPTYBOAT_RELEASE")
                    {
                        //S2F41傳過來的CP Item=數
                        lngCPItemCount = lItemBytes;

                        //Host有傳CP Data過來
                        if (lngCPItemCount != 0)
                        {
                            //Check S2F41傳送過來的CPName與CPVAL是否合法
                            for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                            {
                                //  <L[2]
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                if (lItemBytes != 2)
                                {
                                    return (1);
                                }

                                //Decode CPName
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                strTemp = ItemData.ToString().ToUpper();
                                switch (strTemp)
                                {
                                    case "EMPTYBOAT_BARCODE":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.EmptyBoatBarcode = ItemData.ToString();
                                        blnS2F41_EMPTYBOAT_RELEASE_BARCODE = true;
                                        break;

                                    case "EMPTYBOAT_RELEASE_STATUS":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.EmptyBoatReleaseCheck = ItemData.ToString();
                                        blnS2F41_EMPTYBOAT_RELEASE_STATUS = true;
                                        break;

                                    default:
                                        iHCACK = 3;       //3 = At least one parameter is invalid
                                        break;
                                }
                            }
                            if (blnS2F41_EMPTYBOAT_RELEASE_BARCODE == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                            if (blnS2F41_EMPTYBOAT_RELEASE_STATUS == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                        }
                        else
                        {
                            iHCACK = 3;    //3 = At least one parameter is invalid
                            iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                            iCPACK2 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                        }
                    }
                    //else if (strRCMD == "PASS1MAGAZINE_MOVEIN")
                    //{
                    //    //S2F41傳過來的CP Item=數
                    //    lngCPItemCount = lItemBytes;

                    //    //Host有傳CP Data過來
                    //    if (lngCPItemCount != 0)
                    //    {
                    //        //Check S2F41傳送過來的CPName與CPVAL是否合法
                    //        for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                    //        {
                    //            //  <L[2]
                    //            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                    //            if (lItemBytes != 2)
                    //            {
                    //                return (1);
                    //            }

                    //            //Decode CPName
                    //            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                    //            strTemp = ItemData.ToString().ToUpper();
                    //            switch (strTemp)
                    //            {
                    //                case "PASS1MAGAZINE_ID":
                    //                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                    //                    SMT_EqpParameter.Pass1MagazineID = ItemData.ToString();
                    //                    label_ULD_Pass1MagazineID.Text = "Magazine ID : " + ItemData.ToString();
                    //                    blnS2F41_PASS1MAGAZINE_MOVEIN_ID = true;
                    //                    break;
                    //                case "PASS1MAGAZINE_MOVEIN_STATUS":
                    //                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                    //                    SMT_EqpParameter.Pass1MagazineIDCheck = ItemData.ToString();
                    //                    blnS2F41_PASS1MAGAZINE_MOVEIN_STATUS = true;
                    //                    break;
                    //                default:
                    //                    iHCACK = 3;       //3 = At least one parameter is invalid
                    //                    break;
                    //            }
                    //        }
                    //        if (blnS2F41_PASS1MAGAZINE_MOVEIN_ID == false)
                    //        {
                    //            iHCACK = 3;     //3 = At least one parameter is invalid
                    //            iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                    //        }
                    //        if (blnS2F41_PASS1MAGAZINE_MOVEIN_STATUS == false)
                    //        {
                    //            iHCACK = 3;     //3 = At least one parameter is invalid
                    //            iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                    //        }
                    //    }
                    //    else
                    //    {
                    //        iHCACK = 3;    //3 = At least one parameter is invalid
                    //        iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                    //        iCPACK2 = 2;    //1 = Parameter Name (CPNAME) does Not exist
                    //    }
                    //}
                    //else if (strRCMD == "PASS2MAGAZINE_MOVEIN")
                    //{
                    //    //S2F41傳過來的CP Item=數
                    //    lngCPItemCount = lItemBytes;

                    //    //Host有傳CP Data過來
                    //    if (lngCPItemCount != 0)
                    //    {
                    //        //Check S2F41傳送過來的CPName與CPVAL是否合法
                    //        for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                    //        {
                    //            //  <L[2]
                    //            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                    //            if (lItemBytes != 2)
                    //            {
                    //                return (1);
                    //            }

                    //            //Decode CPName
                    //            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                    //            strTemp = ItemData.ToString().ToUpper();
                    //            switch (strTemp)
                    //            {
                    //                case "PASS2MAGAZINE_ID":
                    //                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                    //                    SMT_EqpParameter.Pass2MagazineID = ItemData.ToString();
                    //                    label_ULD_Pass2MagazineID.Text = "Magazine ID : " + ItemData.ToString();
                    //                    blnS2F41_PASS2MAGAZINE_MOVEIN_ID = true;
                    //                    break;
                    //                case "PASS2MAGAZINE_MOVEIN_STATUS":
                    //                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                    //                    SMT_EqpParameter.Pass2MagazineIDCheck = ItemData.ToString();
                    //                    blnS2F41_PASS2MAGAZINE_MOVEIN_STATUS = true;
                    //                    break;
                    //                default:
                    //                    iHCACK = 3;       //3 = At least one parameter is invalid
                    //                    break;
                    //            }
                    //        }
                    //        if (blnS2F41_PASS2MAGAZINE_MOVEIN_ID == false)
                    //        {
                    //            iHCACK = 3;     //3 = At least one parameter is invalid
                    //            iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                    //        }
                    //        if (blnS2F41_PASS2MAGAZINE_MOVEIN_STATUS == false)
                    //        {
                    //            iHCACK = 3;     //3 = At least one parameter is invalid
                    //            iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                    //        }
                    //    }
                    //    else
                    //    {
                    //        iHCACK = 3;    //3 = At least one parameter is invalid
                    //        iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                    //        iCPACK2 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                    //    }
                    //}
                    //else if (strRCMD == "NGMAGAZINE_MOVEIN")
                    //{
                    //    //S2F41傳過來的CP Item=數
                    //    lngCPItemCount = lItemBytes;

                    //    //Host有傳CP Data過來
                    //    if (lngCPItemCount != 0)
                    //    {
                    //        //Check S2F41傳送過來的CPName與CPVAL是否合法
                    //        for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                    //        {
                    //            //  <L[2]
                    //            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                    //            if (lItemBytes != 2)
                    //            {
                    //                return (1);
                    //            }

                    //            //Decode CPName
                    //            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                    //            strTemp = ItemData.ToString().ToUpper();
                    //            switch (strTemp)
                    //            {
                    //                case "NGMAGAZINE_ID":
                    //                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                    //                    SMT_EqpParameter.NGMagazineID = ItemData.ToString();
                    //                    label_ULD_NGMagazineID.Text = "Magazine ID : " + ItemData.ToString();
                    //                    blnS2F41_NGMAGAZINE_MOVEIN_ID = true;
                    //                    break;
                    //                case "NGMAGAZINE_MOVEIN_STATUS":
                    //                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                    //                    SMT_EqpParameter.NGMagazineIDCheck = ItemData.ToString();
                    //                    blnS2F41_NGMAGAZINE_MOVEIN_STATUS = true;
                    //                    break;
                    //                default:
                    //                    iHCACK = 3;       //3 = At least one parameter is invalid
                    //                    break;
                    //            }
                    //        }
                    //        if (blnS2F41_NGMAGAZINE_MOVEIN_ID == false)
                    //        {
                    //            iHCACK = 3;     //3 = At least one parameter is invalid
                    //            iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                    //        }
                    //        if (blnS2F41_NGMAGAZINE_MOVEIN_STATUS == false)
                    //        {
                    //            iHCACK = 3;     //3 = At least one parameter is invalid
                    //            iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                    //        }
                    //    }
                    //    else
                    //    {
                    //        iHCACK = 3;    //3 = At least one parameter is invalid
                    //        iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                    //        iCPACK2 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                    //    }
                    //}
                    //else if (strRCMD == "EMPTYMAGAZINE_MOVEIN")
                    //{
                    //    //S2F41傳過來的CP Item=數
                    //    lngCPItemCount = lItemBytes;

                    //    //Host有傳CP Data過來
                    //    if (lngCPItemCount != 0)
                    //    {
                    //        //Check S2F41傳送過來的CPName與CPVAL是否合法
                    //        for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                    //        {
                    //            //  <L[2]
                    //            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                    //            if (lItemBytes != 2)
                    //            {
                    //                return (1);
                    //            }

                    //            //Decode CPName
                    //            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                    //            strTemp = ItemData.ToString().ToUpper();
                    //            switch (strTemp)
                    //            {
                    //                case "EMPTYMAGAZINE_ID":
                    //                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                    //                    SMT_EqpParameter.EmptyMagazineID = ItemData.ToString();
                    //                    label_ULD_EmptyMagazineID.Text = "Magazine ID : " + ItemData.ToString();
                    //                    blnS2F41_EMPTYMAGAZINE_MOVEIN_ID = true;
                    //                    break;
                    //                case "EMPTYMAGAZINE_MOVEIN_STATUS":
                    //                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                    //                    SMT_EqpParameter.EmptyMagazineIDCheck = ItemData.ToString();
                    //                    blnS2F41_EMPTYMAGAZINE_MOVEIN_STATUS = true;
                    //                    break;
                    //                default:
                    //                    iHCACK = 3;       //3 = At least one parameter is invalid
                    //                    break;
                    //            }
                    //        }
                    //        if (blnS2F41_EMPTYMAGAZINE_MOVEIN_ID == false)
                    //        {
                    //            iHCACK = 3;     //3 = At least one parameter is invalid
                    //            iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                    //        }
                    //        if (blnS2F41_EMPTYMAGAZINE_MOVEIN_STATUS == false)
                    //        {
                    //            iHCACK = 3;     //3 = At least one parameter is invalid
                    //            iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                    //        }
                    //    }
                    //    else
                    //    {
                    //        iHCACK = 3;    //3 = At least one parameter is invalid
                    //        iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                    //        iCPACK2 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                    //    }
                    //}
                    else if (strRCMD == "PASS1MAGAZINE_MOVEOUT")
                    {
                        //S2F41傳過來的CP Item=數
                        lngCPItemCount = lItemBytes;

                        //Host有傳CP Data過來
                        if (lngCPItemCount != 0)
                        {
                            //Check S2F41傳送過來的CPName與CPVAL是否合法
                            for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                            {
                                //  <L[2]
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                if (lItemBytes != 2)
                                {
                                    return (1);
                                }

                                //Decode CPName
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                strTemp = ItemData.ToString().ToUpper();
                                switch (strTemp)
                                {
                                    case "PASS1MAGAZINE_ID":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.Pass1MagazineID = ItemData.ToString();
                                        blnS2F41_PASS1MAGAZINE_MOVEOUT_ID = true;
                                        break;

                                    case "PASS1MAGAZINE_MOVEOUT_STATUS":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.Pass1MagazineIDCheck = ItemData.ToString();
                                        blnS2F41_PASS1MAGAZINE_MOVEOUT_STATUS = true;
                                        break;

                                    default:
                                        iHCACK = 3;       //3 = At least one parameter is invalid
                                        break;
                                }
                            }
                            if (blnS2F41_PASS1MAGAZINE_MOVEOUT_ID == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                            if (blnS2F41_PASS1MAGAZINE_MOVEOUT_STATUS == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                        }
                        else
                        {
                            iHCACK = 3;    //3 = At least one parameter is invalid
                            iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                            iCPACK2 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                        }
                    }
                    else if (strRCMD == "PASS2MAGAZINE_MOVEOUT")
                    {
                        //S2F41傳過來的CP Item=數
                        lngCPItemCount = lItemBytes;

                        //Host有傳CP Data過來
                        if (lngCPItemCount != 0)
                        {
                            //Check S2F41傳送過來的CPName與CPVAL是否合法
                            for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                            {
                                //  <L[2]
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                if (lItemBytes != 2)
                                {
                                    return (1);
                                }

                                //Decode CPName
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                strTemp = ItemData.ToString().ToUpper();
                                switch (strTemp)
                                {
                                    case "PASS2MAGAZINE_ID":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.Pass2MagazineID = ItemData.ToString();
                                        blnS2F41_PASS2MAGAZINE_MOVEOUT_ID = true;
                                        break;

                                    case "PASS2MAGAZINE_MOVEOUT_STATUS":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.Pass2MagazineIDCheck = ItemData.ToString();
                                        blnS2F41_PASS2MAGAZINE_MOVEOUT_STATUS = true;
                                        break;

                                    default:
                                        iHCACK = 3;       //3 = At least one parameter is invalid
                                        break;
                                }
                            }
                            if (blnS2F41_PASS2MAGAZINE_MOVEOUT_ID == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                            if (blnS2F41_PASS2MAGAZINE_MOVEOUT_STATUS == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                        }
                        else
                        {
                            iHCACK = 3;    //3 = At least one parameter is invalid
                            iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                            iCPACK2 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                        }
                    }
                    else if (strRCMD == "NGMAGAZINE_MOVEOUT")
                    {
                        //S2F41傳過來的CP Item=數
                        lngCPItemCount = lItemBytes;

                        //Host有傳CP Data過來
                        if (lngCPItemCount != 0)
                        {
                            //Check S2F41傳送過來的CPName與CPVAL是否合法
                            for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                            {
                                //  <L[2]
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                if (lItemBytes != 2)
                                {
                                    return (1);
                                }

                                //Decode CPName
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                strTemp = ItemData.ToString().ToUpper();
                                switch (strTemp)
                                {
                                    case "NGMAGAZINE_ID":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.NGMagazineID = ItemData.ToString();
                                        blnS2F41_NGMAGAZINE_MOVEOUT_ID = true;
                                        break;

                                    case "NGMAGAZINE_MOVEOUT_STATUS":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.NGMagazineIDCheck = ItemData.ToString();
                                        blnS2F41_NGMAGAZINE_MOVEOUT_STATUS = true;
                                        break;

                                    default:
                                        iHCACK = 3;       //3 = At least one parameter is invalid
                                        break;
                                }
                            }
                            if (blnS2F41_NGMAGAZINE_MOVEOUT_ID == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                            if (blnS2F41_NGMAGAZINE_MOVEOUT_STATUS == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                        }
                        else
                        {
                            iHCACK = 3;    //3 = At least one parameter is invalid
                            iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                            iCPACK2 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                        }
                    }
                    else if (strRCMD == "BOAT_ASSING_NGBOAT")
                    {
                        //S2F41傳過來的CP Item=數
                        lngCPItemCount = lItemBytes;

                        //Host有傳CP Data過來
                        if (lngCPItemCount != 0)
                        {
                            //Check S2F41傳送過來的CPName與CPVAL是否合法
                            for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                            {
                                //  <L[2]
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                if (lItemBytes != 2)
                                {
                                    return (1);
                                }

                                //Decode CPName
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                strTemp = ItemData.ToString().ToUpper();
                                switch (strTemp)
                                {
                                    case "BOAT_ASSING_NGBOAT_BARCODE":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.BoatAssingNGBoatBarcode = ItemData.ToString();
                                        blnS2F41_BOAT_ASSING_NGBOAT = true;
                                        break;

                                    case "BOAT_ASSING_NGBOAT_STATUS":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.BoatAssingNGBoatBarcodeCheck = ItemData.ToString();
                                        blnS2F41_BOAT_ASSING_NGBOAT_STATUS = true;
                                        break;

                                    default:
                                        iHCACK = 3;       //3 = At least one parameter is invalid
                                        break;
                                }
                            }
                            if (blnS2F41_BOAT_ASSING_NGBOAT == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                            if (blnS2F41_BOAT_ASSING_NGBOAT_STATUS == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                        }
                        else
                        {
                            iHCACK = 3;    //3 = At least one parameter is invalid
                            iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                            iCPACK2 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                        }
                    }
                    else if (strRCMD == "UI_LOCK_COMMAND")
                    {
                        //S2F41傳過來的CP Item=數
                        lngCPItemCount = lItemBytes;

                        //Host有傳CP Data過來
                        if (lngCPItemCount != 0)
                        {
                            //Check S2F41傳送過來的CPName與CPVAL是否合法
                            for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                            {
                                //  <L[2]
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                if (lItemBytes != 2)
                                {
                                    return (1);
                                }

                                //Decode CPName
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                strTemp = ItemData.ToString().ToUpper();
                                switch (strTemp)
                                {
                                    case "UI_LOCK_STATE":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.UILockCommand = ItemData.ToString();
                                        blnS2F41_UI_LOCK_COMMAND = true;
                                        break;

                                    default:
                                        iHCACK = 3;       //3 = At least one parameter is invalid
                                        break;
                                }
                            }
                            if (blnS2F41_UI_LOCK_COMMAND == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                        }
                        else
                        {
                            iHCACK = 3;    //3 = At least one parameter is invalid
                            iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                        }
                    }
                    else if (strRCMD == "EQUIPMENT_OPERATIONAL_COMMAND")
                    {
                        //S2F41傳過來的CP Item=數
                        lngCPItemCount = lItemBytes;

                        //Host有傳CP Data過來
                        if (lngCPItemCount != 0)
                        {
                            //Check S2F41傳送過來的CPName與CPVAL是否合法
                            for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                            {
                                //  <L[2]
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                if (lItemBytes != 2)
                                {
                                    return (1);
                                }

                                //Decode CPName
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                strTemp = ItemData.ToString().ToUpper();
                                switch (strTemp)
                                {
                                    case "EQUIPMENT_OPERATIONAL_STATE":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.EquipmentOperationalCommand = ItemData.ToString();
                                        blnS2F41_EQUIPMENT_OPERATIONAL_COMMAND = true;
                                        break;

                                    default:
                                        iHCACK = 3;       //3 = At least one parameter is invalid
                                        break;
                                }
                            }
                            if (blnS2F41_EQUIPMENT_OPERATIONAL_COMMAND == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                        }
                        else
                        {
                            iHCACK = 3;    //3 = At least one parameter is invalid
                            iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                        }
                    }
                    else if (strRCMD == "EQUIPMENT_ENDLOT_COMMAND")
                    {
                        //S2F41傳過來的CP Item=數
                        lngCPItemCount = lItemBytes;

                        //Host有傳CP Data過來
                        if (lngCPItemCount != 0)
                        {
                            //Check S2F41傳送過來的CPName與CPVAL是否合法
                            for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                            {
                                //  <L[2]
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                if (lItemBytes != 2)
                                {
                                    return (1);
                                }

                                //Decode CPName
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                strTemp = ItemData.ToString().ToUpper();
                                switch (strTemp)
                                {
                                    case "EQUIPMENT_ENDLOT":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.EquipmentEndLotCommand = ItemData.ToString();
                                        blnS2F41_EQUIPMENT_ENDLOT_COMMAND = true;
                                        break;

                                    default:
                                        iHCACK = 3;       //3 = At least one parameter is invalid
                                        break;
                                }
                            }
                            if (blnS2F41_EQUIPMENT_ENDLOT_COMMAND == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                        }
                        else
                        {
                            iHCACK = 3;    //3 = At least one parameter is invalid
                            iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                        }
                    }
                    else if (strRCMD == "EQUIPMENT_STARTRUN")
                    {
                        //S2F41傳過來的CP Item=數
                        lngCPItemCount = lItemBytes;

                        //Host有傳CP Data過來
                        if (lngCPItemCount != 0)
                        {
                            //Check S2F41傳送過來的CPName與CPVAL是否合法
                            for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                            {
                                //  <L[2]
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                if (lItemBytes != 2)
                                {
                                    return (1);
                                }

                                //Decode CPName
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                strTemp = ItemData.ToString().ToUpper();
                                switch (strTemp)
                                {
                                    case "EQUIPMENT_STARTRUN_STATUS":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        SMT_EqpParameter.EquipmentStartRunCheck = ItemData.ToString();
                                        blnS2F41_EQUIPMENT_STARTRUN_STATUS = true;
                                        break;

                                    default:
                                        iHCACK = 3;       //3 = At least one parameter is invalid
                                        break;
                                }
                            }
                            if (blnS2F41_EQUIPMENT_STARTRUN_STATUS == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                        }
                        else
                        {
                            iHCACK = 3;    //3 = At least one parameter is invalid
                            iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                        }
                    }
                    else
                    {
                        iHCACK = 1;   //1：Command does not exist
                    }
                }

                //FVI
                if (MachineType == "FVI")
                {
                    if (strRCMD == "GO-LOCAL")
                        g_lOperationResult = CGWrapper.OnLineLocal();
                    else if (strRCMD == "REELID_VERIFY_RESULT")
                    {
                        //S2F41傳過來的CP Item=數
                        lngCPItemCount = lItemBytes;

                        //Host有傳CP Data過來
                        if (lngCPItemCount != 0)
                        {
                            //Check S2F41傳送過來的CPName與CPVAL是否合法
                            for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                            {
                                //  <L[2]
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                if (lItemBytes != 2)
                                {
                                    return (1);
                                }

                                //Decode CPName
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                strTemp = ItemData.ToString().ToUpper();
                                switch (strTemp)
                                {
                                    case "APN":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        FVI_EqpParameter.APN = ItemData.ToString();
                                        blnS2F41_APN = true;
                                        break;

                                    case "DATECODE":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        FVI_EqpParameter.DateCode = ItemData.ToString();
                                        blnS2F41_DateCode = true;
                                        break;

                                    case "LOTID":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        FVI_EqpParameter.LotID = ItemData.ToString();
                                        blnS2F41_Lot6 = true;
                                        break;

                                    default:
                                        iHCACK = 3;       //3 = At least one parameter is invalid
                                        break;
                                }
                            }
                            if (blnS2F41_APN == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                            if (blnS2F41_DateCode == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                            if (blnS2F41_Lot6 == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK3 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                        }
                        else
                        {
                            iHCACK = 3;    //3 = At least one parameter is invalid
                            iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                            iCPACK2 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                            iCPACK3 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                        }
                    }
                    else if (strRCMD == "RECIPE_CHANGE")
                    {
                        //S2F41傳過來的CP Item=數
                        lngCPItemCount = lItemBytes;

                        //Host有傳CP Data過來
                        if (lngCPItemCount != 0)
                        {
                            //Check S2F41傳送過來的CPName與CPVAL是否合法
                            for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                            {
                                //  <L[2]
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                if (lItemBytes != 2)
                                {
                                    return (1);
                                }

                                //Decode CPName
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                strTemp = ItemData.ToString().ToUpper();
                                switch (strTemp)
                                {
                                    case "RECIPE_NAME":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        FVI_EqpParameter.CurrentRecipeName = ItemData.ToString();
                                        blnS2F41_Current_RecipeName = true;
                                        break;

                                    default:
                                        iHCACK = 3;       //3 = At least one parameter is invalid
                                        break;
                                }
                            }
                            if (blnS2F41_Current_RecipeName == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                        }
                    }
                    else if (strRCMD == "EQUIPMENT_REMOTE_CONTROL")
                    {
                        //S2F41傳過來的CP Item=數
                        lngCPItemCount = lItemBytes;

                        //Host有傳CP Data過來
                        if (lngCPItemCount != 0)
                        {
                            //Check S2F41傳送過來的CPName與CPVAL是否合法
                            for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                            {
                                //  <L[2]
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                if (lItemBytes != 2)
                                {
                                    return (1);
                                }

                                //Decode CPName
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                strTemp = ItemData.ToString().ToUpper();
                                switch (strTemp)
                                {
                                    case "EQUIPMENT_REMOTECONTROL_STATUS":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        FVI_EqpParameter.Equipment_RemoteControlStatus = ItemData.ToString();
                                        blnS2F41_EQUIPMENT_REMOTE_CONTROL = true;
                                        DoOnce = false;
                                        break;

                                    default:
                                        iHCACK = 3;       //3 = At least one parameter is invalid
                                        break;
                                }
                            }
                            if (blnS2F41_EQUIPMENT_REMOTE_CONTROL == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                        }
                    }
                    else if (strRCMD == "DIECOUNT_COMPARE_RESULT")
                    {
                        //S2F41傳過來的CP Item=數
                        lngCPItemCount = lItemBytes;

                        //Host有傳CP Data過來
                        if (lngCPItemCount != 0)
                        {
                            //Check S2F41傳送過來的CPName與CPVAL是否合法
                            for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                            {
                                //  <L[2]
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                if (lItemBytes != 2)
                                {
                                    return (1);
                                }

                                //Decode CPName
                                lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                strTemp = ItemData.ToString().ToUpper();
                                switch (strTemp)
                                {
                                    case "EXPECTED_DIECOUNT":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        FVI_EqpParameter.Expected_DieCount = ItemData.ToString();
                                        blnS2F41_EXPECTED_DIECOUNT = true;
                                        break;

                                    case "DIECOUNT_COMPARE_RESULT":
                                        lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                        FVI_EqpParameter.DieCount_CompareResult = ItemData.ToString();
                                        blnS2F41_DIECOUNT_COMPARE_RESULT = true;
                                        break;

                                    default:
                                        iHCACK = 3;       //3 = At least one parameter is invalid
                                        break;
                                }
                            }
                            if (blnS2F41_EXPECTED_DIECOUNT == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                            if (blnS2F41_DIECOUNT_COMPARE_RESULT == false)
                            {
                                iHCACK = 3;     //3 = At least one parameter is invalid
                                iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                            }
                        }
                        else
                        {
                            iHCACK = 3;    //3 = At least one parameter is invalid
                            iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                            iCPACK2 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                        }
                    }
                    else
                    {
                        iHCACK = 1;   //1：Command does not exist
                    }
                }

                //TrimGap
                if (MachineType == "TrimGap")
                {
                    switch (strRCMD)
                    {
                        case "GO-REMOTE":
                            g_lOperationResult = CGWrapper.OnLineRemote();
                            break;

                        case "GO-LOCAL":
                            g_lOperationResult = CGWrapper.OnLineLocal();
                            break;

                        case "ACCESSMODE-ASK":

                            bWaitSECS_ACCESSMODE_ASK = true;

                            //S2F41傳過來的CP Item=數
                            lngCPItemCount = lItemBytes;

                            //Host有傳CP Data過來
                            if (lngCPItemCount != 0)
                            {
                                //Check S2F41傳送過來的CPName與CPVAL是否合法
                                for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                                {
                                    //  <L[2]
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                    if (lItemBytes != 2)
                                    {
                                        return (1);
                                    }

                                    //Decode CPName
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                    strTemp = ItemData.ToString().ToUpper();
                                    switch (strTemp)
                                    {
                                        case "LOADPORT-ID":
                                            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                            LoadPortID = ItemData.ToString();
                                            blnS2F41_LoadPortID = true;
                                            break;

                                        default:
                                            iHCACK = 3;       //3 = At least one parameter is invalid
                                            break;
                                    }
                                }
                                if (blnS2F41_LoadPortID == false)
                                {
                                    iHCACK = 3;     //3 = At least one parameter is invalid
                                    iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                                }
                                else
                                {
                                    AccessModeAskData.Set(LoadPortID);
                                }
                            }
                            break;

                        case "PORT-TRANSFERSTATUS-ASK":

                            bWaitSECS_PORT_TRANSFERSTATUS_ASK = true;

                            //S2F41傳過來的CP Item=數
                            lngCPItemCount = lItemBytes;

                            //Host有傳CP Data過來
                            if (lngCPItemCount != 0)
                            {
                                //Check S2F41傳送過來的CPName與CPVAL是否合法
                                for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                                {
                                    //  <L[2]
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                    if (lItemBytes != 2)
                                    {
                                        return (1);
                                    }

                                    //Decode CPName
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                    strTemp = ItemData.ToString().ToUpper();
                                    switch (strTemp)
                                    {
                                        case "LOADPORT-ID":
                                            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                            LoadPortID = ItemData.ToString();
                                            blnS2F41_LoadPortID = true;
                                            break;

                                        default:
                                            iHCACK = 3;       //3 = At least one parameter is invalid
                                            break;
                                    }
                                }
                                if (blnS2F41_LoadPortID == false)
                                {
                                    iHCACK = 3;     //3 = At least one parameter is invalid
                                    iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                                }
                            }
                            break;

                        case "PP-SELECT":

                            bWaitSECS_PP_SELECT = true;

                            //S2F41傳過來的CP Item=數
                            lngCPItemCount = lItemBytes;

                            //Host有傳CP Data過來
                            if (lngCPItemCount != 0)
                            {
                                //Check S2F41傳送過來的CPName與CPVAL是否合法
                                for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                                {
                                    //  <L[2]
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                    if (lItemBytes != 2)
                                    {
                                        return (1);
                                    }

                                    //Decode CPName
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                    strTemp = ItemData.ToString().ToUpper();
                                    switch (strTemp)
                                    {
                                        case "LOADPORT-ID":
                                            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                            LoadPortID = ItemData.ToString();
                                            blnS2F41_LoadPortID = true;
                                            break;

                                        case "RECIPE-ID":
                                            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                            RecipeID = ItemData.ToString();
                                            blnS2F41_RecipeID = true;
                                            break;

                                        default:
                                            iHCACK = 3;       //3 = At least one parameter is invalid
                                            break;
                                    }
                                }
                                if (blnS2F41_LoadPortID == false)
                                {
                                    iHCACK = 3;     //3 = At least one parameter is invalid
                                    iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                                }
                                if (blnS2F41_RecipeID == false)
                                {
                                    iHCACK = 3;     //3 = At least one parameter is invalid
                                    iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                                }
                                if (blnS2F41_LoadPortID == true && blnS2F41_RecipeID == true)
                                {
                                    ChangeRecipeData.Set(LoadPortID);
                                }
                            }
                            else
                            {
                                iHCACK = 3;    //3 = At least one parameter is invalid
                                iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                                iCPACK2 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                            }

                            break;

                        case "ACCESSMODE-CHANGE":
                            bSECS_ChangeAccessMode_Recive = true;

                            //S2F41傳過來的CP Item=數
                            lngCPItemCount = lItemBytes;

                            //Host有傳CP Data過來
                            if (lngCPItemCount != 0)
                            {
                                //Check S2F41傳送過來的CPName與CPVAL是否合法
                                for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                                {
                                    //  <L[2]
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                    if (lItemBytes != 2)
                                    {
                                        return (1);
                                    }

                                    //Decode CPName
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                    strTemp = ItemData.ToString().ToUpper();
                                    switch (strTemp)
                                    {
                                        case "LOADPORT-ID":
                                            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                            LoadPortID = ItemData.ToString();
                                            blnS2F41_LoadPortID = true;
                                            break;

                                        case "ACCESS-MODE":
                                            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                            AccessMode = ItemData.ToString();
                                            blnS2F41_AccessMode = true;
                                            break;

                                        default:
                                            iHCACK = 3;       //3 = At least one parameter is invalid
                                            break;
                                    }
                                }
                                if (blnS2F41_LoadPortID == false)
                                {
                                    iHCACK = 3;     //3 = At least one parameter is invalid
                                    iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                                }
                                if (blnS2F41_AccessMode == false)
                                {
                                    iHCACK = 3;     //3 = At least one parameter is invalid
                                    iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                                }
                                if (blnS2F41_LoadPortID == true && blnS2F41_AccessMode == true)
                                {
                                    AccessModeChangeData.Set(LoadPortID);
                                }
                            }
                            else
                            {
                                iHCACK = 3;    //3 = At least one parameter is invalid
                                iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                                iCPACK2 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                            }

                            break;

                        case "SLOTMAP-L&C":

                            bWaitSECS_SlotMapCmd = true;

                            //S2F41傳過來的CP Item=數
                            lngCPItemCount = lItemBytes;

                            //Host有傳CP Data過來
                            if (lngCPItemCount != 0)
                            {
                                //Check S2F41傳送過來的CPName與CPVAL是否合法
                                for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                                {
                                    //  <L[2]
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                    if (lItemBytes != 2)
                                    {
                                        return (1);
                                    }

                                    //Decode CPName
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                    strTemp = ItemData.ToString().ToUpper();
                                    switch (strTemp)
                                    {
                                        case "LOADPORT-ID":
                                            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                            LoadPortID = ItemData.ToString();
                                            blnS2F41_LoadPortID = true;
                                            break;

                                        case "CARRIER-ID":
                                            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                            CarrierID = ItemData.ToString();
                                            blnS2F41_CarrierID = true;
                                            break;

                                        default:
                                            iHCACK = 3;       //3 = At least one parameter is invalid
                                            break;
                                    }
                                }
                                if (blnS2F41_LoadPortID == false)
                                {
                                    iHCACK = 3;     //3 = At least one parameter is invalid
                                    iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                                }
                                if (blnS2F41_CarrierID == false)
                                {
                                    iHCACK = 3;     //3 = At least one parameter is invalid
                                    iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                                }
                                if (blnS2F41_LoadPortID == true && blnS2F41_CarrierID == true)
                                {
                                    SlotMapData.Set(LoadPortID, CarrierID);
                                }
                            }
                            else
                            {
                                iHCACK = 3;    //3 = At least one parameter is invalid
                                iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                                iCPACK2 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                            }

                            break;

                        case "SLOTMAP-L":

                            bWaitSECS_SlotMapCmd = true;

                            //S2F41傳過來的CP Item=數
                            lngCPItemCount = lItemBytes;

                            //Host有傳CP Data過來
                            if (lngCPItemCount != 0)
                            {
                                //Check S2F41傳送過來的CPName與CPVAL是否合法
                                for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                                {
                                    //  <L[2]
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                    if (lItemBytes != 2)
                                    {
                                        return (1);
                                    }

                                    //Decode CPName
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                    strTemp = ItemData.ToString().ToUpper();
                                    switch (strTemp)
                                    {
                                        case "LOADPORT-ID":
                                            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                            LoadPortID = ItemData.ToString();
                                            blnS2F41_LoadPortID = true;
                                            break;

                                        default:
                                            iHCACK = 3;       //3 = At least one parameter is invalid
                                            break;
                                    }
                                }
                                if (blnS2F41_LoadPortID == false)
                                {
                                    iHCACK = 3;     //3 = At least one parameter is invalid
                                    iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                                }
                                else
                                {
                                    SlotMapData.Set(LoadPortID);
                                }
                            }
                            break;

                        case "MEASURE-L&C":

                            bWaitSECS_MeasureCmd = true;

                            //S2F41傳過來的CP Item=數
                            lngCPItemCount = lItemBytes;

                            //Host有傳CP Data過來
                            if (lngCPItemCount != 0)
                            {
                                //Check S2F41傳送過來的CPName與CPVAL是否合法
                                for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                                {
                                    //  <L[2]
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                    if (lItemBytes != 2)
                                    {
                                        return (1);
                                    }

                                    //Decode CPName
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                    strTemp = ItemData.ToString().ToUpper();
                                    switch (strTemp)
                                    {
                                        case "LOADPORT-ID":
                                            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                            LoadPortID = ItemData.ToString();
                                            blnS2F41_LoadPortID = true;
                                            break;

                                        case "CARRIER-ID":
                                            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                            CarrierID = ItemData.ToString();
                                            blnS2F41_CarrierID = true;
                                            break;

                                        default:
                                            iHCACK = 3;       //3 = At least one parameter is invalid
                                            break;
                                    }
                                }
                                if (blnS2F41_LoadPortID == false)
                                {
                                    iHCACK = 3;     //3 = At least one parameter is invalid
                                    iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                                }
                                if (blnS2F41_CarrierID == false)
                                {
                                    iHCACK = 3;     //3 = At least one parameter is invalid
                                    iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                                }
                                if (blnS2F41_LoadPortID == true && blnS2F41_CarrierID == true)
                                {
                                    MeasureStartData.Set(LoadPortID, CarrierID);
                                }
                            }
                            else
                            {
                                iHCACK = 3;    //3 = At least one parameter is invalid
                                iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                                iCPACK2 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                            }
                            break;

                        case "MEASURE-L":

                            bWaitSECS_MeasureCmd = true;

                            //S2F41傳過來的CP Item=數
                            lngCPItemCount = lItemBytes;

                            //Host有傳CP Data過來
                            if (lngCPItemCount != 0)
                            {
                                //Check S2F41傳送過來的CPName與CPVAL是否合法
                                for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                                {
                                    //  <L[2]
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                    if (lItemBytes != 2)
                                    {
                                        return (1);
                                    }

                                    //Decode CPName
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                    strTemp = ItemData.ToString().ToUpper();
                                    switch (strTemp)
                                    {
                                        case "LOADPORT-ID":
                                            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                            LoadPortID = ItemData.ToString();
                                            blnS2F41_LoadPortID = true;
                                            break;

                                        default:
                                            iHCACK = 3;       //3 = At least one parameter is invalid
                                            break;
                                    }
                                }
                                if (blnS2F41_LoadPortID == false)
                                {
                                    iHCACK = 3;     //3 = At least one parameter is invalid
                                    iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                                }
                                else
                                {
                                    MeasureStartData.Set(LoadPortID);
                                }
                            }
                            break;

                        case "RELEASE-L&C":

                            bWaitSECS_ReleaseCmd = true;

                            //S2F41傳過來的CP Item=數
                            lngCPItemCount = lItemBytes;

                            //Host有傳CP Data過來
                            if (lngCPItemCount != 0)
                            {
                                //Check S2F41傳送過來的CPName與CPVAL是否合法
                                for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                                {
                                    //  <L[2]
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                    if (lItemBytes != 2)
                                    {
                                        return (1);
                                    }

                                    //Decode CPName
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                    strTemp = ItemData.ToString().ToUpper();
                                    switch (strTemp)
                                    {
                                        case "LOADPORT-ID":
                                            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                            LoadPortID = ItemData.ToString();
                                            blnS2F41_LoadPortID = true;
                                            break;

                                        case "CARRIER-ID":
                                            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                            CarrierID = ItemData.ToString();
                                            blnS2F41_CarrierID = true;
                                            break;

                                        default:
                                            iHCACK = 3;       //3 = At least one parameter is invalid
                                            break;
                                    }
                                }
                                if (blnS2F41_LoadPortID == false)
                                {
                                    iHCACK = 3;     //3 = At least one parameter is invalid
                                    iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                                }
                                if (blnS2F41_CarrierID == false)
                                {
                                    iHCACK = 3;     //3 = At least one parameter is invalid
                                    iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                                }
                                if (blnS2F41_LoadPortID == true && blnS2F41_CarrierID == true)
                                {
                                    ReleaseData.Set(LoadPortID, CarrierID);
                                }
                            }
                            else
                            {
                                iHCACK = 3;    //3 = At least one parameter is invalid
                                iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                                iCPACK2 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                            }
                            break;

                        case "RELEASE-L":

                            bWaitSECS_ReleaseCmd = true;

                            //S2F41傳過來的CP Item=數
                            lngCPItemCount = lItemBytes;

                            //Host有傳CP Data過來
                            if (lngCPItemCount != 0)
                            {
                                //Check S2F41傳送過來的CPName與CPVAL是否合法
                                for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                                {
                                    //  <L[2]
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                    if (lItemBytes != 2)
                                    {
                                        return (1);
                                    }

                                    //Decode CPName
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                    strTemp = ItemData.ToString().ToUpper();
                                    switch (strTemp)
                                    {
                                        case "LOADPORT-ID":
                                            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                            LoadPortID = ItemData.ToString();
                                            blnS2F41_LoadPortID = true;
                                            break;

                                        default:
                                            iHCACK = 3;       //3 = At least one parameter is invalid
                                            break;
                                    }
                                }
                                if (blnS2F41_LoadPortID == false)
                                {
                                    iHCACK = 3;     //3 = At least one parameter is invalid
                                    iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                                }
                                else
                                {
                                    ReleaseData.Set(LoadPortID);
                                }
                            }
                            break;

                        case "CANCEL-L&C":

                            bWaitSECS_CancelCmd = true;

                            //S2F41傳過來的CP Item=數
                            lngCPItemCount = lItemBytes;

                            //Host有傳CP Data過來
                            if (lngCPItemCount != 0)
                            {
                                //Check S2F41傳送過來的CPName與CPVAL是否合法
                                for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                                {
                                    //  <L[2]
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                    if (lItemBytes != 2)
                                    {
                                        return (1);
                                    }

                                    //Decode CPName
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                    strTemp = ItemData.ToString().ToUpper();
                                    switch (strTemp)
                                    {
                                        case "LOADPORT-ID":
                                            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                            LoadPortID = ItemData.ToString();
                                            blnS2F41_LoadPortID = true;
                                            break;

                                        case "CARRIER-ID":
                                            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                            CarrierID = ItemData.ToString();
                                            blnS2F41_CarrierID = true;
                                            break;

                                        default:
                                            iHCACK = 3;       //3 = At least one parameter is invalid
                                            break;
                                    }
                                }
                                if (blnS2F41_LoadPortID == false)
                                {
                                    iHCACK = 3;     //3 = At least one parameter is invalid
                                    iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                                }
                                if (blnS2F41_CarrierID == false)
                                {
                                    iHCACK = 3;     //3 = At least one parameter is invalid
                                    iCPACK2 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                                }
                                if (blnS2F41_LoadPortID == true && blnS2F41_CarrierID == true)
                                {
                                    CancelData.Set(LoadPortID, CarrierID);
                                }
                            }
                            else
                            {
                                iHCACK = 3;    //3 = At least one parameter is invalid
                                iCPACK1 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                                iCPACK2 = 1;    //1 = Parameter Name (CPNAME) does Not exist
                            }
                            break;

                        case "CANCEL-L":

                            bWaitSECS_CancelCmd = true;

                            //S2F41傳過來的CP Item=數
                            lngCPItemCount = lItemBytes;

                            //Host有傳CP Data過來
                            if (lngCPItemCount != 0)
                            {
                                //Check S2F41傳送過來的CPName與CPVAL是否合法
                                for (intIndex = 1; intIndex <= lngCPItemCount; intIndex++)
                                {
                                    //  <L[2]
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.LIST_TYPE, out lItemBytes, ref Value);
                                    if (lItemBytes != 2)
                                    {
                                        return (1);
                                    }

                                    //Decode CPName
                                    lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                    strTemp = ItemData.ToString().ToUpper();
                                    switch (strTemp)
                                    {
                                        case "LOADPORT-ID":
                                            lOffset = CSWrapper.DataItemIn(ref RawData, lOffset, SECSII_DATA_TYPE.ASCII_TYPE, out lItemBytes, ref ItemData);
                                            LoadPortID = ItemData.ToString();
                                            blnS2F41_LoadPortID = true;
                                            break;

                                        default:
                                            iHCACK = 3;       //3 = At least one parameter is invalid
                                            break;
                                    }
                                }
                                if (blnS2F41_LoadPortID == false)
                                {
                                    iHCACK = 3;     //3 = At least one parameter is invalid
                                    iCPACK1 = 1;     //1 = Parameter Name (CPNAME) does Not exist
                                }
                                else
                                {
                                    CancelData.Set(LoadPortID);
                                }
                            }
                            break;

                        case "START":
                            bWaitSECS_StartCmd = true;
                            break;

                        case "STOP":
                            bWaitSECS_StopCmd = true;
                            break;

                        default:
                            break;
                    }
                }
            }
            ExeRCMD_Reply(strRCMD, ulSystemBytes, iHCACK, iCPACK1, iCPACK2, iCPACK3);
            return (0);
        }

        private void ExeRCMD_Reply(string RCMD, int SystemBytes, int iHCACK, int iCPACK1, int iCPACK2, int iCPACK3)
        {
            object OutputRawData = null;
            long[] lTemp = new long[2];
            object varRawData = null;
            int i = 0;
            object Temp = null;
            object Value = null;
            //        S2F42
            //        <L[2]
            //           <B HCACK>
            //           <L[2]
            //               <L[2]
            //                   <A "PPID">
            //                   <B[1] CPACK1>
            //               >
            //               <L[2]
            //                   <A "PORTID">
            //                   <B[1] CPACK2>
            //               >
            //           >
            //        >.

            CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
            //           <B HCACK>
            lTemp[0] = iHCACK;
            varRawData = lTemp;
            CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref varRawData);
            i = 0;
            if (iCPACK1 > 0)
            {
                i = i + 1;
            }
            if (iCPACK2 > 0)
            {
                i = i + 1;
            }
            if (iCPACK3 > 0)
            {
                i = i + 1;
            }
            if (i == 0)
            {
                CSWrapper.DataItemOut(ref OutputRawData, 0, SECSII_DATA_TYPE.LIST_TYPE, ref varRawData);
            }
            else
            {
                CSWrapper.DataItemOut(ref OutputRawData, i, SECSII_DATA_TYPE.LIST_TYPE, ref Value);

                //SMT
                if (MachineType == "SMT")
                {
                    switch (RCMD)
                    {
                        case "MAGAZINE_MOVEIN":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "LOT_ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            if (iCPACK2 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "MAGAZINE_ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK2;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            if (iCPACK3 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "MAGAZINE_MOVEIN_STATUS".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK3;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "BOAT_MOVEIN":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "BOAT_BARCODE".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            if (iCPACK2 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "BOAT_MOVEIN_STATUS".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK2;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;
                        //case "PASSBOAT_MOVEOUT":
                        //    if (iCPACK1 > 0)
                        //    {
                        //        CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                        //        Value = "PASSBOAT_BARCODE".ToString();
                        //        CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                        //        lTemp[0] = iCPACK1;
                        //        Temp = lTemp;
                        //        CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                        //    }
                        //    break;
                        //case "NGBOAT_MOVEOUT":
                        //    if (iCPACK1 > 0)
                        //    {
                        //        CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                        //        Value = "NGBOAT_BARCODE".ToString();
                        //        CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                        //        lTemp[0] = iCPACK1;
                        //        Temp = lTemp;
                        //        CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                        //    }
                        //    break;
                        case "EMPTYBOAT_RELEASE":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "EMPTYBOAT_BARCODE".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            if (iCPACK2 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "EMPTYBOAT_RELEASE_STATUS".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK2;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;
                        //case "PASS1MAGAZINE_MOVEIN":
                        //    if (iCPACK1 > 0)
                        //    {
                        //        CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                        //        Value = "PASS1MAGAZINE_ID".ToString();
                        //        CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                        //        lTemp[0] = iCPACK1;
                        //        Temp = lTemp;
                        //        CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                        //    }
                        //    if (iCPACK2 > 0)
                        //    {
                        //        CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                        //        Value = "PASS1MAGAZINE_MOVEIN_STATUS".ToString();
                        //        CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                        //        lTemp[0] = iCPACK2;
                        //        Temp = lTemp;
                        //        CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                        //    }
                        //    break;
                        //case "PASS2MAGAZINE_MOVEIN":
                        //    if (iCPACK1 > 0)
                        //    {
                        //        CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                        //        Value = "PASS2MAGAZINE_ID".ToString();
                        //        CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                        //        lTemp[0] = iCPACK1;
                        //        Temp = lTemp;
                        //        CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                        //    }
                        //    if (iCPACK2 > 0)
                        //    {
                        //        CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                        //        Value = "PASS2MAGAZINE_MOVEIN_STATUS".ToString();
                        //        CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                        //        lTemp[0] = iCPACK2;
                        //        Temp = lTemp;
                        //        CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                        //    }
                        //    break;
                        //case "NGMAGAZINE_MOVEIN":
                        //    if (iCPACK1 > 0)
                        //    {
                        //        CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                        //        Value = "NGMAGAZINE_ID".ToString();
                        //        CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                        //        lTemp[0] = iCPACK1;
                        //        Temp = lTemp;
                        //        CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                        //    }
                        //    if (iCPACK2 > 0)
                        //    {
                        //        CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                        //        Value = "NGMAGAZINE_MOVEIN_STATUS".ToString();
                        //        CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                        //        lTemp[0] = iCPACK2;
                        //        Temp = lTemp;
                        //        CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                        //    }
                        //    break;
                        //case "EMPTYMAGAZINE_MOVEIN":
                        //    if (iCPACK1 > 0)
                        //    {
                        //        CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                        //        Value = "EMPTYMAGAZINE_ID".ToString();
                        //        CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                        //        lTemp[0] = iCPACK1;
                        //        Temp = lTemp;
                        //        CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                        //    }
                        //    if (iCPACK2 > 0)
                        //    {
                        //        CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                        //        Value = "EMPTYMAGAZINE_MOVEIN_STATUS".ToString();
                        //        CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                        //        lTemp[0] = iCPACK2;
                        //        Temp = lTemp;
                        //        CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                        //    }
                        //    break;
                        case "PASS1MAGAZINE_MOVEOUT":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "PASS1MAGAZINE_ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            if (iCPACK2 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "PASS1MAGAZINE_MOVEOUT_STATUS".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK2;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "PASS2MAGAZINE_MOVEOUT":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "PASS2MAGAZINE_ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            if (iCPACK2 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "PASS2MAGAZINE_MOVEOUT_STATUS".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK2;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "NGMAGAZINE_MOVEOUT":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "NGMAGAZINE_ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            if (iCPACK2 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "NGMAGAZINE_MOVEOUT_STATUS".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK2;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "BOAT_ASSING_NGBOAT":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "BOAT_ASSING_NGBOAT_BARCODE".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            if (iCPACK2 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "BOAT_ASSING_NGBOAT_STATUS".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK2;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "UI_LOCK_COMMAND":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "UI_LOCK_STATE".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "EQUIPMENT_OPERATIONAL_COMMAND":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "EQUIPMENT_OPERATIONAL_STATE".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "EQUIPMENT_ENDLOT_COMMAND":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "EQUIPMENT_ENDLOT".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "EQUIPMENT_STARTRUN":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "EQUIPMENT_STARTRUN_STATUS".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        default:
                            break;   //Error ...
                    }
                }

                //FVI
                if (MachineType == "FVI")
                {
                    switch (RCMD)
                    {
                        case "REELID_VERIFY_RESULT":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "APN".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            if (iCPACK2 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "LOTID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK2;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            if (iCPACK3 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "DATECODE".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK3;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "RECIPE_CHANGE":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "RECIPE_NAME".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "EQUIPMENT_REMOTE_CONTROL":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "EQUIPMENT_REMOTECONTROL_STATUS".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "DIECOUNT_COMPARE_RESULT":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "EXPECTED_DIECOUNT".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            if (iCPACK2 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "DIECOUNT_COMPARE_RESULT".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK2;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        default:
                            break;   //Error ...
                    }
                }

                if (MachineType == "TrimGap")
                {
                    switch (RCMD)
                    {
                        case "ACCESSMODE-ASK":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "LOADPORT-ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "PORT-TRANSFERSTATUS-ASK":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "LOADPORT-ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "PP-SELECT":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "LOADPORT-ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            if (iCPACK2 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "RECIPE-ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK2;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "ACCESSMODE-CHANGE":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "LOADPORT-ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            if (iCPACK2 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "ACCESS-MODE".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK2;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "SLOT-L&C":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "LOADPORT-ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            if (iCPACK2 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "CARRIER-ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK2;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "SLOTMAP-L":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "LOADPORT-ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "MEASURE-L&C":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "LOADPORT-ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            if (iCPACK2 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "CARRIER-ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK2;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "MEASURE-L":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "LOADPORT-ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "RELEASE-L&C":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "LOADPORT-ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            if (iCPACK2 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "CARRIER-ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK2;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "RELEASE-L":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "LOADPORT-ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "CANCEL-L&C":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "LOADPORT-ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            if (iCPACK2 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "CARRIER-ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK2;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        case "CANCEL-L":
                            if (iCPACK1 > 0)
                            {
                                CSWrapper.DataItemOut(ref OutputRawData, 2, SECSII_DATA_TYPE.LIST_TYPE, ref Value);
                                Value = "LOADPORT-ID".ToString();
                                CSWrapper.DataItemOut(ref OutputRawData, Value.ToString().Length, SECSII_DATA_TYPE.ASCII_TYPE, ref Value);
                                lTemp[0] = iCPACK1;
                                Temp = lTemp;
                                CSWrapper.DataItemOut(ref OutputRawData, 1, SECSII_DATA_TYPE.BINARY_TYPE, ref Temp);
                            }
                            break;

                        default:
                            break;   //Error ...
                    }
                }
            }

            CSWrapper.SendSECSIIMessage(2, 42, 0, ref SystemBytes, OutputRawData);
            txHCACK.Text = iHCACK.ToString();
            txCPACK1.Text = iCPACK1.ToString();
            txCPACK2.Text = iCPACK2.ToString();
            if (iHCACK == 0)
            {
                txHCACK.BackColor = Color.YellowGreen;
            }
            else
            {
                txHCACK.BackColor = Color.Red;
            }
        }

        private void ExeSendS9F7(object Head)
        {
            object OutputRawData = null;
            CSWrapper.DataItemOut(ref OutputRawData, 10, SECSII_DATA_TYPE.BINARY_TYPE, ref Head);
            CSWrapper.SendSECSIIMessage(9, 7, 0, ref g_SystemBytes, OutputRawData);
        }

        private void UpdateSV_Click(object sender, EventArgs e)
        {
            int iValue;
            string strVal;
            float rValue;
            object objTemp;
            bool boolValue;
            byte bValue;

            int SVID_int = Convert.ToInt32(SVID.Text);

            if (MachineType == "SMT")
            {
                switch (SVID_int)
                {
                    //SVID
                    case SMT_EqpID.MagazineIDCheck:
                        SMT_EqpParameter.MagazineIDCheck = SVValue.Text;
                        break;

                    case SMT_EqpID.BoatBarcodeCheck:
                        SMT_EqpParameter.BoatBarcodeCheck = SVValue.Text;
                        break;

                    case SMT_EqpID.Pass1MagazineIDCheck:
                        SMT_EqpParameter.Pass1MagazineIDCheck = SVValue.Text;
                        break;

                    case SMT_EqpID.Pass2MagazineIDCheck:
                        SMT_EqpParameter.Pass2MagazineIDCheck = SVValue.Text;
                        break;

                    case SMT_EqpID.NGMagazineIDCheck:
                        SMT_EqpParameter.NGMagazineIDCheck = SVValue.Text;
                        break;

                    case SMT_EqpID.EmptyMagazineIDCheck:
                        SMT_EqpParameter.EmptyMagazineIDCheck = SVValue.Text;
                        break;

                    case SMT_EqpID.BoatAssingNGBoatBarcodeCheck:
                        SMT_EqpParameter.BoatAssingNGBoatBarcodeCheck = SVValue.Text;
                        break;

                    case SMT_EqpID.UILockCommand:
                        SMT_EqpParameter.UILockCommand = SVValue.Text;
                        break;

                    case SMT_EqpID.EquipmentOperationalCommand:
                        SMT_EqpParameter.EquipmentOperationalCommand = SVValue.Text;
                        break;

                    case SMT_EqpID.EquipmentEndLotCommand:
                        SMT_EqpParameter.EquipmentEndLotCommand = SVValue.Text;
                        break;

                    case SMT_EqpID.RemoteActionResult:
                        SMT_EqpParameter.RemoteActionResult = SVValue.Text;
                        break;

                    case SMT_EqpID.EmptyBoatReleaseCheck:
                        SMT_EqpParameter.EmptyBoatReleaseCheck = SVValue.Text;
                        break;

                    case SMT_EqpID.EquipmentStartRunCheck:
                        SMT_EqpParameter.EquipmentStartRunCheck = SVValue.Text;
                        break;

                    //DVID
                    case SMT_EqpID.LotID:
                        SMT_EqpParameter.LotID = SVValue.Text;
                        break;

                    case SMT_EqpID.MagazineID:
                        SMT_EqpParameter.MagazineID = SVValue.Text;
                        break;

                    case SMT_EqpID.Pass1MagazineID:
                        SMT_EqpParameter.Pass1MagazineID = SVValue.Text;
                        break;

                    case SMT_EqpID.Pass2MagazineID:
                        SMT_EqpParameter.Pass2MagazineID = SVValue.Text;
                        break;

                    case SMT_EqpID.NGMagazineID:
                        SMT_EqpParameter.NGMagazineID = SVValue.Text;
                        break;

                    case SMT_EqpID.EmptyMagazineID:
                        SMT_EqpParameter.EmptyMagazineID = SVValue.Text;
                        break;

                    case SMT_EqpID.BoatBarcode:
                        SMT_EqpParameter.BoatBarcode = SVValue.Text;
                        break;

                    case SMT_EqpID.PassBoatBarcode:
                        SMT_EqpParameter.PassBoatBarcode = SVValue.Text;
                        break;

                    case SMT_EqpID.NGBoatBarcode:
                        SMT_EqpParameter.NGBoatBarcode = SVValue.Text;
                        break;

                    case SMT_EqpID.EmptyBoatBarcode:
                        SMT_EqpParameter.EmptyBoatBarcode = SVValue.Text;
                        break;

                    case SMT_EqpID.BoatAssingNGBoatBarcode:
                        SMT_EqpParameter.BoatAssingNGBoatBarcode = SVValue.Text;
                        break;

                    case SMT_EqpID.RecipeName:
                        SMT_EqpParameter.RecipeName = SVValue.Text;
                        break;

                    case SMT_EqpID.Pass1MagazineUnitCount:
                        SMT_EqpParameter.Pass1MagazineUnitCount = SVValue.Text;
                        break;

                    case SMT_EqpID.Pass2MagazineUnitCount:
                        SMT_EqpParameter.Pass2MagazineUnitCount = SVValue.Text;
                        break;

                    case SMT_EqpID.NGMagazineUnitCount:
                        SMT_EqpParameter.NGMagazineUnitCount = SVValue.Text;
                        break;

                    default:
                        AppendText(" ※ID doesn't exist!!" + Environment.NewLine);
                        return;
                }
                AppendText(" Update SV,DV " + Environment.NewLine);
            }

            if (MachineType == "FVI")
            {
                switch (SVID_int)
                {
                    //SVID
                    case FVI_EqpID.Equipment_RemoteControlStatus:
                        FVI_EqpParameter.Equipment_RemoteControlStatus = SVValue.Text;
                        break;

                    case FVI_EqpID.Equipment_RemoteControlResult:
                        FVI_EqpParameter.Equipment_RemoteControlResult = SVValue.Text;
                        break;

                    case FVI_EqpID.Equipment_LocalControlStatus:
                        FVI_EqpParameter.Equipment_LocalControlStatus = SVValue.Text;
                        break;

                    case FVI_EqpID.Equipment_LocalControlResult:
                        FVI_EqpParameter.Equipment_LocalControlResult = SVValue.Text;
                        break;

                    case FVI_EqpID.Reel_Status:
                        FVI_EqpParameter.Reel_Status = SVValue.Text;
                        break;

                    case FVI_EqpID.DieCount_CompareResult:
                        FVI_EqpParameter.DieCount_CompareResult = SVValue.Text;
                        break;

                    //DVID
                    case FVI_EqpID.ReelID:
                        FVI_EqpParameter.ReelID = SVValue.Text;
                        break;

                    case FVI_EqpID.APN:
                        FVI_EqpParameter.APN = SVValue.Text;
                        break;

                    case FVI_EqpID.DateCode:
                        FVI_EqpParameter.DateCode = SVValue.Text;
                        break;

                    case FVI_EqpID.LotID:
                        FVI_EqpParameter.LotID = SVValue.Text;
                        break;

                    case FVI_EqpID.CurrentRecipeName:
                        FVI_EqpParameter.CurrentRecipeName = SVValue.Text;
                        break;

                    case FVI_EqpID.Expected_DieCount:
                        FVI_EqpParameter.Expected_DieCount = SVValue.Text;
                        break;

                    case FVI_EqpID.Actual_DieCount:
                        FVI_EqpParameter.Actual_DieCount = SVValue.Text;
                        break;

                    default:
                        AppendText(" ※ID doesn't exist!!" + Environment.NewLine);
                        return;
                }
                AppendText(" Update SV,DV " + Environment.NewLine);
            }

            if (MachineType == "TrimGap")
            {
                switch (SVID_int)
                {
                    //SVID

                    //DVID
                    case TrimGap_EqpID.EquipmentStatus:
                        TrimGap_EqpParameter.EquipmentStatus = SVValue.Text;
                        break;

                    case TrimGap_EqpID.RecipeID:
                        TrimGap_EqpParameter.RecipeID = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Loadport1_RecipeID:
                        TrimGap_EqpParameter.Loadport1_RecipeID = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Loadport2_RecipeID:
                        TrimGap_EqpParameter.Loadport2_RecipeID = SVValue.Text;
                        break;

                    case TrimGap_EqpID.CarrierID:
                        TrimGap_EqpParameter.CarrierID = SVValue.Text;
                        break;

                    case TrimGap_EqpID.PortID:
                        TrimGap_EqpParameter.PortID = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.AccessMode:
                        TrimGap_EqpParameter.AccessMode = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMapStatus:
                        TrimGap_EqpParameter.SlotMapStatus = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.PortTransferState:
                        TrimGap_EqpParameter.PortTransferState = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.Loadport1_AccessMode:
                        TrimGap_EqpParameter.Loadport1_AccessMode = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.Loadport2_AccessMode:
                        TrimGap_EqpParameter.Loadport2_AccessMode = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.Loadport1_PortTransferState:
                        TrimGap_EqpParameter.Loadport1_PortTransferState = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.Loadport2_PortTransferState:
                        TrimGap_EqpParameter.Loadport2_PortTransferState = Convert.ToInt32(SVValue.Text);
                        break;

                    //case TrimGap_EqpID.SlotMap_List:
                    //    TrimGap_EqpParameter.SlotMap_List = Convert.ToInt32(SVValue.Text);
                    //    break;

                    case TrimGap_EqpID.Reason:
                        TrimGap_EqpParameter.Reason = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_1:
                        TrimGap_EqpParameter.SlotMap_1 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_2:
                        TrimGap_EqpParameter.SlotMap_2 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_3:
                        TrimGap_EqpParameter.SlotMap_3 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_4:
                        TrimGap_EqpParameter.SlotMap_4 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_5:
                        TrimGap_EqpParameter.SlotMap_5 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_6:
                        TrimGap_EqpParameter.SlotMap_6 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_7:
                        TrimGap_EqpParameter.SlotMap_7 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_8:
                        TrimGap_EqpParameter.SlotMap_8 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_9:
                        TrimGap_EqpParameter.SlotMap_9 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_10:
                        TrimGap_EqpParameter.SlotMap_10 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_11:
                        TrimGap_EqpParameter.SlotMap_11 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_12:
                        TrimGap_EqpParameter.SlotMap_12 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_13:
                        TrimGap_EqpParameter.SlotMap_13 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_14:
                        TrimGap_EqpParameter.SlotMap_14 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_15:
                        TrimGap_EqpParameter.SlotMap_15 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_16:
                        TrimGap_EqpParameter.SlotMap_16 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_17:
                        TrimGap_EqpParameter.SlotMap_17 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_18:
                        TrimGap_EqpParameter.SlotMap_18 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_19:
                        TrimGap_EqpParameter.SlotMap_19 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_20:
                        TrimGap_EqpParameter.SlotMap_20 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_21:
                        TrimGap_EqpParameter.SlotMap_21 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_22:
                        TrimGap_EqpParameter.SlotMap_22 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_23:
                        TrimGap_EqpParameter.SlotMap_23 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_24:
                        TrimGap_EqpParameter.SlotMap_24 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.SlotMap_25:
                        TrimGap_EqpParameter.SlotMap_25 = Convert.ToInt32(SVValue.Text);
                        break;

                    case TrimGap_EqpID.Slot1_Info:
                        TrimGap_EqpParameter.Slot1_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot2_Info:
                        TrimGap_EqpParameter.Slot2_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot3_Info:
                        TrimGap_EqpParameter.Slot3_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot4_Info:
                        TrimGap_EqpParameter.Slot4_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot5_Info:
                        TrimGap_EqpParameter.Slot5_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot6_Info:
                        TrimGap_EqpParameter.Slot6_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot7_Info:
                        TrimGap_EqpParameter.Slot7_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot8_Info:
                        TrimGap_EqpParameter.Slot8_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot9_Info:
                        TrimGap_EqpParameter.Slot9_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot10_Info:
                        TrimGap_EqpParameter.Slot10_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot11_Info:
                        TrimGap_EqpParameter.Slot11_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot12_Info:
                        TrimGap_EqpParameter.Slot12_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot13_Info:
                        TrimGap_EqpParameter.Slot13_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot14_Info:
                        TrimGap_EqpParameter.Slot14_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot15_Info:
                        TrimGap_EqpParameter.Slot15_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot16_Info:
                        TrimGap_EqpParameter.Slot16_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot17_Info:
                        TrimGap_EqpParameter.Slot17_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot18_Info:
                        TrimGap_EqpParameter.Slot18_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot19_Info:
                        TrimGap_EqpParameter.Slot19_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot20_Info:
                        TrimGap_EqpParameter.Slot20_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot21_Info:
                        TrimGap_EqpParameter.Slot21_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot22_Info:
                        TrimGap_EqpParameter.Slot22_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot23_Info:
                        TrimGap_EqpParameter.Slot23_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot24_Info:
                        TrimGap_EqpParameter.Slot24_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot25_Info:
                        TrimGap_EqpParameter.Slot25_Info = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot1_Max:
                        TrimGap_EqpParameter.Slot1_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot2_Max:
                        TrimGap_EqpParameter.Slot2_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot3_Max:
                        TrimGap_EqpParameter.Slot3_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot4_Max:
                        TrimGap_EqpParameter.Slot4_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot5_Max:
                        TrimGap_EqpParameter.Slot5_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot6_Max:
                        TrimGap_EqpParameter.Slot6_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot7_Max:
                        TrimGap_EqpParameter.Slot7_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot8_Max:
                        TrimGap_EqpParameter.Slot8_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot9_Max:
                        TrimGap_EqpParameter.Slot9_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot10_Max:
                        TrimGap_EqpParameter.Slot10_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot11_Max:
                        TrimGap_EqpParameter.Slot11_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot12_Max:
                        TrimGap_EqpParameter.Slot12_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot13_Max:
                        TrimGap_EqpParameter.Slot13_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot14_Max:
                        TrimGap_EqpParameter.Slot14_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot15_Max:
                        TrimGap_EqpParameter.Slot15_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot16_Max:
                        TrimGap_EqpParameter.Slot16_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot17_Max:
                        TrimGap_EqpParameter.Slot17_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot18_Max:
                        TrimGap_EqpParameter.Slot18_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot19_Max:
                        TrimGap_EqpParameter.Slot19_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot20_Max:
                        TrimGap_EqpParameter.Slot20_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot21_Max:
                        TrimGap_EqpParameter.Slot21_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot22_Max:
                        TrimGap_EqpParameter.Slot22_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot23_Max:
                        TrimGap_EqpParameter.Slot23_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot24_Max:
                        TrimGap_EqpParameter.Slot24_Max = SVValue.Text;
                        break;

                    case TrimGap_EqpID.Slot25_Max:
                        TrimGap_EqpParameter.Slot25_Max = SVValue.Text;
                        break;

                    default:
                        AppendText(" ※ID doesn't exist!!" + Environment.NewLine);
                        return;
                }
                AppendText(" Update SV,DV " + Environment.NewLine);
            }
        }

        private void Config_Click(object sender, EventArgs e)
        {
            if (DisableComm.Enabled)
            {
                DialogResult Result = MessageBox.Show("Opening this form will disconnect the connection.Are you sure to open it ? ", "", MessageBoxButtons.YesNo);

                if (Result == DialogResult.Yes)
                {
                    ExeDisableComm();
                    config.ShowDialog();
                }
            }
            else
            {
                config.ShowDialog();
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}