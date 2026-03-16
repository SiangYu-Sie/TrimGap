# TrimGap 系統規格文件

> 文件版本：1.0  
> 建立日期：2026-03-16  
> 設備編號：AP6 / AP6II / N2（竹南 AP6）

---

## 目錄

1. [系統概述](#1-系統概述)
2. [硬體架構](#2-硬體架構)
3. [軟體模組架構](#3-軟體模組架構)
4. [機台型號與硬體配置](#4-機台型號與硬體配置)
5. [量測模式說明](#5-量測模式說明)
6. [量測流程（AutoRun 狀態機）](#6-量測流程autorun-狀態機)
   - 6.1 [主流程（AutoStep）](#61-主流程autostep)
   - 6.2 [TrimGap 量測流程（TrimStep）](#62-trimgap-量測流程trimstep)
   - 6.3 [HTW 量測流程（HTWStep）](#63-htw-量測流程htwstep)
   - 6.4 [BlueTape 量測流程（BluetapeStep）](#64-bluetape-量測流程bluetapestep)
   - 6.5 [TTV 量測流程（TTVStep）](#65-ttv-量測流程ttvstep)
   - 6.6 [RecordCCD 流程（RecordCCDStep）](#66-recordccd-流程recordccdstep)
7. [Recipe 參數規格](#7-recipe-參數規格)
8. [系統參數（fram）規格](#8-系統參數fram規格)
9. [SECS/GEM 介面規格](#9-secsgem-介面規格)
10. [相依 DLL 與參考檔案清單](#10-相依-dll-與參考檔案清單)
11. [資料儲存與 Log 規格](#11-資料儲存與-log-規格)
12. [異常處理](#12-異常處理)
13. [修改歷程](#13-修改歷程)

---

## 目錄（更新）

1. [系統概述](#1-系統概述)
2. [硬體架構](#2-硬體架構)
3. [軟體模組架構](#3-軟體模組架構)
4. [機台型號與硬體配置](#4-機台型號與硬體配置)
5. [量測模式說明](#5-量測模式說明)
6. [量測流程（AutoRun 狀態機）](#6-量測流程autorun-狀態機)
7. [Recipe 參數規格](#7-recipe-參數規格)
8. [系統參數（fram）規格](#8-系統參數fram規格)
9. [SECS/GEM 介面規格](#9-secsgem-介面規格)
10. [相依 DLL 與參考檔案清單](#10-相依-dll-與參考檔案清單)
11. [資料儲存與 Log 規格](#11-資料儲存與-log-規格)
12. [異常處理](#12-異常處理)
13. [修改歷程](#13-修改歷程)

---

## 1. 系統概述

TrimGap 是一套晶圓邊緣量測系統，用於量測晶圓 TrimGap（切削缺口深度/寬度）、BlueTape（藍膜厚度）以及 TTV（Total Thickness Variation，整片厚度變異）。系統透過 SECS/GEM 介面與 HOST（TSMC）整合，支援全自動連續量測。

**主要功能：**
- 多種量測模式（TrimGap、HTW、BlueTape、TTV、CCD記錄）
- 多 Slot 連續量測（最多 25 Slot）
- SECS/GEM GEM300 標準介面（支援 PP 管理、CJ/PJ、Carrier/Lot 管理）
- Recipe 管理（本機儲存、HOST 上傳/下載）
- SQLite 資料庫存檔
- CCD 影像記錄（藍膜 Z 向 / Wafer 正向）

---

## 2. 硬體架構

```
┌─────────────────────────────────────────────────────────────┐
│                        TrimGap 系統                          │
│                                                             │
│  ┌──────────┐  ┌──────────┐  ┌──────────┐  ┌───────────┐  │
│  │  ETEL    │  │  Keyence │  │  HTW     │  │  Otsuka   │  │
│  │ 運動控制  │  │ LJX8000A │  │ Confocal │  │   SF3     │  │
│  │ (AP6II/N2)│  │ 雷射位移計│  │  感測器  │  │ (N2 機型) │  │
│  └──────────┘  └──────────┘  └──────────┘  └───────────┘  │
│                                                             │
│  ┌──────────┐  ┌──────────┐  ┌──────────┐  ┌───────────┐  │
│  │  SSC     │  │  Basler  │  │  Hirata  │  │  光源控制  │  │
│  │ 運動控制  │  │  Camera  │  │  EFEM    │  │ 控制器    │  │
│  │ (AP6)    │  │ CCD 相機 │  │  (FOUP)  │  │           │  │
│  └──────────┘  └──────────┘  └──────────┘  └───────────┘  │
│                                                             │
│  ┌──────────────────────────────────────────────────────┐  │
│  │              SECS/GEM (GEM300) 通訊                   │  │
│  │           創界 or 台達 SECS/GEM Library               │  │
│  └──────────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
```

### 主要感測器

| 感測器 | 型號 | 用途 | 適用機型 |
|--------|------|------|---------|
| 雷射位移計 | Keyence LJX8000A | TrimGap 輪廓掃描 | 全機型 |
| 共焦感測器 | HTW | 晶圓 Z 向高度（三角點量測） | AP6II |
| 光纖共焦 | Otsuka SF3 | Z 向對焦/高度量測 | N2 |
| CCD 相機 | Basler | BlueTape/晶圓外觀記錄 | AP6 / AP6II |
| Precitec | 光學感測器 | 備用 Z 向感測 | 部分機型 |

### 運動軸

| 軸名稱 | 說明 | 適用機型 |
|--------|------|---------|
| DD（Theta） | 晶圓旋轉軸 | 全機型 |
| X | X 方向平移 | 全機型 |
| Y | Y 方向平移 | 全機型 |
| Z1 | 感測器 Z 軸（LJ） | 全機型 |
| Z3 | HTW 感測器 Z 軸 | AP6II |
| Z2 | CCD Z 軸 | AP6II |
| Cylinder（氣缸） | 頂升晶圓 | AP6（平坦台面型） |

---

## 3. 軟體模組架構

```
TrimGap.exe
├── MainForm / Form1          主畫面 UI
├── AutoRunStage.cs           自動量測狀態機（核心流程）
├── AutoRunEFEM.cs            EFEM 自動搬送流程
├── RecipeManagement2.cs      Recipe 管理畫面
├── SecsGemInterface.cs       SECS/GEM 介面封裝
├── ParamFile.cs              參數 INI 檔讀寫
├── Parameter.cs              全域參數定義（fram/sram）
├── Common.cs                 系統初始化/共用物件管理
├── MatlabAnalysis.cs         數據分析（PMLcmpDll.dll）
├── InsertLog.cs              資料庫 Log 寫入
├── IO.cs                     I/O 控制（DI/DO）
├── Mo.cs / MotionControl.cs  運動控制
├── LJ.cs                     Keyence LJ 感測器控制
├── ETEL.cs                   ETEL 驅動器控制
├── HirataEFEM.cs             Hirata EFEM 控制
├── SignalAnalysisForm.cs     訊號分析工具視窗
└── SimulateMeasureFlow.cs    模擬模式

DIASECSGEM300.dll / Library
├── MainForm.cs               SECS/GEM 主畫面
├── PPManager.cs              PP (Process Program) 管理
└── (GEM300 Library)          台達 SECS/GEM 標準庫

分析引擎
└── PMLcmpDll.dll             TrimGap/BlueTape/TTV 數據分析演算法
```

---

## 4. 機台型號與硬體配置

| 參數 | AP6 | N2 | AP6II（本機） |
|------|-----|----|-------------|
| `m_MachineType` | 0 | 1 | **2** |
| 運動控制 | SSC | ETEL | **ETEL** |
| 雷射位移計 (LJ) | ✅ | ✅ | ✅ |
| HTW Confocal | ❌ | ❌ | **✅** |
| Otsuka SF3 | ❌ | ✅ | ❌ |
| CCD 相機 | 藍膜Z向 | ❌ | **藍膜Z向+正向記錄** |
| PT 感測器 | ❌ | ✅ | ❌ |
| Wafer Stage | 平坦+氣缸 | 凹槽+牙叉 | **平坦+氣缸** |
| SECS/GEM | 創界 or 台達 | 台達 | **台達** |

---

## 5. 量測模式說明

Recipe `Type` 欄位決定量測模式：

| Type 值 | 模式名稱 | 說明 |
|---------|---------|------|
| 0 | BlueTape | 藍膜厚度量測（CCD 量測） |
| 1 | Trim1step | TrimGap 單刀量測（LJ 雷射） |
| 2 | Trim2step | TrimGap 雙刀量測第一刀 |
| 3 | Trim2step2nd | TrimGap 雙刀量測第二刀 |
| 4 | Trim2step3rd | TrimGap 雙刀量測第三刀 |
| 5 | RecordCCD | CCD 記錄模式（不量測、只拍照） |
| 6 | Trim1step2nd | TrimGap 單刀第二刀 |
| 7 | Trim1step3rd | TrimGap 單刀第三刀 |
| 8 | TTV | 整片厚度變異量測 |

> **注意：** BlueTape、TrimGap 類型使用 **HTWMode** 流程（含 HTW Confocal AutoFocus）；TTV 使用 **TTVMode** 流程；BlueTape 使用 **BlueTapeMode** 流程。

---

## 6. 量測流程（AutoRun 狀態機）

系統採用**狀態機（State Machine）**設計，以 `BackgroundWorker` 定期輪詢執行。

### 6.1 主流程（AutoStep）

```
[GotoSwitchWaferPos]
      │  等所有軸歸位
      ▼
[WaitWaferPresence]
      │  偵測 Wafer 到位訊號（DI）
      │  或等待 SECS PP_SELECT 命令
      ▼
[VacuumOn]
      │  開真空吸附晶圓
      ▼
[CylinderDown]  ← 僅 AP6（平坦台面型）
      │  氣缸下降，放置晶圓到量測台
      ▼
[GetSlotInfo]
      │  讀取 FOUP Slot 資訊
      │  根據 Recipe.Type 分派至對應量測模式
      ├──→ [TrimMode]       → TrimGap 量測（Type=1~4,6,7）
      ├──→ [TrimMode2nd]    → TrimGap 第二次量測
      ├──→ [HTWMode]        → HTW 量測（Type=0 BlueTape / Trim）
      ├──→ [BlueTapeMode]   → BlueTape 量測（Type=0）
      ├──→ [RecordCCDMode]  → CCD 記錄（Type=5）
      └──→ [TTVMode]        → TTV 量測（Type=8）
      
[量測完成]
      │
      ▼
[JudgeSlotCount]
      │  判斷是否還有下一個 Slot 需要量測
      │  若有 → 回到 WaitWaferPresence
      ▼
[SecsGemUpdate]
      │  上傳量測結果至 HOST（SECS S6F11 Event Report）
      ▼
[VacuumOff]
      │  破真空
      ▼
[CylinderUp]  ← 僅 AP6
      │
      ▼
[GotoWaitGetHomePos]
      │  回到等待換片位置
      ▼
[Stop / 結束]
```

---

### 6.2 TrimGap 量測流程（TrimStep）

適用於 Type=1~4,6,7（TrimGap 系列）

```
[LJHome]
    │  LJ 感測器回 Home 位置（Z 軸定位）
    ▼
[DDRotate]
    │  晶圓旋轉至預設角度（Angle[n]）
    ▼
[Measurement]
    │  啟動 LJX8000A 掃描
    │  設定掃描參數（StartTrigger, Encoder）
    ▼
[Download]
    │  讀取 LJ 掃描資料
    │  取得 ProfileData 輪廓資料陣列
    ▼
[JudgeRotateCount]
    │  判斷是否完成所有角度量測
    │  若未完成 → 回到 DDRotate
    ▼
[Analysis]
    │  呼叫 PMLcmpDll.dll 進行數據分析
    │  計算 H1, H2, W1, W2
    │  套用 Recipe H1/H2/W1/W2 係數
    ▼
[Savelog]
    │  儲存量測結果至 SQLite 資料庫
    ▼
[UpdateSecs]
    │  上傳量測結果至 SECS HOST
    ▼
[Finish]
```

---

### 6.3 HTW 量測流程（HTWStep）

適用於 AP6II 機型，使用 HTW Confocal 感測器進行高度量測。

```
[HTWHome]
    │  HTW 感測器及 X 軸回 Home 位置
    ▼
[HTWAutoFocus]
    │  ┌─────────────────────────────────────────────┐
    │  │  依 is_HTW 選擇 AutoFocus 方式：             │
    │  │                                             │
    │  │  [is_HTW != 0] 舊 Sensor（Confocal）         │
    │  │    → SetTriggerMode(1)                       │
    │  │    → 移動 X 至 HTW_P1_X                      │
    │  │    → 讀取 getData() 取得 Z 高度值(value)     │
    │  │    → Z3 軸移至 value (最多重試 3 次)          │
    │  │                                             │
    │  │  [is_HTW == 0] 新 Sensor（NewSensor）        │
    │  │    → SetTriggerMode(0)                       │
    │  │    → StartTrigger2(1, 48020, 48020, 0)       │
    │  │    → SpinWait 1000ms                         │
    │  │    → getData() 取得 value                    │
    │  │    → Z3 軸移至 value + fram.Fixed_value      │
    │  │      (Fixed_value：Z軸補正偏移量)            │
    │  │    → 最多重試 3 次，失敗觸發 Alarm            │
    │  └─────────────────────────────────────────────┘
    ▼
[DDRotate]
    │  晶圓旋轉至預設角度（Angle[n]）
    ▼
[Measurement]
    │  啟動 HTW 量測（舊 Sensor）
    ▼
[MoveToStart_New_Sensor]
    │  移至新 Sensor 起始掃描位置（新 Sensor 路徑）
    ▼
[Measurement_New_Sensor]
    │  啟動新 Sensor 掃描
    ▼
[Download]
    │  讀取量測資料（第一次）
    ▼
[Measurement2]
    │  第二次 HTW 量測（可選）
    ▼
[Download2]
    │  讀取量測資料（第二次）
    ▼
[JudgeRotateCount]
    │  判斷是否完成所有角度量測
    │  若未完成 → 回到 DDRotate
    ▼
[Analysis]
    │  數據分析
    ▼
[Savelog]
    │  儲存 Log
    │  記錄 ReportData.HTW_Focus_Z（對焦位置）
    ▼
[UpdateSecs]
    │  上傳 HOST
    ▼
[Finish]
```

---

### 6.4 BlueTape 量測流程（BluetapeStep）

使用 Basler CCD 相機進行藍膜厚度量測。

```
[BlueTapeStart]
    │  設定 CCD 相機參數、光源控制器
    ▼
[CCDHome]
    │  CCD Z 軸回 Home
    ▼
[DDRotate]
    │  晶圓旋轉至預設角度
    ▼
[Measurement]
    │  CCD 拍照取像
    │  計算藍膜厚度（影像處理）
    ▼
[Download]
    │  讀取影像結果
    ▼
[JudgeRotateCount]
    │  判斷是否完成所有角度
    ▼
[Analysis]
    │  藍膜厚度分析（PMLcmpDll）
    │  與 BlueTapeThreshold 比對
    ▼
[Savelog] → [UpdateSecs] → [Finish]
```

---

### 6.5 TTV 量測流程（TTVStep）

使用 LJX8000A 掃描整片晶圓厚度（需 TTV 掃描路徑 `.tvr`）。

```
[TTVStart]
    │  載入 TTV 掃描路徑檔（MotionPatternName）
    ▼
[TTVHome]
    │  所有軸回 Home
    ▼
[TTVHomeDone]
    ▼
[DDRotate]
    │  晶圓旋轉至 TTVrotatePosition[n]
    ▼
[DDRotateDone]
    ▼
[ShiftPosition]
    │  X/Y 平移至 TTVshiftPosition[n]
    ▼
[ShiftPositionDone]
    ▼
[Measurement]
    │  LJ 掃描量測
    ▼
[JudgeShiftCount]
    │  判斷平移次數（同一旋轉角下掃描多條線）
    ├──→ 未完成 → ShiftPosition
    ▼
[JudgeRotateCount]
    │  判斷旋轉角度次數
    ├──→ 未完成 → DDRotate
    ▼
[Analysis] → [Savelog] → [Finish]
```

---

### 6.6 RecordCCD 流程（RecordCCDStep）

僅用於拍照記錄，不進行量測計算。

```
[RecordCCDStart]
    ▼
[CCDHome]
    ▼
[DDRotate]
    ▼
[Measurement]
    │  CCD 拍照，儲存影像至指定路徑
    ▼
[Download]
    ▼
[JudgeRotateCount]
    ├──→ 可按 RecordCCDRule 決定觸發條件
    │   0: 依 Position（固定角度）
    │   1: 依 Pitch（固定間距）
    ▼
[Savelog] → [UpdateSecs] → [Finish]
```

---

## 7. Recipe 參數規格

Recipe 檔案儲存於：`D:\FTGM1\ParameterDirectory\Recipe\<RecipeName>.ini`  
（Section: `[Recipe]`）

### 基本設定

| 參數鍵值 | 型別 | 預設值 | 說明 |
|---------|------|--------|------|
| `fram.Recipe.Type` | int | 0 | 量測模式（見第5節） |
| `fram.Recipe.Rotate_Count` | int | 8 | 旋轉角度數量（4 or 8） |
| `fram.Recipe.OffsetType` | int | 0 | Offset 計算類型 |
| `fram.Recipe.RepeatTimes` | int | 1 | 重複量測次數 |
| `fram.Recipe.RepeatTimes_now` | int | 0 | 目前量測次數（執行時更新） |
| `fram.Recipe.CreateTime` | string | — | Recipe 建立時間 |
| `fram.Recipe.ReviseTime` | string | — | Recipe 最後修改時間 |

### Slot 設定（最多 25 Slot）

| 參數鍵值 | 型別 | 說明 |
|---------|------|------|
| `fram.Recipe.Slot1` ~ `Slot25` | int (0/1) | Slot 啟用（1=啟用） |

### 量測角度設定（最多 8 角度）

| 參數鍵值 | 型別 | 說明 |
|---------|------|------|
| `fram.Recipe.Angle1` ~ `Angle8` | int | 量測角度 (0~315°)，需遞增排列 |

### TrimGap 掃描範圍（單位：μm）

| 參數鍵值 | 說明 | 備註 |
|---------|------|------|
| `fram.Recipe.Step1_Range_step1x0` | 單刀掃描起點 | 預設 1 |
| `fram.Recipe.Step1_Range_step1x1` | 單刀掃描終點 | 預設 4000 |
| `fram.Recipe.Step2_Range_step1x0` | 雙刀第一刀起點 | 預設 2300 |
| `fram.Recipe.Step2_Range_step1x1` | 雙刀第一刀終點 | 預設 2900 |
| `fram.Recipe.Step2_Range_step2x0` | 雙刀第二刀起點 | 預設 1800 |
| `fram.Recipe.Step2_Range_step2x1` | 雙刀第二刀終點 | 預設 2000 |
| `fram.Recipe.Range1_Percent` | 基準範圍 (%) | 預設 5 |
| `fram.Recipe.Range2_Percent` | 基準範圍 (%) | 預設 15 |

### BlueTape 設定

| 參數鍵值 | 型別 | 說明 |
|---------|------|------|
| `fram.Recipe.BlueTapeThreshold` | int | 藍膜判斷閾值（灰階值），預設 17 |
| `fram.Recipe.WaferEdgeEvaluate` | int | Wafer 邊緣評估（0=off, 1=on） |

### 量測係數（20240628 加入）

| 參數鍵值 | 型別 | 說明 |
|---------|------|------|
| `fram.Recipe.H1` | double | H1 量測結果係數（預設 1.0） |
| `fram.Recipe.H2` | double | H2 量測結果係數（預設 1.0） |
| `fram.Recipe.W1` | double | W1 量測結果係數（預設 1.0） |
| `fram.Recipe.W2` | double | W2 量測結果係數（預設 1.0） |

> **說明：** 最終結果 = 原始量測值 × 係數。用於機台間一致性校正。

### 數值上下限判斷（20250815 加入）

| 參數鍵值 | 說明 |
|---------|------|
| `fram.Recipe.LimitMethod` | 0=關閉；1=啟用上下限判斷 |
| `fram.Recipe.H1_LowerLimit` / `H1_UpperLimit` | H1 下限 / 上限 |
| `fram.Recipe.H2_LowerLimit` / `H2_UpperLimit` | H2 下限 / 上限 |
| `fram.Recipe.W1_LowerLimit` / `W1_UpperLimit` | W1 下限 / 上限 |
| `fram.Recipe.W2_LowerLimit` / `W2_UpperLimit` | W2 下限 / 上限 |

### CCD 拍照設定

| 參數鍵值 | 說明 |
|---------|------|
| `fram.Recipe.RecordCCDRule` | 0=依角度位置（Position） 1=依角度間距（Pitch） |
| `fram.Recipe.RecordCCD_Angle_Start` | 起始角度，預設 0° |
| `fram.Recipe.RecordCCD_Angle_End` | 結束角度，預設 315° |
| `fram.Recipe.RecordCCD_Angle_Pitch` | 角度間距，預設 45° |
| `fram.Recipe.RecordAfterMeasure` | 1=量測後拍照 |

### LJ 量測輔助設定

| 參數鍵值 | 說明 |
|---------|------|
| `fram.Recipe.LJ_Flat` | LJ 量測平面參考高度（mm），預設 1.5 |
| `fram.Recipe.RD_LJ` | 0=正常；1=讀取已有資料（不重新掃描） |
| `fram.Recipe.Analysis_method` | 0=預設；1=備用分析方法 |

### TTV 掃描路徑

| 參數鍵值 | 說明 |
|---------|------|
| `fram.Recipe.MotionPatternName` | TTV 掃描路徑檔名（.tvr） |
| `fram.Recipe.MotionPatternPath` | TTV 掃描路徑資料夾 |

### SF3 設定（N2 機型）

| 參數鍵值 | 說明 |
|---------|------|
| `fram.Recipe.SF3_ID` | SF3 Recipe ID |
| `fram.Recipe.SF3_Name` | SF3 Recipe 名稱 |

---

## 8. 系統參數（fram）規格

系統參數儲存於：`D:\FTGM1\ParameterDirectory\param\param.ini`

### 機台型號設定

| 參數 | 說明 |
|------|------|
| `m_MachineType` | 0=AP6, 1=N2, 2=AP6II |
| `m_simulateRun` | 0=實機；1=模擬模式 |
| `m_SecsgemType` | 0=創界；1=台達 GEM300 |
| `m_EFEMbypass` | 1=bypass EFEM（不連接 EFEM） |
| `m_DEMOMode` | 1=展覽循環模式 |
| `m_NG_RecordCCD` | 1=NG 時自動拍照 |

### Wafer 對位設定

| 參數 | 說明 |
|------|------|
| `m_WaferAlignAngle` | Wafer 對位角度（度） |
| `m_WaferBackToFoupAngle` | Wafer 回 FOUP 的偏轉角，最終角度 = AlignAngle + BackToFoupAngle |

### AutoFocus 參數（HTW / AP6II）

| 參數 | 說明 |
|------|------|
| `HTW_Autofocus_Index` | AutoFocus 掃描起始索引 |
| `HTW_Autofocus_Index_Last_Used` | 上次成功對焦的索引（預留上次成功值加速下次對焦） |
| `Fixed_value` | **新 Sensor AutoFocus Z 軸固定補正值（20260313 加入）** |

> `Fixed_value` 在 ParaForm → Page2 → PT_2 分組內設定

### 位置參數（Position 結構）

| 參數 | 說明 |
|------|------|
| `HTW_P1_X` | HTW 感測器量測位置 X |
| `RecordCCD_Z` | CCD 拍照 Z 軸高度 |
| （其他位置參數） | 各量測位置的 X/Y/Z/Theta 座標 |

---

## 9. SECS/GEM 介面規格

使用 **台達 DIASECSGEM300** Library（GEM300 標準）。

### 支援的 Remote Command

| Command | 說明 |
|---------|------|
| `GO` | 設備啟動量測 |
| `STOP` | 停止量測 |
| `GO-LOCAL` | 切換為 Local 模式 |
| `GO-REMOTE` | 切換為 Remote 模式 |
| `PP-SELECT` | 選擇 Process Program（Recipe） |
| `MEASURE-L` / `MEASURE-L-C` | 量測命令 |
| `SLOTMAP-L` / `SLOTMAP-L-C` | Slot Map 回報 |
| `RELEASE-L` / `RELEASE-L-C` | 釋放 LoadPort |
| `CANCEL-L` / `CANCEL-L-C` | 取消 |
| `TRANSFER` | 傳送命令 |
| `ACCESSMODE-ASK` / `ACCESSMODE-CHANGE` | 存取模式切換 |
| `PORT-TRANSFERSTATUS-ASK` | LoadPort 傳輸狀態查詢 |
| `START` | 量測開始 |

### SECS 旗標（SecsGemInterface 屬性）

| 屬性 | 說明 |
|------|------|
| `bWaitSECS_PP_SELECT` | 等待 HOST PP-SELECT 命令 |
| `bWaitSECS_SlotMapCmd` | 等待 SlotMap 命令 |
| `bWaitSECS_MeasureCmd` | 等待量測命令 |
| `bWaitSECS_ReleaseCmd` | 等待 Release 命令 |
| `bWaitSECS_CancelCmd` | 等待 Cancel 命令 |
| `bSECS_ReadyToLoad_LP1/LP2` | LoadPort 1/2 準備就緒 |
| `bSECS_ChangeAccessMode_Recive` | 已收到 Access Mode 變更 |
| `bWaitSECS_StartCmd` | 等待 START 命令 |
| `bWaitSECS_StopCmd` | 等待 STOP 命令 |
| `bWaitSECS_ACCESSMODE_ASK` | 等待 Access Mode 詢問 |
| `bWaitSECS_PORT_TRANSFERSTATUS_ASK` | 等待 Port 傳輸狀態詢問 |

### PP（Process Program）管理

PP 由 `PPManager` 類別管理，從 Recipe 目錄自動載入。

**PPBody 封包欄位（上傳至 HOST）：**

| 欄位 | 型別 | 說明 |
|------|------|------|
| Rotate_Count | U1 | 旋轉角度數 |
| Type | U1 | 量測模式 |
| RepeatTimes | U1 | 重複次數 |
| RepeatTimes_now | U1 | 目前重複次數 |
| Slot1~Slot25 | U1×25 | Slot 啟用狀態 |
| Angle1~Angle8 | U2×8 | 量測角度 |
| CreateTime | A | 建立時間 |
| ReviseTime | A | 修改時間 |
| OffsetType | U1 | Offset 類型 |
| LJStdSurface | U1 | Wafer 邊緣評估 |
| BTTH | U2 | BlueTape 閾值 |
| S1_1x0/x1 | U2×2 | 單刀掃描範圍 |
| S2_1x0/x1, S2_2x0/x1 | U2×4 | 雙刀掃描範圍 |
| Range1/Range2 | U1×2 | 基準範圍百分比 |

> ⚠️ **已知不足：** H1/H2/W1/W2 係數、LimitMethod 及上下限值、Analysis_method、LJ_Flat、RecordCCD 設定、TTV MotionPattern 等欄位**尚未納入 PP 上傳**，HOST 無法取得這些資訊。

---

## 10. 相依 DLL 與參考檔案清單

> **編譯環境：** .NET Framework 4.8、Visual Studio、Platform x64  
> **輸出路徑：** `TrimGap\bin\x64\Debug\`

---

### 10.1 專案自行開發的 DLL（需先建置對應子專案）

| DLL 名稱 | 來源專案路徑 | 說明 |
|---------|------------|------|
| `Camera.dll` | `..\Camera\bin\Debug\` | Basler CCD 相機封裝（Basler Pylon SDK） |
| `LJX8000A.dll` | `..\LJX8000A\bin\x64\Debug\` | Keyence LJX8000A 雷射位移計封裝 |
| `DiaGemLib_Equipment.dll` | `..\DIASECSGEM300\bin\Debug\` | 台達 SECS/GEM GEM300 Equipment 封裝 |
| `DIASECSGEM.dll` | `..\DIASECSGEM300\bin\Debug\` | 台達 SECS/GEM 核心庫 |
| `GEMDataModel.dll` | `..\DIASECSGEM300\bin\Debug\` | 台達 GEM300 資料模型 |
| `Modules.dll` | `..\Modules\Modules\bin\x64\Debug\` | 通用模組庫 |

---

### 10.2 硬體 SDK DLL（由廠商提供，需安裝對應 SDK）

#### 感測器

| DLL 名稱 | 版本/架構 | 廠商 | 說明 |
|---------|---------|------|------|
| `PMLcmpDll.dll` | v1.22.906.1, MSIL | 自研 | TrimGap/BlueTape/TTV **核心分析演算法**（量測結果計算） |
| `CSharpSingleChannelScanning.dll` | x64 | Precitec/CHRocodile | HTW 共焦感測器新 Sensor SDK（單通道掃描） |
| `CHRocodile.dll` | x64 | Precitec | CHRocodile 感測器核心驅動 |
| `KEYENCE_LJ.dll` | x64 | Keyence | Keyence LJ 系列雷射位移計舊版串流庫 |
| `LJX8_IF.dll` | x64 | Keyence | Keyence LJX8000A 串流介面庫（LJX8000A.dll 相依） |
| `SF3.dll` | — | Otsuka | Otsuka SF3 感測器 SDK（N2 機型） |

#### 運動控制

| DLL 名稱 | 版本/架構 | 廠商 | 說明 |
|---------|---------|------|------|
| `dmd40net.dll` | v4.36.128.0, AMD64 | ETEL | ETEL DMD40 驅動器 .NET 封裝 |
| `dsa40net.dll` | v4.36.128.0, AMD64 | ETEL | ETEL DSA40 驅動器 .NET 封裝 |
| `dmd40c.dll` / `dmd40cd.dll` | x64 | ETEL | ETEL DMD40 C 原生驅動 |
| `dsa40c.dll` / `dsa40cd.dll` | x64 | ETEL | ETEL DSA40 C 原生驅動 |
| `lib40c.dll` / `lib40cd.dll` | x64 | ETEL | ETEL 驅動基礎庫 |
| `tra40c.dll` / `tra40cd.dll` | x64 | ETEL | ETEL 軌道規劃庫 |
| `dex40c.dll` / `dex40cd.dll` | x64 | ETEL | ETEL DEX40 擴充模組 |
| `emp40c.dll` / `emp40cd.dll` | x64 | ETEL | ETEL EMP40 I/O 模組 |
| `esc40c.dll` / `esc40cd.dll` | x64 | ETEL | ETEL ESC40 安全控制 |
| `esd40c.dll` / `esd40cd.dll` | x64 | ETEL | ETEL ESD40 共享記憶體 |
| `etb40c.dll` / `etb40cd.dll` | x64 | ETEL | ETEL ETB40 匯流排介面 |
| `etne40c.dll` / `etne40cd.dll` | x64 | ETEL | ETEL ETne40 網路模組 |
| `ekd40c.dll` / `ekd40cd.dll` | x64 | ETEL | ETEL EKD40 Encoder 介面 |
| `assert40c.dll` / `assert40cd.dll` | x64 | ETEL | ETEL 斷言/除錯庫 |
| `Motion_PB.dll` | x64, v1.0.0.0 | — | SSC 運動控制封裝（AP6 機型） |
| `IO4.dll` | x64, v2.0.0.1 | — | I/O 控制庫（DI/DO） |
| `AdvMotAPI.dll` | x64 | Advantech | Advantech 運動控制 API |
| `mc2xxstd_x64.dll` | x64 | — | 馬達控制標準庫 |
| `wdapi1021.dll` / `wdapi1110.dll` / `wdapi1411.dll` | x64/x32 | Jungo WinDriver | 驅動程式 API（WinDriver 介面） |

#### 相機

| DLL 名稱 | 版本/架構 | 廠商 | 說明 |
|---------|---------|------|------|
| `Basler.Pylon.dll` | — | Basler | Basler 工業相機 Pylon SDK .NET 封裝 |

#### EFEM / 機械手臂

| DLL 名稱 | 版本/架構 | 廠商 | 說明 |
|---------|---------|------|------|
| `HirataEFEM.dll` | AMD64, v1.0.0.0 | Hirata | Hirata EFEM（FOUP 搬送機械手臂）控制 |

#### MELSEC PLC

| DLL 名稱 | 廠商 | 說明 |
|---------|------|------|
| `Interop.ActUtlType64Lib.dll` | Mitsubishi | MELSEC PLC 通訊（64-bit COM Interop） |
| `Interop.ActUtlTypeLib.dll` | Mitsubishi | MELSEC PLC 通訊（32-bit COM Interop） |

#### 光源控制器

| DLL 名稱 | 說明 |
|---------|------|
| `LightController.exe` | RS232/Ethernet 光源控制器獨立程序（由主程序呼叫） |

---

### 10.3 SECS/GEM 通訊 DLL

| DLL 名稱 | 版本 | 來源 | 說明 |
|---------|------|------|------|
| `DIASECSGEM.dll` | v2.2.0.0 | 台達電子 | 台達 GEM300 SECS/GEM 通訊主庫 |
| `GEMDataModel.dll` | v2.0.0.0 | 台達電子 | GEM300 資料模型 |
| `DiaGemLib_Equipment.dll` | — | 台達電子 | Equipment 端 GEM Library |
| `DIASECS.dll` | — | 台達電子 | 台達 SECS 底層庫 |
| `DIASECS_Configuration_Service.dll` | — | 台達電子 | SECS 配置服務 |
| `CTLT.GEM.dll` | v2.9.3.0, MSIL | 創界 | 創界 GEM 通訊庫（備用，`m_SecsgemType=0`） |
| `CTLT.SECS.dll` | v2.3.6.0, MSIL | 創界 | 創界 SECS 底層庫 |
| `CTLT.Interop.SECS.dll` | — | 創界 | 創界 SECS Interop |
| `SECSGEM.dll` | v1.0.0.0, MSIL | — | SECS/GEM 封裝層 |
| `HVM412_DIAAuto64_DIASECSGEM300.dll` | 64-bit | 台達電子 | GEM300 HVM412 硬體模組（64-bit） |
| `HVM412_DIAAuto_DIASECSGEM300.dll` | 32-bit | 台達電子 | GEM300 HVM412 硬體模組（32-bit） |

---

### 10.4 分析與演算法 DLL

| DLL 名稱 | 版本 | 說明 |
|---------|------|------|
| `PMLcmpDll.dll` | v1.22.906.1 | **主要分析引擎**，計算 H1/H2/W1/W2/TTV/BlueTape |
| `PMLcmpDllNative.dll` | — | PMLcmpDll 原生加速庫（C++ 核心） |
| `MWArray.dll` | v2.22.0.0 | MathWorks MATLAB Runtime 陣列封裝（PMLcmpDll 相依） |
| `RecipeCompiler.dll` | — | 掃描路徑 Recipe 編譯器（TTV .tvr 格式） |

> **注意：** `PMLcmpDll.dll` 為核心演算法，使用前須確認版本。目前版本 v1.22.906.1。歷史版本備份於 output 目錄（`PMLcmpDll_0307.dll`、`PMLcmpDll_20250328.dll` 等）。

---

### 10.5 資料庫 DLL

| DLL 名稱 | 說明 |
|---------|------|
| `SQLite.dll` | SQLite 封裝層（自封） |
| `System.Data.SQLite.dll` | System.Data.SQLite 官方庫 |
| `SQLite.Interop.dll` | SQLite 原生 Interop |

---

### 10.6 NuGet 套件（packages.config）

| 套件名稱 | 版本 | 說明 |
|---------|------|------|
| `Spire.XLS` | 15.12.0 | Excel 報表輸出（不需安裝 Office） |
| `Nito.AsyncEx`（含子套件） | 5.1.2 | 非同步工具集（Context/Coordination/Tasks/Oop） |
| `System.Reactive` | 5.0.0 | Reactive Extensions（Rx.NET） |
| `HarfBuzzSharp` | 8.3.0.1 | 文字排版引擎（UI 相依） |
| `MvvmFx-Bindings-WinForms` | 3.0.1 | WinForms MVVM 資料繫結 |
| `Microsoft.Bcl.AsyncInterfaces` | 1.0.0 | IAsyncEnumerable 等非同步介面 |
| `System.Buffers` | 4.5.1 | 記憶體 Buffer 工具 |
| `System.Collections.Immutable` | 1.7.1 | 不可變集合 |
| `System.Memory` | 4.5.5 | Span/Memory 高效記憶體操作 |
| `System.Numerics.Vectors` | 4.5.0 | SIMD 向量運算 |
| `System.Runtime.CompilerServices.Unsafe` | 6.0.0 | 低階不安全記憶體操作 |
| `System.Threading.Tasks.Extensions` | 4.5.4 | Task 非同步延伸 |
| `Nito.Cancellation` | 1.1.2 | 取消 Token 工具 |
| `Nito.Collections.Deque` | 1.1.1 | 雙向佇列 |
| `Nito.Disposables` | 2.2.1 | IDisposable 工具 |

---

### 10.7 其他工具 DLL

| DLL 名稱 | 說明 |
|---------|------|
| `SocketClient2.dll` | TCP Socket 客戶端封裝（本地通訊） |
| `CsvHelper.dll` | CSV 檔案讀寫 |
| `NPOI.dll` | Excel/POI 格式讀寫 |
| `Newtonsoft.Json.dll` | JSON 序列化/反序列化 |
| `SunnyUI.dll` / `SunnyUI.Common.dll` | 自定義 UI 控制項套件 |
| `MaterialDesignThemes.Wpf.dll` | Material Design UI 主題 |
| `MetroFramework.dll`（含 Design/Fonts） | Metro 風格 UI 框架 |
| `WinFormAnimation.dll` | WinForms 動畫效果 |
| `CircularProgressBar.dll` | 圓形進度條控制項 |
| `Rockey4ND.dll` / `Rockey4ND_X64.dll` | **加密狗驅動**（授權保護，需插 Rockey 硬體加密狗） |
| `ActivateCode.dll` | 軟體序號啟動模組 |
| `SoftLicenseAPI.dll` | 軟體授權 API |
| `ProductLicenseChecker.dll` | 產品授權檢查 |
| `DIALink.HMI.dll` | 台達 HMI 通訊介面 |
| `UniAuto.UniBCS.*.dll` | UniAuto BCS 模組（狀態機、加密等） |
| `DIAUn.exe` | 台達配置工具（獨立執行程序） |

---

### 10.8 MATLAB Runtime 需求

`PMLcmpDll.dll` 使用 MATLAB 編譯生成，執行時需安裝對應版本的 **MATLAB Compiler Runtime (MCR)**：

| 需求 | 說明 |
|------|------|
| MWArray.dll 版本 | v2.22.0.0 → 對應 **MATLAB R2022b** |
| 安裝路徑 | 需安裝 MATLAB Runtime R2022b（或對應版本） |
| 下載位置 | MathWorks 官方：`https://www.mathworks.com/products/compiler/matlab-runtime.html` |

---

### 10.9 .NET Framework 系統組件

需要 **.NET Framework 4.8**（或以上）安裝：

| 組件 | 說明 |
|------|------|
| `System.Windows.Forms` | WinForms UI 框架 |
| `System.Windows.Forms.DataVisualization` | Chart 圖表控制項 |
| `System.Drawing` | GDI+ 繪圖 |
| `System.Data` | 資料庫存取 |
| `System.DirectoryServices` | Active Directory 存取 |
| `System.Numerics` | 數學運算 |
| `WindowsBase` | WPF 基礎（部分 UI 控件相依） |

---

### 10.10 執行時期所需外部程式

| 程式 | 說明 |
|------|------|
| `MATLAB Compiler Runtime R2022b` | PMLcmpDll.dll 分析引擎執行環境 |
| `Rockey 加密狗驅動` | 授權保護，需插入 USB 硬體加密狗並安裝驅動 |
| `DIASECS_GEM.ConfigurationTool.exe` | 台達 SECS/GEM 設定工具（通訊參數設定） |
| `LightController.exe` | 光源控制器通訊程序 |
| `DIAUn.exe` | 台達 SECS/GEM 更新工具 |

---

### 10.11 重要設定檔

| 檔案 | 路徑 | 說明 |
|------|------|------|
| `param.ini` | `D:\FTGM1\ParameterDirectory\param\` | 系統主參數（fram 結構） |
| `IOparam.ini` | `TrimGap\bin\x64\Debug\` | I/O 腳位設定 |
| `Default.prm2` / `FTGM.prm2` | `TrimGap\bin\x64\Debug\` | 運動控制參數（ETEL） |
| `TrimGap.exe.config` | `TrimGap\bin\x64\Debug\` | .NET 應用程式設定（AppSettings） |
| `SECSGEM.dll.config` | `TrimGap\bin\x64\Debug\` | SECS/GEM 通訊設定 |
| `CSharpSingleChannelScanning.dll.config` | `TrimGap\bin\x64\Debug\` | CHRocodile 感測器設定 |
| `<RecipeName>.ini` | `D:\FTGM1\ParameterDirectory\Recipe\` | Recipe 參數檔 |

---

## 11. 資料儲存與 Log 規格

### 11.1 SQLite 資料庫

- 路徑：`sram.ErrorCodeCsvPath`（param.ini 設定）
- 透過 `InsertLog.SavetoDB(errorCode, message)` 寫入
- 量測結果、操作 Log、異常 Log 均存入資料庫

### 11.2 Log 類型

| ErrorCode | 說明 |
|-----------|------|
| 5 | Save Recipe（存 Recipe） |
| 9 | Change Recipe（更換 Recipe） |
| `TrimGap_EqpID.EQP_HTW_Z_TimeoutError` | HTW AutoFocus 逾時/失敗 |

### 11.3 ReportData 量測結果欄位

| 欄位 | 說明 |
|------|------|
| `HTW_Focus_Z` | HTW AutoFocus 對焦位置記錄（格式：`value(NewSensor, Fixed_value)` 或 `value(OldSensor)`） |

### 11.4 影像儲存

- BlueTape CCD 影像：儲存於設定路徑，依年/月日分資料夾
- 格式：`YYYYMMDD-HHmmss-<index>.csv` 或 `.bmp`

---

## 12. 異常處理

### HTW AutoFocus 失敗

- 最多重試 3 次（`sram.AutoFocus_Retry_Count`）
- 第 4 次失敗 → 觸發 Alarm（`EQP_HTW_Z_TimeoutError`），停止 AutoRun
- 成功後 `AutoFocus_Retry_Count` 重設為 0

### Angle 設定錯誤

- 儲存 Recipe 時驗證 Angle1 < Angle2 < ... < Angle8
- 違反時顯示錯誤訊息，禁止儲存

### Slot 設定錯誤

- 至少要選擇一個 Slot
- 全未選擇時顯示錯誤訊息，禁止儲存

### SECS/GEM 通訊異常

- 透過 `AlarmReportSend()` 上報 HOST
- 等待命令超時時，依各旗標狀態決定後續處理

---

## 13. 修改歷程

| 日期 | 說明 | 修改檔案 |
|------|------|---------|
| 2024-06-28 | 新增量測係數 H1/H2/W1/W2（Recipe） | Parameter.cs, ParamFile.cs, RecipeManagement2.cs |
| 2025-08-15 | 新增 LimitMethod 及 8 個上下限欄位（Recipe） | Parameter.cs, ParamFile.cs, RecipeManagement2.cs |
| 2026-03-13 | ParaForm 新增 Fixed_value 參數欄位（新 Sensor Z 補正） | Parameter.cs, ParamFile.cs, ParaForm.cs, ParaForm.designer.cs |
| 2026-03-12 | 換新 Sensor 做 AutoFocus 流程（is_HTW==0 路徑） | AutoRunStage.cs |
| 2026-03-13 | 修正新 Sensor AutoFocus else 分支無限迴圈問題 | AutoRunStage.cs |

---

*本文件由 GitHub Copilot 根據原始碼自動生成，建議定期與程式碼同步更新。*
