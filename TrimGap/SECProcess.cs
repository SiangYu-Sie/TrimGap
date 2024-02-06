using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.IO;

namespace TrimGap
{
    public partial class SECProcess : Form
    {
        public static DemoFormDiaGemLib.MainForm MainForm;
        public static string err = string.Empty;

        #region  //======================  區域變數設置 ======================

        static object m_objLock = new object();

        /// <summary> 是否刷新DirectRow數量 </summary>
        public bool bRefreshDirectRowNum = true;

        /// <summary> 是否刷新 Foup1 狀態 </summary>
        public bool bRefreshFoup1Status = false;

        /// <summary> 是否刷新 Foup2 狀態 </summary>
        public bool bRefreshFoup2Status = false;

        /// <summary> 是否刷新 下牙插 狀態 </summary>
        public bool bRefreshLowerArmStatus = false;

        /// <summary> 是否刷新 上牙插 狀態 </summary>
        public bool bRefreshUpperArmStatus = false;

        /// <summary> 是否刷新 對準器 狀態 </summary>
        public bool bRefreshAlignerStatus = false;

        /// <summary> 是否刷新 Statge 狀態 </summary>
        public bool bRefreshStatgeStatus = false;

        /// <summary> 是否刷新 對準器OCR </summary>
        public bool bRefreshAlignerOCR = false;

        /// <summary>最後一個點擊Panel內元件的號碼</summary>
        public int iNowShowImageNum = 0;

        /// <summary>取得或設置晶圓目前計數</summary>
        public int iTotalWaferCounter
        {
            get;
            set;
        }

        /// <summary>取得或設置晶圓目前計數</summary>
        public int iTotalWaferCounter5Min
        {
            get;
            set;
        }

        /// <summary>取得或設置Pass目前計數</summary>
        public int iPassProductCounter
        {
            get;
            set;
        }

        /// <summary>取得或設置Pass目前計數</summary>
        public int iPassProductCounter5Min
        {
            get;
            set;
        }

        /// <summary>取得或設置Ng目前計數</summary>
        public int iNgProductCounter
        {
            get;
            set;
        }

        /// <summary>取得或設置Pass目前計數</summary>
        public int iNgProductCounter5Min
        {
            get;
            set;
        }

        /// <summary>顯示於介面的時間計算</summary>
        public string ShowRunTime = "";
        public string ShowStopTime = "";
        public string ShowWaitingTime = "";

        public bool Wafer_List_Clicked = false;
        CheckBoxState Wafer_List_State;

        public bool Run_PJ_List_Clicked = false;
        CheckBoxState Run_PJ_List_State;

        public bool Run_CJ_List_Clicked = false;
        CheckBoxState Run_CJ_List_State;

        #endregion

        public SECProcess()
        {
            InitializeComponent();

            MainForm = new DemoFormDiaGemLib.MainForm();

            SetToolTip();

            Refresh_Wafer_List_Status();
            InitComboBox();

        }

