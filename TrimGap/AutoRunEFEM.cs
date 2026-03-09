using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DemoFormDiaGemLib;

namespace TrimGap
{
    internal class AutoRunEFEM
    {
        public static EFEMStep AutoRunEFEMStep1;
        public static EFEMStep AutoRunEFEMStep2;
        public static EFEMStep AutoRunEFEMStepBack1;
        public static EFEMStep AutoRunEFEMStepBack2;
        public static bool EFEMCmd = false; // True:不能下指令， False:可以下指令
        public static string HomeAllFailStr = "";
        private static MachineType machineType; //0:AP6  1:N2
        public static string err = string.Empty;
        public static List<int> listRelease = new List<int>();
        public static int ReleaseFlag = 0;
        /// <summary>
        /// Camera 是否已成功初始化，供 AutoRunStage Bluetape/CCD 量測前檢查
        /// 模擬模式下若 Camera 無法初始化則為 false，量測流程應跳過 CCD
        /// </summary>
        public static bool CameraReady = false;
        /// <summary>
        /// CJ 被 Stop/Abort 時設為 true，避免 Finish 步驟將 CARRIER_STOPPED 覆蓋為 CARRIER_COMPLETE
        /// </summary>
        private static bool _carrierAccessingAlreadySet = false;

        public enum MachineType
        {
            AP6 = 0,
            N2 = 1,
            AP6II = 2,
        }

        public enum EFEMStep : int
        {
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

            [Description("Finish")]
            Finish,
        }

        private static void EFEMGotoErrorCheckStep()
        {
            if (Common.EFEM.CmdSend != "")
            {
                Common.EFEM.CmdSend_Clear();
            }
            Flag.AutoidleFlag = false;
            Flag.Autoidle_LocalFlag = false;
            Flag.AlarmFlag = true;
            bWEFEMAutoRun.ReportProgress(99);
        }

        public static void Init_EFEMSts()
        {
            foreach (var i in Enum.GetValues(typeof(EFEMStep)))
            {
                if (fram.EFEMSts.StepBack1 == i.ToString())
                {
                    AutoRunEFEMStep1 = ((EFEMStep)i);
                }
                if (fram.EFEMSts.StepBack2 == i.ToString())
                {
                    AutoRunEFEMStep2 = ((EFEMStep)i);
                }
            }
            if (fram.EFEMSts.LoadPortRun == "P1")
            {
                Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort1;
            }
            else if (fram.EFEMSts.LoadPortRun == "P2")
            {
                Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort2;
            }
            if (Common.EFEM.LoadPort_Run != null)
            {
                Common.EFEM.Aligner.Slot_pn = Common.EFEM.LoadPort_Run;
                Common.EFEM.Stage1.Slot_pn = Common.EFEM.LoadPort_Run;
                Common.EFEM.Robot.Slot_pn = Common.EFEM.LoadPort_Run;
            }
            if (Common.io.In(IOName.In.StageWafer在席))
            {
                Common.EFEM.Stage1.Slot = fram.EFEMSts.Stage_Slot;
            }
            else
            {
                Common.EFEM.Stage1.Slot = 0;
            }

            //if (fram.EFEMSts.Stage_Slotpn == "P1")
            //{
            //    Common.EFEM.Stage1.Slot_pn = Common.EFEM.LoadPort1;
            //}
            //else if (fram.EFEMSts.Stage_Slotpn == "P2")
            //{
            //    Common.EFEM.Stage1.Slot_pn = Common.EFEM.LoadPort2;
            //}

            for (int i = 0; i < Common.EFEM.LoadPort1.Slot.Length; i++)
            {
                switch (fram.EFEMSts.Slot_Sts1[i])
                {
                    case nameof(EFEM.slot_status.Empty):
                        Common.EFEM.LoadPort1.slot_Status[i] = EFEM.slot_status.Empty;
                        break;

                    case nameof(EFEM.slot_status.Ready):
                        Common.EFEM.LoadPort1.slot_Status[i] = EFEM.slot_status.Ready;
                        break;

                    case nameof(EFEM.slot_status.Error):
                        Common.EFEM.LoadPort1.slot_Status[i] = EFEM.slot_status.Error;
                        break;

                    case nameof(EFEM.slot_status.Thickness):
                        Common.EFEM.LoadPort1.slot_Status[i] = EFEM.slot_status.Thickness;
                        break;

                    case nameof(EFEM.slot_status.Thin):
                        Common.EFEM.LoadPort1.slot_Status[i] = EFEM.slot_status.Thin;
                        break;

                    case nameof(EFEM.slot_status.ProcessingAligner1):
                        Common.EFEM.LoadPort1.slot_Status[i] = EFEM.slot_status.ProcessingAligner1;
                        break;

                    case nameof(EFEM.slot_status.ProcessingStage1):
                        Common.EFEM.LoadPort1.slot_Status[i] = EFEM.slot_status.ProcessingStage1;
                        break;

                    case nameof(EFEM.slot_status.ProcessEnd):
                        Common.EFEM.LoadPort1.slot_Status[i] = EFEM.slot_status.ProcessEnd;
                        break;

                    case nameof(EFEM.slot_status.Unknow):
                        Common.EFEM.LoadPort1.slot_Status[i] = EFEM.slot_status.Unknow;
                        break;

                    default:
                        break;
                }
            }
            for (int i = 0; i < Common.EFEM.LoadPort2.Slot.Length; i++)
            {
                switch (fram.EFEMSts.Slot_Sts2[i])
                {
                    case nameof(EFEM.slot_status.Empty):
                        Common.EFEM.LoadPort2.slot_Status[i] = EFEM.slot_status.Empty;
                        break;

                    case nameof(EFEM.slot_status.Ready):
                        Common.EFEM.LoadPort2.slot_Status[i] = EFEM.slot_status.Ready;
                        break;

                    case nameof(EFEM.slot_status.Error):
                        Common.EFEM.LoadPort2.slot_Status[i] = EFEM.slot_status.Error;
                        break;

                    case nameof(EFEM.slot_status.Thickness):
                        Common.EFEM.LoadPort2.slot_Status[i] = EFEM.slot_status.Thickness;
                        break;

                    case nameof(EFEM.slot_status.Thin):
                        Common.EFEM.LoadPort2.slot_Status[i] = EFEM.slot_status.Thin;
                        break;

                    case nameof(EFEM.slot_status.ProcessingAligner1):
                        Common.EFEM.LoadPort2.slot_Status[i] = EFEM.slot_status.ProcessingAligner1;
                        break;

                    case nameof(EFEM.slot_status.ProcessingStage1):
                        Common.EFEM.LoadPort2.slot_Status[i] = EFEM.slot_status.ProcessingStage1;
                        break;

                    case nameof(EFEM.slot_status.ProcessEnd):
                        Common.EFEM.LoadPort2.slot_Status[i] = EFEM.slot_status.ProcessEnd;
                        break;

                    case nameof(EFEM.slot_status.Unknow):
                        Common.EFEM.LoadPort2.slot_Status[i] = EFEM.slot_status.Unknow;
                        break;

                    default:
                        break;
                }
            }
            Common.EFEM.Robot.GetStatus();
            if (Common.EFEM.Robot.WaferPresence_Upper)
            {
                Common.EFEM.Robot.Slot_Arm_upper = fram.EFEMSts.Robot_Upper_Slot;
                if (fram.EFEMSts.Robot_Upper_Slot == 0)
                {
                    Common.EFEM.Robot.Update_slot_Status(Robot.ArmID.UpperArm, EFEM.slot_status.Unknow);
                }
                else
                {
                    Common.EFEM.Robot.Update_slot_Status(Robot.ArmID.UpperArm, Common.EFEM.LoadPort_Run.slot_Status[Common.EFEM.Robot.Slot_Arm_upper - 1]);
                }
            }
            else
            {
                Common.EFEM.Robot.Slot_Arm_upper = 0;
                fram.EFEMSts.Robot_Upper_Slot = 0;
            }
            if (Common.EFEM.Robot.WaferPresence_Lower)
            {
                Common.EFEM.Robot.Slot_Arm_lower = fram.EFEMSts.Robot_Lower_Slot;
                if (fram.EFEMSts.Robot_Lower_Slot == 0)
                {
                    Common.EFEM.Robot.Update_slot_Status(Robot.ArmID.LowerArm, EFEM.slot_status.Unknow);
                }
                else
                {
                    Common.EFEM.Robot.Update_slot_Status(Robot.ArmID.LowerArm, Common.EFEM.LoadPort_Run.slot_Status[Common.EFEM.Robot.Slot_Arm_lower - 1]);
                }
            }
            else
            {
                Common.EFEM.Robot.Slot_Arm_lower = 0;
                fram.EFEMSts.Robot_Lower_Slot = 0;
            }

            Common.EFEM.Aligner.GetStatus();
            if (Common.EFEM.Aligner.WaferPresence)
            {
                Common.EFEM.Aligner.Slot = fram.EFEMSts.Aligner_Slot;

                if (fram.EFEMSts.Aligner_Slotpn == "P1")
                {
                    Common.EFEM.Aligner.Slot_pn = Common.EFEM.LoadPort1;
                }
                else if (fram.EFEMSts.Aligner_Slotpn == "P2")
                {
                    Common.EFEM.Aligner.Slot_pn = Common.EFEM.LoadPort2;
                }
            }
            else
            {
                Common.EFEM.Aligner.Slot = 0;
                //Common.EFEM.Aligner.Update_Sts();
                fram.EFEMSts.Aligner_Slot = 0;
            }
        }

        private static BackgroundWorker bWEFEMAutoRun = new BackgroundWorker();

        public static void InitEFEMAutoRun(int Machine_Type)
        {
            bWEFEMAutoRun.DoWork += new DoWorkEventHandler(bWEFEMAutoRun_DoWork);
            bWEFEMAutoRun.ProgressChanged += new ProgressChangedEventHandler(bWEFEMAutoRun_ProgressChanged);
            //bWAutoRun.RunWorkerCompleted += new RunWorkerCompletedEventHandler(MotionAutoFocusWorker_RunWorkerCompleted);
            bWEFEMAutoRun.WorkerReportsProgress = true;
            switch (Machine_Type)
            {
                case (int)MachineType.AP6:
                    machineType = MachineType.AP6;
                    break;

                case (int)MachineType.N2:
                    machineType = MachineType.N2;
                    break;

                case (int)MachineType.AP6II:
                    machineType = MachineType.AP6II;
                    break;

                default:
                    break;
            }

            // 嘗試確認 Camera 初始化狀態，模擬模式下 Camera 硬體不存在可能會失敗
            if (fram.m_simulateRun != 0)
            {
                CameraReady = false;
                Console.WriteLine("Camera Init Skip (模擬模式)");
            }
            else
            {
                CameraReady = true;
            }

            if (fram.EFEMSts.Skip == 0)
            {
                Init_EFEMSts();
            }

            if (!bWEFEMAutoRun.IsBusy)
            {
                bWEFEMAutoRun.RunWorkerAsync();
            }
        }

