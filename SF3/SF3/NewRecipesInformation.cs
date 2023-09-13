using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otsuka
{
    public partial class NewRecipesInformation : Form
    {
        public string Recipe_ID = "";
        public string Recipe_Name = "";
        public string Recipe_Memo = "";
        public bool isOK = false;
        public static List<string> Recipes_list = new List<string>();
        public NewRecipesInformation(List<string> list)
        {
            InitializeComponent();
            Recipes_list = list;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            Recipe_ID = Convert.ToString(Convert.ToUInt32(txt_RecipeID.Text));
            Recipe_Name = txt_Name.Text;
            Recipe_Memo = txt_Memo.Text;
            if(txt_RecipeID.Text == "")
            {
                MessageBox.Show("請確認Recipe_ID有沒有值");
                return;
            }
            if(txt_Name.Text == "")
            {
                MessageBox.Show("請確認Name有沒有值");
                return;
            }
            try
            {
                if (Convert.ToInt32(txt_RecipeID.Text) > 99 || Convert.ToInt32(txt_RecipeID.Text) < 0)
                {
                    MessageBox.Show("Recipe_ID的範圍為0-99");
                    return;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message + "Recipe_ID的範圍為0-99");
                return;
            }
            
            for (int i = 0; i < Recipes_list.Count; i++)
            {
                if (txt_RecipeID.Text == Recipes_list[i])
                {
                    MessageBox.Show("已經有這個ID");
                    return;
                }
            }
            isOK = true;
            this.Close();
        }
    }
}
