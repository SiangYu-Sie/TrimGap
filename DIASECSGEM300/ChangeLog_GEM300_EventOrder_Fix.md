# 變更紀錄：GEM300 E87 載入流程與事件順序大修復 (2026-03)

本文件完整記錄了在進行自動化主機 (Host Simulator) 「載入流程 (Load Scenario)」測試時，機台未符合 SEMI E87 標準規範所引發的一連串錯誤，以及我們依序突破與修正的所有過程。

## 修正歷程總覽 (依修改時間先後)

### 階段一：Carrier ID 提早上報機制的修正
* **問題描述**：機台在 LoadPort 剛偵測到 FOUP 放置、但還未經過夾爪夾合讀碼前，就立刻將 FoupID 填入系統標準變數 `CarrierID` 中並隨 Placement 事件上報。Host 拒絕接受，因為依照 SEMI E87 狀態模型，尚未讀取條碼前的放置當下，Carrier ID 必須是未知 (Unknown) 狀態。
* **修正方式 (`AutoRunEFEM.cs`)**：
  1. 將 `WatchMaterialRecive()` 內發送事件的 `CarrierID` 強制改為 `""`（空字串）。
  2. 將真正的讀碼行為 (`ReadFoupID()`) 與 ID 上報動作，向後分離並延遲到接下來的 `ClampAndReadID()` 步驟（夾合完成後）才執行，完美符合硬體動作的實際時序。

### 階段二：Slot Map (Wafer Map) 取代盲發事件，導入標準 E87 狀態模型
* **問題描述**：原先機台在建構完成 Wafer Mapping 資料後，僅單方面透過呼叫舊函式或盲發 `EventReportSend(SlotMap)` 送出自訂事件，且原本未確實轉換為長度對齊與標準定義值的格式 (造成陣列異常)。由於未符合標準的 `SLOTMAP` 及 `SLOTMAPSTATUS` 屬性轉換，導致 E87 驗證的 Host 無法取得正確的 Wafer Map，驗證中斷。
* **修正方式 (`DIASECSGEM300\MainForm.cs`, `AutoRunEFEM.cs`)**：
  1. **建構標準 SlotMap 資料轉換方法 (`MainForm.cs`)**：新增了 `SetCarrierAttr_SlotMap` 函式。專門將原機台掃描的 0 / 1 / 2 整數陣列狀態，對應轉換為 SEMI E87 標準的晶圓識別碼格式（Empty=1, 正確放置=3, 跨槽=5, 異常=2），並確實綁定到系統標準的 Carrier 物件 `SLOTMAP (447)` 屬性中。
  2. **建置標準狀態機引擊 (`MainForm.cs`)**：新增 `SetCarrierStatus_SlotMap` 狀態控制程序，負責驅動標準 `WAITING_FOR_HOST` / `SLOT_MAP_VERIFICATION_OK` 等關聯機制。
  3. **套用正式流程 (`AutoRunEFEM.cs`)**：在硬體完成感測掃描 (執行 `loadPort.Map()` 完畢) 準備回報時，全面刪去以往的 `EventReportSend` 土炮送法。改為呼叫 `SetCarrierAttr_SlotMap(loadPort.Slot)` 將測出的精確晶圓圖賦值進入系統，並推進 `SetCarrierStatus_SlotMap(WAITING_FOR_HOST)`。此舉不只解決了當機破圖問題，系統現在更能自行正確派發符合 E87 規定的 Mapping 系列變更事件！

### 階段三：Carrier Placement 驗證失敗 (Not Receive/Timeout)
* **問題描述**：Host 模擬軟體執行 LoadComplete 判定時，拒絕承認機台發出的 Carrier 放置事件，並因等待過久造成 Timeout。追查後發現 E87 放置的先決條件 (狀態、物件、標準代碼) 並未完全滿足。
* **修正方式 (`AutoRunEFEM.cs`, `SecsGemInterface.cs`)**：
  1. **提早建立 Carrier 物件**：SEMI 要求實體放置送出狀態事件當下，關聯的 Carrier 物件就必須存在。因此將呼叫 `CreateCarrier` 從原本的夾合階段提早移至 `WatchMaterialRecive()` 驗證觸發的最前面執行。
  2. **補足關聯狀態更新**：E87 規定當 Carrier 放上後，LoadPort 必須從 `NOT ASSOCIATED` 變更為 `ASSOCIATED` 狀態。因此於 `SecsGemInterface.cs` 開放 `UpdateLoadPortAssociationState`，並於流程中第一時間加上關聯值 `1`。
  3. **更正錯誤的 Placement CEID**：將原程式錯發的 `CEID 223` (在現行設定中屬於 OutOfServiceToInService)，更正為 Host 真正引頸期盼的標準對應事件 `CEID 229` (ReadyToLoadToTransferBlocked)。

