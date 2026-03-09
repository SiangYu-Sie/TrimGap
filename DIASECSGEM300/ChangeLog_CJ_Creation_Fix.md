# DIASECSGEM300\MainForm.cs 修改記錄

## 修改日期：2025 年

## 問題描述
遠端（ToolSoftwareTester）建立 CJ 時，本地設備無法成功建立 CJ。

---

## 修改 1：加入 try-catch 例外處理（行 4816 ~ 5206）

### 位置
`_gemControler_CreateObjectRequestCommand()` 方法

### 原因
原本 handler 沒有任何 try-catch，若發生例外會靜默失敗，不會回覆 S14F10，導致 Host 端 timeout。

### 修改內容
```csharp
// 在 foreach 外層加入 try-catch
try
{
    foreach (ObjectInstance objectEntity in e.ListObjectEntities)
    {
        // ... 原有邏輯 ...
    }
    result = _gemControler.CreateObjectRequestCommandReply(...);
}
catch (Exception ex)
{
    System.Diagnostics.Debug.WriteLine("CreateObjectRequestCommand Exception: " + ex.Message + "\n" + ex.StackTrace);
    try
    {
        bAck = 1;
        listErrorReports.Clear();
        ErrorReport er = new ErrorReport();
        er.ErrorCode = 99;
        er.ErrorText = "Exception: " + ex.Message;
        listErrorReports.Add(er);
        _gemControler.CreateObjectRequestCommandReply(createType, e.ListObjectEntities, bAck, listErrorReports, uSystemBytes, out err);
    }
    catch { }
}
```

---

## 修改 2：ATTRID 比較改為不區分大小寫（行 5028 ~ 5108）

### 位置
`_gemControler_CreateObjectRequestCommand()` → CONTROLJOB case → S14F9 → foreach (ObjectAttribute) 

### 原因
ToolSoftwareTester（Host）發送的 ATTRID 是混合大小寫（如 `ProcessingCtrlSpec`），
但 `ObjectAttributeKey` 常數是全大寫（如 `PROCESSINGCTRLSPEC`）。
`==` 做 case-sensitive 比較導致屬性匹配失敗，PRJob 陣列為空，CJ 建立失敗。

### 修改內容（7 處）
將所有 `==` 改為 `string.Equals(..., StringComparison.OrdinalIgnoreCase)`：

| # | 行號 | 原本 | 修改後 |
|---|------|------|--------|
| 1 | 5028 | `objAttr.ATTRID == ObjectAttributeKey.OBJID` | `string.Equals(objAttr.ATTRID, ObjectAttributeKey.OBJID, StringComparison.OrdinalIgnoreCase)` |
| 2 | 5041 | `objAttr.ATTRID == ObjectAttributeKey.ControlJob.CARRIERINPUTSPEC` | `string.Equals(objAttr.ATTRID, ObjectAttributeKey.ControlJob.CARRIERINPUTSPEC, StringComparison.OrdinalIgnoreCase)` |
| 3 | 5054 | `objAttr.ATTRID == ObjectAttributeKey.ControlJob.DATACOLLECTIONPLAN` | `string.Equals(objAttr.ATTRID, ObjectAttributeKey.ControlJob.DATACOLLECTIONPLAN, StringComparison.OrdinalIgnoreCase)` |
| 4 | 5058 | `objAttr.ATTRID == ObjectAttributeKey.ControlJob.PAUSEEVENT` | `string.Equals(objAttr.ATTRID, ObjectAttributeKey.ControlJob.PAUSEEVENT, StringComparison.OrdinalIgnoreCase)` |
| 5 | 5080 | `objAttr.ATTRID == ObjectAttributeKey.ControlJob.PROCESSINGCTRLSPEC` | `string.Equals(objAttr.ATTRID, ObjectAttributeKey.ControlJob.PROCESSINGCTRLSPEC, StringComparison.OrdinalIgnoreCase)` |
| 6 | 5093 | `objAttr.ATTRID == ObjectAttributeKey.ControlJob.PROCESSORDERMGMT` | `string.Equals(objAttr.ATTRID, ObjectAttributeKey.ControlJob.PROCESSORDERMGMT, StringComparison.OrdinalIgnoreCase)` |
| 7 | 5101 | `objAttr.ATTRID == ObjectAttributeKey.ControlJob.STARTMETHOD` | `string.Equals(objAttr.ATTRID, ObjectAttributeKey.ControlJob.STARTMETHOD, StringComparison.OrdinalIgnoreCase)` |

