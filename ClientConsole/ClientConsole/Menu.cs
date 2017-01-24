using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class Menu
    {
        public int MenuMod()
        {
            List<string> items = new List<string>() {
                "                 Log in                   ",
                "             Registration                 ",
                "                 Exit                     "
            };

            int selected = 0;
            ConsoleKey key = new ConsoleKey();

            while (true)
            {
                this.RenderMenu(items, selected);

                key = Console.ReadKey().Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selected--;
                    if (selected < 0)
                        selected = items.Count - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selected++;
                    if (selected > items.Count - 1)
                        selected = 0;
                }
                else if (key == ConsoleKey.Enter)
                    return selected + 1;
            }
        }

        public void RenderMenu(List<string> items, int selected)
        {
            Console.Clear();
            Console.SetWindowSize(45, 15);
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("                THUNDER CHAT                 ");
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
