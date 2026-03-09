using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;
using HirataEFEM;
using DemoFormDiaGemLib;

namespace TrimGap
{
    internal class SECSListening
    {
        private static BackgroundWorker bWSECSListening = new BackgroundWorker();
        private static string ReadyToDo_CarrierID = "";
        private static string ReadyToDo_LoadPortID = "";
        public static string err = string.Empty;

        public static void InitSECSListening()
        {
            bWSECSListening.DoWork += new DoWorkEventHandler(bWSECSListening_DoWork);
            bWSECSListening.ProgressChanged += new ProgressChangedEventHandler(bWSECSListening_ProgressChanged);

            //bWAutoRun.RunWorkerCompleted += new RunWorkerCompletedEventHandler(MotionAutoFocusWorker_RunWorkerCompleted);
            bWSECSListening.WorkerReportsProgress = true;
            if (!bWSECSListening.IsBusy)
            {
                bWSECSListening.RunWorkerAsync();
            }
        }

        private static void bWSECSListening_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    SpinWait.SpinUntil(() => false, 700);
                    Listening();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private static void bWSECSListening_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private static void Listening()
        {
            if (Common.SecsgemForm.bWaitSECS_ACCESSMODE_ASK)
            {
                AccessModeAsk();
                Common.SecsgemForm.bWaitSECS_ACCESSMODE_ASK = false;
                Common.SecsgemForm.SecsDataClear(SecsData.AccessModeAsk);
            }
            //if (Common.SecsgemForm.bSECS_ChangeAccessMode_Recive)
            //{
                //AccessModeChange();
                //Common.SecsgemForm.bSECS_ChangeAccessMode_Recive = false;
                //Common.SecsgemForm.SecsDataClear(SecsData.AccessModeChange);
           // }
            if (Common.SecsgemForm.bWaitSECS_PP_SELECT) // 等secs
            {
                ChangeRecipe();
                Common.SecsgemForm.bWaitSECS_PP_SELECT = false;
                Common.SecsgemForm.SecsDataClear(SecsData.ChangeRecipe);
            }
            if (Common.SecsgemForm.bWaitSECS_MeasureCmd)
            {
                MeasureStart(); // 進去之後裡面會清flag
                Common.SecsgemForm.bWaitSECS_MeasureCmd = false;
                Common.SecsgemForm.SecsDataClear(SecsData.MeasureStart);
            }
            if (Common.SecsgemForm.bWaitSECS_StartCmd)
            {
                Flag.AutoidleFlag = true;
                Common.SecsgemForm.bWaitSECS_StartCmd = false;
            }
            if (Common.SecsgemForm.bWaitSECS_StopCmd)
            {
                Flag.AutoidleFlag = false;
                Common.SecsgemForm.bWaitSECS_StopCmd = false;
            }
            if (fram.EFEMSts.Skip == 0 || fram.m_simulateRun == 1)
            {
                if (!Common.EFEM.LoadPort1.Busy && !Common.EFEM.LoadPort2.Busy && fram.m_SecsgemType == 0)
                {   // 同時有兩個LP，其中一個做完，兩個busy都是F
                    if (ReadyToDo_LoadPortID != "" || ReadyToDo_CarrierID != "")
                    {   // 看有沒有排隊中的指令
                        if (ReadyToDo_LoadPortID == "1" && !Common.EFEM.LoadPort1.Busy)
                        {
                            // 如果排隊中的指令是LP1，& LP1 沒有在做， 就往下觸發measurestart
                        }
                        else if (ReadyToDo_LoadPortID == "2" && !Common.EFEM.LoadPort2.Busy)
                        {
                            // 如果排隊中的指令是LP2，& LP2 沒有在做， 就往下觸發measurestart
                        }
                        else
                        {
                            return;
                        }
                        Common.SecsgemForm.SecsDataSet(SecsData.MeasureStart, SecsDataElement.CarrierID, ReadyToDo_CarrierID);
                        Common.SecsgemForm.SecsDataSet(SecsData.MeasureStart, SecsDataElement.LoadPortID, ReadyToDo_LoadPortID);
                        MeasureStart();
                    }
                }
                if (MainForm.CJ_list.Count >= 1 && fram.m_SecsgemType == 1)
                {
                    if (!(Common.EFEM.LoadPort1.Busy || Common.EFEM.LoadPort2.Busy))
                    {
                        string[] lotID, substrateID;
                        lotID = new string[25];
                        substrateID = new string[25];
                        int lpn = Common.SecsgemForm.LoadportMatchToRun(MainForm.CJ_list[0], ref lotID, ref substrateID);
                        if (lpn == 1 && sram.LoadPort1_Carrier_Vertify)
                        {
                            sram.RunningCJ = MainForm.CJ_list[0];
                            for (int i = 0; i < 25; i++)
                            {
                                sram.CarrierInfo.lotID[i] = lotID[i];
                                sram.CarrierInfo.substrateID[i] = substrateID[i];
                                Common.EFEM.LoadPort1.LotID[i] = lotID[i];
                                Common.EFEM.LoadPort1.SubstrateID[i] = substrateID[i];
                            }
                            CJStart(lpn);
                        }
                        else if (lpn == 2 && sram.LoadPort2_Carrier_Vertify)
                        {
                            sram.RunningCJ = MainForm.CJ_list[0];
                            for (int i = 0; i < 25; i++)
                            {
                                sram.CarrierInfo.lotID[i] = lotID[i];
                                sram.CarrierInfo.substrateID[i] = substrateID[i];
                                Common.EFEM.LoadPort2.LotID[i] = lotID[i];
                                Common.EFEM.LoadPort2.SubstrateID[i] = substrateID[i];
                            }
                            CJStart(lpn);
                        }
                    }
                }
            }
        }

