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
                    if (Common.SecsgemForm.isRemote() && !Flag.AutoidleFlag && Flag.AllHomeFlag)
                    {
                        Flag.AutoidleFlag = true;
                        Flag.Autoidle_LocalFlag = false;
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
                            if (fram.m_simulateRun == 0) //模擬模式不跑不然會卡住20230908
                            {
                                WatchMaterialRecive();
                                WatchMaterialRemove();
                            }

                            #region slotmap cmd ->  load & slotmap  // 完成

                            if (Common.SecsgemForm.bWaitSECS_SlotMapCmd && !EFEMCmd) // false 才能下指令
                            {
                                if (Common.SecsgemForm.SecsDataGet(SecsData.SlotMap, SecsDataElement.CarrierID) == "")
                                {
                                    if (Common.SecsgemForm.SecsDataGet(SecsData.SlotMap, SecsDataElement.LoadPortID) == "1")
                                    {
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
                                        FoupLoad(ref Common.EFEM.LoadPort1); // 要看Flag來切換 看是哪個foup
                                        SlotMap(ref Common.EFEM.LoadPort1);
                                        Common.EFEM.LoadPort1.Busy = false;
                                        AutoRunEFEMStep1 = EFEMStep.JudgeStep;
                                    }
                                }
                                else if (Common.SecsgemForm.SecsDataGet(SecsData.SlotMap, SecsDataElement.LoadPortID) == "2")
                                {
                                    if (Common.SecsgemForm.SecsDataGet(SecsData.SlotMap, SecsDataElement.CarrierID) == Common.EFEM.LoadPort2.FoupID && fram.SECSPara.Loadport2_AccessMode == Mode.Auto.GetHashCode())
                                    {
                                        FoupLoad(ref Common.EFEM.LoadPort2); // 要看Flag來切換 看是哪個foup
                                        SlotMap(ref Common.EFEM.LoadPort2);
                                        Common.EFEM.LoadPort2.Busy = false;
                                        AutoRunEFEMStep2 = EFEMStep.JudgeStep;
                                    }
                                }
                                Common.EFEM.LoadPort_Run = null;
                                Common.SecsgemForm.CarrierID = "";
                                Common.SecsgemForm.LoadPortID = "";
                                Common.SecsgemForm.bWaitSECS_SlotMapCmd = false;
                                Common.SecsgemForm.SecsDataClear(SecsData.SlotMap);
                            }

                            #endregion slotmap cmd ->  load & slotmap  // 完成

                            #region ReleaseCmd  unload

                            if (Common.SecsgemForm.bWaitSECS_ReleaseCmd && !EFEMCmd) // false 才能下指令         // 要看Flag來切換 看是哪個foup
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
                            }

                            #endregion ReleaseCmd  unload

                            #region Cancel

                            if (Common.SecsgemForm.bWaitSECS_CancelCmd && !EFEMCmd && !Common.EFEM.LoadPort1.Busy && !Common.EFEM.LoadPort2.Busy)
                            {
                                if (Common.SecsgemForm.SecsDataGet(SecsData.Cancel, SecsDataElement.CarrierID) == "")
                                {
                                    if (Common.SecsgemForm.SecsDataGet(SecsData.Cancel, SecsDataElement.LoadPortID) == "1" && fram.SECSPara.Loadport1_AccessMode == Mode.Auto.GetHashCode())
                                    {
                                        //FoupHome(ref Common.EFEM.LoadPort1);
                                        AutoRunEFEMStep1 = EFEMStep.JudgeStep;
                                        Flag.AbortFlag = true;
                                        bWEFEMAutoRun.ReportProgress(99);
                                    }
                                    else if (Common.SecsgemForm.SecsDataGet(SecsData.Cancel, SecsDataElement.LoadPortID) == "2" && fram.SECSPara.Loadport1_AccessMode == Mode.Auto.GetHashCode())
                                    {
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

        private static void WatchMaterialRecive()
        {
            // Placement 在socket會收到
            // PortTransferState: 1:block, 2:RTL, 3:RTUL
            // 對應到TSMC的狀態    1:ini  , 2:Mir, 3:Mor
            // block的時候就不用再看了
            if (fram.SECSPara.Loadport1_PortTransferState == PortTransferState.ReadyToLoad.GetHashCode() && Common.EFEM.LoadPort1.Placement)
            {
                Common.EFEM.LoadPort1.ReadFoupID();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort1.FoupID, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MaterialReceived, out err);
                SpinWait.SpinUntil(() => false, 2000);
                ClampAndReadID(ref Common.EFEM.LoadPort1);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, "", out err);
                fram.SECSPara.Loadport1_PortTransferState = PortTransferState.TransferBlocked.GetHashCode();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err);
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
                Common.EFEM.LoadPort2.ReadFoupID();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort2.FoupID, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MaterialReceived, out err);
                SpinWait.SpinUntil(() => false, 2000);
                ClampAndReadID(ref Common.EFEM.LoadPort2);

                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, "", out err);
                fram.SECSPara.Loadport2_PortTransferState = PortTransferState.TransferBlocked.GetHashCode();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err);
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
                Common.EFEM.LoadPort1.ReadFoupID();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort1.FoupID, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MaterialRemove, out err);
                fram.SECSPara.Loadport1_PortTransferState = PortTransferState.ReadyToLoad.GetHashCode();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, (byte)PortTransferState.ReadyToLoad.GetHashCode(), out err); // RTL
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err); // ReadyToLoad
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ReadyToLoad, out err);
                Common.EFEM.LoadPort1.LEDLoad(LoadPort.LEDsts.Off);
                Common.EFEM.LoadPort1.LEDUnLoad(LoadPort.LEDsts.Off);
            }
            if (fram.SECSPara.Loadport2_PortTransferState == PortTransferState.ReadyToUnload.GetHashCode() && !Common.EFEM.LoadPort2.Placement)
            {
                Common.SecsgemForm.DeleteCarrier(Common.EFEM.LoadPort2.FoupID, out err);  //刪除Carrier Object
                // 東西拿走後 切成 RTL
                Common.EFEM.LoadPort2.ReadFoupID();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, Common.EFEM.LoadPort2.FoupID, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MaterialRemove, out err);
                fram.SECSPara.Loadport2_PortTransferState = PortTransferState.ReadyToLoad.GetHashCode();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, (byte)PortTransferState.ReadyToLoad.GetHashCode(), out err); // RTL
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err); // ReadyToLoad
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ReadyToLoad, out err);
                Common.EFEM.LoadPort2.LEDLoad(LoadPort.LEDsts.Off);
                Common.EFEM.LoadPort2.LEDUnLoad(LoadPort.LEDsts.Off);
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
                }
            }
            else if (fram.SECSPara.Loadport2_AccessMode == Mode.Auto.GetHashCode())
            {
                Common.EFEM.LoadPort2.GetLEDStatus();
                if (Common.EFEM.LoadPort2.LED_Placement && Common.EFEM.LoadPort2.LED_Persence)
                {
                    Common.EFEM.LoadPort2.Placement = true;
                }
            }
        }

        private static void ClampAndReadID(ref LoadPort loadPort)
        {
            bool rtn;
            rtn = loadPort.Clamp();
            if (rtn)
            {
                if (loadPort.pn == LoadPort.Pn.P1)
                {
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);
                }
                else if (loadPort.pn == LoadPort.Pn.P2)
                {
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);
                }
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierClamped, out err);
                rtn = loadPort.ReadFoupID();
                if (rtn)
                {
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierIDRead, out err);
                    Common.SecsgemForm.CreateCarrier(loadPort.FoupID);
                    if (loadPort.pn == LoadPort.Pn.P1)
                        Common.SecsgemForm.SetCarrierAttr_Location(loadPort.FoupID, "LoadPort1");
                    else if (loadPort.pn == LoadPort.Pn.P2)
                        Common.SecsgemForm.SetCarrierAttr_Location(loadPort.FoupID, "LoadPort2");
                }
                else
                {
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierIDReadFail, out err);
                }
            }
        }

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
                }
                else if (loadPort.pn == LoadPort.Pn.P2)
                {
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
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

        private static void FoupUnLoad(ref LoadPort loadPort)
        {
            bool rtn;
            loadPort.LEDLoad(LoadPort.LEDsts.Off);
            loadPort.LEDUnLoad(LoadPort.LEDsts.Flash);
            rtn = loadPort.Unload();
            if (rtn)
            {
                loadPort.Busy = false;
                if (loadPort.pn == LoadPort.Pn.P1)
                {
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                    fram.SECSPara.Loadport1_PortTransferState = PortTransferState.ReadyToUnload.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err); // ReadyToUnload
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_RecipeID, "", out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err); // ReadyToUnload
                }
                else if (loadPort.pn == LoadPort.Pn.P2)
                {
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                    fram.SECSPara.Loadport2_PortTransferState = PortTransferState.ReadyToUnload.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err); // ReadyToUnload
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_RecipeID, "", out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err); // ReadyToUnload
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
                        fram.SECSPara.Loadport1_PortTransferState = PortTransferState.ReadyToUnload.GetHashCode();
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err); // ReadyToUnload
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_RecipeID, "", out err);
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err); // ReadyToUnload
                    }
                    else if (loadPort.pn == LoadPort.Pn.P2)
                    {
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                        fram.SECSPara.Loadport2_PortTransferState = PortTransferState.ReadyToUnload.GetHashCode();
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err); // ReadyToUnload
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_RecipeID, "", out err);
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err); // ReadyToUnload
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
                }
                else if (loadPort.pn == LoadPort.Pn.P2)
                {
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.LP2_CarrierMapped, out err);
                }
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);
                //Common.SecsgemForm.EventReportSend(TrimGap_EqpID.SlotMap, out err);

                if(fram.m_SecsgemType == 1)
                {
                    Common.SecsgemForm.SetCarrierAttr_SlotMap(loadPort.FoupID, loadPort.Slot);
                    Common.SecsgemForm.SetCarrierStatus_SlotMap(loadPort.FoupID, SlotMapState.WAITING_FOR_HOST);
                }
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
                    }
                    else if (loadPort.pn == LoadPort.Pn.P2)
                    {
                        Common.SecsgemForm.EventReportSend(TrimGap_EqpID.LP2_CarrierMapped, out err);
                    }
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);
                    //Common.SecsgemForm.EventReportSend(TrimGap_EqpID.SlotMap, out err);

                    Common.SecsgemForm.SetCarrierAttr_SlotMap(loadPort.FoupID, loadPort.Slot);
                    Common.SecsgemForm.SetCarrierStatus_SlotMap(loadPort.FoupID, SlotMapState.WAITING_FOR_HOST);
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

                    rtn = Common.EFEM.Robot.GetStatus();
                    rtn = Common.EFEM.Aligner.GetStatus();
                    if (!rtn)
                    {
                        Console.WriteLine(Common.EFEM.Aligner.ErrorDescription);
                        //eFEMStep = EFEMStep.ErrorCheckSts;
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
                                    if (loadPort.ReadryToLoadWafer)
                                    {
                                        eFEMStep = EFEMStep.WaferGetFormLoadPort;
                                    }
                                    else
                                    {
                                        eFEMStep = EFEMStep.Finish;
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
                                }
                                if(machineType == MachineType.AP6)
                                {
                                    if (!Common.io.In(IOName.In.Wafer汽缸_抬起檢) && (fram.m_Hardware_PT == 0 || Common.PTForm.GetPointMoveFinish(9)))
                                    {
                                        Common.io.WriteOut(IOName.Out.Wafer汽缸_降下, false);
                                        Common.io.WriteOut(IOName.Out.Wafer汽缸_抬起, true);
                                    }
                                }
                                else if(machineType == MachineType.AP6II)
                                {
                                    if (!Common.io.In(IOName.In.Wafer汽缸_抬起檢) && Math.Abs((Common.motion.Get_FBPos(Mo.AxisNo.AP6II_Z2)))<0.1 && Math.Abs((Common.motion.Get_FBPos(Mo.AxisNo.AP6II_X))) < 0.1)
                                    {
                                        Common.io.WriteOut(IOName.Out.Wafer汽缸_降下, false);
                                        Common.io.WriteOut(IOName.Out.Wafer汽缸_抬起, true);
                                    }
                                }
                                SpinWait.SpinUntil(() => (!Common.io.In(IOName.In.真空平台_負壓檢) && Common.io.In(IOName.In.Wafer汽缸_抬起檢)), 5000);
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
                                    if(!Common.motion.MotionDone(Mo.AxisNo.DD))
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
                            rtn = Common.EFEM.Robot.WaferGet(Robot.ArmID.UpperArm, Robot.Pn.Stage1, Common.EFEM.Stage1.Slot, loadPort, EFEM.slot_status.ProcessEnd);
                            if (rtn)
                            {
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
                                SpinWait.SpinUntil(() => Common.io.In(IOName.In.Wafer汽缸_抬起檢), 5000);
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
                                SpinWait.SpinUntil(() => Common.io.In(IOName.In.Wafer汽缸_抬起檢), 5000);
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
                            SpinWait.SpinUntil(() => Common.io.In(IOName.In.Wafer汽缸_抬起檢), 5000);
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
                    if (loadPort.pn == LoadPort.Pn.P1)
                    {
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)1, out err);
                    }
                    else if (loadPort.pn == LoadPort.Pn.P2)
                    {
                        Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, (byte)2, out err);
                    }
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, loadPort.FoupID, out err);
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MeasureResultSend, out err);  // 先更新 PortID & CarrierID在報
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
                    SpinWait.SpinUntil(() => (!Common.io.In(IOName.In.Wafer汽缸_抬起檢) && Common.io.In(IOName.In.Wafer汽缸_降下檢)), 5000);
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
                    SpinWait.SpinUntil(() => (!Common.io.In(IOName.In.Wafer汽缸_抬起檢) && Common.io.In(IOName.In.Wafer汽缸_降下檢)), 5000);
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
                /*if (!rtn)
                {
                    Flag.AllHome_busyFlag = false;
                    HomeAllFailStr = Common.EFEM.Aligner.ErrorDescription;
                    return false;
                }*/   //遇到破片的話，這樣退出會變成破片放不回去 20231016
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
                    SpinWait.SpinUntil(() => (!Common.io.In(IOName.In.真空平台_負壓檢) && Common.io.In(IOName.In.Wafer汽缸_抬起檢)), 5000);
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
    }
}