//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

namespace TrimGap
{
    internal class MatlabAnalysis
    {
        private PMLcmpDll.Class1 pml = new PMLcmpDll.Class1();

        #region TrimGap

        public void CalculateBlueTape(byte[] data, int w, int h, int line, bool plotflag, int threshold, bool Rotate, out double[] Resultdata)
        {
            MWArray result;

            MWArray[] argIn = new MWArray[] { (MWNumericArray)data, w, h, line, (MWLogicalArray)plotflag, threshold, (MWLogicalArray)Rotate };
            MWArray[] argOut = new MWArray[3];
            pml.calculateBlueTapeW(4, ref argOut, argIn);

            double[,] MatlabToCsharp = (double[,])argOut[1].ToArray(); // 格式轉換
            double[] OutputData1 = new double[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData1[i] = Math.Round(MatlabToCsharp[0, i], 4, MidpointRounding.AwayFromZero);
            }

            Resultdata = OutputData1;
        }

        public void convert(byte[] data, int w, int h)
        {
            MWArray result;
            pml.testconvert((MWNumericArray)data, w, h);
        }

        

        public void tilting(bool plotflag, double[] rawdata, double x_interval, out double[] tiltingdata_x, out double[] tiltingdata_y)
        {
            double[] InputData = rawdata;
            MWNumericArray array1 = (MWNumericArray)InputData;

            MWArray[] argIn = new MWArray[] { (MWLogicalArray)plotflag, (MWNumericArray)InputData, (double)x_interval };
            MWArray[] argOut = new MWArray[2];
            pml.tilting(2, ref argOut, argIn);
            double[,] MatlabToCsharp = (double[,])argOut[0].ToArray(); // 格式轉換

            double[] OutputData1 = new double[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                //OutputData1[i] = MatlabToCsharp[0, i];
                OutputData1[i] = Math.Round(MatlabToCsharp[0, i], 2, MidpointRounding.AwayFromZero);
            }
            double[,] MatlabToCsharp2 = (double[,])argOut[1].ToArray(); // 格式轉換

            double[] OutputData2 = new double[MatlabToCsharp2.Length];
            for (int i = 0; i < MatlabToCsharp2.Length; i++)
            {
                OutputData2[i] = MatlabToCsharp2[0, i];
                //OutputData2[i] = Math.Round(MatlabToCsharp2[0, i], 2, MidpointRounding.AwayFromZero);
            }
            tiltingdata_x = OutputData1;
            tiltingdata_y = OutputData2;
        }


        public void tilting3(bool plotflag, double[] rawdata, double[] rawdata2, double[] rawdata3, double x_interval, out double[] tiltingdata_x1, out double[] tiltingdata_x2, out double[] tiltingdata_x3, out double[] tiltingdata_y1, out double[] tiltingdata_y2, out double[] tiltingdata_y3)
        {
            double[] InputData = rawdata;
            double[] InputData2 = rawdata2;
            double[] InputData3 = rawdata3;

            MWNumericArray array1 = (MWNumericArray)InputData;
            MWNumericArray array2 = (MWNumericArray)InputData2;
            MWNumericArray array3 = (MWNumericArray)InputData3;

            MWArray[] argIn = new MWArray[] { (MWLogicalArray)plotflag, (MWNumericArray)InputData, (MWNumericArray)InputData2, (MWNumericArray)InputData3, (double)x_interval };
            MWArray[] argOut = new MWArray[6];
            pml.tilting3(6, ref argOut, argIn);
            double[,] MatlabToCsharp = (double[,])argOut[0].ToArray(); // 格式轉換

            double[] OutputData1 = new double[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                //OutputData1[i] = MatlabToCsharp[0, i];
                OutputData1[i] = Math.Round(MatlabToCsharp[0, i], 2, MidpointRounding.AwayFromZero);
            }
            //ParamFile.SaveRawdata_Csv(OutputData1, "tilting3_OutputData1", DateTime.Now);

            double[,] MatlabToCsharp2 = (double[,])argOut[1].ToArray(); // 格式轉換

            double[] OutputData2 = new double[MatlabToCsharp2.Length];
            for (int i = 0; i < MatlabToCsharp2.Length; i++)
            {
                //OutputData2[i] = MatlabToCsharp2[0, i];
                OutputData2[i] = Math.Round(MatlabToCsharp2[0, i], 2, MidpointRounding.AwayFromZero);
            }
            //ParamFile.SaveRawdata_Csv(OutputData2, "tilting3_OutputData2", DateTime.Now);

            double[,] MatlabToCsharp3 = (double[,])argOut[2].ToArray(); // 格式轉換

            double[] OutputData3 = new double[MatlabToCsharp3.Length];
            for (int i = 0; i < MatlabToCsharp3.Length; i++)
            {
                //OutputData3[i] = MatlabToCsharp3[0, i];
                OutputData3[i] = Math.Round(MatlabToCsharp3[0, i], 2, MidpointRounding.AwayFromZero);
            }
            //ParamFile.SaveRawdata_Csv(OutputData3, "tilting3_OutputData3", DateTime.Now);

            double[,] MatlabToCsharp4 = (double[,])argOut[3].ToArray(); // 格式轉換

            double[] OutputData4 = new double[MatlabToCsharp4.Length];
            for (int i = 0; i < MatlabToCsharp4.Length; i++)
            {
                //OutputData4[i] = MatlabToCsharp4[0, i];
                OutputData4[i] = Math.Round(MatlabToCsharp4[0, i], 2, MidpointRounding.AwayFromZero);
            }
            //ParamFile.SaveRawdata_Csv(OutputData4, "tilting3_OutputData4", DateTime.Now);

            double[,] MatlabToCsharp5 = (double[,])argOut[4].ToArray(); // 格式轉換

            double[] OutputData5 = new double[MatlabToCsharp5.Length];
            for (int i = 0; i < MatlabToCsharp5.Length; i++)
            {
                //OutputData5[i] = MatlabToCsharp5[0, i];
                OutputData5[i] = Math.Round(MatlabToCsharp5[0, i], 2, MidpointRounding.AwayFromZero);
            }
            //ParamFile.SaveRawdata_Csv(OutputData5, "tilting3_OutputData5", DateTime.Now);

            double[,] MatlabToCsharp6 = (double[,])argOut[5].ToArray(); // 格式轉換

            double[] OutputData6 = new double[MatlabToCsharp6.Length];
            for (int i = 0; i < MatlabToCsharp6.Length; i++)
            {
                //OutputData6[i] = MatlabToCsharp6[0, i];
                OutputData6[i] = Math.Round(MatlabToCsharp6[0, i], 2, MidpointRounding.AwayFromZero);
            }
            //ParamFile.SaveRawdata_Csv(OutputData6, "tilting3_OutputData6", DateTime.Now);

            tiltingdata_x1 = OutputData1;
            tiltingdata_x2 = OutputData2;
            tiltingdata_x3 = OutputData3;
            tiltingdata_y1 = OutputData4;
            tiltingdata_y2 = OutputData5;
            tiltingdata_y3 = OutputData6;
        }

        public void CalculateGap(bool plotflag, double[] rawdata_x, double[] rawdata_y, double x_interval, int Step, int step1x0, int step1x1, int step2x0, int step2x1, int range1, int range2, out double[] Resultdata)
        {
            double[] InputData = rawdata_x;
            double[] InputData2 = rawdata_y;
            MWNumericArray array1 = (MWNumericArray)InputData;
            MWNumericArray array2 = (MWNumericArray)InputData2;

            MWArray[] argIn = new MWArray[] { (MWLogicalArray)plotflag, Step, (MWNumericArray)InputData, (MWNumericArray)InputData2, (double)x_interval, step1x0, step1x1, step2x0, step2x1, range1, range2 };
            MWArray[] argOut = new MWArray[13];
            pml.calculateGap(13, ref argOut, argIn);
            double[,] MatlabToCsharp;
            double[] OutputData = new double[argOut.Length];
            for (int i = 0; i < argOut.Length; i++)
            {
                MatlabToCsharp = (double[,])argOut[i].ToArray(); // 格式轉換
                //OutputData[i] = MatlabToCsharp[0, 0];
                OutputData[i] = Math.Round(MatlabToCsharp[0, 0], 2, MidpointRounding.AwayFromZero);
            }

            //for (int i = 0; i < MatlabToCsharp.Length; i++)
            //{
            //    OutputData1[i] = MatlabToCsharp[0, i];
            //}
            Resultdata = OutputData;
        }

