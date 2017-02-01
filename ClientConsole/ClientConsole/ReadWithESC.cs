using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class ReadWithESC
    {
        public static bool GoBack = false;
        public static bool F5_Pressed = false;

        public static string ReadLineWithESC(bool password = false)
        {
            StringBuilder sb = new StringBuilder();

            string result = null;
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape && info.Key != ConsoleKey.F5)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    if (password)
                        Console.Write("*");
                    else
                        Console.Write(info.KeyChar);
                    sb.Append(info.KeyChar);
                }
                else if (sb.Length >= 1)
                {
                    Console.Write("\b ");
                    sb.Length--;
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                }
                info = Console.ReadKey(true);
            }

            if (info.Key == ConsoleKey.Escape)
            {
                GoBack = true;
            }

            if (info.Key == ConsoleKey.F5)
            {
                F5_Pressed = true;
            }

            if (info.Key == ConsoleKey.Enter)
            {
                F5_Pressed = false;
                GoBack = false;
                Console.WriteLine();
                result = sb.ToString();
            }

            return result;
        }
    }
}
