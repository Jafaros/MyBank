using System;
using System.Collections.Generic;

namespace MyBank
{
    //Konstruktor pro vytvoření běžného účtu
    public class Account
    {
        // Proměnné mají modifikátor přístupu private, což znamená že jsou přístupny pouze ,,zevnitř" této třídy
        private int id { get; set; }
        private string name { get; set; }
        private string password { get; set; }
        private string fullName { get; set; }
        private int bankId { get; set; }
        private double money { get; set; }
        private int transactionCount = 0;

        public int ID {
            get { return id; }
            set { id = value; }
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public string Password {
            get { return password; }
            set { password = value; }
        }

        public string Fullname { 
            get { return fullName; }
            set { fullName = value; }
        }

        public int BankID { 
            get { return bankId; }
            set { bankId = value; }
        } 

        public double Money { 
            get { return money; }
            set { money = value; }
        }

        public int TransactionCount { 
            get { return transactionCount; }
            set { transactionCount = value; }
        }

        //List se všemi transakcemi(příchozí i odchozí)
        public List<Transaction> transactionHistory { get; set; }

        public Account() 
        { 
            transactionHistory = new List<Transaction>();
        }
    }
}