### 階段四：Auto Clamp 出現「Unable to determine the port of the event」錯誤
* **問題描述**：Host 順利來到第三步並收到了 Clamp 的 `1006` 的夾合事件，卻發生解析拒絕，報錯無法辨識出該事件是屬於 LoadPort 1 還是 LoadPort 2 發出。
* **修正方式 (`DIASECSGEM300\MainForm.cs`, `AutoRunEFEM.cs`)**：
  1. **修復底層 Typo 錯誤 (`MainForm.cs`)**：查閱建構程式後發現，LoadPort 2 在內部映射屬性建立時被誤打為駝峰式縮寫 `"PortID"`。由於底層的 SECS 綁定引擎嚴格要求變數名必須為全大寫，這造成 LoadPort 2 在綁定時直接遺失該屬性。我們已將其強制更正為 `"PORTID"`。
  2. **增補 E87 通用識別 SVID (`AutoRunEFEM.cs`)**：為了對抗部分市面上嚴苛的 E87 模擬軟體，在這類實體 Port 操作事件 (如 `1006` 夾合) 發生前，強制執行 `Common.SecsgemForm.UpdateSV(462, (byte)X, out err);`。`SVID 462` 即是法規中代表 `CMS_PORTID` 的常數位置，塞入對應的 1 或 2，可確保任何廠牌 Host 在解析 Report 時都能掌握確切硬體來源。

### 階段五：模擬模式下 Carrier Removed 事件搶先觸發 (2026-03-06)
* **問題描述**：在模擬模式 (`m_simulateRun != 0`) 下，程式在 Carrier 處理完成、準備 Unload 時就自動設定 `Placement = false`，這會導致 `WatchMaterialRemove()` 搶先觸發 `MaterialRemove` 事件，Host 收到的事件順序混亂。
* **修正方式 (`AutoRunEFEM.cs`)**：
  1. 移除 `Carrier 完成處理區塊` 中的 `Common.EFEM.LoadPort1.Placement = false` / `Common.EFEM.LoadPort2.Placement = false` 自動設定。
  2. 改由模擬按鈕手動控制 Carrier 的移除時機，避免 `WatchMaterialRemove()` 在錯誤的時間點觸發。

### 階段六：新增 Carrier Load/Unload Complete 事件 (2026-03-06)
* **問題描述**：Host 測試軟體 (ToolSoftwareTester) 在 Step 25 檢查 `CarrierUnloadComplete` (CEID 5118) 事件，但機台從未發送過此事件。
* **修正方式 (`SECSGEM\EqpID.cs`, `AutoRunEFEM.cs`)**：
  1. **新增常數定義**：在 `SECSGEM\EqpID.cs` 中新增 `CarrierLoadComplete = 5117` 和 `CarrierUnloadComplete = 5118`。
  2. **發送事件**：在 `WatchMaterialRemove()` 偵測到 LoadPort1/LoadPort2 的 Carrier 移除後，緊接著 `MaterialRemove` 事件之後，加上 `EventReportSend(5118)` 發送 `CarrierUnloadComplete`。

### 階段七：Wafer Process Start/End Event — LotID/SubstrateID 空值修復 (2026-03-06)
* **問題描述**：
  1. Wafer Process Start (CEID 151) 送出後 Host 報錯 `LotID cannot be empty`。RPID 55 需要的 DV 342 (LotID) 為空。
  2. 原先直接呼叫 `EventReportSend(151)` 時，GEM Controller 不知道目前是哪個 Substrate，無法從 Substrate Object 自動帶出 LotID 等屬性值。
