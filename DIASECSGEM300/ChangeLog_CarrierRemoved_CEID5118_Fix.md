# 變更紀錄：ToolSoftwareTester Step 25 Carrier Removed Event 修復 (CEID 5118)

本文件記錄了在進行自動化主機 (Host Simulator / ToolSoftwareTester) 「卸載流程 (Unloading Scenario)」Step 25 "Carrier Removed Event" 測試時，機台未正確發送 CEID 5118 (CarrierUnloadComplete) 導致 Host 判定紅燈的問題，以及所有修正內容。

---

## 問題描述

ToolSoftwareTester 執行 Unloading Scenario 時，Step 25 "Carrier Removed Event" 顯示為紅燈 (HOST 未收到預期事件)。

### 根本原因分析

1. **CEID 5118 (CarrierUnloadComplete) 從未定義也從未發送**：`SECSGEM\EqpID.cs` 中缺少此常數定義，因此 `AutoRunEFEM.cs` 的 `WatchMaterialRemove()` 和 `Form2.cs` 的模擬按鈕都沒有發送該事件。
2. **模擬模式下的競態條件 (Race Condition)**：`AutoRunEFEM.cs` 的 `FoupUnLoad()` 在模擬模式下會立即設定 `Placement = false`，導致背景 `WatchMaterialRemove()` 迴圈自動觸發 MaterialRemove 流程，搶先在使用者按下模擬按鈕之前就已完成，按鈕反而無事件可發。
3. **缺少 CEID 253 (AssociatedToNotAssociated)**：Carrier 移除時未發送 LoadPort Association 狀態變更事件。

---

## 修正的檔案與內容

### 檔案 1：`SECSGEM\EqpID.cs`

**修改內容**：新增 CEID 常數定義。

在 `CarrierUnDock = 5116` 之後加入：
```csharp
public const int CarrierLoadComplete = 5117;
public const int CarrierUnloadComplete = 5118;
```

---

### 檔案 2：`TrimGap\AutoRunEFEM.cs`

#### 修改點 A：`FoupUnLoad()` — 移除模擬模式自動 Placement = false

**修改前**：
```csharp
// LP1 部分
if (fram.m_simulateRun != 0) Common.EFEM.LoadPort1.Placement = false;

// LP2 部分
if (fram.m_simulateRun != 0) Common.EFEM.LoadPort2.Placement = false;
```

**修改後**：將上述兩行移除（或註解），讓模擬模式下的 Placement 狀態由使用者透過 Form2 的模擬按鈕手動控制，避免與 `WatchMaterialRemove()` 產生競態條件。

#### 修改點 B：`WatchMaterialRemove()` — 加入 CEID 5118

**修改後**：在 LP1 和 LP2 的 MaterialRemove 事件發送後（CEID 2105 之後），各加入一行：
```csharp
// LP1 區塊，在 EventReportSend(2105) 之後加入：
Common.SecsgemForm.EventReportSend(5118, out err);

// LP2 區塊，在 EventReportSend(2105) 之後加入：
Common.SecsgemForm.EventReportSend(5118, out err);
```

---

### 檔案 3：`TrimGap\Form2.cs`

#### 修改點 A：新增 `InitSimulateCarrierRemoveButtons()` 方法

在模擬模式 (`fram.m_simulateRun != 0`) 下，於 `InitUI()` 中呼叫此方法，動態建立一個 GroupBox 包含兩顆按鈕：
- **"25) Carrier Removed Event"** (`btnCarrierRemovedEvent_Click`)
- **"26) Ready To Load Event"** (`btnReadyToLoadEvent_Click`)

#### 修改點 B：`btnCarrierRemovedEvent_Click` 事件處理

完整的 Carrier Removed 模擬流程（LP1 與 LP2 各自獨立處理）：

```
事件發送順序：
1. DeleteCarrier(FoupID)
2. UpdateLoadPortAssociationState → 0 (NOT_ASSOCIATED)
3. CEID 253 (AssociatedToNotAssociated)          ← 新增
4. ReadFoupID() + UpdateSV(CarrierID)
5. CEID 2105 (MaterialRemove)
6. CEID 5118 (CarrierUnloadComplete)             ← 新增（關鍵修正）
7. PortTransferState → ReadyToLoad
8. UpdateSV(PortTransferState) + CEID ReadyToLoad
9. LEDLoad(Off) + LEDUnLoad(Off)
10. Placement = false                            ← 移至最後，防止競態條件
```

**關鍵設計**：`Placement = false` 放在最後一步執行。先將 `PortTransferState` 設為 `ReadyToLoad` 再設定 `Placement = false`，確保背景 `WatchMaterialRemove()` 迴圈不會因為偵測到 `Placement == false` 而重複觸發事件序列。

#### 修改點 C：`btnReadyToLoadEvent_Click` 事件處理

模擬強制發送 ReadyToLoad 事件，將 `PortTransferState` 設為 `ReadyToLoad` 並發送對應 CEID。根據 `LoadPort_Run` 或 `Placement` 狀態判斷對 LP1 或 LP2 操作。

#### 修改點 D：`InitUI()` 呼叫

在 `InitUI()` 中，當 `fram.m_simulateRun != 0` 時呼叫 `InitSimulateCarrierRemoveButtons()`：
```csharp
if (fram.m_simulateRun == 0)
{
    btnSimulatePlacement1.Visible = false;
    btnSimulatePlacement2.Visible = false;
}
else
{
    InitSimulateCarrierRemoveButtons();
}
```

---

## 正確的 E87 Unloading 事件流程（修正後）

```
Host 發送 CarrierOut 指令
  ↓
機台執行 Unclamp + Open Door
  ↓
PortTransferState → ReadyToUnload (CEID ReadyToUnload)
  ↓
等待 Carrier 被實體移除 (或模擬按鈕觸發)
  ↓
DeleteCarrier(FoupID)
  ↓
CEID 253 (AssociatedToNotAssociated)
  ↓
CEID 2105 (MaterialRemove)
  ↓
CEID 5118 (CarrierUnloadComplete)    ← ToolSoftwareTester Step 25 所需
  ↓
PortTransferState → ReadyToLoad (CEID ReadyToLoad)  ← Step 26
```

---

## 影響範圍

| 項目 | 說明 |
|------|------|
| 正常量測流程 | `WatchMaterialRemove()` 新增 CEID 5118，所有正常卸載流程均會正確發送 |
| 模擬模式 | 新增模擬按鈕，移除 FoupUnLoad 自動 Placement=false，由使用者手動操作 |
| 向後相容 | CEID 5118 為新增事件，不影響原有 CEID 2105 的發送邏輯 |

---

*備註：本文件建立於 2025 年，記錄為解決 ToolSoftwareTester Unloading Scenario Step 25 "Carrier Removed Event" 驗證失敗所作之修改，供後續開發與軟體佈署交接參考。*
