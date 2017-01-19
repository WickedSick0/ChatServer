using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class Chat
    {
        public List<MESSAGE> Messages = new List<MESSAGE>();

        public int ChatMod()
        {
            Console.Clear();
            Console.SetWindowSize(45, 15);
            Console.CursorVisible = true;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("                THUNDER CHAT                 ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("                  Chatroom                   ");
            Console.WriteLine("            Chatroom name:                   ");
            Console.SetCursorPosition(27, 2);
            Console.WriteLine(Program.Chatroom.Chatroom_Name);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();

            this.GetMessages().Wait();

            Console.ReadLine();

            return 0;
        }

        async Task GetMessages()
        {
            GetTask<List<MESSAGE>> GetUserFriends = new GetTask<List<MESSAGE>>();
            this.Messages = await GetUserFriends.GetAsync($"api/MESSAGEs/" + Program.Chatroom.Id + "?token=" + Program.Token.Token);
        }
    }
}
