using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TrimGap
{
    public class Mo
    {
        public static Motion.Motion_PB motion;
        public static Motion_ETEL motion_ETEL;
        private static bool binitial;
        private const double deg1 = 1000;
        private static int MotionType = 0; //0:SSCNET  1:ETEL
        public static int Axisnum = 0;


        public Mo(int type)
        {
            if (type == 1)
            {
                MotionType = 1;
                Axisnum = 3;
            }
            else
            {
                MotionType = 0;
                Axisnum = 2;
            }
        }

        public static string Get_AxisNo_Description(Enum value)
        {
            System.Reflection.FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public enum AxisNo : short
        {
            [Description("DD馬達")]
            DD = 0,

            [Description("Z軸馬達")]
            Z = 1,

            [Description("X軸馬達")]
            X = 1,

            [Description("Y軸馬達")]
            Y = 2,
            [Description("AP6 2軸")]
            indexAP6 = 2,
            [Description("N2 3軸")]
            indexN2 = 3,
        }

        public enum Dir
        {
            Postive = 1,
            Negative = -1,
        }

        #region Get/Set

        public bool bInitial
        {
            get { return binitial; }
        }

        public bool ServoReady(AxisNo axisNo)
        {
            if (MotionType == 1)
                return motion_ETEL.servoReady((short)axisNo);
            else
                return motion.servoReady((short)axisNo);
        }

        public double Get_CmdPos(AxisNo axisNo)
        {
            if (MotionType == 1)
            {
                if(axisNo == 0)
                    return motion_ETEL.getCmdPos((short)axisNo)*360000;
                else
                    return motion_ETEL.getCmdPos((short)axisNo)*1000000;
            }
            else
                return motion.getCmdPos((short)axisNo);
        }

        public double Get_FBPos(AxisNo axisNo)
        {
            if (MotionType == 1)
            {
                if (axisNo == 0)
                    return motion_ETEL.getPos((short)axisNo) * 360000;
                else
                    return motion_ETEL.getPos((short)axisNo) * 1000000;
            }
            else
                return motion.getPos((short)axisNo);
        }

        public double Get_FBAngle(AxisNo axisNo)
        {
            if (MotionType == 1)
            {
                return motion_ETEL.getPos((short)axisNo) * 360;
            }
            else
                return motion.getPos((short)axisNo) / deg1;
        }

        public void Get_Alarm1(AxisNo axisNo, out ushort code, out ushort detailCode)
        {
            if (MotionType == 1)
                motion_ETEL.getAlarm((short)axisNo, out code, out detailCode);
            else
                motion.getAlarm((short)axisNo, 1, out code, out detailCode);
        }

        public void Get_Alarm2(AxisNo axisNo, out ushort code, out ushort detailCode)
        {
            if (MotionType == 1)
                motion_ETEL.getAlarm2((short)axisNo, out code, out detailCode);
            else
                motion.getAlarm((short)axisNo, 2, out code, out detailCode);
        }

        public bool MotionDone(AxisNo axisNo)
        {
            if (bInitial)
            {
                if (MotionType == 1)
                    return motion_ETEL.motionDone((short)axisNo);
                else
                    return motion.motionDone((short)axisNo);
            }
            else
            {
                return true;
            }
        }

        #endregion Get/Set

        public bool InitMotion(string ParamPath, bool VirMode)
        {
            if (MotionType == 1 && !VirMode)
            {
                motion_ETEL = new Motion_ETEL();
                binitial = motion_ETEL.InitMotion(ParamPath, VirMode);
                Console.WriteLine(binitial.ToString());
                if (binitial)
                {
                    //motion.virMove(VirMode); // 虛擬模式
                    //motion.setTestMode(true);
                    for (short i = 0; i < (short)Axisnum; i++)
                    {
                        motion_ETEL.setServo(i, true);
                    }
                }
                
                return binitial;
            }
            else
            {
                motion = new Motion.Motion_PB(); // SSC
                if (VirMode) motion.virMove(true); // 虛擬模式
                motion.prmFilePath = (ParamPath);
                motion.setPrmPath(ParamPath);
                binitial = motion.initial();
                Console.WriteLine(binitial.ToString());
                if (binitial)
                {
                    
                    motion.setTestMode(true);
                    for (short i = 0; i < (short)Axisnum; i++)
                    {
                        motion.setServo(i, true);
                    }
                }
                return binitial;
            }
        }

        public void Close()
        {
            if (MotionType == 1)
                motion_ETEL.close();
            else
                motion.close();
        }

        public void SetServo(AxisNo axisNo, bool bOn)
        {
            if (MotionType == 1)
                motion_ETEL.setServo((short)axisNo, bOn);
            else
                motion.setServo((short)axisNo, bOn);
        }

        public void SetHome(AxisNo axisNo)
        {
            if (MotionType == 1)
                motion_ETEL.setHome((short)axisNo);
            else
                motion.setHome((short)axisNo);
        }

        public void FindHome(AxisNo axisNo)
        {
            if (MotionType == 1)
            {
                short axis = (short)axisNo;
                motion_ETEL.findHome(axis);
            }
            else
                motion.findHome((short)axisNo, 0);
        }

        public void Stop(AxisNo axisNo)
        {
            if (MotionType == 1)
                motion_ETEL.stop((short)axisNo);
            else
                motion.stop((short)axisNo);
        }

        public void Pitch(AxisNo axisNo, Dir dir)
        {
            int axis = (int)axisNo;
            if (MotionType == 1)
            {
                if (axisNo == 0)
                    motion_ETEL.pitch((short)axisNo, (double)dir * fram.m_pitch[axis] / 360,0, fram.m_pitchV[axis], fram.m_Acc[axis], fram.m_Dec[axis]);
                else
                    motion_ETEL.pitch((short)axisNo, (double)dir * fram.m_pitch[axis] / 1000000,0, fram.m_pitchV[axis], 0.1*fram.m_Acc[axis], 0.1 * fram.m_Dec[axis]);
            }
            
            else
                motion.incStart((short)axisNo, (double)dir * fram.m_pitch[axis], fram.m_pitchV[axis], fram.m_pitchV[axis], fram.m_Acc[axis], fram.m_Dec[axis]);
        }
        public void Pitch(AxisNo axisNo, double dist)
        {
            int axis = (int)axisNo;
            if (MotionType == 1)
            {
                if (axisNo == 0)
                    motion_ETEL.pitch((short)axisNo, dist / 360, 0, fram.m_pitchV[axis], fram.m_Acc[axis], fram.m_Dec[axis]);
                else
                    motion_ETEL.pitch((short)axisNo, dist / 1000000, 0, fram.m_pitchV[axis], 0.1 * fram.m_Acc[axis], 0.1 * fram.m_Dec[axis]);
            }

            else
                motion.incStart((short)axisNo, dist, fram.m_pitchV[axis], fram.m_pitchV[axis], fram.m_Acc[axis], fram.m_Dec[axis]);
        }

        public void PosMove(AxisNo axisNo, double pos)
        {
            int axis = (int)axisNo;
            if (MotionType == 1)
            {
                if (axisNo == 0)
                    motion_ETEL.pos((short)axisNo, pos / 360, 0, fram.m_pitchV[axis], fram.m_Acc[axis], fram.m_Dec[axis]);
                else
                    motion_ETEL.pos((short)axisNo, pos / 1000000, 0, fram.m_pitchV[axis], 0.1 * fram.m_Acc[axis], 0.1 * fram.m_Dec[axis]);
            }

            else
            {
                if (axisNo == 0)
                    motion.posStart((short)axisNo, pos * deg1, fram.m_pitchV[axis], fram.m_pitchV[axis], fram.m_Acc[axis], fram.m_Dec[axis]);
                else
                    motion.posStart((short)axisNo, pos, fram.m_pitchV[axis], fram.m_pitchV[axis], fram.m_Acc[axis], fram.m_Dec[axis]);
            }
        }

        /// <summary>
        /// DD 旋轉固定角度用
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="dir"></param>
        /// <param name="angle"></param>
        public void PitchAngle(AxisNo axisNo, Dir dir, double angle)
        {
            int axis = (int)axisNo;
            double degree = 1.0 / 360;
            double distance = angle * degree * (int)dir;
            if (MotionType == 1)
                motion_ETEL.pitch((short)axisNo, distance, 0, fram.m_pitchV[axis], fram.m_Acc[axis], fram.m_Dec[axis]);
            else
                motion.incStart((short)axisNo, (double)dir * deg1 * angle, fram.m_pitchV[axis], fram.m_pitchV[axis], fram.m_Acc[axis], fram.m_Dec[axis]);
        }

        public void JogStart(AxisNo axisNo, Dir dir)
        {
            int axis = (int)axisNo;
            if (MotionType == 1)
                motion_ETEL.jogStart((short)axisNo, (double)dir, fram.m_jogV[axis], fram.m_Acc[axis]);
            else
                motion.jogStart((short)axisNo, (double)dir * fram.m_jogV[axis], (double)dir * fram.m_jogV[axis], fram.m_Acc[axis]);
        }

        public void JogStop(AxisNo axisNo)
        {
            if (MotionType == 1)
                motion_ETEL.stop((short)axisNo);
            else
                motion.jogStop((short)axisNo);
        }

        public void ResetAlarm(AxisNo axisNo)
        {
            if (MotionType == 1)
            {
                motion_ETEL.resetAlarm((short)axisNo);
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    motion.resetAlarm((short)axisNo, i);
                }
            }
        }
    }
}