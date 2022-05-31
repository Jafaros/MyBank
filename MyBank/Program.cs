//Petr Grajciar, 3.B, 31. 5. 2022, PVA, MyBank

using System;
using System.Collections.Generic;

namespace MyBank
{
    class Program
    {
        public static bool adminLogged = false;
        public static List<Account> accounts = new List<Account>();
        public static Account loggedAccount;

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
                //Přihlašovací sekce
                Console.Write("\nZadejte sve uzivatelske jmeno: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                name = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("Zadejte sve heslo: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                password = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;

                //Kontrola přihlašovacích údajů, zda se nepřihlašujete jako admin
                if (name == Admin.name && password == Admin.password)
                {
                    adminLogged = true;

                    while (adminLogged)
                    {
                        //Načtení příkazu
                        Console.Write("\nPrikaz(help): ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        string cmd = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.White;

                        //Příkazy pro administrátory
                        switch (cmd)
                        {
                            //Nastaví peněžní částku zadanému účtu
                            case "set":
                                Console.Write("Cislo uctu, na ktery chcete penize nastavit: ");
                                int accountId = int.Parse(Console.ReadLine());
                                Console.Write("Kod banky, u ktere je ucet zrizeny: ");
                                int BankId = int.Parse(Console.ReadLine());
                                Console.Write("Castka, ktera ma byt pripsana: ");
                                int castka = int.Parse(Console.ReadLine());

                                Commands.SetMoney(accountId, BankId, castka);

                                break;

                            //Získá informace o zadaném účtu
                            case "get":
                                Console.Write("Jmeno uctu: ");
                                string searchAccountName = Console.ReadLine();

                                Commands.GetAccount(searchAccountName);
                                break;

                            //Vytvoří nový účet v bance
                            case "create":
                                Commands.CreateAccount();
                                break;

                            //Příkaz, který zobrazí nápovědu
                            case "help":
                                Commands.Help();
                                break;
                            
                            //Příkaz, který odhlásí admina
                            case "exit":
                                Commands.LogOut();
                                break;

                            //Hláška, která se spustí když zadaný příkaz neexistuje
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
                        //Kontrola zda se uživatel přihlásil jako klient
                        if (name == account.name && password == account.password)
                        {
                            //Přiřazení dat o přihlášeném uživateli do třídy
                            loggedAccount = account;
                            logged = true;
                            Console.WriteLine("\nByl/a jste uspesne prihlasen! Vitejte {0}\n", account.fullName);

                            while (logged)
                            {
                                Console.WriteLine("1. Zaplatit/Poslat penize");
                                Console.WriteLine("2. Informace o uctu");
                                Console.WriteLine("3. Historie transakci");
                                Console.WriteLine("4. Odhlasit se");

                                int option = int.Parse(Console.ReadLine());

                                //Switch s dovolenými příkazy pro uživatele
                                switch (option) {
                                    //Poslání peněz na zadaný účet
                                    case 1:
                                        Console.Write("\nZadejte cislo uctu, na ktery maji byt penize poslany: ");
                                        int accountId = int.Parse(Console.ReadLine());
                                        Console.Write("Zadejte kod banky, u ktere je ucet zrizeny: ");
                                        int bankId = int.Parse(Console.ReadLine());
                                        Console.Write("Zadejte castku: ");
                                        int amount = int.Parse(Console.ReadLine());

                                        Console.WriteLine("\n" + UserCommands.SendMoney(accountId, bankId, amount, loggedAccount.id, loggedAccount.bankId) + "\n");
                                        break;

                                    //Získání informací o vlastním účtu
                                    case 2:
                                        UserCommands.GetAccountInfo(loggedAccount.name);
                                        break;

                                    //Výpis všech transakcí
                                    case 3:
                                        foreach (var transaction in loggedAccount.transactionHistory)
                                        {
                                            if (transaction.Name == "Platba prijata")
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                            }

                                            if (transaction.Name == "Platba odeslana")
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                            }

                                            Console.WriteLine(transaction.ToString());
                                            Console.ForegroundColor = ConsoleColor.White;
                                        }
                                        break;

                                    //Odhlásí uživatele
                                    case 4:
                                        logged = false;
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
