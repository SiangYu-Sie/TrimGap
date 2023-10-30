using Delta.DIAAuto.DIASECSGEM;
using Delta.DIAAuto.DIASECSGEM.GEMDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DemoFormDiaGemLib
{
    public class RCMD_STOP
    {
        public RCMD_STOP()
        {
        }
    }
    public partial class MainForm
    {
        public bool RunDecode(RemoteControlEventRemoteCommandArgs e, out RCMD_STOP rcmdInfo)
        {
            bool bDataCheckOK = true;
            rcmdInfo = new RCMD_STOP();

            return bDataCheckOK;
        }
    }
}
