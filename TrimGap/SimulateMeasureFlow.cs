using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using DemoFormDiaGemLib;

namespace TrimGap
{
    /// <summary>
    /// 模擬量測流程 — 從 Carrier 放到 LoadPort 開始，到量測完 Carrier 拿走
    /// 用於 SECS/GEM300 遠端通訊測試，不需要實體 EFEM / Stage 硬體
    /// 
    /// 完整流程 (參考 SEMI E87 / E40 / E94):
    ///   Step 0   初始化 → PortTransferState = ReadyToLoad
    ///   Step 1   模擬 Carrier 放上 LoadPort (MaterialReceived)
    ///   Step 2   Clamp + CreateCarrier + ReadID → WAITING_FOR_HOST
    ///   Step 3   等待 Host ProceedWithCarrier #1 (IDVerification)
    ///   Step 4   FoupLoad → Dock → DoorOpen
    ///   Step 5   SlotMap → WAITING_FOR_HOST
    ///   Step 6   等待 Host ProceedWithCarrier #2 (SlotMapVerification)
    ///   Step 7   等待 Host 建立 CJ/PJ (E94/E40)
    ///   Step 8   CJ/PJ Start (EXECUTING / PROCESSING)
    ///   Step 9   逐片模擬量測 (MeasureStart → MeasureEnd per wafer)
    ///   Step 10  PJ/CJ Complete
    ///   Step 11  FoupUnLoad → DoorClose → UnDock → Unclamp → ReadyToUnload
    ///   Step 12  模擬 Carrier 拿走 (MaterialRemove) → ReadyToLoad
    /// </summary>
    internal class SimulateMeasureFlow
    {
        private static BackgroundWorker bWSimulate = new BackgroundWorker();
        private static string err = string.Empty;
        private static bool _isRunning = false;

        // 可設定參數
        private static int _loadPortNumber = 1;           // 1 或 2
        private static string _simulateCarrierID = "TEST12345";
        private static int _simulateSlotCount = 3;        // 模擬幾片 wafer (1~25)
        private static int _measureDelayMs = 2000;        // 每片量測模擬耗時(ms)
        private static int _stepDelayMs = 1000;           // 步驟間等待(ms)
        private static int _waitTimeoutMs = 120000;       // 等待 Host 操作逾時(ms)，逾時後自動繼續

        public enum SimStep : int
        {
            Init = 0,
            CarrierPlace,       // 1
            ClampAndReadID,     // 2
            WaitProceed1,       // 3
            FoupLoad,           // 4
            SlotMap,            // 5
            WaitProceed2,       // 6
            WaitCJPJ,           // 7
            CJPJStart,          // 8
            WaferMeasure,       // 9
            CJPJComplete,       // 10
            FoupUnLoad,         // 11
            CarrierRemove,      // 12
            Done                // 13
        }

        public static SimStep CurrentStep { get; private set; } = SimStep.Init;
        public static bool IsRunning { get { return _isRunning; } }

        /// <summary>
        /// 啟動模擬量測流程
        /// </summary>
        public static void Start(int loadPortNumber = 1, string carrierID = "SIM_CARRIER_001",
                                  int slotCount = 3, int measureDelayMs = 2000)
        {
            if (_isRunning)
            {
                Gem300Monitor.AddAlarm("SimulateMeasureFlow 已在執行中，請等待完成");
                return;
            }

            _loadPortNumber = (loadPortNumber == 2) ? 2 : 1;
            _simulateCarrierID = carrierID;
            _simulateSlotCount = Math.Max(1, Math.Min(25, slotCount));
            _measureDelayMs = measureDelayMs;

            bWSimulate = new BackgroundWorker();
            bWSimulate.DoWork += bWSimulate_DoWork;
            bWSimulate.WorkerReportsProgress = true;
            bWSimulate.RunWorkerAsync();
        }

