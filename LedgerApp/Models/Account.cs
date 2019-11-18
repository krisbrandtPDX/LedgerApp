using System;
using System.Collections.Generic;
using System.Text;

namespace LedgerApp.Models
{
    class Account
    {
        public Account()
        {
            Transactions = new List<Transaction>();
        }
        public string Name { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
