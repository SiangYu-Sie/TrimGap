namespace TrimGap
{
    partial class SignalAnalysisForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series15 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series16 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series17 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series18 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnSignalAnalysis = new System.Windows.Forms.Button();
            this.label115 = new System.Windows.Forms.Label();
            this.lb_analysisFileName = new System.Windows.Forms.Label();
            this.progressBar_SignalAnalysis = new System.Windows.Forms.ProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.bWSignalPlot = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_result_2 = new System.Windows.Forms.TextBox();
            this.tb_result_1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_result_0 = new System.Windows.Forms.TextBox();
            this.tb_result_3 = new System.Windows.Forms.TextBox();
            this.cb_WaferType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chartSignal = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_ClearDatagridview = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabTrim = new System.Windows.Forms.TabPage();
            this.tabTape = new System.Windows.Forms.TabPage();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pB_BlueTape = new System.Windows.Forms.PictureBox();
            this.tabTrimPt = new System.Windows.Forms.TabPage();
            this.label_HTW_method = new System.Windows.Forms.Label();
            this.chartSignalPt = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cb_TrimWaferEdgeEvaluate = new System.Windows.Forms.CheckBox();
            this.lb_RecipeName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSelectRecipe = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Gap = new System.Windows.Forms.TextBox();
            this.cbAnalysis_method = new System.Windows.Forms.CheckBox();
            this.txt_StartPoint = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSignal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabTrim.SuspendLayout();
            this.tabTape.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB_BlueTape)).BeginInit();
            this.tabTrimPt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSignalPt)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSignalAnalysis
            // 
            this.btnSignalAnalysis.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSignalAnalysis.Location = new System.Drawing.Point(12, 12);
            this.btnSignalAnalysis.Name = "btnSignalAnalysis";
            this.btnSignalAnalysis.Size = new System.Drawing.Size(204, 44);
            this.btnSignalAnalysis.TabIndex = 40;
            this.btnSignalAnalysis.Text = "ReadFile && Analysis";
            this.btnSignalAnalysis.UseVisualStyleBackColor = true;
            this.btnSignalAnalysis.Click += new System.EventHandler(this.btnSignalAnalysis_Click);
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label115.Location = new System.Drawing.Point(222, 12);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(143, 24);
            this.label115.TabIndex = 65;
            this.label115.Text = "目前分析資料：";
            // 
            // lb_analysisFileName
            // 
            this.lb_analysisFileName.AutoEllipsis = true;
            this.lb_analysisFileName.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_analysisFileName.Location = new System.Drawing.Point(222, 46);
            this.lb_analysisFileName.Name = "lb_analysisFileName";
            this.lb_analysisFileName.Size = new System.Drawing.Size(167, 102);
            this.lb_analysisFileName.TabIndex = 66;
            this.lb_analysisFileName.Text = "filename";
            // 
            // progressBar_SignalAnalysis
            // 
            this.progressBar_SignalAnalysis.Location = new System.Drawing.Point(12, 62);
            this.progressBar_SignalAnalysis.Name = "progressBar_SignalAnalysis";
            this.progressBar_SignalAnalysis.Size = new System.Drawing.Size(203, 24);
            this.progressBar_SignalAnalysis.TabIndex = 68;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // bWSignalPlot
            // 
            this.bWSignalPlot.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bWSignalPlot_DoWork);
            this.bWSignalPlot.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bWSignalPlot_ProgressChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tb_result_2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.tb_result_1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tb_result_0, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_result_3, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 230);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(136, 127);
            this.tableLayoutPanel1.TabIndex = 72;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 12);
            this.label1.TabIndex = 82;
            this.label1.Text = "W2";
            // 
            // tb_result_2
            // 
            this.tb_result_2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb_result_2.Location = new System.Drawing.Point(72, 66);
            this.tb_result_2.Name = "tb_result_2";
            this.tb_result_2.Size = new System.Drawing.Size(60, 22);
            this.tb_result_2.TabIndex = 80;
            // 
            // tb_result_1
            // 
            this.tb_result_1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb_result_1.Location = new System.Drawing.Point(72, 35);
            this.tb_result_1.Name = "tb_result_1";
            this.tb_result_1.Size = new System.Drawing.Size(60, 22);
            this.tb_result_1.TabIndex = 78;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 12);
            this.label2.TabIndex = 73;
            this.label2.Text = "H1";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 12);
            this.label3.TabIndex = 74;
            this.label3.Text = "H2";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 12);
            this.label4.TabIndex = 75;
            this.label4.Text = "W1";
            // 
            // tb_result_0
            // 
            this.tb_result_0.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb_result_0.Location = new System.Drawing.Point(72, 4);
            this.tb_result_0.Name = "tb_result_0";
            this.tb_result_0.Size = new System.Drawing.Size(60, 22);
            this.tb_result_0.TabIndex = 76;
            // 
            // tb_result_3
            // 
            this.tb_result_3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb_result_3.Location = new System.Drawing.Point(72, 99);
            this.tb_result_3.Name = "tb_result_3";
            this.tb_result_3.Size = new System.Drawing.Size(60, 22);
            this.tb_result_3.TabIndex = 81;
            // 
            // cb_WaferType
            // 
            this.cb_WaferType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_WaferType.FormattingEnabled = true;
            this.cb_WaferType.Items.AddRange(new object[] {
            "Blue Tape",
            "Trim 1 step",
            "Trim 2 step",
            "Trim 2 step PT",
            "Trim 2 Inline",
            "CCD",
            "Trim 1 step PT",
            "Trim 1 Inline",
            "TTV"});
            this.cb_WaferType.Location = new System.Drawing.Point(12, 128);
            this.cb_WaferType.Name = "cb_WaferType";
            this.cb_WaferType.Size = new System.Drawing.Size(121, 20);
            this.cb_WaferType.TabIndex = 73;
            this.cb_WaferType.SelectedIndexChanged += new System.EventHandler(this.cb_WaferType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(8, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 24);
            this.label5.TabIndex = 74;
            this.label5.Text = "Wafer Type：";
            // 
            // chartSignal
            // 
            this.chartSignal.BackColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.ScaleBreakStyle.CollapsibleSpaceThreshold = 10;
            chartArea1.AxisX.ScaleBreakStyle.Enabled = true;
            chartArea1.AxisX.Title = "um";
            chartArea1.AxisX.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX2.LineColor = System.Drawing.Color.Blue;
            chartArea1.AxisY.Title = "um";
            chartArea1.AxisY.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY2.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea1.AxisY2.Title = "%";
            chartArea1.AxisY2.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea1.AxisY2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.Name = "ChartAreaSignal";
            this.chartSignal.ChartAreas.Add(chartArea1);
            this.chartSignal.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.chartSignal.Location = new System.Drawing.Point(63, 6);
            this.chartSignal.Name = "chartSignal";
            series1.ChartArea = "ChartAreaSignal";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Name = "Series1";
            series2.ChartArea = "ChartAreaSignal";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Name = "Series2";
            series3.BorderColor = System.Drawing.Color.Red;
            series3.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series3.ChartArea = "ChartAreaSignal";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.Red;
            series3.Name = "Surface1";
            series4.BorderColor = System.Drawing.Color.Black;
            series4.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series4.ChartArea = "ChartAreaSignal";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Black;
            series4.Name = "Surface2";
            series5.BorderColor = System.Drawing.Color.Blue;
            series5.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series5.ChartArea = "ChartAreaSignal";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = System.Drawing.Color.Blue;
            series5.Name = "Surface3";
            series6.BorderColor = System.Drawing.Color.Red;
            series6.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series6.ChartArea = "ChartAreaSignal";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = System.Drawing.Color.Red;
            series6.Name = "Slope1";
            series7.BorderColor = System.Drawing.Color.Black;
            series7.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series7.ChartArea = "ChartAreaSignal";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Color = System.Drawing.Color.Black;
            series7.Name = "Slope2";
            series8.BorderColor = System.Drawing.Color.Blue;
            series8.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series8.ChartArea = "ChartAreaSignal";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Color = System.Drawing.Color.Blue;
            series8.Name = "Slope3";
            this.chartSignal.Series.Add(series1);
            this.chartSignal.Series.Add(series2);
            this.chartSignal.Series.Add(series3);
            this.chartSignal.Series.Add(series4);
            this.chartSignal.Series.Add(series5);
            this.chartSignal.Series.Add(series6);
            this.chartSignal.Series.Add(series7);
            this.chartSignal.Series.Add(series8);
            this.chartSignal.Size = new System.Drawing.Size(787, 529);
            this.chartSignal.TabIndex = 75;
            this.chartSignal.Text = "chart1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column6,
            this.Column5});
            this.dataGridView1.Location = new System.Drawing.Point(12, 364);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(396, 208);
            this.dataGridView1.TabIndex = 76;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "H1";
            this.Column1.Name = "Column1";
            this.Column1.Width = 60;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "W1";
            this.Column2.Name = "Column2";
            this.Column2.Width = 60;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "H2";
            this.Column3.Name = "Column3";
            this.Column3.Width = 60;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "W2";
            this.Column4.Name = "Column4";
            this.Column4.Width = 60;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Gap";
            this.Column6.Name = "Column6";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Note";
            this.Column5.Name = "Column5";
            this.Column5.Width = 200;
            // 
            // btn_ClearDatagridview
            // 
            this.btn_ClearDatagridview.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_ClearDatagridview.Location = new System.Drawing.Point(321, 327);
            this.btn_ClearDatagridview.Name = "btn_ClearDatagridview";
            this.btn_ClearDatagridview.Size = new System.Drawing.Size(87, 30);
            this.btn_ClearDatagridview.TabIndex = 77;
            this.btn_ClearDatagridview.Text = "Clear";
            this.btn_ClearDatagridview.UseVisualStyleBackColor = true;
            this.btn_ClearDatagridview.Click += new System.EventHandler(this.btn_ClearDatagridview_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tabTrim);
            this.tabControl1.Controls.Add(this.tabTape);
            this.tabControl1.Controls.Add(this.tabTrimPt);
            this.tabControl1.Location = new System.Drawing.Point(392, 2);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(866, 570);
            this.tabControl1.TabIndex = 78;
            // 
            // tabTrim
            // 
            this.tabTrim.BackColor = System.Drawing.SystemColors.Control;
            this.tabTrim.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabTrim.Controls.Add(this.chartSignal);
            this.tabTrim.Location = new System.Drawing.Point(22, 4);
            this.tabTrim.Name = "tabTrim";
            this.tabTrim.Padding = new System.Windows.Forms.Padding(3);
            this.tabTrim.Size = new System.Drawing.Size(840, 562);
            this.tabTrim.TabIndex = 0;
            this.tabTrim.Text = "Trim";
            // 
            // tabTape
            // 
            this.tabTape.BackColor = System.Drawing.SystemColors.Control;
            this.tabTape.Controls.Add(this.chart1);
            this.tabTape.Controls.Add(this.pB_BlueTape);
            this.tabTape.Location = new System.Drawing.Point(22, 4);
            this.tabTape.Name = "tabTape";
            this.tabTape.Padding = new System.Windows.Forms.Padding(3);
            this.tabTape.Size = new System.Drawing.Size(840, 562);
            this.tabTape.TabIndex = 1;
            this.tabTape.Text = "Tape";
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Location = new System.Drawing.Point(356, 94);
            this.chart1.Name = "chart1";
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series9.Name = "Series1";
            this.chart1.Series.Add(series9);
            this.chart1.Size = new System.Drawing.Size(463, 322);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            // 
            // pB_BlueTape
            // 
            this.pB_BlueTape.BackColor = System.Drawing.Color.White;
            this.pB_BlueTape.Location = new System.Drawing.Point(25, 94);
            this.pB_BlueTape.Name = "pB_BlueTape";
            this.pB_BlueTape.Size = new System.Drawing.Size(307, 391);
            this.pB_BlueTape.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pB_BlueTape.TabIndex = 1;
            this.pB_BlueTape.TabStop = false;
            // 
            // tabTrimPt
            // 
            this.tabTrimPt.Controls.Add(this.label_HTW_method);
            this.tabTrimPt.Controls.Add(this.chartSignalPt);
            this.tabTrimPt.Location = new System.Drawing.Point(22, 4);
            this.tabTrimPt.Name = "tabTrimPt";
            this.tabTrimPt.Size = new System.Drawing.Size(840, 562);
            this.tabTrimPt.TabIndex = 2;
            this.tabTrimPt.Text = "tabTrimPt";
            this.tabTrimPt.UseVisualStyleBackColor = true;
            // 
            // label_HTW_method
            // 
            this.label_HTW_method.AutoSize = true;
            this.label_HTW_method.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_HTW_method.Location = new System.Drawing.Point(236, 509);
            this.label_HTW_method.Name = "label_HTW_method";
            this.label_HTW_method.Size = new System.Drawing.Size(187, 24);
            this.label_HTW_method.TabIndex = 77;
            this.label_HTW_method.Text = "label_HTW_method";
            // 
            // chartSignalPt
            // 
            this.chartSignalPt.BackColor = System.Drawing.Color.Transparent;
            chartArea3.AxisX.ScaleBreakStyle.CollapsibleSpaceThreshold = 10;
            chartArea3.AxisX.ScaleBreakStyle.Enabled = true;
            chartArea3.AxisX.Title = "um";
            chartArea3.AxisX.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea3.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea3.AxisX2.LineColor = System.Drawing.Color.Blue;
            chartArea3.AxisY.Title = "um";
            chartArea3.AxisY.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea3.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea3.AxisY2.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Horizontal;
            chartArea3.AxisY2.Title = "%";
            chartArea3.AxisY2.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea3.AxisY2.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea3.Name = "ChartAreaSignal";
            this.chartSignalPt.ChartAreas.Add(chartArea3);
            this.chartSignalPt.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.chartSignalPt.Location = new System.Drawing.Point(32, 4);
            this.chartSignalPt.Name = "chartSignalPt";
            series10.ChartArea = "ChartAreaSignal";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series10.Name = "Series1";
            series11.ChartArea = "ChartAreaSignal";
            series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series11.Name = "Series2";
            series12.ChartArea = "ChartAreaSignal";
            series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series12.Name = "Series3";
            series13.BorderColor = System.Drawing.Color.Red;
            series13.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series13.ChartArea = "ChartAreaSignal";
            series13.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series13.Color = System.Drawing.Color.Red;
            series13.Name = "Surface1";
            series14.BorderColor = System.Drawing.Color.Black;
            series14.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series14.ChartArea = "ChartAreaSignal";
            series14.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series14.Color = System.Drawing.Color.Black;
            series14.Name = "Surface2";
            series15.BorderColor = System.Drawing.Color.Blue;
            series15.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series15.ChartArea = "ChartAreaSignal";
            series15.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series15.Color = System.Drawing.Color.Blue;
            series15.Name = "Surface3";
            series16.BorderColor = System.Drawing.Color.Red;
            series16.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series16.ChartArea = "ChartAreaSignal";
            series16.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series16.Color = System.Drawing.Color.Red;
            series16.Name = "Slope1";
            series17.BorderColor = System.Drawing.Color.Black;
            series17.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series17.ChartArea = "ChartAreaSignal";
            series17.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series17.Color = System.Drawing.Color.Black;
            series17.Name = "Slope2";
            series18.BorderColor = System.Drawing.Color.Blue;
            series18.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series18.ChartArea = "ChartAreaSignal";
            series18.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series18.Color = System.Drawing.Color.Blue;
            series18.Name = "Slope3";
            this.chartSignalPt.Series.Add(series10);
            this.chartSignalPt.Series.Add(series11);
            this.chartSignalPt.Series.Add(series12);
            this.chartSignalPt.Series.Add(series13);
            this.chartSignalPt.Series.Add(series14);
            this.chartSignalPt.Series.Add(series15);
            this.chartSignalPt.Series.Add(series16);
            this.chartSignalPt.Series.Add(series17);
            this.chartSignalPt.Series.Add(series18);
            this.chartSignalPt.Size = new System.Drawing.Size(787, 529);
            this.chartSignalPt.TabIndex = 76;
            this.chartSignalPt.Text = "chart1";
            // 
            // cb_TrimWaferEdgeEvaluate
            // 
            this.cb_TrimWaferEdgeEvaluate.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cb_TrimWaferEdgeEvaluate.Location = new System.Drawing.Point(12, 158);
            this.cb_TrimWaferEdgeEvaluate.Name = "cb_TrimWaferEdgeEvaluate";
            this.cb_TrimWaferEdgeEvaluate.Size = new System.Drawing.Size(266, 31);
            this.cb_TrimWaferEdgeEvaluate.TabIndex = 79;
            this.cb_TrimWaferEdgeEvaluate.Text = "Trim Wafer Edge Evaluate";
            this.cb_TrimWaferEdgeEvaluate.UseVisualStyleBackColor = true;
            // 
            // lb_RecipeName
            // 
            this.lb_RecipeName.AutoEllipsis = true;
            this.lb_RecipeName.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_RecipeName.Location = new System.Drawing.Point(231, 259);
            this.lb_RecipeName.Name = "lb_RecipeName";
            this.lb_RecipeName.Size = new System.Drawing.Size(167, 59);
            this.lb_RecipeName.TabIndex = 81;
            this.lb_RecipeName.Text = "Default";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(231, 230);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 24);
            this.label7.TabIndex = 80;
            this.label7.Text = "參考Recipe：";
            // 
            // btnSelectRecipe
            // 
            this.btnSelectRecipe.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSelectRecipe.Location = new System.Drawing.Point(155, 230);
            this.btnSelectRecipe.Name = "btnSelectRecipe";
            this.btnSelectRecipe.Size = new System.Drawing.Size(79, 79);
            this.btnSelectRecipe.TabIndex = 82;
            this.btnSelectRecipe.Text = "Read Recipe";
            this.btnSelectRecipe.UseVisualStyleBackColor = true;
            this.btnSelectRecipe.Click += new System.EventHandler(this.btnSelectRecipe_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(152, 327);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 20);
            this.label6.TabIndex = 83;
            this.label6.Text = "Gap：";
            // 
            // txt_Gap
            // 
            this.txt_Gap.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txt_Gap.Location = new System.Drawing.Point(212, 325);
            this.txt_Gap.Name = "txt_Gap";
            this.txt_Gap.Size = new System.Drawing.Size(100, 29);
            this.txt_Gap.TabIndex = 84;
            // 
            // cbAnalysis_method
            // 
            this.cbAnalysis_method.AutoSize = true;
            this.cbAnalysis_method.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbAnalysis_method.Location = new System.Drawing.Point(12, 195);
            this.cbAnalysis_method.Name = "cbAnalysis_method";
            this.cbAnalysis_method.Size = new System.Drawing.Size(183, 28);
            this.cbAnalysis_method.TabIndex = 85;
            this.cbAnalysis_method.Text = "Analysis_method";
            this.cbAnalysis_method.UseVisualStyleBackColor = true;
            // 
            // txt_StartPoint
            // 
            this.txt_StartPoint.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txt_StartPoint.Location = new System.Drawing.Point(258, 119);
            this.txt_StartPoint.Name = "txt_StartPoint";
            this.txt_StartPoint.Size = new System.Drawing.Size(100, 29);
            this.txt_StartPoint.TabIndex = 87;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(161, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 20);
            this.label8.TabIndex = 86;
            this.label8.Text = "StartPoint：";
            // 
            // SignalAnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1258, 582);
            this.Controls.Add(this.txt_StartPoint);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbAnalysis_method);
            this.Controls.Add(this.txt_Gap);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSelectRecipe);
            this.Controls.Add(this.lb_RecipeName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cb_TrimWaferEdgeEvaluate);
            this.Controls.Add(this.lb_analysisFileName);
            this.Controls.Add(this.btn_ClearDatagridview);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cb_WaferType);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.progressBar_SignalAnalysis);
            this.Controls.Add(this.label115);
            this.Controls.Add(this.btnSignalAnalysis);
            this.Controls.Add(this.tabControl1);
            this.Name = "SignalAnalysisForm";
            this.Text = "SignalAnalysis";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSignal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabTrim.ResumeLayout(false);
            this.tabTape.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB_BlueTape)).EndInit();
            this.tabTrimPt.ResumeLayout(false);
            this.tabTrimPt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSignalPt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSignalAnalysis;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.Label lb_analysisFileName;
        private System.Windows.Forms.ProgressBar progressBar_SignalAnalysis;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.ComponentModel.BackgroundWorker bWSignalPlot;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tb_result_1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_result_0;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_result_2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_result_3;
        private System.Windows.Forms.ComboBox cb_WaferType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSignal;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_ClearDatagridview;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabTrim;
        private System.Windows.Forms.TabPage tabTape;
        private System.Windows.Forms.PictureBox pB_BlueTape;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TabPage tabTrimPt;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSignalPt;
        private System.Windows.Forms.CheckBox cb_TrimWaferEdgeEvaluate;
        private System.Windows.Forms.Label lb_RecipeName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSelectRecipe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_Gap;
        private System.Windows.Forms.CheckBox cbAnalysis_method;
        private System.Windows.Forms.Label label_HTW_method;
        private System.Windows.Forms.TextBox txt_StartPoint;
        private System.Windows.Forms.Label label8;
    }
}