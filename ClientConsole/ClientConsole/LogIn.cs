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
        public static ConsoleKey Tlacitko = ConsoleKey.F1; // Ukladani zmacknutych tlacitek do Tlacitko
        public static bool Back = false;
        public static bool IsLoginValid = false;

        public static int LogInMod()
        {
            Console.Clear();
            Console.SetWindowSize(45, 15);
            Console.CursorVisible = true;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("                   LOG IN                    ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();

            Console.Write("Enter your login: ");
            Program.LoggedInUser.Login = ReadWithESC.ReadLineWithESC();
            if (ReadWithESC.GoBack)
                return 0;

            Console.Write("Enter your password: ");
            Program.LoggedInUser.Password = ReadWithESC.ReadLineWithESC();
            if (ReadWithESC.GoBack)
                return 0;

            Console.WriteLine();

            CheckLogin().Wait();

            if (IsLoginValid)
            {
                Console.WriteLine("Login successful");
                //System.Threading.Thread.Sleep(2000);
                return 4;
            }
            else
            {
                Console.WriteLine("Login or password is incorect");
                Console.WriteLine();
                Console.WriteLine("Press ENTER and try it again...");
                Console.WriteLine("Press ESC to go back to menu...");
                Tlacitko = Console.ReadKey().Key;
                if (Tlacitko == ConsoleKey.Escape)
                    return 0;
                else if (Tlacitko == ConsoleKey.Enter)
                    return 1;
            }

            return 0;
        } // LogInMod konec

        //static async Task CheckLogin()
        //{
        //    GetTask<List<USER>> GetUsers = new GetTask<List<USER>>();
        //    foreach (USER item in await GetUsers.GetAsync($"api/USERs/"))
        //    {
        //        if (item.Login == Program.LoggedInUser.Login && item.Password == Program.LoggedInUser.Password)
        //        {
        //            Program.LoggedInUser.Id = item.Id;
        //            Program.LoggedInUser.Login = item.Login;
        //            Program.LoggedInUser.Password = item.Password;
        //            Program.LoggedInUser.Nick = item.Nick;
        //            Program.LoggedInUser.Photo = item.Photo;
        //            IsLoginValid = true;
        //            break;
        //        }
        //        else
        //            IsLoginValid = false;
        //    }
        //}

        public static string Token { get; set; }

        static async Task CheckLogin()
        {
            LoginModel lmodel = new LoginModel();
            lmodel.Username = Program.LoggedInUser.Login;
            lmodel.Password = Program.LoggedInUser.Password;


            GetTask<LoginModel> CreateToken = new GetTask<LoginModel>();
            Uri Token = await CreateToken.CreateAsync($"api/USER_TOKENS", lmodel);
            Console.WriteLine(Token.UserInfo);
        }
    }
}