### Host 實際發送 vs 程式期望值對照
```
Host 發送                程式期望（ObjectAttributeKey）   原本匹配？
ObjID                    OBJID                           ❌
CarrierInputSpec         CARRIERINPUTSPEC                ❌
DATACOLLECTIONPLAN       DATACOLLECTIONPLAN              ✅
PAUSEEVENT               PAUSEEVENT                      ✅
ProcessingCtrlSpec       PROCESSINGCTRLSPEC              ❌ ← 關鍵
ProcessOrderMgmt         PROCESSORDERMGMT                ❌
StartMethod              STARTMETHOD                     ❌
```

---

## 修改 3：移除 PROCESSINGCTRLSPEC 多餘的解包邏輯（行 5080 ~ 5092）

### 位置
`_gemControler_CreateObjectRequestCommand()` → CONTROLJOB → PROCESSINGCTRLSPEC 解析

### 原因
原本有 `if (lw.Items.Count == 1)` 會多解一層 ListWrapper。
但 Host 發送的結構是：
```
<L [1]>                    ← 外層：1 個 PJ
  <L [3]>                  ← 內層：PJ 的 3 個屬性
    <A "PJ001">            ← Items[0] = String (PJ ID)
    <L ...>                ← Items[1] = ListWrapper
    <L ...>                ← Items[2] = ListWrapper
  >
>
```
多解一層後 lw 指向 `<L [3]>`，程式把 3 個子項目當成 3 個 PJ 處理，
嘗試將 Items[0]（String "PJ001"）轉型為 ListWrapper 導致 `InvalidCastException`。

### 修改內容
```csharp
// ===== 修改前 =====
lw = (ListWrapper)objAttr.ATTRDATA;
if (lw.Items.Count == 1)                    // ← 多餘的解包
{
    lw = (ListWrapper)lw.Items[0].Value;    // ← 解到 PJ 內部結構
}
PRJob = new string[lw.Items.Count];         // ← 變成 3，錯誤
for (int i = 0; i < lw.Items.Count; i++)
{
    lw_1 = (ListWrapper)lw.Items[i].Value;  // ← Items[0] 是 String → 💥
    PRJob[i] = lw_1.Items[0].ToString();
}

// ===== 修改後 =====
lw = (ListWrapper)objAttr.ATTRDATA;
PRJob = new string[lw.Items.Count];         // ← 正確：1 個 PJ
for (int i = 0; i < lw.Items.Count; i++)
{
    lw_1 = (ListWrapper)lw.Items[i].Value;  // ← Items[0] 是 <L[3]> ✅
    PRJob[i] = lw_1.Items[0].ToString();    // ← "PJ001" ✅
}
```

---

## 修改 4：加入 Debug.WriteLine 診斷日誌

### 位置與內容

| 行號 | 位置 | 日誌內容 |
|------|------|----------|
| 4843 | PROCESSJOB case 入口 | `"Recv CreateObject: PROCESSJOB  ObjID=..."` |
| 4999 | CONTROLJOB case 入口 | `"Recv CreateObject: CONTROLJOB  ObjID=..."` |
| 5000 | CONTROLJOB case 入口 | `"Expected PROCESSINGCTRLSPEC key=[...]"` |
| 5001 | CONTROLJOB case 入口 | `"Total attributes count=..."` |
| 5027 | foreach 屬性迴圈內 | `"CJ Attr: ATTRID=[...] VID=... DataType=..."` |
| 5082 | PROCESSINGCTRLSPEC 匹配時 | `">> PROCESSINGCTRLSPEC matched!"` |
| 5084 | PROCESSINGCTRLSPEC 解析 | `">> lw.Items.Count=..."` |
| 5090 | PRJob 解析結果 | `">> PRJob[i]=..."` |
| 5194 | catch 區塊 | `"CreateObjectRequestCommand Exception: ..."` |
| 6678 | CreateControlJob 入口 | `"CreateControlJob: objID=..., PRJob=[...], bStart=..."` |
| 6686 | CreateObject 失敗時 | `"CreateControlJob: CreateObject failed, ..."` |
| 6757 | PJ GetObject 失敗時 | `"CreateControlJob: PJ not found! pj=..."` |

### 注意
這些 `Debug.WriteLine` 是用於除錯目的，確認 CJ 建立正常後可考慮移除或保留作為日後診斷用途。

---

## 修改摘要

| # | 修改類型 | 說明 |
|---|----------|------|
| 1 | 穩定性 | 加入 try-catch，避免例外導致無回覆 |
| 2 | Bug 修正 | ATTRID 比較改為不區分大小寫（**關鍵修正**） |
| 3 | Bug 修正 | 移除 PROCESSINGCTRLSPEC 多餘解包（**關鍵修正**） |
| 4 | 除錯輔助 | 加入 Debug.WriteLine 診斷日誌 |
