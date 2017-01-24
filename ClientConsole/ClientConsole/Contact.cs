using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class Contact
    {
        public List<USER> UserFriends = new List<USER>();

        public int ContactMod()
        {
            this.GetFriends().Wait();

            int selected = 0;
            ConsoleKey key = new ConsoleKey();

            while (true)
            {
                this.RenderContactMod(selected);

                key = Console.ReadKey().Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selected--;
                    if (selected < 0)
                        selected = this.UserFriends.Count - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selected++;
                    if (selected > this.UserFriends.Count - 1)
                        selected = 0;
                }
                else if (key == ConsoleKey.Escape)
                {
                    return 4;
                }
                else if (key == ConsoleKey.Enter)
                {
                    Program.Friend = new USER() { Nick = this.UserFriends[selected].Nick };

                    foreach (USER item in this.UserFriends)
                    {
                        if (item.Nick == Program.Friend.Nick)
                        {
                            Program.Friend.Id = item.Id;
                            Program.Friend.Login = item.Login;
                            Program.Friend.Password = item.Password;
                            Program.Friend.Nick = item.Nick;
                            Program.Friend.Photo = item.Photo;
                            return 8;
                        }
                    }

                    return 10;
                }
            }
        }

        public void RenderContactMod(int selected)
        {
            Console.Clear();
            Console.SetWindowSize(45, 15);
            Console.CursorVisible = false;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("                THUNDER CHAT                 ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("                Contact list                 ");
            Console.WriteLine("                Your nick:                   ");
            Console.SetCursorPosition(27, 2);
            Console.WriteLine(Program.LoggedInUser.Nick);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();

            int step = 0;

            foreach (USER item in this.UserFriends)
            {
                if (step == selected)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(" ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(item.Login + " (" + item.Nick + ")");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("  " + item.Login + " (" + item.Nick + ")");
                }

                Console.BackgroundColor = ConsoleColor.White;

                step++;
            }
        }

        public async Task GetFriends()
        {
            GetTask<List<USER>> GetUserFriends = new GetTask<List<USER>>();
            this.UserFriends = await GetUserFriends.GetAsync($"api/USER_FRIENDS/" + Program.LoggedInUser.Id + "?token=" + Program.Token.Token);
        }
    }
}
