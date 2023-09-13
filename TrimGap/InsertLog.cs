using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace TrimGap
{
    internal class InsertLog
    {
        public static SQLite.DataBase dataBase = new SQLite.DataBase(sram.DataBasePath, Language.TC);
        public static DataTable dt = new DataTable();
        public static List<string> dtColumnsName = new List<string>();
        private static bool initFlag = false;

        public static bool Init(string fullpath)
        {
            try
            {
                //string fullpath = @"E:\TL-Chen\C# project file\FilmThickness\FilmThickness\bin\ParameterDirectory\param\NoMap.csv";

                FileStream fs = new FileStream(fullpath, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                //紀錄每次讀取的一行紀錄
                //DataTable dt = new DataTable();
                string strLine = "";
                //記錄每行紀錄中的各字段內容
                string[] aryLine = null;
                string[] tableHead = null;
                //標示列數
                int columnCount = 1;
                //標示是否是讀取的第一行
                bool isFirst = true;

                //逐行讀取csv數據
                while ((strLine = sr.ReadLine()) != null)
                {
                    if (isFirst == true)
                    {
                        tableHead = strLine.Split(',');
                        isFirst = false;
                        columnCount = tableHead.Length;
                        //創建列
                        for (int i = 0; i < columnCount; i++)
                        {
                            tableHead[i] = tableHead[i].Replace("\"", "");
                            dtColumnsName.Add(tableHead[i]);
                            DataColumn dc = new DataColumn(tableHead[i]);
                            //dc = new DataColumn(tableHead[i]);
                            dt.Columns.Add(dc);
                        }
                    }
                    else
                    {
                        aryLine = strLine.Split(',');

                        DataRow dr = dt.NewRow();
                        dr = dt.NewRow();
                        for (int j = 0; j < columnCount; j++)
                        {
                            dr[j] = aryLine[j].Replace("\"", "");
                        }
                        dt.Rows.Add(dr);
                    }
                }
                if (aryLine != null && aryLine.Length > 0)
                {
                    dt.DefaultView.Sort = tableHead[0];
                }
                sr.Close();
                fs.Close();
                initFlag = true;

                return initFlag;
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                //outputdata = new float[1];
                //MessageBox.Show("Log資料讀取錯誤，請確認NoMap.csv是否被開啟");
                MessageBox.Show(ee.Message);
                initFlag = false;

                return initFlag;
            }
        }

        public static void SavetoDB(int CODE, string Note)
        {
            if (!initFlag)
            {
                //Init();
            }

            int CODECount = 0;
            // For each row, print the values of each column.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (CODE.ToString() == dt.Rows[i]["CODE"].ToString())
                {
                    CODECount = i;
                    break;
                }
            }

            object[,] Data = new object[dtColumnsName.Count + 1, 2];

            Data[0, 0] = "TIME";
            Data[0, 1] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            for (int i = 0; i < dtColumnsName.Count; i++)
            {
                Data[i + 1, 0] = dtColumnsName[i];

                if (dtColumnsName[i] == "NOTE")
                {
                    Data[i + 1, 1] = Note;
                }
                else
                {
                    Data[i + 1, 1] = dt.Rows[CODECount][dtColumnsName[i]].ToString();
                }
            }
            dataBase.sQ.InsertData("logTable", Data);
        }

        public static void SavetoDB(int CODE)
        {
            int CODECount = 0;
            // For each row, print the values of each column.
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (CODE.ToString() == dt.Rows[i]["CODE"].ToString())
                {
                    CODECount = i;
                    break;
                }
            }

            object[,] Data = new object[dtColumnsName.Count + 1, 2];

            Data[0, 0] = "TIME";
            Data[0, 1] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            for (int i = 0; i < dtColumnsName.Count; i++)
            {
                Data[i + 1, 0] = dtColumnsName[i];
                if (dtColumnsName[i] == "NOTE")
                {
                    Data[i + 1, 1] = "";
                }
                else
                {
                    Data[i + 1, 1] = dt.Rows[CODECount][dtColumnsName[i]].ToString();
                }
            }
            dataBase.sQ.InsertData("logTable", Data);
        }
    }
}