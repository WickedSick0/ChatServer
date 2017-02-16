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
using System.IO;

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
        OpenFileDialog ofd = new OpenFileDialog();
        private int tab;
        private int idChatR = -1;       

        public UserInterface(USER_TOKENS tok, HttpClient clint)
        {
            client = clint;
            uTok = tok;
            client.DefaultRequestHeaders.Accept.Clear();

            //Get logged user
            response = client.GetAsync("api/USERs/" +  uTok.Id_User + "?token=" + uTok.Token).Result;
            usr = response.Content.ReadAsAsync<USER>().Result;

            if (!response.IsSuccessStatusCode) Close();

            this.tab = 0; //tab = friends

            InitializeComponent();

            btn_Close.TabStop = false;
            btn_Close.FlatStyle = FlatStyle.Flat;
            btn_Close.FlatAppearance.BorderSize = 0;

            btn_AddChroom.FlatStyle = FlatStyle.Flat;
            btn_AddChroom.FlatAppearance.BorderSize = 0;

            this.txt_MSGS.Font = new Font("Arial", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0, false);

            this.KeyPreview = true;

        }
        
        private void UserInterface_Load(object sender, EventArgs e)
        {
            this.profilePic.ImageLocation = "http://localhost:53098/Content/Photos/default-avatar.jpg"; //set picture
            this.label1.Text = usr.Nick;  //set name        
            GetChrooms();  //get all chatrooms
            GetFriends();  //get all friends

            this.btn_Friends.Enabled = false; //disable current tab button

            this.datagrid_Friends.Columns[0].Visible = false; //id
            this.datagrid_Friends.Columns[1].Visible = false; //username
            this.datagrid_Friends.Columns[3].Visible = false; //password
            this.datagrid_Friends.Columns[4].Visible = false; //photo

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
                    if ((r.Cells[2].Value).ToString().ToUpper().Contains(txtSearch.Text.ToUpper())) //if searched text == user.name
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
                    if ((r.Cells[1].Value).ToString().ToUpper().Contains(txtSearch.Text.ToUpper())) //if searched text == chroom.name
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
            client.DeleteAsync("api/USER_TOKENS/" + uTok.Id_User + "?token=" + uTok.Token); //delete token after closing
            this.timer1.Stop();
        }

        private void btn_Friends_Click(object sender, EventArgs e)
        {
            //enable other tab and disable current tab
            this.btn_Chatrooms.Enabled = true;
            this.btn_Friends.Enabled = false;

            this.tab = 0; //tab = friends
            this.idChatR = -1; //chatroom.id not selected
            this.datagrid_Friends.DataSource = friends;
            this.datagrid_Friends.Columns[0].Visible = false;
            this.datagrid_Friends.Columns[1].Visible = false;
            this.datagrid_Friends.Columns[3].Visible = false;
            this.datagrid_Friends.Columns[4].Visible = false;
            this.txt_MSGS.Clear(); //clear messages
        }

        private void btn_Chatrooms_Click(object sender, EventArgs e)
        {
            //enable other tab and disable current tab
            this.btn_Friends.Enabled = true;
            this.btn_Chatrooms.Enabled = false;

            this.tab = 1; //tab = chatrooms
            this.datagrid_Friends.DataSource = chatrooms;

        }

        private void datagrid_Friends_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            client.DeleteAsync("api/USER_TOKENS/" + uTok.Id_User + "?token=" + uTok.Token); //delete token
            this.timer1.Stop();
            this.Close();
        }

        private void datagrid_Friends_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.tab == 1) //click on chatroom
            {
                int i = this.datagrid_Friends.CurrentRow.Index;

                var idChat = this.datagrid_Friends.Rows[i].Cells[0].Value;

                this.idChatR = Convert.ToInt32(idChat); //get chat.id on click

                RefreshChat(this.idChatR);
            }
        } 

        private void btn_Send_Click(object sender, EventArgs e)
        {
            if(this.idChatR != -1) this.SendMessage(this.idChatR); //if chat.id selected then send
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
            StringBuilder sb = new StringBuilder();

            //Add MSGS to rich text box
            foreach (MESSAGE item in messages)
            {
                foreach (USER item2 in friends)
                {
                    if (item2.Id == item.Id_User_Post) //foreach msg get nick + text
                    {
                        sb.Append(item2.Nick + ": " + item.Message_text + "\n\r");
                        break;
                    }
                    else if (item.Id_User_Post == this.usr.Id)
                    {
                        sb.Append(this.usr.Nick + ": " + item.Message_text + "\n\r");
                        break;
                    }

                }

            }
            this.txt_MSGS.Text = sb.ToString();
            this.txt_MSGS.Select(txt_MSGS.TextLength, 0); //select end of the chat
            this.txt_MSGS.ScrollToCaret(); //scroll to the end

            //Change color of logged user
             if (txt_MSGS.Text.Contains(this.usr.Nick))
             {
                 var matchString = Regex.Escape(this.usr.Nick); //find nick
                 foreach (Match match in Regex.Matches(txt_MSGS.Text, matchString)) //foreach match set color
                 {
                     txt_MSGS.Select(match.Index, this.usr.Nick.Length);
                     txt_MSGS.SelectionColor = Color.Blue;
                 }
             }
        }//end

        private void txt_MSG_SEND_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.idChatR != -1) //if enter pressed + chat selected
            {
                this.SendMessage(this.idChatR);
                e.SuppressKeyPress = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if chat selected && tab = chat && mouse not scrolling then refresh chat
            if (this.idChatR != - 1 && this.tab == 1 && this.txt_MSGS.Capture == false) RefreshChat(this.idChatR);
        }

        private void button3_Click(object sender, EventArgs e) //add friend
        {
            AddFriend frm = new AddFriend(uTok,client);
            frm.Show();
            frm.Closed += (s, args) => GetFriends(); //refresh friends and chatrooms after AddFriend form closed
            frm.Closed += (s, args) => GetChrooms();


        }
        private void GetFriends()
        {
            responseFriends = client.GetAsync("api/USER_FRIENDS/" + uTok.Id_User + "?token=" + uTok.Token).Result;
            var emp = responseFriends.Content.ReadAsAsync<IEnumerable<USER>>().Result;
            friends = emp.ToList<USER>();

            if (tab == 0)
            {
                this.datagrid_Friends.DataSource = friends;
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
            }
        }

        private void datagrid_Friends_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && this.tab == 0) //tab = friends => delete user from friendlist
            {
                int i = datagrid_Friends.Rows[e.RowIndex].Index;

                int idFriendToDelete = Convert.ToInt32(this.datagrid_Friends.Rows[i].Cells[0].Value); //get user.id from clicked row

                string userName = this.datagrid_Friends.Rows[i].Cells[2].Value.ToString(); //get user.name

                DialogResult dialog = MessageBox.Show("Delete this user from your friendlist: " + userName + "?", "Delete user", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    client.DeleteAsync("api/USER_FRIENDS/" + uTok.Id_User + "/" + idFriendToDelete + "/" + uTok.Token).Wait();
                    GetFriends();
                }
            }
            else if (e.Button == MouseButtons.Right && this.tab == 1) //tab = chatrooms => leave chatroom
            {
                int i = datagrid_Friends.Rows[e.RowIndex].Index;

                int idchroomToLeave = Convert.ToInt32(this.datagrid_Friends.Rows[i].Cells[0].Value); //get chroom.id from clicked row

                string chroomName = this.datagrid_Friends.Rows[i].Cells[1].Value.ToString(); //chroom.name

                int[] idMemberToleave = {uTok.Id_User}; //have to create array because of the syntax on server

                DeleteAddMember leave = new DeleteAddMember() {chatroomID = idchroomToLeave, ChatroomMembersID = idMemberToleave, IDUser = uTok.Id_User, token = uTok.Token};

                DialogResult dialog = MessageBox.Show("Leave this chatroom: " + chroomName + "?", "Leave chatroom", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    client.PostAsJsonAsync("api/CHATleave", leave).Wait();
                    GetChrooms();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) //Update user
        {
            UpdateUser frm = new UpdateUser(this.uTok, this.client, usr.Nick);
            frm.Show();
            frm.Closed += (s, args) => refreshUserNick(); //refresh nick after edit
        }

        private void refreshUserNick()
        {
            response = client.GetAsync("api/USERs/" + uTok.Id_User + "?token=" + uTok.Token).Result;
            usr = response.Content.ReadAsAsync<USER>().Result;
            this.label1.Text = usr.Nick;
        }

        private void btn_Upload_Click(object sender, EventArgs e)
        {
            if(ofd.ShowDialog() == DialogResult.OK)
            {
              /*  MultipartFormDataContent form = new MultipartFormDataContent();

                form.Add(new StringContent(uTok.Token), "token");
                form.Add(new StringContent(uTok.Id_User.ToString()), "id");
                form.Add(new ByteArrayContent(File.ReadAllBytes(ofd.FileName), 0, File.ReadAllBytes(ofd.FileName).Count()));
                

                client.PostAsJsonAsync("api/Upload/", form);*/
            }
        }
    }
}
