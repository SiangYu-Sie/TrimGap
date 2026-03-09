// file name: WrINI.cs
// date: 20171114
// programer: Martin

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TrimGap
{
    internal class WrINI
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern uint GetPrivateProfileStringA(string section, string key, string def, Byte[] retVal, int size, string filePath);

        public static int total;

        private static void IniWriteValue(string Section, string Key, string Value, string filepath)//對ini文件進行寫操作的函数
        {
            WritePrivateProfileString(Section, Key, Value, filepath);
        }

        public static void LogWrite(string Info, string Control)//Log 寫入
        {
            string Log_AppRoot = Application.StartupPath;        // 取得執行檔的路徑
            string Filename = Log_AppRoot + @"log.ini";  // 資料夾名(存檔路徑) + 資料名稱
            if (!File.Exists(Filename)) // 有資料夾 -> 無檔案 -> 建檔ini(給存檔路徑.名稱)
            {
                System.IO.File.Create(Filename).Close();
            }

            IniWriteValue("Trigger ", "Time: |" + Control + "|", Info, Filename); //寫入ini
        }


        //讀取KEY
        public static List<string> ReadKeys(string SectionName, string iniFilename)
        {
            List<string> result = new List<string>();
            Byte[] buf = new Byte[65536];
            uint len = GetPrivateProfileStringA(SectionName, null, null, buf, buf.Length, iniFilename);
            int j = 0;
            for (int i = 0; i < len; i++)
                if (buf[i] == 0)
                {
                    result.Add(Encoding.Default.GetString(buf, j, i - j));
                    j = i + 1;
                }
            return result;
        }

        //讀取KEY值(多筆)
        public static List<string> ReadKeyValue(string SectionName, List<string> listKey, string sIniFileName)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < listKey.Count; i++)
            {
                StringBuilder strBuffer = new StringBuilder(255);
                GetPrivateProfileString(SectionName, listKey[i].ToString(), "", strBuffer, 255, sIniFileName);
                result.Add(strBuffer.ToString());
            }

            return result;
        }


        //取得INI檔
        private static string GetFromINI(string Section, string key, string sIniFileName)
        {
            StringBuilder strBuffer = new StringBuilder(255);
            GetPrivateProfileString(Section, key, "", strBuffer, 255, sIniFileName);
            return strBuffer.ToString();
        }

        //寫入INI檔
        private static void WriteToINI(string Section, string key, string KeyValue, string sIniFileName)
        {
            WritePrivateProfileString(Section, key, KeyValue, sIniFileName);
        }

        public static string Readini(string Section, string key, string FilePath)
        {
            string iniread;
            //iniread = GetFromINI(Section, key, FilePath);
            return iniread = GetFromINI(Section, key, FilePath);
        }

        public static void Writeini(string Section, string key, string KeyValue, string FilePath)
        {
            WriteToINI(Section, key, KeyValue, FilePath);
        }

        //byte
        public static void iniVal(string wr, string filename, string KeyName, ref byte variable, string key, byte KeyValue)
        {
            string iniStr = null;
            string iniFileName;
            iniFileName = filename;
            total++;
            switch (wr)
            {
                case "r":
                    iniStr = GetFromINI(KeyName, key, iniFileName).Trim();
                    if (iniStr.Length > 0)
                        variable = Convert.ToByte(iniStr);
                    else
                    {
                        WriteToINI(KeyName, key, KeyValue.ToString(), iniFileName);
                        variable = KeyValue;
                    }
                    break;

                case "w":
                    WriteToINI(KeyName, key, variable.ToString(), iniFileName);
                    break;
            }
            return;
        }

        //int
        public static void iniVal(string wr, string filename, string KeyName, ref int variable, string key, int KeyValue)
        {
            string iniStr = null;
            string iniFileName;
            iniFileName = filename;
            total++;
            switch (wr)
            {
                case "r":
                    iniStr = GetFromINI(KeyName, key, iniFileName).Trim();
                    if (iniStr.Length > 0)
                        variable = Convert.ToInt32(iniStr);
                    else
                    {
                        WriteToINI(KeyName, key, KeyValue.ToString(), iniFileName);
                        variable = KeyValue;
                    }
                    break;

                case "w":
                    WriteToINI(KeyName, key, variable.ToString(), iniFileName);
                    break;
            }
            return;
        }

        //ushort
        public static void iniVal(string wr, string filename, string KeyName, ref ushort variable, string key, ushort KeyValue)
        {
            string iniStr = null;
            string iniFileName;
            iniFileName = filename;
            total++;
            switch (wr)
            {
                case "r":
                    iniStr = GetFromINI(KeyName, key, iniFileName).Trim();
                    if (iniStr.Length > 0)
                        variable = Convert.ToUInt16(iniStr);
                    else
                    {
                        WriteToINI(KeyName, key, KeyValue.ToString(), iniFileName);
                        variable = KeyValue;
                    }
                    break;

                case "w":
                    WriteToINI(KeyName, key, variable.ToString(), iniFileName);
                    break;
            }
            return;
        }

        //long
        public static void iniVal(string wr, string filename, string KeyName, ref long variable, string key, long KeyValue)
        {
            string iniStr = null;
            string iniFileName;
            iniFileName = filename;
            total++;
            switch (wr)
            {
                case "r":
                    iniStr = GetFromINI(KeyName, key, iniFileName).Trim();
                    if (iniStr.Length > 0)
                        variable = Convert.ToInt64(iniStr);
                    else
                    {
                        WriteToINI(KeyName, key, KeyValue.ToString(), iniFileName);
                        variable = KeyValue;
                    }
                    break;

                case "w":
                    WriteToINI(KeyName, key, variable.ToString(), iniFileName);
                    break;
            }
            return;
        }

        //double
        public static void iniVal(string wr, string filename, string KeyName, ref double variable, string key, double KeyValue)
        {
            string iniStr = null;
            string iniFileName;
            iniFileName = filename;
            total++;
            switch (wr)
            {
                case "r":
                    iniStr = GetFromINI(KeyName, key, iniFileName).Trim();
                    if (iniStr.Length > 0)
                        variable = Convert.ToDouble(iniStr);
                    else
                    {
                        WriteToINI(KeyName, key, KeyValue.ToString(), iniFileName);
                        variable = KeyValue;
                    }
                    break;

                case "w":
                    WriteToINI(KeyName, key, variable.ToString(), iniFileName);
                    break;
            }
            return;
        }

        //float
        public static void iniVal(string wr, string filename, string KeyName, ref float variable, string key, float KeyValue)
        {
            string iniStr = null;
            string iniFileName;
            iniFileName = filename;
            total++;
            switch (wr)
            {
                case "r":
                    iniStr = GetFromINI(KeyName, key, iniFileName).Trim();
                    if (iniStr.Length > 0)
                        variable = Convert.ToSingle(iniStr);
                    else
                    {
                        WriteToINI(KeyName, key, KeyValue.ToString(), iniFileName);
                        variable = KeyValue;
                    }
                    break;

                case "w":
                    WriteToINI(KeyName, key, variable.ToString(), iniFileName);
                    break;
            }
            return;
        }

        //char
        public static void iniVal(string wr, string filename, string KeyName, ref char variable, string key, char KeyValue)
        {
            string iniStr = null;
            string iniFileName;
            iniFileName = filename;
            total++;
            switch (wr)
            {
                case "r":
                    iniStr = GetFromINI(KeyName, key, iniFileName).Trim();
                    if (iniStr.Length > 0)
                        variable = Convert.ToChar(iniStr);
                    else
                    {
                        WriteToINI(KeyName, key, KeyValue.ToString(), iniFileName);
                        variable = KeyValue;
                    }
                    break;

                case "w":
                    WriteToINI(KeyName, key, KeyValue.ToString(), iniFileName);
                    break;
            }
            return;
        }

        //string
        public static void iniVal(string wr, string filename, string KeyName, ref string variable, string key, string KeyValue)
        {
            string iniStr = null;
            string iniFileName;
            iniFileName = filename;
            total++;
            switch (wr)
            {
                case "r":
                    iniStr = GetFromINI(KeyName, key, iniFileName).Trim();
                    if (iniStr.Length > 0)
                        variable = iniStr;
                    else
                    {
                        WriteToINI(KeyName, key, KeyValue.ToString(), iniFileName);
                        variable = KeyValue;
                    }
                    break;

                case "w":
                    WriteToINI(KeyName, key, variable.ToString(), iniFileName);
                    break;
            }
            return;
        }
    }
}