namespace TrimGap
{
    partial class CreatPjCJ
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxPJ = new System.Windows.Forms.GroupBox();
            this.panelPJInput = new System.Windows.Forms.Panel();
            this.chkAutoStart = new System.Windows.Forms.CheckBox();
            this.lblSelectedRecipe = new System.Windows.Forms.Label();
            this.txtCarrierID = new System.Windows.Forms.TextBox();
            this.lblCarrierID = new System.Windows.Forms.Label();
            this.txtPJID = new System.Windows.Forms.TextBox();
            this.gbMaterialType = new System.Windows.Forms.GroupBox();
            this.rbSubstrate = new System.Windows.Forms.RadioButton();
            this.rbCarriers = new System.Windows.Forms.RadioButton();
            this.lblPJID = new System.Windows.Forms.Label();
            this.btnCreatePJ = new System.Windows.Forms.Button();
            this.lblStep1 = new System.Windows.Forms.Label();
            this.dgvRecipe = new System.Windows.Forms.DataGridView();
            this.lblRecipe = new System.Windows.Forms.Label();
            this.dgvPJ = new System.Windows.Forms.DataGridView();
            this.lblPJList = new System.Windows.Forms.Label();
            this.groupBoxCJ = new System.Windows.Forms.GroupBox();
            this.panelCJInput = new System.Windows.Forms.Panel();
            this.cboProcessOrder = new System.Windows.Forms.ComboBox();
            this.lblProcessOrder = new System.Windows.Forms.Label();
            this.chkCJAutoStart = new System.Windows.Forms.CheckBox();
            this.btnRemovePJFromCJ = new System.Windows.Forms.Button();
            this.btnAddPJToCJ = new System.Windows.Forms.Button();
            this.lstSelectedPJ = new System.Windows.Forms.ListBox();
            this.lblSelectedPJ = new System.Windows.Forms.Label();
            this.txtCJID = new System.Windows.Forms.TextBox();
            this.lblCJID = new System.Windows.Forms.Label();
            this.btnCreateCJ = new System.Windows.Forms.Button();
            this.lblStep2 = new System.Windows.Forms.Label();
            this.dgvCJ = new System.Windows.Forms.DataGridView();
            this.lblCJList = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.rtbPJDetails = new System.Windows.Forms.RichTextBox();
            this.lblDetails = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxPJ.SuspendLayout();
            this.panelPJInput.SuspendLayout();
            this.gbMaterialType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecipe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPJ)).BeginInit();
            this.groupBoxCJ.SuspendLayout();
            this.panelCJInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCJ)).BeginInit();
            this.panelDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.splitContainer1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1200, 700);
            this.panelMain.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxPJ);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxCJ);
            this.splitContainer1.Panel2.Controls.Add(this.panelDetails);
            this.splitContainer1.Size = new System.Drawing.Size(1200, 700);
            this.splitContainer1.SplitterDistance = 580;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBoxPJ
            // 
            this.groupBoxPJ.Controls.Add(this.panelPJInput);
            this.groupBoxPJ.Controls.Add(this.dgvRecipe);
            this.groupBoxPJ.Controls.Add(this.lblRecipe);
            this.groupBoxPJ.Controls.Add(this.dgvPJ);
            this.groupBoxPJ.Controls.Add(this.lblPJList);
            this.groupBoxPJ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxPJ.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBoxPJ.Location = new System.Drawing.Point(0, 0);
            this.groupBoxPJ.Name = "groupBoxPJ";
            this.groupBoxPJ.Size = new System.Drawing.Size(580, 700);
            this.groupBoxPJ.TabIndex = 0;
            this.groupBoxPJ.TabStop = false;
            this.groupBoxPJ.Text = "Step 1: 建立製程作業 (Create PJ - SEMI E40)";
            // 
            // panelPJInput
            // 
            this.panelPJInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPJInput.Controls.Add(this.chkAutoStart);
            this.panelPJInput.Controls.Add(this.lblSelectedRecipe);
            this.panelPJInput.Controls.Add(this.txtCarrierID);
            this.panelPJInput.Controls.Add(this.lblCarrierID);
            this.panelPJInput.Controls.Add(this.txtPJID);
            this.panelPJInput.Controls.Add(this.gbMaterialType);
            this.panelPJInput.Controls.Add(this.lblPJID);
            this.panelPJInput.Controls.Add(this.btnCreatePJ);
            this.panelPJInput.Controls.Add(this.lblStep1);
            this.panelPJInput.Location = new System.Drawing.Point(10, 25);
            this.panelPJInput.Name = "panelPJInput";
            this.panelPJInput.Size = new System.Drawing.Size(560, 150);
            this.panelPJInput.TabIndex = 0;
            // 
            // chkAutoStart
            // 
            this.chkAutoStart.AutoSize = true;
            this.chkAutoStart.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.chkAutoStart.Location = new System.Drawing.Point(311, 70);
            this.chkAutoStart.Name = "chkAutoStart";
            this.chkAutoStart.Size = new System.Drawing.Size(109, 20);
            this.chkAutoStart.TabIndex = 7;
            this.chkAutoStart.Text = "PRProcessStart";
            this.chkAutoStart.UseVisualStyleBackColor = true;
            // 
            // lblSelectedRecipe
            // 
            this.lblSelectedRecipe.AutoSize = true;
            this.lblSelectedRecipe.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.lblSelectedRecipe.ForeColor = System.Drawing.Color.Blue;
            this.lblSelectedRecipe.Location = new System.Drawing.Point(10, 120);
            this.lblSelectedRecipe.Name = "lblSelectedRecipe";
            this.lblSelectedRecipe.Size = new System.Drawing.Size(132, 16);
            this.lblSelectedRecipe.TabIndex = 6;
            this.lblSelectedRecipe.Text = "已選擇: (請選擇Recipe)";
            // 
            // txtCarrierID
            // 
            this.txtCarrierID.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.txtCarrierID.Location = new System.Drawing.Point(100, 63);
            this.txtCarrierID.Name = "txtCarrierID";
            this.txtCarrierID.Size = new System.Drawing.Size(200, 23);
            this.txtCarrierID.TabIndex = 5;
            // 
            // lblCarrierID
            // 
            this.lblCarrierID.AutoSize = true;
            this.lblCarrierID.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.lblCarrierID.Location = new System.Drawing.Point(10, 66);
            this.lblCarrierID.Name = "lblCarrierID";
            this.lblCarrierID.Size = new System.Drawing.Size(59, 16);
            this.lblCarrierID.TabIndex = 4;
            this.lblCarrierID.Text = "CarrierID:";
            // 
            // txtPJID
            // 
            this.txtPJID.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.txtPJID.Location = new System.Drawing.Point(100, 33);
            this.txtPJID.Name = "txtPJID";
            this.txtPJID.Size = new System.Drawing.Size(200, 23);
            this.txtPJID.TabIndex = 3;
            // 
            // gbMaterialType
            // 
            this.gbMaterialType.Controls.Add(this.rbSubstrate);
            this.gbMaterialType.Controls.Add(this.rbCarriers);
            this.gbMaterialType.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            this.gbMaterialType.Location = new System.Drawing.Point(311, 8);
            this.gbMaterialType.Name = "gbMaterialType";
            this.gbMaterialType.Size = new System.Drawing.Size(229, 56);
            this.gbMaterialType.TabIndex = 10;
            this.gbMaterialType.TabStop = false;
            this.gbMaterialType.Text = "選擇材料類型 (Select Material Type)";
            // 
            // rbSubstrate
            // 
            this.rbSubstrate.AutoSize = true;
            this.rbSubstrate.Checked = true;
            this.rbSubstrate.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.rbSubstrate.Location = new System.Drawing.Point(6, 22);
            this.rbSubstrate.Name = "rbSubstrate";
            this.rbSubstrate.Size = new System.Drawing.Size(94, 20);
            this.rbSubstrate.TabIndex = 8;
            this.rbSubstrate.TabStop = true;
            this.rbSubstrate.Text = "基板 (Wafer)";
            this.rbSubstrate.UseVisualStyleBackColor = true;
            this.rbSubstrate.CheckedChanged += new System.EventHandler(this.rbMaterialType_CheckedChanged);
            // 
            // rbCarriers
            // 
            this.rbCarriers.AutoSize = true;
            this.rbCarriers.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.rbCarriers.Location = new System.Drawing.Point(106, 22);
            this.rbCarriers.Name = "rbCarriers";
            this.rbCarriers.Size = new System.Drawing.Size(97, 20);
            this.rbCarriers.TabIndex = 9;
            this.rbCarriers.Text = "載具 (Carrier)";
            this.rbCarriers.UseVisualStyleBackColor = true;
            this.rbCarriers.CheckedChanged += new System.EventHandler(this.rbMaterialType_CheckedChanged);
            // 
            // lblPJID
            // 
            this.lblPJID.AutoSize = true;
            this.lblPJID.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.lblPJID.Location = new System.Drawing.Point(10, 36);
            this.lblPJID.Name = "lblPJID";
            this.lblPJID.Size = new System.Drawing.Size(58, 16);
            this.lblPJID.TabIndex = 2;
            this.lblPJID.Text = "PRJobID:";
            // 
            // btnCreatePJ
            // 
            this.btnCreatePJ.BackColor = System.Drawing.Color.LightGreen;
            this.btnCreatePJ.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            this.btnCreatePJ.Location = new System.Drawing.Point(420, 100);
            this.btnCreatePJ.Name = "btnCreatePJ";
            this.btnCreatePJ.Size = new System.Drawing.Size(120, 35);
            this.btnCreatePJ.TabIndex = 1;
            this.btnCreatePJ.Text = "建立 PJ";
            this.btnCreatePJ.UseVisualStyleBackColor = false;
            this.btnCreatePJ.Click += new System.EventHandler(this.btnCreatePJ_Click);
            // 
            // lblStep1
            // 
            this.lblStep1.AutoSize = true;
            this.lblStep1.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.lblStep1.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblStep1.Location = new System.Drawing.Point(10, 8);
            this.lblStep1.Name = "lblStep1";
            this.lblStep1.Size = new System.Drawing.Size(295, 16);
            this.lblStep1.TabIndex = 0;
            this.lblStep1.Text = "1. 從下方選擇 Recipe → 2. 輸入資訊 → 3. 點擊建立 PJ";
            // 
            // dgvRecipe
            // 
            this.dgvRecipe.AllowUserToAddRows = false;
            this.dgvRecipe.AllowUserToDeleteRows = false;
            this.dgvRecipe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecipe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecipe.Location = new System.Drawing.Point(10, 200);
            this.dgvRecipe.MultiSelect = false;
            this.dgvRecipe.Name = "dgvRecipe";
            this.dgvRecipe.ReadOnly = true;
            this.dgvRecipe.RowHeadersVisible = false;
            this.dgvRecipe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecipe.Size = new System.Drawing.Size(250, 200);
            this.dgvRecipe.TabIndex = 2;
            this.dgvRecipe.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecipe_CellClick);
            // 
            // lblRecipe
            // 
            this.lblRecipe.AutoSize = true;
            this.lblRecipe.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            this.lblRecipe.Location = new System.Drawing.Point(10, 180);
            this.lblRecipe.Name = "lblRecipe";
            this.lblRecipe.Size = new System.Drawing.Size(115, 16);
            this.lblRecipe.TabIndex = 1;
            this.lblRecipe.Text = "Recipe清單 (RecID)";
            // 
            // dgvPJ
            // 
            this.dgvPJ.AllowUserToAddRows = false;
            this.dgvPJ.AllowUserToDeleteRows = false;
            this.dgvPJ.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPJ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPJ.Location = new System.Drawing.Point(10, 430);
            this.dgvPJ.MultiSelect = false;
            this.dgvPJ.Name = "dgvPJ";
            this.dgvPJ.ReadOnly = true;
            this.dgvPJ.RowHeadersVisible = false;
            this.dgvPJ.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPJ.Size = new System.Drawing.Size(560, 260);
            this.dgvPJ.TabIndex = 4;
            this.dgvPJ.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPJ_CellClick);
            // 
            // lblPJList
            // 
            this.lblPJList.AutoSize = true;
            this.lblPJList.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            this.lblPJList.Location = new System.Drawing.Point(10, 410);
            this.lblPJList.Name = "lblPJList";
            this.lblPJList.Size = new System.Drawing.Size(98, 16);
            this.lblPJList.TabIndex = 3;
            this.lblPJList.Text = "已建立的 PJ 清單";
            // 
            // groupBoxCJ
            // 
            this.groupBoxCJ.Controls.Add(this.panelCJInput);
            this.groupBoxCJ.Controls.Add(this.dgvCJ);
            this.groupBoxCJ.Controls.Add(this.lblCJList);
            this.groupBoxCJ.Controls.Add(this.btnRefresh);
            this.groupBoxCJ.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxCJ.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBoxCJ.Location = new System.Drawing.Point(0, 0);
            this.groupBoxCJ.Name = "groupBoxCJ";
            this.groupBoxCJ.Size = new System.Drawing.Size(616, 450);
            this.groupBoxCJ.TabIndex = 0;
            this.groupBoxCJ.TabStop = false;
            this.groupBoxCJ.Text = "Step 2: 建立控制作業 (Create CJ - SEMI E94)";
            // 
            // panelCJInput
            // 
            this.panelCJInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCJInput.Controls.Add(this.cboProcessOrder);
            this.panelCJInput.Controls.Add(this.lblProcessOrder);
            this.panelCJInput.Controls.Add(this.chkCJAutoStart);
            this.panelCJInput.Controls.Add(this.btnRemovePJFromCJ);
            this.panelCJInput.Controls.Add(this.btnAddPJToCJ);
            this.panelCJInput.Controls.Add(this.lstSelectedPJ);
            this.panelCJInput.Controls.Add(this.lblSelectedPJ);
            this.panelCJInput.Controls.Add(this.txtCJID);
            this.panelCJInput.Controls.Add(this.lblCJID);
            this.panelCJInput.Controls.Add(this.btnCreateCJ);
            this.panelCJInput.Controls.Add(this.lblStep2);
            this.panelCJInput.Location = new System.Drawing.Point(10, 25);
            this.panelCJInput.Name = "panelCJInput";
            this.panelCJInput.Size = new System.Drawing.Size(596, 220);
            this.panelCJInput.TabIndex = 0;
            // 
            // cboProcessOrder
            // 
            this.cboProcessOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProcessOrder.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.cboProcessOrder.FormattingEnabled = true;
            this.cboProcessOrder.Items.AddRange(new object[] {
            "LIST (按清單順序)",
            "ARRIVAL (按抵達順序)",
            "OPTIMIZE (優化順序)"});
            this.cboProcessOrder.Location = new System.Drawing.Point(350, 63);
            this.cboProcessOrder.Name = "cboProcessOrder";
            this.cboProcessOrder.Size = new System.Drawing.Size(180, 24);
            this.cboProcessOrder.TabIndex = 10;
            // 
            // lblProcessOrder
            // 
            this.lblProcessOrder.AutoSize = true;
            this.lblProcessOrder.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.lblProcessOrder.Location = new System.Drawing.Point(350, 43);
            this.lblProcessOrder.Name = "lblProcessOrder";
            this.lblProcessOrder.Size = new System.Drawing.Size(120, 16);
            this.lblProcessOrder.TabIndex = 9;
            this.lblProcessOrder.Text = "ProcessOrderMgmt:";
            // 
            // chkCJAutoStart
            // 
            this.chkCJAutoStart.AutoSize = true;
            this.chkCJAutoStart.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.chkCJAutoStart.Location = new System.Drawing.Point(350, 95);
            this.chkCJAutoStart.Name = "chkCJAutoStart";
            this.chkCJAutoStart.Size = new System.Drawing.Size(98, 20);
            this.chkCJAutoStart.TabIndex = 8;
            this.chkCJAutoStart.Text = "StartMethod";
            this.chkCJAutoStart.UseVisualStyleBackColor = true;
            // 
            // btnRemovePJFromCJ
            // 
            this.btnRemovePJFromCJ.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.btnRemovePJFromCJ.Location = new System.Drawing.Point(250, 140);
            this.btnRemovePJFromCJ.Name = "btnRemovePJFromCJ";
            this.btnRemovePJFromCJ.Size = new System.Drawing.Size(80, 28);
            this.btnRemovePJFromCJ.TabIndex = 7;
            this.btnRemovePJFromCJ.Text = "移除 ←";
            this.btnRemovePJFromCJ.UseVisualStyleBackColor = true;
            this.btnRemovePJFromCJ.Click += new System.EventHandler(this.btnRemovePJFromCJ_Click);
            // 
            // btnAddPJToCJ
            // 
            this.btnAddPJToCJ.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.btnAddPJToCJ.Location = new System.Drawing.Point(250, 105);
            this.btnAddPJToCJ.Name = "btnAddPJToCJ";
            this.btnAddPJToCJ.Size = new System.Drawing.Size(80, 28);
            this.btnAddPJToCJ.TabIndex = 6;
            this.btnAddPJToCJ.Text = "→ 加入";
            this.btnAddPJToCJ.UseVisualStyleBackColor = true;
            this.btnAddPJToCJ.Click += new System.EventHandler(this.btnAddPJToCJ_Click);
            // 
            // lstSelectedPJ
            // 
            this.lstSelectedPJ.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.lstSelectedPJ.FormattingEnabled = true;
            this.lstSelectedPJ.ItemHeight = 16;
            this.lstSelectedPJ.Location = new System.Drawing.Point(13, 80);
            this.lstSelectedPJ.Name = "lstSelectedPJ";
            this.lstSelectedPJ.Size = new System.Drawing.Size(220, 116);
            this.lstSelectedPJ.TabIndex = 5;
            // 
            // lblSelectedPJ
            // 
            this.lblSelectedPJ.AutoSize = true;
            this.lblSelectedPJ.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.lblSelectedPJ.Location = new System.Drawing.Point(10, 60);
            this.lblSelectedPJ.Name = "lblSelectedPJ";
            this.lblSelectedPJ.Size = new System.Drawing.Size(179, 16);
            this.lblSelectedPJ.TabIndex = 4;
            this.lblSelectedPJ.Text = "ProcessingCtrlSpec (選擇的 PJ):";
            // 
            // txtCJID
            // 
            this.txtCJID.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.txtCJID.Location = new System.Drawing.Point(100, 33);
            this.txtCJID.Name = "txtCJID";
            this.txtCJID.Size = new System.Drawing.Size(200, 23);
            this.txtCJID.TabIndex = 3;
            // 
            // lblCJID
            // 
            this.lblCJID.AutoSize = true;
            this.lblCJID.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.lblCJID.Location = new System.Drawing.Point(10, 36);
            this.lblCJID.Name = "lblCJID";
            this.lblCJID.Size = new System.Drawing.Size(38, 16);
            this.lblCJID.TabIndex = 2;
            this.lblCJID.Text = "CJ ID:";
            // 
            // btnCreateCJ
            // 
            this.btnCreateCJ.BackColor = System.Drawing.Color.LightBlue;
            this.btnCreateCJ.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            this.btnCreateCJ.Location = new System.Drawing.Point(460, 170);
            this.btnCreateCJ.Name = "btnCreateCJ";
            this.btnCreateCJ.Size = new System.Drawing.Size(120, 35);
            this.btnCreateCJ.TabIndex = 1;
            this.btnCreateCJ.Text = "建立 CJ";
            this.btnCreateCJ.UseVisualStyleBackColor = false;
            this.btnCreateCJ.Click += new System.EventHandler(this.btnCreateCJ_Click);
            // 
            // lblStep2
            // 
            this.lblStep2.AutoSize = true;
            this.lblStep2.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.lblStep2.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblStep2.Location = new System.Drawing.Point(10, 8);
            this.lblStep2.Name = "lblStep2";
            this.lblStep2.Size = new System.Drawing.Size(300, 16);
            this.lblStep2.TabIndex = 0;
            this.lblStep2.Text = "1. 從左側 PJ 清單選擇 → 2. 加入此 CJ → 3. 點擊建立 CJ";
            // 
            // dgvCJ
            // 
            this.dgvCJ.AllowUserToAddRows = false;
            this.dgvCJ.AllowUserToDeleteRows = false;
            this.dgvCJ.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCJ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCJ.Location = new System.Drawing.Point(10, 280);
            this.dgvCJ.MultiSelect = false;
            this.dgvCJ.Name = "dgvCJ";
            this.dgvCJ.ReadOnly = true;
            this.dgvCJ.RowHeadersVisible = false;
            this.dgvCJ.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCJ.Size = new System.Drawing.Size(596, 160);
            this.dgvCJ.TabIndex = 2;
            // 
            // lblCJList
            // 
            this.lblCJList.AutoSize = true;
            this.lblCJList.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Bold);
            this.lblCJList.Location = new System.Drawing.Point(10, 260);
            this.lblCJList.Name = "lblCJList";
            this.lblCJList.Size = new System.Drawing.Size(98, 16);
            this.lblCJList.TabIndex = 1;
            this.lblCJList.Text = "已建立的 CJ 清單";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("微軟正黑體", 9F);
            this.btnRefresh.Location = new System.Drawing.Point(520, 250);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(86, 28);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panelDetails
            // 
            this.panelDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetails.Controls.Add(this.rtbPJDetails);
            this.panelDetails.Controls.Add(this.lblDetails);
            this.panelDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelDetails.Location = new System.Drawing.Point(0, 450);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(616, 250);
            this.panelDetails.TabIndex = 1;
            // 
            // rtbPJDetails
            // 
            this.rtbPJDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbPJDetails.Font = new System.Drawing.Font("Consolas", 9F);
            this.rtbPJDetails.Location = new System.Drawing.Point(0, 17);
            this.rtbPJDetails.Name = "rtbPJDetails";
            this.rtbPJDetails.ReadOnly = true;
            this.rtbPJDetails.Size = new System.Drawing.Size(614, 231);
            this.rtbPJDetails.TabIndex = 1;
            this.rtbPJDetails.Text = "";
            // 
            // lblDetails
            // 
            this.lblDetails.AutoSize = true;
            this.lblDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetails.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblDetails.Location = new System.Drawing.Point(0, 0);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(76, 17);
            this.lblDetails.TabIndex = 0;
            this.lblDetails.Text = "PJ 詳細資訊";
            // 
            // CreatPjCJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelMain);
            this.Name = "CreatPjCJ";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "建立 PJ / CJ (E40 / E94)";
            this.Load += new System.EventHandler(this.CreatPjCJ_Load);
            this.panelMain.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxPJ.ResumeLayout(false);
            this.groupBoxPJ.PerformLayout();
            this.panelPJInput.ResumeLayout(false);
            this.panelPJInput.PerformLayout();
            this.gbMaterialType.ResumeLayout(false);
            this.gbMaterialType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecipe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPJ)).EndInit();
            this.groupBoxCJ.ResumeLayout(false);
            this.groupBoxCJ.PerformLayout();
            this.panelCJInput.ResumeLayout(false);
            this.panelCJInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCJ)).EndInit();
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBoxPJ;
        private System.Windows.Forms.Panel panelPJInput;
        private System.Windows.Forms.Label lblStep1;
        private System.Windows.Forms.Button btnCreatePJ;
        private System.Windows.Forms.TextBox txtPJID;
        private System.Windows.Forms.Label lblPJID;
        private System.Windows.Forms.TextBox txtCarrierID;
        private System.Windows.Forms.Label lblCarrierID;
        private System.Windows.Forms.CheckBox chkAutoStart;
        private System.Windows.Forms.Label lblSelectedRecipe;
        private System.Windows.Forms.DataGridView dgvRecipe;
        private System.Windows.Forms.Label lblRecipe;
        private System.Windows.Forms.DataGridView dgvPJ;
        private System.Windows.Forms.Label lblPJList;
        private System.Windows.Forms.GroupBox groupBoxCJ;
        private System.Windows.Forms.Panel panelCJInput;
        private System.Windows.Forms.Label lblStep2;
        private System.Windows.Forms.Button btnCreateCJ;
        private System.Windows.Forms.TextBox txtCJID;
        private System.Windows.Forms.Label lblCJID;
        private System.Windows.Forms.ListBox lstSelectedPJ;
        private System.Windows.Forms.Label lblSelectedPJ;
        private System.Windows.Forms.Button btnAddPJToCJ;
        private System.Windows.Forms.Button btnRemovePJFromCJ;
        private System.Windows.Forms.CheckBox chkCJAutoStart;
        private System.Windows.Forms.ComboBox cboProcessOrder;
        private System.Windows.Forms.Label lblProcessOrder;
        private System.Windows.Forms.DataGridView dgvCJ;
        private System.Windows.Forms.Label lblCJList;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.RichTextBox rtbPJDetails;
        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.RadioButton rbSubstrate;
        private System.Windows.Forms.RadioButton rbCarriers;
        private System.Windows.Forms.GroupBox gbMaterialType;
    }
}
