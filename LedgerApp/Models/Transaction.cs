using System;
using System.Collections.Generic;
using System.Text;

namespace LedgerApp.Models
{
    class Transaction
    {
        public Transaction()
        {
            Timestamp = DateTime.Now;
        }
        public double Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
