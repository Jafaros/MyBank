using System;
using System.Collections.Generic;

namespace MyBank
{
    //Konstruktor pro vytvoření běžného účtu
    public class Account
    {
        public int id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string fullName { get; set; }
        public int bankId { get; set; }
        public double money { get; set; }
        public int transactionCount = 0;

        //List se všemi transakcemi(příchozí i odchozí)
        public List<Transaction> transactionHistory { get; set; }

        public Account() 
        { 
            transactionHistory = new List<Transaction>();
        }
    }
}
