using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class Registration
    {
        public USER User = new USER();

        public bool IsCreated = false;

        public int RegisterMod()
        {
            Console.Clear();
            Console.SetWindowSize(45, 15);
            Console.CursorVisible = true;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("                THUNDER CHAT                 ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("                Registration                 ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();

            Console.Write(" Enter your login: ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            this.User.Login = ReadWithESC.ReadLineWithESC();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            if (ReadWithESC.GoBack)
                return 0;
            Console.Write(" Enter your password: ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            this.User.Password = ReadWithESC.ReadLineWithESC(true);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            if (ReadWithESC.GoBack)
                return 0;
            Console.Write(" Enter your nick: ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            this.User.Nick = ReadWithESC.ReadLineWithESC();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            if (ReadWithESC.GoBack)
                return 0;

            Console.WriteLine();

            this.CreateUser().Wait();

            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.White;
            if (this.IsCreated)
                Console.WriteLine(" Success user was created                    ");
            else
                Console.WriteLine(" Failure user was not created                ");

            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Press ENTER to continue...                  ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ReadLine();

            return 0;
        }

        public async Task CreateUser()
        {
            try
            {
                GetTask<USER> CreateUser = new GetTask<USER>();
                CreateUser.CreateAsync($"api/USERs", this.User).Wait();
                this.IsCreated = true;
            }
            catch
            {
                this.IsCreated = false;
            }
        }
    }
}
