using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientWindowsForms
{
    public partial class UserInterface : Form
    {
        HttpClient client = new HttpClient();
        USER usr;
        private List<USER> friends;
        private List<CHATROOM> chatrooms;
        private List<MESSAGE> messages;
        HttpResponseMessage response;
        HttpResponseMessage responseFriends;
        HttpResponseMessage responseChrooms;
        HttpResponseMessage responseMsgs;
        USER_TOKENS uTok;
        private int tab;

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

            responseChrooms = client.GetAsync("api/CHATROOMs/" + uTok.Id_User + "?token=" + uTok.Token).Result;
            var emp2 = responseChrooms.Content.ReadAsAsync<IEnumerable<CHATROOM>>().Result;
            chatrooms = emp2.ToList<CHATROOM>();

            if (!response.IsSuccessStatusCode) Close();

            this.tab = 0;

            InitializeComponent();

            btn_Close.TabStop = false;
            btn_Close.FlatStyle = FlatStyle.Flat;
            btn_Close.FlatAppearance.BorderSize = 0;

        }
        
        private void UserInterface_Load(object sender, EventArgs e)
        {
            this.profilePic.Image = ClientWindowsForms.Properties.Resources.profilePic;
            this.label1.Text = usr.Nick;
            this.datagrid_Friends.DataSource = friends;
            this.datagrid_Friends.Columns[0].Visible = false;
            this.datagrid_Friends.Columns[1].Visible = false;
            this.datagrid_Friends.Columns[3].Visible = false;
            this.datagrid_Friends.Columns[4].Visible = false;

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
            if (this.tab == 0) //friends
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
            else if (this.tab == 1) //chatrooms
            {
                foreach (System.Windows.Forms.DataGridViewRow r in datagrid_Friends.Rows)
                {
                    if ((r.Cells[1].Value).ToString().ToUpper().Contains(txtSearch.Text.ToUpper()))
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
        }

        private void UserInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.DeleteAsync("api/USER_TOKENS/" + uTok.Id_User + "?token=" + uTok.Token);
        }

        private void btn_Friends_Click(object sender, EventArgs e)
        {
            this.tab = 0;
            this.datagrid_Friends.DataSource = friends;
            this.datagrid_Friends.Columns[0].Visible = false;
            this.datagrid_Friends.Columns[1].Visible = false;
            this.datagrid_Friends.Columns[3].Visible = false;
            this.datagrid_Friends.Columns[4].Visible = false;
            this.txt_MSGS.Clear();
        }

        private void btn_Chatrooms_Click(object sender, EventArgs e)
        {
            this.tab = 1;
            this.datagrid_Friends.DataSource = chatrooms;
            this.datagrid_Friends.Columns[0].Visible = false;

        }

        private void datagrid_Friends_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            client.DeleteAsync("api/USER_TOKENS/" + uTok.Id);
            this.Close();
        }

        private void datagrid_Friends_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.tab == 1) //chatroom
            {
                this.txt_MSGS.Clear();

                int i = this.datagrid_Friends.CurrentRow.Index;

                var idChat = this.datagrid_Friends.Rows[i].Cells[0].Value;


                responseMsgs = client.GetAsync("api/MESSAGEs/" + idChat + "?token=" + uTok.Token).Result;
                var emp = responseMsgs.Content.ReadAsAsync<IEnumerable<MESSAGE>>().Result;
                messages = emp.ToList<MESSAGE>();

                //Add MSGS to rich text box
                foreach (MESSAGE item in messages)
                {
                    foreach (USER item2 in friends)
                    {
                        if (item2.Id == item.Id_User_Post)
                        {
                            this.txt_MSGS.Text += item2.Nick + ": " + item.Message_text + "\n\r";
                            break;
                        }
                        else if (item.Id_User_Post == this.usr.Id)
                        {
                            this.txt_MSGS.Text += this.usr.Nick + ": " + item.Message_text + "\n\r";
                            break;
                        }

                    }

                }

                //Change color of logged user
                if (txt_MSGS.Text.Contains(this.usr.Nick))
                {
                    var matchString = Regex.Escape(this.usr.Nick);
                    foreach (Match match in Regex.Matches(txt_MSGS.Text, matchString))
                    {
                        txt_MSGS.Select(match.Index, this.usr.Nick.Length);
                        txt_MSGS.SelectionColor = Color.Blue;
                        txt_MSGS.Select(txt_MSGS.TextLength, 0);
                        txt_MSGS.SelectionColor = txt_MSGS.ForeColor;
                    }
                }
            }
        } //end
    }
}