        private void btn_Add_PJ_Click(object sender, EventArgs e)
        {
            

            string err;
            string foupID;
            bool bStart = false;
            byte valMethod = (byte)0;
            byte[] slotmap = new byte[25];
            bool bSlot = false;
            for (int i = 0; i < 25; i++)
            {
                slotmap[i] = (byte)(list_Wafer.Items[i].Checked ? 1 : 0);
                bSlot |= list_Wafer.Items[i].Checked;
            }
            if (radio_Foup1.Checked)
                foupID = "1";
            else
                foupID = "2";

            if (comboBox_PRProcessStart.SelectedItem.ToString() == "True")
                bStart = true;

            if (comboBox_PRRecipeMethod.SelectedItem.ToString() == "True")
                valMethod = (byte)1;

            if (list_Recipe.SelectedItems.Count == 1 && bSlot)
            {
                int rtn = MainForm.CreateProcessJob(text_Add_PJ.Text, list_Recipe.SelectedItems[0].SubItems[1].Text, bStart, foupID, slotmap, out err);
                if ((valMethod == 1 || textBox_PauseEvent.Text != String.Empty) && rtn == 0)
                {
                    //PJState = 255 就是不變更 slot = null 就是不變更
                    rtn = MainForm.SetProcessJobAttr(text_Add_PJ.Text, textBox_PauseEvent.Text, foupID, null, (byte)13, bStart, valMethod, list_Recipe.SelectedItems[0].SubItems[1].Text, textBox_RecVariableList.Text, out err);
                }
                else if (rtn == 1)
                {
                    MessageBox.Show("系統尚未初始化，無法取得Object");
                }
                else if (rtn == 2)
                {
                    MessageBox.Show("系統沒有掛載ObjectServiceStandard模組，停止處理API函式");
                }
                else if (rtn == 3)
                {
                    MessageBox.Show("系統License授權過期，停止處理API函式");
                }
                else if (rtn == 4)
                {
                    MessageBox.Show("系統不支援此ObjectType，停止處理API函式");
                }
                else if (rtn == 5)
                {
                    MessageBox.Show("系統已存在此Object資訊，停止處理API函式");
                }
                else if (rtn == 6)
                {
                    MessageBox.Show("系統處理功能發生問題，停止處理API函式");
                }
            }
            else if (list_Recipe.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Recipe Selected.");
            }
            else if (!bSlot)
            {
                MessageBox.Show("No Slot Selected.");
            }
            Refresh_Run_PJ_List_Status();
            /*
            rtn:
            0 : 取得系統Object對應的資料(正常)
            1 : 系統尚未初始化，無法取得Object
            2 : 系統沒有掛載ObjectServiceStandard模組，停止處理API函式
            3 : 系統License授權過期，停止處理API函式
            4 : 系統不支援此ObjectType，停止處理API函式
            5 : 系統已存在此Object資訊，停止處理API函式
            6 : 系統處理功能發生問題，停止處理API函式
             */

        }

        public void Refresh_Run_PJ_List_Status()
        {
            //MainForm = new DemoFormDiaGemLib.MainForm();
            list_Run_PJ.Clear();
            list_Run_PJ.Columns.Add("", 25, HorizontalAlignment.Center);
            list_Run_PJ.Columns.Add("PJ", 215, HorizontalAlignment.Center);

            list_Run_PJ.View = View.Details;
            list_Run_PJ.GridLines = true;
            list_Run_PJ.FullRowSelect = true;
            list_Run_PJ.OwnerDraw = true;
            list_Run_PJ.CheckBoxes = true;
            list_Run_PJ.HeaderStyle = ColumnHeaderStyle.Clickable;

            list_Run_PJ.AllowColumnReorder = true;
            List<string> pj = DemoFormDiaGemLib.MainForm.PJ_list;
            list_Run_PJ.BeginUpdate();

            for (int i = 1; i <= pj.Count; i++)
            {
                list_Run_PJ.Items.Add("folder" + (i - 1), "", 0);
                list_Run_PJ.Items["folder" + (i - 1)].SubItems.Add(pj[i - 1]);
            }
            if (pj.Count == 0)
            {
                label_PJ_Info.Text = "";
                label_Run_PJ_Info.Text = "";
            }
            list_Run_PJ.EndUpdate();

        }

