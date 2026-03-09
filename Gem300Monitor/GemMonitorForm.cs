using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gem300Monitor
{
    public partial class GemMonitorForm : Form
    {
        private RichTextBox rtbLog;
        private Button btnClear;
        private Button btnSimulateTx;
        private Button btnSimulateRxError;
        private Label lblStatus;

        public GemMonitorForm()
        {
            InitializeComponent();
            InitializeCustomUI();
        }

        private void InitializeCustomUI()
        {
            this.Text = "GEM300 雙向溝通監看系統";
            this.Size = new Size(950, 650);
            this.StartPosition = FormStartPosition.CenterScreen;

            var topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(45, 45, 48)
            };

            lblStatus = new Label
            {
                Text = "系統狀態: 監聽中...",
                ForeColor = Color.White,
                Font = new Font("微軟正黑體", 12F, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            topPanel.Controls.Add(lblStatus);

            btnSimulateTx = new Button
            {
                Text = "模擬傳送成功(TX)",
                Location = new Point(480, 15),
                Size = new Size(130, 35),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("微軟正黑體", 9F)
            };
            btnSimulateTx.FlatAppearance.BorderSize = 0;
            btnSimulateTx.Click += BtnSimulateTx_Click;
            topPanel.Controls.Add(btnSimulateTx);

            btnSimulateRxError = new Button
            {
                Text = "模擬接收錯誤(RX)",
                Location = new Point(620, 15),
                Size = new Size(130, 35),
                BackColor = Color.FromArgb(204, 74, 74),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("微軟正黑體", 9F)
            };
            btnSimulateRxError.FlatAppearance.BorderSize = 0;
            btnSimulateRxError.Click += BtnSimulateRxError_Click;
            topPanel.Controls.Add(btnSimulateRxError);

            btnClear = new Button
            {
                Text = "清除日誌",
                Location = new Point(760, 15),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(85, 85, 85),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("微軟正黑體", 9F)
            };
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Click += (s, e) => rtbLog.Clear();
            topPanel.Controls.Add(btnClear);

            rtbLog = new RichTextBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                Font = new Font("Consolas", 11F),
                ReadOnly = true,
                Margin = new Padding(10)
            };

            this.Controls.Add(rtbLog);
            this.Controls.Add(topPanel);
        }

        private void BtnSimulateTx_Click(object sender, EventArgs e)
        {
            LogCommunication("Local(EQP)", "Remote(HOST)", "S1F1 (Are You There Request)", true, "");
            LogCommunication("Remote(HOST)", "Local(EQP)", "S1F2 (Are You There Data)", true, "Online");
        }

        private void BtnSimulateRxError_Click(object sender, EventArgs e)
        {
            LogCommunication("Remote(HOST)", "Local(EQP)", "S2F41 (Host Command Send)", false, "參數錯誤 (Format Error) - CPVAL 型態不符");
            LogCommunication("Local(EQP)", "Remote(HOST)", "S9F7 (Illegal Data)", false, "發生於 S2F41, 錯誤節點: <L <A 'HCACK'> <A 'CPNAME'> <U4 123>> => 預期應為 Ascii(A) 但收到 U4");
        }

        /// <summary>
        /// 記錄通訊的函式
        /// </summary>
        /// <param name="source">來源端 (例如: Local 或是 Remote)</param>
        /// <param name="target">目標端</param>
        /// <param name="command">SECS指令 (例如: S1F1)</param>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="details">詳細內容或錯誤資訊</param>
        public void LogCommunication(string source, string target, string command, bool isSuccess, string details)
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.Invoke(new Action(() => LogCommunication(source, target, command, isSuccess, details)));
                return;
            }

            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string direction = $"{source} -> {target}";
            
            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.SelectionLength = 0;

            // 1. 寫入時間
            rtbLog.SelectionColor = Color.LightGray;
            rtbLog.AppendText($"[{timeStamp}] ");

            // 2. 寫入方向
            rtbLog.SelectionColor = Color.Cyan;
            rtbLog.AppendText($"{direction,-28} | ");

            // 3. 寫入狀態 (成功/錯誤)
            if (isSuccess)
            {
                rtbLog.SelectionColor = Color.LimeGreen;
                rtbLog.AppendText("[SUCCESS] ");
            }
            else
            {
                rtbLog.SelectionColor = Color.Tomato;
                rtbLog.AppendText("[ERROR]   ");
            }

            // 4. 寫入指令
            rtbLog.SelectionColor = Color.White;
            rtbLog.AppendText($"{command,-25}");

            // 5. 寫入詳細資訊 (如果是錯誤，標示出錯位置)
            if (!string.IsNullOrEmpty(details))
            {
                rtbLog.AppendText(" | ");
                if (!isSuccess)
                {
                    rtbLog.SelectionColor = Color.Yellow;
                    rtbLog.AppendText($"錯誤點: {details}");
                }
                else
                {
                    rtbLog.SelectionColor = Color.DarkGray;
                    rtbLog.AppendText(details);
                }
            }

            rtbLog.AppendText(Environment.NewLine);
            
            // 捲動到最底
            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.ScrollToCaret();
        }
    }
}
