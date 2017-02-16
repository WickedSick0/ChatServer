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
        HttpResponseMessage responseFriends;
        private List<USER> friends;
        private USER_TOKENS uTok;
        private List<int> chroomMembersID = new List<int>();
        private string chroomName;

        public AddChatroom(USER_TOKENS tok, HttpClient clint)
        {
            InitializeComponent();

            //this.idChat = chroom.Id;
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

            if (this.txt_ChatName.Text == null || this.txt_ChatName.Text.Trim() == "")
            {
                MessageBox.Show("Enter chatroom name!");
            }

            else if (this.txt_ChatName.Enabled == false) //reset chroom name
            {
                this.txt_ChatName.Enabled = true;
                this.dataGridFriends.DataSource = null;
                chroomMembersID = new List<int>(); //reset added members
            }
            else
            {
                this.chroomName = txt_ChatName.Text;

                this.txt_ChatName.Enabled = false;
                responseFriends = client.GetAsync("api/USER_FRIENDS/" + uTok.Id_User + "?token=" + uTok.Token).Result; //get all friends
                var emp = responseFriends.Content.ReadAsAsync<IEnumerable<USER>>().Result;
                friends = emp.ToList<USER>();

                this.dataGridFriends.DataSource = friends;
                this.dataGridFriends.Columns[0].Visible = false;
                this.dataGridFriends.Columns[3].Visible = false;
                this.dataGridFriends.Columns[4].Visible = false;
            }         
        }


        private void dataGridFriends_CellDoubleClick(object sender, DataGridViewCellEventArgs e) //add friend to chatroom
        {
            int i = this.dataGridFriends.CurrentRow.Index;

            int idU = Convert.ToInt32(this.dataGridFriends.Rows[i].Cells[0].Value);

            this.chroomMembersID.Add(idU);  //add friend.id to list<int>          

            this.dataGridFriends.CurrentCell = null; //deselect current row
            this.dataGridFriends.Rows[i].Visible = false; //remove clicked friend from datagrid
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            if (this.txt_ChatName.Enabled == false)
            {
                if (chroomMembersID.Count != 0) //atleast 1 friend required
                {
                    CreateChatroom createchroom = new CreateChatroom() { ChatroomMembersID = chroomMembersID.ToArray(), chatroomName = this.chroomName, IDUser = this.uTok.Id_User, Token = this.uTok.Token};
                    client.PostAsJsonAsync("api/CHATROOMs", createchroom).Wait();

                    this.Close();
                }
                else MessageBox.Show("Add atleast one friend to the chatroom!");
            }
            else MessageBox.Show("Enter chatroom name!");
        }
    }
}
