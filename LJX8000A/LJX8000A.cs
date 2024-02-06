using LJXA_ImageAcquisitionSample;
using LJXA_ImageAcquisitionSample.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace LJX8000A
{
    public class LJX8000A
    {
        private string ipAddress { get; set; }
        private ushort portNo { get; set; }
        private int deviceId { get; set; }
        private int yImageSize;
        private List<ushort> HeightImage = new List<ushort>();
        private List<ushort> LuminanceImage = new List<ushort>();
        public LJX8000A(string ipAddress, ushort portNo = 24691, int deviceId = 0)
        {
            this.deviceId = deviceId;
            this.ipAddress = ipAddress;
            this.portNo = portNo;
        }
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Generate ethernet config for LJX8IF API.
        /// </summary>
        /// <returns>Ethernet config</returns>
        private LJX8IF_ETHERNET_CONFIG GetEthernetConfig()
        {
            return new LJX8IF_ETHERNET_CONFIG
            {
                abyIpAddress = IPAddress.Parse(ipAddress).GetAddressBytes(),
                wPortNo = portNo
            };
        }

        public void Connect()
        {
            var config = GetEthernetConfig();
            var rc = (Rc)KeyenceLJXAAcq.OpenDevice(deviceId, config, 24692);
            if (rc != Rc.Ok) throw new Exception("LJX80000A Connect Failed!");
            IsConnected = true;
        }
        public void Disconnect()
        {
            NativeMethods.LJX8IF_CommunicationClose(deviceId);
            IsConnected = false;
        }
        public void Trigger()
        {
            var rc = (Rc)KeyenceLJXAAcq.Trigger(deviceId);
        }

        public void ClearMemory()
        {
            var rc = (Rc)KeyenceLJXAAcq.ClearMemory(deviceId);
        }

        public double[] GetProfile()
        {
            return KeyenceLJXAAcq.GetProfile(deviceId);
        }
        public void ScanAsync(int distance)
        {
            int deviceId = 0;                   // Set "0" if you use only 1 head.
            yImageSize = distance / 4;              // Number of Y lines.
            float yPitchUm = 20.0f;             // Data pitch of Y data. (e.g. your encoder setting)
            int timeoutMs = 60000;               // Timeout value for the acquiring image (in millisecond).
            int useExternalBatchStart = 0;      // Set "1" if you controll the batch start timing externally. (e.g. terminal input)

            SetParam setParam = new SetParam
            {
                YLineNum = yImageSize,
                YPitchUm = yPitchUm,
                TimeoutMs = timeoutMs,
                UseExternalBatchStart = useExternalBatchStart,
            };

            int errCode = KeyenceLJXAAcq.StartAsync(deviceId, setParam);
            if (errCode != (int)Rc.Ok)
            {
                throw new Exception("LJX8000A Scan Fail");
            }
        }
        public void StopScan()
        {
            int errCode = NativeMethods.LJX8IF_StopMeasure(deviceId);
            Console.WriteLine(@"[@(StartAsync) Measure Start(Batch Start)](0x{0:x})", errCode);
        }
        public List<double[,]> GetData(bool isSavingCSV = false)
        {
            float yPitchUm = 20.0f;             // Data pitch of Y data. (e.g. your encoder setting)
            int timeoutMs = 60000;               // Timeout value for the acquiring image (in millisecond).
            int useExternalBatchStart = 0;      // Set "1" if you controll the batch start timing externally. (e.g. terminal input)

            GetParam getParam = new GetParam();
            SetParam setParam = new SetParam
            {
                YLineNum = yImageSize,
                YPitchUm = yPitchUm,
                TimeoutMs = timeoutMs,
                UseExternalBatchStart = useExternalBatchStart,
            };
            HeightImage.Clear();
            LuminanceImage.Clear();

            int errCode = KeyenceLJXAAcq.AcquireAsync(deviceId, HeightImage, LuminanceImage, setParam, ref getParam);

            if (errCode != (int)Rc.Ok)
            {
                throw new Exception("LJX8000A Scan TimeOut");
            }

            
            List<double[,]> rtnValue = new List<double[,]>();
            try
            {
                var heightRst = TranListDimension(HeightImage, 3200, getParam.ZPitchUm);
                if(isSavingCSV)
                    CsvConverter.Save1D("Test_Height.csv", HeightImage, yImageSize, 3200 * setParam.YLineNum, getParam.ZPitchUm);
                rtnValue.Add(heightRst);

                if (getParam.LuminanceEnabled == 1)
                {
                    var luminanceRst = TranListDimension(LuminanceImage, 3200, getParam.ZPitchUm);
                    if(isSavingCSV)
                        CsvConverter.Save1D("Test_LuminanceImage1D.csv", LuminanceImage, yImageSize, 3200 * setParam.YLineNum, getParam.ZPitchUm);
                    rtnValue.Add(luminanceRst);
                }
            }
            catch
            {
                throw new Exception("LJX8000A Failed to save file");
            }
            return rtnValue;
        }

        private const int CORRECT_VALUE = 32768;
        private const double INVALID_VALUE = -999.9999;
        private double[,] TranListDimension(List<ushort> source, int width, float zPitchUm)
        {
            if (source.Count % width != 0)
                return null;

            int height = source.Count / width;
            double[,] rst = new double[width, height];
            for (int i = 0; i < source.Count; i++)
            {
                double value = source[i] == 0 ? INVALID_VALUE : (source[i] - CORRECT_VALUE) * zPitchUm / 1000;
                rst[i % width, i / width] = Math.Round(value,4);
            }
            return rst;
        }
    }
}
