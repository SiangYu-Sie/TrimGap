using System;
using System.Drawing;
using System.Windows.Forms;

namespace TrimGap
{
    /// <summary>
    /// 模擬量測流程 控制面板
    /// 用於操作 SimulateMeasureFlow，設定參數並監看執行進度
    /// </summary>
    public partial class SimulateForm : Form
    {
        // ── Step Label 陣列（從 Designer 個別 Label 組合） ──
        private Label[] lbSteps;

        // ── Timer ──
        private System.Windows.Forms.Timer timerRefresh;

        // Step 名稱對應
        private static readonly string[] StepNames = new string[]
        {
            "Step 0: 初始化 (ReadyToLoad)",
            "Step 1: Carrier 放上 LoadPort",
            "Step 2: Clamp + CreateCarrier + ReadID",
            "Step 3: 等待 ProceedWithCarrier #1",
            "Step 4: FoupLoad (Dock/DoorOpen)",
            "Step 5: SlotMap",
            "Step 6: 等待 ProceedWithCarrier #2",
            "Step 7: 等待 CJ/PJ 建立",
            "Step 8: CJ/PJ Start",
            "Step 9: 逐片量測中...",
            "Step 10: PJ/CJ Complete",
            "Step 11: FoupUnLoad (DoorClose)",
            "Step 12: Carrier 拿走 (MaterialRemove)",
            "完成"
        };

        public SimulateForm()
        {
            InitializeComponent();

            // 將 Designer 中個別的 Step Label 組成陣列，方便迴圈操作
            lbSteps = new Label[]
            {
                lbStep0, lbStep1, lbStep2, lbStep3, lbStep4,
                lbStep5, lbStep6, lbStep7, lbStep8, lbStep9,
                lbStep10, lbStep11, lbStep12, lbStepDone
            };

            // 設定 ComboBox 預設選項
            cmbLoadPort.SelectedIndex = 0;

            // 啟動 Timer 定時刷新進度
            timerRefresh = new System.Windows.Forms.Timer();
            timerRefresh.Interval = 200;
            timerRefresh.Tick += TimerRefresh_Tick;
            timerRefresh.Start();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            timerRefresh.Stop();
            timerRefresh.Dispose();
            base.OnFormClosed(e);
        }

        // ============================================================
        //  Timer Tick — 刷新進度
        // ============================================================
        private void TimerRefresh_Tick(object sender, EventArgs e)
        {
            RefreshProgress();
        }

        private void RefreshProgress()
        {
            bool running = SimulateMeasureFlow.IsRunning;
            var step = SimulateMeasureFlow.CurrentStep;
            int stepIdx = (int)step;

            // 更新按鈕狀態
            btnStart.Enabled = !running;
            btnStart.Text = running ? "執行中..." : "▶ 開始模擬";
            btnStart.BackColor = running ? Color.FromArgb(80, 80, 80) : Color.FromArgb(0, 120, 215);

            // 輸入區域鎖定
            cmbLoadPort.Enabled = !running;
            txtCarrierID.Enabled = !running;
            nudSlotCount.Enabled = !running;
            nudMeasureDelay.Enabled = !running;

            // ProgressBar
            pgBar.Maximum = StepNames.Length - 1;
            pgBar.Value = Math.Min(stepIdx, pgBar.Maximum);

            // 更新每個 Step Label 的顏色
            for (int i = 0; i < lbSteps.Length; i++)
            {
                if (i < stepIdx)
                {
                    // 已完成
                    lbSteps[i].ForeColor = Color.FromArgb(100, 200, 100);
                    lbSteps[i].Text = "  ✔ " + StepNames[i];
                }
                else if (i == stepIdx && running)
                {
                    // 執行中
                    lbSteps[i].ForeColor = Color.FromArgb(255, 220, 80);
                    lbSteps[i].Text = "  ▶ " + StepNames[i];
                }
                else if (i == stepIdx && !running && step == SimulateMeasureFlow.SimStep.Done)
                {
                    // Done
                    lbSteps[i].ForeColor = Color.FromArgb(100, 200, 100);
                    lbSteps[i].Text = "  ✔ " + StepNames[i];
                }
                else
                {
                    // 尚未執行
                    lbSteps[i].ForeColor = Color.FromArgb(120, 120, 120);
                    lbSteps[i].Text = "  ○ " + StepNames[i];
                }
            }

            // 目前狀態摘要
            if (!running && step == SimulateMeasureFlow.SimStep.Done)
            {
                lbCurrentInfo.Text = "✔ 模擬流程已完成";
                lbCurrentInfo.ForeColor = Color.LimeGreen;
            }
            else if (running)
            {
                string stepName = (stepIdx >= 0 && stepIdx < StepNames.Length) ? StepNames[stepIdx] : "---";
                lbCurrentInfo.Text = "執行中: " + stepName;
                lbCurrentInfo.ForeColor = Color.FromArgb(255, 220, 80);
            }
            else
            {
                lbCurrentInfo.Text = "等待啟動...";
                lbCurrentInfo.ForeColor = Color.Gray;
            }
        }

        // ============================================================
        //  Button Handler
        // ============================================================
        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (SimulateMeasureFlow.IsRunning)
            {
                MessageBox.Show("模擬流程正在執行中，請等待完成。", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int loadPort = cmbLoadPort.SelectedIndex + 1; // 0→1, 1→2
            string carrierID = txtCarrierID.Text.Trim();
            int slotCount = (int)nudSlotCount.Value;
            int measureDelay = (int)nudMeasureDelay.Value;

            if (string.IsNullOrEmpty(carrierID))
            {
                MessageBox.Show("請輸入 Carrier ID。", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCarrierID.Focus();
                return;
            }

            // 重置 step labels
            for (int i = 0; i < lbSteps.Length; i++)
            {
                lbSteps[i].ForeColor = Color.FromArgb(120, 120, 120);
                lbSteps[i].Text = "  ○ " + StepNames[i];
            }

            SimulateMeasureFlow.Start(loadPort, carrierID, slotCount, measureDelay);
        }
    }
}
