using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Delta.DIAAuto.DIASECSGEM;
using Delta.DIAAuto.DIASECSGEM.Equipment;
using Delta.DIAAuto.DIASECSGEM.GEMDataModel;

namespace TrimGap
{
    public enum SecsData
    {
        SlotMap,
        Release,
        MeasureStart,
        Cancel,
        AccessModeChange,
        AccessModeAsk,
        ChangeRecipe,
    }
    public enum SecsDataElement
    {
        LoadPortID,
        CarrierID,
    }
    class SecsGemInterface
    {
        private static int type;
        public static SECSGEM.SecsgemForm SecsgemForm;
        public static CTLT.ctlt_GEM CGWrapper;

        public static DemoFormDiaGemLib.MainForm MainForm;
        public static DIASecsGemController _gemControler;
        public static string err = string.Empty;

        public int CarrierReCreate = 0;

        public bool bWaitSECS_ACCESSMODE_ASK
        {
            get
            {
                if (type == 0)
                {
                    return SecsgemForm.bWaitSECS_ACCESSMODE_ASK;
                }
                else if (type == 1)
                {
                    return MainForm.bWaitSECS_ACCESSMODE_ASK;
                }
                else
                    return false;
            }
            set
            {
                if (type == 0)
                {
                    SecsgemForm.bWaitSECS_ACCESSMODE_ASK = value;
                }
                else if (type == 1)
                {
                    MainForm.bWaitSECS_ACCESSMODE_ASK = value;
                }
            }
        }
        public bool bWaitSECS_PORT_TRANSFERSTATUS_ASK
        {
            get
            {
                if (type == 0)
                {
                    return SecsgemForm.bWaitSECS_PORT_TRANSFERSTATUS_ASK;
                }
                else if (type == 1)
                {
                    return MainForm.bWaitSECS_PORT_TRANSFERSTATUS_ASK;
                }
                else
                    return false;
            }
            set
            {
                if (type == 0)
                {
                    SecsgemForm.bWaitSECS_PORT_TRANSFERSTATUS_ASK = value;
                }
                else if (type == 1)
                {
                    MainForm.bWaitSECS_PORT_TRANSFERSTATUS_ASK = value;
                }
            }
        }
        public bool bWaitSECS_PP_SELECT
        {
            get
            {
                if (type == 0)
                {
                    return SecsgemForm.bWaitSECS_PP_SELECT;
                }
                else if (type == 1)
                {
                    return MainForm.bWaitSECS_PP_SELECT;
                }
                else
                    return false;
            }
            set
            {
                if (type == 0)
                {
                    SecsgemForm.bWaitSECS_PP_SELECT = value;
                }
                else if (type == 1)
                {
                    MainForm.bWaitSECS_PP_SELECT = value;
                }
            }
        }
        public bool bWaitSECS_SlotMapCmd
        {
            get
            {
                if (type == 0)
                {
                    return SecsgemForm.bWaitSECS_SlotMapCmd;
                }
                else if (type == 1)
                {
                    return MainForm.bWaitSECS_SlotMapCmd;
                }
                else
                    return false;
            }
            set
            {
                if (type == 0)
                {
                    SecsgemForm.bWaitSECS_SlotMapCmd = value;
                }
                else if (type == 1)
                {
                    MainForm.bWaitSECS_SlotMapCmd = value;
                }
            }
        }
        public bool bWaitSECS_MeasureCmd
        {
            get
            {
                if (type == 0)
                {
                    return SecsgemForm.bWaitSECS_MeasureCmd;
                }
                else if (type == 1)
                {
                    return MainForm.bWaitSECS_MeasureCmd;
                }
                else
                    return false;
            }
            set
            {
                if (type == 0)
                {
                    SecsgemForm.bWaitSECS_MeasureCmd = value;
                }
                else if (type == 1)
                {
                    MainForm.bWaitSECS_MeasureCmd = value;
                }
            }
        }
        public bool bWaitSECS_ReleaseCmd
        {
            get
            {
                if (type == 0)
                {
                    return SecsgemForm.bWaitSECS_ReleaseCmd;
                }
                else if (type == 1)
                {
                    return MainForm.bWaitSECS_ReleaseCmd;
                }
                else
                    return false;
            }
            set
            {
                if (type == 0)
                {
                    SecsgemForm.bWaitSECS_ReleaseCmd = value;
                }
                else if (type == 1)
                {
                    MainForm.bWaitSECS_ReleaseCmd = value;
                }
            }
        }
        public bool bWaitSECS_CancelCmd
        {
            get
            {
                if (type == 0)
                {
                    return SecsgemForm.bWaitSECS_CancelCmd;
                }
                else if (type == 1)
                {
                    return MainForm.bWaitSECS_CancelCmd;
                }
                else
                    return false;
            }
            set
            {
                if (type == 0)
                {
                    SecsgemForm.bWaitSECS_CancelCmd = value;
                }
                else if (type == 1)
                {
                    MainForm.bWaitSECS_CancelCmd = value;
                }
            }
        }
        public bool bSECS_ReadyToLoad_LP1
        {
            get
            {
                if (type == 0)
                {
                    return SecsgemForm.bSECS_ReadyToLoad_LP1;
                }
                else if (type == 1)
                {
                    return MainForm.bSECS_ReadyToLoad_LP1;
                }
                else
                    return false;
            }
            set
            {
                if (type == 0)
                {
                    SecsgemForm.bSECS_ReadyToLoad_LP1 = value;
                }
                else if (type == 1)
                {
                    MainForm.bSECS_ReadyToLoad_LP1 = value;
                }
            }
        }
        public bool bSECS_ReadyToLoad_LP2
        {
            get
            {
                if (type == 0)
                {
                    return SecsgemForm.bSECS_ReadyToLoad_LP2;
                }
                else if (type == 1)
                {
                    return MainForm.bSECS_ReadyToLoad_LP2;
                }
                else
                    return false;
            }
            set
            {
                if (type == 0)
                {
                    SecsgemForm.bSECS_ReadyToLoad_LP2 = value;
                }
                else if (type == 1)
                {
                    MainForm.bSECS_ReadyToLoad_LP2 = value;
                }
            }
        }
        public bool bSECS_ChangeAccessMode_Recive
        {
            get
            {
                if (type == 0)
                {
                    return SecsgemForm.bSECS_ChangeAccessMode_Recive;
                }
                else if (type == 1)
                {
                    return MainForm.bSECS_ChangeAccessMode_Recive;
                }
                else
                    return false;
            }
            set
            {
                if (type == 0)
                {
                    SecsgemForm.bSECS_ChangeAccessMode_Recive = value;
                }
                else if (type == 1)
                {
                    MainForm.bSECS_ChangeAccessMode_Recive = value;
                }
            }
        }
        public bool bWaitSECS_StartCmd
        {
            get
            {
                if (type == 0)
                {
                    return SecsgemForm.bWaitSECS_StartCmd;
                }
                else if (type == 1)
                {
                    return MainForm.bWaitSECS_StartCmd;
                }
                else
                    return false;
            }
            set
            {
                if (type == 0)
                {
                    SecsgemForm.bWaitSECS_StartCmd = value;
                }
                else if (type == 1)
                {
                    MainForm.bWaitSECS_StartCmd = value;
                }
            }
        }
        public bool bWaitSECS_StopCmd
        {
            get
            {
                if (type == 0)
                {
                    return SecsgemForm.bWaitSECS_StopCmd;
                }
                else if (type == 1)
                {
                    return MainForm.bWaitSECS_StopCmd;
                }
                else
                    return false;
            }
            set
            {
                if (type == 0)
                {
                    SecsgemForm.bWaitSECS_StopCmd = value;
                }
                else if (type == 1)
                {
                    MainForm.bWaitSECS_StopCmd = value;
                }
            }
        }

