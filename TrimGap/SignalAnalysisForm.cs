using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TrimGap
{
    public partial class SignalAnalysisForm : Form
    {
        private bool plotflag = false;
        private int Wafertype = 2;
        private double W1 = 0, W2 = 0;
        private string errormsg = "";
        private bool SignalPlotFlag = false;
        private string _filename = "";
        private Bitmap img;
        private int imgW, imgH, point;
        private byte[] imgData;
        private RecipeFormat recipe;

        public SignalAnalysisForm()
        {
            if (sram.UserAuthority == permissionEnum.ad)
            {
                plotflag = true;
            }
            else
            {
                plotflag = false;
            }
            InitializeComponent();
            Init_chartSignal();
            Init_chartSignalPt();
            cb_WaferType.SelectedIndex = 0;
            if (!bWSignalPlot.IsBusy)
            {
                bWSignalPlot.RunWorkerAsync();
            }
            recipe.BlueTapeThreshold = fram.Analysis.BlueTapeThreshold;
            recipe.Step1_Range_step1x0 = fram.Analysis.Step1_Range_step1x0;
            recipe.Step1_Range_step1x1 = fram.Analysis.Step1_Range_step1x1;
            recipe.Step2_Range_step2x0 = fram.Analysis.Step2_Range_step2x0;
            recipe.Step2_Range_step2x1 = fram.Analysis.Step2_Range_step2x1;
            recipe.Step2_Range_step1x0 = fram.Analysis.Step2_Range_step1x0;
            recipe.Step2_Range_step1x1 = fram.Analysis.Step2_Range_step1x1;
            recipe.Range1_Percent = fram.Analysis.Range1_Percent;
            recipe.Range2_Percent = fram.Analysis.Range2_Percent;
        }

        private void Init_chartSignal()
        {
            // 每次使用此function前先清除圖表
            chartSignal.Series[0].Points.Clear();//清除圖表
            chartSignal.Series[1].Points.Clear();//清除圖表
            chartSignal.Series[2].Points.Clear();//清除圖表
            chartSignal.Series[3].Points.Clear();//清除圖表
            chartSignal.Series[4].Points.Clear();//清除圖表
            chartSignal.Series[5].Points.Clear();//清除圖表
            chartSignal.Series[6].Points.Clear();//清除圖表
            chartSignal.Series[7].Points.Clear();//清除圖表

            chartSignal.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray; //背景網格顏色
            chartSignal.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray; //背景網格顏色

            chartSignal.ChartAreas[0].AxisX.Title = "um";
            chartSignal.ChartAreas[0].AxisX.TitleFont = new Font(chartSignal.ChartAreas[0].AxisX.Name, 22);

            //chartSensor.ChartAreas[0].CursorX.IsUserEnabled = true;
            //chartSensor.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            //chartSensor.ChartAreas[0].CursorX.Interval = 0;
            //chartSensor.ChartAreas[0].CursorX.IntervalOffset = 0;
            //chartSensor.ChartAreas[0].AxisX.ScaleView.Zoomable = true;           //啟用縮放視圖
            //chartSensor.ChartAreas[0].AxisX.ScrollBar.BackColor = Color.RosyBrown;
            //chartSensor.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            //chartSensor.ChartAreas[0].AxisX.ScrollBar.ButtonColor = Color.White;
            //chartSensor.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;//啟用X軸滾動條按鈕

            chartSignal.ChartAreas[0].AxisY.Title = "um";
            chartSignal.ChartAreas[0].AxisY.TitleFont = new Font(chartSignal.ChartAreas[0].AxisY.Name, 22);
            chartSignal.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Far;    //設定title位置
            chartSignal.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270; //設定title文字方向
            chartSignal.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Blue;       //設定軸顏色

            chartSignal.ChartAreas[0].CursorX.IsUserEnabled = true;
            chartSignal.ChartAreas[0].CursorX.AutoScroll = true;
            chartSignal.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartSignal.ChartAreas[0].CursorX.SelectionColor = System.Drawing.SystemColors.Highlight;
            chartSignal.ChartAreas[0].CursorY.IsUserEnabled = true;
            chartSignal.ChartAreas[0].CursorY.AutoScroll = true;
            chartSignal.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chartSignal.ChartAreas[0].CursorY.SelectionColor = System.Drawing.SystemColors.Highlight;

            //chartSensor.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
            //chartSensor.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
            //chartSensor.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            //chartSensor.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;
            //chartSensor.ChartAreas[0].CursorY.IsUserEnabled = true;
            //chartSensor.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            //chartSensor.ChartAreas[0].CursorY.Interval = 0;
            //chartSensor.ChartAreas[0].CursorY.IntervalOffset = 0;

            //chartSignal.ChartAreas[0].AxisY2.Title = "%";
            //chartSignal.ChartAreas[0].AxisY2.TitleFont = new Font(chartSignal.ChartAreas[0].AxisY2.Name, 14);
            //chartSignal.ChartAreas[0].AxisY2.TitleAlignment = StringAlignment.Far;
            //chartSignal.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Horizontal;
            //chartSignal.ChartAreas[0].AxisY2.LabelStyle.ForeColor = Color.Red;      //設定線條顏色

            // distance
            // ---------------------
            chartSignal.Series[0].Color = Color.Blue;                 //設定線條顏色
            chartSignal.Series[0].ToolTip = "#VALX,#VALY";            //鼠標停留在數據上，顯示XY值
            chartSignal.Series[0].ChartType = SeriesChartType.FastLine;   //設定線條種類 折線圖 fastline不能用marker
            chartSignal.Series[0].YAxisType = AxisType.Primary;           //主坐標軸
            chartSignal.Series[0].MarkerStyle = MarkerStyle.None;
            chartSignal.Series[1].ToolTip = "#VALX,#VALY";            //鼠標停留在數據上，顯示XY值
            chartSignal.Series[1].ChartType = SeriesChartType.FastLine;   //設定線條種類 折線圖
            chartSignal.Series[1].YAxisType = AxisType.Primary;           //主坐標軸
            chartSignal.Series[1].Color = System.Drawing.Color.Red;                 //設定線條顏色
            chartSignal.ChartAreas[0].AxisX.Maximum = 7000;  //設定X軸最大值 預設為10000
            chartSignal.ChartAreas[0].AxisX.Minimum = 0;     //設定X軸最小值
            chartSignal.ChartAreas[0].AxisX.Interval = 500; //設定X軸間隔 最大值/10

            chartSignal.ChartAreas[0].AxisY.Maximum = 100;  //設定Y軸最大值
            chartSignal.ChartAreas[0].AxisY.Minimum = -70;     //設定Y軸最小值
            chartSignal.ChartAreas[0].AxisY.Interval = 10; //設定Y軸間隔 最大值/10

            chartSignal.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
            chartSignal.ChartAreas[0].AxisY.ScrollBar.Enabled = true;

            chartSignal.Series[0].Points.AddXY(0, 0);
            chartSignal.Series[1].Points.AddXY(0, 0);
            //this.chartSignal.Series.Add(series3);//將線畫在圖上
        }

        private void Init_chartSignalPt()
        {
            // 每次使用此function前先清除圖表
            chartSignalPt.Series[0].Points.Clear();//清除圖表
            chartSignalPt.Series[1].Points.Clear();//清除圖表
            chartSignalPt.Series[2].Points.Clear();//清除圖表
            chartSignalPt.Series[3].Points.Clear();//清除圖表
            chartSignalPt.Series[4].Points.Clear();//清除圖表
            chartSignalPt.Series[5].Points.Clear();//清除圖表
            chartSignalPt.Series[6].Points.Clear();//清除圖表
            chartSignalPt.Series[7].Points.Clear();//清除圖表
            chartSignalPt.Series[8].Points.Clear();//清除圖表

            chartSignalPt.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray; //背景網格顏色
            chartSignalPt.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray; //背景網格顏色

            chartSignalPt.ChartAreas[0].AxisX.Title = "um";
            chartSignalPt.ChartAreas[0].AxisX.TitleFont = new Font(chartSignal.ChartAreas[0].AxisX.Name, 22);

            //chartSensor.ChartAreas[0].CursorX.IsUserEnabled = true;
            //chartSensor.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            //chartSensor.ChartAreas[0].CursorX.Interval = 0;
            //chartSensor.ChartAreas[0].CursorX.IntervalOffset = 0;
            //chartSensor.ChartAreas[0].AxisX.ScaleView.Zoomable = true;           //啟用縮放視圖
            //chartSensor.ChartAreas[0].AxisX.ScrollBar.BackColor = Color.RosyBrown;
            //chartSensor.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            //chartSensor.ChartAreas[0].AxisX.ScrollBar.ButtonColor = Color.White;
            //chartSensor.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;//啟用X軸滾動條按鈕

            chartSignalPt.ChartAreas[0].AxisY.Title = "um";
            chartSignalPt.ChartAreas[0].AxisY.TitleFont = new Font(chartSignal.ChartAreas[0].AxisY.Name, 22);
            chartSignalPt.ChartAreas[0].AxisY.TitleAlignment = StringAlignment.Far;    //設定title位置
            chartSignalPt.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270; //設定title文字方向
            chartSignalPt.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Blue;       //設定軸顏色

            chartSignalPt.ChartAreas[0].CursorX.IsUserEnabled = true;
            chartSignalPt.ChartAreas[0].CursorX.AutoScroll = true;
            chartSignalPt.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartSignalPt.ChartAreas[0].CursorX.SelectionColor = System.Drawing.SystemColors.Highlight;
            chartSignalPt.ChartAreas[0].CursorY.IsUserEnabled = true;
            chartSignalPt.ChartAreas[0].CursorY.AutoScroll = true;
            chartSignalPt.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            chartSignalPt.ChartAreas[0].CursorY.SelectionColor = System.Drawing.SystemColors.Highlight;

            //chartSensor.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
            //chartSensor.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
            //chartSensor.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            //chartSensor.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.All;
            //chartSensor.ChartAreas[0].CursorY.IsUserEnabled = true;
            //chartSensor.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            //chartSensor.ChartAreas[0].CursorY.Interval = 0;
            //chartSensor.ChartAreas[0].CursorY.IntervalOffset = 0;

            //chartSignal.ChartAreas[0].AxisY2.Title = "%";
            //chartSignal.ChartAreas[0].AxisY2.TitleFont = new Font(chartSignal.ChartAreas[0].AxisY2.Name, 14);
            //chartSignal.ChartAreas[0].AxisY2.TitleAlignment = StringAlignment.Far;
            //chartSignal.ChartAreas[0].AxisY2.TextOrientation = TextOrientation.Horizontal;
            //chartSignal.ChartAreas[0].AxisY2.LabelStyle.ForeColor = Color.Red;      //設定線條顏色

            // distance
            // ---------------------
            chartSignalPt.Series[0].Color = Color.Blue;                 //設定線條顏色
            chartSignalPt.Series[0].ToolTip = "#VALX,#VALY";            //鼠標停留在數據上，顯示XY值
            chartSignalPt.Series[0].ChartType = SeriesChartType.FastLine;   //設定線條種類 折線圖 fastline不能用marker
            chartSignalPt.Series[0].YAxisType = AxisType.Primary;           //主坐標軸
            chartSignalPt.Series[0].MarkerStyle = MarkerStyle.None;
            chartSignalPt.Series[1].ToolTip = "#VALX,#VALY";            //鼠標停留在數據上，顯示XY值
            chartSignalPt.Series[1].ChartType = SeriesChartType.FastLine;   //設定線條種類 折線圖
            chartSignalPt.Series[1].YAxisType = AxisType.Primary;           //主坐標軸
            chartSignalPt.Series[1].Color = System.Drawing.Color.Red;                 //設定線條顏色
            //chartSignalPt.ChartAreas[0].AxisX.Maximum = 7000;  //設定X軸最大值 預設為10000
            //chartSignalPt.ChartAreas[0].AxisX.Minimum = 0;     //設定X軸最小值
            //chartSignalPt.ChartAreas[0].AxisX.Interval = 500; //設定X軸間隔 最大值/10

            chartSignalPt.ChartAreas[0].AxisY.Maximum = 100;  //設定Y軸最大值
            chartSignalPt.ChartAreas[0].AxisY.Minimum = 0;     //設定Y軸最小值
            chartSignalPt.ChartAreas[0].AxisY.Interval = 10; //設定Y軸間隔 最大值/10

            chartSignalPt.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
            chartSignalPt.ChartAreas[0].AxisY.ScrollBar.Enabled = true;

            chartSignalPt.Series[0].Points.AddXY(0, 0);
            chartSignalPt.Series[1].Points.AddXY(0, 0);
            //this.chartSignal.Series.Add(series3);//將線畫在圖上
        }

        private void btnSignalAnalysis_Click(object sender, EventArgs e)
        {
            if (!Flag.SensorAnalysisFlag) // 分析中 或是Stage在席On 不給分析
            {
                progressBar_SignalAnalysis.Value = 0;
                //MessageBox.Show("請點選.txt檔案");
                //Console.WriteLine("D:\\FTGM1\\DataDirectory\\" + DateTime.Now.Year.ToString() + "\\");
                if (Directory.Exists("D:\\FTGM1\\DataDirectory\\" + DateTime.Now.Year.ToString() + "\\"))
                {
                    openFileDialog1.InitialDirectory = "D:\\FTGM1\\DataDirectory\\" + DateTime.Now.Year.ToString() + "\\";
                }
                else
                {
                    openFileDialog1.InitialDirectory = "D:\\FTGM1\\DataDirectory\\";
                }
                openFileDialog1.FileName = "請選擇日期及分析檔案名稱";
                /*if (Wafertype == 0)
                {
                    openFileDialog1.Filter = "png files (*.png)|*.txt|All files (*.*)|*.*";
                }
                else
                {
                    openFileDialog1.Filter = "csv files (*.csv)|*.txt|All files (*.*)|*.*";
                }*/

                try
                {
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        //string[] names = openFileDialog1.FileNames;

                        ///
                        Init_chartSignal();
                        //StreamReader readline = new StreamReader(@"C:\Users\user\Desktop\ttt.txt");
                        Init_chartSignalPt();
                        string openFile = openFileDialog1.FileName;//儲存路徑
                        System.IO.FileInfo openName = new FileInfo(openFileDialog1.FileName);//取得完整檔名(含副檔名)
                        string Name = openName.Name.Substring(0, openName.Name.Length - 4);//取得檔名

                        if (Wafertype == 0)
                        {
                            img = new Bitmap(openFileDialog1.FileName);
                            PixelFormat a = img.PixelFormat;
                            imgW = img.Width;
                            imgH = img.Height;
                            point = imgW / 2;
                            BitmapData bd = img.LockBits(new Rectangle(0, 0, imgW, imgH), ImageLockMode.ReadWrite, a);
                            IntPtr intPtr = bd.Scan0;
                            imgData = new byte[imgW * imgH];
                            Marshal.Copy(intPtr, imgData, 0, imgW * imgH);
                            img.UnlockBits(bd);
                            //MemoryStream ms = new MemoryStream();
                            //img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp,);
                            //imgData = ms.GetBuffer();
                            //ImageConverter converter = new ImageConverter();
                            //imgData = (byte[])converter.ConvertTo(img, typeof(byte[]));
                            pB_BlueTape.Image = img;
                            pB_BlueTape.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            pB_BlueTape.Refresh();
                        }
                        else if (Wafertype == 2 || Wafertype == 1)
                        {
                            StreamReader read = new StreamReader(openFileDialog1.FileName);

                            string ReadAll;
                            string[] ReadArray1;//, ReadArray2;
                            ReadAll = read.ReadToEnd(); // 一次讀全部
                            ReadArray1 = Regex.Split(ReadAll, "\r\n", RegexOptions.IgnoreCase);
                            double[] array2 = new double[ReadArray1.Length - 1];
                            for (int i = 0; i < ReadArray1.Length - 1; i++)
                            {
                                array2[i] = Convert.ToSingle(ReadArray1[i]);
                            }
                            progressBar_SignalAnalysis.Value = 10;
                            SignalPlotData.rawData = array2;
                            read.Close();
                        }
                        else if (Wafertype == 3)
                        {
                            StreamReader read = new StreamReader(openFileDialog1.FileName);

                            string ReadAll;
                            string[] ReadArray1;//, ReadArray2;
                            ReadAll = read.ReadToEnd(); // 一次讀全部
                            ReadArray1 = Regex.Split(ReadAll, "\r\n", RegexOptions.IgnoreCase);
                            double[] array2 = new double[ReadArray1.Length - 1];
                            double[] array3 = new double[ReadArray1.Length - 1];
                            double[] array4 = new double[ReadArray1.Length - 1];
                            for (int i = 0; i < ReadArray1.Length - 1; i++)
                            {
                                string[] tmpArray = Regex.Split(ReadArray1[i], ",", RegexOptions.IgnoreCase);
                                array2[i] = Convert.ToSingle(tmpArray[0]);
                                array3[i] = Convert.ToSingle(tmpArray[1]);
                                array4[i] = Convert.ToSingle(tmpArray[2]);
                                Console.WriteLine(i.ToString());
                            }
                            progressBar_SignalAnalysis.Value = 10;
                            SignalPlotData.rawData = array2;
                            SignalPlotData.rawData2 = array3;
                            SignalPlotData.rawData3 = array4;
                            read.Close();
                        }

                        InsertLog.SavetoDB(4, "分析檔案: " + Name);
                        lb_analysisFileName.Text = Name;
                        _filename = Name;
                        SignalPlotFlag = true;
                    }
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                    MessageBox.Show("資料選取錯誤");
                }
            }
            else
            {
                MessageBox.Show("自動模式資料分析中，請等待分析結束再操作。");
            }
        }

        private void bWSignalPlot_DoWork(object sender, DoWorkEventArgs e)
        {
            bWSignalPlot.WorkerReportsProgress = true;
            while (true)
            {
                try
                {
                    SpinWait.SpinUntil(() => false, 100);
                    if (SignalPlotFlag)
                    {
                        if (Wafertype == 0)
                        {
                            Common.TrimGapAnalysis.CalculateBlueTape(imgData, imgW, imgH, 1000, false, recipe.BlueTapeThreshold, true, out SignalPlotData.resultdata);
                        }
                        else if (Wafertype == 1)
                        {
                            for (int i = 0; i < SignalPlotData.rawData.Length; i++)
                            {
                                SignalPlotData.rawData[i] = SignalPlotData.rawData[i] * AnalysisData.um2mm * fram.Analysis.Coefficient;
                            }
                            SignalPlotData.removezeroData = Common.TrimGapAnalysis.removeZero2_threshold(SignalPlotData.rawData, fram.Analysis.LJ_StandardPlane);
                            Console.WriteLine("Analysis start:" + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                            Common.TrimGapAnalysis.tilting(plotflag, SignalPlotData.removezeroData, AnalysisData.Interval_X, out SignalPlotData.tiltingdata_x, out SignalPlotData.tiltingdata_y);
                            //ParamFile.SaveRawdata_Csv(SignalPlotData.tiltingdata_y, "tilting", DateTime.Now);
                            Common.TrimGapAnalysis.CalculateGap(plotflag, SignalPlotData.tiltingdata_x, SignalPlotData.tiltingdata_y, AnalysisData.Interval_X, Wafertype, recipe.Step1_Range_step1x0, recipe.Step1_Range_step1x1, 0, 0, recipe.Range1_Percent, recipe.Range2_Percent, out SignalPlotData.resultdata);
                            Console.WriteLine("Analysis Finish:" + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                        }
                        else if (Wafertype == 2)
                        {
                            for (int i = 0; i < SignalPlotData.rawData.Length; i++)
                            {
                                SignalPlotData.rawData[i] = SignalPlotData.rawData[i] * AnalysisData.um2mm * fram.Analysis.Coefficient;
                            }
                            SignalPlotData.removezeroData = Common.TrimGapAnalysis.removeZero2_threshold(SignalPlotData.rawData, fram.Analysis.LJ_StandardPlane);
                            Console.WriteLine("Analysis start:" + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                            Common.TrimGapAnalysis.tilting(plotflag, SignalPlotData.removezeroData, AnalysisData.Interval_X, out SignalPlotData.tiltingdata_x, out SignalPlotData.tiltingdata_y);
                            //ParamFile.SaveRawdata_Csv(SignalPlotData.tiltingdata_y, "tilting", DateTime.Now);
                            Common.TrimGapAnalysis.CalculateGap(plotflag, SignalPlotData.tiltingdata_x, SignalPlotData.tiltingdata_y, AnalysisData.Interval_X, Wafertype, recipe.Step2_Range_step1x0, recipe.Step2_Range_step1x1, recipe.Step2_Range_step2x0, recipe.Step2_Range_step2x1, recipe.Range1_Percent, recipe.Range2_Percent, out SignalPlotData.resultdata);
                            Console.WriteLine("Analysis Finish:" + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                        }
                        else if (Wafertype == 3)
                        {
                            //int Range2_Percent_tmp = 2100;
                            int Wafertype_tmp = 2;
                            int Interval_X_tmp = 1;

                            Common.TrimGapAnalysis.removeZero3(SignalPlotData.rawData, SignalPlotData.rawData2, SignalPlotData.rawData3, out SignalPlotData.removezeroData, out SignalPlotData.removezeroData2, out SignalPlotData.removezeroData3);
                            Console.WriteLine("Analysis start:" + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                            //Common.TrimGapAnalysis.tilting(plotflag, SignalPlotData.removezeroData, AnalysisData.Interval_X, out SignalPlotData.tiltingdata_x, out SignalPlotData.tiltingdata_y);
                            Common.TrimGapAnalysis.tilting3(plotflag, SignalPlotData.removezeroData, SignalPlotData.removezeroData2, SignalPlotData.removezeroData3, Interval_X_tmp, out SignalPlotData.tiltingdata_x, out SignalPlotData.tiltingdata_x2, out SignalPlotData.tiltingdata_x3, out SignalPlotData.tiltingdata_y, out SignalPlotData.tiltingdata_y2, out SignalPlotData.tiltingdata_y3);
                            //ParamFile.SaveRawdata_Csv(SignalPlotData.tiltingdata_y, "tilting", DateTime.Now);
                            //Common.TrimGapAnalysis.CalculateGap(plotflag, SignalPlotData.tiltingdata_x, SignalPlotData.tiltingdata_y, AnalysisData.Interval_X, Wafertype, fram.Analysis.Step2_Range_step1x0, fram.Analysis.Step2_Range_step1x1, fram.Analysis.Step2_Range_step2x0, fram.Analysis.Step2_Range_step2x1, fram.Analysis.Range1_Percent, fram.Analysis.Range2_Percent, out SignalPlotData.resultdata);
                            Common.TrimGapAnalysis.CalculateGap3(plotflag, Wafertype_tmp, SignalPlotData.tiltingdata_x, SignalPlotData.tiltingdata_x2, SignalPlotData.tiltingdata_x3, SignalPlotData.tiltingdata_y, SignalPlotData.tiltingdata_y2, SignalPlotData.tiltingdata_y3, AnalysisData.Interval_X, recipe.Step2_Range_step1x0, recipe.Step2_Range_step1x1, recipe.Step2_Range_step2x0, recipe.Step2_Range_step2x1, recipe.Range1_Percent, recipe.Range2_Percent, out SignalPlotData.resultdata);
                            Console.WriteLine("Analysis Finish:" + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                        }

                        SignalPlotFlag = false;
                        bWSignalPlot.ReportProgress(0);
                    }
                    else
                    {
                    }
                }
                catch (Exception ee)
                {
                    SignalPlotFlag = false;
                    bWSignalPlot.ReportProgress(10);
                    errormsg = ee.Message;
                    Console.WriteLine(ee.Message);
                    throw;
                }
            }
        }

        private void btnSelectRecipe_Click(object sender, EventArgs e)
        {
            if (Flag.SensorAnalysisFlag) return; // 分析中 或是Stage在席On 不給分析

            openFileDialog1.InitialDirectory = sram.dirfilepath + "\\ProcessJob\\";
            openFileDialog1.FileName = "請選擇Recipe檔案名稱";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileInfo openName = new FileInfo(openFileDialog1.FileName);//取得完整檔名(含副檔名)
                string Name = openName.Name.Substring(0, openName.Name.Length - 4);//取得檔名
                ParamFile.ReadRcpini(Name, "Recipe", recipe);
            }
        }

        private void bWSignalPlot_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                try
                {
                    //

                    #region showtextbox

                    double dif_H = 0;
                    double dif_W = 0;
                    double _w1 = 0;
                    double _w2 = 0;
                    double _h1 = 0;
                    double _h2 = 0;

                    if (Wafertype == 0)
                    {
                        tb_result_0.Text = "0";
                        tb_result_1.Text = "0";
                        tb_result_2.Text = SignalPlotData.resultdata[0].ToString("f2");// + fram.Analysis.OffsetBlueTapeW;
                        tb_result_3.Text = "0";

                        List<double> xData = new List<double>();
                        for (int i = 0; i < imgH; i++)
                        {
                            xData.Add(i * 1.2);
                        }

                        List<int> yData = new List<int>();
                        for (int i = 0; i < imgH; i++)
                        {
                            yData.Add(imgData[imgW * i + point]);
                        }
                        chart1.Series[0].Points.DataBindXY(xData, yData);
                    }
                    else
                    {
                        #region Plot
                        if (Wafertype != 3)
                        {
                            double dif = -(SignalPlotData.removezeroData.Length - SignalPlotData.tiltingdata_x.Length) * 2.5;

                            chartSignal.ChartAreas[0].AxisX.Minimum = -500;
                            chartSignal.ChartAreas[0].AxisY.Minimum = -10;

                            if (SignalPlotData.removezeroData.Max() + Math.Abs(SignalPlotData.removezeroData.Min()) > SignalPlotData.tiltingdata_y.Max())
                            {
                                if (SignalPlotData.removezeroData.Max() / 10 < 10)
                                {
                                    chartSignal.ChartAreas[0].AxisY.Maximum = ((int)((SignalPlotData.removezeroData.Max() + Math.Abs(SignalPlotData.removezeroData.Min())) / 10) + 1) * 10;
                                }
                                else
                                {
                                    chartSignal.ChartAreas[0].AxisY.Maximum = ((int)((SignalPlotData.removezeroData.Max() + Math.Abs(SignalPlotData.removezeroData.Min())) / 100) + 1) * 100;
                                }
                            }
                            else
                            {
                                if (SignalPlotData.tiltingdata_y.Max() / 10 < 10)
                                {
                                    chartSignal.ChartAreas[0].AxisY.Maximum = ((int)(SignalPlotData.tiltingdata_y.Max() / 10) + 1) * 10;
                                }
                                else
                                {
                                    chartSignal.ChartAreas[0].AxisY.Maximum = ((int)(SignalPlotData.tiltingdata_y.Max() / 100) + 1) * 100;
                                }
                            }

                            if (chartSignal.ChartAreas[0].AxisY.Maximum - chartSignal.ChartAreas[0].AxisY.Minimum > 100)
                            {
                                chartSignal.ChartAreas[0].AxisY.Interval = 100; //設定Y軸間隔 最大值/10
                            }
                            else
                            {
                                chartSignal.ChartAreas[0].AxisY.Interval = 10; //設定Y軸間隔 最大值/10
                            }

                            for (int i = 0; i < SignalPlotData.tiltingdata_y.Length; i++)
                            {
                                chartSignal.Series[0].Points.AddXY(SignalPlotData.tiltingdata_x[i] + dif, SignalPlotData.removezeroData[i] - SignalPlotData.removezeroData.Min());
                                chartSignal.Series[1].Points.AddXY(SignalPlotData.tiltingdata_x[i], SignalPlotData.tiltingdata_y[i]);
                            }
                          
                            if (Wafertype == 1)
                            {
                                chartSignal.Series[2].Points.AddXY(0, SignalPlotData.resultdata[6]);
                                chartSignal.Series[2].Points.AddXY(SignalPlotData.tiltingdata_x[SignalPlotData.tiltingdata_x.Length - 1], SignalPlotData.resultdata[6]);
                                chartSignal.Series[3].Points.AddXY(0, SignalPlotData.resultdata[7]);
                                chartSignal.Series[3].Points.AddXY(SignalPlotData.tiltingdata_x[SignalPlotData.tiltingdata_x.Length - 1], SignalPlotData.resultdata[7]);
                                chartSignal.Series[4].Points.AddXY(0, SignalPlotData.resultdata[8]);
                                chartSignal.Series[4].Points.AddXY(SignalPlotData.tiltingdata_x[SignalPlotData.tiltingdata_x.Length - 1], SignalPlotData.resultdata[8]);
                                if(cb_TrimWaferEdgeEvaluate.Checked)
                                {
                                    chartSignal.Series[5].Points.AddXY(SignalPlotData.resultdata[9]+dif, 0);
                                    chartSignal.Series[5].Points.AddXY(SignalPlotData.resultdata[9]+dif, SignalPlotData.tiltingdata_y.Max());
                                }
                                else
                                {
                                    chartSignal.Series[5].Points.AddXY(SignalPlotData.resultdata[9], 0);
                                    chartSignal.Series[5].Points.AddXY(SignalPlotData.resultdata[9], SignalPlotData.tiltingdata_y.Max());
                                }

                                chartSignal.Series[6].Points.AddXY(SignalPlotData.resultdata[10], 0);
                                chartSignal.Series[6].Points.AddXY(SignalPlotData.resultdata[10], SignalPlotData.tiltingdata_y.Max());
                                chartSignal.Series[7].Points.AddXY(SignalPlotData.resultdata[11], 0);
                                chartSignal.Series[7].Points.AddXY(SignalPlotData.resultdata[11], SignalPlotData.tiltingdata_y.Min());

                            }
                            else
                            {
                                chartSignal.Series[2].Points.AddXY(0, SignalPlotData.resultdata[6]);    // Surface 1 H2
                                chartSignal.Series[2].Points.AddXY(SignalPlotData.tiltingdata_x[SignalPlotData.tiltingdata_x.Length - 1], SignalPlotData.resultdata[6]);
                                chartSignal.Series[3].Points.AddXY(0, SignalPlotData.resultdata[7]);    // Surface 2 H1
                                chartSignal.Series[3].Points.AddXY(SignalPlotData.tiltingdata_x[SignalPlotData.tiltingdata_x.Length - 1], SignalPlotData.resultdata[7]);
                                chartSignal.Series[4].Points.AddXY(0, SignalPlotData.resultdata[8]);    // Surface 3
                                chartSignal.Series[4].Points.AddXY(SignalPlotData.tiltingdata_x[SignalPlotData.tiltingdata_x.Length - 1], SignalPlotData.resultdata[8]);
                                if (cb_TrimWaferEdgeEvaluate.Checked)
                                {
                                    chartSignal.Series[5].Points.AddXY(SignalPlotData.resultdata[9]+dif, 0);    // Slope 1
                                    chartSignal.Series[5].Points.AddXY(SignalPlotData.resultdata[9]+dif, SignalPlotData.tiltingdata_y.Max());
                                }
                                else
                                {
                                    chartSignal.Series[5].Points.AddXY(SignalPlotData.resultdata[9], 0);    // Slope 1
                                    chartSignal.Series[5].Points.AddXY(SignalPlotData.resultdata[9], SignalPlotData.tiltingdata_y.Max());
                                }
                                chartSignal.Series[6].Points.AddXY(SignalPlotData.resultdata[10], 0);   // Slope 2
                                chartSignal.Series[6].Points.AddXY(SignalPlotData.resultdata[10], SignalPlotData.tiltingdata_y.Max());
                                chartSignal.Series[7].Points.AddXY(SignalPlotData.resultdata[11], 0);   // Slope 3
                                chartSignal.Series[7].Points.AddXY(SignalPlotData.resultdata[11], SignalPlotData.tiltingdata_y.Max());

                            }

                            #endregion Plot

                            if (SignalPlotData.resultdata != null)
                            {
                                if (Wafertype == 1)
                                {
                                    tb_result_0.Text = SignalPlotData.resultdata[0].ToString("f1");// + fram.Analysis.Offset1StepH;
                                    _h1 = SignalPlotData.resultdata[0];
                                    tb_result_1.Text = "0";
                                    if (cb_TrimWaferEdgeEvaluate.Checked)
                                    {
                                        tb_result_2.Text = (SignalPlotData.resultdata[1] - dif).ToString("f1");// + fram.Analysis.Offset1StepW;
                                        _w1 = SignalPlotData.resultdata[1]-dif;
                                    }
                                    else
                                    {
                                        tb_result_2.Text = SignalPlotData.resultdata[1].ToString("f1");// + fram.Analysis.Offset1StepW;
                                        _w1 = SignalPlotData.resultdata[1];
                                    }
                                    tb_result_3.Text = "0";
                                }
                                else
                                {
                                    tb_result_0.Text = SignalPlotData.resultdata[0].ToString("f1");// + fram.Analysis.Offset2StepH1;
                                    _h1 = SignalPlotData.resultdata[0];
                                    tb_result_1.Text = (SignalPlotData.resultdata[2] + SignalPlotData.resultdata[0]).ToString("f1");// + fram.Analysis.Offset2StepH2;
                                    _h2 = SignalPlotData.resultdata[2] + SignalPlotData.resultdata[0];
                                    if (cb_TrimWaferEdgeEvaluate.Checked)
                                    {
                                        tb_result_2.Text = (SignalPlotData.resultdata[1] + SignalPlotData.resultdata[3]-dif).ToString("f1");// + fram.Analysis.Offset2StepW1;
                                        _w1 = SignalPlotData.resultdata[1] + SignalPlotData.resultdata[3] - dif;
                                        tb_result_3.Text = (SignalPlotData.resultdata[3] - dif).ToString("f1");// + fram.Analysis.Offset2StepW2;
                                        _w2 = SignalPlotData.resultdata[3] - dif;
                                    }
                                    else
                                    {
                                        tb_result_2.Text = (SignalPlotData.resultdata[1] + SignalPlotData.resultdata[3]).ToString("f1");// + fram.Analysis.Offset2StepW1;
                                        _w1 = SignalPlotData.resultdata[1] + SignalPlotData.resultdata[3];
                                        tb_result_3.Text = SignalPlotData.resultdata[3].ToString("f1");// + fram.Analysis.Offset2StepW2;
                                        _w2 = SignalPlotData.resultdata[3];
                                    }
                                }
                            }

                            if (Wafertype != 0)
                                //ParamFile.SaveRawdata_png(chartSignal, lb_analysisFileName.Text, DateTime.Now);
                                ParamFile.SaveRawdata_png(chartSignal, lb_analysisFileName.Text, DateTime.Now, dif_H.ToString(), dif_W.ToString(), _w1, _w2, _h1, _h2);
                        }
                        else
                        {//Wafertype == 3
                            double dif = -(SignalPlotData.removezeroData.Length - SignalPlotData.tiltingdata_x.Length) * 2.5;

                            chartSignalPt.ChartAreas[0].AxisX.Minimum = 0;
                            chartSignalPt.ChartAreas[0].AxisY.Minimum = 0;

                            //if (SignalPlotData.removezeroData.Max() + Math.Abs(SignalPlotData.removezeroData.Min()) > SignalPlotData.tiltingdata_y.Max())
                            //{
                            //    if (SignalPlotData.removezeroData.Max() / 10 < 10)
                            //    {
                            //        chartSignalPt.ChartAreas[0].AxisY.Maximum = ((int)((SignalPlotData.removezeroData.Max() + Math.Abs(SignalPlotData.removezeroData.Min())) / 10) + 1) * 10;
                            //    }
                            //    else
                            //    {
                            //        chartSignalPt.ChartAreas[0].AxisY.Maximum = ((int)((SignalPlotData.removezeroData.Max() + Math.Abs(SignalPlotData.removezeroData.Min())) / 100) + 1) * 100;
                            //    }
                            //}
                            //else
                            //{
                            //    if (SignalPlotData.tiltingdata_y.Max() / 10 < 10)
                            //    {
                            //        chartSignalPt.ChartAreas[0].AxisY.Maximum = ((int)(SignalPlotData.tiltingdata_y.Max() / 10) + 1) * 10;
                            //    }
                            //    else
                            //    {
                            //        chartSignalPt.ChartAreas[0].AxisY.Maximum = ((int)(SignalPlotData.tiltingdata_y.Max() / 100) + 1) * 100;
                            //    }
                            //}

                            chartSignalPt.ChartAreas[0].AxisY.Maximum = SignalPlotData.tiltingdata_y.Max() + 20;

                            if (chartSignalPt.ChartAreas[0].AxisY.Maximum - chartSignalPt.ChartAreas[0].AxisY.Minimum > 100)
                            {
                                chartSignalPt.ChartAreas[0].AxisY.Interval = 100; //設定Y軸間隔 最大值/10
                            }
                            else
                            {
                                chartSignalPt.ChartAreas[0].AxisY.Interval = 10; //設定Y軸間隔 最大值/10
                            }


                            chartSignalPt.Series[0].ChartType = SeriesChartType.Point;
                            chartSignalPt.Series[0].Color = Color.Yellow;
                            chartSignalPt.Series[1].ChartType = SeriesChartType.Point;
                            chartSignalPt.Series[1].Color = Color.Red;
                            chartSignalPt.Series[2].ChartType = SeriesChartType.Point;
                            chartSignalPt.Series[2].Color = Color.Green;
                            for (int i = 0; i < SignalPlotData.tiltingdata_y.Length; i++)
                            {
                                chartSignalPt.Series[0].Points.AddXY(SignalPlotData.tiltingdata_x[i], SignalPlotData.tiltingdata_y[i]);
                            }

                            for (int i = 0; i < SignalPlotData.tiltingdata_y2.Length; i++)
                            {
                                chartSignalPt.Series[1].Points.AddXY(SignalPlotData.tiltingdata_x2[i], SignalPlotData.tiltingdata_y2[i]);
                            }

                            for (int i = 0; i < SignalPlotData.tiltingdata_y3.Length; i++)
                            {
                                chartSignalPt.Series[2].Points.AddXY(SignalPlotData.tiltingdata_x3[i], SignalPlotData.tiltingdata_y3[i]);
                            }
                            chartSignalPt.Series[3].Points.AddXY(0, SignalPlotData.resultdata[6]);    // Surface 1 H2
                            chartSignalPt.Series[3].Points.AddXY(SignalPlotData.tiltingdata_x[SignalPlotData.tiltingdata_x.Length - 1], SignalPlotData.resultdata[6]);
                            chartSignalPt.Series[4].Points.AddXY(0, SignalPlotData.resultdata[7]);    // Surface 2 H1
                            chartSignalPt.Series[4].Points.AddXY(SignalPlotData.tiltingdata_x[SignalPlotData.tiltingdata_x.Length - 1], SignalPlotData.resultdata[7]);
                            chartSignalPt.Series[5].Points.AddXY(0, SignalPlotData.resultdata[8]);    // Surface 3
                            chartSignalPt.Series[5].Points.AddXY(SignalPlotData.tiltingdata_x[SignalPlotData.tiltingdata_x.Length - 1], SignalPlotData.resultdata[8]);
                            chartSignalPt.Series[6].Points.AddXY(SignalPlotData.resultdata[9], 0);    // Slope 1
                            chartSignalPt.Series[6].Points.AddXY(SignalPlotData.resultdata[9], SignalPlotData.tiltingdata_y.Max());
                            chartSignalPt.Series[7].Points.AddXY(SignalPlotData.resultdata[10], 0);   // Slope 2
                            chartSignalPt.Series[7].Points.AddXY(SignalPlotData.resultdata[10], SignalPlotData.tiltingdata_y.Max());
                            chartSignalPt.Series[8].Points.AddXY(SignalPlotData.resultdata[11], 0);   // Slope 3
                            chartSignalPt.Series[8].Points.AddXY(SignalPlotData.resultdata[11], SignalPlotData.tiltingdata_y.Max());

                            if (SignalPlotData.resultdata != null)
                            {
                                tb_result_0.Text = SignalPlotData.resultdata[0].ToString("f1");// + fram.Analysis.Offset2StepH1;
                                _h1 = SignalPlotData.resultdata[0];
                                tb_result_1.Text = (SignalPlotData.resultdata[2] + SignalPlotData.resultdata[0]).ToString("f1");// + fram.Analysis.Offset2StepH2;
                                _h2 = SignalPlotData.resultdata[2] + SignalPlotData.resultdata[0];
                                tb_result_2.Text = (SignalPlotData.resultdata[1] + SignalPlotData.resultdata[3]).ToString("f1");// + fram.Analysis.Offset2StepW1;
                                _w1 = SignalPlotData.resultdata[1] + SignalPlotData.resultdata[3];
                                tb_result_3.Text = SignalPlotData.resultdata[3].ToString("f1");// + fram.Analysis.Offset2StepW2;
                                _w2 = SignalPlotData.resultdata[3];
                            }

                            ParamFile.SaveRawdata_png(chartSignalPt, lb_analysisFileName.Text, DateTime.Now, dif_H.ToString(), dif_W.ToString(), _w1, _w2, _h1, _h2);
                        
                        
                        }


                    }

                    #endregion showtextbox

                    

                    #region datagridview

                    string[] tmp = new string[5];
                    if (Wafertype == 1)
                    {
                        tmp[0] = _h1.ToString("f1");//SignalPlotData.resultdata[0].ToString("f1");// + fram.Analysis.Offset1StepH;                                       //H1
                        tmp[1] = _w1.ToString("f1"); //SignalPlotData.resultdata[1].ToString("f1");// + fram.Analysis.Offset1StepW;     //W1
                        tmp[2] = "0";//(SignalPlotData.resultdata[2] + SignalPlotData.resultdata[0]).ToString("f1");       //H2
                        tmp[3] = "0";//SignalPlotData.resultdata[3].ToString("f1");                                       //W2
                    }
                    else if (Wafertype == 2 || Wafertype == 3)
                    {
                        tmp[0] = _h1.ToString("f1"); //SignalPlotData.resultdata[0].ToString("f1");// + fram.Analysis.Offset2StepH1;                                       //H1
                        tmp[1] = _w1.ToString("f1"); //(SignalPlotData.resultdata[1] + SignalPlotData.resultdata[3]).ToString("f1");// + fram.Analysis.Offset2StepW1;     //W1
                        tmp[2] = _h2.ToString("f1"); //(SignalPlotData.resultdata[2] + SignalPlotData.resultdata[0]).ToString("f1");// + fram.Analysis.Offset2StepH2;       //H2
                        tmp[3] = _w2.ToString("f1"); //SignalPlotData.resultdata[3].ToString("f1");// + fram.Analysis.Offset2StepW2;                                       //W2
                    }
                    else
                    {
                        tmp[0] = "0";   //H1
                        tmp[1] = SignalPlotData.resultdata[0].ToString("f2");// + fram.Analysis.OffsetBlueTapeW;
                        tmp[2] = "0";   //H2
                        tmp[3] = "0";   //W2
                    }
                    if (Wafertype != 0)
                    {
                        if (SignalPlotData.resultdata[12] == 0)
                        {
                            tmp[4] = _filename;                                                          //Note
                        }
                        if (SignalPlotData.resultdata[12] == 1)
                        {
                            tmp[4] = _filename + " Chipping";                                                          //Note
                        }
                        dataGridView1.Rows.Add(tmp);
                    }

                    #endregion datagridview

                    progressBar_SignalAnalysis.Value = 100;
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    Console.WriteLine(ee.Message);
                    progressBar_SignalAnalysis.Value = 100;
                }
            }
            else if (e.ProgressPercentage == 10)
            {
                MessageBox.Show(errormsg);
                //MessageBox.Show("分析錯誤，請重新選擇檔案及確認參數");
            }
        }

        private void btn_ClearDatagridview_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void cb_WaferType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Wafertype = cb_WaferType.SelectedIndex;
            if (Wafertype == 0)
            {
                tabControl1.SelectedIndex = 1;
            }
            else if (Wafertype == 3)
            {
                tabControl1.SelectedIndex = 2;
            }
            else {
                tabControl1.SelectedIndex = 0;
            }
        }
    }
}