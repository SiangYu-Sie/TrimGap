//#define test
#define release

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.ComponentModel;
using System.Drawing;

namespace TrimGap
{
    public enum EFEMStep : int
    {
        [Description("等待E84")]
        WaitE84Load,

        [Description("等待LoadPort")]
        WaitLoadPort,

        [Description("ClampAndRead")]
        ClampAndRead,

        [Description("Load")]
        Load,

        [Description("Mapping Slot")]
        Map,

        [Description("判斷要做哪一步")]
        JudgeStep,

        [Description("至LoadPort取Wafer")]
        WaferGetFormLoadPort,

        [Description("至Aligner1取Wafer")]
        WaferGetFormAligner1,

        [Description("至Stage1取Wafer")]
        WaferGetFormStage1,

        [Description("至LoadPort放Wafer")]
        WaferPut2LoadPort,

        [Description("至Aligner1放Wafer")]
        WaferPut2Aligner1,

        [Description("至Stage1放Wafer")]
        WaferPut2Stage1,

        [Description("DoAlignement")]
        DoAlignement,

        [Description("Foup Unload")]
        Unload,

        [Description("E84Unload")]
        E84Unload,

        [Description("UpdateSecs")]
        UpdateSecs,

        [Description("ErrorCheckSts")]
        ErrorCheckSts,

        [Description("Finish")]
        Finish,
    }

    public enum Machine_Status_light
    {
        Green,
        Yellow,
        Red,
    }

    public enum ManualMode : int
    {
        Normal,
        ManualStop,
        ManualStopAnalysis,
        TriggerStop,
        TriggerStopAnalysis,
    }

    public enum PortTransferState
    {
        OutOfService = 0,    // ini
        TransferBlocked = 1, // ini
        ReadyToLoad = 2,     // Mir
        ReadyToUnload = 3,   // Mor
    }

    public enum Mode
    {
        [Description("手動")]
        Manual,

        [Description("自動")]
        Auto,

        SystemVibration,
    }

    public enum permissionEnum : int
    {
        [Description("Op")]
        op = 0, //作業員

        [Description("Eng")]
        eng = 1,//工程師

        [Description("Ad")]
        ad = 2, //管理員

        //op = 0, //作業員
        //te = 1, //技術員
        //eng = 2,//工程師
        //ad = 3, //管理員
        //tl = 4, //大量科技
    }

    public struct Flag
    {
        public static bool FormOpenFlag = true;
        public static bool FormRecipeOpenFlag = false;

        // EFEM
        public static bool EFEMConnectFlag = false;

        public static bool EFEMStepDoneFlag = true;
        public static bool AllHomeFlag = false;
        public static bool AllHome_busyFlag = false;
        public static bool EFEMAlarmReportFlag = false;

        //Autorun
        public static bool AutoidleFlag = false;

        public static bool Autoidle_LocalFlag = false;

        // ChangeAuthority
        public static bool ChangeAuthority = true;

        public static bool IO_Trigger_Measure = false;

        // autorun的trigger訊號
        public static bool IO_VirFlag = false;

        // autorun的trigger訊號
        public static bool IO_VirTrigger_Measure = false;

        //IO
        public static bool IOFlag = true;

        // Remotrfile
        public static bool RemoteFileConnectFlag = false;

        // SecsGem
        public static bool SECSGEM_InitSuccessFlag = false;

        // Sensor
        public static bool SensorConnectFlag = false;

        public static bool SensorConnectReportFlag = true;
        public static bool SensorMaterialChangeFlag = false;
        public static bool VirSensorFlag = false;
        public static bool SensorAnalysisFlag = false;
        public static bool SaveChartFlag = true;
        public static bool SignalPlotFlag = false;
        //io 是否連線

        // alarm
        public static bool AlarmFlag = false;

        public static bool EQAlarmReportFlag = false;
        public static bool AlarmOpenFlag = false;

        public static bool isPT = false;

        //進行Abort流程
        public static bool AbortFlag = false;

        //PJ/CJ
        public static bool FormPJOpenFlag = false;
    }

    //需記憶參數
    public struct fram
    {
        public static string ad_password;

        public static string eng_password;

