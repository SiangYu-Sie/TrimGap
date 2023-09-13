namespace SECSGEM
{
    partial class SecsgemForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該公開 Managed 資源則為 true，否則為 false。</param>
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnS1F1 = new System.Windows.Forms.Button();
            this.GroupBox9 = new System.Windows.Forms.GroupBox();
            this.btn_OnlineLocal = new System.Windows.Forms.Button();
            this.btn_OnLineRemote = new System.Windows.Forms.Button();
            this.btn_Offline = new System.Windows.Forms.Button();
            this.btn_OnLine = new System.Windows.Forms.Button();
            this.lbl_CcontrolStats = new System.Windows.Forms.Label();
            this.GroupBox13 = new System.Windows.Forms.GroupBox();
            this.opt_OnLineLocal = new System.Windows.Forms.RadioButton();
            this.opt_OnLineRemote = new System.Windows.Forms.RadioButton();
            this.GroupBox12 = new System.Windows.Forms.GroupBox();
            this.opt_FailToHostOffLine = new System.Windows.Forms.RadioButton();
            this.opt_FailToEqpOffLine = new System.Windows.Forms.RadioButton();
            this.GroupBox11 = new System.Windows.Forms.GroupBox();
            this.opt_HostOffLine = new System.Windows.Forms.RadioButton();
            this.opt_AttemptOnLine = new System.Windows.Forms.RadioButton();
            this.opt_EqpOffLine = new System.Windows.Forms.RadioButton();
            this.GroupBox10 = new System.Windows.Forms.GroupBox();
            this.opt_OffLine = new System.Windows.Forms.RadioButton();
            this.opt_OnLine = new System.Windows.Forms.RadioButton();
            this.GroupBox7 = new System.Windows.Forms.GroupBox();
            this.GroupBox8 = new System.Windows.Forms.GroupBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.lbl_SpoolingState = new System.Windows.Forms.Label();
            this.GroupBox5 = new System.Windows.Forms.GroupBox();
            this.DisableComm = new System.Windows.Forms.Button();
            this.EnableComm = new System.Windows.Forms.Button();
            this.lbl_CommState = new System.Windows.Forms.Label();
            this.GroupBox6 = new System.Windows.Forms.GroupBox();
            this.opt_DisableComm = new System.Windows.Forms.RadioButton();
            this.opt_EnableComm = new System.Windows.Forms.RadioButton();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.GroupBox4 = new System.Windows.Forms.GroupBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.lbl_SECSConnectState = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.lbl_ErrorCode = new System.Windows.Forms.Label();
            this.lbl_OpResult = new System.Windows.Forms.TextBox();
            this.frEQPStatus = new System.Windows.Forms.GroupBox();
            this.txtEQPStatus = new System.Windows.Forms.Label();
            this.RefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.State = new System.Windows.Forms.TabPage();
            this.lbl_GemVer = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.lbl_GemSOFTREV = new System.Windows.Forms.Label();
            this.lbl_GemClock = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.lbl_GemMDLN = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.DefaultSubState = new System.Windows.Forms.TabPage();
            this.SVDVEC = new System.Windows.Forms.TabPage();
            this.GroupBox14 = new System.Windows.Forms.GroupBox();
            this.SetSV = new System.Windows.Forms.Button();
            this.SVValue = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.GetSV = new System.Windows.Forms.Button();
            this.SVID = new System.Windows.Forms.TextBox();
            this.GroupBox16 = new System.Windows.Forms.GroupBox();
            this.tbHostSendnewEC = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.lbl_ECChanged = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.UpdateEC = new System.Windows.Forms.Button();
            this.GetEC = new System.Windows.Forms.Button();
            this.txt_ECType = new System.Windows.Forms.Label();
            this.ECID = new System.Windows.Forms.TextBox();
            this.ECVal = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Alarm = new System.Windows.Forms.TabPage();
            this.GroupBox19 = new System.Windows.Forms.GroupBox();
            this.opt_S5WbitOff = new System.Windows.Forms.RadioButton();
            this.opt_S5WbitOn = new System.Windows.Forms.RadioButton();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbAlarmSetSV = new System.Windows.Forms.TextBox();
            this.btnAlarmSet = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.AlarmIDSet = new System.Windows.Forms.TextBox();
            this.GroupBox21 = new System.Windows.Forms.GroupBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.AlarmCD = new System.Windows.Forms.TextBox();
            this.AlarmReportSend = new System.Windows.Forms.Button();
            this.Label13 = new System.Windows.Forms.Label();
            this.AlarmID = new System.Windows.Forms.TextBox();
            this.Event = new System.Windows.Forms.TabPage();
            this.GroupBox15 = new System.Windows.Forms.GroupBox();
            this.opt_S6WbitOff = new System.Windows.Forms.RadioButton();
            this.opt_S6WbitOn = new System.Windows.Forms.RadioButton();
            this.GroupBox18 = new System.Windows.Forms.GroupBox();
            this.SendEvent = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.EventID = new System.Windows.Forms.TextBox();
            this.Spooling = new System.Windows.Forms.TabPage();
            this.GroupBox24 = new System.Windows.Forms.GroupBox();
            this.opt_OverWriteSpoolDisable = new System.Windows.Forms.RadioButton();
            this.opt_OverWriteSpoolEnable = new System.Windows.Forms.RadioButton();
            this.GroupBox23 = new System.Windows.Forms.GroupBox();
            this.opt_ConfigSpoolDisable = new System.Windows.Forms.RadioButton();
            this.opt_ConfigSpoolEnable = new System.Windows.Forms.RadioButton();
            this.GroupBox25 = new System.Windows.Forms.GroupBox();
            this.lbl_SpoolActualCount = new System.Windows.Forms.Label();
            this.Label19 = new System.Windows.Forms.Label();
            this.lbl_MaxSpoolTransmit = new System.Windows.Forms.Label();
            this.Label27 = new System.Windows.Forms.Label();
            this.lbl_SpoolStartTime = new System.Windows.Forms.Label();
            this.Label25 = new System.Windows.Forms.Label();
            this.lbl_SpoolFullTime = new System.Windows.Forms.Label();
            this.Label23 = new System.Windows.Forms.Label();
            this.lbl_SpoolTotalCount = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.Terminal = new System.Windows.Forms.TabPage();
            this.GroupBox22 = new System.Windows.Forms.GroupBox();
            this.opt_S10WbitOff = new System.Windows.Forms.RadioButton();
            this.opt_S10WbitOn = new System.Windows.Forms.RadioButton();
            this.txt_TerminalTextToHost = new System.Windows.Forms.TextBox();
            this.SendTerminalMsg = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.btn_MsgRecognitionEvent = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.txt_MessageFromHost = new System.Windows.Forms.Label();
            this.PP = new System.Windows.Forms.TabPage();
            this.groupBox26 = new System.Windows.Forms.GroupBox();
            this.Cmd_SendPP_Formatted = new System.Windows.Forms.Button();
            this.Cmd_RequestPP_Formatted = new System.Windows.Forms.Button();
            this.Cmd_RequestPP = new System.Windows.Forms.Button();
            this.Cmd_SendPP = new System.Windows.Forms.Button();
            this.Cmd_LoadInquire = new System.Windows.Forms.Button();
            this.txtPPID = new System.Windows.Forms.TextBox();
            this.FrameGuickGEM = new System.Windows.Forms.GroupBox();
            this.PPEventText = new System.Windows.Forms.TextBox();
            this.Remote = new System.Windows.Forms.TabPage();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.txCPACK2 = new System.Windows.Forms.TextBox();
            this.txCPACK1 = new System.Windows.Forms.TextBox();
            this.txHCACK = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.lbCPVAL2 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.lbCPNAME2 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.lbCPVAL1 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lbCPNAME1 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lbRemoteCmd = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.SettingMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Config = new System.Windows.Forms.ToolStripMenuItem();
            this.Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox_LD_Info = new System.Windows.Forms.GroupBox();
            this.label_RecipeName = new System.Windows.Forms.Label();
            this.label_LD_MagazineID = new System.Windows.Forms.Label();
            this.label_LotID = new System.Windows.Forms.Label();
            this.groupBox_ULD_Pass1MagazineInfo = new System.Windows.Forms.GroupBox();
            this.label_ULD_Pass1MagazineUnitCount = new System.Windows.Forms.Label();
            this.label_ULD_Pass1MagazineBoatCount = new System.Windows.Forms.Label();
            this.label_ULD_Pass1MagazineID = new System.Windows.Forms.Label();
            this.groupBox_ULD_Pass2MagazineInfo = new System.Windows.Forms.GroupBox();
            this.label_ULD_Pass2MagazineUnitCount = new System.Windows.Forms.Label();
            this.label_ULD_Pass2MagazineBoatCount = new System.Windows.Forms.Label();
            this.label_ULD_Pass2MagazineID = new System.Windows.Forms.Label();
            this.groupBox_ULD_NGMagazineInfo = new System.Windows.Forms.GroupBox();
            this.label_ULD_NGMagazineUnitCount = new System.Windows.Forms.Label();
            this.label_ULD_NGMagazineBoatCount = new System.Windows.Forms.Label();
            this.label_ULD_NGMagazineID = new System.Windows.Forms.Label();
            this.groupBox_ULD_EmptyMagazineInfo = new System.Windows.Forms.GroupBox();
            this.label_ULD_EmptyMagazineBoatCount = new System.Windows.Forms.Label();
            this.label_ULD_EmptyMagazineID = new System.Windows.Forms.Label();
            this.GroupBox9.SuspendLayout();
            this.GroupBox13.SuspendLayout();
            this.GroupBox12.SuspendLayout();
            this.GroupBox11.SuspendLayout();
            this.GroupBox10.SuspendLayout();
            this.GroupBox7.SuspendLayout();
            this.GroupBox8.SuspendLayout();
            this.GroupBox5.SuspendLayout();
            this.GroupBox6.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.GroupBox4.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.frEQPStatus.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.State.SuspendLayout();
            this.DefaultSubState.SuspendLayout();
            this.SVDVEC.SuspendLayout();
            this.GroupBox14.SuspendLayout();
            this.GroupBox16.SuspendLayout();
            this.Alarm.SuspendLayout();
            this.GroupBox19.SuspendLayout();
            this.groupBox27.SuspendLayout();
            this.GroupBox21.SuspendLayout();
            this.Event.SuspendLayout();
            this.GroupBox15.SuspendLayout();
            this.GroupBox18.SuspendLayout();
            this.Spooling.SuspendLayout();
            this.GroupBox24.SuspendLayout();
            this.GroupBox23.SuspendLayout();
            this.GroupBox25.SuspendLayout();
            this.Terminal.SuspendLayout();
            this.GroupBox22.SuspendLayout();
            this.PP.SuspendLayout();
            this.groupBox26.SuspendLayout();
            this.FrameGuickGEM.SuspendLayout();
            this.Remote.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox_LD_Info.SuspendLayout();
            this.groupBox_ULD_Pass1MagazineInfo.SuspendLayout();
            this.groupBox_ULD_Pass2MagazineInfo.SuspendLayout();
            this.groupBox_ULD_NGMagazineInfo.SuspendLayout();
            this.groupBox_ULD_EmptyMagazineInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(304, 53);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(552, 622);
            this.textBox1.TabIndex = 3;
            // 
            // btnS1F1
            // 
            this.btnS1F1.Location = new System.Drawing.Point(761, 681);
            this.btnS1F1.Name = "btnS1F1";
            this.btnS1F1.Size = new System.Drawing.Size(95, 40);
            this.btnS1F1.TabIndex = 10;
            this.btnS1F1.Text = "S1F1";
            this.btnS1F1.UseVisualStyleBackColor = true;
            this.btnS1F1.Click += new System.EventHandler(this.btnS1F1_Click);
            // 
            // GroupBox9
            // 
            this.GroupBox9.Controls.Add(this.btn_OnlineLocal);
            this.GroupBox9.Controls.Add(this.btn_OnLineRemote);
            this.GroupBox9.Controls.Add(this.btn_Offline);
            this.GroupBox9.Controls.Add(this.btn_OnLine);
            this.GroupBox9.Controls.Add(this.lbl_CcontrolStats);
            this.GroupBox9.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GroupBox9.Location = new System.Drawing.Point(6, 274);
            this.GroupBox9.Name = "GroupBox9";
            this.GroupBox9.Size = new System.Drawing.Size(260, 140);
            this.GroupBox9.TabIndex = 15;
            this.GroupBox9.TabStop = false;
            this.GroupBox9.Text = "Control State";
            // 
            // btn_OnlineLocal
            // 
            this.btn_OnlineLocal.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_OnlineLocal.Location = new System.Drawing.Point(137, 93);
            this.btn_OnlineLocal.Name = "btn_OnlineLocal";
            this.btn_OnlineLocal.Size = new System.Drawing.Size(114, 32);
            this.btn_OnlineLocal.TabIndex = 7;
            this.btn_OnlineLocal.Text = "On Line Local";
            this.btn_OnlineLocal.Click += new System.EventHandler(this.btn_OnlineLocal_Click);
            // 
            // btn_OnLineRemote
            // 
            this.btn_OnLineRemote.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_OnLineRemote.Location = new System.Drawing.Point(9, 93);
            this.btn_OnLineRemote.Name = "btn_OnLineRemote";
            this.btn_OnLineRemote.Size = new System.Drawing.Size(114, 32);
            this.btn_OnLineRemote.TabIndex = 6;
            this.btn_OnLineRemote.Text = "On Line Remote";
            this.btn_OnLineRemote.Click += new System.EventHandler(this.btn_OnLineRemote_Click);
            // 
            // btn_Offline
            // 
            this.btn_Offline.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Offline.Location = new System.Drawing.Point(137, 55);
            this.btn_Offline.Name = "btn_Offline";
            this.btn_Offline.Size = new System.Drawing.Size(114, 32);
            this.btn_Offline.TabIndex = 5;
            this.btn_Offline.Text = "Off Line";
            this.btn_Offline.Click += new System.EventHandler(this.btn_Offline_Click);
            // 
            // btn_OnLine
            // 
            this.btn_OnLine.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_OnLine.Location = new System.Drawing.Point(9, 55);
            this.btn_OnLine.Name = "btn_OnLine";
            this.btn_OnLine.Size = new System.Drawing.Size(114, 32);
            this.btn_OnLine.TabIndex = 4;
            this.btn_OnLine.Text = "On Line Request";
            this.btn_OnLine.Click += new System.EventHandler(this.btn_OnLine_Click);
            // 
            // lbl_CcontrolStats
            // 
            this.lbl_CcontrolStats.BackColor = System.Drawing.Color.White;
            this.lbl_CcontrolStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_CcontrolStats.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_CcontrolStats.Location = new System.Drawing.Point(8, 18);
            this.lbl_CcontrolStats.Name = "lbl_CcontrolStats";
            this.lbl_CcontrolStats.Size = new System.Drawing.Size(246, 30);
            this.lbl_CcontrolStats.TabIndex = 1;
            this.lbl_CcontrolStats.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GroupBox13
            // 
            this.GroupBox13.Controls.Add(this.opt_OnLineLocal);
            this.GroupBox13.Controls.Add(this.opt_OnLineRemote);
            this.GroupBox13.Location = new System.Drawing.Point(6, 386);
            this.GroupBox13.Name = "GroupBox13";
            this.GroupBox13.Size = new System.Drawing.Size(270, 88);
            this.GroupBox13.TabIndex = 11;
            this.GroupBox13.TabStop = false;
            this.GroupBox13.Text = "On Line Subtate <EC:51>";
            // 
            // opt_OnLineLocal
            // 
            this.opt_OnLineLocal.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_OnLineLocal.Location = new System.Drawing.Point(10, 57);
            this.opt_OnLineLocal.Name = "opt_OnLineLocal";
            this.opt_OnLineLocal.Size = new System.Drawing.Size(80, 21);
            this.opt_OnLineLocal.TabIndex = 1;
            this.opt_OnLineLocal.Text = "Local";
            this.opt_OnLineLocal.Click += new System.EventHandler(this.opt_OnLineLocal_Click);
            // 
            // opt_OnLineRemote
            // 
            this.opt_OnLineRemote.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_OnLineRemote.Location = new System.Drawing.Point(10, 22);
            this.opt_OnLineRemote.Name = "opt_OnLineRemote";
            this.opt_OnLineRemote.Size = new System.Drawing.Size(80, 28);
            this.opt_OnLineRemote.TabIndex = 0;
            this.opt_OnLineRemote.Text = "Remote";
            this.opt_OnLineRemote.Click += new System.EventHandler(this.opt_OnLineRemote_Click);
            // 
            // GroupBox12
            // 
            this.GroupBox12.Controls.Add(this.opt_FailToHostOffLine);
            this.GroupBox12.Controls.Add(this.opt_FailToEqpOffLine);
            this.GroupBox12.Location = new System.Drawing.Point(6, 262);
            this.GroupBox12.Name = "GroupBox12";
            this.GroupBox12.Size = new System.Drawing.Size(270, 95);
            this.GroupBox12.TabIndex = 10;
            this.GroupBox12.TabStop = false;
            this.GroupBox12.Text = "On Line Fail Substate <EC:50>";
            // 
            // opt_FailToHostOffLine
            // 
            this.opt_FailToHostOffLine.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_FailToHostOffLine.Location = new System.Drawing.Point(10, 60);
            this.opt_FailToHostOffLine.Name = "opt_FailToHostOffLine";
            this.opt_FailToHostOffLine.Size = new System.Drawing.Size(181, 24);
            this.opt_FailToHostOffLine.TabIndex = 1;
            this.opt_FailToHostOffLine.Text = "Host OffLine";
            this.opt_FailToHostOffLine.Click += new System.EventHandler(this.opt_FailToHostOffLine_Click);
            // 
            // opt_FailToEqpOffLine
            // 
            this.opt_FailToEqpOffLine.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_FailToEqpOffLine.Location = new System.Drawing.Point(10, 25);
            this.opt_FailToEqpOffLine.Name = "opt_FailToEqpOffLine";
            this.opt_FailToEqpOffLine.Size = new System.Drawing.Size(151, 24);
            this.opt_FailToEqpOffLine.TabIndex = 0;
            this.opt_FailToEqpOffLine.Text = "Eqp OffLine";
            this.opt_FailToEqpOffLine.Click += new System.EventHandler(this.opt_FailToEqpOffLine_Click);
            // 
            // GroupBox11
            // 
            this.GroupBox11.Controls.Add(this.opt_HostOffLine);
            this.GroupBox11.Controls.Add(this.opt_AttemptOnLine);
            this.GroupBox11.Controls.Add(this.opt_EqpOffLine);
            this.GroupBox11.Location = new System.Drawing.Point(6, 114);
            this.GroupBox11.Name = "GroupBox11";
            this.GroupBox11.Size = new System.Drawing.Size(270, 116);
            this.GroupBox11.TabIndex = 9;
            this.GroupBox11.TabStop = false;
            this.GroupBox11.Text = "Off Line Substate <EC:49>";
            // 
            // opt_HostOffLine
            // 
            this.opt_HostOffLine.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_HostOffLine.Location = new System.Drawing.Point(10, 84);
            this.opt_HostOffLine.Name = "opt_HostOffLine";
            this.opt_HostOffLine.Size = new System.Drawing.Size(171, 29);
            this.opt_HostOffLine.TabIndex = 2;
            this.opt_HostOffLine.Text = "Host OffLine";
            this.opt_HostOffLine.Click += new System.EventHandler(this.opt_HostOffLine_Click);
            // 
            // opt_AttemptOnLine
            // 
            this.opt_AttemptOnLine.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_AttemptOnLine.Location = new System.Drawing.Point(10, 53);
            this.opt_AttemptOnLine.Name = "opt_AttemptOnLine";
            this.opt_AttemptOnLine.Size = new System.Drawing.Size(144, 28);
            this.opt_AttemptOnLine.TabIndex = 1;
            this.opt_AttemptOnLine.Text = "Attempt OnLine";
            this.opt_AttemptOnLine.Click += new System.EventHandler(this.opt_AttemptOnLine_Click);
            // 
            // opt_EqpOffLine
            // 
            this.opt_EqpOffLine.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_EqpOffLine.Location = new System.Drawing.Point(10, 18);
            this.opt_EqpOffLine.Name = "opt_EqpOffLine";
            this.opt_EqpOffLine.Size = new System.Drawing.Size(105, 35);
            this.opt_EqpOffLine.TabIndex = 0;
            this.opt_EqpOffLine.Text = "Eqp OffLine";
            this.opt_EqpOffLine.Click += new System.EventHandler(this.opt_EqpOffLine_Click);
            // 
            // GroupBox10
            // 
            this.GroupBox10.Controls.Add(this.opt_OffLine);
            this.GroupBox10.Controls.Add(this.opt_OnLine);
            this.GroupBox10.Location = new System.Drawing.Point(6, 6);
            this.GroupBox10.Name = "GroupBox10";
            this.GroupBox10.Size = new System.Drawing.Size(270, 89);
            this.GroupBox10.TabIndex = 8;
            this.GroupBox10.TabStop = false;
            this.GroupBox10.Text = "Control State <EC:8>";
            // 
            // opt_OffLine
            // 
            this.opt_OffLine.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_OffLine.Location = new System.Drawing.Point(10, 56);
            this.opt_OffLine.Name = "opt_OffLine";
            this.opt_OffLine.Size = new System.Drawing.Size(80, 27);
            this.opt_OffLine.TabIndex = 1;
            this.opt_OffLine.Text = "Off Line";
            this.opt_OffLine.Click += new System.EventHandler(this.opt_OffLine_Click);
            // 
            // opt_OnLine
            // 
            this.opt_OnLine.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_OnLine.Location = new System.Drawing.Point(10, 23);
            this.opt_OnLine.Name = "opt_OnLine";
            this.opt_OnLine.Size = new System.Drawing.Size(80, 27);
            this.opt_OnLine.TabIndex = 0;
            this.opt_OnLine.Text = "On Line";
            this.opt_OnLine.Click += new System.EventHandler(this.opt_OnLine_Click);
            // 
            // GroupBox7
            // 
            this.GroupBox7.Controls.Add(this.GroupBox8);
            this.GroupBox7.Controls.Add(this.lbl_SpoolingState);
            this.GroupBox7.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GroupBox7.Location = new System.Drawing.Point(6, 420);
            this.GroupBox7.Name = "GroupBox7";
            this.GroupBox7.Size = new System.Drawing.Size(260, 56);
            this.GroupBox7.TabIndex = 14;
            this.GroupBox7.TabStop = false;
            this.GroupBox7.Text = "Spooling State <SV:58>";
            // 
            // GroupBox8
            // 
            this.GroupBox8.Controls.Add(this.Label1);
            this.GroupBox8.Location = new System.Drawing.Point(8, 56);
            this.GroupBox8.Name = "GroupBox8";
            this.GroupBox8.Size = new System.Drawing.Size(192, 56);
            this.GroupBox8.TabIndex = 5;
            this.GroupBox8.TabStop = false;
            this.GroupBox8.Text = "Quick GEM init. Result";
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.White;
            this.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label1.Location = new System.Drawing.Point(8, 18);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(176, 24);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Result";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_SpoolingState
            // 
            this.lbl_SpoolingState.BackColor = System.Drawing.Color.White;
            this.lbl_SpoolingState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_SpoolingState.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_SpoolingState.Location = new System.Drawing.Point(9, 18);
            this.lbl_SpoolingState.Name = "lbl_SpoolingState";
            this.lbl_SpoolingState.Size = new System.Drawing.Size(245, 30);
            this.lbl_SpoolingState.TabIndex = 0;
            this.lbl_SpoolingState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GroupBox5
            // 
            this.GroupBox5.Controls.Add(this.DisableComm);
            this.GroupBox5.Controls.Add(this.EnableComm);
            this.GroupBox5.Controls.Add(this.lbl_CommState);
            this.GroupBox5.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GroupBox5.Location = new System.Drawing.Point(6, 128);
            this.GroupBox5.Name = "GroupBox5";
            this.GroupBox5.Size = new System.Drawing.Size(260, 92);
            this.GroupBox5.TabIndex = 13;
            this.GroupBox5.TabStop = false;
            this.GroupBox5.Text = "Communicating State";
            // 
            // DisableComm
            // 
            this.DisableComm.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.DisableComm.Location = new System.Drawing.Point(136, 52);
            this.DisableComm.Name = "DisableComm";
            this.DisableComm.Size = new System.Drawing.Size(118, 32);
            this.DisableComm.TabIndex = 3;
            this.DisableComm.Text = "DisableComm";
            this.DisableComm.Click += new System.EventHandler(this.DisableComm_Click);
            // 
            // EnableComm
            // 
            this.EnableComm.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.EnableComm.Location = new System.Drawing.Point(6, 53);
            this.EnableComm.Name = "EnableComm";
            this.EnableComm.Size = new System.Drawing.Size(116, 32);
            this.EnableComm.TabIndex = 2;
            this.EnableComm.Text = "EnableComm";
            this.EnableComm.Click += new System.EventHandler(this.EnableComm_Click);
            // 
            // lbl_CommState
            // 
            this.lbl_CommState.BackColor = System.Drawing.Color.White;
            this.lbl_CommState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_CommState.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_CommState.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_CommState.Location = new System.Drawing.Point(8, 16);
            this.lbl_CommState.Name = "lbl_CommState";
            this.lbl_CommState.Size = new System.Drawing.Size(246, 32);
            this.lbl_CommState.TabIndex = 1;
            this.lbl_CommState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GroupBox6
            // 
            this.GroupBox6.Controls.Add(this.opt_DisableComm);
            this.GroupBox6.Controls.Add(this.opt_EnableComm);
            this.GroupBox6.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold);
            this.GroupBox6.Location = new System.Drawing.Point(6, 226);
            this.GroupBox6.Name = "GroupBox6";
            this.GroupBox6.Size = new System.Drawing.Size(260, 42);
            this.GroupBox6.TabIndex = 4;
            this.GroupBox6.TabStop = false;
            this.GroupBox6.Text = "Default Communicting State <EC:7>";
            // 
            // opt_DisableComm
            // 
            this.opt_DisableComm.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_DisableComm.Location = new System.Drawing.Point(120, 20);
            this.opt_DisableComm.Name = "opt_DisableComm";
            this.opt_DisableComm.Size = new System.Drawing.Size(80, 16);
            this.opt_DisableComm.TabIndex = 1;
            this.opt_DisableComm.Text = "Disable";
            this.opt_DisableComm.Click += new System.EventHandler(this.opt_DisableComm_Click);
            // 
            // opt_EnableComm
            // 
            this.opt_EnableComm.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_EnableComm.Location = new System.Drawing.Point(24, 20);
            this.opt_EnableComm.Name = "opt_EnableComm";
            this.opt_EnableComm.Size = new System.Drawing.Size(80, 16);
            this.opt_EnableComm.TabIndex = 0;
            this.opt_EnableComm.Text = "Enable";
            this.opt_EnableComm.Click += new System.EventHandler(this.opt_EnableComm_Click);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.GroupBox4);
            this.GroupBox3.Controls.Add(this.lbl_SECSConnectState);
            this.GroupBox3.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GroupBox3.Location = new System.Drawing.Point(6, 68);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(260, 56);
            this.GroupBox3.TabIndex = 12;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "SECS Connection State";
            // 
            // GroupBox4
            // 
            this.GroupBox4.Controls.Add(this.Label3);
            this.GroupBox4.Location = new System.Drawing.Point(8, 56);
            this.GroupBox4.Name = "GroupBox4";
            this.GroupBox4.Size = new System.Drawing.Size(192, 56);
            this.GroupBox4.TabIndex = 5;
            this.GroupBox4.TabStop = false;
            this.GroupBox4.Text = "Quick GEM init. Result";
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.White;
            this.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label3.Location = new System.Drawing.Point(8, 18);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(176, 24);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "Result";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_SECSConnectState
            // 
            this.lbl_SECSConnectState.BackColor = System.Drawing.Color.Red;
            this.lbl_SECSConnectState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_SECSConnectState.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_SECSConnectState.Location = new System.Drawing.Point(8, 18);
            this.lbl_SECSConnectState.Name = "lbl_SECSConnectState";
            this.lbl_SECSConnectState.Size = new System.Drawing.Size(246, 30);
            this.lbl_SECSConnectState.TabIndex = 0;
            this.lbl_SECSConnectState.Text = "Disconnection";
            this.lbl_SECSConnectState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.GroupBox2);
            this.GroupBox1.Controls.Add(this.lbl_ErrorCode);
            this.GroupBox1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GroupBox1.Location = new System.Drawing.Point(6, 6);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(260, 56);
            this.GroupBox1.TabIndex = 11;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "GEM init. Result";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.Label2);
            this.GroupBox2.Location = new System.Drawing.Point(8, 56);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(192, 56);
            this.GroupBox2.TabIndex = 5;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Quick GEM init. Result";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.White;
            this.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label2.Location = new System.Drawing.Point(8, 18);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(176, 24);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "Result";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ErrorCode
            // 
            this.lbl_ErrorCode.BackColor = System.Drawing.Color.White;
            this.lbl_ErrorCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ErrorCode.Font = new System.Drawing.Font("新細明體", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_ErrorCode.Location = new System.Drawing.Point(8, 18);
            this.lbl_ErrorCode.Name = "lbl_ErrorCode";
            this.lbl_ErrorCode.Size = new System.Drawing.Size(246, 30);
            this.lbl_ErrorCode.TabIndex = 0;
            this.lbl_ErrorCode.Text = "Result";
            this.lbl_ErrorCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OpResult
            // 
            this.lbl_OpResult.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_OpResult.Location = new System.Drawing.Point(304, 689);
            this.lbl_OpResult.Name = "lbl_OpResult";
            this.lbl_OpResult.Size = new System.Drawing.Size(451, 27);
            this.lbl_OpResult.TabIndex = 18;
            this.lbl_OpResult.Text = "Result";
            this.lbl_OpResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frEQPStatus
            // 
            this.frEQPStatus.Controls.Add(this.txtEQPStatus);
            this.frEQPStatus.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.frEQPStatus.Location = new System.Drawing.Point(6, 482);
            this.frEQPStatus.Name = "frEQPStatus";
            this.frEQPStatus.Size = new System.Drawing.Size(260, 42);
            this.frEQPStatus.TabIndex = 20;
            this.frEQPStatus.TabStop = false;
            this.frEQPStatus.Text = "EQP Status";
            // 
            // txtEQPStatus
            // 
            this.txtEQPStatus.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtEQPStatus.Location = new System.Drawing.Point(9, 12);
            this.txtEQPStatus.Name = "txtEQPStatus";
            this.txtEQPStatus.Size = new System.Drawing.Size(245, 24);
            this.txtEQPStatus.TabIndex = 2;
            this.txtEQPStatus.Text = "IDLE";
            this.txtEQPStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RefreshTimer
            // 
            this.RefreshTimer.Interval = 1000;
            this.RefreshTimer.Tick += new System.EventHandler(this.RefreshTimer_Tick);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.State);
            this.tabControl2.Controls.Add(this.DefaultSubState);
            this.tabControl2.Controls.Add(this.SVDVEC);
            this.tabControl2.Controls.Add(this.Alarm);
            this.tabControl2.Controls.Add(this.Event);
            this.tabControl2.Controls.Add(this.Spooling);
            this.tabControl2.Controls.Add(this.Terminal);
            this.tabControl2.Controls.Add(this.PP);
            this.tabControl2.Controls.Add(this.Remote);
            this.tabControl2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold);
            this.tabControl2.Location = new System.Drawing.Point(12, 27);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(290, 694);
            this.tabControl2.TabIndex = 22;
            // 
            // State
            // 
            this.State.Controls.Add(this.lbl_GemVer);
            this.State.Controls.Add(this.Label14);
            this.State.Controls.Add(this.lbl_GemSOFTREV);
            this.State.Controls.Add(this.lbl_GemClock);
            this.State.Controls.Add(this.Label12);
            this.State.Controls.Add(this.lbl_GemMDLN);
            this.State.Controls.Add(this.Label7);
            this.State.Controls.Add(this.Label5);
            this.State.Controls.Add(this.GroupBox3);
            this.State.Controls.Add(this.frEQPStatus);
            this.State.Controls.Add(this.GroupBox5);
            this.State.Controls.Add(this.GroupBox6);
            this.State.Controls.Add(this.GroupBox1);
            this.State.Controls.Add(this.GroupBox9);
            this.State.Controls.Add(this.GroupBox7);
            this.State.Location = new System.Drawing.Point(4, 26);
            this.State.Name = "State";
            this.State.Padding = new System.Windows.Forms.Padding(3);
            this.State.Size = new System.Drawing.Size(282, 664);
            this.State.TabIndex = 0;
            this.State.Text = "State";
            this.State.UseVisualStyleBackColor = true;
            // 
            // lbl_GemVer
            // 
            this.lbl_GemVer.BackColor = System.Drawing.Color.White;
            this.lbl_GemVer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_GemVer.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_GemVer.Location = new System.Drawing.Point(129, 630);
            this.lbl_GemVer.Name = "lbl_GemVer";
            this.lbl_GemVer.Size = new System.Drawing.Size(136, 28);
            this.lbl_GemVer.TabIndex = 31;
            // 
            // Label14
            // 
            this.Label14.Location = new System.Drawing.Point(-7, 634);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(120, 24);
            this.Label14.TabIndex = 30;
            this.Label14.Text = "QuickGEM Ver:";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_GemSOFTREV
            // 
            this.lbl_GemSOFTREV.BackColor = System.Drawing.Color.White;
            this.lbl_GemSOFTREV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_GemSOFTREV.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_GemSOFTREV.Location = new System.Drawing.Point(129, 596);
            this.lbl_GemSOFTREV.Name = "lbl_GemSOFTREV";
            this.lbl_GemSOFTREV.Size = new System.Drawing.Size(136, 28);
            this.lbl_GemSOFTREV.TabIndex = 29;
            // 
            // lbl_GemClock
            // 
            this.lbl_GemClock.BackColor = System.Drawing.Color.White;
            this.lbl_GemClock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_GemClock.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_GemClock.Location = new System.Drawing.Point(129, 529);
            this.lbl_GemClock.Name = "lbl_GemClock";
            this.lbl_GemClock.Size = new System.Drawing.Size(136, 28);
            this.lbl_GemClock.TabIndex = 25;
            // 
            // Label12
            // 
            this.Label12.Location = new System.Drawing.Point(9, 600);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(104, 24);
            this.Label12.TabIndex = 28;
            this.Label12.Text = "GEM_SOFTREV";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_GemMDLN
            // 
            this.lbl_GemMDLN.BackColor = System.Drawing.Color.White;
            this.lbl_GemMDLN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_GemMDLN.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_GemMDLN.Location = new System.Drawing.Point(129, 562);
            this.lbl_GemMDLN.Name = "lbl_GemMDLN";
            this.lbl_GemMDLN.Size = new System.Drawing.Size(136, 28);
            this.lbl_GemMDLN.TabIndex = 27;
            // 
            // Label7
            // 
            this.Label7.Location = new System.Drawing.Point(5, 565);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(88, 24);
            this.Label7.TabIndex = 26;
            this.Label7.Text = "GEM_MDLN";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label5
            // 
            this.Label5.Location = new System.Drawing.Point(6, 533);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(51, 24);
            this.Label5.TabIndex = 24;
            this.Label5.Text = "GEM Clock";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DefaultSubState
            // 
            this.DefaultSubState.Controls.Add(this.GroupBox13);
            this.DefaultSubState.Controls.Add(this.GroupBox12);
            this.DefaultSubState.Controls.Add(this.GroupBox11);
            this.DefaultSubState.Controls.Add(this.GroupBox10);
            this.DefaultSubState.Location = new System.Drawing.Point(4, 26);
            this.DefaultSubState.Name = "DefaultSubState";
            this.DefaultSubState.Padding = new System.Windows.Forms.Padding(3);
            this.DefaultSubState.Size = new System.Drawing.Size(282, 664);
            this.DefaultSubState.TabIndex = 1;
            this.DefaultSubState.Text = "Default SubState";
            this.DefaultSubState.UseVisualStyleBackColor = true;
            // 
            // SVDVEC
            // 
            this.SVDVEC.Controls.Add(this.GroupBox14);
            this.SVDVEC.Controls.Add(this.GroupBox16);
            this.SVDVEC.Location = new System.Drawing.Point(4, 26);
            this.SVDVEC.Name = "SVDVEC";
            this.SVDVEC.Size = new System.Drawing.Size(282, 664);
            this.SVDVEC.TabIndex = 2;
            this.SVDVEC.Text = "Manual - SV,DV,EC";
            this.SVDVEC.UseVisualStyleBackColor = true;
            // 
            // GroupBox14
            // 
            this.GroupBox14.Controls.Add(this.SetSV);
            this.GroupBox14.Controls.Add(this.SVValue);
            this.GroupBox14.Controls.Add(this.Label6);
            this.GroupBox14.Controls.Add(this.GetSV);
            this.GroupBox14.Controls.Add(this.SVID);
            this.GroupBox14.Location = new System.Drawing.Point(8, 300);
            this.GroupBox14.Name = "GroupBox14";
            this.GroupBox14.Size = new System.Drawing.Size(263, 133);
            this.GroupBox14.TabIndex = 15;
            this.GroupBox14.TabStop = false;
            this.GroupBox14.Text = "SV, DV";
            // 
            // SetSV
            // 
            this.SetSV.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SetSV.Location = new System.Drawing.Point(136, 91);
            this.SetSV.Name = "SetSV";
            this.SetSV.Size = new System.Drawing.Size(118, 32);
            this.SetSV.TabIndex = 14;
            this.SetSV.Text = "UpdateSV(DV)";
            this.SetSV.Click += new System.EventHandler(this.UpdateSV_Click);
            // 
            // SVValue
            // 
            this.SVValue.AccessibleRole = System.Windows.Forms.AccessibleRole.Clock;
            this.SVValue.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SVValue.Location = new System.Drawing.Point(6, 57);
            this.SVValue.Name = "SVValue";
            this.SVValue.Size = new System.Drawing.Size(248, 27);
            this.SVValue.TabIndex = 12;
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label6.Location = new System.Drawing.Point(3, 23);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(96, 24);
            this.Label6.TabIndex = 11;
            this.Label6.Text = "SVID,DVID";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GetSV
            // 
            this.GetSV.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GetSV.Location = new System.Drawing.Point(6, 91);
            this.GetSV.Name = "GetSV";
            this.GetSV.Size = new System.Drawing.Size(118, 32);
            this.GetSV.TabIndex = 9;
            this.GetSV.Text = "GetSV(DV)";
            this.GetSV.Click += new System.EventHandler(this.GetSV_Click);
            // 
            // SVID
            // 
            this.SVID.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SVID.Location = new System.Drawing.Point(96, 21);
            this.SVID.Name = "SVID";
            this.SVID.Size = new System.Drawing.Size(158, 27);
            this.SVID.TabIndex = 0;
            this.SVID.Text = "1001";
            // 
            // GroupBox16
            // 
            this.GroupBox16.Controls.Add(this.tbHostSendnewEC);
            this.GroupBox16.Controls.Add(this.label30);
            this.GroupBox16.Controls.Add(this.lbl_ECChanged);
            this.GroupBox16.Controls.Add(this.Label11);
            this.GroupBox16.Controls.Add(this.UpdateEC);
            this.GroupBox16.Controls.Add(this.GetEC);
            this.GroupBox16.Controls.Add(this.txt_ECType);
            this.GroupBox16.Controls.Add(this.ECID);
            this.GroupBox16.Controls.Add(this.ECVal);
            this.GroupBox16.Controls.Add(this.Label8);
            this.GroupBox16.Controls.Add(this.Label9);
            this.GroupBox16.Location = new System.Drawing.Point(8, 12);
            this.GroupBox16.Name = "GroupBox16";
            this.GroupBox16.Size = new System.Drawing.Size(263, 256);
            this.GroupBox16.TabIndex = 8;
            this.GroupBox16.TabStop = false;
            this.GroupBox16.Text = "EC";
            // 
            // tbHostSendnewEC
            // 
            this.tbHostSendnewEC.Location = new System.Drawing.Point(6, 219);
            this.tbHostSendnewEC.Name = "tbHostSendnewEC";
            this.tbHostSendnewEC.ReadOnly = true;
            this.tbHostSendnewEC.Size = new System.Drawing.Size(245, 27);
            this.tbHostSendnewEC.TabIndex = 14;
            // 
            // label30
            // 
            this.label30.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label30.Location = new System.Drawing.Point(6, 192);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(206, 24);
            this.label30.TabIndex = 13;
            this.label30.Text = "Host Send New ECID,Value";
            // 
            // lbl_ECChanged
            // 
            this.lbl_ECChanged.BackColor = System.Drawing.Color.White;
            this.lbl_ECChanged.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ECChanged.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_ECChanged.Location = new System.Drawing.Point(136, 137);
            this.lbl_ECChanged.Name = "lbl_ECChanged";
            this.lbl_ECChanged.Size = new System.Drawing.Size(118, 32);
            this.lbl_ECChanged.TabIndex = 12;
            // 
            // Label11
            // 
            this.Label11.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label11.Location = new System.Drawing.Point(6, 145);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(124, 24);
            this.Label11.TabIndex = 11;
            this.Label11.Text = "SV:EC Changed";
            // 
            // UpdateEC
            // 
            this.UpdateEC.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.UpdateEC.Location = new System.Drawing.Point(136, 87);
            this.UpdateEC.Name = "UpdateEC";
            this.UpdateEC.Size = new System.Drawing.Size(118, 32);
            this.UpdateEC.TabIndex = 10;
            this.UpdateEC.Text = "Update EC";
            this.UpdateEC.Click += new System.EventHandler(this.UpdateEC_Click);
            // 
            // GetEC
            // 
            this.GetEC.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GetEC.Location = new System.Drawing.Point(6, 87);
            this.GetEC.Name = "GetEC";
            this.GetEC.Size = new System.Drawing.Size(118, 32);
            this.GetEC.TabIndex = 9;
            this.GetEC.Text = "Get EC";
            this.GetEC.Click += new System.EventHandler(this.GetEC_Click);
            // 
            // txt_ECType
            // 
            this.txt_ECType.BackColor = System.Drawing.Color.White;
            this.txt_ECType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ECType.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txt_ECType.Location = new System.Drawing.Point(161, 19);
            this.txt_ECType.Name = "txt_ECType";
            this.txt_ECType.Size = new System.Drawing.Size(93, 27);
            this.txt_ECType.TabIndex = 8;
            this.txt_ECType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ECID
            // 
            this.ECID.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ECID.Location = new System.Drawing.Point(54, 19);
            this.ECID.Name = "ECID";
            this.ECID.Size = new System.Drawing.Size(64, 27);
            this.ECID.TabIndex = 7;
            this.ECID.Text = "1003";
            // 
            // ECVal
            // 
            this.ECVal.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ECVal.Location = new System.Drawing.Point(9, 54);
            this.ECVal.Name = "ECVal";
            this.ECVal.Size = new System.Drawing.Size(248, 27);
            this.ECVal.TabIndex = 6;
            // 
            // Label8
            // 
            this.Label8.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label8.Location = new System.Drawing.Point(122, 18);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(40, 24);
            this.Label8.TabIndex = 4;
            this.Label8.Text = "Type";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label9
            // 
            this.Label9.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label9.Location = new System.Drawing.Point(-3, 18);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(64, 24);
            this.Label9.TabIndex = 2;
            this.Label9.Text = "EC ID";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Alarm
            // 
            this.Alarm.Controls.Add(this.GroupBox19);
            this.Alarm.Controls.Add(this.groupBox27);
            this.Alarm.Controls.Add(this.GroupBox21);
            this.Alarm.Location = new System.Drawing.Point(4, 26);
            this.Alarm.Name = "Alarm";
            this.Alarm.Size = new System.Drawing.Size(282, 664);
            this.Alarm.TabIndex = 3;
            this.Alarm.Text = "Manual - Alarm";
            this.Alarm.UseVisualStyleBackColor = true;
            // 
            // GroupBox19
            // 
            this.GroupBox19.Controls.Add(this.opt_S5WbitOff);
            this.GroupBox19.Controls.Add(this.opt_S5WbitOn);
            this.GroupBox19.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GroupBox19.Location = new System.Drawing.Point(7, 282);
            this.GroupBox19.Name = "GroupBox19";
            this.GroupBox19.Size = new System.Drawing.Size(267, 57);
            this.GroupBox19.TabIndex = 6;
            this.GroupBox19.TabStop = false;
            this.GroupBox19.Text = "S5 WBit <EC:21>";
            // 
            // opt_S5WbitOff
            // 
            this.opt_S5WbitOff.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_S5WbitOff.Location = new System.Drawing.Point(166, 22);
            this.opt_S5WbitOff.Name = "opt_S5WbitOff";
            this.opt_S5WbitOff.Size = new System.Drawing.Size(56, 26);
            this.opt_S5WbitOff.TabIndex = 1;
            this.opt_S5WbitOff.Text = "Off";
            this.opt_S5WbitOff.Click += new System.EventHandler(this.opt_S5WbitOff_Click);
            // 
            // opt_S5WbitOn
            // 
            this.opt_S5WbitOn.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_S5WbitOn.Location = new System.Drawing.Point(46, 22);
            this.opt_S5WbitOn.Name = "opt_S5WbitOn";
            this.opt_S5WbitOn.Size = new System.Drawing.Size(64, 26);
            this.opt_S5WbitOn.TabIndex = 0;
            this.opt_S5WbitOn.Text = "On";
            this.opt_S5WbitOn.Click += new System.EventHandler(this.opt_S5WbitOn_Click);
            // 
            // groupBox27
            // 
            this.groupBox27.Controls.Add(this.label20);
            this.groupBox27.Controls.Add(this.tbAlarmSetSV);
            this.groupBox27.Controls.Add(this.btnAlarmSet);
            this.groupBox27.Controls.Add(this.label24);
            this.groupBox27.Controls.Add(this.AlarmIDSet);
            this.groupBox27.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox27.Location = new System.Drawing.Point(7, 144);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new System.Drawing.Size(267, 97);
            this.groupBox27.TabIndex = 5;
            this.groupBox27.TabStop = false;
            this.groupBox27.Text = "Alarm Set";
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label20.Location = new System.Drawing.Point(127, 22);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(91, 24);
            this.label20.TabIndex = 19;
            this.label20.Text = "SV: SlarmSet (40)";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbAlarmSetSV
            // 
            this.tbAlarmSetSV.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbAlarmSetSV.Location = new System.Drawing.Point(218, 18);
            this.tbAlarmSetSV.Name = "tbAlarmSetSV";
            this.tbAlarmSetSV.Size = new System.Drawing.Size(42, 27);
            this.tbAlarmSetSV.TabIndex = 18;
            this.tbAlarmSetSV.Text = "0";
            this.tbAlarmSetSV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnAlarmSet
            // 
            this.btnAlarmSet.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAlarmSet.Location = new System.Drawing.Point(6, 57);
            this.btnAlarmSet.Name = "btnAlarmSet";
            this.btnAlarmSet.Size = new System.Drawing.Size(253, 32);
            this.btnAlarmSet.TabIndex = 17;
            this.btnAlarmSet.Text = "Alarm Set";
            this.btnAlarmSet.Click += new System.EventHandler(this.btnAlarmSet_Click);
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label24.Location = new System.Drawing.Point(6, 23);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(56, 24);
            this.label24.TabIndex = 16;
            this.label24.Text = "Alarm ID";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AlarmIDSet
            // 
            this.AlarmIDSet.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.AlarmIDSet.Location = new System.Drawing.Point(59, 19);
            this.AlarmIDSet.Name = "AlarmIDSet";
            this.AlarmIDSet.Size = new System.Drawing.Size(56, 27);
            this.AlarmIDSet.TabIndex = 15;
            this.AlarmIDSet.Text = "9001";
            // 
            // GroupBox21
            // 
            this.GroupBox21.Controls.Add(this.Label15);
            this.GroupBox21.Controls.Add(this.AlarmCD);
            this.GroupBox21.Controls.Add(this.AlarmReportSend);
            this.GroupBox21.Controls.Add(this.Label13);
            this.GroupBox21.Controls.Add(this.AlarmID);
            this.GroupBox21.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GroupBox21.Location = new System.Drawing.Point(7, 13);
            this.GroupBox21.Name = "GroupBox21";
            this.GroupBox21.Size = new System.Drawing.Size(265, 104);
            this.GroupBox21.TabIndex = 4;
            this.GroupBox21.TabStop = false;
            this.GroupBox21.Text = "Alarm Report Send";
            // 
            // Label15
            // 
            this.Label15.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label15.Location = new System.Drawing.Point(163, 26);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(40, 24);
            this.Label15.TabIndex = 19;
            this.Label15.Text = "CD";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AlarmCD
            // 
            this.AlarmCD.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.AlarmCD.Location = new System.Drawing.Point(198, 26);
            this.AlarmCD.Name = "AlarmCD";
            this.AlarmCD.Size = new System.Drawing.Size(59, 27);
            this.AlarmCD.TabIndex = 18;
            this.AlarmCD.Text = "128";
            // 
            // AlarmReportSend
            // 
            this.AlarmReportSend.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.AlarmReportSend.Location = new System.Drawing.Point(6, 64);
            this.AlarmReportSend.Name = "AlarmReportSend";
            this.AlarmReportSend.Size = new System.Drawing.Size(253, 32);
            this.AlarmReportSend.TabIndex = 17;
            this.AlarmReportSend.Text = "AlarmReportSend";
            this.AlarmReportSend.Click += new System.EventHandler(this.AlarmReportSend_Click);
            // 
            // Label13
            // 
            this.Label13.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label13.Location = new System.Drawing.Point(6, 31);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(56, 24);
            this.Label13.TabIndex = 16;
            this.Label13.Text = "Alarm ID";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AlarmID
            // 
            this.AlarmID.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.AlarmID.Location = new System.Drawing.Point(59, 27);
            this.AlarmID.Name = "AlarmID";
            this.AlarmID.Size = new System.Drawing.Size(56, 27);
            this.AlarmID.TabIndex = 15;
            this.AlarmID.Text = "9001";
            // 
            // Event
            // 
            this.Event.Controls.Add(this.GroupBox15);
            this.Event.Controls.Add(this.GroupBox18);
            this.Event.Location = new System.Drawing.Point(4, 26);
            this.Event.Name = "Event";
            this.Event.Size = new System.Drawing.Size(282, 664);
            this.Event.TabIndex = 4;
            this.Event.Text = "Manual - Event";
            this.Event.UseVisualStyleBackColor = true;
            // 
            // GroupBox15
            // 
            this.GroupBox15.Controls.Add(this.opt_S6WbitOff);
            this.GroupBox15.Controls.Add(this.opt_S6WbitOn);
            this.GroupBox15.Font = new System.Drawing.Font("新細明體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GroupBox15.Location = new System.Drawing.Point(8, 150);
            this.GroupBox15.Name = "GroupBox15";
            this.GroupBox15.Size = new System.Drawing.Size(265, 64);
            this.GroupBox15.TabIndex = 3;
            this.GroupBox15.TabStop = false;
            this.GroupBox15.Text = "S6 WBit <EC:22>";
            // 
            // opt_S6WbitOff
            // 
            this.opt_S6WbitOff.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_S6WbitOff.Location = new System.Drawing.Point(173, 22);
            this.opt_S6WbitOff.Name = "opt_S6WbitOff";
            this.opt_S6WbitOff.Size = new System.Drawing.Size(56, 26);
            this.opt_S6WbitOff.TabIndex = 1;
            this.opt_S6WbitOff.Text = "Off";
            this.opt_S6WbitOff.Click += new System.EventHandler(this.opt_S6WbitOff_Click);
            // 
            // opt_S6WbitOn
            // 
            this.opt_S6WbitOn.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_S6WbitOn.Location = new System.Drawing.Point(44, 22);
            this.opt_S6WbitOn.Name = "opt_S6WbitOn";
            this.opt_S6WbitOn.Size = new System.Drawing.Size(64, 26);
            this.opt_S6WbitOn.TabIndex = 0;
            this.opt_S6WbitOn.Text = "On";
            this.opt_S6WbitOn.Click += new System.EventHandler(this.opt_S6WbitOn_Click);
            // 
            // GroupBox18
            // 
            this.GroupBox18.Controls.Add(this.SendEvent);
            this.GroupBox18.Controls.Add(this.Label4);
            this.GroupBox18.Controls.Add(this.EventID);
            this.GroupBox18.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GroupBox18.Location = new System.Drawing.Point(8, 6);
            this.GroupBox18.Name = "GroupBox18";
            this.GroupBox18.Size = new System.Drawing.Size(265, 120);
            this.GroupBox18.TabIndex = 2;
            this.GroupBox18.TabStop = false;
            this.GroupBox18.Text = "Event Report Send";
            // 
            // SendEvent
            // 
            this.SendEvent.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SendEvent.Location = new System.Drawing.Point(6, 79);
            this.SendEvent.Name = "SendEvent";
            this.SendEvent.Size = new System.Drawing.Size(253, 32);
            this.SendEvent.TabIndex = 14;
            this.SendEvent.Text = "Event Report Send";
            this.SendEvent.Click += new System.EventHandler(this.SendEvent_Click);
            // 
            // Label4
            // 
            this.Label4.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label4.Location = new System.Drawing.Point(0, 33);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(84, 24);
            this.Label4.TabIndex = 13;
            this.Label4.Text = "Event ID";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EventID
            // 
            this.EventID.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.EventID.Location = new System.Drawing.Point(87, 34);
            this.EventID.Name = "EventID";
            this.EventID.Size = new System.Drawing.Size(172, 27);
            this.EventID.TabIndex = 12;
            this.EventID.Text = "101";
            // 
            // Spooling
            // 
            this.Spooling.Controls.Add(this.GroupBox24);
            this.Spooling.Controls.Add(this.GroupBox23);
            this.Spooling.Controls.Add(this.GroupBox25);
            this.Spooling.Location = new System.Drawing.Point(4, 26);
            this.Spooling.Name = "Spooling";
            this.Spooling.Size = new System.Drawing.Size(282, 664);
            this.Spooling.TabIndex = 5;
            this.Spooling.Text = "Manual - Spooling";
            this.Spooling.UseVisualStyleBackColor = true;
            // 
            // GroupBox24
            // 
            this.GroupBox24.Controls.Add(this.opt_OverWriteSpoolDisable);
            this.GroupBox24.Controls.Add(this.opt_OverWriteSpoolEnable);
            this.GroupBox24.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GroupBox24.Location = new System.Drawing.Point(10, 531);
            this.GroupBox24.Name = "GroupBox24";
            this.GroupBox24.Size = new System.Drawing.Size(261, 90);
            this.GroupBox24.TabIndex = 13;
            this.GroupBox24.TabStop = false;
            this.GroupBox24.Text = "Over Write Spool <EC:67>";
            // 
            // opt_OverWriteSpoolDisable
            // 
            this.opt_OverWriteSpoolDisable.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_OverWriteSpoolDisable.Location = new System.Drawing.Point(11, 56);
            this.opt_OverWriteSpoolDisable.Name = "opt_OverWriteSpoolDisable";
            this.opt_OverWriteSpoolDisable.Size = new System.Drawing.Size(82, 24);
            this.opt_OverWriteSpoolDisable.TabIndex = 1;
            this.opt_OverWriteSpoolDisable.Text = "Disable";
            this.opt_OverWriteSpoolDisable.Click += new System.EventHandler(this.opt_OverWriteSpoolDisable_Click);
            // 
            // opt_OverWriteSpoolEnable
            // 
            this.opt_OverWriteSpoolEnable.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_OverWriteSpoolEnable.Location = new System.Drawing.Point(11, 26);
            this.opt_OverWriteSpoolEnable.Name = "opt_OverWriteSpoolEnable";
            this.opt_OverWriteSpoolEnable.Size = new System.Drawing.Size(72, 24);
            this.opt_OverWriteSpoolEnable.TabIndex = 0;
            this.opt_OverWriteSpoolEnable.Text = "Enable";
            this.opt_OverWriteSpoolEnable.Click += new System.EventHandler(this.opt_OverWriteSpoolEnable_Click);
            // 
            // GroupBox23
            // 
            this.GroupBox23.Controls.Add(this.opt_ConfigSpoolDisable);
            this.GroupBox23.Controls.Add(this.opt_ConfigSpoolEnable);
            this.GroupBox23.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GroupBox23.Location = new System.Drawing.Point(10, 416);
            this.GroupBox23.Name = "GroupBox23";
            this.GroupBox23.Size = new System.Drawing.Size(261, 91);
            this.GroupBox23.TabIndex = 12;
            this.GroupBox23.TabStop = false;
            this.GroupBox23.Text = "Config Spool <EC:66>";
            // 
            // opt_ConfigSpoolDisable
            // 
            this.opt_ConfigSpoolDisable.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_ConfigSpoolDisable.Location = new System.Drawing.Point(11, 58);
            this.opt_ConfigSpoolDisable.Name = "opt_ConfigSpoolDisable";
            this.opt_ConfigSpoolDisable.Size = new System.Drawing.Size(80, 26);
            this.opt_ConfigSpoolDisable.TabIndex = 1;
            this.opt_ConfigSpoolDisable.Text = "Disable";
            this.opt_ConfigSpoolDisable.Click += new System.EventHandler(this.opt_ConfigSpoolDisable_Click);
            // 
            // opt_ConfigSpoolEnable
            // 
            this.opt_ConfigSpoolEnable.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_ConfigSpoolEnable.Location = new System.Drawing.Point(11, 26);
            this.opt_ConfigSpoolEnable.Name = "opt_ConfigSpoolEnable";
            this.opt_ConfigSpoolEnable.Size = new System.Drawing.Size(72, 26);
            this.opt_ConfigSpoolEnable.TabIndex = 0;
            this.opt_ConfigSpoolEnable.Text = "Enable";
            this.opt_ConfigSpoolEnable.Click += new System.EventHandler(this.opt_ConfigSpoolEnable_Click);
            // 
            // GroupBox25
            // 
            this.GroupBox25.Controls.Add(this.lbl_SpoolActualCount);
            this.GroupBox25.Controls.Add(this.Label19);
            this.GroupBox25.Controls.Add(this.lbl_MaxSpoolTransmit);
            this.GroupBox25.Controls.Add(this.Label27);
            this.GroupBox25.Controls.Add(this.lbl_SpoolStartTime);
            this.GroupBox25.Controls.Add(this.Label25);
            this.GroupBox25.Controls.Add(this.lbl_SpoolFullTime);
            this.GroupBox25.Controls.Add(this.Label23);
            this.GroupBox25.Controls.Add(this.lbl_SpoolTotalCount);
            this.GroupBox25.Controls.Add(this.Label21);
            this.GroupBox25.Location = new System.Drawing.Point(10, 3);
            this.GroupBox25.Name = "GroupBox25";
            this.GroupBox25.Size = new System.Drawing.Size(261, 385);
            this.GroupBox25.TabIndex = 11;
            this.GroupBox25.TabStop = false;
            // 
            // lbl_SpoolActualCount
            // 
            this.lbl_SpoolActualCount.BackColor = System.Drawing.Color.White;
            this.lbl_SpoolActualCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_SpoolActualCount.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_SpoolActualCount.Location = new System.Drawing.Point(11, 45);
            this.lbl_SpoolActualCount.Name = "lbl_SpoolActualCount";
            this.lbl_SpoolActualCount.Size = new System.Drawing.Size(235, 24);
            this.lbl_SpoolActualCount.TabIndex = 19;
            // 
            // Label19
            // 
            this.Label19.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label19.Location = new System.Drawing.Point(12, 16);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(212, 24);
            this.Label19.TabIndex = 18;
            this.Label19.Text = "Spool Actual Count<SV:53>";
            this.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_MaxSpoolTransmit
            // 
            this.lbl_MaxSpoolTransmit.BackColor = System.Drawing.Color.White;
            this.lbl_MaxSpoolTransmit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_MaxSpoolTransmit.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_MaxSpoolTransmit.Location = new System.Drawing.Point(11, 346);
            this.lbl_MaxSpoolTransmit.Name = "lbl_MaxSpoolTransmit";
            this.lbl_MaxSpoolTransmit.Size = new System.Drawing.Size(235, 24);
            this.lbl_MaxSpoolTransmit.TabIndex = 17;
            // 
            // Label27
            // 
            this.Label27.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label27.Location = new System.Drawing.Point(12, 321);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(249, 24);
            this.Label27.TabIndex = 16;
            this.Label27.Text = "Max Spool Transmit <EC:52>";
            this.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_SpoolStartTime
            // 
            this.lbl_SpoolStartTime.BackColor = System.Drawing.Color.White;
            this.lbl_SpoolStartTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_SpoolStartTime.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_SpoolStartTime.Location = new System.Drawing.Point(11, 264);
            this.lbl_SpoolStartTime.Name = "lbl_SpoolStartTime";
            this.lbl_SpoolStartTime.Size = new System.Drawing.Size(235, 24);
            this.lbl_SpoolStartTime.TabIndex = 15;
            // 
            // Label25
            // 
            this.Label25.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label25.Location = new System.Drawing.Point(12, 238);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(234, 24);
            this.Label25.TabIndex = 14;
            this.Label25.Text = "Spool Start Time<SV:57>";
            this.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_SpoolFullTime
            // 
            this.lbl_SpoolFullTime.BackColor = System.Drawing.Color.White;
            this.lbl_SpoolFullTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_SpoolFullTime.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_SpoolFullTime.Location = new System.Drawing.Point(11, 188);
            this.lbl_SpoolFullTime.Name = "lbl_SpoolFullTime";
            this.lbl_SpoolFullTime.Size = new System.Drawing.Size(235, 24);
            this.lbl_SpoolFullTime.TabIndex = 13;
            // 
            // Label23
            // 
            this.Label23.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label23.Location = new System.Drawing.Point(12, 161);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(234, 24);
            this.Label23.TabIndex = 12;
            this.Label23.Text = "Spool Full Time <SV:55>";
            this.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_SpoolTotalCount
            // 
            this.lbl_SpoolTotalCount.BackColor = System.Drawing.Color.White;
            this.lbl_SpoolTotalCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_SpoolTotalCount.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbl_SpoolTotalCount.Location = new System.Drawing.Point(11, 113);
            this.lbl_SpoolTotalCount.Name = "lbl_SpoolTotalCount";
            this.lbl_SpoolTotalCount.Size = new System.Drawing.Size(235, 24);
            this.lbl_SpoolTotalCount.TabIndex = 11;
            // 
            // Label21
            // 
            this.Label21.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label21.Location = new System.Drawing.Point(12, 86);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(234, 24);
            this.Label21.TabIndex = 10;
            this.Label21.Text = "Spool Total Count<SV:54>";
            this.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Terminal
            // 
            this.Terminal.Controls.Add(this.GroupBox22);
            this.Terminal.Controls.Add(this.txt_TerminalTextToHost);
            this.Terminal.Controls.Add(this.SendTerminalMsg);
            this.Terminal.Controls.Add(this.label16);
            this.Terminal.Controls.Add(this.btn_MsgRecognitionEvent);
            this.Terminal.Controls.Add(this.label28);
            this.Terminal.Controls.Add(this.txt_MessageFromHost);
            this.Terminal.Location = new System.Drawing.Point(4, 26);
            this.Terminal.Name = "Terminal";
            this.Terminal.Size = new System.Drawing.Size(282, 664);
            this.Terminal.TabIndex = 6;
            this.Terminal.Text = "Manual - Terminal";
            this.Terminal.UseVisualStyleBackColor = true;
            // 
            // GroupBox22
            // 
            this.GroupBox22.Controls.Add(this.opt_S10WbitOff);
            this.GroupBox22.Controls.Add(this.opt_S10WbitOn);
            this.GroupBox22.Location = new System.Drawing.Point(10, 297);
            this.GroupBox22.Name = "GroupBox22";
            this.GroupBox22.Size = new System.Drawing.Size(259, 65);
            this.GroupBox22.TabIndex = 24;
            this.GroupBox22.TabStop = false;
            this.GroupBox22.Text = "S10 WBit <EC:23>";
            // 
            // opt_S10WbitOff
            // 
            this.opt_S10WbitOff.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_S10WbitOff.Location = new System.Drawing.Point(167, 27);
            this.opt_S10WbitOff.Name = "opt_S10WbitOff";
            this.opt_S10WbitOff.Size = new System.Drawing.Size(56, 26);
            this.opt_S10WbitOff.TabIndex = 1;
            this.opt_S10WbitOff.Text = "Off";
            this.opt_S10WbitOff.Click += new System.EventHandler(this.opt_S10WbitOff_Click);
            // 
            // opt_S10WbitOn
            // 
            this.opt_S10WbitOn.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.opt_S10WbitOn.Location = new System.Drawing.Point(46, 27);
            this.opt_S10WbitOn.Name = "opt_S10WbitOn";
            this.opt_S10WbitOn.Size = new System.Drawing.Size(64, 26);
            this.opt_S10WbitOn.TabIndex = 0;
            this.opt_S10WbitOn.Text = "On";
            this.opt_S10WbitOn.Click += new System.EventHandler(this.opt_S10WbitOn_Click);
            // 
            // txt_TerminalTextToHost
            // 
            this.txt_TerminalTextToHost.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txt_TerminalTextToHost.Location = new System.Drawing.Point(10, 179);
            this.txt_TerminalTextToHost.Name = "txt_TerminalTextToHost";
            this.txt_TerminalTextToHost.Size = new System.Drawing.Size(259, 27);
            this.txt_TerminalTextToHost.TabIndex = 23;
            this.txt_TerminalTextToHost.Text = "Hi ! This is Eqp\'s Message";
            // 
            // SendTerminalMsg
            // 
            this.SendTerminalMsg.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SendTerminalMsg.Location = new System.Drawing.Point(10, 214);
            this.SendTerminalMsg.Name = "SendTerminalMsg";
            this.SendTerminalMsg.Size = new System.Drawing.Size(259, 32);
            this.SendTerminalMsg.TabIndex = 22;
            this.SendTerminalMsg.Text = "Terminal Message Send";
            this.SendTerminalMsg.Click += new System.EventHandler(this.SendTerminalMsg_Click);
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label16.Location = new System.Drawing.Point(70, 141);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(160, 24);
            this.label16.TabIndex = 21;
            this.label16.Text = "Message To Host";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_MsgRecognitionEvent
            // 
            this.btn_MsgRecognitionEvent.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_MsgRecognitionEvent.Location = new System.Drawing.Point(10, 73);
            this.btn_MsgRecognitionEvent.Name = "btn_MsgRecognitionEvent";
            this.btn_MsgRecognitionEvent.Size = new System.Drawing.Size(259, 32);
            this.btn_MsgRecognitionEvent.TabIndex = 20;
            this.btn_MsgRecognitionEvent.Text = "Msg Recognition Event Send";
            this.btn_MsgRecognitionEvent.Click += new System.EventHandler(this.btn_MsgRecognitionEvent_Click);
            // 
            // label28
            // 
            this.label28.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label28.Location = new System.Drawing.Point(63, 9);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(160, 24);
            this.label28.TabIndex = 19;
            this.label28.Text = "Message From Host";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_MessageFromHost
            // 
            this.txt_MessageFromHost.BackColor = System.Drawing.Color.White;
            this.txt_MessageFromHost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_MessageFromHost.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txt_MessageFromHost.Location = new System.Drawing.Point(10, 41);
            this.txt_MessageFromHost.Name = "txt_MessageFromHost";
            this.txt_MessageFromHost.Size = new System.Drawing.Size(259, 24);
            this.txt_MessageFromHost.TabIndex = 6;
            // 
            // PP
            // 
            this.PP.Controls.Add(this.groupBox26);
            this.PP.Controls.Add(this.FrameGuickGEM);
            this.PP.Location = new System.Drawing.Point(4, 26);
            this.PP.Name = "PP";
            this.PP.Size = new System.Drawing.Size(282, 664);
            this.PP.TabIndex = 7;
            this.PP.Text = "Manual - PP";
            this.PP.UseVisualStyleBackColor = true;
            // 
            // groupBox26
            // 
            this.groupBox26.Controls.Add(this.Cmd_SendPP_Formatted);
            this.groupBox26.Controls.Add(this.Cmd_RequestPP_Formatted);
            this.groupBox26.Controls.Add(this.Cmd_RequestPP);
            this.groupBox26.Controls.Add(this.Cmd_SendPP);
            this.groupBox26.Controls.Add(this.Cmd_LoadInquire);
            this.groupBox26.Controls.Add(this.txtPPID);
            this.groupBox26.Location = new System.Drawing.Point(11, 113);
            this.groupBox26.Name = "groupBox26";
            this.groupBox26.Size = new System.Drawing.Size(262, 280);
            this.groupBox26.TabIndex = 17;
            this.groupBox26.TabStop = false;
            this.groupBox26.Text = "PPID";
            // 
            // Cmd_SendPP_Formatted
            // 
            this.Cmd_SendPP_Formatted.Location = new System.Drawing.Point(6, 199);
            this.Cmd_SendPP_Formatted.Name = "Cmd_SendPP_Formatted";
            this.Cmd_SendPP_Formatted.Size = new System.Drawing.Size(250, 30);
            this.Cmd_SendPP_Formatted.TabIndex = 12;
            this.Cmd_SendPP_Formatted.Text = "Send  PP (S7F23)";
            this.Cmd_SendPP_Formatted.UseVisualStyleBackColor = true;
            this.Cmd_SendPP_Formatted.Click += new System.EventHandler(this.Cmd_SendPP_Formatted_Click);
            // 
            // Cmd_RequestPP_Formatted
            // 
            this.Cmd_RequestPP_Formatted.Location = new System.Drawing.Point(6, 239);
            this.Cmd_RequestPP_Formatted.Name = "Cmd_RequestPP_Formatted";
            this.Cmd_RequestPP_Formatted.Size = new System.Drawing.Size(250, 30);
            this.Cmd_RequestPP_Formatted.TabIndex = 12;
            this.Cmd_RequestPP_Formatted.Text = "Request  PP (S7F25)";
            this.Cmd_RequestPP_Formatted.UseVisualStyleBackColor = true;
            this.Cmd_RequestPP_Formatted.Click += new System.EventHandler(this.Cmd_RequestPP_Formatted_Click);
            // 
            // Cmd_RequestPP
            // 
            this.Cmd_RequestPP.Location = new System.Drawing.Point(6, 159);
            this.Cmd_RequestPP.Name = "Cmd_RequestPP";
            this.Cmd_RequestPP.Size = new System.Drawing.Size(250, 30);
            this.Cmd_RequestPP.TabIndex = 12;
            this.Cmd_RequestPP.Text = "Request  PP (S7F5)";
            this.Cmd_RequestPP.UseVisualStyleBackColor = true;
            this.Cmd_RequestPP.Click += new System.EventHandler(this.Cmd_RequestPP_Click);
            // 
            // Cmd_SendPP
            // 
            this.Cmd_SendPP.Location = new System.Drawing.Point(6, 119);
            this.Cmd_SendPP.Name = "Cmd_SendPP";
            this.Cmd_SendPP.Size = new System.Drawing.Size(250, 30);
            this.Cmd_SendPP.TabIndex = 12;
            this.Cmd_SendPP.Text = "Send  PP (S7F3)";
            this.Cmd_SendPP.UseVisualStyleBackColor = true;
            this.Cmd_SendPP.Click += new System.EventHandler(this.Cmd_SendPP_Click);
            // 
            // Cmd_LoadInquire
            // 
            this.Cmd_LoadInquire.Location = new System.Drawing.Point(6, 79);
            this.Cmd_LoadInquire.Name = "Cmd_LoadInquire";
            this.Cmd_LoadInquire.Size = new System.Drawing.Size(250, 30);
            this.Cmd_LoadInquire.TabIndex = 12;
            this.Cmd_LoadInquire.Text = "Load Inquire (S7F1)";
            this.Cmd_LoadInquire.UseVisualStyleBackColor = true;
            this.Cmd_LoadInquire.Click += new System.EventHandler(this.Cmd_LoadInquire_Click);
            // 
            // txtPPID
            // 
            this.txtPPID.Location = new System.Drawing.Point(6, 30);
            this.txtPPID.Name = "txtPPID";
            this.txtPPID.Size = new System.Drawing.Size(250, 27);
            this.txtPPID.TabIndex = 0;
            this.txtPPID.Text = "PP1";
            // 
            // FrameGuickGEM
            // 
            this.FrameGuickGEM.Controls.Add(this.PPEventText);
            this.FrameGuickGEM.Location = new System.Drawing.Point(11, 5);
            this.FrameGuickGEM.Name = "FrameGuickGEM";
            this.FrameGuickGEM.Size = new System.Drawing.Size(262, 70);
            this.FrameGuickGEM.TabIndex = 16;
            this.FrameGuickGEM.TabStop = false;
            this.FrameGuickGEM.Text = "PP Event";
            // 
            // PPEventText
            // 
            this.PPEventText.Location = new System.Drawing.Point(6, 26);
            this.PPEventText.Name = "PPEventText";
            this.PPEventText.Size = new System.Drawing.Size(250, 27);
            this.PPEventText.TabIndex = 0;
            // 
            // Remote
            // 
            this.Remote.Controls.Add(this.label36);
            this.Remote.Controls.Add(this.label35);
            this.Remote.Controls.Add(this.txCPACK2);
            this.Remote.Controls.Add(this.txCPACK1);
            this.Remote.Controls.Add(this.txHCACK);
            this.Remote.Controls.Add(this.label34);
            this.Remote.Controls.Add(this.lbCPVAL2);
            this.Remote.Controls.Add(this.label31);
            this.Remote.Controls.Add(this.lbCPNAME2);
            this.Remote.Controls.Add(this.label33);
            this.Remote.Controls.Add(this.lbCPVAL1);
            this.Remote.Controls.Add(this.label29);
            this.Remote.Controls.Add(this.lbCPNAME1);
            this.Remote.Controls.Add(this.label26);
            this.Remote.Controls.Add(this.lbRemoteCmd);
            this.Remote.Controls.Add(this.label22);
            this.Remote.Location = new System.Drawing.Point(4, 26);
            this.Remote.Name = "Remote";
            this.Remote.Size = new System.Drawing.Size(282, 664);
            this.Remote.TabIndex = 8;
            this.Remote.Text = "Manual - Remote";
            this.Remote.UseVisualStyleBackColor = true;
            // 
            // label36
            // 
            this.label36.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label36.Location = new System.Drawing.Point(7, 98);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(77, 24);
            this.label36.TabIndex = 34;
            this.label36.Text = "CPACK2";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label35
            // 
            this.label35.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label35.Location = new System.Drawing.Point(6, 54);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(78, 24);
            this.label35.TabIndex = 35;
            this.label35.Text = "CPACK1";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txCPACK2
            // 
            this.txCPACK2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txCPACK2.Location = new System.Drawing.Point(103, 99);
            this.txCPACK2.Name = "txCPACK2";
            this.txCPACK2.Size = new System.Drawing.Size(166, 27);
            this.txCPACK2.TabIndex = 32;
            // 
            // txCPACK1
            // 
            this.txCPACK1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txCPACK1.Location = new System.Drawing.Point(103, 55);
            this.txCPACK1.Name = "txCPACK1";
            this.txCPACK1.Size = new System.Drawing.Size(166, 27);
            this.txCPACK1.TabIndex = 33;
            // 
            // txHCACK
            // 
            this.txHCACK.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txHCACK.Location = new System.Drawing.Point(103, 12);
            this.txHCACK.Name = "txHCACK";
            this.txHCACK.Size = new System.Drawing.Size(166, 27);
            this.txHCACK.TabIndex = 30;
            // 
            // label34
            // 
            this.label34.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label34.Location = new System.Drawing.Point(3, 12);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(80, 24);
            this.label34.TabIndex = 31;
            this.label34.Text = "HCACK";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCPVAL2
            // 
            this.lbCPVAL2.BackColor = System.Drawing.Color.White;
            this.lbCPVAL2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCPVAL2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbCPVAL2.Location = new System.Drawing.Point(103, 320);
            this.lbCPVAL2.Name = "lbCPVAL2";
            this.lbCPVAL2.Size = new System.Drawing.Size(166, 24);
            this.lbCPVAL2.TabIndex = 29;
            this.lbCPVAL2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label31
            // 
            this.label31.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label31.Location = new System.Drawing.Point(-1, 320);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(84, 24);
            this.label31.TabIndex = 28;
            this.label31.Text = "CPVAL2";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCPNAME2
            // 
            this.lbCPNAME2.BackColor = System.Drawing.Color.White;
            this.lbCPNAME2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCPNAME2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbCPNAME2.Location = new System.Drawing.Point(103, 276);
            this.lbCPNAME2.Name = "lbCPNAME2";
            this.lbCPNAME2.Size = new System.Drawing.Size(166, 24);
            this.lbCPNAME2.TabIndex = 27;
            this.lbCPNAME2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label33
            // 
            this.label33.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label33.Location = new System.Drawing.Point(-2, 276);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(97, 24);
            this.label33.TabIndex = 26;
            this.label33.Text = "CPNAME2";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCPVAL1
            // 
            this.lbCPVAL1.BackColor = System.Drawing.Color.White;
            this.lbCPVAL1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCPVAL1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbCPVAL1.Location = new System.Drawing.Point(103, 232);
            this.lbCPVAL1.Name = "lbCPVAL1";
            this.lbCPVAL1.Size = new System.Drawing.Size(166, 24);
            this.lbCPVAL1.TabIndex = 25;
            this.lbCPVAL1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label29
            // 
            this.label29.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label29.Location = new System.Drawing.Point(-1, 232);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(81, 24);
            this.label29.TabIndex = 24;
            this.label29.Text = "CPVAL1";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCPNAME1
            // 
            this.lbCPNAME1.BackColor = System.Drawing.Color.White;
            this.lbCPNAME1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCPNAME1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbCPNAME1.Location = new System.Drawing.Point(103, 189);
            this.lbCPNAME1.Name = "lbCPNAME1";
            this.lbCPNAME1.Size = new System.Drawing.Size(166, 24);
            this.lbCPNAME1.TabIndex = 23;
            this.lbCPNAME1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label26.Location = new System.Drawing.Point(-6, 188);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(102, 24);
            this.label26.TabIndex = 22;
            this.label26.Text = "CPNAME1";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbRemoteCmd
            // 
            this.lbRemoteCmd.BackColor = System.Drawing.Color.White;
            this.lbRemoteCmd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbRemoteCmd.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbRemoteCmd.Location = new System.Drawing.Point(103, 146);
            this.lbRemoteCmd.Name = "lbRemoteCmd";
            this.lbRemoteCmd.Size = new System.Drawing.Size(166, 24);
            this.lbRemoteCmd.TabIndex = 21;
            this.lbRemoteCmd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label22.Location = new System.Drawing.Point(0, 146);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(102, 24);
            this.label22.TabIndex = 20;
            this.label22.Text = "Remote Cmd";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1160, 24);
            this.menuStrip1.TabIndex = 23;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // SettingMenu
            // 
            this.SettingMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Config,
            this.Exit});
            this.SettingMenu.Name = "SettingMenu";
            this.SettingMenu.Size = new System.Drawing.Size(52, 20);
            this.SettingMenu.Text = "Menu";
            // 
            // Config
            // 
            this.Config.Name = "Config";
            this.Config.Size = new System.Drawing.Size(112, 22);
            this.Config.Text = "Config";
            this.Config.Click += new System.EventHandler(this.Config_Click);
            // 
            // Exit
            // 
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(112, 22);
            this.Exit.Text = "Exit";
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // groupBox_LD_Info
            // 
            this.groupBox_LD_Info.Controls.Add(this.label_RecipeName);
            this.groupBox_LD_Info.Controls.Add(this.label_LD_MagazineID);
            this.groupBox_LD_Info.Controls.Add(this.label_LotID);
            this.groupBox_LD_Info.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_LD_Info.Location = new System.Drawing.Point(870, 46);
            this.groupBox_LD_Info.Name = "groupBox_LD_Info";
            this.groupBox_LD_Info.Size = new System.Drawing.Size(281, 132);
            this.groupBox_LD_Info.TabIndex = 28;
            this.groupBox_LD_Info.TabStop = false;
            this.groupBox_LD_Info.Text = "Loader Info";
            // 
            // label_RecipeName
            // 
            this.label_RecipeName.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_RecipeName.Location = new System.Drawing.Point(6, 92);
            this.label_RecipeName.Name = "label_RecipeName";
            this.label_RecipeName.Size = new System.Drawing.Size(269, 38);
            this.label_RecipeName.TabIndex = 29;
            this.label_RecipeName.Text = "Recipe : ";
            this.label_RecipeName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_LD_MagazineID
            // 
            this.label_LD_MagazineID.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_LD_MagazineID.Location = new System.Drawing.Point(6, 55);
            this.label_LD_MagazineID.Name = "label_LD_MagazineID";
            this.label_LD_MagazineID.Size = new System.Drawing.Size(269, 38);
            this.label_LD_MagazineID.TabIndex = 28;
            this.label_LD_MagazineID.Text = "Magazine ID : ";
            this.label_LD_MagazineID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_LotID
            // 
            this.label_LotID.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_LotID.Location = new System.Drawing.Point(6, 17);
            this.label_LotID.Name = "label_LotID";
            this.label_LotID.Size = new System.Drawing.Size(269, 38);
            this.label_LotID.TabIndex = 27;
            this.label_LotID.Text = "Lot ID : ";
            this.label_LotID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox_ULD_Pass1MagazineInfo
            // 
            this.groupBox_ULD_Pass1MagazineInfo.Controls.Add(this.label_ULD_Pass1MagazineUnitCount);
            this.groupBox_ULD_Pass1MagazineInfo.Controls.Add(this.label_ULD_Pass1MagazineBoatCount);
            this.groupBox_ULD_Pass1MagazineInfo.Controls.Add(this.label_ULD_Pass1MagazineID);
            this.groupBox_ULD_Pass1MagazineInfo.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_ULD_Pass1MagazineInfo.Location = new System.Drawing.Point(870, 187);
            this.groupBox_ULD_Pass1MagazineInfo.Name = "groupBox_ULD_Pass1MagazineInfo";
            this.groupBox_ULD_Pass1MagazineInfo.Size = new System.Drawing.Size(281, 134);
            this.groupBox_ULD_Pass1MagazineInfo.TabIndex = 29;
            this.groupBox_ULD_Pass1MagazineInfo.TabStop = false;
            this.groupBox_ULD_Pass1MagazineInfo.Text = "Pass1Magazine Info";
            // 
            // label_ULD_Pass1MagazineUnitCount
            // 
            this.label_ULD_Pass1MagazineUnitCount.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_ULD_Pass1MagazineUnitCount.Location = new System.Drawing.Point(6, 94);
            this.label_ULD_Pass1MagazineUnitCount.Name = "label_ULD_Pass1MagazineUnitCount";
            this.label_ULD_Pass1MagazineUnitCount.Size = new System.Drawing.Size(269, 38);
            this.label_ULD_Pass1MagazineUnitCount.TabIndex = 30;
            this.label_ULD_Pass1MagazineUnitCount.Text = "Unit Count : ";
            this.label_ULD_Pass1MagazineUnitCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_ULD_Pass1MagazineBoatCount
            // 
            this.label_ULD_Pass1MagazineBoatCount.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_ULD_Pass1MagazineBoatCount.Location = new System.Drawing.Point(6, 56);
            this.label_ULD_Pass1MagazineBoatCount.Name = "label_ULD_Pass1MagazineBoatCount";
            this.label_ULD_Pass1MagazineBoatCount.Size = new System.Drawing.Size(269, 38);
            this.label_ULD_Pass1MagazineBoatCount.TabIndex = 29;
            this.label_ULD_Pass1MagazineBoatCount.Text = "Boat Count : ";
            this.label_ULD_Pass1MagazineBoatCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_ULD_Pass1MagazineID
            // 
            this.label_ULD_Pass1MagazineID.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_ULD_Pass1MagazineID.Location = new System.Drawing.Point(6, 18);
            this.label_ULD_Pass1MagazineID.Name = "label_ULD_Pass1MagazineID";
            this.label_ULD_Pass1MagazineID.Size = new System.Drawing.Size(269, 38);
            this.label_ULD_Pass1MagazineID.TabIndex = 28;
            this.label_ULD_Pass1MagazineID.Text = "Magazine ID : ";
            this.label_ULD_Pass1MagazineID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox_ULD_Pass2MagazineInfo
            // 
            this.groupBox_ULD_Pass2MagazineInfo.Controls.Add(this.label_ULD_Pass2MagazineUnitCount);
            this.groupBox_ULD_Pass2MagazineInfo.Controls.Add(this.label_ULD_Pass2MagazineBoatCount);
            this.groupBox_ULD_Pass2MagazineInfo.Controls.Add(this.label_ULD_Pass2MagazineID);
            this.groupBox_ULD_Pass2MagazineInfo.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_ULD_Pass2MagazineInfo.Location = new System.Drawing.Point(870, 333);
            this.groupBox_ULD_Pass2MagazineInfo.Name = "groupBox_ULD_Pass2MagazineInfo";
            this.groupBox_ULD_Pass2MagazineInfo.Size = new System.Drawing.Size(281, 134);
            this.groupBox_ULD_Pass2MagazineInfo.TabIndex = 30;
            this.groupBox_ULD_Pass2MagazineInfo.TabStop = false;
            this.groupBox_ULD_Pass2MagazineInfo.Text = "Pass2Magazine Info";
            // 
            // label_ULD_Pass2MagazineUnitCount
            // 
            this.label_ULD_Pass2MagazineUnitCount.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_ULD_Pass2MagazineUnitCount.Location = new System.Drawing.Point(6, 94);
            this.label_ULD_Pass2MagazineUnitCount.Name = "label_ULD_Pass2MagazineUnitCount";
            this.label_ULD_Pass2MagazineUnitCount.Size = new System.Drawing.Size(269, 38);
            this.label_ULD_Pass2MagazineUnitCount.TabIndex = 31;
            this.label_ULD_Pass2MagazineUnitCount.Text = "Unit Count : ";
            this.label_ULD_Pass2MagazineUnitCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_ULD_Pass2MagazineBoatCount
            // 
            this.label_ULD_Pass2MagazineBoatCount.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_ULD_Pass2MagazineBoatCount.Location = new System.Drawing.Point(6, 56);
            this.label_ULD_Pass2MagazineBoatCount.Name = "label_ULD_Pass2MagazineBoatCount";
            this.label_ULD_Pass2MagazineBoatCount.Size = new System.Drawing.Size(269, 38);
            this.label_ULD_Pass2MagazineBoatCount.TabIndex = 29;
            this.label_ULD_Pass2MagazineBoatCount.Text = "Boat Count : ";
            this.label_ULD_Pass2MagazineBoatCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_ULD_Pass2MagazineID
            // 
            this.label_ULD_Pass2MagazineID.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_ULD_Pass2MagazineID.Location = new System.Drawing.Point(6, 18);
            this.label_ULD_Pass2MagazineID.Name = "label_ULD_Pass2MagazineID";
            this.label_ULD_Pass2MagazineID.Size = new System.Drawing.Size(269, 38);
            this.label_ULD_Pass2MagazineID.TabIndex = 28;
            this.label_ULD_Pass2MagazineID.Text = "Magazine ID : ";
            this.label_ULD_Pass2MagazineID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox_ULD_NGMagazineInfo
            // 
            this.groupBox_ULD_NGMagazineInfo.Controls.Add(this.label_ULD_NGMagazineUnitCount);
            this.groupBox_ULD_NGMagazineInfo.Controls.Add(this.label_ULD_NGMagazineBoatCount);
            this.groupBox_ULD_NGMagazineInfo.Controls.Add(this.label_ULD_NGMagazineID);
            this.groupBox_ULD_NGMagazineInfo.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_ULD_NGMagazineInfo.Location = new System.Drawing.Point(870, 478);
            this.groupBox_ULD_NGMagazineInfo.Name = "groupBox_ULD_NGMagazineInfo";
            this.groupBox_ULD_NGMagazineInfo.Size = new System.Drawing.Size(281, 134);
            this.groupBox_ULD_NGMagazineInfo.TabIndex = 31;
            this.groupBox_ULD_NGMagazineInfo.TabStop = false;
            this.groupBox_ULD_NGMagazineInfo.Text = "NGMagazine Info";
            // 
            // label_ULD_NGMagazineUnitCount
            // 
            this.label_ULD_NGMagazineUnitCount.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_ULD_NGMagazineUnitCount.Location = new System.Drawing.Point(6, 94);
            this.label_ULD_NGMagazineUnitCount.Name = "label_ULD_NGMagazineUnitCount";
            this.label_ULD_NGMagazineUnitCount.Size = new System.Drawing.Size(269, 38);
            this.label_ULD_NGMagazineUnitCount.TabIndex = 32;
            this.label_ULD_NGMagazineUnitCount.Text = "Unit Count : ";
            this.label_ULD_NGMagazineUnitCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_ULD_NGMagazineBoatCount
            // 
            this.label_ULD_NGMagazineBoatCount.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_ULD_NGMagazineBoatCount.Location = new System.Drawing.Point(6, 56);
            this.label_ULD_NGMagazineBoatCount.Name = "label_ULD_NGMagazineBoatCount";
            this.label_ULD_NGMagazineBoatCount.Size = new System.Drawing.Size(269, 38);
            this.label_ULD_NGMagazineBoatCount.TabIndex = 29;
            this.label_ULD_NGMagazineBoatCount.Text = "Boat Count : ";
            this.label_ULD_NGMagazineBoatCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_ULD_NGMagazineID
            // 
            this.label_ULD_NGMagazineID.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_ULD_NGMagazineID.Location = new System.Drawing.Point(6, 18);
            this.label_ULD_NGMagazineID.Name = "label_ULD_NGMagazineID";
            this.label_ULD_NGMagazineID.Size = new System.Drawing.Size(269, 38);
            this.label_ULD_NGMagazineID.TabIndex = 28;
            this.label_ULD_NGMagazineID.Text = "Magazine ID : ";
            this.label_ULD_NGMagazineID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox_ULD_EmptyMagazineInfo
            // 
            this.groupBox_ULD_EmptyMagazineInfo.Controls.Add(this.label_ULD_EmptyMagazineBoatCount);
            this.groupBox_ULD_EmptyMagazineInfo.Controls.Add(this.label_ULD_EmptyMagazineID);
            this.groupBox_ULD_EmptyMagazineInfo.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox_ULD_EmptyMagazineInfo.Location = new System.Drawing.Point(870, 623);
            this.groupBox_ULD_EmptyMagazineInfo.Name = "groupBox_ULD_EmptyMagazineInfo";
            this.groupBox_ULD_EmptyMagazineInfo.Size = new System.Drawing.Size(281, 98);
            this.groupBox_ULD_EmptyMagazineInfo.TabIndex = 32;
            this.groupBox_ULD_EmptyMagazineInfo.TabStop = false;
            this.groupBox_ULD_EmptyMagazineInfo.Text = "EmptyMagazine Info";
            // 
            // label_ULD_EmptyMagazineBoatCount
            // 
            this.label_ULD_EmptyMagazineBoatCount.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_ULD_EmptyMagazineBoatCount.Location = new System.Drawing.Point(6, 56);
            this.label_ULD_EmptyMagazineBoatCount.Name = "label_ULD_EmptyMagazineBoatCount";
            this.label_ULD_EmptyMagazineBoatCount.Size = new System.Drawing.Size(269, 38);
            this.label_ULD_EmptyMagazineBoatCount.TabIndex = 29;
            this.label_ULD_EmptyMagazineBoatCount.Text = "Boat Count : ";
            this.label_ULD_EmptyMagazineBoatCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_ULD_EmptyMagazineID
            // 
            this.label_ULD_EmptyMagazineID.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_ULD_EmptyMagazineID.Location = new System.Drawing.Point(6, 18);
            this.label_ULD_EmptyMagazineID.Name = "label_ULD_EmptyMagazineID";
            this.label_ULD_EmptyMagazineID.Size = new System.Drawing.Size(269, 38);
            this.label_ULD_EmptyMagazineID.TabIndex = 28;
            this.label_ULD_EmptyMagazineID.Text = "Magazine ID : ";
            this.label_ULD_EmptyMagazineID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SecsgemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 729);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox_ULD_EmptyMagazineInfo);
            this.Controls.Add(this.groupBox_ULD_NGMagazineInfo);
            this.Controls.Add(this.groupBox_ULD_Pass2MagazineInfo);
            this.Controls.Add(this.groupBox_ULD_Pass1MagazineInfo);
            this.Controls.Add(this.groupBox_LD_Info);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.lbl_OpResult);
            this.Controls.Add(this.btnS1F1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SecsgemForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SECSGEM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.GroupBox9.ResumeLayout(false);
            this.GroupBox13.ResumeLayout(false);
            this.GroupBox12.ResumeLayout(false);
            this.GroupBox11.ResumeLayout(false);
            this.GroupBox10.ResumeLayout(false);
            this.GroupBox7.ResumeLayout(false);
            this.GroupBox8.ResumeLayout(false);
            this.GroupBox5.ResumeLayout(false);
            this.GroupBox6.ResumeLayout(false);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox4.ResumeLayout(false);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox2.ResumeLayout(false);
            this.frEQPStatus.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.State.ResumeLayout(false);
            this.DefaultSubState.ResumeLayout(false);
            this.SVDVEC.ResumeLayout(false);
            this.GroupBox14.ResumeLayout(false);
            this.GroupBox14.PerformLayout();
            this.GroupBox16.ResumeLayout(false);
            this.GroupBox16.PerformLayout();
            this.Alarm.ResumeLayout(false);
            this.GroupBox19.ResumeLayout(false);
            this.groupBox27.ResumeLayout(false);
            this.groupBox27.PerformLayout();
            this.GroupBox21.ResumeLayout(false);
            this.GroupBox21.PerformLayout();
            this.Event.ResumeLayout(false);
            this.GroupBox15.ResumeLayout(false);
            this.GroupBox18.ResumeLayout(false);
            this.GroupBox18.PerformLayout();
            this.Spooling.ResumeLayout(false);
            this.GroupBox24.ResumeLayout(false);
            this.GroupBox23.ResumeLayout(false);
            this.GroupBox25.ResumeLayout(false);
            this.Terminal.ResumeLayout(false);
            this.Terminal.PerformLayout();
            this.GroupBox22.ResumeLayout(false);
            this.PP.ResumeLayout(false);
            this.groupBox26.ResumeLayout(false);
            this.groupBox26.PerformLayout();
            this.FrameGuickGEM.ResumeLayout(false);
            this.FrameGuickGEM.PerformLayout();
            this.Remote.ResumeLayout(false);
            this.Remote.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox_LD_Info.ResumeLayout(false);
            this.groupBox_ULD_Pass1MagazineInfo.ResumeLayout(false);
            this.groupBox_ULD_Pass2MagazineInfo.ResumeLayout(false);
            this.groupBox_ULD_NGMagazineInfo.ResumeLayout(false);
            this.groupBox_ULD_EmptyMagazineInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnS1F1;
        internal System.Windows.Forms.GroupBox GroupBox9;
        internal System.Windows.Forms.GroupBox GroupBox13;
        internal System.Windows.Forms.RadioButton opt_OnLineLocal;
        internal System.Windows.Forms.RadioButton opt_OnLineRemote;
        internal System.Windows.Forms.GroupBox GroupBox12;
        internal System.Windows.Forms.RadioButton opt_FailToHostOffLine;
        internal System.Windows.Forms.RadioButton opt_FailToEqpOffLine;
        internal System.Windows.Forms.GroupBox GroupBox11;
        internal System.Windows.Forms.RadioButton opt_HostOffLine;
        internal System.Windows.Forms.RadioButton opt_AttemptOnLine;
        internal System.Windows.Forms.RadioButton opt_EqpOffLine;
        internal System.Windows.Forms.GroupBox GroupBox10;
        internal System.Windows.Forms.RadioButton opt_OffLine;
        internal System.Windows.Forms.RadioButton opt_OnLine;
        internal System.Windows.Forms.Button btn_OnlineLocal;
        internal System.Windows.Forms.Button btn_OnLineRemote;
        internal System.Windows.Forms.Button btn_Offline;
        internal System.Windows.Forms.Button btn_OnLine;
        internal System.Windows.Forms.Label lbl_CcontrolStats;
        internal System.Windows.Forms.GroupBox GroupBox7;
        internal System.Windows.Forms.GroupBox GroupBox8;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label lbl_SpoolingState;
        internal System.Windows.Forms.GroupBox GroupBox5;
        internal System.Windows.Forms.GroupBox GroupBox6;
        internal System.Windows.Forms.RadioButton opt_DisableComm;
        internal System.Windows.Forms.RadioButton opt_EnableComm;
        internal System.Windows.Forms.Button DisableComm;
        internal System.Windows.Forms.Button EnableComm;
        internal System.Windows.Forms.Label lbl_CommState;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.GroupBox GroupBox4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label lbl_SECSConnectState;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label lbl_ErrorCode;
        internal System.Windows.Forms.TextBox lbl_OpResult;
        internal System.Windows.Forms.GroupBox frEQPStatus;
        internal System.Windows.Forms.Label txtEQPStatus;
        internal System.Windows.Forms.Timer RefreshTimer;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage State;
        private System.Windows.Forms.TabPage DefaultSubState;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem SettingMenu;
        private System.Windows.Forms.ToolStripMenuItem Config;
        private System.Windows.Forms.ToolStripMenuItem Exit;
        private System.Windows.Forms.TabPage SVDVEC;
        internal System.Windows.Forms.GroupBox GroupBox16;
        internal System.Windows.Forms.Button UpdateEC;
        internal System.Windows.Forms.Button GetEC;
        internal System.Windows.Forms.Label txt_ECType;
        internal System.Windows.Forms.TextBox ECID;
        internal System.Windows.Forms.TextBox ECVal;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.GroupBox GroupBox14;
        internal System.Windows.Forms.TextBox SVValue;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Button GetSV;
        internal System.Windows.Forms.TextBox SVID;
        internal System.Windows.Forms.TextBox tbHostSendnewEC;
        internal System.Windows.Forms.Label label30;
        internal System.Windows.Forms.Label lbl_ECChanged;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label lbl_GemVer;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.Label lbl_GemSOFTREV;
        internal System.Windows.Forms.Label lbl_GemClock;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Label lbl_GemMDLN;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label5;
        private System.Windows.Forms.TabPage Alarm;
        internal System.Windows.Forms.GroupBox groupBox27;
        internal System.Windows.Forms.Label label20;
        internal System.Windows.Forms.TextBox tbAlarmSetSV;
        internal System.Windows.Forms.Button btnAlarmSet;
        internal System.Windows.Forms.Label label24;
        internal System.Windows.Forms.TextBox AlarmIDSet;
        internal System.Windows.Forms.GroupBox GroupBox21;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.TextBox AlarmCD;
        internal System.Windows.Forms.Button AlarmReportSend;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.TextBox AlarmID;
        internal System.Windows.Forms.GroupBox GroupBox19;
        internal System.Windows.Forms.RadioButton opt_S5WbitOff;
        internal System.Windows.Forms.RadioButton opt_S5WbitOn;
        private System.Windows.Forms.TabPage Event;
        internal System.Windows.Forms.GroupBox GroupBox18;
        internal System.Windows.Forms.Button SendEvent;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox EventID;
        internal System.Windows.Forms.GroupBox GroupBox15;
        internal System.Windows.Forms.RadioButton opt_S6WbitOff;
        internal System.Windows.Forms.RadioButton opt_S6WbitOn;
        private System.Windows.Forms.TabPage Spooling;
        internal System.Windows.Forms.GroupBox GroupBox25;
        internal System.Windows.Forms.Label lbl_SpoolActualCount;
        internal System.Windows.Forms.Label Label19;
        internal System.Windows.Forms.Label lbl_MaxSpoolTransmit;
        internal System.Windows.Forms.Label Label27;
        internal System.Windows.Forms.Label lbl_SpoolStartTime;
        internal System.Windows.Forms.Label Label25;
        internal System.Windows.Forms.Label lbl_SpoolFullTime;
        internal System.Windows.Forms.Label Label23;
        internal System.Windows.Forms.Label lbl_SpoolTotalCount;
        internal System.Windows.Forms.Label Label21;
        internal System.Windows.Forms.GroupBox GroupBox24;
        internal System.Windows.Forms.RadioButton opt_OverWriteSpoolDisable;
        internal System.Windows.Forms.RadioButton opt_OverWriteSpoolEnable;
        internal System.Windows.Forms.GroupBox GroupBox23;
        internal System.Windows.Forms.RadioButton opt_ConfigSpoolDisable;
        internal System.Windows.Forms.RadioButton opt_ConfigSpoolEnable;
        private System.Windows.Forms.TabPage Terminal;
        internal System.Windows.Forms.Button btn_MsgRecognitionEvent;
        internal System.Windows.Forms.Label label28;
        internal System.Windows.Forms.Label txt_MessageFromHost;
        internal System.Windows.Forms.TextBox txt_TerminalTextToHost;
        internal System.Windows.Forms.Button SendTerminalMsg;
        internal System.Windows.Forms.Label label16;
        internal System.Windows.Forms.GroupBox GroupBox22;
        internal System.Windows.Forms.RadioButton opt_S10WbitOff;
        internal System.Windows.Forms.RadioButton opt_S10WbitOn;
        private System.Windows.Forms.TabPage PP;
        internal System.Windows.Forms.GroupBox groupBox26;
        private System.Windows.Forms.Button Cmd_SendPP_Formatted;
        private System.Windows.Forms.Button Cmd_RequestPP_Formatted;
        private System.Windows.Forms.Button Cmd_RequestPP;
        private System.Windows.Forms.Button Cmd_SendPP;
        private System.Windows.Forms.Button Cmd_LoadInquire;
        internal System.Windows.Forms.TextBox txtPPID;
        internal System.Windows.Forms.GroupBox FrameGuickGEM;
        internal System.Windows.Forms.TextBox PPEventText;
        private System.Windows.Forms.TabPage Remote;
        internal System.Windows.Forms.Label lbCPVAL2;
        internal System.Windows.Forms.Label label31;
        internal System.Windows.Forms.Label lbCPNAME2;
        internal System.Windows.Forms.Label label33;
        internal System.Windows.Forms.Label lbCPVAL1;
        internal System.Windows.Forms.Label label29;
        internal System.Windows.Forms.Label lbCPNAME1;
        internal System.Windows.Forms.Label label26;
        internal System.Windows.Forms.Label lbRemoteCmd;
        internal System.Windows.Forms.Label label22;
        internal System.Windows.Forms.Label label36;
        internal System.Windows.Forms.Label label35;
        internal System.Windows.Forms.TextBox txCPACK2;
        internal System.Windows.Forms.TextBox txCPACK1;
        internal System.Windows.Forms.TextBox txHCACK;
        internal System.Windows.Forms.Label label34;
        internal System.Windows.Forms.Button SetSV;
        private System.Windows.Forms.GroupBox groupBox_LD_Info;
        internal System.Windows.Forms.Label label_RecipeName;
        internal System.Windows.Forms.Label label_LD_MagazineID;
        internal System.Windows.Forms.Label label_LotID;
        private System.Windows.Forms.GroupBox groupBox_ULD_Pass1MagazineInfo;
        internal System.Windows.Forms.Label label_ULD_Pass1MagazineBoatCount;
        internal System.Windows.Forms.Label label_ULD_Pass1MagazineID;
        private System.Windows.Forms.GroupBox groupBox_ULD_Pass2MagazineInfo;
        internal System.Windows.Forms.Label label_ULD_Pass2MagazineBoatCount;
        internal System.Windows.Forms.Label label_ULD_Pass2MagazineID;
        private System.Windows.Forms.GroupBox groupBox_ULD_NGMagazineInfo;
        internal System.Windows.Forms.Label label_ULD_NGMagazineBoatCount;
        internal System.Windows.Forms.Label label_ULD_NGMagazineID;
        private System.Windows.Forms.GroupBox groupBox_ULD_EmptyMagazineInfo;
        internal System.Windows.Forms.Label label_ULD_EmptyMagazineBoatCount;
        internal System.Windows.Forms.Label label_ULD_EmptyMagazineID;
        internal System.Windows.Forms.Label label_ULD_Pass1MagazineUnitCount;
        internal System.Windows.Forms.Label label_ULD_Pass2MagazineUnitCount;
        internal System.Windows.Forms.Label label_ULD_NGMagazineUnitCount;
    }
}