* **修正方式 (`SecsGemInterface.cs`, `AutoRunEFEM.cs`, `DIASECSGEM300\MainForm.cs`)**：
  1. **新增 `SetSubstrateStatus_Proc` wrapper (`SecsGemInterface.cs`)**：封裝 `MainForm.SetSubstrateStatus_Proc()`，讓 `AutoRunEFEM` 可呼叫標準 Substrate 狀態轉換函式。
  2. **防禦性檢查 (`MainForm.cs`)**：在 `SetSubstrateStatus_Proc` 中加入 `GetObject` 失敗時的提前返回判斷，避免 Substrate 不存在導致 NullReferenceException。
  3. **完整更新 RPID 55/56 所有 DV (`AutoRunEFEM.cs`)**：在 CEID 151 和 152 送出前，手動 `UpdateSV` 以下所有必要 DV：
     | DVID | 名稱 | 值來源 |
     |------|------|--------|
     | 341 | SubstrateID | `loadPort.SubstrateID[slot]` |
     | 342 | LotID | `loadPort.LotID[slot]`，fallback: FoupID |
     | 4170 | SlotID | Slot 號碼 (1-25) |
     | 346 | SubstrateLocationID | `"Stage1"` |
     | 347 | SubstProcState | `1` (IN_PROCESS) / `2` (PROCESSED) |
     | 4180 | ActivePRJobID | `sram.RunningPJ` |
     | 4181 | ActivePRJobPortID | Port 號碼 |
     | 4182 | ActivePRJobCarrierID | `loadPort.FoupID` |
     | 4183 | ActiveRecipeID | `sram.PJInfo.recID` |
  4. **在 SlotMap 階段建立 Substrate 物件 (`AutoRunEFEM.cs`)**：在 `SlotMap()` 完成後，為每個有 Wafer 的 Slot 呼叫 `CreateSubstrate(substID, lotID, location)`，確保後續 `SetSubstrateStatus_Proc` 能正確找到 Substrate Object。

### 階段八：Wafer Process End Event 位置修正與 SlotID=0 修復 (2026-03-06)
* **問題描述**：
  1. CEID 152 (Wafer Process End) 原先放在 `EFEMStep.Finish`，那是**整個 Carrier** 所有 Wafer 做完後才執行的步驟。E90 標準要求**每片 Wafer** 處理完成時就送事件。
  2. 修正位置到 `WaferGetFormStage1` 後，Host 報錯 `SlotID actual=[0]`，因為 `WaferGet()` 呼叫後會把 `Stage1.Slot` 清零。
* **修正方式 (`AutoRunEFEM.cs`)**：
  1. **移動事件位置**：將 CEID 152 從 `EFEMStep.Finish` 移至 `EFEMStep.WaferGetFormStage1` — 每片 Wafer 從 Stage 取走成功時立即送出。
  2. **提前保存 Slot**：在 `WaferGet()` 前先 `int savedSlot = Common.EFEM.Stage1.Slot`，後續所有 DV 更新 (`slotIdx`, `UpdateSV(4170, ...)`, log) 都使用 `savedSlot`，避免取到清零後的值。
  3. **修正後的正確流程**：
     ```
     WaferPut2Stage1  → 送 CEID 151 (WaferProcessStart)
       → 量測中...
     WaferGetFormStage1 → 送 CEID 152 (WaferProcessEnd)
       → 回 LoadPort 或取下一片
     ```

### 階段九：SubstrateID 陣列索引 off-by-one 修復 (2026-03-06)
* **問題描述**：量測到最後一片時，Host 報錯 `SubstrateID expected in=[["1","2",...,"20",...]] actual=[TEST12345]`。最後一片的 SubstrateID 取到 FoupID 而非正確的 Substrate 名稱。
* **根因**：`Common.EFEM.Stage1.Slot` 是 **1-indexed**（物理 Slot 號碼 1-25），但 `loadPort.SubstrateID[]` 是 **0-indexed**（陣列索引 0-24）。先前將 `slotIdx = Stage1.Slot - 1` 改成 `slotIdx = Stage1.Slot`（移除 -1），導致最後一片 Slot=20 → `SubstrateID[20]` = `""`（空值）→ fallback 到 FoupID。
* **修正方式 (`AutoRunEFEM.cs`)**：
  1. 四處事件發送邏輯（AP6 WaferProcessStart、N2 WaferProcessStart、WaferGetFormStage1 WaferProcessEnd、EFEMStep.Finish WaferProcessEnd）全部改回 `slotIdx = Stage1.Slot - 1` 或 `slotIdx = savedSlot - 1`。
  2. 注意 `UpdateSV(4170, ...)` 的 SlotID 值**不需要** -1，因為 Host 期望的是 1-indexed 的 Slot 號碼。

