//Petr Grajciar, 3.B, 29. 5. 2022, PVA, MyBank

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank
{
    class Program
    {
        public static bool adminLogged = false;
        public static List<Account> accounts = new List<Account>();

        static void Main(string[] args)
        {       
            Console.WriteLine("========== MyBank ==========");

            bool stav = true, logged = false;
            string name, password;  

            accounts.Add(new Account() { id = 1, name = "default", password = "default", fullName = "Default account", money = 1000000, bankId = 1234 });
            accounts.Add(new Account() { id = 2, name = "petr", password = "petr", fullName = "Petr Grajciar", money = 1000, bankId = 1234 });
            accounts.Add(new Account() { id = 3, name = "jiri", password = "jiri", fullName = "Jiri Novak", money = 10000, bankId = 1234 });

            while (stav)
            {
                Console.Write("\nZadejte sve uzivatelske jmeno: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                name = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("Zadejte sve heslo: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                password = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;

                if (name == Admin.name && password == Admin.password)
                {
                    adminLogged = true;

                    while (adminLogged)
                    {
                        Console.Write("\nPrikaz(help): ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        string cmd = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.White;

                        switch (cmd)
                        {
                            case "set":
                                Console.Write("Cislo uctu, na ktery chcete penize nastavit: ");
                                int accountId = int.Parse(Console.ReadLine());
                                Console.Write("Kod banky, u ktere je ucet zrizeny: ");
                                int BankId = int.Parse(Console.ReadLine());
                                Console.Write("Castka, ktera ma byt pripsana: ");
                                int castka = int.Parse(Console.ReadLine());

                                Commands.SetMoney(accountId, BankId, castka);

                                break;

                            case "get":
                                Console.Write("Jmeno uctu: ");
                                string searchAccountName = Console.ReadLine();

                                Commands.GetAccount(searchAccountName);

                                break;

                            case "create":
                                Commands.CreateAccount();
                                break;

                            case "help":
                                Commands.Help();
                                break;

                            case "exit":
                                Commands.LogOut();
                                break;

                            default:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Zadany prikaz neexistuje.");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                        }
                    }
                }
                else 
                {
                    foreach (var account in accounts)
                    {
                        if (name == account.name && password == account.password)
                        {
                            string loggedName = account.name;
                            int id = account.id;

                            logged = true;
                            Console.WriteLine("\nByl/a jste uspesne prihlasen! Vitejte {0}\n", account.fullName);

                            while (logged)
                            {
                                Console.WriteLine("1. Zaplatit/Poslat penize");
                                Console.WriteLine("2. Informace o uctu");
                                Console.WriteLine("3. Historie transakci");
                                Console.WriteLine("4. Odhlasit se");

                                int option = int.Parse(Console.ReadLine());

                                switch (option) {
                                    case 1:
                                        Console.Write("\nZadejte cislo uctu, na ktery maji byt penize poslany: ");
                                        int accountId = int.Parse(Console.ReadLine());
                                        Console.Write("Zadejte kod banky, u ktere je ucet zrizeny: ");
                                        int bankId = int.Parse(Console.ReadLine());
                                        Console.Write("Zadejte castku: ");
                                        int amount = int.Parse(Console.ReadLine());

                                        Console.WriteLine("\n" + UserCommands.SendMoney(accountId, bankId, amount, id) + "\n");
                                        break;

                                    case 2:
                                        UserCommands.GetAccountInfo(loggedName);
                                        break;

                                    case 3:
                                        foreach (var loggedAccount in accounts)
                                        {
                                            if (loggedAccount.name == loggedName)
                                            {
                                                foreach (var action in accounts)
                                                {
                                                    if(action.transactionHistory.Count > 0)
                                                    {
                                                        foreach (var transaction in action.transactionHistory)
                                                        {
                                                            Console.ForegroundColor = ConsoleColor.Cyan;
                                                            Console.WriteLine("\n" + transaction + "\n");
                                                            Console.ForegroundColor = ConsoleColor.White;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        break;

                                    case 4:
                                        logged = false;
                                        id = 0;
                                        loggedName = String.Empty;
                                        break;

                                    default:
                                        Console.WriteLine("\nZadana moznost neexistuje!\n");
                                        break;
                                }
                            }
                        }
                    }
                }                      
            }
        }
    }
}
