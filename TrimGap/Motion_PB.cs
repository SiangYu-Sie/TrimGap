using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mc2xxstd;

namespace Motion
{
    public struct PRM_TBL
    {
        public int axis_num;                    /* axis number [ 0: system, 1-32: axis ]							*/
        public short prm_num;                   /* parameter number													*/
        public short prm_data;                  /* parameter data													*/

        public PRM_TBL(int ax_num, short p_num, short p_data)
        {
            axis_num = ax_num;
            prm_num = p_num;
            prm_data = p_data;
        }
    };
    public class Motion_PB
    {
        #region 常數
        public const int AXIS_MAX = 32;
        public const int AXIS_MIN = 1;
        public const int DRIVE_FIN_TIMEOUT = 5000;
        public const int RDY_ON_TIMEOUT = 5000;
        public const short THREAD_PRIORITY_TIME_CRITICAL = 15;
        public const short INTERRUPT_THREAD_PRIORITY = THREAD_PRIORITY_TIME_CRITICAL;
        public const string PRM_DEFAULT_PATH = "Default.prm2";
        #endregion

        #region Variable變數
        static int board_id = 0;
        static int channel = 1;
        static int exist_axis = 0x00000001;         /* exist axis flag [ bit0: axis1, bit1: axis2, ... bit31: axis32 ]	*/

        public bool[] axis_Status = new bool[AXIS_MAX];
        double[] virPos = new double[AXIS_MAX];
        public bool bVir = false;
        bool[] virServo = new bool[AXIS_MAX];
        int[] virIdle = new int[AXIS_MAX];
        double[] virTarget = new double[AXIS_MAX];
        double[] virSpeed = new double[AXIS_MAX];

        bool load_lib_flg;
        int ans;
        public string prmFilePath = null;
        public int[] m_aCmdPos = new int[AXIS_MAX];
        public int[] m_aRealPos = new int[AXIS_MAX];
        public bool bTestMode = false;
        public bool bInitial = false;
        public short[,] aDefPar = new short[37, 65536];
        public static int openFormNum = 0;
        public int[] m_aFinStatus = new int[AXIS_MAX];
        public int[] m_aHomePos = new int[AXIS_MAX];
        public bool[] m_abFirstFindHome = new bool[AXIS_MAX];
        #endregion

        #region UI視窗
        public void manualUIShow()
        {
            if(openFormNum == 0)
            {
                FormMotionUI mainForm = new FormMotionUI(this);
                mainForm.testMode(bTestMode);
                openFormNum++;
                mainForm.ShowDialog();
                openFormNum--;
            }
        }
        public void setTestMode(bool bOn)
        {
            bTestMode = bOn;
        }
        #endregion

        #region 初始化
        public bool initial()
        {            
            for (int i = 0; i < AXIS_MAX; i++)
            {
                virPos[i] = 0;
                virServo[i] = false;
                virIdle[i] = 0;
                virTarget[i] = 0;
                virSpeed[i] = 0;
                m_aFinStatus[i] = 0;
                m_aHomePos[i] = 0;
                m_abFirstFindHome[i] = false;
            }

            if(!bVir)
            {
                load_lib_flg = SscApi.LoadLibraryDll();
                if (load_lib_flg == false)
                {
                    MessageBox.Show("Load PB DLL fail");
                    bInitial = false;
                    return (bInitial);
                }
                ans = StartSscnet();
                if (ans != SscApi.SSC_OK)
                {
                    /* stop sscnet system */
                    CloseSscnet();

                    /* free libfary */
                    SscApi.FreeLibraryDll();
                    bInitial = false;
                    return (bInitial);
                }
            }
            bInitial = true;
            return (bInitial);
        }
        public void setPrmPath(string path)
        {
            prmFilePath = path;
        }