### 階段十：EFEMStep.Finish 重複發送 CEID 152 導致 ActivePRJobID 為空 (2026-03-06)
* **問題描述**：Wafer Process End Event 在最後出現 `ActivePRJobID cannot be empty`。
* **根因**：CEID 152 在兩處被發送：
  1. `WaferGetFormStage1`：每片 Wafer 取走時送出（正確，此時 `sram.RunningPJ` 有值）
  2. `EFEMStep.Finish`：整個 Carrier 完成後再送一次（多餘，此時 `sram.RunningPJ` 已在 `ProcessJobCompleted` 流程中被清為 `""`）
* **修正方式 (`AutoRunEFEM.cs`)**：
  1. 移除 `EFEMStep.Finish` 中的 CEID 152 發送區塊，僅保留註解說明已移至 `WaferGetFormStage1`。
  2. 保留 `m_SecsgemType == 0` 的 `MeasureResultSend` 邏輯不受影響。

### 階段十一：PJ Aborting Report 發送失敗修正 (2026-03-06)
* **問題描述**：收到 Host 發出的 `ABORT` 命令後，Host 模擬軟體提示未收到對應的狀態變更報告。
* **根因**：機台設定檔 `LinkEventReport.txt` 缺失 `CEID 53 (PJ_ExecutingToAborting)` 與其他 PJ 狀態事件 (CEID 47-55) 的 RPID 綁定。雖然程式碼有執行 `EventReportSend(53)`，但 GEM 控制器不知道該發送哪個 Report 內容。
* **修正方式 (`LinkEventReport.txt`)**：
  1. 將 CEID 47 (PJ_ProcessCompleteToComplete) 到 CEID 55 (PAUSING/PAUSED/STOPPING/ABORTING 等所有相關狀態事件) 全部手動綁定到 **RPID 36** (PJ 狀態報告內容)。
  2. 此舉確保了所有 PJ 狀態轉換事件都能符合 SEMI E40 標準並成功上報給 Host。

### 階段十二：Aborting 事件 DVID 4160 (完成晶圓數) 空值報錯修復 (2026-03-06)
* **問題描述**：Host 收到 Aborting 事件後報錯 `PJProcessedWaferCount_CompletedNormally cannot be empty`。
* **根因**：RPID 36 報告中包含 **DVID 4160** (正常量測完成晶圓數量)，原先程式碼僅在 PJ 正常流程結束 (`PROCESS_COMPLETE`) 時才會更新此變數。當 Host 中斷 Job (`Abort`) 時，該變數為空，導致 Report 內容驗證失敗。
* **修正方式 (`AutoRunEFEM.cs`, `SECSListening.cs`)**：
  1. **主動更新流程 (`AutoRunEFEM.cs`)**：在狀態輪詢偵測到 `Stop` 或 `Abort` 標記、準備切換 PJ 狀態之前，主動遍歷 LoadPort 的 Slot 狀態，計算目前已完成 (`ProcessEnd`) 的 Wafer 數量，並執行 `UpdateSV(4160, ...)`。
  2. **主動更新指令處理器 (`SECSListening.cs`)**：在接收到遠端 `ABORT` 指令的當下，於切換 Job 狀態前，同樣執行累計計算並第一時間更新 DVID 4160。
  * **結果**：這確保了不論是正常完成或被中斷抽離，送出的狀態報告 DVID 4160 始終具有有效的數值。

### 階段十三：Control Job Completed Report 事件綁定缺失 (2026-03-07)
* **問題描述**：Host 在 ControlJob Abort/Stop 流程中，Step 9 (Control Job Completed Report) 始終無法收到資料。
* **根因**：機台設定檔 `LinkEventReport.txt` 中完全缺少 **CEID 100** (CJ 正常完成)、**CEID 101** (CJ Stop 完成)、**CEID 102** (CJ Abort 完成)、**CEID 103** (CJ 刪除完成) 的 RPID 綁定。雖然程式碼有呼叫 `EventReportSend(101/102/103)`，但 GEM 控制器不知道該附帶哪個 Report，導致 Host 收到的是空事件。
* **修正方式 (`LinkEventReport.txt`)**：
  1. 在設定檔末尾加入以下綁定（格式為 `CEID: [ID]` 換行 ` [RPID]`）：
     - `CEID: 100` → `RPID 40` (Control Job 資料報告)
     - `CEID: 101` → `RPID 40`
     - `CEID: 102` → `RPID 40`
     - `CEID: 103` → `RPID 40`
  2. 同步更新所有 Profile 路徑下的 `LinkEventReport.txt`。

