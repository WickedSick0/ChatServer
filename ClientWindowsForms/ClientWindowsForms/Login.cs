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
    public partial class Login : Form
    {
        HttpClient client = new HttpClient();
        public Login()
        {
            client.BaseAddress = new Uri("http://localhost:53098/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
            txt_passwd.PasswordChar = '*';
            btn_Close.TabStop = false;
            btn_Close.FlatStyle = FlatStyle.Flat;
            btn_Close.FlatAppearance.BorderSize = 0;
            this.KeyPreview = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.LoginMethod();
            /*HttpResponseMessage response = client.PostAsJsonAsync("api/USER_TOKENS", usr).Result;
            USER_TOKENS tok = response.Content.ReadAsAsync<USER_TOKENS>().Result;
            if (response.IsSuccessStatusCode)
            {
                UserInterface frm = new UserInterface(tok);
                this.Close();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Invalid Username / Password");
            }*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register frm = new Register();
            this.Hide();
            frm.Closed += (s, args) => this.Close();
            frm.Show();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.LoginMethod();

            //else if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void LoginMethod()
        {
            LoginModel usr = new LoginModel() { Username = txt_username.Text, Password = txt_passwd.Text };
            try
            {
                HttpResponseMessage response = client.PostAsJsonAsync("api/USER_TOKENS", usr).Result;
                USER_TOKENS tok = response.Content.ReadAsAsync<USER_TOKENS>().Result;
                if (response.IsSuccessStatusCode)
                {
                    UserInterface frm = new UserInterface(tok);
                    this.Hide();
                    frm.Closed += (s, args) => this.Close();
                    frm.Show();
                    // frm.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Username / Password");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Server is down");
            }
        }
    }
}
