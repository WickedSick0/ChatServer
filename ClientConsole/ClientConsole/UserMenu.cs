using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class UserMenu
    {
        public ConsoleKey Key = ConsoleKey.F1;

        public int UserMenuMod()
        {
            string[] items = new string[] {
                "              Contacts                     ",
                "              Add contact                  ",
                "              Chatrooms                    ",

                "              Requests                     ",
                                "              Logout                       "
            };

            int selected = 0;

            while (true)
            {
                this.RenderUserMenu(items, selected);

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
                    if (selected == 4)
                    {
                        Program.LoggedInUser = null;
                        Program.Token = null;

                        return 0;
                    }
                    else
                    {
                        return selected + 1 + 4;
                    }
                }
            }
        }

        public void RenderUserMenu(string[] items, int selected)
        {
            Console.Clear();
            Console.SetWindowSize(45, 15);
            Console.CursorVisible = false;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("                THUNDER CHAT                 ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("                User Profile                 ");
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
    }
}