        public string LoadPortID
        {
            get
            {
                if (type == 0)
                {
                    return SecsgemForm.LoadPortID;
                }
                else if (type == 1)
                {
                    return MainForm.LoadPortID;
                }
                else
                    return "";
            }
            set
            {
                if (type == 0)
                {
                    SecsgemForm.LoadPortID = value;
                }
                else if (type == 1)
                {
                    MainForm.LoadPortID = value;
                }
            }
        }
        public string CarrierID
        {
            get
            {
                if (type == 0)
                {
                    return SecsgemForm.CarrierID;
                }
                else if (type == 1)
                {
                    return MainForm.CarrierID;
                }
                else
                    return "";
            }
            set
            {
                if (type == 0)
                {
                    SecsgemForm.CarrierID = value;
                }
                else if (type == 1)
                {
                    MainForm.CarrierID = value;
                }
            }
        }
        public string RecipeID
        {
            get
            {
                if (type == 0)
                {
                    return SecsgemForm.RecipeID;
                }
                else if (type == 1)
                {
                    return MainForm.RecipeID;
                }
                else
                    return "";
            }
            set
            {
                if (type == 0)
                {
                    SecsgemForm.RecipeID = value;
                }
                else if (type == 1)
                {
                    MainForm.RecipeID = value;
                }
            }
        }
        public string AccessMode
        {
            get
            {
                if (type == 0)
                {
                    return SecsgemForm.AccessMode;
                }
                else if (type == 1)
                {
                    return MainForm.AccessMode;
                }
                else
                    return "";
            }
            set
            {
                if (type == 0)
                {
                    SecsgemForm.AccessMode = value;
                }
                else if (type == 1)
                {
                    MainForm.AccessMode = value;
                }
            }
        }

