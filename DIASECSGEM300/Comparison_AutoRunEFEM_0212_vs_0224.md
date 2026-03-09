# 程式比對報告：AutoRunEFEM.cs (2026-02-12 vs 2026-02-24)

本文件比對了現行工作目錄 (`0212`) 與目標備份目錄 (`0224`) 之間 `AutoRunEFEM.cs` 的代碼差異。

## 主要修改項目總覽

### 1. Carrier 載入與事件觸發時序 (WatchMaterialRecive)
*   **0212 版本 (現行修復版)**：
    *   **提前建立物件**：在發送任何 Placement 事件前，先呼叫 `Common.SecsgemForm.CreateCarrier()`。
    *   **狀態更新**：呼叫 `Common.SecsgemForm.UpdateLoadPortAssociationState("1", 1)` 將 LoadPort 設為 `ASSOCIATED`。
    *   **正確事件 ID**：使用 **CEID 229** (`ReadyToLoadToTransferBlocked`) 作為放置事件。
    *   **延遲與讀碼**：使用 `SpinWait` 二秒後才執行 `ClampAndReadID()`。
*   **0224 版本**：
    *   **新增事件**：在 `MaterialReceived` 之後，額外發送了 `CarrierDock` (1004) 與 `CarrierDoorOpened` (1005) 事件（標註日期 20260224）。
    *   **缺少 E87 修復**：未包含提前建立 Carrier 物件與 `ASSOCIATED` 狀態的邏輯。
    *   **事件 ID**：仍使用舊有的事件組合，未針對 E87 模擬器的放置檢查進行優化。

### 2. 資料變量更新邏輯
*   **現行版本 (`0212`)**：針對 LoadPort 1 與 2 增加了 `TrimGap_EqpID.Loadport1_PortID` 的更新，確保回報時變數完整。
*   **0224 版本**：僅保留最基本的 `PortID` 與 `CarrierID` 更新，未針對多 Port 驗證進行補強。

### 3. 未來擴充性
*   現行版本已整合 E87 標準的 `CMS_PORTID (462)` 更新邏輯（於 `ClampAndReadID` 中），目標版本 (`0224`) 則未見此部分，可能在嚴格測試下仍會報錯「Unable to determine the port」。

---
**結論**：`0224` 版本包含了 2 月 24 日針對 Dock/DoorOpened 事件的補正，但缺少了我們近期為了解決 Host Simulator 「Not Receive」與「PortID 無法辨識」所作的 E87 標準化核心修正。
