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
    public partial class DeleteRecipes : Form
    {
        public bool isOK = false;
        public DeleteRecipes(string ID, string Name, string Memo)
        {
            InitializeComponent();
            txt_RecipeID.Text = ID;
            txt_Name.Text = Name;
            txt_Memo.Text = Memo;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            isOK= true;
            this.Close();
        }
    }
}