        public void virMove(bool on)
        {
            bVir = on;
        }
        int StartSscnet()
        {
            int ans;
            int axis_cnt;
            ushort code;
            ushort detail_code;

            /* open device driver */
            ans = SscApi.sscOpen(board_id);
            if (ans != SscApi.SSC_OK)
            {
                MessageBox.Show("sscOpen failure. sscGetLastError=0x"+SscApi.sscGetLastError().ToString("X8"));
                return (SscApi.SSC_NG);
            }

            /* reboot system */
            ans = SscApi.sscReboot(board_id, channel, SscApi.SSC_DEFAULT_TIMEOUT);
            if (ans != SscApi.SSC_OK)
            {
                MessageBox.Show("sscReboot failure. sscGetLastError=0x" + SscApi.sscGetLastError().ToString("X8"));
                return (SscApi.SSC_NG);
            }

            /* set default parameter */
            ans = SscApi.sscResetAllParameter(board_id, channel, SscApi.SSC_DEFAULT_TIMEOUT);
            if (ans != SscApi.SSC_OK)
            {
                MessageBox.Show("sscResetAllParameter failure. sscGetLastError=0x" + SscApi.sscGetLastError().ToString("X8"));
                return (SscApi.SSC_NG);
            }
            //MessageBox.Show("afterResetAllPar");
            /* change parameter */
            if (prmFilePath != null)
            {
                
                for(int i=0; i<37; i++)
                {
                    for(int j=0;j<65536; j++)
                    {
                        aDefPar[i, j] = 0;
                    }
                }
                string[] aTmp;
                string tmp;
                FileInfo f1 = new FileInfo(PRM_DEFAULT_PATH);
                StreamReader sr = f1.OpenText();
                while (sr.Peek() >= 0)
                {
                    tmp = sr.ReadLine();
                    aTmp = tmp.Split(',');
                    aDefPar[Convert.ToInt16(aTmp[0]) + 4, Convert.ToInt16(aTmp[1],16)] = Convert.ToInt16(aTmp[2],16);
                }
                //MessageBox.Show("after sr1");
                FileInfo f2 = new FileInfo(prmFilePath);
                StreamReader sr2 = f2.OpenText();
                while (sr2.Peek() >= 0)
                {
                    tmp = sr2.ReadLine();
                    aTmp = tmp.Split(',');
                    if (aDefPar[Convert.ToInt16(aTmp[0]) + 4, Convert.ToInt16(aTmp[1],16)] != Convert.ToInt16(aTmp[2],16))
                    {
                        ans = SscApi.sscChangeParameter(board_id, channel, Convert.ToInt16(aTmp[0]), Convert.ToInt16(aTmp[1], 16), Convert.ToInt16(aTmp[2], 16));
                        aDefPar[Convert.ToInt16(aTmp[0]) + 4, Convert.ToInt16(aTmp[1], 16)] = Convert.ToInt16(aTmp[2], 16);
                    }                        
                    if (ans != SscApi.SSC_OK)
                    {
                        MessageBox.Show("sscChangeParameter failure. sscGetLastError=0x" + SscApi.sscGetLastError().ToString("X8")+ aTmp[0]+aTmp[1] + "(" + (Convert.ToInt16(aTmp[1], 16)).ToString() + ")" + aTmp[2]+"("+ (Convert.ToInt16(aTmp[2], 16)).ToString() + ")");
                        return (SscApi.SSC_NG);
                    }
                }
                //MessageBox.Show("after sr2");
            }

            short rtn;
            for(int i=0; i<AXIS_MAX; i++)
            {
                getPar1(i, 0x0200, out rtn);
                axis_Status[i] = rtn % 2 == 1 ? true : false;
            }

            /* start system */
            ans = SscApi.sscSystemStart(board_id, channel, SscApi.SSC_DEFAULT_TIMEOUT);
            if (ans != SscApi.SSC_OK)
            {
                MessageBox.Show("sscSystemStart failure. sscGetLastError=0x" + SscApi.sscGetLastError().ToString("X8"));
                return (SscApi.SSC_NG);
            }

            /* check control alarm */
            ans = SscApi.sscGetAlarm(board_id, channel, 0, SscApi.SSC_ALARM_SYSTEM, out code, out detail_code);
            if (ans != SscApi.SSC_OK)
            {
                MessageBox.Show("sscGetAlarm(system alarm) failure. sscGetLastError=0x" + SscApi.sscGetLastError().ToString("X8"));
                return (SscApi.SSC_NG);
            }
            else if (code != 0)
            {
                /*======================================================*/
                /* Please add processing to this position if necessary. */
                /*======================================================*/

                MessageBox.Show("system alarm : 0x" +  code.ToString("X2") +"(0x"+ detail_code.ToString("X2")+")");
                return (SscApi.SSC_NG);
            }

            /* check axis alarm */
            for (axis_cnt = 0; axis_cnt < AXIS_MAX; axis_cnt++)
            {
                if ((exist_axis & (1 << axis_cnt)) == SscApi.SSC_BIT_OFF)
                {
                    continue;
                }

                /* check operation alarm */
                ans = SscApi.sscGetAlarm(board_id, channel, axis_cnt + 1, SscApi.SSC_ALARM_OPERATION, out code, out detail_code);
                if (ans != SscApi.SSC_OK)
                {
                    MessageBox.Show("sscGetAlarm(operation alarm) failure. sscGetLastError=0x" + SscApi.sscGetLastError().ToString("X8"));
                    return (SscApi.SSC_NG);
                }
                else if (code != 0)
                {
                    /*======================================================*/
                    /* Please add processing to this position if necessary. */
                    /*======================================================*/

                    MessageBox.Show("operation alarm : 0x"+ code.ToString("X2")+ "(0x"+ detail_code.ToString("X2")+")");
                    return (SscApi.SSC_NG);
                }

                /* check servo alarm */
                ans = SscApi.sscGetAlarm(board_id, channel, axis_cnt + 1, SscApi.SSC_ALARM_SERVO, out code, out detail_code);
                if (ans != SscApi.SSC_OK)
                {
                    MessageBox.Show("sscGetAlarm(servo alarm) failure. sscGetLastError=0x" + SscApi.sscGetLastError().ToString("X8"));
                    return (SscApi.SSC_NG);
                }
                else if (code != 0)
                {
                    /*======================================================*/
                    /* Please add processing to this position if necessary. */
                    /*======================================================*/

                    MessageBox.Show("servo alarm : 0x"+ code.ToString("X2")+ "(0x"+ detail_code.ToString("X2")+")");
                    return (SscApi.SSC_NG);
                }
            }

            /*======================================================*/
            /* Please add processing to this position if necessary. */
            /*======================================================*/
            return (SscApi.SSC_OK);
        }

