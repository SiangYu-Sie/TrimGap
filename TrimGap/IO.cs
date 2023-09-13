using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TrimGap
{
    public class IOName
    {
        public enum In
        {
            門檢 = 0,
            緊急停止訊號 = 1,
            空壓檢 = 2,
            START_預留 = 3,
            STOP_預留 = 4,
            RESET_預留 = 5,
            真空平台_負壓檢 = 6,
            Wafer汽缸_抬起檢 = 7,
            Wafer汽缸_降下檢 = 8,
            StageWafer在席 = 9,
            Wafer取放原點檢 = 10,
            差壓檢 = 11,
            靜電消除器警報 = 12,
            TotalInCount = 13
        }

        public enum Out
        {
            三色燈_紅燈_預留 = 0,
            三色燈_黃燈_預留 = 1,
            三色燈_綠燈_預留 = 2,
            三色燈_蜂鳴器_預留 = 3,
            START_預留 = 4,
            STOP_預留 = 5,
            RESET_預留 = 6,
            門鎖電磁閥 = 7,
            Wafer汽缸_抬起 = 8,
            Wafer汽缸_降下 = 9,
            平台真空電磁閥 = 10,
            平台破真空電磁閥 = 11,
            門鎖電磁閥偵測開啟 = 12,
            StageWaferReady = 13,
            StageWafer在席 = 14,
            門檢Bypass = 15,
            TotalOutCount = 16
        }

        public enum Analog_In
        {
        }

        public enum Analog_Out
        {
        }
    }

    public class Io
    {
        public int[] inmap = new int[(int)IOName.In.TotalInCount];
        public int[] outmap = new int[(int)IOName.Out.TotalOutCount];
        private int MachineType = 0;
        private int IoType = 0;     // -1: 模擬用  0:研華 
        public static IO4.BDaq ioBDaq;
        //private bool[] virin = new bool[32];
        //private bool[] virout = new bool[32];
        public Io(int machine_type, string io_card_name, int io_type = 0)
        {
            MachineType = machine_type;
            IoType = io_type;
            setIoMap(MachineType);
            if (IoType == 0)
            {
                ioBDaq = new IO4.BDaq(io_card_name);
            }
            else if(IoType == -1)
            {
                ioBDaq = new IO4.BDaq("-1");
            }
        }

        public bool initial(string parapath)
        {
            if (IoType == 0)
            {
                return ioBDaq.initial(parapath);
            }
            else if (IoType == -1)
            {
                return ioBDaq.initial(parapath);
            }
            else
                return false;
        }

        public bool bDebug()
        {
            if (IoType == 0)
            {
                return ioBDaq.bDebug;
            }
            else
                return true;
        }

        public bool In(IOName.In name)
        {
            if (IoType == 0)
            {
                if (inmap[(int)name] == -1)  //未連結實體IO
                    return false;
                else
                    return ioBDaq.In[inmap[(int)name]];
            }
            else
                return false;
        }

        public bool Out(IOName.Out name)
        {
            if (IoType == 0)
            {
                if (outmap[(int)name] == -1)  //未連結實體IO
                    return false;
                else
                    return ioBDaq.Out[outmap[(int)name]];
            }
            else
                return false;
        }

        public void WriteOut(IOName.Out name, bool on)
        {
            if (IoType == 0)
            {
                if (outmap[(int)name] != -1)  //有連結實體IO
                    ioBDaq.WriteOut(outmap[(int)name], on);
            }
        }

        public void WriteIn(IOName.In name, bool on)
        {
            if (IoType == -1)
            {
                //if (inmap[(int)name] != -1)  //有連結實體IO
                   // virin[outmap[(int)name]] = on;
            }
        }

        public void setIoMap(int machinetype)
        {
            for (int i = 0; i < (int)IOName.In.TotalInCount; i++)
                inmap[i] = -1;
            for (int i = 0; i < (int)IOName.Out.TotalOutCount; i++)
                outmap[i] = -1;
            if (machinetype == 0) //AP6
            {
                //IN
                inmap[(int)IOName.In.門檢] = 0;  //門檢
                inmap[(int)IOName.In.緊急停止訊號] = 1;  //緊急停止訊號
                inmap[(int)IOName.In.空壓檢] = 2;  //空壓檢
                inmap[(int)IOName.In.START_預留] = 3;  //START_預留
                inmap[(int)IOName.In.STOP_預留] = 4;  //STOP_預留
                inmap[(int)IOName.In.RESET_預留] = 5;  //RESET_預留
                inmap[(int)IOName.In.真空平台_負壓檢] = 6;  //真空平台_負壓檢
                inmap[(int)IOName.In.Wafer汽缸_抬起檢] = 7;  //Wafer汽缸_抬起檢
                inmap[(int)IOName.In.Wafer汽缸_降下檢] = 8;  //Wafer汽缸_降下檢
                inmap[(int)IOName.In.StageWafer在席] = 9;  //StageWafer在席

                //OUT
                outmap[(int)IOName.Out.三色燈_紅燈_預留] = 0;
                outmap[(int)IOName.Out.三色燈_黃燈_預留] = 1;
                outmap[(int)IOName.Out.三色燈_綠燈_預留] = 2;
                outmap[(int)IOName.Out.三色燈_蜂鳴器_預留] = 3;
                outmap[(int)IOName.Out.START_預留] = 4;
                outmap[(int)IOName.Out.STOP_預留] = 5;
                outmap[(int)IOName.Out.RESET_預留] = 6;
                outmap[(int)IOName.Out.門鎖電磁閥] = 7;
                outmap[(int)IOName.Out.Wafer汽缸_抬起] = 8;
                outmap[(int)IOName.Out.Wafer汽缸_降下] = 9;
                outmap[(int)IOName.Out.平台真空電磁閥] = 10;
                outmap[(int)IOName.Out.平台破真空電磁閥] = 11;
                outmap[(int)IOName.Out.門鎖電磁閥偵測開啟] = 12;
                outmap[(int)IOName.Out.StageWaferReady] = 13;
                outmap[(int)IOName.Out.StageWafer在席] = 14;
            }
            else if (machinetype == 1) //N2
            {
                //IN
                inmap[(int)IOName.In.門檢] = 0;
                inmap[(int)IOName.In.緊急停止訊號] = 1;
                inmap[(int)IOName.In.空壓檢] = 2;
                inmap[(int)IOName.In.START_預留] = 3;
                inmap[(int)IOName.In.STOP_預留] = 4;
                inmap[(int)IOName.In.RESET_預留] = 5;
                inmap[(int)IOName.In.真空平台_負壓檢] = 6;
                inmap[(int)IOName.In.Wafer取放原點檢] = 7;
                inmap[(int)IOName.In.差壓檢] = 8;
                inmap[(int)IOName.In.StageWafer在席] = 9;
                inmap[(int)IOName.In.靜電消除器警報] = 10;

                //OUT
                outmap[(int)IOName.Out.三色燈_紅燈_預留] = 0;
                outmap[(int)IOName.Out.三色燈_黃燈_預留] = 1;
                outmap[(int)IOName.Out.三色燈_綠燈_預留] = 2;
                outmap[(int)IOName.Out.三色燈_蜂鳴器_預留] = 3;
                outmap[(int)IOName.Out.START_預留] = 4;
                outmap[(int)IOName.Out.STOP_預留] = 5;
                outmap[(int)IOName.Out.RESET_預留] = 6;
                outmap[(int)IOName.Out.門鎖電磁閥] = 7;
                outmap[(int)IOName.Out.平台真空電磁閥] = 10;
                outmap[(int)IOName.Out.平台破真空電磁閥] = 11;
                outmap[(int)IOName.Out.門檢Bypass] = 12;
                outmap[(int)IOName.Out.StageWaferReady] = 13;
                outmap[(int)IOName.Out.StageWafer在席] = 14;
            }
        }

        public void ShowUI()
        {
            if (IoType == 0 || IoType == -1)
            {
                ioBDaq.ShowUI();
            }
        }
    }
}