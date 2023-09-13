using CsvHelper;
using Otsuka;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using System.Xml.Linq;
using static Otsuka.Common;
using static Otsuka.SF3;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using ListViewItem = System.Windows.Forms.ListViewItem;
using TextBox = System.Windows.Forms.TextBox;

namespace SF3_Form
{

    public partial class Recipes : Form
    {
        #region Parameter

        //Form_Parameter
        Otsuka.SF3 SF3;
        public int AmbientLayerCount = 5;
        public int OptimLayerModel_NumOfLayers = 0;
        public int CmdSetResultType_enum_num = 0;
        public int CmdSetMeasMode_enum_num = 0;
        public int Substrate_MaterialID = 0;
        public int CmdSetOptimLayerModel_LayerMaterialID_Num = 0;
        public float Substrate_MaterialThickness = 0;
        public string Recipe_ID = "";
        public string Recipe_Name = "";
        public string Recipe_Memo = "";
        public string Recipe_DateCreated = "";
        public string Recipe_DateModified = "";
        public string CSVName = "";
        public List<string> Recipes_list = new List<string>();
        //Main settings
        public List<float> list_AnalysisThicknessRange_ThicknessRangeLowerLimit = new List<float>();
        public List<float> list_AnalysisThicknessRange_ThicknessRangeUpperLimit = new List<float>();
        public List<float> list_FFTPowerRange_FFTIntensityRangeLowerLimit = new List<float>();
        public List<float> list_FFTPowerRange_FFTIntensityRangeUpperLimit = new List<float>();
        public List<int> list_FFTSearchDirection_list = new List<int>();
        public List<float> list_AnalysisRefractiveIndex_ConstantRefractiveIndex = new List<float>();
        public List<int> list_FFTPeakNum_PeakNumber = new List<int>();
        //Detail sttings
        public List<int> list_ThicknessCoef_list = new List<int>();
        public List<float> list_ThicknessCoef_PrimaryCoefficientC1 = new List<float>();
        public List<float> list_ThicknessCoef_ConstantCoefficientC0 = new List<float>();
        //Detail sttings (Optimization method)
        public List<int> list_OptimLayerModel_LayerMaterialID = new List<int>();
        public List<float> list_OptimLayerModel_Thickness = new List<float>();
        #endregion

        public Recipes(Otsuka.SF3 sF3)
        {
            SF3 = sF3;
            InitializeComponent();
            this.ControlBox = false;
            this.tabPage4.Parent = null;//Detail settings隱藏
            this.tabPage5.Parent = null;//Detail settings (Optimization method)隱藏
        }
        private void Recipes_Load(object sender, EventArgs e)
        {
            //對所有dataGridView加上一個欄位
            for (int i = 1; i <= num_NumOfAnalysisLayers.Value; i++)
            {
                dataGridView_Mainsetting.Rows.Add();
                dataGridView_DetailSetting.Rows.Add();
            }
            for (int i = 0; i < AmbientLayerCount; i++)
            {
                dataGridView1.Rows.Add();
            }

            //設定listview模板
            lv_Recipes.Clear();
            lv_Recipes.View = View.Details;
            lv_Recipes.GridLines = true;
            lv_Recipes.FullRowSelect = true;
            lv_Recipes.Scrollable = true;
            lv_Recipes.MultiSelect = false;
            lv_Recipes.Columns.Add("ID", 100);
            lv_Recipes.Columns.Add("Name", 150);
            //lv_Recipes.Columns.Add("Memo", 350);
            lv_Recipes.Columns.Add("Date Created", 200);
            lv_Recipes.Columns.Add("Date Modified", 200);

            //在listview上顯示Recipes
            SF3.Action_CmdLoadDefaultRecipe();
            ListViewItem item = new ListViewItem();
            item.SubItems.Clear();
            item.SubItems[0].Text = Convert.ToString(0); //ID
            item.SubItems.Add("Default"); //Name
            //item.SubItems.Add(""); //Memo
            item.SubItems.Add(""); //DateCreated
            item.SubItems.Add(""); //DateModified
            lv_Recipes.Items.Add(item);

            SF3.Action_CmdGetRecipeList();
            for (int i = 0; i < CommonResult.GetRecipeList_RecipesNo; i++)
            {
                item = new ListViewItem();
                item.SubItems.Clear();
                item.SubItems[0].Text = Convert.ToString(CommonResult.GetRecipeList_RecipesID[i]); //ID
                item.SubItems.Add(CommonResult.GetRecipeList_RecipesName[i]); //Name
                //item.SubItems.Add(""); //Memo
                item.SubItems.Add(CommonResult.GetRecipeList_DateAndTimeOfUpdate[i]); //DateCreated
                item.SubItems.Add(CommonResult.GetRecipeList_DateAndTimeOfUpdate[i]); //DateModified
                lv_Recipes.Items.Add(item);
            }

            //設定控建Enabled = false
            Close_Control();
            btn_StartMonitoring.Enabled = false;

            //顯示chart
            Init_chart();
        }
        private void Init_chart()
        {
            //每次使用此function前先清除圖表
            chart1.Series[0].Points.Clear();

            chart1.Series[0].Color = Color.Blue;                 //設定線條顏色
            chart1.Series[0].ToolTip = "#VALX,#VALY";            //鼠標停留在數據上，顯示XY值
            chart1.Series[0].ChartType = SeriesChartType.FastLine;   //設定線條種類 折線圖
            chart1.Series[0].YAxisType = AxisType.Primary;           //主坐標軸
            chart1.ChartAreas[0].AxisX.Maximum = Convert.ToDouble(num_AnalysisWavelengthNumber_Upper.Value);  //設定X軸最大值
            chart1.ChartAreas[0].AxisX.Minimum = Convert.ToDouble(num_AnalysisWavelengthNumber_Lower.Value);     //設定X軸最小值
            chart1.ChartAreas[0].AxisX.Interval = chart1.ChartAreas[0].AxisX.Maximum / 5; //設定X軸間隔 最大值/5
            chart1.ChartAreas[0].AxisY.Maximum = 1;  //設定Y軸最大值
            chart1.ChartAreas[0].AxisY.Minimum = 0;     //設定Y軸最小值
            chart1.ChartAreas[0].AxisY.Interval = chart1.ChartAreas[0].AxisY.Maximum / 5; //設定Y軸間隔 最大值/5
            //X軸
            chart1.ChartAreas[0].AxisX.Title = "Wavelength Number";
            chart1.ChartAreas[0].AxisX.TitleFont = new Font(chart1.ChartAreas[0].AxisX.Name, 20);
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;           //啟用縮放視圖
            chart1.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;//啟用X軸滾動條按鈕
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.AutoScroll = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorX.SelectionColor = System.Drawing.SystemColors.Highlight;
            //Y軸
            chart1.ChartAreas[0].AxisY.Title = "Signal[0-1]";
            chart1.ChartAreas[0].AxisY.TitleFont = new Font(chart1.ChartAreas[0].AxisY.Name, 14);
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
            chart1.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorY.AutoScroll = true;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].CursorY.SelectionColor = System.Drawing.SystemColors.Highlight;

            //chart1.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Far;    //設定title位置
            //chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270; //設定title文字方向
            //chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Blue;       //設定軸顏色
            //chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
            //chart1.ChartAreas[0].CursorY.Interval = 0;
            //chart1.ChartAreas[0].CursorY.IntervalOffset = 0;

