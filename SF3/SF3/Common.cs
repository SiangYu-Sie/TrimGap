using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otsuka
{
    public class Common
    {
        public static int SwTimer = 5;//秒數

        //CmdSetDateTime Parameter - 10002
        public static string CmdSetDateTime_DataTime = "20230201125959";//14 ASCII letters(yyyyMMddHHmmss)
        //CmdMeasReference Parameter - 10003
        public static int CmdMeasReference_ExposureTime = 6;//4 bytes int(6 - 20000000)
        public static int CmdMeasReference_NumOfAccumulations = 1;//4 bytes int(1 - 65535)
        //CmdSetReference Parameter - 10004
        public static int CmdSetReference_ExposureTime = 6;//4 bytes int(6 - 20000000)
        public static int CmdSetReference_NumOfAccumulations = 1;//4 bytes int(1 - 65535)
        public static int CmdSetReference_NumOfsignals = 0;//4 bytes int
        public static List<float> CmdSetReference_ReferenceDarkSpectrum = new List<float>();//4 bytes float × No. of signals
        public static List<float> CmdSetReference_ReferenceSignalSpectrum = new List<float>();//4 bytes float × No. of signals
        //CmdLoadRecipeID Parameter - 10009
        public static int CmdLoadRecipeID_RecipeID = 1;//4 bytes int(1 - 999)
        //CmdMeasSignal Parameter - 10012
        public static int CmdMeasSignal_ExposureTime = 6;//4 bytes int(6 - 20000000)
        public static int CmdMeasSignal_NumOfAccumulations = 1;//4 bytes int(1 - 65535)
        //CmdCtrlLight Parameter - 10016
        public static int CmdCtrlLight_enum = 0;
        public enum CmdCtrlLight
        {
            NotLight,
            Light,
        }
        //CmdReanalyze Parameter - 10019
        public static int CmdReanalyze_NumOfReflectances = 0;//4 bytes int
        public static List<float> CmdReanalyze_ReflectanceSpectrum = new List<float>();//4 bytes float x No.of reflectances
        //CmdSetTriggerEdge Parameter - 20005
        public static int CmdSetTriggerEdge_enum = 0;
        public enum CmdSetTriggerEdge : int
        {
            Rising = 0,
            Falling = 1
        }
        //CmdSetTriggerFilter Parameter - 20007
        public static int CmdSetTriggerFilter_Time = 0;//4 bytes int(0 - 65535)
        //CmdSetUseAnalog Parameter - 20009
        public static int CmdSetUseAnalog_enum = 0;
        public enum CmdSetUseAnalog : int
        {
            NotUse = 0,
            Use = 1
        }
        //CmdSetAnalogRange Parameter - 20011
        public static float CmdSetAnalogRange_LowerLimit = 0.0F;//4 bytes float(0.0 - 100000.0)
        public static float CmdSetAnalogRange_UpperLimit = 100000.0F;//4 bytes float(LowerLimits - 100000.0)
        //CmdSetIPAdress Parameter - 20013
        public static string IPAddress = "192,168,1,1,2000";//(0 - 255, 0 - 255, 0 - 255, 0 - 255, 0 - 65535)
        //CmdTestAnalog Parameter - 20014
        public static float CmdTestAnalog_Thickness = 0.0F;//4 bytes float(0.0 - 100000.0)
        //CmdSetCalibrateThickness Parameter - 20015
        public static float CmdSetCalibrateThickness_StandardThicknessValue1 = 0.0F;//4 bytes float(0.0 - 100000.0)
        public static float CmdSetCalibrateThickness_MeasurementThicknessValue1 = 0.0F;//4 bytes float(0.0 - 100000.0)
        public static float CmdSetCalibrateThickness_StandardThicknessValue2 = 0.0F;//4 bytes float(0.0 - 100000.0)
        public static float CmdSetCalibrateThickness_MeasurementThicknessValue2 = 0.0F;//4 bytes float(0.0 - 100000.0)
        //CmdSetProtocol Parameter - 20017
        public static int CmdSetProtocol_enum = 0;
        public enum CmdSetProtocol : int
        {
            HandshakeType = 0,
            EventType = 1
        }
        //CmdSetTriggerResetTime Parameter - 30002
        public static int CmdSetTriggerResetTime_enum = 0;
        public enum CmdSetTriggerResetTime : int
        {
            NotReset = 0,
            Reset = 1
        }
        //CmdSetResultType Parameter - 30004
        public static int CmdSetResultType_enum = 0;
        public enum CmdSetResultType : int
        {
            Time_SignalMaximum_Thickness_Only = 0,
            Signal = 1,
            Reflectance = 2,
            Signal_Reflectance = 3,
            AnalysisSpectra = 4,
            Signal_AnalysisSpectra = 5,
            Reflectance_AnalysisSpectra = 6,
            Signal_Reflectance_AnalysisSpectra = 7,
        }
        //CmdSetMeasMode Parameter - 30006
        public static int CmdSetMeasMode_enum = 0;
        public enum CmdSetMeasMode : int
        {
            Single_Measurement_Mode = 1,
            Continuous_Measurement_Mode = 2,
            Electrical_TriggerMeasurement_Mode = 3
        }
        //CmdSetMeasCount Parameter - 30008
        public static int CmdSetMeasCount_NumOfContinuousMeasurement = 1;//4 bytes int(1 - 999)
        //CmdSetMeasCycle Parameter - 30010
        public static int CmdSetMeasCycle_MeasurementCycle = 75;//4 bytes int(75 - 2147483647)(approximately 35 min)
        //CmdSetExposure Parameter - 30012
        public static int CmdSetExposure_ExposureTime = 6;//4 bytes int(6 - 20000000)
        //CmdSetAccumulation Parameter - 30014
        public static int CmdSetAccumulation_NumOfAccumulations = 1;//4 bytes int(1 - 65535)
        //CmdSetTriggerDelay Parameter - 30016
        public static int CmdSetTriggerDelay_TriggerDelayTime = 0;//4 bytes int(0 - 65535)
        //CmdSetAnalysisRange Parameter - 40001
        public static int CmdSetAnalysisRange_AnalysisStartWavelengthNum = 0;//4 bytes int(0 - 511)
        public static int CmdSetAnalysisRange_AnalysisEndWavelengthNum = 511;//4 bytes int(AnalysisStartWavelengthNum - 511)
        //CmdSetAnalysisMethod Parameter - 40003
        public static int CmdSetAnalysisMethod_enum = 0;
        public enum CmdSetAnalysisMethod : int
        {
            FFTUsingConstantRefractiveIndex = 0,
            FFTUsingMaterialRefractiveIndex = 1,
            OptimizationMethod = 2,
            FFTUsingMaterialRefractiveIndex_OptimizationMethod = 3
    }
        //CmdSetAnalysisNum Parameter - 40005
        public static int CmdSetAnalysisNum_NumOfAnalysisLayers = 1;//4 bytes int(1 - 5)
        //CmdSetSmoothing Parameter - 40007
        public static int CmdSetSmoothing_enum = 1;
        public enum CmdSetSmoothing : int
        {
            Point_1 = 1,
            Point_3 = 3,
            Point_5 = 5,
            Point_7 = 7,
            Point_9 = 9,
            Point_11 = 11,
            Point_13 = 13,
            Point_15 = 15,
            Point_17 = 17,
            Point_21 = 21,
            Point_23 = 23,
            Point_25 = 25
        }
        //CmdSetFFTNum Parameter - 40009
        public static int CmdSetFFTNum_enum = 128;
        public enum CmdSetFFTNum : int
        {
            Point_128 = 128,
            Point_256 = 256,
            Point_512 = 512,
            Point_1024 = 1024,
            Point_2048 = 2048,
            Point_4096 = 4096
        }
        //CmdSetFFTMaxThickness Parameter - 40011
        public static float CmdSetFFTMaxThickness_FFTMaxThickness = 0.0F;//4 bytes float(0.0 - 100000.0)
        //CmdSetFFTWindow Parameter - 40013
        public static int CmdSetFFTWindow_enum = 0;
        public enum CmdSetFFTWindow : int
        {
            NotUse = 0,
            Use = 1
        }
        //CmdSetFFTNormalization Parameter - 40015
        public static int CmdSetFFTNormalization_enum = 0;
        public enum CmdSetFFTNormalization : int
        {
            NotUse = 0,
            Use = 1
        }
        //CmdSetUsePeakJudge Parameter - 40017
        public static int CmdSetUsePeakJudge_enum = 0;
        public enum CmdSetUsePeakJudge : int
        {
            NotExclude = 0,
            Exclude = 1
        }
        //CmdSetAnalysisMaterial Parameter - 40019
        public static int CmdSetAnalysisMaterial_AnalysisLayerMaterialID = 1;//4 bytes int(1 - 999)
        //CmdSetAnalysisRefractiveIndex Parameter - 40021
        public static int CmdSetAnalysisRefractiveIndex_DataQuantity = 1;//4 bytes int(1 - 5)
        public static List<float> CmdSetAnalysisRefractiveIndex_ConstantRefractiveIndex = new List<float>();//4 bytes float(0 - 10)
        //CmdSetAnalysisThicknessRange Parameter - 40023
        public static int CmdSetAnalysisThicknessRange_DataQuantity = 1;//4 bytes int(1 - 5)
        public static List<float> CmdSetAnalysisThicknessRange_ThicknessRangeLowerLimit = new List<float>();//4 bytes float(0.0 - 100000.0)
        public static List<float> CmdSetAnalysisThicknessRange_ThicknessRangeUpperLimit = new List<float>();//4 bytes float(ThicknessRangeLowerLimit - 100000.0)
        //CmdSetFFTPowerRange Parameter - 40025
        public static int CmdSetFFTPowerRange_DataQuantity = 1;//4 bytes int(1 - 5)
        public static List<float> CmdSetFFTPowerRange_FFTIntensityRangeLowerLimit = new List<float>();//4 bytes float(0.0 - 10000.0)
        public static List<float> CmdSetFFTPowerRange_FFTIntensityRangeUpperLimit = new List<float>();//4 bytes float(FFTIntensityRangeLowerLimit - 10000.0)
        //CmdSetFFTSearchDirection Parameter - 40027
        public static int CmdSetFFTSearchDirection_DataQuantity = 1;//4 bytes int(1 - 5)
        public static List<int> CmdSetFFTSearchDirection_list = new List<int>();
        public static int CmdSetFFTSearchDirection_enum = 0;
        public enum CmdSetFFTSearchDirection_PeakSearchDirection : int
        {
            Left = 0,
            Top = 1,
            Right = 2,
            Bottom = 3
        }
        //CmdSetFFTPeakNum Parameter - 40029
        public static int CmdSetFFTPeakNum_DataQuantity = 1;//4 bytes int(1 - 5)
        public static List<int> CmdSetFFTPeakNum_PeakNumber = new List<int>();//4 bytes int(1 - 99)
        //CmdSetThicknessCoef Parameter - 40031
        public static int CmdSetThicknessCoef_DataQuantity = 1;//4 bytes int(1 - 5)
        public static List<int> CmdSetThicknessCoef_list = new List<int>();
        public static int CmdSetThicknessCoef_enum = 0;
        public enum CmdSetThicknessCoef : int
        {
            NotUse = 0,
            Use_InputValue = 1,
            Use_CalibratedValue = 2,
        }
        public static List<float> CmdSetThicknessCoef_PrimaryCoefficientC1 = new List<float>();//4 bytes float
        public static List<float> CmdSetThicknessCoef_ConstantCoefficientC0 = new List<float>();//4 bytes float
        //CmdSetOptimAmbient Parameter - 50001
        public static int CmdSetOptimAmbient_AmbientLayerMaterialID = 1;//4 bytes int(1 - 999)
        //CmdSetOptimSubstrate Parameter - 50003
        public static int CmdSetOptimSubstrate_SubstrateMaterialID = 1;//4 bytes int(1 - 999)
        public static float CmdSetOptimSubstrate_Thickness = 0.0F;//4 bytes float(0.0 - 100000.0)
        //CmdSetOptimLayerModel Parameter - 50005
        public static int CmdSetOptimLayerModel_NumOfLayers = 0;//4 bytes int(0 - 4)
        public static List<int> CmdSetOptimLayerModel_LayerMaterialID = new List<int>();//4 bytes int(1 - 999)
        public static List<float> CmdSetOptimLayerModel_Thickness = new List<float>();//4 bytes float(0.0 - 100000.0)
        //CmdSetOptimSwitchThickness Parameter - 50007
        public static float CmdSetOptimSwitchThickness_Thickness = 0.0F;//4 bytes float(0.0 - 100000.0)
        //CmdSetOptimThicknessStep Parameter - 50009
        public static float CmdSetOptimThicknessStep_ThicknessStep = 0.0F;//4 bytes float(0.0 - 100000.0)
        //CmdSetFollowUpFilter Parameter - 60001
        public static float CmdSetFollowUpFilter_FollowUpFilterPlusMinusRange = 0.0F;//4 bytes float(0.0 - 100000.0)
        //CmdSetFilterApplyTime Parameter - 60003
        public static int CmdSetFilterApplyTime_FollowUpFilterApplicationTime = 0;//4 bytes int(0 - 2147483647)(sec)
        //CmdSetFilterCenterMA Parameter - 60005
        public static int CmdSetFilterCenterMA_FollowUpFilterCenterValueMovingAverage = 1;//4 bytes int(1 - 99999)
        //CmdSetFilterTrend Parameter - 60007
        public static int CmdSetFilterTrend_enum = 0;
        public enum CmdSetFilterTrend : int
        {
            NoTrend = 0,
            Decreasing = 1,
        }
        //CmdSetThicknessMA Parameter - 60009
        public static int CmdSetThicknessMA_ThicknessMovingAverageFrequency = 1;//4 bytes int(1 - 99999)
        //CmdSetErrorVaule Parameter - 60011
        public static int CmdSetErrorVaule_ErrorVaule = 0;//4 bytes int(0 - 2147483647)
        //CmdSaveRecipeID Parameter - 70001
        public static int CmdSaveRecipeID_RecipeID = 1;//4 bytes int(1 - 999)
        public static string CmdSaveRecipeID_RecipeName = "";//ASCII
        //CmdDelRecipeID Parameter - 70003
        public static int CmdDelRecipeID_RecipeID = 1;//4 bytes int(1 - 999)
        //CmdSaveMaterial Parameter - 80001
        public static int CmdSaveMaterial_NumOfLettersOfMaterialName = 0;//4 bytes int(0 - 100)
        public static int CmdSaveMaterial_DataQuantity = 2;//4 bytes int(2 - 3000)
        public static int CmdSaveMaterial_MaterialID = 1;//4 bytes int(1 - 999)
        public static string CmdSaveMaterial_MaterialName = "";//ASCII
        public static List<float> CmdSaveMaterial_Wavelength = new List<float>();//4 bytes float(100.0 - 3000.0)
        public static List<float> CmdSaveMaterial_RefractiveIndex = new List<float>();//4 bytes float
        public static List<float> CmdSaveMaterial_ExtinctionCoefficient = new List<float>();//4 bytes float
        //CmdGetMaterial Parameter - 80002
        public static int CmdGetMaterial_MaterialID = 1;//4 bytes int(1 - 999)
        //CmdDelMaterial Parameter - 80004
        public static int CmdDelMaterial_MaterialID = 1;//4 bytes int(1 - 999)

        public enum Command : int
        {
            //Measurement/Analysis Command
            CmdReady = 10001,//確認通訊狀態
            CmdSetDateTime = 10002,//調整日期和時間
            CmdMeasReference = 10003,//參考測量
            CmdSetReference = 10004,//參考設置
            CmdGetReference = 10005,//獲取參考
            CmdUpdateReference = 10006,//更新設備參考
            CmdLoadReference = 10007,//讀取設備參考
            CmdLoadDefaultRecipe = 10008,//讀取默認配方
            CmdLoadRecipeID = 10009,//讀取指定的配方
            CmdMeasDark = 10010,//測量樣品黑暗
            CmdMakeSimulationData = 10011,//為優化方法創建分析數據
            CmdMeasSignal = 10012,//信號測量
            CmdMeasStart = 10013,//開始測量
            CmdMeasStop = 10014,//停止測量
            CmdGetResult = 10015,//取得結果
            CmdCtrlLight = 10016,//燈光控制
            CmdGetStatus = 10017,//獲取最新狀態
            CmdGetFFTPowerXAxis = 10018,//獲取功率譜橫軸值
            CmdReanalyze = 10019,//重新分析厚度

            //System Setting Command
            CmdUpdateAll = 20001,//更新所有設備設置
            CmdLoadDefaultAll = 20002,//讀取所有設備設置
            CmdUpdateSystemConfig = 20003,//更新設備系統設置
            CmdLoadSystemConfig = 20004,//加載設備系統設置
            CmdSetTriggerEdge = 20005,//設置觸發邊緣
            CmdGetTriggerEdge = 20006,//取得觸發邊緣
            CmdSetTriggerFilter = 20007,//設置觸發濾波器
            CmdGetTriggerFilter = 20008,//取得觸發濾波器
            CmdSetUseAnalog = 20009,//設置模擬輸出使用設置
            CmdGetUseAnalog = 20010,//取得模擬輸出使用設置
            CmdSetAnalogRange = 20011,//設置模擬輸出厚度範圍
            CmdGetAnalogRange = 20012,//取得模擬輸出厚度範圍
            CmdSetIPAdress = 20013,//設置IP地址和端口號
            CmdTestAnalog = 20014,//確認模擬輸出值
            CmdSetCalibrateThickness = 20015,//設置厚度校準值
            CmdGetCalibrateThickness = 20016,//取得厚度校準值
            CmdSetProtocol = 20017,//設置數據傳輸協議
            CmdGetProtocol = 20018,//取得數據傳輸協議

            //Recipe Change Commands (Measurement Conditions)
            CmdUpdateRecipe = 30001,//更新默認配方
            CmdSetTriggerResetTime = 30002,//設置重置觸發時間
            CmdGetTriggerResetTime = 30003,//取得重置觸發時間
            CmdSetResultType = 30004,//設置結果數據類型
            CmdGetResultType = 30005,//取得結果數據類型
            CmdSetMeasMode = 30006,//設置測量模式
            CmdGetMeasMode = 30007,//取得測量模式
            CmdSetMeasCount = 30008,//設置號碼連續測量
            CmdGetMeasCount = 30009,//取得號碼連續測量
            CmdSetMeasCycle = 30010,//設置測量週期
            CmdGetMeasCycle = 30011,//取得測量週期
            CmdSetExposure = 30012,//設置曝光時間
            CmdGetExposure = 30013,//取得曝光時間
            CmdSetAccumulation = 30014,//設置號碼積累
            CmdGetAccumulation = 30015,//取得號碼積累
            CmdSetTriggerDelay = 30016,//設置觸發延遲時間
            CmdGetTriggerDelay = 30017,//取得觸發延遲時間

            //Change Recipe Command (Analysis Condition)
            CmdSetAnalysisRange = 40001,//設置分析波長號
            CmdGetAnalysisRange = 40002,//取得分析波長號
            CmdSetAnalysisMethod = 40003,//設置集合分析法
            CmdGetAnalysisMethod = 40004,//取得集合分析法
            CmdSetAnalysisNum = 40005,//設置號碼分析層數
            CmdGetAnalysisNum = 40006,//取得號碼分析層數
            CmdSetSmoothing = 40007,//設置平滑點
            CmdGetSmoothing = 40008,//取得平滑點
            CmdSetFFTNum = 40009,//設置 FFT 號碼
            CmdGetFFTNum = 40010,//取得 FFT 號碼
            CmdSetFFTMaxThickness = 40011,//設置 FFT 最大厚度
            CmdGetFFTMaxThickness = 40012,//取得 FFT 最大厚度
            CmdSetFFTWindow = 40013,//設置 FFT 窗函數
            CmdGetFFTWindow = 40014,//取得 FFT 窗函數
            CmdSetFFTNormalization = 40015,//設置 FFT 歸一化
            CmdGetFFTNormalization = 40016,//取得 FFT 歸一化
            CmdSetUsePeakJudge = 40017,//設置NG峰排除
            CmdGetUsePeakJudge = 40018,//取得NG峰排除
            CmdSetAnalysisMaterial = 40019,//設置分析層材質
            CmdGetAnalysisMaterial = 40020,//取得分析層材質
            CmdSetAnalysisRefractiveIndex = 40021,//設置分析層折射率
            CmdGetAnalysisRefractiveIndex = 40022,//取得分析層折射率
            CmdSetAnalysisThicknessRange = 40023,//設定分析厚度範圍
            CmdGetAnalysisThicknessRange = 40024,//取得分析厚度範圍
            CmdSetFFTPowerRange = 40025,//設置 FFT 強度範圍
            CmdGetFFTPowerRange = 40026,//取得 FFT 強度範圍
            CmdSetFFTSearchDirection = 40027,//設置 FFT 峰值搜索方向
            CmdGetFFTSearchDirection = 40028,//取得 FFT 峰值搜索方向
            CmdSetFFTPeakNum = 40029,//設置 FFT 峰值數
            CmdGetFFTPeakNum = 40030,//取得 FFT 峰值數
            CmdSetThicknessCoef = 40031,//設定厚度係數
            CmdGetThicknessCoef = 40032,//取得厚度係數

            //Change Recipe Commands (Conditions for Optimization Method)
            CmdSetOptimAmbient = 50001,//設置環境層材質
            CmdGetOptimAmbient = 50002,//取得環境層材質
            CmdSetOptimSubstrate = 50003,//設置基板
            CmdGetOptimSubstrate = 50004,//取得基板
            CmdSetOptimLayerModel = 50005,//設置中間層
            CmdGetOptimLayerModel = 50006,//取得中間層
            CmdSetOptimSwitchThickness = 50007,//設置優化方法切換厚度
            CmdGetOptimSwitchThickness = 50008,//取得優化方法切換厚度
            CmdSetOptimThicknessStep = 50009,//設置優化方法厚度步長值
            CmdGetOptimThicknessStep = 50010,//取得優化方法厚度步長值

            //Change Recipe Commands (Conditions for Noise Processing)
            CmdSetFollowUpFilter = 60001,//設置後續過濾範圍
            CmdGetFollowUpFilter = 60002,//取得後續過濾範圍
            CmdSetFilterApplyTime = 60003,//設置後續過濾器應用時間
            CmdGetFilterApplyTime = 60004,//取得後續過濾器應用時間
            CmdSetFilterCenterMA = 60005,//設置後續濾波器中心值移動平均
            CmdGetFilterCenterMA = 60006,//取得後續濾波器中心值移動平均
            CmdSetFilterTrend = 60007,//設置厚度變化趨勢
            CmdGetFilterTrend = 60008,//取得厚度變化趨勢
            CmdSetThicknessMA = 60009,//設置厚度移動平均線
            CmdGetThicknessMA = 60010,//取得厚度移動平均線
            CmdSetErrorVaule = 60011,//設置錯誤值
            CmdGetErrorVaule = 60012,//取得錯誤值

            //Recipe Management Commands
            CmdSaveRecipeID = 70001,//創建配方
            CmdGetRecipeList = 70002,//獲取配方清單
            CmdDelRecipeID = 70003,//刪除配方

            //Material Management Commands
            CmdSaveMaterial = 80001,//創建材料
            CmdGetMaterial = 80002,//獲取材料
            CmdGetMaterialList = 80003,//創建材料清單
            CmdDelMaterial = 80004//刪除材料
        }
    }
}
