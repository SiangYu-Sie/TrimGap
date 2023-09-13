using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrimGap
{
    public partial class TTVScanPattern : Form
    {
        private BindingSource bs;
        private BindingSource bsAngle;
        public string TTV_Recipe_Path;
        private List<Point> lsXYTable = new List<Point>();
        private List<int> lsAngleTable = new List<int>();
        public string recipeFolder = "D:\\TrimGap";
        public string TTV_Recipe_Name;
        private Bitmap bitmap = new Bitmap(401, 401, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

        public TTVScanPattern()
        {
            InitializeComponent();
            refreshTreeView();
            initList();
        }

        public TTVScanPattern(string folder, string name)
        {
            recipeFolder = folder;
            TTV_Recipe_Name = name;
            TTV_Recipe_Path = folder + "\\" + name + ".tvr";
            
            InitializeComponent();
            refreshTreeView();
            initList();
            tbRecipeInitPath.Text = folder;
            tbRecipeName.Text = name;
        }

        private void initList()
        {
            lsXYTable.Add(new Point(0, 0));
            lsAngleTable.Add(0);
            bs = new BindingSource();
            bs.DataSource = lsXYTable;
            lbPoint.DataSource = bs;
            bsAngle = new BindingSource();
            bsAngle.DataSource = lsAngleTable;
            lbAngles.DataSource = bsAngle;
            DrawPoint();
        }

        private void btnPointSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = "Save TTV Recipe";
            savefile.Filter = "Recipe File(*.tvr)|*.tvr";
            savefile.InitialDirectory = recipeFolder;
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                SaveRecipe(savefile.FileName);
                bs = new BindingSource();
                bs.DataSource = lsXYTable;
                lbPoint.DataSource = bs;
            }
            tbRecipeName.BackColor = SystemColors.Control;
            refreshTreeView();
        }

        private void btnPointLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadfile = new OpenFileDialog();
            loadfile.Title = "Select TTV Recipe";
            loadfile.Filter = "Recipe File(*.tvr)|*.tvr";
            loadfile.InitialDirectory = recipeFolder;
            if (loadfile.ShowDialog() == DialogResult.OK)
            {
                loadRecpie(loadfile.FileName.ToString());
            }
        }

        public void loadRecpie(string path)
        {
            lsXYTable = GetPointsFromRecipe(path);
            bs = new BindingSource();
            bs.DataSource = lsXYTable;
            lbPoint.DataSource = bs;
            lsAngleTable = GetAnglesFromRecipe(path);
            bsAngle = new BindingSource();
            bsAngle.DataSource = lsAngleTable;
            lbAngles.DataSource = bsAngle;
            DrawPoint();
            tbRecipeName.Text = Path.GetFileNameWithoutExtension(path);
            TTV_Recipe_Name = tbRecipeName.Text;
        }

        public List<Point> GetPointsFromRecipe(string path)
        {
            List<Point> rtn_List = new List<Point>();
            Point tmp = new Point(0, 0);
            if (!File.Exists(path))
            {
                return rtn_List;
            }

            string[] fileContents = File.ReadAllLines(path, Encoding.UTF8);
            bool bFound = false;
            foreach (string s in fileContents)
            {
                if (s.Contains("PointCount = "))
                {
                    bFound = true;
                    continue;
                }
                if (bFound)
                {
                    tmp.X = Convert.ToInt32(s.Split(',')[0]);
                    tmp.Y = Convert.ToInt32(s.Split(',')[1]);
                    rtn_List.Add(tmp);
                }
            }
            return rtn_List;
        }

        public List<int> GetAnglesFromRecipe(string path)
        {
            List<int> rtn_List = new List<int>();
            int tmp;
            if (!File.Exists(path))
            {
                return rtn_List;
            }

            string[] fileContents = File.ReadAllLines(path, Encoding.UTF8);
            bool bFound = false;
            foreach (string s in fileContents)
            {
                if (s.Contains("AngleCount = "))
                {
                    bFound = true;
                    continue;
                }
                else if (s.Contains("[Point]"))
                {
                    break;
                }
                if (bFound)
                {
                    tmp = Convert.ToInt32(s);
                    rtn_List.Add(tmp);
                }
            }
            return rtn_List;
        }

        public void SaveRecipe(string path)
        {
            using (FileStream filePermissionStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                string strW = "[Angle]\r\n" + "AngleCount = " + lsAngleTable.Count() + "\r\n";
                foreach (int a in lsAngleTable)
                {
                    strW += a.ToString() + "\r\n";
                }

                strW += "[Point]\r\n" + "PointCount = " + lsXYTable.Count() + "\r\n";
                foreach (Point p in lsXYTable)
                {
                    strW += p.X.ToString() + ',' + p.Y.ToString() + "\r\n";
                }

                byte[] buffer = Encoding.UTF8.GetBytes(strW);
                filePermissionStream.Write(buffer, 0, buffer.Length);
            }
        }

        private void btnPointAdd_Click(object sender, EventArgs e)
        {
            int x, y;
            if (Int32.TryParse(tbX.Text, out x) && Int32.TryParse(tbY.Text, out y))
            {
                if (Math.Sqrt(x^2 + y^2) > 150000 )
                {
                    MessageBox.Show("超過可量測範圍");
                    return;
                }
                Point pt = new Point(x, y);
                lsXYTable.Add(pt);
                lsXYTable.Sort(PointCompare);
                bs = new BindingSource();
                bs.DataSource = lsXYTable;
                lbPoint.DataSource = bs;
                DrawPoint();
            }
        }

        private void btnAngleAdd_Click(object sender, EventArgs e)
        {
            int a;
            if (Int32.TryParse(tbAngle.Text, out a))
            {
                if ( a<0 || a>= 360)
                {
                    MessageBox.Show("超過可量測範圍");
                    return;
                }
                lsAngleTable.Add(a);
                lsAngleTable.Sort();
                bsAngle = new BindingSource();
                bsAngle.DataSource = lsAngleTable;
                lbAngles.DataSource = bsAngle;
            }
        }

        private void btnPointDelete_Click(object sender, EventArgs e)
        {
            if (lbPoint.SelectedIndex == -1) return;
            lsXYTable.RemoveAt(lbPoint.SelectedIndex);
            bs = new BindingSource();
            bs.DataSource = lsXYTable;
            lbPoint.DataSource = bs;
            DrawPoint();
        }

        private void btnAngleDelete_Click(object sender, EventArgs e)
        {
            if (lbAngles.SelectedIndex == -1) return;
            lsAngleTable.RemoveAt(lbAngles.SelectedIndex);
            bsAngle = new BindingSource();
            bsAngle.DataSource = lsAngleTable;
            lbAngles.DataSource = bsAngle;
        }

        private void btnPointGenerate_Click(object sender, EventArgs e)
        {
            int points, pitch, points2, pitch2;
            Point pt = new Point(0, 0);
            Point pt2 = new Point(0, 0);
            if (Int32.TryParse(tbPoints.Text, out points) && Int32.TryParse(tbPitch.Text, out pitch))
            {
                if (points > 0 && pitch > 0)
                {
                    lsXYTable.Clear();
                    if (points % 2 == 1)
                    {
                        for (int i = -1 * (points - 1) / 2; i <= (points - 1) / 2; i++)
                        {
                            pt.Y = i * pitch;
                            lsXYTable.Add(pt);
                        }
                    }
                    else
                    {
                        for (int i = -1 * points / 2; i < points / 2; i++)
                        {
                            pt.Y = i * pitch + pitch / 2;
                            lsXYTable.Add(pt);
                        }
                    }
                    if (Int32.TryParse(tbPoints2.Text, out points2) && Int32.TryParse(tbPitch2.Text, out pitch2))
                    {
                        if (points2 > 0 && pitch2 > 0)
                        {
                            pt2 = new Point(0, 0);
                            for (int i = 1; i <= points2; i++)
                            {
                                pt2.Y = pt.Y + i * pitch2;
                                lsXYTable.Add(pt2);
                                pt2.Y = -1 * pt2.Y;
                                lsXYTable.Add(pt2);
                            }
                        }
                    }
                    if (pt2.Y < -150000)
                    {
                        MessageBox.Show("超過可量測範圍");
                        return;
                    }
                    lsXYTable.Sort(PointCompare);
                    bs = new BindingSource();
                    bs.DataSource = lsXYTable;
                    lbPoint.DataSource = bs;
                    lbPoint.Update();
                }
                DrawPoint();
            }
        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = recipeFolder;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                recipeFolder = folderBrowserDialog.SelectedPath;
                tbRecipeInitPath.Text = recipeFolder;
                refreshTreeView();
            }
        }

        private void refreshTreeView()
        {
            treeView_Recipe.Nodes.Clear();
            treeView_Recipe.Nodes.Add(recipeFolder);
            System.IO.FileInfo fileName;
            string filename2;
            foreach (string fname in System.IO.Directory.GetFiles(recipeFolder))
            {
                fileName = new System.IO.FileInfo(fname);//取得完整檔名(含副檔名)
                if (System.IO.Path.GetExtension(fileName.Name) == ".tvr")
                {
                    filename2 = fileName.ToString();
                    filename2 = System.IO.Path.GetFileNameWithoutExtension(filename2);
                    treeView_Recipe.Nodes[0].Nodes.Add(filename2);
                }
            }
            treeView_Recipe.ExpandAll();
        }

        private void treeView_Recipe_DoubleClick(object sender, EventArgs e)
        {
            if (treeView_Recipe.SelectedNode.Text == tbRecipeName.Text) return;
            tbRecipeName.Text = treeView_Recipe.SelectedNode.Text;
            tbRecipeName.BackColor = SystemColors.Control;
            string path = recipeFolder + "\\" + tbRecipeName.Text + ".tvr";
            if (File.Exists(path))
            {
                lsXYTable = GetPointsFromRecipe(path);
                bs = new BindingSource();
                bs.DataSource = lsXYTable;
                lbPoint.DataSource = bs;
                lsAngleTable = GetAnglesFromRecipe(path);
                bsAngle = new BindingSource();
                bsAngle.DataSource = lsAngleTable;
                lbAngles.DataSource = bsAngle;
                DrawPoint();
            }
        }

        private bool setRecipe(string path)
        {

            if (File.Exists(path))
            {
                TTV_Recipe_Path = path;
                tbRecipeName.BackColor = Color.LightGreen;
                return true;
            }
            else
            {
                tbRecipeName.BackColor = Color.Pink;
                return false;
            }
        }

        private void btnSetRecipe_Click(object sender, EventArgs e)
        {
            fram.Recipe.MotionPatternName = tbRecipeName.Text;
            TTV_Recipe_Name = tbRecipeName.Text; ;
            string path = recipeFolder + "\\" + tbRecipeName.Text + ".tvr";
            if (setRecipe(path))
            {
                lsXYTable = GetPointsFromRecipe(path);
                bs = new BindingSource();
                bs.DataSource = lsXYTable;
                lbPoint.DataSource = bs;
                lsAngleTable = GetAnglesFromRecipe(path);
                bsAngle = new BindingSource();
                bsAngle.DataSource = lsAngleTable;
                lbAngles.DataSource = bsAngle;
                DrawPoint();
            }
        }

        private void btnRecipeSave_Click(object sender, EventArgs e)
        {
            string path = recipeFolder + "\\" + tbRecipeName.Text + ".tvr";
            if (File.Exists(path))
            {
                SaveRecipe(path);
                tbRecipeName.BackColor = SystemColors.Control;
            }
        }

        public string GetTTVPointPath()
        {
            return TTV_Recipe_Path;
        }

        public void SetTTVPointPath(string path)
        {
            TTV_Recipe_Path = path;
        }

        public string GetTTVPointFolder()
        {
            return recipeFolder;
        }

        public void SetTTVPointFolder(string path)
        {
           recipeFolder = path;
        }

        private void DrawPoint()
        {
            int size = 3;
            Graphics graphics = Graphics.FromImage(bitmap);
            SolidBrush solidbush = new SolidBrush(Color.FromKnownColor(KnownColor.Gray));
            graphics.FillEllipse(solidbush, 0, 0, 400 + size, 400 + size);
            SolidBrush solidbush2 = new SolidBrush(Color.FromKnownColor(KnownColor.Blue));
            int x, y;

            foreach (Point p in lsXYTable)
            {
                x = p.Y / 750 + 200 + size / 2;
                y = p.X / 750 + 200 + size / 2;
                if (x > size / 2 - 1 && y > size / 2 - 1 && x < size / 2 + 399 && y < size / 2 + 399)
                {
                    graphics.FillRectangle(solidbush2, x - 1, y - 1, size, size);
                }
            }
            pbImage.Image = bitmap;
        }

        public static int PointCompare(Point a, Point b)
        {
            if (a.X == b.X)
            {
                return a.Y.CompareTo(b.Y);
            }
            else
            {
                return a.X.CompareTo(b.X);
            }
        }
    }
}
