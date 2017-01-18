﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientWindowsForms
{
    public partial class Login : Form
    {
        HttpClient client = new HttpClient();
        public Login()
        {
            client.BaseAddress = new Uri("http://localhost:53098/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginModel usr = new LoginModel() { Username = txt_username.Text, Password = txt_passwd.Text };
            HttpResponseMessage response = client.PostAsJsonAsync("api/USER_TOKENS", usr).Result;
            USER_TOKENS tok = response.Content.ReadAsAsync<USER_TOKENS>().Result;
            if (response.IsSuccessStatusCode)
            {
                UserInterface frm = new UserInterface();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Invalid Username / Password");
            }
        }
    }
}