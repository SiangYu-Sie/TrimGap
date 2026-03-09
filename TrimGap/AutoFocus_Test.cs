using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading;
using System.Windows.Forms;
using static TrimGap.AutoRunStage;

namespace TrimGap
{
    public partial class AutoFocus_Test : Form
    {
        private DataTable originalData;
        private DataTable transposedData;
        List<short[]> list_spectrumData = new List<short[]>();

        public AutoFocus_Test()
        {
            InitializeComponent();
        }

        private void AutoFocus_Test_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "請選擇 Excel 或 CSV 檔案上傳";
        }

        /// <summary>
        /// 上傳 Excel 或 CSV 檔案按鈕事件
        /// </summary>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "所有支援格式|*.xls;*.xlsx;*.csv|Excel Files|*.xls;*.xlsx|CSV Files|*.csv";
                openFileDialog.Title = "選擇 Excel 或 CSV 檔案";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        lblStatus.Text = "正在讀取檔案...";
                        Application.DoEvents();

                        string filePath = openFileDialog.FileName;
                        txtFilePath.Text = filePath;

                        // 根據副檔名選擇讀取方式
                        string extension = Path.GetExtension(filePath).ToLower();
                        if (extension == ".csv")
                        {
                            originalData = ReadCsvFile(filePath);
                        }
                        else
                        {
                            originalData = ReadExcelFile(filePath);
                        }

