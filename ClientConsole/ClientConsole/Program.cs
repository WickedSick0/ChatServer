using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class Program
    {
        static HttpClient client = new HttpClient();

        private static int Mod = 0; // Urcuje Mod
        static ConsoleKey Tlacitko = ConsoleKey.F1; // Ukladani zmacknutych tlacitek do Tlacitko
        //static bool IsLoginbValid = false;
        public static USER LoggedInUser = new USER();
        //static List<USER> UserFriends = new List<USER>();
        public static USER Friend = new USER();
        public static List<CHATROOM> Chatrooms = new List<CHATROOM>();
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;

            while (Mod != -1) // Pokud se Mod = -1 program se ukonci
            {
                if (Mod == 0) // Skoci do MenuMod 0
                    Mod = Menu.MenuMod(); // Mod = MenuMod()
                else if (Mod == 1) // Skoci do PlayMod 1
                    Mod = LogIn.LogInMod(); // Mod = PlayMod()
                else if (Mod == 2) // Skoci do OptionMod 2
                    Mod = Register.RegisterMod(); // Mod = OptionsMod()
                else if (Mod == 3) // Exit 3
                    Mod = -1;
                else if (Mod == 4) // Skoci do OptionMod 2
                    Mod = UserMenu.UserMenuMod(); // Mod = OptionsMod()
                else if (Mod == 5) // Skoci do OptionMod 2
                    Mod = Contact.ContactMod(); // Mod = OptionsMod()
                else if (Mod == 7) // Skoci do OptionMod 2
                    Mod = ChatroomMod(); // Mod = OptionsMod()
                else if (Mod == 10) // Skoci do OptionMod 2
                    Mod = MessageMod(); // Mod = OptionsMod()
            } // Konec programu   

            //RunAsync().Wait();

            //Console.ReadLine();
        }

        static int ChatroomMod()
        {
            Console.Clear(); // Vymaze konzoli
            Console.SetWindowSize(45, 15); // Nastavi rozmery konzole (41 + 3, 21 - 6)
            Console.CursorVisible = false; // Kurzor neni videt
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            //VykresleniNazvu(); // Vykresli Nadpis

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("             CHATROOMS               "); // Vypise HighScore fialove
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Your friend:   " + Friend.Nick + "   ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();

            //Console.WriteLine("Id: " + Friend.Id);
            //Console.WriteLine("Login: " + Friend.Login);
            //Console.WriteLine("Nick: " + Friend.Nick);
            //Console.WriteLine("Password: " + Friend.Password);
            //Console.WriteLine("Photo: " + Friend.Photo);

            GetChatrooms().Wait();

            foreach (CHATROOM item in Chatrooms)
            { 
                Console.WriteLine("Chatroom_Name: " + item.Chatroom_Name);
            }

            Console.ReadLine();

            return 0;
        }
        static async Task GetChatrooms()
        {
            GetTask<List<CHATROOM>> GetChatroomsByUser = new GetTask<List<CHATROOM>>();
            Chatrooms = await GetChatroomsByUser.GetAsync($"api/CHATROOM_MEMBERS/" + Program.LoggedInUser.Id);
        }

        static int MessageMod()
        {
            Console.Clear(); // Vymaze konzoli
            Console.SetWindowSize(45, 15); // Nastavi rozmery konzole (41 + 3, 21 - 6)
            Console.CursorVisible = false; // Kurzor neni videt
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            //VykresleniNazvu(); // Vykresli Nadpis

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("             MESSAGE               "); // Vypise HighScore fialove
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Your friend:   " + Friend.Nick + "   ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();

            Console.WriteLine("Id: " + Friend.Id);
            Console.WriteLine("Login: " + Friend.Login);
            Console.WriteLine("Nick: " + Friend.Nick);
            Console.WriteLine("Password: " + Friend.Password);
            Console.WriteLine("Photo: " + Friend.Photo);

            Console.ReadLine();

            return 0;
        }


        static async Task RunAsync()
        {

            // Create a new user
            //USER user = new USER() { Nick = "test" };

            //var url = await CreateUserAsync(user);
            //Console.WriteLine($"Created at {url}");


            // Get the user
            GetTask<USER> GetUser = new GetTask<USER>();
            USER user = await GetUser.GetAsync($"api/USERs/1");
            Console.WriteLine("Id: " + user.Id);
            Console.WriteLine("Login: " + user.Login);
            Console.WriteLine("Nick: " + user.Nick);
            Console.WriteLine("Password: " + user.Password);
            Console.WriteLine("Photo: " + user.Photo);

            Console.WriteLine();

            // Get users
            GetTask<List<USER>> GetUsers = new GetTask<List<USER>>();
            foreach (USER item in await GetUsers.GetAsync($"api/USERs/"))
            {
                Console.WriteLine("Id: " + item.Id + ", Login: " + item.Login + ", Nick: " + item.Nick + ", Password: " + item.Password + ", Photo: " + item.Photo);
            }

            /*
            // Update the user
            Console.WriteLine("Updating price...");
            user.Nick = "tttttt";
            await UpdateuserAsync(user);

            // Get the updated user
            user = await GetUserAsync(url.PathAndQuery);
            Showuser(user);

            // Delete the user
            var statusCode = await DeleteUserAsync(user.Id.ToString());
            Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
            */
        }

        //GET
        static async Task<USER> GetUserAsync2(string path)
        {
            USER user = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<USER>();
            }
            return user;
        }

        //GET ALL
        static async Task<List<USER>> GetUsersAsync2(string path)
        {
            List<USER> users = null;
            HttpResponseMessage response = await client.GetAsync($"api/USERs/");
            if (response.IsSuccessStatusCode)
            {
                users = await response.Content.ReadAsAsync<List<USER>>();
            }
            return users;
        }

        ////POST
        //static async Task<Uri> CreateUserAsync(USER user)
        //{
        //    HttpResponseMessage response = await client.PostAsJsonAsync("api/USERs", user);
        //    response.EnsureSuccessStatusCode();

        //    // return URI of the created resource.
        //    return response.Headers.Location;
        //}

        //PUT
        static async Task<USER> UpdateuserAsync(USER user)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/USERs/{user.Id}", user);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated user from the response body.
            user = await response.Content.ReadAsAsync<USER>();
            return user;
        }

        //DELETE
        static async Task<HttpStatusCode> DeleteUserAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/USERs/{id}");
            return response.StatusCode;
        }
    }
}