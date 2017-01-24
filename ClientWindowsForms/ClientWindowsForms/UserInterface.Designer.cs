﻿namespace ClientWindowsForms
{
    partial class UserInterface
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.datagrid_Friends = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.profilePic = new System.Windows.Forms.PictureBox();
            this.btn_Friends = new System.Windows.Forms.Button();
            this.btn_Chatrooms = new System.Windows.Forms.Button();
            this.txt_MSGS = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.datagrid_Friends)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.profilePic)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label1.Location = new System.Drawing.Point(118, 9);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(107, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "NAME";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(499, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 2;
            this.button1.Text = "Options";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(593, 21);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 33);
            this.button2.TabIndex = 3;
            this.button2.Text = "Logout";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // datagrid_Friends
            // 
            this.datagrid_Friends.AllowUserToAddRows = false;
            this.datagrid_Friends.AllowUserToDeleteRows = false;
            this.datagrid_Friends.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagrid_Friends.Location = new System.Drawing.Point(13, 190);
            this.datagrid_Friends.Name = "datagrid_Friends";
            this.datagrid_Friends.ReadOnly = true;
            this.datagrid_Friends.Size = new System.Drawing.Size(142, 327);
            this.datagrid_Friends.TabIndex = 4;
            this.datagrid_Friends.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagrid_Friends_CellDoubleClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(80, 523);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 35);
            this.button3.TabIndex = 5;
            this.button3.Text = "Add Friend";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(204, 505);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(392, 62);
            this.textBox1.TabIndex = 6;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(602, 538);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(59, 29);
            this.button4.TabIndex = 7;
            this.button4.Text = "Send";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(12, 135);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(143, 22);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // profilePic
            // 
            this.profilePic.Cursor = System.Windows.Forms.Cursors.Default;
            this.profilePic.Location = new System.Drawing.Point(12, 6);
            this.profilePic.Name = "profilePic";
            this.profilePic.Size = new System.Drawing.Size(100, 109);
            this.profilePic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profilePic.TabIndex = 1;
            this.profilePic.TabStop = false;
            // 
            // btn_Friends
            // 
            this.btn_Friends.Location = new System.Drawing.Point(13, 161);
            this.btn_Friends.Name = "btn_Friends";
            this.btn_Friends.Size = new System.Drawing.Size(59, 23);
            this.btn_Friends.TabIndex = 10;
            this.btn_Friends.Text = "Friends";
            this.btn_Friends.UseVisualStyleBackColor = true;
            this.btn_Friends.Click += new System.EventHandler(this.btn_Friends_Click);
            // 
            // btn_Chatrooms
            // 
            this.btn_Chatrooms.Location = new System.Drawing.Point(89, 161);
            this.btn_Chatrooms.Name = "btn_Chatrooms";
            this.btn_Chatrooms.Size = new System.Drawing.Size(66, 23);
            this.btn_Chatrooms.TabIndex = 11;
            this.btn_Chatrooms.Text = "Chatrooms";
            this.btn_Chatrooms.UseVisualStyleBackColor = true;
            this.btn_Chatrooms.Click += new System.EventHandler(this.btn_Chatrooms_Click);
            // 
            // txt_MSGS
            // 
            this.txt_MSGS.Location = new System.Drawing.Point(204, 92);
            this.txt_MSGS.Name = "txt_MSGS";
            this.txt_MSGS.Size = new System.Drawing.Size(431, 390);
            this.txt_MSGS.TabIndex = 12;
            this.txt_MSGS.Text = "";
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 579);
            this.Controls.Add(this.txt_MSGS);
            this.Controls.Add(this.btn_Chatrooms);
            this.Controls.Add(this.btn_Friends);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.datagrid_Friends);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.profilePic);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UserInterface";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thunder Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserInterface_FormClosing);
            this.Load += new System.EventHandler(this.UserInterface_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datagrid_Friends)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.profilePic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox profilePic;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView datagrid_Friends;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btn_Friends;
        private System.Windows.Forms.Button btn_Chatrooms;
        private System.Windows.Forms.RichTextBox txt_MSGS;
    }
}