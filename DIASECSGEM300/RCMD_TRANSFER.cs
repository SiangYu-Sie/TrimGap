using Delta.DIAAuto.DIASECSGEM;
using Delta.DIAAuto.DIASECSGEM.GEMDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DemoFormDiaGemLib
{
    public class RCMD_TRANSFER
    {
        public CommandInfo COMMANDINFO { get; set; }
        public TransferInfo TRANSFERINFO { get; set; }

        public RCMD_TRANSFER()
        {
            COMMANDINFO = new CommandInfo();
            TRANSFERINFO = new TransferInfo();
        }

        public class CommandInfo
        {
            public string COMMANDID { get; set; }
            public ushort PRIORITY { get; set; }
        }
        public class TransferInfo
        {
            public string CARRIERID { get; set; }
            public string SOURCE { get; set; }
            public string DEST { get; set; }
        }
    }
    public partial class MainForm
    {
        public bool RunDecode(RemoteControlEventRemoteCommandArgs e, out RCMD_TRANSFER rcmdInfo)
        {
            bool bDataCheckOK = true;
            rcmdInfo = new RCMD_TRANSFER();

            foreach (CommandParameter obj in e.ReceiveCommandParameters)
            {
                //Case : obj.Value = source value type array[]
                //       obj.Ack   = byte
                if (obj.Format != ItemFmt.L)
                {
                    
                }
                else
                {
                    switch (obj.ValueFormatCase)
                    {
                        //LIST_B : obj.Value = List<CommandParameter>
                        case eCPValueFormatCase.LIST_B:
                            {
                                switch (obj.Name)
                                {
                                    case "COMMANDINFO":
                                        {
                                            //List<CommandParameter>
                                            List<CommandParameter> subListCPs = (List<CommandParameter>)obj.Value;

                                            foreach (CommandParameter subObj in subListCPs)
                                            {
                                                //Case : obj.Value = source value type array[]
                                                //       obj.Ack   = byte
                                                if (subObj.Format != ItemFmt.L)
                                                {
                                                    switch (subObj.Name)
                                                    {
                                                        case "COMMANDID":
                                                            {
                                                                //ItemFmt.A
                                                                if (subObj.Format == ItemFmt.A)
                                                                {
                                                                    //OK, Keep Data..
                                                                    rcmdInfo.COMMANDINFO.COMMANDID = ((string)subObj.Value);
                                                                }
                                                                else
                                                                {
                                                                    //NG ,Reply Info..
                                                                    subObj.Ack = (byte)3;
                                                                    bDataCheckOK = false;
                                                                }
                                                            }
                                                            break;
                                                        case "PRIORITY":
                                                            {
                                                                //ItemFmt.U2
                                                                if (subObj.Format == ItemFmt.U2)
                                                                {
                                                                    //OK, Keep Data..
                                                                    rcmdInfo.COMMANDINFO.PRIORITY = ((ushort[])subObj.Value)[0];
                                                                }
                                                                else
                                                                {
                                                                    //NG ,Reply Info..
                                                                    subObj.Ack = (byte)3;
                                                                    bDataCheckOK = false;
                                                                }
                                                            }
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    switch (subObj.ValueFormatCase)
                                                    {
                                                        //LIST_A : obj.Value = List<Tuple<ItemFmt, object>>
                                                        //         obj.Ack   = List<byte>
                                                        case eCPValueFormatCase.LIST_A:
                                                            {

                                                            }
                                                            break;
                                                        //LIST_B : obj.value = List<CommandParameter>
                                                        case eCPValueFormatCase.LIST_B:
                                                            {

                                                            }
                                                            break;
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    case "TRANSFERINFO":
                                        {
                                            //List<CommandParameter>
                                            List<CommandParameter> subListCPs = (List<CommandParameter>)obj.Value;

                                            foreach (CommandParameter subObj in subListCPs)
                                            {
                                                //Case : obj.Value = source value type array[]
                                                //       obj.Ack   = byte
                                                if (subObj.Format != ItemFmt.L)
                                                {
                                                    switch (subObj.Name)
                                                    {
                                                        case "CARRIERID":
                                                            {
                                                                //ItemFmt.A
                                                                if (subObj.Format == ItemFmt.A)
                                                                {
                                                                    //OK, Keep Data..
                                                                    rcmdInfo.TRANSFERINFO.CARRIERID = ((string)subObj.Value);
                                                                }
                                                                else
                                                                {
                                                                    //NG ,Reply Info..
                                                                    subObj.Ack = (byte)3;
                                                                    bDataCheckOK = false;
                                                                }
                                                            }
                                                            break;
                                                        case "SOURCE":
                                                            {
                                                                //ItemFmt.A
                                                                if (subObj.Format == ItemFmt.A)
                                                                {
                                                                    //OK, Keep Data..
                                                                    rcmdInfo.TRANSFERINFO.SOURCE = ((string)subObj.Value);
                                                                }
                                                                else
                                                                {
                                                                    //NG ,Reply Info..
                                                                    subObj.Ack = (byte)3;
                                                                    bDataCheckOK = false;
                                                                }
                                                            }
                                                            break;
                                                        case "DEST":
                                                            {
                                                                //ItemFmt.A
                                                                if (subObj.Format == ItemFmt.A)
                                                                {
                                                                    //OK, Keep Data..
                                                                    rcmdInfo.TRANSFERINFO.DEST = ((string)subObj.Value);
                                                                }
                                                                else
                                                                {
                                                                    //NG ,Reply Info..
                                                                    subObj.Ack = (byte)3;
                                                                    bDataCheckOK = false;
                                                                }
                                                            }
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    switch (subObj.ValueFormatCase)
                                                    {
                                                        //LIST_A : obj.Value = List<Tuple<ItemFmt, object>>
                                                        //         obj.Ack   = List<byte>
                                                        case eCPValueFormatCase.LIST_A:
                                                            {

                                                            }
                                                            break;
                                                        //LIST_B : obj.Value = List<CommandParameter>
                                                        case eCPValueFormatCase.LIST_B:
                                                            {

                                                            }
                                                            break;
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                }
                                break;
                            }
                    }
                }
            }

            return bDataCheckOK;
        }
    }
}