            chart1.Series[0].Points.AddXY(0, 0);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            //給SF3
            SF3.Action_CmdMeasSignal(Convert.ToInt32(num_ExposureTime.Value), Convert.ToInt32(num_NumberOfAccumulations.Value));
            for (int i = 0; i < MeasSignal.SignalSpectrum.Count; i++)
            {
                chart1.Series[0].Points.AddXY(i, MeasSignal.SignalSpectrum[i]);
            }
        }
        private void ListClear()
        {
            //dataGridView_Mainsetting
            list_AnalysisThicknessRange_ThicknessRangeLowerLimit.Clear();
            list_AnalysisThicknessRange_ThicknessRangeUpperLimit.Clear();
            list_FFTPowerRange_FFTIntensityRangeLowerLimit.Clear();
            list_FFTPowerRange_FFTIntensityRangeUpperLimit.Clear();
            list_FFTSearchDirection_list.Clear();
            list_AnalysisRefractiveIndex_ConstantRefractiveIndex.Clear();
            list_FFTPeakNum_PeakNumber.Clear();

            //dataGridView_DetailSetting
            list_ThicknessCoef_list.Clear();
            list_ThicknessCoef_PrimaryCoefficientC1.Clear();
            list_ThicknessCoef_ConstantCoefficientC0.Clear();

            //dataGridView1
            list_OptimLayerModel_LayerMaterialID.Clear();
            list_OptimLayerModel_Thickness.Clear();
        }
        public void UI_To_SF3()
        {
            ListClear();
            Save_dataGridView_Mainsetting();
            Save_dataGridView_DetailSetting();
            Save_dataGridView1();
            SF3.Action_CmdSetExposure(Convert.ToInt32(num_ExposureTime.Value));
            SF3.Action_CmdSetAccumulation(Convert.ToInt32(num_NumberOfAccumulations.Value));
            SF3.Action_CmdSetTriggerDelay(Convert.ToInt32(num_TriggerDelayTime.Value));
            SF3.Action_CmdSetMeasCycle(Convert.ToInt32(num_MeasurementCycle.Value));
            SF3.Action_CmdSetMeasCount(Convert.ToInt32(num_NumberOfContinuous_measurements.Value));
            CmdSetResultType_enum_num = 0;
            if (rad_GetSignalSpectrum_Yes.Checked) CmdSetResultType_enum_num += 1; //有選Signal Spectrum，CmdSetResultType_enum為1(001)
            if (rad_GetReflectanceSpectrum_Yes.Checked) CmdSetResultType_enum_num += 2; //有選Reflectance Spectrum，CmdSetResultType_enum為2(010)
            if (rad_GetAnalysisSpectrum_Yes.Checked) CmdSetResultType_enum_num += 4; //有選Analysis Spectrum，CmdSetResultType_enum為4(100)
            SF3.Action_CmdSetResultType((CmdSetResultType)CmdSetResultType_enum_num);
            SF3.Action_CmdSetTriggerResetTime((CmdSetUseAnalog)Convert.ToInt32(rad_ResetElapsedTimeOnTriggerInput_Yes.Checked));
            CmdSetMeasMode_enum_num = 0;
            if (Convert.ToString(cb_MeasurementMode.SelectedItem) == "Single") CmdSetMeasMode_enum_num = 1;
            else if (Convert.ToString(cb_MeasurementMode.SelectedItem) == "Continuous") CmdSetMeasMode_enum_num = 2;
            else if (Convert.ToString(cb_MeasurementMode.SelectedItem) == "Trigger") CmdSetMeasMode_enum_num = 3;
            SF3.Action_CmdSetMeasMode((CmdSetMeasMode)CmdSetMeasMode_enum_num);//SF3.Action_CmdSetMeasMode((CmdSetMeasMode)Enum.Parse(typeof(CmdSetMeasMode), (string)cb_MeasurementMode.SelectedItem));
            SF3.Action_CmdGetMeasCycle();
            SF3.Action_CmdSetAnalysisRange(Convert.ToInt32(num_AnalysisWavelengthNumber_Lower.Value), Convert.ToInt32(num_AnalysisWavelengthNumber_Upper.Value));
            SF3.Action_CmdSetAnalysisMethod((CmdSetAnalysisMethod)cb_AnalysisMethod.SelectedIndex);
            SF3.Action_CmdSetSmoothing((CmdSetSmoothing)Convert.ToInt32(cb_SmoothingPoint.SelectedItem));//SF3.Action_CmdSetSmoothing((CmdSetSmoothing)Enum.Parse(typeof(CmdSetSmoothing), (string)cb_SmoothingPoint.SelectedItem));
            SF3.Action_CmdSetFFTNum((CmdSetFFTNum)Convert.ToInt32(cb_NumOfFFTDataPoints.SelectedItem));//SF3.Action_CmdSetFFTNum((CmdSetFFTNum)Enum.Parse(typeof(CmdSetFFTNum), (string)cb_NumOfFFTDataPoints.SelectedItem));
            SF3.Action_CmdSetFFTMaxThickness(Convert.ToSingle(num_FFTMaximumThickness.Value));
            SF3.Action_CmdSetFFTWindow((CmdSetFFTWindow)Convert.ToInt32(checkBox_FFTWindowFunction.Checked));
            SF3.Action_CmdSetFFTNormalization((CmdSetFFTNormalization)Convert.ToInt32(checkBox_FFTNormalization.Checked));
            SF3.Action_CmdSetUsePeakJudge((CmdSetUsePeakJudge)Convert.ToInt32(checkBox_NGPeakExclusion.Checked));
            SF3.Action_CmdSetAnalysisNum(Convert.ToInt32(num_NumOfAnalysisLayers.Value));
            SF3.Action_CmdSetAnalysisMaterial(Convert.ToInt32(cb_AnalysisLayerMaterial.SelectedIndex));
            //cmdSetAnalysisRefractiveIndex_DataQuantity、cmdSetAnalysisThicknessRange_DataQuantity、cmdSetFFTPowerRange_DataQuantity、cmdSetFFTSearchDirection_DataQuantity
            //cmdSetFFTPeakNum_DataQuantity = Convert.ToInt32(num_NumOfAmalysisLayers.Value)
            SF3.Action_CmdSetAnalysisRefractiveIndex(Convert.ToInt32(num_NumOfAnalysisLayers.Value), list_AnalysisRefractiveIndex_ConstantRefractiveIndex);
            SF3.Action_CmdSetAnalysisThicknessRange(Convert.ToInt32(num_NumOfAnalysisLayers.Value), list_AnalysisThicknessRange_ThicknessRangeLowerLimit, list_AnalysisThicknessRange_ThicknessRangeUpperLimit);
            SF3.Action_CmdSetFFTPowerRange(Convert.ToInt32(num_NumOfAnalysisLayers.Value), list_FFTPowerRange_FFTIntensityRangeLowerLimit, list_FFTPowerRange_FFTIntensityRangeUpperLimit);
            SF3.Action_CmdSetFFTSearchDirection(Convert.ToInt32(num_NumOfAnalysisLayers.Value), list_FFTSearchDirection_list);
            SF3.Action_CmdSetFFTPeakNum(Convert.ToInt32(num_NumOfAnalysisLayers.Value), list_FFTPeakNum_PeakNumber);
            //cmdSetThicknessCoef_DataQuantity = Convert.ToInt32(num_NumOfAmalysisLayers.Value)
            SF3.Action_CmdSetThicknessCoef(Convert.ToInt32(num_NumOfAnalysisLayers.Value), list_ThicknessCoef_list, list_ThicknessCoef_PrimaryCoefficientC1, list_ThicknessCoef_ConstantCoefficientC0);
            SF3.Action_CmdSetOptimSwitchThickness(Convert.ToSingle(num_OptimizationMethodSwitchoverThickness.Value));
            SF3.Action_CmdSetOptimThicknessStep(Convert.ToSingle(num_OptimizationMethodThicknessStepValue.Value));
            SF3.Action_CmdSetOptimAmbient(Convert.ToInt32(cb_AmbientLayerMaterial.SelectedIndex));
            //cmdSetOptimLayerModel_NumOfLayers = OptimLayerModel_NumOfLayers
            SF3.Action_CmdSetOptimLayerModel(OptimLayerModel_NumOfLayers, list_OptimLayerModel_LayerMaterialID, list_OptimLayerModel_Thickness);
            SF3.Action_CmdSetOptimSubstrate(Substrate_MaterialID, Substrate_MaterialThickness);
            SF3.Action_CmdSetFollowUpFilter(Convert.ToSingle(num_FollowupFilterRange.Value));
            SF3.Action_CmdSetFilterApplyTime(Convert.ToInt32(num_FollowupFilterApplicationTime.Value));
            SF3.Action_CmdSetFilterCenterMA(Convert.ToInt32(num_FollowupFilterCenterValueMovingAverage.Value));
            SF3.Action_CmdSetFilterTrend((CmdSetFilterTrend)Convert.ToInt32(cb_ThicknessVariationTrend.SelectedIndex));
            SF3.Action_CmdSetThicknessMA(Convert.ToInt32(num_ThicknessMoveingAverage.Value));
            SF3.Action_CmdSetErrorVaule(Convert.ToInt32(num_NumOfErrorForNaN.Value));
            SF3.Action_CmdGetFFTPowerXAxis();
        }
        public void SF3_To_UI()
        {
            SF3.Action_CmdGetExposure();
            num_ExposureTime.Value = CommonResult.GetExposure_ExposureTime;
            SF3.Action_CmdGetAccumulation();
            num_NumberOfAccumulations.Value = CommonResult.GetAccumulation_AccumulationsNo;
            SF3.Action_CmdGetTriggerDelay();
            num_TriggerDelayTime.Value = CommonResult.GetTriggerDelay_TriggerDelayTime;
            SF3.Action_CmdGetMeasCycle();
            num_MeasurementCycle.Value = CommonResult.GetMeasCycle_MeasurementCycle;
            SF3.Action_CmdGetMeasCount();
            num_NumberOfContinuous_measurements.Value = CommonResult.GetMeasCount_ContinuousMeasurementNo;
            SF3.Action_CmdGetResultType();
            int count = CommonResult.GetResultType_ResultDataType;
            if (count >= 4)
            {
                count -= 4;
                rad_GetAnalysisSpectrum_Yes.Checked = true;
            }
            if (count >= 2)
            {
                count -= 2;
                rad_GetReflectanceSpectrum_Yes.Checked = true;
            }
            if (count >= 1)
            {
                count -= 1;
                rad_GetSignalSpectrum_Yes.Checked = true;
            }
            SF3.Action_CmdGetTriggerResetTime();
            if (CommonResult.GetTriggerResetTime == 0) rad_ResetElapsedTimeOnTriggerInput_Yes.Checked = false;
            else if (CommonResult.GetTriggerResetTime == 1) rad_ResetElapsedTimeOnTriggerInput_Yes.Checked = true;
            SF3.Action_CmdGetMeasMode();
            if (CommonResult.GetMeasMode_MeasurementMode == 1) cb_MeasurementMode.SelectedItem = "Single";
            else if (CommonResult.GetMeasMode_MeasurementMode == 2) cb_MeasurementMode.SelectedItem = "Continuous";
            else if (CommonResult.GetMeasMode_MeasurementMode == 3) cb_MeasurementMode.SelectedItem = "Trigger";
            //SF3.Action_CmdGetMeasCycle();
            SF3.Action_CmdGetAnalysisRange();
            num_AnalysisWavelengthNumber_Lower.Value = CommonResult.GetAnalysisRange_StartAnalysisWavelengthNumber;
            num_AnalysisWavelengthNumber_Upper.Value = CommonResult.GetAnalysisRange_EndAnalysisWavelengthNumber;
            SF3.Action_CmdGetAnalysisMethod();
            cb_AnalysisMethod.SelectedIndex = CommonResult.GetAnalysisMethod_AnalysisMethod;
            SF3.Action_CmdGetSmoothing();
            cb_SmoothingPoint.SelectedItem = Convert.ToString(CommonResult.GetSmoothing_SmoothingPoint);
            SF3.Action_CmdGetFFTNum();
            cb_NumOfFFTDataPoints.SelectedItem = Convert.ToString(CommonResult.GetFFTNum_FFTDataPointsNo);
            SF3.Action_CmdGetFFTMaxThickness();
            num_FFTMaximumThickness.Value = (decimal)CommonResult.GetFFTMaxThickness_FFTMaximumThickness;
            SF3.Action_CmdGetFFTWindow();
            if (CommonResult.GetFFTWindow == 0) checkBox_FFTWindowFunction.Checked = false;
            else if (CommonResult.GetFFTWindow == 1) checkBox_FFTWindowFunction.Checked = true;
            SF3.Action_CmdGetFFTNormalization();
            if (CommonResult.GetFFTNormalization == 0) checkBox_FFTNormalization.Checked = false;
            else if (CommonResult.GetFFTNormalization == 1) checkBox_FFTNormalization.Checked = true;
            SF3.Action_CmdGetUsePeakJudge();
            if (CommonResult.GetUsePeakJudge == 0) checkBox_NGPeakExclusion.Checked = false;
            else if (CommonResult.GetUsePeakJudge == 1) checkBox_NGPeakExclusion.Checked = true;
            SF3.Action_CmdGetAnalysisNum();
            num_NumOfAnalysisLayers.Value = CommonResult.GetAnalysisNum_AnalysisLayerNo;
            SF3.Action_CmdGetAnalysisMaterial();
            cb_AnalysisLayerMaterial.SelectedIndex = CommonResult.GetAnalysisMaterial_AnalysisLayerMaterialID;
            SF3.Action_CmdGetAnalysisRefractiveIndex();
            num_NumOfAnalysisLayers.Value = CommonResult.GetAnalysisRefractiveindex_DataQuantity;
            //list_AnalysisRefractiveIndex_ConstantRefractiveIndex = CommonResult.GetAnalysisRefractiveindex_ConstantRefractiveIndex;
            for (int i = 0; i < CommonResult.GetAnalysisRefractiveindex_DataQuantity; i++)
            {
                dataGridView_Mainsetting.Rows[i].Cells[5].Value = CommonResult.GetAnalysisRefractiveindex_ConstantRefractiveIndex[i];
            }
            SF3.Action_CmdGetAnalysisThicknessRange();
            num_NumOfAnalysisLayers.Value = CommonResult.GetAnalysisThicknessRange_DataQuantity;
            //list_AnalysisThicknessRange_ThicknessRangeLowerLimit = CommonResult.GetAnalysisThicknessRange_ThicknessRangeLowerLimit;
            //list_AnalysisThicknessRange_ThicknessRangeUpperLimit = CommonResult.GetAnalysisThicknessRange_ThicknessRangeUpperLimit;
            for (int i = 0; i < CommonResult.GetAnalysisThicknessRange_DataQuantity; i++)
            {
                dataGridView_Mainsetting.Rows[i].Cells[0].Value = CommonResult.GetAnalysisThicknessRange_ThicknessRangeLowerLimit[i];
                dataGridView_Mainsetting.Rows[i].Cells[1].Value = CommonResult.GetAnalysisThicknessRange_ThicknessRangeUpperLimit[i];
            }
            SF3.Action_CmdGetFFTPowerRange();
            num_NumOfAnalysisLayers.Value = CommonResult.GetFFTPowerRange_DataQuantity;
            //list_FFTPowerRange_FFTIntensityRangeLowerLimit = CommonResult.GetFFTPowerRange_FFTIntensityRangeLowerLimit;
            //list_FFTPowerRange_FFTIntensityRangeUpperLimit = CommonResult.GetFFTPowerRange_FFTIntensityRangeUpperLimit;
            for (int i = 0; i < CommonResult.GetFFTPowerRange_DataQuantity; i++)
            {
                dataGridView_Mainsetting.Rows[i].Cells[2].Value = CommonResult.GetFFTPowerRange_FFTIntensityRangeLowerLimit[i];
                dataGridView_Mainsetting.Rows[i].Cells[3].Value = CommonResult.GetFFTPowerRange_FFTIntensityRangeUpperLimit[i];
            }
            SF3.Action_CmdGetFFTSearchDirection();
            num_NumOfAnalysisLayers.Value = CommonResult.GetFFTSearchDirection_DataQuantity;
            //list_FFTSearchDirection_list = ;
            for (int i = 0; i < CommonResult.GetFFTSearchDirection_DataQuantity; i++)
            {
                if (CommonResult.GetFFTSearchDirection_PeakSearchDirection[i] == 0) dataGridView_Mainsetting.Rows[i].Cells[4].Value = "From left";
                else if (CommonResult.GetFFTSearchDirection_PeakSearchDirection[i] == 1) dataGridView_Mainsetting.Rows[i].Cells[4].Value = "From top";
                else if (CommonResult.GetFFTSearchDirection_PeakSearchDirection[i] == 2) dataGridView_Mainsetting.Rows[i].Cells[4].Value = "From right";
                else if (CommonResult.GetFFTSearchDirection_PeakSearchDirection[i] == 3) dataGridView_Mainsetting.Rows[i].Cells[4].Value = "From bottom";
            }
            SF3.Action_CmdGetFFTPeakNum();
            num_NumOfAnalysisLayers.Value = CommonResult.GetFFTPeakNum_DataQuantity;
            //list_FFTPeakNum_PeakNumber = CommonResult.GetFFTPeakNum_PeakNumber;
            for (int i = 0; i < CommonResult.GetFFTPeakNum_DataQuantity; i++)
            {
                dataGridView_Mainsetting.Rows[i].Cells[6].Value = CommonResult.GetFFTPeakNum_PeakNumber[i];
            }
            SF3.Action_CmdGetThicknessCoef();
            num_NumOfAnalysisLayers.Value = CommonResult.GetThicknessCoef_DataQuantity;
            //list_ThicknessCoef_list = ;
            //list_ThicknessCoef_PrimaryCoefficientC1 = CommonResult.GetThicknessCoef_PrimaryCoefficientC1;
            //list_ThicknessCoef_ConstantCoefficientC0 = CommonResult.GetThicknessCoef_ConstantCoefficientC0;
            for (int i = 0; i < CommonResult.GetThicknessCoef_DataQuantity; i++)
            {
                if (CommonResult.GetThicknessCoef[i] == 0) dataGridView_DetailSetting.Rows[i].Cells[0].Value = "Not use";
                else if (CommonResult.GetThicknessCoef[i] == 1) dataGridView_DetailSetting.Rows[i].Cells[0].Value = "Use input value";
                else if (CommonResult.GetThicknessCoef[i] == 2) dataGridView_DetailSetting.Rows[i].Cells[0].Value = "Use calibrated value";
                dataGridView_DetailSetting.Rows[i].Cells[1].Value = CommonResult.GetThicknessCoef_PrimaryCoefficientC1[i];
                dataGridView_DetailSetting.Rows[i].Cells[2].Value = CommonResult.GetThicknessCoef_ConstantCoefficientC0[i];
            }
            SF3.Action_CmdGetOptimSwitchThickness();
            num_OptimizationMethodSwitchoverThickness.Value = (decimal)CommonResult.GetOptimSwitchThickness_Thickness;
            SF3.Action_CmdGetOptimThicknessStep();
            num_OptimizationMethodThicknessStepValue.Value = (decimal)CommonResult.GetOptimThicknessStep_ThicknessStep;
            SF3.Action_CmdGetOptimAmbient();
            cb_AmbientLayerMaterial.SelectedIndex = CommonResult.GetOptimAmbient_AmbientLayerMaterialID;
            SF3.Action_CmdGetOptimLayerModel();
            OptimLayerModel_NumOfLayers = CommonResult.GetOptimLayermodel_LayersNo;
            //list_OptimLayerModel_LayerMaterialID = CommonResult.GetOptimLayermodel_LayerMaterialID;
            //list_OptimLayerModel_Thickness = CommonResult.GetOptimLayermodel_Thickness;
            int a = CommonResult.GetOptimLayermodel_LayersNo;
            int b = 0;
            for (int i = 0; i < 4; i++)
            {
                if (4 - a > b) //GetOptimLayermodel_LayersNo最多有四層
                {
                    dataGridView1.Rows[i].Cells[0].Value = "NoLayer";
                    dataGridView1.Rows[i].Cells[1].Value = "0";
                    b++;
                }
                else
                {
                    if (CommonResult.GetOptimLayermodel_LayerMaterialID[i - a] == 1) dataGridView1.Rows[i].Cells[0].Value = "1:Al";
                    else if (CommonResult.GetOptimLayermodel_LayerMaterialID[i - a] == 2) dataGridView1.Rows[i].Cells[0].Value = "2:Air";
                    else if (CommonResult.GetOptimLayermodel_LayerMaterialID[i - a] == 3) dataGridView1.Rows[i].Cells[0].Value = "3:BK-7";
                    else if (CommonResult.GetOptimLayermodel_LayerMaterialID[i - a] == 4) dataGridView1.Rows[i].Cells[0].Value = "4:Si";
                    else if (CommonResult.GetOptimLayermodel_LayerMaterialID[i - a] == 5) dataGridView1.Rows[i].Cells[0].Value = "5:SiO2";
                    else if (CommonResult.GetOptimLayermodel_LayerMaterialID[i - a] == 6) dataGridView1.Rows[i].Cells[0].Value = "6:Ag";
                    else if (CommonResult.GetOptimLayermodel_LayerMaterialID[i - a] == 7) dataGridView1.Rows[i].Cells[0].Value = "7:Au";
                    else if (CommonResult.GetOptimLayermodel_LayerMaterialID[i - a] == 8) dataGridView1.Rows[i].Cells[0].Value = "8:Cr";
                    else if (CommonResult.GetOptimLayermodel_LayerMaterialID[i - a] == 9) dataGridView1.Rows[i].Cells[0].Value = "9:Cu";
                    else if (CommonResult.GetOptimLayermodel_LayerMaterialID[i - a] == 10) dataGridView1.Rows[i].Cells[0].Value = "10:Si3N4";
                    else if (CommonResult.GetOptimLayermodel_LayerMaterialID[i - a] == 11) dataGridView1.Rows[i].Cells[0].Value = "11:Ti";
                    else if (CommonResult.GetOptimLayermodel_LayerMaterialID[i - a] == 12) dataGridView1.Rows[i].Cells[0].Value = "12:test";
                    else if (CommonResult.GetOptimLayermodel_LayerMaterialID[i - a] == 13) dataGridView1.Rows[i].Cells[0].Value = "13:Quartz";
                    else if (CommonResult.GetOptimLayermodel_LayerMaterialID[i - a] == 14) dataGridView1.Rows[i].Cells[0].Value = "14:GaAs";
                    else if (CommonResult.GetOptimLayermodel_LayerMaterialID[i - a] == 15) dataGridView1.Rows[i].Cells[0].Value = "15:Sapphire";
                    dataGridView1.Rows[i].Cells[1].Value = CommonResult.GetOptimLayermodel_Thickness[i - a];
                }
            }
            //for (int i = 0; i < CommonResult.GetOptimLayermodel_LayersNo; i++)
            //{
            //    if (CommonResult.GetOptimLayermodel_LayerMaterialID[i] == 0) dataGridView1.Rows[i].Cells[0].Value = "Not use";
            //    else if (CommonResult.GetOptimLayermodel_LayerMaterialID[i] == 1) dataGridView1.Rows[i].Cells[0].Value = "Use input value";
            //    else if (CommonResult.GetOptimLayermodel_LayerMaterialID[i] == 2) dataGridView1.Rows[i].Cells[0].Value = "Use calibrated value";
            //    //dataGridView1.Rows[i].Cells[0].Value = CommonResult.GetOptimLayermodel_LayerMaterialID[i];
            //    dataGridView1.Rows[i].Cells[1].Value = CommonResult.GetOptimLayermodel_Thickness[i];
            //}
            SF3.Action_CmdGetOptimSubstrate();
            if (CommonResult.GetOptimSubstrate_SubstrateMaterialID == 1) dataGridView1.Rows[4].Cells[0].Value = "1:Al";
            else if (CommonResult.GetOptimSubstrate_SubstrateMaterialID == 2) dataGridView1.Rows[4].Cells[0].Value = "2:Air";
            else if (CommonResult.GetOptimSubstrate_SubstrateMaterialID == 3) dataGridView1.Rows[4].Cells[0].Value = "3:BK - 7";
            else if (CommonResult.GetOptimSubstrate_SubstrateMaterialID == 4) dataGridView1.Rows[4].Cells[0].Value = "4:Si";
            else if (CommonResult.GetOptimSubstrate_SubstrateMaterialID == 5) dataGridView1.Rows[4].Cells[0].Value = "5:SiO2";
            else if (CommonResult.GetOptimSubstrate_SubstrateMaterialID == 6) dataGridView1.Rows[4].Cells[0].Value = "6:Ag";
            else if (CommonResult.GetOptimSubstrate_SubstrateMaterialID == 7) dataGridView1.Rows[4].Cells[0].Value = "7:Au";
            else if (CommonResult.GetOptimSubstrate_SubstrateMaterialID == 8) dataGridView1.Rows[4].Cells[0].Value = "8:Cr";
            else if (CommonResult.GetOptimSubstrate_SubstrateMaterialID == 9) dataGridView1.Rows[4].Cells[0].Value = "9:Cu";
            else if (CommonResult.GetOptimSubstrate_SubstrateMaterialID == 10) dataGridView1.Rows[4].Cells[0].Value = "10:Si3N4";
            else if (CommonResult.GetOptimSubstrate_SubstrateMaterialID == 11) dataGridView1.Rows[4].Cells[0].Value = "11:Ti";
            else if (CommonResult.GetOptimSubstrate_SubstrateMaterialID == 12) dataGridView1.Rows[4].Cells[0].Value = "12:test";
            else if (CommonResult.GetOptimSubstrate_SubstrateMaterialID == 13) dataGridView1.Rows[4].Cells[0].Value = "13:Quartz";
            else if (CommonResult.GetOptimSubstrate_SubstrateMaterialID == 14) dataGridView1.Rows[4].Cells[0].Value = "14:GaAs";
            else if (CommonResult.GetOptimSubstrate_SubstrateMaterialID == 15) dataGridView1.Rows[4].Cells[0].Value = "15:Sapphire";
            dataGridView1.Rows[4].Cells[1].Value = CommonResult.GetOptimSubstrate_Thickness;
            //Substrate_MaterialID = CommonResult.GetOptimSubstrate_SubstrateMaterialID;
            //Substrate_MaterialThickness = CommonResult.GetOptimSubstrate_Thickness;
            SF3.Action_CmdGetFollowUpFilter();
            num_FollowupFilterRange.Value = (decimal)CommonResult.GetFollowupFilter_FollowupFilterPlusMinusRange;
            SF3.Action_CmdGetFilterApplyTime();
            num_FollowupFilterApplicationTime.Value = CommonResult.GetFilterApplytime_FollowupFilterApplicationTime;
            SF3.Action_CmdGetFilterCenterMA();
            num_FollowupFilterCenterValueMovingAverage.Value = CommonResult.GetFilterCenterMA_FollowupFilterCenterValueMovingAverage;
            SF3.Action_CmdGetFilterTrend();
            cb_ThicknessVariationTrend.SelectedIndex = CommonResult.GetFilterTrend;
            SF3.Action_CmdGetThicknessMA();
            num_ThicknessMoveingAverage.Value = CommonResult.GetThicknessMA_ThicknessMovingAverageFrequency;
            SF3.Action_CmdGetErrorVaule();
            num_NumOfErrorForNaN.Value = CommonResult.GetErrorvalue;
            //SF3.Action_CmdGetFFTPowerXAxis();
        }

