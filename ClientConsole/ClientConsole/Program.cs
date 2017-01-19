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
        public static HttpClient client = new HttpClient();

        public static int Mod = 0;
        public ConsoleKey Key = ConsoleKey.F1;
        public static USER LoggedInUser = new USER();
        public static USER Friend = new USER();
        //public static List<CHATROOM> Chatrooms = new List<CHATROOM>();
        public static CHATROOM Chatroom = new CHATROOM();
        public static USER_TOKENS Token = null;
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;

            while (Mod != -1)
            {
                if (Mod == 0)
                {
                    Menu menu = new Menu();
                    Mod = menu.MenuMod();
                }
                else if (Mod == 1)
                {
                    LogIn login = new LogIn();
                    Mod = login.LogInMod();
                }
                else if (Mod == 2)
                {
                    Registration register = new Registration();
                    Mod = register.RegisterMod();
                }
                else if (Mod == 3) // Exit
                    Mod = -1;
                else if (Mod == 4)
                {
                    UserMenu usermenu = new UserMenu();
                    Mod = usermenu.UserMenuMod();
                }
                else if (Mod == 5)
                {
                    Contact contact = new Contact();
                    Mod = contact.ContactMod();
                }
                else if (Mod == 7)
                {
                    Chatrooms chatrooms = new Chatrooms();
                    Mod = chatrooms.ChatroomMod();
                }
                else if (Mod == 10)
                {
                    Mod = MessageMod();
                }
                else if (Mod == 11)
                {
                    Chat chat = new Chat();
                    Mod = chat.ChatMod();
                }
            }   
        }



        static int MessageMod()
        {
            Console.Clear(); 
            Console.SetWindowSize(45, 15);
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("             MESSAGE               ");
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
            /*
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
            */

            /*
            // Update the user
            Console.WriteLine("Updating price...");
            user.Nick = "tttttt";
            await UpdateuserAsync(user);

            // Get the updated user
            user = await GetUserAsync(url.PathAndQuery);
            Showuser(user);

            */

            // Delete the user
            //var statusCode = await DeleteUserAsync("16");
            //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
            
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