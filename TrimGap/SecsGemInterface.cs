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
                long rtn = _gemControler.UpdateSV((ulong)id, val, out errLog);
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
        public int LoadportMatchToRun(string lpno)
        {
            if (type == 1)
            {
                return MainForm.LoadportMatchToRun(lpno);
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

        public int SetCarrierStatus_ID(string data, DemoFormDiaGemLib.CarrierIDState state)
        {
            if (type == 1)
                return MainForm.SetCarrierStatus_ID(data, state);
            else
                return 0;
        }

        public int CreateCarrier(string foupid)
        {
            if (type == 1)
                return MainForm.CreateCarrier(foupid);
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

        public int SetCarrierStatus_SlotMap(string foupid, DemoFormDiaGemLib.SlotMapState state)
        {
            if (type == 1)
                return MainForm.SetCarrierStatus_SlotMap(foupid, state);
            else
                return 0;
        }
    }
}
