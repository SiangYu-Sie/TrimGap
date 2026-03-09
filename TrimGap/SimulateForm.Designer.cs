namespace TrimGap
{
    partial class SimulateForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbLoadPort = new System.Windows.Forms.Label();
            this.lbCarrierID = new System.Windows.Forms.Label();
            this.lbSlotCount = new System.Windows.Forms.Label();
            this.lbMeasureDelay = new System.Windows.Forms.Label();
            this.cmbLoadPort = new System.Windows.Forms.ComboBox();
            this.txtCarrierID = new System.Windows.Forms.TextBox();
            this.nudSlotCount = new System.Windows.Forms.NumericUpDown();
            this.nudMeasureDelay = new System.Windows.Forms.NumericUpDown();
            this.lbSlotHint = new System.Windows.Forms.Label();
            this.lbDelayHint = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.pnlProgress = new System.Windows.Forms.Panel();
            this.lbStepTitle = new System.Windows.Forms.Label();
            this.pgBar = new System.Windows.Forms.ProgressBar();
            this.lbStep0 = new System.Windows.Forms.Label();
            this.lbStep1 = new System.Windows.Forms.Label();
            this.lbStep2 = new System.Windows.Forms.Label();
            this.lbStep3 = new System.Windows.Forms.Label();
            this.lbStep4 = new System.Windows.Forms.Label();
            this.lbStep5 = new System.Windows.Forms.Label();
            this.lbStep6 = new System.Windows.Forms.Label();
            this.lbStep7 = new System.Windows.Forms.Label();
            this.lbStep8 = new System.Windows.Forms.Label();
            this.lbStep9 = new System.Windows.Forms.Label();
            this.lbStep10 = new System.Windows.Forms.Label();
            this.lbStep11 = new System.Windows.Forms.Label();
            this.lbStep12 = new System.Windows.Forms.Label();
            this.lbStepDone = new System.Windows.Forms.Label();
            this.lbCurrentInfo = new System.Windows.Forms.Label();
            this.pnlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlotCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMeasureDelay)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.pnlProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft JhengHei UI", 13F, System.Drawing.FontStyle.Bold);
            this.lbTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(255)))));
            this.lbTitle.Location = new System.Drawing.Point(16, 12);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(295, 25);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "模擬量測流程 (SECS 通訊測試)";
            // 
            // pnlSettings
            // 
            this.pnlSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlSettings.Controls.Add(this.lbLoadPort);
            this.pnlSettings.Controls.Add(this.cmbLoadPort);
            this.pnlSettings.Controls.Add(this.lbCarrierID);
            this.pnlSettings.Controls.Add(this.txtCarrierID);
            this.pnlSettings.Controls.Add(this.lbSlotCount);
            this.pnlSettings.Controls.Add(this.nudSlotCount);
            this.pnlSettings.Controls.Add(this.lbSlotHint);
            this.pnlSettings.Controls.Add(this.lbMeasureDelay);
            this.pnlSettings.Controls.Add(this.nudMeasureDelay);
            this.pnlSettings.Controls.Add(this.lbDelayHint);
            this.pnlSettings.Location = new System.Drawing.Point(12, 44);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Padding = new System.Windows.Forms.Padding(8);
            this.pnlSettings.Size = new System.Drawing.Size(480, 140);
            this.pnlSettings.TabIndex = 1;
            // 
            // lbLoadPort
            // 
            this.lbLoadPort.AutoSize = true;
            this.lbLoadPort.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.lbLoadPort.Location = new System.Drawing.Point(10, 12);
            this.lbLoadPort.Name = "lbLoadPort";
            this.lbLoadPort.Size = new System.Drawing.Size(72, 16);
            this.lbLoadPort.TabIndex = 0;
            this.lbLoadPort.Text = "LoadPort：";
            // 
            // cmbLoadPort
            // 
            this.cmbLoadPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.cmbLoadPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoadPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbLoadPort.ForeColor = System.Drawing.Color.White;
            this.cmbLoadPort.Items.AddRange(new object[] {
            "LoadPort 1",
            "LoadPort 2"});
            this.cmbLoadPort.Location = new System.Drawing.Point(130, 8);
            this.cmbLoadPort.Name = "cmbLoadPort";
            this.cmbLoadPort.Size = new System.Drawing.Size(150, 24);
            this.cmbLoadPort.TabIndex = 1;
            // 
            // lbCarrierID
            // 
            this.lbCarrierID.AutoSize = true;
            this.lbCarrierID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.lbCarrierID.Location = new System.Drawing.Point(10, 44);
            this.lbCarrierID.Name = "lbCarrierID";
            this.lbCarrierID.Size = new System.Drawing.Size(80, 16);
            this.lbCarrierID.TabIndex = 2;
            this.lbCarrierID.Text = "Carrier ID：";
            // 
            // txtCarrierID
            // 
            this.txtCarrierID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtCarrierID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCarrierID.ForeColor = System.Drawing.Color.White;
            this.txtCarrierID.Location = new System.Drawing.Point(130, 40);
            this.txtCarrierID.Name = "txtCarrierID";
            this.txtCarrierID.Size = new System.Drawing.Size(320, 23);
            this.txtCarrierID.TabIndex = 3;
            this.txtCarrierID.Text = "SIM_CARRIER_001";
            // 
            // lbSlotCount
            // 
            this.lbSlotCount.AutoSize = true;
            this.lbSlotCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.lbSlotCount.Location = new System.Drawing.Point(10, 76);
            this.lbSlotCount.Name = "lbSlotCount";
            this.lbSlotCount.Size = new System.Drawing.Size(88, 16);
            this.lbSlotCount.TabIndex = 4;
            this.lbSlotCount.Text = "Wafer 片數：";
            // 
            // nudSlotCount
            // 
            this.nudSlotCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.nudSlotCount.ForeColor = System.Drawing.Color.White;
            this.nudSlotCount.Location = new System.Drawing.Point(130, 72);
            this.nudSlotCount.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.nudSlotCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSlotCount.Name = "nudSlotCount";
            this.nudSlotCount.Size = new System.Drawing.Size(80, 23);
            this.nudSlotCount.TabIndex = 5;
            this.nudSlotCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lbSlotHint
            // 
            this.lbSlotHint.AutoSize = true;
            this.lbSlotHint.ForeColor = System.Drawing.Color.Gray;
            this.lbSlotHint.Location = new System.Drawing.Point(218, 76);
            this.lbSlotHint.Name = "lbSlotHint";
            this.lbSlotHint.Size = new System.Drawing.Size(42, 16);
            this.lbSlotHint.TabIndex = 6;
            this.lbSlotHint.Text = "(1~25)";
            // 
            // lbMeasureDelay
            // 
            this.lbMeasureDelay.AutoSize = true;
            this.lbMeasureDelay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.lbMeasureDelay.Location = new System.Drawing.Point(10, 108);
            this.lbMeasureDelay.Name = "lbMeasureDelay";
            this.lbMeasureDelay.Size = new System.Drawing.Size(104, 16);
            this.lbMeasureDelay.TabIndex = 7;
            this.lbMeasureDelay.Text = "量測耗時(ms)：";
            // 
            // nudMeasureDelay
            // 
            this.nudMeasureDelay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.nudMeasureDelay.ForeColor = System.Drawing.Color.White;
            this.nudMeasureDelay.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudMeasureDelay.Location = new System.Drawing.Point(130, 104);
            this.nudMeasureDelay.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nudMeasureDelay.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudMeasureDelay.Name = "nudMeasureDelay";
            this.nudMeasureDelay.Size = new System.Drawing.Size(100, 23);
            this.nudMeasureDelay.TabIndex = 8;
            this.nudMeasureDelay.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // lbDelayHint
            // 
            this.lbDelayHint.AutoSize = true;
            this.lbDelayHint.ForeColor = System.Drawing.Color.Gray;
            this.lbDelayHint.Location = new System.Drawing.Point(238, 108);
            this.lbDelayHint.Name = "lbDelayHint";
            this.lbDelayHint.Size = new System.Drawing.Size(62, 16);
            this.lbDelayHint.TabIndex = 9;
            this.lbDelayHint.Text = "ms / 每片";
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnStart);
            this.pnlButtons.Location = new System.Drawing.Point(12, 190);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(480, 42);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Microsoft JhengHei UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(0, 0);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(480, 38);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "▶ 開始模擬";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // pnlProgress
            // 
            this.pnlProgress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlProgress.Controls.Add(this.lbStepTitle);
            this.pnlProgress.Controls.Add(this.pgBar);
            this.pnlProgress.Controls.Add(this.lbStep0);
            this.pnlProgress.Controls.Add(this.lbStep1);
            this.pnlProgress.Controls.Add(this.lbStep2);
            this.pnlProgress.Controls.Add(this.lbStep3);
            this.pnlProgress.Controls.Add(this.lbStep4);
            this.pnlProgress.Controls.Add(this.lbStep5);
            this.pnlProgress.Controls.Add(this.lbStep6);
            this.pnlProgress.Controls.Add(this.lbStep7);
            this.pnlProgress.Controls.Add(this.lbStep8);
            this.pnlProgress.Controls.Add(this.lbStep9);
            this.pnlProgress.Controls.Add(this.lbStep10);
            this.pnlProgress.Controls.Add(this.lbStep11);
            this.pnlProgress.Controls.Add(this.lbStep12);
            this.pnlProgress.Controls.Add(this.lbStepDone);
            this.pnlProgress.Controls.Add(this.lbCurrentInfo);
            this.pnlProgress.Location = new System.Drawing.Point(12, 240);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Padding = new System.Windows.Forms.Padding(8);
            this.pnlProgress.Size = new System.Drawing.Size(480, 347);
            this.pnlProgress.TabIndex = 3;
            // 
            // lbStepTitle
            // 
            this.lbStepTitle.AutoSize = true;
            this.lbStepTitle.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lbStepTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(220)))), ((int)(((byte)(255)))));
            this.lbStepTitle.Location = new System.Drawing.Point(10, 8);
            this.lbStepTitle.Name = "lbStepTitle";
            this.lbStepTitle.Size = new System.Drawing.Size(72, 19);
            this.lbStepTitle.TabIndex = 0;
            this.lbStepTitle.Text = "流程進度";
            // 
            // pgBar
            // 
            this.pgBar.Location = new System.Drawing.Point(10, 32);
            this.pgBar.Maximum = 13;
            this.pgBar.Name = "pgBar";
            this.pgBar.Size = new System.Drawing.Size(456, 14);
            this.pgBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgBar.TabIndex = 1;
            // 
            // lbStep0
            // 
            this.lbStep0.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.lbStep0.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lbStep0.Location = new System.Drawing.Point(10, 54);
            this.lbStep0.Name = "lbStep0";
            this.lbStep0.Size = new System.Drawing.Size(456, 19);
            this.lbStep0.TabIndex = 2;
            this.lbStep0.Text = "  ○ Step 0: 初始化 (ReadyToLoad)";
            // 
            // lbStep1
            // 
            this.lbStep1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.lbStep1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lbStep1.Location = new System.Drawing.Point(10, 73);
            this.lbStep1.Name = "lbStep1";
            this.lbStep1.Size = new System.Drawing.Size(456, 19);
            this.lbStep1.TabIndex = 3;
            this.lbStep1.Text = "  ○ Step 1: Carrier 放上 LoadPort";
            // 
            // lbStep2
            // 
            this.lbStep2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.lbStep2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lbStep2.Location = new System.Drawing.Point(10, 92);
            this.lbStep2.Name = "lbStep2";
            this.lbStep2.Size = new System.Drawing.Size(456, 19);
            this.lbStep2.TabIndex = 4;
            this.lbStep2.Text = "  ○ Step 2: Clamp + CreateCarrier + ReadID";
            // 
            // lbStep3
            // 
            this.lbStep3.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.lbStep3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lbStep3.Location = new System.Drawing.Point(10, 111);
            this.lbStep3.Name = "lbStep3";
            this.lbStep3.Size = new System.Drawing.Size(456, 19);
            this.lbStep3.TabIndex = 5;
            this.lbStep3.Text = "  ○ Step 3: 等待 ProceedWithCarrier #1";
            // 
            // lbStep4
            // 
            this.lbStep4.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.lbStep4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lbStep4.Location = new System.Drawing.Point(10, 130);
            this.lbStep4.Name = "lbStep4";
            this.lbStep4.Size = new System.Drawing.Size(456, 19);
            this.lbStep4.TabIndex = 6;
            this.lbStep4.Text = "  ○ Step 4: FoupLoad (Dock/DoorOpen)";
            // 
            // lbStep5
            // 
            this.lbStep5.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.lbStep5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lbStep5.Location = new System.Drawing.Point(10, 149);
            this.lbStep5.Name = "lbStep5";
            this.lbStep5.Size = new System.Drawing.Size(456, 19);
            this.lbStep5.TabIndex = 7;
            this.lbStep5.Text = "  ○ Step 5: SlotMap";
            // 
            // lbStep6
            // 
            this.lbStep6.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.lbStep6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lbStep6.Location = new System.Drawing.Point(10, 168);
            this.lbStep6.Name = "lbStep6";
            this.lbStep6.Size = new System.Drawing.Size(456, 19);
            this.lbStep6.TabIndex = 8;
            this.lbStep6.Text = "  ○ Step 6: 等待 ProceedWithCarrier #2";
            // 
            // lbStep7
            // 
            this.lbStep7.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.lbStep7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lbStep7.Location = new System.Drawing.Point(10, 187);
            this.lbStep7.Name = "lbStep7";
            this.lbStep7.Size = new System.Drawing.Size(456, 19);
            this.lbStep7.TabIndex = 9;
            this.lbStep7.Text = "  ○ Step 7: 等待 CJ/PJ 建立";
            // 
            // lbStep8
            // 
            this.lbStep8.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.lbStep8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lbStep8.Location = new System.Drawing.Point(10, 206);
            this.lbStep8.Name = "lbStep8";
            this.lbStep8.Size = new System.Drawing.Size(456, 19);
            this.lbStep8.TabIndex = 10;
            this.lbStep8.Text = "  ○ Step 8: CJ/PJ Start";
            // 
            // lbStep9
            // 
            this.lbStep9.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.lbStep9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lbStep9.Location = new System.Drawing.Point(10, 225);
            this.lbStep9.Name = "lbStep9";
            this.lbStep9.Size = new System.Drawing.Size(456, 19);
            this.lbStep9.TabIndex = 11;
            this.lbStep9.Text = "  ○ Step 9: 逐片量測中...";
            // 
            // lbStep10
            // 
            this.lbStep10.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.lbStep10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lbStep10.Location = new System.Drawing.Point(10, 244);
            this.lbStep10.Name = "lbStep10";
            this.lbStep10.Size = new System.Drawing.Size(456, 19);
            this.lbStep10.TabIndex = 12;
            this.lbStep10.Text = "  ○ Step 10: PJ/CJ Complete";
            // 
            // lbStep11
            // 
            this.lbStep11.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.lbStep11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lbStep11.Location = new System.Drawing.Point(10, 263);
            this.lbStep11.Name = "lbStep11";
            this.lbStep11.Size = new System.Drawing.Size(456, 19);
            this.lbStep11.TabIndex = 13;
            this.lbStep11.Text = "  ○ Step 11: FoupUnLoad (DoorClose)";
            // 
            // lbStep12
            // 
            this.lbStep12.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.lbStep12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lbStep12.Location = new System.Drawing.Point(10, 282);
            this.lbStep12.Name = "lbStep12";
            this.lbStep12.Size = new System.Drawing.Size(456, 19);
            this.lbStep12.TabIndex = 14;
            this.lbStep12.Text = "  ○ Step 12: Carrier 拿走 (MaterialRemove)";
            // 
            // lbStepDone
            // 
            this.lbStepDone.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.lbStepDone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lbStepDone.Location = new System.Drawing.Point(10, 301);
            this.lbStepDone.Name = "lbStepDone";
            this.lbStepDone.Size = new System.Drawing.Size(456, 19);
            this.lbStepDone.TabIndex = 15;
            this.lbStepDone.Text = "  ○ 完成";
            // 
            // lbCurrentInfo
            // 
            this.lbCurrentInfo.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lbCurrentInfo.ForeColor = System.Drawing.Color.Gray;
            this.lbCurrentInfo.Location = new System.Drawing.Point(10, 326);
            this.lbCurrentInfo.Name = "lbCurrentInfo";
            this.lbCurrentInfo.Size = new System.Drawing.Size(456, 22);
            this.lbCurrentInfo.TabIndex = 16;
            this.lbCurrentInfo.Text = "等待啟動...";
            // 
            // SimulateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(504, 598);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.pnlSettings);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlProgress);
            this.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(480, 597);
            this.Name = "SimulateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模擬量測流程 控制面板";
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlotCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMeasureDelay)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.pnlProgress.ResumeLayout(false);
            this.pnlProgress.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSettings;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbLoadPort;
        private System.Windows.Forms.Label lbCarrierID;
        private System.Windows.Forms.Label lbSlotCount;
        private System.Windows.Forms.Label lbMeasureDelay;
        private System.Windows.Forms.ComboBox cmbLoadPort;
        private System.Windows.Forms.TextBox txtCarrierID;
        private System.Windows.Forms.NumericUpDown nudSlotCount;
        private System.Windows.Forms.NumericUpDown nudMeasureDelay;
        private System.Windows.Forms.Label lbSlotHint;
        private System.Windows.Forms.Label lbDelayHint;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel pnlProgress;
        private System.Windows.Forms.Label lbStepTitle;
        private System.Windows.Forms.ProgressBar pgBar;
        private System.Windows.Forms.Label lbStep0;
        private System.Windows.Forms.Label lbStep1;
        private System.Windows.Forms.Label lbStep2;
        private System.Windows.Forms.Label lbStep3;
        private System.Windows.Forms.Label lbStep4;
        private System.Windows.Forms.Label lbStep5;
        private System.Windows.Forms.Label lbStep6;
        private System.Windows.Forms.Label lbStep7;
        private System.Windows.Forms.Label lbStep8;
        private System.Windows.Forms.Label lbStep9;
        private System.Windows.Forms.Label lbStep10;
        private System.Windows.Forms.Label lbStep11;
        private System.Windows.Forms.Label lbStep12;
        private System.Windows.Forms.Label lbStepDone;
        private System.Windows.Forms.Label lbCurrentInfo;
    }
}
