using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class LogIn
    {
        public static ConsoleKey Tlacitko = ConsoleKey.F1; // Ukladani zmacknutych tlacitek do Tlacitko
        public static bool Back = false;
        public static bool IsLoginbValid = false;

        public static int LogInMod() // PlayMod = Mod 1
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

            Program.LoggedInUser.Login = readLineWithCancel();
            if (Back)
                return 0;
            Console.Write("Enter your password: ");
            Program.LoggedInUser.Password = readLineWithCancel();
            if (Back)
                return 0;

            Console.WriteLine();

            CheckLogin().Wait();

            if (IsLoginbValid)
            {
                Console.WriteLine("Login successful");
                //System.Threading.Thread.Sleep(2000);

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

        public static string readLineWithCancel()
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


        static async Task CheckLogin()
        {
            GetTask<List<USER>> GetUsers = new GetTask<List<USER>>();
            foreach (USER item in await GetUsers.GetAsync($"api/USERs/"))
            {
                if (item.Login == Program.LoggedInUser.Login && item.Password == Program.LoggedInUser.Password)
                {
                    Program.LoggedInUser.Id = item.Id;
                    Program.LoggedInUser.Login = item.Login;
                    Program.LoggedInUser.Password = item.Password;
                    Program.LoggedInUser.Nick = item.Nick;
                    Program.LoggedInUser.Photo = item.Photo;
                    IsLoginbValid = true;
                    break;
                }
                else
                    IsLoginbValid = false;
            }
        }
    }
}
