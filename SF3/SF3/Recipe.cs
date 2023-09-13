using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Otsuka
{
    public partial class Recipe
    {
        string filePath = Directory.GetCurrentDirectory() + @"\EqpInitData\ConfigParameter.csv";
        public class Parameter
        {
            //Measurement condition
            public string ExposureTime { get; set; }
            public string NumberOfAccumulations { get; set; }
            public string TriggerDelayTime { get; set; }
            public bool ResetElapsedTimeOnTriggerInput_Yes { get; set; }
            public bool ResetElapsedTimeOnTriggerInput_No { get; set; }
            public string MeasurementMode { get; set; }
            public string NumberOfContinuous_measurements { get; set; }
            public string MeasurementCycle { get; set; }
            public string GetSignalSpectrum_Yes { get; set; }
            public string GetSignalSpectrum_No { get; set; }
            public string GetReflectanceSpectrum_Yes { get; set; }
            public string GetReflectanceSpectrum_No { get; set; }
            public string GetAnalysisSpectrum_Yes { get; set; }
            public string GetAnalysisSpectrum_No { get; set; }
            //Main settings
            public string AnalysisWavelengthNumber_Lower { get; set; }
            public string AnalysisWavelengthNumber_Upper { get; set; }
            public string AnalysisMethod { get; set; }
            public string NumOfFFTDataPoints { get; set; }
            public string FFTMaximumThickness { get; set; }
            public string Resolution { get; set; }
            public string NumOfAmalysisLayers { get; set; }
            public List<string> AnalysisThicknessRange_ThicknessRangeLowerLimit { get; set; }
            public List<string> AnalysisThicknessRange_ThicknessRangeUpperLimit { get; set; }
            public List<string> FFTPowerRange_FFTIntensityRangeLowerLimit { get; set; }
            public List<string> FFTPowerRange_FFTIntensityRangeUpperLimit { get; set; }
            public List<string> FFTSearchDirection_list { get; set; }
            public List<string> AnalysisRefractiveIndex_ConstantRefractiveIndex { get; set; }
            public List<string> FFTPeakNum_PeakNumber { get; set; }
            public string AnalysisLayerMaterial { get; set; }
            public string ThicknessMoveingAverage { get; set; }
            //Detail sttings
            public string SmoothingPoint { get; set; }
            public string FFTWindowFunction { get; set; }
            public string FFTNormalization { get; set; }
            public string NGPeakExclusion { get; set; }
            public string FollowupFilterRange { get; set; }
            public string FollowupFilterApplicationTime { get; set; }
            public string FollowupFilterCenterValueMovingAverage { get; set; }
            public string ThicknessVariationTrend { get; set; }
            public string ErrorValue { get; set; }
            public string NumOfErrorForNaN { get; set; }
            public List<string> ThicknessCoef_list { get; set; }
            public List<string> ThicknessCoef_PrimaryCoefficientC1 { get; set; }
            public List<string> ThicknessCoef_ConstantCoefficientC0 { get; set; }
            //Detail sttings (Optimization method)

            public string OptimizationMethodSwitchoverThickness { get; set; }
            public string OptimizationMethodThicknessStepValue { get; set; }
            public string AmbientLayerMaterial { get; set; }
            public List<string> OptimLayerModel_LayerMaterialID { get; set; }
            public List<string> OptimLayerModel_Thickness { get; set; }
        }
        public void WriteToCSV()
        {
            var records = new List<Parameter>
            {
                new Parameter
                {
                    ////Measurement condition
                    //ExposureTime = num_ExposureTime.Text,
                    //NumberOfAccumulations = num_NumberOfAccumulations.Text,
                    //TriggerDelayTime = num_TriggerDelayTime.Text,
                    //ResetElapsedTimeOnTriggerInput_Yes = rad_ResetElapsedTimeOnTriggerInput_Yes.Checked,
                    //ResetElapsedTimeOnTriggerInput_No = rad_ResetElapsedTimeOnTriggerInput_No.Checked,
                    //MeasurementMode = cb_MeasurementMode.Text,
                    //NumberOfContinuous_measurements = num_NumberOfContinuous_measurements.Text,
                    //MeasurementCycle = num_MeasurementCycle.Text,
                    //GetSignalSpectrum_Yes = rad_GetSignalSpectrum_Yes.Checked,
                    //GetSignalSpectrum_No = rad_GetSignalSpectrum_No.Checked,
                    //GetReflectanceSpectrum_Yes = rad_GetReflectanceSpectrum_Yes.Checked,
                    //GetReflectanceSpectrum_No = rad_GetReflectanceSpectrum_No.Checked,
                    //GetAnalysisSpectrum_Yes = rad_GetAnalysisSpectrum_Yes.Checked,
                    //GetAnalysisSpectrum_No = rad_GetAnalysisSpectrum_No.Checked,
                    ////Main settings
                    //AnalysisWavelengthNumber_Lower = num＿AnalysisWavelengthNumber_Lower.Text,
                    //AnalysisWavelengthNumber_Upper = num＿AnalysisWavelengthNumber_Upper.Text,
                    //AnalysisMethod = cb_AnalysisMethod.Text,
                    //NumOfFFTDataPoints = cb_NumOfFFTDataPoints.Text,
                    //FFTMaximumThickness = num_FFTMaximumThickness.Text,
                    //Resolution = txt_Resolution.Text,
                    //NumOfAmalysisLayers = num_NumOfAmalysisLayers.Text,
                    //// = .Text,
                    //AnalysisLayerMaterial = cb_AnalysisLayerMaterial.Text,
                    //ThicknessMoveingAverage = num_ThicknessMoveingAverage.Text,
                    ////Detail sttings
                    //SmoothingPoint = cb_SmoothingPoint.Text,
                    //FFTWindowFunction = checkBox_FFTWindowFunction.Checked,
                    //FFTNormalization = checkBox_FFTNormalization.Checked,
                    //NGPeakExclusion = checkBox_NGPeakExclusion.Checked,
                    //FollowupFilterRange = num_FollowupFilterRange.Text,
                    //FollowupFilterApplicationTime = num_FollowupFilterApplicationTime.Text,
                    //FollowupFilterCenterValueMovingAverage = num_FollowupFilterCenterValueMovingAverage.Text,
                    //ThicknessVariationTrend = cb_ThicknessVariationTrend.Text,
                    //ErrorValue = cb_ErrorValue.Text,
                    //NumOfErrorForNaN = num_NumOfErrorForNaN.Text,
                    // = .Text,
                    ////Detail sttings (Optimization method)
                    //OptimizationMethodSwitchoverThickness = num_OptimizationMethodSwitchoverThickness.Text,
                    //OptimizationMethodThicknessStepValue = num_OptimizationMethodThicknessStepValue.Text,
                    //AmbientLayerMaterial = cb_AmbientLayerMaterial.Text
                    // = .Text,
                }
            };

            using (var writer = new StreamWriter(filePath)) using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) csv.WriteRecords(records);
        }

        public void ReadFromCSV()
        {
            using (var reader = new StreamReader(filePath))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        //                ComboMachineType.Text = csv.GetField<string>("ComboMachineType");
                        //                ComboHSMSRole.Text = csv.GetField<string>("ComboHSMSRole");
                        //                edtHSMST3.Text = csv.GetField<string>("T3");
                        //                edtT5.Text = csv.GetField<string>("T5");
                        //                edtT6.Text = csv.GetField<string>("T6");
                        //                edtT7.Text = csv.GetField<string>("T7");
                        //                edtT8.Text = csv.GetField<string>("T8");
                        //                edtLocalIP.Text = csv.GetField<string>("LocalIP");
                        //                edtLocalPort.Text = csv.GetField<string>("LocalPort");
                        //                edtRemoteIP.Text = csv.GetField<string>("RemoteIP");
                        //                edtRemotePort.Text = csv.GetField<string>("RemotePort");
                        //                edtHSMSDeviceID.Text = csv.GetField<string>("DeviceID");
                        //                edtLinktestPeriod.Text = csv.GetField<string>("LinkTestPeriod");
                    }
                }
            }
        }
    }
}
