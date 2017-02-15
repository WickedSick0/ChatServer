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
        HttpResponseMessage respondtoRequest;
        HttpResponseMessage responseGetUsers;
        List<FRIEND_REQUEST> requests = new List<FRIEND_REQUEST>();
        List<USER> listusers = new List<USER>();
        private int idRq;
        bool accept;
        private List<int> idusers = new List<int>();
        private List<int> idrequests = new List<int>();
        private List<DateTime> daterequests = new List<DateTime>();
        List<Requestor> requestor = new List<Requestor>();
        private int index = 0;


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
            responseRequests = client.GetAsync("api/FRIEND_REQUEST/" + uTok.Id_User + "?token=" + uTok.Token).Result; //get all pending requests
            if (responseRequests.IsSuccessStatusCode)
            {
                var emp = responseRequests.Content.ReadAsAsync<IEnumerable<FRIEND_REQUEST>>().Result;
                requests = emp.ToList<FRIEND_REQUEST>();

                if (requests.Count != 0) //if minimal 1 pending request
                {
                    foreach (var item in requests)
                    {
                        this.idusers.Add(item.Id_Friendlist_Owner_sender); //get idusers from requests -> need this for getting name from requestor
                        this.idrequests.Add(item.Id); //get idrequests from requests -> for responding to request
                        this.daterequests.Add(item.Send_Time); //get date from requests -> for requestor info
                    }
                    //get user from request
                    GetUsersFromRequest usrs = new GetUsersFromRequest() { ID_User = this.uTok.Id_User, token = this.uTok.Token, id_users = this.idusers.ToArray() };
                    responseGetUsers = client.PostAsJsonAsync("api/USERsrequesting", usrs).Result;
                    if (responseGetUsers.IsSuccessStatusCode)
                    {                        
                        var emp2 = responseGetUsers.Content.ReadAsAsync<IEnumerable<USER>>().Result;
                        listusers = emp2.ToList<USER>();
                        foreach (var item in listusers) //get requestor info - idrequest, name, sendtime
                        {
                            this.requestor.Add(new Requestor { Id_Request = idrequests[index], Send_Time = daterequests[index], Requestor_name = item.Login });
                            this.index++; //go to next request (and user)
                        }

                        this.dataGridRequests.DataSource = requestor;
                        this.dataGridRequests.Columns[0].Visible = false;
                        this.dataGridRequests.Columns[1].HeaderText = "Sender";
                        this.dataGridRequests.Columns[2].HeaderText = "Time sent";

                    }

                }             

            }

        }

        private void dataGridRequests_CellDoubleClick(object sender, DataGridViewCellEventArgs e) //accept
        {
            this.accept = true;

            int i = this.dataGridRequests.CurrentRow.Index;

            var idRequestOwner = this.dataGridRequests.Rows[i].Cells[0].Value;

            this.idRq = Convert.ToInt32(idRequestOwner);
            RespondRequest(this.idRq, i, this.accept);
            

        }
        private void RespondRequest(int idReq, int index, bool accepted)
        {
            AcceptUpdateRequest request = new AcceptUpdateRequest() { ID = uTok.Id_User, idfriend_request = idReq, token = uTok.Token, bitAccept = accepted };
            respondtoRequest = client.PostAsJsonAsync("api/FRIEND_REQUEST_ACCEPTSTATUS", request).Result;
            if (respondtoRequest.IsSuccessStatusCode)
            {
                this.dataGridRequests.CurrentCell = null;
                this.dataGridRequests.Rows[index].Visible = false;
            }
            else MessageBox.Show("You responded to this request already!");
        }

        private void dataGridRequests_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) //decline
        {
            if (e.Button == MouseButtons.Right)
            {
                this.accept = false;


                int i = dataGridRequests.Rows[e.RowIndex].Index;

                var idRequestOwner = this.dataGridRequests.Rows[i].Cells[0].Value;

                this.idRq = Convert.ToInt32(idRequestOwner);

                RespondRequest(this.idRq, i, this.accept);

            }
        }
    }
}
