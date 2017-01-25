using System;
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
    public partial class Register : Form
    {
        HttpClient client = new HttpClient();
        public string imgPath { get; set; } = null;
        public Register()
        {
            client.BaseAddress = new Uri("http://localhost:53098/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
            txt_passwd.PasswordChar = '*';
            txt_passwdConfirm.PasswordChar = '*';

            btn_Close.TabStop = false;
            btn_Close.FlatStyle = FlatStyle.Flat;
            btn_Close.FlatAppearance.BorderSize = 0;

            btn_ok.FlatStyle = FlatStyle.Flat;
            btn_ok.FlatAppearance.BorderSize = 0;

            btn_Cancel.FlatStyle = FlatStyle.Flat;
            btn_Cancel.FlatAppearance.BorderSize = 0;

            this.KeyPreview = true;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.RegisterMethod();
            //if (this.txt_ImgPath.Text == "".Trim()) this.imgPath = null;
            //else this.imgPath = this.txt_ImgPath.Text;    
                    
            
           /* HttpResponseMessage response = client.PostAsJsonAsync("api/USERs", usr).Result;
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Invalid data");
            }
            else this.Close();*/
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Login frm = new Login();
            this.Hide();
            frm.Closed += (s, args) => this.Close();
            frm.Show();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Register_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.RegisterMethod();

            else if (e.KeyCode == Keys.Escape)
            {
                Login frm = new Login();
                this.Hide();
                frm.Closed += (s, args) => this.Close();
                frm.Show();
            }
        }
        private void RegisterMethod()
        {
            USER usr = new USER() { Login = txt_username.Text, Nick = txt_nick.Text, Password = txt_passwd.Text, Photo = this.imgPath };

            try
            {
                if (txt_passwd.Text == txt_passwdConfirm.Text)
                {

                    HttpResponseMessage response = client.PostAsJsonAsync("api/USERs", usr).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Invalid data");
                    }
                    else
                    {
                        Login frm = new Login();
                        this.Hide();
                        frm.Closed += (s, args) => this.Close();
                        frm.Show();
                    }
                }
                else MessageBox.Show("Confirmed password didn't match.");
            }
            catch (Exception)
            {

                MessageBox.Show("Server is down");
            }
        }
    }
}
