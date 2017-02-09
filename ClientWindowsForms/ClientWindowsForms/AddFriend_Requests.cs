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
    public partial class AddFriend_Requests : Form
    {

        HttpClient client = new HttpClient();
        private USER_TOKENS uTok;
        HttpResponseMessage responseRequests;
        List<FRIEND_REQUEST> requests = new List<FRIEND_REQUEST>();
        private int idRq;
        //List<Requestor> requestor = new List<Requestor>();


        public AddFriend_Requests(USER_TOKENS tok, HttpClient clint)
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

            btn_Friends.FlatStyle = FlatStyle.Flat;
            btn_Friends.FlatAppearance.BorderSize = 0;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Friends_Click(object sender, EventArgs e)
        {
            AddFriend frm = new AddFriend(uTok, client);
            this.Hide();
            frm.Closed += (s, args) => this.Close();
            frm.Show();
        }

        private void AddFriend_Requests_Load(object sender, EventArgs e)
        {
            responseRequests = client.GetAsync("api/FRIEND_REQUEST/" + uTok.Id_User + "?token=" + uTok.Token).Result;
            if (responseRequests.IsSuccessStatusCode)
            {
                var emp = responseRequests.Content.ReadAsAsync<IEnumerable<FRIEND_REQUEST>>().Result;
                requests = emp.ToList<FRIEND_REQUEST>();
                this.dataGridRequests.DataSource = requests;
                this.dataGridRequests.Columns[0].Visible = false;
                this.dataGridRequests.Columns[2].Visible = false;
                this.dataGridRequests.Columns[4].Visible = false;               

            }

        }

        private void dataGridRequests_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = this.dataGridRequests.CurrentRow.Index;

            var idRequestOwner = this.dataGridRequests.Rows[i].Cells[0].Value;

            //var idRequest = this.dataGridRequests.Rows[i].Cells[0].Value

            this.idRq = Convert.ToInt32(idRequestOwner);
            AcceptUpdateRequest request = new AcceptUpdateRequest() { ID = uTok.Id_User, idfriend_request = this.idRq, token = uTok.Token, bitAccept = true };
            client.PostAsJsonAsync("api/FRIEND_REQUEST_ACCEPTSTATUS", request);
        }
    }
}