        private void btn_Add_CJ_Click(object sender, EventArgs e)
        {
            //MainForm = new DemoFormDiaGemLib.MainForm();

            string err;
            string foupID;
            bool bStart = false;
            byte processOrder = (byte)2;
            byte[] slotmap = new byte[25];
            int rtn;
            List<string> ls = new List<string>();
            //string pjname;
            for (int i = 0; i < list_Run_PJ.Items.Count; i++)
            {
                if (list_Run_PJ.Items[i].Checked)
                    ls.Add(list_Run_PJ.Items[i].SubItems[1].Text);
            }
            if (ls.Count == 0)
            {
                MessageBox.Show("No ProcessJob Selected.");
                return;
            }
            if (radio_Foup1.Checked)
                foupID = "1";
            else
                foupID = "2";

            if (comboBox_ControlJobStart.SelectedItem.ToString() == "True")
                bStart = true;

            if (comboBox_ProcessOrderMgmt.SelectedItem.ToString() == "ARRIVAL")
                processOrder = (byte)1;
            else if (comboBox_ProcessOrderMgmt.SelectedItem.ToString() == "LIST")
                processOrder = (byte)3;

            rtn = MainForm.CreateControlJob(text_Add_CJ.Text, foupID, "", ls.ToArray(), processOrder, bStart, out err);
            if (rtn == 0)
            {
                //PJState = 255 就是不變更 slot = null 就是不變更
                //rtn = ucTlhomeSECS.GetSingleton().SetProcessJobAttr(text_Add_PJ.Text, textBox_PauseEvent.Text, (byte)255, foupID, null, (byte)13, bStart, valMethod, list_Recipe.SelectedItems[0].SubItems[1].Text, textBox_RecVariableList.Text, out err);
            }
            else if (rtn == 1)
            {
                MessageBox.Show("系統尚未初始化，無法取得Object");
            }
            else if (rtn == 2)
            {
                MessageBox.Show("系統沒有掛載ObjectServiceStandard模組");
            }
            else if (rtn == 3)
            {
                MessageBox.Show("系統License授權過期");
            }
            else if (rtn == 4)
            {
                MessageBox.Show("系統不支援此ObjectType");
            }
            else if (rtn == 5)
            {
                MessageBox.Show("系統已存在此Object資訊");
            }
            else if (rtn == 6)
            {
                MessageBox.Show("系統處理功能發生問題");
            }
            Refresh_Run_CJ_List_Status();
            Refresh_CJ_List_Status();
            /*
            rtn:
            0 : 取得系統Object對應的資料(正常)
            1 : 系統尚未初始化，無法取得Object
            2 : 系統沒有掛載ObjectServiceStandard模組，停止處理API函式
            3 : 系統License授權過期，停止處理API函式
            4 : 系統不支援此ObjectType，停止處理API函式
            5 : 系統已存在此Object資訊，停止處理API函式
            6 : 系統處理功能發生問題，停止處理API函式
             */
        }

        /// <summary> 刷新顯示頁Run_PJ_LisetView顯示內容 </summary>
        public void Refresh_Run_CJ_List_Status()
        {
            MainForm = new DemoFormDiaGemLib.MainForm();

            list_Run_CJ.Clear();
            list_Run_CJ.Columns.Add("", 0, HorizontalAlignment.Center);
            list_Run_CJ.Columns.Add("CJ", 260, HorizontalAlignment.Center);

            list_Run_CJ.View = View.Details;
            list_Run_CJ.GridLines = true;
            list_Run_CJ.FullRowSelect = true;
            list_Run_CJ.HeaderStyle = ColumnHeaderStyle.Clickable;

            list_Run_CJ.AllowColumnReorder = true;
            List<string> cj = DemoFormDiaGemLib.MainForm.CJ_list;
            list_Run_CJ.BeginUpdate();
            for (int i = 1; i <= cj.Count; i++)
            {
                list_Run_CJ.Items.Add("folder" + (i - 1), "", 0);
                list_Run_CJ.Items["folder" + (i - 1)].SubItems.Add(cj[i - 1]);
            }
            if (cj.Count == 0)
            {
                label_CJ_Info.Text = "";
                label_Run_CJ_Info.Text = "";
            }
            list_Run_CJ.EndUpdate();

        }

        /// <summary> 刷新顯示頁PJ_LisetView顯示內容 </summary>
        public void Refresh_CJ_List_Status()
        {
            list_CJ.Clear();
            list_CJ.Columns.Add("", 0, HorizontalAlignment.Center);
            list_CJ.Columns.Add("CJ", 260, HorizontalAlignment.Center);

            list_CJ.View = View.Details;
            list_CJ.GridLines = true;
            list_CJ.FullRowSelect = true;
            list_CJ.HeaderStyle = ColumnHeaderStyle.Clickable;

            list_CJ.BeginUpdate();
            for (int i = 1; i < 6; i++)
            {
                list_CJ.Items.Add("folder" + (i - 1), "", 0);
                list_CJ.Items["folder" + (i - 1)].SubItems.Add("CJ-" + i);
            }

            list_CJ.EndUpdate();

        }

