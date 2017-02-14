using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class Request
    {
        public List<FRIEND_REQUEST> Requests = new List<FRIEND_REQUEST>();
        public List<USER> Response = new List<USER>();

        public int RequestMod()
        {
            this.GetRequests().Wait();

            Console.WriteLine(this.Requests.Count);

            List<int> rq = new List<int>();

            foreach (FRIEND_REQUEST item in this.Requests)
            {
                if(item.Accepted == null)
                    rq.Add(item.Id_Friendlist_Owner_sender);
            }

            GetUsersFromRequest ufr = new GetUsersFromRequest();
            ufr.token = Program.Token.Token;
            ufr.id_users = rq.ToArray();
            ufr.ID_User = Program.LoggedInUser.Id;

            this.PostAsync(ufr).Wait();

            int selected = 0;
            ConsoleKey key = new ConsoleKey();

            while (true)
            {
                this.RenderRequest(selected);

                key = Console.ReadKey().Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selected--;
                    if (selected < 0)
                        selected = this.Response.Count - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selected++;
                    if (selected > this.Response.Count - 1)
                        selected = 0;
                }
                else if (key == ConsoleKey.Escape)
                {
                    return 4;
                }
                else if (key == ConsoleKey.Enter)
                {
                    AcceptUpdateRequest aur = new AcceptUpdateRequest();
                    aur.bitAccept = true;
                    aur.idfriend_request = this.Requests[selected].Id;
                    aur.token = Program.Token.Token;
                    aur.ID = Program.LoggedInUser.Id;

                    this.PostRequestStatus(aur).Wait();
                    return 9;
                }
                else if (key == ConsoleKey.Delete)
                {
                    AcceptUpdateRequest aur = new AcceptUpdateRequest();
                    aur.bitAccept = false;
                    aur.idfriend_request = this.Requests[selected].Id;
                    aur.token = Program.Token.Token;
                    aur.ID = Program.LoggedInUser.Id;

                    this.PostRequestStatus(aur).Wait();
                    return 9;
                }
            }
        }

        public void RenderRequest(int selected)
        {
            Console.Clear();
            Console.SetWindowSize(45, 15);
            Console.CursorVisible = false;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("                THUNDER CHAT                 ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("                  Requests                   ");
            Console.WriteLine("                Your nick:                   ");
            Console.SetCursorPosition(27, 2);
            Console.WriteLine(Program.LoggedInUser.Nick);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();

            int step = 0;

            foreach (USER item in this.Response)
            {
                if (step == selected)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(" ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(item.Nick);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("  " + item.Nick);
                }

                Console.BackgroundColor = ConsoleColor.White;

                step++;
            }

            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Press DEL to deny request...                ");
            Console.WriteLine(" Press ENTER to accept request...            ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public async Task GetRequests()
        {
            GetTask<List<FRIEND_REQUEST>> GetRequest = new GetTask<List<FRIEND_REQUEST>>();
            this.Requests = await GetRequest.GetAsync($"api/FRIEND_REQUEST/" + Program.LoggedInUser.Id + "?token=" + Program.Token.Token);
        }

        async Task PostRequestStatus(AcceptUpdateRequest status)
        {
            GetTask<AcceptUpdateRequest> PostRequest = new GetTask<AcceptUpdateRequest>();
            PostRequest.CreateAsync($"api/FRIEND_REQUEST_ACCEPTSTATUS", status).Wait();
        }

        public async Task<Uri> PostAsync(GetUsersFromRequest ufr)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:53098/");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await Client.PostAsJsonAsync($"api/USERsrequesting", ufr);
            response.EnsureSuccessStatusCode();

            var emp2 = response.Content.ReadAsAsync<IEnumerable<USER>>().Result;
            this.Response = emp2.ToList<USER>();           

            return response.Headers.Location;
        }
    }
}
