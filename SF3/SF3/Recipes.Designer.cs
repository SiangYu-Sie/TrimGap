
using static System.Net.Mime.MediaTypeNames;

namespace SF3_Form
{
    partial class Recipes
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recipes));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lv_Recipes = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_New = new System.Windows.Forms.Button();
            this.lab_OK = new System.Windows.Forms.Label();
            this.lab_Cancel = new System.Windows.Forms.Label();
            this.lab_New = new System.Windows.Forms.Label();
            this.lab_Delete = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lab_RecipeID = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_RecipeID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lab_Name = new System.Windows.Forms.Label();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.lab_Memo = new System.Windows.Forms.Label();
            this.txt_Memo = new System.Windows.Forms.TextBox();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rad_GetAnalysisSpectrum_Yes = new System.Windows.Forms.RadioButton();
            this.rad_GetAnalysisSpectrum_No = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rad_GetReflectanceSpectrum_Yes = new System.Windows.Forms.RadioButton();
            this.rad_GetReflectanceSpectrum_No = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rad_GetSignalSpectrum_Yes = new System.Windows.Forms.RadioButton();
            this.rad_GetSignalSpectrum_No = new System.Windows.Forms.RadioButton();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lab_GetAnalysisSpectrum = new System.Windows.Forms.Label();
            this.lab_GetReflectanceSpectrum = new System.Windows.Forms.Label();
            this.lab_GetSignalSpectrum = new System.Windows.Forms.Label();
            this.num_MeasurementCycle = new System.Windows.Forms.NumericUpDown();
            this.lab_MeasurementCycle = new System.Windows.Forms.Label();
            this.num_NumberOfContinuous_measurements = new System.Windows.Forms.NumericUpDown();
            this.lab_Number_of_continuous_measurements = new System.Windows.Forms.Label();
            this.cb_MeasurementMode = new System.Windows.Forms.ComboBox();
            this.lab_MeasurementMode = new System.Windows.Forms.Label();
            this.lab_ResetElapsedTimeOnTriggerInput = new System.Windows.Forms.Label();
            this.num_TriggerDelayTime = new System.Windows.Forms.NumericUpDown();
            this.lab_TriggerDelayTime = new System.Windows.Forms.Label();
            this.btn_StopMonitoring = new System.Windows.Forms.Button();
            this.btn_StartMonitoring = new System.Windows.Forms.Button();
            this.num_NumberOfAccumulations = new System.Windows.Forms.NumericUpDown();
            this.lab_Number_of_accumulations = new System.Windows.Forms.Label();
            this.num_ExposureTime = new System.Windows.Forms.NumericUpDown();
            this.lab_ExposureTime = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rad_ResetElapsedTimeOnTriggerInput_No = new System.Windows.Forms.RadioButton();
            this.rad_ResetElapsedTimeOnTriggerInput_Yes = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.subTabControl = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.num_ThicknessMoveingAverage = new System.Windows.Forms.NumericUpDown();
            this.lab_ThicknessMoveingAverage = new System.Windows.Forms.Label();
            this.cb_AnalysisLayerMaterial = new System.Windows.Forms.ComboBox();
            this.lab_AnalysisLayerMaterial = new System.Windows.Forms.Label();
            this.dataGridView_Mainsetting = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.num_NumOfAnalysisLayers = new System.Windows.Forms.NumericUpDown();
            this.lab_NumOfAmalysisLayers = new System.Windows.Forms.Label();
            this.num_FFTMaximumThickness = new System.Windows.Forms.NumericUpDown();
            this.lab_FFTMaximumThickness = new System.Windows.Forms.Label();
            this.cb_NumOfFFTDataPoints = new System.Windows.Forms.ComboBox();
            this.lab_NumOfFFTDataPoints = new System.Windows.Forms.Label();
            this.cb_AnalysisMethod = new System.Windows.Forms.ComboBox();
            this.lab_AnalysisMethod = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.num_AnalysisWavelengthNumber_Upper = new System.Windows.Forms.NumericUpDown();
            this.num_AnalysisWavelengthNumber_Lower = new System.Windows.Forms.NumericUpDown();
            this.lab_AnalysisWavelengthNumber = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dataGridView_DetailSetting = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lab_ThicknessCoefficient = new System.Windows.Forms.Label();
            this.num_NumOfErrorForNaN = new System.Windows.Forms.NumericUpDown();
            this.lab_NumOfErrorForNaN = new System.Windows.Forms.Label();
            this.cb_ErrorValue = new System.Windows.Forms.ComboBox();
            this.lab_ErrorValue = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_ThicknessVariationTrend = new System.Windows.Forms.ComboBox();
            this.lab_ThicknessVariationTrend = new System.Windows.Forms.Label();
            this.num_FollowupFilterCenterValueMovingAverage = new System.Windows.Forms.NumericUpDown();
            this.lab_FollowupFilterCenterValueMovingAverage = new System.Windows.Forms.Label();
            this.num_FollowupFilterApplicationTime = new System.Windows.Forms.NumericUpDown();
            this.lab_FollowupFilterApplicationTime = new System.Windows.Forms.Label();
            this.num_FollowupFilterRange = new System.Windows.Forms.NumericUpDown();
            this.lab_FollowupFilterRange = new System.Windows.Forms.Label();
            this.checkBox_NGPeakExclusion = new System.Windows.Forms.CheckBox();
            this.checkBox_FFTNormalization = new System.Windows.Forms.CheckBox();
            this.checkBox_FFTWindowFunction = new System.Windows.Forms.CheckBox();
            this.cb_SmoothingPoint = new System.Windows.Forms.ComboBox();
            this.lab_SmoothingPoint = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column11 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cb_AmbientLayerMaterial = new System.Windows.Forms.ComboBox();
            this.lab_AmbientLayerMaterial = new System.Windows.Forms.Label();
            this.num_OptimizationMethodThicknessStepValue = new System.Windows.Forms.NumericUpDown();
            this.lab_OptimizationMethodThicknessStepValue = new System.Windows.Forms.Label();
            this.num_OptimizationMethodSwitchoverThickness = new System.Windows.Forms.NumericUpDown();
            this.lab_OptimizationMethodSwitchoverThickness = new System.Windows.Forms.Label();
            this.lab_Edit = new System.Windows.Forms.Label();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.bW_chart = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btn_Save = new System.Windows.Forms.Button();
            this.mainTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_MeasurementCycle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumberOfContinuous_measurements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TriggerDelayTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumberOfAccumulations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_ExposureTime)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.subTabControl.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_ThicknessMoveingAverage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Mainsetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumOfAnalysisLayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_FFTMaximumThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_AnalysisWavelengthNumber_Upper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_AnalysisWavelengthNumber_Lower)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_DetailSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumOfErrorForNaN)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_FollowupFilterCenterValueMovingAverage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_FollowupFilterApplicationTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_FollowupFilterRange)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_OptimizationMethodThicknessStepValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_OptimizationMethodSwitchoverThickness)).BeginInit();
            this.SuspendLayout();
            // 
            // lv_Recipes
            // 
            this.lv_Recipes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lv_Recipes.HideSelection = false;
            this.lv_Recipes.Location = new System.Drawing.Point(104, 12);
            this.lv_Recipes.Name = "lv_Recipes";
            this.lv_Recipes.Size = new System.Drawing.Size(1092, 157);
            this.lv_Recipes.TabIndex = 0;
            this.lv_Recipes.UseCompatibleStateImageBehavior = false;
            this.lv_Recipes.SelectedIndexChanged += new System.EventHandler(this.lv_Recipes_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Memo";
            // 
            // btn_OK
            // 
            this.btn_OK.BackColor = System.Drawing.Color.Transparent;
            this.btn_OK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_OK.BackgroundImage")));
            this.btn_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_OK.Location = new System.Drawing.Point(12, 28);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(70, 70);
            this.btn_OK.TabIndex = 1;
            this.btn_OK.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_OK.UseVisualStyleBackColor = false;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.Transparent;
            this.btn_Cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.BackgroundImage")));
            this.btn_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Cancel.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold);
            this.btn_Cancel.Location = new System.Drawing.Point(12, 142);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(70, 70);
            this.btn_Cancel.TabIndex = 7;
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_New
            // 
            this.btn_New.BackColor = System.Drawing.Color.Transparent;
            this.btn_New.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_New.BackgroundImage")));
            this.btn_New.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_New.Location = new System.Drawing.Point(12, 261);
            this.btn_New.Name = "btn_New";
            this.btn_New.Size = new System.Drawing.Size(70, 70);
            this.btn_New.TabIndex = 8;
            this.btn_New.UseVisualStyleBackColor = false;
            this.btn_New.Click += new System.EventHandler(this.btn_New_Click);
            // 
            // lab_OK
            // 
            this.lab_OK.AutoSize = true;
            this.lab_OK.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_OK.Location = new System.Drawing.Point(32, 101);
            this.lab_OK.Name = "lab_OK";
            this.lab_OK.Size = new System.Drawing.Size(31, 19);
            this.lab_OK.TabIndex = 11;
            this.lab_OK.Text = "OK";
            // 
            // lab_Cancel
            // 
            this.lab_Cancel.AutoSize = true;
            this.lab_Cancel.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold);
            this.lab_Cancel.Location = new System.Drawing.Point(20, 215);
            this.lab_Cancel.Name = "lab_Cancel";
            this.lab_Cancel.Size = new System.Drawing.Size(57, 19);
            this.lab_Cancel.TabIndex = 12;
            this.lab_Cancel.Text = "Cancel";
            // 
            // lab_New
            // 
            this.lab_New.AutoSize = true;
            this.lab_New.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold);
            this.lab_New.Location = new System.Drawing.Point(27, 334);
            this.lab_New.Name = "lab_New";
            this.lab_New.Size = new System.Drawing.Size(42, 19);
            this.lab_New.TabIndex = 13;
            this.lab_New.Text = "New";
            // 
            // lab_Delete
            // 
            this.lab_Delete.AutoSize = true;
            this.lab_Delete.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold);
            this.lab_Delete.Location = new System.Drawing.Point(26, 574);
            this.lab_Delete.Name = "lab_Delete";
            this.lab_Delete.Size = new System.Drawing.Size(43, 19);
            this.lab_Delete.TabIndex = 15;
            this.lab_Delete.Text = "Save";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(8, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 2);
            this.label1.TabIndex = 16;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(8, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 2);
            this.label2.TabIndex = 17;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(8, 364);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 2);
            this.label3.TabIndex = 18;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(8, 483);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 2);
            this.label4.TabIndex = 19;
            this.label4.Text = "label4";
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Location = new System.Drawing.Point(8, 604);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 2);
            this.label5.TabIndex = 20;
            this.label5.Text = "label5";
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(8, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 2);
            this.label6.TabIndex = 21;
            this.label6.Text = "label6";
            // 
            // lab_RecipeID
            // 
            this.lab_RecipeID.AutoSize = true;
            this.lab_RecipeID.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_RecipeID.Location = new System.Drawing.Point(109, 182);
            this.lab_RecipeID.Name = "lab_RecipeID";
            this.lab_RecipeID.Size = new System.Drawing.Size(98, 24);
            this.lab_RecipeID.TabIndex = 22;
            this.lab_RecipeID.Text = "Recipe ID";
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Location = new System.Drawing.Point(104, 179);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(2, 33);
            this.label7.TabIndex = 23;
            this.label7.Text = "label7";
            // 
            // txt_RecipeID
            // 
            this.txt_RecipeID.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.txt_RecipeID.Location = new System.Drawing.Point(207, 178);
            this.txt_RecipeID.Name = "txt_RecipeID";
            this.txt_RecipeID.Size = new System.Drawing.Size(124, 33);
            this.txt_RecipeID.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(339, 179);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(2, 33);
            this.label8.TabIndex = 25;
            this.label8.Text = "label8";
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Location = new System.Drawing.Point(1195, 179);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(2, 33);
            this.label9.TabIndex = 26;
            this.label9.Text = "label9";
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.Location = new System.Drawing.Point(708, 179);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(2, 33);
            this.label10.TabIndex = 27;
            this.label10.Text = "label10";
            // 
            // lab_Name
            // 
            this.lab_Name.AutoSize = true;
            this.lab_Name.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_Name.Location = new System.Drawing.Point(345, 182);
            this.lab_Name.Name = "lab_Name";
            this.lab_Name.Size = new System.Drawing.Size(66, 24);
            this.lab_Name.TabIndex = 28;
            this.lab_Name.Text = "Name";
            // 
            // txt_Name
            // 
            this.txt_Name.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.txt_Name.Location = new System.Drawing.Point(409, 178);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(293, 33);
            this.txt_Name.TabIndex = 29;
            this.txt_Name.TextChanged += new System.EventHandler(this.txt_Name_TextChanged);
            // 
            // lab_Memo
            // 
            this.lab_Memo.AutoSize = true;
            this.lab_Memo.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lab_Memo.Location = new System.Drawing.Point(715, 182);
            this.lab_Memo.Name = "lab_Memo";
            this.lab_Memo.Size = new System.Drawing.Size(70, 24);
            this.lab_Memo.TabIndex = 30;
            this.lab_Memo.Text = "Memo";
            // 
            // txt_Memo
            // 
            this.txt_Memo.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Bold);
            this.txt_Memo.Location = new System.Drawing.Point(782, 178);
            this.txt_Memo.Name = "txt_Memo";
            this.txt_Memo.Size = new System.Drawing.Size(407, 33);
            this.txt_Memo.TabIndex = 31;
            this.txt_Memo.TextChanged += new System.EventHandler(this.txt_Memo_TextChanged);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.tabPage1);
            this.mainTabControl.Controls.Add(this.tabPage2);
            this.mainTabControl.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.mainTabControl.Location = new System.Drawing.Point(104, 224);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1093, 722);
            this.mainTabControl.TabIndex = 32;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.chart1);
            this.tabPage1.Controls.Add(this.lab_GetAnalysisSpectrum);
            this.tabPage1.Controls.Add(this.lab_GetReflectanceSpectrum);
            this.tabPage1.Controls.Add(this.lab_GetSignalSpectrum);
            this.tabPage1.Controls.Add(this.num_MeasurementCycle);
            this.tabPage1.Controls.Add(this.lab_MeasurementCycle);
            this.tabPage1.Controls.Add(this.num_NumberOfContinuous_measurements);
            this.tabPage1.Controls.Add(this.lab_Number_of_continuous_measurements);
            this.tabPage1.Controls.Add(this.cb_MeasurementMode);
            this.tabPage1.Controls.Add(this.lab_MeasurementMode);
            this.tabPage1.Controls.Add(this.lab_ResetElapsedTimeOnTriggerInput);
            this.tabPage1.Controls.Add(this.num_TriggerDelayTime);
            this.tabPage1.Controls.Add(this.lab_TriggerDelayTime);
            this.tabPage1.Controls.Add(this.btn_StopMonitoring);
            this.tabPage1.Controls.Add(this.btn_StartMonitoring);
            this.tabPage1.Controls.Add(this.num_NumberOfAccumulations);
            this.tabPage1.Controls.Add(this.lab_Number_of_accumulations);
            this.tabPage1.Controls.Add(this.num_ExposureTime);
            this.tabPage1.Controls.Add(this.lab_ExposureTime);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1085, 692);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Measurement condition";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rad_GetAnalysisSpectrum_Yes);
            this.groupBox5.Controls.Add(this.rad_GetAnalysisSpectrum_No);
            this.groupBox5.Location = new System.Drawing.Point(257, 368);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(127, 30);
            this.groupBox5.TabIndex = 30;
            this.groupBox5.TabStop = false;
            // 
            // rad_GetAnalysisSpectrum_Yes
            // 
            this.rad_GetAnalysisSpectrum_Yes.AutoSize = true;
            this.rad_GetAnalysisSpectrum_Yes.Location = new System.Drawing.Point(7, 10);
            this.rad_GetAnalysisSpectrum_Yes.Name = "rad_GetAnalysisSpectrum_Yes";
            this.rad_GetAnalysisSpectrum_Yes.Size = new System.Drawing.Size(49, 20);
            this.rad_GetAnalysisSpectrum_Yes.TabIndex = 25;
            this.rad_GetAnalysisSpectrum_Yes.Text = "Yes";
            this.rad_GetAnalysisSpectrum_Yes.UseVisualStyleBackColor = true;
            // 
            // rad_GetAnalysisSpectrum_No
            // 
            this.rad_GetAnalysisSpectrum_No.AutoSize = true;
            this.rad_GetAnalysisSpectrum_No.Checked = true;
            this.rad_GetAnalysisSpectrum_No.Location = new System.Drawing.Point(73, 10);
            this.rad_GetAnalysisSpectrum_No.Name = "rad_GetAnalysisSpectrum_No";
            this.rad_GetAnalysisSpectrum_No.Size = new System.Drawing.Size(44, 20);
            this.rad_GetAnalysisSpectrum_No.TabIndex = 26;
            this.rad_GetAnalysisSpectrum_No.TabStop = true;
            this.rad_GetAnalysisSpectrum_No.Text = "No";
            this.rad_GetAnalysisSpectrum_No.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rad_GetReflectanceSpectrum_Yes);
            this.groupBox4.Controls.Add(this.rad_GetReflectanceSpectrum_No);
            this.groupBox4.Location = new System.Drawing.Point(257, 338);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(127, 30);
            this.groupBox4.TabIndex = 29;
            this.groupBox4.TabStop = false;
            // 
            // rad_GetReflectanceSpectrum_Yes
            // 
            this.rad_GetReflectanceSpectrum_Yes.AutoSize = true;
            this.rad_GetReflectanceSpectrum_Yes.Location = new System.Drawing.Point(7, 10);
            this.rad_GetReflectanceSpectrum_Yes.Name = "rad_GetReflectanceSpectrum_Yes";
            this.rad_GetReflectanceSpectrum_Yes.Size = new System.Drawing.Size(49, 20);
            this.rad_GetReflectanceSpectrum_Yes.TabIndex = 22;
            this.rad_GetReflectanceSpectrum_Yes.Text = "Yes";
            this.rad_GetReflectanceSpectrum_Yes.UseVisualStyleBackColor = true;
            // 
            // rad_GetReflectanceSpectrum_No
            // 
            this.rad_GetReflectanceSpectrum_No.AutoSize = true;
            this.rad_GetReflectanceSpectrum_No.Checked = true;
            this.rad_GetReflectanceSpectrum_No.Location = new System.Drawing.Point(73, 10);
            this.rad_GetReflectanceSpectrum_No.Name = "rad_GetReflectanceSpectrum_No";
            this.rad_GetReflectanceSpectrum_No.Size = new System.Drawing.Size(44, 20);
            this.rad_GetReflectanceSpectrum_No.TabIndex = 23;
            this.rad_GetReflectanceSpectrum_No.TabStop = true;
            this.rad_GetReflectanceSpectrum_No.Text = "No";
            this.rad_GetReflectanceSpectrum_No.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rad_GetSignalSpectrum_Yes);
            this.groupBox3.Controls.Add(this.rad_GetSignalSpectrum_No);
            this.groupBox3.Location = new System.Drawing.Point(257, 308);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(127, 30);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            // 
            // rad_GetSignalSpectrum_Yes
            // 
            this.rad_GetSignalSpectrum_Yes.AutoSize = true;
            this.rad_GetSignalSpectrum_Yes.Location = new System.Drawing.Point(7, 10);
            this.rad_GetSignalSpectrum_Yes.Name = "rad_GetSignalSpectrum_Yes";
            this.rad_GetSignalSpectrum_Yes.Size = new System.Drawing.Size(49, 20);
            this.rad_GetSignalSpectrum_Yes.TabIndex = 19;
            this.rad_GetSignalSpectrum_Yes.Text = "Yes";
            this.rad_GetSignalSpectrum_Yes.UseVisualStyleBackColor = true;
            // 
            // rad_GetSignalSpectrum_No
            // 
            this.rad_GetSignalSpectrum_No.AutoSize = true;
            this.rad_GetSignalSpectrum_No.Checked = true;
            this.rad_GetSignalSpectrum_No.Location = new System.Drawing.Point(73, 10);
            this.rad_GetSignalSpectrum_No.Name = "rad_GetSignalSpectrum_No";
            this.rad_GetSignalSpectrum_No.Size = new System.Drawing.Size(44, 20);
            this.rad_GetSignalSpectrum_No.TabIndex = 20;
            this.rad_GetSignalSpectrum_No.TabStop = true;
            this.rad_GetSignalSpectrum_No.Text = "No";
            this.rad_GetSignalSpectrum_No.UseVisualStyleBackColor = true;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(412, 6);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(667, 680);
            this.chart1.TabIndex = 27;
            this.chart1.Text = "chart1";
            title1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Top;
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title1";
            title1.Text = "Signal montior";
            this.chart1.Titles.Add(title1);
            // 
            // lab_GetAnalysisSpectrum
            // 
            this.lab_GetAnalysisSpectrum.AutoSize = true;
            this.lab_GetAnalysisSpectrum.Location = new System.Drawing.Point(6, 381);
            this.lab_GetAnalysisSpectrum.Name = "lab_GetAnalysisSpectrum";
            this.lab_GetAnalysisSpectrum.Size = new System.Drawing.Size(144, 16);
            this.lab_GetAnalysisSpectrum.TabIndex = 24;
            this.lab_GetAnalysisSpectrum.Text = "Get analysis spectrum";
            // 
            // lab_GetReflectanceSpectrum
            // 
            this.lab_GetReflectanceSpectrum.AutoSize = true;
            this.lab_GetReflectanceSpectrum.Location = new System.Drawing.Point(6, 351);
            this.lab_GetReflectanceSpectrum.Name = "lab_GetReflectanceSpectrum";
            this.lab_GetReflectanceSpectrum.Size = new System.Drawing.Size(162, 16);
            this.lab_GetReflectanceSpectrum.TabIndex = 21;
            this.lab_GetReflectanceSpectrum.Text = "Get reflectance spectrum";
            // 
            // lab_GetSignalSpectrum
            // 
            this.lab_GetSignalSpectrum.AutoSize = true;
            this.lab_GetSignalSpectrum.Location = new System.Drawing.Point(6, 321);
            this.lab_GetSignalSpectrum.Name = "lab_GetSignalSpectrum";
            this.lab_GetSignalSpectrum.Size = new System.Drawing.Size(131, 16);
            this.lab_GetSignalSpectrum.TabIndex = 18;
            this.lab_GetSignalSpectrum.Text = "Get signal spectrum";
            // 
            // num_MeasurementCycle
            // 
            this.num_MeasurementCycle.Location = new System.Drawing.Point(226, 280);
            this.num_MeasurementCycle.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.num_MeasurementCycle.Minimum = new decimal(new int[] {
            75,
            0,
            0,
            0});
            this.num_MeasurementCycle.Name = "num_MeasurementCycle";
            this.num_MeasurementCycle.Size = new System.Drawing.Size(180, 27);
            this.num_MeasurementCycle.TabIndex = 17;
            this.num_MeasurementCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num_MeasurementCycle.Value = new decimal(new int[] {
            75,
            0,
            0,
            0});
            // 
            // lab_MeasurementCycle
            // 
            this.lab_MeasurementCycle.AutoSize = true;
            this.lab_MeasurementCycle.Location = new System.Drawing.Point(6, 284);
            this.lab_MeasurementCycle.Name = "lab_MeasurementCycle";
            this.lab_MeasurementCycle.Size = new System.Drawing.Size(156, 16);
            this.lab_MeasurementCycle.TabIndex = 16;
            this.lab_MeasurementCycle.Text = "Measurement cycle [us]";
            // 
            // num_NumberOfContinuous_measurements
            // 
            this.num_NumberOfContinuous_measurements.Location = new System.Drawing.Point(226, 247);
            this.num_NumberOfContinuous_measurements.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.num_NumberOfContinuous_measurements.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_NumberOfContinuous_measurements.Name = "num_NumberOfContinuous_measurements";
            this.num_NumberOfContinuous_measurements.Size = new System.Drawing.Size(180, 27);
            this.num_NumberOfContinuous_measurements.TabIndex = 15;
            this.num_NumberOfContinuous_measurements.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num_NumberOfContinuous_measurements.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lab_Number_of_continuous_measurements
            // 
            this.lab_Number_of_continuous_measurements.AutoSize = true;
            this.lab_Number_of_continuous_measurements.Location = new System.Drawing.Point(6, 250);
            this.lab_Number_of_continuous_measurements.Name = "lab_Number_of_continuous_measurements";
            this.lab_Number_of_continuous_measurements.Size = new System.Drawing.Size(213, 16);
            this.lab_Number_of_continuous_measurements.TabIndex = 14;
            this.lab_Number_of_continuous_measurements.Text = "No. of continuous measurements";
            // 
            // cb_MeasurementMode
            // 
            this.cb_MeasurementMode.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cb_MeasurementMode.FormattingEnabled = true;
            this.cb_MeasurementMode.Items.AddRange(new object[] {
            "Single",
            "Continuous",
            "Trigger"});
            this.cb_MeasurementMode.Location = new System.Drawing.Point(226, 213);
            this.cb_MeasurementMode.Name = "cb_MeasurementMode";
            this.cb_MeasurementMode.Size = new System.Drawing.Size(180, 27);
            this.cb_MeasurementMode.TabIndex = 13;
            this.cb_MeasurementMode.Text = "Single";
            // 
            // lab_MeasurementMode
            // 
            this.lab_MeasurementMode.AutoSize = true;
            this.lab_MeasurementMode.Location = new System.Drawing.Point(6, 218);
            this.lab_MeasurementMode.Name = "lab_MeasurementMode";
            this.lab_MeasurementMode.Size = new System.Drawing.Size(130, 16);
            this.lab_MeasurementMode.TabIndex = 12;
            this.lab_MeasurementMode.Text = "Measurement mode";
            // 
            // lab_ResetElapsedTimeOnTriggerInput
            // 
            this.lab_ResetElapsedTimeOnTriggerInput.AutoSize = true;
            this.lab_ResetElapsedTimeOnTriggerInput.Location = new System.Drawing.Point(6, 185);
            this.lab_ResetElapsedTimeOnTriggerInput.Name = "lab_ResetElapsedTimeOnTriggerInput";
            this.lab_ResetElapsedTimeOnTriggerInput.Size = new System.Drawing.Size(224, 16);
            this.lab_ResetElapsedTimeOnTriggerInput.TabIndex = 8;
            this.lab_ResetElapsedTimeOnTriggerInput.Text = "Reset elapsed time on trigger input";
            // 
            // num_TriggerDelayTime
            // 
            this.num_TriggerDelayTime.Location = new System.Drawing.Point(226, 145);
            this.num_TriggerDelayTime.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.num_TriggerDelayTime.Name = "num_TriggerDelayTime";
            this.num_TriggerDelayTime.Size = new System.Drawing.Size(180, 27);
            this.num_TriggerDelayTime.TabIndex = 7;
            this.num_TriggerDelayTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lab_TriggerDelayTime
            // 
            this.lab_TriggerDelayTime.AutoSize = true;
            this.lab_TriggerDelayTime.Location = new System.Drawing.Point(6, 149);
            this.lab_TriggerDelayTime.Name = "lab_TriggerDelayTime";
            this.lab_TriggerDelayTime.Size = new System.Drawing.Size(154, 16);
            this.lab_TriggerDelayTime.TabIndex = 6;
            this.lab_TriggerDelayTime.Text = "Trigger delay time [ms]";
            // 
            // btn_StopMonitoring
            // 
            this.btn_StopMonitoring.Location = new System.Drawing.Point(209, 77);
            this.btn_StopMonitoring.Name = "btn_StopMonitoring";
            this.btn_StopMonitoring.Size = new System.Drawing.Size(197, 59);
            this.btn_StopMonitoring.TabIndex = 5;
            this.btn_StopMonitoring.Text = "Stop monitoring";
            this.btn_StopMonitoring.UseVisualStyleBackColor = true;
            this.btn_StopMonitoring.Click += new System.EventHandler(this.btn_StopMonitoring_Click);
            // 
            // btn_StartMonitoring
            // 
            this.btn_StartMonitoring.Location = new System.Drawing.Point(9, 77);
            this.btn_StartMonitoring.Name = "btn_StartMonitoring";
            this.btn_StartMonitoring.Size = new System.Drawing.Size(197, 59);
            this.btn_StartMonitoring.TabIndex = 4;
            this.btn_StartMonitoring.Text = "Start monitoring";
            this.btn_StartMonitoring.UseVisualStyleBackColor = true;
            this.btn_StartMonitoring.Click += new System.EventHandler(this.btn_StartMonitoring_Click);
            // 
            // num_NumberOfAccumulations
            // 
            this.num_NumberOfAccumulations.Location = new System.Drawing.Point(226, 41);
            this.num_NumberOfAccumulations.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.num_NumberOfAccumulations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_NumberOfAccumulations.Name = "num_NumberOfAccumulations";
            this.num_NumberOfAccumulations.Size = new System.Drawing.Size(180, 27);
            this.num_NumberOfAccumulations.TabIndex = 3;
            this.num_NumberOfAccumulations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num_NumberOfAccumulations.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lab_Number_of_accumulations
            // 
            this.lab_Number_of_accumulations.AutoSize = true;
            this.lab_Number_of_accumulations.Location = new System.Drawing.Point(6, 44);
            this.lab_Number_of_accumulations.Name = "lab_Number_of_accumulations";
            this.lab_Number_of_accumulations.Size = new System.Drawing.Size(188, 16);
            this.lab_Number_of_accumulations.TabIndex = 2;
            this.lab_Number_of_accumulations.Text = "No. of accumulations [times]";
            // 
            // num_ExposureTime
            // 
            this.num_ExposureTime.Location = new System.Drawing.Point(226, 8);
            this.num_ExposureTime.Maximum = new decimal(new int[] {
            20000000,
            0,
            0,
            0});
            this.num_ExposureTime.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.num_ExposureTime.Name = "num_ExposureTime";
            this.num_ExposureTime.Size = new System.Drawing.Size(180, 27);
            this.num_ExposureTime.TabIndex = 1;
            this.num_ExposureTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num_ExposureTime.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // lab_ExposureTime
            // 
            this.lab_ExposureTime.AutoSize = true;
            this.lab_ExposureTime.Location = new System.Drawing.Point(6, 13);
            this.lab_ExposureTime.Name = "lab_ExposureTime";
            this.lab_ExposureTime.Size = new System.Drawing.Size(125, 16);
            this.lab_ExposureTime.TabIndex = 0;
            this.lab_ExposureTime.Text = "Exposure time [us]";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rad_ResetElapsedTimeOnTriggerInput_No);
            this.groupBox2.Controls.Add(this.rad_ResetElapsedTimeOnTriggerInput_Yes);
            this.groupBox2.Location = new System.Drawing.Point(257, 174);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(127, 30);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            // 
            // rad_ResetElapsedTimeOnTriggerInput_No
            // 
            this.rad_ResetElapsedTimeOnTriggerInput_No.AutoSize = true;
            this.rad_ResetElapsedTimeOnTriggerInput_No.Checked = true;
            this.rad_ResetElapsedTimeOnTriggerInput_No.Location = new System.Drawing.Point(73, 10);
            this.rad_ResetElapsedTimeOnTriggerInput_No.Name = "rad_ResetElapsedTimeOnTriggerInput_No";
            this.rad_ResetElapsedTimeOnTriggerInput_No.Size = new System.Drawing.Size(44, 20);
            this.rad_ResetElapsedTimeOnTriggerInput_No.TabIndex = 11;
            this.rad_ResetElapsedTimeOnTriggerInput_No.TabStop = true;
            this.rad_ResetElapsedTimeOnTriggerInput_No.Text = "No";
            this.rad_ResetElapsedTimeOnTriggerInput_No.UseVisualStyleBackColor = true;
            // 
            // rad_ResetElapsedTimeOnTriggerInput_Yes
            // 
            this.rad_ResetElapsedTimeOnTriggerInput_Yes.AutoSize = true;
            this.rad_ResetElapsedTimeOnTriggerInput_Yes.Location = new System.Drawing.Point(7, 10);
            this.rad_ResetElapsedTimeOnTriggerInput_Yes.Name = "rad_ResetElapsedTimeOnTriggerInput_Yes";
            this.rad_ResetElapsedTimeOnTriggerInput_Yes.Size = new System.Drawing.Size(49, 20);
            this.rad_ResetElapsedTimeOnTriggerInput_Yes.TabIndex = 10;
            this.rad_ResetElapsedTimeOnTriggerInput_Yes.Text = "Yes";
            this.rad_ResetElapsedTimeOnTriggerInput_Yes.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.subTabControl);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1085, 692);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Analysis condition";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // subTabControl
            // 
            this.subTabControl.Controls.Add(this.tabPage3);
            this.subTabControl.Controls.Add(this.tabPage4);
            this.subTabControl.Controls.Add(this.tabPage5);
            this.subTabControl.Location = new System.Drawing.Point(6, 6);
            this.subTabControl.Name = "subTabControl";
            this.subTabControl.SelectedIndex = 0;
            this.subTabControl.Size = new System.Drawing.Size(1073, 681);
            this.subTabControl.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.num_ThicknessMoveingAverage);
            this.tabPage3.Controls.Add(this.lab_ThicknessMoveingAverage);
            this.tabPage3.Controls.Add(this.cb_AnalysisLayerMaterial);
            this.tabPage3.Controls.Add(this.lab_AnalysisLayerMaterial);
            this.tabPage3.Controls.Add(this.dataGridView_Mainsetting);
            this.tabPage3.Controls.Add(this.num_NumOfAnalysisLayers);
            this.tabPage3.Controls.Add(this.lab_NumOfAmalysisLayers);
            this.tabPage3.Controls.Add(this.num_FFTMaximumThickness);
            this.tabPage3.Controls.Add(this.lab_FFTMaximumThickness);
            this.tabPage3.Controls.Add(this.cb_NumOfFFTDataPoints);
            this.tabPage3.Controls.Add(this.lab_NumOfFFTDataPoints);
            this.tabPage3.Controls.Add(this.cb_AnalysisMethod);
            this.tabPage3.Controls.Add(this.lab_AnalysisMethod);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.num_AnalysisWavelengthNumber_Upper);
            this.tabPage3.Controls.Add(this.num_AnalysisWavelengthNumber_Lower);
            this.tabPage3.Controls.Add(this.lab_AnalysisWavelengthNumber);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1065, 651);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Main settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // num_ThicknessMoveingAverage
            // 
            this.num_ThicknessMoveingAverage.Location = new System.Drawing.Point(601, 593);
            this.num_ThicknessMoveingAverage.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.num_ThicknessMoveingAverage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_ThicknessMoveingAverage.Name = "num_ThicknessMoveingAverage";
            this.num_ThicknessMoveingAverage.Size = new System.Drawing.Size(442, 27);
            this.num_ThicknessMoveingAverage.TabIndex = 32;
            this.num_ThicknessMoveingAverage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num_ThicknessMoveingAverage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lab_ThicknessMoveingAverage
            // 
            this.lab_ThicknessMoveingAverage.AutoSize = true;
            this.lab_ThicknessMoveingAverage.Location = new System.Drawing.Point(17, 595);
            this.lab_ThicknessMoveingAverage.Name = "lab_ThicknessMoveingAverage";
            this.lab_ThicknessMoveingAverage.Size = new System.Drawing.Size(222, 16);
            this.lab_ThicknessMoveingAverage.TabIndex = 31;
            this.lab_ThicknessMoveingAverage.Text = "Thickness moving average [times]";
            // 
            // cb_AnalysisLayerMaterial
            // 
            this.cb_AnalysisLayerMaterial.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cb_AnalysisLayerMaterial.FormattingEnabled = true;
            this.cb_AnalysisLayerMaterial.Items.AddRange(new object[] {
            "NoLayer",
            "1:Al",
            "2:Air",
            "3:BK-7",
            "4:Si",
            "5:SiO2",
            "6:Ag",
            "7:Au",
            "8:Cr",
            "9:Cu",
            "10:Si3N4",
            "11:Ti",
            "12:test",
            "13:Quartz",
            "14:GaAs",
            "15:Sapphire"});
            this.cb_AnalysisLayerMaterial.Location = new System.Drawing.Point(601, 553);
            this.cb_AnalysisLayerMaterial.Name = "cb_AnalysisLayerMaterial";
            this.cb_AnalysisLayerMaterial.Size = new System.Drawing.Size(442, 27);
            this.cb_AnalysisLayerMaterial.TabIndex = 30;
            this.cb_AnalysisLayerMaterial.Text = "4:Si";
            this.cb_AnalysisLayerMaterial.Visible = false;
            // 
            // lab_AnalysisLayerMaterial
            // 
            this.lab_AnalysisLayerMaterial.AutoSize = true;
            this.lab_AnalysisLayerMaterial.Location = new System.Drawing.Point(17, 558);
            this.lab_AnalysisLayerMaterial.Name = "lab_AnalysisLayerMaterial";
            this.lab_AnalysisLayerMaterial.Size = new System.Drawing.Size(150, 16);
            this.lab_AnalysisLayerMaterial.TabIndex = 29;
            this.lab_AnalysisLayerMaterial.Text = "Analysis layer material";
            this.lab_AnalysisLayerMaterial.Visible = false;
            // 
            // dataGridView_Mainsetting
            // 
            this.dataGridView_Mainsetting.AllowUserToAddRows = false;
            this.dataGridView_Mainsetting.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Mainsetting.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_Mainsetting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Mainsetting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dataGridView_Mainsetting.Location = new System.Drawing.Point(20, 219);
            this.dataGridView_Mainsetting.Name = "dataGridView_Mainsetting";
            this.dataGridView_Mainsetting.RowTemplate.Height = 24;
            this.dataGridView_Mainsetting.Size = new System.Drawing.Size(1023, 323);
            this.dataGridView_Mainsetting.TabIndex = 28;
            this.dataGridView_Mainsetting.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.dataGridView_Mainsetting_CellStateChanged);
            this.dataGridView_Mainsetting.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_Mainsetting_EditingControlShowing);
            this.dataGridView_Mainsetting.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView_RowPostPaint);
            // 
            // Column1
            // 
            dataGridViewCellStyle2.NullValue = "0";
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "Analysis thickness range lower limit";
            this.Column1.Name = "Column1";
            this.Column1.Width = 120;
            // 
            // Column2
            // 
            dataGridViewCellStyle3.NullValue = "0";
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.HeaderText = "Analysis thickness range upper limit";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            dataGridViewCellStyle4.NullValue = "0";
            this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column3.HeaderText = "FFT intensity range lower limit";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            dataGridViewCellStyle5.NullValue = "0";
            this.Column4.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column4.HeaderText = "FFT intensity range upper limit";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            dataGridViewCellStyle6.NullValue = "From top";
            this.Column5.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column5.HeaderText = "Peak search direction";
            this.Column5.Items.AddRange(new object[] {
            "From left",
            "From top",
            "From right",
            "From bottom"});
            this.Column5.Name = "Column5";
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column5.Width = 200;
            // 
            // Column6
            // 
            dataGridViewCellStyle7.NullValue = "1";
            this.Column6.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column6.HeaderText = "Refractive index";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            dataGridViewCellStyle8.Format = "N0";
            dataGridViewCellStyle8.NullValue = "1";
            this.Column7.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column7.HeaderText = "Peak number";
            this.Column7.Name = "Column7";
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // num_NumOfAnalysisLayers
            // 
            this.num_NumOfAnalysisLayers.Location = new System.Drawing.Point(601, 172);
            this.num_NumOfAnalysisLayers.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.num_NumOfAnalysisLayers.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_NumOfAnalysisLayers.Name = "num_NumOfAnalysisLayers";
            this.num_NumOfAnalysisLayers.Size = new System.Drawing.Size(442, 27);
            this.num_NumOfAnalysisLayers.TabIndex = 27;
            this.num_NumOfAnalysisLayers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num_NumOfAnalysisLayers.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_NumOfAnalysisLayers.ValueChanged += new System.EventHandler(this.num_NumOfAmalysisLayers_ValueChanged);
            // 
            // lab_NumOfAmalysisLayers
            // 
            this.lab_NumOfAmalysisLayers.AutoSize = true;
            this.lab_NumOfAmalysisLayers.Location = new System.Drawing.Point(17, 175);
            this.lab_NumOfAmalysisLayers.Name = "lab_NumOfAmalysisLayers";
            this.lab_NumOfAmalysisLayers.Size = new System.Drawing.Size(170, 16);
            this.lab_NumOfAmalysisLayers.TabIndex = 26;
            this.lab_NumOfAmalysisLayers.Text = "Number of analysis layers";
            // 
            // num_FFTMaximumThickness
            // 
            this.num_FFTMaximumThickness.DecimalPlaces = 1;
            this.num_FFTMaximumThickness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.num_FFTMaximumThickness.Location = new System.Drawing.Point(601, 130);
            this.num_FFTMaximumThickness.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            65536});
            this.num_FFTMaximumThickness.Name = "num_FFTMaximumThickness";
            this.num_FFTMaximumThickness.Size = new System.Drawing.Size(442, 27);
            this.num_FFTMaximumThickness.TabIndex = 18;
            this.num_FFTMaximumThickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lab_FFTMaximumThickness
            // 
            this.lab_FFTMaximumThickness.AutoSize = true;
            this.lab_FFTMaximumThickness.Location = new System.Drawing.Point(18, 133);
            this.lab_FFTMaximumThickness.Name = "lab_FFTMaximumThickness";
            this.lab_FFTMaximumThickness.Size = new System.Drawing.Size(195, 16);
            this.lab_FFTMaximumThickness.TabIndex = 17;
            this.lab_FFTMaximumThickness.Text = "FFT maximum thickness [um]";
            // 
            // cb_NumOfFFTDataPoints
            // 
            this.cb_NumOfFFTDataPoints.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cb_NumOfFFTDataPoints.FormattingEnabled = true;
            this.cb_NumOfFFTDataPoints.Items.AddRange(new object[] {
            "128",
            "256",
            "512",
            "1024",
            "2048",
            "4096"});
            this.cb_NumOfFFTDataPoints.Location = new System.Drawing.Point(601, 90);
            this.cb_NumOfFFTDataPoints.Name = "cb_NumOfFFTDataPoints";
            this.cb_NumOfFFTDataPoints.Size = new System.Drawing.Size(442, 27);
            this.cb_NumOfFFTDataPoints.TabIndex = 16;
            this.cb_NumOfFFTDataPoints.Text = "128";
            // 
            // lab_NumOfFFTDataPoints
            // 
            this.lab_NumOfFFTDataPoints.AutoSize = true;
            this.lab_NumOfFFTDataPoints.Location = new System.Drawing.Point(17, 93);
            this.lab_NumOfFFTDataPoints.Name = "lab_NumOfFFTDataPoints";
            this.lab_NumOfFFTDataPoints.Size = new System.Drawing.Size(176, 16);
            this.lab_NumOfFFTDataPoints.TabIndex = 15;
            this.lab_NumOfFFTDataPoints.Text = "Number of FFT data points";
            // 
            // cb_AnalysisMethod
            // 
            this.cb_AnalysisMethod.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cb_AnalysisMethod.FormattingEnabled = true;
            this.cb_AnalysisMethod.Items.AddRange(new object[] {
            "FFT using constant refractive index",
            "FFT using material refractive index",
            "Optimization method",
            "FFT using material refractive index + Optimization method"});
            this.cb_AnalysisMethod.Location = new System.Drawing.Point(601, 52);
            this.cb_AnalysisMethod.Name = "cb_AnalysisMethod";
            this.cb_AnalysisMethod.Size = new System.Drawing.Size(442, 27);
            this.cb_AnalysisMethod.TabIndex = 14;
            this.cb_AnalysisMethod.Text = "FFT using constant refractive index";
            // 
            // lab_AnalysisMethod
            // 
            this.lab_AnalysisMethod.AutoSize = true;
            this.lab_AnalysisMethod.Location = new System.Drawing.Point(17, 55);
            this.lab_AnalysisMethod.Name = "lab_AnalysisMethod";
            this.lab_AnalysisMethod.Size = new System.Drawing.Size(112, 16);
            this.lab_AnalysisMethod.TabIndex = 8;
            this.lab_AnalysisMethod.Text = "Analysis method";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(810, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 16);
            this.label11.TabIndex = 7;
            this.label11.Text = "～";
            // 
            // num_AnalysisWavelengthNumber_Upper
            // 
            this.num_AnalysisWavelengthNumber_Upper.Location = new System.Drawing.Point(857, 14);
            this.num_AnalysisWavelengthNumber_Upper.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.num_AnalysisWavelengthNumber_Upper.Name = "num_AnalysisWavelengthNumber_Upper";
            this.num_AnalysisWavelengthNumber_Upper.Size = new System.Drawing.Size(186, 27);
            this.num_AnalysisWavelengthNumber_Upper.TabIndex = 6;
            this.num_AnalysisWavelengthNumber_Upper.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num_AnalysisWavelengthNumber_Upper.Value = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.num_AnalysisWavelengthNumber_Upper.ValueChanged += new System.EventHandler(this.num_AnalysisWavelengthNumber_Upper_ValueChanged);
            // 
            // num_AnalysisWavelengthNumber_Lower
            // 
            this.num_AnalysisWavelengthNumber_Lower.Location = new System.Drawing.Point(601, 14);
            this.num_AnalysisWavelengthNumber_Lower.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.num_AnalysisWavelengthNumber_Lower.Name = "num_AnalysisWavelengthNumber_Lower";
            this.num_AnalysisWavelengthNumber_Lower.Size = new System.Drawing.Size(186, 27);
            this.num_AnalysisWavelengthNumber_Lower.TabIndex = 5;
            this.num_AnalysisWavelengthNumber_Lower.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num_AnalysisWavelengthNumber_Lower.ValueChanged += new System.EventHandler(this.num_AnalysisWavelengthNumber_Lower_ValueChanged);
            // 
            // lab_AnalysisWavelengthNumber
            // 
            this.lab_AnalysisWavelengthNumber.AutoSize = true;
            this.lab_AnalysisWavelengthNumber.Location = new System.Drawing.Point(17, 17);
            this.lab_AnalysisWavelengthNumber.Name = "lab_AnalysisWavelengthNumber";
            this.lab_AnalysisWavelengthNumber.Size = new System.Drawing.Size(189, 16);
            this.lab_AnalysisWavelengthNumber.TabIndex = 2;
            this.lab_AnalysisWavelengthNumber.Text = "Analysis wavelength number";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dataGridView_DetailSetting);
            this.tabPage4.Controls.Add(this.lab_ThicknessCoefficient);
            this.tabPage4.Controls.Add(this.num_NumOfErrorForNaN);
            this.tabPage4.Controls.Add(this.lab_NumOfErrorForNaN);
            this.tabPage4.Controls.Add(this.cb_ErrorValue);
            this.tabPage4.Controls.Add(this.lab_ErrorValue);
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Controls.Add(this.checkBox_NGPeakExclusion);
            this.tabPage4.Controls.Add(this.checkBox_FFTNormalization);
            this.tabPage4.Controls.Add(this.checkBox_FFTWindowFunction);
            this.tabPage4.Controls.Add(this.cb_SmoothingPoint);
            this.tabPage4.Controls.Add(this.lab_SmoothingPoint);
            this.tabPage4.Location = new System.Drawing.Point(4, 26);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1065, 651);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Detail settings";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dataGridView_DetailSetting
            // 
            this.dataGridView_DetailSetting.AllowUserToAddRows = false;
            this.dataGridView_DetailSetting.AllowUserToDeleteRows = false;
            this.dataGridView_DetailSetting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_DetailSetting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column9,
            this.Column10});
            this.dataGridView_DetailSetting.Location = new System.Drawing.Point(20, 478);
            this.dataGridView_DetailSetting.Name = "dataGridView_DetailSetting";
            this.dataGridView_DetailSetting.RowTemplate.Height = 24;
            this.dataGridView_DetailSetting.Size = new System.Drawing.Size(1023, 163);
            this.dataGridView_DetailSetting.TabIndex = 29;
            this.dataGridView_DetailSetting.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.dataGridView_DetailSetting_CellStateChanged);
            this.dataGridView_DetailSetting.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_DetailSetting_EditingControlShowing);
            this.dataGridView_DetailSetting.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView_RowPostPaint);
            // 
            // Column8
            // 
            dataGridViewCellStyle9.NullValue = "Not use";
            this.Column8.DefaultCellStyle = dataGridViewCellStyle9;
            this.Column8.HeaderText = "Coefficient type";
            this.Column8.Items.AddRange(new object[] {
            "Not use",
            "Use input value",
            "Use calibrated value"});
            this.Column8.Name = "Column8";
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column8.Width = 140;
            // 
            // Column9
            // 
            dataGridViewCellStyle10.NullValue = "1";
            this.Column9.DefaultCellStyle = dataGridViewCellStyle10;
            this.Column9.HeaderText = "C1";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            dataGridViewCellStyle11.NullValue = "0";
            this.Column10.DefaultCellStyle = dataGridViewCellStyle11;
            this.Column10.HeaderText = "C0";
            this.Column10.Name = "Column10";
            // 
            // lab_ThicknessCoefficient
            // 
            this.lab_ThicknessCoefficient.AutoSize = true;
            this.lab_ThicknessCoefficient.Location = new System.Drawing.Point(18, 451);
            this.lab_ThicknessCoefficient.Name = "lab_ThicknessCoefficient";
            this.lab_ThicknessCoefficient.Size = new System.Drawing.Size(285, 16);
            this.lab_ThicknessCoefficient.TabIndex = 28;
            this.lab_ThicknessCoefficient.Text = "Thickness coefficient (C1 * Thickness + C0)";
            // 
            // num_NumOfErrorForNaN
            // 
            this.num_NumOfErrorForNaN.Location = new System.Drawing.Point(626, 411);
            this.num_NumOfErrorForNaN.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.num_NumOfErrorForNaN.Name = "num_NumOfErrorForNaN";
            this.num_NumOfErrorForNaN.Size = new System.Drawing.Size(417, 27);
            this.num_NumOfErrorForNaN.TabIndex = 26;
            this.num_NumOfErrorForNaN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num_NumOfErrorForNaN.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lab_NumOfErrorForNaN
            // 
            this.lab_NumOfErrorForNaN.AutoSize = true;
            this.lab_NumOfErrorForNaN.Location = new System.Drawing.Point(18, 413);
            this.lab_NumOfErrorForNaN.Name = "lab_NumOfErrorForNaN";
            this.lab_NumOfErrorForNaN.Size = new System.Drawing.Size(211, 16);
            this.lab_NumOfErrorForNaN.TabIndex = 25;
            this.lab_NumOfErrorForNaN.Text = "Number of error for NaN [times]";
            // 
            // cb_ErrorValue
            // 
            this.cb_ErrorValue.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cb_ErrorValue.FormattingEnabled = true;
            this.cb_ErrorValue.Items.AddRange(new object[] {
            "NaN",
            "Previous value"});
            this.cb_ErrorValue.Location = new System.Drawing.Point(626, 371);
            this.cb_ErrorValue.Name = "cb_ErrorValue";
            this.cb_ErrorValue.Size = new System.Drawing.Size(417, 27);
            this.cb_ErrorValue.TabIndex = 24;
            this.cb_ErrorValue.Text = "NaN";
            this.cb_ErrorValue.SelectedIndexChanged += new System.EventHandler(this.cb_ErrorValue_SelectedIndexChanged);
            // 
            // lab_ErrorValue
            // 
            this.lab_ErrorValue.AutoSize = true;
            this.lab_ErrorValue.Location = new System.Drawing.Point(18, 376);
            this.lab_ErrorValue.Name = "lab_ErrorValue";
            this.lab_ErrorValue.Size = new System.Drawing.Size(77, 16);
            this.lab_ErrorValue.TabIndex = 23;
            this.lab_ErrorValue.Text = "Error value";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_ThicknessVariationTrend);
            this.groupBox1.Controls.Add(this.lab_ThicknessVariationTrend);
            this.groupBox1.Controls.Add(this.num_FollowupFilterCenterValueMovingAverage);
            this.groupBox1.Controls.Add(this.lab_FollowupFilterCenterValueMovingAverage);
            this.groupBox1.Controls.Add(this.num_FollowupFilterApplicationTime);
            this.groupBox1.Controls.Add(this.lab_FollowupFilterApplicationTime);
            this.groupBox1.Controls.Add(this.num_FollowupFilterRange);
            this.groupBox1.Controls.Add(this.lab_FollowupFilterRange);
            this.groupBox1.Location = new System.Drawing.Point(21, 169);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1022, 186);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1st analysis layer";
            // 
            // cb_ThicknessVariationTrend
            // 
            this.cb_ThicknessVariationTrend.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cb_ThicknessVariationTrend.FormattingEnabled = true;
            this.cb_ThicknessVariationTrend.Items.AddRange(new object[] {
            "No trend",
            "Decreasing"});
            this.cb_ThicknessVariationTrend.Location = new System.Drawing.Point(605, 140);
            this.cb_ThicknessVariationTrend.Name = "cb_ThicknessVariationTrend";
            this.cb_ThicknessVariationTrend.Size = new System.Drawing.Size(401, 27);
            this.cb_ThicknessVariationTrend.TabIndex = 26;
            this.cb_ThicknessVariationTrend.Text = "Decreasing";
            // 
            // lab_ThicknessVariationTrend
            // 
            this.lab_ThicknessVariationTrend.AutoSize = true;
            this.lab_ThicknessVariationTrend.Location = new System.Drawing.Point(14, 145);
            this.lab_ThicknessVariationTrend.Name = "lab_ThicknessVariationTrend";
            this.lab_ThicknessVariationTrend.Size = new System.Drawing.Size(165, 16);
            this.lab_ThicknessVariationTrend.TabIndex = 25;
            this.lab_ThicknessVariationTrend.Text = "Thickness variation trend";
            // 
            // num_FollowupFilterCenterValueMovingAverage
            // 
            this.num_FollowupFilterCenterValueMovingAverage.Location = new System.Drawing.Point(605, 103);
            this.num_FollowupFilterCenterValueMovingAverage.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.num_FollowupFilterCenterValueMovingAverage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_FollowupFilterCenterValueMovingAverage.Name = "num_FollowupFilterCenterValueMovingAverage";
            this.num_FollowupFilterCenterValueMovingAverage.Size = new System.Drawing.Size(401, 27);
            this.num_FollowupFilterCenterValueMovingAverage.TabIndex = 24;
            this.num_FollowupFilterCenterValueMovingAverage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num_FollowupFilterCenterValueMovingAverage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lab_FollowupFilterCenterValueMovingAverage
            // 
            this.lab_FollowupFilterCenterValueMovingAverage.AutoSize = true;
            this.lab_FollowupFilterCenterValueMovingAverage.Location = new System.Drawing.Point(14, 105);
            this.lab_FollowupFilterCenterValueMovingAverage.Name = "lab_FollowupFilterCenterValueMovingAverage";
            this.lab_FollowupFilterCenterValueMovingAverage.Size = new System.Drawing.Size(336, 16);
            this.lab_FollowupFilterCenterValueMovingAverage.TabIndex = 23;
            this.lab_FollowupFilterCenterValueMovingAverage.Text = "Follow-up filter center value moving average [times]";
            // 
            // num_FollowupFilterApplicationTime
            // 
            this.num_FollowupFilterApplicationTime.Location = new System.Drawing.Point(605, 65);
            this.num_FollowupFilterApplicationTime.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.num_FollowupFilterApplicationTime.Name = "num_FollowupFilterApplicationTime";
            this.num_FollowupFilterApplicationTime.Size = new System.Drawing.Size(401, 27);
            this.num_FollowupFilterApplicationTime.TabIndex = 22;
            this.num_FollowupFilterApplicationTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lab_FollowupFilterApplicationTime
            // 
            this.lab_FollowupFilterApplicationTime.AutoSize = true;
            this.lab_FollowupFilterApplicationTime.Location = new System.Drawing.Point(14, 67);
            this.lab_FollowupFilterApplicationTime.Name = "lab_FollowupFilterApplicationTime";
            this.lab_FollowupFilterApplicationTime.Size = new System.Drawing.Size(246, 16);
            this.lab_FollowupFilterApplicationTime.TabIndex = 21;
            this.lab_FollowupFilterApplicationTime.Text = "Follow-up filter application time  [sec]";
            // 
            // num_FollowupFilterRange
            // 
            this.num_FollowupFilterRange.DecimalPlaces = 1;
            this.num_FollowupFilterRange.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.num_FollowupFilterRange.Location = new System.Drawing.Point(605, 28);
            this.num_FollowupFilterRange.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            65536});
            this.num_FollowupFilterRange.Name = "num_FollowupFilterRange";
            this.num_FollowupFilterRange.Size = new System.Drawing.Size(401, 27);
            this.num_FollowupFilterRange.TabIndex = 20;
            this.num_FollowupFilterRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lab_FollowupFilterRange
            // 
            this.lab_FollowupFilterRange.AutoSize = true;
            this.lab_FollowupFilterRange.Location = new System.Drawing.Point(14, 30);
            this.lab_FollowupFilterRange.Name = "lab_FollowupFilterRange";
            this.lab_FollowupFilterRange.Size = new System.Drawing.Size(177, 16);
            this.lab_FollowupFilterRange.TabIndex = 19;
            this.lab_FollowupFilterRange.Text = "Follow-up filter range [um]";
            // 
            // checkBox_NGPeakExclusion
            // 
            this.checkBox_NGPeakExclusion.AutoSize = true;
            this.checkBox_NGPeakExclusion.Checked = true;
            this.checkBox_NGPeakExclusion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_NGPeakExclusion.Location = new System.Drawing.Point(21, 132);
            this.checkBox_NGPeakExclusion.Name = "checkBox_NGPeakExclusion";
            this.checkBox_NGPeakExclusion.Size = new System.Drawing.Size(146, 20);
            this.checkBox_NGPeakExclusion.TabIndex = 21;
            this.checkBox_NGPeakExclusion.Text = "NG peak exclusion";
            this.checkBox_NGPeakExclusion.UseVisualStyleBackColor = true;
            // 
            // checkBox_FFTNormalization
            // 
            this.checkBox_FFTNormalization.AutoSize = true;
            this.checkBox_FFTNormalization.Checked = true;
            this.checkBox_FFTNormalization.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_FFTNormalization.Location = new System.Drawing.Point(21, 95);
            this.checkBox_FFTNormalization.Name = "checkBox_FFTNormalization";
            this.checkBox_FFTNormalization.Size = new System.Drawing.Size(176, 20);
            this.checkBox_FFTNormalization.TabIndex = 20;
            this.checkBox_FFTNormalization.Text = "FFT normalization [0-1]";
            this.checkBox_FFTNormalization.UseVisualStyleBackColor = true;
            // 
            // checkBox_FFTWindowFunction
            // 
            this.checkBox_FFTWindowFunction.AutoSize = true;
            this.checkBox_FFTWindowFunction.Checked = true;
            this.checkBox_FFTWindowFunction.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_FFTWindowFunction.Location = new System.Drawing.Point(21, 56);
            this.checkBox_FFTWindowFunction.Name = "checkBox_FFTWindowFunction";
            this.checkBox_FFTWindowFunction.Size = new System.Drawing.Size(161, 20);
            this.checkBox_FFTWindowFunction.TabIndex = 19;
            this.checkBox_FFTWindowFunction.Text = "FFT window function";
            this.checkBox_FFTWindowFunction.UseVisualStyleBackColor = true;
            // 
            // cb_SmoothingPoint
            // 
            this.cb_SmoothingPoint.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cb_SmoothingPoint.FormattingEnabled = true;
            this.cb_SmoothingPoint.Items.AddRange(new object[] {
            "1",
            "3",
            "5",
            "7",
            "9",
            "11",
            "13",
            "15",
            "17",
            "21",
            "23",
            "25"});
            this.cb_SmoothingPoint.Location = new System.Drawing.Point(626, 12);
            this.cb_SmoothingPoint.Name = "cb_SmoothingPoint";
            this.cb_SmoothingPoint.Size = new System.Drawing.Size(417, 27);
            this.cb_SmoothingPoint.TabIndex = 18;
            this.cb_SmoothingPoint.Text = "1";
            // 
            // lab_SmoothingPoint
            // 
            this.lab_SmoothingPoint.AutoSize = true;
            this.lab_SmoothingPoint.Location = new System.Drawing.Point(18, 18);
            this.lab_SmoothingPoint.Name = "lab_SmoothingPoint";
            this.lab_SmoothingPoint.Size = new System.Drawing.Size(111, 16);
            this.lab_SmoothingPoint.TabIndex = 17;
            this.lab_SmoothingPoint.Text = "Smoothing point";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dataGridView1);
            this.tabPage5.Controls.Add(this.cb_AmbientLayerMaterial);
            this.tabPage5.Controls.Add(this.lab_AmbientLayerMaterial);
            this.tabPage5.Controls.Add(this.num_OptimizationMethodThicknessStepValue);
            this.tabPage5.Controls.Add(this.lab_OptimizationMethodThicknessStepValue);
            this.tabPage5.Controls.Add(this.num_OptimizationMethodSwitchoverThickness);
            this.tabPage5.Controls.Add(this.lab_OptimizationMethodSwitchoverThickness);
            this.tabPage5.Location = new System.Drawing.Point(4, 26);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(1065, 651);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Detail settings (Optimization method)";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column11,
            this.Column12});
            this.dataGridView1.Location = new System.Drawing.Point(20, 132);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1023, 298);
            this.dataGridView1.TabIndex = 30;
            this.dataGridView1.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.dataGridView1_CellStateChanged);
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            // 
            // Column11
            // 
            dataGridViewCellStyle12.NullValue = "2:Air";
            this.Column11.DefaultCellStyle = dataGridViewCellStyle12;
            this.Column11.HeaderText = "Material";
            this.Column11.Items.AddRange(new object[] {
            "NoLayer",
            "1:Al",
            "2:Air",
            "3:BK-7",
            "4:Si",
            "5:SiO2",
            "6:Ag",
            "7:Au",
            "8:Cr",
            "9:Cu",
            "10:Si3N4",
            "11:Ti",
            "12:test",
            "13:Quartz",
            "14:GaAs",
            "15:Sapphire"});
            this.Column11.Name = "Column11";
            // 
            // Column12
            // 
            dataGridViewCellStyle13.NullValue = "0";
            this.Column12.DefaultCellStyle = dataGridViewCellStyle13;
            this.Column12.HeaderText = "Thickness [um]";
            this.Column12.Name = "Column12";
            this.Column12.Width = 130;
            // 
            // cb_AmbientLayerMaterial
            // 
            this.cb_AmbientLayerMaterial.Font = new System.Drawing.Font("新細明體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cb_AmbientLayerMaterial.FormattingEnabled = true;
            this.cb_AmbientLayerMaterial.Items.AddRange(new object[] {
            "NoLayer",
            "1:Al",
            "2:Air",
            "3:BK-7",
            "4:Si",
            "5:SiO2",
            "6:Ag",
            "7:Au",
            "8:Cr",
            "9:Cu",
            "10:Si3N4",
            "11:Ti",
            "12:test",
            "13:Quartz",
            "14:GaAs",
            "15:Sapphire"});
            this.cb_AmbientLayerMaterial.Location = new System.Drawing.Point(601, 90);
            this.cb_AmbientLayerMaterial.Name = "cb_AmbientLayerMaterial";
            this.cb_AmbientLayerMaterial.Size = new System.Drawing.Size(442, 27);
            this.cb_AmbientLayerMaterial.TabIndex = 24;
            this.cb_AmbientLayerMaterial.Text = "2:Air";
            // 
            // lab_AmbientLayerMaterial
            // 
            this.lab_AmbientLayerMaterial.AutoSize = true;
            this.lab_AmbientLayerMaterial.Location = new System.Drawing.Point(17, 94);
            this.lab_AmbientLayerMaterial.Name = "lab_AmbientLayerMaterial";
            this.lab_AmbientLayerMaterial.Size = new System.Drawing.Size(150, 16);
            this.lab_AmbientLayerMaterial.TabIndex = 23;
            this.lab_AmbientLayerMaterial.Text = "Ambient layer material";
            // 
            // num_OptimizationMethodThicknessStepValue
            // 
            this.num_OptimizationMethodThicknessStepValue.DecimalPlaces = 7;
            this.num_OptimizationMethodThicknessStepValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            458752});
            this.num_OptimizationMethodThicknessStepValue.Location = new System.Drawing.Point(601, 52);
            this.num_OptimizationMethodThicknessStepValue.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            65536});
            this.num_OptimizationMethodThicknessStepValue.Name = "num_OptimizationMethodThicknessStepValue";
            this.num_OptimizationMethodThicknessStepValue.Size = new System.Drawing.Size(442, 27);
            this.num_OptimizationMethodThicknessStepValue.TabIndex = 22;
            this.num_OptimizationMethodThicknessStepValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lab_OptimizationMethodThicknessStepValue
            // 
            this.lab_OptimizationMethodThicknessStepValue.AutoSize = true;
            this.lab_OptimizationMethodThicknessStepValue.Location = new System.Drawing.Point(17, 56);
            this.lab_OptimizationMethodThicknessStepValue.Name = "lab_OptimizationMethodThicknessStepValue";
            this.lab_OptimizationMethodThicknessStepValue.Size = new System.Drawing.Size(302, 16);
            this.lab_OptimizationMethodThicknessStepValue.TabIndex = 21;
            this.lab_OptimizationMethodThicknessStepValue.Text = "Optimization method thickness step value [um]";
            // 
            // num_OptimizationMethodSwitchoverThickness
            // 
            this.num_OptimizationMethodSwitchoverThickness.DecimalPlaces = 1;
            this.num_OptimizationMethodSwitchoverThickness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.num_OptimizationMethodSwitchoverThickness.Location = new System.Drawing.Point(601, 14);
            this.num_OptimizationMethodSwitchoverThickness.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            65536});
            this.num_OptimizationMethodSwitchoverThickness.Name = "num_OptimizationMethodSwitchoverThickness";
            this.num_OptimizationMethodSwitchoverThickness.Size = new System.Drawing.Size(442, 27);
            this.num_OptimizationMethodSwitchoverThickness.TabIndex = 20;
            this.num_OptimizationMethodSwitchoverThickness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.num_OptimizationMethodSwitchoverThickness.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lab_OptimizationMethodSwitchoverThickness
            // 
            this.lab_OptimizationMethodSwitchoverThickness.AutoSize = true;
            this.lab_OptimizationMethodSwitchoverThickness.Location = new System.Drawing.Point(17, 18);
            this.lab_OptimizationMethodSwitchoverThickness.Name = "lab_OptimizationMethodSwitchoverThickness";
            this.lab_OptimizationMethodSwitchoverThickness.Size = new System.Drawing.Size(307, 16);
            this.lab_OptimizationMethodSwitchoverThickness.TabIndex = 19;
            this.lab_OptimizationMethodSwitchoverThickness.Text = "Optimization method switchover thickness [um]";
            // 
            // lab_Edit
            // 
            this.lab_Edit.AutoSize = true;
            this.lab_Edit.Font = new System.Drawing.Font("微軟正黑體", 11.25F, System.Drawing.FontStyle.Bold);
            this.lab_Edit.Location = new System.Drawing.Point(19, 454);
            this.lab_Edit.Name = "lab_Edit";
            this.lab_Edit.Size = new System.Drawing.Size(58, 19);
            this.lab_Edit.TabIndex = 14;
            this.lab_Edit.Text = "Delete";
            // 
            // btn_Delete
            // 
            this.btn_Delete.BackColor = System.Drawing.Color.Transparent;
            this.btn_Delete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Delete.BackgroundImage")));
            this.btn_Delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Delete.Location = new System.Drawing.Point(12, 381);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(70, 70);
            this.btn_Delete.TabIndex = 35;
            this.btn_Delete.UseVisualStyleBackColor = false;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.Color.Transparent;
            this.btn_Save.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Save.BackgroundImage")));
            this.btn_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Save.Location = new System.Drawing.Point(12, 501);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(70, 70);
            this.btn_Save.TabIndex = 36;
            this.btn_Save.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Save.UseVisualStyleBackColor = false;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // Recipes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 952);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.txt_Memo);
            this.Controls.Add(this.lab_Memo);
            this.Controls.Add(this.txt_Name);
            this.Controls.Add(this.lab_Name);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txt_RecipeID);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lab_RecipeID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lab_Delete);
            this.Controls.Add(this.lab_Edit);
            this.Controls.Add(this.lab_New);
            this.Controls.Add(this.lab_Cancel);
            this.Controls.Add(this.lab_OK);
            this.Controls.Add(this.btn_New);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.lv_Recipes);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.btn_Save);
            this.Name = "Recipes";
            this.Text = "Recipes";
            this.Load += new System.EventHandler(this.Recipes_Load);
            this.mainTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_MeasurementCycle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumberOfContinuous_measurements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TriggerDelayTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumberOfAccumulations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_ExposureTime)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.subTabControl.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_ThicknessMoveingAverage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Mainsetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumOfAnalysisLayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_FFTMaximumThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_AnalysisWavelengthNumber_Upper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_AnalysisWavelengthNumber_Lower)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_DetailSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_NumOfErrorForNaN)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_FollowupFilterCenterValueMovingAverage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_FollowupFilterApplicationTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_FollowupFilterRange)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_OptimizationMethodThicknessStepValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_OptimizationMethodSwitchoverThickness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lv_Recipes;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_New;
        private System.Windows.Forms.Label lab_OK;
        private System.Windows.Forms.Label lab_Cancel;
        private System.Windows.Forms.Label lab_New;
        private System.Windows.Forms.Label lab_Delete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lab_RecipeID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_RecipeID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lab_Name;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.Label lab_Memo;
        private System.Windows.Forms.TextBox txt_Memo;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lab_ExposureTime;
        private System.Windows.Forms.Button btn_StopMonitoring;
        private System.Windows.Forms.Button btn_StartMonitoring;
        private System.Windows.Forms.NumericUpDown num_NumberOfAccumulations;
        private System.Windows.Forms.Label lab_Number_of_accumulations;
        private System.Windows.Forms.NumericUpDown num_ExposureTime;
        private System.Windows.Forms.NumericUpDown num_TriggerDelayTime;
        private System.Windows.Forms.Label lab_TriggerDelayTime;
        private System.Windows.Forms.Label lab_ResetElapsedTimeOnTriggerInput;
        private System.Windows.Forms.Label lab_MeasurementMode;
        private System.Windows.Forms.ComboBox cb_MeasurementMode;
        private System.Windows.Forms.NumericUpDown num_NumberOfContinuous_measurements;
        private System.Windows.Forms.Label lab_Number_of_continuous_measurements;
        private System.Windows.Forms.NumericUpDown num_MeasurementCycle;
        private System.Windows.Forms.Label lab_MeasurementCycle;
        private System.Windows.Forms.Label lab_GetAnalysisSpectrum;
        private System.Windows.Forms.RadioButton rad_GetReflectanceSpectrum_No;
        private System.Windows.Forms.RadioButton rad_GetReflectanceSpectrum_Yes;
        private System.Windows.Forms.Label lab_GetReflectanceSpectrum;
        private System.Windows.Forms.RadioButton rad_GetSignalSpectrum_No;
        private System.Windows.Forms.RadioButton rad_GetSignalSpectrum_Yes;
        private System.Windows.Forms.Label lab_GetSignalSpectrum;
        private System.Windows.Forms.TabControl subTabControl;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown num_AnalysisWavelengthNumber_Upper;
        private System.Windows.Forms.NumericUpDown num_AnalysisWavelengthNumber_Lower;
        private System.Windows.Forms.Label lab_AnalysisWavelengthNumber;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ComboBox cb_AnalysisMethod;
        private System.Windows.Forms.Label lab_AnalysisMethod;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.ComboBox cb_NumOfFFTDataPoints;
        private System.Windows.Forms.Label lab_NumOfFFTDataPoints;
        private System.Windows.Forms.NumericUpDown num_FFTMaximumThickness;
        private System.Windows.Forms.Label lab_FFTMaximumThickness;
        private System.Windows.Forms.DataGridView dataGridView_Mainsetting;
        private System.Windows.Forms.NumericUpDown num_NumOfAnalysisLayers;
        private System.Windows.Forms.Label lab_NumOfAmalysisLayers;
        private System.Windows.Forms.NumericUpDown num_ThicknessMoveingAverage;
        private System.Windows.Forms.Label lab_ThicknessMoveingAverage;
        private System.Windows.Forms.ComboBox cb_AnalysisLayerMaterial;
        private System.Windows.Forms.Label lab_AnalysisLayerMaterial;
        private System.Windows.Forms.ComboBox cb_SmoothingPoint;
        private System.Windows.Forms.Label lab_SmoothingPoint;
        private System.Windows.Forms.CheckBox checkBox_NGPeakExclusion;
        private System.Windows.Forms.CheckBox checkBox_FFTNormalization;
        private System.Windows.Forms.CheckBox checkBox_FFTWindowFunction;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown num_FollowupFilterCenterValueMovingAverage;
        private System.Windows.Forms.Label lab_FollowupFilterCenterValueMovingAverage;
        private System.Windows.Forms.NumericUpDown num_FollowupFilterApplicationTime;
        private System.Windows.Forms.Label lab_FollowupFilterApplicationTime;
        private System.Windows.Forms.NumericUpDown num_FollowupFilterRange;
        private System.Windows.Forms.Label lab_FollowupFilterRange;
        private System.Windows.Forms.ComboBox cb_ThicknessVariationTrend;
        private System.Windows.Forms.Label lab_ThicknessVariationTrend;
        private System.Windows.Forms.NumericUpDown num_NumOfErrorForNaN;
        private System.Windows.Forms.Label lab_NumOfErrorForNaN;
        private System.Windows.Forms.ComboBox cb_ErrorValue;
        private System.Windows.Forms.Label lab_ErrorValue;
        private System.Windows.Forms.Label lab_ThicknessCoefficient;
        private System.Windows.Forms.DataGridView dataGridView_DetailSetting;
        private System.Windows.Forms.NumericUpDown num_OptimizationMethodThicknessStepValue;
        private System.Windows.Forms.Label lab_OptimizationMethodThicknessStepValue;
        private System.Windows.Forms.NumericUpDown num_OptimizationMethodSwitchoverThickness;
        private System.Windows.Forms.Label lab_OptimizationMethodSwitchoverThickness;
        private System.Windows.Forms.ComboBox cb_AmbientLayerMaterial;
        private System.Windows.Forms.Label lab_AmbientLayerMaterial;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.RadioButton rad_GetAnalysisSpectrum_No;
        private System.Windows.Forms.RadioButton rad_GetAnalysisSpectrum_Yes;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rad_ResetElapsedTimeOnTriggerInput_No;
        private System.Windows.Forms.RadioButton rad_ResetElapsedTimeOnTriggerInput_Yes;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lab_Edit;
        private System.Windows.Forms.Button btn_Delete;
        private System.ComponentModel.BackgroundWorker bW_chart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
    }
}