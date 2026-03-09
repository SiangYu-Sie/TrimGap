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
    
    public partial class CJ_PJ : Form
    {
        public static DemoFormDiaGemLib.MainForm MainForm;

        List<CJ_Attr> listCJ = new List<CJ_Attr>();
        List<PJ> listPJ = new List<PJ>();
        List<Recipe> listRecipe = new List<Recipe>();
        string s_Recipe = string.Empty;
        string s_PJ = string.Empty;
        int i_Del_index;
        RecipeFormat recipe = new RecipeFormat();

        #region =================== Class =========================
        public class PJ
        {
            //public int obj_PJ_ID { get; set; }
            //public string PJ_NAME { get; set; }
            //public string ObjType { get; set; }
            //public string PauseEvent { get; set; }
            //public string RECIPE_NAME { get; set; }

            public string ObjID { get; set; }
            public string ObjType { get; set; }
            public string PauseEvent { get; set; }
            public int PRJobState { get; set; }
            public List<PRMtlName> PRMtlNameList { get; set; }
            public int PRMtlType { get; set; }
            public bool PRProcessStart { get; set; }
            public int PRRecipeMethod { get; set; }
            public string RecID { get; set; }
            public List<Recipe> RecVariableList { get; set; }

        }

        public class PRMtlName
        {
            public string FoupID { get; set; }
            public string SlotID { get; set; }
        }

        public class Recipe
        {
            public string RECIPE_NAME { get; set; }
            public List<int> Slot { get; set; }
        }

        public class CJ_Attr
        {
            public string ObjID { get; set; }
            public string ObjType { get; set; }
            public List<string> CurrentPRJob { get; set; }
            public string DataCollectionPlan { get; set; }
            public List<string> CarrierInputSpec { get; set; }
            public List<string> MtrlOutSpec { get; set; }
            public List<string> MtrlOutByStatus { get; set; }
            public List<string> PauseEvent { get; set; }
            public List<string> ProcessingCtrlSpec { get; set; }
            public int processordermgmt { get; set; }
            public bool StartMethod { get; set; }
            public string state { get; set; }
        }

        public enum ProcessOrderMgmt
        {
            LIST = 0,
            ARRIVAL = 1,
            OPTIMIZE = 2
        }

        public enum PRJobState
        {
            QUEUED = 0,
            POOLED = 1,
            SETTING_UP = 2,
            WAITING_FOR_START = 3,
            PROCESSING = 4,
            PROCESS_COMPLETED = 5,
            EXECUTING = 6,
            PAUSING = 7,
            PAUSE = 8,
            STOPPING = 9,
            ABORTING = 10,
        }
        #endregion

        public CJ_PJ()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            InitCJ();
            InitPJ();
            InitRecipe();
            InitPJ_Main();
            
        }

        private void InitCJ()
        {
            if (DemoFormDiaGemLib.MainForm.CJ_list == null)
            {
                ShowMessage("DemoFormDiaGemLib.MainForm.CJ_list 尚未初始化，CJ 資料無法載入");
                return;
            }

            for (int i = 0; i < DemoFormDiaGemLib.MainForm.CJ_list.Count; i++)
            {
                CJ_Attr cj = new CJ_Attr { ObjID = DemoFormDiaGemLib.MainForm.CJ_list[i] };
                listCJ.Add(cj);
            }
            dgv_CJ.DataSource = listCJ;
        }

        private void InitPJ()
        {
            //DataTable dt_PJ = new DataTable();
            ////dt_PJ.Columns.Add("ID", typeof(int));
            //dt_PJ.Columns.Add("PJ_Name", typeof(string));

            //dgv_PJ.DataSource = dt_PJ;

            //dgv_PJ.Columns[0].HeaderText = "PJ ID";
            //dgv_PJ.Columns[1].HeaderText = "PJ ID";

            for (int i = 0; i < DemoFormDiaGemLib.MainForm.PJ_list.Count; i++)
            {
                PJ pj = new PJ();
                //pj.obj_PJ_ID = i + 1;
                pj.ObjID = DemoFormDiaGemLib.MainForm.PJ_list[i].ToString();
                //pj.RECIPE_NAME = "";

                listPJ.Add(pj);
            }

            dgv_PJ.DataSource = listPJ;
        }

        private void InitRecipe()
        {
            string path = fram.Recipe?.Path;
            if (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path))
            {
                ShowMessage($"Recipe 路徑不存在: {path}");
                return;
            }

            foreach (string fname in Directory.GetFiles(path, "*.ini"))
            {
                var filename2 = Path.GetFileNameWithoutExtension(fname);
                listRecipe.Add(new Recipe { RECIPE_NAME = filename2 });
            }
            dgv_Recipe.DataSource = listRecipe;
        }

        private void dgv_Visable()
        {
            // 安全檢查欄位存在才設定 Visible
            if (dgv_PJ != null && dgv_PJ.Columns != null)
            {
                if (dgv_PJ.Columns.Count > 2) dgv_PJ.Columns[2].Visible = false; // or false per logic
                if (dgv_PJ.Columns.Count > 3) dgv_PJ.Columns[3].Visible = false;
                if (dgv_PJ.Columns.Count > 4) dgv_PJ.Columns[4].Visible = false;
                if (dgv_PJ.Columns.Count > 5) dgv_PJ.Columns[5].Visible = false;
                if (dgv_PJ.Columns.Count > 6) dgv_PJ.Columns[6].Visible = false;
            }

            if (dgv_PJ_Main != null && dgv_PJ_Main.Columns != null)
            {
                if (dgv_PJ_Main.Columns.Count > 2) dgv_PJ_Main.Columns[2].Visible = false;
                if (dgv_PJ_Main.Columns.Count > 3) dgv_PJ_Main.Columns[3].Visible = false;
                if (dgv_PJ_Main.Columns.Count > 4) dgv_PJ_Main.Columns[4].Visible = false;
                if (dgv_PJ_Main.Columns.Count > 5) dgv_PJ_Main.Columns[5].Visible = false;
                if (dgv_PJ_Main.Columns.Count > 6) dgv_PJ_Main.Columns[6].Visible = false;
            }
        }


        private void InitPJ_Main()
        {
            DataTable dt_PJ_Main = new DataTable();
            //dt_PJ_Main.Columns.Add("ID", typeof(int));
            dt_PJ_Main.Columns.Add("PJ_Name", typeof(string));

            dgv_PJ_Main.DataSource = dt_PJ_Main;


            //for(int i = 0; i < DemoFormDiaGemLib.MainForm.PJ_list.Count; i++)
            //{
            //    PJ pj = new PJ();
            //    pj.obj_PJ_ID = i + 1;
            //    pj.PJ_NAME = DemoFormDiaGemLib.MainForm.PJ_list[i].ToString();
            //    pj.RECIPE_NAME = "";

            //    listPJ.Add(pj);
            //}

            dgv_PJ_Main.DataSource = listPJ;
        }

        private void CJ_Add_Click(object sender, EventArgs e)
        {
            //DataRow dr = new DataRow();
            panel9.Visible = true;

        }

        private void CJ_Cancel_Click(object sender, EventArgs e)
        {
            txt_CJ_Name.Text = "";
            panel9.Visible = false;
        }

        private void CJ_Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_CJ_Name.Text))
            {
                lb_CJ_Err.Text = "CJ NAME 不可為空白!!!";
                return;
            }

            if (listCJ.Any(c => string.Equals(c.ObjID, txt_CJ_Name.Text, StringComparison.OrdinalIgnoreCase)))
            {
                ShowMessage("CJ ID 已存在，請輸入不同的 CJ 名稱");
                return;
            }

            CJ_Attr cj = new CJ_Attr();
            cj.ObjID = txt_CJ_Name.Text;
            cj.ObjType = "ControlJob";

            listCJ.Add(cj);
            dgv_CJ.DataSource = null;
            dgv_CJ.DataSource = listCJ;
        }

        private void btn_Add_PJ_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
        }

        private void btn_Cancel_PJ_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            txt_PJ.Text = "";
        }

        private void btn_Save_PJ_Click(object sender, EventArgs e)
        {
            //======= 檢查 ========
            if (string.IsNullOrWhiteSpace(txt_PJ.Text))
            {
                lb_PJ_Err.Text = "PJ Name 不可為空白!!!";
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_FOUPID.Text))
            {
                lb_PJ_Err.Text = "Foup ID 不可為空白!!!";
                return;
            }

            if (listPJ.Any(p => string.Equals(p.ObjID, txt_PJ.Text, StringComparison.OrdinalIgnoreCase)))
            {
                ShowMessage("PJ ID 已存在，請輸入不同的 PJ 名稱");
                return;
            }
            //======================


            MainForm = SecsGemInterface.MainForm;

            PJ pj = new PJ();
            pj.ObjID = txt_PJ.Text;
            pj.ObjType = "PROCESSJOB";
            pj.PRProcessStart = chkPRProcessStart.Checked ? true : false;
            pj.RecID = s_Recipe;

            List<PRMtlName> listPrm = new List<PRMtlName>();
            byte[] slot = new byte[25];

            for (int i = 0; i < recipe.Slot.Length; i++)
            {
                if (recipe.Slot[i] == 1)
                {
                    listPrm.Add(new PRMtlName
                    {
                        FoupID = txt_FOUPID.Text,
                        SlotID = (i + 1).ToString()
                    });
                }
                slot[i] = (byte)recipe.Slot[i];
            }

            pj.PRMtlNameList = listPrm;

            string err = string.Empty;

            listPJ.Add(pj);
            dgv_PJ_Main.DataSource = null;
            dgv_PJ_Main.DataSource = listPJ;

            dgv_PJ.DataSource = null;
            dgv_PJ.DataSource = listPJ;

            dgv_Visable();

            int rtn = MainForm.CreateProcessJob(pj.ObjID.ToString(), s_Recipe, pj.PRProcessStart, "1", slot, out err);

        }

        

        private void dgv_Recipe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgv_Recipe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string g = dgv_Recipe.Rows[e.RowIndex].Cells[0].Value.ToString();
                s_Recipe = g;

                string sPath = sram.dirfilepath + "\\ProcessJob\\" + g + ".ini";
                System.IO.FileInfo openName = new FileInfo(sPath);
                ParamFile.ReadRcpini(openName.FullName, "Recipe", recipe);
            }
        }

        private void btn_Delete_PJ_Click(object sender, EventArgs e)
        {
            listPJ.RemoveAt(i_Del_index);

            dgv_PJ_Main.DataSource = null;
            dgv_PJ_Main.DataSource = listPJ;
        }

        private void dgv_PJ_Main_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string PJID, pauseEvent, carrierID, recID, recVarList, err, PJStateS, slotS, prtypeS, recMethodS;
            byte PJState, PRType, recMethod;
            byte[] slot;
            bool bStart;
            string strPJ_Info = "";
            ListViewItem listItem = new ListViewItem();

            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                //string g = dgv_Recipe.Rows[e.RowIndex];
                DataGridView ls = (DataGridView)sender;
                i_Del_index = e.RowIndex;

                string objid = listPJ[i_Del_index].ObjID.ToString();
                MainForm.GetProcessJobAttr(objid, out pauseEvent, out PJState, out carrierID, out slot, out PRType, out bStart, out recMethod, out recID, out recVarList, out err);

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

                listItem.SubItems.Add("PRJobState: " + PJStateS);
                listItem.SubItems.Add("PRMtlNameList(CarrierID): " + carrierID);
                for (int i = 0; i < 25; i++)
                {
                    slotS += slot[i].ToString();
                    if (i % 5 == 4)
                        slotS += " ";
                }
                listItem.SubItems.Add("PRMtlNameList(Slot 1->25): " + slotS);
                prtypeS = PRType == 14 ? "Substrate" : "Carriers";
                listItem.SubItems.Add("PRMtlType: " + prtypeS);
                listItem.SubItems.Add("PRProcessStart: " + bStart);
                recMethodS = recMethod == 0 ? "Recipe only" : "Recipe with Variable Tuning";
                listItem.SubItems.Add("PRRecipeMethod: " + recMethodS);
                listItem.SubItems.Add("RecID: " + recID);
                listItem.SubItems.Add("RecVariableList: " + recVarList);

                listView2.Items.Add(listItem);

            }
        }

        private void dgv_PJ_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgv_PJ.Columns == null || dgv_PJ.Columns.Count <= 1) return;
            var cell = dgv_PJ.Rows[e.RowIndex].Cells[1].Value;
            s_PJ = cell?.ToString() ?? string.Empty;
        }

        private void dgv_CJ_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string carrierInputSpec = string.Empty;
            string curPJ = string.Empty;
            string dataCollection = string.Empty;
            string mtrloutStatus = string.Empty;
            string mtrloutSpec = string.Empty;
            string pauseEvent = string.Empty;
            string procCtrlSpec = string.Empty;
            byte procOrder = 0;
            bool bStart = false;
            byte state = 0;
            string err = string.Empty;

            MainForm = SecsGemInterface.MainForm;

            if (e.RowIndex != -1)
            {
                string cj_t = dgv_CJ.Rows[e.RowIndex].Cells[0].Value.ToString();
                MainForm.GetControlJobAttr(cj_t, out carrierInputSpec, out curPJ, out dataCollection, out mtrloutStatus, out mtrloutSpec, out pauseEvent, out procCtrlSpec, out procOrder, out bStart, out state, out err);

                string s = "CarrierInputSpec" + carrierInputSpec + "\n" +
                    "CurPJ：" + curPJ + "\n" +
                    "DataCollection：" + dataCollection + "\n" +
                    "mtrloutStatus：" + mtrloutStatus + "\n" +
                    "mtrloutSpec：" + mtrloutSpec + "\n" +
                    "PauseEvent：" + pauseEvent + "\n" +
                    "ProcCtrlSpec：" + procCtrlSpec + "\n";


                richTextBox1.Text = s;


                //MessageBox.Show(carrierInputSpec + "/" + curPJ + "/" + dataCollection + "/" + mtrloutStatus + "/" + mtrloutSpec + "/" + pauseEvent + "/" + procCtrlSpec + "/" + procOrder.ToString() + "/" + bStart.ToString() + "/" + state.ToString() + "/" + err);
            }

           
        }

        // 新增 ShowMessage 方法於 CJ_PJ 類別內
        private void ShowMessage(string message)
        {
            MessageBox.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
