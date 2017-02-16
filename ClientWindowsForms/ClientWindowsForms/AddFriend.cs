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
using ClientWindowsForms.Tables;

namespace ClientWindowsForms
{
    public partial class AddFriend : Form
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage responseSearched;
        HttpResponseMessage responseAdd;
        private List<USER> friends;
        private USER_TOKENS uTok;
        private int idFriend = -1;

        public AddFriend(USER_TOKENS tok, HttpClient clint)
        {         
            InitializeComponent();

            uTok = tok;
            client = clint;

            client.DefaultRequestHeaders.Accept.Clear();

            btn_Close.TabStop = false;

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox1.Text == null || this.textBox1.Text.Trim() == "")
            {
                this.dataGridSearched.DataSource = null;
                this.idFriend = -1; //user not selected
            }
            if (this.dataGridSearched.SelectedCells == null) this.idFriend = -1; //user not selected

            responseSearched = client.GetAsync("api/USERsearch/" + textBox1.Text + "?token=" + uTok.Token + "&id=" + uTok.Id_User).Result; //find user
            if (responseSearched.IsSuccessStatusCode)
            {
                    var emp = responseSearched.Content.ReadAsAsync<IEnumerable<USER>>().Result;
                    this.friends = emp.ToList<USER>(); //searched users to list USER

                    this.dataGridSearched.DataSource = friends; //get searched users to datagrid

                    if (dataGridSearched.RowCount > 0) this.idFriend = Convert.ToInt32(dataGridSearched.Rows[0].Cells[0].Value); //selects first row ID
                    else this.idFriend = -1; //user not found
                    this.dataGridSearched.Columns[0].Visible = false;
                    this.dataGridSearched.Columns[3].Visible = false;
                    this.dataGridSearched.Columns[4].Visible = false;
            }                   
            

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (this.idFriend != -1) FriendAdd(idFriend);
            else MessageBox.Show("Select a friend to add!");
        }

        private void FriendAdd(int idF)
        {
            PostRequest friend = new PostRequest() { Id_Friendlist_Owner_sender = uTok.Id_User, Id_Friend_receiver = idF, token = uTok.Token };            
            responseAdd = client.PostAsJsonAsync("api/FRIEND_REQUEST", friend).Result;

            if (responseAdd.IsSuccessStatusCode) MessageBox.Show("Request sent!");
            else MessageBox.Show("Request already sent!");

        }

        private void dataGridSearched_CellClick(object sender, DataGridViewCellEventArgs e) //on cell click get user id
        {
            int i = this.dataGridSearched.CurrentRow.Index;

            var idF = this.dataGridSearched.Rows[i].Cells[0].Value;

            this.idFriend = Convert.ToInt32(idF);

        }

        private void btn_request_Click(object sender, EventArgs e) //go to requests
        {
            AddFriend_Requests frm = new AddFriend_Requests(uTok, client);
            this.Hide();
            frm.Closed += (s, args) => this.Close();
            frm.Show();
        }
    }
}
