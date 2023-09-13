using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrimGap
{
    partial class UserPermission
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
            this.cboUserCount = new System.Windows.Forms.ComboBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnChange = new System.Windows.Forms.Button();
            this.txtPasswordChange = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(39, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "使用者";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(39, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 35);
            this.label2.TabIndex = 1;
            this.label2.Text = "使用者密碼";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboUserCount
            // 
            this.cboUserCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUserCount.Font = new System.Drawing.Font("新細明體", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cboUserCount.FormattingEnabled = true;
            this.cboUserCount.ItemHeight = 27;
            this.cboUserCount.Location = new System.Drawing.Point(267, 35);
            this.cboUserCount.Name = "cboUserCount";
            this.cboUserCount.Size = new System.Drawing.Size(140, 35);
            this.cboUserCount.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtPassword.Location = new System.Drawing.Point(267, 100);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(140, 36);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // btnChange
            // 
            this.btnChange.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnChange.Location = new System.Drawing.Point(267, 236);
            this.btnChange.Margin = new System.Windows.Forms.Padding(2);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(140, 50);
            this.btnChange.TabIndex = 13;
            this.btnChange.Text = "登入確認";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // txtPasswordChange
            // 
            this.txtPasswordChange.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtPasswordChange.Location = new System.Drawing.Point(267, 162);
            this.txtPasswordChange.Name = "txtPasswordChange";
            this.txtPasswordChange.PasswordChar = '*';
            this.txtPasswordChange.Size = new System.Drawing.Size(140, 36);
            this.txtPasswordChange.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(39, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 35);
            this.label3.TabIndex = 14;
            this.label3.Text = "密碼修改";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnChangePassword.Location = new System.Drawing.Point(39, 236);
            this.btnChangePassword.Margin = new System.Windows.Forms.Padding(2);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(180, 50);
            this.btnChangePassword.TabIndex = 16;
            this.btnChangePassword.Text = "修改密碼";
            this.btnChangePassword.UseVisualStyleBackColor = true;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // UserPermission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 325);
            this.Controls.Add(this.btnChangePassword);
            this.Controls.Add(this.txtPasswordChange);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.cboUserCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UserPermission";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.UserPermission_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboUserCount;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.TextBox txtPasswordChange;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnChangePassword;
    }
}