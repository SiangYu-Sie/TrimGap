public static class GemSystemID
{
    //ECV's:
    //GEM-Stander:
    public const int GEM_ESTAB_COMM_DELAY = 44;       //U2        GemEstabCommDelay
    public const int GEM_MAX_SPOOL_TRANSMIT = 52;     //U4        GemMaxSpoolTransmit
    public const int GEM_OVER_WRITE_SPOOL = 67;       //BOOLEAN   GemOverWriteSpool       'T:Overwrite, F:Do Not Overwrite
    //Non GEM-Stander:
    public const int GEM_CONFIG_SPOOL = 66;           //U1        GemConfigSpool          '0:Disable, 1:Enable
    public const int GEM_INIT_COMM_STATE = 7;         //U1        GemInitCommState        ' 0:Disable, 1:Enable
    public const int GEM_INIT_CONTROL_STATE = 8;      //U1        GemInitControlState     '1:OffLine, 2:OnLine
    public const int GEM_LIMIT_DELAY = 65;            //U2        GemLimitDelay           'Units: sec
    public const int GEM_OFF_LINE_SUBSTATE = 49;      //U1        GemOffLineSubstate      ' 1: Eqp Off Line, 2:Attempt On Line, 3:Host Off Line
    public const int GEM_ON_LINE_FAILED = 50;         //U1        GemOnlineFailed         '1: Eqp Off Line, 3:Host Off Line
    public const int GEM_ON_LINE_SUBSTATE = 51;       //U1        GemOnLineSubstate,      '4:On-line/Local, 5:ON-line/Remote
    public const int GEM_POLL_DELAY = 26;             //U2        GemPollDelay            ' 0:no S1F1 sent, >0: delay time sec's
    public const int GEM_DATAID_FORMAT = 71;          //U1        GemDATAIDFormat         '1:INT_1, 2:INT_2, 3:INT_4, 4:UINT_1, 4:UINT_1

    public const int GEM_RPTID_FORMAT = 73;           //U1        GemRPTIDFormat
    public const int GEM_TIME_FORMAT = 68;            //U1        GemTimeFormat           ' 0:12-bytes, 1:16-bytes, 2:14-bytes 3:ISO8601 format
    public const int GEM_TRID_FORMAT = 74;            //U1        GemTRIDFormat
    public const int GEM_SAMPLN_FORMAT = 75;          //U1        GemSAMPLNFormat         '1:INT_1, 2:INT_2, 3:INT_4, 4:UINT_1, 5:UINT_2, 6:UINT_4
    public const int GEM_WBIT_S5 = 21;                //U1        GemWBitS5
    public const int GEM_WBIT_S6 = 22;                //U1        GemWBitS6
    public const int GEM_WBIT_S10 = 23;               //U1        GemWBitS10

    // SV's:
    //GEM-Stander:
    public const int GEM_ALARM_ENABLED = 39;          //List      GemAlarmsEnabled
    public const int GEM_ALARM_SET = 40;              //List      GemAlarmSet
    public const int GEM_CLOCK = 3;                   //A[16]     GemClock
    public const int GEM_CONTROL_STATE = 4;           //U1        GemControlState ' 1:Eqp OffLine, 2:OnLine Local, 3:OnLine Remote
    public const int GEM_OFF_LINE_SUB_STATE_SV = 12;  //U1        GemOffLineSubStateSV    ' 1:Eqp OffLine, 2:Attempt OnLine, 3:HostOffLiine, 0:unknow

    public const int GEM_EVENT_ENABLED = 41;          //List      GemEventsEnabled
    public const int GEM_PP_EXEC_NAME = 42;           //A         GemPPExecName
    public const int GEM_PREVIOUS_PROCESS_STATE = 14; //U1        GemPreviousProcessState
    public const int GEM_PROCESS_STATE = 15;          //U1        GemProcessState
    public const int GEM_SPOOL_COUNT_ACTUAL = 53;     //U4        GemSpoolCountActual
    public const int GEM_SPOOL_COUNT_TOTAL = 54;      //U4        GemSpoolCountTotal
    public const int GEM_SPOOL_FULL_TIME = 55;        //A[16]     GemSpoolFullTime
    public const int GEM_SPOOL_START_TIME = 57;       //A[16]     GemSpoolStartTime
    //Non GEM-Stander:
    public const int GEM_LINK_STATE = 5;              //U1        GemLinkState  '0:Disabled, 1:Enabled/Not Communicating , 2: Communicating
    public const int GEM_COMM_MODE = 6;               //U1        SECS Communication Mode '0:HSMS, 1:SECS I (Must Set By AP)
    public const int GEM_MAX_SPOOL_SIZE = 76;         //U4        GemMaxSpoolSize
    public const int GEM_MDLN = 24;                   //A[0..6]   GemMDLN
    public const int GEM_PREVIOUS_CEID = 11;          //U4        GemPreviousCEID
    public const int GEM_PREVIOUS_CONTROL_STATE = 13; //U1        GemPreviousControlState '1:Eqp OffLine, 2:OnLine Local, 3:OnLine Remote
    public const int GEM_SOFTREV = 25;                //A[0..6]   GemSOFTREV
    public const int GEM_SPOOL_STATE = 58;            //U1        GemSpoolState  '1:Inactive , 2:Active
    public const int GEM_SPOOL_LOAD_SUBSTATE = 56;    //U1        GemSpoolLoadSubstate   ' 6:Not Full , 7:Full
    public const int GEM_SPOOL_UNLOAD_SUBSTATE = 59;  //U1        GemSpoolUnloadSubstate '3:Purge , 4:transmit , No Spool Out
    public const int GEM_TIME = 17;                   //A16, A12  GemTime
    public const int GEM_SOFTWARE_REVISION = 80;      //A[0..20]  Gem Soft Ware Revision
    public const int LICENSE_CODE_SVID = 1;           //A[39]     License Code,(Read Only)
    public const int LICENSE_STATUS_SVID = 2;         //U1        License Status,"0:No License, 1:Good License (Read Only)"
    public const int PP_FORMAT = 43;                  //U1        1: Unformatted process programs(default), 2: Formatted process programs, 3 Both unformatted and formatted process programs

    // DVVAL's (目前暫時由 SV 管理):
    //GEM-Stander:
    public const int GEM_ALARM_ID = 38;               //U4        GemAlarmID
    public const int GEM_ECID_CHANGED = 46;           //U4        GemECIDChanged
    public const int GEM_EVENT_LIMIT = 63;            //B         GemEventLimit
    public const int GEM_LIMIT_VID = 62;              //U4        GemLimitVID
    public const int GEM_PP_CHANGE_NAME = 9;          //A[0..80]  GemPPChangeName
    public const int GEM_PP_CHANGE_STATUS = 10;       //U1        GemPPChangeStatus '1:Create, 2:Changed, 3Deleded
    public const int GEM_TRANSTION_TYPE = 64;        //B         GemTransitionType ' 0x00:Lower->Upper, 0x01:Upper->Lower
    //Non-GEM-Stander:
    public const int GEM_EC_VALUE_CHANGED = 47;       //A         Gem EC value be changed by OP
    public const int GEM_PREVIOUS_EC_VALUE = 48;      //A         Gem previous EC value be changed by OP

                                                                                                                                                       
    //Event ID:
    //GEM-Stander:
    public const int GEM_EQP_OFF_LINE = 22;           //GemEqpOffLine
    public const int GEM_CONTROL_STATE_LOCAL = 8;     //GemControlStateLocal
    public const int GEM_CONTROL_STATE_REMOTE = 9;    //GemControlStateRemote
    public const int GEM_EQ_CONST_CHANGED = 20;       //GemEqConstChanged
    public const int GEM_LIMIT_ZONE_TRANSITION = 51;  //GemLimitZoneTransition
    public const int GEM_PP_CHANGE = 3;               //GemProcessProgramChange
    public const int GEM_SPOOLING_ACTIVED = 23;       //GemSpoolingActived
    public const int GEM_SPOOLING_DEACTIVED = 24;     //GemSpoolingDeactived
    public const int GEM_SPOOL_TRANSMIT_FAILURE = 25; //GemSpoolTransmitFailure
    public const int GEM_MESSAGE_RECOGNITION = 21;    //GemMessageRecognition
}