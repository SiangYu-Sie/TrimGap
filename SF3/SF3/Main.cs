using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using static Otsuka.Common;
using System.Windows.Forms.DataVisualization.Charting;
using static Otsuka.SF3;
using Otsuka;

namespace SF3_Form
{

    public partial class Main : Form
    {
        Otsuka.SF3 SF3;
        public List<int> MaterialID = new List<int>();
        public List<string> MaterialName = new List<string>();
        public double Chart_Value = 0;
        public double Chart_Max = 0;
        public double Chart_Min = 0;
        public int layerNum = 0;
        public double Thickness_XMaximum;
        public double Thickness_XMinimum;
        public double PowerSpectrum_XMaximum;
        public double PowerSpectrum_YMaximum;
        public double PowerSpectrum_YMinimum;
        public double ReflectanceSpectrum_YMaximum = 0.008;
        public double ReflectanceSpectrum_YMinimum = -0.0;
        public double SampleSpectrum_YMaximum = 0.008;
        public double SampleSpectrum_YMinimum = -0.002;
        public double ReferenceSpectrum_YMaximum = 0.1;
        public double ReferenceSpectrum_YMinimum = -0.002;
        public int Meas_Count;
        public int count;//目前count(總共Meas_Count)
        public double SubMaxMin;
        public double XValue;
        public double ElapsedTime;

