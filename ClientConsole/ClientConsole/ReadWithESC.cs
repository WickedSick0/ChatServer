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

        public static string ReadLineWithESC()
        {
            StringBuilder sb = new StringBuilder();

            string result = null;
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
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

            if (info.Key == ConsoleKey.Enter)
            {
                GoBack = false;
                Console.WriteLine();
                result = sb.ToString();
            }

            return result;
        }
    }
}
