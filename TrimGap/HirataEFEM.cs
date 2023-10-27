using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;

namespace TrimGap
{
    public class EFEM
    {
        private static SocketClient2.Client cSocket = new SocketClient2.Client();
        private static string _initFlag = "";
        private static string _ReceiveStr = "";
        private static bool _receiveFlag = false;
        public static string err = string.Empty;

        private static string _socket_cmdSend = "";

        public API API = new API();
        public DeviceCommon DeviceCommon = new DeviceCommon();
        public LoadPort LoadPort_Run;
        public LoadPort LoadPort1;
        public LoadPort LoadPort2;
        public LoadPort LoadPort3;
        public LoadPort LoadPort4;
        public Robot Robot = new Robot();
        public Aligner Aligner = new Aligner();
        public IO IO = new IO();
        public E84 E84 = new E84();

        //public Alignment Alignment = new Alignment();
        public Reader Reader = new Reader();

        public Stage Stage1 = new Stage();
        private static string rtn = "";
        private static string[] rtnstring;
        private static bool[] DIState = new bool[11];

        public struct slot_status_Color
        {
            public static Color Empty = Color.LightGray;
            public static Color Ready = Color.Gold;
            public static Color Error = Color.Red;
            public static Color Thickness = Color.MediumVioletRed;
            public static Color Thin = Color.SaddleBrown;
            public static Color ProcessingAligner1 = Color.LightBlue;
            public static Color ProcessingStage1 = Color.Blue;
            public static Color ProcessEnd = Color.LimeGreen;
            public static Color Unknow = Color.Gray;
            public static Color AlignerStageReady = SystemColors.Control;
        }

        public EFEM(string ServerIP, int port, int updateTime, int LoadPortNum)
        {
            cSocket.ipAddress = ServerIP;
            cSocket.portNumber = port;
            cSocket.updateTime = updateTime;   //50會爆掉 先改1000
            cSocket.newLineTF = false;

            _initFlag = cSocket.clientStart();
            Thread _GetMessage = new Thread(getMessage);
            _GetMessage.IsBackground = true;
            _GetMessage.Start();
            switch (LoadPortNum)
            {
                case 1:
                    LoadPort1 = new LoadPort(LoadPort.Pn.P1);
                    break;

                case 2:
                    LoadPort1 = new LoadPort(LoadPort.Pn.P1);
                    LoadPort2 = new LoadPort(LoadPort.Pn.P2);
                    break;

                case 3:
                    LoadPort1 = new LoadPort(LoadPort.Pn.P1);
                    LoadPort2 = new LoadPort(LoadPort.Pn.P2);
                    LoadPort3 = new LoadPort(LoadPort.Pn.P3);
                    break;

                case 4:
                    LoadPort1 = new LoadPort(LoadPort.Pn.P1);
                    LoadPort2 = new LoadPort(LoadPort.Pn.P2);
                    LoadPort3 = new LoadPort(LoadPort.Pn.P3);
                    LoadPort4 = new LoadPort(LoadPort.Pn.P4);
                    break;

                default:
                    break;
            }
        }

        private static string[] rtnSplit(string str)
        {
            string[] Str = Regex.Split(str, "\r", RegexOptions.Singleline);
            string[] Str2 = Regex.Split(Str[0], ",", RegexOptions.Singleline);

            return Str2;
        }

        public bool Pressure_Sts
        {
            get { return DIState[(int)EFEM_DI_State.Pressure]; }
        }

        public bool Vacuum_Sts
        {
            get { return DIState[(int)EFEM_DI_State.Vacuum]; }
        }

        public bool Ionizer_Sts
        {
            get { return DIState[(int)EFEM_DI_State.Ionizer]; }
        }

        public bool FFU1_Sts
        {
            get { return DIState[(int)EFEM_DI_State.FFU1]; }
        }

        public bool FFU2_Sts
        {
            get { return DIState[(int)EFEM_DI_State.FFU2]; }
        }

        public bool FFU3_Sts
        {
            get { return DIState[(int)EFEM_DI_State.FFU3]; }
        }

        public bool RobotMode_Sts
        {
            get { return DIState[(int)EFEM_DI_State.RobotMode]; }
        }

        public bool RobotEnable_Sts
        {
            get { return DIState[(int)EFEM_DI_State.RobotEnable]; }
        }

        public bool Door_Sts
        {
            get { return DIState[(int)EFEM_DI_State.Door]; }
        }

        public bool EMO_Sts
        {
            get { return DIState[(int)EFEM_DI_State.EMO]; }
        }

        public bool Power_Sts
        {
            get { return DIState[(int)EFEM_DI_State.Power]; }
        }

        public string CmdSend
        {
            get { return _socket_cmdSend; }
        }

        public void CmdSend_Clear()
        {
            _socket_cmdSend = "";
        }

        public void CmdRcv_Clear()
        {
            _ReceiveStr = "";
        }

        public bool GetStatus()
        {
            rtn = EFEM.EFEMCommand_Send("GetStatus,EFEM");
            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length < 11)
            {
                return false;
            }
            for (int i = 0; i < 11; i++)
            {
                switch (i)
                {
                    case (int)EFEM_DI_State.Pressure:
                        DIState[i] = rtnstring[i + 2] == "1" ? true : false;
                        break;

                    case (int)EFEM_DI_State.Vacuum:
                        DIState[i] = rtnstring[i + 2] == "1" ? true : false;
                        break;

                    case (int)EFEM_DI_State.Ionizer:
                        DIState[i] = rtnstring[i + 2] == "1" ? true : false;
                        break;

                    case (int)EFEM_DI_State.FFU1:
                        DIState[i] = rtnstring[i + 2] == "1" ? true : false;
                        break;

                    case (int)EFEM_DI_State.FFU2:
                        DIState[i] = rtnstring[i + 2] == "1" ? true : false;
                        break;

                    case (int)EFEM_DI_State.FFU3:
                        DIState[i] = rtnstring[i + 2] == "1" ? true : false;
                        break;

                    case (int)EFEM_DI_State.RobotMode:
                        DIState[i] = rtnstring[i + 2] == "1" ? true : false;
                        break;

                    case (int)EFEM_DI_State.RobotEnable:
                        DIState[i] = rtnstring[i + 2] == "1" ? true : false;
                        break;

                    case (int)EFEM_DI_State.Door:
                        DIState[i] = rtnstring[i + 2] == "1" ? true : false;
                        break;

                    case (int)EFEM_DI_State.EMO:
                        DIState[i] = rtnstring[i + 2] == "0" ? true : false;
                        break;

                    case (int)EFEM_DI_State.Power:
                        DIState[i] = rtnstring[i + 2] == "1" ? true : false;
                        break;

                    default:
                        break;
                }
            }
            return true;
        }

        public enum EFEM_DI_State
        {
            Pressure,
            Vacuum,
            Ionizer,
            FFU1,
            FFU2,
            FFU3,
            RobotMode,
            RobotEnable,
            Door,
            EMO,
            Power,
        }

        public enum slot_status
        {
            Empty,
            Ready,
            Error,
            Thickness,
            Thin,
            ProcessingAligner1,
            ProcessingStage1,
            ProcessEnd,
            Unknow,
        }

        private struct Evevtlist
        {
            public struct P1
            {
                public static string Place = "FoupPlace";
                public static string Remove = "FoupRemove";
                public static string FoupPlacementON = "Foup PlacementON";
                public static string OperatorAccessButtomClick = "OperatorAccessButtomClick";
            }

            public struct P2
            {
                public static string Place = "FoupPlace";
                public static string Remove = "FoupRemove";
                public static string FoupPlacementON = "Foup PlacementON";
                public static string OperatorAccessButtomClick = "OperatorAccessButtomClick";
            }

            public struct EFEM
            {
                public static string Pressure = "Pressure";
                public static string Vacuum = "Vacuum";
                public static string Ionaizer = "Ionaizer";
                public static string FFU1 = "FFU1";
                public static string FFU2 = "FFU2";
                public static string FFU3 = "FFU3";
                public static string RobotMode = "RobotMode";
                public static string RobotEnable = "RobotEnable";
                public static string Door = "Door";
                public static string EMO = "EMO";
                public static string Power = "Power";
            }