        public void CalculateGap3(bool plotflag, int Step, double[] tiltdata_x1, double[] tiltdata_x2, double[] tiltdata_x3, double[] tiltdata_y1, double[] tiltdata_y2, double[] tiltdata_y3, double x_interval, int step1x0, int step1x1, int step2x0, int step2x1, int range1, int range2, out double[] Resultdata)
        {
            double[] InputData = tiltdata_x1;
            double[] InputData2 = tiltdata_x2;
            double[] InputData3 = tiltdata_x3;
            double[] InputData4 = tiltdata_y1;
            double[] InputData5 = tiltdata_y2;
            double[] InputData6 = tiltdata_y3;

            MWNumericArray array1 = (MWNumericArray)InputData;
            MWNumericArray array2 = (MWNumericArray)InputData2;
            MWNumericArray array3 = (MWNumericArray)InputData3;
            MWNumericArray array4 = (MWNumericArray)InputData4;
            MWNumericArray array5 = (MWNumericArray)InputData5;
            MWNumericArray array6 = (MWNumericArray)InputData6;

            //MWArray[] argIn = new MWArray[] { (MWLogicalArray)plotflag, 2, (MWNumericArray)InputData, (MWNumericArray)InputData2, (MWNumericArray)InputData3, (MWNumericArray)InputData4, (MWNumericArray)InputData5, (MWNumericArray)InputData6 , (double)x_interval, step1x0, step1x1, step2x0, step2x1, range1, range2 };
            MWArray[] argIn = new MWArray[] { (MWLogicalArray)plotflag, 2, (MWNumericArray)InputData, (MWNumericArray)InputData2, (MWNumericArray)InputData3, (MWNumericArray)InputData4, (MWNumericArray)InputData5, (MWNumericArray)InputData6, 1, step1x0, step1x1, step2x0, 2100, range1, range2 };
            MWArray[] argOut = new MWArray[13];
            pml.calculateGap3(13, ref argOut, argIn);
            //argOut = pml.calculateGap3(13, (MWLogicalArray)plotflag, Step, (MWNumericArray)tiltdata_x1, (MWNumericArray)tiltdata_x2, (MWNumericArray)tiltdata_x3, (MWNumericArray)tiltdata_y1, (MWNumericArray)tiltdata_y2, (MWNumericArray)tiltdata_y3, x_interval, step1x0, step1x1, step2x0, step2x1, range1, range2);
            double[,] MatlabToCsharp;
            double[] OutputData = new double[argOut.Length];
            for (int i = 0; i < argOut.Length; i++)
            {
                MatlabToCsharp = (double[,])argOut[i].ToArray(); // 格式轉換
                //OutputData[i] = MatlabToCsharp[0, 0];
                OutputData[i] = Math.Round(MatlabToCsharp[0, 0], 2, MidpointRounding.AwayFromZero);
            }

            //for (int i = 0; i < MatlabToCsharp.Length; i++)
            //{
            //    OutputData1[i] = MatlabToCsharp[0, i];
            //}
            Resultdata = OutputData;
        }

        #endregion TrimGap

        #region 公用區

