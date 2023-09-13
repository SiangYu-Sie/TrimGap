namespace Otsuka
{
    partial class DeleteRecipes
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
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.txt_Memo = new System.Windows.Forms.TextBox();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.txt_RecipeID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Close.Location = new System.Drawing.Point(260, 234);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(134, 61);
            this.btn_Close.TabIndex = 15;
            this.btn_Close.Text = "取消";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_OK.Location = new System.Drawing.Point(110, 234);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(134, 61);
            this.btn_OK.TabIndex = 14;
            this.btn_OK.Text = "確定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // txt_Memo
            // 
            this.txt_Memo.Enabled = false;
            this.txt_Memo.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txt_Memo.Location = new System.Drawing.Point(260, 130);
            this.txt_Memo.Name = "txt_Memo";
            this.txt_Memo.Size = new System.Drawing.Size(173, 46);
            this.txt_Memo.TabIndex = 13;
            this.txt_Memo.Visible = false;
            // 
            // txt_Name
            // 
            this.txt_Name.Enabled = false;
            this.txt_Name.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txt_Name.Location = new System.Drawing.Point(260, 80);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(173, 46);
            this.txt_Name.TabIndex = 12;
            // 
            // txt_RecipeID
            // 
            this.txt_RecipeID.Enabled = false;
            this.txt_RecipeID.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txt_RecipeID.Location = new System.Drawing.Point(260, 30);
            this.txt_RecipeID.Name = "txt_RecipeID";
            this.txt_RecipeID.Size = new System.Drawing.Size(173, 46);
            this.txt_RecipeID.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(139, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 32);
            this.label3.TabIndex = 10;
            this.label3.Text = "Memo：";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(146, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 32);
            this.label2.TabIndex = 9;
            this.label2.Text = "Name：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(95, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 32);
            this.label1.TabIndex = 8;
            this.label1.Text = "Recipe ID：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(78, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(384, 32);
            this.label4.TabIndex = 16;
            this.label4.Text = "請問是否要刪除這個Recipe";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DeleteRecipes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 328);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.txt_Memo);
            this.Controls.Add(this.txt_Name);
            this.Controls.Add(this.txt_RecipeID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DeleteRecipes";
            this.Text = "DeleteRecipes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.TextBox txt_Memo;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.TextBox txt_RecipeID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
    }
}