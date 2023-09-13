namespace SECSGEM
{
    partial class Config
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
            this.edtLinktestPeriod = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.edtHSMSDeviceID = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.edtRemotePort = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.edtRemoteIP = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.edtLocalPort = new System.Windows.Forms.TextBox();
            this.Port = new System.Windows.Forms.Label();
            this.edtLocalIP = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.edtT8 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.edtT7 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.edtT6 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.edtT5 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.edtHSMST3 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ComboHSMSRole = new System.Windows.Forms.ComboBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.FileList = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ComboMachineType = new System.Windows.Forms.ComboBox();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // edtLinktestPeriod
            // 
            this.edtLinktestPeriod.Location = new System.Drawing.Point(232, 304);
            this.edtLinktestPeriod.Name = "edtLinktestPeriod";
            this.edtLinktestPeriod.Size = new System.Drawing.Size(67, 22);
            this.edtLinktestPeriod.TabIndex = 22;
            this.edtLinktestPeriod.Text = "60";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(147, 309);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(75, 12);
            this.label18.TabIndex = 21;
            this.label18.Text = "Linktest Period";
            // 
            // edtHSMSDeviceID
            // 
            this.edtHSMSDeviceID.Location = new System.Drawing.Point(74, 304);
            this.edtHSMSDeviceID.Name = "edtHSMSDeviceID";
            this.edtHSMSDeviceID.Size = new System.Drawing.Size(67, 22);
            this.edtHSMSDeviceID.TabIndex = 20;
            this.edtHSMSDeviceID.Text = "1";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(18, 309);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 12);
            this.label17.TabIndex = 19;
            this.label17.Text = "Device ID";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.edtRemotePort);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.edtRemoteIP);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Location = new System.Drawing.Point(18, 243);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(285, 55);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Remote IP && Port";
            // 
            // edtRemotePort
            // 
            this.edtRemotePort.Location = new System.Drawing.Point(218, 21);
            this.edtRemotePort.Name = "edtRemotePort";
            this.edtRemotePort.Size = new System.Drawing.Size(61, 22);
            this.edtRemotePort.TabIndex = 18;
            this.edtRemotePort.Text = "5001";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(194, 21);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(24, 12);
            this.label15.TabIndex = 17;
            this.label15.Text = "Port";
            // 
            // edtRemoteIP
            // 
            this.edtRemoteIP.Location = new System.Drawing.Point(31, 21);
            this.edtRemoteIP.Name = "edtRemoteIP";
            this.edtRemoteIP.Size = new System.Drawing.Size(154, 22);
            this.edtRemoteIP.TabIndex = 16;
            this.edtRemoteIP.Text = "127.0.0.1";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 21);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 12);
            this.label16.TabIndex = 15;
            this.label16.Text = "IP";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.edtLocalPort);
            this.groupBox3.Controls.Add(this.Port);
            this.groupBox3.Controls.Add(this.edtLocalIP);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Location = new System.Drawing.Point(20, 182);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(285, 55);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Local IP && Port";
            // 
            // edtLocalPort
            // 
            this.edtLocalPort.Location = new System.Drawing.Point(218, 21);
            this.edtLocalPort.Name = "edtLocalPort";
            this.edtLocalPort.Size = new System.Drawing.Size(61, 22);
            this.edtLocalPort.TabIndex = 18;
            this.edtLocalPort.Text = "5001";
            // 
            // Port
            // 
            this.Port.AutoSize = true;
            this.Port.Location = new System.Drawing.Point(194, 21);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(24, 12);
            this.Port.TabIndex = 17;
            this.Port.Text = "Port";
            // 
            // edtLocalIP
            // 
            this.edtLocalIP.Location = new System.Drawing.Point(31, 21);
            this.edtLocalIP.Name = "edtLocalIP";
            this.edtLocalIP.Size = new System.Drawing.Size(154, 22);
            this.edtLocalIP.TabIndex = 16;
            this.edtLocalIP.Text = "127.0.0.1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 21);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(15, 12);
            this.label14.TabIndex = 15;
            this.label14.Text = "IP";
            // 
            // edtT8
            // 
            this.edtT8.Location = new System.Drawing.Point(138, 154);
            this.edtT8.Name = "edtT8";
            this.edtT8.Size = new System.Drawing.Size(67, 22);
            this.edtT8.TabIndex = 16;
            this.edtT8.Text = "5";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(114, 154);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(18, 12);
            this.label13.TabIndex = 15;
            this.label13.Text = "T8";
            // 
            // edtT7
            // 
            this.edtT7.Location = new System.Drawing.Point(42, 154);
            this.edtT7.Name = "edtT7";
            this.edtT7.Size = new System.Drawing.Size(67, 22);
            this.edtT7.TabIndex = 14;
            this.edtT7.Text = "10";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 154);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(18, 12);
            this.label12.TabIndex = 13;
            this.label12.Text = "T7";
            // 
            // edtT6
            // 
            this.edtT6.Location = new System.Drawing.Point(238, 126);
            this.edtT6.Name = "edtT6";
            this.edtT6.Size = new System.Drawing.Size(67, 22);
            this.edtT6.TabIndex = 12;
            this.edtT6.Text = "5";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(214, 126);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 12);
            this.label11.TabIndex = 11;
            this.label11.Text = "T6";
            // 
            // edtT5
            // 
            this.edtT5.Location = new System.Drawing.Point(138, 126);
            this.edtT5.Name = "edtT5";
            this.edtT5.Size = new System.Drawing.Size(67, 22);
            this.edtT5.TabIndex = 10;
            this.edtT5.Text = "10";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(114, 126);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "T5";
            // 
            // edtHSMST3
            // 
            this.edtHSMST3.Location = new System.Drawing.Point(42, 126);
            this.edtHSMST3.Name = "edtHSMST3";
            this.edtHSMST3.Size = new System.Drawing.Size(67, 22);
            this.edtHSMST3.TabIndex = 8;
            this.edtHSMST3.Text = "45";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 126);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "T3";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ComboHSMSRole);
            this.groupBox2.Location = new System.Drawing.Point(12, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(293, 52);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Role";
            // 
            // ComboHSMSRole
            // 
            this.ComboHSMSRole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboHSMSRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboHSMSRole.FormattingEnabled = true;
            this.ComboHSMSRole.Items.AddRange(new object[] {
            "Passive",
            "Active"});
            this.ComboHSMSRole.Location = new System.Drawing.Point(6, 21);
            this.ComboHSMSRole.Name = "ComboHSMSRole";
            this.ComboHSMSRole.Size = new System.Drawing.Size(281, 20);
            this.ComboHSMSRole.TabIndex = 0;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(5, 334);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(152, 23);
            this.OKButton.TabIndex = 2;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(166, 334);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(152, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // FileList
            // 
            this.FileList.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ComboMachineType);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(293, 52);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MachineType";
            // 
            // ComboMachineType
            // 
            this.ComboMachineType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboMachineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboMachineType.FormattingEnabled = true;
            this.ComboMachineType.Items.AddRange(new object[] {
            "SMT",
            "FVI",
            "TrimGap"});
            this.ComboMachineType.Location = new System.Drawing.Point(6, 21);
            this.ComboMachineType.Name = "ComboMachineType";
            this.ComboMachineType.Size = new System.Drawing.Size(281, 20);
            this.ComboMachineType.TabIndex = 0;
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 363);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.edtLinktestPeriod);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.edtHSMSDeviceID);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.edtT8);
            this.Controls.Add(this.edtHSMST3);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.edtT7);
            this.Controls.Add(this.edtT5);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.edtT6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Config";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Config";
            this.Load += new System.EventHandler(this.Config_Load);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox ComboHSMSRole;
        private System.Windows.Forms.TextBox edtT6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox edtT5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox edtHSMST3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox edtT8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox edtT7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox edtLocalIP;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox edtRemotePort;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox edtRemoteIP;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox edtLocalPort;
        private System.Windows.Forms.Label Port;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog FileList;
        private System.Windows.Forms.TextBox edtLinktestPeriod;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox edtHSMSDeviceID;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox ComboMachineType;
    }
}