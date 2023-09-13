using System;
using System.IO;
using System.Text;
using System.Reflection;

namespace TcpTest
{
	class Log
	{
		const string format = "yyyyMMdd_HHmmss";

		static Log _instance;

		/// <summary>LogFileDir</summary>
		string _logFileDir => String.Format(@"{0}\{1}", @"D:\", nameof(Log));
		/// <summary>LogFileName</summary>
		string _logFileName => $"{_logFileDir}\\{nameof(Log)}_{DateTime.Now.ToString(format)}.{nameof(Log).ToLower()}";

		StreamWriter _logFileStream;
		FileStream _stream;    
		int _intValidityTerm;       
		DateTime _datLastLogCreate; 
		bool _blnLogEnable;

		static object _lockObject = new object(); 


		private static object lockObject = new object();

		public static event Action<string, string, string> addLogEvent;

		Log(int intValidityTerm, bool blnEnable)
		{
			Initialize(intValidityTerm, blnEnable);
		}

		bool Initialize(int intValidityTerm, bool blnEnable) 
		{
			_intValidityTerm = intValidityTerm;
			_blnLogEnable = blnEnable;
			try
			{
				if (!Directory.Exists(_logFileDir))
				{
					Directory.CreateDirectory(_logFileDir);
				}
				_stream = File.Open(_logFileName, FileMode.Create, FileAccess.Write, FileShare.Read);
				_logFileStream = new StreamWriter(_stream, Encoding.GetEncoding("Shift_JIS"));

				_datLastLogCreate = DateTime.Now;

				DeleteLogfile();
				WriteTitle();

			}
			catch (Exception ex)
			{
				AddLog(ex.Message, nameof(Log));
				return false;
			}

			return true;
		}

		public static Log CreateInstance(int intValidTerm, bool blnEnable)
		{
			lock (_lockObject)
			{
				if (_instance == null)
				{
					_instance = new Log(intValidTerm, blnEnable);
				}
				return _instance;
			}
		}

        void ChangeDayProc()
        {
            if (_datLastLogCreate.Day.CompareTo(DateTime.Now.Day) < 0)
            {
                _datLastLogCreate = DateTime.Now;

                //DeleteLog();
                DeleteLogfile();

                try
                {
                    _logFileStream.Close();
                    _stream =
                        File.Open(_logFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                    _logFileStream =
                        new StreamWriter(_stream, Encoding.GetEncoding("Shift_JIS"));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine ("SF3 TCP Tester : (Log Class)", " Daily Logfile Change Error " + ex.Message);
                }
            }
        }

        public void AddLog(string strLog, string strDevice)
        {
            var strDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ffff");
            if (_blnLogEnable)
            {
                lock (_lockObject)
                {
                    ChangeDayProc();

                    strLog = strLog.Replace("\r\n", "<CRLF>");
                    strLog = strLog.Replace("\r", "<CR>");
                    strLog = strLog.Replace("\n/", "<LF>");
                    strDevice = (strDevice + new string((char)0x20, 10)).Substring(0, 10);

                    var strMessage = $"{strDate} {strDevice} {strLog}";

                    _logFileStream.WriteLine(strMessage);
                    _logFileStream.Flush();
                }
            }
            if (addLogEvent != null)
                addLogEvent(strDate, strDevice, strLog);
        }

        bool WriteTitle()
        {
            _logFileStream.WriteLine("SF3 TCP Tester");
            var assembly = Assembly.GetExecutingAssembly().GetName();
            _logFileStream.WriteLine(assembly.Version);
            _logFileStream.WriteLine($"--------- Start SF3 TCP Tester {DateTime.Now.ToLocalTime()} ---------");
            _logFileStream.Flush();
            return true;
        }

        bool DeleteLog()
        {
            string[] fileEntries;
            try
            {
                fileEntries = Directory.GetFiles(_logFileDir, "*.log");
            }
            catch
            {
                return false;
            }

            foreach (var fileName in fileEntries)
            {
                if (!fileName.Substring(fileName.Length - 3).ToUpper().Equals("LOG")) continue;

                var strDate = fileName.Substring(_logFileName.Length - format.Length - 4, format.Length);
                try
                {
                    var datCreateDate = DateTime.ParseExact(strDate, format, null);
                    datCreateDate = datCreateDate.AddDays(_intValidityTerm);
                    if (datCreateDate.CompareTo(DateTime.Now) < 0)
                    {
                        File.Delete(fileName);
                    }
                }
                catch (Exception ex)
                {
                    AddLog(ex.Message, nameof(Log));
                }
            }

            return true;
        }
        bool DeleteLogfile()
        {
            string[] entryFiles;
            try
            {
                entryFiles = Directory.GetFiles(_logFileDir, "*.log");
            }
            catch
            {
                return false;
            }

            if (_intValidityTerm < 1) return true;

            foreach (var deleteFile in entryFiles)
            {
                try
                {
                    var lastTime = File.GetLastWriteTime(deleteFile);
                    var validTime = lastTime.AddDays(_intValidityTerm);
                    if (validTime.CompareTo(DateTime.Now) < 0)
                    {
                        File.Delete(deleteFile);
                    }
                }
                catch (Exception ex)
                {
                    AddLog(ex.Message, nameof(Log));
                }
            }

            return true;
        }
    }
}
