using System;

namespace MyBank
{
    //Zde jsou všechny příkazy pro běžné uživatele
    class UserCommands
    {
        public static string SendMoney(int id, int bankId, int amount, int senderId, int senderBankId) 
        {
            foreach (var receiver in Program.accounts)
            {
                if (receiver.id == id && receiver.bankId == bankId)
                { 
                    receiver.money += amount;

                    receiver.transactionHistory.Add(new Transaction() { Id = receiver.transactionCount++, Name = "Platba prijata", Amount = amount, Sender = senderId.ToString(), SenderBankId = senderBankId, Receiver = id.ToString() });
                }
            }

            foreach (var sender in Program.accounts)
            {
                if (sender.id == senderId)
                {
                    sender.money -= amount;

                    sender.transactionHistory.Add(new Transaction() { Id = sender.transactionCount++, Name = "Platba odeslana", Receiver = id.ToString(), Amount = amount, ReceiverBankId = bankId, Sender = senderId.ToString()});

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
