using ClientWindowsForms.Tables;
using System;
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
    public partial class AddChatroom : Form
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage responseChRoom;
        HttpResponseMessage responseFriends;
        private List<USER> friends;
        private USER_TOKENS uTok;
        private CHATROOM chroom = new CHATROOM();
        private List<CHATROOM_MEMBERS> friendsToAdd = new List<CHATROOM_MEMBERS>();
        private int idChat;

        public AddChatroom(USER_TOKENS tok, HttpClient clint)
        {
            InitializeComponent();

            this.idChat = chroom.Id;
            this.client = clint;
            this.uTok = tok;

            client.DefaultRequestHeaders.Accept.Clear();

            btn_Close.TabStop = false;
            btn_Close.FlatStyle = FlatStyle.Flat;
            btn_Close.FlatAppearance.BorderSize = 0;

            btn_Cancel.FlatStyle = FlatStyle.Flat;
            btn_Cancel.FlatAppearance.BorderSize = 0;

            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e) //create chroom
        {

            if (this.txt_ChatName.Text.Trim() == null || this.txt_ChatName.Text.Trim() == "")
            {
                MessageBox.Show("Enter chatroom name!");
            }

            else if (this.txt_ChatName.Enabled == false)
            {
                this.txt_ChatName.Enabled = true;
                this.dataGridFriends.DataSource = null;
                friendsToAdd = new List<CHATROOM_MEMBERS>(); //reset added friends
            }
            else
            {
                this.chroom.Chatroom_Name = txt_ChatName.Text;
                //responseChRoom = client.PostAsJsonAsync("api/CHATROOMs", chroom).Result;
                //   if (responseChRoom.IsSuccessStatusCode)
                //  {
                this.txt_ChatName.Enabled = false;
                responseFriends = client.GetAsync("api/USER_FRIENDS/" + uTok.Id_User + "?token=" + uTok.Token).Result;
                var emp = responseFriends.Content.ReadAsAsync<IEnumerable<USER>>().Result;
                friends = emp.ToList<USER>();

                this.dataGridFriends.DataSource = friends;
                this.dataGridFriends.Columns[0].Visible = false;
                this.dataGridFriends.Columns[3].Visible = false;
                this.dataGridFriends.Columns[4].Visible = false;
                //    }
            }         
        }


        private void dataGridFriends_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = this.dataGridFriends.CurrentRow.Index;

            int idU = Convert.ToInt32(this.dataGridFriends.Rows[i].Cells[0].Value);

            CHATROOM_MEMBERS friendtoadd = new CHATROOM_MEMBERS() { Id_User = idU, Id_Chatroom = idChat };

            this.friendsToAdd.Add(friendtoadd);

            this.dataGridFriends.CurrentCell = null;
            this.dataGridFriends.Rows[i].Visible = false;
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            if (this.txt_ChatName.Enabled == false)
            {
                if (friendsToAdd.Count != 0) //atleast 1 friend required
                {
                    responseChRoom = client.PostAsJsonAsync("api/CHATROOMs", chroom).Result;
                    CHATROOM addedChroom = responseChRoom.Content.ReadAsAsync<CHATROOM>().Result;
                    this.idChat = addedChroom.Id; //get addedChroom ID
                    if (responseChRoom.IsSuccessStatusCode)
                    {
                        CHATROOM_MEMBERS chroomCreator = new CHATROOM_MEMBERS() { Id_User = this.uTok.Id_User, Id_Chatroom = this.idChat }; //adds creator to chroom
                        client.PostAsJsonAsync("api/CHATROOM_MEMBERS", chroomCreator);
                       // if (friendsToAdd.Count != 0)
                       // {
                            foreach (var item in friendsToAdd) //adds all selected friends to chroom
                            {
                                item.Id_Chatroom = idChat;
                                client.PostAsJsonAsync("api/CHATROOM_MEMBERS", item);
                            }
                      //  }
                    }
                    this.Close();
                }
                else MessageBox.Show("Add atleast one friend to the chatroom!");
            }
            else MessageBox.Show("Enter chatroom name!");
        }
    }
}
