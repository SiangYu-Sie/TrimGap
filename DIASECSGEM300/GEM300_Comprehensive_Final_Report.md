# GEM300 系統修改與比對綜合報告 (2026-03)

本文件整合了近期針對 GEM300 / E87 標準化所進行的所有核心修改紀錄，以及不同版本程式碼之間的差異分析。

---

## 1. GEM300 E87 載入流程與事件順序大修復 (2026-03)

本章節記錄了為通過 Host Simulator 「載入流程 (Load Scenario)」測試所進行的所有過程。

### 階段一：Carrier ID 提早上報機制的修正
* **問題描述**：機台在 FOUP 放置當下即上報 `CarrierID`，不符 E87 讀碼前需為 Unknown 的規範。
* **修正方式**：將 `WatchMaterialRecive()` 內的 `CarrierID` 設為空字串，真正的 ID 上報延後至 `ClampAndReadID()` 夾合讀碼後。

### 階段二：Slot Map (Wafer Map) 標準化導入
* **問題描述**：原先使用盲發事件且陣列值未對齊標準。
* **修正方式**：
  1. 新增 `SetCarrierAttr_SlotMap` 方法，將 0/1/2 轉換為標準編碼（Empty=1, 3, 5, 2）。
  2. 建置 `SetCarrierStatus_SlotMap` 狀態機，驅動 `WAITING_FOR_HOST` 流程。

### 階段三：Carrier Placement 驗證失敗 (Timeout)
* **問題描述**：Host 拒絕承認放置事件。
* **修正方式**：
  1. **提早建立物件**：將 `CreateCarrier` 移至流程最前端。
  2. **關聯狀態**：第一時間將 LoadPort 設為 `ASSOCIATED` (1)。
  3. **修正 CEID**：將錯誤的 `223` 改為標準的 **`CEID 229`**。

### 階段四：Auto Clamp 無法辨識 Port 來源
* **問題描述**：Host 無法判斷事件屬於 LP1 還是 LP2。
* **修正方式**：
  1. **修正 Typo**：將屬性名 `"PortID"` 修正為全大寫的 **`"PORTID"`**。
  2. **增補 SVID 462**：在夾合事件前強制更新標準 SVID `CMS_PORTID`。

---

## 2. Control Job (CJ) 建立失敗修復紀錄

### 問題描述
遠端建立 CJ 時，本地設備無法成功建立，導致 Host Timeout。

### 主要修改項目
1. **加入 try-catch**：避免 `_gemControler_CreateObjectRequestCommand()` 發生例外時靜默失敗。
2. **ATTRID 大小寫不敏感比較**：將 `==` 改為 `OrdinalIgnoreCase` 比較，解決 Host 發送混合大小寫（如 `ProcessingCtrlSpec`）導致匹配失敗的問題。
3. **移除多餘解包邏輯**：修正對 `PROCESSINGCTRLSPEC` 內部 `ListWrapper` 結構的錯誤解析（解決 `InvalidCastException`）。
4. **診斷日誌**：加入大量 `Debug.WriteLine` 以利追蹤 CJ/PJ 的建立過程。

---

## 3. AutoRunEFEM.cs 版本比對分析 (0212 vs 0224)

### 主要差異點
* **現行版 (0212)**：包含 E87 物件預先建立、`ASSOCIATED` 狀態切換、以及精確的 **CEID 229** 邏輯。
* **備份版 (0224)**：額外新增了 `CarrierDock` (1004) 與 `CarrierDoorOpened` (1005) 事件，但缺少 0212 中的 E87 標準化核心修復。

---

## 4. MainForm.cs 版本比對分析 (0212 vs 0224)

### 主要差異點
* **現行版 (0212)**：成功修復 LoadPort 2 屬性名大小寫錯誤 (`PortID` → **`PORTID`**)，這是解決 Host 「Unable to determine port」的關鍵。
* **備份版 (0224)**：維持舊有的大小寫格式，且 Slot Map 的轉譯邏輯尚未與最新的物件模型對齊。

---
*文件彙整日期：2026-03-05*