        // [Sensor LJ]
        public static string LJ_ipaddress_FTP; // 拆完存到下面

        //public static byte[] ipAddress_FTP = new byte[4];
        public static string LJ_User_FTP;

        public static string LJ_Password_FTP;
        public static string LJ_ipaddress_Controller; // 拆完存到下面

        //public static byte[] ipAddress_Controller = new byte[4];
        public static int LJ_portNo_Controller;

        public static string LJ_FTPfilePath;

        // [UserPermission]
        public static string op_password;

        // [Product]
        public static string P_LotName = "";

        public static string P_Note = "";

        public static string P_Result = "";

        public static int P_Slot = 0;

        public static string P_TaskID = "";

        public static float P_Thickness = 0;

        public static float P_Tolerance = 0;

        public static int P_WaferSize = 0;

        public static string R_RemoteFilePassword;

        // [System]
        public static int S_SECSGEM_Use;
        public static int m_simulateRun;
        public static string S_IOCardName;
        public static int S_SensorConnectType;  // 0:FTP, 1:LJX8IF
        public static string S_MotionRotate;
        public static string S_Sensorbypass;
        public static int m_MachineType;  // 0:AP6, 1:N2, 2:AP6II 
        public static int m_MotionType;   // 0:SSC, 1:ETEL
        public static int m_Hardware_LJ;  // 0:None 1:all type
        public static int m_Hardware_SF3; // 0:None 1:all type
        public static int m_WaferStageType; // 0:平坦台面+氣缸頂升  1:凹槽台面+牙叉抬升
        public static int m_Hardware_CCD; // 0:None 1:藍膜Z向拍照
        public static int m_Hardware_PT;  // 0:None 1:all type
        public static int m_Hardware_HTW;  // 0:None 1:all type
        public static int m_SecsgemType;  // 0:創界 1:台達

        public static int m_WaferAlignAngle;  // WaferAlignAngle
        public static int m_WaferBackToFoupAngle;  // WaferBackToFoupAngle 所以最後回到FOUP的角度會是WaferAlignAngle+WaferBackToFoupAngle

        // [motion]
        public static string m_MotionParamPath;

        public static double[] m_Acc = new double[5];            // Acc
        public static double[] m_Dec = new double[5];            // Dec
        public static double[] m_posV = new double[5];            // 自動對焦速度
        public static double[] m_pitch = new double[5];            // pitch距離
        public static double[] m_pitchV = new double[5];            // pitch速度
        public static double[] m_jogV = new double[5];            // jov速度
        public static double[] m_unit = new double[5];            // 單位

        public static double[] m_CurrentV = new double[5];        // 目前速度
        public static double[] m_homeV = new double[5];      // homeoffset
        public static int[] m_SoftLimit_SPEL = new int[5];      // Soft Limit SPEL
        public static int[] m_SoftLimit_SMEL = new int[5];      // Soft Limit SMEL
        public static int[] m_SoftLimit_SPEL_Flag = new int[5]; // Soft Limit SPEL Flag
        public static int[] m_SoftLimit_SMEL_Flag = new int[5]; // Soft Limit SMEL Flag

        // [Position]
        public struct Position
        {
            public static double LJ_Z;
            public static double RecordCCD_Z;
            public static double HTW_P1_X;
            public static double HTW_P1_Z;
            public static double HTW_P2_X;
            public static double HTW_P2_Z;
            public static int HTW_P1_FocusRange; // 搜尋次數，每次上升10um，看要找幾次
        }

        // [Sensor]
        public static int S_ShowDataNum;

        public static int S_SensorProgram;

        // [PLC]
        public struct PLC
        {
            public static int ActCpuType;
            public static int ActUnitType;
            public static int ActProtocolType;
            public static int ActStationNumber;
            public static int ActIONumber;
            public static int ActTimeOut;
            public static string ActHostAddress;
            public static int ActDestinationPortNumber;
        }

        public static int PT_PLC_AutoRunStage_RetryCount;
        public static int PT_PLC_AutoRunEFEM_RetryCount;

        public static int HTW_Autofocus_Index;

