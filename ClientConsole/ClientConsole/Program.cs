using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    class Program
    {
        static HttpClient client = new HttpClient();

        private static int Mod = 0; // Urcuje Mod
        private static bool Back = false; // Urcuje Mod
        static ConsoleKey Tlacitko = ConsoleKey.F1; // Ukladani zmacknutych tlacitek do Tlacitko
        static bool IsLoginbValid = false;
        static USER LoggedInUser = new USER();

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;

            while (Mod != -1) // Pokud se Mod = -1 program se ukonci
            {
                if (Mod == 0) // Skoci do MenuMod 0
                    Mod = MenuMod(); // Mod = MenuMod()
                else if (Mod == 1) // Skoci do PlayMod 1
                    Mod = LogInMod(); // Mod = PlayMod()
                else if (Mod == 2) // Skoci do OptionMod 2
                    Mod = RegisterMod(); // Mod = OptionsMod()
                else if (Mod == 4) // Skoci do OptionMod 2
                    Mod = UserMod(); // Mod = OptionsMod()
                else if (Mod == 3) // Exit 3
                    Mod = -1;
            } // Konec programu   

            //RunAsync().Wait();

            //Console.ReadLine();
        }

        static int MenuMod() // MenuMod kdyz Mod = 0
        {
            string[] Polozky = new string[] { "                 Log in                   ", "                 Register                 ", "                 Exit                     " }; // Pole stringů položky v menu
            int Vybrana = 0; // Urcuje vybranou polozku v menu

            while (true) // Hlida tlacitka a vykresluje menu
            {
                VykresliMenu(Polozky, Vybrana); // Vykresluje menu

                Tlacitko = Console.ReadKey().Key; // Ceka na zmacknuti tlacitka

                if (Tlacitko == ConsoleKey.UpArrow) // ↑
                {
                    Vybrana--; // Zmensi Vybrana o 1
                    if (Vybrana < 0) // Pokud je Vybrana mensi nez 0
                        Vybrana = Polozky.Length - 1; // Skoci na posledni polozku (Vybrana se precisluje)
                }
                else if (Tlacitko == ConsoleKey.DownArrow) // ↓
                {
                    Vybrana++; // Zvetsi Vybrana o 1
                    if (Vybrana > Polozky.Length - 1) // Pokud je Vybrana vetsi nez index posleni polozky
                        Vybrana = 0; // Skoci na prvni polozku (Vybrana se precisluje)
                }
                else if (Tlacitko == ConsoleKey.Enter) // Pokud se zmackne Enter
                    return Vybrana + 1; // Vrati hodnotu ktera se pouzije jako cislo modu (Vybrana = 0; - Enter -, vrati 1 jako Mod = 1)
            }
        } // MenuMod konec

        static void VykresliMenu(string[] Polozky, int Vybrana) // Vykresluje menu
        {
            Console.Clear(); // Vymaze konzoli
            Console.SetWindowSize(45, 15); // Nastavi rozmery konzole (41 + 3, 21 - 6)
            Console.CursorVisible = false; // Kurzor neni videt
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            //VykresleniNazvu(); // Vykresli Nadpis

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("                THUNDER CHAT                 "); // Vypise HighScore fialove
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();

            int Krok = 0; // Pomaha vykreslit vybranou polozku cervene

            foreach (string polozka in Polozky) // Vypise pole stringu
            {
                if (Krok == Vybrana) // Na zacatku bude prvni polozka cervena protoze Krok = 0 && Vybrana = 0
                {
                    Console.BackgroundColor = ConsoleColor.White; // Nastavi pozadi na cernou barvu
                    Console.Write(" "); // Odsadí mezeru
                    Console.ForegroundColor = ConsoleColor.White; // Nastavi vybranou polozku na cervenou barvu
                    Console.BackgroundColor = ConsoleColor.DarkGray; // Nastavi pozadi vybrane polozky na zlutou barvu
                    Console.WriteLine(polozka); // Vypise vsechny polozky
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black; // Vse ostatni na zlutou barvu
                    Console.WriteLine("  " + polozka); // Vypise vsechny polozky
                }
                Console.BackgroundColor = ConsoleColor.White; // Nastavi pozadi na cernou barvu

                Krok++; // Zvetsi Krok o 1
            }
        } // VykresleniMenu konec

        static int LogInMod() // PlayMod = Mod 1
        {
            Console.Clear();
            Console.SetWindowSize(45, 15); // Nastavi rozmery konzole (41 + 3, 21 - 6)
            Console.CursorVisible = true;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("                   LOG IN                    "); // Vypise HighScore fialove
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();

            //USER user = new USER();
            Console.Write("Enter your login: ");
            LoggedInUser.Login = readLineWithCancel();
            if (Back)
                return 0;
            Console.Write("Enter your password: ");
            LoggedInUser.Password = readLineWithCancel();
            if (Back)
                return 0;

            Console.WriteLine();

            CheckLogin().Wait();

            if (IsLoginbValid)
            { 
                Console.WriteLine("Login successful");
                System.Threading.Thread.Sleep(2000);

                return 4;
                //todo
            }
            else
            {
                Console.WriteLine("Login or password is incorect");
                Console.WriteLine("Press ENTER and try it again...");
                Tlacitko = Console.ReadKey().Key;
                if (Tlacitko == ConsoleKey.Escape)
                    return 0;
                else if (Tlacitko == ConsoleKey.Enter)
                    return 1;
            }



            //readLineWithCancel();

            return 0; // Vrati se do MenuMod = Mod 0
        } // LogInMod konec

        static async Task CheckLogin()
        {
            GetTask<List<USER>> GetUsers = new GetTask<List<USER>>();
            foreach (USER item in await GetUsers.GetUserAsync($"api/USERs/"))
            {
                if (item.Login == LoggedInUser.Login && item.Password == LoggedInUser.Password)
                {
                    LoggedInUser.Id = item.Id;
                    LoggedInUser.Login = item.Login;
                    LoggedInUser.Password = item.Password;
                    LoggedInUser.Nick = item.Nick;
                    LoggedInUser.Photo = item.Photo;
                    IsLoginbValid = true;
                    break;
                }
                else
                    IsLoginbValid = false;
            }
        }

        static int RegisterMod() // PlayMod = Mod 1
        {
            Console.Clear();
            Console.SetWindowSize(45, 15); // Nastavi rozmery konzole (41 + 3, 21 - 6)
            Console.CursorVisible = true;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("                REGISTRATION                 "); // Vypise HighScore fialove
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();

            USER user = new USER() { Photo = "default.png" };
            Console.Write("Enter your login: ");
            user.Login = readLineWithCancel();
            if (Back)
                return 0;
            Console.Write("Enter your password: ");
            user.Password = readLineWithCancel();
            if (Back)
                return 0;
            Console.Write("Enter your nick: ");
            user.Nick = readLineWithCancel();
            if (Back)
                return 0;

            Console.WriteLine();

            try
            {
                GetTask<USER> GetUser = new GetTask<USER>();
                GetUser.CreateUserAsync("api/USERs", user).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine("Failure user was not created");
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Success user was created");
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            return 0; // Vrati se do MenuMod = Mod 0
        } // RegisterMod konec


        static int UserMod()
        {
            Console.Clear();
            Console.SetWindowSize(45, 15); // Nastavi rozmery konzole (41 + 3, 21 - 6)
            Console.CursorVisible = true;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("              USER PROFILE               "); // Vypise HighScore fialove
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Your nick: " + LoggedInUser.Nick + "    ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();

            //Console.WriteLine("Id: " + LoggedInUser.Id);
            //Console.WriteLine("Login: " + LoggedInUser.Login);
            //Console.WriteLine("Nick: " + LoggedInUser.Nick);
            //Console.WriteLine("Password: " + LoggedInUser.Password);
            //Console.WriteLine("Photo: " + LoggedInUser.Photo);

            Console.ReadLine();

            return 0;
        }

        static int UserMenuMod()
        {

            return 0;
        }

        private static string readLineWithCancel()
        {
            string result = null;

            StringBuilder buffer = new StringBuilder();

            //The key is read passing true for the intercept argument to prevent
            //any characters from displaying when the Escape key is pressed.
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape)
            {
                Console.Write(info.KeyChar);
                buffer.Append(info.KeyChar);
                info = Console.ReadKey(true);
            }

            if (info.Key == ConsoleKey.Enter)
            {
                result = buffer.ToString();
            }

            if (info.Key == ConsoleKey.Escape)
                Back = true;
            else
            {
                Back = false;
                Console.WriteLine();
            }

            return result;
        }

        static async Task RunAsync()
        {

            // Create a new user
            //USER user = new USER() { Nick = "test" };

            //var url = await CreateUserAsync(user);
            //Console.WriteLine($"Created at {url}");


            // Get the user
            GetTask<USER> GetUser = new GetTask<USER>();
            USER user = await GetUser.GetUserAsync($"api/USERs/1");
            Console.WriteLine("Id: " + user.Id);
            Console.WriteLine("Login: " + user.Login);
            Console.WriteLine("Nick: " + user.Nick);
            Console.WriteLine("Password: " + user.Password);
            Console.WriteLine("Photo: " + user.Photo);

            Console.WriteLine();

            // Get users
            GetTask<List<USER>> GetUsers = new GetTask<List<USER>>();
            foreach (USER item in await GetUsers.GetUserAsync($"api/USERs/"))
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