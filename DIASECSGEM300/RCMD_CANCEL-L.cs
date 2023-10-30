using Delta.DIAAuto.DIASECSGEM.GEMDataModel;
using Delta.DIAAuto.DIASECSGEM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DemoFormDiaGemLib
{
    public class RCMD_CANCEL_L
    {
        public string LOADPORT_ID { get; set; }

        public RCMD_CANCEL_L()
        {
            LOADPORT_ID = string.Empty;
        }
    }
    public partial class MainForm
    {
        public bool RunDecode(RemoteControlEventRemoteCommandArgs e, out RCMD_CANCEL_L rcmdInfo)
        {
            bool bDataCheckOK = true;
            rcmdInfo = new RCMD_CANCEL_L();
            foreach (CommandParameter obj in e.ReceiveCommandParameters)
            {
                switch (obj.Name)
                {
                    case "LOADPORT-ID":
                        {
                            if (obj.Format == ItemFmt.A)
                            {
                                string LOADPORT_ID = (string)obj.Value;
                                if (LOADPORT_ID.Trim() != string.Empty)
                                {
                                    //OK, Keep Data..
                                    rcmdInfo.LOADPORT_ID = (string)obj.Value;
                                }
                                else
                                {
                                    //NG ,Reply Info..
                                    obj.Ack = (byte)2;
                                    bDataCheckOK = false;
                                }
                            }
                            else
                            {
                                //NG ,Reply Info..
                                obj.Ack = (byte)3;
                                bDataCheckOK = false;
                            }
                        }
                        break;
                }

                this.Invoke((MethodInvoker)delegate ()
                {
                    string[] row = new string[] { obj.Name, obj.Value.ToString(), obj.Ack.ToString() };  // 定義一列的字串陣列
                    dgvCPs.Rows.Add(row);           // 加入列  
                });
            }

            return bDataCheckOK;
        }
    }
}
