/*
* ==============================================================================
*
* Filename: $$
* Description:
*
* Version: 1.0
* Created:
* Compiler: Visual Studio 2017
*
* Author: CC
* Company: TL Tech
*
* ==============================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;

//using System.Windows.Forms;

namespace TrimGap
{
    internal class ParamFile : WrINI
    {
        public static void initparam()
        {
            //sram.baseinifilepath = System.Windows.Forms.Application.StartupPath;  // 改版前的路徑
            //string filePath = sram.baseinifilepath + @"\param\param.ini";         // 改版前的路徑
            string filePath = sram.ParamPath + "param.ini";
            sram.saveinifilepath = sram.ParamPath + "param.ini";
            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(sram.ParamPath);
            }
            iniAllVal("r", filePath, "all");
        }

        public static void saveparam(string KeyName)
        {
            string filePath = sram.saveinifilepath;
            iniAllVal("w", filePath, KeyName);
        }

        public static void saveUsData(string KeyName, string strUser)
        {
            string filePath = sram.saveinifilepath;
            string usedata = fram.Up_UserData;
            iniVal("w", filePath, KeyName, ref fram.Up_UserData, "fram."+ strUser, usedata);
            //iniVal(m, p, s, ref fram.Up_UserData, "fram.op_password", "111");
        }

        public static void wr(string m, string KeyName, string p)
        {
            //   string p;
            //    p = newfilePath + filename;
            total = 0;
            try
            {
                iniAllVal(m, p, KeyName);
            }
            catch
            {
                MessageBox.Show("Set File Error!" + "(" + total + ")\t", "ParamFile", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static void wr(string m, string KeyName, ref int v, string k)    //int
        {
            string p;
            string s;
            // p = AppDomain.CurrentDomain.BaseDirectory + filename;
            //   p = newfilePath + filename;
            p = "";
            s = KeyName;
            total = 0;
            try
            {
                iniVal(m, p, s, ref v, k, 0);
            }
            catch
            {
                MessageBox.Show("Set File Error!" + "(" + total + ")\t", "fileFRAM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static void wr(string m, string KeyName, ref char v, string k)    //char
        {
            string p;
            string s;
            //p = AppDomain.CurrentDomain.BaseDirectory + filename;
            //     p = newfilePath + filename;
            p = "";
            s = KeyName;
            total = 0;
            try
            {
                iniVal(m, p, s, ref v, k, '1');
            }
            catch
            {
                MessageBox.Show("Set File Error!" + "(" + total + ")\t", "fileFRAM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static void wr(string m, string KeyName, ref long v, string k)    //long
        {
            string p;
            string s;
            //p = AppDomain.CurrentDomain.BaseDirectory + filename;
            ///   p = newfilePath + filename;
            p = "";
            s = KeyName;
            total = 0;
            try
            {
                iniVal(m, p, s, ref v, k, 1L);
            }
            catch
            {
                MessageBox.Show("Set File Error!" + "(" + total + ")\t", "fileFRAM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static void wr(string m, string KeyName, ref double v, string k)    //double
        {
            string p;
            string s;
            //p = AppDomain.CurrentDomain.BaseDirectory + filename;
            //     p = newfilePath + filename;
            p = @"D:\HAMAI\CMP_Measurement\CMP_Measurement\para\param.ini";
            s = KeyName;
            total = 0;
            try
            {
                iniVal(m, p, s, ref v, k, 1.0);
            }
            catch
            {
                MessageBox.Show("Set File Error!" + "(" + total + ")\t", "fileFRAM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// 對參數的讀取和存儲功能
        /// </summary>
        /// <param name="m"></param>
        /// <param name="p"></param>
        /// <param name="KeyName"></param>
        private static void iniAllVal(string m, string p, string KeyName)
        {
            string s;

            if (KeyName == "all" || KeyName == "System")
            {
                s = "System"; 
                iniVal(m, p, s, ref fram.S_SECSGEM_Use, "fram.S_SECSGEM_Use", 0);
                iniVal(m, p, s, ref fram.m_simulateRun, "fram.m_simulateRun", 0);
                iniVal(m, p, s, ref fram.S_IOCardName, "fram.S_IOCardName", "PCI-1711");
                iniVal(m, p, s, ref fram.m_WaferAlignAngle, "fram.m_WaferAlignAngle", 0);
                iniVal(m, p, s, ref fram.m_WaferBackToFoupAngle, "fram.m_WaferBackToFoupAngle", 0);
                //iniVal(m, p, s, ref fram.S_SensorType, "fram.S_SensorType", 1);
                iniVal(m, p, s, ref fram.S_SensorConnectType, "fram.S_SensorConnectType", 0);
                //iniVal(m, p, s, ref fram.S_SensorProgram, "fram.S_SensorProgram", 0);
                //iniVal(m, p, s, ref fram.S_Triggerbool, "fram.S_Triggerbool", 1);
                iniVal(m, p, s, ref fram.m_SecsgemType, "fram.m_SecsgemType", 0);
                iniVal(m, p, s, ref fram.m_EFEMbypass, "fram.m_EFEMbypass", 0);
                iniVal(m, p, s, ref fram.m_DEMOMode, "fram.m_DEMOMode", 0);
                iniVal(m, p, s, ref fram.m_NG_RecordCCD, "fram.m_NG_RecordCCD", 0); 
            }

            if (KeyName == "all" || KeyName == "System")
            {
                s = "System";
                iniVal(m, p, s, ref fram.S_ShowDataNum, "fram.S_ShowDataNum", 3000);
                iniVal(m, p, s, ref fram.S_MotionRotate, "fram.S_MotionRotate", "False");
                iniVal(m, p, s, ref fram.S_Sensorbypass, "fram.S_Sensorbypass", "False");
                //iniVal(m, p, s, ref fram.S_StartMeasureWaitTime, "fram.S_StartMeasureWaitTime", 3000);
                //iniVal(m, p, s, ref fram.S_EndMeasureWaitTime, "fram.S_EndMeasureWaitTime", 1000);
            }

            if (KeyName == "all" || KeyName == "UserPermission")
            {
                s = "UserPermission";
                List<string> listkey = new List<string>();

                listkey = ReadKeys(s, p);
                fram.UserData = ReadKeyValue(s, listkey, p); 

                //iniVal(m, p, s, ref fram.Up_UserData, "fram.op_password", "111");
                 
                //iniVal(m, p, s, ref fram.op_password, "fram.op_password", "111");
                //iniVal(m, p, s, ref fram.eng_password, "fram.eng_password", "222");
                //iniVal(m, p, s, ref fram.ad_password, "fram.ad_password", "333");
            }

            if (KeyName == "all" || KeyName == "SensorLJ")
            {
                s = "SensorLJ";
                //iniVal(m, p, s, ref fram.ipaddress_FTP, "fram.ipaddress_FTP", "192/168/10/30");
                //iniVal(m, p, s, ref fram.ipaddress_Controller, "fram.ipaddress_Controller", "192/168/10/10");
                //string[] FTP = Regex.Split(fram.ipaddress_FTP, "/", RegexOptions.Singleline);
                //string[] Controller = Regex.Split(fram.ipaddress_FTP, "/", RegexOptions.Singleline);
                //for (int i = 0; i < 4; i++)
                //{
                //    fram.ipAddress_FTP[i] = Convert.ToByte(FTP[i]);
                //    fram.ipAddress_FTP[i] = Convert.ToByte(Controller[i]);
                //}

                iniVal(m, p, s, ref fram.LJ_ipaddress_Controller, "fram.LJ_ipaddress_Controller", "192.168.10.10");
                iniVal(m, p, s, ref fram.LJ_portNo_Controller, "fram.LJ_portNo_Controller", 8500);
                iniVal(m, p, s, ref fram.LJ_ipaddress_FTP, "fram.LJ_ipaddress_FTP", "192.168.10.30");
                iniVal(m, p, s, ref fram.LJ_User_FTP, "fram.LJ_User_FTP", "LJ");
                iniVal(m, p, s, ref fram.LJ_Password_FTP, "fram.LJ_Password_FTP", "8020");
                iniVal(m, p, s, ref fram.LJ_FTPfilePath, "fram.LJ_FTPfilePath", "lj-x2d/profile/SD1_001/");
            }

            if (KeyName == "all" || KeyName == "PLC")
            {
                s = "PLC";
                iniVal(m, p, s, ref fram.PLC.ActCpuType, "fram.PLC.ActCpuType", 0);
                iniVal(m, p, s, ref fram.PLC.ActUnitType, "fram.PLC.ActUnitType", 0);
                iniVal(m, p, s, ref fram.PLC.ActProtocolType, "fram.PLC.ActProtocolType", 0);
                iniVal(m, p, s, ref fram.PLC.ActStationNumber, "fram.PLC.ActStationNumber", 0);
                iniVal(m, p, s, ref fram.PLC.ActIONumber, "fram.PLC.ActIONumber", 0);
                iniVal(m, p, s, ref fram.PLC.ActTimeOut, "fram.PLC.ActTimeOut", 0);
                iniVal(m, p, s, ref fram.PLC.ActHostAddress, "fram.PLC.ActHostAddress", "");
                iniVal(m, p, s, ref fram.PLC.ActDestinationPortNumber, "fram.PLC.ActDestinationPortNumber", 0);
            }

            if (KeyName == "all" || KeyName == "Analysis")
            {
                s = "Analysis";
                iniVal(m, p, s, ref fram.Analysis.Coefficient, "fram.Analysis.Coefficient", 0.931);
                iniVal(m, p, s, ref fram.Analysis.BlueTapeThreshold, "fram.Analysis.BlueTapeThreshold", 69);
                iniVal(m, p, s, ref fram.Analysis.PitchAngle, "fram.Analysis.PitchAngle", 45);
                iniVal(m, p, s, ref fram.Analysis.RotateCount, "fram.Analysis.RotateCount", 8);
                iniVal(m, p, s, ref fram.Analysis.Step1_Range_step1x0, "fram.Analysis.Step1_Range_step1x0", 1800);
                iniVal(m, p, s, ref fram.Analysis.Step1_Range_step1x1, "fram.Analysis.Step1_Range_step1x1", 2000);
                iniVal(m, p, s, ref fram.Analysis.Step2_Range_step1x0, "fram.Analysis.Step2_Range_step1x0", 2300);
                iniVal(m, p, s, ref fram.Analysis.Step2_Range_step1x1, "fram.Analysis.Step2_Range_step1x1", 2900);
                iniVal(m, p, s, ref fram.Analysis.Step2_Range_step2x0, "fram.Analysis.Step2_Range_step2x0", 1800);
                iniVal(m, p, s, ref fram.Analysis.Step2_Range_step2x1, "fram.Analysis.Step2_Range_step2x1", 2000);
                iniVal(m, p, s, ref fram.Analysis.Range1_Percent, "fram.Analysis.Range1_Percent", 5);
                iniVal(m, p, s, ref fram.Analysis.Range2_Percent, "fram.Analysis.Range2_Percent", 15);
                iniVal(m, p, s, ref fram.Analysis.LJ_StandardPlane, "fram.Analysis.LJ_StandardPlane", -99999.9999);
                iniVal(m, p, s, ref fram.Analysis.Offset1StepH, "fram.Analysis.Offset1StepH", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset1StepW, "fram.Analysis.Offset1StepW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset2StepH1, "fram.Analysis.Offset2StepH1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset2StepW1, "fram.Analysis.Offset2StepW1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset2StepH2, "fram.Analysis.Offset2StepH2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset2StepW2, "fram.Analysis.Offset2StepW2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset_PT_1StepH, "fram.Analysis.Offset_PT_1StepH", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset_PT_1StepW, "fram.Analysis.Offset_PT_1StepW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset_PT_2StepH1, "fram.Analysis.Offset_PT_2StepH1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset_PT_2StepW1, "fram.Analysis.Offset_PT_2StepW1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset_PT_2StepH2, "fram.Analysis.Offset_PT_2StepH2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset_PT_2StepW2, "fram.Analysis.Offset_PT_2StepW2", 0);
                iniVal(m, p, s, ref fram.Analysis.OffsetBlueTapeW, "fram.Analysis.OffsetBlueTapeW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset_EDGE_1StepW, "fram.Analysis.Offset_EDGE_1StepW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset_EDGE_2StepW1, "fram.Analysis.Offset_EDGE_2StepW1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Inline_1StepH, "fram.Analysis.Offset.Inline_1StepH", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Inline_1StepW, "fram.Analysis.Offset.Inline_1StepW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Inline_2StepH1, "fram.Analysis.Offset.Inline_2StepH1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Inline_2StepW1, "fram.Analysis.Offset.Inline_2StepW1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Inline_2StepH2, "fram.Analysis.Offset.Inline_2StepH2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Inline_2StepW2, "fram.Analysis.Offset.Inline_2StepW2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Inline_PT_1StepH, "fram.Analysis.Offset.PT_Inline_1StepH", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Inline_PT_1StepW, "fram.Analysis.Offset.PT_Inline_1StepW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Inline_PT_2StepH1, "fram.Analysis.Offset.PT_Inline_2StepH1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Inline_PT_2StepW1, "fram.Analysis.Offset.PT_Inline_2StepW1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Inline_PT_2StepH2, "fram.Analysis.Offset.PT_Inline_2StepH2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Inline_PT_2StepW2, "fram.Analysis.Offset.PT_Inline_2StepW2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Inline_BlueTapeW, "fram.Analysis.Offset.Inline_BlueTapeW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Inline_EDGE_1StepW, "fram.Analysis.Offset.Inline_EDGE_1StepW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Inline_EDGE_2StepW1, "fram.Analysis.Offset.Inline_EDGE_2StepW1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Offline_1StepH, "fram.Analysis.Offset.Offline_1StepH", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Offline_1StepW, "fram.Analysis.Offset.Offline_1StepW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Offline_2StepH1, "fram.Analysis.Offset.Offline_2StepH1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Offline_2StepW1, "fram.Analysis.Offset.Offline_2StepW1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Offline_2StepH2, "fram.Analysis.Offset.Offline_2StepH2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Offline_2StepW2, "fram.Analysis.Offset.Offline_2StepW2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Offline_PT_1StepH, "fram.Analysis.Offset.PT_Offline_1StepH", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Offline_PT_1StepW, "fram.Analysis.Offset.PT_Offline_1StepW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Offline_PT_2StepH1, "fram.Analysis.Offset.PT_Offline_2StepH1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Offline_PT_2StepW1, "fram.Analysis.Offset.PT_Offline_2StepW1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Offline_PT_2StepH2, "fram.Analysis.Offset.PT_Offline_2StepH2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Offline_PT_2StepW2, "fram.Analysis.Offset.PT_Offline_2StepW2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Offline_BlueTapeW, "fram.Analysis.Offset.Offline_BlueTapeW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Offline_EDGE_1StepW, "fram.Analysis.Offset.Offline_EDGE_1StepW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Offline_EDGE_2StepW1, "fram.Analysis.Offset.Offline_EDGE_2StepW1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Trim0_1StepH, "fram.Analysis.Offset.Trim0_1StepH", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Trim0_1StepW, "fram.Analysis.Offset.Trim0_1StepW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Trim0_2StepH1, "fram.Analysis.Offset.Trim0_2StepH1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Trim0_2StepW1, "fram.Analysis.Offset.Trim0_2StepW1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Trim0_2StepH2, "fram.Analysis.Offset.Trim0_2StepH2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Trim0_2StepW2, "fram.Analysis.Offset.Trim0_2StepW2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Trim0_PT_1StepH, "fram.Analysis.Offset.PT_Trim0_1StepH", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Trim0_PT_1StepW, "fram.Analysis.Offset.PT_Trim0_1StepW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Trim0_PT_2StepH1, "fram.Analysis.Offset.PT_Trim0_2StepH1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Trim0_PT_2StepW1, "fram.Analysis.Offset.PT_Trim0_2StepW1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Trim0_PT_2StepH2, "fram.Analysis.Offset.PT_Trim0_2StepH2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Trim0_PT_2StepW2, "fram.Analysis.Offset.PT_Trim0_2StepW2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Trim0_BlueTapeW, "fram.Analysis.Offset.Trim0_BlueTapeW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Trim0_EDGE_1StepW, "fram.Analysis.Offset.Trim0_EDGE_1StepW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.Trim0_EDGE_2StepW1, "fram.Analysis.Offset.Trim0_EDGE_2StepW1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.F2F_1StepH, "fram.Analysis.Offset.F2F_1StepH", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.F2F_1StepW, "fram.Analysis.Offset.F2F_1StepW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.F2F_2StepH1, "fram.Analysis.Offset.F2F_2StepH1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.F2F_2StepW1, "fram.Analysis.Offset.F2F_2StepW1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.F2F_2StepH2, "fram.Analysis.Offset.F2F_2StepH2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.F2F_2StepW2, "fram.Analysis.Offset.F2F_2StepW2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.F2F_PT_1StepH, "fram.Analysis.Offset.PT_F2F_1StepH", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.F2F_PT_1StepW, "fram.Analysis.Offset.PT_F2F_1StepW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.F2F_PT_2StepH1, "fram.Analysis.Offset.PT_F2F_2StepH1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.F2F_PT_2StepW1, "fram.Analysis.Offset.PT_F2F_2StepW1", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.F2F_PT_2StepH2, "fram.Analysis.Offset.PT_F2F_2StepH2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.F2F_PT_2StepW2, "fram.Analysis.Offset.PT_F2F_2StepW2", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.F2F_BlueTapeW, "fram.Analysis.Offset.F2F_BlueTapeW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.F2F_EDGE_1StepW, "fram.Analysis.Offset.F2F_EDGE_1StepW", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.F2F_EDGE_2StepW1, "fram.Analysis.Offset.F2F_EDGE_2StepW1", 0);
                for (int i = 0; i < 8; i++)
                {
                    iniVal(m, p, s, ref fram.Analysis.Offset.QC_1StepH[i], "fram.Analysis.Offset.QC_1StepH" + i, 0);
                    iniVal(m, p, s, ref fram.Analysis.Offset.QC_1StepW[i], "fram.Analysis.Offset.QC_1StepW" + i, 0);
                    iniVal(m, p, s, ref fram.Analysis.Offset.QC_2StepH1[i], "fram.Analysis.Offset.QC_2StepH1" + i, 0);
                    iniVal(m, p, s, ref fram.Analysis.Offset.QC_2StepW1[i], "fram.Analysis.Offset.QC_2StepW1" + i, 0);
                    iniVal(m, p, s, ref fram.Analysis.Offset.QC_2StepH2[i], "fram.Analysis.Offset.QC_2StepH2" + i, 0);
                    iniVal(m, p, s, ref fram.Analysis.Offset.QC_2StepW2[i], "fram.Analysis.Offset.QC_2StepW2" + i, 0);
                    iniVal(m, p, s, ref fram.Analysis.Offset.QC_PT_1StepH[i], "fram.Analysis.Offset.PT_QC_1StepH" + i, 0);
                    iniVal(m, p, s, ref fram.Analysis.Offset.QC_PT_1StepW[i], "fram.Analysis.Offset.PT_QC_1StepW" + i, 0);
                    iniVal(m, p, s, ref fram.Analysis.Offset.QC_PT_2StepH1[i], "fram.Analysis.Offset.PT_QC_2StepH1" + i, 0);
                    iniVal(m, p, s, ref fram.Analysis.Offset.QC_PT_2StepW1[i], "fram.Analysis.Offset.PT_QC_2StepW1" + i, 0);
                    iniVal(m, p, s, ref fram.Analysis.Offset.QC_PT_2StepH2[i], "fram.Analysis.Offset.PT_QC_2StepH2" + i, 0);
                    iniVal(m, p, s, ref fram.Analysis.Offset.QC_PT_2StepW2[i], "fram.Analysis.Offset.PT_QC_2StepW2" + i, 0);
                    iniVal(m, p, s, ref fram.Analysis.Offset.QC_BlueTapeW[i], "fram.Analysis.Offset.QC_BlueTapeW" + i, 0);
                    iniVal(m, p, s, ref fram.Analysis.Offset.QC_EDGE_1StepW[i], "fram.Analysis.Offset.EDGE_QC_1StepW" + i, 0);
                    iniVal(m, p, s, ref fram.Analysis.Offset.QC_EDGE_2StepW1[i], "fram.Analysis.Offset.EDGE_QC_2StepW1" + i, 0);
                }

                iniVal(m, p, s, ref fram.Analysis.HTW_StandardPlane, "fram.Analysis.HTW_StandardPlane", 10.00);
				iniVal(m, p, s, ref fram.Analysis.HTW_W2EdgeThreshold, "fram.Analysis.HTW_W2EdgeThreshold", 5.00);
                iniVal(m, p, s, ref fram.Analysis.HTW_H0FromTilt, "fram.Analysis.HTW_H0FromTilt", 1);
                iniVal(m, p, s, ref fram.Analysis.HTW_HistogramRange, "fram.Analysis.HTW_HistogramRange", 0.5); 
                iniVal(m, p, s, ref fram.Analysis.HTW_TrimSearchMaxDifference, "fram.Analysis.HTW_TrimSearchMaxDifference", 4);
                iniVal(m, p, s, ref fram.Analysis.HTW_GroupPoints, "fram.Analysis.HTW_GroupPoints", 3);
                iniVal(m, p, s, ref fram.Analysis.HTW_TrimToIntensityShift, "fram.Analysis.HTW_TrimToIntensityShift", 15);

                iniVal(m, p, s, ref fram.Analysis.Use_Leveling, "fram.Analysis.Use_Leveling", 1.00);
                iniVal(m, p, s, ref fram.Analysis.nZone, "fram.Analysis.nZone", 50.00);
                iniVal(m, p, s, ref fram.Analysis.PT_TrimSearchMaxDifference, "fram.Analysis.PT_TrimSearchMaxDifference", 30);
                iniVal(m, p, s, ref fram.Analysis.Use_Intensity, "fram.Analysis.Use_Intensity", 1.00); 
                iniVal(m, p, s, ref fram.Analysis.W2_LJ_Replace_HTW, "fram.Analysis.W2_LJ_Replace_HTW", 0.00);

                //20250102
                iniVal(m, p, s, ref fram.Analysis.LJ_Measure_Count, "fram.Analysis.LJ_Measure_Count", 0.00);
                iniVal(m, p, s, ref fram.Analysis.HTW_Measure_Count, "fram.Analysis.HTW_Measure_Count", 0.00);
                iniVal(m, p, s, ref fram.Analysis.PT_Measure_Count, "fram.Analysis.HTW_Measure_Count", 0.00);

                //20250407
                iniVal(m, p, s, ref fram.Analysis.Offset.RD_H, "fram.Analysis.Offset.RD_H", 0);
                iniVal(m, p, s, ref fram.Analysis.Offset.RD_W, "fram.Analysis.Offset.RD_W", 0);

                //20250418
                iniVal(m, p, s, ref fram.Analysis.PT_2, "fram.Analysis.PT_2", 0.00);
                iniVal(m, p, s, ref fram.Analysis.PT_2_Z_Offset, "fram.Analysis.PT_2_Z_Offset", 0.00);
                iniVal(m, p, s, ref fram.Analysis.PT_2_X, "fram.Analysis.PT_2_X", 0.00);

                //20250724
                iniVal(m, p, s, ref fram.Analysis.BlueTapeMethod, "fram.Analysis.BlueTapeMethod", 1); 
            }

            if (KeyName == "all" || KeyName == "Motion")
            {
                s = "Motion";

                iniVal(m, p, s, ref fram.m_MachineType, "fram.m_MachineType", 0);
                iniVal(m, p, s, ref fram.m_MotionType, "fram.m_MotionType", 0);
                iniVal(m, p, s, ref fram.m_MotionParamPath, "fram.m_MotionParamPath", "C:\\Users\\TL\\Documents\\Position Board\\FTGM1\\testData0830.prm2");
                int axisnum = 0;
                if (fram.m_MachineType == 0)
                {
                    axisnum = 2;
                }
                else if(fram.m_MachineType == 1)
                {
                    axisnum = 3;
                }
                else
                {
                    axisnum = 5;
                }
                for (int i = 0; i < axisnum; i++)
                {
                    iniVal(m, p, s, ref fram.m_Acc[i], "fram.m_Acc" + i, 100);
                    iniVal(m, p, s, ref fram.m_Dec[i], "fram.m_Dec" + i, 100);
                    iniVal(m, p, s, ref fram.m_posV[i], "fram.m_posV" + i, 10000);
                    iniVal(m, p, s, ref fram.m_pitch[i], "fram.m_pitch" + i, 1000);
                    iniVal(m, p, s, ref fram.m_pitchV[i], "fram.m_pitchV" + i, 10000);
                    iniVal(m, p, s, ref fram.m_jogV[i], "fram.m_jogV" + i, 10000);
                    iniVal(m, p, s, ref fram.m_homeV[i], "fram.m_homeV" + i, 1000);
                    iniVal(m, p, s, ref fram.m_unit[i], "fram.m_unit" + i, 1);
                }
            }

            if (KeyName == "all" || KeyName == "Position")
            {
                s = "Position";
                iniVal(m, p, s, ref fram.Position.LJ_Z, "fram.Position.LJ_Z", 0);
                iniVal(m, p, s, ref fram.Position.RecordCCD_Z, "fram.Position.RecordCCD_Z", 0);
                iniVal(m, p, s, ref fram.Position.HTW_P1_X, "fram.Position.HTW_P1_X", 0);
                iniVal(m, p, s, ref fram.Position.HTW_P1_Z, "fram.Position.HTW_P1_Z", 0);
                iniVal(m, p, s, ref fram.Position.HTW_P2_X, "fram.Position.HTW_P2_X", 0);
                iniVal(m, p, s, ref fram.Position.HTW_P2_Z, "fram.Position.HTW_P2_Z", 0);
				iniVal(m, p, s, ref fram.Position.HTW_P1_FocusRange, "fram.Position.HTW_P1_FocusRange", 30); 
                iniVal(m, p, s, ref fram.Position.HTW_AutoFocus_X, "fram.Position.HTW_AutoFocus_X", 0);
            }

            if (KeyName == "all" || KeyName == "EFEMSts")
            {
                s = "EFEMSts";
                iniVal(m, p, s, ref fram.EFEMSts.TotalCompletedCount, "fram.EFEMSts.TotalCompletedCount", 0);
                iniVal(m, p, s, ref fram.EFEMSts.AlarmSts, "fram.EFEMSts.AlarmSts", 0);
                iniVal(m, p, s, ref fram.EFEMSts.StepBack1, "fram.EFEMSts.StepBack1", "");
                iniVal(m, p, s, ref fram.EFEMSts.StepBack2, "fram.EFEMSts.StepBack2", "");
                iniVal(m, p, s, ref fram.EFEMSts.LoadPortRun, "fram.EFEMSts.LoadPortRun", "");
                iniVal(m, p, s, ref fram.EFEMSts.LoadPort1_FoupID, "fram.EFEMSts.LoadPort1_FoupID", "");
                iniVal(m, p, s, ref fram.EFEMSts.LoadPort2_FoupID, "fram.EFEMSts.LoadPort2_FoupID", "");
                iniVal(m, p, s, ref fram.EFEMSts.Robot_Upper_Slot, "fram.EFEMSts.Robot_Upper_Slot", 0);
                iniVal(m, p, s, ref fram.EFEMSts.Robot_Upper_Slotpn, "fram.EFEMSts.Robot_Upper_Slotpn", "");
                iniVal(m, p, s, ref fram.EFEMSts.Robot_Lower_Slot, "fram.EFEMSts.Robot_Lower_Slot", 0);
                iniVal(m, p, s, ref fram.EFEMSts.Robot_Lower_Slotpn, "fram.EFEMSts.Robot_Lower_Slotpn", "");
                iniVal(m, p, s, ref fram.EFEMSts.Aligner_Slot, "fram.EFEMSts.Aligner_Slot", 0);
                iniVal(m, p, s, ref fram.EFEMSts.Aligner_Slotpn, "fram.EFEMSts.Aligner_Slotpn", "");
                iniVal(m, p, s, ref fram.EFEMSts.Stage_Slot, "fram.EFEMSts.Stage_Slot", 0);
                iniVal(m, p, s, ref fram.EFEMSts.Stage_Slotpn, "fram.EFEMSts.Stage_Slotpn", "");
                for (int i = 0; i < 25; i++)
                {
                    iniVal(m, p, s, ref fram.EFEMSts.Slot_Sts1[i], "fram.EFEMSts.Slot_Sts1" + (i + 1), EFEM.slot_status.Ready.ToString());
                }
                for (int i = 0; i < 25; i++)
                {
                    iniVal(m, p, s, ref fram.EFEMSts.Slot_Sts2[i], "fram.EFEMSts.Slot_Sts2" + (i + 1), EFEM.slot_status.Ready.ToString());
                }

                iniVal(m, p, s, ref fram.EFEMSts.Skip, "fram.EFEMSts.Skip", 0);
            }
            if (KeyName == "all" || KeyName == "SECSPara")
            {
                s = "SECSPara";
                iniVal(m, p, s, ref fram.SECSPara.Loadport1_AccessMode, "fram.SECSPara.Loadport1_AccessMode", 0);
                iniVal(m, p, s, ref fram.SECSPara.Loadport2_AccessMode, "fram.SECSPara.Loadport2_AccessMode", 0);
                iniVal(m, p, s, ref fram.SECSPara.Loadport1_PortTransferState, "fram.SECSPara.Loadport1_PortTransferState", 0);
                iniVal(m, p, s, ref fram.SECSPara.Loadport2_PortTransferState, "fram.SECSPara.Loadport2_PortTransferState", 0);
            }
            if (KeyName == "all" || KeyName == "Recipe")
            {
                s = "Recipe";
                iniVal(m, p, s, ref fram.Recipe.Path, "fram.Recipe.Path", "D:\\FTGM1\\ParameterDirectory\\Recipe");
                if (!Directory.Exists(fram.Recipe.Path))
                {
                    Directory.CreateDirectory(fram.Recipe.Path);
                }
                iniVal(m, p, s, ref fram.Recipe.MotionPatternPath, "fram.Recipe.MotionPatternPath", "D:\\FTGM1\\ParameterDirectory\\TTVPath");
                if (!Directory.Exists(fram.Recipe.MotionPatternPath))
                {
                    Directory.CreateDirectory(fram.Recipe.MotionPatternPath);
                }
                iniVal(m, p, s, ref fram.Recipe.MotionPatternName, "fram.Recipe.MotionPatternName", "D:\\FTGM1\\ParameterDirectory\\TTVPath\\Default.tvr");
                iniVal(m, p, s, ref fram.Recipe.FilenameSelect, "fram.Recipe.FilenameSelect", "");
                iniVal(m, p, s, ref fram.Recipe.Filename_LP1, "fram.Recipe.Filename_LP1", "");
                iniVal(m, p, s, ref fram.Recipe.Filename_LP2, "fram.Recipe.Filename_LP2", "");
            }
        }

        public static void CreateRCPini(string Path)
        {
            iniRCPVal("w", Path, "Recipe");
        }

        /// <summary>
        /// KeyName: all, Recipe, Recipetmp
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="KeyName"></param>
        public static void SaveRCPini(string Path, string KeyName)
        {
            iniRCPVal("w", Path, KeyName);
        }

        /// <summary>
        /// KeyName: all, Recipe, Recipetmp
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="KeyName"></param>
        public static void ReadRcpini(string Path, string KeyName)
        {
            iniRCPVal("r", Path, KeyName);
        }

        public static void ReadRcpini(string Path, string KeyName, RecipeFormat recipe)
        {
            iniRCPVal("r", Path, KeyName, recipe);
        }

        private static void iniRCPVal(string m, string p, string KeyName)
        {
            iniRCPVal(m, p, KeyName, fram.Recipe);
        }
        private static void iniRCPVal(string m, string p, string KeyName, RecipeFormat recipe)
        {
            string s;
            //
            if (KeyName == "all" || KeyName == "Recipe")
            {
                s = "Recipe";

                iniVal(m, p, s, ref recipe.Rotate_Count, "fram.Recipe.Rotate_Count", 8);
                iniVal(m, p, s, ref recipe.Type, "fram.Recipe.Type", 0);
                iniVal(m, p, s, ref recipe.OffsetType, "fram.Recipe.OffsetType", 0);
                iniVal(m, p, s, ref recipe.RepeatTimes, "fram.Recipe.RepeatTimes", 1);
                iniVal(m, p, s, ref recipe.RepeatTimes_now, "fram.Recipe.RepeatTimes_now", 0);
                for (int i = 0; i < 25; i++)
                {
                    iniVal(m, p, s, ref recipe.Slot[i], "fram.Recipe.Slot" + (i + 1), 1);
                }
                iniVal(m, p, s, ref recipe.Angle[0], "fram.Recipe.Angle" + 1, 1);
                iniVal(m, p, s, ref recipe.Angle[1], "fram.Recipe.Angle" + 2, 45);
                iniVal(m, p, s, ref recipe.Angle[2], "fram.Recipe.Angle" + 3, 90);
                iniVal(m, p, s, ref recipe.Angle[3], "fram.Recipe.Angle" + 4, 135);
                iniVal(m, p, s, ref recipe.Angle[4], "fram.Recipe.Angle" + 5, 180);
                iniVal(m, p, s, ref recipe.Angle[5], "fram.Recipe.Angle" + 6, 225);
                iniVal(m, p, s, ref recipe.Angle[6], "fram.Recipe.Angle" + 7, 270);
                iniVal(m, p, s, ref recipe.Angle[7], "fram.Recipe.Angle" + 8, 315);
                iniVal(m, p, s, ref recipe.CreateTime, "fram.Recipe.CreateTime", DateTime.Now.ToString());
                iniVal(m, p, s, ref recipe.ReviseTime, "fram.Recipe.ReviseTime", DateTime.Now.ToString());
                iniVal(m, p, s, ref recipe.MotionPatternName, "fram.Recipe.MotionPatternName", "Default");
                iniVal(m, p, s, ref recipe.MotionPatternPath, "fram.Recipe.MotionPatternPath", "D:\\FTGM1\\ParameterDirectory\\TTVPath");
                iniVal(m, p, s, ref recipe.SF3_ID, "fram.Recipe.SF3_ID", "1");
                iniVal(m, p, s, ref recipe.SF3_Name, "fram.Recipe.SF3_Name", "1");
                iniVal(m, p, s, ref recipe.WaferEdgeEvaluate, "fram.Recipe.WaferEdgeEvaluate", 0);
                iniVal(m, p, s, ref recipe.Analysis_method, "fram.Recipe.Analysis_method", 0);
                iniVal(m, p, s, ref recipe.BlueTapeThreshold, "fram.Recipe.BlueTapeThreshold", 17);
                iniVal(m, p, s, ref recipe.Step1_Range_step1x0, "fram.Recipe.Step1_Range_step1x0", 1);
                iniVal(m, p, s, ref recipe.Step1_Range_step1x1, "fram.Recipe.Step1_Range_step1x1", 4000);
                iniVal(m, p, s, ref recipe.Step2_Range_step1x0, "fram.Recipe.Step2_Range_step1x0", 2300);
                iniVal(m, p, s, ref recipe.Step2_Range_step1x1, "fram.Recipe.Step2_Range_step1x1", 2900);
                iniVal(m, p, s, ref recipe.Step2_Range_step2x0, "fram.Recipe.Step2_Range_step2x0", 1800);
                iniVal(m, p, s, ref recipe.Step2_Range_step2x1, "fram.Recipe.Step2_Range_step2x1", 2000);
                iniVal(m, p, s, ref recipe.Range1_Percent, "fram.Recipe.Range1_Percent", 5);
                iniVal(m, p, s, ref recipe.Range2_Percent, "fram.Recipe.Range2_Percent", 15);
                iniVal(m, p, s, ref recipe.RecordCCDRule, "fram.Recipe.RecordCCDRule", 0);
                iniVal(m, p, s, ref recipe.RecordCCD_Angle_Start, "fram.Recipe.RecordCCD_Angle_Start", 0);
                iniVal(m, p, s, ref recipe.RecordCCD_Angle_End, "fram.Recipe.RecordCCD_Angle_End", 315);
                iniVal(m, p, s, ref recipe.RecordCCD_Angle_Pitch, "fram.Recipe.RecordCCD_Angle_Pitch", 45);
                iniVal(m, p, s, ref recipe.RecordAfterMeasure, "fram.Recipe.RecordAfterMeasure", 0);
                iniVal(m, p, s, ref recipe.LJ_Flat, "fram.Recipe.LJ_Flat", 1.5);
                iniVal(m, p, s, ref recipe.RD_LJ, "fram.Recipe.RD_LJ", 0);

                //=============== 20240628係數 ===================
                iniVal(m, p, s, ref recipe.H1, "fram.Recipe.H1", 1);
                iniVal(m, p, s, ref recipe.H2, "fram.Recipe.H2", 1);
                iniVal(m, p, s, ref recipe.W1, "fram.Recipe.W1", 1);
                iniVal(m, p, s, ref recipe.W2, "fram.Recipe.W2", 1);

                //=============== 20250815 Limit ===================
                iniVal(m, p, s, ref recipe.LimitMethod, "fram.Recipe.LimitMethod", 0);
                iniVal(m, p, s, ref recipe.H1_LowerLimit, "fram.Recipe.H1_LowerLimit", 0);
                iniVal(m, p, s, ref recipe.H2_LowerLimit, "fram.Recipe.H2_LowerLimit", 0);
                iniVal(m, p, s, ref recipe.W1_LowerLimit, "fram.Recipe.W1_LowerLimit", 0);
                iniVal(m, p, s, ref recipe.W2_LowerLimit, "fram.Recipe.W2_LowerLimit", 0);
                iniVal(m, p, s, ref recipe.H1_UpperLimit, "fram.Recipe.H1_UpperLimit", 1000);
                iniVal(m, p, s, ref recipe.H2_UpperLimit, "fram.Recipe.H2_UpperLimit", 1000);
                iniVal(m, p, s, ref recipe.W1_UpperLimit, "fram.Recipe.W1_UpperLimit", 1000);
                iniVal(m, p, s, ref recipe.W2_UpperLimit, "fram.Recipe.W2_UpperLimit", 1000);
            }
        }
        //public static bool Save3MDistribution_Csv(System.Windows.Forms.DataGridView dataGridView1, string fullPath) // 20200310
        public static bool SaveDistribution_Csv(float[,] All_hist, string fullPath, DateTime dt, int index) // 20200310
        {
            #region 舊版210525

            /*
            try
            {
                string savePath;
                DateTime _dt = DateTime.Now;
                string Year, Month, Date;
                Year = string.Format("{0:yyyy}", _dt);
                Month = string.Format("{0:MM}", _dt);
                Date = string.Format("{0:dd}", _dt);
                string time, YY, MM, DD, hh, mm, ss;
                string reportname = fullPath + Year + "\\" + Month + Date + "\\";// + dateime + ".csv";
                string[] datetime, datetime1, datetime2;
                if (dataGridView1.RowCount > 0)
                {
                    //(舊的)先判斷有多少筆資料
                    //for (int num = 0; num < dataGridView1.RowCount; num++)
                    // 改為存第一筆
                    for (int num = 0; num < 1; num++)
                    {
                        time = dataGridView1[0, num].Value.ToString();  //[columns, rows];
                        datetime = Regex.Split(time, " ", RegexOptions.IgnoreCase);
                        datetime1 = Regex.Split(datetime[0], "-", RegexOptions.IgnoreCase); // 日期
                        datetime2 = Regex.Split(datetime[1], ":", RegexOptions.IgnoreCase); // 時間
                        YY = datetime1[0]; // 年
                        MM = datetime1[1]; // 月
                        DD = datetime1[2]; // 日
                        hh = datetime2[0]; // hr
                        mm = datetime2[1]; // min
                        ss = datetime2[2]; // sec
                        savePath = reportname + YY + MM + DD + "-" + hh + mm + ss + ".csv";
                        FileInfo fi = new FileInfo(savePath);
                        if (!fi.Exists)  // 判斷檔案是否存在，若存過則跳過
                        {
                            if (!fi.Directory.Exists) // 判斷是否有資料夾
                            {
                                fi.Directory.Create();
                            }
                            FileStream fs = new FileStream(savePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                            string data = "";

                            //寫入列名稱
                            for (int i = 0; i < 2; i++)
                            {
                                //data += "\"" + dataGridView1.Columns[i].ToString() + "\"";
                                //data += "\"" + dataGridView1.Columns[i].HeaderText.ToString() + "\"";
                                if (i == 0)
                                {
                                    //data += ",";
                                    data += "Relative Height" + ",";
                                }
                                if (i == 1)
                                {
                                    //data += ",";
                                    data += "Number of Data" + ",";
                                }
                            }
                            sw.WriteLine(data);

                            //寫出各行數據

                            string[,] All_hist = new string[200, 2];
                            string histdata = dataGridView1.Rows[num].Cells[sram.distributionColumnNum].Value.ToString(); // sram.distributionColumnNum = 7
                            string[] ReadArray1 = Regex.Split(histdata, ",", RegexOptions.IgnoreCase);
                            string[] s;
                            int num_x = 0, num_y = 0;       // 計數的檢查功能
                            for (int i = 0; i < ReadArray1.Length; i++)
                            {
                                if (ReadArray1[i].IndexOf("/x", StringComparison.OrdinalIgnoreCase) > 0)  // x
                                {
                                    //x += ReadArray1[i];
                                    s = Regex.Split(ReadArray1[i], "/x", RegexOptions.IgnoreCase);
                                    All_hist[num_x, 0] = s[0];  // x
                                    num_x++;
                                }
                                else if (ReadArray1[i].IndexOf("/y", StringComparison.OrdinalIgnoreCase) > 0)  // y
                                {
                                    //y += ReadArray1[i];
                                    s = Regex.Split(ReadArray1[i], "/y", RegexOptions.IgnoreCase);
                                    All_hist[num_y, 1] = s[0];  // y
                                    num_y++;
                                }
                            }
                            if (num_x == 200 || num_y == 200)       // 確定讀取回的資料數量正確在畫
                            {
                            }

                            // All_hist[200,2]
                            for (int i = 0; i < 200; i++)
                            {
                                data = "";

                                string str = All_hist[i, 0] + "," + All_hist[i, 1];
                                data = str;

                                //dataGridView1.Rows[0].Cells[1].Value = Convert.ToString(2);
                                //string str = dataGridView1.Rows[i].Cells[j].Value.ToString(); //沒數據會儲存錯誤
                                //str = string.Format("\"{0}\"", str);
                                //    data += str;

                                //    if (j < dataGridView1.Columns.Count - 1)
                                //    {
                                //        data += ",";
                                //    }

                                sw.WriteLine(data);
                            }
                            sw.Close();
                            fs.Close();
                            //MessageBox.Show("Save data OK");
                            //return true;
                        }
                    }
                }
                return true;
            }
            catch
            {
                //MessageBox.Show("數據儲存錯誤");
                return false;
            }
            */

            #endregion 舊版210525

            try
            {
                string savePath;
                DateTime _dt = dt;
                string Year, Month, Date;
                Year = string.Format("{0:yyyy}", _dt);
                Month = string.Format("{0:MM}", _dt);
                Date = string.Format("{0:dd}", _dt);
                string time, YY, MM, DD, hh, mm, ss;
                string reportname = fullPath + Year + "\\" + Month + Date + "\\";// + dateime + ".csv";
                string[] datetime, datetime1, datetime2;

                //time = dataGridView1[0, num].Value.ToString();  //[columns, rows];
                //sram.saveDataTime = DateTime.Now;
                time = _dt.ToString("yyyy-MM-dd HH:mm:ss"); //與存在datagridview的時間相同
                datetime = Regex.Split(time, " ", RegexOptions.IgnoreCase);
                datetime1 = Regex.Split(datetime[0], "-", RegexOptions.IgnoreCase); // 日期
                datetime2 = Regex.Split(datetime[1], ":", RegexOptions.IgnoreCase); // 時間

                YY = datetime1[0]; // 年
                MM = datetime1[1]; // 月
                DD = datetime1[2]; // 日
                hh = datetime2[0]; // hr
                mm = datetime2[1]; // min
                ss = datetime2[2]; // sec
                //savePath = reportname + YY + MM + DD + "-" + hh + mm + ss + ".csv";
                savePath = fullPath + YY + "\\" + MM + DD + "\\" + YY + MM + DD + "-" + hh + mm + ss + "-" + index + ".csv";
                FileInfo fi = new FileInfo(savePath);
                if (!fi.Exists)  // 判斷檔案是否存在，若存過則跳過
                {
                    if (!fi.Directory.Exists) // 判斷是否有資料夾
                    {
                        fi.Directory.Create();
                    }
                    FileStream fs = new FileStream(savePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                    string data = "";

                    //寫入列名稱
                    for (int i = 0; i < 2; i++)
                    {
                        //data += "\"" + dataGridView1.Columns[i].ToString() + "\"";
                        //data += "\"" + dataGridView1.Columns[i].HeaderText.ToString() + "\"";
                        if (i == 0)
                        {
                            //data += ",";
                            data += "Relative Height" + ",";
                        }
                        if (i == 1)
                        {
                            //data += ",";
                            data += "Number of Data" + ",";
                        }
                    }
                    sw.WriteLine(data);

                    //寫出各行數據
                    // All_hist[200,2]
                    for (int i = 0; i < 200; i++)
                    {
                        data = "";
                        string str = All_hist[i, 0].ToString() + "," + All_hist[i, 1].ToString();
                        data = str;
                        sw.WriteLine(data);
                    }
                    sw.Close();
                    fs.Close();
                    //MessageBox.Show("Save data OK");
                    //return true;
                }

                return true;
            }
            catch
            {
                //MessageBox.Show("數據儲存錯誤");
                return false;
            }
        }

        public static void SaveRawdataforAI_txt(float[] rawdata, int startindex, int endindex)
        {
            // int startindex,int endindex = 10000
            string DatasavPath = sram.dirfilepath; // exe執行檔目錄
                                                   // 存txt
            string Savefilepath;
            Savefilepath = DatasavPath + "\\testpythondata.txt";
            //bool busy = true;
            StreamWriter sw = new StreamWriter(Savefilepath);
            if (endindex - startindex > 0)
            {
                for (int j = 0; j < endindex - startindex; j++)
                {
                    sw.WriteLine(rawdata[j]);
                }
            }
            sw.Close();
        }

        public static void SaveRawdataforAItrain_txt(float[] rawdata, int startindex, int endindex, string path, int num)
        {
            // int startindex,int endindex = 10000

            string DatasavPath = sram.dirfilepath; // exe執行檔目錄
            // 存txt
            string Savefilepath;
            Savefilepath = path + "-" + num + ".txt";
            StreamWriter sw = new StreamWriter(Savefilepath);
            if (endindex - startindex > 0)
            {
                for (int j = startindex; j < endindex; j++)
                {
                    sw.WriteLine(rawdata[j]);
                }
            }
            sw.Close();
        }

        public static void SaveRawdata_txt(float[] rawdata, string note, DateTime dt)
        {
            //BinaryWriter bw;
            //BinaryWriter br;
            string UIDate, Year, month, Date, Hour, minutes, Second, DatasavPath = "";
            DateTime _dt = dt;
            Year = string.Format("{0:yyyy}", _dt);
            month = string.Format("{0:MM}", _dt);
            Date = string.Format("{0:dd}", _dt);
            Hour = string.Format("{0:HH}", _dt);
            minutes = string.Format("{0:mm}", _dt);
            Second = string.Format("{0:ss}", _dt);
            UIDate = string.Format("{0:yyyy-MM-dd-HH:mm:ss}", _dt);
            DatasavPath = sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\RawData\\" + Year + "\\" + month + Date;
            //logsavPath = ParamFile.dirname + "\\Log\\" + Year + "\\" + month + Date;
            //if (Directory.Exists(ParamFile.dirname + "\\Log\\" + Year))//***
            if (Directory.Exists(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year))//***
            {//判斷有無資料夾
                if (!Directory.Exists(DatasavPath))
                {
                    Directory.CreateDirectory(DatasavPath);
                }
            }
            else
            {//無年資料夾
             //Directory.CreateDirectory(ParamFile.dirname + "\\Log\\" + Year);
                Directory.CreateDirectory(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year);
                if (!Directory.Exists(DatasavPath))
                {
                    Directory.CreateDirectory(DatasavPath);
                }
            }
            // 存txt
            string Savefilepath;
            Savefilepath = DatasavPath + "\\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "_" + note;
            Savefilepath = Savefilepath + ".txt";
            StreamWriter sw = new StreamWriter(Savefilepath);

            for (int j = 0; j < rawdata.Length; j++)
            {
                sw.WriteLine(rawdata[j]);
            }
            /*
            if (sram.STILdata.Count > 0)
            {
                for (int j = 0; j < sram.STILdata.Count; j++)
                {
                    sw.WriteLine(sram.STILdata[j][0].ToString());
                }
            }
            if (sram.Keyencedata.Count > 0)
            {
                for (int j = 0; j < sram.Keyencedata.Count; j++)
                {
                    sw.WriteLine(sram.Keyencedata[j].ToString());
                }
            }
            */
            sw.Close();
        }

        public static bool SaveRawdata_bin(float[] rawdata, DateTime dt)
        {
            BinaryWriter bw;
            //BinaryWriter br;
            string UIDate, Year, month, Date, Hour, minutes, Second, DatasavPath = "";
            DateTime _dt = dt;
            Year = string.Format("{0:yyyy}", _dt);
            month = string.Format("{0:MM}", _dt);
            Date = string.Format("{0:dd}", _dt);
            Hour = string.Format("{0:HH}", _dt);
            minutes = string.Format("{0:mm}", _dt);
            Second = string.Format("{0:ss}", _dt);
            UIDate = string.Format("{0:yyyy-MM-dd-HH:mm:ss}", _dt);
            DatasavPath = sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\RawData\\" + Year + "\\" + month + Date;
            //logsavPath = ParamFile.dirname + "\\Log\\" + Year + "\\" + month + Date;
            //if (Directory.Exists(ParamFile.dirname + "\\Log\\" + Year))//***
            if (Directory.Exists(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year))//***
            {//判斷有無資料夾
                if (!Directory.Exists(DatasavPath))
                {
                    Directory.CreateDirectory(DatasavPath);
                }
            }
            else
            {//無年資料夾
             //Directory.CreateDirectory(ParamFile.dirname + "\\Log\\" + Year);
                Directory.CreateDirectory(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year);
                if (!Directory.Exists(DatasavPath))
                {
                    Directory.CreateDirectory(DatasavPath);
                }
            }

            string Savefilepath;
            Savefilepath = DatasavPath + "\\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".bin";

            try
            {
                bw = new BinaryWriter(new FileStream(Savefilepath, FileMode.Create));
                // 寫入文件
                try
                {
                    for (int j = 0; j < rawdata.Length; j++)
                    {
                        bw.Write(rawdata[j]);
                    }

                    //if (sram.STILdata.Count > 0)
                    //{
                    //    for (int j = 0; j < sram.STILdata.Count; j++)
                    //    {
                    //        bw.Write(sram.STILdata[j][0]);
                    //        bw.Write(sram.STILdata[j][1]);
                    //    }
                    //}
                    //if (sram.Keyencedata.Count > 0)
                    //{
                    //    for (int j = 0; j < sram.Keyencedata.Count; j++)
                    //    {
                    //        bw.Write(sram.Keyencedata[j]);
                    //    }
                    //}
                }
                catch (IOException e1)
                {
                    MessageBox.Show(e1.Message + "\n Cannot write to file.");
                    return false;
                }
                bw.Close();
                return true;
            }
            catch (IOException e1)
            {
                MessageBox.Show(e1.Message + "\n Cannot create file.");
                return false;
            }
        }

        public static void SaveRawdata_Csv(double[] rawdata, string LotID, string note, DateTime dt)
        {
            string UIDate, Year, month, Date, Hour, minutes, Second, DatasavPath = "", DatasavPath2 = "";
            DateTime _dt = dt;
            Year = string.Format("{0:yyyy}", _dt);
            month = string.Format("{0:MM}", _dt);
            Date = string.Format("{0:dd}", _dt);
            Hour = string.Format("{0:HH}", _dt);
            minutes = string.Format("{0:mm}", _dt);
            Second = string.Format("{0:ss}", _dt);
            UIDate = string.Format("{0:yyyy-MM-dd-HH:mm:ss}", _dt);
            DatasavPath = sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\Baseline\\" + Year + "\\" + month + Date;
            //logsavPath = ParamFile.dirname + "\\Log\\" + Year + "\\" + month + Date;
            //if (Directory.Exists(sram.dirfilepath + "\\Baseline\\" + Year))//***
            DatasavPath2 = DatasavPath + "\\" + LotID;
            if (!Directory.Exists(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year))//***
            {//無年資料夾
                Directory.CreateDirectory(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year);
            }
            if (!Directory.Exists(DatasavPath))
            {//無月日資料夾
                Directory.CreateDirectory(DatasavPath);
            }
            if (!Directory.Exists(DatasavPath2))
            {//無FoupID資料夾
                Directory.CreateDirectory(DatasavPath2);
            }

            string Savefilepath;
            //Savefilepath = DatasavPath + "\\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".bin";
            Savefilepath = DatasavPath2 + "\\" + dt.ToString("yyyyMMdd-HHmmss") + "_" + note; // 加在檔名最後面
            //ParamFile.SaveCsv(rawdata, Savefilepath);
            try
            {
                string dateime = dt.ToString("yyyyMMddHHmmss");
                //File.WriteAllText(@"C:\Users\user\Desktop\laser\1.txt", rtbLaserData.Text); // 也可以指定編碼方式
                //fullPath = "C:\\AHM-022\\CSV報表\\";
                //fullPath = @"C:\Users\user\Desktop\laser\";
                //string reportname = fullPath;// + dateime + ".csv";
                string reportname = Savefilepath + ".csv";
                AnalysisData.sPath_Base = reportname;
                //if (!Directory.Exists(fullPath))
                //Directory.CreateDirectory(fullPath);

                FileInfo fi = new FileInfo(reportname);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
                FileStream fs = new FileStream(reportname, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                string data = "";

                //寫入列名稱
                for (int i = 0; i < 1; i++)
                {
                    //data += "\"" + dataGridView1.Columns[i].ToString() + "\"";
                    //if (i == 0)
                    //{
                    //    //data += "\"" + "Distance" + "\"";
                    //    data += "Distance";
                    //}
                    //else if (i == 1)
                    //{
                    //    data += "\"" + "Intensity" + "\"";
                    //}
                    //if (i < 2 - 1)
                    //{
                    //    data += ",";
                    //}
                }
                //sw.WriteLine(data);

                //寫出各行數據
                for (int i = 0; i < rawdata.Length; i++)
                {
                    data = "";
                    for (int j = 0; j < 1; j++)
                    {
                        //dataGridView1.Rows[0].Cells[1].Value = Convert.ToString(2);
                        string str = rawdata[i].ToString(); //沒數據會儲存錯誤
                        //str = string.Format("\"{0}\"", str);
                        data += str;

                        //if (j < 2 - 1)
                        //{
                        //    data += ",";
                        //}
                    }
                    sw.WriteLine(data);
                }
                sw.Close();
                fs.Close();
                //MessageBox.Show("Save data OK");
            }
            catch
            {
                //MessageBox.Show("數據儲存錯誤");
            }
        }

        public static void SaveRawdata_Csv3(double[] rawdata, double[] rawdata2, double[] rawdata3, string LotID, string note, DateTime dt)
        {
            string UIDate, Year, month, Date, Hour, minutes, Second, DatasavPath = "", DatasavPath2 = "";
            DateTime _dt = dt;
            Year = string.Format("{0:yyyy}", _dt);
            month = string.Format("{0:MM}", _dt);
            Date = string.Format("{0:dd}", _dt);
            Hour = string.Format("{0:HH}", _dt);
            minutes = string.Format("{0:mm}", _dt);
            Second = string.Format("{0:ss}", _dt);
            UIDate = string.Format("{0:yyyy-MM-dd-HH:mm:ss}", _dt);
            DatasavPath = sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\Baseline\\" + Year + "\\" + month + Date;
            //logsavPath = ParamFile.dirname + "\\Log\\" + Year + "\\" + month + Date;
            DatasavPath2 = DatasavPath + "\\" + LotID;
            //if (Directory.Exists(sram.dirfilepath + "\\Baseline\\" + Year))//***
            if (!Directory.Exists(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year))//***
            {//無年資料夾
                Directory.CreateDirectory(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year);
            }
            if (!Directory.Exists(DatasavPath))
            {//無月日資料夾
                Directory.CreateDirectory(DatasavPath);
            }
            if (!Directory.Exists(DatasavPath2))
            {//無FoupID資料夾
                Directory.CreateDirectory(DatasavPath2);
            }

            string Savefilepath;
            //Savefilepath = DatasavPath + "\\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".bin";
            Savefilepath = DatasavPath2 + "\\" + dt.ToString("yyyyMMdd-HHmmss") + "_" + note; // 加在檔名最後面
            //ParamFile.SaveCsv(rawdata, Savefilepath);
            try
            {
                string dateime = dt.ToString("yyyyMMddHHmmss");
                //File.WriteAllText(@"C:\Users\user\Desktop\laser\1.txt", rtbLaserData.Text); // 也可以指定編碼方式
                //fullPath = "C:\\AHM-022\\CSV報表\\";
                //fullPath = @"C:\Users\user\Desktop\laser\";
                //string reportname = fullPath;// + dateime + ".csv";
                string reportname = Savefilepath + ".csv";
                AnalysisData.sPath_Raw = reportname;
                //if (!Directory.Exists(fullPath))
                //Directory.CreateDirectory(fullPath);

                FileInfo fi = new FileInfo(reportname);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
                FileStream fs = new FileStream(reportname, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                string data = "";

                //寫入列名稱
                for (int i = 0; i < 1; i++)
                {
                    //data += "\"" + dataGridView1.Columns[i].ToString() + "\"";
                    //if (i == 0)
                    //{
                    //    //data += "\"" + "Distance" + "\"";
                    //    data += "Distance";
                    //}
                    //else if (i == 1)
                    //{
                    //    data += "\"" + "Intensity" + "\"";
                    //}
                    //if (i < 2 - 1)
                    //{
                    //    data += ",";
                    //}
                }
                //sw.WriteLine(data);

                //寫出各行數據
                for (int i = 0; i < rawdata.Length; i++)
                {
                    data = "";
                    //dataGridView1.Rows[0].Cells[1].Value = Convert.ToString(2);
                    string str = rawdata[i].ToString(); //沒數據會儲存錯誤
                    string str2 = rawdata2[i].ToString(); //沒數據會儲存錯誤
                    string str3 = rawdata3[i].ToString(); //沒數據會儲存錯誤
                    //str = string.Format("\"{0}\"", str);
                    data += str + "," + str2 + "," + str3;
                    sw.WriteLine(data);
                }
                sw.Close();
                fs.Close();
                //MessageBox.Show("Save data OK");
            }
            catch
            {
                //MessageBox.Show("數據儲存錯誤");
            }
        }

        public static void SaveRawdata_Csv4(double[] rawdata, double[] rawdata2, double[] rawdata3, double[] rawdata4, double max_h1, double max_w1, double max_h2, double max_w2, string LotID, string note, DateTime dt, int iCount)
        {
            string UIDate, Year, month, Date, Hour, minutes, Second, DatasavPath = "", DatasavPath2 = "";
            DateTime _dt = dt;
            Year = string.Format("{0:yyyy}", _dt);
            month = string.Format("{0:MM}", _dt);
            Date = string.Format("{0:dd}", _dt);
            Hour = string.Format("{0:HH}", _dt);
            minutes = string.Format("{0:mm}", _dt);
            Second = string.Format("{0:ss}", _dt);
            UIDate = string.Format("{0:yyyy-MM-dd-HH:mm:ss}", _dt);
            DatasavPath = sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\Baseline\\" + Year + "\\" + month + Date;
            //logsavPath = ParamFile.dirname + "\\Log\\" + Year + "\\" + month + Date;
            DatasavPath2 = DatasavPath + "\\" + LotID;
            //if (Directory.Exists(sram.dirfilepath + "\\Baseline\\" + Year))//***
            if (!Directory.Exists(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year))//***
            {//無年資料夾
                Directory.CreateDirectory(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year);
            }
            if (!Directory.Exists(DatasavPath))
            {//無月日資料夾
                Directory.CreateDirectory(DatasavPath);
            }
            if (!Directory.Exists(DatasavPath2))
            {//無FoupID資料夾
                Directory.CreateDirectory(DatasavPath2);
            }

            string str = string.Empty; //沒數據會儲存錯誤
            string str2 = string.Empty; //沒數據會儲存錯誤
            string str3 = string.Empty; //沒數據會儲存錯誤
            string str4 = string.Empty; //沒數據會儲存錯誤
            string str5 = string.Empty; //沒數據會儲存錯誤
            string str6 = string.Empty; //沒數據會儲存錯誤
            string str7 = string.Empty; //沒數據會儲存錯誤
            string str8 = string.Empty; //沒數據會儲存錯誤

            string Savefilepath;
            //Savefilepath = DatasavPath + "\\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".bin";
            Savefilepath = DatasavPath2 + "\\" + dt.ToString("yyyyMMdd-HHmmss") + "_" + note; // 加在檔名最後面
            //ParamFile.SaveCsv(rawdata, Savefilepath);
            try
            {
                string dateime = dt.ToString("yyyyMMddHHmmss");
                //File.WriteAllText(@"C:\Users\user\Desktop\laser\1.txt", rtbLaserData.Text); // 也可以指定編碼方式
                //fullPath = "C:\\AHM-022\\CSV報表\\";
                //fullPath = @"C:\Users\user\Desktop\laser\";
                //string reportname = fullPath;// + dateime + ".csv";
                string reportname = Savefilepath + ".csv";
                //if (!Directory.Exists(fullPath))
                //Directory.CreateDirectory(fullPath);

                FileInfo fi = new FileInfo(reportname);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
                FileStream fs = new FileStream(reportname, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                string data = "";

                //寫入列名稱
                for (int i = 0; i < 1; i++)
                {
                    //data += "\"" + dataGridView1.Columns[i].ToString() + "\"";
                    //if (i == 0)
                    //{
                    //    //data += "\"" + "Distance" + "\"";
                    //    data += "Distance";
                    //}
                    //else if (i == 1)
                    //{
                    //    data += "\"" + "Intensity" + "\"";
                    //}
                    //if (i < 2 - 1)
                    //{
                    //    data += ",";
                    //}
                }
                //sw.WriteLine(data);

                //寫出各行數據
                data = "Angle  , H1 , W1 , H2 , W2";
                sw.WriteLine(data);
                if (iCount == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        //data = "Angle0" + (i+1).ToString() + "," + rawdata[i * 2] + "," + rawdata2[i * 2] + "," + rawdata3[i * 2] + "," + rawdata4[i * 2];
                        data = "Angle0" + (i + 1).ToString() + "," + rawdata[i] + "," + rawdata2[i] + "," + rawdata3[i] + "," + rawdata4[i];
                        sw.WriteLine(data);
                    }
                }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        data = "Angle0" + (i + 1).ToString() + "," + rawdata[i] + "," + rawdata2[i] + "," + rawdata3[i] + "," + rawdata4[i];
                        sw.WriteLine(data);
                    }
                }
                data = "Max," + max_h1 + "," + max_w1 + "," + max_h2 + "," + max_w2;
                sw.WriteLine(data);
                sw.Close();
                fs.Close();
                //MessageBox.Show("Save data OK");
            }
            catch
            {
                //MessageBox.Show("數據儲存錯誤");
            }
        }

        public static void SaveRawdata_Csv5(double[] rawdata, double[] rawdata2, string LotID, string note, DateTime dt)
        {
            string UIDate, Year, month, Date, Hour, minutes, Second, DatasavPath = "", DatasavPath2 = "";
            DateTime _dt = dt;
            Year = string.Format("{0:yyyy}", _dt);
            month = string.Format("{0:MM}", _dt);
            Date = string.Format("{0:dd}", _dt);
            Hour = string.Format("{0:HH}", _dt);
            minutes = string.Format("{0:mm}", _dt);
            Second = string.Format("{0:ss}", _dt);
            UIDate = string.Format("{0:yyyy-MM-dd-HH:mm:ss}", _dt);
            DatasavPath = sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\Baseline\\" + Year + "\\" + month + Date;
            //logsavPath = ParamFile.dirname + "\\Log\\" + Year + "\\" + month + Date;
            DatasavPath2 = DatasavPath + "\\" + LotID;
            //if (Directory.Exists(sram.dirfilepath + "\\Baseline\\" + Year))//***
            if (!Directory.Exists(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year))//***
            {//無年資料夾
                Directory.CreateDirectory(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year);
            }
            if (!Directory.Exists(DatasavPath))
            {//無月日資料夾
                Directory.CreateDirectory(DatasavPath);
            }
            if (!Directory.Exists(DatasavPath2))
            {//無FoupID資料夾
                Directory.CreateDirectory(DatasavPath2);
            }

            string Savefilepath;
            //Savefilepath = DatasavPath + "\\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".bin";
            Savefilepath = DatasavPath2 + "\\" + dt.ToString("yyyyMMdd-HHmmss") + "_" + note; // 加在檔名最後面
            //ParamFile.SaveCsv(rawdata, Savefilepath);
            try
            {
                string dateime = dt.ToString("yyyyMMddHHmmss");
                //File.WriteAllText(@"C:\Users\user\Desktop\laser\1.txt", rtbLaserData.Text); // 也可以指定編碼方式
                //fullPath = "C:\\AHM-022\\CSV報表\\";
                //fullPath = @"C:\Users\user\Desktop\laser\";
                //string reportname = fullPath;// + dateime + ".csv";
                string reportname = Savefilepath + ".csv";
                AnalysisData.sPath_Int = reportname;
                //if (!Directory.Exists(fullPath))
                //Directory.CreateDirectory(fullPath);

                FileInfo fi = new FileInfo(reportname);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
                FileStream fs = new FileStream(reportname, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                string data = "";

                //寫入列名稱
                for (int i = 0; i < 1; i++)
                {
                    //data += "\"" + dataGridView1.Columns[i].ToString() + "\"";
                    //if (i == 0)
                    //{
                    //    //data += "\"" + "Distance" + "\"";
                    //    data += "Distance";
                    //}
                    //else if (i == 1)
                    //{
                    //    data += "\"" + "Intensity" + "\"";
                    //}
                    //if (i < 2 - 1)
                    //{
                    //    data += ",";
                    //}
                }
                //sw.WriteLine(data);

                //寫出各行數據
                for (int i = 0; i < rawdata.Length; i++)
                {
                    data = "";
                    //dataGridView1.Rows[0].Cells[1].Value = Convert.ToString(2);
                    string str = rawdata[i].ToString(); //沒數據會儲存錯誤
                    string str2 = rawdata2[i].ToString(); //沒數據會儲存錯誤
                    //str = string.Format("\"{0}\"", str);
                    data += str + "," + str2 ;
                    sw.WriteLine(data);
                }
                sw.Close();
                fs.Close();
                //MessageBox.Show("Save data OK");
            }
            catch
            {
                //MessageBox.Show("數據儲存錯誤");
            }
        }

        //20251222
        public static void SaveRawdata_AutoFocus(List<short[]> list_AutoFocus, string LotID, string note, DateTime dt)
        {
            string UIDate, Year, month, Date, Hour, minutes, Second, DatasavPath = "", DatasavPath2 = "";
            DateTime _dt = dt;
            Year = string.Format("{0:yyyy}", _dt);
            month = string.Format("{0:MM}", _dt);
            Date = string.Format("{0:dd}", _dt);
            Hour = string.Format("{0:HH}", _dt);
            minutes = string.Format("{0:mm}", _dt);
            Second = string.Format("{0:ss}", _dt);
            UIDate = string.Format("{0:yyyy-MM-dd-HH:mm:ss}", _dt);
            DatasavPath = sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\Baseline\\" + Year + "\\" + month + Date;
            //logsavPath = ParamFile.dirname + "\\Log\\" + Year + "\\" + month + Date;
            DatasavPath2 = DatasavPath + "\\" + LotID;
            //if (Directory.Exists(sram.dirfilepath + "\\Baseline\\" + Year))//***
            if (!Directory.Exists(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year))//***
            {//無年資料夾
                Directory.CreateDirectory(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year);
            }
            if (!Directory.Exists(DatasavPath))
            {//無月日資料夾
                Directory.CreateDirectory(DatasavPath);
            }
            if (!Directory.Exists(DatasavPath2))
            {//無FoupID資料夾
                Directory.CreateDirectory(DatasavPath2);
            }

            string Savefilepath;
            //Savefilepath = DatasavPath + "\\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".bin";
            Savefilepath = DatasavPath2 + "\\" + dt.ToString("yyyyMMdd-HHmmss") + "_" + note; // 加在檔名最後面
            //ParamFile.SaveCsv(rawdata, Savefilepath);
            try
            {
                string dateime = dt.ToString("yyyyMMddHHmmss");
                //File.WriteAllText(@"C:\Users\user\Desktop\laser\1.txt", rtbLaserData.Text); // 也可以指定編碼方式
                //fullPath = "C:\\AHM-022\\CSV報表\\";
                //fullPath = @"C:\Users\user\Desktop\laser\";
                //string reportname = fullPath;// + dateime + ".csv";
                string reportname = Savefilepath + ".csv";
                AnalysisData.sPath_Int = reportname;
                //if (!Directory.Exists(fullPath))
                //Directory.CreateDirectory(fullPath);

                FileInfo fi = new FileInfo(reportname);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
                FileStream fs = new FileStream(reportname, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                string data = "";

                

                //寫出各行數據
                for (int i = 0; i < list_AutoFocus.Count; i++)
                {
                    data = "";
                    string str = string.Empty;
                    for (int j = 0; j < list_AutoFocus[i].Length; j++)
                    {
                        str += list_AutoFocus[i][j].ToString() + ",";
                    }
                    data = str.Substring(0, str.Length - 1);
                    sw.WriteLine(data);
                }
                sw.Close();
                fs.Close();
                //MessageBox.Show("Save data OK");
            }
            catch
            {
                //MessageBox.Show("數據儲存錯誤");
            }
        }

        public static void SaveSingledata_Csv(string datain, string LotID, string note, DateTime dt)
        {
            string UIDate, Year, month, Date, Hour, minutes, Second, DatasavPath = "", DatasavPath2 = "";
            DateTime _dt = dt;
            Year = string.Format("{0:yyyy}", _dt);
            month = string.Format("{0:MM}", _dt);
            Date = string.Format("{0:dd}", _dt);
            Hour = string.Format("{0:HH}", _dt);
            minutes = string.Format("{0:mm}", _dt);
            Second = string.Format("{0:ss}", _dt);
            UIDate = string.Format("{0:yyyy-MM-dd-HH:mm:ss}", _dt);
            DatasavPath = sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\Baseline\\" + Year + "\\" + month + Date;
            //logsavPath = ParamFile.dirname + "\\Log\\" + Year + "\\" + month + Date;
            //DatasavPath2 = DatasavPath + "\\" + LotID;
            //if (Directory.Exists(sram.dirfilepath + "\\Baseline\\" + Year))//***
            if (!Directory.Exists(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year))//***
            {//無年資料夾
                Directory.CreateDirectory(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year);
            }
            if (!Directory.Exists(DatasavPath))
            {//無月日資料夾
                Directory.CreateDirectory(DatasavPath);
            }
            /*
            if (!Directory.Exists(DatasavPath2))
            {//無FoupID資料夾
                Directory.CreateDirectory(DatasavPath2);
            }*/

            string Savefilepath;
            //Savefilepath = DatasavPath + "\\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".bin";
            if(note != string.Empty)
                Savefilepath = DatasavPath + "\\" + "HTW_DailyData_" + note; // 加在檔名最後面
            else
                Savefilepath = DatasavPath + "\\" + "HTW_DailyData"; 
            //ParamFile.SaveCsv(rawdata, Savefilepath);
            try
            {
                string datetime = dt.ToString("yyyyMMddHHmmss");
                string reportname = Savefilepath + ".csv";

                FileInfo fi = new FileInfo(reportname);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
                //FileStream fs = new FileStream(reportname, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(reportname, true, System.Text.Encoding.UTF8);

                string data = datetime + "," + LotID + "," + datain;

                sw.WriteLine(data);
                sw.Close();
            }
            catch
            {
                //MessageBox.Show("數據儲存錯誤");
            }
        }

        public static void SaveRawdata_png(Chart chart, string LotID, string note, DateTime dt, int extraDir = 0)
        {
            string UIDate, Year, month, Date, Hour, minutes, Second, DatasavPath = "", DatasavPath2 = "";
            DateTime _dt = dt;
            Year = string.Format("{0:yyyy}", _dt);
            month = string.Format("{0:MM}", _dt);
            Date = string.Format("{0:dd}", _dt);
            Hour = string.Format("{0:HH}", _dt);
            minutes = string.Format("{0:mm}", _dt);
            Second = string.Format("{0:ss}", _dt);
            UIDate = string.Format("{0:yyyy-MM-dd-HH:mm:ss}", _dt);
            DatasavPath = sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\Baseline\\" + Year + "\\" + month + Date;
            //logsavPath = ParamFile.dirname + "\\Log\\" + Year + "\\" + month + Date;
            //if (Directory.Exists(sram.dirfilepath + "\\Baseline\\" + Year))//***
            DatasavPath2 = DatasavPath + "\\" + LotID;
            string DatasavPath2_Origin = DatasavPath2 + "\\" + "Origin";
            string DatasavPath2_Correct = DatasavPath2 + "\\" + "Correct";
            if (!Directory.Exists(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year))//***
            {//無年資料夾
                Directory.CreateDirectory(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year);
            }
            if (!Directory.Exists(DatasavPath))
            {//無月日資料夾
                Directory.CreateDirectory(DatasavPath);
            }
            if (!Directory.Exists(DatasavPath2))
            {//無FoupID資料夾
                Directory.CreateDirectory(DatasavPath2);           
            }
            if (!Directory.Exists(DatasavPath2_Origin))
            {
                Directory.CreateDirectory(DatasavPath2_Origin);
            }
            if (!Directory.Exists(DatasavPath2_Correct))
            {
                Directory.CreateDirectory(DatasavPath2_Correct);
            }
            string Savefilepath;
            if(extraDir == 1)
                Savefilepath = DatasavPath2_Origin + "\\" + dt.ToString("yyyyMMdd-HHmmss") + "_" + note; // 加在檔名最後面
            else if (extraDir == 2)
                Savefilepath = DatasavPath2_Correct + "\\" + dt.ToString("yyyyMMdd-HHmmss") + "_" + note; // 加在檔名最後面
            else
                Savefilepath = DatasavPath2 + "\\" + dt.ToString("yyyyMMdd-HHmmss") + "_" + note; // 加在檔名最後面
            //ParamFile.SaveCsv(rawdata, Savefilepath);
            try
            {
                chart.BackColor = Color.White;
                chart.SaveImage(Savefilepath + ".png", ChartImageFormat.Png);
                chart.BackColor = Color.Transparent;

                //AddText("測試測試", Savefilepath + ".png");
            }
            catch (Exception ee)
            {
                InsertLog.SavetoDB(5, "存圖錯誤, " + ee.Message);
                //MessageBox.Show("數據儲存錯誤");
            }
        }

        public static void SaveRawdata_png(Chart chart, string LotID, string note, DateTime dt, string sText1, string sText2, double w1, double w2, double h1, double h2, int extraDir = 0)
        {
            string UIDate, Year, month, Date, Hour, minutes, Second, DatasavPath = "";
            DateTime _dt = dt;
            Year = string.Format("{0:yyyy}", _dt);
            month = string.Format("{0:MM}", _dt);
            Date = string.Format("{0:dd}", _dt);
            Hour = string.Format("{0:HH}", _dt);
            minutes = string.Format("{0:mm}", _dt);
            Second = string.Format("{0:ss}", _dt);
            UIDate = string.Format("{0:yyyy-MM-dd-HH:mm:ss}", _dt);
            DatasavPath = sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\Baseline\\" + Year + "\\" + month + Date;
            //logsavPath = ParamFile.dirname + "\\Log\\" + Year + "\\" + month + Date;
            //if (Directory.Exists(sram.dirfilepath + "\\Baseline\\" + Year))//***
            string DatasavPath2 = DatasavPath + "\\" + LotID;
            string DatasavPath2_Origin = DatasavPath2 + "\\" + "Origin";
            string DatasavPath2_Correct = DatasavPath2 + "\\" + "Correct";
            if (!Directory.Exists(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year))//***
            {//無年資料夾
                Directory.CreateDirectory(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year);
            }
            if (!Directory.Exists(DatasavPath))
            {//無月日資料夾
                Directory.CreateDirectory(DatasavPath);
            }
            if (!Directory.Exists(DatasavPath2))
            {//無FoupID資料夾
                Directory.CreateDirectory(DatasavPath2);
            }
            if (!Directory.Exists(DatasavPath2_Origin))
            {
                Directory.CreateDirectory(DatasavPath2_Origin);
            }
            if (!Directory.Exists(DatasavPath2_Correct))
            {
                Directory.CreateDirectory(DatasavPath2_Correct);
            }
            string Savefilepath;
            if (extraDir == 1)
                Savefilepath = DatasavPath2_Origin + "\\" + dt.ToString("yyyyMMdd-HHmmss") + "_" + note; // 加在檔名最後面
            else if (extraDir == 2)
                Savefilepath = DatasavPath2_Correct + "\\" + dt.ToString("yyyyMMdd-HHmmss") + "_" + note; // 加在檔名最後面
            else
                Savefilepath = DatasavPath2 + "\\" + dt.ToString("yyyyMMdd-HHmmss") + "_" + note; // 加在檔名最後面
            //ParamFile.SaveCsv(rawdata, Savefilepath);
            try
            {
                chart.BackColor = Color.White;
                chart.SaveImage(Savefilepath + ".png", ChartImageFormat.Png);
                chart.BackColor = Color.Transparent;

                AddText(sText1, sText2, Savefilepath, w1, w2, h1, h2);
            }
            catch (Exception ee)
            {
                InsertLog.SavetoDB(5, "存圖錯誤, " + ee.Message);
                //MessageBox.Show("數據儲存錯誤");
            }
        }

        public static void SaveImg_png(Image img, string LotID, string note, DateTime dt, int extraDir = 0)
        {
            string UIDate, Year, month, Date, Hour, minutes, Second, DatasavPath = "";
            DateTime _dt = dt;
            Year = string.Format("{0:yyyy}", _dt);
            month = string.Format("{0:MM}", _dt);
            Date = string.Format("{0:dd}", _dt);
            Hour = string.Format("{0:HH}", _dt);
            minutes = string.Format("{0:mm}", _dt);
            Second = string.Format("{0:ss}", _dt);
            UIDate = string.Format("{0:yyyy-MM-dd-HH:mm:ss}", _dt);
            DatasavPath = sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\Baseline\\" + Year + "\\" + month + Date;
            //logsavPath = ParamFile.dirname + "\\Log\\" + Year + "\\" + month + Date;
            //if (Directory.Exists(sram.dirfilepath + "\\Baseline\\" + Year))//***
            string DatasavPath2 = DatasavPath + "\\" + LotID;
            string DatasavPath2_Origin = DatasavPath2 + "\\" + "Origin";
            string DatasavPath2_Correct = DatasavPath2 + "\\" + "Correct";
            if (!Directory.Exists(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year))//***
            {//無年資料夾
                Directory.CreateDirectory(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year);
            }
            if (!Directory.Exists(DatasavPath))
            {//無月日資料夾
                Directory.CreateDirectory(DatasavPath);
            }
            if (!Directory.Exists(DatasavPath2))
            {//無FoupID資料夾
                Directory.CreateDirectory(DatasavPath2);
            }
            if (!Directory.Exists(DatasavPath2_Origin))
            {
                Directory.CreateDirectory(DatasavPath2_Origin);
            }
            if (!Directory.Exists(DatasavPath2_Correct))
            {
                Directory.CreateDirectory(DatasavPath2_Correct);
            }
            string Savefilepath;
            if (extraDir == 1)
                Savefilepath = DatasavPath2_Origin + "\\" + dt.ToString("yyyyMMdd-HHmmss") + "_" + note; // 加在檔名最後面
            else if (extraDir == 2)
                Savefilepath = DatasavPath2_Correct + "\\" + dt.ToString("yyyyMMdd-HHmmss") + "_" + note; // 加在檔名最後面
            else
                Savefilepath = DatasavPath2 + "\\" + dt.ToString("yyyyMMdd-HHmmss") + "_" + note; // 加在檔名最後面
            //ParamFile.SaveCsv(rawdata, Savefilepath);
            try
            {
                img.Save(Savefilepath + ".png", ImageFormat.Png);
            }
            catch (Exception ee)
            {
                InsertLog.SavetoDB(5, "存圖錯誤, " + ee.Message);
                //MessageBox.Show("數據儲存錯誤");
            }
        }

        public static void SaveImg_jpg(Image img, string LotID, string note, DateTime dt, int extraDir = 0)
        {
            string UIDate, Year, month, Date, Hour, minutes, Second, DatasavPath = "";
            DateTime _dt = dt;
            Year = string.Format("{0:yyyy}", _dt);
            month = string.Format("{0:MM}", _dt);
            Date = string.Format("{0:dd}", _dt);
            Hour = string.Format("{0:HH}", _dt);
            minutes = string.Format("{0:mm}", _dt);
            Second = string.Format("{0:ss}", _dt);
            UIDate = string.Format("{0:yyyy-MM-dd-HH:mm:ss}", _dt);
            DatasavPath = sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\RawData\\" + Year + "\\" + month + Date;
            //DatasavPath = sram.dirfilepath + "\\Baseline\\" + Year + "\\" + month + Date;
            //logsavPath = ParamFile.dirname + "\\Log\\" + Year + "\\" + month + Date;
            //if (Directory.Exists(sram.dirfilepath + "\\Baseline\\" + Year))//***
            string DatasavPath2 = DatasavPath + "\\" + LotID;
            string DatasavPath2_Origin = DatasavPath2 + "\\" + "Origin";
            string DatasavPath2_Correct = DatasavPath2 + "\\" + "Correct";
            if (!Directory.Exists(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year))//***
            {//無年資料夾
                Directory.CreateDirectory(sram.Rootfilepath + "\\DataDirectory\\RawData\\" + Year);
            }
            if (!Directory.Exists(DatasavPath))
            {//無月日資料夾
                Directory.CreateDirectory(DatasavPath);
            }
            if (!Directory.Exists(DatasavPath2))
            {//無FoupID資料夾
                Directory.CreateDirectory(DatasavPath2);
            }
            if (!Directory.Exists(DatasavPath2_Origin))
            {
                Directory.CreateDirectory(DatasavPath2_Origin);
            }
            if (!Directory.Exists(DatasavPath2_Correct))
            {
                Directory.CreateDirectory(DatasavPath2_Correct);
            }
            string Savefilepath;
            if (extraDir == 1)
                Savefilepath = DatasavPath2_Origin + "\\" + dt.ToString("yyyyMMdd-HHmmss") + "_" + note; // 加在檔名最後面
            else if (extraDir == 2)
                Savefilepath = DatasavPath2_Correct + "\\" + dt.ToString("yyyyMMdd-HHmmss") + "_" + note; // 加在檔名最後面
            else
                Savefilepath = DatasavPath2 + "\\" + dt.ToString("yyyyMMdd-HHmmss") + "_" + note; // 加在檔名最後面
            //ParamFile.SaveCsv(rawdata, Savefilepath);
            try
            {
                img.Save(Savefilepath + ".jpg", ImageFormat.Jpeg);
            }
            catch (Exception ee)
            {
                InsertLog.SavetoDB(5, "存圖錯誤, " + ee.Message);
                //MessageBox.Show("數據儲存錯誤");
            }
        }

        public static double mapping(double value, double Vin_min, double Vin_max, double Vout_min, double Vout_max)
        {
            if (value < Vin_min)
            {
                return 0;
            }
            else
            {
                return ((value - Vin_min) * (Vout_max - Vout_min) / (Vin_max - Vin_min)) + Vout_min;
            }
        }

        public static void AddText(string sText1, string sText2, string sPath, double w1, double w2, double h1, double h2)
        {

            Image image = Image.FromFile(sPath + ".png");
            Bitmap bitmap = new Bitmap(image, image.Width, image.Height);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //字體大小
            float fontSize = 10.0f;
            //文本的長度
            float textWidth = sText1.Length * fontSize;
            //下面定義一個矩形區域，以後在這個矩形裏畫上白底黑字
            //float rectX = 360;
            //float rectY = 350;
            float rectX = 100;
            float rectY = image.Height - 40; ;
            float rectWidth = Convert.ToUInt32(w1) * (fontSize + 40);
            float rectHeight = fontSize + 40;

            //float rectX2 = 300;
            //float rectY2 = 300;
            float rectX2 = 200;
            float rectY2 = image.Height - 40; ;
            float rectWidth2 = Convert.ToUInt32(w2) * (fontSize + 40);
            float rectHeight2 = fontSize + 40;

            float rectX3 = 300;
            float rectY3 = image.Height - 40; ;
            float rectWidth3 = Convert.ToUInt32(h1) * (fontSize + 40);
            float rectHeight3 = fontSize + 40;

            float rectX4 = 400;
            float rectY4 = image.Height - 40; ;
            float rectWidth4 = Convert.ToUInt32(h2) * (fontSize + 40);
            float rectHeight4 = fontSize + 40;
            //聲明矩形域
            RectangleF textArea = new RectangleF(rectX, rectY, rectWidth, rectHeight);
            RectangleF textArea2 = new RectangleF(rectX2, rectY2, rectWidth2, rectHeight2);
            RectangleF textArea3 = new RectangleF(rectX3, rectY3, rectWidth3, rectHeight3);
            RectangleF textArea4 = new RectangleF(rectX4, rectY4, rectWidth4, rectHeight4);
            //定義字體
            System.Drawing.Font font = new System.Drawing.Font("微軟雅黑", fontSize, System.Drawing.FontStyle.Bold);
            //font.Bold = true;
            //白筆刷，畫文字用
            Brush whiteBrush1 = new SolidBrush(System.Drawing.Color.Red);
            Brush whiteBrush2 = new SolidBrush(System.Drawing.Color.Black);
            //黑筆刷，畫背景用
            //Brush blackBrush = new SolidBrush(Color.Black);   
            //g.FillRectangle(blackBrush, rectX, rectY, rectWidth, rectHeight);
            g.DrawString("W1:" + w1.ToString(), font, whiteBrush1, textArea);
            g.DrawString("W2:" + w2.ToString(), font, whiteBrush2, textArea2);
            g.DrawString("H1:" + h1.ToString(), font, whiteBrush2, textArea3);
            g.DrawString("H2:" + h2.ToString(), font, whiteBrush1, textArea4);
            //輸出方法一：將文件生成並保存到C盤


            string path = sPath + "_text.png";
            bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            g.Dispose();
        }
    }
}