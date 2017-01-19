using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class Contact
    {
        public ConsoleKey Key = ConsoleKey.F1;
        public List<USER> UserFriends = new List<USER>();

        public int ContactMod()
        {
            this.GetFriends().Wait();

            string[] items = new string[this.UserFriends.Count];

            int i = 0;
            foreach (USER item in this.UserFriends)
            {
                items[i] = item.Nick;
                i++;
            }

            int selected = 0;

            while (true)
            {
                this.RenderContactMod(items, selected);

                this.Key = Console.ReadKey().Key;

                if (this.Key == ConsoleKey.UpArrow)
                {
                    selected--;
                    if (selected < 0)
                        selected = items.Length - 1;
                }
                else if (this.Key == ConsoleKey.DownArrow)
                {
                    selected++;
                    if (selected > items.Length - 1)
                        selected = 0;
                }
                else if (this.Key == ConsoleKey.Enter)
                {
                    Program.Friend = new USER() { Nick = items[selected] };

                    foreach (USER item in this.UserFriends)
                    {
                        if (item.Nick == Program.Friend.Nick)
                        {
                            Program.Friend.Id = item.Id;
                            Program.Friend.Login = item.Login;
                            Program.Friend.Password = item.Password;
                            Program.Friend.Nick = item.Nick;
                            Program.Friend.Photo = item.Photo;
                        }
                    }

                    return 10;
                }
            }
        }

        public void RenderContactMod(string[] items, int selected)
        {
            Console.Clear();
            Console.SetWindowSize(45, 15);
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("             YOUR CONTACT LIST               ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Your nick:   " + Program.LoggedInUser.Nick + "   ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();

            int step = 0;

            foreach (string item in items)
            {
                if (step == selected)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(" ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(item);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("  " + item);
                }
                Console.BackgroundColor = ConsoleColor.White;

                step++;
            }
        }

        async Task GetFriends()
        {
            GetTask<List<USER>> GetUserFriends = new GetTask<List<USER>>();
            this.UserFriends = await GetUserFriends.GetAsync($"api/USER_FRIENDS/" + Program.LoggedInUser.Id + "?token=" + Program.Token.Token);

            //UserFriends = UserFriendsss;
            /*
            GetTask<List<USER_FRIENDS>> GetUserFriends = new GetTask<List<USER_FRIENDS>>();
            List<USER_FRIENDS> UserFriendsss = await GetUserFriends.GetAsync($"api/USER_FRIENDS");
            List<int> temp = new List<int>();
            foreach (USER_FRIENDS item in UserFriendsss)
            {
                if (item.Id_Friendlist_Owner == LoggedInUser.Id)
                    temp.Add(item.Id_Friend);
            }

            GetTask<USER> GetUser = new GetTask<USER>();
            List<USER> GetUsers = new List<USER>();
            foreach (int item in temp)
            {
                GetUsers.Add(await GetUser.GetAsync($"api/USERs/" + item));
            }

            UserFriends = GetUsers;
            */
        }
    }
}
