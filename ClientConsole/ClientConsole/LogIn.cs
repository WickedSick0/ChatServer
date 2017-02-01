using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class LogIn
    {
        public bool IsLoginValid = false;

        public static HttpResponseMessage resp { get; set; }

        public int LogInMod()
        {
            Program.LoggedInUser = new USER();

            Console.Clear();
            Console.SetWindowSize(45, 15);
            Console.CursorVisible = true;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("                THUNDER CHAT                 ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("                   Log In                    ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();

            Console.Write(" Enter your login: ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Program.LoggedInUser.Login = ReadWithESC.ReadLineWithESC();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            if (ReadWithESC.GoBack)
                return 0;

            Console.Write(" Enter your password: ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Program.LoggedInUser.Password = ReadWithESC.ReadLineWithESC(true);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            if (ReadWithESC.GoBack)
                return 0;

            Console.WriteLine();

            this.CheckLogin().Wait();

            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            if (this.IsLoginValid)
            {
                Console.WriteLine("Login successful");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                return 4;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(" Login or password is incorect               ");
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" Press ENTER and try it again...             ");
                Console.WriteLine(" Press ESC to go back to menu...             ");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;

                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Escape)
                    return 0;
                else if (key == ConsoleKey.Enter)
                    return 1;
            }

            return 0;
        }

        async Task CheckLogin()
        {
            this.IsLoginValid = false;

            LoginModel lmodel = new LoginModel();
            lmodel.Username = Program.LoggedInUser.Login;
            lmodel.Password = Program.LoggedInUser.Password;

            try
            {
                GetTask<LoginModel> CreateToken = new GetTask<LoginModel>();
                CreateToken.CreateAsync($"api/USER_TOKENS", lmodel).Wait();

                Program.Token = await resp.Content.ReadAsAsync<USER_TOKENS>();

                GetTask<USER> GetUsers = new GetTask<USER>();
                Program.LoggedInUser = await GetUsers.GetAsync($"api/USERs/" + Program.Token.Id_User + "?token=" + Program.Token.Token);

                if (Program.LoggedInUser != null)
                    this.IsLoginValid = true;
            }
            catch
            {
                Program.LoggedInUser = null;
                this.IsLoginValid = false;
            }
        }
    }
}
