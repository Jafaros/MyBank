using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank
{
    class UserCommands
    {
        public static string SendMoney(int id, int bankId, int amount, int senderId) 
        {
            foreach (var receiver in Program.accounts)
            {
                if (receiver.id == id && receiver.bankId == bankId)
                { 
                    receiver.money += amount;
                }
            }

            foreach (var sender in Program.accounts)
            {
                if (sender.id == senderId)
                {
                    sender.money -= amount;

                    sender.transactionHistory.Add(" Transakce cislo: " + 0001 + "\n Na ucet: " + id + "\n Kod banky: " + bankId + "\n Castka: " + amount + " CZK");

                    return "Platba byla uspesne provedena";
                }
            }

            return "Platba neprobehla uspesne";
        }

        public static void GetAccountInfo(string name)
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
    }
}
