using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class CreateChatroom
    {
        public CHATROOM NewChat = new CHATROOM();

        public int CreateChatroomMod()
        {
            Console.Clear();
            Console.SetWindowSize(45, 15);
            Console.CursorVisible = true;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("                THUNDER CHAT                 ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("               Create chatroom               ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();

            Console.Write(" Enter chatroom name: ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            this.NewChat.Chatroom_Name = ReadWithESC.ReadLineWithESC();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            if (ReadWithESC.GoBack)
                return 4;

            this.CreateChat().Wait();
            this.AddMembers(Program.LoggedInUser.Id).Wait();
            this.AddMembers(Program.Friend.Id).Wait();

            Program.Chatroom = this.NewChat;

            return 11;
        }

        public async Task CreateChat()
        {
            GetTask<CHATROOM> CreateChatr = new GetTask<CHATROOM>();
            CreateChatr.CreateAsync($"api/CHATROOMs", this.NewChat).Wait();

            this.NewChat = await LogIn.resp.Content.ReadAsAsync<CHATROOM>();
        }

        public async Task AddMembers(int id_user)
        {
            CHATROOM_MEMBERS chat = new CHATROOM_MEMBERS();
            chat.Id_Chatroom = this.NewChat.Id;
            chat.Id_User = id_user;

            GetTask<CHATROOM_MEMBERS> CreateChatr = new GetTask<CHATROOM_MEMBERS>();
            CreateChatr.CreateAsync($"api/CHATROOM_MEMBERS", chat).Wait();
        }
    }
}