        public float calculationAvg(float[] data)
        {
            int num = 0;
            float value = 0, sum = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] > 0)
                {
                    sum += data[i];
                    num++;
                }
            }

            if (num > 0)
            {
                value = sum / num;
            }
            value = (float)Math.Round(value, 2, MidpointRounding.AwayFromZero);
            return value;
        }

        public float calculationAvg(float[][] data)
        {
            int num = 0;
            float value = 0, sum = 0;

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    if (data[i][j] > 0)
                    {
                        sum += data[i][j];
                        num++;
                    }
                }
            }

            if (num > 0)
            {
                value = sum / num;
            }
            value = (float)Math.Round(value, 2, MidpointRounding.AwayFromZero);
            return value;
        }

        public float calculationStd(float[] data)
        {
            double value;

            double avg = data.Average();
            double sum = data.Sum(d => Math.Pow(d - avg, 2));
            value = Math.Sqrt(sum / data.Count());
            value = Math.Round(value, 2, MidpointRounding.AwayFromZero);
            return (float)value;
        }

        public float calculationStd(float[][] data)
        {
            int num = 0;
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    if (data[i][j] > 0)
                    {
                        num++;
                    }
                }
            }

            double[] data2 = new double[num];

            num = 0;
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    if (data[i][j] > 0)
                    {
                        data2[num] = data[i][j];
                        num++;
                    }
                }
            }

            double value;
            if (num == 0)
            {
                return 0;
            }
            double avg = data2.Average();
            double sum = data2.Sum(d => Math.Pow(d - avg, 2));
            value = Math.Sqrt(sum / data2.Count());
            value = Math.Round(value, 2, MidpointRounding.AwayFromZero);
            return (float)value;
        }

        #endregion 公用區

        #region float類型 20200821

        public void grooveCalculationLowRPM(float[] rawdata, int samplingrate, int smoothlength_L, int GrooveThreshold, out float[] groovedata, out float groove_depth_avg, out float groove_depth_std) //把數據為data error的直接拿掉 資料出來會比進去少 for keyence
        {
            float[] InputData = rawdata;
            MWNumericArray array1 = (MWNumericArray)InputData;
            //                           1000          100             350
            MWArray[] argIn = new MWArray[] { (MWNumericArray)InputData, samplingrate, smoothlength_L, GrooveThreshold };
            MWArray[] argOut = new MWArray[3];
            pml.grooveCalculationLowRPM(3, ref argOut, argIn);
            //object[,] MatlabToCsharp = (object[,])argOut[0].ToArray(); // 格式轉換
            float[,] MatlabToCsharp = (float[,])argOut[0].ToArray(); // 格式轉換
            float[,] MatlabToCsharp2 = (float[,])argOut[1].ToArray(); // 格式轉換
            float[,] MatlabToCsharp3 = (float[,])argOut[2].ToArray(); // 格式轉換

            //float[,] filtersmallprofile = (float[,])MatlabToCsharp[0, 0];
            //float[,] topprofile = (float[,])MatlabToCsharp[0, 1];

            float[] OutputData1 = new float[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData1[i] = MatlabToCsharp[0, i];
            }
            float OutputData2 = new float();
            OutputData2 = MatlabToCsharp2[0, 0];

            float OutputData3 = new float();
            OutputData3 = MatlabToCsharp3[0, 0];

            groovedata = OutputData1;
            groove_depth_avg = OutputData2;
            groove_depth_std = OutputData3;
        }

        public void localTopData(float[] rawdata, int groove_left, int groove_right, int GrooveThreshold, int topdatanum, out float[] localtopdata, out int[,] topPosition) //把數據為data error的直接拿掉 資料出來會比進去少 for keyence
        {
            float[] InputData = rawdata;
            MWNumericArray array1 = (MWNumericArray)InputData;
            //                                                               3            3             350
            MWArray[] argIn = new MWArray[] { (MWNumericArray)InputData, groove_left, groove_right, GrooveThreshold, topdatanum };
            MWArray[] argOut = new MWArray[2];
            pml.localtopdata(2, ref argOut, argIn);
            //object[,] MatlabToCsharp = (object[,])argOut[0].ToArray(); // 格式轉換
            float[,] MatlabToCsharp = (float[,])argOut[0].ToArray(); // 格式轉換
            double[,] MatlabToCsharp2 = (double[,])argOut[1].ToArray(); // 格式轉換

            float[] OutputData1 = new float[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData1[i] = MatlabToCsharp[0, i];
            }

            int[,] OutputData2 = new int[MatlabToCsharp2.Length / 2, 2];
            for (int i = 0; i < MatlabToCsharp2.Length / 2; i++)
            {
                OutputData2[i, 0] = (int)MatlabToCsharp2[i, 0];
                OutputData2[i, 1] = (int)MatlabToCsharp2[i, 1];
            }

            localtopdata = OutputData1;
            topPosition = OutputData2;
        }

        public float[] extractData(float[] rawdata) // 把資料為零的數據補點 "nearest" 用最近的值去補
        {
            MWArray result;

            float[] InputData = rawdata;
            MWNumericArray array1 = (MWNumericArray)InputData;
            result = pml.extractData(array1);
            float[,] MatlabToCsharp = (float[,])result.ToArray(); // 格式轉換
            float[] OutputData = new float[MatlabToCsharp.Length];
            float[] extraData;
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = MatlabToCsharp[0, i];
            }
            extraData = OutputData;
            return extraData;
        }

        public float[] removeZero(float[] data) //把數據為data error的直接拿掉 資料出來會比進去少 for keyence
        {
            MWArray result;

            float[] InputData = data;
            MWNumericArray array1 = (MWNumericArray)InputData;
            result = pml.removeZero(array1);
            float[,] MatlabToCsharp = (float[,])result.ToArray(); // 格式轉換
            float[] OutputData = new float[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = MatlabToCsharp[0, i];
            }
            return OutputData;
        }

        public float[] removeZero2(float[] data) //把數據為0的直接拿掉 資料出來會比進去少
        {
            MWArray result;

            float[] InputData = data;
            MWNumericArray array1 = (MWNumericArray)InputData;
            result = pml.removeZero2(array1);
            float[,] MatlabToCsharp = (float[,])result.ToArray(); // 格式轉換
            float[] OutputData = new float[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = MatlabToCsharp[0, i];
            }
            return OutputData;
        }

        public void removeSmallProfile(float[] filtereddata, int smoothlength1, int smoothlength2, out float[] filterSmallProfile, out float[] topProfile) //把數據為data error的直接拿掉 資料出來會比進去少 for keyence
        {
            float[] InputData = filtereddata;
            MWNumericArray array1 = (MWNumericArray)InputData;

            MWArray[] argIn = new MWArray[] { (MWNumericArray)InputData, smoothlength1, smoothlength2 };
            MWArray[] argOut = new MWArray[2];
            pml.removeSmallProfile(2, ref argOut, argIn);
            //object[,] MatlabToCsharp = (object[,])argOut[0].ToArray(); // 格式轉換
            float[,] MatlabToCsharp = (float[,])argOut[0].ToArray(); // 格式轉換
            float[,] MatlabToCsharp2 = (float[,])argOut[1].ToArray(); // 格式轉換

            //float[,] filtersmallprofile = (float[,])MatlabToCsharp[0, 0];
            //float[,] topprofile = (float[,])MatlabToCsharp[0, 1];

            float[] OutputData1 = new float[MatlabToCsharp.Length];
            float[] OutputData2 = new float[MatlabToCsharp2.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData1[i] = MatlabToCsharp[0, i];
            }
            for (int i = 0; i < MatlabToCsharp2.Length; i++)
            {
                OutputData2[i] = MatlabToCsharp2[0, i];
            }

            filterSmallProfile = OutputData1;
            topProfile = OutputData2;
        }

        public void Hist(float[] filtereddata, int HistNum, out float[,] hist)
        {
            float[] InputData = filtereddata;
            MWNumericArray array1 = (MWNumericArray)InputData;

            MWArray[] argIn = new MWArray[] { (MWNumericArray)InputData, HistNum };
            MWArray[] argOut = new MWArray[1];
            pml.Hist(1, ref argOut, argIn);
            //object[,] MatlabToCsharp = (object[,])argOut[0].ToArray(); // 格式轉換
            double[,] MatlabToCsharp = (double[,])argOut[0].ToArray(); // 格式轉換

            //
            float[,] OutputData1 = new float[MatlabToCsharp.Length / 2, 2];
            for (int i = 0; i < MatlabToCsharp.Length / 2; i++)
            {
                OutputData1[i, 0] = (float)MatlabToCsharp[i, 0];
                OutputData1[i, 1] = (float)MatlabToCsharp[i, 1];
            }

            hist = OutputData1;
        }

        public void GrooveFind2(float[] topProfile, int HistNum, double botPercent, double topPercent, out float[,] All_hist, out float[] fwhmValue) //把數據為data error的直接拿掉 資料出來會比進去少 for keyence
        {
            float[] InputData = topProfile;
            MWNumericArray array1 = (MWNumericArray)InputData;

            MWArray[] argIn = new MWArray[] { (MWNumericArray)InputData, HistNum, botPercent, topPercent };
            MWArray[] argOut = new MWArray[6];
            pml.GrooveFind2(6, ref argOut, argIn);
            //object[,] MatlabToCsharp = (object[,])argOut[0].ToArray(); // 格式轉換
            double[,] MatlabToCsharp = (double[,])argOut[0].ToArray(); // 格式轉換

            //
            float[,] OutputData1 = new float[MatlabToCsharp.Length / 2, 2];
            for (int i = 0; i < MatlabToCsharp.Length / 2; i++)
            {
                OutputData1[i, 0] = (float)MatlabToCsharp[i, 0];
                OutputData1[i, 1] = (float)MatlabToCsharp[i, 1];
            }
            //
            double[,] MatlabToCsharp1; // 格式轉換
            double Result; //= Math.Round(x, 3, MidpointRounding.AwayFromZero);
            float[] OutputData2 = new float[5];
            for (int i = 1; i < 6; i++)
            {
                MatlabToCsharp1 = (double[,])argOut[i].ToArray();
                Result = Math.Round(MatlabToCsharp1[0, 0], 1, MidpointRounding.AwayFromZero); //取到小數第一位
                OutputData2[i - 1] = (float)Result;
                //OutputData2[i - 1] = (float)MatlabToCsharp1[0, 0];
            }

            //float[,] OutputData1 = new float[2, all_hist.Length];
            //float[] OutputData2 = new float[5];
            //for (int i = 0; i < all_hist.Length; i++)
            //{
            //    OutputData1[0, i] = all_hist[0, i];
            //}

            //float[,] botvalue1 = (float[,])MatlabToCsharp[0, 1];
            //float[,] botvalue2 = (float[,])MatlabToCsharp[0, 2];
            //float[,] centervalue = (float[,])MatlabToCsharp[0, 3];
            //float[,] topvalue1 = (float[,])MatlabToCsharp[0, 4];
            //float[,] topvalue2 = (float[,])MatlabToCsharp[0, 5];

            //OutputData2[0] = botvalue1[0, 0];
            //OutputData2[1] = botvalue2[0, 0];
            //OutputData2[2] = centervalue[0, 0];
            //OutputData2[3] = topvalue1[0, 0];
            //OutputData2[4] = topvalue2[0, 0];

            All_hist = OutputData1;
            fwhmValue = OutputData2;
        }

        public void GrooveFindSoft(float[] filtereddata, int HistNum, double botPercent, out float[,] All_hist, out float[] noGroove, out float GrooveDepth) //把數據為data error的直接拿掉 資料出來會比進去少 for keyence
        {
            float[] InputData = filtereddata;
            MWNumericArray array1 = (MWNumericArray)InputData;

            MWArray[] argIn = new MWArray[] { (MWNumericArray)InputData, HistNum, botPercent };
            MWArray[] argOut = new MWArray[3];
            pml.GrooveFindSoft(3, ref argOut, argIn);
            //object[,] MatlabToCsharp = (object[,])argOut[0].ToArray(); // 格式轉換
            double[,] MatlabToCsharp1 = (double[,])argOut[0].ToArray(); // 格式轉換

            //
            float[,] OutputData1 = new float[MatlabToCsharp1.Length / 2, 2];
            for (int i = 0; i < MatlabToCsharp1.Length / 2; i++)
            {
                OutputData1[i, 0] = (float)MatlabToCsharp1[i, 0];
                OutputData1[i, 1] = (float)MatlabToCsharp1[i, 1];
            }
            //
            float[,] MatlabToCsharp2 = (float[,])argOut[1].ToArray(); // 格式轉換
            float[] OutputData2 = new float[MatlabToCsharp2.Length];
            for (int i = 0; i < MatlabToCsharp2.Length; i++)
            {
                OutputData2[i] = MatlabToCsharp2[0, i];
            }
            float[,] MatlabToCsharp3 = (float[,])argOut[2].ToArray(); // 格式轉換
            float OutputData3 = (float)MatlabToCsharp3[0, 0];

            All_hist = OutputData1;
            noGroove = OutputData2;
            GrooveDepth = OutputData3;
        }

        public float effectiveRatio(float[] rawData, float[] removeZero2)
        {
            float EffectiveRatio = (float)removeZero2.Length / (float)rawData.Length; // 拿前面已經處理過的數據計算
            return (float)(Math.Round((double)EffectiveRatio, 4));                    // 總共取到小數第3位
        }

        /// <summary>
        /// ratioData : spk,sk,svk,smr1,smr2,smrSpk,smrSk,smrSvk,a,b
        /// ratioAreaData : a,b [2 x X]
        /// ZHeigh :
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ratioData"></param>
        /// <param name="ratioAreaData"></param>
        /// <param name="ZHeigh"></param>
        public void bearingRatio(float[] filtereddata, int removelength, out float[] ratioValue, out float[,] ratioAreaData, out float[] ZHeight)
        {
            //資料輸出接口完成 object

            // MWArray result;

            float[] InputData = filtereddata;

            // 210906 測試 去頭去尾 計算Rpk
            // 220428 去頭尾 1000 -> 100
            //int removelength = 100; //前後個去掉的數量  H:100, L:0
            float[] InputData_removeFirstAndLast = new float[InputData.Length - removelength * 2];
            Array.Copy(InputData, removelength, InputData_removeFirstAndLast, 0, InputData.Length - removelength * 2);

            // MWNumericArray argIn = (MWNumericArray)InputData;
            float[] OutputData1 = new float[10];

            //MWArray[] argIn = new MWArray[] { (MWNumericArray)InputData };
            MWArray[] argIn = new MWArray[] { (MWNumericArray)InputData_removeFirstAndLast };   //210906
            MWArray[] argOut = new MWArray[13];
            pml.bearingratio(2, ref argOut, argIn);
            object[,] MatlabToCsharp = (object[,])argOut[0].ToArray(); // 格式轉換

            float[,] spk = (float[,])MatlabToCsharp[0, 0];
            float[,] sk = (float[,])MatlabToCsharp[0, 1];
            float[,] svk = (float[,])MatlabToCsharp[0, 2];
            double[,] smr1 = (double[,])MatlabToCsharp[0, 3];
            double[,] smr2 = (double[,])MatlabToCsharp[0, 4];
            double[,] smrSpk = (double[,])MatlabToCsharp[0, 5];
            double[,] smrSk = (double[,])MatlabToCsharp[0, 6];
            double[,] smrSvk = (double[,])MatlabToCsharp[0, 7];
            double[,] b_r = (double[,])MatlabToCsharp[0, 8];
            float[,] z = (float[,])MatlabToCsharp[0, 9];
            float[,] a = (float[,])MatlabToCsharp[0, 10];
            float[,] b = (float[,])MatlabToCsharp[0, 11];
            float[,] ZHeightall = (float[,])MatlabToCsharp[0, 12];

            OutputData1[0] = spk[0, 0];
            OutputData1[1] = sk[0, 0];
            OutputData1[2] = svk[0, 0];
            OutputData1[3] = (float)smr1[0, 0];
            OutputData1[4] = (float)smr2[0, 0];
            OutputData1[5] = (float)smrSpk[0, 0];
            OutputData1[6] = (float)smrSk[0, 0];
            OutputData1[7] = (float)smrSvk[0, 0];
            OutputData1[8] = a[0, 0];
            OutputData1[9] = b[0, 0];

            float[,] OutputData2 = new float[2, b_r.Length];
            float[] OutputData3 = new float[ZHeightall.Length];
            for (int i = 0; i < b_r.Length; i++)
            {
                OutputData2[0, i] = (float)b_r[0, i];
                OutputData2[1, i] = z[0, i];
            }
            for (int i = 0; i < ZHeightall.Length; i++)
            {
                OutputData3[i] = ZHeightall[0, i];
            }
            ratioValue = OutputData1;
            ratioAreaData = OutputData2;
            ZHeight = OutputData3;
        }

        public float[] roughnessAnalysis(float[] filtereddata, int removelength) //計算粗糙度
        {
            MWArray result;

            float[] InputData = filtereddata;
            // 210906 測試 去頭去尾 計算Rpk
            // 220428 去頭尾 1000-> 100
            //int removelength = 100; //前後個去掉的數量
            float[] InputData_removeFirstAndLast = new float[InputData.Length - removelength * 2];
            Array.Copy(InputData, removelength, InputData_removeFirstAndLast, 0, InputData.Length - removelength * 2);
            //

            //MWNumericArray array1 = (MWNumericArray)InputData;
            MWNumericArray array1 = (MWNumericArray)InputData_removeFirstAndLast; //改為去頭去尾計算
            result = pml.roughnessAnalysis(array1);
            float[,] MatlabToCsharp = (float[,])result.ToArray(); // 格式轉換
            float[] OutputData = new float[MatlabToCsharp.Length];
            float[] allRa;
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = MatlabToCsharp[0, i];
            }
            allRa = OutputData;
            return allRa; // [Rq Ra Rz Rp Rv Rsk Rku] 輸出7個參數
        }

        /// <summary>
        /// 輸出 [part , 7]的矩陣 [Rq Ra Rz Rp Rv Rsk Rku] 輸出7個參數
        /// </summary>
        /// <param name="data"></param>
        /// <param name="part"></param>
        /// <returns></returns>
        public float[,] ringRoughness(float[] filtereddata, int part) // 分區計算粗糙度
        {
            MWArray result;

            float[] InputData = filtereddata;
            MWNumericArray array1 = (MWNumericArray)InputData;
            result = pml.ringRoughness(array1, part);
            float[,] MatlabToCsharp = (float[,])result.ToArray(); // 格式轉換
            float[,] ringRa = MatlabToCsharp;
            return ringRa; // 輸出 [part , 7]的矩陣
                           //            [Rq
                           //             Ra
                           //             Rz
                           //             Rp
                           //             Rv
                           //             Rsk
                           //             Rku] 輸出7個參數
        }

        /// <summary>
        /// 輸出 [part]的矩陣 看分幾區就有幾個data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="part"></param>
        /// <returns></returns>
        public float[] ringGroove(float[] grooveDepthDatadata, int part) // 分區計算溝深
        {
            MWArray result;

            float[] InputData = grooveDepthDatadata;
            MWNumericArray array1 = (MWNumericArray)InputData;
            result = pml.ringGroove(array1, part);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            float[] OutputData = new float[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = (float)MatlabToCsharp[0, i];
            }
            float[] ringGrooveValue = OutputData;
            return ringGrooveValue; // 輸出 [part]的矩陣 看分幾區就有幾個data
        }

        public float[] removeGroove(float[] extradata, int threashold) // 取出無溝槽資料 //threshold為要切的深度閥值 設越高即越靠近表面高度 0-100
        {
            MWArray result;

            float[] InputData = extradata;
            MWNumericArray array1 = (MWNumericArray)InputData;
            result = pml.removeGroove(array1, 70);
            float[,] MatlabToCsharp = (float[,])result.ToArray(); // 格式轉換
            float[] OutputData = new float[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = MatlabToCsharp[i, 0];
            }
            float[] noGrooveData = OutputData;
            return noGrooveData; //
        }

        public float[] removeGroove2(float[] extradata, int threashold, int samplingRate) // 取出無溝槽資料 //threshold為要切的深度閥值 設越高即越靠近表面高度 0-100
        {
            MWArray result;

            //float[] InputData = extradata;
            double[] InputData = new double[extradata.Length];
            for (int i = 0; i < extradata.Length; i++)
            {
                InputData[i] = (double)extradata[i];
            }
            MWNumericArray array1 = (MWNumericArray)InputData;
            result = pml.removeGroove2(array1, threashold, samplingRate);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            float[] OutputData = new float[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = (float)MatlabToCsharp[0, i];
            }
            float[] noGrooveData = OutputData;
            return noGrooveData; //
        }

        /// <summary>
        /// threashold 70 可調整
        /// </summary>
        /// <param name="extradata"></param>
        /// <param name="threashold"></param>
        /// <param name="samplingRate"></param>
        /// <returns></returns>
        public float[] removeGroove2_FixRadius(float[] extradata, int downthreashold, int upthreashold, int fillmissingPoint, int samplingRate, int smoothlength_FixRadius) // 取出無溝槽資料 //threshold為要切的深度閥值 設越高即越靠近表面高度 0-100
        {
            MWArray result;

            //float[] InputData = extradata;
            double[] InputData = new double[extradata.Length];
            for (int i = 0; i < extradata.Length; i++)
            {
                InputData[i] = (double)extradata[i];
            }
            MWNumericArray array1 = (MWNumericArray)InputData;
            result = pml.removeGroove2_FixRadius(array1, downthreashold, upthreashold, fillmissingPoint, samplingRate, smoothlength_FixRadius);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            float[] OutputData = new float[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = (float)MatlabToCsharp[0, i];
            }
            float[] noGrooveData = OutputData;
            return noGrooveData; //
        }

        /// <summary>
        /// Highpass 3 Hz左右, band stop 40 - 50 Hz 機台頻率, samplingrate 1000 Hz
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Highpass"></param>
        /// <param name="FH2"></param>
        /// <param name="FL2"></param>
        /// <param name="SamplingRate"></param>
        /// <returns></returns>
        public float[] iirFilter(float[] noGrooveData, int FH1, int FL2, int FH2, int SamplingRate)
        {
            MWArray result;
            // FH1 high pass 3 Hz左右
            // FL2 band stop 40 Hz 搖臂頻率
            // FH2 band stop 50 Hz
            int Samplingrate = SamplingRate;
            double[] InputData1 = new double[noGrooveData.Length];  //輸入轉double
            for (int i = 0; i < noGrooveData.Length; i++)
            {
                InputData1[i] = Convert.ToDouble(noGrooveData[i]);
            }

            MWNumericArray array1 = (MWNumericArray)InputData1;
            result = pml.iirFilter(array1, (MWArray)FH1, (MWArray)FH2, (MWArray)FL2, (MWArray)Samplingrate);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            float[] OutputData = new float[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = (float)MatlabToCsharp[0, i];
            }
            float[] filteredData = OutputData;
            return filteredData; //
        }

        /// <summary>
        /// Highpass 5 Hz左右, 150 200, samplingrate 1000 Hz
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Highpass"></param>
        /// <param name="FH2"></param>
        /// <param name="FL2"></param>
        /// <param name="SamplingRate"></param>
        /// <returns></returns>
        public float[] iirFilter2(float[] noGrooveData, double FH1, double FL2, int SamplingRate)
        {
            MWArray result;
            // FH1 high pass 3 Hz左右
            // FL2 band stop 40 Hz 搖臂頻率
            // FH2 band stop 50 Hz
            int Samplingrate = SamplingRate;
            double[] InputData1 = new double[noGrooveData.Length];  //輸入轉double
            for (int i = 0; i < noGrooveData.Length; i++)
            {
                InputData1[i] = Convert.ToDouble(noGrooveData[i]);
            }

            MWNumericArray array1 = (MWNumericArray)InputData1;
            result = pml.iirFilter2(array1, (MWArray)FH1, (MWArray)FL2, (MWArray)Samplingrate);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            float[] OutputData = new float[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = (float)MatlabToCsharp[0, i];
            }
            float[] filteredData = OutputData;
            return filteredData; //
        }

        public float[] iirFilter3M(float[] noGrooveData, int FH1, int FL2, int FH2, int SamplingRate)
        {
            MWArray result;
            // FH1 high pass 3 Hz左右
            // FL2 band stop 40 Hz 搖臂頻率
            // FH2 band stop 50 Hz
            int Samplingrate = SamplingRate;
            double[] InputData1 = new double[noGrooveData.Length];  //輸入轉double
            for (int i = 0; i < noGrooveData.Length; i++)
            {
                InputData1[i] = Convert.ToDouble(noGrooveData[i]);
            }

            MWNumericArray array1 = (MWNumericArray)InputData1;
            result = pml.iirFilter3M(array1, (MWArray)FH1, (MWArray)FL2, (MWArray)FH2, (MWArray)Samplingrate);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            float[] OutputData = new float[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = (float)MatlabToCsharp[i, 0];
            }
            float[] filteredData = OutputData;
            return filteredData; //
        }

        /// <summary>
        /// (校正後資料，pad原始厚度(um)，取樣頻率) 輸出為百分比
        /// </summary>
        /// <param name="data"></param>
        /// <param name="padThickness"></param>
        /// <param name="SamplingRate"></param>
        /// <returns></returns>
        public float puCaculation(float[] PUdata, float padThickness, int SamplingRate) //把數據為0的直接拿掉 資料出來會比進去少
        {
            MWArray result;

            float[] InputData = PUdata;
            MWNumericArray array1 = (MWNumericArray)InputData;
            result = pml.puCalculation(array1, padThickness, SamplingRate);
            float[,] MatlabToCsharp = (float[,])result.ToArray(); // 格式轉換
            float OutputData;
            OutputData = MatlabToCsharp[0, 0];

            float PUValue = (float)Math.Round(OutputData, 2, MidpointRounding.AwayFromZero);
            return PUValue;
        }

        public void grooveCalculation(float[] DetrendGroovedata, int TopProfileRange, int GrooveRange, out float[] grooveDepthData, out float grooveAvgValue) //
        {
            float[] InputData = DetrendGroovedata;
            // MWNumericArray argIn = (MWNumericArray)InputData;
            float[,] MatlabToCsharp;

            MWArray[] argIn = new MWArray[] { (MWNumericArray)InputData, TopProfileRange, GrooveRange };
            MWArray[] argOut = new MWArray[2];
            pml.grooveCalculation(2, ref argOut, argIn);
            // grooveDepthData 矩陣資料
            MatlabToCsharp = (float[,])argOut[0].ToArray(); // 格式轉換
            float[] outputdata1 = new float[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                outputdata1[i] = MatlabToCsharp[0, i];
            }
            grooveDepthData = outputdata1;

            // grooveAvg 單一個值
            float outputdata2;
            MatlabToCsharp = (float[,])argOut[1].ToArray(); // 格式轉換
            outputdata2 = MatlabToCsharp[0, 0];
            grooveAvgValue = outputdata2;
        }

        public float grooveCalculation2(float[] Groovedata, int SamplingRate, int SmoothlengthFixRadius) //
        {
            float[] InputData = Groovedata;
            // MWNumericArray argIn = (MWNumericArray)InputData;
            float[,] MatlabToCsharp;

            MWArray[] argIn = new MWArray[] { (MWNumericArray)InputData, SamplingRate, SmoothlengthFixRadius };
            MWArray[] argOut = new MWArray[1];
            pml.grooveCalculation2(1, ref argOut, argIn);

            float grooveDepthData;
            MatlabToCsharp = (float[,])argOut[0].ToArray(); // 格式轉換
            grooveDepthData = MatlabToCsharp[0, 0];

            return grooveDepthData;
        }

        public float[] removeSwingArmTrendline(float[] rawdata, int SamplingRate) // 取出無溝槽資料 //threshold為要切的深度閥值 設越高即越靠近表面高度 0-100
        {
            MWArray result;

            //float[] InputData1 = NoGroovedata;
            float[] InputData2 = rawdata;
            //MWNumericArray array1 = (MWNumericArray)InputData1;
            MWNumericArray array2 = (MWNumericArray)InputData2;
            result = pml.removeSwingArmTrendline(array2, SamplingRate);
            float[,] MatlabToCsharp = (float[,])result.ToArray(); // 格式轉換
            float[] OutputData = new float[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = MatlabToCsharp[0, i];
            }
            float[] detrendGrooveData = OutputData;
            return detrendGrooveData; //
        }

        /* 0929前 舊的
        public float[] removeSwingArmTrendline(float[] NoGroovedata, float[] rawdata, int SamplingRate) // 取出無溝槽資料 //threshold為要切的深度閥值 設越高即越靠近表面高度 0-100
        {
            MWArray result;

            float[] InputData1 = NoGroovedata;
            float[] InputData2 = rawdata;
            MWNumericArray array1 = (MWNumericArray)InputData1;
            MWNumericArray array2 = (MWNumericArray)InputData2;
            result = pml.removeSwingArmTrendline(array1, array2, SamplingRate);
            float[,] MatlabToCsharp = (float[,])result.ToArray(); // 格式轉換
            float[] OutputData = new float[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = MatlabToCsharp[0, i];
            }
            float[] detrendGrooveData = OutputData;
            return detrendGrooveData; //
        }
        */

        public void FFT(float[] data, int samplingrate, out float[,] f) // 20200819
        {
            //MWArray result;
            double[,] MatlabToCsharp1;
            float[,] MatlabToCsharp2;
            float[] InputData1 = data;
            int InputData2 = samplingrate;
            MWNumericArray array1 = (MWNumericArray)InputData1;
            MWNumericArray array2 = (MWNumericArray)InputData2;
            MWArray[] argIn = new MWArray[] { (MWNumericArray)array1, (MWNumericArray)array2 };
            MWArray[] argOut = new MWArray[2];
            pml.FFTfuntion(2, ref argOut, argIn);

            // frequency 矩陣資料
            MatlabToCsharp1 = (double[,])argOut[0].ToArray(); // 格式轉換
            float[,] outputdata1 = new float[2, MatlabToCsharp1.Length];
            for (int i = 0; i < MatlabToCsharp1.Length; i++)
            {
                outputdata1[0, i] = (float)MatlabToCsharp1[0, i];
                outputdata1[0, i] = (float)Math.Round(outputdata1[0, i], 3, MidpointRounding.AwayFromZero);
            }

            // amp 矩陣資料
            MatlabToCsharp2 = (float[,])argOut[1].ToArray(); // 格式轉換
            //float[] outputdata2 = new float[MatlabToCsharp2.Length];
            for (int i = 0; i < MatlabToCsharp2.Length; i++)
            {
                outputdata1[1, i] = MatlabToCsharp2[0, i];
                outputdata1[0, i] = (float)Math.Round(outputdata1[0, i], 3, MidpointRounding.AwayFromZero);
            }

            f = outputdata1;
        }

        public float[] halffiltereddata(float[] filtereddata) // 20200817
        {
            float[] halffiltereddata = new float[filtereddata.Length / 2];
            float[] OutputData = new float[filtereddata.Length / 2];
            for (int i = 0; i < filtereddata.Length / 2; i++)
            {
                halffiltereddata[i] = filtereddata[i];
                OutputData = halffiltereddata;
            }
            return OutputData;
        }

        public void BaselineCreat(int baselinecount, string[] filepath, out float[] baseline) // 20200818
        {
            string[] baselinePath = new string[baselinecount];
            float[][] baseline_data = new float[8][];
            int min_length = 0;
            for (int i = 0; i < baselinecount; i++)
            {
                //StreamReader readline = new StreamReader(@"C:\Users\user\Desktop\ttt.txt");
                //System.IO.FileInfo openName = new FileInfo(filepath[i]);//取得完整檔名(含副檔名)
                //string Name = openName.Name.Substring(0, openName.Name.Length - 4);//取得檔名
                //System.IO.FileInfo openName = new FileInfo(filepath[i]);//取得完整檔名(含副檔名)
                StreamReader read = new StreamReader(filepath[i]);
                string ReadAll;
                string[] ReadArray1;//, ReadArray2;
                                    //string[] ReadArray2;
                                    //ReadLine1 = str.ReadLine();
                                    //ReadLine2 = str.ReadLine(); // 一次讀一行
                ReadAll = read.ReadToEnd(); // 一次讀全部
                ReadArray1 = Regex.Split(ReadAll, "\r\n", RegexOptions.IgnoreCase);
                float[] array2 = new float[ReadArray1.Length - 2];
                for (int j = 1; j < ReadArray1.Length - 1; j++)
                {
                    array2[j - 1] = Convert.ToSingle(ReadArray1[j]);
                }
                read.Close();
                baseline_data[i] = new float[array2.Length];
                //Array.Copy(array2, baseline_data[i][], array2.Length);
                baseline_data[i] = array2;
                if (min_length == 0)
                {
                    min_length = array2.Length;
                }
                else if (array2.Length < min_length)
                {
                    min_length = array2.Length;
                }
            }

            double[,] MatlabToCsharp1;
            int SamplingRate = 1000;
            double[,] InputData1 = new double[8, min_length];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < min_length; j++)
                {
                    InputData1[i, j] = (double)baseline_data[i][j];
                }
            }

            MWNumericArray array1 = (MWNumericArray)InputData1;
            MWArray[] argIn = new MWArray[] { (MWNumericArray)array1, SamplingRate };
            MWArray[] argOut = new MWArray[1];
            //pml.FFTfuntion(2, ref argOut, argIn);
            MWArray result;
            result = pml.baselineCalculate(array1, SamplingRate);
            pml.baselineCalculate(1, ref argOut, argIn);

            // frequency 矩陣資料
            MatlabToCsharp1 = (double[,])argOut[0].ToArray(); // 格式轉換
            float[] outputdata1 = new float[MatlabToCsharp1.Length];
            for (int i = 0; i < MatlabToCsharp1.Length; i++)
            {
                outputdata1[i] = (float)MatlabToCsharp1[0, i];
            }
            baseline = outputdata1;
        }

        public float[] ceramicBaseline(float[] noGrooveData, float[] BaselineData, int samplingRate) // 20200818
        {
            //
            MWArray result;
            double[] InputData1 = new double[noGrooveData.Length];  //輸入轉double
            for (int i = 0; i < noGrooveData.Length; i++)
            {
                InputData1[i] = Convert.ToDouble(noGrooveData[i]);
            }
            double[] InputData2 = new double[BaselineData.Length];  // 輸入轉double
            for (int i = 0; i < BaselineData.Length; i++)
            {
                InputData2[i] = Convert.ToDouble(BaselineData[i]);
            }

            MWNumericArray array1 = (MWNumericArray)InputData1;
            MWNumericArray array2 = (MWNumericArray)InputData2;
            result = pml.ceramicBaseline(array1, samplingRate, array2);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            float[] OutputData = new float[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = (float)MatlabToCsharp[0, i];
            }
            float[] PUData = OutputData;
            return PUData;
        }

        public float[] ceramicBaseline2(float[] noGrooveData, int samplingRate, int Height) // 20200818
        {
            //
            MWArray result;
            double[] InputData1 = new double[noGrooveData.Length];  //輸入轉double
            for (int i = 0; i < noGrooveData.Length; i++)
            {
                InputData1[i] = Convert.ToDouble(noGrooveData[i]);
            }

            MWNumericArray array1 = (MWNumericArray)InputData1;
            result = pml.ceramicBaseline2(array1, samplingRate, Height);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            float[] OutputData = new float[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = (float)MatlabToCsharp[0, i];
            }
            float[] PUData = OutputData;
            return PUData;
        }

        public float[] ceramicBaseline3(float[] noGrooveData, float[] baseline) // 20200818
        {
            //
            MWArray result;
            double[] InputData1 = new double[noGrooveData.Length];  //輸入轉double
            for (int i = 0; i < noGrooveData.Length; i++)
            {
                InputData1[i] = Convert.ToDouble(noGrooveData[i]);
            }
            double[] InputData2 = new double[baseline.Length];  //輸入轉double
            for (int i = 0; i < baseline.Length; i++)
            {
                InputData2[i] = Convert.ToDouble(baseline[i]);
            }

            result = pml.ceramicBaseline3((MWNumericArray)InputData1, (MWNumericArray)InputData2);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            float[] OutputData = new float[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = (float)MatlabToCsharp[0, i];
            }
            float[] PUData = OutputData;
            return PUData;
        }

        public void ReviseLocus(double Swingangle, int samplingRate, double Drpm, double Prpm, double dx, double dy, double L, double DresserStartAngle, double PadStartAngle, out float[] xx, out float[] yy)
        {
            //MWArray result;
            double[,] MatlabToCsharp1;
            double[,] MatlabToCsharp2;
            double InputData1 = Swingangle;
            int InputData2 = samplingRate;
            MWNumericArray array1 = (MWNumericArray)InputData1;
            MWNumericArray array2 = (MWNumericArray)InputData2;
            //MWArray[] argIn = new MWArray[] { (MWNumericArray)array1, (MWNumericArray)array2 };
            MWArray[] argIn = new MWArray[] { Swingangle, samplingRate, Drpm, Prpm, dx, dy, L, DresserStartAngle, PadStartAngle };
            MWArray[] argOut = new MWArray[2];
            pml.ReviseLocus(2, ref argOut, argIn);

            // xx 矩陣資料
            MatlabToCsharp1 = (double[,])argOut[0].ToArray(); // 格式轉換
            float[] outputdata1 = new float[MatlabToCsharp1.Length];
            for (int i = 0; i < MatlabToCsharp1.Length; i++)
            {
                outputdata1[i] = (float)MatlabToCsharp1[0, i];
                outputdata1[i] = (float)Math.Round(outputdata1[i], 3, MidpointRounding.AwayFromZero);
            }

            // yy 矩陣資料
            MatlabToCsharp2 = (double[,])argOut[1].ToArray(); // 格式轉換
            float[] outputdata2 = new float[MatlabToCsharp2.Length];
            for (int i = 0; i < MatlabToCsharp2.Length; i++)
            {
                outputdata2[i] = (float)MatlabToCsharp2[0, i];
                outputdata2[i] = (float)Math.Round(outputdata2[i], 3, MidpointRounding.AwayFromZero);
            }

            xx = outputdata1;
            yy = outputdata2;
        }

        public void sectiongroove2(float[,] PUvq, int part, float origin_pad_groove, out float[] ringGrooveValue)
        {
            double[,] InputData1 = new double[PUvq.GetLength(0), PUvq.GetLength(0)];
            for (int i = 0; i < PUvq.GetLength(0); i++)
            {
                for (int j = 0; j < PUvq.GetLength(0); j++)
                {
                    InputData1[i, j] = (double)PUvq[i, j];
                }
            }
            // MWArray result;
            // result = pml.sectiongroove2((MWNumericArray)InputData1, part, origin_pad_groove);
            // double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換

            MWArray[] argIn = new MWArray[] { (MWNumericArray)InputData1, part, origin_pad_groove };
            MWArray[] argOut = new MWArray[1];
            pml.sectiongroove2(1, ref argOut, argIn);

            double[,] MatlabToCsharp = (double[,])argOut[0].ToArray(); // 格式轉換
            float[] OutputData = new float[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = (float)MatlabToCsharp[0, i];
            }
            ringGrooveValue = OutputData;
        }

        public void meshplot(float[] PUxx, float[] PUyy, float[] puData, int Di, out float[,] xq, out float[,] yq, out float[,] PUvq)
        {
            // MWArray result;

            double[] InputData1 = new double[PUxx.Length];
            for (int i = 0; i < PUxx.Length; i++)
            {
                InputData1[i] = (double)PUxx[i];
            }
            double[] InputData2 = new double[PUyy.Length];
            for (int i = 0; i < PUyy.Length; i++)
            {
                InputData2[i] = (double)PUyy[i];
            }
            double[] InputData3 = new double[puData.Length];
            for (int i = 0; i < puData.Length; i++)
            {
                InputData3[i] = (double)puData[i];
            }
            // MWNumericArray argIn = (MWNumericArray)InputData;
            //float[] OutputData1 = new float[3];

            MWArray[] argIn = new MWArray[] { (MWNumericArray)InputData1, (MWNumericArray)InputData2, (MWNumericArray)InputData3, Di };
            MWArray[] argOut = new MWArray[3];
            pml.meshplot(3, ref argOut, argIn);
            // xq 矩陣資料
            double[,] MatlabToCsharp1 = (double[,])argOut[0].ToArray(); // 格式轉換
            float[,] outputdata1 = new float[MatlabToCsharp1.GetLength(0), MatlabToCsharp1.GetLength(0)];
            float tmp = 0;
            for (int i = 0; i < MatlabToCsharp1.GetLength(0); i++)
            {
                for (int j = 0; j < MatlabToCsharp1.GetLength(0); j++)
                {
                    tmp = (float)MatlabToCsharp1[i, j];
                    outputdata1[i, j] = (float)Math.Round(tmp, 3, MidpointRounding.AwayFromZero);
                }
            }

            // yq 矩陣資料
            double[,] MatlabToCsharp2 = (double[,])argOut[1].ToArray(); // 格式轉換
            float[,] outputdata2 = new float[MatlabToCsharp2.GetLength(0), MatlabToCsharp2.GetLength(0)];
            for (int i = 0; i < MatlabToCsharp2.GetLength(0); i++)
            {
                for (int j = 0; j < MatlabToCsharp2.GetLength(0); j++)
                {
                    tmp = (float)MatlabToCsharp2[i, j];
                    outputdata2[i, j] = (float)Math.Round(tmp, 3, MidpointRounding.AwayFromZero);
                }
            }

            // PUvq 矩陣資料
            double[,] MatlabToCsharp3 = (double[,])argOut[2].ToArray(); // 格式轉換
            float[,] outputdata3 = new float[MatlabToCsharp3.GetLength(0), MatlabToCsharp3.GetLength(0)];
            for (int i = 0; i < MatlabToCsharp3.GetLength(0); i++)
            {
                for (int j = 0; j < MatlabToCsharp3.GetLength(0); j++)
                {
                    tmp = (float)MatlabToCsharp3[i, j];
                    outputdata3[i, j] = (float)Math.Round(tmp, 3, MidpointRounding.AwayFromZero);
                }
            }

            xq = outputdata1;
            yq = outputdata2;
            PUvq = outputdata3;
        }

        public void meshplotfigure(float[,] PUxq, float[,] PUyq, float[,] PUvq, bool saveFlag, int ZMax, int Zmin)
        {
            // MWArray result;
            double[,] InputData1 = new double[PUxq.GetLength(0), PUxq.GetLength(0)];
            for (int i = 0; i < PUxq.GetLength(0); i++)
            {
                for (int j = 0; j < PUxq.GetLength(0); j++)
                {
                    InputData1[i, j] = (double)PUxq[i, j];
                }
            }
            double[,] InputData2 = new double[PUyq.GetLength(0), PUyq.GetLength(0)];
            for (int i = 0; i < PUyq.GetLength(0); i++)
            {
                for (int j = 0; j < PUyq.GetLength(0); j++)
                {
                    InputData2[i, j] = (double)PUyq[i, j];
                }
            }
            double[,] InputData3 = new double[PUvq.GetLength(0), PUvq.GetLength(0)];
            for (int i = 0; i < PUvq.GetLength(0); i++)
            {
                for (int j = 0; j < PUvq.GetLength(0); j++)
                {
                    InputData3[i, j] = (double)PUvq[i, j];
                }
            }
            // MWNumericArray argIn = (MWNumericArray)InputData;
            //float[] OutputData1 = new float[3];

            MWArray[] argIn = new MWArray[] { (MWNumericArray)InputData1, (MWNumericArray)InputData2, (MWNumericArray)InputData3, (MWLogicalArray)saveFlag, ZMax, Zmin };
            MWArray[] argOut = new MWArray[0];
            //pml.meshplotfigure(0, ref argOut, argIn);
            pml.meshplotfigure((MWNumericArray)InputData1, (MWNumericArray)InputData2, (MWNumericArray)InputData3, (MWLogicalArray)saveFlag, ZMax, Zmin);
        }

        public void PELICalculation(float[] ringGrooveValue, float OriGroove, out float PELI)
        {
            float Sum = 0;
            for (int i = 0; i < ringGrooveValue.Length; i++)
            {
                Sum += ringGrooveValue[i];
            }

            PELI = ((Sum / ringGrooveValue.Length) / OriGroove) * 100;
        }

        #endregion float類型 20200821

        #region double類型 先註解掉(保留)

        public double[] extractData(double[] rawdata) // 把資料為零的數據補點 "nearest" 用最近的值去補
        {
            MWArray result;

            double[] InputData = rawdata;
            MWNumericArray array1 = (MWNumericArray)InputData;
            result = pml.extractData(array1);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            double[] OutputData = new double[MatlabToCsharp.Length];
            double[] extraData;
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = MatlabToCsharp[0, i];
            }
            extraData = OutputData;
            return extraData;
        }

        public double[] removeZero(double[] data) //把數據為0的直接拿掉 資料出來會比進去少
        {
            MWArray result;

            double[] InputData = data;
            MWNumericArray array1 = (MWNumericArray)InputData;
            result = pml.removeZero(array1);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            double[] OutputData = new double[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = MatlabToCsharp[0, i];
            }
            return OutputData;
        }

        public double[] removeZero2(double[] data) //把數據為0的直接拿掉 資料出來會比進去少
        {
            MWArray result;

            double[] InputData = data;
            MWNumericArray array1 = (MWNumericArray)InputData;
            result = pml.removeZero2(array1);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            double[] OutputData = new double[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = MatlabToCsharp[0, i];
            }
            return OutputData;
        }

        public void removeZero3(double[] data, double[] data2, double[] data3, out double[] newdata1, out double[] newdata2, out double[] newdata3) //把數據為0的直接拿掉 資料出來會比進去少
        {
            MWArray[] result;

            double[] InputData = data;
            double[] InputData2 = data2;
            double[] InputData3 = data3;
            

            MWNumericArray array1 = (MWNumericArray)InputData;
            MWNumericArray array2 = (MWNumericArray)InputData2;
            MWNumericArray array3 = (MWNumericArray)InputData3;

            result = pml.removeZero3(3, array1, array2, array3);
            double[,] MatlabToCsharp = (double[,])result[0].ToArray(); // 格式轉換
            double[,] MatlabToCsharp2 = (double[,])result[1].ToArray(); // 格式轉換
            double[,] MatlabToCsharp3 = (double[,])result[2].ToArray(); // 格式轉換
            double[] OutputData = new double[MatlabToCsharp.Length];
            double[] OutputData2 = new double[MatlabToCsharp2.Length];
            double[] OutputData3 = new double[MatlabToCsharp3.Length];

            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = MatlabToCsharp[0, i];
            }

            for (int i = 0; i < MatlabToCsharp2.Length; i++)
            {
                OutputData2[i] = MatlabToCsharp2[0, i];
            }

            for (int i = 0; i < MatlabToCsharp3.Length; i++)
            {
                OutputData3[i] = MatlabToCsharp3[0, i];
            }

            newdata1 = OutputData;
            newdata2 = OutputData2;
            newdata3 = OutputData3;
        }

        /// <summary>
        /// ratioData : spk,sk,svk,smr1,smr2,smrSpk,smrSk,smrSvk,a,b
        /// ratioAreaData : a,b [2 x X]
        /// ZHeigh :
        /// </summary>
        /// <param name="data"></param>
        /// <param name="ratioData"></param>
        /// <param name="ratioAreaData"></param>
        /// <param name="ZHeigh"></param>
        public void bearingRatio(double[] filtereddata, out double[] ratioValue, out double[,] ratioAreaData, out double[] ZHeight)
        {
            //資料輸出接口完成 object

            // MWArray result;

            double[] InputData = filtereddata;

            // MWNumericArray argIn = (MWNumericArray)InputData;
            double[] OutputData1 = new double[10];

            MWArray[] argIn = new MWArray[] { (MWNumericArray)InputData };
            MWArray[] argOut = new MWArray[13];
            pml.bearingratio(2, ref argOut, argIn);
            object[,] MatlabToCsharp = (object[,])argOut[0].ToArray(); // 格式轉換

            double[,] spk = (double[,])MatlabToCsharp[0, 0];
            double[,] sk = (double[,])MatlabToCsharp[0, 1];
            double[,] svk = (double[,])MatlabToCsharp[0, 2];
            double[,] smr1 = (double[,])MatlabToCsharp[0, 3];
            double[,] smr2 = (double[,])MatlabToCsharp[0, 4];
            double[,] smrSpk = (double[,])MatlabToCsharp[0, 5];
            double[,] smrSk = (double[,])MatlabToCsharp[0, 6];
            double[,] smrSvk = (double[,])MatlabToCsharp[0, 7];
            double[,] b_r = (double[,])MatlabToCsharp[0, 8];
            double[,] z = (double[,])MatlabToCsharp[0, 9];
            double[,] a = (double[,])MatlabToCsharp[0, 10];
            double[,] b = (double[,])MatlabToCsharp[0, 11];
            double[,] ZHeightall = (double[,])MatlabToCsharp[0, 12];

            OutputData1[0] = spk[0, 0];
            OutputData1[1] = sk[0, 0];
            OutputData1[2] = svk[0, 0];
            OutputData1[3] = smr1[0, 0];
            OutputData1[4] = smr2[0, 0];
            OutputData1[5] = smrSpk[0, 0];
            OutputData1[6] = smrSk[0, 0];
            OutputData1[7] = smrSvk[0, 0];
            OutputData1[8] = a[0, 0];
            OutputData1[9] = b[0, 0];

            double[,] OutputData2 = new double[2, b_r.Length];
            double[] OutputData3 = new double[ZHeightall.Length];
            for (int i = 0; i < b_r.Length; i++)
            {
                OutputData2[0, i] = b_r[0, i];
                OutputData2[1, i] = z[0, i];
            }
            for (int i = 0; i < ZHeightall.Length; i++)
            {
                OutputData3[i] = ZHeightall[0, i];
            }
            ratioValue = OutputData1;
            ratioAreaData = OutputData2;
            ZHeight = OutputData3;
        }

        public double[] roughnessAnalysis(double[] halffiltereddata) //計算粗糙度
        {
            MWArray result;

            double[] InputData = halffiltereddata;
            MWNumericArray array1 = (MWNumericArray)InputData;
            result = pml.roughnessAnalysis(array1);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            double[] OutputData = new double[MatlabToCsharp.Length];
            double[] allRa;
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = MatlabToCsharp[0, i];
            }
            allRa = OutputData;
            return allRa; // [Rq Ra Rz Rp Rv Rsk Rku] 輸出7個參數
        }

        /// <summary>
        /// 輸出 [part , 7]的矩陣 [Rq Ra Rz Rp Rv Rsk Rku] 輸出7個參數
        /// </summary>
        /// <param name="data"></param>
        /// <param name="part"></param>
        /// <returns></returns>
        public double[,] ringRoughness(double[] filtereddata, int part) // 分區計算粗糙度
        {
            MWArray result;

            double[] InputData = filtereddata;
            MWNumericArray array1 = (MWNumericArray)InputData;
            result = pml.ringRoughness(array1, part);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            double[,] ringRa = MatlabToCsharp;
            return ringRa; // 輸出 [part , 7]的矩陣
                           //            [Rq
                           //             Ra
                           //             Rz
                           //             Rp
                           //             Rv
                           //             Rsk
                           //             Rku] 輸出7個參數
        }

        /// <summary>
        /// 輸出 [part]的矩陣 看分幾區就有幾個data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="part"></param>
        /// <returns></returns>
        public double[] ringGroove(double[] grooveDepthDatadata, int part) // 分區計算溝深
        {
            MWArray result;

            double[] InputData = grooveDepthDatadata;
            MWNumericArray array1 = (MWNumericArray)InputData;
            result = pml.ringGroove(array1, part);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            double[] OutputData = new double[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = MatlabToCsharp[0, i];
            }
            double[] ringGrooveValue = OutputData;
            return ringGrooveValue; // 輸出 [part]的矩陣 看分幾區就有幾個data
        }

        public double[] removeGroove(double[] extradata, int threashold) // 取出無溝槽資料 //threshold為要切的深度閥值 設越高即越靠近表面高度 0-100
        {
            MWArray result;

            double[] InputData = extradata;
            MWNumericArray array1 = (MWNumericArray)InputData;
            result = pml.removeGroove(array1, 70);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            double[] OutputData = new double[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = MatlabToCsharp[i, 0];
            }
            double[] noGrooveData = OutputData;
            return noGrooveData; //
        }

        /// <summary>
        /// Highpass 3 Hz左右, band stop 40 - 50 Hz 機台頻率, samplingrate 1000 Hz
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Highpass"></param>
        /// <param name="FH2"></param>
        /// <param name="FL2"></param>
        /// <param name="SamplingRate"></param>
        /// <returns></returns>
        public double[] iirFilter(double[] noGrooveData, int FH1, int FL2, int FH2, int SamplingRate)
        {
            MWArray result;
            // FH1 high pass 3 Hz左右
            // FL2 band stop 40 Hz 搖臂頻率
            // FH2 band stop 50 Hz
            int Samplingrate = SamplingRate;
            double[] InputData1 = new double[noGrooveData.Length];  //輸入轉double
            for (int i = 0; i < noGrooveData.Length; i++)
            {
                InputData1[i] = Convert.ToDouble(noGrooveData[i]);
            }

            MWNumericArray array1 = (MWNumericArray)InputData1;
            result = pml.iirFilter(array1, (MWArray)FH1, (MWArray)FH2, (MWArray)FL2, (MWArray)Samplingrate);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            double[] OutputData = new double[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = MatlabToCsharp[0, i];
            }
            double[] filteredData = OutputData;
            return filteredData; //
        }

        /// <summary>
        /// (校正後資料，pad原始厚度(um)，取樣頻率) 輸出為百分比
        /// </summary>
        /// <param name="data"></param>
        /// <param name="padThickness"></param>
        /// <param name="SamplingRate"></param>
        /// <returns></returns>
        public double puCaculation(double[] PUdata, double padThickness, int SamplingRate) //把數據為0的直接拿掉 資料出來會比進去少
        {
            MWArray result;

            double[] InputData = PUdata;
            MWNumericArray array1 = (MWNumericArray)InputData;
            result = pml.puCalculation(array1, padThickness, SamplingRate);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            double OutputData;
            OutputData = MatlabToCsharp[0, 0];

            double PUValue = Math.Round(OutputData, 2, MidpointRounding.AwayFromZero);
            return PUValue;
        }

        public void grooveCalculation(double[] DetrendGroovedata, int TopProfileRange, int GrooveRange, out double[] grooveDepthData, out double grooveAvgValue) //
        {
            double[] InputData = DetrendGroovedata;
            // MWNumericArray argIn = (MWNumericArray)InputData;
            double[,] MatlabToCsharp;

            MWArray[] argIn = new MWArray[] { (MWNumericArray)InputData, TopProfileRange, GrooveRange };
            MWArray[] argOut = new MWArray[2];
            pml.grooveCalculation(2, ref argOut, argIn);
            // grooveDepthData 矩陣資料
            MatlabToCsharp = (double[,])argOut[0].ToArray(); // 格式轉換
            double[] outputdata1 = new double[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                outputdata1[i] = MatlabToCsharp[0, i];
            }
            grooveDepthData = outputdata1;

            // grooveAvg 單一個值
            double outputdata2;
            MatlabToCsharp = (double[,])argOut[1].ToArray(); // 格式轉換
            outputdata2 = MatlabToCsharp[0, 0];
            grooveAvgValue = outputdata2;
        }

        public double[] removeSwingArmTrendline(double[] rawdata, int SamplingRate) // 取出無溝槽資料 //threshold為要切的深度閥值 設越高即越靠近表面高度 0-100
        {
            MWArray result;

            //double[] InputData1 = NoGroovedata;
            double[] InputData2 = rawdata;
            //MWNumericArray array1 = (MWNumericArray)InputData1;
            MWNumericArray array2 = (MWNumericArray)InputData2;
            result = pml.removeSwingArmTrendline(array2, SamplingRate);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            double[] OutputData = new double[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = MatlabToCsharp[0, i];
            }
            double[] detrendGrooveData = OutputData;
            return detrendGrooveData; //
        }

        /*0929前舊的
        public double[] removeSwingArmTrendline(double[] NoGroovedata, double[] rawdata, int SamplingRate) // 取出無溝槽資料 //threshold為要切的深度閥值 設越高即越靠近表面高度 0-100
        {
            MWArray result;

            double[] InputData1 = NoGroovedata;
            double[] InputData2 = rawdata;
            MWNumericArray array1 = (MWNumericArray)InputData1;
            MWNumericArray array2 = (MWNumericArray)InputData2;
            result = pml.removeSwingArmTrendline(array1, array2, SamplingRate);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            double[] OutputData = new double[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = MatlabToCsharp[0, i];
            }
            double[] detrendGrooveData = OutputData;
            return detrendGrooveData; //
        }
        */

        public void FFT(double[] data, int samplingrate, out double[,] f) // 20200819
        {
            //MWArray result;
            double[,] MatlabToCsharp1;
            double[,] MatlabToCsharp2;
            double[] InputData1 = data;
            int InputData2 = samplingrate;
            MWNumericArray array1 = (MWNumericArray)InputData1;
            MWNumericArray array2 = (MWNumericArray)InputData2;
            MWArray[] argIn = new MWArray[] { (MWNumericArray)array1, (MWNumericArray)array2 };
            MWArray[] argOut = new MWArray[2];
            pml.FFTfuntion(2, ref argOut, argIn);

            // frequency 矩陣資料
            MatlabToCsharp1 = (double[,])argOut[0].ToArray(); // 格式轉換
            double[,] outputdata1 = new double[2, MatlabToCsharp1.Length];
            for (int i = 0; i < MatlabToCsharp1.Length; i++)
            {
                outputdata1[0, i] = MatlabToCsharp1[0, i];
                outputdata1[0, i] = Math.Round(outputdata1[0, i], 3, MidpointRounding.AwayFromZero);
            }

            // amp 矩陣資料
            MatlabToCsharp2 = (double[,])argOut[1].ToArray(); // 格式轉換
            //float[] outputdata2 = new float[MatlabToCsharp2.Length];
            for (int i = 0; i < MatlabToCsharp2.Length; i++)
            {
                outputdata1[1, i] = MatlabToCsharp2[0, i];
                outputdata1[0, i] = Math.Round(outputdata1[0, i], 3, MidpointRounding.AwayFromZero);
            }

            f = outputdata1;
        }

        public double[] halffiltereddata(double[] filtereddata) // 20200817
        {
            double[] halffiltereddata = new double[filtereddata.Length / 2];
            double[] OutputData = new double[filtereddata.Length / 2];
            for (int i = 0; i < filtereddata.Length / 2; i++)
            {
                halffiltereddata[i] = filtereddata[i];
                OutputData = halffiltereddata;
            }
            return OutputData;
        }

        public double[] ceramicBaseline(double[] noGrooveData, double[] BaselineData, int samplingRate) // 20200818
        {
            //
            MWArray result;
            double[] InputData1 = new double[noGrooveData.Length];  //輸入轉double
            for (int i = 0; i < noGrooveData.Length; i++)
            {
                InputData1[i] = Convert.ToDouble(noGrooveData[i]);
            }
            double[] InputData2 = new double[BaselineData.Length];  // 輸入轉double
            for (int i = 0; i < BaselineData.Length; i++)
            {
                InputData2[i] = Convert.ToDouble(BaselineData[i]);
            }

            MWNumericArray array1 = (MWNumericArray)InputData1;
            MWNumericArray array2 = (MWNumericArray)InputData2;
            result = pml.ceramicBaseline(array1, samplingRate, array2);
            double[,] MatlabToCsharp = (double[,])result.ToArray(); // 格式轉換
            double[] OutputData = new double[MatlabToCsharp.Length];
            for (int i = 0; i < MatlabToCsharp.Length; i++)
            {
                OutputData[i] = MatlabToCsharp[0, i];
            }
            double[] PUData = OutputData;
            return PUData;
        }

        #endregion double類型 先註解掉(保留)
    }
}