        static int CloseSscnet()
        {
            int ans;

            /*======================================================*/
            /* Please add processing to this position if necessary. */
            /*======================================================*/

            /* close device driver */
            ans = SscApi.sscClose(board_id);
            if (ans != SscApi.SSC_OK)
            {
                MessageBox.Show("sscClose failure. sscGetLastError=0x"+SscApi.sscGetLastError().ToString("X8"));
                return (SscApi.SSC_NG);
            }

            return (SscApi.SSC_OK);
        }
        #endregion

        #region 關閉
        public bool close()
        {
            if(!bVir)
            {
                CloseSscnet();
            }
            return true;
        }
        #endregion

        #region 軸參數設定
        public void setPlsOutmode(short axis, short mode)
        {
            //C8154._8154_set_pls_outmode(axis, mode);
        }
        public void setAlm(short axis, short alarm, short mode)
        {
           // C8154._8154_set_alm(axis, alarm, mode);
        }
        public void setLimitLogic(short axis, short el)
        {
           // C8154._8154_set_limit_logic(axis, el);
        }
        public void setHomeConfig(short axis, short mode, short orgLogic, short ezLogic, short ezCount, short ercOut)
        {
           // C8154._8154_set_home_config(axis, mode, orgLogic, ezLogic, ezCount, ercOut);
        }
        #endregion