        // 量測開始延遲時間
        //   192//1//10//16 用// 拆字串
        // [Analysis]
        public struct Analysis
        {
            public static double Coefficient;
            public static int BlueTapeThreshold;
            public static double PitchAngle;
            public static int RotateCount;
            public static int Step2_Range_step1x0; // 2階產品第一刀
            public static int Step2_Range_step1x1;
            public static int Step2_Range_step2x0; // 2階產品第二刀
            public static int Step2_Range_step2x1;
            public static int Step1_Range_step1x0; // 1階產品第一刀
            public static int Step1_Range_step1x1;
            public static int Range1_Percent; // 基準範圍 5%
            public static int Range2_Percent; // 基準範圍 15%
            public static double LJ_StandardPlane; // LJ基準面
            public static double Offset1StepH; // 1階產品 offset H
            public static double Offset1StepW; // 1階產品 offset W
            public static double Offset2StepH1; // 2階產品 offset H1
            public static double Offset2StepW1; // 2階產品 offset W1
            public static double Offset2StepH2; // 2階產品 offset H2
            public static double Offset2StepW2; // 2階產品 offset W2
            public static double Offset_PT_1StepH; // 1階產品 PT offset H
            public static double Offset_PT_1StepW; // 1階產品 PT offset W
            public static double Offset_PT_2StepH1; // 2階產品 PT offset H1
            public static double Offset_PT_2StepW1; // 2階產品 PT offset W1
            public static double Offset_PT_2StepH2; // 2階產品 PT offset H2
            public static double Offset_PT_2StepW2; // 2階產品 PT offset W2
            public static double OffsetBlueTapeW; // 藍膜 offset W
            public static double Offset_EDGE_1StepW; // 1階產品 EDGE offset W
            public static double Offset_EDGE_2StepW1; // 2階產品 EDGE offset W1

            public static double HTW_StandardPlane; // HTW基準面

            public struct Offset
            {
                public static double Inline_1StepH;  // Inline 1階產品 offset H
                public static double Inline_1StepW;  // Inline 1階產品 offset W
                public static double Inline_2StepH1;  // Inline 2階產品 offset H1
                public static double Inline_2StepW1;  // Inline 2階產品 offset W1
                public static double Inline_2StepH2;  // Inline 2階產品 offset H2
                public static double Inline_2StepW2;  // Inline 2階產品 offset W2
                public static double Inline_PT_1StepH;  // Inline PT 1階產品 offset H
                public static double Inline_PT_1StepW;  // Inline PT 1階產品 offset W
                public static double Inline_PT_2StepH1;  // Inline PT 2階產品 offset H1
                public static double Inline_PT_2StepW1;  // Inline PT 2階產品 offset W1
                public static double Inline_PT_2StepH2;  // Inline PT 2階產品 offset H2
                public static double Inline_PT_2StepW2;  // Inline PT 2階產品 offset W2
                public static double Inline_BlueTapeW; // Inline 藍膜 offset W
                public static double Inline_EDGE_1StepW;  // Inline EDGE 1階產品 offset W
                public static double Inline_EDGE_2StepW1;  // Inline EDGE 2階產品 offset W1
                public static double Offline_1StepH; // Offline 1階產品 offset H
                public static double Offline_1StepW; // Offline 1階產品 offset W
                public static double Offline_2StepH1; // Offline 2階產品 offset H1
                public static double Offline_2StepW1; // Offline 2階產品 offset W1
                public static double Offline_2StepH2; // Offline 2階產品 offset H2
                public static double Offline_2StepW2; // Offline 2階產品 offset W2
                public static double Offline_PT_1StepH; // Offline PT 1階產品 offset H
                public static double Offline_PT_1StepW; // Offline PT 1階產品 offset W
                public static double Offline_PT_2StepH1; // Offline PT 2階產品 offset H1
                public static double Offline_PT_2StepW1; // Offline PT 2階產品 offset W1
                public static double Offline_PT_2StepH2; // Offline PT 2階產品 offset H2
                public static double Offline_PT_2StepW2; // Offline PT 2階產品 offset W2
                public static double Offline_BlueTapeW; // Offline 藍膜 offset W
                public static double Offline_EDGE_1StepW;  // Offline EDGE 1階產品 offset W
                public static double Offline_EDGE_2StepW1;  // Offline EDGE 2階產品 offset W1
                public static double[] QC_1StepH = new double[8];  // QC 1階產品 offset H
                public static double[] QC_1StepW = new double[8];  // QC 1階產品 offset W
                public static double[] QC_2StepH1 = new double[8];  // QC 2階產品 offset H1
                public static double[] QC_2StepW1 = new double[8];  // QC 2階產品 offset W1
                public static double[] QC_2StepH2 = new double[8];  // QC 2階產品 offset H2
                public static double[] QC_2StepW2 = new double[8];  // QC 2階產品 offset W2
                public static double[] QC_PT_1StepH = new double[8];  // QC PT 1階產品 offset H
                public static double[] QC_PT_1StepW = new double[8];  // QC PT 1階產品 offset W
                public static double[] QC_PT_2StepH1 = new double[8];  // QC PT 2階產品 offset H1
                public static double[] QC_PT_2StepW1 = new double[8];  // QC PT 2階產品 offset W1
                public static double[] QC_PT_2StepH2 = new double[8];  // QC PT 2階產品 offset H2
                public static double[] QC_PT_2StepW2 = new double[8];  // QC PT 2階產品 offset W2
                public static double[] QC_BlueTapeW = new double[8]; // QC 藍膜 offset W
                public static double[] QC_EDGE_1StepW = new double[8];  // QC EDGE 1階產品 offset W
                public static double[] QC_EDGE_2StepW1 = new double[8];  // QC EDGE 2階產品 offset W1
            }
        }