        private void PJ_List_Cancel_Click(object sender, EventArgs e)
        {
            MainForm = new DemoFormDiaGemLib.MainForm();

            string err;
            int rtn;
            //string pjname;
            for (int i = list_Run_PJ.Items.Count - 1; i >= 0; i--)
            {
                if (list_Run_PJ.Items[i].Checked)
                    rtn = MainForm.DeleteProcessJob(list_Run_PJ.Items[i].SubItems[1].Text, out err);
            }
            Refresh_Run_PJ_List_Status();
        }

        private void CJ_List_Cancel_Click(object sender, EventArgs e)
        {
            MainForm = new DemoFormDiaGemLib.MainForm();

            string err;
            int rtn;
            //string pjname;
            for (int i = list_Run_CJ.Items.Count - 1; i >= 0; i--)
            {
                if (list_Run_CJ.Items[i].Checked)
                    rtn = MainForm.DeleteControlJob(list_Run_CJ.Items[i].SubItems[1].Text, out err);
            }
            Refresh_Run_CJ_List_Status();
            Refresh_CJ_List_Status();
        }

        private void list_Wafer_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            TextFormatFlags flags = TextFormatFlags.SingleLine | TextFormatFlags.GlyphOverhangPadding | TextFormatFlags.VerticalCenter | TextFormatFlags.WordEllipsis | TextFormatFlags.HorizontalCenter;

            e.DrawBackground();
            CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(4, 5), Wafer_List_State);