        #region 運動功能
        public void findHome(short axis, int directionFactor)
        {
            if (bVir)
            {
                if (!virServo[axis]) return;
                virPos[axis] = 0;
                virTarget[axis] = 0;
                virIdle[axis] = 1;
            }
            else
            {
                int ans, axnum;
                axnum = axis + 1;
                ans = SscApi.sscHomeReturnStart(board_id, channel, axnum);
                if (ans != SscApi.SSC_OK)
                    MessageBox.Show("sscHomeReturnStart failure. axnum="+ axnum.ToString()+", sscGetLastError=0x"+SscApi.sscGetLastError().ToString("X8"));
            }
        }
        public void setHome(short axis)
        {
            if (bVir)
            {
                virPos[axis] = 0;
                virTarget[axis] = 0;
                virIdle[axis] = 1;
            }
            else
            {
                int ans, axnum;
                axnum = axis + 1;
                ans = SscApi.sscDataSetStart(board_id, channel, axnum);
                if (ans != SscApi.SSC_OK)
                    MessageBox.Show("sscDataSetStart failure. axnum=" + axnum.ToString() + ", sscGetLastError=0x" + SscApi.sscGetLastError().ToString("X8"));
            }
        }
        public void stop(short axis)
        {//停止
            if (!bVir)
            {
                short bStopFinish, axnum;
                axnum = (short)(axis + 1);
                //ans = SscApi.sscDriveStopNoWait(board_id, channel, axnum, out bStopFinish);
                ans = SscApi.sscDriveStop(board_id, channel, axnum, 1000);
            }
            else
            {              
                virTarget[axis] = virPos[axis];
                virIdle[axis] = 0;
                virSpeed[axis] = 0;
            }
        }
        public void stopAll()
        {//停止
            for (int i = 0; i < AXIS_MAX; i++)
            {
                if (axis_Status[i])
                {
                    stop((short)i);
                }
            }
        }
        public void incStart(short axis, double dist, double strVel, double maxVel, double acc, double dec)
        {//開始
            if (bVir)
            {
                if (!virServo[axis]) return;

                virTarget[axis] = virPos[axis] + dist;

                if (dist >= 0)
                    virSpeed[axis] = maxVel;
                else
                    virSpeed[axis] = -1 * maxVel;

                virIdle[axis] = 1;
            }
            else
            {
                if (!axis_Status[axis]) return;
                int ans, axnum, pos, vel;
                short ac, dc;
                axnum = axis  + 1;
                pos = (int)dist;
                vel = (int)maxVel;
                ac = (short)acc;
                dc = (short)dec;
                ans = SscApi.sscIncStart(board_id, channel, axnum, pos, vel, ac, dc);
                if (ans != SscApi.SSC_OK)
                    MessageBox.Show("Inc start failure. axnum="+ axnum.ToString()+", sscGetLastError=0x"+SscApi.sscGetLastError().ToString("X8"));
            }
        }
        //Jog運動
        public void jogStart(short axis, double strVel, double maxVel, double acc)
        {
            if (bVir)
            {
                if (!virServo[axis]) return;

                virTarget[axis] = virPos[axis] + maxVel * 1000;
                virSpeed[axis] = maxVel;
                virIdle[axis] = 1;
            }
            else
            {
                int ans, axnum, vel;
                short ac, dc;
                sbyte dir;
                axnum = axis + 1;
                vel = (int)maxVel;
                ac = (short)acc;
                dc = (short)acc;
                if (vel < 0)
                {
                    dir = SscApi.SSC_DIR_MINUS;
                    vel = -1 * vel;
                }
                else
                    dir = SscApi.SSC_DIR_PLUS;
                ans = SscApi.sscJogStart(board_id, channel, axnum, vel, ac, dc, dir);
                if (ans != SscApi.SSC_OK)
                    MessageBox.Show("jog start failure. axnum=" + axnum.ToString() + ", sscGetLastError=0x" + SscApi.sscGetLastError().ToString("X8"));
            }
        }
        public void jogStop(short axis)
        {
            stop(axis);
        }

        public void setServo(short axis, bool bOn)
        {
            if (bVir)
            {
                virServo[axis] = bOn;
            }
            else
            {
                int ans, axnum, on;
                axnum = axis + 1;
                on = bOn ? SscApi.SSC_BIT_ON : SscApi.SSC_BIT_OFF;
                ans = SscApi.sscSetCommandBitSignalEx(board_id, channel, axnum, SscApi.SSC_CMDBIT_AX_SON, on);
                
                if (ans != SscApi.SSC_OK)
                    MessageBox.Show("Servo Control failure. axnum=" + axnum.ToString() + ", sscGetLastError=0x", SscApi.sscGetLastError().ToString("X8"));
            }
        }

