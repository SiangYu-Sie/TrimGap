using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TrimGap
{
    internal class AutoRunStage
    {
        public static int Trimtest = 0;
        public static int Trimtest_break = 0;
        public static int Tapetest = 0;
        public static AutoStep AutoRunStageStep;
        public static TrimStep AutoTrimStep; // 第一步都會先旋轉
        public static Trim2ndStep AutoTrim2ndStep;
        public static HTWStep AutoHTWStep;
        public static BluetapeStep AutoBlueTapeStep;
        public static TTVStep AutoTTVStep;
        public static RecordCCDStep AutoRecordCCDStep;
        public static Chart chartSensor;
        public static string err = string.Empty;

        //public static bool WaferPresence = false;
        public static bool AnalysisFlag = true;

        public static bool Measure_TrimType_On = false;
        public static bool Measure_TrimType2nd_On = false;
        public static bool Measure_BlueTape_On = false;
        public static bool Measure_TTV_On = false;
        public static bool Measure_HTW_On = false;
        public static bool Measure_RecordCCD_On = false;
        public static bool Measure_TrimType_Done = false;
        public static bool Measure_TrimType2nd_Done = false;
        public static bool Measure_BlueTape_Done = false;
        public static bool Measure_TTV_Done = false;
        public static bool Measure_HTW_Done = false;
        public static bool Measure_RecordCCD_Done = false;
        public static string FoupID = "r";
        public static int Slot = 0;
        private static int MachineType = 0; // 0:AP6, 1:N2
        private static int MotionType = 0; // 0:SSC, 1:ETEL
        private static int DemoModeSlot = 1;
        private static int DemoModeFileIndex = 0;

        private static List<double[]> list_data = new List<double[]>();
        private static List<double[]> list_data_2 = new List<double[]>();
        private static List<double[]> list_data_3 = new List<double[]>();
        private static List<double[]> list_data_4 = new List<double[]>();
        private static List<double[]> list_data_5 = new List<double[]>();
        private static List<double[]> list_data_base = new List<double[]>();

        public static bool isDarkReference = false;

        int is_HTW = 0; //選擇哪種SENSOR去做AutoFocus，0是HTW原本的對焦方式，1是confocal的對焦方式


        //public bool AnalysisFlag
        //{
        //    get { return analysisFlag; }
        //    set { analysisFlag = value; }
        //}

        public struct Homepos
        {
            public struct AP6
            {
                public struct BlueTape
                {
                    public static double X = 135.86 * 1000;
                    public static double Y = 171.24 * 1000;
                    public static double Angle = -92;
                }
                public struct Precitec
                {
                    public static double X = 135.86 * 1000;
                    public static double Y = 171.24 * 1000;
                    public static double Angle = 90;
                }
            }

            public struct AP6II
            {
                public struct LJ8020
                {
                    public static double X = 135.86 * 1000;
                    public static double Y = 171.24 * 1000;
                    public static double Angle = -90;
                    public static double Z = 0;
                }
                public struct BlueTape
                {
                    public static double X = 135.86 * 1000;
                    public static double Y = 171.24 * 1000;
                    public static double Angle = -2;
                }
                public struct HTW
                {
                    public static double X = 135.86 * 1000;
                    public static double Y = 171.24 * 1000;
                    public static double Angle = 90;
                    public static double AP6II_Z3 = 0;
                    public static double AP6II_X = 0;
                }
                public struct RecordCCD
                {
                    public static double X = 135.86 * 1000;
                    public static double Y = 171.24 * 1000;
                    public static double Angle = 45;
                    public static double AP6II_Z2 = 0;
                }
            }

            public struct N2
            {
                public struct WaferSwitch
                {
                    public static double X = 315 * 1000;
                    public static double Y = 79.5 * 1000;
                }

                public struct LJ8020
                {
                    public static double X = 135.86 * 1000;
                    public static double Y = 171.24 * 1000;
                }
                public struct BlueTape
                {
                    public static double X = 135.86 * 1000;
                    public static double Y = 171.24 * 1000;
                    public static double Angle = -92;
                }
                public struct SF3
                {
                    public static double X = 166 * 1000;
                    public static double Y = 168 * 1000;
                }
                public struct RecordCCD
                {
                    public static double X = 135.86 * 1000;
                    public static double Y = 171.24 * 1000;
                    public static double Angle = 45;
                }

                public struct Precitec
                {
                    public static double X = 135.86 * 1000;
                    public static double Y = 171.24 * 1000;
                    public static double Angle = 90;
                }
            }

            public struct Done
            {
                public static bool DD = false;
                public static bool X = false;
                public static bool Y = false;
                public static bool Z = false;
            }
        }

        public void inputChart(Chart chart) // 把form的chart丟進來這裡做更新
        {
            chartSensor = chart;
        }

        public static BackgroundWorker bWAutoRun = new BackgroundWorker();

        public AutoRunStage(int machineType)
        {
            MachineType = machineType;
            //MotionType = motionType;
            AutoTrimStep = TrimStep.LJHome;
            if (MachineType == 0) // AP6
            {
                Measure_TrimType_On = true;
                Measure_TrimType2nd_On = true;
                Measure_BlueTape_On = true;
                Measure_TTV_On = false;
                Measure_RecordCCD_On = false;
                Measure_HTW_On = false;
            }
            else if (MachineType == 2) // AP6 II
            {
                Measure_TrimType_On = true;
                Measure_TrimType2nd_On = false;
                Measure_BlueTape_On = true;
                Measure_TTV_On = false;
                Measure_RecordCCD_On = true;
                Measure_HTW_On = true;

                //20250418
                Flag.isPT_2 = Convert.ToBoolean(fram.Analysis.PT_2);

                if(fram.m_simulateRun == 0)
                {
                    Common.motion.FindHome(Mo.AxisNo.DD);
                    Common.motion.FindHome(Mo.AxisNo.Z);
                    Common.motion.FindHome(Mo.AxisNo.AP6II_Z2);
                    Common.motion.FindHome(Mo.AxisNo.AP6II_Z3);
                    Common.motion.FindHome(Mo.AxisNo.AP6II_X);
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 20000); // 等待10秒
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.Z), 10000); // 等待10秒
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_Z2), 10000); // 等待10秒
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3), 10000); // 等待10秒
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_X), 20000); // 等待20秒
                    if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.motion.MotionDone(Mo.AxisNo.Z) && Common.motion.MotionDone(Mo.AxisNo.AP6II_Z2) && Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3) && Common.motion.MotionDone(Mo.AxisNo.AP6II_X))
                    {
                        Common.motion.PosMove(Mo.AxisNo.DD, 0);
                        Common.motion.PosMove(Mo.AxisNo.Z, 0);
                        Common.motion.PosMove(Mo.AxisNo.AP6II_Z2, 0);
                        Common.motion.PosMove(Mo.AxisNo.AP6II_Z3, 0);
                        Common.motion.PosMove(Mo.AxisNo.AP6II_X, 0);
                    }
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 10000); // 等待10秒
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.Z), 5000); // 等待5秒
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_Z2), 5000); // 等待5秒
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3), 5000); // 等待5秒
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_X), 15000); // 等待15秒
                }
            }
            else if (MachineType == 1) // N2
            {
                Measure_TrimType_On = false;
                Measure_TrimType2nd_On = true;
                Measure_BlueTape_On = false;
                Measure_TTV_On = true;
                Measure_RecordCCD_On = false;
                Measure_HTW_On = false;

                if (fram.m_simulateRun == 0)
                {
                    Common.motion.FindHome(Mo.AxisNo.DD);
                    Common.motion.FindHome(Mo.AxisNo.X);
                    Common.motion.FindHome(Mo.AxisNo.Y);
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 20000); // 等待10秒
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.X), 10000); // 等待10秒
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.Y), 10000); // 等待10秒
                    if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y))
                    {
                        Common.motion.PosMove(Mo.AxisNo.DD, 0);
                        Common.motion.PosMove(Mo.AxisNo.X, Homepos.N2.WaferSwitch.X);
                        Common.motion.PosMove(Mo.AxisNo.Y, Homepos.N2.WaferSwitch.Y);
                    }
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 10000); // 等待10秒
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.X), 15000); // 等待10秒
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.Y), 15000); // 等待10秒
                }
            }
            AutoRunStageStep = AutoStep.GotoSwitchWaferPos;
            InitBwAutorun();
        }

        public static void InitBwAutorun()
        {
            bWAutoRun.DoWork += new DoWorkEventHandler(bWAutoRun_DoWork);
            bWAutoRun.ProgressChanged += new ProgressChangedEventHandler(bWAutoRun_ProgressChanged);
            //bWAutoRun.RunWorkerCompleted += new RunWorkerCompletedEventHandler(MotionAutoFocusWorker_RunWorkerCompleted);
            bWAutoRun.WorkerReportsProgress = true;
            if (!bWAutoRun.IsBusy)
            {
                bWAutoRun.RunWorkerAsync();
            }
        }

        public enum TrimStep : int
        {
            [Description("LJHome")]
            LJHome,

            [Description("Wafer旋轉")]
            DDRotate,

            [Description("開始量測")]
            Measurement,

            [Description("取得量測資料")]
            Download,

            [Description("判斷量測次數")]
            JudgeRotateCount,

            [Description("數據分析")]
            Analysis,

            [Description("儲存log")]
            Savelog,

            [Description("上拋SECS")]
            UpdateSecs,

            [Description("結束")]
            Finish = 255,
        }

        public enum Trim2ndStep : int
        {
            [Description("PTHome")]
            PTHome,

            [Description("Wafer旋轉")]
            DDRotate,

            [Description("開始量測")]
            Measurement,

            [Description("取得量測資料")]
            Download,

            [Description("判斷量測次數")]
            JudgeRotateCount,

            [Description("數據分析")]
            Analysis,

            [Description("儲存log")]
            Savelog,

            [Description("上拋SECS")]
            UpdateSecs,

            [Description("結束")]
            Finish = 255,
        }

        public enum HTWStep : int
        {
            [Description("HTWHome")]
            HTWHome,

			[Description("HTWAutoFocus")]
            HTWAutoFocus,							 					 
            [Description("Wafer旋轉")]
            DDRotate,

            [Description("開始量測")]
            Measurement,

            [Description("移至起點_New")]
            MoveToStart_New_Sensor,

            [Description("開始量測_New")]
            Measurement_New_Sensor,

            [Description("取得量測資料")]
            Download,

            [Description("開始量測2")]
            Measurement2,

            [Description("取得量測資料2")]
            Download2,

            [Description("判斷量測次數")]
            JudgeRotateCount,

            [Description("數據分析")]
            Analysis,

            [Description("儲存log")]
            Savelog,

            [Description("上拋SECS")]
            UpdateSecs,

            [Description("結束")]
            Finish = 255,
        }

        public enum BluetapeStep : int
        {
            [Description("BlueTapeStart")]
            BlueTapeStart,

            [Description("CCDHome")]
            CCDHome,

            [Description("Wafer旋轉")]
            DDRotate,

            [Description("開始量測")]
            Measurement,

            [Description("取得量測資料")]
            Download,

            [Description("判斷量測次數")]
            JudgeRotateCount,

            [Description("數據分析")]
            Analysis,

            [Description("儲存log")]
            Savelog,

            [Description("上拋SECS")]
            UpdateSecs,

            [Description("結束")]
            Finish = 255,
        }

        public enum TTVStep : int
        {
            [Description("TTVStart")]
            TTVStart,

            [Description("TTVHome")]
            TTVHome,

            [Description("TTVHomeDone")]
            TTVHomeDone,

            [Description("Wafer旋轉")]
            DDRotate,

            [Description("Wafer旋轉完成")]
            DDRotateDone,

            [Description("Wafer平移")]
            ShiftPosition,

            [Description("Wafer平移完成")]
            ShiftPositionDone,

            [Description("開始量測")]
            Measurement,

            [Description("判斷量測次數XY Shift")]
            JudgeShiftCount,

            [Description("判斷量測次數")]
            JudgeRotateCount,

            [Description("數據分析")]
            Analysis,

            [Description("儲存log")]
            Savelog,

            [Description("結束")]
            Finish = 255,
        }
        

        public enum RecordCCDStep : int
        {
            [Description("RecordCCDStepStart")]
            RecordCCDStart,

            [Description("CCDHome")]
            CCDHome,

            [Description("Wafer旋轉")]
            DDRotate,

            [Description("開始量測")]
            Measurement,

            [Description("取得量測資料")]
            Download,

            [Description("判斷量測次數")]
            JudgeRotateCount,

            [Description("數據分析")]
            Analysis,

            [Description("儲存log")]
            Savelog,

            [Description("上拋SECS")]
            UpdateSecs,

            [Description("結束")]
            Finish = 255,
        }
        public enum AutoStep : int
        {
            [Description("GotoSwitchWaferPos")]
            GotoSwitchWaferPos,

            [Description("GotoModePos")]
            GotoModePos,

            [Description("TrimMode")]
            TrimMode,

            [Description("TrimMode2nd")]
            TrimMode2nd,

            [Description("HTWMode")]
            HTWMode,

            [Description("BlueTapeMode")]
            BlueTapeMode,

            [Description("TTVMode")]
            TTVMode,

            [Description("RecordCCDMode")]
            RecordCCDMode,

            [Description("等待Stage1_Wafer")]
            WaitWaferPresence,

            [Description("汽缸下降")]
            CylinderDown,

            [Description("開真空")]
            VacuumOn,

            [Description("取得LoadPortSlot資訊")]
            GetSlotInfo,

            [Description("開始量測")]
            Measurement,

            [Description("Wafer旋轉")]
            DDRotate,

            [Description("取得量測資料")]
            Download,

            [Description("判斷量測次數")]
            JudgeRotateCount,

            [Description("判斷Slot次數")]
            JudgeSlotCount,

            [Description("停止")]
            Stop,

            [Description("資料上傳SECS")]
            SecsGemUpdate,

            [Description("數據分析")]
            Analysis,

            [Description("儲存log")]
            Savelog,

            [Description("儲存異常log")]
            Errorlog,

            [Description("清除資料")]
            ClearData,

            [Description("破真空")]
            VacuumOff,

            [Description("汽缸上升")]
            CylinderUp,

            [Description("回等待取走wafer位置")]
            GotoWaitGetHomePos,

            [Description("等待取走wafer")]
            WaitWaferunload,

            [Description("結束")]
            Finish = 255,
        }

        private static void Measurement_TrimType_LJ()
        {
            switch (AutoTrimStep)
            {
                case TrimStep.LJHome:
                    sram.PitchAngleTotal = 0;
                    AnalysisData.rotateCount_current = 0;
                    for (int i = 0; i < sram.Recipe.Rotate_Count; i++)
                    {
                        AnalysisData.WaferMeasure[i] = false;
                    }
                    if (MachineType == 0) // AP6 不用回home
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.DD))
                        {
                            //Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive,0);
                            Common.motion.PosMove(Mo.AxisNo.DD, fram.m_WaferAlignAngle);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 5000);
                            AutoTrimStep = TrimStep.DDRotate;
                            Flag.isPT = false;
                        }
                    }
                    else if (MachineType == 2) // AP6II
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.motion.MotionDone(Mo.AxisNo.Z))
                        {
                            Common.motion.PosMove(Mo.AxisNo.Z, fram.Position.LJ_Z);
                            Common.motion.PosMove(Mo.AxisNo.DD, Homepos.AP6II.LJ8020.Angle + fram.m_WaferAlignAngle);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 5000);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.Z), 5000);
                            AutoTrimStep = TrimStep.DDRotate;
                            Flag.isPT = false;
                        }
                    }
                    else if (MachineType == 1) // N2 要移動到LJ的home點
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y))
                        {
                            //這裡要下XY軸的絕對位置，走去LJ的原點
                            Common.motion.PosMove(Mo.AxisNo.X, Homepos.N2.LJ8020.X);
                            Common.motion.PosMove(Mo.AxisNo.Y, Homepos.N2.LJ8020.Y);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.X), 10000); // 等待10秒
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.Y), 10000); // 等待10秒
                            if (Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y))
                            {
                                AutoTrimStep = TrimStep.DDRotate;
                            }
                        }
                    }
                    AnalysisData.LJ_W2 = new double[8];
                    break;

                case TrimStep.DDRotate:
                    if (fram.S_MotionRotate == "False") // Bypass motion
                    {
                        AutoTrimStep = TrimStep.Measurement;
                    }
                    else
                    {
                        if (MachineType == 0 || MachineType == 2) // AP6
                        {
                            if (!Common.motion.MotionDone(Mo.AxisNo.DD))
                            {
                                if (Trimtest_break < 2)
                                {
                                    InsertLog.SavetoDB(67, "Trim:358-0");
                                    Trimtest_break++;
                                }
                                break;  // 動作未完成就跳走等等再看一次
                            }
                            if (Trimtest < 2)
                            {
                                InsertLog.SavetoDB(67, "Trim:358");
                                Trimtest++;
                            }
                        }
                        else if (MachineType == 1) // N26
                        {
                            if (!Common.motion.MotionDone(Mo.AxisNo.DD) || !Common.motion.MotionDone(Mo.AxisNo.X) || !Common.motion.MotionDone(Mo.AxisNo.Y))
                            {
                                break; // 動作未完成就跳走等等再看一次
                            }
                            InsertLog.SavetoDB(67);
                        }
                        if (AnalysisData.rotateCount_current == 0)
                        {
                            //sram.PitchAngle = 1;
                            sram.PitchAngle = sram.Recipe.Angle[0];
                        }
                        else if (AnalysisData.rotateCount_current == 1)
                        {
                            //sram.PitchAngle = fram.Analysis.PitchAngle - 1;
                            if (sram.Recipe.Rotate_Count == 4)
                            {
                                sram.PitchAngle = sram.Recipe.Angle[2] - sram.Recipe.Angle[0];
                            }
                            else
                            {
                                sram.PitchAngle = sram.Recipe.Angle[1] - sram.Recipe.Angle[0];
                            }
                        }
                        else
                        {
                            //sram.PitchAngle = fram.Analysis.PitchAngle;
                            if (sram.Recipe.Rotate_Count == 4)
                            {
                                sram.PitchAngle = sram.Recipe.Angle[AnalysisData.rotateCount_current * 2]
                                                - sram.Recipe.Angle[AnalysisData.rotateCount_current * 2 - 2];
                            }
                            else
                            {
                                sram.PitchAngle = sram.Recipe.Angle[AnalysisData.rotateCount_current]
                                                - sram.Recipe.Angle[AnalysisData.rotateCount_current - 1];
                            }
                        }
                        sram.PitchAngleTotal += sram.PitchAngle;
                        InsertLog.SavetoDB(67, sram.PitchAngleTotal.ToString());
                        //20240104 自我檢測時不進入
                        if (fram.EFEMSts.Skip == 0 || fram.EFEMSts.Skip == 1)
                        {
                            Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive, sram.PitchAngle);
                        }
                        SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 2000);
                        if (!Common.motion.MotionDone(Mo.AxisNo.DD))
                        {
                            Console.WriteLine("DD Rotate Time Out");
                            InsertLog.SavetoDB(67, "DD Rotate Time Out");
                        }
                        AutoTrimStep = TrimStep.Measurement;
                    }
                    break;

                case TrimStep.Measurement:
                    if (Common.motion.MotionDone(Mo.AxisNo.DD))
                    {
                        double posnow = Common.motion.Get_FBAngle(Mo.AxisNo.DD) - fram.m_WaferAlignAngle;
                        InsertLog.SavetoDB(69, "Target:" + sram.PitchAngleTotal.ToString() + ",Real:" + posnow.ToString());
                        Console.WriteLine(DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString() + " Measure Start");

                        if (fram.S_SensorConnectType == 0)
                        {

                            Common.LJ.GetFolderPath(fram.LJ_FTPfilePath);
                            Common.LJ.Csvlist = Common.LJ.GetCsvlist(Common.LJ.FolderPath);
                            Common.LJ.CurrentListCount = Common.LJ.GetCurrentCsvListCount(Common.LJ.FolderPath);

                            AnalysisData.rtn = Common.LJ.Measure();
                        }
                        else if (fram.S_SensorConnectType == 1)
                        {
                            int a_Angle = (int)fram.Analysis.LJ_Measure_Count;

                            if (a_Angle == 0)
                            {
                                //存量測數值
                                double[] s_val = new double[3200];

                                //清除
                                Common.LJX8000A.ClearMemory();

                                //開始Trigger
                                Common.LJX8000A.Trigger();
                                Common.LJX8000A.Trigger();
                                Common.LJX8000A.Trigger();
                                Common.LJX8000A.Trigger();
                                Common.LJX8000A.Trigger();
                                Common.LJX8000A.Trigger();

                                //取得數值
                                AnalysisData.rawData = Common.LJX8000A.GetProfile();
                                //s_val = Common.LJX8000A.GetProfile();

                                //存至List
                                //list_data.Add(s_val);
                            }
                            else
                            {
                                double[] Add_Angle = new double[11];

                                switch (a_Angle)
                                {
                                    case 1:
                                        Add_Angle = new double[] { -0.1, 0, 0.1 };
                                        break;
                                    case 2:
                                        Add_Angle = new double[] { -0.2, -0.1, 0, 0.1, 0.1, 0.2 };
                                        break;
                                    case 3:
                                        Add_Angle = new double[] { -0.3, -0.2, -0.1, 0, 0.1, 0.2, 0.3 };
                                        break;
                                    case 4:
                                        Add_Angle = new double[] { -0.4, -0.3, -0.2, -0.1, 0, 0.1, 0.2, 0.3, 0.4 };
                                        break;
                                    case 5:
                                        Add_Angle = new double[] { -0.5, -0.4 - 0.3, -0.2, -0.1, 0, 0.1, 0.2, 0.3, 0.4, 0.5 };
                                        break;
                                }

                                for (int i = 0; i < Add_Angle.Count(); i++)
                                {
                                    //存量測數值
                                    double[] s_val = new double[3200];

                                    //根據原本角度加減
                                    double a = posnow + Add_Angle[i];
                                    //旋轉至加減後的角度
                                    Common.motion.PosMove(Mo.AxisNo.DD, posnow);

                                    //清除
                                    Common.LJX8000A.ClearMemory();

                                    //開始Trigger
                                    Common.LJX8000A.Trigger();
                                    Common.LJX8000A.Trigger();
                                    Common.LJX8000A.Trigger();
                                    Common.LJX8000A.Trigger();
                                    Common.LJX8000A.Trigger();
                                    Common.LJX8000A.Trigger();

                                    //取得數值
                                    s_val = Common.LJX8000A.GetProfile();

                                    //存至List
                                    list_data.Add(s_val);

                                }
                            }
                            //Common.LJX8000A.ClearMemory();

                            //Common.LJX8000A.Trigger();
                            //Common.LJX8000A.Trigger();
                            //Common.LJX8000A.Trigger();
                            //Common.LJX8000A.Trigger();
                            //Common.LJX8000A.Trigger();
                            //Common.LJX8000A.Trigger();
                        }

                        AutoTrimStep = TrimStep.Download;
                        Console.WriteLine(DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString() + " Measure End");
                    }

                    break;

                case TrimStep.Download:
                    if (fram.S_SensorConnectType == 0)
                    {
                        Common.LJ.Csvlist = Common.LJ.GetCsvlist(Common.LJ.FolderPath);
                        AnalysisData.LastFilePath = Common.LJ.GetlastfilePath(Common.LJ.FolderPath);
                        // 先判斷資料數量在判斷資料大小
                        if (Common.LJ.GetCurrentCsvListCount(Common.LJ.FolderPath) > Common.LJ.CurrentListCount)
                        {
                            Console.WriteLine(DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString() + " Download");
                            SpinWait.SpinUntil(() => Common.LJ.GetFileSize(AnalysisData.LastFilePath) > AnalysisData.FtpFileSize, 1000);

                            Common.LJ.DownloadName = Common.LJ.Csvlist[Common.LJ.Csvlist.Length - 2];
                            AnalysisData.rawData = Common.LJ.Downloaddata(Common.LJ.DownloadName);
                            if (AnalysisData.rawData.Length < 3200)
                            {
                                break;
                            }
                            sram.saveDataTime = DateTime.Now;
                            ParamFile.SaveRawdata_Csv(AnalysisData.rawData, FoupID, FoupID + "_" + Slot + "_" + sram.PitchAngleTotal, sram.saveDataTime);
                            Console.WriteLine("Download End " + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                            InsertLog.SavetoDB(70, Common.LJ.DownloadName);
                            AutoTrimStep = TrimStep.Analysis;
                        }
                    }
                    else if (fram.S_SensorConnectType == 1)
                    {
                        //AnalysisData.rawData = AnalysisData.list_data[0];//Common.LJX8000A.GetProfile();
                        //AnalysisData.rawData = Avg_Data(list_data, 3200);
                        
                        sram.saveDataTime = DateTime.Now;
                        ParamFile.SaveRawdata_Csv(AnalysisData.rawData, FoupID, FoupID + "_" + Slot + "_" + sram.PitchAngleTotal, sram.saveDataTime);
                        Console.WriteLine("Download End " + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                        InsertLog.SavetoDB(70, Common.LJ.DownloadName);
                        Common.LJX8000A.ClearMemory();
                        AutoTrimStep = TrimStep.Analysis;                        
                    }
                    if (fram.m_DEMOMode == 1)
                    {
                        string DatasavPath;
                        DatasavPath = sram.Rootfilepath + "\\DataDirectory\\DemoData\\LJ";
                        DirectoryInfo di = new DirectoryInfo(DatasavPath);
                        FileInfo[] fi = di.GetFiles();
                        StreamReader read = new StreamReader(DatasavPath + "\\" + fi[DemoModeFileIndex].Name);

                        string ReadAll;
                        string[] ReadArray1;//, ReadArray2;
                        ReadAll = read.ReadToEnd(); // 一次讀全部
                        ReadArray1 = Regex.Split(ReadAll, "\r\n", RegexOptions.IgnoreCase);
                        double[] array2 = new double[ReadArray1.Length - 1];
                        for (int i = 0; i < ReadArray1.Length - 1; i++)
                        {
                            array2[i] = Convert.ToSingle(ReadArray1[i]);
                        }
                        AnalysisData.rawData = array2;
                        read.Close();
                        DemoModeFileIndex++;
                        if (DemoModeFileIndex >= fi.Count())
                            DemoModeFileIndex = 0;
                    }
                    break;

                case TrimStep.Analysis:
                    InsertLog.SavetoDB(71);
                    Flag.SensorAnalysisFlag = true;
                    Flag.SaveChartFlag = false; //主畫面分析
                    //AutoTrimStep = TrimStep.JudgeRotateCount;
                    AutoTrimStep = TrimStep.Savelog;

                    
                    break;

                case TrimStep.Savelog:
                    if (!Flag.SensorAnalysisFlag && Flag.SaveChartFlag)
                    {
                        InsertLog.SavetoDB(72);
                        AnalysisData.WaferMeasure[AnalysisData.rotateCount_current] = true;
                        SpinWait.SpinUntil(() => false, 100);
                        AutoTrimStep = TrimStep.JudgeRotateCount;
                    }
                    break;

                case TrimStep.JudgeRotateCount:
                    Console.WriteLine("CurrentTimes : " + AnalysisData.rotateCount_current);

                    if (AnalysisData.rotateCount_current < sram.Recipe.Rotate_Count)
                    {
                        AnalysisData.rotateCount_current += 1;
                        if (AnalysisData.rotateCount_current == sram.Recipe.Rotate_Count)
                        {
                            //Common.motion.PitchAngle(SSCNET.AxisNo.DD, SSCNET.Dir.Postive, sram.PitchAngle);
                            //Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive, 360 - sram.PitchAngleTotal); // 最到最後一個角度後 補到360度
                            if((MachineType == 0 && sram.Recipe.Type == 3) || MachineType == 1 || MachineType == 2)  //有做PT時先回0度 或是  N2那種檯面一定要回0度
                            {
                                Common.motion.PosMove(Mo.AxisNo.DD, 0); // 回0度                               
                            }
                            else
                            {
                                Common.motion.PosMove(Mo.AxisNo.DD, fram.m_WaferBackToFoupAngle); 
                            }                            
                            SpinWait.SpinUntil(() => false, 500);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 20000);
                            InsertLog.SavetoDB(72);
                            AutoTrimStep = TrimStep.UpdateSecs;
                            break;
                        }
                        else
                        {
                            AutoTrimStep = TrimStep.DDRotate;
                        }
                    }

                    break;

                case TrimStep.UpdateSecs:

                    #region 算完8個角度後計算最大值

                    string Slot_InfoMax = "";
                    double max_H1 = 0, max_H2 = 0, max_W1 = 0, max_W2 = 0;

                    for (int i = 0; i < 25; i++)
                    {
                        max_H1 = 0; max_H2 = 0; max_W1 = 0; max_W2 = 0;
                        if (EFEM.IsInit)
                        {
                            if (sram.Recipe.Slot[i] == 1)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    if (fram.EFEMSts.H1[i, j] > max_H1)
                                    {
                                        max_H1 = fram.EFEMSts.H1[i, j];
                                    }
                                }
                                for (int j = 0; j < 8; j++)
                                {
                                    if (fram.EFEMSts.H2[i, j] > max_H2)
                                    {
                                        max_H2 = fram.EFEMSts.H2[i, j];
                                    }
                                }
                                for (int j = 0; j < 8; j++)
                                {
                                    if (fram.EFEMSts.W1[i, j] > max_W1)
                                    {
                                        max_W1 = fram.EFEMSts.W1[i, j];
                                    }
                                }
                                for (int j = 0; j < 8; j++)
                                {
                                    if (fram.EFEMSts.W2[i, j] > max_W2)
                                    {
                                        max_W2 = fram.EFEMSts.W2[i, j];
                                    }
                                }

                                if (sram.Recipe.Type == 0)
                                {
                                    Slot_InfoMax += "0,";
                                    Slot_InfoMax += max_W1 + ",";
                                    Slot_InfoMax += "0,";
                                    Slot_InfoMax += "0";
                                }
                                else if (sram.Recipe.Type == 1) // 1step
                                {
                                    Slot_InfoMax += max_H1 + ",";
                                    Slot_InfoMax += max_W1 + ",";
                                    Slot_InfoMax += "0,";
                                    Slot_InfoMax += "0";
                                }
                                else if (sram.Recipe.Type == 2 || sram.Recipe.Type == 3) // 2 step    //
                                {
                                    Slot_InfoMax += max_H1 + ",";
                                    Slot_InfoMax += max_W1 + ",";
                                    Slot_InfoMax += max_H2 + ",";
                                    Slot_InfoMax += max_W2 + "";
                                }
                            }
                            else
                            {
                                Slot_InfoMax += "0,";
                                Slot_InfoMax += "0,";
                                Slot_InfoMax += "0,";
                                Slot_InfoMax += "0";
                            }
                        }
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Slot1_Max + i, Slot_InfoMax, out err);
                        Slot_InfoMax = "";
                    }

                    #endregion 算完8個角度後計算最大值

                    string Slot_Info = "";
                    for (int i = 0; i < 25; i++)
                    {
                        Slot_Info += sram.Recipe.Type + ","; // 0: blue tape, 1: 1step, 2: 2step
                        Slot_Info += sram.Recipe.Rotate_Count + ","; // 4 or 8 個點
                        for (int j = 0; j < sram.Recipe.Rotate_Count; j++) // 每一片的 4/8 個位置
                        {
                            // 加入 Angle
                            if (sram.Recipe.Rotate_Count == 4)
                            {
                                if (EFEM.IsInit)
                                {
                                    if (Common.EFEM.LoadPort_Run.Slot[i] == 1)
                                    {
                                        Slot_Info += sram.Recipe.Angle[j * 2] + ",";
                                    }
                                    else
                                    {
                                        Slot_Info += "0,";
                                    }
                                }
                                else
                                {
                                    Slot_Info += sram.Recipe.Angle[j * 2] + ",";
                                }
                            }
                            else
                            {
                                if (EFEM.IsInit)
                                {
                                    if (Common.EFEM.LoadPort_Run.Slot[i] == 1)
                                    {
                                        Slot_Info += sram.Recipe.Angle[j] + ",";
                                    }
                                    else
                                    {
                                        Slot_Info += "0,";
                                    }
                                }
                                else
                                {
                                    Slot_Info += sram.Recipe.Angle[j] + ",";
                                }
                            }

                            // 每個type上傳的不一樣
                            if (sram.Recipe.Type == 0) // Blue Tape
                            {
                                Slot_Info += 0 + ",";
                                Slot_Info += fram.EFEMSts.W1[i, j] + ",";                           // blueTape只有W1
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                            }
                            else if (sram.Recipe.Type == 1) // 1 step
                            {
                                Slot_Info += fram.EFEMSts.H1[i, j] + ",";                           // H1
                                Slot_Info += fram.EFEMSts.W1[i, j] + ",";                           // H2
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                            }
                            else if (sram.Recipe.Type == 2 || sram.Recipe.Type == 3) // 2 step    //
                            {
                                Slot_Info += fram.EFEMSts.H1[i, j] + ",";                           // h1 = H2
                                Slot_Info += fram.EFEMSts.W1[i, j] + ",";   // w1 = W1+W2
                                Slot_Info += fram.EFEMSts.H2[i, j] + ",";   // h2 = H1+H2
                                Slot_Info += fram.EFEMSts.W2[i, j] + ",";                           // w2 = W1
                            }
                            else
                            {
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                            }
                        }
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Slot1_Info + i, Slot_Info, out err);
                        Slot_Info = "";
                    }
                    if (EFEM.IsInit)
                    {
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort_Run.FoupID, out err);
                        if (Common.EFEM.LoadPort_Run.pn == LoadPort.Pn.P1)
                        {
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                        }
                        else if (Common.EFEM.LoadPort_Run.pn == LoadPort.Pn.P2)
                        {
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                        }
                    }

                    double[] data1 = new double[8];
                    double[] data2 = new double[8];
                    double[] data3 = new double[8];
                    double[] data4 = new double[8];
                    for (int i=0; i<8; i++)
                    {
                        data1[i] = fram.EFEMSts.H1[Slot - 1, i];
                        data2[i] = fram.EFEMSts.W1[Slot - 1, i];
                        data3[i] = fram.EFEMSts.H2[Slot - 1, i];
                        data4[i] = fram.EFEMSts.W2[Slot - 1, i];
                    }

                    ParamFile.SaveRawdata_Csv4(data1, data2, data3, data4, max_H1, max_W1, max_H2, max_W2, FoupID, FoupID + "_" + Slot + "_" + "LJ_resultdata", DateTime.Now, AnalysisData.rotateCount_current);

                    AutoTrimStep = TrimStep.Finish;
                    break;

                case TrimStep.Finish:
                    
                    Measure_TrimType_Done = true;
                    AnalysisData.rotateCount_current = 0;
                    AutoTrimStep = TrimStep.LJHome;

                    Trimtest = 0;
                    Trimtest_break = 0;
                    break;

                default:
                    AutoTrimStep = TrimStep.LJHome;
                    break;
            }
        }

        private static void Measurement_TrimType_PT()
        {
            switch (AutoTrim2ndStep)
            {
                case Trim2ndStep.PTHome:
                    sram.PitchAngleTotal = 0;
                    sram.PTRetry = 0;
                    AnalysisData.rotateCount_current = 0;
                    for (int i = 0; i < sram.Recipe.Rotate_Count; i++)
                    {
                        AnalysisData.WaferMeasure[i] = false;
                    }
                    if (MachineType == 0 || MachineType == 2) // AP6 不用回home
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.DD))
                        {
                            //Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive,Homepos.Precitec.Angle);
                            Common.motion.PosMove(Mo.AxisNo.DD, Homepos.AP6.Precitec.Angle + fram.m_WaferAlignAngle);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 5000);
                            AutoTrim2ndStep = Trim2ndStep.DDRotate;
                            Flag.isPT = true;
                        }
                    }
                    else if (MachineType == 1) // N2 要移動到LJ的home點
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y))
                        {
                            //這裡要下XY軸的絕對位置，走去LJ的原點
                            Common.motion.PosMove(Mo.AxisNo.X, Homepos.N2.Precitec.X);
                            Common.motion.PosMove(Mo.AxisNo.Y, Homepos.N2.Precitec.Y);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.X), 10000); // 等待10秒
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.Y), 10000); // 等待10秒
                            if (Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y))
                            {
                                AutoTrim2ndStep = Trim2ndStep.DDRotate;
                            }
                        }
                    }
                    break;

                case Trim2ndStep.DDRotate:
                    if (fram.S_MotionRotate == "False") // Bypass motion
                    {
                        AutoTrim2ndStep = Trim2ndStep.Measurement;
                    }
                    else
                    {
                        if (MachineType == 0 || MachineType == 2) // AP6
                        {
                            if (!Common.motion.MotionDone(Mo.AxisNo.DD))
                            {
                                if (Trimtest_break < 2)
                                {
                                    InsertLog.SavetoDB(67, "Trim:358-0");
                                    Trimtest_break++;
                                }
                                break;  // 動作未完成就跳走等等再看一次
                            }
                            if (Trimtest < 2)
                            {
                                InsertLog.SavetoDB(67, "Trim:358");
                                Trimtest++;
                            }
                        }
                        else if (MachineType == 1) // N26
                        {
                            if (!Common.motion.MotionDone(Mo.AxisNo.DD) || !Common.motion.MotionDone(Mo.AxisNo.X) || !Common.motion.MotionDone(Mo.AxisNo.Y))
                            {
                                break; // 動作未完成就跳走等等再看一次
                            }
                            InsertLog.SavetoDB(67);
                        }
                        if (AnalysisData.rotateCount_current == 0)
                        {
                            //sram.PitchAngle = 1;
                            sram.PitchAngle = sram.Recipe.Angle[0];
                        }
                        else if (AnalysisData.rotateCount_current == 1)
                        {
                            //sram.PitchAngle = fram.Analysis.PitchAngle - 1;
                            if (sram.Recipe.Rotate_Count == 4)
                            {
                                sram.PitchAngle = sram.Recipe.Angle[2] - sram.Recipe.Angle[0];
                            }
                            else
                            {
                                sram.PitchAngle = sram.Recipe.Angle[1] - sram.Recipe.Angle[0];
                            }
                        }
                        else
                        {
                            //sram.PitchAngle = fram.Analysis.PitchAngle;
                            if (sram.Recipe.Rotate_Count == 4)
                            {
                                sram.PitchAngle = sram.Recipe.Angle[AnalysisData.rotateCount_current * 2]
                                                - sram.Recipe.Angle[AnalysisData.rotateCount_current * 2 - 2];
                            }
                            else
                            {
                                sram.PitchAngle = sram.Recipe.Angle[AnalysisData.rotateCount_current]
                                                - sram.Recipe.Angle[AnalysisData.rotateCount_current - 1];
                            }
                        }
                        sram.PitchAngleTotal += sram.PitchAngle;
                        InsertLog.SavetoDB(67, sram.PitchAngleTotal.ToString());
                        Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive, sram.PitchAngle);
                        Common.PTForm.PointMove(1);//PLC走到起點
                        SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 2000);
                        if (!Common.motion.MotionDone(Mo.AxisNo.DD))
                        {
                            Console.WriteLine("DD Rotate Time Out");
                            InsertLog.SavetoDB(67, "DD Rotate Time Out");
                        }
                        AutoTrim2ndStep = Trim2ndStep.Measurement;
                        sram.PTRetry = 0;
                        fram.PT_PLC_AutoRunStage_RetryCount = 0;
                    }
                    break;

                case Trim2ndStep.Measurement:
                    if(MachineType == 1)
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.DD))
                        {
                            //x = Mo.motion_ETEL.listDsaDrive[1];

                            //if (x.getStatus().isPresent())
                            //{
                            //    Console.WriteLine("X axis is alive");
                            //}
                            //// check X axis homed status, if not do the homing after that move to 0.0
                            //if (x.getStatusFromDrive().isHomingDone())
                            //{
                            //    Console.WriteLine("X axis homing have already done.");
                            //    x.startProfiledMovement(0.137500, 0.1, 1.0, 0.01, 3000); // X move to 0.0
                            //    Console.WriteLine("X axis homing has done, X axis at 0.0 position");
                            //}
                            //else
                            //{
                            //    Console.WriteLine("Homing X axis");
                            //    x.homingStart(10000);
                            //    x.waitMovement(15000);
                            //    x.startProfiledMovement(0.137500, 0.1, 1.50, 0.01, 3000); // X move to 0.0
                            //}

                            ////int a_Angle = (int)fram.Analysis.PT_Measure_Count;

                            ////if(a_Angle == 0)
                            ////{
                            //Common.PTForm.StartTrigger_N2();
                            //x.waitTime(2.0, 3500);                                       // wait 2s for starting trigger function
                            //Console.WriteLine("X axis start to trigger function");
                            //x.enableTrigger(Dsa.TRIGGER_NOT_MOVING, 0, 2, false, Dsa.DEF_TIMEOUT);  // eable trigger function
                            //x.startProfiledMovement(0.132650, 0.0008, 3.0, 0.05, 3000000);                    // X move to 6mm
                            //x.disableTrigger(Dsa.TRIGGER_NOT_MOVING, false, 30000);                        // disable trigger function
                            //x.waitTime(2.0, 3500);
                            //x.startProfiledMovement(0.137500, 0.0008, 3.0, 0.01, 3000000);                    // X move back to 0.0m with fast speed
                            //                                                                                  //}

                            //Common.motion.Reset_All_Trigger_Parameters(Mo.AxisNo.X);
                            //AutoTrim2ndStep = Trim2ndStep.Download;
                            //Console.WriteLine(DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString() + " Measure End");
                        }
                    }
                    else
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.PTForm.GetPointMoveFinish(1))
                        {
                            double posnow = Common.motion.Get_FBAngle(Mo.AxisNo.DD) - fram.m_WaferAlignAngle - Homepos.AP6.Precitec.Angle;
                            InsertLog.SavetoDB(69, "Target:" + sram.PitchAngleTotal.ToString() + ",Real:" + posnow.ToString());
                            Console.WriteLine(DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString() + " Measure Start");

                            Common.PTForm.StartTrigger(4802, 48010, 0, -10);
                            SpinWait.SpinUntil(() => false, 1000);
                            Common.PTForm.PointMove(2);
                            //Common.LJ.GetFolderPath(fram.LJ_FTPfilePath);
                            //Common.LJ.Csvlist = Common.LJ.GetCsvlist(Common.LJ.FolderPath);
                            //Common.LJ.CurrentListCount = Common.LJ.GetCurrentCsvListCount(Common.LJ.FolderPath);

                            //AnalysisData.rtn = Common.LJ.Measure();

                            AutoTrim2ndStep = Trim2ndStep.Download;
                            Console.WriteLine(DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString() + " Measure End");
                        }
                        else if (fram.PT_PLC_AutoRunStage_RetryCount < 100)
                        {
                            SpinWait.SpinUntil(() => false, 50);
                            fram.PT_PLC_AutoRunStage_RetryCount++;
                        }
                        else
                        {
                            fram.PT_PLC_AutoRunStage_RetryCount = 0;
                            Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_PT_PLC_MoveTimeoutError, true, out err);
                            InsertLog.SavetoDB(TrimGap_EqpID.EQP_PT_PLC_MoveTimeoutError, "PT PLC Move To Point 1 Timeout");
                            //MessageBox.Show("PT PLC Move To Point 1 Timeout");
                        }
                    }
                    

                    break;

                case Trim2ndStep.Download:
                    if (Common.PTForm.GetPointMoveFinish(2))
                    {
                        if (fram.m_simulateRun == 0)
                        {
                            SpinWait.SpinUntil(() => false, 1500);
                            bool rtn = Common.PTForm.isFinish();

                            if (rtn || sram.PTRetry >= 3)
                            {
                                sram.saveDataTime = DateTime.Now;
                                //Common.PTForm.getData3(out AnalysisData.rawData, out AnalysisData.rawData2, out AnalysisData.rawData3);
                                Common.PTForm.getData(out AnalysisData.rawData);
                                Common.PTForm.getData2(out AnalysisData.rawData2);
                                Common.PTForm.getData3(out AnalysisData.rawData3);
                                ParamFile.SaveRawdata_Csv3(AnalysisData.rawData, AnalysisData.rawData2, AnalysisData.rawData3, FoupID, FoupID + "_" + Slot + "_" + sram.PitchAngleTotal + "_PT_" + rtn.ToString(), sram.saveDataTime);


                                Console.WriteLine("PT Download End " + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                                InsertLog.SavetoDB(70, FoupID + "_" + Slot + "_" + sram.PitchAngleTotal + "_PT");
                                AutoTrim2ndStep = Trim2ndStep.Analysis;
                                sram.PTRetry = 0;
                            }
                            else
                            {
                                Common.PTForm.PointMove(1);//PLC走到起點
                                sram.PTRetry++;
                                AutoTrim2ndStep = Trim2ndStep.Measurement;
                            }
                        }
                        else
                        {
                            StreamReader read = new StreamReader("PT.csv");

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
                            AnalysisData.rawData = array2;
                            AnalysisData.rawData2 = array3;
                            AnalysisData.rawData3 = array4;
                            read.Close();
                        }

                    }
                    break;

                case Trim2ndStep.Analysis:
                    InsertLog.SavetoDB(71);

                    /*
                    int zeroindex = 0;

                    for (int i = 0; i < AnalysisData.rawData.Length; i++)
                    {
                        if (AnalysisData.rawData[i] > 10)
                        {
                            zeroindex = i;
                            break;
                        } 
                    }
                    double[] rawData2 = new double[AnalysisData.rawData.Length - zeroindex];
                    for (int i = 0; i < rawData2.Length; i++)
                    {
                        rawData2[i] = -1*AnalysisData.rawData[i + AnalysisData.rawData.Length - rawData2.Length];
                    }
                    int mid = (fram.Analysis.Step2_Range_step1x0 + fram.Analysis.Step2_Range_step2x1) / 2;
                    int low = fram.Analysis.Step2_Range_step2x0 - 500;
                    int high = fram.Analysis.Step2_Range_step1x1 + 500;
                    if (low - 50>=0 && high +50 < rawData2.Length)   //都有資料
                    {
                        double h0, h1, h2, h0a, h1a, h2a;
                        h1 = 0;
                        h2 = 0;
                        h0 = 0;
                        h0a = 0;
                        h1a = 0;
                        h2a = 0;
                        int h0i = 0;
                        int h1i = 0;
                        int h2i = 0;
                        for(int i = 0;i<101;i++)
                        {
                            if(rawData2[low - 50 + i] != 0) 
                                h0 += rawData2[low - 50 + i];
                            if (rawData2[mid - 50 + i] != 0)                                  
                                h1 += rawData2[mid - 50 + i];
                            if (rawData2[high - 50 + i] != 0)
                                h2 += rawData2[high - 50 + i];
                        }
                        h0 = h0 / 101;
                        h1 = h1 / 101;
                        h2 = h2 / 101;

                        for (int i = 0; i < 101; i++)
                        {
                            if((rawData2[low - 50 + i] - h0) <=5 && (rawData2[low - 50 + i] - h0) >= -5)
                            {
                                h0a += rawData2[low - 50 + i];
                                h0i++;
                            }
                            if ((rawData2[mid - 50 + i] - h1) <= 5 && (rawData2[mid - 50 + i] - h1) >= -5)
                            {
                                h1a += rawData2[mid - 50 + i];
                                h1i++;
                            }
                            if ((rawData2[high - 50 + i] - h2) <= 5 && (rawData2[high - 50 + i] - h2) >= -5)
                            {
                                h2a += rawData2[high - 50 + i];
                                h2i++;
                            }

                        }

                        if(h0i == 0 || h1i == 0 || h2i == 0)
                        {
                            fram.EFEMSts.H1[Slot - 1, AnalysisData.rotateCount_current] = 0;
                            fram.EFEMSts.H2[Slot - 1, AnalysisData.rotateCount_current] = 0;
                            InsertReportTable(DateTime.Now, FoupID, Slot, sram.PitchAngleTotal, 0, 0, 0, 0, "Fail", "PT");
                        }
                        else
                        {
                            switch (sram.Recipe.OffsetType)
                            {
                                case 1:
                                    fram.Analysis.Offset_PT_1StepH = fram.Analysis.Offset.Offline_PT_1StepH;
                                    fram.Analysis.Offset_PT_1StepW = fram.Analysis.Offset.Offline_PT_1StepW;
                                    break;

                                case 2:
                                    fram.Analysis.Offset_PT_2StepH1 = fram.Analysis.Offset.Offline_PT_2StepH1;
                                    fram.Analysis.Offset_PT_2StepW1 = fram.Analysis.Offset.Offline_PT_2StepW1;
                                    fram.Analysis.Offset_PT_2StepH2 = fram.Analysis.Offset.Offline_PT_2StepH2;
                                    fram.Analysis.Offset_PT_2StepW2 = fram.Analysis.Offset.Offline_PT_2StepW2;
                                    break;

                                case 3:
                                    fram.Analysis.Offset_PT_1StepH = fram.Analysis.Offset.Inline_PT_1StepH;
                                    fram.Analysis.Offset_PT_1StepW = fram.Analysis.Offset.Inline_PT_1StepW;
                                    break;

                                case 4:
                                    fram.Analysis.Offset_PT_2StepH1 = fram.Analysis.Offset.Inline_PT_2StepH1;
                                    fram.Analysis.Offset_PT_2StepW1 = fram.Analysis.Offset.Inline_PT_2StepW1;
                                    fram.Analysis.Offset_PT_2StepH2 = fram.Analysis.Offset.Inline_PT_2StepH2;
                                    fram.Analysis.Offset_PT_2StepW2 = fram.Analysis.Offset.Inline_PT_2StepW2;
                                    break;

                                case 5: // QC offset
                                    if (sram.Recipe.Rotate_Count == 8)
                                    {
                                        fram.Analysis.Offset_PT_1StepH = fram.Analysis.Offset.QC_PT_1StepH[AnalysisData.rotateCount_current];
                                        fram.Analysis.Offset_PT_1StepW = fram.Analysis.Offset.QC_PT_1StepW[AnalysisData.rotateCount_current];
                                    }
                                    else
                                    {
                                        fram.Analysis.Offset_PT_1StepH = fram.Analysis.Offset.QC_PT_1StepH[AnalysisData.rotateCount_current * 2 + 1];
                                        fram.Analysis.Offset_PT_1StepW = fram.Analysis.Offset.QC_PT_1StepW[AnalysisData.rotateCount_current * 2 + 1];
                                    }
                                    break;
                                default:
                                    break;
                            }

                            h0 = h0a / h0i;
                            h1 = h1a / h1i;
                            h2 = h2a / h2i;

                            fram.EFEMSts.H1[Slot - 1, AnalysisData.rotateCount_current] = Math.Round((h2 - h1),2) + fram.Analysis.Offset_PT_2StepH1;
                            fram.EFEMSts.H2[Slot - 1, AnalysisData.rotateCount_current] = Math.Round((h2 - h0),2) + fram.Analysis.Offset_PT_2StepH2;

                            double d1, d2;
                            d1 = fram.EFEMSts.H1[Slot - 1, AnalysisData.rotateCount_current];
                            d2 = fram.EFEMSts.H2[Slot - 1, AnalysisData.rotateCount_current];
                            InsertReportTable(DateTime.Now, FoupID, Slot, sram.PitchAngleTotal, d1, 0, d2, 0, "OK", "PT");
                        }
                    }
                    else
                    {
                        fram.EFEMSts.H1[Slot - 1, AnalysisData.rotateCount_current] = 0;
                        fram.EFEMSts.H2[Slot - 1, AnalysisData.rotateCount_current] = 0;
                        InsertReportTable(DateTime.Now, FoupID, Slot, sram.PitchAngleTotal, 0, 0, 0, 0, "Fail", "PT");
                    }*/

                    Flag.SensorAnalysisFlag = true;
                    Flag.SaveChartFlag = false; //主畫面分析
                    AutoTrim2ndStep = Trim2ndStep.Savelog;
                    break;

                case Trim2ndStep.Savelog:
                    if (!Flag.SensorAnalysisFlag && Flag.SaveChartFlag)
                    {
                        InsertLog.SavetoDB(72);
                        AnalysisData.WaferMeasure[AnalysisData.rotateCount_current] = true;
                        SpinWait.SpinUntil(() => false, 100);
                        AutoTrim2ndStep = Trim2ndStep.JudgeRotateCount;
                    }
                    break;
                case Trim2ndStep.JudgeRotateCount:
                    Console.WriteLine("CurrentTimes : " + AnalysisData.rotateCount_current);

                    if (AnalysisData.rotateCount_current < sram.Recipe.Rotate_Count)
                    {
                        AnalysisData.rotateCount_current += 1;
                        if (AnalysisData.rotateCount_current == sram.Recipe.Rotate_Count)
                        {
                            //Common.motion.PitchAngle(SSCNET.AxisNo.DD, SSCNET.Dir.Postive, sram.PitchAngle);
                            //Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive, 360 - sram.PitchAngleTotal - Homepos.Precitec.Angle - fram.m_WaferAlignAngle); // 最到最後一個角度後 補到360度
                            if (MachineType == 0 || MachineType == 2)
                            {
                                Common.motion.PosMove(Mo.AxisNo.DD, fram.m_WaferBackToFoupAngle);
                            }
                            else
                            {
                                Common.motion.PosMove(Mo.AxisNo.DD, 0); // 回0度
                            }
                            Common.PTForm.PointMove(9);//PLC退出
                            SpinWait.SpinUntil(() => false, 500);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD) && Common.PTForm.GetPointMoveFinish(9), 20000);
                            InsertLog.SavetoDB(72);
                            AutoTrim2ndStep = Trim2ndStep.UpdateSecs;
                            /*
                            double max_h1 = 0, max_h2 = 0;
                            for (int j = 0; j < 8; j++)
                            {
                                if (fram.EFEMSts.H1[Slot-1, j] > max_h1)
                                {
                                    max_h1 = fram.EFEMSts.H1[Slot-1, j];
                                }
                            }
                            for (int j = 0; j < 8; j++)
                            {
                                if (fram.EFEMSts.H2[Slot-1, j] > max_h2)
                                {
                                    max_h2 = fram.EFEMSts.H2[Slot-1, j];
                                }
                            }

                            InsertReportTable(DateTime.Now, FoupID, Slot, 0, max_h1, 0, max_h2, 0, "OK", "MAX_PT");
                            break;*/
                        }
                        else
                        {
                            AutoTrim2ndStep = Trim2ndStep.DDRotate;
                        }
                    }

                    break;

                case Trim2ndStep.UpdateSecs:

                    #region 算完8個角度後計算最大值

                    string Slot_InfoMax = "";
                    double max_H1 = 0, max_H2 = 0, max_W1 = 0, max_W2 = 0;

                    for (int i = 0; i < 25; i++)
                    {
                        max_H1 = 0; max_H2 = 0; max_W1 = 0; max_W2 = 0;
                        if (EFEM.IsInit)
                        {
                            if (sram.Recipe.Slot[i] == 1)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    if (fram.EFEMSts.H1[i, j] > max_H1)
                                    {
                                        max_H1 = fram.EFEMSts.H1[i, j];
                                    }
                                }
                                for (int j = 0; j < 8; j++)
                                {
                                    if (fram.EFEMSts.H2[i, j] > max_H2)
                                    {
                                        max_H2 = fram.EFEMSts.H2[i, j];
                                    }
                                }
                                for (int j = 0; j < 8; j++)
                                {
                                    if (fram.EFEMSts.W1[i, j] > max_W1)
                                    {
                                        max_W1 = fram.EFEMSts.W1[i, j];
                                    }
                                }
                                for (int j = 0; j < 8; j++)
                                {
                                    if (fram.EFEMSts.W2[i, j] > max_W2)
                                    {
                                        max_W2 = fram.EFEMSts.W2[i, j];
                                    }
                                }

                                if (sram.Recipe.Type == 0)
                                {
                                    Slot_InfoMax += "0,";
                                    Slot_InfoMax += max_W1 + ",";
                                    Slot_InfoMax += "0,";
                                    Slot_InfoMax += "0";
                                }
                                else if (sram.Recipe.Type == 1) // 1step
                                {
                                    Slot_InfoMax += max_H1 + ",";
                                    Slot_InfoMax += max_W1 + ",";
                                    Slot_InfoMax += "0,";
                                    Slot_InfoMax += "0";
                                }
                                else if (sram.Recipe.Type == 2 || sram.Recipe.Type == 3) // 2 step    //
                                {
                                    Slot_InfoMax += max_H1 + ",";
                                    Slot_InfoMax += max_W1 + ",";
                                    Slot_InfoMax += max_H2 + ",";
                                    Slot_InfoMax += max_W2 + "";
                                }
                            }
                            else
                            {
                                Slot_InfoMax += "0,";
                                Slot_InfoMax += "0,";
                                Slot_InfoMax += "0,";
                                Slot_InfoMax += "0";
                            }
                        }

                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Slot1_Max + i, Slot_InfoMax, out err);
                        Slot_InfoMax = "";
                    }

                    #endregion 算完8個角度後計算最大值

                    string Slot_Info = "";
                    for (int i = 0; i < 25; i++)
                    {
                        Slot_Info += sram.Recipe.Type + ","; // 0: blue tape, 1: 1step, 2: 2step
                        Slot_Info += sram.Recipe.Rotate_Count + ","; // 4 or 8 個點
                        for (int j = 0; j < sram.Recipe.Rotate_Count; j++) // 每一片的 4/8 個位置
                        {
                            // 加入 Angle
                            if (sram.Recipe.Rotate_Count == 4)
                            {
                                if (EFEM.IsInit)
                                {
                                    if (Common.EFEM.LoadPort_Run.Slot[i] == 1)
                                    {
                                        Slot_Info += sram.Recipe.Angle[j * 2] + ",";
                                    }
                                    else
                                    {
                                        Slot_Info += "0,";
                                    }
                                }
                                else
                                {
                                    Slot_Info += sram.Recipe.Angle[j * 2] + ",";
                                }
                            }
                            else
                            {
                                if (EFEM.IsInit)
                                {
                                    if (Common.EFEM.LoadPort_Run.Slot[i] == 1)
                                    {
                                        Slot_Info += sram.Recipe.Angle[j] + ",";
                                    }
                                    else
                                    {
                                        Slot_Info += "0,";
                                    }
                                }
                                else
                                {
                                    Slot_Info += sram.Recipe.Angle[j * 2] + ",";
                                }
                            }

                            // 每個type上傳的不一樣
                            if (sram.Recipe.Type == 0) // Blue Tape
                            {
                                Slot_Info += 0 + ",";
                                Slot_Info += fram.EFEMSts.W1[i, j] + ",";                           // blueTape只有W1
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                            }
                            else if (sram.Recipe.Type == 1) // 1 step
                            {
                                Slot_Info += fram.EFEMSts.H1[i, j] + ",";                           // H1
                                Slot_Info += fram.EFEMSts.W1[i, j] + ",";                           // H2
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                            }
                            else if (sram.Recipe.Type == 2 || sram.Recipe.Type == 3) // 2 step    //
                            {
                                Slot_Info += fram.EFEMSts.H1[i, j] + ",";                           // h1 = H2
                                Slot_Info += fram.EFEMSts.W1[i, j] + ",";   // w1 = W1+W2
                                Slot_Info += fram.EFEMSts.H2[i, j] + ",";   // h2 = H1+H2
                                Slot_Info += fram.EFEMSts.W2[i, j] + ",";                           // w2 = W1
                            }
                            else
                            {
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                            }
                        }
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Slot1_Info + i, Slot_Info, out err);
                        Slot_Info = "";
                    }
                    if (EFEM.IsInit)
                    {
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort_Run.FoupID, out err);
                        if (Common.EFEM.LoadPort_Run.pn == LoadPort.Pn.P1)
                        {
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                        }
                        else if (Common.EFEM.LoadPort_Run.pn == LoadPort.Pn.P2)
                        {
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                        }
                    }

                    double[] data1 = new double[8];
                    double[] data2 = new double[8];
                    double[] data3 = new double[8];
                    double[] data4 = new double[8];
                    for (int i = 0; i < 8; i++)
                    {
                        data1[i] = fram.EFEMSts.H1[Slot - 1, i];
                        data2[i] = fram.EFEMSts.W1[Slot - 1, i];
                        data3[i] = fram.EFEMSts.H2[Slot - 1, i];
                        data4[i] = fram.EFEMSts.W2[Slot - 1, i];
                    }

                    ParamFile.SaveRawdata_Csv4(data1, data2, data3, data4, max_H1, max_W1, max_H2, max_W2, FoupID, FoupID + "_" + Slot + "_" + "PT_resultdata", DateTime.Now, AnalysisData.rotateCount_current);

                    AutoTrim2ndStep = Trim2ndStep.Finish;
                    Flag.isPT = false;
                    break;

                case Trim2ndStep.Finish:

                    Measure_TrimType2nd_Done = true;
                    AnalysisData.rotateCount_current = 0;
                    AutoTrim2ndStep = Trim2ndStep.PTHome;

                    Trimtest = 0;
                    Trimtest_break = 0;
                    break;

                default:
                    AutoTrim2ndStep = Trim2ndStep.PTHome;
                    break;
            }
        }

        private static void Measurement_TrimType_HTW()
        {
            switch (AutoHTWStep)
            {
                case HTWStep.HTWHome:
                    sram.PitchAngleTotal = 0;
                    sram.PTRetry = 0;
                    AnalysisData.rotateCount_current = 0;
                    Flag.isPT = false;
                    Flag.isLJforHTW = false;
                    for (int i = 0; i < sram.Recipe.Rotate_Count; i++)
                    {
                        AnalysisData.WaferMeasure[i] = false;
                    }
                    if (MachineType == 0) // AP6 不用回home
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.DD))
                        {
                            //Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive,Homepos.Precitec.Angle);
                            Common.motion.PosMove(Mo.AxisNo.DD, Homepos.AP6.Precitec.Angle + fram.m_WaferAlignAngle);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 5000);
                            AutoHTWStep = HTWStep.DDRotate;
                        }
                    }
                    else if (MachineType == 1) // N2 要移動到LJ的home點
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y))
                        {
                            //這裡要下XY軸的絕對位置，走去LJ的原點
                            Common.motion.PosMove(Mo.AxisNo.X, Homepos.N2.LJ8020.X);
                            Common.motion.PosMove(Mo.AxisNo.Y, Homepos.N2.LJ8020.Y);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.X), 10000); // 等待10秒
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.Y), 10000); // 等待10秒
                            if (Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y))
                            {
                                AutoHTWStep = HTWStep.DDRotate;
                            }
                        }
                    }
                    else if(MachineType == 2) //AP6II
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3) && Common.motion.MotionDone(Mo.AxisNo.AP6II_X))
                        {
                            Common.motion.PosMove(Mo.AxisNo.DD, Homepos.AP6II.HTW.Angle + fram.m_WaferAlignAngle);
                            Common.motion.PosMove(Mo.AxisNo.AP6II_Z3, Homepos.AP6II.HTW.AP6II_Z3);
                            //Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_P1_X + 5000);
                            //Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_P1_X + 13000);
                            Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_P1_X - 3000);
                            bool rtn = true;
                            rtn &= SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 10000);
                            rtn &= SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3), 10000);
                            rtn &= SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_X), 30000);

                            //20250818
                            if (fram.m_simulateRun == 0)
                            {
                                if(isDarkReference == false)
                                {                                   
                                    Common.PTForm.DarkReference();
                                }          
                            }

                            if (rtn)
                                AutoHTWStep = HTWStep.DDRotate;
                            else
                            {
                                Flag.AutoidleFlag = false;
                                Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_HTW_ReturnHome_TimeoutError, true, out err);
                                InsertLog.SavetoDB(TrimGap_EqpID.EQP_HTW_ReturnHome_TimeoutError, "HTW ReturnHome Timeout");
                            }
                        }
                    }
                    break;

                case HTWStep.DDRotate:
                    if (fram.S_MotionRotate == "False") // Bypass motion
                    { 
                        AutoHTWStep = HTWStep.HTWAutoFocus;
                    }
                    else
                    {
                        if (MachineType == 0) // AP6
                        {
                            if (!Common.motion.MotionDone(Mo.AxisNo.DD))
                            {
                                break;  // 動作未完成就跳走等等再看一次
                            }
                        }
                        else if (MachineType == 1) // N26
                        {
                            if (!Common.motion.MotionDone(Mo.AxisNo.DD) || !Common.motion.MotionDone(Mo.AxisNo.X) || !Common.motion.MotionDone(Mo.AxisNo.Y))
                            {
                                break; // 動作未完成就跳走等等再看一次
                            }
                        }
                        else if (MachineType == 2) // AP6II
                        {
                            if (!Common.motion.MotionDone(Mo.AxisNo.DD) || !Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3) || !Common.motion.MotionDone(Mo.AxisNo.AP6II_X))
                            {
                                break; // 動作未完成就跳走等等再看一次
                            }
                        }
                        if (AnalysisData.rotateCount_current == 0)
                        {
                            //sram.PitchAngle = 1;
                            sram.PitchAngle = sram.Recipe.Angle[0];
                        }
                        else if (AnalysisData.rotateCount_current == 1)
                        {
                            //sram.PitchAngle = fram.Analysis.PitchAngle - 1;
                            if (sram.Recipe.Rotate_Count == 4)
                            {
                                sram.PitchAngle = sram.Recipe.Angle[2] - sram.Recipe.Angle[0];
                            }
                            else
                            {
                                sram.PitchAngle = sram.Recipe.Angle[1] - sram.Recipe.Angle[0];
                            }
                        }
                        else
                        {
                            //sram.PitchAngle = fram.Analysis.PitchAngle;
                            if (sram.Recipe.Rotate_Count == 4)
                            {
                                sram.PitchAngle = sram.Recipe.Angle[AnalysisData.rotateCount_current * 2]
                                                - sram.Recipe.Angle[AnalysisData.rotateCount_current * 2 - 2];
                            }
                            else
                            {
                                sram.PitchAngle = sram.Recipe.Angle[AnalysisData.rotateCount_current]
                                                - sram.Recipe.Angle[AnalysisData.rotateCount_current - 1];
                            }
                        }
                        sram.PitchAngleTotal += sram.PitchAngle;
                        InsertLog.SavetoDB(67, sram.PitchAngleTotal.ToString());
                        Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive, sram.PitchAngle);
                        //Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_P1_X + 5000);

                        //20250828
                        if (fram.m_simulateRun == 0)
                        {
                            if (isDarkReference == true)
                            {
                                Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_P1_X - 3000);
                                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_X), 30000);
                                Common.PTForm.DarkReference();
                            }
                        }

                        Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_AutoFocus_X);  //改AutoFocus位置
                        Common.motion.PosMove(Mo.AxisNo.AP6II_Z3, fram.Position.HTW_P1_Z);
                        sram.AutoFocus_Retry_Count = 0;
                        bool rtn;
                        rtn = SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 5000);
                        if (!rtn)
                        {
                            Console.WriteLine("DD Rotate Time Out");
                            InsertLog.SavetoDB(67, "DD Rotate Time Out");
                            Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_HTW_DD_TimeoutError, true, out err);
                            //AutoHTWStep = HTWStep.Finish;
                            Flag.AutoidleFlag = false;
                            break;
                        }
                        rtn = SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3), 4000);
                        if (!rtn)
                        {
                            Console.WriteLine("Axis Z3 GoTo P1 Time Out");
                            InsertLog.SavetoDB(67, "Axis Z3 GoTo P1 Time Out");
                            Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_HTW_Z_TimeoutError, true, out err);
                            //AutoHTWStep = HTWStep.Finish;
                            Flag.AutoidleFlag = false;
                            break;
                        }
                        //rtn = SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_X), 10000);
                        rtn = SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_X), 13000);
                        if (!rtn)
                        {
                            Console.WriteLine("Axis X GoTo P1 Time Out");
                            InsertLog.SavetoDB(67, "Axis X GoTo P1 Time Out");
                            Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_HTW_X_TimeoutError, true, out err);
                            //AutoHTWStep = HTWStep.Finish;
                            Flag.AutoidleFlag = false;
                            break;
                        }
                        if (fram.m_simulateRun == 0)
                            AutoHTWStep = HTWStep.HTWAutoFocus;
                        else
                            AutoHTWStep = HTWStep.Download;
                        fram.PT_PLC_AutoRunStage_RetryCount = 0;
                        ReportData.HTW_Focus_Z = "";
                    }
                    break;
                case HTWStep.HTWAutoFocus:
                    
                    if(is_HTW == 1)
                    {
                        fram.HTW_Autofocus_DetectIndex = 80;//175 -> 180 -> 190  ->120 -> 80(改到左邊去，大約高度降了70um)
                        fram.HTW_Autofocus_Index = 0;
                        fram.HTW_Autofocus_2ndPos_Shift = 200;  //第一次找不到焦點時，要偏移多少距離再量一次

                        sram.Focus_Offset = 0;
                        short[] spectrumData;
                        short[] spectrumDataOld = new short[1];
                        sram.SpectrumMaxValue = new short[fram.Position.HTW_P1_FocusRange];
                        sram.SpectrumMaxValueBias = new int[fram.Position.HTW_P1_FocusRange];
                        short max_allpos = 0;
                        Dictionary<int, short> max3 = new Dictionary<int, short>();
                        bool RightToLeft = false;
                        double LeftSum = 0;
                        double RightSum = 0;
                        List<double> list_Left = new List<double>();
                        List<double> list_Right = new List<double>();

                        //20251222
                        List<short[]> list_spectrumData = new List<short[]>();

                        for (int i = 0; i < fram.Position.HTW_P1_FocusRange; i++)
                        {
                            Common.motion.PosMove(Mo.AxisNo.AP6II_Z3, fram.Position.HTW_P1_Z + i * 10);//下往上
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3), 8000);
                            spectrumData = Common.PTForm.GetSpectrum(2); //Get FT
                            //20251222
                            list_spectrumData.Add(spectrumData);

                            LeftSum = 0;
                            RightSum = 0;
                            if (spectrumData.Length >= fram.HTW_Autofocus_DetectIndex + 20)
                            {
                                short max = 0;
                                int pos = fram.HTW_Autofocus_DetectIndex - 5;
                                for (int j = fram.HTW_Autofocus_DetectIndex - 5; j <= fram.HTW_Autofocus_DetectIndex + 5; j++)
                                {
                                    list_Left = new List<double>();
                                    list_Right = new List<double>();

                                    if (spectrumData[j] > max)
                                    {
                                        max = spectrumData[j];
                                        pos = j;
                                    }
                                    //if (i != 0)  //不是第一筆資料，可以比對舊資料
                                    //{
                                    //    LeftSum += Math.Abs((double)(spectrumDataOld[j - 10] - spectrumData[j]));
                                    //    RightSum += Math.Abs((double)(spectrumDataOld[j + 10] - spectrumData[j]));
                                    //}
                                    if (i != 0)
                                    {
                                        for (int k = 1; k <= 10; k++)
                                        {
                                            list_Left.Add((double)(spectrumDataOld[j - k]));
                                            list_Right.Add((double)(spectrumDataOld[j + k]));
                                        }
                                    }
                                }
                                sram.SpectrumMaxValueBias[i] = pos - fram.HTW_Autofocus_DetectIndex;
                                sram.SpectrumMaxValue[i] = max;

                                //20250828
                                //LeftSum = Math.Abs((double)(spectrumDataOld[pos - 10] - spectrumData[pos]));
                                //RightSum = Math.Abs((double)(spectrumDataOld[pos + 10] - spectrumData[pos]));
                                LeftSum = list_Left.Max();
                                RightSum = list_Right.Max();

                                if ((max > max_allpos) && (LeftSum < RightSum))  //有最大值要更新 && 右邊過來
                                {
                                    //一組方式(原方法)
                                    max_allpos = max;
                                    fram.HTW_Autofocus_Index = i;

                                    //三組方式20240722
                                    /*
                                    max3.Add(i, max);  
                                    if(max3.Count>3)   //新的值做為第四組加進來一起排序
                                    {
                                        max3 = max3.OrderBy(o => o.Value).ToDictionary(o => o.Key, o => o.Value);  //Value小到大排序
                                        max3.Remove(max3.Keys.ElementAt(0));  //刪除第一個(最小的)
                                        max_allpos = max3.Values.ElementAt(0);  //剩3個裡面最小的當作max_allpos，之後有比他大的值就放進來重排
                                        fram.HTW_Autofocus_Index = max3.Keys.Min();   //剩3個裡面 最靠左的那個頻譜peak
                                        if (max3[fram.HTW_Autofocus_Index] < max3.Values.Max() * 0.7)   //最靠左的那個頻譜peak 強度不能太低(至少要有最強peak的7成吧)
                                            fram.HTW_Autofocus_Index = max3.Keys.ElementAt(2);          //太弱了就不要用，改回最強的peak
                                    }
                                    else
                                    {
                                        max3 = max3.OrderBy(o => o.Value).ToDictionary(o => o.Key, o => o.Value);  //Value小到大排序
                                        fram.HTW_Autofocus_Index = max3.Keys.ElementAt(max3.Count-1);   //最大的，max_allpos維持0以便繼續收滿4組資料
                                    }*/
                                }
                                spectrumDataOld = new short[spectrumData.Length];
                                spectrumData.CopyTo(spectrumDataOld, 0);
                            }
                            else
                                sram.SpectrumMaxValue[i] = 0;
                        }
                        //對不到焦第二次機會
                        if(max_allpos <= 3000)
                        {
                            if (sram.AutoFocus_Retry_Count == 0)  //第一次retry 往正方向走
                            {
                                Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_AutoFocus_X + fram.HTW_Autofocus_2ndPos_Shift);  //改AutoFocus位置
                                Common.motion.PosMove(Mo.AxisNo.AP6II_Z3, fram.Position.HTW_P1_Z);

                                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_X), 10000);
                                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3), 4000);

                                sram.AutoFocus_Retry_Count = 1;
                                break;
                            }
                            else if(sram.AutoFocus_Retry_Count == 1)  //第二次retry 往負方向走
                            {
                                Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_AutoFocus_X - fram.HTW_Autofocus_2ndPos_Shift);  //改AutoFocus位置
                                Common.motion.PosMove(Mo.AxisNo.AP6II_Z3, fram.Position.HTW_P1_Z);

                                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_X), 10000);
                                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3), 4000);

                                sram.AutoFocus_Retry_Count = 2;
                                break;
                            }
                            else
                            {
                                sram.AutoFocus_Retry_Count = 3;
                                if (fram.HTW_Autofocus_Index_Last_Used != 0)  //三次Focus都失敗，用上一次成功的對焦位置替代
                                    fram.HTW_Autofocus_Index = fram.HTW_Autofocus_Index_Last_Used;
                            }
                        }
                        else
                        {
                            fram.HTW_Autofocus_Index_Last_Used = fram.HTW_Autofocus_Index;  //保存成功的Focus位置
                        }

                        sram.Focus_Offset = 1 * fram.HTW_Autofocus_Index * 10 + sram.SpectrumMaxValueBias[fram.HTW_Autofocus_Index];

                        Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_P1_X);//走到起點
                        Common.motion.PosMove(Mo.AxisNo.AP6II_Z3, fram.Position.HTW_P1_Z + sram.Focus_Offset);
                        string focusretry = "中";
                        if(sram.AutoFocus_Retry_Count == 1)
                            focusretry = "右";
                        else if(sram.AutoFocus_Retry_Count == 2)
                            focusretry = "左";
                        else
                            focusretry = "上";
                        ReportData.HTW_Focus_Z = (fram.Position.HTW_P1_Z + sram.Focus_Offset).ToString() + "(" + fram.HTW_Autofocus_Index + "," + max_allpos + "," + focusretry + ")";
                        sram.PTRetry = 0;
                        sram.AutoFocus_Retry_Count = 0;

                        //20251222
                        ParamFile.SaveRawdata_AutoFocus(list_spectrumData, FoupID, "AutoFocus" + sram.PitchAngleTotal, sram.saveDataTime);

                        AutoHTWStep = HTWStep.Measurement;
                    }
                    else
                    {
                        //==================== 20260312 換SENSOR做AUTOFOCUS ====================

                        Common.PTForm.SetTriggerMode(0);  //改成量測模式
                        Common.PTForm.StartTrigger2(1, 48020, 48020, 0);  //先觸發一次讓sensor有訊號了再來讀值，避免一開始讀到的值是亂的
                        SpinWait.SpinUntil(() => false, 1000);
                        Common.PTForm.getData(out AnalysisData.rawData);
                        double value = AnalysisData.rawData[0];

                        

                        if(value > 0)  //有訊號了，代表有對到焦了
                        {
                            sram.Focus_Offset = 0;
                            sram.AutoFocus_Retry_Count = 0;
                            ReportData.HTW_Focus_Z = value.ToString() + "(NewSensor," + fram.Fixed_value + ")";
                            Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_P1_X);//走到起點
                            Common.motion.PosMove(Mo.AxisNo.AP6II_Z3, value + fram.Fixed_value);
                            AutoHTWStep = HTWStep.Measurement;
                        }
                        else
                        {
                            if (sram.AutoFocus_Retry_Count < 3)  //最多重試3次
                            {
                                sram.AutoFocus_Retry_Count++;
                                Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_P1_X + fram.HTW_Autofocus_2ndPos_Shift);//改AutoFocus位置
                                Common.motion.PosMove(Mo.AxisNo.AP6II_Z3, fram.Position.HTW_P1_Z);
                                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_X), 10000);
                                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3), 4000);
                                AutoHTWStep = HTWStep.HTWAutoFocus;  //再試一次對焦
                            }
                            else
                            {
                                //3次仍無訊號 → 停機報警
                                sram.AutoFocus_Retry_Count = 0;
                                InsertLog.SavetoDB(TrimGap_EqpID.EQP_HTW_Z_TimeoutError, "NewSensor AutoFocus Failed after 3 retries");
                                Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_HTW_Z_TimeoutError, true, out err);
                                Flag.AutoidleFlag = false;
                            }
                        }

                        Common.PTForm.SetTriggerMode(2);  //恢復觸發模式

                    }
                    
                    break;
                case HTWStep.Measurement:
                    if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3) && Common.motion.MotionDone(Mo.AxisNo.AP6II_X))
                    {
                        double posnow = Common.motion.Get_FBAngle(Mo.AxisNo.DD) - fram.m_WaferAlignAngle - Homepos.AP6II.HTW.Angle;
                        InsertLog.SavetoDB(69, "(HTW) Target:" + sram.PitchAngleTotal.ToString() + ",Real:" + posnow.ToString());
                        Console.WriteLine(DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString() + " Measure Start");

                        Common.PTForm.StartTrigger(4802, 48020, 0, -10);
                        SpinWait.SpinUntil(() => false, 1000);

                        Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_P1_X + 5000, 0.0008);//0.001  
                        if (Flag.isPT_2 && Flag.NewSensorFlag)
                            AutoHTWStep = HTWStep.MoveToStart_New_Sensor;
                        else
                            AutoHTWStep = HTWStep.Download;
                       

                        Console.WriteLine(DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString() + " HTW Measure 1 End");
                    }
                    else if (fram.PT_PLC_AutoRunStage_RetryCount < 200)
                    {
                        SpinWait.SpinUntil(() => false, 50);
                        fram.PT_PLC_AutoRunStage_RetryCount++;
                    }
                    else
                    {
                        fram.PT_PLC_AutoRunStage_RetryCount = 0;
                        Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_HTW_X_TimeoutError, true, out err);
                        InsertLog.SavetoDB(TrimGap_EqpID.EQP_HTW_X_TimeoutError, "PT PLC Move To Point 1 Timeout");
                        Flag.AutoidleFlag = false;
                    }

                    break;

                case HTWStep.MoveToStart_New_Sensor:
                    if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3) && Common.motion.MotionDone(Mo.AxisNo.AP6II_X))
                    {
                        double posnow = Common.motion.Get_FBAngle(Mo.AxisNo.DD) - fram.m_WaferAlignAngle - Homepos.AP6II.HTW.Angle;
                        InsertLog.SavetoDB(69, "(HTW) Target:" + sram.PitchAngleTotal.ToString() + ",Real:" + posnow.ToString());
                        Console.WriteLine(DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString() + " Measure New Sensor Start");

                        Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Analysis.PT_2_X);//0.001  
                        //Common.motion.PosMove(Mo.AxisNo.AP6II_Z3, fram.Analysis.PT_2_Z_Offset + sram.Focus_Offset);

                        //===============================================
                        //Common.PTForm.StartTrigger(4802, 124040, 76020, -10);
                        //Common.motion.PosMove(Mo.AxisNo.AP6II_X, 8189, 0.0008);//0.001  
                        //Common.motion.PosMove(Mo.AxisNo.AP6II_X, 5398, 0.0008);//0.001  

                        //Common.PTForm.StartTrigger2(4802, 48020, 0, -10);
                        //===============================================

                        //Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_P1_X + 5000, 0.0008);//0.001  

                        AutoHTWStep = HTWStep.Measurement_New_Sensor;
                    }

                    break;

                case HTWStep.Measurement_New_Sensor:
                    if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3) && Common.motion.MotionDone(Mo.AxisNo.AP6II_X))
                    {
                        double posnow = Common.motion.Get_FBAngle(Mo.AxisNo.DD) - fram.m_WaferAlignAngle - Homepos.AP6II.HTW.Angle;
                        InsertLog.SavetoDB(69, "(HTW) Target:" + sram.PitchAngleTotal.ToString() + ",Real:" + posnow.ToString());
                        Console.WriteLine(DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString() + " Measure New Sensor Start");

                        Common.PTForm.StartTrigger2(4802, 48020, -30, -10);
                        SpinWait.SpinUntil(() => false, 1000);

                        Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Analysis.PT_2_X + 5000, 0.0008);//0.001  

                        AutoHTWStep = HTWStep.Download;
                    }

                    break;

                case HTWStep.Download:
                    if (Common.motion.MotionDone(Mo.AxisNo.AP6II_X))
                    {
                        if (fram.m_simulateRun == 0)
                        {
                            SpinWait.SpinUntil(() => false, 1500);
                            bool rtn = Common.PTForm.isFinish();
                            bool rtn2 = Common.PTForm.isFinish2();

                            if (rtn || sram.PTRetry >= 3)
                            {
                                sram.saveDataTime = DateTime.Now;
                                if (sram.PTRetry >= 3)
                                {
                                    AnalysisData.rawData = new double[4802];
                                    AnalysisData.rawData2 = new double[4802];
                                    AnalysisData.rawData3 = new double[4802];
                                    AnalysisData.rawData4 = new double[4802];
                                    AnalysisData.rawData5 = new double[4802];

                                    //======================================== 20250418
                                    AnalysisData.rawData_2 = new double[4802];
                                    AnalysisData.rawData2_2 = new double[4802];
                                    AnalysisData.rawData3_2 = new double[4802];
                                    AnalysisData.rawData4_2 = new double[4802];
                                    AnalysisData.rawData5_2 = new double[4802];
                                }
                                else
                                {
                                    Common.PTForm.getData(out AnalysisData.rawData);
                                    Common.PTForm.getData2(out AnalysisData.rawData2);
                                    Common.PTForm.getData3(out AnalysisData.rawData3);
                                    Common.PTForm.getData4(out AnalysisData.rawData4);
                                    Common.PTForm.getData5(out AnalysisData.rawData5);

                                    //========================================= 20250418

                                    if(Flag.isPT_2 && Flag.NewSensorFlag)
                                    {
                                        Common.PTForm.getData_PT(out AnalysisData.rawData_2);
                                        Common.PTForm.getData2_PT(out AnalysisData.rawData2_2);
                                        Common.PTForm.getData3_PT(out AnalysisData.rawData3_2);
                                    }
                                    

                                }

                                for (int i = 0; i < AnalysisData.rawData.Length; i++)
                                {
                                    if (Double.NaN.Equals((AnalysisData.rawData[i])))
                                    {
                                        AnalysisData.rawData[i] = 0;
                                    }
                                    if (Double.NaN.Equals((AnalysisData.rawData2[i])))
                                    {
                                        AnalysisData.rawData2[i] = 0;
                                    }
                                    if (Double.NaN.Equals((AnalysisData.rawData3[i])))
                                    {
                                        AnalysisData.rawData3[i] = 0;
                                    }
                                    if (Double.NaN.Equals((AnalysisData.rawData4[i])))
                                    {
                                        AnalysisData.rawData4[i] = 0;
                                    }
                                    if (Double.NaN.Equals((AnalysisData.rawData5[i])))
                                    {
                                        AnalysisData.rawData5[i] = 0;
                                    }
                                }

                                    

                                ParamFile.SaveRawdata_Csv3(AnalysisData.rawData, AnalysisData.rawData2, AnalysisData.rawData3, FoupID, FoupID + "_" + Slot + "_" + sram.PitchAngleTotal + "_HTW_RAW_" + rtn.ToString(), sram.saveDataTime);
                                ParamFile.SaveRawdata_Csv5(AnalysisData.rawData4, AnalysisData.rawData5, FoupID, FoupID + "_" + Slot + "_" + sram.PitchAngleTotal + "_HTW_RAW_Intensity" + rtn.ToString(), sram.saveDataTime);

                                //========================================= 20250418
                                if (Flag.isPT_2 && Flag.NewSensorFlag)
                                {
                                    ParamFile.SaveRawdata_Csv3(AnalysisData.rawData_2, AnalysisData.rawData2_2, AnalysisData.rawData3_2, FoupID, FoupID + "_" + Slot + "_" + sram.PitchAngleTotal + "_NEW_PT_" + rtn2.ToString(), sram.saveDataTime);


                                    Console.WriteLine("NEW_PT Download End " + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                                    InsertLog.SavetoDB(70, FoupID + "_" + Slot + "_" + sram.PitchAngleTotal + "_NEW_PT");
                                }
                                //========================================= 20250418

                                //Console.WriteLine("PT Download End " + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                                //InsertLog.SavetoDB(70, FoupID + "_" + Slot + "_" + sram.PitchAngleTotal + "_PT");
                                AutoHTWStep = HTWStep.Measurement2;
                                //sram.PTRetry = 0;
                                Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_P1_X);//走到起點
                                Common.motion.PosMove(Mo.AxisNo.AP6II_Z3, fram.Position.HTW_P2_Z);//走到起點
                            }
                            else
                            {
                                Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_P1_X);//走到起點
                                sram.PTRetry++;
                                //AutoHTWStep = HTWStep.Measurement;
                                AutoHTWStep = HTWStep.Measurement;
                            }
                        }
                        else
                        {
                            StreamReader read = new StreamReader("HTW.csv");
                            StreamReader read2 = new StreamReader("HTW2.csv");

                            string ReadAll,ReadAll2;
                            string[] ReadArray1, ReadArray2;

                            ReadAll = read.ReadToEnd(); // 一次讀全部
                            ReadAll2 = read2.ReadToEnd();

                            ReadArray1 = Regex.Split(ReadAll, "\r\n", RegexOptions.IgnoreCase);
                            ReadArray2 = Regex.Split(ReadAll2, "\r\n", RegexOptions.IgnoreCase);

                            double[] array2 = new double[ReadArray1.Length - 1];
                            double[] array3 = new double[ReadArray1.Length - 1];
                            double[] array4 = new double[ReadArray1.Length - 1];
                            double[] array5 = new double[ReadArray1.Length - 1];
                            double[] array6 = new double[ReadArray1.Length - 1];
                            double[] arrayb = new double[ReadArray1.Length - 1];
                            for (int i = 0; i < ReadArray1.Length - 1; i++)
                            {
                                string[] tmpArray = Regex.Split(ReadArray1[i], ",", RegexOptions.IgnoreCase);
                                array2[i] = Convert.ToSingle(tmpArray[0]);
                                array3[i] = Convert.ToSingle(tmpArray[1]);
                                array4[i] = Convert.ToSingle(tmpArray[2]);
                                array5[i] = Convert.ToSingle(tmpArray[3]);
                                array6[i] = Convert.ToSingle(tmpArray[4]);
                                arrayb[i] = Convert.ToSingle(tmpArray[5]);
                                Console.WriteLine(i.ToString());
                            }

                            AnalysisData.rawData = array2;
                            AnalysisData.rawData2 = array3;
                            AnalysisData.rawData3 = array4;
                            AnalysisData.rawData4 = array5;
                            AnalysisData.rawData5 = array6;
                            AnalysisData.rawData_base = arrayb;

                            //=====20250925 翻模擬資料=====
                            /*double m = array2.Max();
                            for (int i = 0; i < ReadArray1.Length - 1; i++)
                            {
                                AnalysisData.rawData[i] = m - AnalysisData.rawData[i];
                                AnalysisData.rawData2[i] = m - AnalysisData.rawData2[i];
                                AnalysisData.rawData3[i] = m - AnalysisData.rawData3[i];
                            }*/
                            //=============================

                            //============== 20250611
                            double[] array2_1 = new double[ReadArray2.Length - 1];
                            double[] array3_1 = new double[ReadArray2.Length - 1];
                            double[] array4_1 = new double[ReadArray2.Length - 1];
                            //double[] array5_1 = new double[ReadArray2.Length - 1];
                            //ouble[] array6_1 = new double[ReadArray2.Length - 1];
                            //double[] arrayb_1 = new double[ReadArray2.Length - 1];

                            for (int i = 0; i < ReadArray2.Length - 1; i++)
                            {
                                string[] tmpArray = Regex.Split(ReadArray2[i], ",", RegexOptions.IgnoreCase);
                                array2_1[i] = Convert.ToSingle(tmpArray[0]);
                                array3_1[i] = Convert.ToSingle(tmpArray[1]);
                                array4_1[i] = Convert.ToSingle(tmpArray[2]);
                                //array5_1[i] = Convert.ToSingle(tmpArray[3]);
                                //array6_1[i] = Convert.ToSingle(tmpArray[4]);
                                //arrayb_1[i] = Convert.ToSingle(tmpArray[5]);
                                //Console.WriteLine(i.ToString());
                            }
                            AnalysisData.rawData_2 = array2_1;
                            AnalysisData.rawData2_2 = array3_1;
                            AnalysisData.rawData3_2 = array4_1;
                            //AnalysisData.rawData4_2 = array5_1;
                            //AnalysisData.rawData5_2 = array6_1;
                            //AnalysisData.rawData_base_2 = arrayb_1;

                            read.Close();
                            read2.Close();
                            AutoHTWStep = HTWStep.Analysis;
                        }

                    }
                    break;
                case HTWStep.Measurement2:
                    if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3) && Common.motion.MotionDone(Mo.AxisNo.AP6II_X))
                    {
                        //sram.PTRetry = 0;
                        double posnow = Common.motion.Get_FBAngle(Mo.AxisNo.DD) - fram.m_WaferAlignAngle - Homepos.AP6II.HTW.Angle;
                        InsertLog.SavetoDB(69, "(HTW) Target:" + sram.PitchAngleTotal.ToString() + ",Real:" + posnow.ToString());
                        Console.WriteLine(DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString() + " Measure 2 Start");

                        Common.PTForm.StartTrigger(4802, 48020, 0, -10);
                        SpinWait.SpinUntil(() => false, 1000);
                        Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_P1_X + 5000, 0.0008);

                        AutoHTWStep = HTWStep.Download2;
                        Console.WriteLine(DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString() + " HTW Measure 2 End");
                    }
                    else if (fram.PT_PLC_AutoRunStage_RetryCount < 200)
                    {
                        SpinWait.SpinUntil(() => false, 50);
                        fram.PT_PLC_AutoRunStage_RetryCount++;
                    }
                    else
                    {
                        fram.PT_PLC_AutoRunStage_RetryCount = 0;
                        Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_HTW_X_TimeoutError, true, out err);
                        InsertLog.SavetoDB(TrimGap_EqpID.EQP_HTW_X_TimeoutError, "PT PLC Move To Point 2 Timeout");
                        Flag.AutoidleFlag = false;
                    }

                    break;

                case HTWStep.Download2:
                    if (Common.motion.MotionDone(Mo.AxisNo.AP6II_X))
                    {
                        if (fram.m_simulateRun == 0)
                        {
                            SpinWait.SpinUntil(() => false, 1500);
                            bool rtn = Common.PTForm.isFinish();

                            if (rtn || sram.PTRetry >= 3)
                            {
                                //sram.saveDataTime = DateTime.Now;
                                if(sram.PTRetry >= 3)
                                    AnalysisData.rawData_base = new double[4802];
                                else
                                    Common.PTForm.getData(out AnalysisData.rawData_base);
                                ParamFile.SaveRawdata_Csv(AnalysisData.rawData_base, FoupID, FoupID + "_" + Slot + "_" + sram.PitchAngleTotal + "_HTW_RAW_BASE_" + rtn.ToString(), sram.saveDataTime);
                                
                                Console.WriteLine("HTW Download End " + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                                InsertLog.SavetoDB(70, FoupID + "_" + Slot + "_" + sram.PitchAngleTotal + "_HTW");
                                AutoHTWStep = HTWStep.Analysis;
                                sram.PTRetry = 0;
                                Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_P1_X);//走到起點
                                Common.motion.PosMove(Mo.AxisNo.AP6II_Z3, fram.Position.HTW_P1_Z);
                            }
                            else
                            {
                                Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_P1_X);//走到起點
                                sram.PTRetry++;
                                AutoHTWStep = HTWStep.Measurement2;
                            }
                        }
                    }
                    break;
                case HTWStep.Analysis:
                    InsertLog.SavetoDB(71);

                    Flag.SensorAnalysisFlag = true;
                    Flag.SaveChartFlag = false; //主畫面分析
                    AutoHTWStep = HTWStep.Savelog;
                    break;

                case HTWStep.Savelog:
                    if (!Flag.SensorAnalysisFlag && Flag.SaveChartFlag && Common.motion.MotionDone(Mo.AxisNo.AP6II_X) && Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3))
                    {
                        InsertLog.SavetoDB(72);
                        AnalysisData.WaferMeasure[AnalysisData.rotateCount_current] = true;
                        SpinWait.SpinUntil(() => false, 100);
                        AutoHTWStep = HTWStep.JudgeRotateCount;
                    }
                    break;
                case HTWStep.JudgeRotateCount:
                    Console.WriteLine("CurrentTimes : " + AnalysisData.rotateCount_current);

                    if (AnalysisData.rotateCount_current < sram.Recipe.Rotate_Count)
                    {
                        AnalysisData.rotateCount_current += 1;
                        if (AnalysisData.rotateCount_current == sram.Recipe.Rotate_Count)
                        {
                            if (MachineType == 0 || MachineType == 2)
                            {
                                Common.motion.PosMove(Mo.AxisNo.DD, fram.m_WaferBackToFoupAngle);
                            }
                            else
                            {
                                Common.motion.PosMove(Mo.AxisNo.DD, 0); // 回0度
                            }
                            Common.motion.PosMove(Mo.AxisNo.AP6II_X, Homepos.AP6II.HTW.AP6II_X);
                            SpinWait.SpinUntil(() => false, 500);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD) && Common.motion.MotionDone(Mo.AxisNo.AP6II_X), 20000);
                            InsertLog.SavetoDB(72);
                            AutoHTWStep = HTWStep.UpdateSecs;
                        }
                        else
                        {
                            if (isDarkReference == true)
                            {
                                Common.motion.PosMove(Mo.AxisNo.AP6II_Z3, Homepos.AP6II.HTW.AP6II_Z3);
                            }
                            AutoHTWStep = HTWStep.DDRotate;
                        }
                    }

                    break;

                case HTWStep.UpdateSecs:

                    #region 算完8個角度後計算最大值

                    string Slot_InfoMax = "";
                    double max_H1_now = 0, max_W1_now = 0, max_H2_now = 0, max_W2_now = 0;
                    double max_H1 = 0, max_H2 = 0, max_W1 = 0, max_W2 = 0;

                    for (int i = 0; i < 25; i++)
                    {
                        max_H1 = 0; max_H2 = 0; max_W1 = 0; max_W2 = 0;
                        if (EFEM.IsInit || fram.EFEMSts.Skip == 1)
                        {
                            if (sram.Recipe.Slot[i] == 1)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    if (fram.EFEMSts.H1[i, j] > max_H1)
                                    {
                                        max_H1 = fram.EFEMSts.H1[i, j];
                                    }

                                    if (fram.EFEMSts.H2[i, j] > max_H2)
                                    {
                                        max_H2 = fram.EFEMSts.H2[i, j];
                                    }

                                    if (fram.EFEMSts.W1[i, j] > max_W1)
                                    {
                                        max_W1 = fram.EFEMSts.W1[i, j];
                                    }

                                    if (fram.EFEMSts.W2[i, j] > max_W2)
                                    {
                                        max_W2 = fram.EFEMSts.W2[i, j];
                                    }
                                }
                                //for (int j = 0; j < 8; j++)
                                //{
                                //    if (fram.EFEMSts.H2[i, j] > max_H2)
                                //    {
                                //        max_H2 = fram.EFEMSts.H2[i, j];
                                //    }
                                //}
                                //for (int j = 0; j < 8; j++)
                                //{
                                //    if (fram.EFEMSts.W1[i, j] > max_W1)
                                //    {
                                //        max_W1 = fram.EFEMSts.W1[i, j];
                                //    }
                                //}
                                //for (int j = 0; j < 8; j++)
                                //{
                                //    if (fram.EFEMSts.W2[i, j] > max_W2)
                                //    {
                                //        max_W2 = fram.EFEMSts.W2[i, j];
                                //    }
                                //}

                                if (sram.Recipe.Type == 0)
                                {
                                    Slot_InfoMax += "0,";
                                    Slot_InfoMax += max_W1 + ",";
                                    Slot_InfoMax += "0,";
                                    Slot_InfoMax += "0";
                                }
                                else if (sram.Recipe.Type == 1) // 1step
                                {
                                    Slot_InfoMax += max_H1 + ",";
                                    Slot_InfoMax += max_W1 + ",";
                                    Slot_InfoMax += "0,";
                                    Slot_InfoMax += "0";
                                }
                                else if (sram.Recipe.Type == 2 || sram.Recipe.Type == 3 || sram.Recipe.Type == 4) // 2 step    //
                                {
                                    Slot_InfoMax += max_H1 + ",";
                                    Slot_InfoMax += max_W1 + ",";
                                    Slot_InfoMax += max_H2 + ",";
                                    Slot_InfoMax += max_W2 + "";
                                }
                            }
                            else
                            {
                                Slot_InfoMax += "0,";
                                Slot_InfoMax += "0,";
                                Slot_InfoMax += "0,";
                                Slot_InfoMax += "0";
                            }
                        }

                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Slot1_Max + i, Slot_InfoMax, out err);
                        Slot_InfoMax = "";

                        if( i + 1 == Slot)
                        {
                            if (sram.Recipe.Type == 0)
                            {
                                max_W1_now = max_W1;
                            }
                            else if (sram.Recipe.Type == 1 || sram.Recipe.Type == 6 || sram.Recipe.Type == 7) // 1step
                            {
                                max_H1_now = max_H1;
                                max_W1_now = max_W1;
                            }
                            else if (sram.Recipe.Type == 2 || sram.Recipe.Type == 3 || sram.Recipe.Type == 4) // 2 step    //
                            {
                                max_H1_now = max_H1;
                                max_W1_now = max_W1; 
                                max_H2_now = max_H2;
                                max_W2_now = max_W2;
                            }
                        }
                    }

                    #endregion 算完8個角度後計算最大值

                    string Slot_Info = "";
                    for (int i = 0; i < 25; i++)
                    {
                        Slot_Info += "3,";//sram.Recipe.Type + ","; // 0: blue tape, 1: 1step, 2: 2step  暫時改20240611
                        Slot_Info += sram.Recipe.Rotate_Count + ","; // 4 or 8 個點
                        for (int j = 0; j < sram.Recipe.Rotate_Count; j++) // 每一片的 4/8 個位置
                        {
                            // 加入 Angle
                            if (sram.Recipe.Rotate_Count == 4)
                            {
                                if (EFEM.IsInit || fram.EFEMSts.Skip == 1)
                                {
                                    if (Common.EFEM.LoadPort_Run.Slot[i] == 1)
                                    {
                                        Slot_Info += sram.Recipe.Angle[j * 2] + ",";
                                    }
                                    else
                                    {
                                        Slot_Info += "0,";
                                    }
                                }
                                else
                                {
                                    Slot_Info += sram.Recipe.Angle[j * 2] + ",";
                                }
                            }
                            else
                            {
                                if (EFEM.IsInit || fram.EFEMSts.Skip == 1)
                                {
                                    if (Common.EFEM.LoadPort_Run.Slot[i] == 1)
                                    {
                                        Slot_Info += sram.Recipe.Angle[j] + ",";
                                    }
                                    else
                                    {
                                        Slot_Info += "0,";
                                    }
                                }
                                else
                                {
                                    Slot_Info += sram.Recipe.Angle[j] + ",";
                                }
                            }

                            // 每個type上傳的不一樣
                            if (sram.Recipe.Type == 0) // Blue Tape
                            {
                                Slot_Info += 0 + ",";
                                Slot_Info += fram.EFEMSts.W1[i, j] + ",";                           // blueTape只有W1
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                            }
                            else if (sram.Recipe.Type == 1) // 1 step
                            {
                                Slot_Info += fram.EFEMSts.H1[i, j] + ",";                           // H1
                                Slot_Info += fram.EFEMSts.W1[i, j] + ",";                           // H2
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                            }
                            else if (sram.Recipe.Type == 2 || sram.Recipe.Type == 3 || sram.Recipe.Type == 4) // 2 step    //
                            {
                                Slot_Info += fram.EFEMSts.H1[i, j] + ",";                           // h1 = H2
                                Slot_Info += fram.EFEMSts.W1[i, j] + ",";   // w1 = W1+W2
                                Slot_Info += fram.EFEMSts.H2[i, j] + ",";   // h2 = H1+H2
                                Slot_Info += fram.EFEMSts.W2[i, j] + ",";                           // w2 = W1

                            }
                            else
                            {
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                            }
                        }
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Slot1_Info + i, Slot_Info, out err);
                        Slot_Info = "";
                    }
                    if (EFEM.IsInit)//要模擬的話才要強制進 || true)
                    {
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort_Run.FoupID, out err);
                        if (Common.EFEM.LoadPort_Run.pn == LoadPort.Pn.P1)
                        {
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                        }
                        else if (Common.EFEM.LoadPort_Run.pn == LoadPort.Pn.P2)
                        {
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                        }
                        if (fram.m_SecsgemType == 1)
                        {
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort_Run.FoupID, out err);
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Recipe_Type, "3", out err);
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.LotID, Common.EFEM.LoadPort_Run.LotID[Slot - 1], out err);
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.SubstrateID, Common.EFEM.LoadPort_Run.SubstrateID[Slot - 1], out err);
                            string[] angle = new string[sram.Recipe.Rotate_Count];
                            double[] h1 = new double[sram.Recipe.Rotate_Count];
                            double[] w1 = new double[sram.Recipe.Rotate_Count];
                            double[] h2 = new double[sram.Recipe.Rotate_Count];
                            double[] w2 = new double[sram.Recipe.Rotate_Count];

                            for (int i = 0; i< sram.Recipe.Rotate_Count; i++)
                            {
                                if (sram.Recipe.Rotate_Count == 4)
                                    angle[i] = sram.Recipe.Angle[i * 2].ToString();
                                else
                                    angle[i] = sram.Recipe.Angle[i].ToString();

                                h1[i] = fram.EFEMSts.H1[Slot - 1, i];
                                w1[i] = fram.EFEMSts.W1[Slot - 1, i];
                                h2[i] = fram.EFEMSts.H2[Slot - 1, i];
                                w2[i] = fram.EFEMSts.W2[Slot - 1, i];
                            }

                            Common.SecsgemForm.UpdateMeasurementData(angle, h1, w1, h2, w2);
                            Common.SecsgemForm.UpdateMeasurementMax(max_H1_now, max_W1_now, max_H2_now, max_W2_now);
                            Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MeasureEnd, out err);
                        }                                                   
                    }

                    double[] data1 = new double[8];
                    double[] data2 = new double[8];
                    double[] data3 = new double[8];
                    double[] data4 = new double[8];
                    for (int i = 0; i < 8; i++)
                    {
                        data1[i] = fram.EFEMSts.H1[Slot - 1, i];
                        data2[i] = fram.EFEMSts.W1[Slot - 1, i];
                        data3[i] = fram.EFEMSts.H2[Slot - 1, i];
                        data4[i] = fram.EFEMSts.W2[Slot - 1, i];
                    }

                    ParamFile.SaveRawdata_Csv4(data1, data2, data3, data4, max_H1, max_W1, max_H2, max_W2, FoupID, FoupID + "_" + Slot + "_" + "HTW_resultdata", DateTime.Now, AnalysisData.rotateCount_current);

                    Measure_HTW_Done = true;
                    AutoHTWStep = HTWStep.Finish;
                    break;

                case HTWStep.Finish:

                    AnalysisData.rotateCount_current = 0;
                    AutoHTWStep = HTWStep.HTWHome;

                    Trimtest = 0;
                    Trimtest_break = 0;
                    break;

                default:
                    AutoHTWStep = HTWStep.HTWHome;
                    break;
            }
        }

        private static void Measurement_TTV_SF3()
        {
            switch (AutoTTVStep)
            {
                case TTVStep.TTVStart:
                    //fram.Recipe.MotionPatternName = "D:\\FTGM1\\ParameterDirectory\\TTVPath\\Default.tvr";
                    TTVScanPattern ttv = new TTVScanPattern(sram.Recipe.MotionPatternPath, sram.Recipe.MotionPatternName);
                    TTVData.lsRotate = ttv.GetAnglesFromRecipe(sram.Recipe.MotionPatternPath + "\\" + sram.Recipe.MotionPatternName + ".tvr");
                    TTVData.lsShift = ttv.GetPointsFromRecipe(sram.Recipe.MotionPatternPath + "\\" + sram.Recipe.MotionPatternName + ".tvr");
                    if (TTVData.lsRotate.Count == 0)
                    {
                        ReportData.TTVData = new double[TTVData.lsShift.Count];
                    }
                    else if (TTVData.lsRotate.Count > 0 && TTVData.lsShift.Count > 0)
                    {
                        ReportData.TTVData = new double[TTVData.lsRotate.Count * TTVData.lsShift.Count];
                    }

                    if (TTVData.lsRotate.Count == 0 || TTVData.lsShift.Count == 0)
                    {
                        AutoTTVStep = TTVStep.Finish;
                        Measure_TTV_Done = true;
                        //Error Recipe
                    }
                    else
                    {
                        AutoTTVStep = TTVStep.TTVHome;  //AutoTTVStep = TTVStep.TTVHome;  未測試考慮安全先不做
                        Common.SF3.Set_Recipe(sram.Recipe.SF3_ID);    // 給Recipe ID
                        //Common.SF3.Action_CmdLoadRecipeID(Convert.ToInt32(fram.Recipe.SF3_ID));
                        Common.SF3.Action_CmdMeasDark();
                        Common.SF3.Action_CmdReady();
                        Common.SF3.Action_CmdCtrlLight(Otsuka.Common.CmdCtrlLight.Light);
                        Common.SF3.Action_CmdMeasStart();
                    }
                    break;

                case TTVStep.TTVHome:
                    sram.PitchAngleTotal = 0;
                    if (fram.m_WaferStageType == 0) // AP6 不用回home
                    {
                        AutoTTVStep = TTVStep.DDRotate;
                    }
                    else if (fram.m_WaferStageType == 1) // N2 要移動到LJ的home點
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y))
                        {
                            //這裡要下XY軸的絕對位置，走去SF3的原點
                            Common.motion.PosMove(Mo.AxisNo.X, Homepos.N2.SF3.X);
                            Common.motion.PosMove(Mo.AxisNo.Y, Homepos.N2.SF3.Y);
                            AutoTTVStep = TTVStep.TTVHomeDone;
                        }
                    }
                    break;

                case TTVStep.TTVHomeDone:
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y), 10000); // 等待10秒
                    if (Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y))
                    {
                        AutoTTVStep = TTVStep.DDRotate;
                    }
                    else
                    {
                        Measure_TTV_Done = true;
                        AutoTTVStep = TTVStep.Finish;
                        //Error
                    }
                    break;

                case TTVStep.DDRotate:
                    if (fram.S_MotionRotate == "False") // Bypass motion
                    {
                        AutoTTVStep = TTVStep.Measurement;
                    }
                    else
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y))
                        {
                            InsertLog.SavetoDB(67);
                            Common.motion.PosMove(Mo.AxisNo.DD, TTVData.lsRotate[TTVData.rotateCount_current] + fram.m_WaferAlignAngle);

                            AutoTTVStep = TTVStep.DDRotateDone;
                        }
                    }

                    break;

                case TTVStep.DDRotateDone:
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 5000);

                    if (Common.motion.MotionDone(Mo.AxisNo.DD))
                    {
                        AutoTTVStep = TTVStep.ShiftPosition;
                    }
                    else
                    {
                        Measure_TTV_Done = true;
                        AutoTTVStep = TTVStep.Finish;
                        InsertLog.SavetoDB(67, "DD Rotate Time Out");
                        Console.WriteLine("DD Rotate Time Out");
                    }
                    break;

                case TTVStep.ShiftPosition:
                    if (fram.S_MotionRotate == "False") // Bypass motion
                    {
                        AutoTTVStep = TTVStep.Measurement;
                    }
                    else
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y))
                        {
                            InsertLog.SavetoDB(68, TTVData.shiftCount_current.ToString());
                            Common.motion.PosMove(Mo.AxisNo.X, Homepos.N2.SF3.X + TTVData.lsShift[TTVData.shiftCount_current].X);
                            Common.motion.PosMove(Mo.AxisNo.Y, Homepos.N2.SF3.Y + TTVData.lsShift[TTVData.shiftCount_current].Y);
                            AutoTTVStep = TTVStep.ShiftPositionDone;
                        }
                    }
                    break;

                case TTVStep.ShiftPositionDone:
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y), 10000); // 等待10秒
                                                                                                                                     // SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.Y), 10000); // 等待10秒

                    if (Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y))
                    {
                        InsertLog.SavetoDB(67);
                        AutoTTVStep = TTVStep.Measurement;
                    }
                    else
                    {
                        Measure_TTV_Done = true;
                        Common.SF3.Action_CmdMeasStop();
                        AutoTTVStep = TTVStep.Finish;
                        //Error
                    }
                    break;

                case TTVStep.Measurement:

                    //int count;
                    //count = Otsuka.SF3.GetResult.Thickness.Count;

                    try
                    {
                        Common.SF3.Action_CmdGetResult();
                        if (Otsuka.SF3.GetResult.Thickness.Count > 0)
                        {
                            InsertLog.SavetoDB(69);
                            ReportData.TTVData[TTVData.rotateCount_current * TTVData.lsShift.Count + TTVData.shiftCount_current] = Otsuka.SF3.GetResult.Thickness[0];
                            AutoTTVStep = TTVStep.JudgeShiftCount;
                        }
                    }
                    catch (Exception ee)
                    {
                        Flag.AlarmFlag = true;
                        Console.WriteLine(ee.Message);
                    }

                    break;

                case TTVStep.JudgeShiftCount:
                    if (TTVData.shiftCount_current < TTVData.lsShift.Count - 1)
                    {
                        TTVData.shiftCount_current++;
                        AutoTTVStep = TTVStep.ShiftPosition;
                    }
                    else
                    {
                        TTVData.shiftCount_current = 0;
                        AutoTTVStep = TTVStep.JudgeRotateCount;
                    }
                    break;

                case TTVStep.JudgeRotateCount:
                    //結算一次這個角度的資料
                    if (TTVData.rotateCount_current < TTVData.lsRotate.Count - 1)
                    {
                        TTVData.rotateCount_current++;
                        AutoTTVStep = TTVStep.DDRotate;
                    }
                    else
                    {
                        if (MachineType == 0 || MachineType == 2)
                        {
                            Common.motion.PosMove(Mo.AxisNo.DD, fram.m_WaferBackToFoupAngle);
                        }
                        else
                        {
                            Common.motion.PosMove(Mo.AxisNo.DD, 0); // 回0度
                        }
                        TTVData.rotateCount_current = 0;
                        AutoTTVStep = TTVStep.Analysis;
                    }
                    break;

                case TTVStep.Analysis:
                    InsertLog.SavetoDB(71);
                    ReportData.TTVMax = Math.Round(ReportData.TTVData.Max(), 2);
                    ReportData.TTVMin = ReportData.TTVMax;
                    ReportData.TTVMean = 0;
                    int count = 0;
                    double sum = 0;
                    for (int i = 0; i < ReportData.TTVData.Length; i++)
                    {
                        if (ReportData.TTVData[i] < ReportData.TTVMin && ReportData.TTVData[i] != double.NaN && ReportData.TTVData[i] != 0)
                        {
                            ReportData.TTVMin = ReportData.TTVData[i];
                            sum += ReportData.TTVData[i];
                            count++;
                        }
                    }
                    ReportData.TTVMin = Math.Round(ReportData.TTVMin, 2);
                    ReportData.TTVMean = Math.Round(sum / count, 2);
                    //ReportData.TTVMean = Math.Round(ReportData.TTVData.Average(), 2);

                    ReportData.TTVDataValue = Math.Round(ReportData.TTVMax - ReportData.TTVMin, 2);

                    InsertReportTableTTV(DateTime.Now, FoupID, Slot, TTVData.rotateCount_current, ReportData.TTVDataValue, ReportData.TTVMax, ReportData.TTVMin, ReportData.TTVMean, "", "");
                    AutoTTVStep = TTVStep.Savelog;
                    break;

                case TTVStep.Savelog:
                    InsertLog.SavetoDB(72);
                    Measure_TTV_Done = true;            //改true之後AutoRunStageStep會跳到下一步驟
                    sram.saveDataTime = DateTime.Now;
                    ParamFile.SaveRawdata_Csv(ReportData.TTVData, FoupID, FoupID + "_" + Slot + "_" + sram.Recipe.MotionPatternName, sram.saveDataTime);
                    Common.SF3.Action_CmdMeasStop();
                    AutoTTVStep = TTVStep.Finish;
                    break;

                case TTVStep.Finish:
                    if (Measure_TTV_Done == false)        //在Finish待命的時候，如果有把Measure_TTV_Done改false會觸發一次TTV檢測
                        AutoTTVStep = TTVStep.TTVStart;
                    break;

                default:
                    AutoTTVStep = TTVStep.Finish;
                    break;
            }
        }

        private static void Measurement_BlueTape_CCD()
        {
            switch (AutoBlueTapeStep)
            {
                case BluetapeStep.BlueTapeStart:
                    sram.PitchAngleTotal = 0;
                    AnalysisData.rotateCount_current = 0;
                    for (int i = 0; i < sram.Recipe.Rotate_Count; i++)
                    {
                        AnalysisData.WaferMeasure[i] = false;
                    }
                    AutoBlueTapeStep = BluetapeStep.CCDHome;
                    break;

                case BluetapeStep.CCDHome:
                    if (MachineType == 0 || MachineType == 2)    // AP6 BT起點
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.DD))
                        {
                            //Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive, Homepos.BlueTape.Angle);
                            if(MachineType == 0)
                                Common.motion.PosMove(Mo.AxisNo.DD, Homepos.AP6.BlueTape.Angle + fram.m_WaferAlignAngle);
                            else
                                Common.motion.PosMove(Mo.AxisNo.DD, Homepos.AP6II.BlueTape.Angle + fram.m_WaferAlignAngle);

                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 5000);
                            if (Common.motion.MotionDone(Mo.AxisNo.DD))
                            {
                                AutoBlueTapeStep = BluetapeStep.DDRotate;
                            }
                            if (Tapetest < 2)
                            {
                                InsertLog.SavetoDB(67, "Tape:359");
                                Tapetest++;
                            }
                        }
                    }
                    else if (MachineType == 1) // N2 要移動到BT的home點
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y) && Common.motion.MotionDone(Mo.AxisNo.DD))
                        {
                            //這裡要下XY軸的絕對位置，走去LJ的原點
                            Common.motion.PosMove(Mo.AxisNo.X, Homepos.N2.BlueTape.X);
                            Common.motion.PosMove(Mo.AxisNo.Y, Homepos.N2.BlueTape.Y);
                            Common.motion.PosMove(Mo.AxisNo.DD, Homepos.N2.BlueTape.Angle);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y) && Common.motion.MotionDone(Mo.AxisNo.DD), 10000); // 等待10秒
                            if (Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y) && Common.motion.MotionDone(Mo.AxisNo.DD))
                            {
                                AutoBlueTapeStep = BluetapeStep.DDRotate;
                            }
                        }
                    }
                    break;

                case BluetapeStep.DDRotate:
                    if (fram.S_MotionRotate == "False") // Bypass motion
                    {
                        AutoBlueTapeStep = BluetapeStep.Measurement;
                    }
                    else
                    {
                        if (MachineType == 0 || MachineType == 2) // AP6
                        {
                            if (!Common.motion.MotionDone(Mo.AxisNo.DD))
                            {
                                break;  // 動作未完成就跳走等等再看一次
                            }
                        }
                        else if (MachineType == 1) // N26
                        {
                            if (!Common.motion.MotionDone(Mo.AxisNo.DD) && !Common.motion.MotionDone(Mo.AxisNo.X) && !Common.motion.MotionDone(Mo.AxisNo.Y))
                            {
                                break; // 動作未完成就跳走等等再看一次
                            }
                        }
                        if (AnalysisData.rotateCount_current == 0)
                        {
                            sram.PitchAngle = sram.Recipe.Angle[0];
                        }
                        else if (AnalysisData.rotateCount_current == 1)
                        {
                            //sram.PitchAngle = fram.Analysis.PitchAngle - 1;
                            if (sram.Recipe.Rotate_Count == 4)
                            {
                                sram.PitchAngle = sram.Recipe.Angle[2] - sram.Recipe.Angle[0];
                            }
                            else
                            {
                                sram.PitchAngle = sram.Recipe.Angle[1] - sram.Recipe.Angle[0];
                            }
                        }
                        else
                        {
                            //sram.PitchAngle = fram.Analysis.PitchAngle;
                            if (sram.Recipe.Rotate_Count == 4)
                            {
                                sram.PitchAngle = sram.Recipe.Angle[AnalysisData.rotateCount_current * 2]
                                                - sram.Recipe.Angle[AnalysisData.rotateCount_current * 2 - 2];
                            }
                            else
                            {
                                sram.PitchAngle = sram.Recipe.Angle[AnalysisData.rotateCount_current]
                                                - sram.Recipe.Angle[AnalysisData.rotateCount_current - 1];
                            }
                        }
                        sram.PitchAngleTotal += sram.PitchAngle;
                        InsertLog.SavetoDB(67, sram.PitchAngleTotal.ToString());
                        //20240104 2:自我檢測 不進入
                        if (fram.EFEMSts.Skip == 0 || fram.EFEMSts.Skip == 1)
                        {
                            Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive, sram.PitchAngle);
                        }
                        SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 2000);
                        //if (!Common.motion.MotionDone(Mo.AxisNo.DD))
                        //{
                        //    Measure_BlueTape_Done = true;
                        //   AutoBlueTapeStep = BluetapeStep.Finish;
                        //   Console.WriteLine("DD Rotate Time Out");
                        //}
                        //else
                            AutoBlueTapeStep = BluetapeStep.Measurement;
                    }
                    break;

                case BluetapeStep.Measurement:
                    if (Common.motion.MotionDone(Mo.AxisNo.DD))
                    {
                        double posnow = 1*(Common.motion.Get_FBAngle(Mo.AxisNo.DD) - fram.m_WaferAlignAngle - Homepos.AP6II.BlueTape.Angle);
                        InsertLog.SavetoDB(69, "Target:" + sram.PitchAngleTotal.ToString() + ",Real:" + posnow.ToString());
                        Console.WriteLine(DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString() + " Bluetape Measure Start");
                        //Common.Rs232LightingController.SetIntensity(1, 24); // 開燈           C:1, F:8, 1*16+8 = 24
                        //Common.EthernetLightingController.SetIntensity(0, 25); // 開燈 Ch0    25
                        //Common.EthernetLightingController.SetIntensity(1, 0); // 開燈 Ch1    0
                        Common.camera.GetImage();
                        //Common.Rs232LightingController.SetIntensity(1, 0); // 關燈
                        //Common.EthernetLightingController.SetIntensity(0, 0); // 關燈 Ch0
                        //Common.EthernetLightingController.SetIntensity(1, 0); // 關燈 Ch1

                        AutoBlueTapeStep = BluetapeStep.Download;
                        Console.WriteLine(DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString() + " Bluetape Measure End");
                    }
                    else if(Tapetest>100)
                    {
                           Measure_BlueTape_Done = true;
                           AutoBlueTapeStep = BluetapeStep.Finish;
                           InsertLog.SavetoDB(67, "DD Rotate Time Out");
                    }
                    else
                    {
                        Tapetest += 10;
                        SpinWait.SpinUntil(() => false, 300);
                    }
                    break;

                case BluetapeStep.Download:
                    InsertLog.SavetoDB(70);
                    byte[] data = Common.camera.image; //1D gray level array
                    int w = Common.camera.Width;    // image Width
                    int h = Common.camera.Height;   // image Height
                    AutoBlueTapeStep = BluetapeStep.Analysis;
                    break;

                case BluetapeStep.Analysis:
                    InsertLog.SavetoDB(71);
                    Flag.SensorAnalysisFlag = true;
                    Flag.SaveChartFlag = false; //主畫面分析
                    AutoBlueTapeStep = BluetapeStep.Savelog;
                    break;

                case BluetapeStep.Savelog:
                    if (!Flag.SensorAnalysisFlag && Flag.SaveChartFlag)
                    {
                        InsertLog.SavetoDB(72);
                        //AnalysisData.WaferMeasure[AnalysisData.rotateCount_current] = true;
                        //SpinWait.SpinUntil(() => false, 100);
                        AutoBlueTapeStep = BluetapeStep.JudgeRotateCount;
                    }

                    break;

                case BluetapeStep.JudgeRotateCount:
                    Console.WriteLine("CurrentTimes : " + AnalysisData.rotateCount_current);

                    if (AnalysisData.rotateCount_current < sram.Recipe.Rotate_Count)
                    {
                        AnalysisData.rotateCount_current += 1;
                        if (AnalysisData.rotateCount_current == sram.Recipe.Rotate_Count)
                        {
                            //Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive, 360 - sram.PitchAngleTotal); // 最到最後一個角度後 補到360度
                            if (MachineType == 0)
                            {
                                Common.motion.PosMove(Mo.AxisNo.DD, fram.m_WaferBackToFoupAngle);
                            }
                            else
                            {
                                Common.motion.PosMove(Mo.AxisNo.DD, 0); // 回0度
                            }
                            SpinWait.SpinUntil(() => false, 500);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 20000);

                            AutoBlueTapeStep = BluetapeStep.UpdateSecs;
                            break;
                        }
                        else
                        {
                            AutoBlueTapeStep = BluetapeStep.DDRotate;
                            Tapetest = 0;
                        }
                    }

                    break;

                case BluetapeStep.UpdateSecs:

                    #region 算完8個角度後計算最大值

                    string Slot_InfoMax = "";
                    double max_W1 = 0;

                    for (int i = 0; i < 25; i++)
                    {
                        max_W1 = 0;
                        if (EFEM.IsInit)
                        {
                            if (sram.Recipe.Slot[i] == 1)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    if (fram.EFEMSts.H1[i, j] > max_W1)
                                    {
                                        max_W1 = fram.EFEMSts.H1[i, j];
                                    }
                                }

                                if (sram.Recipe.Type == 0)
                                {
                                    Slot_InfoMax += "0,";
                                    Slot_InfoMax += "0,";//max_W1 + ",";   20230628
                                    Slot_InfoMax += "0,";
                                    Slot_InfoMax += "0";
                                }
                            }
                            else
                            {
                                Slot_InfoMax += "0,";
                                Slot_InfoMax += "0,";
                                Slot_InfoMax += "0,";
                                Slot_InfoMax += "0";
                            }
                        }
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Slot1_Max + i, Slot_InfoMax, out err);
                        Slot_InfoMax = "";
                    }

                    #endregion 算完8個角度後計算最大值

                    string Slot_Info = "";

                    for (int i = 0; i < 25; i++)
                    {
                        Slot_Info += sram.Recipe.Type + ","; // 0: blue tape, 1: 1step, 2: 2step
                        Slot_Info += sram.Recipe.Rotate_Count + ","; // 4 or 8 個點
                        for (int j = 0; j < sram.Recipe.Rotate_Count; j++) // 每一片的 4/8 個位置
                        {
                            // 加入 Angle
                            if (sram.Recipe.Rotate_Count == 4)
                            {
                                if (EFEM.IsInit)
                                {
                                    if (Common.EFEM.LoadPort_Run.Slot[i] == 1)
                                    {
                                        Slot_Info += sram.Recipe.Angle[j * 2] + ",";
                                    }
                                    else
                                    {
                                        Slot_Info += "0,";
                                    }
                                }
                                else
                                {
                                    Slot_Info += sram.Recipe.Angle[j * 2] + ",";
                                }
                            }
                            else
                            {
                                if (EFEM.IsInit)
                                {
                                    if (Common.EFEM.LoadPort_Run.Slot[i] == 1)
                                    {
                                        Slot_Info += sram.Recipe.Angle[j] + ",";
                                    }
                                    else
                                    {
                                        Slot_Info += "0,";
                                    }
                                }
                                else
                                {
                                    Slot_Info += sram.Recipe.Angle[j] + ",";
                                }
                            }

                            // 每個type上傳的不一樣
                            if (sram.Recipe.Type == 0) // Blue Tape
                            {
                                if (Double.NaN.Equals(fram.EFEMSts.W1[i, j]))
                                {
                                    fram.EFEMSts.W1[i, j] = 0;
                                }

                                Slot_Info += 0 + ",";
                                Slot_Info += fram.EFEMSts.W1[i, j] + ",";                           // blueTape只有W1
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                            }
                            else
                            {
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                            }
                        }
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Slot1_Info + i, Slot_Info, out err);
                        Slot_Info = "";
                    }
                    if (EFEM.IsInit)
                    {
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort_Run.FoupID, out err);
                        if (Common.EFEM.LoadPort_Run.pn == LoadPort.Pn.P1)
                        {
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                        }
                        else if (Common.EFEM.LoadPort_Run.pn == LoadPort.Pn.P2)
                        {
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                        }
                    }

                    double[] data1 = new double[8];
                    double[] data2 = new double[8];
                    double[] data3 = new double[8];
                    double[] data4 = new double[8];
                    for (int i = 0; i < 8; i++)
                    {
                        data1[i] = fram.EFEMSts.H1[Slot - 1, i];
                        data2[i] = fram.EFEMSts.W1[Slot - 1, i];
                        data3[i] = fram.EFEMSts.H2[Slot - 1, i];
                        data4[i] = fram.EFEMSts.W2[Slot - 1, i];
                    }

                    ParamFile.SaveRawdata_Csv4(data1, data2, data3, data4, 0, max_W1, 0, 0, FoupID, FoupID + "_" + Slot + "_" + "BlueTape_resultdata", DateTime.Now, AnalysisData.rotateCount_current);

                    Measure_BlueTape_Done = true;
                    AutoBlueTapeStep = BluetapeStep.Finish;
                    AnalysisData.rotateCount_current = 0;
                    break;

                case BluetapeStep.Finish:
                    if (Measure_BlueTape_Done == false)        //在Finish待命的時候，如果有把Measure_BlueTape_Done改false會觸發一次BT檢測
                        AutoBlueTapeStep = BluetapeStep.BlueTapeStart;
                    AnalysisData.rotateCount_current = 0;

                    Tapetest = 0;
                    break;

                default:
                    AutoBlueTapeStep = BluetapeStep.Finish;
                    break;
            }
        }

        private static void Measurement_Record_CCD()
        {
            switch (AutoRecordCCDStep)
            {
                case RecordCCDStep.RecordCCDStart:
                    sram.PitchAngleTotal = 0;
                    AnalysisData.rotateCount_current = 0;
                    for (int i = 0; i < sram.Recipe.Rotate_Count; i++)
                    {
                        //AnalysisData.WaferMeasure[i] = false;
                    }
                    AutoRecordCCDStep = RecordCCDStep.CCDHome;
                    break;

                case RecordCCDStep.CCDHome:
                    if (MachineType == 0)    // AP6 
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.DD))
                        {
                            //Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive, Homepos.BlueTape.Angle);
                            Common.motion.PosMove(Mo.AxisNo.DD, Homepos.AP6II.RecordCCD.Angle + fram.m_WaferAlignAngle);
                            bool rtn = true;
                            rtn &= SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 5000);
                            if (rtn)
                            {
                                AutoRecordCCDStep = RecordCCDStep.DDRotate;
                            }
                            else
                            {
                                AutoRecordCCDStep = RecordCCDStep.Finish;
                                Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_HTW_ReturnHome_TimeoutError, true, out err);
                                InsertLog.SavetoDB(TrimGap_EqpID.EQP_HTW_ReturnHome_TimeoutError, "HTW ReturnHome Timeout");
                            }
                        }
                    }
                    else if (MachineType == 2)    // AP6 II
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.motion.MotionDone(Mo.AxisNo.AP6II_Z2))
                        {
                            //Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive, Homepos.BlueTape.Angle);
                            Common.motion.PosMove(Mo.AxisNo.DD, Homepos.AP6II.RecordCCD.Angle + fram.m_WaferAlignAngle);
                            Common.motion.PosMove(Mo.AxisNo.AP6II_Z2, fram.Position.RecordCCD_Z);
                            bool rtn = true;
                            rtn &= SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 10000);
                            rtn &= SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_Z2), 10000);
                            if (rtn)
                            {
                                AutoRecordCCDStep = RecordCCDStep.DDRotate;
                            }
                            else
                            {
                                AutoRecordCCDStep = RecordCCDStep.Finish;
                                Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_HTW_ReturnHome_TimeoutError, true, out err);
                                InsertLog.SavetoDB(TrimGap_EqpID.EQP_HTW_ReturnHome_TimeoutError, "HTW ReturnHome Timeout");
                            }
                        }
                    }
                    else if (MachineType == 1) // N2 要移動到BT的home點
                    {
                        /*
                        if (Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y) && Common.motion.MotionDone(Mo.AxisNo.DD))
                        {
                            //這裡要下XY軸的絕對位置，走去LJ的原點
                            Common.motion.PosMove(Mo.AxisNo.X, Homepos.N2.RecordCCD.X);
                            Common.motion.PosMove(Mo.AxisNo.Y, Homepos.N2.RecordCCD.Y);
                            Common.motion.PosMove(Mo.AxisNo.DD, Homepos.N2.RecordCCD.Angle);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y) && Common.motion.MotionDone(Mo.AxisNo.DD), 10000); // 等待10秒
                            if (Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y) && Common.motion.MotionDone(Mo.AxisNo.DD))
                            {
                                AutoBlueTapeStep = BluetapeStep.DDRotate;
                            }
                        }*/
                        Measure_RecordCCD_Done = true;
                        AutoRecordCCDStep = RecordCCDStep.Finish;
                    }
                    break;

                case RecordCCDStep.DDRotate:
                    if (fram.S_MotionRotate == "False") // Bypass motion
                    {
                        AutoRecordCCDStep = RecordCCDStep.Measurement;
                    }
                    else
                    {
                        if (MachineType == 0 || MachineType == 2) // AP6
                        {
                            if (!Common.motion.MotionDone(Mo.AxisNo.DD))
                            {
                                break;  // 動作未完成就跳走等等再看一次
                            }
                        }


                        if (AnalysisData.rotateCount_current == 0)
                        {
                            sram.PitchAngle = sram.Recipe.Angle[0];
                            if(sram.Recipe.RecordCCDRule == 1)  //修改起始點
                                sram.PitchAngle = sram.Recipe.RecordCCD_Angle_Start;
                        }
                        else if (AnalysisData.rotateCount_current == 1)
                        {
                            if (sram.Recipe.Rotate_Count == 4)
                            {
                                sram.PitchAngle = sram.Recipe.Angle[2] - sram.Recipe.Angle[0];
                            }
                            else
                            {
                                sram.PitchAngle = sram.Recipe.Angle[1] - sram.Recipe.Angle[0];
                            }
                            if (sram.Recipe.RecordCCDRule == 1)  //修改Pitch
                                sram.PitchAngle = sram.Recipe.RecordCCD_Angle_Pitch;
                        }
                        else
                        {
                            if (sram.Recipe.Rotate_Count == 4)
                            {
                                sram.PitchAngle = sram.Recipe.Angle[AnalysisData.rotateCount_current * 2]
                                                - sram.Recipe.Angle[AnalysisData.rotateCount_current * 2 - 2];
                            }
                            else
                            {
                                sram.PitchAngle = sram.Recipe.Angle[AnalysisData.rotateCount_current]
                                                - sram.Recipe.Angle[AnalysisData.rotateCount_current - 1];
                            }
                            if (sram.Recipe.RecordCCDRule == 1)  //修改Pitch
                                sram.PitchAngle = sram.Recipe.RecordCCD_Angle_Pitch;
                        }
                        sram.PitchAngleTotal += sram.PitchAngle;
                        InsertLog.SavetoDB(67, sram.PitchAngleTotal.ToString());
                        Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive, sram.PitchAngle);
                        SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 2000);
                        AutoRecordCCDStep = RecordCCDStep.Measurement;
                    }
                    break;

                case RecordCCDStep.Measurement:
                    if (Common.motion.MotionDone(Mo.AxisNo.DD))
                    {
                        double posnow = 1 * (Common.motion.Get_FBAngle(Mo.AxisNo.DD) - fram.m_WaferAlignAngle - Homepos.AP6II.RecordCCD.Angle);
                        InsertLog.SavetoDB(69, "Target:" + sram.PitchAngleTotal.ToString() + ",Real:" + posnow.ToString());
                        Console.WriteLine(DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString() + " RecordCCD Measure Start");
                        //Common.Rs232LightingController.SetIntensity(1, 24); // 開燈           C:1, F:8, 1*16+8 = 24
                        //Common.EthernetLightingController.SetIntensity(0, 25); // 開燈 Ch0    25
                        //Common.EthernetLightingController.SetIntensity(1, 0); // 開燈 Ch1    0
                        Common.camera2.GetImage();
                        //Common.Rs232LightingController.SetIntensity(1, 0); // 關燈
                        //Common.EthernetLightingController.SetIntensity(0, 0); // 關燈 Ch0
                        //Common.EthernetLightingController.SetIntensity(1, 0); // 關燈 Ch1

                        AutoRecordCCDStep = RecordCCDStep.Download;
                        Console.WriteLine(DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString() + " RecordCCD Measure End");
                    }
                    break;

                case RecordCCDStep.Download:
                    InsertLog.SavetoDB(70);
                    byte[] data = Common.camera2.image; //1D gray level array
                    int w = Common.camera2.Width;    // image Width
                    int h = Common.camera2.Height;   // image Height
                    AutoRecordCCDStep = RecordCCDStep.Analysis;
                    break;

                case RecordCCDStep.Analysis:
                    InsertLog.SavetoDB(71);
                    Flag.SensorAnalysisFlag = true;
                    Flag.SaveChartFlag = false; //主畫面分析
                    AutoRecordCCDStep = RecordCCDStep.Savelog;
                    break;

                case RecordCCDStep.Savelog:
                    if (!Flag.SensorAnalysisFlag && Flag.SaveChartFlag)
                    {
                        InsertLog.SavetoDB(72);
                        AutoRecordCCDStep = RecordCCDStep.JudgeRotateCount;
                    }
                    break;

                case RecordCCDStep.JudgeRotateCount:
                    Console.WriteLine("CurrentTimes : " + AnalysisData.rotateCount_current);
                    if(sram.Recipe.RecordCCDRule == 0)
                    {
                        if (AnalysisData.rotateCount_current < sram.Recipe.Rotate_Count)
                        {
                            AnalysisData.rotateCount_current += 1;
                            if (AnalysisData.rotateCount_current == sram.Recipe.Rotate_Count)
                            {
                                //Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive, 360 - sram.PitchAngleTotal); // 最到最後一個角度後 補到360度
                                if (MachineType == 0 || MachineType == 2)
                                {
                                    Common.motion.PosMove(Mo.AxisNo.DD, fram.m_WaferBackToFoupAngle);
                                    if (MachineType == 2)
                                        Common.motion.PosMove(Mo.AxisNo.AP6II_Z2, Homepos.AP6II.RecordCCD.AP6II_Z2);
                                }
                                else
                                {
                                    Common.motion.PosMove(Mo.AxisNo.DD, 0); // 回0度
                                }
                                SpinWait.SpinUntil(() => false, 500);
                                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 20000);
                                if (MachineType == 2)
                                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_Z2), 20000);
                                AutoRecordCCDStep = RecordCCDStep.UpdateSecs;
                                break;
                            }
                            else
                            {
                                AutoRecordCCDStep = RecordCCDStep.DDRotate;
                                Tapetest = 0;
                            }
                        }
                    }
                    else if(sram.Recipe.RecordCCDRule == 1)
                    {
                        AnalysisData.rotateCount_current += 1;
                        if (sram.PitchAngleTotal + sram.Recipe.RecordCCD_Angle_Pitch > sram.Recipe.RecordCCD_Angle_End)  //超過了要停了
                        {
                            //Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive, 360 - sram.PitchAngleTotal); // 最到最後一個角度後 補到360度
                            if (MachineType == 0 || MachineType == 2)
                            {
                                Common.motion.PosMove(Mo.AxisNo.DD, fram.m_WaferBackToFoupAngle);
                                if (MachineType == 2)
                                    Common.motion.PosMove(Mo.AxisNo.AP6II_Z2, Homepos.AP6II.RecordCCD.AP6II_Z2);
                            }
                            else
                            {
                                Common.motion.PosMove(Mo.AxisNo.DD, 0); // 回0度
                            }
                            SpinWait.SpinUntil(() => false, 500);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 20000);
                            if (MachineType == 2)
                                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_Z2), 20000);
                            AutoRecordCCDStep = RecordCCDStep.UpdateSecs;
                            break;
                        }
                        else
                        {
                            AutoRecordCCDStep = RecordCCDStep.DDRotate;
                            Tapetest = 0;
                        }
                    }

                    break;

                case RecordCCDStep.UpdateSecs:

                    #region 算完8個角度後計算最大值

                    string Slot_InfoMax = "";
                    double max_W1 = 0;

                    for (int i = 0; i < 25; i++)
                    {
                        max_W1 = 0;
                        if (EFEM.IsInit)
                        {
                            if (sram.Recipe.Slot[i] == 1)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    if (fram.EFEMSts.H1[i, j] > max_W1)
                                    {
                                        max_W1 = fram.EFEMSts.H1[i, j];
                                    }
                                }

                                if (sram.Recipe.Type == 0)
                                {
                                    Slot_InfoMax += "0,";
                                    Slot_InfoMax += "0,";//max_W1 + ",";   20230628
                                    Slot_InfoMax += "0,";
                                    Slot_InfoMax += "0";
                                }
                            }
                            else
                            {
                                Slot_InfoMax += "0,";
                                Slot_InfoMax += "0,";
                                Slot_InfoMax += "0,";
                                Slot_InfoMax += "0";
                            }
                        }
                        if(!Flag.isRecordCCD)   //不是量測附屬的拍照時，才要去更新，否則會洗掉剛才量測的數據
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Slot1_Max + i, Slot_InfoMax, out err);
                        Slot_InfoMax = "";
                    }

                    #endregion 算完8個角度後計算最大值

                    string Slot_Info = "";

                    for (int i = 0; i < 25; i++)
                    {
                        Slot_Info += sram.Recipe.Type + ","; // 0: blue tape, 1: 1step, 2: 2step
                        Slot_Info += sram.Recipe.Rotate_Count + ","; // 4 or 8 個點
                        for (int j = 0; j < sram.Recipe.Rotate_Count; j++) // 每一片的 4/8 個位置
                        {
                            // 加入 Angle
                            if (sram.Recipe.Rotate_Count == 4)
                            {
                                if (EFEM.IsInit)
                                {
                                    if (Common.EFEM.LoadPort_Run.Slot[i] == 1)
                                    {
                                        Slot_Info += sram.Recipe.Angle[j * 2] + ",";
                                    }
                                    else
                                    {
                                        Slot_Info += "0,";
                                    }
                                }
                                else
                                {
                                    Slot_Info += sram.Recipe.Angle[j * 2] + ",";
                                }
                            }
                            else
                            {
                                if (EFEM.IsInit)
                                {
                                    if (Common.EFEM.LoadPort_Run.Slot[i] == 1)
                                    {
                                        Slot_Info += sram.Recipe.Angle[j] + ",";
                                    }
                                    else
                                    {
                                        Slot_Info += "0,";
                                    }
                                }
                                else
                                {
                                    Slot_Info += sram.Recipe.Angle[j] + ",";
                                }
                            }

                            // 每個type上傳的不一樣
                            if (sram.Recipe.Type == 0) // Blue Tape
                            {
                                Slot_Info += 0 + ",";
                                Slot_Info += fram.EFEMSts.W1[i, j] + ",";                           // blueTape只有W1
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                            }
                            else
                            {
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                            }
                        }
                        if (!Flag.isRecordCCD)   //不是量測附屬的拍照時，才要去更新，否則會洗掉剛才量測的數據
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Slot1_Info + i, Slot_Info, out err);
                        Slot_Info = "";
                    }
                    if (EFEM.IsInit)
                    {
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort_Run.FoupID, out err);
                        if (Common.EFEM.LoadPort_Run.pn == LoadPort.Pn.P1)
                        {
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                        }
                        else if (Common.EFEM.LoadPort_Run.pn == LoadPort.Pn.P2)
                        {
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                        }
                    }


                    Measure_RecordCCD_Done = true;
                    AutoRecordCCDStep = RecordCCDStep.Finish;
                    AnalysisData.rotateCount_current = 0;
                    break;

                case RecordCCDStep.Finish:
                    if (Measure_RecordCCD_Done == false)        //在Finish待命的時候，如果有把Measure_RecordCCD_Done改false會觸發一次BT檢測
                        AutoRecordCCDStep = RecordCCDStep.RecordCCDStart;
                    AnalysisData.rotateCount_current = 0;

                    Tapetest = 0;
                    break;

                default:
                    AutoRecordCCDStep = RecordCCDStep.Finish;
                    break;
            }
        }

        private static void AutoModule()
        {
            switch (AutoRunStageStep)
            {
                case AutoStep.GotoSwitchWaferPos:
                    if (MachineType == 0 || MachineType == 2) // AP6 直接解真空
                    {

                        AutoRunStageStep = AutoStep.WaitWaferPresence;
                    }
                    else if (MachineType == 1) // N2 X Y Theta 軸也要回原點
                    {
                        Common.motion.PosMove(Mo.AxisNo.DD, 0);
                        Common.motion.PosMove(Mo.AxisNo.X, Homepos.N2.WaferSwitch.X);
                        Common.motion.PosMove(Mo.AxisNo.Y, Homepos.N2.WaferSwitch.Y);
                        SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 20000);
                        SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.X), 10000);
                        SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.Y), 10000);
                        if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y) && Common.io.In(IOName.In.Wafer取放原點檢))
                        {
                            AutoRunStageStep = AutoStep.WaitWaferPresence; // 所有軸都回去了 才能解真空
                        }
                    }
                    break;

                case AutoStep.WaitWaferPresence:
                    if (fram.EFEMSts.Skip == 0 && EFEM.IsInit || fram.m_simulateRun != 0)
                    { 
                        if (MachineType == 0 || MachineType == 2)
                        {
                            if ((Common.io.In(IOName.In.Wafer汽缸_抬起檢) || fram.m_simulateRun != 0) && !Common.EFEM.Stage1.Measuredone)
                            {
                                if ((Common.io.In(IOName.In.StageWafer在席) || fram.m_simulateRun != 0) && Common.EFEM.Stage1.WaferPresence)
                                {
                                    Common.EFEM.Stage1.Ready = false;
                                    Common.io.WriteOut(IOName.Out.StageWaferReady, false);
                                    Common.io.WriteOut(IOName.Out.StageWafer在席, true);
                                    AutoRunStageStep = AutoStep.VacuumOn;
                                }
                            }
                        }
                        else if (MachineType == 1)
                        {
                            if (Common.io.In(IOName.In.StageWafer在席) || fram.m_simulateRun != 0)
                            {
                                Common.io.WriteOut(IOName.Out.平台破真空電磁閥, false); // 在席On就先開
                                Common.io.WriteOut(IOName.Out.平台真空電磁閥, true);
                                if (Common.EFEM.Stage1.WaferPresence)
                                {
                                    Common.EFEM.Stage1.Ready = false;
                                    Common.io.WriteOut(IOName.Out.StageWaferReady, false);
                                    Common.io.WriteOut(IOName.Out.StageWafer在席, true);
                                    AutoRunStageStep = AutoStep.VacuumOn;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (MachineType == 0 || MachineType == 2)
                        {
                            //|| fram.m_simulateRun != 0
                            //if (Common.io.In(IOName.In.Wafer汽缸_抬起檢) || true)
                            if (true)
                            {
                                //if (Common.io.In(IOName.In.StageWafer在席) && !Common.EFEM.Robot.WaferPresence_Upper)
                                //if (Common.io.In(IOName.In.StageWafer在席)) // 把EFEM分離
                                if (true) // 把EFEM分離
                                {
                                    Common.io.WriteOut(IOName.Out.StageWaferReady, false);
                                    Common.io.WriteOut(IOName.Out.StageWafer在席, true);
                                    AutoRunStageStep = AutoStep.VacuumOn;
                                }
                            }
                        }
                        else if (MachineType == 1)
                        {
                            if (Common.io.In(IOName.In.Wafer取放原點檢))
                            {
                                if (!Common.io.In(IOName.In.StageWafer在席) && (Common.motion.MotionDone(Mo.AxisNo.DD))) // 把EFEM分離  20230327 Sean 增加MotionDone確保安全
                                {
                                    Common.io.WriteOut(IOName.Out.StageWaferReady, false);
                                    Common.io.WriteOut(IOName.Out.StageWafer在席, true);
                                    AutoRunStageStep = AutoStep.VacuumOn;
                                }
                            }
                            else if (Common.motion.MotionDone(Mo.AxisNo.DD))
                            {
                                Common.motion.PosMove(Mo.AxisNo.DD, 0);
                            }
                        }
                    }

                    break;

                case AutoStep.VacuumOn:
                    if (MachineType == 0 || MachineType == 2)
                    {
                        Common.io.WriteOut(IOName.Out.平台破真空電磁閥, false);
                        Common.io.WriteOut(IOName.Out.平台真空電磁閥, true);
                        AutoRunStageStep = AutoStep.CylinderDown;
                    }
                    else if (MachineType == 1) // 跟AP6取放片不太一樣，可能要變成常On，等EFEM來測試
                    {
                        Common.io.WriteOut(IOName.Out.平台破真空電磁閥, false);
                        Common.io.WriteOut(IOName.Out.平台真空電磁閥, true);
                        AutoRunStageStep = AutoStep.GetSlotInfo;
                    }
                    break;

                case AutoStep.CylinderDown: //AP6 才進來
                    Common.io.WriteOut(IOName.Out.Wafer汽缸_抬起, false);
                    Common.io.WriteOut(IOName.Out.Wafer汽缸_降下, true);
                    SpinWait.SpinUntil(() => false, 500);
                    AutoRunStageStep = AutoStep.GetSlotInfo;
                    break;

                case AutoStep.GetSlotInfo:
                    // AP6 N6負壓檢同一個
                    // AP6 還要檢查降下檢
                    // N2 只看負壓檢就好
					Flag.isRecordCCD = false;
                    if (MachineType == 0 || MachineType == 2)
                    {
                        SpinWait.SpinUntil(() => (Common.io.In(IOName.In.Wafer汽缸_降下檢) || fram.m_simulateRun != 0), 10000);
                        SpinWait.SpinUntil(() => (Common.io.In(IOName.In.真空平台_負壓檢) || fram.m_simulateRun != 0), 10000);
                        if (Common.io.In(IOName.In.真空平台_負壓檢) && Common.io.In(IOName.In.Wafer汽缸_降下檢) || fram.m_DEMOMode == 1 || fram.m_simulateRun != 0)
                        //if (true)
                        {
                            if(fram.EFEMSts.Skip == 0 || fram.m_simulateRun != 0)
                            {
                                ReportData.Lot = FoupID;
                                ReportData.Slot = Slot;
                            }
                            else
                            {
                                ReportData.Lot = "TestLot";
                                if(fram.m_DEMOMode != 1)
                                    ReportData.Slot = 7;
                                else
                                {
                                    ReportData.Slot = DemoModeSlot;
                                    DemoModeSlot++;
                                    DemoModeSlot = DemoModeSlot >= 26? DemoModeSlot - 25 : DemoModeSlot;
                                }

                                Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort1;

                                Common.EFEM.LoadPort_Run.FoupID = ReportData.Lot;
                                Common.EFEM.Stage1.Slot = ReportData.Slot;
                                FoupID = ReportData.Lot;
                                Slot = ReportData.Slot;
                            }

                            if(fram.m_SecsgemType == 1)
                            {
                                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MeasureStart, out err);
                            }
                            //HTW進入LJ量測
                            //Flag.isLJforHTW = Measure_TrimType_On && Measure_HTW_On && (sram.Recipe.Type == 4) && fram.Analysis.W2_LJ_Replace_HTW == 1;
                            //直接配發選擇的recipe type
                            if (Measure_TrimType_On && (sram.Recipe.Type == 1 || sram.Recipe.Type == 2)
                                || (Measure_TrimType_On && Measure_HTW_On && (sram.Recipe.Type == 4) && fram.Analysis.W2_LJ_Replace_HTW == 1))
                            {                      
                                if (fram.S_SensorConnectType == 0)
                                {
                                    Common.LJ.ModeInfo();

                                    if (Common.LJ.LJ_Mode == Common.LJ.LJMode.Setting_Mode)
                                    {
                                        Common.LJ.Set_RunningMode();
                                    }
                                    Common.LJ.Reset();   // 每個lot結束後把量測資料夾reset
                                    Common.LJ.Measure(); // 馬上拍一個避免抓不到最新的資料夾
                                }
                                else if (fram.S_SensorConnectType == 1)
                                {

                                }
                                AutoTrimStep = TrimStep.LJHome;
                                AutoRunStageStep = AutoStep.TrimMode;
                            }
                            else if (Measure_TrimType2nd_On && (sram.Recipe.Type == 3 || sram.Recipe.Type == 6))
                            {
                                AutoTrim2ndStep = Trim2ndStep.PTHome;
                                AutoRunStageStep = AutoStep.TrimMode2nd;
                            }
                            else if (Measure_HTW_On && (sram.Recipe.Type == 4 || sram.Recipe.Type == 7))
                            {
                                AutoHTWStep = HTWStep.Finish;
                                AutoRunStageStep = AutoStep.HTWMode;
                            }
                            else if (Measure_BlueTape_On && sram.Recipe.Type == 0)
                            {
                                AutoBlueTapeStep = BluetapeStep.Finish;
                                AutoRunStageStep = AutoStep.BlueTapeMode;
                            }
                            else if (Measure_BlueTape_On && sram.Recipe.Type == 5)
                            {
                                AutoRecordCCDStep = RecordCCDStep.Finish;
                                AutoRunStageStep = AutoStep.RecordCCDMode;
                            }
                            else if (Measure_TTV_On && sram.Recipe.Type == 8)
                            {
                                AutoTTVStep = TTVStep.Finish;
                                AutoRunStageStep = AutoStep.TTVMode;
                            }
                            else
                                AutoRunStageStep = AutoStep.GotoWaitGetHomePos;

                            sram.bRecordCCD = false;
                        }
                        else if (!Common.io.In(IOName.In.Wafer汽缸_降下檢) && (fram.m_simulateRun == 0))
                        {
                            Flag.AlarmFlag = true;
                            Flag.EQAlarmReportFlag = true; // True是可以report
                            InsertLog.SavetoDB(150);
                            Console.WriteLine("Wafer汽缸_降下檢 Time Out");
                        }
                        else if (!Common.io.In(IOName.In.真空平台_負壓檢) && (fram.m_simulateRun == 0))
                        {
                            Flag.AlarmFlag = true;
                            Flag.EQAlarmReportFlag = true; // True是可以report
                            InsertLog.SavetoDB(152);
                            Console.WriteLine("真空平台_負壓檢 Time Out");
                        }
                        else if(fram.m_simulateRun == 0)
                        {
                            Flag.AlarmFlag = true;
                            Flag.EQAlarmReportFlag = true; // True是可以report
                            InsertLog.SavetoDB(150);
                            InsertLog.SavetoDB(152);
                            Console.WriteLine("Wafer汽缸_降下檢 Time Out");
                            Console.WriteLine("真空平台_負壓檢 Time Out");
                        }
                    }
                    else if (MachineType == 1)
                    {
                        SpinWait.SpinUntil(() => Common.io.In(IOName.In.真空平台_負壓檢), 10000);
                        if (Common.io.In(IOName.In.真空平台_負壓檢))
                        {
                            ReportData.Lot = FoupID;
                            ReportData.Slot = Slot;

                            if (fram.m_SecsgemType == 1)
                            {
                                //Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MeasureStart, out err);
                            }
                            //HTW進入LJ量測
                            //Flag.isLJforHTW = Measure_TrimType_On && Measure_HTW_On && (sram.Recipe.Type == 4) && fram.Analysis.W2_LJ_Replace_HTW == 1;
                            //直接配發選擇的recipe type
                            if (Measure_TrimType_On && (sram.Recipe.Type == 1 || sram.Recipe.Type == 2)
                                || (Measure_TrimType_On && Measure_HTW_On && (sram.Recipe.Type == 4) && fram.Analysis.W2_LJ_Replace_HTW == 1))
                            {
                                if (fram.S_SensorConnectType == 0)
                                {
                                    Common.LJ.ModeInfo();

                                    if (Common.LJ.LJ_Mode == Common.LJ.LJMode.Setting_Mode)
                                    {
                                        Common.LJ.Set_RunningMode();
                                    }
                                    Common.LJ.Reset();   // 每個lot結束後把量測資料夾reset
                                    Common.LJ.Measure(); // 馬上拍一個避免抓不到最新的資料夾
                                }
                                else if (fram.S_SensorConnectType == 1)
                                {

                                }
                                AutoTrimStep = TrimStep.LJHome;
                                AutoRunStageStep = AutoStep.TrimMode;
                            }
                            else if (Measure_TrimType2nd_On && (sram.Recipe.Type == 3 || sram.Recipe.Type == 6))
                            {
                                AutoTrim2ndStep = Trim2ndStep.PTHome;
                                AutoRunStageStep = AutoStep.TrimMode2nd;
                            }
                            else if (Measure_HTW_On && (sram.Recipe.Type == 4 || sram.Recipe.Type == 7))
                            {
                                AutoHTWStep = HTWStep.Finish;
                                AutoRunStageStep = AutoStep.HTWMode;
                            }
                            else if (Measure_BlueTape_On && sram.Recipe.Type == 0)
                            {
                                AutoBlueTapeStep = BluetapeStep.Finish;
                                AutoRunStageStep = AutoStep.BlueTapeMode;
                            }
                            else if (Measure_BlueTape_On && sram.Recipe.Type == 5)
                            {
                                AutoRecordCCDStep = RecordCCDStep.Finish;
                                AutoRunStageStep = AutoStep.RecordCCDMode;
                            }
                            else if (Measure_TTV_On && sram.Recipe.Type == 8)
                            {
                                AutoTTVStep = TTVStep.Finish;
                                AutoRunStageStep = AutoStep.TTVMode;
                            }
                            else
                                AutoRunStageStep = AutoStep.GotoWaitGetHomePos;

                            sram.bRecordCCD = false;
                        }
                        else if (!Common.io.In(IOName.In.真空平台_負壓檢))
                        {
                            Flag.AlarmFlag = true;
                            Flag.EQAlarmReportFlag = true; // True是可以report
                            InsertLog.SavetoDB(152);
                            Console.WriteLine("真空平台_負壓檢 Time Out");
                        }
                    }

                    break;

                case AutoStep.TrimMode:
                    if (Measure_TrimType_On)
                    {
                        Measurement_TrimType_LJ();

                        // 完成之後跳下一個量測模式
                        if (Measure_TrimType_Done)
                        {
                            Measure_TrimType_Done = false;
                            if((Measure_TrimType_On && Measure_HTW_On && (sram.Recipe.Type == 4 || sram.Recipe.Type == 7)) && fram.Analysis.W2_LJ_Replace_HTW == 1)
                            {
                                AutoHTWStep = HTWStep.Finish;
                                AutoRunStageStep = AutoStep.HTWMode;
                            }
                            else if (sram.bRecordCCD || (sram.Recipe.RecordAfterMeasure == 1))
                            {
                                AutoRecordCCDStep = RecordCCDStep.Finish;
                                AutoRunStageStep = AutoStep.RecordCCDMode;
								Flag.isRecordCCD = true;
                            }
                            else
                            {
                                AutoRunStageStep = AutoStep.GotoWaitGetHomePos;
                            }
                        }
                    }
                    else
                    {
                        AutoRunStageStep = AutoStep.GotoWaitGetHomePos;
                    }
                    break;
                case AutoStep.TrimMode2nd:
                    if (Measure_TrimType_On)
                    {
                        Measurement_TrimType_PT();

                        // 完成之後跳下一個量測模式
                        if (Measure_TrimType2nd_Done)
                        {
                            Measure_TrimType2nd_Done = false;
                            if (sram.bRecordCCD || (sram.Recipe.RecordAfterMeasure == 1))
                            {
                                AutoRecordCCDStep = RecordCCDStep.Finish;
                                AutoRunStageStep = AutoStep.RecordCCDMode;
								Flag.isRecordCCD = true;
                            }
                            else
                            {
                                AutoRunStageStep = AutoStep.GotoWaitGetHomePos;
                            }
                        }
                    }
                    else
                    {
                        AutoRunStageStep = AutoStep.GotoWaitGetHomePos;
                    }
                    break;
                case AutoStep.HTWMode:
                    if (Measure_HTW_On)
                    {
                        Measurement_TrimType_HTW();

                        // 完成之後跳下一個量測模式
                        if (Measure_HTW_Done)
                        {
                            Measure_HTW_Done = false;
                            if (sram.bRecordCCD || (sram.Recipe.RecordAfterMeasure == 1))
                            {
                                AutoRecordCCDStep = RecordCCDStep.Finish;
                                AutoRunStageStep = AutoStep.RecordCCDMode;
								Flag.isRecordCCD = true;
                            }
                            else
                            {
                                AutoRunStageStep = AutoStep.GotoWaitGetHomePos;
                            }
                        }
                    }
                    else
                    {
                        AutoRunStageStep = AutoStep.GotoWaitGetHomePos;
                    }
                    break;
                case AutoStep.BlueTapeMode:
                    if (Measure_BlueTape_On)
                    {
                        // 模擬模式下 Camera 未初始化，跳過 CCD 量測
                        if (!AutoRunEFEM.CameraReady && fram.m_simulateRun != 0)
                        {
                            Console.WriteLine("模擬模式跳過 Bluetape CCD 量測 (Camera 未初始化)");
                            Measure_BlueTape_Done = true;
                        }
                        else
                        {
                            Measure_BlueTape_Done = false;
                            Measurement_BlueTape_CCD();
                        }
                        if (Measure_BlueTape_Done)
                        {
                            if (sram.bRecordCCD || (sram.Recipe.RecordAfterMeasure == 1))
                            {
                                AutoRecordCCDStep = RecordCCDStep.Finish;
                                AutoRunStageStep = AutoStep.RecordCCDMode;
								Flag.isRecordCCD = true;
                            }
                            else
                            {
                                AutoRunStageStep = AutoStep.GotoWaitGetHomePos;
                            }
                        }
                    }
                    else
                    {
                        AutoRunStageStep = AutoStep.GotoWaitGetHomePos;
                    }
                    break;

                case AutoStep.TTVMode:
                    if (Measure_TTV_On)
                    {
                        Measure_TTV_Done = false;
                        Measurement_TTV_SF3();
                        if (Measure_TTV_Done)
                        {
                            if (sram.bRecordCCD || (sram.Recipe.RecordAfterMeasure == 1))
                            {
                                AutoRecordCCDStep = RecordCCDStep.Finish;
                                AutoRunStageStep = AutoStep.RecordCCDMode;
								Flag.isRecordCCD = true;
                            }
                            else
                            {
                                AutoRunStageStep = AutoStep.GotoWaitGetHomePos;
                            }
                        }
                    }
                    else
                    {
                        AutoRunStageStep = AutoStep.GotoWaitGetHomePos;
                    }
                    break;

                case AutoStep.RecordCCDMode:
                    if (Measure_RecordCCD_On)
                    {
                        Measure_RecordCCD_Done = false;
                        Measurement_Record_CCD();
                        if (Measure_RecordCCD_Done)
                        {
                            AutoRunStageStep = AutoStep.GotoWaitGetHomePos;
                        }
                    }
                    else
                    {
                        AutoRunStageStep = AutoStep.GotoWaitGetHomePos;
                    }
                    break;

                case AutoStep.GotoWaitGetHomePos:
                    if (fram.m_SecsgemType == 1)
                    {
                        Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MeasureEnd, out err);
                    }

                    if (MachineType == 0) // AP6 直接解真空
                    {
                        if (fram.EFEMSts.Skip == 0 || fram.m_simulateRun != 0)
                            AutoRunStageStep = AutoStep.VacuumOff;
                        else
                            AutoRunStageStep = AutoStep.Finish;
                        if (fram.m_Hardware_PT == 1 && Common.PTForm.GetPointMoveFinish(9))
                        {
                            Common.PTForm.PointMove(9);
                        }
                    }
                    else if(MachineType == 2)
                    {
                        if (fram.EFEMSts.Skip == 0 || fram.m_simulateRun != 0)
                            AutoRunStageStep = AutoStep.VacuumOff;
                        else
                            AutoRunStageStep = AutoStep.Finish;
                        Common.motion.PosMove(Mo.AxisNo.AP6II_X, Homepos.AP6II.HTW.AP6II_X);
                        Common.motion.PosMove(Mo.AxisNo.AP6II_Z2, Homepos.AP6II.RecordCCD.AP6II_Z2);
                    }
                    else if (MachineType == 1 || fram.m_simulateRun != 0) // N2 X Y Theta 軸也要回原點
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y))
                        {
                            Common.motion.PosMove(Mo.AxisNo.DD, 0);
                            Common.motion.PosMove(Mo.AxisNo.X, Homepos.N2.WaferSwitch.X);
                            Common.motion.PosMove(Mo.AxisNo.Y, Homepos.N2.WaferSwitch.Y);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 20000);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.X), 10000);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.Y), 10000);
                            if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y) && Common.io.In(IOName.In.Wafer取放原點檢))
                            {
                                AutoRunStageStep = AutoStep.VacuumOff; // 所有軸都回去了 才能解真空
                                //AutoRunStageStep = AutoStep.Finish;
                            }
                        }
                    }
                    break;

                case AutoStep.VacuumOff:
                    if (MachineType == 0)
                    {
                        Common.io.WriteOut(IOName.Out.平台真空電磁閥, false);
                        Common.io.WriteOut(IOName.Out.平台破真空電磁閥, true);
                        if (fram.m_Hardware_PT == 1 && Common.PTForm.GetPointMoveFinish(9))
                            AutoRunStageStep = AutoStep.CylinderUp;
                    }
                    else if (MachineType == 2)
                    {
                        if(fram.EFEMSts.Skip == 0)
                        {
                            Common.io.WriteOut(IOName.Out.平台真空電磁閥, false);
                            Common.io.WriteOut(IOName.Out.平台破真空電磁閥, true);
                        }
                        if(Common.motion.MotionDone(Mo.AxisNo.AP6II_X) && Common.motion.MotionDone(Mo.AxisNo.AP6II_Z2) 
                            && Common.motion.Get_FBPos(Mo.AxisNo.AP6II_X) == 0 && Common.motion.Get_FBPos(Mo.AxisNo.AP6II_Z2) == 0)
                            AutoRunStageStep = AutoStep.CylinderUp;
                    }
                    else if (MachineType == 1)
                    {
                        Common.io.WriteOut(IOName.Out.平台真空電磁閥, false);
                        Common.io.WriteOut(IOName.Out.平台破真空電磁閥, true);

                        AutoRunStageStep = AutoStep.CylinderUp;
                    }
                    break;

                case AutoStep.CylinderUp:
                    if (MachineType == 0 || MachineType == 2)
                    {
                        if (fram.m_Hardware_PT == 1)
                        {
                            if (Common.PTForm.GetPointMoveFinish(9))
                            {
                                Common.io.WriteOut(IOName.Out.Wafer汽缸_降下, false);
                                Common.io.WriteOut(IOName.Out.Wafer汽缸_抬起, true);
                                SpinWait.SpinUntil(() => (Common.io.In(IOName.In.Wafer汽缸_抬起檢)), 10000);
                            }
                            else
                            {
                                Common.PTForm.PointMove(9);
                                SpinWait.SpinUntil(() => Common.PTForm.GetPointMoveFinish(9), 2000);
                                break;
                            }
                        }
                        else if(MachineType == 2)
                        {
                            if (Math.Abs(Common.motion.Get_FBPos(Mo.AxisNo.AP6II_X)) < 0.1 && Math.Abs(Common.motion.Get_FBPos(Mo.AxisNo.AP6II_Z2)) < 0.1)
                            {
                                if (fram.EFEMSts.Skip == 0)
                                {
                                    Common.io.WriteOut(IOName.Out.Wafer汽缸_降下, false);
                                    Common.io.WriteOut(IOName.Out.Wafer汽缸_抬起, true);
                                    SpinWait.SpinUntil(() => (Common.io.In(IOName.In.Wafer汽缸_抬起檢)), 10000);
                                }
                            }
                            else
                            {
                                Common.motion.PosMove(Mo.AxisNo.AP6II_X, Homepos.AP6II.HTW.AP6II_X);
                                Common.motion.PosMove(Mo.AxisNo.AP6II_Z2, Homepos.AP6II.RecordCCD.AP6II_Z2);
                                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_X) && Common.motion.MotionDone(Mo.AxisNo.AP6II_Z2), 5000);
                                break;
                            }
                        }
                        else
                        {
                            Common.io.WriteOut(IOName.Out.Wafer汽缸_降下, false);
                            Common.io.WriteOut(IOName.Out.Wafer汽缸_抬起, true);
                            SpinWait.SpinUntil(() => (Common.io.In(IOName.In.Wafer汽缸_抬起檢)), 10000);
                        }

                        if (Common.io.In(IOName.In.Wafer汽缸_抬起檢) || fram.EFEMSts.Skip == 1)
                        {
                            Common.io.WriteOut(IOName.Out.StageWaferReady, true);
                            AutoRunStageStep = AutoStep.WaitWaferunload;
                            if (fram.m_SecsgemType == 1)
                            {
                                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, sram.PJInfo.carrierID, out err);
                                //Common.SecsgemForm.UpdateSV(TrimGap_EqpID.LotID, sram.CarrierInfo.lotID[Slot], out err);
                                //Common.SecsgemForm.UpdateSV(TrimGap_EqpID.SubstrateID, sram.CarrierInfo.substrateID[Slot], out err);
                                //Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MeasureEnd, out err);
                            }
                        }
                        else
                        {
                            Flag.AlarmFlag = true;
                            Flag.EQAlarmReportFlag = true; // True是可以report
                            InsertLog.SavetoDB(151);
                            Console.WriteLine("Wafer汽缸抬起檢 Time out");
                        }
                    }
                    else if (MachineType == 1)
                    {
                        //XY軸已經回取放片位置了
                        Common.io.WriteOut(IOName.Out.StageWaferReady, true);
                        AutoRunStageStep = AutoStep.WaitWaferunload;
                        if (fram.m_SecsgemType == 1)
                        {
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, sram.PJInfo.carrierID, out err);
                            //Common.SecsgemForm.UpdateSV(TrimGap_EqpID.LotID, sram.CarrierInfo.lotID[Slot], out err);
                            //Common.SecsgemForm.UpdateSV(TrimGap_EqpID.SubstrateID, sram.CarrierInfo.substrateID[Slot], out err);
                            //Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MeasureEnd, out err);
                        }
                    }

                    break;

                case AutoStep.WaitWaferunload:
                    if (EFEM.IsInit && fram.EFEMSts.Skip == 0 || fram.m_simulateRun!= 0) // 這裡做完後等著被拿走
                    {
                        Common.EFEM.Stage1.Ready = true;
                        Common.EFEM.Stage1.Measuredone = true;
                    }

                    if (!Common.io.In(IOName.In.StageWafer在席) && !Common.EFEM.Stage1.WaferPresence) // Common.EFEM.Stage1.WaferPresence => WagetGet完後會自動切
                    {
                        if (MachineType == 0 || MachineType == 2)
                        {
                            Common.motion.SetHome(Mo.AxisNo.DD);
                        }
                        else if (MachineType == 1)
                        {
                        }
                        Common.io.WriteOut(IOName.Out.StageWafer在席, false);

                        AutoRunStageStep = AutoStep.Finish;
                    }
                    else if(!(EFEM.IsInit && fram.EFEMSts.Skip == 0))
                    {
                        AutoRunStageStep = AutoStep.Finish;
                    }
                    break;

                case AutoStep.Finish:

                    AutoRunStageStep = AutoStep.GotoSwitchWaferPos;
                    if (fram.EFEMSts.Skip == 1 && fram.m_DEMOMode != 1)
                    {
                        Flag.AutoidleFlag = false;
                        Flag.Autoidle_LocalFlag = false;
                    }

                    break;

                default:
                    break;
            }
        }

        private static void bWAutoRun_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    if (Flag.AutoidleFlag && !Flag.AlarmFlag)
                    {
                        AutoModule();

                        SpinWait.SpinUntil(() => false, 10);
                        if (AutoRunStageStep == AutoStep.JudgeRotateCount)
                        {
                        }
                        else if (AutoRunStageStep == AutoStep.Finish)
                        {
                            //bWAutoRun.ReportProgress(100);
                        }
                    }
                }
                catch (Exception ee)
                {
                    Console.WriteLine("bwAutoRun : " + ee.Message);
                    MessageBox.Show("bwAutoRun : " + ee.Message);
                }
            }
        }

        #region Insertlog

        private static void InsertReportTable(DateTime Time, string Lotnum, int slot, double Angle, double H1, double W1, double H2, double W2, string Result, string NOTE)
        {
            object[,] Data = new object[,] {
                        {"Time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},
                        {"Lot", Lotnum },
                        {"Slot", slot },
                        {"Angle", Angle },
                        {"H1", H1 },
                        {"W1", W1 },
                        {"H2", H2 },
                        {"W2", W2 },
                        {"Result",  Result},
                        {"Note", NOTE } };
            Common.dataBase.sQ.InsertData("reportTable", Data);
        }

        private static void InsertReportTableTTV(DateTime Time, string Lotnum, int slot, double Angle, double TTV, double Max, double Min, double Mean, string Result, string NOTE)
        {
            object[,] Data = new object[,] {
                        {"Time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},
                        {"Lot", Lotnum },
                        {"Slot", slot },
                        {"Angle", Angle },
                        {"TTV", TTV },
                        {"Max", Max },
                        {"Min", Min },
                        {"Mean", Mean },
                        {"Result",  Result},
                        {"Note", NOTE } };
            Common.dataBase.sQ.InsertData("reportTableTTV", Data);
        }

        #endregion Insertlog

        private static void bWAutoRun_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 7)
            {
            }
            else if (e.ProgressPercentage == 10)  // TrimTypeUpd
            {
            }
            else if (e.ProgressPercentage == 20)
            {
                //Update_UI_Sts();
            }
            else if (e.ProgressPercentage == 100)
            {
                GC.Collect();
            }
        }

        private static double[] Avg_Data(List<double[]> list_data, int list_len)
        {
            double[] do_Data = new double[list_len];

            for (int i = 0; i < list_len; i++)
            {
                List<double> tmp_data = new List<double>();
                for(int j = 0; j < list_data.Count(); j++)
                {
                    tmp_data.Add(list_data[j][i]);
                }

                //if(fram.Analysis.HTW_Measure_Count >= 3 || fram.Analysis.LJ_Measure_Count >= 3)
                List<double> list_Filter = (from pair in tmp_data
                                           where pair < tmp_data.Max() && pair > tmp_data.Min()
                                           select pair).ToList();

                if(list_Filter.Count() == 0)
                {
                    list_Filter = tmp_data;
                }


                do_Data[i] = list_Filter.Average();
            }

            return do_Data;
        }
    }
}