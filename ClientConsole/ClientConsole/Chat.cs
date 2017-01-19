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
        public List<USER> UsersInChatroom = new List<USER>();

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

            foreach (MESSAGE item in this.Messages)
            {
                Console.WriteLine(item.Id_User_Post + " " + item.Message_text);
            }


            Console.ReadLine();

            return 0;
        }

        public async Task GetMessages()
        {
            GetTask<List<MESSAGE>> GetUserFriends = new GetTask<List<MESSAGE>>();
            //this.Messages = await GetUserFriends.GetAsync($"api/MESSAGEs/" + Program.Chatroom.Id + "?token=" + Program.Token.Token);
            this.Messages = await GetUserFriends.GetAsync($"api/MESSAGEs/" + Program.Chatroom.Id);
        }

        public async Task GetUsersInChatroom()
        {
            GetTask<USER> GetUsersInChat = new GetTask<USER>();
            //this.Messages = await GetUserFriends.GetAsync($"api/MESSAGEs/" + Program.Chatroom.Id + "?token=" + Program.Token.Token);
            //this.Messages = await GetUsersInChat.GetAsync($"api/MESSAGEs/" + Program.Chatroom.Id);
        }
    }
}