        public bool servoReady(short axis)
        {
            if (bVir)
            {
                return virServo[axis];
            }
            else
            {
                int ans, axnum, rtn;
                axnum = axis + 1;
                ans = SscApi.sscGetStatusBitSignalEx(board_id, channel, axnum, SscApi.SSC_STSBIT_AX_RDY, out rtn);
                if (ans != SscApi.SSC_OK)
                    MessageBox.Show("Get Servo Ready failure. axnum=" + axnum.ToString() + ", sscGetLastError=0x", SscApi.sscGetLastError().ToString("X8"));
               
                return (rtn == 1);
            }
        }

        public bool motionDone(short axis)
        {
            if (bVir)
            {
                return virIdle[axis]!= 2;
            }
            else
            {
                int ans, axnum, fin_status;
                axnum = axis + 1;

                ans = SscApi.sscGetDriveFinStatus(board_id, channel, axnum, SscApi.SSC_FIN_TYPE_INP, out fin_status);

                if (ans != SscApi.SSC_OK)
                    MessageBox.Show("sscGetDriveFinStatus failure. axnum=" + axnum.ToString() + ", sscGetLastError=0x" + SscApi.sscGetLastError().ToString("X8"));

                bool rtn;
                rtn = (fin_status == SscApi.SSC_FIN_STS_RDY) || (fin_status == SscApi.SSC_FIN_STS_STP);
                m_aFinStatus[axis] = fin_status;
                return rtn;
            }
        }

        public bool motionStart(short axis)
        {
            if (bVir)
            {
                return virIdle[axis] != 0;
            }
            else
            {
                int ans, axnum, fin_status;
                axnum = axis + 1;

                ans = SscApi.sscGetDriveFinStatus(board_id, channel, axnum, SscApi.SSC_FIN_TYPE_INP, out fin_status);

                if (ans != SscApi.SSC_OK)
                    MessageBox.Show("sscGetDriveFinStatus failure. axnum=" + axnum.ToString() + ", sscGetLastError=0x" + SscApi.sscGetLastError().ToString("X8"));

                bool rtn;
                rtn = (fin_status == SscApi.SSC_FIN_STS_MOV) || (fin_status == SscApi.SSC_FIN_STS_STP);
                m_aFinStatus[axis] = fin_status;
                return rtn;
            }
        }

        public int getExternalSignal(short axis, out bool lsp, out bool lsn, out bool dog)
        {
            lsp = false;
            lsn = false;
            dog = false;
            if (bVir) return 0;
            short rtn;
            int ans, axnum;
            axnum = axis + 1;
            ans = SscApi.sscGetIoStatusFast(board_id, channel, axnum, out rtn);
            if (ans != SscApi.SSC_OK)
                MessageBox.Show("sscGetIoStatusFast failure. axnum=" + axnum.ToString() + ", sscGetLastError=0x" + SscApi.sscGetLastError().ToString("X8"));
            lsp = (rtn % 2 == 1);
            lsn = ((rtn/2)%2) == 1;
            dog = ((rtn / 4) % 2) == 1;
            return ans;
        }

