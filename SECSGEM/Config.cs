using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace SECSGEM
{
    public partial class Config : Form
    {
        string filePath = Directory.GetCurrentDirectory() + @"\EqpInitData\ConfigParameter.csv";
        public class ConfigParameter
        {
            public string ComboMachineType { get; set; }
            public string ComboHSMSRole { get; set; }
            public string T3 { get; set; }
            public string T5 { get; set; }
            public string T6 { get; set; }
            public string T7 { get; set; }
            public string T8 { get; set; }
            public string LocalIP { get; set; }
            public string LocalPort { get; set; }
            public string RemoteIP { get; set; }
            public string RemotePort { get; set; }
            public string DeviceID { get; set; }
            public string LinkTestPeriod { get; set; }
        }

        public Config()
        {
            InitializeComponent();
            LoadParameter();
            Config_Setting();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Config_Load(object sender, EventArgs e)
        {
            LoadParameter();
            Config_Setting();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            SaveParameter();
            Config_Setting();
            Close();
        }

        public void Config_Setting()
        {
            SecsgemForm.MachineType = ComboMachineType.Text;

            if (ComboHSMSRole.Text == ComboHSMSRole.Items[0].ToString()) 
                SECSGEMDefine.HSMS_SS_Params.HSMS_SS_Conect_Mode = SECSGEMDefine.HSMS_SS_Comm_Mode.HSMS_PASSIVE_MODE;
            else 
                SECSGEMDefine.HSMS_SS_Params.HSMS_SS_Conect_Mode = SECSGEMDefine.HSMS_SS_Comm_Mode.HSMS_ACTIVE_MODE;

            SECSGEMDefine.SECS_Common_Params.T3 = int.Parse(edtHSMST3.Text);
            SECSGEMDefine.HSMS_SS_Params.T5 = int.Parse(edtT5.Text);
            SECSGEMDefine.HSMS_SS_Params.T6 = int.Parse(edtT6.Text);
            SECSGEMDefine.HSMS_SS_Params.T7 = int.Parse(edtT7.Text);
            SECSGEMDefine.HSMS_SS_Params.T8 = int.Parse(edtT8.Text);
            SECSGEMDefine.HSMS_SS_Params.LocalIP = edtLocalIP.Text;
            SECSGEMDefine.HSMS_SS_Params.LocalPort = int.Parse(edtLocalPort.Text);
            SECSGEMDefine.HSMS_SS_Params.RemoteIP = edtRemoteIP.Text;
            SECSGEMDefine.HSMS_SS_Params.RemotePort = int.Parse(edtRemotePort.Text);
            SECSGEMDefine.SECS_Common_Params.DeviceID = int.Parse(edtHSMSDeviceID.Text);
            SECSGEMDefine.HSMS_SS_Params.LinkTestPeriod = int.Parse(edtLinktestPeriod.Text);

            SECSGEMDefine.SECS_Common_Params.SECS_Connect_Mode = SECSGEMDefine.SECS_Comm_Mode.HSMS_MODE;
        }

        public void SaveParameter()
        {
            WriteToCSV();
        }

        public void LoadParameter()
        {
            ReadFromCSV();
        }

        public void WriteToCSV()
        {
            var records = new List<ConfigParameter>
            {
                new ConfigParameter 
                { 
                    ComboMachineType = ComboMachineType.Text,
                    ComboHSMSRole = ComboHSMSRole.Text, 
                    T3 = edtHSMST3.Text, 
                    T5 = edtT5.Text, 
                    T6 = edtT6.Text, 
                    T7 = edtT7.Text, 
                    T8 = edtT8.Text, 
                    LocalIP = edtLocalIP.Text, 
                    LocalPort = edtLocalPort.Text, 
                    RemoteIP = edtRemoteIP.Text, 
                    RemotePort = edtRemotePort.Text, 
                    DeviceID = edtHSMSDeviceID.Text, 
                    LinkTestPeriod = edtLinktestPeriod.Text
                }
            };

            using (var writer = new StreamWriter(filePath)) using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) csv.WriteRecords(records);
        }

        public void ReadFromCSV()
        {
            using (var reader = new StreamReader(filePath))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        ComboMachineType.Text = csv.GetField<string>("ComboMachineType");
                        ComboHSMSRole.Text = csv.GetField<string>("ComboHSMSRole");
                        edtHSMST3.Text = csv.GetField<string>("T3");
                        edtT5.Text = csv.GetField<string>("T5");
                        edtT6.Text = csv.GetField<string>("T6");
                        edtT7.Text = csv.GetField<string>("T7");
                        edtT8.Text = csv.GetField<string>("T8");
                        edtLocalIP.Text = csv.GetField<string>("LocalIP");
                        edtLocalPort.Text = csv.GetField<string>("LocalPort");
                        edtRemoteIP.Text = csv.GetField<string>("RemoteIP");
                        edtRemotePort.Text = csv.GetField<string>("RemotePort");
                        edtHSMSDeviceID.Text = csv.GetField<string>("DeviceID");
                        edtLinktestPeriod.Text = csv.GetField<string>("LinkTestPeriod");
                    }
                }
            }
        }
    }
}