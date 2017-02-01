using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class AddContact
    {
        public List<USER> Users = new List<USER>();

        public int AddContactMod()
        {
            Console.Clear();
            Console.SetWindowSize(45, 15);
            Console.CursorVisible = true;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("                THUNDER CHAT                 ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("                Add contact                  ");
            Console.WriteLine("                Your nick:                   ");
            Console.SetCursorPosition(27, 2);
            Console.WriteLine(Program.LoggedInUser.Nick);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();

            //this.GetUsers().Wait();

            USER contact = new USER();

            Console.Write(" Enter contact name: ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            contact.Nick = ReadWithESC.ReadLineWithESC();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            if (ReadWithESC.GoBack)
                return 4;

            this.GetUsers(contact.Nick).Wait();

            bool isCreated = false;

            foreach (USER item in this.Users)
            {
                bool next = true;
                if (item.Nick == contact.Nick)
                {
                    USER_FRIENDS uf = new USER_FRIENDS();
                    uf.Id_Friendlist_Owner = Program.LoggedInUser.Id;
                    uf.Id_Friend = item.Id;

                    this.CreateContact(uf).Wait();

                    next = false;
                    isCreated = true;
                }
                else if (item.Login == contact.Nick && next)
                {
                    USER_FRIENDS uf = new USER_FRIENDS();
                    uf.Id_Friendlist_Owner = Program.LoggedInUser.Id;
                    uf.Id_Friend = item.Id;

                    this.CreateContact(uf).Wait();

                    isCreated = true;
                }
            }

            Console.WriteLine();

            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.White;
            if (isCreated)
                Console.WriteLine(" Success user was added to your contact      ");
            else
                Console.WriteLine(" Failure user was not found                  ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Press ENTER to continue...                  ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ReadLine();

            return 4;
        }

        public async Task GetUsers(string search)
        {
            GetTask<List<USER>> GetUser = new GetTask<List<USER>>();
            this.Users = await GetUser.GetAsync($"api/USERsearch/" + search + "?token=" + Program.Token.Token + "&id=" + Program.LoggedInUser.Id);
        }

        public async Task CreateContact(USER_FRIENDS uf)
        {
            GetTask<USER_FRIENDS> CreateCont = new GetTask<USER_FRIENDS>();
            CreateCont.CreateAsync($"api/USER_FRIENDS?token=" + Program.Token.Token, uf).Wait();
        }
    }
}
