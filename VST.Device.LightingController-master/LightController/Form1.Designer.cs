namespace LightController
{
    partial class Form1
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
            this.btn_VLP_ch1_Confirm = new System.Windows.Forms.Button();
            this.num_channel1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_VLP_ch1_Zero = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_VLP_ch2_Zero = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.num_channel2 = new System.Windows.Forms.NumericUpDown();
            this.btn_VLP_ch2_Confirm = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btn_VLP_Close = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Open = new System.Windows.Forms.Button();
            this.txt_IP = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cb_COMNum = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btn_GLC_Zero = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.num_GLC = new System.Windows.Forms.NumericUpDown();
            this.btn_GLC_Confirm = new System.Windows.Forms.Button();
            this.btn_GLC_Close = new System.Windows.Forms.Button();
            this.btn_GLC_Open = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.num_channel1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_channel2)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_GLC)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_VLP_ch1_Confirm
            // 
            this.btn_VLP_ch1_Confirm.Enabled = false;
            this.btn_VLP_ch1_Confirm.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_VLP_ch1_Confirm.Location = new System.Drawing.Point(181, 22);
            this.btn_VLP_ch1_Confirm.Name = "btn_VLP_ch1_Confirm";
            this.btn_VLP_ch1_Confirm.Size = new System.Drawing.Size(75, 34);
            this.btn_VLP_ch1_Confirm.TabIndex = 0;
            this.btn_VLP_ch1_Confirm.Text = "確認";
            this.btn_VLP_ch1_Confirm.UseVisualStyleBackColor = true;
            this.btn_VLP_ch1_Confirm.Click += new System.EventHandler(this.btn_VLP_ch1_Confirm_Click);
            // 
            // num_channel1
            // 
            this.num_channel1.Enabled = false;
            this.num_channel1.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.num_channel1.Location = new System.Drawing.Point(89, 24);
            this.num_channel1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.num_channel1.Name = "num_channel1";
            this.num_channel1.Size = new System.Drawing.Size(86, 30);
            this.num_channel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("標楷體", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "強度";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_VLP_ch1_Zero);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.num_channel1);
            this.groupBox1.Controls.Add(this.btn_VLP_ch1_Confirm);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 65);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "channel 1";
            // 
            // btn_VLP_ch1_Zero
            // 
            this.btn_VLP_ch1_Zero.Enabled = false;
            this.btn_VLP_ch1_Zero.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_VLP_ch1_Zero.Location = new System.Drawing.Point(259, 23);
            this.btn_VLP_ch1_Zero.Name = "btn_VLP_ch1_Zero";
            this.btn_VLP_ch1_Zero.Size = new System.Drawing.Size(75, 34);
            this.btn_VLP_ch1_Zero.TabIndex = 16;
            this.btn_VLP_ch1_Zero.Text = "歸零";
            this.btn_VLP_ch1_Zero.UseVisualStyleBackColor = true;
            this.btn_VLP_ch1_Zero.Click += new System.EventHandler(this.btn_VLP_ch1_Zero_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_VLP_ch2_Zero);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.num_channel2);
            this.groupBox2.Controls.Add(this.btn_VLP_ch2_Confirm);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 180);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(340, 65);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "channel 2";
            // 
            // btn_VLP_ch2_Zero
            // 
            this.btn_VLP_ch2_Zero.Enabled = false;
            this.btn_VLP_ch2_Zero.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_VLP_ch2_Zero.Location = new System.Drawing.Point(259, 22);
            this.btn_VLP_ch2_Zero.Name = "btn_VLP_ch2_Zero";
            this.btn_VLP_ch2_Zero.Size = new System.Drawing.Size(75, 34);
            this.btn_VLP_ch2_Zero.TabIndex = 17;
            this.btn_VLP_ch2_Zero.Text = "歸零";
            this.btn_VLP_ch2_Zero.UseVisualStyleBackColor = true;
            this.btn_VLP_ch2_Zero.Click += new System.EventHandler(this.btn_VLP_ch2_Zero_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("標楷體", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(12, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 29);
            this.label4.TabIndex = 2;
            this.label4.Text = "強度";
            // 
            // num_channel2
            // 
            this.num_channel2.Enabled = false;
            this.num_channel2.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.num_channel2.Location = new System.Drawing.Point(89, 24);
            this.num_channel2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.num_channel2.Name = "num_channel2";
            this.num_channel2.Size = new System.Drawing.Size(86, 30);
            this.num_channel2.TabIndex = 1;
            // 
            // btn_VLP_ch2_Confirm
            // 
            this.btn_VLP_ch2_Confirm.Enabled = false;
            this.btn_VLP_ch2_Confirm.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_VLP_ch2_Confirm.Location = new System.Drawing.Point(181, 22);
            this.btn_VLP_ch2_Confirm.Name = "btn_VLP_ch2_Confirm";
            this.btn_VLP_ch2_Confirm.Size = new System.Drawing.Size(75, 34);
            this.btn_VLP_ch2_Confirm.TabIndex = 0;
            this.btn_VLP_ch2_Confirm.Text = "確認";
            this.btn_VLP_ch2_Confirm.UseVisualStyleBackColor = true;
            this.btn_VLP_ch2_Confirm.Click += new System.EventHandler(this.btn_VLP_ch2_Confirm_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.textBox2);
            this.groupBox4.Controls.Add(this.btn_VLP_Close);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.btn_Open);
            this.groupBox4.Controls.Add(this.txt_IP);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(13, 201);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(359, 259);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "VLP-2460-4eN";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(247, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 23);
            this.label5.TabIndex = 11;
            this.label5.Text = "Port";
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(296, 74);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(47, 30);
            this.textBox2.TabIndex = 12;
            this.textBox2.Text = "1000";
            // 
            // btn_VLP_Close
            // 
            this.btn_VLP_Close.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_VLP_Close.Location = new System.Drawing.Point(186, 27);
            this.btn_VLP_Close.Name = "btn_VLP_Close";
            this.btn_VLP_Close.Size = new System.Drawing.Size(133, 34);
            this.btn_VLP_Close.TabIndex = 10;
            this.btn_VLP_Close.Text = "Close";
            this.btn_VLP_Close.UseVisualStyleBackColor = true;
            this.btn_VLP_Close.Click += new System.EventHandler(this.btn_VLP_Close_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "IP Address";
            // 
            // btn_Open
            // 
            this.btn_Open.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Open.Location = new System.Drawing.Point(47, 27);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(133, 34);
            this.btn_Open.TabIndex = 3;
            this.btn_Open.Text = "Open";
            this.btn_Open.UseVisualStyleBackColor = true;
            this.btn_Open.Click += new System.EventHandler(this.btn_VLP_Open_Click);
            // 
            // txt_IP
            // 
            this.txt_IP.Enabled = false;
            this.txt_IP.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_IP.Location = new System.Drawing.Point(116, 74);
            this.txt_IP.Name = "txt_IP";
            this.txt_IP.ReadOnly = true;
            this.txt_IP.Size = new System.Drawing.Size(128, 30);
            this.txt_IP.TabIndex = 8;
            this.txt_IP.Text = "192.168.11.20";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cb_COMNum);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.groupBox7);
            this.groupBox6.Controls.Add(this.btn_GLC_Close);
            this.groupBox6.Controls.Add(this.btn_GLC_Open);
            this.groupBox6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(13, 12);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(359, 183);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "GLC-DPI2-170";
            // 
            // cb_COMNum
            // 
            this.cb_COMNum.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_COMNum.FormattingEnabled = true;
            this.cb_COMNum.Items.AddRange(new object[] {
            "COM1"});
            this.cb_COMNum.Location = new System.Drawing.Point(130, 71);
            this.cb_COMNum.Name = "cb_COMNum";
            this.cb_COMNum.Size = new System.Drawing.Size(121, 31);
            this.cb_COMNum.TabIndex = 14;
            this.cb_COMNum.Text = "COM1";
            this.cb_COMNum.SelectedIndexChanged += new System.EventHandler(this.cb_COMNum_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(52, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 23);
            this.label7.TabIndex = 11;
            this.label7.Text = "COM埠";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btn_GLC_Zero);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.num_GLC);
            this.groupBox7.Controls.Add(this.btn_GLC_Confirm);
            this.groupBox7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(12, 106);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(340, 65);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "channel 1";
            // 
            // btn_GLC_Zero
            // 
            this.btn_GLC_Zero.Enabled = false;
            this.btn_GLC_Zero.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_GLC_Zero.Location = new System.Drawing.Point(259, 22);
            this.btn_GLC_Zero.Name = "btn_GLC_Zero";
            this.btn_GLC_Zero.Size = new System.Drawing.Size(75, 34);
            this.btn_GLC_Zero.TabIndex = 15;
            this.btn_GLC_Zero.Text = "歸零";
            this.btn_GLC_Zero.UseVisualStyleBackColor = true;
            this.btn_GLC_Zero.Click += new System.EventHandler(this.btn_GLC_Zero_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("標楷體", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(12, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "強度";
            // 
            // num_GLC
            // 
            this.num_GLC.Enabled = false;
            this.num_GLC.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.num_GLC.Location = new System.Drawing.Point(89, 24);
            this.num_GLC.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.num_GLC.Name = "num_GLC";
            this.num_GLC.Size = new System.Drawing.Size(86, 30);
            this.num_GLC.TabIndex = 1;
            // 
            // btn_GLC_Confirm
            // 
            this.btn_GLC_Confirm.Enabled = false;
            this.btn_GLC_Confirm.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_GLC_Confirm.Location = new System.Drawing.Point(181, 22);
            this.btn_GLC_Confirm.Name = "btn_GLC_Confirm";
            this.btn_GLC_Confirm.Size = new System.Drawing.Size(75, 34);
            this.btn_GLC_Confirm.TabIndex = 0;
            this.btn_GLC_Confirm.Text = "確認";
            this.btn_GLC_Confirm.UseVisualStyleBackColor = true;
            this.btn_GLC_Confirm.Click += new System.EventHandler(this.btn_GLC_Confirm_Click);
            // 
            // btn_GLC_Close
            // 
            this.btn_GLC_Close.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_GLC_Close.Location = new System.Drawing.Point(186, 26);
            this.btn_GLC_Close.Name = "btn_GLC_Close";
            this.btn_GLC_Close.Size = new System.Drawing.Size(133, 34);
            this.btn_GLC_Close.TabIndex = 13;
            this.btn_GLC_Close.Text = "Close";
            this.btn_GLC_Close.UseVisualStyleBackColor = true;
            this.btn_GLC_Close.Click += new System.EventHandler(this.btn_GLC_Close_Click);
            // 
            // btn_GLC_Open
            // 
            this.btn_GLC_Open.Font = new System.Drawing.Font("標楷體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_GLC_Open.Location = new System.Drawing.Point(47, 26);
            this.btn_GLC_Open.Name = "btn_GLC_Open";
            this.btn_GLC_Open.Size = new System.Drawing.Size(133, 34);
            this.btn_GLC_Open.TabIndex = 11;
            this.btn_GLC_Open.Text = "Open";
            this.btn_GLC_Open.UseVisualStyleBackColor = true;
            this.btn_GLC_Open.Click += new System.EventHandler(this.btn_GLC_Open_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.BackgroundImage = global::LightController.Properties.Resources.cancel1;
            this.btn_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Close.Location = new System.Drawing.Point(379, 20);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(67, 67);
            this.btn_Close.TabIndex = 13;
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 483);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Name = "Form1";
            this.Text = "Light Controller";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num_channel1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_channel2)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_GLC)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_VLP_ch1_Confirm;
        private System.Windows.Forms.NumericUpDown num_channel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown num_channel2;
        private System.Windows.Forms.Button btn_VLP_ch2_Confirm;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txt_IP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown num_GLC;
        private System.Windows.Forms.Button btn_GLC_Confirm;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox cb_COMNum;
        private System.Windows.Forms.Button btn_GLC_Zero;
        private System.Windows.Forms.Button btn_VLP_ch1_Zero;
        private System.Windows.Forms.Button btn_VLP_ch2_Zero;
        private System.Windows.Forms.Button btn_VLP_Close;
        private System.Windows.Forms.Button btn_Open;
        private System.Windows.Forms.Button btn_GLC_Close;
        private System.Windows.Forms.Button btn_GLC_Open;
        private System.Windows.Forms.Button btn_Close;
    }
}

