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
using ClientWindowsForms.Tables;

namespace ClientWindowsForms
{
    public partial class UserInterface : Form
    {
        HttpClient client = new HttpClient();
        private USER usr;
        private List<USER> friends;
        private List<CHATROOM> chatrooms;
        private List<MESSAGE> messages;
        private HttpResponseMessage response;
        private HttpResponseMessage responseFriends;
        private HttpResponseMessage responseChrooms;
        private HttpResponseMessage responseMsgs;
        private HttpResponseMessage responseMSG_SEND;
        private USER_TOKENS uTok;
        private int tab;
        private int idChatR = -1;       

        public UserInterface(USER_TOKENS tok, HttpClient clint)
        {
            client = clint;
            uTok = tok;
            //client.BaseAddress = new Uri("http://localhost:53098/");
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Get logged user
            response = client.GetAsync("api/USERs/" +  uTok.Id_User + "?token=" + uTok.Token).Result;
            usr = response.Content.ReadAsAsync<USER>().Result;

            //Get his friends
            //responseFriends = client.GetAsync("api/USER_FRIENDS/" + uTok.Id_User + "?token=" + uTok.Token).Result;
            //var emp = responseFriends.Content.ReadAsAsync<IEnumerable<USER>>().Result;
            //friends = emp.ToList<USER>();

            //Get his chrooms
            //responseChrooms = client.GetAsync("api/CHATROOMs/" + uTok.Id_User + "?token=" + uTok.Token).Result;
            //var emp2 = responseChrooms.Content.ReadAsAsync<IEnumerable<CHATROOM>>().Result;
            //chatrooms = emp2.ToList<CHATROOM>();

            if (!response.IsSuccessStatusCode) Close();

            this.tab = 0;

            InitializeComponent();

            btn_Close.TabStop = false;
            btn_Close.FlatStyle = FlatStyle.Flat;
            btn_Close.FlatAppearance.BorderSize = 0;

            btn_AddChroom.FlatStyle = FlatStyle.Flat;
            btn_AddChroom.FlatAppearance.BorderSize = 0;

            this.KeyPreview = true;

        }
        
        private void UserInterface_Load(object sender, EventArgs e)
        {
            this.profilePic.Image = ClientWindowsForms.Properties.Resources.profilePic;
            this.label1.Text = usr.Nick;            
            GetChrooms();
            GetFriends();
            //this.datagrid_Friends.DataSource = friends;
            //this.datagrid_Friends.Columns[0].Visible = false;
            //this.datagrid_Friends.Columns[1].Visible = false;
            //this.datagrid_Friends.Columns[3].Visible = false;
            //this.datagrid_Friends.Columns[4].Visible = false;

            //refresh every 10 sec
            timer1.Interval = (10000);
            timer1.Tick += timer1_Tick;
            timer1.Start();

        }

        private void button2_Click(object sender, EventArgs e) //logout
        {
            client.DeleteAsync("api/USER_TOKENS/" + uTok.Id_User + "?token=" + uTok.Token); //delete token
            usr = null;
            this.timer1.Stop();
            this.Hide();
            Login frm = new Login();
            frm.Closed += (s, args) => this.Close();
            frm.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e) //search
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
            this.timer1.Stop();
        }

        private void btn_Friends_Click(object sender, EventArgs e)
        {
            this.tab = 0;
            this.idChatR = -1;
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
            client.DeleteAsync("api/USER_TOKENS/" + uTok.Id_User + "?token=" + uTok.Token);
            this.timer1.Stop();
            this.Close();
        }

        private void datagrid_Friends_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.tab == 1) //chatroom
            {
                int i = this.datagrid_Friends.CurrentRow.Index;

                var idChat = this.datagrid_Friends.Rows[i].Cells[0].Value;

                this.idChatR = Convert.ToInt32(idChat);

                RefreshChat(this.idChatR);
            }
        } 

        private void btn_Send_Click(object sender, EventArgs e)
        {
            if(this.idChatR != -1) this.SendMessage(this.idChatR);
        }

        private void SendMessage(int idChatRoom)
        {            
            MessageAutorization msg = new MessageAutorization() { Id_Chatroom = idChatRoom, Id_User_Post = this.uTok.Id_User, Message_text = this.txt_MSG_SEND.Text, token = uTok.Token };

            responseMSG_SEND = client.PostAsJsonAsync("/api/MESSAGEs/", msg).Result;

            this.txt_MSG_SEND.Text = null;

            RefreshChat(idChatRoom);


        }

        private void UserInterface_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter && this.idChatR != -1) this.SendMessage(this.idChatR); 
        }

        private void RefreshChat(int idChat)
        {
            this.txt_MSGS.Clear();                      

            responseMsgs = client.GetAsync("api/MESSAGEs/" + idChat + "?token=" + uTok.Token).Result;
            var emp = responseMsgs.Content.ReadAsAsync<IEnumerable<MESSAGE>>().Result;
            messages = emp.ToList<MESSAGE>();

            //Add MSGS to rich text box
            foreach (MESSAGE item in messages)
            {
                foreach (USER item2 in friends)
                {
                    if (item2.Id == item.Id_User_Post) //foreach msg get nick + text
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
            this.txt_MSGS.ScrollToCaret(); //scroll to end
        }//end

        private void txt_MSG_SEND_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.idChatR != -1)
            {
                this.SendMessage(this.idChatR);
                e.SuppressKeyPress = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           if(this.idChatR != - 1 && this.tab == 1 && this.txt_MSGS.Capture == false) RefreshChat(this.idChatR);
        }

        private void button3_Click(object sender, EventArgs e) //add friend
        {
            AddFriend frm = new AddFriend(uTok,client);
            frm.Show();
            frm.Closed += (s, args) => GetFriends(); //refresh friends after AddFriend form closed


        }
        private void GetFriends()
        {
            responseFriends = client.GetAsync("api/USER_FRIENDS/" + uTok.Id_User + "?token=" + uTok.Token).Result;
            var emp = responseFriends.Content.ReadAsAsync<IEnumerable<USER>>().Result;
            friends = emp.ToList<USER>();

            if (tab == 0)
            {
                this.datagrid_Friends.DataSource = friends;
                this.datagrid_Friends.Columns[0].Visible = false;
                this.datagrid_Friends.Columns[1].Visible = false;
                this.datagrid_Friends.Columns[3].Visible = false;
                this.datagrid_Friends.Columns[4].Visible = false;
            }
        }       

        private void btn_AddChroom_Click(object sender, EventArgs e)
        {
            AddChatroom frm = new AddChatroom(uTok, client);
            frm.Show();
            frm.Closed += (s, args) => GetChrooms(); //refresh chatrooms
        }

        private void GetChrooms()
        {
            responseChrooms = client.GetAsync("api/CHATROOMs/" + uTok.Id_User + "?token=" + uTok.Token).Result;
            var emp = responseChrooms.Content.ReadAsAsync<IEnumerable<CHATROOM>>().Result;
            chatrooms = emp.ToList<CHATROOM>();

            if (tab == 1)
            {
                this.datagrid_Friends.DataSource = chatrooms;
                this.datagrid_Friends.Columns[0].Visible = false;
            }
        }
    }
}
