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
        HttpResponseMessage response;
        HttpResponseMessage responseFriends;

        public UserInterface(USER_TOKENS tok)
        {
            client.BaseAddress = new Uri("http://localhost:53098/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            response = client.GetAsync("api/USERs/" +  tok.Id_User + "?token=" + tok.Token).Result;
            usr = response.Content.ReadAsAsync<USER>().Result;
            //responseFriends = client.GetAsync("api/USER_FRIENDS/" + tok.Id_User + "?token=" + tok.Token).Result;
            //var emp = responseFriends.Content.ReadAsAsync<IEnumerable<USER>>().Result;
            //List<USER> friends = emp.ToList<MESSAGE>();
            if (!response.IsSuccessStatusCode) Close();
            

            InitializeComponent();
        }
        
        private void UserInterface_Load(object sender, EventArgs e)
        {
            this.profilePic.Image = ClientWindowsForms.Properties.Resources.profilePic;
            this.label1.Text = usr.Nick;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            usr = null;
            this.Close();
        }
    }
}
