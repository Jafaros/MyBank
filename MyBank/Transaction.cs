using System;
using System.Collections.Generic;

namespace MyBank
{
    //Kontruktor pro transakci
    public class Transaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Receiver { get; set; }
        public string Sender { get; set; }
        public int Amount { get; set; }
        public int SenderBankId { get; set; }
        public int ReceiverBankId { get; set; }

        //Přepíše funkci ToString() do vlastního tvaru
        public override string ToString()
        {
            return $"\n Jmeno: {Name}\n Cislo transakce: {Id}\n Prijemce: {Receiver}\n Odesilatel: {Sender}\n Castka: {Amount}\n";
        }
    }
}
