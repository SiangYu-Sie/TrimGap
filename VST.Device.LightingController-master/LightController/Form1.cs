using LightController;
using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace LightController
{
    public partial class Form1 : Form
    {
        // 建立控制器物件，給予控制器名稱以及通道數
        EthernetLightingController VLP_controller = new EthernetLightingController("VLP-2460-4eN", 2);
        Rs232LightingController GLC_controller = new Rs232LightingController("GLC-DPI2-170", 1);
        public Form1()
        {
            InitializeComponent();
            // 初始化所有通道的狀態
            // 亮度:0, Strobe Mode:F1, ON/OFF:OFF
            VLP_controller.Initialize();
            GLC_controller.Initialize();
            cb_COMNum.Items.Clear();
            ControlBox = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form Load時, 如有現存的COM, 執行下面程式
            if (COMPorts.Length >= 1)
            {
                //改變SerialPort的PortName為現存的COM, 因為Default之設定為COM1.
                VFDPort.PortName = COMPorts[0];

                //Combo.Text先Show一個現存的COM.
                cb_COMNum.Text = COMPorts[0];

                //將方法帶入, 所有的現存COM加入Combo,Text
                cb_COMNum_Add();
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Hide();
        }

        #region GLC-DPI2-170

        SerialPort VFDPort = new SerialPort("COM1", 19200, Parity.None, 8, StopBits.One);

        //取得存在的COM. 如果存在二個COM則是在COMPorts[0]和COMPorts[1], 以此類推
        String[] COMPorts = SerialPort.GetPortNames();

        //寫一個將存在的COM寫入Combo.Text的方法.
        // CB_COM是我命名該Combo之名稱

        private void cb_COMNum_Add()
        {
            //先將原來在Combo裡頭的內容清掉, 避免重覆寫入
            cb_COMNum.Items.Clear();

            //將找到之現有COM加入Combo,Text中.
            foreach (string port in COMPorts) { cb_COMNum.Items.Add(port); }
        }

        //最後只要將使用者所選擇的COM帶入PortName即可.
        private void cb_COMNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            VFDPort.PortName = cb_COMNum.Text;
        }

        private void btn_GLC_Open_Click(object sender, EventArgs e)
        {
            GLC_controller.Open(cb_COMNum.Text);
            if (GLC_controller.isOK && GLC_controller.isCOM)
            {
                btn_GLC_Zero.Enabled = true;
                btn_GLC_Confirm.Enabled = true;
                num_GLC.Enabled = true;
                cb_COMNum.Enabled = false;
            }
            else if (!GLC_controller.isOK)
            {
                MessageBox.Show("GLC連線失敗");
            }
            else if (!GLC_controller.isCOM)
            {
                MessageBox.Show("GLC COM選擇錯誤");
            }
        }

        private void btn_GLC_Close_Click(object sender, EventArgs e)
        {
            GLC_controller.SetIntensity(1, 0);
            GLC_controller.Close();
            btn_GLC_Zero.Enabled = false;
            btn_GLC_Confirm.Enabled = false;
            num_GLC.Enabled = false;
            num_GLC.Value = 0;
            cb_COMNum.Enabled = true;
        }
        private void btn_GLC_Confirm_Click(object sender, EventArgs e)
        {
            GLC_controller.SetIntensity(1, Convert.ToInt32(num_GLC.Value));
        }

        private void btn_GLC_Zero_Click(object sender, EventArgs e)
        {
            GLC_controller.SetIntensity(1, 0);
            num_GLC.Value = 0;
        }

        #endregion

        #region VLP-2460-4eN
        private void btn_VLP_Open_Click(object sender, EventArgs e)
        {
            // 打開指定的TcpClient通道
            VLP_controller.Open("192.168.11.20");

            if (VLP_controller.isOK)
            {
                VLP_controller.Initialize();
                // ON/OFF，將通道1、2開啟
                VLP_controller.SetOnOff(0, true);
                VLP_controller.SetOnOff(1, true);

                //channel 1
                btn_VLP_ch1_Zero.Enabled = true;
                btn_VLP_ch1_Confirm.Enabled = true;
                num_channel1.Enabled = true;

                //channel 2
                btn_VLP_ch2_Zero.Enabled = true;
                btn_VLP_ch2_Confirm.Enabled = true;
                num_channel2.Enabled = true;
            }
            else
            {
                MessageBox.Show("VLP連線失敗");
            }
        }
        private void btn_VLP_Close_Click(object sender, EventArgs e)
        {
            VLP_controller.Initialize();
            VLP_controller.Close();
            VLP_controller.SetOnOff(0, false);
            VLP_controller.SetOnOff(1, false);
            //channel 1
            btn_VLP_ch1_Zero.Enabled = false;
            btn_VLP_ch1_Confirm.Enabled = false;
            num_channel1.Enabled = false;
            num_channel1.Value = 0;

            //channel2
            btn_VLP_ch2_Zero.Enabled = false;
            btn_VLP_ch2_Confirm.Enabled = false;
            num_channel2.Enabled = false;
            num_channel2.Value = 0;
        }

        private void btn_VLP_ch1_Confirm_Click(object sender, EventArgs e)
        {
            VLP_controller.SetIntensity(0, Convert.ToInt32(num_channel1.Value));
        }

        private void btn_VLP_ch2_Confirm_Click(object sender, EventArgs e)
        {
            VLP_controller.SetIntensity(1, Convert.ToInt32(num_channel2.Value));
        }

        private void btn_VLP_ch1_Zero_Click(object sender, EventArgs e)
        {
            VLP_controller.SetIntensity(0, 0);
            num_channel1.Value = 0;
        }

        private void btn_VLP_ch2_Zero_Click(object sender, EventArgs e)
        {
            VLP_controller.SetIntensity(1, 0);
            num_channel2.Value = 0;
        }

        #endregion
    }
}
