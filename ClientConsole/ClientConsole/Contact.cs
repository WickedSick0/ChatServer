using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class Contact
    {
        public ConsoleKey Tlacitko = ConsoleKey.F1;
        static List<USER> UserFriends = new List<USER>();
        public int ContactMod()
        {
            GetFriends().Wait();

            string[] Polozky = new string[UserFriends.Count];

            int i = 0;
            foreach (USER item in UserFriends)
            {
                Polozky[i] = item.Nick;
                i++;
            }

            int Vybrana = 0; // Urcuje vybranou polozku v menu

            while (true) // Hlida tlacitka a vykresluje menu
            {
                VykresliContactMod(Polozky, Vybrana); // Vykresluje menu

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
                {
                    Program.Friend = new USER() { Nick = Polozky[Vybrana] };

                    foreach (USER item in UserFriends)
                    {
                        if (item.Nick == Program.Friend.Nick)
                        {
                            Program.Friend.Id = item.Id;
                            Program.Friend.Login = item.Login;
                            Program.Friend.Password = item.Password;
                            Program.Friend.Nick = item.Nick;
                            Program.Friend.Photo = item.Photo;
                        }
                    }

                    return 10; // Vrati hodnotu ktera se pouzije jako cislo modu (Vybrana = 0; - Enter -, vrati 1 jako Mod = 1)
                }
            }
        }

        public void VykresliContactMod(string[] Polozky, int Vybrana)
        {
            Console.Clear(); // Vymaze konzoli
            Console.SetWindowSize(45, 15); // Nastavi rozmery konzole (41 + 3, 21 - 6)
            Console.CursorVisible = false; // Kurzor neni videt
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            //VykresleniNazvu(); // Vykresli Nadpis

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("             YOUR CONTACT LIST               "); // Vypise HighScore fialove
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

            //return 4;
        }

        async Task GetFriends()
        {
            GetTask<List<USER>> GetUserFriends = new GetTask<List<USER>>();
            UserFriends = await GetUserFriends.GetAsync($"api/USER_FRIENDS/" + Program.LoggedInUser.Id);

            //UserFriends = UserFriendsss;
            /*
            GetTask<List<USER_FRIENDS>> GetUserFriends = new GetTask<List<USER_FRIENDS>>();
            List<USER_FRIENDS> UserFriendsss = await GetUserFriends.GetAsync($"api/USER_FRIENDS");
            List<int> temp = new List<int>();
            foreach (USER_FRIENDS item in UserFriendsss)
            {
                if (item.Id_Friendlist_Owner == LoggedInUser.Id)
                    temp.Add(item.Id_Friend);
            }

            GetTask<USER> GetUser = new GetTask<USER>();
            List<USER> GetUsers = new List<USER>();
            foreach (int item in temp)
            {
                GetUsers.Add(await GetUser.GetAsync($"api/USERs/" + item));
            }

            UserFriends = GetUsers;
            */
        }

    }
}