        public struct SECSPara
        {
            public static int Loadport1_AccessMode;
            public static int Loadport2_AccessMode;
            public static int Loadport1_PortTransferState;
            public static int Loadport2_PortTransferState;
        }

        public struct EFEMSts
        {
            public static int TotalCompletedCount;
            public static int AlarmSts;
            public static string StepBack1;
            public static string StepBack2;
            public static string LoadPortRun;
            public static string LoadPort1_FoupID;
            public static string LoadPort2_FoupID;
            public static int Robot_Upper_Slot;
            public static string Robot_Upper_Slotpn;
            public static int Robot_Lower_Slot;
            public static string Robot_Lower_Slotpn;
            public static int Aligner_Slot;
            public static string Aligner_Slotpn;
            public static int Stage_Slot;
            public static string Stage_Slotpn;
            public static string[] Slot_Sts1 = new string[25];
            public static string[] Slot_Sts2 = new string[25];
            public static double[,] H1 = new double[25, 8];
            public static double[,] H2 = new double[25, 8];
            public static double[,] W1 = new double[25, 8];
            public static double[,] W2 = new double[25, 8];
            public static int Skip; //0:正常 1:略過EFEM 2:自我檢測用
        }

        public static RecipeFormat Recipe = new RecipeFormat();
    }

    public struct ReportData
    {
        public static DateTime Time = DateTime.Now;
        public static string Lot = "";
        public static int Slot = 0;
        public static double H1 = 0;
        public static double W1 = 0;
        public static double H2 = 0;
        public static double W2 = 0;
        public static int Chipping = 0;
        public static double[] TTVData;
        public static double TTVDataValue;
        public static double TTVMax;
        public static double TTVMin;
        public static double TTVMean;

        public static void ClearResult()
        {
            H1 = 0;
            W1 = 0;
            H2 = 0;
            W2 = 0;
            Chipping = 0;
        }

        public static void ClearAll()
        {
            Lot = "";
            Slot = 0;
            H1 = 0;
            W1 = 0;
            H2 = 0;
            W2 = 0;
            Chipping = 0;
        }
    }

