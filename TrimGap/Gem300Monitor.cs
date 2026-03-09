using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrimGap
{
    // ── 訊息方向 ──
    public enum Gem300Dir
    {
        Receive,  // ↓ Host → EQ
        Send,     // ↑ EQ → Host
        State,    //    狀態變更
        Alarm,    // ! 警報
        Info      //   資訊
    }

    // ── 訊息類別 ──
    public enum Gem300Cat
    {
        Command,
        Event,
        State,
        Alarm,
        Info
    }

    // ── 訊息資料 ──
    public class Gem300MessageItem
    {
        public DateTime Time     { get; set; }
        public Gem300Dir Dir     { get; set; }
        public Gem300Cat Cat     { get; set; }
        public string Message    { get; set; }
        public Color BackColor   { get; set; }

        public string DirLabel
        {
            get
            {
                switch (Dir)
                {
                    case Gem300Dir.Receive: return "↓ RCV";
                    case Gem300Dir.Send:    return "↑ SND";
                    case Gem300Dir.State:   return "  STS";
                    case Gem300Dir.Alarm:   return "! ALM";
                    default:               return "  INF";
                }
            }
        }

        public string CatLabel
        {
            get
            {
                switch (Cat)
                {
                    case Gem300Cat.Command: return "Command ";
                    case Gem300Cat.Event:   return "Event   ";
                    case Gem300Cat.State:   return "State   ";
                    case Gem300Cat.Alarm:   return "Alarm   ";
                    default:               return "Info    ";
                }
            }
        }
    }

    /// <summary>
    /// GEM300 SECS 通訊監看視窗
    /// 監看 Remote ↔ Local 之間的所有訊息、事件及狀態變化
    /// </summary>
    public partial class Gem300Monitor : Form
    {
        // ── 靜態訊息緩衝區（任何類別都可呼叫） ──
        private static readonly object _lock = new object();
        private static readonly List<Gem300MessageItem> _buffer = new List<Gem300MessageItem>();
        private const int MaxBuffer = 2000;

        // ── 渲染追蹤 ──
        private int _lastRendered = 0;
        private bool _autoScroll  = true;

        // ── UI 控制項 ──
        private RichTextBox  rtbLog;
        private Panel        pnlToolbar;
        private Panel        pnlStatus;
        private Label        lbConnSts;
        private Button       btnClear;
        private Button       btnAutoScroll;
        private CheckBox     chkCmd;
        private CheckBox     chkEvt;
        private CheckBox     chkSts;
        private CheckBox     chkAlm;
        private System.Windows.Forms.Timer timerRefresh;

        // 狀態 Labels
        private Label lbLP1, lbLP2, lbRobot, lbAligner, lbStage, lbCJ, lbPJ, lbMode;

        public Gem300Monitor()
        {
            Text            = "GEM300 Monitor";
            Size            = new Size(1100, 700);
            MinimumSize     = new Size(800, 500);
            StartPosition   = FormStartPosition.CenterScreen;
            BackColor       = Color.FromArgb(30, 30, 30);
            Font            = new Font("Consolas", 9f);

            BuildUI();

            timerRefresh          = new System.Windows.Forms.Timer();
            timerRefresh.Interval = 250; // 250ms 刷新一次
            timerRefresh.Tick    += TimerRefresh_Tick;
            timerRefresh.Start();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            timerRefresh.Stop();
            timerRefresh.Dispose();
            base.OnFormClosed(e);
        }

        // ============================================================
        //  Public static API — 在任何地方呼叫這些方法來記錄訊息
        // ============================================================

        /// <summary>收到 Host 下來的指令（Command）</summary>
        public static void AddReceive(string message)
            => Enqueue(Gem300Dir.Receive, Gem300Cat.Command, message,
                       Color.FromArgb(255, 255, 200));       // 淡黃

        /// <summary>送出事件給 Host（Event）</summary>
        public static void AddSend(string message)
            => Enqueue(Gem300Dir.Send, Gem300Cat.Event, message,
                       Color.FromArgb(200, 255, 200));        // 淡綠

        /// <summary>狀態變更（State change）</summary>
        public static void AddState(string message)
            => Enqueue(Gem300Dir.State, Gem300Cat.State, message,
                       Color.FromArgb(200, 230, 255));        // 淡藍

        /// <summary>警報 / 錯誤</summary>
        public static void AddAlarm(string message)
            => Enqueue(Gem300Dir.Alarm, Gem300Cat.Alarm, message,
                       Color.FromArgb(255, 180, 180));        // 淡紅

        /// <summary>一般資訊</summary>
        public static void AddInfo(string message)
            => Enqueue(Gem300Dir.Info, Gem300Cat.Info, message,
                       Color.FromArgb(210, 210, 210));        // 淡灰

        // ============================================================
        //  私有 Enqueue
        // ============================================================
        private static void Enqueue(Gem300Dir dir, Gem300Cat cat, string msg, Color backColor)
        {
            lock (_lock)
            {
                if (_buffer.Count >= MaxBuffer)
                    _buffer.RemoveAt(0);

                _buffer.Add(new Gem300MessageItem
                {
                    Time      = DateTime.Now,
                    Dir       = dir,
                    Cat       = cat,
                    Message   = msg,
                    BackColor = backColor
                });
            }
        }

        // ============================================================
        //  Timer tick
        // ============================================================
        private void TimerRefresh_Tick(object sender, EventArgs e)
        {
            RefreshLog();
            RefreshStatus();
        }

        private void RefreshLog()
        {
            lock (_lock)
            {
                int total = _buffer.Count;
                if (total == _lastRendered) return;

                rtbLog.SuspendLayout();
                for (int i = _lastRendered; i < total; i++)
                {
                    var m = _buffer[i];
                    if (!ShouldShow(m)) continue;

                    string line = string.Format("[{0:HH:mm:ss.fff}] {1} │ {2}│ {3}\n",
                        m.Time, m.DirLabel, m.CatLabel, m.Message);

                    rtbLog.SelectionStart  = rtbLog.TextLength;
                    rtbLog.SelectionLength = 0;
                    rtbLog.SelectionBackColor = m.BackColor;
                    rtbLog.SelectionColor     = Color.Black;
                    rtbLog.AppendText(line);
                }
                _lastRendered = total;
                rtbLog.ResumeLayout();

                if (_autoScroll)
                    rtbLog.ScrollToCaret();
            }
        }

        private bool ShouldShow(Gem300MessageItem m)
        {
            switch (m.Cat)
            {
                case Gem300Cat.Command: return chkCmd.Checked;
                case Gem300Cat.Event:   return chkEvt.Checked;
                case Gem300Cat.State:   return chkSts.Checked;
                case Gem300Cat.Alarm:   return chkAlm.Checked;
                default:               return true;
            }
        }

        private void RefreshStatus()
        {
            try
            {
                // SECS 連線狀態
                bool communicating = Common.SecsgemForm.isCommunicating();
                bool remote        = Common.SecsgemForm.isRemote();

                lbConnSts.Text      = communicating ? (remote ? "● REMOTE" : "● LOCAL") : "● OFFLINE";
                lbConnSts.ForeColor = communicating ? (remote ? Color.Lime : Color.Yellow) : Color.Red;

                // LP1
                lbLP1.Text = string.Format("LP1: {0,-12} Placement:{1}  FoupID:{2}",
                    ((PortTransferState)fram.SECSPara.Loadport1_PortTransferState).ToString(),
                    Common.EFEM.LoadPort1.Placement ? "Y" : "N",
                    Common.EFEM.LoadPort1.FoupID ?? "-");

                // LP2
                lbLP2.Text = string.Format("LP2: {0,-12} Placement:{1}  FoupID:{2}",
                    ((PortTransferState)fram.SECSPara.Loadport2_PortTransferState).ToString(),
                    Common.EFEM.LoadPort2.Placement ? "Y" : "N",
                    Common.EFEM.LoadPort2.FoupID ?? "-");

                // Robot
                lbRobot.Text = string.Format("Robot: Upper={0} Slot:{1}  Lower={2} Slot:{3}",
                    Common.EFEM.Robot.WaferPresence_Upper ? "●" : "○",
                    Common.EFEM.Robot.Slot_Arm_upper,
                    Common.EFEM.Robot.WaferPresence_Lower ? "●" : "○",
                    Common.EFEM.Robot.Slot_Arm_lower);

                // Aligner
                lbAligner.Text = string.Format("Aligner: {0}  Slot:{1}  AlignDone:{2}",
                    Common.EFEM.Aligner.WaferPresence ? "●" : "○",
                    Common.EFEM.Aligner.Slot,
                    Common.EFEM.Aligner.Alignement_Done ? "Y" : "N");

                // Stage
                lbStage.Text = string.Format("Stage:   {0}  Slot:{1}  Ready:{2}  MeasDone:{3}",
                    Common.EFEM.Stage1.WaferPresence ? "●" : "○",
                    Common.EFEM.Stage1.Slot,
                    Common.EFEM.Stage1.Ready ? "Y" : "N",
                    Common.EFEM.Stage1.Measuredone ? "Y" : "N");

                // CJ / PJ
                lbCJ.Text = string.Format("CJ: {0}   QueuePJ:{1}",
                    string.IsNullOrEmpty(sram.RunningCJ) ? "-" : sram.RunningCJ,
                    sram.QueuePJ != null ? sram.QueuePJ.Count.ToString() : "0");

                lbPJ.Text = string.Format("PJ: {0}",
                    string.IsNullOrEmpty(sram.RunningPJ) ? "-" : sram.RunningPJ);

                // Flags
                lbMode.Text = string.Format(
                    "AutoIdle:{0}  Abort:{1}  Pause:{2}  Alarm:{3}  HomeOK:{4}",
                    Flag.AutoidleFlag   ? "Y" : "N",
                    Flag.AbortFlag      ? "Y" : "N",
                    Flag.PauseFlag      ? "Y" : "N",
                    Flag.AlarmFlag      ? "Y" : "N",
                    Flag.AllHomeFlag    ? "Y" : "N");
            }
            catch { /* 防止 UI 更新時的例外中斷 */ }
        }

        // ============================================================
        //  Button handlers
        // ============================================================
        private void BtnClear_Click(object sender, EventArgs e)
        {
            lock (_lock)
            {
                _buffer.Clear();
                _lastRendered = 0;
            }
            rtbLog.Clear();
        }

        private void BtnAutoScroll_Click(object sender, EventArgs e)
        {
            _autoScroll              = !_autoScroll;
            btnAutoScroll.BackColor  = _autoScroll ? Color.LimeGreen : Color.DimGray;
            btnAutoScroll.Text       = _autoScroll ? "AutoScroll ON" : "AutoScroll OFF";
        }

        private void ChkFilter_CheckedChanged(object sender, EventArgs e)
        {
            // 重新渲染全部 log
            lock (_lock) { _lastRendered = 0; }
            rtbLog.Clear();
        }

        // ============================================================
        //  UI Builder
        // ============================================================
        private void BuildUI()
        {
            // ── Toolbar ──
            pnlToolbar              = new Panel();
            pnlToolbar.Dock         = DockStyle.Top;
            pnlToolbar.Height       = 36;
            pnlToolbar.BackColor    = Color.FromArgb(45, 45, 45);

            lbConnSts               = new Label();
            lbConnSts.AutoSize      = true;
            lbConnSts.Font          = new Font("Consolas", 10f, FontStyle.Bold);
            lbConnSts.ForeColor     = Color.Gray;
            lbConnSts.Text          = "● OFFLINE";
            lbConnSts.Location      = new Point(8, 9);

            btnClear                = new Button();
            btnClear.Text           = "Clear";
            btnClear.Size           = new Size(70, 26);
            btnClear.Location       = new Point(140, 5);
            btnClear.BackColor      = Color.FromArgb(80, 80, 80);
            btnClear.ForeColor      = Color.White;
            btnClear.FlatStyle      = FlatStyle.Flat;
            btnClear.Click         += BtnClear_Click;

            btnAutoScroll           = new Button();
            btnAutoScroll.Text      = "AutoScroll ON";
            btnAutoScroll.Size      = new Size(110, 26);
            btnAutoScroll.Location  = new Point(220, 5);
            btnAutoScroll.BackColor = Color.LimeGreen;
            btnAutoScroll.ForeColor = Color.Black;
            btnAutoScroll.FlatStyle = FlatStyle.Flat;
            btnAutoScroll.Click    += BtnAutoScroll_Click;

            chkCmd = CreateCheckBox("↓ Command", 345, Color.FromArgb(255, 255, 200));
            chkEvt = CreateCheckBox("↑ Event",   465, Color.FromArgb(200, 255, 200));
            chkSts = CreateCheckBox("  State",   565, Color.FromArgb(200, 230, 255));
            chkAlm = CreateCheckBox("! Alarm",   655, Color.FromArgb(255, 180, 180));

            chkCmd.CheckedChanged += ChkFilter_CheckedChanged;
            chkEvt.CheckedChanged += ChkFilter_CheckedChanged;
            chkSts.CheckedChanged += ChkFilter_CheckedChanged;
            chkAlm.CheckedChanged += ChkFilter_CheckedChanged;

            pnlToolbar.Controls.AddRange(new Control[]
                { lbConnSts, btnClear, btnAutoScroll, chkCmd, chkEvt, chkSts, chkAlm });

            // ── Status panel ──
            pnlStatus            = new Panel();
            pnlStatus.Dock       = DockStyle.Bottom;
            pnlStatus.Height     = 160;
            pnlStatus.BackColor  = Color.FromArgb(40, 40, 40);
            pnlStatus.Padding    = new Padding(6);

            lbLP1    = BuildStatusLabel(0);
            lbLP2    = BuildStatusLabel(1);
            lbRobot  = BuildStatusLabel(2);
            lbAligner= BuildStatusLabel(3);
            lbStage  = BuildStatusLabel(4);
            lbCJ     = BuildStatusLabel(5);
            lbPJ     = BuildStatusLabel(6);
            lbMode   = BuildStatusLabel(7);

            pnlStatus.Controls.AddRange(new Control[]
                { lbLP1, lbLP2, lbRobot, lbAligner, lbStage, lbCJ, lbPJ, lbMode });

            // ── Log RichTextBox ──
            rtbLog                 = new RichTextBox();
            rtbLog.Dock            = DockStyle.Fill;
            rtbLog.BackColor       = Color.FromArgb(20, 20, 20);
            rtbLog.ForeColor       = Color.White;
            rtbLog.Font            = new Font("Consolas", 9f);
            rtbLog.ReadOnly        = true;
            rtbLog.ScrollBars      = RichTextBoxScrollBars.Both;
            rtbLog.WordWrap        = false;
            rtbLog.BorderStyle     = BorderStyle.None;

            Controls.Add(rtbLog);
            Controls.Add(pnlStatus);
            Controls.Add(pnlToolbar);
        }

        private CheckBox CreateCheckBox(string text, int x, Color backColor)
        {
            var cb           = new CheckBox();
            cb.Text          = text;
            cb.Checked       = true;
            cb.ForeColor     = Color.White;
            cb.BackColor     = Color.Transparent;
            cb.AutoSize      = true;
            cb.Location      = new Point(x, 9);
            return cb;
        }

        private Label BuildStatusLabel(int index)
        {
            var lb         = new Label();
            lb.AutoSize    = false;
            lb.Width       = 700;
            lb.Height      = 18;
            lb.Location    = new Point(6, 4 + index * 19);
            lb.ForeColor   = Color.FromArgb(180, 220, 255);
            lb.Font        = new Font("Consolas", 8.5f);
            lb.Text        = "-";
            return lb;
        }

        // WinForms required stub
        private void InitializeComponent()
        {
            SuspendLayout();
            ResumeLayout(false);
        }
    }
}