using System;

namespace MyBank
{
    //Zde jsou všechny příkazy pro běžné uživatele
    class UserCommands
    {
        //Pošle peníze na zadaní účet podle kódu banky
        public static string SendMoney(int id, int bankId, int amount, int senderId, int senderBankId) 
        {
            foreach (var receiver in Program.accounts)
            {
                if (receiver.ID == id && receiver.BankID == bankId)
                { 
                    receiver.Money += amount;

                    receiver.transactionHistory.Add(new Transaction() { Id = receiver.TransactionCount++, Name = "Platba prijata", Amount = amount, Sender = senderId.ToString(), SenderBankId = senderBankId, Receiver = id.ToString() });
                }
            }

            foreach (var sender in Program.accounts)
            {
                if (sender.ID == senderId)
                {
                    sender.Money -= amount;

                    sender.transactionHistory.Add(new Transaction() { Id = sender.TransactionCount++, Name = "Platba odeslana", Receiver = id.ToString(), Amount = amount, ReceiverBankId = bankId, Sender = senderId.ToString()});

                    return "Platba byla uspesne provedena";
                }
            }

            return "Platba neprobehla uspesne";
        }

        //Získá informace o vlastním účtu
        public static void GetAccountInfo(string name)
        {
            foreach (var account in Program.accounts)
            {
                if (name == account.Name)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nJmeno a prijmeni: {0}\nUzivatelske jmeno: {1}\nHeslo: {2}\nCislo uctu: {3}\nCislo banky: {4}\nPenezni castka: {5} CZK\n", account.Fullname, account.Name, account.Password, account.ID, account.BankID, account.Money);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}