        private static void bWEFEMAutoRun_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                try
                {
                    SpinWait.SpinUntil(() => false, 1000);
                    if (!Flag.AutoidleFlag && Flag.AllHomeFlag)
                    {
                        if (Common.SecsgemForm.isRemote())
                        {
                            if (Flag.PauseFlag)
                            {
                                if (fram.m_SecsgemType == 1)
                                {
                                    int rtn = Common.SecsgemForm.GetProcessJobAttr(sram.RunningPJ, out string pe, out byte state,
                                        out string carrierID, out byte[] slot, out byte PRType, out bool bStart, out byte recMethod,
                                        out string recID, out string rec, out err);
                                    if (state == (byte)ProcessJobState.PAUSING && rtn == 0)
                                    {
                                        rtn = Common.SecsgemForm.ChangeProcessJobState(sram.RunningPJ, ProcessJobState.PAUSED);
                                    }
                                    else if (state == (byte)ProcessJobState.ABORTING && rtn == 0)
                                    {
                                        Flag.PauseFlag = false;
                                        Flag.PJAbortFlag = true;
                                    }
                                    else if (state == (byte)ProcessJobState.STOPPING && rtn == 0)
                                    {
                                        Flag.PauseFlag = false;
                                        Flag.PJStopFlag = true;
                                    }
                                }
                            }
                            else
                            {
                                Flag.AutoidleFlag = true;
                                Flag.Autoidle_LocalFlag = false;
                            }
                        }

                    }

                    if (Flag.AutoidleFlag && Flag.AllHomeFlag)
                    {
                        SpinWait.SpinUntil(() => false, 50);
                        if (Common.SecsgemForm.isRemote()) // Remote
                        {
                            if (Flag.Autoidle_LocalFlag)
                            {
                                Flag.Autoidle_LocalFlag = false; //
                            }
                            //if (fram.m_simulateRun == 0) //模擬模式不跑不然會卡住20230908
                            //{
                                WatchMaterialRecive();
                                WatchMaterialRemove();
                            //}

                            #region slotmap cmd ->  load & slotmap  // 完成
                            // 【SimStep 3+4+5】Host 發 ProceedWithCarrier #1 → FoupLoad + SlotMap
                            if(DemoFormDiaGemLib.MainForm.tmp_pwc_step == "1")
                            {
                                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierDock, out err);
                                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierDoorOpened, out err);

                                DemoFormDiaGemLib.MainForm.tmp_pwc_step = "0";
                            }
                            #endregion

                            #region EFEMRun
                            // 【SimStep 7~10】EFEMRun 狀態機：CJ/PJ 管理 + 逐片 Wafer 搬運
                            if (Common.SecsgemForm.bWaitSECS_SlotMapCmd && !EFEMCmd) // false 才能下指令
                            {
                                if (Common.SecsgemForm.SecsDataGet(SecsData.SlotMap, SecsDataElement.CarrierID) == "")
                                {
                                    if (Common.SecsgemForm.SecsDataGet(SecsData.SlotMap, SecsDataElement.LoadPortID) == "1")
                                    {
                                        //已經用完了可以先清除secsgem傳遞用的中繼資料，不用等slotmap跑完，這樣可以繼續收下一次的slotmap指令
                                        Common.SecsgemForm.CarrierID = "";
                                        Common.SecsgemForm.LoadPortID = "";
                                        Common.SecsgemForm.bWaitSECS_SlotMapCmd = false;
                                        Common.SecsgemForm.SecsDataClear(SecsData.SlotMap);
                                        if (fram.SECSPara.Loadport1_AccessMode == Mode.Auto.GetHashCode())
                                        {
                                            FoupLoad(ref Common.EFEM.LoadPort1); // 要看Flag來切換 看是哪個foup
                                            SlotMap(ref Common.EFEM.LoadPort1);
                                            Common.EFEM.LoadPort1.Busy = false;
                                            AutoRunEFEMStep1 = EFEMStep.JudgeStep;
                                        }
                                    }
                                    else
                                    {
                                        //已經用完了可以先清除secsgem傳遞用的中繼資料，不用等slotmap跑完，這樣可以繼續收下一次的slotmap指令
                                        Common.SecsgemForm.CarrierID = "";
                                        Common.SecsgemForm.LoadPortID = "";
                                        Common.SecsgemForm.bWaitSECS_SlotMapCmd = false;
                                        Common.SecsgemForm.SecsDataClear(SecsData.SlotMap);
                                        if (fram.SECSPara.Loadport2_AccessMode == Mode.Auto.GetHashCode())
                                        {
                                            FoupLoad(ref Common.EFEM.LoadPort2); // 要看Flag來切換 看是哪個foup
                                            SlotMap(ref Common.EFEM.LoadPort2);
                                            Common.EFEM.LoadPort2.Busy = false;
                                            AutoRunEFEMStep2 = EFEMStep.JudgeStep;
                                        }
                                    }
                                }
                                else if (Common.SecsgemForm.SecsDataGet(SecsData.SlotMap, SecsDataElement.LoadPortID) == "1")
                                {
                                    if (Common.SecsgemForm.SecsDataGet(SecsData.SlotMap, SecsDataElement.CarrierID) == Common.EFEM.LoadPort1.FoupID && fram.SECSPara.Loadport1_AccessMode == Mode.Auto.GetHashCode())
                                    {
                                        //已經用完了可以先清除secsgem傳遞用的中繼資料，不用等slotmap跑完，這樣可以繼續收下一次的slotmap指令
                                        Common.SecsgemForm.CarrierID = "";
                                        Common.SecsgemForm.LoadPortID = "";
                                        Common.SecsgemForm.bWaitSECS_SlotMapCmd = false;
                                        Common.SecsgemForm.SecsDataClear(SecsData.SlotMap);

                                        FoupLoad(ref Common.EFEM.LoadPort1); // 要看Flag來切換 看是哪個foup
                                        SlotMap(ref Common.EFEM.LoadPort1);
                                        Common.EFEM.LoadPort1.Busy = false;
                                        AutoRunEFEMStep1 = EFEMStep.JudgeStep;
                                    }
                                    else
                                    {
                                        //已經用完了可以先清除secsgem傳遞用的中繼資料，不用等slotmap跑完，這樣可以繼續收下一次的slotmap指令
                                        Common.SecsgemForm.CarrierID = "";
                                        Common.SecsgemForm.LoadPortID = "";
                                        Common.SecsgemForm.bWaitSECS_SlotMapCmd = false;
                                        Common.SecsgemForm.SecsDataClear(SecsData.SlotMap);
                                    }
                                }
                                else if (Common.SecsgemForm.SecsDataGet(SecsData.SlotMap, SecsDataElement.LoadPortID) == "2")
                                {
                                    if (Common.SecsgemForm.SecsDataGet(SecsData.SlotMap, SecsDataElement.CarrierID) == Common.EFEM.LoadPort2.FoupID && fram.SECSPara.Loadport2_AccessMode == Mode.Auto.GetHashCode())
                                    {
                                        //已經用完了可以先清除secsgem傳遞用的中繼資料，不用等slotmap跑完，這樣可以繼續收下一次的slotmap指令
                                        Common.SecsgemForm.CarrierID = "";
                                        Common.SecsgemForm.LoadPortID = "";
                                        Common.SecsgemForm.bWaitSECS_SlotMapCmd = false;
                                        Common.SecsgemForm.SecsDataClear(SecsData.SlotMap);

                                        FoupLoad(ref Common.EFEM.LoadPort2); // 要看Flag來切換 看是哪個foup
                                        SlotMap(ref Common.EFEM.LoadPort2);
                                        Common.EFEM.LoadPort2.Busy = false;
                                        AutoRunEFEMStep2 = EFEMStep.JudgeStep;
                                    }
                                    else
                                    {
                                        //已經用完了可以先清除secsgem傳遞用的中繼資料，不用等slotmap跑完，這樣可以繼續收下一次的slotmap指令
                                        Common.SecsgemForm.CarrierID = "";
                                        Common.SecsgemForm.LoadPortID = "";
                                        Common.SecsgemForm.bWaitSECS_SlotMapCmd = false;
                                        Common.SecsgemForm.SecsDataClear(SecsData.SlotMap);
                                    }
                                }
                                //Common.EFEM.LoadPort_Run = null;//20240828 此行導致一個loadport在autorun時，去做另一個loadport slotmap會清除Common.EFEM.LoadPort_Run，這是不對的
                            }

                            #endregion slotmap cmd ->  load & slotmap  // 完成

                            #region ReleaseCmd  unload

                            if (Common.SecsgemForm.bWaitSECS_ReleaseCmd && !EFEMCmd) // false 才能下指令         // 要看Flag來切換 看是哪個foup
                            {
                                if (Common.SecsgemForm.SecsDataGet(SecsData.Release, SecsDataElement.LoadPortID) == "1")
                                {
                                    listRelease.Add(1);
                                }

                                if (Common.SecsgemForm.SecsDataGet(SecsData.Release, SecsDataElement.LoadPortID) == "2")
                                {
                                    listRelease.Add(2);
                                }

                                if (listRelease.Count > 0)
                                {
                                    if (ReleaseFlag != listRelease[0])
                                    {
                                        if (Common.SecsgemForm.SecsDataGet(SecsData.Release, SecsDataElement.CarrierID) == "")
                                        {
                                            if (Common.SecsgemForm.SecsDataGet(SecsData.Release, SecsDataElement.LoadPortID) == "1" && fram.SECSPara.Loadport1_AccessMode == Mode.Auto.GetHashCode())
                                            {
                                                FoupUnLoad(ref Common.EFEM.LoadPort1);
                                                AutoRunEFEMStep1 = EFEMStep.JudgeStep;
                                                bWEFEMAutoRun.ReportProgress(99);
                                            }
                                            else
                                            {
                                                if (fram.SECSPara.Loadport2_AccessMode == 0)
                                                {
                                                    FoupUnLoad(ref Common.EFEM.LoadPort2);
                                                    AutoRunEFEMStep2 = EFEMStep.JudgeStep;
                                                    bWEFEMAutoRun.ReportProgress(99);
                                                }
                                            }
                                        }
                                        else if (Common.SecsgemForm.SecsDataGet(SecsData.Release, SecsDataElement.LoadPortID) == "1")
                                        {
                                            if (Common.SecsgemForm.SecsDataGet(SecsData.Release, SecsDataElement.CarrierID) == Common.EFEM.LoadPort1.FoupID && fram.SECSPara.Loadport1_AccessMode == Mode.Auto.GetHashCode())
                                            {
                                                FoupUnLoad(ref Common.EFEM.LoadPort1);
                                                AutoRunEFEMStep1 = EFEMStep.JudgeStep;
                                                bWEFEMAutoRun.ReportProgress(99);
                                            }
                                        }
                                        else if (Common.SecsgemForm.SecsDataGet(SecsData.Release, SecsDataElement.LoadPortID) == "2")
                                        {
                                            if (Common.SecsgemForm.SecsDataGet(SecsData.Release, SecsDataElement.CarrierID) == Common.EFEM.LoadPort2.FoupID && fram.SECSPara.Loadport2_AccessMode == Mode.Auto.GetHashCode())
                                            {
                                                FoupUnLoad(ref Common.EFEM.LoadPort2);
                                                AutoRunEFEMStep2 = EFEMStep.JudgeStep;
                                                bWEFEMAutoRun.ReportProgress(99);
                                            }
                                        }
                                        Common.SecsgemForm.CarrierID = "";
                                        Common.SecsgemForm.LoadPortID = "";
                                        Common.SecsgemForm.bWaitSECS_ReleaseCmd = false;
                                        Common.SecsgemForm.SecsDataClear(SecsData.Release);

                                        listRelease.Remove(ReleaseFlag);
                                    }
                                }
                            }

                            #endregion ReleaseCmd  unload

                            if (Common.SecsgemForm.bSECS_ChangeAccessMode_Recive)
                            {
                                AccessModeChange();
                                Common.SecsgemForm.bSECS_ChangeAccessMode_Recive = false;
                                Common.SecsgemForm.SecsDataClear(SecsData.AccessModeChange);
                            }

                            #region Cancel

                            if (Common.SecsgemForm.bWaitSECS_CancelCmd && !EFEMCmd && !Common.EFEM.LoadPort1.Busy && !Common.EFEM.LoadPort2.Busy)
                            {
                                if (Common.SecsgemForm.SecsDataGet(SecsData.Cancel, SecsDataElement.CarrierID) == "" || Common.SecsgemForm.SecsDataGet(SecsData.Cancel, SecsDataElement.CarrierID) == "(Unknown)")
                                {
                                    if (Common.SecsgemForm.SecsDataGet(SecsData.Cancel, SecsDataElement.LoadPortID) == "1" && fram.SECSPara.Loadport1_AccessMode == Mode.Auto.GetHashCode())
                                    {
                                        Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort1;
                                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort1.FoupID, out err);
                                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                                        Common.EFEM.LoadPort1.Busy = true;
                                        //FoupHome(ref Common.EFEM.LoadPort1);
                                        AutoRunEFEMStep1 = EFEMStep.JudgeStep;
                                        Flag.AbortFlag = true;
                                        bWEFEMAutoRun.ReportProgress(99);
                                    }
                                    else if (Common.SecsgemForm.SecsDataGet(SecsData.Cancel, SecsDataElement.LoadPortID) == "2" && fram.SECSPara.Loadport1_AccessMode == Mode.Auto.GetHashCode())
                                    {
                                        Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort2;
                                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort2.FoupID, out err);
                                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                                        Common.EFEM.LoadPort2.Busy = true;
                                        //FoupHome(ref Common.EFEM.LoadPort2);
                                        AutoRunEFEMStep2 = EFEMStep.JudgeStep;
                                        Flag.AbortFlag = true;
                                        bWEFEMAutoRun.ReportProgress(99);
                                    }
                                }
                                else if (Common.SecsgemForm.SecsDataGet(SecsData.Cancel, SecsDataElement.LoadPortID) == "1")
                                {
                                    if (Common.SecsgemForm.SecsDataGet(SecsData.Cancel, SecsDataElement.CarrierID) == Common.EFEM.LoadPort1.FoupID && fram.SECSPara.Loadport1_AccessMode == Mode.Auto.GetHashCode())
                                    {
                                        Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort1;
                                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort1.FoupID, out err);
                                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                                        Common.EFEM.LoadPort1.Busy = true;
                                        //FoupHome(ref Common.EFEM.LoadPort1);
                                        AutoRunEFEMStep1 = EFEMStep.JudgeStep;
                                        Flag.AbortFlag = true;
                                        bWEFEMAutoRun.ReportProgress(99);
                                    }
                                }
                                else if (Common.SecsgemForm.SecsDataGet(SecsData.Cancel, SecsDataElement.LoadPortID) == "2")
                                {
                                    if (Common.SecsgemForm.SecsDataGet(SecsData.Cancel, SecsDataElement.CarrierID) == Common.EFEM.LoadPort2.FoupID && fram.SECSPara.Loadport2_AccessMode == Mode.Auto.GetHashCode())
                                    {
                                        Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort2;
                                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort2.FoupID, out err);
                                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                                        Common.EFEM.LoadPort2.Busy = true;
                                        //FoupHome(ref Common.EFEM.LoadPort2);
                                        AutoRunEFEMStep2 = EFEMStep.JudgeStep;
                                        Flag.AbortFlag = true;
                                        bWEFEMAutoRun.ReportProgress(99);
                                    }
                                }
                                Common.SecsgemForm.CarrierID = "";
                                Common.SecsgemForm.LoadPortID = "";
                                Common.SecsgemForm.bWaitSECS_CancelCmd = false;
                                Common.SecsgemForm.SecsDataClear(SecsData.Cancel);
                            }

                            #endregion Cancel

                            #region CJ/PJ Start Detection
                            // 【SimStep 7+8】偵測 Host 建立 CJ/PJ → 設定 Busy 啟動 EFEMRun
                            if (!EFEMCmd && !Common.EFEM.LoadPort1.Busy && !Common.EFEM.LoadPort2.Busy
                                && (sram.RunningCJ == "" || sram.RunningCJ == null)
                                && MainForm.CJ_list != null && MainForm.CJ_list.Count > 0)
                            {
                                sram.RunningCJ = MainForm.CJ_list[0];
                                Common.SecsgemForm.GetControlJobAttr(sram.RunningCJ,
                                    out sram.CJInfo.carrierInputSpec, out sram.CJInfo.curPJ,
                                    out sram.CJInfo.dataCollection, out sram.CJInfo.mtrloutStatus,
                                    out sram.CJInfo.mtrloutSpec, out sram.CJInfo.pauseEvent,
                                    out sram.CJInfo.procCtrlSpec, out sram.CJInfo.procOrder,
                                    out sram.CJInfo.bStart, out sram.CJInfo.state, out err);

                                // 解析 PJ 佇列
                                string[] pjl = sram.CJInfo.procCtrlSpec.Split(';');
                                sram.QueuePJ = new List<string>();
                                foreach (string s in pjl)
                                {
                                    if (!string.IsNullOrEmpty(s))
                                        sram.QueuePJ.Add(s);
                                }

                                // CJ → EXECUTING
                                Common.SecsgemForm.ChangeControlJobState(sram.RunningCJ, ControlJobState.EXECUTING, 0);
                                Gem300Monitor.AddSend("ChangeControlJobState → EXECUTING  CJ=" + sram.RunningCJ);

                                // 透過 CJ 的 carrierInputSpec 比對 LoadPort FoupID 來判斷對應 LP
                                LoadPort targetLP = null;
                                if (Common.EFEM.LoadPort1.Placement
                                    && !string.IsNullOrEmpty(Common.EFEM.LoadPort1.FoupID)
                                    && fram.SECSPara.Loadport1_AccessMode == Mode.Auto.GetHashCode())
                                {
                                    targetLP = Common.EFEM.LoadPort1;
                                }
                                else if (Common.EFEM.LoadPort2.Placement
                                    && !string.IsNullOrEmpty(Common.EFEM.LoadPort2.FoupID)
                                    && fram.SECSPara.Loadport2_AccessMode == Mode.Auto.GetHashCode())
                                {
                                    targetLP = Common.EFEM.LoadPort2;
                                }

                                if (targetLP != null)
                                {
                                    // Carrier → IN_ACCESS
                                    Common.SecsgemForm.SetCarrierStatus_Accessing(targetLP.FoupID, CarrierAccessingState.IN_ACCESS);
                                    Gem300Monitor.AddState("CarrierAccessing → IN_ACCESS");

                                    SECSListening.ClearReportdata();
                                    targetLP.AutoGetSlot();
                                    Common.EFEM.LoadPort_Run = targetLP;
                                    targetLP.Busy = true;
                                    Flag.GreenLightFlag = true;

                                    if (targetLP.pn == LoadPort.Pn.P1)
                                        AutoRunEFEMStep1 = EFEMStep.JudgeStep;
                                    else
                                        AutoRunEFEMStep2 = EFEMStep.JudgeStep;

                                    Gem300Monitor.AddState("CJ/PJ Start: LP=" + targetLP.pn + " CJ=" + sram.RunningCJ
                                        + " PJ Count=" + sram.QueuePJ.Count);
                                }
                                else
                                {
                                    // 找不到對應 LP，重置 RunningCJ 等下次重試
                                    Gem300Monitor.AddAlarm("CJ/PJ Start: 找不到對應 LoadPort, CJ=" + sram.RunningCJ);
                                    sram.RunningCJ = "";
                                }
                            }
                            #endregion CJ/PJ Start Detection

                            #region EFEMRun

                            if (!EFEMCmd)
                            {
                                if (Common.EFEM.LoadPort1.Busy)
                                {
                                    if (Flag.GreenLightFlag)
                                    {
                                        Common.EFEM.IO.SignalTower(IO.LampState.AllOff);
                                        Common.EFEM.IO.SignalTower(IO.LampState.GreenOn);
                                        Flag.GreenLightFlag = false;
                                    }
                                    EFEMRun(ref Common.EFEM.LoadPort1, ref AutoRunEFEMStep1, ref AutoRunEFEMStepBack1);
                                    Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort1;
                                }
                                else if (Common.EFEM.LoadPort2.Busy)
                                {
                                    if (Flag.GreenLightFlag)
                                    {
                                        Common.EFEM.IO.SignalTower(IO.LampState.AllOff);
                                        Common.EFEM.IO.SignalTower(IO.LampState.GreenOn);
                                        Flag.GreenLightFlag = false;
                                    }
                                    EFEMRun(ref Common.EFEM.LoadPort2, ref AutoRunEFEMStep2, ref AutoRunEFEMStepBack2);
                                    Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort2;
                                }
                            }

                            #endregion EFEMRun

                            Flag.EFEMStepDoneFlag = false;
                            Flag.EFEMStepDoneFlag = true;
                        }
                        else if (!Common.SecsgemForm.isRemote() && Flag.Autoidle_LocalFlag)// Local , Offline // 只要不是remote狀態 都來這裡
                        {
                            #region clamp readid // local模式 先看有沒有foup 有就直接做

                            if (fram.EFEMSts.Skip == 0)
                            {
                                EFEMCmd = false;
                                CheckMaterialLocal();
                            }
                            //else
                            //{
                            //    EFEMCmd = true;
                            //}

                            if (!EFEMCmd) // false 才能下指令         // 要看Flag來切換 看是哪個foup
                            {
                                if (!Common.EFEM.LoadPort1.Busy && Common.EFEM.LoadPort1.Placement && fram.SECSPara.Loadport1_AccessMode == Mode.Auto.GetHashCode())
                                {
                                    ClampAndReadID(ref Common.EFEM.LoadPort1);
                                }
                                else if (!Common.EFEM.LoadPort2.Busy && Common.EFEM.LoadPort2.Placement && fram.SECSPara.Loadport2_AccessMode == Mode.Auto.GetHashCode())
                                {
                                    ClampAndReadID(ref Common.EFEM.LoadPort2);
                                }
                            }

                            #endregion clamp readid // local模式 先看有沒有foup 有就直接做

                            #region slotmap cmd ->  load & slotmap  // 完成
                            //if (Flag.AllHomeFlag) return;
                            if (!EFEMCmd) // false 才能下指令
                            {
                                if (!Common.EFEM.LoadPort1.Busy && Common.EFEM.LoadPort1.Placement && fram.SECSPara.Loadport1_AccessMode == Mode.Auto.GetHashCode())
                                {
                                    FoupLoad(ref Common.EFEM.LoadPort1); // 要看Flag來切換 看是哪個foup
                                    SlotMap(ref Common.EFEM.LoadPort1);

                                    for (int i = 0; i < 25; i++)
                                    {
                                        if (Common.EFEM.LoadPort1.slot_Status[i] == EFEM.slot_status.Ready)
                                        {
                                            if (sram.Recipe.Slot[i] == 0)
                                            {
                                                Common.EFEM.LoadPort1.Update_slot_Status(i + 1, EFEM.slot_status.ProcessEnd);
                                            }
                                        }
                                    }
                                    Common.EFEM.LoadPort1.AutoGetSlot(); // 此步驟是先更新 LoadPort Ready to load
                                    //
                                    Common.EFEM.LoadPort1.Busy = true;
                                    Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort1;
                                    bWEFEMAutoRun.ReportProgress(99);
                                }
                                else if (!Common.EFEM.LoadPort2.Busy && Common.EFEM.LoadPort2.Placement && fram.SECSPara.Loadport2_AccessMode == Mode.Auto.GetHashCode())
                                {
                                    FoupLoad(ref Common.EFEM.LoadPort2); // 要看Flag來切換 看是哪個foup
                                    SlotMap(ref Common.EFEM.LoadPort2);
                                    //
                                    for (int i = 0; i < 25; i++)
                                    {
                                        if (Common.EFEM.LoadPort2.slot_Status[i] == EFEM.slot_status.Ready)
                                        {
                                            if (sram.Recipe.Slot[i] == 0)
                                            {
                                                Common.EFEM.LoadPort2.Update_slot_Status(i + 1, EFEM.slot_status.ProcessEnd);
                                            }
                                        }
                                    }
                                    Common.EFEM.LoadPort2.AutoGetSlot(); // 此步驟是先更新 LoadPort Ready to load
                                    //
                                    Common.EFEM.LoadPort2.Busy = true;
                                    Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort2;
                                    bWEFEMAutoRun.ReportProgress(99);
                                }
                            }

                            #endregion slotmap cmd ->  load & slotmap  // 完成

                            #region EFEMRun

                            if (!EFEMCmd)
                            {
                                if (Common.EFEM.LoadPort1.Busy)
                                {
                                    EFEMRun(ref Common.EFEM.LoadPort1, ref AutoRunEFEMStep1, ref AutoRunEFEMStepBack1);
                                    Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort1;
                                }
                                else if (Common.EFEM.LoadPort2.Busy)
                                {
                                    EFEMRun(ref Common.EFEM.LoadPort2, ref AutoRunEFEMStep2, ref AutoRunEFEMStepBack1);
                                    Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort2;
                                }
                                else if(fram.EFEMSts.Skip == 1)
                                {
                                    Common.EFEM.LoadPort_Run = Common.EFEM.LoadPort1;
                                }
                            }

                            #endregion EFEMRun

                            #region unload

                            if (!EFEMCmd) // false 才能下指令         // efemStep == Finish才進去unload
                            {
                                if (Common.EFEM.LoadPort1.Busy && Common.EFEM.LoadPort1.Placement && fram.SECSPara.Loadport1_AccessMode == Mode.Auto.GetHashCode())
                                {
                                    if (AutoRunEFEMStep1 == EFEMStep.Finish)
                                    {
                                        FoupUnLoad(ref Common.EFEM.LoadPort1);
                                        sram.Recipe.RepeatTimes_now--;
                                        AutoRunEFEMStep1 = EFEMStep.JudgeStep;
                                        bWEFEMAutoRun.ReportProgress(99);
                                    }
                                }
                                else if (Common.EFEM.LoadPort2.Busy && Common.EFEM.LoadPort2.Placement && fram.SECSPara.Loadport2_AccessMode == Mode.Auto.GetHashCode())
                                {
                                    if (AutoRunEFEMStep2 == EFEMStep.Finish)
                                    {
                                        FoupUnLoad(ref Common.EFEM.LoadPort2);
                                        sram.Recipe.RepeatTimes_now--;
                                        AutoRunEFEMStep2 = EFEMStep.JudgeStep;
                                        bWEFEMAutoRun.ReportProgress(99);
                                    }
                                }
                                if (sram.Recipe.RepeatTimes_now == 0)
                                {
                                    Common.EFEM.LoadPort_Run = null;
                                    Flag.AutoidleFlag = false;
                                    Flag.Autoidle_LocalFlag = false;
                                }
                            }

                            #endregion unload
                        }
                    }
                    if (!Flag.AutoidleFlag && Flag.AllHome_busyFlag && !Flag.AllHomeFlag)
                    {
                        if (EFEMHomeAll())
                        {
                            bWEFEMAutoRun.ReportProgress(1);
                        }
                        else
                        {
                            bWEFEMAutoRun.ReportProgress(2);
                        }
                        AutoRunEFEMStep1 = EFEMStep.JudgeStep;
                        AutoRunEFEMStep2 = EFEMStep.JudgeStep;
                        if (fram.EFEMSts.Skip == 0)
                        {
                            Common.EFEM.LoadPort1.Busy = false;
                            Common.EFEM.LoadPort2.Busy = false;
                        }
                        AutoRunStage.AutoRunStageStep = AutoRunStage.AutoStep.GotoSwitchWaferPos;
                        AutoRunStage.AutoTrimStep = AutoRunStage.TrimStep.LJHome;
                        AutoRunStage.AutoBlueTapeStep = AutoRunStage.BluetapeStep.Finish;
                        AutoRunStage.AutoTTVStep = AutoRunStage.TTVStep.Finish;
                        bWEFEMAutoRun.ReportProgress(99);
                    }
                }
                catch (Exception ee)
                {
                    Console.WriteLine("bwEFEMRun : " + ee.Message);
                    //MessageBox.Show("bwEFEMRun : " + ee.Message);
                }
            }
        }

        private static void bWEFEMAutoRun_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1) // Home all成功
            {
                Common.findHomePanel.Hide();
                InsertLog.SavetoDB(62);
            }
            else if (e.ProgressPercentage == 2)// Home all失敗
            {
                Common.findHomePanel.Hide();
                InsertLog.SavetoDB(112, HomeAllFailStr);
            }
            else if (e.ProgressPercentage == 99)
            {
                if (fram.EFEMSts.Skip == 0)
                {
                    SaveEFEMSts();
                    Update_EFEM_Sts();
                }
                ParamFile.saveparam("EFEMSts");
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // 【SimStep 1+2】偵測 Carrier 放上 LoadPort
        //   ReadyToLoad && Placement → MaterialReceived + CarrierDock + CarrierDoorOpened
        //   → ClampAndReadID() → TransferBlocked
        // 【SimStep 0】TransferBlocked && !Placement → ReadyToLoad
        // ═══════════════════════════════════════════════════════════════
        private static void WatchMaterialRecive()
        {
            // Placement 在socket會收到
            // PortTransferState: 1:block, 2:RTL, 3:RTUL
            // 對應到TSMC的狀態    1:ini  , 2:Mir, 3:Mor
            // block的時候就不用再看了
            if (fram.SECSPara.Loadport1_PortTransferState == PortTransferState.ReadyToLoad.GetHashCode() && Common.EFEM.LoadPort1.Placement)
            {
                // ** CRITICAL FIX: To pass Host Carrier Placement validation, Carrier Object MUST exist before events!
                Common.EFEM.LoadPort1.ReadFoupID();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort1.FoupID, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortID, (byte)1, out err);

                // Create the Carrier Object on LoadPort1
                Common.SecsgemForm.CreateCarrier(Common.EFEM.LoadPort1.FoupID, "LOADPORT1");
                Common.SecsgemForm.UpdateLoadPortAssociationState("1", 1); // 1 = ASSOCIATED
                
                // Immediately change state to TransferBlocked and fire CEID 229 for standard E87 "Carrier Placement"
                fram.SECSPara.Loadport1_PortTransferState = PortTransferState.TransferBlocked.GetHashCode();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err);
                if (fram.m_SecsgemType == 1)
                    Common.SecsgemForm.EventReportSend(229, out err); // E87: ReadyToLoadToTransferBlocked
                Gem300Monitor.AddState("LP1 PortState → TransferBlocked");

                // Report Material Received / Load Complete
                if (fram.m_SecsgemType == 0)
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MaterialReceived, out err);
                else
                {
                    Common.SecsgemForm.EventReportSend(2104, out err);
                    Common.SecsgemForm.EventReportSend(5117, out err); // 5117 = CarrierLoadComplete
                }
                
                Gem300Monitor.AddSend(string.Format("MaterialReceived  LP=1  FoupID={0}", Common.EFEM.LoadPort1.FoupID));
                
                // Wait mechanically
                SpinWait.SpinUntil(() => false, 2000);
                
                // Execute Auto Clamp and Auto Read, which internally send CarrierClamped and CarrierIDRead
                ClampAndReadID(ref Common.EFEM.LoadPort1);

                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, "", out err);
            }
            else if (fram.SECSPara.Loadport1_PortTransferState == PortTransferState.TransferBlocked.GetHashCode())
            {
                if (Common.EFEM.LoadPort1.Placement == false)
                {
                    Common.EFEM.LoadPort1.GetLEDStatus();
                    if (!Common.EFEM.LoadPort1.LED_Placement && !Common.EFEM.LoadPort1.LED_Persence)
                    {
                        fram.SECSPara.Loadport1_PortTransferState = PortTransferState.ReadyToLoad.GetHashCode();
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err); // RTL
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err); // ReadyToLoad
                        Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ReadyToLoad, out err);
                    }
                }
            }
            else if (fram.SECSPara.Loadport1_PortTransferState == PortTransferState.OutOfService.GetHashCode())
            {
                Common.EFEM.LoadPort1.GetLEDStatus();
                if (Common.EFEM.LoadPort1.LED_Placement && Common.EFEM.LoadPort1.LED_Persence)
                {
                    fram.SECSPara.Loadport1_PortTransferState = PortTransferState.TransferBlocked.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err);
                }
                else
                {
                    fram.SECSPara.Loadport1_PortTransferState = PortTransferState.ReadyToLoad.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err); // RTL
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err); // ReadyToLoad
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ReadyToLoad, out err);
                }
            }

            if (fram.SECSPara.Loadport2_PortTransferState == PortTransferState.ReadyToLoad.GetHashCode() && Common.EFEM.LoadPort2.Placement)
            {
                // ** CRITICAL FIX: To pass Host Carrier Placement validation, Carrier Object MUST exist before events!
                Common.EFEM.LoadPort2.ReadFoupID();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort2.FoupID, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortID, (byte)2, out err);

                // Create the Carrier Object on LoadPort2
                Common.SecsgemForm.CreateCarrier(Common.EFEM.LoadPort2.FoupID, "LOADPORT2");
                Common.SecsgemForm.UpdateLoadPortAssociationState("2", 1); // 1 = ASSOCIATED

                // Immediately change state to TransferBlocked and fire CEID 229 for standard E87 "Carrier Placement"
                fram.SECSPara.Loadport2_PortTransferState = PortTransferState.TransferBlocked.GetHashCode();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err);
                if (fram.m_SecsgemType == 1)
                    Common.SecsgemForm.EventReportSend(229, out err); // E87: ReadyToLoadToTransferBlocked
                Gem300Monitor.AddState("LP2 PortState → TransferBlocked");

                // Report Material Received / Load Complete
                if (fram.m_SecsgemType == 0)
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MaterialReceived, out err);
                else
                {
                    Common.SecsgemForm.EventReportSend(2104, out err);
                    Common.SecsgemForm.EventReportSend(5117, out err); // 5117 = CarrierLoadComplete
                }

                Gem300Monitor.AddSend(string.Format("MaterialReceived  LP=2  FoupID={0}", Common.EFEM.LoadPort2.FoupID));
                
                // Wait mechanically
                SpinWait.SpinUntil(() => false, 2000);
                
                // Execute Auto Clamp and Auto Read, which internally send CarrierClamped and CarrierIDRead
                ClampAndReadID(ref Common.EFEM.LoadPort2);

                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, "", out err);
            }
            else if (fram.SECSPara.Loadport2_PortTransferState == PortTransferState.TransferBlocked.GetHashCode())
            {
                if (Common.EFEM.LoadPort2.Placement == false)
                {
                    Common.EFEM.LoadPort2.GetLEDStatus();
                    if (!Common.EFEM.LoadPort2.LED_Placement && !Common.EFEM.LoadPort2.LED_Persence)
                    {
                        fram.SECSPara.Loadport2_PortTransferState = PortTransferState.ReadyToLoad.GetHashCode();
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err); // RTL
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err); // ReadyToLoad
                        Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ReadyToLoad, out err);
                    }
                }
            }
            else if (fram.SECSPara.Loadport2_PortTransferState == PortTransferState.OutOfService.GetHashCode())
            {
                Common.EFEM.LoadPort2.GetLEDStatus();
                if (Common.EFEM.LoadPort2.LED_Placement && Common.EFEM.LoadPort2.LED_Persence)
                {
                    fram.SECSPara.Loadport2_PortTransferState = PortTransferState.TransferBlocked.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err);
                }
                else
                {
                    fram.SECSPara.Loadport2_PortTransferState = PortTransferState.ReadyToLoad.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err); // RTL
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err); // ReadyToLoad
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ReadyToLoad, out err);
                }
            }
        }



        // ═══════════════════════════════════════════════════════════════
        // 【SimStep 12】偵測 Carrier 拿走
        //   ReadyToUnload && !Placement → DeleteCarrier → MaterialRemove
        //   → PortTransferState = ReadyToLoad
        // ═══════════════════════════════════════════════════════════════
        private static void WatchMaterialRemove()
        {
            // Placement 在socket會收到
            // PortTransferState: 1:block, 2:RTL, 3:RTUL
            // RTUL 才要檢查
            //fram.SECSPara.Loadport1_PortTransferState = PortTransferState.ReadyToUnload.GetHashCode();
            if (fram.SECSPara.Loadport1_PortTransferState == PortTransferState.ReadyToUnload.GetHashCode() && !Common.EFEM.LoadPort1.Placement)
            {
                // 東西拿走後 切成 RTL
                Common.SecsgemForm.DeleteCarrier(Common.EFEM.LoadPort1.FoupID, out err);  //刪除Carrier Object
                
                // ⭐ 新增: 解除 LoadPort Association 並發送 253 (AssociatedToNotAssociated)
                Common.SecsgemForm.UpdateLoadPortAssociationState("1", 0); 
                if (fram.m_SecsgemType == 1)
                {
                    Common.SecsgemForm.EventReportSend(253, out err); 
                    Gem300Monitor.AddSend("CarrierRemoved (AssociatedToNotAssociated CEID 253) LP=1");
                }

                Common.EFEM.LoadPort1.ReadFoupID();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort1.FoupID, out err);
                if (fram.m_SecsgemType == 0)
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MaterialRemove, out err);
                else
                    Common.SecsgemForm.EventReportSend(2105, out err);

                // ⭐ CEID 5118: CarrierUnloadComplete (Excel Index 11, ToolSoftwareTester Step 25)
                Common.SecsgemForm.EventReportSend(5118, out err);

                fram.SECSPara.Loadport1_PortTransferState = PortTransferState.ReadyToLoad.GetHashCode();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, (byte)PortTransferState.ReadyToLoad.GetHashCode(), out err); // RTL
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err); // ReadyToLoad
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ReadyToLoad, out err);
                Common.EFEM.LoadPort1.LEDLoad(LoadPort.LEDsts.Off);
                Common.EFEM.LoadPort1.LEDUnLoad(LoadPort.LEDsts.Off);

                //Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);
                //Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierDock, out err);
                //Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierDoorOpened, out err);
                //loadPort.LEDLoad(LoadPort.LEDsts.On);
                //loadPort.Busy = true;
            }
            else if(fram.SECSPara.Loadport1_PortTransferState == PortTransferState.ReadyToUnload.GetHashCode() && Common.SecsgemForm.CarrierReCreate == 1)
            {
                Common.SecsgemForm.CarrierReCreate = 0;
                // ReCreate
                Common.EFEM.LoadPort1.ReadFoupID();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort1.FoupID, out err);
                if (fram.m_SecsgemType == 0)
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MaterialReceived, out err);
                else
                    Common.SecsgemForm.EventReportSend(2104, out err);
                Gem300Monitor.AddSend(string.Format("MaterialReceived  LP=1  FoupID={0}", Common.EFEM.LoadPort1.FoupID));
                SpinWait.SpinUntil(() => false, 2000);
                ClampAndReadID(ref Common.EFEM.LoadPort1);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, "", out err);
                fram.SECSPara.Loadport1_PortTransferState = PortTransferState.TransferBlocked.GetHashCode();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err);
                Gem300Monitor.AddState("LP1 PortState → TransferBlocked");
            }
            if (fram.SECSPara.Loadport2_PortTransferState == PortTransferState.ReadyToUnload.GetHashCode() && !Common.EFEM.LoadPort2.Placement)
            {
                Common.SecsgemForm.DeleteCarrier(Common.EFEM.LoadPort2.FoupID, out err);  //刪除Carrier Object
                
                // ⭐ 新增: 解除 LoadPort Association 並發送 253 (AssociatedToNotAssociated)
                Common.SecsgemForm.UpdateLoadPortAssociationState("2", 0);
                if (fram.m_SecsgemType == 1)
                {
                    Common.SecsgemForm.EventReportSend(253, out err);
                    Gem300Monitor.AddSend("CarrierRemoved (AssociatedToNotAssociated CEID 253) LP=2");
                }

                // 東西拿走後 切成 RTL
                Common.EFEM.LoadPort2.ReadFoupID();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort2.FoupID, out err);
                if (fram.m_SecsgemType == 0)
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MaterialRemove, out err);
                else
                    Common.SecsgemForm.EventReportSend(2105, out err);

                // ⭐ CEID 5118: CarrierUnloadComplete (Excel Index 11, ToolSoftwareTester Step 25)
                Common.SecsgemForm.EventReportSend(5118, out err);

                fram.SECSPara.Loadport2_PortTransferState = PortTransferState.ReadyToLoad.GetHashCode();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, (byte)PortTransferState.ReadyToLoad.GetHashCode(), out err); // RTL
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err); // ReadyToLoad
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ReadyToLoad, out err);
                Common.EFEM.LoadPort2.LEDLoad(LoadPort.LEDsts.Off);
                Common.EFEM.LoadPort2.LEDUnLoad(LoadPort.LEDsts.Off);
            }
            else if (fram.SECSPara.Loadport2_PortTransferState == PortTransferState.ReadyToUnload.GetHashCode() && Common.SecsgemForm.CarrierReCreate == 2)
            {
                Common.SecsgemForm.CarrierReCreate = 0;
                Common.EFEM.LoadPort2.ReadFoupID();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort2.FoupID, out err);
                if (fram.m_SecsgemType == 0)
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MaterialReceived, out err);
                else
                    Common.SecsgemForm.EventReportSend(2104, out err);
                Gem300Monitor.AddSend(string.Format("MaterialReceived  LP=2  FoupID={0}", Common.EFEM.LoadPort2.FoupID));
                SpinWait.SpinUntil(() => false, 2000);
                ClampAndReadID(ref Common.EFEM.LoadPort2);

                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, "", out err);
                fram.SECSPara.Loadport2_PortTransferState = PortTransferState.TransferBlocked.GetHashCode();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err);
                Gem300Monitor.AddState("LP2 PortState → TransferBlocked");
            }
        }

        private static void CheckMaterialLocal()
        {
            if (fram.SECSPara.Loadport1_AccessMode == Mode.Auto.GetHashCode())
            {
                Common.EFEM.LoadPort1.GetLEDStatus();
                if (Common.EFEM.LoadPort1.LED_Placement && Common.EFEM.LoadPort1.LED_Persence)
                {
                    Common.EFEM.LoadPort1.Placement = true;
                    sram.LoadPort1_Carrier_Vertify = true;
                }
            }
            else if (fram.SECSPara.Loadport2_AccessMode == Mode.Auto.GetHashCode())
            {
                Common.EFEM.LoadPort2.GetLEDStatus();
                if (Common.EFEM.LoadPort2.LED_Placement && Common.EFEM.LoadPort2.LED_Persence)
                {
                    Common.EFEM.LoadPort2.Placement = true;
                    sram.LoadPort1_Carrier_Vertify = true;
                }
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // 【SimStep 2】Clamp + ReadID + CreateCarrier
        //   Clamp() → CarrierClamped → ReadFoupID() → CarrierIDRead
        //   → CreateCarrier() → SetCarrierStatus_ID(WAITING_FOR_HOST)
        // ═══════════════════════════════════════════════════════════════
        private static void ClampAndReadID(ref LoadPort loadPort)
        {
            bool rtn;
            rtn = loadPort.Clamp();
            if (rtn)
            {
                if (loadPort.pn == LoadPort.Pn.P1)
                {
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                    Common.SecsgemForm.UpdateSV(462, (byte)1, out err); // CMS_PORTID 462 for E87 Standard
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortID, (byte)1, out err);
                    sram.LoadPort1_Carrier_Vertify = false;
                }
                else if (loadPort.pn == LoadPort.Pn.P2)
                {
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                    Common.SecsgemForm.UpdateSV(462, (byte)2, out err); // CMS_PORTID 462 for E87 Standard
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortID, (byte)2, out err);
                    sram.LoadPort2_Carrier_Vertify = false;
                }
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierClamped, out err);
                rtn = loadPort.ReadFoupID();
                if (rtn)
                {
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);
                    
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierIDRead, out err);
                    Common.SecsgemForm.SetCarrierStatus_ID(loadPort.FoupID, CarrierIDState.WAITING_FOR_HOST);
                }
                else
                {
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierIDReadFail, out err);
                }
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // 【SimStep 4】FoupLoad → Dock → DoorOpen
        //   Load() → CarrierDock → CarrierDoorOpened
        // ═══════════════════════════════════════════════════════════════
        private static void FoupLoad(ref LoadPort loadPort)
        {
            bool rtn;
            loadPort.LEDUnLoad(LoadPort.LEDsts.Off);
            loadPort.LEDLoad(LoadPort.LEDsts.Flash);
            rtn = loadPort.Load();

            if (rtn)
            {
                if (loadPort.pn == LoadPort.Pn.P1)
                {
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortID, (byte)1, out err);
                }
                else if (loadPort.pn == LoadPort.Pn.P2)
                {
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortID, (byte)2, out err);
                }
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierDock, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierDoorOpened, out err);
                loadPort.LEDLoad(LoadPort.LEDsts.On);
                loadPort.Busy = true;
            }
            else
            {
                Console.WriteLine("Load Fail " + loadPort.pn.ToString());
                EFEMGotoErrorCheckStep();
            }
        }

        // ═══════════════════════════════════════════════════════════════
        // 【SimStep 11】FoupUnLoad → DoorClose → UnDock → Unclamp
        //   Unload() → CarrierDoorClosed → CarrierUnDock → CarrierUnclamped
        //   → PortTransferState = ReadyToUnload
        // ═══════════════════════════════════════════════════════════════
        private static void FoupUnLoad(ref LoadPort loadPort)
        {
            bool rtn;
            loadPort.LEDLoad(LoadPort.LEDsts.Off);
            loadPort.LEDUnLoad(LoadPort.LEDsts.Flash);
            rtn = loadPort.Unload();
            if (rtn)
            {
                loadPort.Busy = false;

                // ⭐ 先更新 PortID / CarrierID，確保 RPID 18 (CEID 1010) 的 VID 有值
                if (loadPort.pn == LoadPort.Pn.P1)
                {
                    // 模擬模式下不自動設定 Placement = false，由模擬按鈕控制 Carrier 移除時機
                    // 避免 WatchMaterialRemove() 搶先觸發 MaterialRemove 事件

                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortID, (byte)1, out err);
                    fram.SECSPara.Loadport1_PortTransferState = PortTransferState.ReadyToUnload.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err); // ReadyToUnload
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_RecipeID, "", out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err); // ReadyToUnload
                    sram.LoadPort1_Carrier_Vertify = false;
                }
                else if (loadPort.pn == LoadPort.Pn.P2)
                {
                    // 模擬模式下不自動設定 Placement = false，由模擬按鈕控制 Carrier 移除時機
                    // 避免 WatchMaterialRemove() 搶先觸發 MaterialRemove 事件

                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortID, (byte)2, out err);
                    fram.SECSPara.Loadport2_PortTransferState = PortTransferState.ReadyToUnload.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err); // ReadyToUnload
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_RecipeID, "", out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err); // ReadyToUnload
                    sram.LoadPort2_Carrier_Vertify = false;
                }
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);

                // ⭐ PortID/CarrierID 更新完後才發事件，Host Report 才能正確讀值
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierDoorClosed, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierUnDock, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierUnclamped, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ReadyToUnload, out err);
                for (int i = 0; i < 25; i++)
                {
                    loadPort.Slot[i] = 0;
                    loadPort.slot_Status[i] = EFEM.slot_status.Empty;
                    loadPort.Update_slot_Status(i + 1, EFEM.slot_status.Empty);
                    loadPort.SubstrateID[i] = null;
                    loadPort.LotID[i] = null;
                }
            }
            else
            {
                rtn = loadPort.Home(); // 如果unload fail 嘗試一次 home
                if (rtn)
                {
                    loadPort.Busy = false;
                    if (loadPort.pn == LoadPort.Pn.P1)
                    {
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortID, (byte)1, out err);
                        fram.SECSPara.Loadport1_PortTransferState = PortTransferState.ReadyToUnload.GetHashCode();
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err); // ReadyToUnload
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_RecipeID, "", out err);
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err); // ReadyToUnload
                        sram.LoadPort1_Carrier_Vertify = false;
                    }
                    else if (loadPort.pn == LoadPort.Pn.P2)
                    {
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortID, (byte)2, out err);
                        fram.SECSPara.Loadport2_PortTransferState = PortTransferState.ReadyToUnload.GetHashCode();
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err); // ReadyToUnload
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_RecipeID, "", out err);
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err); // ReadyToUnload
                        sram.LoadPort2_Carrier_Vertify = false;
                    }
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierDoorClosed, out err);
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierUnDock, out err);
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierUnclamped, out err);
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ReadyToUnload, out err);
                    for (int i = 0; i < 25; i++)
                    {
                        loadPort.Slot[i] = 0;
                        loadPort.slot_Status[i] = EFEM.slot_status.Empty;
                        loadPort.Update_slot_Status(i + 1, EFEM.slot_status.Empty);
                        loadPort.SubstrateID[i] = null;
                        loadPort.LotID[i] = null;
                    }
                }
                else
                {
                    InsertLog.SavetoDB(110, "Pn：" + loadPort.pn + ", " + loadPort.ErrorDescription);
                }
            }

            loadPort.LEDUnLoad(LoadPort.LEDsts.On);
        }

        private static void FoupHome(ref LoadPort loadPort)
        {
            bool rtn;
            loadPort.LEDLoad(LoadPort.LEDsts.Off);
            loadPort.LEDUnLoad(LoadPort.LEDsts.Flash);

            rtn = loadPort.Home(); // 如果unload fail 嘗試一次 home
            if (rtn)
            {
                //Common.CGWrapper.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, fram.SECSPara.Loadport2_PortTransferState); // RTL
                loadPort.Busy = false;
                if (loadPort.pn == LoadPort.Pn.P1)
                {
                    fram.SECSPara.Loadport1_PortTransferState = PortTransferState.ReadyToUnload.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err); // ReadyToUnload
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_RecipeID, "", out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err); // ReadyToUnload
                }
                else if (loadPort.pn == LoadPort.Pn.P2)
                {
                    fram.SECSPara.Loadport2_PortTransferState = PortTransferState.ReadyToUnload.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err); // ReadyToUnload
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_RecipeID, "", out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err); // ReadyToUnload
                }
                for (int i = 0; i < 25; i++)
                {
                    loadPort.slot_Status[i] = EFEM.slot_status.Empty;
                    loadPort.Update_slot_Status(i + 1, EFEM.slot_status.Empty);
                }

                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierDoorClosed, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierUnDock, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierUnclamped, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ReadyToUnload, out err);
            }
            else
            {
                InsertLog.SavetoDB(110, "Pn：" + loadPort.pn + ", " + loadPort.ErrorDescription);
            }

            loadPort.LEDUnLoad(LoadPort.LEDsts.On);
        }

        // ═══════════════════════════════════════════════════════════════
        // 【SimStep 5】SlotMap
        //   CarrierMapStarted → Map() → GetWaferSlot2() → CarrierMapped
        //   → SetCarrierAttr_SlotMap → SetCarrierStatus_SlotMap(WAITING_FOR_HOST)
        //   → SlotMap事件
        // ═══════════════════════════════════════════════════════════════
        private static void SlotMap(ref LoadPort loadPort)
        {
            bool rtn;
            if (loadPort.pn == LoadPort.Pn.P1)
            {
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.LP1_CarrierMapStarted, out err);
            }
            else if (loadPort.pn == LoadPort.Pn.P2)
            {
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.LP2_CarrierMapStarted, out err);
            }
            rtn = loadPort.Map();

            if (rtn)
            {
                loadPort.GetWaferSlot2(); //已做完
                //Common.SecsgemForm.UpdateSV(TrimGap_EqpID.SlotMapStatus, (byte)2, out err);
                // 0 = SLOTMAP NOT READ
                // 1 = WAITING FOR HOST
                // 2 = SLOTMAP VERIFIKATION OK
                // 3 = SLOTMAP VERIFICATION FAILED
                string testslotmap = "";
                for (int i = 0; i < loadPort.Slot.Length; i++)
                {
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.SlotMap_1 + i, (byte)loadPort.Slot[i], out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Reason, (byte)0, out err); // map 成功沒有error ，等待驗證
                    if (loadPort.Slot[i] > 1)
                    {
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Reason, (byte)3, out err); // Wafer有放歪
                    }
                    testslotmap += Convert.ToString(loadPort.Slot[i]); //測試
                }
                InsertLog.SavetoDB(67, "Slotmap:" + testslotmap);

                if (loadPort.pn == LoadPort.Pn.P1)
                {
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.LP1_CarrierMapped, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                }
                else if (loadPort.pn == LoadPort.Pn.P2)
                {
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.LP2_CarrierMapped, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                }
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);

                if(fram.m_SecsgemType == 1)
                {

                    Common.SecsgemForm.SetCarrierStatus_SlotMap(loadPort.FoupID, SlotMapState.WAITING_FOR_HOST);

                    int rtn2 = Common.SecsgemForm.SetCarrierAttr_SlotMap(loadPort.FoupID, loadPort.Slot);
                    Console.WriteLine($"[Debug] SetCarrierAttr_SlotMap 回傳: {rtn2}, Slot長度: {loadPort.Slot.Length}");

                    // ⭐ 為每個有 Wafer 的 Slot 建立 Substrate 物件
                    string lpLocation = loadPort.pn == LoadPort.Pn.P1 ? "LOADPORT1" : "LOADPORT2";
                    for (int i = 0; i < loadPort.Slot.Length; i++)
                    {
                        if (loadPort.Slot[i] == 1) // Wafer 存在
                        {
                            string substID = loadPort.SubstrateID != null && i < loadPort.SubstrateID.Length && !string.IsNullOrEmpty(loadPort.SubstrateID[i])
                                ? loadPort.SubstrateID[i]
                                : loadPort.FoupID;
                            string lotID = loadPort.LotID != null && i < loadPort.LotID.Length && !string.IsNullOrEmpty(loadPort.LotID[i])
                                ? loadPort.LotID[i]
                                : loadPort.FoupID;

                            // 將 substID 存回 LoadPort 供後續 EFEMRun 使用
                            if (loadPort.SubstrateID != null && i < loadPort.SubstrateID.Length)
                                loadPort.SubstrateID[i] = substID;
                            if (loadPort.LotID != null && i < loadPort.LotID.Length)
                                loadPort.LotID[i] = lotID;

                            Common.SecsgemForm.CreateSubstrate(substID, lotID, lpLocation);
                        }
                    }
                }
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.SlotMap, out err);
            }
            else
            {
                if (loadPort.Map())
                {
                    loadPort.GetWaferSlot2();
                    //Common.SecsgemForm.UpdateSV(TrimGap_EqpID.SlotMapStatus, (byte)2, out err);
                    // 0 = SLOTMAP NOT READ
                    // 1 = WAITING FOR HOST
                    // 2 = SLOTMAP VERIFIKATION OK
                    // 3 = SLOTMAP VERIFICATION FAILED

                    //Reason
                    // 0 = VERIFICATION NEEDED
                    // 1 = VERIFICATION BY EQUIPMENT UNSUCCESSFUL                     
                    // 2 = READ FAIL, 
                    // 3 = IMPROPER SUBSTRATE POSITION
                    for (int i = 0; i < loadPort.Slot.Length; i++)
                    {
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.SlotMap_1 + i, (byte)loadPort.Slot[i], out err); 
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Reason, (byte)0, out err); // map 成功沒有error ，等待驗證
                        if (loadPort.Slot[i] > 1)
                        {
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Reason, (byte)3, out err); // Wafer有放歪
                        }
                    }

                    if (loadPort.pn == LoadPort.Pn.P1)
                    {
                        Common.SecsgemForm.EventReportSend(TrimGap_EqpID.LP1_CarrierMapped, out err);
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                    }
                    else if (loadPort.pn == LoadPort.Pn.P2)
                    {
                        Common.SecsgemForm.EventReportSend(TrimGap_EqpID.LP2_CarrierMapped, out err);
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                    }
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);

                    if (fram.m_SecsgemType == 1)
                    {
                        Common.SecsgemForm.SetCarrierAttr_SlotMap(loadPort.FoupID, loadPort.Slot);
                        Common.SecsgemForm.SetCarrierStatus_SlotMap(loadPort.FoupID, SlotMapState.WAITING_FOR_HOST);

                        // ⭐ 為每個有 Wafer 的 Slot 建立 Substrate 物件
                        string lpLocation = loadPort.pn == LoadPort.Pn.P1 ? "LOADPORT1" : "LOADPORT2";
                        for (int i = 0; i < loadPort.Slot.Length; i++)
                        {
                            if (loadPort.Slot[i] == 1) // Wafer 存在
                            {
                                string substID = loadPort.SubstrateID != null && i < loadPort.SubstrateID.Length && !string.IsNullOrEmpty(loadPort.SubstrateID[i])
                                    ? loadPort.SubstrateID[i]
                                    : loadPort.FoupID;
                                string lotID = loadPort.LotID != null && i < loadPort.LotID.Length && !string.IsNullOrEmpty(loadPort.LotID[i])
                                    ? loadPort.LotID[i]
                                    : loadPort.FoupID;

                                // 將 substID 存回 LoadPort 供後續 EFEMRun 使用
                                if (loadPort.SubstrateID != null && i < loadPort.SubstrateID.Length)
                                    loadPort.SubstrateID[i] = substID;
                                if (loadPort.LotID != null && i < loadPort.LotID.Length)
                                    loadPort.LotID[i] = lotID;

                                Common.SecsgemForm.CreateSubstrate(substID, lotID, lpLocation);
                            }
                        }
                    }
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.SlotMap, out err);
                }
                else
                {
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Reason, (byte)2, out err); // 2 = READ FAIL
                    Console.WriteLine("Map Fail " + loadPort.pn.ToString());
                    InsertLog.SavetoDB(133, loadPort.pn.ToString());
                    EFEMGotoErrorCheckStep();
                }
            }
        }

        private static void EFEMRun(ref LoadPort loadPort, ref EFEMStep eFEMStep, ref EFEMStep eFEMStep_Back)
        {
            bool rtn;
            switch (eFEMStep)
            {
                case EFEMStep.JudgeStep:
                    // 兩站 + robot *2 的排列組合
                    // 兩站都沒有 & Robot 都沒有 = 重頭開始
                    // 還要再加入判斷 重新map
                    if (fram.m_simulateRun == 0)
                    {
                        rtn = Common.EFEM.Robot.GetStatus();
                        rtn = Common.EFEM.Aligner.GetStatus();
                        if (!rtn)
                        {
                            Console.WriteLine(Common.EFEM.Aligner.ErrorDescription);
                            //eFEMStep = EFEMStep.ErrorCheckSts;
                        }
                    }
                    loadPort.AutoGetSlot();
                    fram.PT_PLC_AutoRunEFEM_RetryCount = 0; //PTPLC的retry次數做重置

                    if(!Flag.AbortFlag)   //正常情況
                    {
                        #region Stage 沒有

                        if (!Common.EFEM.Stage1.WaferPresence)
                        {
                            #region Aligner 沒有

                            if (!Common.EFEM.Aligner.WaferPresence)
                            {
                                #region 兩隻arm的排列組合 Lower / Upper

                                #region 0 0 0 0

                                if (!Common.EFEM.Robot.WaferPresence_Lower && !Common.EFEM.Robot.WaferPresence_Upper)
                                {
                                    if (loadPort.ReadryToLoadWafer && ((sram.RunningCJ == "" || sram.RunningCJ == null) || !(sram.RunningPJ == "" || sram.RunningPJ == null))) //沒有使用CJ的情況(舊方法) 或 有用CJ但沒有在跑的PJ
                                    {
                                        eFEMStep = EFEMStep.WaferGetFormLoadPort;
                                    }
                                    else
                                    {
                                        if(sram.RunningCJ == "" || sram.RunningCJ == null)
                                            eFEMStep = EFEMStep.Finish;   //沒有使用CJ的情況(舊方法)
                                        else
                                        {
                                            if (sram.RunningPJ == "" || sram.RunningPJ == null)   //有CJ沒有正在跑的PJ，看看還有沒有沒run到的PJ
                                            {
                                                if (Flag.CJStopFlag || Flag.CJAbortFlag)  //收到CJSTOP 或 CJABORT (不論Queue是否還有PJ，都要走異常完成路徑)
                                                {
                                                    // ⭐ 先推動 CJ 狀態到 COMPLETED (1: Stop, 2: Abort)
                                                    Common.SecsgemForm.ChangeControlJobState(sram.RunningCJ, ControlJobState.COMPLETED, Flag.CJStopFlag ? 1 : 2);

                                                    if (Flag.PJDeleteWithCJFlag)
                                                        Common.SecsgemForm.DeleteControlJobWithAssociatedProcessJob(sram.RunningCJ, out err);
                                                    else
                                                        Common.SecsgemForm.DeleteControlJob(sram.RunningCJ, out err);

                                                    Common.SecsgemForm.SetCarrierStatus_Accessing(Common.EFEM.LoadPort_Run.FoupID, CarrierAccessingState.CARRIER_STOPPED);
                                                    _carrierAccessingAlreadySet = true;
                                                    sram.RunningCJ = "";
                                                    Flag.CJStopFlag = false;
                                                    Flag.CJAbortFlag = false;
                                                    eFEMStep = EFEMStep.Finish;
                                                }
                                                else if (sram.QueuePJ.Count == 0)  //pj run光了 (正常完成)
                                                {
                                                    Common.SecsgemForm.ChangeControlJobState(sram.RunningCJ, ControlJobState.COMPLETED, 0); //CJ完成
                                                    Common.SecsgemForm.SetCarrierStatus_Accessing(Common.EFEM.LoadPort_Run.FoupID, CarrierAccessingState.CARRIER_COMPLETE);
                                                    sram.RunningCJ = "";
                                                    eFEMStep = EFEMStep.Finish;
                                                }
                                                else
                                                {
                                                    sram.RunningPJ = sram.QueuePJ[0];  //run一個pj
                                                    int PJ_ok = Common.SecsgemForm.ChangeProcessJobState(sram.RunningPJ, ProcessJobState.SETTING_UP);
                                                    if (PJ_ok == 0)
                                                    {
                                                        Common.SecsgemForm.GetProcessJobAttr(sram.RunningPJ, out sram.PJInfo.pauseEvent, out sram.PJInfo.PJState, out sram.PJInfo.carrierID, out sram.PJInfo.slot,
                                                            out sram.PJInfo.PRType, out sram.PJInfo.bStart, out sram.PJInfo.recMethod, out sram.PJInfo.recID, out sram.PJInfo.recVarList, out err);
                                                        if (sram.PJInfo.bStart)
                                                            Common.SecsgemForm.ChangeProcessJobState(sram.RunningPJ, ProcessJobState.PROCESSING);
                                                        else
                                                        {
                                                            Common.SecsgemForm.ChangeProcessJobState(sram.RunningPJ, ProcessJobState.WAITING_FOR_START);  //不知道收什麼指令來繼續 可能要透過修改sram.PJInfo.bStart
                                                            Flag.PauseFlag = true;
                                                            Flag.AutoidleFlag = false;
                                                            Flag.Autoidle_LocalFlag = false;  //借用PAUSE動作，等待START命令
                                                            //Common.SecsgemForm.ChangeProcessJobState(sram.RunningPJ, ProcessJobState.PROCESSING);
                                                        }
                                                        Common.ChangeRecipe(sram.PJInfo.recID);
                                                        for (int i = 0; i < 25; i++)
                                                        {
                                                            if (loadPort.slot_Status[i] == EFEM.slot_status.Ready || loadPort.slot_Status[i] == EFEM.slot_status.ProcessEnd)
                                                            {
                                                                if (sram.PJInfo.slot[i] == 1)
                                                                {
                                                                    loadPort.Update_slot_Status(i + 1, EFEM.slot_status.Ready);
                                                                }
                                                                else
                                                                {
                                                                    loadPort.Update_slot_Status(i + 1, EFEM.slot_status.ProcessEnd);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else  //不知為何失敗，可能是實際找不到這個PJ，把現在這個PJ刪掉
                                                    {
                                                        sram.QueuePJ.RemoveAt(0);
                                                        sram.RunningPJ = "";
                                                    }
                                                }
                                            }
                                            else     //有CJ有PJ，但是已經沒有還需要跑的slot，pj完成
                                            {
                                                if (Flag.PJStopFlag) //是PJ STOP過來的
                                                {
                                                    // ⭐ 確保 DVID 4160 有值
                                                    byte doneWaferCount = 0;
                                                    for (int si = 0; si < loadPort.slot_Status.Length; si++)
                                                        if (loadPort.slot_Status[si] == EFEM.slot_status.ProcessEnd) doneWaferCount++;
                                                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PJProcessedWaferCount, doneWaferCount, out err);

                                                    Common.SecsgemForm.ChangeProcessJobState(sram.RunningPJ, ProcessJobState.STOPPED);
                                                    Flag.PJStopFlag = false;
                                                }
                                                else if (Flag.PJAbortFlag) //是PJ ABORT過來的
                                                {
                                                    // ⭐ 確保 DVID 4160 有值
                                                    byte doneWaferCount = 0;
                                                    for (int si = 0; si < loadPort.slot_Status.Length; si++)
                                                        if (loadPort.slot_Status[si] == EFEM.slot_status.ProcessEnd) doneWaferCount++;
                                                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PJProcessedWaferCount, doneWaferCount, out err);

                                                    Common.SecsgemForm.ChangeProcessJobState(sram.RunningPJ, ProcessJobState.ABORTED);
                                                    Flag.PJAbortFlag = false;
                                                }
                                                else
                                                {
                                                    // ⭐ 先計算本次 PJ 完成的 Wafer 數量，並更新 SVID 4160 (PJProcessedWaferCount_CompletedNormally)
                                                    byte doneWaferCount = 0;
                                                    for (int si = 0; si < loadPort.slot_Status.Length; si++)
                                                    {
                                                        if (loadPort.slot_Status[si] == EFEM.slot_status.ProcessEnd)
                                                            doneWaferCount++;
                                                    }
                                                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PJProcessedWaferCount, doneWaferCount, out err);

                                                    Common.SecsgemForm.ChangeProcessJobState(sram.RunningPJ, ProcessJobState.PROCESS_COMPLETE);

                                                    // ⭐ Process Job Completed Event
                                                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)(loadPort.pn == LoadPort.Pn.P1 ? 1 : 2), out err);
                                                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);

                                                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.PJ_JobComplete, out err);
                                                    Gem300Monitor.AddSend("ProcessJobCompleted  PJ=" + sram.RunningPJ + " WaferCount=" + doneWaferCount);
                                                }
                                                    
                                                sram.QueuePJ.RemoveAt(0);
                                                sram.RunningPJ = "";
                                            }
                                        }
                                    }
                                }

                                #endregion 0 0 0 0

                                #region 0 1 0 0

                                else if (!Common.EFEM.Robot.WaferPresence_Lower && Common.EFEM.Robot.WaferPresence_Upper)
                                {
                                    if (Common.EFEM.Robot.slot_Status[(int)Robot.ArmID.UpperArm - 1] == EFEM.slot_status.ProcessEnd)
                                    {
                                        eFEMStep = EFEMStep.WaferPut2LoadPort; // 做完才回LoadPort
                                    }
                                    else
                                    {
                                        eFEMStep = EFEMStep.WaferPut2Stage1; // 還沒做完去Stage
                                    }
                                }

                                #endregion 0 1 0 0

                                #region 1 0 0 0

                                else if (Common.EFEM.Robot.WaferPresence_Lower && !Common.EFEM.Robot.WaferPresence_Upper)
                                {
                                    eFEMStep = EFEMStep.WaferPut2Aligner1;
                                }

                                #endregion 1 0 0 0

                                #region 1 1 0 0

                                else if (Common.EFEM.Robot.WaferPresence_Lower && Common.EFEM.Robot.WaferPresence_Upper)
                                {
                                    eFEMStep = EFEMStep.WaferPut2Aligner1;
                                }

                                #endregion 1 1 0 0

                                #region ? ? error

                                else
                                {
                                }

                                #endregion ? ? error

                                #endregion 兩隻arm的排列組合 Lower / Upper
                            }

                            #endregion Aligner 沒有

                            #region Aligner 有

                            else
                            {
                                #region 兩隻arm的排列組合 Lower / Upper

                                #region 0 0 1 0

                                if (!Common.EFEM.Robot.WaferPresence_Lower && !Common.EFEM.Robot.WaferPresence_Upper)
                                {
                                    if (loadPort.ReadryToLoadWafer)
                                    {
                                        if (!Common.EFEM.Aligner.Alignement_Done)
                                        {
                                            eFEMStep = EFEMStep.DoAlignement;
                                        }
                                        else
                                        {
                                            eFEMStep = EFEMStep.WaferGetFormLoadPort;
                                        }
                                    }
                                    else
                                    {
                                        if (!Common.EFEM.Aligner.Alignement_Done)
                                        {
                                            eFEMStep = EFEMStep.DoAlignement;
                                        }
                                        else
                                        {
                                            eFEMStep = EFEMStep.WaferGetFormAligner1;
                                        }
                                    }
                                }

                                #endregion 0 0 1 0

                                #region 0 1 1 0

                                else if (!Common.EFEM.Robot.WaferPresence_Lower && Common.EFEM.Robot.WaferPresence_Upper)
                                {
                                    if (Common.EFEM.Robot.slot_Status[(int)Robot.ArmID.UpperArm - 1] == EFEM.slot_status.ProcessEnd)
                                    {
                                        eFEMStep = EFEMStep.WaferPut2LoadPort;
                                    }
                                    else
                                    {
                                        eFEMStep = EFEMStep.WaferPut2Stage1;
                                    }
                                }

                                #endregion 0 1 1 0

                                #region 1 0 1 0

                                else if (Common.EFEM.Robot.WaferPresence_Lower && !Common.EFEM.Robot.WaferPresence_Upper)
                                {
                                    eFEMStep = EFEMStep.WaferGetFormAligner1;
                                }

                                #endregion 1 0 1 0

                                #region 1 1 1 0

                                else if (Common.EFEM.Robot.WaferPresence_Lower && Common.EFEM.Robot.WaferPresence_Upper)
                                {
                                    //eFEMStep = EFEMStep.WaferPut2LoadPort;
                                }

                                #endregion 1 1 1 0

                                #region ? ? error

                                else
                                {
                                }

                                #endregion ? ? error

                                #endregion 兩隻arm的排列組合 Lower / Upper
                            }

                            #endregion Aligner 有
                        }

                        #endregion Stage 沒有

                        #region Stage有

                        else
                        {
                            #region Aligner 沒有

                            if (!Common.EFEM.Aligner.WaferPresence)
                            {
                                #region 兩隻arm的排列組合 Lower / Upper

                                #region 0 0 0 1

                                // Stage有 兩手空空
                                if (!Common.EFEM.Robot.WaferPresence_Lower && !Common.EFEM.Robot.WaferPresence_Upper)
                                {
                                    eFEMStep = EFEMStep.WaferGetFormStage1;
                                }

                                #endregion 0 0 0 1

                                #region 0 1 0 1

                                else if (!Common.EFEM.Robot.WaferPresence_Lower && Common.EFEM.Robot.WaferPresence_Upper)
                                {
                                    //eFEMStep = EFEMStep.WaferPut2Stage1; // 還沒做完去Stage
                                }

                                #endregion 0 1 0 1

                                #region 1 0 0 1

                                else if (Common.EFEM.Robot.WaferPresence_Lower && !Common.EFEM.Robot.WaferPresence_Upper)
                                {
                                }

                                #endregion 1 0 0 1

                                #region 1 1 0 1

                                else if (Common.EFEM.Robot.WaferPresence_Lower && Common.EFEM.Robot.WaferPresence_Upper)
                                {
                                }

                                #endregion 1 1 0 1

                                #region ? ? error

                                else
                                {
                                }

                                #endregion ? ? error

                                #endregion 兩隻arm的排列組合 Lower / Upper
                            }

                            #endregion Aligner 沒有

                            #region Aligner 有

                            else
                            {
                                #region 兩隻arm的排列組合 Lower / Upper

                                #region 0 0 1 1

                                // 兩站都有 兩手空空
                                // 先做 Do Alignement ->
                                if (!Common.EFEM.Robot.WaferPresence_Lower && !Common.EFEM.Robot.WaferPresence_Upper)
                                {
                                    if (!Common.EFEM.Aligner.Alignement_Done)
                                    {
                                        eFEMStep = EFEMStep.DoAlignement;
                                    }
                                    else
                                    {
                                        eFEMStep = EFEMStep.WaferGetFormStage1;
                                    }
                                }

                                #endregion 0 0 1 1

                                #region 0 1 1 1

                                else if (!Common.EFEM.Robot.WaferPresence_Lower && Common.EFEM.Robot.WaferPresence_Upper)
                                {
                                    //eFEMStep = EFEMStep.;
                                }

                                #endregion 0 1 1 1

                                #region 1 0 1 1

                                else if (Common.EFEM.Robot.WaferPresence_Lower && !Common.EFEM.Robot.WaferPresence_Upper)
                                {
                                    //eFEMStep = EFEMStep.WaferGetFormStage1;
                                }

                                #endregion 1 0 1 1

                                #region 1 1 1 1

                                else if (Common.EFEM.Robot.WaferPresence_Lower && Common.EFEM.Robot.WaferPresence_Upper)
                                {
                                    //eFEMStep = EFEMStep.;
                                }

                                #endregion 1 1 1 1

                                #region ? ? error

                                else
                                {
                                }

                                #endregion ? ? error

                                #endregion 兩隻arm的排列組合 Lower / Upper
                            }

                            #endregion Aligner 有
                        }

                        #endregion Stage有
                    }
                    else                 //Abort時
                    {
                        //Lower Arm有
                        if(Common.EFEM.Robot.WaferPresence_Lower)
                        {
                            InsertLog.SavetoDB(61, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_lower);
                            switch (loadPort.pn)
                            {
                                case LoadPort.Pn.P1:
                                    rtn = Common.EFEM.Robot.WaferPut(Robot.ArmID.LowerArm, Robot.Pn.P1, Common.EFEM.Robot.Slot_Arm_lower, loadPort, EFEM.slot_status.ProcessEnd);
                                    break;

                                case LoadPort.Pn.P2:
                                    rtn = Common.EFEM.Robot.WaferPut(Robot.ArmID.LowerArm, Robot.Pn.P2, Common.EFEM.Robot.Slot_Arm_lower, loadPort, EFEM.slot_status.ProcessEnd);
                                    break;

                                default:
                                    rtn = false;
                                    break;
                            }
                            if (rtn)
                            {
                                eFEMStep_Back = EFEMStep.JudgeStep;
                            }
                            else
                            {
                                InsertLog.SavetoDB(104, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_lower + ", msg：" + loadPort.ErrorDescription); //
                                EFEMGotoErrorCheckStep();
                            }
                            Common.EFEM.Robot.GetStatus();
                        }
                        else if(Common.EFEM.Robot.WaferPresence_Upper)   //Lower Arm 沒有了 Upper Arm 有
                        {
                            InsertLog.SavetoDB(61, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_upper);
                            switch (loadPort.pn)
                            {
                                case LoadPort.Pn.P1:
                                    rtn = Common.EFEM.Robot.WaferPut(Robot.ArmID.UpperArm, Robot.Pn.P1, Common.EFEM.Robot.Slot_Arm_upper, loadPort, EFEM.slot_status.ProcessEnd);
                                    break;

                                case LoadPort.Pn.P2:
                                    rtn = Common.EFEM.Robot.WaferPut(Robot.ArmID.UpperArm, Robot.Pn.P2, Common.EFEM.Robot.Slot_Arm_upper, loadPort, EFEM.slot_status.ProcessEnd);
                                    break;

                                default:
                                    rtn = false;
                                    break;
                            }
                            if (rtn)
                            {
                                eFEMStep_Back = EFEMStep.JudgeStep;
                            }
                            else
                            {
                                InsertLog.SavetoDB(104, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_upper + ", msg：" + loadPort.ErrorDescription); //
                                EFEMGotoErrorCheckStep();
                            }
                            Common.EFEM.Robot.GetStatus();
                        }
                        else if(Common.EFEM.Aligner.WaferPresence) //Lower Arm Upper Arm 沒有了 Aligner 有
                        {
                            eFEMStep = EFEMStep.WaferGetFormAligner1; //借用 把Aligner Wafer拿在手上，之後又回到上面手上有Wafer情況
                        }
                        else if (Common.io.In(IOName.In.StageWafer在席)) //Lower Arm Upper Arm 沒有了 Aligner 沒有 stage 有
                        {
                            if (machineType == MachineType.AP6 || machineType == MachineType.AP6II)
                            {
                                // 解真空 & 汽缸up
                                if (Common.io.In(IOName.In.真空平台_負壓檢))
                                {
                                    Common.io.WriteOut(IOName.Out.平台真空電磁閥, false);
                                    Common.io.WriteOut(IOName.Out.平台破真空電磁閥, true);
                                    if (fram.m_simulateRun != 0)
                                        Common.io.WriteIn(IOName.In.真空平台_負壓檢, false);
                                }
                                if(machineType == MachineType.AP6)
                                {
                                    if (!Common.io.In(IOName.In.Wafer汽缸_抬起檢) && (fram.m_Hardware_PT == 0 || Common.PTForm.GetPointMoveFinish(9)))
                                    {
                                        Common.io.WriteOut(IOName.Out.Wafer汽缸_降下, false);
                                        Common.io.WriteOut(IOName.Out.Wafer汽缸_抬起, true);
                                        if (fram.m_simulateRun != 0)
                                            Common.io.WriteIn(IOName.In.Wafer汽缸_抬起檢, true);
                                    }
                                }
                                else if(machineType == MachineType.AP6II)
                                {
                                    if (!Common.io.In(IOName.In.Wafer汽缸_抬起檢) && Math.Abs((Common.motion.Get_FBPos(Mo.AxisNo.AP6II_Z2))) < 0.1 && Math.Abs((Common.motion.Get_FBPos(Mo.AxisNo.AP6II_X))) < 0.1)
                                    {
                                        Common.io.WriteOut(IOName.Out.Wafer汽缸_降下, false);
                                        Common.io.WriteOut(IOName.Out.Wafer汽缸_抬起, true);
                                        if (fram.m_simulateRun != 0)
                                            Common.io.WriteIn(IOName.In.Wafer汽缸_抬起檢, true);
                                    }
                                }
                                SpinWait.SpinUntil(() => (!Common.io.In(IOName.In.真空平台_負壓檢) && Common.io.In(IOName.In.Wafer汽缸_抬起檢)), 10000);
                                if (!Common.io.In(IOName.In.Wafer汽缸_抬起檢))
                                {
                                    InsertLog.SavetoDB(103, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Stage1.Slot + ", msg：" + "Wafer汽缸_抬起檢"); //
                                    EFEMGotoErrorCheckStep();
                                    break;
                                }
                                else if (Common.io.In(IOName.In.真空平台_負壓檢))
                                {
                                    InsertLog.SavetoDB(103, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Stage1.Slot + ", msg：" + "真空平台_負壓檢"); //
                                    EFEMGotoErrorCheckStep();
                                    break;
                                }
                                else if ((fram.m_Hardware_PT == 1 && !Common.PTForm.GetPointMoveFinish(9)))
                                {
                                    InsertLog.SavetoDB(103, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Stage1.Slot + ", msg：" + "PT PLC未退至安全位置"); //
                                    EFEMGotoErrorCheckStep();
                                    break;
                                }
                                else if ((machineType == MachineType.AP6II && !(Math.Abs(Common.motion.Get_FBPos(Mo.AxisNo.AP6II_Z2)) < 0.1)))
                                {
                                    InsertLog.SavetoDB(103, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Stage1.Slot + ", msg：" + "AP6II_Z2未退至安全位置"); //
                                    EFEMGotoErrorCheckStep();
                                    break;
                                }
                                else if ((machineType == MachineType.AP6II && !(Math.Abs(Common.motion.Get_FBPos(Mo.AxisNo.AP6II_X)) < 0.1)))
                                {
                                    InsertLog.SavetoDB(103, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Stage1.Slot + ", msg：" + "AP6II_X未退至安全位置"); //
                                    EFEMGotoErrorCheckStep();
                                    break;
                                }
                                Common.EFEM.Stage1.Ready = true;
                                Common.io.WriteOut(IOName.Out.StageWaferReady, true);
                                Common.io.WriteOut(IOName.Out.StageWafer在席, true);
                                rtn = Common.EFEM.Robot.WaferGet(Robot.ArmID.UpperArm, Robot.Pn.Stage1, Common.EFEM.Stage1.Slot, Common.EFEM.LoadPort_Run, EFEM.slot_status.Ready);
                                if (rtn)
                                {
                                    Common.EFEM.Robot.Update_slot_Status(Robot.ArmID.UpperArm, EFEM.slot_status.Ready);
                                    SpinWait.SpinUntil(() => false, 1000);
                                    Common.io.WriteOut(IOName.Out.StageWafer在席, false);
                                }
                                else
                                {
                                    InsertLog.SavetoDB(103, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_upper + ", msg：" + loadPort.ErrorDescription); //
                                    EFEMGotoErrorCheckStep();
                                    break;
                                }
                            }
                            else if (machineType == MachineType.N2)
                            {
                                // 先移動 在解真空
                                Common.motion.PosMove(Mo.AxisNo.DD, 0);
                                Common.motion.PosMove(Mo.AxisNo.X, AutoRunStage.Homepos.N2.WaferSwitch.X);
                                Common.motion.PosMove(Mo.AxisNo.Y, AutoRunStage.Homepos.N2.WaferSwitch.Y);

                                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 15000);
                                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.X), 15000);
                                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.Y), 15000);
                                if (!Common.motion.MotionDone(Mo.AxisNo.DD) || !Common.motion.MotionDone(Mo.AxisNo.X) || !Common.motion.MotionDone(Mo.AxisNo.Y))
                                {
                                    if (!Common.motion.MotionDone(Mo.AxisNo.DD))
                                        InsertLog.SavetoDB(103, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Stage1.Slot + ", msg：" + "DD未達取片位置"); //
                                    if (!Common.motion.MotionDone(Mo.AxisNo.X))
                                        InsertLog.SavetoDB(103, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Stage1.Slot + ", msg：" + "X軸未達取片位置"); //
                                    if (!Common.motion.MotionDone(Mo.AxisNo.Y))
                                        InsertLog.SavetoDB(103, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Stage1.Slot + ", msg：" + "Y軸未達取片位置"); //
                                    EFEMGotoErrorCheckStep();
                                    break;
                                }

                                // 解真空 & 汽缸up
                                if (Common.io.In(IOName.In.真空平台_負壓檢))
                                {
                                    Common.io.WriteOut(IOName.Out.平台真空電磁閥, false);
                                    Common.io.WriteOut(IOName.Out.平台破真空電磁閥, true);
                                    if (fram.m_simulateRun != 0)
                                        Common.io.WriteIn(IOName.In.真空平台_負壓檢, false);
                                }
                                SpinWait.SpinUntil(() => !Common.io.In(IOName.In.真空平台_負壓檢), 3000);
                                if (Common.io.In(IOName.In.真空平台_負壓檢))
                                {
                                    InsertLog.SavetoDB(103, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Stage1.Slot + ", msg：" + "真空平台_破真空異常"); //
                                    EFEMGotoErrorCheckStep();
                                    break;
                                }

                                Common.EFEM.Stage1.Ready = true;
                                Common.io.WriteOut(IOName.Out.StageWaferReady, true);
                                Common.io.WriteOut(IOName.Out.StageWafer在席, true);
                                rtn = Common.EFEM.Robot.WaferGet(Robot.ArmID.UpperArm, Robot.Pn.Stage1, Common.EFEM.Stage1.Slot, Common.EFEM.LoadPort_Run, EFEM.slot_status.Ready);
                                if (rtn)
                                {
                                    Common.EFEM.Robot.Update_slot_Status(Robot.ArmID.UpperArm, EFEM.slot_status.Ready);
                                    SpinWait.SpinUntil(() => false, 1000);
                                    Common.io.WriteOut(IOName.Out.StageWafer在席, false);
                                }
                                else
                                {
                                    InsertLog.SavetoDB(103, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Stage1.Slot + ", msg：" + loadPort.ErrorDescription); //
                                    EFEMGotoErrorCheckStep();
                                    break;
                                }
                            }
                        }
                        else
                        {
                            //Abort完成
                            loadPort.Busy = false;
                            if (loadPort.pn == LoadPort.Pn.P1)
                            {
                                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                                FoupHome(ref Common.EFEM.LoadPort1);
                            }
                            else if (loadPort.pn == LoadPort.Pn.P2)
                            {
                                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                                FoupHome(ref Common.EFEM.LoadPort2);
                            }
                            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);
                            //Common.CGWrapper.EventReportSend(TrimGap_EqpID.MeasureResultSend);  // 這邊應該要回報一個Abort Finish
                            for (int i = 0; i < 25; i++)
                            {
                                for (int j = 0; j < sram.Recipe.Rotate_Count; j++)
                                {
                                    fram.EFEMSts.H1[i, j] = 0;
                                    fram.EFEMSts.W1[i, j] = 0;
                                    fram.EFEMSts.H2[i, j] = 0;
                                    fram.EFEMSts.W2[i, j] = 0;
                                }
                            }
                            fram.EFEMSts.bLimitCheckPass = true;
                            bWEFEMAutoRun.ReportProgress(99);
                            Flag.AbortFlag = false;
                        }
                    }

                    bWEFEMAutoRun.ReportProgress(99);
                    break;

                case EFEMStep.DoAlignement:
                    // 做兩次

                    eFEMStep_Back = EFEMStep.DoAlignement;

                    if (Common.EFEM.Aligner.Alignment())
                    {
                        Common.EFEM.Aligner.Alignement_Done = true;
                        eFEMStep = EFEMStep.JudgeStep;
                    }
                    else //timeout
                    {
                        if (Common.EFEM.Aligner.ResetError())
                        {
                            Common.EFEM.Aligner.Home();//20240916
                            if (Common.EFEM.Aligner.Alignment())
                            {
                                Common.EFEM.Aligner.Alignement_Done = true;
                                eFEMStep = EFEMStep.JudgeStep;

                                eFEMStep_Back = EFEMStep.JudgeStep;
                            }
                            else
                            {
                                Console.WriteLine("Alignment Fail " + Common.EFEM.Aligner.ErrorDescription);
                                Common.EFEM.Aligner.ResetError();
                                Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EFEM_Aligner1_AlignmentError, true, out err);
                                InsertLog.SavetoDB(134, Common.EFEM.Aligner.ErrorDescription);
                                EFEMGotoErrorCheckStep();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Alignment Fail " + Common.EFEM.Aligner.ErrorDescription);
                            Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EFEM_Aligner1_AlignmentError, true, out err);
                            InsertLog.SavetoDB(134, Common.EFEM.Aligner.ErrorDescription);
                            EFEMGotoErrorCheckStep();
                        }
                    }
                    bWEFEMAutoRun.ReportProgress(99);
                    break;

                case EFEMStep.WaferGetFormLoadPort:

                    eFEMStep_Back = EFEMStep.WaferGetFormLoadPort;

                    if (!Common.EFEM.Robot.WaferPresence_Lower)
                    {
                        InsertLog.SavetoDB(60, "Pn：" + loadPort.pn + ", Slot：" + loadPort.AutoGetSlot());
                        switch (loadPort.pn)
                        {
                            case LoadPort.Pn.P1:
                                rtn = Common.EFEM.Robot.WaferGet(Robot.ArmID.LowerArm, Robot.Pn.P1, loadPort.AutoGetSlot(), loadPort, EFEM.slot_status.ProcessingAligner1);
                                break;

                            case LoadPort.Pn.P2:
                                rtn = Common.EFEM.Robot.WaferGet(Robot.ArmID.LowerArm, Robot.Pn.P2, loadPort.AutoGetSlot(), loadPort, EFEM.slot_status.ProcessingAligner1);
                                break;

                            default:
                                rtn = false;
                                break;
                        }
                        if (rtn)
                        {
                            eFEMStep_Back = EFEMStep.JudgeStep;
                        }
                        else
                        {
                            InsertLog.SavetoDB(101, "Pn：" + loadPort.pn + ", Slot：" + loadPort.AutoGetSlot() + ", msg：" + loadPort.ErrorDescription);
                            EFEMGotoErrorCheckStep();
                        }

                        Common.EFEM.Robot.GetStatus();
                        if (Common.EFEM.Robot.WaferPresence_Lower)
                        {
                            bWEFEMAutoRun.ReportProgress(10);
                            eFEMStep = EFEMStep.JudgeStep;
                        }
                        else
                        {
                            Console.WriteLine(Common.EFEM.Robot.ErrorDescription);
                            EFEMGotoErrorCheckStep();
                        }
                    }
                    bWEFEMAutoRun.ReportProgress(99);
                    break;

                case EFEMStep.WaferGetFormAligner1:

                    eFEMStep_Back = EFEMStep.WaferGetFormAligner1;

                    if (!Common.EFEM.Aligner.AlignerVacuum(Aligner.OnOff.Off))
                    {
                        if (!Common.EFEM.Aligner.AlignerVacuum(Aligner.OnOff.Off))
                        {
                            Console.WriteLine("WaferGetFormAligner1 Fail");
                            EFEMGotoErrorCheckStep();
                        }
                    }

                    Common.EFEM.Aligner.GetStatus();
                    Common.EFEM.Robot.GetStatus();

                    if (!Common.EFEM.Robot.WaferPresence_Upper && Common.EFEM.Aligner.WaferPresence)
                    {
                        SpinWait.SpinUntil(() => false, 100); //解真空後延遲 100ms 才取片
                        rtn = Common.EFEM.Robot.WaferGet(Robot.ArmID.UpperArm, Robot.Pn.Aligner1, Common.EFEM.Aligner.Slot, loadPort, EFEM.slot_status.ProcessingStage1);
                        InsertLog.SavetoDB(64, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_upper);
                        if (rtn)
                        {
                            bWEFEMAutoRun.ReportProgress(10);
                            eFEMStep = EFEMStep.JudgeStep;

                            eFEMStep_Back = EFEMStep.JudgeStep;
                        }
                        else
                        {
                            InsertLog.SavetoDB(102, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Aligner.Slot + ", msg：" + loadPort.ErrorDescription); //WaferGetFormAligner1
                            Console.WriteLine("WaferGetFormAligner1 Fail");
                            EFEMGotoErrorCheckStep();
                        }
                    }
                    bWEFEMAutoRun.ReportProgress(99);
                    break;

                case EFEMStep.WaferGetFormStage1:

                    eFEMStep_Back = EFEMStep.WaferGetFormStage1;

                    if (!Common.EFEM.Stage1.Busy && Common.EFEM.Stage1.Ready)
                    {
                        Common.EFEM.Robot.GetStatus();
                        if (!Common.EFEM.Robot.WaferPresence_Upper)
                        {
                            int savedSlot = Common.EFEM.Stage1.Slot; // ⭐ WaferGet 會清零 Stage1.Slot，先存起來
                            rtn = Common.EFEM.Robot.WaferGet(Robot.ArmID.UpperArm, Robot.Pn.Stage1, savedSlot, loadPort, EFEM.slot_status.ProcessEnd);
                            if (rtn)
                            {
                                // ⭐ Wafer Process End Event (CEID 152) — 每片 Wafer 從 Stage 取走時送出
                                if (fram.m_SecsgemType == 1)
                                {
                                    int slotIdx = savedSlot - 1; // Stage1.Slot 是 1-indexed, 陣列是 0-indexed
                                    string substID = loadPort.SubstrateID != null && slotIdx >= 0 && slotIdx < loadPort.SubstrateID.Length ? loadPort.SubstrateID[slotIdx] : "";
                                    string lotID = loadPort.LotID != null && slotIdx >= 0 && slotIdx < loadPort.LotID.Length ? loadPort.LotID[slotIdx] : "";
                                    if (string.IsNullOrEmpty(substID)) substID = loadPort.FoupID;
                                    if (string.IsNullOrEmpty(lotID)) lotID = loadPort.FoupID;

                                    Common.SecsgemForm.UpdateSV(341, substID, out err);
                                    Common.SecsgemForm.UpdateSV(342, lotID, out err);
                                    Common.SecsgemForm.UpdateSV(4170, savedSlot.ToString(), out err);
                                    Common.SecsgemForm.UpdateSV(346, "Stage1", out err);
                                    Common.SecsgemForm.UpdateSV(347, (byte)2, out err);  // PROCESSED
                                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)(loadPort.pn == LoadPort.Pn.P1 ? 1 : 2), out err);
                                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);
                                    Common.SecsgemForm.UpdateSV(4180, sram.RunningPJ, out err);
                                    Common.SecsgemForm.UpdateSV(4181, (byte)(loadPort.pn == LoadPort.Pn.P1 ? 1 : 2), out err);
                                    Common.SecsgemForm.UpdateSV(4182, loadPort.FoupID, out err);
                                    Common.SecsgemForm.UpdateSV(4183, sram.PJInfo.recID, out err);

                                    if (!string.IsNullOrEmpty(substID))
                                        Common.SecsgemForm.SetSubstrateStatus_Proc(substID, SubstProcState.PROCESSED);
                                    else
                                        Common.SecsgemForm.EventReportSend(152, out err);
                                    Gem300Monitor.AddSend("WaferProcessEnd (CEID152) LP=" + loadPort.pn + " Slot=" + savedSlot);
                                }

                                InsertLog.SavetoDB(65, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_upper);
                                eFEMStep_Back = EFEMStep.JudgeStep;
                            }
                            else
                            {
                                if (machineType == MachineType.AP6)
                                {
                                    if (!Common.io.In(IOName.In.StageWafer在席))
                                    {
                                        InsertLog.SavetoDB(103, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Stage1.Slot + ", msg： StageWafer在席 off");//WaferGetFormStage1
                                    }
                                    else if (!Common.io.In(IOName.In.Wafer汽缸_抬起檢))
                                    {
                                        InsertLog.SavetoDB(103, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Stage1.Slot + ", msg： Wafer汽缸_抬起檢 off");//WaferGetFormStage1
                                    }
                                    else if (!Common.io.Out(IOName.Out.StageWaferReady))
                                    {
                                        InsertLog.SavetoDB(103, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Stage1.Slot + ", msg： StageWaferReady off");//WaferGetFormStage1
                                    }
                                    else
                                    {
                                        InsertLog.SavetoDB(103, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Stage1.Slot + ", msg：" + loadPort.ErrorDescription);//WaferGetFormStage1
                                    }
                                }
                                else if (machineType == MachineType.N2)
                                {
                                }

                                EFEMGotoErrorCheckStep();
                            }
                            Common.EFEM.Robot.GetStatus();
                            if (Common.EFEM.Robot.WaferPresence_Upper)
                            {
                                bWEFEMAutoRun.ReportProgress(10);
                                SpinWait.SpinUntil(() => false, 1000); // 取放片完 delay 1秒 才更新 Stage1.WaferPresence 狀態

                                Common.io.WriteOut(IOName.Out.StageWafer在席, false);

                                Common.EFEM.Stage1.WaferPresence = false;
                                eFEMStep = EFEMStep.JudgeStep;

                                eFEMStep_Back = EFEMStep.JudgeStep;
                            }
                            else
                            {
                                Console.WriteLine("WaferGetFormStage1 Fail ");
                                Console.WriteLine(Common.EFEM.Robot.ErrorDescription);
                                EFEMGotoErrorCheckStep();

                                // error
                            }
                        }
                        else
                        {
                            eFEMStep = EFEMStep.JudgeStep;

                            eFEMStep_Back = EFEMStep.JudgeStep;
                        }
                    }
                    bWEFEMAutoRun.ReportProgress(99);
                    break;

                case EFEMStep.WaferPut2LoadPort:

                    eFEMStep_Back = EFEMStep.WaferPut2LoadPort;

                    if (Common.EFEM.Robot.WaferPresence_Upper)
                    {
                        InsertLog.SavetoDB(61, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_upper);
                        switch (loadPort.pn)
                        {
                            case LoadPort.Pn.P1:
                                rtn = Common.EFEM.Robot.WaferPut(Robot.ArmID.UpperArm, Robot.Pn.P1, Common.EFEM.Robot.Slot_Arm_upper, loadPort, EFEM.slot_status.ProcessEnd);
                                break;

                            case LoadPort.Pn.P2:
                                rtn = Common.EFEM.Robot.WaferPut(Robot.ArmID.UpperArm, Robot.Pn.P2, Common.EFEM.Robot.Slot_Arm_upper, loadPort, EFEM.slot_status.ProcessEnd);
                                break;

                            default:
                                rtn = false;
                                break;
                        }
                        if (rtn)
                        {
                            eFEMStep_Back = EFEMStep.JudgeStep;
                        }
                        else
                        {
                            InsertLog.SavetoDB(104, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_upper + ", msg：" + loadPort.ErrorDescription); //
                            EFEMGotoErrorCheckStep();
                        }
                        Common.EFEM.Robot.GetStatus();
                        if (!Common.EFEM.Robot.WaferPresence_Upper)
                        {
                            bWEFEMAutoRun.ReportProgress(10);
                            //Common.EFEM.Aligner.Alignement_Done = false;
                            fram.EFEMSts.TotalCompletedCount++;
                            eFEMStep = EFEMStep.JudgeStep;

                            eFEMStep_Back = EFEMStep.JudgeStep;
                        }
                        else
                        {
                            Console.WriteLine("WaferPut2LoadPort Fail ");
                            //eFEMStep = EFEMStep.ErrorCheckSts;
                            Console.WriteLine(Common.EFEM.Robot.ErrorDescription);
                            // error
                        }
                    }
                    bWEFEMAutoRun.ReportProgress(99);
                    break;

                case EFEMStep.WaferPut2Aligner1:

                    eFEMStep_Back = EFEMStep.WaferPut2Aligner1;

                    Common.EFEM.Robot.GetStatus();
                    Common.EFEM.Aligner.GetStatus();
                    if (Common.EFEM.Aligner.Home())
                    {
                        if (Common.EFEM.Robot.WaferPresence_Lower)
                        {
                            Common.EFEM.Aligner.Slot = Common.EFEM.Robot.Slot_Arm_lower;
                            InsertLog.SavetoDB(64, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_lower);
                            rtn = Common.EFEM.Robot.WaferPut(Robot.ArmID.LowerArm, Robot.Pn.Aligner1, Common.EFEM.Robot.Slot_Arm_lower, loadPort, EFEM.slot_status.ProcessingAligner1);
                            if (rtn)
                            {
                                bWEFEMAutoRun.ReportProgress(10);
                                Common.EFEM.Aligner.Alignement_Done = false;
                                eFEMStep = EFEMStep.JudgeStep;
                                eFEMStep_Back = EFEMStep.JudgeStep;
                            }
                            else
                            {
                                InsertLog.SavetoDB(105, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_lower + ", msg：" + loadPort.ErrorDescription); //
                                Common.EFEM.Aligner.Slot = 0;
                                EFEMGotoErrorCheckStep();
                            }
                        }
                        else if (Common.EFEM.Aligner.WaferPresence && !Common.EFEM.Robot.WaferPresence_Lower)
                        {
                            eFEMStep = EFEMStep.JudgeStep;
                            eFEMStep_Back = EFEMStep.JudgeStep;
                        }
                    }
                    else
                    {
                        EFEMGotoErrorCheckStep();
                    }
                    bWEFEMAutoRun.ReportProgress(99);
                    break;

                case EFEMStep.WaferPut2Stage1:

                    eFEMStep_Back = EFEMStep.WaferPut2Stage1;
                    if (machineType == MachineType.AP6 || machineType == MachineType.AP6II)
                    {
                        if (fram.m_Hardware_PT == 1)
                        {
                            if (Common.PTForm.GetPointMoveFinish(9))
                            {
                                fram.PT_PLC_AutoRunEFEM_RetryCount = 0;
                                Common.io.WriteOut(IOName.Out.Wafer汽缸_降下, false);
                                Common.io.WriteOut(IOName.Out.Wafer汽缸_抬起, true);
                                if (fram.m_simulateRun != 0)
                                    Common.io.WriteIn(IOName.In.Wafer汽缸_抬起檢, true);
                                SpinWait.SpinUntil(() => Common.io.In(IOName.In.Wafer汽缸_抬起檢), 10000);
                            }
                            else if(fram.PT_PLC_AutoRunEFEM_RetryCount < 1)
                            {
                                fram.PT_PLC_AutoRunEFEM_RetryCount++;
                                Common.PTForm.PointMove(9);
                                SpinWait.SpinUntil(() => Common.PTForm.GetPointMoveFinish(9), 3000);
                                break;
                            }
                            else
                            {
                                InsertLog.SavetoDB(TrimGap_EqpID.EQP_PT_PLC_MoveToSafePointError, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_upper + ", PT PLC 無法退回安全位置");
                                Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_PT_PLC_MoveToSafePointError, true, out err);
                                Flag.AlarmFlag = true;
                                fram.PT_PLC_AutoRunEFEM_RetryCount = 0;
                                MessageBox.Show("PT PLC 無法退回安全位置");
                                break;
                            }
                        }
                        else if (machineType == MachineType.AP6II)
                        { 
                            if (Math.Abs(Common.motion.Get_FBPos(Mo.AxisNo.AP6II_X)) < 0.1 && Math.Abs(Common.motion.Get_FBPos(Mo.AxisNo.AP6II_Z2)) < 0.1)
                            {
                                fram.PT_PLC_AutoRunEFEM_RetryCount = 0;
                                Common.io.WriteOut(IOName.Out.Wafer汽缸_降下, false);
                                Common.io.WriteOut(IOName.Out.Wafer汽缸_抬起, true);
                                if (fram.m_simulateRun != 0)
                                    Common.io.WriteIn(IOName.In.Wafer汽缸_抬起檢, true);
                                SpinWait.SpinUntil(() => Common.io.In(IOName.In.Wafer汽缸_抬起檢), 10000);
                            }
                            else if (fram.PT_PLC_AutoRunEFEM_RetryCount < 1)
                            {
                                fram.PT_PLC_AutoRunEFEM_RetryCount++;
                                Common.motion.PosMove(Mo.AxisNo.AP6II_X, 0);
                                Common.motion.PosMove(Mo.AxisNo.AP6II_Z2, 0);
                                SpinWait.SpinUntil(() => (Math.Abs(Common.motion.Get_FBPos(Mo.AxisNo.AP6II_X)) < 0.1 && Math.Abs(Common.motion.Get_FBPos(Mo.AxisNo.AP6II_Z2)) < 0.1), 5000);
                                break;
                            }
                            else
                            {
                                InsertLog.SavetoDB(TrimGap_EqpID.EQP_PT_PLC_MoveToSafePointError, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_upper + ", PT PLC 無法退回安全位置");
                                Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_PT_PLC_MoveToSafePointError, true, out err);
                                Flag.AlarmFlag = true;
                                fram.PT_PLC_AutoRunEFEM_RetryCount = 0;
                                if(!(Math.Abs(Common.motion.Get_FBPos(Mo.AxisNo.AP6II_X)) < 0.1))
                                    MessageBox.Show("AP6II_X軸無法退回安全位置");
                                if (!(Math.Abs(Common.motion.Get_FBPos(Mo.AxisNo.AP6II_Z2)) < 0.1))
                                    MessageBox.Show("AP6II_Z2軸無法退回安全位置");
                                break;
                            }
                        }
                        else
                        {
                            Common.io.WriteOut(IOName.Out.Wafer汽缸_降下, false);
                            Common.io.WriteOut(IOName.Out.Wafer汽缸_抬起, true);
                            if (fram.m_simulateRun != 0)
                                Common.io.WriteIn(IOName.In.Wafer汽缸_抬起檢, true);
                            SpinWait.SpinUntil(() => Common.io.In(IOName.In.Wafer汽缸_抬起檢), 10000);
                        }
                        if (!Common.io.In(IOName.In.Wafer汽缸_抬起檢))
                        {
                            InsertLog.SavetoDB(151);
                            InsertLog.SavetoDB(106, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_upper + ", Wafer汽缸_抬起檢 Time out");
                            Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_CylinderUpError, true, out err);
                            Flag.AlarmFlag = true;
                            break;
                        }
                        if (Common.EFEM.Stage1.Ready && Common.io.In(IOName.In.Wafer汽缸_抬起檢))
                        {
                            Common.EFEM.Stage1.Slot = Common.EFEM.Robot.Slot_Arm_upper;
                            InsertLog.SavetoDB(66, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_upper);
                            rtn = Common.EFEM.Robot.WaferPut(Robot.ArmID.UpperArm, Robot.Pn.Stage1, Common.EFEM.Stage1.Slot, loadPort, EFEM.slot_status.ProcessingStage1);
                            if (rtn)
                            {
                                eFEMStep_Back = EFEMStep.DoAlignement;

                                bWEFEMAutoRun.ReportProgress(10);
                                Common.EFEM.Stage1.WaferPresence = true;
                                Common.EFEM.Stage1.Measuredone = false;
                                AutoRunStage.FoupID = Common.EFEM.LoadPort_Run.FoupID;
                                AutoRunStage.Slot = Common.EFEM.Stage1.Slot;

                                //// ⭐ Wafer Process Start Event (CEID 151) — AP6/AP6II
                                if (fram.m_SecsgemType == 1)
                                {
                                    int slotIdx = Common.EFEM.Stage1.Slot - 1; // Stage1.Slot 是 1-indexed, 陣列是 0-indexed
                                    string substID = loadPort.SubstrateID != null && slotIdx >= 0 && slotIdx < loadPort.SubstrateID.Length ? loadPort.SubstrateID[slotIdx] : "";
                                    string lotID = loadPort.LotID != null && slotIdx >= 0 && slotIdx < loadPort.LotID.Length ? loadPort.LotID[slotIdx] : "";
                                    if (string.IsNullOrEmpty(substID)) substID = loadPort.FoupID;
                                    if (string.IsNullOrEmpty(lotID)) lotID = loadPort.FoupID;

                                    // RPID 55 Substrate DV
                                    Common.SecsgemForm.UpdateSV(341, substID, out err);                    // SubstrateID (STS_OBJID)
                                    Common.SecsgemForm.UpdateSV(342, lotID, out err);                      // LotID (STS_LOTID)
                                    Common.SecsgemForm.UpdateSV(4170, Common.EFEM.Stage1.Slot.ToString(), out err); // SlotID (STS_SLOTID)
                                    Common.SecsgemForm.UpdateSV(346, "Stage1", out err);                   // SubstrateLocationID
                                    Common.SecsgemForm.UpdateSV(347, (byte)1, out err);                    // SubstProcState = IN_PROCESS

                                    // Port / Carrier / Recipe
                                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)(loadPort.pn == LoadPort.Pn.P1 ? 1 : 2), out err);
                                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);
                                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.RecipeID, sram.PJInfo.recID, out err);

                                    // Active DV
                                    Common.SecsgemForm.UpdateSV(4180, sram.RunningPJ, out err);            // ActivePRJobID
                                    Common.SecsgemForm.UpdateSV(4181, (byte)(loadPort.pn == LoadPort.Pn.P1 ? 1 : 2), out err); // ActivePRJobPortID
                                    Common.SecsgemForm.UpdateSV(4182, loadPort.FoupID, out err);           // ActivePRJobCarrierID
                                    Common.SecsgemForm.UpdateSV(4183, sram.PJInfo.recID, out err);         // ActiveRecipeID

                                    if (!string.IsNullOrEmpty(substID))
                                        Common.SecsgemForm.SetSubstrateStatus_Proc(substID, SubstProcState.IN_PROCESS);
                                    else
                                        Common.SecsgemForm.EventReportSend(151, out err);
                                    Gem300Monitor.AddSend("WaferProcessStart (CEID151) LP=" + loadPort.pn + " Slot=" + Common.EFEM.Stage1.Slot);
                                }

                                SpinWait.SpinUntil(() => false, 500); // 取放片完 delay 1秒 才更新 Stage1.WaferPresence 狀態
                                Common.io.WriteOut(IOName.Out.StageWafer在席, true);
                                Common.EFEM.Aligner.GetStatus();
                                if (Common.EFEM.Aligner.WaferPresence) // 放下wafer後 檢查Aligner有沒有片
                                {
                                    if (!Common.EFEM.Aligner.Alignement_Done)  // 如果有片，先看Alignement_done， 應該都是還沒做，先做alignement
                                    {
                                        eFEMStep = EFEMStep.DoAlignement;
                                        eFEMStep_Back = EFEMStep.DoAlignement;
                                    }
                                    else
                                    {
                                        eFEMStep = EFEMStep.JudgeStep;
                                        eFEMStep_Back = EFEMStep.JudgeStep;
                                    }
                                }
                                else // 放下wafer後 檢查Aligner有沒有片，沒有片就去判斷步驟
                                {
                                    eFEMStep = EFEMStep.JudgeStep;
                                    eFEMStep_Back = EFEMStep.JudgeStep;
                                }

                                ///
                                if (Common.EFEM.Aligner.Alignement_Done) // 放下wafer後
                                {
                                    eFEMStep = EFEMStep.JudgeStep;
                                    eFEMStep_Back = EFEMStep.JudgeStep;
                                }
                                else
                                {
                                    if (Common.EFEM.Aligner.WaferPresence)
                                    {
                                        eFEMStep = EFEMStep.DoAlignement;

                                        eFEMStep_Back = EFEMStep.DoAlignement;
                                    }
                                    else
                                    {
                                        eFEMStep = EFEMStep.JudgeStep;
                                        eFEMStep_Back = EFEMStep.JudgeStep;
                                    }
                                }
                                ///
                            }
                            else
                            {
                                InsertLog.SavetoDB(106, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_upper); //
                                Common.EFEM.Stage1.Slot = 0;
                                EFEMGotoErrorCheckStep();
                            }
                        }
                    }
                    else if (machineType == MachineType.N2)
                    {
                        if (fram.m_simulateRun != 0)
                            Common.io.WriteIn(IOName.In.Wafer取放原點檢, true);
                        SpinWait.SpinUntil(() => Common.io.In(IOName.In.Wafer取放原點檢), 5000);
                        if (!Common.io.In(IOName.In.Wafer取放原點檢))
                        {
                            InsertLog.SavetoDB(151);
                            InsertLog.SavetoDB(106, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_upper + ", Wafer汽缸_抬起檢 Time out");
                            Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_CylinderUpError, true, out err);
                            Flag.AlarmFlag = true;
                            break;
                        }
                        if (Common.EFEM.Stage1.Ready)
                        {
                            Common.EFEM.Stage1.Slot = Common.EFEM.Robot.Slot_Arm_upper;
                            InsertLog.SavetoDB(66, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_upper);
                            rtn = Common.EFEM.Robot.WaferPut(Robot.ArmID.UpperArm, Robot.Pn.Stage1, Common.EFEM.Stage1.Slot, loadPort, EFEM.slot_status.ProcessingStage1);
                            if (rtn)
                            {
                                eFEMStep_Back = EFEMStep.DoAlignement;

                                bWEFEMAutoRun.ReportProgress(10);
                                Common.EFEM.Stage1.WaferPresence = true;
                                Common.EFEM.Stage1.Measuredone = false;
                                AutoRunStage.FoupID = Common.EFEM.LoadPort_Run.FoupID;
                                AutoRunStage.Slot = Common.EFEM.Stage1.Slot;

                                //// ⭐ Wafer Process Start Event (CEID 151) — N2
                                if (fram.m_SecsgemType == 1)
                                {
                                    int slotIdx = Common.EFEM.Stage1.Slot - 1; // Stage1.Slot 是 1-indexed, 陣列是 0-indexed
                                    string substID = loadPort.SubstrateID != null && slotIdx >= 0 && slotIdx < loadPort.SubstrateID.Length ? loadPort.SubstrateID[slotIdx] : "";
                                    string lotID = loadPort.LotID != null && slotIdx >= 0 && slotIdx < loadPort.LotID.Length ? loadPort.LotID[slotIdx] : "";
                                    if (string.IsNullOrEmpty(substID)) substID = loadPort.FoupID;
                                    if (string.IsNullOrEmpty(lotID)) lotID = loadPort.FoupID;

                                    // RPID 55 Substrate DV
                                    Common.SecsgemForm.UpdateSV(341, substID, out err);                    // SubstrateID (STS_OBJID)
                                    Common.SecsgemForm.UpdateSV(342, lotID, out err);                      // LotID (STS_LOTID)
                                    Common.SecsgemForm.UpdateSV(4170, Common.EFEM.Stage1.Slot.ToString(), out err); // SlotID (STS_SLOTID)
                                    Common.SecsgemForm.UpdateSV(346, "Stage1", out err);                   // SubstrateLocationID
                                    Common.SecsgemForm.UpdateSV(347, (byte)1, out err);                    // SubstProcState = IN_PROCESS

                                    // Port / Carrier / Recipe
                                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)(loadPort.pn == LoadPort.Pn.P1 ? 1 : 2), out err);
                                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);
                                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.RecipeID, sram.PJInfo.recID, out err);

                                    // Active DV
                                    Common.SecsgemForm.UpdateSV(4180, sram.RunningPJ, out err);            // ActivePRJobID
                                    Common.SecsgemForm.UpdateSV(4181, (byte)(loadPort.pn == LoadPort.Pn.P1 ? 1 : 2), out err); // ActivePRJobPortID
                                    Common.SecsgemForm.UpdateSV(4182, loadPort.FoupID, out err);           // ActivePRJobCarrierID
                                    Common.SecsgemForm.UpdateSV(4183, sram.PJInfo.recID, out err);         // ActiveRecipeID

                                    if (!string.IsNullOrEmpty(substID))
                                        Common.SecsgemForm.SetSubstrateStatus_Proc(substID, SubstProcState.IN_PROCESS);
                                    else
                                        Common.SecsgemForm.EventReportSend(151, out err);
                                    Gem300Monitor.AddSend("WaferProcessStart (CEID151) LP=" + loadPort.pn + " Slot=" + Common.EFEM.Stage1.Slot);
                                }

                                SpinWait.SpinUntil(() => false, 500); // 取放片完 delay 1秒 才更新 Stage1.WaferPresence 狀態
                                Common.io.WriteOut(IOName.Out.StageWafer在席, true);
                                Common.EFEM.Aligner.GetStatus();
                                if (Common.EFEM.Aligner.WaferPresence) // 放下wafer後 檢查Aligner有沒有片
                                {
                                    if (!Common.EFEM.Aligner.Alignement_Done)  // 如果有片，先看Alignement_done， 應該都是還沒做，先做alignement
                                    {
                                        eFEMStep = EFEMStep.DoAlignement;
                                        eFEMStep_Back = EFEMStep.DoAlignement;
                                    }
                                    else
                                    {
                                        eFEMStep = EFEMStep.JudgeStep;
                                        eFEMStep_Back = EFEMStep.JudgeStep;
                                    }
                                }
                                else // 放下wafer後 檢查Aligner有沒有片，沒有片就去判斷步驟
                                {
                                    eFEMStep = EFEMStep.JudgeStep;
                                    eFEMStep_Back = EFEMStep.JudgeStep;
                                }

                                ///
                                if (Common.EFEM.Aligner.Alignement_Done) // 放下wafer後
                                {
                                    eFEMStep = EFEMStep.JudgeStep;
                                    eFEMStep_Back = EFEMStep.JudgeStep;
                                }
                                else
                                {
                                    if (Common.EFEM.Aligner.WaferPresence)
                                    {
                                        eFEMStep = EFEMStep.DoAlignement;

                                        eFEMStep_Back = EFEMStep.DoAlignement;
                                    }
                                    else
                                    {
                                        eFEMStep = EFEMStep.JudgeStep;
                                        eFEMStep_Back = EFEMStep.JudgeStep;
                                    }
                                }
                                ///
                            }
                            else
                            {
                                InsertLog.SavetoDB(106, "Pn：" + loadPort.pn + ", Slot：" + Common.EFEM.Robot.Slot_Arm_upper); //
                                Common.EFEM.Stage1.Slot = 0;
                                EFEMGotoErrorCheckStep();
                            }
                        }
                    }
                    bWEFEMAutoRun.ReportProgress(99);
                    break;

                case EFEMStep.Finish:
                    loadPort.Busy = false;
                    if (!_carrierAccessingAlreadySet)
                        Common.SecsgemForm.SetCarrierStatus_Accessing(Common.EFEM.LoadPort_Run.FoupID, CarrierAccessingState.CARRIER_COMPLETE);
                    _carrierAccessingAlreadySet = false;
                    if (loadPort.pn == LoadPort.Pn.P1)
                    {
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                    }
                    else if (loadPort.pn == LoadPort.Pn.P2)
                    {
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                    }
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);

                    //Check Limit
                    if(sram.Recipe.LimitMethod == 1 && !fram.EFEMSts.bLimitCheckPass)
                    {
                        if (fram.m_SecsgemType == 0)
                            Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_DataAnalysisError, true, out err);
                    }
                    else
                    {
                        if (fram.m_SecsgemType == 0)
                            Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MeasureResultSend, out err);  // 先更新 PortID & CarrierID在報

                        // ⭐ CEID 152 (WaferProcessEnd) 已移至 WaferGetFormStage1，每片取走時即送出
                        // 這裡不再重複發送，避免到 Finish 時 sram.RunningPJ 已清空導致 ActivePRJobID 為空
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
                    fram.EFEMSts.bLimitCheckPass = true;

                    // ⭐ GEM300 模式下：CARRIER_COMPLETE 後直接主動 Unload（發 Auto Door Closed Event）
                    //    不等 Host 的 S3F17 Release 指令，符合 ToolSoftwareTester Processing Scenario 期望
                    if (fram.m_SecsgemType == 1)
                    {
                        Gem300Monitor.AddSend("Auto FoupUnLoad (AutoDoorClosed) LP=" + loadPort.pn);
                        FoupUnLoad(ref loadPort);
                    }

                    bWEFEMAutoRun.ReportProgress(99);
                    break;

                default:
                    break;
            }
        }

        public static void SaveEFEMSts()
        {
            if (!EFEM.IsInit)
            {
                return;
            }
            if (Common.EFEM.LoadPort1.Busy && Common.EFEM.LoadPort_Run != null)
            {
                fram.EFEMSts.LoadPortRun = Common.EFEM.LoadPort_Run.pn.ToString();
                fram.EFEMSts.StepBack1 = AutoRunEFEMStepBack1.ToString();
                fram.EFEMSts.LoadPort1_FoupID = Common.EFEM.LoadPort1.FoupID;

                if (Common.EFEM.Robot.Slot_pn != null)
                {
                    fram.EFEMSts.Robot_Upper_Slotpn = Common.EFEM.Robot.Slot_pn.pn.ToString();
                    fram.EFEMSts.Robot_Lower_Slotpn = Common.EFEM.Robot.Slot_pn.pn.ToString();
                    fram.EFEMSts.Robot_Upper_Slot = Common.EFEM.Robot.Slot_Arm_upper;
                    fram.EFEMSts.Robot_Lower_Slot = Common.EFEM.Robot.Slot_Arm_lower;
                }
                fram.EFEMSts.Aligner_Slotpn = Common.EFEM.LoadPort1.pn.ToString();
                fram.EFEMSts.Stage_Slotpn = Common.EFEM.LoadPort1.pn.ToString();
                for (int i = 0; i < Common.EFEM.LoadPort1.Slot.Length; i++)
                {
                    fram.EFEMSts.Slot_Sts1[i] = Common.EFEM.LoadPort1.slot_Status[i].ToString();
                }
                fram.EFEMSts.Aligner_Slot = Common.EFEM.Aligner.Slot;
                fram.EFEMSts.Stage_Slot = Common.EFEM.Stage1.Slot;
            }
            else if (Common.EFEM.LoadPort2.Busy && Common.EFEM.LoadPort_Run != null)
            {
                fram.EFEMSts.LoadPortRun = Common.EFEM.LoadPort_Run.pn.ToString();
                fram.EFEMSts.StepBack2 = AutoRunEFEMStepBack2.ToString();
                fram.EFEMSts.LoadPort2_FoupID = Common.EFEM.LoadPort2.FoupID;
                if (Common.EFEM.Robot.Slot_pn != null)
                {
                    fram.EFEMSts.Robot_Upper_Slotpn = Common.EFEM.Robot.Slot_pn.pn.ToString();
                    fram.EFEMSts.Robot_Lower_Slotpn = Common.EFEM.Robot.Slot_pn.pn.ToString();
                    fram.EFEMSts.Robot_Upper_Slot = Common.EFEM.Robot.Slot_Arm_upper;
                    fram.EFEMSts.Robot_Lower_Slot = Common.EFEM.Robot.Slot_Arm_lower;
                }
                fram.EFEMSts.Aligner_Slotpn = Common.EFEM.LoadPort2.pn.ToString();
                fram.EFEMSts.Stage_Slotpn = Common.EFEM.LoadPort2.pn.ToString();
                for (int i = 0; i < Common.EFEM.LoadPort2.Slot.Length; i++)
                {
                    fram.EFEMSts.Slot_Sts2[i] = Common.EFEM.LoadPort2.slot_Status[i].ToString();
                }
                fram.EFEMSts.Aligner_Slot = Common.EFEM.Aligner.Slot;
                fram.EFEMSts.Stage_Slot = Common.EFEM.Stage1.Slot;
            }
            else
            {
                fram.EFEMSts.LoadPortRun = "";
                fram.EFEMSts.StepBack1 = AutoRunEFEMStepBack1.ToString();
                fram.EFEMSts.StepBack2 = AutoRunEFEMStepBack1.ToString();
                fram.EFEMSts.LoadPort1_FoupID = "";
                fram.EFEMSts.LoadPort2_FoupID = "";
                fram.EFEMSts.Robot_Upper_Slotpn = "";
                fram.EFEMSts.Robot_Lower_Slotpn = "";
                fram.EFEMSts.Aligner_Slotpn = "";
                fram.EFEMSts.Stage_Slotpn = "";
                for (int i = 0; i < fram.EFEMSts.Slot_Sts1.Length; i++)
                {
                    fram.EFEMSts.Slot_Sts1[i] = EFEM.slot_status.Empty.ToString();
                    fram.EFEMSts.Slot_Sts2[i] = EFEM.slot_status.Empty.ToString();
                }
                fram.EFEMSts.Robot_Upper_Slot = 0;
                fram.EFEMSts.Robot_Lower_Slot = 0;
                fram.EFEMSts.Aligner_Slot = 0;
                fram.EFEMSts.Stage_Slot = 0;
            }
        }

        public static void Update_EFEM_Sts()
        {
            Common.EFEM.LoadPort1.Update_Sts();
            Common.EFEM.LoadPort2.Update_Sts();
            Common.EFEM.Aligner.Update_Sts();
            Common.EFEM.Stage1.Update_Sts();
            Common.EFEM.Robot.Update_Sts();
        }

        public static bool EFEMHomeAll()
        {
            bool rtn;
            Flag.AbortFlag = false;  // 20231019
            Flag.CJStopFlag = false;
            Flag.CJAbortFlag = false;
            Flag.PJStopFlag = false;
            Flag.PJAbortFlag = false;
            Flag.PauseFlag = false;
            #region PT PLC
            if (fram.m_Hardware_PT == 1 && machineType == MachineType.AP6)
            {
                if (Common.PTForm.GetDriverAlarm())
                {
                    Common.PTForm.ResetDriverAlarm();
                    SpinWait.SpinUntil(() => false, 50);
                }

                if(!Common.PTForm.FindHomeFinish())
                {
                    //PLC歸零前要先降汽缸才不會干涉
                    if (Common.io.In(IOName.In.Wafer汽缸_抬起檢) || !Common.io.In(IOName.In.Wafer汽缸_降下檢))
                    {
                        Common.io.WriteOut(IOName.Out.Wafer汽缸_降下, true);
                        Common.io.WriteOut(IOName.Out.Wafer汽缸_抬起, false);
                    }
                    SpinWait.SpinUntil(() => (!Common.io.In(IOName.In.Wafer汽缸_抬起檢) && Common.io.In(IOName.In.Wafer汽缸_降下檢)), 10000);
                    if (Common.io.In(IOName.In.Wafer汽缸_抬起檢) || !Common.io.In(IOName.In.Wafer汽缸_降下檢))
                    {
                        Flag.AllHome_busyFlag = false;
                        Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_PT_PLC_ReturnHomeBlockError, true, out err);
                        HomeAllFailStr = "PT PLC 無法回原點，Wafer汽缸未降下";
                        MessageBox.Show(HomeAllFailStr);
                        return false;
                    }
                    Common.PTForm.FindHome();
                    SpinWait.SpinUntil(() => Common.PTForm.FindHomeFinish(), 10000);
                    if(!Common.PTForm.FindHomeFinish())
                    {
                        Flag.AllHome_busyFlag = false;
                        Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_PT_PLC_ReturnHomeError, true, out err);
                        HomeAllFailStr = "PT PLC 回原點失敗";
                        MessageBox.Show(HomeAllFailStr);
                        return false;
                    }
                }
                Common.PTForm.PointMove(9);
                SpinWait.SpinUntil(() => Common.PTForm.GetPointMoveFinish(9), 5000);
                if (!Common.PTForm.GetPointMoveFinish(9))
                {
                    Flag.AllHome_busyFlag = false;
                    Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_PT_PLC_MoveToSafePointError, true, out err);
                    HomeAllFailStr = "PT PLC 無法退回安全位置";
                    MessageBox.Show(HomeAllFailStr);
                    return false;
                }
            }
            #endregion PT PLC

            #region AP6II Home
            if (machineType == MachineType.AP6II)
            {
                if (!(Math.Abs(Common.motion.Get_FBPos(Mo.AxisNo.AP6II_X)) < 0.1 && Math.Abs(Common.motion.Get_FBPos(Mo.AxisNo.AP6II_Z2)) < 0.1))
                {
                    //PLC歸零前要先降汽缸才不會干涉
                    if (Common.io.In(IOName.In.Wafer汽缸_抬起檢) || !Common.io.In(IOName.In.Wafer汽缸_降下檢))
                    {
                        Common.io.WriteOut(IOName.Out.Wafer汽缸_降下, true);
                        Common.io.WriteOut(IOName.Out.Wafer汽缸_抬起, false);
                    }
                    SpinWait.SpinUntil(() => (!Common.io.In(IOName.In.Wafer汽缸_抬起檢) && Common.io.In(IOName.In.Wafer汽缸_降下檢)), 10000);
                    if (Common.io.In(IOName.In.Wafer汽缸_抬起檢) || !Common.io.In(IOName.In.Wafer汽缸_降下檢))
                    {
                        Flag.AllHome_busyFlag = false;
                        Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_PT_PLC_ReturnHomeBlockError, true, out err);
                        HomeAllFailStr = "無法回原點，Wafer汽缸未降下";
                        MessageBox.Show(HomeAllFailStr);
                        return false;
                    }
                    Common.motion.PosMove(Mo.AxisNo.AP6II_X, 0);
                    Common.motion.PosMove(Mo.AxisNo.AP6II_Z2, 0);
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_X), 10000);
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_Z2), 10000);
                }
                SpinWait.SpinUntil(() => ((Math.Abs(Common.motion.Get_FBPos(Mo.AxisNo.AP6II_X)) < 0.1 && Math.Abs(Common.motion.Get_FBPos(Mo.AxisNo.AP6II_Z2)) < 0.1)), 5000);
                if (!(Math.Abs(Common.motion.Get_FBPos(Mo.AxisNo.AP6II_X)) < 0.1 && Math.Abs(Common.motion.Get_FBPos(Mo.AxisNo.AP6II_Z2)) < 0.1))
                {
                    Flag.AllHome_busyFlag = false;
                    Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_PT_PLC_MoveToSafePointError, true, out err);
                    HomeAllFailStr = "PT PLC 無法退回安全位置";
                    MessageBox.Show(HomeAllFailStr);
                    return false;
                }
            }
            #endregion AP6II Home

            #region Robot Home

            Common.EFEM.Robot.ResetError();
            Common.EFEM.Robot.GetStatus();
            // 如果有wafer 先確保真空 On
            if (Common.EFEM.Robot.WaferPresence_Lower)
            {
                if (!Common.EFEM.Robot.VacuumOn(Robot.ArmID.LowerArm))
                {
                    Flag.AllHome_busyFlag = false;
                    HomeAllFailStr = "Lower Arm Vacuum On Fail";
                    return false;
                }
            }
            if (Common.EFEM.Robot.WaferPresence_Upper)
            {
                if (!Common.EFEM.Robot.VacuumOn(Robot.ArmID.UpperArm))
                {
                    Flag.AllHome_busyFlag = false;
                    HomeAllFailStr = "Upper Arm Vacuum On Fail";
                    return false;
                }
            }

            rtn = Common.EFEM.Robot.Home();
            if (!rtn)
            {
                Flag.AllHome_busyFlag = false;
                HomeAllFailStr = "Robot Home Fail";
                return false;
            }

            #endregion Robot Home

            #region Aligner Home

            Common.EFEM.Aligner.ResetError();
            Common.EFEM.Aligner.GetStatus();
            rtn = Common.EFEM.Aligner.Home();
            if (Common.EFEM.Aligner.WaferPresence)
            {
                rtn = Common.EFEM.Aligner.Alignment();
                if (!rtn)
                {
                    Common.EFEM.Aligner.ResetError();
                    Common.EFEM.Aligner.GetStatus();
                    Common.EFEM.Aligner.Home();													 
                    //Flag.AllHome_busyFlag = false;
                    //HomeAllFailStr = Common.EFEM.Aligner.ErrorDescription;
                    //return false;
                }   //遇到破片的話，這樣退出會變成破片放不回去 20231016
            }

            #endregion Aligner Home

            #region WaferPut to LoadPort

            if (Common.EFEM.LoadPort_Run != null)
            {
                switch (Common.EFEM.LoadPort_Run.pn)
                {// 先放手上的
                    case LoadPort.Pn.P1:

                        if (Common.EFEM.Robot.WaferPresence_Lower)
                        {
                            rtn = Common.EFEM.Robot.WaferPut(Robot.ArmID.LowerArm, Robot.Pn.P1, Common.EFEM.Robot.Slot_Arm_lower, Common.EFEM.LoadPort_Run, EFEM.slot_status.Ready);
                            if (rtn)
                            {
                                //Common.EFEM.Robot.Slot_Arm_lower = 0;
                                //SaveEFEMSts();
                                //ParamFile.saveparam("EFEMSts");
                            }
                            else
                            {
                                Flag.AllHome_busyFlag = false;

                                HomeAllFailStr = "Lower Arm Wafer Home P1 Fail";
                                return false;
                            }
                        }
                        if (Common.EFEM.Robot.WaferPresence_Upper)
                        {
                            rtn = Common.EFEM.Robot.WaferPut(Robot.ArmID.UpperArm, Robot.Pn.P1, Common.EFEM.Robot.Slot_Arm_upper, Common.EFEM.LoadPort_Run, EFEM.slot_status.Ready);
                            if (rtn)
                            {
                                //Common.EFEM.Robot.Slot_Arm_upper = 0;
                                //SaveEFEMSts();
                                //ParamFile.saveparam("EFEMSts");
                            }
                            else
                            {
                                Flag.AllHome_busyFlag = false;

                                HomeAllFailStr = "Upper Arm Wafer Home P1 Fail";
                                return false;
                            }
                        }
                        break;

                    case LoadPort.Pn.P2:
                        if (Common.EFEM.Robot.WaferPresence_Lower)
                        {
                            rtn = Common.EFEM.Robot.WaferPut(Robot.ArmID.LowerArm, Robot.Pn.P2, Common.EFEM.Robot.Slot_Arm_lower, Common.EFEM.LoadPort_Run, EFEM.slot_status.Ready);
                            if (rtn)
                            {
                                //Common.EFEM.Robot.Slot_Arm_lower = 0;
                                //SaveEFEMSts();
                                //ParamFile.saveparam("EFEMSts");
                            }
                            else
                            {
                                Flag.AllHome_busyFlag = false;

                                HomeAllFailStr = "Lower Arm Wafer Home P2 Fail";
                                return false;
                            }
                        }
                        if (Common.EFEM.Robot.WaferPresence_Upper)
                        {
                            rtn = Common.EFEM.Robot.WaferPut(Robot.ArmID.UpperArm, Robot.Pn.P2, Common.EFEM.Robot.Slot_Arm_upper, Common.EFEM.LoadPort_Run, EFEM.slot_status.Ready);
                            if (rtn)
                            {
                                //Common.EFEM.Robot.Slot_Arm_upper = 0;
                                //SaveEFEMSts();
                                //ParamFile.saveparam("EFEMSts");
                            }
                            else
                            {
                                Flag.AllHome_busyFlag = false;

                                HomeAllFailStr = "Upper Arm Wafer Home P2 Fail";
                                return false;
                            }
                        }
                        break;
                }
            }

            // 再拿起 Aligner -> Lower / Stage -> Upper
            if (Common.EFEM.Aligner.WaferPresence)
            {
                rtn = Common.EFEM.Aligner.AlignerVacuum(Aligner.OnOff.Off);
                rtn = Common.EFEM.Aligner.GetStatus();
                rtn = Common.EFEM.Robot.WaferGet(Robot.ArmID.LowerArm, Robot.Pn.Aligner1, Common.EFEM.Aligner.Slot, Common.EFEM.LoadPort_Run, EFEM.slot_status.Ready);

                if (rtn)
                {
                    //Common.EFEM.Robot.Slot_Arm_lower = Common.EFEM.Aligner.Slot;
                    //Common.EFEM.Aligner.Slot = 0;
                    Common.EFEM.Robot.Update_slot_Status(Robot.ArmID.LowerArm, EFEM.slot_status.Ready);
                    //SaveEFEMSts();
                    //ParamFile.saveparam("EFEMSts");
                }
                else
                {
                    Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EFEM_Robot_WaferGetError_LowerArm_Aligner1_Slot1 + Common.EFEM.Aligner.Slot - 1, true, out err);
                    InsertLog.SavetoDB(102, Common.EFEM.Robot.ErrorDescription);
                    Flag.AllHome_busyFlag = false;
                    HomeAllFailStr = "Aligner Wafer Get Fail";
                    return false;
                }
            }
            if (machineType == MachineType.AP6 || machineType == MachineType.AP6II)
            {
                if (Common.io.In(IOName.In.StageWafer在席))
                {
                    // 解真空 & 汽缸up
                    if (Common.io.In(IOName.In.真空平台_負壓檢))
                    {
                        Common.io.WriteOut(IOName.Out.平台真空電磁閥, false);
                        Common.io.WriteOut(IOName.Out.平台破真空電磁閥, true);
                    }
                    if (!Common.io.In(IOName.In.Wafer汽缸_抬起檢)&&(fram.m_Hardware_PT == 1 && Common.PTForm.GetPointMoveFinish(9)))
                    {
                        Common.io.WriteOut(IOName.Out.Wafer汽缸_降下, false);
                        Common.io.WriteOut(IOName.Out.Wafer汽缸_抬起, true);
                    }
                    else if (machineType == MachineType.AP6II)
                    {
                        if (!Common.io.In(IOName.In.Wafer汽缸_抬起檢) && Math.Abs((Common.motion.Get_FBPos(Mo.AxisNo.AP6II_Z2))) < 0.1 && Math.Abs((Common.motion.Get_FBPos(Mo.AxisNo.AP6II_X))) < 0.1)
                        {
                            Common.io.WriteOut(IOName.Out.Wafer汽缸_降下, false);
                            Common.io.WriteOut(IOName.Out.Wafer汽缸_抬起, true);
                        }
                    }
                    SpinWait.SpinUntil(() => (!Common.io.In(IOName.In.真空平台_負壓檢) && Common.io.In(IOName.In.Wafer汽缸_抬起檢)), 10000);
                    if(!Common.io.In(IOName.In.Wafer汽缸_抬起檢))
                    {
                        Flag.AllHome_busyFlag = false;

                        HomeAllFailStr = "Stage Wafer Home Fail, Cylinder Lift Fail";
                        return false;
                    }
                    else if(Common.io.In(IOName.In.真空平台_負壓檢))
                    {
                        Flag.AllHome_busyFlag = false;

                        HomeAllFailStr = "Stage Wafer Home Fail, Stage Vacuum Is On";
                        return false;
                    }
                    else if ((fram.m_Hardware_PT == 1 && !Common.PTForm.GetPointMoveFinish(9)))
                    {
                        Flag.AllHome_busyFlag = false;

                        HomeAllFailStr = "Stage Wafer Home Fail, PT PLC Is at Unsafe Position";
                        return false;
                    }
                    Common.EFEM.Stage1.Ready = true;
                    Common.io.WriteOut(IOName.Out.StageWaferReady, true);
                    Common.io.WriteOut(IOName.Out.StageWafer在席, true);
                    rtn = Common.EFEM.Robot.WaferGet(Robot.ArmID.UpperArm, Robot.Pn.Stage1, Common.EFEM.Stage1.Slot, Common.EFEM.LoadPort_Run, EFEM.slot_status.Ready);
                    if (rtn)
                    {
                        Common.EFEM.Robot.Update_slot_Status(Robot.ArmID.UpperArm, EFEM.slot_status.Ready);
                        //SaveEFEMSts();
                        //ParamFile.saveparam("EFEMSts");
                        SpinWait.SpinUntil(() => false, 1000);
                        Common.io.WriteOut(IOName.Out.StageWafer在席, false);
                        rtn = Common.EFEM.Aligner.Home();
                        if (!rtn)
                        {
                            Flag.AllHome_busyFlag = false;

                            HomeAllFailStr = "Aligner home Fail";
                            return false;
                        }
                        Common.EFEM.Aligner.GetStatus();
                        if (Common.EFEM.Robot.WaferPut(Robot.ArmID.UpperArm, Robot.Pn.Aligner1, Common.EFEM.Robot.Slot_Arm_upper, Common.EFEM.LoadPort_Run, EFEM.slot_status.Ready))
                        {
                            Common.EFEM.Aligner.GetStatus();
                            //SaveEFEMSts();
                            //ParamFile.saveparam("EFEMSts");
                            if (Common.EFEM.Aligner.WaferPresence)
                            {
                                if (Common.EFEM.Aligner.Alignment())
                                {
                                    rtn = Common.EFEM.Aligner.AlignerVacuum(Aligner.OnOff.Off);
                                    if (Common.EFEM.Robot.WaferGet(Robot.ArmID.UpperArm, Robot.Pn.Aligner1, Common.EFEM.Aligner.Slot, Common.EFEM.LoadPort_Run, EFEM.slot_status.Ready))
                                    {
                                        Common.EFEM.Robot.GetStatus();
                                        //SaveEFEMSts();
                                        //ParamFile.saveparam("EFEMSts");
                                    }
                                    else
                                    {
                                        Flag.AllHome_busyFlag = false;

                                        HomeAllFailStr = "Aligner Wafer Home Fail";
                                        return false;
                                    }
                                }
                                else
                                {
                                    Flag.AllHome_busyFlag = false;

                                    HomeAllFailStr = "Aligment Time Out, 請手動處理Wafer";
                                    return false;
                                }
                            }
                            else
                            {
                                Flag.AllHome_busyFlag = false;

                                HomeAllFailStr = "Aligner Wafer Home Fail";
                                return false;
                            }
                        }
                        else
                        {
                            Flag.AllHome_busyFlag = false;

                            HomeAllFailStr = "Aligner Wafer Home Fail";
                            return false;
                        }
                    }
                    else
                    {
                        Flag.AllHome_busyFlag = false;

                        HomeAllFailStr = "Stage Wafer Home Fail";
                        return false;
                    }
                }
            }
            else if (machineType == MachineType.N2)
            {
                if (Common.io.In(IOName.In.StageWafer在席))
                {
                    // 先移動 在解真空
                    Common.motion.PosMove(Mo.AxisNo.DD, 0);
                    Common.motion.PosMove(Mo.AxisNo.X, AutoRunStage.Homepos.N2.WaferSwitch.X);
                    Common.motion.PosMove(Mo.AxisNo.Y, AutoRunStage.Homepos.N2.WaferSwitch.Y);

                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.DD), 15000);
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.X), 15000);
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.Y), 15000);
                    if (!Common.motion.MotionDone(Mo.AxisNo.DD) || !Common.motion.MotionDone(Mo.AxisNo.X) || !Common.motion.MotionDone(Mo.AxisNo.Y))
                    {
                        if (!Common.motion.MotionDone(Mo.AxisNo.DD))
                            HomeAllFailStr = "DD未達取片位置"; //
                        if (!Common.motion.MotionDone(Mo.AxisNo.X))
                            HomeAllFailStr = "X軸未達取片位置"; //
                        if (!Common.motion.MotionDone(Mo.AxisNo.Y))
                            HomeAllFailStr = "Y軸未達取片位置"; //

                        Flag.AllHome_busyFlag = false;
                        return false;
                    }

                    // 解真空
                    if (Common.io.In(IOName.In.真空平台_負壓檢))
                    {
                        Common.io.WriteOut(IOName.Out.平台真空電磁閥, false);
                        Common.io.WriteOut(IOName.Out.平台破真空電磁閥, true);
                    }
                    SpinWait.SpinUntil(() => !Common.io.In(IOName.In.真空平台_負壓檢), 3000);
                    if(Common.io.In(IOName.In.真空平台_負壓檢))
                    {
                        HomeAllFailStr = "真空平台_破真空異常"; //
                        Flag.AllHome_busyFlag = false;
                        return false;
                    }

                    Common.EFEM.Stage1.Ready = true;
                    Common.io.WriteOut(IOName.Out.StageWaferReady, true);
                    Common.io.WriteOut(IOName.Out.StageWafer在席, true);
                    rtn = Common.EFEM.Robot.WaferGet(Robot.ArmID.UpperArm, Robot.Pn.Stage1, Common.EFEM.Stage1.Slot, Common.EFEM.LoadPort_Run, EFEM.slot_status.Ready);
                    if (rtn)
                    {
                        Common.EFEM.Robot.Update_slot_Status(Robot.ArmID.UpperArm, EFEM.slot_status.Ready);
                        //SaveEFEMSts();
                        //ParamFile.saveparam("EFEMSts");
                        SpinWait.SpinUntil(() => false, 1000);
                        Common.io.WriteOut(IOName.Out.StageWafer在席, false);
                        rtn = Common.EFEM.Aligner.Home();
                        if (!rtn)
                        {
                            Flag.AllHome_busyFlag = false;

                            HomeAllFailStr = "Aligner home Fail";
                            return false;
                        }
                        Common.EFEM.Aligner.GetStatus();
                        if (Common.EFEM.Robot.WaferPut(Robot.ArmID.UpperArm, Robot.Pn.Aligner1, Common.EFEM.Robot.Slot_Arm_upper, Common.EFEM.LoadPort_Run, EFEM.slot_status.Ready))
                        {
                            Common.EFEM.Aligner.GetStatus();
                            //SaveEFEMSts();
                            //ParamFile.saveparam("EFEMSts");
                            if (Common.EFEM.Aligner.WaferPresence)
                            {
                                if (Common.EFEM.Aligner.Alignment())
                                {
                                    rtn = Common.EFEM.Aligner.AlignerVacuum(Aligner.OnOff.Off);
                                    if (Common.EFEM.Robot.WaferGet(Robot.ArmID.UpperArm, Robot.Pn.Aligner1, Common.EFEM.Aligner.Slot, Common.EFEM.LoadPort_Run, EFEM.slot_status.Ready))
                                    {
                                        Common.EFEM.Robot.GetStatus();
                                        //SaveEFEMSts();
                                        //ParamFile.saveparam("EFEMSts");
                                    }
                                    else
                                    {
                                        Flag.AllHome_busyFlag = false;

                                        HomeAllFailStr = "Aligner Wafer Home Fail";
                                        return false;
                                    }
                                }
                                else
                                {
                                    Flag.AllHome_busyFlag = false;

                                    HomeAllFailStr = "Aligment Time Out, 請手動處理Wafer";
                                    return false;
                                }
                            }
                            else
                            {
                                Flag.AllHome_busyFlag = false;

                                HomeAllFailStr = "Aligner Wafer Home Fail";
                                return false;
                            }
                        }
                        else
                        {
                            Flag.AllHome_busyFlag = false;

                            HomeAllFailStr = "Aligner Wafer Home Fail";
                            return false;
                        }
                    }
                    else
                    {
                        Flag.AllHome_busyFlag = false;

                        HomeAllFailStr = "Stage Wafer Home Fail";
                        return false;
                    }
                }
            }

            Common.EFEM.Robot.GetStatus();
            if (Common.EFEM.LoadPort_Run != null)
            {
                switch (Common.EFEM.LoadPort_Run.pn)
                {// 再放剛剛拿起來的
                    case LoadPort.Pn.P1:

                        if (Common.EFEM.Robot.WaferPresence_Lower)
                        {
                            rtn = Common.EFEM.Robot.WaferPut(Robot.ArmID.LowerArm, Robot.Pn.P1, Common.EFEM.Robot.Slot_Arm_lower, Common.EFEM.LoadPort_Run, EFEM.slot_status.Ready);
                            if (rtn)
                            {
                                //SaveEFEMSts();
                                //ParamFile.saveparam("EFEMSts");
                            }
                            else
                            {
                                Flag.AllHome_busyFlag = false;

                                HomeAllFailStr = "Lower Arm Wafer Home P1 Fail";
                                return false;
                            }
                        }
                        if (Common.EFEM.Robot.WaferPresence_Upper)
                        {
                            rtn = Common.EFEM.Robot.WaferPut(Robot.ArmID.UpperArm, Robot.Pn.P1, Common.EFEM.Robot.Slot_Arm_upper, Common.EFEM.LoadPort_Run, EFEM.slot_status.Ready);
                            if (rtn)
                            {
                                //SaveEFEMSts();
                                //Update_EFEM_Sts();
                                //ParamFile.saveparam("EFEMSts");
                            }
                            else
                            {
                                Flag.AllHome_busyFlag = false;

                                HomeAllFailStr = "Upper Arm Wafer Home P1 Fail";
                                return false;
                            }
                        }
                        break;

                    case LoadPort.Pn.P2:
                        if (Common.EFEM.Robot.WaferPresence_Lower)
                        {
                            rtn = Common.EFEM.Robot.WaferPut(Robot.ArmID.LowerArm, Robot.Pn.P2, Common.EFEM.Robot.Slot_Arm_lower, Common.EFEM.LoadPort_Run, EFEM.slot_status.Ready);
                            if (rtn)
                            {
                                //SaveEFEMSts();
                                //ParamFile.saveparam("EFEMSts");
                            }
                            else
                            {
                                Flag.AllHome_busyFlag = false;

                                HomeAllFailStr = "Lower Arm Wafer Home P2 Fail";
                                return false;
                            }
                        }
                        if (Common.EFEM.Robot.WaferPresence_Upper)
                        {
                            rtn = Common.EFEM.Robot.WaferPut(Robot.ArmID.UpperArm, Robot.Pn.P2, Common.EFEM.Robot.Slot_Arm_upper, Common.EFEM.LoadPort_Run, EFEM.slot_status.Ready);
                            if (rtn)
                            {
                                //SaveEFEMSts();
                                //ParamFile.saveparam("EFEMSts");
                            }
                            else
                            {
                                Flag.AllHome_busyFlag = false;

                                HomeAllFailStr = "Upper Arm Wafer Home P2 Fail";
                                return false;
                            }
                        }

                        break;
                }
            }

            #endregion WaferPut to LoadPort

            #region LoadPort Home

            Common.EFEM.LoadPort1.ResetError();
            Common.EFEM.LoadPort2.ResetError();
            Common.EFEM.LoadPort1.GetLEDStatus();
            Common.EFEM.LoadPort2.GetLEDStatus();
            if (Common.EFEM.LoadPort1.LED_Placement && Common.EFEM.LoadPort1.LED_Persence)
            {
                rtn = Common.EFEM.LoadPort1.Home();

                if (!rtn)
                {
                    Flag.AllHome_busyFlag = false;

                    HomeAllFailStr = "LP1 Home Fail";
                    return false;
                }
                else
                {
                    for (int i = 0; i < 25; i++)
                    {
                        Common.EFEM.LoadPort1.Slot[i] = 0;
                        Common.EFEM.LoadPort1.slot_Status[i] = EFEM.slot_status.Empty;
                        Common.EFEM.LoadPort1.Update_slot_Status(i + 1, EFEM.slot_status.Empty);
                        Common.EFEM.LoadPort1.SubstrateID[i] = null;
                        Common.EFEM.LoadPort1.LotID[i] = null;
                    }
                }
            }
            if (Common.EFEM.LoadPort2.LED_Placement && Common.EFEM.LoadPort2.LED_Persence)
            {
                rtn = Common.EFEM.LoadPort2.Home();

                if (!rtn)
                {
                    Flag.AllHome_busyFlag = false;

                    HomeAllFailStr = "LP2 Home Fail";
                    return false;
                }
                else
                {
                    for (int i = 0; i < 25; i++)
                    {
                        Common.EFEM.LoadPort2.Slot[i] = 0;
                        Common.EFEM.LoadPort2.slot_Status[i] = EFEM.slot_status.Empty;
                        Common.EFEM.LoadPort2.Update_slot_Status(i + 1, EFEM.slot_status.Empty);
                        Common.EFEM.LoadPort2.SubstrateID[i] = null;
                        Common.EFEM.LoadPort2.LotID[i] = null;
                    }
                }
            }

            #endregion LoadPort Home

            if (rtn)
            {
                Flag.AlarmFlag = false;
                Common.EFEM.IO.SignalTower(IO.LampState.AllOff);
                Common.EFEM.IO.SignalTower(IO.LampState.YellowOn);
                Common.EFEM.IO.Buzzer(IO.BuzzerSts.Off);

                if (!Common.io.In(IOName.In.StageWafer在席))
                {
                    Common.EFEM.Stage1.WaferPresence = false;
                    Common.io.WriteOut(IOName.Out.StageWaferReady, true);
                    Common.io.WriteOut(IOName.Out.StageWafer在席, false);
                }
                Flag.AllHomeFlag = true;
                Flag.EFEMAlarmReportFlag = false;
                Flag.EQAlarmReportFlag = false;
                InsertLog.SavetoDB(62);

                for (int i = 0; i < sram.Recipe.Rotate_Count; i++)
                {
                    AnalysisData.WaferMeasure[i] = false;
                }
                sram.PitchAngleTotal = 0;

                //ParamFile.saveparam("EFEMSts");
                Flag.AllHome_busyFlag = false;
                HomeAllFailStr = "";

                return true;
            }
            else
            {
                Flag.AllHome_busyFlag = false;

                HomeAllFailStr = "Home All Fail";
                return false;
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
    }
}