### 階段十四：CJSTOP/CJABORT 同步中斷 Process Job (2026-03-07)
* **問題描述**：Host 下達 `CJABORT` 後，機台遲遲不發出 CJ Completed 事件，因為正在跑的 PJ 沒有被同步中斷。
* **根因**：`SECSListening.cs` 在收到 `CJSTOP` 或 `CJABORT` 指令時，只設定了 `Flag.CJStopFlag` / `Flag.CJAbortFlag`，但沒有同時設定 `Flag.PJStopFlag` / `Flag.PJAbortFlag`。導致 PJ 繼續跑完整個流程後，CJ 才會結束。
* **修正方式 (`SECSListening.cs`)**：
  1. 在 `CJSTOP` 的 `dataList[3] == "1"` (ACTION=1, 連帶停止) 分支中，加入 `Flag.PJStopFlag = true;`。
  2. 在 `CJABORT` 的 `dataList[3] == "1"` 分支中，加入 `Flag.PJAbortFlag = true;`。
  * **結果**：收到 CJ 中止指令後，PJ 也會跟著立刻進入中止流程，加速整個結束過程。

### 階段十五：Enabled Alarm List Total Count 為 0 修正 (2026-03-08)
* **問題描述**：Host 測試軟體提示 `Enabled Alarm List total count is 0`，代表機台回報沒有任何已啟用的 Alarm。
* **根因**：設定檔 `HostDisableAlarm.csv` 中列出了**全部 930 個 Alarm ID**，導致 DIASECSGEM 在 `Initialize` 時將所有 Alarm 標記為 Disabled。Host 透過 S5F7 查詢 Enabled Alarm List 時回傳數量為 0。
* **修正方式 (`HostDisableAlarm.csv`)**：
  1. 清空 `HostDisableAlarm.csv` 內容，僅保留標題行 `//ID`。
  2. 這讓所有在 `EqpAlarm.csv` 中定義的 930 個 Alarm 恢復為 **Enabled** 預設狀態。

### 階段十六：S1F22 Data Variable UNITS 欄位缺失 (2026-03-08)
* **問題描述**：Host 測試軟體提示 `Equipment can report the data variable unit (UNITS) in S1,F22`（紅燈），代表機台回覆 S1F22 時不包含 UNITS 資訊。
* **根因**：設定檔 `EqpDV.csv` 中所有 DV 的 **Unit** 欄位（第 5 欄）為空（兩個逗號之間無值），導致 DIASECSGEM 在組裝 S1F22 回覆時省略 UNITS 項目。
* **修正方式 (`EqpDV.csv`)**：
  1. 將所有 122 個 DV 的 Unit 欄位統一填入 `N/A`。
  2. 格式範例：`EQUIPMENTSTATUS,1998,UINT_1,,N/A,Identifier for the equipment status,1`

### 階段十七：Recipe 刪除未同步磁碟檔案 (2026-03-08)
* **問題描述**：Host 透過 S7F17 刪除 Recipe 後，PPID List Inquiry (S7F19/S7F20) 仍能查詢到被刪除的 PPID，測試軟體報錯 `PPID [xxx] found in S7F20 (deletion failed)`。
* **根因**：`PPManager.cs` 的 `DeletePPID()` 方法只從記憶體 `_DicPPBody` Dictionary 中移除 PPID，但**未刪除磁碟上的 Recipe 檔案** (`D:\FTGM1\ParameterDirectory\Recipe\{ppid}.ini`)。而 S7F19 查詢時，`ProcessProgramDirectoryQuery` 會呼叫 `_ppManager.Initial()` 重新從磁碟讀取所有 Recipe 檔案，導致已「刪除」的 PPID 又出現在清單中。
* **修正方式 (`PPManager.cs`)**：
  1. 在 `DeletePPID(string ppid)` 方法中，於 `_DicPPBody.Remove(ppid)` 之後，新增磁碟檔案刪除邏輯：檢查 `D:\FTGM1\ParameterDirectory\Recipe\{ppid}.ini` 是否存在，若存在則刪除。
  2. 以 `try-catch` 包住，避免因檔案權限問題導致整個指令失敗。

---
*備註：本文件建立於 2026-03-05，最後更新於 2026-03-08。詳實記錄為解決 TrimGap E87/E90/E40 流程無法與標準 Host 測試系統相容所作之所有核心逆向追蹤與修改作業，供後續開發與軟體佈署交接參考。*