            e.DrawText(flags);
        }

        private void list_Wafer_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void list_Wafer_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void list_Wafer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.list_Wafer.SelectedItems.Count == 0)
                return;

            if (list_Wafer.Items[list_Wafer.FocusedItem.Index].Checked == true)
            {
                list_Wafer.Items[list_Wafer.FocusedItem.Index].Checked = false;
            }
            else
            {
                list_Wafer.Items[list_Wafer.FocusedItem.Index].Checked = true;
            }
        }

        private void list_Wafer_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (!Wafer_List_Clicked)
            {
                Wafer_List_Clicked = true;
                Wafer_List_State = CheckBoxState.CheckedPressed;

                foreach (ListViewItem item in list_Wafer.Items)
                {
                    item.Checked = true;
                }

                Invalidate();
            }
            else
            {
                Wafer_List_Clicked = false;
                Wafer_List_State = CheckBoxState.UncheckedNormal;
                Invalidate();

                foreach (ListViewItem item in list_Wafer.Items)
                {
                    item.Checked = false;
                }
            }
        }

        private void list_Run_PJ_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            TextFormatFlags flags = TextFormatFlags.SingleLine | TextFormatFlags.GlyphOverhangPadding | TextFormatFlags.VerticalCenter | TextFormatFlags.WordEllipsis | TextFormatFlags.HorizontalCenter;

            e.DrawBackground();
            CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(4, 5), Run_PJ_List_State);

            e.DrawText(flags);
        }

        private void list_Run_PJ_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void list_Run_PJ_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void list_PJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainForm = new DemoFormDiaGemLib.MainForm();

            ListView ls = (ListView)sender;
            int PJ_index = ls.FocusedItem.Index;

            string strPJ_Info = "";
            string PJID, pauseEvent, carrierID, recID, recVarList, err, PJStateS, slotS, prtypeS, recMethodS;
            byte PJState, PRType, recMethod;
            byte[] slot;
            bool bStart;
            PJID = ls.Items[PJ_index].SubItems[1].Text;
            MainForm.GetProcessJobAttr(PJID, out pauseEvent, out PJState, out carrierID, out slot, out PRType, out bStart, out recMethod, out recID, out recVarList, out err);
            strPJ_Info += "ObjID: " + PJID + "\r\n";
            strPJ_Info += "ObjType: ProcessJob\r\n";
            strPJ_Info += "PauseEvent: " + pauseEvent + "\r\n";
            PJStateS = string.Empty;
            slotS = string.Empty;
            switch (PJState)
            {
                case 0://
                    PJStateS = "QUEUED / POOLED";
                    break;
                case 1://
                    PJStateS = "SETTING UP";
                    break;
                case 2:// 
                    PJStateS = "WAITING FOR START";
                    break;
                case 3://
                    PJStateS = "PROCESSING";
                    break;
                case 4:
                    PJStateS = "PROCESS COMPLETE";
                    break;
                case 6:
                    PJStateS = "PAUSING";
                    break;
                case 7:
                    PJStateS = "PAUSED";
                    break;
                case 8:
                    PJStateS = "STOPPING";
                    break;
                case 9:
                    PJStateS = "ABORTING";
                    break;
                default:
                    break;
            }
            strPJ_Info += "PRJobState: " + PJStateS + "\r\n";
            strPJ_Info += "PRMtlNameList(CarrierID): " + carrierID + "\r\n";
            for (int i = 0; i < 25; i++)
            {
                slotS += slot[i].ToString();
                if (i % 5 == 4)
                    slotS += " ";
            }
            strPJ_Info += "PRMtlNameList(Slot 1->25):\r\n";
            strPJ_Info += slotS + "\r\n";
            prtypeS = PRType == 14 ? "Substrate" : "Carriers";
            strPJ_Info += "PRMtlType: " + prtypeS + "\r\n";
            strPJ_Info += "PRProcessStart: " + bStart + "\r\n";
            recMethodS = recMethod == 0 ? "Recipe only" : "Recipe with Variable Tuning";
            strPJ_Info += "PRRecipeMethod: " + recMethodS + "\r\n";
            strPJ_Info += "RecID: " + recID + "\r\n";
            strPJ_Info += "RecVariableList: " + recVarList + "\r\n";
            if (ls.Name == "list_PJ")
                label_PJ_Info.Text = strPJ_Info;
            else if (ls.Name == "list_Run_PJ")
                label_Run_PJ_Info.Text = strPJ_Info;
        }

        private void list_Run_PJ_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (!Run_PJ_List_Clicked)
            {
                Run_PJ_List_Clicked = true;
                Run_PJ_List_State = CheckBoxState.CheckedPressed;

                foreach (ListViewItem item in list_Run_PJ.Items)
                {
                    item.Checked = true;
                }

                Invalidate();
            }
            else
            {
                Run_PJ_List_Clicked = false;
                Run_PJ_List_State = CheckBoxState.UncheckedNormal;
                Invalidate();

                foreach (ListViewItem item in list_Run_PJ.Items)
                {
                    item.Checked = false;
                }
            }
        }

        private void list_Run_CJ_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (!Run_CJ_List_Clicked)
            {
                Run_CJ_List_Clicked = true;
                Run_CJ_List_State = CheckBoxState.CheckedPressed;

                foreach (ListViewItem item in list_Run_CJ.Items)
                {
                    item.Checked = true;
                }

                Invalidate();
            }
            else
            {
                Run_CJ_List_Clicked = false;
                Run_CJ_List_State = CheckBoxState.UncheckedNormal;
                Invalidate();

                foreach (ListViewItem item in list_Run_CJ.Items)
                {
                    item.Checked = false;
                }
            }
        }

        private void btn_LoadPort_1_Auto_Click(object sender, EventArgs e)
        {
            if (Common.SecsgemForm.isRemote()) //Remote
            {
                MessageBox.Show("The current status is remote control");
            }
            else
            {
                if (sram.UserAuthority == permissionEnum.op && fram.SECSPara.Loadport2_AccessMode == Mode.Auto.GetHashCode())
                {
                    MessageBox.Show("LP2 is Auto Mode");
                    return;
                }

                Common.EFEM.E84.Reset(E84.E84_Num.E841);
                Common.EFEM.E84.SetAuto(E84.E84_Num.E841);

                fram.SECSPara.Loadport1_AccessMode = Mode.Auto.GetHashCode();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_AccessMode, (byte)fram.SECSPara.Loadport1_AccessMode, out err);
                InsertLog.SavetoDB(50, "LP1 Switch to Auto");// Auto
            }
        }

        private void btn_LoadPort_1_Manual_Click(object sender, EventArgs e)
        {
            if (Common.SecsgemForm.isRemote()) //Remote
            {
                MessageBox.Show("The current status is remote control");
            }
            else
            {
                if (Flag.AutoidleFlag)
                {
                    MessageBox.Show("請先停止後再切換");
                    return;
                }

                Common.EFEM.E84.SetManual(E84.E84_Num.E841);
                Console.WriteLine("LP1 Switch to Manual");
                fram.SECSPara.Loadport1_AccessMode = Mode.Manual.GetHashCode();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport1_AccessMode, (byte)fram.SECSPara.Loadport1_AccessMode, out err);
                InsertLog.SavetoDB(51, "LP1 Switch to Manual");// Manual
            }
        }

        private void btn_LoadPort_2_Auto_Click(object sender, EventArgs e)
        {
            if (Common.SecsgemForm.isRemote()) //Remote
            {
                MessageBox.Show("The current status is remote control");
            }
            else
            {
                if (sram.UserAuthority == permissionEnum.op && fram.SECSPara.Loadport1_AccessMode == Mode.Auto.GetHashCode())
                {
                    MessageBox.Show("LP1 is Auto Mode");
                    return;
                }

                Common.EFEM.E84.Reset(E84.E84_Num.E842);
                Common.EFEM.E84.SetAuto(E84.E84_Num.E842);

                fram.SECSPara.Loadport2_AccessMode = Mode.Auto.GetHashCode();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_AccessMode, (byte)fram.SECSPara.Loadport2_AccessMode, out err);
                InsertLog.SavetoDB(50, "LP2 Switch to Auto");// Auto
            }
        }

        private void btn_LoadPort_2_Manual_Click(object sender, EventArgs e)
        {
            if (Common.SecsgemForm.isRemote()) //Remote
            {
                MessageBox.Show("The current status is remote control");
            }
            else
            {
                if (Flag.AutoidleFlag)
                {
                    MessageBox.Show("請先停止後再切換");
                    return;
                }
                Common.EFEM.E84.SetManual(E84.E84_Num.E842);
                fram.SECSPara.Loadport2_AccessMode = Mode.Manual.GetHashCode();
                Common.SecsgemForm.UpdateSV(TrimGap_EqpID.Loadport2_AccessMode, (byte)fram.SECSPara.Loadport2_AccessMode, out err);
                InsertLog.SavetoDB(51, "LP2 Switch to Manual");// Manual
            }
        }

        private void list_CJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CJ_index = list_CJ.FocusedItem.Index;
            Refresh_PJ_List_Status(CJ_index + 1);

            string strCJ_Info = "";
            for (int i = 1; i < 6; i++)
            {
                strCJ_Info += "CJ-" + CJ_index + "_PJ-" + i + "\r\n";
            }
            label_CJ_Info.Text = strCJ_Info;
            label_PJ_Info.Text = "";
        }

        /// <summary> 刷新顯示頁PJ_LisetView顯示內容 </summary>
        public void Refresh_PJ_List_Status(int CJ_index = -1)
        {
            list_PJ.Clear();
            list_PJ.Columns.Add("", 0, HorizontalAlignment.Center);
            list_PJ.Columns.Add("PJ", 260, HorizontalAlignment.Center);

            list_PJ.View = View.Details;
            list_PJ.GridLines = true;
            list_PJ.FullRowSelect = true;
            list_PJ.HeaderStyle = ColumnHeaderStyle.Clickable;

            list_PJ.BeginUpdate();
            if (CJ_index > -1)
            {
                for (int i = 1; i < 6; i++)
                {

                    list_PJ.Items.Add("folder" + (i - 1), "", 0);
                    list_PJ.Items["folder" + (i - 1)].SubItems.Add("CJ-" + CJ_index + "_PJ-" + i);
                }
            }

            list_PJ.EndUpdate();

        }

        private void list_Run_CJ_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            TextFormatFlags flags = TextFormatFlags.SingleLine | TextFormatFlags.GlyphOverhangPadding | TextFormatFlags.VerticalCenter | TextFormatFlags.WordEllipsis | TextFormatFlags.HorizontalCenter;

            e.DrawBackground();
            CheckBoxRenderer.DrawCheckBox(e.Graphics, new Point(4, 5), Run_CJ_List_State);

            e.DrawText(flags);
        }

        private void list_Run_CJ_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void list_Run_CJ_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void list_Run_CJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.list_Run_CJ.SelectedItems.Count == 0)
                return;

            if (list_Run_CJ.Items[list_Run_CJ.FocusedItem.Index].Checked == true)
            {
                list_Run_CJ.Items[list_Run_CJ.FocusedItem.Index].Checked = false;
            }
            else
            {
                list_Run_CJ.Items[list_Run_CJ.FocusedItem.Index].Checked = true;
            }
        }

        /// <summary> 設置元件提示 </summary>
        public void SetToolTip()
        {
            textBox_Aligner_Presence.Text = "對準器 晶圓 存在";
            textBox_Aligner_Vacuum.Text = "對準器 真空 存在";
        }

        /// <summary> 刷新顯示頁Wafer_LisetView顯示內容 </summary>
        public void Refresh_Wafer_List_Status()
        {

            list_Wafer.Clear();
            list_Wafer.Columns.Add("", 25, HorizontalAlignment.Center);
            list_Wafer.Columns.Add("Wafer", 215, HorizontalAlignment.Center);

            list_Wafer.View = View.Details;
            list_Wafer.GridLines = true;
            list_Wafer.FullRowSelect = true;
            list_Wafer.OwnerDraw = true;
            list_Wafer.CheckBoxes = true;
            list_Wafer.HeaderStyle = ColumnHeaderStyle.Clickable;

            list_Wafer.AllowColumnReorder = true;

            list_Wafer.BeginUpdate();
            for (int i = 1; i < 26; i++)
            {
                list_Wafer.Items.Add("folder" + (i - 1), "", 0);
                list_Wafer.Items["folder" + (i - 1)].SubItems.Add("Wafer-" + i);
            }

            list_Wafer.EndUpdate();

        }

        /// <summary> 初始ComboBox為Index 0 </summary>
        private void InitComboBox()
        {
            comboBox_PRProcessStart.SelectedIndex = 0;
            comboBox_PRRecipeMethod.SelectedIndex = 0;
        }

        private void Tab_Control_Index_Changed(object sender, EventArgs e)
        {
            Refresh_Recipe_List_Status();
            //Refresh_PJ_List_Status();
            Refresh_Run_PJ_List_Status();
            Refresh_CJ_List_Status();
            Refresh_Run_CJ_List_Status();
        }

        /// <summary> 刷新顯示頁Recipe_LisetView顯示內容 </summary>
        public void Refresh_Recipe_List_Status()
        {
            MainForm = new DemoFormDiaGemLib.MainForm();
            string s = fram.Recipe.Path;

            list_Recipe.Clear();
            list_Recipe.Columns.Add("", 0, HorizontalAlignment.Center);
            list_Recipe.Columns.Add("Recipe", 260, HorizontalAlignment.Center);         

            list_Recipe.View = View.Details;
            list_Recipe.GridLines = true;
            list_Recipe.FullRowSelect = true;
            list_Recipe.HeaderStyle = ColumnHeaderStyle.Clickable;

            DirectoryInfo di = new DirectoryInfo(s + @"\\Recipe\\");
            object[] iniFiles = di.GetFiles();
            list_Recipe.BeginUpdate();

            if (iniFiles.Any())
            {
                for (int i = 0; i < iniFiles.Count(); i++)
                {
                    list_Recipe.Items.Add("folder" + (i - 1), "", 0);
                    list_Recipe.Items["folder" + (i - 1)].SubItems.Add(iniFiles[i].ToString());
                }
            }

                //MainForm.
                //ucTlhomeSECS.GetSingleton().UpdatePPIDList();
                //List<string> pp = ucTlhomeSECS.GetSingleton()._ppManager.PPIDList;

                //list_Recipe.BeginUpdate();
                //for (int i = 0; i < pp.Count; i++)
                //{
                //    list_Recipe.Items.Add("folder" + (i - 1), "", 0);
                //    list_Recipe.Items["folder" + (i - 1)].SubItems.Add(pp[i]);
                //}

                list_Recipe.EndUpdate();

        }

        private void SECProcess_FormClosed(object sender, FormClosedEventArgs e)
        {
            Flag.FormPJOpenFlag = false;
        }
    }
}
