//Petr Grajciar, 23. 9. 2023

using System;
using System.Collections.Generic;

namespace MyBank
{
    class Program
    {
        public static bool adminLogged = false;
        //List se všemi klientskými účty
        public static List<Account> accounts = new List<Account>();
        //Vytvoření statické instance ze třídy Account pro přihlášeného uživatele
        public static Account loggedAccount;

        //Hlavní funkce, která se spustí při zapnutí programu
        static void Main(string[] args)
        {       
            Console.WriteLine("========== MyBank ==========");

            bool stav = true, logged = false;
            string name, password;  

            accounts.Add(new Account() { ID = 1, Name = "default", Password = "default", Fullname = "Default account", Money = 1000000, BankID = 1234 });
            accounts.Add(new Account() { ID = 2, Name = "petr", Password = "petr", Fullname = "Petr Grajciar", Money = 1000, BankID = 1234 });
            accounts.Add(new Account() { ID = 3, Name = "jiri", Password = "jiri", Fullname = "Jiri Novak", Money = 10000, BankID = 1234 });

            //Nekonečný cyklus pro přihlašování
            while (stav)
            {
                //Přihlašovací sekce
                Console.Write("\nZadejte sve uzivatelske jmeno('exit' pro ukonceni programu): ");
                Console.ForegroundColor = ConsoleColor.Yellow;

                try
                {
                    name = Console.ReadLine();
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    name = "exit";
                }

                Console.ForegroundColor = ConsoleColor.White;

                //Pokud na místo jména napíšeme 'exit' tak se program vypne
                if (name == "exit")
                {
                    stav = false;
                    Console.Write("Vase jmeno: ");
                    string nacteniJmena = Console.ReadLine();

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(nacteniJmena);
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.ReadKey();
                    break;
                }

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
                                Console.Write("ID uctu: ");
                                int searchAccountID = int.Parse(Console.ReadLine());

                                Commands.GetAccount(searchAccountID);
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
                        if (name == account.Name && password == account.Password)
                        {
                            //Přiřazení dat o přihlášeném uživateli do třídy
                            loggedAccount = account;
                            logged = true;
                            Console.WriteLine("\nByl/a jste uspesne prihlasen! Vitejte {0}\n", account.Fullname);

                            //Pokud je uživatel přihlášený, tak se spustí nekonečný cyklus s dalšími příkazy
                            while (logged)
                            {

                                string[] prikazy = { "1. Zaplatit/Poslat penize", "2. Informace o uctu", "3. Historie transakci", "4. Odhlasit se" };

                                foreach (string prikaz in prikazy) Console.WriteLine(prikaz);
                                
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("\nCislo akce: ");
                                Console.ForegroundColor = ConsoleColor.White;
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

                                        Console.WriteLine("\n" + UserCommands.SendMoney(accountId, bankId, amount, loggedAccount.ID, loggedAccount.BankID) + "\n");
                                        break;

                                    //Získání informací o vlastním účtu
                                    case 2:
                                        UserCommands.GetAccountInfo(loggedAccount.Name);
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
