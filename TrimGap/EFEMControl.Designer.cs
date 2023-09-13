namespace TrimGap
{
    partial class EFEMControl
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
            this.gb_Robot = new System.Windows.Forms.GroupBox();
            this.Robot_Send = new System.Windows.Forms.Button();
            this.listBox_Robot = new System.Windows.Forms.ListBox();
            this.cb_Robot_Slot = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_Robot_Destination = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_Robot_Arm = new System.Windows.Forms.ComboBox();
            this.gb_Aligner = new System.Windows.Forms.GroupBox();
            this.Aligner_Send = new System.Windows.Forms.Button();
            this.cb_Alignment_Angle = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox_Aligner = new System.Windows.Forms.ListBox();
            this.gb_LoadPort = new System.Windows.Forms.GroupBox();
            this.tb_FoupID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_LP_Port = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.LoadPort_Send = new System.Windows.Forms.Button();
            this.cb_LP_LEDSts = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox_LoadPort = new System.Windows.Forms.ListBox();
            this.gb_API = new System.Windows.Forms.GroupBox();
            this.tb_API_Sts = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.API_Send = new System.Windows.Forms.Button();
            this.listBox_API = new System.Windows.Forms.ListBox();
            this.gb_SignalTower = new System.Windows.Forms.GroupBox();
            this.SignalTower_Send = new System.Windows.Forms.Button();
            this.listBox_SignalTower = new System.Windows.Forms.ListBox();
            this.bW_SendCmd = new System.ComponentModel.BackgroundWorker();
            this.gbSetStatus = new System.Windows.Forms.GroupBox();
            this.cb_SetStatus_Robot = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cb_SetStatus = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cb_Set_Robot = new System.Windows.Forms.ComboBox();
            this.cb_SetSlot_Robot = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btn_SetSlotSts_Robot = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.cb_SetPort = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_SetSlotSts = new System.Windows.Forms.Button();
            this.cb_SetSlot = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_E84_Num = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.E84_Send = new System.Windows.Forms.Button();
            this.listBox_E84 = new System.Windows.Forms.ListBox();
            this.gb_Robot.SuspendLayout();
            this.gb_Aligner.SuspendLayout();
            this.gb_LoadPort.SuspendLayout();
            this.gb_API.SuspendLayout();
            this.gb_SignalTower.SuspendLayout();
            this.gbSetStatus.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_Robot
            // 
            this.gb_Robot.Controls.Add(this.Robot_Send);
            this.gb_Robot.Controls.Add(this.listBox_Robot);
            this.gb_Robot.Controls.Add(this.cb_Robot_Slot);
            this.gb_Robot.Controls.Add(this.label3);
            this.gb_Robot.Controls.Add(this.cb_Robot_Destination);
            this.gb_Robot.Controls.Add(this.label2);
            this.gb_Robot.Controls.Add(this.label1);
            this.gb_Robot.Controls.Add(this.cb_Robot_Arm);
            this.gb_Robot.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.gb_Robot.Location = new System.Drawing.Point(12, 7);
            this.gb_Robot.Name = "gb_Robot";
            this.gb_Robot.Size = new System.Drawing.Size(225, 321);
            this.gb_Robot.TabIndex = 0;
            this.gb_Robot.TabStop = false;
            this.gb_Robot.Text = "Robot";
            // 
            // Robot_Send
            // 
            this.Robot_Send.Location = new System.Drawing.Point(9, 269);
            this.Robot_Send.Name = "Robot_Send";
            this.Robot_Send.Size = new System.Drawing.Size(200, 40);
            this.Robot_Send.TabIndex = 7;
            this.Robot_Send.Text = "Send";
            this.Robot_Send.UseVisualStyleBackColor = true;
            this.Robot_Send.Click += new System.EventHandler(this.Robot_Send_Click);
            // 
            // listBox_Robot
            // 
            this.listBox_Robot.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.listBox_Robot.FormattingEnabled = true;
            this.listBox_Robot.ItemHeight = 17;
            this.listBox_Robot.Items.AddRange(new object[] {
            "GetStatus",
            "ResetError",
            "Home",
            "WaferGet",
            "WaferPut",
            "VacuumOn",
            "VacuumOff",
            "SetSpeed10",
            "SetSpeed30",
            "SetSpeed50"});
            this.listBox_Robot.Location = new System.Drawing.Point(9, 123);
            this.listBox_Robot.Name = "listBox_Robot";
            this.listBox_Robot.Size = new System.Drawing.Size(200, 140);
            this.listBox_Robot.TabIndex = 6;
            // 
            // cb_Robot_Slot
            // 
            this.cb_Robot_Slot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Robot_Slot.FormattingEnabled = true;
            this.cb_Robot_Slot.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
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
            this.cb_Robot_Slot.Location = new System.Drawing.Point(111, 84);
            this.cb_Robot_Slot.Name = "cb_Robot_Slot";
            this.cb_Robot_Slot.Size = new System.Drawing.Size(99, 28);
            this.cb_Robot_Slot.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(6, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Slot：";
            // 
            // cb_Robot_Destination
            // 
            this.cb_Robot_Destination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Robot_Destination.FormattingEnabled = true;
            this.cb_Robot_Destination.Items.AddRange(new object[] {
            "P1",
            "P2",
            "Pn_Run",
            "Aligner1",
            "Stage1"});
            this.cb_Robot_Destination.Location = new System.Drawing.Point(111, 53);
            this.cb_Robot_Destination.Name = "cb_Robot_Destination";
            this.cb_Robot_Destination.Size = new System.Drawing.Size(99, 28);
            this.cb_Robot_Destination.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Destination：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Arm：";
            // 
            // cb_Robot_Arm
            // 
            this.cb_Robot_Arm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Robot_Arm.FormattingEnabled = true;
            this.cb_Robot_Arm.Items.AddRange(new object[] {
            "Lower",
            "Upper"});
            this.cb_Robot_Arm.Location = new System.Drawing.Point(111, 22);
            this.cb_Robot_Arm.Name = "cb_Robot_Arm";
            this.cb_Robot_Arm.Size = new System.Drawing.Size(99, 28);
            this.cb_Robot_Arm.TabIndex = 0;
            // 
            // gb_Aligner
            // 
            this.gb_Aligner.Controls.Add(this.Aligner_Send);
            this.gb_Aligner.Controls.Add(this.cb_Alignment_Angle);
            this.gb_Aligner.Controls.Add(this.label4);
            this.gb_Aligner.Controls.Add(this.listBox_Aligner);
            this.gb_Aligner.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.gb_Aligner.Location = new System.Drawing.Point(487, 230);
            this.gb_Aligner.Name = "gb_Aligner";
            this.gb_Aligner.Size = new System.Drawing.Size(225, 275);
            this.gb_Aligner.TabIndex = 1;
            this.gb_Aligner.TabStop = false;
            this.gb_Aligner.Text = "Aligner";
            // 
            // Aligner_Send
            // 
            this.Aligner_Send.Location = new System.Drawing.Point(10, 224);
            this.Aligner_Send.Name = "Aligner_Send";
            this.Aligner_Send.Size = new System.Drawing.Size(200, 40);
            this.Aligner_Send.TabIndex = 13;
            this.Aligner_Send.Text = "Send";
            this.Aligner_Send.UseVisualStyleBackColor = true;
            this.Aligner_Send.Click += new System.EventHandler(this.Aligner_Send_Click);
            // 
            // cb_Alignment_Angle
            // 
            this.cb_Alignment_Angle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Alignment_Angle.FormattingEnabled = true;
            this.cb_Alignment_Angle.Items.AddRange(new object[] {
            "0",
            "90",
            "180",
            "270"});
            this.cb_Alignment_Angle.Location = new System.Drawing.Point(111, 20);
            this.cb_Alignment_Angle.Name = "cb_Alignment_Angle";
            this.cb_Alignment_Angle.Size = new System.Drawing.Size(99, 28);
            this.cb_Alignment_Angle.TabIndex = 9;
            this.cb_Alignment_Angle.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(7, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Degree：";
            this.label4.Visible = false;
            // 
            // listBox_Aligner
            // 
            this.listBox_Aligner.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.listBox_Aligner.FormattingEnabled = true;
            this.listBox_Aligner.ItemHeight = 17;
            this.listBox_Aligner.Items.AddRange(new object[] {
            "GetStatus",
            "ResetError",
            "Home",
            "VacuumOn",
            "VacuumOff",
            "Alignment"});
            this.listBox_Aligner.Location = new System.Drawing.Point(10, 61);
            this.listBox_Aligner.Name = "listBox_Aligner";
            this.listBox_Aligner.Size = new System.Drawing.Size(200, 157);
            this.listBox_Aligner.TabIndex = 7;
            // 
            // gb_LoadPort
            // 
            this.gb_LoadPort.Controls.Add(this.tb_FoupID);
            this.gb_LoadPort.Controls.Add(this.label7);
            this.gb_LoadPort.Controls.Add(this.cb_LP_Port);
            this.gb_LoadPort.Controls.Add(this.label6);
            this.gb_LoadPort.Controls.Add(this.LoadPort_Send);
            this.gb_LoadPort.Controls.Add(this.cb_LP_LEDSts);
            this.gb_LoadPort.Controls.Add(this.label5);
            this.gb_LoadPort.Controls.Add(this.listBox_LoadPort);
            this.gb_LoadPort.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.gb_LoadPort.Location = new System.Drawing.Point(243, 7);
            this.gb_LoadPort.Name = "gb_LoadPort";
            this.gb_LoadPort.Size = new System.Drawing.Size(232, 498);
            this.gb_LoadPort.TabIndex = 2;
            this.gb_LoadPort.TabStop = false;
            this.gb_LoadPort.Text = "LoadPort";
            // 
            // tb_FoupID
            // 
            this.tb_FoupID.Location = new System.Drawing.Point(118, 52);
            this.tb_FoupID.Name = "tb_FoupID";
            this.tb_FoupID.ReadOnly = true;
            this.tb_FoupID.Size = new System.Drawing.Size(99, 29);
            this.tb_FoupID.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(17, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 17);
            this.label7.TabIndex = 15;
            this.label7.Text = "FoupID：";
            // 
            // cb_LP_Port
            // 
            this.cb_LP_Port.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_LP_Port.FormattingEnabled = true;
            this.cb_LP_Port.Items.AddRange(new object[] {
            "P1",
            "P2",
            "Pn_Run"});
            this.cb_LP_Port.Location = new System.Drawing.Point(118, 21);
            this.cb_LP_Port.Name = "cb_LP_Port";
            this.cb_LP_Port.Size = new System.Drawing.Size(99, 28);
            this.cb_LP_Port.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(17, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 17);
            this.label6.TabIndex = 13;
            this.label6.Text = "Port：";
            // 
            // LoadPort_Send
            // 
            this.LoadPort_Send.Location = new System.Drawing.Point(18, 447);
            this.LoadPort_Send.Name = "LoadPort_Send";
            this.LoadPort_Send.Size = new System.Drawing.Size(200, 40);
            this.LoadPort_Send.TabIndex = 12;
            this.LoadPort_Send.Text = "Send";
            this.LoadPort_Send.UseVisualStyleBackColor = true;
            this.LoadPort_Send.Click += new System.EventHandler(this.LoadPort_Send_Click);
            // 
            // cb_LP_LEDSts
            // 
            this.cb_LP_LEDSts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_LP_LEDSts.FormattingEnabled = true;
            this.cb_LP_LEDSts.Items.AddRange(new object[] {
            "On",
            "Off",
            "Flash"});
            this.cb_LP_LEDSts.Location = new System.Drawing.Point(118, 84);
            this.cb_LP_LEDSts.Name = "cb_LP_LEDSts";
            this.cb_LP_LEDSts.Size = new System.Drawing.Size(99, 28);
            this.cb_LP_LEDSts.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(17, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "LED：";
            // 
            // listBox_LoadPort
            // 
            this.listBox_LoadPort.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.listBox_LoadPort.FormattingEnabled = true;
            this.listBox_LoadPort.ItemHeight = 17;
            this.listBox_LoadPort.Items.AddRange(new object[] {
            "GetStatus",
            "ResetError",
            "Home",
            "Load",
            "Unload",
            "Mapping",
            "ReadFoupID",
            "LEDLoad",
            "LEDUnLoad",
            "LEDStatus1",
            "LEDStatus2",
            "SetOperatorAccessButton"});
            this.listBox_LoadPort.Location = new System.Drawing.Point(17, 130);
            this.listBox_LoadPort.Name = "listBox_LoadPort";
            this.listBox_LoadPort.Size = new System.Drawing.Size(200, 310);
            this.listBox_LoadPort.TabIndex = 7;
            // 
            // gb_API
            // 
            this.gb_API.Controls.Add(this.tb_API_Sts);
            this.gb_API.Controls.Add(this.label8);
            this.gb_API.Controls.Add(this.API_Send);
            this.gb_API.Controls.Add(this.listBox_API);
            this.gb_API.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.gb_API.Location = new System.Drawing.Point(718, 7);
            this.gb_API.Name = "gb_API";
            this.gb_API.Size = new System.Drawing.Size(225, 171);
            this.gb_API.TabIndex = 3;
            this.gb_API.TabStop = false;
            this.gb_API.Text = "API";
            // 
            // tb_API_Sts
            // 
            this.tb_API_Sts.Location = new System.Drawing.Point(111, 19);
            this.tb_API_Sts.Name = "tb_API_Sts";
            this.tb_API_Sts.ReadOnly = true;
            this.tb_API_Sts.Size = new System.Drawing.Size(99, 29);
            this.tb_API_Sts.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(6, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 17);
            this.label8.TabIndex = 17;
            this.label8.Text = "API Status：";
            // 
            // API_Send
            // 
            this.API_Send.Location = new System.Drawing.Point(10, 118);
            this.API_Send.Name = "API_Send";
            this.API_Send.Size = new System.Drawing.Size(200, 40);
            this.API_Send.TabIndex = 8;
            this.API_Send.Text = "Send";
            this.API_Send.UseVisualStyleBackColor = true;
            this.API_Send.Click += new System.EventHandler(this.API_Send_Click);
            // 
            // listBox_API
            // 
            this.listBox_API.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.listBox_API.FormattingEnabled = true;
            this.listBox_API.ItemHeight = 17;
            this.listBox_API.Items.AddRange(new object[] {
            "Remote",
            "Local",
            "CurrentMode"});
            this.listBox_API.Location = new System.Drawing.Point(10, 55);
            this.listBox_API.Name = "listBox_API";
            this.listBox_API.Size = new System.Drawing.Size(200, 55);
            this.listBox_API.TabIndex = 7;
            // 
            // gb_SignalTower
            // 
            this.gb_SignalTower.Controls.Add(this.SignalTower_Send);
            this.gb_SignalTower.Controls.Add(this.listBox_SignalTower);
            this.gb_SignalTower.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.gb_SignalTower.Location = new System.Drawing.Point(487, 7);
            this.gb_SignalTower.Name = "gb_SignalTower";
            this.gb_SignalTower.Size = new System.Drawing.Size(225, 212);
            this.gb_SignalTower.TabIndex = 4;
            this.gb_SignalTower.TabStop = false;
            this.gb_SignalTower.Text = "SignalTower";
            // 
            // SignalTower_Send
            // 
            this.SignalTower_Send.Location = new System.Drawing.Point(10, 158);
            this.SignalTower_Send.Name = "SignalTower_Send";
            this.SignalTower_Send.Size = new System.Drawing.Size(200, 40);
            this.SignalTower_Send.TabIndex = 9;
            this.SignalTower_Send.Text = "Send";
            this.SignalTower_Send.UseVisualStyleBackColor = true;
            this.SignalTower_Send.Click += new System.EventHandler(this.SignalTower_Send_Click);
            // 
            // listBox_SignalTower
            // 
            this.listBox_SignalTower.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.listBox_SignalTower.FormattingEnabled = true;
            this.listBox_SignalTower.ItemHeight = 17;
            this.listBox_SignalTower.Items.AddRange(new object[] {
            "GreenOn",
            "GreenOff",
            "GreenFlash",
            "YellowOn",
            "YellowOff",
            "YellowFlash",
            "RedOn",
            "RedOff",
            "RedFlash",
            "AllOff",
            "AllFlash",
            "BuzzerOn",
            "BuzzerOff"});
            this.listBox_SignalTower.Location = new System.Drawing.Point(10, 33);
            this.listBox_SignalTower.Name = "listBox_SignalTower";
            this.listBox_SignalTower.Size = new System.Drawing.Size(200, 123);
            this.listBox_SignalTower.TabIndex = 7;
            // 
            // bW_SendCmd
            // 
            this.bW_SendCmd.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bW_SendCmd_DoWork);
            this.bW_SendCmd.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bW_SendCmd_ProgressChanged);
            // 
            // gbSetStatus
            // 
            this.gbSetStatus.Controls.Add(this.cb_SetStatus_Robot);
            this.gbSetStatus.Controls.Add(this.label14);
            this.gbSetStatus.Controls.Add(this.cb_SetStatus);
            this.gbSetStatus.Controls.Add(this.label11);
            this.gbSetStatus.Controls.Add(this.cb_Set_Robot);
            this.gbSetStatus.Controls.Add(this.cb_SetSlot_Robot);
            this.gbSetStatus.Controls.Add(this.label13);
            this.gbSetStatus.Controls.Add(this.btn_SetSlotSts_Robot);
            this.gbSetStatus.Controls.Add(this.label12);
            this.gbSetStatus.Controls.Add(this.cb_SetPort);
            this.gbSetStatus.Controls.Add(this.label10);
            this.gbSetStatus.Controls.Add(this.btn_SetSlotSts);
            this.gbSetStatus.Controls.Add(this.cb_SetSlot);
            this.gbSetStatus.Controls.Add(this.label9);
            this.gbSetStatus.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.gbSetStatus.Location = new System.Drawing.Point(718, 184);
            this.gbSetStatus.Name = "gbSetStatus";
            this.gbSetStatus.Size = new System.Drawing.Size(225, 321);
            this.gbSetStatus.TabIndex = 5;
            this.gbSetStatus.TabStop = false;
            this.gbSetStatus.Text = "SetStatus";
            // 
            // cb_SetStatus_Robot
            // 
            this.cb_SetStatus_Robot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_SetStatus_Robot.FormattingEnabled = true;
            this.cb_SetStatus_Robot.Items.AddRange(new object[] {
            "Ready",
            "Aligner1",
            "Stage1",
            "End"});
            this.cb_SetStatus_Robot.Location = new System.Drawing.Point(111, 239);
            this.cb_SetStatus_Robot.Name = "cb_SetStatus_Robot";
            this.cb_SetStatus_Robot.Size = new System.Drawing.Size(99, 28);
            this.cb_SetStatus_Robot.TabIndex = 28;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label14.Location = new System.Drawing.Point(6, 239);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 17);
            this.label14.TabIndex = 27;
            this.label14.Text = "Status：";
            // 
            // cb_SetStatus
            // 
            this.cb_SetStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_SetStatus.FormattingEnabled = true;
            this.cb_SetStatus.Items.AddRange(new object[] {
            "Ready",
            "Aligner1",
            "Stage1",
            "End",
            "Empty"});
            this.cb_SetStatus.Location = new System.Drawing.Point(111, 88);
            this.cb_SetStatus.Name = "cb_SetStatus";
            this.cb_SetStatus.Size = new System.Drawing.Size(99, 28);
            this.cb_SetStatus.TabIndex = 26;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(6, 88);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 17);
            this.label11.TabIndex = 25;
            this.label11.Text = "Status：";
            // 
            // cb_Set_Robot
            // 
            this.cb_Set_Robot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Set_Robot.FormattingEnabled = true;
            this.cb_Set_Robot.Items.AddRange(new object[] {
            "Lower",
            "Upper"});
            this.cb_Set_Robot.Location = new System.Drawing.Point(111, 177);
            this.cb_Set_Robot.Name = "cb_Set_Robot";
            this.cb_Set_Robot.Size = new System.Drawing.Size(99, 28);
            this.cb_Set_Robot.TabIndex = 24;
            // 
            // cb_SetSlot_Robot
            // 
            this.cb_SetSlot_Robot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_SetSlot_Robot.FormattingEnabled = true;
            this.cb_SetSlot_Robot.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
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
            this.cb_SetSlot_Robot.Location = new System.Drawing.Point(111, 208);
            this.cb_SetSlot_Robot.Name = "cb_SetSlot_Robot";
            this.cb_SetSlot_Robot.Size = new System.Drawing.Size(99, 28);
            this.cb_SetSlot_Robot.TabIndex = 23;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label13.Location = new System.Drawing.Point(8, 180);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 17);
            this.label13.TabIndex = 22;
            this.label13.Text = "Arm：";
            // 
            // btn_SetSlotSts_Robot
            // 
            this.btn_SetSlotSts_Robot.Location = new System.Drawing.Point(12, 270);
            this.btn_SetSlotSts_Robot.Name = "btn_SetSlotSts_Robot";
            this.btn_SetSlotSts_Robot.Size = new System.Drawing.Size(200, 40);
            this.btn_SetSlotSts_Robot.TabIndex = 19;
            this.btn_SetSlotSts_Robot.Text = "Send";
            this.btn_SetSlotSts_Robot.UseVisualStyleBackColor = true;
            this.btn_SetSlotSts_Robot.Click += new System.EventHandler(this.btn_SetSlotSts_Robot_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.Location = new System.Drawing.Point(7, 211);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 17);
            this.label12.TabIndex = 17;
            this.label12.Text = "Slot：";
            // 
            // cb_SetPort
            // 
            this.cb_SetPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_SetPort.FormattingEnabled = true;
            this.cb_SetPort.Items.AddRange(new object[] {
            "P1",
            "P2",
            "Pn_Run",
            "Aligner1",
            "Stage1"});
            this.cb_SetPort.Location = new System.Drawing.Point(111, 26);
            this.cb_SetPort.Name = "cb_SetPort";
            this.cb_SetPort.Size = new System.Drawing.Size(99, 28);
            this.cb_SetPort.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(7, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 17);
            this.label10.TabIndex = 15;
            this.label10.Text = "Port：";
            // 
            // btn_SetSlotSts
            // 
            this.btn_SetSlotSts.Location = new System.Drawing.Point(10, 118);
            this.btn_SetSlotSts.Name = "btn_SetSlotSts";
            this.btn_SetSlotSts.Size = new System.Drawing.Size(200, 40);
            this.btn_SetSlotSts.TabIndex = 7;
            this.btn_SetSlotSts.Text = "Send";
            this.btn_SetSlotSts.UseVisualStyleBackColor = true;
            this.btn_SetSlotSts.Click += new System.EventHandler(this.btn_SetSlotSts_Click);
            // 
            // cb_SetSlot
            // 
            this.cb_SetSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_SetSlot.FormattingEnabled = true;
            this.cb_SetSlot.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
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
            this.cb_SetSlot.Location = new System.Drawing.Point(111, 57);
            this.cb_SetSlot.Name = "cb_SetSlot";
            this.cb_SetSlot.Size = new System.Drawing.Size(99, 28);
            this.cb_SetSlot.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(6, 60);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 17);
            this.label9.TabIndex = 4;
            this.label9.Text = "Slot：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_E84_Num);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.E84_Send);
            this.groupBox1.Controls.Add(this.listBox_E84);
            this.groupBox1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(12, 334);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 171);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "E84";
            // 
            // cb_E84_Num
            // 
            this.cb_E84_Num.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_E84_Num.FormattingEnabled = true;
            this.cb_E84_Num.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cb_E84_Num.Location = new System.Drawing.Point(110, 22);
            this.cb_E84_Num.Name = "cb_E84_Num";
            this.cb_E84_Num.Size = new System.Drawing.Size(99, 28);
            this.cb_E84_Num.TabIndex = 10;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label15.Location = new System.Drawing.Point(6, 28);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 17);
            this.label15.TabIndex = 9;
            this.label15.Text = "E84：";
            // 
            // E84_Send
            // 
            this.E84_Send.Location = new System.Drawing.Point(9, 120);
            this.E84_Send.Name = "E84_Send";
            this.E84_Send.Size = new System.Drawing.Size(200, 40);
            this.E84_Send.TabIndex = 8;
            this.E84_Send.Text = "Send";
            this.E84_Send.UseVisualStyleBackColor = true;
            this.E84_Send.Click += new System.EventHandler(this.E84_Send_Click);
            // 
            // listBox_E84
            // 
            this.listBox_E84.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.listBox_E84.FormattingEnabled = true;
            this.listBox_E84.ItemHeight = 17;
            this.listBox_E84.Items.AddRange(new object[] {
            "Reset",
            "Auto",
            "Manual"});
            this.listBox_E84.Location = new System.Drawing.Point(9, 58);
            this.listBox_E84.Name = "listBox_E84";
            this.listBox_E84.Size = new System.Drawing.Size(199, 55);
            this.listBox_E84.TabIndex = 7;
            // 
            // EFEMControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 512);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbSetStatus);
            this.Controls.Add(this.gb_API);
            this.Controls.Add(this.gb_SignalTower);
            this.Controls.Add(this.gb_Aligner);
            this.Controls.Add(this.gb_LoadPort);
            this.Controls.Add(this.gb_Robot);
            this.Name = "EFEMControl";
            this.Text = "EFEMControl";
            this.gb_Robot.ResumeLayout(false);
            this.gb_Robot.PerformLayout();
            this.gb_Aligner.ResumeLayout(false);
            this.gb_Aligner.PerformLayout();
            this.gb_LoadPort.ResumeLayout(false);
            this.gb_LoadPort.PerformLayout();
            this.gb_API.ResumeLayout(false);
            this.gb_API.PerformLayout();
            this.gb_SignalTower.ResumeLayout(false);
            this.gbSetStatus.ResumeLayout(false);
            this.gbSetStatus.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_Robot;
        private System.Windows.Forms.GroupBox gb_Aligner;
        private System.Windows.Forms.GroupBox gb_LoadPort;
        private System.Windows.Forms.ComboBox cb_Robot_Arm;
        private System.Windows.Forms.ListBox listBox_Robot;
        private System.Windows.Forms.ComboBox cb_Robot_Slot;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_Robot_Destination;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox_Aligner;
        private System.Windows.Forms.ListBox listBox_LoadPort;
        private System.Windows.Forms.GroupBox gb_API;
        private System.Windows.Forms.ListBox listBox_API;
        private System.Windows.Forms.ComboBox cb_Alignment_Angle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_LP_LEDSts;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gb_SignalTower;
        private System.Windows.Forms.ListBox listBox_SignalTower;
        private System.Windows.Forms.Button Robot_Send;
        private System.Windows.Forms.Button Aligner_Send;
        private System.Windows.Forms.TextBox tb_FoupID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_LP_Port;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button LoadPort_Send;
        private System.Windows.Forms.Button API_Send;
        private System.Windows.Forms.Button SignalTower_Send;
        private System.Windows.Forms.TextBox tb_API_Sts;
        private System.Windows.Forms.Label label8;
        private System.ComponentModel.BackgroundWorker bW_SendCmd;
        private System.Windows.Forms.GroupBox gbSetStatus;
        private System.Windows.Forms.ComboBox cb_Set_Robot;
        private System.Windows.Forms.ComboBox cb_SetSlot_Robot;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btn_SetSlotSts_Robot;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cb_SetPort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_SetSlotSts;
        private System.Windows.Forms.ComboBox cb_SetSlot;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cb_SetStatus;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cb_SetStatus_Robot;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button E84_Send;
        private System.Windows.Forms.ListBox listBox_E84;
        private System.Windows.Forms.ComboBox cb_E84_Num;
        private System.Windows.Forms.Label label15;
    }
}