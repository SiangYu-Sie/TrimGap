using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TrimGap
{
    /// <summary>
    /// E94/E40 標準流程：先建立 PJ，再建立 CJ
    /// CJ 必須包含已存在的 PJ
    /// 
    /// 關鍵注意事項：
    /// 1. 作業關聯性限制 - PJ 必須先存在，CJ 才能建立
    /// 2. 唯一性與空間限制 - ID 必須唯一，需檢查隊列空間
    /// 3. 修改作業的時機 - CJ 在 EXECUTING/COMPLETED 狀態下不可修改
    /// 4. 異常處理與材料風險 - 支援 Abort/Stop/Cancel 命令
    /// 5. 材料追蹤一致性 - 確保 ID 映射一致
    /// </summary>
    public partial class CreatPjCJ : Form
    {
        #region =================== Fields =========================
        public static DemoFormDiaGemLib.MainForm MainForm;

        private List<PJInfo> listPJ = new List<PJInfo>();
        private List<CJInfo> listCJ = new List<CJInfo>();
        private List<RecipeInfo> listRecipe = new List<RecipeInfo>();
        private RecipeFormat currentRecipe = new RecipeFormat();
        private string selectedRecipeId = string.Empty;
        private List<string> selectedPJsForCJ = new List<string>();

        // BindingSource 用於正確管理 DataGridView 的 CurrencyManager
        private BindingSource bsPJ = new BindingSource();
        private BindingSource bsRecipe = new BindingSource();
        private BindingSource bsCJ = new BindingSource();

        // 隊列空間限制常數
        private const int MAX_PJ_QUEUE_SIZE = 100;
        private const int MAX_CJ_QUEUE_SIZE = 50;
        #endregion

        #region =================== Classes (E40/E94) =========================

        /// <summary>
        /// 製程作業 (Process Job) - SEMI E40
        /// </summary>
        public class PJInfo
        {
            /// <summary>PJ 的唯一識別碼</summary>
            public string PRJobID { get; set; }

            /// <summary>物件類型</summary>
            public string ObjType { get; set; } = "ProcessJob";

            /// <summary>暫停事件</summary>
            public string PauseEvent { get; set; }

            /// <summary>PJ 狀態</summary>
            public PRJobStateEnum PRJobState { get; set; } = PRJobStateEnum.QUEUED;

            /// <summary>待處理的材料清單（晶圓 ID）</summary>
            public List<PRMtlNameItem> PRMtlNameList { get; set; } = new List<PRMtlNameItem>();

            /// <summary>材料類型 (14=Substrate, 其他=Carriers)</summary>
            public byte PRMtlType { get; set; } = 14;

            /// <summary>自動啟動 (TRUE) 或手動啟動 (FALSE)</summary>
            public bool PRProcessStart { get; set; }

            /// <summary>Recipe方法 (0=Recipe only, 1=Recipe with Variable Tuning)</summary>
            public byte PRRecipeMethod { get; set; }

            /// <summary>指定要使用的Recipe (Recipe)</summary>
            public string RecID { get; set; }

            /// <summary>Recipe變數清單</summary>
            public string RecVariableList { get; set; }

            /// <summary>載具 ID</summary>
            public string CarrierID { get; set; }

            /// <summary>Slot 陣列 (25 slots)</summary>
            public byte[] Slots { get; set; } = new byte[25];

            /// <summary>建立時間</summary>
            public DateTime CreateTime { get; set; } = DateTime.Now;

            /// <summary>是否已被 CJ 關聯 (E94 模式下 PJ 處於 Pooled 狀態)</summary>
            public bool IsPooled { get; set; } = false;

            /// <summary>關聯的 CJ ID</summary>
            public string LinkedCJID { get; set; }
        }

        /// <summary>
        /// 材料名稱項目
        /// </summary>
        public class PRMtlNameItem
        {
            public string CarrierID { get; set; }
            public string SlotID { get; set; }
        }

        /// <summary>
        /// 控制作業 (Control Job) - SEMI E94
        /// </summary>
        public class CJInfo
        {
            /// <summary>CJ 的唯一識別碼</summary>
            public string ObjID { get; set; }

            /// <summary>物件類型</summary>
            public string ObjType { get; set; } = "ControlJob";

            /// <summary>目前執行的 PJ 清單</summary>
            public List<string> CurrentPRJob { get; set; } = new List<string>();

            /// <summary>資料收集計畫</summary>
            public string DataCollectionPlan { get; set; }

            /// <summary>來源載具 ID 清單</summary>
            public List<string> CarrierInputSpec { get; set; } = new List<string>();

            /// <summary>處理完成後材料目的地</summary>
            public List<string> MtrlOutSpec { get; set; } = new List<string>();

            /// <summary>依狀態輸出材料</summary>
            public List<string> MtrlOutByStatus { get; set; } = new List<string>();

            /// <summary>暫停事件清單</summary>
            public List<string> PauseEvent { get; set; } = new List<string>();

            /// <summary>CJ 要執行的 PRJobID 及其規則</summary>
            public List<string> ProcessingCtrlSpec { get; set; } = new List<string>();

            /// <summary>PJ 啟動順序</summary>
            public ProcessOrderMgmtEnum ProcessOrderMgmt { get; set; } = ProcessOrderMgmtEnum.LIST;

            /// <summary>啟動方法</summary>
            public bool StartMethod { get; set; }

            /// <summary>CJ 狀態</summary>
            public CJStateEnum State { get; set; } = CJStateEnum.QUEUED;

            /// <summary>建立時間</summary>
            public DateTime CreateTime { get; set; } = DateTime.Now;

            /// <summary>檢查 CJ 是否可修改 (注意事項 3: 狀態限制)</summary>
            public bool IsModifiable
            {
                get
                {
                    return State != CJStateEnum.EXECUTING && State != CJStateEnum.COMPLETED;
                }
            }
        }

        /// <summary>
        /// Recipe資訊
        /// </summary>
        public class RecipeInfo
        {
            public string RecipeName { get; set; }
            public string FullPath { get; set; }
        }

        /// <summary>
        /// 驗證結果
        /// </summary>
        public class ValidationResult
        {
            public bool IsValid { get; set; }
            public string ErrorMessage { get; set; }
            public List<string> Warnings { get; set; } = new List<string>();

            public static ValidationResult Success() => new ValidationResult { IsValid = true };
            public static ValidationResult Fail(string message) => new ValidationResult { IsValid = false, ErrorMessage = message };
        }

        #endregion

        #region =================== Enums =========================

        /// <summary>
        /// PJ 狀態列舉 - SEMI E40
        /// </summary>
        public enum PRJobStateEnum
        {
            QUEUED = 0,
            POOLED = 1,
            SETTING_UP = 2,
            WAITING_FOR_START = 3,
            PROCESSING = 4,
            PROCESS_COMPLETED = 5,
            EXECUTING = 6,
            PAUSING = 7,
            PAUSED = 8,
            STOPPING = 9,
            ABORTING = 10
        }

        /// <summary>
        /// CJ 狀態列舉 - SEMI E94
        /// </summary>
        public enum CJStateEnum
        {
            QUEUED = 0,
            SELECTED = 1,
            WAITING_FOR_START = 2,
            EXECUTING = 3,
            PAUSED = 4,
            COMPLETED = 5
        }

        /// <summary>
        /// PJ 啟動順序
        /// </summary>
        public enum ProcessOrderMgmtEnum
        {
            /// <summary>按清單順序</summary>
            LIST = 0,
            /// <summary>按抵達順序</summary>
            ARRIVAL = 1,
            /// <summary>優化順序</summary>
            OPTIMIZE = 2
        }

        /// <summary>
        /// 作業命令類型 (注意事項 4: 異常處理)
        /// </summary>
        public enum JobCommandEnum
        {
            /// <summary>取消 - 僅適用於 QUEUED 狀態的作業</summary>
            Cancel,
            /// <summary>停止 - 有序停止，保護材料完整性</summary>
            Stop,
            /// <summary>中止 - 立即停止，材料可能處於未知狀態</summary>
            Abort
        }

        #endregion

        #region =================== Constructor =========================

        public CreatPjCJ()
        {
            InitializeComponent();
            MainForm = SecsGemInterface.MainForm;
        }

        #endregion

        #region =================== Initialization =========================

        private void CreatPjCJ_Load(object sender, EventArgs e)
        {
            InitializeRecipeList();
            RefreshPJList();
            RefreshCJList();
            UpdateUIState();
            UpdateQueueSpaceInfo();
            
            // 預設選擇 Substrate
            rbSubstrate.Checked = true;
        }

        /// <summary>
        /// 初始化Recipe清單
        /// </summary>
        private void InitializeRecipeList()
        {
            listRecipe.Clear();

            string recipePath = fram.Recipe?.Path;
            if (string.IsNullOrWhiteSpace(recipePath) || !Directory.Exists(recipePath))
            {
                ShowMessage($"Recipe 路徑不存在: {recipePath}");
                return;
            }

            foreach (string filePath in Directory.GetFiles(recipePath, "*.ini"))
            {
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                listRecipe.Add(new RecipeInfo
                {
                    RecipeName = fileName,
                    FullPath = filePath
                });
            }

            bsRecipe.DataSource = null;
            bsRecipe.DataSource = listRecipe;
            dgvRecipe.DataSource = bsRecipe;
            dgvRecipe.ClearSelection();
        }

        /// <summary>
        /// 刷新 PJ 清單
        /// </summary>
        private void RefreshPJList()
        {
            listPJ.Clear();

            if (DemoFormDiaGemLib.MainForm.PJ_list != null)
            {
                foreach (var pjId in DemoFormDiaGemLib.MainForm.PJ_list)
                {
                    if (!string.IsNullOrEmpty(pjId))
                    {
                        var pjInfo = GetPJInfoFromMainForm(pjId);
                        if (pjInfo != null)
                        {
                            listPJ.Add(pjInfo);
                        }
                    }
                }
            }

            bsPJ.DataSource = null;
            bsPJ.DataSource = listPJ;
            dgvPJ.DataSource = bsPJ;
            dgvPJ.ClearSelection();
            UpdatePJGridColumns();
        }

        /// <summary>
        /// 刷新 CJ 清單
        /// </summary>
        private void RefreshCJList()
        {
            listCJ.Clear();

            if (DemoFormDiaGemLib.MainForm.CJ_list != null)
            {
                foreach (var cjId in DemoFormDiaGemLib.MainForm.CJ_list)
                {
                    if (!string.IsNullOrEmpty(cjId))
                    {
                        var cjInfo = GetCJInfoFromMainForm(cjId);
                        listCJ.Add(cjInfo);
                    }
                }
            }

            bsCJ.DataSource = null;
            bsCJ.DataSource = listCJ;
            dgvCJ.DataSource = bsCJ;
            dgvCJ.ClearSelection();
        }

        /// <summary>
        /// 從 MainForm 取得 PJ 詳細資訊
        /// </summary>
        private PJInfo GetPJInfoFromMainForm(string pjId)
        {
            if (MainForm == null) return new PJInfo { PRJobID = pjId };

            string pauseEvent = string.Empty;
            string carrierId = string.Empty;
            string recId = string.Empty;
            string recVarList = string.Empty;
            string errMsg = string.Empty;
            byte pjState = 0;
            byte prType = 0;
            byte recMethod = 0;
            byte[] slotArray = null; // 修正：用 byte[] 接收
            bool bStart = false;

            int ret = -1;
            try
            {
                ret = MainForm.GetProcessJobAttr(
                    pjId,
                    out pauseEvent,
                    out pjState,
                    out carrierId,
                    out slotArray, // 這裡改為 byte[] 型別
                    out prType,
                    out bStart,
                    out recMethod,
                    out recId,
                    out recVarList,
                    out errMsg);
            }
            catch (InvalidCastException castEx)
            {
                Console.WriteLine($"GetProcessJobAttr 型別轉換錯誤 PJ={pjId}: {castEx.Message}");
                ret = -99;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetProcessJobAttr 錯誤 PJ={pjId}: {ex.Message}");
                ret = -1;
            }

            if (ret != 0)
            {
                return new PJInfo { PRJobID = pjId };
            }

            // slotArray 已是 byte[]，直接使用
            var slots = slotArray ?? new byte[25];

            return new PJInfo
            {
                PRJobID = pjId,
                PauseEvent = pauseEvent ?? string.Empty,
                PRJobState = (PRJobStateEnum)pjState,
                CarrierID = carrierId ?? string.Empty,
                Slots = slots,
                PRMtlType = prType,
                PRProcessStart = bStart,
                PRRecipeMethod = recMethod,
                RecID = recId ?? string.Empty,
                RecVariableList = recVarList ?? string.Empty,
                IsPooled = (PRJobStateEnum)pjState == PRJobStateEnum.POOLED
            };
        }

        /// <summary>
        /// 從 MainForm 取得 CJ 詳細資訊
        /// </summary>
        private CJInfo GetCJInfoFromMainForm(string cjId)
        {
            if (MainForm == null) return new CJInfo { ObjID = cjId };

            string carrierInputSpec, curPJ, dataCollection, mtrloutStatus, mtrloutSpec, pauseEvent, procCtrlSpec, err;
            byte procOrder, state;
            bool bStart;

            int ret = MainForm.GetControlJobAttr(
                cjId,
                out carrierInputSpec,
                out curPJ,
                out dataCollection,
                out mtrloutStatus,
                out mtrloutSpec,
                out pauseEvent,
                out procCtrlSpec,
                out procOrder,
                out bStart,
                out state,
                out err);

            if (ret != 0)
            {
                return new CJInfo { ObjID = cjId };
            }

            // 驗證 ProcessOrderMgmt 列舉值是否有效 (0-2)
            var processOrderMgmt = ProcessOrderMgmtEnum.LIST;
            if (Enum.IsDefined(typeof(ProcessOrderMgmtEnum), (int)procOrder))
            {
                processOrderMgmt = (ProcessOrderMgmtEnum)procOrder;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"[GetCJInfoFromMainForm] 警告: CJ={cjId} 的 ProcessOrderMgmt 值 {procOrder} 無效，使用預設值 LIST");
            }

            // 驗證 CJState 列舉值是否有效 (0-5)
            var cjState = CJStateEnum.QUEUED;
            if (Enum.IsDefined(typeof(CJStateEnum), (int)state))
            {
                cjState = (CJStateEnum)state;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"[GetCJInfoFromMainForm] 警告: CJ={cjId} 的 State 值 {state} 無效，使用預設值 QUEUED");
            }

            var cjInfo = new CJInfo
            {
                ObjID = cjId,
                DataCollectionPlan = dataCollection,
                ProcessOrderMgmt = processOrderMgmt,
                StartMethod = bStart,
                State = cjState
            };

            // 解析 ProcessingCtrlSpec
            if (!string.IsNullOrEmpty(procCtrlSpec))
            {
                cjInfo.ProcessingCtrlSpec = procCtrlSpec.Split(',').ToList();
            }

            // 解析 CarrierInputSpec
            if (!string.IsNullOrEmpty(carrierInputSpec))
            {
                cjInfo.CarrierInputSpec = carrierInputSpec.Split(',').ToList();
            }

            return cjInfo;
        }

        private void UpdatePJGridColumns()
        {
            if (dgvPJ.Columns.Count > 0)
            {
                foreach (DataGridViewColumn col in dgvPJ.Columns)
                {
                    col.Visible = false;
                }

                if (dgvPJ.Columns["PRJobID"] != null)
                    dgvPJ.Columns["PRJobID"].Visible = true;
                if (dgvPJ.Columns["RecID"] != null)
                    dgvPJ.Columns["RecID"].Visible = true;
                if (dgvPJ.Columns["PRJobState"] != null)
                    dgvPJ.Columns["PRJobState"].Visible = true;
                if (dgvPJ.Columns["CarrierID"] != null)
                    dgvPJ.Columns["CarrierID"].Visible = true;
                if (dgvPJ.Columns["IsPooled"] != null)
                    dgvPJ.Columns["IsPooled"].Visible = true;
            }
        }

        private void UpdateUIState()
        {
            btnCreateCJ.Enabled = listPJ.Count > 0;
            UpdateQueueSpaceInfo();
        }

        /// <summary>
        /// 更新隊列空間資訊顯示 (注意事項 2: 隊列空間)
        /// </summary>
        private void UpdateQueueSpaceInfo()
        {
            int pjSpace = GetAvailablePJQueueSpace();
            int cjSpace = GetAvailableCJQueueSpace();

            // 如果有 lblQueueSpace 控制項，更新顯示
            // lblQueueSpace.Text = $"PJ 可用空間: {pjSpace} | CJ 可用空間: {cjSpace}";
        }

        #endregion

        #region =================== 注意事項 2: 隊列空間檢查 =========================

        /// <summary>
        /// 取得 PJ 可用隊列空間 (PRGetSpace - E40)
        /// </summary>
        private int GetAvailablePJQueueSpace()
        {
            int currentCount = DemoFormDiaGemLib.MainForm.PJ_list?.Count ?? 0;
            return Math.Max(0, MAX_PJ_QUEUE_SIZE - currentCount);
        }

        /// <summary>
        /// 取得 CJ 可用隊列空間 (QueueAvailableSpace - E94)
        /// </summary>
        private int GetAvailableCJQueueSpace()
        {
            int currentCount = DemoFormDiaGemLib.MainForm.CJ_list?.Count ?? 0;
            return Math.Max(0, MAX_CJ_QUEUE_SIZE - currentCount);
        }

        /// <summary>
        /// 檢查是否有足夠的 PJ 隊列空間
        /// </summary>
        private bool HasAvailablePJSpace()
        {
            return GetAvailablePJQueueSpace() > 0;
        }

        /// <summary>
        /// 檢查是否有足夠的 CJ 隊列空間
        /// </summary>
        private bool HasAvailableCJSpace()
        {
            return GetAvailableCJQueueSpace() > 0;
        }

        #endregion

        #region =================== 注意事項 2: ID 唯一性驗證 =========================

        /// <summary>
        /// 檢查 PJ ID 是否唯一
        /// </summary>
        private bool IsPJIDUnique(string pjId)
        {
            if (string.IsNullOrWhiteSpace(pjId)) return false;

            // 檢查本地清單
            if (listPJ.Any(p => string.Equals(p.PRJobID, pjId, StringComparison.OrdinalIgnoreCase)))
                return false;

            // 檢查 MainForm 的 PJ_list
            if (DemoFormDiaGemLib.MainForm.PJ_list != null)
            {
                if (DemoFormDiaGemLib.MainForm.PJ_list.Any(id => string.Equals(id, pjId, StringComparison.OrdinalIgnoreCase)))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// 檢查 CJ ID 是否唯一
        /// </summary>
        private bool IsCJIDUnique(string cjId)
        {
            if (string.IsNullOrWhiteSpace(cjId)) return false;

            // 檢查本地清單
            if (listCJ.Any(c => string.Equals(c.ObjID, cjId, StringComparison.OrdinalIgnoreCase)))
                return false;

            // 檢查 MainForm 的 CJ_list
            if (DemoFormDiaGemLib.MainForm.CJ_list != null)
            {
                if (DemoFormDiaGemLib.MainForm.CJ_list.Any(id => string.Equals(id, cjId, StringComparison.OrdinalIgnoreCase)))
                    return false;
            }

            return true;
        }

        #endregion

        #region =================== 注意事項 1: PJ 存在性驗證 =========================

        /// <summary>
        /// 驗證所有指定的 PJ 是否存在 (注意事項 1: 作業關聯性限制)
        /// </summary>
        private ValidationResult ValidateAllPJsExist(List<string> pjIds)
        {
            if (pjIds == null || pjIds.Count == 0)
            {
                return ValidationResult.Fail("ProcessingCtrlSpec 中必須至少指定一個 PJ");
            }

            var missingPJs = new List<string>();
            var existingPJList = DemoFormDiaGemLib.MainForm.PJ_list ?? new List<string>();

            foreach (var pjId in pjIds)
            {
                if (!existingPJList.Contains(pjId))
                {
                    missingPJs.Add(pjId);
                }
            }

            if (missingPJs.Count > 0)
            {
                return ValidationResult.Fail(
                    $"以下 PJ 不存在，CJ 建立失敗:\n{string.Join(", ", missingPJs)}\n\n" +
                    "請先建立這些 PJ 後再建立 CJ。");
            }

            return ValidationResult.Success();
        }

        /// <summary>
        /// 檢查 PJ 是否可被加入 CJ (檢查是否已被其他 CJ 關聯)
        /// </summary>
        private ValidationResult ValidatePJCanBeLinked(string pjId)
        {
            var pjInfo = GetPJInfoFromMainForm(pjId);

            // 檢查 PJ 狀態是否適合加入 CJ
            if (pjInfo.PRJobState == PRJobStateEnum.PROCESS_COMPLETED ||
                pjInfo.PRJobState == PRJobStateEnum.ABORTING)
            {
                return ValidationResult.Fail($"PJ '{pjId}' 狀態為 {pjInfo.PRJobState}，無法加入 CJ");
            }

            // 檢查是否已被其他 CJ 關聯
            if (pjInfo.IsPooled && !string.IsNullOrEmpty(pjInfo.LinkedCJID))
            {
                return ValidationResult.Fail($"PJ '{pjId}' 已被 CJ '{pjInfo.LinkedCJID}' 關聯");
            }

            return ValidationResult.Success();
        }

        #endregion

        #region =================== 注意事項 5: 材料追蹤一致性驗證 =========================

        /// <summary>
        /// 驗證材料 ID 映射一致性 (注意事項 5)
        /// 確保 CJ/PJ 中定義的材料識別碼與設備讀取到的實體材料資訊一致
        /// </summary>
        private ValidationResult ValidateMaterialConsistency(string carrierId, byte[] slots)
        {
            var warnings = new List<string>();

            // 檢查載具是否存在於 LoadPort
            bool carrierFound = false;

            if (Common.EFEM.LoadPort1.FoupID == carrierId)
            {
                carrierFound = true;
                // 驗證 Slot 映射
                var slotMismatch = ValidateSlotMapping(Common.EFEM.LoadPort1, slots);
                if (!string.IsNullOrEmpty(slotMismatch))
                {
                    warnings.Add($"LP1 Slot 映射警告: {slotMismatch}");
                }
            }
            else if (Common.EFEM.LoadPort2.FoupID == carrierId)
            {
                carrierFound = true;
                var slotMismatch = ValidateSlotMapping(Common.EFEM.LoadPort2, slots);
                if (!string.IsNullOrEmpty(slotMismatch))
                {
                    warnings.Add($"LP2 Slot 映射警告: {slotMismatch}");
                }
            }

            if (!carrierFound)
            {
                return ValidationResult.Fail(
                    $"載具 '{carrierId}' 未在任何 LoadPort 上找到。\n" +
                    "請確認載具已正確放置且 ID 讀取正確。");
            }

            var result = ValidationResult.Success();
            result.Warnings = warnings;
            return result;
        }

        /// <summary>
        /// 驗證 Slot 映射
        /// </summary>
        private string ValidateSlotMapping(object loadPort, byte[] requestedSlots)
        {
            // 這裡需要根據實際的 LoadPort 類型實作
            // 比對請求的 Slot 與實際載具中的 Wafer 存在狀態
            return string.Empty;
        }

        #endregion

        #region =================== Step 1: Create PJ (SEMI E40) =========================

        /// <summary>
        /// 步驟 1: 建立製程作業 (Create PJ - SEMI E40)
        /// PRJobCreate 請求
        /// </summary>
        private void btnCreatePJ_Click(object sender, EventArgs e)
        {
            // 驗證輸入
            var validationResult = ValidatePJInputComplete();
            if (!validationResult.IsValid)
            {
                ShowError(validationResult.ErrorMessage);
                return;
            }

            // 顯示警告 (如果有)
            if (validationResult.Warnings.Count > 0)
            {
                var warningMsg = string.Join("\n", validationResult.Warnings);
                var result = MessageBox.Show(
                    $"發現以下警雹˛\n{warningMsg}\n\n是否繼續建立 PJ?",
                    "警告",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes) return;
            }

            try
            {
                // 根據使用者選擇設定 PRMtlType
                byte mtlType = rbSubstrate.Checked ? (byte)14 : (byte)13; // 14=Substrate, 13=Carriers

                // 建立 PJ 物件
                var newPJ = new PJInfo
                {
                    PRJobID = txtPJID.Text.Trim(),
                    RecID = selectedRecipeId,
                    CarrierID = txtCarrierID.Text.Trim(),
                    PRProcessStart = chkAutoStart.Checked,
                    PRMtlType = mtlType,
                    PRRecipeMethod = 0, // Recipe only
                    PRJobState = PRJobStateEnum.QUEUED
                };

                // 設定 Slot 陣列
                byte[] slots = new byte[25];
                if (currentRecipe.Slot != null)
                {
                    for (int i = 0; i < Math.Min(currentRecipe.Slot.Length, 25); i++)
                    {
                        slots[i] = (byte)currentRecipe.Slot[i];

                        if (currentRecipe.Slot[i] == 1)
                        {
                            newPJ.PRMtlNameList.Add(new PRMtlNameItem
                            {
                                CarrierID = newPJ.CarrierID,
                                SlotID = (i + 1).ToString()
                            });
                        }
                    }
                }
                newPJ.Slots = slots;

                // 呼叫 MainForm 建立 PJ
                string errMsg;
                int result = MainForm.CreateProcessJob(
                    newPJ.PRJobID,
                    newPJ.RecID,
                    newPJ.PRProcessStart,
                    newPJ.CarrierID,
                    slots,
                    out errMsg);

                if (result == 0)
                {
                    // 建立成功後，設定 PJ 屬性
                    string setAttrErr;
                    int setAttrResult = MainForm.SetProcessJobAttr(
                        newPJ.PRJobID,
                        newPJ.PauseEvent,
                        newPJ.CarrierID,
                        slots,
                        newPJ.PRMtlType,
                        newPJ.PRProcessStart,
                        newPJ.PRRecipeMethod,
                        newPJ.RecID,
                        newPJ.RecVariableList ?? string.Empty,
                        out setAttrErr);

                    if (setAttrResult != 0)
                    {
                        System.Diagnostics.Debug.WriteLine($"[CreatePJ] SetProcessJobAttr 警告: {setAttrErr}");
                    }

                    // 直接將新建立的 PJ 加入清單並更新 UI
                    listPJ.Add(newPJ);
                    bsPJ.ResetBindings(false);
                    dgvPJ.ClearSelection();
                    UpdatePJGridColumns();
                    ClearPJInput();
                    UpdateUIState();

                    ShowMessage($"PJ '{newPJ.PRJobID}' 建立成功\n狀態: QUEUED/POoled\nMaterial Type: {(mtlType == 14 ? "Substrate" : "Carriers")}\n可用 PJ 空間: {GetAvailablePJQueueSpace()}");

                    InsertLog.SavetoDB(50, $"Create PJ: {newPJ.PRJobID}, Recipe: {newPJ.RecID}, Carrier: {newPJ.CarrierID}, MtlType: {mtlType}");
                }
                else
                {
                    ShowError($"PJ 建立失敗: {errMsg}");
                }
            }
            catch (Exception ex)
            {
                ShowError($"建立 PJ 發生錯誤: {ex.Message}");
                InsertLog.SavetoDB(67, $"Create PJ Error: {ex.Message}");
            }
        }

        /// <summary>
        /// 完整驗證 PJ 輸入 (包含所有注意事項)
        /// </summary>
        private ValidationResult ValidatePJInputComplete()
        {
            // 基本欄位驗證
            if (string.IsNullOrWhiteSpace(txtPJID.Text))
            {
                txtPJID.Focus();
                return ValidationResult.Fail("請輸入 PJ ID (PRJobID)");
            }

            // 注意事項 2: ID 唯一性
            if (!IsPJIDUnique(txtPJID.Text.Trim()))
            {
                txtPJID.Focus();
                return ValidationResult.Fail("PJ ID 已存在，請輸入不同的名稱\n(所有 PRJobID 在設備內必須是唯一的)");
            }

            // 注意事項 2: 隊列空間
            if (!HasAvailablePJSpace())
            {
                return ValidationResult.Fail(
                    $"PJ 隊列已滿 (最大容量: {MAX_PJ_QUEUE_SIZE})\n" +
                    "請先完成或刪除現有的 PJ 後再建立新的 PJ");
            }

            if (string.IsNullOrWhiteSpace(selectedRecipeId))
            {
                return ValidationResult.Fail("請選擇Recipe (RecID)");
            }

            if (string.IsNullOrWhiteSpace(txtCarrierID.Text))
            {
                txtCarrierID.Focus();
                return ValidationResult.Fail("請輸入載具 ID (CarrierID)");
            }

            // 注意事項 5: 材料追蹤一致性
            byte[] slots = new byte[25];
            if (currentRecipe.Slot != null)
            {
                for (int i = 0; i < Math.Min(currentRecipe.Slot.Length, 25); i++)
                {
                    slots[i] = (byte)currentRecipe.Slot[i];
                }
            }

            var materialValidation = ValidateMaterialConsistency(txtCarrierID.Text.Trim(), slots);
            if (!materialValidation.IsValid)
            {
                return materialValidation;
            }

            var result = ValidationResult.Success();
            result.Warnings = materialValidation.Warnings;
            return result;
        }

        private void ClearPJInput()
        {
            txtPJID.Text = string.Empty;
            txtCarrierID.Text = string.Empty;
            chkAutoStart.Checked = false;
            rbSubstrate.Checked = true; // 預設選擇 Substrate
            selectedRecipeId = string.Empty;
            lblSelectedRecipe.Text = "已選擇: (請選擇 Recipe)";
        }

        #endregion

        #region =================== Step 2: Create CJ (SEMI E94) =========================

        /// <summary>
        /// 步驟 2: 建立控制作業 (Create CJ - SEMI E94)
        /// </summary>
        private void btnCreateCJ_Click(object sender, EventArgs e)
        {
            // 完整驗證 (包含所有注意事項)
            var validationResult = ValidateCJInputComplete();
            if (!validationResult.IsValid)
            {
                ShowError(validationResult.ErrorMessage);
                return;
            }

            // 顯示警告
            if (validationResult.Warnings.Count > 0)
            {
                var warningMsg = string.Join("\n", validationResult.Warnings);
                var result = MessageBox.Show(
                    $"發現以下警告:\n{warningMsg}\n\n是否繼續建立 CJ?",
                    "警告",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes) return;
            }

            try
            {
                var newCJ = new CJInfo
                {
                    ObjID = txtCJID.Text.Trim(),
                    ObjType = "ControlJob",
                    ProcessingCtrlSpec = new List<string>(selectedPJsForCJ),
                    CarrierInputSpec = GetCarrierInputSpec(),
                    MtrlOutSpec = GetMtrlOutSpec(),
                    ProcessOrderMgmt = (ProcessOrderMgmtEnum)(cboProcessOrder.SelectedIndex >= 0 ? cboProcessOrder.SelectedIndex : 0),
                    StartMethod = chkCJAutoStart.Checked,
                    State = CJStateEnum.QUEUED
                };

                string errMsg;
                int result = MainForm.CreateControlJob(
                    newCJ.ObjID,
                    newCJ.CarrierInputSpec,
                    "",
                    newCJ.ProcessingCtrlSpec.ToArray(),
                    (byte)newCJ.ProcessOrderMgmt,
                    newCJ.StartMethod,
                    "",
                    out errMsg);

                if (result == 0)
                {
                    // 更新關聯的 PJ 狀態為 POOLED 並設定 LinkedCJID
                    foreach (var pjId in selectedPJsForCJ)
                    {
                        var pj = listPJ.FirstOrDefault(p => p.PRJobID == pjId);
                        if (pj != null)
                        {
                            pj.IsPooled = true;
                            pj.LinkedCJID = newCJ.ObjID;
                            pj.PRJobState = PRJobStateEnum.POOLED;
                            
                            // 偵錯輸出：確認 LinkedCJID 已設置
                            System.Diagnostics.Debug.WriteLine($"[CreateCJ] PJ={pjId} LinkedCJID={pj.LinkedCJID} IsPooled={pj.IsPooled}");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"[CreateCJ] 警告: 在 listPJ 中找不到 PJ={pjId}");
                        }
                    }

                    // 直接將新建立的 CJ 加入清單並更新 UI
                    listCJ.Add(newCJ);
                    bsCJ.ResetBindings(false);
                    dgvCJ.ClearSelection();

                    // 更新 PJ 清單顯示 (不重新載入，保留 LinkedCJID)
                    bsPJ.ResetBindings(false);
                    dgvPJ.ClearSelection();

                    ClearCJInput();
                    UpdateUIState();
                    
                    // 偵錯輸出：驗證更新後的狀態
                    System.Diagnostics.Debug.WriteLine($"[CreateCJ] CJ={newCJ.ObjID} 建立成功，關聯 PJ 數量={newCJ.ProcessingCtrlSpec.Count}");

                    ShowMessage(
                        $"CJ '{newCJ.ObjID}' 建立成功\n" +
                        $"狀態: QUEUED\n" +
                        $"已加入設備控制作業隊列\n" +
                        $"關聯的 PJ: {string.Join(", ", newCJ.ProcessingCtrlSpec)}\n" +
                        $"可用 CJ 空間: {GetAvailableCJQueueSpace()}");

                    InsertLog.SavetoDB(50, $"Create CJ: {newCJ.ObjID}, PJs: {string.Join(",", newCJ.ProcessingCtrlSpec)}");
                }
                else
                {
                    ShowError($"CJ 建立失敗: {errMsg}");
                }
            }
            catch (Exception ex)
            {
                ShowError($"建立 CJ 發生錯誤: {ex.Message}");
                InsertLog.SavetoDB(67, $"Create CJ Error: {ex.Message}");
            }
        }

        /// <summary>
        /// 完整驗證 CJ 輸入 (包含所有注意事項)
        /// </summary>
        private ValidationResult ValidateCJInputComplete()
        {
            var warnings = new List<string>();

            // 基本欄位驗證
            if (string.IsNullOrWhiteSpace(txtCJID.Text))
            {
                txtCJID.Focus();
                return ValidationResult.Fail("請輸入 CJ ID");
            }

            // 注意事項 2: ID 唯一性
            if (!IsCJIDUnique(txtCJID.Text.Trim()))
            {
                txtCJID.Focus();
                return ValidationResult.Fail("CJ ID 已存在，請輸入不同的名稱\n(所有 CtrlJobID 在設備內必須是唯一的)");
            }

            // 注意事項 2: 隊列空間
            if (!HasAvailableCJSpace())
            {
                return ValidationResult.Fail(
                    $"CJ 隊列已滿 (最大容量: {MAX_CJ_QUEUE_SIZE})\n" +
                    "請先完成或刪除現有的 CJ 後再建立新的 CJ");
            }

            if (selectedPJsForCJ.Count == 0)
            {
                return ValidationResult.Fail("請選擇至少一個 PJ 加入 CJ (ProcessingCtrlSpec)");
            }

            // 注意事項 1: PJ 必須先存在
            var pjExistValidation = ValidateAllPJsExist(selectedPJsForCJ);
            if (!pjExistValidation.IsValid)
            {
                return pjExistValidation;
            }

            // 檢查每個 PJ 是否可被關聯
            foreach (var pjId in selectedPJsForCJ)
            {
                var linkValidation = ValidatePJCanBeLinked(pjId);
                if (!linkValidation.IsValid)
                {
                    return linkValidation;
                }
            }

            // 注意事項 5: 驗證材料一致性
            var carriers = GetCarrierInputSpec();
            foreach (var carrierId in carriers)
            {
                var materialValidation = ValidateMaterialConsistency(carrierId, new byte[25]);
                if (!materialValidation.IsValid)
                {
                    return materialValidation;
                }
                warnings.AddRange(materialValidation.Warnings);
            }

            var result = ValidationResult.Success();
            result.Warnings = warnings;
            return result;
        }

        private List<string> GetCarrierInputSpec()
        {
            var carriers = new List<string>();
            foreach (var pjId in selectedPJsForCJ)
            {
                var pj = listPJ.FirstOrDefault(p => p.PRJobID == pjId);
                if (pj != null && !string.IsNullOrEmpty(pj.CarrierID) && !carriers.Contains(pj.CarrierID))
                {
                    carriers.Add(pj.CarrierID);
                }
            }
            return carriers;
        }

        private List<string> GetMtrlOutSpec()
        {
            return GetCarrierInputSpec();
        }

        private void ClearCJInput()
        {
            txtCJID.Text = string.Empty;
            selectedPJsForCJ.Clear();
            lstSelectedPJ.Items.Clear();
            chkCJAutoStart.Checked = false;
            if (cboProcessOrder.Items.Count > 0)
                cboProcessOrder.SelectedIndex = 0;
        }

        #endregion

        #region =================== 注意事項 3: 修改作業的時機 =========================

        /// <summary>
        /// 檢查 CJ 是否可修改
        /// </summary>
        private bool CanModifyCJ(string cjId)
        {
            var cj = listCJ.FirstOrDefault(c => c.ObjID == cjId);
            if (cj == null) return false;

            return cj.IsModifiable;
        }

        /// <summary>
        /// 修改 CJ 屬性 (僅當狀態允許時)
        /// </summary>
        private bool TryModifyCJ(string cjId, Action<CJInfo> modifyAction, out string errorMessage)
        {
            errorMessage = string.Empty;

            var cj = listCJ.FirstOrDefault(c => c.ObjID == cjId);
            if (cj == null)
            {
                errorMessage = $"CJ '{cjId}' 不存在";
                return false;
            }

            // 注意事項 3: 狀態限制
            if (!cj.IsModifiable)
            {
                errorMessage = $"CJ '{cjId}' 目前狀態為 {cj.State}，無法修改\n" +
                              "只有當 CJ 不處於 EXECUTING 或 COMPLETED 狀態時才可修改";
                return false;
            }

            try
            {
                modifyAction(cj);
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 檢查在 PAUSED 狀態下是否可修改 PJ
        /// </summary>
        private bool CanModifyPJInPausedState(string pjId)
        {
            var pj = listPJ.FirstOrDefault(p => p.PRJobID == pjId);
            if (pj == null) return false;

            // 注意事項 3: 暫停中的修改
            // 只有尚未開始執行的 PJ 可修改
            return pj.PRJobState == PRJobStateEnum.QUEUED ||
                   pj.PRJobState == PRJobStateEnum.POOLED ||
                   pj.PRJobState == PRJobStateEnum.WAITING_FOR_START;
        }

        #endregion

        #region =================== 注意事項 4: 異常處理與材料風險 =========================

        /// <summary>
        /// 執行作業命令 (Cancel/Stop/Abort)
        /// 使用 MainForm 提供的 ChangeProcessJobState 和 ChangeControlJobState 方法
        /// </summary>
        private bool ExecuteJobCommand(string jobId, bool isCJ, JobCommandEnum command, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                switch (command)
                {
                    case JobCommandEnum.Cancel:
                        return ExecuteCancelCommand(jobId, isCJ, out errorMessage);

                    case JobCommandEnum.Stop:
                        return ExecuteStopCommand(jobId, isCJ, out errorMessage);

                    case JobCommandEnum.Abort:
                        return ExecuteAbortCommand(jobId, isCJ, out errorMessage);

                    default:
                        errorMessage = "未知的命令類型";
                        return false;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                InsertLog.SavetoDB(67, $"JobCommand Error: {command} on {jobId}, {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Cancel 命令 - 僅適用於 QUEUED 狀態
        /// 使用 DeleteProcessJob / DeleteControlJob 或變更狀態到 COMPLETED
        /// </summary>
        private bool ExecuteCancelCommand(string jobId, bool isCJ, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (isCJ)
            {
                var cj = listCJ.FirstOrDefault(c => c.ObjID == jobId);
                if (cj == null)
                {
                    errorMessage = $"CJ '{jobId}' 不存在";
                    return false;
                }

                if (cj.State != CJStateEnum.QUEUED)
                {
                    errorMessage = $"Cancel 僅適用於 QUEUED 狀態的作業\n" +
                                  $"CJ '{jobId}' 目前狀態為 {cj.State}";
                    return false;
                }

                // 使用 ChangeControlJobState 變更為 COMPLETED (unnormal=0 表示正常取消)
                string err;
                int result = MainForm.ChangeControlJobState(jobId, DemoFormDiaGemLib.ControlJobState.COMPLETED, 0);
                if (result != 0)
                {
                    errorMessage = $"Cancel CJ 失敗，錯誤碼: {result}";
                    return false;
                }

                // 釋放關聯的 PJ
                foreach (var pjId in cj.ProcessingCtrlSpec)
                {
                    var pj = listPJ.FirstOrDefault(p => p.PRJobID == pjId);
                    if (pj != null)
                    {
                        pj.IsPooled = false;
                        pj.LinkedCJID = null;
                        pj.PRJobState = PRJobStateEnum.QUEUED;
                    }
                }

                listCJ.Remove(cj);
                InsertLog.SavetoDB(51, $"Cancel CJ: {jobId}");
            }
            else
            {
                var pj = listPJ.FirstOrDefault(p => p.PRJobID == jobId);
                if (pj == null)
                {
                    errorMessage = $"PJ '{jobId}' 不存在";
                    return false;
                }

                if (pj.PRJobState != PRJobStateEnum.QUEUED && pj.PRJobState != PRJobStateEnum.POOLED)
                {
                    errorMessage = $"Cancel 僅適用於 QUEUED/POoled 狀態的作業\n" +
                                  $"PJ '{jobId}' 目前狀態為 {pj.PRJobState}";
                    return false;
                }

                // 使用 ChangeProcessJobState 變更為 UNQUEUED (用於取消 QUEUED 狀態的 PJ)
                string err;
                int result = MainForm.ChangeProcessJobState(jobId, DemoFormDiaGemLib.ProcessJobState.UNQUEUED);
                if (result != 0)
                {
                    errorMessage = $"Cancel PJ 失敗，錯誤碼: {result}";
                    return false;
                }

                listPJ.Remove(pj);
                InsertLog.SavetoDB(51, $"Cancel PJ: {jobId}");
            }

            RefreshPJList();
            RefreshCJList();
            return true;
        }

        /// <summary>
        /// Stop 命令 - 有序停止，保護材料完整性
        /// 使用 ChangeProcessJobState(STOPPING) / ChangeControlJobState(COMPLETED, 1)
        /// </summary>
        private bool ExecuteStopCommand(string jobId, bool isCJ, out string errorMessage)
        {
            errorMessage = string.Empty;

            // 顯示警告
            var dialogResult = MessageBox.Show(
                $"Stop 命令將以有序的方式停止作業 '{jobId}'，盡可能保護材料完整性。\n\n" +
                "確定要執行 Stop 命令嗎?",
                "確認 Stop",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialogResult != DialogResult.Yes) return false;

            int ret;

            if (isCJ)
            {
                // unnormal=1 表示 Stop
                ret = MainForm.ChangeControlJobState(jobId, DemoFormDiaGemLib.ControlJobState.COMPLETED, 1);
            }
            else
            {
                // 變更 PJ 狀態為 STOPPING
                ret = MainForm.ChangeProcessJobState(jobId, DemoFormDiaGemLib.ProcessJobState.STOPPING);
            }

            if (ret != 0)
            {
                errorMessage = $"Stop 失敗，錯誤碼: {ret}";
                return false;
            }

            InsertLog.SavetoDB(51, $"Stop {(isCJ ? "CJ" : "PJ")}: {jobId}");
            RefreshPJList();
            RefreshCJList();
            return true;
        }

        /// <summary>
        /// Abort 命令 - 立即停止，材料可能處於未知狀態
        /// 使用 ChangeProcessJobState(ABORTING) / ChangeControlJobState(COMPLETED, 2)
        /// </summary>
        private bool ExecuteAbortCommand(string jobId, bool isCJ, out string errorMessage)
        {
            errorMessage = string.Empty;

            // 顯示嚴重警告
            var dialogResult = MessageBox.Show(
                $"?? 警告：Abort 命令將立即停止作業 '{jobId}'！\n\n" +
                "這可能導致:\n" +
                "? 材料處於未知狀態\n" +
                "? 材料可能面臨損壞風險\n" +
                "? 需要人工介入確認設備狀態\n\n" +
                "確定要執行 Abort 命令嗎?",
                "?? 確認 Abort - 材料風險警告",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dialogResult != DialogResult.Yes) return false;

            int ret;

            if (isCJ)
            {
                // unnormal=2 表示 Abort
                ret = MainForm.ChangeControlJobState(jobId, DemoFormDiaGemLib.ControlJobState.COMPLETED, 2);
            }
            else
            {
                // 變更 PJ 狀態為 ABORTING
                ret = MainForm.ChangeProcessJobState(jobId, DemoFormDiaGemLib.ProcessJobState.ABORTING);
            }

            if (ret != 0)
            {
                errorMessage = $"Abort 失敗，錯誤碼: {ret}";
                return false;
            }

            // 記錄 Abort 事件 (重要性較高)
            InsertLog.SavetoDB(67, $"ABORT {(isCJ ? "CJ" : "PJ")}: {jobId} - 材料可能處於未知狀態");

            MessageBox.Show(
                $"已執行 Abort 命令\n\n" +
                "請注意:\n" +
                "1. 檢查設備狀態\n" +
                "2. 確認材料狀態\n" +
                "3. 必要時執行設備復位",
                "Abort 完成",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            RefreshPJList();
            RefreshCJList();
            return true;
        }

        #endregion

        #region =================== Event Handlers =========================

        /// <summary>
        /// 材料類型選擇變更事件處理
        /// </summary>
        private void rbMaterialType_CheckedChanged(object sender, EventArgs e)
        {
            // 根據選擇的材料類型更新 UI 狀態
            UpdateUIState();
        }

        private void dgvRecipe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvRecipe.Rows.Count) return;
            if (listRecipe == null || listRecipe.Count == 0) return;

            var recipeName = dgvRecipe.Rows[e.RowIndex].Cells["RecipeName"].Value?.ToString();
            if (!string.IsNullOrEmpty(recipeName))
            {
                selectedRecipeId = recipeName;
                lblSelectedRecipe.Text = $"已選擇: {recipeName}";

                string recipePath = Path.Combine(fram.Recipe.Path, recipeName + ".ini");
                if (File.Exists(recipePath))
                {
                    ParamFile.ReadRcpini(recipePath, "Recipe", currentRecipe);
                }
            }
        }

        private void dgvPJ_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvPJ.Rows.Count) return;
            if (listPJ == null || listPJ.Count == 0) return;

            var pjId = dgvPJ.Rows[e.RowIndex].Cells["PRJobID"].Value?.ToString();
            if (!string.IsNullOrEmpty(pjId))
            {
                var pj = listPJ.FirstOrDefault(p => p.PRJobID == pjId);
                if (pj != null)
                {
                    DisplayPJDetails(pj);
                }
            }
        }

        private void dgvCJ_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvCJ.Rows.Count) return;
            if (listCJ == null || listCJ.Count == 0) return;

            // 如果需要顯示 CJ 詳細資訊，可以在這裡實作
        }

        private void btnAddPJToCJ_Click(object sender, EventArgs e)
        {
            if (dgvPJ.CurrentRow == null) return;

            var pjId = dgvPJ.CurrentRow.Cells["PRJobID"].Value?.ToString();
            if (string.IsNullOrEmpty(pjId)) return;

            // 檢查 PJ 是否可被加入
            var validation = ValidatePJCanBeLinked(pjId);
            if (!validation.IsValid)
            {
                ShowError(validation.ErrorMessage);
                return;
            }

            if (!selectedPJsForCJ.Contains(pjId))
            {
                selectedPJsForCJ.Add(pjId);
                lstSelectedPJ.Items.Add(pjId);
            }
        }

        private void btnRemovePJFromCJ_Click(object sender, EventArgs e)
        {
            if (lstSelectedPJ.SelectedItem != null)
            {
                var pjId = lstSelectedPJ.SelectedItem.ToString();
                selectedPJsForCJ.Remove(pjId);
                lstSelectedPJ.Items.Remove(lstSelectedPJ.SelectedItem);
            }
        }

        private void DisplayPJDetails(PJInfo pj)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"===== PJ 詳細資訊 =====");
            sb.AppendLine($"PRJobID: {pj.PRJobID}");
            sb.AppendLine($"RecID: {pj.RecID}");
            sb.AppendLine($"PRJobState: {pj.PRJobState}");
            sb.AppendLine($"CarrierID: {pj.CarrierID}");
            sb.AppendLine($"PRProcessStart: {pj.PRProcessStart}");
            sb.AppendLine($"PRMtlType: {GetMaterialTypeName(pj.PRMtlType)}");
            sb.AppendLine($"PRRecipeMethod: {(pj.PRRecipeMethod == 0 ? "Recipe only" : "Recipe with Variable Tuning")}");
            sb.AppendLine();
            sb.AppendLine($"===== E94 狀態 =====");
            sb.AppendLine($"IsPooled: {pj.IsPooled}");
            sb.AppendLine($"LinkedCJID: {pj.LinkedCJID ?? "(無)"}");
            sb.AppendLine($"可修改: {CanModifyPJInPausedState(pj.PRJobID)}");

            if (pj.Slots != null)
            {
                var slotStr = string.Join(" ", pj.Slots.Select((s, i) => (i + 1) % 5 == 0 ? s.ToString() + " " : s.ToString()));
                sb.AppendLine($"\nSlots (1-25): {slotStr}");
            }

            rtbPJDetails.Text = sb.ToString();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshPJList();
            RefreshCJList();
            UpdateUIState();
            ShowMessage($"已刷新 PJ/CJ 清單\nPJ 可用空間: {GetAvailablePJQueueSpace()}\nCJ 可用空間: {GetAvailableCJQueueSpace()}");
        }

        private void dgvCJ_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // 防止空資料時的錯誤
            e.ThrowException = false;
        }

        #endregion

        #region =================== Helper Methods =========================

        private void ShowMessage(string message)
        {
            MessageBox.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 取得材料類型名稱
        /// </summary>
        private string GetMaterialTypeName(byte mtlType)
        {
            return mtlType == 14 ? "Substrate (14)" : $"Carriers ({mtlType})";
        }

        #endregion
    }
}
