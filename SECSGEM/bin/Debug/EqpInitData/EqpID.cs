static class EqpID
{
// Event ID 
public const int EQP_EVENT_1 = 100;          //UINT_4    Eqp1Event
public const int EQP_EVENT_2 = 101;          //UINT_4    Eqp2Event
public const int EQP_EVENT_3 = 102;          //UINT_4    Eqp3Event
public const int EQP_EVENT_4 = 103;          //UINT_4    Eqp4Event
public const int EQP_EVENT_5 = 104;          //UINT_4    Eqp5Event
public const int EQP_EVENT_6 = 105;          //UINT_4    Eqp6Event

// SV ID 
public const int SV_BOOLEAN = 2001;          //BOOLEAN   BooleanTypeStatus
public const int SV_CONTROL_JOB_NAME = 2002; //ASCII     ControlJobName
public const int SV_INMP1_NOW_TEMP = 2003;   //FT_4      INMP1NowTemp
public const int SV_BINARY = 2004;           //BINARY    BinaryTypeStatus
public const int LIST_SV1 = 3001;            //LIST      WaferMappingList
public const int LIST_SV2 = 3002;            //LIST      WaferStatusList
public const int SV_EQP_STSTUS = 100001;     //UINT_1    EquipmentStatus
public const int SV_UINT_4 = 100002;         //UINT_4    UINT_4typeStatus

// DV ID 
public const int DV_8100 = 8100;             //FT_4      DVID_8100
public const int DV_8101 = 8101;             //UINT_1    DVID_8101
public const int DV_8102 = 8102;             //UINT_2    DVID_8102
public const int DV_8103 = 8103;             //UINT_4    DVID_8103
public const int DV_8104 = 8104;             //FT_4      DVID_8104
public const int DV_8105 = 8105;             //INT_1     DVID_8105
public const int DV_8106 = 8106;             //INT_2     DVID_8106
public const int DV_8107 = 8107;             //INT_4     DVID_8107
public const int DV_8108 = 8108;             //ASCII     DVID_8108
public const int DV_8109 = 8109;             //BOOLEAN   DVID_8109

// EConst ID 
public const int EC1_UINT_1 = 1001;          //UINT_1    EC1def
public const int EC2_UINT_2 = 1002;          //UINT_2    EC2def
public const int EC3_FT_4 = 1003;            //FT_4      EC3def
public const int EC4_FT_8 = 1004;            //FT_8      EC4def
public const int EC5_UINT_4 = 1005;          //UINT_4    EC5def
public const int EC6_INT_1 = 1006;           //INT_1     EC6def
public const int EC7_INT_2 = 1007;           //INT_2     EC7def
public const int EC8_INT_4 = 1008;           //INT_4     EC8def
public const int EC9_BINARY = 1009;          //BINARY    EC9def
public const int EC10_BOOLEAN = 1010;        //BOOLEAN   EC10def
public const int EC11_ASCII = 1011;          //ASCII     EC11def

// Alarm ID 
public const int ALARM_PUMP_PRESS = 9001;    //UINT_4    壓力異常PumpPressFressl
public const int ALARM_EM = 9002;            //UINT_4    EFEM緊急停止開關按下EmergencyStoppressed
public const int ALARM_HEATER_FAIL = 9003;   //UINT_4    加熱器失效HeaterFail
public const int ALARM_HEATER_FAIL2 = 1000001;                                                                                    //UINT_4加

}