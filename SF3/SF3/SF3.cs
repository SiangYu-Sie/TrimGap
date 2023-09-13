using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TcpTest;
using System.Threading;
using static Otsuka.Common;
using CsvHelper;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Documents;

namespace Otsuka
{
    public class SF3
    {
        public static byte byteSOH = 0x01;
        public static byte byteSTX = 0x02;
        public static byte byteETX = 0x03;

        public int iSerialNumber = 0;
        public bool blnAscii = true;

        public bool isTimeout = false;
        public string sResponseCode = string.Empty;
        public string sStatus = string.Empty;

        static Log _log = Log.CreateInstance(7, true);
        static SimpleTcp Tcp = new SimpleTcp(_log);

        public List<string[]> list_RecipesIDName = new List<string[]>();

        public SF3_Form.Main SF3_Form;
        private SF3_Form.Recipes SF3_Recipe;

        #region Parameter
        public int ExposureTime;
        public int AccumulationsNo;
        public int TriggerDelayTime;
        public int MeasurementCycle;
        public int ContinuousMeasurementNo;
        public int ResultDataType;
        public int TriggerResetTime;
        public int MeasurementMode;
        public int StartAnalysisWavelengthNumber;
        public int EndAnalysisWavelengthNumber;
        public int AnalysisMethod;
        public int SmoothingPoint;
        public int FFTDataPointsNo;
        public float FFTMaximumThickness;
        public int FFTWindow;
        public int FFTNormalization;
        public int UsePeakJudge;
        public int AnalysisLayerNo;
        public int AnalysisLayerMaterialID;
        public int AnalysisRefractiveindex_DataQuantity;
        public List<float> ConstantRefractiveIndex = new List<float>();
        public int AnalysisThicknessRange_DataQuantity;
        public List<float> ThicknessRangeLowerLimit = new List<float>();
        public List<float> ThicknessRangeUpperLimit = new List<float>();
        public int FFTPowerRange_DataQuantity;
        public List<float> FFTIntensityRangeLowerLimit = new List<float>();
        public List<float> FFTIntensityRangeUpperLimit = new List<float>();
        public int FFTSearchDirection_DataQuantity;
        public List<int> PeakSearchDirection = new List<int>();
        public int FFTPeakNum_DataQuantity;
        public List<int> PeakNumber = new List<int>();
        public int ThicknessCoef_DataQuantity;
        public List<int> ThicknessCoef = new List<int>();
        public List<float> PrimaryCoefficientC1 = new List<float>();
        public List<float> ConstantCoefficientC0 = new List<float>();
        public float OptimSwitchThickness;
        public float OptimThicknessStep;
        public int AmbientLayerMaterialID;
        public int OptimLayermodel_LayersNo;
        public List<int> OptimLayermodel_LayerMaterialID = new List<int>();
        public List<float> OptimLayermodel_Thickness = new List<float>();
        public int SubstrateMaterialID;
        public float OptimSubstrate_Thickness;
        public float FollowupFilterPlusMinusRange;
        public int FollowupFilterApplicationTime;
        public int FilterCenterMA;
        public int GetFilterTrend;
        public int ThicknessMA;
        public int GetErrorvalue;
        #endregion

        public SF3()
        {
            SF3_Form = new SF3_Form.Main(this);
            SF3_Recipe = new SF3_Form.Recipes(this);
        }

        public void Set_Recipe(string RecipeID)
        {
            if (RecipeID == "0") Action_CmdLoadDefaultRecipe();
            else Action_CmdLoadRecipeID(Convert.ToInt32(RecipeID));
            GetSF3();
        }
        bool SF3_isOpen = false;
        public bool Open(string IpAddress, int PortNo, bool IsServer)
        {
            DeviceSetting aSetting = new DeviceSetting();
            string mIpAddrStr = IpAddress;
            try
            {
                // IP轉換
                IPAddress mIpAddr;
                if (!System.Net.IPAddress.TryParse(mIpAddrStr, out mIpAddr))
                {
                    mIpAddrStr = "192.168.1.20";
                    AddLog("IP Setting Error, IP:" + mIpAddrStr);
                }
                aSetting.Address = mIpAddrStr;
                aSetting.TcpPort = (int)PortNo;
                aSetting.UseServer = IsServer;
                aSetting.Timeout = 5000;

                //讀取recipe Default
                SF3_isOpen = Tcp.Open(aSetting);
                Set_Recipe("0");
                Get_RecipeList();


                return SF3_isOpen;
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Open error");
                return false;
            }
        }
        public void Close()
        {
            Tcp.Close();
        }

        public void Get_RecipeList()
        {
            Action_CmdGetRecipeList();
            for (int i = 0; i < CommonResult.GetRecipeList_RecipesNo; i++)
            {
                list_RecipesIDName.Add(new string[] { Convert.ToString(CommonResult.GetRecipeList_RecipesID[i]), CommonResult.GetRecipeList_RecipesName[i] });
            }
        }
        public void ShowUI()
        {
            SF3_Form.Show();
        }
        public void ShowRecipeUI()
        {
            SF3_Recipe.Show();
        }
        public void SetSF3()
        {
            Action_CmdSetExposure(ExposureTime);
            Action_CmdSetAccumulation(AccumulationsNo);
            Action_CmdSetTriggerDelay(TriggerDelayTime);
            Action_CmdSetMeasCycle(MeasurementCycle);
            Action_CmdSetMeasCount(ContinuousMeasurementNo);
            Action_CmdSetResultType((CmdSetResultType)ResultDataType);
            Action_CmdSetTriggerResetTime((CmdSetUseAnalog)TriggerResetTime);
            Action_CmdSetMeasMode((CmdSetMeasMode)MeasurementMode);
            Action_CmdGetMeasCycle();
            Action_CmdSetAnalysisRange(StartAnalysisWavelengthNumber, EndAnalysisWavelengthNumber);
            Action_CmdSetAnalysisMethod((CmdSetAnalysisMethod)AnalysisMethod);
            Action_CmdSetSmoothing((CmdSetSmoothing)SmoothingPoint);
            Action_CmdSetFFTNum((CmdSetFFTNum)FFTDataPointsNo);
            Action_CmdSetFFTMaxThickness(FFTMaximumThickness);
            Action_CmdSetFFTWindow((CmdSetFFTWindow)FFTWindow);
            Action_CmdSetFFTNormalization((CmdSetFFTNormalization)FFTNormalization);
            Action_CmdSetUsePeakJudge((CmdSetUsePeakJudge)UsePeakJudge);
            Action_CmdSetAnalysisNum(AnalysisLayerNo);
            Action_CmdSetAnalysisMaterial(AnalysisLayerMaterialID);
            Action_CmdSetAnalysisRefractiveIndex(AnalysisRefractiveindex_DataQuantity, ConstantRefractiveIndex);
            Action_CmdSetAnalysisThicknessRange(AnalysisThicknessRange_DataQuantity, ThicknessRangeLowerLimit, ThicknessRangeUpperLimit);
            Action_CmdSetFFTPowerRange(FFTPowerRange_DataQuantity, FFTIntensityRangeLowerLimit, FFTIntensityRangeUpperLimit);
            Action_CmdSetFFTSearchDirection(FFTSearchDirection_DataQuantity, PeakSearchDirection);
            Action_CmdSetFFTPeakNum(FFTPeakNum_DataQuantity, PeakNumber);
            Action_CmdSetThicknessCoef(ThicknessCoef_DataQuantity, ThicknessCoef, PrimaryCoefficientC1, ConstantCoefficientC0);
            Action_CmdSetOptimSwitchThickness(OptimSwitchThickness);
            Action_CmdSetOptimThicknessStep(OptimThicknessStep);
            Action_CmdSetOptimAmbient(AmbientLayerMaterialID);
            Action_CmdSetOptimLayerModel(OptimLayermodel_LayersNo, OptimLayermodel_LayerMaterialID, OptimLayermodel_Thickness);
            Action_CmdSetOptimSubstrate(SubstrateMaterialID, OptimSubstrate_Thickness);
            Action_CmdSetFollowUpFilter(FollowupFilterPlusMinusRange);
            Action_CmdSetFilterApplyTime(FollowupFilterApplicationTime);
            Action_CmdSetFilterCenterMA(FilterCenterMA);
            Action_CmdSetFilterTrend((CmdSetFilterTrend)GetFilterTrend);
            Action_CmdSetThicknessMA(ThicknessMA);
            Action_CmdSetErrorVaule(GetErrorvalue);
            Action_CmdGetFFTPowerXAxis();
        }
        public void GetSF3()
        {
            Action_CmdGetExposure();
            ExposureTime = CommonResult.GetExposure_ExposureTime;
            Action_CmdGetAccumulation();
            AccumulationsNo = CommonResult.GetAccumulation_AccumulationsNo;
            Action_CmdGetTriggerDelay();
            TriggerDelayTime = CommonResult.GetTriggerDelay_TriggerDelayTime;
            Action_CmdGetMeasCycle();
            MeasurementCycle = CommonResult.GetMeasCycle_MeasurementCycle;
            Action_CmdGetMeasCount();
            ContinuousMeasurementNo = CommonResult.GetMeasCount_ContinuousMeasurementNo;
            Action_CmdGetResultType();
            ResultDataType = CommonResult.GetResultType_ResultDataType;
            Action_CmdGetTriggerResetTime();
            TriggerResetTime = CommonResult.GetTriggerResetTime;
            Action_CmdGetMeasMode();
            MeasurementMode = CommonResult.GetMeasMode_MeasurementMode;
            Action_CmdGetAnalysisRange();
            StartAnalysisWavelengthNumber = CommonResult.GetAnalysisRange_StartAnalysisWavelengthNumber;
            EndAnalysisWavelengthNumber = CommonResult.GetAnalysisRange_EndAnalysisWavelengthNumber;
            Action_CmdGetAnalysisMethod();
            AnalysisMethod = CommonResult.GetAnalysisMethod_AnalysisMethod;
            Action_CmdGetSmoothing();
            SmoothingPoint = CommonResult.GetSmoothing_SmoothingPoint;
            Action_CmdGetFFTNum();
            FFTDataPointsNo = CommonResult.GetFFTNum_FFTDataPointsNo;
            Action_CmdGetFFTMaxThickness();
            FFTMaximumThickness = CommonResult.GetFFTMaxThickness_FFTMaximumThickness;
            Action_CmdGetFFTWindow();
            FFTWindow = CommonResult.GetFFTWindow;
            Action_CmdGetFFTNormalization();
            FFTNormalization = CommonResult.GetFFTNormalization;
            Action_CmdGetUsePeakJudge();
            UsePeakJudge = CommonResult.GetUsePeakJudge;
            Action_CmdGetAnalysisNum();
            AnalysisLayerNo = CommonResult.GetAnalysisNum_AnalysisLayerNo;
            Action_CmdGetAnalysisMaterial();
            AnalysisLayerMaterialID = CommonResult.GetAnalysisMaterial_AnalysisLayerMaterialID;
            Action_CmdGetAnalysisRefractiveIndex();
            AnalysisRefractiveindex_DataQuantity = CommonResult.GetAnalysisRefractiveindex_DataQuantity;
            ConstantRefractiveIndex = CommonResult.GetAnalysisRefractiveindex_ConstantRefractiveIndex;
            Action_CmdGetAnalysisThicknessRange();
            AnalysisThicknessRange_DataQuantity = CommonResult.GetAnalysisThicknessRange_DataQuantity;
            ThicknessRangeLowerLimit = CommonResult.GetAnalysisThicknessRange_ThicknessRangeLowerLimit;
            ThicknessRangeUpperLimit = CommonResult.GetAnalysisThicknessRange_ThicknessRangeUpperLimit;
            Action_CmdGetFFTPowerRange();
            FFTPowerRange_DataQuantity = CommonResult.GetFFTPowerRange_DataQuantity;
            FFTIntensityRangeLowerLimit = CommonResult.GetFFTPowerRange_FFTIntensityRangeLowerLimit;
            FFTIntensityRangeUpperLimit = CommonResult.GetFFTPowerRange_FFTIntensityRangeUpperLimit;
            Action_CmdGetFFTSearchDirection();
            FFTSearchDirection_DataQuantity = CommonResult.GetFFTSearchDirection_DataQuantity;
            PeakSearchDirection = CommonResult.GetFFTSearchDirection_PeakSearchDirection;
            Action_CmdGetFFTPeakNum();
            FFTPeakNum_DataQuantity = CommonResult.GetFFTPeakNum_DataQuantity;
            PeakNumber = CommonResult.GetFFTPeakNum_PeakNumber;
            Action_CmdGetThicknessCoef();
            ThicknessCoef_DataQuantity = CommonResult.GetThicknessCoef_DataQuantity;
            ThicknessCoef = CommonResult.GetThicknessCoef;
            PrimaryCoefficientC1 = CommonResult.GetThicknessCoef_PrimaryCoefficientC1;
            ConstantCoefficientC0 = CommonResult.GetThicknessCoef_ConstantCoefficientC0;
            Action_CmdGetOptimSwitchThickness();
            OptimSwitchThickness = CommonResult.GetOptimSwitchThickness_Thickness;
            Action_CmdGetOptimThicknessStep();
            OptimThicknessStep = CommonResult.GetOptimThicknessStep_ThicknessStep;
            Action_CmdGetOptimAmbient();
            AmbientLayerMaterialID = CommonResult.GetOptimAmbient_AmbientLayerMaterialID;
            Action_CmdGetOptimLayerModel();
            OptimLayermodel_LayersNo = CommonResult.GetOptimLayermodel_LayersNo;
            OptimLayermodel_LayerMaterialID = CommonResult.GetOptimLayermodel_LayerMaterialID;
            OptimLayermodel_Thickness = CommonResult.GetOptimLayermodel_Thickness;
            Action_CmdGetOptimSubstrate();
            SubstrateMaterialID = CommonResult.GetOptimSubstrate_SubstrateMaterialID;
            OptimSubstrate_Thickness = CommonResult.GetOptimSubstrate_Thickness;
            Action_CmdGetFollowUpFilter();
            FollowupFilterPlusMinusRange = CommonResult.GetFollowupFilter_FollowupFilterPlusMinusRange;
            Action_CmdGetFilterApplyTime();
            FollowupFilterApplicationTime = CommonResult.GetFilterApplytime_FollowupFilterApplicationTime;
            Action_CmdGetFilterCenterMA();
            FilterCenterMA = CommonResult.GetFilterCenterMA_FollowupFilterCenterValueMovingAverage;
            Action_CmdGetFilterTrend();
            GetFilterTrend = CommonResult.GetFilterTrend;
            Action_CmdGetThicknessMA();
            ThicknessMA = CommonResult.GetThicknessMA_ThicknessMovingAverageFrequency;
            Action_CmdGetErrorVaule();
            GetErrorvalue = CommonResult.GetErrorvalue;
        }