            public struct E84
            {
                public static string CS_0On = "CS_0 ON";
                public static string CS_0Off = "CS_0 OFF";
                public static string L_REQ_On = "L_REQ ON";
                public static string L_REQ_Off = "L_REQ OFF";
                public static string Ready_On = "Ready ON";
                public static string Ready_Off = "Ready OFF";
                public static string U_REQ_On = "L_REQ ON";
                public static string U_REQ_Off = "L_REQ OFF";
                public static string BusyOn = "BUSY ON";
            }
        }

        public static bool IsInit
        {
            get
            {
                if (_initFlag == "Client true")
                {
                    return true;
                }
                else
                {
                    // Client false
                    return false;
                }
            }
        }

        public string ReceiveStr
        {
            get
            {
                return _ReceiveStr;
            }
        }

        public bool ReceiveFlag
        {
            get
            {
                return _receiveFlag;
            }
        }

        public static string SendStr
        {
            get
            {
                return _socket_cmdSend;
            }
        }

        private void getMessage()
        {
            string EventStr = "";
            string Rcvstr = "";
            //string Pnstr = "";
            string[] Str;
            string[] Str2;
            try
            {
                while (true)
                {
                    if (cSocket.getMessage.Length >= 5)
                    {
                        Str = Regex.Split(cSocket.getMessage.Substring(1, cSocket.getMessage.Length - 2), "\r", RegexOptions.Singleline);
                        Str2 = Regex.Split(Str[0], ",", RegexOptions.Singleline);
                        if (Str2[0] == "Event")
                        {
                            if (Str2[1] == "EFEM")
                            {
                                EventStr = Str2[2];
                                #region 判斷Event list
                                if (EventStr == EFEM.Evevtlist.EFEM.Pressure && Str2[3] == "0")
                                {
                                    Flag.EFEMAlarmReportFlag = false;
                                    Flag.AlarmFlag = true;
                                }
                                if (EventStr == EFEM.Evevtlist.EFEM.Vacuum && Str2[3] == "0")
                                {
                                    Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_StageVacuumError, true, out err);
                                    Flag.EFEMAlarmReportFlag = false;
                                    Flag.AlarmFlag = true;
                                }
                                if (EventStr == EFEM.Evevtlist.EFEM.EMO && Str2[3] == "0")
                                {
                                    Common.SecsgemForm.AlarmReportSend(TrimGap_EqpID.EQP_EMO, true, out err);
                                    InsertLog.SavetoDB(18);
                                    _socket_cmdSend = "";
                                    cSocket.getMessage = "";
                                    Flag.EFEMAlarmReportFlag = false;
                                    Flag.AlarmFlag = true;
                                }
                                if (EventStr == EFEM.Evevtlist.EFEM.Power && Str2[3] == "0")
                                {
                                    Flag.EFEMAlarmReportFlag = false;
                                    _socket_cmdSend = "";
                                    cSocket.getMessage = "";
                                    Flag.AlarmFlag = true;
                                }
                                if (EventStr == EFEM.Evevtlist.EFEM.RobotMode && Str2[3] == "0")
                                {
                                    Flag.EFEMAlarmReportFlag = false;
                                    Flag.AlarmFlag = true;
                                }
                                #endregion 判斷Event list
                            }
                            else if (Str2[1] == "P1" || Str2[1] == "P2")
                            {
                                if (Str2[1] == "P1")
                                {
                                    if (Str2[2] == EFEM.Evevtlist.P1.Place)
                                    {
                                        Common.EFEM.LoadPort1.Placement = true;
                                    }
                                    else if (Str2[2] == EFEM.Evevtlist.P1.Remove)
                                    {
                                        Common.EFEM.LoadPort1.Placement = false;
                                    }
                                }

                                if (Str2[1] == "P2")
                                {
                                    if (Str2[2] == EFEM.Evevtlist.P2.Place)
                                    {
                                        Common.EFEM.LoadPort2.Placement = true;
                                    }
                                    else if (Str2[2] == EFEM.Evevtlist.P2.Remove)
                                    {
                                        Common.EFEM.LoadPort2.Placement = false;
                                    }
                                }
                            }
                            else if (Str2[1] == "E841" || Str2[1] == "E842")
                            {
                                if (Str2[1] == "P1" && Str2[3] == EFEM.Evevtlist.E84.L_REQ_Off)
                                {
                                    //Common.EFEM.LoadPort1.Busy = false;
                                }
                                if (Str2[1] == "P2" && Str2[3] == EFEM.Evevtlist.E84.L_REQ_Off)
                                {
                                    //Common.EFEM.LoadPort2.Busy = false;
                                }
                                if (Str2[1] == "P1" && Str2[3] == EFEM.Evevtlist.E84.U_REQ_Off)
                                {
                                    //Common.EFEM.LoadPort1.Busy = true;
                                }
                                if (Str2[1] == "P2" && Str2[3] == EFEM.Evevtlist.E84.U_REQ_Off)
                                {
                                    //Common.EFEM.LoadPort2.Busy = true;
                                }
                            }
                            cSocket.getMessage = "";
                        }
                        else if (_socket_cmdSend != "" && cSocket.getMessage != "" && EventStr != "Event")
                        {
                            Rcvstr = cSocket.getMessage.Substring(1, cSocket.getMessage.Length - 2);
                            _ReceiveStr = Rcvstr;
                            Console.WriteLine("Receive_Org : " + cSocket.getMessage);
                            Console.WriteLine("Receive : " + Rcvstr);
                            _socket_cmdSend = "";
                            cSocket.getMessage = "";
                            Rcvstr = "";
                            //_ReceiveStr = cSocket.getMessage;

                            _receiveFlag = true;
                            //}
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
        }

        public string EFEMCommand_S(string str)
        {
            if (_socket_cmdSend != "")
            {
                SpinWait.SpinUntil(() => _receiveFlag, 60000);
                if (!_receiveFlag)
                {
                    return "";
                }
            }
            string StrCmd = str;
            _socket_cmdSend = StrCmd;
            try
            {
                _receiveFlag = false;
                cSocket.sendMessageSE(StrCmd);
                Console.WriteLine("SendCmd：" + StrCmd);
                SpinWait.SpinUntil(() => _receiveFlag, 3000);
                Console.WriteLine("Rcv：" + _ReceiveStr);
                return ReceiveStr;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                StrCmd = str + "," + e.Message;
                return StrCmd;
            }
        }

        public string EFEMCommand_S(string str, int Timeout_ms)
        {
            if (_socket_cmdSend != "")
            {
                SpinWait.SpinUntil(() => _receiveFlag, 60000);
            }
            string StrCmd = str;
            _socket_cmdSend = StrCmd;
            try
            {
                _receiveFlag = false;
                cSocket.sendMessageSE(StrCmd);
                Console.WriteLine(DateTime.Now.ToString() + " SendCmd：" + StrCmd);
                SpinWait.SpinUntil(() => _receiveFlag, Timeout_ms);
                Console.WriteLine(DateTime.Now.ToString() + " Rcv：" + _ReceiveStr);
                return ReceiveStr;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                StrCmd = str + "," + e.Message;
                return StrCmd;
            }
        }

        public static string EFEMCommand_Send(string str)
        {
            if (_socket_cmdSend != "")
            {
                SpinWait.SpinUntil(() => _receiveFlag, 60000);
            }
            else
            {
                _ReceiveStr = "";
            }
            string StrCmd = str;
            _socket_cmdSend = StrCmd;
            try
            {
                _receiveFlag = false;
                cSocket.sendMessageSE(StrCmd);
                Console.WriteLine(DateTime.Now.ToString() + " SendCmd：" + StrCmd);
                SpinWait.SpinUntil(() => _receiveFlag, 3000);
                Console.WriteLine(DateTime.Now.ToString() + " Rcv：" + _ReceiveStr);
                return _ReceiveStr;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                StrCmd = str + "," + e.Message;
                return StrCmd;
            }
        }

        public static string EFEMCommand_Send(string str, int Timeout_ms)
        {
            if (_socket_cmdSend != "")
            {
                SpinWait.SpinUntil(() => _receiveFlag, 60000);
            }
            string StrCmd = str;
            _socket_cmdSend = StrCmd;
            try
            {
                _receiveFlag = false;
                cSocket.sendMessageSE(StrCmd);
                Console.WriteLine("TimeOut:" + Timeout_ms);
                Console.WriteLine(DateTime.Now.ToString() + "SendCmd：" + StrCmd);
                SpinWait.SpinUntil(() => _receiveFlag, Timeout_ms);
                Console.WriteLine(DateTime.Now.ToString() + "Rcv：" + _ReceiveStr);
                Console.WriteLine("");
                return _ReceiveStr;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                StrCmd = str + "," + e.Message;
                return StrCmd;
            }
        }
    }

    public class API
    {
        private static string rtn = "";
        private static string[] rtnstring;

        private static string[] rtnSplit(string str)
        {
            string[] Str = Regex.Split(str, "\r", RegexOptions.Singleline);
            string[] Str2 = Regex.Split(Str[0], ",", RegexOptions.Singleline);

            return Str2;
        }

        public string Version()
        {
            rtn = EFEM.EFEMCommand_Send("Version,API");
            rtnstring = rtnSplit(rtn);
            return rtnstring[2];
        }

        public bool Remote()
        {
            rtn = EFEM.EFEMCommand_Send("Remote,API");
            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length < 2)
            {
                return false;
            }
            else
            {
                return rtnstring[2] == "0" ? true : false;
            }
        }

        public bool Local()
        {
            rtn = EFEM.EFEMCommand_Send("Local,API");
            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length < 2)
            {
                return false;
            }
            else
            {
                return rtnstring[2] == "0" ? true : false;
            }
        }

        public string CurrentMode()
        {
            rtn = EFEM.EFEMCommand_Send("CurrentMode,API");
            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length >= 2)
            {
                return rtnstring[2];
            }
            else
            {
                return "";
            }
        }
    }

    public class DeviceCommon
    {
        private static string rtn = "";
        private static string[] rtnstring;

        private static string[] rtnSplit(string str)
        {
            string[] Str = Regex.Split(str, "\r", RegexOptions.Singleline);
            string[] Str2 = Regex.Split(Str[0], ",", RegexOptions.Singleline);

            return Str2;
        }

        public static bool Home(Pn pn)
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("Home," + str);
            rtnstring = rtnSplit(rtn);
            return rtnstring[2] == "0" ? true : false;
        }

        public static bool ResetError(Pn pn)
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("ResetError," + str);
            rtnstring = rtnSplit(rtn);
            return rtnstring[2] == "0" ? true : false;
        }

        public static string GetStatus(Status status)
        {
            string str = status.ToString();
            rtn = EFEM.EFEMCommand_Send("GetStatus," + str);
            return rtn;
        }

        public enum Pn
        {
            ALL,
            P1,
            P2,
            P3,
            P4,
            Robot,
            Aligner1,
        }

        public enum Status
        {
            EFEM,
            P1,
            P2,
            P3,
            P4,
            Robot,
            Aligner1,
        }
    }

    public class LoadPort
    {
        private string rtn = "";
        private string[] rtnstring;
        public int[] Slot = new int[25];
        public Pn pn;
        public bool[] LEDSts = new bool[7];
        private bool LED_persence = false;        //0
        private bool LED_placement = false;       //1
        private bool LED_load = false;            //2
        private bool LED_unload = false;          //3
        private bool LED_operatorAccess1 = false; //4
        private bool LED_status1 = false;         //5
        private bool LED_status2 = false;         //6
        private bool LED_operatorAccess2 = false; //7
        private string foupID = "";
        private string errorDescription;
        private bool readytoload_foup = false;
        private bool readytoUnload_foup = false;
        private bool placement = false;             // 判斷有沒有foup
        private bool busy = false;                   // 判斷有沒有在預設false
        private bool readrtoloadWafer = true;
        public EFEM.slot_status[] slot_Status = new EFEM.slot_status[25];
        public PictureBox[] pb = new PictureBox[25];
        #region get

        public int AutoGetSlot()
        {
            int slot = 0;
            for (int i = 0; i < slot_Status.Length; i++)
            {
                if (slot_Status[i] == EFEM.slot_status.Ready)
                {
                    slot = i + 1;
                    break;
                }
            }
            Console.WriteLine("Get Slot：" + slot);

            readrtoloadWafer = slot > 0 ? true : false;
            return slot;
        }

        public void Update_Sts()
        {
            for (int i = 0; i < slot_Status.Length; i++)
            {
                switch (slot_Status[i])
                {
                    case EFEM.slot_status.Empty:
                        pb[i].BackColor = EFEM.slot_status_Color.Empty;
                        break;

                    case EFEM.slot_status.Ready:
                        pb[i].BackColor = EFEM.slot_status_Color.Ready;
                        break;

                    case EFEM.slot_status.Error:
                        pb[i].BackColor = EFEM.slot_status_Color.Error;
                        break;

                    case EFEM.slot_status.Thickness:
                        pb[i].BackColor = EFEM.slot_status_Color.Thickness;
                        break;

                    case EFEM.slot_status.Thin:
                        pb[i].BackColor = EFEM.slot_status_Color.Thin;
                        break;

                    case EFEM.slot_status.ProcessingAligner1:
                        pb[i].BackColor = EFEM.slot_status_Color.ProcessingAligner1;
                        break;

                    case EFEM.slot_status.ProcessingStage1:
                        pb[i].BackColor = EFEM.slot_status_Color.ProcessingStage1;
                        break;

                    case EFEM.slot_status.ProcessEnd:
                        pb[i].BackColor = EFEM.slot_status_Color.ProcessEnd;
                        break;

                    case EFEM.slot_status.Unknow:
                        pb[i].BackColor = EFEM.slot_status_Color.Unknow;
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Slot 是從1開始
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="Slot_Status"></param>
        public void Update_slot_Status(int slot, EFEM.slot_status Slot_Status)
        {
            if (slot > 0 && slot <= 25)
            {
                slot_Status[slot - 1] = Slot_Status;
            }
            // Thread Update_thread = new Thread(Update_Sts);
            // Update_thread.IsBackground = true;
            // Update_thread.Start();
            //Update_Sts();
        }

        public bool ReadryToLoadWafer
        {
            get { return readrtoloadWafer; }
            set { readrtoloadWafer = value; }
        }

        public bool Placement
        {
            get { return placement; }
            set { placement = value; }
        }

        public bool Busy
        {
            get { return busy; }
            set { busy = value; }
        }

        public bool ReadyToLoad
        {
            get { return readytoload_foup; }
            set { readytoload_foup = value; }
        }

        public bool ReadyToUnLoad
        {
            get { return readytoUnload_foup; }
            set { readytoUnload_foup = value; }
        }

        public string ErrorDescription
        {
            get { return errorDescription; }
        }

        public string FoupID
        {
            get { return foupID; }
            set { foupID = value; }
        }

        public bool LED_Persence
        {
            get
            {
                return LED_persence;
            }
        }

        public bool LED_Placement
        {
            get
            {
                return LED_placement;
            }
        }

        public bool LED_Load
        {
            get
            {
                return LED_load;
            }
        }

        public bool LED_Unload
        {
            get
            {
                return LED_unload;
            }
        }

        public bool LED_OperatorAccess1
        {
            get
            {
                return LED_operatorAccess1;
            }
        }

        public bool LED_Status1
        {
            get
            {
                return LED_status1;
            }
        }

        public bool LED_Status2
        {
            get
            {
                return LED_status2;
            }
        }

        public bool LED_OperatorAccess2
        {
            get
            {
                return LED_operatorAccess2;
            }
        }

        #endregion get

        public LoadPort(Pn _pn)
        {
            for (int i = 0; i < slot_Status.Length; i++)
            {
                slot_Status[i] = EFEM.slot_status.Empty;
            }
            switch (_pn)
            {
                case Pn.P1:
                    pn = Pn.P1;
                    break;

                case Pn.P2:
                    pn = Pn.P2;
                    break;

                case Pn.P3:
                    pn = Pn.P3;
                    break;

                case Pn.P4:
                    pn = Pn.P4;
                    break;

                default:
                    pn = Pn.P1;
                    break;
            }
        }

        #region 指令

        private static string[] rtnSplit(string str)
        {
            string[] Str = Regex.Split(str, "\r", RegexOptions.Singleline);
            string[] Str2 = Regex.Split(Str[0], ",", RegexOptions.Singleline);

            return Str2;
        }

        public bool GetStatus()
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("GetStatus," + str, Timeout_ms.GetStatus);
            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length >= 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Home()
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("Home," + str, Timeout_ms.Home);
            rtnstring = rtnSplit(rtn);
            return rtnstring[2] == "0" ? true : false;
        }

        public bool ResetError()
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("ResetError," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length >= 2)
            {
                return rtnstring[2] == "0" ? true : false;
            }
            else
            {
                return false;
            }
        }

        public bool ResetComPort()
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("ResetComPort," + str);
            rtnstring = rtnSplit(rtn);
            return rtnstring[2] == "0" ? true : false;
        }

        public bool ReadFoupID()
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("ReadFoupID," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[2] == "0")
            {
                if (rtnstring[3].Length >= 9)
                {
                    foupID = rtnstring[3].Substring(0, 9);
                }
                else
                {
                    foupID = rtnstring[3];
                }

                return true;
            }
            else if (rtnstring[2] == "1")
            {
                errorDescription = rtnstring[3];
                foupID = "";
                return false;
            }
            else
            {
                foupID = "";
                return false;
            }
        }

        public bool Clamp()
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("Clamp," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[2] == "0")
            {
                return true;
            }
            else
            {
                errorDescription = rtnstring[2];
                return false;
            }
        }

        public bool UnClamp()
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("UnClamp," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[2] == "0")
            {
                return true;
            }
            else
            {
                errorDescription = rtnstring[2];
                return false;
            }
        }

        public bool Load()
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("Load," + str, Timeout_ms.Load);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[2] == "0")
            {
                return true;
            }
            else
            {
                errorDescription = rtnstring[2];
                return false;
            }
        }

        public bool Unload()
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("Unload," + str, Timeout_ms.Unload);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[2] == "0")
            {
                return true;
            }
            else
            {
                errorDescription = rtnstring[2];
                return false;
            }
        }

        public bool Map()
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("Map," + str, Timeout_ms.Map);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[2] == "0")
            {
                return true;
            }
            else
            {
                errorDescription = rtnstring[2];
                return false;
            }
        }

        /// <summary>
        /// Top To Below
        /// </summary>
        /// <param name="pn"></param>
        public bool GetWaferSlot()
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("GetWaferSlot," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[2] == "OK")
            {
                for (int i = 0; i < 25; i++)
                {
                    Slot[i] = Convert.ToInt32(rtnstring[27 - i]);
                    switch (Slot[i])
                    {
                        case 0:
                            slot_Status[i] = EFEM.slot_status.Empty;
                            break;

                        case 1:
                            slot_Status[i] = EFEM.slot_status.Ready;
                            break;

                        case 2:
                            slot_Status[i] = EFEM.slot_status.Error;
                            break;

                        case 9:
                            slot_Status[i] = EFEM.slot_status.Unknow;
                            break;

                        default:
                            slot_Status[i] = EFEM.slot_status.Unknow;
                            break;
                    }
                }
                return true;
            }
            else
            {
                errorDescription = rtnstring[2];
                return false;
            }
        }

        /// <summary>
        /// Below To Top
        /// </summary>
        /// <param name="pn"></param>
        public bool GetWaferSlot2()
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("GetWaferSlot2," + str);
            rtnstring = rtnSplit(rtn);
            //bool[] Slot = new bool[25];
            if (rtnstring[2] == "OK")
            {
                for (int i = 0; i < 25; i++)
                {
                    Slot[i] = Convert.ToInt32(rtnstring[i + 3]);
                    switch (Slot[i])
                    {
                        case 0:
                            slot_Status[i] = EFEM.slot_status.Empty;
                            break;

                        case 1:
                            slot_Status[i] = EFEM.slot_status.Ready;
                            break;

                        case 2:
                            slot_Status[i] = EFEM.slot_status.Error;
                            break;

                        case 3:
                            slot_Status[i] = EFEM.slot_status.Thickness;
                            break;

                        case 4:
                            slot_Status[i] = EFEM.slot_status.Thin;
                            break;

                        case 9:
                            slot_Status[i] = EFEM.slot_status.Unknow;
                            break;

                        default:
                            slot_Status[i] = EFEM.slot_status.Unknow;
                            break;
                    }
                }
                //Thread Update_thread = new Thread(Update_Sts);

                //Update_thread.IsBackground = true;
                //Update_thread.Start();

                //Update_Sts();
                //slot = Slot;
                return true;
            }
            else
            {
                // 如果掃失敗，全部給unknow
                for (int i = 0; i < 25; i++)
                {
                    Slot[i] = 9;
                    slot_Status[i] = EFEM.slot_status.Unknow;
                }
                errorDescription = rtnstring[2];
                //slot = Slot;
                return false;
            }
        }

        /// <summary>
        /// Below To Top
        /// </summary>
        /// <param name="pn"></param>
        public bool GetWaferSlot2(ref int[] slot)
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("GetWaferSlot2," + str);
            rtnstring = rtnSplit(rtn);
            //bool[] Slot = new bool[25];
            if (rtnstring[2] == "OK")
            {
                for (int i = 0; i < 25; i++)
                {
                    Slot[i] = Convert.ToInt32(rtnstring[i + 3]);
                    switch (Slot[i])
                    {
                        case 0:
                            slot_Status[i] = EFEM.slot_status.Empty;
                            break;

                        case 1:
                            slot_Status[i] = EFEM.slot_status.Ready;
                            break;

                        case 2:
                            slot_Status[i] = EFEM.slot_status.Error;
                            break;

                        case 9:
                            slot_Status[i] = EFEM.slot_status.Unknow;
                            break;

                        default:
                            slot_Status[i] = EFEM.slot_status.Unknow;
                            break;
                    }
                }
                return true;
            }
            else
            {
                errorDescription = rtnstring[2];
                return false;
            }
        }

        /// <summary>
        /// Top To Below
        /// </summary>
        /// <param name="pn"></param>
        public bool GetWaferThickness(ref double[] waferThickness)
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("GetWaferThickness," + str);
            double[] WaferThickness = new double[25];
            if (rtnstring[2] == "OK")
            {
                for (int i = 0; i < 25; i++)
                {
                    WaferThickness[i] = Convert.ToDouble(rtnstring[27 - i]);
                }
                waferThickness = WaferThickness;
                return true;
            }
            else
            {
                errorDescription = rtnstring[2];
                waferThickness = WaferThickness;
                return false;
            }
        }

        public void Dock()
        { }

        public void DoorOpen()
        { }

        public void DoorClose()
        { }

        public bool LEDLoad(LEDsts ledsts)
        {
            string str = pn.ToString() + "," + ledsts.ToString();
            rtn = EFEM.EFEMCommand_Send("LEDLoad," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length >= 3 && rtnstring[3] == "0")
            {
                return true;
            }
            else
            {
                errorDescription = "EFEM LEDLoad Error"; //20230901
                return false;
            }
        }

        public bool LEDUnLoad(LEDsts ledsts)
        {
            string str = pn.ToString() + "," + ledsts.ToString();
            rtn = EFEM.EFEMCommand_Send("LEDUnLoad," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[3] == "0")
            {
                return true;
            }
            else
            {
                errorDescription = "EFEM LEDUnLoad Error"; //20230901
                return false;
            }
        }

        public bool LEDStatus1(LEDsts ledsts)
        {
            string str = pn.ToString() + "," + ledsts.ToString();
            rtn = EFEM.EFEMCommand_Send("LEDStatus1," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[3] == "0")
            {
                return true;
            }
            else
            {
                errorDescription = "EFEM LEDStatus1 Error"; //20230901
                return false;
            }
        }

        public bool LEDStatus2(LEDsts ledsts)
        {
            string str = pn.ToString() + "," + ledsts.ToString();
            rtn = EFEM.EFEMCommand_Send("LEDStatus2," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[3] == "0")
            {
                return true;
            }
            else
            {
                errorDescription = "EFEM LEDStatus2 Error"; //20230901
                return false;
            }
        }

        public bool GetLEDStatus()
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("GetLEDStatus," + str);
            rtnstring = rtnSplit(rtn);
            bool[] lEDSts = new bool[7];
            if (rtnstring[0] != "GetLEDStatus")
            {
                errorDescription = "Send GetLEDStatus, Recive " + rtnstring[0];
                return false;
            }
            if (rtnstring[2] != "1")
            {
                for (int i = 0; i < LEDSts.Length; i++)
                {
                    lEDSts[i] = rtnstring[2].Substring(i, 1) == "0" ? false : true;
                    switch (i)
                    {
                        case (int)LEDName.Persence:
                            LED_persence = lEDSts[i];
                            break;

                        case (int)LEDName.Placement:
                            LED_placement = lEDSts[i];
                            break;

                        case (int)LEDName.Load:
                            LED_load = lEDSts[i];
                            break;

                        case (int)LEDName.Unload:
                            LED_unload = lEDSts[i];
                            break;

                        case (int)LEDName.OperatorAccess1:
                            LED_operatorAccess1 = lEDSts[i];
                            break;

                        case (int)LEDName.Status1:
                            LED_status1 = lEDSts[i];
                            break;

                        case (int)LEDName.Status2:
                            LED_status2 = lEDSts[i];
                            break;

                        case (int)LEDName.OperatorAccess2:
                            LED_operatorAccess2 = lEDSts[i];
                            break;
                    }
                }
                LEDSts = lEDSts;
                return true;
            }
            else
            {
                errorDescription = "EFEM GetLEDStatus Error"; //20230901 rtnstring[3];
                return false;
            }
        }

        public bool SetOperatorAccessButton(LEDsts ledsts)
        {
            string str = pn.ToString() + "," + ledsts.ToString();
            rtn = EFEM.EFEMCommand_Send("SetOperatorAccessButton," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[3] == "0")
            {
                return true;
            }
            else
            {
                errorDescription = "EFEM SetOperatorAccessButton Error"; //20230901 rtnstring[3];
                return false;
            }
        }

        public bool GetZAxisPos(ref string zPos)
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("GetZAxisPos," + str);
            string ZPos = "";
            rtnstring = rtnSplit(rtn);
            if (rtnstring[2] != "1")
            {
                ZPos = rtnstring[2];
                zPos = ZPos;
                return true;
            }
            else
            {
                zPos = ZPos;
                errorDescription = "EFEM GetZAxisPos Error"; //20230901 rtnstring[3];
                return false;
            }
        }

        public enum Pn
        {
            P1,
            P2,
            P3,
            P4,
        }

        public enum LEDsts
        {
            On,
            Off,
            Flash,
        }

        public enum LEDName
        {
            Persence,
            Placement,
            Load,
            Unload,
            OperatorAccess1,
            Status1,
            Status2,
            OperatorAccess2,
        }

        public enum loadport_status
        {
            Ready,
            Load,
            Unload,
        }

        private class Timeout_ms
        {
            public const int Home = 40000;
            public const int GetStatus = 1000;
            public const int Load = 40000;
            public const int Unload = 40000;
            public const int Map = 20000;
        }

        #endregion 指令
    }

    public class Robot
    {
        private bool waferPresence_Arm_lower = false;
        private bool waferPresence_Arm_upper = false;
        private int slot_Arm_lower = 0;
        private int slot_Arm_upper = 0;
        private string statusCode;
        private string rtn = "";
        private string[] rtnstring;
        private string Waferinfo_Arm_lower_Ready = "Lower_Arm Ready";
        private string Waferinfo_Arm_lower = "Lower_Arm Ready";
        private string Waferinfo_Arm_upper_Ready = "Upper_Arm Ready";
        private string Waferinfo_Arm_upper = "Upper_Arm Ready";
        private string errorDescription = "";
        public LoadPort Slot_pn;
        public EFEM.slot_status[] slot_Status = new EFEM.slot_status[2];
        public PictureBox[] pb = new PictureBox[2];
        public Label[] lbwaferinfo = new Label[2];
        #region get/set

        public void Update_Sts()
        {
            lbwaferinfo[0].Text = WaferInfo_Lower;
            lbwaferinfo[1].Text = WaferInfo_Upper;

            for (int i = 0; i < slot_Status.Length; i++)
            {
                switch (slot_Status[i])
                {
                    case EFEM.slot_status.Empty:
                        pb[i].BackColor = EFEM.slot_status_Color.Empty;
                        break;

                    case EFEM.slot_status.Ready:
                        pb[i].BackColor = EFEM.slot_status_Color.Ready;
                        break;

                    case EFEM.slot_status.Error:
                        pb[i].BackColor = EFEM.slot_status_Color.Error;
                        break;

                    case EFEM.slot_status.ProcessingAligner1:
                        pb[i].BackColor = EFEM.slot_status_Color.ProcessingAligner1;
                        break;

                    case EFEM.slot_status.ProcessingStage1:
                        pb[i].BackColor = EFEM.slot_status_Color.ProcessingStage1;
                        break;

                    case EFEM.slot_status.ProcessEnd:
                        pb[i].BackColor = EFEM.slot_status_Color.ProcessEnd;
                        break;

                    case EFEM.slot_status.Unknow:
                        pb[i].BackColor = EFEM.slot_status_Color.Unknow;
                        break;

                    default:
                        break;
                }
            }
        }

        public void Update_slot_Status(ArmID armID, EFEM.slot_status Slot_Status)
        {
            switch (armID)
            {
                case ArmID.LowerArm:
                    slot_Status[0] = Slot_Status;

                    break;

                case ArmID.UpperArm:
                    slot_Status[1] = Slot_Status;
                    break;

                default:
                    break;
            }
            //Thread Update_thread = new Thread(Update_Sts);

            //Update_thread.IsBackground = true;
            //Update_thread.Start();

            //Update_Sts();
        }

        public string ErrorDescription
        {
            get
            {
                return errorDescription;
            }
        }

        public int Slot_Arm_lower
        {
            get { return slot_Arm_lower; }
            set { slot_Arm_lower = value; }
        }

        public int Slot_Arm_upper
        {
            get { return slot_Arm_upper; }
            set { slot_Arm_upper = value; }
        }

        public bool WaferPresence_Lower
        {
            get { return waferPresence_Arm_lower; }
        }

        public bool WaferPresence_Upper
        {
            get
            { return waferPresence_Arm_upper; }
        }

        public string StatusCode
        {
            get { return statusCode; }
        }

        public string WaferInfo_Lower
        {
            get
            {
                if (Slot_pn != null && slot_Arm_lower != 0)
                {
                    Waferinfo_Arm_lower = Slot_pn.pn.ToString() + "," + slot_Arm_lower;
                    return Waferinfo_Arm_lower;
                }
                else
                {
                    return Waferinfo_Arm_lower = Waferinfo_Arm_lower_Ready;
                }
            }
        }

        public string WaferInfo_Upper
        {
            get
            {
                if (Slot_pn != null && slot_Arm_upper != 0)
                {
                    Waferinfo_Arm_upper = Slot_pn.pn.ToString() + "," + slot_Arm_upper;
                    return Waferinfo_Arm_upper;
                }
                else
                {
                    return Waferinfo_Arm_upper = Waferinfo_Arm_upper_Ready;
                }
            }
        }

        #endregion get/set

        private static string[] rtnSplit(string str)
        {
            string[] Str = Regex.Split(str, "\r", RegexOptions.Singleline);
            string[] Str2 = Regex.Split(Str[0], ",", RegexOptions.Singleline);

            return Str2;
        }

        public bool GetStatus()
        {
            string str = "Robot";
            rtn = EFEM.EFEMCommand_Send("GetStatus," + str, Timeout_ms.GetStatus);
            rtnstring = rtnSplit(rtn);
            //statusCode = rtnstring[2];  //20230912
            if (rtnstring.Length >= 4)
            {
                waferPresence_Arm_lower = rtnstring[3] == "1" ? true : false;
                waferPresence_Arm_upper = rtnstring[4] == "1" ? true : false;
                return true;
            }
            else
            {
                waferPresence_Arm_lower = false;
                waferPresence_Arm_upper = false;
                return false;
            }
        }

        public bool ResetError()
        {
            string str = "Robot";
            rtn = EFEM.EFEMCommand_Send("ResetError," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length >= 2)
            {
                return rtnstring[2] == "0" ? true : false;
            }
            else
            {
                return false;
            }
        }

        public bool Home()
        {
            string str = "Robot";
            rtn = EFEM.EFEMCommand_Send("Home," + str, Timeout_ms.Home);
            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length >= 2)
            {
                return rtnstring[2] == "0" ? true : false;
            }
            else
            {
                return false;
            }
        }

        public bool Stop()
        {
            rtn = EFEM.EFEMCommand_Send("Stop,Robot");
            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length >= 2)
            {
                return rtnstring[2] == "0" ? true : false;
            }
            else
            {
                return false;
            }
        }

        public bool ReStart()
        {
            rtn = EFEM.EFEMCommand_Send("ReStart,Robot");
            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length >= 2)
            {
                return rtnstring[2] == "0" ? true : false;
            }
            else
            {
                return false;
            }
        }

        public bool SetRobotSpeed(int RobotSpeed1, int RobotSpeed2)
        {
            if (RobotSpeed1 < 5)
            {
                RobotSpeed1 = 5;
            }
            else if (RobotSpeed1 > 100)
            {
                RobotSpeed1 = 100;
            }
            if (RobotSpeed2 < 5)
            {
                RobotSpeed2 = 5;
            }
            else if (RobotSpeed2 > 100)
            {
                RobotSpeed2 = 100;
            }
            rtn = EFEM.EFEMCommand_Send("SetRobotSpeed,Robot," + RobotSpeed1.ToString() + "," + RobotSpeed2.ToString());
            rtnstring = rtnSplit(rtn);
            return rtnstring[2] == "0" ? true : false;
        }

        public string ReadPosition()
        {
            rtn = EFEM.EFEMCommand_Send("ReadPosition,Robot");
            return rtn;
        }

        public bool WaferGet(ArmID ArmID, Pn pn, int Slot, LoadPort loadPort, EFEM.slot_status slot_Status)
        {
            if (Slot > 0 && Slot <= 25)
            {
            }
            else
            {
                return false;
            }
            string str = "";
            switch (pn)
            {
                case Pn.P1:
                    str = (int)ArmID + "," + pn.ToString() + "," + Slot.ToString();
                    break;

                case Pn.P2:
                    str = (int)ArmID + "," + pn.ToString() + "," + Slot.ToString();
                    break;

                case Pn.P3:
                    str = (int)ArmID + "," + pn.ToString() + "," + Slot.ToString();
                    break;

                case Pn.P4:
                    str = (int)ArmID + "," + pn.ToString() + "," + Slot.ToString();
                    break;

                case Pn.Aligner1:
                    str = (int)ArmID + "," + pn.ToString() + "," + 1;
                    break;

                case Pn.Stage1:
                    str = (int)ArmID + "," + pn.ToString() + "," + 1;
                    break;
            }

            rtn = EFEM.EFEMCommand_Send("WaferGet,Robot," + str, Timeout_ms.WaferGet);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[2] == "0")
            {
                loadPort.Update_slot_Status(Slot, slot_Status);
                Slot_pn = loadPort;
                switch (ArmID)
                {
                    case ArmID.LowerArm:
                        //Waferinfo_Arm_lower = loadPort.pn.ToString() + "," + Slot.ToString();

                        slot_Arm_lower = Slot;
                        break;

                    case ArmID.UpperArm:
                        //Waferinfo_Arm_upper = loadPort.pn.ToString() + "," + Slot.ToString();
                        slot_Arm_upper = Slot;
                        break;

                    default:
                        break;
                }

                switch (pn)
                {
                    case Pn.P1:
                        Update_slot_Status(ArmID, EFEM.slot_status.ProcessingAligner1);

                        break;

                    case Pn.P2:
                        Update_slot_Status(ArmID, EFEM.slot_status.ProcessingAligner1);
                        break;

                    case Pn.P3:
                        Update_slot_Status(ArmID, EFEM.slot_status.ProcessingAligner1);
                        break;

                    case Pn.P4:
                        Update_slot_Status(ArmID, EFEM.slot_status.ProcessingAligner1);
                        break;

                    case Pn.Aligner1:
                        Update_slot_Status(ArmID, EFEM.slot_status.ProcessingStage1);
                        Common.EFEM.Aligner.Slot = 0;
                        Common.EFEM.Aligner.Slot_pn = loadPort;
                        break;

                    case Pn.Stage1:
                        Update_slot_Status(ArmID, EFEM.slot_status.ProcessEnd);
                        Common.EFEM.Stage1.Slot = 0;
                        Common.EFEM.Stage1.WaferPresence = false;
                        Common.EFEM.Stage1.Slot_pn = loadPort;
                        break;

                    default:
                        break;
                }
                if (slot_Status == EFEM.slot_status.Unknow)
                {
                    Update_slot_Status(ArmID, EFEM.slot_status.Unknow);
                }
                errorDescription = "";
                return true;
            }
            else
            {
                if (rtnstring.Length >= 5)
                {
                    errorDescription = rtnstring[3] + "," + rtnstring[4];
                }

                switch (ArmID)
                {
                    case ArmID.LowerArm:
                        Waferinfo_Arm_lower = "Lower_Arm Get Error";
                        break;

                    case ArmID.UpperArm:
                        Waferinfo_Arm_upper = "Upper_Arm Get Error";
                        break;

                    default:
                        break;
                }
                return false;
            }
        }

        public bool WaferPut(ArmID ArmID, Pn pn, int Slot, LoadPort loadPort, EFEM.slot_status slot_Status)
        {
            if (Slot > 0 && Slot <= 25)
            {
            }
            else
            {
                return false;
            }
            string str = "";
            switch (pn)
            {
                case Pn.P1:
                    str = (int)ArmID + "," + pn.ToString() + "," + Slot.ToString();
                    break;

                case Pn.P2:
                    str = (int)ArmID + "," + pn.ToString() + "," + Slot.ToString();
                    break;

                case Pn.P3:
                    str = (int)ArmID + "," + pn.ToString() + "," + Slot.ToString();
                    break;

                case Pn.P4:
                    str = (int)ArmID + "," + pn.ToString() + "," + Slot.ToString();
                    break;

                case Pn.Aligner1:
                    str = (int)ArmID + "," + pn.ToString() + "," + 1;

                    break;

                case Pn.Stage1:
                    str = (int)ArmID + "," + pn.ToString() + "," + 1;
                    break;
            }

            rtn = EFEM.EFEMCommand_Send("WaferPut,Robot," + str, Timeout_ms.WaferPut);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[2] == "0")
            {
                loadPort.Update_slot_Status(Slot, slot_Status);
                switch (ArmID)
                {
                    case ArmID.LowerArm:
                        if (pn == Pn.Aligner1)
                        {
                            Common.EFEM.Aligner.Slot = slot_Arm_lower;
                            Common.EFEM.Aligner.Slot_pn = loadPort;
                        }
                        else if (pn == Pn.Stage1)
                        {
                            Common.EFEM.Stage1.Slot = slot_Arm_lower;
                        }
                        Common.EFEM.Robot.slot_Arm_lower = 0;
                        //Waferinfo_Arm_lower = "Lower_Arm Ready";
                        break;

                    case ArmID.UpperArm:
                        if (pn == Pn.Aligner1)
                        {
                            Common.EFEM.Aligner.Slot = slot_Arm_upper;
                            Common.EFEM.Aligner.Slot_pn = loadPort;
                        }
                        else if (pn == Pn.Stage1)
                        {
                            Common.EFEM.Stage1.Slot = slot_Arm_upper;
                            Common.EFEM.Stage1.Slot_pn = loadPort;
                        }
                        Common.EFEM.Robot.slot_Arm_upper = 0;
                        //Waferinfo_Arm_upper = "Upper_Arm Ready";

                        break;

                    default:
                        break;
                }
                Update_slot_Status(ArmID, EFEM.slot_status.Empty);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TopWaferGet(ArmID ArmID, Pn pn)
        {
            string str = ArmID.ToString() + "," + pn.ToString();
            rtn = EFEM.EFEMCommand_Send("TopWaferGet,Robot," + str);
            return rtnstring[2] == "0" ? true : false;
        }

        public bool TopWaferPut(ArmID ArmID, Pn pn)
        {
            string str = ArmID.ToString() + "," + pn.ToString();
            rtn = EFEM.EFEMCommand_Send("TopWaferPut,Robot," + str);
            return rtnstring[2] == "0" ? true : false;
        }

        public bool GetStandby(ArmID ArmID, Pn pn)
        {
            string str = ArmID.ToString() + "," + pn.ToString();
            rtn = EFEM.EFEMCommand_Send("GetStandby,Robot," + str);
            return rtnstring[2] == "0" ? true : false;
        }

        public bool PutStandby(ArmID ArmID, Pn pn, int Slot)
        {
            string str = ArmID.ToString() + "," + pn.ToString() + "," + Slot.ToString();
            rtn = EFEM.EFEMCommand_Send("PutStandby,Robot," + str);
            return rtnstring[2] == "0" ? true : false;
        }

        public bool BernoulliOn(ArmID ArmID)
        {
            string str = ArmID.ToString();
            rtn = EFEM.EFEMCommand_Send("BernoulliOn,Robot," + str);
            return rtnstring[2] == "0" ? true : false;
        }

        public bool BernoulliOff(ArmID ArmID)
        {
            string str = ArmID.ToString();
            rtn = EFEM.EFEMCommand_Send("BernoulliOff,Robot," + str);
            return rtnstring[2] == "0" ? true : false;
        }

        public bool EdgeGripON(ArmID ArmID)
        {
            string str = ArmID.ToString();
            rtn = EFEM.EFEMCommand_Send("EdgeGripON,Robot," + str);
            return rtnstring[2] == "0" ? true : false;
        }

        public bool EdgeGripOFF(ArmID ArmID)
        {
            string str = ArmID.ToString();
            rtn = EFEM.EFEMCommand_Send("EdgeGripOFF,Robot," + str);
            return rtnstring[2] == "0" ? true : false;
        }

        public bool VacuumOn(ArmID ArmID)
        {
            string str = ((int)ArmID).ToString();

            rtn = EFEM.EFEMCommand_Send("VacuumOn,Robot," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length > 2)
            {
                return rtnstring[2] == "0" ? true : false;
            }
            else
            {
                if (rtnstring.Length >= 4)
                {
                    errorDescription = rtnstring[3];
                }
                return false;
            }
        }

        public bool VacuumOff(ArmID ArmID)
        {
            string str = ((int)ArmID).ToString();

            rtn = EFEM.EFEMCommand_Send("VacuumOff,Robot," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length > 2)
            {
                return rtnstring[2] == "0" ? true : false;
            }
            else
            {
                return false;
            }
        }

        public bool SetEEType(EEType eeType)
        {
            string str = ((int)eeType).ToString();
            rtn = EFEM.EFEMCommand_Send("SetEEType,Robot," + str);
            return rtnstring[2] == "0" ? true : false;
        }

        public string GetEEType()
        {
            rtn = EFEM.EFEMCommand_Send("GetEEType,Robot,");
            switch (rtnstring[2])
            {
                case "0":
                    return EEType.Bernoulli.ToString();

                case "1":
                    return EEType.EdgeGrip.ToString();

                case "2":
                    return EEType.Pad.ToString();

                case "3":
                    return EEType.EdgeGripAndBernoulli.ToString();

                default:
                    return rtnstring[2];
            }
        }

        public enum Pn
        {
            P1,
            P2,
            P3,
            P4,
            Aligner1,
            Stage1,
        }

        public enum ArmID
        {
            LowerArm = 1,
            UpperArm = 2,
        }

        public enum EEType
        {
            Bernoulli = 0,
            EdgeGrip = 1,
            Pad = 2,
            EdgeGripAndBernoulli = 3,
        }

        private class Timeout_ms
        {
            public const int Home = 20000;
            public const int GetStatus = 3000;
            public const int WaferGet = 20000;
            public const int WaferPut = 20000;
        }
    }

    public class Aligner
    {
        private bool waferPresence = false;
        private string mode = "";
        private bool vacuumStatus = false;
        private bool alignement_Done = false;
        private bool busy = false;
        private int slot = 0;
        public LoadPort Slot_pn;
        private string errorDescription = "";
        private static string rtn = "";
        private static string[] rtnstring;
        public Label lb;
        public PictureBox pb;
        public static string state_ready = "Aligner1 Ready";
        private string waferinfo = state_ready;

        public void Update_Sts()
        {
            if (lb != null)
            {
                if (Slot_pn != null && slot != 0)
                {
                    waferinfo = Slot_pn.pn.ToString() + "," + slot;
                    lb.Text = WaferInfo;
                }
                else
                {
                    waferinfo = state_ready;
                    lb.Text = WaferInfo;
                }
            }
            if (pb != null)
            {
                if (Slot_pn != null && slot != 0)
                {
                    pb.BackColor = EFEM.slot_status_Color.ProcessingAligner1;
                }
                else
                {
                    pb.BackColor = EFEM.slot_status_Color.AlignerStageReady;
                }
            }
        }

        public bool Alignement_Done
        {
            get { return alignement_Done; }
            set { alignement_Done = value; }
        }

        public int Slot
        {
            get { return slot; }
            set { slot = value; }
        }

        public bool Busy
        {
            get { return busy; }
            set { busy = value; }
        }

        public bool WaferPresence
        {
            get { return waferPresence; }
            set { }
        }

        public bool VacuumStatus
        {
            get { return vacuumStatus; }
        }

        public string Mode
        {
            get { return mode; }
        }

        public string ErrorDescription
        {
            get { return errorDescription; }
        }

        public string WaferInfo
        {
            get { return waferinfo; }
            set { waferinfo = value; }
        }

        private static string[] rtnSplit(string str)
        {
            string[] Str = Regex.Split(str, "\r", RegexOptions.Singleline);
            string[] Str2 = Regex.Split(Str[0], ",", RegexOptions.Singleline);

            return Str2;
        }

        public bool Home()
        {
            string str = "Aligner1";
            rtn = EFEM.EFEMCommand_Send("Home," + str, Timeout_ms.Home);
            rtnstring = rtnSplit(rtn);
            return rtnstring[2] == "0" ? true : false;
        }

        public bool ResetError()
        {
            string str = "Aligner1";
            rtn = EFEM.EFEMCommand_Send("ResetError," + str);
            rtnstring = rtnSplit(rtn);
            return rtnstring[2] == "0" ? true : false;
        }

        public bool GetStatus()
        {
            rtn = EFEM.EFEMCommand_Send("GetStatus,Aligner1", Timeout_ms.GetStatus);
            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length >= 5)
            {
                mode = rtnstring[2];
                waferPresence = rtnstring[3] == "True" ? true : false;
                vacuumStatus = rtnstring[4] == "True" ? true : false;
                busy = rtnstring[5] == "True" ? true : false;
                errorDescription = "";
                if (mode != "Online")
                {
                    errorDescription = "Aligner1:Matain Mode";
                    return false;
                }
                return true;
            }
            else
            {
                mode = "";
                waferPresence = true;
                vacuumStatus = true;
                busy = true;
                return false;
            }
        }

        public bool Alignment()
        {
            int Degree = fram.m_WaferAlignAngle;
            string str = Degree.ToString();
            rtn = EFEM.EFEMCommand_Send("Alignment,Aligner1," + str, Timeout_ms.Alignment);
            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length >= 3)
            {
                if (rtnstring[2] == "0")
                {
                    alignement_Done = true;
                    return true;
                }
                else
                {
                    if (rtnstring.Length >= 5)
                    {
                        alignement_Done = false;
                        errorDescription = rtnstring[3] + "," + rtnstring[4];
                    }
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void FindNotch()
        { }

        public bool ToAngle(int Degree)
        {
            string str = Degree.ToString();
            rtn = EFEM.EFEMCommand_Send("ToAngle,Aligner1," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length >= 3)
            {
                return rtnstring[3] == "0" ? true : false;
            }
            else
            {
                return false;
            }
        }

        public bool GetAlignerDegree()
        {
            rtn = EFEM.EFEMCommand_Send("GetAlignerDegree,Aligner1");
            rtnstring = rtnSplit(rtn);
            return rtnstring[2] == "0" ? true : false;
        }

        public bool SetAlignerDegree(int Degree)
        {
            string str = Degree.ToString();
            rtn = EFEM.EFEMCommand_Send("SetAlignerDegree,Aligner1," + str);
            rtnstring = rtnSplit(rtn);
            return rtnstring[2] == "0" ? true : false;
        }

        public bool AlignerVacuum(OnOff onOff)
        {
            string str = onOff.ToString();
            rtn = EFEM.EFEMCommand_Send("AlignerVacuum,Aligner1," + str, Timeout_ms.AlignerVacuum);
            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length >= 4)
            {
                return rtnstring[3] == "0" ? true : false;
            }
            else
            {
                return false;
            }
        }

        public enum Pn
        {
            P1,
            P2,
            P3,
            P4,
        }

        public enum OnOff
        {
            On,
            Off,
        }

        private class Timeout_ms
        {
            public const int Home = 10000;
            public const int GetStatus = 3000;
            public const int Alignment = 90000;
            public const int AlignerVacuum = 3000;
        }
    }

    public class IO
    {
        private static string rtn = "";
        private static string[] rtnstring;
        private static string errorDescription = "";
        private static bool eFEMInterlock;
        private static bool eQInterlock;
        #region

        public string ErrorDescription
        {
            get { return errorDescription; }
        }

        public bool EFEMInterlock
        {
            get { return eFEMInterlock; }
        }

        public bool EQInterlock
        {
            get { return eQInterlock; }
        }

        #endregion

        private static string[] rtnSplit(string str)
        {
            string[] Str = Regex.Split(str, "\r", RegexOptions.Singleline);
            string[] Str2 = Regex.Split(Str[0], ",", RegexOptions.Singleline);
            return Str2;
        }

        public bool SignalTower(LampState lampState)
        {
            string str = lampState.ToString();
            rtn = EFEM.EFEMCommand_Send("SignalTower,IO," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length == 4)
            {
                return rtnstring[3] == "0" ? true : false;
            }
            else
            {
                return false;
            }
        }

        //沒接真空未測試
        public bool GetPressureDifference(ref int pressure)
        {
            rtn = EFEM.EFEMCommand_Send("GetPressureDifference,IO");
            rtnstring = rtnSplit(rtn);
            int Pressure;
            if (rtnstring[2] == "0")
            {
                Pressure = Convert.ToInt16(rtnstring[2]);
                pressure = Pressure;
                return true;
            }
            else
            {
                pressure = 0;
                return false;
            }
        }

        public bool SetFFUVoltage(double Voltage)
        {
            string str = Voltage.ToString();
            rtn = EFEM.EFEMCommand_Send("SetFFUVoltage,IO," + str);
            rtnstring = rtnSplit(rtn);
            return rtnstring[3] == "0" ? true : false;
        }

        public bool Buzzer(BuzzerSts buzzer)
        {
            string str = buzzer.ToString();
            rtn = EFEM.EFEMCommand_Send("Buzzer,IO," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[3] == "0")
            {
                errorDescription = "";
                return true;
            }
            else
            {
                errorDescription = rtnstring[4];
                return false;
            }
        }

        public bool GetEFEMInterlock()
        {
            rtn = EFEM.EFEMCommand_Send("GetEFEMInterlock,IO");
            rtnstring = rtnSplit(rtn);
            bool OnOff;
            if (rtnstring.Length == 3)
            {
                OnOff = rtnstring[2] == "0" ? false : true;
                eFEMInterlock = OnOff;
                return true;
            }
            else
            {
                errorDescription = rtnstring[4];
                eFEMInterlock = false;
                return false;
            }
        }

        public bool SetEFEMInterlock(bool onOff)
        {
            string str = onOff ? "1" : "0";
            rtn = EFEM.EFEMCommand_Send("SetEFEMInterlock,IO," + str);
            rtnstring = rtnSplit(rtn);
            return rtnstring[2] == "0" ? true : false;
        }

        public bool GetEQInterlock()
        {
            rtn = EFEM.EFEMCommand_Send("GetEQInterlock,IO");

            rtnstring = rtnSplit(rtn);
            if (rtnstring.Length == 3)
            {
                eQInterlock = rtnstring[2] == "0" ? false : true;

                return true;
            }
            else if (rtnstring.Length == 5)
            {
                errorDescription = rtnstring[4];
                eQInterlock = rtnstring[2] == "0" ? false : true;
                return false;
            }
            else
            {
                eQInterlock = false;
                return false;
            }
        }

        public enum LampState
        {
            AllOff,
            AllFlash,
            RedOn,
            RedOff,
            RedFlash,
            YellowOn,
            YellowOff,
            YellowFlash,
            GreenOn,
            GreenOff,
            GreenFlash,
        }

        public enum BuzzerSts
        {
            On,
            Off,
        }
    }

    public class Alignment
    {
        private static string rtn = "";
        private static string[] rtnstring;

        private static string[] rtnSplit(string str)
        {
            string[] Str = Regex.Split(str, "\r", RegexOptions.Singleline);
            string[] Str2 = Regex.Split(Str[0], ",", RegexOptions.Singleline);

            return Str2;
        }

        public static bool Clamp(bool onOff)
        {
            string str = onOff ? "1" : "0";
            rtn = EFEM.EFEMCommand_Send("Clamp,Alignment," + str);
            rtnstring = rtnSplit(rtn);
            return rtnstring[3] == "0" ? true : false;
        }

        public static bool DoAlignerment()
        {
            rtn = EFEM.EFEMCommand_Send("DoAlignerment,Alignment");
            rtnstring = rtnSplit(rtn);
            return rtnstring[2] == "0" ? true : false;
        }

        public static bool WaferPresence(ref bool presense)
        {
            rtn = EFEM.EFEMCommand_Send("WaferPresence,Alignment");
            rtnstring = rtnSplit(rtn);
            bool Presence;
            if (rtnstring.Length == 4)
            {
                Presence = rtnstring[2] == "1" ? true : false;
                presense = Presence;
                return true;
            }
            else
            {
                presense = false;
                return false;
            }
        }
    }

    public class Reader
    {
        private static string rtn = "";
        private static string[] rtnstring;

        private static string[] rtnSplit(string str)
        {
            string[] Str = Regex.Split(str, "\r", RegexOptions.Singleline);
            string[] Str2 = Regex.Split(Str[0], ",", RegexOptions.Singleline);

            return Str2;
        }

        public static string BarcodeRead(Pn pn)
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("Reader,BarcodeRead," + str);
            rtnstring = rtnSplit(rtn);
            return rtnstring[2];
        }

        public static string OCRRead(Pn pn)
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("Reader,OCRRead," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[2] != "1")
            {
                return rtnstring[2];
            }
            else
            {
                return rtnstring[3];
            }
        }

        public static string OCRConnect(Pn pn)
        {
            string str = pn.ToString();
            rtn = EFEM.EFEMCommand_Send("Connect,OCRRead," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[2] != "1")
            {
                return rtnstring[2];
            }
            else
            {
                return rtnstring[3];
            }
        }

        public enum Pn
        {
            P1,
            P2,
            P3,
            P4,
        }
    }

    public class Stage
    {
        private bool waferPresence = false;
        private bool ready = true;
        private bool measuredone = false;
        private bool busy = false;
        private int slot;
        public LoadPort Slot_pn;
        public Label lb;
        public PictureBox pb;
        public static string state_ready = "Stage1 Ready";
        private string waferinfo = state_ready;

        public void Update_Sts()
        {
            if (lb != null)
            {
                if (Slot_pn != null && slot != 0)
                {
                    waferinfo = Slot_pn.pn.ToString() + "," + slot;
                    lb.Text = waferinfo;
                }
                else
                {
                    lb.Text = state_ready;
                }
            }
            if (pb != null)
            {
                if (Slot_pn != null && slot != 0)
                {
                    pb.BackColor = EFEM.slot_status_Color.ProcessingStage1;
                }
                else
                {
                    pb.BackColor = EFEM.slot_status_Color.AlignerStageReady;
                }
            }
        }

        #region get/set

        public bool Measuredone
        {
            get { return measuredone; }
            set { measuredone = value; }
        }

        public int Slot
        {
            get { return slot; }
            set { slot = value; }
        }

        public string WaferInfo
        {
            get { return waferinfo; }
        }

        public bool WaferPresence
        {
            get { return waferPresence; }
            set { waferPresence = value; }
        }

        public bool Ready
        {
            get { return ready; }
            set { ready = value; }
        }

        public bool Busy
        {
            get { return busy; }
            set { busy = value; }
        }

        #endregion
    }

    public class E84
    {
        private bool ready2load = false;
        private bool ready2unload = false;

        //private bool busy = false;
        private static string rtn = "";

        private static string[] rtnstring;

        public enum E84_Num
        {
            E841,
            E842,
        }

        private static string[] rtnSplit(string str)
        {
            string[] Str = Regex.Split(str, "\r", RegexOptions.Singleline);
            string[] Str2 = Regex.Split(Str[0], ",", RegexOptions.Singleline);
            return Str2;
        }

        public bool SetAuto(E84_Num e84_Num)
        {
            if (!EFEM.IsInit)
            {
                return false;
            }
            string str = e84_Num.ToString();
            rtn = EFEM.EFEMCommand_Send("Auto," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[2] == "OK")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetManual(E84_Num e84_Num)
        {
            if (!EFEM.IsInit)
            {
                return false;
            }
            string str = e84_Num.ToString();
            rtn = EFEM.EFEMCommand_Send("Manual," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[2] == "OK")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Reset(E84_Num e84_Num)
        {
            if (!EFEM.IsInit)
            {
                return false;
            }
            string str = e84_Num.ToString();
            rtn = EFEM.EFEMCommand_Send("Reset," + str);
            rtnstring = rtnSplit(rtn);
            if (rtnstring[2] == "OK")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Ready2Load
        {
            get { return ready2load; }
            set { ready2load = value; }
        }

        public bool Ready2UnLoad
        {
            get { return ready2unload; }
            set { ready2unload = value; }
        }
    }
}