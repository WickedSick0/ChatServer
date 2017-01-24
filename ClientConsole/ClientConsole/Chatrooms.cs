using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class Chatrooms
    {
        public List<CHATROOM> Chat_room = new List<CHATROOM>();

        public int ChatroomMod()
        {
            this.GetChatrooms().Wait();

            int selected = 0;
            ConsoleKey key = new ConsoleKey();

            while (true)
            {
                this.RenderChatroom(selected);

                key = Console.ReadKey().Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selected--;
                    if (selected < 0)
                        selected = this.Chat_room.Count - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selected++;
                    if (selected > this.Chat_room.Count - 1)
                        selected = 0;
                }
                else if (key == ConsoleKey.Escape)
                {
                    return 4;
                }
                else if (key == ConsoleKey.Enter)
                {
                    CHATROOM c = new CHATROOM() { Chatroom_Name = Chat_room[selected].Chatroom_Name };

                    foreach (CHATROOM item in this.Chat_room)
                    {
                        if (item.Chatroom_Name == c.Chatroom_Name)
                        {
                            Program.Chatroom.Id = item.Id;
                            Program.Chatroom.Chatroom_Name = item.Chatroom_Name;
                        }
                    }

                    return 11;
                }
            }
        }

        public void RenderChatroom(int selected)
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

            foreach (CHATROOM item in this.Chat_room)
            {
                if (step == selected)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(" ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(item.Chatroom_Name);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("  " + item.Chatroom_Name);
                }

                Console.BackgroundColor = ConsoleColor.White;

                step++;
            }
        }

        public async Task GetChatrooms()
        {
            GetTask<List<CHATROOM>> GetChatroomsByUser = new GetTask<List<CHATROOM>>();
            this.Chat_room = await GetChatroomsByUser.GetAsync($"api/CHATROOMs/" + Program.Token.Id_User + "?token=" + Program.Token.Token);
        }
    }
}