        public Main(Otsuka.SF3 sf3)
        {
            SF3 = sf3;
            InitializeComponent();
            this.ControlBox = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SF3.Action_CmdGetAnalysisThicknessRange();
            Thickness_Init_chart();
            PowerSpectrum_Init_chart();
            ReflectanceSpectrum_Init_chart();
            SampleSpectrum_Init_chart();
            ReferenceSpectrum_Init_chart();
            SF3.Action_CmdGetAnalysisNum();
            layerNum = CommonResult.GetAnalysisNum_AnalysisLayerNo;
            Series_Clear(layerNum);
            //給Main.Form的Chart
            //Thickness_Init_chart
            Thickness_XMinimum = 100000;
            Thickness_XMaximum = 0;
            for (int i = 0; i < CommonResult.GetAnalysisThicknessRange_DataQuantity; i++)
            {
                if (Thickness_XMinimum > CommonResult.GetAnalysisThicknessRange_ThicknessRangeLowerLimit[i]) Thickness_XMinimum = CommonResult.GetAnalysisThicknessRange_ThicknessRangeLowerLimit[i];
                if (Thickness_XMaximum < CommonResult.GetAnalysisThicknessRange_ThicknessRangeUpperLimit[i]) Thickness_XMaximum = CommonResult.GetAnalysisThicknessRange_ThicknessRangeUpperLimit[i];
            }
            Thickness_chart.ChartAreas[0].AxisY.Maximum = Math.Round(Thickness_XMaximum, 3);
            Thickness_chart.ChartAreas[0].AxisY.Minimum = Math.Round(Thickness_XMinimum, 3);
            SubMaxMin = (double)decimal.Round((decimal)(Thickness_chart.ChartAreas[0].AxisY.Maximum - Thickness_chart.ChartAreas[0].AxisY.Minimum), 3);
            Thickness_chart.ChartAreas[0].AxisY.Interval = Math.Round(SubMaxMin / 5, 3);

            //PowerSpectrum_Init_chart
            PowerSpectrum_YMinimum = 100000;
            PowerSpectrum_YMaximum = 0;
            PowerSpectrum_chart.ChartAreas[0].AxisX.Maximum = CommonResult.GetFFTMaxThickness_FFTMaximumThickness;
            PowerSpectrum_chart.ChartAreas[0].AxisX.Interval = PowerSpectrum_chart.ChartAreas[0].AxisX.Maximum / 5; //設定X軸間隔 最大值/5
            for (int i = 0; i < CommonResult.GetFFTPowerRange_DataQuantity; i++)
            {
                if (PowerSpectrum_YMinimum > CommonResult.GetFFTPowerRange_FFTIntensityRangeLowerLimit[i]) PowerSpectrum_YMinimum = CommonResult.GetFFTPowerRange_FFTIntensityRangeLowerLimit[i];
                if (PowerSpectrum_YMaximum < CommonResult.GetFFTPowerRange_FFTIntensityRangeUpperLimit[i]) PowerSpectrum_YMaximum = CommonResult.GetFFTPowerRange_FFTIntensityRangeUpperLimit[i];
            }
            PowerSpectrum_chart.ChartAreas[0].AxisY.Maximum = Math.Round(PowerSpectrum_YMaximum, 3);
            PowerSpectrum_chart.ChartAreas[0].AxisY.Minimum = Math.Round(PowerSpectrum_YMinimum, 3);
            SubMaxMin = (double)decimal.Round((decimal)(PowerSpectrum_chart.ChartAreas[0].AxisY.Maximum - PowerSpectrum_chart.ChartAreas[0].AxisY.Minimum), 3);
            PowerSpectrum_chart.ChartAreas[0].AxisY.Interval = Math.Round(SubMaxMin / 5, 3); //設定Y軸間隔 最大值/5

            SF3.Action_CmdGetReference();
            SF3.Action_CmdGetMaterialList();
            MaterialID = CommonResult.GetMaterialList_MaterialID;
            MaterialName = CommonResult.GetMaterialList_MaterialName;
            for (int i = 0; i < MaterialID.Count; i++)
            {
                SF3.Action_CmdGetMaterial(MaterialID[i]);
            }

            Meas_Count = Convert.ToInt32(num_MaxMeasCount.Value);
            btn_AbortMeas.Enabled = false;
        }
        #region Timer
        private void Monitortimer_Tick(object sender, EventArgs e)
        {
            //給SF3
            SF3.Action_CmdGetResult();

            //歸零
            PowerSpectrum_Init_chart();
            ReflectanceSpectrum_Init_chart();
            SampleSpectrum_Init_chart();
            ReferenceSpectrum_Init_chart();

            //PowerSpectrum
            XValue = PowerSpectrum_XMaximum / SF3.GetResult.PowerSpectrum.Count;
            for (int i = 0; i < SF3.GetResult.PowerSpectrum.Count; i++)
            {
                PowerSpectrum_chart.Series[0].Points.AddXY(i * XValue, GetResult.PowerSpectrum[i]);
            }

            //for (int i = 0; i < SF3.GetResult.PowerSpectrum.Count; i++)
            //{
            //    PowerSpectrum_chart.Series[0].Points.AddXY(i * XValue, GetResult.PowerSpectrum[i]);
            //    if (i == 0)
            //    {
            //        PowerSpectrum_YMaximum = GetResult.PowerSpectrum[i] + 0.01;
            //        PowerSpectrum_YMinimum = GetResult.PowerSpectrum[i] - 0.01;
            //    }
            //    if (PowerSpectrum_YMaximum < GetResult.PowerSpectrum[i]) PowerSpectrum_YMaximum = GetResult.PowerSpectrum[i] + 0.1;
            //    if (PowerSpectrum_YMinimum > GetResult.PowerSpectrum[i]) PowerSpectrum_YMinimum = GetResult.PowerSpectrum[i] - 0.1;
            //}
            //PowerSpectrum_chart.ChartAreas[0].AxisY.Maximum = Math.Round(PowerSpectrum_YMaximum, 3);
            //PowerSpectrum_chart.ChartAreas[0].AxisY.Minimum = Math.Round(PowerSpectrum_YMinimum, 3);
            //SubMaxMin = (double)decimal.Round((decimal)(PowerSpectrum_chart.ChartAreas[0].AxisY.Maximum - PowerSpectrum_chart.ChartAreas[0].AxisY.Minimum), 3);
            //PowerSpectrum_chart.ChartAreas[0].AxisY.Interval = Math.Round(SubMaxMin / 5, 3);



            //ReflectanceSpectrum
            for (int i = 0; i < SF3.GetResult.ReflectanceSpectrum.Count; i++)
            {
                ReflectanceSpectrum_chart.Series[0].Points.AddXY(i, GetResult.ReflectanceSpectrum[i]);
                if (i == 0)
                {
                    ReflectanceSpectrum_YMaximum = GetResult.ReflectanceSpectrum[i] + 0.01;
                    ReflectanceSpectrum_YMinimum = GetResult.ReflectanceSpectrum[i] - 0.01;
                }
                if (ReflectanceSpectrum_YMaximum < GetResult.ReflectanceSpectrum[i]) ReflectanceSpectrum_YMaximum = GetResult.ReflectanceSpectrum[i] + 0.1;
                if (ReflectanceSpectrum_YMinimum > GetResult.ReflectanceSpectrum[i]) ReflectanceSpectrum_YMinimum = GetResult.ReflectanceSpectrum[i] - 0.1;
            }
            ReflectanceSpectrum_chart.ChartAreas[0].AxisY.Maximum = Math.Round(ReflectanceSpectrum_YMaximum, 3);
            ReflectanceSpectrum_chart.ChartAreas[0].AxisY.Minimum = Math.Round(ReflectanceSpectrum_YMinimum, 3);
            SubMaxMin = (double)decimal.Round((decimal)(ReflectanceSpectrum_chart.ChartAreas[0].AxisY.Maximum - ReflectanceSpectrum_chart.ChartAreas[0].AxisY.Minimum), 3);
            ReflectanceSpectrum_chart.ChartAreas[0].AxisY.Interval = Math.Round(SubMaxMin / 5, 3);

            //Sample
            for (int i = 0; i < SF3.GetResult.SignalSpectrum.Count; i++) //MeasDark.SampleDarkSpectrum
            {
                SampleSpectrum_chart.Series[0].Points.AddXY(i, GetResult.SignalSpectrum[i]);
                if (i == 0)
                {
                    SampleSpectrum_YMaximum = GetResult.SignalSpectrum[i] + 0.01;
                    SampleSpectrum_YMinimum = GetResult.SignalSpectrum[i] - 0.01;
                }
                if (SampleSpectrum_YMaximum < GetResult.SignalSpectrum[i]) SampleSpectrum_YMaximum = GetResult.SignalSpectrum[i] + 0.1;
                if (SampleSpectrum_YMinimum > GetResult.SignalSpectrum[i]) SampleSpectrum_YMinimum = GetResult.SignalSpectrum[i] - 0.1;
            }
            SampleSpectrum_chart.ChartAreas[0].AxisY.Maximum = Math.Round(SampleSpectrum_YMaximum, 3);
            SampleSpectrum_chart.ChartAreas[0].AxisY.Minimum = Math.Round(SampleSpectrum_YMinimum, 3);
            SubMaxMin = (double)decimal.Round((decimal)(SampleSpectrum_chart.ChartAreas[0].AxisY.Maximum - SampleSpectrum_chart.ChartAreas[0].AxisY.Minimum), 3);
            SampleSpectrum_chart.ChartAreas[0].AxisY.Interval = Math.Round(SubMaxMin / 5, 3);

            //while (GetResult.SignalSpectrum[i] > SampleSpectrum_Maximum)
            //{
            //    SampleSpectrum_Maximum = Math.Round(SampleSpectrum_Maximum + 0.01, 3);
            //}

            //Reference
            for (int i = 0; i < SF3.GetReference.ReferenceSignalSpectrum.Count; i++)
            {
                ReferenceSpectrum_chart.Series[0].Points.AddXY(i, SF3.GetReference.ReferenceSignalSpectrum[i]);
                if (i == 0)
                {
                    ReferenceSpectrum_YMaximum = GetReference.ReferenceSignalSpectrum[i] + 0.01;
                    ReferenceSpectrum_YMinimum = GetReference.ReferenceSignalSpectrum[i] - 0.01;
                }
                if (ReferenceSpectrum_YMaximum < GetReference.ReferenceSignalSpectrum[i]) ReferenceSpectrum_YMaximum = GetReference.ReferenceSignalSpectrum[i] + 0.1;
                if (ReferenceSpectrum_YMinimum > GetReference.ReferenceSignalSpectrum[i]) ReferenceSpectrum_YMinimum = GetReference.ReferenceSignalSpectrum[i] - 0.1;
            }
            ReferenceSpectrum_chart.ChartAreas[0].AxisY.Maximum = Math.Round(ReferenceSpectrum_YMaximum, 3);
            ReferenceSpectrum_chart.ChartAreas[0].AxisY.Minimum = Math.Round(ReferenceSpectrum_YMinimum, 3);
            SubMaxMin = (double)decimal.Round((decimal)(ReferenceSpectrum_chart.ChartAreas[0].AxisY.Maximum - ReferenceSpectrum_chart.ChartAreas[0].AxisY.Minimum), 3);
            ReferenceSpectrum_chart.ChartAreas[0].AxisY.Interval = Math.Round(SubMaxMin / 5, 3);

            txt_Thickness.Text = "Thickness:";// + Convert.ToString(Math.Round(GetResult.Thickness[0], 3)) + "[um]";
            for (int i = 0; i < GetResult.Thickness.Count; i++) txt_Thickness.Text += Convert.ToString(Math.Round(GetResult.Thickness[i], 3) + "[um],");
            txt_Thickness.Text = txt_Thickness.Text.Trim(',');

            txt_SignalMax.Text = "Signal max:" + Convert.ToString(Math.Round(GetResult.SignalMaximum, 3));
            txt_ExposureTime.Text = "Elapsed time:" + Convert.ToString(Math.Round(ElapsedTime, 3)) + "[sec]";
            txt_NumOfMeas.Text = "Num. of meas.:" + Convert.ToString(count);

            ElapsedTime += GetResult.ElapsedTime / 1000.0;

        }
        private void Meastimer_Tick(object sender, EventArgs e)
        {
            //給SF3
            SF3.Action_CmdGetResult();

            //開始前歸零
            PowerSpectrum_Init_chart();
            ReflectanceSpectrum_Init_chart();
            SampleSpectrum_Init_chart();
            ReferenceSpectrum_Init_chart();

            //Thickness
            if (Chart_Max < Chart_Value + GetResult.ElapsedTime / 1000.0)
            {
                Thickness_chart.ChartAreas[0].AxisX.Maximum += GetResult.ElapsedTime / 1000.0;
                Thickness_chart.ChartAreas[0].AxisX.Minimum += GetResult.ElapsedTime / 1000.0;
                Chart_Max = Convert.ToDouble(Thickness_chart.ChartAreas[0].AxisX.Maximum);
                Chart_Min = Convert.ToDouble(Thickness_chart.ChartAreas[0].AxisX.Minimum);
            }
            Chart_Value = Math.Round(ElapsedTime, 3);
            for (int i = 0; i < layerNum; i++)
            {
                Thickness_chart.Series[i].Points.AddXY(Chart_Value, GetResult.Thickness[i]);
                if(layerNum > 1)
                {
                    if (count == 1)
                    {
                        Thickness_XMaximum = GetResult.Thickness[i] + 100;
                        Thickness_XMinimum = GetResult.Thickness[i] - 100;
                    }
                    if (Thickness_XMaximum < GetResult.Thickness[i]) Thickness_XMaximum = GetResult.Thickness[i] + 100;
                    if (Thickness_XMinimum > GetResult.Thickness[i]) Thickness_XMinimum = GetResult.Thickness[i] - 100;
                }
                else
                {
                    if (count == 1)
                    {
                        Thickness_XMaximum = GetResult.Thickness[i] + 0.01;
                        Thickness_XMinimum = GetResult.Thickness[i] - 0.01;
                    }
                    if (Thickness_XMaximum < GetResult.Thickness[i]) Thickness_XMaximum = GetResult.Thickness[i] + 0.01;
                    if (Thickness_XMinimum > GetResult.Thickness[i]) Thickness_XMinimum = GetResult.Thickness[i] - 0.01;
                }
            }
            Thickness_chart.ChartAreas[0].AxisY.Maximum = Math.Round(Thickness_XMaximum, 3);
            Thickness_chart.ChartAreas[0].AxisY.Minimum = Math.Round(Thickness_XMinimum, 3);
            SubMaxMin = (double)decimal.Round((decimal)(Thickness_chart.ChartAreas[0].AxisY.Maximum - Thickness_chart.ChartAreas[0].AxisY.Minimum), 3);
            Thickness_chart.ChartAreas[0].AxisY.Interval = Math.Round(SubMaxMin / 5, 3);

            //PowerSpectrum
            XValue = PowerSpectrum_XMaximum / SF3.GetResult.PowerSpectrum.Count;
            for (int i = 0; i < SF3.GetResult.PowerSpectrum.Count; i++)
            {
                PowerSpectrum_chart.Series[0].Points.AddXY(i * XValue, GetResult.PowerSpectrum[i]);
            }
            //ReflectanceSpectrum
            for (int i = 0; i < SF3.GetResult.ReflectanceSpectrum.Count; i++)
            {
                ReflectanceSpectrum_chart.Series[0].Points.AddXY(i, GetResult.ReflectanceSpectrum[i]);
                if (i == 0)
                {
                    ReflectanceSpectrum_YMaximum = GetResult.ReflectanceSpectrum[i] + 0.01;
                    ReflectanceSpectrum_YMinimum = GetResult.ReflectanceSpectrum[i] - 0.01;
                }
                if (ReflectanceSpectrum_YMaximum < GetResult.ReflectanceSpectrum[i]) ReflectanceSpectrum_YMaximum = GetResult.ReflectanceSpectrum[i] + 0.1;
                if (ReflectanceSpectrum_YMinimum > GetResult.ReflectanceSpectrum[i]) ReflectanceSpectrum_YMinimum = GetResult.ReflectanceSpectrum[i] - 0.1;
            }
            ReflectanceSpectrum_chart.ChartAreas[0].AxisY.Maximum = Math.Round(ReflectanceSpectrum_YMaximum, 3);
            ReflectanceSpectrum_chart.ChartAreas[0].AxisY.Minimum = Math.Round(ReflectanceSpectrum_YMinimum, 3);
            SubMaxMin = (double)decimal.Round((decimal)(ReflectanceSpectrum_chart.ChartAreas[0].AxisY.Maximum - ReflectanceSpectrum_chart.ChartAreas[0].AxisY.Minimum), 3);
            ReflectanceSpectrum_chart.ChartAreas[0].AxisY.Interval = Math.Round(SubMaxMin / 5, 3);

            //Sample
            for (int i = 0; i < SF3.GetResult.SignalSpectrum.Count; i++) //MeasDark.SampleDarkSpectrum
            {
                SampleSpectrum_chart.Series[0].Points.AddXY(i, GetResult.SignalSpectrum[i]);
                if (i == 0)
                {
                    SampleSpectrum_YMaximum = GetResult.SignalSpectrum[i] + 0.01;
                    SampleSpectrum_YMinimum = GetResult.SignalSpectrum[i] - 0.01;
                }
                if (SampleSpectrum_YMaximum < GetResult.SignalSpectrum[i]) SampleSpectrum_YMaximum = GetResult.SignalSpectrum[i] + 0.1;
                if (SampleSpectrum_YMinimum > GetResult.SignalSpectrum[i]) SampleSpectrum_YMinimum = GetResult.SignalSpectrum[i] - 0.1;
            }
            SampleSpectrum_chart.ChartAreas[0].AxisY.Maximum = Math.Round(SampleSpectrum_YMaximum, 3);
            SampleSpectrum_chart.ChartAreas[0].AxisY.Minimum = Math.Round(SampleSpectrum_YMinimum, 3);
            SubMaxMin = (double)decimal.Round((decimal)(SampleSpectrum_chart.ChartAreas[0].AxisY.Maximum - SampleSpectrum_chart.ChartAreas[0].AxisY.Minimum), 3);
            SampleSpectrum_chart.ChartAreas[0].AxisY.Interval = Math.Round(SubMaxMin / 5, 3);

            //Reference
            for (int i = 0; i < SF3.GetReference.ReferenceSignalSpectrum.Count; i++)
            {
                ReferenceSpectrum_chart.Series[0].Points.AddXY(i, GetReference.ReferenceSignalSpectrum[i]);
                if (i == 0)
                {
                    ReferenceSpectrum_YMaximum = GetReference.ReferenceSignalSpectrum[i] + 0.01;
                    ReferenceSpectrum_YMinimum = GetReference.ReferenceSignalSpectrum[i] - 0.01;
                }
                if (ReferenceSpectrum_YMaximum < GetReference.ReferenceSignalSpectrum[i]) ReferenceSpectrum_YMaximum = GetReference.ReferenceSignalSpectrum[i] + 0.1;
                if (ReferenceSpectrum_YMinimum > GetReference.ReferenceSignalSpectrum[i]) ReferenceSpectrum_YMinimum = GetReference.ReferenceSignalSpectrum[i] - 0.1;
            }
            ReferenceSpectrum_chart.ChartAreas[0].AxisY.Maximum = Math.Round(ReferenceSpectrum_YMaximum, 3);
            ReferenceSpectrum_chart.ChartAreas[0].AxisY.Minimum = Math.Round(ReferenceSpectrum_YMinimum, 3);
            SubMaxMin = (double)decimal.Round((decimal)(ReferenceSpectrum_chart.ChartAreas[0].AxisY.Maximum - ReferenceSpectrum_chart.ChartAreas[0].AxisY.Minimum), 3);
            ReferenceSpectrum_chart.ChartAreas[0].AxisY.Interval = Math.Round(SubMaxMin / 5, 3);
            
            
            txt_Thickness.Text = "Thickness:";// + Convert.ToString(Math.Round(GetResult.Thickness[0], 3)) + "[um]";
            for (int i = 0; i < GetResult.Thickness.Count; i++) txt_Thickness.Text += Convert.ToString(Math.Round(GetResult.Thickness[i], 3) + "[um],");
            txt_Thickness.Text = txt_Thickness.Text.Trim(',');

            txt_SignalMax.Text = "Signal max:" + Convert.ToString(Math.Round(GetResult.SignalMaximum, 3));
            txt_ExposureTime.Text = "Elapsed time:" + Convert.ToString(Math.Round(ElapsedTime, 3)) + "[sec] ";
            txt_NumOfMeas.Text = "Num. of meas.:" + Convert.ToString(count);

            ElapsedTime += GetResult.ElapsedTime / 1000.0;
            count++;
            if(count == Meas_Count + 1)
            {
                SF3.Action_CmdMeasStop();
                btn_Recipes.Enabled = true;
                btn_StartMeas.Enabled = true;
                Meastimer.Enabled = false;
            }
        }
        #endregion