    public struct AnalysisData
    {
        public static bool rtn;
        public static double Interval_X = 2.5;
        public static int um2mm = 1000;
        public static long FtpFileSize = 28000; //檔案大小超過這個才可以下載
        public static string LastFilePath; //檔案大小超過這個才可以下載
        public static int rotateCount_current = 0;
        public static bool[] WaferMeasure = new bool[8];
        public static double[] rawData;
        public static double[] rawData2;
        public static double[] rawData3;
        public static double[] removezeroData;
        public static double[] removezeroData2;
        public static double[] removezeroData3;
        public static double[] tiltingdata_x;
        public static double[] tiltingdata_x2;
        public static double[] tiltingdata_x3;
        public static double[] tiltingdata_y;
        public static double[] tiltingdata_y2;
        public static double[] tiltingdata_y3;
        public static double[][] resultdata = new double[8][];
        public static double[][] resultdata2 = new double[8][];
        public static double[][] resultdata_blueW = new double[8][];
        public static double htw_gap;
        public static double htw_cut;
        public static double[] rawData_base;
        public static int htw_baselineIndex;
    }

    public struct AnalysisData2
    {
        public static bool rtn;
        public static double Interval_X = 2.5;
        public static int um2mm = 1000;
        public static long FtpFileSize = 28000; //檔案大小超過這個才可以下載
        public static string LastFilePath; //檔案大小超過這個才可以下載
        public static int rotateCount_current = 0;
        public static bool[] WaferMeasure = new bool[8];
        public static double[] rawData;
        public static double[] rawData2;
        public static double[] rawData3;
        public static double[] removezeroData;
        public static double[] removezeroData2;
        public static double[] removezeroData3;
        public static double[] tiltingdata_x;
        public static double[] tiltingdata_x2;
        public static double[] tiltingdata_x3;
        public static double[] tiltingdata_y;
        public static double[] tiltingdata_y2;
        public static double[] tiltingdata_y3;
        public static double[][] resultdata = new double[8][];
        public static double[][] resultdata2 = new double[8][];
        public static double[][] resultdata_blueW = new double[8][];
    }



    public struct SignalPlotData
    {
        public static double[] rawData;
        public static double[] rawData2;
        public static double[] rawData3;
        public static double[] removezeroData;
        public static double[] removezeroData2;
        public static double[] removezeroData3;
        public static double[] tiltingdata_x;
        public static double[] tiltingdata_x2;
        public static double[] tiltingdata_x3;
        public static double[] tiltingdata_y;
        public static double[] tiltingdata_y2;
        public static double[] tiltingdata_y3;
        public static double[] resultdata;
        public static double htw_gap;
        public static double htw_cut;
        public static double[] rawData_base;
        public static int htw_baselineIndex;
    }


    public struct TTVData
    {
        public static int rotateCount_current = 0;
        public static int shiftCount_current = 0;
        public static List<int> lsRotate;
        public static List<Point> lsShift;
    }

    // 暫存參數
    public struct sram
    {
        //版本號
        public static string ProgramVersion;
        public static string AnalysisVersion;

        public static double PitchAngle = 0;
        public static double PitchAngleTotal = 0;
        public static EFEMStep EFEMStep1 = EFEMStep.WaitLoadPort; //預設等待loadport
        public static EFEMStep EFEMStep2 = EFEMStep.WaitLoadPort; //預設等待loadport
        public static EFEMStep EFEMStep_Back1 = EFEMStep.WaitLoadPort; //預設等待loadport，一進去開始run就會切
        public static EFEMStep EFEMStep_Back2 = EFEMStep.WaitLoadPort; //預設等待loadport，一進去開始run就會切
        public static string dirfilepath = System.Windows.Forms.Application.StartupPath;

        // 機台狀態
        public static int Machine_status;

        public static int ManualMode = 0;

        public static int Manualstep = 0;

        // Mode
        public static int Mode = 0;

        // 當前目錄(exe) D:\CMPPadMetrology\WorkDirectory\Run\
        public static System.IO.DirectoryInfo root = System.IO.Directory.GetParent(sram.dirfilepath);

        //D:\FilmThickness\WorkDirectory\
        public static System.IO.DirectoryInfo root2 = System.IO.Directory.GetParent(root.FullName);

        //參數檔路徑

        public static string DataBasePath = root2 + "\\ParameterDirectory\\TrimGapMainDB";
        //public static string DataBasePath = "E:\\TL-Chen\\C# project file\\BWM-122\\FilmThickness\\bin\\ParameterDirectory\\FilmMainDB";

        public static string ErrorCodeCsvPath = root2 + "\\ParameterDirectory\\param\\NoMap.csv";
        public static string Recipe_Path = "";

