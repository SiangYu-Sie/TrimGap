using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace TrimGap
{
    public partial class MotionControl : Form
    {
        private TableLayoutPanel _tlp = new TableLayoutPanel();
        private TextBox[] _TextBox;    //
        private ComboBox[] _ComboBox;    //
        private int _TextBoxCount = 0;    //
        private string[] _TextBox_Name, _ComboBox_Name; //
        private Mo.AxisNo _AxisNo;
        private bool[] _ServoSts = new bool[4];
        private ushort[] _code = new ushort[2];
        private ushort[] _detailCode = new ushort[2];

        private string[] _TextBox_Name_str = { "tbCmdpos", "tbCurrentpos", "tbMotorSts",
            "tbPitch", "tbPitchV", "tbJogV",
            "tbPitch_copy" , "tbPitchV_copy", "tbJogV_copy" };

        private string[] _ComboBox_Name_str = { "unit" };
        private static int MotionType = 0;
        private static int MotionAxisMax = 0;
        private static bool[] HomeAdditionMove = new bool[3];
        public MotionControl(int type)
        {
            MotionType = type;
            if (MotionType == 0)
            {
                MotionAxisMax = (int)Mo.AxisNo.indexAP6;
            }
            else if (MotionType == 1)
            {
                MotionAxisMax = (int)Mo.AxisNo.indexN2;
            }
            InitializeComponent();
            Init();
            initparameter_UI();
            if (Common.motion.bInitial)
            {
                timer1.Enabled = true;
            }
            
        }

        private void initparameter_UI()
        {
            // [System]

            for (int i = 0; i < MotionAxisMax; i++)
            {
                for (int j = 0; j < _TextBox_Name_str.Length; j++)
                {
                    if (_TextBox[j + i * _TextBox_Name_str.Length].Name == _TextBox_Name_str[3])
                    {
                        _TextBox[j + i * _TextBox_Name_str.Length].Text = fram.m_pitch[i].ToString();
                    }
                    else if (_TextBox[j + i * _TextBox_Name_str.Length].Name == _TextBox_Name_str[4])
                    {
                        _TextBox[j + i * _TextBox_Name_str.Length].Text = fram.m_pitchV[i].ToString();
                    }
                    else if (_TextBox[j + i * _TextBox_Name_str.Length].Name == _TextBox_Name_str[5])
                    {
                        _TextBox[j + i * _TextBox_Name_str.Length].Text = fram.m_jogV[i].ToString();
                    }
                }
            }
            if (MotionType == 0)
            {
                groupBox3.Visible = false;
                button20.Text = "Jog↑";
                button21.Text = "Jog↓";
                button22.Text = "Pitch↑";
                button12.Text = "Pitch↓";
                this.Size = new Size(705, 670);
            }
            else
            {
                groupBox3.Visible = true;
                groupBox2.Text = "Ax2_X軸Sensor馬達";
                button20.Text = "Jog←";
                button21.Text = "Jog→";
                button22.Text = "Pitch←";
                button12.Text = "Pitch→";
                this.Size = new Size(1380, 670);
            }
        }

        private void Init()
        {
            #region axis 1

            foreach (Control ctl in tableLayoutPanel1.Controls)
            {
                ctl.Tag = "1";
                if (ctl is Button)
                {
                    if (ctl.Text == "JogCW" || ctl.Text == "JogCCW")
                    {
                        ctl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMouseUp);
                        ctl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMouseDown);
                    }
                    else
                    {
                        ctl.Click += new System.EventHandler(this.btnFunction);
                    }
                }
                else if (ctl is TextBox)
                {
                    string str = ((TextBox)ctl).Name;
                    _TextBoxCount++;
                }
                else if (ctl is ComboBox)
                {
                    ((ComboBox)ctl).Items.Add("100");
                    ((ComboBox)ctl).Items.Add("10");
                    ((ComboBox)ctl).Items.Add("1");
                    ((ComboBox)ctl).Items.Add("0.1");
                    int index = int.Parse(ctl.Tag.ToString()) - 1;
                    ((ComboBox)ctl).Text = fram.m_unit[index].ToString();
                }
            }
            _TextBox = new TextBox[_TextBoxCount * Mo.Axisnum];
            _TextBox_Name = new string[_TextBoxCount];
            _ComboBox = new ComboBox[_ComboBox_Name_str.Length * Mo.Axisnum];
            _ComboBox_Name = new string[_ComboBox_Name_str.Length];

            foreach (Control ctl in tableLayoutPanel1.Controls)
            {
                if (ctl is TextBox)
                {
                    int index = ((TextBox)ctl).TabIndex;
                    _TextBox[index - 1] = ((TextBox)ctl);
                    switch (index)
                    {
                        case 1:
                            _TextBox_Name[index - 1] = _TextBox_Name_str[0];//"tbCurrentpos";
                            _TextBox[index - 1].Name = _TextBox_Name_str[0];// "tbCurrentpos";
                            //_TextBox[index - 1].Text = "tbCurrentpos";
                            break;

                        case 2:
                            _TextBox_Name[index - 1] = _TextBox_Name_str[1];//"tbCurrentSpeed";
                            _TextBox[index - 1].Name = _TextBox_Name_str[1];//"tbCurrentSpeed";
                            //_TextBox[index - 1].Text = "tbCurrentSpeed";
                            break;

                        case 3:
                            _TextBox_Name[index - 1] = _TextBox_Name_str[2];//"tbMotorSts";
                            _TextBox[index - 1].Name = _TextBox_Name_str[2];//"tbMotorSts";
                            //_TextBox[index - 1].Text = "tbMotorSts";
                            break;

                        case 4:
                            _TextBox_Name[index - 1] = _TextBox_Name_str[3];//"tbPitch";
                            _TextBox[index - 1].Name = _TextBox_Name_str[3];//"tbPitch";
                            //_TextBox[index - 1].Text = "tbPitch";
                            break;

                        case 5:
                            _TextBox_Name[index - 1] = _TextBox_Name_str[4];//"tbPitchV";
                            _TextBox[index - 1].Name = _TextBox_Name_str[4];//"tbPitchV";
                            //_TextBox[index - 1].Text = "tbPitchV";
                            break;

                        case 6:
                            _TextBox_Name[index - 1] = _TextBox_Name_str[5];//"tbJogV";
                            _TextBox[index - 1].Name = _TextBox_Name_str[5];//"tbJogV";
                            //_TextBox[index - 1].Text = "tbJogV";
                            break;

                        case 7:
                            _TextBox_Name[index - 1] = _TextBox_Name_str[6];//"tbPitch_copy";
                            _TextBox[index - 1].Name = _TextBox_Name_str[6];//"tbPitch_copy";
                            //_TextBox[index - 1].Text = "tbPitch_copy";
                            break;

                        case 8:
                            _TextBox_Name[index - 1] = _TextBox_Name_str[7];//"tbPitchV_copy";
                            _TextBox[index - 1].Name = _TextBox_Name_str[7];//"tbPitchV_copy";
                            //_TextBox[index - 1].Text = "tbPitchV_copy";
                            break;

                        case 9:
                            _TextBox_Name[index - 1] = _TextBox_Name_str[8];//"tbJogV_copy";
                            _TextBox[index - 1].Name = _TextBox_Name_str[8];//"tbJogV_copy";
                            //_TextBox[index - 1].Text = "tbJogV_copy";
                            break;

                        default:
                            break;
                    }
                }
                else if (ctl is ComboBox)
                {
                    for (int i = 0; i < _ComboBox.Length; i++)
                    {
                        if (_ComboBox[i] == null)
                        {
                            _ComboBox[i] = ((ComboBox)ctl);
                            _ComboBox[i].Name = _ComboBox_Name_str[0];
                            break;
                        }
                    }
                }
            }

            #endregion axis 1
            if (MotionAxisMax < 2)
            {
                return;
            }
            #region axis 2

            foreach (Control ctl in tableLayoutPanel2.Controls)
            {
                ctl.Tag = "2";
                if (ctl is Button)
                {
                    if (MotionType == 0)
                    {
                        if (ctl.Text == "Jog↑" || ctl.Text == "Jog↓")
                        {
                            ctl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMouseUp);
                            ctl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMouseDown);
                        }
                        else
                        {
                            ctl.Click += new System.EventHandler(this.btnFunction);
                        }
                    }
                    else
                    {
                        if (ctl.Text == "Jog←" || ctl.Text == "Jog→")
                        {
                            ctl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMouseUp);
                            ctl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMouseDown);
                        }
                        else
                        {
                            ctl.Click += new System.EventHandler(this.btnFunction);
                        }
                    }

                }
                else if (ctl is TextBox)
                {
                    int index = ((TextBox)ctl).TabIndex;
                    _TextBox[index - 1 + _TextBoxCount * (Convert.ToInt32(ctl.Tag) - 1)] = ((TextBox)ctl);
                    _TextBox[index - 1 + _TextBoxCount * (Convert.ToInt32(ctl.Tag) - 1)].Name = _TextBox_Name[index - 1];
                    //_TextBox[index - 1 + _TextBoxCount * 1].Text = _TextBox_Name[index - 1];
                }
                else if (ctl is ComboBox)
                {
                    ((ComboBox)ctl).Items.Add("10000");
                    ((ComboBox)ctl).Items.Add("1000");
                    ((ComboBox)ctl).Items.Add("100");
                    ((ComboBox)ctl).Items.Add("10");
                    ((ComboBox)ctl).Items.Add("1");
                    int index = int.Parse(ctl.Tag.ToString()) - 1;
                    ((ComboBox)ctl).Text = fram.m_unit[index].ToString();
                    for (int i = 0; i < _ComboBox.Length; i++)
                    {
                        if (_ComboBox[i] == null)
                        {
                            _ComboBox[i] = ((ComboBox)ctl);
                            _ComboBox[i].Name = _ComboBox_Name_str[0];
                            break;
                        }
                    }
                }
            }

            #endregion axis 2
            if (MotionAxisMax < 3)
            {
                return;            
            }
            #region axis 3

            foreach (Control ctl in tableLayoutPanel3.Controls)
            {
                ctl.Tag = "3";
                if (ctl is Button)
                {
                    if (ctl.Text == "Jog↑" || ctl.Text == "Jog↓")
                    {
                        ctl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMouseUp);
                        ctl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMouseDown);
                    }
                    else
                    {
                        ctl.Click += new System.EventHandler(this.btnFunction);
                    }
                }
                else if (ctl is TextBox)
                {
                    int index = ((TextBox)ctl).TabIndex;
                    _TextBox[index - 1 + _TextBoxCount * (Convert.ToInt32(ctl.Tag) - 1)] = ((TextBox)ctl);
                    _TextBox[index - 1 + _TextBoxCount * (Convert.ToInt32(ctl.Tag) - 1)].Name = _TextBox_Name[index - 1];
                    //_TextBox[index - 1 + _TextBoxCount * 1].Text = _TextBox_Name[index - 1];
                }
                else if (ctl is ComboBox)
                {
                    ((ComboBox)ctl).Items.Add("10000");
                    ((ComboBox)ctl).Items.Add("1000");
                    ((ComboBox)ctl).Items.Add("100");
                    ((ComboBox)ctl).Items.Add("10");
                    ((ComboBox)ctl).Items.Add("1");
                    int index = int.Parse(ctl.Tag.ToString()) - 1;
                    ((ComboBox)ctl).Text = fram.m_unit[index].ToString();
                    for (int i = 0; i < _ComboBox.Length; i++)
                    {
                        if (_ComboBox[i] == null)
                        {
                            _ComboBox[i] = ((ComboBox)ctl);
                            _ComboBox[i].Name = _ComboBox_Name_str[0];
                            break;
                        }
                    }
                }
            }

            #endregion axis 3
        }

        private void SavePara(Mo.AxisNo axisNo, string ParamName, double newValue)
        {
            string AxisNoStr = Mo.Get_AxisNo_Description(axisNo);
            string Name = ParamName.Substring(2, ParamName.Length - 2);
            if (ParamName == _TextBox_Name_str[3]) //tbPitch
            {
                InsertLog.SavetoDB(8, AxisNoStr + Name + "： " + fram.m_pitch[(int)axisNo] + " -> " + newValue.ToString());
                fram.m_pitch[(int)axisNo] = newValue;
            }
            else if (ParamName == _TextBox_Name_str[4])//tbPitchV
            {
                InsertLog.SavetoDB(8, AxisNoStr + Name + "： " + fram.m_pitchV[(int)axisNo] + " -> " + newValue.ToString());
                fram.m_pitchV[(int)axisNo] = newValue;
            }
            else if (ParamName == _TextBox_Name_str[5]) //tbJogV
            {
                InsertLog.SavetoDB(8, AxisNoStr + Name + "： " + fram.m_jogV[(int)axisNo] + " -> " + newValue.ToString());
                fram.m_jogV[(int)axisNo] = newValue;
            }

            if (ParamName == _ComboBox_Name_str[0])
            {
                InsertLog.SavetoDB(8, AxisNoStr + "Unit" + "： " + fram.m_unit[(int)axisNo] + " -> " + newValue.ToString());
                fram.m_unit[(int)axisNo] = newValue;
            }
        }

        private void btnFunction(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            TableLayoutPanel tlp = new TableLayoutPanel();
            string btntext = btn.Text;
            string btnsavename = "";
            int last;
            double unit = 1;
            Mo.AxisNo axisNo;//= new SSCNET.AxisNo();
            switch (btn.Tag.ToString())
            {
                case "1":
                    axisNo = Mo.AxisNo.DD;
                    tlp = this.tableLayoutPanel1;
                    break;

                case "2":
                    if (MotionType == 0)
                        axisNo = Mo.AxisNo.Z;
                    else
                        axisNo = Mo.AxisNo.X;
                    tlp = this.tableLayoutPanel2;
                    break;

                case "3":
                    axisNo = Mo.AxisNo.Y;
                    tlp = this.tableLayoutPanel3;
                    break;

                default:
                    axisNo = Mo.AxisNo.DD;

                    break;
            }

            switch (btntext)
            {
                case "ResetAlarm":
                    Common.motion.ResetAlarm(axisNo);
                    break;

                case "Find Home":
                    Common.motion.FindHome(axisNo);
                    HomeAdditionMove[(short)axisNo] = true;
                    break;

                case "Servo On":
                    Common.motion.SetServo(axisNo, true);
                    break;

                case "Servo Off":
                    Common.motion.SetServo(axisNo, false);
                    break;

                case "Pitch↑":
                    Common.motion.Pitch(axisNo, Mo.Dir.Postive);
                    break;

                case "Pitch↓":
                    Common.motion.Pitch(axisNo, Mo.Dir.Negative);
                    break;

                case "Pitch←":
                    Common.motion.Pitch(axisNo, Mo.Dir.Postive);
                    break;

                case "Pitch→":
                    Common.motion.Pitch(axisNo, Mo.Dir.Negative);
                    break;

                case "PitchCCW":

                    foreach (Control ctl in tlp.Controls)
                    {
                        if (ctl is ComboBox)
                        {
                            unit = double.Parse(((ComboBox)ctl).Text);
                        }
                    }
                    Common.motion.PitchAngle(axisNo, Mo.Dir.Negative, fram.m_pitch[(int)axisNo] * unit);
                    break;

                case "PitchCW":
                    foreach (Control ctl in tlp.Controls)
                    {
                        if (ctl is ComboBox)
                        {
                            unit = double.Parse(((ComboBox)ctl).Text);
                        }
                    }
                    Common.motion.PitchAngle(axisNo, Mo.Dir.Postive, fram.m_pitch[(int)axisNo] * unit);
                    break;

                case "Save":
                    string pattern = "";
                    //pattern = "^[+]?([0-9]+\\.?)?[0-9]+$";
                    pattern = "^([0-9]+\\.?)?[0-9]+$";
                    foreach (Control ctrl in tlp.Controls)
                    {
                        if (ctrl is TextBox)
                        {
                            last = ctrl.Name.LastIndexOf("copy");
                            if (last > 0 && ctrl.Text != "")
                            {
                                if (Regex.IsMatch(ctrl.Text, pattern))         // 判斷數字輸入
                                {
                                    btnsavename = ctrl.Name.Substring(0, last - 1);
                                    foreach (Control ctrl2 in tlp.Controls)     // 扣掉最後面的_copy找一樣的名字
                                    {
                                        if (ctrl2 is TextBox && ctrl2.Name == btnsavename)
                                        {
                                            ctrl2.Text = ctrl.Text;
                                            SavePara(axisNo, ctrl2.Name, double.Parse(ctrl.Text));
                                        }
                                    }
                                    ctrl.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show(ctrl.Text + "資料輸入錯誤");
                                }
                            }
                        }
                        else if (ctrl is ComboBox)
                        {
                            SavePara(axisNo, ctrl.Name, double.Parse(ctrl.Text));
                        }
                    }
                    ParamFile.saveparam("Motion");
                    break;

                case "Clear":

                    foreach (Control ctrl in tlp.Controls)
                    {
                        if (ctrl is TextBox)
                        {
                            last = ctrl.Name.LastIndexOf("copy");
                            if (last > 0)
                            {
                                ctrl.Text = "";
                            }
                        }
                    }
                    break;

                case "EMG Stop":
                    Common.motion.Stop(axisNo);
                    break;

                default:
                    break;
            }
        }

        private void btnMouseUp(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            Mo.AxisNo axisNo = new Mo.AxisNo();
            switch (btn.Tag.ToString())
            {
                case "1":
                    axisNo = Mo.AxisNo.DD;
                    break;

                case "2":
                    if (MotionType == 0)
                        axisNo = Mo.AxisNo.Z;
                    else
                        axisNo = Mo.AxisNo.X;
                    break;

                case "3":
                    axisNo = Mo.AxisNo.Y;
                    break;

                default:
                    break;
            }
            Common.motion.JogStop(axisNo);
        }

        private void btnMouseDown(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            string btntext = btn.Text;
            Mo.AxisNo axisNo = new Mo.AxisNo();
            switch (btn.Tag.ToString())
            {
                case "1":
                    axisNo = Mo.AxisNo.DD;
                    break;

                case "2":
                    if (MotionType == 0)
                        axisNo = Mo.AxisNo.Z;
                    else
                        axisNo = Mo.AxisNo.X;
                    break;

                case "3":
                    axisNo = Mo.AxisNo.Y;
                    break;

                default:
                    break;
            }
            if (HomeAdditionMove[(short)axisNo] == true)
                return;
            switch (btntext)
            {
                case "Jog↑":
                    Common.motion.JogStart(axisNo, Mo.Dir.Postive);
                    break;

                case "Jog↓":
                    Common.motion.JogStart(axisNo, Mo.Dir.Negative);
                    break;

                case "Jog←":
                    Common.motion.JogStart(axisNo, Mo.Dir.Postive);
                    break;

                case "Jog→":
                    Common.motion.JogStart(axisNo, Mo.Dir.Negative);
                    break;

                case "JogCW":
                    Common.motion.JogStart(axisNo, Mo.Dir.Postive);
                    break;

                case "JogCCW":
                    Common.motion.JogStart(axisNo, Mo.Dir.Negative);
                    break;

                default:
                    MessageBox.Show("error");
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                
                 if (MotionType == 0)
                {
                    if (Common.motion.ServoReady(Mo.AxisNo.DD))  // 0
                    {
                        button1.BackColor = Color.LimeGreen;
                        button2.BackColor = SystemColors.Control;
                    }
                    else
                    {
                        button1.BackColor = SystemColors.Control;
                        button2.BackColor = Color.LimeGreen;
                    }
                    if (Common.motion.ServoReady(Mo.AxisNo.Z)) // 1
                    {
                        button15.BackColor = Color.LimeGreen;
                        button17.BackColor = SystemColors.Control;
                    }
                    else
                    {
                        button15.BackColor = SystemColors.Control;
                        button17.BackColor = Color.LimeGreen;
                    }
                }
                else
                {
                    if (Common.motion.ServoReady(Mo.AxisNo.DD))  // 0
                    {
                        button1.BackColor = Color.LimeGreen;
                        button2.BackColor = SystemColors.Control;
                    }
                    else
                    {
                        button1.BackColor = SystemColors.Control;
                        button2.BackColor = Color.LimeGreen;
                    }
                    if (Common.motion.ServoReady(Mo.AxisNo.X))  // 1
                    {
                        button15.BackColor = Color.LimeGreen;
                        button17.BackColor = SystemColors.Control;
                    }
                    else
                    {
                        button15.BackColor = SystemColors.Control;
                        button17.BackColor = Color.LimeGreen;
                    }
                    if (Common.motion.ServoReady(Mo.AxisNo.Y))  // 2
                    {
                        button24.BackColor = Color.LimeGreen;
                        button26.BackColor = SystemColors.Control;
                    }
                    else
                    {
                        button24.BackColor = SystemColors.Control;
                        button26.BackColor = Color.LimeGreen;
                    }
                }
                

                for (int i = 0; i < _TextBox.Length; i++)
                {
                    if ((MotionType == 0))
                    {
                        if (i / _TextBox_Name_str.Length == 0)
                        {
                            _AxisNo = Mo.AxisNo.DD;
                        }
                        else if (i / _TextBox_Name_str.Length > 0)
                        {
                            _AxisNo = Mo.AxisNo.Z;
                        }
                    }
                    else
                    {
                        if (i / _TextBox_Name_str.Length == 0)
                        {
                            _AxisNo = Mo.AxisNo.DD;
                        }
                        else if (i / _TextBox_Name_str.Length == 1)
                        {
                            _AxisNo = Mo.AxisNo.X;
                        }
                        else if (i / _TextBox_Name_str.Length == 2)
                        {
                            _AxisNo = Mo.AxisNo.Y;
                        }
                    }
                   

                    if (_TextBox[i].Name == _TextBox_Name_str[0])       // tbCmdpos
                    {
                        _TextBox[i].Text = Common.motion.Get_CmdPos(_AxisNo).ToString();
                    }
                    else if (_TextBox[i].Name == _TextBox_Name_str[1])  // tbCurrentpos
                    {
                        _TextBox[i].Text = Common.motion.Get_FBPos(_AxisNo).ToString();
                    }
                    else if (_TextBox[i].Name == _TextBox_Name_str[2])  // tbMotorSts
                    {
                        _ServoSts[0] = Common.motion.ServoReady(_AxisNo);
                        _ServoSts[1] = Common.motion.MotionDone(_AxisNo);
                        Common.motion.Get_Alarm1(_AxisNo, out _code[0], out _detailCode[0]);
                        _ServoSts[2] = _code[0] == 0 ? true : false;
                        Common.motion.Get_Alarm2(_AxisNo, out _code[1], out _detailCode[1]);
                        _ServoSts[3] = _code[1] == 0 ? true : false;
                        if ( MotionType == 0)
                        {
                            _TextBox[i].BackColor = SystemColors.Control;
                            if (!_ServoSts[0])
                            {
                                _TextBox[i].Text = "Servo Off";
                                _TextBox[i].BackColor = Color.Red;
                            }
                            else if (_ServoSts[0] && _ServoSts[1] && _ServoSts[2] && _ServoSts[3])
                            {
                                _TextBox[i].Text = "Ready";
                            }
                            else if (_ServoSts[0] && !_ServoSts[1] && _ServoSts[2] && _ServoSts[3])
                            {
                                _TextBox[i].Text = "Moving";
                            }
                            else if (_ServoSts[0] && !_ServoSts[2])
                            {
                                _TextBox[i].Text = _code[0].ToString("X") + "(" + _detailCode[0].ToString() + ")";
                                _TextBox[i].BackColor = Color.Red;
                            }
                            else if (_ServoSts[0] && !_ServoSts[3])
                            {
                                _TextBox[i].Text = _code[1].ToString("X") + "(" + _detailCode[1].ToString() + ")";
                                _TextBox[i].BackColor = Color.Red;
                            }
                        }
                        else if (MotionType == 1)
                        {
                            _TextBox[i].BackColor = SystemColors.Control;
                            if (!_ServoSts[0])
                            {
                                _TextBox[i].Text = "Servo Off";
                                _TextBox[i].BackColor = Color.Red;
                            }
                            else if (_ServoSts[0] && _ServoSts[1] && _ServoSts[2] && _ServoSts[3])
                            {
                                _TextBox[i].Text = "Ready";
                                if (HomeAdditionMove[(short)_AxisNo] == true)
                                {
                                    double pos = Common.motion.Get_CmdPos(_AxisNo);
                                    if ( pos == 0)
                                    {
                                        HomeAdditionMove[(short)_AxisNo] = false;
                                    }
                                    else
                                    {
                                        Common.motion.PosMove(_AxisNo, 0);
                                    }
                                }
                            }
                            else if (_ServoSts[0] && !_ServoSts[1] && _ServoSts[2] && _ServoSts[3])
                            {
                                _TextBox[i].Text = "Moving";
                            }
                            else if (_ServoSts[0] && !_ServoSts[2])
                            {
                                _TextBox[i].Text = _code[0].ToString("X") + "(" + _detailCode[0].ToString() + ")";
                                _TextBox[i].BackColor = Color.Red;
                            }
                            else if (_ServoSts[0] && !_ServoSts[3])
                            {
                                _TextBox[i].Text = _code[1].ToString("X") + "(" + _detailCode[1].ToString() + ")";
                                _TextBox[i].BackColor = Color.Red;
                            }
                        }
                        
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void MotionControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}