        public double getPos(short axis)
        {
            if (bVir)
            {
                virPos[axis] = virPos[axis] + virSpeed[axis];
                if (virSpeed[axis] >= 0 && virPos[axis] >= virTarget[axis])
                    virPos[axis] = virTarget[axis];
                if (virSpeed[axis] < 0 && virPos[axis] <= virTarget[axis])
                    virPos[axis] = virTarget[axis];

                if (virTarget[axis] != virPos[axis])
                    virIdle[axis] = 2;
                else
                    virIdle[axis] = 0;

                return virPos[axis];
            }
            else
            {
                double m_Pos = 0;
                int ans, axnum;
                int pos;
                axnum = axis + 1;
                //ans = SscApi.sscGetCurrentCmdPositionFast(board_id, channel, axnum, out pos);
                //m_aCmdPos[axis] = pos;
                //if (ans != SscApi.SSC_OK)
                //    MessageBox.Show("GetCurrentCmdPosition failure. axnum="+ axnum .ToString()+ ", sscGetLastError=0x"+ SscApi.sscGetLastError().ToString("X8"));

                ans = SscApi.sscGetCurrentFbPositionFast(board_id, channel, axnum, out pos);
                if (ans != SscApi.SSC_OK)
                    MessageBox.Show("GetCurrentFbPosition failure. axnum=" + axnum.ToString() + ", sscGetLastError=0x", SscApi.sscGetLastError().ToString("X8"));
                m_aRealPos[axis] = pos;
                m_Pos = pos;
                return m_Pos;
            }
        }
        public double getCmdPos(short axis)
        {
            if (bVir)
                return virPos[axis];
            double m_Pos = 0;
            int ans, axnum;
            int pos;
            axnum = axis + 1;
            ans = SscApi.sscGetCurrentCmdPositionFast(board_id, channel, axnum, out pos);
            m_aCmdPos[axis] = pos;
            if (ans != SscApi.SSC_OK)
                MessageBox.Show("GetCurrentCmdPosition failure. axnum="+ axnum .ToString()+ ", sscGetLastError=0x"+ SscApi.sscGetLastError().ToString("X8"));

            //ans = SscApi.sscGetCurrentFbPositionFast(board_id, channel, axnum, out pos);
            //if (ans != SscApi.SSC_OK)
            //    MessageBox.Show("GetCurrentFbPosition failure. axnum=" + axnum.ToString() + ", sscGetLastError=0x", SscApi.sscGetLastError().ToString("X8"));
            //m_aRealPos[axis] = pos;
            m_Pos = pos;
            return m_Pos;
        }
        public void setPos(short axis, double pos)
        {
            if (bVir)
            {
                virPos[axis] = pos;
                virTarget[axis] = pos;
            }
            else
            {
                int ans, axnum, pos2;
                sbyte[] status = new sbyte[2];
                short[] spos = new short[2];
                short[] addr = new short[2];
                axnum = axis + 1;
                pos2 = (int)pos;
                spos[0] = (short)(pos2 & 0x0000FFFF);
                spos[1] = (short)((pos2 >> 8) & 0x0000FFFF);
                addr[0] = 0x0246;
                addr[1] = 0x0247;
                //MessageBox.Show("set home pos axis(%d), pos(%d), spos1(%x), spos2(%x)\n", axnum, pos, spos[0], spos[1], status);
                ans = SscApi.sscChange2Parameter(board_id, channel, axnum, addr, spos, status);
                if (ans != SscApi.SSC_OK)
                    MessageBox.Show("set home position failure. axnum=" + axnum.ToString() + ", sscGetLastError=0x", SscApi.sscGetLastError().ToString("X8"));

                ans = SscApi.sscDataSetStart(board_id, channel, axnum);
                if (ans != SscApi.SSC_OK)
                    MessageBox.Show("Data Set failure. axnum=" + axnum.ToString() + ", sscGetLastError=0x", SscApi.sscGetLastError().ToString("X8"));
                getPos(axis);
            }
        }
        public void posStart(short axis, double pos, double strVel, double maxVel, double acc, double dec)
        {
            if (bVir)
            {
                if (!virServo[axis]) return;
                virTarget[axis] = pos;
                if (pos >= virPos[axis])
                    virSpeed[axis] = maxVel;
                else
                    virSpeed[axis] = -1 * maxVel;

                virIdle[axis] = 1;
            }
            else
            {
                double posNow;
                posNow = getPos(axis);
                incStart(axis, pos - posNow, strVel, maxVel, acc, dec);
            }
        }
        #endregion
        public int getAlarm(short axis, int type, out ushort code, out ushort detailCode)
        {
            if (bVir)
            {
                code = 0;
                detailCode = 0;
                return 0;
            }
            int ans, axnum;
            if (type == SscApi.SSC_ALARM_SYSTEM)
                axnum = 0;
            else
                axnum = axis + 1;
            ans = SscApi.sscGetAlarm(board_id, channel, axnum, type, out code, out detailCode);
            if (ans != SscApi.SSC_OK)
                MessageBox.Show("sscGetAlarm failure. axnum=" + axnum.ToString() + ", sscGetLastError=0x", SscApi.sscGetLastError().ToString("X8"));
            return (ans);
        }//getAlarm

