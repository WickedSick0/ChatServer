using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class Register
    {
        public static bool Back = false;

        public static int RegisterMod() // PlayMod = Mod 1
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
                GetUser.CreateAsync("api/USERs", user).Wait();
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
    }
}
