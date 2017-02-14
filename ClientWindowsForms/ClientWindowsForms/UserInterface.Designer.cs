namespace ClientWindowsForms
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInterface));
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.datagrid_Friends = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.txt_MSG_SEND = new System.Windows.Forms.TextBox();
            this.btn_Send = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.profilePic = new System.Windows.Forms.PictureBox();
            this.btn_Friends = new System.Windows.Forms.Button();
            this.btn_Chatrooms = new System.Windows.Forms.Button();
            this.txt_MSGS = new System.Windows.Forms.RichTextBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btn_AddChroom = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.datagrid_Friends)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.profilePic)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label1.Location = new System.Drawing.Point(175, 18);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(69, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "NAME";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(175, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 39);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(175, 89);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(52, 39);
            this.button2.TabIndex = 3;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // datagrid_Friends
            // 
            this.datagrid_Friends.AllowUserToAddRows = false;
            this.datagrid_Friends.AllowUserToDeleteRows = false;
            this.datagrid_Friends.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagrid_Friends.Location = new System.Drawing.Point(13, 200);
            this.datagrid_Friends.Name = "datagrid_Friends";
            this.datagrid_Friends.ReadOnly = true;
            this.datagrid_Friends.Size = new System.Drawing.Size(142, 486);
            this.datagrid_Friends.TabIndex = 4;
            this.datagrid_Friends.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagrid_Friends_CellClick);
            this.datagrid_Friends.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagrid_Friends_CellDoubleClick);
            this.datagrid_Friends.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.datagrid_Friends_CellMouseDown);
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(103, 698);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(52, 39);
            this.button3.TabIndex = 5;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txt_MSG_SEND
            // 
            this.txt_MSG_SEND.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MSG_SEND.Location = new System.Drawing.Point(189, 712);
            this.txt_MSG_SEND.Multiline = true;
            this.txt_MSG_SEND.Name = "txt_MSG_SEND";
            this.txt_MSG_SEND.Size = new System.Drawing.Size(431, 63);
            this.txt_MSG_SEND.TabIndex = 6;
            this.txt_MSG_SEND.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_MSG_SEND_KeyDown);
            // 
            // btn_Send
            // 
            this.btn_Send.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Send.BackgroundImage")));
            this.btn_Send.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Send.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Send.Location = new System.Drawing.Point(636, 726);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(52, 39);
            this.btn_Send.TabIndex = 7;
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(12, 145);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(141, 20);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // profilePic
            // 
            this.profilePic.Cursor = System.Windows.Forms.Cursors.Default;
            this.profilePic.Location = new System.Drawing.Point(69, 19);
            this.profilePic.Name = "profilePic";
            this.profilePic.Size = new System.Drawing.Size(100, 109);
            this.profilePic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profilePic.TabIndex = 1;
            this.profilePic.TabStop = false;
            // 
            // btn_Friends
            // 
            this.btn_Friends.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Friends.Location = new System.Drawing.Point(13, 171);
            this.btn_Friends.Name = "btn_Friends";
            this.btn_Friends.Size = new System.Drawing.Size(59, 23);
            this.btn_Friends.TabIndex = 10;
            this.btn_Friends.Text = "Friends";
            this.btn_Friends.UseVisualStyleBackColor = true;
            this.btn_Friends.Click += new System.EventHandler(this.btn_Friends_Click);
            // 
            // btn_Chatrooms
            // 
            this.btn_Chatrooms.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Chatrooms.Location = new System.Drawing.Point(78, 171);
            this.btn_Chatrooms.Name = "btn_Chatrooms";
            this.btn_Chatrooms.Size = new System.Drawing.Size(75, 23);
            this.btn_Chatrooms.TabIndex = 11;
            this.btn_Chatrooms.Text = "Chatrooms";
            this.btn_Chatrooms.UseVisualStyleBackColor = true;
            this.btn_Chatrooms.Click += new System.EventHandler(this.btn_Chatrooms_Click);
            // 
            // txt_MSGS
            // 
            this.txt_MSGS.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MSGS.Location = new System.Drawing.Point(189, 200);
            this.txt_MSGS.Name = "txt_MSGS";
            this.txt_MSGS.ReadOnly = true;
            this.txt_MSGS.Size = new System.Drawing.Size(431, 486);
            this.txt_MSGS.TabIndex = 12;
            this.txt_MSGS.Text = "";
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.Transparent;
            this.btn_Close.BackgroundImage = global::ClientWindowsForms.Properties.Resources.close_grey_192x192;
            this.btn_Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Close.Location = new System.Drawing.Point(658, 12);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(30, 30);
            this.btn_Close.TabIndex = 13;
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btn_AddChroom
            // 
            this.btn_AddChroom.BackColor = System.Drawing.Color.Transparent;
            this.btn_AddChroom.BackgroundImage = global::ClientWindowsForms.Properties.Resources.chat_icon__4_;
            this.btn_AddChroom.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_AddChroom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AddChroom.Location = new System.Drawing.Point(13, 698);
            this.btn_AddChroom.Name = "btn_AddChroom";
            this.btn_AddChroom.Size = new System.Drawing.Size(42, 39);
            this.btn_AddChroom.TabIndex = 14;
            this.btn_AddChroom.UseVisualStyleBackColor = false;
            this.btn_AddChroom.Click += new System.EventHandler(this.btn_AddChroom_Click);
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(700, 807);
            this.Controls.Add(this.btn_AddChroom);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.txt_MSGS);
            this.Controls.Add(this.btn_Chatrooms);
            this.Controls.Add(this.btn_Friends);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btn_Send);
            this.Controls.Add(this.txt_MSG_SEND);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.datagrid_Friends);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.profilePic);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Nirmala UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserInterface";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thunder Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserInterface_FormClosing);
            this.Load += new System.EventHandler(this.UserInterface_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserInterface_KeyDown);
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
        private System.Windows.Forms.TextBox txt_MSG_SEND;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btn_Friends;
        private System.Windows.Forms.Button btn_Chatrooms;
        private System.Windows.Forms.RichTextBox txt_MSGS;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_AddChroom;
    }
}