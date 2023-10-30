using Delta.DIAAuto.DIASECSGEM;
using Delta.DIAAuto.DIASECSGEM.GEMDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DemoFormDiaGemLib
{
    public class RCMD_GO
    {
        public string LOADPORT_ID { get; set; }
        public string PPID { get; set; }
        public TagInfo TAG { get; set; }

        public RCMD_GO()
        {
            LOADPORT_ID = string.Empty;
            PPID = string.Empty;
            TAG = new TagInfo();
        }

        public class TagInfo
        {
            public string Tag1 { get; set; }
            public uint Tag2 { get; set; }
            public short Tag3 { get; set; }
        }
    }
    public partial class MainForm
    {
        public bool RunDecode(RemoteControlEventRemoteCommandArgs e, out RCMD_GO rcmdInfo)
        {
            bool bDataCheckOK = true;
            rcmdInfo = new RCMD_GO();

            foreach (CommandParameter obj in e.ReceiveCommandParameters)
            {
                //Case : obj.Value = source value type array[]
                //       obj.Ack   = byte
                if (obj.Format != ItemFmt.L)
                {
                    switch (obj.Name)
                    {
                        case "LOADPORT_ID":
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
                        case "PPID":
                            {
                                if (obj.Format == ItemFmt.A)
                                {
                                    string strPPID = (string)obj.Value;
                                    if (strPPID.Trim() != string.Empty)
                                    {
                                        //OK, Keep Data..
                                        rcmdInfo.PPID = (string)obj.Value;
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
                }
                else
                {
                    switch (obj.ValueFormatCase)
                    {
                        //LIST_A : obj.Value = List<Tuple<ItemFmt, object>>
                        //         obj.Ack   = List<byte>
                        case eCPValueFormatCase.LIST_A:
                            {
                                switch (obj.Name)
                                {
                                    case "TAG":
                                        {
                                            //List<Tuple<ItemFmt, object>>
                                            List<Tuple<ItemFmt, object>> listValue = (List<Tuple<ItemFmt, object>>)obj.Value;

                                            List<byte> listReplyAck = new List<byte>();

                                            Tuple<ItemFmt, object> item1 = listValue[0];
                                            //ItemFmt.A
                                            //item1.Item1
                                            if (item1.Item1 == ItemFmt.A)
                                            {
                                                //OK, Keep Data..
                                                rcmdInfo.TAG.Tag1 = ((string)item1.Item2);
                                                listReplyAck.Add((byte)0);
                                            }
                                            else
                                            {
                                                //NG ,Reply Info..
                                                listReplyAck.Add((byte)3);
                                                bDataCheckOK = false;
                                            }

                                            Tuple<ItemFmt, object> item2 = listValue[1];
                                            //ItemFmt.U4
                                            //item2.Item1
                                            if (item2.Item1 == ItemFmt.U4)
                                            {
                                                //OK, Keep Data..
                                                rcmdInfo.TAG.Tag2 = ((uint[])item2.Item2)[0];
                                                listReplyAck.Add((byte)0);
                                            }
                                            else
                                            {
                                                //NG ,Reply Info..
                                                listReplyAck.Add((byte)3);
                                                bDataCheckOK = false;
                                            }

                                            Tuple<ItemFmt, object> item3 = listValue[2];
                                            //ItemFmt.I2
                                            //item3.Item1
                                            short item3Value = ((short[])item3.Item2)[0];
                                            if (item3.Item1 == ItemFmt.I2)
                                            {
                                                //OK, Keep Data..
                                                rcmdInfo.TAG.Tag3 = ((short[])item3.Item2)[0];
                                                listReplyAck.Add((byte)0);
                                            }
                                            else
                                            {
                                                //NG ,Reply Info..
                                                listReplyAck.Add((byte)3);
                                                bDataCheckOK = false;
                                            }

                                            obj.Ack = listReplyAck;
                                        }
                                        break;
                                }
                            }
                            break;
                    }
                }
            }

            return bDataCheckOK;
        }
    }
}