        #region Button
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (lv_Recipes.SelectedItems.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("請選擇一個Recipe");
                return;
            }
            //給SF3
            if(lv_Recipes.FocusedItem.SubItems[0].Text == "0") SF3.Action_CmdLoadDefaultRecipe();
            else SF3.Action_CmdLoadRecipeID(Convert.ToInt32(lv_Recipes.FocusedItem.SubItems[0].Text));
            UI_To_SF3();
            //給Main.Form的Chart
            //Thickness_Init_chart
            double min = 100000, max = 0;
            for (int i = 0; i < Convert.ToInt32(num_NumOfAnalysisLayers.Value); i++)
            {
                if (min > Convert.ToDouble(list_AnalysisThicknessRange_ThicknessRangeLowerLimit[i])) min = Convert.ToDouble(list_AnalysisThicknessRange_ThicknessRangeLowerLimit[i]);
                if (max < Convert.ToDouble(list_AnalysisThicknessRange_ThicknessRangeUpperLimit[i])) max = Convert.ToDouble(list_AnalysisThicknessRange_ThicknessRangeUpperLimit[i]);
            }
            SF3.SF3_Form.Thickness_chart.ChartAreas[0].AxisY.Maximum = max;  //設定Y軸最大值
            SF3.SF3_Form.Thickness_chart.ChartAreas[0].AxisY.Minimum = min;   //設定Y軸最小值
            SF3.SF3_Form.Thickness_chart.ChartAreas[0].AxisY.Interval = SF3.SF3_Form.Thickness_chart.ChartAreas[0].AxisY.Maximum / 5; //設定Y軸間隔 最大值/10
            
