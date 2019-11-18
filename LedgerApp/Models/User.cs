using System;
using System.Collections.Generic;
using System.Text;

namespace LedgerApp.Models
{
    class User
    {
        public User()
        {
            Accounts = new List<Account>();
        }
        public string Name { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
