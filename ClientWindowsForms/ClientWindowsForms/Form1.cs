using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientWindowsForms
{
    public partial class Form1 : Form
    {
        HttpClient client = new HttpClient();
        public Form1()
        {
            client.BaseAddress = new Uri("http://localhost:53098/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            /*client.DefaultRequestHeaders.Authorization =
           new AuthenticationHeaderValue(
                "Basic", 
                 Convert.ToBase64String(
                     System.Text.ASCIIEncoding.ASCII.GetBytes(
                 string.Format("{0}:{1}", "admin", "admin"))));*/
           // HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:53098/token");
          //  request.Content = new StringContent("{\"UserName\":\"admin\",\"PassWord\":admin}");
          //  client.SendAsync(request);
            InitializeComponent();
        }

        private void btn_send_Click(object sender, EventArgs e)
        {

               MESSAGE msg = new MESSAGE() { Id_Chatroom = 1, Id_User_Post = 1, Message_text = this.msg.Text, Send_time = DateTime.Now};
            /*   HttpClient clint = new HttpClient();
               clint.BaseAddress = new Uri("http://localhost:53098/");
               var myContent = JsonConvert.SerializeObject(msg);
               var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
               var byteContent = new ByteArrayContent(buffer);
               byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");*/


            HttpResponseMessage response = client.PostAsJsonAsync("/api/MESSAGEs/", msg).Result;

                 response.EnsureSuccessStatusCode();


        }
    

        private void btn_get_Click(object sender, EventArgs e)
        {
            /*var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "grant_type", "password" ),
                        new KeyValuePair<string, string>( "UserName", "admin" ),
                        new KeyValuePair<string, string> ( "Password", "admin" )
                    };
            var content = new FormUrlEncodedContent(pairs);

             var token = client.PostAsync("http://localhost:53098/token", content);*/




            HttpResponseMessage response = client.GetAsync("/api/MESSAGEs/").Result;
            //Client<MESSAGE> clnt = new Client<MESSAGE>();
            //var emp = clnt.GetAsync("/api/MESSAGEs/1");
            var emp = response.Content.ReadAsAsync<IEnumerable<MESSAGE>>().Result;
            List<MESSAGE> msgs = emp.ToList<MESSAGE>();
            this.msg.Text = msgs[1].Message_text;
            this.dataGridView1.DataSource = emp;
            
        }

        /*   ?   */
        /*
        static string GetToken(string url, string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "grant_type", "password" ),
                        new KeyValuePair<string, string>( "username", userName ),
                        new KeyValuePair<string, string> ( "Password", password )
                    };
            var content = new FormUrlEncodedContent(pairs);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53098/");
                var response = client.PostAsync(url + "Token", content).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
        static string CallApi(string url, string token)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53098/");
                if (!string.IsNullOrWhiteSpace(token))
                {
                    var t = JsonConvert.DeserializeObject<Token>(token);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + t.access_token);
                }
                var response = client.GetAsync(url).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
        */
        /*   ?   */
    }
}