        public void SecsDataClear(SecsData s)
        {
            if (type == 0)
            {
                switch(s)
                {
                    case SecsData.SlotMap:
                        SecsgemForm.SlotMapData.Clear();
                        break;
                    case SecsData.Release:
                        SecsgemForm.ReleaseData.Clear();
                        break;
                    case SecsData.MeasureStart:
                        SecsgemForm.MeasureStartData.Clear();
                        break;
                    case SecsData.Cancel:
                        SecsgemForm.CancelData.Clear();
                        break;
                    case SecsData.AccessModeChange:
                        SecsgemForm.AccessModeChangeData.Clear();
                        break;
                    case SecsData.AccessModeAsk:
                        SecsgemForm.AccessModeAskData.Clear();
                        break;
                    case SecsData.ChangeRecipe:
                        SecsgemForm.ChangeRecipeData.Clear();
                        break;
                    default:
                        break;
                }
            }
            else if (type == 1)
            {
                switch (s)
                {
                    case SecsData.SlotMap:
                        MainForm.SlotMapData.Clear();
                        break;
                    case SecsData.Release:
                        MainForm.ReleaseData.Clear();
                        break;
                    case SecsData.MeasureStart:
                        MainForm.MeasureStartData.Clear();
                        break;
                    case SecsData.Cancel:
                        MainForm.CancelData.Clear();
                        break;
                    case SecsData.AccessModeChange:
                        MainForm.AccessModeChangeData.Clear();
                        break;
                    case SecsData.AccessModeAsk:
                        MainForm.AccessModeAskData.Clear();
                        break;
                    case SecsData.ChangeRecipe:
                        MainForm.ChangeRecipeData.Clear();
                        break;
                    default:
                        break;
                }
            }
        }
        public string SecsDataGet(SecsData s, SecsDataElement e)
        {
            string rtn = "";
            if(e == SecsDataElement.LoadPortID)
            {
                if (type == 0)
                {
                    switch (s)
                    {
                        case SecsData.SlotMap:
                            rtn = SecsgemForm.SlotMapData.LoadPortID;
                            break;
                        case SecsData.Release:
                            rtn = SecsgemForm.ReleaseData.LoadPortID;
                            break;
                        case SecsData.MeasureStart:
                            rtn = SecsgemForm.MeasureStartData.LoadPortID;
                            break;
                        case SecsData.Cancel:
                            rtn = SecsgemForm.CancelData.LoadPortID;
                            break;
                        case SecsData.AccessModeChange:
                            rtn = SecsgemForm.AccessModeChangeData.LoadPortID;
                            break;
                        case SecsData.AccessModeAsk:
                            rtn = SecsgemForm.AccessModeAskData.LoadPortID;
                            break;
                        case SecsData.ChangeRecipe:
                            rtn = SecsgemForm.ChangeRecipeData.LoadPortID;
                            break;
                        default:
                            break;
                    }
                }
                else if (type == 1)
                {
                    switch (s)
                    {
                        case SecsData.SlotMap:
                            rtn = MainForm.SlotMapData.LoadPortID;
                            break;
                        case SecsData.Release:
                            rtn = MainForm.ReleaseData.LoadPortID;
                            break;
                        case SecsData.MeasureStart:
                            rtn = MainForm.MeasureStartData.LoadPortID;
                            break;
                        case SecsData.Cancel:
                            rtn = MainForm.CancelData.LoadPortID;
                            break;
                        case SecsData.AccessModeChange:
                            rtn = MainForm.AccessModeChangeData.LoadPortID;
                            break;
                        case SecsData.AccessModeAsk:
                            rtn = MainForm.AccessModeAskData.LoadPortID;
                            break;
                        case SecsData.ChangeRecipe:
                            rtn = MainForm.ChangeRecipeData.LoadPortID;
                            break;
                        default:
                            break;
                    }
                }
            }
            else if(e == SecsDataElement.CarrierID)
            {
                if (type == 0)
                {
                    switch (s)
                    {
                        case SecsData.SlotMap:
                            rtn = SecsgemForm.SlotMapData.CarrierID;
                            break;
                        case SecsData.Release:
                            rtn = SecsgemForm.ReleaseData.CarrierID;
                            break;
                        case SecsData.MeasureStart:
                            rtn = SecsgemForm.MeasureStartData.CarrierID;
                            break;
                        case SecsData.Cancel:
                            rtn = SecsgemForm.CancelData.CarrierID;
                            break;
                        case SecsData.AccessModeChange:
                            rtn = SecsgemForm.AccessModeChangeData.CarrierID;
                            break;
                        case SecsData.AccessModeAsk:
                            rtn = SecsgemForm.AccessModeAskData.CarrierID;
                            break;
                        case SecsData.ChangeRecipe:
                            rtn = SecsgemForm.ChangeRecipeData.CarrierID;
                            break;
                        default:
                            break;
                    }
                }
                else if (type == 1)
                {
                    switch (s)
                    {
                        case SecsData.SlotMap:
                            rtn = MainForm.SlotMapData.CarrierID;
                            break;
                        case SecsData.Release:
                            rtn = MainForm.ReleaseData.CarrierID;
                            break;
                        case SecsData.MeasureStart:
                            rtn = MainForm.MeasureStartData.CarrierID;
                            break;
                        case SecsData.Cancel:
                            rtn = MainForm.CancelData.CarrierID;
                            break;
                        case SecsData.AccessModeChange:
                            rtn = MainForm.AccessModeChangeData.CarrierID;
                            break;
                        case SecsData.AccessModeAsk:
                            rtn = MainForm.AccessModeAskData.CarrierID;
                            break;
                        case SecsData.ChangeRecipe:
                            rtn = MainForm.ChangeRecipeData.CarrierID;
                            break;
                        default:
                            break;
                    }
                }
            }
            return rtn;
        }

