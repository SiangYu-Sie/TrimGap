using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECSGEM
{
    static class SECSGEMDefine
    {
        public enum SECS_I_Comm_Mode
        {
            SECS_EQUIP_MODE = 0,
            SECS_HOST_MODE = 1
        }

        public enum HSMS_SS_Comm_Mode
        {
            HSMS_PASSIVE_MODE = 0,
            HSMS_ACTIVE_MODE = 1
        }

        public enum SECS_Comm_Mode
        {
            HSMS_MODE = 0,
            SECS_MODE = 1
        }
        
        public struct HSMS_SS_Parameters
        {
            public int T5;
            public int T6;
            public int T7;
            public int T8;
            public int LinkTestPeriod;
            public string LocalIP;
            public int LocalPort;
            public string RemoteIP;
            public int RemotePort;
            public HSMS_SS_Comm_Mode HSMS_SS_Conect_Mode;
        }
        
        public struct SECS_Common_Parameters
        {
            public int T3;
            public int DeviceID;
            public SECS_Comm_Mode SECS_Connect_Mode;
        }

        public static HSMS_SS_Parameters HSMS_SS_Params;
        public static SECS_Common_Parameters SECS_Common_Params; 
    }
}
