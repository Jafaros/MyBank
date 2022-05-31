using System;

namespace MyBank
{
    //Zde jsou všechny dostupné příkazy pro administrátory
    class Commands
    {
        public static void SetMoney(int accountId, int bankId, int count)
        {
            if (Program.adminLogged)
            {
                foreach (var account in Program.accounts)
                {
                    if (account.id == accountId && account.bankId == bankId)
                    { 
                        account.money = count;
                    }
                }

                Console.WriteLine("Castka {0} byla uspesne pripsana na ucet {1}", count, accountId);
            }
            else 
            { 
                Console.WriteLine("Nemate administratosrka opravneni k provedeni tohoto prikazu");
            } 
        }

        public static void GetAccount(string name)
        {
            if (Program.adminLogged)
            {
                foreach (var account in Program.accounts)
                {
                    if (name == account.name)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\nJmeno a prijmeni: {0}\nUzivatelske jmeno: {1}\nHeslo: {2}\nCislo uctu: {3}\nCislo banky: {4}\nPenezni castka: {5} CZK\n", account.fullName, account.name, account.password, account.id, account.bankId, account.money);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
            else
            {
                Console.WriteLine("Nemate administratosrka opravneni k provedeni tohoto prikazu");
            }
        }

        public static void CreateAccount()
        {
            if (Program.adminLogged)
            {
                string password = "0";

                Console.Write("Zadejte id uctu: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("Zadejte cele jmeno uctu: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string fullName = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("Zadejte uzivatelske jmeno uctu: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string name = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("Zadejte ID banky: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                int bankId = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("Zadejte pocatecni castku: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                int money = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.White;

                Random random = new Random();

                for (int i = 0; i < 8; i++)
                {                    
                    password += random.Next(9);
                }

                Program.accounts.Add(new Account { id = id, fullName = fullName, name = name, password = password, bankId = bankId, money = money});
                Console.WriteLine("Uzivatel byl uspesne vytvoren!");
            }
            else
            {
                Console.WriteLine("Nemate administratosrka opravneni k provedeni tohoto prikazu");
            }
        }

        public static void Help()
        {
            Console.WriteLine("\nPrikaz 'help' zobrazi vsechny prikazy");
            Console.WriteLine("Prikaz 'exit' vas odhlasi");
            Console.WriteLine("Prikaz 'create' vytvori novy ucet (Pouze pro administratory)");
            Console.WriteLine("Prikaz 'set' nastavi castku na dany ucet (Pouze pro administratory)");
            Console.WriteLine("Prikaz 'get' zobrazi informace o danem ucte (Pouze pro administratory)\n");
        }

        public static void LogOut()
        {
            Program.adminLogged = false;
        }
    }
}
