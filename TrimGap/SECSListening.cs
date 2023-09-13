using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;
using HirataEFEM;

namespace TrimGap
{
    internal class SECSListening
    {
        private static BackgroundWorker bWSECSListening = new BackgroundWorker();
        private static string ReadyToDo_CarrierID = "";
        private static string ReadyToDo_LoadPortID = "";

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
                Common.SecsgemForm.AccessModeAskData.Clear();
            }
            if (Common.SecsgemForm.bSECS_ChangeAccessMode_Recive)
            {
                AccessModeChange();
                Common.SecsgemForm.bSECS_ChangeAccessMode_Recive = false;
                Common.SecsgemForm.AccessModeChangeData.Clear();
            }
            if (Common.SecsgemForm.bWaitSECS_PP_SELECT) // 等secs
            {
                ChangeRecipe();
                Common.SecsgemForm.bWaitSECS_PP_SELECT = false;
                Common.SecsgemForm.ChangeRecipeData.Clear();
            }
            if (Common.SecsgemForm.bWaitSECS_MeasureCmd)
            {
                MeasureStart(); // 進去之後裡面會清flag
                Common.SecsgemForm.bWaitSECS_MeasureCmd = false;
                Common.SecsgemForm.MeasureStartData.Clear();
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
            if (!Common.EFEM.LoadPort1.Busy && !Common.EFEM.LoadPort2.Busy)
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
                    Common.SecsgemForm.MeasureStartData.CarrierID = ReadyToDo_CarrierID;
                    Common.SecsgemForm.MeasureStartData.LoadPortID = ReadyToDo_LoadPortID;
                    MeasureStart();
                }
            }
        }

        private static void AccessModeAsk()
        {
            if (Common.SecsgemForm.AccessModeAskData.LoadPortID == "1")
            {
                Common.CGWrapper.UpdateSV(TrimGap_EqpID.PortID, 1);
                Common.CGWrapper.UpdateSV(TrimGap_EqpID.AccessMode, fram.SECSPara.Loadport1_AccessMode);
                Common.CGWrapper.EventReportSend(TrimGap_EqpID.AccessModeAsk);
            }
            else if (Common.SecsgemForm.AccessModeAskData.LoadPortID == "2")
            {
                Common.CGWrapper.UpdateSV(TrimGap_EqpID.PortID, 2);
                Common.CGWrapper.UpdateSV(TrimGap_EqpID.AccessMode, fram.SECSPara.Loadport2_AccessMode);
                Common.CGWrapper.EventReportSend(TrimGap_EqpID.AccessModeAsk);
            }
        }

        private static void AccessModeChange()
        {
            if (Common.SecsgemForm.AccessModeChangeData.LoadPortID == "1")
            {
                if (Common.SecsgemForm.AccessMode == Mode.Auto.GetHashCode().ToString())
                {
                    Common.EFEM.E84.Reset(E84.E84_Num.E841);
                    Common.EFEM.E84.SetAuto(E84.E84_Num.E841);
                    fram.SECSPara.Loadport1_AccessMode = Mode.Auto.GetHashCode();
                    Common.CGWrapper.UpdateSV(TrimGap_EqpID.Loadport1_AccessMode, fram.SECSPara.Loadport1_AccessMode); // Auto
                }
                else if (Common.SecsgemForm.AccessMode == Mode.Manual.GetHashCode().ToString())
                {
                    Common.EFEM.E84.SetManual(E84.E84_Num.E841);
                    fram.SECSPara.Loadport1_AccessMode = Mode.Manual.GetHashCode();
                    Common.CGWrapper.UpdateSV(TrimGap_EqpID.Loadport1_AccessMode, fram.SECSPara.Loadport1_AccessMode); // Manual
                    fram.SECSPara.Loadport1_PortTransferState = PortTransferState.TransferBlocked.GetHashCode();
                    Common.CGWrapper.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, fram.SECSPara.Loadport1_PortTransferState);
                }
            }
            if (Common.SecsgemForm.AccessModeChangeData.LoadPortID == "2")
            {
                if (Common.SecsgemForm.AccessMode == Mode.Auto.GetHashCode().ToString())
                {
                    Common.EFEM.E84.Reset(E84.E84_Num.E842);
                    Common.EFEM.E84.SetAuto(E84.E84_Num.E842);
                    fram.SECSPara.Loadport2_AccessMode = Mode.Auto.GetHashCode();
                    Common.CGWrapper.UpdateSV(TrimGap_EqpID.Loadport2_AccessMode, fram.SECSPara.Loadport2_AccessMode); // Auto
                }
                else if (Common.SecsgemForm.AccessMode == Mode.Manual.GetHashCode().ToString())
                {
                    Common.EFEM.E84.SetManual(E84.E84_Num.E842);
                    fram.SECSPara.Loadport2_AccessMode = Mode.Manual.GetHashCode();
                    Common.CGWrapper.UpdateSV(TrimGap_EqpID.Loadport2_AccessMode, fram.SECSPara.Loadport2_AccessMode); // Manual
                    fram.SECSPara.Loadport2_PortTransferState = PortTransferState.TransferBlocked.GetHashCode();
                    Common.CGWrapper.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, fram.SECSPara.Loadport2_PortTransferState);
                }
            }
            Common.SecsgemForm.LoadPortID = "";
            Common.SecsgemForm.AccessMode = "";
        }

        private static void ChangeRecipe()
        {
            System.IO.FileInfo fileName;
            string filename2;
            if (Common.SecsgemForm.ChangeRecipeData.LoadPortID == "1")
            {
                foreach (string fname in System.IO.Directory.GetFiles(fram.Recipe.Path))
                {
                    fileName = new System.IO.FileInfo(fname);//取得完整檔名(含副檔名)
                    filename2 = fileName.ToString();
                    filename2 = System.IO.Path.GetFileNameWithoutExtension(filename2);
                    if (Common.SecsgemForm.RecipeID == filename2)
                    {
                        fram.Recipe.Filename_LP1 = Common.SecsgemForm.RecipeID;
                        Common.CGWrapper.UpdateSV(TrimGap_EqpID.Loadport1_RecipeID, fram.Recipe.Filename_LP1);
                    }
                }
            }
            else if (Common.SecsgemForm.ChangeRecipeData.LoadPortID == "2")
            {
                foreach (string fname in System.IO.Directory.GetFiles(fram.Recipe.Path))
                {
                    fileName = new System.IO.FileInfo(fname);//取得完整檔名(含副檔名)
                    filename2 = fileName.ToString();
                    filename2 = System.IO.Path.GetFileNameWithoutExtension(filename2);
                    if (Common.SecsgemForm.RecipeID == filename2)
                    {
                        fram.Recipe.Filename_LP2 = Common.SecsgemForm.RecipeID;

                        Common.CGWrapper.UpdateSV(TrimGap_EqpID.Loadport2_RecipeID, fram.Recipe.Filename_LP2);
                    }
                }
            }
            Common.SecsgemForm.RecipeID = "";
        }

        private static void MeasureStart()
        {
            if (Common.EFEM.LoadPort1.Busy || Common.EFEM.LoadPort2.Busy)
            {
                ReadyToDo_CarrierID = Common.SecsgemForm.MeasureStartData.CarrierID;
                ReadyToDo_LoadPortID = Common.SecsgemForm.MeasureStartData.LoadPortID;
                Common.CGWrapper.EventReportSend(TrimGap_EqpID.MachineRunning);
            }
            else // 都沒人在做才來切換
            {
                ReadyToDo_CarrierID = "";
                ReadyToDo_LoadPortID = "";
                string testRecipeslotmap = "";
                if (Common.SecsgemForm.MeasureStartData.CarrierID == "")
                {
                    if (Common.SecsgemForm.MeasureStartData.LoadPortID == "1")
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
                        Common.CGWrapper.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort1.FoupID);
                        Common.CGWrapper.UpdateSV(TrimGap_EqpID.PortID, 1);
                        Common.CGWrapper.EventReportSend(TrimGap_EqpID.ProcessStart);
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
                        Common.CGWrapper.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort2.FoupID);
                        Common.CGWrapper.UpdateSV(TrimGap_EqpID.PortID, 2);
                        Common.CGWrapper.EventReportSend(TrimGap_EqpID.ProcessStart);
                        Common.EFEM.LoadPort2.Busy = true;
                    }
                }
                else if (Common.SecsgemForm.MeasureStartData.LoadPortID == "1")
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
                    Common.CGWrapper.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort1.FoupID);
                    Common.CGWrapper.UpdateSV(TrimGap_EqpID.PortID, 1);
                    Common.CGWrapper.EventReportSend(TrimGap_EqpID.ProcessStart);
                    Common.EFEM.LoadPort1.Busy = true;
                }
                else if (Common.SecsgemForm.MeasureStartData.LoadPortID == "2")
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
                    Common.CGWrapper.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort2.FoupID);
                    Common.CGWrapper.UpdateSV(TrimGap_EqpID.PortID, 2);
                    Common.CGWrapper.EventReportSend(TrimGap_EqpID.ProcessStart);
                    Common.EFEM.LoadPort2.Busy = true;
                }
            }
            Common.SecsgemForm.CarrierID = "";
            Common.SecsgemForm.LoadPortID = "";
            Common.EFEM.IO.SignalTower(IO.LampState.AllOff);
            Common.EFEM.IO.SignalTower(IO.LampState.GreenOn);
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
                            Slot_Info += sram.Recipe.Angle[j * 2] + ",";
                        }
                    }

                    Slot_Info += 0 + ",";
                    Slot_Info += 0 + ",";
                    Slot_Info += 0 + ",";
                    Slot_Info += 0 + ",";
                }
                Common.CGWrapper.UpdateSV(TrimGap_EqpID.Slot1_Info + i, Slot_Info);
            }
        }
    }
}