        private static void bWSimulate_DoWork(object sender, DoWorkEventArgs e)
        {
            _isRunning = true;
            bool isGem300 = (fram.m_SecsgemType == 1);
            try
            {
                LoadPort lp = (_loadPortNumber == 1) ? Common.EFEM.LoadPort1 : Common.EFEM.LoadPort2;
                byte portByte = (byte)_loadPortNumber;

                Gem300Monitor.AddInfo("========== 模擬量測流程開始 ==========");
                Gem300Monitor.AddInfo(string.Format("LoadPort={0}  CarrierID={1}  SlotCount={2}  GEM300={3}",
                    _loadPortNumber, _simulateCarrierID, _simulateSlotCount, isGem300));

                // 重置相關旗標
                if (isGem300)
                {
                    Common.SecsgemForm.bWaitSECS_SlotMapCmd = false;
                    if (_loadPortNumber == 1) sram.LoadPort1_Carrier_Vertify = false;
                    else sram.LoadPort2_Carrier_Vertify = false;
                    sram.RunningCJ = "";
                    sram.RunningPJ = "";
                }

                // ============================================================
                // Step 0: 初始化 — 確保 PortTransferState = ReadyToLoad
                // ============================================================
                CurrentStep = SimStep.Init;
                Gem300Monitor.AddState("Step 0: 初始化 PortTransferState → ReadyToLoad");

                if (_loadPortNumber == 1)
                {
                    fram.SECSPara.Loadport1_PortTransferState = PortTransferState.ReadyToLoad.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState,
                        (byte)fram.SECSPara.Loadport1_PortTransferState, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState,
                        (byte)fram.SECSPara.Loadport1_PortTransferState, out err);
                }
                else
                {
                    fram.SECSPara.Loadport2_PortTransferState = PortTransferState.ReadyToLoad.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState,
                        (byte)fram.SECSPara.Loadport2_PortTransferState, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState,
                        (byte)fram.SECSPara.Loadport2_PortTransferState, out err);
                }
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, portByte, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ReadyToLoad, out err);
                Gem300Monitor.AddSend("EventReport: ReadyToLoad  LP=" + _loadPortNumber);
                SpinWait.SpinUntil(() => false, _stepDelayMs);

                // ============================================================
                // Step 1: 模擬 Carrier 放上 LoadPort → MaterialReceived
                //         (參考 WatchMaterialRecive 實際流程)
                // ============================================================
                CurrentStep = SimStep.CarrierPlace;
                Gem300Monitor.AddState("Step 1: 模擬 Carrier 放上 LoadPort (MaterialReceived)");

                lp.Placement = true;
                lp.FoupID = _simulateCarrierID;
                Common.SecsgemForm.CarrierID = _simulateCarrierID;
                Common.SecsgemForm.LoadPortID = _loadPortNumber.ToString();

