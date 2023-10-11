using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
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
        public static BluetapeStep AutoBlueTapeStep;
        public static TTVStep AutoTTVStep;
        public static Chart chartSensor;

        //public static bool WaferPresence = false;
        public static bool AnalysisFlag = true;

        public static bool Measure_TrimType_On = false;
        public static bool Measure_BlueTape_On = false;
        public static bool Measure_TTV_On = false;
        public static bool Measure_TrimType_Done = false;
        public static bool Measure_TrimType2nd_Done = false;
        public static bool Measure_BlueTape_Done = false;
        public static bool Measure_TTV_Done = false;
        public static string FoupID = "r";
        public static int Slot = 0;
        private static int MachineType = 0; // 0:AP6, 1:N2
        private static int MotionType = 0; // 0:SSC, 1:ETEL

        //public bool AnalysisFlag
        //{
        //    get { return analysisFlag; }
        //    set { analysisFlag = value; }
        //}

        public struct Homepos
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

            public struct Precitec
            {
                public static double X = 135.86 * 1000;
                public static double Y = 171.24 * 1000;
                public static double Angle = 90;
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
                Measure_BlueTape_On = true;
                Measure_TTV_On = false;
            }
            else if (MachineType == 1) // N2
            {
                Measure_TrimType_On = true;
                Measure_BlueTape_On = false;
                Measure_TTV_On = true;

                Common.motion.FindHome(Mo.AxisNo.DD);
                Common.motion.FindHome(Mo.AxisNo.X);
                Common.motion.FindHome(Mo.AxisNo.Y);
                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 20000); // 等待10秒
                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.X), 10000); // 等待10秒
                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.Y), 10000); // 等待10秒
                if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y))
                {
                    Common.motion.PosMove(Mo.AxisNo.DD, 0);
                    Common.motion.PosMove(Mo.AxisNo.X, Homepos.WaferSwitch.X);
                    Common.motion.PosMove(Mo.AxisNo.Y, Homepos.WaferSwitch.Y);
                }
                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 10000); // 等待10秒
                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.X), 15000); // 等待10秒
                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.Y), 15000); // 等待10秒
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

            [Description("BlueTapeMode")]
            BlueTapeMode,

            [Description("TTVMode")]
            TTVMode,

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
                    else if (MachineType == 1) // N2 要移動到LJ的home點
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y))
                        {
                            //這裡要下XY軸的絕對位置，走去LJ的原點
                            Common.motion.PosMove(Mo.AxisNo.X, Homepos.LJ8020.X);
                            Common.motion.PosMove(Mo.AxisNo.Y, Homepos.LJ8020.Y);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.X), 10000); // 等待10秒
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.Y), 10000); // 等待10秒
                            if (Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y))
                            {
                                AutoTrimStep = TrimStep.DDRotate;
                            }
                        }
                    }
                    break;

                case TrimStep.DDRotate:
                    if (fram.S_MotionRotate == "False") // Bypass motion
                    {
                        AutoTrimStep = TrimStep.Measurement;
                    }
                    else
                    {
                        if (MachineType == 0) // AP6
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
                            if (!Common.motion.MotionDone(Mo.AxisNo.DD) && !Common.motion.MotionDone(Mo.AxisNo.X) && !Common.motion.MotionDone(Mo.AxisNo.Y))
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
                        Common.LJ.GetFolderPath(fram.LJ_FTPfilePath);
                        Common.LJ.Csvlist = Common.LJ.GetCsvlist(Common.LJ.FolderPath);
                        Common.LJ.CurrentListCount = Common.LJ.GetCurrentCsvListCount(Common.LJ.FolderPath);

                        AnalysisData.rtn = Common.LJ.Measure();

                        AutoTrimStep = TrimStep.Download;
                        Console.WriteLine(DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString() + " Measure End");
                    }

                    break;

                case TrimStep.Download:
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
                        ParamFile.SaveRawdata_Csv(AnalysisData.rawData, FoupID + "_" + Slot + "_" + sram.PitchAngleTotal, sram.saveDataTime);
                        Console.WriteLine("Download End " + DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString());
                        InsertLog.SavetoDB(70, Common.LJ.DownloadName);
                        AutoTrimStep = TrimStep.Analysis;
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
                            if((MachineType == 0 && sram.Recipe.Type == 3) || MachineType == 1)  //有做PT時先回0度 或是  N2那種檯面一定要回0度
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
                        Common.CGWrapper.UpdateSV(TrimGap_EqpID.Slot1_Max + i, Slot_InfoMax);
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
                        Common.CGWrapper.UpdateSV(TrimGap_EqpID.Slot1_Info + i, Slot_Info);
                        Slot_Info = "";
                    }
                    if (EFEM.IsInit)
                    {
                        Common.CGWrapper.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort_Run.FoupID);
                        if (Common.EFEM.LoadPort_Run.pn == LoadPort.Pn.P1)
                        {
                            Common.CGWrapper.UpdateSV(TrimGap_EqpID.PortID, 1);
                        }
                        else if (Common.EFEM.LoadPort_Run.pn == LoadPort.Pn.P2)
                        {
                            Common.CGWrapper.UpdateSV(TrimGap_EqpID.PortID, 2);
                        }
                    }

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
                    if (MachineType == 0) // AP6 不用回home
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.DD))
                        {
                            //Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive,Homepos.Precitec.Angle);
                            Common.motion.PosMove(Mo.AxisNo.DD, Homepos.Precitec.Angle + fram.m_WaferAlignAngle);
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
                            Common.motion.PosMove(Mo.AxisNo.X, Homepos.LJ8020.X);
                            Common.motion.PosMove(Mo.AxisNo.Y, Homepos.LJ8020.Y);
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
                        if (MachineType == 0) // AP6
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
                            if (!Common.motion.MotionDone(Mo.AxisNo.DD) && !Common.motion.MotionDone(Mo.AxisNo.X) && !Common.motion.MotionDone(Mo.AxisNo.Y))
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
                    if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.PTForm.GetPointMoveFinish(1))
                    {
                        double posnow = Common.motion.Get_FBAngle(Mo.AxisNo.DD) - fram.m_WaferAlignAngle - Homepos.Precitec.Angle;
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
                        Common.CGWrapper.AlarmReportSend(TrimGap_EqpID.EQP_PT_PLC_MoveTimeoutError, 128);
                        InsertLog.SavetoDB(TrimGap_EqpID.EQP_PT_PLC_MoveTimeoutError, "PT PLC Move To Point 1 Timeout");
                        //MessageBox.Show("PT PLC Move To Point 1 Timeout");
                    }

                    break;

                case Trim2ndStep.Download:
                    if(Common.PTForm.GetPointMoveFinish(2))
                    {
                        if(fram.m_simulateRun == 0)
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
                                ParamFile.SaveRawdata_Csv3(AnalysisData.rawData, AnalysisData.rawData2, AnalysisData.rawData3, FoupID + "_" + Slot + "_" + sram.PitchAngleTotal + "_PT_" + rtn.ToString(), sram.saveDataTime);

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
                            if (MachineType == 0)
                            {
                                Common.motion.PosMove(Mo.AxisNo.DD, fram.m_WaferBackToFoupAngle);
                            }
                            else
                            {
                                Common.motion.PosMove(Mo.AxisNo.DD, 0); // 回0度
                            }
                            Common.PTForm.PointMove(9);//PLC退出
                            SpinWait.SpinUntil(() => false, 500);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD)&& Common.PTForm.GetPointMoveFinish(9), 20000);
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
                        
                        Common.CGWrapper.UpdateSV(TrimGap_EqpID.Slot1_Max + i, Slot_InfoMax);
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
                        Common.CGWrapper.UpdateSV(TrimGap_EqpID.Slot1_Info + i, Slot_Info);
                        Slot_Info = "";
                    }
                    if (EFEM.IsInit)
                    {
                        Common.CGWrapper.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort_Run.FoupID);
                        if (Common.EFEM.LoadPort_Run.pn == LoadPort.Pn.P1)
                        {
                            Common.CGWrapper.UpdateSV(TrimGap_EqpID.PortID, 1);
                        }
                        else if (Common.EFEM.LoadPort_Run.pn == LoadPort.Pn.P2)
                        {
                            Common.CGWrapper.UpdateSV(TrimGap_EqpID.PortID, 2);
                        }
                    }

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

        private static void Measurement_TTV_SF3()
        {
            switch (AutoTTVStep)
            {
                case TTVStep.TTVStart:
                    //fram.Recipe.MotionPatternName = "D:\\FTGM1\\ParameterDirectory\\TTVPath\\Default.tvr";
                    TTVScanPattern ttv = new TTVScanPattern(fram.Recipe.MotionPatternPath, fram.Recipe.MotionPatternName);
                    TTVData.lsRotate = ttv.GetAnglesFromRecipe(fram.Recipe.MotionPatternPath + "\\" + fram.Recipe.MotionPatternName + ".tvr");
                    TTVData.lsShift = ttv.GetPointsFromRecipe(fram.Recipe.MotionPatternPath + "\\" + fram.Recipe.MotionPatternName + ".tvr");
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
                        Common.SF3.Set_Recipe(fram.Recipe.SF3_ID);    // 給Recipe ID
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
                            Common.motion.PosMove(Mo.AxisNo.X, Homepos.SF3.X);
                            Common.motion.PosMove(Mo.AxisNo.Y, Homepos.SF3.Y);
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
                            Common.motion.PosMove(Mo.AxisNo.X, Homepos.SF3.X + TTVData.lsShift[TTVData.shiftCount_current].X);
                            Common.motion.PosMove(Mo.AxisNo.Y, Homepos.SF3.Y + TTVData.lsShift[TTVData.shiftCount_current].Y);
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
                        if (MachineType == 0)
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
                    ParamFile.SaveRawdata_Csv(ReportData.TTVData, FoupID + "_" + Slot + "_" + fram.Recipe.MotionPatternName, sram.saveDataTime);
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
                        //AnalysisData.WaferMeasure[i] = false;
                    }
                    AutoBlueTapeStep = BluetapeStep.CCDHome;
                    break;

                case BluetapeStep.CCDHome:
                    if (MachineType == 0)    // AP6 BT起點
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.DD))
                        {
                            //Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive, Homepos.BlueTape.Angle);
                            Common.motion.PosMove(Mo.AxisNo.DD, Homepos.BlueTape.Angle + fram.m_WaferAlignAngle);
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
                            Common.motion.PosMove(Mo.AxisNo.X, Homepos.BlueTape.X);
                            Common.motion.PosMove(Mo.AxisNo.Y, Homepos.BlueTape.Y);
                            Common.motion.PosMove(Mo.AxisNo.DD, Homepos.BlueTape.Angle);
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
                        if (MachineType == 0) // AP6
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
                        Common.motion.PitchAngle(Mo.AxisNo.DD, Mo.Dir.Postive, sram.PitchAngle);
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
                        double posnow = Common.motion.Get_FBAngle(Mo.AxisNo.DD) - fram.m_WaferAlignAngle - Homepos.Precitec.Angle;
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
                        Common.CGWrapper.UpdateSV(TrimGap_EqpID.Slot1_Max + i, Slot_InfoMax);
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
                            else
                            {
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                                Slot_Info += 0 + ",";
                            }
                        }
                        Common.CGWrapper.UpdateSV(TrimGap_EqpID.Slot1_Info + i, Slot_Info);
                        Slot_Info = "";
                    }
                    if (EFEM.IsInit)
                    {
                        Common.CGWrapper.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort_Run.FoupID);
                        if (Common.EFEM.LoadPort_Run.pn == LoadPort.Pn.P1)
                        {
                            Common.CGWrapper.UpdateSV(TrimGap_EqpID.PortID, 1);
                        }
                        else if (Common.EFEM.LoadPort_Run.pn == LoadPort.Pn.P2)
                        {
                            Common.CGWrapper.UpdateSV(TrimGap_EqpID.PortID, 2);
                        }
                    }


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

        private static void AutoModule()
        {
            switch (AutoRunStageStep)
            {
                case AutoStep.GotoSwitchWaferPos:
                    if (MachineType == 0) // AP6 直接解真空
                    {

                        AutoRunStageStep = AutoStep.WaitWaferPresence;
                    }
                    else if (MachineType == 1) // N2 X Y Theta 軸也要回原點
                    {
                        Common.motion.PosMove(Mo.AxisNo.DD, 0);
                        Common.motion.PosMove(Mo.AxisNo.X, Homepos.WaferSwitch.X);
                        Common.motion.PosMove(Mo.AxisNo.Y, Homepos.WaferSwitch.Y);
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
                    if (EFEM.IsInit)
                    {
                        if (MachineType == 0)
                        {
                            if (Common.io.In(IOName.In.Wafer汽缸_抬起檢) && !Common.EFEM.Stage1.Measuredone)
                            {
                                if (Common.io.In(IOName.In.StageWafer在席) && Common.EFEM.Stage1.WaferPresence)
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
                            if (Common.io.In(IOName.In.StageWafer在席))
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
                        if (MachineType == 0)
                        {
                            //|| fram.m_simulateRun != 0
                            if (Common.io.In(IOName.In.Wafer汽缸_抬起檢))
                            {
                                //if (Common.io.In(IOName.In.StageWafer在席) && !Common.EFEM.Robot.WaferPresence_Upper)
                                if (Common.io.In(IOName.In.StageWafer在席)) // 把EFEM分離
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
                    if (MachineType == 0)
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
                    if (MachineType == 0)
                    {
                        SpinWait.SpinUntil(() => Common.io.In(IOName.In.Wafer汽缸_降下檢), 10000);
                        SpinWait.SpinUntil(() => Common.io.In(IOName.In.真空平台_負壓檢), 10000);
                        if (Common.io.In(IOName.In.真空平台_負壓檢) && Common.io.In(IOName.In.Wafer汽缸_降下檢))
                        {
                            ReportData.Lot = FoupID;
                            ReportData.Slot = Slot;
                            if (Measure_TrimType_On)
                            {
                                Common.LJ.ModeInfo();

                                if (Common.LJ.LJ_Mode == Common.LJ.LJMode.Setting_Mode)
                                {
                                    Common.LJ.Set_RunningMode();
                                }
                                Common.LJ.Reset();   // 每個lot結束後把量測資料夾reset
                                Common.LJ.Measure(); // 馬上拍一個避免抓不到最新的資料夾
                                AutoTrimStep = TrimStep.LJHome;
                                AutoRunStageStep = AutoStep.TrimMode;
                            }
                        }
                        else if (!Common.io.In(IOName.In.Wafer汽缸_降下檢))
                        {
                            Flag.AlarmFlag = true;
                            Flag.EQAlarmReportFlag = true; // True是可以report
                            InsertLog.SavetoDB(150);
                            Console.WriteLine("Wafer汽缸_降下檢 Time Out");
                        }
                        else if (!Common.io.In(IOName.In.真空平台_負壓檢))
                        {
                            Flag.AlarmFlag = true;
                            Flag.EQAlarmReportFlag = true; // True是可以report
                            InsertLog.SavetoDB(152);
                            Console.WriteLine("真空平台_負壓檢 Time Out");
                        }
                        else
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
                            if (Measure_TrimType_On)
                            {
                                Common.LJ.ModeInfo();

                                if (Common.LJ.LJ_Mode == Common.LJ.LJMode.Setting_Mode)
                                {
                                    Common.LJ.Set_RunningMode();
                                }
                                Common.LJ.Reset();   // 每個lot結束後把量測資料夾reset
                                Common.LJ.Measure(); // 馬上拍一個避免抓不到最新的資料夾
                                AutoTrimStep = TrimStep.LJHome;

                                AutoRunStageStep = AutoStep.TrimMode;
                            }
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
                        if (fram.m_MachineType == 0 && sram.Recipe.Type == 0) // AP62的recipe若選擇 0(藍膜)， LJ直接bypass
                        {
                            Measure_TrimType_Done = false;
                            AutoBlueTapeStep = BluetapeStep.Finish;
                            AutoRunStageStep = AutoStep.BlueTapeMode;
                        }
                        else
                        {
                            Measurement_TrimType_LJ();

                            // 完成之後跳下一個量測模式
                            if (Measure_TrimType_Done)
                            {
                                Measure_TrimType_Done = false;
                                if (fram.m_MachineType == 0 && sram.Recipe.Type == 3)
                                {
                                    AutoRunStageStep = AutoStep.TrimMode2nd;
                                }
                                else if (fram.m_MachineType == 0 && sram.Recipe.Type > 0)
                                {
                                    AutoRunStageStep = AutoStep.TTVMode;
                                }
                                else
                                {
                                    AutoBlueTapeStep = BluetapeStep.Finish;
                                    AutoRunStageStep = AutoStep.BlueTapeMode;
                                }
                            }
                        }
                    }
                    else
                    {
                        AutoRunStageStep = AutoStep.BlueTapeMode;
                    }
                    break;

                case AutoStep.TrimMode2nd:
                    if (Measure_TrimType_On)
                    {
                        if (fram.m_MachineType == 0 && sram.Recipe.Type == 0) // AP62的recipe若選擇 0(藍膜)， LJ直接bypass
                        {
                            Measure_TrimType2nd_Done = false;
                            AutoBlueTapeStep = BluetapeStep.Finish;
                            AutoRunStageStep = AutoStep.BlueTapeMode;
                        }
                        else
                        {
                            Measurement_TrimType_PT();

                            // 完成之後跳下一個量測模式
                            if (Measure_TrimType2nd_Done)
                            {
                                Measure_TrimType2nd_Done = false;
                                if (fram.m_MachineType == 0 && sram.Recipe.Type > 0)
                                {
                                    AutoRunStageStep = AutoStep.TTVMode;
                                }
                                else
                                {
                                    AutoBlueTapeStep = BluetapeStep.Finish;
                                    AutoRunStageStep = AutoStep.BlueTapeMode;
                                }
                            }
                        }
                    }
                    else
                    {
                        AutoRunStageStep = AutoStep.BlueTapeMode;
                    }
                    break;

                case AutoStep.BlueTapeMode:
                    if (Measure_BlueTape_On)
                    {
                        Measure_BlueTape_Done = false;
                        Measurement_BlueTape_CCD();
                        if (Measure_BlueTape_Done)
                        {
                            AutoTTVStep = TTVStep.Finish;
                            AutoRunStageStep = AutoStep.TTVMode;
                        }
                    }
                    else
                    {
                        AutoTTVStep = TTVStep.Finish;
                        AutoRunStageStep = AutoStep.TTVMode;
                    }
                    break;

                case AutoStep.TTVMode:
                    if (Measure_TTV_On)
                    {
                        Measure_TTV_Done = false;
                        Measurement_TTV_SF3();
                        if (Measure_TTV_Done)
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
                    if (MachineType == 0) // AP6 直接解真空
                    {
                        AutoRunStageStep = AutoStep.VacuumOff;
                        if(fram.m_Hardware_PT == 1 && Common.PTForm.GetPointMoveFinish(9))
                        {
                            Common.PTForm.PointMove(9);
                        }
                    }
                    else if (MachineType == 1) // N2 X Y Theta 軸也要回原點
                    {
                        if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y))
                        {
                            Common.motion.PosMove(Mo.AxisNo.DD, 0);
                            Common.motion.PosMove(Mo.AxisNo.X, Homepos.WaferSwitch.X);
                            Common.motion.PosMove(Mo.AxisNo.Y, Homepos.WaferSwitch.Y);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 20000);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.X), 10000);
                            SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.Y), 10000);
                            if (Common.motion.MotionDone(Mo.AxisNo.DD) && Common.motion.MotionDone(Mo.AxisNo.X) && Common.motion.MotionDone(Mo.AxisNo.Y) && Common.io.In(IOName.In.Wafer取放原點檢))
                            {
                                AutoRunStageStep = AutoStep.VacuumOff; // 所有軸都回去了 才能解真空
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
                    else if (MachineType == 1)
                    {
                        Common.io.WriteOut(IOName.Out.平台真空電磁閥, false);
                        Common.io.WriteOut(IOName.Out.平台破真空電磁閥, true);

                        AutoRunStageStep = AutoStep.CylinderUp;
                    }
                    break;

                case AutoStep.CylinderUp:
                    if (MachineType == 0)
                    {
                        if (fram.m_Hardware_PT == 1)
                        {
                            if (Common.PTForm.GetPointMoveFinish(9))
                            {
                                Common.io.WriteOut(IOName.Out.Wafer汽缸_降下, false);
                                Common.io.WriteOut(IOName.Out.Wafer汽缸_抬起, true);
                                SpinWait.SpinUntil(() => (Common.io.In(IOName.In.Wafer汽缸_抬起檢)), 5000);
                            }
                            else
                            {
                                Common.PTForm.PointMove(9);
                                SpinWait.SpinUntil(() => Common.PTForm.GetPointMoveFinish(9), 2000);
                                break;
                            }
                        }
                        else
                        {
                            Common.io.WriteOut(IOName.Out.Wafer汽缸_降下, false);
                            Common.io.WriteOut(IOName.Out.Wafer汽缸_抬起, true);
                            SpinWait.SpinUntil(() => (Common.io.In(IOName.In.Wafer汽缸_抬起檢)), 5000);
                        }

                        if (Common.io.In(IOName.In.Wafer汽缸_抬起檢))
                        {
                            Common.io.WriteOut(IOName.Out.StageWaferReady, true);
                            AutoRunStageStep = AutoStep.WaitWaferunload;
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
                    }

                    break;

                case AutoStep.WaitWaferunload:
                    if (EFEM.IsInit) // 這裡做完後等著被拿走
                    {
                        Common.EFEM.Stage1.Ready = true;
                        Common.EFEM.Stage1.Measuredone = true;
                    }

                    if (!Common.io.In(IOName.In.StageWafer在席) && !Common.EFEM.Stage1.WaferPresence) // Common.EFEM.Stage1.WaferPresence => WagetGet完後會自動切
                    {
                        if (MachineType == 0)
                        {
                            Common.motion.SetHome(Mo.AxisNo.DD);
                        }
                        else if (MachineType == 1)
                        {
                        }
                        Common.io.WriteOut(IOName.Out.StageWafer在席, false);

                        AutoRunStageStep = AutoStep.Finish;
                    }
                    break;

                case AutoStep.Finish:

                    AutoRunStageStep = AutoStep.GotoSwitchWaferPos;
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
    }
}