        public void Action_Measure_1()
        {
            Action_CmdReady();
            Action_CmdCtrlLight(Common.CmdCtrlLight.Light);
            Action_CmdMeasStart();
            Action_CmdGetResult();
            Action_CmdMeasStop();
        }
        public void Action_CmdReady()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdReady))) return;
            CheckRecv((int)Common.Command.CmdReady);
        }

        /// <summary>
        /// 14 ASCII letters(yyyyMMddHHmmss)
        /// </summary>
        /// <param name="cmdSetDateTime_DataTime"></param>
        public void Action_CmdSetDateTime(string cmdSetDateTime_DataTime)
        {
            Common.CmdSetDateTime_DataTime = cmdSetDateTime_DataTime;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetDateTime))) return;
            CheckRecv((int)Common.Command.CmdSetDateTime);
        }

        /// <summary>
        /// 4 bytes int(6 - 20000000), 4 bytes int(1 - 65535)
        /// </summary>
        /// <param name="cmdMeasReference_ExposureTime"></param>
        /// <param name="cmdMeasReference_NumOfAccumulations"></param>
        public void Action_CmdMeasReference(int cmdMeasReference_ExposureTime, int cmdMeasReference_NumOfAccumulations)
        {
            Common.CmdMeasReference_ExposureTime = cmdMeasReference_ExposureTime;
            Common.CmdMeasReference_NumOfAccumulations = cmdMeasReference_NumOfAccumulations;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdMeasReference))) return;
            CheckMeasReference();
        }

        /// <summary>
        /// 4 bytes int(6 - 20000000), 4 bytes int(1 - 65535), 4 bytes int, 4 bytes float × No. of signals, 4 bytes float × No. of signals
        /// </summary>
        /// <param name="cmdSetReference_ExposureTime"></param>
        /// <param name="cmdSetReference_NumOfAccumulations"></param>
        /// <param name="cmdSetReference_NumOfsignals"></param>
        /// <param name="cmdSetReference_ReferenceDarkSpectrum"></param>
        /// <param name="cmdSetReference_ReferenceSignalSpectrum"></param>
        public void Action_CmdSetReference(int cmdSetReference_ExposureTime, int cmdSetReference_NumOfAccumulations, int cmdSetReference_NumOfsignals, List<float> cmdSetReference_ReferenceDarkSpectrum, List<float> cmdSetReference_ReferenceSignalSpectrum)
        {
            Common.CmdSetReference_ReferenceDarkSpectrum.Clear();
            Common.CmdSetReference_ReferenceSignalSpectrum.Clear();

            Common.CmdSetReference_ExposureTime = cmdSetReference_ExposureTime;
            Common.CmdSetReference_NumOfAccumulations = cmdSetReference_NumOfAccumulations;
            Common.CmdSetReference_NumOfsignals = cmdSetReference_NumOfsignals;
            Common.CmdSetReference_ReferenceDarkSpectrum = cmdSetReference_ReferenceDarkSpectrum;
            Common.CmdSetReference_ReferenceSignalSpectrum = cmdSetReference_ReferenceSignalSpectrum;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetReference))) return;
            CheckRecv((int)Common.Command.CmdSetReference);
        }

        public void Action_CmdGetReference()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetReference))) return;
            CheckGetReference();
        }

        public void Action_CmdUpdateReference()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdUpdateReference))) return;
            CheckRecv((int)Common.Command.CmdUpdateReference);
        }

        public void Action_CmdLoadReference()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdLoadReference))) return;
            CheckRecv((int)Common.Command.CmdLoadReference);
        }

        public void Action_CmdLoadDefaultRecipe()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdLoadDefaultRecipe))) return;
            CheckRecv((int)Common.Command.CmdLoadDefaultRecipe);
        }

        /// <summary>
        /// 4 bytes int(1 - 999)
        /// </summary>
        /// <param name="cmdLoadRecipeID_RecipeID"></param>
        public void Action_CmdLoadRecipeID(int cmdLoadRecipeID_RecipeID)
        {
            Common.CmdLoadRecipeID_RecipeID = cmdLoadRecipeID_RecipeID;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdLoadRecipeID))) return;
            CheckRecv((int)Common.Command.CmdLoadRecipeID);
        }

        public void Action_CmdMeasDark()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdMeasDark))) return;
            CheckMeasDark();
        }

        public void Action_CmdMakeSimulationData()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdMakeSimulationData))) return;
            CheckRecv((int)Common.Command.CmdMakeSimulationData);
        }

        /// <summary>
        /// 4 bytes int(6 - 20000000), 4 bytes int(1 - 65535)
        /// </summary>
        /// <param name="cmdMeasSignal_ExposureTime"></param>
        /// <param name="cmdMeasSignal_NumOfAccumulations"></param>
        public void Action_CmdMeasSignal(int cmdMeasSignal_ExposureTime, int cmdMeasSignal_NumOfAccumulations)
        {
            Common.CmdMeasSignal_ExposureTime = cmdMeasSignal_ExposureTime;
            Common.CmdMeasSignal_NumOfAccumulations = cmdMeasSignal_NumOfAccumulations;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdMeasSignal))) return;
            CheckMeasSignal();
        }

        public void Action_CmdMeasStart()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdMeasStart))) return;
            CheckRecv((int)Common.Command.CmdMeasStart);
        }

        public void Action_CmdMeasStop()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdMeasStop))) return;
            CheckRecv((int)Common.Command.CmdMeasStop);
        }

        public void Action_CmdGetResult()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetResult))) return;
            CheckGetResult();
        }

        public void Action_CmdCtrlLight(Common.CmdCtrlLight cmdCtrlLight)
        {
            Common.CmdCtrlLight_enum = (int)cmdCtrlLight;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdCtrlLight))) return;
            CheckRecv((int)Common.Command.CmdCtrlLight);
        }

        public void Action_CmdGetStatus()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetStatus))) return;
            CheckRecv((int)Common.Command.CmdGetStatus);
        }

        public void Action_CmdGetFFTPowerXAxis()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetFFTPowerXAxis))) return;
            CheckGetFFTPowerXAxis();
        }

        /// <summary>
        /// 4 bytes int, 4 bytes float x No.of reflectances
        /// </summary>
        /// <param name="cmdReanalyze_NumOfReflectances"></param>
        /// <param name="cmdReanalyze_ReflectanceSpectrum"></param>
        public void Action_CmdReanalyze(int cmdReanalyze_NumOfReflectances, List<float> cmdReanalyze_ReflectanceSpectrum)
        {
            Common.CmdReanalyze_ReflectanceSpectrum.Clear();

            Common.CmdReanalyze_NumOfReflectances = cmdReanalyze_NumOfReflectances;
            Common.CmdReanalyze_ReflectanceSpectrum = cmdReanalyze_ReflectanceSpectrum;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdReanalyze))) return;
            CheckReanalyze();
        }

        public void Action_CmdUpdateAll()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdUpdateAll))) return;
            CheckRecv((int)Common.Command.CmdUpdateAll);
        }

        public void Action_CmdLoadDefaultAll()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdLoadDefaultAll))) return;
            CheckRecv((int)Common.Command.CmdLoadDefaultAll);
        }

        public void Action_CmdUpdateSystemConfig()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdUpdateSystemConfig))) return;
            CheckRecv((int)Common.Command.CmdUpdateSystemConfig);
        }

        public void Action_CmdLoadSystemConfig()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdLoadSystemConfig))) return;
            CheckRecv((int)Common.Command.CmdLoadSystemConfig);
        }

        public void Action_CmdSetTriggerEdge(Common.CmdSetTriggerEdge cmdSetTriggerEdge)
        {
            Common.CmdSetTriggerEdge_enum = (int)cmdSetTriggerEdge;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetTriggerEdge))) return;
            CheckRecv((int)Common.Command.CmdSetTriggerEdge);
        }

        public void Action_CmdGetTriggerEdge()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetTriggerEdge))) return;
            CheckRecv((int)Common.Command.CmdGetTriggerEdge);
        }

        /// <summary>
        /// 4 bytes int(0 - 65535)
        /// </summary>
        /// <param name="cmdSetTriggerFilter_Time"></param>
        public void Action_CmdSetTriggerFilter(int cmdSetTriggerFilter_Time)
        {
            Common.CmdSetTriggerFilter_Time = cmdSetTriggerFilter_Time;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetTriggerFilter))) return;
            CheckRecv((int)Common.Command.CmdSetTriggerFilter);
        }

        public void Action_CmdGetTriggerFilter()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetTriggerFilter))) return;
            CheckRecv((int)Common.Command.CmdGetTriggerFilter);
        }

        public void Action_CmdSetUseAnalog(Common.CmdSetUseAnalog cmdSetUseAnalog)
        {
            Common.CmdSetUseAnalog_enum = (int)cmdSetUseAnalog;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetUseAnalog))) return;
            CheckRecv((int)Common.Command.CmdSetUseAnalog);
        }

        public void Action_CmdGetUseAnalog()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetUseAnalog))) return;
            CheckRecv((int)Common.Command.CmdGetUseAnalog);
        }

        /// <summary>
        /// 4 bytes float(0.0 - 100000.0), 4 bytes float(LowerLimits - 100000.0)
        /// </summary>
        /// <param name="cmdSetAnalogRange_LowerLimit"></param>
        /// <param name="cmdSetAnalogRange_UpperLimit"></param>
        public void Action_CmdSetAnalogRange(int cmdSetAnalogRange_LowerLimit, int cmdSetAnalogRange_UpperLimit)
        {
            Common.CmdSetAnalogRange_LowerLimit = cmdSetAnalogRange_LowerLimit;
            Common.CmdSetAnalogRange_UpperLimit = cmdSetAnalogRange_UpperLimit;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetAnalogRange))) return;
            CheckRecv((int)Common.Command.CmdSetAnalogRange);
        }

        public void Action_CmdGetAnalogRange()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetAnalogRange))) return;
            CheckRecv((int)Common.Command.CmdGetAnalogRange);
        }

        /// <summary>
        /// (0 - 255, 0 - 255, 0 - 255, 0 - 255, 0 - 65535)
        /// </summary>
        /// <param name="iPAddress"></param>
        public void Action_CmdSetIPAdress(string iPAddress)
        {
            Common.IPAddress = iPAddress;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetIPAdress))) return;
            CheckRecv((int)Common.Command.CmdSetIPAdress);
        }

        /// <summary>
        /// 4 bytes float(0.0 - 100000.0)
        /// </summary>
        /// <param name="cmdTestAnalog_Thickness"></param>
        public void Action_CmdTestAnalog(float cmdTestAnalog_Thickness)
        {
            Common.CmdTestAnalog_Thickness = cmdTestAnalog_Thickness;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdTestAnalog))) return;
            CheckRecv((int)Common.Command.CmdTestAnalog);
        }

        /// <summary>
        /// 4 bytes float(0.0 - 100000.0), 4 bytes float(0.0 - 100000.0), 4 bytes float(0.0 - 100000.0), 4 bytes float(0.0 - 100000.0)
        /// </summary>
        /// <param name="cmdSetCalibrateThickness_StandardThicknessValue1"></param>
        /// <param name="cmdSetCalibrateThickness_MeasurementThicknessValue1"></param>
        /// <param name="cmdSetCalibrateThickness_StandardThicknessValue2"></param>
        /// <param name="cmdSetCalibrateThickness_MeasurementThicknessValue2"></param>
        public void Action_CmdSetCalibrateThickness(float cmdSetCalibrateThickness_StandardThicknessValue1, float cmdSetCalibrateThickness_MeasurementThicknessValue1, float cmdSetCalibrateThickness_StandardThicknessValue2, float cmdSetCalibrateThickness_MeasurementThicknessValue2)
        {
            Common.CmdSetCalibrateThickness_StandardThicknessValue1 = cmdSetCalibrateThickness_StandardThicknessValue1;
            Common.CmdSetCalibrateThickness_MeasurementThicknessValue1 = cmdSetCalibrateThickness_MeasurementThicknessValue1;
            Common.CmdSetCalibrateThickness_StandardThicknessValue2 = cmdSetCalibrateThickness_StandardThicknessValue2;
            Common.CmdSetCalibrateThickness_MeasurementThicknessValue2 = cmdSetCalibrateThickness_MeasurementThicknessValue2;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetCalibrateThickness))) return;
            CheckRecv((int)Common.Command.CmdSetCalibrateThickness);
        }

        public void Action_CmdGetCalibrateThickness()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetCalibrateThickness))) return;
            CheckRecv((int)Common.Command.CmdGetCalibrateThickness);
        }

        public void Action_CmdSetProtocol(Common.CmdSetUseAnalog cmdSetProtocol)
        {
            Common.CmdSetProtocol_enum = (int)cmdSetProtocol;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetProtocol))) return;
            CheckRecv((int)Common.Command.CmdSetProtocol);
        }

        public void Action_CmdGetProtocol()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetProtocol))) return;
            CheckRecv((int)Common.Command.CmdGetProtocol);
        }

        public void Action_CmdUpdateRecipe()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdUpdateRecipe))) return;
            CheckRecv((int)Common.Command.CmdUpdateRecipe);
        }

        public void Action_CmdSetTriggerResetTime(Common.CmdSetUseAnalog cmdSetTriggerResetTime_enum)
        {
            Common.CmdSetTriggerResetTime_enum = (int)cmdSetTriggerResetTime_enum;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetTriggerResetTime))) return;
            CheckRecv((int)Common.Command.CmdSetTriggerResetTime);
        }

        public void Action_CmdGetTriggerResetTime()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetTriggerResetTime))) return;
            CheckRecv((int)Common.Command.CmdGetTriggerResetTime);
        }

        public void Action_CmdSetResultType(Common.CmdSetResultType cmdSetResultType)
        {
            Common.CmdSetResultType_enum = (int)cmdSetResultType;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetResultType))) return;
            CheckRecv((int)Common.Command.CmdSetResultType);
        }

        public void Action_CmdGetResultType()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetResultType))) return;
            CheckRecv((int)Common.Command.CmdGetResultType);
        }

        public void Action_CmdSetMeasMode(Common.CmdSetMeasMode cmdSetMeasMode)
        {
            Common.CmdSetMeasMode_enum = (int)cmdSetMeasMode;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetMeasMode))) return;
            CheckRecv((int)Common.Command.CmdSetMeasMode);
        }

        public void Action_CmdGetMeasMode()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetMeasMode))) return;
            CheckRecv((int)Common.Command.CmdGetMeasMode);
        }

        /// <summary>
        /// 4 bytes int(1 - 999)
        /// </summary>
        /// <param name="cmdSetMeasCount_NumOfContinuousMeasurement"></param>
        public void Action_CmdSetMeasCount(int cmdSetMeasCount_NumOfContinuousMeasurement)
        {
            Common.CmdSetMeasCount_NumOfContinuousMeasurement = cmdSetMeasCount_NumOfContinuousMeasurement;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetMeasCount))) return;
            CheckRecv((int)Common.Command.CmdSetMeasCount);
        }

        public void Action_CmdGetMeasCount()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetMeasCount))) return;
            CheckRecv((int)Common.Command.CmdGetMeasCount);
        }

        /// <summary>
        /// 4 bytes int(75 - 2147483647)(approximately 35 min)
        /// </summary>
        /// <param name="cmdSetMeasCycle_MeasurementCycle"></param>
        public void Action_CmdSetMeasCycle(int cmdSetMeasCycle_MeasurementCycle)
        {
            Common.CmdSetMeasCycle_MeasurementCycle = cmdSetMeasCycle_MeasurementCycle;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetMeasCycle))) return;
            CheckRecv((int)Common.Command.CmdSetMeasCycle);
        }

        public void Action_CmdGetMeasCycle()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetMeasCycle))) return;
            CheckRecv((int)Common.Command.CmdGetMeasCycle);
        }

        /// <summary>
        /// 4 bytes int(6 - 20000000)
        /// </summary>
        /// <param name="cmdSetExposure_ExposureTime"></param>
        public void Action_CmdSetExposure(int cmdSetExposure_ExposureTime)
        {
            Common.CmdSetExposure_ExposureTime = cmdSetExposure_ExposureTime;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetExposure))) return;
            CheckRecv((int)Common.Command.CmdSetExposure);
        }

        public void Action_CmdGetExposure()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetExposure))) return;
            CheckRecv((int)Common.Command.CmdGetExposure);
        }

        /// <summary>
        /// 4 bytes int(1 - 65535)
        /// </summary>
        /// <param name="cmdSetAccumulation_NumOfAccumulations"></param>
        public void Action_CmdSetAccumulation(int cmdSetAccumulation_NumOfAccumulations)
        {
            Common.CmdSetAccumulation_NumOfAccumulations = cmdSetAccumulation_NumOfAccumulations;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetAccumulation))) return;
            CheckRecv((int)Common.Command.CmdSetAccumulation);
        }

        public void Action_CmdGetAccumulation()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetAccumulation))) return;
            CheckRecv((int)Common.Command.CmdGetAccumulation);
        }

        /// <summary>
        /// 4 bytes int(0 - 65535)
        /// </summary>
        /// <param name="cmdSetTriggerDelay_TriggerDelayTime"></param>
        public void Action_CmdSetTriggerDelay(int cmdSetTriggerDelay_TriggerDelayTime)
        {
            Common.CmdSetTriggerDelay_TriggerDelayTime = cmdSetTriggerDelay_TriggerDelayTime;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetTriggerDelay))) return;
            CheckRecv((int)Common.Command.CmdSetTriggerDelay);
        }

        public void Action_CmdGetTriggerDelay()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetTriggerDelay))) return;
            CheckRecv((int)Common.Command.CmdGetTriggerDelay);
        }

        /// <summary>
        /// 4 bytes int(0 - 511), 4 bytes int(AnalysisStartWavelengthNum - 511)
        /// </summary>
        /// <param name="cmdSetAnalysisRange_AnalysisStartWavelengthNum"></param>
        /// <param name="CmdSetAnalysisRange_AnalysisEndWavelengthNum"></param>
        public void Action_CmdSetAnalysisRange(int cmdSetAnalysisRange_AnalysisStartWavelengthNum, int CmdSetAnalysisRange_AnalysisEndWavelengthNum)
        {
            Common.CmdSetAnalysisRange_AnalysisStartWavelengthNum = cmdSetAnalysisRange_AnalysisStartWavelengthNum;
            Common.CmdSetAnalysisRange_AnalysisEndWavelengthNum = CmdSetAnalysisRange_AnalysisEndWavelengthNum;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetAnalysisRange))) return;
            CheckRecv((int)Common.Command.CmdSetAnalysisRange);
        }

        public void Action_CmdGetAnalysisRange()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetAnalysisRange))) return;
            CheckRecv((int)Common.Command.CmdGetAnalysisRange);
        }

        public void Action_CmdSetAnalysisMethod(Common.CmdSetAnalysisMethod cmdSetAnalysisMethod)
        {
            Common.CmdSetAnalysisMethod_enum = (int)cmdSetAnalysisMethod;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetAnalysisMethod))) return;
            CheckRecv((int)Common.Command.CmdSetAnalysisMethod);
        }

        public void Action_CmdGetAnalysisMethod()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetAnalysisMethod))) return;
            CheckRecv((int)Common.Command.CmdGetAnalysisMethod);
        }

        /// <summary>
        /// 4 bytes int(1 - 5)
        /// </summary>
        /// <param name="cmdSetAnalysisNum_NumOfAnalysisLayers"></param>
        public void Action_CmdSetAnalysisNum(int cmdSetAnalysisNum_NumOfAnalysisLayers)
        {
            Common.CmdSetAnalysisNum_NumOfAnalysisLayers = cmdSetAnalysisNum_NumOfAnalysisLayers;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetAnalysisNum))) return;
            CheckRecv((int)Common.Command.CmdSetAnalysisNum);
        }

        public void Action_CmdGetAnalysisNum()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetAnalysisNum))) return;
            CheckRecv((int)Common.Command.CmdGetAnalysisNum);
        }

        public void Action_CmdSetSmoothing(Common.CmdSetSmoothing cmdSetSmoothing)
        {
            Common.CmdSetSmoothing_enum = (int)cmdSetSmoothing;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetSmoothing))) return;
            CheckRecv((int)Common.Command.CmdSetSmoothing);
        }

        public void Action_CmdGetSmoothing()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetSmoothing))) return;
            CheckRecv((int)Common.Command.CmdGetSmoothing);
        }

        public void Action_CmdSetFFTNum(Common.CmdSetFFTNum cmdSetFFTNum)
        {
            Common.CmdSetFFTNum_enum = (int)cmdSetFFTNum;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetFFTNum))) return;
            CheckRecv((int)Common.Command.CmdSetFFTNum);
        }

        public void Action_CmdGetFFTNum()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetFFTNum))) return;
            CheckRecv((int)Common.Command.CmdGetFFTNum);
        }

        /// <summary>
        /// 4 bytes float(0.0 - 100000.0)
        /// </summary>
        /// <param name="cmdSetFFTMaxThickness_FFTMaxThickness"></param>
        public void Action_CmdSetFFTMaxThickness(float cmdSetFFTMaxThickness_FFTMaxThickness)
        {
            Common.CmdSetFFTMaxThickness_FFTMaxThickness = cmdSetFFTMaxThickness_FFTMaxThickness;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetFFTMaxThickness))) return;
            CheckRecv((int)Common.Command.CmdSetFFTMaxThickness);
        }

        public void Action_CmdGetFFTMaxThickness()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetFFTMaxThickness))) return;
            CheckRecv((int)Common.Command.CmdGetFFTMaxThickness);
        }

        public void Action_CmdSetFFTWindow(Common.CmdSetFFTWindow cmdSetFFTWindow)
        {
            Common.CmdSetFFTWindow_enum = (int)cmdSetFFTWindow;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetFFTWindow))) return;
            CheckRecv((int)Common.Command.CmdSetFFTWindow);
        }

        public void Action_CmdGetFFTWindow()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetFFTWindow))) return;
            CheckRecv((int)Common.Command.CmdGetFFTWindow);
        }

        public void Action_CmdSetFFTNormalization(Common.CmdSetFFTNormalization cmdSetFFTNormalization)
        {
            Common.CmdSetFFTNormalization_enum = (int)cmdSetFFTNormalization;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetFFTNormalization))) return;
            CheckRecv((int)Common.Command.CmdSetFFTNormalization);
        }

        public void Action_CmdGetFFTNormalization()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetFFTNormalization))) return;
            CheckRecv((int)Common.Command.CmdGetFFTNormalization);
        }

        public void Action_CmdSetUsePeakJudge(Common.CmdSetUsePeakJudge cmdSetUsePeakJudge)
        {
            Common.CmdSetUsePeakJudge_enum = (int)cmdSetUsePeakJudge;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetUsePeakJudge))) return;
            CheckRecv((int)Common.Command.CmdSetUsePeakJudge);
        }

        public void Action_CmdGetUsePeakJudge()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetUsePeakJudge))) return;
            CheckRecv((int)Common.Command.CmdGetUsePeakJudge);
        }

        /// <summary>
        /// 4 bytes int(1 - 999)
        /// </summary>
        /// <param name="cmdSetAnalysisMaterial_AnalysisLayerMaterialID"></param>
        public void Action_CmdSetAnalysisMaterial(int cmdSetAnalysisMaterial_AnalysisLayerMaterialID)
        {
            Common.CmdSetAnalysisMaterial_AnalysisLayerMaterialID = cmdSetAnalysisMaterial_AnalysisLayerMaterialID;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetAnalysisMaterial))) return;
            CheckRecv((int)Common.Command.CmdSetAnalysisMaterial);
        }

        public void Action_CmdGetAnalysisMaterial()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetAnalysisMaterial))) return;
            CheckRecv((int)Common.Command.CmdGetAnalysisMaterial);
        }

        /// <summary>
        /// 4 bytes int(1 - 5), 4 bytes int(0 - 10)
        /// </summary>
        /// <param name="cmdSetAnalysisRefractiveIndex_DataQuantity"></param>
        /// <param name="CmdSetAnalysisRefractiveIndex_ConstantRefractiveIndex"></param>
        public void Action_CmdSetAnalysisRefractiveIndex(int cmdSetAnalysisRefractiveIndex_DataQuantity, List<float> CmdSetAnalysisRefractiveIndex_ConstantRefractiveIndex)
        {
            Common.CmdSetAnalysisRefractiveIndex_ConstantRefractiveIndex.Clear();

            Common.CmdSetAnalysisRefractiveIndex_DataQuantity = cmdSetAnalysisRefractiveIndex_DataQuantity;
            Common.CmdSetAnalysisRefractiveIndex_ConstantRefractiveIndex = CmdSetAnalysisRefractiveIndex_ConstantRefractiveIndex.ToList();

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetAnalysisRefractiveIndex))) return;
            CheckRecv((int)Common.Command.CmdSetAnalysisRefractiveIndex);
        }

        public void Action_CmdGetAnalysisRefractiveIndex()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetAnalysisRefractiveIndex))) return;
            CheckRecv((int)Common.Command.CmdGetAnalysisRefractiveIndex);
        }

        /// <summary>
        /// 4 bytes int(1 - 5), 4 bytes float(0.0 - 100000.0), 4 bytes float(ThicknessRangeLowerLimit - 100000.0)
        /// </summary>
        /// <param name="cmdSetAnalysisThicknessRange_DataQuantity"></param>
        /// <param name="cmdSetAnalysisThicknessRange_ThicknessRangeLowerLimit"></param>
        /// <param name="cmdSetAnalysisThicknessRange_ThicknessRangeUpperLimit"></param>
        public void Action_CmdSetAnalysisThicknessRange(int cmdSetAnalysisThicknessRange_DataQuantity, List<float> cmdSetAnalysisThicknessRange_ThicknessRangeLowerLimit, List<float> cmdSetAnalysisThicknessRange_ThicknessRangeUpperLimit)
        {
            Common.CmdSetAnalysisThicknessRange_ThicknessRangeLowerLimit.Clear();
            Common.CmdSetAnalysisThicknessRange_ThicknessRangeUpperLimit.Clear();

            Common.CmdSetAnalysisThicknessRange_DataQuantity = cmdSetAnalysisThicknessRange_DataQuantity;
            Common.CmdSetAnalysisThicknessRange_ThicknessRangeLowerLimit = cmdSetAnalysisThicknessRange_ThicknessRangeLowerLimit.ToList();
            Common.CmdSetAnalysisThicknessRange_ThicknessRangeUpperLimit = cmdSetAnalysisThicknessRange_ThicknessRangeUpperLimit.ToList();

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetAnalysisThicknessRange))) return;
            CheckRecv((int)Common.Command.CmdSetAnalysisThicknessRange);
        }

        public void Action_CmdGetAnalysisThicknessRange()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetAnalysisThicknessRange))) return;
            CheckRecv((int)Common.Command.CmdGetAnalysisThicknessRange);
        }

        /// <summary>
        /// 4 bytes int(1 - 5), 4 bytes float(0.0 - 10000.0), 4 bytes float(FFTIntensityRangeLowerLimit - 10000.0)
        /// </summary>
        /// <param name="cmdSetFFTPowerRange_DataQuantity"></param>
        /// <param name="cmdSetFFTPowerRange_FFTIntensityRangeLowerLimit"></param>
        /// <param name="cmdSetFFTPowerRange_FFTIntensityRangeUpperLimit"></param>
        public void Action_CmdSetFFTPowerRange(int cmdSetFFTPowerRange_DataQuantity, List<float> cmdSetFFTPowerRange_FFTIntensityRangeLowerLimit, List<float> cmdSetFFTPowerRange_FFTIntensityRangeUpperLimit)
        {
            Common.CmdSetFFTPowerRange_FFTIntensityRangeLowerLimit.Clear();
            Common.CmdSetFFTPowerRange_FFTIntensityRangeUpperLimit.Clear();

            Common.CmdSetFFTPowerRange_DataQuantity = cmdSetFFTPowerRange_DataQuantity;
            Common.CmdSetFFTPowerRange_FFTIntensityRangeLowerLimit = cmdSetFFTPowerRange_FFTIntensityRangeLowerLimit.ToList();
            Common.CmdSetFFTPowerRange_FFTIntensityRangeUpperLimit = cmdSetFFTPowerRange_FFTIntensityRangeUpperLimit.ToList();

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetFFTPowerRange))) return;
            CheckRecv((int)Common.Command.CmdSetFFTPowerRange);
        }

        public void Action_CmdGetFFTPowerRange()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetFFTPowerRange))) return;
            CheckRecv((int)Common.Command.CmdGetFFTPowerRange);
        }

        /// <summary>
        /// 4 bytes int(1 - 5)
        /// </summary>
        /// <param name="cmdSetFFTSearchDirection_DataQuantity"></param>
        public void Action_CmdSetFFTSearchDirection(int cmdSetFFTSearchDirection_DataQuantity, List<int> CmdSetFFTSearchDirection_list)
        {
            Common.CmdSetFFTSearchDirection_list.Clear();

            Common.CmdSetFFTSearchDirection_DataQuantity = cmdSetFFTSearchDirection_DataQuantity;
            Common.CmdSetFFTSearchDirection_list = CmdSetFFTSearchDirection_list.ToList();

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetFFTSearchDirection))) return;
            CheckRecv((int)Common.Command.CmdSetFFTSearchDirection);
        }

        public void Action_CmdGetFFTSearchDirection()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetFFTSearchDirection))) return;
            CheckRecv((int)Common.Command.CmdGetFFTSearchDirection);
        }

        /// <summary>
        /// 4 bytes int(1 - 5), 4 bytes int(1 - 99)
        /// </summary>
        /// <param name="cmdSetFFTPeakNum_DataQuantity"></param>
        /// <param name="cmdSetFFTPeakNum_PeakNumber"></param>
        public void Action_CmdSetFFTPeakNum(int cmdSetFFTPeakNum_DataQuantity, List<int> cmdSetFFTPeakNum_PeakNumber)
        {
            Common.CmdSetFFTPeakNum_PeakNumber.Clear();

            Common.CmdSetFFTPeakNum_DataQuantity = cmdSetFFTPeakNum_DataQuantity;
            Common.CmdSetFFTPeakNum_PeakNumber = cmdSetFFTPeakNum_PeakNumber.ToList();

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetFFTPeakNum))) return;
            CheckRecv((int)Common.Command.CmdSetFFTPeakNum);
        }

        public void Action_CmdGetFFTPeakNum()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetFFTPeakNum))) return;
            CheckRecv((int)Common.Command.CmdGetFFTPeakNum);
        }

        /// <summary>
        /// 4 bytes int(1 - 5), 4 bytes float, 4 bytes float
        /// </summary>
        /// <param name="cmdSetThicknessCoef_DataQuantity"></param>
        public void Action_CmdSetThicknessCoef(int cmdSetThicknessCoef_DataQuantity, List<int> cmdSetThicknessCoef, List<float> cmdSetThicknessCoef_PrimaryCoefficientC1, List<float> cmdSetThicknessCoef_ConstantCoefficientC0)
        {
            Common.CmdSetThicknessCoef_DataQuantity = cmdSetThicknessCoef_DataQuantity;
            Common.CmdSetThicknessCoef_list = cmdSetThicknessCoef.ToList();
            Common.CmdSetThicknessCoef_PrimaryCoefficientC1 = cmdSetThicknessCoef_PrimaryCoefficientC1.ToList();
            Common.CmdSetThicknessCoef_ConstantCoefficientC0 = cmdSetThicknessCoef_ConstantCoefficientC0.ToList();

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetThicknessCoef))) return;
            CheckRecv((int)Common.Command.CmdSetThicknessCoef);
        }

        public void Action_CmdGetThicknessCoef()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetThicknessCoef))) return;
            CheckRecv((int)Common.Command.CmdGetThicknessCoef);
        }

        /// <summary>
        /// 4 bytes int(1 - 999)
        /// </summary>
        /// <param name="cmdSetOptimAmbient_AmbientLayerMaterialID"></param>
        public void Action_CmdSetOptimAmbient(int cmdSetOptimAmbient_AmbientLayerMaterialID)
        {
            Common.CmdSetOptimAmbient_AmbientLayerMaterialID = cmdSetOptimAmbient_AmbientLayerMaterialID;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetOptimAmbient))) return;
            CheckRecv((int)Common.Command.CmdSetOptimAmbient);
        }

        public void Action_CmdGetOptimAmbient()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetOptimAmbient))) return;
            CheckRecv((int)Common.Command.CmdGetOptimAmbient);
        }

        /// <summary>
        /// 4 bytes int(1 - 999), 4 bytes float(0.0 - 100000.0)
        /// </summary>
        /// <param name="cmdSetOptimSubstrate_SubstrateMaterialID"></param>
        /// <param name="cmdSetOptimSubstrate_Thickness"></param>
        public void Action_CmdSetOptimSubstrate(int cmdSetOptimSubstrate_SubstrateMaterialID, float cmdSetOptimSubstrate_Thickness)
        {
            Common.CmdSetOptimSubstrate_SubstrateMaterialID = cmdSetOptimSubstrate_SubstrateMaterialID;
            Common.CmdSetOptimSubstrate_Thickness = cmdSetOptimSubstrate_Thickness;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetOptimSubstrate))) return;
            CheckRecv((int)Common.Command.CmdSetOptimSubstrate);
        }

        public void Action_CmdGetOptimSubstrate()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetOptimSubstrate))) return;
            CheckRecv((int)Common.Command.CmdGetOptimSubstrate);
        }

        /// <summary>
        /// 4 bytes int(0 - 4), 4 bytes int(1 - 999), 4 bytes float(0.0 - 100000.0)
        /// </summary>
        /// <param name="cmdSetOptimLayerModel_NumOfLayers"></param>
        /// <param name="cmdSetOptimLayerModel_LayerMaterialID"></param>
        /// <param name="cmdSetOptimLayerModel_Thickness"></param>
        public void Action_CmdSetOptimLayerModel(int cmdSetOptimLayerModel_NumOfLayers, List<int> cmdSetOptimLayerModel_LayerMaterialID, List<float> cmdSetOptimLayerModel_Thickness)
        {
            Common.CmdSetOptimLayerModel_LayerMaterialID.Clear();
            Common.CmdSetOptimLayerModel_Thickness.Clear();

            Common.CmdSetOptimLayerModel_NumOfLayers = cmdSetOptimLayerModel_NumOfLayers;
            Common.CmdSetOptimLayerModel_LayerMaterialID = cmdSetOptimLayerModel_LayerMaterialID.ToList();
            Common.CmdSetOptimLayerModel_Thickness = cmdSetOptimLayerModel_Thickness.ToList();

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetOptimLayerModel))) return;
            CheckRecv((int)Common.Command.CmdSetOptimLayerModel);
        }

        public void Action_CmdGetOptimLayerModel()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetOptimLayerModel))) return;
            CheckRecv((int)Common.Command.CmdGetOptimLayerModel);
        }

        /// <summary>
        /// 4 bytes float(0.0 - 100000.0)
        /// </summary>
        /// <param name="cmdSetOptimSwitchThickness_Thickness"></param>
        public void Action_CmdSetOptimSwitchThickness(float cmdSetOptimSwitchThickness_Thickness)
        {
            Common.CmdSetOptimSwitchThickness_Thickness = cmdSetOptimSwitchThickness_Thickness;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetOptimSwitchThickness))) return;
            CheckRecv((int)Common.Command.CmdSetOptimSwitchThickness);
        }

        public void Action_CmdGetOptimSwitchThickness()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetOptimSwitchThickness))) return;
            CheckRecv((int)Common.Command.CmdGetOptimSwitchThickness);
        }

        /// <summary>
        /// 4 bytes float(0.0 - 100000.0)
        /// </summary>
        /// <param name="cmdSetOptimThicknessStep_ThicknessStep"></param>
        public void Action_CmdSetOptimThicknessStep(float cmdSetOptimThicknessStep_ThicknessStep)
        {
            Common.CmdSetOptimThicknessStep_ThicknessStep = cmdSetOptimThicknessStep_ThicknessStep;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetOptimThicknessStep))) return;
            CheckRecv((int)Common.Command.CmdSetOptimThicknessStep);
        }

        public void Action_CmdGetOptimThicknessStep()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetOptimThicknessStep))) return;
            CheckRecv((int)Common.Command.CmdGetOptimThicknessStep);
        }

        /// <summary>
        /// 4 bytes float(0.0 - 100000.0)
        /// </summary>
        /// <param name="cmdSetFollowUpFilter_FollowUpFilterPlusMinusRange"></param>
        public void Action_CmdSetFollowUpFilter(float cmdSetFollowUpFilter_FollowUpFilterPlusMinusRange)
        {
            Common.CmdSetFollowUpFilter_FollowUpFilterPlusMinusRange = cmdSetFollowUpFilter_FollowUpFilterPlusMinusRange;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetFollowUpFilter))) return;
            CheckRecv((int)Common.Command.CmdSetFollowUpFilter);
        }

        public void Action_CmdGetFollowUpFilter()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetFollowUpFilter))) return;
            CheckRecv((int)Common.Command.CmdGetFollowUpFilter);
        }

        /// <summary>
        /// 4 bytes int(0 - 2147483647)(sec)
        /// </summary>
        /// <param name="cmdSetFilterApplyTime_FollowUpFilterApplicationTime"></param>
        public void Action_CmdSetFilterApplyTime(int cmdSetFilterApplyTime_FollowUpFilterApplicationTime)
        {
            Common.CmdSetFilterApplyTime_FollowUpFilterApplicationTime = cmdSetFilterApplyTime_FollowUpFilterApplicationTime;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetFilterApplyTime))) return;
            CheckRecv((int)Common.Command.CmdSetFilterApplyTime);
        }

        public void Action_CmdGetFilterApplyTime()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetFilterApplyTime))) return;
            CheckRecv((int)Common.Command.CmdGetFilterApplyTime);
        }

        /// <summary>
        /// 4 bytes int(1 - 99999)
        /// </summary>
        /// <param name="cmdSetFilterCenterMA_FollowUpFilterCenterValueMovingAverage"></param>
        public void Action_CmdSetFilterCenterMA(int cmdSetFilterCenterMA_FollowUpFilterCenterValueMovingAverage)
        {
            Common.CmdSetFilterCenterMA_FollowUpFilterCenterValueMovingAverage = cmdSetFilterCenterMA_FollowUpFilterCenterValueMovingAverage;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetFilterCenterMA))) return;
            CheckRecv((int)Common.Command.CmdSetFilterCenterMA);
        }

        public void Action_CmdGetFilterCenterMA()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetFilterCenterMA))) return;
            CheckRecv((int)Common.Command.CmdGetFilterCenterMA);
        }

        public void Action_CmdSetFilterTrend(Common.CmdSetFilterTrend cmdSetFilterTrend)
        {
            Common.CmdSetFilterTrend_enum = (int)cmdSetFilterTrend;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetFilterTrend))) return;
            CheckRecv((int)Common.Command.CmdSetFilterTrend);
        }

        public void Action_CmdGetFilterTrend()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetFilterTrend))) return;
            CheckRecv((int)Common.Command.CmdGetFilterTrend);
        }

        /// <summary>
        /// 4 bytes int(1 - 99999)
        /// </summary>
        /// <param name="cmdSetThicknessMA_ThicknessMovingAverageFrequency"></param>
        public void Action_CmdSetThicknessMA(int cmdSetThicknessMA_ThicknessMovingAverageFrequency)
        {
            Common.CmdSetThicknessMA_ThicknessMovingAverageFrequency = cmdSetThicknessMA_ThicknessMovingAverageFrequency;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetThicknessMA))) return;
            CheckRecv((int)Common.Command.CmdSetThicknessMA);
        }

        public void Action_CmdGetThicknessMA()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetThicknessMA))) return;
            CheckRecv((int)Common.Command.CmdGetThicknessMA);
        }

        /// <summary>
        /// 4 bytes int(0 - 2147483647)
        /// </summary>
        /// <param name="cmdSetErrorVaule_ErrorVaule"></param>
        public void Action_CmdSetErrorVaule(int cmdSetErrorVaule_ErrorVaule)
        {
            Common.CmdSetErrorVaule_ErrorVaule = cmdSetErrorVaule_ErrorVaule;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSetErrorVaule))) return;
            CheckRecv((int)Common.Command.CmdSetErrorVaule);
        }

        public void Action_CmdGetErrorVaule()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetErrorVaule))) return;
            CheckRecv((int)Common.Command.CmdGetErrorVaule);
        }

        /// <summary>
        /// 4 bytes int(1 - 999), ASCII
        /// </summary>
        /// <param name="cmdSaveRecipeID_RecipeID"></param>
        /// <param name="cmdSaveRecipeID_RecipeName"></param>
        public void Action_CmdSaveRecipeID(int cmdSaveRecipeID_RecipeID, string cmdSaveRecipeID_RecipeName)
        {
            Common.CmdSaveRecipeID_RecipeID = cmdSaveRecipeID_RecipeID;
            Common.CmdSaveRecipeID_RecipeName = cmdSaveRecipeID_RecipeName;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSaveRecipeID))) return;
            CheckRecv((int)Common.Command.CmdSaveRecipeID);
        }

        public void Action_CmdGetRecipeList()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetRecipeList))) return;
            CheckRecv((int)Common.Command.CmdGetRecipeList);
        }

        /// <summary>
        /// 4 bytes int(1 - 999)
        /// </summary>
        /// <param name="cmdDelRecipeID_RecipeID"></param>
        public void Action_CmdDelRecipeID(int cmdDelRecipeID_RecipeID)
        {
            Common.CmdDelRecipeID_RecipeID = cmdDelRecipeID_RecipeID;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdDelRecipeID))) return;
            CheckRecv((int)Common.Command.CmdDelRecipeID);
        }

        /// <summary>
        /// 4 bytes int(0 - 100), 4 bytes int(2 - 3000), 4 bytes int(1 - 999), ASCII, 4 bytes float(100.0 - 3000.0), 4 bytes float, 4 bytes float
        /// </summary>
        /// <param name="cmdSaveMaterial_NumOfLettersOfMaterialName"></param>
        /// <param name="cmdSaveMaterial_DataQuantity"></param>
        /// <param name="cmdSaveMaterial_MaterialID"></param>
        /// <param name="cmdSaveMaterial_MaterialName"></param>
        /// <param name="cmdSaveMaterial_Wavelength"></param>
        /// <param name="cmdSaveMaterial_RefractiveIndex"></param>
        /// <param name="cmdSaveMaterial_ExtinctionCoefficient"></param>
        public void Action_CmdSaveMaterial(int cmdSaveMaterial_NumOfLettersOfMaterialName, int cmdSaveMaterial_DataQuantity, int cmdSaveMaterial_MaterialID, string cmdSaveMaterial_MaterialName, List<float> cmdSaveMaterial_Wavelength, List<float> cmdSaveMaterial_RefractiveIndex, List<float> cmdSaveMaterial_ExtinctionCoefficient)
        {
            Common.CmdSaveMaterial_Wavelength.Clear();
            Common.CmdSaveMaterial_RefractiveIndex.Clear();
            Common.CmdSaveMaterial_ExtinctionCoefficient.Clear();

            Common.CmdSaveMaterial_NumOfLettersOfMaterialName = cmdSaveMaterial_NumOfLettersOfMaterialName;
            Common.CmdSaveMaterial_DataQuantity = cmdSaveMaterial_DataQuantity;
            Common.CmdSaveMaterial_MaterialID = cmdSaveMaterial_MaterialID;
            Common.CmdSaveMaterial_MaterialName = cmdSaveMaterial_MaterialName;
            Common.CmdSaveMaterial_Wavelength = cmdSaveMaterial_Wavelength;
            Common.CmdSaveMaterial_RefractiveIndex = cmdSaveMaterial_RefractiveIndex;
            Common.CmdSaveMaterial_ExtinctionCoefficient = cmdSaveMaterial_ExtinctionCoefficient;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdSaveMaterial))) return;
            CheckRecv((int)Common.Command.CmdSaveMaterial);
        }

        /// <summary>
        /// 4 bytes int(1 - 999)
        /// </summary>
        /// <param name="cmdGetMaterial_MaterialID"></param>
        public void Action_CmdGetMaterial(int cmdGetMaterial_MaterialID)
        {
            Common.CmdGetMaterial_MaterialID = cmdGetMaterial_MaterialID;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetMaterial))) return;
            CheckGetMaterial();
        }

        public void Action_CmdGetMaterialList()
        {
            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdGetMaterialList))) return;
            CheckRecv((int)Common.Command.CmdGetMaterialList);
        }

        /// <summary>
        /// 4 bytes int(1 - 999)
        /// </summary>
        /// <param name="cmdDelMaterial_MaterialID"></param>
        public void Action_CmdDelMaterial(int cmdDelMaterial_MaterialID)
        {
            Common.CmdDelMaterial_MaterialID = cmdDelMaterial_MaterialID;

            ResetBeforeSend();
            if (!Tcp.Send(ConvToSendBytes((int)Common.Command.CmdDelMaterial))) return;
            CheckRecv((int)Common.Command.CmdDelMaterial);
        }

        void ResetBeforeSend()
        {
            isTimeout = false;
            sResponseCode = "";
            sStatus = "";
            Tcp.ClearRecvBuffer();
        }

        byte[] ConvToSendBytes(int iCmd)
        {
            string temp = "";
            iSerialNumber++;
            if (iSerialNumber > 9999) iSerialNumber = 1;

            List<byte> mList = new List<byte>();
            switch (iCmd)
            {
                case 10001: //READY (confirm communication state)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},READY"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10002: //SET-DATETIME (adjust date and time)

                    mList.Add(byteSTX);
                    mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-DATETIME,{Common.CmdSetDateTime_DataTime}"));
                    mList.Add(byteETX);

                    break;

                case 10003: //MEAS-REFERENCE (reference measurement)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},MEAS-REFERENCE,{Common.CmdMeasReference_ExposureTime},{Common.CmdMeasReference_NumOfAccumulations}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdMeasReference_ExposureTime));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdMeasReference_NumOfAccumulations));
                    }
                    break;

                case 10004: //SET-REFERENCE (reference settings)
                    if (blnAscii)
                    {
                        temp = "";
                        mList.Add(byteSTX);

                        for (int i = 0; i < Common.CmdSetReference_NumOfsignals; i++)
                            temp = temp + Common.CmdSetReference_ReferenceDarkSpectrum[i].ToString() + ",";

                        for (int j = 0; j < Common.CmdSetReference_NumOfsignals; j++)
                            temp = temp + Common.CmdSetReference_ReferenceSignalSpectrum[j].ToString() + ",";

                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-REFERENCE,{Common.CmdSetReference_ExposureTime},{Common.CmdSetReference_NumOfAccumulations},{Common.CmdSetReference_NumOfsignals},{temp.Trim(',')}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetReference_ExposureTime));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetReference_NumOfAccumulations));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetReference_NumOfsignals));

                        for (int i = 0; i < Common.CmdSetReference_NumOfsignals; i++)
                            mList.AddRange(BitConverter.GetBytes(Common.CmdSetReference_ReferenceDarkSpectrum[i]));

                        for (int j = 0; j < Common.CmdSetReference_NumOfsignals; j++)
                            mList.AddRange(BitConverter.GetBytes(Common.CmdSetReference_ReferenceSignalSpectrum[j]));
                    }
                    break;

                case 10005: //GET-REFERENCE (get reference)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-REFERENCE"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10006: //UPDATE-REFERENCE (update device reference)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},UPDATE-REFERENCE"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10007: //LOAD-REFERENCE (load device reference)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},LOAD-REFERENCE"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10008: //LOAD-DEFAULT-RECIPE (load default recipe)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},LOAD-DEFAULT-RECIPE"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10009: //LOAD-RECIPE-ID (load specified recipe)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},LOAD-RECIPE-ID,{Common.CmdLoadRecipeID_RecipeID}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdLoadRecipeID_RecipeID));
                    }
                    break;

                case 10010: //MEAS-DARK (measure sample dark)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},MEAS-DARK"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10011: //MAKE-SIMULATION-DATA (create analysis data for optimization method)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},MAKE-SIMULATION-DATA"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10012: //MEAS-SIGNAL (signal measurement)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},MEAS-SIGNAL,{Common.CmdMeasSignal_ExposureTime},{Common.CmdMeasSignal_NumOfAccumulations}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdMeasSignal_ExposureTime));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdMeasSignal_NumOfAccumulations));
                    }
                    break;

                case 10013: //MEAS-START (start measurement)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},MEAS-START"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10014: //MEAS-STOP (abort measurement)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},MEAS-STOP"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10015: //GET-RESULT (get result)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-RESULT"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10016: //CTRL-LIGHT (light control)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},CTRL-LIGHT,{Common.CmdCtrlLight_enum}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdCtrlLight_enum));
                    }
                    break;

                case 10017: //GET-STATUS (get latest status)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-STATUS"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10018: //GET-FFT-POWER-XAXIS (get power spectrum horizontal axis value)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FFT-POWER-XAXIS"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10019: //REANALYZE (reanalyze thickness)
                    if (blnAscii)
                    {
                        temp = "";
                        mList.Add(byteSTX);

                        for (int i = 0; i < Common.CmdReanalyze_NumOfReflectances; i++)
                            temp = temp + Common.CmdReanalyze_ReflectanceSpectrum[i].ToString() + ",";

                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},REANALYZE,{Common.CmdReanalyze_NumOfReflectances},{temp.Trim(',')}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdReanalyze_NumOfReflectances));

                        for (int i = 0; i < Common.CmdReanalyze_NumOfReflectances; i++)
                            mList.AddRange(BitConverter.GetBytes(Common.CmdReanalyze_ReflectanceSpectrum[i]));
                    }
                    break;

                case 20001: //UPDATE-ALL (update all device settings)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},UPDATE-ALL"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20002: //LOAD-DEFAULT-ALL (load all device settings)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},LOAD-DEFAULT-ALL"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20003: //UPDATE-SYSTEM-CONFIG (update device system settings)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},UPDATE-SYSTEM-CONFIG"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20004: //LOAD-SYSTEM-CONFIG (load device system settings)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},LOAD-SYSTEM-CONFIG"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20005: //SET-TRIGGER-EDGE (set trigger input edge)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-TRIGGER-EDGE,{Common.CmdSetTriggerEdge_enum}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetTriggerEdge_enum));
                    }
                    break;

                case 20006: //GET-TRIGGER-EDGE (get trigger input edge)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-TRIGGER-EDGE"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20007: //SET-TRIGGER-FILTER (set trigger input digital filter)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-TRIGGER-FILTER,{Common.CmdSetTriggerFilter_Time}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetTriggerFilter_Time));
                    }
                    break;

                case 20008: //GET-TRIGGER-FILTER (get trigger input digital filter)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-TRIGGER-FILTER"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20009: //SET-USE-ANALOG (set analog output usage setting)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-USE-ANALOG,{Common.CmdSetUseAnalog_enum}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetUseAnalog_enum));
                    }
                    break;

                case 20010: //GET-USE-ANALOG (get analog output usage setting)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-USE-ANALOG"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20011: //SET-ANALOG-RANGE (set analog output thickness range)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ANALOG-RANGE,{Common.CmdSetAnalogRange_LowerLimit},{Common.CmdSetAnalogRange_UpperLimit}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetAnalogRange_LowerLimit));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetAnalogRange_UpperLimit));
                    }
                    break;

                case 20012: //SET-ANALOG-RANGE (set analog output thickness range)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ANALOG-RANGE,{Common.CmdSetAnalogRange_LowerLimit},{Common.CmdSetAnalogRange_UpperLimit}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetAnalogRange_LowerLimit));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetAnalogRange_UpperLimit));
                    }
                    break;

                case 20013: //SET-IP-ADDRESS (set IP address and port number)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-IP-ADDRESS,{Common.IPAddress}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        string[] subs = Common.IPAddress.Split();
                        foreach (string sub in subs)
                            mList.AddRange(BitConverter.GetBytes(Convert.ToInt32(sub)));
                    }
                    break;

                case 20014: //TEST-ANALOG (confirm analog output value)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},TEST-ANALOG,{Common.CmdTestAnalog_Thickness}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdTestAnalog_Thickness));
                    }
                    break;

                case 20015: //SET-CALIBRATE-THICKNESS (set thickness calibration value)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-CALIBRATE-THICKNESS,{Common.CmdSetCalibrateThickness_StandardThicknessValue1},{Common.CmdSetCalibrateThickness_MeasurementThicknessValue1},{Common.CmdSetCalibrateThickness_StandardThicknessValue2},{Common.CmdSetCalibrateThickness_MeasurementThicknessValue2}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetCalibrateThickness_StandardThicknessValue1));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetCalibrateThickness_MeasurementThicknessValue1));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetCalibrateThickness_StandardThicknessValue2));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetCalibrateThickness_MeasurementThicknessValue2));
                    }
                    break;

                case 20016: //GET-CALIBRATE-THICKNESS (get thickness calibration value)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-CALIBRATE-THICKNESS"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20017: //SET-PROTOCOL (set data transmission protocol)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-PROTOCOL,{Common.CmdSetProtocol_enum}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetProtocol_enum));
                    }
                    break;

                case 20018: //GET-PROTOCOL (get data transmission protocol)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-PROTOCOL"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30001: //UPDATE-RECIPE (update default recipe)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},UPDATE-RECIPE"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30002: //SET-TRIGGER-RESET-TIME (set reset elapsed time on trigger input)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-TRIGGER-RESET-TIME,{Common.CmdSetTriggerResetTime_enum}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetTriggerResetTime_enum));
                    }
                    break;

                case 30003: //GET-TRIGGER-RESET-TIME (get reset elapsed time on trigger input)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-TRIGGER-RESET-TIME"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30004: //SET-RESULT-TYPE (set result data type)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-RESULT-TYPE,{Common.CmdSetResultType_enum}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetResultType_enum));
                    }
                    break;

                case 30005: //GET-RESULT-TYPE (get result data type)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-RESULT-TYPE"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30006: //SET-MEAS-MODE (set measurement mode)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-MEAS-MODE,{Common.CmdSetMeasMode_enum}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetMeasMode_enum));
                    }
                    break;

                case 30007: //GET-MEAS-MODE (get measurement mode)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-MEAS-MODE"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30008: //SET-MEAS-COUNT (set no. of continuous measurements)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-MEAS-COUNT,{Common.CmdSetMeasCount_NumOfContinuousMeasurement}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetMeasCount_NumOfContinuousMeasurement));
                    }
                    break;

                case 30009: //GET-MEAS-COUNT (get no. of continuous measurements)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-MEAS-COUNT"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30010: //SET-MEAS-CYCLE (set measurement cycle)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-MEAS-CYCLE,{Common.CmdSetMeasCycle_MeasurementCycle}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetMeasCycle_MeasurementCycle));
                    }
                    break;

                case 30011: //GET-MEAS-CYCLE (get measurement cycle)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-MEAS-CYCLE"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30012: //SET-EXPOSURE (set exposure time)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-EXPOSURE,{Common.CmdSetExposure_ExposureTime}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetExposure_ExposureTime));
                    }
                    break;

                case 30013: //GET-EXPOSURE (get exposure time)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-EXPOSURE"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30014: //SET-ACCUMULATION (set no. of accumulations)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ACCUMULATION,{Common.CmdSetAccumulation_NumOfAccumulations}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetAccumulation_NumOfAccumulations));
                    }
                    break;

                case 30015: //GET-ACCUMULATION (get no. of accumulations)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-ACCUMULATION"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30016: //SET-TRIGGER-DELAY (set trigger delay time)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-TRIGGER-DELAY,{Common.CmdSetTriggerDelay_TriggerDelayTime}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetTriggerDelay_TriggerDelayTime));
                    }
                    break;

                case 30017: //GET-TRIGGER-DELAY (get trigger delay time)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-TRIGGER-DELAY"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40001: //SET-ANALYSIS-RANGE (set analysis wavelength number)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ANALYSIS-RANGE,{Common.CmdSetAnalysisRange_AnalysisStartWavelengthNum},{Common.CmdSetAnalysisRange_AnalysisEndWavelengthNum}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetAnalysisRange_AnalysisStartWavelengthNum));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetAnalysisRange_AnalysisEndWavelengthNum));
                    }
                    break;

                case 40002: //GET-ANALYSIS-RANGE (get analysis wavelength number)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-ANALYSIS-RANGE"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40003: //SET-ANALYSIS-METHOD (set analysis method)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ANALYSIS-METHOD,{Common.CmdSetAnalysisMethod_enum}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetAnalysisMethod_enum));
                    }
                    break;

                case 40004: //GET-ANALYSIS-METHOD (get analysis method)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-ANALYSIS-METHOD"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40005: //SET-ANALYSIS-NUM (set no. of analysis layers)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ANALYSIS-NUM,{Common.CmdSetAnalysisNum_NumOfAnalysisLayers}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetAnalysisNum_NumOfAnalysisLayers));
                    }
                    break;

                case 40006: //GET-ANALYSIS-NUM (get no. of analysis layers)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-ANALYSIS-NUM"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40007: //SET-SMOOTHING (set smoothing point)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-SMOOTHING,{Common.CmdSetSmoothing_enum}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetSmoothing_enum));
                    }
                    break;

                case 40008: //GET-SMOOTHING (get smoothing point)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-SMOOTHING"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40009: //SET-FFTNUM (set no. of FFT data points)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FFTNUM,{Common.CmdSetFFTNum_enum}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetFFTNum_enum));
                    }
                    break;

                case 40010: //GET-FFTNUM (get no. of FFT data points)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FFTNUM"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40011: //SET-FFT-MAXTHICKNESS (set FFT maximum thickness)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FFT-MAXTHICKNESS,{Common.CmdSetFFTMaxThickness_FFTMaxThickness}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetFFTMaxThickness_FFTMaxThickness));
                    }
                    break;

                case 40012: //GET-FFT-MAXTHICKNESS (get FFT maximum thickness)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FFT-MAXTHICKNESS"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40013: //SET-FFT-WINDOW (set FFT window function)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FFT-WINDOW,{Common.CmdSetFFTWindow_enum}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetFFTWindow_enum));
                    }
                    break;

                case 40014: //GET-FFT-WINDOW (get FFT window function)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FFT-WINDOW"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40015: //SET-FFT-NORMALIZATION (set FFT normalization)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FFT-NORMALIZATION,{Common.CmdSetFFTNormalization_enum}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetFFTNormalization_enum));
                    }
                    break;

                case 40016: //GET-FFT-NORMALIZATION (get FFT normalization)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FFT-NORMALIZATION"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40017: //SET-USE-PEAK-JUDGE (set NG peak exclusion)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-USE-PEAK-JUDGE,{Common.CmdSetUsePeakJudge_enum}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetUsePeakJudge_enum));
                    }
                    break;

                case 40018: //GET-USE-PEAK-JUDGE (get NG peak exclusion)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-USE-PEAK-JUDGE"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40019: //SET-ANALYSIS-MATERIAL (set analysis layer material)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ANALYSIS-MATERIAL,{Common.CmdSetAnalysisMaterial_AnalysisLayerMaterialID}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetAnalysisMaterial_AnalysisLayerMaterialID));
                    }
                    break;

                case 40020: //GET-ANALYSIS-MATERIAL (get analysis layer material)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-ANALYSIS-MATERIAL"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40021: //SET-ANALYSIS-REFRACTIVEINDEX (set analysis layer refractive index)
                    if (blnAscii)
                    {
                        temp = "";
                        mList.Add(byteSTX);

                        for (int i = 0; i < Common.CmdSetAnalysisRefractiveIndex_DataQuantity; i++)
                            temp = temp + Common.CmdSetAnalysisRefractiveIndex_ConstantRefractiveIndex[i].ToString() + ",";

                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ANALYSIS-REFRACTIVEINDEX,{Common.CmdSetAnalysisRefractiveIndex_DataQuantity},{temp.Trim(',')}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetAnalysisRefractiveIndex_DataQuantity));

                        for (int i = 0; i < Common.CmdSetAnalysisRefractiveIndex_DataQuantity; i++)
                            mList.AddRange(BitConverter.GetBytes(Common.CmdSetAnalysisRefractiveIndex_ConstantRefractiveIndex[i]));
                    }
                    break;

                case 40022: //GET-ANALYSIS-REFRACTIVEINDEX (get analysis layer refractive index)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-ANALYSIS-REFRACTIVEINDEX"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40023: //SET-ANALYSIS-THICKNESS-RANGE (set analysis thickness range)
                    if (blnAscii)
                    {
                        temp = "";

                        for (int i = 0; i < Common.CmdSetAnalysisThicknessRange_DataQuantity; i++)
                        {
                            temp = temp + Common.CmdSetAnalysisThicknessRange_ThicknessRangeLowerLimit[i].ToString() + ",";
                            temp = temp + Common.CmdSetAnalysisThicknessRange_ThicknessRangeUpperLimit[i].ToString() + ",";
                        }

                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ANALYSIS-THICKNESS-RANGE,{Common.CmdSetAnalysisThicknessRange_DataQuantity},{temp.Trim(',')}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetAnalysisThicknessRange_DataQuantity));

                        for (int i = 0; i < Common.CmdSetAnalysisThicknessRange_DataQuantity; i++)
                        {
                            mList.AddRange(BitConverter.GetBytes(Common.CmdSetAnalysisThicknessRange_ThicknessRangeLowerLimit[i]));
                            mList.AddRange(BitConverter.GetBytes(Common.CmdSetAnalysisThicknessRange_ThicknessRangeUpperLimit[i]));
                        }
                    }
                    break;

                case 40024: //GET-ANALYSIS-THICKNESS-RANGE (get analysis thickness range)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-ANALYSIS-THICKNESS-RANGE"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40025: //SET-FFT-POWER-RANGE (set FFT intensity range)
                    if (blnAscii)
                    {
                        temp = "";

                        for (int i = 0; i < Common.CmdSetFFTPowerRange_DataQuantity; i++)
                        {
                            temp = temp + Common.CmdSetFFTPowerRange_FFTIntensityRangeLowerLimit[i].ToString() + ",";
                            temp = temp + Common.CmdSetFFTPowerRange_FFTIntensityRangeUpperLimit[i].ToString() + ",";
                        }

                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FFT-POWER-RANGE,{Common.CmdSetFFTPowerRange_DataQuantity},{temp.Trim(',')}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetFFTPowerRange_DataQuantity));

                        for (int i = 0; i < Common.CmdSetFFTPowerRange_DataQuantity; i++)
                        {
                            mList.AddRange(BitConverter.GetBytes(Common.CmdSetFFTPowerRange_FFTIntensityRangeLowerLimit[i]));
                            mList.AddRange(BitConverter.GetBytes(Common.CmdSetFFTPowerRange_FFTIntensityRangeUpperLimit[i]));
                        }
                    }
                    break;

                case 40026: //GET-FFT-POWER-RANGE (get FFT intensity range)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FFT-POWER-RANGE"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40027: //SET-FFT-SEARCH-DIRECTION (set FFT peak search direction)
                    if (blnAscii)
                    {
                        temp = "";

                        for (int i = 0; i < Common.CmdSetFFTSearchDirection_DataQuantity; i++)
                            temp = temp + Common.CmdSetFFTSearchDirection_list[i].ToString() + ",";

                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FFT-SEARCH-DIRECTION,{Common.CmdSetFFTSearchDirection_DataQuantity},{temp.Trim(',')}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetFFTPowerRange_DataQuantity));

                        for (int i = 0; i < Common.CmdSetFFTPowerRange_DataQuantity; i++)
                            mList.AddRange(BitConverter.GetBytes(Common.CmdSetFFTSearchDirection_list[i]));
                    }
                    break;

                case 40028: //GET-FFT-SEARCH-DIRECTION (get FFT peak search direction)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FFT-SEARCH-DIRECTION"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40029: //SET-FFT-PEAKNUM (set FFT peak number)
                    if (blnAscii)
                    {
                        temp = "";

                        for (int i = 0; i < Common.CmdSetFFTPeakNum_DataQuantity; i++)
                            temp = temp + Common.CmdSetFFTPeakNum_PeakNumber[i].ToString() + ",";

                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FFT-PEAKNUM,{Common.CmdSetFFTPeakNum_DataQuantity},{temp.Trim(',')}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetFFTPeakNum_DataQuantity));

                        for (int i = 0; i < Common.CmdSetFFTPeakNum_DataQuantity; i++)
                            mList.AddRange(BitConverter.GetBytes(Common.CmdSetFFTPeakNum_PeakNumber[i]));
                    }
                    break;

                case 40030: //GET-FFT-PEAKNUM (get FFT peak number)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FFT-PEAKNUM"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40031: //SET-THICKNESS-COEF (set thickness coefficient)
                    if (blnAscii)
                    {
                        temp = "";

                        for (int i = 0; i < Common.CmdSetThicknessCoef_DataQuantity; i++)
                        {
                            temp = temp + Common.CmdSetThicknessCoef_list[i].ToString() + ",";
                            temp = temp + Common.CmdSetThicknessCoef_PrimaryCoefficientC1[i].ToString() + ",";
                            temp = temp + Common.CmdSetThicknessCoef_ConstantCoefficientC0[i].ToString() + ",";
                        }

                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-THICKNESS-COEF,{Common.CmdSetThicknessCoef_DataQuantity},{temp.Trim(',')}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetThicknessCoef_DataQuantity));

                        for (int i = 0; i < Common.CmdSetThicknessCoef_DataQuantity; i++)
                        {
                            mList.AddRange(BitConverter.GetBytes((int)Common.CmdSetThicknessCoef_list[i]));
                            mList.AddRange(BitConverter.GetBytes(Common.CmdSetThicknessCoef_PrimaryCoefficientC1[i]));
                            mList.AddRange(BitConverter.GetBytes(Common.CmdSetThicknessCoef_ConstantCoefficientC0[i]));
                        }
                    }
                    break;

                case 40032: //GET-THICKNESS-COEF (get thickness coefficient)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-THICKNESS-COEF"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 50001: //SET-OPTIM-AMBIENT (set ambient layer material)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-OPTIM-AMBIENT,{Common.CmdSetOptimAmbient_AmbientLayerMaterialID}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetOptimAmbient_AmbientLayerMaterialID));
                    }
                    break;

                case 50002: //GET-OPTIM-AMBIENT (get ambient layer material)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-OPTIM-AMBIENT"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 50003: //SET-OPTIM-SUBSTRATE (set substrate)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-OPTIM-SUBSTRATE,{Common.CmdSetOptimSubstrate_SubstrateMaterialID},{Common.CmdSetOptimSubstrate_Thickness}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetOptimSubstrate_SubstrateMaterialID));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetOptimSubstrate_Thickness));
                    }
                    break;

                case 50004: //GET-OPTIM-SUBSTRATE (get substrate)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-OPTIM-SUBSTRATE"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 50005: //SET-OPTIM-LAYERMODEL (set intermediate layers)
                    if (blnAscii)
                    {
                        temp = "";

                        for (int i = 0; i < Common.CmdSetOptimLayerModel_NumOfLayers; i++)
                        {
                            temp = temp + Common.CmdSetOptimLayerModel_LayerMaterialID[i].ToString() + ",";
                            temp = temp + Common.CmdSetOptimLayerModel_Thickness[i].ToString() + ",";
                        }

                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-OPTIM-LAYERMODEL,{Common.CmdSetOptimLayerModel_NumOfLayers},{temp.Trim(',')}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetOptimLayerModel_NumOfLayers));

                        for (int i = 0; i < Common.CmdSetOptimLayerModel_NumOfLayers; i++)
                        {
                            mList.AddRange(BitConverter.GetBytes(Common.CmdSetOptimLayerModel_LayerMaterialID[i]));
                            mList.AddRange(BitConverter.GetBytes(Common.CmdSetOptimLayerModel_Thickness[i]));
                        }
                    }
                    break;

                case 50006: //GET-OPTIM-LAYERMODEL (get intermediate layers)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-OPTIM-LAYERMODEL"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 50007: //SET-OPTIM-SWITCH-THICKNESS (set optimization method switchover thickness)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-OPTIM-SWITCH-THICKNESS,{Common.CmdSetOptimSwitchThickness_Thickness}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetOptimSwitchThickness_Thickness));
                    }
                    break;

                case 50008: //GET-OPTIM-SWITCH-THICKNESS (get optimization method switchover thickness)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-OPTIM-SWITCH-THICKNESS"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 50009: //SET-OPTIM-THICKNESS-STEP (set optimization method thickness step value)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-OPTIM-THICKNESS-STEP,{Common.CmdSetOptimThicknessStep_ThicknessStep}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetOptimThicknessStep_ThicknessStep));
                    }
                    break;

                case 50010: //GET-OPTIM-THICKNESS-STEP (get optimization method thickness step value)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-OPTIM-THICKNESS-STEP"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 60001: //SET-FOLLOWUP-FILTER (set follow-up filter range)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FOLLOWUP-FILTER,{Common.CmdSetFollowUpFilter_FollowUpFilterPlusMinusRange}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetFollowUpFilter_FollowUpFilterPlusMinusRange));
                    }
                    break;

                case 60002: //GET-FOLLOWUP-FILTER (get follow-up filter range)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FOLLOWUP-FILTER"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 60003: //SET-FILTER-APPLYTIME (set follow-up filter application time)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FILTER-APPLYTIME,{Common.CmdSetFilterApplyTime_FollowUpFilterApplicationTime}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetFilterApplyTime_FollowUpFilterApplicationTime));
                    }
                    break;

                case 60004: //GET-FILTER-APPLYTIME (get follow-up filter application time)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FILTER-APPLYTIME"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 60005: //SET-FILTER-CENTER-MA (set follow-up filter center value moving average)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FILTER-CENTER-MA,{Common.CmdSetFilterCenterMA_FollowUpFilterCenterValueMovingAverage}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetFilterCenterMA_FollowUpFilterCenterValueMovingAverage));
                    }
                    break;

                case 60006: //GET-FILTER-CENTER-MA (get follow-up filter center value moving average)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FILTER-CENTER-MA"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 60007: //SET-FILTER-TREND (set thickness variation trend)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FILTER-TREND,{Common.CmdSetFilterTrend_enum}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetFilterTrend_enum));
                    }
                    break;

                case 60008: //GET-FILTER-TREND (get thickness variation trend)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FILTER-TREND"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 60009: //SET-THICKNESS-MA (set thickness moving average)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-THICKNESS-MA,{Common.CmdSetThicknessMA_ThicknessMovingAverageFrequency}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetThicknessMA_ThicknessMovingAverageFrequency));
                    }
                    break;

                case 60010: //GET-THICKNESS-MA (get thickness moving average count)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-THICKNESS-MA"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 60011: //SET-ERRORVALUE (set error value)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ERRORVALUE,{Common.CmdSetErrorVaule_ErrorVaule}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdSetErrorVaule_ErrorVaule));
                    }
                    break;

                case 60012: //GET-ERRORVALUE (get error value)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-ERRORVALUE"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 70001: //SAVE-RECIPE-ID (create recipe)

                    mList.Add(byteSTX);
                    mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SAVE-RECIPE-ID,{Common.CmdSaveRecipeID_RecipeID},{Common.CmdSaveRecipeID_RecipeName}"));
                    mList.Add(byteETX);

                    break;

                case 70002: //GET-RECIPELIST (get recipe list)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-RECIPELIST"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 70003: //DEL-RECIPE-ID (delete recipe)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},DEL-RECIPE-ID,{Common.CmdDelRecipeID_RecipeID}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdDelRecipeID_RecipeID));
                    }
                    break;

                case 80001: //SAVE-MATERIAL (create material)

                    temp = "";
                    mList.Add(byteSTX);

                    for (int i = 0; i < Common.CmdSaveMaterial_DataQuantity; i++)
                        temp = temp + Common.CmdSaveMaterial_Wavelength[i].ToString() + ",";

                    for (int j = 0; j < Common.CmdSaveMaterial_DataQuantity; j++)
                        temp = temp + Common.CmdSaveMaterial_RefractiveIndex[j].ToString() + ",";

                    for (int k = 0; k < Common.CmdSaveMaterial_DataQuantity; k++)
                        temp = temp + Common.CmdSaveMaterial_ExtinctionCoefficient[k].ToString() + ",";

                    mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SAVE-MATERIAL,{Common.CmdSaveMaterial_NumOfLettersOfMaterialName},{Common.CmdSaveMaterial_DataQuantity},{Common.CmdSaveMaterial_MaterialID},{Common.CmdSaveMaterial_MaterialName},{temp.Trim(',')}"));
                    mList.Add(byteETX);

                    break;

                case 80002: //GET-MATERIAL (get material)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-MATERIAL,{Common.CmdGetMaterial_MaterialID}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdGetMaterial_MaterialID));
                    }
                    break;

                case 80003: //GET-MATERIALLIST (get material list)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-MATERIALLIST"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 80004: //DEL-MATERIAL (delete material)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},DEL-MATERIAL,{Common.CmdDelMaterial_MaterialID}"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                        mList.AddRange(BitConverter.GetBytes(Common.CmdDelMaterial_MaterialID));
                    }
                    break;

                default:
                    break;
            }
            return mList.ToArray();
        }

        byte[] ConvToRecvBytes(int iCmd)
        {
            List<byte> mList = new List<byte>();
            switch (iCmd)
            {
                case 10001: //READY (confirm communication state)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},READY,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10002: //SET-DATETIME (adjust date and time)

                    mList.Add(byteSTX);
                    mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-DATETIME,"));

                    break;

                case 10003: //MEAS-REFERENCE (reference measurement)
                    if (blnAscii)
                    {
                        mList.Add(byteSOH);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10004: //SET-REFERENCE (reference settings)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-REFERENCE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10005: //GET-REFERENCE (get reference)
                    if (blnAscii)
                    {
                        mList.Add(byteSOH);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10006: //UPDATE-REFERENCE (update device reference)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},UPDATE-REFERENCE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10007: //LOAD-REFERENCE (load device reference)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},LOAD-REFERENCE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10008: //LOAD-DEFAULT-RECIPE (load default recipe)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},LOAD-DEFAULT-RECIPE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10009: //LOAD-RECIPE-ID (load specified recipe)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},LOAD-RECIPE-ID,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10010: //MEAS-DARK (measure sample dark)
                    if (blnAscii)
                    {
                        mList.Add(byteSOH);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10011: //MAKE-SIMULATION-DATA (create analysis data for optimization method)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},MAKE-SIMULATION-DATA,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10012: //MEAS-SIGNAL (signal measurement)
                    if (blnAscii)
                    {
                        mList.Add(byteSOH);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10013: //MEAS-START (start measurement)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},MEAS-START,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10014: //MEAS-STOP (abort measurement)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},MEAS-STOP,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10015: //GET-RESULT (get result)
                    if (blnAscii)
                    {
                        mList.Add(byteSOH);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10016: //CTRL-LIGHT (light control)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},CTRL-LIGHT,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10017: //GET-STATUS (get latest status)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-STATUS,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10018: //GET-FFT-POWER-XAXIS (get power spectrum horizontal axis value)
                    if (blnAscii)
                    {
                        mList.Add(byteSOH);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 10019: //REANALYZE (reanalyze thickness)
                    if (blnAscii)
                    {
                        mList.Add(byteSOH);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20001: //UPDATE-ALL (update all device settings)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},UPDATE-ALL,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20002: //LOAD-DEFAULT-ALL (load all device settings)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},LOAD-DEFAULT-ALL,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20003: //UPDATE-SYSTEM-CONFIG (update device system settings)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},UPDATE-SYSTEM-CONFIG,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20004: //LOAD-SYSTEM-CONFIG (load device system settings)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},LOAD-SYSTEM-CONFIG,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20005: //SET-TRIGGER-EDGE (set trigger input edge)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-TRIGGER-EDGE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20006: //GET-TRIGGER-EDGE (get trigger input edge)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-TRIGGER-EDGE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20007: //SET-TRIGGER-FILTER (set trigger input digital filter)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-TRIGGER-FILTER,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20008: //GET-TRIGGER-FILTER (get trigger input digital filter)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-TRIGGER-FILTER,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20009: //SET-USE-ANALOG (set analog output usage setting)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-USE-ANALOG,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20010: //GET-USE-ANALOG (get analog output usage setting)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-USE-ANALOG,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20011: //SET-ANALOG-RANGE (set analog output thickness range)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ANALOG-RANGE,"));
                        mList.Add(byteETX);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20012: //SET-ANALOG-RANGE (set analog output thickness range)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ANALOG-RANGE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20013: //SET-IP-ADDRESS (set IP address and port number)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-IP-ADDRESS,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20014: //TEST-ANALOG (confirm analog output value)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},TEST-ANALOG,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20015: //SET-CALIBRATE-THICKNESS (set thickness calibration value)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-CALIBRATE-THICKNESS,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20016: //GET-CALIBRATE-THICKNESS (get thickness calibration value)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-CALIBRATE-THICKNESS,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20017: //SET-PROTOCOL (set data transmission protocol)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-PROTOCOL,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 20018: //GET-PROTOCOL (get data transmission protocol)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-PROTOCOL,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30001: //UPDATE-RECIPE (update default recipe)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},UPDATE-RECIPE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30002: //SET-TRIGGER-RESET-TIME (set reset elapsed time on trigger input)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-TRIGGER-RESET-TIME,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30003: //GET-TRIGGER-RESET-TIME (get reset elapsed time on trigger input)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-TRIGGER-RESET-TIME,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30004: //SET-RESULT-TYPE (set result data type)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-RESULT-TYPE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30005: //GET-RESULT-TYPE (get result data type)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-RESULT-TYPE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30006: //SET-MEAS-MODE (set measurement mode)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-MEAS-MODE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30007: //GET-MEAS-MODE (get measurement mode)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-MEAS-MODE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30008: //SET-MEAS-COUNT (set no. of continuous measurements)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-MEAS-COUNT,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30009: //GET-MEAS-COUNT (get no. of continuous measurements)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-MEAS-COUNT,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30010: //SET-MEAS-CYCLE (set measurement cycle)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-MEAS-CYCLE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30011: //GET-MEAS-CYCLE (get measurement cycle)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-MEAS-CYCLE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30012: //SET-EXPOSURE (set exposure time)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-EXPOSURE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30013: //GET-EXPOSURE (get exposure time)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-EXPOSURE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30014: //SET-ACCUMULATION (set no. of accumulations)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ACCUMULATION,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30015: //GET-ACCUMULATION (get no. of accumulations)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-ACCUMULATION,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30016: //SET-TRIGGER-DELAY (set trigger delay time)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-TRIGGER-DELAY,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 30017: //GET-TRIGGER-DELAY (get trigger delay time)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-TRIGGER-DELAY,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40001: //SET-ANALYSIS-RANGE (set analysis wavelength number)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ANALYSIS-RANGE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40002: //GET-ANALYSIS-RANGE (get analysis wavelength number)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-ANALYSIS-RANGE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40003: //SET-ANALYSIS-METHOD (set analysis method)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ANALYSIS-METHOD,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40004: //GET-ANALYSIS-METHOD (get analysis method)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-ANALYSIS-METHOD,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40005: //SET-ANALYSIS-NUM (set no. of analysis layers)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ANALYSIS-NUM,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40006: //GET-ANALYSIS-NUM (get no. of analysis layers)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-ANALYSIS-NUM,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40007: //SET-SMOOTHING (set smoothing point)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-SMOOTHING,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40008: //GET-SMOOTHING (get smoothing point)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-SMOOTHING,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40009: //SET-FFTNUM (set no. of FFT data points)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FFTNUM,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40010: //GET-FFTNUM (get no. of FFT data points)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FFTNUM,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40011: //SET-FFT-MAXTHICKNESS (set FFT maximum thickness)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FFT-MAXTHICKNESS,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40012: //GET-FFT-MAXTHICKNESS (get FFT maximum thickness)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FFT-MAXTHICKNESS,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40013: //SET-FFT-WINDOW (set FFT window function)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FFT-WINDOW,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40014: //GET-FFT-WINDOW (get FFT window function)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FFT-WINDOW,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40015: //SET-FFT-NORMALIZATION (set FFT normalization)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FFT-NORMALIZATION,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40016: //GET-FFT-NORMALIZATION (get FFT normalization)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FFT-NORMALIZATION,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40017: //SET-USE-PEAK-JUDGE (set NG peak exclusion)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-USE-PEAK-JUDGE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40018: //GET-USE-PEAK-JUDGE (get NG peak exclusion)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-USE-PEAK-JUDGE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40019: //SET-ANALYSIS-MATERIAL (set analysis layer material)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ANALYSIS-MATERIAL,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40020: //GET-ANALYSIS-MATERIAL (get analysis layer material)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-ANALYSIS-MATERIAL,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40021: //SET-ANALYSIS-REFRACTIVEINDEX (set analysis layer refractive index)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ANALYSIS-REFRACTIVEINDEX,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40022: //GET-ANALYSIS-REFRACTIVEINDEX (get analysis layer refractive index)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-ANALYSIS-REFRACTIVEINDEX,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40023: //SET-ANALYSIS-THICKNESS-RANGE (set analysis thickness range)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ANALYSIS-THICKNESS-RANGE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40024: //GET-ANALYSIS-THICKNESS-RANGE (get analysis thickness range)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-ANALYSIS-THICKNESS-RANGE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40025: //SET-FFT-POWER-RANGE (set FFT intensity range)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FFT-POWER-RANGE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40026: //GET-FFT-POWER-RANGE (get FFT intensity range)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FFT-POWER-RANGE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40027: //SET-FFT-SEARCH-DIRECTION (set FFT peak search direction)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FFT-SEARCH-DIRECTION,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40028: //GET-FFT-SEARCH-DIRECTION (get FFT peak search direction)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FFT-SEARCH-DIRECTION,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40029: //SET-FFT-PEAKNUM (set FFT peak number)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FFT-PEAKNUM,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40030: //GET-FFT-PEAKNUM (get FFT peak number)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FFT-PEAKNUM,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40031: //SET-THICKNESS-COEF (set thickness coefficient)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-THICKNESS-COEF,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 40032: //GET-THICKNESS-COEF (get thickness coefficient)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-THICKNESS-COEF,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 50001: //SET-OPTIM-AMBIENT (set ambient layer material)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-OPTIM-AMBIENT,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 50002: //GET-OPTIM-AMBIENT (get ambient layer material)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-OPTIM-AMBIENT,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 50003: //SET-OPTIM-SUBSTRATE (set substrate)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-OPTIM-SUBSTRATE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 50004: //GET-OPTIM-SUBSTRATE (get substrate)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-OPTIM-SUBSTRATE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 50005: //SET-OPTIM-LAYERMODEL (set intermediate layers)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-OPTIM-LAYERMODEL,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 50006: //GET-OPTIM-LAYERMODEL (get intermediate layers)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-OPTIM-LAYERMODEL,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 50007: //SET-OPTIM-SWITCH-THICKNESS (set optimization method switchover thickness)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-OPTIM-SWITCH-THICKNESS,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 50008: //GET-OPTIM-SWITCH-THICKNESS (get optimization method switchover thickness)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-OPTIM-SWITCH-THICKNESS,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 50009: //SET-OPTIM-THICKNESS-STEP (set optimization method thickness step value)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-OPTIM-THICKNESS-STEP,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 50010: //GET-OPTIM-THICKNESS-STEP (get optimization method thickness step value)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-OPTIM-THICKNESS-STEP,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 60001: //SET-FOLLOWUP-FILTER (set follow-up filter range)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FOLLOWUP-FILTER,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 60002: //GET-FOLLOWUP-FILTER (get follow-up filter range)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FOLLOWUP-FILTER,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 60003: //SET-FILTER-APPLYTIME (set follow-up filter application time)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FILTER-APPLYTIME,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 60004: //GET-FILTER-APPLYTIME (get follow-up filter application time)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FILTER-APPLYTIME,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 60005: //SET-FILTER-CENTER-MA (set follow-up filter center value moving average)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FILTER-CENTER-MA,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 60006: //GET-FILTER-CENTER-MA (get follow-up filter center value moving average)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FILTER-CENTER-MA,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 60007: //SET-FILTER-TREND (set thickness variation trend)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-FILTER-TREND,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 60008: //GET-FILTER-TREND (get thickness variation trend)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-FILTER-TREND,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 60009: //SET-THICKNESS-MA (set thickness moving average)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-THICKNESS-MA,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 60010: //GET-THICKNESS-MA (get thickness moving average count)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-THICKNESS-MA,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 60011: //SET-ERRORVALUE (set error value)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SET-ERRORVALUE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 60012: //GET-ERRORVALUE (get error value)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-ERRORVALUE,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 70001: //SAVE-RECIPE-ID (create recipe)

                    mList.Add(byteSTX);
                    mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SAVE-RECIPE-ID,"));

                    break;

                case 70002: //GET-RECIPELIST (get recipe list)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-RECIPELIST,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 70003: //DEL-RECIPE-ID (delete recipe)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},DEL-RECIPE-ID,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                    }
                    break;

                case 80001: //SAVE-MATERIAL (create material)

                    mList.Add(byteSTX);
                    mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},SAVE-MATERIAL,"));

                    break;

                case 80002: //GET-MATERIAL (get material)
                    if (blnAscii)
                    {
                        mList.Add(byteSOH);
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 80003: //GET-MATERIALLIST (get material list)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},GET-MATERIALLIST,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                case 80004: //DEL-MATERIAL (delete material)
                    if (blnAscii)
                    {
                        mList.Add(byteSTX);
                        mList.AddRange(Encoding.ASCII.GetBytes($"{iSerialNumber},DEL-MATERIAL,"));
                    }
                    else
                    {
                        mList.AddRange(BitConverter.GetBytes(iSerialNumber));
                        mList.AddRange(BitConverter.GetBytes(iCmd));
                    }
                    break;

                default:
                    break;
            }
            return mList.ToArray();
        }

        public struct CommonResult
        {
            //10016
            public static int CtrlLight_RetryCount = 0;
            //20006
            public static int GetTriggerEdge = 0;
            //20008
            public static int GetTriggerFilter_Time = 0;
            //20010
            public static int GetUseAnalog = 0;
            //20012
            public static float GetAnalogRange_LowerLimit = 0.0F;
            public static float GetAnalogRange_UpperLimit = 0.0F;
            //20015
            public static float SetCalibrateThickness_PrimaryCoefficientC1 = 0.0F;
            public static float SetCalibrateThickness_ConstantCoefficientC0 = 0.0F;
            //20016
            public static float GetCalibrateThickness_StandardThicknessValue1 = 0.0F;
            public static float GetCalibrateThickness_MeasurementThicknessValue1 = 0.0F;
            public static float GetCalibrateThickness_StandardThicknessValue2 = 0.0F;
            public static float GetCalibrateThickness_MeasurementThicknessValue2 = 0.0F;
            public static float GetCalibrateThickness_PrimaryCoefficientC1 = 0.0F;
            public static float GetCalibrateThickness_ConstantCoefficientC0 = 0.0F;
            //20018
            public static int GetProtocol_DataTransmissionProtocol = 0;
            //30003
            public static int GetTriggerResetTime = 0;
            //30005
            public static int GetResultType_ResultDataType = 0;
            //30007
            public static int GetMeasMode_MeasurementMode = 0;
            //30009
            public static int GetMeasCount_ContinuousMeasurementNo = 0;
            //30011
            public static int GetMeasCycle_MeasurementCycle = 0;
            //30013
            public static int GetExposure_ExposureTime = 0;
            //30015
            public static int GetAccumulation_AccumulationsNo = 0;
            //30017
            public static int GetTriggerDelay_TriggerDelayTime = 0;
            //40002
            public static int GetAnalysisRange_StartAnalysisWavelengthNumber = 0;
            public static int GetAnalysisRange_EndAnalysisWavelengthNumber = 0;
            //40004
            public static int GetAnalysisMethod_AnalysisMethod = 0;
            //40006
            public static int GetAnalysisNum_AnalysisLayerNo = 0;
            //40008
            public static int GetSmoothing_SmoothingPoint = 0;
            //40010
            public static int GetFFTNum_FFTDataPointsNo = 0;
            //40012
            public static float GetFFTMaxThickness_FFTMaximumThickness = 0.0F;
            //40014
            public static int GetFFTWindow = 0;
            //40016
            public static int GetFFTNormalization = 0;
            //40018
            public static int GetUsePeakJudge = 0;
            //40020
            public static int GetAnalysisMaterial_AnalysisLayerMaterialID = 0;
            //40022
            public static int GetAnalysisRefractiveindex_DataQuantity = 0;
            public static List<float> GetAnalysisRefractiveindex_ConstantRefractiveIndex = new List<float>();
            //40024
            public static int GetAnalysisThicknessRange_DataQuantity = 0;
            public static List<float> GetAnalysisThicknessRange_ThicknessRangeLowerLimit = new List<float>();
            public static List<float> GetAnalysisThicknessRange_ThicknessRangeUpperLimit = new List<float>();
            //40026
            public static int GetFFTPowerRange_DataQuantity = 0;
            public static List<float> GetFFTPowerRange_FFTIntensityRangeLowerLimit = new List<float>();
            public static List<float> GetFFTPowerRange_FFTIntensityRangeUpperLimit = new List<float>();
            //40028
            public static int GetFFTSearchDirection_DataQuantity = 0;
            public static List<int> GetFFTSearchDirection_PeakSearchDirection = new List<int>();
            //40030
            public static int GetFFTPeakNum_DataQuantity = 0;
            public static List<int> GetFFTPeakNum_PeakNumber = new List<int>();
            //40032
            public static int GetThicknessCoef_DataQuantity = 0;
            public static List<int> GetThicknessCoef = new List<int>();
            public static List<float> GetThicknessCoef_PrimaryCoefficientC1 = new List<float>();
            public static List<float> GetThicknessCoef_ConstantCoefficientC0 = new List<float>();
            //50002
            public static int GetOptimAmbient_AmbientLayerMaterialID = 0;
            //50004
            public static int GetOptimSubstrate_SubstrateMaterialID = 0;
            public static float GetOptimSubstrate_Thickness = 0.0F;
            //50006
            public static int GetOptimLayermodel_LayersNo = 0;
            public static List<int> GetOptimLayermodel_LayerMaterialID = new List<int>();
            public static List<float> GetOptimLayermodel_Thickness = new List<float>();
            //50008
            public static float GetOptimSwitchThickness_Thickness = 0.0F;
            //50010
            public static float GetOptimThicknessStep_ThicknessStep = 0.0F;
            //60002
            public static float GetFollowupFilter_FollowupFilterPlusMinusRange = 0.0F;
            //60004
            public static int GetFilterApplytime_FollowupFilterApplicationTime = 0;
            //60006
            public static int GetFilterCenterMA_FollowupFilterCenterValueMovingAverage = 0;
            //60008
            public static int GetFilterTrend = 0;
            //60010
            public static int GetThicknessMA_ThicknessMovingAverageFrequency = 0;
            //60012
            public static int GetErrorvalue = 0;
            //70002
            public static int GetRecipeList_RecipesNo = 0;
            public static List<int> GetRecipeList_RecipesID = new List<int>();
            public static List<string> GetRecipeList_RecipesName = new List<string>();
            public static List<string> GetRecipeList_DateAndTimeOfUpdate = new List<string>();
            //80003
            public static int GetMaterialList_MaterialsNo = 0;
            public static List<int> GetMaterialList_MaterialID = new List<int>();
            public static List<string> GetMaterialList_MaterialName = new List<string>();
            public static List<string> GetMaterialList_DateAndTimeOfUpdate = new List<string>();
        }

        void CheckRecv(int iCmd)
        {
            TimeoutSW timeout = new TimeoutSW(Common.SwTimer);
            timeout.Start();

            while (true)
            {
                isTimeout = timeout.IsTimeout;
                if (isTimeout)
                {
                    AddLog($"Command {iCmd} Is Timeout");
                    break;
                }

                var bytes = Tcp.Recv(blnAscii);
                if (bytes is null || bytes.Length == 0)
                {
                    //MessageBox.Show("接收失敗");
                    continue;
                }

                if (blnAscii)
                {
                    var strRecv = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    AddLog(strRecv);
                    var arrRecv = strRecv.Split(',');
                    sResponseCode = arrRecv[2];
                    sStatus = arrRecv[3];
                    if (sResponseCode != "0")
                    {
                        System.Windows.Forms.MessageBox.Show(arrRecv[1] + "指令失敗");
                        return;
                    }
                    switch (iCmd)
                    {
                        case 10016:
                            CommonResult.CtrlLight_RetryCount = 0;
                            CommonResult.CtrlLight_RetryCount = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 20006:
                            CommonResult.GetTriggerEdge = 0;
                            CommonResult.GetTriggerEdge = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 20008:
                            CommonResult.GetTriggerFilter_Time = 0;
                            CommonResult.GetTriggerFilter_Time = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 20010:
                            CommonResult.GetUseAnalog = 0;
                            CommonResult.GetUseAnalog = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 20012:
                            CommonResult.GetAnalogRange_LowerLimit = 0.0F;
                            CommonResult.GetAnalogRange_UpperLimit = 0.0F;
                            CommonResult.GetAnalogRange_LowerLimit = Convert.ToSingle(arrRecv[4]);
                            CommonResult.GetAnalogRange_UpperLimit = Convert.ToSingle(arrRecv[5]);
                            break;

                        case 20015:
                            CommonResult.SetCalibrateThickness_PrimaryCoefficientC1 = 0.0F;
                            CommonResult.SetCalibrateThickness_ConstantCoefficientC0 = 0.0F;
                            CommonResult.SetCalibrateThickness_PrimaryCoefficientC1 = Convert.ToSingle(arrRecv[4]);
                            CommonResult.SetCalibrateThickness_ConstantCoefficientC0 = Convert.ToSingle(arrRecv[5]);
                            break;

                        case 20016:
                            CommonResult.GetCalibrateThickness_StandardThicknessValue1 = 0.0F;
                            CommonResult.GetCalibrateThickness_MeasurementThicknessValue1 = 0.0F;
                            CommonResult.GetCalibrateThickness_StandardThicknessValue2 = 0.0F;
                            CommonResult.GetCalibrateThickness_MeasurementThicknessValue2 = 0.0F;
                            CommonResult.GetCalibrateThickness_PrimaryCoefficientC1 = 0.0F;
                            CommonResult.GetCalibrateThickness_ConstantCoefficientC0 = 0.0F;
                            CommonResult.GetCalibrateThickness_StandardThicknessValue1 = Convert.ToSingle(arrRecv[4]);
                            CommonResult.GetCalibrateThickness_MeasurementThicknessValue1 = Convert.ToSingle(arrRecv[5]);
                            CommonResult.GetCalibrateThickness_StandardThicknessValue2 = Convert.ToSingle(arrRecv[6]);
                            CommonResult.GetCalibrateThickness_MeasurementThicknessValue2 = Convert.ToSingle(arrRecv[7]);
                            CommonResult.GetCalibrateThickness_PrimaryCoefficientC1 = Convert.ToSingle(arrRecv[8]);
                            CommonResult.GetCalibrateThickness_ConstantCoefficientC0 = Convert.ToSingle(arrRecv[9]);
                            break;

                        case 20018:
                            CommonResult.GetProtocol_DataTransmissionProtocol = 0;
                            CommonResult.GetProtocol_DataTransmissionProtocol = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 30003:
                            CommonResult.GetTriggerResetTime = 0;
                            CommonResult.GetTriggerResetTime = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 30005:
                            CommonResult.GetResultType_ResultDataType = 0;
                            CommonResult.GetResultType_ResultDataType = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 30007:
                            CommonResult.GetMeasMode_MeasurementMode = 0;
                            CommonResult.GetMeasMode_MeasurementMode = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 30009:
                            CommonResult.GetMeasCount_ContinuousMeasurementNo = 0;
                            CommonResult.GetMeasCount_ContinuousMeasurementNo = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 30011:
                            CommonResult.GetMeasCycle_MeasurementCycle = 0;
                            CommonResult.GetMeasCycle_MeasurementCycle = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 30013:
                            CommonResult.GetExposure_ExposureTime = 0;
                            CommonResult.GetExposure_ExposureTime = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 30015:
                            CommonResult.GetAccumulation_AccumulationsNo = 0;
                            CommonResult.GetAccumulation_AccumulationsNo = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 30017:
                            CommonResult.GetTriggerDelay_TriggerDelayTime = 0;
                            CommonResult.GetTriggerDelay_TriggerDelayTime = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 40002:
                            CommonResult.GetAnalysisRange_StartAnalysisWavelengthNumber = 0;
                            CommonResult.GetAnalysisRange_EndAnalysisWavelengthNumber = 0;
                            CommonResult.GetAnalysisRange_StartAnalysisWavelengthNumber = Convert.ToInt32(arrRecv[4]);
                            CommonResult.GetAnalysisRange_EndAnalysisWavelengthNumber = Convert.ToInt32(arrRecv[5]);
                            break;

                        case 40004:
                            CommonResult.GetAnalysisMethod_AnalysisMethod = 0;
                            CommonResult.GetAnalysisMethod_AnalysisMethod = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 40006:
                            CommonResult.GetAnalysisNum_AnalysisLayerNo = 0;
                            CommonResult.GetAnalysisNum_AnalysisLayerNo = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 40008:
                            CommonResult.GetSmoothing_SmoothingPoint = 0;
                            CommonResult.GetSmoothing_SmoothingPoint = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 40010:
                            CommonResult.GetFFTNum_FFTDataPointsNo = 0;
                            CommonResult.GetFFTNum_FFTDataPointsNo = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 40012:
                            CommonResult.GetFFTMaxThickness_FFTMaximumThickness = 0;
                            CommonResult.GetFFTMaxThickness_FFTMaximumThickness = Convert.ToSingle(arrRecv[4]);
                            break;

                        case 40014:
                            CommonResult.GetFFTWindow = 0;
                            CommonResult.GetFFTWindow = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 40016:
                            CommonResult.GetFFTNormalization = 0;
                            CommonResult.GetFFTNormalization = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 40018:
                            CommonResult.GetUsePeakJudge = 0;
                            CommonResult.GetUsePeakJudge = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 40020:
                            CommonResult.GetAnalysisMaterial_AnalysisLayerMaterialID = 0;
                            CommonResult.GetAnalysisMaterial_AnalysisLayerMaterialID = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 40022:
                            CommonResult.GetAnalysisRefractiveindex_DataQuantity = 0;
                            CommonResult.GetAnalysisRefractiveindex_ConstantRefractiveIndex.Clear();
                            CommonResult.GetAnalysisRefractiveindex_DataQuantity = Convert.ToInt32(arrRecv[4]);
                            for (int i = 0; i < CommonResult.GetAnalysisRefractiveindex_DataQuantity; i++)
                                CommonResult.GetAnalysisRefractiveindex_ConstantRefractiveIndex.Add(Convert.ToSingle(arrRecv[5 + i]));
                            break;

                        case 40024:
                            CommonResult.GetAnalysisThicknessRange_DataQuantity = 0;
                            CommonResult.GetAnalysisThicknessRange_ThicknessRangeLowerLimit.Clear();
                            CommonResult.GetAnalysisThicknessRange_ThicknessRangeUpperLimit.Clear();
                            CommonResult.GetAnalysisThicknessRange_DataQuantity = Convert.ToInt32(arrRecv[4]);

                            for (int i = 0; i < CommonResult.GetAnalysisThicknessRange_DataQuantity; i++)
                            {
                                CommonResult.GetAnalysisThicknessRange_ThicknessRangeLowerLimit.Add(Convert.ToSingle(arrRecv[5 + (i * 2)]));
                                CommonResult.GetAnalysisThicknessRange_ThicknessRangeUpperLimit.Add(Convert.ToSingle(arrRecv[5 + (i * 2) + 1]));
                            }
                            break;

                        case 40026:
                            CommonResult.GetFFTPowerRange_DataQuantity = 0;
                            CommonResult.GetFFTPowerRange_FFTIntensityRangeLowerLimit.Clear();
                            CommonResult.GetFFTPowerRange_FFTIntensityRangeUpperLimit.Clear();
                            CommonResult.GetFFTPowerRange_DataQuantity = Convert.ToInt32(arrRecv[4]);

                            for (int i = 0; i < CommonResult.GetFFTPowerRange_DataQuantity; i++)
                            {
                                CommonResult.GetFFTPowerRange_FFTIntensityRangeLowerLimit.Add(Convert.ToSingle(arrRecv[5 + (i * 2)]));
                                CommonResult.GetFFTPowerRange_FFTIntensityRangeUpperLimit.Add(Convert.ToSingle(arrRecv[5 + (i * 2) + 1]));
                            }
                            break;

                        case 40028:
                            CommonResult.GetFFTSearchDirection_DataQuantity = 0;
                            CommonResult.GetFFTSearchDirection_PeakSearchDirection.Clear();
                            CommonResult.GetFFTSearchDirection_DataQuantity = Convert.ToInt32(arrRecv[4]);

                            for (int i = 0; i < CommonResult.GetFFTSearchDirection_DataQuantity; i++)
                                CommonResult.GetFFTSearchDirection_PeakSearchDirection.Add(Convert.ToInt32(arrRecv[5 + i]));
                            break;

                        case 40030:
                            CommonResult.GetFFTPeakNum_DataQuantity = 0;
                            CommonResult.GetFFTPeakNum_PeakNumber.Clear();
                            CommonResult.GetFFTPeakNum_DataQuantity = Convert.ToInt32(arrRecv[4]);

                            for (int i = 0; i < CommonResult.GetFFTPeakNum_DataQuantity; i++)
                                CommonResult.GetFFTPeakNum_PeakNumber.Add(Convert.ToInt32(arrRecv[5 + i]));
                            break;

                        case 40032:
                            CommonResult.GetThicknessCoef_DataQuantity = 0;
                            CommonResult.GetThicknessCoef.Clear();
                            CommonResult.GetThicknessCoef_PrimaryCoefficientC1.Clear();
                            CommonResult.GetThicknessCoef_ConstantCoefficientC0.Clear();
                            CommonResult.GetThicknessCoef_DataQuantity = Convert.ToInt32(arrRecv[4]);

                            for (int i = 0; i < CommonResult.GetThicknessCoef_DataQuantity; i++)
                            {
                                CommonResult.GetThicknessCoef.Add(Convert.ToInt32(arrRecv[5 + (i * 3)]));
                                CommonResult.GetThicknessCoef_PrimaryCoefficientC1.Add(Convert.ToSingle(arrRecv[5 + (i * 3) + 1]));
                                CommonResult.GetThicknessCoef_ConstantCoefficientC0.Add(Convert.ToSingle(arrRecv[5 + (i * 3) + 2]));
                            }
                            break;

                        case 50002:
                            CommonResult.GetOptimAmbient_AmbientLayerMaterialID = 0;
                            CommonResult.GetOptimAmbient_AmbientLayerMaterialID = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 50004:
                            CommonResult.GetOptimSubstrate_SubstrateMaterialID = 0;
                            CommonResult.GetOptimSubstrate_Thickness = 0;
                            CommonResult.GetOptimSubstrate_SubstrateMaterialID = Convert.ToInt32(arrRecv[4]);
                            CommonResult.GetOptimSubstrate_Thickness = Convert.ToSingle(arrRecv[5]);
                            break;

                        case 50006:
                            CommonResult.GetOptimLayermodel_LayersNo = 0;
                            CommonResult.GetOptimLayermodel_LayerMaterialID.Clear();
                            CommonResult.GetOptimLayermodel_Thickness.Clear();
                            CommonResult.GetOptimLayermodel_LayersNo = Convert.ToInt32(arrRecv[4]);

                            for (int i = 0; i < CommonResult.GetOptimLayermodel_LayersNo; i++)
                            {
                                CommonResult.GetOptimLayermodel_LayerMaterialID.Add(Convert.ToInt32(arrRecv[5 + (i * 2)]));
                                CommonResult.GetOptimLayermodel_Thickness.Add(Convert.ToSingle(arrRecv[5 + (i * 2) + 1]));
                            }
                            break;

                        case 50008:
                            CommonResult.GetOptimSwitchThickness_Thickness = 0;
                            CommonResult.GetOptimSwitchThickness_Thickness = Convert.ToSingle(arrRecv[4]);
                            break;

                        case 50010:
                            CommonResult.GetOptimThicknessStep_ThicknessStep = 0;
                            CommonResult.GetOptimThicknessStep_ThicknessStep = Convert.ToSingle(arrRecv[4]);
                            break;

                        case 60002:
                            CommonResult.GetFollowupFilter_FollowupFilterPlusMinusRange = 0;
                            CommonResult.GetFollowupFilter_FollowupFilterPlusMinusRange = Convert.ToSingle(arrRecv[4]);
                            break;

                        case 60004:
                            CommonResult.GetFilterApplytime_FollowupFilterApplicationTime = 0;
                            CommonResult.GetFilterApplytime_FollowupFilterApplicationTime = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 60006:
                            CommonResult.GetFilterCenterMA_FollowupFilterCenterValueMovingAverage = 0;
                            CommonResult.GetFilterCenterMA_FollowupFilterCenterValueMovingAverage = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 60008:
                            CommonResult.GetFilterTrend = 0;
                            CommonResult.GetFilterTrend = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 60010:
                            CommonResult.GetThicknessMA_ThicknessMovingAverageFrequency = 0;
                            CommonResult.GetThicknessMA_ThicknessMovingAverageFrequency = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 60012:
                            CommonResult.GetErrorvalue = 0;
                            CommonResult.GetErrorvalue = Convert.ToInt32(arrRecv[4]);
                            break;

                        case 70002:
                            CommonResult.GetRecipeList_RecipesNo = 0;
                            CommonResult.GetRecipeList_RecipesID.Clear();
                            CommonResult.GetRecipeList_RecipesName.Clear();
                            CommonResult.GetRecipeList_DateAndTimeOfUpdate.Clear();
                            CommonResult.GetRecipeList_RecipesNo = Convert.ToInt32(arrRecv[4]);

                            for (int i = 0; i < CommonResult.GetRecipeList_RecipesNo; i++)
                            {
                                CommonResult.GetRecipeList_RecipesID.Add(Convert.ToInt32(arrRecv[5 + (i * 3)]));
                                CommonResult.GetRecipeList_RecipesName.Add(Convert.ToString(arrRecv[5 + (i * 3) + 1]));
                                CommonResult.GetRecipeList_DateAndTimeOfUpdate.Add(Convert.ToString(arrRecv[5 + (i * 3) + 2]));
                            }
                            break;

                        case 80003:
                            CommonResult.GetMaterialList_MaterialsNo = 0;
                            CommonResult.GetMaterialList_MaterialID.Clear();
                            CommonResult.GetMaterialList_MaterialName.Clear();
                            CommonResult.GetMaterialList_DateAndTimeOfUpdate.Clear();
                            CommonResult.GetMaterialList_MaterialsNo = Convert.ToInt32(arrRecv[4]);

                            for (int i = 0; i < CommonResult.GetMaterialList_MaterialsNo; i++)
                            {
                                CommonResult.GetMaterialList_MaterialID.Add(Convert.ToInt32(arrRecv[5 + (i * 3)]));
                                CommonResult.GetMaterialList_MaterialName.Add(Convert.ToString(arrRecv[5 + (i * 3) + 1]));
                                CommonResult.GetMaterialList_DateAndTimeOfUpdate.Add(Convert.ToString(arrRecv[5 + (i * 3) + 2]));
                            }
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    sResponseCode = BitConverter.ToInt32(bytes, 8).ToString();
                    sStatus = BitConverter.ToInt32(bytes, 8 + 4).ToString();
                }

                break;
            }
            SpinWait.SpinUntil(() => false, 1);
        }

        #region MEAS-REFERENCE
        public struct MeasReference
        {
            public static int SignalsNo = 0;
            public static List<float> ReferenceDarkSpectrum = new List<float>();
            public static List<float> ReferenceSignalSpectrum = new List<float>();
        }

        void CheckMeasReference()
        {
            MeasReference.SignalsNo = 0;
            MeasReference.ReferenceDarkSpectrum.Clear();
            MeasReference.ReferenceSignalSpectrum.Clear();

            TimeoutSW timeout = new TimeoutSW(Common.SwTimer);
            timeout.Start();
            while (true)
            {
                isTimeout = timeout.IsTimeout;
                if (isTimeout)
                {
                    AddLog($"Command {(int)Common.Command.CmdMeasReference} Is Timeout");
                    break;
                }

                var bytes = Tcp.RecvGetResult();
                if (bytes is null) continue;
                if (bytes.Length == 0) continue;

                // SF-3_CommunicationSpecifications_20180928.pdf Page.10
                // SOH,Binary number,Transmission No,Binary data type,Response code,Status,Reponse parameter 1...
                // bytes 已經去掉SOH
                // 從Transmission No開始算
                sResponseCode = BitConverter.ToInt32(bytes, 4 * 3).ToString();
                sStatus = BitConverter.ToInt32(bytes, 4 * 4).ToString();

                // MEAS-REFERENCE Binary data type = 1
                var iType = BitConverter.ToInt32(bytes, 4 * 2);
                if (iType != 1)
                {
                    AddLog("SOH Binary data type <> 1, not MEAS-REFERENCE");
                    break;
                }

                MeasReference.SignalsNo = BitConverter.ToInt32(bytes, 4 * 5);

                for (int i = 0; i < MeasReference.SignalsNo; i++)
                    MeasReference.ReferenceDarkSpectrum.Add(BitConverter.ToSingle(bytes, 4 * (6 + i)));

                for (int i = 0; i < MeasReference.SignalsNo; i++)
                    MeasReference.ReferenceSignalSpectrum.Add(BitConverter.ToSingle(bytes, 4 * (7 + (MeasReference.SignalsNo - 1) + i)));

                break;
            }
        }
        #endregion

        #region GET-REFERENCE
        public struct GetReference
        {
            public static int ExposureTime = 0;
            public static int AccumulationsNo = 0;
            public static string DateTime = "";
            public static int SignalsNo = 0;
            public static List<float> ReferenceDarkSpectrum = new List<float>();
            public static List<float> ReferenceSignalSpectrum = new List<float>();
        }

        void CheckGetReference()
        {
            GetReference.ExposureTime = 0;
            GetReference.AccumulationsNo = 0;
            GetReference.DateTime = "";
            GetReference.SignalsNo = 0;
            GetReference.ReferenceDarkSpectrum.Clear();
            GetReference.ReferenceSignalSpectrum.Clear();

            TimeoutSW timeout = new TimeoutSW(Common.SwTimer);
            timeout.Start();
            while (true)
            {
                isTimeout = timeout.IsTimeout;
                if (isTimeout)
                {
                    AddLog($"Command {(int)Common.Command.CmdGetReference} Is Timeout");
                    break;
                }

                var bytes = Tcp.RecvGetResult();
                if (bytes is null) continue;
                if (bytes.Length == 0) continue;

                // SF-3_CommunicationSpecifications_20180928.pdf Page.10
                // SOH,Binary number,Transmission No,Binary data type,Response code,Status,Reponse parameter 1...
                // bytes 已經去掉SOH
                // 從Transmission No開始算
                sResponseCode = BitConverter.ToInt32(bytes, 4 * 3).ToString();
                sStatus = BitConverter.ToInt32(bytes, 4 * 4).ToString();
                // GET-REFERENCE Binary data type = 2
                var iType = BitConverter.ToInt32(bytes, 4 * 2);
                if (iType != 2)
                {
                    AddLog("SOH Binary data type <> 2, not GET-REFERENCE");
                    break;
                }

                GetReference.ExposureTime = BitConverter.ToInt32(bytes, 4 * 5);
                GetReference.AccumulationsNo = BitConverter.ToInt32(bytes, 4 * 6);
                GetReference.DateTime = Encoding.ASCII.GetString(bytes, 4 * 7, 14);
                GetReference.SignalsNo = BitConverter.ToInt32(bytes, (4 * 7) + 14);

                for (int i = 0; i < GetReference.SignalsNo; i++)
                    GetReference.ReferenceDarkSpectrum.Add(BitConverter.ToSingle(bytes, (4 * (8 + i)) + 14));

                for (int i = 0; i < GetReference.SignalsNo; i++)
                    GetReference.ReferenceSignalSpectrum.Add(BitConverter.ToSingle(bytes, (4 * (9 + (512 - 1) + i)) + 14));

                break;
            }
        }
        #endregion

        #region MEAS-DARK
        public struct MeasDark
        {
            public static int SignalsNo = 0;
            public static List<float> SampleDarkSpectrum = new List<float>();
        }

        void CheckMeasDark()
        {
            MeasDark.SignalsNo = 0;
            MeasDark.SampleDarkSpectrum.Clear();

            TimeoutSW timeout = new TimeoutSW(Common.SwTimer);
            timeout.Start();
            while (true)
            {
                isTimeout = timeout.IsTimeout;
                if (isTimeout)
                {
                    AddLog($"Command {(int)Common.Command.CmdMeasDark} Is Timeout");
                    break;
                }

                var bytes = Tcp.RecvGetResult();
                if (bytes is null) continue;
                if (bytes.Length == 0) continue;

                // SF-3_CommunicationSpecifications_20180928.pdf Page.10
                // SOH,Binary number,Transmission No,Binary data type,Response code,Status,Reponse parameter 1...
                // bytes 已經去掉SOH
                // 從Transmission No開始算
                sResponseCode = BitConverter.ToInt32(bytes, 4 * 3).ToString();
                sStatus = BitConverter.ToInt32(bytes, 4 * 4).ToString();

                // MEAS-DARK Binary data type = 3
                var iType = BitConverter.ToInt32(bytes, 4 * 2);
                if (iType != 3)
                {
                    AddLog("SOH Binary data type <> 3, not MEAS-DARK");
                    break;
                }

                MeasDark.SignalsNo = BitConverter.ToInt32(bytes, 4 * 5);

                for (int i = 0; i < MeasDark.SignalsNo; i++)
                    MeasDark.SampleDarkSpectrum.Add(BitConverter.ToSingle(bytes, 4 * (6 + i)));

                break;
            }
        }
        #endregion

        #region MEAS-SIGNAL
        public struct MeasSignal
        {
            public static int SignalsNo = 0;
            public static List<float> SignalSpectrum = new List<float>();
        }

        void CheckMeasSignal()
        {
            MeasSignal.SignalsNo = 0;
            MeasSignal.SignalSpectrum.Clear();

            TimeoutSW timeout = new TimeoutSW(Common.SwTimer);
            timeout.Start();
            while (true)
            {
                isTimeout = timeout.IsTimeout;
                if (isTimeout)
                {
                    AddLog($"Command {(int)Common.Command.CmdMeasSignal} Is Timeout");
                    break;
                }

                var bytes = Tcp.RecvGetResult();
                if (bytes is null) continue;
                if (bytes.Length == 0) continue;

                // SF-3_CommunicationSpecifications_20180928.pdf Page.10
                // SOH,Binary number,Transmission No,Binary data type,Response code,Status,Reponse parameter 1...
                // bytes 已經去掉SOH
                // 從Transmission No開始算
                sResponseCode = BitConverter.ToInt32(bytes, 4 * 3).ToString();
                sStatus = BitConverter.ToInt32(bytes, 4 * 4).ToString();

                // MEAS-SIGNAL Binary data type = 4
                var iType = BitConverter.ToInt32(bytes, 4 * 2);
                if (iType != 4)
                {
                    AddLog("SOH Binary data type <> 4, not MEAS-SIGNAL");
                    break;
                }

                MeasSignal.SignalsNo = BitConverter.ToInt32(bytes, 4 * 5);

                for (int i = 0; i < MeasSignal.SignalsNo; i++)
                    MeasSignal.SignalSpectrum.Add(BitConverter.ToSingle(bytes, 4 * (6 + i)));

                break;
            }
        }
        #endregion

        #region GET-RESULT
        public struct GetResult
        {
            public static int ContinuousNo = 0;
            public static int ResultDataType = 0;
            public static int AnalysisLayersNo = 0;
            public static int SignalsNo = 0;
            public static int ReflectancesNo = 0;
            public static int FFTDataPointsNo = 0;
            public static int SimulationSpectraNo = 0;
            public static uint ElapsedTime = 0;
            public static float SignalMaximum = 0.0F;
            public static List<float> Thickness = new List<float>();
            public static List<float> SignalSpectrum = new List<float>();
            public static List<float> ReflectanceSpectrum = new List<float>();
            public static int AnalysisDataType = 0;
            public static List<float> PowerSpectrum = new List<float>();
            public static List<float> SimulationSpectrum = new List<float>();
        }

        void CheckGetResult()
        {
            GetResult.ContinuousNo = 0;
            GetResult.ResultDataType = 0;
            GetResult.AnalysisLayersNo = 0;
            GetResult.SignalsNo = 0;
            GetResult.ReflectancesNo = 0;
            GetResult.FFTDataPointsNo = 0;
            GetResult.SimulationSpectraNo = 0;
            GetResult.ElapsedTime = 0;
            GetResult.SignalMaximum = 0.0F;
            GetResult.Thickness.Clear();
            GetResult.SignalSpectrum.Clear();
            GetResult.ReflectanceSpectrum.Clear();
            GetResult.AnalysisDataType = 0;
            GetResult.PowerSpectrum.Clear();
            GetResult.SimulationSpectrum.Clear();
            
            TimeoutSW timeout = new TimeoutSW(Common.SwTimer);
            timeout.Start();
            while (true)
            {
                isTimeout = timeout.IsTimeout;
                if (isTimeout)
                {
                    AddLog($"Command {(int)Common.Command.CmdGetResult} Is Timeout");
                    break;
                }

                var bytes = Tcp.RecvGetResult();
                if (bytes is null) continue;
                if (bytes.Length == 0) continue;

                // SF-3_CommunicationSpecifications_20180928.pdf Page.10
                // SOH,Binary number,Transmission No,Binary data type,Response code,Status,Reponse parameter 1...
                // bytes 已經去掉SOH
                // 從Transmission No開始算
                sResponseCode = BitConverter.ToInt32(bytes, 4 * 3).ToString();
                sStatus = BitConverter.ToInt32(bytes, 4 * 4).ToString();

                // GET-RESULT Binary data type = 5
                var iType = BitConverter.ToInt32(bytes, 4 * 2);
                if (iType != 5)
                {
                    AddLog("SOH Binary data type <> 5, not GET-RESULT");
                    break;
                }

                //GetResult.ContinuousNo = BitConverter.ToInt32(bytes, 4 * 5);
                GetResult.ContinuousNo = 1;
                GetResult.ResultDataType = BitConverter.ToInt32(bytes, 4 * 6);
                GetResult.AnalysisLayersNo = BitConverter.ToInt32(bytes, 4 * 7);
                GetResult.SignalsNo = BitConverter.ToInt32(bytes, 4 * 8);
                GetResult.ReflectancesNo = BitConverter.ToInt32(bytes, 4 * 9);
                GetResult.FFTDataPointsNo = BitConverter.ToInt32(bytes, 4 * 10);
                GetResult.SimulationSpectraNo = BitConverter.ToInt32(bytes, 4 * 11);
                GetResult.ElapsedTime = BitConverter.ToUInt32(bytes, 4 * 12);
                GetResult.SignalMaximum = BitConverter.ToSingle(bytes, 4 * 13);

                for (int i = 0; i < GetResult.AnalysisLayersNo; i++)
                    GetResult.Thickness.Add(BitConverter.ToSingle(bytes, 4 * (14 + i)));

                for (int i = 0; i < GetResult.SignalsNo; i++)
                {
                    GetResult.SignalSpectrum.Add(BitConverter.ToSingle(bytes, 4 * (15 + (GetResult.AnalysisLayersNo - 1) + i)));
                }

                for (int i = 0; i < GetResult.ReflectancesNo; i++)
                    GetResult.ReflectanceSpectrum.Add(BitConverter.ToSingle(bytes, 4 * (16 + (GetResult.AnalysisLayersNo - 1) + (GetResult.SignalsNo - 1) + i)));

                GetResult.AnalysisDataType = BitConverter.ToInt32(bytes, 4 * (17 + (GetResult.AnalysisLayersNo - 1) + (GetResult.SignalsNo - 1) + (GetResult.ReflectancesNo - 1)));
                
                if (GetResult.AnalysisDataType == 0)
                    for (int i = 0; i < GetResult.FFTDataPointsNo; i++)
                        GetResult.PowerSpectrum.Add(BitConverter.ToSingle(bytes, 4 * (18 + (GetResult.AnalysisLayersNo - 1) + (GetResult.SignalsNo - 1) + (GetResult.ReflectancesNo - 1) + i)));
                else if (GetResult.AnalysisDataType == 1)
                    for (int i = 0; i < GetResult.SimulationSpectraNo; i++)
                        GetResult.SimulationSpectrum.Add(BitConverter.ToSingle(bytes, 4 * (18 + (GetResult.AnalysisLayersNo - 1) + (GetResult.SignalsNo - 1) + (GetResult.ReflectancesNo - 1) + i)));
                //GetResult.SimulationSpectrum.Add(BitConverter.ToSingle(bytes, 4 * (19 + (GetResult.AnalysisLayersNo - 1) + (GetResult.SignalsNo - 1) + (GetResult.ReflectancesNo - 1) + (GetResult.FFTDataPointsNo - 1) + i)));

                break;
            }
        }
        #endregion

        #region REANALYZE
        public struct Reanalyze
        {
            public static int ThicknessValuesNo = 0;
            public static int PowerSpectraNo = 0;
            public static int SimulationSpectraNo = 0;
            public static List<float> ThicknessValue = new List<float>();
            public static int AnalysisDataType = 0;
            public static List<float> PowerSpectrum = new List<float>();
            public static List<float> SimulationSpectrum = new List<float>();
        }

        void CheckReanalyze()
        {
            Reanalyze.ThicknessValuesNo = 0;
            Reanalyze.PowerSpectraNo = 0;
            Reanalyze.SimulationSpectraNo = 0;
            Reanalyze.ThicknessValue.Clear();
            Reanalyze.AnalysisDataType = 0;
            Reanalyze.PowerSpectrum.Clear();
            Reanalyze.SimulationSpectrum.Clear();

            TimeoutSW timeout = new TimeoutSW(Common.SwTimer);
            timeout.Start();
            while (true)
            {
                isTimeout = timeout.IsTimeout;
                if (isTimeout)
                {
                    AddLog($"Command {(int)Common.Command.CmdReanalyze} Is Timeout");
                    break;
                }

                var bytes = Tcp.RecvGetResult();
                if (bytes is null) continue;
                if (bytes.Length == 0) continue;

                // SF-3_CommunicationSpecifications_20180928.pdf Page.10
                // SOH,Binary number,Transmission No,Binary data type,Response code,Status,Reponse parameter 1...
                // bytes 已經去掉SOH
                // 從Transmission No開始算
                sResponseCode = BitConverter.ToInt32(bytes, 4 * 3).ToString();
                sStatus = BitConverter.ToInt32(bytes, 4 * 4).ToString();

                // REANALYZE Binary data type = 6
                var iType = BitConverter.ToInt32(bytes, 4 * 2);
                if (iType != 6)
                {
                    AddLog("SOH Binary data type <> 6, not REANALYZE");
                    break;
                }

                Reanalyze.ThicknessValuesNo = BitConverter.ToInt32(bytes, 4 * 5);
                Reanalyze.PowerSpectraNo = BitConverter.ToInt32(bytes, 4 * 6);
                Reanalyze.SimulationSpectraNo = BitConverter.ToInt32(bytes, 4 * 7);

                for (int i = 0; i < Reanalyze.ThicknessValuesNo; i++)
                    Reanalyze.ThicknessValue.Add(BitConverter.ToSingle(bytes, 4 * (8 + i)));

                Reanalyze.AnalysisDataType = BitConverter.ToInt32(bytes, 4 * (9 + (Reanalyze.ThicknessValuesNo - 1)));

                for (int i = 0; i < Reanalyze.PowerSpectraNo; i++)
                    Reanalyze.PowerSpectrum.Add(BitConverter.ToSingle(bytes, 4 * (10 + (Reanalyze.ThicknessValuesNo - 1) + i)));

                for (int i = 0; i < Reanalyze.SimulationSpectraNo; i++)
                    Reanalyze.SimulationSpectrum.Add(BitConverter.ToSingle(bytes, 4 * (11 + (Reanalyze.ThicknessValuesNo - 1) + (Reanalyze.PowerSpectraNo - 1) + i)));

                break;
            }
        }
        #endregion

        #region GET-MATERIAL
        public struct GetMaterial
        {
            public static int MaterialNameNo = 0;
            public static int DataQuantity = 0;
            public static int MaterialID = 0;
            public static List<string> MaterialName = new List<string>();
            public static List<float> Wavelength = new List<float>();
            public static List<float> RefractiveIndex = new List<float>();
            public static List<float> ExtinctionCoefficient = new List<float>();
        }

        void CheckGetMaterial()
        {
            GetMaterial.MaterialNameNo = 0;
            GetMaterial.DataQuantity = 0;
            GetMaterial.MaterialID = 0;
            GetMaterial.MaterialName.Clear();
            GetMaterial.Wavelength.Clear();
            GetMaterial.RefractiveIndex.Clear();
            GetMaterial.ExtinctionCoefficient.Clear();

            TimeoutSW timeout = new TimeoutSW(Common.SwTimer);
            timeout.Start();
            while (true)
            {
                isTimeout = timeout.IsTimeout;
                if (isTimeout)
                {
                    AddLog($"Command {(int)Common.Command.CmdGetMaterial} Is Timeout");
                    break;
                }

                var bytes = Tcp.RecvGetResult();
                if (bytes is null) continue;
                if (bytes.Length == 0) continue;

                // SF-3_CommunicationSpecifications_20180928.pdf Page.10
                // SOH,Binary number,Transmission No,Binary data type,Response code,Status,Reponse parameter 1...
                // bytes 已經去掉SOH
                // 從Transmission No開始算
                sResponseCode = BitConverter.ToInt32(bytes, 4 * 3).ToString();
                sStatus = BitConverter.ToInt32(bytes, 4 * 4).ToString();

                // GET-MATERIAL Binary data type = 7
                var iType = BitConverter.ToInt32(bytes, 4 * 2);
                if (iType != 7)
                {
                    AddLog("SOH Binary data type <> 7, not GET-MATERIAL");
                    break;
                }

                GetMaterial.MaterialNameNo = BitConverter.ToInt32(bytes, 4 * 5);
                GetMaterial.DataQuantity = BitConverter.ToInt32(bytes, 4 * 6);
                GetMaterial.MaterialID = BitConverter.ToInt32(bytes, 4 * 7);

                int temp = GetMaterial.MaterialNameNo;
                GetMaterial.MaterialName.Add(Encoding.ASCII.GetString(bytes, (4 * 8), temp));

                for (int i = 0; i < GetMaterial.DataQuantity; i++)
                    GetMaterial.Wavelength.Add(BitConverter.ToSingle(bytes, (4 * (8 + i)) + temp));

                for (int i = 0; i < GetMaterial.DataQuantity; i++)
                    GetMaterial.RefractiveIndex.Add(BitConverter.ToSingle(bytes, (4 * (9 + i + (GetMaterial.DataQuantity - 1))) + temp));

                for (int i = 0; i < GetMaterial.DataQuantity; i++)
                    GetMaterial.ExtinctionCoefficient.Add(BitConverter.ToSingle(bytes, (4 * (10 + i + (GetMaterial.DataQuantity - 1) + (GetMaterial.DataQuantity - 1))) + temp));

                break;
            }
        }
        #endregion

        #region GET-FFT-POWER-XAXIS
        public struct GetFFTPowerXAxis
        {
            public static int PowerSpectrumHorizontalAxisType = 0;
            public static int DataQuantity = 0;
            public static List<float> PowerSpectrumHorizontalAxis = new List<float>();
        }

        void CheckGetFFTPowerXAxis()
        {
            GetFFTPowerXAxis.PowerSpectrumHorizontalAxisType = 0;
            GetFFTPowerXAxis.DataQuantity = 0;
            GetFFTPowerXAxis.PowerSpectrumHorizontalAxis.Clear();

            TimeoutSW timeout = new TimeoutSW(Common.SwTimer);
            timeout.Start();
            while (true)
            {
                isTimeout = timeout.IsTimeout;
                if (isTimeout)
                {
                    AddLog($"Command {(int)Common.Command.CmdGetFFTPowerXAxis} Is Timeout");
                    break;
                }

                var bytes = Tcp.RecvGetResult();
                if (bytes is null) continue;
                if (bytes.Length == 0) continue;

                // SF-3_CommunicationSpecifications_20180928.pdf Page.10
                // SOH,Binary number,Transmission No,Binary data type,Response code,Status,Reponse parameter 1...
                // bytes 已經去掉SOH
                // 從Transmission No開始算
                sResponseCode = BitConverter.ToInt32(bytes, 4 * 3).ToString();
                sStatus = BitConverter.ToInt32(bytes, 4 * 4).ToString();

                // GET-FFT-POWER-XAXIS Binary data type = 8
                var iType = BitConverter.ToInt32(bytes, 4 * 2);
                if (iType != 8)
                {
                    AddLog("SOH Binary data type <> 8, not GET-FFT-POWER-XAXIS");
                    break;
                }

                GetFFTPowerXAxis.PowerSpectrumHorizontalAxisType = BitConverter.ToInt32(bytes, 4 * 5);
                GetFFTPowerXAxis.DataQuantity = BitConverter.ToInt32(bytes, 4 * 6);

                for (int i = 0; i < GetFFTPowerXAxis.DataQuantity; i++)
                    Reanalyze.ThicknessValue.Add(BitConverter.ToSingle(bytes, 4 * (7 + i)));

                break;
            }
        }
        #endregion

        void AddLog(string msg) => _log.AddLog(msg, "SF3");
    }
}