                if (_loadPortNumber == 1)
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_CarrierID, _simulateCarrierID, out err);
                else
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_CarrierID, _simulateCarrierID, out err);

                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, portByte, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, _simulateCarrierID, out err);
                if (_loadPortNumber == 1)
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortID, portByte, out err);
                else
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortID, portByte, out err);

                if (fram.m_SecsgemType == 0)
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MaterialReceived, out err);
                else
                    Common.SecsgemForm.EventReportSend(2104, out err);

                // CarrierDock + CarrierDoorOpened (與實機 WatchMaterialRecive 一致)
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierDock, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierDoorOpened, out err);

                Gem300Monitor.AddSend(string.Format("EventReport: MaterialReceived + Dock + DoorOpened  LP={0}  FoupID={1}",
                    _loadPortNumber, _simulateCarrierID));
                SpinWait.SpinUntil(() => false, 2000);

                // ============================================================
                // Step 2: Clamp + ReadID + CreateCarrier → WAITING_FOR_HOST
                //         (順序完全對照實機 ClampAndReadID 方法)
                // ============================================================
                CurrentStep = SimStep.ClampAndReadID;
                Gem300Monitor.AddState("Step 2: Clamp + ReadID + CreateCarrier");

                // 1) CarrierClamped 事件 (實機: loadPort.Clamp() → EventReportSend)
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, portByte, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, _simulateCarrierID, out err);
                if (_loadPortNumber == 1)
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortID, portByte, out err);
                else
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortID, portByte, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierClamped, out err);
                Gem300Monitor.AddSend("EventReport: CarrierClamped");
                SpinWait.SpinUntil(() => false, 500);

                // 2) CarrierIDRead 事件 (實機: ReadFoupID() → EventReportSend)
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, _simulateCarrierID, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierIDRead, out err);
                Gem300Monitor.AddSend("EventReport: CarrierIDRead  ID=" + _simulateCarrierID);

                // 3) GEM300: CreateCarrier (實機: CarrierIDRead 之後才建立)
                if (isGem300)
                {
                    //string locationName = (_loadPortNumber == 1) ? "LOADPORT1" : "LOADPORT2";
                    string locationName = (_loadPortNumber == 1) ? "1" : "2";
                    Common.SecsgemForm.CreateCarrier(_simulateCarrierID, locationName);
                    Gem300Monitor.AddState("CreateCarrier: " + _simulateCarrierID + " @ " + locationName);
                }

                // 4) SetCarrierStatus_ID → WAITING_FOR_HOST (實機: CreateCarrier 之後)
                if (isGem300)
                {
                    Common.SecsgemForm.SetCarrierStatus_ID(_simulateCarrierID, CarrierIDState.WAITING_FOR_HOST);
                    Gem300Monitor.AddState("CarrierIDState → WAITING_FOR_HOST");
                }
                else
                {
                    Common.SecsgemForm.SetCarrierStatus_ID(_simulateCarrierID, CarrierIDState.ID_VERIFICATION_OK);
                    Gem300Monitor.AddState("CarrierIDState → ID_VERIFICATION_OK (Legacy)");
                }

                // 5) TransferBlocked (實機: SetCarrierStatus_ID 之後才設定)
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, portByte, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, "", out err);
                if (_loadPortNumber == 1)
                {
                    fram.SECSPara.Loadport1_PortTransferState = PortTransferState.TransferBlocked.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState,
                        (byte)fram.SECSPara.Loadport1_PortTransferState, out err);
                }
                else
                {
                    fram.SECSPara.Loadport2_PortTransferState = PortTransferState.TransferBlocked.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState,
                        (byte)fram.SECSPara.Loadport2_PortTransferState, out err);
                }
                Gem300Monitor.AddState("LP" + _loadPortNumber + " PortState → TransferBlocked");
                SpinWait.SpinUntil(() => false, _stepDelayMs);

                // ============================================================
                // Step 3: 等待 Host ProceedWithCarrier #1 (ID Verification)
                // ============================================================
                CurrentStep = SimStep.WaitProceed1;
                if (isGem300)
                {
                    Gem300Monitor.AddState("Step 3: 等待 Host ProceedWithCarrier #1 (ID Verification)...");
                    bool ok = SpinWait.SpinUntil(() => Common.SecsgemForm.bWaitSECS_SlotMapCmd, _waitTimeoutMs);
                    if (ok)
                    {
                        Common.SecsgemForm.bWaitSECS_SlotMapCmd = false;
                        Gem300Monitor.AddState("收到 ProceedWithCarrier #1 → CarrierIDState = ID_VERIFICATION_OK");
                    }
                    else
                    {
                        // Host 未回應，自動設定 ID_VERIFICATION_OK 繼續流程
                        Gem300Monitor.AddAlarm("等待 ProceedWithCarrier #1 逾時，自動繼續 (ID_VERIFICATION_OK)");
                        Common.SecsgemForm.SetCarrierStatus_ID(_simulateCarrierID, CarrierIDState.ID_VERIFICATION_OK);
                        Common.SecsgemForm.bWaitSECS_SlotMapCmd = false;
                    }
                }
                else
                {
                    Gem300Monitor.AddState("Step 3: Skip (Legacy SECS, 不需等待 ProceedWithCarrier)");
                }
                SpinWait.SpinUntil(() => false, _stepDelayMs);

                // ============================================================
                // Step 4: FoupLoad → Dock → DoorOpen
                // ============================================================
                CurrentStep = SimStep.FoupLoad;
                Gem300Monitor.AddState("Step 4: FoupLoad (Dock → DoorOpen)");

                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, portByte, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, _simulateCarrierID, out err);
                if (_loadPortNumber == 1)
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortID, portByte, out err);
                else
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortID, portByte, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierDock, out err);
                Gem300Monitor.AddSend("EventReport: CarrierDock");

                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierDoorOpened, out err);
                Gem300Monitor.AddSend("EventReport: CarrierDoorOpened");
                SpinWait.SpinUntil(() => false, _stepDelayMs);

                // ============================================================
                // Step 5: SlotMap — 模擬 wafer slot 狀態
                // ============================================================
                CurrentStep = SimStep.SlotMap;
                Gem300Monitor.AddState("Step 5: SlotMap (模擬 " + _simulateSlotCount + " 片 wafer)");

                // 發 CarrierMapStarted
                if (_loadPortNumber == 1)
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.LP1_CarrierMapStarted, out err);
                else
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.LP2_CarrierMapStarted, out err);
                Gem300Monitor.AddSend("EventReport: CarrierMapStarted");

                // 模擬 slot map 結果 + 虛擬 SubstrateID / LotID
                for (int i = 0; i < 25; i++)
                {
                    if (i < _simulateSlotCount)
                    {
                        lp.Slot[i] = 1;
                        lp.slot_Status[i] = EFEM.slot_status.Ready;
                        lp.Update_slot_Status(i + 1, EFEM.slot_status.Ready);
                        //lp.SubstrateID[i] = _simulateCarrierID + "-W" + (i + 1).ToString("D2");
                        //lp.LotID[i] = "SIM_LOT_001";
                        lp.SubstrateID[i] = (i + 1).ToString();
                        lp.LotID[i] = (i+1).ToString();
                    }
                    else
                    {
                        lp.Slot[i] = 0;
                        lp.slot_Status[i] = EFEM.slot_status.Empty;
                        lp.Update_slot_Status(i + 1, EFEM.slot_status.Empty);
                        lp.SubstrateID[i] = null;
                        lp.LotID[i] = null;
                    }
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.SlotMap_1 + i, (byte)lp.Slot[i], out err);
                }
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Reason, (byte)0, out err);

                // 發 CarrierMapped
                if (_loadPortNumber == 1)
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.LP1_CarrierMapped, out err);
                else
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.LP2_CarrierMapped, out err);
                Gem300Monitor.AddSend("EventReport: CarrierMapped");

                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, portByte, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, _simulateCarrierID, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.SlotMap, out err);
                Gem300Monitor.AddSend("EventReport: SlotMap");

                // GEM300: 設定 SlotMap + ContentMap → WAITING_FOR_HOST
                if (isGem300)
                {
                    Common.SecsgemForm.SetCarrierAttr_SlotMap(_simulateCarrierID, lp.Slot);

                    // 設定 ContentMap (虛擬 SubstrateID / LotID，需 25 筆)
                    string[] contentLotIDs = new string[25];
                    string[] contentSubIDs = new string[25];
                    for (int ci = 0; ci < 25; ci++)
                    {
                        contentLotIDs[ci] = lp.LotID[ci] ?? "";
                        contentSubIDs[ci] = lp.SubstrateID[ci] ?? "";
                    }
                    Common.SecsgemForm.SetCarrierAttr_ContentMap(_simulateCarrierID, contentLotIDs, contentSubIDs);
                    Gem300Monitor.AddState("SetCarrierAttr_ContentMap: " + _simulateSlotCount + " wafers");

                    Common.SecsgemForm.SetCarrierStatus_SlotMap(_simulateCarrierID, SlotMapState.WAITING_FOR_HOST);
                    Gem300Monitor.AddState("CarrierSlotMap → WAITING_FOR_HOST");
                }

                string slotmapStr = "";
                for (int i = 0; i < 25; i++)
                    slotmapStr += lp.Slot[i].ToString();
                InsertLog.SavetoDB(67, "SimSlotmap:" + slotmapStr);
                SpinWait.SpinUntil(() => false, _stepDelayMs);

                // ============================================================
                // Step 6: 等待 Host ProceedWithCarrier #2 (SlotMap Verification)
                // ============================================================
                CurrentStep = SimStep.WaitProceed2;
                if (isGem300)
                {
                    Gem300Monitor.AddState("Step 6: 等待 Host ProceedWithCarrier #2 (SlotMap Verification)...");
                    bool carrierVerify = false;
                    bool ok = SpinWait.SpinUntil(() =>
                    {
                        carrierVerify = (_loadPortNumber == 1) ? sram.LoadPort1_Carrier_Vertify : sram.LoadPort2_Carrier_Vertify;
                        return carrierVerify;
                    }, _waitTimeoutMs);
                    if (ok)
                    {
                        // 重置旗標
                        if (_loadPortNumber == 1) sram.LoadPort1_Carrier_Vertify = false;
                        else sram.LoadPort2_Carrier_Vertify = false;
                        Gem300Monitor.AddState("收到 ProceedWithCarrier #2 → SlotMapState = SLOT_MAP_VERIFICATION_OK");
                    }
                    else
                    {
                        // Host 未回應，自動設定 SLOT_MAP_VERIFICATION_OK 繼續流程
                        Gem300Monitor.AddAlarm("等待 ProceedWithCarrier #2 逾時，自動繼續 (SLOT_MAP_VERIFICATION_OK)");
                        Common.SecsgemForm.SetCarrierStatus_SlotMap(_simulateCarrierID, SlotMapState.SLOT_MAP_VERIFICATION_OK);
                        if (_loadPortNumber == 1) sram.LoadPort1_Carrier_Vertify = false;
                        else sram.LoadPort2_Carrier_Vertify = false;
                    }
                }
                else
                {
                    Gem300Monitor.AddState("Step 6: Skip (Legacy SECS)");
                }
                SpinWait.SpinUntil(() => false, _stepDelayMs);

                // ============================================================
                // Step 7: 等待 Host 建立 CJ/PJ
                // ============================================================
                CurrentStep = SimStep.WaitCJPJ;
                bool skipCJPJ = false;
                if (isGem300)
                {
                    Gem300Monitor.AddState("Step 7: 等待 Host 建立 CJ/PJ (E94/E40)...");
                    bool ok = SpinWait.SpinUntil(() => MainForm.CJ_list != null && MainForm.CJ_list.Count >= 1, _waitTimeoutMs);
                    if (ok)
                    {
                        Gem300Monitor.AddState("Host 已建立 CJ: " + MainForm.CJ_list[0]);
                    }
                    else
                    {
                        // Host 未建立 CJ/PJ，改用 Legacy ProcessStart 方式繼續
                        Gem300Monitor.AddAlarm("等待 CJ/PJ 建立逾時，自動繼續 (改用 Legacy ProcessStart)");
                        skipCJPJ = true;
                    }
                }
                else
                {
                    Gem300Monitor.AddState("Step 7: Skip (Legacy SECS, 不需 CJ/PJ)");
                }
                SpinWait.SpinUntil(() => false, _stepDelayMs);

                // ============================================================
                // Step 8: CJ/PJ Start
                // ============================================================
                CurrentStep = SimStep.CJPJStart;
                Gem300Monitor.AddState("Step 8: CJ/PJ Start");

                SECSListening.ClearReportdata();
                lp.AutoGetSlot();
                Common.EFEM.LoadPort_Run = lp;
                lp.Busy = true;

                if (isGem300 && !skipCJPJ)
                {
                    // 取得 CJ 資訊
                    sram.RunningCJ = MainForm.CJ_list[0];
                    Common.SecsgemForm.GetControlJobAttr(sram.RunningCJ,
                        out sram.CJInfo.carrierInputSpec, out sram.CJInfo.curPJ,
                        out sram.CJInfo.dataCollection, out sram.CJInfo.mtrloutStatus,
                        out sram.CJInfo.mtrloutSpec, out sram.CJInfo.pauseEvent,
                        out sram.CJInfo.procCtrlSpec, out sram.CJInfo.procOrder,
                        out sram.CJInfo.bStart, out sram.CJInfo.state, out err);
                    Gem300Monitor.AddState("CJ: " + sram.RunningCJ + "  ProcCtrlSpec: " + sram.CJInfo.procCtrlSpec);

                    // 解析 PJ 列表
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

                    // Carrier → IN_ACCESS
                    Common.SecsgemForm.SetCarrierStatus_Accessing(_simulateCarrierID, CarrierAccessingState.IN_ACCESS);
                    Gem300Monitor.AddState("CarrierAccessing → IN_ACCESS");

                    // 取第一個 PJ 啟動
                    if (sram.QueuePJ.Count > 0)
                    {
                        sram.RunningPJ = sram.QueuePJ[0];

                        // PJ → SETTING_UP
                        int pjOk = Common.SecsgemForm.ChangeProcessJobState(sram.RunningPJ, ProcessJobState.SETTING_UP);
                        Gem300Monitor.AddSend("ChangeProcessJobState → SETTING_UP  PJ=" + sram.RunningPJ);

                        if (pjOk == 0)
                        {
                            // 讀取 PJ 屬性取得 Recipe
                            Common.SecsgemForm.GetProcessJobAttr(sram.RunningPJ,
                                out sram.PJInfo.pauseEvent, out sram.PJInfo.PJState,
                                out sram.PJInfo.carrierID, out sram.PJInfo.slot,
                                out sram.PJInfo.PRType, out sram.PJInfo.bStart,
                                out sram.PJInfo.recMethod, out sram.PJInfo.recID,
                                out sram.PJInfo.recVarList, out err);

                            if (!string.IsNullOrEmpty(sram.PJInfo.recID))
                            {
                                Common.ChangeRecipe(sram.PJInfo.recID);
                                Gem300Monitor.AddState("ChangeRecipe: " + sram.PJInfo.recID);
                            }

                            // PJ → PROCESSING
                            Common.SecsgemForm.ChangeProcessJobState(sram.RunningPJ, ProcessJobState.PROCESSING);
                            Gem300Monitor.AddSend("ChangeProcessJobState → PROCESSING  PJ=" + sram.RunningPJ);
                        }
                    }
                }
                else
                {
                    // Legacy: ProcessStart 事件
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, _simulateCarrierID, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, portByte, out err);
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ProcessStart, out err);
                    Gem300Monitor.AddSend("EventReport: ProcessStart (Legacy)");
                }

                Flag.GreenLightFlag = true;
                SpinWait.SpinUntil(() => false, _stepDelayMs);

                // ============================================================
                // Step 9: 逐片模擬量測
                // ============================================================
                CurrentStep = SimStep.WaferMeasure;
                Gem300Monitor.AddState("Step 9: 開始逐片模擬量測");

                for (int slotIdx = 0; slotIdx < _simulateSlotCount; slotIdx++)
                {
                    int slotNo = slotIdx + 1;
                    Gem300Monitor.AddInfo(string.Format("  ── Slot {0}/{1} 量測開始 ──", slotNo, _simulateSlotCount));

                    // 模擬 WaferGetFromLoadPort
                    lp.Update_slot_Status(slotNo, EFEM.slot_status.ProcessingStage1);
                    Common.EFEM.Stage1.Slot = slotNo;
                    Gem300Monitor.AddState(string.Format("  Slot {0}: LoadPort → Robot → Aligner → Stage", slotNo));

                    // 發 MeasureStart 事件
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, portByte, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, _simulateCarrierID, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.SubstrateID, "W" + slotNo.ToString("D2"), out err);
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MeasureStart, out err);
                    Gem300Monitor.AddSend(string.Format("EventReport: MeasureStart  Slot={0}", slotNo));

                    // 模擬量測耗時
                    SpinWait.SpinUntil(() => false, _measureDelayMs);

                    // 模擬產生量測結果
                    SimulateMeasurementResult(slotIdx);

                    // 發 MeasureEnd 事件
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MeasureEnd, out err);
                    Gem300Monitor.AddSend(string.Format("EventReport: MeasureEnd  Slot={0}", slotNo));

                    // 發 MeasureResultSend 事件
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MeasureResultSend, out err);
                    Gem300Monitor.AddSend(string.Format("EventReport: MeasureResultSend  Slot={0}", slotNo));

                    // 模擬 Wafer 放回 LoadPort
                    lp.Update_slot_Status(slotNo, EFEM.slot_status.ProcessEnd);
                    Gem300Monitor.AddState(string.Format("  Slot {0}: Stage → Robot → LoadPort (ProcessEnd)", slotNo));

                    SpinWait.SpinUntil(() => false, 500);
                }

                Gem300Monitor.AddInfo("  ── 所有 Slot 量測完成 ──");
                SpinWait.SpinUntil(() => false, _stepDelayMs);

                // ============================================================
                // Step 10: PJ/CJ Complete
                // ============================================================
                CurrentStep = SimStep.CJPJComplete;
                Gem300Monitor.AddState("Step 10: PJ/CJ Complete");

                lp.Busy = false;

                if (isGem300 && !skipCJPJ)
                {
                    // PJ → PROCESS_COMPLETE
                    if (!string.IsNullOrEmpty(sram.RunningPJ))
                    {
                        Common.SecsgemForm.ChangeProcessJobState(sram.RunningPJ, ProcessJobState.PROCESS_COMPLETE);
                        Gem300Monitor.AddSend("ChangeProcessJobState → PROCESS_COMPLETE  PJ=" + sram.RunningPJ);
                        if (sram.QueuePJ != null && sram.QueuePJ.Count > 0)
                            sram.QueuePJ.RemoveAt(0);
                        sram.RunningPJ = "";
                    }

                    SpinWait.SpinUntil(() => false, 500);

                    // CJ → COMPLETED
                    if (!string.IsNullOrEmpty(sram.RunningCJ))
                    {
                        Common.SecsgemForm.ChangeControlJobState(sram.RunningCJ, ControlJobState.COMPLETED, 0);
                        Gem300Monitor.AddSend("ChangeControlJobState → COMPLETED  CJ=" + sram.RunningCJ);
                        sram.RunningCJ = "";
                    }

                    // Carrier → CARRIER_COMPLETE
                    Common.SecsgemForm.SetCarrierStatus_Accessing(_simulateCarrierID, CarrierAccessingState.CARRIER_COMPLETE);
                    Gem300Monitor.AddState("CarrierAccessing → CARRIER_COMPLETE");
                }
                SpinWait.SpinUntil(() => false, _stepDelayMs);

                // ============================================================
                // Step 11: FoupUnLoad → DoorClose → UnDock → Unclamp → ReadyToUnload
                // ============================================================
                CurrentStep = SimStep.FoupUnLoad;
                Gem300Monitor.AddState("Step 11: FoupUnLoad (DoorClose → UnDock → Unclamp)");

                // CarrierDoorClosed
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, portByte, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, _simulateCarrierID, out err);
                if (_loadPortNumber == 1)
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortID, portByte, out err);
                else
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortID, portByte, out err);
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierDoorClosed, out err);
                Gem300Monitor.AddSend("EventReport: CarrierDoorClosed");

                // CarrierUnDock
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierUnDock, out err);
                Gem300Monitor.AddSend("EventReport: CarrierUnDock");

                // CarrierUnclamped
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.CarrierUnclamped, out err);
                Gem300Monitor.AddSend("EventReport: CarrierUnclamped");

                // PortTransferState → ReadyToUnload
                if (_loadPortNumber == 1)
                {
                    fram.SECSPara.Loadport1_PortTransferState = PortTransferState.ReadyToUnload.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState,
                        (byte)fram.SECSPara.Loadport1_PortTransferState, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_RecipeID, "", out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState,
                        (byte)fram.SECSPara.Loadport1_PortTransferState, out err);
                }
                else
                {
                    fram.SECSPara.Loadport2_PortTransferState = PortTransferState.ReadyToUnload.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState,
                        (byte)fram.SECSPara.Loadport2_PortTransferState, out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_RecipeID, "", out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState,
                        (byte)fram.SECSPara.Loadport2_PortTransferState, out err);
                }

                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ReadyToUnload, out err);
                Gem300Monitor.AddSend("EventReport: ReadyToUnload");
                Gem300Monitor.AddState("LP" + _loadPortNumber + " PortState → ReadyToUnload");

                // 清除 Slot 資訊
                for (int i = 0; i < 25; i++)
                {
                    lp.Slot[i] = 0;
                    lp.slot_Status[i] = EFEM.slot_status.Empty;
                    lp.Update_slot_Status(i + 1, EFEM.slot_status.Empty);
                    lp.SubstrateID[i] = null;
                    lp.LotID[i] = null;
                }
                SpinWait.SpinUntil(() => false, _stepDelayMs);

                // ============================================================
                // Step 12: 模擬 Carrier 拿走 → MaterialRemove → ReadyToLoad
                // ============================================================
                CurrentStep = SimStep.CarrierRemove;
                Gem300Monitor.AddState("Step 12: 模擬 Carrier 拿走 (MaterialRemove)");

                // DeleteCarrier
                if (isGem300)
                {
                    Common.SecsgemForm.DeleteCarrier(_simulateCarrierID, out err);
                    Gem300Monitor.AddState("DeleteCarrier: " + _simulateCarrierID);
                }

                // MaterialRemove 事件
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortID, portByte, out err);
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.CarrierID, _simulateCarrierID, out err);
                if (fram.m_SecsgemType == 0)
                    Common.SecsgemForm.EventReportSend(TrimGap_EqpID.MaterialRemove, out err);
                else
                    Common.SecsgemForm.EventReportSend(2105, out err);
                Gem300Monitor.AddSend("EventReport: MaterialRemove");

                // PortTransferState → ReadyToLoad
                if (_loadPortNumber == 1)
                {
                    fram.SECSPara.Loadport1_PortTransferState = PortTransferState.ReadyToLoad.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState,
                        (byte)PortTransferState.ReadyToLoad.GetHashCode(), out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState,
                        (byte)fram.SECSPara.Loadport1_PortTransferState, out err);
                }
                else
                {
                    fram.SECSPara.Loadport2_PortTransferState = PortTransferState.ReadyToLoad.GetHashCode();
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState,
                        (byte)PortTransferState.ReadyToLoad.GetHashCode(), out err);
                    Common.SecsgemForm.UpdateSV(TrimGap_EqpID.PortTransferState,
                        (byte)fram.SECSPara.Loadport2_PortTransferState, out err);
                }
                Common.SecsgemForm.EventReportSend(TrimGap_EqpID.ReadyToLoad, out err);
                Gem300Monitor.AddSend("EventReport: ReadyToLoad");
                Gem300Monitor.AddState("LP" + _loadPortNumber + " PortState → ReadyToLoad");

                lp.Placement = false;
                lp.Busy = false;
                Common.SecsgemForm.CarrierID = "";
                Common.SecsgemForm.LoadPortID = "";

                // ============================================================
                // Done
                // ============================================================
                CurrentStep = SimStep.Done;
                Gem300Monitor.AddInfo("========== 模擬量測流程完成 ==========");
                InsertLog.SavetoDB(67, "SimulateMeasureFlow Done  LP=" + _loadPortNumber + " CarrierID=" + _simulateCarrierID);
            }
            catch (Exception ex)
            {
                Gem300Monitor.AddAlarm("SimulateMeasureFlow 異常: " + ex.Message);
                InsertLog.SavetoDB(67, "SimulateMeasureFlow Error: " + ex.Message);
            }
            finally
            {
                _isRunning = false;
            }
        }

        /// <summary>
        /// 模擬產生某個 Slot 的量測結果資料，並更新 SECS SV
        /// </summary>
        private static void SimulateMeasurementResult(int slotIdx)
        {
            Random rnd = new Random(DateTime.Now.Millisecond + slotIdx);
            string slotInfo = "";

            int rotateCount = (sram.Recipe.Rotate_Count > 0) ? sram.Recipe.Rotate_Count : 4;
            int recipeType = sram.Recipe.Type;

            slotInfo += recipeType + ",";
            slotInfo += rotateCount + ",";

            for (int j = 0; j < rotateCount; j++)
            {
                double angle = 0;
                if (rotateCount == 4)
                    angle = (sram.Recipe.Angle != null && sram.Recipe.Angle.Length > j * 2) ? sram.Recipe.Angle[j * 2] : j * 90;
                else
                    angle = (sram.Recipe.Angle != null && sram.Recipe.Angle.Length > j) ? sram.Recipe.Angle[j] : j * 45;

                slotInfo += angle + ",";

                double h1 = Math.Round(50.0 + rnd.NextDouble() * 10.0, 2);
                double w1 = Math.Round(100.0 + rnd.NextDouble() * 20.0, 2);
                double h2 = Math.Round(50.0 + rnd.NextDouble() * 10.0, 2);
                double w2 = Math.Round(100.0 + rnd.NextDouble() * 20.0, 2);

                slotInfo += h1 + ",";
                slotInfo += w1 + ",";
                slotInfo += h2 + ",";
                slotInfo += w2 + ",";

                if (slotIdx < 25 && j < 8)
                {
                    fram.EFEMSts.H1[slotIdx, j] = h1;
                    fram.EFEMSts.W1[slotIdx, j] = w1;
                    fram.EFEMSts.H2[slotIdx, j] = h2;
                    fram.EFEMSts.W2[slotIdx, j] = w2;
                }
            }

            Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Slot1_Info + slotIdx, slotInfo, out err);
        }
    }
}