        #region Init_chart
        private void Series_Clear(int layer)
        {
            for (int i = 1; i < layer; i++)
            {
                Thickness_chart.Series[i].Points.Clear();
                Thickness_chart.Series[i].Points.AddXY(0, 0);
            }
        }
        private void Thickness_Init_chart()
        {
            //SF3.Action_CmdGetAnalysisThicknessRange();

            //每次使用此function前先清除圖表
            Thickness_chart.Series[0].Points.Clear();
            //無資料畫面
            Thickness_chart.Series[0].Color = Color.Blue;                 //設定線條顏色
            Thickness_chart.Series[0].ToolTip = "#VALX,#VALY";            //鼠標停留在數據上，顯示XY值
            Thickness_chart.Series[0].ChartType = SeriesChartType.FastLine;   //設定線條種類 折線圖
            Thickness_chart.Series[0].YAxisType = AxisType.Primary;           //主坐標軸
            Thickness_chart.Series[0].BorderWidth = 2;
            Thickness_chart.ChartAreas[0].AxisX.Maximum = 10;  //設定X軸最大值
            Thickness_chart.ChartAreas[0].AxisX.Minimum = 0;     //設定X軸最小值
            Thickness_chart.ChartAreas[0].AxisX.Interval = Thickness_chart.ChartAreas[0].AxisX.Maximum / 5; //設定X軸間隔 最大值/5
            //double min = 100000, max = 0;
            //for (int i = 0; i < CommonResult.GetAnalysisThicknessRange_DataQuantity; i++)
            //{
            //    if (min > Convert.ToDouble(CommonResult.GetAnalysisThicknessRange_ThicknessRangeLowerLimit[i])) min = Convert.ToDouble(CommonResult.GetAnalysisThicknessRange_ThicknessRangeLowerLimit[i]);
            //    if (max < Convert.ToDouble(CommonResult.GetAnalysisThicknessRange_ThicknessRangeUpperLimit[i])) max = Convert.ToDouble(CommonResult.GetAnalysisThicknessRange_ThicknessRangeUpperLimit[i]);
            //}
            Thickness_chart.ChartAreas[0].AxisY.Maximum = Math.Round(Thickness_XMaximum, 3);  //設定Y軸最大值 //max
            Thickness_chart.ChartAreas[0].AxisY.Minimum = Math.Round(Thickness_XMinimum, 3);   //設定Y軸最小值 //min
            Thickness_chart.ChartAreas[0].AxisY.Interval = Math.Round(Thickness_chart.ChartAreas[0].AxisY.Maximum / 5, 3); //設定Y軸間隔 最大值/5
            Chart_Max = Thickness_chart.ChartAreas[0].AxisX.Maximum;
            Chart_Min = Thickness_chart.ChartAreas[0].AxisX.Minimum;
            Chart_Value = 0;

            //X軸
            Thickness_chart.ChartAreas[0].AxisX.Title = "Time[sec]";
            Thickness_chart.ChartAreas[0].AxisX.TitleFont = new Font(Thickness_chart.ChartAreas[0].AxisX.Name, 20);
            Thickness_chart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;           //啟用縮放視圖
            Thickness_chart.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            Thickness_chart.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;//啟用X軸滾動條按鈕
            Thickness_chart.ChartAreas[0].CursorX.IsUserEnabled = true;
            Thickness_chart.ChartAreas[0].CursorX.AutoScroll = true;
            Thickness_chart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            Thickness_chart.ChartAreas[0].CursorX.SelectionColor = System.Drawing.SystemColors.Highlight;
            //Y軸
            Thickness_chart.ChartAreas[0].AxisY.Title = "Thickness[um]";
            Thickness_chart.ChartAreas[0].AxisY.TitleFont = new Font(Thickness_chart.ChartAreas[0].AxisY.Name, 14);
            Thickness_chart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            Thickness_chart.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
            Thickness_chart.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;
            Thickness_chart.ChartAreas[0].CursorY.IsUserEnabled = true;
            Thickness_chart.ChartAreas[0].CursorY.AutoScroll = true;
            Thickness_chart.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            Thickness_chart.ChartAreas[0].CursorY.SelectionColor = System.Drawing.SystemColors.Highlight;

            Thickness_chart.Series[0].Points.AddXY(0, 0);
        }
        private void PowerSpectrum_Init_chart()
        {
            PowerSpectrum_XMaximum = CommonResult.GetFFTMaxThickness_FFTMaximumThickness;
            //每次使用此function前先清除圖表
            PowerSpectrum_chart.Series[0].Points.Clear();
            //無資料畫面
            PowerSpectrum_chart.Series[0].Color = Color.Blue;                 //設定線條顏色
            PowerSpectrum_chart.Series[0].ToolTip = "#VALX,#VALY";            //鼠標停留在數據上，顯示XY值
            PowerSpectrum_chart.Series[0].ChartType = SeriesChartType.FastLine;   //設定線條種類 折線圖
            PowerSpectrum_chart.Series[0].YAxisType = AxisType.Primary;           //主坐標軸
            PowerSpectrum_chart.ChartAreas[0].AxisX.Maximum = PowerSpectrum_XMaximum;
            PowerSpectrum_chart.ChartAreas[0].AxisX.Minimum = 0;     //設定X軸最小值
            PowerSpectrum_chart.ChartAreas[0].AxisX.Interval = PowerSpectrum_chart.ChartAreas[0].AxisX.Maximum / 5; //設定X軸間隔 最大值/5
            
            
            PowerSpectrum_chart.ChartAreas[0].AxisY.Maximum = 1.05;  //設定Y軸最大值 //max
            PowerSpectrum_chart.ChartAreas[0].AxisY.Minimum = -0.05;     //設定Y軸最小值 //min
            PowerSpectrum_chart.ChartAreas[0].AxisY.Interval = Math.Round(PowerSpectrum_chart.ChartAreas[0].AxisY.Maximum / 5, 3); //設定Y軸間隔 最大值/5

            //X軸
            PowerSpectrum_chart.ChartAreas[0].AxisX.Title = "Thickness[um]";
            PowerSpectrum_chart.ChartAreas[0].AxisX.TitleFont = new Font(PowerSpectrum_chart.ChartAreas[0].AxisX.Name, 20);
            PowerSpectrum_chart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;           //啟用縮放視圖
            PowerSpectrum_chart.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            PowerSpectrum_chart.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;//啟用X軸滾動條按鈕
            PowerSpectrum_chart.ChartAreas[0].CursorX.IsUserEnabled = true;
            PowerSpectrum_chart.ChartAreas[0].CursorX.AutoScroll = true;
            PowerSpectrum_chart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            PowerSpectrum_chart.ChartAreas[0].CursorX.SelectionColor = System.Drawing.SystemColors.Highlight;
            //Y軸
            PowerSpectrum_chart.ChartAreas[0].AxisY.Title = "Power spectrum";
            PowerSpectrum_chart.ChartAreas[0].AxisY.TitleFont = new Font(PowerSpectrum_chart.ChartAreas[0].AxisY.Name, 14);
            PowerSpectrum_chart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            PowerSpectrum_chart.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
            PowerSpectrum_chart.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;
            PowerSpectrum_chart.ChartAreas[0].CursorY.IsUserEnabled = true;
            PowerSpectrum_chart.ChartAreas[0].CursorY.AutoScroll = true;
            PowerSpectrum_chart.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            PowerSpectrum_chart.ChartAreas[0].CursorY.SelectionColor = System.Drawing.SystemColors.Highlight;

            PowerSpectrum_chart.Series[0].Points.AddXY(0, 0);
        }
        private void ReflectanceSpectrum_Init_chart()
        {
            //每次使用此function前先清除圖表
            ReflectanceSpectrum_chart.Series[0].Points.Clear();
            //無資料畫面
            ReflectanceSpectrum_chart.Series[0].Color = Color.Blue;                 //設定線條顏色
            ReflectanceSpectrum_chart.Series[0].ToolTip = "#VALX,#VALY";            //鼠標停留在數據上，顯示XY值
            ReflectanceSpectrum_chart.Series[0].ChartType = SeriesChartType.FastLine;   //設定線條種類 折線圖
            ReflectanceSpectrum_chart.Series[0].YAxisType = AxisType.Primary;           //主坐標軸

            ReflectanceSpectrum_chart.ChartAreas[0].AxisX.Maximum = 511;
            ReflectanceSpectrum_chart.ChartAreas[0].AxisX.Minimum = 0;
            ReflectanceSpectrum_chart.ChartAreas[0].AxisX.Interval = Math.Round(ReflectanceSpectrum_chart.ChartAreas[0].AxisX.Maximum / 5, 3);
            ReflectanceSpectrum_chart.ChartAreas[0].AxisY.Maximum = Math.Round(ReflectanceSpectrum_YMaximum, 3);
            ReflectanceSpectrum_chart.ChartAreas[0].AxisY.Minimum = Math.Round(ReflectanceSpectrum_YMinimum, 3);
            ReflectanceSpectrum_chart.ChartAreas[0].AxisY.Interval = Math.Round(ReflectanceSpectrum_chart.ChartAreas[0].AxisY.Maximum / 5, 3);

            //X軸
            ReflectanceSpectrum_chart.ChartAreas[0].AxisX.Title = "Wavelength number";
            ReflectanceSpectrum_chart.ChartAreas[0].AxisX.TitleFont = new Font(ReflectanceSpectrum_chart.ChartAreas[0].AxisX.Name, 20);
            ReflectanceSpectrum_chart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;           //啟用縮放視圖
            ReflectanceSpectrum_chart.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            ReflectanceSpectrum_chart.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;//啟用X軸滾動條按鈕
            ReflectanceSpectrum_chart.ChartAreas[0].CursorX.IsUserEnabled = true;
            ReflectanceSpectrum_chart.ChartAreas[0].CursorX.AutoScroll = true;
            ReflectanceSpectrum_chart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            ReflectanceSpectrum_chart.ChartAreas[0].CursorX.SelectionColor = System.Drawing.SystemColors.Highlight;
            //Y軸
            ReflectanceSpectrum_chart.ChartAreas[0].AxisY.Title = "Reflectance";
            ReflectanceSpectrum_chart.ChartAreas[0].AxisY.TitleFont = new Font(ReflectanceSpectrum_chart.ChartAreas[0].AxisY.Name, 14);
            ReflectanceSpectrum_chart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            ReflectanceSpectrum_chart.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
            ReflectanceSpectrum_chart.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;
            ReflectanceSpectrum_chart.ChartAreas[0].CursorY.IsUserEnabled = true;
            ReflectanceSpectrum_chart.ChartAreas[0].CursorY.AutoScroll = true;
            ReflectanceSpectrum_chart.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            ReflectanceSpectrum_chart.ChartAreas[0].CursorY.SelectionColor = System.Drawing.SystemColors.Highlight;

            ReflectanceSpectrum_chart.Series[0].Points.AddXY(0, 0);
        }
        private void SampleSpectrum_Init_chart()
        {
            //每次使用此function前先清除圖表
            SampleSpectrum_chart.Series[0].Points.Clear();
            //無資料畫面
            SampleSpectrum_chart.Series[0].Color = Color.Blue;                 //設定線條顏色
            SampleSpectrum_chart.Series[0].ToolTip = "#VALX,#VALY";            //鼠標停留在數據上，顯示XY值
            SampleSpectrum_chart.Series[0].ChartType = SeriesChartType.FastLine;   //設定線條種類 折線圖
            SampleSpectrum_chart.Series[0].YAxisType = AxisType.Primary;           //主坐標軸
            SampleSpectrum_chart.ChartAreas[0].AxisX.Maximum = 511;
            SampleSpectrum_chart.ChartAreas[0].AxisX.Minimum = 0;
            SampleSpectrum_chart.ChartAreas[0].AxisX.Interval = Math.Round(SampleSpectrum_chart.ChartAreas[0].AxisX.Maximum / 5, 3);
            SampleSpectrum_chart.ChartAreas[0].AxisY.Maximum = Math.Round(SampleSpectrum_YMaximum, 3);
            SampleSpectrum_chart.ChartAreas[0].AxisY.Minimum = Math.Round(SampleSpectrum_YMinimum, 3);
            SampleSpectrum_chart.ChartAreas[0].AxisY.Interval = Math.Round(SampleSpectrum_chart.ChartAreas[0].AxisY.Maximum / 5, 3);
            
            //X軸
            SampleSpectrum_chart.ChartAreas[0].AxisX.Title = "Wavelength number";
            SampleSpectrum_chart.ChartAreas[0].AxisX.TitleFont = new Font(SampleSpectrum_chart.ChartAreas[0].AxisX.Name, 20);
            SampleSpectrum_chart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;           //啟用縮放視圖
            SampleSpectrum_chart.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            SampleSpectrum_chart.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;//啟用X軸滾動條按鈕
            SampleSpectrum_chart.ChartAreas[0].CursorX.IsUserEnabled = true;
            SampleSpectrum_chart.ChartAreas[0].CursorX.AutoScroll = true;
            SampleSpectrum_chart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            SampleSpectrum_chart.ChartAreas[0].CursorX.SelectionColor = System.Drawing.SystemColors.Highlight;
            //Y軸
            SampleSpectrum_chart.ChartAreas[0].AxisY.Title = "Signal - Dark";
            SampleSpectrum_chart.ChartAreas[0].AxisY.TitleFont = new Font(SampleSpectrum_chart.ChartAreas[0].AxisY.Name, 14);
            SampleSpectrum_chart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            SampleSpectrum_chart.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
            SampleSpectrum_chart.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;
            SampleSpectrum_chart.ChartAreas[0].CursorY.IsUserEnabled = true;
            SampleSpectrum_chart.ChartAreas[0].CursorY.AutoScroll = true;
            SampleSpectrum_chart.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            SampleSpectrum_chart.ChartAreas[0].CursorY.SelectionColor = System.Drawing.SystemColors.Highlight;

            SampleSpectrum_chart.Series[0].Points.AddXY(0, 0);
        }
        private void ReferenceSpectrum_Init_chart()
        {
            //SF3.Action_CmdGetAnalysisRange();
            //每次使用此function前先清除圖表
            ReferenceSpectrum_chart.Series[0].Points.Clear();
            //無資料畫面
            ReferenceSpectrum_chart.Series[0].Color = Color.Blue;                 //設定線條顏色
            ReferenceSpectrum_chart.Series[0].ToolTip = "#VALX,#VALY";            //鼠標停留在數據上，顯示XY值
            ReferenceSpectrum_chart.Series[0].ChartType = SeriesChartType.FastLine;   //設定線條種類 折線圖
            ReferenceSpectrum_chart.Series[0].YAxisType = AxisType.Primary;           //主坐標軸
            ReferenceSpectrum_chart.ChartAreas[0].AxisX.Maximum = 511;
            ReferenceSpectrum_chart.ChartAreas[0].AxisX.Minimum = 0;
            ReferenceSpectrum_chart.ChartAreas[0].AxisX.Interval = Math.Round(ReferenceSpectrum_chart.ChartAreas[0].AxisX.Maximum / 5, 3);
            ReferenceSpectrum_chart.ChartAreas[0].AxisY.Maximum = Math.Round(ReferenceSpectrum_YMaximum, 3);
            ReferenceSpectrum_chart.ChartAreas[0].AxisY.Minimum = Math.Round(ReferenceSpectrum_YMinimum, 3);
            ReferenceSpectrum_chart.ChartAreas[0].AxisY.Interval = Math.Round(ReferenceSpectrum_chart.ChartAreas[0].AxisY.Maximum / 5, 3);

            //X軸
            ReferenceSpectrum_chart.ChartAreas[0].AxisX.Title = "Wavelength number";
            ReferenceSpectrum_chart.ChartAreas[0].AxisX.TitleFont = new Font(ReferenceSpectrum_chart.ChartAreas[0].AxisX.Name, 20);
            ReferenceSpectrum_chart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;           //啟用縮放視圖
            ReferenceSpectrum_chart.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            ReferenceSpectrum_chart.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;//啟用X軸滾動條按鈕
            ReferenceSpectrum_chart.ChartAreas[0].CursorX.IsUserEnabled = true;
            ReferenceSpectrum_chart.ChartAreas[0].CursorX.AutoScroll = true;
            ReferenceSpectrum_chart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            ReferenceSpectrum_chart.ChartAreas[0].CursorX.SelectionColor = System.Drawing.SystemColors.Highlight;
            //Y軸
            ReferenceSpectrum_chart.ChartAreas[0].AxisY.Title = "Signal - Dark";
            ReferenceSpectrum_chart.ChartAreas[0].AxisY.TitleFont = new Font(ReferenceSpectrum_chart.ChartAreas[0].AxisY.Name, 14);
            ReferenceSpectrum_chart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            ReferenceSpectrum_chart.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
            ReferenceSpectrum_chart.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;
            ReferenceSpectrum_chart.ChartAreas[0].CursorY.IsUserEnabled = true;
            ReferenceSpectrum_chart.ChartAreas[0].CursorY.AutoScroll = true;
            ReferenceSpectrum_chart.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            ReferenceSpectrum_chart.ChartAreas[0].CursorY.SelectionColor = System.Drawing.SystemColors.Highlight;

            ReferenceSpectrum_chart.Series[0].Points.AddXY(0, 0);
        }
        #endregion

