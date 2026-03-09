namespace PT
{
    partial class SingleChannel
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RBCHRC = new System.Windows.Forms.RadioButton();
            this.BtDisCon = new System.Windows.Forms.Button();
            this.BtConnect = new System.Windows.Forms.Button();
            this.RBCHR2 = new System.Windows.Forms.RadioButton();
            this.RBCHRNomal = new System.Windows.Forms.RadioButton();
            this.TbConInfo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TBEncoderPos = new System.Windows.Forms.TextBox();
            this.BtEncoderPos = new System.Windows.Forms.Button();
            this.TBInterval = new System.Windows.Forms.TextBox();
            this.TBStopPos = new System.Windows.Forms.TextBox();
            this.TBStartPos = new System.Windows.Forms.TextBox();
            this.CBAxis = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CBTriggerOnReturn = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RBEncTrigger = new System.Windows.Forms.RadioButton();
            this.RBSyncSig = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TBSignal = new System.Windows.Forms.TextBox();
            this.TBSampleNo = new System.Windows.Forms.TextBox();
            this.TBLineNo = new System.Windows.Forms.TextBox();
            this.BtScan = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.TBSigMax = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.TBSigMin = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.CBDisplaySig = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.PPaint = new System.Windows.Forms.Panel();
            this.timerProcess = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbPLC = new System.Windows.Forms.TabPage();
            this.btnResetAlarm = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.tb_JogSpeed = new System.Windows.Forms.TextBox();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnSavePosSpd = new System.Windows.Forms.Button();
            this.tb_InputSpd = new System.Windows.Forms.TextBox();
            this.tb_InputPos = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cbSelectPoint = new System.Windows.Forms.ComboBox();
            this.btnJogN = new System.Windows.Forms.Button();
            this.btnJogP = new System.Windows.Forms.Button();
            this.btnGotoPoint = new System.Windows.Forms.Button();
            this.btnGotoP0 = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lb_DECNum = new System.Windows.Forms.Label();
            this.lb_SpdNum = new System.Windows.Forms.Label();
            this.lb_PosNum = new System.Windows.Forms.Label();
            this.lb_DriverErrorCode = new System.Windows.Forms.Label();
            this.lb_Speed = new System.Windows.Forms.Label();
            this.lb_Positon = new System.Windows.Forms.Label();
            this.lb_INP9 = new System.Windows.Forms.Label();
            this.lb_INP8 = new System.Windows.Forms.Label();
            this.lb_INP7 = new System.Windows.Forms.Label();
            this.lb_INP6 = new System.Windows.Forms.Label();
            this.lb_INP5 = new System.Windows.Forms.Label();
            this.lb_INP4 = new System.Windows.Forms.Label();
            this.lb_INP3 = new System.Windows.Forms.Label();
            this.lb_INP2 = new System.Windows.Forms.Label();
            this.lb_INP1 = new System.Windows.Forms.Label();
            this.lb_INP0 = new System.Windows.Forms.Label();
            this.lb_NJogging = new System.Windows.Forms.Label();
            this.lb_PJogging = new System.Windows.Forms.Label();
            this.lb_HomeFinish = new System.Windows.Forms.Label();
            this.lb_HomeGoing = new System.Windows.Forms.Label();
            this.lb_ServoBusy = new System.Windows.Forms.Label();
            this.lb_ServoReady = new System.Windows.Forms.Label();
            this.lb_PLimit = new System.Windows.Forms.Label();
            this.lb_NLimit = new System.Windows.Forms.Label();
            this.tbHTW = new System.Windows.Forms.TabPage();
            this.btnDarkReference = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel8 = new System.Windows.Forms.Panel();
            this.cbGetSpectrum = new System.Windows.Forms.CheckBox();
            this.RBFFT = new System.Windows.Forms.RadioButton();
            this.RBConfocal = new System.Windows.Forms.RadioButton();
            this.RBRaw = new System.Windows.Forms.RadioButton();
            this.label15 = new System.Windows.Forms.Label();
            this.timerProcess2 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbPLC.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tbHTW.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RBCHRC);
            this.panel1.Controls.Add(this.BtDisCon);
            this.panel1.Controls.Add(this.BtConnect);
            this.panel1.Controls.Add(this.RBCHR2);
            this.panel1.Controls.Add(this.RBCHRNomal);
            this.panel1.Controls.Add(this.TbConInfo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(535, 62);
            this.panel1.TabIndex = 17;
            // 
            // RBCHRC
            // 
            this.RBCHRC.AutoSize = true;
            this.RBCHRC.Checked = true;
            this.RBCHRC.Enabled = false;
            this.RBCHRC.Location = new System.Drawing.Point(243, 35);
            this.RBCHRC.Margin = new System.Windows.Forms.Padding(2);
            this.RBCHRC.Name = "RBCHRC";
            this.RBCHRC.Size = new System.Drawing.Size(58, 16);
            this.RBCHRC.TabIndex = 24;
            this.RBCHRC.TabStop = true;
            this.RBCHRC.Text = "CHR C";
            this.RBCHRC.UseVisualStyleBackColor = true;
            // 
            // BtDisCon
            // 
            this.BtDisCon.Enabled = false;
            this.BtDisCon.Location = new System.Drawing.Point(427, 31);
            this.BtDisCon.Margin = new System.Windows.Forms.Padding(2);
            this.BtDisCon.Name = "BtDisCon";
            this.BtDisCon.Size = new System.Drawing.Size(94, 26);
            this.BtDisCon.TabIndex = 23;
            this.BtDisCon.Text = "Disconnect";
            this.BtDisCon.UseVisualStyleBackColor = true;
            this.BtDisCon.Click += new System.EventHandler(this.BtConnect_Click);
            // 
            // BtConnect
            // 
            this.BtConnect.Location = new System.Drawing.Point(427, 4);
            this.BtConnect.Margin = new System.Windows.Forms.Padding(2);
            this.BtConnect.Name = "BtConnect";
            this.BtConnect.Size = new System.Drawing.Size(94, 25);
            this.BtConnect.TabIndex = 22;
            this.BtConnect.Text = "Connect";
            this.BtConnect.UseVisualStyleBackColor = true;
            this.BtConnect.Click += new System.EventHandler(this.BtConnect_Click);
            // 
            // RBCHR2
            // 
            this.RBCHR2.AutoSize = true;
            this.RBCHR2.Enabled = false;
            this.RBCHR2.Location = new System.Drawing.Point(151, 35);
            this.RBCHR2.Margin = new System.Windows.Forms.Padding(2);
            this.RBCHR2.Name = "RBCHR2";
            this.RBCHR2.Size = new System.Drawing.Size(86, 16);
            this.RBCHR2.TabIndex = 20;
            this.RBCHR2.Text = "CHR² Device";
            this.RBCHR2.UseVisualStyleBackColor = true;
            // 
            // RBCHRNomal
            // 
            this.RBCHRNomal.AutoSize = true;
            this.RBCHRNomal.Enabled = false;
            this.RBCHRNomal.Location = new System.Drawing.Point(11, 35);
            this.RBCHRNomal.Margin = new System.Windows.Forms.Padding(2);
            this.RBCHRNomal.Name = "RBCHRNomal";
            this.RBCHRNomal.Size = new System.Drawing.Size(132, 16);
            this.RBCHRNomal.TabIndex = 19;
            this.RBCHRNomal.Text = "First Generation Device";
            this.RBCHRNomal.UseVisualStyleBackColor = true;
            // 
            // TbConInfo
            // 
            this.TbConInfo.Location = new System.Drawing.Point(125, 7);
            this.TbConInfo.Margin = new System.Windows.Forms.Padding(2);
            this.TbConInfo.Name = "TbConInfo";
            this.TbConInfo.Size = new System.Drawing.Size(169, 22);
            this.TbConInfo.TabIndex = 18;
            this.TbConInfo.Text = "192.168.170.4";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "Connection Info";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.TBEncoderPos);
            this.panel2.Controls.Add(this.BtEncoderPos);
            this.panel2.Controls.Add(this.TBInterval);
            this.panel2.Controls.Add(this.TBStopPos);
            this.panel2.Controls.Add(this.TBStartPos);
            this.panel2.Controls.Add(this.CBAxis);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.CBTriggerOnReturn);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.RBEncTrigger);
            this.panel2.Controls.Add(this.RBSyncSig);
            this.panel2.Location = new System.Drawing.Point(0, 62);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(535, 122);
            this.panel2.TabIndex = 19;
            // 
            // TBEncoderPos
            // 
            this.TBEncoderPos.Location = new System.Drawing.Point(211, 97);
            this.TBEncoderPos.Name = "TBEncoderPos";
            this.TBEncoderPos.Size = new System.Drawing.Size(83, 22);
            this.TBEncoderPos.TabIndex = 32;
            this.TBEncoderPos.Text = "48030";
            // 
            // BtEncoderPos
            // 
            this.BtEncoderPos.Enabled = false;
            this.BtEncoderPos.Location = new System.Drawing.Point(22, 94);
            this.BtEncoderPos.Name = "BtEncoderPos";
            this.BtEncoderPos.Size = new System.Drawing.Size(178, 21);
            this.BtEncoderPos.TabIndex = 31;
            this.BtEncoderPos.Text = "Set Trigger Axis Encoder Position:";
            this.BtEncoderPos.UseVisualStyleBackColor = true;
            this.BtEncoderPos.Click += new System.EventHandler(this.BtEncoderPos_Click);
            // 
            // TBInterval
            // 
            this.TBInterval.Location = new System.Drawing.Point(440, 70);
            this.TBInterval.Name = "TBInterval";
            this.TBInterval.Size = new System.Drawing.Size(86, 22);
            this.TBInterval.TabIndex = 29;
            this.TBInterval.Text = "-10";
            this.TBInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBEncoderTriggerProperty_KeyPress);
            // 
            // TBStopPos
            // 
            this.TBStopPos.Location = new System.Drawing.Point(257, 70);
            this.TBStopPos.Name = "TBStopPos";
            this.TBStopPos.Size = new System.Drawing.Size(120, 22);
            this.TBStopPos.TabIndex = 28;
            this.TBStopPos.Text = "0";
            this.TBStopPos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBEncoderTriggerProperty_KeyPress);
            // 
            // TBStartPos
            // 
            this.TBStartPos.Location = new System.Drawing.Point(72, 70);
            this.TBStartPos.Name = "TBStartPos";
            this.TBStartPos.Size = new System.Drawing.Size(119, 22);
            this.TBStartPos.TabIndex = 27;
            this.TBStartPos.Text = "48010";
            this.TBStartPos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBEncoderTriggerProperty_KeyPress);
            // 
            // CBAxis
            // 
            this.CBAxis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBAxis.FormattingEnabled = true;
            this.CBAxis.Items.AddRange(new object[] {
            "X",
            "Y",
            "Z"});
            this.CBAxis.Location = new System.Drawing.Point(70, 49);
            this.CBAxis.Name = "CBAxis";
            this.CBAxis.Size = new System.Drawing.Size(121, 20);
            this.CBAxis.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "Axis:";
            // 
            // CBTriggerOnReturn
            // 
            this.CBTriggerOnReturn.AutoSize = true;
            this.CBTriggerOnReturn.Location = new System.Drawing.Point(211, 50);
            this.CBTriggerOnReturn.Name = "CBTriggerOnReturn";
            this.CBTriggerOnReturn.Size = new System.Drawing.Size(134, 16);
            this.CBTriggerOnReturn.TabIndex = 24;
            this.CBTriggerOnReturn.Text = "Trigger on return move";
            this.CBTriggerOnReturn.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(390, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "Interval:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(196, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "Stop Pos:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "Start Pos:";
            // 
            // RBEncTrigger
            // 
            this.RBEncTrigger.AutoSize = true;
            this.RBEncTrigger.Checked = true;
            this.RBEncTrigger.Location = new System.Drawing.Point(14, 27);
            this.RBEncTrigger.Name = "RBEncTrigger";
            this.RBEncTrigger.Size = new System.Drawing.Size(156, 16);
            this.RBEncTrigger.TabIndex = 20;
            this.RBEncTrigger.TabStop = true;
            this.RBEncTrigger.Text = "Trigger by Encoder Counter";
            this.RBEncTrigger.UseVisualStyleBackColor = true;
            this.RBEncTrigger.Click += new System.EventHandler(this.RBSyncSig_Click);
            // 
            // RBSyncSig
            // 
            this.RBSyncSig.AutoSize = true;
            this.RBSyncSig.Location = new System.Drawing.Point(14, 6);
            this.RBSyncSig.Name = "RBSyncSig";
            this.RBSyncSig.Size = new System.Drawing.Size(134, 16);
            this.RBSyncSig.TabIndex = 19;
            this.RBSyncSig.Text = "Trigger by Sync. Signal";
            this.RBSyncSig.UseVisualStyleBackColor = true;
            this.RBSyncSig.Click += new System.EventHandler(this.RBSyncSig_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.TBSignal);
            this.panel3.Controls.Add(this.TBSampleNo);
            this.panel3.Controls.Add(this.TBLineNo);
            this.panel3.Controls.Add(this.BtScan);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Location = new System.Drawing.Point(0, 184);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(535, 56);
            this.panel3.TabIndex = 21;
            // 
            // TBSignal
            // 
            this.TBSignal.Enabled = false;
            this.TBSignal.Location = new System.Drawing.Point(126, 30);
            this.TBSignal.Name = "TBSignal";
            this.TBSignal.Size = new System.Drawing.Size(177, 22);
            this.TBSignal.TabIndex = 34;
            this.TBSignal.Text = "256,264,272,257";
            this.TBSignal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBSignal_KeyPress);
            // 
            // TBSampleNo
            // 
            this.TBSampleNo.Enabled = false;
            this.TBSampleNo.Location = new System.Drawing.Point(327, 8);
            this.TBSampleNo.Name = "TBSampleNo";
            this.TBSampleNo.Size = new System.Drawing.Size(71, 22);
            this.TBSampleNo.TabIndex = 33;
            this.TBSampleNo.Text = "4801";
            // 
            // TBLineNo
            // 
            this.TBLineNo.Location = new System.Drawing.Point(125, 8);
            this.TBLineNo.Name = "TBLineNo";
            this.TBLineNo.Size = new System.Drawing.Size(64, 22);
            this.TBLineNo.TabIndex = 32;
            this.TBLineNo.Text = "1";
            // 
            // BtScan
            // 
            this.BtScan.Enabled = false;
            this.BtScan.Location = new System.Drawing.Point(404, 6);
            this.BtScan.Name = "BtScan";
            this.BtScan.Size = new System.Drawing.Size(117, 48);
            this.BtScan.TabIndex = 31;
            this.BtScan.Text = "Start Scan";
            this.BtScan.UseVisualStyleBackColor = true;
            this.BtScan.Click += new System.EventHandler(this.BtScan_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 12);
            this.label8.TabIndex = 23;
            this.label8.Text = "Device Output Signals:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(195, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "Sample Counter per Line:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "Number of Scan Linear:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.TBSigMax);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.TBSigMin);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.CBDisplaySig);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Location = new System.Drawing.Point(0, 240);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(535, 25);
            this.panel4.TabIndex = 22;
            // 
            // TBSigMax
            // 
            this.TBSigMax.Location = new System.Drawing.Point(416, 3);
            this.TBSigMax.Name = "TBSigMax";
            this.TBSigMax.Size = new System.Drawing.Size(107, 22);
            this.TBSigMax.TabIndex = 35;
            this.TBSigMax.Text = "4800";
            this.TBSigMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBSigMax_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(380, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 34;
            this.label11.Text = "Max:";
            // 
            // TBSigMin
            // 
            this.TBSigMin.Location = new System.Drawing.Point(257, 3);
            this.TBSigMin.Name = "TBSigMin";
            this.TBSigMin.Size = new System.Drawing.Size(107, 22);
            this.TBSigMin.TabIndex = 33;
            this.TBSigMin.Text = "0";
            this.TBSigMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBSigMax_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(220, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 12);
            this.label10.TabIndex = 28;
            this.label10.Text = "Min:";
            // 
            // CBDisplaySig
            // 
            this.CBDisplaySig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBDisplaySig.FormattingEnabled = true;
            this.CBDisplaySig.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.CBDisplaySig.Location = new System.Drawing.Point(90, 3);
            this.CBDisplaySig.Name = "CBDisplaySig";
            this.CBDisplaySig.Size = new System.Drawing.Size(112, 20);
            this.CBDisplaySig.TabIndex = 27;
            this.CBDisplaySig.SelectedIndexChanged += new System.EventHandler(this.CBDisplaySig_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 12);
            this.label9.TabIndex = 22;
            this.label9.Text = "Display Signal:";
            // 
            // PPaint
            // 
            this.PPaint.Location = new System.Drawing.Point(0, 265);
            this.PPaint.Name = "PPaint";
            this.PPaint.Size = new System.Drawing.Size(535, 316);
            this.PPaint.TabIndex = 23;
            this.PPaint.Paint += new System.Windows.Forms.PaintEventHandler(this.PPaint_Paint);
            // 
            // timerProcess
            // 
            this.timerProcess.Tick += new System.EventHandler(this.timerProcess_Tick);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(546, 531);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(113, 40);
            this.btnExit.TabIndex = 39;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbPLC);
            this.tabControl1.Controls.Add(this.tbHTW);
            this.tabControl1.Location = new System.Drawing.Point(542, 7);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(514, 518);
            this.tabControl1.TabIndex = 40;
            // 
            // tbPLC
            // 
            this.tbPLC.Controls.Add(this.btnResetAlarm);
            this.tbPLC.Controls.Add(this.label14);
            this.tbPLC.Controls.Add(this.tb_JogSpeed);
            this.tbPLC.Controls.Add(this.btnHome);
            this.tbPLC.Controls.Add(this.btnStop);
            this.tbPLC.Controls.Add(this.btnSavePosSpd);
            this.tbPLC.Controls.Add(this.tb_InputSpd);
            this.tbPLC.Controls.Add(this.tb_InputPos);
            this.tbPLC.Controls.Add(this.label12);
            this.tbPLC.Controls.Add(this.label13);
            this.tbPLC.Controls.Add(this.cbSelectPoint);
            this.tbPLC.Controls.Add(this.btnJogN);
            this.tbPLC.Controls.Add(this.btnJogP);
            this.tbPLC.Controls.Add(this.btnGotoPoint);
            this.tbPLC.Controls.Add(this.btnGotoP0);
            this.tbPLC.Controls.Add(this.panel5);
            this.tbPLC.Location = new System.Drawing.Point(4, 22);
            this.tbPLC.Name = "tbPLC";
            this.tbPLC.Padding = new System.Windows.Forms.Padding(3);
            this.tbPLC.Size = new System.Drawing.Size(506, 492);
            this.tbPLC.TabIndex = 0;
            this.tbPLC.Text = "PLC";
            this.tbPLC.UseVisualStyleBackColor = true;
            // 
            // btnResetAlarm
            // 
            this.btnResetAlarm.Location = new System.Drawing.Point(166, 394);
            this.btnResetAlarm.Name = "btnResetAlarm";
            this.btnResetAlarm.Size = new System.Drawing.Size(83, 37);
            this.btnResetAlarm.TabIndex = 56;
            this.btnResetAlarm.Text = "Reset Alarm";
            this.btnResetAlarm.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(395, 26);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 23);
            this.label14.TabIndex = 55;
            this.label14.Text = "Jog Speed(mm/s)";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_JogSpeed
            // 
            this.tb_JogSpeed.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_JogSpeed.Location = new System.Drawing.Point(405, 51);
            this.tb_JogSpeed.Name = "tb_JogSpeed";
            this.tb_JogSpeed.Size = new System.Drawing.Size(83, 30);
            this.tb_JogSpeed.TabIndex = 54;
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(405, 101);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(83, 37);
            this.btnHome.TabIndex = 53;
            this.btnHome.Text = "GoHome";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(284, 101);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(83, 37);
            this.btnStop.TabIndex = 52;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // btnSavePosSpd
            // 
            this.btnSavePosSpd.Location = new System.Drawing.Point(284, 394);
            this.btnSavePosSpd.Name = "btnSavePosSpd";
            this.btnSavePosSpd.Size = new System.Drawing.Size(161, 37);
            this.btnSavePosSpd.TabIndex = 51;
            this.btnSavePosSpd.Text = "Change Position and Speed";
            this.btnSavePosSpd.UseVisualStyleBackColor = true;
            // 
            // tb_InputSpd
            // 
            this.tb_InputSpd.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_InputSpd.Location = new System.Drawing.Point(346, 331);
            this.tb_InputSpd.Name = "tb_InputSpd";
            this.tb_InputSpd.Size = new System.Drawing.Size(132, 30);
            this.tb_InputSpd.TabIndex = 50;
            // 
            // tb_InputPos
            // 
            this.tb_InputPos.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tb_InputPos.Location = new System.Drawing.Point(346, 275);
            this.tb_InputPos.Name = "tb_InputPos";
            this.tb_InputPos.Size = new System.Drawing.Size(132, 30);
            this.tb_InputPos.TabIndex = 49;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(240, 334);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 23);
            this.label12.TabIndex = 48;
            this.label12.Text = "Speed(mm/s)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(240, 282);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(100, 23);
            this.label13.TabIndex = 47;
            this.label13.Text = "Position(mm)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbSelectPoint
            // 
            this.cbSelectPoint.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbSelectPoint.FormattingEnabled = true;
            this.cbSelectPoint.ItemHeight = 19;
            this.cbSelectPoint.Items.AddRange(new object[] {
            "P0",
            "P1",
            "P2",
            "P3",
            "P4",
            "P5",
            "P6",
            "P7",
            "P8",
            "P9"});
            this.cbSelectPoint.Location = new System.Drawing.Point(284, 211);
            this.cbSelectPoint.Name = "cbSelectPoint";
            this.cbSelectPoint.Size = new System.Drawing.Size(161, 27);
            this.cbSelectPoint.TabIndex = 46;
            // 
            // btnJogN
            // 
            this.btnJogN.Location = new System.Drawing.Point(284, 30);
            this.btnJogN.Name = "btnJogN";
            this.btnJogN.Size = new System.Drawing.Size(83, 37);
            this.btnJogN.TabIndex = 45;
            this.btnJogN.Text = "Jog - ";
            this.btnJogN.UseVisualStyleBackColor = true;
            // 
            // btnJogP
            // 
            this.btnJogP.Location = new System.Drawing.Point(166, 30);
            this.btnJogP.Name = "btnJogP";
            this.btnJogP.Size = new System.Drawing.Size(83, 37);
            this.btnJogP.TabIndex = 44;
            this.btnJogP.Text = "Jog + ";
            this.btnJogP.UseVisualStyleBackColor = true;
            // 
            // btnGotoPoint
            // 
            this.btnGotoPoint.Location = new System.Drawing.Point(166, 207);
            this.btnGotoPoint.Name = "btnGotoPoint";
            this.btnGotoPoint.Size = new System.Drawing.Size(83, 37);
            this.btnGotoPoint.TabIndex = 43;
            this.btnGotoPoint.Text = "GoToPoint";
            this.btnGotoPoint.UseVisualStyleBackColor = true;
            // 
            // btnGotoP0
            // 
            this.btnGotoP0.Location = new System.Drawing.Point(166, 101);
            this.btnGotoP0.Name = "btnGotoP0";
            this.btnGotoP0.Size = new System.Drawing.Size(83, 37);
            this.btnGotoP0.TabIndex = 42;
            this.btnGotoP0.Text = "GoToP0";
            this.btnGotoP0.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.AutoSize = true;
            this.panel5.Controls.Add(this.lb_DECNum);
            this.panel5.Controls.Add(this.lb_SpdNum);
            this.panel5.Controls.Add(this.lb_PosNum);
            this.panel5.Controls.Add(this.lb_DriverErrorCode);
            this.panel5.Controls.Add(this.lb_Speed);
            this.panel5.Controls.Add(this.lb_Positon);
            this.panel5.Controls.Add(this.lb_INP9);
            this.panel5.Controls.Add(this.lb_INP8);
            this.panel5.Controls.Add(this.lb_INP7);
            this.panel5.Controls.Add(this.lb_INP6);
            this.panel5.Controls.Add(this.lb_INP5);
            this.panel5.Controls.Add(this.lb_INP4);
            this.panel5.Controls.Add(this.lb_INP3);
            this.panel5.Controls.Add(this.lb_INP2);
            this.panel5.Controls.Add(this.lb_INP1);
            this.panel5.Controls.Add(this.lb_INP0);
            this.panel5.Controls.Add(this.lb_NJogging);
            this.panel5.Controls.Add(this.lb_PJogging);
            this.panel5.Controls.Add(this.lb_HomeFinish);
            this.panel5.Controls.Add(this.lb_HomeGoing);
            this.panel5.Controls.Add(this.lb_ServoBusy);
            this.panel5.Controls.Add(this.lb_ServoReady);
            this.panel5.Controls.Add(this.lb_PLimit);
            this.panel5.Controls.Add(this.lb_NLimit);
            this.panel5.Location = new System.Drawing.Point(6, 10);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(120, 472);
            this.panel5.TabIndex = 41;
            // 
            // lb_DECNum
            // 
            this.lb_DECNum.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_DECNum.Location = new System.Drawing.Point(8, 436);
            this.lb_DECNum.Name = "lb_DECNum";
            this.lb_DECNum.Size = new System.Drawing.Size(100, 23);
            this.lb_DECNum.TabIndex = 27;
            this.lb_DECNum.Text = "0";
            this.lb_DECNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_SpdNum
            // 
            this.lb_SpdNum.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_SpdNum.Location = new System.Drawing.Point(8, 384);
            this.lb_SpdNum.Name = "lb_SpdNum";
            this.lb_SpdNum.Size = new System.Drawing.Size(100, 23);
            this.lb_SpdNum.TabIndex = 26;
            this.lb_SpdNum.Text = "10.0000";
            this.lb_SpdNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_PosNum
            // 
            this.lb_PosNum.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_PosNum.Location = new System.Drawing.Point(8, 332);
            this.lb_PosNum.Name = "lb_PosNum";
            this.lb_PosNum.Size = new System.Drawing.Size(100, 23);
            this.lb_PosNum.TabIndex = 25;
            this.lb_PosNum.Text = "0";
            this.lb_PosNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_DriverErrorCode
            // 
            this.lb_DriverErrorCode.Location = new System.Drawing.Point(8, 410);
            this.lb_DriverErrorCode.Name = "lb_DriverErrorCode";
            this.lb_DriverErrorCode.Size = new System.Drawing.Size(100, 23);
            this.lb_DriverErrorCode.TabIndex = 20;
            this.lb_DriverErrorCode.Text = "DriverErrorCode";
            this.lb_DriverErrorCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_Speed
            // 
            this.lb_Speed.Location = new System.Drawing.Point(8, 358);
            this.lb_Speed.Name = "lb_Speed";
            this.lb_Speed.Size = new System.Drawing.Size(100, 23);
            this.lb_Speed.TabIndex = 19;
            this.lb_Speed.Text = "Speed(mm/s)";
            this.lb_Speed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_Positon
            // 
            this.lb_Positon.Location = new System.Drawing.Point(8, 306);
            this.lb_Positon.Name = "lb_Positon";
            this.lb_Positon.Size = new System.Drawing.Size(100, 23);
            this.lb_Positon.TabIndex = 18;
            this.lb_Positon.Text = "Position(mm)";
            this.lb_Positon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_INP9
            // 
            this.lb_INP9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_INP9.Location = new System.Drawing.Point(22, 288);
            this.lb_INP9.Name = "lb_INP9";
            this.lb_INP9.Size = new System.Drawing.Size(73, 16);
            this.lb_INP9.TabIndex = 17;
            this.lb_INP9.Text = "Point 9 INP";
            // 
            // lb_INP8
            // 
            this.lb_INP8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_INP8.Location = new System.Drawing.Point(22, 272);
            this.lb_INP8.Name = "lb_INP8";
            this.lb_INP8.Size = new System.Drawing.Size(73, 16);
            this.lb_INP8.TabIndex = 16;
            this.lb_INP8.Text = "Point 8 INP";
            // 
            // lb_INP7
            // 
            this.lb_INP7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_INP7.Location = new System.Drawing.Point(22, 256);
            this.lb_INP7.Name = "lb_INP7";
            this.lb_INP7.Size = new System.Drawing.Size(73, 16);
            this.lb_INP7.TabIndex = 15;
            this.lb_INP7.Text = "Point 7 INP";
            // 
            // lb_INP6
            // 
            this.lb_INP6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_INP6.Location = new System.Drawing.Point(22, 240);
            this.lb_INP6.Name = "lb_INP6";
            this.lb_INP6.Size = new System.Drawing.Size(73, 16);
            this.lb_INP6.TabIndex = 14;
            this.lb_INP6.Text = "Point 6 INP";
            // 
            // lb_INP5
            // 
            this.lb_INP5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_INP5.Location = new System.Drawing.Point(22, 224);
            this.lb_INP5.Name = "lb_INP5";
            this.lb_INP5.Size = new System.Drawing.Size(73, 16);
            this.lb_INP5.TabIndex = 13;
            this.lb_INP5.Text = "Point 5 INP";
            // 
            // lb_INP4
            // 
            this.lb_INP4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_INP4.Location = new System.Drawing.Point(22, 208);
            this.lb_INP4.Name = "lb_INP4";
            this.lb_INP4.Size = new System.Drawing.Size(73, 16);
            this.lb_INP4.TabIndex = 12;
            this.lb_INP4.Text = "Point 4 INP";
            // 
            // lb_INP3
            // 
            this.lb_INP3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_INP3.Location = new System.Drawing.Point(22, 192);
            this.lb_INP3.Name = "lb_INP3";
            this.lb_INP3.Size = new System.Drawing.Size(73, 16);
            this.lb_INP3.TabIndex = 11;
            this.lb_INP3.Text = "Point 3 INP";
            // 
            // lb_INP2
            // 
            this.lb_INP2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_INP2.Location = new System.Drawing.Point(22, 176);
            this.lb_INP2.Name = "lb_INP2";
            this.lb_INP2.Size = new System.Drawing.Size(73, 16);
            this.lb_INP2.TabIndex = 10;
            this.lb_INP2.Text = "Point 2 INP";
            // 
            // lb_INP1
            // 
            this.lb_INP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_INP1.Location = new System.Drawing.Point(22, 160);
            this.lb_INP1.Name = "lb_INP1";
            this.lb_INP1.Size = new System.Drawing.Size(73, 16);
            this.lb_INP1.TabIndex = 9;
            this.lb_INP1.Text = "Point 1 INP";
            // 
            // lb_INP0
            // 
            this.lb_INP0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_INP0.Location = new System.Drawing.Point(22, 144);
            this.lb_INP0.Name = "lb_INP0";
            this.lb_INP0.Size = new System.Drawing.Size(73, 16);
            this.lb_INP0.TabIndex = 8;
            this.lb_INP0.Text = "Point 0 INP";
            // 
            // lb_NJogging
            // 
            this.lb_NJogging.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_NJogging.Location = new System.Drawing.Point(22, 128);
            this.lb_NJogging.Name = "lb_NJogging";
            this.lb_NJogging.Size = new System.Drawing.Size(73, 16);
            this.lb_NJogging.TabIndex = 7;
            this.lb_NJogging.Text = "Jogging -";
            // 
            // lb_PJogging
            // 
            this.lb_PJogging.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_PJogging.Location = new System.Drawing.Point(22, 112);
            this.lb_PJogging.Name = "lb_PJogging";
            this.lb_PJogging.Size = new System.Drawing.Size(73, 16);
            this.lb_PJogging.TabIndex = 6;
            this.lb_PJogging.Text = "Jogging +";
            // 
            // lb_HomeFinish
            // 
            this.lb_HomeFinish.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_HomeFinish.Location = new System.Drawing.Point(22, 96);
            this.lb_HomeFinish.Name = "lb_HomeFinish";
            this.lb_HomeFinish.Size = new System.Drawing.Size(73, 16);
            this.lb_HomeFinish.TabIndex = 5;
            this.lb_HomeFinish.Text = "HomeFinish";
            // 
            // lb_HomeGoing
            // 
            this.lb_HomeGoing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_HomeGoing.Location = new System.Drawing.Point(22, 80);
            this.lb_HomeGoing.Name = "lb_HomeGoing";
            this.lb_HomeGoing.Size = new System.Drawing.Size(73, 16);
            this.lb_HomeGoing.TabIndex = 4;
            this.lb_HomeGoing.Text = "HomeGoing";
            // 
            // lb_ServoBusy
            // 
            this.lb_ServoBusy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_ServoBusy.Location = new System.Drawing.Point(22, 64);
            this.lb_ServoBusy.Name = "lb_ServoBusy";
            this.lb_ServoBusy.Size = new System.Drawing.Size(73, 16);
            this.lb_ServoBusy.TabIndex = 3;
            this.lb_ServoBusy.Text = "ServoBusy";
            // 
            // lb_ServoReady
            // 
            this.lb_ServoReady.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_ServoReady.Location = new System.Drawing.Point(22, 48);
            this.lb_ServoReady.Name = "lb_ServoReady";
            this.lb_ServoReady.Size = new System.Drawing.Size(73, 16);
            this.lb_ServoReady.TabIndex = 2;
            this.lb_ServoReady.Text = "ServoReady";
            // 
            // lb_PLimit
            // 
            this.lb_PLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_PLimit.Location = new System.Drawing.Point(22, 32);
            this.lb_PLimit.Name = "lb_PLimit";
            this.lb_PLimit.Size = new System.Drawing.Size(73, 16);
            this.lb_PLimit.TabIndex = 1;
            this.lb_PLimit.Text = "Limit +";
            // 
            // lb_NLimit
            // 
            this.lb_NLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_NLimit.Location = new System.Drawing.Point(22, 16);
            this.lb_NLimit.Name = "lb_NLimit";
            this.lb_NLimit.Size = new System.Drawing.Size(73, 16);
            this.lb_NLimit.TabIndex = 0;
            this.lb_NLimit.Text = "Limit -";
            // 
            // tbHTW
            // 
            this.tbHTW.Controls.Add(this.btnDarkReference);
            this.tbHTW.Controls.Add(this.panel6);
            this.tbHTW.Location = new System.Drawing.Point(4, 22);
            this.tbHTW.Name = "tbHTW";
            this.tbHTW.Padding = new System.Windows.Forms.Padding(3);
            this.tbHTW.Size = new System.Drawing.Size(506, 492);
            this.tbHTW.TabIndex = 1;
            this.tbHTW.Text = "HTW Spectrum";
            this.tbHTW.UseVisualStyleBackColor = true;
            // 
            // btnDarkReference
            // 
            this.btnDarkReference.Location = new System.Drawing.Point(399, 8);
            this.btnDarkReference.Name = "btnDarkReference";
            this.btnDarkReference.Size = new System.Drawing.Size(101, 24);
            this.btnDarkReference.TabIndex = 42;
            this.btnDarkReference.Text = "DarkReference";
            this.btnDarkReference.UseVisualStyleBackColor = true;
            this.btnDarkReference.Click += new System.EventHandler(this.btnDarkReference_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.chart1);
            this.panel6.Controls.Add(this.panel8);
            this.panel6.Location = new System.Drawing.Point(0, 37);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(505, 454);
            this.panel6.TabIndex = 0;
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Black;
            this.chart1.BorderlineColor = System.Drawing.Color.Yellow;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisX.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.MajorTickMark.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisX.MinorTickMark.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.MinorTickMark.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisX.ScaleBreakStyle.LineColor = System.Drawing.Color.Yellow;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.MajorGrid.Interval = 0D;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.MajorTickMark.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.MinorTickMark.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.MinorTickMark.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.White;
            chartArea1.BackColor = System.Drawing.Color.Black;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 24);
            this.chart1.Margin = new System.Windows.Forms.Padding(0);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Yellow;
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.MarkerColor = System.Drawing.Color.Red;
            series1.MarkerSize = 2;
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(505, 430);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.cbGetSpectrum);
            this.panel8.Controls.Add(this.RBFFT);
            this.panel8.Controls.Add(this.RBConfocal);
            this.panel8.Controls.Add(this.RBRaw);
            this.panel8.Controls.Add(this.label15);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Margin = new System.Windows.Forms.Padding(2);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(505, 24);
            this.panel8.TabIndex = 2;
            // 
            // cbGetSpectrum
            // 
            this.cbGetSpectrum.AutoSize = true;
            this.cbGetSpectrum.Location = new System.Drawing.Point(336, 5);
            this.cbGetSpectrum.Name = "cbGetSpectrum";
            this.cbGetSpectrum.Size = new System.Drawing.Size(87, 16);
            this.cbGetSpectrum.TabIndex = 25;
            this.cbGetSpectrum.Text = "Get Spectrum";
            this.cbGetSpectrum.UseVisualStyleBackColor = true;
            this.cbGetSpectrum.CheckedChanged += new System.EventHandler(this.cbGetSpectrum_CheckedChanged);
            // 
            // RBFFT
            // 
            this.RBFFT.AutoSize = true;
            this.RBFFT.Location = new System.Drawing.Point(256, 4);
            this.RBFFT.Margin = new System.Windows.Forms.Padding(2);
            this.RBFFT.Name = "RBFFT";
            this.RBFFT.Size = new System.Drawing.Size(42, 16);
            this.RBFFT.TabIndex = 3;
            this.RBFFT.Text = "FFT";
            this.RBFFT.UseVisualStyleBackColor = true;
            // 
            // RBConfocal
            // 
            this.RBConfocal.AutoSize = true;
            this.RBConfocal.Location = new System.Drawing.Point(169, 4);
            this.RBConfocal.Margin = new System.Windows.Forms.Padding(2);
            this.RBConfocal.Name = "RBConfocal";
            this.RBConfocal.Size = new System.Drawing.Size(66, 16);
            this.RBConfocal.TabIndex = 2;
            this.RBConfocal.Text = "Confocal";
            this.RBConfocal.UseVisualStyleBackColor = true;
            // 
            // RBRaw
            // 
            this.RBRaw.AutoSize = true;
            this.RBRaw.Checked = true;
            this.RBRaw.Location = new System.Drawing.Point(105, 4);
            this.RBRaw.Margin = new System.Windows.Forms.Padding(2);
            this.RBRaw.Name = "RBRaw";
            this.RBRaw.Size = new System.Drawing.Size(44, 16);
            this.RBRaw.TabIndex = 1;
            this.RBRaw.TabStop = true;
            this.RBRaw.Text = "Raw";
            this.RBRaw.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 6);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(79, 12);
            this.label15.TabIndex = 0;
            this.label15.Text = "Spectrum Type:";
            // 
            // timerProcess2
            // 
            this.timerProcess2.Tick += new System.EventHandler(this.timerProcess2_Tick);
            // 
            // SingleChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 581);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.PPaint);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SingleChannel";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.SingleChannel_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tbPLC.ResumeLayout(false);
            this.tbPLC.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.tbHTW.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton RBCHRC;
        private System.Windows.Forms.Button BtDisCon;
        private System.Windows.Forms.Button BtConnect;
        private System.Windows.Forms.RadioButton RBCHR2;
        private System.Windows.Forms.RadioButton RBCHRNomal;
        private System.Windows.Forms.TextBox TbConInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton RBEncTrigger;
        private System.Windows.Forms.RadioButton RBSyncSig;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox CBTriggerOnReturn;
        private System.Windows.Forms.TextBox TBInterval;
        private System.Windows.Forms.TextBox TBStopPos;
        private System.Windows.Forms.TextBox TBStartPos;
        private System.Windows.Forms.ComboBox CBAxis;
        private System.Windows.Forms.TextBox TBEncoderPos;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox TBSignal;
        private System.Windows.Forms.TextBox TBSampleNo;
        private System.Windows.Forms.TextBox TBLineNo;
        private System.Windows.Forms.Button BtScan;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox CBDisplaySig;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel PPaint;
        private System.Windows.Forms.Timer timerProcess;
        private System.Windows.Forms.TextBox TBSigMax;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TBSigMin;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button BtEncoderPos;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbPLC;
        private System.Windows.Forms.Button btnResetAlarm;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tb_JogSpeed;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnSavePosSpd;
        private System.Windows.Forms.TextBox tb_InputSpd;
        private System.Windows.Forms.TextBox tb_InputPos;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbSelectPoint;
        private System.Windows.Forms.Button btnJogN;
        private System.Windows.Forms.Button btnJogP;
        private System.Windows.Forms.Button btnGotoPoint;
        private System.Windows.Forms.Button btnGotoP0;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lb_DECNum;
        private System.Windows.Forms.Label lb_SpdNum;
        private System.Windows.Forms.Label lb_PosNum;
        private System.Windows.Forms.Label lb_DriverErrorCode;
        private System.Windows.Forms.Label lb_Speed;
        private System.Windows.Forms.Label lb_Positon;
        private System.Windows.Forms.Label lb_INP9;
        private System.Windows.Forms.Label lb_INP8;
        private System.Windows.Forms.Label lb_INP7;
        private System.Windows.Forms.Label lb_INP6;
        private System.Windows.Forms.Label lb_INP5;
        private System.Windows.Forms.Label lb_INP4;
        private System.Windows.Forms.Label lb_INP3;
        private System.Windows.Forms.Label lb_INP2;
        private System.Windows.Forms.Label lb_INP1;
        private System.Windows.Forms.Label lb_INP0;
        private System.Windows.Forms.Label lb_NJogging;
        private System.Windows.Forms.Label lb_PJogging;
        private System.Windows.Forms.Label lb_HomeFinish;
        private System.Windows.Forms.Label lb_HomeGoing;
        private System.Windows.Forms.Label lb_ServoBusy;
        private System.Windows.Forms.Label lb_ServoReady;
        private System.Windows.Forms.Label lb_PLimit;
        private System.Windows.Forms.Label lb_NLimit;
        private System.Windows.Forms.TabPage tbHTW;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.RadioButton RBFFT;
        private System.Windows.Forms.RadioButton RBConfocal;
        private System.Windows.Forms.RadioButton RBRaw;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox cbGetSpectrum;
        private System.Windows.Forms.Button btnDarkReference;
        private System.Windows.Forms.Timer timerProcess2;
    }
}

