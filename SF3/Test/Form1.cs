using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        Otsuka.SF3 sf3 = new Otsuka.SF3();
        public List<string[]> a = new List<string[]>();
        public Form1()
        {
            InitializeComponent();
            sf3.Open("192.168.1.20", 65432, false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a = sf3.list_RecipesIDName;
            sf3.ShowUI();
        }
    }
}
