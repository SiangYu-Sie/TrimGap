using Delta.DIAAuto.DIASECSGEM.GEMDataModel;
using Delta.DIAAuto.DIASECSGEM;
using DemoFormDiaGemLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoFormDiaGemLib
{
    public class RCMD_GO_REMOTE
    {
        public RCMD_GO_REMOTE()
        {
        }
    }
    public partial class MainForm
    {
        public bool RunDecode(RemoteControlEventRemoteCommandArgs e, out RCMD_GO_REMOTE rcmdInfo)
        {
            bool bDataCheckOK = true;
            rcmdInfo = new RCMD_GO_REMOTE();

            return bDataCheckOK;
        }
    }
}
