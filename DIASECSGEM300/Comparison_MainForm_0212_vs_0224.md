# 程式比對報告：MainForm.cs (2026-02-12 vs 2026-02-24)

本文件比對了現行工作目錄 (`0212`) 與目標備份目錄 (`0224`) 之間 `MainForm.cs` 的代碼差異。

## 主要修改項目總覽

### 1. 變數識別碼大小寫修正 (UpdateLoadPortPortID)
*   **0212 版本 (現行修復版)**：
    *   在處理 LoadPort 2 (objID == "2") 時，將 `ObjectAttribute` 中的屬性名稱由 `"PortID"` 修正為全大寫的 **`"PORTID"`**。
    *   **重要性**：此修正解決了 SECS/GEM 反射引擎無法正確綁定 LoadPort 2 屬性的問題（導致 Host 抱怨無法判斷 Port 來源）。
*   **0224 版本**：
    *   仍維持為 `"PortID"`（僅字首大寫），在標準 E87 模擬環境中會發生解析錯誤。

### 2. Slot Map 存取介面
*   **0212 版本**：
    *   增加了 `SlotMap_List` 相關屬性的宣告與內部 mapping 處理。
    *   優化了 `SetCarrierAttr_SlotMap` 方法，使其能正確將 0/1/2 狀態轉譯為 E87 標準編碼 (1/3/5)。
*   **0224 版本**：
    *   此部分的實作較為分散，且 Slot Map 的狀態更新邏輯未與最新的 Carrier 物件模型完全同步。

### 3. LoadPort 初始化與屬性更新
*   **0212 版本**：
    *   包含了 `UpdateLoadPortAssociationState` 的實作，支援對外呼叫以手動切換 `ASSOCIATED` 狀態。
*   **0224 版本**：
    *   部分 LoadPort 屬性的屬性 ID (如 2016, 2017) 與現行使用的規範略有差異，或是在更新物件 (UpdateObject) 時的 Attribute 封裝方式不同。

---
**結論**：現行 `0212` 版本針對 Host 測試軟體所回饋的細節錯誤（特別是大小寫敏感的屬性名）進行了精確的調校；`0224` 版本雖較新，但在解決「Unable to determine port」這類底層通訊 Bug 上尚未包含最新的修復。
