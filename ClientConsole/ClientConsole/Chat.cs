using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
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
            Console.SetWindowSize(45, 25);
            Console.CursorVisible = true;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("                THUNDER CHAT                 ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("                  Chatroom                   ");
            Console.WriteLine("                Your nick:                   ");
            Console.SetCursorPosition(27, 2);
            Console.WriteLine(Program.LoggedInUser.Nick);
            Console.WriteLine("            Chatroom name:                   ");
            Console.SetCursorPosition(27, 3);
            Console.WriteLine(Program.Chatroom.Chatroom_Name);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();

            this.GetMessages().Wait();
            this.GetUsersInChatroom(Program.Chatroom.Id).Wait();

            foreach (MESSAGE item in this.Messages)
            {
                string own = this.FindUserPost(item.Id_User_Post);
                Console.WriteLine(" (" + own + "): " + item.Message_text);
            }

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("------------Press F5 to refresh--------------");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" Enter your message: ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            MessageAutorization msg = new MessageAutorization();
            msg.Id_Chatroom = Program.Chatroom.Id;
            msg.Id_User_Post = Program.LoggedInUser.Id;
            msg.Send_time = DateTime.Now;
            msg.token = Program.Token.Token;
            msg.Message_text = ReadWithESC.ReadLineWithESC();

            if (ReadWithESC.F5_Pressed != true)
                this.CreateMessage(msg).Wait();

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            if (ReadWithESC.GoBack)
                return 7;
            if (ReadWithESC.F5_Pressed)
                return 11;


            return 11;
        }

        public async Task GetMessages()
        {
            GetTask<List<MESSAGE>> GetMessage = new GetTask<List<MESSAGE>>();
            this.Messages = await GetMessage.GetAsync($"api/MESSAGEs/" + Program.Chatroom.Id + "?token=" + Program.Token.Token);
        }

        public async Task GetUsersInChatroom(int id)
        {
            GetTask<List<USER>> GetUsersInChat = new GetTask<List<USER>>();
            this.UsersInChatroom = await GetUsersInChat.GetAsync($"api/CHATROOM_MEMBERS/" + id + "?token=" + Program.Token.Token);
        }

        async Task CreateMessage(MessageAutorization message)
        {
            GetTask<MessageAutorization> CreateUser = new GetTask<MessageAutorization>();
            CreateUser.CreateAsync($"api/MESSAGEs", message).Wait();
        }

        public string FindUserPost(int id_Post)
        {
            foreach (USER item in this.UsersInChatroom)
            {
                if (id_Post == item.Id)
                    return item.Nick;
            }

            return null;
        }
    }
}