        #region Button
        private void btn_Recipes_Click(object sender, EventArgs e)
        {
            btn_StartMonitor.Enabled = true;
            btn_StartMeas.Enabled = true;
            btn_AbortMeas.Enabled = false;
            Series_Clear(layerNum);
            Thickness_Init_chart();
            PowerSpectrum_Init_chart();
            ReflectanceSpectrum_Init_chart();
            SampleSpectrum_Init_chart();
            ReferenceSpectrum_Init_chart();
            count = 1;
            Chart_Value = 0;
            ElapsedTime = 0;
            txt_Thickness.Text = "Thickness:0.00[um]";
            txt_SignalMax.Text = "Signal max:0.00";
            txt_ExposureTime.Text = "Elapsed time:0.00[sec]";
            SF3.ShowRecipeUI();
        }
        private void btn_StartMonitor_Click(object sender, EventArgs e)
        {
            //開始前歸零
            Series_Clear(layerNum);
            Thickness_Init_chart();
            PowerSpectrum_Init_chart();
            ReflectanceSpectrum_Init_chart();
            SampleSpectrum_Init_chart();
            ReferenceSpectrum_Init_chart();

            btn_Recipes.Enabled = false;
            btn_StartMonitor.Enabled = false;
            btn_StartMeas.Enabled = false;
            btn_AbortMeas.Enabled = true;
            //SF3.Action_CmdLoadReference();
            //SF3.Action_CmdGetReference();
            //SF3.Action_CmdReady();
            SF3.Action_CmdCtrlLight(Common.CmdCtrlLight.Light);
            SF3.Action_CmdMeasDark();
            SF3.Action_CmdMeasStart();
            Monitorttimer.Enabled = true;
        }
        private void btn_StartMeas_Click(object sender, EventArgs e)
        {
            btn_Recipes.Enabled = false;
            btn_StartMonitor.Enabled = false;
            btn_StartMeas.Enabled = false;
            btn_AbortMeas.Enabled = true;
            //開始前歸零
            SF3.Action_CmdGetAnalysisNum();
            layerNum = CommonResult.GetAnalysisNum_AnalysisLayerNo;
            Series_Clear(layerNum);
            Thickness_Init_chart();
            PowerSpectrum_Init_chart();
            ReflectanceSpectrum_Init_chart();
            SampleSpectrum_Init_chart();
            ReferenceSpectrum_Init_chart();
            count = 1;
            Chart_Value = 0;
            ElapsedTime = 0;
            txt_Thickness.Text = "Thickness:0.00[um]";
            txt_SignalMax.Text = "Signal max:0.00";
            txt_ExposureTime.Text = "Elapsed time:0.00[sec]";
            //開始量測
            //SF3.Action_CmdReady();
            SF3.Action_CmdCtrlLight(Common.CmdCtrlLight.Light);
            SF3.Action_CmdMeasDark();
            SF3.Action_CmdMeasStart();
            Meastimer.Enabled = true;
        }
        private void btn_AbortMeas_Click(object sender, EventArgs e)
        {
            Meastimer.Enabled = false;
            Monitorttimer.Enabled = false;
            if (!btn_StartMeas.Enabled) SF3.Action_CmdMeasStop();
            btn_Recipes.Enabled = true;
            btn_StartMonitor.Enabled = true;
            btn_StartMeas.Enabled = true;
            btn_AbortMeas.Enabled = false;
            Series_Clear(layerNum);
            Thickness_Init_chart();
            PowerSpectrum_Init_chart();
            ReflectanceSpectrum_Init_chart();
            SampleSpectrum_Init_chart();
            ReferenceSpectrum_Init_chart();
            count = 1;
            Chart_Value = 0;
            ElapsedTime = 0;
            txt_Thickness.Text = "Thickness:0.00[um]";
            txt_SignalMax.Text = "Signal max:0.00";
            txt_ExposureTime.Text = "Elapsed time:0.00[sec]";
        }
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        #endregion
        private void num_MaxMeasCount_ValueChanged(object sender, EventArgs e)
        {
            Meas_Count = Convert.ToInt32(num_MaxMeasCount.Value);
        }

    }
}