        public void SecsDataSet(SecsData s, SecsDataElement e, string value)
        {
            if (e == SecsDataElement.LoadPortID)
            {
                if (type == 0)
                {
                    switch (s)
                    {
                        case SecsData.SlotMap:
                            SecsgemForm.SlotMapData.LoadPortID = value;
                            break;
                        case SecsData.Release:
                            SecsgemForm.ReleaseData.LoadPortID = value;
                            break;
                        case SecsData.MeasureStart:
                            SecsgemForm.MeasureStartData.LoadPortID = value;
                            break;
                        case SecsData.Cancel:
                            SecsgemForm.CancelData.LoadPortID = value;
                            break;
                        case SecsData.AccessModeChange:
                            SecsgemForm.AccessModeChangeData.LoadPortID = value;
                            break;
                        case SecsData.AccessModeAsk:
                            SecsgemForm.AccessModeAskData.LoadPortID = value;
                            break;
                        case SecsData.ChangeRecipe:
                            SecsgemForm.ChangeRecipeData.LoadPortID = value;
                            break;
                        default:
                            break;
                    }
                }
                else if (type == 1)
                {
                    switch (s)
                    {
                        case SecsData.SlotMap:
                            MainForm.SlotMapData.LoadPortID = value;
                            break;
                        case SecsData.Release:
                            MainForm.ReleaseData.LoadPortID = value;
                            break;
                        case SecsData.MeasureStart:
                            MainForm.MeasureStartData.LoadPortID = value;
                            break;
                        case SecsData.Cancel:
                            MainForm.CancelData.LoadPortID = value;
                            break;
                        case SecsData.AccessModeChange:
                            MainForm.AccessModeChangeData.LoadPortID = value;
                            break;
                        case SecsData.AccessModeAsk:
                            MainForm.AccessModeAskData.LoadPortID = value;
                            break;
                        case SecsData.ChangeRecipe:
                            MainForm.ChangeRecipeData.LoadPortID = value;
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (e == SecsDataElement.CarrierID)
            {
                if (type == 0)
                {
                    switch (s)
                    {
                        case SecsData.SlotMap:
                            SecsgemForm.SlotMapData.CarrierID = value;
                            break;
                        case SecsData.Release:
                            SecsgemForm.ReleaseData.CarrierID = value;
                            break;
                        case SecsData.MeasureStart:
                            SecsgemForm.MeasureStartData.CarrierID = value;
                            break;
                        case SecsData.Cancel:
                            SecsgemForm.CancelData.CarrierID = value;
                            break;
                        case SecsData.AccessModeChange:
                            SecsgemForm.AccessModeChangeData.CarrierID = value;
                            break;
                        case SecsData.AccessModeAsk:
                            SecsgemForm.AccessModeAskData.CarrierID = value;
                            break;
                        case SecsData.ChangeRecipe:
                            SecsgemForm.ChangeRecipeData.CarrierID = value;
                            break;
                        default:
                            break;
                    }
                }
                else if (type == 1)
                {
                    switch (s)
                    {
                        case SecsData.SlotMap:
                            MainForm.SlotMapData.CarrierID = value;
                            break;
                        case SecsData.Release:
                            MainForm.ReleaseData.CarrierID = value;
                            break;
                        case SecsData.MeasureStart:
                            MainForm.MeasureStartData.CarrierID = value;
                            break;
                        case SecsData.Cancel:
                            MainForm.CancelData.CarrierID = value;
                            break;
                        case SecsData.AccessModeChange:
                            MainForm.AccessModeChangeData.CarrierID = value;
                            break;
                        case SecsData.AccessModeAsk:
                            MainForm.AccessModeAskData.CarrierID = value;
                            break;
                        case SecsData.ChangeRecipe:
                            MainForm.ChangeRecipeData.CarrierID = value;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public SecsGemInterface(int t)
        {
            type = t;
        }

        public void InitSecs()
        {
            if(type == 0)
            {
                SecsgemForm = new SECSGEM.SecsgemForm();
                CGWrapper = SECSGEM.SecsgemForm.CGWrapper;
                CGWrapper.UpdateSV(GemSystemID.GEM_SOFTREV, sram.SofewareVersion); // 先更新版本號，把SECS裡自動更新版本號的地方刪除
                SecsgemForm.Show();
                SpinWait.SpinUntil(() => CGWrapper.GetCurrentCommState().GetHashCode() == 7, 5000);
                SecsgemForm.Hide();

                // init secs後 先update EC 更新機台參數

                CGWrapper.UpdateEC(TrimGap_EqpID.ChunkRotateDistance, fram.m_posV[0]);
                CGWrapper.UpdateEC(TrimGap_EqpID.Cofficient, fram.Analysis.Coefficient);
                CGWrapper.UpdateEC(TrimGap_EqpID.RobotSpeed, 50);
                CGWrapper.UpdateEC(TrimGap_EqpID.SoftWareVersion, 110);  // 版本號 目前是1.1.0.0503 ，更新前面三碼就好，這個之後再改成直接抓版本號
                if (fram.S_MotionRotate == "True")
                {
                    CGWrapper.UpdateEC(TrimGap_EqpID.MotionRotate, 1);
                }
                else
                {
                    CGWrapper.UpdateEC(TrimGap_EqpID.MotionRotate, 0);
                }
                CGWrapper.UpdateSV(TrimGap_EqpID.Loadport1_AccessMode, fram.SECSPara.Loadport1_AccessMode);              // 開啟程式後先更新 Access Mode & PortTranmsferState
                CGWrapper.UpdateSV(TrimGap_EqpID.Loadport2_AccessMode, fram.SECSPara.Loadport2_AccessMode);
                CGWrapper.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, fram.SECSPara.Loadport1_PortTransferState);
                CGWrapper.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, fram.SECSPara.Loadport2_PortTransferState);
            }
            else if (type == 1) //台達
            {
                int result = 0;
                MainForm = new DemoFormDiaGemLib.MainForm();
                MainForm.Initnal_SECSFunction_CallBack(SECSListening.FunctionCommand_CallBack);
                MainForm.Initnal_RemoteCommand_CallBack(SECSListening.RemoteCommand_CallBack);
                _gemControler = DemoFormDiaGemLib.MainForm._gemControler;
                _gemControler.UpdateSV(GemSystemID.GEM_SOFTREV, sram.SofewareVersion, out err); // 先更新版本號，把SECS裡自動更新版本號的地方刪除
                MainForm.Show();
                result = _gemControler.DriverStart(out err);
                SpinWait.SpinUntil(() => _gemControler.SECSDriverStatus == eSECSDriverConnectState.Connection, 7000);
                
                if (_gemControler.SECSDriverStatus == eSECSDriverConnectState.Connection)
                {
                    Thread.Sleep(1000);
                    result = _gemControler.EnableComm(out err);
                    SpinWait.SpinUntil(() => _gemControler.CommunicationState == eCommunicationState.Communicating, 7000);
                    if (_gemControler.CommunicationState == eCommunicationState.Communicating)
                    {
                        Thread.Sleep(1000);
                        result = _gemControler.OnlineRemote(out err);
                        SpinWait.SpinUntil(() => _gemControler.ControlMode == eControlMode.OnlineRemote, 7000);
                    }
                }
                MainForm.Hide();

                // ⭐ 初始化所有 ASCII 類型的 SV，避免 S1F3 查詢時回傳 zero-length 導致 Host 報錯
                // _gemControler_InitialCompleted 可能在 DriverStart 之前觸發，UpdateSV 可能失敗，
                // 因此在此處（Driver 已連線、Comm 已啟用後）重新確保所有 ASCII SV 有值
                _gemControler.UpdateSV(1, " ", out err);                                          // SYS_LICENSE_CODE
                _gemControler.UpdateSV(3, DateTime.Now.ToString("yyyyMMddHHmmssff"), out err);    // SYS_CLOCK
                _gemControler.UpdateSV(9, "TrimGap", out err);                                    // SYS_MDLN
                _gemControler.UpdateSV(10, sram.SofewareVersion, out err);                        // SYS_SOFTREV
                _gemControler.UpdateSV(14, " ", out err);                                         // SYS_PP_EXEC_NAME
                _gemControler.UpdateSV(19, "0000000000000000", out err);                          // SYS_SPOOL_START_TIME
                _gemControler.UpdateSV(20, "0000000000000000", out err);                          // SYS_SPOOL_FULL_TIME
                _gemControler.UpdateSV(24, "1.0", out err);                                       // SYS_SOFTWARE_REVISION
                _gemControler.UpdateSV(25, "TrimGap_1", out err);                                 // GEM_EQP_SERIAL_NUM
                _gemControler.UpdateSV(26, "TSMC", out err);                                      // GEM_E30_EQUIPMENT_SUPPLIER

                // ⭐ 20260312 新增: 初始化所有 EqpDV.csv 中的 ASCII VIDs，避免 S1F3 查詢 zero-length 報錯
                _gemControler.UpdateSV(TrimGap_EqpID.RecipeID, " ", out err);
                _gemControler.UpdateSV(TrimGap_EqpID.CarrierID, " ", out err);
                _gemControler.UpdateSV(TrimGap_EqpID.Loadport1_RecipeID, " ", out err);
                _gemControler.UpdateSV(TrimGap_EqpID.Loadport2_RecipeID, " ", out err);
                _gemControler.UpdateSV(TrimGap_EqpID.LotID, " ", out err);
                _gemControler.UpdateSV(TrimGap_EqpID.SubstrateID, " ", out err);
                _gemControler.UpdateSV(TrimGap_EqpID.MeasurementMax, " ", out err);

                for (int i = 0; i < 25; i++)
                {
                    _gemControler.UpdateSV((ulong)(TrimGap_EqpID.Slot1_Info + i), " ", out err);
                    _gemControler.UpdateSV((ulong)(TrimGap_EqpID.Slot1_Max + i), " ", out err);
                }
                for (int i = 0; i < 8; i++)
                {
                    _gemControler.UpdateSV((ulong)(TrimGap_EqpID.Angle1_Info + i), " ", out err);
                }

                // ⭐ 初始化其他 "Set by AP" 的數值型與 List SV避免未初始化長度為 0
                _gemControler.UpdateSV(4, (byte)0, out err);                                      // SYS_SECS_COMM_MODE (0:HSMS)
                _gemControler.UpdateSV(7, (byte)1, out err);                                      // SYS_PREVIOUS_PROCESS_STATE
                _gemControler.UpdateSV(8, (byte)1, out err);                                      // SYS_PROCESS_STATE
                _gemControler.UpdateSV(15, (byte)2, out err);                                     // SYS_PP_FORMAT (2:Formatted)
                _gemControler.UpdateSV(16, (byte)1, out err);                                     // SYS_SPOOL_STATE
                _gemControler.UpdateSV(17, (byte)0, out err);                                     // SYS_SPOOL_LOAD_SUBSTATE
                _gemControler.UpdateSV(18, (byte)0, out err);                                     // SYS_SPOOL_UNLOAD_SUBSTATE

                // ⭐ 初始化 SystemSV.csv 中物件綁定 (Object-Bound) 的 ASCII SV
                // CARRIERLOC / SUBSTLOC / EPTTRACKER 物件在 config 中預設存在，
                // 但其 ASCII 欄位啟動時為空字串 (zero-length)，導致 S1F3 查詢失敗
                // 使用 SystemID 對應 SystemSV.csv 的第 7 欄
                // -- CARRIERLOC "QWE" --
                _gemControler.UpdateSV(312, " ", out err);   // CMS_CARRIERID1_QWE
                _gemControler.UpdateSV(313, " ", out err);   // CMS_OBJID1_QWE
                // -- SUBSTLOC "QWE" (啟動時會被刪除，但 SV 可能仍存在) --
                _gemControler.UpdateSV(314, " ", out err);   // STS_SUBSTID1_QWE
                _gemControler.UpdateSV(316, " ", out err);   // STS_OBJID1_QWE
                // -- CARRIERLOC "QWET" --
                _gemControler.UpdateSV(317, " ", out err);   // CMS_CARRIERID2_QWET
                _gemControler.UpdateSV(318, " ", out err);   // CMS_OBJID2_QWET
                // -- EPTTRACKER "QWEASD" --
                _gemControler.UpdateSV(323, " ", out err);   // EPT_TRANSITIONTIMESTAMP1_QWEASD
                _gemControler.UpdateSV(324, " ", out err);   // EPT_OBJID1_QWEASD
                _gemControler.UpdateSV(326, " ", out err);   // EPT_EPTELEMENTNAME1_QWEASD
                _gemControler.UpdateSV(329, " ", out err);   // EPT_PREVIOUSTASKNAME1_QWEASD
                _gemControler.UpdateSV(330, " ", out err);   // EPT_BLOCKEDREASONTEXT1_QWEASD
                _gemControler.UpdateSV(334, " ", out err);   // EPT_TASKNAME1_QWEASD
                // -- SUBSTLOC "1" (啟動時會被刪除，但 SV 可能仍存在) --
                _gemControler.UpdateSV(563, " ", out err);   // STS_SUBSTID2_1
                _gemControler.UpdateSV(565, " ", out err);   // STS_OBJID2_1

                // ⭐ 20260312 新增: 初始化 Host S1F3 SVID Set 2 所查詢的全部 VIDs
                // -- LoadPort 額外屬性 (2026-2029) --
                _gemControler.UpdateSV(2026, (byte)0, out err);   // LP1 ReservationState
                _gemControler.UpdateSV(2027, (byte)0, out err);   // LP2 ReservationState
                _gemControler.UpdateSV(2028, " ", out err);      // LP1 CarrierID
                _gemControler.UpdateSV(2029, " ", out err);      // LP2 CarrierID
                // -- Unknown SVs (100092-100094) --
                _gemControler.UpdateSV(100092, " ", out err);
                _gemControler.UpdateSV(100093, " ", out err);
                _gemControler.UpdateSV(100094, " ", out err);
                // -- SUBSTRATE attributes (100262-100265) --
                _gemControler.UpdateSV(100262, " ", out err);    // STS_LOTID
                _gemControler.UpdateSV(100263, (byte)0, out err); // STS_MATERIALSTATUS
                _gemControler.UpdateSV(100264, " ", out err);    // STS_SUBSTDESTINATION
                // STS_SUBSTHISTORY (LIST)
                {
                    ListWrapper lwHist = new ListWrapper();
                    lwHist.TryAdd(ItemFmt.A, " ", out err);
                    _gemControler.UpdateSV(100265, lwHist, out err);
                }
                // -- CARRIER instance SVs (100100-100107) --
                _gemControler.UpdateSV(100100, (byte)0, out err);  // CMS_CARRIERIDSTATUS (UINT_1)
                _gemControler.UpdateSV(100101, (byte)0, out err);  // CMS_CARRIERACCESSINGSTATUS (UINT_1)
                _gemControler.UpdateSV(100102, (byte)0, out err);  // CMS_SUBSTRATECOUNT (UINT_1)
                // CMS_CONTENTMAP (LIST)
                {
                    ListWrapper lwCM = new ListWrapper();
                    lwCM.TryAdd(ItemFmt.A, " ", out err);
                    _gemControler.UpdateSV(100103, lwCM, out err);
                }
                // CMS_SLOTMAP (LIST)
                {
                    ListWrapper lwSM = new ListWrapper();
                    lwSM.TryAdd(ItemFmt.A, " ", out err);
                    _gemControler.UpdateSV(100104, lwSM, out err);
                }
                _gemControler.UpdateSV(100105, " ", out err);     // CMS_LOCATIONID (ASCII)
                _gemControler.UpdateSV(100106, (byte)0, out err);  // CMS_SLOTMAPSTATUS (UINT_1)
                _gemControler.UpdateSV(100107, " ", out err);     // CMS_USAGE (ASCII)
                // -- SUBSTRATE instance SVs (100108-100117) --
                _gemControler.UpdateSV(100108, " ", out err);     // STS_SUBSTLOCID (ASCII)
                _gemControler.UpdateSV(100109, (byte)0, out err);  // STS_SUBSTPROCSTATE (UINT_1)
                _gemControler.UpdateSV(100110, " ", out err);     // STS_SUBSTSOURCE (ASCII)
                _gemControler.UpdateSV(100111, (byte)0, out err);  // STS_SUBSTSTATE (UINT_1)
                _gemControler.UpdateSV(100112, (byte)0, out err);  // STS_SUBSTTYPE (UINT_1)
                _gemControler.UpdateSV(100113, (byte)0, out err);  // STS_SUBSTUSAGE (UINT_1)
                _gemControler.UpdateSV(100114, " ", out err);     // STS_SUBSTACQUIREDID (ASCII)
                _gemControler.UpdateSV(100115, " ", out err);     // STS_SUBSTBATCHLOCID (ASCII)
                _gemControler.UpdateSV(100116, " ", out err);     // STS_SUBSTSUBSTIDSTATUS (ASCII)
                _gemControler.UpdateSV(100117, " ", out err);     // STS_SUBSTSUBSTPOSINBATCH (ASCII)
                // -- Remaining SVs (100118-100150) --
                for (int i = 100118; i <= 100150; i++)
                {
                    _gemControler.UpdateSV((ulong)i, " ", out err);
                }

                // init secs後 先update EC 更新機台參數

                _gemControler.UpdateEC(TrimGap_EqpID.ChunkRotateDistance, Convert.ToUInt16(fram.m_posV[0]), true, out err);
                _gemControler.UpdateEC(TrimGap_EqpID.Cofficient, (float)(fram.Analysis.Coefficient), true, out err);
                _gemControler.UpdateEC(TrimGap_EqpID.RobotSpeed, (byte)50, true, out err);
                _gemControler.UpdateEC(TrimGap_EqpID.SoftWareVersion, (byte)110, true, out err);  // 版本號 目前是1.1.0.0503 ，更新前面三碼就好，這個之後再改成直接抓版本號

                if (fram.S_MotionRotate == "True")
                {
                    _gemControler.UpdateEC(TrimGap_EqpID.MotionRotate, (byte)1, true, out err);
                }
                else
                {
                    _gemControler.UpdateEC(TrimGap_EqpID.MotionRotate, (byte)0, true, out err);
                }
                _gemControler.UpdateSV(TrimGap_EqpID.Loadport1_AccessMode, (byte)fram.SECSPara.Loadport1_AccessMode, out err);              // 開啟程式後先更新 Access Mode & PortTranmsferState
                _gemControler.UpdateSV(TrimGap_EqpID.Loadport2_AccessMode, (byte)fram.SECSPara.Loadport2_AccessMode, out err);
                _gemControler.UpdateSV(TrimGap_EqpID.Loadport1_PortTransferState, (byte)fram.SECSPara.Loadport1_PortTransferState, out err);
                _gemControler.UpdateSV(TrimGap_EqpID.Loadport2_PortTransferState, (byte)fram.SECSPara.Loadport2_PortTransferState, out err);
            }
        }

        public bool isRemote()
        {
            return isControlRemote() && isCommunicating();
        }

        public bool isCommunicating()
        {
            if (type == 0)
            {
                try
                {
                    if (SecsgemForm != null && SecsgemForm.Comm_State == 7)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            else if (type == 1) //台達
            {
                try
                {

                    if (MainForm != null && _gemControler.CommunicationState == eCommunicationState.Communicating)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool isControlRemote()
        {
            if (type == 0)
            {
                try
                {
                    if (SecsgemForm != null && SecsgemForm.Control_State == 5)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            else if (type == 1) //台達
            {
                try
                {

                    if (MainForm != null && _gemControler.ControlMode == eControlMode.OnlineRemote)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool isControlLocal()
        {
            if (type == 0)
            {
                try
                {
                    if (SecsgemForm != null && SecsgemForm.Control_State == 4)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            else if (type == 1) //台達
            {
                try
                {

                    if (MainForm != null && _gemControler.ControlMode == eControlMode.OnlineLocal)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void Show()
        {
            if(type == 0)
            {
                SecsgemForm.Show();
            }
            else if(type == 1)
            {
                MainForm.Show();
            }
        }

        public long UpdateSV(int id, object val, out string errLog)
        {
            errLog = string.Empty;
            if (type == 0)
            {
                return CGWrapper.UpdateSV(id, val);
            }
            else if (type == 1)
            {
                long rtn;
                switch (id)
                {
                    case 2006:
                        rtn = MainForm.UpdateLoadPortAccessMode("1", (byte)val);
                        break;
                    case 2007:
                        rtn = MainForm.UpdateLoadPortAccessMode("2", (byte)val);
                        break;
                    case 2008:
                        rtn = MainForm.UpdateLoadPortTransferState("1", (byte)val);
                        break;
                    case 2009:
                        rtn = MainForm.UpdateLoadPortTransferState("2", (byte)val);
                        break;
                    case 2016:
                        rtn = MainForm.UpdateLoadPortAssociationState("1", (byte)val);
                        break;
                    case 2017:
                        rtn = MainForm.UpdateLoadPortAssociationState("2", (byte)val);
                        break;
                    case 2020:
                        rtn = MainForm.UpdateLoadPortPortID("1", (byte)val);
                        break;
                    case 2021:
                        rtn = MainForm.UpdateLoadPortPortID("2", (byte)val);
                        break;
                    default:
                        rtn = _gemControler.UpdateSV((ulong)id, val, out errLog);

                        /*if(rtn != 0)
                        {
                            string[] str = errLog.Split('\'');

                            if (str.Length > 0)
                            {
                                switch(str[2])
                                {
                                    case "U1":
                                        byte val2 = (byte)val;
                                        rtn = _gemControler.UpdateSV((ulong)id, val2, out errLog);
                                        long rtn2 = rtn;
                                        break;
                                    case "U2":
                                        rtn = _gemControler.UpdateSV((ulong)id, (ushort)val, out errLog);
                                        break;
                                    case "U4":
                                        rtn = _gemControler.UpdateSV((ulong)id, (uint)val, out errLog);
                                        break;
                                    default:
                                        break;
                                }
                            }

                        }*/

                        break;
                }

                return rtn;
            }
            else
            {
                return 0;
            }
        }

        public long UpdateEC(int id, object val, out string errLog)
        {
            errLog = string.Empty;
            if (type == 0)
            {
                return CGWrapper.UpdateEC(id, val);
            }
            else if (type == 1)
            {
                return _gemControler.UpdateEC((ulong)id, val, true, out errLog);
            }
            else
            {
                return 0;
            }
        }

        public long EventReportSend(int id, out string errLog)
        {
            errLog = string.Empty;
            if (type == 0)
            {
                return CGWrapper.EventReportSend(id);
            }
            else if (type == 1)
            {
                return _gemControler.EventReportSend((ulong)id, out errLog);
            }
            else
            {
                return 0;
            }
        }

        public long AlarmReportSend(int id, bool bSet, out string errLog)
        {
            errLog = string.Empty;
            if (type == 0)
            {
                if(bSet)
                    return CGWrapper.AlarmReportSend(id, 128);
                else
                    return CGWrapper.AlarmReportSend(id, 0);
            }
            else if (type == 1)
            {
                return _gemControler.AlarmReportSend((ulong)id, bSet, out errLog);
            }
            else
            {
                return 0;
            }
        }

        // ====================  GEM300  ==============================
        public int LoadportMatchToRun(string lpno, ref string[] lotID, ref string[] substrateID )
        {
            if (type == 1)
            {
                return MainForm.LoadportMatchToRun(lpno, ref lotID, ref substrateID);
            }
            else
            {
                return 0;
            }
        }

        public int GetControlJobAttr(string RunningCJ, out string carrierInputSpec, out string curPJ, out string dataCollection, out string mtrloutStatus,
                out string stringmtrloutSpec, out string pauseEvent, out string procCtrlSpec, out byte procOrder, out bool bStart, out byte state, out string err)
        {
            if (type == 1)
                return MainForm.GetControlJobAttr(RunningCJ, out carrierInputSpec, out curPJ, out dataCollection, out mtrloutStatus,
                out stringmtrloutSpec, out pauseEvent, out procCtrlSpec, out procOrder, out bStart, out state, out err);
            else
            {
                carrierInputSpec = "";
                curPJ = "";
                dataCollection = "";
                mtrloutStatus = "";
                stringmtrloutSpec = "";
                pauseEvent = "";
                procCtrlSpec = "";
                procOrder = 0;
                bStart = true;
                state = 0;
                err = "";
                return 0;
            }
        }

        public int GetProcessJobAttr(string objID, out string pauseEvent, out byte PJState, out string carrierID, out byte[] slot, out byte PRType, out bool bStart, out byte recMethod, out string recID, out string recVarList, out string err)
        {
            if (type == 1)
                return MainForm.GetProcessJobAttr(objID, out pauseEvent, out PJState, out carrierID, out slot,
                out PRType, out bStart, out recMethod, out recID, out recVarList, out err);
            else
            {
                pauseEvent = "";
                PJState = 0;
                carrierID = "";
                slot = new byte[25];
                PRType = 0;
                bStart = true;
                recMethod = 0;
                recID = "";
                recVarList = "";
                err = "";
                return 0;
            }
        }

        public int SetCarrierStatus_ID(string data, DemoFormDiaGemLib.CarrierIDState state)
        {
            if (type == 1)
                return MainForm.SetCarrierStatus_ID(data, state);
            else
                return 0;
        }

        public int CreateCarrier(string foupid, string loc)
        {
            if (type == 1)
                return MainForm.CreateCarrier(foupid, loc);
            else
                return 0;
        }

        public int UpdateLoadPortAssociationState(string objID, byte state)
        {
            if (type == 1)
                return MainForm.UpdateLoadPortAssociationState(objID, state);
            else
                return 0;
        }

        public int GetCarrierContentMap(string foupid, out string[] lot, out string[] substrate, out string err)
        {
            lot = null;
            substrate = null;
            err = "";
            if (type == 1)
                return MainForm.GetContentMap(foupid, out lot, out substrate, out err);
            else
                return 0;
        }

        public int DeleteCarrier(string foupid, out string err)  //刪除Carrier Object
        {
            err = "";
            if (type == 1)
                return MainForm.DeleteCarrier(foupid, out err);
            else
                return 0;
        }

        public int SetCarrierAttr_Location(string foupid, string location, byte cap = 25)
        {
            if (type == 1)
                return MainForm.SetCarrierAttr_Location(foupid, location, cap);
            else
                return 0;
        }

        public int SetCarrierAttr_SlotMap(string foupid, int[] slot)
        {
            if (type == 1)
                return MainForm.SetCarrierAttr_SlotMap(foupid, slot);
            else
                return 0;
        }

        public int SetCarrierAttr_ContentMap(string foupid, string[] lotID, string[] substrateID)
        {
            if (type == 1)
                return MainForm.SetCarrierAttr_ContentMap(foupid, lotID, substrateID);
            else
                return 0;
        }

        public int SetCarrierStatus_SlotMap(string foupid, DemoFormDiaGemLib.SlotMapState state)
        {
            if (type == 1)
                return MainForm.SetCarrierStatus_SlotMap(foupid, state);
            else
                return 0;
        }

        public int SetCarrierStatus_Accessing(string foupid, DemoFormDiaGemLib.CarrierAccessingState state)
        {
            if (type == 1)
                return MainForm.SetCarrierStatus_Accessing(foupid, state);
            else
                return 0;
        }

        public int ChangeControlJobState(string cj, DemoFormDiaGemLib.ControlJobState state, int unnormal)
        {
            if (type == 1)
                return MainForm.ChangeControlJobState(cj, state, unnormal);
            else
                return 0;
        }

        public int SetControlJobCurrentPrJob(string cj, string pj)
        {
            if (type == 1)
                return MainForm.SetControlJobCurrentPrJob(cj, pj);
            else
                return 0;
        }

        public int ChangeProcessJobState(string pj, DemoFormDiaGemLib.ProcessJobState state)
        {
            if (type == 1)
                return MainForm.ChangeProcessJobState(pj, state);
            else
                return 0;
        }

        public int CreateSubstrate(string objID, string lotID, string locationID, string objSpec = "")
        {
            if (type == 1)
                return MainForm.CreateSubstrate(objID, lotID, locationID);
            else
                return 0;
        }

        public int SetSubstrateStatus_Proc(string objID, DemoFormDiaGemLib.SubstProcState status)
        {
            if (type == 1)
                return MainForm.SetSubstrateStatus_Proc(objID, status);
            else
                return 0;
        }

        public int SetSubstrateAttr_Location(string objID, string location)
        {
            if (type == 1)
                return MainForm.SetSubstrateAttr_Location(objID, location);
            else
                return 0;
        }

        public int UpdateMeasurementData(string[] angle, double[] h1, double[] w1, double[] h2, double[] w2)
        {
            if (type == 1)
                return MainForm.UpdateMeasurementData( angle, h1, w1, h2, w2 );
            else
                return 0;
        }

        public int UpdateMeasurementMax(double h1, double w1, double h2, double w2)
        {
            if (type == 1)
                return MainForm.UpdateMeasurementMax(h1, w1, h2, w2);
            else
                return 0;
        }

        public int DeleteControlJobWithAssociatedProcessJob(string objID, out string err)
        {
            err = "";
            if (type == 1)
                return MainForm.DeleteControlJobWithAssociatedProcessJob(objID, out err);
            else
                return 0;
        }
        public int DeleteControlJob(string objID, out string err)
        {
            err = "";
            if (type == 1)
                return MainForm.DeleteControlJob(objID, out err);
            else
                return 0;
        }
    }
}
