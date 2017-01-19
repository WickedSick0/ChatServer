using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class Chatrooms
    {
        public ConsoleKey Key = ConsoleKey.F1;
        public List<CHATROOM> Chat_room = new List<CHATROOM>();

        public int ChatroomMod()
        {
            this.GetChatrooms().Wait();

            string[] items = new string[this.Chat_room.Count];

            int i = 0;
            foreach (CHATROOM item in this.Chat_room)
            {
                items[i] = item.Chatroom_Name;
                i++;
            }

            int selected = 0;

            while (true)
            {
                this.RenderChatroom(items, selected);

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
                    Program.Chatroom = new CHATROOM() { Chatroom_Name = items[selected] };

                    foreach (CHATROOM item in this.Chat_room)
                    {
                        if (item.Chatroom_Name == Program.Friend.Nick)
                        {
                            Program.Chatroom.Chatroom_Name = item.Chatroom_Name;
                        }
                    }

                    return 11;
                }
            }
        }

        public void RenderChatroom(string[] items, int selected)
        {
            Console.Clear();
            Console.SetWindowSize(45, 15);
            Console.CursorVisible = false;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("                THUNDER CHAT                 ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("                 Chatrooms                   ");
            Console.WriteLine("                Your nick:                   ");
            Console.SetCursorPosition(27, 2);
            Console.WriteLine(Program.LoggedInUser.Nick);
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

        public async Task GetChatrooms()
        {
            GetTask<List<CHATROOM>> GetChatroomsByUser = new GetTask<List<CHATROOM>>();
            this.Chat_room = await GetChatroomsByUser.GetAsync($"api/CHATROOM_MEMBERS/" + Program.Token.Id_User + "?token=" + Program.Token.Token);
        }
    }
}
