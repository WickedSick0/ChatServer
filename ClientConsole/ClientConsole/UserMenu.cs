using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class UserMenu
    {
        public static ConsoleKey Tlacitko = ConsoleKey.F1; // Ukladani zmacknutych tlacitek do Tlacitko

        public static int UserMenuMod()
        {
            string[] Polozky = new string[] { "              Contacts                     ", "              Add contact                  ", "              Chatrooms                    ", "              Add Chatroom                 " }; // Pole stringů položky v menu
            int Vybrana = 0; // Urcuje vybranou polozku v menu

            while (true) // Hlida tlacitka a vykresluje menu
            {
                VykresliUserMenu(Polozky, Vybrana); // Vykresluje menu

                Tlacitko = Console.ReadKey().Key; // Ceka na zmacknuti tlacitka

                if (Tlacitko == ConsoleKey.UpArrow) // ↑
                {
                    Vybrana--; // Zmensi Vybrana o 1
                    if (Vybrana < 0) // Pokud je Vybrana mensi nez 0
                        Vybrana = Polozky.Length - 1; // Skoci na posledni polozku (Vybrana se precisluje)
                }
                else if (Tlacitko == ConsoleKey.DownArrow) // ↓
                {
                    Vybrana++; // Zvetsi Vybrana o 1
                    if (Vybrana > Polozky.Length - 1) // Pokud je Vybrana vetsi nez index posleni polozky
                        Vybrana = 0; // Skoci na prvni polozku (Vybrana se precisluje)
                }
                else if (Tlacitko == ConsoleKey.Enter) // Pokud se zmackne Enter
                    return Vybrana + 1 + 4; // Vrati hodnotu ktera se pouzije jako cislo modu (Vybrana = 0; - Enter -, vrati 1 jako Mod = 1)
            }
        }

        public static void VykresliUserMenu(string[] Polozky, int Vybrana) // Vykresluje menu
        {
            Console.Clear(); // Vymaze konzoli
            Console.SetWindowSize(45, 15); // Nastavi rozmery konzole (41 + 3, 21 - 6)
            Console.CursorVisible = false; // Kurzor neni videt
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            //VykresleniNazvu(); // Vykresli Nadpis

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("                USER PROFILE                 "); // Vypise HighScore fialove
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Your nick:   " + Program.LoggedInUser.Nick + "   ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine();

            int Krok = 0; // Pomaha vykreslit vybranou polozku cervene

            foreach (string polozka in Polozky) // Vypise pole stringu
            {
                if (Krok == Vybrana) // Na zacatku bude prvni polozka cervena protoze Krok = 0 && Vybrana = 0
                {
                    Console.BackgroundColor = ConsoleColor.White; // Nastavi pozadi na cernou barvu
                    Console.Write(" "); // Odsadí mezeru
                    Console.ForegroundColor = ConsoleColor.White; // Nastavi vybranou polozku na cervenou barvu
                    Console.BackgroundColor = ConsoleColor.DarkGray; // Nastavi pozadi vybrane polozky na zlutou barvu
                    Console.WriteLine(polozka); // Vypise vsechny polozky
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black; // Vse ostatni na zlutou barvu
                    Console.WriteLine("  " + polozka); // Vypise vsechny polozky
                }
                Console.BackgroundColor = ConsoleColor.White; // Nastavi pozadi na cernou barvu

                Krok++; // Zvetsi Krok o 1
            }
        } // VykresleniMenu konec
    }
}
