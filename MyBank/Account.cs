using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank
{
    public class Account
    {
        public int id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string fullName { get; set; }
        public int bankId { get; set; }
        public double money { get; set; }

        public List<string> transactionHistory = new List<string>();
    }
}
