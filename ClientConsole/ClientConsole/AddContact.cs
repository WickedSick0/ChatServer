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
            this.RenderContact(0);

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

            int selected = 0;
            ConsoleKey key = new ConsoleKey();

            while (true)
            {
                this.RenderContact(selected);

                key = Console.ReadKey().Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selected--;
                    if (selected < 0)
                        selected = this.Users.Count - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selected++;
                    if (selected > this.Users.Count - 1)
                        selected = 0;
                }
                else if (key == ConsoleKey.Escape)
                {
                    return 4;
                }
                else if (key == ConsoleKey.Enter)
                {
                    USER_FRIENDS uf = new USER_FRIENDS();
                    uf.Id_Friendlist_Owner = Program.LoggedInUser.Id;
                    uf.Id_Friend = this.Users[selected].Id;

                    Console.ForegroundColor = ConsoleColor.Black;

                    //Console.WriteLine(uf.Id_Friend);
                    //Console.WriteLine(uf.Id_Friendlist_Owner);
                    //Console.ReadLine();

                    this.CreateContact(uf).Wait();


                    return 6;
                }
            }
        }

        public void RenderContact(int selected)
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

            int step = 0;

            foreach (USER item in this.Users)
            {
                if (step == selected)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(" ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(item.Nick);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("  " + item.Nick);
                }

                Console.BackgroundColor = ConsoleColor.White;

                step++;
            }
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