        private static void AccessModeAsk()
        {
            if (Common.SecsgemForm.SecsDataGet(SecsData.AccessModeAsk, SecsDataElement.LoadPortID) == "1")
            {
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.AccessMode, (byte)fram.SECSPara.Loadport1_AccessMode, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.AccessModeAsk, out err);
            }
            else if (Common.SecsgemForm.SecsDataGet(SecsData.AccessModeAsk, SecsDataElement.LoadPortID) == "2")
            {
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.AccessMode, (byte)fram.SECSPara.Loadport2_AccessMode, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.AccessModeAsk, out err);
            }
        }

        private static void AccessModeChange()
        {
            if (Common.SecsgemForm.SecsDataGet(SecsData.AccessModeChange, SecsDataElement.LoadPortID) == "1")
            {
                if (Common.SecsgemForm.AccessMode == Mode.Auto.GetHashCode().ToString())
                {
                    Common.EFEM.E84.Reset(E84.E84_Num.E841);
                    Common.EFEM.E84.SetAuto(E84.E84_Num.E841);
                    fram.SECSPara.Loadport1_AccessMode = Mode.Auto.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_AccessMode, (byte)fram.SECSPara.Loadport1_AccessMode, out err); // Auto
                }
                else if (Common.SecsgemForm.AccessMode == Mode.Manual.GetHashCode().ToString())
                {
                    Common.EFEM.E84.SetManual(E84.E84_Num.E841);
                    fram.SECSPara.Loadport1_AccessMode = Mode.Manual.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_AccessMode, (byte)fram.SECSPara.Loadport1_AccessMode, out err); // Manual
                    fram.SECSPara.Loadport1_PortTransferState = PortTransferState.TransferBlocked.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err);
                }
            }
            if (Common.SecsgemForm.SecsDataGet(SecsData.AccessModeChange, SecsDataElement.LoadPortID) == "2")
            {
                if (Common.SecsgemForm.AccessMode == Mode.Auto.GetHashCode().ToString())
                {
                    Common.EFEM.E84.Reset(E84.E84_Num.E842);
                    Common.EFEM.E84.SetAuto(E84.E84_Num.E842);
                    fram.SECSPara.Loadport2_AccessMode = Mode.Auto.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_AccessMode, (byte)fram.SECSPara.Loadport2_AccessMode, out err); // Auto
                }
                else if (Common.SecsgemForm.AccessMode == Mode.Manual.GetHashCode().ToString())
                {
                    Common.EFEM.E84.SetManual(E84.E84_Num.E842);
                    fram.SECSPara.Loadport2_AccessMode = Mode.Manual.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_AccessMode, (byte)fram.SECSPara.Loadport2_AccessMode, out err); // Manual
                    fram.SECSPara.Loadport2_PortTransferState = PortTransferState.TransferBlocked.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err);
                }
            }
            Common.SecsgemForm.LoadPortID = "";
            Common.SecsgemForm.AccessMode = "";
        }

        private static void ChangeRecipe()
        {
            System.IO.FileInfo fileName;
            string filename2;
            if (Common.SecsgemForm.SecsDataGet(SecsData.ChangeRecipe, SecsDataElement.LoadPortID) == "1")
            {
                foreach (string fname in System.IO.Directory.GetFiles(fram.Recipe.Path))
                {
                    fileName = new System.IO.FileInfo(fname);//取得完整檔名(含副檔名)
                    filename2 = fileName.ToString();
                    filename2 = System.IO.Path.GetFileNameWithoutExtension(filename2);
                    if (Common.SecsgemForm.RecipeID == filename2)
                    {
                        fram.Recipe.Filename_LP1 = Common.SecsgemForm.RecipeID;
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_RecipeID, fram.Recipe.Filename_LP1, out err);
                    }
                }
            }
            else if (Common.SecsgemForm.SecsDataGet(SecsData.ChangeRecipe, SecsDataElement.LoadPortID) == "2")
            {
                foreach (string fname in System.IO.Directory.GetFiles(fram.Recipe.Path))
                {
                    fileName = new System.IO.FileInfo(fname);//取得完整檔名(含副檔名)
                    filename2 = fileName.ToString();
                    filename2 = System.IO.Path.GetFileNameWithoutExtension(filename2);
                    if (Common.SecsgemForm.RecipeID == filename2)
                    {
                        fram.Recipe.Filename_LP2 = Common.SecsgemForm.RecipeID;

                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_RecipeID, fram.Recipe.Filename_LP2, out err);
                    }
                }
            }
            Common.SecsgemForm.RecipeID = "";
        }

        private static void MeasureStart()
        {
            if (Common.EFEM.LoadPort1.Busy || Common.EFEM.LoadPort2.Busy)
            {
                ReadyToDo_CarrierID = Common.SecsgemForm.SecsDataGet(SecsData.MeasureStart, SecsDataElement.CarrierID);
                ReadyToDo_LoadPortID = Common.SecsgemForm.SecsDataGet(SecsData.MeasureStart, SecsDataElement.LoadPortID);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MachineRunning, out err);
            }
            else // 都沒人在做才來切換
            {
                ReadyToDo_CarrierID = "";
                ReadyToDo_LoadPortID = "";
                string testRecipeslotmap = "";
                if (Common.SecsgemForm.SecsDataGet(SecsData.MeasureStart, SecsDataElement.CarrierID) == "")
                {
                    if (Common.SecsgemForm.SecsDataGet(SecsData.MeasureStart, SecsDataElement.LoadPortID) == "1")
                    {
                        Common.ChangeRecipe(fram.Recipe.Filename_LP1);
                        ClearReportdata();
                        for (int i = 0; i < 25; i++)
                        {
                            if (Common.EFEM.LoadPort1.slot_Status[i] == EFEM.slot_status.Ready)
                            {
                                if (sram.Recipe.Slot[i] == 0)
                                {
                                    Common.EFEM.LoadPort1.Update_slot_Status(i + 1, EFEM.slot_status.ProcessEnd);
                                }
                            }
                            testRecipeslotmap += Convert.ToString(sram.Recipe.Slot[i]); //測試
                        }
                        InsertLog.SavetoDB(67, "Recipeslotmap:" + testRecipeslotmap);
                        Common.EFEM.LoadPort1.AutoGetSlot();

                        Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort1;
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort1.FoupID, out err);
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                        Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ProcessStart, out err);
                        Common.EFEM.LoadPort1.Busy = true;
                    }
                    else
                    {
                        Common.ChangeRecipe(fram.Recipe.Filename_LP2);
                        ClearReportdata();
                        for (int i = 0; i < 25; i++)
                        {
                            if (Common.EFEM.LoadPort2.slot_Status[i] == EFEM.slot_status.Ready)
                            {
                                if (sram.Recipe.Slot[i] == 0)
                                {
                                    Common.EFEM.LoadPort2.Update_slot_Status(i + 1, EFEM.slot_status.ProcessEnd);
                                }
                            }
                            testRecipeslotmap += Convert.ToString(sram.Recipe.Slot[i]); //測試
                        }
                        InsertLog.SavetoDB(67, "Recipeslotmap:" + testRecipeslotmap);
                        Common.EFEM.LoadPort2.AutoGetSlot();

                        Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort2;
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort2.FoupID, out err);
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                        Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ProcessStart, out err);
                        Common.EFEM.LoadPort2.Busy = true;
                    }
                }
                else if (Common.SecsgemForm.SecsDataGet(SecsData.MeasureStart, SecsDataElement.LoadPortID) == "1")
                {
                    Common.ChangeRecipe(fram.Recipe.Filename_LP1);
                    ClearReportdata();
                    for (int i = 0; i < 25; i++)
                    {
                        if (Common.EFEM.LoadPort1.slot_Status[i] == EFEM.slot_status.Ready)
                        {
                            if (sram.Recipe.Slot[i] == 0)
                            {
                                Common.EFEM.LoadPort1.Update_slot_Status(i + 1, EFEM.slot_status.ProcessEnd);
                            }
                        }
                        testRecipeslotmap += Convert.ToString(sram.Recipe.Slot[i]); //測試
                    }
                    InsertLog.SavetoDB(67, "Recipeslotmap:" + testRecipeslotmap);
                    Common.EFEM.LoadPort1.AutoGetSlot();

                    Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort1;
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort1.FoupID, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ProcessStart, out err);
                    Common.EFEM.LoadPort1.Busy = true;
                }
                else if (Common.SecsgemForm.SecsDataGet(SecsData.MeasureStart, SecsDataElement.LoadPortID) == "2")
                {
                    Common.ChangeRecipe(fram.Recipe.Filename_LP2);
                    ClearReportdata();
                    for (int i = 0; i < 25; i++)
                    {
                        if (Common.EFEM.LoadPort2.slot_Status[i] == EFEM.slot_status.Ready)
                        {
                            if (sram.Recipe.Slot[i] == 0)
                            {
                                Common.EFEM.LoadPort2.Update_slot_Status(i + 1, EFEM.slot_status.ProcessEnd);
                            }
                        }
                        testRecipeslotmap += Convert.ToString(sram.Recipe.Slot[i]); //測試
                    }
                    InsertLog.SavetoDB(67, "Recipeslotmap:" + testRecipeslotmap);
                    Common.EFEM.LoadPort2.AutoGetSlot();

                    Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort2;
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort2.FoupID, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ProcessStart, out err);
                    Common.EFEM.LoadPort2.Busy = true;
                }
                //20240916 搬進來以免亂開綠燈
                //Common.SecsgemForm.CarrierID = "";
                //Common.SecsgemForm.LoadPortID = "";
                //Common.EFEM.IO.SignalTower(IO.LampState.AllOff);
                //Common.EFEM.IO.SignalTower(IO.LampState.GreenOn);
                Flag.GreenLightFlag = true;
            }
        }

        private static void CJStart(int lpn)
        {
            //Common.EFEM.IO.SignalTower(IO.LampState.AllOff);
            //Common.EFEM.IO.SignalTower(IO.LampState.GreenOn);
            Common.SecsgemForm.GetControlJobAttr(sram.RunningCJ, out sram.CJInfo.carrierInputSpec, out sram.CJInfo.curPJ, out sram.CJInfo.dataCollection, out sram.CJInfo.mtrloutStatus,
                out sram.CJInfo.mtrloutSpec, out sram.CJInfo.pauseEvent, out sram.CJInfo.procCtrlSpec, out sram.CJInfo.procOrder, out sram.CJInfo.bStart, out sram.CJInfo.state, out err);
            string[] pjl = sram.CJInfo.procCtrlSpec.Split(';');
            sram.QueuePJ = new List<string>();
            foreach (string s in pjl)
            {
                if((s != null) && (s != ""))
                    sram.QueuePJ.Add(s);
            }
            ClearReportdata();
            if (lpn == 1)
            {
                Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort1;
                Common.EFEM.LoadPort1.Busy = true;
                Common.SecsgemForm.ChangeControlJobState(sram.RunningCJ, ControlJobState.EXECUTING, 0);
                Common.SecsgemForm.SetCarrierStatus_Accessing(Common.EFEM.LoadPort_Run.FoupID, CarrierAccessingState.IN_ACCESS);
            }
            else if(lpn == 2)
            {
                Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort2;
                Common.EFEM.LoadPort2.Busy = true;
                Common.SecsgemForm.ChangeControlJobState(sram.RunningCJ, ControlJobState.EXECUTING, 0);
                Common.SecsgemForm.SetCarrierStatus_Accessing(Common.EFEM.LoadPort_Run.FoupID, CarrierAccessingState.IN_ACCESS);
            }
            Flag.GreenLightFlag = true;
        }

        public static void ClearReportdata()
        {
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
                            Slot_Info += "0,";
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
                            Slot_Info += "0,";
                        }
                        else
                        {
                            Slot_Info += sram.Recipe.Angle[j] + ",";
                        }
                    }

                    Slot_Info += 0 + ",";
                    Slot_Info += 0 + ",";
                    Slot_Info += 0 + ",";
                    Slot_Info += 0 + ",";
                }
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Slot1_Info + i, Slot_Info, out err);
            }

            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    fram.EFEMSts.H1[i, j] = 0;
                    fram.EFEMSts.W1[i, j] = 0;
                    fram.EFEMSts.H2[i, j] = 0;
                    fram.EFEMSts.W2[i, j] = 0;
                }
            }
        }

        public static byte FunctionCommand_CallBack(List<string> dataList) 
        {
            byte rtn = 0;
            switch (dataList[0])
            {
                case "ACCESSMODE-CHANGE":
                    if (dataList[2] == "1")  //loarport
                    {
                        if(Common.EFEM.LoadPort1.Busy)
                            rtn = 5;
                        else if(dataList[1] == "1")   //accessmode
                        {
                            Common.EFEM.E84.Reset(E84.E84_Num.E841);
                            Common.EFEM.E84.SetAuto(E84.E84_Num.E841);
                            fram.SECSPara.Loadport1_AccessMode = Mode.Auto.GetHashCode();
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_AccessMode, (byte)fram.SECSPara.Loadport1_AccessMode, out err); // Auto
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.AccessMode, (byte)fram.SECSPara.Loadport1_AccessMode, out err);
                            Common.SecsgemForm.EventReportSend(TrimGap_EqpID.AccessModeAuto, out err);
                        }
                        else if (dataList[1] == "0")
                        {
                            Common.EFEM.E84.SetManual(E84.E84_Num.E841);
                            fram.SECSPara.Loadport1_AccessMode = Mode.Manual.GetHashCode();
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_AccessMode, (byte)fram.SECSPara.Loadport1_AccessMode, out err); // Manual
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.AccessMode, (byte)fram.SECSPara.Loadport1_AccessMode, out err);
                            Common.SecsgemForm.EventReportSend(TrimGap_EqpID.AccessModeAuto, out err);
                            fram.SECSPara.Loadport1_PortTransferState = PortTransferState.TransferBlocked.GetHashCode();
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err);
                        }
                    }
                    else if (dataList[2] == "2")  //loarport
                    {
                        if (Common.EFEM.LoadPort2.Busy)
                            rtn = 5;
                        else if (dataList[1] == "1")   //accessmode
                        {
                            Common.EFEM.E84.Reset(E84.E84_Num.E842);
                            Common.EFEM.E84.SetAuto(E84.E84_Num.E842);
                            fram.SECSPara.Loadport2_AccessMode = Mode.Auto.GetHashCode();
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_AccessMode, (byte)fram.SECSPara.Loadport2_AccessMode, out err); // Auto
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.AccessMode, (byte)fram.SECSPara.Loadport2_AccessMode, out err);
                            Common.SecsgemForm.EventReportSend(TrimGap_EqpID.AccessModeAuto, out err);
                        }
                        else if (dataList[1] == "0")
                        {
                            Common.EFEM.E84.SetManual(E84.E84_Num.E842);
                            fram.SECSPara.Loadport2_AccessMode = Mode.Manual.GetHashCode();
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_AccessMode, (byte)fram.SECSPara.Loadport2_AccessMode, out err); // Manual
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.AccessMode, (byte)fram.SECSPara.Loadport2_AccessMode, out err);
                            Common.SecsgemForm.EventReportSend(TrimGap_EqpID.AccessModeAuto, out err);
                            fram.SECSPara.Loadport2_PortTransferState = PortTransferState.TransferBlocked.GetHashCode();
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err);
                        }
                    }
                    break;
                case "PROCEEDWITHCARRIER":
                    if (dataList[2] == "1")  //loarport
                    {
                        Common.SecsgemForm.SetCarrierStatus_ID(dataList[1], CarrierIDState.ID_VERIFICATION_OK);
                        if (Common.EFEM.LoadPort1.Busy)
                            rtn = 5;
                        if(MainForm.Carrier_list.Contains(dataList[1]))
                        {
                            if(dataList[3] == "1")
                                Common.SecsgemForm.bWaitSECS_SlotMapCmd = true;
                            else if(dataList[3] == "2")
                            {
                                sram.LoadPort1_Carrier_Vertify = true;
                            }                               
                        }       
                    }
                    else if(dataList[2] == "2")  //loarport
                    {
                        Common.SecsgemForm.SetCarrierStatus_ID(dataList[1], CarrierIDState.ID_VERIFICATION_OK);
                        if (Common.EFEM.LoadPort2.Busy)
                            rtn = 5;
                        if (MainForm.Carrier_list.Contains(dataList[1]))
                        {
                            if (dataList[3] == "1")
                                Common.SecsgemForm.bWaitSECS_SlotMapCmd = true;
                            else if (dataList[3] == "2")
                            {
                                sram.LoadPort2_Carrier_Vertify = true;
                            }                            
                        }
                    }
                    break;
                case "CARRIERRELEASE":
                    if (dataList[2] == "1")  //loarport
                    {
                        if (Common.EFEM.LoadPort1.Busy)
                            rtn = 5;
                        else if (MainForm.Carrier_list.Contains(dataList[1]))
                        {
                            Common.SecsgemForm.bWaitSECS_ReleaseCmd = true;
                        }
                    }
                    else if (dataList[2] == "2")  //loarport
                    {
                        if (Common.EFEM.LoadPort2.Busy)
                            rtn = 5;
                        else if (MainForm.Carrier_list.Contains(dataList[1]))
                        {
                            Common.SecsgemForm.bWaitSECS_ReleaseCmd = true;
                        }
                    }
                    break;
                case "CANCELCARRIER":
                    if (dataList[1] == Common.EFEM.LoadPort1.FoupID)  //loarport
                    {
                        if (Common.EFEM.LoadPort2.Busy)
                            rtn = 5;
                        else 
                        {
                            Flag.AbortFlag = true;
                            Common.EFEM.LoadPort1.Busy = true;
                        }
                    }
                    else if (dataList[1] == Common.EFEM.LoadPort2.FoupID)  //loarport
                    {
                        if (Common.EFEM.LoadPort1.Busy)
                            rtn = 5;
                        else
                        {                         
                            Flag.AbortFlag = true;
                            Common.EFEM.LoadPort2.Busy = true;
                        }
                    }
                    break;
                case "CANCELCARRIERATPORT":
                    if (dataList[2] == "1")  //loarport
                    {
                        if (Common.EFEM.LoadPort2.Busy)
                            rtn = 5;
                        else 
                        {                           
                            Flag.AbortFlag = true;
                            Common.EFEM.LoadPort1.Busy = true;
                        }
                    }
                    else if (dataList[2] == "2")  //loarport
                    {
                        if (Common.EFEM.LoadPort1.Busy)
                            rtn = 5;
                        else
                        {
                            Flag.AbortFlag = true;
                            Common.EFEM.LoadPort2.Busy = true;
                        }
                    }
                    break;
                case "PROCESSJOBCOMMAND":
                    if(dataList[1] == "CANCEL")
                    {
                        if (sram.RunningPJ == dataList[2])
                            rtn = 1;  //PJ is running
                        else
                        {
                            Common.SecsgemForm.ChangeProcessJobState(dataList[2], ProcessJobState.PROCESS_COMPLETE);
                        }
                    }
                    else if(dataList[1] == "ABORT")
                    {
                        if (sram.RunningPJ == dataList[2])
                        {
                            rtn = 0;  //PJ is running
                            // ⭐ 在 Aborting 事件上報前，先計算完成數以供 DVID 4160 使用
                            byte doneWaferCount = 0;
                            if (Common.EFEM.LoadPort_Run != null)
                            {
                                for (int si = 0; si < Common.EFEM.LoadPort_Run.slot_Status.Length; si++)
                                    if (Common.EFEM.LoadPort_Run.slot_Status[si] == EFEM.slot_status.ProcessEnd)
                                        doneWaferCount++;
                            }
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PJProcessedWaferCount, doneWaferCount, out string err);

                            Common.SecsgemForm.ChangeProcessJobState(dataList[2], ProcessJobState.ABORTING);
                            Flag.PauseFlag = true;
                            Flag.AutoidleFlag = false;
                            Flag.Autoidle_LocalFlag = false;
                        }
                        else
                        {
                            if (sram.QueuePJ.Contains(dataList[2]))
                            {
                                rtn = 0;
                                Common.SecsgemForm.ChangeProcessJobState(dataList[2], ProcessJobState.PROCESS_COMPLETE);
                            }
                            else
                            {
                                rtn = 2;
                            }
                        }
                    }
                    else if (dataList[1] == "STOP")
                    {
                        if (sram.RunningPJ == dataList[2])
                        {
                            rtn = 0;  //PJ is running
                            Common.SecsgemForm.ChangeProcessJobState(dataList[2], ProcessJobState.STOPPING);
                            Flag.PauseFlag = true;
                            Flag.AutoidleFlag = false;
                            Flag.Autoidle_LocalFlag = false;
                        }
                        else
                        {
                            if (sram.QueuePJ.Contains(dataList[2]))
                            {
                                rtn = 0;
                                Common.SecsgemForm.ChangeProcessJobState(dataList[2], ProcessJobState.PROCESS_COMPLETE);
                            }
                            else
                            {
                                rtn = 2;
                            }
                        }
                    }
                    else if (dataList[1] == "PAUSE")
                    {
                        //有PJ在run
                        if (sram.RunningPJ == dataList[2] && Flag.AutoidleFlag && dataList[3] == ProcessJobState.PROCESSING.ToString())
                        {
                            rtn = 0;
                            Flag.PauseFlag = true;
                            Flag.AutoidleFlag = false;
                            Flag.Autoidle_LocalFlag = false;

                            Common.SecsgemForm.ChangeProcessJobState(dataList[2], ProcessJobState.PAUSING);
                        }
                        else
                            rtn = 3;
                    }
                    else if (dataList[1] == "RESUME")
                    {
                        //有PJ在run且Pause
                        if (sram.RunningPJ == dataList[2] && !Flag.AutoidleFlag && Flag.PauseFlag && dataList[3] == ProcessJobState.PAUSED.ToString())
                        {
                            rtn = 0;
                            Flag.PauseFlag = false;
                            Common.SecsgemForm.ChangeProcessJobState(dataList[2], ProcessJobState.PROCESSING);
                        }
                        else
                            rtn = 4;
                    }
                    else if (dataList[1] == "START")
                    {
                        if (dataList[3] == ProcessJobState.WAITING_FOR_START.ToString())
                        {
                            Common.SecsgemForm.ChangeProcessJobState(dataList[2], ProcessJobState.PROCESSING);
                            Flag.PauseFlag = false;
                        }
                        rtn = 0;
                    }
                    break;
                case "CONTROLJOBCOMMAND":
                    if (dataList[1] == "CJSTART")
                    {
                        rtn = 0;
                    }
                    else if (dataList[1] == "CJPAUSE")
                    {
                        //有CJ在run
                        if (sram.RunningCJ == dataList[2] && Flag.AutoidleFlag && dataList[4] == ControlJobState.EXECUTING.ToString())
                        {
                            rtn = 0;
                            Flag.PauseFlag = true;
                            Flag.AutoidleFlag = false;
                            Flag.Autoidle_LocalFlag = false;

                            Common.SecsgemForm.ChangeControlJobState(dataList[2], ControlJobState.PAUSED, 0);
                        }
                        else
                            rtn = 3;
                    }
                    else if (dataList[1] == "CJRESUME")
                    {
                        //有CJ在run且Pause
                        if (sram.RunningCJ == dataList[2] && !Flag.AutoidleFlag && Flag.PauseFlag && dataList[4] == ControlJobState.PAUSED.ToString())
                        {
                            rtn = 0;
                            Flag.PauseFlag = false;
                            Common.SecsgemForm.ChangeControlJobState(dataList[2], ControlJobState.EXECUTING, 0);
                        }
                        else
                            rtn = 4;
                    }
                    else if (dataList[1] == "CJCANCEL")
                    {
                        if (sram.RunningCJ == dataList[2])
                        {
                            rtn = 2;
                        }
                        else
                        {
                            rtn = 0;
                            if (dataList[3] == "1")
                            {
                                Flag.PJDeleteWithCJFlag = true;
                            }
                            else
                            {
                                Flag.PJDeleteWithCJFlag = false;
                            }
                        }
                    }
                    else if (dataList[1] == "CJDESELECT")
                    {
                        if (sram.RunningCJ == "" || sram.RunningCJ == null)
                            rtn = 0;
                        else
                            rtn = 5;
                    }
                    else if (dataList[1] == "CJSTOP") //正常停 Wafer製程要做好之後可以繼續
                    {
                        if (sram.RunningCJ == dataList[2])
                        {
                            rtn = 0;  //CJ is running
                            Flag.CJStopFlag = true;
                            if (dataList[4] == ControlJobState.PAUSED.ToString()) //暫停中接受到STOP，讓他恢復動作才能收片
                            {
                                Flag.PauseFlag = false;
                                Common.SecsgemForm.ChangeControlJobState(dataList[2], ControlJobState.EXECUTING, 0);
                            }

                            if (dataList[3] == "1")
                            {
                                Flag.PJDeleteWithCJFlag = true;
                                Flag.PJStopFlag = true; // ⭐ 同步中斷正在跑的 PJ
                            }
                            else
                            {
                                Flag.PJDeleteWithCJFlag = false;
                            }
                        }
                        else
                        {
                            rtn = 0;
                        }
                    }
                    else if (dataList[1] == "CJABORT") //緊急停 Wafer可能會因中斷而報廢(可能類似急停) //先暫時當成跟STOP相同
                    {
                        if (sram.RunningCJ == dataList[2])
                        {
                            rtn = 0;  //CJ is running
                            Flag.CJAbortFlag = true;
                            if (dataList[4] == ControlJobState.PAUSED.ToString())//暫停中接受到ABORT，讓他恢復動作才能收片
                            {
                                Flag.PauseFlag = false;
                                Common.SecsgemForm.ChangeControlJobState(dataList[2], ControlJobState.EXECUTING, 0);
                            }

                            if (dataList[3] == "1")
                            {
                                Flag.PJDeleteWithCJFlag = true;
                                Flag.PJAbortFlag = true; // ⭐ 同步中斷正在跑的 PJ
                            }
                            else
                            {
                                Flag.PJDeleteWithCJFlag = false;
                            }
                        }
                        else
                        {
                            rtn = 0;
                        }
                    }
                    else if (dataList[1] == "CJHOQ")
                    {
                        if (sram.RunningCJ == "" || sram.RunningCJ == null)
                            rtn = 0;
                        else
                            rtn = 5;
                    }
                    break;
            }
            return rtn;
        }

        public static byte RemoteCommand_CallBack(List<string> dataList) //S2F41
        {
            byte rtn = 0;
            switch(dataList[0])
            {
                case "ACCESSMODE-ASK":
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)Convert.ToByte(dataList[1]), out err);
                    if (dataList[1] == "1")
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.AccessMode, (byte)fram.SECSPara.Loadport1_AccessMode, out err);
                    else if (dataList[1] == "2")
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.AccessMode, (byte)fram.SECSPara.Loadport2_AccessMode, out err);
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.AccessModeAsk, out err);
                    break;
            }
            return rtn;
        }
    }
}