using Advantech.Motion;
using ch.etel.edi.dmd.v40;
using ch.etel.edi.dsa.v40;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modules
{
    public class ETEL : MotionController
    {
        private List<DsaDrive> dsaDriveList = new List<DsaDrive>();
        public List<DsaDrive> dsaDriveList2 = new List<DsaDrive>();
        private List<Axis> axes = new List<Axis>();
        private DsaMaster ultimet;
        public override IReadOnlyList<Axis> Axes => axes;
        protected override void Open()
        {
            ultimet = new DsaMaster();
            ultimet.open($"etb:ULTIMET:*");
            ultimet.resetError(0);
            //從軸卡暫存器取得可使用軸數，轉換成string後顯示數值為16進位，例:0x800000000000000f，取最後位轉換為2進位1111代表可用軸數為4軸。
            long result = ultimet.getRegisterInt64(DmdData.TYP_MONITOR_INT64, 512, 0, Dsa.GET_CURRENT, Dsa.DEF_TIMEOUT);
            string axiscount = Convert.ToString(result, 16);
            string hexAxisCount = axiscount.Substring(axiscount.Length - 4);
            int count = Convert.ToString(Convert.ToInt32(hexAxisCount, 16), 2).Count(p => p == '1');
            for (int i = 0; i < count; i++)
            {
                DsaDrive drv = new DsaDrive();
                drv.open($"etb:ULTIMET:{i}");
                drv.resetErrorEx(0);
                dsaDriveList.Add(drv);
                dsaDriveList2.Add(drv);
                axes.Add(new Axis(this, i));
            }
        }
        protected override void Close()
        {
            foreach (var item in dsaDriveList)
            {
                item.close();
            }
            ultimet.close();
            dsaDriveList.Clear();
        }
        public override void AxisClose(int axisId)
        {
            dsaDriveList[axisId].powerOff();
        }

        public override void AxisOpen(int axisId)
        {
            dsaDriveList[axisId].powerOn();
        }
        public override bool IsAxisOpen(int axisId)
        {
            DsaStatus status = dsaDriveList[axisId].getStatusFromDrive();
            if (status.isPowerOn()) return true;
            else return false;
        }
        public override double GetCommand(int axisId)
        {
            double command = Math.Round(dsaDriveList[axisId].getPositionDemandValue(),6);
            return command;
        }

        public override double GetFeedback(int axisId)
        {
            double feedback = Math.Round(dsaDriveList[axisId].getPositionActualValue(),6);
            return feedback;
        }
        public override void SetCommand(int axisId, double command)
        {
            throw new NotImplementedException();
        }

        public override void SetFeedback(int axisId, double feedback)
        {
            throw new NotImplementedException();
        }
        public override Sensors GetSensorStatus(int axisId)
        {
            return Sensors.RDY;
        }

        public override List<DsaDrive> GetDsaDrives()
        {
            return dsaDriveList2;
        }

        public override AxisStatus GetStatus(int axisId)
        {
            DsaStatus status = dsaDriveList[axisId].getStatusFromDrive();
            if (status.isInWindow()) return AxisStatus.Stopped;
            else if (status.isFatal()) return AxisStatus.ErrorStopped;
            else return AxisStatus.Moving;
        }

        public override void HomeCommand(int axisId, HomeModes mode, MotionDirections direction, VelocityParams vel)
        {
            dsaDriveList[axisId].homingStart(-1);
        }

        public override void MoveCommand(int axisId, MotionDirections direction, VelocityParams vel)
        {
            int position = direction == MotionDirections.Backward ? -3 : 3;
            MoveToCommand(axisId, position, vel);
        }

        public override void MoveCommand(int axisId, double distance, VelocityParams vel)
        {
            dsaDriveList[axisId].startRelativeProfiledMovement(distance, vel.FinalVel, vel.AccelerationTime);
        }

        public override void MoveToCommand(int axisId, double position, VelocityParams vel)
        {
            dsaDriveList[axisId].startProfiledMovement(position, vel.FinalVel, vel.AccelerationTime);
        }

        public override List<DsaDrive> listDsaDrive()
        {
            return dsaDriveList2;
        }


        public override void LinearMove(int[] axes, double[] distances, VelocityParams vel)
        {

        }
        public override void LinearMoveTo(int[] axes, double[] positions, VelocityParams vel)
        {
            List<DsaDrive> drives = new List<DsaDrive>();
            foreach (var item in axes)
            {
                drives.Add(dsaDriveList[item]);
            }
            DsaIpolGroup igrp = new DsaIpolGroup(drives.ToArray());
            //igrp.setMaster(ultimet);
            igrp.ipolBegin();
            igrp.ipolSetAbsMode(true);

            igrp.ipolTanVelocity(vel.FinalVel);
            igrp.ipolTanAcceleration(vel.AccelerationTime);
            igrp.ipolTanDeceleration(vel.DecelerationTime);
            igrp.ipolLine(positions[0], positions[1]);
            igrp.waitWindow();
            igrp.ipolEnd();
        }
        public override void StopCommand(int axisId)
        {
            dsaDriveList[axisId].quickStop(Dsa.QS_PROGRAMMED_DEC, Dsa.DSA_QS_BYPASS_AND_NO_STOP_SEQUENCE);
        }
        public override void Wait(int axisId)
        {
            dsaDriveList[axisId].waitWindow(-1);
        }

        public override void ResetError(int axisId)
        {
            dsaDriveList[axisId].resetError(0);
        }
        #region I/O
        public override bool ReadDigitalInput(int groupId, int channel)
        {
            throw new NotImplementedException();
        }

        public override bool ReadDigitalOutput(int groupId, int channel)
        {
            throw new NotImplementedException();
        }
        public override void WriteDigitalOutput(int groupId, int channel, bool state)
        {
            throw new NotImplementedException();
        }
        public override void CMPCommand(int axisId, double position, double distance)
        {
            throw new NotImplementedException();
        }
        #endregion

        public void Trigger_Parameters(int axisId)
        {
            dsaDriveList[axisId].setTriggerPositionType(Dsa.TRIGGER_TYPE_REAL_POSITION, Dsa.DEF_TIMEOUT);          // set K336 = Real position
            dsaDriveList[axisId].setTriggerFdoutMask(0x01, Dsa.DEF_TIMEOUT);                                       // set C359 = 0x1 = FDOUT1
            dsaDriveList[axisId].setTriggerPulseGeneratorFdoutMask(Dsa.TRIGGER_PG_1, 0x01, Dsa.DEF_TIMEOUT);       // set K340 PG1 = 0x1 = FDOUT1
            dsaDriveList[axisId].setTriggerPulseGeneratorDelay(Dsa.TRIGGER_PG_1, 0.0, Dsa.DEF_TIMEOUT);            // set K342 = Pulse Generator Delay 0 sec
            dsaDriveList[axisId].setTriggerPulseGeneratorPulseWidth(Dsa.TRIGGER_PG_1, 0.000001, Dsa.DEF_TIMEOUT);  // set K343 = Pulse width 1us
            dsaDriveList[axisId].setTriggerPulseGeneratorInterval(Dsa.TRIGGER_PG_1, 0.000001, Dsa.DEF_TIMEOUT);  // set K344 = Pulse interval 1us
            dsaDriveList[axisId].setTriggerPulseGeneratorNumber(Dsa.TRIGGER_PG_1, 1, Dsa.DEF_TIMEOUT);             // set K345 = Pulse count
            dsaDriveList[axisId].getRegister(DmdData.TYP_MONITOR, 346, 0, Dsa.GET_CURRENT, Dsa.DEF_TIMEOUT);
        }
        public void Trigger_ELtable(int axisId)
        {
            dsaDriveList[axisId].setTriggerIncrementalModeEvent(0, 0.001, Dsa.TRIGGER_POSITIVE, Dsa.TRIGGER_ACTION_START_PG1, 0.000001, Dsa.DEF_TIMEOUT);
            // EL table start from 0.001m, the delta position is 0.000001m = 1um
            dsaDriveList[axisId].setTriggerIncrementalModeEvent(1, 0.003000, Dsa.TRIGGER_POSITIVE, Dsa.TRIGGER_ACTION_STOP_PG1, 0.0, Dsa.DEF_TIMEOUT);
            // EL table stop at 0.003000m,
        }
        public void Reset_All_Trigger_Parameters(int axisId)
        {
            dsaDriveList[axisId].setTriggerFdoutMask(0, Dsa.DEF_TIMEOUT);                                     // Reset C359 
            dsaDriveList[axisId].setTriggerPulseGeneratorFdoutMask(Dsa.TRIGGER_PG_1, 0, Dsa.DEF_TIMEOUT);     // Reset K340 
            dsaDriveList[axisId].setTriggerPulseGeneratorPulseWidth(Dsa.TRIGGER_PG_1, 0, Dsa.DEF_TIMEOUT);    // Reset K343
            dsaDriveList[axisId].setTriggerPulseGeneratorInterval(Dsa.TRIGGER_PG_1, 0, Dsa.DEF_TIMEOUT);      // Reset K344
            dsaDriveList[axisId].setTriggerPulseGeneratorNumber(Dsa.TRIGGER_PG_1, 0, Dsa.DEF_TIMEOUT);        // Reset K345

        }

        public void EnableTrigger(int axisId)
        {
            dsaDriveList[axisId].enableTrigger(Dsa.TRIGGER_NOT_MOVING, 0, 2, false, Dsa.DEF_TIMEOUT);
        }

        public void StartProfiledMovement(int axisId, double pos, double speed, double acc, double jerkTime, int TimeOut)
        {
            dsaDriveList[axisId].startProfiledMovement(pos, speed, acc, jerkTime, TimeOut); // dsaDriveList[axisId] move to 6mm
        }

        public void DisableTrigger(int axisId)
        {
            dsaDriveList[axisId].disableTrigger(Dsa.TRIGGER_NOT_MOVING, false, 30000);
        }

        public void WaitTime(int axisId)
        {
            dsaDriveList[axisId].waitTime(2.0, 3500);
        }




    }
}
