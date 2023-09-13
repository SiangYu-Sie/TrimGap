namespace Otsuka
{
    partial class NewRecipesInformation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_RecipeID = new System.Windows.Forms.TextBox();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.txt_Memo = new System.Windows.Forms.TextBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(95, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Recipe ID：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(146, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(139, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "Memo：";
            this.label3.Visible = false;
            // 
            // txt_RecipeID
            // 
            this.txt_RecipeID.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txt_RecipeID.Location = new System.Drawing.Point(260, 50);
            this.txt_RecipeID.Name = "txt_RecipeID";
            this.txt_RecipeID.Size = new System.Drawing.Size(173, 46);
            this.txt_RecipeID.TabIndex = 3;
            // 
            // txt_Name
            // 
            this.txt_Name.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txt_Name.Location = new System.Drawing.Point(260, 100);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(173, 46);
            this.txt_Name.TabIndex = 4;
            // 
            // txt_Memo
            // 
            this.txt_Memo.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txt_Memo.Location = new System.Drawing.Point(260, 150);
            this.txt_Memo.Name = "txt_Memo";
            this.txt_Memo.Size = new System.Drawing.Size(173, 46);
            this.txt_Memo.TabIndex = 5;
            this.txt_Memo.Visible = false;
            // 
            // btn_OK
            // 
            this.btn_OK.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_OK.Location = new System.Drawing.Point(110, 214);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(134, 61);
            this.btn_OK.TabIndex = 6;
            this.btn_OK.Text = "確定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Close.Location = new System.Drawing.Point(260, 214);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(134, 61);
            this.btn_Close.TabIndex = 7;
            this.btn_Close.Text = "取消";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // NewRecipesInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 328);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.txt_Memo);
            this.Controls.Add(this.txt_Name);
            this.Controls.Add(this.txt_RecipeID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "NewRecipesInformation";
            this.Text = "NewRecipesInformation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_RecipeID;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.TextBox txt_Memo;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Close;
    }
}