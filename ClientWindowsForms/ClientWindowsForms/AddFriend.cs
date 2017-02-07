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
            btn_Close.FlatStyle = FlatStyle.Flat;
            btn_Close.FlatAppearance.BorderSize = 0;

            btn_Cancel.FlatStyle = FlatStyle.Flat;
            btn_Cancel.FlatAppearance.BorderSize = 0;
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
            if (this.textBox1.Text.Trim() == null || this.textBox1.Text.Trim() == "")
            {
                this.dataGridSearched.DataSource = null;
                this.idFriend = -1;
            }
            if (this.dataGridSearched.SelectedCells == null) this.idFriend = -1;

            responseSearched = client.GetAsync("api/USERsearch/" + textBox1.Text + "?token=" + uTok.Token + "&id=" + uTok.Id_User).Result;
            if (responseSearched.IsSuccessStatusCode)
            {
                    var emp = responseSearched.Content.ReadAsAsync<IEnumerable<USER>>().Result;
                    this.friends = emp.ToList<USER>();

                    this.dataGridSearched.DataSource = friends;

                    this.idFriend = Convert.ToInt32(dataGridSearched.Rows[0].Cells[0].Value);
                    this.dataGridSearched.Columns[0].Visible = false;
                    this.dataGridSearched.Columns[3].Visible = false;
                    this.dataGridSearched.Columns[4].Visible = false;
            }                   
            

        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (this.idFriend != -1) FriendAdd(idFriend);
        }

        private void FriendAdd(int idF)
        {
            USER_FRIENDS friend = new USER_FRIENDS() {Id_Friend = idF, Id_Friendlist_Owner = uTok.Id_User };
            responseAdd = client.PostAsJsonAsync("api/USER_FRIENDS" + "?token=" + uTok.Token, friend).Result;
            if (responseAdd.IsSuccessStatusCode) MessageBox.Show("Friend added");
            else MessageBox.Show("User is already in your friend list");

        }

        private void dataGridSearched_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = this.dataGridSearched.CurrentRow.Index;

            var idF = this.dataGridSearched.Rows[i].Cells[0].Value;

            this.idFriend = Convert.ToInt32(idF);

        }
    }
}
