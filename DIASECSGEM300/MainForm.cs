using Delta.DIAAuto.DIASECSGEM;
using Delta.DIAAuto.DIASECSGEM.Equipment;
using Delta.DIAAuto.DIASECSGEM.GEMDataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace DemoFormDiaGemLib
{
    public partial class MainForm : Form
    {
        private DriverConfigInfo _frmDriverConfigInfo;
        public static DIASecsGemController _gemControler;
        private System.Threading.Timer _updateSVTimer;
        private ProcessState _CurProcessState = ProcessState.Initial;
        private List<ulong> listAlarmID = new List<ulong>();
        private List<CommandParameter> objLastRecvCPs;
        private PPManager _ppManager;
        private RCMD_TRANSFER _rcmdTRANSFER;
        private RCMD_GO _rcmdGO;

        private RCMD_START _rcmdSTART;
        private RCMD_STOP _rcmdSTOP;
        private RCMD_GO_REMOTE _rcmdGO_REMOTE;
        private RCMD_GO_LOCAL _rcmdGO_LOCAL;
        private RCMD_ACCESSMODE_ASK _rcmdACCESSMODE_ASK;
        private RCMD_PP_SELECT _rcmdPP_SELECT;
        private RCMD_ACCESSMODE_CHANGE _rcmdACCESSMODE_CHANGE;
        private RCMD_SLOTMAP_L_C _rcmdSLOTMAP_L_C;
        private RCMD_SLOTMAP_L _rcmdSLOTMAP_L;
        private RCMD_MEASURE_L_C _rcmdMEASURE_L_C;
        private RCMD_MEASURE_L _rcmdMEASURE_L;
        private RCMD_RELEASE_L_C _rcmdRRELEASE_L_C;
        private RCMD_RELEASE_L _rcmdRRELEASE_L;
        private RCMD_PORT_TRANSFERSTATUS_ASK _rcmdPORT_TRANSFERSTATUS_ASK;
        private RCMD_CANCEL_L_C _rcmdCANCEL_L_C;
        private RCMD_CANCEL_L _rcmdCANCEL_L;



        private string _modelType = "TrimGap";
        private string _softRev = "1.0.0.0";
        private PPBodyType _ppBodyType = PPBodyType.Both;
        private int paramCount = 40;//Recipe Parameters Count
        private bool bDataCheckOK;
        private string err;

        public bool bCanReplyCreateObjectRequestCommand = false;
        public bool bCanReplyDeleteObjectRequestCommand = false;
        public bool bCanReplyCarrierActionRequest = false;
        public List<ObjectInstance> lastRecvListObjectInstance = null;

        public byte[] TestABS = new byte[] { 10 };

        public bool bCanReplyS2F42 = false;
        public bool bCanReplyS10F04orS10F06 = false;
        public bool bCanReplySetsCONTROLStateModel = false;

        public static List<string> PJ_list = new List<string>();
        public static List<string> CJ_list = new List<string>();
        public static List<string> Carrier_list = new List<string>();
        public static List<string> Substrate_list = new List<string>();

        public delegate byte SECSFunction_CallBack(List<string> data);
        SECSFunction_CallBack function_CallBack;

        public delegate byte RemoteCommand_CallBack(List<string> data); //S2F41
        RemoteCommand_CallBack remoteCommand_CallBack;

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

        public class SecsDataTemp
        {
            public string LoadPortID;
            public string CarrierID;

            public SecsDataTemp()
            {
                LoadPortID = "";
                CarrierID = "";
            }

            public void Clear()
            {
                LoadPortID = "";
                CarrierID = "";
            }

            public void Set(string lpID, string caID = "")
            {
                LoadPortID = lpID;
                CarrierID = caID;
            }

            public bool IsEmpty()
            {
                return LoadPortID == "" && CarrierID == "";
            }
        }

        public SecsDataTemp SlotMapData = new SecsDataTemp();
        public SecsDataTemp ReleaseData = new SecsDataTemp();
        public SecsDataTemp MeasureStartData = new SecsDataTemp();
        public SecsDataTemp CancelData = new SecsDataTemp();
        public SecsDataTemp AccessModeChangeData = new SecsDataTemp();
        public SecsDataTemp AccessModeAskData = new SecsDataTemp();
        public SecsDataTemp ChangeRecipeData = new SecsDataTemp();

        public MainForm()
        {
            InitializeComponent();
            _frmDriverConfigInfo = new DriverConfigInfo();

            dgvCPs.ColumnCount = 3;                             // 定義所需要的行數
            dgvCPs.Columns[0].Name = "NAME";
            dgvCPs.Columns[1].Name = "VALUE";
            dgvCPs.Columns[2].Name = "ACK";
            dgvCPs.Columns[0].ReadOnly = true;
            dgvCPs.Columns[1].ReadOnly = true;
            dgvCPs.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCPs.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCPs.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;

            _ppManager = new PPManager();
            _rcmdGO = new RCMD_GO();
            _rcmdTRANSFER = new RCMD_TRANSFER();

            _rcmdSTART = new RCMD_START();
            _rcmdSTOP = new RCMD_STOP();
            _rcmdGO_REMOTE = new RCMD_GO_REMOTE();
            _rcmdGO_LOCAL = new RCMD_GO_LOCAL();
            _rcmdACCESSMODE_ASK = new RCMD_ACCESSMODE_ASK();
            _rcmdPP_SELECT = new RCMD_PP_SELECT();
            _rcmdACCESSMODE_CHANGE = new RCMD_ACCESSMODE_CHANGE();
            _rcmdSLOTMAP_L_C = new RCMD_SLOTMAP_L_C();
            _rcmdSLOTMAP_L = new RCMD_SLOTMAP_L();
            _rcmdMEASURE_L_C = new RCMD_MEASURE_L_C();
            _rcmdMEASURE_L = new RCMD_MEASURE_L();
            _rcmdRRELEASE_L_C = new RCMD_RELEASE_L_C();
            _rcmdRRELEASE_L = new RCMD_RELEASE_L();
            _rcmdPORT_TRANSFERSTATUS_ASK = new RCMD_PORT_TRANSFERSTATUS_ASK();
            _rcmdCANCEL_L_C = new RCMD_CANCEL_L_C();
            _rcmdCANCEL_L = new RCMD_CANCEL_L();

            objLastRecvCPs = new List<CommandParameter>();

            foreach (string ppid in _ppManager.PPIDList)
            {
                lvPPIDs.Items.Add(ppid);
            }

            _updateSVTimer = new System.Threading.Timer(ContinueUpdateSV, null, Timeout.Infinite, Timeout.Infinite);

            _gemControler = new DIASecsGemController();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            _gemControler.DebugOutLogEvent += _gemControler_DebugOutLogEvent;
            _gemControler.InitialCompleted += _gemControler_InitialCompleted;
            _gemControler.LicenseEffectivenessExpired += _gemControler_LicenseEffectivenessExpired;
            _gemControler.LoopbackDiagnosticData += _gemControler_LoopbackDiagnosticData;
            _gemControler.DateTimeSyncCommand += _gemControler_DateTimeSyncCommand;
            _gemControler.TerminalDisplay += _gemControler_TerminalDisplay;
            _gemControler.NewEqpConstantDownload += _gemControler_NewEqpConstantDownload;
            _gemControler.RemoteCommand += _gemControler_RemoteCommand;
            _gemControler.ProcessProgramLoadInquire += _gemControler_ProcessProgramLoadInquire;
            _gemControler.ProcessProgramDownload += _gemControler_ProcessProgramDownload;
            _gemControler.ProcessProgramUploadRequest += _gemControler_ProcessProgramUploadRequest;
            //FormattedProcessProgramDownload (CCODE, U2 Format)
            //_gemControler.FormattedProcessProgramDownload += _gemControler_FormattedProcessProgramDownload;
            //FormattedProcessProgramDownload2(CCODE, A/U2/U4/I2/I4 Format)
            _gemControler.FormattedProcessProgramDownload2 += _gemControler_FormattedProcessProgramDownload2;
            _gemControler.FormattedProcessProgramUploadRequest += _gemControler_FormattedProcessProgramUploadRequest;
            _gemControler.ProcessProgramSendReply += _gemControler_ProcessProgramSendReply;
            _gemControler.ProcessProgramDelete += _gemControler_ProcessProgramDelete;
            _gemControler.ProcessProgramDirectoryQuery += _gemControler_ProcessProgramDirectoryQuery;
            _gemControler.NotifyMessageNG += _gemControler_NotifyMessageNG;
            _gemControler.HostSetsCONTROLStateModel += _gemControler_HostSetsCONTROLStateModel;
            _gemControler.CreateObjectRequestCommand += _gemControler_CreateObjectRequestCommand;
            _gemControler.DeleteObjectRequestCommand += _gemControler_DeleteObjectRequestCommand;
            _gemControler.SetAttrRequestCommand += _gemControler_SetAttrRequestCommand;
            _gemControler.ProcessJobCommand += _gemControler_ProcessJobCommand;
            _gemControler.ProcessJobDequeue += _gemControler_ProcessJobDequeue;
            _gemControler.ControlJobCommand += _gemControler_ControlJobCommand;
            _gemControler.CarrierActionRequest += _gemControler_CarrierActionRequest;
            _gemControler.CancelAllCarrierOutRequest += _gemControler_CancelAllCarrierOutRequest;
            _gemControler.PortChangeAccessRequest += _gemControler_PortChangeAccessRequest;
            _gemControler.PortActionRequest += _gemControler_PortActionRequest;
            _gemControler.CarrierTagReadRequest += _gemControler_CarrierTagReadRequest;
            _gemControler.CarrierTagWriteDataRequest += _gemControler_CarrierTagWriteDataRequest;
            _gemControler.LoopbackDiagnosticData += _gemControler_LoopbackDiagnosticData;

            _gemControler.SECSMessageSent += _gemControler_SECSMessageSent;
            _gemControler.SECSMessageReplyT3 += _gemControler_SECSMessageReplyT3;
            _gemControler.UnknownSECSMessageReceived += _gemControler_UnknownSECSMessageReceived;
            _gemControler.ErrorSECSMessageReceived += _gemControler_ErrorSECSMessageReceived;

            InitialDIASecsGem();
        }

        //System Inform----------------------------------------------------------------------------------------------------
        private void _gemControler_LicenseEffectivenessExpired(object sender, LicenseEffectivenessExpiredArgs e)
        {
            //Inform License Effectiveness Expired
        }

        private void _gemControler_NotifyMessageNG(object sender, NotifyMessageNGArgs e)
        {
            string err;
            if (e.Stream == 2 && e.Function == 49 && e.Ack != 0)
            {
                //e.Systembytes
                WriteLog(LogLevel.Error, e.Message);
            }
        }

        private void _gemControler_SECSMessageSent(object sender, SECSMessageSentArgs e)
        {
            byte stream = e.Message.Header.Stream;
            byte function = e.Message.Header.Function;
            uint systembytes = e.Message.Header.SystemBytes;
            object tag = e.Message.Tag;
            string msg = $"S{stream}F{function}, Systembytes({systembytes}), Sent.";
            WriteLog(LogLevel.Trace, msg);
        }

        private void _gemControler_SECSMessageReplyT3(object sender, SECSMessageReplyT3Args e)
        {
            byte stream = e.MessageHeader.Stream;
            byte function = e.MessageHeader.Function;
            uint systembytes = e.MessageHeader.SystemBytes;
            object tag = e.Tag;
            string msg = $"S{stream}F{function}, Systembytes({systembytes}), T3 Timeout.";
            WriteLog(LogLevel.Warn, msg);
        }

        //Unknown SECS Message(Customized SECS Message)----------------------------------------------------------------------------------------------------
        private void _gemControler_UnknownSECSMessageReceived(object sender, UnknownSECSMessageReceivedArgs e)
        {
            byte stream = e.Message.Header.Stream;
            byte function = e.Message.Header.Function;
            uint systembytes = e.Message.Header.SystemBytes;
            object tag = e.Message.Tag;
            string msg = $"S{stream}F{function}, Systembytes({systembytes}), Received.";
            WriteLog(LogLevel.Trace, msg);

            string sxfy = $"s{stream}f{function}";
            switch (sxfy)
            {
                case "s13f11":

                    break;
            }
        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
            #region Structure:
            //L,3 
            //	1. < ALCD >
            //	2. < ALID >
            //	3. < ALTX >
            #endregion

            byte[] rawData = null;
            string errMsg = string.Empty;

            //encode rawdata to buffer
            byte[] buf = new byte[256 * 1024];
            int offset = 0;
            try
            {
                Delta.DIAAuto.DIASECSGEM.SECSLib.ItemOut(ref buf, ref offset, ItemFmt.L, 3);
                Delta.DIAAuto.DIASECSGEM.SECSLib.ItemOut(ref buf, ref offset, ItemFmt.B, 1, (byte)128);
                Delta.DIAAuto.DIASECSGEM.SECSLib.ItemOut(ref buf, ref offset, ItemFmt.U4, 1, (ulong)100);
                Delta.DIAAuto.DIASECSGEM.SECSLib.ItemOut(ref buf, ref offset, ItemFmt.A, 3, "AAA");

                rawData = new byte[offset];
                Array.Copy(buf, rawData, rawData.Length);

                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("key1", "EQ_value1");
                dic.Add("key2", "EQ_value2");

                int result = _gemControler.SECSMessageSend(13, 1, 1, 0, rawData, out errMsg, dic);
                if (result != 0)
                {
                    MessageBox.Show(errMsg);
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.ToString();
            }
        }

        //Error SECS Message----------------------------------------------------------------------------------------------------
        private void _gemControler_ErrorSECSMessageReceived(object sender, ErrorSECSMessageReceivedArgs e)
        {
            //Inform Error SECS Message Received
            byte stream = e.Message.Header.Stream;
            byte function = e.Message.Header.Function;
            uint systembytes = e.Message.Header.SystemBytes;
            object tag = e.Message.Tag;
            string msg = $"Error SECS Message S{stream}F{function}, Systembytes({systembytes}), Received.";
            WriteLog(LogLevel.Error, msg);
        }

        //Log Info----------------------------------------------------------------------------------------------------
        private void _gemControler_DebugOutLogEvent(object sender, GemLogArgs e)
        {
            DIASecsGemController gem = (DIASecsGemController)sender;
            string log = string.Format("[{0:yyyy/MM/dd HH:mm:ss:fff}] {1}", e.DateTime, e.Message);

            //select color
            Color displayColor = Color.Black; //default
            switch (e.Message.Substring(1, 5).Trim())
            {
                case "Fatal":
                case "Error":
                    displayColor = Color.Red;
                    break;
                case "Warn":
                    displayColor = Color.Orange;
                    break;
            }

            this.Invoke((MethodInvoker)delegate ()
            {
                switch (e.Type)
                {
                    case eLogType.Main:
                        rtbGemLog.SelectionColor = displayColor;
                        rtbGemLog.AppendText(log + Environment.NewLine);
                        break;
                    case eLogType.Driver:
                        rtbDriverLog.SelectionColor = displayColor;
                        rtbDriverLog.AppendText(log + Environment.NewLine);
                        break;
                    case eLogType.SML:
                        rtbSecsLog.AppendText(log + Environment.NewLine);
                        break;
                }
            });
        }

        //Initial----------------------------------------------------------------------------------------------------
        private void InitialDIASecsGem()
        {
            string log = string.Empty;
            string errLog = string.Empty;

            //listAlarmID.Add(10001);
            //listAlarmID.Add(10002);

            int iniResult;
            //Eqp
            iniResult = _gemControler.Initialize(out errLog, listAlarmID);

            if (iniResult != 0)
            {
                WriteLog(LogLevel.Error, errLog);
                lblInitResult.BackColor = Color.Red;
                lblInitResult.Text = iniResult.ToString();
                MessageBox.Show(errLog);
                return;
            }

            //DIASECSGEM Inital OK
            if (iniResult == 0)
            {
                WriteLog(LogLevel.Info, string.Format("{0:yyyyMMddhhmmss.fff} :DIASECSGEM Initial Completed....", DateTime.Now));
                lblInitResult.BackColor = Color.GreenYellow;
            }

            lblInitResult.Text = iniResult.ToString();
        }

        private void _gemControler_InitialCompleted(object sender, EventArgs e)
        {
            int result = 0;
            string err;
            object obj = null;
            ItemFmt format;
            string errLog = string.Empty;
            
            result = _gemControler.EnableComm(out err);
            if (result == 0)
            {
                SpinWait.SpinUntil(()=>false, 1000);
                result = _gemControler.OnlineRemote(out err);
            }
            
            //Sync Data, Update All Setting SV/DV/EC Data..
            //..
            //..

            //SYS_MDLN
            _gemControler.UpdateSV(9, _modelType, out errLog);
            //SYS_SOFTREV
            _gemControler.UpdateSV(10, _softRev, out errLog);
            //SYS_PROCESS_STATE
            _gemControler.UpdateSV(8, (byte)_CurProcessState, out errLog);
            //SYS_PP_FORMAT
            if (rdoPPBody_Unformatted.Checked)
                _gemControler.UpdateSV(15, (byte)1, out errLog);
            else if (rdoPPBody_Formatted.Checked)
                _gemControler.UpdateSV(15, (byte)2, out errLog);
            else
                _gemControler.UpdateSV(15, (byte)3, out errLog);

            chkContinueUpdateSV.Checked = false;

            //SYS_LOG_LEVEL
            if (_gemControler.GetEC(83, out format, out obj, out errLog) == 0)
            {
                byte level = (byte)obj;
                switch (level)
                {
                    case 0: if (!rdoLog_Trace.Checked) rdoLog_Trace.Checked = true; break;
                    case 1: if (!rdoLog_Debug.Checked) rdoLog_Debug.Checked = true; break;
                    case 2: if (!rdoLog_Info.Checked) rdoLog_Info.Checked = true; break;
                    case 3: if (!rdoLog_Warn.Checked) rdoLog_Warn.Checked = true; break;
                    case 4: if (!rdoLog_Error.Checked) rdoLog_Error.Checked = true; break;
                }
            }

            //Update Object Info
            List<ObjectInstance> listObjectInstance = null;
            _gemControler.GetObject(out listObjectInstance, out err);
            string strObjectInfo = string.Empty;
            List<ObjectAttribute> listObjectAttributes = null;
            foreach (ObjectInstance objectInstance in listObjectInstance.OrderBy(o => o.ObjType))
            {
                switch (objectInstance.ObjType)
                {
                    case ObjectTypeKey.PROCESSJOB:
                        PJ_list.Add(objectInstance.ObjID);
                        break;
                    case ObjectTypeKey.CONTROLJOB:
                        CJ_list.Add(objectInstance.ObjID);
                        break;
                    case ObjectTypeKey.CARRIER:
                        Carrier_list.Add(objectInstance.ObjID);
                        break;
                    default:
                        break;
                }
            }
            //GetObject
            /* ObjectInstance objectInstance = null;
             result = _gemControler.GetObject(txtCreateObjType.Text, txtCreateObjID.Text, out objectInstance, out err);
             //UpdateObject
             if (objectInstance != null)
             {
                 List<ObjectAttribute> listObjectAttributes = new List<ObjectAttribute>();
                 //CARRIER Update
                 ObjectAttribute objectAttribute = new ObjectAttribute(441, ObjectAttributeKey.OBJID, objectInstance.ObjID);
                 listObjectAttributes.Add(objectAttribute);
                 objectAttribute = new ObjectAttribute(443, "CARRIERIDSTATUS", (byte)0);
                 listObjectAttributes.Add(objectAttribute);
                 //...other ObjectAttribute

                 objectInstance.ListObjectAttributes = listObjectAttributes;
                 result = _gemControler.UpdateObject(objectInstance, out err);
             }*/
        }

        //SECS Driver Connect Status----------------------------------------------------------------------------------------------------
        private void btnStart_Click(object sender, EventArgs e)
        {
            string log = string.Empty;
            if (_gemControler != null)
            {
                string err;
                int result = _gemControler.DriverStart(out err);
                if (result != 0)
                {
                    log = string.Format("Start Fialure, ({0})Reason:{1}", result, err);
                    WriteLog(LogLevel.Warn, log);
                    MessageBox.Show(log);
                    return;
                }

                //btnStart.Enabled = false;
                //btnStop.Enabled = true;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            string log = string.Empty;
            if (_gemControler != null)
            {
                _gemControler.DriverStop();

                //btnStart.Enabled = true;
                //btnStop.Enabled = false;
            }
        }

        //GEM Communication----------------------------------------------------------------------------------------------------
        private void btnEnableComm_Click(object sender, EventArgs e)
        {
            string log = string.Empty;
            if (_gemControler != null)
            {
                string err;
                int result = _gemControler.EnableComm(out err);
                if (result != 0)
                {
                    log = string.Format("EnableComm Fialure, ({0})Reason:{1}", result, err);
                    WriteLog(LogLevel.Warn, log);
                    MessageBox.Show(log);
                }
            }
        }

        private void btnDisableComm_Click(object sender, EventArgs e)
        {
            string log = string.Empty;
            if (_gemControler != null)
            {
                string err;
                int result = _gemControler.DisableComm(out err);
                if (result != 0)
                {
                    log = string.Format("DisableComm Fialure, ({0})Reason:{1}", result, err);
                    WriteLog(LogLevel.Warn, log);
                    MessageBox.Show(log);
                }
            }

            //btnDisableComm.Enabled = false;
            //btnEnableComm.Enabled = true;          
        }

        //GEM Control State----------------------------------------------------------------------------------------------------
        private void btnOffLine_Click(object sender, EventArgs e)
        {
            string log = string.Empty;
            if (_gemControler != null)
            {
                string err;
                int result = _gemControler.OffLine(out err);
                if (result != 0)
                {
                    log = string.Format("OffLine Fialure, ({0})Reason:{1}", result, err);
                    WriteLog(LogLevel.Warn, log);
                    MessageBox.Show(log);
                }
            }
        }

        private void btnOnLineLocal_Click(object sender, EventArgs e)
        {
            string log = string.Empty;
            if (_gemControler != null)
            {
                string err;
                int result = _gemControler.OnLineLocal(out err);
                if (result != 0)
                {
                    log = string.Format("OnLineLocal Fialure, ({0})Reason:{1}", result, err);
                    WriteLog(LogLevel.Warn, log);
                    MessageBox.Show(log);
                }
            }
        }

        private void btnOnLineRemote_Click(object sender, EventArgs e)
        {
            string log = string.Empty;
            if (_gemControler != null)
            {
                string err;
                int result = _gemControler.OnlineRemote(out err);
                if (result != 0)
                {
                    log = string.Format("OnLineRemote Fialure, ({0})Reason:{1}", result, err);
                    WriteLog(LogLevel.Warn, log);
                    MessageBox.Show(log);
                }
            }
        }

        private void _gemControler_HostSetsCONTROLStateModel(object sender, HostSetsCONTROLStateModelArgs e)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                txtSetsCONTROLStateModel.Text = e.HostSetsCONTROLStateModel.ToString();
                txtSetsCONTROLStateModelSystemBytes.Text = e.SystemBytes.ToString();
            });
            bCanReplySetsCONTROLStateModel = true;

            //Handle


            //Reply
            //Call _gemControler.HostSetsCONTROLStateModelReply


            //Sample Use Button Reply 
            //private void btnSetsCONTROLStateModelReplyMsg_Click(object sender, EventArgs e)
        }

        private void btnSetsCONTROLStateModelReplyMsg_Click(object sender, EventArgs e)
        {
            if (bCanReplySetsCONTROLStateModel)
            {
                eHostSetsCONTROLStateModel HostSetsCONTROLStateModel = txtSetsCONTROLStateModel.Text == eHostSetsCONTROLStateModel.RequestOffline.ToString()
                    ? eHostSetsCONTROLStateModel.RequestOffline : eHostSetsCONTROLStateModel.RequestOnline;
                byte bAck = byte.Parse(txtSetsCONTROLStateModelReplyAck.Text);
                uint uSystemBytes = uint.Parse(txtSetsCONTROLStateModelSystemBytes.Text);

                string err;
                _gemControler.HostSetsCONTROLStateModelReply(HostSetsCONTROLStateModel, bAck, uSystemBytes, out err);
                bCanReplySetsCONTROLStateModel = false;
            }
            else
            {
                string log = string.Format("Can't Reply Command, Because Not Receive Command!");
                WriteLog(LogLevel.Warn, log);
                MessageBox.Show(log);
            }
        }

        //GEM Event Notification----------------------------------------------------------------------------------------------------
        private void btnEventReport_Click(object sender, EventArgs e)
        {
            string log = string.Empty;
            if (_gemControler != null)
            {
                uint ceid;
                lblEventResultText.Text = "Result";
                lblEventResultText.BackColor = Color.Transparent;
                if (uint.TryParse(txtEventID.Text.Trim(), out ceid))
                {
                    string err;
                    int result = _gemControler.EventReportSend(uint.Parse(txtEventID.Text), out err);
                    lblEventResultText.Text = string.Format("EventReportSend Result={0}", result);
                    if (result != 0)
                    {
                        log = string.Format("EventReport CEID({0}) Failure, ({1})Reason:{2}", ceid, result, err);
                        lblEventResultText.BackColor = Color.Red;
                        WriteLog(LogLevel.Warn, log);
                        MessageBox.Show(log);
                    }
                    else
                    {
                        lblEventResultText.BackColor = Color.GreenYellow;
                    }
                }
                else
                {
                    log = string.Format("CEID({0}) was not illeage uint value.", txtEventID.Text);
                    WriteLog(LogLevel.Warn, log);
                    MessageBox.Show(log);
                }
            }
        }

        private void rdoProcessState_Click(object sender, EventArgs e)
        {
            string err;
            string log = string.Empty;
            ProcessState newProcessState = ProcessState.Initial;

            if (rdoProcessState_Initial.Checked)
                newProcessState = ProcessState.Initial;
            else if (rdoProcessState_Idle.Checked)
                newProcessState = ProcessState.Idle;
            else if (rdoProcessState_Run.Checked)
                newProcessState = ProcessState.Run;
            else if (rdoProcessState_Pause.Checked)
                newProcessState = ProcessState.Pause;
            else if (rdoProcessState_Stop.Checked)
                newProcessState = ProcessState.Stop;
            else if (rdoProcessState_Down.Checked)
                newProcessState = ProcessState.Down;

            if (_CurProcessState != newProcessState)
            {
                ProcessState previousState = _CurProcessState;
                _CurProcessState = newProcessState;

                //SYS_PREVIOUS_PROCESS_STATE
                if (_gemControler.UpdateSV(7, (byte)previousState, out log) != 0)
                {
                    WriteLog(LogLevel.Warn, log);
                    return;
                }
                //SYS_PROCESS_STATE
                if (_gemControler.UpdateSV(8, (byte)_CurProcessState, out log) != 0)
                {
                    WriteLog(LogLevel.Warn, log);
                    return;
                }

                //Processing_State_Change Event
                uint ceid = 4;
                int result = _gemControler.EventReportSend(ceid, out err);
                if (result != 0)
                {
                    log = string.Format("EventReport CEID({0}) Failure, ({1})Reason:{2}", ceid, result, err);
                    WriteLog(LogLevel.Warn, log);
                }
            }
        }

        //GEM Alarm Management----------------------------------------------------------------------------------------------------
        private void btnAlarmReportSend_Click(object sender, EventArgs e)
        {
            string log = string.Empty;
            if (_gemControler != null)
            {
                uint alarmid;
                lblAlarmResultText.Text = "Result";
                lblAlarmResultText.BackColor = Color.Transparent;

                if (uint.TryParse(txtAlarmID.Text.Trim(), out alarmid))
                {
                    if (rdoAlarmSet.Checked)
                    {
                        if (!listAlarmID.Contains(alarmid))
                        {
                            listAlarmID.Add(alarmid);
                        }
                    }
                    else
                    {
                        if (listAlarmID.Contains(alarmid))
                        {
                            listAlarmID.Remove(alarmid);
                        }
                    }

                    string err;
                    int result = _gemControler.AlarmReportSend(alarmid, rdoAlarmSet.Checked, out err);
                    lblAlarmResultText.Text = string.Format("AlarmReportSend Result={0}", result);
                    if (result != 0)
                    {
                        log = string.Format("AlarmReport AlarmID({0}) Fialure, ({1})Reason:{2}", alarmid, result, err);
                        WriteLog(LogLevel.Warn, log);
                        lblAlarmResultText.BackColor = Color.Red;
                        MessageBox.Show(log);
                    }
                    else
                    {
                        lblAlarmResultText.BackColor = Color.GreenYellow;
                    }
                }
                else
                {
                    log = string.Format("AlarmID({0}) was not illeage uint value.", txtAlarmID.Text);
                    WriteLog(LogLevel.Warn, log);
                    MessageBox.Show(log);
                }
            }
        }

        //GEM Status Data Collection, Data Value ID(DVID)----------------------------------------------------------------------------------------------------
        private void btnUpdateSV_Click(object sender, EventArgs e)
        {
            string log = string.Empty;
            uint svid = 0;
            object obj = null;
            string err = string.Empty;
            int result = 0;

            uint.TryParse(txtUpdateSVID.Text.Trim(), out svid);
            string updateData = txtUpdateSV.Text.Trim();

            if (string.IsNullOrWhiteSpace(updateData))
            {
                MessageBox.Show("Miss SV Value.");
                return;
            }

            ItemFmt fmt = ConvertString2Fmt(cmbUpdateSVFormat.Text);
            if (ConvertString2Object(fmt, updateData, out obj))
            {
                result = _gemControler.UpdateSV(svid, obj, out err);
                lblSVResultText.Text = string.Format("UpdateSV Result={0}", result);
                if (result != 0)
                {
                    log = string.Format("UpdateSV SVID({0}) Fialure, ({1})Reason:{2}", svid, result, err);
                    WriteLog(LogLevel.Warn, log);
                    lblSVResultText.BackColor = Color.Red;
                    MessageBox.Show(log);
                }
                else
                {
                    lblSVResultText.BackColor = Color.GreenYellow;
                }
            }
        }

        private void btnUpdateListTypeSV_Click(object sender, EventArgs e)
        {
            string log = string.Empty;
            object obj = null;
            string err = string.Empty;
            int result = 0;

            uint svid = 3004;
            ListWrapper lw = new ListWrapper();
            lw.TryAdd(ItemFmt.A, "ABC", out err);
            lw.TryAdd(ItemFmt.B, new byte[] { 1, 2, 3 }, out err);
            lw.TryAdd(ItemFmt.Boolean, new bool[] { true, false }, out err);
            lw.TryAdd(ItemFmt.F4, new float[] { 1.2F, 3.4F }, out err);
            ListWrapper lw_1 = new ListWrapper();
            lw.TryAdd(ItemFmt.L, lw_1, out err);
            lw_1.TryAdd(ItemFmt.F8, new double[] { 5.6, 7.8 }, out err);
            lw_1.TryAdd(ItemFmt.I1, new sbyte[] { -1, -2 }, out err);
            lw_1.TryAdd(ItemFmt.I2, new short[] { -12, -34 }, out err);
            lw.TryAdd(ItemFmt.I4, new int[] { -567, -890 }, out err);
            ListWrapper lw_2 = new ListWrapper();
            lw.TryAdd(ItemFmt.L, lw_2, out err);
            lw_2.TryAdd(ItemFmt.I8, new long[] { -1234, -5678 }, out err);
            ListWrapper lw_2_1 = new ListWrapper();
            lw_2.TryAdd(ItemFmt.L, lw_2_1, out err);
            lw_2_1.TryAdd(ItemFmt.U1, new byte[] { 1, 2 }, out err);
            lw_2_1.TryAdd(ItemFmt.U2, new ushort[] { 12, 34 }, out err);
            lw_2.TryAdd(ItemFmt.U4, new uint[] { 567, 890 }, out err);
            lw.TryAdd(ItemFmt.U8, new ulong[] { 1234, 5678 }, out err);
            obj = lw;

            result = _gemControler.UpdateSV(svid, obj, out err);
            lblSVResultText.Text = string.Format("UpdateSV Result={0}", result);
            if (result != 0)
            {
                log = string.Format("Update List Type SV, SVID({0}) Fialure, ({1})Reason:{2}", svid, result, err);
                WriteLog(LogLevel.Warn, log);
                lblSVResultText.BackColor = Color.Red;
                MessageBox.Show(log);
            }
            else
            {
                lblSVResultText.BackColor = Color.GreenYellow;
            }
        }

        private void btnGetSV_Click(object sender, EventArgs e)
        {
            string log = string.Empty;
            uint svid = 0;
            object obj = null;
            string err = string.Empty;
            int result = 0;
            ItemFmt format;

            uint.TryParse(txtGetSVID.Text.Trim(), out svid);
            result = _gemControler.GetSV(svid, out format, out obj, out err);
            txtGetSVFormat.Text = "";
            txtGetSV.Text = "";

            lblSVResultText.Text = string.Format("GetSV Result={0}", result);
            if (result != 0)
            {
                log = string.Format("GetSV SVID({0}) Fialure, ({1})Reason:{2}", svid, result, err);
                WriteLog(LogLevel.Warn, log);
                lblSVResultText.BackColor = Color.Red;
                MessageBox.Show(log);
            }
            else
            {
                Tuple<string, string> desc = GetDataDesc(format, obj);
                txtGetSVFormat.Text = desc.Item1;
                txtGetSV.Text = desc.Item2;
                lblSVResultText.BackColor = Color.GreenYellow;
            }
        }

        private void ContinueUpdateSV(object obj)
        {
            string err = string.Empty;
            //continueSVV++;
            //for (uint i = 5000; i < 6000; i++)
            //{
            //    _gemControler.UpdateSV(i, continueSVV, out err);
            //}
        }

        private void chkContinueUpdateSV_CheckedChanged(object sender, EventArgs e)
        {
            if (chkContinueUpdateSV.Checked)
                _updateSVTimer.Change(0, 1000);
            else
                _updateSVTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        //GEM Equipment Constants----------------------------------------------------------------------------------------------------
        private void btnUpdateEC_Click(object sender, EventArgs e)
        {
            string log = string.Empty;
            uint ecid = 0;
            object obj = null;
            string err = string.Empty;
            int result = 0;

            uint.TryParse(txtUpdateECID.Text.Trim(), out ecid);
            string updateData = txtUpdateEC.Text.Trim();

            if (string.IsNullOrWhiteSpace(updateData))
            {
                MessageBox.Show("Miss EC Value.");
                return;
            }

            if (updateData.Split(',').Length > 1)
            {
                MessageBox.Show("EC's value did not support array data.");
                return;
            }

            ItemFmt fmt = ConvertString2Fmt(cmbUpdateECFormat.Text);
            if (ConvertString2Object(fmt, updateData, out obj))
            {
                //if (obj.GetType().IsArray) { obj = ((Array)obj).GetValue(0); }

                result = _gemControler.UpdateEC(ecid, obj, true, out err);
                lblECResultText.Text = string.Format("UpdateEC Result={0}", result);
                if (result != 0)
                {
                    log = string.Format("UpdateEC ECID({0}) Fialure, ({1})Reason:{2}", ecid, result, err);
                    WriteLog(LogLevel.Warn, log);
                    lblECResultText.BackColor = Color.Red;
                    MessageBox.Show(log);
                }
                else
                {
                    lblECResultText.BackColor = Color.GreenYellow;
                }
            }
        }

        private void btnGetEC_Click(object sender, EventArgs e)
        {
            string log = string.Empty;
            uint ecid = 0;
            object obj = null;
            string err = string.Empty;
            int result = 0;
            ItemFmt format;

            uint.TryParse(txtGetECID.Text.Trim(), out ecid);
            result = _gemControler.GetEC(ecid, out format, out obj, out err);
            txtGetECFormat.Text = "";
            txtGetEC.Text = "";

            lblECResultText.Text = string.Format("GetEC Result={0}", result);
            if (result != 0)
            {
                log = string.Format("GetEC ECID({0}) Fialure, ({1})Reason:{2}", ecid, result, err);
                WriteLog(LogLevel.Warn, log);
                lblECResultText.BackColor = Color.Red;
                MessageBox.Show(log);
            }
            else
            {
                Tuple<string, string> desc = GetDataDesc(format, obj);
                txtGetECFormat.Text = desc.Item1;
                txtGetEC.Text = desc.Item2;
                lblECResultText.BackColor = Color.GreenYellow;
            }
        }

        private void _gemControler_NewEqpConstantDownload(object sender, NewECVsArgs e)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                dgvNewECs.Rows.Clear();
                foreach (NewECV newECV in e.NewECVs)
                {
                    string[] row = new string[] { newECV.ID.ToString(), newECV.Format.ToString(), newECV.Value.ToString() };  // 定義一列的字串陣列
                    dgvNewECs.Rows.Add(row);
                }
                txtNewECsSystemBytes.Text = e.SystemBytes.ToString();
                dgvNewECs.Tag = e.NewECVs;
            });
            string err;
            byte eac = 0;
            _gemControler.NewEqpConstantDownloadReply(eac, e.SystemBytes, out err);

            if (eac == 0 && dgvNewECs.Tag != null)
            {
                List<NewECV> newECVs = (List<NewECV>)dgvNewECs.Tag;
                foreach (NewECV newECV in newECVs)
                {
                    _gemControler.UpdateEC(newECV.ID, newECV.Value, false, out err);
                }
                dgvNewECs.Tag = null;
            }
        }

        private void btnNewECsReply_Click(object sender, EventArgs e)
        {
            uint systembytes;
            byte eac;

            if (!uint.TryParse(txtNewECsSystemBytes.Text, out systembytes))
            {
                return;
            }

            if (!byte.TryParse(txtEAC.Text, out eac))
            {
                return;
            }

            string err;
            _gemControler.NewEqpConstantDownloadReply(eac, systembytes, out err);

            if (eac == 0 && dgvNewECs.Tag != null)
            {
                List<NewECV> newECVs = (List<NewECV>)dgvNewECs.Tag;
                foreach (NewECV newECV in newECVs)
                {
                    _gemControler.UpdateEC(newECV.ID, newECV.Value, false, out err);
                }
                dgvNewECs.Tag = null;
            }
        }

        private void rdoLog_CheckedChanged(object sender, EventArgs e)
        {
            string err = string.Empty;
            byte level = 2;
            if (rdoLog_Trace.Checked) level = 0;
            else if (rdoLog_Debug.Checked) level = 1;
            else if (rdoLog_Info.Checked) level = 2;
            else if (rdoLog_Warn.Checked) level = 3;
            else if (rdoLog_Error.Checked) level = 4;

            //SYS_LOG_LEVEL
            _gemControler.UpdateEC(83, level, true, out err);
        }

        //GEM Remote Control----------------------------------------------------------------------------------------------------

        public string LoadPortID = "";
        public string CarrierID = "";
        public string RecipeID = "";
        public string AccessMode = "";
        public string PPID = "";

        public bool bWaitSECS_StartCmd = false;
        public bool bWaitSECS_StopCmd = false;
        public bool bWaitSECS_ACCESSMODE_ASK = false;
        public bool bWaitSECS_PP_SELECT = false;
        public bool bSECS_ChangeAccessMode_Recive = false; // 判斷有沒有收到
        public bool bWaitSECS_SlotMapCmd = false;
        public bool bWaitSECS_MeasureCmd = false;
        public bool bWaitSECS_ReleaseCmd = false;
        public bool bWaitSECS_PORT_TRANSFERSTATUS_ASK = false;
        public bool bWaitSECS_CancelCmd = false;

        public bool bSECS_ReadyToLoad_LP1 = false;
        public bool bSECS_ReadyToLoad_LP2 = false;

        private void _gemControler_RemoteCommand(object sender, RemoteControlEventRemoteCommandArgs e)
        {
            LoadPortID = "";
            CarrierID = "";
            RecipeID = "";
            AccessMode = "";
            PPID = "";

            string strCommandParameters = string.Empty;

            this.Invoke((MethodInvoker)delegate ()
            {
                dgvCPs.Rows.Clear();
            });

            //dgvCPs.ColumnCount = 2;                             // 定義所需要的行數
            //dgvCPs.Columns[0].Name = "NAME";
            //dgvCPs.Columns[1].Name = "ACK";
            //dgvCPs.Columns[0].ReadOnly = true;
            //dgvCPs.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvCPs.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            //ItemFmt.A,        string
            //ItemFmt.B,        byte[]
            //ItemFmt.Boolean,  bool[]
            //ItemFmt.F4,       float[]
            //ItemFmt.F8,       double[]
            //ItemFmt.I1,       sbyte[]
            //ItemFmt.I2,       short[]
            //ItemFmt.I4,       int[] 
            //ItemFmt.I8,       long[]
            //ItemFmt.U1,       byte[]
            //ItemFmt.U2,       ushort[]
            //ItemFmt.U4,       uint[]
            //ItemFmt.U8,       ulong[]

            string[] row = new string[e.ReceiveCommandParameters.Count];
            bDataCheckOK = false;
            List<string> dataList = new List<string>();
            List<string> errList = new List<string>();
            #region Check Data, Keep Data, Reply CP Ack..
            byte btHCACK = 0;
            /*
            ack
            0 - ok, completed
            1 - invalid command
            2 - cannot do now
            3 - parameter error
            4 - initiated for asynchronous completion
            5 - rejected, already in desired condition
            6 - invalid object

            CPAck
            1 - unknown CPNAME
            2 - illegal value for CPVAL
            3 - illegal format for CPVAL
            */
            switch (e.ReceiveMessageName)
            {
                case eReceiveRemoteControlMessageName.S2F41:
                    {
                        //Sample S2F42 Use Button Reply
                        bCanReplyS2F42 = true;
                        objLastRecvCPs = e.ReceiveCommandParameters;

                        switch (e.RCMD)
                        {
                            case "START":
                                {
                                    bWaitSECS_StartCmd = true;
                                    RCMD_START obj = new RCMD_START();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdSTART = obj;
                                    }

                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        txtMessageName.Text = e.ReceiveMessageName.ToString();
                                        txtOBJSPEC.Text = e.ObjSpec;
                                        txtRCMD.Text = e.RCMD;
                                        txtSystemBytes.Text = e.SystemBytes.ToString();
                                    });
                                }
                                break;
                            case "STOP":
                                {
                                    bWaitSECS_StopCmd = true;
                                    RCMD_STOP obj = new RCMD_STOP();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        _rcmdSTOP = obj;
                                    }

                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        txtMessageName.Text = e.ReceiveMessageName.ToString();
                                        txtOBJSPEC.Text = e.ObjSpec;
                                        txtRCMD.Text = e.RCMD;
                                        txtSystemBytes.Text = e.SystemBytes.ToString();
                                    });
                                }
                                break;
                            case "GO-REMOTE":
                                {
                                    RCMD_GO_REMOTE obj = new RCMD_GO_REMOTE();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdGO_REMOTE = obj;
                                        string log = string.Empty;
                                        if (_gemControler != null)
                                        {
                                            int result = _gemControler.OnlineRemote(out err);
                                            if (result != 0)
                                            {
                                                log = string.Format("OnLineRemote Fialure, ({0})Reason:{1}", result, err);
                                                WriteLog(LogLevel.Warn, log);
                                                MessageBox.Show(log);
                                            }
                                        }
                                    }

                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        txtMessageName.Text = e.ReceiveMessageName.ToString();
                                        txtOBJSPEC.Text = e.ObjSpec;
                                        txtRCMD.Text = e.RCMD;
                                        txtSystemBytes.Text = e.SystemBytes.ToString();
                                    });
                                }
                                break;
                            case "GO-LOCAL":
                                {
                                    RCMD_GO_LOCAL obj = new RCMD_GO_LOCAL();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdGO_LOCAL = obj;
                                        string log = string.Empty;
                                        if (_gemControler != null)
                                        {
                                            int result = _gemControler.OnLineLocal(out err);
                                            if (result != 0)
                                            {
                                                log = string.Format("OnLineLocal Fialure, ({0})Reason:{1}", result, err);
                                                WriteLog(LogLevel.Warn, log);
                                                MessageBox.Show(log);
                                            }
                                        }
                                    }

                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        txtMessageName.Text = e.ReceiveMessageName.ToString();
                                        txtOBJSPEC.Text = e.ObjSpec;
                                        txtRCMD.Text = e.RCMD;
                                        txtSystemBytes.Text = e.SystemBytes.ToString();
                                    });
                                }
                                break;
                            case "ACCESSMODE-ASK":
                                {
                                    bWaitSECS_ACCESSMODE_ASK = true;
                                    errList.Clear();
                                    RCMD_ACCESSMODE_ASK obj = new RCMD_ACCESSMODE_ASK();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdACCESSMODE_ASK = obj;
                                        LoadPortID = _rcmdACCESSMODE_ASK.LOADPORT_ID;
                                        //AccessModeAskData.Set(LoadPortID);
                                        if(LoadPortID == "1" || LoadPortID == "2")
                                        {
                                            dataList.Clear();
                                            dataList.Add(e.RCMD);
                                            dataList.Add(LoadPortID);
                                            btHCACK = remoteCommand_CallBack(dataList);
                                        }
                                        else
                                        {
                                            btHCACK = 3;
                                            e.ReceiveCommandParameters[0].Ack = (byte)2;
                                        }

                                    }
                                    else
                                        btHCACK = 3;



                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        txtMessageName.Text = e.ReceiveMessageName.ToString();
                                        txtOBJSPEC.Text = e.ObjSpec;
                                        txtRCMD.Text = e.RCMD;
                                        txtSystemBytes.Text = e.SystemBytes.ToString();
                                    });
                                }
                                break;
                            case "PP-SELECT":
                                {
                                    bWaitSECS_PP_SELECT = true;
                                    RCMD_PP_SELECT obj = new RCMD_PP_SELECT();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdPP_SELECT = obj;
                                        LoadPortID = _rcmdPP_SELECT.LOADPORT_ID;
                                        RecipeID = _rcmdPP_SELECT.RECIPE_ID;
                                        ChangeRecipeData.Set(LoadPortID);
                                    }

                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        txtMessageName.Text = e.ReceiveMessageName.ToString();
                                        txtOBJSPEC.Text = e.ObjSpec;
                                        txtRCMD.Text = e.RCMD;
                                        txtSystemBytes.Text = e.SystemBytes.ToString();
                                    });
                                }
                                break;
                            case "ACCESSMODE-CHANGE":
                                {
                                    bSECS_ChangeAccessMode_Recive = true;
                                    RCMD_ACCESSMODE_CHANGE obj = new RCMD_ACCESSMODE_CHANGE();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdACCESSMODE_CHANGE = obj;
                                        LoadPortID = _rcmdACCESSMODE_CHANGE.LOADPORT_ID;
                                        AccessMode = _rcmdACCESSMODE_CHANGE.ACCESS_MODE;
                                        AccessModeChangeData.Set(LoadPortID);
                                    }

                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        txtMessageName.Text = e.ReceiveMessageName.ToString();
                                        txtOBJSPEC.Text = e.ObjSpec;
                                        txtRCMD.Text = e.RCMD;
                                        txtSystemBytes.Text = e.SystemBytes.ToString();
                                    });
                                }
                                break;
                            case "SLOTMAP-L&C":
                                {
                                    bWaitSECS_SlotMapCmd = true;
                                    RCMD_SLOTMAP_L_C obj = new RCMD_SLOTMAP_L_C();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdSLOTMAP_L_C = obj;
                                        LoadPortID = _rcmdSLOTMAP_L_C.LOADPORT_ID;
                                        CarrierID = _rcmdSLOTMAP_L_C.CARRIER_ID;
                                        SlotMapData.Set(LoadPortID, CarrierID);
                                    }

                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        txtMessageName.Text = e.ReceiveMessageName.ToString();
                                        txtOBJSPEC.Text = e.ObjSpec;
                                        txtRCMD.Text = e.RCMD;
                                        txtSystemBytes.Text = e.SystemBytes.ToString();
                                    });
                                }
                                break;
                            case "SLOTMAP-L":
                                {
                                    bWaitSECS_SlotMapCmd = true;
                                    RCMD_SLOTMAP_L obj = new RCMD_SLOTMAP_L();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdSLOTMAP_L = obj;
                                        LoadPortID = _rcmdSLOTMAP_L.LOADPORT_ID;
                                        SlotMapData.Set(LoadPortID);
                                    }

                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        txtMessageName.Text = e.ReceiveMessageName.ToString();
                                        txtOBJSPEC.Text = e.ObjSpec;
                                        txtRCMD.Text = e.RCMD;
                                        txtSystemBytes.Text = e.SystemBytes.ToString();
                                    });
                                }
                                break;
                            case "MEASURE-L&C":
                                {
                                    bWaitSECS_MeasureCmd = true;
                                    RCMD_MEASURE_L_C obj = new RCMD_MEASURE_L_C();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdMEASURE_L_C = obj;
                                        LoadPortID = _rcmdMEASURE_L_C.LOADPORT_ID;
                                        CarrierID = _rcmdMEASURE_L_C.CARRIER_ID;
                                        MeasureStartData.Set(LoadPortID, CarrierID);
                                    }

                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        txtMessageName.Text = e.ReceiveMessageName.ToString();
                                        txtOBJSPEC.Text = e.ObjSpec;
                                        txtRCMD.Text = e.RCMD;
                                        txtSystemBytes.Text = e.SystemBytes.ToString();
                                    });
                                }
                                break;
                            case "MEASURE-L":
                                {
                                    bWaitSECS_MeasureCmd = true;
                                    RCMD_MEASURE_L obj = new RCMD_MEASURE_L();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdMEASURE_L = obj;
                                        LoadPortID = _rcmdMEASURE_L.LOADPORT_ID;
                                        MeasureStartData.Set(LoadPortID);
                                    }

                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        txtMessageName.Text = e.ReceiveMessageName.ToString();
                                        txtOBJSPEC.Text = e.ObjSpec;
                                        txtRCMD.Text = e.RCMD;
                                        txtSystemBytes.Text = e.SystemBytes.ToString();
                                    });
                                }
                                break;
                            case "RELEASE-L&C":
                                {
                                    bWaitSECS_ReleaseCmd = true;
                                    RCMD_RELEASE_L_C obj = new RCMD_RELEASE_L_C();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdRRELEASE_L_C = obj;
                                        LoadPortID = _rcmdRRELEASE_L_C.LOADPORT_ID;
                                        CarrierID = _rcmdRRELEASE_L_C.CARRIER_ID;
                                        ReleaseData.Set(LoadPortID, CarrierID);
                                    }

                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        txtMessageName.Text = e.ReceiveMessageName.ToString();
                                        txtOBJSPEC.Text = e.ObjSpec;
                                        txtRCMD.Text = e.RCMD;
                                        txtSystemBytes.Text = e.SystemBytes.ToString();
                                    });
                                }
                                break;
                            case "RELEASE-L":
                                {
                                    bWaitSECS_ReleaseCmd = true;
                                    RCMD_RELEASE_L obj = new RCMD_RELEASE_L();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdRRELEASE_L = obj;
                                        LoadPortID = _rcmdRRELEASE_L.LOADPORT_ID;
                                        ReleaseData.Set(LoadPortID);
                                    }

                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        txtMessageName.Text = e.ReceiveMessageName.ToString();
                                        txtOBJSPEC.Text = e.ObjSpec;
                                        txtRCMD.Text = e.RCMD;
                                        txtSystemBytes.Text = e.SystemBytes.ToString();
                                    });
                                }
                                break;
                            case "PORT-TRANSFERSTATUS":
                                {
                                    bWaitSECS_PORT_TRANSFERSTATUS_ASK = true;
                                    RCMD_PORT_TRANSFERSTATUS_ASK obj = new RCMD_PORT_TRANSFERSTATUS_ASK();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdPORT_TRANSFERSTATUS_ASK = obj;
                                        LoadPortID = _rcmdPORT_TRANSFERSTATUS_ASK.LOADPORT_ID;
                                    }

                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        txtMessageName.Text = e.ReceiveMessageName.ToString();
                                        txtOBJSPEC.Text = e.ObjSpec;
                                        txtRCMD.Text = e.RCMD;
                                        txtSystemBytes.Text = e.SystemBytes.ToString();
                                    });
                                }
                                break;
                            case "CANCEL-L&C":
                                {
                                    bWaitSECS_CancelCmd = true;
                                    RCMD_CANCEL_L_C obj = new RCMD_CANCEL_L_C();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdCANCEL_L_C = obj;
                                        LoadPortID = _rcmdCANCEL_L_C.LOADPORT_ID;
                                        CarrierID = _rcmdCANCEL_L_C.CARRIER_ID;
                                        CancelData.Set(LoadPortID, CarrierID);
                                    }

                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        txtMessageName.Text = e.ReceiveMessageName.ToString();
                                        txtOBJSPEC.Text = e.ObjSpec;
                                        txtRCMD.Text = e.RCMD;
                                        txtSystemBytes.Text = e.SystemBytes.ToString();
                                    });
                                }
                                break;
                            case "CANCEL-L":
                                {
                                    bWaitSECS_CancelCmd = true;
                                    RCMD_CANCEL_L obj = new RCMD_CANCEL_L();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdCANCEL_L = obj;
                                        LoadPortID = _rcmdCANCEL_L.LOADPORT_ID;
                                        CancelData.Set(LoadPortID);
                                    }

                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        txtMessageName.Text = e.ReceiveMessageName.ToString();
                                        txtOBJSPEC.Text = e.ObjSpec;
                                        txtRCMD.Text = e.RCMD;
                                        txtSystemBytes.Text = e.SystemBytes.ToString();
                                    });
                                }
                                break;
                            //...case
                            //...
                            //...
                            default:
                                {
                                    foreach (CommandParameter obj in e.ReceiveCommandParameters)
                                    {
                                        this.Invoke((MethodInvoker)delegate ()
                                        {
                                            row = new string[] { obj.Name, obj.Value.ToString(), "0" };  // 定義一列的字串陣列
                                            dgvCPs.Rows.Add(row);           // 加入列  
                                        });
                                    }

                                    this.Invoke((MethodInvoker)delegate ()
                                    {
                                        txtMessageName.Text = e.ReceiveMessageName.ToString();
                                        txtOBJSPEC.Text = e.ObjSpec;
                                        txtRCMD.Text = e.RCMD;
                                        txtSystemBytes.Text = e.SystemBytes.ToString();
                                    });
                                }
                                break;
                        }
                    }
                    break;
                case eReceiveRemoteControlMessageName.S2F49:
                    {
                        switch (e.RCMD)
                        {
                            case "PP-SELECT":
                                {
                                    RCMD_PP_SELECT obj = new RCMD_PP_SELECT();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdPP_SELECT = obj;
                                    }
                                }
                                break;
                            case "TRANSFER":
                                {
                                    RCMD_TRANSFER obj = new RCMD_TRANSFER();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdTRANSFER = obj;
                                    }
                                }
                                break;
                            case "START":
                                {
                                    RCMD_START obj = new RCMD_START();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdSTART = obj;
                                    }
                                }
                                break;
                            case "GO":
                                {
                                    RCMD_GO obj = new RCMD_GO();
                                    bDataCheckOK = RunDecode(e, out obj);
                                    if (bDataCheckOK)
                                    {
                                        //OK, get Info
                                        _rcmdGO = obj;
                                    }
                                }
                                break;
                            //...case
                            //...
                            //...
                            default:
                                {

                                }
                                break;
                        }
                    }
                    break;
            }
            #endregion

            #region Check OK Use Keep Data


            //Sample handle
            if (e.ReceiveMessageName == eReceiveRemoteControlMessageName.S2F49)
            {
                btHCACK = bDataCheckOK == true ? (byte)0 : (byte)3;

                if (bDataCheckOK)
                {
                    //Sample view
                    SamplePrintCPsInfo(e.RCMD, e.SystemBytes);
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        txtEnhancedRemoteDataView.Text = $"SystemBytes: {e.SystemBytes} (NG)\r\n";
                    });
                }

                //Sample S2F50 Auto Reply
                
                _gemControler.RemoteCommandReply(eReceiveRemoteControlMessageName.S2F49, e.SystemBytes, btHCACK, e.ReceiveCommandParameters, out err);
            }
            else if(e.ReceiveMessageName == eReceiveRemoteControlMessageName.S2F41)
            {
                int a = _gemControler.RemoteCommandReply(eReceiveRemoteControlMessageName.S2F41, e.SystemBytes, btHCACK, e.ReceiveCommandParameters, out err);
            }
            #endregion

            //_gemControler.RemoteCommandAck(txtMessageName.Text, e.SystemBytes, byte.Parse(txtReplyHcAck.Text), e.ReceiveCommandParameters);
        }

        private void btnReplyMessage_Click(object sender, EventArgs e)
        {
            if (bCanReplyS2F42)
            {
                string ppid = string.Empty;
                List<CommandParameter> ReceiveCommandParameters = new List<CommandParameter>();

                for (int i = 0; i < dgvCPs.Rows.Count; i++)
                {
                    CommandParameter objCommandParameter = new CommandParameter();

                    string strName = dgvCPs.Rows[i].Cells[0].Value.ToString();

                    CommandParameter obj = objLastRecvCPs.FirstOrDefault(c => c.Name == strName);
                    if (obj != null)
                    {
                        var objTemp = obj.Clone();
                        objCommandParameter = (CommandParameter)objTemp;

                        objCommandParameter.Ack = byte.Parse(dgvCPs.Rows[i].Cells[2].Value.ToString());

                        if (objCommandParameter.Name == "PPID") ppid = dgvCPs.Rows[i].Cells[1].Value.ToString();

                        if ((byte)objCommandParameter.Ack == 0) continue;
                        ReceiveCommandParameters.Add(objCommandParameter);
                    }
                    else
                    {
                        objCommandParameter.Name = strName;
                        objCommandParameter.Ack = (byte)3;
                        ReceiveCommandParameters.Add(objCommandParameter);
                    }
                }

                string err;
                _gemControler.RemoteCommandReply(txtMessageName.Text == "S2F41" ? eReceiveRemoteControlMessageName.S2F41 : eReceiveRemoteControlMessageName.S2F49,
                    uint.Parse(txtSystemBytes.Text), byte.Parse(txtReplyHcAck.Text), ReceiveCommandParameters, out err);
                bCanReplyS2F42 = false;
                objLastRecvCPs = null;

                if (txtRCMD.Text.Trim() == "PP-SELECT" && txtReplyHcAck.Text.Trim() == "0")
                {
                    string errLog = string.Empty;
                    //SYS_PP_EXEC_NAME
                    _gemControler.UpdateSV(14, ppid, out errLog);

                    //Process_Program_Selected Event
                    uint ceid = 9;
                    int result = _gemControler.EventReportSend(ceid, out err);
                    if (result != 0)
                    {
                        string log = string.Format("EventReport CEID({0}) Failure, ({1})Reason:{2}", ceid, result, err);
                        WriteLog(LogLevel.Warn, log);
                    }
                }
            }
            else
            {
                string log = string.Format("Can't Reply Command, Because Not Receive Command!");
                WriteLog(LogLevel.Warn, log);
                MessageBox.Show(log);
            }
        }

        private void SamplePrintCPsInfo(string strRCMD, uint uiSystemBytes)
        {
            string strPrintInfo = string.Empty;
            strPrintInfo += $"SystemBytes: {uiSystemBytes} (OK)\r\n";
            switch (strRCMD)
            {
                case "PP-SELECT":
                    {
                        //RCMD: PP-SELECT
                        // LOADPORT_ID,ASCII
                        // PPID,ASCII
                        // TAG,LIST
                        // COMMANDINFO,LIST
                        // TRANSFERINFO,LIST
                        strPrintInfo += $"RCMD: {strRCMD}\r\n";
                        strPrintInfo += $" LOADPORT_ID: {_rcmdPP_SELECT.LOADPORT_ID}\r\n";
                        //strPrintInfo += $" PPID: {_rcmdPP_SELECT.PPID}\r\n";
                        strPrintInfo += $" TAG:\r\n";
                        //strPrintInfo += $"  Tag1: {_rcmdPP_SELECT.TAG.Tag1}\r\n";
                        //strPrintInfo += $"  Tag2: {_rcmdPP_SELECT.TAG.Tag2}\r\n";
                        //strPrintInfo += $"  Tag3: {_rcmdPP_SELECT.TAG.Tag3}\r\n";
                        strPrintInfo += $" COMMANDINFO:\r\n";
                        //strPrintInfo += $"  COMMANDID: {_rcmdPP_SELECT.COMMANDINFO.COMMANDID}\r\n";
                        //strPrintInfo += $"  PRIORITY: {_rcmdPP_SELECT.COMMANDINFO.PRIORITY}\r\n";
                        strPrintInfo += $" TRANSFERINFO:\r\n";
                        //strPrintInfo += $"  CARRIERID: {_rcmdPP_SELECT.TRANSFERINFO.CARRIERID}\r\n";
                        //strPrintInfo += $"  SOURCE: {_rcmdPP_SELECT.TRANSFERINFO.SOURCE}\r\n";
                        //strPrintInfo += $"  DEST: {_rcmdPP_SELECT.TRANSFERINFO.DEST}\r\n";
                    }
                    break;
                case "TRANSFER":
                    {
                        //RCMD: TRANSFER
                        // COMMANDINFO,LIST
                        // TRANSFERINFO,LIST
                        strPrintInfo += $"RCMD: {strRCMD}\r\n";
                        strPrintInfo += $" COMMANDINFO:\r\n";
                        strPrintInfo += $"  COMMANDID: {_rcmdTRANSFER.COMMANDINFO.COMMANDID}\r\n";
                        strPrintInfo += $"  PRIORITY: {_rcmdTRANSFER.COMMANDINFO.PRIORITY}\r\n";
                        strPrintInfo += $" TRANSFERINFO:\r\n";
                        strPrintInfo += $"  CARRIERID: {_rcmdTRANSFER.TRANSFERINFO.CARRIERID}\r\n";
                        strPrintInfo += $"  SOURCE: {_rcmdTRANSFER.TRANSFERINFO.SOURCE}\r\n";
                        strPrintInfo += $"  DEST: {_rcmdTRANSFER.TRANSFERINFO.DEST}\r\n";
                    }
                    break;
                case "START":
                    {
                        //RCMD: START
                        // LOADPORT_ID,ASCII
                        // PPID,ASCII
                        strPrintInfo += $"RCMD: {strRCMD}\r\n";
                        //strPrintInfo += $" LOADPORT_ID: {_rcmdSTART.LOADPORT_ID}\r\n";
                        //strPrintInfo += $" PPID: {_rcmdSTART.PPID}\r\n";
                    }
                    break;
                case "GO":
                    {
                        //RCMD: PP-SELECT
                        // LOADPORT_ID,ASCII
                        // PPID,ASCII
                        // TAG,LIST
                        strPrintInfo += $"RCMD: {strRCMD}\r\n";
                        strPrintInfo += $" LOADPORT_ID: {_rcmdGO.LOADPORT_ID}\r\n";
                        strPrintInfo += $" PPID: {_rcmdGO.PPID}\r\n";
                        strPrintInfo += $" TAG:\r\n";
                        strPrintInfo += $"  Tag1: {_rcmdGO.TAG.Tag1}\r\n";
                        strPrintInfo += $"  Tag2: {_rcmdGO.TAG.Tag2}\r\n";
                        strPrintInfo += $"  Tag3: {_rcmdGO.TAG.Tag3}\r\n";
                    }
                    break;
            }

            this.Invoke((MethodInvoker)delegate ()
            {
                txtEnhancedRemoteDataView.Text = strPrintInfo;
            });
        }

        //GEM Process Program----------------------------------------------------------------------------------------------------
        //>>EQP Device Update PPID BODY (Unformatted)
        private void btnSendS7F3_Click(object sender, EventArgs e)
        {
            txtS7Systembytes.Text = string.Empty;
            txtAckc7.Text = string.Empty;
            txtReceivedPPID.Text = string.Empty;

            string msg = string.Empty;
            string ppid = txtPPBodyPPID.Text.Trim();
            if (string.IsNullOrWhiteSpace(ppid))
            {
                msg = "Miss PPID Data";
                MessageBox.Show(msg);
                return;
            }

            string err;
            PPBody ppBody = _ppManager.GetPPBody(ppid);
            if (ppBody == null)
            {
                msg = string.Format("PPID({0}) is not exist", ppid);
                MessageBox.Show(msg);
                return;
            }

            ProcessProgramData ppData = ppBody.GetPPBody_Unformatted();
            _gemControler.ProcessProgramSend(ppData, 0, out err);
        }
        //>>EQP Device Update PPID BODY (Formatted)
        private void btnSendS7F23_Click(object sender, EventArgs e)
        {
            txtS7Systembytes.Text = string.Empty;
            txtAckc7.Text = string.Empty;
            txtReceivedPPID.Text = string.Empty;

            string msg = string.Empty;
            string ppid = txtPPBodyPPID.Text.Trim();
            if (string.IsNullOrWhiteSpace(ppid))
            {
                msg = "Miss PPID Data";
                MessageBox.Show(msg);
                return;
            }

            PPBody ppBody = _ppManager.GetPPBody(ppid);
            if (ppBody == null)
            {
                msg = string.Format("PPID({0}) is not exist", ppid);
                MessageBox.Show(msg);
                return;
            }

            string err;
            //FormattedProcessProgramData ppData = ppBody.GetPPBody_Formatted(_modelType, _softRev);
            //_gemControler.ProcessProgramSend(ppData, 0, out err);

            FormattedProcessProgramData2 ppData = ppBody.GetPPBody_Formatted2(_modelType, _softRev);
            _gemControler.ProcessProgramSend2(ppData, 0, out err);
        }
        //>>EQP Device Update PPID BODY, Host Reply Result (Unformatted/Formatted)
        private void _gemControler_ProcessProgramSendReply(object sender, ProcessProgramSendReplyArgs e)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                txtS7Systembytes.Text = string.Empty;
                txtAckc7.Text = e.Ackc.ToString();
                txtReceivedPPID.Text = string.Empty;
            });

            string log = string.Empty;
            if (e.Type == eHostPPSendReplyType.ProcessProgram)
            {
                log = "Host Reply S7F4";
                WriteLog(LogLevel.Info, log);
            }

            if (e.Type == eHostPPSendReplyType.FormattedProcessProgram)
            {
                log = "Host Reply S7F24";
                WriteLog(LogLevel.Info, log);
            }
        }

        //>>EQP Device Qurey PPID BODY (Unformatted)
        private void btnSendS7F5_Click(object sender, EventArgs e)
        {
            txtS7Systembytes.Text = string.Empty;
            txtAckc7.Text = string.Empty;
            txtReceivedPPID.Text = string.Empty;

            string msg = string.Empty;
            string ppid = txtPPBodyPPID.Text.Trim();
            if (string.IsNullOrWhiteSpace(ppid))
            {
                msg = "Miss PPID Data";
                MessageBox.Show(msg);
                return;
            }

            string err;
            _gemControler.ProcessProgramRequest(ePPRequestType.ProcessProgramDownload, ppid, out err);
        }
        //>>EQP Device Qurey PPID BODY, Host Reply Result (Unformatted)
        private void _gemControler_ProcessProgramDownload(object sender, ProcessProgramDownloadArgs e)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                txtS7Systembytes.Text = e.SystemBytes.ToString();
                txtAckc7.Text = string.Empty;
                txtReceivedPPID.Text = e.PPData.PPID;
            });

            UpdateProcessProgram_Unformatted(e.PPData, e.SystemBytes);
        }
        //Case1: SystemBytes = 0, Handle Host Reply Result (Unformatted)
        //Case2: SystemBytes != 0, Handle Host Qurey PPID BODY (Unformatted)
        private void UpdateProcessProgram_Unformatted(ProcessProgramData ppData, uint systemBytes)
        {
            byte ackc7 = 0;
            byte[] byteBody;
            string err;

            if (systemBytes != 0)
            {
                #region Handle S7F3
                if (_ppBodyType == PPBodyType.Unformatted || _ppBodyType == PPBodyType.Both)
                {
                    if (ppData.Body.GetType() != typeof(byte[]))
                    {
                        //data type error
                        ackc7 = 7;
                    }
                    else
                    {
                        byteBody = (byte[])ppData.Body;

                        if (byteBody.Length != 200)
                        {
                            ackc7 = 2;
                        }
                        else
                        {
                            string ppid = ppData.PPID.Trim();
                            PPBody ppBody = new PPBody();
                            //ppBody.Tank1_WaterPressure = BitConverter.ToSingle(byteBody, 0);
                            //ppBody.Tank1_Voltage = BitConverter.ToInt32(byteBody, 4);
                            //ppBody.Tank1_AirPressure = BitConverter.ToSingle(byteBody, 8);
                            //ppBody.Tank2_WaterPressure = BitConverter.ToSingle(byteBody, 100);
                            //ppBody.Tank2_Voltage = BitConverter.ToInt32(byteBody, 104);
                            //ppBody.Tank2_AirPressure = BitConverter.ToSingle(byteBody, 108);
                            ppBody.Rotate_Count = (byte)BitConverter.ToUInt32(byteBody, 0);
                            ppBody.Type = (byte)BitConverter.ToUInt32(byteBody, 1);
                            ppBody.RepeatTimes = (byte)BitConverter.ToUInt32(byteBody, 2);
                            ppBody.RepeatTimes_now = (byte)BitConverter.ToUInt32(byteBody, 3);
                            ppBody.Slot1 = (byte)BitConverter.ToUInt32(byteBody, 4);
                            ppBody.Slot2 = (byte)BitConverter.ToUInt32(byteBody, 5);
                            ppBody.Slot3 = (byte)BitConverter.ToUInt32(byteBody, 6);
                            ppBody.Slot4 = (byte)BitConverter.ToUInt32(byteBody, 7);
                            ppBody.Slot5 = (byte)BitConverter.ToUInt32(byteBody, 8);
                            ppBody.Slot6 = (byte)BitConverter.ToUInt32(byteBody, 9);
                            ppBody.Slot7 = (byte)BitConverter.ToUInt32(byteBody, 10);
                            ppBody.Slot8 = (byte)BitConverter.ToUInt32(byteBody, 11);
                            ppBody.Slot9 = (byte)BitConverter.ToUInt32(byteBody, 12);
                            ppBody.Slot10 = (byte)BitConverter.ToUInt32(byteBody, 13);
                            ppBody.Slot11 = (byte)BitConverter.ToUInt32(byteBody, 14);
                            ppBody.Slot12 = (byte)BitConverter.ToUInt32(byteBody, 15);
                            ppBody.Slot13 = (byte)BitConverter.ToUInt32(byteBody, 16);
                            ppBody.Slot14 = (byte)BitConverter.ToUInt32(byteBody, 17);
                            ppBody.Slot15 = (byte)BitConverter.ToUInt32(byteBody, 18);
                            ppBody.Slot16 = (byte)BitConverter.ToUInt32(byteBody, 19);
                            ppBody.Slot17 = (byte)BitConverter.ToUInt32(byteBody, 20);
                            ppBody.Slot18 = (byte)BitConverter.ToUInt32(byteBody, 21);
                            ppBody.Slot19 = (byte)BitConverter.ToUInt32(byteBody, 22);
                            ppBody.Slot20 = (byte)BitConverter.ToUInt32(byteBody, 23);
                            ppBody.Slot21 = (byte)BitConverter.ToUInt32(byteBody, 24);
                            ppBody.Slot22 = (byte)BitConverter.ToUInt32(byteBody, 25);
                            ppBody.Slot23 = (byte)BitConverter.ToUInt32(byteBody, 26);
                            ppBody.Slot24 = (byte)BitConverter.ToUInt32(byteBody, 27);
                            ppBody.Slot25 = (byte)BitConverter.ToUInt32(byteBody, 28);
                            ppBody.Angle1 = (ushort)BitConverter.ToUInt32(byteBody, 29);
                            ppBody.Angle2 = (ushort)BitConverter.ToUInt32(byteBody, 31);
                            ppBody.Angle3 = (ushort)BitConverter.ToUInt32(byteBody, 33);
                            ppBody.Angle4 = (ushort)BitConverter.ToUInt32(byteBody, 35);
                            ppBody.Angle5 = (ushort)BitConverter.ToUInt32(byteBody, 37);
                            ppBody.Angle6 = (ushort)BitConverter.ToUInt32(byteBody, 39);
                            ppBody.Angle7 = (ushort)BitConverter.ToUInt32(byteBody, 41);
                            ppBody.Angle8 = (ushort)BitConverter.ToUInt32(byteBody, 43);
                            //ppBody.CreateTime = ;
                            //ppBody.ReviseTime = ;
                            //ppBody.OffsetType = ;

                            _ppManager.AddorUpdatePPID(ppid, ppBody);
                            ReflashPPIDList();
                        }
                    }
                }
                else
                {
                    //Mode unsupported 
                    ackc7 = 5;
                }
                _gemControler.ProcessProgramReply(ePPAckType.HostDownloadProcessProgram, ackc7, systemBytes, out err);
                #endregion
            }
            else  //S7F6
            {
                #region Handle S7F6
                if (_ppBodyType == PPBodyType.Unformatted || _ppBodyType == PPBodyType.Both)
                {
                    if (ppData.Body.GetType() != typeof(byte[]))
                    {
                        //data type error
                        WriteLog(LogLevel.Warn, "S7F6 PPBody Type Error.");
                    }
                    else
                    {
                        byteBody = (byte[])ppData.Body;

                        if (byteBody.Length == 0)
                        {
                            WriteLog(LogLevel.Warn, "S7F5 request was denied");
                        }
                        else if (byteBody.Length != 200)
                        {
                            WriteLog(LogLevel.Warn, "S7F6 PPBody Length error.");
                        }
                        else
                        {
                            string ppid = ppData.PPID.Trim();
                            PPBody ppBody = new PPBody();
                            //ppBody.Tank1_WaterPressure = BitConverter.ToSingle(byteBody, 0);
                            //ppBody.Tank1_Voltage = BitConverter.ToInt32(byteBody, 4);
                            //ppBody.Tank1_AirPressure = BitConverter.ToSingle(byteBody, 8);
                            //ppBody.Tank2_WaterPressure = BitConverter.ToSingle(byteBody, 100);
                            //ppBody.Tank2_Voltage = BitConverter.ToInt32(byteBody, 104);
                            //ppBody.Tank2_AirPressure = BitConverter.ToSingle(byteBody, 108);
                            ppBody.Rotate_Count = (byte)BitConverter.ToUInt32(byteBody, 0);
                            ppBody.Type = (byte)BitConverter.ToUInt32(byteBody, 1);
                            ppBody.RepeatTimes = (byte)BitConverter.ToUInt32(byteBody, 2);
                            ppBody.RepeatTimes_now = (byte)BitConverter.ToUInt32(byteBody, 3);
                            ppBody.Slot1 = (byte)BitConverter.ToUInt32(byteBody, 4);
                            ppBody.Slot2 = (byte)BitConverter.ToUInt32(byteBody, 5);
                            ppBody.Slot3 = (byte)BitConverter.ToUInt32(byteBody, 6);
                            ppBody.Slot4 = (byte)BitConverter.ToUInt32(byteBody, 7);
                            ppBody.Slot5 = (byte)BitConverter.ToUInt32(byteBody, 8);
                            ppBody.Slot6 = (byte)BitConverter.ToUInt32(byteBody, 9);
                            ppBody.Slot7 = (byte)BitConverter.ToUInt32(byteBody, 10);
                            ppBody.Slot8 = (byte)BitConverter.ToUInt32(byteBody, 11);
                            ppBody.Slot9 = (byte)BitConverter.ToUInt32(byteBody, 12);
                            ppBody.Slot10 = (byte)BitConverter.ToUInt32(byteBody, 13);
                            ppBody.Slot11 = (byte)BitConverter.ToUInt32(byteBody, 14);
                            ppBody.Slot12 = (byte)BitConverter.ToUInt32(byteBody, 15);
                            ppBody.Slot13 = (byte)BitConverter.ToUInt32(byteBody, 16);
                            ppBody.Slot14 = (byte)BitConverter.ToUInt32(byteBody, 17);
                            ppBody.Slot15 = (byte)BitConverter.ToUInt32(byteBody, 18);
                            ppBody.Slot16 = (byte)BitConverter.ToUInt32(byteBody, 19);
                            ppBody.Slot17 = (byte)BitConverter.ToUInt32(byteBody, 20);
                            ppBody.Slot18 = (byte)BitConverter.ToUInt32(byteBody, 21);
                            ppBody.Slot19 = (byte)BitConverter.ToUInt32(byteBody, 22);
                            ppBody.Slot20 = (byte)BitConverter.ToUInt32(byteBody, 23);
                            ppBody.Slot21 = (byte)BitConverter.ToUInt32(byteBody, 24);
                            ppBody.Slot22 = (byte)BitConverter.ToUInt32(byteBody, 25);
                            ppBody.Slot23 = (byte)BitConverter.ToUInt32(byteBody, 26);
                            ppBody.Slot24 = (byte)BitConverter.ToUInt32(byteBody, 27);
                            ppBody.Slot25 = (byte)BitConverter.ToUInt32(byteBody, 28);
                            ppBody.Angle1 = (ushort)BitConverter.ToUInt32(byteBody, 29);
                            ppBody.Angle2 = (ushort)BitConverter.ToUInt32(byteBody, 31);
                            ppBody.Angle3 = (ushort)BitConverter.ToUInt32(byteBody, 33);
                            ppBody.Angle4 = (ushort)BitConverter.ToUInt32(byteBody, 35);
                            ppBody.Angle5 = (ushort)BitConverter.ToUInt32(byteBody, 37);
                            ppBody.Angle6 = (ushort)BitConverter.ToUInt32(byteBody, 39);
                            ppBody.Angle7 = (ushort)BitConverter.ToUInt32(byteBody, 41);
                            ppBody.Angle8 = (ushort)BitConverter.ToUInt32(byteBody, 43);
                            //ppBody.CreateTime = ;
                            //ppBody.ReviseTime = ;
                            //ppBody.OffsetType = ;

                            _ppManager.AddorUpdatePPID(ppid, ppBody);
                            ReflashPPIDList();
                        }
                    }
                }
                else
                {
                    WriteLog(LogLevel.Warn, "Mode unsupported.");
                }
                #endregion
            }
        }

        //>>EQP Device Qurey PPID BODY (Formatted)
        private void btnSendS7F25_Click(object sender, EventArgs e)
        {
            txtS7Systembytes.Text = string.Empty;
            txtAckc7.Text = string.Empty;
            txtReceivedPPID.Text = string.Empty;

            string msg = string.Empty;
            string ppid = txtPPBodyPPID.Text.Trim();
            if (string.IsNullOrWhiteSpace(ppid))
            {
                msg = "Miss PPID Data";
                MessageBox.Show(msg);
                return;
            }

            string err;
            _gemControler.ProcessProgramRequest(ePPRequestType.FormattedProcessProgramDownload, ppid, out err);
        }
        //>>EQP Device Qurey PPID BODY, Host Reply Result (Formatted, CCODE U2 Format)
        private void _gemControler_FormattedProcessProgramDownload(object sender, FormattedProcessProgramDownloadArgs e)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                txtS7Systembytes.Text = e.SystemBytes.ToString();
                txtAckc7.Text = string.Empty;
                txtReceivedPPID.Text = e.PPData.PPID;
            });

            UpdateProcessProgram_Formatted(e.PPData, e.SystemBytes);
        }
        //Case1: SystemBytes = 0, Handle Host Reply Result (Formatted)
        //Case2: SystemBytes != 0, Handle Host Send PPID BODY (Formatted)
        private void UpdateProcessProgram_Formatted(FormattedProcessProgramData ppData, uint systemBytes)
        {
            byte ackc7 = 0;
            string err;

            List<ushort> notDealWithCCode = new List<ushort> { 100, 200 };
            string ppid = ppData.PPID.Trim();
            PPBody ppBody = new PPBody();

            if (systemBytes != 0) //S7F23
            {
                #region Handle S7F23
                if (_ppBodyType == PPBodyType.Formatted || _ppBodyType == PPBodyType.Both)
                {
                    ProcessProgramVerification verification = CheckFormattedProcessProgramData(ppData, out ppBody);

                    _gemControler.ProcessProgramReply(ePPAckType.HostDownloadFormattedProcessProgram, ackc7, systemBytes, out err);

                    if (verification.ErrorMessages.Count == 0)
                    {
                        _ppManager.AddorUpdatePPID(ppid, ppBody);
                        ReflashPPIDList();
                    }
                    else
                    {
                        string errLog = string.Empty;
                        string ppErr = verification.ErrorMessages[0].ErrorDescription;
                        //SYS_PP_ERROR
                        _gemControler.UpdateSV(45, ppErr, out errLog);
                    }

                    _gemControler.ProcessProgramVerificationSend(verification, out err);
                }
                else
                {
                    //Mode unsupported 
                    ackc7 = 5;

                    _gemControler.ProcessProgramReply(ePPAckType.HostDownloadFormattedProcessProgram, ackc7, systemBytes, out err);
                }
                #endregion
            }
            else //S7F26
            {
                #region Handle S7F26
                if (_ppBodyType == PPBodyType.Formatted || _ppBodyType == PPBodyType.Both)
                {
                    if (ppData.CCodes.Count != 0)
                    {
                        ProcessProgramVerification verification = CheckFormattedProcessProgramData(ppData, out ppBody);

                        if (verification.ErrorMessages.Count == 0)
                        {
                            _ppManager.AddorUpdatePPID(ppid, ppBody);
                            ReflashPPIDList();
                        }
                        else
                        {
                            string errLog = string.Empty;
                            string ppErr = verification.ErrorMessages[0].ErrorDescription;
                            //SYS_PP_ERROR
                            _gemControler.UpdateSV(45, ppErr, out errLog);
                        }

                        _gemControler.ProcessProgramVerificationSend(verification, out err);
                    }
                    else
                    {
                        //Mode unsupported 
                        WriteLog(LogLevel.Warn, "S7F25 request was denied");
                    }
                }
                else
                {
                    WriteLog(LogLevel.Warn, "Mode unsupported");
                }
                #endregion
            }
        }
        private ProcessProgramVerification CheckFormattedProcessProgramData(FormattedProcessProgramData ppData, out PPBody ppBody)
        {
            ppBody = new PPBody();
            List<ushort> notDealWithCCode = new List<ushort> { 100 };
            ProcessProgramVerification verification = new ProcessProgramVerification();
            verification.PPID = ppData.PPID.Trim();

            if (ppData.MDLN.Trim() != _modelType)
            {
                VerificationErrorMessage msg = new VerificationErrorMessage();
                msg.AcknowledgeCode = 1;
                msg.CommandNumber = 0;
                msg.ErrorDescription = "MDLN is inconsistent";
                verification.ErrorMessages.Add(msg);
            }

            if (ppData.SOFTREV.Trim() != _softRev)
            {
                VerificationErrorMessage msg = new VerificationErrorMessage();
                msg.AcknowledgeCode = 2;
                msg.CommandNumber = 0;
                msg.ErrorDescription = "SOFTREV is inconsistent";
                verification.ErrorMessages.Add(msg);
            }

            foreach (CommandCode commandCode in ppData.CCodes)
            {
                notDealWithCCode.Remove(commandCode.CCode);

                switch (commandCode.CCode)
                {
                    case 100:
                        #region CCode 100
                        if (commandCode.Parameters.Count != paramCount)
                        {
                            VerificationErrorMessage msg = new VerificationErrorMessage();
                            msg.AcknowledgeCode = 5;
                            msg.CommandNumber = 100;
                            msg.ErrorDescription = "PPARM Count is not equal"+ paramCount;
                            verification.ErrorMessages.Add(msg);
                        }
                        else
                        {
                            if (commandCode.Parameters[0].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Rotate_Count Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Rotate_Count = (byte)commandCode.Parameters[0].Value;
                            }

                            if (commandCode.Parameters[1].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Type Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Type = (byte)commandCode.Parameters[1].Value;
                            }

                            if (commandCode.Parameters[2].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "RepeatTimes Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.RepeatTimes = (byte)commandCode.Parameters[2].Value;
                            }

                            if (commandCode.Parameters[3].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "RepeatTimes_now Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.RepeatTimes_now = (byte)commandCode.Parameters[3].Value;
                            }

                            if (commandCode.Parameters[4].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot1 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot1 = (byte)commandCode.Parameters[4].Value;
                            }

                            if (commandCode.Parameters[5].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot2 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot2 = (byte)commandCode.Parameters[5].Value;
                            }

                            if (commandCode.Parameters[6].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot3 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot3 = (byte)commandCode.Parameters[6].Value;
                            }

                            if (commandCode.Parameters[7].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot4 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot4 = (byte)commandCode.Parameters[7].Value;
                            }

                            if (commandCode.Parameters[8].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot5 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot5 = (byte)commandCode.Parameters[8].Value;
                            }

                            if (commandCode.Parameters[9].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot6 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot6 = (byte)commandCode.Parameters[9].Value;
                            }

                            if (commandCode.Parameters[10].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot7 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot7 = (byte)commandCode.Parameters[10].Value;
                            }

                            if (commandCode.Parameters[11].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot8 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot8 = (byte)commandCode.Parameters[11].Value;
                            }

                            if (commandCode.Parameters[12].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot9 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot9 = (byte)commandCode.Parameters[12].Value;
                            }

                            if (commandCode.Parameters[13].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot10 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot10 = (byte)commandCode.Parameters[13].Value;
                            }

                            if (commandCode.Parameters[14].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot11 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot11 = (byte)commandCode.Parameters[14].Value;
                            }

                            if (commandCode.Parameters[15].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot12 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot12 = (byte)commandCode.Parameters[15].Value;
                            }

                            if (commandCode.Parameters[16].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot13 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot13 = (byte)commandCode.Parameters[16].Value;
                            }

                            if (commandCode.Parameters[17].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot14 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot14 = (byte)commandCode.Parameters[17].Value;
                            }

                            if (commandCode.Parameters[18].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot15 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot15 = (byte)commandCode.Parameters[18].Value;
                            }

                            if (commandCode.Parameters[19].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot16 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot16 = (byte)commandCode.Parameters[19].Value;
                            }

                            if (commandCode.Parameters[20].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot17 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot17 = (byte)commandCode.Parameters[20].Value;
                            }

                            if (commandCode.Parameters[21].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot18 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot18 = (byte)commandCode.Parameters[21].Value;
                            }

                            if (commandCode.Parameters[22].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot19 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot19 = (byte)commandCode.Parameters[22].Value;
                            }

                            if (commandCode.Parameters[23].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot20 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot20 = (byte)commandCode.Parameters[23].Value;
                            }

                            if (commandCode.Parameters[24].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot21 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot21 = (byte)commandCode.Parameters[24].Value;
                            }

                            if (commandCode.Parameters[25].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot22 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot22 = (byte)commandCode.Parameters[25].Value;
                            }

                            if (commandCode.Parameters[26].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot23 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot23 = (byte)commandCode.Parameters[26].Value;
                            }

                            if (commandCode.Parameters[27].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot24 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot24 = (byte)commandCode.Parameters[27].Value;
                            }

                            if (commandCode.Parameters[28].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot25 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot25 = (byte)commandCode.Parameters[28].Value;
                            }

                            if (commandCode.Parameters[29].Format != ItemFmt.U2)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle1 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Angle1 = (ushort)commandCode.Parameters[29].Value;
                            }

                            if (commandCode.Parameters[30].Format != ItemFmt.U2)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle2 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Angle2 = (ushort)commandCode.Parameters[30].Value;
                            }

                            if (commandCode.Parameters[31].Format != ItemFmt.U2)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle3 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Angle3 = (ushort)commandCode.Parameters[31].Value;
                            }

                            if (commandCode.Parameters[32].Format != ItemFmt.U2)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle4 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Angle4 = (ushort)commandCode.Parameters[32].Value;
                            }

                            if (commandCode.Parameters[33].Format != ItemFmt.U2)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle5 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Angle5 = (ushort)commandCode.Parameters[33].Value;
                            }

                            if (commandCode.Parameters[34].Format != ItemFmt.U2)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle6 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Angle6 = (ushort)commandCode.Parameters[34].Value;
                            }

                            if (commandCode.Parameters[35].Format != ItemFmt.U2)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle7 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Angle7 = (ushort)commandCode.Parameters[35].Value;
                            }

                            if (commandCode.Parameters[36].Format != ItemFmt.U2)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle8 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Angle8 = (ushort)commandCode.Parameters[36].Value;
                            }

                            if (commandCode.Parameters[37].Format != ItemFmt.A)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "CreateTime Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.CreateTime = (string)commandCode.Parameters[37].Value;
                            }

                            if (commandCode.Parameters[38].Format != ItemFmt.A)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.ReviseTime = (string)commandCode.Parameters[38].Value;
                            }

                            if (commandCode.Parameters[39].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.OffsetType = (byte)commandCode.Parameters[39].Value;
                            }
                        }
                        #endregion
                        break;
                    //case 100:
                    //    #region CCode 100
                    //    if (commandCode.Parameters.Count != 3)
                    //    {
                    //        VerificationErrorMessage msg = new VerificationErrorMessage();
                    //        msg.AcknowledgeCode = 5;
                    //        msg.CommandNumber = 100;
                    //        msg.ErrorDescription = "PPARM Count is not equal 3";
                    //        verification.ErrorMessages.Add(msg);
                    //    }
                    //    else
                    //    {
                    //        if (commandCode.Parameters[0].Format != ItemFmt.F4)
                    //        {
                    //            VerificationErrorMessage msg = new VerificationErrorMessage();
                    //            msg.AcknowledgeCode = 4;
                    //            msg.CommandNumber = 100;
                    //            msg.ErrorDescription = "Tank1_WaterPressure Format error";
                    //            verification.ErrorMessages.Add(msg);
                    //        }
                    //        else
                    //        {
                    //            ppBody.Tank1_WaterPressure = (float)commandCode.Parameters[0].Value;
                    //        }

                    //        if (commandCode.Parameters[1].Format != ItemFmt.I4)
                    //        {
                    //            VerificationErrorMessage msg = new VerificationErrorMessage();
                    //            msg.AcknowledgeCode = 4;
                    //            msg.CommandNumber = 100;
                    //            msg.ErrorDescription = "Tank1_Voltage Format error";
                    //            verification.ErrorMessages.Add(msg);
                    //        }
                    //        else
                    //        {
                    //            ppBody.Tank1_Voltage = (int)commandCode.Parameters[1].Value;
                    //        }

                    //        if (commandCode.Parameters[2].Format != ItemFmt.F4)
                    //        {
                    //            VerificationErrorMessage msg = new VerificationErrorMessage();
                    //            msg.AcknowledgeCode = 4;
                    //            msg.CommandNumber = 100;
                    //            msg.ErrorDescription = "Tank1_AirPressure Format error";
                    //            verification.ErrorMessages.Add(msg);
                    //        }
                    //        else
                    //        {
                    //            ppBody.Tank1_AirPressure = (float)commandCode.Parameters[2].Value;
                    //        }
                    //    }
                    //    #endregion
                    //    break;
                        //case 200:
                        //    #region CCode 200
                        //    if (commandCode.Parameters.Count != 3)
                        //    {
                        //        VerificationErrorMessage msg = new VerificationErrorMessage();
                        //        msg.AcknowledgeCode = 5;
                        //        msg.CommandNumber = 200;
                        //        msg.ErrorDescription = "PPARM Count is not equal 3";
                        //        verification.ErrorMessages.Add(msg);
                        //    }
                        //    else
                        //    {
                        //        if (commandCode.Parameters[0].Format != ItemFmt.F4)
                        //        {
                        //            VerificationErrorMessage msg = new VerificationErrorMessage();
                        //            msg.AcknowledgeCode = 4;
                        //            msg.CommandNumber = 100;
                        //            msg.ErrorDescription = "Tank2_WaterPressure Format error";
                        //            verification.ErrorMessages.Add(msg);
                        //        }
                        //        else
                        //        {
                        //            ppBody.Tank2_WaterPressure = (float)commandCode.Parameters[0].Value;
                        //        }

                        //        if (commandCode.Parameters[1].Format != ItemFmt.I4)
                        //        {
                        //            VerificationErrorMessage msg = new VerificationErrorMessage();
                        //            msg.AcknowledgeCode = 4;
                        //            msg.CommandNumber = 200;
                        //            msg.ErrorDescription = "Tank2_Voltage Format error";
                        //            verification.ErrorMessages.Add(msg);
                        //        }
                        //        else
                        //        {
                        //            ppBody.Tank2_Voltage = (int)commandCode.Parameters[1].Value;
                        //        }

                        //        if (commandCode.Parameters[2].Format != ItemFmt.F4)
                        //        {
                        //            VerificationErrorMessage msg = new VerificationErrorMessage();
                        //            msg.AcknowledgeCode = 4;
                        //            msg.CommandNumber = 200;
                        //            msg.ErrorDescription = "Tank2_AirPressure Format error";
                        //            verification.ErrorMessages.Add(msg);
                        //        }
                        //        else
                        //        {
                        //            ppBody.Tank2_AirPressure = (float)commandCode.Parameters[2].Value;
                        //        }
                        //    }
                        //    #endregion
                        //    break;
                }
            }

            if (notDealWithCCode.Count > 0)
            {
                string missCCodes = string.Join(",", notDealWithCCode);
                VerificationErrorMessage msg = new VerificationErrorMessage();
                msg.AcknowledgeCode = 6;
                msg.CommandNumber = 0;
                msg.ErrorDescription = string.Format("Miss CCode({0}) Data", missCCodes);
                verification.ErrorMessages.Add(msg);
            }

            return verification;
        }

        //>>EQP Device Qurey PPID BODY, Host Reply Result (Formatted, CCODE A/U2/U4/I2/I4 Format)
        private void _gemControler_FormattedProcessProgramDownload2(object sender, FormattedProcessProgramDownloadArgs2 e)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                txtS7Systembytes.Text = e.SystemBytes.ToString();
                txtAckc7.Text = string.Empty;
                txtReceivedPPID.Text = e.PPData.PPID;
            });

            UpdateProcessProgram_Formatted2(e.PPData, e.SystemBytes);
        }
        //Case1: SystemBytes = 0, Handle Host Reply Result (Formatted)
        //Case2: SystemBytes != 0, Handle Host Send PPID BODY (Formatted)
        private void UpdateProcessProgram_Formatted2(FormattedProcessProgramData2 ppData, uint systemBytes)
        {
            byte ackc7 = 0;
            string err;

            List<string> notDealWithCCode = new List<string> { "100", "200" };
            string ppid = ppData.PPID.Trim();
            PPBody ppBody = new PPBody();

            if (systemBytes != 0) //S7F23
            {
                #region Handle S7F23
                if (_ppBodyType == PPBodyType.Formatted || _ppBodyType == PPBodyType.Both)
                {
                    ProcessProgramVerification verification = CheckFormattedProcessProgramData2(ppData, out ppBody);

                    _gemControler.ProcessProgramReply(ePPAckType.HostDownloadFormattedProcessProgram, ackc7, systemBytes, out err);

                    if (verification.ErrorMessages.Count == 0)
                    {
                        _ppManager.AddorUpdatePPID(ppid, ppBody);
                        ReflashPPIDList();
                        
                        string path = "D:\\FTGM1\\ParameterDirectory\\Recipe"+ "\\" + ppid + ".ini";
                        ReadWriteINIfile INIfile = new ReadWriteINIfile(path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Filename", "D:\\FTGM1\\ParameterDirectory\\Recipe", path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Rotate_Count", Convert.ToString(ppBody.Rotate_Count), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Type", Convert.ToString(ppBody.Type), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.RepeatTimes", Convert.ToString(ppBody.RepeatTimes), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.RepeatTimes_now", Convert.ToString(ppBody.RepeatTimes_now), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot1", Convert.ToString(ppBody.Slot1), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot2", Convert.ToString(ppBody.Slot2), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot3", Convert.ToString(ppBody.Slot3), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot4", Convert.ToString(ppBody.Slot4), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot5", Convert.ToString(ppBody.Slot5), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot6", Convert.ToString(ppBody.Slot6), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot7", Convert.ToString(ppBody.Slot7), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot8", Convert.ToString(ppBody.Slot8), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot9", Convert.ToString(ppBody.Slot9), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot10", Convert.ToString(ppBody.Slot10), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot11", Convert.ToString(ppBody.Slot11), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot12", Convert.ToString(ppBody.Slot12), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot13", Convert.ToString(ppBody.Slot13), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot14", Convert.ToString(ppBody.Slot14), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot15", Convert.ToString(ppBody.Slot15), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot16", Convert.ToString(ppBody.Slot16), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot17", Convert.ToString(ppBody.Slot17), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot18", Convert.ToString(ppBody.Slot18), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot19", Convert.ToString(ppBody.Slot19), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot20", Convert.ToString(ppBody.Slot20), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot21", Convert.ToString(ppBody.Slot21), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot22", Convert.ToString(ppBody.Slot22), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot23", Convert.ToString(ppBody.Slot23), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot24", Convert.ToString(ppBody.Slot24), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Slot25", Convert.ToString(ppBody.Slot25), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Angle1", Convert.ToString(ppBody.Angle1), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Angle2", Convert.ToString(ppBody.Angle2), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Angle3", Convert.ToString(ppBody.Angle3), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Angle4", Convert.ToString(ppBody.Angle4), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Angle5", Convert.ToString(ppBody.Angle5), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Angle6", Convert.ToString(ppBody.Angle6), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Angle7", Convert.ToString(ppBody.Angle7), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.Angle8", Convert.ToString(ppBody.Angle8), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.CreateTime", Convert.ToString(ppBody.CreateTime), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.ReviseTime", Convert.ToString(ppBody.ReviseTime), path);
                        INIfile.IniWriteValue("Recipe", "fram.Recipe.OffsetType", Convert.ToString(ppBody.OffsetType), path);
                    }
                    else
                    {
                        string errLog = string.Empty;
                        string ppErr = verification.ErrorMessages[0].ErrorDescription;
                        //SYS_PP_ERROR
                        _gemControler.UpdateSV(45, ppErr, out errLog);
                    }

                    _gemControler.ProcessProgramVerificationSend(verification, out err);
                }
                else
                {
                    //Mode unsupported 
                    ackc7 = 5;

                    _gemControler.ProcessProgramReply(ePPAckType.HostDownloadFormattedProcessProgram, ackc7, systemBytes, out err);
                }
                #endregion
            }
            else //S7F26
            {
                #region Handle S7F26
                if (_ppBodyType == PPBodyType.Formatted || _ppBodyType == PPBodyType.Both)
                {
                    if (ppData.CCodes.Count != 0)
                    {
                        ProcessProgramVerification verification = CheckFormattedProcessProgramData2(ppData, out ppBody);

                        if (verification.ErrorMessages.Count == 0)
                        {
                            _ppManager.AddorUpdatePPID(ppid, ppBody);
                            ReflashPPIDList();
                        }
                        else
                        {
                            string errLog = string.Empty;
                            string ppErr = verification.ErrorMessages[0].ErrorDescription;
                            //SYS_PP_ERROR
                            _gemControler.UpdateSV(45, ppErr, out errLog);
                        }

                        _gemControler.ProcessProgramVerificationSend(verification, out err);
                    }
                    else
                    {
                        //Mode unsupported 
                        WriteLog(LogLevel.Warn, "S7F25 request was denied");
                    }
                }
                else
                {
                    WriteLog(LogLevel.Warn, "Mode unsupported");
                }
                #endregion
            }
        }
        private ProcessProgramVerification CheckFormattedProcessProgramData2(FormattedProcessProgramData2 ppData, out PPBody ppBody)
        {
            ppBody = new PPBody();
            List<string> notDealWithCCode = new List<string> { "100" };
            ProcessProgramVerification verification = new ProcessProgramVerification();
            verification.PPID = ppData.PPID.Trim();

            if (ppData.MDLN.Trim() != _modelType)
            {
                VerificationErrorMessage msg = new VerificationErrorMessage();
                msg.AcknowledgeCode = 1;
                msg.CommandNumber = 0;
                msg.ErrorDescription = "MDLN is inconsistent";
                verification.ErrorMessages.Add(msg);
            }

            if (ppData.SOFTREV.Trim() != _softRev)
            {
                VerificationErrorMessage msg = new VerificationErrorMessage();
                msg.AcknowledgeCode = 2;
                msg.CommandNumber = 0;
                msg.ErrorDescription = "SOFTREV is inconsistent";
                verification.ErrorMessages.Add(msg);
            }

            foreach (CommandCode2 commandCode in ppData.CCodes)
            {
                string cCode = commandCode.CCode.Value.ToString();
                notDealWithCCode.Remove(cCode);

                switch (cCode)
                {
                    case "100":
                        #region CCode 100
                        if (commandCode.Parameters.Count != paramCount)
                        {
                            VerificationErrorMessage msg = new VerificationErrorMessage();
                            msg.AcknowledgeCode = 5;
                            msg.CommandNumber = 100;
                            msg.ErrorDescription = "PPARM Count is not equal"+ paramCount;
                            verification.ErrorMessages.Add(msg);
                        }
                        else
                        {
                            if (commandCode.Parameters[0].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Rotate_Count Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Rotate_Count = (byte)commandCode.Parameters[0].Value;
                            }

                            if (commandCode.Parameters[1].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Type Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Type = (byte)commandCode.Parameters[1].Value;
                            }

                            if (commandCode.Parameters[2].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "RepeatTimes Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.RepeatTimes = (byte)commandCode.Parameters[2].Value;
                            }

                            if (commandCode.Parameters[3].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "RepeatTimes_now Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.RepeatTimes_now = (byte)commandCode.Parameters[3].Value;
                            }

                            if (commandCode.Parameters[4].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot1 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot1 = (byte)commandCode.Parameters[4].Value;
                            }

                            if (commandCode.Parameters[5].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot2 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot2 = (byte)commandCode.Parameters[5].Value;
                            }

                            if (commandCode.Parameters[6].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot3 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot3 = (byte)commandCode.Parameters[6].Value;
                            }

                            if (commandCode.Parameters[7].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot4 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot4 = (byte)commandCode.Parameters[7].Value;
                            }

                            if (commandCode.Parameters[8].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot5 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot5 = (byte)commandCode.Parameters[8].Value;
                            }

                            if (commandCode.Parameters[9].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot6 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot6 = (byte)commandCode.Parameters[9].Value;
                            }

                            if (commandCode.Parameters[10].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot7 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot7 = (byte)commandCode.Parameters[10].Value;
                            }

                            if (commandCode.Parameters[11].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot8 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot8 = (byte)commandCode.Parameters[11].Value;
                            }

                            if (commandCode.Parameters[12].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot9 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot9 = (byte)commandCode.Parameters[12].Value;
                            }

                            if (commandCode.Parameters[13].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot10 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot10 = (byte)commandCode.Parameters[13].Value;
                            }

                            if (commandCode.Parameters[14].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot11 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot11 = (byte)commandCode.Parameters[14].Value;
                            }

                            if (commandCode.Parameters[15].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot12 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot12 = (byte)commandCode.Parameters[15].Value;
                            }

                            if (commandCode.Parameters[16].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot13 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot13 = (byte)commandCode.Parameters[16].Value;
                            }

                            if (commandCode.Parameters[17].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot14 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot14 = (byte)commandCode.Parameters[17].Value;
                            }

                            if (commandCode.Parameters[18].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot15 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot15 = (byte)commandCode.Parameters[18].Value;
                            }

                            if (commandCode.Parameters[19].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot16 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot16 = (byte)commandCode.Parameters[19].Value;
                            }

                            if (commandCode.Parameters[20].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot17 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot17 = (byte)commandCode.Parameters[20].Value;
                            }

                            if (commandCode.Parameters[21].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot18 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot18 = (byte)commandCode.Parameters[21].Value;
                            }

                            if (commandCode.Parameters[22].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot19 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot19 = (byte)commandCode.Parameters[22].Value;
                            }

                            if (commandCode.Parameters[23].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot20 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot20 = (byte)commandCode.Parameters[23].Value;
                            }

                            if (commandCode.Parameters[24].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot21 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot21 = (byte)commandCode.Parameters[24].Value;
                            }

                            if (commandCode.Parameters[25].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot22 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot22 = (byte)commandCode.Parameters[25].Value;
                            }

                            if (commandCode.Parameters[26].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot23 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot23 = (byte)commandCode.Parameters[26].Value;
                            }

                            if (commandCode.Parameters[27].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot24 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot24 = (byte)commandCode.Parameters[27].Value;
                            }

                            if (commandCode.Parameters[28].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Slot25 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Slot25 = (byte)commandCode.Parameters[28].Value;
                            }

                            if (commandCode.Parameters[29].Format != ItemFmt.U2)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle1 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Angle1 = (ushort)commandCode.Parameters[29].Value;
                            }

                            if (commandCode.Parameters[30].Format != ItemFmt.U2)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle2 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Angle2 = (ushort)commandCode.Parameters[30].Value;
                            }

                            if (commandCode.Parameters[31].Format != ItemFmt.U2)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle3 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Angle3 = (ushort)commandCode.Parameters[31].Value;
                            }

                            if (commandCode.Parameters[32].Format != ItemFmt.U2)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle4 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Angle4 = (ushort)commandCode.Parameters[32].Value;
                            }

                            if (commandCode.Parameters[33].Format != ItemFmt.U2)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle5 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Angle5 = (ushort)commandCode.Parameters[33].Value;
                            }

                            if (commandCode.Parameters[34].Format != ItemFmt.U2)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle6 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Angle6 = (ushort)commandCode.Parameters[34].Value;
                            }

                            if (commandCode.Parameters[35].Format != ItemFmt.U2)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle7 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Angle7 = (ushort)commandCode.Parameters[35].Value;
                            }

                            if (commandCode.Parameters[36].Format != ItemFmt.U2)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle8 Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.Angle8 = (ushort)commandCode.Parameters[36].Value;
                            }

                            if (commandCode.Parameters[37].Format != ItemFmt.A)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "CreateTime Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.CreateTime = (string)commandCode.Parameters[37].Value;
                            }

                            if (commandCode.Parameters[38].Format != ItemFmt.A)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.ReviseTime = (string)commandCode.Parameters[38].Value;
                            }

                            if (commandCode.Parameters[39].Format != ItemFmt.U1)
                            {
                                VerificationErrorMessage msg = new VerificationErrorMessage();
                                msg.AcknowledgeCode = 4;
                                msg.CommandNumber = 100;
                                msg.ErrorDescription = "Angle Format error";
                                verification.ErrorMessages.Add(msg);
                            }
                            else
                            {
                                ppBody.OffsetType = (byte)commandCode.Parameters[39].Value;
                            }
                        }
                        #endregion
                        break;
                    //case "100":
                    //    #region CCode 100
                    //    if (commandCode.Parameters.Count != 3)
                    //    {
                    //        VerificationErrorMessage msg = new VerificationErrorMessage();
                    //        msg.AcknowledgeCode = 5;
                    //        msg.CommandNumber = 100;
                    //        msg.ErrorDescription = "PPARM Count is not equal 3";
                    //        verification.ErrorMessages.Add(msg);
                    //    }
                    //    else
                    //    {
                    //        if (commandCode.Parameters[0].Format != ItemFmt.F4)
                    //        {
                    //            VerificationErrorMessage msg = new VerificationErrorMessage();
                    //            msg.AcknowledgeCode = 4;
                    //            msg.CommandNumber = 100;
                    //            msg.ErrorDescription = "Tank1_WaterPressure Format error";
                    //            verification.ErrorMessages.Add(msg);
                    //        }
                    //        else
                    //        {
                    //            ppBody.Tank1_WaterPressure = (float)commandCode.Parameters[0].Value;
                    //        }

                    //        if (commandCode.Parameters[1].Format != ItemFmt.I4)
                    //        {
                    //            VerificationErrorMessage msg = new VerificationErrorMessage();
                    //            msg.AcknowledgeCode = 4;
                    //            msg.CommandNumber = 100;
                    //            msg.ErrorDescription = "Tank1_Voltage Format error";
                    //            verification.ErrorMessages.Add(msg);
                    //        }
                    //        else
                    //        {
                    //            ppBody.Tank1_Voltage = (int)commandCode.Parameters[1].Value;
                    //        }

                    //        if (commandCode.Parameters[2].Format != ItemFmt.F4)
                    //        {
                    //            VerificationErrorMessage msg = new VerificationErrorMessage();
                    //            msg.AcknowledgeCode = 4;
                    //            msg.CommandNumber = 100;
                    //            msg.ErrorDescription = "Tank1_AirPressure Format error";
                    //            verification.ErrorMessages.Add(msg);
                    //        }
                    //        else
                    //        {
                    //            ppBody.Tank1_AirPressure = (float)commandCode.Parameters[2].Value;
                    //        }
                    //    }
                    //    #endregion
                    //    break;
                    //case "200":
                    //    #region CCode 200
                    //    if (commandCode.Parameters.Count != 3)
                    //    {
                    //        VerificationErrorMessage msg = new VerificationErrorMessage();
                    //        msg.AcknowledgeCode = 5;
                    //        msg.CommandNumber = 200;
                    //        msg.ErrorDescription = "PPARM Count is not equal 3";
                    //        verification.ErrorMessages.Add(msg);
                    //    }
                    //    else
                    //    {
                    //        if (commandCode.Parameters[0].Format != ItemFmt.F4)
                    //        {
                    //            VerificationErrorMessage msg = new VerificationErrorMessage();
                    //            msg.AcknowledgeCode = 4;
                    //            msg.CommandNumber = 100;
                    //            msg.ErrorDescription = "Tank2_WaterPressure Format error";
                    //            verification.ErrorMessages.Add(msg);
                    //        }
                    //        else
                    //        {
                    //            ppBody.Tank2_WaterPressure = (float)commandCode.Parameters[0].Value;
                    //        }

                    //        if (commandCode.Parameters[1].Format != ItemFmt.I4)
                    //        {
                    //            VerificationErrorMessage msg = new VerificationErrorMessage();
                    //            msg.AcknowledgeCode = 4;
                    //            msg.CommandNumber = 200;
                    //            msg.ErrorDescription = "Tank2_Voltage Format error";
                    //            verification.ErrorMessages.Add(msg);
                    //        }
                    //        else
                    //        {
                    //            ppBody.Tank2_Voltage = (int)commandCode.Parameters[1].Value;
                    //        }

                    //        if (commandCode.Parameters[2].Format != ItemFmt.F4)
                    //        {
                    //            VerificationErrorMessage msg = new VerificationErrorMessage();
                    //            msg.AcknowledgeCode = 4;
                    //            msg.CommandNumber = 200;
                    //            msg.ErrorDescription = "Tank2_AirPressure Format error";
                    //            verification.ErrorMessages.Add(msg);
                    //        }
                    //        else
                    //        {
                    //            ppBody.Tank2_AirPressure = (float)commandCode.Parameters[2].Value;
                    //        }
                    //    }
                    //    #endregion
                    //    break;
                }
            }

            if (notDealWithCCode.Count > 0)
            {
                string missCCodes = string.Join(",", notDealWithCCode);
                VerificationErrorMessage msg = new VerificationErrorMessage();
                msg.AcknowledgeCode = 6;
                msg.CommandNumber = 0;
                msg.ErrorDescription = string.Format("Miss CCode({0}) Data", missCCodes);
                verification.ErrorMessages.Add(msg);
            }

            return verification;
        }

        //>Host Send Process Program Load Inquire
        private void _gemControler_ProcessProgramLoadInquire(object sender, ProcessProgramLoadInquireArgs e)
        {
            byte ppGnt = 0;
            string err;

            this.Invoke((MethodInvoker)delegate ()
            {
                txtS7Systembytes.Text = e.SystemBytes.ToString();
                txtAckc7.Text = string.Empty;
                txtReceivedPPID.Text = e.PPID;
            });

            _gemControler.ProcessProgramLoadGrant(e.PPID, ppGnt, e.SystemBytes, out err);
        }
        //>>Host Qurey PPID BODY (Unformatted)
        private void _gemControler_ProcessProgramUploadRequest(object sender, ProcessProgramDataUploadRequestArgs e)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                txtS7Systembytes.Text = e.SystemBytes.ToString();
                txtAckc7.Text = string.Empty;
                txtReceivedPPID.Text = e.PPID;
            });

            byte[] byteBody;
            string err;
            if (_ppBodyType == PPBodyType.Unformatted || _ppBodyType == PPBodyType.Both)
            {
                PPBody ppBody = _ppManager.GetPPBody(e.PPID.Trim());
                if (ppBody != null)
                {
                    byte[] tmpParam;
                    byteBody = Enumerable.Repeat<byte>(0, 200).ToArray();
                    //tmpParam = BitConverter.GetBytes(ppBody.Tank1_WaterPressure);
                    //Buffer.BlockCopy(tmpParam, 0, byteBody, 0, tmpParam.Length);
                    //tmpParam = BitConverter.GetBytes(ppBody.Tank1_Voltage);
                    //Buffer.BlockCopy(tmpParam, 0, byteBody, 4, tmpParam.Length);
                    //tmpParam = BitConverter.GetBytes(ppBody.Tank1_AirPressure);
                    //Buffer.BlockCopy(tmpParam, 0, byteBody, 8, tmpParam.Length);
                    //tmpParam = BitConverter.GetBytes(ppBody.Tank2_WaterPressure);
                    //Buffer.BlockCopy(tmpParam, 0, byteBody, 100, tmpParam.Length);
                    //tmpParam = BitConverter.GetBytes(ppBody.Tank2_Voltage);
                    //Buffer.BlockCopy(tmpParam, 0, byteBody, 104, tmpParam.Length);
                    //tmpParam = BitConverter.GetBytes(ppBody.Tank2_AirPressure);
                    //Buffer.BlockCopy(tmpParam, 0, byteBody, 108, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Rotate_Count);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 0, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Type);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 1, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.RepeatTimes);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 2, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.RepeatTimes_now);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 3, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot1);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 4, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot2);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 5, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot3);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 6, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot4);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 7, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot5);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 8, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot6);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 9, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot7);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 10, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot8);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 11, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot9);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 12, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot10);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 13, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot11);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 14, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot12);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 15, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot13);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 16, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot14);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 17, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot15);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 18, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot16);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 19, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot17);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 20, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot18);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 21, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot19);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 22, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot20);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 23, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot21);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 24, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot22);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 25, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot23);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 26, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot24);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 27, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Slot25);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 28, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Angle1);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 29, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Angle2);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 31, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Angle3);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 33, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Angle4);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 35, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Angle5);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 37, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Angle6);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 39, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Angle7);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 41, tmpParam.Length);
                    tmpParam = BitConverter.GetBytes(ppBody.Angle8);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 43, tmpParam.Length);
                    int CreateCharCount = ppBody.CreateTime.ToCharArray().Length;
                    for (int i = 0; i < CreateCharCount; i++)
                    {
                        tmpParam = BitConverter.GetBytes(ppBody.CreateTime.ToCharArray()[i]);
                        Buffer.BlockCopy(tmpParam, 0, byteBody, 45 + i, tmpParam.Length);
                    }
                    int ReviseCharCount = ppBody.ReviseTime.ToCharArray().Length;
                    for (int i = 0; i < ReviseCharCount; i++)
                    {
                        tmpParam = BitConverter.GetBytes(ppBody.ReviseTime.ToCharArray()[i]);
                        Buffer.BlockCopy(tmpParam, 0, byteBody, 46 + CreateCharCount, tmpParam.Length);
                    }
                    tmpParam = BitConverter.GetBytes(ppBody.OffsetType);
                    Buffer.BlockCopy(tmpParam, 0, byteBody, 47 + CreateCharCount + ReviseCharCount, tmpParam.Length);

                }
                else
                {
                    byteBody = new byte[0];
                }
            }
            else
            {
                byteBody = new byte[0];
            }

            ProcessProgramData ppData = new ProcessProgramData();
            ppData.PPID = e.PPID.Trim();
            ppData.Format = ItemFmt.B;
            ppData.Body = byteBody;
            _gemControler.ProcessProgramSend(ppData, e.SystemBytes, out err);
        }
        //>>Host Qurey PPID BODY (Formatted)
        private void _gemControler_FormattedProcessProgramUploadRequest(object sender, FormattedProcessProgramDataUploadRequestArgs e)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                txtS7Systembytes.Text = e.SystemBytes.ToString();
                txtAckc7.Text = string.Empty;
                txtReceivedPPID.Text = e.PPID;
            });

            string err;
            //CCode Only U2 Format
            //FormattedProcessProgramData ppData = new FormattedProcessProgramData();
            //ppData.PPID = e.PPID.Trim();
            //ppData.MDLN = _modelType;
            //ppData.SOFTREV = _softRev;

            //if (_ppBodyType == PPBodyType.Formatted || _ppBodyType == PPBodyType.Both)
            //{
            //    PPBody ppBody = _ppManager.GetPPBody(e.PPID.Trim());
            //    if (ppBody != null)
            //    {
            //        CommandCode cCode1 = new CommandCode();
            //        cCode1.CCode = 100;
            //        cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.F4, ppBody.Tank1_WaterPressure));
            //        cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.I4, ppBody.Tank1_Voltage));
            //        cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.F4, ppBody.Tank1_AirPressure));
            //        ppData.CCodes.Add(cCode1);
            //        CommandCode cCode2 = new CommandCode();
            //        cCode2.CCode = 200;
            //        cCode2.Parameters.Add(SecsIIValue.Create(ItemFmt.F4, ppBody.Tank2_WaterPressure));
            //        cCode2.Parameters.Add(SecsIIValue.Create(ItemFmt.I4, ppBody.Tank2_Voltage));
            //        cCode2.Parameters.Add(SecsIIValue.Create(ItemFmt.F4, ppBody.Tank2_AirPressure));
            //        ppData.CCodes.Add(cCode2);
            //    }
            //}

            //_gemControler.ProcessProgramSend(ppData, e.SystemBytes, out err);


            //CCode Multi Format (A/U2/U4/I2/I4)
            FormattedProcessProgramData2 ppData = new FormattedProcessProgramData2();
            ppData.PPID = e.PPID.Trim();
            ppData.MDLN = _modelType;
            ppData.SOFTREV = _softRev;

            if (_ppBodyType == PPBodyType.Formatted || _ppBodyType == PPBodyType.Both)
            {
                _ppManager.Initial();
                PPBody ppBody = _ppManager.GetPPBody(e.PPID.Trim());
                if (ppBody != null)
                {
                    //CommandCode2 cCode1 = new CommandCode2();
                    //cCode1.CCode = SecsIIValue.Create(ItemFmt.I4, 100);
                    //cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.F4, ppBody.Tank1_WaterPressure));
                    //cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.I4, ppBody.Tank1_Voltage));
                    //cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.F4, ppBody.Tank1_AirPressure));
                    //ppData.CCodes.Add(cCode1);
                    //CommandCode2 cCode2 = new CommandCode2();
                    //cCode2.CCode = SecsIIValue.Create(ItemFmt.I4, 200);
                    //cCode2.Parameters.Add(SecsIIValue.Create(ItemFmt.F4, ppBody.Tank2_WaterPressure));
                    //cCode2.Parameters.Add(SecsIIValue.Create(ItemFmt.I4, ppBody.Tank2_Voltage));
                    //cCode2.Parameters.Add(SecsIIValue.Create(ItemFmt.F4, ppBody.Tank2_AirPressure));
                    CommandCode2 cCode1 = new CommandCode2();
                    cCode1.CCode = SecsIIValue.Create(ItemFmt.A, "100");
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Rotate_Count));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Type));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.RepeatTimes));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.RepeatTimes_now));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot1));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot2));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot3));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot4));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot5));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot6));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot7));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot8));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot9));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot10));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot11));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot12));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot13));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot14));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot15));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot16));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot17));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot18));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot19));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot20));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot21));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot22));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot23));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot24));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Slot25));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, ppBody.Angle1));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, ppBody.Angle2));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, ppBody.Angle3));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, ppBody.Angle4));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, ppBody.Angle5));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, ppBody.Angle6));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, ppBody.Angle7));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, ppBody.Angle8));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.A, ppBody.CreateTime));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.A, ppBody.ReviseTime));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.OffsetType));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.LJStdSurface));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, ppBody.BTTH));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, ppBody.S1_1x0));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, ppBody.S1_1x1));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, ppBody.S2_1x0));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, ppBody.S2_1x1));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, ppBody.S2_2x0));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, ppBody.S2_2x1));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Range1));
                    cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, ppBody.Range2));

                    ppData.CCodes.Add(cCode1);
                }
            }

            _gemControler.ProcessProgramSend2(ppData, e.SystemBytes, out err);
        }

        //>>Host Send Delete Process Program
        private void _gemControler_ProcessProgramDelete(object sender, ProcessProgramDeleteArgs e)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                txtS7Systembytes.Text = e.SystemBytes.ToString();
                txtAckc7.Text = string.Empty;
                txtReceivedPPID.Text = string.Empty;
            });

            byte ack = 0;
            string err;

            if (e.Type == ePPDeleteType.DeleteAll)
            {
                _ppManager.DeletePPID_All();
                ReflashPPIDList();
            }
            else
            {
                foreach (string ppid in e.PPIDs)
                {
                    _ppManager.DeletePPID(ppid.Trim());
                }
                ReflashPPIDList();
            }

            _gemControler.ProcessProgramReply(ePPAckType.HostDeleteProcessProgram, ack, e.SystemBytes, out err);
        }

        //>>Host Qeruy Process Program List
        private void _gemControler_ProcessProgramDirectoryQuery(object sender, ProcessProgramDirectoryQueryArgs e)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                txtS7Systembytes.Text = e.SystemBytes.ToString();
                txtAckc7.Text = string.Empty;
                txtReceivedPPID.Text = string.Empty;
            });

            string err;
            //System.IO.FileInfo fileName;
            //string path = "D:\\FTGM1\\ParameterDirectory\\Recipe";
            //List<string> PPIDList = new List<string>();
            //ReadWriteINIfile INIfile = new ReadWriteINIfile(path);
            //string filename2;
            //foreach (string fname in System.IO.Directory.GetFiles(path))
            //{
            //    fileName = new System.IO.FileInfo(fname);//取得完整檔名(含副檔名)
            //    filename2 = fileName.ToString();
            //    filename2 = System.IO.Path.GetFileNameWithoutExtension(filename2);
            //    PPIDList.Add(filename2);
            //}

            _ppManager.Initial();
            _gemControler.ProcessProgramDirectory(_ppManager.PPIDList, e.SystemBytes, out err);
            //_gemControler.ProcessProgramDirectory(_ppManager.PPIDList, e.SystemBytes, out err);
        }

        private void lvPPIDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPPIDs.SelectedItems.Count == 0) return;

            string ppid = lvPPIDs.SelectedItems[0].Text;
            txtPPID.Text = ppid;

            PPBody ppBody = _ppManager.GetPPBody(ppid);
            txtRotateCount.Text = ppBody.Rotate_Count.ToString();
            txtType.Text = ppBody.Type.ToString();
            txtRepeat.Text = ppBody.RepeatTimes.ToString();
            txtRepeatTimes_now.Text = ppBody.RepeatTimes_now.ToString();
            txtOffsetType.Text = ppBody.OffsetType.ToString();
            txtLJStdSurface.Text = ppBody.LJStdSurface.ToString();
            cLBPP.SetItemChecked(0, ppBody.Slot1 == 1);
            cLBPP.SetItemChecked(1, ppBody.Slot2 == 1);
            cLBPP.SetItemChecked(2, ppBody.Slot3 == 1);
            cLBPP.SetItemChecked(3, ppBody.Slot4 == 1);
            cLBPP.SetItemChecked(4, ppBody.Slot5 == 1);
            cLBPP.SetItemChecked(5, ppBody.Slot6 == 1);
            cLBPP.SetItemChecked(6, ppBody.Slot7 == 1);
            cLBPP.SetItemChecked(7, ppBody.Slot8 == 1);
            cLBPP.SetItemChecked(8, ppBody.Slot9 == 1);
            cLBPP.SetItemChecked(9, ppBody.Slot10 == 1);
            cLBPP.SetItemChecked(10, ppBody.Slot11 == 1);
            cLBPP.SetItemChecked(11, ppBody.Slot12 == 1);
            cLBPP.SetItemChecked(12, ppBody.Slot13 == 1);
            cLBPP.SetItemChecked(13, ppBody.Slot14 == 1);
            cLBPP.SetItemChecked(14, ppBody.Slot15 == 1);
            cLBPP.SetItemChecked(15, ppBody.Slot16 == 1);
            cLBPP.SetItemChecked(16, ppBody.Slot17 == 1);
            cLBPP.SetItemChecked(17, ppBody.Slot18 == 1);
            cLBPP.SetItemChecked(18, ppBody.Slot19 == 1);
            cLBPP.SetItemChecked(19, ppBody.Slot20 == 1);
            cLBPP.SetItemChecked(20, ppBody.Slot21 == 1);
            cLBPP.SetItemChecked(21, ppBody.Slot22 == 1);
            cLBPP.SetItemChecked(22, ppBody.Slot23 == 1);
            cLBPP.SetItemChecked(23, ppBody.Slot24 == 1);
            cLBPP.SetItemChecked(24, ppBody.Slot25 == 1);
            txtAngle1.Text = ppBody.Angle1.ToString();
            txtAngle2.Text = ppBody.Angle2.ToString();
            txtAngle3.Text = ppBody.Angle3.ToString();
            txtAngle4.Text = ppBody.Angle4.ToString();
            txtAngle5.Text = ppBody.Angle5.ToString();
            txtAngle6.Text = ppBody.Angle6.ToString();
            txtAngle7.Text = ppBody.Angle7.ToString();
            txtAngle8.Text = ppBody.Angle8.ToString();
            txtCreateTime.Text = ppBody.CreateTime.ToString();
            txtReviseTime.Text = ppBody.ReviseTime.ToString();
            txtBTTH.Text = ppBody.BTTH.ToString();
            txtS1_1x0.Text = ppBody.S1_1x0.ToString();
            txtS1_1x1.Text = ppBody.S1_1x1.ToString();
            txtS2_1x0.Text = ppBody.S2_1x0.ToString();
            txtS2_1x1.Text = ppBody.S2_1x1.ToString();
            txtS2_2x0.Text = ppBody.S2_2x0.ToString();
            txtS2_2x1.Text = ppBody.S2_2x1.ToString();
            txtRange1.Text = ppBody.Range1.ToString();
            txtRange2.Text = ppBody.Range2.ToString();

            if (_ppBodyType == PPBodyType.Unformatted || _ppBodyType == PPBodyType.Both)
            {
                txtPPBody_Unformatted.Text = ppBody.GetPPBody_Bytes().ToHexString().Replace(" ", "");
            }
            else
            {
                txtPPBody_Unformatted.Text = string.Empty;
            }
        }

        private void btnPPChanged_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            string log = string.Empty;
            string err;

            if (_gemControler != null)
            {
                string ppid = txtPPID.Text.Trim();
                if (string.IsNullOrWhiteSpace(ppid))
                {
                    msg = "Miss PPID Data";
                    MessageBox.Show(msg);
                    return;
                }

                PPBody ppBody = new PPBody();
                if (cmbChangedPPType.Text == "Create" || cmbChangedPPType.Text == "Changed")
                {
                    byte RotateCount;
                    if (!byte.TryParse(txtRotateCount.Text, out RotateCount))
                    {
                        msg = "Parse RotateCount to Byte Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    byte pptype;
                    if (!byte.TryParse(txtType.Text, out pptype))
                    {
                        msg = "Parse Type to Byte Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    byte RepeatTimes;
                    if (!byte.TryParse(txtRepeat.Text, out RepeatTimes))
                    {
                        msg = "Parse RepeatTimes to Byte Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    byte RepeatTimes_now;
                    if (!byte.TryParse(txtRepeatTimes_now.Text, out RepeatTimes_now))
                    {
                        msg = "Parse RepeatTimes_now to Byte Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    byte OffsetType;
                    if (!byte.TryParse(txtOffsetType.Text, out OffsetType))
                    {
                        msg = "Parse OffsetType to Byte Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    byte LJStdSurface;
                    if (!byte.TryParse(txtLJStdSurface.Text, out LJStdSurface))
                    {
                        msg = "Parse LJStdSurface to Byte Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    ushort Angle1;
                    if (!ushort.TryParse(txtAngle1.Text, out Angle1))
                    {
                        msg = "Parse Angle1 to ushort Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    ushort Angle2;
                    if (!ushort.TryParse(txtAngle2.Text, out Angle2))
                    {
                        msg = "Parse Angle2 to ushort Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    ushort Angle3;
                    if (!ushort.TryParse(txtAngle3.Text, out Angle3))
                    {
                        msg = "Parse Angle3 to ushort Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    ushort Angle4;
                    if (!ushort.TryParse(txtAngle4.Text, out Angle4))
                    {
                        msg = "Parse Angle4 to ushort Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    ushort Angle5;
                    if (!ushort.TryParse(txtAngle5.Text, out Angle5))
                    {
                        msg = "Parse Angle5 to ushort Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    ushort Angle6;
                    if (!ushort.TryParse(txtAngle6.Text, out Angle6))
                    {
                        msg = "Parse Angle6 to ushort Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    ushort Angle7;
                    if (!ushort.TryParse(txtAngle7.Text, out Angle7))
                    {
                        msg = "Parse Angle7 to ushort Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    ushort Angle8;
                    if (!ushort.TryParse(txtAngle8.Text, out Angle8))
                    {
                        msg = "Parse Angle8 to ushort Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    ushort BTTH;
                    if (!ushort.TryParse(txtBTTH.Text, out BTTH))
                    {
                        msg = "Parse BTTH to ushort Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    ushort S1_1x0;
                    if (!ushort.TryParse(txtS1_1x0.Text, out S1_1x0))
                    {
                        msg = "Parse S1_1x0 to ushort Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    ushort S1_1x1;
                    if (!ushort.TryParse(txtS1_1x1.Text, out S1_1x1))
                    {
                        msg = "Parse S1_1x1 to ushort Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    ushort S2_1x0;
                    if (!ushort.TryParse(txtS2_1x0.Text, out S2_1x0))
                    {
                        msg = "Parse S2_1x0 to ushort Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    ushort S2_1x1;
                    if (!ushort.TryParse(txtS2_1x1.Text, out S2_1x1))
                    {
                        msg = "Parse S2_1x1 to ushort Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    ushort S2_2x0;
                    if (!ushort.TryParse(txtS2_2x0.Text, out S2_2x0))
                    {
                        msg = "Parse S2_2x0 to ushort Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    ushort S2_2x1;
                    if (!ushort.TryParse(txtS2_2x1.Text, out S2_2x1))
                    {
                        msg = "Parse S2_2x1 to ushort Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    byte Range1;
                    if (!byte.TryParse(txtRange1.Text, out Range1))
                    {
                        msg = "Parse Range1 to Byte Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    byte Range2;
                    if (!byte.TryParse(txtRange2.Text, out Range2))
                    {
                        msg = "Parse Range2 to Byte Error";
                        MessageBox.Show(msg);
                        return;
                    }
                    byte t = 1;
                    byte f = 0;
                    ppBody.Rotate_Count = RotateCount;
                    ppBody.Type = pptype;
                    ppBody.RepeatTimes = RepeatTimes;
                    ppBody.RepeatTimes_now = RepeatTimes_now;
                    ppBody.OffsetType = OffsetType;
                    ppBody.LJStdSurface = LJStdSurface;
                    ppBody.Slot1 = cLBPP.GetItemChecked(0) ? t : f;
                    ppBody.Slot2 = cLBPP.GetItemChecked(1) ? t : f;
                    ppBody.Slot3 = cLBPP.GetItemChecked(2) ? t : f;
                    ppBody.Slot4 = cLBPP.GetItemChecked(3) ? t : f;
                    ppBody.Slot5 = cLBPP.GetItemChecked(4) ? t : f;
                    ppBody.Slot6 = cLBPP.GetItemChecked(5) ? t : f;
                    ppBody.Slot7 = cLBPP.GetItemChecked(6) ? t : f;
                    ppBody.Slot8 = cLBPP.GetItemChecked(7) ? t : f;
                    ppBody.Slot9 = cLBPP.GetItemChecked(8) ? t : f;
                    ppBody.Slot10 = cLBPP.GetItemChecked(9) ? t : f;
                    ppBody.Slot11 = cLBPP.GetItemChecked(10) ? t : f;
                    ppBody.Slot12 = cLBPP.GetItemChecked(11) ? t : f;
                    ppBody.Slot13 = cLBPP.GetItemChecked(12) ? t : f;
                    ppBody.Slot14 = cLBPP.GetItemChecked(13) ? t : f;
                    ppBody.Slot15 = cLBPP.GetItemChecked(14) ? t : f;
                    ppBody.Slot16 = cLBPP.GetItemChecked(15) ? t : f;
                    ppBody.Slot17 = cLBPP.GetItemChecked(16) ? t : f;
                    ppBody.Slot18 = cLBPP.GetItemChecked(17) ? t : f;
                    ppBody.Slot19 = cLBPP.GetItemChecked(18) ? t : f;
                    ppBody.Slot20 = cLBPP.GetItemChecked(19) ? t : f;
                    ppBody.Slot21 = cLBPP.GetItemChecked(20) ? t : f;
                    ppBody.Slot22 = cLBPP.GetItemChecked(21) ? t : f;
                    ppBody.Slot23 = cLBPP.GetItemChecked(22) ? t : f;
                    ppBody.Slot24 = cLBPP.GetItemChecked(23) ? t : f;
                    ppBody.Slot25 = cLBPP.GetItemChecked(24) ? t : f;
                    ppBody.Angle1 = Angle1;
                    ppBody.Angle2 = Angle2;
                    ppBody.Angle3 = Angle3;
                    ppBody.Angle4 = Angle4;
                    ppBody.Angle5 = Angle5;
                    ppBody.Angle6 = Angle6;
                    ppBody.Angle7 = Angle7;
                    ppBody.Angle8 = Angle8;
                    if(cmbChangedPPType.Text == "Create")
                    {
                        ppBody.CreateTime = DateTime.Now.ToString();
                        ppBody.ReviseTime = DateTime.Now.ToString();
                    }
                    else if(cmbChangedPPType.Text == "Changed")
                    {
                        ppBody.CreateTime = txtCreateTime.Text;
                        ppBody.ReviseTime = DateTime.Now.ToString();
                    }                    
                    ppBody.BTTH = BTTH;
                    ppBody.S1_1x0 = S1_1x0;
                    ppBody.S1_1x1 = S1_1x1;
                    ppBody.S2_1x0 = S2_1x0;
                    ppBody.S2_1x1 = S2_1x1;
                    ppBody.S2_2x0 = S2_2x0;
                    ppBody.S2_2x1 = S2_2x1;
                    ppBody.Range1 = Range1;
                    ppBody.Range2 = Range2;
                }

                byte type = 0;
                switch (cmbChangedPPType.Text)
                {
                    case "Create":
                        type = 1;
                        if (_ppManager.IsConatain(ppid))
                        {
                            msg = string.Format("PPID({0}) is exist, can not create.", ppid);
                            MessageBox.Show(msg);
                            return;
                        }

                        _ppManager.AddorUpdatePPID(ppid, ppBody);
                        ReflashPPIDList();
                        break;

                    case "Changed":
                        type = 2;
                        _ppManager.AddorUpdatePPID(ppid, ppBody);
                        ReflashPPIDList();
                        break;
                    case "Deleted":
                        type = 3;
                        _ppManager.DeletePPID(ppid);
                        ReflashPPIDList();
                        break;
                    default:
                        msg = "Miss Legal Type Data";
                        MessageBox.Show(msg);
                        return;
                }

                //SYS_PP_CHANGE_NAME
                if (_gemControler.UpdateSV(41, ppid, out log) != 0)
                {
                    WriteLog(LogLevel.Warn, log);
                    return;
                }
                //SYS_PP_CHANGE_STATUS
                if (_gemControler.UpdateSV(42, type, out log) != 0)
                {
                    WriteLog(LogLevel.Warn, log);
                    return;
                }
                //SYS_UNFORMATTED_PP_CHANGE_CONTENT
                if (_gemControler.UpdateSV(43, ppBody.GetPPBody_Bytes(), out log) != 0)
                {
                    WriteLog(LogLevel.Warn, log);
                    return;
                }
                //SYS_FORMATTED_PP_CHANGE_CONTENT
                if (_gemControler.UpdateSV(44, ppBody.GetPPBody_ListWrapper(_modelType, _softRev), out log) != 0)
                {
                    WriteLog(LogLevel.Warn, log);
                    return;
                }

                //Process_Program_Change Event
                uint ceid = 8;
                int result = _gemControler.EventReportSend(ceid, out err);
                if (result != 0)
                {
                    log = string.Format("EventReport CEID({0}) Failure, ({1})Reason:{2}", ceid, result, err);
                    WriteLog(LogLevel.Warn, log);
                }

                txtPPID.Text = string.Empty;
                txtRotateCount.Text = string.Empty;
                txtType.Text = string.Empty;
                txtRepeat.Text = string.Empty;
                txtRepeatTimes_now.Text = string.Empty;
                txtOffsetType.Text = string.Empty;
                txtLJStdSurface.Text = string.Empty;
                for (int i = 0; i < cLBPP.Items.Count; i++)
                {
                    cLBPP.SetItemChecked(i, false);
                }
                txtAngle1.Text = string.Empty;
                txtAngle2.Text = string.Empty;
                txtAngle3.Text = string.Empty;
                txtAngle4.Text = string.Empty;
                txtAngle5.Text = string.Empty;
                txtAngle6.Text = string.Empty;
                txtAngle7.Text = string.Empty;
                txtAngle8.Text = string.Empty;
                txtCreateTime.Text = string.Empty;
                txtReviseTime.Text = string.Empty;
                txtBTTH.Text = string.Empty;
                txtS1_1x0.Text = string.Empty;
                txtS1_1x1.Text = string.Empty;
                txtS2_1x0.Text = string.Empty;
                txtS2_1x1.Text = string.Empty;
                txtS2_2x0.Text = string.Empty;
                txtS2_2x1.Text = string.Empty;
                txtRange1.Text = string.Empty;
                txtRange2.Text = string.Empty;
            }
        }

        private void ReflashPPIDList()
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                lvPPIDs.Items.Clear();
                foreach (string ppid in _ppManager.PPIDList)
                {
                    lvPPIDs.Items.Add(ppid);
                }
            });
        }

        private void cmbChangedPPType_SelectedValueChanged(object sender, EventArgs e)
        {
            txtPPID.ReadOnly = true;
            txtRotateCount.ReadOnly = true;
            txtType.ReadOnly = true;
            txtRepeat.ReadOnly = true;
            txtRepeatTimes_now.ReadOnly = true;
            txtOffsetType.ReadOnly = true;
            txtLJStdSurface.ReadOnly = true;
            cLBPP.Enabled = false;
            txtAngle1.ReadOnly = true;
            txtAngle2.ReadOnly = true;
            txtAngle3.ReadOnly = true;
            txtAngle4.ReadOnly = true;
            txtAngle5.ReadOnly = true;
            txtAngle6.ReadOnly = true;
            txtAngle7.ReadOnly = true;
            txtAngle8.ReadOnly = true;
            txtCreateTime.ReadOnly = true;
            txtReviseTime.ReadOnly = true;
            txtBTTH.ReadOnly = true;
            txtS1_1x0.ReadOnly = true;
            txtS1_1x1.ReadOnly = true;
            txtS2_1x0.ReadOnly = true;
            txtS2_1x1.ReadOnly = true;
            txtS2_2x0.ReadOnly = true;
            txtS2_2x1.ReadOnly = true;
            txtRange1.ReadOnly = true;
            txtRange2.ReadOnly = true;

            switch (cmbChangedPPType.Text)
            {
                case "Create":
                    txtPPID.ReadOnly = false;
                    txtRotateCount.ReadOnly = false;
                    txtType.ReadOnly = false;
                    txtRepeat.ReadOnly = false;
                    txtRepeatTimes_now.ReadOnly = false;
                    txtOffsetType.ReadOnly = false;
                    txtLJStdSurface.ReadOnly = false;
                    cLBPP.Enabled = true;
                    txtAngle1.ReadOnly = false;
                    txtAngle2.ReadOnly = false;
                    txtAngle3.ReadOnly = false;
                    txtAngle4.ReadOnly = false;
                    txtAngle5.ReadOnly = false;
                    txtAngle6.ReadOnly = false;
                    txtAngle7.ReadOnly = false;
                    txtAngle8.ReadOnly = false;
                    txtCreateTime.ReadOnly = false;
                    txtReviseTime.ReadOnly = false;
                    txtBTTH.ReadOnly = false;
                    txtS1_1x0.ReadOnly = false;
                    txtS1_1x1.ReadOnly = false;
                    txtS2_1x0.ReadOnly = false;
                    txtS2_1x1.ReadOnly = false;
                    txtS2_2x0.ReadOnly = false;
                    txtS2_2x1.ReadOnly = false;
                    txtRange1.ReadOnly = false;
                    txtRange2.ReadOnly = false;
                    break;
                case "Changed":
                    txtPPID.ReadOnly = false;
                    txtRotateCount.ReadOnly = false;
                    txtType.ReadOnly = false;
                    txtRepeat.ReadOnly = false;
                    txtRepeatTimes_now.ReadOnly = false;
                    txtOffsetType.ReadOnly = false;
                    txtLJStdSurface.ReadOnly = false;
                    cLBPP.Enabled = true;
                    txtAngle1.ReadOnly = false;
                    txtAngle2.ReadOnly = false;
                    txtAngle3.ReadOnly = false;
                    txtAngle4.ReadOnly = false;
                    txtAngle5.ReadOnly = false;
                    txtAngle6.ReadOnly = false;
                    txtAngle7.ReadOnly = false;
                    txtAngle8.ReadOnly = false;
                    txtCreateTime.ReadOnly = false;
                    txtReviseTime.ReadOnly = false;
                    txtBTTH.ReadOnly = false;
                    txtS1_1x0.ReadOnly = false;
                    txtS1_1x1.ReadOnly = false;
                    txtS2_1x0.ReadOnly = false;
                    txtS2_1x1.ReadOnly = false;
                    txtS2_2x0.ReadOnly = false;
                    txtS2_2x1.ReadOnly = false;
                    txtRange1.ReadOnly = false;
                    txtRange2.ReadOnly = false;
                    break;
                case "Deleted":
                    break;
            }
        }

        private void rdoPPBody_Type_CheckedChanged(object sender, EventArgs e)
        {
            string errLog = string.Empty;
            if (rdoPPBody_Unformatted.Checked)
            {
                btnSendS7F3.Enabled = true;
                btnSendS7F5.Enabled = true;
                btnSendS7F23.Enabled = false;
                btnSendS7F25.Enabled = false;
                _ppBodyType = PPBodyType.Unformatted;
                _gemControler.UpdateSV(15, (byte)1, out errLog);
            }
            else if (rdoPPBody_Formatted.Checked)
            {
                btnSendS7F3.Enabled = false;
                btnSendS7F5.Enabled = false;
                btnSendS7F23.Enabled = true;
                btnSendS7F25.Enabled = true;
                _ppBodyType = PPBodyType.Formatted;
                txtPPBody_Unformatted.Text = string.Empty;
                _gemControler.UpdateSV(15, (byte)2, out errLog);
            }
            else
            {
                btnSendS7F3.Enabled = true;
                btnSendS7F5.Enabled = true;
                btnSendS7F23.Enabled = true;
                btnSendS7F25.Enabled = true;
                _ppBodyType = PPBodyType.Both;
                _gemControler.UpdateSV(15, (byte)3, out errLog);
            }
        }

        //GEM Clock----------------------------------------------------------------------------------------------------
        private void btnEQPDateAndTimeRequest_Click(object sender, EventArgs e)
        {
            string err;
            _gemControler.EquipmentDateAndTimeRequest(out err);
        }

        private void _gemControler_DateTimeSyncCommand(object sender, ClockEventDateTimeSyncCommandArgs e)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                txtClockMessageName.Text = e.ReceiveClockMessageName.ToString();
                txtHostDownloadDateTime.Text = e.HostDownloadDateTime;
                txtSetSystemDateTimeSuccess.Text = e.SetSystemDateTimeSuccess.ToString();
            });
        }

        //GEM Equipment Terminal----------------------------------------------------------------------------------------------------
        private void btnTerminalRequest_Click(object sender, EventArgs e)
        {
            string err;
            _gemControler.TerminalRequest(byte.Parse(txtTID.Text), txtText.Text, out err);
        }

        private void _gemControler_TerminalDisplay(object sender, EquipmentTerminalServicesEventTerminalDisplayArgs e)
        {
            bCanReplyS10F04orS10F06 = true;

            this.Invoke((MethodInvoker)delegate ()
            {
                txtTerminalDisplayType.Text = e.TerminalDisplayType.ToString();
                txtTerminalNumber.Text = e.TerminalNumber.ToString();
                txtTerminalSystemBytes.Text = e.SystemBytes.ToString();
                txtTerminalText.Text = string.Join("\r\n", e.Texts).ToString();
            });

            //e.Ack = 1;
            //e.MultiblockNotAllowed = true;
            //_gemControler.TerminalDisplayReply(e);
        }

        private void btnTerminalACKC10_Click(object sender, EventArgs e)
        {
            if (bCanReplyS10F04orS10F06)
            {
                string err;
                eTerminalDisplayType terminalDisplayType = txtTerminalDisplayType.Text == "Single" ? eTerminalDisplayType.Single : eTerminalDisplayType.Multiblock;
                byte ack = byte.Parse(txtTerminalACKC10.Text);
                uint systemBytes = uint.Parse(txtTerminalSystemBytes.Text);
                bool multiblockNotAllowed = rdoMultiblockNotAllowedTrue.Checked ? true : false;
                byte terminalNumber = byte.Parse(txtTerminalNumber.Text);
                _gemControler.TerminalDisplayReply(terminalDisplayType, ack, systemBytes, out err, multiblockNotAllowed, terminalNumber);

                bCanReplyS10F04orS10F06 = false;
            }
            else
            {
                string log = string.Format("Can't Reply Command, Because Not Receive Command!");
                WriteLog(LogLevel.Warn, log);
                MessageBox.Show(log);
            }
        }

        private void btnMessageRecognition_Click(object sender, EventArgs e)
        {
            string err;
            txtTerminalText.Text = string.Empty;
            //Message_Recognition Event
            _gemControler.EventReportSend(13, out err);
        }

        //GEM ObjectServiceStandard----------------------------------------------------------------------------------------------------
        //EQP Device CreateObject
        private void btnCreateObject_Click(object sender, EventArgs e)
        {
            int result = 0;
            string err;
            //CreateObject
            result = _gemControler.CreateObject(txtCreateObjType.Text, txtCreateObjID.Text, out err, txtCreateObjSpec.Text);

            //GetObject
            ObjectInstance objectInstance = null;
            result = _gemControler.GetObject(txtCreateObjType.Text, txtCreateObjID.Text, out objectInstance, out err);

            //UpdateObject
            if (objectInstance != null)
            {
                List<ObjectAttribute> listObjectAttributes = new List<ObjectAttribute>();
                //CARRIER Update
                ObjectAttribute objectAttribute = new ObjectAttribute(441, ObjectAttributeKey.OBJID, objectInstance.ObjID);
                listObjectAttributes.Add(objectAttribute);
                objectAttribute = new ObjectAttribute(443, "CARRIERIDSTATUS", (byte)0);
                listObjectAttributes.Add(objectAttribute);
                //...other ObjectAttribute

                objectInstance.ListObjectAttributes = listObjectAttributes;
                result = _gemControler.UpdateObject(objectInstance, out err);
            }
        }

        ////Host CreateObject
        private void _gemControler_CreateObjectRequestCommand(object sender, CreateObjectRequestArgs e)
        {
            string err;
            foreach (ObjectInstance objectEntity in e.ListObjectEntities)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    txtS14F9_OBJSPEC_Recv.Text = objectEntity.ObjSpec.ToString();
                    txtS14F9_OBJTYPE_Recv.Text = objectEntity.ObjType.ToString();
                    txtS14F9_OBJID_Recv.Text = objectEntity.ObjID.ToString();
                    txtS14F9_SystemBytes_Recv.Text = e.SystemBytes.ToString();
                    txtS14F9_CreateOBJ_Type.Text = e.ReceiveMessageName.ToString();
                });
                bCanReplyCreateObjectRequestCommand = true;
                lastRecvListObjectInstance = e.ListObjectEntities;

                eReceiveCreateMessageName createType = (eReceiveCreateMessageName)Enum.Parse(typeof(eReceiveCreateMessageName), e.ReceiveMessageName.ToString());
                List<ObjectInstance> listObjectEntities = new List<ObjectInstance>();
                List<ErrorReport> listErrorReports = new List<ErrorReport>();
                byte bAck;
                uint uSystemBytes = e.SystemBytes;
                ObjectInstance createObjectEntity = new ObjectInstance();
                createObjectEntity.ObjType = objectEntity.ObjType;
                createObjectEntity.ObjID = objectEntity.ObjID;
                createObjectEntity.ObjSpec = objectEntity.ObjSpec;
                List<ObjectAttribute> listObjectAttributes = new List<ObjectAttribute>();
                foreach (ObjectAttribute objAttr in e.ListObjectEntities[0].ListObjectAttributes)
                {
                    createObjectEntity.ListObjectAttributes.Add(objAttr);
                }

                switch (objectEntity.ObjType.ToUpper())
                {
                    case ObjectTypeKey.PROCESSJOB:                 
                        bAck = PJ_list.Contains(objectEntity.ObjID) ? byte.Parse("1") : byte.Parse("0");  //檢查pj是否重複

                        switch (createType)
                        {
                            case eReceiveCreateMessageName.S16F11:
                            case eReceiveCreateMessageName.S16F15:
                                if (bAck == 1)
                                {
                                    ErrorReport er = new ErrorReport();
                                    er.ErrorCode = 11;
                                    er.ErrorText = "object ID in use";
                                    listErrorReports.Add(er);
                                }
                                int result = _gemControler.CreateObjectRequestCommandReply(createType, listObjectEntities, bAck, listErrorReports, uSystemBytes, out err);

                                PJ_list.Add(objectEntity.ObjID);
                                ChangeProcessJobState(objectEntity.ObjID, ProcessJobState.QUEUED);
                                break;
                            default:
                                break;
                        }
                        break;
                    case ObjectTypeKey.CONTROLJOB:
                        bAck = CJ_list.Contains(objectEntity.ObjID) ? byte.Parse("1") : byte.Parse("0");  //檢查cj是否重複
                        switch (createType)
                        {
                            case eReceiveCreateMessageName.S14F9:

                                listObjectEntities.Add(createObjectEntity);
                                if (bAck == 1)
                                {
                                    ErrorReport er = new ErrorReport();
                                    er.ErrorCode = 11;
                                    er.ErrorText = "object ID in use";
                                    listErrorReports.Add(er);
                                }
                                int result = _gemControler.CreateObjectRequestCommandReply(createType, listObjectEntities, bAck, listErrorReports, uSystemBytes, out err);
                                if (objectEntity.ObjType.ToUpper() == ObjectTypeKey.CONTROLJOB)
                                {
                                    CJ_list.Add(objectEntity.ObjID);
                                    ChangeControlJobState(objectEntity.ObjID, ControlJobState.QUEUED, 0);
                                }
                                if(CJ_list.Count == 1)
                                {
                                    ChangeControlJobState(objectEntity.ObjID, ControlJobState.SELECTED, 0);  //只有一個人就直接選了
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case ObjectTypeKey.CARRIER:
                        bAck = Carrier_list.Contains(objectEntity.ObjID) ? byte.Parse("1") : byte.Parse("0");  //檢查carrier是否重複
                        switch (createType)
                        {
                            case eReceiveCreateMessageName.S14F9:

                                listObjectEntities.Add(createObjectEntity);
                                if (bAck == 1)
                                {
                                    ErrorReport er = new ErrorReport();
                                    er.ErrorCode = 11;
                                    er.ErrorText = "object ID in use";
                                    listErrorReports.Add(er);
                                }
                                int result = _gemControler.CreateObjectRequestCommandReply(createType, listObjectEntities, bAck, listErrorReports, uSystemBytes, out err);
                                if (objectEntity.ObjType.ToUpper() == ObjectTypeKey.CARRIER)
                                {
                                    Carrier_list.Add(objectEntity.ObjID);
                                    _gemControler.EventReportSend(181, out err); //CMS_NoStateToCarrier
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case ObjectTypeKey.SUBSTRATE:
                        bAck = Substrate_list.Contains(objectEntity.ObjID) ? byte.Parse("1") : byte.Parse("0");  //檢查substrate是否重複
                        switch (createType)
                        {
                            case eReceiveCreateMessageName.S14F9:

                                listObjectEntities.Add(createObjectEntity);
                                if (bAck == 1)
                                {
                                    ErrorReport er = new ErrorReport();
                                    er.ErrorCode = 11;
                                    er.ErrorText = "object ID in use";
                                    listErrorReports.Add(er);
                                }
                                int result = _gemControler.CreateObjectRequestCommandReply(createType, listObjectEntities, bAck, listErrorReports, uSystemBytes, out err);
                                if (objectEntity.ObjType.ToUpper() == ObjectTypeKey.SUBSTRATE)
                                {
                                    Substrate_list.Add(objectEntity.ObjID);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        {
                            //Handle

                            //Reply
                            //Call _gemControler.CreateObjectRequestCommandReply

                            //Sample Use Button Reply 
                            //private void btnCreate_Reply_Click(object sender, EventArgs e)
                        }
                        break;
                }
            }
        }

        private void btnCreate_Reply_Click(object sender, EventArgs e)
        {
            if (bCanReplyCreateObjectRequestCommand)
            {
                string strObjType = txtS14F9_OBJTYPE_Recv.Text;
                string strObjID = txtS14F9_OBJID_Recv.Text;
                string strObjSpec = txtS14F9_OBJSPEC_Recv.Text;
                byte bAck = byte.Parse(txtS14F9_OBJACK_Send.Text);
                uint uSystemBytes = uint.Parse(txtS14F9_SystemBytes_Recv.Text);
                eReceiveCreateMessageName createType = (eReceiveCreateMessageName)Enum.Parse(typeof(eReceiveCreateMessageName), txtS14F9_CreateOBJ_Type.Text);

                string err;

                List<ObjectInstance> listObjectEntities = new List<ObjectInstance>();
                List<ErrorReport> listErrorReports = new List<ErrorReport>();

                switch (createType)
                {
                    case eReceiveCreateMessageName.S14F9:
                    default:
                        ObjectInstance createObjectEntity = new ObjectInstance();
                        createObjectEntity.ObjType = strObjType;
                        createObjectEntity.ObjID = strObjID;
                        createObjectEntity.ObjSpec = strObjSpec;

                        List<ObjectAttribute> listObjectAttributes = new List<ObjectAttribute>();

                        foreach (ObjectAttribute objAttr in lastRecvListObjectInstance[0].ListObjectAttributes)
                        {
                            createObjectEntity.ListObjectAttributes.Add(objAttr);
                        }

                        //ErrorReport objectErrors = new ErrorReport();
                        //objectErrors.ErrorCode = 3;
                        //objectErrors.ErrorText = "TestError3";
                        //listErrorReports.Add(objectErrors);
                        //objectErrors = new ErrorReport();
                        //objectErrors.ErrorCode = 5;
                        //objectErrors.ErrorText = "TestError5";
                        //listErrorReports.Add(objectErrors);

                        listObjectEntities.Add(createObjectEntity);
                        break;
                }

                _gemControler.CreateObjectRequestCommandReply(createType, listObjectEntities, bAck, listErrorReports, uSystemBytes, out err);
                bCanReplyCreateObjectRequestCommand = false;
            }
            else
            {
                string log = string.Format("Can't Reply Command, Because Not Receive Command!");
                WriteLog(LogLevel.Warn, log);
                MessageBox.Show(log);
            }
        }
        //EQP Device UpdateObject
        private void btnUpdateObject_ProcessJob_Click(object sender, EventArgs e)
        {
            string err;
            ObjectInstance updateObjectEntity = new ObjectInstance();
            updateObjectEntity.ObjType = ObjectTypeKey.PROCESSJOB;
            updateObjectEntity.ObjID = "PRJOBID01";
            updateObjectEntity.ObjSpec = string.Empty;

            ObjectAttribute objectAttribute = new ObjectAttribute(141, ObjectAttributeKey.OBJID, "PRJOBID01");
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(143, ObjectAttributeKey.ProcessJob.PRJOBSTATE, (byte)1);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);

            _gemControler.UpdateObject(updateObjectEntity, out err);
        }

        private void _gemControler_DeleteObjectRequestCommand(object sender, DeleteObjectRequestArgs e)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                txtS14F11_OBJSPEC_Recv.Text = e.ObjSpec.ToString();
                txtS14F11_SystemBytes_Recv.Text = e.SystemBytes.ToString();
            });
            bCanReplyDeleteObjectRequestCommand = true;

            byte bAck = 0;
            List<ObjectAttribute> listObjectAttributes = new List<ObjectAttribute>();
            //Handle
            switch (e.ObjSpec)
            {
                case ObjectTypeKey.PROCESSJOB:
                    break;
                case ObjectTypeKey.CONTROLJOB:
                    //ddd
                    break;
                case ObjectTypeKey.CARRIER:
                    break;
            }
            //e.ListObjectAttributes.
            ObjectAttribute objectAttribute = new ObjectAttribute(ObjectAttributeKey.OBJID, "");
            listObjectAttributes.Add(objectAttribute);

            List<ErrorReport> listErrorReports = new List<ErrorReport>();
            ErrorReport objectErrors = new ErrorReport();
            objectErrors.ErrorCode = 3;
            objectErrors.ErrorText = "TestError3";
            listErrorReports.Add(objectErrors);
            objectErrors = new ErrorReport();
            objectErrors.ErrorCode = 5;
            objectErrors.ErrorText = "TestError5";
            listErrorReports.Add(objectErrors);

            //Reply
            _gemControler.DeleteObjectRequestCommandReply(listObjectAttributes, bAck, listErrorReports, e.SystemBytes, out err);

            //Ex: 
            //OK, Call EventReportSend
            //System Delete Object Event
            //PROCESSJOB => PJ_ProcessCompleteToComplete, PJ_AbortingToComplete, PJ_StoppingToComplete, PJ_QueuedToComplete
            //CONTROLJOB => CJ_QueuedToComplete, CJ_CompletedToComplete
            //SUBSTRATE  => STS_Transport_AtDestinationToExtinction, STS_AnySubstateSubStateToExtinction
            //CARRIER    => CMS_CarrierToNoState
        }

        private void btnS14F12_Reply_Click(object sender, EventArgs e)
        {
            if (bCanReplyDeleteObjectRequestCommand)
            {
                string strObjSpec = txtS14F11_OBJSPEC_Recv.Text;
                byte bAck = byte.Parse(txtS14F11_OBJACK_Send.Text);
                uint uSystemBytes = uint.Parse(txtS14F11_SystemBytes_Recv.Text);

                string err;

                List<ObjectAttribute> listObjectAttributes = new List<ObjectAttribute>();
                ObjectAttribute objectAttribute = new ObjectAttribute("OBJID", "PRJOBID01");
                listObjectAttributes.Add(objectAttribute);

                List<ErrorReport> listErrorReports = new List<ErrorReport>();
                ErrorReport objectErrors = new ErrorReport();
                objectErrors.ErrorCode = 3;
                objectErrors.ErrorText = "TestError3";
                listErrorReports.Add(objectErrors);
                objectErrors = new ErrorReport();
                objectErrors.ErrorCode = 5;
                objectErrors.ErrorText = "TestError5";
                listErrorReports.Add(objectErrors);

                _gemControler.DeleteObjectRequestCommandReply(listObjectAttributes, bAck, listErrorReports, uSystemBytes, out err);
                bCanReplyDeleteObjectRequestCommand = false;
            }
            else
            {
                string log = string.Format("Can't Reply Command, Because Not Receive Command!");
                WriteLog(LogLevel.Warn, log);
                MessageBox.Show(log);
            }
        }

        private void btnProcessJobEventNotify_Click(object sender, EventArgs e)
        {
            string err = string.Empty;
            string strObjID = txtS16F9_OBJID.Text;
            eProcessingEventID eProcessingEventID = (eProcessingEventID)ulong.Parse(txtS16F9_PREVENTID.Text);
            string[] strVIDs = txtS16F9_VIDs.Text.Split(',');
            List<ulong> listVids = new List<ulong>();
            foreach (string vid in strVIDs)
            {
                listVids.Add(ulong.Parse(vid));
            }

            _gemControler.ProcessJobEventNotify(strObjID, eProcessingEventID, listVids, out err);
        }

        private void btnProcessJobAlertNotify_Click(object sender, EventArgs e)
        {
            string err = string.Empty;
            string strObjID = txtS16F7_OBJID.Text;
            eProcessingStatus eProcessingEventID = (eProcessingStatus)ulong.Parse(txtS16F7_PRJOBMILESTONE.Text);
            bool bAck = bool.Parse(txtS16F7_OBJACKA.Text);

            List<ErrorReport> listErrorReports = new List<ErrorReport>();
            ErrorReport objectErrors = new ErrorReport();
            objectErrors.ErrorCode = 3;
            objectErrors.ErrorText = "TestError3";
            listErrorReports.Add(objectErrors);
            objectErrors = new ErrorReport();
            objectErrors.ErrorCode = 5;
            objectErrors.ErrorText = "TestError5";
            listErrorReports.Add(objectErrors);

            _gemControler.ProcessJobAlertNotify(strObjID, eProcessingEventID, bAck, listErrorReports, out err);
        }

        private void _gemControler_ControlJobCommand(object sender, ControlJobCommandArgs e)
        {
            txtS16F27_JobId.Text = e.CTLJOBID;
            txtS16F27_JobCmd.Text = e.CTLJOBCMD.ToString();
            if (e.ReceiveCommandParameters != null)
            {
                txtS16F27_CPName.Text = e.ReceiveCommandParameters.Name;
                txtS16F27_CPVal.Text = e.ReceiveCommandParameters.Value.ToString();
            }
            else
            {
                txtS16F27_CPName.Text = string.Empty;
                txtS16F27_CPVal.Text = string.Empty;
            }

            txtS16F27_Systembytes.Text = e.SystemBytes.ToString();
        }

        private void btnControlJobCommandAck_Click(object sender, EventArgs e)
        {
            string log = string.Empty;
            string err = string.Empty;
            int result = 0;
            uint systemBytes = uint.Parse(txtS16F27_Systembytes.Text);
            bool bAck = bool.Parse(txtS16F28_Acka.Text);

            if (string.IsNullOrWhiteSpace(txtS16F28_ErrCode.Text) && string.IsNullOrWhiteSpace(txtS16F28_ErrText.Text))
            {
                result = _gemControler.ControlJobCommandReply(bAck, null, systemBytes, out err);
            }
            else
            {
                ErrorReport cmdError = new ErrorReport();
                cmdError.ErrorCode = ulong.Parse(txtS16F28_ErrCode.Text);
                cmdError.ErrorText = txtS16F28_ErrText.Text;

                result = _gemControler.ControlJobCommandReply(bAck, cmdError, systemBytes, out err);
            }

            if (result != 0)
            {
                log = string.Format("ControlJobCommandReply Fialure, ({0})Reason:{1}", result, err);
                WriteLog(LogLevel.Warn, log);
            }
        }

        private void _gemControler_CarrierActionRequest(object sender, CarrierActionRequestArgs e)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                txtS3F17_CARRIERACTION_Recv.Text = e.CARRIERACTION.ToString();
                txtS3F17_CARRIERID_Recv.Text = e.CARRIERID.ToString();
                txtS3F17_PTN_Recv.Text = e.PORTNO.ToString();
                txtS3F17_SystemBytes_Recv.Text = e.SystemBytes.ToString();
            });
            bCanReplyCarrierActionRequest = true;

            //Handle
            byte ack = 0;
            byte btHCACK = 0;
            List<ErrorReport> listErrorReports = new List<ErrorReport>();
            List<string> dataList = new List<string>();
            ErrorReport errR = new ErrorReport();
            switch (e.CARRIERACTION)
            {
                case "PROCEEDWITHCARRIER":
                    LoadPortID = e.PORTNO.ToString();
                    CarrierID = e.CARRIERID;
                    SlotMapData.Set(LoadPortID, CarrierID);
                    dataList.Clear();
                    dataList.Add("PROCEEDWITHCARRIER");
                    dataList.Add(CarrierID);
                    dataList.Add(LoadPortID);
                    errR = new ErrorReport();
                    if (LoadPortID == "1" || LoadPortID == "2")
                    {
                        ack = function_CallBack(dataList);
                        if (ack != 0)
                        {
                            btHCACK = ack;
                            errR.ErrorCode = 22;
                            errR.ErrorText = "failure when processing";
                        }
                    }
                    else
                    {
                        btHCACK = 3;

                        errR.ErrorCode = 47;
                        errR.ErrorText = "invalid parameter";
                    }
                    listErrorReports.Add(errR);
                    break;
                case "PROCEEDWITHCARRIER2":
                    if(Carrier_list.Contains(e.CARRIERID))
                    {
                        SetCarrierStatus_ID(e.CARRIERID, CarrierIDState.ID_VERIFICATION_OK);
                    }
                    else
                    {
                        errR = new ErrorReport();
                        btHCACK = 3;

                        errR.ErrorCode = 47;
                        errR.ErrorText = "invalid parameter. CarrierID is not found";
                    }
                    break;
                case "CARRIERRELEASE":
                    LoadPortID = e.PORTNO.ToString();
                    CarrierID = e.CARRIERID;
                    ReleaseData.Set(LoadPortID, CarrierID);
                    dataList.Clear();
                    dataList.Add("CARRIERRELEASE");
                    dataList.Add(CarrierID);
                    dataList.Add(LoadPortID);
                    errR = new ErrorReport();
                    if (LoadPortID == "1" || LoadPortID == "2")
                    {
                        ack = function_CallBack(dataList);
                        if (ack != 0)
                        {
                            btHCACK = ack;
                            errR.ErrorCode = 22;
                            errR.ErrorText = "failure when processing";
                        }
                    }
                    else
                    {
                        btHCACK = 3;

                        errR.ErrorCode = 47;
                        errR.ErrorText = "invalid parameter";
                    }
                    listErrorReports.Add(errR);
                    break;
                case "CANCELCARRIER":
                    break;
                case "CARRIEROUT":
                    break;
                case "CARRIERRECREATE":
                    break;
                case "CANCELCARRIERATPORT":
                    break;
            }

            //Reply
            _gemControler.CarrierActionRequestReply(btHCACK, listErrorReports, e.SystemBytes, out err);

        }

        private void btnCarrierActionAck_Click(object sender, EventArgs e)
        {
            if (bCanReplyCarrierActionRequest)
            {
                byte bAck = byte.Parse(txtS3F18_Caack.Text);
                uint uSystemBytes = uint.Parse(txtS3F17_SystemBytes_Recv.Text);

                string err;

                List<ErrorReport> listErrorReports = new List<ErrorReport>();
                ErrorReport error = new ErrorReport();
                error.ErrorCode = ulong.Parse(txtS3F18_ErrCode.Text);
                error.ErrorText = txtS3F18_ErrText.Text;
                listErrorReports.Add(error);
                error = new ErrorReport();
                error.ErrorCode = 5;
                error.ErrorText = "TestError5";
                listErrorReports.Add(error);

                _gemControler.CarrierActionRequestReply(bAck, listErrorReports, uSystemBytes, out err);
                bCanReplyCarrierActionRequest = false;
            }
            else
            {
                string log = string.Format("Can't Reply Request, Because Not Receive Request!");
                WriteLog(LogLevel.Warn, log);
                MessageBox.Show(log);
            }
        }

        private void _gemControler_SetAttrRequestCommand(object sender, SetAttrRequestArgs e)
        {
            //Handle

            //Call _gemControler.SetAttrRequestCommandReply

            //Ex: OK, Call UpdateObject API
        }

        private void _gemControler_ProcessJobCommand(object sender, ProcessJobCommandArgs e)
        {
            string log = string.Empty;
            string err = string.Empty;
            uint systemBytes = e.SystemBytes;
            bool ack = true;
            string prJobId = e.PRJOBID;
            string prJobCmd = e.PRCMD;

            foreach (ProcessJobCommandParameter commandParameter in e.ReceiveCommandParameters)
            {
                string name = commandParameter.Name;
                string value = commandParameter.Value.ToString();
            }

            List<ErrorReport> errorReports = new List<ErrorReport>();
            ErrorReport error1 = new ErrorReport();
            error1.ErrorCode = 100;
            error1.ErrorText = "error 100";
            errorReports.Add(error1);
            ErrorReport error2 = new ErrorReport();
            error2.ErrorCode = 200;
            error2.ErrorText = "error 200";
            errorReports.Add(error2);

            ack = errorReports.Count == 0 ? true : false;

            int result = _gemControler.ProcessJobCommandReply(prJobId, ack, errorReports, systemBytes, out err);

            if (result != 0)
            {
                log = string.Format("ProcessJobCommandReply Fialure, ({0})Reason:{1}", result, err);
                WriteLog(LogLevel.Warn, log);
            }
        }

        private void _gemControler_ProcessJobDequeue(object sender, ProcessJobDequeueArgs e)
        {
            string log = string.Empty;
            uint systembytes = e.SystemBytes;
            int idx = 0;

            List<string> removedJobIds = new List<string>();
            List<ErrorReport> errorReports = new List<ErrorReport>();
            bool ack = true;

            foreach (string prJobId in e.PRJOBIDs)
            {
                if (idx % 2 == 0)
                {
                    removedJobIds.Add(prJobId);
                }
                else
                {
                    ErrorReport errorReport = new ErrorReport();
                    errorReport.ErrorCode = 111;
                    errorReport.ErrorText = $"JobId({prJobId}) can not dequeue.";
                    errorReports.Add(errorReport);
                }
                idx++;
            }

            string err;
            int result = _gemControler.ProcessJobDequeueReply(removedJobIds, ack, errorReports, systembytes, out err);

            if (result != 0)
            {
                log = string.Format("ProcessJobDequeueReply Fialure, ({0})Reason:{1}", result, err);
                WriteLog(LogLevel.Warn, log);
            }
        }

        private void _gemControler_CancelAllCarrierOutRequest(object sender, CancelAllCarrierOutRequestArgs e)
        {
            //Handle

            //Reply
            //Call _gemControler.CancelAllCarrierOutRequestReply
        }

        private void _gemControler_PortChangeAccessRequest(object sender, PortChangeAccessRequestArgs e)
        {
            //Handle
            bDataCheckOK = false;
            List<string> dataList = new List<string>();
            List<ErrorReport> errList = new List<ErrorReport>();
            byte ack = 0;
            byte btHCACK = 0;

            foreach (byte p in e.PORTNOs)
            {
                LoadPortID = p.ToString();
                AccessMode = e.ACCESSMODE.ToString();
                dataList.Clear();
                dataList.Add("ACCESSMODE-CHANGE");
                dataList.Add(AccessMode);
                dataList.Add(LoadPortID);
                ErrorReport errR = new ErrorReport();
                if ((AccessMode == "0" || AccessMode == "1") && (LoadPortID == "1" || LoadPortID == "2"))
                {
                    ack = function_CallBack(dataList);
                    if (ack != 0)
                    {
                        btHCACK = ack;
                        errR.ErrorCode = 22;
                        errR.ErrorText = "failure when processing";
                    }
                }
                else
                {
                    btHCACK = 3;
                    
                    errR.ErrorCode = 47;
                    errR.ErrorText = "invalid parameter";
                }
                errList.Add(errR);
            }
            
            //Reply
            _gemControler.PortChangeAccessReply(e.ReceiveMessageName, btHCACK, errList, e.SystemBytes, out err);
        }

        private void _gemControler_PortActionRequest(object sender, PortActionRequestArgs e)
        {
            //Handle

            //Reply
            //Call _gemControler.PortActionRequestReply
        }

        private void _gemControler_CarrierTagReadRequest(object sender, CarrierTagReadRequestArgs e)
        {
            //Handle

            //Reply
            //Call _gemControler.CarrierTagReadRequestReply
        }

        private void _gemControler_CarrierTagWriteDataRequest(object sender, CarrierTagWriteDataRequestArgs e)
        {
            //Handle

            //Reply
            //Call _gemControler.CarrierTagWriteDataRequestReply
        }


        //Loopback Diagnostic----------------------------------------------------------------------------------------------------
        private void btnLoopbackDiagnostic_Click(object sender, EventArgs e)
        {
            string err;
            _gemControler.LoopbackDiagnosticRequest(TestABS, out err);
        }

        private void _gemControler_LoopbackDiagnosticData(object sender, LoopbackDiagnosticDataArgs e)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                txtABS_Recv.Text = e.ABS[0].ToString();
                txtABS_RecvSystemBytes.Text = e.SystemBytes.ToString();

                lblABS_Check.Text = $" Check = {TestABS[0] == e.ABS[0]}";
            });
        }

        // GEM300

        public int CreateProcessJob(string objID, string recID, bool bStart, string carrierID, byte[] slot, out string err, string objSpec = "")
        {
            int result = 0;
            int result2 = 0;
            //CreateObject
            result = _gemControler.CreateObject(ObjectTypeKey.PROCESSJOB, objID, out err, objSpec);

            if (result != 0) return result;
            ObjectInstance updateObjectEntity = new ObjectInstance();
            updateObjectEntity.ObjType = ObjectTypeKey.PROCESSJOB;
            updateObjectEntity.ObjID = objID;
            updateObjectEntity.ObjSpec = objSpec;

            ObjectAttribute objectAttribute = new ObjectAttribute(141, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(143, ObjectAttributeKey.ProcessJob.PRJOBSTATE, (byte)ProcessJobState.QUEUED);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            ListWrapper lw = new ListWrapper();
            ListWrapper lw_1 = new ListWrapper();
            lw.TryAdd(ItemFmt.L, lw_1, out err);
            lw_1.TryAdd(ItemFmt.A, carrierID, out err);
            List<byte> lsts = new List<byte>();
            for (byte i = 0; i < 25; i++)
            {
                if (slot[i] == 1)
                    lsts.Add(i);
            }
            lw_1.TryAdd(ItemFmt.U1, lsts.ToArray(), out err);
            objectAttribute = new ObjectAttribute(144, ObjectAttributeKey.ProcessJob.PRMTLNAMELIST, lw);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(145, ObjectAttributeKey.ProcessJob.PRMTLTYPE, (byte)13);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(146, ObjectAttributeKey.ProcessJob.PRPROCESSSTART, bStart);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(147, ObjectAttributeKey.ProcessJob.PRRECIPEMETHOD, (byte)0);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(148, ObjectAttributeKey.ProcessJob.RECID, recID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            result2 = _gemControler.UpdateObject(updateObjectEntity, out err);
            PJ_list.Add(objID);
            //ObjectInstance updateObjectEntity2 = null;
            //result = gtlhomeSECS._gemControler.GetObject(ObjectTypeKey.PROCESSJOB, objID, out updateObjectEntity2, out err);
            return result;
        }

        public int SetProcessJobAttr(string objID, string pauseEvent, string carrierID, byte[] slot, byte PRType, bool bStart, byte recMethod, string recID, string recVarList, out string err, string objSpec = "")
        {
            int result = 0;
            ObjectInstance updateObjectEntity = new ObjectInstance();
            updateObjectEntity.ObjType = ObjectTypeKey.PROCESSJOB;
            updateObjectEntity.ObjID = objID;
            updateObjectEntity.ObjSpec = objSpec;
            ListWrapper lw, lw_1;
            ObjectAttribute objectAttribute = new ObjectAttribute(141, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            if (!(pauseEvent == string.Empty || pauseEvent == null))
            {
                string[] s = pauseEvent.Split(',');
                uint[] os = new uint[s.Length];
                for (int i = 0; i < s.Length; i++)
                    os[i] = Convert.ToUInt32(s[i]);
                lw = new ListWrapper();
                lw.TryAdd(ItemFmt.U4, os, out err);
                objectAttribute = new ObjectAttribute(142, ObjectAttributeKey.ProcessJob.PAUSEEVENT, lw);
                updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            }
            /*if (PJState != 255)
            {
                objectAttribute = new ObjectAttribute(143, ObjectAttributeKey.ProcessJob.PRJOBSTATE, PJState);
                updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            }*/
            if (slot != null)
            {
                lw = new ListWrapper();
                lw_1 = new ListWrapper();
                lw.TryAdd(ItemFmt.L, lw_1, out err);
                lw_1.TryAdd(ItemFmt.A, carrierID, out err);
                lw_1.TryAdd(ItemFmt.U1, slot, out err);
                objectAttribute = new ObjectAttribute(144, ObjectAttributeKey.ProcessJob.PRMTLNAMELIST, lw);
                updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            }
            objectAttribute = new ObjectAttribute(145, ObjectAttributeKey.ProcessJob.PRMTLTYPE, PRType);  // 13 Carrier  14 substrate
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(146, ObjectAttributeKey.ProcessJob.PRPROCESSSTART, bStart);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            if (recMethod != null)
            {
                objectAttribute = new ObjectAttribute(147, ObjectAttributeKey.ProcessJob.PRRECIPEMETHOD, recMethod);  //0 - recipe without variable tuning   1 - recipe with variable tuning
                updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            }
            objectAttribute = new ObjectAttribute(148, ObjectAttributeKey.ProcessJob.RECID, recID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            if (!(recVarList == string.Empty || recVarList == null))
            {
                string[] s1 = recVarList.Split(';');
                if (s1.Length > 0)
                {
                    lw = new ListWrapper();
                    foreach (string s in s1)
                    {
                        if (s != String.Empty)
                        {
                            string[] s2 = s.Split(',');
                            if (s2.Length == 2)
                            {
                                lw_1 = new ListWrapper();
                                lw_1.TryAdd(ItemFmt.A, s2[0], out err);
                                lw_1.TryAdd(ItemFmt.A, s2[1], out err);
                                lw.TryAdd(ItemFmt.L, lw_1, out err);
                            }
                        }
                    }
                    objectAttribute = new ObjectAttribute(149, ObjectAttributeKey.ProcessJob.RECVARIABLELIST, lw);
                    updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
                }
            }

            result = _gemControler.UpdateObject(updateObjectEntity, out err);

            //ObjectInstance updateObjectEntity2 = null;
            //result = gtlhomeSECS._gemControler.GetObject(ObjectTypeKey.PROCESSJOB, objID, out updateObjectEntity2, out err);
            return result;
        }

        public int GetProcessJobAttr(string objID, out string pauseEvent, out byte PJState, out string carrierID, out byte[] slot, out byte PRType, out bool bStart, out byte recMethod, out string recID, out string recVarList, out string err)
        {
            int result = 0;
            ObjectInstance updateObjectEntity = null;
            result = _gemControler.GetObject(ObjectTypeKey.PROCESSJOB, objID, out updateObjectEntity, out err);

            ListWrapper lw, lw_1;
            SecsIIValue val;
            pauseEvent = string.Empty;
            PJState = 0;
            carrierID = string.Empty;
            slot = new byte[25];
            PRType = 0x0D;
            bStart = false;
            recMethod = 0;
            recID = string.Empty;
            recVarList = string.Empty;
            if (result != 0) return result;
            foreach (ObjectAttribute o in updateObjectEntity.ListObjectAttributes)
            {
                switch (o.ATTRID)
                {
                    case ObjectAttributeKey.ProcessJob.PAUSEEVENT:
                        lw = (ListWrapper)o.ATTRDATA;
                        pauseEvent = string.Empty;
                        for (int i = 0; i < lw.Items.Count; i++)
                        {
                            uint[] b = (uint[])lw.Items[i].Value;
                            for (int j = 0; j < b.Length; ++j)
                            {
                                if (i == 0 && j == 0)
                                {
                                    pauseEvent += Convert.ToString((b as uint[])[j]);
                                }
                                else
                                {
                                    pauseEvent += ',' + Convert.ToString((b as uint[])[j]);
                                }
                            }
                        };
                        break;
                    case ObjectAttributeKey.ProcessJob.PRJOBSTATE:
                        PJState = Convert.ToByte(o.ATTRDATA.ToString());
                        break;
                    case ObjectAttributeKey.ProcessJob.PRMTLNAMELIST:
                        lw = (ListWrapper)o.ATTRDATA;
                        lw_1 = (ListWrapper)lw.Items[0].Value;
                        carrierID = lw_1.Items[0].ToString();
                        byte[] slot2 = (byte[])lw_1.Items[1].Value;
                        foreach (byte a in slot2)
                        {
                            slot[a] = 1;
                        }
                        break;
                    case ObjectAttributeKey.ProcessJob.PRMTLTYPE:
                        PRType = Convert.ToByte(o.ATTRDATA.ToString());
                        break;
                    case ObjectAttributeKey.ProcessJob.PRPROCESSSTART:
                        bStart = Convert.ToBoolean(o.ATTRDATA.ToString());
                        break;
                    case ObjectAttributeKey.ProcessJob.PRRECIPEMETHOD:
                        recMethod = Convert.ToByte(o.ATTRDATA.ToString());
                        break;
                    case ObjectAttributeKey.ProcessJob.RECID:
                        recID = o.ATTRDATA.ToString();
                        break;
                    case ObjectAttributeKey.ProcessJob.RECVARIABLELIST:
                        lw = (ListWrapper)o.ATTRDATA;
                        recVarList = string.Empty;
                        for (int i = 0; i < lw.Items.Count; i++)
                        {
                            lw_1 = (ListWrapper)lw.Items[i].Value;
                            recVarList += lw_1.Items[0].ToString() + ',' + lw_1.Items[1].ToString() + ';';
                        };
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        public int DeleteProcessJob(string objID, out string errstr)
        {
            string err;
            int result;
            ObjectInstance updateObjectEntity = new ObjectInstance();
            updateObjectEntity.ObjType = ObjectTypeKey.PROCESSJOB;
            updateObjectEntity.ObjID = objID;
            updateObjectEntity.ObjSpec = string.Empty;

            ObjectAttribute objectAttribute = new ObjectAttribute(141, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(143, ObjectAttributeKey.ProcessJob.PRJOBSTATE, (byte)4);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);

            result = _gemControler.UpdateObject(updateObjectEntity, out err);
            if (result == 0)
            {
                errstr = err;
                result = _gemControler.EventReportSend(47, out err);
                if (result == 0)  //Offline就刪不掉，list也就不刪了
                    PJ_list.Remove(objID);
            }
            else
            {
                errstr = err;
                return result;
            }
            errstr = err;
            return result;
        }

        public int ChangeProcessJobState(string objID, ProcessJobState state)
        {
            int result = 0;
            ObjectInstance updateObjectEntity = null;
            ProcessJobState oldstate = ProcessJobState.QUEUED;

            result = _gemControler.GetObject(ObjectTypeKey.CONTROLJOB, objID, out updateObjectEntity, out err);
            foreach (ObjectAttribute o in updateObjectEntity.ListObjectAttributes)
            {
                if (o.ATTRID == ObjectAttributeKey.ProcessJob.PRJOBSTATE)
                    oldstate = (ProcessJobState)Convert.ToByte(o.ATTRDATA);
            }
            ObjectInstance updateObjectEntity2 = new ObjectInstance();
            updateObjectEntity2.ObjType = updateObjectEntity.ObjType;
            updateObjectEntity2.ObjID = updateObjectEntity.ObjID;
            updateObjectEntity2.ObjSpec = updateObjectEntity.ObjSpec;
            ObjectAttribute objectAttribute2 = new ObjectAttribute(143, ObjectAttributeKey.ProcessJob.PRJOBSTATE, (byte)state);
            updateObjectEntity2.ListObjectAttributes.Add(objectAttribute2);
            result = _gemControler.UpdateObject(updateObjectEntity, out err);

            if (result == 0)
            {
                /*
                 QUEUED = 0,
                 SETTING_UP = 1,
                 WAITING_FOR_START = 2,
                 PROCESSING = 3,
                 PROCESS_COMPLETE = 4,
                 RESERVED = 5,
                 PAUSING = 6, 
                 PAUSED = 7,
                 STOPPING = 8,
                 ABORTING = 9,
                 STOPPED = 10,
                 ABORTED = 11,
                 UNQUEUED = 12,
                */
                switch (state)
                {
                    case ProcessJobState.QUEUED:
                            _gemControler.EventReportSend(41, out err);
                        break;
                    case ProcessJobState.SETTING_UP:
                        if (oldstate == ProcessJobState.QUEUED)
                            _gemControler.EventReportSend(42, out err);
                        break;
                    case ProcessJobState.WAITING_FOR_START:
                        if (oldstate == ProcessJobState.SETTING_UP)
                            _gemControler.EventReportSend(43, out err);
                        break;
                    case ProcessJobState.PROCESSING:
                        if (oldstate == ProcessJobState.SETTING_UP)
                            _gemControler.EventReportSend(44, out err);
                        else if (oldstate == ProcessJobState.WAITING_FOR_START)
                            _gemControler.EventReportSend(45, out err);
                        else if (oldstate == ProcessJobState.PAUSED)
                            _gemControler.EventReportSend(50, out err);
                        break;
                    case ProcessJobState.PROCESS_COMPLETE:
                        if (oldstate == ProcessJobState.PROCESSING)
                        {
                            _gemControler.EventReportSend(46, out err);
                            result = _gemControler.EventReportSend(47, out err);  //PJ_ProcessCompleteToComplete 刪除PJ
                            if (result == 0)  //Offline就刪不掉，list也就不刪了
                                PJ_list.Remove(objID);
                        }
                        break;
                    case ProcessJobState.PAUSING:
                        if (oldstate == ProcessJobState.PROCESSING)
                            _gemControler.EventReportSend(48, out err);  
                        break;
                    case ProcessJobState.PAUSED:
                        if (oldstate == ProcessJobState.PAUSING)
                            _gemControler.EventReportSend(49, out err);
                        break;
                    case ProcessJobState.STOPPING:
                        if (oldstate == ProcessJobState.PROCESSING)
                            _gemControler.EventReportSend(51, out err);
                        else if (oldstate == ProcessJobState.PAUSED)
                            _gemControler.EventReportSend(52, out err);
                        break;
                    case ProcessJobState.ABORTING:
                        if (oldstate == ProcessJobState.PROCESSING)
                            _gemControler.EventReportSend(53, out err);
                        else if (oldstate == ProcessJobState.STOPPING)
                            _gemControler.EventReportSend(54, out err);
                        else if (oldstate == ProcessJobState.PAUSED)
                            _gemControler.EventReportSend(55, out err);
                        break;
                    case ProcessJobState.ABORTED:
                        if (oldstate == ProcessJobState.ABORTING)
                        {
                            result = _gemControler.EventReportSend(56, out err); //PJ_AbortingToComplete 刪除PJ
                            if (result == 0)  //Offline就刪不掉，list也就不刪了
                                PJ_list.Remove(objID);
                        }                          
                        break;
                    case ProcessJobState.STOPPED:
                        if (oldstate == ProcessJobState.STOPPING)
                        {
                            _gemControler.EventReportSend(57, out err); //PJ_StoppingToComplete 刪除PJ
                            if (result == 0)  //Offline就刪不掉，list也就不刪了
                                PJ_list.Remove(objID);
                        }
                        break;
                    case ProcessJobState.UNQUEUED:
                        if (oldstate == ProcessJobState.QUEUED)
                        {
                            _gemControler.EventReportSend(58, out err); //PJ_QueuedToComplete 刪除PJ
                            if (result == 0)  //Offline就刪不掉，list也就不刪了
                                PJ_list.Remove(objID);
                        }
                        break;
                }
            }
            return result;
        }

        public int CreateControlJob(string objID, string CarrierInputSpec, string dcp, string[] PRJob, byte PROrder, bool bStart, out string err, string objSpec = "")
        {
            int result = 0;
            int result2 = 0;
            //CreateObject
            result = _gemControler.CreateObject(ObjectTypeKey.CONTROLJOB, objID, out err, objSpec);

            if (result != 0) return result;
            ObjectInstance updateObjectEntity = new ObjectInstance();
            updateObjectEntity.ObjType = ObjectTypeKey.CONTROLJOB;
            updateObjectEntity.ObjID = objID;
            updateObjectEntity.ObjSpec = objSpec;

            ObjectAttribute objectAttribute = new ObjectAttribute(241, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            string[] s = CarrierInputSpec.Split(',');
            ListWrapper lw = new ListWrapper();
            lw.TryAdd(ItemFmt.A, s, out err);
            objectAttribute = new ObjectAttribute(242, ObjectAttributeKey.ControlJob.CARRIERINPUTSPEC, lw);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(244, ObjectAttributeKey.ControlJob.DATACOLLECTIONPLAN, dcp);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            lw = new ListWrapper();
            ListWrapper lw_1 = new ListWrapper();
            ListWrapper lw_0 = new ListWrapper();
            foreach (string pj in PRJob)
            {
                lw_1 = new ListWrapper();
                lw_1.TryAdd(ItemFmt.A, pj, out err);
                lw_1.TryAdd(ItemFmt.L, lw_0, out err);
                lw_1.TryAdd(ItemFmt.L, lw_0, out err);
                lw.TryAdd(ItemFmt.L, lw_1, out err);
            }
            objectAttribute = new ObjectAttribute(245, ObjectAttributeKey.ControlJob.PROCESSINGCTRLSPEC, lw);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(249, ObjectAttributeKey.ControlJob.PROCESSORDERMGMT, PROrder);  //1:ARRIVAL 2:OPTIMIZE 3:LIST
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(250, ObjectAttributeKey.ControlJob.STARTMETHOD, bStart);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(251, ObjectAttributeKey.ControlJob.STATE, (byte)ControlJobState.QUEUED);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            lw = new ListWrapper();
            ObjectInstance updateObjectEntity2 = null;
            byte PJState = (byte)0;
            foreach (string pj in PRJob)
            {
                lw_1 = new ListWrapper();
                lw_1.TryAdd(ItemFmt.A, pj, out err);
                updateObjectEntity2 = null;
                _gemControler.GetObject(ObjectTypeKey.PROCESSJOB, pj, out updateObjectEntity2, out err);
                foreach (ObjectAttribute o in updateObjectEntity2.ListObjectAttributes)
                {
                    if (o.ATTRID == ObjectAttributeKey.ProcessJob.PRJOBSTATE)
                    {
                        PJState = Convert.ToByte(o.ATTRDATA.ToString());
                    }
                }
                lw_1.TryAdd(ItemFmt.U1, PJState, out err);
                lw.TryAdd(ItemFmt.L, lw_1, out err);
            }
            objectAttribute = new ObjectAttribute(252, "PRJOBSTATUSLIST", lw);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            result2 = _gemControler.UpdateObject(updateObjectEntity, out err);
            if(result2 == 0)
            {
                _gemControler.EventReportSend(91, out err); //CJ_NoStateToQueued
            }
            CJ_list.Add(objID);
            if (CJ_list.Count == 1)
            {
                ChangeControlJobState(objID, ControlJobState.SELECTED, 0);  //只有一個人就直接選了
            }
            //ObjectInstance updateObjectEntity2 = null;
            //result = gtlhomeSECS._gemControler.GetObject(ObjectTypeKey.PROCESSJOB, objID, out updateObjectEntity2, out err);
            return result;
        }

        public int SetControlJobAttr(string objID, string carrierInput, string dataPlan, string pauseEvent, string[] pj, byte processOrder, bool bStart, byte recMethod, string recID, string recVarList, out string err, string objSpec = "")
        {
            int result = 0;
            ObjectInstance updateObjectEntity = new ObjectInstance();
            updateObjectEntity.ObjType = ObjectTypeKey.CONTROLJOB;
            updateObjectEntity.ObjID = objID;
            updateObjectEntity.ObjSpec = objSpec;
            ListWrapper lw, lw_1, lw_2;
            ObjectAttribute objectAttribute = new ObjectAttribute(241, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            if (!(carrierInput == null))
            {
                string[] s = carrierInput.Split(',');
                lw = new ListWrapper();
                lw.TryAdd(ItemFmt.A, s, out err);
                objectAttribute = new ObjectAttribute(242, ObjectAttributeKey.ControlJob.CARRIERINPUTSPEC, lw);
                updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            }
            if (dataPlan != null)
            {
                objectAttribute = new ObjectAttribute(244, ObjectAttributeKey.ControlJob.DATACOLLECTIONPLAN, dataPlan);
                updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            }
            if (!(pauseEvent == string.Empty || pauseEvent == null))
            {
                string[] s = pauseEvent.Split(',');
                uint[] os = new uint[s.Length];
                for (int i = 0; i < s.Length; i++)
                    os[i] = Convert.ToUInt32(s[i]);
                lw = new ListWrapper();
                lw.TryAdd(ItemFmt.U4, os, out err);
                objectAttribute = new ObjectAttribute(247, ObjectAttributeKey.ControlJob.PAUSEEVENT, lw);
                updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            }
            if (!(pj == null))
            {
                lw = new ListWrapper();
                lw_2 = new ListWrapper();
                foreach (string s in pj)
                {
                    lw_1 = new ListWrapper();

                    lw_1.TryAdd(ItemFmt.A, s, out err);
                    lw_1.TryAdd(ItemFmt.L, lw_2, out err);
                    lw_1.TryAdd(ItemFmt.L, lw_2, out err);
                    lw.TryAdd(ItemFmt.L, lw_1, out err);
                }
                objectAttribute = new ObjectAttribute(248, ObjectAttributeKey.ControlJob.PROCESSINGCTRLSPEC, lw);
                updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            }
            if((processOrder != null) && processOrder >= 1 && processOrder <= 3)
            {
                objectAttribute = new ObjectAttribute(249, ObjectAttributeKey.ControlJob.PROCESSORDERMGMT, processOrder);
                updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            }
            if (recMethod != null)
            {
                objectAttribute = new ObjectAttribute(250, ObjectAttributeKey.ControlJob.STARTMETHOD, bStart);  
                updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            }
            result = _gemControler.UpdateObject(updateObjectEntity, out err);

            //ObjectInstance updateObjectEntity2 = null;
            //result = gtlhomeSECS._gemControler.GetObject(ObjectTypeKey.PROCESSJOB, objID, out updateObjectEntity2, out err);
            return result;
        }

        public int SetControlJobCurrentPrJob(string objID, string pj)
        {
            int result = 0;
            ObjectInstance updateObjectEntity = new ObjectInstance();
            updateObjectEntity.ObjType = ObjectTypeKey.CONTROLJOB;
            updateObjectEntity.ObjID = objID;
            ListWrapper lw = new ListWrapper();
            if (pj != null && pj != String.Empty)
                lw.TryAdd(ItemFmt.A, pj, out err);
            ObjectAttribute objectAttribute = new ObjectAttribute(241, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(243, ObjectAttributeKey.ControlJob.CURRENTPRJOB, lw);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            result = _gemControler.UpdateObject(updateObjectEntity, out err);
            return result;
        }
        public int GetControlJobAttr(string objID, out string carrierInputSpec, out string curPJ, out string dataCollection, out string mtrloutStatus, out string mtrloutSpec, out string pauseEvent, out string procCtrlSpec, out byte procOrder, out bool bStart, out byte state, out string err)
        {
            int result = 0;
            ObjectInstance updateObjectEntity = null;
            result = _gemControler.GetObject(ObjectTypeKey.CONTROLJOB, objID, out updateObjectEntity, out err);

            ListWrapper lw, lw_1;
            SecsIIValue val;
            carrierInputSpec = string.Empty;
            curPJ = string.Empty;
            dataCollection = string.Empty;
            mtrloutStatus = string.Empty;
            mtrloutSpec = string.Empty;
            pauseEvent = string.Empty;
            procCtrlSpec = string.Empty;
            procOrder = 0;
            bStart = false;
            state = (byte)ControlJobState.QUEUED;

            if (result != 0) return result;
            foreach (ObjectAttribute o in updateObjectEntity.ListObjectAttributes)
            {
                switch (o.ATTRID)
                {
                    case ObjectAttributeKey.ControlJob.CARRIERINPUTSPEC:
                        lw = (ListWrapper)o.ATTRDATA;
                        for (int i = 0; i < lw.Items.Count; i++)
                        {
                            string[] b = (string[])lw.Items[i].Value;
                            for (int j = 0; j < b.Length; ++j)
                            {
                                if (i == 0 && j == 0)
                                {
                                    carrierInputSpec += b[j];
                                }
                                else
                                {
                                    carrierInputSpec += ',' + b[j];
                                }
                            }
                        };
                        break;
                    case ObjectAttributeKey.ControlJob.CURRENTPRJOB:
                        lw = (ListWrapper)o.ATTRDATA;
                        curPJ = lw.Items[0].ToString();
                        break;
                    case ObjectAttributeKey.ControlJob.DATACOLLECTIONPLAN:
                        lw = (ListWrapper)o.ATTRDATA;
                        for (int i = 0; i < lw.Items.Count; i++)
                        {
                            string[] b = (string[])lw.Items[i].Value;
                            for (int j = 0; j < b.Length; ++j)
                            {
                                if (i == 0 && j == 0)
                                {
                                    dataCollection += b[j];
                                }
                                else
                                {
                                    dataCollection += ',' + b[j];
                                }
                            }
                        };
                        break;
                    case ObjectAttributeKey.ControlJob.MTRLOUTBYSTATUS:
                        break;
                    case ObjectAttributeKey.ControlJob.MTRLOUTSPEC:
                        break;
                    case ObjectAttributeKey.ControlJob.PAUSEEVENT:
                        lw = (ListWrapper)o.ATTRDATA;
                        for (int i = 0; i < lw.Items.Count; i++)
                        {
                            string[] b = (string[])lw.Items[i].Value;
                            for (int j = 0; j < b.Length; ++j)
                            {
                                if (i == 0 && j == 0)
                                {
                                    pauseEvent += b[j];
                                }
                                else
                                {
                                    pauseEvent += ',' + b[j];
                                }
                            }
                        };
                        break;
                    case ObjectAttributeKey.ControlJob.PROCESSINGCTRLSPEC:
                        lw = (ListWrapper)o.ATTRDATA;
                        for (int i = 0; i < lw.Items.Count; i++)
                        {
                            lw_1 = (ListWrapper)lw.Items[i].Value;
                            procCtrlSpec += lw_1.Items[0].ToString() + ';';
                        };
                        break;
                    case ObjectAttributeKey.ControlJob.PROCESSORDERMGMT:
                        procOrder = Convert.ToByte(o.ATTRDATA.ToString());
                        break;
                    case ObjectAttributeKey.ControlJob.STARTMETHOD:
                        bStart = Convert.ToBoolean(o.ATTRDATA.ToString());
                        break;
                    case ObjectAttributeKey.ControlJob.STATE:
                        state = Convert.ToByte(o.ATTRDATA);
                        break;

                    default:
                        break;
                }
            }

            return result;
        }

        public int DeleteControlJob(string objID, out string errstr)
        {
            string err = "";
            int result;
            /*
            ObjectInstance updateObjectEntity = new ObjectInstance();
            updateObjectEntity.ObjType = ObjectTypeKey.CONTROLJOB;
            updateObjectEntity.ObjID = objID;
            updateObjectEntity.ObjSpec = string.Empty;

            ObjectAttribute objectAttribute = new ObjectAttribute(241, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(143, ObjectAttributeKey.ControlJob.STATE, (byte)ControlJobState.COMPLETED);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);

            result = _gemControler.UpdateObject(updateObjectEntity, out err);*/
            result = ChangeControlJobState(objID, ControlJobState.COMPLETED, 0);

            errstr = err;
            return result;
        }

        public int ChangeControlJobState(string objID, ControlJobState state, int unnormal)    //unnormal 0:normal 1:stop 2:abort
        {
            int result = 0;
            ObjectInstance updateObjectEntity = null;
            byte oldstate = (byte)ControlJobState.QUEUED;

            result = _gemControler.GetObject(ObjectTypeKey.CONTROLJOB, objID, out updateObjectEntity, out err);
            foreach(ObjectAttribute o in updateObjectEntity.ListObjectAttributes)
            {
                if(o.ATTRID == ObjectAttributeKey.ControlJob.STATE)
                    oldstate = Convert.ToByte(o.ATTRDATA);
            }
            ObjectInstance updateObjectEntity2 = new ObjectInstance();
            updateObjectEntity2.ObjType = updateObjectEntity.ObjType;
            updateObjectEntity2.ObjID = updateObjectEntity.ObjID;
            updateObjectEntity2.ObjSpec = updateObjectEntity.ObjSpec;
            ObjectAttribute objectAttribute2 = new ObjectAttribute(251, ObjectAttributeKey.ControlJob.STATE, state);
            updateObjectEntity2.ListObjectAttributes.Add(objectAttribute2);
            result = _gemControler.UpdateObject(updateObjectEntity, out err);

            if(result == 0)
            {
                /*
                QUEUED = 0,
                SELECTED = 1,
                WAITING_FOR_START = 2,
                EXECUTING = 3,
                PAUSED = 4,
                COMPLETED = 5  
                */
                switch(state)
                {
                    case ControlJobState.QUEUED:
                        if(oldstate == 1)
                            _gemControler.EventReportSend(94, out err);
                        else
                            _gemControler.EventReportSend(91, out err);
                        break;
                    case ControlJobState.SELECTED:
                        if (oldstate == 0)
                            _gemControler.EventReportSend(93, out err);
                        break;
                    case ControlJobState.WAITING_FOR_START:
                        if (oldstate == 1)
                            _gemControler.EventReportSend(96, out err);
                        break;
                    case ControlJobState.EXECUTING:
                        if (oldstate == 1)
                            _gemControler.EventReportSend(95, out err);
                        else if (oldstate == 2)
                            _gemControler.EventReportSend(97, out err);
                        else if (oldstate == 4)
                            _gemControler.EventReportSend(99, out err);
                        break;
                    case ControlJobState.PAUSED:
                        if (oldstate == 3)
                            _gemControler.EventReportSend(98, out err);
                        break;
                    case ControlJobState.COMPLETED:
                        if (oldstate == 0 && unnormal == 0)
                        {
                            result = _gemControler.EventReportSend(92, out err);  //CJ_QueuedToComplete  刪除CJ
                            if (result == 0)
                            {
                                if (result == 0)  //Offline就刪不掉，list也就不刪了
                                    CJ_list.Remove(objID);
                                if (CJ_list.Count >= 1)
                                {
                                    ChangeControlJobState(CJ_list[0], ControlJobState.SELECTED, 0);  //Select CJ
                                }
                            }
                            break;
                        }
                        else if (oldstate == 3 && unnormal == 0)
                            _gemControler.EventReportSend(100, out err);
                        else if (unnormal == 1)
                            _gemControler.EventReportSend(101, out err);
                        else if (unnormal == 2)
                            _gemControler.EventReportSend(102, out err);
                        result = _gemControler.EventReportSend(103, out err);   //CJ_CompletedToComplete  刪除CJ
                        if (result == 0)
                        {
                            if (result == 0)  //Offline就刪不掉，list也就不刪了
                                CJ_list.Remove(objID);
                            if (CJ_list.Count >= 1)
                            {
                                ChangeControlJobState(CJ_list[0], ControlJobState.SELECTED, 0);  //Select CJ
                            }
                        }
                        break;
                }
            }
            return result;
        }

        public int CreateCarrier(string objID, string objSpec = "")
        {
            int result = 0;
            int result2 = 0;
            //CreateObject
            result = _gemControler.CreateObject(ObjectTypeKey.CARRIER, objID, out err, objSpec);

            if (result != 0) return result;

            _gemControler.EventReportSend(181, out err); //CMS_NoStateToCarrier

            ObjectInstance updateObjectEntity = new ObjectInstance();
            updateObjectEntity.ObjType = ObjectTypeKey.CARRIER;
            updateObjectEntity.ObjID = objID;
            updateObjectEntity.ObjSpec = objSpec;

            ObjectAttribute objectAttribute = new ObjectAttribute(441, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(442, "CAPACITY", (byte)25);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(443, "CARRIERIDSTATUS", (byte)CarrierIDState.ID_NOT_READ);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(444, "CARRIERACCESSINGSTATUS", (byte)CarrierAccessingState.NOT_ACCESSED);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(449, "SLOTMAPSTATUS", (byte)SlotMapState.SLOT_MAP_NOT_READ);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            result2 = _gemControler.UpdateObject(updateObjectEntity, out err);
            Carrier_list.Add(objID);
            _gemControler.EventReportSend(182, out err); //CMS_NoStateToIDNotRead
            _gemControler.EventReportSend(192, out err); //CMS_NoStateToSlotMapNotRead
            _gemControler.EventReportSend(197, out err); //CMS_NoStateToNotAccessed
            return result;
        }

        public int SetCarrierAttr_Location(string objID, string location, byte capacity = 25)
        {
            int result = 0;
            ObjectInstance updateObjectEntity = new ObjectInstance();
            updateObjectEntity.ObjType = ObjectTypeKey.CARRIER;
            updateObjectEntity.ObjID = objID;

            ObjectAttribute objectAttribute = new ObjectAttribute(441, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(442, "CAPACITY", capacity);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(448, "LOCATIONID", location);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);

            result = _gemControler.UpdateObject(updateObjectEntity, out err);
            return result;
        }

        public int SetCarrierStatus_ID(string objID, CarrierIDState status)
        {
            int result = 0;
            ObjectInstance updateObjectEntity = null;
            CarrierIDState oldstate = CarrierIDState.ID_NOT_READ;
            byte portid = 0;
            result = _gemControler.GetObject(ObjectTypeKey.CONTROLJOB, objID, out updateObjectEntity, out err);
            foreach (ObjectAttribute o in updateObjectEntity.ListObjectAttributes)
            {
                if (o.ATTRID == "CARRIERIDSTATUS")
                    oldstate = (CarrierIDState)Convert.ToByte(o.ATTRDATA);
                if (o.ATTRID == "LOCATIONID")
                {
                    if (o.ATTRDATA.ToString() == "LoadPort1")
                        portid = 1;
                    else if (o.ATTRDATA.ToString() == "LoadPort2")
                        portid = 2;
                }
            }
            if (portid != 0)
                UpdateSV(2001, portid, out err);

            ObjectInstance updateObjectEntity2 = new ObjectInstance();
            updateObjectEntity2.ObjType = ObjectTypeKey.CARRIER;
            updateObjectEntity2.ObjID = objID;

            ObjectAttribute objectAttribute = new ObjectAttribute(441, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity2.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(443, "CARRIERIDSTATUS", (byte)status);
            updateObjectEntity2.ListObjectAttributes.Add(objectAttribute);

            result = _gemControler.UpdateObject(updateObjectEntity2, out err);
            if(result == 0)
            {
                switch(status)
                {
                    case CarrierIDState.ID_NOT_READ:
                        _gemControler.EventReportSend(182, out err); 
                        break;
                    case CarrierIDState.WAITING_FOR_HOST:
                        if(oldstate == CarrierIDState.ID_NOT_READ)
                            _gemControler.EventReportSend(187, out err);
                        else
                            _gemControler.EventReportSend(183, out err);
                        break;
                    case CarrierIDState.ID_VERIFICATION_OK:
                        if (oldstate == CarrierIDState.ID_NOT_READ)
                            _gemControler.EventReportSend(186, out err);
                        else if (oldstate == CarrierIDState.WAITING_FOR_HOST)
                            _gemControler.EventReportSend(188, out err);
                        else
                            _gemControler.EventReportSend(184, out err);
                        break;
                    case CarrierIDState.ID_VERIFICATION_FAILED:
                        if (oldstate == CarrierIDState.WAITING_FOR_HOST)
                            _gemControler.EventReportSend(189, out err);
                        else
                            _gemControler.EventReportSend(185, out err);
                        break;
                }
            }
            return result;
        }

        public int SetCarrierStatus_Accessing(string objID, CarrierAccessingState status)
        {
            int result = 0;
            ObjectInstance updateObjectEntity = null;
            CarrierAccessingState oldstate = CarrierAccessingState.NOT_ACCESSED;

            result = _gemControler.GetObject(ObjectTypeKey.CONTROLJOB, objID, out updateObjectEntity, out err);
            foreach (ObjectAttribute o in updateObjectEntity.ListObjectAttributes)
            {
                if (o.ATTRID == "CARRIERACCESSINGSTATUS")
                    oldstate = (CarrierAccessingState)Convert.ToByte(o.ATTRDATA);
            }
            ObjectInstance updateObjectEntity2 = new ObjectInstance();
            updateObjectEntity2.ObjType = ObjectTypeKey.CARRIER;
            updateObjectEntity2.ObjID = objID;

            ObjectAttribute objectAttribute = new ObjectAttribute(441, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity2.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(444, "CARRIERACCESSINGSTATUS", (byte)status);
            updateObjectEntity2.ListObjectAttributes.Add(objectAttribute);

            result = _gemControler.UpdateObject(updateObjectEntity2, out err);
            if (result == 0)
            {
                switch (status)
                {
                    case CarrierAccessingState.NOT_ACCESSED:
                        _gemControler.EventReportSend(197, out err);
                        break;
                    case CarrierAccessingState.IN_ACCESS:
                        _gemControler.EventReportSend(198, out err);
                        break;
                    case CarrierAccessingState.CARRIER_COMPLETE:
                        _gemControler.EventReportSend(199, out err);
                        break;
                    case CarrierAccessingState.CARRIER_STOPPED:
                        _gemControler.EventReportSend(200, out err);
                        break;
                }
            }
            return result;
        }

        public int SetCarrierStatus_SlotMap(string objID, SlotMapState status)
        {
            int result = 0;
            ObjectInstance updateObjectEntity = null;
            SlotMapState oldstate = SlotMapState.SLOT_MAP_NOT_READ;
            byte portid = 0;
            byte acc = 0;
            string locationid = "";
            result = _gemControler.GetObject(ObjectTypeKey.CONTROLJOB, objID, out updateObjectEntity, out err);
            foreach (ObjectAttribute o in updateObjectEntity.ListObjectAttributes)
            {
                if (o.ATTRID == "SLOTMAPSTATUS")
                    oldstate = (SlotMapState)Convert.ToByte(o.ATTRDATA);
                if (o.ATTRID == "LOCATIONID")
                {
                    locationid = o.ATTRDATA.ToString();
                    if (locationid == "LoadPort1")
                        portid = 1;
                    else if (locationid == "LoadPort2")
                        portid = 2;
                }
                if (o.ATTRID == "CARRIERACCESSINGSTATUS")
                    acc = Convert.ToByte(o.ATTRDATA);
            }
            if (portid != 0)
                UpdateSV(2001, portid, out err);
            ObjectInstance updateObjectEntity2 = new ObjectInstance();
            updateObjectEntity2.ObjType = ObjectTypeKey.CARRIER;
            updateObjectEntity2.ObjID = objID;

            ObjectAttribute objectAttribute = new ObjectAttribute(441, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity2.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(444, "CARRIERACCESSINGSTATUS", (byte)status);
            updateObjectEntity2.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(448, "LOCATIONID", locationid);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(449, "SLOTMAPSTATUS", (byte)status);
            updateObjectEntity2.ListObjectAttributes.Add(objectAttribute);

            result = _gemControler.UpdateObject(updateObjectEntity2, out err);
            if (result == 0)
            {
                switch (status)
                {
                    case SlotMapState.SLOT_MAP_NOT_READ:
                        _gemControler.EventReportSend(192, out err);
                        break;
                    case SlotMapState.WAITING_FOR_HOST:
                        _gemControler.EventReportSend(194, out err);
                        break;
                    case SlotMapState.SLOT_MAP_VERIFICATION_OK:
                        if(oldstate == SlotMapState.WAITING_FOR_HOST)
                            _gemControler.EventReportSend(195, out err);
                        else
                            _gemControler.EventReportSend(193, out err);
                        break;
                    case SlotMapState.SLOT_MAP_VERIFICATION_FAILED:
                        _gemControler.EventReportSend(196, out err);
                        break;
                }
            }
            return result;
        }

        public int SetCarrierAttr_SlotMap(string objID, int[] slotmap)
        {
            int result = 0;
            byte[] slot = new byte[25];
            byte count = 0;
            if (slotmap.Length != 25)
            {
                return 1;
            }
            else
            {
                for(int i = 0; i<25; i++)
                {
                    if (slotmap[i] == 0)  //  empty
                        slot[i] = 1;
                    else if(slotmap[i] == 1)  // ok
                    {
                        slot[i] = 3;
                        count++;
                    }
                    else if (slotmap[i] == 2)  //cross
                        slot[i] = 5;
                    else
                        slot[i] = 2;   //error
                }
            }
            ObjectInstance updateObjectEntity = new ObjectInstance();
            updateObjectEntity.ObjType = ObjectTypeKey.CARRIER;
            updateObjectEntity.ObjID = objID;

            ObjectAttribute objectAttribute = new ObjectAttribute(441, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(445, "SUBSTRATECOUNT", count);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            ListWrapper lw = new ListWrapper();
            lw.TryAdd(ItemFmt.U1, slot, out err);
            objectAttribute = new ObjectAttribute(447, "SLOTMAP", count);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);

            result = _gemControler.UpdateObject(updateObjectEntity, out err);
            return result;
        }

        public int SetCarrierAttr_ContentMap(string objID, string[] lotID, string[] substrateID)
        {
            int result = 0;
            ObjectInstance updateObjectEntity = new ObjectInstance();
            updateObjectEntity.ObjType = ObjectTypeKey.CARRIER;
            updateObjectEntity.ObjID = objID;
            ObjectAttribute objectAttribute = new ObjectAttribute(441, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            ListWrapper lw = new ListWrapper();
            ListWrapper lw_1;
            if (lotID.Length != 25 || substrateID.Length != 25)
            {
                return 1;
            }
            else
            {
                for(int i = 0; i< 25; i++)
                {
                    lw_1 = new ListWrapper();
                    lw_1.TryAdd(ItemFmt.A, lotID[i], out err);
                    lw_1.TryAdd(ItemFmt.A, substrateID[i], out err);
                    lw.TryAdd(ItemFmt.L, lw_1, out err);
                }
            }
            
            objectAttribute = new ObjectAttribute(446, "CONTENTMAP", lw);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);

            result = _gemControler.UpdateObject(updateObjectEntity, out err);
            return result;
        }

        public int SetCarrierStatus_Usage(string objID, string usage)
        {
            int result = 0;
            ObjectInstance updateObjectEntity = new ObjectInstance();
            updateObjectEntity.ObjType = ObjectTypeKey.CARRIER;
            updateObjectEntity.ObjID = objID;

            ObjectAttribute objectAttribute = new ObjectAttribute(441, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(450, "USAGE", usage);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);

            result = _gemControler.UpdateObject(updateObjectEntity, out err);
            return result;
        }

        public int GetCarrierAttr(string objID, out byte cap, out byte count, out string[] lotid, out string[] substrateid, out byte[] slotmap, out string loc, out string usage, out string err)
        {
            int result = 0;
            ObjectInstance updateObjectEntity = null;
            result = _gemControler.GetObject(ObjectTypeKey.CARRIER, objID, out updateObjectEntity, out err);

            ListWrapper lw, lw_1;
            SecsIIValue val;
            cap = 25;
            count = 0;
            lotid = new string[25];
            substrateid = new string[25];
            slotmap = new byte[25];
            loc = string.Empty;
            usage = string.Empty;
            if (result != 0) return result;
            foreach (ObjectAttribute o in updateObjectEntity.ListObjectAttributes)
            {
                switch (o.ATTRID)
                {
                    case "CAPACITY":
                        cap = Convert.ToByte(o.ATTRDATA.ToString());
                        break;
                    case "SUBSTRATECOUNT":
                        count = Convert.ToByte(o.ATTRDATA.ToString());
                        break;
                    case "CONTENTMAP":
                        lw = (ListWrapper)o.ATTRDATA;
                        for (int i = 0; i < lw.Items.Count; i++)
                        {
                            lw_1 = (ListWrapper)lw.Items[i].Value;
                            lotid[i] = (lw_1.Items[0].Value).ToString();
                            substrateid[i] = (lw_1.Items[1].Value).ToString();
                        };
                        break;
                    case "SLOTMAP":
                        lw = (ListWrapper)o.ATTRDATA;
                        for (int i = 0; i < lw.Items.Count; i++)
                        {
                            slotmap[i] = Convert.ToByte(lw.Items[i].Value);
                        };
                        break;
                    case "LOCATIONID":
                        loc = o.ATTRDATA.ToString();
                        break;
                    case "USAGE":
                        usage = o.ATTRDATA.ToString();
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        public int GetCarrierStatus(string objID, out byte id, out byte accessing, out byte slotmap, out string err)
        {
            int result = 0;
            ObjectInstance updateObjectEntity = null;
            result = _gemControler.GetObject(ObjectTypeKey.CARRIER, objID, out updateObjectEntity, out err);

            id = 0;
            accessing = 0;
            slotmap = 0;

            foreach (ObjectAttribute o in updateObjectEntity.ListObjectAttributes)
            {
                switch (o.ATTRID)
                {
                    case "CARRIERIDSTATUS":
                        id = Convert.ToByte(o.ATTRDATA.ToString());
                        break;
                    case "CARRIERACCESSINGSTATUS":
                        accessing = Convert.ToByte(o.ATTRDATA.ToString());
                        break;
                    case "SLOTMAPSTATUS":
                        accessing = Convert.ToByte(o.ATTRDATA.ToString());
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        public int DeleteCarrier(string objID, out string errstr)
        {
            string err;
            int result;
            ObjectInstance updateObjectEntity = new ObjectInstance();
            updateObjectEntity.ObjType = ObjectTypeKey.CARRIER;
            updateObjectEntity.ObjID = objID;
            updateObjectEntity.ObjSpec = string.Empty;

            ObjectAttribute objectAttribute = new ObjectAttribute(441, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);

            result = _gemControler.UpdateObject(updateObjectEntity, out err);
            if (result == 0)
            {
                errstr = err;
                result = _gemControler.EventReportSend(201, out err); //CMS_CarrierToNoState
                if (result == 0)  //Offline就刪不掉，list也就不刪了
                    Carrier_list.Remove(objID);
            }
            else
            {
                errstr = err;
                return result;
            }
            errstr = err;
            return result;
        }

        public int CreateSubstrate(string objID, string lotID, string locationID, string objSpec = "")
        {
            int result = 0;
            int result2 = 0;
            //CreateObject
            result = _gemControler.CreateObject(ObjectTypeKey.SUBSTRATE, objID, out err, objSpec);

            if (result != 0) return result;

            ObjectInstance updateObjectEntity = new ObjectInstance();
            updateObjectEntity.ObjType = ObjectTypeKey.SUBSTRATE;
            updateObjectEntity.ObjID = objID;
            updateObjectEntity.ObjSpec = objSpec;

            ObjectAttribute objectAttribute = new ObjectAttribute(341, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(342, ObjectAttributeKey.Substrate.LOTID, lotID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            string clock;
            GetSV(3, out clock, out err);
            ListWrapper lw = new ListWrapper();
            ListWrapper lw_1 = new ListWrapper();
            lw_1.TryAdd(ItemFmt.A, locationID, out err);
            lw_1.TryAdd(ItemFmt.A, clock, out err);
            lw_1.TryAdd(ItemFmt.A, "", out err);
            lw.TryAdd(ItemFmt.L, lw_1, out err);
            objectAttribute = new ObjectAttribute(345, ObjectAttributeKey.Substrate.SUBSTHISTORY, lw);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(346, ObjectAttributeKey.Substrate.SUBSTLOCID, locationID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(347, ObjectAttributeKey.Substrate.SUBSTPROCSTATE, (byte)SubstProcState.NEEDS_PROCESSING);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(348, ObjectAttributeKey.Substrate.SUBSTSOURCE, locationID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(349, ObjectAttributeKey.Substrate.SUBSTSTATE, (byte)SubstrateTransState.AT_SOURCE);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(350, ObjectAttributeKey.Substrate.SUBSTTYPE, (byte)0); //0:wafer
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            //objectAttribute = new ObjectAttribute(351, ObjectAttributeKey.Substrate.SUBSTUSAGE, (byte)0); 
            //updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            result2 = _gemControler.UpdateObject(updateObjectEntity, out err);
            Substrate_list.Add(objID);
            _gemControler.EventReportSend(141, out err); //STS_Transport_NoStateToAtSource
            _gemControler.EventReportSend(150, out err); //STS_Processing_NoStateToNeedsProcessing
            return result;
        }

        public int SetSubstrateAttr_Location(string objID, string location)
        {
            int result = 0;
            ListWrapper lw, lw_1, lwout;
            lwout = new ListWrapper();
            ObjectInstance updateObjectEntity = null;
            result = _gemControler.GetObject(ObjectTypeKey.SUBSTRATE, objID, out updateObjectEntity, out err);
            string clock;
            GetSV(3, out clock, out err);
            foreach (ObjectAttribute o in updateObjectEntity.ListObjectAttributes)
            {
                if (o.ATTRID == ObjectAttributeKey.Substrate.SUBSTHISTORY)
                {
                    lw = (ListWrapper)o.ATTRDATA;
                    for(int i = 0; i< lw.Items.Count; i++)
                    {
                        lw_1 = (ListWrapper)lw.Items[i].Value;
                        if (i != lw.Items.Count-1)
                        {                          
                            lwout.TryAdd(ItemFmt.L, lw_1, out err);
                        }
                        else
                        {
                            string[] s1 = new string[3];
                            s1[0] = lw_1.Items[0].ToString();
                            s1[1] = lw_1.Items[1].ToString();
                            s1[2] = clock;
                            lw_1 = new ListWrapper();
                            lw_1.TryAdd(ItemFmt.A, s1, out err);
                            lwout.TryAdd(ItemFmt.L, lw_1, out err);
                        }
                    }
                    string[] s2 = new string[3];
                    s2[0] = location;
                    s2[1] = clock;
                    s2[2] = "";
                    lw_1 = new ListWrapper();
                    lw_1.TryAdd(ItemFmt.A, s2, out err);
                    lwout.TryAdd(ItemFmt.L, lw_1, out err);
                }
            }

            ObjectInstance updateObjectEntity2 = new ObjectInstance();
            updateObjectEntity2.ObjType = ObjectTypeKey.SUBSTRATE;
            updateObjectEntity2.ObjID = objID;

            ObjectAttribute objectAttribute = new ObjectAttribute(341, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity2.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(345, ObjectAttributeKey.Substrate.SUBSTHISTORY, lwout);
            updateObjectEntity2.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(346, ObjectAttributeKey.Substrate.SUBSTLOCID, location);
            updateObjectEntity2.ListObjectAttributes.Add(objectAttribute);

            result = _gemControler.UpdateObject(updateObjectEntity2, out err);
            return result;
        }

        public int SetSubstrateStatus_Proc(string objID, SubstProcState status)
        {
            int result = 0;
            ObjectInstance updateObjectEntity = null;
            SubstProcState oldstate = SubstProcState.NEEDS_PROCESSING;
            result = _gemControler.GetObject(ObjectTypeKey.SUBSTRATE, objID, out updateObjectEntity, out err);
            foreach (ObjectAttribute o in updateObjectEntity.ListObjectAttributes)
            {
                if (o.ATTRID == ObjectAttributeKey.Substrate.SUBSTPROCSTATE)
                    oldstate = (SubstProcState)Convert.ToByte(o.ATTRDATA);
            }

            ObjectInstance updateObjectEntity2 = new ObjectInstance();
            updateObjectEntity2.ObjType = ObjectTypeKey.SUBSTRATE;
            updateObjectEntity2.ObjID = objID;

            ObjectAttribute objectAttribute = new ObjectAttribute(341, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity2.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(347, ObjectAttributeKey.Substrate.SUBSTPROCSTATE, (byte)status);
            updateObjectEntity2.ListObjectAttributes.Add(objectAttribute);

            result = _gemControler.UpdateObject(updateObjectEntity2, out err);
            if (result == 0)
            {
                switch (status)
                {
                    case SubstProcState.NEEDS_PROCESSING:
                        _gemControler.EventReportSend(150, out err);
                        break;
                    case SubstProcState.IN_PROCESS:
                        _gemControler.EventReportSend(151, out err);
                        break;
                    case SubstProcState.PROCESSED:
                        _gemControler.EventReportSend(152, out err);
                        break;
                }
            }
            return result;
        }

        public int SetSubstrateStatus_Trans(string objID, SubstrateTransState status)
        {
            int result = 0;
            int result2 = 0;
            ObjectInstance updateObjectEntity = null;
            SubstrateTransState oldstate = SubstrateTransState.AT_SOURCE;
            result = _gemControler.GetObject(ObjectTypeKey.SUBSTRATE, objID, out updateObjectEntity, out err);
            foreach (ObjectAttribute o in updateObjectEntity.ListObjectAttributes)
            {
                if (o.ATTRID == ObjectAttributeKey.Substrate.SUBSTSTATE)
                    oldstate = (SubstrateTransState)Convert.ToByte(o.ATTRDATA);
            }

            ObjectInstance updateObjectEntity2 = new ObjectInstance();
            updateObjectEntity2.ObjType = ObjectTypeKey.SUBSTRATE;
            updateObjectEntity2.ObjID = objID;

            ObjectAttribute objectAttribute = new ObjectAttribute(341, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity2.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(349, ObjectAttributeKey.Substrate.SUBSTSTATE, (byte)status);
            updateObjectEntity2.ListObjectAttributes.Add(objectAttribute);

            result = _gemControler.UpdateObject(updateObjectEntity2, out err);
            if (result == 0)
            {
                switch (status)
                {
                    case SubstrateTransState.AT_SOURCE:
                        _gemControler.EventReportSend(141, out err);
                        break;
                    case SubstrateTransState.AT_WORK:
                        _gemControler.EventReportSend(142, out err);
                        break;
                    case SubstrateTransState.AT_DESTINATION:
                        _gemControler.EventReportSend(145, out err);
                        break;
                    case SubstrateTransState.EXTINXTION:
                        if(oldstate == SubstrateTransState.AT_DESTINATION)
                            result2 = _gemControler.EventReportSend(147, out err); //STS_Transport_AtDestinationToExtinction
                        else
                            result2 = _gemControler.EventReportSend(149, out err); //STS_AnySubstateSubStateToExtinction
                        if (result2 == 0)
                            Substrate_list.Remove(objID);
                        break;
                }
            }
            return result;
        }

        public int SetSubstrateAttr(string objID, string lotID, string sourceloc)
        {
            int result2 = 0;

            ObjectInstance updateObjectEntity = new ObjectInstance();
            updateObjectEntity.ObjType = ObjectTypeKey.SUBSTRATE;
            updateObjectEntity.ObjID = objID;

            ObjectAttribute objectAttribute = new ObjectAttribute(341, ObjectAttributeKey.OBJID, objID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(342, ObjectAttributeKey.Substrate.LOTID, lotID);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(348, ObjectAttributeKey.Substrate.SUBSTSOURCE, sourceloc);
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            objectAttribute = new ObjectAttribute(350, ObjectAttributeKey.Substrate.SUBSTTYPE, (byte)0); //0:wafer
            updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            //objectAttribute = new ObjectAttribute(351, ObjectAttributeKey.Substrate.SUBSTUSAGE, (byte)0); 
            //updateObjectEntity.ListObjectAttributes.Add(objectAttribute);
            result2 = _gemControler.UpdateObject(updateObjectEntity, out err);
            Substrate_list.Add(objID);
            _gemControler.EventReportSend(141, out err); //STS_Transport_NoStateToAtSource
            _gemControler.EventReportSend(150, out err); //STS_Processing_NoStateToNeedsProcessing
            return result2;
        }

        public int GetSubstrateAttr(string objID, out Substrate data, out string err)
        {
            int result = 0;
            ObjectInstance updateObjectEntity = null;
            result = _gemControler.GetObject(ObjectTypeKey.CARRIER, objID, out updateObjectEntity, out err);
            data = new Substrate();
            ListWrapper lw, lw_1;
            if (result != 0) return result;

            foreach (ObjectAttribute o in updateObjectEntity.ListObjectAttributes)
            {
                switch (o.ATTRID)
                {
                    case ObjectAttributeKey.OBJID:
                        data.objID = o.ATTRDATA.ToString();
                        break;
                    case ObjectAttributeKey.Substrate.LOTID:
                        data.lotID = o.ATTRDATA.ToString();
                        break;
                    case ObjectAttributeKey.Substrate.MATERIALSTATUS:
                        data.materialStatus = Convert.ToByte(o.ATTRDATA.ToString());
                        break;
                    case ObjectAttributeKey.Substrate.SUBSTDESTINATIO:
                        data.substDesrination = o.ATTRDATA.ToString();
                        break;
                    case ObjectAttributeKey.Substrate.SUBSTHISTORY:
                        lw = (ListWrapper)o.ATTRDATA;
                        List<string> history = new List<string>();
                        for (int i = 0; i < lw.Items.Count; i++)
                        {
                            lw_1 = (ListWrapper)lw.Items[i].Value;
                            history.Add((lw_1.Items[0].Value).ToString() + ',' + (lw_1.Items[1].Value).ToString() + ',' + (lw_1.Items[2].Value).ToString());
                        }
                        data.substHistory = history;
                        break;
                    case ObjectAttributeKey.Substrate.SUBSTLOCID:
                        data.substLocID = o.ATTRDATA.ToString();
                        break;
                    case ObjectAttributeKey.Substrate.SUBSTPROCSTATE:
                        data.substProcState = (SubstProcState)Convert.ToByte(o.ATTRDATA.ToString());
                        break;
                    case ObjectAttributeKey.Substrate.SUBSTSOURCE:
                        data.substSource = o.ATTRDATA.ToString();
                        break;
                    case ObjectAttributeKey.Substrate.SUBSTSTATE:
                        data.substState = (SubstrateTransState)Convert.ToByte(o.ATTRDATA.ToString());
                        break;
                    case ObjectAttributeKey.Substrate.SUBSTTYPE:
                        data.substType = Convert.ToByte(o.ATTRDATA.ToString());
                        break;
                    case ObjectAttributeKey.Substrate.SUBSTUSAGE:
                        data.substUsage = Convert.ToByte(o.ATTRDATA.ToString());
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        public int LoadportMatchToRun(string selectedCJ) //return 0 就是沒有match到
        {
            int rtn = 0;
            string carrierInputSpec;
            string curPJ;
            string dataCollection;
            string mtrloutStatus;
            string mtrloutSpec;
            string pauseEvent;
            string procCtrlSpec;
            byte procOrder;
            bool bStart;
            byte state;
            GetControlJobAttr(selectedCJ, out carrierInputSpec, out curPJ, out dataCollection, out mtrloutStatus, out mtrloutSpec, out pauseEvent, out procCtrlSpec, out procOrder, out bStart, out state, out string err);
            string[] cain = carrierInputSpec.Split(',');
            if ((ControlJobState)state != ControlJobState.SELECTED)
                return rtn;
            foreach(string s in cain)
            {
                foreach (string carrier in Carrier_list)
                {
                    if (s == carrier)  //Selected CJ有找到對應的Carrier有出現
                    {
                        byte id, accessing, slotmap;
                        GetCarrierStatus(carrier, out id, out accessing, out slotmap, out err);
                        if(id == (byte)CarrierIDState.ID_VERIFICATION_OK && slotmap == (byte)SlotMapState.SLOT_MAP_VERIFICATION_OK)  //ID跟Slotmap都通過驗證
                        {
                            if(accessing == (byte)CarrierAccessingState.NOT_ACCESSED)  //Carrier 還沒開run 所以要開run了
                            {
                                byte cap;
                                byte count;
                                string[] lotid;
                                string[] substrateid;
                                byte[] slot;
                                string loc;
                                string usage;
                                GetCarrierAttr(carrier, out cap, out count, out lotid, out substrateid, out slot, out loc, out usage, out err);
                                if (loc == "LoadPort1")
                                    rtn = 1;
                                else if (loc == "LoadPort2")
                                    rtn = 2;
                            }
                        }
                    }
                }
            }
            return rtn;
        }

        //Form Control----------------------------------------------------------------------------------------------------
        private void WriteLog(LogLevel level, string msg)
        {
            string log = string.Format("{0:yyyy/MM/dd HH:mm:ss:fff}, Level[{1}] => {2}", DateTime.Now, level, msg);
            this.Invoke((MethodInvoker)delegate ()
            {
                if (level == LogLevel.Error)
                    rtbEqpLog.SelectionColor = Color.Red;
                else if (level == LogLevel.Warn)
                    rtbEqpLog.SelectionColor = Color.Orange;
                else
                    rtbEqpLog.SelectionColor = Color.Black;

                rtbEqpLog.AppendText(log + Environment.NewLine);
            });
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            txtEqpCurrentAlarmSet.Text = string.Join("\r\n", listAlarmID);

            if (_gemControler != null && _gemControler.InitFinished == true)
            {
                //if(_gemControler.SECSDriverConnecting)
                //{
                //    lblSecsDriverStatus.Text = "Connection";
                //    lblSecsDriverStatus.BackColor = Color.GreenYellow;
                //}
                //else
                //{
                //    if(btnStart.Enabled == false)
                //    {
                //        lblSecsDriverStatus.Text = "Connecting";
                //        lblSecsDriverStatus.BackColor = Color.Red;
                //    }
                //    else
                //    {
                //        lblSecsDriverStatus.Text = "DisConnection";
                //        lblSecsDriverStatus.BackColor = Color.Red;
                //    }
                //}

                switch (_gemControler.SECSDriverStatus)
                {
                    case eSECSDriverConnectState.Disconnection:
                        lblSecsDriverStatus.Text = _gemControler.SECSDriverStatus.ToString();
                        lblSecsDriverStatus.BackColor = Color.FromArgb(255, 40, 0);
                        btnStart.Enabled = true;
                        btnStop.Enabled = false;
                        break;
                    case eSECSDriverConnectState.Listening:
                    case eSECSDriverConnectState.Connecting:
                        lblSecsDriverStatus.Text = _gemControler.SECSDriverStatus.ToString();
                        lblSecsDriverStatus.BackColor = Color.Yellow;
                        btnStart.Enabled = false;
                        btnStop.Enabled = true;
                        break;
                    case eSECSDriverConnectState.Connection:
                        lblSecsDriverStatus.Text = _gemControler.SECSDriverStatus.ToString();
                        lblSecsDriverStatus.BackColor = Color.FromArgb(0, 219, 0);
                        btnStart.Enabled = false;
                        btnStop.Enabled = true;
                        break;
                }

                switch (_gemControler.CommunicationState)
                {
                    case eCommunicationState.Disabled:
                        btnEnableComm.Enabled = true;
                        btnDisableComm.Enabled = false;
                        lblCommunicatingState.Text = "Disable";
                        lblCommunicatingState.BackColor = Color.White;
                        break;
                    case eCommunicationState.NotCommunicating:
                        btnEnableComm.Enabled = false;
                        btnDisableComm.Enabled = true;
                        lblCommunicatingState.Text = "NotCommunicating";
                        lblCommunicatingState.BackColor = Color.Red;
                        break;
                    case eCommunicationState.Communicating:
                        btnEnableComm.Enabled = false;
                        btnDisableComm.Enabled = true;
                        lblCommunicatingState.Text = "Communicating";
                        lblCommunicatingState.BackColor = Color.GreenYellow;
                        break;
                }

                #region SystemID - SV
                ItemFmt fmt = ItemFmt.L;
                object val;
                string err;

                //ControlState
                #region ControlState(6)
                if (_gemControler.GetSV(6, out fmt, out val, out err) == 0)
                {
                    if (val != null)
                    {
                        switch ((byte)val)
                        {
                            case 1:
                                lblSysSV_ControlState.Text = "1.EquipmentOffLine";
                                lblControlState.Text = string.Format("Offline");
                                lblControlState.BackColor = Color.Red;
                                btnOffLine.Enabled = false;
                                btnOnLineLocal.Enabled = true;
                                btnOnLineRemote.Enabled = true;
                                break;
                            case 2:
                                lblSysSV_ControlState.Text = "2.AttemptOnLine";
                                lblControlState.Text = string.Format("Offline");
                                lblControlState.BackColor = Color.Red;
                                btnOffLine.Enabled = false;
                                btnOnLineLocal.Enabled = false;
                                btnOnLineRemote.Enabled = false;
                                break;
                            case 3:
                                lblSysSV_ControlState.Text = "3.HostOffLine";
                                lblControlState.Text = string.Format("Offline");
                                lblControlState.BackColor = Color.Red;
                                btnOffLine.Enabled = false;
                                btnOnLineLocal.Enabled = true;
                                btnOnLineRemote.Enabled = true;
                                break;
                            case 4:
                                lblSysSV_ControlState.Text = "4.OnlineLocal";
                                lblControlState.Text = "Online-Local";
                                lblControlState.BackColor = Color.Yellow;
                                btnOffLine.Enabled = true;
                                btnOnLineLocal.Enabled = false;
                                btnOnLineRemote.Enabled = true;
                                break;
                            case 5:
                                lblSysSV_ControlState.Text = "5.OnlineRemote";
                                lblControlState.Text = "Online-Remote";
                                lblControlState.BackColor = Color.GreenYellow;
                                btnOffLine.Enabled = true;
                                btnOnLineLocal.Enabled = true;
                                btnOnLineRemote.Enabled = false;
                                break;
                            default:
                                lblSysSV_ControlState.Text = val + ".Unknown";
                                btnOffLine.Enabled = false;
                                btnOnLineLocal.Enabled = false;
                                btnOnLineRemote.Enabled = false;
                                break;
                        }
                    }
                    else
                    {
                        lblSysSV_ControlState.Text = string.Empty;
                    }
                }
                else
                {
                    lblSysSV_ControlState.Text = string.Empty;
                }
                #endregion

                #region SpoolState(16)
                if (_gemControler.GetSV(16, out fmt, out val, out err) == 0)
                {
                    if (val != null)
                    {
                        switch ((byte)val)
                        {
                            case 1:
                                lblSysSV_SpoolState.Text = "SpoolState(16): 1:Inactive";
                                break;
                            case 2:
                                lblSysSV_SpoolState.Text = "SpoolState(16): 2:Active";
                                break;
                            default:
                                lblSysSV_SpoolState.Text = val + "SpoolState(16): Unknown";
                                break;
                        }
                    }
                    else
                    {
                        lblSysSV_SpoolState.Text = "SpoolState(16):";
                    }
                }
                else
                {
                    lblSysSV_SpoolState.Text = string.Empty;
                }
                #endregion

                #region SpoolLoadSubState(17)
                if (_gemControler.GetSV(17, out fmt, out val, out err) == 0)
                {
                    lblSysSV_SpoolLoadSubState.Text = "SpoolLoadSubState(17):";
                    if (val != null)
                    {
                        switch ((byte)val)
                        {
                            case 0:
                                lblSysSV_SpoolLoadSubState.Text = "SpoolLoadSubState(17): 0:Not Full";
                                break;
                            case 1:
                                lblSysSV_SpoolLoadSubState.Text = "SpoolLoadSubState(17): 1:Full";
                                break;
                        }
                    }
                    else
                    {
                        lblSysSV_SpoolLoadSubState.Text = "SpoolLoadSubState(17):";
                    }
                }
                else
                {
                    lblSysSV_SpoolLoadSubState.Text = string.Empty;
                }
                #endregion

                #region SpoolUnloadSubState(18)
                if (_gemControler.GetSV(18, out fmt, out val, out err) == 0)
                {
                    if (val != null)
                    {
                        switch ((byte)val)
                        {
                            case 0:
                                lblSysSV_SpoolUnloadSubState.Text = "SpoolUnloadSubState(18): 0 No Spool Out";
                                break;
                            case 1:
                                lblSysSV_SpoolUnloadSubState.Text = "SpoolUnloadSubState(18): 1:transmit";
                                break;
                            case 2:
                                lblSysSV_SpoolUnloadSubState.Text = "SpoolUnloadSubState(18): 2:Purge";
                                break;
                        }
                    }
                    else
                    {
                        lblSysSV_SpoolUnloadSubState.Text = "SpoolUnloadSubState(18):";
                    }
                }
                else
                {
                    lblSysSV_SpoolUnloadSubState.Text = string.Empty;
                }
                #endregion

                #region SpoolCountActual(21)
                if (_gemControler.GetSV(21, out fmt, out val, out err) == 0)
                {
                    if (val != null)
                        lblSysSV_SpoolCountActual.Text = "SpoolCountActual(21): " + val;
                    else
                        lblSysSV_SpoolCountActual.Text = "SpoolCountActual(21):";
                }
                else
                {
                    lblSysSV_SpoolCountActual.Text = string.Empty;
                }
                #endregion

                #region SpoolCountTotal(22)
                if (_gemControler.GetSV(22, out fmt, out val, out err) == 0)
                {
                    if (val != null)
                        lblSysSV_SpoolCountTotal.Text = "SpoolCountTotal(22): " + val;
                    else
                        lblSysSV_SpoolCountTotal.Text = "SpoolCountTotal(22):";
                }
                else
                {
                    lblSysSV_SpoolCountTotal.Text = string.Empty;
                }
                #endregion

                #region SpoolStartTime(19)
                if (_gemControler.GetSV(19, out fmt, out val, out err) == 0)
                {
                    if (val != null)
                        lblSysSV_SpoolStartTime.Text = "SpoolStartTime(19): " + val;
                    else
                        lblSysSV_SpoolStartTime.Text = "SpoolStartTime(19):";
                }
                else
                {
                    lblSysSV_SpoolStartTime.Text = string.Empty;
                }
                #endregion

                #region SpoolFullTime(20)
                if (_gemControler.GetSV(20, out fmt, out val, out err) == 0)
                {
                    if (val != null)
                        lblSysSV_SpoolFullTime.Text = "SpoolFullTime(20): " + val;
                    else
                        lblSysSV_SpoolFullTime.Text = "SpoolFullTime(20):";
                }
                else
                {
                    lblSysSV_SpoolFullTime.Text = string.Empty;
                }
                #endregion

                #region Clock(3)
                if (_gemControler.GetSV(3, out fmt, out val, out err) == 0)
                {
                    if (val != null)
                        lblSysSV_Clock.Text = val.ToString();
                    else
                        lblSysSV_Clock.Text = "Clock(3)";
                }
                else
                {
                    lblSysSV_Clock.Text = string.Empty;
                }
                #endregion

                #region PJ_PRJobSpace(101)
                if (_gemControler.GetSV(101, out fmt, out val, out err) == 0)
                {
                    if (val != null)
                        lblSysSV_PRJobSpace.Text = "(E40-PM)PJ_PRJobSpace(101):" + val.ToString();
                    else
                        lblSysSV_PRJobSpace.Text = "(E40-PM)PJ_PRJobSpace(101):";
                }
                else
                {
                    lblSysSV_PRJobSpace.Text = string.Empty;
                }
                #endregion

                #endregion

                #region SystemID - EC

                #region ConfigSpool(77)
                if (_gemControler.GetEC(77, out fmt, out val, out err) == 0)
                {
                    switch ((bool)val)
                    {
                        case true:
                            lblSysEC_ConfigSpool.Text = "ConfigSpool(77): true";
                            break;
                        case false:
                            lblSysEC_ConfigSpool.Text = "ConfigSpool(77): false";
                            break;
                        default:
                            lblSysEC_ConfigSpool.Text = val + "ConfigSpool(77): Unknown";
                            break;
                    }
                }
                else
                {
                    lblSysEC_ConfigSpool.Text = string.Empty;
                }
                #endregion

                #region OverWriteSpool(79)
                if (_gemControler.GetEC(79, out fmt, out val, out err) == 0)
                {
                    switch ((bool)val)
                    {
                        case true:
                            lblSysEC_OverWriteSpool.Text = "OverWriteSpool(79): true";
                            break;
                        case false:
                            lblSysEC_OverWriteSpool.Text = "OverWriteSpool(79): false";
                            break;
                        default:
                            lblSysEC_OverWriteSpool.Text = val + "OverWriteSpool(79): Unknown";
                            break;
                    }
                }
                else
                {
                    lblSysEC_OverWriteSpool.Text = string.Empty;
                }
                #endregion

                #region MaxSpoolTransmit(78)
                if (_gemControler.GetEC(78, out fmt, out val, out err) == 0)
                {
                    lblSysEC_MaxSpoolTransmit.Text = "MaxSpoolTransmit(78): " + val;
                }
                else
                {
                    lblSysEC_MaxSpoolTransmit.Text = string.Empty;
                }
                #endregion

                #region PJ_PRMaxJobSpace(171)
                if (_gemControler.GetEC(171, out fmt, out val, out err) == 0)
                {
                    lblSysEC_PRMaxJobSpace.Text = "(E40-PM)PJ_PRMaxJobSpace(171): " + val;
                }
                else
                {
                    lblSysEC_PRMaxJobSpace.Text = string.Empty;
                }
                #endregion

                #endregion
            }
        }

        private void tsmiClearDriverLog_Click(object sender, EventArgs e)
        {
            rtbDriverLog.Clear();
        }

        private void tsmiClearGemLog_Click(object sender, EventArgs e)
        {
            rtbGemLog.Clear();
        }

        private void tsmiClearSECSLog_Click(object sender, EventArgs e)
        {
            rtbSecsLog.Clear();
        }

        private void tsmiClearEQPLog_Click(object sender, EventArgs e)
        {
            rtbEqpLog.Clear();
        }

        private void tsmiDriverParameter_Click(object sender, EventArgs e)
        {
            _frmDriverConfigInfo.ShowData(_gemControler.SECSDriverSetting);
        }

        private string CreateSMLMessage(byte[] secs2RawData)
        {
            int[] myStack = new int[30];    //預設單一SECS最大有30層
            int myStackPtr = 0;
            int secs2TotalLength = 0;       //SECS2 Data 總長度
            int secs2ItemOffset = 0;        //SECS2 item 位移指標
            int secs2ItemNum = 0;           //SECS2 item 長度
            object secs2ItemData = null;    //SECS2 item 值

            StringBuilder sbSml = new StringBuilder();

            #region Verify whether the input data is an array or not
            if (secs2RawData != null)
            {
                Array myArray = secs2RawData as Array;
                if (myArray != null)
                {
                    secs2TotalLength = myArray.Length;
                }
            }
            else
                return string.Empty;
            #endregion

            //if (secs2TotalLength == 0)
            //{
            //    sbSml.Append(".");
            //    return sbSml.ToString();
            //}

            try
            {
                StringBuilder sb = new StringBuilder();

                while (secs2ItemOffset < secs2TotalLength)
                {
                    if (myStackPtr > 0)
                    {
                        if (myStack[myStackPtr - 1] > 0)
                        {
                            myStack[myStackPtr - 1] = myStack[myStackPtr - 1] - 1;
                        }
                        else
                        {
                            if (myStackPtr > 1)
                            {
                                //while (myStackPtr > 0 && myStack[myStackPtr - 1] == 0)
                                while (myStack[myStackPtr - 1] == 0)
                                {
                                    myStackPtr = myStackPtr - 1;
                                    sbSml.Append(AppendIndent(myStackPtr));
                                    sbSml.Append(">\r\n");
                                }
                                myStack[myStackPtr - 1] = myStack[myStackPtr - 1] - 1;
                            }

                            //myStackPtr = myStackPtr - 1;
                            //if (myStackPtr == 0)
                            //{
                            //    // force show end. Normal message should not run to here
                            //    sbSml.Append(">" + "\r\n");
                            //    sbSml.Append("." + "\r\n");

                            //    return sbSml.ToString();
                            //}

                            //while (myStack[myStackPtr] == 0)
                            //{
                            //    sbSml.Append(AppendIndent(myStackPtr));
                            //    sbSml.Append(">" + "\r\n");
                            //    myStackPtr = myStackPtr - 1;
                            //}
                            //myStackPtr = myStackPtr + 1;
                            //myStack[myStackPtr - 1] = myStack[myStackPtr - 1] - 1;
                        }
                    }

                    ItemFmt lItemType = Delta.DIAAuto.DIASECSGEM.SECSLib.GetItemType(secs2RawData, secs2ItemOffset);
                    Delta.DIAAuto.DIASECSGEM.SECSLib.ItemIn(ref secs2RawData, ref secs2ItemOffset, ref lItemType, ref secs2ItemNum, ref secs2ItemData);
                    sb.Clear();

                    switch (lItemType)
                    {
                        case ItemFmt.L:
                            // Display the data item
                            sbSml.Append(AppendIndent(myStackPtr));
                            sbSml.Append("<L[" + secs2ItemNum + "]\r\n");
                            // Increase the indent level
                            myStack[myStackPtr] = secs2ItemNum;
                            myStackPtr = myStackPtr + 1;
                            break;
                        case ItemFmt.A:
                            sbSml.Append(AppendIndent(myStackPtr));
                            sbSml.Append("<" + lItemType + "[" + secs2ItemNum + "]");
                            if (secs2ItemData != null) sbSml.Append(" " + secs2ItemData.ToString());
                            sbSml.Append(">\r\n");
                            break;
                        case ItemFmt.B:
                            for (int i = 0; i < secs2ItemNum; ++i) { sb.Append(" " + Convert.ToString((secs2ItemData as byte[])[i], 16).ToUpper()); }
                            sbSml.Append(AppendIndent(myStackPtr));
                            sbSml.Append("<" + lItemType + "[" + secs2ItemNum + "]" + sb.ToString() + ">\r\n");
                            break;
                        case ItemFmt.Boolean:
                            for (int i = 0; i < secs2ItemNum; ++i) { sb.Append(" " + Convert.ToString((secs2ItemData as bool[])[i]).ToUpper()); }
                            sbSml.Append(AppendIndent(myStackPtr));
                            sbSml.Append("<" + lItemType + "[" + secs2ItemNum + "]" + sb.ToString() + ">\r\n");
                            break;
                        case ItemFmt.F4:
                            for (int i = 0; i < secs2ItemNum; ++i) { sb.Append(" " + Convert.ToString((secs2ItemData as float[])[i]).ToUpper()); }
                            sbSml.Append(AppendIndent(myStackPtr));
                            sbSml.Append("<" + lItemType + "[" + secs2ItemNum + "]" + sb.ToString() + ">\r\n");
                            break;
                        case ItemFmt.F8:
                            for (int i = 0; i < secs2ItemNum; ++i) { sb.Append(" " + Convert.ToString((secs2ItemData as double[])[i]).ToUpper()); }
                            sbSml.Append(AppendIndent(myStackPtr));
                            sbSml.Append("<" + lItemType + "[" + secs2ItemNum + "]" + sb.ToString() + ">\r\n");
                            break;
                        case ItemFmt.I1:
                            for (int i = 0; i < secs2ItemNum; ++i) { sb.Append(" " + Convert.ToString((secs2ItemData as sbyte[])[i]).ToUpper()); }
                            sbSml.Append(AppendIndent(myStackPtr));
                            sbSml.Append("<" + lItemType + "[" + secs2ItemNum + "]" + sb.ToString() + ">\r\n");
                            break;
                        case ItemFmt.I2:
                            for (int i = 0; i < secs2ItemNum; ++i) { sb.Append(" " + Convert.ToString((secs2ItemData as short[])[i]).ToUpper()); }
                            sbSml.Append(AppendIndent(myStackPtr));
                            sbSml.Append("<" + lItemType + "[" + secs2ItemNum + "]" + sb.ToString() + ">\r\n");
                            break;
                        case ItemFmt.I4:
                            for (int i = 0; i < secs2ItemNum; ++i) { sb.Append(" " + Convert.ToString((secs2ItemData as int[])[i]).ToUpper()); }
                            sbSml.Append(AppendIndent(myStackPtr));
                            sbSml.Append("<" + lItemType + "[" + secs2ItemNum + "]" + sb.ToString() + ">\r\n");
                            break;
                        case ItemFmt.I8:
                            for (int i = 0; i < secs2ItemNum; ++i) { sb.Append(" " + Convert.ToString((secs2ItemData as long[])[i]).ToUpper()); }
                            sbSml.Append(AppendIndent(myStackPtr));
                            sbSml.Append("<" + lItemType + "[" + secs2ItemNum + "]" + sb.ToString() + ">\r\n");
                            break;
                        case ItemFmt.U1:
                            for (int i = 0; i < secs2ItemNum; ++i) { sb.Append(" " + Convert.ToString((secs2ItemData as byte[])[i]).ToUpper()); }
                            sbSml.Append(AppendIndent(myStackPtr));
                            sbSml.Append("<" + lItemType + "[" + secs2ItemNum + "]" + sb.ToString() + ">\r\n");
                            break;
                        case ItemFmt.U2:
                            for (int i = 0; i < secs2ItemNum; ++i) { sb.Append(" " + Convert.ToString((secs2ItemData as ushort[])[i]).ToUpper()); }
                            sbSml.Append(AppendIndent(myStackPtr));
                            sbSml.Append("<" + lItemType + "[" + secs2ItemNum + "]" + sb.ToString() + ">\r\n");
                            break;
                        case ItemFmt.U4:
                            for (int i = 0; i < secs2ItemNum; ++i) { sb.Append(" " + Convert.ToString((secs2ItemData as uint[])[i]).ToUpper()); }
                            sbSml.Append(AppendIndent(myStackPtr));
                            sbSml.Append("<" + lItemType + "[" + secs2ItemNum + "]" + sb.ToString() + ">\r\n");
                            break;
                        case ItemFmt.U8:
                            for (int i = 0; i < secs2ItemNum; ++i) { sb.Append(" " + Convert.ToString((secs2ItemData as ulong[])[i]).ToUpper()); }
                            sbSml.Append(AppendIndent(myStackPtr));
                            sbSml.Append("<" + lItemType + "[" + secs2ItemNum + "]" + sb.ToString() + ">\r\n");
                            break;
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            while (myStackPtr > 0)
            {
                myStackPtr = myStackPtr - 1;
                sbSml.Append(AppendIndent(myStackPtr));
                sbSml.Append(">" + "\r\n");
            }

            sbSml.Append("." + "\r\n");
            return sbSml.ToString();
        }

        private string AppendIndent(int myIndentLevel)
        {
            string indentString = string.Empty;

            return indentString.PadLeft(2 * myIndentLevel, ' ');
        }

        private bool ParseForBool(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;

            if (s == "0")
                return false;
            if (s == "1")
                return true;

            bool b;
            bool.TryParse(s, out b);
            return b;
        }

        private ItemFmt ConvertString2Fmt(string value)
        {
            switch (value.ToUpper())
            {
                case "L":
                case "LIST":
                    return ItemFmt.L;
                case "BOOLEAN":
                    return ItemFmt.Boolean;
                case "B":
                    return ItemFmt.B;
                case "A":
                case "ASCII":
                    return ItemFmt.A;
                case "U1":
                    return ItemFmt.U1;
                case "U2":
                    return ItemFmt.U2;
                case "U4":
                    return ItemFmt.U4;
                case "U8":
                    return ItemFmt.U8;
                case "I1":
                    return ItemFmt.I1;
                case "I2":
                    return ItemFmt.I2;
                case "I4":
                    return ItemFmt.I4;
                case "I8":
                    return ItemFmt.I8;
                case "F4":
                    return ItemFmt.F4;
                case "F8":
                    return ItemFmt.F8;
                default:
                    throw new Exception(string.Format("'{0}' is unknow illegal format", value));
            }
        }

        private bool ConvertString2Object(ItemFmt fmt, string value, out object valueObj)
        {
            string log = string.Empty;
            bool ret = true;
            valueObj = null;

            try
            {
                switch (fmt)
                {
                    case ItemFmt.Boolean:
                        valueObj = value.Split(',').Select(x => ParseForBool(x)).ToArray();
                        break;
                    case ItemFmt.B:
                        valueObj = value.Split(',').Select(x => Convert.ToByte(x, 16)).ToArray();
                        break;
                    case ItemFmt.A:
                        valueObj = value;
                        break;
                    case ItemFmt.U1:
                        valueObj = value.Split(',').Select(x => byte.Parse(x)).ToArray();
                        break;
                    case ItemFmt.U2:
                        valueObj = value.Split(',').Select(x => ushort.Parse(x)).ToArray();
                        break;
                    case ItemFmt.U4:
                        valueObj = value.Split(',').Select(x => uint.Parse(x)).ToArray();
                        break;
                    case ItemFmt.U8:
                        valueObj = value.Split(',').Select(x => ulong.Parse(x)).ToArray();
                        break;
                    case ItemFmt.I1:
                        valueObj = value.Split(',').Select(x => sbyte.Parse(x)).ToArray();
                        break;
                    case ItemFmt.I2:
                        valueObj = value.Split(',').Select(x => short.Parse(x)).ToArray();
                        break;
                    case ItemFmt.I4:
                        valueObj = value.Split(',').Select(x => int.Parse(x)).ToArray();
                        break;
                    case ItemFmt.I8:
                        valueObj = value.Split(',').Select(x => long.Parse(x)).ToArray();
                        break;
                    case ItemFmt.F4:
                        valueObj = value.Split(',').Select(x => float.Parse(x)).ToArray();
                        break;
                    case ItemFmt.F8:
                        valueObj = value.Split(',').Select(x => double.Parse(x)).ToArray();
                        break;
                    default:
                        ret = false;
                        break;
                }

                if (ret)
                {
                    if (valueObj.GetType().IsArray && (valueObj as Array).Length == 1)
                        valueObj = (valueObj as Array).GetValue(0);
                }

            }
            catch (Exception ex)
            {
                ret = false;
                log = string.Format("Convert '{0}' to object Fail, Err=[{1}]", value, ex.ToString());
                MessageBox.Show(log);
            }

            return ret;
        }

        private Tuple<string, string> GetDataDesc(ItemFmt fmt, object value)
        {
            string fmtDesc = string.Empty;
            string valDesc = string.Empty;
            StringBuilder sb = new StringBuilder();

            switch (fmt)
            {
                case ItemFmt.L:
                    if (value == null)
                    {
                        fmtDesc = "L[0]";
                    }
                    else if (value is ListWrapper)
                    {
                        ListWrapper listWrapper = value as ListWrapper;
                        fmtDesc = string.Format("L[{0}]", listWrapper.Count);
                    }
                    break;
                case ItemFmt.B:
                    if (value == null)
                    {
                        fmtDesc = "B[0]";
                    }
                    else
                    {
                        if (value is byte)
                        {
                            byte b = (byte)value;
                            fmtDesc = "B[1]";
                            valDesc = Convert.ToString(b, 16).ToUpper();
                        }
                        else if (value is byte[])
                        {
                            byte[] b = (byte[])value;
                            for (int i = 0; i < b.Length; ++i) { sb.Append(" " + Convert.ToString((b as byte[])[i], 16).ToUpper()); }

                            fmtDesc = string.Format("B[{0}]", b.Length);
                            valDesc = sb.ToString();
                        }
                    }
                    break;
                case ItemFmt.Boolean:
                    if (value == null)
                    {
                        fmtDesc = "BOOLEAN[0]";
                    }
                    else
                    {
                        if (value is bool)
                        {
                            bool b = (bool)value;
                            fmtDesc = "BOOLEAN[1]";
                            valDesc = Convert.ToString(b);
                        }
                        else if (value is bool[])
                        {
                            bool[] b = (bool[])value;
                            for (int i = 0; i < b.Length; ++i) { sb.Append(" " + Convert.ToString((b as bool[])[i])); }

                            fmtDesc = string.Format("BOOLEAN[{0}]", b.Length);
                            valDesc = sb.ToString();
                        }
                    }
                    break;
                case ItemFmt.A:
                    if (value == null)
                    {
                        fmtDesc = "A[0]";
                    }
                    else
                    {
                        if (value is string)
                        {
                            string b = (string)value;
                            fmtDesc = string.Format("A[{0}]", b.Length);
                            valDesc = b;
                        }
                    }
                    break;
                case ItemFmt.U1:
                    if (value == null)
                    {
                        fmtDesc = "U1[0]";
                    }
                    else
                    {
                        if (value is byte)
                        {
                            byte b = (byte)value;
                            fmtDesc = "U1[1]";
                            valDesc = Convert.ToString(b);
                        }
                        else if (value is byte[])
                        {
                            byte[] b = (byte[])value;
                            for (int i = 0; i < b.Length; ++i) { sb.Append(" " + Convert.ToString((b as byte[])[i])); }

                            fmtDesc = string.Format("U1[{0}]", b.Length);
                            valDesc = sb.ToString();
                        }
                    }
                    break;
                case ItemFmt.U2:
                    if (value == null)
                    {
                        fmtDesc = "U2[0]";
                    }
                    else
                    {
                        if (value is ushort)
                        {
                            ushort b = (ushort)value;
                            fmtDesc = "U2[1]";
                            valDesc = Convert.ToString(b);
                        }
                        else if (value is ushort[])
                        {
                            ushort[] b = (ushort[])value;
                            for (int i = 0; i < b.Length; ++i) { sb.Append(" " + Convert.ToString((b as ushort[])[i])); }

                            fmtDesc = string.Format("U2[{0}]", b.Length);
                            valDesc = sb.ToString();
                        }
                    }
                    break;
                case ItemFmt.U4:
                    if (value == null)
                    {
                        fmtDesc = "U4[0]";
                    }
                    else
                    {
                        if (value is uint)
                        {
                            uint b = (uint)value;
                            fmtDesc = "U4[1]";
                            valDesc = Convert.ToString(b);
                        }
                        else if (value is uint[])
                        {
                            uint[] b = (uint[])value;
                            for (int i = 0; i < b.Length; ++i) { sb.Append(" " + Convert.ToString((b as uint[])[i])); }

                            fmtDesc = string.Format("U4[{0}]", b.Length);
                            valDesc = sb.ToString();
                        }
                    }
                    break;
                case ItemFmt.U8:
                    if (value == null)
                    {
                        fmtDesc = "U8[0]";
                    }
                    else
                    {
                        if (value is ulong)
                        {
                            ulong b = (ulong)value;
                            fmtDesc = "U8[1]";
                            valDesc = Convert.ToString(b);
                        }
                        else if (value is ulong[])
                        {
                            ulong[] b = (ulong[])value;
                            for (int i = 0; i < b.Length; ++i) { sb.Append(" " + Convert.ToString((b as ulong[])[i])); }

                            fmtDesc = string.Format("U8[{0}]", b.Length);
                            valDesc = sb.ToString();
                        }
                    }
                    break;
                case ItemFmt.I1:
                    if (value == null)
                    {
                        fmtDesc = "I1[0]";
                    }
                    else
                    {
                        if (value is sbyte)
                        {
                            sbyte b = (sbyte)value;
                            fmtDesc = "I1[1]";
                            valDesc = Convert.ToString(b);
                        }
                        else if (value is sbyte[])
                        {
                            sbyte[] b = (sbyte[])value;
                            for (int i = 0; i < b.Length; ++i) { sb.Append(" " + Convert.ToString((b as sbyte[])[i])); }

                            fmtDesc = string.Format("I1[{0}]", b.Length);
                            valDesc = sb.ToString();
                        }
                    }
                    break;
                case ItemFmt.I2:
                    if (value == null)
                    {
                        fmtDesc = "I2[0]";
                    }
                    else
                    {
                        if (value is short)
                        {
                            short b = (short)value;
                            fmtDesc = "I2[1]";
                            valDesc = Convert.ToString(b);
                        }
                        else if (value is short[])
                        {
                            short[] b = (short[])value;
                            for (int i = 0; i < b.Length; ++i) { sb.Append(" " + Convert.ToString((b as short[])[i])); }

                            fmtDesc = string.Format("I2[{0}]", b.Length);
                            valDesc = sb.ToString();
                        }
                    }
                    break;
                case ItemFmt.I4:
                    if (value == null)
                    {
                        fmtDesc = "I4[0]";
                    }
                    else
                    {
                        if (value is int)
                        {
                            int b = (int)value;
                            fmtDesc = "I4[1]";
                            valDesc = Convert.ToString(b);
                        }
                        else if (value is int[])
                        {
                            int[] b = (int[])value;
                            for (int i = 0; i < b.Length; ++i) { sb.Append(" " + Convert.ToString((b as int[])[i])); }

                            fmtDesc = string.Format("I4[{0}]", b.Length);
                            valDesc = sb.ToString();
                        }
                    }
                    break;
                case ItemFmt.I8:
                    if (value == null)
                    {
                        fmtDesc = "I8[0]";
                    }
                    else
                    {
                        if (value is long)
                        {
                            long b = (long)value;
                            fmtDesc = "I8[1]";
                            valDesc = Convert.ToString(b);
                        }
                        else if (value is long[])
                        {
                            long[] b = (long[])value;
                            for (int i = 0; i < b.Length; ++i) { sb.Append(" " + Convert.ToString((b as long[])[i])); }

                            fmtDesc = string.Format("I8[{0}]", b.Length);
                            valDesc = sb.ToString();
                        }
                    }
                    break;
                case ItemFmt.F4:
                    if (value == null)
                    {
                        fmtDesc = "F4[0]";
                    }
                    else
                    {
                        if (value is float)
                        {
                            float b = (float)value;
                            fmtDesc = "F4[1]";
                            valDesc = Convert.ToString(b);
                        }
                        else if (value is float[])
                        {
                            float[] b = (float[])value;
                            for (int i = 0; i < b.Length; ++i) { sb.Append(" " + Convert.ToString((b as float[])[i])); }

                            fmtDesc = string.Format("F4[{0}]", b.Length);
                            valDesc = sb.ToString();
                        }
                    }
                    break;
                case ItemFmt.F8:
                    if (value == null)
                    {
                        fmtDesc = "F8[0]";
                    }
                    else
                    {
                        if (value is double)
                        {
                            double b = (double)value;
                            fmtDesc = "F8[1]";
                            valDesc = Convert.ToString(b);
                        }
                        else if (value is double[])
                        {
                            double[] b = (double[])value;
                            for (int i = 0; i < b.Length; ++i) { sb.Append(" " + Convert.ToString((b as double[])[i])); }

                            fmtDesc = string.Format("F8[{0}]", b.Length);
                            valDesc = sb.ToString();
                        }
                    }
                    break;
            }


            return Tuple.Create<string, string>(fmtDesc, valDesc);
        }

        #region delegate Method
        delegate void TextBoxAppendCallback(TextBox Ctl, string Str);
        public void TextBoxAppend(TextBox Ctl, string Str)
        {
            if (Ctl.InvokeRequired)
            {
                TextBoxAppendCallback d = new TextBoxAppendCallback(TextBoxAppend);
                this.Invoke(d, new object[] { Ctl, Str });
            }
            else
            {
                Ctl.AppendText(Str);
            }
        }

        delegate void GridViewAddRows(DataGridView Ctl, string[] strArray);
        public void InvokeGridViewAddRows(DataGridView Ctl, string[] strArray)
        {
            if (Ctl.InvokeRequired)
            {
                GridViewAddRows addRows = new GridViewAddRows((InvokeGridViewAddRows));
                this.Invoke(addRows, strArray);
            }
            else
            {
                // 在這裡寫入原本取到str後要對dataGridView做的事
                Ctl.Rows.Add(strArray);           // 加入列  
            }
        }

        delegate void GridVieClearRows(DataGridView Ctl);
        public void InvokeGridVieclearRows(DataGridView Ctl)
        {
            if (Ctl.InvokeRequired)
            {
                GridVieClearRows clearRows = new GridVieClearRows((InvokeGridVieclearRows));
                this.Invoke(clearRows);
            }
            else
            {
                // 在這裡寫入原本取到str後要對dataGridView做的事
                Ctl.Rows.Clear();
            }
        }
        #endregion

        private void 離開ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
        }

        #region Initnal Call Back
        public void Initnal_SECSFunction_CallBack(SECSFunction_CallBack funcCallBack)
        {
            function_CallBack = funcCallBack;
        }

        public void Initnal_RemoteCommand_CallBack(RemoteCommand_CallBack funcCallBack)  //S2F41
        {
            remoteCommand_CallBack = funcCallBack;
        }

        #endregion

        //擴張用
        public bool isRemote()
        {
            return isControlRemote() && isCommunicating();
        }

        public bool isCommunicating()
        {
            try
            {

                if (_gemControler.CommunicationState == eCommunicationState.Communicating)
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

        public bool isControlRemote()
        {
            try
            {

                if (_gemControler.ControlMode == eControlMode.OnlineRemote)
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

        public bool isControlLocal()
        {
            try
            {

                if (_gemControler.ControlMode == eControlMode.OnlineLocal)
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

        public int GetSV(int id, out byte val, out string errLog)
        {
            errLog = string.Empty;
            ItemFmt fmt;
            object v;
            int rtn =_gemControler.GetSV((ulong)id, out fmt, out v, out errLog);
            val = Convert.ToByte(v);
            if (fmt == ItemFmt.B || fmt == ItemFmt.U1)
                return rtn;
            else
                return -1;
        }

        public int GetSV(int id, out uint val, out string errLog)
        {
            errLog = string.Empty;
            ItemFmt fmt;
            object v;
            int rtn = _gemControler.GetSV((ulong)id, out fmt, out v, out errLog);
            val = Convert.ToUInt32(v);
            if (fmt == ItemFmt.U4)
                return rtn;
            else
                return -1;
        }

        public int GetSV(int id, out string val, out string errLog)
        {
            errLog = string.Empty;
            ItemFmt fmt;
            object v;
            int rtn = _gemControler.GetSV((ulong)id, out fmt, out v, out errLog);
            val = Convert.ToString(v);
            if (fmt == ItemFmt.A)
                return rtn;
            else
                return -1;
        }

        public int UpdateSV(int id, object val, out string errLog)
        {
            errLog = string.Empty;
            return _gemControler.UpdateSV((ulong)id, val, out errLog);
        }

        public int UpdateEC(int id, object val, out string errLog)
        {
            errLog = string.Empty;
            return _gemControler.UpdateEC((ulong)id, val, true, out errLog);
        }

        public int EventReportSend(int id, out string errLog)
        {
            errLog = string.Empty;
            return _gemControler.EventReportSend((ulong)id, out errLog);
        }

        public int AlarmReportSend(int id, bool bSet, out string errLog)
        {
            errLog = string.Empty;
            return _gemControler.AlarmReportSend((ulong)id, bSet, out errLog);
        }

        public void SecsDataClear(SecsData s)
        {
            switch (s)
            {
                case SecsData.SlotMap:
                    SlotMapData.Clear();
                    break;
                case SecsData.Release:
                    ReleaseData.Clear();
                    break;
                case SecsData.MeasureStart:
                    MeasureStartData.Clear();
                    break;
                case SecsData.Cancel:
                    CancelData.Clear();
                    break;
                case SecsData.AccessModeChange:
                    AccessModeChangeData.Clear();
                    break;
                case SecsData.AccessModeAsk:
                    AccessModeAskData.Clear();
                    break;
                case SecsData.ChangeRecipe:
                    ChangeRecipeData.Clear();
                    break;
                default:
                    break;
            }
        }
        public string SecsDataGet(SecsData s, SecsDataElement e)
        {
            string rtn = "";
            if (e == SecsDataElement.LoadPortID)
            {
                switch (s)
                {
                    case SecsData.SlotMap:
                        rtn = SlotMapData.LoadPortID;
                        break;
                    case SecsData.Release:
                        rtn = ReleaseData.LoadPortID;
                        break;
                    case SecsData.MeasureStart:
                        rtn = MeasureStartData.LoadPortID;
                        break;
                    case SecsData.Cancel:
                        rtn = CancelData.LoadPortID;
                        break;
                    case SecsData.AccessModeChange:
                        rtn = AccessModeChangeData.LoadPortID;
                        break;
                    case SecsData.AccessModeAsk:
                        rtn = AccessModeAskData.LoadPortID;
                        break;
                    case SecsData.ChangeRecipe:
                        rtn = ChangeRecipeData.LoadPortID;
                        break;
                    default:
                        break;
                }
            }
            else if (e == SecsDataElement.CarrierID)
            {
                switch (s)
                {
                    case SecsData.SlotMap:
                        rtn = SlotMapData.CarrierID;
                        break;
                    case SecsData.Release:
                        rtn = ReleaseData.CarrierID;
                        break;
                    case SecsData.MeasureStart:
                        rtn = MeasureStartData.CarrierID;
                        break;
                    case SecsData.Cancel:
                        rtn = CancelData.CarrierID;
                        break;
                    case SecsData.AccessModeChange:
                        rtn = AccessModeChangeData.CarrierID;
                        break;
                    case SecsData.AccessModeAsk:
                        rtn = AccessModeAskData.CarrierID;
                        break;
                    case SecsData.ChangeRecipe:
                        rtn = ChangeRecipeData.CarrierID;
                        break;
                    default:
                        break;
                }
            }
            return rtn;
        }

        public void SecsDataSet(SecsData s, SecsDataElement e, string value)
        {
            if (e == SecsDataElement.LoadPortID)
            {
                switch (s)
                {
                    case SecsData.SlotMap:
                        SlotMapData.LoadPortID = value;
                        break;
                    case SecsData.Release:
                        ReleaseData.LoadPortID = value;
                        break;
                    case SecsData.MeasureStart:
                        MeasureStartData.LoadPortID = value;
                        break;
                    case SecsData.Cancel:
                        CancelData.LoadPortID = value;
                        break;
                    case SecsData.AccessModeChange:
                        AccessModeChangeData.LoadPortID = value;
                        break;
                    case SecsData.AccessModeAsk:
                        AccessModeAskData.LoadPortID = value;
                        break;
                    case SecsData.ChangeRecipe:
                        ChangeRecipeData.LoadPortID = value;
                        break;
                    default:
                        break;
                }
            }
            else if (e == SecsDataElement.CarrierID)
            {
                switch (s)
                {
                    case SecsData.SlotMap:
                        SlotMapData.CarrierID = value;
                        break;
                    case SecsData.Release:
                        ReleaseData.CarrierID = value;
                        break;
                    case SecsData.MeasureStart:
                        MeasureStartData.CarrierID = value;
                        break;
                    case SecsData.Cancel:
                        CancelData.CarrierID = value;
                        break;
                    case SecsData.AccessModeChange:
                        AccessModeChangeData.CarrierID = value;
                        break;
                    case SecsData.AccessModeAsk:
                        AccessModeAskData.CarrierID = value;
                        break;
                    case SecsData.ChangeRecipe:
                        ChangeRecipeData.CarrierID = value;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public enum ProcessState
    {
        Initial = 0,
        Idle = 1,
        Run = 2,
        Stop = 3,
        Pause = 4,
        Down = 5
    }

    public enum PPBodyType
    {
        Unformatted,
        Formatted,
        Both
    }

    public enum ControlJobState
    {
        QUEUED = 0,
        SELECTED = 1,
        WAITING_FOR_START = 2,
        EXECUTING = 3,
        PAUSED = 4,
        COMPLETED = 5,
    }

    public enum ProcessJobState
    {
        QUEUED = 0,
        SETTING_UP = 1,
        WAITING_FOR_START = 2,
        PROCESSING = 3,
        PROCESS_COMPLETE = 4,
        RESERVED = 5,
        PAUSING = 6,
        PAUSED = 7,
        STOPPING = 8,
        ABORTING = 9,
        STOPPED = 10,
        ABORTED = 11,
        UNQUEUED = 12,
    }

    public enum CarrierAccessingState
    {
        NOT_ACCESSED = 0,
        IN_ACCESS = 1,
        CARRIER_COMPLETE = 2,
        CARRIER_STOPPED = 3,
    }

    public enum CarrierIDState
    {
        ID_NOT_READ = 0,
        WAITING_FOR_HOST = 1,
        ID_VERIFICATION_OK = 2,
        ID_VERIFICATION_FAILED = 3,
    }

    public enum SlotMapState
    {
        SLOT_MAP_NOT_READ = 0,
        WAITING_FOR_HOST = 1,
        SLOT_MAP_VERIFICATION_OK = 2,
        SLOT_MAP_VERIFICATION_FAILED = 3,
    }

    public enum SubstrateTransState
    {
        AT_SOURCE = 0,
        AT_WORK = 1,
        AT_DESTINATION = 2,
        EXTINXTION = 3,
    }

    public enum SubstProcState
    {
        NEEDS_PROCESSING = 0,
        IN_PROCESS = 1,
        PROCESSED = 2,
        ABORTED = 3,
        STOPPED = 4,
        REJECTED = 5,
        LOST = 6,
        SKIPPED = 7,
    }

    public class Substrate
    {
        public string objID;
        public string lotID;
        public byte materialStatus;
        public string substDesrination;
        public List<string> substHistory;
        public string substLocID;
        public SubstProcState substProcState;
        public string substSource;
        public SubstrateTransState substState;
        public byte substType;
        public byte substUsage;
        //public ushort substWID_Angle;
        //public string batchLocID;
        //public string substPosInBatch;

        public Substrate()
        {
            objID = "";
            lotID = "";
            substHistory = new List<string>();
            substLocID = "";
            substSource = "";
            substState = SubstrateTransState.AT_SOURCE;
            substType = 0; //0 = WAFER 1 = FLAT PANEL 2 = COMPACT DISK 3 = MASK OR RETICLE
        }
    }
}
