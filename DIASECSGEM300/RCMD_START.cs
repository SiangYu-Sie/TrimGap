using Delta.DIAAuto.DIASECSGEM;
using Delta.DIAAuto.DIASECSGEM.GEMDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DemoFormDiaGemLib
{
    public class RCMD_START
    {
        public RCMD_START()
        {
        }
    }
    public partial class MainForm
    {
        public bool RunDecode(RemoteControlEventRemoteCommandArgs e, out RCMD_START rcmdInfo)
        {
            bool bDataCheckOK = true;
            rcmdInfo = new RCMD_START();

            return bDataCheckOK;
        }
    }
}