                        if (originalData != null && originalData.Rows.Count > 0)
                        {
                            // 顯示原始資料
                            dgvOriginal.DataSource = originalData;

                            // 行列轉置
                            TransposeDataTable(originalData);

                            // 顯示轉置後資料
                            //dgvTransposed.DataSource = transposedData;
                            CalculateAutoFocus();

                            lblStatus.Text = $"讀取完成。";
                        }
                        else
                        {
                            lblStatus.Text = "檔案為空或讀取失敗";
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show($"讀取 Excel 檔案時發生錯誤:\n{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblStatus.Text = "讀取失敗";
                    }
                }
            }
        }

        /// <summary>
        /// 讀取 CSV 檔案 (不需要標題)
        /// </summary>
        private DataTable ReadCsvFile(string filePath)
        {
            DataTable dt = new DataTable();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length == 0)
                {
                    throw new Exception("CSV 檔案中沒有資料");
                }

                // 使用第一行來決定欄位數量 (不當作標題)
                string[] firstLineValues = lines[0].Split(',');
                int colCount = firstLineValues.Length;

                // 建立欄位 (自動命名)
                for (int col = 0; col < colCount; col++)
                {
                    dt.Columns.Add($"Column{col + 1}");
                }

                // 讀取所有資料 (從第一行開始，沒有標題)
                for (int row = 0; row < lines.Length; row++)
                {
                    if (string.IsNullOrWhiteSpace(lines[row]))
                        continue;

                    string[] values = lines[row].Split(',');
                    DataRow dr = dt.NewRow();

                    for (int col = 0; col < colCount; col++)
                    {
                        if (col < values.Length)
                        {
                            dr[col] = values[col]?.Trim() ?? "";
                        }
                        else
                        {
                            dr[col] = "";
                        }
                    }
                    dt.Rows.Add(dr);
                }

                System.Diagnostics.Debug.WriteLine($"CSV 資料: 總行數={dt.Rows.Count}, 總列數={dt.Columns.Count}");
            }
            catch (Exception ex)
            {
                throw new Exception($"讀取 CSV 失敗: {ex.Message}");
            }

            return dt;
        }

        /// <summary>
        /// 讀取 Excel 檔案 (使用 Spire.XLS，不需要標題)
        /// </summary>
        private DataTable ReadExcelFile(string filePath)
        {
            DataTable dt = new DataTable();

            try
            {
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(filePath);

                if (workbook.Worksheets.Count == 0)
                {
                    throw new Exception("Excel 檔案中沒有工作表");
                }

                Worksheet sheet = workbook.Worksheets[0];

                // 使用 AllocatedRange 取得實際有資料的範圍
                CellRange usedRange = sheet.AllocatedRange;
                if (usedRange == null)
                {
                    throw new Exception("工作表中沒有資料");
                }

                int rowCount = usedRange.RowCount;
                int colCount = usedRange.ColumnCount;
                int startRow = usedRange.Row;
                int startCol = usedRange.Column;

                // 偵錯資訊
                System.Diagnostics.Debug.WriteLine($"資料範圍: 起始行={startRow}, 起始列={startCol}, 總行數={rowCount}, 總列數={colCount}");

                if (rowCount == 0 || colCount == 0)
                {
                    throw new Exception("工作表中沒有有效資料");
                }

                // 建立欄位 (自動命名，不使用第一列作為標題)
                for (int col = 0; col < colCount; col++)
                {
                    dt.Columns.Add($"Column{col + 1}");
                }

                // 讀取所有資料 (從第一列開始，沒有標題)
                for (int row = 0; row < rowCount; row++)
                {
                    DataRow dr = dt.NewRow();
                    for (int col = 0; col < colCount; col++)
                    {
                        var cellValue = sheet.Range[startRow + row, startCol + col].Value;
                        dr[col] = cellValue?.ToString() ?? "";
                    }
                    dt.Rows.Add(dr);
                }

                workbook.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception($"讀取 Excel 失敗: {ex.Message}");
            }

            return dt;
        }

        /// <summary>
        /// 將 DataTable 的行與列轉置
        /// </summary>
        private void TransposeDataTable(DataTable source)
        {
            // 清空之前的資料
            list_spectrumData.Clear();

            // 建立轉置後的 DataTable
            transposedData = new DataTable();

            // 建立欄位 (每一列變成一個欄位)
            for (int col = 0; col < source.Columns.Count; col++)
            {
                transposedData.Columns.Add($"Column{col + 1}", typeof(short));
            }

            // 每個欄位變成一個 short 陣列
            //foreach (DataRow row in source.Rows)
            //{
            for (int i = 0; i < source.Rows.Count; i++)
            {
                short[] tmpExcel = Array.ConvertAll(source.Rows[i].ItemArray, item =>
                {
                    short value;
                    return short.TryParse(item.ToString(), out value) ? value : (short)0;
                });

                // 存到 list_spectrumData
                list_spectrumData.Add(tmpExcel);

                // 存到 transposedData DataTable
                DataRow dr = transposedData.NewRow();
                for (int col = 0; col < tmpExcel.Length; col++)
                {
                    dr[col] = tmpExcel[col];
                }
                transposedData.Rows.Add(dr);
            }
            //}

            // 顯示在 dgvTransposed 元件上
            dgvTransposed.DataSource = transposedData;
        }

        /// <summary>
        /// 匯出轉置後的資料
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (transposedData == null || transposedData.Rows.Count == 0)
            {
                MessageBox.Show("沒有可匯出的資料，請先上傳 Excel 檔案。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "儲存轉置後的 Excel 檔案";
                saveFileDialog.FileName = "Transposed_Data.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ExportToExcel(transposedData, saveFileDialog.FileName);
                        MessageBox.Show("匯出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblStatus.Text = $"已匯出至: {saveFileDialog.FileName}";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"匯出失敗:\n{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// 匯出 DataTable 到 Excel (使用 Spire.XLS)
        /// </summary>
        private void ExportToExcel(DataTable dt, string filePath)
        {
            Workbook workbook = new Workbook();
            Worksheet sheet = workbook.Worksheets[0];

            // 寫入標題
            for (int col = 0; col < dt.Columns.Count; col++)
            {
                sheet.Range[1, col + 1].Text = dt.Columns[col].ColumnName;
                sheet.Range[1, col + 1].Style.Font.IsBold = true;
            }

            // 寫入資料
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    sheet.Range[row + 2, col + 1].Text = dt.Rows[row][col]?.ToString() ?? "";
                }
            }

            // 自動調整欄寬
            for (int col = 1; col <= dt.Columns.Count; col++)
            {
                sheet.AutoFitColumn(col);
            }

            workbook.SaveToFile(filePath, ExcelVersion.Version2013);
            workbook.Dispose();
        }

        /// <summary>
        /// 清除資料
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            originalData = null;
            transposedData = null;
            dgvOriginal.DataSource = null;
            dgvTransposed.DataSource = null;
            txtFilePath.Text = "";
            lblStatus.Text = "已清除資料";
        }

        private void CalculateAutoFocus()
        {
            fram.HTW_Autofocus_DetectIndex = 80;//175 -> 180 -> 190  ->120 -> 80(改到左邊去，大約高度降了70um)
            fram.HTW_Autofocus_Index = 0;
            fram.HTW_Autofocus_2ndPos_Shift = 200;  //第一次找不到焦點時，要偏移多少距離再量一次

            sram.Focus_Offset = 0;
            short[] spectrumData;
            short[] spectrumDataOld = new short[1];
            sram.SpectrumMaxValue = new short[list_spectrumData.Count];
             
            sram.SpectrumMaxValueBias = new int[list_spectrumData.Count];
            short max_allpos = 0;
            Dictionary<int, short> max3 = new Dictionary<int, short>();
            bool RightToLeft = false;
            double LeftSum = 0;
            double RightSum = 0;
            List<double> list_Left = new List<double>();
            List<double> list_Right = new List<double>();
            //20251222
            // List<short[]> list_spectrumData = new List<short[]>();

            //for (int i = 0; i < fram.Position.HTW_P1_FocusRange; i++)
            for (int i = 0; i < list_spectrumData.Count; i++)
            {
                Common.motion.PosMove(Mo.AxisNo.AP6II_Z3, fram.Position.HTW_P1_Z + i * 10);//下往上
                SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3), 8000);
                //spectrumData = Common.PTForm.GetSpectrum(2); //Get FT
                spectrumData = list_spectrumData[i];
                //20251222
                //list_spectrumData.Add(spectrumData);

                LeftSum = 0;
                RightSum = 0;
                if (spectrumData.Length >= fram.HTW_Autofocus_DetectIndex + 20)
                {
                    short max = 0;
                    

                    int pos = fram.HTW_Autofocus_DetectIndex - 5;
                    for (int j = fram.HTW_Autofocus_DetectIndex - 5; j <= fram.HTW_Autofocus_DetectIndex + 5; j++)
                    {
                        list_Left = new List<double>();
                        list_Right = new List<double>();

                        if (spectrumData[j] > max)
                        {
                            max = spectrumData[j];
                            pos = j;
                        }
                        //if (i != 0)  //不是第一筆資料，可以比對舊資料
                        //{
                        //    LeftSum += Math.Abs((double)(spectrumDataOld[j - 10] - spectrumData[j]));
                        //    RightSum += Math.Abs((double)(spectrumDataOld[j + 10] - spectrumData[j]));
                        //}
                        if (i != 0)
                        {
                            for (int k = 1; k <= 10; k++)
                            {
                                list_Left.Add((double)(spectrumDataOld[j - k]));
                                list_Right.Add((double)(spectrumDataOld[j + k]));
                            }
                        }
                    }
                    sram.SpectrumMaxValueBias[i] = pos - fram.HTW_Autofocus_DetectIndex;
                    sram.SpectrumMaxValue[i] = max;

                    //20250828
                    if (i != 0)  //不是第一筆資料，可以比對舊資料
                    {
                        //LeftSum = Math.Abs((double)(spectrumDataOld[pos - 10] - spectrumData[pos]));
                        //RightSum = Math.Abs((double)(spectrumDataOld[pos + 10] - spectrumData[pos]));
                        LeftSum = list_Left.Max();
                        RightSum = list_Right.Max();
                    }

                    if ((max > max_allpos) && (LeftSum < RightSum))  //有最大值要更新 && 右邊過來
                    {
                        //一組方式(原方法)
                        max_allpos = max;
                        fram.HTW_Autofocus_Index = i;

                        //三組方式20240722
                        /*
                        max3.Add(i, max);  
                        if(max3.Count>3)   //新的值做為第四組加進來一起排序
                        {
                            max3 = max3.OrderBy(o => o.Value).ToDictionary(o => o.Key, o => o.Value);  //Value小到大排序
                            max3.Remove(max3.Keys.ElementAt(0));  //刪除第一個(最小的)
                            max_allpos = max3.Values.ElementAt(0);  //剩3個裡面最小的當作max_allpos，之後有比他大的值就放進來重排
                            fram.HTW_Autofocus_Index = max3.Keys.Min();   //剩3個裡面 最靠左的那個頻譜peak
                            if (max3[fram.HTW_Autofocus_Index] < max3.Values.Max() * 0.7)   //最靠左的那個頻譜peak 強度不能太低(至少要有最強peak的7成吧)
                                fram.HTW_Autofocus_Index = max3.Keys.ElementAt(2);          //太弱了就不要用，改回最強的peak
                        }
                        else
                        {
                            max3 = max3.OrderBy(o => o.Value).ToDictionary(o => o.Key, o => o.Value);  //Value小到大排序
                            fram.HTW_Autofocus_Index = max3.Keys.ElementAt(max3.Count-1);   //最大的，max_allpos維持0以便繼續收滿4組資料
                        }*/
                    }
                    spectrumDataOld = new short[spectrumData.Length];
                    spectrumData.CopyTo(spectrumDataOld, 0);
                }
                else
                    sram.SpectrumMaxValue[i] = 0;
            }
            //對不到焦第二次機會
            if (max_allpos <= 3000)
            {
                if (sram.AutoFocus_Retry_Count == 0)  //第一次retry 往正方向走
                {
                    Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_AutoFocus_X + fram.HTW_Autofocus_2ndPos_Shift);  //改AutoFocus位置
                    Common.motion.PosMove(Mo.AxisNo.AP6II_Z3, fram.Position.HTW_P1_Z);

                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_X), 10000);
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3), 4000);

                    sram.AutoFocus_Retry_Count = 1;
                    //break;
                }
                else if (sram.AutoFocus_Retry_Count == 1)  //第二次retry 往負方向走
                {
                    Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_AutoFocus_X - fram.HTW_Autofocus_2ndPos_Shift);  //改AutoFocus位置
                    Common.motion.PosMove(Mo.AxisNo.AP6II_Z3, fram.Position.HTW_P1_Z);

                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_X), 10000);
                    SpinWait.SpinUntil(() => Common.motion.MotionDone(Mo.AxisNo.AP6II_Z3), 4000);

                    sram.AutoFocus_Retry_Count = 2;
                    //break;
                }
                else
                {
                    sram.AutoFocus_Retry_Count = 3;
                    if (fram.HTW_Autofocus_Index_Last_Used != 0)  //三次Focus都失敗，用上一次成功的對焦位置替代
                        fram.HTW_Autofocus_Index = fram.HTW_Autofocus_Index_Last_Used;
                }
            }
            else
            {
                fram.HTW_Autofocus_Index_Last_Used = fram.HTW_Autofocus_Index;  //保存成功的Focus位置
            }

            sram.Focus_Offset = 1 * fram.HTW_Autofocus_Index * 10 + sram.SpectrumMaxValueBias[fram.HTW_Autofocus_Index];

            Common.motion.PosMove(Mo.AxisNo.AP6II_X, fram.Position.HTW_P1_X);//走到起點
            Common.motion.PosMove(Mo.AxisNo.AP6II_Z3, fram.Position.HTW_P1_Z + sram.Focus_Offset);
            string focusretry = "中";
            if (sram.AutoFocus_Retry_Count == 1)
                focusretry = "右";
            else if (sram.AutoFocus_Retry_Count == 2)
                focusretry = "左";
            else
                focusretry = "上";
            ReportData.HTW_Focus_Z = (fram.Position.HTW_P1_Z + sram.Focus_Offset).ToString() + "(" + fram.HTW_Autofocus_Index + "," + max_allpos + "," + focusretry + ")";
            MessageBox.Show(ReportData.HTW_Focus_Z);
            sram.PTRetry = 0;
            sram.AutoFocus_Retry_Count = 0;
        }
    }
}
