using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Modules;

namespace TrimGap
{
    public class Motion_ETEL
    {
        private MotionController etel;
        private static bool binitial;
        private const double deg1 = 1000;
        /*
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

            [Description("X軸馬達")]
            X = 1,

            [Description("Y軸馬達")]
            Y = 2,

            index,
        }*/

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

        public bool servoReady(short axisNo)
        {
            return etel.Axes[(int)axisNo].IsOpen;
        }

        public double getCmdPos(short axisNo)
        {
            return etel.Axes[(int)axisNo].Command;
        }

        public double getPos(short axisNo)
        {
            return etel.Axes[(int)axisNo].Feedback;
        }

        public void getAlarm(short axisNo, out ushort code, out ushort detailCode)
        {
            
            code = etel.Axes[(int)axisNo].Status == AxisStatus.ErrorStopped ? (ushort)1: (ushort)0;
            detailCode = 0;
        }

        public void getAlarm2(short axisNo, out ushort code, out ushort detailCode)
        {
            code = 0;
            detailCode = 0;
        }

        public bool getAlarm(short axisNo)
        {
            return etel.Axes[(int)axisNo].Status == AxisStatus.ErrorStopped ? true : false;
        }

        public bool motionDone(short axisNo)
        {
            if (bInitial)
            {
                return etel.Axes[(int)axisNo].Status == AxisStatus.Stopped ? true : false;
            }
            else
            {
                return true;
            }
        }

        #endregion Get/Set

        public bool InitMotion(string ParamPath, bool VirMode)
        {
            try
            {
                etel = new ETEL();
                etel.Initialize();
                binitial = true;
            }
            catch (Exception e)
            {
                binitial = false;
            }
            return binitial;
        }

        public void close()
        {
            etel.Dispose();
        }

        public void setServo(short axisNo, bool bOn)
        {
            if (bOn)
                etel.Axes[(int)axisNo].Open();
            else
                etel.Axes[(int)axisNo].Close();
        }

        public void setHome(short axisNo)
        {
        }

        public void findHome(short axisNo)
        {
            if (getAlarm(axisNo)) return;
            etel.Axes[(int)axisNo].HomeVelParams = new VelocityParams(10);
            etel.Axes[(int)axisNo].HomeAsync();
        }

        public void stop(short axisNo)
        {
            etel.Axes[(int)axisNo].Stop();
        }

        public void pitch(short axisNo, double dist, double vel, double max, double acc, double dec)
        {
            if (getAlarm(axisNo)) return;
            int axis = (int)axisNo;
            VelocityParams par = new VelocityParams(vel, max, acc, dec); 
            etel.Axes[(int)axisNo].MoveAsync(dist, par);
        }

        public void pos(short axisNo, double dist, double vel, double max, double acc, double dec)
        {
            if (getAlarm(axisNo)) return;
            int axis = (int)axisNo;
            VelocityParams par = new VelocityParams(vel, max, acc, dec);
            etel.Axes[(int)axisNo].MoveToAsync(dist, par);
        }

        public void jogStart(short axisNo, double dir, double vel, double acc)
        {
            if (getAlarm(axisNo)) return;
            int axis = (int)axisNo;
            MotionDirections direction = dir >0 ? MotionDirections.Forward : MotionDirections.Backward;
            VelocityParams par = new VelocityParams(vel, acc);
            etel.Axes[(int)axisNo].MoveAsync(direction, par);
        }

        public void jogStop(short axisNo)
        {
            etel.Axes[(int)axisNo].Stop();
        }

        public void resetAlarm(short axisNo)
        {
            etel.ResetError((int)axisNo);
        }
    }
}