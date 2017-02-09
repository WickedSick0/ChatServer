﻿namespace ClientWindowsForms
{
    partial class AddFriend_Requests
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddFriend_Requests));
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Friends = new System.Windows.Forms.Button();
            this.dataGridRequests = new System.Windows.Forms.DataGridView();
            this.btn_Add = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRequests)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.BackgroundImage = global::ClientWindowsForms.Properties.Resources.close_grey_192x192;
            this.btn_Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Close.Location = new System.Drawing.Point(378, 12);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(30, 30);
            this.btn_Close.TabIndex = 0;
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.Transparent;
            this.btn_Cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.BackgroundImage")));
            this.btn_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Cancel.Location = new System.Drawing.Point(98, 435);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(63, 18);
            this.btn_Cancel.TabIndex = 1;
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Friends
            // 
            this.btn_Friends.BackColor = System.Drawing.Color.Transparent;
            this.btn_Friends.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Friends.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Friends.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Friends.ForeColor = System.Drawing.Color.Gray;
            this.btn_Friends.Location = new System.Drawing.Point(181, 76);
            this.btn_Friends.Name = "btn_Friends";
            this.btn_Friends.Size = new System.Drawing.Size(75, 23);
            this.btn_Friends.TabIndex = 2;
            this.btn_Friends.Text = "FRIENDS";
            this.btn_Friends.UseVisualStyleBackColor = false;
            this.btn_Friends.Click += new System.EventHandler(this.btn_Friends_Click);
            // 
            // dataGridRequests
            // 
            this.dataGridRequests.AllowUserToAddRows = false;
            this.dataGridRequests.AllowUserToDeleteRows = false;
            this.dataGridRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridRequests.Location = new System.Drawing.Point(98, 161);
            this.dataGridRequests.Name = "dataGridRequests";
            this.dataGridRequests.ReadOnly = true;
            this.dataGridRequests.Size = new System.Drawing.Size(240, 237);
            this.dataGridRequests.TabIndex = 3;
            // 
            // btn_Add
            // 
            this.btn_Add.BackColor = System.Drawing.Color.Transparent;
            this.btn_Add.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Add.BackgroundImage")));
            this.btn_Add.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Add.Location = new System.Drawing.Point(286, 414);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(52, 39);
            this.btn_Add.TabIndex = 4;
            this.btn_Add.UseVisualStyleBackColor = false;
            // 
            // AddFriend_Requests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(420, 516);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.dataGridRequests);
            this.Controls.Add(this.btn_Friends);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddFriend_Requests";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddFriend_Requests";
            this.Load += new System.EventHandler(this.AddFriend_Requests_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRequests)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Friends;
        private System.Windows.Forms.DataGridView dataGridRequests;
        private System.Windows.Forms.Button btn_Add;
    }
}