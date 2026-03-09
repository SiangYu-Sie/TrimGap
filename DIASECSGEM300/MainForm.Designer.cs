namespace DemoFormDiaGemLib
{
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClearDriverLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClearGemLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClearSECSLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiClearEQPLog = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDriverParameter = new System.Windows.Forms.ToolStripMenuItem();
            this.離開ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tbcLog = new System.Windows.Forms.TabControl();
            this.tbpDriverLog = new System.Windows.Forms.TabPage();
            this.rtbDriverLog = new System.Windows.Forms.RichTextBox();
            this.tbpGemLog = new System.Windows.Forms.TabPage();
            this.rtbGemLog = new System.Windows.Forms.RichTextBox();
            this.tbpSecsLog = new System.Windows.Forms.TabPage();
            this.rtbSecsLog = new System.Windows.Forms.RichTextBox();
            this.tbpEQPLog = new System.Windows.Forms.TabPage();
            this.rtbEqpLog = new System.Windows.Forms.RichTextBox();
            this.tbcFunctions = new System.Windows.Forms.TabControl();
            this.tbpSystemID = new System.Windows.Forms.TabPage();
            this.tbcSystemID = new System.Windows.Forms.TabControl();
            this.tbpSysSV = new System.Windows.Forms.TabPage();
            this.groupBox43 = new System.Windows.Forms.GroupBox();
            this.lblSysSV_PRJobSpace = new System.Windows.Forms.Label();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.lblSysSV_SpoolFullTime = new System.Windows.Forms.Label();
            this.lblSysSV_SpoolStartTime = new System.Windows.Forms.Label();
            this.lblSysSV_SpoolCountTotal = new System.Windows.Forms.Label();
            this.lblSysSV_SpoolCountActual = new System.Windows.Forms.Label();
            this.lblSysSV_SpoolUnloadSubState = new System.Windows.Forms.Label();
            this.lblSysSV_SpoolLoadSubState = new System.Windows.Forms.Label();
            this.lblSysSV_SpoolState = new System.Windows.Forms.Label();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.lblSysSV_Clock = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.lblSysSV_ControlState = new System.Windows.Forms.Label();
            this.tbpSysEC = new System.Windows.Forms.TabPage();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.rdoLog_Error = new System.Windows.Forms.RadioButton();
            this.rdoLog_Warn = new System.Windows.Forms.RadioButton();
            this.rdoLog_Info = new System.Windows.Forms.RadioButton();
            this.rdoLog_Debug = new System.Windows.Forms.RadioButton();
            this.rdoLog_Trace = new System.Windows.Forms.RadioButton();
            this.groupBox42 = new System.Windows.Forms.GroupBox();
            this.lblSysEC_PRMaxJobSpace = new System.Windows.Forms.Label();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.lblSysEC_MaxSpoolTransmit = new System.Windows.Forms.Label();
            this.lblSysEC_OverWriteSpool = new System.Windows.Forms.Label();
            this.lblSysEC_ConfigSpool = new System.Windows.Forms.Label();
            this.tbpEvent = new System.Windows.Forms.TabPage();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.rdoProcessState_Down = new System.Windows.Forms.RadioButton();
            this.rdoProcessState_Pause = new System.Windows.Forms.RadioButton();
            this.rdoProcessState_Stop = new System.Windows.Forms.RadioButton();
            this.rdoProcessState_Run = new System.Windows.Forms.RadioButton();
            this.rdoProcessState_Idle = new System.Windows.Forms.RadioButton();
            this.rdoProcessState_Initial = new System.Windows.Forms.RadioButton();
            this.flpEventResult = new System.Windows.Forms.FlowLayoutPanel();
            this.lblEventResultCaption = new System.Windows.Forms.Label();
            this.lblEventResultText = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblEventID = new System.Windows.Forms.Label();
            this.btnEventReport = new System.Windows.Forms.Button();
            this.txtEventID = new System.Windows.Forms.TextBox();
            this.tbpAlarm = new System.Windows.Forms.TabPage();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.txtEqpCurrentAlarmSet = new System.Windows.Forms.TextBox();
            this.flpAlarmResult = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAlarmResultCaption = new System.Windows.Forms.Label();
            this.lblAlarmResultText = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnAlarmReportSend = new System.Windows.Forms.Button();
            this.txtAlarmID = new System.Windows.Forms.TextBox();
            this.lblAlarmID = new System.Windows.Forms.Label();
            this.rdoAlarmClean = new System.Windows.Forms.RadioButton();
            this.lblAlarmState = new System.Windows.Forms.Label();
            this.rdoAlarmSet = new System.Windows.Forms.RadioButton();
            this.tbpSV = new System.Windows.Forms.TabPage();
            this.chkContinueUpdateSV = new System.Windows.Forms.CheckBox();
            this.btnUpdateListTypeSV = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnGetSV = new System.Windows.Forms.Button();
            this.txtGetSV = new System.Windows.Forms.TextBox();
            this.lblGetSV = new System.Windows.Forms.Label();
            this.txtGetSVFormat = new System.Windows.Forms.TextBox();
            this.lblGetSVFormat = new System.Windows.Forms.Label();
            this.txtGetSVID = new System.Windows.Forms.TextBox();
            this.lblGetSVID = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtUpdateSV = new System.Windows.Forms.TextBox();
            this.lblUpdateSV = new System.Windows.Forms.Label();
            this.lblUpdateSVFormat = new System.Windows.Forms.Label();
            this.cmbUpdateSVFormat = new System.Windows.Forms.ComboBox();
            this.txtUpdateSVID = new System.Windows.Forms.TextBox();
            this.lblUpdateSVID = new System.Windows.Forms.Label();
            this.btnUpdateSV = new System.Windows.Forms.Button();
            this.flpSVResult = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSVResultCaption = new System.Windows.Forms.Label();
            this.lblSVResultText = new System.Windows.Forms.Label();
            this.tbpEC = new System.Windows.Forms.TabPage();
            this.groupBox24 = new System.Windows.Forms.GroupBox();
            this.btnNewECsReply = new System.Windows.Forms.Button();
            this.txtEAC = new System.Windows.Forms.TextBox();
            this.lblEAC = new System.Windows.Forms.Label();
            this.txtNewECsSystemBytes = new System.Windows.Forms.TextBox();
            this.lblNewECsSystemBytes = new System.Windows.Forms.Label();
            this.dgvNewECs = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Format = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnGetEC = new System.Windows.Forms.Button();
            this.txtGetEC = new System.Windows.Forms.TextBox();
            this.lblGetEC = new System.Windows.Forms.Label();
            this.txtGetECFormat = new System.Windows.Forms.TextBox();
            this.lblGetECFormat = new System.Windows.Forms.Label();
            this.txtGetECID = new System.Windows.Forms.TextBox();
            this.lblGetECID = new System.Windows.Forms.Label();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.txtUpdateEC = new System.Windows.Forms.TextBox();
            this.lblUpdateEC = new System.Windows.Forms.Label();
            this.lblUpdateECFormat = new System.Windows.Forms.Label();
            this.cmbUpdateECFormat = new System.Windows.Forms.ComboBox();
            this.txtUpdateECID = new System.Windows.Forms.TextBox();
            this.lblUpdateECID = new System.Windows.Forms.Label();
            this.btnUpdateEC = new System.Windows.Forms.Button();
            this.flpECResult = new System.Windows.Forms.FlowLayoutPanel();
            this.lblECResultCaption = new System.Windows.Forms.Label();
            this.lblECResultText = new System.Windows.Forms.Label();
            this.tbpRemote = new System.Windows.Forms.TabPage();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.lblReplyHcAck = new System.Windows.Forms.Label();
            this.lblCPs = new System.Windows.Forms.Label();
            this.dgvCPs = new System.Windows.Forms.DataGridView();
            this.lblSystemBytes = new System.Windows.Forms.Label();
            this.lblRCMD = new System.Windows.Forms.Label();
            this.txtReplyHcAck = new System.Windows.Forms.TextBox();
            this.txtSystemBytes = new System.Windows.Forms.TextBox();
            this.txtRCMD = new System.Windows.Forms.TextBox();
            this.txtMessageName = new System.Windows.Forms.TextBox();
            this.txtOBJSPEC = new System.Windows.Forms.TextBox();
            this.lblMessageName = new System.Windows.Forms.Label();
            this.lblOBJSPEC = new System.Windows.Forms.Label();
            this.btnReplyMessage = new System.Windows.Forms.Button();
            this.tabEnhanced = new System.Windows.Forms.TabPage();
            this.groupBox28 = new System.Windows.Forms.GroupBox();
            this.txtEnhancedRemoteDataView = new System.Windows.Forms.TextBox();
            this.tabPP = new System.Windows.Forms.TabPage();
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.lblS7Systembytes = new System.Windows.Forms.Label();
            this.txtS7Systembytes = new System.Windows.Forms.TextBox();
            this.lblAckc7 = new System.Windows.Forms.Label();
            this.txtReceivedPPID = new System.Windows.Forms.TextBox();
            this.txtAckc7 = new System.Windows.Forms.TextBox();
            this.lblReceivedPPID = new System.Windows.Forms.Label();
            this.lvPPIDs = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtPPBody_Unformatted = new System.Windows.Forms.TextBox();
            this.txtPPBodyPPID = new System.Windows.Forms.TextBox();
            this.lblPPBodyPPID = new System.Windows.Forms.Label();
            this.groupBox26 = new System.Windows.Forms.GroupBox();
            this.rdoPPBody_Formatted = new System.Windows.Forms.RadioButton();
            this.rdoPPBody_Unformatted = new System.Windows.Forms.RadioButton();
            this.rdoPPBody_Both = new System.Windows.Forms.RadioButton();
            this.btnSendS7F25 = new System.Windows.Forms.Button();
            this.btnSendS7F23 = new System.Windows.Forms.Button();
            this.btnSendS7F5 = new System.Windows.Forms.Button();
            this.btnSendS7F3 = new System.Windows.Forms.Button();
            this.btnPPChanged = new System.Windows.Forms.Button();
            this.cmbChangedPPType = new System.Windows.Forms.ComboBox();
            this.groupBox25 = new System.Windows.Forms.GroupBox();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tbPPSetting = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLJStdSurface = new System.Windows.Forms.TextBox();
            this.lblOffsetType = new System.Windows.Forms.Label();
            this.txtOffsetType = new System.Windows.Forms.TextBox();
            this.lblRepeatTimes = new System.Windows.Forms.Label();
            this.txtRepeat = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtRotateCount = new System.Windows.Forms.TextBox();
            this.lblRotateCount = new System.Windows.Forms.Label();
            this.tbPPSlotMap = new System.Windows.Forms.TabPage();
            this.cLBPP = new System.Windows.Forms.CheckedListBox();
            this.tbAngle = new System.Windows.Forms.TabPage();
            this.txtAngle4 = new System.Windows.Forms.TextBox();
            this.txtAngle3 = new System.Windows.Forms.TextBox();
            this.txtAngle2 = new System.Windows.Forms.TextBox();
            this.txtAngle1 = new System.Windows.Forms.TextBox();
            this.txtAngle8 = new System.Windows.Forms.TextBox();
            this.txtAngle7 = new System.Windows.Forms.TextBox();
            this.txtAngle6 = new System.Windows.Forms.TextBox();
            this.txtAngle5 = new System.Windows.Forms.TextBox();
            this.lblAngle8 = new System.Windows.Forms.Label();
            this.lblAngle7 = new System.Windows.Forms.Label();
            this.lblAngle6 = new System.Windows.Forms.Label();
            this.lblAngle5 = new System.Windows.Forms.Label();
            this.lblAngle4 = new System.Windows.Forms.Label();
            this.lblAngle3 = new System.Windows.Forms.Label();
            this.lblAngle2 = new System.Windows.Forms.Label();
            this.lblAngle1 = new System.Windows.Forms.Label();
            this.tbPPCofficient = new System.Windows.Forms.TabPage();
            this.txtS2_2x1 = new System.Windows.Forms.TextBox();
            this.txtS2_2x0 = new System.Windows.Forms.TextBox();
            this.txtS2_1x1 = new System.Windows.Forms.TextBox();
            this.txtS2_1x0 = new System.Windows.Forms.TextBox();
            this.txtBTTH = new System.Windows.Forms.TextBox();
            this.txtRange2 = new System.Windows.Forms.TextBox();
            this.txtRange1 = new System.Windows.Forms.TextBox();
            this.txtS1_1x1 = new System.Windows.Forms.TextBox();
            this.txtS1_1x0 = new System.Windows.Forms.TextBox();
            this.lblRange2 = new System.Windows.Forms.Label();
            this.lblRange1 = new System.Windows.Forms.Label();
            this.lblS1_1x1 = new System.Windows.Forms.Label();
            this.lblS1_1x0 = new System.Windows.Forms.Label();
            this.lblS2_2x1 = new System.Windows.Forms.Label();
            this.lblS2_2x0 = new System.Windows.Forms.Label();
            this.lblS2_1x1 = new System.Windows.Forms.Label();
            this.lblS2_1x0 = new System.Windows.Forms.Label();
            this.lblBTTH = new System.Windows.Forms.Label();
            this.tbMisc = new System.Windows.Forms.TabPage();
            this.lblReviseTime = new System.Windows.Forms.Label();
            this.txtReviseTime = new System.Windows.Forms.TextBox();
            this.lblRepeatTimes_now = new System.Windows.Forms.Label();
            this.txtRepeatTimes_now = new System.Windows.Forms.TextBox();
            this.txtCreateTime = new System.Windows.Forms.TextBox();
            this.lblCreateTime = new System.Windows.Forms.Label();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.txtPPID = new System.Windows.Forms.TextBox();
            this.lblPPID = new System.Windows.Forms.Label();
            this.lblPPIDList = new System.Windows.Forms.Label();
            this.tabClock = new System.Windows.Forms.TabPage();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.btnEQPDateAndTimeRequest = new System.Windows.Forms.Button();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.txtClockMessageName = new System.Windows.Forms.TextBox();
            this.txtHostDownloadDateTime = new System.Windows.Forms.TextBox();
            this.txtSetSystemDateTimeSuccess = new System.Windows.Forms.TextBox();
            this.lblClockMessageName = new System.Windows.Forms.Label();
            this.lblHostDownloadDateTime = new System.Windows.Forms.Label();
            this.lblSetSystemDateTimeSuccess = new System.Windows.Forms.Label();
            this.tabEQPTerminalServices = new System.Windows.Forms.TabPage();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.rdoMultiblockNotAllowedFalse = new System.Windows.Forms.RadioButton();
            this.lblMultiblockNotAllowed = new System.Windows.Forms.Label();
            this.rdoMultiblockNotAllowedTrue = new System.Windows.Forms.RadioButton();
            this.btnTerminalACKC10 = new System.Windows.Forms.Button();
            this.lblTerminalACKC10 = new System.Windows.Forms.Label();
            this.txtTerminalACKC10 = new System.Windows.Forms.TextBox();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.lblTerminalText = new System.Windows.Forms.Label();
            this.btnMessageRecognition = new System.Windows.Forms.Button();
            this.txtTerminalDisplayType = new System.Windows.Forms.TextBox();
            this.txtTerminalSystemBytes = new System.Windows.Forms.TextBox();
            this.txtTerminalNumber = new System.Windows.Forms.TextBox();
            this.txtTerminalText = new System.Windows.Forms.TextBox();
            this.lblTerminalDisplayType = new System.Windows.Forms.Label();
            this.lblTerminalNumber = new System.Windows.Forms.Label();
            this.lblTerminalSystemBytes = new System.Windows.Forms.Label();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.lblText = new System.Windows.Forms.Label();
            this.lblTID = new System.Windows.Forms.Label();
            this.btnTerminalRequest = new System.Windows.Forms.Button();
            this.txtText = new System.Windows.Forms.TextBox();
            this.txtTID = new System.Windows.Forms.TextBox();
            this.tabHostSetsCONTROLStateModel = new System.Windows.Forms.TabPage();
            this.groupBox29 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSetsCONTROLStateModelReplyAck = new System.Windows.Forms.TextBox();
            this.txtSetsCONTROLStateModelSystemBytes = new System.Windows.Forms.TextBox();
            this.txtSetsCONTROLStateModel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSetsCONTROLStateModelReplyMsg = new System.Windows.Forms.Button();
            this.tabObject = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox44 = new System.Windows.Forms.GroupBox();
            this.label43 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.btnCreateObject = new System.Windows.Forms.Button();
            this.txtCreateObjID = new System.Windows.Forms.TextBox();
            this.txtCreateObjSpec = new System.Windows.Forms.TextBox();
            this.txtCreateObjType = new System.Windows.Forms.TextBox();
            this.groupBox41 = new System.Windows.Forms.GroupBox();
            this.btnUpdateObject_ProcessJob = new System.Windows.Forms.Button();
            this.groupBox31 = new System.Windows.Forms.GroupBox();
            this.txtS14F9_CreateOBJ_Type = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCreate_Reply = new System.Windows.Forms.Button();
            this.txtS14F9_SystemBytes_Recv = new System.Windows.Forms.TextBox();
            this.txtS14F9_OBJACK_Send = new System.Windows.Forms.TextBox();
            this.txtS14F9_OBJID_Recv = new System.Windows.Forms.TextBox();
            this.txtS14F9_OBJSPEC_Recv = new System.Windows.Forms.TextBox();
            this.txtS14F9_OBJTYPE_Recv = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox34 = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtS14F11_OBJSPEC_Recv = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.btnS14F12_Reply = new System.Windows.Forms.Button();
            this.txtS14F11_SystemBytes_Recv = new System.Windows.Forms.TextBox();
            this.txtS14F11_OBJACK_Send = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox37 = new System.Windows.Forms.GroupBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.btnProcessJobAlertNotify = new System.Windows.Forms.Button();
            this.txtS16F7_OBJACKA = new System.Windows.Forms.TextBox();
            this.txtS16F7_PRJOBMILESTONE = new System.Windows.Forms.TextBox();
            this.txtS16F7_OBJID = new System.Windows.Forms.TextBox();
            this.groupBox36 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.btnProcessJobEventNotify = new System.Windows.Forms.Button();
            this.txtS16F9_VIDs = new System.Windows.Forms.TextBox();
            this.txtS16F9_PREVENTID = new System.Windows.Forms.TextBox();
            this.txtS16F9_OBJID = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox39 = new System.Windows.Forms.GroupBox();
            this.btnControlJobCommandAck = new System.Windows.Forms.Button();
            this.label38 = new System.Windows.Forms.Label();
            this.txtS16F28_ErrText = new System.Windows.Forms.TextBox();
            this.label37 = new System.Windows.Forms.Label();
            this.txtS16F28_ErrCode = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.txtS16F28_Acka = new System.Windows.Forms.TextBox();
            this.txtS16F27_CPVal = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.txtS16F27_CPName = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.txtS16F27_Systembytes = new System.Windows.Forms.TextBox();
            this.txtS16F27_JobCmd = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.txtS16F27_JobId = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox40 = new System.Windows.Forms.GroupBox();
            this.btnCarrierActionAck = new System.Windows.Forms.Button();
            this.label40 = new System.Windows.Forms.Label();
            this.txtS3F18_ErrText = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.txtS3F18_ErrCode = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.txtS3F18_Caack = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.txtS3F17_PTN_Recv = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.txtS3F17_SystemBytes_Recv = new System.Windows.Forms.TextBox();
            this.txtS3F17_CARRIERID_Recv = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.txtS3F17_CARRIERACTION_Recv = new System.Windows.Forms.TextBox();
            this.tabSendSECSMessage = new System.Windows.Forms.TabPage();
            this.btnCustom = new System.Windows.Forms.Button();
            this.tabLoopback = new System.Windows.Forms.TabPage();
            this.groupBox30 = new System.Windows.Forms.GroupBox();
            this.txtABS_Recv = new System.Windows.Forms.TextBox();
            this.txtABS_RecvSystemBytes = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblABS_Check = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox32 = new System.Windows.Forms.GroupBox();
            this.btnLoopbackDiagnostic = new System.Windows.Forms.Button();
            this.pnlInit = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnOnLineLocal = new System.Windows.Forms.Button();
            this.btnOnLineRemote = new System.Windows.Forms.Button();
            this.btnOffLine = new System.Windows.Forms.Button();
            this.lblControlState = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDisableComm = new System.Windows.Forms.Button();
            this.btnEnableComm = new System.Windows.Forms.Button();
            this.lblCommunicatingState = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblSecsDriverStatus = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblInitResult = new System.Windows.Forms.Label();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tbcLog.SuspendLayout();
            this.tbpDriverLog.SuspendLayout();
            this.tbpGemLog.SuspendLayout();
            this.tbpSecsLog.SuspendLayout();
            this.tbpEQPLog.SuspendLayout();
            this.tbcFunctions.SuspendLayout();
            this.tbpSystemID.SuspendLayout();
            this.tbcSystemID.SuspendLayout();
            this.tbpSysSV.SuspendLayout();
            this.groupBox43.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.tbpSysEC.SuspendLayout();
            this.groupBox27.SuspendLayout();
            this.groupBox42.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.tbpEvent.SuspendLayout();
            this.groupBox23.SuspendLayout();
            this.flpEventResult.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tbpAlarm.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.flpAlarmResult.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tbpSV.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.flpSVResult.SuspendLayout();
            this.tbpEC.SuspendLayout();
            this.groupBox24.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNewECs)).BeginInit();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.flpECResult.SuspendLayout();
            this.tbpRemote.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCPs)).BeginInit();
            this.tabEnhanced.SuspendLayout();
            this.groupBox28.SuspendLayout();
            this.tabPP.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.groupBox26.SuspendLayout();
            this.groupBox25.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tbPPSetting.SuspendLayout();
            this.tbPPSlotMap.SuspendLayout();
            this.tbAngle.SuspendLayout();
            this.tbPPCofficient.SuspendLayout();
            this.tbMisc.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabClock.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.tabEQPTerminalServices.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.tabHostSetsCONTROLStateModel.SuspendLayout();
            this.groupBox29.SuspendLayout();
            this.tabObject.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox44.SuspendLayout();
            this.groupBox41.SuspendLayout();
            this.groupBox31.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox34.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox37.SuspendLayout();
            this.groupBox36.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox39.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox40.SuspendLayout();
            this.tabSendSECSMessage.SuspendLayout();
            this.tabLoopback.SuspendLayout();
            this.groupBox30.SuspendLayout();
            this.groupBox32.SuspendLayout();
            this.pnlInit.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEdit,
            this.tsmiInfo,
            this.離開ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(940, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiEdit
            // 
            this.tsmiEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiClearDriverLog,
            this.tsmiClearGemLog,
            this.tsmiClearSECSLog,
            this.tsmiClearEQPLog});
            this.tsmiEdit.Name = "tsmiEdit";
            this.tsmiEdit.Size = new System.Drawing.Size(43, 20);
            this.tsmiEdit.Text = "編輯";
            // 
            // tsmiClearDriverLog
            // 
            this.tsmiClearDriverLog.Name = "tsmiClearDriverLog";
            this.tsmiClearDriverLog.Size = new System.Drawing.Size(164, 22);
            this.tsmiClearDriverLog.Text = "Clear Driver Log";
            this.tsmiClearDriverLog.Click += new System.EventHandler(this.tsmiClearDriverLog_Click);
            // 
            // tsmiClearGemLog
            // 
            this.tsmiClearGemLog.Name = "tsmiClearGemLog";
            this.tsmiClearGemLog.Size = new System.Drawing.Size(164, 22);
            this.tsmiClearGemLog.Text = "Clear Gem Log";
            this.tsmiClearGemLog.Click += new System.EventHandler(this.tsmiClearGemLog_Click);
            // 
            // tsmiClearSECSLog
            // 
            this.tsmiClearSECSLog.Name = "tsmiClearSECSLog";
            this.tsmiClearSECSLog.Size = new System.Drawing.Size(164, 22);
            this.tsmiClearSECSLog.Text = "Clear SECS Log";
            this.tsmiClearSECSLog.Click += new System.EventHandler(this.tsmiClearSECSLog_Click);
            // 
            // tsmiClearEQPLog
            // 
            this.tsmiClearEQPLog.Name = "tsmiClearEQPLog";
            this.tsmiClearEQPLog.Size = new System.Drawing.Size(164, 22);
            this.tsmiClearEQPLog.Text = "Clear EQP Log";
            this.tsmiClearEQPLog.Click += new System.EventHandler(this.tsmiClearEQPLog_Click);
            // 
            // tsmiInfo
            // 
            this.tsmiInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDriverParameter});
            this.tsmiInfo.Name = "tsmiInfo";
            this.tsmiInfo.Size = new System.Drawing.Size(43, 20);
            this.tsmiInfo.Text = "資訊";
            // 
            // tsmiDriverParameter
            // 
            this.tsmiDriverParameter.Name = "tsmiDriverParameter";
            this.tsmiDriverParameter.Size = new System.Drawing.Size(168, 22);
            this.tsmiDriverParameter.Text = "Driver Parameter";
            this.tsmiDriverParameter.Click += new System.EventHandler(this.tsmiDriverParameter_Click);
            // 
            // 離開ToolStripMenuItem
            // 
            this.離開ToolStripMenuItem.Name = "離開ToolStripMenuItem";
            this.離開ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.離開ToolStripMenuItem.Text = "離開";
            this.離開ToolStripMenuItem.Click += new System.EventHandler(this.離開ToolStripMenuItem_Click);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 221F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tbcLog, 1, 0);
            this.tlpMain.Controls.Add(this.tbcFunctions, 0, 1);
            this.tlpMain.Controls.Add(this.pnlInit, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 24);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(2);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54.81284F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.18716F));
            this.tlpMain.Size = new System.Drawing.Size(940, 643);
            this.tlpMain.TabIndex = 1;
            // 
            // tbcLog
            // 
            this.tbcLog.Controls.Add(this.tbpDriverLog);
            this.tbcLog.Controls.Add(this.tbpGemLog);
            this.tbcLog.Controls.Add(this.tbpSecsLog);
            this.tbcLog.Controls.Add(this.tbpEQPLog);
            this.tbcLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcLog.Location = new System.Drawing.Point(223, 2);
            this.tbcLog.Margin = new System.Windows.Forms.Padding(2);
            this.tbcLog.Name = "tbcLog";
            this.tbcLog.SelectedIndex = 0;
            this.tbcLog.Size = new System.Drawing.Size(715, 348);
            this.tbcLog.TabIndex = 0;
            // 
            // tbpDriverLog
            // 
            this.tbpDriverLog.Controls.Add(this.rtbDriverLog);
            this.tbpDriverLog.Location = new System.Drawing.Point(4, 22);
            this.tbpDriverLog.Margin = new System.Windows.Forms.Padding(2);
            this.tbpDriverLog.Name = "tbpDriverLog";
            this.tbpDriverLog.Padding = new System.Windows.Forms.Padding(2);
            this.tbpDriverLog.Size = new System.Drawing.Size(707, 322);
            this.tbpDriverLog.TabIndex = 0;
            this.tbpDriverLog.Text = "DIASECS Log";
            this.tbpDriverLog.UseVisualStyleBackColor = true;
            // 
            // rtbDriverLog
            // 
            this.rtbDriverLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDriverLog.Font = new System.Drawing.Font("Courier New", 9F);
            this.rtbDriverLog.Location = new System.Drawing.Point(2, 2);
            this.rtbDriverLog.Margin = new System.Windows.Forms.Padding(2);
            this.rtbDriverLog.Name = "rtbDriverLog";
            this.rtbDriverLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtbDriverLog.Size = new System.Drawing.Size(703, 318);
            this.rtbDriverLog.TabIndex = 0;
            this.rtbDriverLog.Text = "";
            this.rtbDriverLog.WordWrap = false;
            // 
            // tbpGemLog
            // 
            this.tbpGemLog.Controls.Add(this.rtbGemLog);
            this.tbpGemLog.Location = new System.Drawing.Point(4, 22);
            this.tbpGemLog.Margin = new System.Windows.Forms.Padding(2);
            this.tbpGemLog.Name = "tbpGemLog";
            this.tbpGemLog.Padding = new System.Windows.Forms.Padding(2);
            this.tbpGemLog.Size = new System.Drawing.Size(707, 322);
            this.tbpGemLog.TabIndex = 2;
            this.tbpGemLog.Text = "DIASECSGEM Log";
            this.tbpGemLog.UseVisualStyleBackColor = true;
            // 
            // rtbGemLog
            // 
            this.rtbGemLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbGemLog.Font = new System.Drawing.Font("Courier New", 9F);
            this.rtbGemLog.Location = new System.Drawing.Point(2, 2);
            this.rtbGemLog.Margin = new System.Windows.Forms.Padding(2);
            this.rtbGemLog.Name = "rtbGemLog";
            this.rtbGemLog.Size = new System.Drawing.Size(703, 318);
            this.rtbGemLog.TabIndex = 0;
            this.rtbGemLog.Text = "";
            this.rtbGemLog.WordWrap = false;
            // 
            // tbpSecsLog
            // 
            this.tbpSecsLog.Controls.Add(this.rtbSecsLog);
            this.tbpSecsLog.Location = new System.Drawing.Point(4, 22);
            this.tbpSecsLog.Margin = new System.Windows.Forms.Padding(2);
            this.tbpSecsLog.Name = "tbpSecsLog";
            this.tbpSecsLog.Padding = new System.Windows.Forms.Padding(2);
            this.tbpSecsLog.Size = new System.Drawing.Size(707, 322);
            this.tbpSecsLog.TabIndex = 1;
            this.tbpSecsLog.Text = "SECS Log";
            this.tbpSecsLog.UseVisualStyleBackColor = true;
            // 
            // rtbSecsLog
            // 
            this.rtbSecsLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbSecsLog.Font = new System.Drawing.Font("Courier New", 10F);
            this.rtbSecsLog.Location = new System.Drawing.Point(2, 2);
            this.rtbSecsLog.Margin = new System.Windows.Forms.Padding(2);
            this.rtbSecsLog.Name = "rtbSecsLog";
            this.rtbSecsLog.Size = new System.Drawing.Size(703, 318);
            this.rtbSecsLog.TabIndex = 0;
            this.rtbSecsLog.Text = "";
            this.rtbSecsLog.WordWrap = false;
            // 
            // tbpEQPLog
            // 
            this.tbpEQPLog.Controls.Add(this.rtbEqpLog);
            this.tbpEQPLog.Location = new System.Drawing.Point(4, 22);
            this.tbpEQPLog.Margin = new System.Windows.Forms.Padding(2);
            this.tbpEQPLog.Name = "tbpEQPLog";
            this.tbpEQPLog.Size = new System.Drawing.Size(707, 322);
            this.tbpEQPLog.TabIndex = 3;
            this.tbpEQPLog.Text = "EQP Log";
            this.tbpEQPLog.UseVisualStyleBackColor = true;
            // 
            // rtbEqpLog
            // 
            this.rtbEqpLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbEqpLog.Font = new System.Drawing.Font("Courier New", 9F);
            this.rtbEqpLog.Location = new System.Drawing.Point(0, 0);
            this.rtbEqpLog.Margin = new System.Windows.Forms.Padding(2);
            this.rtbEqpLog.Name = "rtbEqpLog";
            this.rtbEqpLog.Size = new System.Drawing.Size(707, 322);
            this.rtbEqpLog.TabIndex = 0;
            this.rtbEqpLog.Text = "";
            this.rtbEqpLog.WordWrap = false;
            // 
            // tbcFunctions
            // 
            this.tlpMain.SetColumnSpan(this.tbcFunctions, 2);
            this.tbcFunctions.Controls.Add(this.tbpSystemID);
            this.tbcFunctions.Controls.Add(this.tbpEvent);
            this.tbcFunctions.Controls.Add(this.tbpAlarm);
            this.tbcFunctions.Controls.Add(this.tbpSV);
            this.tbcFunctions.Controls.Add(this.tbpEC);
            this.tbcFunctions.Controls.Add(this.tbpRemote);
            this.tbcFunctions.Controls.Add(this.tabEnhanced);
            this.tbcFunctions.Controls.Add(this.tabPP);
            this.tbcFunctions.Controls.Add(this.tabClock);
            this.tbcFunctions.Controls.Add(this.tabEQPTerminalServices);
            this.tbcFunctions.Controls.Add(this.tabHostSetsCONTROLStateModel);
            this.tbcFunctions.Controls.Add(this.tabObject);
            this.tbcFunctions.Controls.Add(this.tabSendSECSMessage);
            this.tbcFunctions.Controls.Add(this.tabLoopback);
            this.tbcFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcFunctions.Location = new System.Drawing.Point(2, 354);
            this.tbcFunctions.Margin = new System.Windows.Forms.Padding(2);
            this.tbcFunctions.Name = "tbcFunctions";
            this.tbcFunctions.SelectedIndex = 0;
            this.tbcFunctions.Size = new System.Drawing.Size(936, 287);
            this.tbcFunctions.TabIndex = 1;
            // 
            // tbpSystemID
            // 
            this.tbpSystemID.Controls.Add(this.tbcSystemID);
            this.tbpSystemID.Location = new System.Drawing.Point(4, 22);
            this.tbpSystemID.Margin = new System.Windows.Forms.Padding(2);
            this.tbpSystemID.Name = "tbpSystemID";
            this.tbpSystemID.Padding = new System.Windows.Forms.Padding(2);
            this.tbpSystemID.Size = new System.Drawing.Size(928, 261);
            this.tbpSystemID.TabIndex = 0;
            this.tbpSystemID.Text = "System ID";
            this.tbpSystemID.UseVisualStyleBackColor = true;
            // 
            // tbcSystemID
            // 
            this.tbcSystemID.Controls.Add(this.tbpSysSV);
            this.tbcSystemID.Controls.Add(this.tbpSysEC);
            this.tbcSystemID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcSystemID.Location = new System.Drawing.Point(2, 2);
            this.tbcSystemID.Margin = new System.Windows.Forms.Padding(2);
            this.tbcSystemID.Name = "tbcSystemID";
            this.tbcSystemID.SelectedIndex = 0;
            this.tbcSystemID.Size = new System.Drawing.Size(924, 257);
            this.tbcSystemID.TabIndex = 0;
            // 
            // tbpSysSV
            // 
            this.tbpSysSV.Controls.Add(this.groupBox43);
            this.tbpSysSV.Controls.Add(this.groupBox13);
            this.tbpSysSV.Controls.Add(this.groupBox15);
            this.tbpSysSV.Controls.Add(this.groupBox12);
            this.tbpSysSV.Location = new System.Drawing.Point(4, 22);
            this.tbpSysSV.Margin = new System.Windows.Forms.Padding(2);
            this.tbpSysSV.Name = "tbpSysSV";
            this.tbpSysSV.Padding = new System.Windows.Forms.Padding(2);
            this.tbpSysSV.Size = new System.Drawing.Size(916, 231);
            this.tbpSysSV.TabIndex = 0;
            this.tbpSysSV.Text = "SV";
            this.tbpSysSV.UseVisualStyleBackColor = true;
            // 
            // groupBox43
            // 
            this.groupBox43.Controls.Add(this.lblSysSV_PRJobSpace);
            this.groupBox43.Location = new System.Drawing.Point(414, 12);
            this.groupBox43.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox43.Name = "groupBox43";
            this.groupBox43.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox43.Size = new System.Drawing.Size(307, 86);
            this.groupBox43.TabIndex = 3;
            this.groupBox43.TabStop = false;
            this.groupBox43.Text = "GEM300 Object";
            // 
            // lblSysSV_PRJobSpace
            // 
            this.lblSysSV_PRJobSpace.AutoSize = true;
            this.lblSysSV_PRJobSpace.Location = new System.Drawing.Point(12, 24);
            this.lblSysSV_PRJobSpace.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSysSV_PRJobSpace.Name = "lblSysSV_PRJobSpace";
            this.lblSysSV_PRJobSpace.Size = new System.Drawing.Size(151, 12);
            this.lblSysSV_PRJobSpace.TabIndex = 0;
            this.lblSysSV_PRJobSpace.Text = "(E40-PM)PJ_PRJobSpace(101)";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.lblSysSV_SpoolFullTime);
            this.groupBox13.Controls.Add(this.lblSysSV_SpoolStartTime);
            this.groupBox13.Controls.Add(this.lblSysSV_SpoolCountTotal);
            this.groupBox13.Controls.Add(this.lblSysSV_SpoolCountActual);
            this.groupBox13.Controls.Add(this.lblSysSV_SpoolUnloadSubState);
            this.groupBox13.Controls.Add(this.lblSysSV_SpoolLoadSubState);
            this.groupBox13.Controls.Add(this.lblSysSV_SpoolState);
            this.groupBox13.Location = new System.Drawing.Point(178, 12);
            this.groupBox13.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox13.Size = new System.Drawing.Size(225, 198);
            this.groupBox13.TabIndex = 1;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Spooling";
            // 
            // lblSysSV_SpoolFullTime
            // 
            this.lblSysSV_SpoolFullTime.AutoSize = true;
            this.lblSysSV_SpoolFullTime.Location = new System.Drawing.Point(12, 144);
            this.lblSysSV_SpoolFullTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSysSV_SpoolFullTime.Name = "lblSysSV_SpoolFullTime";
            this.lblSysSV_SpoolFullTime.Size = new System.Drawing.Size(94, 12);
            this.lblSysSV_SpoolFullTime.TabIndex = 2;
            this.lblSysSV_SpoolFullTime.Text = "SpoolFullTime(20)";
            // 
            // lblSysSV_SpoolStartTime
            // 
            this.lblSysSV_SpoolStartTime.AutoSize = true;
            this.lblSysSV_SpoolStartTime.Location = new System.Drawing.Point(12, 124);
            this.lblSysSV_SpoolStartTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSysSV_SpoolStartTime.Name = "lblSysSV_SpoolStartTime";
            this.lblSysSV_SpoolStartTime.Size = new System.Drawing.Size(97, 12);
            this.lblSysSV_SpoolStartTime.TabIndex = 1;
            this.lblSysSV_SpoolStartTime.Text = "SpoolStartTime(19)";
            // 
            // lblSysSV_SpoolCountTotal
            // 
            this.lblSysSV_SpoolCountTotal.AutoSize = true;
            this.lblSysSV_SpoolCountTotal.Location = new System.Drawing.Point(12, 104);
            this.lblSysSV_SpoolCountTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSysSV_SpoolCountTotal.Name = "lblSysSV_SpoolCountTotal";
            this.lblSysSV_SpoolCountTotal.Size = new System.Drawing.Size(105, 12);
            this.lblSysSV_SpoolCountTotal.TabIndex = 0;
            this.lblSysSV_SpoolCountTotal.Text = "SpoolCountTotal(22)";
            // 
            // lblSysSV_SpoolCountActual
            // 
            this.lblSysSV_SpoolCountActual.AutoSize = true;
            this.lblSysSV_SpoolCountActual.Location = new System.Drawing.Point(12, 84);
            this.lblSysSV_SpoolCountActual.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSysSV_SpoolCountActual.Name = "lblSysSV_SpoolCountActual";
            this.lblSysSV_SpoolCountActual.Size = new System.Drawing.Size(111, 12);
            this.lblSysSV_SpoolCountActual.TabIndex = 0;
            this.lblSysSV_SpoolCountActual.Text = "SpoolCountActual(21)";
            // 
            // lblSysSV_SpoolUnloadSubState
            // 
            this.lblSysSV_SpoolUnloadSubState.AutoSize = true;
            this.lblSysSV_SpoolUnloadSubState.Location = new System.Drawing.Point(12, 64);
            this.lblSysSV_SpoolUnloadSubState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSysSV_SpoolUnloadSubState.Name = "lblSysSV_SpoolUnloadSubState";
            this.lblSysSV_SpoolUnloadSubState.Size = new System.Drawing.Size(126, 12);
            this.lblSysSV_SpoolUnloadSubState.TabIndex = 0;
            this.lblSysSV_SpoolUnloadSubState.Text = "SpoolUnloadSubState(18)";
            // 
            // lblSysSV_SpoolLoadSubState
            // 
            this.lblSysSV_SpoolLoadSubState.AutoSize = true;
            this.lblSysSV_SpoolLoadSubState.Location = new System.Drawing.Point(12, 44);
            this.lblSysSV_SpoolLoadSubState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSysSV_SpoolLoadSubState.Name = "lblSysSV_SpoolLoadSubState";
            this.lblSysSV_SpoolLoadSubState.Size = new System.Drawing.Size(116, 12);
            this.lblSysSV_SpoolLoadSubState.TabIndex = 0;
            this.lblSysSV_SpoolLoadSubState.Text = "SpoolLoadSubState(17)";
            // 
            // lblSysSV_SpoolState
            // 
            this.lblSysSV_SpoolState.AutoSize = true;
            this.lblSysSV_SpoolState.Location = new System.Drawing.Point(12, 24);
            this.lblSysSV_SpoolState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSysSV_SpoolState.Name = "lblSysSV_SpoolState";
            this.lblSysSV_SpoolState.Size = new System.Drawing.Size(74, 12);
            this.lblSysSV_SpoolState.TabIndex = 0;
            this.lblSysSV_SpoolState.Text = "SpoolState(16)";
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.lblSysSV_Clock);
            this.groupBox15.Location = new System.Drawing.Point(4, 76);
            this.groupBox15.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox15.Size = new System.Drawing.Size(169, 57);
            this.groupBox15.TabIndex = 0;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Clock(3)";
            // 
            // lblSysSV_Clock
            // 
            this.lblSysSV_Clock.AutoSize = true;
            this.lblSysSV_Clock.Location = new System.Drawing.Point(12, 26);
            this.lblSysSV_Clock.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSysSV_Clock.Name = "lblSysSV_Clock";
            this.lblSysSV_Clock.Size = new System.Drawing.Size(47, 12);
            this.lblSysSV_Clock.TabIndex = 0;
            this.lblSysSV_Clock.Text = "Clock(3)";
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.lblSysSV_ControlState);
            this.groupBox12.Location = new System.Drawing.Point(4, 12);
            this.groupBox12.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox12.Size = new System.Drawing.Size(169, 57);
            this.groupBox12.TabIndex = 0;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "ControlState(6)";
            // 
            // lblSysSV_ControlState
            // 
            this.lblSysSV_ControlState.AutoSize = true;
            this.lblSysSV_ControlState.Location = new System.Drawing.Point(12, 26);
            this.lblSysSV_ControlState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSysSV_ControlState.Name = "lblSysSV_ControlState";
            this.lblSysSV_ControlState.Size = new System.Drawing.Size(63, 12);
            this.lblSysSV_ControlState.TabIndex = 0;
            this.lblSysSV_ControlState.Text = "ControlState";
            // 
            // tbpSysEC
            // 
            this.tbpSysEC.Controls.Add(this.groupBox27);
            this.tbpSysEC.Controls.Add(this.groupBox42);
            this.tbpSysEC.Controls.Add(this.groupBox14);
            this.tbpSysEC.Location = new System.Drawing.Point(4, 22);
            this.tbpSysEC.Margin = new System.Windows.Forms.Padding(2);
            this.tbpSysEC.Name = "tbpSysEC";
            this.tbpSysEC.Padding = new System.Windows.Forms.Padding(2);
            this.tbpSysEC.Size = new System.Drawing.Size(916, 231);
            this.tbpSysEC.TabIndex = 1;
            this.tbpSysEC.Text = "EC";
            this.tbpSysEC.UseVisualStyleBackColor = true;
            // 
            // groupBox27
            // 
            this.groupBox27.Controls.Add(this.rdoLog_Error);
            this.groupBox27.Controls.Add(this.rdoLog_Warn);
            this.groupBox27.Controls.Add(this.rdoLog_Info);
            this.groupBox27.Controls.Add(this.rdoLog_Debug);
            this.groupBox27.Controls.Add(this.rdoLog_Trace);
            this.groupBox27.Location = new System.Drawing.Point(178, 12);
            this.groupBox27.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox27.Size = new System.Drawing.Size(307, 56);
            this.groupBox27.TabIndex = 3;
            this.groupBox27.TabStop = false;
            this.groupBox27.Text = "DIASECSGEM Debug Out Log Level(83)";
            // 
            // rdoLog_Error
            // 
            this.rdoLog_Error.AutoSize = true;
            this.rdoLog_Error.Location = new System.Drawing.Point(250, 24);
            this.rdoLog_Error.Margin = new System.Windows.Forms.Padding(2);
            this.rdoLog_Error.Name = "rdoLog_Error";
            this.rdoLog_Error.Size = new System.Drawing.Size(57, 16);
            this.rdoLog_Error.TabIndex = 4;
            this.rdoLog_Error.Text = "4:Error";
            this.rdoLog_Error.UseVisualStyleBackColor = true;
            this.rdoLog_Error.CheckedChanged += new System.EventHandler(this.rdoLog_CheckedChanged);
            // 
            // rdoLog_Warn
            // 
            this.rdoLog_Warn.AutoSize = true;
            this.rdoLog_Warn.Location = new System.Drawing.Point(191, 24);
            this.rdoLog_Warn.Margin = new System.Windows.Forms.Padding(2);
            this.rdoLog_Warn.Name = "rdoLog_Warn";
            this.rdoLog_Warn.Size = new System.Drawing.Size(58, 16);
            this.rdoLog_Warn.TabIndex = 3;
            this.rdoLog_Warn.Text = "3:Warn";
            this.rdoLog_Warn.UseVisualStyleBackColor = true;
            this.rdoLog_Warn.CheckedChanged += new System.EventHandler(this.rdoLog_CheckedChanged);
            // 
            // rdoLog_Info
            // 
            this.rdoLog_Info.AutoSize = true;
            this.rdoLog_Info.Location = new System.Drawing.Point(137, 24);
            this.rdoLog_Info.Margin = new System.Windows.Forms.Padding(2);
            this.rdoLog_Info.Name = "rdoLog_Info";
            this.rdoLog_Info.Size = new System.Drawing.Size(52, 16);
            this.rdoLog_Info.TabIndex = 2;
            this.rdoLog_Info.Text = "2:Info";
            this.rdoLog_Info.UseVisualStyleBackColor = true;
            this.rdoLog_Info.CheckedChanged += new System.EventHandler(this.rdoLog_CheckedChanged);
            // 
            // rdoLog_Debug
            // 
            this.rdoLog_Debug.AutoSize = true;
            this.rdoLog_Debug.Location = new System.Drawing.Point(74, 24);
            this.rdoLog_Debug.Margin = new System.Windows.Forms.Padding(2);
            this.rdoLog_Debug.Name = "rdoLog_Debug";
            this.rdoLog_Debug.Size = new System.Drawing.Size(63, 16);
            this.rdoLog_Debug.TabIndex = 1;
            this.rdoLog_Debug.Text = "1:Debug";
            this.rdoLog_Debug.UseVisualStyleBackColor = true;
            this.rdoLog_Debug.CheckedChanged += new System.EventHandler(this.rdoLog_CheckedChanged);
            // 
            // rdoLog_Trace
            // 
            this.rdoLog_Trace.AutoSize = true;
            this.rdoLog_Trace.Checked = true;
            this.rdoLog_Trace.Location = new System.Drawing.Point(14, 24);
            this.rdoLog_Trace.Margin = new System.Windows.Forms.Padding(2);
            this.rdoLog_Trace.Name = "rdoLog_Trace";
            this.rdoLog_Trace.Size = new System.Drawing.Size(58, 16);
            this.rdoLog_Trace.TabIndex = 0;
            this.rdoLog_Trace.TabStop = true;
            this.rdoLog_Trace.Text = "0:Trace";
            this.rdoLog_Trace.UseVisualStyleBackColor = true;
            this.rdoLog_Trace.CheckedChanged += new System.EventHandler(this.rdoLog_CheckedChanged);
            // 
            // groupBox42
            // 
            this.groupBox42.Controls.Add(this.lblSysEC_PRMaxJobSpace);
            this.groupBox42.Location = new System.Drawing.Point(178, 71);
            this.groupBox42.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox42.Name = "groupBox42";
            this.groupBox42.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox42.Size = new System.Drawing.Size(307, 86);
            this.groupBox42.TabIndex = 2;
            this.groupBox42.TabStop = false;
            this.groupBox42.Text = "GEM300 Object";
            // 
            // lblSysEC_PRMaxJobSpace
            // 
            this.lblSysEC_PRMaxJobSpace.AutoSize = true;
            this.lblSysEC_PRMaxJobSpace.Location = new System.Drawing.Point(12, 24);
            this.lblSysEC_PRMaxJobSpace.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSysEC_PRMaxJobSpace.Name = "lblSysEC_PRMaxJobSpace";
            this.lblSysEC_PRMaxJobSpace.Size = new System.Drawing.Size(172, 12);
            this.lblSysEC_PRMaxJobSpace.TabIndex = 0;
            this.lblSysEC_PRMaxJobSpace.Text = "(E40-PM)PJ_PRMaxJobSpace(171)";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.lblSysEC_MaxSpoolTransmit);
            this.groupBox14.Controls.Add(this.lblSysEC_OverWriteSpool);
            this.groupBox14.Controls.Add(this.lblSysEC_ConfigSpool);
            this.groupBox14.Location = new System.Drawing.Point(4, 12);
            this.groupBox14.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox14.Size = new System.Drawing.Size(169, 198);
            this.groupBox14.TabIndex = 2;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Spooling";
            // 
            // lblSysEC_MaxSpoolTransmit
            // 
            this.lblSysEC_MaxSpoolTransmit.AutoSize = true;
            this.lblSysEC_MaxSpoolTransmit.Location = new System.Drawing.Point(12, 64);
            this.lblSysEC_MaxSpoolTransmit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSysEC_MaxSpoolTransmit.Name = "lblSysEC_MaxSpoolTransmit";
            this.lblSysEC_MaxSpoolTransmit.Size = new System.Drawing.Size(114, 12);
            this.lblSysEC_MaxSpoolTransmit.TabIndex = 0;
            this.lblSysEC_MaxSpoolTransmit.Text = "MaxSpoolTransmit(78)";
            // 
            // lblSysEC_OverWriteSpool
            // 
            this.lblSysEC_OverWriteSpool.AutoSize = true;
            this.lblSysEC_OverWriteSpool.Location = new System.Drawing.Point(12, 44);
            this.lblSysEC_OverWriteSpool.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSysEC_OverWriteSpool.Name = "lblSysEC_OverWriteSpool";
            this.lblSysEC_OverWriteSpool.Size = new System.Drawing.Size(101, 12);
            this.lblSysEC_OverWriteSpool.TabIndex = 0;
            this.lblSysEC_OverWriteSpool.Text = "OverWriteSpool(79)";
            // 
            // lblSysEC_ConfigSpool
            // 
            this.lblSysEC_ConfigSpool.AutoSize = true;
            this.lblSysEC_ConfigSpool.Location = new System.Drawing.Point(12, 24);
            this.lblSysEC_ConfigSpool.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSysEC_ConfigSpool.Name = "lblSysEC_ConfigSpool";
            this.lblSysEC_ConfigSpool.Size = new System.Drawing.Size(85, 12);
            this.lblSysEC_ConfigSpool.TabIndex = 0;
            this.lblSysEC_ConfigSpool.Text = "ConfigSpool(77)";
            // 
            // tbpEvent
            // 
            this.tbpEvent.Controls.Add(this.groupBox23);
            this.tbpEvent.Controls.Add(this.flpEventResult);
            this.tbpEvent.Controls.Add(this.groupBox5);
            this.tbpEvent.Location = new System.Drawing.Point(4, 22);
            this.tbpEvent.Margin = new System.Windows.Forms.Padding(2);
            this.tbpEvent.Name = "tbpEvent";
            this.tbpEvent.Padding = new System.Windows.Forms.Padding(2);
            this.tbpEvent.Size = new System.Drawing.Size(928, 261);
            this.tbpEvent.TabIndex = 1;
            this.tbpEvent.Text = "Event";
            this.tbpEvent.UseVisualStyleBackColor = true;
            // 
            // groupBox23
            // 
            this.groupBox23.BackColor = System.Drawing.Color.LightCyan;
            this.groupBox23.Controls.Add(this.rdoProcessState_Down);
            this.groupBox23.Controls.Add(this.rdoProcessState_Pause);
            this.groupBox23.Controls.Add(this.rdoProcessState_Stop);
            this.groupBox23.Controls.Add(this.rdoProcessState_Run);
            this.groupBox23.Controls.Add(this.rdoProcessState_Idle);
            this.groupBox23.Controls.Add(this.rdoProcessState_Initial);
            this.groupBox23.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox23.Location = new System.Drawing.Point(456, 31);
            this.groupBox23.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox23.Size = new System.Drawing.Size(203, 94);
            this.groupBox23.TabIndex = 5;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "Process State";
            // 
            // rdoProcessState_Down
            // 
            this.rdoProcessState_Down.AutoSize = true;
            this.rdoProcessState_Down.Location = new System.Drawing.Point(137, 55);
            this.rdoProcessState_Down.Margin = new System.Windows.Forms.Padding(2);
            this.rdoProcessState_Down.Name = "rdoProcessState_Down";
            this.rdoProcessState_Down.Size = new System.Drawing.Size(67, 23);
            this.rdoProcessState_Down.TabIndex = 5;
            this.rdoProcessState_Down.Text = "Down";
            this.rdoProcessState_Down.UseVisualStyleBackColor = true;
            this.rdoProcessState_Down.Click += new System.EventHandler(this.rdoProcessState_Click);
            // 
            // rdoProcessState_Pause
            // 
            this.rdoProcessState_Pause.AutoSize = true;
            this.rdoProcessState_Pause.Location = new System.Drawing.Point(75, 55);
            this.rdoProcessState_Pause.Margin = new System.Windows.Forms.Padding(2);
            this.rdoProcessState_Pause.Name = "rdoProcessState_Pause";
            this.rdoProcessState_Pause.Size = new System.Drawing.Size(67, 23);
            this.rdoProcessState_Pause.TabIndex = 4;
            this.rdoProcessState_Pause.Text = "Pause";
            this.rdoProcessState_Pause.UseVisualStyleBackColor = true;
            this.rdoProcessState_Pause.Click += new System.EventHandler(this.rdoProcessState_Click);
            // 
            // rdoProcessState_Stop
            // 
            this.rdoProcessState_Stop.AutoSize = true;
            this.rdoProcessState_Stop.Location = new System.Drawing.Point(13, 55);
            this.rdoProcessState_Stop.Margin = new System.Windows.Forms.Padding(2);
            this.rdoProcessState_Stop.Name = "rdoProcessState_Stop";
            this.rdoProcessState_Stop.Size = new System.Drawing.Size(59, 23);
            this.rdoProcessState_Stop.TabIndex = 3;
            this.rdoProcessState_Stop.Text = "Stop";
            this.rdoProcessState_Stop.UseVisualStyleBackColor = true;
            this.rdoProcessState_Stop.Click += new System.EventHandler(this.rdoProcessState_Click);
            // 
            // rdoProcessState_Run
            // 
            this.rdoProcessState_Run.AutoSize = true;
            this.rdoProcessState_Run.Location = new System.Drawing.Point(137, 25);
            this.rdoProcessState_Run.Margin = new System.Windows.Forms.Padding(2);
            this.rdoProcessState_Run.Name = "rdoProcessState_Run";
            this.rdoProcessState_Run.Size = new System.Drawing.Size(54, 23);
            this.rdoProcessState_Run.TabIndex = 2;
            this.rdoProcessState_Run.Text = "Run";
            this.rdoProcessState_Run.UseVisualStyleBackColor = true;
            this.rdoProcessState_Run.Click += new System.EventHandler(this.rdoProcessState_Click);
            // 
            // rdoProcessState_Idle
            // 
            this.rdoProcessState_Idle.AutoSize = true;
            this.rdoProcessState_Idle.Location = new System.Drawing.Point(76, 25);
            this.rdoProcessState_Idle.Margin = new System.Windows.Forms.Padding(2);
            this.rdoProcessState_Idle.Name = "rdoProcessState_Idle";
            this.rdoProcessState_Idle.Size = new System.Drawing.Size(52, 23);
            this.rdoProcessState_Idle.TabIndex = 1;
            this.rdoProcessState_Idle.Text = "Idle";
            this.rdoProcessState_Idle.UseVisualStyleBackColor = true;
            this.rdoProcessState_Idle.Click += new System.EventHandler(this.rdoProcessState_Click);
            // 
            // rdoProcessState_Initial
            // 
            this.rdoProcessState_Initial.AutoSize = true;
            this.rdoProcessState_Initial.Checked = true;
            this.rdoProcessState_Initial.Location = new System.Drawing.Point(13, 25);
            this.rdoProcessState_Initial.Margin = new System.Windows.Forms.Padding(2);
            this.rdoProcessState_Initial.Name = "rdoProcessState_Initial";
            this.rdoProcessState_Initial.Size = new System.Drawing.Size(65, 23);
            this.rdoProcessState_Initial.TabIndex = 0;
            this.rdoProcessState_Initial.TabStop = true;
            this.rdoProcessState_Initial.Text = "Initial";
            this.rdoProcessState_Initial.UseVisualStyleBackColor = true;
            this.rdoProcessState_Initial.Click += new System.EventHandler(this.rdoProcessState_Click);
            // 
            // flpEventResult
            // 
            this.flpEventResult.Controls.Add(this.lblEventResultCaption);
            this.flpEventResult.Controls.Add(this.lblEventResultText);
            this.flpEventResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpEventResult.Location = new System.Drawing.Point(2, 222);
            this.flpEventResult.Margin = new System.Windows.Forms.Padding(2);
            this.flpEventResult.Name = "flpEventResult";
            this.flpEventResult.Size = new System.Drawing.Size(924, 37);
            this.flpEventResult.TabIndex = 4;
            // 
            // lblEventResultCaption
            // 
            this.lblEventResultCaption.AutoSize = true;
            this.lblEventResultCaption.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEventResultCaption.Location = new System.Drawing.Point(2, 8);
            this.lblEventResultCaption.Margin = new System.Windows.Forms.Padding(2, 8, 2, 2);
            this.lblEventResultCaption.Name = "lblEventResultCaption";
            this.lblEventResultCaption.Size = new System.Drawing.Size(175, 19);
            this.lblEventResultCaption.TabIndex = 0;
            this.lblEventResultCaption.Text = "Event Command Result :";
            // 
            // lblEventResultText
            // 
            this.lblEventResultText.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEventResultText.Location = new System.Drawing.Point(181, 8);
            this.lblEventResultText.Margin = new System.Windows.Forms.Padding(2, 8, 2, 2);
            this.lblEventResultText.Name = "lblEventResultText";
            this.lblEventResultText.Size = new System.Drawing.Size(193, 18);
            this.lblEventResultText.TabIndex = 1;
            this.lblEventResultText.Text = "Result";
            this.lblEventResultText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblEventID);
            this.groupBox5.Controls.Add(this.btnEventReport);
            this.groupBox5.Controls.Add(this.txtEventID);
            this.groupBox5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(218, 22);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(199, 104);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "S6F11";
            // 
            // lblEventID
            // 
            this.lblEventID.AutoSize = true;
            this.lblEventID.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEventID.Location = new System.Drawing.Point(19, 25);
            this.lblEventID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEventID.Name = "lblEventID";
            this.lblEventID.Size = new System.Drawing.Size(74, 19);
            this.lblEventID.TabIndex = 0;
            this.lblEventID.Text = "Event ID :";
            // 
            // btnEventReport
            // 
            this.btnEventReport.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEventReport.Location = new System.Drawing.Point(22, 60);
            this.btnEventReport.Margin = new System.Windows.Forms.Padding(2);
            this.btnEventReport.Name = "btnEventReport";
            this.btnEventReport.Size = new System.Drawing.Size(172, 27);
            this.btnEventReport.TabIndex = 2;
            this.btnEventReport.Text = "Event Report Send";
            this.btnEventReport.UseVisualStyleBackColor = true;
            this.btnEventReport.Click += new System.EventHandler(this.btnEventReport_Click);
            // 
            // txtEventID
            // 
            this.txtEventID.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEventID.Location = new System.Drawing.Point(99, 22);
            this.txtEventID.Margin = new System.Windows.Forms.Padding(2);
            this.txtEventID.Name = "txtEventID";
            this.txtEventID.Size = new System.Drawing.Size(96, 27);
            this.txtEventID.TabIndex = 1;
            this.txtEventID.Text = "101";
            this.txtEventID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbpAlarm
            // 
            this.tbpAlarm.Controls.Add(this.groupBox20);
            this.tbpAlarm.Controls.Add(this.flpAlarmResult);
            this.tbpAlarm.Controls.Add(this.groupBox6);
            this.tbpAlarm.Location = new System.Drawing.Point(4, 22);
            this.tbpAlarm.Margin = new System.Windows.Forms.Padding(2);
            this.tbpAlarm.Name = "tbpAlarm";
            this.tbpAlarm.Padding = new System.Windows.Forms.Padding(2);
            this.tbpAlarm.Size = new System.Drawing.Size(928, 261);
            this.tbpAlarm.TabIndex = 2;
            this.tbpAlarm.Text = "Alarm";
            this.tbpAlarm.UseVisualStyleBackColor = true;
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.txtEqpCurrentAlarmSet);
            this.groupBox20.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox20.Location = new System.Drawing.Point(10, 21);
            this.groupBox20.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox20.Size = new System.Drawing.Size(264, 178);
            this.groupBox20.TabIndex = 10;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "EqpCurrentAlarmSet";
            // 
            // txtEqpCurrentAlarmSet
            // 
            this.txtEqpCurrentAlarmSet.Location = new System.Drawing.Point(4, 25);
            this.txtEqpCurrentAlarmSet.Margin = new System.Windows.Forms.Padding(2);
            this.txtEqpCurrentAlarmSet.MaxLength = 10;
            this.txtEqpCurrentAlarmSet.Multiline = true;
            this.txtEqpCurrentAlarmSet.Name = "txtEqpCurrentAlarmSet";
            this.txtEqpCurrentAlarmSet.ReadOnly = true;
            this.txtEqpCurrentAlarmSet.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtEqpCurrentAlarmSet.Size = new System.Drawing.Size(242, 149);
            this.txtEqpCurrentAlarmSet.TabIndex = 5;
            this.txtEqpCurrentAlarmSet.WordWrap = false;
            // 
            // flpAlarmResult
            // 
            this.flpAlarmResult.Controls.Add(this.lblAlarmResultCaption);
            this.flpAlarmResult.Controls.Add(this.lblAlarmResultText);
            this.flpAlarmResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpAlarmResult.Location = new System.Drawing.Point(2, 222);
            this.flpAlarmResult.Margin = new System.Windows.Forms.Padding(2);
            this.flpAlarmResult.Name = "flpAlarmResult";
            this.flpAlarmResult.Size = new System.Drawing.Size(924, 37);
            this.flpAlarmResult.TabIndex = 1;
            // 
            // lblAlarmResultCaption
            // 
            this.lblAlarmResultCaption.AutoSize = true;
            this.lblAlarmResultCaption.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlarmResultCaption.Location = new System.Drawing.Point(2, 8);
            this.lblAlarmResultCaption.Margin = new System.Windows.Forms.Padding(2, 8, 2, 2);
            this.lblAlarmResultCaption.Name = "lblAlarmResultCaption";
            this.lblAlarmResultCaption.Size = new System.Drawing.Size(177, 19);
            this.lblAlarmResultCaption.TabIndex = 0;
            this.lblAlarmResultCaption.Text = "Alarm Command Result :";
            // 
            // lblAlarmResultText
            // 
            this.lblAlarmResultText.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlarmResultText.Location = new System.Drawing.Point(183, 8);
            this.lblAlarmResultText.Margin = new System.Windows.Forms.Padding(2, 8, 2, 2);
            this.lblAlarmResultText.Name = "lblAlarmResultText";
            this.lblAlarmResultText.Size = new System.Drawing.Size(193, 18);
            this.lblAlarmResultText.TabIndex = 1;
            this.lblAlarmResultText.Text = "Result";
            this.lblAlarmResultText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnAlarmReportSend);
            this.groupBox6.Controls.Add(this.txtAlarmID);
            this.groupBox6.Controls.Add(this.lblAlarmID);
            this.groupBox6.Controls.Add(this.rdoAlarmClean);
            this.groupBox6.Controls.Add(this.lblAlarmState);
            this.groupBox6.Controls.Add(this.rdoAlarmSet);
            this.groupBox6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(285, 21);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(230, 137);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Alarm Report Set";
            // 
            // btnAlarmReportSend
            // 
            this.btnAlarmReportSend.Location = new System.Drawing.Point(26, 95);
            this.btnAlarmReportSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnAlarmReportSend.Name = "btnAlarmReportSend";
            this.btnAlarmReportSend.Size = new System.Drawing.Size(191, 29);
            this.btnAlarmReportSend.TabIndex = 5;
            this.btnAlarmReportSend.Text = "Alarm Report Send";
            this.btnAlarmReportSend.UseVisualStyleBackColor = true;
            this.btnAlarmReportSend.Click += new System.EventHandler(this.btnAlarmReportSend_Click);
            // 
            // txtAlarmID
            // 
            this.txtAlarmID.Location = new System.Drawing.Point(105, 57);
            this.txtAlarmID.Margin = new System.Windows.Forms.Padding(2);
            this.txtAlarmID.Name = "txtAlarmID";
            this.txtAlarmID.Size = new System.Drawing.Size(114, 27);
            this.txtAlarmID.TabIndex = 4;
            // 
            // lblAlarmID
            // 
            this.lblAlarmID.AutoSize = true;
            this.lblAlarmID.Location = new System.Drawing.Point(23, 59);
            this.lblAlarmID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAlarmID.Name = "lblAlarmID";
            this.lblAlarmID.Size = new System.Drawing.Size(76, 19);
            this.lblAlarmID.TabIndex = 3;
            this.lblAlarmID.Text = "Alarm ID :";
            // 
            // rdoAlarmClean
            // 
            this.rdoAlarmClean.AutoSize = true;
            this.rdoAlarmClean.Checked = true;
            this.rdoAlarmClean.Location = new System.Drawing.Point(159, 30);
            this.rdoAlarmClean.Margin = new System.Windows.Forms.Padding(2);
            this.rdoAlarmClean.Name = "rdoAlarmClean";
            this.rdoAlarmClean.Size = new System.Drawing.Size(64, 23);
            this.rdoAlarmClean.TabIndex = 2;
            this.rdoAlarmClean.TabStop = true;
            this.rdoAlarmClean.Text = "Clean";
            this.rdoAlarmClean.UseVisualStyleBackColor = true;
            // 
            // lblAlarmState
            // 
            this.lblAlarmState.AutoSize = true;
            this.lblAlarmState.Location = new System.Drawing.Point(4, 28);
            this.lblAlarmState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAlarmState.Name = "lblAlarmState";
            this.lblAlarmState.Size = new System.Drawing.Size(98, 19);
            this.lblAlarmState.TabIndex = 1;
            this.lblAlarmState.Text = "Alarm State :";
            // 
            // rdoAlarmSet
            // 
            this.rdoAlarmSet.AutoSize = true;
            this.rdoAlarmSet.Checked = true;
            this.rdoAlarmSet.Location = new System.Drawing.Point(105, 30);
            this.rdoAlarmSet.Margin = new System.Windows.Forms.Padding(2);
            this.rdoAlarmSet.Name = "rdoAlarmSet";
            this.rdoAlarmSet.Size = new System.Drawing.Size(49, 23);
            this.rdoAlarmSet.TabIndex = 0;
            this.rdoAlarmSet.TabStop = true;
            this.rdoAlarmSet.Text = "Set";
            this.rdoAlarmSet.UseVisualStyleBackColor = true;
            // 
            // tbpSV
            // 
            this.tbpSV.Controls.Add(this.chkContinueUpdateSV);
            this.tbpSV.Controls.Add(this.btnUpdateListTypeSV);
            this.tbpSV.Controls.Add(this.groupBox8);
            this.tbpSV.Controls.Add(this.groupBox7);
            this.tbpSV.Controls.Add(this.flpSVResult);
            this.tbpSV.Location = new System.Drawing.Point(4, 22);
            this.tbpSV.Margin = new System.Windows.Forms.Padding(2);
            this.tbpSV.Name = "tbpSV";
            this.tbpSV.Padding = new System.Windows.Forms.Padding(2);
            this.tbpSV.Size = new System.Drawing.Size(928, 261);
            this.tbpSV.TabIndex = 3;
            this.tbpSV.Text = "SV,DV";
            this.tbpSV.UseVisualStyleBackColor = true;
            // 
            // chkContinueUpdateSV
            // 
            this.chkContinueUpdateSV.AutoSize = true;
            this.chkContinueUpdateSV.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkContinueUpdateSV.Location = new System.Drawing.Point(686, 18);
            this.chkContinueUpdateSV.Margin = new System.Windows.Forms.Padding(2);
            this.chkContinueUpdateSV.Name = "chkContinueUpdateSV";
            this.chkContinueUpdateSV.Size = new System.Drawing.Size(140, 21);
            this.chkContinueUpdateSV.TabIndex = 8;
            this.chkContinueUpdateSV.Text = "ContinueUpdateSV";
            this.chkContinueUpdateSV.UseVisualStyleBackColor = true;
            this.chkContinueUpdateSV.CheckedChanged += new System.EventHandler(this.chkContinueUpdateSV_CheckedChanged);
            // 
            // btnUpdateListTypeSV
            // 
            this.btnUpdateListTypeSV.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateListTypeSV.Location = new System.Drawing.Point(4, 139);
            this.btnUpdateListTypeSV.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdateListTypeSV.Name = "btnUpdateListTypeSV";
            this.btnUpdateListTypeSV.Size = new System.Drawing.Size(142, 59);
            this.btnUpdateListTypeSV.TabIndex = 6;
            this.btnUpdateListTypeSV.Text = "UpdateListTypeSV  (3004)";
            this.btnUpdateListTypeSV.UseVisualStyleBackColor = true;
            this.btnUpdateListTypeSV.Click += new System.EventHandler(this.btnUpdateListTypeSV_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnGetSV);
            this.groupBox8.Controls.Add(this.txtGetSV);
            this.groupBox8.Controls.Add(this.lblGetSV);
            this.groupBox8.Controls.Add(this.txtGetSVFormat);
            this.groupBox8.Controls.Add(this.lblGetSVFormat);
            this.groupBox8.Controls.Add(this.txtGetSVID);
            this.groupBox8.Controls.Add(this.lblGetSVID);
            this.groupBox8.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(325, 5);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox8.Size = new System.Drawing.Size(308, 130);
            this.groupBox8.TabIndex = 5;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Get SV,DV";
            // 
            // btnGetSV
            // 
            this.btnGetSV.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetSV.Location = new System.Drawing.Point(182, 22);
            this.btnGetSV.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetSV.Name = "btnGetSV";
            this.btnGetSV.Size = new System.Drawing.Size(114, 59);
            this.btnGetSV.TabIndex = 11;
            this.btnGetSV.Text = "GetSV";
            this.btnGetSV.UseVisualStyleBackColor = true;
            this.btnGetSV.Click += new System.EventHandler(this.btnGetSV_Click);
            // 
            // txtGetSV
            // 
            this.txtGetSV.Location = new System.Drawing.Point(102, 92);
            this.txtGetSV.Margin = new System.Windows.Forms.Padding(2);
            this.txtGetSV.Name = "txtGetSV";
            this.txtGetSV.ReadOnly = true;
            this.txtGetSV.Size = new System.Drawing.Size(194, 27);
            this.txtGetSV.TabIndex = 10;
            // 
            // lblGetSV
            // 
            this.lblGetSV.AutoSize = true;
            this.lblGetSV.Location = new System.Drawing.Point(4, 94);
            this.lblGetSV.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGetSV.Name = "lblGetSV";
            this.lblGetSV.Size = new System.Drawing.Size(63, 19);
            this.lblGetSV.TabIndex = 9;
            this.lblGetSV.Text = "Value：";
            // 
            // txtGetSVFormat
            // 
            this.txtGetSVFormat.Location = new System.Drawing.Point(102, 56);
            this.txtGetSVFormat.Margin = new System.Windows.Forms.Padding(2);
            this.txtGetSVFormat.Name = "txtGetSVFormat";
            this.txtGetSVFormat.ReadOnly = true;
            this.txtGetSVFormat.Size = new System.Drawing.Size(76, 27);
            this.txtGetSVFormat.TabIndex = 8;
            // 
            // lblGetSVFormat
            // 
            this.lblGetSVFormat.AutoSize = true;
            this.lblGetSVFormat.Location = new System.Drawing.Point(4, 61);
            this.lblGetSVFormat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGetSVFormat.Name = "lblGetSVFormat";
            this.lblGetSVFormat.Size = new System.Drawing.Size(75, 19);
            this.lblGetSVFormat.TabIndex = 7;
            this.lblGetSVFormat.Text = "Format：";
            // 
            // txtGetSVID
            // 
            this.txtGetSVID.Location = new System.Drawing.Point(102, 22);
            this.txtGetSVID.Margin = new System.Windows.Forms.Padding(2);
            this.txtGetSVID.MaxLength = 10;
            this.txtGetSVID.Name = "txtGetSVID";
            this.txtGetSVID.Size = new System.Drawing.Size(76, 27);
            this.txtGetSVID.TabIndex = 6;
            // 
            // lblGetSVID
            // 
            this.lblGetSVID.AutoSize = true;
            this.lblGetSVID.Location = new System.Drawing.Point(4, 28);
            this.lblGetSVID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGetSVID.Name = "lblGetSVID";
            this.lblGetSVID.Size = new System.Drawing.Size(100, 19);
            this.lblGetSVID.TabIndex = 0;
            this.lblGetSVID.Text = "SVID(DVID)：";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtUpdateSV);
            this.groupBox7.Controls.Add(this.lblUpdateSV);
            this.groupBox7.Controls.Add(this.lblUpdateSVFormat);
            this.groupBox7.Controls.Add(this.cmbUpdateSVFormat);
            this.groupBox7.Controls.Add(this.txtUpdateSVID);
            this.groupBox7.Controls.Add(this.lblUpdateSVID);
            this.groupBox7.Controls.Add(this.btnUpdateSV);
            this.groupBox7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(4, 5);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox7.Size = new System.Drawing.Size(316, 130);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Update SV,DV";
            // 
            // txtUpdateSV
            // 
            this.txtUpdateSV.Location = new System.Drawing.Point(110, 92);
            this.txtUpdateSV.Margin = new System.Windows.Forms.Padding(2);
            this.txtUpdateSV.MaxLength = 500000;
            this.txtUpdateSV.Name = "txtUpdateSV";
            this.txtUpdateSV.Size = new System.Drawing.Size(194, 27);
            this.txtUpdateSV.TabIndex = 9;
            // 
            // lblUpdateSV
            // 
            this.lblUpdateSV.AutoSize = true;
            this.lblUpdateSV.Location = new System.Drawing.Point(4, 92);
            this.lblUpdateSV.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUpdateSV.Name = "lblUpdateSV";
            this.lblUpdateSV.Size = new System.Drawing.Size(63, 19);
            this.lblUpdateSV.TabIndex = 8;
            this.lblUpdateSV.Text = "Value：";
            // 
            // lblUpdateSVFormat
            // 
            this.lblUpdateSVFormat.AutoSize = true;
            this.lblUpdateSVFormat.Location = new System.Drawing.Point(4, 61);
            this.lblUpdateSVFormat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUpdateSVFormat.Name = "lblUpdateSVFormat";
            this.lblUpdateSVFormat.Size = new System.Drawing.Size(75, 19);
            this.lblUpdateSVFormat.TabIndex = 7;
            this.lblUpdateSVFormat.Text = "Format：";
            // 
            // cmbUpdateSVFormat
            // 
            this.cmbUpdateSVFormat.FormattingEnabled = true;
            this.cmbUpdateSVFormat.Items.AddRange(new object[] {
            "Boolean",
            "B",
            "A",
            "U1",
            "U2",
            "U4",
            "U8",
            "I1",
            "I2",
            "I4",
            "I8",
            "F4",
            "F8"});
            this.cmbUpdateSVFormat.Location = new System.Drawing.Point(110, 58);
            this.cmbUpdateSVFormat.Margin = new System.Windows.Forms.Padding(2);
            this.cmbUpdateSVFormat.Name = "cmbUpdateSVFormat";
            this.cmbUpdateSVFormat.Size = new System.Drawing.Size(76, 27);
            this.cmbUpdateSVFormat.TabIndex = 6;
            // 
            // txtUpdateSVID
            // 
            this.txtUpdateSVID.Location = new System.Drawing.Point(110, 25);
            this.txtUpdateSVID.Margin = new System.Windows.Forms.Padding(2);
            this.txtUpdateSVID.MaxLength = 10;
            this.txtUpdateSVID.Name = "txtUpdateSVID";
            this.txtUpdateSVID.Size = new System.Drawing.Size(76, 27);
            this.txtUpdateSVID.TabIndex = 5;
            // 
            // lblUpdateSVID
            // 
            this.lblUpdateSVID.AutoSize = true;
            this.lblUpdateSVID.Location = new System.Drawing.Point(4, 28);
            this.lblUpdateSVID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUpdateSVID.Name = "lblUpdateSVID";
            this.lblUpdateSVID.Size = new System.Drawing.Size(100, 19);
            this.lblUpdateSVID.TabIndex = 4;
            this.lblUpdateSVID.Text = "SVID(DVID)：";
            // 
            // btnUpdateSV
            // 
            this.btnUpdateSV.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateSV.Location = new System.Drawing.Point(190, 25);
            this.btnUpdateSV.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdateSV.Name = "btnUpdateSV";
            this.btnUpdateSV.Size = new System.Drawing.Size(114, 59);
            this.btnUpdateSV.TabIndex = 3;
            this.btnUpdateSV.Text = "UpdateSV";
            this.btnUpdateSV.UseVisualStyleBackColor = true;
            this.btnUpdateSV.Click += new System.EventHandler(this.btnUpdateSV_Click);
            // 
            // flpSVResult
            // 
            this.flpSVResult.Controls.Add(this.lblSVResultCaption);
            this.flpSVResult.Controls.Add(this.lblSVResultText);
            this.flpSVResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpSVResult.Location = new System.Drawing.Point(2, 222);
            this.flpSVResult.Margin = new System.Windows.Forms.Padding(2);
            this.flpSVResult.Name = "flpSVResult";
            this.flpSVResult.Size = new System.Drawing.Size(924, 37);
            this.flpSVResult.TabIndex = 2;
            // 
            // lblSVResultCaption
            // 
            this.lblSVResultCaption.AutoSize = true;
            this.lblSVResultCaption.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSVResultCaption.Location = new System.Drawing.Point(2, 8);
            this.lblSVResultCaption.Margin = new System.Windows.Forms.Padding(2, 8, 2, 2);
            this.lblSVResultCaption.Name = "lblSVResultCaption";
            this.lblSVResultCaption.Size = new System.Drawing.Size(153, 19);
            this.lblSVResultCaption.TabIndex = 0;
            this.lblSVResultCaption.Text = "SV Command Result :";
            // 
            // lblSVResultText
            // 
            this.lblSVResultText.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSVResultText.Location = new System.Drawing.Point(159, 8);
            this.lblSVResultText.Margin = new System.Windows.Forms.Padding(2, 8, 2, 2);
            this.lblSVResultText.Name = "lblSVResultText";
            this.lblSVResultText.Size = new System.Drawing.Size(193, 18);
            this.lblSVResultText.TabIndex = 1;
            this.lblSVResultText.Text = "Result";
            this.lblSVResultText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbpEC
            // 
            this.tbpEC.Controls.Add(this.groupBox24);
            this.tbpEC.Controls.Add(this.groupBox9);
            this.tbpEC.Controls.Add(this.groupBox10);
            this.tbpEC.Controls.Add(this.flpECResult);
            this.tbpEC.Location = new System.Drawing.Point(4, 22);
            this.tbpEC.Margin = new System.Windows.Forms.Padding(2);
            this.tbpEC.Name = "tbpEC";
            this.tbpEC.Padding = new System.Windows.Forms.Padding(2);
            this.tbpEC.Size = new System.Drawing.Size(928, 261);
            this.tbpEC.TabIndex = 4;
            this.tbpEC.Text = "EC";
            this.tbpEC.UseVisualStyleBackColor = true;
            // 
            // groupBox24
            // 
            this.groupBox24.Controls.Add(this.btnNewECsReply);
            this.groupBox24.Controls.Add(this.txtEAC);
            this.groupBox24.Controls.Add(this.lblEAC);
            this.groupBox24.Controls.Add(this.txtNewECsSystemBytes);
            this.groupBox24.Controls.Add(this.lblNewECsSystemBytes);
            this.groupBox24.Controls.Add(this.dgvNewECs);
            this.groupBox24.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox24.Location = new System.Drawing.Point(492, 9);
            this.groupBox24.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox24.Size = new System.Drawing.Size(345, 190);
            this.groupBox24.TabIndex = 8;
            this.groupBox24.TabStop = false;
            this.groupBox24.Text = "New EC Download";
            // 
            // btnNewECsReply
            // 
            this.btnNewECsReply.Location = new System.Drawing.Point(274, 131);
            this.btnNewECsReply.Margin = new System.Windows.Forms.Padding(2);
            this.btnNewECsReply.Name = "btnNewECsReply";
            this.btnNewECsReply.Size = new System.Drawing.Size(66, 54);
            this.btnNewECsReply.TabIndex = 5;
            this.btnNewECsReply.Text = "Reply";
            this.btnNewECsReply.UseVisualStyleBackColor = true;
            this.btnNewECsReply.Click += new System.EventHandler(this.btnNewECsReply_Click);
            // 
            // txtEAC
            // 
            this.txtEAC.Location = new System.Drawing.Point(214, 139);
            this.txtEAC.Margin = new System.Windows.Forms.Padding(2);
            this.txtEAC.Name = "txtEAC";
            this.txtEAC.Size = new System.Drawing.Size(44, 27);
            this.txtEAC.TabIndex = 4;
            this.txtEAC.Text = "0";
            // 
            // lblEAC
            // 
            this.lblEAC.AutoSize = true;
            this.lblEAC.Location = new System.Drawing.Point(170, 142);
            this.lblEAC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEAC.Name = "lblEAC";
            this.lblEAC.Size = new System.Drawing.Size(52, 19);
            this.lblEAC.TabIndex = 3;
            this.lblEAC.Text = "EAC：";
            // 
            // txtNewECsSystemBytes
            // 
            this.txtNewECsSystemBytes.Location = new System.Drawing.Point(110, 139);
            this.txtNewECsSystemBytes.Margin = new System.Windows.Forms.Padding(2);
            this.txtNewECsSystemBytes.Name = "txtNewECsSystemBytes";
            this.txtNewECsSystemBytes.Size = new System.Drawing.Size(56, 27);
            this.txtNewECsSystemBytes.TabIndex = 2;
            this.txtNewECsSystemBytes.Text = "0";
            // 
            // lblNewECsSystemBytes
            // 
            this.lblNewECsSystemBytes.AutoSize = true;
            this.lblNewECsSystemBytes.Location = new System.Drawing.Point(4, 142);
            this.lblNewECsSystemBytes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNewECsSystemBytes.Name = "lblNewECsSystemBytes";
            this.lblNewECsSystemBytes.Size = new System.Drawing.Size(112, 19);
            this.lblNewECsSystemBytes.TabIndex = 1;
            this.lblNewECsSystemBytes.Text = "SystemBytes：";
            // 
            // dgvNewECs
            // 
            this.dgvNewECs.AllowUserToAddRows = false;
            this.dgvNewECs.AllowUserToDeleteRows = false;
            this.dgvNewECs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNewECs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Format,
            this.Value});
            this.dgvNewECs.Location = new System.Drawing.Point(4, 25);
            this.dgvNewECs.Margin = new System.Windows.Forms.Padding(2);
            this.dgvNewECs.Name = "dgvNewECs";
            this.dgvNewECs.RowHeadersWidth = 51;
            this.dgvNewECs.RowTemplate.Height = 27;
            this.dgvNewECs.Size = new System.Drawing.Size(336, 102);
            this.dgvNewECs.TabIndex = 0;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 6;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 125;
            // 
            // Format
            // 
            this.Format.HeaderText = "Format";
            this.Format.MinimumWidth = 6;
            this.Format.Name = "Format";
            this.Format.ReadOnly = true;
            this.Format.Width = 125;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.MinimumWidth = 6;
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            this.Value.Width = 125;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnGetEC);
            this.groupBox9.Controls.Add(this.txtGetEC);
            this.groupBox9.Controls.Add(this.lblGetEC);
            this.groupBox9.Controls.Add(this.txtGetECFormat);
            this.groupBox9.Controls.Add(this.lblGetECFormat);
            this.groupBox9.Controls.Add(this.txtGetECID);
            this.groupBox9.Controls.Add(this.lblGetECID);
            this.groupBox9.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox9.Location = new System.Drawing.Point(262, 5);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox9.Size = new System.Drawing.Size(226, 130);
            this.groupBox9.TabIndex = 7;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Get EC";
            // 
            // btnGetEC
            // 
            this.btnGetEC.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetEC.Location = new System.Drawing.Point(158, 22);
            this.btnGetEC.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetEC.Name = "btnGetEC";
            this.btnGetEC.Size = new System.Drawing.Size(59, 59);
            this.btnGetEC.TabIndex = 11;
            this.btnGetEC.Text = "GetEC";
            this.btnGetEC.UseVisualStyleBackColor = true;
            this.btnGetEC.Click += new System.EventHandler(this.btnGetEC_Click);
            // 
            // txtGetEC
            // 
            this.txtGetEC.Location = new System.Drawing.Point(78, 92);
            this.txtGetEC.Margin = new System.Windows.Forms.Padding(2);
            this.txtGetEC.Name = "txtGetEC";
            this.txtGetEC.ReadOnly = true;
            this.txtGetEC.Size = new System.Drawing.Size(140, 27);
            this.txtGetEC.TabIndex = 10;
            // 
            // lblGetEC
            // 
            this.lblGetEC.AutoSize = true;
            this.lblGetEC.Location = new System.Drawing.Point(4, 94);
            this.lblGetEC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGetEC.Name = "lblGetEC";
            this.lblGetEC.Size = new System.Drawing.Size(63, 19);
            this.lblGetEC.TabIndex = 9;
            this.lblGetEC.Text = "Value：";
            // 
            // txtGetECFormat
            // 
            this.txtGetECFormat.Location = new System.Drawing.Point(78, 56);
            this.txtGetECFormat.Margin = new System.Windows.Forms.Padding(2);
            this.txtGetECFormat.Name = "txtGetECFormat";
            this.txtGetECFormat.ReadOnly = true;
            this.txtGetECFormat.Size = new System.Drawing.Size(76, 27);
            this.txtGetECFormat.TabIndex = 8;
            // 
            // lblGetECFormat
            // 
            this.lblGetECFormat.AutoSize = true;
            this.lblGetECFormat.Location = new System.Drawing.Point(4, 61);
            this.lblGetECFormat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGetECFormat.Name = "lblGetECFormat";
            this.lblGetECFormat.Size = new System.Drawing.Size(75, 19);
            this.lblGetECFormat.TabIndex = 7;
            this.lblGetECFormat.Text = "Format：";
            // 
            // txtGetECID
            // 
            this.txtGetECID.Location = new System.Drawing.Point(78, 22);
            this.txtGetECID.Margin = new System.Windows.Forms.Padding(2);
            this.txtGetECID.MaxLength = 10;
            this.txtGetECID.Name = "txtGetECID";
            this.txtGetECID.Size = new System.Drawing.Size(76, 27);
            this.txtGetECID.TabIndex = 6;
            // 
            // lblGetECID
            // 
            this.lblGetECID.AutoSize = true;
            this.lblGetECID.Location = new System.Drawing.Point(4, 28);
            this.lblGetECID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGetECID.Name = "lblGetECID";
            this.lblGetECID.Size = new System.Drawing.Size(56, 19);
            this.lblGetECID.TabIndex = 0;
            this.lblGetECID.Text = "ECID：";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.txtUpdateEC);
            this.groupBox10.Controls.Add(this.lblUpdateEC);
            this.groupBox10.Controls.Add(this.lblUpdateECFormat);
            this.groupBox10.Controls.Add(this.cmbUpdateECFormat);
            this.groupBox10.Controls.Add(this.txtUpdateECID);
            this.groupBox10.Controls.Add(this.lblUpdateECID);
            this.groupBox10.Controls.Add(this.btnUpdateEC);
            this.groupBox10.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox10.Location = new System.Drawing.Point(4, 5);
            this.groupBox10.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox10.Size = new System.Drawing.Size(253, 130);
            this.groupBox10.TabIndex = 6;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Update EC";
            // 
            // txtUpdateEC
            // 
            this.txtUpdateEC.Location = new System.Drawing.Point(82, 92);
            this.txtUpdateEC.Margin = new System.Windows.Forms.Padding(2);
            this.txtUpdateEC.Name = "txtUpdateEC";
            this.txtUpdateEC.Size = new System.Drawing.Size(164, 27);
            this.txtUpdateEC.TabIndex = 9;
            // 
            // lblUpdateEC
            // 
            this.lblUpdateEC.AutoSize = true;
            this.lblUpdateEC.Location = new System.Drawing.Point(4, 92);
            this.lblUpdateEC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUpdateEC.Name = "lblUpdateEC";
            this.lblUpdateEC.Size = new System.Drawing.Size(63, 19);
            this.lblUpdateEC.TabIndex = 8;
            this.lblUpdateEC.Text = "Value：";
            // 
            // lblUpdateECFormat
            // 
            this.lblUpdateECFormat.AutoSize = true;
            this.lblUpdateECFormat.Location = new System.Drawing.Point(4, 61);
            this.lblUpdateECFormat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUpdateECFormat.Name = "lblUpdateECFormat";
            this.lblUpdateECFormat.Size = new System.Drawing.Size(75, 19);
            this.lblUpdateECFormat.TabIndex = 7;
            this.lblUpdateECFormat.Text = "Format：";
            // 
            // cmbUpdateECFormat
            // 
            this.cmbUpdateECFormat.FormattingEnabled = true;
            this.cmbUpdateECFormat.Items.AddRange(new object[] {
            "Boolean",
            "B",
            "A",
            "U1",
            "U2",
            "U4",
            "U8",
            "I1",
            "I2",
            "I4",
            "I8",
            "F4",
            "F8"});
            this.cmbUpdateECFormat.Location = new System.Drawing.Point(82, 58);
            this.cmbUpdateECFormat.Margin = new System.Windows.Forms.Padding(2);
            this.cmbUpdateECFormat.Name = "cmbUpdateECFormat";
            this.cmbUpdateECFormat.Size = new System.Drawing.Size(76, 27);
            this.cmbUpdateECFormat.TabIndex = 6;
            // 
            // txtUpdateECID
            // 
            this.txtUpdateECID.Location = new System.Drawing.Point(82, 25);
            this.txtUpdateECID.Margin = new System.Windows.Forms.Padding(2);
            this.txtUpdateECID.MaxLength = 10;
            this.txtUpdateECID.Name = "txtUpdateECID";
            this.txtUpdateECID.Size = new System.Drawing.Size(76, 27);
            this.txtUpdateECID.TabIndex = 5;
            // 
            // lblUpdateECID
            // 
            this.lblUpdateECID.AutoSize = true;
            this.lblUpdateECID.Location = new System.Drawing.Point(4, 28);
            this.lblUpdateECID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUpdateECID.Name = "lblUpdateECID";
            this.lblUpdateECID.Size = new System.Drawing.Size(56, 19);
            this.lblUpdateECID.TabIndex = 4;
            this.lblUpdateECID.Text = "ECID：";
            // 
            // btnUpdateEC
            // 
            this.btnUpdateEC.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateEC.Location = new System.Drawing.Point(162, 25);
            this.btnUpdateEC.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdateEC.Name = "btnUpdateEC";
            this.btnUpdateEC.Size = new System.Drawing.Size(83, 59);
            this.btnUpdateEC.TabIndex = 3;
            this.btnUpdateEC.Text = "UpdateEC";
            this.btnUpdateEC.UseVisualStyleBackColor = true;
            this.btnUpdateEC.Click += new System.EventHandler(this.btnUpdateEC_Click);
            // 
            // flpECResult
            // 
            this.flpECResult.Controls.Add(this.lblECResultCaption);
            this.flpECResult.Controls.Add(this.lblECResultText);
            this.flpECResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpECResult.Location = new System.Drawing.Point(2, 222);
            this.flpECResult.Margin = new System.Windows.Forms.Padding(2);
            this.flpECResult.Name = "flpECResult";
            this.flpECResult.Size = new System.Drawing.Size(924, 37);
            this.flpECResult.TabIndex = 3;
            // 
            // lblECResultCaption
            // 
            this.lblECResultCaption.AutoSize = true;
            this.lblECResultCaption.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblECResultCaption.Location = new System.Drawing.Point(2, 8);
            this.lblECResultCaption.Margin = new System.Windows.Forms.Padding(2, 8, 2, 2);
            this.lblECResultCaption.Name = "lblECResultCaption";
            this.lblECResultCaption.Size = new System.Drawing.Size(152, 19);
            this.lblECResultCaption.TabIndex = 0;
            this.lblECResultCaption.Text = "EC Command Result :";
            // 
            // lblECResultText
            // 
            this.lblECResultText.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblECResultText.Location = new System.Drawing.Point(158, 8);
            this.lblECResultText.Margin = new System.Windows.Forms.Padding(2, 8, 2, 2);
            this.lblECResultText.Name = "lblECResultText";
            this.lblECResultText.Size = new System.Drawing.Size(193, 18);
            this.lblECResultText.TabIndex = 1;
            this.lblECResultText.Text = "Result";
            this.lblECResultText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbpRemote
            // 
            this.tbpRemote.Controls.Add(this.groupBox11);
            this.tbpRemote.Location = new System.Drawing.Point(4, 22);
            this.tbpRemote.Margin = new System.Windows.Forms.Padding(2);
            this.tbpRemote.Name = "tbpRemote";
            this.tbpRemote.Padding = new System.Windows.Forms.Padding(2);
            this.tbpRemote.Size = new System.Drawing.Size(928, 261);
            this.tbpRemote.TabIndex = 5;
            this.tbpRemote.Text = "Remote";
            this.tbpRemote.UseVisualStyleBackColor = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.lblReplyHcAck);
            this.groupBox11.Controls.Add(this.lblCPs);
            this.groupBox11.Controls.Add(this.dgvCPs);
            this.groupBox11.Controls.Add(this.lblSystemBytes);
            this.groupBox11.Controls.Add(this.lblRCMD);
            this.groupBox11.Controls.Add(this.txtReplyHcAck);
            this.groupBox11.Controls.Add(this.txtSystemBytes);
            this.groupBox11.Controls.Add(this.txtRCMD);
            this.groupBox11.Controls.Add(this.txtMessageName);
            this.groupBox11.Controls.Add(this.txtOBJSPEC);
            this.groupBox11.Controls.Add(this.lblMessageName);
            this.groupBox11.Controls.Add(this.lblOBJSPEC);
            this.groupBox11.Controls.Add(this.btnReplyMessage);
            this.groupBox11.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox11.Location = new System.Drawing.Point(4, 5);
            this.groupBox11.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox11.Size = new System.Drawing.Size(835, 197);
            this.groupBox11.TabIndex = 7;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Receive Message";
            // 
            // lblReplyHcAck
            // 
            this.lblReplyHcAck.AutoSize = true;
            this.lblReplyHcAck.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReplyHcAck.Location = new System.Drawing.Point(645, 19);
            this.lblReplyHcAck.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblReplyHcAck.Name = "lblReplyHcAck";
            this.lblReplyHcAck.Size = new System.Drawing.Size(106, 19);
            this.lblReplyHcAck.TabIndex = 8;
            this.lblReplyHcAck.Text = "ReplyHcAck：";
            // 
            // lblCPs
            // 
            this.lblCPs.AutoSize = true;
            this.lblCPs.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCPs.Location = new System.Drawing.Point(292, 19);
            this.lblCPs.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCPs.Name = "lblCPs";
            this.lblCPs.Size = new System.Drawing.Size(116, 19);
            this.lblCPs.TabIndex = 8;
            this.lblCPs.Text = "ReplyCPs Ack：";
            // 
            // dgvCPs
            // 
            this.dgvCPs.AllowUserToAddRows = false;
            this.dgvCPs.AllowUserToDeleteRows = false;
            this.dgvCPs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCPs.Location = new System.Drawing.Point(292, 50);
            this.dgvCPs.Margin = new System.Windows.Forms.Padding(2);
            this.dgvCPs.Name = "dgvCPs";
            this.dgvCPs.RowHeadersWidth = 51;
            this.dgvCPs.RowTemplate.Height = 27;
            this.dgvCPs.Size = new System.Drawing.Size(345, 142);
            this.dgvCPs.TabIndex = 8;
            // 
            // lblSystemBytes
            // 
            this.lblSystemBytes.AutoSize = true;
            this.lblSystemBytes.Location = new System.Drawing.Point(4, 116);
            this.lblSystemBytes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSystemBytes.Name = "lblSystemBytes";
            this.lblSystemBytes.Size = new System.Drawing.Size(112, 19);
            this.lblSystemBytes.TabIndex = 7;
            this.lblSystemBytes.Text = "SystemBytes：";
            // 
            // lblRCMD
            // 
            this.lblRCMD.AutoSize = true;
            this.lblRCMD.Location = new System.Drawing.Point(4, 86);
            this.lblRCMD.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRCMD.Name = "lblRCMD";
            this.lblRCMD.Size = new System.Drawing.Size(67, 19);
            this.lblRCMD.TabIndex = 7;
            this.lblRCMD.Text = "RCMD：";
            // 
            // txtReplyHcAck
            // 
            this.txtReplyHcAck.Location = new System.Drawing.Point(754, 19);
            this.txtReplyHcAck.Margin = new System.Windows.Forms.Padding(2);
            this.txtReplyHcAck.MaxLength = 10;
            this.txtReplyHcAck.Name = "txtReplyHcAck";
            this.txtReplyHcAck.Size = new System.Drawing.Size(76, 27);
            this.txtReplyHcAck.TabIndex = 5;
            this.txtReplyHcAck.Text = "0";
            // 
            // txtSystemBytes
            // 
            this.txtSystemBytes.Enabled = false;
            this.txtSystemBytes.Location = new System.Drawing.Point(127, 110);
            this.txtSystemBytes.Margin = new System.Windows.Forms.Padding(2);
            this.txtSystemBytes.MaxLength = 10;
            this.txtSystemBytes.Name = "txtSystemBytes";
            this.txtSystemBytes.Size = new System.Drawing.Size(155, 27);
            this.txtSystemBytes.TabIndex = 5;
            this.txtSystemBytes.Text = "0";
            // 
            // txtRCMD
            // 
            this.txtRCMD.Enabled = false;
            this.txtRCMD.Location = new System.Drawing.Point(127, 79);
            this.txtRCMD.Margin = new System.Windows.Forms.Padding(2);
            this.txtRCMD.MaxLength = 10;
            this.txtRCMD.Name = "txtRCMD";
            this.txtRCMD.Size = new System.Drawing.Size(155, 27);
            this.txtRCMD.TabIndex = 5;
            // 
            // txtMessageName
            // 
            this.txtMessageName.Enabled = false;
            this.txtMessageName.Location = new System.Drawing.Point(127, 19);
            this.txtMessageName.Margin = new System.Windows.Forms.Padding(2);
            this.txtMessageName.MaxLength = 10;
            this.txtMessageName.Name = "txtMessageName";
            this.txtMessageName.Size = new System.Drawing.Size(155, 27);
            this.txtMessageName.TabIndex = 5;
            // 
            // txtOBJSPEC
            // 
            this.txtOBJSPEC.Enabled = false;
            this.txtOBJSPEC.Location = new System.Drawing.Point(127, 50);
            this.txtOBJSPEC.Margin = new System.Windows.Forms.Padding(2);
            this.txtOBJSPEC.MaxLength = 10;
            this.txtOBJSPEC.Name = "txtOBJSPEC";
            this.txtOBJSPEC.Size = new System.Drawing.Size(155, 27);
            this.txtOBJSPEC.TabIndex = 5;
            // 
            // lblMessageName
            // 
            this.lblMessageName.AutoSize = true;
            this.lblMessageName.Location = new System.Drawing.Point(4, 22);
            this.lblMessageName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMessageName.Name = "lblMessageName";
            this.lblMessageName.Size = new System.Drawing.Size(94, 19);
            this.lblMessageName.TabIndex = 4;
            this.lblMessageName.Text = "MsgName：";
            // 
            // lblOBJSPEC
            // 
            this.lblOBJSPEC.AutoSize = true;
            this.lblOBJSPEC.Location = new System.Drawing.Point(4, 53);
            this.lblOBJSPEC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOBJSPEC.Name = "lblOBJSPEC";
            this.lblOBJSPEC.Size = new System.Drawing.Size(84, 19);
            this.lblOBJSPEC.TabIndex = 4;
            this.lblOBJSPEC.Text = "OBJSPEC：";
            // 
            // btnReplyMessage
            // 
            this.btnReplyMessage.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReplyMessage.Location = new System.Drawing.Point(716, 133);
            this.btnReplyMessage.Margin = new System.Windows.Forms.Padding(2);
            this.btnReplyMessage.Name = "btnReplyMessage";
            this.btnReplyMessage.Size = new System.Drawing.Size(114, 59);
            this.btnReplyMessage.TabIndex = 3;
            this.btnReplyMessage.Text = "ReplyMsg";
            this.btnReplyMessage.UseVisualStyleBackColor = true;
            this.btnReplyMessage.Click += new System.EventHandler(this.btnReplyMessage_Click);
            // 
            // tabEnhanced
            // 
            this.tabEnhanced.Controls.Add(this.groupBox28);
            this.tabEnhanced.Location = new System.Drawing.Point(4, 22);
            this.tabEnhanced.Margin = new System.Windows.Forms.Padding(2);
            this.tabEnhanced.Name = "tabEnhanced";
            this.tabEnhanced.Size = new System.Drawing.Size(928, 261);
            this.tabEnhanced.TabIndex = 10;
            this.tabEnhanced.Text = "Enhanced Remote";
            this.tabEnhanced.UseVisualStyleBackColor = true;
            // 
            // groupBox28
            // 
            this.groupBox28.Controls.Add(this.txtEnhancedRemoteDataView);
            this.groupBox28.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox28.Location = new System.Drawing.Point(4, 10);
            this.groupBox28.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox28.Size = new System.Drawing.Size(514, 228);
            this.groupBox28.TabIndex = 11;
            this.groupBox28.TabStop = false;
            this.groupBox28.Text = "Recv Enhanced Use Keep Data View";
            // 
            // txtEnhancedRemoteDataView
            // 
            this.txtEnhancedRemoteDataView.Location = new System.Drawing.Point(4, 25);
            this.txtEnhancedRemoteDataView.Margin = new System.Windows.Forms.Padding(2);
            this.txtEnhancedRemoteDataView.MaxLength = 10;
            this.txtEnhancedRemoteDataView.Multiline = true;
            this.txtEnhancedRemoteDataView.Name = "txtEnhancedRemoteDataView";
            this.txtEnhancedRemoteDataView.ReadOnly = true;
            this.txtEnhancedRemoteDataView.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtEnhancedRemoteDataView.Size = new System.Drawing.Size(492, 199);
            this.txtEnhancedRemoteDataView.TabIndex = 5;
            this.txtEnhancedRemoteDataView.WordWrap = false;
            // 
            // tabPP
            // 
            this.tabPP.Controls.Add(this.groupBox22);
            this.tabPP.Controls.Add(this.lvPPIDs);
            this.tabPP.Controls.Add(this.txtPPBody_Unformatted);
            this.tabPP.Controls.Add(this.txtPPBodyPPID);
            this.tabPP.Controls.Add(this.lblPPBodyPPID);
            this.tabPP.Controls.Add(this.groupBox26);
            this.tabPP.Controls.Add(this.btnSendS7F25);
            this.tabPP.Controls.Add(this.btnSendS7F23);
            this.tabPP.Controls.Add(this.btnSendS7F5);
            this.tabPP.Controls.Add(this.btnSendS7F3);
            this.tabPP.Controls.Add(this.btnPPChanged);
            this.tabPP.Controls.Add(this.cmbChangedPPType);
            this.tabPP.Controls.Add(this.groupBox25);
            this.tabPP.Controls.Add(this.lblPPIDList);
            this.tabPP.Location = new System.Drawing.Point(4, 22);
            this.tabPP.Margin = new System.Windows.Forms.Padding(2);
            this.tabPP.Name = "tabPP";
            this.tabPP.Padding = new System.Windows.Forms.Padding(2);
            this.tabPP.Size = new System.Drawing.Size(928, 261);
            this.tabPP.TabIndex = 9;
            this.tabPP.Text = "ProcessProgram";
            this.tabPP.UseVisualStyleBackColor = true;
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.lblS7Systembytes);
            this.groupBox22.Controls.Add(this.txtS7Systembytes);
            this.groupBox22.Controls.Add(this.lblAckc7);
            this.groupBox22.Controls.Add(this.txtReceivedPPID);
            this.groupBox22.Controls.Add(this.txtAckc7);
            this.groupBox22.Controls.Add(this.lblReceivedPPID);
            this.groupBox22.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold);
            this.groupBox22.Location = new System.Drawing.Point(694, 10);
            this.groupBox22.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox22.Size = new System.Drawing.Size(143, 221);
            this.groupBox22.TabIndex = 24;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "Received Host Data";
            // 
            // lblS7Systembytes
            // 
            this.lblS7Systembytes.AutoSize = true;
            this.lblS7Systembytes.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblS7Systembytes.Location = new System.Drawing.Point(14, 18);
            this.lblS7Systembytes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblS7Systembytes.Name = "lblS7Systembytes";
            this.lblS7Systembytes.Size = new System.Drawing.Size(112, 19);
            this.lblS7Systembytes.TabIndex = 18;
            this.lblS7Systembytes.Text = "SystemBytes：";
            // 
            // txtS7Systembytes
            // 
            this.txtS7Systembytes.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtS7Systembytes.Location = new System.Drawing.Point(20, 42);
            this.txtS7Systembytes.Margin = new System.Windows.Forms.Padding(2);
            this.txtS7Systembytes.Name = "txtS7Systembytes";
            this.txtS7Systembytes.ReadOnly = true;
            this.txtS7Systembytes.Size = new System.Drawing.Size(90, 27);
            this.txtS7Systembytes.TabIndex = 19;
            // 
            // lblAckc7
            // 
            this.lblAckc7.AutoSize = true;
            this.lblAckc7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblAckc7.Location = new System.Drawing.Point(35, 82);
            this.lblAckc7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAckc7.Name = "lblAckc7";
            this.lblAckc7.Size = new System.Drawing.Size(66, 19);
            this.lblAckc7.TabIndex = 17;
            this.lblAckc7.Text = "Ackc7：";
            // 
            // txtReceivedPPID
            // 
            this.txtReceivedPPID.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtReceivedPPID.Location = new System.Drawing.Point(20, 176);
            this.txtReceivedPPID.Margin = new System.Windows.Forms.Padding(2);
            this.txtReceivedPPID.Name = "txtReceivedPPID";
            this.txtReceivedPPID.ReadOnly = true;
            this.txtReceivedPPID.Size = new System.Drawing.Size(90, 27);
            this.txtReceivedPPID.TabIndex = 22;
            // 
            // txtAckc7
            // 
            this.txtAckc7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtAckc7.Location = new System.Drawing.Point(20, 103);
            this.txtAckc7.Margin = new System.Windows.Forms.Padding(2);
            this.txtAckc7.Name = "txtAckc7";
            this.txtAckc7.ReadOnly = true;
            this.txtAckc7.Size = new System.Drawing.Size(90, 27);
            this.txtAckc7.TabIndex = 20;
            // 
            // lblReceivedPPID
            // 
            this.lblReceivedPPID.AutoSize = true;
            this.lblReceivedPPID.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblReceivedPPID.Location = new System.Drawing.Point(10, 150);
            this.lblReceivedPPID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblReceivedPPID.Name = "lblReceivedPPID";
            this.lblReceivedPPID.Size = new System.Drawing.Size(123, 19);
            this.lblReceivedPPID.TabIndex = 21;
            this.lblReceivedPPID.Text = "Received PPID：";
            // 
            // lvPPIDs
            // 
            this.lvPPIDs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvPPIDs.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvPPIDs.HideSelection = false;
            this.lvPPIDs.LabelWrap = false;
            this.lvPPIDs.Location = new System.Drawing.Point(8, 27);
            this.lvPPIDs.Margin = new System.Windows.Forms.Padding(2);
            this.lvPPIDs.MultiSelect = false;
            this.lvPPIDs.Name = "lvPPIDs";
            this.lvPPIDs.Size = new System.Drawing.Size(80, 142);
            this.lvPPIDs.TabIndex = 16;
            this.lvPPIDs.UseCompatibleStateImageBehavior = false;
            this.lvPPIDs.View = System.Windows.Forms.View.Details;
            this.lvPPIDs.SelectedIndexChanged += new System.EventHandler(this.lvPPIDs_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "PPID";
            this.columnHeader1.Width = 70;
            // 
            // txtPPBody_Unformatted
            // 
            this.txtPPBody_Unformatted.Location = new System.Drawing.Point(381, 178);
            this.txtPPBody_Unformatted.Margin = new System.Windows.Forms.Padding(2);
            this.txtPPBody_Unformatted.Multiline = true;
            this.txtPPBody_Unformatted.Name = "txtPPBody_Unformatted";
            this.txtPPBody_Unformatted.ReadOnly = true;
            this.txtPPBody_Unformatted.Size = new System.Drawing.Size(302, 54);
            this.txtPPBody_Unformatted.TabIndex = 23;
            // 
            // txtPPBodyPPID
            // 
            this.txtPPBodyPPID.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPPBodyPPID.Location = new System.Drawing.Point(494, 64);
            this.txtPPBodyPPID.Margin = new System.Windows.Forms.Padding(2);
            this.txtPPBodyPPID.Name = "txtPPBodyPPID";
            this.txtPPBodyPPID.Size = new System.Drawing.Size(76, 27);
            this.txtPPBodyPPID.TabIndex = 16;
            // 
            // lblPPBodyPPID
            // 
            this.lblPPBodyPPID.AutoSize = true;
            this.lblPPBodyPPID.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPPBodyPPID.Location = new System.Drawing.Point(382, 68);
            this.lblPPBodyPPID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPPBodyPPID.Name = "lblPPBodyPPID";
            this.lblPPBodyPPID.Size = new System.Drawing.Size(115, 19);
            this.lblPPBodyPPID.TabIndex = 16;
            this.lblPPBodyPPID.Text = "PPBody PPID：";
            // 
            // groupBox26
            // 
            this.groupBox26.Controls.Add(this.rdoPPBody_Formatted);
            this.groupBox26.Controls.Add(this.rdoPPBody_Unformatted);
            this.groupBox26.Controls.Add(this.rdoPPBody_Both);
            this.groupBox26.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox26.Location = new System.Drawing.Point(381, 10);
            this.groupBox26.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox26.Name = "groupBox26";
            this.groupBox26.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox26.Size = new System.Drawing.Size(301, 47);
            this.groupBox26.TabIndex = 12;
            this.groupBox26.TabStop = false;
            this.groupBox26.Text = "Support PPBody Type";
            // 
            // rdoPPBody_Formatted
            // 
            this.rdoPPBody_Formatted.AutoSize = true;
            this.rdoPPBody_Formatted.Location = new System.Drawing.Point(117, 22);
            this.rdoPPBody_Formatted.Margin = new System.Windows.Forms.Padding(2);
            this.rdoPPBody_Formatted.Name = "rdoPPBody_Formatted";
            this.rdoPPBody_Formatted.Size = new System.Drawing.Size(87, 21);
            this.rdoPPBody_Formatted.TabIndex = 2;
            this.rdoPPBody_Formatted.Text = "Formatted";
            this.rdoPPBody_Formatted.UseVisualStyleBackColor = true;
            this.rdoPPBody_Formatted.CheckedChanged += new System.EventHandler(this.rdoPPBody_Type_CheckedChanged);
            // 
            // rdoPPBody_Unformatted
            // 
            this.rdoPPBody_Unformatted.AutoSize = true;
            this.rdoPPBody_Unformatted.Location = new System.Drawing.Point(4, 21);
            this.rdoPPBody_Unformatted.Margin = new System.Windows.Forms.Padding(2);
            this.rdoPPBody_Unformatted.Name = "rdoPPBody_Unformatted";
            this.rdoPPBody_Unformatted.Size = new System.Drawing.Size(102, 21);
            this.rdoPPBody_Unformatted.TabIndex = 1;
            this.rdoPPBody_Unformatted.Text = "Unformatted";
            this.rdoPPBody_Unformatted.UseVisualStyleBackColor = true;
            this.rdoPPBody_Unformatted.CheckedChanged += new System.EventHandler(this.rdoPPBody_Type_CheckedChanged);
            // 
            // rdoPPBody_Both
            // 
            this.rdoPPBody_Both.AutoSize = true;
            this.rdoPPBody_Both.Checked = true;
            this.rdoPPBody_Both.Location = new System.Drawing.Point(216, 22);
            this.rdoPPBody_Both.Margin = new System.Windows.Forms.Padding(2);
            this.rdoPPBody_Both.Name = "rdoPPBody_Both";
            this.rdoPPBody_Both.Size = new System.Drawing.Size(55, 21);
            this.rdoPPBody_Both.TabIndex = 0;
            this.rdoPPBody_Both.TabStop = true;
            this.rdoPPBody_Both.Text = "Both";
            this.rdoPPBody_Both.UseVisualStyleBackColor = true;
            this.rdoPPBody_Both.CheckedChanged += new System.EventHandler(this.rdoPPBody_Type_CheckedChanged);
            // 
            // btnSendS7F25
            // 
            this.btnSendS7F25.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.btnSendS7F25.Location = new System.Drawing.Point(526, 137);
            this.btnSendS7F25.Margin = new System.Windows.Forms.Padding(2);
            this.btnSendS7F25.Name = "btnSendS7F25";
            this.btnSendS7F25.Size = new System.Drawing.Size(155, 33);
            this.btnSendS7F25.TabIndex = 11;
            this.btnSendS7F25.Text = "Request  PP (S7F25)";
            this.btnSendS7F25.UseVisualStyleBackColor = true;
            this.btnSendS7F25.Click += new System.EventHandler(this.btnSendS7F25_Click);
            // 
            // btnSendS7F23
            // 
            this.btnSendS7F23.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.btnSendS7F23.Location = new System.Drawing.Point(526, 94);
            this.btnSendS7F23.Margin = new System.Windows.Forms.Padding(2);
            this.btnSendS7F23.Name = "btnSendS7F23";
            this.btnSendS7F23.Size = new System.Drawing.Size(155, 34);
            this.btnSendS7F23.TabIndex = 10;
            this.btnSendS7F23.Text = "Send PP (S7F23)";
            this.btnSendS7F23.UseVisualStyleBackColor = true;
            this.btnSendS7F23.Click += new System.EventHandler(this.btnSendS7F23_Click);
            // 
            // btnSendS7F5
            // 
            this.btnSendS7F5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.btnSendS7F5.Location = new System.Drawing.Point(381, 137);
            this.btnSendS7F5.Margin = new System.Windows.Forms.Padding(2);
            this.btnSendS7F5.Name = "btnSendS7F5";
            this.btnSendS7F5.Size = new System.Drawing.Size(141, 33);
            this.btnSendS7F5.TabIndex = 9;
            this.btnSendS7F5.Text = "Request PP (S7F5)";
            this.btnSendS7F5.UseVisualStyleBackColor = true;
            this.btnSendS7F5.Click += new System.EventHandler(this.btnSendS7F5_Click);
            // 
            // btnSendS7F3
            // 
            this.btnSendS7F3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.btnSendS7F3.Location = new System.Drawing.Point(381, 94);
            this.btnSendS7F3.Margin = new System.Windows.Forms.Padding(2);
            this.btnSendS7F3.Name = "btnSendS7F3";
            this.btnSendS7F3.Size = new System.Drawing.Size(141, 34);
            this.btnSendS7F3.TabIndex = 8;
            this.btnSendS7F3.Text = "Send PP (S7F3)";
            this.btnSendS7F3.UseVisualStyleBackColor = true;
            this.btnSendS7F3.Click += new System.EventHandler(this.btnSendS7F3_Click);
            // 
            // btnPPChanged
            // 
            this.btnPPChanged.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.btnPPChanged.Location = new System.Drawing.Point(8, 200);
            this.btnPPChanged.Margin = new System.Windows.Forms.Padding(2);
            this.btnPPChanged.Name = "btnPPChanged";
            this.btnPPChanged.Size = new System.Drawing.Size(80, 37);
            this.btnPPChanged.TabIndex = 7;
            this.btnPPChanged.Text = "Change";
            this.btnPPChanged.UseVisualStyleBackColor = true;
            this.btnPPChanged.Click += new System.EventHandler(this.btnPPChanged_Click);
            // 
            // cmbChangedPPType
            // 
            this.cmbChangedPPType.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChangedPPType.FormattingEnabled = true;
            this.cmbChangedPPType.Items.AddRange(new object[] {
            "Create",
            "Changed",
            "Deleted"});
            this.cmbChangedPPType.Location = new System.Drawing.Point(8, 173);
            this.cmbChangedPPType.Margin = new System.Windows.Forms.Padding(2);
            this.cmbChangedPPType.Name = "cmbChangedPPType";
            this.cmbChangedPPType.Size = new System.Drawing.Size(80, 25);
            this.cmbChangedPPType.TabIndex = 4;
            this.cmbChangedPPType.SelectedValueChanged += new System.EventHandler(this.cmbChangedPPType_SelectedValueChanged);
            // 
            // groupBox25
            // 
            this.groupBox25.Controls.Add(this.tabControl3);
            this.groupBox25.Controls.Add(this.tabControl2);
            this.groupBox25.Controls.Add(this.txtPPID);
            this.groupBox25.Controls.Add(this.lblPPID);
            this.groupBox25.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox25.Location = new System.Drawing.Point(97, 10);
            this.groupBox25.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox25.Name = "groupBox25";
            this.groupBox25.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox25.Size = new System.Drawing.Size(275, 246);
            this.groupBox25.TabIndex = 2;
            this.groupBox25.TabStop = false;
            this.groupBox25.Text = "PPID Info";
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tbPPSetting);
            this.tabControl3.Controls.Add(this.tbPPSlotMap);
            this.tabControl3.Controls.Add(this.tbAngle);
            this.tabControl3.Controls.Add(this.tbPPCofficient);
            this.tabControl3.Controls.Add(this.tbMisc);
            this.tabControl3.Location = new System.Drawing.Point(5, 50);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(269, 191);
            this.tabControl3.TabIndex = 17;
            // 
            // tbPPSetting
            // 
            this.tbPPSetting.Controls.Add(this.label6);
            this.tbPPSetting.Controls.Add(this.txtLJStdSurface);
            this.tbPPSetting.Controls.Add(this.lblOffsetType);
            this.tbPPSetting.Controls.Add(this.txtOffsetType);
            this.tbPPSetting.Controls.Add(this.lblRepeatTimes);
            this.tbPPSetting.Controls.Add(this.txtRepeat);
            this.tbPPSetting.Controls.Add(this.lblType);
            this.tbPPSetting.Controls.Add(this.txtType);
            this.tbPPSetting.Controls.Add(this.txtRotateCount);
            this.tbPPSetting.Controls.Add(this.lblRotateCount);
            this.tbPPSetting.Location = new System.Drawing.Point(4, 28);
            this.tbPPSetting.Name = "tbPPSetting";
            this.tbPPSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tbPPSetting.Size = new System.Drawing.Size(261, 159);
            this.tbPPSetting.TabIndex = 0;
            this.tbPPSetting.Text = "Setting";
            this.tbPPSetting.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(9, 129);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 19);
            this.label6.TabIndex = 29;
            this.label6.Text = "LJ Std Surface：";
            // 
            // txtLJStdSurface
            // 
            this.txtLJStdSurface.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtLJStdSurface.Location = new System.Drawing.Point(160, 126);
            this.txtLJStdSurface.Margin = new System.Windows.Forms.Padding(2);
            this.txtLJStdSurface.Name = "txtLJStdSurface";
            this.txtLJStdSurface.ReadOnly = true;
            this.txtLJStdSurface.Size = new System.Drawing.Size(90, 27);
            this.txtLJStdSurface.TabIndex = 28;
            // 
            // lblOffsetType
            // 
            this.lblOffsetType.AutoSize = true;
            this.lblOffsetType.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblOffsetType.Location = new System.Drawing.Point(25, 99);
            this.lblOffsetType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOffsetType.Name = "lblOffsetType";
            this.lblOffsetType.Size = new System.Drawing.Size(104, 19);
            this.lblOffsetType.TabIndex = 27;
            this.lblOffsetType.Text = "Offset Type：";
            // 
            // txtOffsetType
            // 
            this.txtOffsetType.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtOffsetType.Location = new System.Drawing.Point(160, 96);
            this.txtOffsetType.Margin = new System.Windows.Forms.Padding(2);
            this.txtOffsetType.Name = "txtOffsetType";
            this.txtOffsetType.ReadOnly = true;
            this.txtOffsetType.Size = new System.Drawing.Size(90, 27);
            this.txtOffsetType.TabIndex = 26;
            // 
            // lblRepeatTimes
            // 
            this.lblRepeatTimes.AutoSize = true;
            this.lblRepeatTimes.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblRepeatTimes.Location = new System.Drawing.Point(12, 69);
            this.lblRepeatTimes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRepeatTimes.Name = "lblRepeatTimes";
            this.lblRepeatTimes.Size = new System.Drawing.Size(117, 19);
            this.lblRepeatTimes.TabIndex = 25;
            this.lblRepeatTimes.Text = "Repeat Times：";
            // 
            // txtRepeat
            // 
            this.txtRepeat.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtRepeat.Location = new System.Drawing.Point(160, 66);
            this.txtRepeat.Margin = new System.Windows.Forms.Padding(2);
            this.txtRepeat.Name = "txtRepeat";
            this.txtRepeat.ReadOnly = true;
            this.txtRepeat.Size = new System.Drawing.Size(90, 27);
            this.txtRepeat.TabIndex = 24;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblType.Location = new System.Drawing.Point(70, 39);
            this.lblType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(59, 19);
            this.lblType.TabIndex = 23;
            this.lblType.Text = "Type：";
            // 
            // txtType
            // 
            this.txtType.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtType.Location = new System.Drawing.Point(160, 36);
            this.txtType.Margin = new System.Windows.Forms.Padding(2);
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = true;
            this.txtType.Size = new System.Drawing.Size(90, 27);
            this.txtType.TabIndex = 22;
            // 
            // txtRotateCount
            // 
            this.txtRotateCount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtRotateCount.Location = new System.Drawing.Point(160, 6);
            this.txtRotateCount.Margin = new System.Windows.Forms.Padding(2);
            this.txtRotateCount.Name = "txtRotateCount";
            this.txtRotateCount.ReadOnly = true;
            this.txtRotateCount.Size = new System.Drawing.Size(90, 27);
            this.txtRotateCount.TabIndex = 21;
            // 
            // lblRotateCount
            // 
            this.lblRotateCount.AutoSize = true;
            this.lblRotateCount.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblRotateCount.Location = new System.Drawing.Point(12, 9);
            this.lblRotateCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRotateCount.Name = "lblRotateCount";
            this.lblRotateCount.Size = new System.Drawing.Size(117, 19);
            this.lblRotateCount.TabIndex = 18;
            this.lblRotateCount.Text = "Rotate Count：";
            // 
            // tbPPSlotMap
            // 
            this.tbPPSlotMap.Controls.Add(this.cLBPP);
            this.tbPPSlotMap.Location = new System.Drawing.Point(4, 28);
            this.tbPPSlotMap.Name = "tbPPSlotMap";
            this.tbPPSlotMap.Padding = new System.Windows.Forms.Padding(3);
            this.tbPPSlotMap.Size = new System.Drawing.Size(261, 159);
            this.tbPPSlotMap.TabIndex = 1;
            this.tbPPSlotMap.Text = "SlotMap";
            this.tbPPSlotMap.UseVisualStyleBackColor = true;
            // 
            // cLBPP
            // 
            this.cLBPP.FormattingEnabled = true;
            this.cLBPP.HorizontalScrollbar = true;
            this.cLBPP.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25"});
            this.cLBPP.Location = new System.Drawing.Point(6, 6);
            this.cLBPP.MultiColumn = true;
            this.cLBPP.Name = "cLBPP";
            this.cLBPP.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.cLBPP.Size = new System.Drawing.Size(249, 136);
            this.cLBPP.TabIndex = 0;
            // 
            // tbAngle
            // 
            this.tbAngle.Controls.Add(this.txtAngle4);
            this.tbAngle.Controls.Add(this.txtAngle3);
            this.tbAngle.Controls.Add(this.txtAngle2);
            this.tbAngle.Controls.Add(this.txtAngle1);
            this.tbAngle.Controls.Add(this.txtAngle8);
            this.tbAngle.Controls.Add(this.txtAngle7);
            this.tbAngle.Controls.Add(this.txtAngle6);
            this.tbAngle.Controls.Add(this.txtAngle5);
            this.tbAngle.Controls.Add(this.lblAngle8);
            this.tbAngle.Controls.Add(this.lblAngle7);
            this.tbAngle.Controls.Add(this.lblAngle6);
            this.tbAngle.Controls.Add(this.lblAngle5);
            this.tbAngle.Controls.Add(this.lblAngle4);
            this.tbAngle.Controls.Add(this.lblAngle3);
            this.tbAngle.Controls.Add(this.lblAngle2);
            this.tbAngle.Controls.Add(this.lblAngle1);
            this.tbAngle.Location = new System.Drawing.Point(4, 28);
            this.tbAngle.Name = "tbAngle";
            this.tbAngle.Size = new System.Drawing.Size(261, 159);
            this.tbAngle.TabIndex = 3;
            this.tbAngle.Text = "Angle";
            this.tbAngle.UseVisualStyleBackColor = true;
            // 
            // txtAngle4
            // 
            this.txtAngle4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtAngle4.Location = new System.Drawing.Point(69, 111);
            this.txtAngle4.Margin = new System.Windows.Forms.Padding(2);
            this.txtAngle4.Name = "txtAngle4";
            this.txtAngle4.ReadOnly = true;
            this.txtAngle4.Size = new System.Drawing.Size(60, 27);
            this.txtAngle4.TabIndex = 55;
            // 
            // txtAngle3
            // 
            this.txtAngle3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtAngle3.Location = new System.Drawing.Point(69, 81);
            this.txtAngle3.Margin = new System.Windows.Forms.Padding(2);
            this.txtAngle3.Name = "txtAngle3";
            this.txtAngle3.ReadOnly = true;
            this.txtAngle3.Size = new System.Drawing.Size(60, 27);
            this.txtAngle3.TabIndex = 54;
            // 
            // txtAngle2
            // 
            this.txtAngle2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtAngle2.Location = new System.Drawing.Point(69, 51);
            this.txtAngle2.Margin = new System.Windows.Forms.Padding(2);
            this.txtAngle2.Name = "txtAngle2";
            this.txtAngle2.ReadOnly = true;
            this.txtAngle2.Size = new System.Drawing.Size(60, 27);
            this.txtAngle2.TabIndex = 53;
            // 
            // txtAngle1
            // 
            this.txtAngle1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtAngle1.Location = new System.Drawing.Point(69, 21);
            this.txtAngle1.Margin = new System.Windows.Forms.Padding(2);
            this.txtAngle1.Name = "txtAngle1";
            this.txtAngle1.ReadOnly = true;
            this.txtAngle1.Size = new System.Drawing.Size(60, 27);
            this.txtAngle1.TabIndex = 52;
            // 
            // txtAngle8
            // 
            this.txtAngle8.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtAngle8.Location = new System.Drawing.Point(197, 111);
            this.txtAngle8.Margin = new System.Windows.Forms.Padding(2);
            this.txtAngle8.Name = "txtAngle8";
            this.txtAngle8.ReadOnly = true;
            this.txtAngle8.Size = new System.Drawing.Size(60, 27);
            this.txtAngle8.TabIndex = 51;
            // 
            // txtAngle7
            // 
            this.txtAngle7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtAngle7.Location = new System.Drawing.Point(197, 81);
            this.txtAngle7.Margin = new System.Windows.Forms.Padding(2);
            this.txtAngle7.Name = "txtAngle7";
            this.txtAngle7.ReadOnly = true;
            this.txtAngle7.Size = new System.Drawing.Size(60, 27);
            this.txtAngle7.TabIndex = 50;
            // 
            // txtAngle6
            // 
            this.txtAngle6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtAngle6.Location = new System.Drawing.Point(197, 51);
            this.txtAngle6.Margin = new System.Windows.Forms.Padding(2);
            this.txtAngle6.Name = "txtAngle6";
            this.txtAngle6.ReadOnly = true;
            this.txtAngle6.Size = new System.Drawing.Size(60, 27);
            this.txtAngle6.TabIndex = 49;
            // 
            // txtAngle5
            // 
            this.txtAngle5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtAngle5.Location = new System.Drawing.Point(197, 21);
            this.txtAngle5.Margin = new System.Windows.Forms.Padding(2);
            this.txtAngle5.Name = "txtAngle5";
            this.txtAngle5.ReadOnly = true;
            this.txtAngle5.Size = new System.Drawing.Size(60, 27);
            this.txtAngle5.TabIndex = 48;
            // 
            // lblAngle8
            // 
            this.lblAngle8.AutoSize = true;
            this.lblAngle8.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblAngle8.Location = new System.Drawing.Point(131, 114);
            this.lblAngle8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAngle8.Name = "lblAngle8";
            this.lblAngle8.Size = new System.Drawing.Size(73, 19);
            this.lblAngle8.TabIndex = 63;
            this.lblAngle8.Text = "Angle8：";
            // 
            // lblAngle7
            // 
            this.lblAngle7.AutoSize = true;
            this.lblAngle7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblAngle7.Location = new System.Drawing.Point(131, 84);
            this.lblAngle7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAngle7.Name = "lblAngle7";
            this.lblAngle7.Size = new System.Drawing.Size(73, 19);
            this.lblAngle7.TabIndex = 62;
            this.lblAngle7.Text = "Angle7：";
            // 
            // lblAngle6
            // 
            this.lblAngle6.AutoSize = true;
            this.lblAngle6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblAngle6.Location = new System.Drawing.Point(131, 54);
            this.lblAngle6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAngle6.Name = "lblAngle6";
            this.lblAngle6.Size = new System.Drawing.Size(73, 19);
            this.lblAngle6.TabIndex = 61;
            this.lblAngle6.Text = "Angle6：";
            // 
            // lblAngle5
            // 
            this.lblAngle5.AutoSize = true;
            this.lblAngle5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblAngle5.Location = new System.Drawing.Point(131, 24);
            this.lblAngle5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAngle5.Name = "lblAngle5";
            this.lblAngle5.Size = new System.Drawing.Size(73, 19);
            this.lblAngle5.TabIndex = 60;
            this.lblAngle5.Text = "Angle5：";
            // 
            // lblAngle4
            // 
            this.lblAngle4.AutoSize = true;
            this.lblAngle4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblAngle4.Location = new System.Drawing.Point(1, 114);
            this.lblAngle4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAngle4.Name = "lblAngle4";
            this.lblAngle4.Size = new System.Drawing.Size(73, 19);
            this.lblAngle4.TabIndex = 59;
            this.lblAngle4.Text = "Angle4：";
            // 
            // lblAngle3
            // 
            this.lblAngle3.AutoSize = true;
            this.lblAngle3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblAngle3.Location = new System.Drawing.Point(1, 84);
            this.lblAngle3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAngle3.Name = "lblAngle3";
            this.lblAngle3.Size = new System.Drawing.Size(73, 19);
            this.lblAngle3.TabIndex = 58;
            this.lblAngle3.Text = "Angle3：";
            // 
            // lblAngle2
            // 
            this.lblAngle2.AutoSize = true;
            this.lblAngle2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblAngle2.Location = new System.Drawing.Point(1, 54);
            this.lblAngle2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAngle2.Name = "lblAngle2";
            this.lblAngle2.Size = new System.Drawing.Size(73, 19);
            this.lblAngle2.TabIndex = 57;
            this.lblAngle2.Text = "Angle2：";
            // 
            // lblAngle1
            // 
            this.lblAngle1.AutoSize = true;
            this.lblAngle1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblAngle1.Location = new System.Drawing.Point(1, 24);
            this.lblAngle1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAngle1.Name = "lblAngle1";
            this.lblAngle1.Size = new System.Drawing.Size(73, 19);
            this.lblAngle1.TabIndex = 56;
            this.lblAngle1.Text = "Angle1：";
            // 
            // tbPPCofficient
            // 
            this.tbPPCofficient.Controls.Add(this.txtS2_2x1);
            this.tbPPCofficient.Controls.Add(this.txtS2_2x0);
            this.tbPPCofficient.Controls.Add(this.txtS2_1x1);
            this.tbPPCofficient.Controls.Add(this.txtS2_1x0);
            this.tbPPCofficient.Controls.Add(this.txtBTTH);
            this.tbPPCofficient.Controls.Add(this.txtRange2);
            this.tbPPCofficient.Controls.Add(this.txtRange1);
            this.tbPPCofficient.Controls.Add(this.txtS1_1x1);
            this.tbPPCofficient.Controls.Add(this.txtS1_1x0);
            this.tbPPCofficient.Controls.Add(this.lblRange2);
            this.tbPPCofficient.Controls.Add(this.lblRange1);
            this.tbPPCofficient.Controls.Add(this.lblS1_1x1);
            this.tbPPCofficient.Controls.Add(this.lblS1_1x0);
            this.tbPPCofficient.Controls.Add(this.lblS2_2x1);
            this.tbPPCofficient.Controls.Add(this.lblS2_2x0);
            this.tbPPCofficient.Controls.Add(this.lblS2_1x1);
            this.tbPPCofficient.Controls.Add(this.lblS2_1x0);
            this.tbPPCofficient.Controls.Add(this.lblBTTH);
            this.tbPPCofficient.Location = new System.Drawing.Point(4, 28);
            this.tbPPCofficient.Name = "tbPPCofficient";
            this.tbPPCofficient.Size = new System.Drawing.Size(261, 159);
            this.tbPPCofficient.TabIndex = 2;
            this.tbPPCofficient.Text = "Cofficient";
            this.tbPPCofficient.UseVisualStyleBackColor = true;
            // 
            // txtS2_2x1
            // 
            this.txtS2_2x1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtS2_2x1.Location = new System.Drawing.Point(69, 126);
            this.txtS2_2x1.Margin = new System.Windows.Forms.Padding(2);
            this.txtS2_2x1.Name = "txtS2_2x1";
            this.txtS2_2x1.ReadOnly = true;
            this.txtS2_2x1.Size = new System.Drawing.Size(60, 27);
            this.txtS2_2x1.TabIndex = 38;
            // 
            // txtS2_2x0
            // 
            this.txtS2_2x0.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtS2_2x0.Location = new System.Drawing.Point(69, 96);
            this.txtS2_2x0.Margin = new System.Windows.Forms.Padding(2);
            this.txtS2_2x0.Name = "txtS2_2x0";
            this.txtS2_2x0.ReadOnly = true;
            this.txtS2_2x0.Size = new System.Drawing.Size(60, 27);
            this.txtS2_2x0.TabIndex = 37;
            // 
            // txtS2_1x1
            // 
            this.txtS2_1x1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtS2_1x1.Location = new System.Drawing.Point(69, 66);
            this.txtS2_1x1.Margin = new System.Windows.Forms.Padding(2);
            this.txtS2_1x1.Name = "txtS2_1x1";
            this.txtS2_1x1.ReadOnly = true;
            this.txtS2_1x1.Size = new System.Drawing.Size(60, 27);
            this.txtS2_1x1.TabIndex = 36;
            // 
            // txtS2_1x0
            // 
            this.txtS2_1x0.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtS2_1x0.Location = new System.Drawing.Point(69, 36);
            this.txtS2_1x0.Margin = new System.Windows.Forms.Padding(2);
            this.txtS2_1x0.Name = "txtS2_1x0";
            this.txtS2_1x0.ReadOnly = true;
            this.txtS2_1x0.Size = new System.Drawing.Size(60, 27);
            this.txtS2_1x0.TabIndex = 35;
            // 
            // txtBTTH
            // 
            this.txtBTTH.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtBTTH.Location = new System.Drawing.Point(69, 6);
            this.txtBTTH.Margin = new System.Windows.Forms.Padding(2);
            this.txtBTTH.Name = "txtBTTH";
            this.txtBTTH.ReadOnly = true;
            this.txtBTTH.Size = new System.Drawing.Size(60, 27);
            this.txtBTTH.TabIndex = 34;
            // 
            // txtRange2
            // 
            this.txtRange2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtRange2.Location = new System.Drawing.Point(199, 96);
            this.txtRange2.Margin = new System.Windows.Forms.Padding(2);
            this.txtRange2.Name = "txtRange2";
            this.txtRange2.ReadOnly = true;
            this.txtRange2.Size = new System.Drawing.Size(60, 27);
            this.txtRange2.TabIndex = 32;
            // 
            // txtRange1
            // 
            this.txtRange1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtRange1.Location = new System.Drawing.Point(199, 66);
            this.txtRange1.Margin = new System.Windows.Forms.Padding(2);
            this.txtRange1.Name = "txtRange1";
            this.txtRange1.ReadOnly = true;
            this.txtRange1.Size = new System.Drawing.Size(60, 27);
            this.txtRange1.TabIndex = 31;
            // 
            // txtS1_1x1
            // 
            this.txtS1_1x1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtS1_1x1.Location = new System.Drawing.Point(199, 36);
            this.txtS1_1x1.Margin = new System.Windows.Forms.Padding(2);
            this.txtS1_1x1.Name = "txtS1_1x1";
            this.txtS1_1x1.ReadOnly = true;
            this.txtS1_1x1.Size = new System.Drawing.Size(60, 27);
            this.txtS1_1x1.TabIndex = 30;
            // 
            // txtS1_1x0
            // 
            this.txtS1_1x0.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtS1_1x0.Location = new System.Drawing.Point(199, 6);
            this.txtS1_1x0.Margin = new System.Windows.Forms.Padding(2);
            this.txtS1_1x0.Name = "txtS1_1x0";
            this.txtS1_1x0.ReadOnly = true;
            this.txtS1_1x0.Size = new System.Drawing.Size(60, 27);
            this.txtS1_1x0.TabIndex = 29;
            // 
            // lblRange2
            // 
            this.lblRange2.AutoSize = true;
            this.lblRange2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblRange2.Location = new System.Drawing.Point(129, 99);
            this.lblRange2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRange2.Name = "lblRange2";
            this.lblRange2.Size = new System.Drawing.Size(76, 19);
            this.lblRange2.TabIndex = 47;
            this.lblRange2.Text = "Range2：";
            // 
            // lblRange1
            // 
            this.lblRange1.AutoSize = true;
            this.lblRange1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblRange1.Location = new System.Drawing.Point(129, 69);
            this.lblRange1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRange1.Name = "lblRange1";
            this.lblRange1.Size = new System.Drawing.Size(76, 19);
            this.lblRange1.TabIndex = 46;
            this.lblRange1.Text = "Range1：";
            // 
            // lblS1_1x1
            // 
            this.lblS1_1x1.AutoSize = true;
            this.lblS1_1x1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblS1_1x1.Location = new System.Drawing.Point(132, 39);
            this.lblS1_1x1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblS1_1x1.Name = "lblS1_1x1";
            this.lblS1_1x1.Size = new System.Drawing.Size(73, 19);
            this.lblS1_1x1.TabIndex = 45;
            this.lblS1_1x1.Text = "S1_1x1：";
            // 
            // lblS1_1x0
            // 
            this.lblS1_1x0.AutoSize = true;
            this.lblS1_1x0.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblS1_1x0.Location = new System.Drawing.Point(132, 9);
            this.lblS1_1x0.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblS1_1x0.Name = "lblS1_1x0";
            this.lblS1_1x0.Size = new System.Drawing.Size(73, 19);
            this.lblS1_1x0.TabIndex = 44;
            this.lblS1_1x0.Text = "S1_1x0：";
            // 
            // lblS2_2x1
            // 
            this.lblS2_2x1.AutoSize = true;
            this.lblS2_2x1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblS2_2x1.Location = new System.Drawing.Point(0, 129);
            this.lblS2_2x1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblS2_2x1.Name = "lblS2_2x1";
            this.lblS2_2x1.Size = new System.Drawing.Size(73, 19);
            this.lblS2_2x1.TabIndex = 43;
            this.lblS2_2x1.Text = "S2_2x1：";
            // 
            // lblS2_2x0
            // 
            this.lblS2_2x0.AutoSize = true;
            this.lblS2_2x0.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblS2_2x0.Location = new System.Drawing.Point(0, 99);
            this.lblS2_2x0.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblS2_2x0.Name = "lblS2_2x0";
            this.lblS2_2x0.Size = new System.Drawing.Size(73, 19);
            this.lblS2_2x0.TabIndex = 42;
            this.lblS2_2x0.Text = "S2_2x0：";
            // 
            // lblS2_1x1
            // 
            this.lblS2_1x1.AutoSize = true;
            this.lblS2_1x1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblS2_1x1.Location = new System.Drawing.Point(0, 69);
            this.lblS2_1x1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblS2_1x1.Name = "lblS2_1x1";
            this.lblS2_1x1.Size = new System.Drawing.Size(73, 19);
            this.lblS2_1x1.TabIndex = 41;
            this.lblS2_1x1.Text = "S2_1x1：";
            // 
            // lblS2_1x0
            // 
            this.lblS2_1x0.AutoSize = true;
            this.lblS2_1x0.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblS2_1x0.Location = new System.Drawing.Point(0, 39);
            this.lblS2_1x0.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblS2_1x0.Name = "lblS2_1x0";
            this.lblS2_1x0.Size = new System.Drawing.Size(73, 19);
            this.lblS2_1x0.TabIndex = 40;
            this.lblS2_1x0.Text = "S2_1x0：";
            // 
            // lblBTTH
            // 
            this.lblBTTH.AutoSize = true;
            this.lblBTTH.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblBTTH.Location = new System.Drawing.Point(12, 9);
            this.lblBTTH.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBTTH.Name = "lblBTTH";
            this.lblBTTH.Size = new System.Drawing.Size(61, 19);
            this.lblBTTH.TabIndex = 39;
            this.lblBTTH.Text = "BTTH：";
            // 
            // tbMisc
            // 
            this.tbMisc.Controls.Add(this.lblReviseTime);
            this.tbMisc.Controls.Add(this.txtReviseTime);
            this.tbMisc.Controls.Add(this.lblRepeatTimes_now);
            this.tbMisc.Controls.Add(this.txtRepeatTimes_now);
            this.tbMisc.Controls.Add(this.txtCreateTime);
            this.tbMisc.Controls.Add(this.lblCreateTime);
            this.tbMisc.Location = new System.Drawing.Point(4, 28);
            this.tbMisc.Name = "tbMisc";
            this.tbMisc.Size = new System.Drawing.Size(261, 159);
            this.tbMisc.TabIndex = 4;
            this.tbMisc.Text = "Misc";
            this.tbMisc.UseVisualStyleBackColor = true;
            // 
            // lblReviseTime
            // 
            this.lblReviseTime.AutoSize = true;
            this.lblReviseTime.Enabled = false;
            this.lblReviseTime.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblReviseTime.Location = new System.Drawing.Point(2, 68);
            this.lblReviseTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblReviseTime.Name = "lblReviseTime";
            this.lblReviseTime.Size = new System.Drawing.Size(102, 19);
            this.lblReviseTime.TabIndex = 31;
            this.lblReviseTime.Text = "ReviseTime：";
            // 
            // txtReviseTime
            // 
            this.txtReviseTime.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtReviseTime.Location = new System.Drawing.Point(15, 93);
            this.txtReviseTime.Margin = new System.Windows.Forms.Padding(2);
            this.txtReviseTime.Name = "txtReviseTime";
            this.txtReviseTime.ReadOnly = true;
            this.txtReviseTime.Size = new System.Drawing.Size(234, 27);
            this.txtReviseTime.TabIndex = 30;
            // 
            // lblRepeatTimes_now
            // 
            this.lblRepeatTimes_now.AutoSize = true;
            this.lblRepeatTimes_now.Enabled = false;
            this.lblRepeatTimes_now.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblRepeatTimes_now.Location = new System.Drawing.Point(4, 130);
            this.lblRepeatTimes_now.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRepeatTimes_now.Name = "lblRepeatTimes_now";
            this.lblRepeatTimes_now.Size = new System.Drawing.Size(151, 19);
            this.lblRepeatTimes_now.TabIndex = 29;
            this.lblRepeatTimes_now.Text = "RepeatTimes_now：";
            // 
            // txtRepeatTimes_now
            // 
            this.txtRepeatTimes_now.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtRepeatTimes_now.Location = new System.Drawing.Point(159, 127);
            this.txtRepeatTimes_now.Margin = new System.Windows.Forms.Padding(2);
            this.txtRepeatTimes_now.Name = "txtRepeatTimes_now";
            this.txtRepeatTimes_now.ReadOnly = true;
            this.txtRepeatTimes_now.Size = new System.Drawing.Size(90, 27);
            this.txtRepeatTimes_now.TabIndex = 28;
            // 
            // txtCreateTime
            // 
            this.txtCreateTime.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.txtCreateTime.Location = new System.Drawing.Point(15, 33);
            this.txtCreateTime.Margin = new System.Windows.Forms.Padding(2);
            this.txtCreateTime.Name = "txtCreateTime";
            this.txtCreateTime.ReadOnly = true;
            this.txtCreateTime.Size = new System.Drawing.Size(234, 27);
            this.txtCreateTime.TabIndex = 27;
            // 
            // lblCreateTime
            // 
            this.lblCreateTime.AutoSize = true;
            this.lblCreateTime.Enabled = false;
            this.lblCreateTime.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblCreateTime.Location = new System.Drawing.Point(2, 6);
            this.lblCreateTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCreateTime.Name = "lblCreateTime";
            this.lblCreateTime.Size = new System.Drawing.Size(103, 19);
            this.lblCreateTime.TabIndex = 26;
            this.lblCreateTime.Text = "CreateTime：";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Location = new System.Drawing.Point(229, 22);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(8, 8);
            this.tabControl2.TabIndex = 16;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 28);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(0, 0);
            this.tabPage6.TabIndex = 0;
            this.tabPage6.Text = "tabPage6";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Location = new System.Drawing.Point(4, 28);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(0, 0);
            this.tabPage7.TabIndex = 1;
            this.tabPage7.Text = "tabPage7";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // txtPPID
            // 
            this.txtPPID.Location = new System.Drawing.Point(62, 22);
            this.txtPPID.Margin = new System.Windows.Forms.Padding(2);
            this.txtPPID.Name = "txtPPID";
            this.txtPPID.ReadOnly = true;
            this.txtPPID.Size = new System.Drawing.Size(155, 27);
            this.txtPPID.TabIndex = 1;
            // 
            // lblPPID
            // 
            this.lblPPID.AutoSize = true;
            this.lblPPID.Location = new System.Drawing.Point(4, 25);
            this.lblPPID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPPID.Name = "lblPPID";
            this.lblPPID.Size = new System.Drawing.Size(58, 19);
            this.lblPPID.TabIndex = 0;
            this.lblPPID.Text = "PPID：";
            // 
            // lblPPIDList
            // 
            this.lblPPIDList.AutoSize = true;
            this.lblPPIDList.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.lblPPIDList.Location = new System.Drawing.Point(4, 6);
            this.lblPPIDList.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPPIDList.Name = "lblPPIDList";
            this.lblPPIDList.Size = new System.Drawing.Size(85, 19);
            this.lblPPIDList.TabIndex = 1;
            this.lblPPIDList.Text = "PPID List：";
            // 
            // tabClock
            // 
            this.tabClock.Controls.Add(this.groupBox17);
            this.tabClock.Controls.Add(this.groupBox16);
            this.tabClock.Location = new System.Drawing.Point(4, 22);
            this.tabClock.Margin = new System.Windows.Forms.Padding(2);
            this.tabClock.Name = "tabClock";
            this.tabClock.Size = new System.Drawing.Size(928, 261);
            this.tabClock.TabIndex = 7;
            this.tabClock.Text = "Clock";
            this.tabClock.UseVisualStyleBackColor = true;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.btnEQPDateAndTimeRequest);
            this.groupBox17.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox17.Location = new System.Drawing.Point(4, 5);
            this.groupBox17.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox17.Size = new System.Drawing.Size(215, 65);
            this.groupBox17.TabIndex = 9;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "S2F17";
            // 
            // btnEQPDateAndTimeRequest
            // 
            this.btnEQPDateAndTimeRequest.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEQPDateAndTimeRequest.Location = new System.Drawing.Point(8, 25);
            this.btnEQPDateAndTimeRequest.Margin = new System.Windows.Forms.Padding(2);
            this.btnEQPDateAndTimeRequest.Name = "btnEQPDateAndTimeRequest";
            this.btnEQPDateAndTimeRequest.Size = new System.Drawing.Size(194, 27);
            this.btnEQPDateAndTimeRequest.TabIndex = 2;
            this.btnEQPDateAndTimeRequest.Text = "EQP Date And Time Request";
            this.btnEQPDateAndTimeRequest.UseVisualStyleBackColor = true;
            this.btnEQPDateAndTimeRequest.Click += new System.EventHandler(this.btnEQPDateAndTimeRequest_Click);
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.txtClockMessageName);
            this.groupBox16.Controls.Add(this.txtHostDownloadDateTime);
            this.groupBox16.Controls.Add(this.txtSetSystemDateTimeSuccess);
            this.groupBox16.Controls.Add(this.lblClockMessageName);
            this.groupBox16.Controls.Add(this.lblHostDownloadDateTime);
            this.groupBox16.Controls.Add(this.lblSetSystemDateTimeSuccess);
            this.groupBox16.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox16.Location = new System.Drawing.Point(420, 5);
            this.groupBox16.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox16.Size = new System.Drawing.Size(411, 197);
            this.groupBox16.TabIndex = 8;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Receive Message";
            // 
            // txtClockMessageName
            // 
            this.txtClockMessageName.Enabled = false;
            this.txtClockMessageName.Location = new System.Drawing.Point(218, 29);
            this.txtClockMessageName.Margin = new System.Windows.Forms.Padding(2);
            this.txtClockMessageName.MaxLength = 10;
            this.txtClockMessageName.Name = "txtClockMessageName";
            this.txtClockMessageName.Size = new System.Drawing.Size(155, 27);
            this.txtClockMessageName.TabIndex = 5;
            // 
            // txtHostDownloadDateTime
            // 
            this.txtHostDownloadDateTime.Enabled = false;
            this.txtHostDownloadDateTime.Location = new System.Drawing.Point(218, 61);
            this.txtHostDownloadDateTime.Margin = new System.Windows.Forms.Padding(2);
            this.txtHostDownloadDateTime.MaxLength = 10;
            this.txtHostDownloadDateTime.Name = "txtHostDownloadDateTime";
            this.txtHostDownloadDateTime.Size = new System.Drawing.Size(155, 27);
            this.txtHostDownloadDateTime.TabIndex = 5;
            // 
            // txtSetSystemDateTimeSuccess
            // 
            this.txtSetSystemDateTimeSuccess.Enabled = false;
            this.txtSetSystemDateTimeSuccess.Location = new System.Drawing.Point(218, 93);
            this.txtSetSystemDateTimeSuccess.Margin = new System.Windows.Forms.Padding(2);
            this.txtSetSystemDateTimeSuccess.MaxLength = 10;
            this.txtSetSystemDateTimeSuccess.Name = "txtSetSystemDateTimeSuccess";
            this.txtSetSystemDateTimeSuccess.Size = new System.Drawing.Size(155, 27);
            this.txtSetSystemDateTimeSuccess.TabIndex = 5;
            // 
            // lblClockMessageName
            // 
            this.lblClockMessageName.AutoSize = true;
            this.lblClockMessageName.Location = new System.Drawing.Point(4, 29);
            this.lblClockMessageName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblClockMessageName.Name = "lblClockMessageName";
            this.lblClockMessageName.Size = new System.Drawing.Size(160, 19);
            this.lblClockMessageName.TabIndex = 4;
            this.lblClockMessageName.Text = "ClockMessageName：";
            // 
            // lblHostDownloadDateTime
            // 
            this.lblHostDownloadDateTime.AutoSize = true;
            this.lblHostDownloadDateTime.Location = new System.Drawing.Point(4, 61);
            this.lblHostDownloadDateTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHostDownloadDateTime.Name = "lblHostDownloadDateTime";
            this.lblHostDownloadDateTime.Size = new System.Drawing.Size(192, 19);
            this.lblHostDownloadDateTime.TabIndex = 4;
            this.lblHostDownloadDateTime.Text = "HostDownloadDateTime：";
            // 
            // lblSetSystemDateTimeSuccess
            // 
            this.lblSetSystemDateTimeSuccess.AutoSize = true;
            this.lblSetSystemDateTimeSuccess.Location = new System.Drawing.Point(4, 93);
            this.lblSetSystemDateTimeSuccess.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSetSystemDateTimeSuccess.Name = "lblSetSystemDateTimeSuccess";
            this.lblSetSystemDateTimeSuccess.Size = new System.Drawing.Size(213, 19);
            this.lblSetSystemDateTimeSuccess.TabIndex = 4;
            this.lblSetSystemDateTimeSuccess.Text = "SetSystemDateTimeSuccess：";
            // 
            // tabEQPTerminalServices
            // 
            this.tabEQPTerminalServices.Controls.Add(this.groupBox21);
            this.tabEQPTerminalServices.Controls.Add(this.groupBox19);
            this.tabEQPTerminalServices.Controls.Add(this.groupBox18);
            this.tabEQPTerminalServices.Location = new System.Drawing.Point(4, 22);
            this.tabEQPTerminalServices.Margin = new System.Windows.Forms.Padding(2);
            this.tabEQPTerminalServices.Name = "tabEQPTerminalServices";
            this.tabEQPTerminalServices.Size = new System.Drawing.Size(928, 261);
            this.tabEQPTerminalServices.TabIndex = 8;
            this.tabEQPTerminalServices.Text = "EQPTerminalServices";
            this.tabEQPTerminalServices.UseVisualStyleBackColor = true;
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.rdoMultiblockNotAllowedFalse);
            this.groupBox21.Controls.Add(this.lblMultiblockNotAllowed);
            this.groupBox21.Controls.Add(this.rdoMultiblockNotAllowedTrue);
            this.groupBox21.Controls.Add(this.btnTerminalACKC10);
            this.groupBox21.Controls.Add(this.lblTerminalACKC10);
            this.groupBox21.Controls.Add(this.txtTerminalACKC10);
            this.groupBox21.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox21.Location = new System.Drawing.Point(638, 5);
            this.groupBox21.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox21.Size = new System.Drawing.Size(199, 203);
            this.groupBox21.TabIndex = 10;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Reply Message";
            // 
            // rdoMultiblockNotAllowedFalse
            // 
            this.rdoMultiblockNotAllowedFalse.AutoSize = true;
            this.rdoMultiblockNotAllowedFalse.Checked = true;
            this.rdoMultiblockNotAllowedFalse.Location = new System.Drawing.Point(101, 91);
            this.rdoMultiblockNotAllowedFalse.Margin = new System.Windows.Forms.Padding(2);
            this.rdoMultiblockNotAllowedFalse.Name = "rdoMultiblockNotAllowedFalse";
            this.rdoMultiblockNotAllowedFalse.Size = new System.Drawing.Size(60, 23);
            this.rdoMultiblockNotAllowedFalse.TabIndex = 6;
            this.rdoMultiblockNotAllowedFalse.TabStop = true;
            this.rdoMultiblockNotAllowedFalse.Text = "False";
            this.rdoMultiblockNotAllowedFalse.UseVisualStyleBackColor = true;
            // 
            // lblMultiblockNotAllowed
            // 
            this.lblMultiblockNotAllowed.AutoSize = true;
            this.lblMultiblockNotAllowed.Location = new System.Drawing.Point(19, 57);
            this.lblMultiblockNotAllowed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMultiblockNotAllowed.Name = "lblMultiblockNotAllowed";
            this.lblMultiblockNotAllowed.Size = new System.Drawing.Size(180, 19);
            this.lblMultiblockNotAllowed.TabIndex = 5;
            this.lblMultiblockNotAllowed.Text = "Multiblock Not Allowed :";
            // 
            // rdoMultiblockNotAllowedTrue
            // 
            this.rdoMultiblockNotAllowedTrue.AutoSize = true;
            this.rdoMultiblockNotAllowedTrue.Location = new System.Drawing.Point(47, 91);
            this.rdoMultiblockNotAllowedTrue.Margin = new System.Windows.Forms.Padding(2);
            this.rdoMultiblockNotAllowedTrue.Name = "rdoMultiblockNotAllowedTrue";
            this.rdoMultiblockNotAllowedTrue.Size = new System.Drawing.Size(57, 23);
            this.rdoMultiblockNotAllowedTrue.TabIndex = 4;
            this.rdoMultiblockNotAllowedTrue.Text = "True";
            this.rdoMultiblockNotAllowedTrue.UseVisualStyleBackColor = true;
            // 
            // btnTerminalACKC10
            // 
            this.btnTerminalACKC10.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTerminalACKC10.Location = new System.Drawing.Point(13, 129);
            this.btnTerminalACKC10.Margin = new System.Windows.Forms.Padding(2);
            this.btnTerminalACKC10.Name = "btnTerminalACKC10";
            this.btnTerminalACKC10.Size = new System.Drawing.Size(172, 27);
            this.btnTerminalACKC10.TabIndex = 3;
            this.btnTerminalACKC10.Text = "Terminal Reply";
            this.btnTerminalACKC10.UseVisualStyleBackColor = true;
            this.btnTerminalACKC10.Click += new System.EventHandler(this.btnTerminalACKC10_Click);
            // 
            // lblTerminalACKC10
            // 
            this.lblTerminalACKC10.AutoSize = true;
            this.lblTerminalACKC10.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerminalACKC10.Location = new System.Drawing.Point(19, 25);
            this.lblTerminalACKC10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTerminalACKC10.Name = "lblTerminalACKC10";
            this.lblTerminalACKC10.Size = new System.Drawing.Size(76, 19);
            this.lblTerminalACKC10.TabIndex = 0;
            this.lblTerminalACKC10.Text = "ACKC10：";
            // 
            // txtTerminalACKC10
            // 
            this.txtTerminalACKC10.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTerminalACKC10.Location = new System.Drawing.Point(99, 22);
            this.txtTerminalACKC10.Margin = new System.Windows.Forms.Padding(2);
            this.txtTerminalACKC10.Name = "txtTerminalACKC10";
            this.txtTerminalACKC10.Size = new System.Drawing.Size(96, 27);
            this.txtTerminalACKC10.TabIndex = 1;
            this.txtTerminalACKC10.Text = "0";
            this.txtTerminalACKC10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.lblTerminalText);
            this.groupBox19.Controls.Add(this.btnMessageRecognition);
            this.groupBox19.Controls.Add(this.txtTerminalDisplayType);
            this.groupBox19.Controls.Add(this.txtTerminalSystemBytes);
            this.groupBox19.Controls.Add(this.txtTerminalNumber);
            this.groupBox19.Controls.Add(this.txtTerminalText);
            this.groupBox19.Controls.Add(this.lblTerminalDisplayType);
            this.groupBox19.Controls.Add(this.lblTerminalNumber);
            this.groupBox19.Controls.Add(this.lblTerminalSystemBytes);
            this.groupBox19.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox19.Location = new System.Drawing.Point(210, 5);
            this.groupBox19.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox19.Size = new System.Drawing.Size(423, 203);
            this.groupBox19.TabIndex = 9;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Receive Message";
            // 
            // lblTerminalText
            // 
            this.lblTerminalText.AutoSize = true;
            this.lblTerminalText.Location = new System.Drawing.Point(4, 125);
            this.lblTerminalText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTerminalText.Name = "lblTerminalText";
            this.lblTerminalText.Size = new System.Drawing.Size(113, 19);
            this.lblTerminalText.TabIndex = 6;
            this.lblTerminalText.Text = "TerminalText：";
            // 
            // btnMessageRecognition
            // 
            this.btnMessageRecognition.BackColor = System.Drawing.Color.LightCyan;
            this.btnMessageRecognition.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMessageRecognition.Location = new System.Drawing.Point(11, 166);
            this.btnMessageRecognition.Margin = new System.Windows.Forms.Padding(2);
            this.btnMessageRecognition.Name = "btnMessageRecognition";
            this.btnMessageRecognition.Size = new System.Drawing.Size(160, 27);
            this.btnMessageRecognition.TabIndex = 2;
            this.btnMessageRecognition.Text = "Message Recognition";
            this.btnMessageRecognition.UseVisualStyleBackColor = false;
            this.btnMessageRecognition.Click += new System.EventHandler(this.btnMessageRecognition_Click);
            // 
            // txtTerminalDisplayType
            // 
            this.txtTerminalDisplayType.Enabled = false;
            this.txtTerminalDisplayType.Location = new System.Drawing.Point(184, 29);
            this.txtTerminalDisplayType.Margin = new System.Windows.Forms.Padding(2);
            this.txtTerminalDisplayType.MaxLength = 10;
            this.txtTerminalDisplayType.Name = "txtTerminalDisplayType";
            this.txtTerminalDisplayType.Size = new System.Drawing.Size(226, 27);
            this.txtTerminalDisplayType.TabIndex = 5;
            // 
            // txtTerminalSystemBytes
            // 
            this.txtTerminalSystemBytes.Enabled = false;
            this.txtTerminalSystemBytes.Location = new System.Drawing.Point(184, 93);
            this.txtTerminalSystemBytes.Margin = new System.Windows.Forms.Padding(2);
            this.txtTerminalSystemBytes.MaxLength = 10;
            this.txtTerminalSystemBytes.Name = "txtTerminalSystemBytes";
            this.txtTerminalSystemBytes.Size = new System.Drawing.Size(226, 27);
            this.txtTerminalSystemBytes.TabIndex = 5;
            this.txtTerminalSystemBytes.Text = "0";
            // 
            // txtTerminalNumber
            // 
            this.txtTerminalNumber.Enabled = false;
            this.txtTerminalNumber.Location = new System.Drawing.Point(184, 61);
            this.txtTerminalNumber.Margin = new System.Windows.Forms.Padding(2);
            this.txtTerminalNumber.MaxLength = 10;
            this.txtTerminalNumber.Name = "txtTerminalNumber";
            this.txtTerminalNumber.Size = new System.Drawing.Size(226, 27);
            this.txtTerminalNumber.TabIndex = 5;
            // 
            // txtTerminalText
            // 
            this.txtTerminalText.Location = new System.Drawing.Point(184, 125);
            this.txtTerminalText.Margin = new System.Windows.Forms.Padding(2);
            this.txtTerminalText.MaxLength = 10;
            this.txtTerminalText.Multiline = true;
            this.txtTerminalText.Name = "txtTerminalText";
            this.txtTerminalText.ReadOnly = true;
            this.txtTerminalText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTerminalText.Size = new System.Drawing.Size(226, 72);
            this.txtTerminalText.TabIndex = 5;
            this.txtTerminalText.WordWrap = false;
            // 
            // lblTerminalDisplayType
            // 
            this.lblTerminalDisplayType.AutoSize = true;
            this.lblTerminalDisplayType.Location = new System.Drawing.Point(4, 29);
            this.lblTerminalDisplayType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTerminalDisplayType.Name = "lblTerminalDisplayType";
            this.lblTerminalDisplayType.Size = new System.Drawing.Size(167, 19);
            this.lblTerminalDisplayType.TabIndex = 4;
            this.lblTerminalDisplayType.Text = "TerminalDisplayType：";
            // 
            // lblTerminalNumber
            // 
            this.lblTerminalNumber.AutoSize = true;
            this.lblTerminalNumber.Location = new System.Drawing.Point(4, 61);
            this.lblTerminalNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTerminalNumber.Name = "lblTerminalNumber";
            this.lblTerminalNumber.Size = new System.Drawing.Size(141, 19);
            this.lblTerminalNumber.TabIndex = 4;
            this.lblTerminalNumber.Text = "TerminalNumber：";
            // 
            // lblTerminalSystemBytes
            // 
            this.lblTerminalSystemBytes.AutoSize = true;
            this.lblTerminalSystemBytes.Location = new System.Drawing.Point(4, 93);
            this.lblTerminalSystemBytes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTerminalSystemBytes.Name = "lblTerminalSystemBytes";
            this.lblTerminalSystemBytes.Size = new System.Drawing.Size(112, 19);
            this.lblTerminalSystemBytes.TabIndex = 4;
            this.lblTerminalSystemBytes.Text = "SystemBytes：";
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.lblText);
            this.groupBox18.Controls.Add(this.lblTID);
            this.groupBox18.Controls.Add(this.btnTerminalRequest);
            this.groupBox18.Controls.Add(this.txtText);
            this.groupBox18.Controls.Add(this.txtTID);
            this.groupBox18.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox18.Location = new System.Drawing.Point(4, 5);
            this.groupBox18.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox18.Size = new System.Drawing.Size(199, 140);
            this.groupBox18.TabIndex = 4;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "S10F1";
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblText.Location = new System.Drawing.Point(19, 57);
            this.lblText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(59, 19);
            this.lblText.TabIndex = 0;
            this.lblText.Text = "TEXT：";
            // 
            // lblTID
            // 
            this.lblTID.AutoSize = true;
            this.lblTID.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTID.Location = new System.Drawing.Point(19, 25);
            this.lblTID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTID.Name = "lblTID";
            this.lblTID.Size = new System.Drawing.Size(48, 19);
            this.lblTID.TabIndex = 0;
            this.lblTID.Text = "TID：";
            // 
            // btnTerminalRequest
            // 
            this.btnTerminalRequest.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTerminalRequest.Location = new System.Drawing.Point(22, 94);
            this.btnTerminalRequest.Margin = new System.Windows.Forms.Padding(2);
            this.btnTerminalRequest.Name = "btnTerminalRequest";
            this.btnTerminalRequest.Size = new System.Drawing.Size(172, 27);
            this.btnTerminalRequest.TabIndex = 2;
            this.btnTerminalRequest.Text = "Terminal Request";
            this.btnTerminalRequest.UseVisualStyleBackColor = true;
            this.btnTerminalRequest.Click += new System.EventHandler(this.btnTerminalRequest_Click);
            // 
            // txtText
            // 
            this.txtText.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtText.Location = new System.Drawing.Point(99, 50);
            this.txtText.Margin = new System.Windows.Forms.Padding(2);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(96, 27);
            this.txtText.TabIndex = 1;
            this.txtText.Text = "101";
            this.txtText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtTID
            // 
            this.txtTID.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTID.Location = new System.Drawing.Point(99, 22);
            this.txtTID.Margin = new System.Windows.Forms.Padding(2);
            this.txtTID.Name = "txtTID";
            this.txtTID.Size = new System.Drawing.Size(96, 27);
            this.txtTID.TabIndex = 1;
            this.txtTID.Text = "0";
            this.txtTID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabHostSetsCONTROLStateModel
            // 
            this.tabHostSetsCONTROLStateModel.Controls.Add(this.groupBox29);
            this.tabHostSetsCONTROLStateModel.Location = new System.Drawing.Point(4, 22);
            this.tabHostSetsCONTROLStateModel.Margin = new System.Windows.Forms.Padding(2);
            this.tabHostSetsCONTROLStateModel.Name = "tabHostSetsCONTROLStateModel";
            this.tabHostSetsCONTROLStateModel.Size = new System.Drawing.Size(928, 261);
            this.tabHostSetsCONTROLStateModel.TabIndex = 11;
            this.tabHostSetsCONTROLStateModel.Text = "HostSetsCONTROLStateModel";
            this.tabHostSetsCONTROLStateModel.UseVisualStyleBackColor = true;
            // 
            // groupBox29
            // 
            this.groupBox29.Controls.Add(this.label1);
            this.groupBox29.Controls.Add(this.label3);
            this.groupBox29.Controls.Add(this.txtSetsCONTROLStateModelReplyAck);
            this.groupBox29.Controls.Add(this.txtSetsCONTROLStateModelSystemBytes);
            this.groupBox29.Controls.Add(this.txtSetsCONTROLStateModel);
            this.groupBox29.Controls.Add(this.label5);
            this.groupBox29.Controls.Add(this.btnSetsCONTROLStateModelReplyMsg);
            this.groupBox29.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox29.Location = new System.Drawing.Point(4, 5);
            this.groupBox29.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox29.Name = "groupBox29";
            this.groupBox29.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox29.Size = new System.Drawing.Size(835, 197);
            this.groupBox29.TabIndex = 8;
            this.groupBox29.TabStop = false;
            this.groupBox29.Text = "Receive Message";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(399, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "ReplyHcAck：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 58);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "SystemBytes：";
            // 
            // txtSetsCONTROLStateModelReplyAck
            // 
            this.txtSetsCONTROLStateModelReplyAck.Location = new System.Drawing.Point(508, 19);
            this.txtSetsCONTROLStateModelReplyAck.Margin = new System.Windows.Forms.Padding(2);
            this.txtSetsCONTROLStateModelReplyAck.MaxLength = 10;
            this.txtSetsCONTROLStateModelReplyAck.Name = "txtSetsCONTROLStateModelReplyAck";
            this.txtSetsCONTROLStateModelReplyAck.Size = new System.Drawing.Size(76, 27);
            this.txtSetsCONTROLStateModelReplyAck.TabIndex = 5;
            this.txtSetsCONTROLStateModelReplyAck.Text = "0";
            // 
            // txtSetsCONTROLStateModelSystemBytes
            // 
            this.txtSetsCONTROLStateModelSystemBytes.Enabled = false;
            this.txtSetsCONTROLStateModelSystemBytes.Location = new System.Drawing.Point(196, 51);
            this.txtSetsCONTROLStateModelSystemBytes.Margin = new System.Windows.Forms.Padding(2);
            this.txtSetsCONTROLStateModelSystemBytes.MaxLength = 10;
            this.txtSetsCONTROLStateModelSystemBytes.Name = "txtSetsCONTROLStateModelSystemBytes";
            this.txtSetsCONTROLStateModelSystemBytes.Size = new System.Drawing.Size(150, 27);
            this.txtSetsCONTROLStateModelSystemBytes.TabIndex = 5;
            this.txtSetsCONTROLStateModelSystemBytes.Text = "0";
            // 
            // txtSetsCONTROLStateModel
            // 
            this.txtSetsCONTROLStateModel.Enabled = false;
            this.txtSetsCONTROLStateModel.Location = new System.Drawing.Point(196, 17);
            this.txtSetsCONTROLStateModel.Margin = new System.Windows.Forms.Padding(2);
            this.txtSetsCONTROLStateModel.MaxLength = 10;
            this.txtSetsCONTROLStateModel.Name = "txtSetsCONTROLStateModel";
            this.txtSetsCONTROLStateModel.Size = new System.Drawing.Size(150, 27);
            this.txtSetsCONTROLStateModel.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 22);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(199, 19);
            this.label5.TabIndex = 4;
            this.label5.Text = "SetsCONTROLStateModel：";
            // 
            // btnSetsCONTROLStateModelReplyMsg
            // 
            this.btnSetsCONTROLStateModelReplyMsg.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetsCONTROLStateModelReplyMsg.Location = new System.Drawing.Point(470, 58);
            this.btnSetsCONTROLStateModelReplyMsg.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetsCONTROLStateModelReplyMsg.Name = "btnSetsCONTROLStateModelReplyMsg";
            this.btnSetsCONTROLStateModelReplyMsg.Size = new System.Drawing.Size(114, 59);
            this.btnSetsCONTROLStateModelReplyMsg.TabIndex = 3;
            this.btnSetsCONTROLStateModelReplyMsg.Text = "ReplyMsg";
            this.btnSetsCONTROLStateModelReplyMsg.UseVisualStyleBackColor = true;
            this.btnSetsCONTROLStateModelReplyMsg.Click += new System.EventHandler(this.btnSetsCONTROLStateModelReplyMsg_Click);
            // 
            // tabObject
            // 
            this.tabObject.Controls.Add(this.tabControl1);
            this.tabObject.Location = new System.Drawing.Point(4, 22);
            this.tabObject.Margin = new System.Windows.Forms.Padding(2);
            this.tabObject.Name = "tabObject";
            this.tabObject.Size = new System.Drawing.Size(928, 261);
            this.tabObject.TabIndex = 12;
            this.tabObject.Text = "Object";
            this.tabObject.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(928, 261);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox44);
            this.tabPage1.Controls.Add(this.groupBox41);
            this.tabPage1.Controls.Add(this.groupBox31);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(920, 235);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Object1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox44
            // 
            this.groupBox44.Controls.Add(this.label43);
            this.groupBox44.Controls.Add(this.label48);
            this.groupBox44.Controls.Add(this.label49);
            this.groupBox44.Controls.Add(this.btnCreateObject);
            this.groupBox44.Controls.Add(this.txtCreateObjID);
            this.groupBox44.Controls.Add(this.txtCreateObjSpec);
            this.groupBox44.Controls.Add(this.txtCreateObjType);
            this.groupBox44.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox44.Location = new System.Drawing.Point(4, 5);
            this.groupBox44.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox44.Name = "groupBox44";
            this.groupBox44.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox44.Size = new System.Drawing.Size(222, 210);
            this.groupBox44.TabIndex = 6;
            this.groupBox44.TabStop = false;
            this.groupBox44.Text = "Create Object";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(19, 86);
            this.label43.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(56, 19);
            this.label43.TabIndex = 0;
            this.label43.Text = "OBJID :";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(19, 55);
            this.label48.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(75, 19);
            this.label48.TabIndex = 0;
            this.label48.Text = "OBJSPEC :";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(19, 25);
            this.label49.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(75, 19);
            this.label49.TabIndex = 0;
            this.label49.Text = "OBJTYPE :";
            // 
            // btnCreateObject
            // 
            this.btnCreateObject.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateObject.Location = new System.Drawing.Point(22, 178);
            this.btnCreateObject.Margin = new System.Windows.Forms.Padding(2);
            this.btnCreateObject.Name = "btnCreateObject";
            this.btnCreateObject.Size = new System.Drawing.Size(172, 27);
            this.btnCreateObject.TabIndex = 2;
            this.btnCreateObject.Text = "Create Object";
            this.btnCreateObject.UseVisualStyleBackColor = true;
            this.btnCreateObject.Click += new System.EventHandler(this.btnCreateObject_Click);
            // 
            // txtCreateObjID
            // 
            this.txtCreateObjID.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreateObjID.Location = new System.Drawing.Point(99, 83);
            this.txtCreateObjID.Margin = new System.Windows.Forms.Padding(2);
            this.txtCreateObjID.Name = "txtCreateObjID";
            this.txtCreateObjID.Size = new System.Drawing.Size(96, 27);
            this.txtCreateObjID.TabIndex = 1;
            this.txtCreateObjID.Text = "CARRIERID01";
            this.txtCreateObjID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCreateObjSpec
            // 
            this.txtCreateObjSpec.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreateObjSpec.Location = new System.Drawing.Point(99, 53);
            this.txtCreateObjSpec.Margin = new System.Windows.Forms.Padding(2);
            this.txtCreateObjSpec.Name = "txtCreateObjSpec";
            this.txtCreateObjSpec.Size = new System.Drawing.Size(96, 27);
            this.txtCreateObjSpec.TabIndex = 1;
            this.txtCreateObjSpec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCreateObjType
            // 
            this.txtCreateObjType.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCreateObjType.Location = new System.Drawing.Point(99, 22);
            this.txtCreateObjType.Margin = new System.Windows.Forms.Padding(2);
            this.txtCreateObjType.Name = "txtCreateObjType";
            this.txtCreateObjType.Size = new System.Drawing.Size(96, 27);
            this.txtCreateObjType.TabIndex = 1;
            this.txtCreateObjType.Text = "CARRIER";
            this.txtCreateObjType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox41
            // 
            this.groupBox41.Controls.Add(this.btnUpdateObject_ProcessJob);
            this.groupBox41.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox41.Location = new System.Drawing.Point(584, 5);
            this.groupBox41.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox41.Name = "groupBox41";
            this.groupBox41.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox41.Size = new System.Drawing.Size(334, 130);
            this.groupBox41.TabIndex = 5;
            this.groupBox41.TabStop = false;
            this.groupBox41.Text = "Update Object";
            // 
            // btnUpdateObject_ProcessJob
            // 
            this.btnUpdateObject_ProcessJob.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateObject_ProcessJob.Location = new System.Drawing.Point(4, 25);
            this.btnUpdateObject_ProcessJob.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdateObject_ProcessJob.Name = "btnUpdateObject_ProcessJob";
            this.btnUpdateObject_ProcessJob.Size = new System.Drawing.Size(219, 27);
            this.btnUpdateObject_ProcessJob.TabIndex = 3;
            this.btnUpdateObject_ProcessJob.Text = "Update Object (PROCESSJOB)";
            this.btnUpdateObject_ProcessJob.UseVisualStyleBackColor = true;
            this.btnUpdateObject_ProcessJob.Click += new System.EventHandler(this.btnUpdateObject_ProcessJob_Click);
            // 
            // groupBox31
            // 
            this.groupBox31.Controls.Add(this.txtS14F9_CreateOBJ_Type);
            this.groupBox31.Controls.Add(this.label39);
            this.groupBox31.Controls.Add(this.label11);
            this.groupBox31.Controls.Add(this.label10);
            this.groupBox31.Controls.Add(this.label7);
            this.groupBox31.Controls.Add(this.label8);
            this.groupBox31.Controls.Add(this.label9);
            this.groupBox31.Controls.Add(this.btnCreate_Reply);
            this.groupBox31.Controls.Add(this.txtS14F9_SystemBytes_Recv);
            this.groupBox31.Controls.Add(this.txtS14F9_OBJACK_Send);
            this.groupBox31.Controls.Add(this.txtS14F9_OBJID_Recv);
            this.groupBox31.Controls.Add(this.txtS14F9_OBJSPEC_Recv);
            this.groupBox31.Controls.Add(this.txtS14F9_OBJTYPE_Recv);
            this.groupBox31.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox31.Location = new System.Drawing.Point(246, 5);
            this.groupBox31.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox31.Name = "groupBox31";
            this.groupBox31.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox31.Size = new System.Drawing.Size(334, 210);
            this.groupBox31.TabIndex = 4;
            this.groupBox31.TabStop = false;
            this.groupBox31.Text = "Recv S14F9 or S16F11 or S16F13 or S16F15";
            // 
            // txtS14F9_CreateOBJ_Type
            // 
            this.txtS14F9_CreateOBJ_Type.Enabled = false;
            this.txtS14F9_CreateOBJ_Type.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS14F9_CreateOBJ_Type.Location = new System.Drawing.Point(70, 146);
            this.txtS14F9_CreateOBJ_Type.Margin = new System.Windows.Forms.Padding(2);
            this.txtS14F9_CreateOBJ_Type.Name = "txtS14F9_CreateOBJ_Type";
            this.txtS14F9_CreateOBJ_Type.Size = new System.Drawing.Size(75, 27);
            this.txtS14F9_CreateOBJ_Type.TabIndex = 5;
            this.txtS14F9_CreateOBJ_Type.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(15, 150);
            this.label39.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(50, 19);
            this.label39.TabIndex = 4;
            this.label39.Text = "Type :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(14, 118);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 19);
            this.label11.TabIndex = 0;
            this.label11.Text = "SystemBytes :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(164, 150);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 19);
            this.label10.TabIndex = 0;
            this.label10.Text = "ReplyAck :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 86);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 19);
            this.label7.TabIndex = 0;
            this.label7.Text = "OBJID :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(16, 55);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 19);
            this.label8.TabIndex = 0;
            this.label8.Text = "OBJSPEC :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(17, 25);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 19);
            this.label9.TabIndex = 0;
            this.label9.Text = "OBJTYPE :";
            // 
            // btnCreate_Reply
            // 
            this.btnCreate_Reply.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate_Reply.Location = new System.Drawing.Point(119, 178);
            this.btnCreate_Reply.Margin = new System.Windows.Forms.Padding(2);
            this.btnCreate_Reply.Name = "btnCreate_Reply";
            this.btnCreate_Reply.Size = new System.Drawing.Size(172, 27);
            this.btnCreate_Reply.TabIndex = 2;
            this.btnCreate_Reply.Text = "ReplyMsg";
            this.btnCreate_Reply.UseVisualStyleBackColor = true;
            this.btnCreate_Reply.Click += new System.EventHandler(this.btnCreate_Reply_Click);
            // 
            // txtS14F9_SystemBytes_Recv
            // 
            this.txtS14F9_SystemBytes_Recv.Enabled = false;
            this.txtS14F9_SystemBytes_Recv.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS14F9_SystemBytes_Recv.Location = new System.Drawing.Point(119, 115);
            this.txtS14F9_SystemBytes_Recv.Margin = new System.Windows.Forms.Padding(2);
            this.txtS14F9_SystemBytes_Recv.Name = "txtS14F9_SystemBytes_Recv";
            this.txtS14F9_SystemBytes_Recv.Size = new System.Drawing.Size(174, 27);
            this.txtS14F9_SystemBytes_Recv.TabIndex = 1;
            this.txtS14F9_SystemBytes_Recv.Text = "0";
            this.txtS14F9_SystemBytes_Recv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtS14F9_OBJACK_Send
            // 
            this.txtS14F9_OBJACK_Send.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS14F9_OBJACK_Send.Location = new System.Drawing.Point(249, 147);
            this.txtS14F9_OBJACK_Send.Margin = new System.Windows.Forms.Padding(2);
            this.txtS14F9_OBJACK_Send.Name = "txtS14F9_OBJACK_Send";
            this.txtS14F9_OBJACK_Send.Size = new System.Drawing.Size(44, 27);
            this.txtS14F9_OBJACK_Send.TabIndex = 1;
            this.txtS14F9_OBJACK_Send.Text = "0";
            this.txtS14F9_OBJACK_Send.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtS14F9_OBJID_Recv
            // 
            this.txtS14F9_OBJID_Recv.Enabled = false;
            this.txtS14F9_OBJID_Recv.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS14F9_OBJID_Recv.Location = new System.Drawing.Point(119, 83);
            this.txtS14F9_OBJID_Recv.Margin = new System.Windows.Forms.Padding(2);
            this.txtS14F9_OBJID_Recv.Name = "txtS14F9_OBJID_Recv";
            this.txtS14F9_OBJID_Recv.Size = new System.Drawing.Size(174, 27);
            this.txtS14F9_OBJID_Recv.TabIndex = 1;
            this.txtS14F9_OBJID_Recv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtS14F9_OBJSPEC_Recv
            // 
            this.txtS14F9_OBJSPEC_Recv.Enabled = false;
            this.txtS14F9_OBJSPEC_Recv.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS14F9_OBJSPEC_Recv.Location = new System.Drawing.Point(119, 53);
            this.txtS14F9_OBJSPEC_Recv.Margin = new System.Windows.Forms.Padding(2);
            this.txtS14F9_OBJSPEC_Recv.Name = "txtS14F9_OBJSPEC_Recv";
            this.txtS14F9_OBJSPEC_Recv.Size = new System.Drawing.Size(174, 27);
            this.txtS14F9_OBJSPEC_Recv.TabIndex = 1;
            this.txtS14F9_OBJSPEC_Recv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtS14F9_OBJTYPE_Recv
            // 
            this.txtS14F9_OBJTYPE_Recv.Enabled = false;
            this.txtS14F9_OBJTYPE_Recv.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS14F9_OBJTYPE_Recv.Location = new System.Drawing.Point(119, 22);
            this.txtS14F9_OBJTYPE_Recv.Margin = new System.Windows.Forms.Padding(2);
            this.txtS14F9_OBJTYPE_Recv.Name = "txtS14F9_OBJTYPE_Recv";
            this.txtS14F9_OBJTYPE_Recv.Size = new System.Drawing.Size(174, 27);
            this.txtS14F9_OBJTYPE_Recv.TabIndex = 1;
            this.txtS14F9_OBJTYPE_Recv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox34);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(920, 235);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "Object2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox34
            // 
            this.groupBox34.Controls.Add(this.label23);
            this.groupBox34.Controls.Add(this.txtS14F11_OBJSPEC_Recv);
            this.groupBox34.Controls.Add(this.label21);
            this.groupBox34.Controls.Add(this.label22);
            this.groupBox34.Controls.Add(this.btnS14F12_Reply);
            this.groupBox34.Controls.Add(this.txtS14F11_SystemBytes_Recv);
            this.groupBox34.Controls.Add(this.txtS14F11_OBJACK_Send);
            this.groupBox34.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox34.Location = new System.Drawing.Point(4, 5);
            this.groupBox34.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox34.Name = "groupBox34";
            this.groupBox34.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox34.Size = new System.Drawing.Size(303, 210);
            this.groupBox34.TabIndex = 5;
            this.groupBox34.TabStop = false;
            this.groupBox34.Text = "Recv S14F11";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(19, 55);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(75, 19);
            this.label23.TabIndex = 3;
            this.label23.Text = "OBJSPEC :";
            // 
            // txtS14F11_OBJSPEC_Recv
            // 
            this.txtS14F11_OBJSPEC_Recv.Enabled = false;
            this.txtS14F11_OBJSPEC_Recv.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS14F11_OBJSPEC_Recv.Location = new System.Drawing.Point(119, 53);
            this.txtS14F11_OBJSPEC_Recv.Margin = new System.Windows.Forms.Padding(2);
            this.txtS14F11_OBJSPEC_Recv.Name = "txtS14F11_OBJSPEC_Recv";
            this.txtS14F11_OBJSPEC_Recv.Size = new System.Drawing.Size(174, 27);
            this.txtS14F11_OBJSPEC_Recv.TabIndex = 4;
            this.txtS14F11_OBJSPEC_Recv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(19, 118);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(103, 19);
            this.label21.TabIndex = 0;
            this.label21.Text = "SystemBytes :";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(19, 150);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(80, 19);
            this.label22.TabIndex = 0;
            this.label22.Text = "ReplyAck :";
            // 
            // btnS14F12_Reply
            // 
            this.btnS14F12_Reply.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnS14F12_Reply.Location = new System.Drawing.Point(119, 178);
            this.btnS14F12_Reply.Margin = new System.Windows.Forms.Padding(2);
            this.btnS14F12_Reply.Name = "btnS14F12_Reply";
            this.btnS14F12_Reply.Size = new System.Drawing.Size(172, 27);
            this.btnS14F12_Reply.TabIndex = 2;
            this.btnS14F12_Reply.Text = "ReplyMsg";
            this.btnS14F12_Reply.UseVisualStyleBackColor = true;
            this.btnS14F12_Reply.Click += new System.EventHandler(this.btnS14F12_Reply_Click);
            // 
            // txtS14F11_SystemBytes_Recv
            // 
            this.txtS14F11_SystemBytes_Recv.Enabled = false;
            this.txtS14F11_SystemBytes_Recv.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS14F11_SystemBytes_Recv.Location = new System.Drawing.Point(119, 115);
            this.txtS14F11_SystemBytes_Recv.Margin = new System.Windows.Forms.Padding(2);
            this.txtS14F11_SystemBytes_Recv.Name = "txtS14F11_SystemBytes_Recv";
            this.txtS14F11_SystemBytes_Recv.Size = new System.Drawing.Size(174, 27);
            this.txtS14F11_SystemBytes_Recv.TabIndex = 1;
            this.txtS14F11_SystemBytes_Recv.Text = "0";
            this.txtS14F11_SystemBytes_Recv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtS14F11_OBJACK_Send
            // 
            this.txtS14F11_OBJACK_Send.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS14F11_OBJACK_Send.Location = new System.Drawing.Point(119, 147);
            this.txtS14F11_OBJACK_Send.Margin = new System.Windows.Forms.Padding(2);
            this.txtS14F11_OBJACK_Send.Name = "txtS14F11_OBJACK_Send";
            this.txtS14F11_OBJACK_Send.Size = new System.Drawing.Size(174, 27);
            this.txtS14F11_OBJACK_Send.TabIndex = 1;
            this.txtS14F11_OBJACK_Send.Text = "0";
            this.txtS14F11_OBJACK_Send.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox37);
            this.tabPage3.Controls.Add(this.groupBox36);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(920, 235);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Object3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox37
            // 
            this.groupBox37.Controls.Add(this.label31);
            this.groupBox37.Controls.Add(this.label29);
            this.groupBox37.Controls.Add(this.label30);
            this.groupBox37.Controls.Add(this.btnProcessJobAlertNotify);
            this.groupBox37.Controls.Add(this.txtS16F7_OBJACKA);
            this.groupBox37.Controls.Add(this.txtS16F7_PRJOBMILESTONE);
            this.groupBox37.Controls.Add(this.txtS16F7_OBJID);
            this.groupBox37.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox37.Location = new System.Drawing.Point(248, 5);
            this.groupBox37.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox37.Name = "groupBox37";
            this.groupBox37.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox37.Size = new System.Drawing.Size(268, 210);
            this.groupBox37.TabIndex = 5;
            this.groupBox37.TabStop = false;
            this.groupBox37.Text = "Send S16F7";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(19, 86);
            this.label31.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(54, 19);
            this.label31.TabIndex = 0;
            this.label31.Text = "ACKA :";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(19, 55);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(139, 19);
            this.label29.TabIndex = 0;
            this.label29.Text = "PRJOBMILESTONE :";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(19, 25);
            this.label30.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(56, 19);
            this.label30.TabIndex = 0;
            this.label30.Text = "OBJID :";
            // 
            // btnProcessJobAlertNotify
            // 
            this.btnProcessJobAlertNotify.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessJobAlertNotify.Location = new System.Drawing.Point(52, 178);
            this.btnProcessJobAlertNotify.Margin = new System.Windows.Forms.Padding(2);
            this.btnProcessJobAlertNotify.Name = "btnProcessJobAlertNotify";
            this.btnProcessJobAlertNotify.Size = new System.Drawing.Size(172, 27);
            this.btnProcessJobAlertNotify.TabIndex = 2;
            this.btnProcessJobAlertNotify.Text = "ProcessJob Alert Notify";
            this.btnProcessJobAlertNotify.UseVisualStyleBackColor = true;
            this.btnProcessJobAlertNotify.Click += new System.EventHandler(this.btnProcessJobAlertNotify_Click);
            // 
            // txtS16F7_OBJACKA
            // 
            this.txtS16F7_OBJACKA.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS16F7_OBJACKA.Location = new System.Drawing.Point(158, 83);
            this.txtS16F7_OBJACKA.Margin = new System.Windows.Forms.Padding(2);
            this.txtS16F7_OBJACKA.Name = "txtS16F7_OBJACKA";
            this.txtS16F7_OBJACKA.Size = new System.Drawing.Size(96, 27);
            this.txtS16F7_OBJACKA.TabIndex = 1;
            this.txtS16F7_OBJACKA.Text = "true";
            this.txtS16F7_OBJACKA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtS16F7_PRJOBMILESTONE
            // 
            this.txtS16F7_PRJOBMILESTONE.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS16F7_PRJOBMILESTONE.Location = new System.Drawing.Point(158, 53);
            this.txtS16F7_PRJOBMILESTONE.Margin = new System.Windows.Forms.Padding(2);
            this.txtS16F7_PRJOBMILESTONE.Name = "txtS16F7_PRJOBMILESTONE";
            this.txtS16F7_PRJOBMILESTONE.Size = new System.Drawing.Size(96, 27);
            this.txtS16F7_PRJOBMILESTONE.TabIndex = 1;
            this.txtS16F7_PRJOBMILESTONE.Text = "2";
            this.txtS16F7_PRJOBMILESTONE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtS16F7_OBJID
            // 
            this.txtS16F7_OBJID.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS16F7_OBJID.Location = new System.Drawing.Point(158, 22);
            this.txtS16F7_OBJID.Margin = new System.Windows.Forms.Padding(2);
            this.txtS16F7_OBJID.Name = "txtS16F7_OBJID";
            this.txtS16F7_OBJID.Size = new System.Drawing.Size(96, 27);
            this.txtS16F7_OBJID.TabIndex = 1;
            this.txtS16F7_OBJID.Text = "PJ001";
            this.txtS16F7_OBJID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox36
            // 
            this.groupBox36.Controls.Add(this.label24);
            this.groupBox36.Controls.Add(this.label25);
            this.groupBox36.Controls.Add(this.label26);
            this.groupBox36.Controls.Add(this.btnProcessJobEventNotify);
            this.groupBox36.Controls.Add(this.txtS16F9_VIDs);
            this.groupBox36.Controls.Add(this.txtS16F9_PREVENTID);
            this.groupBox36.Controls.Add(this.txtS16F9_OBJID);
            this.groupBox36.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox36.Location = new System.Drawing.Point(12, 5);
            this.groupBox36.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox36.Name = "groupBox36";
            this.groupBox36.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox36.Size = new System.Drawing.Size(222, 210);
            this.groupBox36.TabIndex = 5;
            this.groupBox36.TabStop = false;
            this.groupBox36.Text = "Send S16F9";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(19, 86);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(74, 19);
            this.label24.TabIndex = 0;
            this.label24.Text = "VIDs(\",\") :";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(19, 55);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(93, 19);
            this.label25.TabIndex = 0;
            this.label25.Text = "PREVENTID :";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(19, 25);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(56, 19);
            this.label26.TabIndex = 0;
            this.label26.Text = "OBJID :";
            // 
            // btnProcessJobEventNotify
            // 
            this.btnProcessJobEventNotify.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProcessJobEventNotify.Location = new System.Drawing.Point(22, 178);
            this.btnProcessJobEventNotify.Margin = new System.Windows.Forms.Padding(2);
            this.btnProcessJobEventNotify.Name = "btnProcessJobEventNotify";
            this.btnProcessJobEventNotify.Size = new System.Drawing.Size(172, 27);
            this.btnProcessJobEventNotify.TabIndex = 2;
            this.btnProcessJobEventNotify.Text = "ProcessJob Event Notify";
            this.btnProcessJobEventNotify.UseVisualStyleBackColor = true;
            this.btnProcessJobEventNotify.Click += new System.EventHandler(this.btnProcessJobEventNotify_Click);
            // 
            // txtS16F9_VIDs
            // 
            this.txtS16F9_VIDs.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS16F9_VIDs.Location = new System.Drawing.Point(112, 83);
            this.txtS16F9_VIDs.Margin = new System.Windows.Forms.Padding(2);
            this.txtS16F9_VIDs.Name = "txtS16F9_VIDs";
            this.txtS16F9_VIDs.Size = new System.Drawing.Size(96, 27);
            this.txtS16F9_VIDs.TabIndex = 1;
            this.txtS16F9_VIDs.Text = "10,20";
            this.txtS16F9_VIDs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtS16F9_PREVENTID
            // 
            this.txtS16F9_PREVENTID.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS16F9_PREVENTID.Location = new System.Drawing.Point(112, 53);
            this.txtS16F9_PREVENTID.Margin = new System.Windows.Forms.Padding(2);
            this.txtS16F9_PREVENTID.Name = "txtS16F9_PREVENTID";
            this.txtS16F9_PREVENTID.Size = new System.Drawing.Size(96, 27);
            this.txtS16F9_PREVENTID.TabIndex = 1;
            this.txtS16F9_PREVENTID.Text = "2";
            this.txtS16F9_PREVENTID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtS16F9_OBJID
            // 
            this.txtS16F9_OBJID.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS16F9_OBJID.Location = new System.Drawing.Point(112, 22);
            this.txtS16F9_OBJID.Margin = new System.Windows.Forms.Padding(2);
            this.txtS16F9_OBJID.Name = "txtS16F9_OBJID";
            this.txtS16F9_OBJID.Size = new System.Drawing.Size(96, 27);
            this.txtS16F9_OBJID.TabIndex = 1;
            this.txtS16F9_OBJID.Text = "PJ001";
            this.txtS16F9_OBJID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox39);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(920, 235);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Object4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox39
            // 
            this.groupBox39.Controls.Add(this.btnControlJobCommandAck);
            this.groupBox39.Controls.Add(this.label38);
            this.groupBox39.Controls.Add(this.txtS16F28_ErrText);
            this.groupBox39.Controls.Add(this.label37);
            this.groupBox39.Controls.Add(this.txtS16F28_ErrCode);
            this.groupBox39.Controls.Add(this.label36);
            this.groupBox39.Controls.Add(this.txtS16F28_Acka);
            this.groupBox39.Controls.Add(this.txtS16F27_CPVal);
            this.groupBox39.Controls.Add(this.label34);
            this.groupBox39.Controls.Add(this.label35);
            this.groupBox39.Controls.Add(this.txtS16F27_CPName);
            this.groupBox39.Controls.Add(this.label33);
            this.groupBox39.Controls.Add(this.txtS16F27_Systembytes);
            this.groupBox39.Controls.Add(this.txtS16F27_JobCmd);
            this.groupBox39.Controls.Add(this.label28);
            this.groupBox39.Controls.Add(this.label32);
            this.groupBox39.Controls.Add(this.txtS16F27_JobId);
            this.groupBox39.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox39.Location = new System.Drawing.Point(4, 5);
            this.groupBox39.Name = "groupBox39";
            this.groupBox39.Size = new System.Drawing.Size(451, 202);
            this.groupBox39.TabIndex = 1;
            this.groupBox39.TabStop = false;
            this.groupBox39.Text = "Recv S16F27 (Control Job Command Request)";
            // 
            // btnControlJobCommandAck
            // 
            this.btnControlJobCommandAck.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnControlJobCommandAck.Location = new System.Drawing.Point(253, 25);
            this.btnControlJobCommandAck.Margin = new System.Windows.Forms.Padding(2);
            this.btnControlJobCommandAck.Name = "btnControlJobCommandAck";
            this.btnControlJobCommandAck.Size = new System.Drawing.Size(188, 27);
            this.btnControlJobCommandAck.TabIndex = 24;
            this.btnControlJobCommandAck.Text = "Reply S16F28";
            this.btnControlJobCommandAck.UseVisualStyleBackColor = true;
            this.btnControlJobCommandAck.Click += new System.EventHandler(this.btnControlJobCommandAck_Click);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(249, 136);
            this.label38.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(76, 19);
            this.label38.TabIndex = 22;
            this.label38.Text = "ERRTEXT :";
            // 
            // txtS16F28_ErrText
            // 
            this.txtS16F28_ErrText.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS16F28_ErrText.Location = new System.Drawing.Point(333, 132);
            this.txtS16F28_ErrText.Margin = new System.Windows.Forms.Padding(2);
            this.txtS16F28_ErrText.Name = "txtS16F28_ErrText";
            this.txtS16F28_ErrText.Size = new System.Drawing.Size(108, 27);
            this.txtS16F28_ErrText.TabIndex = 23;
            this.txtS16F28_ErrText.Text = "Status Down";
            this.txtS16F28_ErrText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(249, 102);
            this.label37.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(80, 19);
            this.label37.TabIndex = 20;
            this.label37.Text = "ERRCODE :";
            // 
            // txtS16F28_ErrCode
            // 
            this.txtS16F28_ErrCode.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS16F28_ErrCode.Location = new System.Drawing.Point(333, 98);
            this.txtS16F28_ErrCode.Margin = new System.Windows.Forms.Padding(2);
            this.txtS16F28_ErrCode.Name = "txtS16F28_ErrCode";
            this.txtS16F28_ErrCode.Size = new System.Drawing.Size(108, 27);
            this.txtS16F28_ErrCode.TabIndex = 21;
            this.txtS16F28_ErrCode.Text = "100";
            this.txtS16F28_ErrCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(249, 66);
            this.label36.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(54, 19);
            this.label36.TabIndex = 18;
            this.label36.Text = "ACKA :";
            // 
            // txtS16F28_Acka
            // 
            this.txtS16F28_Acka.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS16F28_Acka.Location = new System.Drawing.Point(333, 62);
            this.txtS16F28_Acka.Margin = new System.Windows.Forms.Padding(2);
            this.txtS16F28_Acka.Name = "txtS16F28_Acka";
            this.txtS16F28_Acka.Size = new System.Drawing.Size(108, 27);
            this.txtS16F28_Acka.TabIndex = 19;
            this.txtS16F28_Acka.Text = "true";
            this.txtS16F28_Acka.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtS16F27_CPVal
            // 
            this.txtS16F27_CPVal.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS16F27_CPVal.Location = new System.Drawing.Point(115, 127);
            this.txtS16F27_CPVal.Margin = new System.Windows.Forms.Padding(2);
            this.txtS16F27_CPVal.Name = "txtS16F27_CPVal";
            this.txtS16F27_CPVal.ReadOnly = true;
            this.txtS16F27_CPVal.Size = new System.Drawing.Size(113, 27);
            this.txtS16F27_CPVal.TabIndex = 17;
            this.txtS16F27_CPVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(14, 132);
            this.label34.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(59, 19);
            this.label34.TabIndex = 16;
            this.label34.Text = "CPVAL :";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(14, 98);
            this.label35.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(77, 19);
            this.label35.TabIndex = 14;
            this.label35.Text = "CPNAME :";
            // 
            // txtS16F27_CPName
            // 
            this.txtS16F27_CPName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS16F27_CPName.Location = new System.Drawing.Point(115, 94);
            this.txtS16F27_CPName.Margin = new System.Windows.Forms.Padding(2);
            this.txtS16F27_CPName.Name = "txtS16F27_CPName";
            this.txtS16F27_CPName.ReadOnly = true;
            this.txtS16F27_CPName.Size = new System.Drawing.Size(113, 27);
            this.txtS16F27_CPName.TabIndex = 15;
            this.txtS16F27_CPName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.label33.Location = new System.Drawing.Point(9, 163);
            this.label33.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(103, 19);
            this.label33.TabIndex = 12;
            this.label33.Text = "SystemBytes :";
            // 
            // txtS16F27_Systembytes
            // 
            this.txtS16F27_Systembytes.Enabled = false;
            this.txtS16F27_Systembytes.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS16F27_Systembytes.Location = new System.Drawing.Point(116, 159);
            this.txtS16F27_Systembytes.Margin = new System.Windows.Forms.Padding(2);
            this.txtS16F27_Systembytes.Name = "txtS16F27_Systembytes";
            this.txtS16F27_Systembytes.Size = new System.Drawing.Size(113, 27);
            this.txtS16F27_Systembytes.TabIndex = 13;
            this.txtS16F27_Systembytes.Text = "0";
            this.txtS16F27_Systembytes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtS16F27_JobCmd
            // 
            this.txtS16F27_JobCmd.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS16F27_JobCmd.Location = new System.Drawing.Point(115, 62);
            this.txtS16F27_JobCmd.Margin = new System.Windows.Forms.Padding(2);
            this.txtS16F27_JobCmd.Name = "txtS16F27_JobCmd";
            this.txtS16F27_JobCmd.ReadOnly = true;
            this.txtS16F27_JobCmd.Size = new System.Drawing.Size(113, 27);
            this.txtS16F27_JobCmd.TabIndex = 11;
            this.txtS16F27_JobCmd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(14, 66);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(97, 19);
            this.label28.TabIndex = 10;
            this.label28.Text = "CTLJOBCMD :";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(14, 33);
            this.label32.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(79, 19);
            this.label32.TabIndex = 8;
            this.label32.Text = "CTLJOBID :";
            // 
            // txtS16F27_JobId
            // 
            this.txtS16F27_JobId.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS16F27_JobId.Location = new System.Drawing.Point(115, 30);
            this.txtS16F27_JobId.Margin = new System.Windows.Forms.Padding(2);
            this.txtS16F27_JobId.Name = "txtS16F27_JobId";
            this.txtS16F27_JobId.ReadOnly = true;
            this.txtS16F27_JobId.Size = new System.Drawing.Size(113, 27);
            this.txtS16F27_JobId.TabIndex = 9;
            this.txtS16F27_JobId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox40);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(920, 235);
            this.tabPage5.TabIndex = 5;
            this.tabPage5.Text = "Object5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox40
            // 
            this.groupBox40.Controls.Add(this.btnCarrierActionAck);
            this.groupBox40.Controls.Add(this.label40);
            this.groupBox40.Controls.Add(this.txtS3F18_ErrText);
            this.groupBox40.Controls.Add(this.label41);
            this.groupBox40.Controls.Add(this.txtS3F18_ErrCode);
            this.groupBox40.Controls.Add(this.label42);
            this.groupBox40.Controls.Add(this.txtS3F18_Caack);
            this.groupBox40.Controls.Add(this.label44);
            this.groupBox40.Controls.Add(this.txtS3F17_PTN_Recv);
            this.groupBox40.Controls.Add(this.label45);
            this.groupBox40.Controls.Add(this.txtS3F17_SystemBytes_Recv);
            this.groupBox40.Controls.Add(this.txtS3F17_CARRIERID_Recv);
            this.groupBox40.Controls.Add(this.label46);
            this.groupBox40.Controls.Add(this.label47);
            this.groupBox40.Controls.Add(this.txtS3F17_CARRIERACTION_Recv);
            this.groupBox40.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox40.Location = new System.Drawing.Point(3, 14);
            this.groupBox40.Name = "groupBox40";
            this.groupBox40.Size = new System.Drawing.Size(586, 202);
            this.groupBox40.TabIndex = 2;
            this.groupBox40.TabStop = false;
            this.groupBox40.Text = "Recv S3F17 (Carrier Action Request)";
            // 
            // btnCarrierActionAck
            // 
            this.btnCarrierActionAck.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCarrierActionAck.Location = new System.Drawing.Point(334, 25);
            this.btnCarrierActionAck.Margin = new System.Windows.Forms.Padding(2);
            this.btnCarrierActionAck.Name = "btnCarrierActionAck";
            this.btnCarrierActionAck.Size = new System.Drawing.Size(188, 27);
            this.btnCarrierActionAck.TabIndex = 24;
            this.btnCarrierActionAck.Text = "Reply S3F18";
            this.btnCarrierActionAck.UseVisualStyleBackColor = true;
            this.btnCarrierActionAck.Click += new System.EventHandler(this.btnCarrierActionAck_Click);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(330, 136);
            this.label40.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(76, 19);
            this.label40.TabIndex = 22;
            this.label40.Text = "ERRTEXT :";
            // 
            // txtS3F18_ErrText
            // 
            this.txtS3F18_ErrText.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS3F18_ErrText.Location = new System.Drawing.Point(414, 132);
            this.txtS3F18_ErrText.Margin = new System.Windows.Forms.Padding(2);
            this.txtS3F18_ErrText.Name = "txtS3F18_ErrText";
            this.txtS3F18_ErrText.Size = new System.Drawing.Size(108, 27);
            this.txtS3F18_ErrText.TabIndex = 23;
            this.txtS3F18_ErrText.Text = "Status Down";
            this.txtS3F18_ErrText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(330, 102);
            this.label41.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(80, 19);
            this.label41.TabIndex = 20;
            this.label41.Text = "ERRCODE :";
            // 
            // txtS3F18_ErrCode
            // 
            this.txtS3F18_ErrCode.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS3F18_ErrCode.Location = new System.Drawing.Point(414, 98);
            this.txtS3F18_ErrCode.Margin = new System.Windows.Forms.Padding(2);
            this.txtS3F18_ErrCode.Name = "txtS3F18_ErrCode";
            this.txtS3F18_ErrCode.Size = new System.Drawing.Size(108, 27);
            this.txtS3F18_ErrCode.TabIndex = 21;
            this.txtS3F18_ErrCode.Text = "100";
            this.txtS3F18_ErrCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(330, 66);
            this.label42.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(62, 19);
            this.label42.TabIndex = 18;
            this.label42.Text = "CACKA :";
            // 
            // txtS3F18_Caack
            // 
            this.txtS3F18_Caack.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS3F18_Caack.Location = new System.Drawing.Point(414, 62);
            this.txtS3F18_Caack.Margin = new System.Windows.Forms.Padding(2);
            this.txtS3F18_Caack.Name = "txtS3F18_Caack";
            this.txtS3F18_Caack.Size = new System.Drawing.Size(108, 27);
            this.txtS3F18_Caack.TabIndex = 19;
            this.txtS3F18_Caack.Text = "0";
            this.txtS3F18_Caack.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(14, 98);
            this.label44.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(45, 19);
            this.label44.TabIndex = 14;
            this.label44.Text = "PTN :";
            // 
            // txtS3F17_PTN_Recv
            // 
            this.txtS3F17_PTN_Recv.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS3F17_PTN_Recv.Location = new System.Drawing.Point(136, 97);
            this.txtS3F17_PTN_Recv.Margin = new System.Windows.Forms.Padding(2);
            this.txtS3F17_PTN_Recv.Name = "txtS3F17_PTN_Recv";
            this.txtS3F17_PTN_Recv.ReadOnly = true;
            this.txtS3F17_PTN_Recv.Size = new System.Drawing.Size(167, 27);
            this.txtS3F17_PTN_Recv.TabIndex = 15;
            this.txtS3F17_PTN_Recv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.label45.Location = new System.Drawing.Point(14, 136);
            this.label45.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(103, 19);
            this.label45.TabIndex = 12;
            this.label45.Text = "SystemBytes :";
            // 
            // txtS3F17_SystemBytes_Recv
            // 
            this.txtS3F17_SystemBytes_Recv.Enabled = false;
            this.txtS3F17_SystemBytes_Recv.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS3F17_SystemBytes_Recv.Location = new System.Drawing.Point(136, 134);
            this.txtS3F17_SystemBytes_Recv.Margin = new System.Windows.Forms.Padding(2);
            this.txtS3F17_SystemBytes_Recv.Name = "txtS3F17_SystemBytes_Recv";
            this.txtS3F17_SystemBytes_Recv.Size = new System.Drawing.Size(167, 27);
            this.txtS3F17_SystemBytes_Recv.TabIndex = 13;
            this.txtS3F17_SystemBytes_Recv.Text = "0";
            this.txtS3F17_SystemBytes_Recv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtS3F17_CARRIERID_Recv
            // 
            this.txtS3F17_CARRIERID_Recv.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS3F17_CARRIERID_Recv.Location = new System.Drawing.Point(136, 65);
            this.txtS3F17_CARRIERID_Recv.Margin = new System.Windows.Forms.Padding(2);
            this.txtS3F17_CARRIERID_Recv.Name = "txtS3F17_CARRIERID_Recv";
            this.txtS3F17_CARRIERID_Recv.ReadOnly = true;
            this.txtS3F17_CARRIERID_Recv.Size = new System.Drawing.Size(167, 27);
            this.txtS3F17_CARRIERID_Recv.TabIndex = 11;
            this.txtS3F17_CARRIERID_Recv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(14, 66);
            this.label46.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(88, 19);
            this.label46.TabIndex = 10;
            this.label46.Text = "CARRIERID :";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(14, 33);
            this.label47.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(126, 19);
            this.label47.TabIndex = 8;
            this.label47.Text = "CARRIERACTION :";
            // 
            // txtS3F17_CARRIERACTION_Recv
            // 
            this.txtS3F17_CARRIERACTION_Recv.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtS3F17_CARRIERACTION_Recv.Location = new System.Drawing.Point(136, 33);
            this.txtS3F17_CARRIERACTION_Recv.Margin = new System.Windows.Forms.Padding(2);
            this.txtS3F17_CARRIERACTION_Recv.Name = "txtS3F17_CARRIERACTION_Recv";
            this.txtS3F17_CARRIERACTION_Recv.ReadOnly = true;
            this.txtS3F17_CARRIERACTION_Recv.Size = new System.Drawing.Size(167, 27);
            this.txtS3F17_CARRIERACTION_Recv.TabIndex = 9;
            this.txtS3F17_CARRIERACTION_Recv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tabSendSECSMessage
            // 
            this.tabSendSECSMessage.Controls.Add(this.btnCustom);
            this.tabSendSECSMessage.Location = new System.Drawing.Point(4, 22);
            this.tabSendSECSMessage.Name = "tabSendSECSMessage";
            this.tabSendSECSMessage.Padding = new System.Windows.Forms.Padding(3);
            this.tabSendSECSMessage.Size = new System.Drawing.Size(928, 261);
            this.tabSendSECSMessage.TabIndex = 12;
            this.tabSendSECSMessage.Text = "SendSECSMessage";
            this.tabSendSECSMessage.UseVisualStyleBackColor = true;
            // 
            // btnCustom
            // 
            this.btnCustom.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustom.Location = new System.Drawing.Point(17, 25);
            this.btnCustom.Margin = new System.Windows.Forms.Padding(2);
            this.btnCustom.Name = "btnCustom";
            this.btnCustom.Size = new System.Drawing.Size(120, 54);
            this.btnCustom.TabIndex = 4;
            this.btnCustom.Text = "Custom Send SECS Message";
            this.btnCustom.UseVisualStyleBackColor = true;
            this.btnCustom.Click += new System.EventHandler(this.btnCustom_Click);
            // 
            // tabLoopback
            // 
            this.tabLoopback.Controls.Add(this.groupBox30);
            this.tabLoopback.Controls.Add(this.groupBox32);
            this.tabLoopback.Location = new System.Drawing.Point(4, 22);
            this.tabLoopback.Name = "tabLoopback";
            this.tabLoopback.Size = new System.Drawing.Size(928, 261);
            this.tabLoopback.TabIndex = 13;
            this.tabLoopback.Text = "Loopback";
            this.tabLoopback.UseVisualStyleBackColor = true;
            // 
            // groupBox30
            // 
            this.groupBox30.Controls.Add(this.txtABS_Recv);
            this.groupBox30.Controls.Add(this.txtABS_RecvSystemBytes);
            this.groupBox30.Controls.Add(this.label2);
            this.groupBox30.Controls.Add(this.lblABS_Check);
            this.groupBox30.Controls.Add(this.label4);
            this.groupBox30.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox30.Location = new System.Drawing.Point(5, 74);
            this.groupBox30.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox30.Name = "groupBox30";
            this.groupBox30.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox30.Size = new System.Drawing.Size(297, 136);
            this.groupBox30.TabIndex = 15;
            this.groupBox30.TabStop = false;
            this.groupBox30.Text = "Receive Message";
            // 
            // txtABS_Recv
            // 
            this.txtABS_Recv.Enabled = false;
            this.txtABS_Recv.Location = new System.Drawing.Point(127, 19);
            this.txtABS_Recv.Margin = new System.Windows.Forms.Padding(2);
            this.txtABS_Recv.MaxLength = 10;
            this.txtABS_Recv.Name = "txtABS_Recv";
            this.txtABS_Recv.Size = new System.Drawing.Size(155, 27);
            this.txtABS_Recv.TabIndex = 5;
            // 
            // txtABS_RecvSystemBytes
            // 
            this.txtABS_RecvSystemBytes.Enabled = false;
            this.txtABS_RecvSystemBytes.Location = new System.Drawing.Point(127, 50);
            this.txtABS_RecvSystemBytes.Margin = new System.Windows.Forms.Padding(2);
            this.txtABS_RecvSystemBytes.MaxLength = 10;
            this.txtABS_RecvSystemBytes.Name = "txtABS_RecvSystemBytes";
            this.txtABS_RecvSystemBytes.Size = new System.Drawing.Size(155, 27);
            this.txtABS_RecvSystemBytes.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "ABS：";
            // 
            // lblABS_Check
            // 
            this.lblABS_Check.AutoSize = true;
            this.lblABS_Check.Location = new System.Drawing.Point(4, 101);
            this.lblABS_Check.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblABS_Check.Name = "lblABS_Check";
            this.lblABS_Check.Size = new System.Drawing.Size(0, 19);
            this.lblABS_Check.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 53);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 19);
            this.label4.TabIndex = 4;
            this.label4.Text = "SystemBytes：";
            // 
            // groupBox32
            // 
            this.groupBox32.Controls.Add(this.btnLoopbackDiagnostic);
            this.groupBox32.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox32.Location = new System.Drawing.Point(5, 2);
            this.groupBox32.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox32.Name = "groupBox32";
            this.groupBox32.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox32.Size = new System.Drawing.Size(228, 65);
            this.groupBox32.TabIndex = 14;
            this.groupBox32.TabStop = false;
            this.groupBox32.Text = "S2F25";
            // 
            // btnLoopbackDiagnostic
            // 
            this.btnLoopbackDiagnostic.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoopbackDiagnostic.Location = new System.Drawing.Point(14, 24);
            this.btnLoopbackDiagnostic.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoopbackDiagnostic.Name = "btnLoopbackDiagnostic";
            this.btnLoopbackDiagnostic.Size = new System.Drawing.Size(194, 27);
            this.btnLoopbackDiagnostic.TabIndex = 2;
            this.btnLoopbackDiagnostic.Text = "Loopback Diagnostic";
            this.btnLoopbackDiagnostic.UseVisualStyleBackColor = true;
            this.btnLoopbackDiagnostic.Click += new System.EventHandler(this.btnLoopbackDiagnostic_Click);
            // 
            // pnlInit
            // 
            this.pnlInit.Controls.Add(this.groupBox4);
            this.pnlInit.Controls.Add(this.groupBox3);
            this.pnlInit.Controls.Add(this.groupBox2);
            this.pnlInit.Controls.Add(this.groupBox1);
            this.pnlInit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInit.Location = new System.Drawing.Point(2, 2);
            this.pnlInit.Margin = new System.Windows.Forms.Padding(2);
            this.pnlInit.Name = "pnlInit";
            this.pnlInit.Size = new System.Drawing.Size(217, 348);
            this.pnlInit.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnOnLineLocal);
            this.groupBox4.Controls.Add(this.btnOnLineRemote);
            this.groupBox4.Controls.Add(this.btnOffLine);
            this.groupBox4.Controls.Add(this.lblControlState);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(0, 214);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(217, 126);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Control State";
            // 
            // btnOnLineLocal
            // 
            this.btnOnLineLocal.Location = new System.Drawing.Point(2, 86);
            this.btnOnLineLocal.Margin = new System.Windows.Forms.Padding(2);
            this.btnOnLineLocal.Name = "btnOnLineLocal";
            this.btnOnLineLocal.Size = new System.Drawing.Size(94, 33);
            this.btnOnLineLocal.TabIndex = 4;
            this.btnOnLineLocal.Text = "On Line Local";
            this.btnOnLineLocal.UseVisualStyleBackColor = true;
            this.btnOnLineLocal.Click += new System.EventHandler(this.btnOnLineLocal_Click);
            // 
            // btnOnLineRemote
            // 
            this.btnOnLineRemote.Location = new System.Drawing.Point(103, 86);
            this.btnOnLineRemote.Margin = new System.Windows.Forms.Padding(2);
            this.btnOnLineRemote.Name = "btnOnLineRemote";
            this.btnOnLineRemote.Size = new System.Drawing.Size(110, 34);
            this.btnOnLineRemote.TabIndex = 3;
            this.btnOnLineRemote.Text = "On Line Remote";
            this.btnOnLineRemote.UseVisualStyleBackColor = true;
            this.btnOnLineRemote.Click += new System.EventHandler(this.btnOnLineRemote_Click);
            // 
            // btnOffLine
            // 
            this.btnOffLine.Location = new System.Drawing.Point(3, 49);
            this.btnOffLine.Margin = new System.Windows.Forms.Padding(2);
            this.btnOffLine.Name = "btnOffLine";
            this.btnOffLine.Size = new System.Drawing.Size(212, 33);
            this.btnOffLine.TabIndex = 2;
            this.btnOffLine.Text = "Off Line";
            this.btnOffLine.UseVisualStyleBackColor = true;
            this.btnOffLine.Click += new System.EventHandler(this.btnOffLine_Click);
            // 
            // lblControlState
            // 
            this.lblControlState.BackColor = System.Drawing.Color.White;
            this.lblControlState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblControlState.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblControlState.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControlState.Location = new System.Drawing.Point(2, 19);
            this.lblControlState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblControlState.Name = "lblControlState";
            this.lblControlState.Size = new System.Drawing.Size(213, 28);
            this.lblControlState.TabIndex = 0;
            this.lblControlState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDisableComm);
            this.groupBox3.Controls.Add(this.btnEnableComm);
            this.groupBox3.Controls.Add(this.lblCommunicatingState);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(0, 128);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(217, 86);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Communicating State";
            // 
            // btnDisableComm
            // 
            this.btnDisableComm.Enabled = false;
            this.btnDisableComm.Location = new System.Drawing.Point(110, 51);
            this.btnDisableComm.Margin = new System.Windows.Forms.Padding(2);
            this.btnDisableComm.Name = "btnDisableComm";
            this.btnDisableComm.Size = new System.Drawing.Size(99, 28);
            this.btnDisableComm.TabIndex = 2;
            this.btnDisableComm.Text = "Disable Comm";
            this.btnDisableComm.UseVisualStyleBackColor = true;
            this.btnDisableComm.Click += new System.EventHandler(this.btnDisableComm_Click);
            // 
            // btnEnableComm
            // 
            this.btnEnableComm.Location = new System.Drawing.Point(4, 51);
            this.btnEnableComm.Margin = new System.Windows.Forms.Padding(2);
            this.btnEnableComm.Name = "btnEnableComm";
            this.btnEnableComm.Size = new System.Drawing.Size(100, 28);
            this.btnEnableComm.TabIndex = 1;
            this.btnEnableComm.Text = "Enable Comm";
            this.btnEnableComm.UseVisualStyleBackColor = true;
            this.btnEnableComm.Click += new System.EventHandler(this.btnEnableComm_Click);
            // 
            // lblCommunicatingState
            // 
            this.lblCommunicatingState.BackColor = System.Drawing.Color.White;
            this.lblCommunicatingState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCommunicatingState.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCommunicatingState.Location = new System.Drawing.Point(2, 19);
            this.lblCommunicatingState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCommunicatingState.Name = "lblCommunicatingState";
            this.lblCommunicatingState.Size = new System.Drawing.Size(213, 28);
            this.lblCommunicatingState.TabIndex = 0;
            this.lblCommunicatingState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Controls.Add(this.btnStart);
            this.groupBox2.Controls.Add(this.lblSecsDriverStatus);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 50);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(217, 78);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SECS Driver Connect Status";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(110, 48);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(99, 26);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(4, 48);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 26);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblSecsDriverStatus
            // 
            this.lblSecsDriverStatus.BackColor = System.Drawing.Color.Red;
            this.lblSecsDriverStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSecsDriverStatus.Location = new System.Drawing.Point(2, 19);
            this.lblSecsDriverStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSecsDriverStatus.Name = "lblSecsDriverStatus";
            this.lblSecsDriverStatus.Size = new System.Drawing.Size(213, 26);
            this.lblSecsDriverStatus.TabIndex = 0;
            this.lblSecsDriverStatus.Text = "Disconnect";
            this.lblSecsDriverStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblInitResult);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(217, 50);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DIASECSGEM Init Result";
            // 
            // lblInitResult
            // 
            this.lblInitResult.BackColor = System.Drawing.Color.White;
            this.lblInitResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInitResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInitResult.Location = new System.Drawing.Point(2, 19);
            this.lblInitResult.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInitResult.Name = "lblInitResult";
            this.lblInitResult.Size = new System.Drawing.Size(213, 29);
            this.lblInitResult.TabIndex = 0;
            this.lblInitResult.Text = "Result";
            this.lblInitResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Interval = 500;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 667);
            this.ControlBox = false;
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "DIASECSGEM_Equipment";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.tbcLog.ResumeLayout(false);
            this.tbpDriverLog.ResumeLayout(false);
            this.tbpGemLog.ResumeLayout(false);
            this.tbpSecsLog.ResumeLayout(false);
            this.tbpEQPLog.ResumeLayout(false);
            this.tbcFunctions.ResumeLayout(false);
            this.tbpSystemID.ResumeLayout(false);
            this.tbcSystemID.ResumeLayout(false);
            this.tbpSysSV.ResumeLayout(false);
            this.groupBox43.ResumeLayout(false);
            this.groupBox43.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.tbpSysEC.ResumeLayout(false);
            this.groupBox27.ResumeLayout(false);
            this.groupBox27.PerformLayout();
            this.groupBox42.ResumeLayout(false);
            this.groupBox42.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.tbpEvent.ResumeLayout(false);
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.flpEventResult.ResumeLayout(false);
            this.flpEventResult.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tbpAlarm.ResumeLayout(false);
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.flpAlarmResult.ResumeLayout(false);
            this.flpAlarmResult.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tbpSV.ResumeLayout(false);
            this.tbpSV.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.flpSVResult.ResumeLayout(false);
            this.flpSVResult.PerformLayout();
            this.tbpEC.ResumeLayout(false);
            this.groupBox24.ResumeLayout(false);
            this.groupBox24.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNewECs)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.flpECResult.ResumeLayout(false);
            this.flpECResult.PerformLayout();
            this.tbpRemote.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCPs)).EndInit();
            this.tabEnhanced.ResumeLayout(false);
            this.groupBox28.ResumeLayout(false);
            this.groupBox28.PerformLayout();
            this.tabPP.ResumeLayout(false);
            this.tabPP.PerformLayout();
            this.groupBox22.ResumeLayout(false);
            this.groupBox22.PerformLayout();
            this.groupBox26.ResumeLayout(false);
            this.groupBox26.PerformLayout();
            this.groupBox25.ResumeLayout(false);
            this.groupBox25.PerformLayout();
            this.tabControl3.ResumeLayout(false);
            this.tbPPSetting.ResumeLayout(false);
            this.tbPPSetting.PerformLayout();
            this.tbPPSlotMap.ResumeLayout(false);
            this.tbAngle.ResumeLayout(false);
            this.tbAngle.PerformLayout();
            this.tbPPCofficient.ResumeLayout(false);
            this.tbPPCofficient.PerformLayout();
            this.tbMisc.ResumeLayout(false);
            this.tbMisc.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabClock.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.tabEQPTerminalServices.ResumeLayout(false);
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.tabHostSetsCONTROLStateModel.ResumeLayout(false);
            this.groupBox29.ResumeLayout(false);
            this.groupBox29.PerformLayout();
            this.tabObject.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox44.ResumeLayout(false);
            this.groupBox44.PerformLayout();
            this.groupBox41.ResumeLayout(false);
            this.groupBox31.ResumeLayout(false);
            this.groupBox31.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox34.ResumeLayout(false);
            this.groupBox34.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox37.ResumeLayout(false);
            this.groupBox37.PerformLayout();
            this.groupBox36.ResumeLayout(false);
            this.groupBox36.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox39.ResumeLayout(false);
            this.groupBox39.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.groupBox40.ResumeLayout(false);
            this.groupBox40.PerformLayout();
            this.tabSendSECSMessage.ResumeLayout(false);
            this.tabLoopback.ResumeLayout(false);
            this.groupBox30.ResumeLayout(false);
            this.groupBox30.PerformLayout();
            this.groupBox32.ResumeLayout(false);
            this.pnlInit.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TabControl tbcLog;
        private System.Windows.Forms.TabPage tbpDriverLog;
        private System.Windows.Forms.TabPage tbpSecsLog;
        private System.Windows.Forms.TabControl tbcFunctions;
        private System.Windows.Forms.TabPage tbpSystemID;
        private System.Windows.Forms.TabPage tbpEvent;
        private System.Windows.Forms.Panel pnlInit;
        private System.Windows.Forms.RichTextBox rtbDriverLog;
        private System.Windows.Forms.RichTextBox rtbSecsLog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblInitResult;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblSecsDriverStatus;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnEnableComm;
        private System.Windows.Forms.Label lblCommunicatingState;
        private System.Windows.Forms.Button btnDisableComm;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblControlState;
        private System.Windows.Forms.Button btnOnLineLocal;
        private System.Windows.Forms.Button btnOnLineRemote;
        private System.Windows.Forms.Button btnOffLine;
        private System.Windows.Forms.ToolStripMenuItem tsmiEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiClearDriverLog;
        private System.Windows.Forms.ToolStripMenuItem tsmiClearSECSLog;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.TabPage tbpGemLog;
        private System.Windows.Forms.RichTextBox rtbGemLog;
        private System.Windows.Forms.ToolStripMenuItem tsmiClearGemLog;
        private System.Windows.Forms.TextBox txtEventID;
        private System.Windows.Forms.Label lblEventID;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnEventReport;
        private System.Windows.Forms.TabPage tbpAlarm;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton rdoAlarmSet;
        private System.Windows.Forms.Label lblAlarmState;
        private System.Windows.Forms.Label lblAlarmID;
        private System.Windows.Forms.RadioButton rdoAlarmClean;
        private System.Windows.Forms.TextBox txtAlarmID;
        private System.Windows.Forms.Button btnAlarmReportSend;
        private System.Windows.Forms.FlowLayoutPanel flpAlarmResult;
        private System.Windows.Forms.Label lblAlarmResultCaption;
        private System.Windows.Forms.Label lblAlarmResultText;
        private System.Windows.Forms.FlowLayoutPanel flpEventResult;
        private System.Windows.Forms.Label lblEventResultCaption;
        private System.Windows.Forms.Label lblEventResultText;
        private System.Windows.Forms.TabPage tbpSV;
        private System.Windows.Forms.FlowLayoutPanel flpSVResult;
        private System.Windows.Forms.Label lblSVResultCaption;
        private System.Windows.Forms.Label lblSVResultText;
        private System.Windows.Forms.Button btnUpdateSV;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label lblUpdateSVFormat;
        private System.Windows.Forms.ComboBox cmbUpdateSVFormat;
        private System.Windows.Forms.TextBox txtUpdateSVID;
        private System.Windows.Forms.Label lblUpdateSVID;
        private System.Windows.Forms.TextBox txtUpdateSV;
        private System.Windows.Forms.Label lblUpdateSV;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label lblGetSVFormat;
        private System.Windows.Forms.TextBox txtGetSVID;
        private System.Windows.Forms.Label lblGetSVID;
        private System.Windows.Forms.TextBox txtGetSVFormat;
        private System.Windows.Forms.Button btnGetSV;
        private System.Windows.Forms.TextBox txtGetSV;
        private System.Windows.Forms.Label lblGetSV;
        private System.Windows.Forms.TabPage tbpEC;
        private System.Windows.Forms.FlowLayoutPanel flpECResult;
        private System.Windows.Forms.Label lblECResultCaption;
        private System.Windows.Forms.Label lblECResultText;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button btnGetEC;
        private System.Windows.Forms.TextBox txtGetEC;
        private System.Windows.Forms.Label lblGetEC;
        private System.Windows.Forms.TextBox txtGetECFormat;
        private System.Windows.Forms.Label lblGetECFormat;
        private System.Windows.Forms.TextBox txtGetECID;
        private System.Windows.Forms.Label lblGetECID;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TextBox txtUpdateEC;
        private System.Windows.Forms.Label lblUpdateEC;
        private System.Windows.Forms.Label lblUpdateECFormat;
        private System.Windows.Forms.ComboBox cmbUpdateECFormat;
        private System.Windows.Forms.TextBox txtUpdateECID;
        private System.Windows.Forms.Label lblUpdateECID;
        private System.Windows.Forms.Button btnUpdateEC;
        private System.Windows.Forms.Button btnUpdateListTypeSV;
        private System.Windows.Forms.TabPage tbpRemote;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label lblRCMD;
        private System.Windows.Forms.TextBox txtRCMD;
        private System.Windows.Forms.TextBox txtOBJSPEC;
        private System.Windows.Forms.Label lblOBJSPEC;
        private System.Windows.Forms.Button btnReplyMessage;
        private System.Windows.Forms.TextBox txtMessageName;
        private System.Windows.Forms.Label lblMessageName;
        private System.Windows.Forms.Label lblCPs;
        private System.Windows.Forms.DataGridView dgvCPs;
        private System.Windows.Forms.Label lblReplyHcAck;
        private System.Windows.Forms.TextBox txtReplyHcAck;
        private System.Windows.Forms.Label lblSystemBytes;
        private System.Windows.Forms.TextBox txtSystemBytes;
        private System.Windows.Forms.CheckBox chkContinueUpdateSV;
        private System.Windows.Forms.TabControl tbcSystemID;
        private System.Windows.Forms.TabPage tbpSysSV;
        private System.Windows.Forms.TabPage tbpSysEC;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Label lblSysSV_ControlState;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Label lblSysSV_SpoolState;
        private System.Windows.Forms.Label lblSysSV_SpoolCountTotal;
        private System.Windows.Forms.Label lblSysSV_SpoolCountActual;
        private System.Windows.Forms.Label lblSysSV_SpoolUnloadSubState;
        private System.Windows.Forms.Label lblSysSV_SpoolLoadSubState;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Label lblSysEC_MaxSpoolTransmit;
        private System.Windows.Forms.Label lblSysEC_OverWriteSpool;
        private System.Windows.Forms.Label lblSysEC_ConfigSpool;
        private System.Windows.Forms.Label lblSysSV_SpoolFullTime;
        private System.Windows.Forms.Label lblSysSV_SpoolStartTime;
        private System.Windows.Forms.TabPage tbpEQPLog;
        private System.Windows.Forms.RichTextBox rtbEqpLog;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.Label lblSysSV_Clock;
        private System.Windows.Forms.TabPage tabClock;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.TextBox txtHostDownloadDateTime;
        private System.Windows.Forms.TextBox txtSetSystemDateTimeSuccess;
        private System.Windows.Forms.Label lblHostDownloadDateTime;
        private System.Windows.Forms.Label lblSetSystemDateTimeSuccess;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.Button btnEQPDateAndTimeRequest;
        private System.Windows.Forms.TextBox txtClockMessageName;
        private System.Windows.Forms.Label lblClockMessageName;
        private System.Windows.Forms.TabPage tabEQPTerminalServices;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Label lblTID;
        private System.Windows.Forms.Button btnTerminalRequest;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.TextBox txtTID;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.TextBox txtTerminalDisplayType;
        private System.Windows.Forms.TextBox txtTerminalNumber;
        private System.Windows.Forms.TextBox txtTerminalText;
        private System.Windows.Forms.Label lblTerminalDisplayType;
        private System.Windows.Forms.Label lblTerminalNumber;
        private System.Windows.Forms.Label lblTerminalSystemBytes;
        private System.Windows.Forms.Button btnMessageRecognition;
        private System.Windows.Forms.GroupBox groupBox23;
        private System.Windows.Forms.RadioButton rdoProcessState_Idle;
        private System.Windows.Forms.RadioButton rdoProcessState_Initial;
        private System.Windows.Forms.RadioButton rdoProcessState_Run;
        private System.Windows.Forms.RadioButton rdoProcessState_Down;
        private System.Windows.Forms.RadioButton rdoProcessState_Pause;
        private System.Windows.Forms.RadioButton rdoProcessState_Stop;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.TextBox txtEqpCurrentAlarmSet;
        private System.Windows.Forms.Label lblTerminalText;
        private System.Windows.Forms.TextBox txtTerminalSystemBytes;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.Button btnTerminalACKC10;
        private System.Windows.Forms.Label lblTerminalACKC10;
        private System.Windows.Forms.TextBox txtTerminalACKC10;
        private System.Windows.Forms.RadioButton rdoMultiblockNotAllowedFalse;
        private System.Windows.Forms.Label lblMultiblockNotAllowed;
        private System.Windows.Forms.RadioButton rdoMultiblockNotAllowedTrue;
        private System.Windows.Forms.GroupBox groupBox24;
        private System.Windows.Forms.TextBox txtNewECsSystemBytes;
        private System.Windows.Forms.Label lblNewECsSystemBytes;
        private System.Windows.Forms.DataGridView dgvNewECs;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Format;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.Button btnNewECsReply;
        private System.Windows.Forms.TextBox txtEAC;
        private System.Windows.Forms.Label lblEAC;
        private System.Windows.Forms.TabPage tabPP;
        private System.Windows.Forms.Label lblPPIDList;
        private System.Windows.Forms.ComboBox cmbChangedPPType;
        private System.Windows.Forms.GroupBox groupBox25;
        private System.Windows.Forms.TextBox txtPPID;
        private System.Windows.Forms.Label lblPPID;
        private System.Windows.Forms.Button btnPPChanged;
        private System.Windows.Forms.Button btnSendS7F25;
        private System.Windows.Forms.Button btnSendS7F23;
        private System.Windows.Forms.Button btnSendS7F5;
        private System.Windows.Forms.Button btnSendS7F3;
        private System.Windows.Forms.GroupBox groupBox26;
        private System.Windows.Forms.RadioButton rdoPPBody_Formatted;
        private System.Windows.Forms.RadioButton rdoPPBody_Unformatted;
        private System.Windows.Forms.RadioButton rdoPPBody_Both;
        private System.Windows.Forms.TextBox txtPPBodyPPID;
        private System.Windows.Forms.Label lblPPBodyPPID;
        private System.Windows.Forms.ToolStripMenuItem tsmiClearEQPLog;
        private System.Windows.Forms.Label lblAckc7;
        private System.Windows.Forms.Label lblReceivedPPID;
        private System.Windows.Forms.TextBox txtAckc7;
        private System.Windows.Forms.TextBox txtS7Systembytes;
        private System.Windows.Forms.Label lblS7Systembytes;
        private System.Windows.Forms.TextBox txtReceivedPPID;
        private System.Windows.Forms.TextBox txtPPBody_Unformatted;
        private System.Windows.Forms.ListView lvPPIDs;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.GroupBox groupBox22;
        private System.Windows.Forms.GroupBox groupBox27;
        private System.Windows.Forms.RadioButton rdoLog_Trace;
        private System.Windows.Forms.RadioButton rdoLog_Debug;
        private System.Windows.Forms.RadioButton rdoLog_Info;
        private System.Windows.Forms.RadioButton rdoLog_Warn;
        private System.Windows.Forms.RadioButton rdoLog_Error;
        private System.Windows.Forms.TabPage tabEnhanced;
        private System.Windows.Forms.GroupBox groupBox28;
        private System.Windows.Forms.TextBox txtEnhancedRemoteDataView;
        private System.Windows.Forms.TabPage tabHostSetsCONTROLStateModel;
        private System.Windows.Forms.GroupBox groupBox29;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSetsCONTROLStateModelReplyAck;
        private System.Windows.Forms.TextBox txtSetsCONTROLStateModelSystemBytes;
        private System.Windows.Forms.TextBox txtSetsCONTROLStateModel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSetsCONTROLStateModelReplyMsg;
        private System.Windows.Forms.TabPage tabObject;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox31;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCreate_Reply;
        private System.Windows.Forms.TextBox txtS14F9_OBJACK_Send;
        private System.Windows.Forms.TextBox txtS14F9_OBJID_Recv;
        private System.Windows.Forms.TextBox txtS14F9_OBJSPEC_Recv;
        private System.Windows.Forms.TextBox txtS14F9_OBJTYPE_Recv;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtS14F9_SystemBytes_Recv;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox34;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnS14F12_Reply;
        private System.Windows.Forms.TextBox txtS14F11_SystemBytes_Recv;
        private System.Windows.Forms.TextBox txtS14F11_OBJACK_Send;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtS14F11_OBJSPEC_Recv;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox36;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button btnProcessJobEventNotify;
        private System.Windows.Forms.TextBox txtS16F9_VIDs;
        private System.Windows.Forms.TextBox txtS16F9_PREVENTID;
        private System.Windows.Forms.TextBox txtS16F9_OBJID;
        private System.Windows.Forms.GroupBox groupBox37;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button btnProcessJobAlertNotify;
        private System.Windows.Forms.TextBox txtS16F7_OBJACKA;
        private System.Windows.Forms.TextBox txtS16F7_PRJOBMILESTONE;
        private System.Windows.Forms.TextBox txtS16F7_OBJID;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox39;
        private System.Windows.Forms.TextBox txtS16F27_JobCmd;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtS16F27_JobId;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox txtS16F27_Systembytes;
        private System.Windows.Forms.TextBox txtS16F27_CPVal;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox txtS16F27_CPName;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox txtS16F28_ErrText;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox txtS16F28_ErrCode;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox txtS16F28_Acka;
        private System.Windows.Forms.Button btnControlJobCommandAck;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ToolStripMenuItem tsmiInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmiDriverParameter;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TabPage tabSendSECSMessage;
        private System.Windows.Forms.Button btnCustom;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox txtS14F9_CreateOBJ_Type;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.GroupBox groupBox40;
        private System.Windows.Forms.Button btnCarrierActionAck;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TextBox txtS3F18_ErrText;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.TextBox txtS3F18_ErrCode;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox txtS3F18_Caack;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox txtS3F17_PTN_Recv;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox txtS3F17_SystemBytes_Recv;
        private System.Windows.Forms.TextBox txtS3F17_CARRIERID_Recv;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox txtS3F17_CARRIERACTION_Recv;
        private System.Windows.Forms.GroupBox groupBox41;
        private System.Windows.Forms.Button btnUpdateObject_ProcessJob;
        private System.Windows.Forms.GroupBox groupBox42;
        private System.Windows.Forms.Label lblSysEC_PRMaxJobSpace;
        private System.Windows.Forms.GroupBox groupBox43;
        private System.Windows.Forms.Label lblSysSV_PRJobSpace;
        private System.Windows.Forms.GroupBox groupBox44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Button btnCreateObject;
        private System.Windows.Forms.TextBox txtCreateObjID;
        private System.Windows.Forms.TextBox txtCreateObjSpec;
        private System.Windows.Forms.TextBox txtCreateObjType;
        private System.Windows.Forms.TabPage tabLoopback;
        private System.Windows.Forms.GroupBox groupBox30;
        private System.Windows.Forms.TextBox txtABS_Recv;
        private System.Windows.Forms.TextBox txtABS_RecvSystemBytes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblABS_Check;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox32;
        private System.Windows.Forms.Button btnLoopbackDiagnostic;
        private System.Windows.Forms.ToolStripMenuItem 離開ToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tbPPSetting;
        private System.Windows.Forms.Label lblRotateCount;
        private System.Windows.Forms.TabPage tbPPSlotMap;
        private System.Windows.Forms.TabPage tbPPCofficient;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLJStdSurface;
        private System.Windows.Forms.Label lblOffsetType;
        private System.Windows.Forms.TextBox txtOffsetType;
        private System.Windows.Forms.Label lblRepeatTimes;
        private System.Windows.Forms.TextBox txtRepeat;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtRotateCount;
        private System.Windows.Forms.CheckedListBox cLBPP;
        private System.Windows.Forms.TextBox txtS2_2x1;
        private System.Windows.Forms.TextBox txtS2_2x0;
        private System.Windows.Forms.TextBox txtS2_1x1;
        private System.Windows.Forms.TextBox txtS2_1x0;
        private System.Windows.Forms.TextBox txtBTTH;
        private System.Windows.Forms.TextBox txtRange2;
        private System.Windows.Forms.TextBox txtRange1;
        private System.Windows.Forms.TextBox txtS1_1x1;
        private System.Windows.Forms.TextBox txtS1_1x0;
        private System.Windows.Forms.Label lblRange2;
        private System.Windows.Forms.Label lblRange1;
        private System.Windows.Forms.Label lblS1_1x1;
        private System.Windows.Forms.Label lblS1_1x0;
        private System.Windows.Forms.Label lblS2_2x1;
        private System.Windows.Forms.Label lblS2_2x0;
        private System.Windows.Forms.Label lblS2_1x1;
        private System.Windows.Forms.Label lblS2_1x0;
        private System.Windows.Forms.Label lblBTTH;
        private System.Windows.Forms.TabPage tbAngle;
        private System.Windows.Forms.TextBox txtAngle4;
        private System.Windows.Forms.TextBox txtAngle3;
        private System.Windows.Forms.TextBox txtAngle2;
        private System.Windows.Forms.TextBox txtAngle1;
        private System.Windows.Forms.TextBox txtAngle8;
        private System.Windows.Forms.TextBox txtAngle7;
        private System.Windows.Forms.TextBox txtAngle6;
        private System.Windows.Forms.TextBox txtAngle5;
        private System.Windows.Forms.Label lblAngle8;
        private System.Windows.Forms.Label lblAngle7;
        private System.Windows.Forms.Label lblAngle6;
        private System.Windows.Forms.Label lblAngle5;
        private System.Windows.Forms.Label lblAngle4;
        private System.Windows.Forms.Label lblAngle3;
        private System.Windows.Forms.Label lblAngle2;
        private System.Windows.Forms.Label lblAngle1;
        private System.Windows.Forms.TabPage tbMisc;
        private System.Windows.Forms.Label lblReviseTime;
        private System.Windows.Forms.TextBox txtReviseTime;
        private System.Windows.Forms.Label lblRepeatTimes_now;
        private System.Windows.Forms.TextBox txtRepeatTimes_now;
        private System.Windows.Forms.TextBox txtCreateTime;
        private System.Windows.Forms.Label lblCreateTime;
    }
}

