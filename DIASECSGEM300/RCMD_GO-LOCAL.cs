using Delta.DIAAuto.DIASECSGEM;
using Delta.DIAAuto.DIASECSGEM.GEMDataModel;
using DemoFormDiaGemLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoFormDiaGemLib
{
    public class RCMD_GO_LOCAL
    {
        public RCMD_GO_LOCAL()
        {
        }
    }
    public partial class MainForm
    {
        public bool RunDecode(RemoteControlEventRemoteCommandArgs e, out RCMD_GO_LOCAL rcmdInfo)
        {
            bool bDataCheckOK = true;
            rcmdInfo = new RCMD_GO_LOCAL();

            return bDataCheckOK;
        }
    }
}
