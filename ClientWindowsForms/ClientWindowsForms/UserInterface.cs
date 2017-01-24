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
    public partial class UserInterface : Form
    {
        HttpClient client = new HttpClient();
        USER usr;
        List<USER> friends;
        HttpResponseMessage response;
        HttpResponseMessage responseFriends;
        USER_TOKENS uTok;

        public UserInterface(USER_TOKENS tok)
        {
            uTok = tok;
            client.BaseAddress = new Uri("http://localhost:53098/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            response = client.GetAsync("api/USERs/" +  uTok.Id_User + "?token=" + uTok.Token).Result;
            usr = response.Content.ReadAsAsync<USER>().Result;


            responseFriends = client.GetAsync("api/USER_FRIENDS/" + uTok.Id_User + "?token=" + uTok.Token).Result;
            var emp = responseFriends.Content.ReadAsAsync<IEnumerable<USER>>().Result;
            friends = emp.ToList<USER>();

            if (!response.IsSuccessStatusCode) Close();
            

            InitializeComponent();
           
        }
        
        private void UserInterface_Load(object sender, EventArgs e)
        {
            this.profilePic.Image = ClientWindowsForms.Properties.Resources.profilePic;
            this.label1.Text = usr.Nick;
            this.datagrid_Friends.DataSource = friends;
            this.datagrid_Friends.Columns[0].Visible = false;
            this.datagrid_Friends.Columns[1].Visible = false;
            //this.datagrid_Friends.Columns[3].Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            client.DeleteAsync("api/USER_TOKENS/" + uTok.Id);
            usr = null;
            this.Hide();
            Login frm = new Login();
            frm.Closed += (s, args) => this.Close();
            frm.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            foreach (System.Windows.Forms.DataGridViewRow r in datagrid_Friends.Rows)
            {
                if ((r.Cells[2].Value).ToString().ToUpper().Contains(txtSearch.Text.ToUpper()))
                {
                    datagrid_Friends.Rows[r.Index].Visible = true;
                }
                else
                {
                    datagrid_Friends.CurrentCell = null;
                    datagrid_Friends.Rows[r.Index].Visible = false;
                }
            }
        }

        private void UserInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.DeleteAsync("api/USER_TOKENS/" + uTok.Id);
        }
    }
}
