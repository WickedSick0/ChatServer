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
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (this.txt_ImgPath.Text == "".Trim()) this.imgPath = null;
            else this.imgPath = this.txt_ImgPath.Text;    
                    
            USER usr = new USER() { Login = txt_username.Text, Nick = txt_nick.Text, Password = txt_passwd.Text, Photo = this.imgPath };

            HttpResponseMessage response = client.PostAsJsonAsync("api/USERs", usr).Result;
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Invalid data");
            }
            else this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
