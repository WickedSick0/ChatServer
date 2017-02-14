﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientWindowsForms
{
    public partial class UpdateUser : Form
    {
        HttpClient client = new HttpClient();
        private USER_TOKENS uTok;
        HttpResponseMessage responseUpdate;

        public UpdateUser(USER_TOKENS tok, HttpClient clint, string nick)
        {
            this.client = clint;
            this.uTok = tok;

            client.DefaultRequestHeaders.Accept.Clear();

            InitializeComponent();

            this.txt_Nick.Text = nick;

            txt_OldPass.PasswordChar = '*';
            txt_PassConf.PasswordChar = '*';
            txt_Pass.PasswordChar = '*';

            btn_Close.TabStop = false;
            btn_Close.FlatStyle = FlatStyle.Flat;
            btn_Close.FlatAppearance.BorderSize = 0;


        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            if (this.txt_PassConf.Text == this.txt_Pass.Text)
            {
                USER updateUser = new USER() {Id = uTok.Id_User, Nick = this.txt_Nick.Text, Password = this.txt_PassConf.Text };
                responseUpdate = client.PostAsJsonAsync("api/USERsupdate" + "?token=" + uTok.Token + "&password=" + this.txt_OldPass.Text, updateUser).Result;
                if (responseUpdate.IsSuccessStatusCode)
                {
                    MessageBox.Show("Succesfully updated!");
                    this.Close();
                }
                else MessageBox.Show("Invalid data!");
                
                
            }
            else MessageBox.Show("Confirmed password didn't match!");
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}