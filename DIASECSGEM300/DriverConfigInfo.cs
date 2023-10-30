using Delta.DIAAuto.DIASECSGEM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DemoFormDiaGemLib
{

    public partial class DriverConfigInfo : Form
    {
        public DriverConfigInfo()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        public void ShowData(SECSDriverSetting driverSetting)
        {
            if (driverSetting == null)
                return;

            //General
            rdoGeneral_ConnectMode_HSMS.Checked = driverSetting.SECS_Connect_Mode == eSECS_Comm_Mode.HSMS_MODE ? true : false;
            rdoGeneral_ConnectMode_SECS1.Checked = driverSetting.SECS_Connect_Mode == eSECS_Comm_Mode.SECSI_MODE ? true : false; ;
            chkGeneral_EnableLog.Checked = driverSetting.EnableLog;
            //HSMS
            txtHSMS_DeviceID.Text = driverSetting.HSMS.DeviceId.ToString();
            cmbHSMS_Mode.SelectedIndex = driverSetting.HSMS.Mode == eHsmsConnectMode.Passive ? 0 : 1;
            txtHSMS_IPAddress.Text = driverSetting.HSMS.PassiveIP;
            txtHSMS_Port.Text = driverSetting.HSMS.PassivePort;
            txtHSMS_T3.Text = driverSetting.HSMS.T3.ToString();
            txtHSMS_T5.Text = driverSetting.HSMS.T5.ToString();
            txtHSMS_T6.Text = driverSetting.HSMS.T6.ToString();
            txtHSMS_T7.Text = driverSetting.HSMS.T7.ToString();
            txtHSMS_T8.Text = driverSetting.HSMS.T8.ToString();
            txtHSMS_LinkTest.Text = driverSetting.HSMS.LinkTest.ToString();
            txtHSMS_MaxMessageLength.Text = driverSetting.HSMS.MaxMessageLength.ToString();
            rdoHSMS_EnableRawDataLog_Y.Checked = driverSetting.HSMS.EnableRawDataLog;
            rdoHSMS_EnableRawDataLog_N.Checked = !driverSetting.HSMS.EnableRawDataLog;
            //SECS1
            txtSECS1_DeviceID.Text = driverSetting.SECSI.DeviceId.ToString();
            txtSECS1_COMPort.Text = driverSetting.SECSI.COMPort.ToString();
            txtSECS1_BaudRate.Text = driverSetting.SECSI.BaudRate.ToString();
            cmbSECS1_Mode.SelectedIndex = driverSetting.SECSI.Mode == eSECSI_Comm_Mode.SECSI_EQUIPMENT_MODE ? 0 : 1;
            txtSECS1_Retry.Text = driverSetting.SECSI.RTY.ToString();
            txtSECS1_T1.Text = driverSetting.SECSI.T1.ToString();
            txtSECS1_T2.Text = driverSetting.SECSI.T2.ToString();
            txtSECS1_T3.Text = driverSetting.SECSI.T3.ToString();
            txtSECS1_T4.Text = driverSetting.SECSI.T4.ToString();
            cmbSECS1_FlowControl.SelectedIndex = driverSetting.SECSI.FlowControl == false ? 0 : 1;
            rdoSECS1_EnableSerialPortLog_Y.Checked = driverSetting.SECSI.EnableSerialPortLog;
            rdoSECS1_EnableSerialPortLog_N.Checked = !driverSetting.SECSI.EnableSerialPortLog;


            Visible = true;
        }
    }

    
}