            //PowerSpectrum_Init_chart
            min = 100000;
            max = 0;
            SF3.SF3_Form.PowerSpectrum_chart.ChartAreas[0].AxisX.Maximum = Convert.ToDouble(num_FFTMaximumThickness.Text);
            SF3.SF3_Form.PowerSpectrum_chart.ChartAreas[0].AxisX.Interval = SF3.SF3_Form.PowerSpectrum_chart.ChartAreas[0].AxisX.Maximum / 5; //設定X軸間隔 最大值/5
            for(int i = 0; i < Convert.ToInt32(num_NumOfAnalysisLayers.Value); i++)
            {
                if (min > Convert.ToDouble(list_FFTPowerRange_FFTIntensityRangeLowerLimit[i])) min = Convert.ToDouble(list_FFTPowerRange_FFTIntensityRangeLowerLimit[i]);
                if (max < Convert.ToDouble(list_FFTPowerRange_FFTIntensityRangeUpperLimit[i])) max = Convert.ToDouble(list_FFTPowerRange_FFTIntensityRangeUpperLimit[i]);
            }
            SF3.SF3_Form.PowerSpectrum_chart.ChartAreas[0].AxisY.Maximum = max;  //設定Y軸最大值
            SF3.SF3_Form.PowerSpectrum_chart.ChartAreas[0].AxisY.Minimum = min;     //設定Y軸最小值
            SF3.SF3_Form.PowerSpectrum_chart.ChartAreas[0].AxisY.Interval = SF3.SF3_Form.PowerSpectrum_chart.ChartAreas[0].AxisY.Maximum / 5; //設定Y軸間隔 最大值/5
            this.Hide();
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            //this.Close();
            this.Hide();
        }
        private void btn_New_Click(object sender, EventArgs e)
        {
            Recipes_list.Clear();
            for (int i = 0; i < lv_Recipes.Items.Count; i++)
            {
                Recipes_list.Add(lv_Recipes.Items[i].SubItems[0].Text);
            }
            NewRecipesInformation newRecipes = new NewRecipesInformation(Recipes_list);
            newRecipes.ShowDialog();

            if (newRecipes.isOK)
            {
                //設定的Recipes初始值
                ListClear();
                Save_dataGridView_Mainsetting();
                Save_dataGridView_DetailSetting();
                Save_dataGridView1();
                Recipe_ID = newRecipes.Recipe_ID;
                Recipe_Name = newRecipes.Recipe_Name;
                //Recipe_Memo = newRecipes.Recipe_Memo;
                Recipe_DateCreated = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                Recipe_DateModified = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                //新增Recipes
                ListViewItem item = new ListViewItem();
                item.SubItems.Clear();
                item.SubItems[0].Text = Recipe_ID; //ID
                item.SubItems.Add(Recipe_Name); //Name
                //item.SubItems.Add(Recipe_Memo); //Memo
                item.SubItems.Add(Recipe_DateCreated); //Date Created
                item.SubItems.Add(Recipe_DateModified); //Date Modified
                lv_Recipes.Items.Add(item);

                //給SF3
                //SF3.Action_CmdGetRecipeList();
                UI_To_SF3();
                SF3.Action_CmdSaveRecipeID(Convert.ToInt32(Recipe_ID), Recipe_Name);
                newRecipes.Close();
            }
        }
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (lv_Recipes.SelectedItems.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("請選擇一個Recipe");
                return;
            }
            if (lv_Recipes.SelectedItems[0].Index == 0)
            {
                System.Windows.Forms.MessageBox.Show("Default不可刪除");
                return;
            }
            DeleteRecipes deleteRecipes = new DeleteRecipes(txt_RecipeID.Text, txt_Name.Text, txt_Memo.Text);
            deleteRecipes.ShowDialog();
            if (deleteRecipes.isOK)
            {
                //給SF3
                SF3.Action_CmdDelRecipeID(Convert.ToInt32(lv_Recipes.FocusedItem.SubItems[0].Text));

                lv_Recipes.Items.RemoveAt(lv_Recipes.SelectedItems[0].Index);
            }
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (lv_Recipes.SelectedItems.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("請選擇一個Recipe");
                return;
            }
            ListClear();
            Save_dataGridView_Mainsetting();
            Save_dataGridView_DetailSetting();
            Save_dataGridView1();

            //給SF3
            UI_To_SF3();
            if (lv_Recipes.FocusedItem.SubItems[1].Text == "Default") SF3.Action_CmdUpdateRecipe();
            //SF3.Action_CmdMakeSimulationData();//
            else SF3.Action_CmdSaveRecipeID(Convert.ToInt32(lv_Recipes.FocusedItem.SubItems[0].Text), lv_Recipes.FocusedItem.SubItems[1].Text);
            System.Windows.Forms.MessageBox.Show("儲存成功");
        }
        private void btn_StartMonitoring_Click(object sender, EventArgs e)
        {
            lv_Recipes.Enabled = false;
            Close_Control();

            //給SF3
            SF3.Action_CmdCtrlLight(Common.CmdCtrlLight.Light);
            timer1.Enabled = true;
        }
        private void btn_StopMonitoring_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (lv_Recipes.SelectedItems.Count != 0)
            {
                lv_Recipes.Enabled = true;
                Open_Control();
            }
            Init_chart();
        }
        #endregion

        #region SaveData
        private void Save_dataGridView_Mainsetting()
        {
            for (int i = 0; i < num_NumOfAnalysisLayers.Value; i++)
            {

                int CmdSetFFTSearchDirection_list_Num = 0; // Left
                if (Convert.ToString(dataGridView_Mainsetting.Rows[i].Cells[4].FormattedValue) == "From top")
                    CmdSetFFTSearchDirection_list_Num = 1;
                else if (Convert.ToString(dataGridView_Mainsetting.Rows[i].Cells[4].FormattedValue) == "From right")
                    CmdSetFFTSearchDirection_list_Num = 2;
                else if (Convert.ToString(dataGridView_Mainsetting.Rows[i].Cells[4].FormattedValue) == "From bottom")
                    CmdSetFFTSearchDirection_list_Num = 3;
                //給SF3
                list_AnalysisThicknessRange_ThicknessRangeLowerLimit.Add(Convert.ToSingle(dataGridView_Mainsetting.Rows[i].Cells[0].FormattedValue));
                list_AnalysisThicknessRange_ThicknessRangeUpperLimit.Add(Convert.ToSingle(dataGridView_Mainsetting.Rows[i].Cells[1].FormattedValue));
                list_FFTPowerRange_FFTIntensityRangeLowerLimit.Add(Convert.ToSingle(dataGridView_Mainsetting.Rows[i].Cells[2].FormattedValue));
                list_FFTPowerRange_FFTIntensityRangeUpperLimit.Add(Convert.ToSingle(dataGridView_Mainsetting.Rows[i].Cells[3].FormattedValue));
                list_FFTSearchDirection_list.Add(CmdSetFFTSearchDirection_list_Num);
                list_AnalysisRefractiveIndex_ConstantRefractiveIndex.Add(Convert.ToSingle(dataGridView_Mainsetting.Rows[i].Cells[5].FormattedValue));
                list_FFTPeakNum_PeakNumber.Add(Convert.ToInt32(dataGridView_Mainsetting.Rows[i].Cells[6].FormattedValue));
            }
        }
        private void Save_dataGridView_DetailSetting()
        {
            for (int i = 0; i < num_NumOfAnalysisLayers.Value; i++)
            {
                int CmdSetThicknessCoef_list_Num = 0; // NotUse
                if (Convert.ToString(dataGridView_DetailSetting.Rows[i].Cells[0].FormattedValue) == "Use input value")
                    CmdSetThicknessCoef_list_Num = 1;
                else if (Convert.ToString(dataGridView_DetailSetting.Rows[i].Cells[0].FormattedValue) == "Use calibrated value")
                    CmdSetThicknessCoef_list_Num = 2;
                //給SF3
                list_ThicknessCoef_list.Add(CmdSetThicknessCoef_list_Num);
                list_ThicknessCoef_PrimaryCoefficientC1.Add(Convert.ToSingle(dataGridView_DetailSetting.Rows[i].Cells[1].FormattedValue));
                list_ThicknessCoef_ConstantCoefficientC0.Add(Convert.ToSingle(dataGridView_DetailSetting.Rows[i].Cells[2].FormattedValue));
            }
        }
        public void Save_dataGridView1()
        {
            OptimLayerModel_NumOfLayers = 0;
            for (int i = 0; i < AmbientLayerCount; i++)
            {
                string str = Convert.ToString(dataGridView1.Rows[i].Cells[0].FormattedValue);
                int num = str.IndexOf(":");
                if (num == -1) CmdSetOptimLayerModel_LayerMaterialID_Num = 0; // NoLayer
                else CmdSetOptimLayerModel_LayerMaterialID_Num = Convert.ToInt32(str.Substring(0, num));

                //給SF3
                if (i == AmbientLayerCount - 1)
                {
                    Substrate_MaterialID = CmdSetOptimLayerModel_LayerMaterialID_Num;
                    Substrate_MaterialThickness = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].FormattedValue);
                }
                else if(CmdSetOptimLayerModel_LayerMaterialID_Num != 0)
                {
                    OptimLayerModel_NumOfLayers++;
                    list_OptimLayerModel_LayerMaterialID.Add(CmdSetOptimLayerModel_LayerMaterialID_Num);
                    list_OptimLayerModel_Thickness.Add(Convert.ToSingle(dataGridView1.Rows[i].Cells[1].FormattedValue));
                }
            }
        }
        #endregion

        #region Control Open/Close
        private void Close_Control()
        {
            txt_RecipeID.Enabled = false;
            txt_Name.Enabled = false;
            txt_Memo.Enabled = false;

            num_ExposureTime.Enabled = false;
            num_NumberOfAccumulations.Enabled = false;
            num_TriggerDelayTime.Enabled = false;
            groupBox2.Enabled = false;
            cb_MeasurementMode.Enabled = false;
            num_NumberOfContinuous_measurements.Enabled = false;
            num_MeasurementCycle.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            groupBox5.Enabled = false;

            num_AnalysisWavelengthNumber_Lower.Enabled = false;
            num_AnalysisWavelengthNumber_Upper.Enabled = false;
            cb_AnalysisMethod.Enabled = false;
            cb_NumOfFFTDataPoints.Enabled = false;
            num_FFTMaximumThickness.Enabled = false;
            dataGridView_Mainsetting.Enabled = false;
            num_NumOfAnalysisLayers.Enabled = false;
            cb_AnalysisLayerMaterial.Enabled = false;
            num_ThicknessMoveingAverage.Enabled = false;

            cb_SmoothingPoint.Enabled = false;
            checkBox_FFTWindowFunction.Enabled = false;
            checkBox_FFTNormalization.Enabled = false;
            checkBox_NGPeakExclusion.Enabled = false;
            num_FollowupFilterRange.Enabled = false;
            num_FollowupFilterApplicationTime.Enabled = false;
            num_FollowupFilterCenterValueMovingAverage.Enabled = false;
            cb_ThicknessVariationTrend.Enabled = false;
            cb_ErrorValue.Enabled = false;
            num_NumOfErrorForNaN.Enabled = false;
            dataGridView_DetailSetting.Enabled = false;

            num_OptimizationMethodSwitchoverThickness.Enabled = false;
            num_OptimizationMethodThicknessStepValue.Enabled = false;
            cb_AmbientLayerMaterial.Enabled = false;
            dataGridView1.Enabled = false;
        }
        private void Open_Control()
        {
            //txt_RecipeID.ReadOnly = false;
            txt_Name.Enabled = true;
            txt_Memo.Enabled = true;

            num_ExposureTime.Enabled = true;
            num_NumberOfAccumulations.Enabled = true;
            num_TriggerDelayTime.Enabled = true;
            groupBox2.Enabled = true;
            cb_MeasurementMode.Enabled = true;
            num_NumberOfContinuous_measurements.Enabled = true;
            num_MeasurementCycle.Enabled = true;
            groupBox3.Enabled = true;
            groupBox4.Enabled = true;
            groupBox5.Enabled = true;

            num_AnalysisWavelengthNumber_Lower.Enabled = true;
            num_AnalysisWavelengthNumber_Upper.Enabled = true;
            //cb_AnalysisMethod.Enabled = true;
            cb_NumOfFFTDataPoints.Enabled = true;
            num_FFTMaximumThickness.Enabled = true;
            dataGridView_Mainsetting.Enabled = true;
            num_NumOfAnalysisLayers.Enabled = true;
            cb_AnalysisLayerMaterial.Enabled = true;
            num_ThicknessMoveingAverage.Enabled = true;

            cb_SmoothingPoint.Enabled = true;
            checkBox_FFTWindowFunction.Enabled = true;
            checkBox_FFTNormalization.Enabled = true;
            checkBox_NGPeakExclusion.Enabled = true;
            num_FollowupFilterRange.Enabled = true;
            num_FollowupFilterApplicationTime.Enabled = true;
            num_FollowupFilterCenterValueMovingAverage.Enabled = true;
            cb_ThicknessVariationTrend.Enabled = true;
            cb_ErrorValue.Enabled = true;
            num_NumOfErrorForNaN.Enabled = true;
            dataGridView_DetailSetting.Enabled = true;

            num_OptimizationMethodSwitchoverThickness.Enabled = true;
            num_OptimizationMethodThicknessStepValue.Enabled = true;
            cb_AmbientLayerMaterial.Enabled = true;
            dataGridView1.Enabled = true;
        }
        #endregion

        #region ListView
        private void lv_Recipes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_Recipes.SelectedItems.Count == 0)
            {
                txt_RecipeID.Text = "";
                txt_Name.Text = "";
                txt_Memo.Text = "";

                //
                Close_Control();
                btn_StartMonitoring.Enabled = false;
                return;
            }
            //ListView修改
            txt_RecipeID.Text = lv_Recipes.FocusedItem.SubItems[0].Text;
            txt_Name.Text = lv_Recipes.FocusedItem.SubItems[1].Text;
            txt_Memo.Text = lv_Recipes.FocusedItem.SubItems[2].Text;
            Open_Control();
            btn_StartMonitoring.Enabled = true;
            
            ////將dataGridView的資料填上
            if (lv_Recipes.FocusedItem.SubItems[0].Text == "0")
            {
                SF3.Action_CmdLoadDefaultRecipe();
                SF3_To_UI();
            }
            else
            {
                SF3.Action_CmdLoadRecipeID(Convert.ToInt32(lv_Recipes.FocusedItem.SubItems[0].Text));
                SF3_To_UI();
            }
            if (Convert.ToString(cb_ErrorValue.SelectedItem) == "Previous value")
            {
                num_NumOfErrorForNaN.Enabled = false;
            }
        }
        private void txt_Name_TextChanged(object sender, EventArgs e)
        {
            if (lv_Recipes.SelectedItems.Count == 0)
            {
                return;
            }
            lv_Recipes.FocusedItem.SubItems[1].Text = txt_Name.Text;
        }
        private void txt_Memo_TextChanged(object sender, EventArgs e)
        {
            if (lv_Recipes.SelectedItems.Count == 0)
            {
                return;
            }
            lv_Recipes.FocusedItem.SubItems[2].Text = txt_Memo.Text;
        }
        #endregion

        #region Analysis condition
        //Main settings
        private void num_AnalysisWavelengthNumber_Lower_ValueChanged(object sender, EventArgs e)
        {
            if ((int)num_AnalysisWavelengthNumber_Lower.Value > (int)num_AnalysisWavelengthNumber_Upper.Value)
            {
                num_AnalysisWavelengthNumber_Lower.Value = num_AnalysisWavelengthNumber_Upper.Value;
            }
            chart1.ChartAreas[0].AxisX.Minimum = Convert.ToDouble(num_AnalysisWavelengthNumber_Lower.Value);
        }
        private void num_AnalysisWavelengthNumber_Upper_ValueChanged(object sender, EventArgs e)
        {
            if ((int)num_AnalysisWavelengthNumber_Lower.Value > (int)num_AnalysisWavelengthNumber_Upper.Value)
            {
                num_AnalysisWavelengthNumber_Upper.Value = num_AnalysisWavelengthNumber_Lower.Value;
            }
            chart1.ChartAreas[0].AxisX.Maximum = Convert.ToDouble(num_AnalysisWavelengthNumber_Upper.Value);
        }
        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();
            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }
        private void num_NumOfAmalysisLayers_ValueChanged(object sender, EventArgs e)
        {
            int temp = dataGridView_Mainsetting.Rows.Count;
            for (; (int)num_NumOfAnalysisLayers.Value != temp;)
            {
                if ((int)num_NumOfAnalysisLayers.Value > temp)
                {
                    dataGridView_Mainsetting.Rows.Add();
                    dataGridView_DetailSetting.Rows.Add();
                    temp++;
                }

                if ((int)num_NumOfAnalysisLayers.Value < temp)
                {
                    dataGridView_Mainsetting.Rows.RemoveAt(dataGridView_Mainsetting.Rows.Count - 1);
                    dataGridView_DetailSetting.Rows.RemoveAt(dataGridView_DetailSetting.Rows.Count - 1);
                    temp--;
                }
            }
        }
        private void dataGridView_Mainsetting_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            for (int i = 0; i < dataGridView_Mainsetting.Rows.Count; i++)
            {
                if (dataGridView_Mainsetting.Rows[i].Cells[0].Value != null && dataGridView_Mainsetting.Rows[i].Cells[1].Value != null)
                {
                    if (e.Cell.ColumnIndex == 0)
                    {
                        if(Convert.ToString(dataGridView_Mainsetting.Rows[i].Cells[0].Value) == "" || Convert.ToDouble(dataGridView_Mainsetting.Rows[i].Cells[0].Value) < 0)
                        {
                            dataGridView_Mainsetting.Rows[i].Cells[0].Value = 0;
                            return;
                        }
                        else if (Convert.ToDouble(dataGridView_Mainsetting.Rows[i].Cells[0].Value) > Convert.ToDouble(dataGridView_Mainsetting.Rows[i].Cells[1].Value))
                        {
                            dataGridView_Mainsetting.Rows[i].Cells[0].Value = dataGridView_Mainsetting.Rows[i].Cells[1].Value;
                            return;
                        }
                    }
                    else if (e.Cell.ColumnIndex == 1)
                    {
                        if (Convert.ToString(dataGridView_Mainsetting.Rows[i].Cells[1].Value) == "" || Convert.ToDouble(dataGridView_Mainsetting.Rows[i].Cells[1].Value) > 100000)
                        {
                            dataGridView_Mainsetting.Rows[i].Cells[1].Value = 100000;
                            return;
                        }
                        else if (Convert.ToDouble(dataGridView_Mainsetting.Rows[i].Cells[1].Value) < Convert.ToDouble(dataGridView_Mainsetting.Rows[i].Cells[0].Value))
                        {
                            dataGridView_Mainsetting.Rows[i].Cells[1].Value = dataGridView_Mainsetting.Rows[i].Cells[0].Value;
                            return;
                        }
                    }
                }

                if (dataGridView_Mainsetting.Rows[i].Cells[2].Value != null || dataGridView_Mainsetting.Rows[i].Cells[3].Value != null)
                {
                    if (e.Cell.ColumnIndex == 2)
                    {
                        if (Convert.ToString(dataGridView_Mainsetting.Rows[i].Cells[2].Value) == "" || Convert.ToDouble(dataGridView_Mainsetting.Rows[i].Cells[2].Value) < 0)
                        {
                            dataGridView_Mainsetting.Rows[i].Cells[2].Value = 0;
                            return;
                        }
                        else if (Convert.ToDouble(dataGridView_Mainsetting.Rows[i].Cells[2].Value) > Convert.ToDouble(dataGridView_Mainsetting.Rows[i].Cells[3].Value))
                        {
                            dataGridView_Mainsetting.Rows[i].Cells[2].Value = dataGridView_Mainsetting.Rows[i].Cells[3].Value;
                            return;
                        }
                    }
                    else if (e.Cell.ColumnIndex == 3)
                    {
                        if (Convert.ToString(dataGridView_Mainsetting.Rows[i].Cells[3].Value) == "" || Convert.ToDouble(dataGridView_Mainsetting.Rows[i].Cells[3].Value) > 100000)
                        {
                            dataGridView_Mainsetting.Rows[i].Cells[3].Value = 100000;
                            return;
                        }
                        else if (Convert.ToDouble(dataGridView_Mainsetting.Rows[i].Cells[3].Value) < Convert.ToDouble(dataGridView_Mainsetting.Rows[i].Cells[2].Value))
                        {
                            dataGridView_Mainsetting.Rows[i].Cells[3].Value = dataGridView_Mainsetting.Rows[i].Cells[2].Value;
                            return;
                        }
                    }
                }
                if (dataGridView_Mainsetting.Rows[i].Cells[5].Value != null)
                {
                    if (e.Cell.ColumnIndex == 5)
                    {
                        if (Convert.ToString(dataGridView_Mainsetting.Rows[i].Cells[5].Value) == "" || Convert.ToDouble(dataGridView_Mainsetting.Rows[i].Cells[5].Value) < 1)
                        {
                            dataGridView_Mainsetting.Rows[i].Cells[5].Value = 1;
                            return;
                        }
                        else if (Convert.ToDouble(dataGridView_Mainsetting.Rows[i].Cells[5].Value) > 10)
                        {
                            dataGridView_Mainsetting.Rows[i].Cells[5].Value = 10;
                            return;
                        }
                    }
                }
                if (dataGridView_Mainsetting.Rows[i].Cells[6].Value != null)
                {
                    if (e.Cell.ColumnIndex == 6)
                    {
                        if (Convert.ToString(dataGridView_Mainsetting.Rows[i].Cells[6].Value) == "" || Convert.ToDouble(dataGridView_Mainsetting.Rows[i].Cells[6].Value) < 1)
                        {
                            dataGridView_Mainsetting.Rows[i].Cells[6].Value = 1;
                            return;
                        }
                        else if (Convert.ToDouble(dataGridView_Mainsetting.Rows[i].Cells[6].Value) > 99)
                        {
                            dataGridView_Mainsetting.Rows[i].Cells[6].Value = 99;
                            return;
                        }
                    }
                }
            }
        }
        private void dataGridView_DetailSetting_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            for (int i = 0; i < dataGridView_DetailSetting.Rows.Count; i++)
            {
                if (dataGridView_DetailSetting.Rows[i].Cells[1].Value != null)
                {
                    if (e.Cell.ColumnIndex == 1)
                    {
                        if (Convert.ToString(dataGridView_DetailSetting.Rows[i].Cells[1].Value) == "")
                        {
                            dataGridView_DetailSetting.Rows[i].Cells[1].Value = 1;
                            return;
                        }
                    }
                }
                if (dataGridView_DetailSetting.Rows[i].Cells[2].Value != null)
                {
                    if (e.Cell.ColumnIndex == 2)
                    {
                        if (Convert.ToString(dataGridView_DetailSetting.Rows[i].Cells[2].Value) == "")
                        {
                            dataGridView_DetailSetting.Rows[i].Cells[2].Value = 0;
                            return;
                        }
                    }
                }
            }
        }
        TextBox control;
        private void dataGridView_Mainsetting_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //只對TextBox型別的單元格進行驗證
            if (e.Control.GetType().BaseType.Name == "TextBox")
            {
                control = new TextBox();
                control = (TextBox)e.Control;
                control.KeyPress += new KeyPressEventHandler(control_KeyPress);
            }
        }
        private void dataGridView_DetailSetting_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //只對TextBox型別的單元格進行驗證
            if (e.Control.GetType().BaseType.Name == "TextBox")
            {
                control = new TextBox();
                control = (TextBox)e.Control;
                control.KeyPress += new KeyPressEventHandler(control_KeyPress);
            }
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //只對TextBox型別的單元格進行驗證
            if (e.Control.GetType().BaseType.Name == "TextBox")
            {
                control = new TextBox();
                control = (TextBox)e.Control;
                control.KeyPress += new KeyPressEventHandler(control_KeyPress);
            }
        }
        void control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                foreach (char i in (sender as TextBox).Text)//判定textBox1是否有小數點
                {
                    if (i == '.')
                        e.Handled = true;//有
                }
                return;
            }

            if (e.KeyChar == (Char)48 || e.KeyChar == (Char)49 ||
                e.KeyChar == (Char)50 || e.KeyChar == (Char)51 ||
                e.KeyChar == (Char)52 || e.KeyChar == (Char)53 ||
                e.KeyChar == (Char)54 || e.KeyChar == (Char)55 ||
                e.KeyChar == (Char)56 || e.KeyChar == (Char)57 ||
                e.KeyChar == (Char)13 || e.KeyChar == (Char)8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        //Detail sttings (Optimization method)
        private void cb_ErrorValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToString(cb_ErrorValue.SelectedItem) == "NaN")
            {
                num_NumOfErrorForNaN.Enabled = true;
            }
            else
            {
                num_NumOfErrorForNaN.Enabled = false;
                num_NumOfErrorForNaN.Value = 0;
            }
        }
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = "Layer " + (AmbientLayerCount - (e.RowIndex + 1)).ToString();
            if ((e.RowIndex + 1) == AmbientLayerCount)
            {
                rowIdx = "Substrate";
            }
            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }
        private void dataGridView1_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value != null)
                {
                    if (e.Cell.ColumnIndex == 1)
                    {
                        if (Convert.ToString(dataGridView1.Rows[i].Cells[1].Value) == "" || Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value) < 0)
                        {
                            dataGridView1.Rows[i].Cells[1].Value = 0;
                            return;
                        }
                        else if (Convert.ToDouble(dataGridView1.Rows[i].Cells[1].Value) > 100000)
                        {
                            dataGridView1.Rows[i].Cells[1].Value = 100000;
                            return;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
//#endregion