        //D:\FilmThickness\
        public static string Rootfilepath = root2.FullName;

        public static string ParamPath = Rootfilepath + @"\ParameterDirectory\param\";
        public static DateTime saveDataTime;

        //D:\FilmThickness\
        public static string saveinifilepath;

        public static TimeSpan SensorMeasureTime;

        // 記錄量測時間秒數
        public static int SensorSamplingRate;

        // Sensor
        public static DateTime SensorStartTime;

        public static DateTime SensorStopTime;

        // 軟體版本
        public static string SofewareVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName).FileVersion.ToString();

        // 存檔/圖 時間紀錄
        public static DateTime SavedataTime;

        // 權限
        public static permissionEnum UserAuthority;

        public static int PTRetry;

        public static short[] SpectrumMaxValue;  //儲存spectrum最大值
        public static int[] SpectrumMaxValueBias;  //儲存spectrum最大值距離中間的偏差值。例如70-80內的最大值在72，則記錄+3，到時候對焦的位置要額外+3
        public static double Focus_Offset; //總對焦Z補正值

        //
        public static RecipeFormat Recipe = new RecipeFormat();

        //GEM300
        public static string RunningCJ;
        public static string RunningPJ;
        public static List<string> QueuePJ;
        public static bool LoadPort1_Carrier_Vertify = false;
        public static bool LoadPort2_Carrier_Vertify = false;

        public struct CJInfo
        {
            public static string objID;
            public static string carrierInputSpec;
            public static string curPJ;
            public static string dataCollection;
            public static string mtrloutStatus;
            public static string mtrloutSpec;
            public static string pauseEvent;
            public static string procCtrlSpec;
            public static byte procOrder;
            public static bool bStart;
            public static byte state;
        }

        public struct PJInfo
        {
            public static string objID;
            public static string pauseEvent;
            public static byte PJState;
            public static string carrierID;
            public static byte[] slot;
            public static byte PRType;
            public static bool bStart;
            public static byte recMethod;
            public static string recID;
            public static string recVarList;
        }
        // [Recipe]
        /*public struct Recipe
        {
            // 刷完Recipe後要進來這裡更新
            public static string Filename = "";

            public static int[] Slot = new int[25];
            public static int[] Angle = new int[8];
            public static int Rotate_Count;
            public static int Type;
            public static int OffsetType;
            public static int RepeatTimes;
            public static int RepeatTimes_now;
            public static List<int> TTVrotatePosition;
            public static List<Point> TTVshiftPosition;
            public static int WaferEdgeEvaluate;
        }*/
    }

    // [Recipe]
    public class RecipeFormat
    {
        public string Path = "";
        public string FilenameSelect = "";
        public string Filename_LP1 = "";
        public string Filename_LP2 = "";

        //----------------ini-------------------拿來刷recipe management
        public string Filename = "";

        public int[] Slot = new int[25];
        public int[] Angle = new int[8];
        public int Rotate_Count;
        public int Type;
        public int OffsetType;
        public int RepeatTimes;
        public int RepeatTimes_now;
        public string CreateTime;
        public string ReviseTime;
        public string MotionPatternName = "";
        public string MotionPatternPath = "";
        public string SF3_ID = "";
        public string SF3_Name = "";
        public List<int> TTVrotatePosition;
        public List<Point> TTVshiftPosition;
        public int WaferEdgeEvaluate;

        public int BlueTapeThreshold;
        public int Step2_Range_step1x0; // 2階產品第一刀
        public int Step2_Range_step1x1;
        public int Step2_Range_step2x0; // 2階產品第二刀
        public int Step2_Range_step2x1;
        public int Step1_Range_step1x0; // 1階產品第一刀
        public int Step1_Range_step1x1;
        public int Range1_Percent; // 基準範圍 5%
        public int Range2_Percent; // 基準範圍 15%

        //Record CCD
        public int RecordCCDRule;   // 0:Position 1:Pitch
        public int RecordCCD_Angle_Start;
        public int RecordCCD_Angle_End;
        public int RecordCCD_Angle_Pitch;

    }

    public struct UI
    {
        public static PictureBox[] pbWafer = new PictureBox[8];
    }
}