        public int resetAlarm(short axis, int type)
        {
            if (bVir) return 0;
            int ans, axnum;
            if (type == SscApi.SSC_ALARM_SYSTEM)
                axnum = 0;
            else
                axnum = axis + 1;
            ans = SscApi.sscResetAlarm(board_id, channel, axnum, type);
            if (ans != SscApi.SSC_OK)
                MessageBox.Show("sscResetAlarm failure. axnum=" + axnum.ToString() + ", sscGetLastError=0x", SscApi.sscGetLastError().ToString("X8"));
            return (ans);
        }

        public int getStatusBit(short axis, int bitnum, out bool rtn)
        {
            if (bVir)
            {
                rtn = false;
                return 0;
            }

            int ans, axnum, irtn;
            
            axnum = axis + 1;
            ans = SscApi.sscGetStatusBitSignalEx(board_id, channel, axnum, bitnum, out irtn);
            if (ans != SscApi.SSC_OK)
                MessageBox.Show("Get Status Bit failure. axnum=" + axnum.ToString() + ", sscGetLastError=0x", SscApi.sscGetLastError().ToString("X8"));
            rtn = irtn == 1;
            return (ans);
        }

        public int setPar1(short axis, short num, short data)
        {
            if (bVir) return 0;

            short axnum = (short)(axis + 1);
            ans = SscApi.sscChangeParameter(board_id, channel, axnum, num, data);
            if (ans != SscApi.SSC_OK)
            {
                MessageBox.Show("sscChangeParameter failure, axis("+ axnum.ToString()+ "),num("+ num.ToString("X4")+ "),data("+data.ToString("X4")+"),sscGetLastError=0x" + SscApi.sscGetLastError().ToString("X8"));
                return (SscApi.SSC_NG);
            }
            return ans;
        }//setPar1

        public int setPar2(int axis, short num, ref short[] data)
        {
            if (bVir) return 0;

            short axnum = (short)(axis + 1);
            short[] anum = new short[2];
            sbyte[] artn = new sbyte[2];
            anum[0] = num;
            anum[1] = (short)(num + 1);
            ans = SscApi.sscChange2Parameter(board_id, channel, axnum, anum, data, artn);
            /* change parameter */
            if (ans != SscApi.SSC_OK)
            {
                MessageBox.Show("sscChange2Parameter failure, axis(" + axnum.ToString() + "),num(" + num.ToString("X4") + "),data(" + data[0].ToString("X4")+data[1].ToString("X4") + "),sscGetLastError=0x" + SscApi.sscGetLastError().ToString("X8"));
                return (SscApi.SSC_NG);
            }
            return ans;
        }//setPar2

        public int getPar1(int axis, short num, out short data)
        {
            if (bVir)
            {
                data = 0;
                return 0;
            }

            short axnum = (short)(axis + 1);
            ans = SscApi.sscCheckParameter(board_id, channel, axnum, num, out data);
            /* change parameter */
            if (ans != SscApi.SSC_OK)
            {
                MessageBox.Show("sscCheckParameter failure, axis(" + axnum.ToString() + "),num(" + num.ToString("X4") + "),data(" + data.ToString("X4") + "),sscGetLastError=0x" + SscApi.sscGetLastError().ToString("X8"));
                return (SscApi.SSC_NG);
            }
            return ans;
        }//getPar1

        public int getPar2(int axis, short num, short[] data)
        {
            if (bVir) return 0;

            short axnum = (short)(axis + 1);
            short[] anum = new short[2];
            sbyte[] artn = new sbyte[2];
            anum[0] = num;
            anum[1] = (short)(num + 1);
            ans = SscApi.sscCheck2Parameter(board_id, channel, axnum, anum, data, artn);
            /* change parameter */
            if (ans != SscApi.SSC_OK)
            {
                MessageBox.Show("sscCheck2Parameter failure, axis(" + axnum.ToString() + "),num(" + num.ToString("X4") + "),data(" + data[0].ToString("X4") + data[1].ToString("X4") + "),sscGetLastError=0x" + SscApi.sscGetLastError().ToString("X8"));
                return (SscApi.SSC_NG);
            }
            return ans;
        }//getPar2
        public void updateUI()
        {
            //mainForm.
        }

        public void setHomePos(int[] aHomePos)
        {
            for (int i = 0; i < AXIS_MAX; i++)
                m_aHomePos = aHomePos;
        }
    }
}
