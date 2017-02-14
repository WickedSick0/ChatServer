using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Threading;

namespace ClientConsole
{
    public class Chat
    {
        public List<MESSAGE> Messages = new List<MESSAGE>();
        public List<USER> UsersInChatroom = new List<USER>();

        //test
        public List<MESSAGE> NewMessages = new List<MESSAGE>();
        public bool Refresh = true;
        public Thread worker { get; set; }

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

            //foreach (USER item in this.UsersInChatroom)
            //{
            //    Console.ForegroundColor = ConsoleColor.Black;
            //    Console.WriteLine(item.Nick);
            //}

            //Console.ReadLine();

            foreach (MESSAGE item in this.Messages)
            {
                string own = this.FindUserPost(item.Id_User_Post);
                Console.WriteLine(" (" + own + "): " + item.Message_text);
            }

            //test
            this.worker = new Thread(() =>
                {
                    while (this.Refresh)
                    {
                        if (ReadWithESC.sb.Length > 0)
                            continue;

                        this.GetNewMessages().Wait();
                        if (this.Messages.Count < this.NewMessages.Count)
                        {
                            //Console.BackgroundColor = ConsoleColor.White;
                            //Console.ForegroundColor = ConsoleColor.Black;
                            this.ChatMod();                 
                        }
                        Thread.Sleep(5000);
                    }
                });

            this.worker.Start();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("------------Press F5 to refresh--------------");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" Enter your message: ");


            MessageAutorization msg = new MessageAutorization();
            msg.Id_Chatroom = Program.Chatroom.Id;
            msg.Id_User_Post = Program.LoggedInUser.Id;
            msg.Send_time = DateTime.Now;
            msg.token = Program.Token.Token;

            //Console.BackgroundColor = ConsoleColor.Black;
            //Console.ForegroundColor = ConsoleColor.White;
            msg.Message_text = ReadWithESC.ReadLineWithESC();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            if (ReadWithESC.F5_Pressed != true && ReadWithESC.GoBack != true && msg.Message_text.Length > 0)
            {
                this.CreateMessage(msg).Wait();
                return 11;
            }

            this.Refresh = false;
            if (ReadWithESC.GoBack)
            {
                this.Refresh = false;
                return 7;
            }
            if (ReadWithESC.F5_Pressed)
            {
                this.Refresh = false;
                return 11;
            }

            this.Refresh = false;
            return 11;
        }

        public async Task GetMessages()
        {
            GetTask<List<MESSAGE>> GetMessage = new GetTask<List<MESSAGE>>();
            this.Messages = await GetMessage.GetAsync($"api/MESSAGEs/" + Program.Chatroom.Id + "?token=" + Program.Token.Token);
        }

        public async Task GetNewMessages()
        {
            GetTask<List<MESSAGE>> GetMessage = new GetTask<List<MESSAGE>>();
            this.NewMessages = await GetMessage.GetAsync($"api/MESSAGEs/" + Program.Chatroom.Id + "?token=" + Program.Token.Token);
        }

        public async Task GetUsersInChatroom(int id)
        {
            GetTask<List<USER>> GetUsersInChat = new GetTask<List<USER>>();
            this.UsersInChatroom = await GetUsersInChat.GetAsync($"api/CHATROOM_MEMBERS/" + id + "?token=" + Program.Token.Token + "&IDUser=" + Program.LoggedInUser.Id);
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
