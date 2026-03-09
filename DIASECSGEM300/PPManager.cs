using Delta.DIAAuto.DIASECSGEM;
using Delta.DIAAuto.DIASECSGEM.GEMDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace DemoFormDiaGemLib
{

    public enum ePPFormat
    {
        UnformattedProcessPrograms = 1,
        FormattedProcessPrograms = 2,
        BothUnformattedAndFormattedProcessPrograms = 3
    }
    public class ReadWriteINIfile
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string name, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public string path;

        public ReadWriteINIfile(string inipath)
        {
            path = inipath;
        }
        public void IniWriteValue(string Section, string Key, string Value, string inipath)
        {
            WritePrivateProfileString(Section, Key, Value, inipath);//Application.StartupPath + "\\" + 
        }
        public string IniReadValue(string Section, string Key, string inipath)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, inipath);//Application.StartupPath + "\\" + 
            if(i == 0)
            {
                if (Key == "fram.Recipe.WaferEdgeEvaluate")
                    temp.Append('0');
                else
                    i = GetPrivateProfileString("Analysis", Key.Replace("Recipe", "Analysis"), "", temp, 255, "D:\\FTGM1\\ParameterDirectory\\param\\param.ini");
            }
            return temp.ToString();
        }
    }

    public class PPManager
    {
        private Dictionary<string, PPBody> _DicPPBody;

        public List<string> PPIDList
        {
            get
            {
                List<string> ppidList = _DicPPBody.Keys.ToList();
                ppidList.Sort();
                return ppidList;
            }
        }

        public PPManager()
        {
            _DicPPBody = new Dictionary<string, PPBody>();
            Initial();
        }

        public void Initial()
        {
            DeletePPID_All();
            PPBody ppBody;
            System.IO.FileInfo fileName;
            string path = "D:\\FTGM1\\ParameterDirectory\\Recipe";
            string parampath = "";
            ReadWriteINIfile INIfile = new ReadWriteINIfile(path);
            string filename2;
            foreach (string fname in System.IO.Directory.GetFiles(path))
            {
                fileName = new System.IO.FileInfo(fname);//取得完整檔名(含副檔名)
                filename2 = fileName.ToString();
                filename2 = System.IO.Path.GetFileNameWithoutExtension(filename2);
                ppBody = new PPBody();
                ppBody.PPID = filename2;
                //Console.WriteLine(fileName.FullName);
                parampath = path + "\\" + filename2 + ".ini";
                ppBody.Rotate_Count = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Rotate_Count", parampath));
                ppBody.Type = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Type", parampath));
                ppBody.RepeatTimes = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.RepeatTimes", parampath));
                ppBody.RepeatTimes_now = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.RepeatTimes_now", parampath));
                ppBody.Slot1 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot1", parampath));
                ppBody.Slot2 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot2", parampath));
                ppBody.Slot3 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot3", parampath));
                ppBody.Slot4 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot4", parampath));
                ppBody.Slot5 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot5", parampath));
                ppBody.Slot6 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot6", parampath));
                ppBody.Slot7 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot7", parampath));
                ppBody.Slot8 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot8", parampath));
                ppBody.Slot9 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot9", parampath));
                ppBody.Slot10 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot10", parampath));
                ppBody.Slot11 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot11", parampath));
                ppBody.Slot12 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot12", parampath));
                ppBody.Slot13 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot13", parampath));
                ppBody.Slot14 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot14", parampath));
                ppBody.Slot15 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot15", parampath));
                ppBody.Slot16 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot16", parampath));
                ppBody.Slot17 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot17", parampath));
                ppBody.Slot18 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot18", parampath));
                ppBody.Slot19 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot19", parampath));
                ppBody.Slot20 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot20", parampath));
                ppBody.Slot21 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot21", parampath));
                ppBody.Slot22 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot22", parampath));
                ppBody.Slot23 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot23", parampath));
                ppBody.Slot24 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot24", parampath));
                ppBody.Slot25 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Slot25", parampath));
                ppBody.Angle1 = Convert.ToUInt16(INIfile.IniReadValue("Recipe", "fram.Recipe.Angle1", parampath));
                ppBody.Angle2 = Convert.ToUInt16(INIfile.IniReadValue("Recipe", "fram.Recipe.Angle2", parampath));
                ppBody.Angle3 = Convert.ToUInt16(INIfile.IniReadValue("Recipe", "fram.Recipe.Angle3", parampath));
                ppBody.Angle4 = Convert.ToUInt16(INIfile.IniReadValue("Recipe", "fram.Recipe.Angle4", parampath));
                ppBody.Angle5 = Convert.ToUInt16(INIfile.IniReadValue("Recipe", "fram.Recipe.Angle5", parampath));
                ppBody.Angle6 = Convert.ToUInt16(INIfile.IniReadValue("Recipe", "fram.Recipe.Angle6", parampath));
                ppBody.Angle7 = Convert.ToUInt16(INIfile.IniReadValue("Recipe", "fram.Recipe.Angle7", parampath));
                ppBody.Angle8 = Convert.ToUInt16(INIfile.IniReadValue("Recipe", "fram.Recipe.Angle8", parampath));
                ppBody.CreateTime = INIfile.IniReadValue("Recipe", "fram.Recipe.CreateTime", parampath);
                ppBody.ReviseTime = INIfile.IniReadValue("Recipe", "fram.Recipe.ReviseTime", parampath);
                ppBody.OffsetType = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.OffsetType", parampath));
                ppBody.LJStdSurface = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.WaferEdgeEvaluate", parampath));
                ppBody.BTTH = Convert.ToUInt16(INIfile.IniReadValue("Recipe", "fram.Recipe.BlueTapeThreshold", parampath));
                ppBody.S1_1x0 = Convert.ToUInt16(INIfile.IniReadValue("Recipe", "fram.Recipe.Step1_Range_step1x0", parampath));
                ppBody.S1_1x1 = Convert.ToUInt16(INIfile.IniReadValue("Recipe", "fram.Recipe.Step1_Range_step1x1", parampath));
                ppBody.S2_1x0 = Convert.ToUInt16(INIfile.IniReadValue("Recipe", "fram.Recipe.Step2_Range_step1x0", parampath));
                ppBody.S2_1x1 = Convert.ToUInt16(INIfile.IniReadValue("Recipe", "fram.Recipe.Step2_Range_step1x1", parampath));
                ppBody.S2_2x0 = Convert.ToUInt16(INIfile.IniReadValue("Recipe", "fram.Recipe.Step2_Range_step2x0", parampath));
                ppBody.S2_2x1 = Convert.ToUInt16(INIfile.IniReadValue("Recipe", "fram.Recipe.Step2_Range_step2x1", parampath));
                ppBody.Range1 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Range1_Percent", parampath));
                ppBody.Range2 = Convert.ToByte(INIfile.IniReadValue("Recipe", "fram.Recipe.Range2_Percent", parampath));

                _DicPPBody.Add(ppBody.PPID, ppBody);
            }
        }

        public bool IsConatain(string ppid)
        {
            return _DicPPBody.ContainsKey(ppid);
        }

        public PPBody GetPPBody(string ppid)
        {
            PPBody ppBody = null;
            if (_DicPPBody.ContainsKey(ppid))
                ppBody = _DicPPBody[ppid];

            return ppBody;
        }

        public void AddorUpdatePPID(string ppid, PPBody ppBody)
        {
            _DicPPBody.Remove(ppid);
            _DicPPBody.Add(ppid, ppBody);
        }

        public void DeletePPID(string ppid)
        {
            _DicPPBody.Remove(ppid);
            // 同步刪除磁碟上的 Recipe 檔案，避免 Initial() 重新讀取時又出現
            try
            {
                string filePath = "D:\\FTGM1\\ParameterDirectory\\Recipe\\" + ppid + ".ini";
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }
            catch { }
        }

        public void DeletePPID_All()
        {
            _DicPPBody.Clear();
        }
    }

    public class PPBody
    {
        public byte Rotate_Count { get; set; }
        public byte Type { get; set; }
        public byte RepeatTimes { get; set; }
        public byte RepeatTimes_now { get; set; }
        public byte Slot1 { get; set; }
        public byte Slot2 { get; set; }
        public byte Slot3 { get; set; }
        public byte Slot4 { get; set; }
        public byte Slot5 { get; set; }
        public byte Slot6 { get; set; }
        public byte Slot7 { get; set; }
        public byte Slot8 { get; set; }
        public byte Slot9 { get; set; }
        public byte Slot10 { get; set; }
        public byte Slot11 { get; set; }
        public byte Slot12 { get; set; }
        public byte Slot13 { get; set; }
        public byte Slot14 { get; set; }
        public byte Slot15 { get; set; }
        public byte Slot16 { get; set; }
        public byte Slot17 { get; set; }
        public byte Slot18 { get; set; }
        public byte Slot19 { get; set; }
        public byte Slot20 { get; set; }
        public byte Slot21 { get; set; }
        public byte Slot22 { get; set; }
        public byte Slot23 { get; set; }
        public byte Slot24 { get; set; }
        public byte Slot25 { get; set; }
        public ushort Angle1 { get; set; }
        public ushort Angle2 { get; set; }
        public ushort Angle3 { get; set; }
        public ushort Angle4 { get; set; }
        public ushort Angle5 { get; set; }
        public ushort Angle6 { get; set; }
        public ushort Angle7 { get; set; }
        public ushort Angle8 { get; set; }
        public string CreateTime { get; set; }
        public string ReviseTime { get; set; }
        public byte OffsetType { get; set; }

        public string PPID { get; set; }

        public byte LJStdSurface { get; set; }
        public ushort BTTH { get; set; }
        public ushort S1_1x0 { get; set; }
        public ushort S1_1x1 { get; set; }
        public ushort S2_1x0 { get; set; }
        public ushort S2_1x1 { get; set; }
        public ushort S2_2x0 { get; set; }
        public ushort S2_2x1 { get; set; }
        public byte Range1 { get; set; }
        public byte Range2 { get; set; }



        public byte[] GetPPBody_Bytes()
        {
            byte[] tmpParam;
            byte[] byteBody = Enumerable.Repeat<byte>(0, 200).ToArray();
            //tmpParam = BitConverter.GetBytes(Tank1_WaterPressure);
            //Buffer.BlockCopy(tmpParam, 0, byteBody, 0, tmpParam.Length);
            //tmpParam = BitConverter.GetBytes(Tank1_Voltage);
            //Buffer.BlockCopy(tmpParam, 0, byteBody, 4, tmpParam.Length);
            //tmpParam = BitConverter.GetBytes(Tank1_AirPressure);
            //Buffer.BlockCopy(tmpParam, 0, byteBody, 8, tmpParam.Length);
            //tmpParam = BitConverter.GetBytes(Tank2_WaterPressure);
            //Buffer.BlockCopy(tmpParam, 0, byteBody, 100, tmpParam.Length);
            //tmpParam = BitConverter.GetBytes(Tank2_Voltage);
            //Buffer.BlockCopy(tmpParam, 0, byteBody, 104, tmpParam.Length);
            //tmpParam = BitConverter.GetBytes(Tank2_AirPressure);
            //Buffer.BlockCopy(tmpParam, 0, byteBody, 108, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Rotate_Count);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 0, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Type);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 1, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(RepeatTimes);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 2, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(RepeatTimes_now);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 3, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot1);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 4, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot2);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 5, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot3);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 6, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot4);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 7, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot5);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 8, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot6);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 9, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot7);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 10, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot8);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 11, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot9);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 12, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot10);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 13, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot11);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 14, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot12);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 15, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot13);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 16, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot14);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 17, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot15);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 18, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot16);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 19, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot17);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 20, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot18);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 21, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot19);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 22, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot20);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 23, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot21);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 24, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot22);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 25, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot23);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 26, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot24);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 27, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot25);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 28, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Angle1);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 29, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Angle2);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 31, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Angle3);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 33, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Angle4);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 35, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Angle5);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 37, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Angle6);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 39, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Angle7);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 41, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Angle8);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 43, tmpParam.Length);
            int CreateCharCount = CreateTime.ToCharArray().Length;
            for (int i = 0; i < CreateCharCount; i++)
            {
                tmpParam = BitConverter.GetBytes(CreateTime.ToCharArray()[i]);
                Buffer.BlockCopy(tmpParam, 0, byteBody, 45 + i, tmpParam.Length);
            }
            int ReviseCharCount = ReviseTime.ToCharArray().Length;
            for (int i = 0; i < ReviseCharCount; i++)
            {
                tmpParam = BitConverter.GetBytes(ReviseTime.ToCharArray()[i]);
                Buffer.BlockCopy(tmpParam, 0, byteBody, 46 + CreateCharCount, tmpParam.Length);
            }
            tmpParam = BitConverter.GetBytes(OffsetType);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 47 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(LJStdSurface);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 48 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(BTTH);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 49 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(S1_1x0);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 51 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(S1_1x1);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 53 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(S2_1x0);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 55 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(S2_1x1);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 57 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(S2_2x0);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 59 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(S2_2x1);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 61 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Range1);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 62 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Range2);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 63 + CreateCharCount + ReviseCharCount, tmpParam.Length);

            return byteBody;
        }

        public ProcessProgramData GetPPBody_Unformatted()
        {
            byte[] tmpParam;
            byte[] byteBody = Enumerable.Repeat<byte>(0, 200).ToArray();
            //tmpParam = BitConverter.GetBytes(Tank1_WaterPressure);
            //Buffer.BlockCopy(tmpParam, 0, byteBody, 0, tmpParam.Length);
            //tmpParam = BitConverter.GetBytes(Tank1_Voltage);
            //Buffer.BlockCopy(tmpParam, 0, byteBody, 4, tmpParam.Length);
            //tmpParam = BitConverter.GetBytes(Tank1_AirPressure);
            //Buffer.BlockCopy(tmpParam, 0, byteBody, 8, tmpParam.Length);
            //tmpParam = BitConverter.GetBytes(Tank2_WaterPressure);
            //Buffer.BlockCopy(tmpParam, 0, byteBody, 100, tmpParam.Length);
            //tmpParam = BitConverter.GetBytes(Tank2_Voltage);
            //Buffer.BlockCopy(tmpParam, 0, byteBody, 104, tmpParam.Length);
            //tmpParam = BitConverter.GetBytes(Tank2_AirPressure);
            //Buffer.BlockCopy(tmpParam, 0, byteBody, 108, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Rotate_Count);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 0, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Type);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 1, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(RepeatTimes);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 2, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(RepeatTimes_now);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 3, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot1);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 4, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot2);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 5, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot3);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 6, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot4);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 7, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot5);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 8, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot6);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 9, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot7);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 10, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot8);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 11, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot9);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 12, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot10);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 13, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot11);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 14, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot12);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 15, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot13);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 16, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot14);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 17, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot15);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 18, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot16);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 19, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot17);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 20, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot18);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 21, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot19);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 22, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot20);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 23, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot21);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 24, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot22);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 25, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot23);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 26, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot24);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 27, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Slot25);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 28, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Angle1);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 29, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Angle2);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 31, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Angle3);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 33, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Angle4);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 35, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Angle5);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 37, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Angle6);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 39, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Angle7);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 41, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Angle8);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 43, tmpParam.Length);
            int CreateCharCount = CreateTime.ToCharArray().Length;
            for (int i = 0; i < CreateCharCount; i++)
            {
                tmpParam = BitConverter.GetBytes(CreateTime.ToCharArray()[i]);
                Buffer.BlockCopy(tmpParam, 0, byteBody, 45 + i, tmpParam.Length);
            }
            int ReviseCharCount = ReviseTime.ToCharArray().Length;
            for (int i = 0; i < ReviseCharCount; i++)
            {
                tmpParam = BitConverter.GetBytes(ReviseTime.ToCharArray()[i]);
                Buffer.BlockCopy(tmpParam, 0, byteBody, 46 + CreateCharCount, tmpParam.Length);
            }
            tmpParam = BitConverter.GetBytes(OffsetType);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 47 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(LJStdSurface);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 48 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(BTTH);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 49 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(S1_1x0);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 51 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(S1_1x1);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 53 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(S2_1x0);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 55 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(S2_1x1);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 57 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(S2_2x0);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 59 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(S2_2x1);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 61 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Range1);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 62 + CreateCharCount + ReviseCharCount, tmpParam.Length);
            tmpParam = BitConverter.GetBytes(Range2);
            Buffer.BlockCopy(tmpParam, 0, byteBody, 63 + CreateCharCount + ReviseCharCount, tmpParam.Length);

            ProcessProgramData ppData = new ProcessProgramData();
            ppData.PPID = PPID;
            ppData.Format = ItemFmt.B;
            ppData.Body = byteBody;

            return ppData;
        }

        public FormattedProcessProgramData GetPPBody_Formatted(string modelType, string softRev)
        {
            FormattedProcessProgramData ppData = new FormattedProcessProgramData();
            ppData.PPID = PPID;
            ppData.MDLN = modelType;
            ppData.SOFTREV = softRev;
            /*
            CommandCode cCode1 = new CommandCode();
            cCode1.CCode = 100;
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.F4, Tank1_WaterPressure));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.I4, Tank1_Voltage));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.F4, Tank1_AirPressure));
            ppData.CCodes.Add(cCode1);
            CommandCode cCode2 = new CommandCode();
            cCode2.CCode = 200;
            cCode2.Parameters.Add(SecsIIValue.Create(ItemFmt.F4, Tank2_WaterPressure));
            cCode2.Parameters.Add(SecsIIValue.Create(ItemFmt.I4, Tank2_Voltage));
            cCode2.Parameters.Add(SecsIIValue.Create(ItemFmt.F4, Tank2_AirPressure));
            ppData.CCodes.Add(cCode2);
            */
            return ppData;
        }

        public FormattedProcessProgramData2 GetPPBody_Formatted2(string modelType, string softRev)
        {
            FormattedProcessProgramData2 ppData = new FormattedProcessProgramData2();
            ppData.PPID = PPID;
            ppData.MDLN = modelType;
            ppData.SOFTREV = softRev;

            CommandCode2 cCode1 = new CommandCode2();
            cCode1.CCode = SecsIIValue.Create(ItemFmt.A, "100");
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Rotate_Count));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Type));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, RepeatTimes));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, RepeatTimes_now));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot1));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot2));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot3));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot4));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot5));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot6));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot7));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot8));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot9));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot10));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot11));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot12));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot13));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot14));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot15));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot16));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot17));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot18));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot19));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot20));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot21));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot22));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot23));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot24));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Slot25));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, Angle1));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, Angle2));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, Angle3));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, Angle4));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, Angle5));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, Angle6));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, Angle7));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, Angle8));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.A, CreateTime));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.A, ReviseTime));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, OffsetType));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, LJStdSurface));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, BTTH));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, S1_1x0));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, S1_1x1));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, S2_1x0));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, S2_1x1));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, S2_2x0));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U2, S2_2x1));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Range1));
            cCode1.Parameters.Add(SecsIIValue.Create(ItemFmt.U1, Range2));

            ppData.CCodes.Add(cCode1);

            return ppData;
        }

        public ListWrapper GetPPBody_ListWrapper(string modelType, string softRev)
        {
            string err = string.Empty;

            ListWrapper lw = new ListWrapper();
            lw.TryAdd(ItemFmt.A, PPID, out err);
            lw.TryAdd(ItemFmt.A, modelType, out err);
            lw.TryAdd(ItemFmt.A, softRev, out err);
            ListWrapper lw_1 = new ListWrapper();
            lw.TryAdd(ItemFmt.L, lw_1, out err);
            //CCode 100
            ListWrapper lw_1_1 = new ListWrapper();
            lw_1.TryAdd(ItemFmt.L, lw_1_1, out err);
            lw_1_1.TryAdd(ItemFmt.U2, (ushort)100, out err);
            ListWrapper lw_1_1_1 = new ListWrapper();
            lw_1_1.TryAdd(ItemFmt.L, lw_1_1_1, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Rotate_Count, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Type, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, RepeatTimes, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, RepeatTimes_now, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot1, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot2, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot3, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot4, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot5, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot6, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot7, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot8, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot9, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot10, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot11, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot12, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot13, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot14, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot15, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot16, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot17, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot18, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot19, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot20, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot21, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot22, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot23, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot24, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Slot25, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Angle1, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Angle2, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Angle3, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Angle4, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Angle5, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Angle6, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Angle7, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Angle8, out err);
            lw_1_1_1.TryAdd(ItemFmt.A, CreateTime, out err);
            lw_1_1_1.TryAdd(ItemFmt.A, ReviseTime, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, OffsetType, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, LJStdSurface, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, BTTH, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, S1_1x0, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, S1_1x1, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, S2_1x0, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, S2_1x1, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, S2_2x0, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, S2_2x1, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Range1, out err);
            lw_1_1_1.TryAdd(ItemFmt.U4, Range2, out err);

            return lw;
        }

    }

}
