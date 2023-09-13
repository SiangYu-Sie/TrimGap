using Advantech.Motion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class PCI_1245 : MotionController
    {
        private DEV_LIST[] curAvailableDevs = new DEV_LIST[Motion.MAX_DEVICES];
        private uint deviceNum = 0;
        private IntPtr deviceHandle = IntPtr.Zero;
        private Axis[] axes;
        private IntPtr[] axisHand;
        public override IReadOnlyList<Axis> Axes => axes;

        protected override void Open()
        {
            uint deviceCount = 0;
            if (Motion.mAcm_GetAvailableDevs(curAvailableDevs, Motion.MAX_DEVICES, ref deviceCount) == (int)ErrorCode.SUCCESS)
            {
                int result;
                string strTemp = "";

                if (deviceCount > 0)
                    deviceNum = curAvailableDevs[0].DeviceNum;
                else
                    throw new InvalidOperationException($"PCI_1245 not found");

                result = (int)Motion.mAcm_DevOpen(deviceNum, ref deviceHandle);
                if (result != (uint)ErrorCode.SUCCESS)
                    throw new Exception("Open Device Failed With Error Code: [0x" + Convert.ToString(result, 16) + "]");


                axes = GenerateAxis().ToArray();

            }
            else
            {
                throw new InvalidOperationException($"PCI_1245 not found");
            }
        }

        public override void AxisClose(int axisId)
        {
            Motion.mAcm_AxSetSvOn(axisHand[axisId], 0);
        }

        public override void AxisOpen(int axisId)
        {
            Motion.mAcm_AxSetSvOn(axisHand[axisId], 1);
        }

        private IEnumerable<Axis> GenerateAxis()
        {
            List<Axis> axesList = new List<Axis>();

            int result;
            uint axesCount = 0;
            result = (int)Motion.mAcm_GetU32Property(deviceHandle, (uint)PropertyID.FT_DevAxesCount, ref axesCount);
            if (result != (uint)ErrorCode.SUCCESS)
                throw new Exception("Get Axis Number Failed With Error Code: [0x" + Convert.ToString(result, 16) + "]");

            axisHand = new IntPtr[axesCount];

            for (uint i = 0; i < axesCount; i++)
            {
                result = (int)Motion.mAcm_AxOpen(deviceHandle, (UInt16)i, ref axisHand[i]);
                if (result != (uint)ErrorCode.SUCCESS)
                    throw new Exception("Open Axis Failed With Error Code: [0x" + Convert.ToString(result, 16) + "]");

                //Motion.mAcm_AxSetCmdPosition(axisHand[i], 0);
                //Motion.mAcm_AxSetActualPosition(axisHand[i], 0);

                AxisOpen((int)i);

                #region set axis param
                // Logic/Mode
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxOrgEnable, (uint)ORGEnable.ORG_ENABLE);        // ORG Enable
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxOrgLogic, (uint)ORGLogic.ORG_ACT_HIGH);        // ORG logic
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxOrgReact, (uint)ORGReact.ORG_IMMED_STOP);      // ORG React 回原點後 0:immediate stop, 1: slow done stop(Default)
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxElEnable, (uint)HLmtEnable.HLMT_EN);           // EL Enable
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxElLogic, (uint)HLmtLogic.HLMT_ACT_HIGH);       // EL logic
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxElReact, (uint)HLmtReact.HLMT_IMMED_STOP);     // EL Mode
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxAlmEnable, (uint)AlarmEnable.ALM_EN);          // ALM Enable
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxAlmLogic, (uint)AlarmLogic.ALM_ACT_HIGH);      // ALM logic
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxAlmReact, (uint)AlarmReact.ALM_IMMED_STOP);    // ALM Mode
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxEzLogic, (uint)EZLogic.EZ_ACT_LOW);           // EZ logic

                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxPulseInLogic, (uint)PulseInLogic.NO_INV_DIR);  // PLS logic

                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxPulseInMode, (uint)PulseInMode.AB_4X);         // PLS IptMode 0:X1, 1:X2, 2:X4, 3:CW/CCW
                                                                                                                                          // 1 OUT / DIR
                                                                                                                                          // 2 OUT / DIR，OUT 负逻辑
                                                                                                                                          // 4 OUT / DIR，DIR 负逻辑
                                                                                                                                          // 8 OUT / DIR，OUT & DIR 负逻辑
                                                                                                                                          // 16 CW / CCW
                                                                                                                                          // 32 CW / CCW，CW & CCW 负逻辑
                                                                                                                                          // 256 CW / CCW，OUT 负逻辑
                                                                                                                                          // 512 CW / CCW，DIR 负逻辑
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxPulseOutMode, (uint)PulseOutMode.O_CW_CCW);     // PLS OptMode
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxInpEnable, (uint)InPositionEnable.INP_EN);     // INP Enable
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxInpLogic, (uint)InPositionLogic.INP_ACT_LOW); // INP logic
                Motion.mAcm_SetU32Property(deviceHandle, (uint)PropertyID.CFG_DevEmgLogic, (uint)EmgLogic.EMG_ACT_HIGH);       // EMG logic

                // Soft Limit

                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxSwPelEnable, (uint)SwLmtEnable.SLMT_EN);       // SPEL Mode 0:Disable, 1:Enable
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxSwMelEnable, (uint)SwLmtEnable.SLMT_EN);       // SMEL Mode 0:Disable, 1:Enable
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxSwPelReact, (uint)SwLmtReact.SLMT_IMMED_STOP); // SPEL 0:immediate stop, 1: slow done stop
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxSwMelReact, (uint)SwLmtReact.SLMT_IMMED_STOP); // SMEL 0:immediate stop, 1: slow done stop
                Motion.mAcm_SetI32Property(axisHand[i], (uint)PropertyID.CFG_AxSwPelValue, 10000000);                         // SPEL Pulse (Positive) -2147483647 ~ 2147483647(Default:10000000)
                Motion.mAcm_SetI32Property(axisHand[i], (uint)PropertyID.CFG_AxSwMelValue, -10000000);                        // SMEL Pulse (Negative) -2147483647 ~ 2147483647(Default:-10000000)

                // Home Parameter
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxHomeResetEnable, (uint)HomeReset.HOME_RESET_EN); // HomeReset 回原點後歸零 0:Disable, 1:Enable
                Motion.mAcm_SetF64Property(axisHand[i], (uint)PropertyID.PAR_AxHomeVelLow, 8000);                             // Home Vel Low (Default:8000)
                Motion.mAcm_SetF64Property(axisHand[i], (uint)PropertyID.PAR_AxHomeVelHigh, 8000);                            // Home Vel High(Default:8000)
                Motion.mAcm_SetF64Property(axisHand[i], (uint)PropertyID.PAR_AxHomeAcc, 10000);                               // Home Acc (Default:10000)
                Motion.mAcm_SetF64Property(axisHand[i], (uint)PropertyID.PAR_AxHomeDec, 10000);                               // Home Dec (Default:10000)
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.PAR_AxHomeExSwitchMode, (uint)HomeExSwitchMode.LevelOn); // HomeExSwitchMode 停止條件 0:Level On(Default), 1:Level Off, 2:Rising Edge, 3:Falling Edge
                                                                                                                                              // 回原點的Mode跟Dir在移動時要一起設定，這裡先設定好 之後直接call就可以了
                                                                                                                                              //Advantech.Home_Mode[i] = (uint)HomeMode.MODE7_AbsSearch;  // Home Mode
               // Advantech.Home_Mode[i] = (uint)HomeMode.MODE8_LmtSearch;  // Home Mode
               // Advantech.Home_Dir[i] = (uint)HomeDir.NegDir;   // Home Dir 0:正, 1:負

                // Set Single Move Parameter
                Motion.mAcm_SetF64Property(axisHand[i], (uint)PropertyID.PAR_AxVelLow, 5000);                                 // Axis VelLow (Default:2000)
                Motion.mAcm_SetF64Property(axisHand[i], (uint)PropertyID.PAR_AxVelHigh, 400000);                                // Axis VelHigh (Default:8000)
                Motion.mAcm_SetF64Property(axisHand[i], (uint)PropertyID.PAR_AxAcc, 10000);                                   // Axis Acc (Default:10000)
                Motion.mAcm_SetF64Property(axisHand[i], (uint)PropertyID.PAR_AxDec, 10000);                                   // Axis Dec (Default:10000)
                Motion.mAcm_SetF64Property(axisHand[i], (uint)PropertyID.PAR_AxHomeCrossDistance, 1000);

                // Set Cmp Parameter
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxCmpEnable, (uint)CmpEnable.CMP_EN);             // CMP Enable
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxCmpSrc, (uint)CmpSource.SRC_ACTUAL_POSITION);   // CMP source 0:cmd Pos, 1: Act Pos
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxCmpMethod, (uint)CmpMethod.MTD_GREATER_POSITION);// CMP Method 0:>, 1:<
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxCmpPulseMode, (uint)CmpPulseMode.CMP_PULSE);    // CMP Mode    0:plus,  1:toggl反向
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxCmpPulseWidth, (uint)CmpPulseWidth.CP_5us);   // CmpPulseWidth  0:5 us, 1:10 us,  2:20 us, 3:50 us, 4:100 us, 5:200 us, 6:500 us, 7:1000 us
                Motion.mAcm_SetU32Property(axisHand[i], (uint)PropertyID.CFG_AxCmpPulseLogic, (uint)CmpPulseLogic.CP_ACT_HIGH);   // CmpPulseLogic

                Motion.mAcm_SetU32Property(axisHand[2], (uint)PropertyID.CFG_AxAlmLogic, (uint)AlarmLogic.ALM_ACT_LOW);      // ALM logi
                Motion.mAcm_SetU32Property(axisHand[2], (uint)PropertyID.CFG_AxInpLogic, (uint)InPositionLogic.INP_ACT_HIGH); // INP logic
                Motion.mAcm_SetU32Property(axisHand[2], (uint)PropertyID.CFG_AxPulseInMode, (uint)PulseInMode.I_CW_CCW);         // PLS IptMode 0:X1, 1:X2, 2:X4, 3:CW/CCW
                #endregion

                axesList.Add(new Axis(this, (int)i));
            }

            return axesList;
        }

        public override double GetCommand(int axisId)
        {
            double commandPos = 0;
            Motion.mAcm_AxGetCmdPosition(axisHand[axisId],ref commandPos);
            return commandPos;
        }

        public override double GetFeedback(int axisId)
        {
            double feedBackPos = 0;
            Motion.mAcm_AxGetActualPosition(axisHand[axisId], ref feedBackPos);
            return feedBackPos;
        }
        public override void SetCommand(int axisId, double command) => Motion.mAcm_AxSetCmdPosition(axisHand[axisId], command);

        public override void SetFeedback(int axisId, double feedback) => Motion.mAcm_AxSetActualPosition(axisHand[axisId], feedback);

        public override Sensors GetSensorStatus(int axisId)
        {
            uint ioStatus = 0;
            uint result = Motion.mAcm_AxGetMotionIO(axisHand[axisId], ref ioStatus);
            if (result == (uint)ErrorCode.SUCCESS)
            {
                if ((ioStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_ALM) > 0)//ALM
                    return Sensors.ALM;
                else if ((ioStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_LMTP) > 0)//PEL
                    return Sensors.PEL;
                else if ((ioStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_LMTN) > 0)//MEL
                    return Sensors.NEL;
                else if ((ioStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_ORG) > 0)//ORG
                    return Sensors.ORG;
                else if ((ioStatus & (uint)Ax_Motion_IO.AX_MOTION_IO_EMG) > 0)//EMG
                    return Sensors.EMG;
                else
                    return Sensors.None;
            }
            else
                throw new Exception($"Can't get Axis {axisId} Status");
        }

        public override AxisStatus GetStatus(int axisId)
        {
            ushort axisState = 0;
            Motion.mAcm_AxGetState(axisHand[axisId], ref axisState);
            if (axisState == (int)AxisState.STA_AX_ERROR_STOP)
                return AxisStatus.ErrorStopped;
            else if (axisState == (int)AxisState.STA_AX_READY)
                return AxisStatus.Stopped;
            else
                return AxisStatus.Moving;
        }

        public override void HomeCommand(int axisId, HomeModes mode, MotionDirections direction, VelocityParams vel)
        {
            Motion.mAcm_AxResetError(axisHand[axisId]);
            uint homeMode = ConverterHomeMode(mode);
            HomeDir homeDir = ConverterHomeDir(direction);
            Motion.mAcm_SetF64Property(axisHand[axisId], (uint)PropertyID.PAR_AxVelHigh, vel.FinalVel);
            Motion.mAcm_AxHome(axisHand[axisId], homeMode, (uint)homeDir);
        }

        public override void MoveCommand(int axisId, MotionDirections direction, VelocityParams vel)
        {
            ushort dir = (ushort)((direction == MotionDirections.Forward) ? 0 : 1);
            SetMotionVelocity(axisId, vel);
            Motion.mAcm_AxMoveVel(axisHand[axisId], dir);
        }

        public override void MoveCommand(int axisId, double distance, VelocityParams vel)
        {
            SetMotionVelocity(axisId, vel);
            Motion.mAcm_AxMoveRel(axisHand[axisId], distance);
        }

        public override void MoveToCommand(int axisId, double position, VelocityParams vel)
        {
            SetMotionVelocity(axisId, vel);
            Motion.mAcm_AxMoveAbs(axisHand[axisId], position);
        }

        public override void StopCommand(int axisId) => Motion.mAcm_AxStopDec(axisHand[axisId]);
        public override void ResetError(int axisId)
        {
            Motion.mAcm_AxResetError(axisHand[axisId]);
        }
        protected override void Close()
        {
            for (int i = 0; i < axisHand.Count(); i++)
            {
                if ((uint)GetStatus(i) == (uint)AxisState.STA_AX_ERROR_STOP)
                {
                    // Reset the axis' state. If the axis is in ErrorStop state, the state will be changed to Ready after calling this function
                    Motion.mAcm_AxResetError(axisHand[i]);
                }
                //To command axis to decelerate to stop.
                Motion.mAcm_AxStopDec(axisHand[i]);

                Motion.mAcm_AxSetSvOn(axisHand[i], 0);
                Motion.mAcm_AxClose(ref axisHand[i]);
            }
            Motion.mAcm_DevClose(ref deviceHandle);
        }

        private uint ConverterHomeMode(HomeModes mode)
        {
            switch (mode)
            {
                case HomeModes.EL: return (uint)HomeMode.MODE8_LmtSearch;
                default: throw new NotSupportedException($"{mode}");
            }
        }

        private HomeDir ConverterHomeDir(MotionDirections dir)
        {
            switch (dir)
            {
                case MotionDirections.Backward:
                    return HomeDir.NegDir;
                case MotionDirections.Forward:
                    return HomeDir.PosiDir;
                default:
                    throw new NotSupportedException($"{dir}");
            }
        }

        private void SetMotionVelocity(int axisId, VelocityParams vel)
        {
            // Set Single Move Parameter
            Motion.mAcm_SetF64Property(axisHand[axisId], (uint)PropertyID.PAR_AxVelLow, vel.InitialVel);                                 
            Motion.mAcm_SetF64Property(axisHand[axisId], (uint)PropertyID.PAR_AxVelHigh, vel.FinalVel);
            Motion.mAcm_SetF64Property(axisHand[axisId], (uint)PropertyID.PAR_AxAcc, vel.AccelerationTime);
            Motion.mAcm_SetF64Property(axisHand[axisId], (uint)PropertyID.PAR_AxDec, vel.DecelerationTime);
        }
       
        #region DI/O
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

        public override void CMPCommand(int axisId,double position,double distance)
        {
            Motion.mAcm_SetU32Property(axisHand[axisId], (uint)PropertyID.CFG_AxCmpEnable, (uint)CmpEnable.CMP_EN);
            Motion.mAcm_SetI32Property(axisHand[axisId], (uint)PropertyID.CFG_AxCmpSrc, (int)CmpSource.SRC_ACTUAL_POSITION);
            Motion.mAcm_SetI32Property(axisHand[axisId], (uint)PropertyID.CFG_AxCmpMethod, 0);

            Motion.mAcm_SetI32Property(axisHand[axisId], (uint)PropertyID.CFG_AxCmpPulseLogic, 0);
            Motion.mAcm_SetU32Property(axisHand[axisId], (uint)PropertyID.CFG_AxCmpPulseWidth, 5);

            Motion.mAcm_SetI32Property(axisHand[axisId], (uint)PropertyID.CFG_AxCmpPulseMode, 0);
            Motion.mAcm_AxSetCmpAuto(axisHand[axisId], position, position+distance, distance);
        }

        public override bool IsAxisOpen(int axisId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
