using System;
using System.Collections.Generic;
using System.Text;

namespace LedgerApp.Models
{
    class Ledger
    {
        public Ledger()
        {
            Users = new List<User>();
        }
        public List<User> Users { get; set; }
    }
}
