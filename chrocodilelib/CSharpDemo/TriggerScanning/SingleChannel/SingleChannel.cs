using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PT
{
    public partial class SingleChannel : Form
    {
        static CHRocodileLib.SynchronousConnection Conn;
        
        private int[] OutputSignals;
        private int ScanLineNo, LineSampleCount;
        private int AllSampleCount;
        private int ScanLineIdx, SampleIdx;

        private CHRocodileLib.Data ScanData = null;
        
        private bool InProcess;
        private bool isShown;
        private Bitmap bm;
        private int FirstXPixel;
        private int CurrentPixelX, PixelXStep;
        private int CurrentPixelY, PixelYStep;
        private int DrawLineIdx, DrawSampleIdx;
        private double SigMin, SigMax;
        public static ActUtlType64Lib.ActUtlType64Class actUtlType;
        private bool bSim;
        public SingleChannel(int simulate)
        {
            InitializeComponent();
            bm = new Bitmap(PPaint.Width, PPaint.Height);
            CleanDataBitmap();
            actUtlType = new ActUtlType64Lib.ActUtlType64Class();
            bSim = simulate != 0 ? true : false;
            if(!bSim)
            {
                try
                {
                    int iRet;
                    actUtlType.ActLogicalStationNumber = 1;
                    iRet = actUtlType.Open();
                    if (iRet != 0)
                    {
                        MessageBox.Show("PT PLC Init Fail.");
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("PT PLC Init Fail:" + ee);
                }
                cbSelectPoint.SelectedIndex = 1;
                updatePosSpd(1);
                timer1.Interval = 200;
                timer1.Enabled = true;
            }
        }

        public int GetDevice(string name)
        {
            int rtn1,rtn2;
            actUtlType.GetDevice(name, out rtn1);
            string tmp1 = name.Substring(0, 1);
            string tmp2 = name.Substring(1);
            int num = Convert.ToInt32(tmp2);
            actUtlType.GetDevice(tmp1 + (num+1).ToString(), out rtn2);
            return rtn1 + rtn2*65536;
        }
        public short GetDevice2(string name)
        {
            short rtn;
            actUtlType.GetDevice2(name, out rtn);
            return rtn;
        }

        public void SetDevice(string name, int data)
        {
            int data1 = data % 65536;
            int data2 = data / 65536;
            string tmp1 = name.Substring(0, 1);
            string tmp2 = name.Substring(1);
            int num = Convert.ToInt32(tmp2);
            string name2 = tmp1 + (num + 1).ToString();
            actUtlType.SetDevice(name, data1);
            actUtlType.SetDevice(name2, data2);
        }
        public void SetDevice2(string name, short data)
        {
            actUtlType.SetDevice2(name, data);
        }

        public int GetPos()
        {
            return GetDevice("D1000");
        }
        public int GetSpeed()
        {
            return GetDevice("D1016");
        }
        public int GetDriverAlarmCode()
        {
            return (int)GetDevice2("D1006");
        }
        public bool GetDriverAlarm()
        {
            if (bSim)
            {
                return false;
            }
            return GetDevice2("X0005")==1?false:true;
        }
        public bool GetJogMoving(int dir)
        {
            string cmd;
            cmd = "M1" + ((3 - dir)*10).ToString("00");
            return GetDevice2(cmd) == 1 ? true : false;
        }
        public bool GetPointMoveFinish(int point)
        {
            if (bSim)
            {
                return true;
            }

            string cmd;
            cmd = "M17" + point.ToString("0");
            return GetDevice2(cmd) == 1 ? true : false;
        }
        public bool GetPLimit()
        {
            return GetDevice2("X0011") == 1 ? true : false;
        }
        public bool GetNLimit()
        {
            return GetDevice2("X0010") == 1 ? true : false;
        }
        public bool GetServoReady()
        {
            return GetDevice2("X0014") == 1 ? true : false;
        }
        public bool GetZeroSpeed()
        {
            return GetDevice2("X0015") == 1 ? true : false;
        }
        public bool FindHomeFinish()
        {
            if (bSim)
            {
                return true;
            }
            return GetDevice2("M70") == 1 ? true : false;
        }
        public bool FindHomeing()
        {
            return GetDevice2("M40") == 1 ? true : false;
        }
        public void SetJogSpeed(int speed)
        {
            SetDevice("D300", speed);
        }
        public int GetJogSpeed()
        {
            return GetDevice("D300");
        }
        public void MotionStop()
        {
            SetDevice2("M55", 1);
            SpinWait.SpinUntil(() => false, 20);
            SetDevice2("M55", 0);
        }
        public void ResetDriverAlarm()
        {
            if (bSim)
            {
                return;
            }
            SetDevice2("M39", 1);
            SpinWait.SpinUntil(() => false, 20);
            SetDevice2("M39", 0);
        }
        public void FindHome()
        {
            if (bSim)
            {
                return;
            }
            SetDevice2("L150", 1);
            SpinWait.SpinUntil(() => false, 20);
            SetDevice2("L150", 0);
        }
        public void SetSpeed(int point, int speed)
        {
            string cmd;
            cmd = "D55" + (point*2).ToString("00");
            SetDevice(cmd, speed);
        }
        public int GetSpeed(int point)
        {
            string cmd;
            cmd = "D55" + (point * 2).ToString("00");
            return GetDevice(cmd);
        }

        public void SetPosition(int point, int pos)
        {
            string cmd;
            cmd = "D40" + (point * 2).ToString("00");
            SetDevice(cmd, pos);
        }
        public int GetPosition(int point)
        {
            string cmd;
            cmd = "D40" + (point * 2).ToString("00");
            return GetDevice(cmd);
        }

        public void PointMove(int point, int speed)
        {
            SetSpeed(point, speed);
            string cmd;
            cmd = "L11" + point.ToString();
            SetDevice2(cmd, 1);
            SpinWait.SpinUntil(() => false, 20);
            SetDevice2(cmd, 0);
        }
        public void PointMove(int point)
        {
            if (bSim)
            {
                return;
            }
            string cmd;
            cmd = "L11" + point.ToString();
            SetDevice2(cmd, 1);
            SpinWait.SpinUntil(() => false, 20);
            SetDevice2(cmd, 0);
        }

        public void JogMove(int dir, short on)
        {
            string cmd;
            cmd = "L10" + (2-dir).ToString();
            SetDevice2(cmd, on);
            SpinWait.SpinUntil(() => false, 20);
        }

        private void BtConnect_Click(object sender, EventArgs e)
        {
            bool bConnect = false;
            //connect to device
            if (sender == BtConnect)
            {
                if (Conn != null) return;
                try
                {
                    //Open connection in synchronous mode
                    var DeviceType = CHRocodileLib.DeviceType.Chr1;
                    if (RBCHR2.Checked)
                        DeviceType = CHRocodileLib.DeviceType.Chr2;
                    else if (RBCHRC.Checked)
                        DeviceType = CHRocodileLib.DeviceType.ChrCMini;
                    string strConInfo = TbConInfo.Text;
                    Conn = new CHRocodileLib.SynchronousConnection(strConInfo, DeviceType);
                    SetupDevice(DeviceType);
                    bConnect = true;
                }
                catch (Exception _e)
                {
                    MessageBox.Show("Error in connecting to the CHR device: " + _e.Message);
                }
            }
            //close connection to device
            else
            {
                CloseConnection();
            }
            EnableGui(bConnect, false);
        }


        public void CloseConnection()
        {
            StopScan();
            while (InProcess)
                Task.Delay(20);
            Conn.Close();
            Conn = null;
        }

        public bool OpenConnection()
        {
            if (Conn != null) return false;
            Conn = new CHRocodileLib.SynchronousConnection(TbConInfo.Text, CHRocodileLib.DeviceType.ChrCMini);
            //SetupDevice(CHRocodileLib.DeviceType.ChrCMini);
            int[] aTempSig = { 256, 264, 272 };
            var oRsp = Conn.Exec(CHRocodileLib.CmdID.OutputSignals, aTempSig);
            EnableGui(true, false);
            return true;
        }

        public void StartTrigger(int len, int start, int stop, int interval)
        {
            Conn.Exec(CHRocodileLib.CmdID.EncoderCounter, 0, start +20);
            
            //Set trigger settings
            SendTriggerSetting(start,stop,interval);
            //use trigger each mode
            Conn.Exec(CHRocodileLib.CmdID.DeviceTriggerMode, (int)CHRocodileLib.TriggerMode.TriggerEach);

            //start recording modes
            Conn.StartRecording(len);

            AllSampleCount = len;
            //reset record data
            ScanData = null;
            InProcess = true;
        }

        public bool isFinish()
        {
            ScanData = Conn.GetNextSamples();
            //get total number of recorded data, TotalNumSamples returns the total recorded sample count
            if (ScanData.TotalNumSamples >= AllSampleCount)
            {
                Conn.StopRecording();
                InProcess = false;
                return true;
            }
            else
            {
                Conn.StopRecording();
                InProcess = false;
                return false;
            }
        }

        public void getData(out double[] data)
        {
            if(ScanData.TotalNumSamples >= AllSampleCount)
            {
                double[] rtn = new double[AllSampleCount];
                for (int i = 0; i < AllSampleCount; i++)
                    rtn[i] = ScanData.Get(i, 0, 0);
                data = rtn;
            }
            else
            {
                double[] rtn2 = new double[ScanData.TotalNumSamples];
                for (int i = 0; i < ScanData.TotalNumSamples; i++)
                    rtn2[i] = ScanData.Get(i, 0, 0);
                data = rtn2;
            }           
        }

        public void getData2(out double[] data2)
        {
            if (ScanData.TotalNumSamples >= AllSampleCount)
            {
                double[] rtn = new double[AllSampleCount];
                for (int i = 0; i < AllSampleCount; i++)
                    rtn[i] = ScanData.Get(i, 1, 0);
                data2 = rtn;
            }
            else
            {
                double[] rtn2 = new double[ScanData.TotalNumSamples];
                for (int i = 0; i < ScanData.TotalNumSamples; i++)
                    rtn2[i] = ScanData.Get(i, 1, 0);
                data2 = rtn2;
            }
        }

        public void getData3(out double[] data3)
        {
            if (ScanData.TotalNumSamples >= AllSampleCount)
            {
                double[] rtn = new double[AllSampleCount];
                for (int i = 0; i < AllSampleCount; i++)
                    rtn[i] = ScanData.Get(i, 2, 0);
                data3 = rtn;
            }
            else
            {
                double[] rtn2 = new double[ScanData.TotalNumSamples];
                for (int i = 0; i < ScanData.TotalNumSamples; i++)
                    rtn2[i] = ScanData.Get(i, 2, 0);
                data3 = rtn2;
            }
        }

        //public void getData3(out double[] data, out double[] data2, out double[] data3)
        //{
        //    if (ScanData.TotalNumSamples >= AllSampleCount)
        //    {
        //        double[] rtn1 = new double[AllSampleCount];
        //        double[] rtn2 = new double[AllSampleCount];
        //        double[] rtn3 = new double[AllSampleCount];
        //        for (int i = 0; i < AllSampleCount; i++)
        //        {
        //            rtn1[i] = ScanData.Get(i, 0, 0);
        //            rtn2[i] = ScanData.Get(i, 1, 0);
        //            rtn3[i] = ScanData.Get(i, 2, 0);
        //        }
        //        data = rtn1;
        //        data2 = rtn2;
        //        data3 = rtn3;
        //    }
        //    else
        //    {
        //        double[] rtn1 = new double[ScanData.TotalNumSamples];
        //        double[] rtn2 = new double[ScanData.TotalNumSamples];
        //        double[] rtn3 = new double[ScanData.TotalNumSamples];
        //        for (int i = 0; i < ScanData.TotalNumSamples; i++)
        //        {
        //            rtn1[i] = ScanData.Get(i, 0, 0);
        //            rtn2[i] = ScanData.Get(i, 1, 0);
        //            rtn3[i] = ScanData.Get(i, 2, 0);
        //        }
        //        data = rtn1;
        //        data2 = rtn2;
        //        data3 = rtn3;
        //    }
        //}

        private void SetupDevice(CHRocodileLib.DeviceType _nDeviceType)
        {
            //according to device type, define available axis
            int nTemp = CBAxis.SelectedIndex;       
            CBAxis.Items.Clear();
            CBAxis.Items.Add("X-Axis");
            CBAxis.Items.Add("Y-Axis");
            CBAxis.Items.Add("Z-Axis");
            if (_nDeviceType != CHRocodileLib.DeviceType.Chr1)
            {
                CBAxis.Items.Add("U-Axis");
                CBAxis.Items.Add("V-Axis");
            }
            if ((nTemp >= 0) && (nTemp < CBAxis.Items.Count))
                CBAxis.SelectedIndex = nTemp;
            else
                CBAxis.SelectedIndex = 0;
            SetDeviceOutput();
        }


        private void SendTriggerSetting()
        {
            if (RBSyncSig.Checked)
            {
                //Use sync-in signal to trigger
                //disabel encoder trigger
                Conn.Exec(CHRocodileLib.CmdID.EncoderTriggerEnabled, 0);
            }
            else
            {
                //use encoder to trigger
                Conn.Exec(CHRocodileLib.CmdID.EncoderTriggerEnabled, 1);
                //set encoder trigger property
                //int nAxis = CBAxis.SelectedIndex;
                int nStartPos = int.Parse(TBStartPos.Text);
                int nStopPos = int.Parse(TBStopPos.Text);
                float nInterval = float.Parse(TBInterval.Text, CultureInfo.InvariantCulture);
                int bTriggerOnReturn = CBTriggerOnReturn.Checked ? 1 : 0;
                Conn.Exec(CHRocodileLib.CmdID.EncoderTriggerProperty, 0, nStartPos, nStopPos, nInterval, bTriggerOnReturn);
                TBSampleNo.Text = ((int)((nStopPos - nStartPos) / nInterval + 1)).ToString();
            }     
        }

        private void SendTriggerSetting(int startPos, int stopPos, float interval)
        {
                //use encoder to trigger
                Conn.Exec(CHRocodileLib.CmdID.EncoderTriggerEnabled, 1);
                //set encoder trigger property
                //int nAxis = CBAxis.SelectedIndex;
                Conn.Exec(CHRocodileLib.CmdID.EncoderTriggerProperty, 0, startPos, stopPos, interval, 0);
        }


        //Set output signals
        private void SetDeviceOutput()
        {
            try
            {
                if (TBSignal.Text == "")
                    throw new Exception("No signals are selected for CHR device!");
                char[] delimiters = new char[] { ' ', ',', ';' };
                string[] aTemp = TBSignal.Text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                int[] aTempSig = Array.ConvertAll(aTemp, int.Parse);
                Array.Sort(aTempSig);
                var oRsp = Conn.Exec(CHRocodileLib.CmdID.OutputSignals, aTempSig);
                OutputSignals = oRsp.GetParam<int[]>(0);
                //update display signal combo box
                int nSelectIdx = -1;
                if (CBDisplaySig.SelectedIndex >= 0)
                {
                    int nLastSig = int.Parse(CBDisplaySig.SelectedItem.ToString());
                    nSelectIdx = Array.IndexOf(OutputSignals, nLastSig);
                }
                CBDisplaySig.Items.Clear();
                foreach (var nID in OutputSignals)
                    CBDisplaySig.Items.Add(nID.ToString());
                if (nSelectIdx > -1)
                    CBDisplaySig.SelectedIndex = nSelectIdx;
                else
                    CBDisplaySig.SelectedIndex = 0;
            }
            catch
            {
                Debug.Fail("Cannot set output signals.");
            }           
        }



        private void EnableGui(bool _bConnect, bool _bInScan)
        {
            BtConnect.Enabled = !_bConnect;
            BtDisCon.Enabled = _bConnect;
            BtScan.Enabled = _bConnect;            
            TBSignal.Enabled = _bConnect && (!_bInScan);
            BtEncoderPos.Enabled = _bConnect && RBEncTrigger.Checked && (!_bInScan);
        } 


        private void BtEncoderPos_Click(object sender, EventArgs e)
        {
            try
            {
                //Set encoder current position
                int nPos = int.Parse(TBEncoderPos.Text);
                Conn.Exec(CHRocodileLib.CmdID.EncoderCounter, CBAxis.SelectedIndex, nPos);
            }
            catch
            {
                Debug.Fail("Cannot set encoder pos.");
            }          
        }

        private void TBSignal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                SetDeviceOutput();
            }
        }

        private void RBSyncSig_Click(object sender, EventArgs e)
        {
            bool bEncTrigger = (sender == RBEncTrigger);
            CBAxis.Enabled = bEncTrigger;
            TBStartPos.Enabled = bEncTrigger;
            TBStopPos.Enabled = bEncTrigger;
            TBInterval.Enabled = bEncTrigger;
            CBTriggerOnReturn.Enabled = bEncTrigger;
            BtEncoderPos.Enabled = bEncTrigger;
            TBEncoderPos.Enabled = bEncTrigger;
            TBSampleNo.Enabled = !bEncTrigger;
        }

        //during scan to process/display data
        private void timerProcess_Tick(object sender, EventArgs e)
        {

            //to make sure the last process has been finished
            if (InProcess)
                return;

            InProcess = true;

            //Read Data
            ScanData = Conn.GetNextSamples();

            //get total number of recorded data, TotalNumSamples returns the total recorded sample count
            var nTotalSampleCount = ScanData.TotalNumSamples;

            ScanLineIdx = (int)(Math.Floor((double)nTotalSampleCount / LineSampleCount));
            SampleIdx = (int)nTotalSampleCount - ScanLineIdx * LineSampleCount;

            //if there is new data comes in, update display
            //NumSamples returns the new sample count from the last call of GetNextSampels
            if (ScanData.NumSamples>0)
                UpdateDataDisplay(false);


            //enough samples have been saved, stop scan
            string data;
            if (nTotalSampleCount == AllSampleCount)
            {
                StopScan();
                FileStream fs = new FileStream("D:\\FTGM1\\TEST.CSV", System.IO.FileMode.Create, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                for (int i = 0; i < AllSampleCount; i++)
                {
                    data = "";
                    for (int j = 0; j < 1; j++)
                    {
                        //dataGridView1.Rows[0].Cells[1].Value = Convert.ToString(2);
                        string str = ScanData.Get(i,0,0).ToString() + ','; //沒數據會儲存錯誤
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
                
            }


            InProcess = false;
        }

        private void BtScan_Click(object sender, EventArgs e)
        {
            if (timerProcess.Enabled)
            {
                StopScan();
            }
            else
            {
                try
                {
                    ScanLineNo = int.Parse(TBLineNo.Text);
                    LineSampleCount = int.Parse(TBSampleNo.Text);
                    if (RBEncTrigger.Checked && CBTriggerOnReturn.Checked)
                        ScanLineNo *= 2;

                    AllSampleCount = ScanLineNo * LineSampleCount;

                    if (AllSampleCount == 0)
                        return;


                    //Set trigger settings
                    SendTriggerSetting();
                    //use trigger each mode
                    Conn.Exec(CHRocodileLib.CmdID.DeviceTriggerMode, (int)CHRocodileLib.TriggerMode.TriggerEach);
                    
                    //start recording modes
                    Conn.StartRecording(AllSampleCount);
                    //reset record data
                    ScanData = null;
                    
                    SigMin = double.Parse(TBSigMin.Text);
                    SigMax = double.Parse(TBSigMax.Text);
                   
                    ScanLineIdx = 0;
                    SampleIdx = 0;
                    
                    InProcess = false;
                    ResetDrawing();

                    //use timer to display data
                    timerProcess.Enabled = true;
                    EnableGui(true, true);
                    BtScan.Text = "Cancel Scan";
                    PPaint.Invalidate();
                }
                catch
                {
                    Debug.Fail("Cannot set scan related parameters.");
                }
                
            }
        }

        private void StopScan()
        {
            if (!timerProcess.Enabled)
                return;

            timerProcess.Enabled = false;
            
            //quit recording modes,
            //StopRecording also returns recorded data object,
            //which is the same as the data object from GetNextSamples in the timer routine,
            Conn.StopRecording();

            try
            {
                // Set back to free run mode
                Conn.Exec(CHRocodileLib.CmdID.DeviceTriggerMode, CHRocodileLib.TriggerMode.FreeRun);
            }
            catch
            {

            }
            EnableGui(true, false);
            BtScan.Text = "Start Scan";
        }

        private void ResetDrawing()
        {
            CleanDataBitmap();
            if (ScanLineNo == 0)
                return;
            int nTemp = PPaint.Height % ScanLineNo;
            CurrentPixelY = nTemp / 2;
            PixelYStep = PPaint.Height / ScanLineNo;
            if (LineSampleCount > PPaint.Width)
            {
                FirstXPixel = 0;
                PixelXStep = 1;
            }
            else
            {
                nTemp = PPaint.Width % LineSampleCount;
                FirstXPixel = nTemp / 2;
                PixelXStep = PPaint.Width / LineSampleCount;
            }
            CurrentPixelX = FirstXPixel;
            DrawLineIdx = 0;
            DrawSampleIdx = 0;
        }

        //set display min and max value for heat map
        private void TBSigMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SigMin = double.Parse(TBSigMin.Text);
                SigMax = double.Parse(TBSigMax.Text);
                UpdateDataDisplay(true);
            }
        }

        private void btnJogP_Click(object sender, EventArgs e)
        {
            JogMove(1,1);
        }

        private void btnJogN_Click(object sender, EventArgs e)
        {
            JogMove(0,1);
        }

        private void btnGotoP0_Click(object sender, EventArgs e)
        {
            PointMove(0);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            MotionStop();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            FindHome();
        }

        private void btnGotoPoint_Click(object sender, EventArgs e)
        {
            int point = cbSelectPoint.SelectedIndex;
            PointMove(point);
        }

        private void btnSavePosSpd_Click(object sender, EventArgs e)
        {
            int point = cbSelectPoint.SelectedIndex;
            int jogspeed = (int)(Convert.ToDouble(tb_JogSpeed.Text) * 100);
            int pos = (int)(Convert.ToDouble(tb_InputPos.Text) * 10000);
            int posspeed = (int)(Convert.ToDouble(tb_InputSpd.Text) * 100);
            SetJogSpeed(jogspeed);
            SetPosition(point, pos);
            SetSpeed(point, posspeed);
        }

        private void updatePosSpd(int point)
        {
            tb_JogSpeed.Text = ((double)GetJogSpeed() / 100.0).ToString("F2");
            tb_InputPos.Text = ((double)GetPosition(point) / 10000.0).ToString("F4");
            tb_InputSpd.Text = ((double)GetSpeed(point) / 100.0).ToString("F2");
        }

        private void cbSelectPoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            updatePosSpd(cbSelectPoint.SelectedIndex);
        }

        private void btnJogP_MouseDown(object sender, MouseEventArgs e)
        {
            JogMove(1, 1);
        }

        private void btnJogP_MouseUp(object sender, MouseEventArgs e)
        {
            JogMove(1, 0);
        }

        private void btnJogN_MouseDown(object sender, MouseEventArgs e)
        {
            JogMove(0, 1);
        }

        private void btnJogN_MouseUp(object sender, MouseEventArgs e)
        {
            JogMove(0, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(isShown)
            {
                lb_DECNum.Text = GetDriverAlarmCode().ToString();
                lb_SpdNum.Text = ((double)GetSpeed() / 10000.0).ToString("F4");
                lb_PosNum.Text = ((double)GetPos() / 10000.0).ToString("F4");
                lb_HomeFinish.BackColor = FindHomeFinish() ? Color.Yellow : Color.Transparent;
                lb_HomeGoing.BackColor = FindHomeing() ? Color.Yellow : Color.Transparent;
                lb_INP0.BackColor = GetPointMoveFinish(0) ? Color.Yellow : Color.Transparent;
                lb_INP1.BackColor = GetPointMoveFinish(1) ? Color.Yellow : Color.Transparent;
                lb_INP2.BackColor = GetPointMoveFinish(2) ? Color.Yellow : Color.Transparent;
                lb_INP3.BackColor = GetPointMoveFinish(3) ? Color.Yellow : Color.Transparent;
                lb_INP4.BackColor = GetPointMoveFinish(4) ? Color.Yellow : Color.Transparent;
                lb_INP5.BackColor = GetPointMoveFinish(5) ? Color.Yellow : Color.Transparent;
                lb_INP6.BackColor = GetPointMoveFinish(6) ? Color.Yellow : Color.Transparent;
                lb_INP7.BackColor = GetPointMoveFinish(7) ? Color.Yellow : Color.Transparent;
                lb_INP8.BackColor = GetPointMoveFinish(8) ? Color.Yellow : Color.Transparent;
                lb_INP9.BackColor = GetPointMoveFinish(9) ? Color.Yellow : Color.Transparent;
                lb_NJogging.BackColor = GetJogMoving(0) ? Color.Yellow : Color.Transparent;
                lb_PJogging.BackColor = GetJogMoving(1) ? Color.Yellow : Color.Transparent;
                lb_NLimit.BackColor = GetNLimit() ? Color.Yellow : Color.Transparent;
                lb_PLimit.BackColor = GetPLimit() ? Color.Yellow : Color.Transparent;
                lb_ServoReady.BackColor = GetServoReady() ? Color.Yellow : Color.Transparent;
                lb_ServoBusy.BackColor = GetZeroSpeed() ? Color.Yellow : Color.Transparent;
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            isShown = false;
            Hide();
        }

        private void btnResetAlarm_Click(object sender, EventArgs e)
        {
            ResetDriverAlarm();
        }

        private void SingleChannel_Shown(object sender, EventArgs e)
        {
            isShown = true;
        }


        //set encoder trigger property
        private void TBEncoderTriggerProperty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                int nStartPos = int.Parse(TBStartPos.Text);
                int nStopPos = int.Parse(TBStopPos.Text);
                float nInterval = float.Parse(TBInterval.Text, CultureInfo.InvariantCulture);
                TBSampleNo.Text = ((int)((nStopPos - nStartPos) / nInterval + 1)).ToString();
            }
        }

        //select signal to display
        private void CBDisplaySig_SelectedIndexChanged(object sender, EventArgs e)
        {
            while (InProcess)
                Task.Delay(20);

            UpdateDataDisplay(true);
        }

        private void PPaint_Paint(object sender, PaintEventArgs e)
        {
           PPaint.CreateGraphics().DrawImage(bm, 0, 0);
        }

        //display data
        private void UpdateDataDisplay(bool _bRepaint)
        {            
            if (_bRepaint)
                ResetDrawing();

            if (ScanData == null)
                return;

            if (AllSampleCount == 0)
                return;


            float nSamplePerPixel = 1;
            if (LineSampleCount > bm.Width)
                nSamplePerPixel = (float)(LineSampleCount) / bm.Width;
            int nSigIdx = CBDisplaySig.SelectedIndex;

            using (Graphics g = Graphics.FromImage(bm))
            using (SolidBrush oBr = new SolidBrush(Color.Black))
            {
                int nInitLineIdx = DrawLineIdx;
                while (DrawLineIdx <= ScanLineIdx)
                {
                    int nSampleNo = LineSampleCount;
                    int nSampleStartIdx = 0;
                    if (DrawLineIdx == ScanLineIdx)
                        nSampleNo = SampleIdx;
                    if (DrawLineIdx == nInitLineIdx)
                        nSampleStartIdx = DrawSampleIdx;
                    int nCurPixelStartIdx = nSampleStartIdx;
                    int nCurPixelStopIdx = (int)(((float)(CurrentPixelX - FirstXPixel) / PixelXStep + 1) * nSamplePerPixel-1);
                    double nData = 0;
                    for (int j = nSampleStartIdx; j < nSampleNo; j++)
                    {
                        //read data from scan data object
                        var nTemp = ScanData.Get(DrawLineIdx * LineSampleCount + j, nSigIdx, 0);
                        if (!double.IsNaN(nTemp))
                            nData += nTemp;
                        if ((j== nCurPixelStopIdx)|| (j== LineSampleCount-1))
                        {
                            nData /= nCurPixelStopIdx - nCurPixelStartIdx + 1;
                            var oColor = getHeatMapColor((float)((nData - SigMin) / (SigMax - SigMin)));
                            oBr.Color = oColor;
                            g.FillRectangle(oBr, new Rectangle(CurrentPixelX, CurrentPixelY, PixelXStep, PixelYStep));
                            nData = 0;
                            CurrentPixelX += PixelXStep;
                            nCurPixelStartIdx = nCurPixelStopIdx + 1;
                            nCurPixelStopIdx = (int)(((CurrentPixelX - FirstXPixel) / PixelXStep + 1) * nSamplePerPixel - 1);
                        }
                    }
                    if (DrawLineIdx != ScanLineIdx)
                    {
                        CurrentPixelY += PixelYStep;
                        CurrentPixelX = FirstXPixel;
                        DrawLineIdx++;
                    }
                    else
                    {
                        DrawSampleIdx = nCurPixelStartIdx;
                        if (DrawSampleIdx >= LineSampleCount)
                        {
                            DrawSampleIdx = 0;
                            DrawLineIdx++;
                        }
                        break;
                    }
                    
                }
            }
            PPaint.Invalidate();
        }

        private void CleanDataBitmap()
        {
            using (Graphics g = Graphics.FromImage(bm))
            using (SolidBrush oBr = new SolidBrush(Color.Black))
            {
                g.FillRectangle(oBr, new Rectangle(0, 0, bm.Width, bm.Height));
            }
        }


        private Color getHeatMapColor(float value)
        {
            const int NUM_COLORS = 4;
            // A static array of 4 colors:  (blue,   green,  yellow,  red) using {r,g,b} for each.
            float[,] color = new float[,] { { 0, 0, 255 }, { 0, 255, 0 }, { 255, 255, 0 }, { 255, 0, 0 } };


            int idx1;        
            int idx2;        
            float fractBetween = 0;  

            if (value <= 0) { idx1 = idx2 = 0; }   
            else if (value >= 1) { idx1 = idx2 = NUM_COLORS - 1; }    
            else
            {
                value = value * (NUM_COLORS - 1);        
                idx1 = (int)(Math.Floor(value));       
                idx2 = idx1 + 1;                      
                fractBetween = value - idx1;   
            }
            int red = (int)((color[idx2, 0] - color[idx1, 0]) * fractBetween + color[idx1, 0]);
            int green = (int)((color[idx2, 1] - color[idx1, 1]) * fractBetween + color[idx1, 1]);
            int blue = (int)((color[idx2, 2] - color[idx1, 2]) * fractBetween + color[idx1, 2]);
            return Color.FromArgb(red, green, blue);
        }
    }
}
